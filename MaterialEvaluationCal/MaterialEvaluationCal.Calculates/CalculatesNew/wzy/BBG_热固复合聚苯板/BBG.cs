using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


namespace Calculates
{
    public class BBG : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;
            string mhgjg, mbhgjg;
            string djjg;
            int mbHggs1, mbHggs2, mbHggs3, mbHggs = 0;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            bool mSFwc;
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
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

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_BBG_DJ"];
            var mrsrfDj = dataExtra["BZ_RF_DJ"];
            var MItem = data["M_BBG"];
            var SItem = data["S_BBG"];
            //var tempTable = data["MS_BW"];
            #endregion

            #region  计算开始
            MItem[0]["JCJGMS"] = "";
            string which = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            foreach (var sitem in SItem)
            {
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(sitem["CPMC"]) && x["LB"].Contains(sitem["XH"]));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    MItem[0]["G_DRXS2"] = mrsDj_Filter["DRXS2"];
                    MItem[0]["G_TJXSL"] = mrsDj_Filter["TJXSL"];
                    MItem[0]["G_KYQD"] = mrsDj_Filter["KYQD"];
                    MItem[0]["G_YSQD"] = mrsDj_Filter["YSQD"];
                    MItem[0]["G_BGMD"] = mrsDj_Filter["MD"];
                    MItem[0]["G_CCWDX"] = mrsDj_Filter["CCWDX"];
                    MItem[0]["G_KLQD"] = mrsDj_Filter["KLQD"];
                    MItem[0]["G_RHXS"] = mrsDj_Filter["RHXS"];
                    MItem[0]["G_CDPC"] = mrsDj_Filter["CDPC"];
                    MItem[0]["G_KDPC"] = mrsDj_Filter["KDPC"];
                    MItem[0]["G_HDPC"] = mrsDj_Filter["HDPC"];
                    which = mrsDj_Filter["WHICH"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "不下结论";
                    MItem[0]["JCJGMS"] = "依据不详";
                }
                double md1, md2, pjmd, md, sum;
                int xd, Gs, mcd, mdwz;
                string bl;
                bool flag, sign;
                double[] nArr;
                bool mark;
                mark = true;
                mhgjg = "";
                mbhgjg = "";
                djjg = "";
                mbHggs1 = 0;
                mbHggs2 = 0;
                mbHggs3 = 0;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                #region 导热系数(25℃)
                if (jcxm.Contains("、导热系数(25℃)、"))
                {
                    jcxmCur = "导热系数(25℃)";
                    mcd = MItem[0]["G_DRXS2"].Length;
                    mdwz = MItem[0]["G_DRXS2"].IndexOf(".") + 1;
                    mcd = mcd - mdwz;
                    sitem["DRXS"] = Round(GetSafeDouble(sitem["DRXS2"]), mcd).ToString();
                    sitem["DRXS2"] = sitem["DRXS"];
                    if (IsQualified(MItem[0]["G_DRXS2"], sitem["DRXS2"], true) == "符合")
                    {
                        MItem[0]["HG_DRXS2"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_DRXS2"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["DRXS2"] = "----";
                    MItem[0]["HG_DRXS2"] = "----";
                    MItem[0]["G_DRXS2"] = "----";
                }
                #endregion

                #region 尺寸稳定性
                if (jcxm.Contains("、尺寸稳定性、"))
                {

                    if (IsQualified(MItem[0]["G_CCWDX"], sitem["CCWDXC"], true) == "符合")
                    {
                        MItem[0]["HG_CCWDXC"] = "合格";
                    }
                    else
                    {
                        jcxmCur = "尺寸稳定性(长)";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_CCWDXC"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                    if (IsQualified(MItem[0]["G_CCWDX"], sitem["CCWDXK"], true) == "符合")
                    {
                        MItem[0]["HG_CCWDXK"] = "合格";
                    }
                    else
                    {
                        jcxmCur = "尺寸稳定性(宽)";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_CCWDXK"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                    if (IsQualified(MItem[0]["G_CCWDX"], sitem["CCWDXH"], true) == "符合")
                    {
                        MItem[0]["HG_CCWDXH"] = "合格";
                    }
                    else
                    {
                        jcxmCur = "尺寸稳定性(厚)";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_CCWDXH"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["CCWDXC"] = "----";
                    sitem["CCWDXK"] = "----";
                    sitem["CCWDXH"] = "----";
                    MItem[0]["HG_CCWDXC"] = "----";
                    MItem[0]["HG_CCWDXK"] = "----";
                    MItem[0]["HG_CCWDXH"] = "----";
                    MItem[0]["G_CCWDX"] = "----";
                }
                #endregion

                #region 密度
                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";
                    sitem["MDPC"] = Round((GetSafeDouble(sitem["BGMD"]) - GetSafeDouble(sitem["BCMD"])) / GetSafeDouble(sitem["BCMD"]), 0).ToString();
                    MItem[0]["G_MD"] = "标称密度±10 % ";
                    if (IsQualified(MItem[0]["G_BGMD"], sitem["MDPC"], true) == "符合")
                    {
                        MItem[0]["HG_BGMD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_BGMD"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["BGMD"] = "----";
                    MItem[0]["HG_BGMD"] = "----";
                    MItem[0]["G_BGMD"] = "----";
                    MItem[0]["G_MD"] = "----";
                }
                #endregion

                #region 抗拉强度、垂直板面方向的抗拉强度
                if (jcxm.Contains("、抗拉强度、") || jcxm.Contains("、垂直板面方向的抗拉强度、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "抗拉强度,垂直板面方向的抗拉强度");
                    if (IsQualified(MItem[0]["G_KLQD"], sitem["KLQD"], true) == "符合")
                    {
                        MItem[0]["HG_KLQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_KLQD"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["KLQD"] = "----";
                    MItem[0]["HG_KLQD"] = "----";
                    MItem[0]["G_KLQD"] = "----";
                }
                #endregion

                #region 压缩强度
                if (jcxm.Contains("、压缩强度、"))
                {
                    //if (Conversion.Val(sitem["YSQD"]) == 0)
                    //bool sjtabcalc = false;
                    jcxmCur = "压缩强度";
                    if (IsQualified(MItem[0]["G_YSQD"], sitem["YSQD"], true) == "符合")
                    {
                        MItem[0]["HG_YSQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_YSQD"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["YSQD"] = "----";
                    MItem[0]["HG_YSQD"] = "----";
                    MItem[0]["G_YSQD"] = "----";
                }
                #endregion

                #region 体积吸水率
                if (jcxm.Contains("、体积吸水率、"))
                {
                    jcxmCur = "体积吸水率";
                    if (IsQualified(MItem[0]["G_TJXSL"], sitem["TJXSL"], true) == "符合")
                    {
                        MItem[0]["HG_TJXSL"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_TJXSL"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["TJXSL"] = "----";
                    MItem[0]["HG_TJXSL"] = "----";
                    MItem[0]["G_TJXSL"] = "----";
                }
                #endregion

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                    if (IsQualified(MItem[0]["G_KYQD"], sitem["KYQD"], true) == "符合")
                    {
                        MItem[0]["HG_KYQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_KYQD"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["KYQD"] = "----";
                    MItem[0]["HG_KYQD"] = "----";
                    MItem[0]["G_KYQD"] = "----";
                }
                #endregion

                #region 软化系数
                if (jcxm.Contains("、软化系数、"))
                {
                    jcxmCur = "软化系数";
                    if (IsQualified(MItem[0]["G_RHXS"], sitem["RHXS"], true) == "符合")
                    {
                        MItem[0]["HG_RHXS"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_RHXS"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["RHXS"] = "----";
                    MItem[0]["HG_RHXS"] = "----";
                    MItem[0]["G_RHXS"] = "----";
                }
                #endregion

                #region 燃烧性能
                //if (jcxm.Contains("、燃烧性能、"))
                //{
                //    if (sitem["RSDJ"].Contains("A2"))
                //    {
                //        IDictionary<string, string> mrsrfDj_Filter = new Dictionary<string, string>();
                //        if (sitem["YPXZ"].Trim() == "管状")
                //        {
                //            if (GetSafeDouble(sitem["GZWJ"].Trim()) < 300)

                //                mrsrfDj_Filter = mrsrfDj.FirstOrDefault(x => x["RSDJ"].Contains(sitem["RSDJ"].Trim()) && x["YPXZ"].Equals("管状"));
                //            else
                //                mrsrfDj_Filter = mrsrfDj.FirstOrDefault(x => x["RSDJ"].Contains(sitem["RSDJ"].Trim()) && x["YPXZ"].Equals("平板状"));
                //        }
                //        else
                //            mrsrfDj_Filter = mrsrfDj.FirstOrDefault(x => x["RSDJ"].Contains(sitem["RSDJ"].Trim()) && x["YPXZ"].Equals("平板状"));
                //        if (mrsrfDj_Filter != null && mrsrfDj_Filter.Count > 0)
                //        {
                //            sitem["G_ZRZ"] = mrsrfDj_Filter["G_ZRZ1"].Trim();
                //            if (sitem["RSDJ"].Trim() == "A2")
                //            {
                //                for (xd = 1; xd <= 5; xd++)
                //                {
                //                    if (sitem["JGLX" + xd].Trim() == "主要组分")
                //                    {
                //                        sitem["G_JGRZ" + xd] = mrsrfDj_Filter["G_ZRZ1"].Trim();
                //                        sitem["JGDW" + xd] = "MJ/kg";
                //                    }
                //                    else if (sitem["JGLX" + xd].Trim() == "内部次要组分" || sitem["JGLX" + xd].Trim() == "外部次要组分")
                //                    {
                //                        sitem["G_JGRZ" + xd] = mrsrfDj_Filter["G_ZRZ2"].Trim();
                //                        sitem["JGDW" + xd] = "MJ/m&scsup2&scend";
                //                    }
                //                    else
                //                    {
                //                        sitem["G_JGRZ" + xd] = "----";
                //                        sitem["G_JGDW" + xd] = "----";
                //                        sitem["JGDW" + xd] = "----";
                //                        sitem["W_PCS" + xd] = "----";
                //                    }
                //                }
                //            }
                //            sitem["G_FIGRA"] = mrsrfDj_Filter["G_FIGRA"].Trim();
                //            sitem["G_SMOGRA"] = mrsrfDj_Filter["G_SMOGRA"].Trim();
                //            sitem["G_THR"] = mrsrfDj_Filter["G_THR"].Trim();
                //            sitem["G_TSP"] = mrsrfDj_Filter["G_TSP"].Trim();
                //        }

                //        if (jcxm.Contains("、燃烧性能、"))
                //        {
                //            sign = true;
                //            sign = IsNumeric(sitem["PCS"]) && !string.IsNullOrEmpty(sitem["PCS"]) ? sign : false;
                //            if (sign)
                //                sitem["GH_ZRZ"] = calc_PB(sitem["G_ZRZ"], sitem["PCS"], false);
                //            for (xd = 1; xd <= 5; xd++)
                //            {
                //                if (sitem["JGDW" + xd].Trim() == "MJ/kg")
                //                    sitem["W_PCS" + xd] = sitem["PCS1_" + xd];
                //                else if (sitem["JGDW" + xd].Trim() == "MJ/m&scsup2&scend")
                //                    sitem["W_PCS" + xd] = sitem["PCS2_" + xd];
                //                else
                //                    sitem["W_PCS" + xd] = "----";
                //                sitem["GH_JGRZ" + xd] = calc_PB(sitem["G_JGRZ" + xd], sitem["W_PCS" + xd], false);
                //            }
                //            mbHggs2 = sitem["GH_ZRZ"] == "不合格" ? mbHggs2 + 1 : mbHggs2;
                //            for (xd = 1; xd <= 5; xd++)
                //                mbHggs2 = sitem["GH_JGRZ" + xd] == "不合格" ? mbHggs2 + 1 : mbHggs2;
                //            if (mbHggs2 == 0)
                //                sitem["GH_RSRZ"] = "符合" + sitem["RSDJ"].Trim() + "级";
                //            else
                //                sitem["GH_RSRZ"] = "不符合" + sitem["RSDJ"].Trim() + "级";
                //        }
                //        else
                //        {
                //            for (xd = 1; xd <= 5; xd++)
                //            {
                //                sitem["GH_JGRZ" + xd] = "----";
                //                sitem["G_JGRZ" + xd] = "----";
                //                sitem["W_PCS" + xd] = "----";
                //            }
                //            sitem["GH_ZRZ"] = "----";
                //            sitem["PCS"] = "----";
                //            sitem["G_ZRZ"] = "----";
                //        }


                //        if (jcxm.Contains("、燃烧性能、"))
                //        {
                //            sign = true;
                //            sitem["FIGRA"] = sitem["FIGRA2"];
                //            sign = IsNumeric(sitem["FIGRA"]) && !string.IsNullOrEmpty(sitem["FIGRA"]) ? sign : false;
                //            if (sign)
                //                sitem["GH_FIGRA"] = calc_PB(sitem["G_FIGRA"], sitem["FIGRA"], false);
                //            sign = true;
                //            sign = IsNumeric(sitem["THR"]) && !string.IsNullOrEmpty(sitem["THR"]) ? sign : false;
                //            if (sign)
                //                MItem[0]["GH_THR"] = calc_PB(sitem["G_THR"], sitem["THR"], false);
                //            if (GetSafeDouble(sitem["HGS3"].Trim()) <= 1)
                //            {
                //                sitem["W_HXMY"] = "是";
                //                sitem["GH_HXMY"] = "合格";
                //            }
                //            else
                //            {
                //                sitem["W_HXMY"] = "否";
                //                sitem["GH_HXMY"] = "不合格";
                //            }
                //            mbHggs3 = sitem["GH_HXMY"] == "不合格" ? mbHggs3 + 1 : mbHggs3;
                //            mbHggs3 = MItem[0]["GH_THR"] == "不合格" ? mbHggs3 + 1 : mbHggs3;
                //            mbHggs3 = sitem["GH_FIGRA"] == "不合格" ? mbHggs3 + 1 : mbHggs3;
                //            if (mbHggs3 == 0)
                //                sitem["GH_DTRS"] = "符合" + sitem["RSDJ"].Trim() + "级";
                //            else
                //                sitem["GH_DTRS"] = "不符合" + sitem["RSDJ"].Trim() + "级";
                //        }
                //        else
                //        {
                //            MItem[0]["GH_THR"] = "----";
                //            sitem["THR"] = "----";
                //            sitem["G_THR"] = "----";
                //            sitem["GH_FIGRA"] = "----";
                //            sitem["FIGRA"] = "----";
                //            sitem["G_FIGRA"] = "----";
                //            sitem["GH_HXMY"] = "----";
                //            sitem["W_HXMY"] = "----";
                //        }


                //        if (mbHggs3 == 0 && mbHggs2 == 0 && mbHggs1 == 0)
                //        {
                //            sitem["JCJG"] = "符合" + sitem["RSDJ"].Trim() + "级";
                //            MItem[0]["JCJGMS"] = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中" + sitem["RSDJ"].Trim() + "级要求";
                //        }
                //        else
                //        {
                //            sitem["JCJG"] = "不符合" + sitem["RSDJ"].Trim() + "级";
                //            mAllHg = false;
                //            MItem[0]["JCJGMS"] = "该试样燃烧性能等级不符合" + MItem[0]["PDBZ"] + "中" + sitem["RSDJ"].Trim() + "级要求";
                //        }
                //    }
                //    if (sitem["RSDJ"].Contains("B1"))
                //    {
                //        IList<IDictionary<string, string>> mrsrfDj_Where = new List<IDictionary<string, string>>();
                //        if (sitem["YPXZ"] == "管状")
                //        {
                //            if (GetSafeDouble(sitem["GZWJ"].Trim()) < 300)
                //                mrsrfDj_Where = mrsrfDj.Where(x => x["YPXZ"].Equals("管状") && x["RFDJ"].Equals("B1")).ToList();
                //            else
                //                mrsrfDj_Where = mrsrfDj.Where(x => x["YPXZ"].Equals("平板状") && x["RFDJ"].Equals("B1")).ToList();
                //        }
                //        else
                //            mrsrfDj_Where = mrsrfDj.Where(x => x["YPXZ"].Equals("平板状") && x["RFDJ"].Equals("B1")).ToList();


                //        var mrsrfDj_Filter = mrsrfDj_Where[0];
                //        Gs = mrsrfDj.Count();
                //        for (xd = 1; xd <= Gs; xd++)
                //        {
                //            if (xd == 1)
                //            {
                //                if (calc_PB(mrsrfDj_Filter["G_FIGRA"], sitem["FIGRA2"], false) == "合格" && calc_PB(mrsrfDj_Filter["G_THR"], sitem["THR"], false) == "合格")
                //                {
                //                    sitem["GH_DTRS"] = "符合B级";
                //                    sitem["G_FIGRA"] = mrsrfDj_Filter["G_FIGRA"];
                //                    sitem["G_THR"] = mrsrfDj_Filter["G_THR"];
                //                    sitem["GH_FIGRA"] = "合格";
                //                    MItem[0]["GH_THR"] = "合格";
                //                    djjg = "B";
                //                    sitem["FIGRA"] = sitem["FIGRA2"];
                //                    break;
                //                }
                //            }
                //            else
                //            {
                //                sitem["G_FIGRA"] = mrsrfDj_Filter["G_FIGRA"];
                //                sitem["G_THR"] = mrsrfDj_Filter["G_THR"];
                //                sitem["FIGRA"] = sitem["FIGRA4"];
                //                sitem["GH_FIGRA"] = calc_PB(mrsrfDj_Filter["G_FIGRA"], sitem["FIGRA4"], false);
                //                MItem[0]["GH_THR"] = calc_PB(mrsrfDj_Filter["G_THR"], sitem["THR"], false);
                //                djjg = mrsrfDj_Filter["RSDJ"];
                //                if (!((calc_PB(mrsrfDj_Filter["G_FIGRA"], sitem["FIGRA4"], false) == "不合格" || calc_PB(mrsrfDj_Filter["G_THR"], sitem["THR"], false) == "不合格")))
                //                    sitem["GH_DTRS"] = "符合" + mrsrfDj_Filter["RSDJ"] + "级";
                //                else
                //                {
                //                    sitem["GH_DTRS"] = "不符合B&scsub1&scend级";
                //                    mbHggs1 = mbHggs1 + 1;
                //                }
                //                break;
                //            }
                //            mrsrfDj_Filter = mrsrfDj_Where[xd];
                //        }

                //        if (GetSafeDouble(sitem["HGS3"].Trim()) <= 1)
                //        {
                //            sitem["W_HXMY"] = "是";
                //            sitem["GH_HXMY"] = "合格";
                //        }
                //        else
                //        {
                //            sitem["W_HXMY"] = "否";
                //            sitem["GH_HXMY"] = "不合格";
                //            mbHggs1 = mbHggs1 + 1;
                //        }
                //        if (GetSafeDouble(sitem["HGS4"].Trim()) <= 1)
                //        {
                //            sitem["G_YJGD"] = "60s内，≤150 mm";
                //            sitem["W_YJGD"] = "是";
                //            sitem["GH_YJGD"] = "合格";
                //        }
                //        else
                //        {
                //            sitem["G_YJGD"] = "60s内，≤150 mm";
                //            sitem["W_YJGD"] = "否";
                //            sitem["GH_YJGD"] = "不合格";
                //            mbHggs1 = mbHggs1 + 1;
                //        }
                //        if (GetSafeDouble(sitem["HGS1"].Trim()) <= 1)
                //        {
                //            sitem["G_SFYR"] = "60s内无燃烧滴落物引燃滤纸现象";
                //            sitem["W_SFYR"] = "否";
                //            sitem["GH_SFYR"] = "合格";
                //        }
                //        else
                //        {
                //            sitem["G_SFYR"] = "60s内无燃烧滴落物引燃滤纸现象";
                //            sitem["W_SFYR"] = "是";
                //            sitem["GH_SFYR"] = "不合格";
                //            mbHggs1 = mbHggs1 + 1;
                //        }
                //        if (sitem["GCBW"].Contains("外墙"))
                //        {
                //            sitem["G_RSYZZ"] = "≥30";
                //            sign = true;
                //            if (sitem["GH_RSYZZ"] == "合格" && (sitem["W_RSYZZ"] == "" || sitem["W_RSYZZ"].Contains("低于")))
                //                sitem["W_RSYZZ"] = "不低于30";

                //            else if (sitem["GH_RSYZZ"] == "不合格" && (sitem["W_RSYZZ"] == "" || sitem["W_RSYZZ"].Contains("低于")))
                //                sitem["W_RSYZZ"] = "低于30";
                //            else
                //            {
                //                sign = IsNumeric(sitem["W_RSYZZ"]) && !string.IsNullOrEmpty(sitem["W_RSYZZ"]) ? sign : false;
                //                if (sign)
                //                    sitem["GH_RSYZZ"] = calc_PB(sitem["G_RSYZZ"], sitem["W_RSYZZ"], false);
                //            }
                //            mbHggs1 = sitem["GH_RSYZZ"] == "不合格" ? mbHggs1 + 1 : mbHggs1;
                //        }
                //        else
                //        {
                //            sitem["GH_RSYZZ"] = "----";
                //            sitem["G_RSYZZ"] = "----";
                //            sitem["W_RSYZZ"] = "----";

                //        }
                //        sitem["RSCDJ"] = djjg;
                //        if (mbHggs1 > 0)
                //        {
                //            sitem["JCJG"] = "不符合B&scsub1&scend级";
                //            mAllHg = false;
                //            MItem[0]["JCJGMS"] = "该试样燃烧性能等级不符合" + MItem[0]["PDBZ"] + "中B&scsub1&scend级要求";
                //        }
                //        else
                //        {
                //            sitem["JCJG"] = "符合B&scsub1&scend级";
                //            MItem[0]["JCJGMS"] = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中B&scsub1&scend级要求";
                //        }
                //    }
                //    if (sitem["RSDJ"].Contains("B2") && !sitem["RSDJ"].Contains("E"))
                //    {
                //        IList<IDictionary<string, string>> mrsrfDj_Where = new List<IDictionary<string, string>>();
                //        if (sitem["YPXZ"] == "管状")
                //        {
                //            if (GetSafeDouble(sitem["GZWJ"].Trim()) < 300)
                //                mrsrfDj_Where = mrsrfDj.Where(x => x["YPXZ"].Equals("管状") && x["RFDJ"].Equals("B2")).ToList();
                //            else
                //                mrsrfDj_Where = mrsrfDj.Where(x => x["YPXZ"].Equals("平板状") && x["RFDJ"].Equals("B2")).ToList();
                //        }
                //        else
                //            mrsrfDj_Where = mrsrfDj.Where(x => x["YPXZ"].Equals("平板状") && x["RFDJ"].Equals("B2")).ToList();


                //        var mrsrfDj_Filter = mrsrfDj_Where[0];
                //        Gs = mrsrfDj.Count();
                //        for (xd = 1; xd <= Gs; xd++)
                //        {
                //            sitem["G_FIGRA"] = mrsrfDj_Filter["G_FIGRA"];
                //            sitem["G_THR"] = mrsrfDj_Filter["G_THR"];
                //            sitem["FIGRA"] = sitem["FIGRA4"];
                //            sitem["GH_FIGRA"] = calc_PB(mrsrfDj_Filter["G_FIGRA"], sitem["FIGRA4"], false);
                //            MItem[0]["GH_THR"] = calc_PB(mrsrfDj_Filter["G_THR"], sitem["THR"], false);
                //            if (!(calc_PB(mrsrfDj_Filter["G_FIGRA"], sitem["FIGRA4"], false) == "不合格" || calc_PB(mrsrfDj_Filter["G_THR"], sitem["THR"], false) == "不合格"))
                //            {
                //                sitem["GH_DTRS"] = "符合" + mrsrfDj_Filter["RSDJ"] + "级";
                //                djjg = mrsrfDj_Filter["RSDJ"];
                //                break;
                //            }
                //            mrsrfDj_Filter = mrsrfDj_Where[xd];
                //        }
                //        if (xd > Gs)
                //        {
                //            sitem["GH_DTRS"] = "不符合B&scsub2&scend级";
                //            mbHggs1 = mbHggs1 + 1;
                //        }
                //        if (GetSafeDouble(sitem["HGS4"].Trim()) <= 1)
                //        {
                //            if (djjg == "E")
                //                sitem["G_YJGD"] = "20s内，≤150 mm";
                //            else
                //                sitem["G_YJGD"] = "60s内，≤150 mm";
                //            sitem["W_YJGD"] = "是";
                //            sitem["GH_YJGD"] = "合格";
                //        }
                //        else if (GetSafeDouble(sitem["HGS5"].Trim()) <= 1)
                //        {
                //            sitem["G_YJGD"] = "20s内，≤150 mm";
                //            sitem["W_YJGD"] = "是";
                //            sitem["GH_YJGD"] = "合格";
                //            djjg = "E";
                //        }
                //        else
                //        {
                //            sitem["G_YJGD"] = "20s内，≤150 mm";
                //            sitem["W_YJGD"] = "否";
                //            sitem["GH_YJGD"] = "不合格";
                //            mbHggs1 = mbHggs1 + 1;
                //        }
                //        if (GetSafeDouble(sitem["HGS1"].Trim()) <= 1)
                //        {
                //            if (djjg == "E")
                //                sitem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //            else
                //                sitem["G_SFYR"] = "60s内无燃烧滴落物引燃滤纸现象";
                //            sitem["W_SFYR"] = "是";
                //            sitem["GH_SFYR"] = "合格";
                //        }
                //        else if (GetSafeDouble(sitem["HGS2"].Trim()) <= 1)
                //        {
                //            sitem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //            sitem["W_SFYR"] = "是";
                //            sitem["GH_SFYR"] = "合格";
                //            djjg = "E";
                //        }
                //        else
                //        {
                //            sitem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //            sitem["W_SFYR"] = "否";
                //            sitem["GH_SFYR"] = "不合格";
                //            mbHggs1 = mbHggs1 + 1;
                //        }



                //        if (sitem["GCBW"].Contains("外墙"))
                //        {
                //            sitem["G_RSYZZ"] = "≥26";
                //            sign = true;
                //            if (sitem["GH_RSYZZ"] == "合格" && (sitem["W_RSYZZ"] == "" || sitem["W_RSYZZ"].Contains("低于")))
                //                sitem["W_RSYZZ"] = "不低于26";
                //            else if (sitem["GH_RSYZZ"] == "不合格" && (sitem["W_RSYZZ"] == "" || sitem["W_RSYZZ"].Contains("低于")))
                //                sitem["W_RSYZZ"] = "低于26";
                //            else
                //            {
                //                sign = IsNumeric(sitem["W_RSYZZ"]) && !string.IsNullOrEmpty(sitem["W_RSYZZ"]) ? sign : false;
                //                if (sign)
                //                    sitem["GH_RSYZZ"] = calc_PB(sitem["G_RSYZZ"], sitem["W_RSYZZ"], false);

                //            }
                //            mbHggs1 = sitem["GH_RSYZZ"] == "不合格" ? mbHggs1 + 1 : mbHggs1;
                //        }
                //        else
                //        {
                //            sitem["GH_RSYZZ"] = "----";
                //            sitem["G_RSYZZ"] = "----";
                //            sitem["W_RSYZZ"] = "----";
                //        }
                //        sitem["RSCDJ"] = djjg;
                //        if (mbHggs1 > 0)
                //        {
                //            sitem["JCJG"] = "不符合B&scsub2&scend级";
                //            MItem[0]["JCJGMS"] = "该试样燃烧性能等级不符合" + MItem[0]["PDBZ"] + "中B&scsub2&scend级要求";
                //        }
                //        else
                //        {
                //            sitem["JCJG"] = "符合B&scsub2&scend级";
                //            MItem[0]["JCJGMS"] = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中B&scsub2&scend级要求";
                //        }
                //    }
                //    if (sitem["RSDJ"].Contains("B2") && sitem["RSDJ"].Contains("E"))
                //    {
                //        if (GetSafeDouble(sitem["HGS5"].Trim()) <= 1)
                //        {
                //            sitem["G_YJGD"] = "20s内，≤150 mm";
                //            sitem["W_YJGD"] = "是";
                //            sitem["GH_YJGD"] = "合格";
                //            djjg = "E";
                //        }
                //        else
                //        {
                //            sitem["G_YJGD"] = "20s内，≤150 mm";
                //            sitem["W_YJGD"] = "否";
                //            sitem["GH_YJGD"] = "不合格";
                //            mbHggs1 = mbHggs1 + 1;
                //        }


                //        if (GetSafeDouble(sitem["HGS2"].Trim()) <= 1)
                //        {
                //            sitem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //            sitem["W_SFYR"] = "是";
                //            sitem["GH_SFYR"] = "合格";
                //            djjg = "E";
                //        }
                //        else
                //        {
                //            sitem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //            sitem["W_SFYR"] = "否";
                //            sitem["GH_SFYR"] = "不合格";
                //            mbHggs1 = mbHggs1 + 1;
                //        }



                //        if (sitem["GCBW"].Contains("外墙"))
                //        {
                //            sitem["G_RSYZZ"] = "≥26";
                //            sign = true;
                //            if (sitem["GH_RSYZZ"] == "合格" && (sitem["W_RSYZZ"] == "" || sitem["W_RSYZZ"].Contains("低于")))
                //                sitem["W_RSYZZ"] = "不低于26";
                //            else if (sitem["GH_RSYZZ"] == "不合格" && (sitem["W_RSYZZ"] == "" || sitem["W_RSYZZ"].Contains("低于")))
                //                sitem["W_RSYZZ"] = "低于26";
                //            else
                //            {
                //                sign = IsNumeric(sitem["W_RSYZZ"]) && !string.IsNullOrEmpty(sitem["W_RSYZZ"]) ? sign : false;
                //                if (sign)
                //                    sitem["GH_RSYZZ"] = calc_PB(sitem["G_RSYZZ"], sitem["W_RSYZZ"], false);
                //            }
                //            mbHggs1 = sitem["GH_RSYZZ"] == "不合格" ? mbHggs1 + 1 : mbHggs1;
                //        }
                //        else
                //        {
                //            sitem["GH_RSYZZ"] = "----";
                //            sitem["G_RSYZZ"] = "----";
                //            sitem["W_RSYZZ"] = "----";
                //        }


                //        if (mbHggs1 > 0)
                //        {
                //            sitem["JCJG"] = "不符合任何级别";
                //            mAllHg = false;
                //            MItem[0]["JCJGMS"] = "该试样不符合" + MItem[0]["PDBZ"] + "中任何级别要求";
                //        }
                //        else
                //        {
                //            sitem["JCJG"] = "符合B&scsub2&scend(E)级";
                //            MItem[0]["JCJGMS"] = "该试样符合" + MItem[0]["PDBZ"] + "中B&scsub2&scend级要求,燃烧性能细化分级为E级";
                //        }
                //    }
                //}
                #endregion

                #region 长度允许偏差
                if (jcxm.Contains("、长度允许偏差、"))
                {
                    jcxmCur = "长度允许偏差";
                    if (IsQualified(MItem[0]["G_CDPC"], sitem["CDPC"], false) == "合格")
                    {
                        MItem[0]["HG_CDPC"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_CDPC"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["CDPC"] = "----";
                    MItem[0]["G_CDPC"] = "----";
                    MItem[0]["HG_CDPC"] = "----";
                }
                #endregion

                #region 宽度允许偏差
                if (jcxm.Contains("、宽度允许偏差、"))
                {
                    jcxmCur = "宽度允许偏差";
                    if (IsQualified(MItem[0]["G_KDPC"], sitem["KDPC"], false) == "合格")
                    {
                        MItem[0]["HG_KDPC"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_KDPC"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["KDPC"] = "----";
                    MItem[0]["G_KDPC"] = "----";
                    MItem[0]["HG_KDPC"] = "----";
                }
                #endregion

                #region 厚度允许偏差
                if (jcxm.Contains("、厚度允许偏差、"))
                {
                    jcxmCur = "厚度允许偏差";
                    if (IsQualified(MItem[0]["G_HDPC"], sitem["HDPC"], false) == "合格")
                    {
                        MItem[0]["HG_HDPC"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_HDPC"] = "不合格";
                        mbHggs = mbHggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["HDPC"] = "----";
                    MItem[0]["G_HDPC"] = "----";
                    MItem[0]["HG_HDPC"] = "----";
                }
                #endregion


                if (mbHggs == 0)
                {
                    MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                    //mjgsm = "该组试样所检项目符合标准要求。"
                }
                if (mbHggs >= 1)
                {
                    MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                    //mjgsm = "该组试样不符合标准要求。"
                    //sitem["JCJG = "不合格"
                    mAllHg = false;
                }
            }
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
