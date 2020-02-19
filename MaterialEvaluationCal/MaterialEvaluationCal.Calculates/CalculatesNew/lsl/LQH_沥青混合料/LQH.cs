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

                sign = true;
                if (jcxm.Contains("、沥青含量、"))
                {
                    gs = 3;
                    for (int i = 1; i <= 3; i++)
                    {
                        if (!IsNumeric(sItem["L_HHL" + i]) && !IsNumeric(sItem["L_LZL" + i]) && !IsNumeric(sItem["L_RQZL" + i])
                            && !IsNumeric(sItem["L_RJG" + i]) && !IsNumeric(sItem["L_LJK" + i]) && !IsNumeric(sItem["L_GGZL" + i])
                             && !IsNumeric(sItem["L_CTL" + i]) && !IsNumeric(sItem["L_ZSCT" + i]) && !IsNumeric(sItem["L_CZZL" + i]))
                        {
                            gs = i - 1;
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
                        nArr[6] = Round(nArr[6], 2);
                        sum = sum + nArr[6];
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    pjmd = Round(pjmd, 2);
                    mItem["W_LQHL"] = pjmd.ToString();
                    mItem["GH_LQHL"] = IsQualified(mItem["G_LQHL"], mItem["W_LQHL"], true);
                }
                else
                {
                    mItem["W_LQHL"] = "----";
                    mItem["GH_LQHL"] = "----";
                    mItem["G_LQHL"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、稳定度、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["SJWD" + i]))
                        {
                            gs = i - 1;
                            break;
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
                    pjmd = Round(pjmd, 2);
                    mItem["W_WDD"] = pjmd.ToString();
                    mItem["GH_WDD"] = IsQualified(mItem["G_WDD"], mItem["W_WDD"], true);
                }
                else
                {
                    mItem["W_WDD"] = "----";
                    mItem["GH_WDD"] = "----";
                    mItem["G_LQHL"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、流值、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["SJLZ" + i]))
                        {
                            gs = i - 1;
                            break;
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
                    pjmd = Round(pjmd, 1);
                    mItem["W_LZ"] = pjmd.ToString();
                    mItem["GH_LZ"] = IsQualified(mItem["G_LZ"], mItem["W_LZ"], true);
                }
                else
                {
                    mItem["W_LZ"] = "----";
                    mItem["GH_LZ"] = "----";
                    mItem["G_LZ"] = "----";
                }
                sign = true;
                //if (jcxm.Contains("、马歇尔模数、"))
                //{
                //    string Bhgxm = "";
                //    //先计算一下修正系数
                //    int Gs = 0;
                //    for (int xd = 1; xd <= 6; xd++)
                //    {
                //        if (IsNumeric(sItem["SJWD" + xd]) && !string.IsNullOrEmpty(sItem["SJWD" + xd].Trim()))
                //            Gs = Gs + 1;

                //    }
                //    double[] dArray = new double[Gs + 1];
                //    Gs = 0;
                //    for (int xd = 1; xd <= 6; xd++)
                //    {
                //        if (IsNumeric(sItem["SJWD" + xd]) && !string.IsNullOrEmpty(sItem["SJWD" + xd].Trim()))
                //        {
                //            Gs = Gs + 1;
                //            dArray[Gs] = Conversion.Val(sItem["SJWD" + xd]);
                //        }
                //    }
                //    sign = Gs > 1 ? sign : false;
                //    if (sign)
                //    {
                //        sum = 0;
                //        for (int xd = 1; xd <= Gs; xd++)
                //        {
                //            sum = sum + dArray[xd];
                //        }
                //        pjmd = sum / Gs;
                //        pjmd = Round(pjmd, 2);
                //        sum = 0;
                //        for (int xd = 1; xd <= Gs; xd++)
                //            sum = sum + Math.Pow(dArray[xd] - pjmd, 2);
                //        md = Math.Sqrt(sum / (Gs - 1));
                //        bool flag = false;
                //        if (Gs == 3)
                //        {
                //            md = md * 1.15;
                //            flag = true;
                //        }
                //        else if (Gs == 4)
                //        {
                //            md = md * 1.46;
                //            flag = true;
                //        }
                //        else if (Gs == 5)
                //        {
                //            md = md * 1.67;
                //            flag = true;
                //        }
                //        else if (Gs == 6)
                //        {
                //            md = md * 1.82;
                //            flag = true;
                //        }
                //        else
                //        {
                //            flag = false;
                //        }
                //        sum = 0;
                //        Gs = 0;
                //        for (int xd = 1; xd < dArray.Length; xd++)
                //        {
                //            if (flag && Math.Abs(dArray[xd] - pjmd) > md)
                //            { }
                //            else
                //            {
                //                sum = sum + dArray[xd];
                //                Gs = Gs + 1;
                //            }
                //        }
                //        pjmd = sum / Gs;
                //        pjmd = Round(pjmd, 2);
                //        sItem["W_WD"] = pjmd.ToString("0.00");
                //        //判定
                //        sItem["PD_WD"] = IsQualified(sItem["G_WD"], sItem["W_WD"], true);
                //        if (sItem["PD_WD"] == "不符合")
                //        {
                //            mAllHg = false;
                //        }
                //    }
                //}
                //else
                //    sign = false;
                //if (!sign)
                //{
                //    sItem["W_WD"] = "----";
                //    sItem["PD_WD"] = "----";
                //    sItem["G_WD"] = "----";
                //    sItem["B_WD"] = "----";
                //}

                if (jcxm.Contains("、空隙率、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["KSL" + i]))
                        {
                            gs = i - 1;
                            break;
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
                    pjmd = Round(pjmd, 1);
                    mItem["W_KXL"] = pjmd.ToString();
                    mItem["GH_KXL"] = IsQualified(mItem["G_KXL"], mItem["W_KXL"], true);
                }
                else
                {
                    mItem["W_KXL"] = "----";
                    mItem["GH_KXL"] = "----";
                    mItem["G_KXL"] = "----";
                }

                if (jcxm.Contains("、沥青饱和度、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["LQBHD" + i]))
                        {
                            gs = i - 1;
                            break;
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
                    pjmd = Round(pjmd, 1);
                    mItem["W_LQBHD"] = pjmd.ToString();
                    mItem["GH_LQBHD"] = IsQualified(mItem["G_LQBHD"], mItem["W_LQBHD"], true);
                }
                else
                {
                    mItem["W_LQBHD"] = "----";
                    mItem["GH_LQBHD"] = "----";
                    mItem["G_LQBHD"] = "----";
                }

                if (jcxm.Contains("、残留稳定度、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["SJWD" + i]) || !IsNumeric(sItem["CLWD" + i]))
                        {
                            gs = i - 1;
                            break;
                        }
                    }
                    sum = 0;
                    for (int i = 1; i <= gs; i++)
                    {
                        md1 = Conversion.Val(sItem["CLWD" + i].Trim());
                        md2 = Conversion.Val(sItem["SJWD" + i].Trim());
                        md = 100 * md1 / md2;
                        md = Round(md, 1);
                        sum = sum + md;
                    }
                    if (gs == 0)
                        pjmd = 0;
                    else
                        pjmd = sum / gs;
                    pjmd = Round(pjmd, 1);
                    mItem["W_CLWDD"] = pjmd.ToString();
                    mItem["GH_CLWDD"] = IsQualified(mItem["G_CLWDD"], mItem["W_CLWDD"], true);
                }
                else
                {
                    mItem["W_CLWDD"] = "----";
                    mItem["GH_CLWDD"] = "----";
                    mItem["G_CLWDD"] = "----";
                }

                if (jcxm.Contains("、密度、"))
                {
                    gs = 6;
                    for (int i = 1; i <= 6; i++)
                    {
                        if (!IsNumeric(sItem["SCMD" + i]))
                        {
                            gs = i - 1;
                            break;
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
                    pjmd = Round(pjmd, 1);
                    mItem["W_MD"] = pjmd.ToString();
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
