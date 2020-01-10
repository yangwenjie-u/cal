using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TP : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            int mbHggs;
            mSFwc = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_TP_DJ"];
            var mrdMdDj = dataExtra["BZ_TPMDB"];
            var MItem = data["M_TP"];
            var mitem = MItem[0];
            var SItem = data["S_TP"];
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
                            sign_fun = true;
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
                                max_bl = true;
                            }
                            if (IsNumeric(r_bl))
                            {
                                min_sjz = GetSafeDouble(r_bl);
                                min_bl = true;
                            }
                            sign_fun = true;
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
                            sign_fun = true;
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
                                min_bl = true;
                            }
                            if (IsNumeric(r_bl))
                            {
                                max_sjz = GetSafeDouble(r_bl);
                                max_bl = true;
                            }
                            sign_fun = true;
                        }
                        if (sj_fun.Contains("～"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("～") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0, dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_bl = true;
                            max_bl = true;
                            sign_fun = true;
                        }
                        if (sj_fun.Contains("±"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("±") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0, dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_sjz = min_sjz - max_sjz;
                            max_sjz = min_sjz + 2 * max_sjz;
                            min_bl = true;
                            max_bl = true;
                            sign_fun = true;
                        }
                        if (sj_fun == "0" && !string.IsNullOrEmpty(sj_fun))
                        {
                            sign_fun = true;
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
                            sign_fun = true; //做为判定了
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
            mAllHg = true;
            foreach (var sitem in SItem)
            {
                mbHggs = 0;
                mSFwc = true;
                Double md1, md2, md, pjmd, sum, Gd1, Gd2;
                string bl;
                int xd, Gs = 0;
                double[] nArr;
                bool flag, sign, mark;
                mbHggs = 0; var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                //密度、抗压、冻融、吸水率
                if (jcxm.Contains("、抗压、"))
                {
                    sign = true;
                    //等级表
                    Gs = mrsDj.Count();
                    var mrsDj_item = mrsDj[0];
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (mrsDj_item["QDDJ"].Trim() == sitem["QDDJ"].Trim())
                        {
                            mitem["G_QD_AVG"] = mrsDj_item["QD_AVG"].Trim();
                            mitem["G_QD_BZZ"] = mrsDj_item["QD_BZZ"].Trim();
                            mitem["G_QD_MIN"] = mrsDj_item["QD_MIN"].Trim();
                            break;
                        }
                        if (xd >= Gs)
                            mrsDj_item = mrsDj[xd - 1];
                        else
                            mrsDj_item = mrsDj[xd];
                    }
                    if (xd > Gs)
                        sign = false;
                    for (xd = 1; xd <= 10; xd++)
                    {
                        if (!IsNumeric(sitem["KYCD" + xd + "_1"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["KYCD" + xd + "_2"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["KYKD" + xd + "_1"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["KYKD" + xd + "_2"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["KYHZ" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        nArr = new double[11];
                        sum = 0;
                        for (xd = 1; xd <= 10; xd++)
                        {
                            md1 = Conversion.Val(sitem["KYCD" + xd + "_1"].Trim());
                            md2 = Conversion.Val(sitem["KYCD" + xd + "_2"].Trim());
                            md = (md1 + md2) / 2;
                            md = Round(md, 0);
                            Gd1 = md;
                            md1 = Conversion.Val(sitem["KYKD" + xd + "_1"].Trim());
                            md2 = Conversion.Val(sitem["KYKD" + xd + "_2"].Trim());
                            md = (md1 + md2) / 2;
                            md = Round(md, 0);
                            Gd2 = md;
                            md1 = Conversion.Val(sitem["KYHZ" + xd].Trim());
                            md2 = Gd1 * Gd2;
                            md2 = Round(md2, 0);
                            md = 1000 * md1 / md2;
                            md = Round(md, 2);
                            mitem["KYQD" + xd] = md.ToString("0.00");
                            sum = sum + md;
                            nArr[xd] = md;
                        }
                        pjmd = sum / 10;
                        pjmd = Round(pjmd, 1);
                        mitem["W_QD_AVG"] = pjmd.ToString("0.0");
                        Array.Sort(nArr);
                        md = nArr[1];
                        md = Round(md, 1);
                        mitem["W_QD_MIN"] = md.ToString("0.0");
                        sum = 0;
                        for (xd = 1; xd <= 10; xd++)
                        {
                            md = Math.Pow(nArr[xd] - pjmd, 2);
                            sum = md + sum;
                        }
                        md = Math.Sqrt(sum / 9);
                        md = Round(md, 2);
                        mitem["W_QD_BZZ"] = md.ToString("0.00");
                        md1 = Conversion.Val(mitem["W_QD_AVG"].Trim());
                        md2 = Conversion.Val(mitem["W_QD_BZZ"]);
                        md = md2 / md1;
                        md = Round(md, 2);
                        mitem["W_QD_BY"] = md.ToString("0.00");
                        if (calc_PB("＞0.21", mitem["W_QD_BZZ"], false) == "合格")
                        {
                            mitem["G_QD"] = "抗压强度平均值" + mitem["G_QD_AVG"] + "MPa," + "单块最小抗压强度值" + mitem["G_QD_MIN"] + "MPa";
                            flag = true;
                            flag = calc_PB(mitem["G_QD_AVG"], mitem["W_QD_AVG"], false) == "合格" ? flag : false;
                            flag = calc_PB(mitem["G_QD_MIN"], mitem["W_QD_MIN"], false) == "合格" ? flag : false;
                        }
                        else
                        {
                            mitem["G_QD"] = "抗压强度平均值" + mitem["G_QD_AVG"] + "MPa," + "强度标准值" + mitem["G_QD_BZZ"] + "MPa";
                            flag = true;
                            flag = calc_PB(mitem["G_QD_AVG"], mitem["W_QD_AVG"], false) == "合格" ? flag : false;
                            flag = calc_PB(mitem["G_QD_BZZ"], mitem["G_QD_BZZ"], false) == "合格" ? flag : false;
                        }
                        mitem["GH_QD"] = flag ? "合格" : "不合格";
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["G_QD_AVG"] = "----";
                    mitem["G_QD_BZZ"] = "----";
                    mitem["G_QD_MIN"] = "----";
                    mitem["G_QD"] = "-----";
                    mitem["GH_QD"] = "----";
                }
                if (jcxm.Contains("、密度、"))
                {
                    sign = true;
                    //等级表 ,
                    Gs = mrdMdDj.Count();
                    var mrdMdDj_item = mrdMdDj[0];
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (mrdMdDj_item["MDDJ"].Trim() == sitem["MDDJ"].Trim())
                        {
                            mitem["G_MD_AVG"] = mrdMdDj_item["MD_AVG"].Trim();
                            break;
                        }
                        if (xd >= Gs)
                            mrdMdDj_item = mrdMdDj[xd - 1];
                        else
                            mrdMdDj_item = mrdMdDj[xd];
                    }
                    if (xd > Gs)
                        sign = false;
                    for (xd = 1; xd <= 3; xd++)
                    {
                        if (!IsNumeric(sitem["MDCD" + xd + "_1"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MDCD" + xd + "_2"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MDKD" + xd + "_1"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MDKD" + xd + "_2"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MDGD" + xd + "_1"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MDGD" + xd + "_2"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["MDZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        nArr = new double[4];
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md1 = Conversion.Val(sitem["MDCD" + xd + "_1"].Trim());
                            md2 = Conversion.Val(sitem["MDCD" + xd + "_2"].Trim());
                            md = (md1 + md2) / 2;
                            md = Round(md, 0);
                            Gd1 = md;
                            md1 = Conversion.Val(sitem["MDKD" + xd + "_1"].Trim());
                            md2 = Conversion.Val(sitem["MDKD" + xd + "_2"].Trim());
                            md = (md1 + md2) / 2;
                            md = Round(md, 0);
                            Gd2 = md;
                            md1 = Conversion.Val(sitem["MDGD" + xd + "_1"].Trim());
                            md2 = Conversion.Val(sitem["MDGD" + xd + "_2"].Trim());
                            md = (md1 + md2) / 2;
                            md = Round(md, 0);
                            md = md * Gd1 * Gd2;
                            md1 = Conversion.Val(sitem["MDZL" + xd].Trim());
                            md2 = Round(md, 0);
                            md = Math.Pow(md1 * 10, 3) / Math.Pow(md2 * 10, 6);
                            md = Round(md, 1);
                            mitem["SCMD" + xd] = md.ToString("0.0");
                            nArr[xd] = md;
                            sum = sum + md;
                        }
                        pjmd = sum / 3;
                        pjmd = Round(pjmd, 0);
                        mitem["W_MD_AVG"] = pjmd.ToString();
                        mitem["GH_MD"] = calc_PB(mitem["G_MD_AVG"], mitem["W_MD_AVG"], false);
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_MD_AVG"] = "----";
                    mitem["GH_MD"] = "----";
                    mitem["G_MD_AVG"] = "----";
                    for (xd = 1; xd <= 3; xd++)
                        mitem["SCMD" + xd] = "----";
                }
                if (jcxm.Contains("、吸水率、"))
                {
                    sign = true;
                    //等级表
                    Gs = mrsDj.Count();
                    switch (sitem["ZLDJ"])
                    {
                        case "优等品":
                            bl = "xsl_dj1";
                            break;
                        case "一等品":
                            bl = "xsl_dj2";
                            break;
                        default:
                            bl = "xsl_dj3";
                            break;
                    }
                    var mrsDj_item = mrsDj[0];
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (mrsDj_item["QDDJ"].Trim() == sitem["QDDJ"].Trim())
                        {
                            mitem["G_XSL"] = mrsDj_item[bl];
                            break;
                        }
                        if (xd >= Gs)
                            mrsDj_item = mrsDj[xd - 1];
                        else
                            mrsDj_item = mrsDj[xd];
                    }
                    if (xd > Gs)
                        sign = false;
                    for (xd = 1; xd <= 5; xd++)
                    {
                        if (!IsNumeric(sitem["XSLGZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["XSLSZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        nArr = new double[6];
                        sum = 0;
                        for (xd = 1; xd <= 5; xd++)
                        {
                            md1 = Conversion.Val(sitem["XSLGZL" + xd].Trim());
                            md2 = Conversion.Val(sitem["XSLSZL" + xd].Trim());
                            md = 100 * (md2 - md1) / md1;
                            md = Round(md, 1);
                            mitem["XSLZ" + xd] = md.ToString("0.0");
                            sum = sum + md;
                        }
                        pjmd = sum / 5;
                        pjmd = Round(pjmd, 1);
                        mitem["W_XSL"] = pjmd.ToString("0.0");
                        mitem["GH_XSL"] = calc_PB(mitem["G_XSL"], mitem["W_XSL"], false);
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["W_XSL"] = "----";
                    mitem["GH_XSL"] = "----";
                    mitem["G_XSL"] = "----";
                }
                if (jcxm.Contains("、冻融、"))
                {
                    sign = true;
                    //等级表
                    var mrsDj_item = mrsDj[0];
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (mrsDj_item["QDDJ"].Trim() == sitem["QDDJ"].Trim())
                        {
                            mitem["G_DR_KDX"] = mrsDj_item["DR_KDX"].Trim();
                            mitem["G_DR_SS"] = "≤2.0";
                            mitem["G_KDX"] = "冻后抗压强度平均值" + mrsDj_item["DR_KDX"].Trim() + "MPa," + "单块砖的干质量损失" + mitem["G_DR_SS"] + "%";
                            break;
                        }
                        if (xd >= Gs)
                            mrsDj_item = mrsDj[xd - 1];
                        else
                            mrsDj_item = mrsDj[xd];
                    }
                    if (xd > Gs)
                        sign = false;
                    for (xd = 1; xd <= 5; xd++)
                    {
                        if (!IsNumeric(sitem["DQZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["DHZL" + xd]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["DRCD" + xd + "_1"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["DRCD" + xd + "_2"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["DRKD" + xd + "_1"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["DRKD" + xd + "_2"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["DRHZ" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        nArr = new double[6];
                        sum = 0;
                        for (xd = 1; xd <= 5; xd++)
                        {
                            md1 = Conversion.Val(sitem["DQZL" + xd].Trim());
                            md2 = Conversion.Val(sitem["DHZL" + xd].Trim());
                            md = 100 * (md1 - md2) / md1;
                            md = Round(md, 1);
                            sum = sum + md;
                            nArr[xd] = md;
                            mitem["DRSS" + xd] = md.ToString("0.0");
                        }
                        Array.Sort(nArr);
                        mitem["W_DR_SS"] = nArr[5].ToString("0.0");
                        sum = 0;
                        for (xd = 1; xd <= 5; xd++)
                        {
                            md1 = Conversion.Val(sitem["DRCD" + xd + "_1"].Trim());
                            md2 = Conversion.Val(sitem["DRCD" + xd + "_2"].Trim());
                            md = (md1 + md2) / 2;
                            md = Round(md, 0);
                            Gd1 = md;
                            md1 = Conversion.Val(sitem["DRKD" + xd + "_1"].Trim());
                            md2 = Conversion.Val(sitem["DRKD" + xd + "_2"].Trim());
                            md = (md1 + md2) / 2;
                            md = Round(md, 0);
                            Gd2 = md;
                            md = Gd1 * Gd2;
                            md1 = Conversion.Val(sitem["DRHZ" + xd].Trim());
                            md2 = Round(md, 0);
                            md = 1000 * md1 / md2;
                            md = Round(md, 2);
                            mitem["DRQD" + xd] = md.ToString("0.00");
                            sum = sum + md;
                        }
                        pjmd = sum / 5;
                        pjmd = Round(pjmd, 2);
                        mitem["W_DR_KDX"] = pjmd.ToString("0.00");
                        flag = true;
                        flag = calc_PB(mitem["G_DR_SS"], mitem["W_DR_SS"], false) == "合格" ? flag : false;
                        flag = calc_PB(mitem["G_DR_KDX"], mitem["W_DR_KDX"], false) == "合格" ? flag : false;
                        mitem["GH_DR"] = flag ? "合格" : "不合格";
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    mitem["G_DR_SS"] = "----";
                    mitem["W_DR_SS"] = "----";
                    mitem["G_DR_KDX"] = "----";
                    mitem["W_DR_KDX"] = "----";
                    mitem["GH_DR"] = "----";
                    for (xd = 1; xd <= 5; xd++)
                    {
                        mitem["DRQD" + xd] = "----";
                        mitem["DRSS" + xd] = "----";
                    }
                    mitem["G_KDX"] = "----";
                }
                mbHggs = mitem["GH_QD"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_DR"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_XSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_MD"] == "不合格" ? mbHggs + 1 : mbHggs;
                sitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                mitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                mitem["JCJGMS"] = mbHggs == 0 ? "该样品符合标准要求。" : "该样品不符合标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
