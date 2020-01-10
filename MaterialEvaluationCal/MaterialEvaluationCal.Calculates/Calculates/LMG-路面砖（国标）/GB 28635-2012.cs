using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class LMG : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            /* 
             * 计算项目：路面砖(国标)
             * 参考标准：
             * GB 28635-2012
             */


            var inspectionItems = retData.Select(u => u.Key).ToArray();
            var nonConformityNum = new Dictionary<string, int>();//单组不合格个数
            nonConformityNum["BHG"] = 0;
            string pdbz = "";
            foreach (var JCXMitem in inspectionItems)
            {
                var tableItems = retData[JCXMitem]["S_LMG"];
                var s_tableItem = retData[JCXMitem]["S_BY_RW_XQ"];
                var ExtraTable = dataExtra["BZ_LMG_DJ"];
                int index = 0;
                foreach (var rowData in tableItems)//单行数据进行验证 key 字段名 val值
                {
                    if (index >= retData[JCXMitem]["S_BY_RW_XQ"].Count())
                        throw new Exception("未找到对应的任务详情数据");
                    var rwxqRow = retData[JCXMitem]["S_BY_RW_XQ"][index];

                    if (!dataExtra.ContainsKey("BZ_LMG_DJ"))
                    {
                        throw new Exception("【BZ_LMG_DJ】 表数据不存在");
                    }
                    var extraItem = ExtraTable.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(rowData["ZLDJ"])));
                    if (extraItem == null)
                    {
                        //依据不详
                        rwxqRow["JCJG"] = "依据不详";
                        rwxqRow["JCJGMS"] = $"单组流水号[{rowData["RECID"]}]规格不祥";
                        continue;
                    }
                    pdbz = ExtraTable.FirstOrDefault()["PDBZ"];
                    double[] arrays = { 0, 0, 0, 0, 0 };
                    double val = 0, mj = 0,zzjl= GetSafeDouble(rowData["ZZJL"]);
                    
                    
                    //抗压、抗折、吸水率、抗冻性、磨坑长度、耐磨度、防滑性
                    switch (JCXMitem)
                    {
                        case "抗压":
                            #region 抗压强度 精确到0.1MPa
                            rowData["KYQDYQ"] = $"抗压强度平均值需≥{GetSafeDouble(extraItem["KYPJZ"]).ToString("0.0")}MPa，单块最小值需≥{GetSafeDouble(extraItem["KYMIN"]).ToString("0.0")}MPa。";
                            nonConformityNum["KY"]= 0;
                            mj = GetSafeDouble(rowData["DBCD"]) * GetSafeDouble(rowData["DBKD"]);
                            for (int i = 1; i < 6; i++)
                            {
                                //抗压强度=抗压荷重*1000 /(试件高度（MM）* 试件宽度（MM）)
                                //精确到0.1MPa
                                if (mj == 0)
                                {
                                    rowData["KYQD" + i] = "0.0";
                                    continue;
                                }
                                val = Math.Round(GetSafeDouble(rowData["KYHZ" + i]) * 1000 / mj, 1);
                                rowData["KYQD" + i] = val.ToString("0.0");
                                arrays[i - 1] = val;
                            }
                            /*
                             * 抗压强度平均值大于标准值且最小抗压强度大于标准值即为合格，否则不合格
                             */
                            Array.Sort(arrays);

                            rowData["KYPJ"] = Math.Round(arrays.Average(),1).ToString("0.0");
                            rowData["KYQDMIN"] = Math.Round(arrays[4], 1).ToString("0.0");


                            #region 更新
                            if (Math.Round(arrays.Average(), 1) > GetSafeDouble(extraItem["KYPJZ"]) && Math.Round(arrays[4], 1) > GetSafeDouble(extraItem["KYMIN"]))
                            {
                                rowData["KYPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样抗压强度平均值{rowData["KYPJ"]}MPa，最小抗压强度{rowData["DDSJQD"]} MPa，所检项目符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {
                                rowData["KYPD"] = "不符合";
                                nonConformityNum["KY"]++;
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样抗压强度平均值{rowData["KYPJ"]}MPa，最小抗压强度{rowData["DDSJQD"]} MPa，所检项目不符合{extraItem["PDBZ"]}标准要求。";
                            }
                            #endregion
                            #endregion
                            break;
                        case "抗折":
                            #region 抗折强度 精确到0.01MPa
                            rowData["KZQDYQ"] = $"抗折强度平均值需≥{GetSafeDouble(extraItem["KZPJZ"]).ToString("0.0")}MPa，单块最小值需≥{GetSafeDouble(extraItem["KZMIN"]).ToString("0.0")}MPa。";

                            nonConformityNum["KZ"] = 0;
                            for (int i = 1; i < 6; i++)
                            {
                                //抗折强度=3*抗折荷重*间距*1000 /(2*试件厚度（MM）* 试件厚度（MM）* 试件宽度（MM）)
                                //精确到0.01MPa
                                mj = 2 * GetSafeDouble(rowData["KD" + i]) * GetSafeDouble(rowData["HD" + i]) * GetSafeDouble(rowData["HD" + i]);
                                if (mj == 0)
                                {
                                    rowData["KZQD" + i] = "0.00";
                                    continue;
                                }
                                val = Math.Round(GetSafeDouble(rowData["KZHZ" + i]) * 3000 * zzjl / mj, 2);
                                rowData["KZQD" + i] = val.ToString("0.00");
                                arrays[i - 1] = val;
                            }
                            /*
                             * 抗折强度平均值大于标准值且最小抗折强度大于标准值即为合格，否则不合格
                             */
                            Array.Sort(arrays);

                            rowData["KZPJ"] = Math.Round(arrays.Average(), 2).ToString("0.00");
                            rowData["KZQDMIN"] = Math.Round(arrays[4], 2).ToString("0.00");


                            #region 更新
                            if (Math.Round(arrays.Average(), 2) > GetSafeDouble(extraItem["KZPJZ"]) && Math.Round(arrays[4], 2) > GetSafeDouble(extraItem["KZMIN"]))
                            {
                                rowData["KZPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样抗折强度平均值{rowData["KZPJ"]}MPa，最小抗折强度{rowData["DDSJQD"]} MPa，所检项目符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {
                                rowData["KZPD"] = "不符合";
                                nonConformityNum["KZ"]++;
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样抗折强度平均值{rowData["KZPJ"]}MPa，最小抗折强度{rowData["DDSJQD"]} MPa，所检项目不符合{extraItem["PDBZ"]}标准要求。";
                            }
                            #endregion
                            #endregion
                            break;
                        case "吸水率":
                            #region 吸水率 精确到0.1%
                            rowData["XSLYQ"] = extraItem["XSL"];
                            nonConformityNum["XSL"] = 0;
                            for (int i = 1; i < 6; i++)
                            {
                                //吸水率=(吸水质量 - 干燥质量) * 100 /干燥质量          ==>     w=(M1-M0)*100/M0
                                //精确到0.1%
                                val = Math.Round((GetSafeDouble(rowData["XSLM1_" + i])- GetSafeDouble(rowData["XSLM0_" + i])) * 100 / GetSafeDouble(rowData["XSLM0_" + i]), 1);
                                rowData["XSL" + i] = val.ToString("0.0");
                                arrays[i - 1] = val;
                            }
                            /*
                             * 吸水率判定：取5个试样的算术平均值<=标准值
                             */
                            Array.Sort(arrays);
                            
                            #region 更新
                            if (Math.Round(arrays.Average(), 1) <= GetSafeDouble(extraItem["XSL"]))
                            {
                                rowData["XSLPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样吸水率平均值{rowData["KZPJ"]}MPa，所检项目符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {
                                rowData["XSLPD"] = "不符合";
                                nonConformityNum["XSL"] ++;
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样吸水率平均值{rowData["KZPJ"]}MPa，所检项目不符合{extraItem["PDBZ"]}标准要求。";
                            }
                            #endregion
                            #endregion
                            break;
                        case "抗冻性":
                            #region 抗冻性 精确到0.1%
                            rowData["QDSSLYQ"] = extraItem["QDSSL"];
                            nonConformityNum["KD"] = 0;
                            //计算冻后强度
                            for (int i = 1; i < 6; i++)
                            {
                                if(GetSafeInt(rowData["CHB"]) <5)
                                {
                                    //冻后抗压强度
                                    mj = GetSafeDouble(rowData["DBCD"]) * GetSafeDouble(rowData["DBKD"]);
                                    if (mj == 0)
                                    {
                                        rowData["DHKYQD" + i] = "0.0";
                                        continue;
                                    }
                                    val = Math.Round(GetSafeDouble(rowData["KYHZ" + i]) * 1000 / mj, 1);
                                    rowData["DHKYQD" + i] = val.ToString("0.0");
                                    arrays[i - 1] = val;
                                }
                                else
                                {
                                    //冻后抗折强度=3*抗折荷重*间距*1000 /(2*冻后厚度（MM）* 冻后厚度（MM）* 冻后宽度（MM）)
                                    //精确到0.1MPa
                                    mj = 2 * GetSafeDouble(rowData["DHKD" + i]) * GetSafeDouble(rowData["DHHD" + i]) * GetSafeDouble(rowData["DHHD" + i]);
                                    if (mj == 0)
                                    {
                                        rowData["DHKZQD" + i] = "0.00";
                                        continue;
                                    }
                                    val = Math.Round(GetSafeDouble(rowData["DHKZHZ" + i]) * 3000 * zzjl / mj, 2);
                                    rowData["DHKZQD" + i] = val.ToString("0.00");
                                    arrays[i - 1] = val;
                                }
                            }
                            /*
                             * 冻融损失率% = (冻前强度-冻后强度)/冻前强度*100
                             * 精确到0.1%
                             */
                            Array.Sort(arrays);

                            if (GetSafeInt(rowData["CHB"]) < 5)
                            {
                                rowData["DHKYPJ"] = Math.Round(arrays.Average(), 1).ToString("0.0");
                                rowData["QDSSL"] = Math.Round((GetSafeDouble(rowData["KYPJ"])- GetSafeDouble(rowData["DHKYPJ"]))/GetSafeDouble(rowData["KYPJ"]) * 100, 1).ToString("0.0");
                            }
                            else
                            {
                                rowData["DHKZPJ"] = Math.Round(arrays.Average(), 1).ToString("0.0");
                                rowData["QDSSL"] = Math.Round((GetSafeDouble(rowData["KZPJ"]) - GetSafeDouble(rowData["DHKZPJ"])) / GetSafeDouble(rowData["KZPJ"]) * 100, 1).ToString("0.0");
                            }



                            #region 更新
                            if (GetSafeDouble(rowData["QDSSL"]) <= GetSafeDouble(extraItem["QDSSL"]))
                            {
                                rowData["QDSSLPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样冻融损失率{rowData["QDSSL"]}%符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {
                                rowData["QDSSLPD"] = "不符合";
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样冻融损失率{rowData["QDSSL"]}%不符合{extraItem["PDBZ"]}标准要求。";
                                nonConformityNum["KD"]++;
                            }
                            #endregion
                            #endregion
                            break;
                        case "磨坑长度":
                            #region 磨坑长度
                            rowData["MKCDYQ"] = extraItem["MKCD"];
                            nonConformityNum["MKCD"] = 0;
                            if (GetSafeDouble(rowData["MKCD"]) <= GetSafeDouble(extraItem["MKCD"]))
                            {
                                rowData["MKCDPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样磨坑长度{rowData["NMD"]}mm符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {
                                rowData["MKCDPD"] = "不符合";
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样磨坑长度{rowData["NMD"]}mm不符合{extraItem["PDBZ"]}标准要求。";
                                nonConformityNum["MKCD"]++;
                            }
                            #endregion
                            break;
                        case "耐磨度":
                            #region 耐磨度
                            rowData["NMDYQ"] = extraItem["NMD"];
                            nonConformityNum["NMD"] = 0;
                            if (GetSafeDouble(rowData["NMD"]) > GetSafeDouble(extraItem["NMD"]))
                            {
                                rowData["MKCDPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样耐磨度{rowData["NMD"]}符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {
                                rowData["MKCDPD"] = "不符合";
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样耐磨度{rowData["NMD"]}不符合{extraItem["PDBZ"]}标准要求。";
                                nonConformityNum["NMD"]++;
                            }
                            #endregion
                            break;
                        case "防滑性":
                            #region 防滑性 精确至1 BPN
                            rowData["FHXYQ"] = extraItem["FHX"];
                            nonConformityNum["FHX"] = 0;
                            if (GetSafeDouble(rowData["FHX"]) >= GetSafeDouble(extraItem["FHX"]))
                            {
                                rowData["MKCDPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样耐磨度{rowData["NMD"]}符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {
                                rowData["MKCDPD"] = "不符合";
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样耐磨度{rowData["NMD"]}不符合{extraItem["PDBZ"]}标准要求。";
                                nonConformityNum["FHX"]++;
                            }
                            #endregion
                            break;
                        case "抗冻盐性":
                            #region 抗冻盐性 精确至1 g/m²
                            rowData["KDYXYQ"] = extraItem["KDYX"];
                            nonConformityNum["KDYX"] = 0;
                            for (int i = 1; i < 6; i++)
                            {
                                //单位面积质量损失（g/m²） = 质量(mg)*1000/面积(mm²)
                                //质量精确到1mg，长度精确到1mm
                                val = Convert.ToInt32( GetSafeDouble(rowData["DYZL" + i]) * 1000 / (GetSafeDouble(rowData["DYCD" + i]) * GetSafeDouble(rowData["DYKD" + i])));
                                rowData["KDYX" + i] = val.ToString();
                                arrays[i - 1] = val;
                            }
                            /*
                             * 吸水率判定：取5个试样的算术平均值<=标准值
                             */
                            Array.Sort(arrays);
                            rowData["KDYXPJ"] = arrays.Average().ToString();
                            rowData["KDYXMAX"] = arrays[0].ToString();
                            #region 更新
                            if (arrays.Average() <= GetSafeInt(extraItem["KDYXPJZ"]) && arrays[0] < GetSafeInt(extraItem["KDYXMAX"]))
                            {
                                rowData["KDYXPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样抗冻盐性平均值{rowData["KZPJ"]}g/m²，最大值{rowData["KZPJ"]}g/m²，所检项目符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {
                                rowData["KDYXPD"] = "不符合";
                                nonConformityNum["KDYX"] ++ ;
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样抗冻盐性平均值{rowData["KZPJ"]}g/m²，最大值{rowData["KZPJ"]}g/m²，所检项目不符合{extraItem["PDBZ"]}标准要求。";
                            }
                            #endregion
                            #endregion
                            break;
                        default:
                            break;
                    }
                    index++;
                }
            }

            #region 更新主表检测结果
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            bgjgDic.Add("JCJG", (nonConformityNum.Sum(u => u.Value) > 0 ? "不合格" : "合格"));
            bgjgDic.Add("JCJGMS", $"该组试样所检项目{(nonConformityNum.Sum(u=>u.Value) > 0 ? "不" : "")}符合{pdbz}标准要求。");
            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG",bgjg);
            #endregion
            return true;
            /************************ 代码结束 *********************/
        }
    }
}
