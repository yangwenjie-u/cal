using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class XSJ : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "", mSjdj = "";
            var extraDJ = dataExtra["BZ_XSJ_DJ"];
            var bzHSBData = dataExtra["BZ_CGLHSB"];
            int mbhggs = 0;//不合格数量
            double g_hnl = 0, g_nkhl = 0, g_bgmd = 0, g_djmd = 0, g_kxl = 0, g_zpzhl = 0, g_jmmd = 0, g_kyqd = 0, g_jhx = 0;
            double mSz = 0, mQdyq = 0;
            string mJSFF = "";
            double mdjmd1 = 0, mdjmd2 = 0;
            double mbgmd1 = 0, mbgmd2 = 0;
            int mzslbs, mslbs;


            var data = retData;

            var SItem = data["S_XSJ"];
            var MItem = data["M_XSJ"];
            if (!data.ContainsKey("M_XSJ"))
            {
                data["M_XSJ"] = new List<IDictionary<string, string>>();
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
                IDictionary<string, string> extraFieldsDj = null;
                //从设计等级表中取得相应的计算数值、等级标准
                extraFieldsDj = extraDJ.FirstOrDefault(x => x["MC"] == sItem["LX"].Trim());
                if (extraFieldsDj != null)
                {
                    g_hnl = GetSafeDouble(extraFieldsDj["G_HNL"]);
                    g_nkhl = GetSafeDouble(extraFieldsDj["G_NKHL"]);
                    g_zpzhl = GetSafeDouble(extraFieldsDj["G_ZPZHL"]);
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

                #region 颗粒级配
                if (jcxm.Contains("、颗粒级配、"))
                {
                    jcxmCur = "颗粒级配";
                    double[] narr = new double[9];
                    for (int i = 0; i < 9; i++)
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
                    //narr[8] = string.IsNullOrEmpty(sItem["SY53_0"]) ? 0 : GetSafeDouble(sItem["SY53_0"]);
                    //narr[9] = string.IsNullOrEmpty(sItem["SY63_0"]) ? 0 : GetSafeDouble(sItem["SY63_0"]);
                    //narr[10] = string.IsNullOrEmpty(sItem["SY75_0"]) ? 0 : GetSafeDouble(sItem["SY75_0"]);
                    //narr[11] = string.IsNullOrEmpty(sItem["SY90"]) ? 0 : GetSafeDouble(sItem["SY90"]);
                    narr[8] = string.IsNullOrEmpty(sItem["SY100"]) ? 0 : GetSafeDouble(sItem["SY100"]);
                    double sum = 0;
                    sum = string.IsNullOrEmpty(MItem[0]["SIZL"]) ? 0 : GetSafeDouble(MItem[0]["SIZL"]);
                    if (sum == 0)
                    {
                        sum = narr.Sum();
                    }

                    double md = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        md = 100 * narr[i] / sum;
                        narr[i] = Math.Round(md, 1);
                    }
                    sum = 0;

                    //md = Math.Round(narr[11], 1);
                    //sum = sum + md;
                    //sItem["CY90"] = md > 100 ? "100" : sum.ToString("0");

                    //md = Math.Round(narr[10], 1);
                    //sum = sum + md;
                    //sItem["CY75_0"] = md > 100 ? "100" : sum.ToString("0");

                    //md = Math.Round(narr[9], 1);
                    //sum = sum + md;
                    //sItem["CY63_0"] = md > 100 ? "100" : sum.ToString("0");

                    //md = Math.Round(narr[8], 1);
                    //sum = sum + md;
                    //sItem["CY53_0"] = md > 100 ? "100" : sum.ToString("0");

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

                    md = Math.Round(narr[8], 1);
                    sum = sum + md;
                    sItem["CY100"] = md > 100 ? "100" : sum.ToString("0");

                    //var bzHSB = bzHSBData.FirstOrDefault(u => u.Keys.Contains("GCCC") && u.Values.Contains(sItem["GCCC"].Trim()));
                    IDictionary<string, string> bzHSB = null;
                    //if (MItem[0]["JCYJ"] == "14685-2011")
                    //{
                        bzHSB = bzHSBData.FirstOrDefault(u => u["GCCC"].Contains(sItem["GCCC"].Trim()) && u["BZDH"].Contains("14685-2011"));
                    //}
                    //else
                    //{
                    //    bzHSB = bzHSBData.FirstOrDefault(u => u["GCCC"].Contains(sItem["GCCC"].Trim()) && u["BZDH"].Contains("14685-2001"));
                    //}
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
                        //flag = GetSafeDouble(sItem["CY53_0"]) >= GetSafeDouble(bzHSB["CY53_0A"]) && GetSafeDouble(sItem["CY53_0"]) <= GetSafeDouble(bzHSB["CY53_0B"]) ? flag : false;
                        //flag = GetSafeDouble(sItem["CY63_0"]) >= GetSafeDouble(bzHSB["CY63_0A"]) && GetSafeDouble(sItem["CY63_0"]) <= GetSafeDouble(bzHSB["CY63_0B"]) ? flag : false;
                        //flag = GetSafeDouble(sItem["CY75_0"]) >= GetSafeDouble(bzHSB["CY75_0A"]) && GetSafeDouble(sItem["CY75_0"]) <= GetSafeDouble(bzHSB["CY75_0B"]) ? flag : false;
                        //flag = GetSafeDouble(sItem["CY90"]) >= GetSafeDouble(bzHSB["CY90A"]) && GetSafeDouble(sItem["CY4_75"]) <= GetSafeDouble(bzHSB["CY90B"]) ? flag : false;

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

                        //if (GetSafeDouble(bzHSB["CY53_0A"]) == 0 && GetSafeDouble(bzHSB["CY53_0B"]) == 100)
                        //{
                        //    sItem["CY53_0"] = "-";
                        //}

                        //if (GetSafeDouble(bzHSB["CY63_0A"]) == 0 && GetSafeDouble(bzHSB["CY63_0B"]) == 100)
                        //{
                        //    sItem["CY63_0"] = "-";
                        //}

                        //if (GetSafeDouble(bzHSB["CY75_0A"]) == 0 && GetSafeDouble(bzHSB["CY75_0B"]) == 100)
                        //{
                        //    sItem["CY75_0"] = "-";
                        //}

                        //if (GetSafeDouble(bzHSB["CY90A"]) == 0 && GetSafeDouble(bzHSB["CY90B"]) == 100)
                        //{
                        //    sItem["CY90"] = "-";
                        //}
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
                    //sItem["CY53_0"] = "-";
                    //sItem["CY63_0"] = "-";
                    //sItem["CY75_0"] = "-";
                    //sItem["CY90"] = "-";
                    sItem["CY100"] = "-";
                }
                #endregion

                #region 微粉含量
                mAllHg = mbhggs > 0 ? false : true;
                sItem["HNLPD"] = "";
                sItem["HNL"] = "0";
                if (jcxm.Contains("、微粉含量、"))
                {
                    jcxmCur = "微粉含量";
                    if (GetSafeDouble(sItem["HNLG1"].Trim()) != 0 && GetSafeDouble(sItem["HNLG1_2"].Trim()) != 0)
                    {
                        double mhnl1 = 0, mhnl2 = 0;
                        mhnl1 = Math.Round((GetSafeDouble(sItem["HNLG1"]) - GetSafeDouble(sItem["HNLG2"])) / GetSafeDouble(sItem["HNLG1"]) * 100, 1);
                        mhnl2 = Math.Round((GetSafeDouble(sItem["HNLG1_2"]) - GetSafeDouble(sItem["HNLG2_2"])) / GetSafeDouble(sItem["HNLG1_2"]) * 100, 1);
                        sItem["HNL"] = Math.Round((mhnl1 + mhnl2) / 2, 1).ToString();
                    }

                    List<IDictionary<string, string>> extraFieldsDj1 = extraDJ.ToList();
                    //if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    //{
                        //if (MItem[0]["JCYJ"].Contains("14685-2011"))
                        //{
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"]==sItem["LX"]).ToList();
                        //}
                        //else
                        //{
                        //    extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                        //}
                    //}

                    foreach (var eDj in extraFieldsDj1)
                    {
                        //sItem["G_HNL"] = "≤" + Math.Round(GetSafeDouble(eDj["G_HNL"]), 1).ToString("0.0");
                        //if (GetSafeDouble(sItem["HNL"]) < GetSafeDouble(eDj["G_HNL"]) || GetSafeDouble(sItem["HNL"]) == 0)
                        sItem["G_HNL"] = eDj["G_HNL"];
                        if (IsQualified(sItem["G_HNL"], sItem["HNL"],false)=="合格" || GetSafeDouble(sItem["HNL"]) == 0)
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
                    //if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    //{
                        //if (MItem[0]["JCYJ"].Contains("14685-2011"))
                        //{
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"]==sItem["LX"]).ToList();
                        //}
                        //else
                        //{
                        //    extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                        //}
                    //}

                    foreach (var eDj in extraFieldsDj1)
                    {
                        //sItem["G_NKHL"] = "≤" + Math.Round(GetSafeDouble(eDj["G_NKHL"]), 1).ToString("0.0");
                        //if (GetSafeDouble(sItem["NKHL"]) < GetSafeDouble(eDj["G_NKHL"]) || GetSafeDouble(sItem["NKHL"]) == 0
                        sItem["G_NKHL"] = eDj["G_NKHL"];
                        if (IsQualified(sItem["G_NKHL"], sItem["NKHL"],false) == "合格" || GetSafeDouble(sItem["NKHL"]) == 0)
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

                #region 针片状颗粒含量
                sItem["ZPZHLPD"] = "";
                sItem["ZPZHL"] = "0";
                if (jcxm.Contains("、针片状颗粒含量、"))
                {
                    jcxmCur = "针片状颗粒含量";
                    if (GetSafeDouble(sItem["ZPZHLG1"]) != 0)
                    {
                        sItem["ZPZHL"] = (Math.Round(GetSafeDouble(sItem["ZPZHLG2"]) / GetSafeDouble(sItem["ZPZHLG1"]) * 100, 0)).ToString();
                    }

                    List<IDictionary<string, string>> extraFieldsDj1 = extraDJ.ToList();
                    //if (sItem["SJDJ"].Trim().ToString().Length > 2)
                    //{
                    //    if (MItem[0]["JCYJ"].Contains("14685-2011"))
                    //    {
                            //extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2011")).ToList();
                            extraFieldsDj1 = extraDJ.Where(u => u["MC"]==sItem["LX"]).ToList();
                    //    }
                    //    else
                    //    {
                    //        extraFieldsDj1 = extraDJ.Where(u => u["MC"].Contains(sItem["SJDJ"].Trim().Substring(0, sItem["SJDJ"].Trim().ToString().Length - 2)) && u["JCYJ"].Contains("14685-2001")).ToList();
                    //    }
                    //}
                    foreach (var eDj in extraFieldsDj1)
                    {
                        //sItem["G_ZPZHL"] = "<" + Math.Round(GetSafeDouble(eDj["G_ZPZHL"]), 1).ToString("0.0");
                        //if (GetSafeDouble(sItem["ZPZHL"]) < GetSafeDouble(eDj["G_ZPZHL"]) || GetSafeDouble(sItem["ZPZHL"]) == 0)
                        sItem["G_ZPZHL"] = eDj["G_ZPZHL"];
                        if (IsQualified(sItem["G_ZPZHL"], sItem["ZPZHL"],false) == "合格" || GetSafeDouble(sItem["ZPZHL"]) == 0)
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

                //mslbs = 1;
                //if (sItem["YJWHLPD"] != "----")
                //{
                //    if (sItem["YJWHLPD"].Contains("Ⅰ"))
                //    {
                //        mslbs = 1;
                //    }
                //    else if (sItem["YJWHLPD"].Contains("Ⅱ"))
                //    {
                //        mslbs = 2;
                //    }
                //    else if (sItem["YJWHLPD"].Contains("Ⅲ"))
                //    {
                //        mslbs = 3;
                //    }
                //    else
                //    {
                //        mslbs = 4;
                //    }
                //}

                //if (mzslbs <= mslbs)
                //{
                //    mzslbs = mslbs;
                //}

                //mslbs = 1;
                //if (sItem["LHWPD"] != "----")
                //{
                //    if (sItem["LHWPD"].Contains("Ⅰ"))
                //    {
                //        mslbs = 1;
                //    }
                //    else if (sItem["LHWPD"].Contains("Ⅱ"))
                //    {
                //        mslbs = 2;
                //    }
                //    else if (sItem["LHWPD"].Contains("Ⅲ"))
                //    {
                //        mslbs = 3;
                //    }
                //    else
                //    {
                //        mslbs = 4;
                //    }
                //}
                //if (mzslbs <= mslbs)
                //{
                //    mzslbs = mslbs;
                //}

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

                //mslbs = 1;
                //if (sItem["JGXPD"] != "----")
                //{
                //    if (sItem["JGXPD"].Contains("Ⅰ"))
                //    {
                //        mslbs = 1;
                //    }
                //    else if (sItem["JGXPD"].Contains("Ⅱ"))
                //    {
                //        mslbs = 2;
                //    }
                //    else if (sItem["JGXPD"].Contains("Ⅲ"))
                //    {
                //        mslbs = 3;
                //    }
                //    else
                //    {
                //        mslbs = 4;
                //    }
                //}
                //if (mzslbs <= mslbs)
                //{
                //    mzslbs = mslbs;
                //}

                //mslbs = 1;
                //if (sItem["YSZBPD"] != "----")
                //{
                //    if (sItem["YSZBPD"].Contains("Ⅰ"))
                //    {
                //        mslbs = 1;
                //    }
                //    else if (sItem["YSZBPD"].Contains("Ⅱ"))
                //    {
                //        mslbs = 2;
                //    }
                //    else if (sItem["YSZBPD"].Contains("Ⅲ"))
                //    {
                //        mslbs = 3;
                //    }
                //    else
                //    {
                //        mslbs = 4;
                //    }
                //}
                //if (mzslbs <= mslbs)
                //{
                //    mzslbs = mslbs;
                //}

                //mslbs = 1;
                //if (sItem["KYQDPD"] != "----")
                //{
                //    if (sItem["KYQDPD"].Contains("Ⅰ"))
                //    {
                //        mslbs = 1;
                //    }
                //    else if (sItem["KYQDPD"].Contains("Ⅱ"))
                //    {
                //        mslbs = 2;
                //    }
                //    else if (sItem["KYQDPD"].Contains("Ⅲ"))
                //    {
                //        mslbs = 3;
                //    }
                //    else
                //    {
                //        mslbs = 4;
                //    }
                //}

                //if (jcxm.Contains("、抗压强度、"))
                //{
                //    if (sItem["KYQDPD"] == "符合")
                //    {
                //        mslbs = 6;
                //    }
                //    else
                //    {
                //        mslbs = 4;
                //    }
                //}
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
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合" + sItem["LX"] + "标准要求。";
                }
                else
                {
                    mjcjg = "不合格";
                    //jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "砼用石子要求。";
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + sItem["LX"] + "标准要求。";
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
