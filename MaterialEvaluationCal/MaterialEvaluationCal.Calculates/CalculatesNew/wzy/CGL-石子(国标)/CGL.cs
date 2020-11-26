using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class CGL : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "", mSjdj = "";
            var extraDJ = dataExtra["BZ_CGL_DJ"];
            var bzHSBData = dataExtra["BZ_CGLHSB"];
            int mbhggs = 0;//不合格数量
            double g_hnl = 0, g_nkhl = 0, g_bgmd = 0, g_djmd = 0, g_kxl = 0, g_zpzhl = 0, g_jmmd = 0, g_kyqd = 0, g_jhx = 0;
            double mSz = 0, mQdyq = 0;
            string mJSFF = "";
            double mdjmd1 = 0, mdjmd2 = 0;
            double mbgmd1 = 0, mbgmd2 = 0;
            int mzslbs, mslbs;


            var data = retData;

            var SItem = data["S_CGL"];
            var MItem = data["M_CGL"];
            if (!data.ContainsKey("M_CGL"))
            {
                data["M_CGL"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                #region 数据准备工作
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //获取设计等级
                mSjdj = sItem["SJDJ"];
                if (string.IsNullOrEmpty(sItem["SJDJ"]))
                {
                    mSjdj = "";
                }

                IDictionary<string, string> extraFieldsDj = null;
                //从设计等级表中取得相应的计算数值、等级标准
                if (MItem[0]["JCYJ"].Contains("14685-2011"))
                {
                    extraFieldsDj = extraDJ.FirstOrDefault(x => x["MC"].Contains(sItem["SJDJ"].Trim()) && x["JCYJ"].Contains("14685-2011"));
                }
                else
                {
                    extraFieldsDj = extraDJ.FirstOrDefault(x => x["MC"].Contains(sItem["SJDJ"].Trim()) && x["JCYJ"].Contains("14685-2001"));
                }

                if (extraFieldsDj != null)
                {
                    g_hnl = GetSafeDouble(extraFieldsDj["HNL"]);
                    g_nkhl = GetSafeDouble(extraFieldsDj["NKHL"]);
                    g_bgmd = GetSafeDouble(extraFieldsDj["BGMD"]);
                    g_djmd = GetSafeDouble(extraFieldsDj["DJMD"]);
                    g_kxl = GetSafeDouble(extraFieldsDj["KXL"]);
                    g_zpzhl = GetSafeDouble(extraFieldsDj["ZPZHL"]);
                    g_jmmd = GetSafeDouble(extraFieldsDj["JMMD"]);
                    g_kyqd = GetSafeDouble(extraFieldsDj["KYQD"]);
                    g_jhx = GetSafeDouble(extraFieldsDj["JHX"]);
                    //MItem[0]["WHICH"] = extraFieldsDj["WHICH"];
                    mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"];
                }
                else
                {
                    mSz = 0;
                    mQdyq = 0;
                    mJSFF = "";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }
                #endregion

                #region
                sItem["JPPD"] = "----";

                #region 级配
                if (jcxm.Contains("、级配、"))
                {
                    jcxmCur = "级配";
                    double[] narr = new double[13];
                    for (int i = 0; i < 13; i++)
                    {
                        narr[i] = 0;
                    }

                    narr[0] = string.IsNullOrEmpty(sItem["SY2_36"]) ? 0 : GetSafeDouble(sItem["SY2_36"]);
                    narr[1] = string.IsNullOrEmpty(sItem["SY4_75"]) ? 0 : GetSafeDouble(sItem["SY4_75"]);
                    narr[2] = string.IsNullOrEmpty(sItem["SY9_50"]) ? 0 : GetSafeDouble(sItem["SY9_50"]);
                    narr[3] = string.IsNullOrEmpty(sItem["SY16_0"]) ? 0 : GetSafeDouble(sItem["SY16_0"]);
                    narr[4] = string.IsNullOrEmpty(sItem["SY19_0"]) ? 0 : GetSafeDouble(sItem["SY19_0"]);
                    narr[5] = string.IsNullOrEmpty(sItem["SY26_5"]) ? 0 : GetSafeDouble(sItem["SY26_5"]);
                    narr[6] = string.IsNullOrEmpty(sItem["SY31_5"]) ? 0 : GetSafeDouble(sItem["SY31_5"]);
                    narr[7] = string.IsNullOrEmpty(sItem["SY37_5"]) ? 0 : GetSafeDouble(sItem["SY37_5"]);
                    narr[8] = string.IsNullOrEmpty(sItem["SY53_0"]) ? 0 : GetSafeDouble(sItem["SY53_0"]);
                    narr[9] = string.IsNullOrEmpty(sItem["SY63_0"]) ? 0 : GetSafeDouble(sItem["SY63_0"]);
                    narr[10] = string.IsNullOrEmpty(sItem["SY75_0"]) ? 0 : GetSafeDouble(sItem["SY75_0"]);
                    narr[11] = string.IsNullOrEmpty(sItem["SY90"]) ? 0 : GetSafeDouble(sItem["SY90"]);
                    narr[12] = string.IsNullOrEmpty(sItem["SY100"]) ? 0 : GetSafeDouble(sItem["SY100"]);
                    double sum = 0;
                    sum = string.IsNullOrEmpty(MItem[0]["SIZL"]) ? 0 : GetSafeDouble(MItem[0]["SIZL"]);
                    if (sum == 0)
                    {
                        sum = narr.Sum();
                    }

                    double md = 0;
                    for (int i = 0; i < 13; i++)
                    {
                        md = 100 * narr[i] / sum;
                        narr[i] = Math.Round(md, 1);
                    }
                    sum = 0;

                    md = Math.Round(narr[11], 1);
                    sum = sum + md;
                    sItem["CY90"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[10], 1);
                    sum = sum + md;
                    sItem["CY75_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[9], 1);
                    sum = sum + md;
                    sItem["CY63_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[8], 1);
                    sum = sum + md;
                    sItem["CY53_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[7], 1);
                    sum = sum + md;
                    sItem["CY37_5"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[6], 1);
                    sum = sum + md;
                    sItem["CY31_5"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[5], 1);
                    sum = sum + md;
                    sItem["CY26_5"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[4], 1);
                    sum = sum + md;
                    sItem["CY19_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[3], 1);
                    sum = sum + md;
                    sItem["CY16_0"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[2], 1);
                    sum = sum + md;
                    sItem["CY9_50"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[1], 1);
                    sum = sum + md;
                    sItem["CY4_75"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[0], 1);
                    sum = sum + md;
                    sItem["CY2_36"] = md > 100 ? "100" : sum.ToString("0");

                    md = Math.Round(narr[12], 1);
                    sum = sum + md;
                    sItem["CY100"] = md > 100 ? "100" : sum.ToString("0");

                    //var bzHSB = bzHSBData.FirstOrDefault(u => u.Keys.Contains("GCCC") && u.Values.Contains(sItem["GCCC"].Trim()));
                    IDictionary<string, string> bzHSB = null;
                    if (MItem[0]["JCYJ"] == "14685-2011")
                    {
                        bzHSB = bzHSBData.FirstOrDefault(u => u["GCCC"].Contains(sItem["GCCC"].Trim()) && u["BZDH"].Contains("14685-2011"));
                    }
                    else
                    {
                        bzHSB = bzHSBData.FirstOrDefault(u => u["GCCC"].Contains(sItem["GCCC"].Trim()) && u["BZDH"].Contains("14685-2001"));
                    }
                    if (bzHSB != null)
                    {
                        bool flag = true;
                        flag = GetSafeDouble(sItem["CY2_36"]) >= GetSafeDouble(bzHSB["CY2_36A"]) && GetSafeDouble(sItem["CY2_36"]) <= GetSafeDouble(bzHSB["CY2_36B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY4_75"]) >= GetSafeDouble(bzHSB["CY4_75A"]) && GetSafeDouble(sItem["CY4_75"]) <= GetSafeDouble(bzHSB["CY4_75B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY9_50"]) >= GetSafeDouble(bzHSB["CY9_50A"]) && GetSafeDouble(sItem["CY9_50"]) <= GetSafeDouble(bzHSB["CY9_50B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY16_0"]) >= GetSafeDouble(bzHSB["CY16_0A"]) && GetSafeDouble(sItem["CY16_0"]) <= GetSafeDouble(bzHSB["CY16_0B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY19_0"]) >= GetSafeDouble(bzHSB["CY19_0A"]) && GetSafeDouble(sItem["CY19_0"]) <= GetSafeDouble(bzHSB["CY19_0B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY26_5"]) >= GetSafeDouble(bzHSB["CY26_5A"]) && GetSafeDouble(sItem["CY26_5"]) <= GetSafeDouble(bzHSB["CY26_5B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY31_5"]) >= GetSafeDouble(bzHSB["CY31_5A"]) && GetSafeDouble(sItem["CY31_5"]) <= GetSafeDouble(bzHSB["CY31_5B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY37_5"]) >= GetSafeDouble(bzHSB["CY37_5A"]) && GetSafeDouble(sItem["CY37_5"]) <= GetSafeDouble(bzHSB["CY37_5B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY53_0"]) >= GetSafeDouble(bzHSB["CY53_0A"]) && GetSafeDouble(sItem["CY53_0"]) <= GetSafeDouble(bzHSB["CY53_0B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY63_0"]) >= GetSafeDouble(bzHSB["CY63_0A"]) && GetSafeDouble(sItem["CY63_0"]) <= GetSafeDouble(bzHSB["CY63_0B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY75_0"]) >= GetSafeDouble(bzHSB["CY75_0A"]) && GetSafeDouble(sItem["CY75_0"]) <= GetSafeDouble(bzHSB["CY75_0B"]) ? flag : false;
                        flag = GetSafeDouble(sItem["CY90"]) >= GetSafeDouble(bzHSB["CY90A"]) && GetSafeDouble(sItem["CY4_75"]) <= GetSafeDouble(bzHSB["CY90B"]) ? flag : false;

                        if (flag)
                        {
                            sItem["JCJG"] = "合格";
                            sItem["JPPD"] = "符合公称尺寸为" + bzHSB["GCCC"] + "的颗粒级配";
                        }
                        else
                        {
                            sItem["JCJG"] = "不合格";
                            sItem["JPPD"] = "不符合公称尺寸为" + bzHSB["GCCC"] + "的颗粒级配";
                            mbhggs++;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        sItem["SJGCCC"] = flag ? bzHSB["GCCC"] : sItem["SJGCCC"];

                        sItem["JPPD"] = (100 - GetSafeDouble(sItem["CY100"]) > 1) ? "试验需重做" : sItem["JPPD"];
                        //针对不同规格的石子 显示不同的筛孔 通过率

                        if (GetSafeDouble(bzHSB["CY2_36A"]) == 0 && GetSafeDouble(bzHSB["CY2_36B"]) == 100)
                        {
                            sItem["CY2_36"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY4_75A"]) == 0 && GetSafeDouble(bzHSB["CY4_75B"]) == 100)
                        {
                            sItem["CY4_75"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY9_50A"]) == 0 && GetSafeDouble(bzHSB["CY9_50B"]) == 100)
                        {
                            sItem["CY9_50"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY16_0A"]) == 0 && GetSafeDouble(bzHSB["CY16_0B"]) == 100)
                        {
                            sItem["CY16_0"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY19_0A"]) == 0 && GetSafeDouble(bzHSB["CY19_0B"]) == 100)
                        {
                            sItem["CY19_0"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY26_5A"]) == 0 && GetSafeDouble(bzHSB["CY26_5B"]) == 100)
                        {
                            sItem["CY26_5"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY31_5A"]) == 0 && GetSafeDouble(bzHSB["CY31_5B"]) == 100)
                        {
                            sItem["CY31_5"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY37_5A"]) == 0 && GetSafeDouble(bzHSB["CY37_5B"]) == 100)
                        {
                            sItem["CY37_5"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY53_0A"]) == 0 && GetSafeDouble(bzHSB["CY53_0B"]) == 100)
                        {
                            sItem["CY53_0"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY63_0A"]) == 0 && GetSafeDouble(bzHSB["CY63_0B"]) == 100)
                        {
                            sItem["CY63_0"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY75_0A"]) == 0 && GetSafeDouble(bzHSB["CY75_0B"]) == 100)
                        {
                            sItem["CY75_0"] = "-";
                        }

                        if (GetSafeDouble(bzHSB["CY90A"]) == 0 && GetSafeDouble(bzHSB["CY90B"]) == 100)
                        {
                            sItem["CY90"] = "-";
                        }
                    }
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
                    jcxmCur = "坚固性";
                    double mfjzlss1 = 0, mfjzlss2 = 0, mfjzlss3 = 0, mfjzlss4 = 0, mfjzlss5 = 0, masum = 0, ma1=0, ma2 = 0, ma3 = 0, ma4 = 0, ma5 = 0;
                    mfjzlss1 = Math.Round((Conversion.Val(sItem["JGXQG1_1"]) - Conversion.Val(sItem["JGXHG2_1"])) / Conversion.Val(sItem["JGXQG1_1"]) * 100, 1);
                    mfjzlss2 = Math.Round((Conversion.Val(sItem["JGXQG1_2"]) - Conversion.Val(sItem["JGXHG2_2"])) / Conversion.Val(sItem["JGXQG1_2"]) * 100, 1);
                    mfjzlss3 = Math.Round((Conversion.Val(sItem["JGXQG1_3"]) - Conversion.Val(sItem["JGXHG2_3"])) / Conversion.Val(sItem["JGXQG1_3"]) * 100, 1);
                    mfjzlss4 = Math.Round((Conversion.Val(sItem["JGXQG1_4"]) - Conversion.Val(sItem["JGXHG2_4"])) / Conversion.Val(sItem["JGXQG1_4"]) * 100, 1);
                    mfjzlss5 = Math.Round((Conversion.Val(sItem["JGXQG1_5"]) - Conversion.Val(sItem["JGXHG2_5"])) / Conversion.Val(sItem["JGXQG1_5"]) * 100, 1);
                    masum = Conversion.Val(sItem["JGXQG1_1"]) + Conversion.Val(sItem["JGXQG1_2"]) + Conversion.Val(sItem["JGXQG1_3"]) + Conversion.Val(sItem["JGXQG1_4"]) + Conversion.Val(sItem["JGXQG1_5"]);
                    ma1 = Math.Round(Conversion.Val(sItem["JGXHG2_1"]) / masum * 100, 2);
                    ma2 = Math.Round(Conversion.Val(sItem["JGXHG2_2"]) / masum * 100, 2);
                    ma3 = Math.Round(Conversion.Val(sItem["JGXHG2_3"]) / masum * 100, 2);
                    ma4 = Math.Round(Conversion.Val(sItem["JGXHG2_4"]) / masum * 100, 2);
                    ma5 = Math.Round(Conversion.Val(sItem["JGXHG2_5"]) / masum * 100, 2);
                    sItem["JGX"] = Math.Round((ma1 * mfjzlss1 + ma2 * mfjzlss2 + ma3 * mfjzlss3 + ma4 * mfjzlss4 + ma5 * mfjzlss5) / (ma1 + ma2 + ma3 + ma4 + ma5), 0).ToString();

                    List <IDictionary<string, string>> extraFieldsDj1 = extraDJ.ToList();
                    if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    {
                        if (MItem[0]["JCYJ"].Contains("14685-2011"))
                        {
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"]) && u["JCYJ"].Contains("14685-2011")).ToList();
                        }
                        else
                        {
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                        }
                    }
                    foreach (var eDj in extraFieldsDj1)
                    {
                        sItem["G_JGX"] = "≤" + Math.Round(GetSafeDouble(eDj["JGX"]),0).ToString("0");
                        if (GetSafeDouble(sItem["JGX"]) < GetSafeDouble(eDj["JGX"]) || GetSafeDouble(sItem["JGX"]) == 0)
                        {
                            sItem["JGXPD"] = eDj["MC"].Trim().Substring(eDj["MC"].Length - 2, 2);
                            break;
                        }
                    }

                    if (sItem["JGXPD"] == "")
                    {
                        sItem["JGXPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["JGXPD"] = "----";
                    sItem["JGX"] = "----";
                    sItem["G_JGX"] = "----";
                }
                #endregion

                IList<IDictionary<string, string>> mrsDj_Where = new List<IDictionary<string, string>>();
                if (mSjdj.Trim().Length > 2)
                {
                    if (MItem[0]["JCYJ"].Contains("14685-2011"))
                    {
                        //mrsDj_Where = extraDJ.Where(x => x["MC"].Contains(mSjdj.Substring(0, mSjdj.Length - 2)) && x["JCYJ"].Contains("14685-2011")).ToList();
                        mrsDj_Where = extraDJ.Where(x => x["MC"].Contains(mSjdj) && x["JCYJ"].Contains("14685-2011")).ToList();
                    }
                    else
                        mrsDj_Where = extraDJ.Where(x => x["MC"].Contains(mSjdj.Substring(0, mSjdj.Length - 2)) && x["JCYJ"].Contains("14685-2001")).ToList();
                }

                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    int mbhgs;
                    mbhgs = 0;
                    int xd;
                    double md1, md2, sum, pjmd;
                    double[] narr;
                    if (jcxm.Contains("、含泥量、"))
                    {
                        sItem["HNLPD"] = "";
                        foreach (var mrsDj_item in mrsDj_Where)
                        {
                            if (Conversion.Val(sItem["HNL"]) <= Conversion.Val(mrsDj_item["HNL"]))
                            {
                                sItem["HNLPD"] = mrsDj_item["MC"].Trim().Substring(mrsDj_item["MC"].Trim().Length - 2, 2);
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["HNLPD"]))
                        {
                            sItem["HNLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sItem["HNL"] = "----";
                        sItem["HNLPD"] = "----";
                    }
                    if (jcxm.Contains("、泥块含量、"))
                    {
                        sItem["NKHLPD"] = "";
                        foreach (var mrsDj_item in mrsDj_Where)
                        {
                            if (Conversion.Val(sItem["NKHL"]) <= Conversion.Val(mrsDj_item["NKHL"]))
                            {
                                sItem["NKHLPD"] = mrsDj_item["MC"].Trim().Substring(mrsDj_item["MC"].Trim().Length - 2, 2);
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["NKHLPD"]))
                        {
                            sItem["NKHLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sItem["NKHLPD"] = "----";
                        sItem["NKHL"] = "----";
                    }
                    if (jcxm.Contains("、堆积密度、"))
                    {
                        if (MItem[0]["JCYJ"].Contains("14685-2011"))
                            sItem["DJMDPD"] = "----";
                        else
                        {
                            if (Conversion.Val(sItem["DJMD"]) > Conversion.Val(mrsDj_Where[0]["DJMD"]))
                                sItem["DJMDPD"] = "符合";
                            else
                            {
                                sItem["DJMDPD"] = "不符合";
                                mbhgs = mbhgs + 1;
                            }
                        }
                    }
                    else
                    {
                        sItem["DJMDPD"] = "----";
                        sItem["DJMD"] = "----";
                    }
                    if (jcxm.Contains("、紧密密度、"))
                    {
                        if (Conversion.Val(sItem["JMMD"]) > Conversion.Val(mrsDj_Where[0]["JMMD"]))
                            sItem["JMMDPD"] = "符合";
                        else
                        {
                            sItem["JMMDPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sItem["JMMD"] = "----";
                        sItem["JMMDPD"] = "----";
                    }
                    if (jcxm.Contains("、表观密度、"))
                    {
                        if (Conversion.Val(sItem["BGMD"]) > Conversion.Val(mrsDj_Where[0]["BGMD"]))
                            sItem["BGMDPD"] = "符合";
                        else
                        {
                            sItem["BGMDPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sItem["BGMDPD"] = "----";
                        sItem["BGMD"] = "----";
                    }
                    if (jcxm.Contains("、空隙率、"))
                    {
                        sItem["KXLPD"] = "";
                        foreach (var mrsDj_item in mrsDj_Where)
                        {
                            if (Conversion.Val(sItem["KXL"]) <= Conversion.Val(mrsDj_item["KXL"]))
                            {
                                sItem["KXLPD"] = mrsDj_item["MC"].Trim().Substring(mrsDj_item["MC"].Trim().Length - 2, 2);
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["KXLPD"]))
                        {
                            sItem["KXLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sItem["KXLPD"] = "----";
                        sItem["KXL"] = "----";
                    }
                    if (jcxm.Contains("、压碎性指标、"))
                    {
                        sItem["YSZBPD"] = "";
                        foreach (var mrsDj_item in mrsDj_Where)
                        {
                            if (Conversion.Val(sItem["YSZB"]) < Conversion.Val(mrsDj_item["YSZB"]) || GetSafeDouble(sItem["YSZB"]) == 0)
                            {
                                sItem["YSZBPD"] = mrsDj_item["MC"].Trim().Substring(mrsDj_item["MC"].Trim().Length - 2, 2);
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["YSZBPD"]))
                        {
                            sItem["YSZBPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sItem["YSZBPD"] = "----";
                        sItem["YSZB"] = "----";
                    }
                    if (jcxm.Contains("、吸水率、"))
                    {
                        if (Conversion.Val(sItem["XSLG2"]) != 0 && Conversion.Val(sItem["XSLG2_2"]) != 0)
                        {
                            double mxsl1 = Round((Conversion.Val(sItem["XSLG1"]) - Conversion.Val(sItem["XSLG2"])) / Conversion.Val(sItem["XSLG2"]) * 100, 1);
                            double mxsl2 = Round((Conversion.Val(sItem["XSLG1_2"]) - Conversion.Val(sItem["XSLG2_2"])) / Conversion.Val(sItem["XSLG2_2"]) * 100, 1);
                            sItem["XSL"] = Round((mxsl1 + mxsl2) / 2, 1).ToString();
                        }
                        sItem["XSLPD"] = "----";
                        foreach (var mrsDj_item in mrsDj_Where)
                        {
                            if (Conversion.Val(sItem["XSL"]) <= Conversion.Val(mrsDj_item["XSL"]))
                            {
                                sItem["XSLPD"] = mrsDj_item["MC"].Trim().Substring(mrsDj_item["MC"].Trim().Length - 2, 2);
                                break;
                            }
                            if (string.IsNullOrEmpty(sItem["XSLPD"]))
                            {
                                sItem["XSLPD"] = "不符合";
                                mbhgs = mbhgs + 1;
                            }
                        }
                    }
                    else
                    {
                        sItem["XSLPD"] = "----";
                        sItem["XSL"] = "----";
                    }
                    sItem["HSLPD"] = "----";
                    if (jcxm.Contains("、含水率、"))
                        sItem["HSLPD"] = "----";
                    else
                    {
                        sItem["HSLPD"] = "----";
                        sItem["HSL"] = "----";
                    }
                    if (jcxm.Contains("、针片状含量、"))
                    {
                        sItem["ZPZHLPD"] = "";
                        foreach (var mrsDj_item in mrsDj_Where)
                        {
                            if (Conversion.Val(sItem["ZPZHL"]) <= Conversion.Val(mrsDj_item["ZPZHL"]) || Conversion.Val(sItem["ZPZHL"]) == 0)
                            {
                                sItem["ZPZHLPD"] = mrsDj_item["MC"].Trim().Substring(mrsDj_item["MC"].Trim().Length - 2, 2);
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["ZPZHLPD"]))
                        {
                            sItem["ZPZHLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sItem["ZPZHLPD"] = "----";
                        sItem["ZPZHL"] = "----";
                    }
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        switch (sItem["SIMC"])
                        {
                            case "碎石(火成岩)":
                                mrsDj_Where[0]["KYQD"] = "80";
                                break;
                            case "碎石(变质岩)":
                                mrsDj_Where[0]["KYQD"] = "60";
                                break;
                            case "碎石(水成岩)":
                                mrsDj_Where[0]["KYQD"] = "30";
                                break;
                            case "卵石(火成岩)":
                                mrsDj_Where[0]["KYQD"] = "80";
                                break;
                            case "卵石(变质岩)":
                                mrsDj_Where[0]["KYQD"] = "60";
                                break;
                            case "卵石(水成岩)":
                                mrsDj_Where[0]["KYQD"] = "30";
                                break;
                        }
                        //if(MItem[0]["BGBH"] == "201900020")
                        // sItem["KYQDPD = "符合"
                        // Else
                        if (Conversion.Val(sItem["KYQD"]) >= Conversion.Val(mrsDj_Where[0]["KYQD"]))
                            sItem["KYQDPD"] = "符合";
                        else
                        {
                            sItem["KYQDPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sItem["QDMIN"] = "----";
                        sItem["KYQD"] = "----";
                        sItem["KYQDPD"] = "----";
                    }
                    if (jcxm.Contains("、硫化物和硫酸盐含量、"))
                    {
                        sItem["LHWPD"] = "";
                        foreach (var mrsDj_item in mrsDj_Where)
                        {
                            if (Conversion.Val(sItem["LHW"]) <= Conversion.Val(mrsDj_item["LHW"]))
                            {
                                sItem["LHWPD"] = mrsDj_item["MC"].Trim().Substring(mrsDj_item["MC"].Trim().Length - 2, 2);
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sItem["LHWPD"]))
                            sItem["LHWPD"] = "不符合";
                    }
                    else
                    {
                        sItem["LHWPD"] = "----";
                        sItem["LHW"] = "----";
                    }
                    if (jcxm.Contains("、有机物含量、"))
                    {
                        if (sItem["YJWHLPD"] == "不合格" || sItem["YJWHLPD"] == "不符合")
                        {
                            sItem["YJWHLPD"] = "不符合";
                            sItem["YJWHL"] = "";
                            mbhgs = mbhgs + 1;
                        }
                        else
                            sItem["YJWHLPD"] = "符合";
                    }
                    else
                    {
                        sItem["YJWHLPD"] = "----";
                        sItem["YJWHL"] = "----";
                    }
                    if (jcxm.Contains("、碱活性、"))
                    {
                        if (Conversion.Val(sItem["JHX"]) <= Conversion.Val(mrsDj_Where[0]["JHX"]))
                            sItem["JHXPD"] = "符合";
                        else
                        {
                            sItem["JHXPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sItem["JHXPD"] = "----";
                        sItem["JHX"] = "----";
                    }
                    mzslbs = 1;
                    mslbs = 1;
                    mzslbs = mzslbs <= mslbs ? mslbs : mzslbs;
                    mslbs = 1;
                    mslbs = sItem["HNLPD"].Contains("Ⅰ") ? 1 : mslbs;
                    mslbs = sItem["HNLPD"].Contains("Ⅱ") ? 2 : mslbs;
                    mslbs = sItem["HNLPD"].Contains("Ⅲ") ? 3 : mslbs;
                    mzslbs = mzslbs <= mslbs ? mslbs : mzslbs;
                    mslbs = 1;
                    mslbs = sItem["NKHLPD"].Contains("Ⅰ") ? 1 : mslbs;
                    mslbs = sItem["NKHLPD"].Contains("Ⅱ") ? 2 : mslbs;
                    mslbs = sItem["NKHLPD"].Contains("Ⅲ") ? 3 : mslbs;
                    mzslbs = mzslbs <= mslbs ? mslbs : mzslbs;
                    mslbs = 1;
                    mslbs = sItem["YJWHLPD"].Contains("Ⅰ") ? 1 : mslbs;
                    mslbs = sItem["YJWHLPD"].Contains("Ⅱ") ? 2 : mslbs;
                    mslbs = sItem["YJWHLPD"].Contains("Ⅲ") ? 3 : mslbs;
                    mzslbs = mzslbs <= mslbs ? mslbs : mzslbs;
                    mslbs = 1;
                    mslbs = sItem["LHWPD"].Contains("Ⅰ") ? 1 : mslbs;
                    mslbs = sItem["LHWPD"].Contains("Ⅱ") ? 2 : mslbs;
                    mslbs = sItem["LHWPD"].Contains("Ⅲ") ? 3 : mslbs;
                    mzslbs = mzslbs <= mslbs ? mslbs : mzslbs;
                    mslbs = 1;
                    mslbs = sItem["ZPZHLPD"].Contains("Ⅰ") ? 1 : mslbs;
                    mslbs = sItem["ZPZHLPD"].Contains("Ⅱ") ? 2 : mslbs;
                    mslbs = sItem["ZPZHLPD"].Contains("Ⅲ") ? 3 : mslbs;
                    mzslbs = mzslbs <= mslbs ? mslbs : mzslbs;
                    mslbs = 1;
                    mslbs = sItem["JGXPD"].Contains("Ⅰ") ? 1 : mslbs;
                    mslbs = sItem["JGXPD"].Contains("Ⅱ") ? 2 : mslbs;
                    mslbs = sItem["JGXPD"].Contains("Ⅲ") ? 3 : mslbs;
                    mzslbs = mzslbs <= mslbs ? mslbs : mzslbs;
                    mslbs = 1;
                    mslbs = sItem["YSZBPD"].Contains("Ⅰ") ? 1 : mslbs;
                    mslbs = sItem["YSZBPD"].Contains("Ⅱ") ? 2 : mslbs;
                    mslbs = sItem["YSZBPD"].Contains("Ⅲ") ? 3 : mslbs;
                    mzslbs = mzslbs <= mslbs ? mslbs : mzslbs;
                    mslbs = 1;
                    mslbs = sItem["KYQDPD"].Contains("Ⅰ") ? 1 : mslbs;
                    mslbs = sItem["KYQDPD"].Contains("Ⅱ") ? 2 : mslbs;
                    mslbs = sItem["KYQDPD"].Contains("Ⅲ") ? 3 : mslbs;
                    mzslbs = mzslbs <= mslbs ? mslbs : mzslbs;
                    if (sItem["JCXM"] == "抗压强度")
                    {
                        if (sItem["KYQDPD"] == "符合")
                            mslbs = 6;
                        else
                            mzslbs = 4;
                    }

                    sItem["JCJG"] = mbhgs == 0 && mzslbs < 4 && mAllHg ? "合格" : "不合格";
                    mAllHg = mAllHg && sItem["JCJG"].Trim() == "合格";
                    if (mAllHg)
                    {
                        MItem[0]["JCJG"] = "合格";
                        if (mzslbs == 1)
                            MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "Ⅰ类石子标准要求。";
                        if (mzslbs == 2)
                            MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "Ⅱ类石子标准要求。";
                        if (mzslbs == 3)
                            MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "Ⅲ类石子标准要求。";
                        if (mslbs == 6)
                            MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                    }
                    else
                    {
                        MItem[0]["JCJG"] = "不合格";
                        MItem[0]["JCJGMS"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                    }
                    continue;
                }

                #region 含泥量
                mAllHg = mbhggs > 0 ? false : true;
                sItem["HNLPD"] = "";
                sItem["HNL"] = "0";
                if (jcxm.Contains("、含泥量、"))
                {
                    jcxmCur = "含泥量";
                    if (GetSafeDouble(sItem["HNLG1"].Trim()) != 0 && GetSafeDouble(sItem["HNLG1_2"].Trim()) != 0)
                    {
                        double mhnl1 = 0, mhnl2 = 0;
                        mhnl1 = Math.Round((GetSafeDouble(sItem["HNLG1"]) - GetSafeDouble(sItem["HNLG2"])) / GetSafeDouble(sItem["HNLG1"]) * 100, 1);
                        mhnl2 = Math.Round((GetSafeDouble(sItem["HNLG1_2"]) - GetSafeDouble(sItem["HNLG2_2"])) / GetSafeDouble(sItem["HNLG1_2"]) * 100, 1);
                        sItem["HNL"] = Math.Round((mhnl1 + mhnl2) / 2, 1).ToString();
                    }

                    List<IDictionary<string, string>> extraFieldsDj1 = extraDJ.ToList();
                    if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    {
                        if (MItem[0]["JCYJ"].Contains("14685-2011"))
                        {
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"]) && u["JCYJ"].Contains("14685-2011")).ToList();
                        }
                        else
                        {
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                        }
                    }
                    
                    foreach (var eDj in extraFieldsDj1)
                    {
                        sItem["G_HNL"] = "≤" + Math.Round(GetSafeDouble(eDj["HNL"]),1).ToString("0.0");
                        if (GetSafeDouble(sItem["HNL"]) < GetSafeDouble(eDj["HNL"]) || GetSafeDouble(sItem["HNL"]) == 0)
                        {
                            sItem["HNLPD"] = eDj["MC"].Trim().Substring(eDj["MC"].Length - 2, 2);
                            break;
                        }
                    }

                    if (sItem["HNLPD"] == "")
                    {
                        sItem["HNLPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["HNL"] = "----";
                    sItem["HNLPD"] = "----";
                    sItem["G_HNL"] = "----";
                }

                #endregion

                #region 泥块含量
                sItem["NKHL"] = "0";
                if (jcxm.Contains("、泥块含量、"))
                {
                    jcxmCur = "泥块含量";
                    if (GetSafeDouble(sItem["NKHLG1"].Trim()) != 0 && GetSafeDouble(sItem["NKHLG1_2"].Trim()) != 0)
                    {
                        double mnkhl1 = 0, mnkhl2 = 0;
                        mnkhl1 = Math.Round((GetSafeDouble(sItem["NKHLG1"]) - GetSafeDouble(sItem["NKHLG2"])) / GetSafeDouble(sItem["NKHLG1"]) * 100, 1);
                        mnkhl2 = Math.Round((GetSafeDouble(sItem["NKHLG1_2"]) - GetSafeDouble(sItem["NKHLG2_2"])) / GetSafeDouble(sItem["NKHLG1_2"]) * 100, 1);
                        sItem["NKHL"] = Math.Round((mnkhl1 + mnkhl2) / 2, 1).ToString();
                    }
                    sItem["NKHLPD"] = "";

                    List<IDictionary<string, string>> extraFieldsDj1 = extraDJ.ToList();
                    if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    {
                        if (MItem[0]["JCYJ"].Contains("14685-2011"))
                        {
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"]) && u["JCYJ"].Contains("14685-2011")).ToList();
                        }
                        else
                        {
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                        }
                    }
                    
                    foreach (var eDj in extraFieldsDj1)
                    {
                        sItem["G_NKHL"] = "≤" + Math.Round(GetSafeDouble(eDj["NKHL"]), 1).ToString("0.0");
                        if (GetSafeDouble(sItem["NKHL"]) < GetSafeDouble(eDj["NKHL"]) || GetSafeDouble(sItem["NKHL"]) == 0)
                        {
                            sItem["NKHLPD"] = eDj["MC"].Trim().Substring(eDj["MC"].Length - 2, 2);
                            break;
                        }
                    }

                    if (sItem["NKHLPD"] == "")
                    {
                        sItem["NKHLPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["NKHLPD"] = "----";
                    sItem["NKHL"] = "----";
                    sItem["G_NKHL"] = "----";
                }
                #endregion

                #region 堆积密度
                sItem["DJMD"] = "0";
                if (jcxm.Contains("、堆积密度、"))
                {
                    jcxmCur = "堆积密度";
                    if (GetSafeDouble(sItem["DJMDV"].Trim()) != 0)
                    {
                        mdjmd1 = Math.Round((GetSafeDouble(sItem["DJMDG1"]) - GetSafeDouble(sItem["DJMDG2"])) * 100 / GetSafeDouble(sItem["DJMDV"]), 0) * 10;
                        mdjmd2 = Math.Round((GetSafeDouble(sItem["DJMDG1_2"]) - GetSafeDouble(sItem["DJMDG2_2"])) * 100 / GetSafeDouble(sItem["DJMDV"]), 0) * 10;
                        sItem["DJMD"] = (Math.Round((mdjmd1 + mdjmd2) / 20, 0) * 10).ToString();
                    }
                    if (MItem[0]["JCYJ"].Contains("14685-2011"))
                    {
                        sItem["DJMDPD"] = "----";
                    }
                    else
                    {
                        if (GetSafeDouble(sItem["DJMD"]) > g_djmd)
                        {
                            sItem["DJMDPD"] = "符合";
                        }
                        else
                        {
                            sItem["DJMDPD"] = "不符合";
                            mbhggs++;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                else
                {
                    sItem["DJMDPD"] = "----";
                    sItem["DJMD"] = "----";
                }
                #endregion

                #region 紧密密度
                sItem["JMMD"] = "0";
                if (jcxm.Contains("、紧密密度、"))
                {
                    jcxmCur = "紧密密度";
                    if (GetSafeDouble(sItem["JMMDV"].Trim()) != 0)
                    {
                        mdjmd1 = Math.Round((GetSafeDouble(sItem["JMMDG1"]) - GetSafeDouble(sItem["JMMDG2"])) * 100 / GetSafeDouble(sItem["JMMDV"]), 0) * 10;
                        mdjmd2 = Math.Round((GetSafeDouble(sItem["JMMDG1_2"]) - GetSafeDouble(sItem["JMMDG2_2"])) * 100 / GetSafeDouble(sItem["JMMDV"]), 0) * 10;
                        sItem["JMMD"] = (Math.Round((mdjmd1 + mdjmd2) / 20, 0) * 10).ToString();
                    }

                    if (GetSafeDouble(sItem["JMMD"]) > g_jmmd)
                    {
                        sItem["JMMDPD"] = "符合";
                    }
                    else
                    {
                        sItem["JMMDPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["JMMD"] = "----";
                    sItem["JMMDPD"] = "----";
                }
                #endregion

                #region 表观密度
                sItem["BGMD"] = "0";
                if (jcxm.Contains("、表观密度、"))
                {
                    jcxmCur = "表观密度";
                    if (GetSafeDouble(sItem["BGMDG0"]) + GetSafeDouble(sItem["BGMDG2"]) - GetSafeDouble(sItem["BGMDG1"]) != 0 && GetSafeDouble(sItem["BGMDG0_2"]) + GetSafeDouble(sItem["BGMDG2_2"]) - GetSafeDouble(sItem["BGMDG1_2"]) != 0 && GetSafeDouble(sItem["SWXZXS"])!=0)
                    {
                        mbgmd1 = Math.Round((GetSafeDouble(sItem["BGMDG0"]) / (GetSafeDouble(sItem["BGMDG0"]) + GetSafeDouble(sItem["BGMDG2"]) - GetSafeDouble(sItem["BGMDG1"]))-GetSafeDouble(sItem["SWXZXS"])) * 100, 0) * 10;
                        mbgmd2 = Math.Round((GetSafeDouble(sItem["BGMDG0_2"]) / (GetSafeDouble(sItem["BGMDG0_2"]) + GetSafeDouble(sItem["BGMDG2_2"]) - GetSafeDouble(sItem["BGMDG1_2"]))- GetSafeDouble(sItem["SWXZXS"])) * 100, 0) * 10;
                        sItem["BGMD"] = (Math.Round((mbgmd1 + mbgmd2) / 20, 0) * 10).ToString();
                    }
                    sItem["G_BGMD"] = "≥" + g_bgmd.ToString();
                    if (GetSafeDouble(sItem["BGMD"]) > g_bgmd)
                    {
                        sItem["BGMDPD"] = "符合";
                    }
                    else
                    {
                        sItem["BGMDPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    if (Math.Abs(mbgmd1 - mbgmd2) > 20)
                    {
                        sItem["BGMDPD"] = "试验需重做";
                        mbhggs++;
                    }
                }
                else
                {
                    sItem["BGMDPD"] = "----";
                    sItem["BGMD"] = "----";
                    sItem["G_BGMD"] = "----";
                }
                #endregion

                #region 空隙率
                sItem["KXL"] = "0";
                if (jcxm.Contains("、空隙率、"))
                {
                    jcxmCur = "空隙率";
                    sItem["KXLP1"] = mdjmd1.ToString();
                    sItem["KXLP1_2"] = mdjmd2.ToString();
                    sItem["KXLP2"] = mbgmd1.ToString();
                    sItem["KXLP2_2"] = mbgmd2.ToString();
                    if (GetSafeDouble(sItem["KXLP2"]) != 0 && GetSafeDouble(sItem["KXLP2_2"]) != 0)
                    {
                        Double mkxl1 = 0, mkxl2 = 0;
                        mkxl1 = Math.Round(1 - (GetSafeDouble(sItem["KXLP1"]) / GetSafeDouble(sItem["KXLP2"])) * 100, 0);
                        mkxl2 = Math.Round(1 - (GetSafeDouble(sItem["KXLP1_2"]) / GetSafeDouble(sItem["KXLP2_2"])) * 100, 0);
                        sItem["KXL"] = (Math.Round((mkxl1 + mkxl2) / 2, 0)).ToString();
                    }

                    sItem["KXLPD"] = "";

                    List<IDictionary<string, string>> extraFieldsDj1 = extraDJ.ToList();
                    if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    {
                        if (MItem[0]["JCYJ"].Contains("14685-2011"))
                        {
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"]) && u["JCYJ"].Contains("14685-2011")).ToList();
                        }
                        else
                        {
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                        }
                    }
                    foreach (var eDj in extraFieldsDj1)
                    {
                        if (GetSafeDouble(sItem["KXL"]) < GetSafeDouble(eDj["KXL"]) || GetSafeDouble(sItem["KXL"]) == 0)
                        {
                            sItem["KXLPD"] = eDj["MC"].Trim().Substring(eDj["MC"].Length - 2, 2);
                            break;
                        }
                    }

                    if (sItem["KXLPD"] == "")
                    {
                        sItem["KXLPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["KXLPD"] = "----";
                    sItem["KXL"] = "----";
                }
                #endregion

                #region 压碎性指标
                sItem["YSZBPD"] = "";
                sItem["YSZB"] = "0";
                if (jcxm.Contains("、压碎性指标、"))
                {
                    jcxmCur = "压碎性指标";
                    if (GetSafeDouble(sItem["YSZBG1"].Trim()) != 0 && GetSafeDouble(sItem["YSZBG1_2"].Trim()) != 0 && GetSafeDouble(sItem["YSZBG1_3"].Trim()) != 0)
                    {
                        double myszb1 = 0, myszb2 = 0, myszb3 = 0;
                        myszb1 = Math.Round((GetSafeDouble(sItem["YSZBG1"]) - GetSafeDouble(sItem["YSZBG2"])) / GetSafeDouble(sItem["YSZBG1"]) * 100, 1);
                        myszb2 = Math.Round((GetSafeDouble(sItem["YSZBG1_2"]) - GetSafeDouble(sItem["YSZBG2_2"])) / GetSafeDouble(sItem["YSZBG1_2"]) * 100, 1);
                        myszb3 = Math.Round((GetSafeDouble(sItem["YSZBG1_3"]) - GetSafeDouble(sItem["YSZBG2_3"])) / GetSafeDouble(sItem["YSZBG1_3"]) * 100, 1);

                        sItem["YSZB"] = (Math.Round((myszb1 + myszb2 + myszb3) / 3, 0)).ToString();
                    }
                    List<IDictionary<string, string>> extraFieldsDj1 = extraDJ.ToList();
                    if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    {
                        if (MItem[0]["JCYJ"].Contains("14685-2011"))
                        {
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"]) && u["JCYJ"].Contains("14685-2011")).ToList();
                        }
                        else
                        {
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                        }
                    }
                    foreach (var eDj in extraFieldsDj1)
                    {
                        sItem["G_YSZB"] = "<"+ Math.Round(GetSafeDouble(eDj["YSZB"]), 0).ToString("0");
                        if (GetSafeDouble(sItem["YSZB"]) < GetSafeDouble(eDj["YSZB"]) || GetSafeDouble(sItem["YSZB"]) == 0)
                        {
                            sItem["YSZBPD"] = eDj["MC"].Trim().Substring(eDj["MC"].Length - 2, 2);
                            break;
                        }
                    }

                    if (sItem["YSZBPD"] == "")
                    {
                        sItem["YSZBPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["YSZBPD"] = "----";
                    sItem["YSZB"] = "----";
                    sItem["G_YSZB"] = "----";
                }
                #endregion

                #region 吸水率
                sItem["XSLPD"] = "----";
                sItem["XSL"] = "0";
                if (jcxm.Contains("、吸水率、"))
                {
                    if (GetSafeDouble(sItem["XSLG2"]) != 0 && GetSafeDouble(sItem["XSLG2_2"]) != 0)
                    {
                        double mxsl1 = 0, mxsl2 = 0;
                        mxsl1 = Math.Round((GetSafeDouble(sItem["XSLG1"]) - GetSafeDouble(sItem["XSLG2"])) / GetSafeDouble(sItem["XSLG2"]) * 100, 1);
                        mxsl2 = Math.Round((GetSafeDouble(sItem["XSLG1_2"]) - GetSafeDouble(sItem["XSLG2_2"])) / GetSafeDouble(sItem["XSLG2_2"]) * 100, 1);
                        sItem["XSL"] = (Math.Round((mxsl1 + mxsl2) / 2, 1)).ToString();
                    }
                }
                else
                {
                    sItem["XSLPD"] = "----";
                }
                #endregion

                #region 含水率
                sItem["HSLPD"] = "----";
                sItem["HSL"] = "0";
                if (jcxm.Contains("、含水率、"))
                {
                    if (GetSafeDouble(sItem["HSLG2"]) != 0 && GetSafeDouble(sItem["HSLG2_2"]) != 0)
                    {
                        double mhsl1 = 0, mhsl2 = 0;
                        mhsl1 = Math.Round((GetSafeDouble(sItem["HSLG1"]) - GetSafeDouble(sItem["HSLG2"])) / GetSafeDouble(sItem["HSLG2"]) * 100, 1);
                        mhsl2 = Math.Round((GetSafeDouble(sItem["HSLG1_2"]) - GetSafeDouble(sItem["HSLG2_2"])) / GetSafeDouble(sItem["HSLG2_2"]) * 100, 1);
                        sItem["HSL"] = (Math.Round((mhsl1 + mhsl2) / 2, 1)).ToString();
                    }
                }
                else
                {
                    sItem["HSLPD"] = "----";
                    sItem["HSL"] = "----";
                }
                #endregion

                #region 针片状含量
                sItem["ZPZHLPD"] = "";
                sItem["ZPZHL"] = "0";
                if (jcxm.Contains("、针片状含量、"))
                {
                    jcxmCur = "针片状含量";
                    if (GetSafeDouble(sItem["ZPZHLG1"]) != 0)
                    {
                        sItem["ZPZHL"] = (Math.Round(GetSafeDouble(sItem["ZPZHLG2"]) / GetSafeDouble(sItem["ZPZHLG1"]) * 100, 0)).ToString();
                    }

                    List<IDictionary<string, string>> extraFieldsDj1 = extraDJ.ToList();
                    if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    {
                        if (MItem[0]["JCYJ"].Contains("14685-2011"))
                        {
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"]) && u["JCYJ"].Contains("14685-2011")).ToList();
                        }
                        else
                        {
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                        }
                    }
                    foreach (var eDj in extraFieldsDj1)
                    {
                        sItem["G_ZPZHL"] = "<"+ Math.Round(GetSafeDouble(eDj["ZPZHL"]), 1).ToString("0.0");
                        if (GetSafeDouble(sItem["ZPZHL"]) < GetSafeDouble(eDj["ZPZHL"]) || GetSafeDouble(sItem["ZPZHL"]) == 0)
                        {
                            sItem["ZPZHLPD"] = eDj["MC"].Trim().Substring(eDj["MC"].Length - 2, 2);
                            break;
                        }
                    }

                    if (sItem["ZPZHLPD"] == "")
                    {
                        sItem["ZPZHLPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["ZPZHLPD"] = "----";
                    sItem["ZPZHL"] = "----";
                    sItem["G_ZPZHL"] = "----";
                }


                #endregion

                #region 抗压强度
                sItem["KYQDPD"] = "";
                sItem["KYQD"] = "0";
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                    if (GetSafeDouble(sItem["KYQDA1"]) != 0 && GetSafeDouble(sItem["KYQDA2"]) != 0 && GetSafeDouble(sItem["KYQDA3"]) != 0 && GetSafeDouble(sItem["KYQDA4"]) != 0 && GetSafeDouble(sItem["KYQDA5"]) != 0 && GetSafeDouble(sItem["KYQDA6"]) != 0)
                    {
                        List<double> mtmpArray = new List<double>();
                        double mkyqd1 = 0, mkyqd2 = 0, mkyqd3 = 0, mkyqd4 = 0, mkyqd5 = 0, mkyqd6 = 0;
                        mkyqd1 = Math.Round(GetSafeDouble(sItem["KYQDF1"]) / GetSafeDouble(sItem["KYQDA1"]), 2);
                        mtmpArray.Add(mkyqd1);
                        mkyqd2 = Math.Round(GetSafeDouble(sItem["KYQDF2"]) / GetSafeDouble(sItem["KYQDA2"]), 2);
                        mtmpArray.Add(mkyqd2);
                        mkyqd3 = Math.Round(GetSafeDouble(sItem["KYQDF3"]) / GetSafeDouble(sItem["KYQDA3"]), 2);
                        mtmpArray.Add(mkyqd3);
                        mkyqd4 = Math.Round(GetSafeDouble(sItem["KYQDF4"]) / GetSafeDouble(sItem["KYQDA4"]), 2);
                        mtmpArray.Add(mkyqd4);
                        mkyqd5 = Math.Round(GetSafeDouble(sItem["KYQDF5"]) / GetSafeDouble(sItem["KYQDA5"]), 2);
                        mtmpArray.Add(mkyqd5);
                        mkyqd6 = Math.Round(GetSafeDouble(sItem["KYQDF6"]) / GetSafeDouble(sItem["KYQDA6"]), 2);
                        mtmpArray.Add(mkyqd6);

                        sItem["KYQD"] = mtmpArray.Average().ToString();
                        mtmpArray.Sort();
                        sItem["QDMIN"] = mtmpArray[0].ToString();
                    }
                    if (GetSafeDouble(sItem["KYQD"]) >= g_kyqd)
                    {
                        sItem["KYQDPD"] = "符合";
                    }
                    else
                    {
                        sItem["KYQDPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["QDMIN"] = "----";
                    sItem["KYQD"] = "----";
                    sItem["KYQDPD"] = "----";
                }
                #endregion

                #region 硫化物和硫酸盐含量
                sItem["LHWPD"] = "";
                sItem["LHW"] = "0";
                if (jcxm.Contains("、硫化物和硫酸盐含量、"))
                {
                    jcxmCur = "硫化物和硫酸盐含量";
                    double mlhw1 = 0, mlhw2 = 0;
                    if (GetSafeDouble(sItem["LHWG1"]) != 0 && GetSafeDouble(sItem["LHWG1_2"]) != 0)
                    {
                        mlhw1 = Math.Round((GetSafeDouble(sItem["LHWG1"]) - GetSafeDouble(sItem["LHWG2"])) / GetSafeDouble(sItem["LHWG1"]) * 100, 1);
                        mlhw2 = Math.Round((GetSafeDouble(sItem["LHWG1_2"]) - GetSafeDouble(sItem["LHWG2_2"])) / GetSafeDouble(sItem["LHWG1_2"]) * 100, 1);
                        sItem["LHW"] = (Math.Round((mlhw1 + mlhw2) / 2, 1)).ToString();
                    }
                    List<IDictionary<string, string>> extraFieldsDj1 = extraDJ.ToList();
                    if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    {
                        if (MItem[0]["JCYJ"].Contains("14685-2011"))
                        {
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"]) && u["JCYJ"].Contains("14685-2011")).ToList();
                        }
                        else
                        {
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                        }
                    }
                    foreach (var eDj in extraFieldsDj1)
                    {
                        if (GetSafeDouble(sItem["LHW"]) < GetSafeDouble(eDj["LHW"]) || GetSafeDouble(sItem["LHW"]) == 0)
                        {
                            sItem["LHWPD"] = eDj["MC"].Trim().Substring(eDj["MC"].Length - 2, 2);
                            break;
                        }
                    }

                    if (sItem["LHWPD"] == "")
                    {
                        sItem["LHWPD"] = "不符合";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    if (Math.Abs(mlhw1 - mlhw2) > 0.2)
                    {
                        sItem["LHWPD"] = "试验需重做";
                        mbhggs++;
                    }
                }
                else
                {
                    sItem["LHWPD"] = "----";
                    sItem["LHW"] = "----";
                }
                #endregion

                #region 有机物含量
                if (jcxm.Contains("、有机物含量、"))
                {
                    jcxmCur = "有机物含量";
                    if (sItem["YJWHLPD"] == "不合格" || sItem["YJWHLPD"] == "不符合")
                    {
                        sItem["YJWHLPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        sItem["YJWHLPD"] = "浅于标准溶液";
                    }
                }
                else
                {
                    sItem["YJWHLPD"] = "----";
                }
                #endregion

                #region 碱活性
                if (jcxm.Contains("、碱活性、"))
                {
                    jcxmCur = "碱活性";
                    if (GetSafeDouble(sItem["JHX"]) <= g_jhx)
                    {
                        sItem["JHXPD"] = "符合";
                    }
                    else
                    {
                        sItem["JHXPD"] = "不符合";
                        mbhggs++;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["JHXPD"] = "----";
                    sItem["JHX"] = "----";
                }
                #endregion

                mzslbs = 1;
                mslbs = 1;
                if (sItem["HNLPD"] != "----")
                {
                    if (sItem["HNLPD"].Contains("Ⅰ"))
                    {
                        mslbs = 1;
                    }
                    else if (sItem["HNLPD"].Contains("Ⅱ"))
                    {
                        mslbs = 2;
                    }
                    else if (sItem["HNLPD"].Contains("Ⅲ"))
                    {
                        mslbs = 3;
                    }
                    else
                    {
                        mslbs = 4;
                    }
                }

                if (mzslbs <= mslbs)
                {
                    mzslbs = mslbs;
                }

                mslbs = 1;
                if (sItem["NKHLPD"] != "----")
                {
                    if (sItem["NKHLPD"].Contains("Ⅰ"))
                    {
                        mslbs = 1;
                    }
                    else if (sItem["NKHLPD"].Contains("Ⅱ"))
                    {
                        mslbs = 2;
                    }
                    else if (sItem["NKHLPD"].Contains("Ⅲ"))
                    {
                        mslbs = 3;
                    }
                    else
                    {
                        mslbs = 4;
                    }
                }
                if (mzslbs <= mslbs)
                {
                    mzslbs = mslbs;
                }

                mslbs = 1;
                if (sItem["YJWHLPD"] != "----")
                {
                    if (sItem["YJWHLPD"].Contains("Ⅰ"))
                    {
                        mslbs = 1;
                    }
                    else if (sItem["YJWHLPD"].Contains("Ⅱ"))
                    {
                        mslbs = 2;
                    }
                    else if (sItem["YJWHLPD"].Contains("Ⅲ"))
                    {
                        mslbs = 3;
                    }
                    else
                    {
                        mslbs = 4;
                    }
                }

                if (mzslbs <= mslbs)
                {
                    mzslbs = mslbs;
                }

                mslbs = 1;
                if (sItem["LHWPD"] != "----")
                {
                    if (sItem["LHWPD"].Contains("Ⅰ"))
                    {
                        mslbs = 1;
                    }
                    else if (sItem["LHWPD"].Contains("Ⅱ"))
                    {
                        mslbs = 2;
                    }
                    else if (sItem["LHWPD"].Contains("Ⅲ"))
                    {
                        mslbs = 3;
                    }
                    else
                    {
                        mslbs = 4;
                    }
                }
                if (mzslbs <= mslbs)
                {
                    mzslbs = mslbs;
                }

                mslbs = 1;
                if (sItem["ZPZHLPD"] != "----")
                {
                    if (sItem["ZPZHLPD"].Contains("Ⅰ"))
                    {
                        mslbs = 1;
                    }
                    else if (sItem["ZPZHLPD"].Contains("Ⅱ"))
                    {
                        mslbs = 2;
                    }
                    else if (sItem["ZPZHLPD"].Contains("Ⅲ"))
                    {
                        mslbs = 3;
                    }
                    else
                    {
                        mslbs = 4;
                    }
                }

                if (mzslbs <= mslbs)
                {
                    mzslbs = mslbs;
                }

                mslbs = 1;
                if (sItem["JGXPD"] != "----")
                {
                    if (sItem["JGXPD"].Contains("Ⅰ"))
                    {
                        mslbs = 1;
                    }
                    else if (sItem["JGXPD"].Contains("Ⅱ"))
                    {
                        mslbs = 2;
                    }
                    else if (sItem["JGXPD"].Contains("Ⅲ"))
                    {
                        mslbs = 3;
                    }
                    else
                    {
                        mslbs = 4;
                    }
                }
                if (mzslbs <= mslbs)
                {
                    mzslbs = mslbs;
                }

                mslbs = 1;
                if (sItem["YSZBPD"] != "----")
                {
                    if (sItem["YSZBPD"].Contains("Ⅰ"))
                    {
                        mslbs = 1;
                    }
                    else if (sItem["YSZBPD"].Contains("Ⅱ"))
                    {
                        mslbs = 2;
                    }
                    else if (sItem["YSZBPD"].Contains("Ⅲ"))
                    {
                        mslbs = 3;
                    }
                    else
                    {
                        mslbs = 4;
                    }
                }
                if (mzslbs <= mslbs)
                {
                    mzslbs = mslbs;
                }

                mslbs = 1;
                if (sItem["KYQDPD"] != "----")
                {
                    if (sItem["KYQDPD"].Contains("Ⅰ"))
                    {
                        mslbs = 1;
                    }
                    else if (sItem["KYQDPD"].Contains("Ⅱ"))
                    {
                        mslbs = 2;
                    }
                    else if (sItem["KYQDPD"].Contains("Ⅲ"))
                    {
                        mslbs = 3;
                    }
                    else
                    {
                        mslbs = 4;
                    }
                }

                if (jcxm.Contains("、抗压强度、"))
                {
                    if (sItem["KYQDPD"] == "符合")
                    {
                        mslbs = 6;
                    }
                    else
                    {
                        mslbs = 4;
                    }
                }
                if (mzslbs <= mslbs)
                {
                    mzslbs = mslbs;
                }

                if (mbhggs == 0 && mzslbs < 4)
                {
                    mAllHg = true;
                }
                else
                {
                    mAllHg = false;
                }

                if (mAllHg)
                {
                    mjcjg = "合格";
                    //if (mzslbs == 1)
                    //{
                    //    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合Ⅰ类石子标准要求。";
                    //}
                    //if (mzslbs == 2)
                    //{
                    //    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合Ⅱ类石子标准要求。";
                    //}
                    //if (mzslbs == 3)
                    //{
                    //    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合Ⅲ类石子标准要求。";
                    //}
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合"+ sItem["SJDJ"] + "标准要求。";
                }
                else
                {
                    mjcjg = "不合格";
                    //jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "砼用石子要求。";
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + sItem["SJDJ"] + "标准要求。";
                }
                #endregion
                #region 添加最终报告
                if (mAllHg && mjcjg != "----")
                {
                    mjcjg = "合格";
                }
                MItem[0]["JCJG"] = mjcjg;
                MItem[0]["JCJGMS"] = jsbeizhu;
                #endregion
            }



            /************************ 代码结束 *********************/
            #endregion
        }
    }
}

