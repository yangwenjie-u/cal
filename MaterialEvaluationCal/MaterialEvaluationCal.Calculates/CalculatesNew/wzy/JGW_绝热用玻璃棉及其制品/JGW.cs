﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JGW : BaseMethods
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
            var mrsDj = dataExtra["BZ_JGW_DJ"];
            var MItem = data["M_JGW"];
            var mitem = MItem[0];
            var SItem = data["S_JGW"];
            #endregion

            #region 自定义函数

            //以下为对用设计值的判定方式
            Func<string, string, string> calc_pd =
                delegate (string sj, string sc)
                {
                    sj = sj.Trim();
                    string calc_pd_ret = "";
                    bool isStart = false;
                    string tmpStr = string.Empty;
                    if (string.IsNullOrEmpty(sc))
                        sc = "";
                    for (int i = 1; i <= sc.Length; i++)
                    {
                        if (IsNumeric(sc.Substring(i - 1, 1)) || sc.Substring(i - 1, 1) == "." || sc.Substring(i - 1, 1) == "-")
                        {
                            isStart = true;
                            tmpStr = tmpStr + sc.Substring(i - 1, 1);
                        }
                        else
                        {
                            if (isStart == false && tmpStr != "")
                                break;
                        }
                    }
                    sc = tmpStr;
                    if (!IsNumeric(sc))
                    {
                        calc_pd_ret = "----";
                    }
                    else
                    {
                        double min_sjz, max_sjz, scz;
                        int length, dw;
                        if (sj.Contains("＞"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("＞") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains(">"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf(">") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("≥"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("≥") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("＜"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("＜") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("<"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("<") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("≤"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("≤") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("="))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("=") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz == max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("～"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("～") + 1;
                            min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= min_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            min_sjz = Conversion.Val(tmpStr);
                            max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= max_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            max_sjz = Conversion.Val(tmpStr);
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("~"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("~") + 1;
                            min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= min_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            min_sjz = Conversion.Val(tmpStr);
                            max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= max_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            max_sjz = Conversion.Val(tmpStr);
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
                        }
                    }
                    return calc_pd_ret;
                };

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

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            foreach (var sitem in SItem)
            {
                mbHggs = 0;
                mSFwc = true;
                int xd, Gs;
                Gs = mrsDj.Count();
                mitem["G_CCWDX"] = "----";
                mitem["G_CQXSL"] = "----";
                mitem["G_DQXSL"] = "----";
                mitem["G_DRXS"] = "----";
                mitem["G_GQPXD"] = "----";
                mitem["G_HDPC"] = "----";
                mitem["G_JSBLL"] = "----";
                mitem["G_KLQD"] = "----";
                mitem["G_MD"] = "----";
                mitem["G_XSL"] = "----";
                mitem["G_YSQD"] = "----";
                mitem["G_ZLXSL"] = "----";
                IDictionary<string, string> mrsDj_item = new Dictionary<string, string>();
                for (xd = 1; xd <= Gs; xd++)
                {
                    mrsDj_item = mrsDj[xd - 1];
                    switch (sitem["CPMC"].Trim())
                    {
                        case "玻璃棉":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["LX"].Trim() == mrsDj_item["LX"])
                            {
                                mitem["G_MD"] = mrsDj_item["G_MD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_XSL"] = mrsDj_item["G_XSL"];
                            }
                            break;
                        case "玻璃棉板":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["BCMD"].Trim() == mrsDj_item["BCMD"])
                            {
                                mitem["G_MD"] = mrsDj_item["G_MD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_XSL"] = mrsDj_item["G_XSL"];
                            }
                            break;
                        case "玻璃棉带":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["BCMD"].Trim() == mrsDj_item["BCMD"])
                            {
                                mitem["G_MD"] = mrsDj_item["G_MD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_XSL"] = mrsDj_item["G_XSL"];
                            }
                            break;
                        case "玻璃棉毯":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["LX"].Trim() == mrsDj_item["LX"])
                            {
                                mitem["G_MD"] = mrsDj_item["G_MD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_XSL"] = mrsDj_item["G_XSL"];
                            }
                            break;
                        case "玻璃棉毡":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["BCMD"].Trim() == mrsDj_item["BCMD"])
                            {
                                mitem["G_MD"] = mrsDj_item["G_MD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_XSL"] = mrsDj_item["G_XSL"];
                            }
                            break;
                        case "玻璃棉管壳":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"])
                            {
                                mitem["G_MD"] = mrsDj_item["G_MD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                mitem["G_GQPXD"] = mrsDj_item["G_GQPXD"];
                            }
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    mbHggs = 0;
                    int mcd, mdwz, hgs, length, cl;
                    bool sign, mark;
                    string CPMC, zpxs, bcz = "";
                    double md1, md2, xd1, xd2, md, pjmd, sum, cd, kd, hd, zl;
                    hgs = 0;
                    CPMC = sitem["CPMC"].Trim();
                    zpxs = sitem["ZPXS"].Trim();
                    sign = false;
                    mark = true;
                    //密度、抗压、冻融、吸水率
                    sign = true;
                    var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                    if (jcxm.Contains("、密度、"))
                    {
                        if (Conversion.Val(mitem["W_PJMD"]) == 0)
                        {
                            continue;
                        }
                        //判定计算了≤≥±＜＞～
                        if (zpxs != "板")
                        {
                            for (xd = 1; xd <= 4; xd++)
                            {
                                if (calc_PB(mitem["G_MD"], (100 * (Conversion.Val(mitem["W_MD" + xd]) - Conversion.Val(sitem["BCMD"])) / Conversion.Val(sitem["BCMD"])).ToString().Trim(), false) == "不合格")
                                    hgs = hgs + 1;
                            }
                            mitem["GH_MD"] = hgs <= 1 ? "合格" : "不合格";
                            length = mitem["G_MD"].Length;
                            cl = mitem["G_MD"].IndexOf("～") + 1;
                            md1 = Conversion.Val(mitem["G_MD"].Substring(0, cl - 1));
                            md2 = Conversion.Val(mitem["G_MD"].Substring(cl, length - cl));
                            md1 = Conversion.Val(sitem["BCMD"]) + Conversion.Val(sitem["BCMD"]) * md1 / 100;
                            md2 = Conversion.Val(sitem["BCMD"]) + Conversion.Val(sitem["BCMD"]) * md2 / 100;
                            mitem["G_MD"] = md1 + "～" + md2;
                        }
                        else
                            mitem["GH_MD"] = calc_PB(mitem["G_MD"], mitem["W_PJMD"], false);
                    }
                    else
                        sign = false;
                    if (!sign)
                    {
                        for (xd = 1; xd <= 4; xd++)
                            mitem["W_MD" + xd] = "----";
                        mitem["GH_MD"] = "----";
                        mitem["G_MDDZ"] = "----";
                        mitem["G_MDBC"] = "----";
                        mitem["G_MD"] = "----";
                        mitem["W_PJMD"] = "----";
                    }
                    sign = true;
                    mark = true;
                    if (jcxm.Contains("、热阻、"))
                    {
                        if (Conversion.Val(mitem["W_RZ"]) == 0)
                        {
                            continue;
                        }
                        if (sitem["BCRZ"].Trim() != "----" && sitem["BCRZ"].Trim() != "")
                        {
                            bcz = sitem["BCRZ"];
                            mitem["G_RZ"] = bcz;
                            mark = calc_PB(bcz, mitem["W_RZ"], false) == "合格" ? mark : false;
                        }
                        else
                            mark = calc_PB(mitem["G_RZ"], mitem["W_RZ"], false) == "合格" ? mark : false;
                        mitem["G_RZ"] = mitem["G_RZ"] + "（标准要求值）,且" + bcz + "标称值";
                        if (mark)
                            mitem["GH_RZ"] = "合格";
                        else
                            mitem["GH_RZ"] = "不合格";
                    }
                    else
                        sign = false;
                    if (!sign)
                    {
                        mitem["W_RZ"] = "----";
                        mitem["G_RZ"] = "----";
                        mitem["GH_RZ"] = "----";
                    }
                    if (jcxm.Contains("、导热系数、"))
                    {
                        if (Conversion.Val(mitem["W_DRXS"]) == 0)
                            continue;
                        //1-棉,棉,1|板,板,0|带,带,0|毡,毡,0|缝毡,缝毡,0|贴面毡,贴面毡,0|管壳,管壳,0
                        if (zpxs == "毯" && GetSafeDouble(sitem["BCMD"]) > 40)
                            mitem["G_DRXS"] = "≤0.043";
                        if (zpxs == "毯" && GetSafeDouble(sitem["BCMD"]) <= 40)
                            mitem["G_DRXS"] = "≤0.048";
                        if (sitem["BCDRXS"].Trim() != "----" && sitem["BCDRXS"].Trim() != "")
                        {
                            bcz = sitem["BCDRXS"];
                            mark = calc_PB(bcz, mitem["W_DRXS"], false) == "合格" ? mark : false;
                            mark = calc_PB(mitem["G_DRXS"], mitem["W_DRXS"], false) == "合格" ? mark : false;
                            mitem["W_DRXS1"] = mitem["W_DRXS"];
                            mcd = mitem["G_DRXS"].Length;
                            mdwz = mitem["G_DRXS"].IndexOf(".") + 1;
                            mcd = mcd - mdwz;
                            mitem["W_DRXS1"] = Round(Conversion.Val(mitem["W_DRXS"]), mcd).ToString();
                            mitem["G_DRXS"] = mitem["G_DRXS"] + "（标准要求值）,且" + bcz + "标称值";
                        }
                        else
                        {
                            mark = calc_PB(mitem["G_DRXS"], mitem["W_DRXS"], false) == "合格" ? mark : false;
                            mitem["W_DRXS1"] = mitem["W_DRXS"];
                            mcd = mitem["G_DRXS"].Length;
                            mdwz = mitem["G_DRXS"].IndexOf(".") + 1;
                            mcd = mcd - mdwz;
                            mitem["W_DRXS1"] = Round(Conversion.Val(mitem["W_DRXS"]), mcd).ToString();
                        }
                        if (mark)
                            mitem["GH_DRXS"] = "合格";
                        else
                            mitem["GH_DRXS"] = "不合格";
                    }
                    else
                        sign = false;
                    if (!sign)
                    {
                        mitem["W_DRXS"] = "----";
                        mitem["G_DRXS"] = "----";
                        mitem["GH_DRXS"] = "----";
                    }
                    sign = true;
                    mark = true;
                    if (jcxm.Contains("、压缩强度、"))
                    {
                        if (CPMC == "绝热用岩棉、矿渣棉及其制品(GB/T 11835-2007)" || CPMC == "建筑用岩棉、矿渣棉绝热制品(GB/T 19686-2005)")
                        {
                            md = Conversion.Val(mitem["W_PJMD"].Trim());
                            if (md <= 120)
                                mitem["G_YSQD"] = "≥10";
                            else if (md > 120 && md <= 160)
                                mitem["G_YSQD"] = "≥20";
                            else
                                mitem["G_YSQD"] = "≥40";
                        }
                        if (sitem["BCYSQD"].Trim() != "----")
                        {
                            bcz = sitem["BCYSQD"];
                            mark = calc_PB(bcz, mitem["W_YSQD"], false) == "合格" ? mark : false;
                            if (mitem["G_YSQD"] == "----" || mitem["G_YSQD"] == "")
                                mitem["G_YSQD"] = bcz + "标称值";
                            else
                            {
                                mark = calc_PB(mitem["G_YSQD"], mitem["W_YSQD"], false) == "合格" ? mark : false;
                                mitem["G_YSQD"] = mitem["G_YSQD"] + "（标准要求值）,且" + bcz + "标称值";
                            }
                        }
                        else
                            mark = calc_PB(mitem["G_YSQD"], mitem["W_YSQD"], false) == "合格" ? mark : false;
                        if (mark)
                            mitem["GH_YSQD"] = "合格";
                        else
                            mitem["GH_YSQD"] = "不合格";
                    }
                    else
                        sign = false;
                    if (!sign)
                    {
                        mitem["G_YSQD"] = "----";
                        mitem["GH_YSQD"] = "----";
                        mitem["W_YSQD"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、质量吸湿率、"))
                    {
                        sign = IsNumeric(mitem["W_ZLXSL"]) && !string.IsNullOrEmpty(mitem["W_ZLXSL"]) ? sign : false;
                        if (sign)
                            mitem["GH_ZLXSL"] = calc_PB(mitem["G_ZLXSL"], mitem["W_ZLXSL"], false);
                        else
                            continue;
                    }
                    else
                    {
                        mitem["W_ZLXSL"] = "----";
                        mitem["G_ZLXSL"] = "----";
                        mitem["GH_ZLXSL"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、尺寸稳定性、"))
                    {
                        sign = IsNumeric(mitem["W_CDWDX"]) && !string.IsNullOrEmpty(mitem["W_CDWDX"]) ? sign : false;
                        sign = IsNumeric(mitem["W_KDWDX"]) && !string.IsNullOrEmpty(mitem["W_KDWDX"]) ? sign : false;
                        sign = IsNumeric(mitem["W_HDWDX"]) && !string.IsNullOrEmpty(mitem["W_HDWDX"]) ? sign : false;
                        if (sign)
                        {
                            mitem["GH_CDWDX"] = calc_PB(mitem["G_CCWDX"], mitem["W_CDWDX"], false);
                            mitem["GH_KDWDX"] = calc_PB(mitem["G_CCWDX"], mitem["W_KDWDX"], false);
                            mitem["GH_HDWDX"] = calc_PB(mitem["G_CCWDX"], mitem["W_HDWDX"], false);
                        }
                        else
                            continue;
                    }
                    else
                    {
                        mitem["W_CDWDX"] = "----";
                        mitem["W_KDWDX"] = "----";
                        mitem["W_HDWDX"] = "----";
                        mitem["G_CCWDX"] = "----";
                        mitem["GH_CDWDX"] = "----";
                        mitem["GH_KDWDX"] = "----";
                        mitem["GH_HDWDX"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、短期吸水量、"))
                    {
                        sign = IsNumeric(mitem["W_DQXSL"]) && !string.IsNullOrEmpty(mitem["W_DQXSL"]) ? sign : false;
                        if (sign)
                            mitem["GH_DQXSL"] = calc_PB(mitem["G_DQXSL"], mitem["W_DQXSL"], false);
                        else
                            continue;
                    }
                    else
                    {
                        mitem["W_DQXSL"] = "----";
                        mitem["G_DQXSL"] = "----";
                        mitem["GH_DQXSL"] = "----";
                    }
                    sign = true;
                    mark = true;
                    if (jcxm.Contains("、长期吸水量、"))
                    {
                        sign = IsNumeric(mitem["W_CQXSL"]) && !string.IsNullOrEmpty(mitem["W_CQXSL"]) ? sign : false;
                        if (sign)
                        {
                            if (sitem["CQXSLZB"] != "----" && sitem["CQXSLZB"] != "")
                            {
                                bcz = sitem["CQXSLZB"];
                                mark = calc_PB(bcz, mitem["W_CQXSL"], false) == "合格" ? mark : false;
                                mark = calc_PB(mitem["G_CQXSL"], mitem["W_CQXSL"], false) == "合格" ? mark : false;
                                mitem["G_CQXSL"] = mitem["G_CQXSL"] + "（标准要求值）,且" + bcz + "标称值";
                            }
                            else
                                mark = calc_PB(mitem["G_CQXSL"], mitem["W_CQXSL"], false) == "合格" ? mark : false;
                            if (mark)
                                mitem["GH_CQXSL"] = "合格";
                            else
                                mitem["GH_CQXSL"] = "不合格";
                        }
                    }
                    else
                    {
                        mitem["W_CQXSL"] = "----";
                        mitem["G_CQXSL"] = "----";
                        mitem["GH_CQXSL"] = "----";
                    }
                    sign = true;
                    mark = true;
                    if (jcxm.Contains("、抗拉强度、"))
                    {
                        sign = IsNumeric(mitem["W_KLQD"]) && !string.IsNullOrEmpty(mitem["W_KLQD"]) ? sign : false;
                        if (sign)
                        {
                            if (sitem["BCKLQD"].Trim() != "----" && sitem["BCKLQD"].Trim() != "")
                            {
                                bcz = sitem["BCKLQD"];
                                mark = calc_PB(bcz, mitem["W_KLQD"], false) == "合格" ? mark : false;
                                mark = calc_PB(mitem["G_KLQD"], mitem["W_KLQD"], false) == "合格" ? mark : false;
                                mitem["G_KLQD"] = mitem["G_KLQD"] + "（标准要求值）,且" + bcz + "标称值";
                            }
                            else
                                mark = calc_PB(mitem["G_KLQD"], mitem["W_KLQD"], false) == "合格" ? mark : false;
                            if (mark)
                                mitem["GH_KLQD"] = "合格";
                            else
                                mitem["GH_KLQD"] = "不合格";
                        }
                        else
                            continue;
                    }
                    else
                    {
                        mitem["W_KLQD"] = "----";
                        mitem["G_KLQD"] = "----";
                        mitem["GH_KLQD"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、粘结强度、"))
                    {
                        sign = IsNumeric(mitem["W_NJQD"]) && !string.IsNullOrEmpty(mitem["W_NJQD"]) ? sign : false;
                        if (sign)
                        {
                            if (!mitem["G_NJQD"].Trim().Contains("≥"))
                                mitem["G_NJQD"] = "≥" + mitem["G_NJQD"];
                            mitem["GH_NJQD"] = calc_PB(mitem["G_NJQD"], mitem["W_NJQD"], false);
                        }
                        else
                            continue;
                    }
                    else
                    {
                        mitem["W_NJQD"] = "----";
                        mitem["G_NJQD"] = "----";
                        mitem["GH_NJQD"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、浸水粘结强度保留率、"))
                    {
                        sign = IsNumeric(mitem["W_JSBLL"]) && !string.IsNullOrEmpty(mitem["W_JSBLL"]) ? sign : false;
                        if (sign)
                            mitem["GH_JSBLL"] = calc_PB(mitem["G_JSBLL"], mitem["W_JSBLL"], false);
                        else
                            continue;
                    }
                    else
                    {
                        mitem["W_JSBLL"] = "----";
                        mitem["G_JSBLL"] = "----";
                        mitem["GH_JSBLL"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、管壳偏心度、"))
                    {
                        sign = IsNumeric(mitem["W_GQPXD"]) && !string.IsNullOrEmpty(mitem["W_GQPXD"]) ? sign : false;
                        if (sign)
                            mitem["GH_GQPXD"] = calc_PB(mitem["G_GQPXD"], mitem["W_GQPXD"], false);
                        else
                            continue;
                    }
                    else
                    {
                        mitem["W_GQPXD"] = "----";
                        mitem["G_GQPXD"] = "----";
                        mitem["GH_GQPXD"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、吸水率、") || jcxm.Contains("、吸水性、"))
                    {
                        if (sitem["XSXZB"].Trim().Contains("千克每立方"))
                        {
                            mdwz = sitem["XSXZB"].IndexOf("千克每立方") + 1;
                            mitem["G_XSL"] = sitem["XSXZB"].Substring(0, mdwz - 1);
                            sitem["XSXZBDW"] = "kg/m&scsup3&scend";
                            if (IsNumeric(mitem["W_XSL2"]) && !string.IsNullOrEmpty(mitem["W_XSL2"]))
                                mitem["W_XSL"] = mitem["W_XSL2"];
                        }
                        else if (sitem["XSXZB"].Contains("千克每平方"))
                        {
                            mdwz = sitem["XSXZB"].Trim().IndexOf("千克每平方") + 1;
                            mitem["G_XSL"] = sitem["XSXZB"].Substring(0, mdwz - 1);
                            sitem["XSXZBDW"] = "kg/m&scsup2&scend";
                            if (IsNumeric(mitem["W_XSL2"]) && !string.IsNullOrEmpty(mitem["W_XSL2"]))
                                mitem["W_XSL"] = mitem["W_XSL2"];
                        }
                        else if (sitem["XSXZB"].Contains("%"))
                        {
                            mdwz = sitem["XSXZB"].Trim().IndexOf("%") + 1;
                            mitem["G_XSL"] = sitem["XSXZB"].Substring(0, mdwz - 1);
                            sitem["XSXZBDW"] = "%";
                            if (IsNumeric(mitem["W_XSL1"]) && !string.IsNullOrEmpty(mitem["W_XSL1"]))
                                mitem["W_XSL"] = mitem["W_XSL1"];
                        }
                        sign = IsNumeric(mitem["W_XSL"]) && !string.IsNullOrEmpty(mitem["W_XSL"]) ? sign : false;
                        if (sign)
                        {
                            if (!mitem["G_XSL"].Trim().Contains("≤"))
                                mitem["G_XSL"] = "≤" + mitem["G_XSL"];
                            mitem["GH_XSL"] = calc_PB(mitem["G_XSL"], mitem["W_XSL"], false);
                        }
                        else
                            continue;
                    }
                    else
                    {
                        sitem["XSXZBDW"] = "----";
                        mitem["W_XSL"] = "----";
                        mitem["G_XSL"] = "----";
                        mitem["GH_XSL"] = "----";
                    }
                    mbHggs = mitem["GH_MD"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_DRXS"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_RSXN"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_YSQD"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_XSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_RZ"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_CDWDX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_KDWDX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_HDWDX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_ZLXSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_DQXSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_CQXSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_KLQD"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_NJQD"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_JSBLL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_GQPXD"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_HDPC"] == "不合格" ? mbHggs + 1 : mbHggs;
                    sitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                    mitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                    mitem["JCJGMS"] = mbHggs == 0 ? "该样品所检项目符合" + mitem["PDBZ"] + "标准要求。" : "该样品所检项目不符合" + mitem["PDBZ"] + "标准要求。";
                    mAllHg = (mAllHg && sitem["JCJG"] == "合格");
                    continue;
                }
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}