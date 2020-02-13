using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class FDJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 参数定义
            string mcalBh;
            double[] mkyqdArray = new double[3];
            string[] mtmpArray;

            double zj1, zj2;
            int mbhggs;
            int mFsgs_qfqd, mFsgs_klqd, mFsgs_scl, mFsgs_lw;
            string mSjdjbh, mSjdj;
            double mQfqd, mKlqd, mScl, mLw;
            int vp, mCnt_FjHg, mCnt_FjHg1, mxlgs, mxwgs;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            bool mSFwc;
            bool mFlag_Hg, mFlag_Bhg;
            mSFwc = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_FDJ_DJ"];
            var MItem = data["M_FDJ"];
            var mitem = MItem[0];
            var SItem = data["S_FDJ"];
            #endregion

            #region  自定义函数
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
                        bool min_bl, max_bl, sign;
                        min_sjz = -99999;
                        max_sjz = 99999;
                        scz = GetSafeDouble(sc_fun);
                        sign = false;
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
                            sign = true;
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
                            sign = true;
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
                            sign = true;
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
                            sign = true;
                        }
                        if (sj_fun.Contains("～"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("～") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0, dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_bl = true;
                            max_bl = true;
                            sign = true;
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
                            sign = true;
                        }
                        if (sj_fun == "0" && !string.IsNullOrEmpty(sj_fun))
                        {
                            sign = true;
                            min_bl = false;
                            max_bl = false;
                            max_sjz = 0;
                        }
                        if (!sign)
                        {
                            calc_PB_fun = "----";
                        }
                        else
                        {
                            string hgjl, bhgjl;
                            hgjl = flag_fun ? "符合" : "合格";
                            bhgjl = flag_fun ? "不符合" : "不合格";
                            sign = true; //做为判定了
                            if (min_bl)
                                sign = scz >= min_sjz ? sign : false;
                            else
                                sign = scz > min_sjz ? sign : false;
                            if (max_bl)
                                sign = scz <= max_sjz ? sign : false;
                            else
                                sign = scz < max_sjz ? sign : false;
                            calc_PB_fun = sign ? hgjl : bhgjl;
                        }
                    }
                    return calc_PB_fun;
                };
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            mitem["JCJGMS"] = "";
            zj1 = 0;
            zj2 = 0;
            foreach (var sitem in SItem)
            {
                mSjdj = sitem["WJJMC"];
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                IDictionary<string, string> mrsDj_item = new Dictionary<string, string>();
                if (sitem["JCXM"].Trim().Contains("强度比"))
                    mrsDj_item = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()) && x["DJ"].Contains(sitem["DJ"].Trim()) && x["KYQDBWD"].Contains(sitem["KYGDWD"]));
                else
                    mrsDj_item = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()) && x["DJ"].Contains(sitem["DJ"].Trim()));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    mitem["G_XD"] = mrsDj_item["XD"];
                    mitem["G_MD"] = mrsDj_item["MD"];
                    mitem["G_MSL"] = mrsDj_item["MSL"];
                    mitem["G_JSL"] = mrsDj_item["JSL"];
                    mitem["G_GTHL"] = mrsDj_item["GTHL"];
                    mitem["G_CNSJC"] = mrsDj_item["CNSJC"];
                    mitem["G_ZNSJC"] = mrsDj_item["ZNSJC"];
                    mitem["G_TLD"] = mrsDj_item["TLD"];
                    mitem["G_HQLBHL"] = mrsDj_item["HQLBHL"];
                    mitem["G_HQL"] = mrsDj_item["HQL"];
                    mitem["G_KYQD1D"] = mrsDj_item["KYQDB1D"];
                    mitem["G_KYQD3D"] = mrsDj_item["KYQDB3D"];
                    mitem["G_KYQD7D"] = mrsDj_item["KYQDB7D"];
                    mitem["G_KYQD28D"] = mrsDj_item["KYQDB28D"];
                    mitem["G_XDNJX"] = mrsDj_item["XDNJX"];
                    mitem["G_PH"] = mrsDj_item["PH"];
                    mitem["G_LLZHL"] = mrsDj_item["LLZHL"];
                    mitem["G_LDD"] = mrsDj_item["LDD"];
                    mitem["G_STGDB"] = mrsDj_item["STGDB"];
                    mitem["G_DRQDB"] = mrsDj_item["DRQDSSLB"];
                    mitem["G_SSLB"] = mrsDj_item["SSLB28D"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_item["JSFF"]) ? "" : mrsDj_item["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                mbhggs = 0;
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    //int mbhggs;
                    mbhggs = 0;
                    int xd;
                    double md1, md2, sum, pjmd;
                    //bool mFlag_Hg, mFlag_Bhg;
                    mFlag_Hg = false;
                    mFlag_Bhg = false;
                    double[] narr;
                    var jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                    if (jcxm.Contains("、细度、"))
                    {
                        mitem["HG_XD"] = calc_pd(sitem["XDKZZ"], sitem["XD"]);
                        mitem["G_XD"] = sitem["XDKZZ"];
                        if (mitem["HG_XD"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        sitem["XD"] = "----";
                        mitem["HG_XD"] = "----";
                        mitem["G_XD"] = "----";
                    }
                    if (jcxm.Contains("、密度、"))
                    {
                        if (Conversion.Val(sitem["MDKZZ"]) > 1.1)
                            mitem["G_MD"] = Round((Conversion.Val(sitem["MDKZZ"]) - 0.03), 3).ToString("0.000") + "~" + Round((Conversion.Val(sitem["MDKZZ"]) + 0.03), 3).ToString("0.000");
                        else
                            mitem["G_MD"] = Round((Conversion.Val(sitem["MDKZZ"]) - 0.02), 3).ToString("0.000") + "~" + Round((Conversion.Val(sitem["MDKZZ"]) + 0.02), 3).ToString("0.000");
                        if (sitem["MDKZZ"] == "----")
                            mitem["HG_MD"] = "----";
                        else
                            mitem["HG_MD"] = calc_pd(mitem["G_MD"], sitem["MD"]);


                        if (mitem["HG_MD"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["MD"] = "----";
                        mitem["HG_MD"] = "----";
                        mitem["G_MD"] = "----";
                    }
                    if (jcxm.Contains("、固体含量、"))
                    {
                        if (Conversion.Val(sitem["HGLKZZ"]) >= 20)
                            mitem["G_GTHL"] = Round((Conversion.Val(sitem["HGLKZZ"]) * 0.95), 2).ToString("0.00") + "~" + Round((Conversion.Val(sitem["HGLKZZ"]) * 1.05), 2).ToString("0.00");
                        else
                            mitem["G_GTHL"] = Round((Conversion.Val(sitem["HGLKZZ"]) * 0.9), 2).ToString("0.00") + "~" + Round((Conversion.Val(sitem["HGLKZZ"]) * 1.1), 2).ToString("0.00");
                        if (sitem["HGLKZZ"] == "----")
                            mitem["HG_GTHL"] = "----";
                        else
                            mitem["HG_GTHL"] = calc_pd(mitem["G_GTHL"], sitem["GTHL"]);
                        if (mitem["HG_GTHL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["GTHL_1"] = "----";
                        sitem["GTHL_2"] = "----";
                        sitem["GTHL"] = "----";
                        mitem["G_GTHL"] = "----";
                        mitem["HG_GTHL"] = "----";
                    }
                    if (jcxm.Contains("、含水率、"))
                    {
                        if (Conversion.Val(sitem["HSLKZZ"]) >= 5)
                            mitem["G_HSL"] = Round(Conversion.Val(sitem["HSLKZZ"]) * 0.9, 2).ToString("0.00") + "~" + Round(Conversion.Val(sitem["HSLKZZ"]) * 1.1, 2).ToString("0.00");
                        else
                            mitem["G_HSL"] = Round(Conversion.Val(sitem["HSLKZZ"]) * 0.8, 2).ToString("0.00") + "~" + Round(Conversion.Val(sitem["HSLKZZ"]) * 1.2, 2).ToString("0.00");

                        if (sitem["HSLKZZ"] == "----")
                            mitem["HG_HSL"] = "----";
                        else
                            mitem["HG_HSL"] = calc_pd(mitem["G_HSL"], sitem["HSL"]);
                        if (mitem["HG_HSL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["HSL"] = "----";
                        mitem["G_HSL"] = "----";
                        mitem["HG_HSL"] = "----";
                    }
                    if (jcxm.Contains("、泌水率比、"))
                    {
                        mitem["HG_MSL"] = calc_pd(mitem["G_MSL"], sitem["MSLB"]);
                        if (mitem["HG_MSL"] == "不符合")
                            mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        mitem["G_MSL"] = "----";
                        sitem["MSLB"] = "-----";
                        mitem["HG_MSL"] = "----";
                    }
                    if (jcxm.Contains("、减水率、"))
                    {
                        mitem["HG_JSL"] = calc_pd(mitem["G_JSL"], sitem["PJJSL"]);
                        if (mitem["HG_JSL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_JSL"] = "----";
                        sitem["PJJSL"] = "-----";
                        mitem["HG_JSL"] = "----";
                    }
                    if (jcxm.Contains("、含气量、"))
                    {
                        mitem["HG_HQL"] = calc_pd(mitem["G_HQL"], sitem["PJHQL"]);
                        if (mitem["HG_HQL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_HQL"] = "----";
                        sitem["PJHQL"] = "---- - ";
                        mitem["HG_HQL"] = "----";
                    }
                    if (jcxm.Contains("、初凝时间差、"))
                    {
                        mitem["HG_CNSJC"] = calc_pd(mitem["G_CNSJC"], sitem["CNPJSJC"]);
                        if (mitem["HG_CNSJC"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_CNSJC"] = "----";
                        sitem["CNPJSJC"] = "-----";
                        mitem["HG_CNSJC"] = "----";
                    }
                    if (jcxm.Contains("、终凝时间差、"))
                    {
                        mitem["HG_ZNSJC"] = calc_pd(mitem["G_ZNSJC"], sitem["ZNPJSJC"]);
                        if (mitem["HG_ZNSJC"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_ZNSJC"] = "----";
                        sitem["ZNPJSJC"] = "-----";
                        mitem["HG_ZNSJC"] = "----";
                    }
                    if (jcxm.Contains("、R-7抗压强度比、") || jcxm.Contains("、抗压强度比、"))
                    {
                        if (!string.IsNullOrEmpty(sitem["QDB1"]))
                        {
                            mitem["HG_KYQD1D"] = calc_pd(mitem["G_KYQD1D"], sitem["QDB1"]);
                            if (mitem["HG_KYQD1D"] == "不符合")
                            {
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                                mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        mitem["G_KYQD1D"] = "----";
                        sitem["QDB1"] = "-----";
                        mitem["HG_KYQD1D"] = "----";
                    }
                    if (jcxm.Contains("、R28抗压强度比、") || jcxm.Contains("、抗压强度比、"))
                    {
                        if (!string.IsNullOrEmpty(sitem["QDB2"]))
                        {
                            mitem["HG_KYQD3D"] = calc_pd(mitem["G_KYQD3D"], sitem["QDB2"]);
                            if (mitem["HG_KYQD3D"] == "不符合")
                            {
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                                mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        mitem["G_KYQD3D"] = "----";
                        sitem["QDB2"] = "-----";
                        mitem["HG_KYQD3D"] = "----";
                    }
                    if (jcxm.Contains("、R-7+28抗压强度比、") || jcxm.Contains("、R－7＋28抗压强度比、") || jcxm.Contains("、抗压强度比、"))
                    {
                        if (!string.IsNullOrEmpty(sitem["QDB3"]))
                        {
                            mitem["HG_KYQD7D"] = calc_pd(mitem["G_KYQD7D"], sitem["QDB3"]);
                            if (mitem["HG_KYQD7D"] == "不符合")
                            {
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                                mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        mitem["G_KYQD7D"] = "----";
                        sitem["QDB3"] = "-----";
                        mitem["HG_KYQD7D"] = "----";
                    }
                    if (jcxm.Contains("、R-7+56抗压强度比、") || jcxm.Contains("、R－7＋56抗压强度比、") || jcxm.Contains("、抗压强度比、"))
                    {
                        if (!string.IsNullOrEmpty(sitem["QDB4"]))
                        {
                            mitem["HG_KYQD28D"] = calc_pd(mitem["G_KYQD28D"], sitem["QDB4"]);
                            if (mitem["HG_KYQD28D"] == "不符合")
                            {
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                                mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        mitem["G_KYQD28D"] = "----";
                        sitem["QDB4"] = "-----";
                        mitem["HG_KYQD28D"] = "----";
                    }
                    if (jcxm.Contains("、收缩率比、"))
                    {
                        mitem["HG_SSLB"] = calc_pd(mitem["G_SSLB"], sitem["SSLB"]);
                        if (mitem["HG_SSLB"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["SSLB"] = "----";
                        mitem["HG_SSLB"] = "----";
                        mitem["G_SSLB"] = "----";
                    }
                    if (jcxm.Contains("、50次冻融强度损失率比、"))
                    {
                        mitem["HG_DRQDB"] = calc_pd(mitem["G_DRQDB"], sitem["DRQDSSLB"]);
                        if (mitem["HG_DRQDB"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["DRQDSSLB"] = "----";
                        mitem["HG_DRQDB"] = "----";
                        mitem["G_DRQDB"] = "----";
                    }
                    if (jcxm.Contains("、渗透高度比、"))
                    {
                        mitem["HG_STGDB"] = calc_pd(mitem["G_STGDB"], sitem["STGDB"]);
                        if (mitem["HG_STGDB"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["STGDB"] = "----";
                        mitem["HG_STGDB"] = "----";
                        mitem["G_STGDB"] = "----";
                    }
                    if (jcxm.Contains("、水泥净浆流动度、"))
                    {
                        if (sitem["LDDKZZ"].Contains("≥"))
                            sitem["LDDKZZ"] = sitem["LDDKZZ"].Substring(sitem["LDDKZZ"].Length - sitem["LDDKZZ"].IndexOf("≥"));
                        else
                            mitem["G_LDD"] = "≥" + Round(Conversion.Val(sitem["LDDKZZ"]) * 0.95, 1).ToString();
                        if (sitem["LDDKZZ"] == "----")
                            mitem["HG_LDD"] = "----";
                        else
                            mitem["HG_LDD"] = calc_pd(mitem["G_LDD"], sitem["LDD"]);
                        if (mitem["HG_LDD"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["LDD"] = "----";
                        mitem["HG_LDD"] = "----";
                        mitem["G_LDD"] = "----";
                    }
                    if (jcxm.Contains("、氯离子含量、"))
                    {
                        if (sitem["LLZKZZ"] == "----")
                            mitem["HG_LLZHL"] = "----";
                        else
                            mitem["HG_LLZHL"] = calc_pd(sitem["LLZKZZ"], sitem["LLZHL"]);
                        mitem["G_LLZHL"] = sitem["LLZKZZ"];
                        if (mitem["HG_LLZHL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["LLZHL"] = "----";
                        mitem["HG_LLZHL"] = "----";
                    }
                    if (jcxm.Contains("、碱含量、"))
                    {
                        mitem["G_ZJL"] = sitem["ZJLKZZ"].Trim();
                        if (sitem["ZJLKZZ"] == "----")
                            mitem["HG_ZJL"] = "----";
                        else
                            mitem["HG_ZJL"] = calc_pd(mitem["G_ZJL"], sitem["ZJL"]);
                        if (mitem["HG_ZJL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_ZJL"] = "----";
                        sitem["ZJL"] = "----";
                        mitem["HG_ZJL"] = "----";
                    }
                    if (sitem["SFFJ"] == "1")
                    {
                        if (mbhggs > 0)
                        {
                            sitem["JCJG"] = "不合格";
                            mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
                            if (mFlag_Bhg && mFlag_Hg)
                                mitem["JCJGMS"] = "该组试样部分符合" + mitem["PDBZ"] + "标准要求";
                            else
                            {
                                mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                                sitem["JCJG"] = "合格";
                            }
                        }
                        else
                        {
                            if (mbhggs == 0)
                            {
                                mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求";
                                sitem["JCJG"] = "合格";
                            }
                            if (mbhggs > 0)
                            {
                                mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求";
                                sitem["JCJG"] = "不合格";
                                if (mFlag_Bhg && mFlag_Hg)
                                    mitem["JCJGMS"] = "该组试样部分符合" + mitem["PDBZ"] + "标准要求";
                            }
                        }
                        mAllHg = (mAllHg && sitem["JCJG"] == "合格");
                        continue;
                    }
                }
            }
            //主表总判断赋值
            if (mAllHg)
                mitem["JCJG"] = "合格";
            else
                mitem["JCJG"] = "不合格";
            #endregion
            /************************ 代码结束 *********************/

        }
    }
}
