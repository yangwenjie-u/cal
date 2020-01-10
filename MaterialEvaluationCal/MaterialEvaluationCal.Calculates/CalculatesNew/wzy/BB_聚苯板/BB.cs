using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class BB : BaseMethods
    {
        public void Calc()
        {
            #region

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

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_BB"];
            var BCRDJ = dataExtra["BZ_BB_DJ"];
            var YZSKB = dataExtra["BZ_YZSKB"];
            var MItem = data["M_BB"];
            var mrsDrxs = data["ZM_DRJL"];
            double zj1, zj2, mMj1, mMj2, mMj3, mMj4, mMj5, mKyqd1, mKyqd2, mKyqd3, mKyqd4, mKyqd5;
            int mbhggs = 0;
            bool mFlag_Bhg = false, mFlag_Hg = false, mSFwc = true;

            if (!data.ContainsKey("M_BB"))
            {
                data["M_BCR"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];

            foreach (var sItem in SItem)
            {
                bool flag = true;
                double md1, md2, sum, pjmd, md;
                int gs;
                string mJSFF = "";
                var mrsDj_item = BCRDJ.FirstOrDefault(x => x["MC"].Contains(sItem["CPMC"].Trim()) && x["LB"].Contains(sItem["XH"].Trim()));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    mItem["G_DRXS1"] = mrsDj_item["DRXS1"];
                    mItem["G_RZ1"] = mrsDj_item["RZ1"];
                    mItem["G_DRXS2"] = mrsDj_item["DRXS2"];
                    mItem["G_RZ2"] = mrsDj_item["RZ2"];
                    mItem["G_YSQD"] = mrsDj_item["YSQD"];
                    mItem["G_BGMD"] = mrsDj_item["BGMD"];
                    mItem["G_CCWDX"] = mrsDj_item["CCWDX"];
                    mItem["G_XSL"] = mrsDj_item["XSL"];
                    mItem["G_KLQD"] = mrsDj_item["KLQD"];
                    mItem["G_RSYZS"] = mrsDj_item["RSYZS"];
                    mItem["G_RSFJ"] = mrsDj_item["RSFJ"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_item["JSFF"]) ? "" : mrsDj_item["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "依据不详";
                    mItem["JCJGMS"] = mItem["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if (!string.IsNullOrEmpty(mItem["SJTABS"]))
                {
                    int mcd, mdwz;
                    mFlag_Hg = false;
                    mFlag_Bhg = false;
                    mbhggs = 0;
                    if (jcxm.Contains("、导热系数(10℃)、"))
                    {
                        mcd = mItem["G_DRXS1"].Length;
                        mdwz = mItem["G_DRXS1"].IndexOf(".") + 1;
                        mcd = mcd - mdwz + 1;
                        if (mItem["DEVCODE"].Contains("XCS17-067") || mItem["DEVCODE"].Contains("XCS17-066"))
                        {
                            //var ZM_DRJL = mrsDrxs.FirstOrDefault(x => x["SYSJBRECID"].Equals(x["RECID"]));
                            //var ZM_DRJL = mrsDrxs.FirstOrDefault(u => u["SYLB"] == "QN" && u["SYBH"] == mitem["JYDBH"]);
                            var ZM_DRJL = mrsDrxs.FirstOrDefault();
                            sItem["DRXS"] = ZM_DRJL["DRXS"];
                        }
                        sItem["DRXS"] = Round(Conversion.Val(sItem["DRXS"]), mcd).ToString();
                        sItem["DRXS1"] = sItem["DRXS"];
                        if (calc_pd(mItem["G_DRXS1"], sItem["DRXS1"]) == "符合")
                        {
                            mItem["HG_DRXS1"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_DRXS1"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["DRXS1"] = "----";
                        mItem["HG_DRXS1"] = "----";
                        mItem["G_DRXS1"] = "----";
                    }
                    if (jcxm.Contains("、导热系数(25℃)、"))
                    {
                        sItem["DRXS2"] = sItem["DRXS"];
                        mcd = mItem["G_DRXS2"].Length;
                        mdwz = mItem["G_DRXS2"].IndexOf(".") + 1;
                        mcd = mcd - mdwz + 1;
                        if (mItem["DEVCODE"].Contains("XCS17-067") || mItem["DEVCODE"].Contains("XCS17-066"))
                        {
                            //var ZM_DRJL = mrsDrxs.FirstOrDefault(x => x["SYSJBRECID"].Equals(x["RECID"]));
                            //var ZM_DRJL = mrsDrxs.FirstOrDefault(u => u["SYLB"] == "QN" && u["SYBH"] == mitem["JYDBH"]);
                            var ZM_DRJL = mrsDrxs.FirstOrDefault();
                            sItem["DRXS2"] = ZM_DRJL["DRXS"];
                        }
                        sItem["DRXS2"] = Round(Conversion.Val(sItem["DRXS2"]), mcd).ToString();
                        if (calc_pd(mItem["G_DRXS2"], sItem["DRXS2"]) == "符合")
                        {
                            mItem["HG_DRXS2"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_DRXS2"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["DRXS2"] = "----";
                        mItem["HG_DRXS2"] = "----";
                        mItem["G_DRXS2"] = "----";
                    }

                    if (jcxm.Contains("、尺寸稳定性、"))
                    {
                        if (calc_pd(mItem["G_CCWDX"], sItem["CCWDXC"]) == "符合")
                        {
                            mItem["HG_CCWDXC"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_CCWDXC"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        if (calc_pd(mItem["G_CCWDX"], sItem["CCWDXK"]) == "符合")
                        {
                            mItem["HG_CCWDXK"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_CCWDXK"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        if (calc_pd(mItem["G_CCWDX"], sItem["CCWDXH"]) == "符合")
                        {
                            mItem["HG_CCWDXH"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_CCWDXH"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["CCWDXC"] = "----";
                        sItem["CCWDXK"] = "----";
                        sItem["CCWDXH"] = "----";
                        mItem["HG_CCWDXC"] = "----";
                        mItem["HG_CCWDXK"] = "----";
                        mItem["HG_CCWDXH"] = "----";
                        mItem["G_CCWDX"] = "----";
                    }
                    if (jcxm.Contains("、表观密度、"))
                    {
                        if (calc_pd(mItem["G_BGMD"], sItem["BGMD"]) == "符合")
                        {
                            mItem["HG_BGMD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_BGMD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["BGMD"] = "----";
                        mItem["HG_BGMD"] = "----";
                        mItem["G_BGMD"] = "----";
                    }
                    if (jcxm.Contains("、抗拉强度、"))
                    {

                        if (calc_pd(mItem["G_KLQD"], sItem["KLQD"]) == "符合")
                        {
                            mItem["HG_KLQD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_KLQD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["KLQD"] = "----";
                        mItem["HG_KLQD"] = "----";
                        mItem["G_KLQD"] = "----";
                    }
                    if (jcxm.Contains("、热阻(10℃)、") && jcxm.Contains("、热阻(25℃)、"))
                    {
                        if (calc_pd(mItem["G_RZ1"], sItem["RZ1"]) == "符合")
                        {
                            mItem["HG_RZ1"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_RZ1"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        if (calc_pd(mItem["G_RZ2"], sItem["RZ2"]) == "符合")
                        {
                            mItem["HG_RZ2"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_RZ2"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["RZ1"] = "----";
                        sItem["RZ2"] = "----";
                        mItem["HG_RZ1"] = "----";
                        mItem["HG_RZ2"] = "----";
                        mItem["G_RZ1"] = "----";
                        mItem["G_RZ2"] = "----";
                    }
                    if (jcxm.Contains("、压缩强度、"))
                    {
                        if (calc_pd(mItem["G_YSQD"], sItem["YSQD"]) == "符合")
                        {
                            mItem["HG_YSQD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_YSQD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["YSQD"] = "----";
                        mItem["HG_YSQD"] = "----";
                        mItem["G_YSQD"] = "----";
                    }
                    if (jcxm.Contains("、吸水率、"))
                    {
                        if (calc_pd(mItem["G_XSL"], sItem["XSL"]) == "符合")
                        {
                            mItem["HG_XSL"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_XSL"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["XSL"] = "----";
                        mItem["HG_XSL"] = "----";
                        mItem["G_XSL"] = "----";
                    }
                    if (jcxm.Contains("、燃烧氧指数、") || jcxm.Contains("、氧指数、"))
                    {
                        sItem["YZSZXZ"] = "30";
                        if (sItem["YZSSYFF"].Trim() == "最小值法")
                        {
                            mItem["G_RSYZS"] = "≥" + sItem["YZSZXZ"];
                            if (mItem["HG_RSYZS"] == "不合格")
                            {
                                sItem["RSYZS"] = "低于" + sItem["YZSZXZ"] + "%";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                            {
                                sItem["RSYZS"] = "不低于" + sItem["YZSZXZ"] + "%";
                                mFlag_Hg = true;
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(sItem["RSNTYND1"]) && !string.IsNullOrEmpty(mItem["HG_RSYZS"]))
                            {
                                if (mItem["HG_RSYZS"] == "不合格")
                                {
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                if (calc_pd(mItem["G_RSYZS"], sItem["RSYZS"]) == "符合")
                                {
                                    mItem["HG_RSYZS"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    mItem["HG_RSYZS"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        sItem["RSYSM"] = "----";
                        sItem["RSYZS"] = "----";
                        mItem["HG_RSYZS"] = "----";
                        mItem["G_RSYZS"] = "----";
                    }
                    if (jcxm.Contains("、燃烧分级、") || jcxm.Contains("、燃烧等级E级、"))
                    {
                        if (sItem["RSFJ"] == "符合")
                        {
                            mItem["HG_RSFJ"] = "合格";
                            sItem["RSFJJG"] = "符合E级";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_RSFJ"] = "不合格";
                            sItem["RSFJJG"] = "不符合";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["RSFJ"] = "----";
                        sItem["RSFJJG"] = "----";
                        mItem["HG_RSFJ"] = "----";
                        mItem["G_RSFJ"] = "----";
                    }
                    mItem["JCJGMS"] = "";
                    if (mbhggs == 0)
                    {
                        jsbeizhu = "该组试样所检项目符合" + mItem["PDBZ"] + "标准要求。";
                        sItem["JCJG"] = "合格";
                    }
                    if (mbhggs >= 1)
                    {
                        jsbeizhu = "该组试样不符合" + mItem["PDBZ"] + "标准要求。";
                        sItem["JCJG"] = "不合格";
                        if (mFlag_Bhg && mFlag_Hg)
                            jsbeizhu = "该组试样所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                    }
                    mAllHg = (mAllHg && sItem["JCJG"] == "合格");
                    continue;
                }

                if (jcxm.Contains("、导热系数(10℃)、"))
                {
                    string drxs1 = calc_PB(mItem["G_DRXS1"], sItem["DRXS1"], true);
                    if (drxs1 == "符合")
                    {
                        mItem["HG_DRXS1"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_DRXS1"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["DRXS1"] = "----";
                    mItem["HG_DRXS1"] = "----";
                    mItem["G_DRXS1"] = "----";
                }

                if (jcxm.Contains("、导热系数(25℃)、"))
                {
                    string drxs1 = calc_PB(mItem["G_DRXS2"], sItem["DRXS2"], true);
                    if (drxs1 == "符合")
                    {
                        mItem["HG_DRXS2"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_DRXS2"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["DRXS2"] = "----";
                    mItem["HG_DRXS2"] = "----";
                    mItem["G_DRXS2"] = "----";
                }

                if (jcxm.Contains("、尺寸稳定性、"))
                {
                    if (double.Parse(sItem["CCWDXCQ1_1"]) != 0)
                    {
                        sItem["CCWDXCQ1"] = Round((double.Parse(sItem["CCWDXCQ1_1"]) + double.Parse(sItem["CCWDXCQ1_2"]) + double.Parse(sItem["CCWDXCQ1_3"])) / 3, 1).ToString();
                        sItem["CCWDXCQ2"] = Round((double.Parse(sItem["CCWDXCQ2_1"]) + double.Parse(sItem["CCWDXCQ2_2"]) + double.Parse(sItem["CCWDXCQ2_3"])) / 3, 1).ToString();
                        sItem["CCWDXCQ3"] = Round((double.Parse(sItem["CCWDXCQ3_1"]) + double.Parse(sItem["CCWDXCQ3_2"]) + double.Parse(sItem["CCWDXCQ3_3"])) / 3, 1).ToString();
                        sItem["CCWDXCH1"] = Round((double.Parse(sItem["CCWDXCH1_1"]) + double.Parse(sItem["CCWDXCH1_2"]) + double.Parse(sItem["CCWDXCH1_3"])) / 3, 1).ToString();
                        sItem["CCWDXCH2"] = Round((double.Parse(sItem["CCWDXCH2_1"]) + double.Parse(sItem["CCWDXCH2_2"]) + double.Parse(sItem["CCWDXCH2_3"])) / 3, 1).ToString();
                        sItem["CCWDXCH3"] = Round((double.Parse(sItem["CCWDXCH3_1"]) + double.Parse(sItem["CCWDXCH3_2"]) + double.Parse(sItem["CCWDXCH3_3"])) / 3, 1).ToString();
                    }
                    for (int i = 1; i <= 3; i++)
                    {
                        sItem["CCWDXCQ" + i] = Round(double.Parse(sItem["CCWDXCQ" + i]), 1).ToString();
                        sItem["CCWDXCH" + i] = Round(double.Parse(sItem["CCWDXCH" + i]), 1).ToString();
                    }

                    if (double.Parse(sItem["CCWDXKQ1_1"]) != 0)
                    {
                        sItem["CCWDXKQ1"] = Round((double.Parse(sItem["CCWDXKQ1_1"]) + double.Parse(sItem["CCWDXKQ1_2"]) + double.Parse(sItem["CCWDXKQ1_3"])) / 3, 1).ToString();
                        sItem["CCWDXKQ2"] = Round((double.Parse(sItem["CCWDXKQ2_1"]) + double.Parse(sItem["CCWDXKQ2_2"]) + double.Parse(sItem["CCWDXKQ2_3"])) / 3, 1).ToString();
                        sItem["CCWDXKQ3"] = Round((double.Parse(sItem["CCWDXKQ3_1"]) + double.Parse(sItem["CCWDXKQ3_2"]) + double.Parse(sItem["CCWDXKQ3_3"])) / 3, 1).ToString();
                        sItem["CCWDXKH1"] = Round((double.Parse(sItem["CCWDXKH1_1"]) + double.Parse(sItem["CCWDXKH1_2"]) + double.Parse(sItem["CCWDXKH1_3"])) / 3, 1).ToString();
                        sItem["CCWDXKH2"] = Round((double.Parse(sItem["CCWDXCH2_1"]) + double.Parse(sItem["CCWDXKH2_2"]) + double.Parse(sItem["CCWDXKH2_3"])) / 3, 1).ToString();
                        sItem["CCWDXKH3"] = Round((double.Parse(sItem["CCWDXKH3_1"]) + double.Parse(sItem["CCWDXKH3_2"]) + double.Parse(sItem["CCWDXKH3_3"])) / 3, 1).ToString();
                    }
                    for (int i = 1; i <= 3; i++)
                    {
                        sItem["CCWDXKQ" + i] = Round(double.Parse(sItem["CCWDXKQ" + i]), 1).ToString();
                        sItem["CCWDXKH" + i] = Round(double.Parse(sItem["CCWDXKH" + i]), 1).ToString();
                    }

                    if (double.Parse(sItem["CCWDXHQ1_1"]) != 0)
                    {
                        sItem["CCWDXHQ1"] = Round((double.Parse(sItem["CCWDXHQ1_1"]) + double.Parse(sItem["CCWDXHQ1_2"]) + double.Parse(sItem["CCWDXHQ1_3"]) + double.Parse(sItem["CCWDXHQ1_4"]) + double.Parse(sItem["CCWDXHQ1_5"])) / 5, 1).ToString();
                        sItem["CCWDXHQ2"] = Round((double.Parse(sItem["CCWDXHQ2_1"]) + double.Parse(sItem["CCWDXHQ2_2"]) + double.Parse(sItem["CCWDXHQ2_3"]) + double.Parse(sItem["CCWDXHQ2_4"]) + double.Parse(sItem["CCWDXHQ2_5"])) / 5, 1).ToString();
                        sItem["CCWDXHQ3"] = Round((double.Parse(sItem["CCWDXHQ3_1"]) + double.Parse(sItem["CCWDXHQ3_2"]) + double.Parse(sItem["CCWDXHQ3_3"]) + double.Parse(sItem["CCWDXHQ3_4"]) + double.Parse(sItem["CCWDXHQ3_5"])) / 5, 1).ToString();
                        sItem["CCWDXHH1"] = Round((double.Parse(sItem["CCWDXHH1_1"]) + double.Parse(sItem["CCWDXHH1_2"]) + double.Parse(sItem["CCWDXHH1_3"]) + double.Parse(sItem["CCWDXHH1_4"]) + double.Parse(sItem["CCWDXHH1_5"])) / 5, 1).ToString();
                        sItem["CCWDXHH2"] = Round((double.Parse(sItem["CCWDXHH2_1"]) + double.Parse(sItem["CCWDXHH2_2"]) + double.Parse(sItem["CCWDXHH2_3"]) + double.Parse(sItem["CCWDXHH2_4"]) + double.Parse(sItem["CCWDXHH2_5"])) / 5, 1).ToString();
                        sItem["CCWDXHH3"] = Round((double.Parse(sItem["CCWDXHH3_1"]) + double.Parse(sItem["CCWDXHH3_2"]) + double.Parse(sItem["CCWDXHH3_3"]) + double.Parse(sItem["CCWDXHH3_4"]) + double.Parse(sItem["CCWDXHH3_5"])) / 5, 1).ToString();
                    }
                    for (int i = 1; i <= 3; i++)
                    {
                        sItem["CCWDXHQ" + i] = Round(double.Parse(sItem["CCWDXHQ" + i]), 1).ToString();
                        sItem["CCWDXHH" + i] = Round(double.Parse(sItem["CCWDXHH" + i]), 1).ToString();
                    }

                    double mcbhl1, mcbhl2, mcbhl3;
                    mcbhl1 = Round((double.Parse(sItem["CCWDXCQ1"]) - double.Parse(sItem["CCWDXCH1"])) / double.Parse(sItem["CCWDXCQ1"]) * 100, 1);
                    mcbhl2 = Round((double.Parse(sItem["CCWDXCQ2"]) - double.Parse(sItem["CCWDXCH2"])) / double.Parse(sItem["CCWDXCQ2"]) * 100, 1);
                    mcbhl3 = Round((double.Parse(sItem["CCWDXCQ3"]) - double.Parse(sItem["CCWDXCH3"])) / double.Parse(sItem["CCWDXCQ3"]) * 100, 1);
                    sItem["CCWDXC"] = Round((mcbhl1 + mcbhl2 + mcbhl3) / 3, 1).ToString();

                    mcbhl1 = Round((double.Parse(sItem["CCWDXKQ1"]) - double.Parse(sItem["CCWDXKH1"])) / double.Parse(sItem["CCWDXKQ1"]) * 100, 1);
                    mcbhl2 = Round((double.Parse(sItem["CCWDXKQ2"]) - double.Parse(sItem["CCWDXKH2"])) / double.Parse(sItem["CCWDXKQ2"]) * 100, 1);
                    mcbhl3 = Round((double.Parse(sItem["CCWDXKQ3"]) - double.Parse(sItem["CCWDXKH3"])) / double.Parse(sItem["CCWDXKQ3"]) * 100, 1);
                    sItem["CCWDXK"] = Round((mcbhl1 + mcbhl2 + mcbhl3) / 3, 1).ToString();

                    mcbhl1 = Round((double.Parse(sItem["CCWDXHQ1"]) - double.Parse(sItem["CCWDXHH1"])) / double.Parse(sItem["CCWDXHQ1"]) * 100, 1);
                    mcbhl2 = Round((double.Parse(sItem["CCWDXHQ2"]) - double.Parse(sItem["CCWDXHH2"])) / double.Parse(sItem["CCWDXHQ2"]) * 100, 1);
                    mcbhl3 = Round((double.Parse(sItem["CCWDXHQ3"]) - double.Parse(sItem["CCWDXHH3"])) / double.Parse(sItem["CCWDXHQ3"]) * 100, 1);
                    sItem["CCWDXH"] = Round((mcbhl1 + mcbhl2 + mcbhl3) / 3, 1).ToString();

                    if ("符合" == calc_PB(mItem["G_CCWDX"], sItem["CCWDXC"], true))
                    {
                        mItem["HG_CCWDXC"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_CCWDXC"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }

                    if ("符合" == calc_PB(mItem["G_CCWDX"], sItem["CCWDXK"], true))
                    {
                        mItem["HG_CCWDXK"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_CCWDXK"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }

                    if ("符合" == calc_PB(mItem["G_CCWDX"], sItem["CCWDXH"], true))
                    {
                        mItem["HG_CCWDXH"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_CCWDXH"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["CCWDXC"] = "----";
                    sItem["CCWDXK"] = "----";
                    sItem["CCWDXH"] = "----";
                    mItem["HG_CCWDXC"] = "----";
                    mItem["HG_CCWDXK"] = "----";
                    mItem["HG_CCWDXH"] = "----";
                    mItem["G_CCWDX"] = "----";
                }

                if (jcxm.Contains("、表观密度、"))
                {
                    double mcd1, mkd1, mhd1, bgmdv1;
                    mcd1 = Round((double.Parse(sItem["BGMDC1_1"]) + double.Parse(sItem["BGMDC1_2"]) + double.Parse(sItem["BGMDC1_3"])) / 3, 1);
                    mkd1 = Round((double.Parse(sItem["BGMDK1_1"]) + double.Parse(sItem["BGMDK1_2"]) + double.Parse(sItem["BGMDK1_3"])) / 3, 1);
                    mhd1 = Round((double.Parse(sItem["BGMDH1_1"]) + double.Parse(sItem["BGMDH1_2"]) + double.Parse(sItem["BGMDH1_3"])) / 3, 1);
                    //if (mcd1 != 0 && 0 >= double.Parse(sItem["BGMDC1"]))
                    if (mcd1 != 0 && !(double.Parse(sItem["BGMDC1"]) > 0))
                    {
                        sItem["BGMDC1"] = Round(mcd1, 1).ToString();
                        sItem["BGMDK1"] = Round(mkd1, 1).ToString();
                        sItem["BGMDH1"] = Round(mhd1, 1).ToString();
                    }
                    double mcd2, mkd2, mhd2, bgmdv2;
                    bgmdv1 = double.Parse(sItem["BGMDC1"]) * double.Parse(sItem["BGMDK1"]) * double.Parse(sItem["BGMDH1"]);
                    mcd2 = Round((double.Parse(sItem["BGMDC2_1"]) + double.Parse(sItem["BGMDC2_2"]) + double.Parse(sItem["BGMDC2_3"])) / 3, 1);
                    mkd2 = Round((double.Parse(sItem["BGMDK2_1"]) + double.Parse(sItem["BGMDK2_2"]) + double.Parse(sItem["BGMDK2_3"])) / 3, 1);
                    mhd2 = Round((double.Parse(sItem["BGMDH2_1"]) + double.Parse(sItem["BGMDH2_2"]) + double.Parse(sItem["BGMDH2_3"])) / 3, 1);
                    //if (mcd2 != 0 && 0 >= double.Parse(sItem["BGMDC2"]))
                    if (mcd2 != 0 && !(double.Parse(sItem["BGMDC2"]) > 0))
                    {
                        sItem["BGMDC2"] = Round(mcd2, 1).ToString();
                        sItem["BGMDK2"] = Round(mkd2, 1).ToString();
                        sItem["BGMDH2"] = Round(mhd2, 1).ToString();
                    }
                    bgmdv2 = double.Parse(sItem["BGMDC2"]) * double.Parse(sItem["BGMDK2"]) * double.Parse(sItem["BGMDH2"]);

                    double mcd3, mkd3, mhd3, bgmdv3;
                    mcd3 = Round((double.Parse(sItem["BGMDC3_1"]) + double.Parse(sItem["BGMDC3_2"]) + double.Parse(sItem["BGMDC3_3"])) / 3, 1);
                    mkd3 = Round((double.Parse(sItem["BGMDK3_1"]) + double.Parse(sItem["BGMDK3_2"]) + double.Parse(sItem["BGMDK3_3"])) / 3, 1);
                    mhd3 = Round((double.Parse(sItem["BGMDH3_1"]) + double.Parse(sItem["BGMDH3_2"]) + double.Parse(sItem["BGMDH3_3"])) / 3, 1);
                    if (mcd2 != 0 && !(double.Parse(sItem["BGMDC3"]) > 0))
                    {
                        sItem["BGMDC3"] = Round(mcd3, 1).ToString();
                        sItem["BGMDK3"] = Round(mkd3, 1).ToString();
                        sItem["BGMDH3"] = Round(mhd3, 1).ToString();
                    }
                    bgmdv3 = double.Parse(sItem["BGMDC3"]) * double.Parse(sItem["BGMDK3"]) * double.Parse(sItem["BGMDH3"]);

                    double mcd4, mkd4, mhd4, bgmdv4;
                    mcd4 = Round((double.Parse(sItem["BGMDC4_1"]) + double.Parse(sItem["BGMDC4_2"]) + double.Parse(sItem["BGMDC4_3"])) / 3, 1);
                    mkd4 = Round((double.Parse(sItem["BGMDK4_1"]) + double.Parse(sItem["BGMDK4_2"]) + double.Parse(sItem["BGMDK4_3"])) / 3, 1);
                    mhd4 = Round((double.Parse(sItem["BGMDH4_1"]) + double.Parse(sItem["BGMDH4_2"]) + double.Parse(sItem["BGMDH4_3"])) / 3, 1);
                    if (mcd2 != 0 && !(double.Parse(sItem["BGMDC4"]) > 0))
                    {
                        sItem["BGMDC4"] = Round(mcd4, 1).ToString();
                        sItem["BGMDK4"] = Round(mkd4, 1).ToString();
                        sItem["BGMDH4"] = Round(mhd4, 1).ToString();
                    }
                    bgmdv4 = double.Parse(sItem["BGMDC4"]) * double.Parse(sItem["BGMDK4"]) * double.Parse(sItem["BGMDH4"]);

                    double mcd5, mkd5, mhd5, bgmdv5;
                    mcd5 = Round((double.Parse(sItem["BGMDC5_1"]) + double.Parse(sItem["BGMDC5_2"]) + double.Parse(sItem["BGMDC5_3"])) / 3, 1);
                    mkd5 = Round((double.Parse(sItem["BGMDK5_1"]) + double.Parse(sItem["BGMDK5_2"]) + double.Parse(sItem["BGMDK5_3"])) / 3, 1);
                    mhd5 = Round((double.Parse(sItem["BGMDH5_1"]) + double.Parse(sItem["BGMDH5_2"]) + double.Parse(sItem["BGMDH5_3"])) / 3, 1);
                    if (mcd2 != 0 && !(double.Parse(sItem["BGMDC5"]) > 0))
                    {
                        sItem["BGMDC5"] = Round(mcd5, 1).ToString();
                        sItem["BGMDK5"] = Round(mkd5, 1).ToString();
                        sItem["BGMDH5"] = Round(mhd5, 1).ToString();
                    }
                    bgmdv5 = double.Parse(sItem["BGMDC5"]) * double.Parse(sItem["BGMDK5"]) * double.Parse(sItem["BGMDH5"]);

                    if (bgmdv1 * bgmdv2 != 0 && bgmdv3 * bgmdv4 * bgmdv5 != 0)
                    {
                        sItem["BGMD1"] = Round(double.Parse(sItem["BGMDM1"]) / bgmdv1 * 1000000, 4).ToString();
                        sItem["BGMD2"] = Round(double.Parse(sItem["BGMDM2"]) / bgmdv2 * 1000000, 4).ToString();
                        sItem["BGMD3"] = Round(double.Parse(sItem["BGMDM3"]) / bgmdv3 * 1000000, 4).ToString();
                        sItem["BGMD4"] = Round(double.Parse(sItem["BGMDM4"]) / bgmdv4 * 1000000, 4).ToString();
                        sItem["BGMD5"] = Round(double.Parse(sItem["BGMDM5"]) / bgmdv5 * 1000000, 4).ToString();
                    }
                    if (23 == double.Parse(sItem["BGMDWD"]))
                    {
                        if (!string.IsNullOrEmpty(sItem["BGMD1"]))
                        {
                            if (15 > double.Parse(sItem["BGMD1"]))
                            {
                                sItem["BGMD1"] = Round(double.Parse(sItem["BGMD1"]) + 1.22, 4).ToString();
                            }
                        }

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sItem["BGMD1"]))
                        {
                            if (15 > double.Parse(sItem["BGMD1"]))
                            {
                                sItem["BGMD1"] = Round(double.Parse(sItem["BGMD1"]) + 1.1955, 4).ToString();
                            }
                        }
                    }

                    if (23 == double.Parse(sItem["BGMDWD"]))
                    {
                        if (15 > double.Parse(sItem["BGMD2"]) && !string.IsNullOrEmpty(sItem["BGMD2"]))
                        {
                            sItem["BGMD2"] = Round(double.Parse(sItem["BGMD2"]) + 1.22, 4).ToString();
                        }
                    }
                    else
                    {
                        if (15 > double.Parse(sItem["BGMD2"]) && !string.IsNullOrEmpty(sItem["BGMD2"]))
                        {
                            sItem["BGMD2"] = Round(double.Parse(sItem["BGMD2"]) + 1.1955, 4).ToString();
                        }
                    }

                    if (23 == double.Parse(sItem["BGMDWD"]))
                    {
                        if (15 > double.Parse(sItem["BGMD3"]) && !string.IsNullOrEmpty(sItem["BGMD3"]))
                        {
                            sItem["BGMD3"] = Round(double.Parse(sItem["BGMD3"]) + 1.22, 4).ToString();
                        }
                    }
                    else
                    {
                        if (15 > double.Parse(sItem["BGMD3"]) && !string.IsNullOrEmpty(sItem["BGMD3"]))
                        {
                            sItem["BGMD3"] = Round(double.Parse(sItem["BGMD3"]) + 1.1955, 4).ToString();
                        }
                    }

                    if (23 == double.Parse(sItem["BGMDWD"]))
                    {
                        if (15 > double.Parse(sItem["BGMD4"]) && !string.IsNullOrEmpty(sItem["BGMD4"]))
                        {
                            sItem["BGMD4"] = Round(double.Parse(sItem["BGMD4"]) + 1.22, 4).ToString();
                        }
                    }
                    else
                    {
                        if (15 > double.Parse(sItem["BGMD4"]) && !string.IsNullOrEmpty(sItem["BGMD4"]))
                        {
                            sItem["BGMD4"] = Round(double.Parse(sItem["BGMD4"]) + 1.1955, 4).ToString();
                        }
                    }

                    if (23 == double.Parse(sItem["BGMDWD"]))
                    {
                        if (15 > double.Parse(sItem["BGMD5"]) && !string.IsNullOrEmpty(sItem["BGMD5"]))
                        {
                            sItem["BGMD5"] = Round(double.Parse(sItem["BGMD5"]) + 1.22, 4).ToString();
                        }
                    }
                    else
                    {
                        if (15 > double.Parse(sItem["BGMD5"]) && !string.IsNullOrEmpty(sItem["BGMD5"]))
                        {
                            sItem["BGMD5"] = Round(double.Parse(sItem["BGMD5"]) + 1.1955, 4).ToString();
                        }
                    }

                    sItem["BGMD"] = Round((double.Parse(sItem["BGMD1"]) + double.Parse(sItem["BGMD2"]) + double.Parse(sItem["BGMD3"]) + double.Parse(sItem["BGMD4"]) + double.Parse(sItem["BGMD5"])) / 5, 1).ToString();

                    string bgmd = calc_PB(mItem["G_BGMD"], sItem["BGMD"], true);
                    if (bgmd == "符合")
                    {
                        mItem["HG_BGMD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_BGMD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["BGMD"] = "----";
                    mItem["HG_BGMD"] = "----";
                    mItem["G_BGMD"] = "----";
                }

                if (jcxm.Contains("、抗拉强度、"))
                {
                    double KLMJ1, KLMJ2, KLMJ3, KLMJ4, KLMJ5;
                    KLMJ1 = double.Parse(sItem["KLC1"]) * double.Parse(sItem["KLK1"]);
                    KLMJ2 = double.Parse(sItem["KLC2"]) * double.Parse(sItem["KLK2"]);
                    KLMJ3 = double.Parse(sItem["KLC3"]) * double.Parse(sItem["KLK3"]);
                    KLMJ4 = double.Parse(sItem["KLC4"]) * double.Parse(sItem["KLK4"]);
                    KLMJ5 = double.Parse(sItem["KLC5"]) * double.Parse(sItem["KLK5"]);
                    sItem["KLQD1"] = Round(double.Parse(sItem["KLYZ1"]) / KLMJ1, 2).ToString();
                    sItem["KLQD2"] = Round(double.Parse(sItem["KLYZ2"]) / KLMJ2, 2).ToString();
                    sItem["KLQD3"] = Round(double.Parse(sItem["KLYZ3"]) / KLMJ3, 2).ToString();
                    sItem["KLQD4"] = Round(double.Parse(sItem["KLYZ4"]) / KLMJ4, 2).ToString();
                    sItem["KLQD5"] = Round(double.Parse(sItem["KLYZ5"]) / KLMJ5, 2).ToString();
                    sItem["KLQD"] = Round(double.Parse(sItem["KLQD1"]) + double.Parse(sItem["KLQD2"]) + double.Parse(sItem["KLQD3"]) + double.Parse(sItem["KLQD4"]) + double.Parse(sItem["KLQD5"]) / 5, 2).ToString();

                    string klqd = calc_PB(mItem["G_KLQD"], sItem["KLQD"], true);
                    if (klqd == "符合")
                    {
                        mItem["HG_KLQD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_KLQD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["KLQD"] = "----";
                    mItem["HG_KLQD"] = "----";
                    mItem["G_KLQD"] = "----";
                }

                if (jcxm.Contains("、热阻(10℃)、") && jcxm.Contains("、热阻(25℃)、"))
                {
                    string rz1 = calc_PB(mItem["G_RZ1"], sItem["RZ1"], true);
                    if (rz1 == "符合")
                    {
                        mItem["HG_RZ1"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_RZ1"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Hg = true;
                    }
                    string rz2 = calc_PB(mItem["G_RZ2"], sItem["RZ2"], true);
                    if (rz1 == "符合")
                    {
                        mItem["HG_RZ2"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_RZ2"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["RZ1"] = "----";
                    sItem["RZ2"] = "----";
                    mItem["HG_RZ1"] = "----";
                    mItem["HG_RZ2"] = "----";
                    mItem["G_RZ1"] = "----";
                    mItem["G_RZ2"] = "----";
                }

                if (jcxm.Contains("、压缩强度、"))
                {
                    double mcd1, mkd1, mcd2, mkd2, mcd3, mkd3, mcd4, mkd4, mcd5, mkd5;
                    mcd1 = Round((double.Parse(sItem["BGMDC1_1"]) + double.Parse(sItem["BGMDC1_2"]) + double.Parse(sItem["BGMDC1_3"])) / 3, 1);
                    mkd1 = Round((double.Parse(sItem["BGMDK1_1"]) + double.Parse(sItem["BGMDK1_2"]) + double.Parse(sItem["BGMDK1_3"])) / 3, 1);
                    mcd2 = Round((double.Parse(sItem["BGMDC2_1"]) + double.Parse(sItem["BGMDC2_2"]) + double.Parse(sItem["BGMDC2_3"])) / 3, 1);
                    mkd2 = Round((double.Parse(sItem["BGMDK2_1"]) + double.Parse(sItem["BGMDK2_2"]) + double.Parse(sItem["BGMDK2_3"])) / 3, 1);
                    mcd3 = Round((double.Parse(sItem["BGMDC3_1"]) + double.Parse(sItem["BGMDC3_2"]) + double.Parse(sItem["BGMDC3_3"])) / 3, 1);
                    mkd3 = Round((double.Parse(sItem["BGMDK3_1"]) + double.Parse(sItem["BGMDK3_2"]) + double.Parse(sItem["BGMDK3_3"])) / 3, 1);
                    mcd4 = Round((double.Parse(sItem["BGMDC4_1"]) + double.Parse(sItem["BGMDC4_2"]) + double.Parse(sItem["BGMDC4_3"])) / 3, 1);
                    mkd4 = Round((double.Parse(sItem["BGMDK4_1"]) + double.Parse(sItem["BGMDK4_2"]) + double.Parse(sItem["BGMDK4_3"])) / 3, 1);
                    mcd5 = Round((double.Parse(sItem["BGMDC5_1"]) + double.Parse(sItem["BGMDC5_2"]) + double.Parse(sItem["BGMDC5_3"])) / 3, 1);
                    mkd5 = Round((double.Parse(sItem["BGMDK5_1"]) + double.Parse(sItem["BGMDK5_2"]) + double.Parse(sItem["BGMDK5_3"])) / 3, 1);

                    if (mcd1 != 0 && !(double.Parse(sItem["BGMDC1"]) > 0))
                    {
                        sItem["BGMDC1"] = Round(mcd1, 1).ToString();
                        sItem["BGMDK1"] = Round(mkd1, 1).ToString();
                    }
                    if (mcd2 != 0 && !(double.Parse(sItem["BGMDC2"]) > 0))
                    {
                        sItem["BGMDC2"] = Round(mcd2, 1).ToString();
                        sItem["BGMDK2"] = Round(mkd2, 1).ToString();
                    }
                    if (mcd3 != 0 && !(double.Parse(sItem["BGMDC3"]) > 0))
                    {
                        sItem["BGMDC3"] = Round(mcd3, 1).ToString();
                        sItem["BGMDK3"] = Round(mkd3, 1).ToString();
                    }
                    if (mcd4 != 0 && !(double.Parse(sItem["BGMDC4"]) > 0))
                    {
                        sItem["BGMDC4"] = Round(mcd4, 1).ToString();
                        sItem["BGMDK4"] = Round(mkd4, 1).ToString();
                    }
                    if (mcd5 != 0 && !(double.Parse(sItem["BGMDC5"]) > 0))
                    {
                        sItem["BGMDC5"] = Round(mcd5, 1).ToString();
                        sItem["BGMDK5"] = Round(mkd5, 1).ToString();
                    }

                    mMj1 = double.Parse(sItem["BGMDC1"]) * double.Parse(sItem["BGMDK1"]);
                    mMj2 = double.Parse(sItem["BGMDC2"]) * double.Parse(sItem["BGMDK2"]);
                    mMj3 = double.Parse(sItem["BGMDC3"]) * double.Parse(sItem["BGMDK3"]);
                    mMj4 = double.Parse(sItem["BGMDC4"]) * double.Parse(sItem["BGMDK4"]);
                    mMj5 = double.Parse(sItem["BGMDC5"]) * double.Parse(sItem["BGMDK5"]);

                    if (mMj1 != 0 & mMj2 != 0 & mMj3 != 0 & mMj4 != 0 & mMj5 != 0)
                    {
                        mKyqd1 = Round(1000 * double.Parse(sItem["KYHZ1"]) / (mMj1), 2);
                        mKyqd2 = Round(1000 * double.Parse(sItem["KYHZ2"]) / (mMj2), 2);
                        mKyqd3 = Round(1000 * double.Parse(sItem["KYHZ3"]) / (mMj3), 2);
                        mKyqd4 = Round(1000 * double.Parse(sItem["KYHZ4"]) / (mMj4), 2);
                        mKyqd5 = Round(1000 * double.Parse(sItem["KYHZ5"]) / (mMj5), 2);
                    }
                    else
                    {
                        mKyqd1 = 0;
                        mKyqd2 = 0;
                        mKyqd3 = 0;
                        mKyqd4 = 0;
                        mKyqd5 = 0;
                    }

                    string mlongStr = (mKyqd1) + "," + (mKyqd2) + "," + (mKyqd3) + "," + (mKyqd4) + "," + (mKyqd5);
                    string[] mtmpArray = mlongStr.Split(',');
                    List<double> mtmpList = new List<double>();
                    foreach (string str in mtmpArray)
                    {
                        mtmpList.Add(double.Parse(str));
                    }
                    mtmpList.Sort();
                    double mMaxKyqd = mtmpList[4];
                    double mMinKyqd = mtmpList[0];
                    double mAvgKyqd = mtmpList.Average();
                    sItem["YSQD"] = mAvgKyqd.ToString("G3");

                    string rz1 = calc_PB(mItem["G_YSQD"], sItem["YSQD"], true);
                    if (rz1 == "符合")
                    {
                        mItem["HG_YSQD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_YSQD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["YSQD"] = "----";
                    mItem["HG_YSQD"] = "----";
                    mItem["G_YSQD"] = "----";
                }

                if (jcxm.Contains("、吸水率、"))
                {
                    string rz1 = calc_PB(mItem["G_XSL"], sItem["XSL"], true);
                    if (rz1 == "符合")
                    {
                        mItem["HG_XSL"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_XSL"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["XSL"] = "----";
                    mItem["HG_XSL"] = "----";
                    mItem["G_XSL"] = "----";
                }

                if (jcxm.Contains("、燃烧氧指数、"))
                {
                    string mnlfy = "";
                    if ("----" != sItem["RSNLFY1"])
                        mnlfy = mnlfy + sItem["RSNLFY1"].Trim();
                    if ("----" != sItem["RSNLFY2"] && sItem["RSNLFY2"] != sItem["RSNLFY1"])
                        mnlfy = mnlfy + sItem["RSNLFY2"].Trim();
                    if ("----" != sItem["RSNLFY3"] && sItem["RSNLFY2"] != sItem["RSNLFY1"])
                        mnlfy = mnlfy + sItem["RSNLFY3"].Trim();
                    if ("----" != sItem["RSNLFY4"] && sItem["RSNLFY2"] != sItem["RSNLFY1"])
                        mnlfy = mnlfy + sItem["RSNLFY4"].Trim();

                    string mntfy = "";
                    if ("----" != sItem["RSNTFY1"])
                        mntfy = mntfy + sItem["RSNTFY1"].Trim();
                    if ("----" != sItem["RSNTFY2"])
                        mntfy = mntfy + sItem["RSNTFY2"].Trim();
                    if ("----" != sItem["RSNTFY3"])
                        mntfy = mntfy + sItem["RSNTFY3"].Trim();
                    if ("----" != sItem["RSNTFY4"])
                        mntfy = mntfy + sItem["RSNTFY4"].Trim();
                    if ("----" != sItem["RSNTFY5"])
                        mntfy = mntfy + sItem["RSNTFY5"].Trim();

                    var mrsyzskb = YZSKB.FirstOrDefault(u => u["HWC"] == mntfy.Trim() && u["QJC"] == mnlfy.Trim());
                    if (mrsyzskb == null)
                    {
                        continue;
                    }
                    double mkz = double.Parse(mrsyzskb["K"]);
                    if (sItem["RSNTFY1"] == "0")
                    {
                        mkz = -mkz;
                    }
                    sItem["RSYZSKZ"] = Round(mkz, 2).ToString();
                    double moi = Round(double.Parse(sItem["RSNTYND5"]) + mkz * double.Parse(sItem["RSYZSD"]), 2);
                    sItem["RSYZS"] = Round((moi * 10) / 10, 1).ToString();
                    double mnlzhynd = double.Parse(sItem["RSNLYND" + mnlfy.Trim().Length]);
                    double mbzc = ((mnlzhynd - moi) * (mnlzhynd - moi) + (double.Parse(sItem["RSNTYND1"]) - moi) * (double.Parse(sItem["RSNTYND1"]) - moi) +
                         (double.Parse(sItem["RSNTYND2"]) - moi) * (double.Parse(sItem["RSNTYND2"]) - moi)
                          + (double.Parse(sItem["RSNTYND3"]) - moi) * (double.Parse(sItem["RSNTYND3"]) - moi)
                           + (double.Parse(sItem["RSNTYND4"]) - moi) * (double.Parse(sItem["RSNTYND4"]) - moi)
                            + (double.Parse(sItem["RSNTYND5"]) - moi) * (double.Parse(sItem["RSNTYND5"]) - moi)) / 5;
                    sItem["RSBZPC"] = Round(mbzc, 3).ToString();
                    sItem["RSYSM"] = "";
                    if (0.2 == double.Parse(sItem["RSYZSD"]) && 0.2 > 3 / 2 * double.Parse(sItem["RSBZPC"]) ||
                        (2 / 3 * double.Parse(sItem["RSBZPC"]) < double.Parse(sItem["RSYZSD"]) &&
                        double.Parse(sItem["RSYZSD"]) < 3 / 2 * double.Parse(sItem["RSBZPC"])))
                    {
                        sItem["RSYSM"] = "有效";
                        string rsyzs = calc_PB(mItem["G_RSYZS"], sItem["RSYZS"], true);
                        if (rsyzs == "符合")
                        {
                            mItem["HG_RSYZS"] = "合格";
                        }
                        else
                        {
                            mItem["HG_RSYZS"] = "不合格";
                            mbhggs++;
                        }
                    }
                    else
                    {
                        sItem["RSYSM"] = "无效";
                    }
                }
                else
                {
                    sItem["RSYSM"] = "----";
                    sItem["RSYZS"] = "----";
                    mItem["HG_RSYZS"] = "----";
                    mItem["G_RSYZS"] = "----";
                }

                if (jcxm.Contains("、燃烧分级、"))
                {
                    if ("符合" == sItem["RSFJ"])
                    {
                        mItem["HG_RSFJ"] = "合格";
                        sItem["RSFJJG"] = "符合E级";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_RSFJ"] = "不合格";
                        sItem["RSFJJG"] = "不符合";
                        mbhggs++;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["RSFJ"] = "----";
                    sItem["RSFJJG"] = "----";
                    mItem["HG_RSFJ"] = "----";
                    mItem["G_RSFJ"] = "----";
                }

                if (mbhggs == 0)
                {
                    jsbeizhu = "该组试样所检项目符合" + mItem["PDBZ"] + "标准要求。";
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    jsbeizhu = "该组试样不符合" + mItem["PDBZ"] + "标准要求。";
                    sItem["JCJG"] = "不合格";
                    if (mFlag_Bhg & mFlag_Hg)
                    {
                        jsbeizhu = "该组试样所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                    }
                }

                #region 添加最终报告
                if (mbhggs == 0)
                {
                    mjcjg = "合格";
                }

                MItem[0]["JCJG"] = mjcjg;
                MItem[0]["JCJGSM"] = jsbeizhu;
                #endregion
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
