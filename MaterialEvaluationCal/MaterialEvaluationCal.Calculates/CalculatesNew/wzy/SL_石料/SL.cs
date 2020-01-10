using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates.CalculatesNew.wzy.SL_石料
{
    public class SL : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 参数定义
            string mcalBh, mlongStr;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            int mbHggs;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_SL_DJ"];
            var MItem = data["M_SL"];
            var mitem = MItem[0];
            var SItem = data["S_SL"];
            #endregion

            #region  自定义函数
            //这个函数总括了对于判定(符合,不符合) 还是判断(合格,不合格)
            Func<string, string, bool, string> calc_PB =
                delegate (string sj_fun, string sc_fun, bool flag_fun)
                {
                    string calc_PB_fun = string.Empty;
                    sj_fun = sj_fun.Trim();
                    sc_fun = sc_fun.Trim();
                    if (!IsNumeric(sc_fun))
                    {
                        calc_PB_fun = "----";
                    }
                    else
                    {
                        sj_fun = sj_fun.Replace("~", "～");
                        string l_bl, r_bl;
                        double min_sjz, max_sjz, scz;
                        int length, dw;
                        bool min_bl, max_bl, sign_fun;
                        min_sjz = -99999;
                        max_sjz = 99999;
                        scz = GetSafeDouble(sc_fun);
                        sign_fun = false;
                        min_bl = false;
                        max_bl = false;
                        if (sj_fun.Contains("＞"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("＞") + 1;
                            l_bl = sj_fun.Substring(0, dw - 1);
                            r_bl = sj_fun.Substring(dw, length - dw);
                            if (IsNumeric(l_bl))
                            {
                                max_sjz = GetSafeDouble(l_bl);
                                max_bl = false;
                            }
                            if (IsNumeric(r_bl))
                            {
                                min_sjz = GetSafeDouble(r_bl);
                                min_bl = false;
                            }
                            sign_fun = true; ;
                        }
                        if (sj_fun.Contains("≥"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("≥") + 1;
                            l_bl = sj_fun.Substring(0, dw - 1);
                            r_bl = sj_fun.Substring(dw, length - dw);
                            if (IsNumeric(l_bl))
                            {
                                max_sjz = GetSafeDouble(l_bl);
                                max_bl = true; ;
                            }
                            if (IsNumeric(r_bl))
                            {
                                min_sjz = GetSafeDouble(r_bl);
                                min_bl = true; ;
                            }
                            sign_fun = true; ;
                        }
                        if (sj_fun.Contains("＜"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("＜") + 1;
                            l_bl = sj_fun.Substring(0, dw - 1);
                            r_bl = sj_fun.Substring(dw, length - dw);
                            if (IsNumeric(l_bl))
                            {
                                min_sjz = GetSafeDouble(l_bl);
                                min_bl = false;
                            }
                            if (IsNumeric(r_bl))
                            {
                                max_sjz = GetSafeDouble(r_bl);
                                max_bl = false;
                            }
                            sign_fun = true; ;
                        }
                        if (sj_fun.Contains("≤"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("≤") + 1;
                            l_bl = sj_fun.Substring(0, dw - 1);
                            r_bl = sj_fun.Substring(dw, length - dw);
                            if (IsNumeric(l_bl))
                            {
                                min_sjz = GetSafeDouble(l_bl);
                                min_bl = true; ;
                            }
                            if (IsNumeric(r_bl))
                            {
                                max_sjz = GetSafeDouble(r_bl);
                                max_bl = true; ;
                            }
                            sign_fun = true; ;
                        }
                        if (sj_fun.Contains("～"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("～") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0, dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_bl = true; ;
                            max_bl = true; ;
                            sign_fun = true; ;
                        }
                        if (sj_fun.Contains("±"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("±") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0, dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_sjz = min_sjz - max_sjz;
                            max_sjz = min_sjz + 2 * max_sjz;
                            min_bl = true; ;
                            max_bl = true; ;
                            sign_fun = true; ;
                        }
                        if (sj_fun == "0" && !string.IsNullOrEmpty(sj_fun))
                        {
                            sign_fun = true; ;
                            min_bl = false;
                            max_bl = false;
                            max_sjz = 0;
                        }
                        if (!sign_fun)
                        {
                            calc_PB_fun = "----";
                        }
                        else
                        {
                            string hgjl, bhgjl;
                            hgjl = flag_fun ? "符合" : "合格";
                            bhgjl = flag_fun ? "不符合" : "不合格";
                            sign_fun = true; ; //做为判定了
                            if (min_bl)
                                sign_fun = scz >= min_sjz ? sign_fun : false;
                            else
                                sign_fun = scz > min_sjz ? sign_fun : false;
                            if (max_bl)
                                sign_fun = scz <= max_sjz ? sign_fun : false;
                            else
                                sign_fun = scz < max_sjz ? sign_fun : false;
                            calc_PB_fun = sign_fun ? hgjl : bhgjl;
                        }
                    }
                    return calc_PB_fun;
                };
            #endregion

            #region  计算开始
            mGetBgbh = false;
            mAllHg = true; ;
            foreach (var sitem in SItem)
            {
                mbHggs = 0;
                mSFwc = true; ;
                double md1, md2, md3, xd1, xd2, xd3, md = 0, pjmd, sum;
                string bl;
                int xd, Gs;
                double[] nArr;
                bool flag, sign, mark;
                mbHggs = 0;
                sign = true; ;
                var jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                if (jcxm.Contains("、抗压强度、"))
                {
                    for (xd = 1; xd <= 6; xd++)
                    {
                        if (!IsNumeric(sitem["KY_JMJ" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["KY_PHHZ" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        mark = true; ;
                        nArr = new double[7];
                        sum = 0;
                        for (xd = 1; xd <= 6; xd++)
                        {
                            md1 = Conversion.Val(sitem["KY_PHHZ" + xd].Trim());
                            md2 = Conversion.Val(sitem["KY_JMJ" + xd].Trim());
                            md = md1 / md2;
                            md = Round(md, 1);
                            sum = sum + md;
                            nArr[xd] = md;
                        }
                        pjmd = sum / 6;
                        pjmd = Round(pjmd, 1);
                        mitem["W_KYQD"] = pjmd.ToString("0.0");
                        mitem["GH_KYQD"] = calc_PB(mitem["G_KYQD"], mitem["W_KYQD"], true);
                    }
                    else
                    {
                        mSFwc = false;
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["G_KYQD"] = "----";
                    mitem["W_KYQD"] = "----";
                    mitem["GH_KYQD"] = "----";
                }
                sign = true; ;
                if (jcxm.Contains("、含水率、"))
                {
                    for (xd = 1; xd <= 5; xd++)
                    {
                        if (!IsNumeric(sitem["HSL_HZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["HSL_QWZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["HSL_HWZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 5; xd++)
                        {
                            md = Conversion.Val(sitem["HSL_QWZL" + xd].Trim()) - Conversion.Val(sitem["HSL_HWZL" + xd].Trim());
                            md = md * 100;
                            md = md / (Conversion.Val(sitem["HSL_HWZL" + xd]) - Conversion.Val(sitem["HSL_HZL" + xd].Trim()));
                            md = Round(md, 1);
                            sum = md + sum;
                        }
                        pjmd = sum / 5;
                        pjmd = Round(pjmd, 1);
                        mitem["W_HSL"] = pjmd.ToString("0.0");
                        mitem["GH_HSL"] = calc_PB(mitem["G_HSL"], mitem["W_HSL"], true);
                    }
                    else
                        mSFwc = false;
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_HSL"] = "----";
                    mitem["GH_HSL"] = "----";
                    mitem["G_HSL"] = "----";
                }
                sign = true; ;
                if (jcxm.Contains("、密度、"))
                {
                    for (xd = 1; xd <= 2; xd++)
                    {
                        if (!IsNumeric(sitem["MD_YFZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MD_PYZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MD_PYFZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MD_SYMD" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 2; xd++)
                        {
                            md1 = Conversion.Val(sitem["MD_YFZL" + xd].Trim()) * Conversion.Val(sitem["MD_SYMD" + xd].Trim());
                            md2 = Conversion.Val(sitem["MD_YFZL" + xd]) + Conversion.Val(sitem["MD_PYZL" + xd]) - Conversion.Val(sitem["MD_PYFZL" + xd]);
                            md = md1 / md2;
                            md = Round(md, 2);
                            sum = md + sum;
                        }
                        pjmd = sum / 2;
                        pjmd = Round(pjmd, 2);
                        mitem["W_MD"] = pjmd.ToString("0.00");
                        mitem["GH_MD"] = calc_PB(mitem["G_MD"], mitem["W_MD"], true);
                    }
                    else
                        mSFwc = false;
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_MD"] = "----";
                    mitem["GH_MD"] = "----";
                    mitem["G_MD"] = "----";
                }
                sign = true; ;
                if (jcxm.Contains("、毛体积密度、"))
                {
                    for (xd = 1; xd <= 3; xd++)
                    {
                        if (!IsNumeric(sitem["MMD_BHZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MMD_HHZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MMD_SZZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MMD_SMD" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md1 = Conversion.Val(sitem["MMD_HHZL" + xd]) * Conversion.Val(sitem["MMD_SMD" + xd]);
                            md2 = Conversion.Val(sitem["MMD_BHZL" + xd]) - Conversion.Val(sitem["MMD_SZZL" + xd]);
                            md = md1 / md2;
                            md = Round(md, 2);
                            sum = md + sum;
                        }
                        pjmd = sum / 2;
                        pjmd = Round(pjmd, 2);
                        mitem["W_MTJMD"] = pjmd.ToString("0.00");
                        mitem["GH_MTJMD"] = calc_PB(mitem["G_MTJMD"], mitem["W_MTJMD"], true);
                    }
                    else
                        mSFwc = false;
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_MTJMD"] = "----";
                    mitem["GH_MTJMD"] = "----";
                    mitem["G_MTJMD"] = "----";
                }
                sign = true; ;
                if (jcxm.Contains("、孔隙率、"))
                {
                    for (xd = 1; xd <= 1; xd++)
                    {
                        if (!IsNumeric(mitem["W_MD"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(mitem["W_MTJMD"]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 1; xd++)
                        {
                            md1 = Conversion.Val(sitem["W_MD"].Trim());
                            md2 = Conversion.Val(sitem["W_MTJMD" + xd].Trim());
                            md = 100 * (1 - md2 / md1);
                            md = Round(md, 1);
                        }
                        pjmd = md;
                        pjmd = Round(md, 1);
                        mitem["W_KXL"] = pjmd.ToString("0.0");
                        mitem["GH_KXL"] = calc_PB(mitem["G_KXL"], mitem["W_KXL"], true);
                    }
                    else
                        mSFwc = false;
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_KXL"] = "----";
                    mitem["GH_KXL"] = "----";
                    mitem["G_KXL"] = "----";
                }
                sign = true; ;
                if (jcxm.Contains("、吸水性、"))
                {
                    for (xd = 1; xd <= 3; xd++)
                    {
                        if (!IsNumeric(sitem["XSX_HZZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["XSX_XZZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["XSX_QBZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md1 = Conversion.Val(sitem["XSX_XZZL" + xd].Trim()) - Conversion.Val(sitem["XSX_HZZL" + xd].Trim());
                            md2 = Conversion.Val(sitem["XSX_HZZL" + xd].Trim());
                            md = md1 / md2;
                            md = Round(md, 2);
                            sum = md + sum;
                        }
                        pjmd = sum / 3;
                        pjmd = Round(pjmd, 2);
                        mitem["W_XSL"] = pjmd.ToString("0.00");
                        sum = 0;
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md1 = Conversion.Val(sitem["XSX_QBZL" + xd].Trim()) - Conversion.Val(sitem["XSX_HZZL" + xd].Trim());
                            md2 = Conversion.Val(sitem["XSX_HZZL" + xd].Trim());
                            md = md1 / md2;
                            md = Round(md, 2);
                            sum = md + sum;
                        }
                        pjmd = sum / 3;
                        pjmd = Round(pjmd, 2);
                        mitem["W_BXSL"] = pjmd.ToString("0.00");
                        md1 = Conversion.Val(mitem["W_XSL"]);
                        md2 = Conversion.Val(mitem["W_BXSL"]);
                        md = md1 / md2;
                        md = Round(md, 2);
                        mitem["W_BSXS"] = md.ToString("0.00");
                        if (calc_PB(mitem["G_XSL"], mitem["W_XSL"], true) == "不符合" ||
                             calc_PB(mitem["G_BXSL"], mitem["W_BXSL"], true) == "不符合" ||
                             calc_PB(mitem["G_BSXS"], mitem["W_BSXS"], true) == "不符合")
                            mitem["GH_XSX"] = "不符合";
                        else if (calc_PB(mitem["G_XSL"], mitem["W_XSL"], true) == "符合" ||
                             calc_PB(mitem["G_BXSL"], mitem["W_BXSL"], true) == "符合" ||
                             calc_PB(mitem["G_BSXS"], mitem["W_BSXS"], true) == "符合")
                            mitem["GH_XSX"] = "符合";
                        else
                            mitem["GH_XSX"] = "----";
                    }
                    else
                        mSFwc = false;
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_XSL"] = "----";
                    mitem["W_BXSL"] = "----";
                    mitem["W_BSXS"] = "----";
                    mitem["G_XSL"] = "----";
                    mitem["G_BXSL"] = "----";
                    mitem["G_BSXS"] = "----";
                    mitem["GH_HSL"] = "----";
                }
                sign = true; ;
                if (jcxm.Contains("、抗折强度、"))
                {
                    for (xd = 1; xd <= 3; xd++)
                    {
                        if (!IsNumeric(sitem["KZ_PHHZ" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["KZ_DMK" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["KZ_DMG" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md1 = 3 * 200 * Conversion.Val(sitem["KZ_PHHZ" + xd].Trim());
                            md2 = 2 * Conversion.Val(sitem["KZ_DMK" + xd].Trim()) * Math.Pow(Conversion.Val(sitem["KZ_DMG" + xd].Trim()), 2);
                            md = md1 / md2;
                            md = Round(md, 1);
                            sum = md + sum;
                        }
                        pjmd = sum / 3;
                        pjmd = Round(pjmd, 1);
                        mitem["W_KZQD"] = pjmd.ToString("0.0");
                        mitem["GH_KZQD"] = calc_PB(mitem["G_KZQD"], mitem["W_KZQD"], true);
                    }
                    else
                        mSFwc = false;
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_KZQD"] = "----";
                    mitem["GH_KZQD"] = "----";
                    mitem["G_KZQD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、坚固性、"))
                {
                    for (xd = 1; xd <= 3; xd++)
                    {
                        if (!IsNumeric(sitem["JG_SYQZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["JG_SYHZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md1 = Conversion.Val(sitem["JG_SYQZL" + xd].Trim()) - Conversion.Val(sitem["JG_SYHZL" + xd].Trim());
                            md2 = Conversion.Val(sitem["JG_SYQZL" + xd].Trim());
                            md = 100 * md1 / md2;
                            md = Round(md, 1);
                            sum = md + sum;
                        }
                        pjmd = sum / 3;
                        pjmd = Round(pjmd, 1);
                        mitem["W_JGX"] = pjmd.ToString("0.0");
                        mitem["GH_JGX"] = calc_PB(mitem["G_JGX"], mitem["W_JGX"], true);
                    }
                    else
                        mSFwc = false;
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_JGX"] = "----";
                    mitem["GH_JGX"] = "----";
                    mitem["G_JGX"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、抗冻性、"))
                {
                    for (xd = 1; xd <= 3; xd++)
                    {
                        if (!IsNumeric(sitem["KD_SYQZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["KD_SYHZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md1 = Conversion.Val(sitem["KD_SYQZL" + xd].Trim()) - Conversion.Val(sitem["KD_SYHZL" + xd].Trim());
                            md2 = Conversion.Val(sitem["KD_SYQZL" + xd].Trim());
                            md = 100 * md1 / md2;
                            md = Round(md, 1);
                            sum = md + sum;
                        }
                        pjmd = sum / 3;
                        pjmd = Round(pjmd, 1);
                        mitem["W_KDX"] = pjmd.ToString("0.0");
                        mitem["GH_KDX"] = calc_PB(mitem["G_KDX"], mitem["W_KDX"], true);
                    }
                    else
                        mSFwc = false;
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_KDX"] = "----";
                    mitem["GH_KDX"] = "----";
                    mitem["G_KDX"] = "----";
                }
                mbHggs = mitem["GH_KYQD"] == "不符合" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_HSL"] == "不符合" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_MD"] == "不符合" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_MTJMD"] == "不符合" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_KXL"] == "不符合" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_KZQD"] == "不符合" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_JGX"] == "不符合" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_KDX"] == "不符合" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_BLLD"] == "不符合" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_WCX"] == "不符合" ? mbHggs + 1 : mbHggs;
                sitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                mitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                if (mbHggs == 0)
                {
                    if (mitem["GH_KYQD"] == "----" &&
                       mitem["GH_HSL"] == "----" &&
                       mitem["GH_MD"] == "----" &&
                       mitem["GH_MTJMD"] == "----" &&
                       mitem["GH_KXL"] == "----" &&
                       mitem["GH_KZQD"] == "----" &&
                       mitem["GH_JGX"] == "----" &&
                       mitem["GH_KDX"] == "----" &&
                       mitem["GH_BLLD"] == "----" &&
                       mitem["GH_WCX"] == "----")
                        mitem["JCJGMS"] = "该试样检测结果如上。";
                    else
                        mitem["JCJGMS"] = "该试样符合设计要求。";
                }
                else
                    mitem["JCJGMS"] = "该试样不符合设计要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
