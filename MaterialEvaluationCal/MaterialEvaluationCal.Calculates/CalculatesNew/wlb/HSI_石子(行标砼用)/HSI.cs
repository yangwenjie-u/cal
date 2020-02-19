using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class HSI : BaseMethods
    {
        public void Calc()
        {
            /*********************** 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_HSI_DJ"];
            var extraHSB = dataExtra["BZ_HSIHSB"];
            var extraZBYQ = dataExtra["BZ_HSIZBYQ"];


            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_HSIS = data["S_HSI"];
            if (!data.ContainsKey("M_HSI"))
            {
                data["M_HSI"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_HSI"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            int mbhgs = 0;//检测项目合格数量
            bool sign, flag = true;
            double md1, md2, md, pjmd, sum = 0;
            double cd1, cd2, kd1, kd2 = 0;
            int xd = 0, vp = 0;
            string mlongStr;
            double[] mkyqdArray = new double[6];
            double[] mkyhzArray = new double[3];
            string[] mtmpArray;


            foreach (var sItem in S_HSIS)
            {
                string a = "0.00";
                bool b = IsNumeric("0.00");
                double[] narr = new double[14];
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                #region 级配
                if (jcxm.Contains("、筛分析、"))
                {
                    #region 赋值
                    for (int i = 1; i < 14; i++)
                    {
                        narr[i] = 0;
                    }
                    if (IsNumeric(sItem["SY2_36"]))
                    {
                        narr[1] = double.Parse(sItem["SY2_36"].Trim());
                    }
                    if (IsNumeric(sItem["SY4_75"]))
                    {
                        narr[2] = double.Parse(sItem["SY4_75"].Trim());
                    }
                    if (IsNumeric(sItem["SY9_50"]))
                    {
                        narr[3] = double.Parse(sItem["SY9_50"].Trim());
                    }
                    if (IsNumeric(sItem["SY16_0"]))
                    {
                        narr[4] = double.Parse(sItem["SY16_0"].Trim());
                    }
                    if (IsNumeric(sItem["SY19_0"]))
                    {
                        narr[5] = double.Parse(sItem["SY19_0"].Trim());
                    }
                    if (IsNumeric(sItem["SY26_5"]))
                    {
                        narr[6] = double.Parse(sItem["SY26_5"].Trim());
                    }
                    if (IsNumeric(sItem["SY31_5"]))
                    {
                        narr[7] = double.Parse(sItem["SY31_5"].Trim());
                    }
                    if (IsNumeric(sItem["SY37_5"]))
                    {
                        narr[8] = double.Parse(sItem["SY37_5"].Trim());
                    }
                    if (IsNumeric(sItem["SY53_0"]))
                    {
                        narr[9] = double.Parse(sItem["SY53_0"].Trim());
                    }
                    if (IsNumeric(sItem["SY63_0"]))
                    {
                        narr[10] = double.Parse(sItem["SY63_0"].Trim());
                    }
                    if (IsNumeric(sItem["SY75_0"]))
                    {
                        narr[11] = double.Parse(sItem["SY75_0"].Trim());
                    }
                    if (IsNumeric(sItem["SY90"]))
                    {
                        narr[12] = double.Parse(sItem["SY90"].Trim());
                    }
                    if (IsNumeric(sItem["SY100"]))
                    {
                        narr[13] = double.Parse(sItem["SY100"].Trim());
                    }

                    sum = 0;
                    if (IsNumeric(MItem[0]["SIZL"]))
                    {
                        sum = double.Parse(MItem[0]["SIZL"]);
                    }

                    if (sum == 0)
                    {
                        for (int i = 1; i < 14; i++)
                        {
                            sum = sum + narr[i];
                        }
                    }

                    for (int i = 1; i < 14; i++)
                    {
                        md = Math.Round(100 * narr[i] / sum, 1);
                        narr[i] = md;
                    }
                    #endregion

                    #region 赋值
                    sum = 0;
                    md = Math.Round(narr[12], 0);
                    sum = sum + narr[12];
                    sItem["CY90"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[11], 0);
                    sum = sum + narr[11];
                    sItem["CY75_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[10], 0);
                    sum = sum + narr[10];
                    sItem["CY63_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[9], 0);
                    sum = sum + narr[9];
                    sItem["CY53_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[8], 0);
                    sum = sum + narr[8];
                    sItem["CY37_5"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[7], 0);
                    sum = sum + narr[7];
                    sItem["CY31_5"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[6], 0);
                    sum = sum + narr[6];
                    sItem["CY26_5"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[5], 0);
                    sum = sum + narr[5];
                    sItem["CY19_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[4], 0);
                    sum = sum + narr[4];
                    sItem["CY16_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[3], 0);
                    sum = sum + narr[3];
                    sItem["CY9_50"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[2], 0);
                    sum = sum + narr[2];
                    sItem["CY4_75"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[1], 0);
                    sum = sum + narr[1];
                    sItem["CY2_36"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[13], 0);
                    sum = sum + narr[13];
                    sItem["CY100"] = md > 100 ? "100" : sum.ToString("0");
                    #endregion

                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsHS = extraHSB.FirstOrDefault(u => u["GCCC"].Trim() == sItem["GCCC"]);

                    flag = true;
                    flag = double.Parse(sItem["CY2_36"]) >= double.Parse(extraFieldsHS["CY2_36A"]) && double.Parse(sItem["CY2_36"]) <= double.Parse(extraFieldsHS["CY2_36B"]) ? flag : false;
                    flag = double.Parse(sItem["CY4_75"]) >= double.Parse(extraFieldsHS["CY4_75A"]) && double.Parse(sItem["CY4_75"]) <= double.Parse(extraFieldsHS["CY4_75B"]) ? flag : false;
                    flag = double.Parse(sItem["CY9_50"]) >= double.Parse(extraFieldsHS["CY9_50A"]) && double.Parse(sItem["CY9_50"]) <= double.Parse(extraFieldsHS["CY9_50B"]) ? flag : false;
                    flag = double.Parse(sItem["CY16_0"]) >= double.Parse(extraFieldsHS["CY16_0A"]) && double.Parse(sItem["CY16_0"]) <= double.Parse(extraFieldsHS["CY16_0B"]) ? flag : false;
                    flag = double.Parse(sItem["CY19_0"]) >= double.Parse(extraFieldsHS["CY19_0A"]) && double.Parse(sItem["CY19_0"]) <= double.Parse(extraFieldsHS["CY19_0B"]) ? flag : false;
                    flag = double.Parse(sItem["CY26_5"]) >= double.Parse(extraFieldsHS["CY26_5A"]) && double.Parse(sItem["CY26_5"]) <= double.Parse(extraFieldsHS["CY26_5B"]) ? flag : false;
                    flag = double.Parse(sItem["CY31_5"]) >= double.Parse(extraFieldsHS["CY31_5A"]) && double.Parse(sItem["CY31_5"]) <= double.Parse(extraFieldsHS["CY31_5B"]) ? flag : false;
                    flag = double.Parse(sItem["CY37_5"]) >= double.Parse(extraFieldsHS["CY37_5A"]) && double.Parse(sItem["CY37_5"]) <= double.Parse(extraFieldsHS["CY37_5B"]) ? flag : false;
                    flag = double.Parse(sItem["CY53_0"]) >= double.Parse(extraFieldsHS["CY53_0A"]) && double.Parse(sItem["CY53_0"]) <= double.Parse(extraFieldsHS["CY53_0B"]) ? flag : false;
                    flag = double.Parse(sItem["CY63_0"]) >= double.Parse(extraFieldsHS["CY63_0A"]) && double.Parse(sItem["CY63_0"]) <= double.Parse(extraFieldsHS["CY63_0B"]) ? flag : false;
                    flag = double.Parse(sItem["CY75_0"]) >= double.Parse(extraFieldsHS["CY75_0A"]) && double.Parse(sItem["CY75_0"]) <= double.Parse(extraFieldsHS["CY75_0B"]) ? flag : false;
                    flag = double.Parse(sItem["CY90"]) >= double.Parse(extraFieldsHS["CY90A"]) && double.Parse(sItem["CY4_75"]) <= double.Parse(extraFieldsHS["CY90B"]) ? flag : false;

                    if (!flag)
                    {
                        itemHG = false;
                        mAllHg = false;
                    }
                    if (flag)
                    {
                        sItem["JPPD"] = "符合";
                    }
                    else
                    {
                        itemHG = false;
                        mAllHg = false;
                        sItem["JPPD"] = "不符合";
                    }
                    sItem["SJGCCC"] = flag ? extraFieldsHS["GCCC"] : "----";
                    mbhgs = flag ? mbhgs : mbhgs + 1;
                    sItem["JPPD"] = 100 - double.Parse(sItem["CY100"].Trim()) > 1 ? "试验需重做" : sItem["JPPD"];

                    #region
                    switch (sItem["GCCC"].Trim())
                    {
                        case "5~10":
                            sItem["CY19_0"] = "-";
                            sItem["CY26_5"] = "-";
                            sItem["CY31_5"] = "-";
                            sItem["CY37_5"] = "-";
                            sItem["CY53_0"] = "-";
                            sItem["CY63_0"] = "-";
                            sItem["CY75_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY90"] = "-";
                            sItem["SY75_0"] = "-";
                            sItem["SY63_0"] = "-";
                            sItem["SY53_0"] = "-";
                            sItem["SY37_5"] = "-";
                            sItem["SY31_5"] = "-";
                            sItem["SY26_5"] = "-";
                            sItem["SY19_0"] = "-";

                            break;
                        case "5~16":
                            sItem["CY26_5"] = "-";
                            sItem["CY31_5"] = "-";
                            sItem["CY37_5"] = "-";
                            sItem["CY53_0"] = "-";
                            sItem["CY63_0"] = "-";
                            sItem["CY75_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY90"] = "-";
                            sItem["SY75_0"] = "-";
                            sItem["SY63_0"] = "-";
                            sItem["SY53_0"] = "-";
                            sItem["SY37_5"] = "-";
                            sItem["SY31_5"] = "-";
                            sItem["SY26_5"] = "-";

                            break;
                        case "5~20":
                            sItem["CY16_0"] = "-";
                            sItem["CY31_5"] = "-";
                            sItem["CY37_5"] = "-";
                            sItem["CY53_0"] = "-";
                            sItem["CY63_0"] = "-";
                            sItem["CY75_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY16_0"] = "-";
                            sItem["SY31_5"] = "-";
                            sItem["SY37_5"] = "-";
                            sItem["SY53_0"] = "-";
                            sItem["SY63_0"] = "-";
                            sItem["SY75_0"] = "-";
                            sItem["SY90"] = "-";
                            break;
                        case "5~25":
                            sItem["CY9_50"] = "-";
                            sItem["CY19_0"] = "-";
                            sItem["CY37_5"] = "-";
                            sItem["CY53_0"] = "-";
                            sItem["CY63_0"] = "-";
                            sItem["CY75_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY9_50"] = "-";
                            sItem["SY19_0"] = "-";
                            sItem["SY37_5"] = "-";
                            sItem["SY53_0"] = "-";
                            sItem["SY63_0"] = "-";
                            sItem["SY75_0"] = "-";
                            sItem["SY90"] = "-";
                            break;
                        case "5~31.5":
                            sItem["CY16_0"] = "-";
                            sItem["CY26_5"] = "-";
                            sItem["CY53_0"] = "-";
                            sItem["CY63_0"] = "-";
                            sItem["CY75_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY16_0"] = "-";
                            sItem["SY26_5"] = "-";
                            sItem["SY53_0"] = "-";
                            sItem["SY63_0"] = "-";
                            sItem["SY75_0"] = "-";
                            sItem["SY90"] = "-";
                            break;
                        case "5~40":
                            sItem["CY2_36"] = "-";
                            sItem["CY16_0"] = "-";
                            sItem["CY26_5"] = "-";
                            sItem["CY31_5"] = "-";
                            sItem["CY63_0"] = "-";
                            sItem["CY75_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY2_36"] = "-";
                            sItem["SY16_0"] = "-";
                            sItem["SY26_5"] = "-";
                            sItem["SY31_5"] = "-";
                            sItem["SY63_0"] = "-";
                            sItem["SY75_0"] = "-";
                            sItem["SY90"] = "-";
                            break;
                        case "10~20":
                            sItem["CY2_36"] = "-";
                            sItem["CY16_0"] = "-";
                            sItem["CY31_5"] = "-";
                            sItem["CY37_5"] = "-";
                            sItem["CY53_0"] = "-";
                            sItem["CY63_0"] = "-";
                            sItem["CY75_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY2_36"] = "-";
                            sItem["SY16_0"] = "-";
                            sItem["SY31_5"] = "-";
                            sItem["SY37_5"] = "-";
                            sItem["SY53_0"] = "-";
                            sItem["SY63_0"] = "-";
                            sItem["SY75_0"] = "-";
                            sItem["SY90"] = "-";
                            break;

                        case "16~31.5":
                            sItem["CY2_36"] = "-";
                            sItem["CY9_50"] = "-";
                            sItem["CY19_0"] = "-";
                            sItem["CY26_5"] = "-";
                            sItem["CY53_0"] = "-";
                            sItem["CY63_0"] = "-";
                            sItem["CY75_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY2_36"] = "-";
                            sItem["SY9_50"] = "-";
                            sItem["SY19_0"] = "-";
                            sItem["SY26_5"] = "-";
                            sItem["SY53_0"] = "-";
                            sItem["SY63_0"] = "-";
                            sItem["SY75_0"] = "-";
                            sItem["SY90"] = "-";
                            break;

                        case "20~40":
                            sItem["CY2_36"] = "-";
                            sItem["CY4_75"] = "-";
                            sItem["CY16_0"] = "-";
                            sItem["CY26_5"] = "-";
                            sItem["CY31_5"] = "-";
                            sItem["CY63_0"] = "-";
                            sItem["CY75_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY2_36"] = "-";
                            sItem["SY4_75"] = "-";
                            sItem["SY16_0"] = "-";
                            sItem["SY26_5"] = "-";
                            sItem["SY31_5"] = "-";
                            sItem["SY63_0"] = "-";
                            sItem["SY75_0"] = "-";
                            sItem["SY90"] = "-";
                            break;

                        case "31.5~63":
                            sItem["CY2_36"] = "-";
                            sItem["CY4_75"] = "-";
                            sItem["CY9_50"] = "-";
                            sItem["CY19_0"] = "-";
                            sItem["CY26_5"] = "-";
                            sItem["CY53_0"] = "-";
                            sItem["CY90"] = "-";
                            sItem["SY2_36"] = "-";
                            sItem["SY4_75"] = "-";
                            sItem["SY9_50"] = "-";
                            sItem["SY19_0"] = "-";
                            sItem["SY26_5"] = "-";
                            sItem["SY53_0"] = "-";
                            sItem["SY90"] = "-";
                            break;

                        case "40~80":
                            sItem["CY2_36"] = "-";
                            sItem["CY4_75"] = "-";
                            sItem["CY9_50"] = "-";
                            sItem["CY16_0"] = "-";
                            sItem["CY26_5"] = "-";
                            sItem["CY31_5"] = "-";
                            sItem["CY53_0"] = "-";
                            sItem["SY2_36"] = "-";
                            sItem["SY4_75"] = "-";
                            sItem["SY9_50"] = "-";
                            sItem["SY16_0"] = "-";
                            sItem["SY26_5"] = "-";
                            sItem["SY31_5"] = "-";
                            sItem["SY53_0"] = "-";
                            break;
                    }
                    #endregion
                }
                else
                {
                    sItem["JPPD"] = "----";
                    sItem["CY2_36"] = "-";
                    sItem["CY4_75"] = "-";
                    sItem["CY9_50"] = "-";
                    sItem["CY16_0"] = "-";
                    sItem["CY19_0"] = "-";
                    sItem["CY26_5"] = "-";
                    sItem["CY31_5"] = "-";
                    sItem["CY37_5"] = "-";
                    sItem["CY53_0"] = "-";
                    sItem["CY63_0"] = "-";
                    sItem["CY75_0"] = "-";
                    sItem["CY90"] = "-";
                    sItem["CY100"] = "-";
                }
                #endregion

                #region 坚固性
                if (jcxm.Contains("、坚固性、"))
                {
                    double mfjzlss1, mfjzlss2, mfjzlss3, mfjzlss4, mfjzlss5 = 0;
                    if (0 != Conversion.Val(sItem["JGXQM1"]))
                    {
                        mfjzlss1 = Math.Round((Conversion.Val(sItem["JGXQM1"]) - Conversion.Val(sItem["JGXHM1"])) / Conversion.Val(sItem["JGXQM1"]) * 100, 1);
                    }
                    else
                    {
                        mfjzlss1 = 0;
                    }
                    if (0 != Conversion.Val(sItem["JGXQM2"]))
                    {
                        mfjzlss2 = Math.Round((Conversion.Val(sItem["JGXQM2"]) - Conversion.Val(sItem["JGXHM2"])) / Conversion.Val(sItem["JGXQM2"]) * 100, 1);
                    }
                    else
                    {
                        mfjzlss2 = 0;
                    }
                    if (0 != Conversion.Val(sItem["JGXQM3"]))
                    {
                        mfjzlss3 = Math.Round((Conversion.Val(sItem["JGXQM3"]) - Conversion.Val(sItem["JGXHM3"])) / Conversion.Val(sItem["JGXQM3"]) * 100, 1);
                    }
                    else
                    {
                        mfjzlss3 = 0;
                    }
                    if (0 != Conversion.Val(sItem["JGXQM2"]))
                    {
                        mfjzlss4 = Math.Round((Conversion.Val(sItem["JGXQM4"]) - Conversion.Val(sItem["JGXHM4"])) / Conversion.Val(sItem["JGXQM4"]) * 100, 1);
                    }
                    else
                    {
                        mfjzlss4 = 0;
                    }
                    if (0 != Conversion.Val(sItem["JGXQM5"]))
                    {
                        mfjzlss5 = Math.Round((Conversion.Val(sItem["JGXQM5"]) - Conversion.Val(sItem["JGXHM5"])) / Conversion.Val(sItem["JGXQM5"]) * 100, 1);
                    }
                    else
                    {
                        mfjzlss5 = 0;
                    }

                    double masum = Conversion.Val(sItem["JGXQM1"]) + Conversion.Val(sItem["JGXQM2"]) + Conversion.Val(sItem["JGXQM3"]) + Conversion.Val(sItem["JGXQM4"]) + Conversion.Val(sItem["JGXQM5"]);
                    double ma1 = Math.Round(Conversion.Val(sItem["JGXHM1"]) / masum * 100, 2);
                    double ma2 = Math.Round(Conversion.Val(sItem["JGXHM2"]) / masum * 100, 2);
                    double ma3 = Math.Round(Conversion.Val(sItem["JGXHM3"]) / masum * 100, 2);
                    double ma4 = Math.Round(Conversion.Val(sItem["JGXHM4"]) / masum * 100, 2);
                    double ma5 = Math.Round(Conversion.Val(sItem["JGXHM5"]) / masum * 100, 2);
                    sItem["JGX"] = Math.Round((ma1 * mfjzlss1 + ma2 * mfjzlss2 + ma3 * mfjzlss3 + ma4 * mfjzlss4 + ma5 * mfjzlss5) / (ma1 + ma2 + ma3 + ma4 + ma5), 0).ToString();

                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsZBYQs = extraZBYQ.Where(u => u["MC"].Trim() == "坚固性");
                    foreach (var extraFieldsZBYQ in extraFieldsZBYQs)
                    {
                        if (IsQualified(extraFieldsZBYQ["YQ"], sItem["JGX"], true) == "符合")
                        {
                            sItem["JGXPD"] = "符合" + extraFieldsZBYQ["DJ"].Trim();
                        }
                        else
                        {
                            sItem["JGXPD"] = "不符合";
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(sItem["JGXPD"]) || "不符合" == sItem["JGXPD"])
                    {
                        sItem["JGXPD"] = "不符合";
                        itemHG = false;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["JGX"] = "----";
                    sItem["JGXPD"] = "----";
                }
                #endregion
                mAllHg = mbhgs > 0 ? false : true;
                #region vb 跳转
                //if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                //{
                    #region 含泥量
                    if (jcxm.Contains("、含泥量、"))
                    {
                        sItem["HNLPD"] = "";
                        //从设计等级表中取得相应的计算数值、等级标准
                        var extraFieldsZBYQs = extraZBYQ.Where(u => u["MC"].Trim() == "含泥量");
                        foreach (var extraFieldsZBYQ in extraFieldsZBYQs)
                        {
                            if (IsQualified(extraFieldsZBYQ["YQ"], sItem["HNL"], true) == "符合")
                            {
                                sItem["HNLPD"] = extraFieldsZBYQ["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["HNLPD"]))
                        {
                            sItem["HNLPD"] = "不符合";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                    else
                    {
                        sItem["HNLPD"] = "----";
                        sItem["HNL"] = "----";
                    }
                    #endregion

                    #region 泥块含量
                    if (jcxm.Contains("、泥块含量、"))
                    {
                        sItem["NKHLPD"] = "";
                        //从设计等级表中取得相应的计算数值、等级标准
                        var extraFieldsZBYQs = extraZBYQ.Where(u => u["MC"].Trim() == "泥块含量");
                        foreach (var extraFieldsZBYQ in extraFieldsZBYQs)
                        {
                            if (IsQualified(extraFieldsZBYQ["YQ"], sItem["NKHL"], true) == "符合")
                            {
                                sItem["NKHLPD"] = extraFieldsZBYQ["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["NKHLPD"]))
                        {
                            sItem["NKHLPD"] = "不符合";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                    else
                    {
                        sItem["NKHLPD"] = "----";
                        sItem["NKHL"] = "----";
                    }
                    #endregion

                    #region 紧密密度和堆积密度
                    if (jcxm.Contains("、紧密密度和堆积密度、"))
                    {
                        sItem["JMMDPD"] = "----";
                        sItem["DJMDPD"] = "----";
                    }
                    else
                    {
                        sItem["JMMDPD"] = "----";
                        sItem["JMMD"] = "----";
                        sItem["DJMDPD"] = "----";
                        sItem["DJMD"] = "----";
                    }
                    #endregion

                    #region 表观密度
                    if (jcxm.Contains("、表观密度、"))
                    {
                        sItem["BGMDPD"] = "----";
                    }
                    else
                    {
                        sItem["BGMD"] = "----";
                        sItem["BGMDPD"] = "----";
                    }
                    #endregion

                    #region 空隙率
                    if (jcxm.Contains("、空隙率、"))
                    {
                        sItem["KXLPD"] = "----";
                    }
                    else
                    {
                        sItem["KXL"] = "----";
                        sItem["KXLPD"] = "----";
                    }
                    #endregion

                    #region 压碎指标值
                    if (jcxm.Contains("、压碎指标值、"))
                    {
                        sItem["YSZBPD"] = "";
                        //从设计等级表中取得相应的计算数值、等级标准
                        var extraFieldsZBYQs = extraZBYQ.Where(u => u["MC"].Trim() == "压碎性指标" && u["SPZ"].Trim() == sItem["SIPZ"].Trim());
                        foreach (var extraFieldsZBYQ in extraFieldsZBYQs)
                        {
                            if (IsQualified(extraFieldsZBYQ["YQ"], sItem["YSZB"], true) == "符合")
                            {
                                sItem["YSZBPD"] = extraFieldsZBYQ["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["YSZBPD"]))
                        {
                            sItem["YSZBPD"] = "不符合";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                    else
                    {
                        sItem["YSZB"] = "----";
                        sItem["YSZBPD"] = "----";
                    }
                    #endregion

                    #region 吸水率
                    if (jcxm.Contains("、吸水率、"))
                    {
                        sItem["XSLPD"] = "----";
                    }
                    else
                    {
                        sItem["XSL"] = "----";
                        sItem["XSLPD"] = "----";
                    }
                    #endregion

                    #region 含水率
                    if (jcxm.Contains("、含水率、"))
                    {
                        sItem["HSLPD"] = "----";
                    }
                    else
                    {
                        sItem["HSL"] = "----";
                        sItem["HSLPD"] = "----";
                    }
                    #endregion

                    #region 针片状含量
                    if (jcxm.Contains("、针片状含量、"))
                    {
                        sItem["ZPZHLPD"] = "";
                        //从设计等级表中取得相应的计算数值、等级标准
                        var extraFieldsZBYQs = extraZBYQ.Where(u => u["MC"].Trim() == "针片状含量");
                        foreach (var extraFieldsZBYQ in extraFieldsZBYQs)
                        {
                            if (IsQualified(extraFieldsZBYQ["YQ"], sItem["ZPZHL"], true) == "符合")
                            {
                                sItem["ZPZHLPD"] = extraFieldsZBYQ["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["ZPZHLPD"]))
                        {
                            sItem["ZPZHLPD"] = "不符合";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                    else
                    {
                        sItem["ZPZHL"] = "----";
                        sItem["ZPZHLPD"] = "----";
                    }
                    #endregion

                    #region 抗压强度
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        sItem["KYQDPD"] = "----";
                    }
                    else
                    {
                        sItem["KYQD"] = "----";
                        sItem["KYQDPD"] = "----";
                        sItem["QDMIN"] = "0";
                    }
                    #endregion

                    #region 硫化物和硫酸盐含量
                    if (jcxm.Contains("、硫化物和硫酸盐含量、"))
                    {
                        sItem["LHWPD"] = "";
                        //从设计等级表中取得相应的计算数值、等级标准
                        var extraFieldsZBYQs = extraZBYQ.Where(u => u["MC"].Trim() == "硫化物和硫酸盐含量");
                        foreach (var extraFieldsZBYQ in extraFieldsZBYQs)
                        {
                            if (IsQualified(extraFieldsZBYQ["YQ"], sItem["LHW"], true) == "符合")
                            {
                                sItem["LHWPD"] = extraFieldsZBYQ["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["LHWPD"]))
                        {
                            sItem["LHWPD"] = "不符合";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                    else
                    {
                        sItem["LHW"] = "----";
                        sItem["LHWPD"] = "----";
                    }
                    #endregion

                    #region 有机物含量
                    if (jcxm.Contains("、有机物含量、"))
                    {
                        if ("不合格" == sItem["YJWHLPD"] || "不符合" == sItem["YJWHLPD"])
                        {
                            sItem["YJWHLPD"] = "不符合";
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            sItem["YJWHLPD"] = "符合";
                        }
                    }
                    else
                    {
                        sItem["YJWHLPD"] = "----";
                        sItem["YJWHL"] = "----";
                    }
                    #endregion

                    #region 碱活性
                    if (jcxm.Contains("、碱活性、"))
                    {
                        if (Conversion.Val(sItem["JHX"]) < 0.1)
                        {
                            sItem["JHXPD"] = "无潜在危害";
                        }
                        else
                        {

                            if (Conversion.Val(sItem["JHX"]) > 0.2)
                            {
                                sItem["JHXPD"] = "有潜在危害";
                            }
                            else
                            {
                                sItem["JHXPD"] = "需按7.17节进行复试";
                            }
                            itemHG = false;
                            mAllHg = false;
                        }

                    }
                    else
                    {
                        sItem["JHXPD"] = "----";
                        sItem["JHX"] = "----";
                    }
                    #endregion

                    //单组判断
                    if (itemHG)
                    {
                        sItem["JCJG"] = "合格";
                    }
                    else
                    {
                        sItem["JCJG"] = "不合格";
                    }
                    //continue;
                //}
                #endregion
                //else
                //{
                //    sItem["HNLPD"] = "";
                //    sItem["HNL"] = "0";
                //    if (jcxm.Contains("、含泥量、"))
                //    {
                //        double mhnl1 = 0;
                //        double mhnl2 = 0;
                //        if (Conversion.Val(sItem["HNLG1"]) != 0 && Conversion.Val(sItem["HNLG1_2"]) != 0)
                //        {
                //            mhnl1 = Round((Conversion.Val(sItem["HNLG1"]) - Conversion.Val(sItem["HNLG2"])) / Conversion.Val(sItem["HNLG1"]) * 100, 1);
                //            mhnl2 = Round((Conversion.Val(sItem["HNLG1_2"]) - Conversion.Val(sItem["HNLG2_2"])) / Conversion.Val(sItem["HNLG1_2"]) * 100, 1);
                //            sItem["HNL"] = Round((mhnl1 + mhnl2) / 2, 1).ToString();
                //            var mrsZbyq_where = extraZBYQ.Where(x => x["MC"].Equals("含泥量"));
                //            foreach (var item in mrsZbyq_where)
                //            {
                //                if (IsQualified(item["YQ"], sItem["HNL"]) == "符合")
                //                {
                //                    sItem["HNLPD"] = item["DJ"];
                //                    break;
                //                }
                //            }
                //            if (sItem["HNLPD"] == "")
                //            {
                //                sItem["HNLPD"] = "不符合";
                //                mbhgs = mbhgs + 1;
                //            }
                //            if (Math.Abs(mhnl1 - mhnl2) > 0.2)
                //                sItem["HNLPD"] = "两次结果之差超过0.2%需重新取样试验";
                //        }
                //    }
                //    else
                //    {
                //        sItem["HNLPD"] = "----";
                //        sItem["HNL"] = "----";
                //    }
                //    sItem["NKHLPD"] = "";
                //    sItem["NKHL"] = "0";
                //    if (jcxm.Contains("、泥块含量、"))
                //    {
                //        double mnkhl1 = 0;
                //        double mnkhl2 = 0;
                //        if (Conversion.Val(sItem["NKHLG1"]) != 0 && Conversion.Val(sItem["NKHLG1_2"]) != 0)
                //        {
                //            mnkhl1 = Round((Conversion.Val(sItem["NKHLG1"]) - Conversion.Val(sItem["NKHLG2"])) / Conversion.Val(sItem["NKHLG1"]) * 100, 1);
                //            mnkhl2 = Round((Conversion.Val(sItem["NKHLG1_2"]) - Conversion.Val(sItem["NKHLG2_2"])) / Conversion.Val(sItem["NKHLG1_2"]) * 100, 1);
                //            sItem["NKHL"] = Round((mnkhl1 + mnkhl2) / 2, 1).ToString();
                //            var mrsZbyq_where = extraZBYQ.Where(x => x["MC"].Equals("泥块含量"));
                //            foreach (var item in mrsZbyq_where)
                //            {
                //                if (IsQualified(item["YQ"], sItem["NKHL"]) == "符合")
                //                {
                //                    sItem["NKHLPD"] = item["DJ"].Trim();
                //                    break;
                //                }
                //            }
                //            if (sItem["NKHLPD"] == "")
                //            {
                //                sItem["NKHLPD"] = "不符合";
                //                mbhgs = mbhgs + 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        sItem["NKHL"] = "----";
                //        sItem["NKHLPD"] = "----";
                //    }
                //    sItem["JMMDPD"] = "";
                //    sItem["JMMD"] = "0";
                //    sItem["DJMDPD"] = "";
                //    sItem["DJMD"] = "0";
                //    double mdjmd1 = 0;
                //    double mdjmd2 = 0;
                //    if (jcxm.Contains("、紧密密度和堆积密度、"))
                //    {
                //        if (Conversion.Val(sItem["JMMDV"]) != 0)
                //        {
                //            mdjmd1 = Round(((Conversion.Val(sItem["JMMDG1"])) - (Conversion.Val(sItem["JMMDG2"]))) / ((Conversion.Val(sItem["JMMDV"])) * 10) * 1000, 0) * 10;
                //            mdjmd2 = Round(((Conversion.Val(sItem["JMMDG1_2"])) - (Conversion.Val(sItem["JMMDG2_2"]))) / ((Conversion.Val(sItem["JMMDV"])) * 10) * 1000, 0) * 10;
                //            sItem["JMMD"] = (Round((mdjmd1 + mdjmd2) / 20, 0) * 10).ToString();
                //        }
                //        if (Conversion.Val(sItem["DJMDV"]) != 0)
                //        {
                //            mdjmd1 = Round(((Conversion.Val(sItem["DJMDG1"])) - (Conversion.Val(sItem["DJMDG2"]))) / ((Conversion.Val(sItem["DJMDV"])) * 10) * 1000, 0) * 10;
                //            mdjmd2 = Round(((Conversion.Val(sItem["DJMDG1_2"])) - (Conversion.Val(sItem["DJMDG2_2"]))) / ((Conversion.Val(sItem["DJMDV"])) * 10) * 1000, 0) * 10;
                //            sItem["DJMD"] = (Round((mdjmd1 + mdjmd2) / 20, 0) * 10).ToString();
                //        }
                //    }
                //    else
                //    {
                //        sItem["JMMD"] = "----";
                //        sItem["JMMDPD"] = "----";
                //        sItem["DJMDPD"] = "----";
                //        sItem["DJMD"] = "----";
                //    }
                //    sItem["BGMDPD"] = "";
                //    sItem["BGMD"] = "0";
                //    double mbgmd1 = 0;
                //    double mbgmd2 = 0;
                //    if (jcxm.Contains("、表观密度、"))
                //    {
                //        if (sItem["BGSYFF"] == "标准法")
                //        {
                //            sum = 0;
                //            narr = new double[3];
                //            for (xd = 1; xd <= 2; xd++)
                //            {
                //                md = Conversion.Val(sItem["BGBZ" + xd + "_M0"].Trim());
                //                md1 = Conversion.Val(sItem["BGBZ" + xd + "_M1"].Trim());
                //                md2 = Conversion.Val(sItem["BGBZ" + xd + "_M2"].Trim());
                //                md = md / (md + md1 - md2);
                //                if (IsNumeric(sItem["SWXZXS"]))
                //                    md = (md) - Conversion.Val(sItem["SWXZXS"].Trim());

                //                md = Round((md * 100), 0) * 10;
                //                sum = sum + md;
                //                narr[xd] = md;
                //            }
                //            mbgmd1 = Round((narr[1]), 0);
                //            mbgmd2 = Round((narr[2]), 0);
                //            pjmd = sum / 20;
                //            pjmd = Round((pjmd), 0) * 10;
                //        }
                //        else
                //        {
                //            mbgmd1 = Round(((Conversion.Val(sItem["BGMDG0"])) / ((Conversion.Val(sItem["BGMDG0"])) + (Conversion.Val(sItem["BGMDG2"])) - (Conversion.Val(sItem["BGMDG1"]))) - (Conversion.Val(sItem["SWXZXS"]))) * 100, 0) * 10;
                //            mbgmd2 = Round(((Conversion.Val(sItem["BGMDG0_2"])) / ((Conversion.Val(sItem["BGMDG0_2"])) + (Conversion.Val(sItem["BGMDG2_2"])) - (Conversion.Val(sItem["BGMDG1_2"]))) - (Conversion.Val(sItem["SWXZXS"]))) * 100, 0) * 10;
                //            pjmd = Round((mbgmd1 + mbgmd2) / 20, 0) * 10;
                //        }

                //        sItem["BGMD"] = pjmd.ToString("0");
                //        if (Math.Abs((mbgmd1 - mbgmd2)) > 20)
                //            sItem["BGMDPD"] = "两次结果差大于20，须重新取样试验";
                //    }
                //    else
                //    {
                //        sItem["BGMD"] = "----";
                //        sItem["BGMDPD"] = "----";
                //    }
                //    sItem["KXLPD"] = "";
                //    sItem["KXL"] = "0";
                //    if (jcxm.Contains("、空隙率、"))
                //    {
                //        sItem["KXLP1"] = mdjmd1.ToString();
                //        sItem["KXLP1_2"] = mdjmd2.ToString();
                //        sItem["KXLP2"] = mbgmd1.ToString();
                //        sItem["KXLP2_2"] = mbgmd2.ToString();
                //        if (Conversion.Val(sItem["KXLP2"]) != 0 && Conversion.Val(sItem["KXLP2_2"]) != 0)
                //        {
                //            double mkxl1 = Round((1 - Conversion.Val(sItem["KXLP1"]) / Conversion.Val(sItem["KXLP2"])) * 100, 0);
                //            double mkxl2 = Round((1 - Conversion.Val(sItem["KXLP1_2"]) / Conversion.Val(sItem["KXLP2_2"])) * 100, 0);
                //            sItem["KXL"] = Round((mkxl1 + mkxl2) / 2, 0).ToString();
                //        }
                //    }
                //    else
                //    {
                //        sItem["KXL"] = "----";
                //        sItem["KXLPD"] = "----";
                //    }
                //    sItem["YSZBPD"] = "";
                //    sItem["YSZB"] = "0";
                //    if (jcxm.Contains("、压碎指标值、"))
                //    {
                //        double myszb1 = 0;
                //        double myszb2 = 0;
                //        double myszb3 = 0;
                //        if (Conversion.Val(sItem["YSZBG1"]) != 0 && Conversion.Val(sItem["YSZBG1_2"]) != 0 && Conversion.Val(sItem["YSZBG1_3"]) != 0)
                //        {
                //            myszb1 = Round((Conversion.Val(sItem["YSZBG1"]) - Conversion.Val(sItem["YSZBG2"])) / Conversion.Val(sItem["YSZBG1"]) * 100, 1);
                //            myszb2 = Round((Conversion.Val(sItem["YSZBG1_2"]) - Conversion.Val(sItem["YSZBG2_2"])) / Conversion.Val(sItem["YSZBG1_2"]) * 100, 1);
                //            myszb3 = Round((Conversion.Val(sItem["YSZBG1_3"]) - Conversion.Val(sItem["YSZBG2_3"])) / Conversion.Val(sItem["YSZBG1_3"]) * 100, 1);
                //            sItem["YSZB"] = Round((myszb1 + myszb2 + myszb3) / 3, 1).ToString();
                //            var mrsZbyq_where = extraZBYQ.Where(x => x["MC"].Equals("压碎性指标") && x["SPZ"].Equals(sItem["SIPZ"].Trim()));
                //            foreach (var item in mrsZbyq_where)
                //            {
                //                if (IsQualified(item["YQ"], sItem["YSZB"]) == "符合")
                //                {
                //                    sItem["YSZBPD"] = item["DJ"].Trim();
                //                    break;
                //                }
                //            }
                //            if (sItem["YSZBPD"] == "")
                //            {
                //                sItem["YSZBPD"] = "不符合";
                //                mbhgs = mbhgs + 1;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        sItem["YSZB"] = "----";
                //        sItem["YSZBPD"] = "----";
                //    }
                //    sItem["XSLPD"] = "";
                //    sItem["XSL"] = "0";
                //    if (jcxm.Contains("、吸水率、"))
                //    {
                //        double mxsl1 = 0;
                //        double mxsl2 = 0;
                //        if ((Conversion.Val(sItem["XSLG1"]) - Conversion.Val(sItem["XSLG3"])) != 0 && (Conversion.Val(sItem["XSLG1_2"]) - Conversion.Val(sItem["XSLG3_2"])) != 0)
                //        {
                //            mxsl1 = Round((Conversion.Val(sItem["XSLG2"]) - Conversion.Val(sItem["XSLG1"])) / (Conversion.Val(sItem["XSLG1"]) - Conversion.Val(sItem["XSLG3"])) * 100, 2);
                //            mxsl2 = Round((Conversion.Val(sItem["XSLG2_2"]) - Conversion.Val(sItem["XSLG1_2"])) / (Conversion.Val(sItem["XSLG1_2"]) - Conversion.Val(sItem["XSLG3_2"])) * 100, 2);
                //            sItem["XSL"] = Round((mxsl1 + mxsl2) / 2, 2).ToString();
                //        }
                //    }
                //    else
                //    {
                //        sItem["XSLPD"] = "----";
                //        sItem["XSL"] = "----";
                //    }
                //    sItem["HSLPD"] = "";
                //    sItem["HSL"] = "0";
                //    if (jcxm.Contains("、含水率、"))
                //    {
                //        double mhsl1 = 0;
                //        double mhsl2 = 0;
                //        if (Conversion.Val(sItem["HSLG2"]) - Conversion.Val(sItem["HSLG3"]) != 0 && (Conversion.Val(sItem["HSLG2_2"]) - Conversion.Val(sItem["HSLG3_2"])) != 0)
                //        {
                //            mhsl1 = Round((Conversion.Val(sItem["HSLG1"]) - Conversion.Val(sItem["HSLG2"])) / (Conversion.Val(sItem["HSLG2"]) - Conversion.Val(sItem["HSLG3"])) * 100, 1);
                //            mhsl2 = Round((Conversion.Val(sItem["HSLG1_2"]) - Conversion.Val(sItem["HSLG2_2"])) / (Conversion.Val(sItem["HSLG2_2"]) - Conversion.Val(sItem["HSLG3_2"])) * 100, 1);
                //            sItem["HSL"] = Round((mhsl1 + mhsl2) / 2, 1).ToString();
                //        }
                //    }
                //    else
                //    {
                //        sItem["HSLPD"] = "----";
                //        sItem["HSL"] = "----";
                //    }
                //    sItem["ZPZHLPD"] = "";
                //    sItem["ZPZHL"] = "0";
                //    if (jcxm.Contains("、针片状含量、"))
                //    {
                //        if (Conversion.Val(sItem["ZPZHLG1"]) != 0)
                //            sItem["ZPZHL"] = Round(Conversion.Val(sItem["ZPZHLG2"]) / Conversion.Val(sItem["ZPZHLG1"]) * 100, 0).ToString();
                //        var mrsZbyq_where = extraZBYQ.Where(x => x["MC"].Equals("针片状含量"));
                //        foreach (var item in mrsZbyq_where)
                //        {
                //            if (IsQualified(item["YQ"], sItem["ZPZHL"]) == "符合")
                //            {
                //                sItem["ZPZHLPD"] = item["DJ"].Trim();
                //                break;
                //            }
                //        }
                //        if (sItem["ZPZHLPD"] == "")
                //        {
                //            sItem["ZPZHLPD"] = "不符合";
                //            mbhgs = mbhgs + 1;
                //        }
                //    }
                //    else
                //    {
                //        sItem["ZPZHLPD"] = "----";
                //        sItem["ZPZHL"] = "----";
                //    }
                //    sItem["KYQDPD"] = "";
                //    sItem["KYQD"] = "0";
                //    if (jcxm.Contains("、抗压强度、"))
                //    {
                //        double mkyqd1 = 0;
                //        double mkyqd2 = 0;
                //        double mkyqd3 = 0;
                //        double mkyqd4 = 0;
                //        double mkyqd5 = 0;
                //        double mkyqd6 = 0;
                //        if (GetSafeDouble(sItem["KYQDA1"]) != 0 && GetSafeDouble(sItem["KYQDA2"]) != 0 && GetSafeDouble(sItem["KYQDA3"]) != 0 && GetSafeDouble(sItem["KYQDA4"]) != 0 && GetSafeDouble(sItem["KYQDA5"]) != 0 && GetSafeDouble(sItem["KYQDA6"]) != 0)
                //        {
                //            mkyqd1 = Round(GetSafeDouble(sItem["KYQDF1"]) / GetSafeDouble(sItem["KYQDA1"]), 2);
                //            mkyqd2 = Round(GetSafeDouble(sItem["KYQDF2"]) / GetSafeDouble(sItem["KYQDA2"]), 2);
                //            mkyqd3 = Round(GetSafeDouble(sItem["KYQDF3"]) / GetSafeDouble(sItem["KYQDA3"]), 2);
                //            mkyqd4 = Round(GetSafeDouble(sItem["KYQDF4"]) / GetSafeDouble(sItem["KYQDA4"]), 2);
                //            mkyqd5 = Round(GetSafeDouble(sItem["KYQDF5"]) / GetSafeDouble(sItem["KYQDA5"]), 2);
                //            mkyqd6 = Round(GetSafeDouble(sItem["KYQDF6"]) / GetSafeDouble(sItem["KYQDA6"]), 2);
                //            sItem["KYQD"] = Round((mkyqd1 + mkyqd2 + mkyqd3 + mkyqd4 + mkyqd5 + mkyqd6) / 6, 0).ToString();
                //            mlongStr = mkyqd1 + "," + mkyqd2 + "," + mkyqd3 + "," + mkyqd4 + "," + mkyqd5 + "," + mkyqd6;
                //            mtmpArray = mlongStr.Split(',');
                //            for (vp = 0; vp <= 5; vp++)
                //                mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                //            Array.Sort(mkyqdArray);
                //            sItem["QDMIN"] = mkyqdArray[0].ToString();
                //            if (3 * (mkyqdArray[0]) < (mkyqdArray.Average() * 6 - mkyqdArray[0] - mkyqdArray[1]) / 4 && 3 * mkyqdArray[1] < (mkyqdArray.Average() * 6 - mkyqdArray[0] - mkyqdArray[1]) / 4)
                //                sItem["KYQD"] = Round((mkyqdArray.Average() * 6 - mkyqdArray[0] - mkyqdArray[1]) / 4, 0).ToString();
                //            if (3 * mkyqdArray[0] < (mkyqdArray.Average() * 6 - mkyqdArray[0] - mkyqdArray[5]) / 4 && mkyqdArray[5] > 3 * (mkyqdArray.Average() * 6 - mkyqdArray[0] - mkyqdArray[5]) / 4)
                //                sItem["KYQD"] = Round((mkyqdArray.Average() * 6 - mkyqdArray[0] - mkyqdArray[5]) / 4, 0).ToString();
                //            if (mkyqdArray[4] > 3 * (mkyqdArray.Average() * 6 - mkyqdArray[4] - mkyqdArray[5]) / 4 && mkyqdArray[5] > 3 * (mkyqdArray.Average() * 6 - mkyqdArray[4] - mkyqdArray[5]) / 4)
                //                sItem["KYQD"] = Round((mkyqdArray.Average() * 6 - mkyqdArray[4] - mkyqdArray[5]) / 4, 0).ToString();

                //        }
                //    }
                //    else
                //    {
                //        sItem["KYQD"] = "----";
                //        sItem["KYQDPD"] = "----";
                //    }
                //    sItem["LHWPD"] = "";
                //    sItem["LHW"] = "0";
                //    if (jcxm.Contains("、硫化物和硫酸盐含量、"))
                //    {
                //        double mlhw1 = 0;
                //        double mlhw2 = 0;
                //        if (GetSafeDouble(sItem["LHWM"]) != 0 && GetSafeDouble(sItem["LHWM_2"]) != 0)
                //        {
                //            mlhw1 = Round((GetSafeDouble(sItem["LHWM2"]) - GetSafeDouble(sItem["LHWM1"])) / GetSafeDouble(sItem["LHWM"]) * 0.343 * 100, 2);
                //            mlhw2 = Round((GetSafeDouble(sItem["LHWM2_2"]) - GetSafeDouble(sItem["LHWM1_2"])) / GetSafeDouble(sItem["LHWM_2"]) * 0.343 * 100, 2);
                //            sItem["LHW"] = Round((mlhw1 + mlhw2) / 2, 2).ToString();
                //            var mrsZbyq_where = extraZBYQ.Where(x => x["MC"].Equals("硫化物和硫酸盐含量"));
                //            foreach (var item in mrsZbyq_where)
                //            {
                //                if (IsQualified(item["YQ"], sItem["LHW"]) == "符合")
                //                {
                //                    sItem["LHWPD"] = item["DJ"].Trim();
                //                    break;
                //                }
                //            }
                //            if (sItem["LHWPD"] == "")
                //            {
                //                sItem["LHWPD"] = "不符合";
                //                mbhgs = mbhgs + 1;
                //            }
                //            if (Math.Abs(mlhw1 - mlhw2) > 0.15)
                //                sItem["LHWPD"] = "两次结果差大于0.15，须重新取样试验";
                //        }
                //    }
                //    else
                //    {
                //        sItem["LHW"] = "----";
                //        sItem["LHWPD"] = "----";
                //    }
                //    if (jcxm.Contains("、有机物含量、"))
                //    {
                //        if (sItem["YJWHLPD"] == "不合格" || sItem["YJWHLPD"] == "不符合")
                //            sItem["YJWHLPD"] = "不符合";
                //        else
                //            sItem["YJWHLPD"] = "符合";
                //    }
                //    else
                //    {
                //        sItem["YJWHLPD"] = "----";
                //        sItem["YJWHLPD"] = "----";
                //        sItem["YJWHL"] = "----";
                //    }
                //    if (jcxm.Contains("、碱活性、"))
                //    {
                //        if (Conversion.Val(sItem["JHX"]) < 0.1)
                //        {
                //            sItem["JHXPD"] = "无潜在危害";
                //        }
                //        else
                //        {

                //            if (Conversion.Val(sItem["JHX"]) > 0.2)
                //            {
                //                sItem["JHXPD"] = "有潜在危害";
                //            }
                //            else
                //            {
                //                sItem["JHXPD"] = "需按7.17节进行复试";
                //            }
                //            itemHG = false;
                //            mAllHg = false;
                //        }
                //    }
                //    else
                //    {
                //        sItem["JHXPD"] = "----";
                //        sItem["JHX"] = "----";
                //    }
                //    if (mbhgs > 0)
                //    {
                //        sItem["JCJG"] = "不合格";
                //        mAllHg = false;
                //    }
                //    else
                //        sItem["JCJG"] = "合格";
                //}

                //单组判断
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合上述标准要求。";
            }
            else
            {
                jsbeizhu = "该组试样所检项目不符合上述标准要求。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
