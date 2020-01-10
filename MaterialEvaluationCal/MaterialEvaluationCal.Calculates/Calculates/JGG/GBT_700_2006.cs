using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections;

namespace Calculates
{
    public partial class GBT_700_2006 : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/

            #region 代码开始
            /* 
             * 计算项目：碳素结构钢
             *  参考标准：
             *  GB/T 700-2006
             * GB/T 228
             * GB/T 232
             */
            



            var inspectionItems = retData.Select(u => u.Key).ToArray();
            double mj, cd, zj;
            var nonConformityNum = new Dictionary<string, int>(){ { "FS", 0 },{ "LS", 0 },{ "CJ", 0 },{ "CJminNum", 0 }, { "BHG", 0 }  };//单组不合格个数
            int baseNum = 1;
            foreach (var JCXMitem in inspectionItems)
            {
                var tableItems = retData[JCXMitem]["S_JGG"];
                var s_tableItem = retData[JCXMitem]["S_BY_RW_XQ"];
                var ExtraTable = dataExtra["BZ_JGG_DJ"];
                foreach (var rowData in tableItems)//单行数据进行验证 key 字段名 val值
                {
                    int index = tableItems.IndexOf(rowData);
                    if (index >= retData[JCXMitem]["S_BY_RW_XQ"].Count())
                        throw new Exception("未找到对应的任务详情数据");
                    var rwxqRow = retData[JCXMitem]["S_BY_RW_XQ"][index];
                    //筛选牌号
                    if (string.IsNullOrEmpty(rowData["GCLX_PH"]))
                    {
                        throw new Exception("钢材牌号不存在");
                    }
                    if (!dataExtra.ContainsKey("BZ_JGG_DJ"))
                    {
                        throw new Exception("【BZ_JGG_DJ】 表数据不存在");
                    }
                    #region 计算直径范围
                    var md = GetSafeDouble(rowData["HD"]) > 0 ? GetSafeDouble(rowData["HD"]) : BaseMethods.GetSafeDouble(rowData["ZJ"]);
                    string hd_fw = "";
                    int sjdjmin = (rowData["SJDJ"] == "碳素结构钢" ? 60 : 63);
                    if (md <= 16)
                        hd_fw = "≤16";
                    else if (md <= 40)
                        hd_fw = "＞16≤40";
                    else if (md <= sjdjmin)
                        hd_fw = "＞40≤" + sjdjmin.ToString();
                    else if (md <= 80)
                        hd_fw = "＞" + sjdjmin.ToString() + "≤80";
                    else if (md <= 100)
                        hd_fw = "＞80≤100";
                    else if (md <= 150)
                        hd_fw = "＞100≤150";
                    else if (md <= 200)
                        hd_fw = "＞150≤200";
                    else if (md <= 250)
                        hd_fw = "＞200≤250";
                    else
                        hd_fw = "";
                    #endregion

                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraItem = ExtraTable.FirstOrDefault(u => (u.ContainsKey("PH") && u.Values.Contains(rowData["GCLX_PH"]))
                    && (u.ContainsKey("MC") && u.Values.Contains(rowData["SJDJ"]))
                    && (u.ContainsKey("ZJM") && u.Values.Contains(hd_fw)));
                    //未找到指定牌号的标准数据，取消判断
                    if (extraItem == null)
                    {
                        //依据不详
                        continue;
                    }
                    //获取主表数据
                    //var s_tableItem = retData["碳素结构钢"]["MJGG"].FirstOrDefault(u => u.ContainsKey("RECID") && u.Values.Contains(rowDate["RECID"]));
                    //if (s_tableItem == null)
                    //    continue;//未找到主表数据不做判断
                    //检验项目

                    #region 计算面积
                    zj = GetSafeDouble(rowData["ZJ"]);
                    if (zj == 0)
                    {
                        rowData["GG"] = $"宽:{rowData["KD"]}厚:{rowData["HD"]}";//规格
                        mj = Math.Round(GetSafeDouble(rowData["KD"]) * GetSafeDouble(rowData["HD"]), 3);
                        cd = Convert.ToInt32(5.65 * Math.Sqrt(mj));
                    }
                    else
                    {
                        mj = Math.Round(3.14159 * Math.Pow(zj / 2, 2), 3);
                        rowData["GG"] = "Φ" + rowData["ZJ"];//规格
                        cd = Convert.ToInt32(GetSafeDouble(rowData["XGM"]) * zj);
                    }
                    rowData["MJ"] = mj.ToString();
                    rowData["CD"] = cd.ToString();
                    #endregion
                    switch (JCXMitem)
                    {
                        case "拉伸":
                            //求单组屈服强度,抗拉强度,伸长率 合格个数, 并且返回值为不同组不合格数的累加值
                            #region 抗拉强度判定
                            // 抗拉强度验证 如KLQD1,KLQD2,KLQD3
                            var checkKLQDItems = rowData.Where(u => u.Key.StartsWith("KLQD")).ToArray();
                            /*
                             * 计算面积
                             * 有直径的话，用直径计算：3.14159*（直径/2）^2
                             * 没有直径的用厚度*宽度
                             */
                            //计算抗拉强度 ： 抗拉荷重/面积 精确到1
                            var klqd1 = mj > 0.0001 ? Convert.ToInt32(1000 * GetSafeDouble(rowData["KLHZ1"]) / mj) : 0;
                            rowData["KLQD1"] = klqd1.ToString();
                            //厚度大于100mm的，抗拉强度允许下降20    
                            //带宽钢（包括剪切钢板）抗拉强度上限不做交货条件  [暂未判定]*
                            var zJjudge = md > 100;
                            if ((GetSafeInt(extraItem["KLQDBZZ"]) - (zJjudge ? 20 : 0) <= klqd1) && ((!zJjudge) || GetSafeInt(extraItem["KLQDBZZ1"]) >= klqd1))
                            {
                                rowData["HG_KL1"] = "符合";
                                rowData["HG_KL"] = (GetSafeInt(rowData["HG_KL"]) + 1).ToString();
                            }
                            else
                            {
                                rowData["HG_KL1"] = "不符合";
                                nonConformityNum["LS"]++;
                            }
                            #endregion
                            #region 断后伸长率判定
                            //伸长率验证 如SCL1,SCL2,SCL3
                            var checkSCLItems = rowData.Where(u => u.Key.StartsWith("SCL")).ToArray();
                            var mscl = GetSafeDouble(extraItem["SCLBZZ"]);
                            //做拉伸和冷弯实验时，型钢和钢棒取纵向式样；钢板和钢带取横向式样，断后伸长率允许比表2降低2%（绝对值）
                            if ((rowData["GCLX_LB"].Contains("板") || rowData["GCLX_LB"].Contains("带")) && (rowData["QYFX"] == "横向"))
                            {
                                mscl = mscl - 2;
                            }

                            if (mj > 0)
                            {
                                if (GetSafeDouble(rowData["SCZ1"]) == 0)
                                    rowData["SCL1"] = "----";
                                else
                                    rowData["SCL1"] = Convert.ToInt32(200 * (GetSafeDouble(rowData["SCZ1"]) - cd) / cd / 2).ToString();
                            }
                            else
                            {
                                rowData["SCL1"] = "0";
                            }
                            if (mscl == 0)
                                rowData["SCL1"] = "----";

                            //伸长率取样1个
                            if (mscl <= GetSafeDouble(rowData["SCL1"]))
                            {
                                rowData["HG_SC1"] = "符合";
                                rowData["HG_SC"] = (GetSafeInt(rowData["HG_SC"]) + 1).ToString();
                            }
                            else
                            {
                                rowData["HG_SC1"] = "不符合";
                                nonConformityNum["LS"]++;
                            }
                            #endregion
                            #region 屈服强度判定
                            //屈服强度取样1个 如QFQD1,QFQD2,QFQD3
                            //计算屈服强度 ： 屈服荷重/面积 精确到1
                            var QFQDval = mj > 0.0001 ? Convert.ToInt32(1000 * GetSafeDouble(rowData["QFQD1"]) / mj) : 0;
                            rowData["QFQD1"] = QFQDval.ToString("0.0");

                            //更新指定的数据
                            if (GetSafeInt(extraItem["QFQDBZZ"].ToString()) <= QFQDval)
                            {
                                rowData["HG_QF1"] = "符合";
                                rowData["HG_QF"] = (GetSafeInt(rowData["HG_QF"]) + 1).ToString();
                            }
                            else
                            {
                                rowData["HG_QF1"] = "不符合";
                                nonConformityNum["LS"]++;
                            }
                            #endregion
                            #region 更新
                            //首次检验不符合需要复试
                            rwxqRow["JCJG"] = "";
                            rwxqRow["JCJGMS"] = "";
                            if (nonConformityNum["LS"] > 0)
                            {
                                rowData["JCJG_LS"] = "不符合";
                                rwxqRow["JCJG"] = "异常";
                                rwxqRow["JCJGMS"] = $"抗拉强度{(rowData["HG_KL1"] == "不符合" ? "不符合，另取双倍样复试" : rowData["HG_KL1"])},断后伸长率{(rowData["HG_SC1"] == "不符合" ? "不符合，另取双倍样复试" : rowData["HG_SC1"])},屈服强度{(rowData["HG_QF1"] == "不符合" ? "不符合，另取双倍样复试" : rowData["HG_QF1"])}。";
                                nonConformityNum["FS"]++;
                            }
                            else
                            {
                                rowData["JCJG_LS"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目【拉伸】符合{extraItem["PDBZ"]}标准要求";
                            }
                            rowData["G_QFQD"] = extraItem["QFQDBZZ"];
                            rowData["G_KLQD"] = extraItem["KLQDBZZ"];
                            rowData["G_KLQD1"] = extraItem["KLQDBZZ1"];
                            rowData["G_SCL"] = extraItem["SCLBZZ"];
                            #endregion
                            break;
                        case "冷弯":
                            #region 冷弯判定
                            var LwBzyq = "";
                            var mlwzj = GetSafeDouble(extraItem["LWZJ"]);
                            if (!(rowData["GCLX_LB"].Contains("板") || rowData["GCLX_LB"].Contains("带")) && extraItem["PDBZ"].Contains("GB 700－2006《碳素结构钢》"))
                            {
                                mlwzj = mlwzj - 0.5;
                            }
                            if (!(rowData["GCLX_LB"].Contains("板") || rowData["GCLX_LB"].Contains("带")) && (rowData["QYFX"] == "横向") && extraItem["PDBZ"].Contains("GB/T 700-2006《碳素结构钢》"))
                            {
                                mlwzj = mlwzj - 2;
                            }
                            if (mlwzj == 0 && GetSafeDouble(extraItem["FFWQCS"]) != 0)
                                LwBzyq = $"弯曲次数不小于" + extraItem["FFWQCS"] + "次，受弯曲部位表面无裂纹。";
                            else
                            {
                                if (mlwzj == 0)
                                    LwBzyq = $"弯心直径d=0,弯曲" + extraItem["LWJD"] + "度后受弯曲部位表面无裂纹。";
                                else
                                {
                                    LwBzyq = $"{(extraItem["JCYJ"].Contains("1999") ? "弯心直径d" : "弯曲压头直径D")}{(GetSafeDouble(extraItem["LWZJ"]) < 1 ? "=0" : "=")}{mlwzj}a弯曲{extraItem["LWJD"]}度后受弯曲部位表面无裂纹。";
                                }
                            }
                            //钢材厚度或直径大于100mm时，弯曲试验由双方协定 
                            if (md > 100)
                                break;
                            //冷弯取样1个
                            if (GetSafeDouble(rowData["LW1"]) == 1)
                            {
                                rowData["HG_LW1"] = "符合";
                                rowData["HG_LW"] = (GetSafeInt(rowData["HG_LW"]) + 1).ToString();
                                rowData["JCJG_LW"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目【冷弯】符合{extraItem["PDBZ"]}标准要求";
                            }
                            else
                            {
                                rowData["HG_LW1"] = "不符合";
                                rowData["JCJG_LW"] = "不符合";
                                rwxqRow["JCJG"] = "异常";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目【冷弯】不符合{extraItem["PDBZ"]}标准要求，另取双倍样复试";
                                nonConformityNum["FS"]++;
                            }

                            rowData["G_LWWZ"] = LwBzyq;

                            #endregion
                            break;
                        case "冲击试验":
                            #region 冲击试验（V型缺口）
                            baseNum = 1;
                            /*
                             * Q195,其他A级钢材可以不考虑冲击功实验
                             * 厚度小于25mm的Q235B级冲击钢，如供方能保证冲击吸收功合格，经需方同意，可不做检验
                             */
                            if (rowData["GCLX_PH"].Contains("Q195") || rowData["GCLX_PH"].Contains("A")
                               //|| (rowDate["GCLX_PH"] == "235B" && rowDate["ZJFW"] == "235B")
                               )
                                break;

                            var checkCJItems = rowData.Where(u => u.Key.StartsWith("CJ")).ToArray();
                            var avgCJ = checkCJItems.Take(3).Sum(u => GetSafeDouble(u.Value)) / 3;

                            var mCJBZZ = GetSafeDouble(extraItem["CJBZZ"].ToString());
                            foreach (var checkItem in checkCJItems)
                            {
                                //冲击取样3个
                                if (baseNum > 3)
                                    break;
                                /*
                                 * 厚度不小于12mm或直径不小于16mm的钢材应做冲击功实验
                                 * 实验取样1组3个，按试样单值的算术平均值计算，允许其中一个试样的单值低于规定值，但不得低于70%(当采用10mm*5mm*55mm试样时，应不小于规定值的50%)
                                 * 如未满足上述条件，可从同一抽样产品上在抽取1组3个进行实验，先后6个试样的平均值不得低于规定值，允许有2个低于规定值，但低于规定值70%的试样只允许有一个
                                 */
                                if (mCJBZZ <= avgCJ)
                                {
                                    if (mCJBZZ < GetSafeDouble(checkItem.Value))
                                    {
                                        rowData["HG_CJ" + baseNum] = "符合";
                                        rowData["HG_CJ"] = (GetSafeInt(rowData["HG_CJ"]) + 1).ToString();
                                    }
                                    else
                                    {
                                        rowData["HG_CJ" + baseNum] = "不符合";
                                        if (mCJBZZ * (rowData["GGXH"].Replace("m", "").Contains("10*5*55") ? 0.5 : 0.7) > GetSafeDouble(checkItem.Value))
                                        {
                                            nonConformityNum["CJminNum"]++;//低于规定值且超出 50%/70% 的范围的复验（复验的记不合格）
                                        }
                                        else
                                        {
                                            nonConformityNum["CJ"]++;//低于规定值但在范围内,允许1个（复验允许2个）
                                        }
                                    }
                                }
                                else
                                {
                                    rowData["HG_CJ" + baseNum] = "不符合";
                                    nonConformityNum["CJ"]++;
                                }
                                baseNum++;
                            }
                            //更新从表数据 冲击不合格判断
                            if (nonConformityNum["CJminNum"] > 1 || nonConformityNum["CJ"] > 2)
                            {
                                rowData["JCJG_CJ"] = "不符合";
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目【冲击】不符合{extraItem["PDBZ"]}标准要求";
                                nonConformityNum["BHG"] = 1;
                            }
                            else if (nonConformityNum["CJ"] > 1)
                            {
                                rowData["JCJG_CJ"] = "复试";
                                rwxqRow["JCJG"] = "异常";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目【冲击】部分符合{extraItem["PDBZ"]}标准要求，另取双倍样复试";
                                nonConformityNum["FS"]++;
                            }
                            else
                            {
                                rowData["JCJG_CJ"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目【冲击】符合{extraItem["PDBZ"]}标准要求";
                            }
                            rowData["G_CJ"] = extraItem["CJBZZ"];
                            #endregion
                            break;
                        default:
                            break;
                    }
                    
                    #region 更新主表检测结果
                    IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
                    IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
                    if(nonConformityNum["FS"] > 0 || nonConformityNum["BHG"] > 0)
                    {
                        bgjgDic.Add("JCJG", "不合格");
                        if(nonConformityNum["BHG"] > 0)
                            bgjgDic.Add("JCJGMS", $"该组试样所检项目不符合{ExtraTable.FirstOrDefault()["PDBZ"]}标准要求。");
                        else
                            bgjgDic.Add("JCJGMS", $"该组试样所检项目部分符合{ExtraTable.FirstOrDefault()["PDBZ"]}标准要求，另取双倍样复试。");
                    }
                    else
                    {
                        bgjgDic.Add("JCJG", "合格");
                        bgjgDic.Add("JCJGMS", $"该组试样所检项目符合{ExtraTable.FirstOrDefault()["PDBZ"]}标准要求。");
                    }
                    
                    bgjg.Add(bgjgDic);
                    retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
                    retData["BGJG"].Add("BGJG", bgjg);
                    #endregion
                }
            }

            return true;
            #endregion

            /************************ 代码结束 *********************/
        }
    }
}
