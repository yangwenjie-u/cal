using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class GJJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            double[] mnjqdArray = new double[5];
            string[] mtmpArray;
            string mcalBh, mlongStr;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            int mbHggs;
            bool sign;
            int vp;
            string mlsfhg1, mlsfhg2;
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
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_GJJ_DJ"];
            var MItem = data["M_GJJ"];
            var mitem = MItem[0];
            var SItem = data["S_GJJ"];
            #endregion

            #region  计算开始
            mGetBgbh = false;
            mAllHg = true;
            foreach (var sitem in SItem)
            {
                mbHggs = 0;
                mSFwc = true;
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_item = mrsDj.FirstOrDefault(x => x["YPLB"].Contains(sitem["YPLB"].Trim()) && x["JLX"].Contains(sitem["CPCJB"].Trim()));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    sitem["G_BGSJ"] = mrsDj_item["G_BGSJ"].Trim();
                    sitem["G_LSML1"] = mrsDj_item["G_LSML"].Trim();
                    sitem["G_LSML2"] = mrsDj_item["G_LSML2"].Trim();
                    sitem["G_TXHFL"] = mrsDj_item["G_TXHFL"].Trim();
                    sitem["G_WG"] = mrsDj_item["G_WG"].Trim();
                    sitem["G_XCD"] = mrsDj_item["G_XCD"].Trim();
                    sitem["G_ZLSSL"] = mrsDj_item["G_ZLSSL"].Trim();
                    sitem["G_JCX"] = mrsDj_item["G_JCX"].Trim();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                int xd, Gs;
                string mddj = "";
                double md1, md2, md, sum, pjqd;
                mbHggs = 0;
                sign = true;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、外观、"))
                {
                    if (mitem["GH_WG"] == "合格")
                        mitem["GH_WG"] = "合格";
                    else
                        mitem["GH_WG"] = "不合格";
                }
                else
                {
                    sitem["G_WG"] = "----";
                    sitem["W_WG"] = "----";
                    mitem["GH_WG"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、下垂度、"))
                {
                    sign = IsNumeric(sitem["W_XCDCZ"]) && !string.IsNullOrEmpty(sitem["W_XCDCZ"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_XCDCZ"] = calc_PB(sitem["G_XCD"], sitem["W_XCDCZ"], false);
                        if (mitem["PDBZ"].Contains("14683-2003"))
                        {
                            if (sitem["W_XCDSP"] == "无变形")
                                mitem["GH_XCDSP"] = "合格";
                            else
                                mitem["GH_XCDSP"] = "不合格";
                        }
                    }
                }
                else
                {
                    sitem["W_XCDCZ"] = "----";
                    sitem["W_XCDSP"] = "----";
                    sitem["G_XCD"] = "----";
                    mitem["GH_XCDCZ"] = "----";
                    mitem["GH_XCDSP"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、挤出性、"))
                    mitem["GH_JCX"] = calc_PB(sitem["G_JCX"], sitem["W_JCX"], false);
                else
                {
                    sitem["G_JCX"] = "----";
                    sitem["W_JCX"] = "----";
                    mitem["GH_JCX"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、适用期、"))
                {
                    sitem["G_SYQ"] = sitem["SYQSDZ"];
                    mitem["GH_SYQ"] = calc_PB(sitem["G_SYQ"], sitem["W_SYQ"], false);
                }
                else
                {
                    sitem["G_SYQ"] = "----";
                    sitem["W_SYQ"] = "----";
                    mitem["GH_SYQ"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、密度、"))
                {
                    sign = IsNumeric(sitem["W_MD"]) && !string.IsNullOrEmpty(sitem["W_MD"]) ? sign : false;
                    if (sign)
                    {
                        if (sitem["BCMD"] != "----")
                        {
                            mddj = (Conversion.Val(sitem["BCMD"]) - 0.1) + "～" + (Conversion.Val(sitem["BCMD"]) + 0.1);
                            mitem["GH_MD"] = calc_PB(mddj, sitem["W_MD"], false);
                            sitem["G_MD"] = sitem["BCMD"] + "±0.1";
                        }
                        else
                        {
                            sitem["G_MD"] = "----";
                            mitem["GH_MD"] = "----";
                        }
                    }
                }
                else
                {
                    sitem["G_MD"] = "----";
                    sitem["W_MD"] = "----";
                    mitem["GH_MD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、表干时间、"))
                {
                    sign = IsNumeric(sitem["W_BGSJ1"]) && !string.IsNullOrEmpty(sitem["W_BGSJ1"]) ? sign : false;
                    sign = IsNumeric(sitem["W_BGSJ2"]) && !string.IsNullOrEmpty(sitem["W_BGSJ2"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_BGSJ1"] = calc_PB(sitem["G_BGSJ"], sitem["W_BGSJ1"], false);
                        mitem["GH_BGSJ2"] = calc_PB(sitem["G_BGSJ"], sitem["W_BGSJ2"], false);
                    }
                }
                else
                {
                    sitem["G_BGSJ"] = "----";
                    sitem["W_BGSJ1"] = "----";
                    sitem["W_BGSJ2"] = "----";
                    mitem["GH_BGSJ1"] = "----";
                    mitem["GH_BGSJ2"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、弹性恢复率、"))
                {
                    sign = IsNumeric(sitem["W_TXHHL"]) && !string.IsNullOrEmpty(sitem["W_TXHHL"]) ? sign : false;
                    if (sign)
                        mitem["GH_TXHHL"] = calc_PB(sitem["G_TXHFL"], sitem["W_TXHHL"], false);
                }
                else
                {
                    sitem["G_TXHFL"] = "----";
                    sitem["W_TXHHL"] = "----";
                    mitem["GH_TXHHL"] = "----";
                }


                sign = true;
                if (jcxm.Contains("、拉伸模量、"))
                {
                    sign = IsNumeric(sitem["W_LSML1"]) && !string.IsNullOrEmpty(sitem["W_LSML1"]) ? sign : false;
                    sign = IsNumeric(sitem["W_LSML2"]) && !string.IsNullOrEmpty(sitem["W_LSML2"]) ? sign : false;
                    if (sign)
                    {
                        mlsfhg1 = calc_PB(sitem["G_LSML1"], sitem["W_LSML1"], false);
                        mlsfhg2 = calc_PB(sitem["G_LSML2"], sitem["W_LSML2"], false);
                        if (sitem["CPCJB"].Contains("高模量"))
                        {
                            if (mlsfhg1 == "不合格" && mlsfhg2 == "不合格")
                                mitem["GH_LSML"] = "不合格";
                            else
                                mitem["GH_LSML"] = "合格";
                            sitem["G_LSML1"] = sitem["G_LSML1"] + "或";
                        }
                        else
                        {
                            if (mlsfhg1 == "不合格" || mlsfhg2 == "不合格")
                                mitem["GH_LSML"] = "不合格";
                            else
                                mitem["GH_LSML"] = "合格";
                        }
                    }
                }
                else
                {
                    sitem["G_LSML1"] = "----";
                    sitem["G_LSML2"] = "----";
                    sitem["W_LSML1"] = "----";
                    sitem["W_LSML2"] = "----";
                    mitem["GH_LSML"] = "----";
                }


                sign = true;
                if (jcxm.Contains("、质量损失率、"))
                {
                    sign = IsNumeric(sitem["W_ZLSSL"]) && !string.IsNullOrEmpty(sitem["W_ZLSSL"]) ? sign : false;
                    if (sign)
                        mitem["GH_ZLSSL"] = calc_PB(sitem["G_ZLSSL"], sitem["W_ZLSSL"], false);
                }
                else
                {
                    sitem["G_ZLSSL"] = "----";
                    sitem["W_ZLSSL"] = "----";
                    mitem["GH_ZLSSL"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、定伸粘结性、"))
                {
                    sign = !string.IsNullOrEmpty(mitem["GH_DSNJQD"]) ? sign : false;
                    if (sign)
                    {
                        if (mitem["GH_DSNJQD"] == "合格")
                            sitem["W_DSNJQD"] = "无破坏";
                        else
                            sitem["W_DSNJQD"] = "破坏";
                    }
                    else
                    {
                        sitem["W_DSNJQD"] = "----";
                        mitem["GH_DSNJQD"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、紫外线辐照后粘结性、"))
                    {
                        sign = !string.IsNullOrEmpty(sitem["GH_ZWXFZHNJX"]) ? sign : false;
                        if (sign)
                        {
                            if (mitem["GH_ZWXFZHNJX"].Trim() == "合格")
                                sitem["W_ZWXFZHNJX"] = "无破坏";
                            else
                                sitem["W_ZWXFZHNJX"] = "破坏";
                        }
                    }
                    else
                    {
                        sitem["W_ZWXFZHNJX"] = "----";
                        mitem["GH_ZWXFZHNJX"] = "----";
                    }
                    sign = true;
                    if (jcxm.Contains("、浸水后定伸粘结性、"))
                    {
                        sign = !string.IsNullOrEmpty(sitem["GH_JSHNJX"]) ? sign : false;
                        if (sign)
                        {
                            if (mitem["GH_JSHNJX"].Trim() == "合格")
                                sitem["W_JSHNJX"] = "无破坏";
                            else
                                sitem["W_JSHNJX"] = "破坏";
                        }
                    }
                    else
                    {
                        sitem["W_JSHNJX"] = "----";
                        mitem["GH_JSHNJX"] = "----";
                    }
                    if (jcxm.Contains("、浸水光照后粘结性、"))
                    {
                        sign = !string.IsNullOrEmpty(sitem["GH_JSGZHNJX"]) ? sign : false;
                        if (sign)
                        {
                            if (mitem["GH_JSGZHNJX"].Trim() == "合格")
                                sitem["W_JSGZHNJX"] = "无破坏";
                            else
                                sitem["W_JSGZHNJX"] = "破坏";
                        }
                    }
                    else
                    {
                        sitem["W_JSGZHNJX"] = "----";
                        mitem["GH_JSGZHNJX"] = "----";
                    }
                    mbHggs = mitem["GH_BGSJ1"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_BGSJ2"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_DSNJQD"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_JCX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_SYQ"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_JSHNJX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_LSML"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_MD"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_TXHHL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_WG"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_XCDSP"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_XCDSP"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_ZLSSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_ZWXFZHNJX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    mbHggs = mitem["GH_JSGZHNJX"] == "不合格" ? mbHggs + 1 : mbHggs;
                    sitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                    mitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                    mitem["JCJGMS"] = mbHggs == 0 ? "该组样品所检项目符合" + mitem["PDBZ"] + "标准要求。" : "该组所检项目样品不符合" + mitem["PDBZ"] + "标准要求。";
                    if (mbHggs > 0 && (mitem["GH_BGSJ1"] == "合格" || mitem["GH_BGSJ2"] == "合格" || mitem["GH_DSNJQD"] == "合格" || mitem["GH_JCX"] == "合格" || mitem["GH_JSHNJX"] == "合格" || mitem["GH_LSML"] == "合格" || mitem["GH_MD"] == "合格" || mitem["GH_TXHHL"] == "合格" || mitem["GH_WG"] == "合格" || mitem["GH_XCDSP"] == "合格" || mitem["GH_XCDSP"] == "合格" || mitem["GH_ZLSSL"] == "合格" || mitem["GH_ZWXFZHNJX"] == "合格"))
                        mitem["JCJGMS"] = "该组样品所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                }
            }
            #endregion


            /************************ 代码结束 *********************/
        }
    }
}
