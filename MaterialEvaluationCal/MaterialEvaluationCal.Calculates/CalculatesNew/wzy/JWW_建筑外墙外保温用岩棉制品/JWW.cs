using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JWW : BaseMethods
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
            bool mFlag_Hg, mFlag_Bhg;
            mSFwc = true;
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

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_JWW_DJ"];
            var MItem = data["M_JWW"];
            var mitem = MItem[0];
            var SItem = data["S_JWW"];
            var mrsDrxs = data["ZM_DRJL"];
            #endregion

            #region  计算开始
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
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
                for (xd = 1; xd <= Gs; xd++)
                {
                    var mrsDj_item = mrsDj[xd - 1];
                    switch (sitem["CPMC"].Trim())
                    {
                        case "建筑外墙外保温用岩棉制品(GB/T 25975-2010)":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["LX"].Trim() == mrsDj_item["LX"])
                            {
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_CCWDX"] = mrsDj_item["G_CCWDX"];
                                mitem["G_CQXSL"] = mrsDj_item["G_CQXSL"];
                                mitem["G_DQXSL"] = mrsDj_item["G_DQXSL"];
                                mitem["G_GQPXD"] = mrsDj_item["G_GQPXD"];
                                mitem["G_KLQD"] = mrsDj_item["G_KLQD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_YSQD"] = mrsDj_item["G_YSQD"];
                            }
                            break;
                        case "建筑外墙外保温用岩棉制品(GB/T 25975-2018)":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["LX"].Trim() == mrsDj_item["LX"])
                            {
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_CCWDX"] = mrsDj_item["G_CCWDX"];
                                mitem["G_CQXSL"] = mrsDj_item["G_CQXSL"];
                                mitem["G_DQXSL"] = mrsDj_item["G_DQXSL"];
                                mitem["G_GQPXD"] = mrsDj_item["G_GQPXD"];
                                mitem["G_KLQD"] = mrsDj_item["G_KLQD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_YSQD"] = mrsDj_item["G_YSQD"];
                            }
                            break;
                        case "岩棉薄抹灰外墙外保温系统材料(JG/T 483-2015)":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"] && sitem["LX"].Trim() == mrsDj_item["LX"])
                            {
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_CCWDX"] = mrsDj_item["G_CCWDX"];
                                mitem["G_CQXSL"] = mrsDj_item["G_CQXSL"];
                                mitem["G_DQXSL"] = mrsDj_item["G_DQXSL"];
                                mitem["G_GQPXD"] = mrsDj_item["G_GQPXD"];
                                mitem["G_KLQD"] = mrsDj_item["G_KLQD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_YSQD"] = mrsDj_item["G_YSQD"];
                            }
                            break;
                    }

                }
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    mbHggs = 0;
                    int mcd, mdwz, hgs, length, cl;
                    bool sign, mark;
                    string CPMC, zpxs, bcz;
                    double md1, md2, xd1, xd2, md, pjmd, sum, cd, kd, hd, zl;
                    hgs = 0;
                    CPMC = sitem["CPMC"].Trim();
                    zpxs = sitem["ZPXS"].Trim();
                    sign = false;
                    mark = true;
                    //密度、抗压、冻融、吸水率
                    sign = true;
                    mark = true;
                    var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                    if (jcxm.Contains("、导热系数、"))
                    {
                        if (mitem["DEVCODE"].Contains("XCS17-067") || mitem["DEVCODE"].Contains("XCS17-066"))
                        {
                            var ZM_DRJL = mrsDrxs.FirstOrDefault(x => x["SYSJBRECID"].Equals(x["RECID"]));
                            //var ZM_DRJL = mrsDrxs.FirstOrDefault(u => u["SYLB"] == "JWW" && u["SYBH"] == mitem["JYDBH"]);
                            //var ZM_DRJL = mrsDrxs.FirstOrDefault();
                            mitem["W_DRXS"] = ZM_DRJL["DRXS"];
                            mitem["JCYJ "] = mitem["JCYJ"].Replace("10294", "10295");
                        }
                        //1-棉,棉,1|板,板,0|带,带,0|毡,毡,0|缝毡,缝毡,0|贴面毡,贴面毡,0|管壳,管壳,0
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
                        if (sitem["CPMC"].Contains("25975-2018"))
                        {
                            if (sitem["ZPXS"] == "岩棉板" && Conversion.Val(sitem["YPHD"]) < 50)
                                mitem["G_YSQD"] = "≥20";
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
                    }
                    else
                    {
                        mitem["W_ZLXSL"] = "----";
                        mitem["G_ZLXSL"] = "----";
                        mitem["GH_ZLXSL"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、体积吸水率、"))
                    {
                        mitem["G_XSL"] = "≤5";
                        sign = IsNumeric(mitem["W_XSL"]) && !string.IsNullOrEmpty(mitem["W_XSL"]) ? sign : false;
                        if (sign)
                            mitem["GH_XSL"] = calc_PB(mitem["G_XSL"], mitem["W_XSL"], false);
                    }
                    else
                    {
                        mitem["W_XSL"] = "----";
                        mitem["G_XSL"] = "----";
                        mitem["GH_XSL"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、密度、"))
                    {
                        sign = IsNumeric(mitem["W_PJMD"]) && !string.IsNullOrEmpty(mitem["W_PJMD"]) ? sign : false;
                        sign = IsNumeric(mitem["W_MDPC"]) && !string.IsNullOrEmpty(mitem["W_MDPC"]) ? sign : false;
                        if (sign)
                            mitem["GH_MD"] = calc_PB("-10～10", mitem["W_MDPC"], false);
                        mitem["G_MD"] = "-10%～10%";
                    }
                    else
                    {
                        mitem["W_PJMD"] = "----";
                        mitem["W_MDPC"] = "----";
                        mitem["G_MD"] = "----";
                        mitem["GH_MD"] = "----";
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
                    if (jcxm.Contains("短期吸水量"))
                    {
                        sign = IsNumeric(mitem["W_DQXSL"]) && !string.IsNullOrEmpty(mitem["W_DQXSL"]) ? sign : false;
                        if (sign)
                            mitem["GH_DQXSL"] = calc_PB(mitem["G_DQXSL"], mitem["W_DQXSL"], false);
                    }
                    else
                    {
                        mitem["W_DQXSL"] = "----";
                        mitem["G_DQXSL"] = "----";
                        mitem["GH_DQXSL"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、憎水率、"))
                    {
                        mitem["G_ZSL"] = "≥98.0";
                        sign = IsNumeric(mitem["W_ZSL"]) && !string.IsNullOrEmpty(mitem["W_ZSL"]) ? sign : false;
                        if (sign)
                            mitem["GH_ZSL"] = calc_PB(mitem["G_ZSL"], mitem["W_ZSL"], false);
                    }
                    else
                    {
                        mitem["W_ZSL"] = "----";
                        mitem["G_ZSL"] = "----";
                        mitem["GH_ZSL"] = "----";
                    }
                    sign = true;
                    mark = true;
                    if (jcxm.Contains("长期吸水量"))
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
                    if (jcxm.Contains("抗拉强度") || jcxm.Contains("垂直于表面的抗拉强度"))
                    {
                        sign = IsNumeric(mitem["W_KLQD"]) && !string.IsNullOrEmpty(mitem["W_KLQD"]) ? sign : false;
                        if (sign)
                        {
                            mark = calc_PB(mitem["G_KLQD"], mitem["W_KLQD"], false) == "合格" ? mark : false;
                            if (mark)
                                mitem["GH_KLQD"] = "合格";
                            else
                                mitem["GH_KLQD"] = "不合格";
                        }
                    }
                    else
                    {
                        mitem["W_KLQD"] = "----";
                        mitem["G_KLQD"] = "----";
                        mitem["GH_KLQD"] = "----";
                    }
                    mbHggs = mitem["GH_DRXS"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_YSQD"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_CDWDX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_KDWDX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_HDWDX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_ZLXSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_DQXSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_CQXSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_KLQD"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_ZSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    sitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                    mitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                    mitem["JCJGMS"] = mbHggs == 0 ? "该样品所检项目符合" + mitem["PDBZ"] + "标准要求。" : "该样品所检项目不符合" + mitem["PDBZ"] + "标准要求。";
                    if (mbHggs > 0 && (mitem["GH_DRXS"] == "合格" ||
                    mitem["GH_YSQD"] == "合格" ||
                    mitem["GH_CDWDX"] == "合格" ||
                    mitem["GH_KDWDX"] == "合格" ||
                    mitem["GH_HDWDX"] == "合格" ||
                    mitem["GH_ZLXSL"] == "合格" ||
                    mitem["GH_DQXSL"] == "合格" ||
                    mitem["GH_CQXSL"] == "合格" ||
                    mitem["GH_KLQD"] == "合格" ||
                    mitem["GH_ZSL"] == "合格"))
                        mitem["JCJGMS"] = "该样品所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                }
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
