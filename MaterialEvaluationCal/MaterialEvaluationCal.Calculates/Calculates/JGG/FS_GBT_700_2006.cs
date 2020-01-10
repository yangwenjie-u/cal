using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Collections;

namespace Calculates
{
    public partial class FS_GBT_700_2006 : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {

            /************************ 代码开始 *********************/
            /* 
             * 计算项目：碳素结构钢复验
             *  参考标准：
             *  GB/T 700-2006
             * GB/T 228
             * GB/T 232
             */
            var tables = retData["碳素结构钢"];
            var tableItems = tables["SJGG"];
            foreach (var rowData in tableItems)//单行数据进行验证 key 字段名 val值
            {
                //Check_JGG(dataExtra, dicFields);
                //筛选牌号
                if (string.IsNullOrEmpty(rowData["GCLX_PH"]))
                {
                    throw new Exception("钢材牌号不存在");
                }
                if (!dataExtra.ContainsKey("JGGDJ"))
                {
                    throw new Exception("【JGGDJ】 表数据不存在");
                }
                var ExtraTable = dataExtra["JGGDJ"];
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
                var s_tableItem = retData["碳素结构钢"]["MJGG"].FirstOrDefault(u => u.ContainsKey("RECID") && u.Values.Contains(rowData["RECID"]));
                if (s_tableItem == null)
                    continue;//未找到主表数据不做判断
                //检验项目
                if (!string.IsNullOrEmpty(rowData["JCXM"]))
                {
                    var inspectionItems = rowData["JCXM"].Split(',', '、');
                    int baseNum = 1;//初始验证次数
                    var nonConformityNum = new Dictionary<string, int>();//单组不合格个数
                    string checkFS = s_tableItem["SFFS"].ToLower() ?? "false";
                    string checkQDFS = s_tableItem["QDFS"].ToLower() ?? "false";
                    string checkLWFS = s_tableItem["LWFS"].ToLower() ?? "false";
                    string checkCJFS = s_tableItem["CJFS"].ToLower() ?? "false";

                    #region 计算面积
                    double mj = 0.0, cd = 0.0, zj = 0.00;
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

                    foreach (var JCXMitem in inspectionItems)
                    {
                        switch (JCXMitem)
                        {
                            case "拉伸":
                                nonConformityNum["LS"] = 0;
                                baseNum = 1;
                                //求单组屈服强度,抗拉强度,伸长率,冷弯 合格个数, 并且返回值为不同组不合格数的累加值
                                #region 抗拉强度判定
                                // 抗拉强度验证 如KLQD1,KLQD2,KLQD3
                                var checkKLQDItems = rowData.Where(u => u.Key.StartsWith("KLQD")).ToArray();
                                foreach (var checkItem in checkKLQDItems)
                                {
                                    //抗拉复验取样个
                                    if (baseNum > 2)
                                        break;

                                    //计算抗拉强度 ： 抗拉荷重/面积
                                    var KLQDval = mj > 0.0001 ? 1000 * GetSafeDouble(rowData["KLHZ" + baseNum]) / mj : 0;
                                    rowData["KLQD" + baseNum] = KLQDval.ToString();

                                    //厚度大于100mm的，抗拉强度允许下降20    
                                    //带宽钢（包括剪切钢板）抗拉强度上限不做交货条件  [暂未判定]*
                                    var zJjudge = md > 100;
                                    if ((GetSafeInt(extraItem["KLQDBZZ"]) - (zJjudge ? 20 : 0) <= KLQDval) && ((!zJjudge) || GetSafeInt(extraItem["KLQDBZZ1"]) >= KLQDval))
                                    {
                                        rowData["HG_KL" + baseNum] = "true";
                                        rowData["HG_KL"] = (GetSafeInt(rowData["HG_KL"]) + 1).ToString();
                                    }
                                    else
                                    {
                                        rowData["HG_KL" + baseNum] = "false";
                                        nonConformityNum["LS"]++;
                                    }
                                    baseNum++;
                                }
                                #endregion
                                #region 断后伸长率判定
                                //伸长率验证 如SCL1,SCL2,SCL3
                                baseNum = 1;
                                var checkSCLItems = rowData.Where(u => u.Key.StartsWith("SCL")).ToArray();
                                var mscl = GetSafeDouble(extraItem["SCLBZZ"]);
                                foreach (var checkItem in checkSCLItems)
                                {
                                    //伸长率复验取样2个
                                    if (baseNum > 2)
                                        break;
                                    //做拉伸和冷弯实验时，型钢和钢棒取纵向式样；钢板和钢带取横向式样，断后伸长率允许比表2降低2%（绝对值）
                                    if ((rowData["GCLX_LB"].Contains("板") || rowData["GCLX_LB"].Contains("带")) && (rowData["QYFX"] == "横向"))
                                    {
                                        mscl = mscl - 2;
                                    }

                                    if (mj > 0)
                                    {
                                        if (GetSafeDouble(rowData["SCZ1"]) == 0)
                                            rowData["SCL" + baseNum] = "----";
                                        else
                                            rowData["SCL" + baseNum] = (Math.Round(200 * (GetSafeDouble(rowData["SCZ"] + baseNum) - cd) / cd, 0) / 2).ToString();
                                    }
                                    else
                                    {
                                        rowData["SCL" + baseNum] = "0";
                                    }
                                    if (mscl == 0)
                                        rowData["SCL" + baseNum] = "----";

                                    //伸长率取样1个
                                    if (mscl <= GetSafeDouble(rowData["SCL" + baseNum]))
                                    {
                                        rowData["HG_SC" + baseNum] = "true";
                                        rowData["HG_SC"] = (GetSafeInt(rowData["HG_SC"]) + 1).ToString();
                                    }
                                    else
                                    {
                                        rowData["HG_SC" + baseNum] = "false";
                                        nonConformityNum["LS"]++;
                                    }
                                    baseNum++;
                                }
                                #endregion
                                #region 屈服强度判定
                                // 屈服强度验证 如QFQD1,QFQD2,QFQD3
                                baseNum = 1;
                                //Q195 的屈服强度仅供参考，不做交货条件
                                if (rowData["GCLX_PH"].Contains("Q195"))
                                    continue;

                                var checkQFQDItems = rowData.Where(u => u.Key.StartsWith("QFQD")).ToArray();
                                foreach (var checkItem in checkQFQDItems)
                                {
                                    //屈服强度复验取样2个
                                    if (baseNum > 2)
                                        break;
                                    //计算屈服强度 ： 屈服荷重/面积
                                    var QFQDval = mj > 0.0001 ? Convert.ToInt32(1000 * GetSafeDouble(rowData["QFQD" + baseNum]) / mj) : 0;
                                    rowData["QFQD" + baseNum] = QFQDval.ToString();

                                    //更新指定的数据
                                    if (GetSafeInt(extraItem["QFQDBZZ"].ToString()) <= QFQDval)
                                    {
                                        rowData["HG_QF" + baseNum] = "true";
                                        rowData["HG_QF"] = (GetSafeInt(rowData["HG_QF"]) + 1).ToString();
                                    }
                                    else
                                    {
                                        rowData["HG_QF" + baseNum] = "false";
                                        nonConformityNum["LS"]++;
                                    }
                                    baseNum++;
                                }
                                #endregion
                                break;
                            case "冷弯":
                                nonConformityNum["LW"] = 0;
                                #region 冷弯判定
                                baseNum = 1;
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
                                var checkLWItems = rowData.Where(u => u.Key.StartsWith("LW")).ToArray();
                                foreach (var checkItem in checkLWItems)
                                {
                                    //钢材厚度或直径大于100mm时，弯曲试验由双方协定 
                                    if (md > 100)
                                        break;
                                    //冷弯复验取样2个
                                    if (baseNum > 2)
                                        break;

                                    var LWval = GetSafeInt(checkItem.Value ?? "0");
                                    //冷弯1为合格，0为不合格，大于1为弯曲次数
                                    if (LWval == 1)
                                    {
                                        rowData["HG_LW" + baseNum] = "true";
                                        rowData["HG_LW"] = (GetSafeInt(rowData["HG_LW"]) + 1).ToString();
                                    }
                                    else if (LWval == 0)
                                    {
                                        rowData["HG_LW" + baseNum] = "false";
                                        nonConformityNum["LW"]++;
                                    }
                                    //else
                                    //{
                                    //    if (LWval - GetSafeInt(extraItem["LWBZZ"]) > 0)
                                    //    {
                                    //        rowDate["HG_LW" + baseNum] = "true";
                                    //        rowDate["HG_LW"] = (GetSafeInt(rowDate["HG_LW"]) + 1).ToString();
                                    //    }
                                    //    else
                                    //    {
                                    //        if (baseNum > 1)
                                    //        {
                                    //            //判断是否把冷弯值全部输在第一个值上
                                    //            if (GetSafeInt(rowDate["LW1"]) - baseNum * GetSafeInt(extraItem["LWBZZ"]) < -0.00001)
                                    //            {
                                    //                rowDate["HG_LW" + baseNum] = "false";
                                    //                nonConformityNum["LW"]++;
                                    //            }
                                    //            else
                                    //            {
                                    //                rowDate["HG_LW" + baseNum] = "true";
                                    //                rowDate["HG_LW"] = (GetSafeInt(rowDate["HG_LW"]) + 1).ToString();
                                    //            }
                                    //        }
                                    //        else
                                    //        {
                                    //            rowDate["HG_LW" + baseNum] = "false";
                                    //            nonConformityNum["LW"]++;
                                    //        }
                                    //    }
                                    //}
                                    baseNum++;
                                }
                                rowData["JCJG_LW"] = nonConformityNum["LW"] > 0 ? "不符合" : "符合";
                                rowData["G_LWWZ"] = LwBzyq;
                                #endregion
                                break;
                            case "冲击实验":
                                nonConformityNum["CJ"] = 0;
                                nonConformityNum["CJminNum"] = 0;
                                #region 冲击实验（V型缺口）
                                baseNum = 1;
                                /*
                                 * Q195,其他A级钢材可以不考虑冲击功实验
                                 * 厚度小于25mm的Q235B级冲击钢，如供方能保证冲击吸收功合格，经需方同意，可不做检验
                                 */
                                if (rowData["GCLX_PH"].Contains("Q195") || rowData["GCLX_PH"].Contains("A")
                                   //|| (rowDate["GCLX_PH"] == "235B" && rowDate["ZJ"] == "235B")
                                   )
                                    break;

                                var checkCJItems = rowData.Where(u => u.Key.StartsWith("CJ")).ToArray();
                                var avgCJ = checkCJItems.Sum(u => GetSafeDouble(u.Value)) / 6;

                                var mCJBZZ = GetSafeDouble(extraItem["CJBZZ"].ToString());

                                foreach (var checkItem in checkCJItems)
                                {
                                    //冲击复试取样2组6个

                                    /*
                                     * 厚度不小于12mm或直径不小于16mm的钢材应做冲击功实验
                                     * 实验取样1组3个，按试样单值的算术平均值计算，允许其中一个试样的单值低于规定值，但不得低于70%(当采用10mm*5mm*55mm试样时，应不小于规定值的50%)
                                     * 如未满足上述条件，可从同一抽样产品上在抽取1组3个进行实验，先后6个试样的平均值不得低于规定值，允许有2个低于规定值，但低于规定值70%的试样只允许有一个
                                     */
                                    if (mCJBZZ <= avgCJ)
                                    {
                                        if (mCJBZZ < GetSafeDouble(checkItem.Value))
                                        {
                                            rowData["HG_CJ" + baseNum] = "true";
                                            rowData["HG_CJ"] = (GetSafeInt(rowData["HG_CJ"]) + 1).ToString();
                                        }
                                        else
                                        {
                                            rowData["HG_CJ" + baseNum] = "false";
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
                                        rowData["HG_CJ" + baseNum] = "false";
                                        nonConformityNum["CJ"]++;
                                    }
                                    baseNum++;
                                }
                                //更新从表数据 冲击不合格判断
                                rowData["JCJG_CJ"] = "符合";
                                if (nonConformityNum["CJminNum"] > 1)
                                    rowData["JCJG_CJ"] = "不符合";
                                if (nonConformityNum["CJ"] > 2)
                                {
                                    rowData["JCJG_CJ"] = "不符合";
                                }
                                #endregion
                                break;
                            case "硬度":
                                nonConformityNum["YD"] = 0;
                                //硬度标准没有，暂不考虑
                                #region 硬度
                                baseNum = 1;
                                var checkYDItems = new Dictionary<string, double>();
                                checkYDItems.Add("YD1", rowData.Where(u => u.Key.StartsWith("YD1_")).Take(6).Sum(u => GetSafeDouble(u.Value)) / 3);
                                checkYDItems.Add("YD2", rowData.Where(u => u.Key.StartsWith("YD2_")).Take(6).Sum(u => GetSafeDouble(u.Value)) / 3);
                                checkYDItems.Add("YD3", rowData.Where(u => u.Key.StartsWith("YD3_")).Take(6).Sum(u => GetSafeDouble(u.Value)) / 3);
                                var ydbzz = rowData["CLLX"] == "退火" ? extraItem["TYDBZZ"] : extraItem["WYDBZZ"];
                                rowData["G_YD"] = ydbzz;
                                foreach (var checkItem in checkYDItems)
                                {
                                    //硬度取样3组每组3个，复试取样3组每组6个
                                    if (baseNum > 3)
                                        break;

                                    var YDval = checkItem.Value;

                                    if (GetSafeDouble(ydbzz) > YDval)
                                    {
                                        rowData["HG_YD" + baseNum] = "false";
                                        nonConformityNum["YD"]++;
                                    }
                                    else
                                        rowData["HG_YD" + baseNum] = "true";
                                    baseNum++;
                                }
                                rowData["JCJG_YD"] = nonConformityNum["YD"] > 1 ? "不符合" : "符合";
                                break;
                            #endregion
                            default:
                                break;
                        }
                    }
                    #region 更新从表检测结果
                    rowData["G_QFQD"] = extraItem["QFQDBZZ"];
                    rowData["G_KLQD"] = extraItem["KLQDBZZ"];
                    rowData["G_KLQD1"] = extraItem["KLQDBZZ1"];
                    rowData["G_SCL"] = extraItem["SCLBZZ"];
                    rowData["G_CJ"] = extraItem["CJBZZ"];
                    //复检结果
                    if (rowData["JCJG_CJ"] == "不符合" || rowData["JCJG_YD"] == "不符合" || rowData["JCJG_LS"] == "不符合" || rowData["JCJG_LW"] == "不符合")
                    {
                        rowData["JCJG_CJ"] = "不合格";
                        s_tableItem["JCJG_CJ"] = "不合格";
                        s_tableItem["FJJJ2"] = $"该组试样不符合{s_tableItem["PDBZ"]}标准要求。";
                    }
                    else
                    {
                        rowData["JCJG_CJ"] = "合格";
                        s_tableItem["JCJG_CJ"] = "合格";
                        s_tableItem["FJJJ3"] = $"该组试样所检项目符合{s_tableItem["PDBZ"]}标准要求。";
                    }
                    #endregion
                }
            }
            return true;
            /************************ 代码结束 *********************/
        }
    }
}
