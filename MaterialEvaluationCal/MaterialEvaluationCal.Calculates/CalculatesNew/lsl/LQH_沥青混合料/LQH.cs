using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class LQH : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_LQH"];
            var MItem = data["M_LQH"];
            if (!data.ContainsKey("M_LQH"))
            {
                data["M_LQH"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            string mJSFF;
            double[] nArr;
            bool sign, mFlag_Bhg = false, mFlag_Hg = false;
            string BHGXM = "", Hgxm = "";

            foreach (var sItem in SItem)
            {
                double md1, md2, md3, xd1, xd2, xd3, md, pjmd, sum; int gs;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                #region 油石比
                sign = true;
                if (jcxm.Contains("、油石比、"))
                {
                    gs = 3;
                    for (int i = 1; i <= 3; i++)
                    {
                        if (!IsNumeric(sItem["L_HHL" + i]) && !IsNumeric(sItem["L_LZL" + i]) && !IsNumeric(sItem["L_RQZL" + i])
                            && !IsNumeric(sItem["L_RJG" + i]) && !IsNumeric(sItem["L_LJK" + i]) && !IsNumeric(sItem["L_GGZL" + i])
                             && !IsNumeric(sItem["L_CTL" + i]) && !IsNumeric(sItem["L_ZSCT" + i]) && !IsNumeric(sItem["L_CZZL" + i]))
                        {
                            gs = gs - 1;
                        }
                    }
                    sum = 0;
                    nArr = new double[8];
                    for (int i = 1; i <= gs; i++)
                    {
                        md1 = Conversion.Val(sItem["L_RJG" + i].Trim());
                        md2 = Conversion.Val(sItem["L_RQZL" + i].Trim());
                        nArr[1] = (md1 - md2); //容器中留下集料干燥质量;
                        md1 = Conversion.Val(sItem["L_LJK" + i].Trim());
                        md2 = Conversion.Val(sItem["L_LZL" + i].Trim());
                        nArr[2] = (md1 - md2); //滤纸在试验前后增质量
                        md1 = Conversion.Val(sItem["L_CZZL" + i].Trim());
                        md2 = Conversion.Val(sItem["L_GGZL" + i].Trim());
                        md1 = md1 - md2;
                        md2 = Conversion.Val(sItem["L_CTL" + i].Trim());
                        md3 = Conversion.Val(sItem["L_ZSCT" + i].Trim());
                        nArr[3] = (md1 * md2 / md3); //泄露入抽提液中矿粉质量
                        nArr[4] = (nArr[1] + nArr[2] + nArr[3]);//沥青混合料中矿料总质量
                        md1 = GetSafeDouble(sItem["L_HHL" + i].Trim());
                        nArr[5] = md1 - nArr[4]; //沥青混合料中沥青质量
                        nArr[6] = 100 * nArr[5] / md1; //沥青含量
                        nArr[7] = 100 * nArr[5] / nArr[3];  //油石比
                        sum = sum + nArr[7];
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    //pjmd = Math.Round(pjmd, 2);
                    mItem["W_YSB"] = pjmd.ToString("0.00");  //油石比
                    mItem["GH_YSB"] = IsQualified(mItem["G_YSB"], mItem["W_YSB"], true);
                }
                else
                {
                    mItem["W_YSB"] = "----";
                    mItem["GH_YSB"] = "----";
                    mItem["G_YSB"] = "----";
                }
                #endregion

                #region 沥青含量 || 沥青用量
                if (jcxm.Contains("、沥青含量、") || jcxm.Contains("沥青用量"))
                {
                    gs = 3;
                    for (int i = 1; i <= 3; i++)
                    {
                        if (!IsNumeric(sItem["L_HHL" + i]) && !IsNumeric(sItem["L_LZL" + i]) && !IsNumeric(sItem["L_RQZL" + i])
                            && !IsNumeric(sItem["L_RJG" + i]) && !IsNumeric(sItem["L_LJK" + i]) && !IsNumeric(sItem["L_GGZL" + i])
                             && !IsNumeric(sItem["L_CTL" + i]) && !IsNumeric(sItem["L_ZSCT" + i]) && !IsNumeric(sItem["L_CZZL" + i]))
                        {
                            gs = gs - 1;
                        }
                    }
                    sum = 0;
                    //List<double> nArr = new List<double>();
                    //double[] nArr = new double[7];
                    //nArr.Add(0);
                    nArr = new double[7];
                    for (int i = 1; i <= gs; i++)
                    {
                        md1 = Conversion.Val(sItem["L_RJG" + i].Trim());
                        md2 = Conversion.Val(sItem["L_RQZL" + i].Trim());
                        nArr[1] = (md1 - md2); //容器中留下集料干燥质量;
                        md1 = Conversion.Val(sItem["L_LJK" + i].Trim());
                        md2 = Conversion.Val(sItem["L_LZL" + i].Trim());
                        nArr[2] = (md1 - md2); //滤纸在试验前后增质量
                        md1 = Conversion.Val(sItem["L_CZZL" + i].Trim());
                        md2 = Conversion.Val(sItem["L_GGZL" + i].Trim());
                        md1 = md1 - md2;
                        md2 = Conversion.Val(sItem["L_CTL" + i].Trim());
                        md3 = Conversion.Val(sItem["L_ZSCT" + i].Trim());
                        nArr[3] = (md1 * md2 / md3); //泄露入抽提液中矿粉质量
                        nArr[4] = (nArr[1] + nArr[2] + nArr[3]);//沥青混合料中矿料总质量
                        md1 = GetSafeDouble(sItem["L_HHL" + i].Trim());
                        nArr[5] = md1 - nArr[4]; //沥青混合料中沥青质量
                        nArr[6] = 100 * nArr[5] / md1; //沥青含量
                        nArr[6] = Math.Round(nArr[6], 2);
                        sum = sum + nArr[6];
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    //pjmd = Math.Round(pjmd, 2);
                    mItem["W_LQHL"] = pjmd.ToString("0.00");
                    mItem["GH_LQHL"] = IsQualified(mItem["G_LQHL"], mItem["W_LQHL"], true);
                }
                else
                {
                    mItem["W_LQHL"] = "----";
                    mItem["GH_LQHL"] = "----";
                    mItem["G_LQHL"] = "----";
                }
                #endregion

                #region 矿料级配
                sign = true;
                if (jcxm.Contains("、矿料级配、"))
                {

                    if (sItem["HG_SFPD"] == "不符合")
                    {
                        mAllHg = false;
                    }
                }
                else
                    sign = false;
                #endregion

                #region 稳定度
                sign = true;
                if (jcxm.Contains("、稳定度、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["SJWD" + i]))
                        {
                            gs = gs - 1;
                            continue;
                        }
                    }
                    sum = 0;
                    for (int i = 1; i <= gs; i++)
                    {
                        md = Conversion.Val(sItem["SJWD" + i].Trim());
                        sum = sum + md;
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    //pjmd =Math.Round(pjmd, 2);
                    //pjmd = Math.Round(pjmd, 2, MidpointRounding.AwayFromZero);
                    mItem["W_WDD"] = pjmd.ToString("0.00");
                    mItem["GH_WDD"] = IsQualified(mItem["G_WDD"], mItem["W_WDD"], true);
                }
                else
                {
                    mItem["W_WDD"] = "----";
                    mItem["GH_WDD"] = "----";
                    mItem["G_WDD"] = "----";
                }
                #endregion

                #region 流值
                sign = true;
                if (jcxm.Contains("、流值、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["SJLZ" + i]))
                        {
                            gs = gs - 1;
                            continue;
                        }
                    }
                    sum = 0;
                    for (int i = 1; i <= gs; i++)
                    {
                        md = Conversion.Val(sItem["SJLZ" + i].Trim());
                        sum = sum + md;
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    //pjmd = Math.Round(pjmd, 1);
                    mItem["W_LZ"] = pjmd.ToString("0.0");
                    mItem["GH_LZ"] = IsQualified(mItem["G_LZ"], mItem["W_LZ"], true);
                }
                else
                {
                    mItem["W_LZ"] = "----";
                    mItem["GH_LZ"] = "----";
                    mItem["G_LZ"] = "----";
                }
                #endregion

                #region 矿料间隙率
                sign = true;
                if (jcxm.Contains("、矿料间隙率、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["KLJXL" + i]))
                        {
                            gs = gs - 1;
                            continue;
                        }
                    }
                    sum = 0;
                    for (int i = 1; i <= gs; i++)
                    {
                        md = Conversion.Val(sItem["KLJXL" + i].Trim());
                        sum = sum + md;
                    }
                    pjmd = sum / gs;
                    //pjmd = Math.Round(pjmd, 1);
                    mItem["W_KLJXL"] = pjmd.ToString("0.0");
                    mItem["GH_KLJXL"] = IsQualified(mItem["G_KLJXL"], mItem["W_KLJXL"], true);
                }
                else
                {
                    mItem["W_KLJXL"] = "----";
                    mItem["GH_KLJXL"] = "----";
                    mItem["G_KLJXL"] = "----";
                }
                #endregion

                #region 马歇尔模数
                if (jcxm.Contains("、马歇尔模数、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["MXEMS" + i]))
                        {
                            gs = gs - 1;
                            continue;
                        }
                    }
                    sum = 0;
                    for (int i = 1; i <= gs; i++)
                    {
                        md = Conversion.Val(sItem["MXEMS" + i].Trim());
                        sum = sum + md;
                    }
                    pjmd = sum / gs;
                    //pjmd = Math.Round(pjmd, 1);
                    mItem["W_MXEMS"] = pjmd.ToString("0.0");
                    mItem["GH_MXEMS"] = IsQualified(mItem["G_MXEMS"], mItem["W_MXEMS"], true);
                }
                else
                {
                    mItem["W_MXEMS"] = "----";
                    mItem["GH_MXEMS"] = "----";
                    mItem["G_MXEMS"] = "----";
                }
                #endregion

                #region 空隙率
                if (jcxm.Contains("、空隙率、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["KSL" + i]))
                        {
                            gs = gs - 1;
                            continue;
                        }
                    }
                    sum = 0;
                    for (int i = 1; i <= gs; i++)
                    {
                        md = Conversion.Val(sItem["KSL" + i].Trim());
                        sum = sum + md;
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    //pjmd = Math.Round(pjmd, 1);
                    mItem["W_KXL"] = pjmd.ToString("0.0");
                    mItem["GH_KXL"] = IsQualified(mItem["G_KXL"], mItem["W_KXL"], true);
                }
                else
                {
                    mItem["W_KXL"] = "----";
                    mItem["GH_KXL"] = "----";
                    mItem["G_KXL"] = "----";
                }
                #endregion

                #region 沥青饱和度
                if (jcxm.Contains("、沥青饱和度、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["LQBHD" + i]))
                        {
                            gs = gs - 1;
                            continue;
                        }
                    }
                    sum = 0;
                    for (int i = 1; i <= gs; i++)
                    {
                        md = Conversion.Val(sItem["LQBHD" + i].Trim());
                        sum = sum + md;
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    //pjmd = Math.Round(pjmd, 1);
                    mItem["W_LQBHD"] = pjmd.ToString("0.0");
                    mItem["GH_LQBHD"] = IsQualified(mItem["G_LQBHD"], mItem["W_LQBHD"], true);
                }
                else
                {
                    mItem["W_LQBHD"] = "----";
                    mItem["GH_LQBHD"] = "----";
                    mItem["G_LQBHD"] = "----";
                }
                #endregion

                #region 残留稳定度
                if (jcxm.Contains("、残留稳定度、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["SJWD" + i]) || !IsNumeric(sItem["CLWD" + i]))
                        {
                            gs = gs - 1;
                            continue;
                        }
                    }
                    sum = 0;
                    for (int i = 1; i <= gs; i++)
                    {
                        md1 = Conversion.Val(sItem["CLWD" + i].Trim());
                        md2 = Conversion.Val(sItem["SJWD" + i].Trim());
                        md = 100 * md1 / md2;
                        md = Math.Round(md, 1);
                        sum = sum + md;
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    //pjmd = Math.Round(pjmd, 1);
                    mItem["W_CLWDD"] = pjmd.ToString("0.0");
                    mItem["GH_CLWDD"] = IsQualified(mItem["G_CLWDD"], mItem["W_CLWDD"], true);
                }
                else
                {
                    mItem["W_CLWDD"] = "----";
                    mItem["GH_CLWDD"] = "----";
                    mItem["G_CLWDD"] = "----";
                }
                #endregion

                #region 密度
                if (jcxm.Contains("、密度、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["SCMD" + i]))
                        {
                            gs = gs - 1;
                            continue;
                        }
                    }
                    sum = 0;
                    for (int i = 1; i <= gs; i++)
                    {
                        md = Conversion.Val(sItem["SCMD" + i].Trim());
                        sum = sum + md;
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    //pjmd = Math.Round(pjmd, 1);
                    mItem["W_MD"] = pjmd.ToString("0.0");
                    mItem["GH_MD"] = IsQualified(mItem["G_MD"], mItem["W_MD"], true);
                }
                else
                {
                    mItem["W_MD"] = "----";
                    mItem["GH_MD"] = "----";
                    mItem["G_MD"] = "----";
                }
                sItem["JCJG"] = "合格";
                mAllHg = true;
                jsbeizhu = "该组样品检测结果如上。";
            }
            #endregion

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
