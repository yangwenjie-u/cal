using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JPH : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            int mbhggs;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            bool mSFwc;
            #endregion

            #region 自定义函数
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
                        bool min_bl, max_bl;
                        bool sign_fun;
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
            var mrsDj = dataExtra["BZ_JPH_DJ"];
            var mrsWd = dataExtra["BZ_DXLWD"];
            var MItem = data["M_JPH"];
            var mitem = MItem[0];
            var SItem = data["S_JPH"];
            var E_JLPB_Temp = data["E_JLPB"];
            #endregion

            #region  计算开始
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            foreach (var sitem in SItem)
            {
                mbhggs = 0; //记录不合格数
                double md, md1, md2, sum;
                int xd, Gs;
                string stemp;
                bool flag;
                bool sign;
                string dzbh;
                stemp = "";
                for (xd = 1; xd <= 4; xd++)
                {
                    if (!string.IsNullOrEmpty(sitem["KLMC" + xd]))
                    {
                        stemp = stemp + sitem["KLMC" + xd] + ":" + sitem["KLGG" + xd] + " " + sitem["KLCD" + xd] + ";";
                    }
                }
                mitem["GGCD"] = stemp;
                var E_JLPB_Filter = E_JLPB_Temp;
                //var E_JLPB_Filter = E_JLPB_Temp.Where(x => x["SYSJBRECID"].Equals(sitem["RECID"]));
                foreach (var E_JLPB in E_JLPB_Filter)
                {
                    stemp = E_JLPB["ZLPHB"];
                }
                //经检测，推荐级配组成为寸子:瓜子片:石屑(质量比)=44.2:27.3:28.5，检测结果详见报告第2、3页。
                mitem["JCJGMS"] = "经检测，推荐级配组成为";
                for (xd = 1; xd <= 4; xd++)
                {
                    sitem["KLMC" + xd] = sitem["KLMC" + xd].Trim();
                    if (!string.IsNullOrEmpty(sitem["KLMC" + xd]))
                        mitem["JCJGMS"] = mitem["JCJGMS"] + sitem["KLMC" + xd] + ":";
                }
                mitem["JCJGMS"] = mitem["JCJGMS"].Substring(0, mitem["JCJGMS"].Length - 1);
                mitem["JCJGMS"] = mitem["JCJGMS"] + "(质量比%)=" + stemp;
                mitem["JCJGMS"] = mitem["JCJGMS"] + "，检测结果详见报告第";
                sign = false;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、针片状含量、"))
                {
                    sitem["P_ZPZ"] = calc_PB(sitem["S_ZPZ"], sitem["ZPZ"], true);
                    if (sitem["P_ZPZ"] == "不符合")
                        mAllHg = false;
                    sign = true;
                }
                else
                {
                    sitem["P_ZPZ"] = "----";
                    sitem["ZPZ"] = "----";
                    sitem["S_ZPZ"] = "----";
                }
                if (jcxm.Contains("、压碎值、"))
                {
                    sitem["P_YSZ"] = calc_PB(sitem["S_YSZ"], sitem["YSZ"], true);
                    if (sitem["P_YSZ"] == "不符合")
                        mAllHg = false;
                    sign = true;
                }
                else
                {
                    sitem["P_YSZ"] = "----";
                    sitem["YSZ"] = "----";
                    sitem["S_YSZ"] = "----";
                }
                if (jcxm.Contains("、最大干密度、"))
                {
                    sitem["P_GMD"] = calc_PB(sitem["S_GMD"], sitem["GMD"], true);
                    if (sitem["P_GMD"] == "不符合")
                        mAllHg = false;
                    sign = true;
                }
                else
                {
                    sitem["P_GMD"] = "----";
                    sitem["GMD"] = "----";
                    sitem["S_GMD"] = "----";
                }

                if (sign)
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "2、3页。";
                else
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "2页。";
                if (mAllHg)
                {
                    sitem["JCJG"] = "合格";
                    mitem["JCJG"] = "合格";
                }
                else
                {
                    sitem["JCJG"] = "不合格";
                    mitem["JCJG"] = "不合格";
                }
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
