using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class FSJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
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
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
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
                            l_bl = sj_fun.Substring(0,dw - 1);
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
                            l_bl = sj_fun.Substring(0,dw - 1);
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
                            l_bl = sj_fun.Substring(0,dw - 1);
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
                            l_bl = sj_fun.Substring(0,dw - 1);
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
                            min_sjz = GetSafeDouble(sj_fun.Substring(0,dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_bl = true;
                            max_bl = true;
                            sign_fun = true;
                        }
                        if (sj_fun.Contains("±"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("±") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0,dw - 1));
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

            Func<double, double, double, double> maxVal =
                delegate (double md1, double md2, double md3)
                {
                    double St;
                    St = md1;
                    St = St < md2 ? md2 : St;
                    St = St < md3 ? md3 : St;
                    return St;
                };
            Func<double, double, double, double> minVal =
                delegate (double md1, double md2, double md3)
                {
                    double St;
                    St = md1;
                    St = St > md2 ? md2 : St;
                    St = St > md3 ? md3 : St;
                    return St;
                };

            Func<IDictionary<string, string>, IDictionary<string, string>, bool, bool> sjtabcalc =
                delegate (IDictionary<string, string> mitem, IDictionary<string, string> sitem, bool mAllHg_fun)
                {
                    mbhggs = 0;
                    int xd;
                    double md1, md2, sum, pjmd;
                    double[] narr;
                    mFlag_Hg = false;
                    mFlag_Bhg = false;
                    var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
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
                            mFlag_Hg = true;
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
                            mitem["G_MD"] = Round(Conversion.Val(sitem["MDKZZ"]) - 0.03, 3).ToString("F3") + "~" + Round(Conversion.Val(sitem["MDKZZ"]) + 0.03, 3).ToString("F3");
                        else
                            mitem["G_MD"] = Round(Conversion.Val(sitem["MDKZZ"]) - 0.02, 3).ToString("F3") + "~" + Round(Conversion.Val(sitem["MDKZZ"]) + 0.02, 3).ToString("F3");
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
                        if (Conversion.Val(sitem["HGLKZZ"]) > 25)
                            mitem["G_GTHL"] = Round(Conversion.Val(sitem["HGLKZZ"]) * 0.95, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HGLKZZ"]) * 1.05, 2).ToString("F2");
                        else
                            mitem["G_GTHL"] = Round(Conversion.Val(sitem["HGLKZZ"]) * 0.9, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HGLKZZ"]) * 1.1, 2).ToString("F2");
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
                        sitem["GTHL"] = "----";
                        mitem["G_GTHL"] = "----";
                        mitem["HG_GTHL"] = "----";
                    }
                    if (jcxm.Contains("、含水率、"))
                    {
                        if (Conversion.Val(sitem["HSLKZZ"]) > 5)
                            mitem["G_HSL"] = Round(Conversion.Val(sitem["HSLKZZ"]) * 0.9, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HSLKZZ"]) * 1.1, 2).ToString("F2");
                        else
                            mitem["G_HSL"] = Round(Conversion.Val(sitem["HSLKZZ"]) * 0.8, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HSLKZZ"]) * 1.2, 2).ToString("F2");
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
                    if (jcxm.Contains("、总碱量、"))
                    {
                        mitem["G_ZJL"] = sitem["ZJLKZZ"].Trim();
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
                    if (jcxm.Contains("、氯离子含量、"))
                    {
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
                        mitem["G_LLZHL"] = "----";
                    }
                    if (jcxm.Contains("、安定性、"))
                    {
                        bool sign = false;
                        if (IsNumeric(sitem["ADXLS"]) && !string.IsNullOrEmpty(sitem["ADXLS"]))
                        {
                            double md = GetSafeDouble(sitem["ADXLS"].Trim());
                            if (md > 0)
                                sign = true;
                        }
                        if (sign)
                            mitem["HG_ADX"] = calc_PB("≤5.0", sitem["ADXLS"], true);
                        else
                            mitem["HG_ADX"] = sitem["ADX"] == "完整" ? "符合" : "不符合";
                        mbhggs = mitem["HG_ADX"] == "不符合" ? mbhggs + 1 : mbhggs;
                        if (mitem["HG_ADX"] != "不符合")
                            mFlag_Hg = true;

                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        mitem["G_ADX"] = "----";
                        mitem["HG_ADX"] = "----";
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
                    if (jcxm.Contains("、透水压力比、"))
                    {
                        mitem["HG_TSYLB"] = calc_pd(mitem["G_TSYLB"], sitem["TSYLB"]);
                        if (mitem["HG_TSYLB"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["TSYLB"] = "----";
                        mitem["HG_TSYLB"] = "----";
                        mitem["G_TSYLB"] = "----";
                    }
                    if (jcxm.Contains("、凝结时间、"))
                    {
                        mitem["HG_CNSJ"] = calc_pd(mitem["G_CNSJ"], sitem["CNSJ"]);
                        mitem["HG_ZNSJ"] = calc_pd(mitem["G_ZNSJ"], sitem["ZNSJ"]);
                        if (mitem["HG_CNSJ"] == "不符合" || mitem["HG_ZNSJ"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["CNSJ"] = "----";
                        sitem["ZNSJ"] = "----";
                        mitem["HG_CNSJ"] = "----";
                        mitem["HG_ZNSJ"] = "----";
                        mitem["G_CNSJ"] = "----";
                        mitem["G_ZNSJ"] = "----";
                    }
                    if (jcxm.Contains("、泌水率比、"))
                    {
                        mitem["HG_MSL"] = calc_pd(mitem["G_MSL"], sitem["MSLB"]);
                        if (mitem["HG_MSL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_MSL"] = "----";
                        sitem["MSLB"] = "---- - ";
                        mitem["HG_MSL"] = "----";
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
                    if (jcxm.Contains("、3d抗压强度比、"))
                    {
                        mitem["HG_KYQD3D"] = calc_pd(mitem["G_KYQD3D"], sitem["PJQDB3D"]);
                        if (mitem["HG_KYQD3D"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_KYQD3D"] = "----";
                        sitem["PJQDB3D"] = "-----";
                        mitem["HG_KYQD3D"] = "----";
                    }
                    if (jcxm.Contains("、7d抗压强度比、"))
                    {
                        mitem["HG_KYQD7D"] = calc_pd(mitem["G_KYQD7D"], sitem["PJQDB7D"]);
                        if (mitem["HG_KYQD7D"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_KYQD7D"] = "----";
                        sitem["PJQDB7D"] = "-----";
                        mitem["HG_KYQD7D"] = "----";
                    }
                    if (jcxm.Contains("、28d抗压强度比、"))
                    {
                        mitem["HG_KYQD28D"] = calc_pd(mitem["G_KYQD28D"], sitem["PJQDB28D"]);
                        if (mitem["HG_KYQD28D"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_KYQD28D"] = "----";
                        sitem["PJQDB28D"] = "-----";
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
                    if (jcxm.Contains("、吸水量比、"))
                    {
                        mitem["HG_XSLB"] = calc_pd(mitem["G_XSLB"], sitem["XSLB"]);
                        if (mitem["HG_XSLB"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["XSLB"] = "----";
                        mitem["HG_XSLB"] = "----";
                        mitem["G_XSLB"] = "----";

                    }
                    if (sitem["SFFJ"] == "1")
                    {
                        if (mbhggs > 0)
                        {
                            sitem["JCJG"] = "不合格";
                            mitem["JCJGMS"] = "该组试件不符合" + mitem["PDBZ"] + "标准要求。";
                            if (mFlag_Bhg && mFlag_Hg)
                                mitem["JCJGMS"] = "该组试件所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                        }
                        else
                        {
                            mitem["JCJGMS"] = "该组试件所检项目符合" + mitem["PDBZ"] + "标准要求。";
                            sitem["JCJG"] = "合格";
                        }
                    }
                    else
                    {
                        if (mbhggs == 0)
                        {
                            mitem["JCJGMS"] = "该组试件所检项目符合" + mitem["PDBZ"] + "标准要求";
                            sitem["JCJG"] = "合格";
                        }
                        if (mbhggs > 0)
                        {
                            mitem["JCJGMS"] = "该组试件不符合" + mitem["PDBZ"] + "标准要求";
                            sitem["JCJG"] = "不合格";
                            if (mFlag_Bhg && mFlag_Hg)
                                mitem["JCJGMS"] = "该组试件所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                        }
                    }
                    mAllHg_fun = mbhggs > 0 ? false : mAllHg_fun;
                    return mAllHg_fun;
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_FSJ_DJ"];
            var mrsadx = dataExtra["BZ_NADXFF"];
            var MItem = data["M_FSJ"];
            var SItem = data["S_FSJ"];
            #endregion

            #region 计算开始
            string which = string.Empty;
            MItem[0]["JCJGMS"] = "";
            zj1 = 0;
            zj2 = 0;
            foreach (var sitem in SItem)
            {
                mSjdj = sitem["WJJMC"];            //设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()) && x["PZ"].Contains(sitem["PZ"].Trim()));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    MItem[0]["G_XD"] = mrsDj_Filter["XD"];
                    MItem[0]["G_MD"] = mrsDj_Filter["MD"];
                    MItem[0]["G_MSL"] = mrsDj_Filter["MSL"];
                    MItem[0]["G_GTHL"] = mrsDj_Filter["GTHL"];
                    MItem[0]["G_CNSJC"] = mrsDj_Filter["CNSJC"];
                    MItem[0]["G_ZNSJC"] = mrsDj_Filter["ZNSJC"];
                    MItem[0]["G_STGDB"] = mrsDj_Filter["STGDB"];
                    MItem[0]["G_TSYLB"] = mrsDj_Filter["TSYLB"];
                    MItem[0]["G_CNSJ"] = mrsDj_Filter["CNSJ"];
                    MItem[0]["G_ZNSJ"] = mrsDj_Filter["ZNSJ"];
                    which = mrsDj_Filter["WHICH"];
                    MItem[0]["G_KYQD1D"] = mrsDj_Filter["KYQDB1D"];
                    MItem[0]["G_KYQD3D"] = mrsDj_Filter["KYQDB3D"];
                    MItem[0]["G_KYQD7D"] = mrsDj_Filter["KYQDB7D"];
                    MItem[0]["G_KYQD28D"] = mrsDj_Filter["KYQDB28D"];
                    MItem[0]["G_SSLB"] = mrsDj_Filter["SSLB28D"];
                    MItem[0]["G_XSLB"] = mrsDj_Filter["XSLB"];
                    MItem[0]["G_LLZHL"] = mrsDj_Filter["LLZHL"];
                    MItem[0]["G_ADX"] = mrsDj_Filter["ADX"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                mbhggs = 0;
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    if (sjtabcalc(MItem[0], sitem, mAllHg))
                    { }
                    continue;
                }
                double md, md1, md2, pjmd, sum;
                int xd, xd1, xd2;
                double[] Arrmd = new double[4];
                string sd;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、细度、"))
                {
                    if (sitem["XDM1_1"] != "" && sitem["XDM1_1"] != "----")
                    {
                        sitem["XD_1"] = Round((Conversion.Val(sitem["XDM1_1"]) / Conversion.Val(sitem["XDM0_1"])) * 100, 2).ToString("F2");
                        sitem["XD_2"] = Round((Conversion.Val(sitem["XDM1_2"]) / Conversion.Val(sitem["XDM0_2"])) * 100, 2).ToString("F2");
                    }
                    if (sitem["XD_1"] != "" && sitem["XD_1"] != "----")
                        sitem["XD"] = Round((Conversion.Val(sitem["XD_1"]) + Conversion.Val(sitem["XD_2"])) / 2, 2).ToString("F2");
                    if (sitem["XDKZZ"] == "----")
                        MItem[0]["HG_XD"] = "---";
                    else
                        MItem[0]["HG_XD"] = calc_pd(sitem["XDKZZ"], sitem["XD"]);
                    MItem[0]["G_XD"] = sitem["XDKZZ"];
                    if (MItem[0]["HG_XD"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["XD_1"] = "----";
                    sitem["XD_2"] = "----";
                    sitem["XD"] = "----";
                    MItem[0]["HG_XD"] = "----";
                    MItem[0]["G_XD"] = "----";
                }
                if (jcxm.Contains("、密度、"))
                {
                    if (sitem["MDBJWJJ_1"] != "" && sitem["MDBJWJJ_1"] != "----")
                    {
                        sitem["MD_1"] = Round(0.9982 * ((Conversion.Val(sitem["MDBJWJJ_1"]) - Conversion.Val(sitem["MDRLBZ_1"])) / Conversion.Val(sitem["MDTJ_1"])), 3).ToString("F3");
                        sitem["MD_2"] = Round(0.9982 * ((Conversion.Val(sitem["MDBJWJJ_2"]) - Conversion.Val(sitem["MDRLBZ_2"])) / Conversion.Val(sitem["MDTJ_2"])), 3).ToString("F3");
                    }
                    if (sitem["MD_1"] != "" && sitem["MD_1"] != "----")
                        sitem["MD"] = Round((Conversion.Val(sitem["MD_1"]) + Conversion.Val(sitem["MD_2"])) / 2, 3).ToString("F3");


                    if (Conversion.Val(sitem["MDKZZ"]) > 1.1)
                        MItem[0]["G_MD"] = Round((Conversion.Val(sitem["MDKZZ"]) - 0.03), 3).ToString("F3") + "~" + Round((Conversion.Val(sitem["MDKZZ"]) + 0.03), 3).ToString("F3");
                    else
                        MItem[0]["G_MD"] = Round((Conversion.Val(sitem["MDKZZ"]) - 0.02), 3).ToString("F3") + "~" + Round((Conversion.Val(sitem["MDKZZ"]) + 0.02), 3).ToString("F3");
                    if (sitem["MDKZZ"] == "----")
                        MItem[0]["HG_MD"] = "----";
                    else
                        MItem[0]["HG_MD"] = calc_pd(MItem[0]["G_MD"], sitem["MD"]);
                    if (MItem[0]["HG_MD"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["MD_1"] = "----";
                    sitem["MD_2"] = "----";
                    sitem["MD"] = "----";
                    MItem[0]["HG_MD"] = "----";
                    MItem[0]["G_MD"] = "----";
                }
                if (jcxm.Contains("、固体含量、"))
                {
                    if (sitem["GTHLM2_1"] != "" && sitem["GTHLM2_1"] != "----")
                    {
                        sitem["GTHL_1"] = Round((Conversion.Val(sitem["GTHLM2_1"]) - Conversion.Val(sitem["GTHLM0_1"])) / (Conversion.Val(sitem["GTHLM1_1"]) - Conversion.Val(sitem["GTHLM0_1"])) * 100, 2).ToString("F2");
                        sitem["GTHL_2"] = Round((Conversion.Val(sitem["GTHLM2_2"]) - Conversion.Val(sitem["GTHLM0_2"])) / (Conversion.Val(sitem["GTHLM1_2"]) - Conversion.Val(sitem["GTHLM0_2"])) * 100, 2).ToString("F2");
                    }
                    if (sitem["GTHL_1"] != "" && sitem["GTHL_1"] != "----")
                        sitem["GTHL"] = Round((Conversion.Val(sitem["GTHL_1"]) + Conversion.Val(sitem["GTHL_2"])) / 2, 2).ToString("F2");
                    if (Conversion.Val(sitem["HGLKZZ"]) > 25)
                        MItem[0]["G_GTHL"] = Round(Conversion.Val(sitem["HGLKZZ"]) * 0.95, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HGLKZZ"]) * 1.05, 2).ToString("F2");
                    else
                        MItem[0]["G_GTHL"] = Round(Conversion.Val(sitem["HGLKZZ"]) * 0.9, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HGLKZZ"]) * 1.1, 2).ToString("F2");
                    if (sitem["HGLKZZ"] == "----")
                        MItem[0]["HG_GTHL"] = "----";
                    else
                        MItem[0]["HG_GTHL"] = calc_pd(MItem[0]["G_GTHL"], sitem["GTHL"]);


                    if (MItem[0]["HG_GTHL"] == "不符合")
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
                    MItem[0]["G_GTHL"] = "----";
                    MItem[0]["HG_GTHL"] = "----";
                }
                if (jcxm.Contains("、含水率、"))
                {
                    if (sitem["HSLM1_1"] != "" && sitem["HSLM1_1"] != "----")
                    {
                        sitem["HSL_1"] = Round((Conversion.Val(sitem["HSLM1_1"]) - Conversion.Val(sitem["HSLM2_1"])) / (Conversion.Val(sitem["HSLM2_1"]) - Conversion.Val(sitem["HSLM0_1"])) * 100, 2).ToString("F2");
                        sitem["HSL_2"] = Round((Conversion.Val(sitem["HSLM1_2"]) - Conversion.Val(sitem["HSLM2_2"])) / (Conversion.Val(sitem["HSLM2_2"]) - Conversion.Val(sitem["HSLM0_2"])) * 100, 2).ToString("F2");
                        sitem["HSL_3"] = Round((Conversion.Val(sitem["HSLM1_3"]) - Conversion.Val(sitem["HSLM2_3"])) / (Conversion.Val(sitem["HSLM2_3"]) - Conversion.Val(sitem["HSLM0_3"])) * 100, 2).ToString("F2");
                    }
                    if (sitem["HSL_1"] != "" && sitem["HSL_1"] != "----")
                        sitem["HSL"] = Round((Conversion.Val(sitem["HSL_1"]) + Conversion.Val(sitem["HSL_2"]) + Conversion.Val(sitem["HSL_3"])) / 3, 1).ToString("F1");
                    if (Conversion.Val(sitem["HSLKZZ"]) > 5)
                        MItem[0]["G_HSL"] = Round(Conversion.Val(sitem["HSLKZZ"]) * 0.9, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HSLKZZ"]) * 1.1, 2).ToString("F2");
                    else
                        MItem[0]["G_HSL"] = Round(Conversion.Val(sitem["HSLKZZ"]) * 0.8, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HSLKZZ"]) * 1.2, 2).ToString("F2");
                    if (sitem["HSLKZZ"] == "----")
                        MItem[0]["HG_HSL"] = "----";
                    else
                        MItem[0]["HG_HSL"] = calc_pd(MItem[0]["G_HSL"], sitem["HSL"]);
                    if (MItem[0]["HG_HSL"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["HSL_1"] = "----";
                    sitem["HSL_2"] = "----";
                    sitem["HSL"] = "----";
                    MItem[0]["G_HSL"] = "----";
                    MItem[0]["HG_HSL"] = "----";
                }
                if (jcxm.Contains("、总碱量、"))
                {
                    MItem[0]["G_ZJL"] = sitem["ZJLKZZ"].Trim();
                    if (sitem["ZJLKZZ"] == "----")
                        MItem[0]["HG_ZJL"] = "----";
                    else
                        MItem[0]["HG_ZJL"] = calc_pd(MItem[0]["G_ZJL"], sitem["ZJL"]);
                    if (MItem[0]["HG_ZJL"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    MItem[0]["G_ZJL"] = "----";
                    sitem["ZJL"] = "----";
                    MItem[0]["HG_ZJL"] = "----";
                }
                if (jcxm.Contains("、氯离子含量、"))
                {
                    if (sitem["LLZKZZ"] == "----")
                        MItem[0]["HG_LLZHL"] = "----";
                    else
                        MItem[0]["HG_LLZHL"] = calc_pd(sitem["LLZKZZ"], sitem["LLZHL"]);
                    MItem[0]["G_LLZHL"] = sitem["LLZKZZ"];
                    if (MItem[0]["HG_LLZHL"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["LLZHL1"] = "----";
                    sitem["LLZHL2"] = "----";
                    sitem["LLZHL"] = "----";
                    MItem[0]["HG_LLZHL"] = "----";
                }
                string mg_adx = "";
                string mBZFpj = "";
                string mBZFxc = "";
                string mg_bzfpj = "";
                string mg_bzfxc = "";
                bool adx_hg = true;
                if (!jcxm.Contains("、安定性、"))
                {
                    sitem["ADXFF"] = "----";
                    MItem[0]["HG_ADX"] = "----";
                    mg_adx = "----";
                    sitem["ADX"] = "----";
                    mBZFpj = "----";
                    mBZFxc = "----";
                    mg_bzfpj = "----";
                    mg_bzfxc = "----";
                    adx_hg = true;
                }
                else
                {

                    if (sitem["ADXFF"] == "----" || string.IsNullOrEmpty(sitem["ADXFF"]))
                        sitem["ADXFF"] = "代用法";
                    var mrsadx_Filter = mrsadx.FirstOrDefault(x => x["MC"].Contains(sitem["ADXFF"].Trim()));
                    if (mrsadx_Filter != null && mrsadx_Filter.Count > 0)
                    {
                        mg_adx = mrsadx_Filter["G_ADX"];
                        mg_bzfpj = mrsadx_Filter["BZFPJ"];
                        mg_bzfxc = mrsadx_Filter["BZFXC"];
                    }
                    //安定性
                    if (sitem["ADXFF"] == "代用法")
                    {
                        if (sitem["ADX"] == "完整")
                        {
                            MItem[0]["HG_ADX"] = "符合";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            MItem[0]["HG_ADX"] = "不符合";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        mBZFpj = Round(((Conversion.Val(sitem["BZFC_1"]) - Conversion.Val(sitem["BZFA_1"])) + (Conversion.Val(sitem["BZFC_2"]) - Conversion.Val(sitem["BZFA_2"]))) / 2, 1).ToString();
                        mBZFxc = Round(Math.Abs((Conversion.Val(sitem["BZFC_1"]) - Conversion.Val(sitem["BZFA_1"])) - (Conversion.Val(sitem["BZFC_2"]) - Conversion.Val(sitem["BZFA_2"]))), 1).ToString();
                        if (Conversion.Val(mBZFpj) <= Conversion.Val(mg_bzfpj) && Conversion.Val(mBZFxc) <= Conversion.Val(mg_bzfxc))
                        {
                            MItem[0]["HG_ADX"] = "符合";
                            mFlag_Bhg = true;
                        }

                        else
                        {

                            MItem[0]["HG_ADX"] = "不符合";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                }
                if (jcxm.Contains("、渗透高度比、"))
                {
                    sitem["STGDB"] = Round((Conversion.Val(sitem["SSTGD"]) / Conversion.Val(sitem["JSTGD"])) * 100, 0).ToString();
                    MItem[0]["HG_STGDB"] = calc_pd(MItem[0]["G_STGDB"], sitem["STGDB"]);
                    if (MItem[0]["HG_STGDB"] == "不符合")
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
                    MItem[0]["HG_STGDB"] = "----";
                    MItem[0]["G_STGDB"] = "----";
                }
                if (jcxm.Contains("、透水压力比、"))
                {
                    sitem["TSYLB"] = Round(Conversion.Val(sitem["STSYL"]) / Conversion.Val(sitem["JTSYL"]) * 100, 0).ToString();
                    MItem[0]["HG_TSYLB"] = calc_pd(MItem[0]["G_TSYLB"], sitem["TSYLB"]);
                    if (MItem[0]["HG_TSYLB"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["TSYLB"] = "----";
                    MItem[0]["HG_TSYLB"] = "----";
                    MItem[0]["G_TSYLB"] = "----";
                }
                if (jcxm.Contains("、凝结时间、"))
                {
                    sitem["CNSJ"] = (Conversion.Val(sitem["CNSJH"]) * 60 + Conversion.Val(sitem["CNSJM"]) - (Conversion.Val(sitem["JSSJH"]) * 60 + Conversion.Val(sitem["JSSJM"]))).ToString();
                    sitem["ZNSJ"] = (Conversion.Val(sitem["ZNSJH"]) * 60 + Conversion.Val(sitem["ZNSJM"]) - (Conversion.Val(sitem["JSSJH"]) * 60 + Conversion.Val(sitem["JSSJM"]))).ToString();
                    MItem[0]["HG_CNSJ"] = calc_pd(MItem[0]["G_CNSJ"], sitem["CNSJ"]);
                    MItem[0]["HG_ZNSJ"] = calc_pd(MItem[0]["G_ZNSJ"], sitem["ZNSJ"]);
                    if (MItem[0]["HG_CNSJ"] == "不符合" || MItem[0]["HG_ZNSJ"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["CNSJ"] = "----";
                    sitem["ZNSJ"] = "----";
                    MItem[0]["HG_CNSJ"] = "----";
                    MItem[0]["HG_ZNSJ"] = "----";
                    MItem[0]["G_CNSJ"] = "----";
                    MItem[0]["G_ZNSJ"] = "----";
                }
                if (jcxm.Contains("、泌水率比、"))
                {
                    if (sitem["PBSN1"] != "" && sitem["PBSN1"] != "----")
                    {
                        sitem["JPHWZL_1"] = (Conversion.Val(sitem["PBSN1"]) + Conversion.Val(sitem["PBS1"]) + Conversion.Val(sitem["PBSA1"]) + Conversion.Val(sitem["PBSZ1"])).ToString();
                        sitem["JPHWZL_2"] = (Conversion.Val(sitem["PBSN2"]) + Conversion.Val(sitem["PBS2"]) + Conversion.Val(sitem["PBSA2"]) + Conversion.Val(sitem["PBSZ2"])).ToString();
                        sitem["JPHWZL_3"] = (Conversion.Val(sitem["PBSN3"]) + Conversion.Val(sitem["PBS3"]) + Conversion.Val(sitem["PBSA3"]) + Conversion.Val(sitem["PBSZ3"])).ToString();
                        sitem["MJBYS_1"] = Conversion.Val(sitem["PBS1"]).ToString();
                        sitem["MJBYS_2"] = Conversion.Val(sitem["PBS2"]).ToString();
                        sitem["MJBYS_3"] = Conversion.Val(sitem["PBS3"]).ToString();
                    }
                    if (sitem["JMSZL_1"] != "" && sitem["JMSZL_1"] != "----")
                    {
                        sitem["JMSL_1"] = Round(Conversion.Val(sitem["JMSZL_1"]) / (Conversion.Val(sitem["MJBYS_1"]) / Conversion.Val(sitem["JPHWZL_1"])) / Conversion.Val(sitem["JSYZL_1"]) * 100, 2).ToString("0.00");
                        sitem["JMSL_2"] = Round(Conversion.Val(sitem["JMSZL_2"]) / (Conversion.Val(sitem["MJBYS_2"]) / Conversion.Val(sitem["JPHWZL_2"])) / Conversion.Val(sitem["JSYZL_2"]) * 100, 2).ToString("0.00");
                        sitem["JMSL_3"] = Round(Conversion.Val(sitem["JMSZL_3"]) / (Conversion.Val(sitem["MJBYS_3"]) / Conversion.Val(sitem["JPHWZL_3"])) / Conversion.Val(sitem["JSYZL_3"]) * 100, 2).ToString("0.00");
                    }
                    if (sitem["JMSL_1"] != "" && sitem["JMSL_1"] != "----")
                    {
                        string mlongStr = sitem["JMSL_1"] + ", " + sitem["JMSL_2"] + ", " + sitem["JMSL_3"];
                        mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 2; vp++)
                            mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkyqdArray);
                        double mMaxKyqd = mkyqdArray[2];
                        double mMinKyqd = mkyqdArray[0];
                        double mMidKyqd = mkyqdArray[1];
                        double mAvgKyqd = mkyqdArray.Average();
                        MItem[0]["JCJGMS"] = "";
                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_MSL"] = "重做";
                            sitem["JPJMSL"] = "重做";
                        }

                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sitem["JPJMSL"] = Round(mMidKyqd, 1).ToString("0.0");
                        }

                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sitem["JPJMSL"] = Round(mMidKyqd, 1).ToString("0.0");
                        }

                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sitem["JPJMSL"] = Round(mAvgKyqd, 1).ToString("0.0");

                        }
                    }
                    if (sitem["SPBSN1"] != "" && sitem["SPBSN1"] != "----")
                    {
                        sitem["SPHWZL_1"] = (Conversion.Val(sitem["SPBSN1"]) + Conversion.Val(sitem["SPBS1"]) + Conversion.Val(sitem["SPBSA1"]) + Conversion.Val(sitem["SPBSZ1"]) + Conversion.Val(sitem["SPBWJJ11"]) + Conversion.Val(sitem["SPBWJJ21"])).ToString();
                        sitem["SPHWZL_2"] = (Conversion.Val(sitem["SPBSN2"]) + Conversion.Val(sitem["SPBS2"]) + Conversion.Val(sitem["SPBSA2"]) + Conversion.Val(sitem["SPBSZ2"]) + Conversion.Val(sitem["SPBWJJ12"]) + Conversion.Val(sitem["SPBWJJ22"])).ToString();
                        sitem["SPHWZL_3"] = (Conversion.Val(sitem["SPBSN3"]) + Conversion.Val(sitem["SPBS3"]) + Conversion.Val(sitem["SPBSA3"]) + Conversion.Val(sitem["SPBSZ3"]) + Conversion.Val(sitem["SPBWJJ13"]) + Conversion.Val(sitem["SPBWJJ23"])).ToString();
                        sitem["MSBYS_1"] = Conversion.Val(sitem["SPBS1"]).ToString();
                        sitem["MSBYS_2"] = Conversion.Val(sitem["SPBS2"]).ToString();
                        sitem["MSBYS_3"] = Conversion.Val(sitem["SPBS3"]).ToString();
                    }
                    if (sitem["SMSZL_1"] != "" && sitem["SMSZL_1"] != "----")
                    {
                        sitem["SMSL_1"] = Round(Conversion.Val(sitem["SMSZL_1"]) / (Conversion.Val(sitem["MSBYS_1"]) / Conversion.Val(sitem["SPHWZL_1"])) / Conversion.Val(sitem["SSYZL_1"]) * 100, 2).ToString();
                        sitem["SMSL_2"] = Round(Conversion.Val(sitem["SMSZL_2"]) / (Conversion.Val(sitem["MSBYS_2"]) / Conversion.Val(sitem["SPHWZL_2"])) / Conversion.Val(sitem["SSYZL_2"]) * 100, 2).ToString();
                        sitem["SMSL_3"] = Round(Conversion.Val(sitem["SMSZL_3"]) / (Conversion.Val(sitem["MSBYS_3"]) / Conversion.Val(sitem["SPHWZL_3"])) / Conversion.Val(sitem["SSYZL_3"]) * 100, 2).ToString();
                    }
                    if (sitem["SMSL_1"] != "" && sitem["SMSL_1"] != "----")
                    {
                        string mlongStr = sitem["SMSL_1"] + "," + sitem["SMSL_2"] + "," + sitem["SMSL_3"];
                        mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 2; vp++)
                            mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkyqdArray);
                        double mMaxKyqd = mkyqdArray[2];
                        double mMinKyqd = mkyqdArray[0];
                        double mMidKyqd = mkyqdArray[1];
                        double mAvgKyqd = mkyqdArray.Average();
                        MItem[0]["JCJGMS"] = "";
                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_MSL"] = "重做";
                            sitem["SPJMSL"] = "重做";
                        }
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sitem["SPJMSL"] = Round(mMidKyqd, 1).ToString("0.0");
                        }

                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sitem["SPJMSL"] = Round(mMidKyqd, 1).ToString("0.0");
                        }

                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sitem["SPJMSL"] = Round(mAvgKyqd, 1).ToString("0.0");
                        }
                    }
                    if (sitem["JPJMSL"] == "重做" || sitem["SPJMSL"] == "重做")
                    {
                        mbhggs = mbhggs + 1;
                        if (sitem["JPJMSL"] == "重做" && sitem["SPJMSL"] == "重做")
                            MItem[0]["HG_MSL"] = "基准受检重做";
                        else
                        {
                            if (sitem["JPJMSL"] == "重做")
                                MItem[0]["HG_MSL"] = "基准重做";

                            else
                                MItem[0]["HG_MSL"] = "受检重做";
                        }
                    }
                    else
                    {
                        if (sitem["SPJMSL"] != "" && sitem["SPJMSL"] != "----")
                            sitem["MSLB"] = Round((Conversion.Val(sitem["SPJMSL"]) / Conversion.Val(sitem["JPJMSL"])) * 100, 0).ToString();
                        MItem[0]["HG_MSL"] = calc_pd(MItem[0]["G_MSL"], sitem["MSLB"]);
                    }
                    if (MItem[0]["HG_MSL"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    MItem[0]["G_MSL"] = "----";
                    sitem["MSLB"] = "-----";
                    MItem[0]["HG_MSL"] = "----";
                }
                if (jcxm.Contains("、初凝时间差、"))
                {
                    if (sitem["CNSJT_1"] != "" && sitem["CNSJT_1"] != "----")
                    {
                        sitem["CNSJC_1"] = (Conversion.Val(sitem["CNSJT_1"]) - Conversion.Val(sitem["CNJZT_1"])).ToString();
                        sitem["CNSJC_2"] = (Conversion.Val(sitem["CNSJT_2"]) - Conversion.Val(sitem["CNJZT_2"])).ToString();
                        sitem["CNSJC_3"] = (Conversion.Val(sitem["CNSJT_3"]) - Conversion.Val(sitem["CNJZT_3"])).ToString();
                    }
                    if (sitem["CNSJC_1"] != "" && sitem["CNSJC_1"] != "----")
                    {
                        string mlongStr = sitem["CNSJC_1"] + ", " + sitem["CNSJC_2"] + ", " + sitem["CNSJC_3"];
                        mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 2; vp++)
                            mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkyqdArray);
                        double mMaxKyqd = mkyqdArray[2];
                        double mMinKyqd = mkyqdArray[0];
                        double mMidKyqd = mkyqdArray[1];
                        double mAvgKyqd = mkyqdArray.Average();
                        MItem[0]["JCJGMS"] = "";
                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) > 30)
                        {
                            MItem[0]["HG_CNSJC"] = "重做";
                            sitem["CNPJSJC"] = "重做";
                        }
                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) <= 30)
                            sitem["CNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) > 30)
                            sitem["CNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) <= 30)
                            sitem["CNPJSJC"] = Round(mAvgKyqd, 0).ToString();
                    }
                    if (sitem["CNPJSJC"] == "重做")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                        MItem[0]["HG_CNSJC"] = calc_pd(MItem[0]["G_CNSJC"], sitem["CNPJSJC"]);
                    }

                    if (MItem[0]["HG_CNSJC"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    MItem[0]["G_CNSJC"] = "----";
                    sitem["CNPJSJC"] = "-----";
                    MItem[0]["HG_CNSJC"] = "----";
                }
                if (jcxm.Contains("、终凝时间差、"))
                {
                    if (sitem["ZNSJT_1"] != "" && sitem["ZNSJT_1"] != "----")
                    {
                        sitem["ZNSJC_1"] = (Conversion.Val(sitem["ZNSJT_1"]) - Conversion.Val(sitem["ZNJZT_1"])).ToString();
                        sitem["ZNSJC_2"] = (Conversion.Val(sitem["ZNSJT_2"]) - Conversion.Val(sitem["ZNJZT_2"])).ToString();
                        sitem["ZNSJC_3"] = (Conversion.Val(sitem["ZNSJT_3"]) - Conversion.Val(sitem["ZNJZT_3"])).ToString();
                    }
                    if (sitem["ZNSJC_1"] != "" && sitem["ZNSJC_1"] != "----")
                    {
                        string mlongStr = sitem["ZNSJC_1"] + "," + sitem["ZNSJC_2"] + "," + sitem["ZNSJC_3"];
                        mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 2; vp++)
                            mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkyqdArray);
                        double mMaxKyqd = mkyqdArray[2];
                        double mMinKyqd = mkyqdArray[0];
                        double mMidKyqd = mkyqdArray[1];
                        double mAvgKyqd = mkyqdArray.Average();


                        MItem[0]["JCJGMS"] = "";
                        //计算抗压平均、达到设计强度、及进行单组合格判定


                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) > 30)
                        {
                            MItem[0]["HG_ZNSJC"] = "重做";
                            sitem["ZNPJSJC"] = "重做";
                        }
                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) <= 30)
                            sitem["ZNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) > 30)
                            sitem["ZNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) <= 30)
                            sitem["ZNPJSJC"] = Round(mAvgKyqd, 0).ToString();
                    }


                    if (sitem["ZNPJSJC"] == "重做")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                        MItem[0]["HG_ZNSJC"] = calc_pd(MItem[0]["G_ZNSJC"], sitem["ZNPJSJC"]);
                    }



                    if (MItem[0]["HG_ZNSJC"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    MItem[0]["G_ZNSJC"] = "----";
                    sitem["ZNPJSJC"] = "---- - ";
                    MItem[0]["HG_ZNSJC"] = "----";
                }

                for (int qdi = 2; qdi <= 4; qdi++)
                {
                    double mhsxs = 0;
                    string mlq = string.Empty;
                    if (qdi == 1)
                        mlq = "1d";
                    if (qdi == 2)
                        mlq = "3d";
                    if (qdi == 3)
                        mlq = "7d";
                    if (qdi == 4)
                        mlq = "28d";
                    mhsxs = 1;
                    if (Conversion.Val(sitem["SJCD" + mlq]) == 100)
                        mhsxs = 0.95;
                    if (Conversion.Val(sitem["SJCD" + mlq]) == 150)
                        mhsxs = 1;
                    if (Conversion.Val(sitem["SJCD" + mlq]) == 200)
                        mhsxs = 1.05;
                    if (jcxm.Contains("、" + mlq + "抗压强度比、"))
                    {
                        if (sitem["JHZ" + mlq + "1_1"] != "" && sitem["JHZ" + mlq + "1_1"] != "----")
                        {
                            for (xd1 = 1; xd1 <= 3; xd1++)
                            {
                                sum = 0;
                                for (xd2 = 1; xd2 <= 3; xd2++)
                                {
                                    md1 = Conversion.Val(sitem["JHZ" + mlq + xd1 + "_" + xd2]);
                                    md2 = Round(1000 * md1 / (Conversion.Val(sitem["SJCD" + mlq]) * Conversion.Val(sitem["SJKD" + mlq])), 1);
                                    Arrmd[xd2] = md2;
                                    sum = sum + Arrmd[xd2];
                                    sitem["JQD" + mlq + xd1 + "_" + xd2] = md2.ToString("0.0");
                                }
                                string mlongStr = Arrmd[1] + "," + Arrmd[2] + "," + Arrmd[3];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                double mMaxKyqd = mkyqdArray[2];
                                double mMinKyqd = mkyqdArray[0];
                                double mMidKyqd = mkyqdArray[1];
                                double mAvgKyqd = mkyqdArray.Average();
                                MItem[0]["JCJGMS"] = "";
                                //计算抗压平均、达到设计强度、及进行单组合格判定

                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sitem["JQDDBZ" + mlq + xd1] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sitem["JQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sitem["JQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sitem["JQDDBZ" + mlq + xd1] = Round(mAvgKyqd * mhsxs, 1).ToString("0.0");
                            }
                        }
                        if (sitem["SHZ" + mlq + "1_1"] != "" && sitem["SHZ" + mlq + "1_1"] != "----")
                        {
                            for (xd1 = 1; xd1 <= 3; xd1++)
                            {
                                sum = 0;
                                for (xd2 = 1; xd2 <= 3; xd2++)
                                {
                                    md1 = Conversion.Val(sitem["SHZ" + mlq + xd1 + "_" + xd2]);
                                    md2 = Round(1000 * md1 / (Conversion.Val(sitem["SJCD" + mlq]) * Conversion.Val(sitem["SJKD" + mlq])), 1);
                                    Arrmd[xd2] = md2;
                                    sum = sum + Arrmd[xd2];
                                    sitem["SQD" + mlq + xd1 + "_" + xd2] = md2.ToString("0.0");
                                }
                                string mlongStr = Arrmd[1] + "," + Arrmd[2] + "," + Arrmd[3];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                double mMaxKyqd = mkyqdArray[2];
                                double mMinKyqd = mkyqdArray[0];
                                double mMidKyqd = mkyqdArray[1];
                                double mAvgKyqd = mkyqdArray.Average();
                                MItem[0]["JCJGMS"] = "";
                                //计算抗压平均、达到设计强度、及进行单组合格判定

                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sitem["SQDDBZ" + mlq + xd1] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sitem["SQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sitem["SQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sitem["SQDDBZ" + mlq + xd1] = Round(mAvgKyqd * mhsxs, 1).ToString("0.0");
                            }
                        }
                        if (sitem["JQDDBZ" + mlq + "1"] != "" && sitem["JQDDBZ" + mlq + "1"] != "----")
                        {
                            for (xd1 = 1; xd1 <= 3; xd1++)
                            {
                                if (sitem["JQDDBZ" + mlq + xd1] == "重做" || sitem["SQDDBZ" + mlq + xd1] == "重做")
                                    sitem["QDB" + mlq + xd1] = "重做";
                                else
                                    sitem["QDB" + mlq + xd1] = Round(Conversion.Val(sitem["SQDDBZ" + mlq + xd1]) / Conversion.Val(sitem["JQDDBZ" + mlq + xd1]) * 100, 0).ToString();
                            }
                        }
                        if (sitem["QDB" + mlq + "1"] != "" && sitem["QDB" + mlq + "1"] != "----")
                        {
                            if (sitem["QDB" + mlq + "1"] == "重做" || sitem["QDB" + mlq + "2"] == "重做" || sitem["QDB" + mlq + "3"] == "重做")
                                sitem["PJQDB" + mlq] = "重做";
                            else
                            {
                                string mlongStr = sitem["QDB" + mlq + "1"] + "," + sitem["QDB" + mlq + "2"] + "," + sitem["QDB" + mlq + "3"];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                double mMaxKyqd = mkyqdArray[2];
                                double mMinKyqd = mkyqdArray[0];
                                double mMidKyqd = mkyqdArray[1];
                                double mAvgKyqd = mkyqdArray.Average();
                                MItem[0]["JCJGMS"] = "";
                                //计算抗压平均、达到设计强度、及进行单组合格判定

                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 0))
                                    sitem["PJQDB" + mlq] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 0))
                                    sitem["PJQDB" + mlq] = Round(mMidKyqd, 0).ToString();
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 0))
                                    sitem["PJQDB" + mlq] = Round(mMidKyqd, 0).ToString();
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 0))
                                    sitem["PJQDB" + mlq] = Round(mAvgKyqd, 0).ToString();

                            }

                        }
                        if (sitem["PJQDB" + mlq] == "重做")
                        {
                            mbhggs = mbhggs + 1;
                            MItem[0]["HG_KYQD" + mlq] = "重做";
                        }
                        else
                            MItem[0]["HG_KYQD" + mlq] = calc_pd(MItem[0]["G_KYQD" + mlq], sitem["PJQDB" + mlq]);


                        if (MItem[0]["HG_KYQD" + mlq] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        MItem[0]["G_KYQD" + mlq] = "----";
                        sitem["PJQDB" + mlq] = "-----";
                        MItem[0]["HG_KYQD" + mlq] = "----";
                    }
                }
                if (jcxm.Contains("、收缩率比、"))
                {
                    sitem["SSLJ1"] = Round((Conversion.Val(sitem["SSLJL0_1"]) - Conversion.Val(sitem["SSLJLT_1"])) / Conversion.Val(sitem["SSLJLB_1"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLJ2"] = Round((Conversion.Val(sitem["SSLJL0_2"]) - Conversion.Val(sitem["SSLJLT_2"])) / Conversion.Val(sitem["SSLJLB_2"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLJ3"] = Round((Conversion.Val(sitem["SSLJL0_3"]) - Conversion.Val(sitem["SSLJLT_3"])) / Conversion.Val(sitem["SSLJLB_3"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLS1"] = Round((Conversion.Val(sitem["SSLSL0_1"]) - Conversion.Val(sitem["SSLSLT_1"])) / Conversion.Val(sitem["SSLSLB_1"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLS2"] = Round((Conversion.Val(sitem["SSLSL0_2"]) - Conversion.Val(sitem["SSLSLT_2"])) / Conversion.Val(sitem["SSLSLB_2"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLS3"] = Round((Conversion.Val(sitem["SSLSL0_3"]) - Conversion.Val(sitem["SSLSLT_3"])) / Conversion.Val(sitem["SSLSLB_3"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLB1"] = Round(Conversion.Val(sitem["SSLS1"]) / Conversion.Val(sitem["SSLJ1"]) * 100, 1).ToString();
                    sitem["SSLB2"] = Round(Conversion.Val(sitem["SSLS2"]) / Conversion.Val(sitem["SSLJ2"]) * 100, 1).ToString();
                    sitem["SSLB3"] = Round(Conversion.Val(sitem["SSLS3"]) / Conversion.Val(sitem["SSLJ3"]) * 100, 1).ToString();
                    sitem["SSLB"] = Round((Conversion.Val(sitem["SSLB1"]) + Conversion.Val(sitem["SSLB2"]) + Conversion.Val(sitem["SSLB3"])) / 3, 0).ToString();

                    MItem[0]["HG_SSLB"] = calc_pd(MItem[0]["G_SSLB"], sitem["SSLB"]);
                    if (MItem[0]["HG_SSLB"] == "不符合")
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
                    MItem[0]["HG_SSLB"] = "----";
                    MItem[0]["G_SSLB"] = "----";
                }
                if (jcxm.Contains("、吸水量比、"))
                {
                    double mjxsl1 = Conversion.Val(sitem["JXSLM1_1"]) - Conversion.Val(sitem["JXSLM0_1"]);
                    double mjxsl2 = Conversion.Val(sitem["JXSLM1_2"]) - Conversion.Val(sitem["JXSLM0_2"]);
                    double mjxsl3 = Conversion.Val(sitem["JXSLM1_3"]) - Conversion.Val(sitem["JXSLM0_3"]);
                    double mjxsl4 = Conversion.Val(sitem["JXSLM1_4"]) - Conversion.Val(sitem["JXSLM0_4"]);
                    double mjxsl5 = Conversion.Val(sitem["JXSLM1_5"]) - Conversion.Val(sitem["JXSLM0_5"]);
                    double mjxsl6 = Conversion.Val(sitem["JXSLM1_6"]) - Conversion.Val(sitem["JXSLM0_6"]);
                    double msxsl1 = Conversion.Val(sitem["SXSLM1_1"]) - Conversion.Val(sitem["SXSLM0_1"]);
                    double msxsl2 = Conversion.Val(sitem["SXSLM1_2"]) - Conversion.Val(sitem["SXSLM0_2"]);
                    double msxsl3 = Conversion.Val(sitem["SXSLM1_3"]) - Conversion.Val(sitem["SXSLM0_3"]);
                    double msxsl4 = Conversion.Val(sitem["SXSLM1_4"]) - Conversion.Val(sitem["SXSLM0_4"]);
                    double msxsl5 = Conversion.Val(sitem["SXSLM1_5"]) - Conversion.Val(sitem["SXSLM0_5"]);
                    double msxsl6 = Conversion.Val(sitem["SXSLM1_6"]) - Conversion.Val(sitem["SXSLM0_6"]);
                    sitem["JXSL"] = Round((mjxsl1 + mjxsl2 + mjxsl3 + mjxsl4 + mjxsl5 + mjxsl6) / 6, 0).ToString();
                    sitem["SXSL"] = Round((msxsl1 + msxsl2 + msxsl3 + msxsl4 + msxsl5 + msxsl6) / 6, 0).ToString();
                    sitem["XSLB"] = Round(100 * (Conversion.Val(sitem["SXSL"]) / Conversion.Val(sitem["JXSL"])), 0).ToString();
                    MItem[0]["HG_XSLB"] = calc_pd(MItem[0]["G_XSLB"], sitem["XSLB"]);


                    if (MItem[0]["HG_XSLB"] == "不符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["XSLB"] = "----";
                    MItem[0]["HG_XSLB"] = "----";
                    MItem[0]["G_XSLB"] = "----";
                }
                MItem[0]["JCJGMS"] = "";
                if (sitem["SFFJ"] == "1")
                {
                    if (mbhggs > 0)
                    {
                        sitem["JCJG"] = "不合格";
                        MItem[0]["JCJGMS"] = "该组试件不符合" + MItem[0]["PDBZ"] + "标准要求。";
                        if (mFlag_Bhg && mFlag_Hg)
                            MItem[0]["JCJGMS"] = "该组试件所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                    }
                    else
                    {
                        MItem[0]["JCJGMS"] = "该组试件所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                        sitem["JCJG"] = "合格";
                    }
                }
                else
                {
                    if (mbhggs == 0)
                    {
                        MItem[0]["JCJGMS"] = "该组试件所检项目符合" + MItem[0]["PDBZ"] + "标准要求";
                        sitem["JCJG"] = "合格";
                    }
                    if (mbhggs > 0)
                    {
                        MItem[0]["JCJGMS"] = "该组试件不符合" + MItem[0]["PDBZ"] + "标准要求";
                        sitem["JCJG"] = "不合格";
                        if (mFlag_Bhg && mFlag_Hg)
                            MItem[0]["JCJGMS"] = "该组试件所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";

                    }
                }
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");
            }
            //综合判断
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            #endregion
            /************************ 代码结束 *********************/


        }
    }
}
