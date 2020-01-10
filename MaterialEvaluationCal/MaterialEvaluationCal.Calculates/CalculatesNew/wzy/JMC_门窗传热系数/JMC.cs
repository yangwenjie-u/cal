using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JMC : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;
            int mbhggs;
            string mMaxBgbh;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            bool mSFwc;
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            #endregion;

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
            var mrsDj = dataExtra["BZ_JMC_DJ"];
            var mrsBwfj = dataExtra["BZ_JMCBWFJ"];
            var MItem = data["M_JMC"];
            var SItem = data["S_JMC"];
            var tempTable = data["MS_BW"];
            #endregion

            #region 计算开始
            MItem[0]["JCJGMS"] = "";
            foreach (var sitem in SItem)
            {
                double md1, md2, sum, pjmd, md;
                int xd, Gs;
                string bl;
                bool flag = true;
                double[] nArr;
                if (sitem["SJGG"].Contains("×"))
                {
                    sitem["MCCD"] = sitem["SJGG"].Substring(0, sitem["SJGG"].IndexOf("×"));
                    sitem["MCCD"] = sitem["MCCD"] + "mm";
                    sitem["MCKD"] = sitem["SJGG"].Substring(sitem["SJGG"].IndexOf("×") + 1);
                    sitem["MCKD"] = sitem["MCKD"] + "mm";
                }
                var MS_BW_Filter = tempTable;
                //var M_BW_Filter = tempTable.Where(x => x["SYSJBRECID"].Equals(sitem["RECID"]));
                Gs = MS_BW_Filter.Count();
                if (Gs > 0)
                {
                    var MS_BW = MS_BW_Filter[0];
                    flag = true;
                    for (xd = 1; xd <= 6; xd++)
                    {
                        if (!IsNumeric(MS_BW["相对湿度" + xd]) || string.IsNullOrEmpty(MS_BW["相对湿度" + xd]))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 6; xd++)
                            sum = sum + Conversion.Val(MS_BW["相对湿度" + xd].Trim());
                        pjmd = sum / 6;
                        pjmd = Round(pjmd, 1);
                        sitem["XDSD"] = pjmd.ToString("F1");
                    }
                }
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、传热系数、"))
                {
                    flag = true;
                    Gs = mrsDj.Count();
                    var mrsDj_Filter = mrsDj[0];
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (calc_PB(mrsDj_Filter["DJFW"], sitem["CRXS"], true) == "符合")
                        {
                            MItem[0]["SSFJ"] = mrsDj_Filter["DJBH"] + "级";
                            MItem[0]["SSFW"] = mrsDj_Filter["DJFW"];
                            break;
                        }
                        mrsDj_Filter = mrsDj[xd];
                    }
                    if (xd > Gs)
                    {
                        MItem[0]["SSFJ"] = "不符合任一级别";
                        MItem[0]["SSFW"] = "----";
                        flag = false;
                    }


                    MItem[0]["JCMGMS"] = "该试样传热系数";
                    if (IsNumeric(sitem["CRXSSJZ"]))
                    {
                        md1 = Conversion.Val(sitem["CRXS"].Trim()); // 试件传热系数
                        md2 = Conversion.Val(sitem["CRXSSJZ"].Trim());   //传热系数设计值K≤
                        if (md1 <= md2)
                            sitem["GH_CRXS"] = "符合";
                        else
                        {
                            sitem["GH_CRXS"] = "不符合";
                            mAllHg = false;
                        }
                        MItem[0]["JCMGMS"] = MItem[0]["JCMGMS"] + sitem["GH_CRXS"] + "设计要求。";
                    }
                    else
                    {
                        if (flag)
                            MItem[0]["JCMGMS"] = MItem[0]["JCMGMS"] + "属于" + MItem[0]["SSFJ"] + "。";
                        else
                            MItem[0]["JCMGMS"] = MItem[0]["JCMGMS"] + "不符合任一级别。";
                        sitem["GH_CRXS"] = "----";
                    }
                    if (jcxm.Contains("、抗结露因子、"))
                    {
                        flag = true;
                        Gs = mrsDj.Count();
                        mrsDj_Filter = mrsDj[0];
                        for (xd = 1; xd <= Gs; xd++)
                        {
                            if (calc_PB(mrsDj_Filter["KJLDJFW"], sitem["JLYZ"], true) == "符合")
                            {
                                MItem[0]["SSFJ1"] = mrsDj_Filter["DJBH"] + "级";
                                break;
                            }
                            mrsDj_Filter = mrsDj[xd];
                        }
                        if (xd > Gs)
                        {
                            MItem[0]["SSFJ1"] = "不符合任一级别";
                            flag = false;
                        }

                    }
                    if (flag)
                        MItem[0]["JCMGMS"] = MItem[0]["JCMGMS"] + "抗结露因子属于" + MItem[0]["SSFJ1"] + "。";
                    else
                        MItem[0]["JCMGMS"] = MItem[0]["JCMGMS"] + "抗结露因子不符合任一级别。";
                    sitem["GH_CRXS"] = "----";
                }
                else
                {
                    sitem["JLYZ"] = "----";
                    sitem["GH_JLYZ"] = "----";
                    sitem["JLJS2"] = "----";
                }
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
