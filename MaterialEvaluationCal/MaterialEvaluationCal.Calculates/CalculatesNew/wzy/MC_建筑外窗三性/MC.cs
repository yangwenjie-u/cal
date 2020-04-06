using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class MC : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;


            string mhgjg, mbhgjg;
            string djjg;
            int mbHggs = 0;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            bool mSFwc;
            mSFwc = true;
            #endregion

            #region 自定义函数
            //这个函数总括了对于判定(符合,不符合) 还是判断(合格,不合格)
            Func<string, string, bool, string> calc_PB =
                delegate (string sj_fun, string sc_fun, bool flag_fun)
                {
                    string calc_PB_fun = string.Empty;
                    if (string.IsNullOrEmpty(sj_fun) || string.IsNullOrEmpty(sc_fun))
                    {
                        if (flag_fun)
                        {
                            return "不符合";
                        }
                        else
                        {
                            return "不合格";
                        }
                    }

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
            var mrsDj = dataExtra["BZ_MC_DJ"];
            //var mrsWd = dataExtra["BZ_DXLWD"];
            var MItem = data["M_MC"];
            var SItem = data["S_MC"];
            var mrsMs = data["MS_MC"];
            #endregion

            #region  计算开始
            mGetBgbh = false;
            mAllHg = true;
            MItem[0]["JCJGMS"] = "";
            double md1, md2, pjmd, md, sum;
            double zqf1, zqf2, zqz1, zqz2, fqf1, fqf2, fqz1, fqz2, zqt, fqt, q1, q2, fq1, fq2, kqfc, kqmj;
            int xd, Gs;
            string bl;
            bool flag = false, sign;
            double[] nArr;
            bool mark;  //标示逢长为----
            mark = true;
            //抗风压性能、气密性能、水密性能
            mhgjg = "";
            mbhgjg = "";
            djjg = "";
            foreach (var sitem in SItem)
            {
                if (IsNumeric(sitem["BLHD"]))
                    sitem["BLHD"] = sitem["BLHD"] + "mm";
                //if (!sitem["BLGZ"].Contains("mm"))
                //    sitem["BLGZ"] = "(" + sitem["BLGZ"] + ")mm";


                if (sitem["GGCC"].Contains("×"))
                {
                    sitem["MCKD"] = sitem["GGCC"].Substring(0, sitem["GGCC"].IndexOf("×"));
                    sitem["MCKD"] = sitem["MCKD"] + "mm";
                    sitem["MCCD"] = sitem["GGCC"].Substring(sitem["GGCC"].IndexOf("×") + 1);
                    sitem["MCCD"] = sitem["MCCD"] + "mm";
                }
                //sitem["BLZD"] = sitem["BLZDC"].Trim() + "×" + sitem["BLZDK"].Trim();
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、气密性能、"))
                {
                    #region old
                    //var MS_MC_Filter = mrsMs;
                    ////var MS_MC_Filter = mrsMs.Where(x => x["SYSJBRECID"].Equals(sitem["RECID"]));
                    //var MS_MC = MS_MC_Filter[0];
                    //for (xd = 1; xd <= 3; xd++)
                    //{
                    //    if (MS_MC.Count == 0)
                    //    {
                    //        continue;
                    //    }
                    //    kqfc = Conversion.Val(MS_MC["开启缝长"].Trim());
                    //    kqmj = Conversion.Val(MS_MC["试件面积"].Trim());
                    //    zqf1 = Conversion.Val(MS_MC["升压流量100F"].Trim());
                    //    zqf2 = Conversion.Val(MS_MC["降压流量100F"].Trim());
                    //    zqz1 = Conversion.Val(MS_MC["升压流量100Z"].Trim());
                    //    zqz2 = Conversion.Val(MS_MC["降压流量100Z"].Trim());
                    //    fqf1 = Conversion.Val(MS_MC["负升压流量100F"].Trim());
                    //    fqf2 = Conversion.Val(MS_MC["负降压流量100F"].Trim());
                    //    fqz1 = Conversion.Val(MS_MC["负升压流量100Z"].Trim());
                    //    fqz2 = Conversion.Val(MS_MC["负降压流量100Z"].Trim());
                    //    zqf1 = (zqf1 + zqf2) / 2;
                    //    zqz1 = (zqz1 + zqz2) / 2;
                    //    fqf1 = (fqf1 + fqf2) / 2;
                    //    fqz1 = (fqz1 + fqz2) / 2;
                    //    zqt = zqz1 - zqf1;
                    //    fqt = fqz1 - fqf1;
                    //    q1 = zqt / kqfc;
                    //    q1 = Round(q1, 2);
                    //    q1 = q1 / 4.65;
                    //    q1 = Round(q1, 2);
                    //    q2 = zqt / kqmj;
                    //    q2 = Round(q2, 2);
                    //    q2 = q2 / 4.65;
                    //    q2 = Round(q2, 2);
                    //    fq1 = fqt / kqfc;
                    //    fq1 = Round(fq1, 2);
                    //    fq1 = fq1 / 4.65;
                    //    fq1 = Round(fq1, 2);
                    //    fq2 = fqt / kqmj;
                    //    fq2 = Round(fq2, 2);
                    //    fq2 = fq2 / 4.65;
                    //    fq2 = Round(fq2, 2);
                    //    sitem["FCQMZY" + xd] = q1.ToString("F2");
                    //    sitem["MJQMZY" + xd] = q2.ToString("F2");
                    //    sitem["FCQMFY" + xd] = fq1.ToString("F2");
                    //    sitem["MJQMFY" + xd] = fq2.ToString("F2");
                    //    if (xd >= MS_MC_Filter.Count())
                    //        MS_MC = MS_MC_Filter[xd - 1];
                    //    else
                    //        MS_MC = MS_MC_Filter[xd];
                    //}
                    #endregion
                    nArr = new double[4];
                    sum = 0;
                    for (xd = 1; xd <= 3; xd++)
                    {
                        if (IsNumeric(sitem["FCQMZY" + xd]))
                            md = Conversion.Val(sitem["FCQMZY" + xd].Trim());
                        else
                        {
                            md = 0;
                            mark = false;
                        }
                        nArr[xd] = md;
                        sum = sum + md;
                    }
                    pjmd = sum / 3;
                    pjmd = Round(pjmd, 1);
                    sitem["AVG_FZ"] = pjmd.ToString("F1");
                    sum = 0;
                    for (xd = 1; xd <= 3; xd++)
                    {
                        if (IsNumeric(sitem["FCQMFY" + xd]))
                            md = Conversion.Val(sitem["FCQMFY" + xd].Trim());
                        else
                        {
                            md = 0;
                            mark = false;
                        }
                        nArr[xd] = md;
                        sum = sum + md;
                    }
                    pjmd = sum / 3;
                    pjmd = Round(pjmd, 1);
                    sitem["AVG_FF"] = pjmd.ToString("F1");
                    if (sitem["AVG_FZ"] == "0.0" && sitem["AVG_FF"] == "0.0" && Conversion.Val(sitem["FCQMZY1"]) == 0 && Conversion.Val(sitem["FCQMZY2"]) == 0 && Conversion.Val(sitem["FCQMZY3"]) == 0 && Conversion.Val(sitem["FCQMFY1"]) == 0 && Conversion.Val(sitem["FCQMFY2"]) == 0 && Conversion.Val(sitem["FCQMFY3"]) == 0)
                    {
                        sitem["AVG_FZ"] = "----";
                        sitem["FCQMZY1"] = "----";
                        sitem["FCQMZY2"] = "----";
                        sitem["FCQMZY3"] = "----";
                        sitem["AVG_FF"] = "----";
                        sitem["FCQMFY1"] = "----";
                        sitem["FCQMFY2"] = "----";
                        sitem["FCQMFY3"] = "----";
                    }
                    sum = 0;
                    for (xd = 1; xd <= 3; xd++)
                    {
                        md = Conversion.Val(sitem["MJQMZY" + xd].Trim());
                        nArr[xd] = md;
                        sum = sum + md;
                    }
                    pjmd = sum / 3;
                    pjmd = Round(pjmd, 1);
                    sitem["AVG_MZ"] = pjmd.ToString("F1");
                    sum = 0;
                    for (xd = 1; xd <= 3; xd++)
                    {
                        md = Conversion.Val(sitem["MJQMFY" + xd].Trim());
                        nArr[xd] = md;
                        sum = sum + md;
                    }
                    pjmd = sum / 3;
                    pjmd = Round(pjmd, 1);
                    sitem["AVG_MF"] = pjmd.ToString("F1");
                    flag = sitem["JCLB"] == "工程检测" ? true : false;
                    sitem["QMXQ1YQ"] = sitem["QMXQ1YQ"].Trim();
                    sitem["QMXQ1YQ"] = IsNumeric(sitem["QMXQ1YQ"]) ? "≥" + sitem["QMXQ1YQ"] : sitem["QMXQ1YQ"];
                    sitem["QMXQ2YQ"] = sitem["QMXQ2YQ"].Trim();
                    sitem["QMXQ2YQ"] = IsNumeric(sitem["QMXQ2YQ"]) ? "≥" + sitem["QMXQ2YQ"] : sitem["QMXQ2YQ"];
                    if (calc_PB(sitem["QMXQ1YQ"], sitem["AVG_FZ"], true) == "符合" && calc_PB(sitem["QMXQ1YQ"], sitem["AVG_FF"], true) == "符合" && calc_PB(sitem["QMXQ2YQ"], sitem["AVG_MZ"], true) == "符合" && calc_PB(sitem["QMXQ2YQ"], sitem["AVG_MF"], true) == "符合")
                        sitem["PD_QM"] = "符合";
                    else if (calc_PB(sitem["QMXQ1YQ"], sitem["AVG_FZ"], true) == "不符合" || calc_PB(sitem["QMXQ1YQ"], sitem["AVG_FF"], true) == "不符合" || calc_PB(sitem["QMXQ2YQ"], sitem["AVG_MZ"], true) == "不符合" || calc_PB(sitem["QMXQ2YQ"], sitem["AVG_MF"], true) == "不符合")
                    {
                        sitem["PD_QM"] = "不符合";
                        mbHggs = mbHggs + 1;
                    }
                    else
                        sitem["PD_QM"] = "----";
                    if (sitem["PD_QM"] == "不符合")
                        mbhgjg = mbhgjg + "、气密性能";
                    if (sitem["PD_QM"] == "符合")
                        mhgjg = mhgjg + "、气密性能";
                    if (sitem["PD_QM"] == "----")
                        djjg = djjg + "、气密性能";
                    var mrsDj_Filter = mrsDj[0];
                    Gs = mrsDj.Count();
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (calc_PB(mrsDj_Filter["S_MJYQ"], sitem["AVG_MZ"], true) == "不符合" || calc_PB(mrsDj_Filter["S_FCYQ"], sitem["AVG_FZ"], true) == "不符合")
                        {
                            bl = (xd - 1).ToString();
                            sitem["DJ_QZ"] = bl + "级";
                            if (xd == 1)
                                sitem["DJ_QZ"] = "不符合" + "任何级别";
                            break;
                        }
                        if (xd >= mrsDj.Count())
                            mrsDj_Filter = mrsDj[xd - 1];
                        else
                            mrsDj_Filter = mrsDj[xd];
                    }
                    mrsDj_Filter = mrsDj[0];
                    Gs = mrsDj.Count();
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (calc_PB(mrsDj_Filter["S_MJYQ"], sitem["AVG_MF"], true) == "不符合" || calc_PB(mrsDj_Filter["S_FCYQ"], sitem["AVG_FF"], true) == "不符合")
                        {
                            bl = (xd - 1).ToString();
                            sitem["DJ_QF"] = bl + "级";
                            if (xd == 1)
                                sitem["DJ_QF"] = "不符合" + "任何级别";
                            break;
                        }
                        if (xd >= mrsDj.Count())
                            mrsDj_Filter = mrsDj[xd - 1];
                        else
                            mrsDj_Filter = mrsDj[xd];
                    }
                    if (!mark)
                    {
                        sitem["AVG_FZ"] = "----";
                        sitem["AVG_FF"] = "----";
                    }
                }
                else
                {
                    sitem["QMXQ1YQ"] = "----";
                    sitem["QMXQ2YQ"] = "----"; ;
                    sitem["AVG_FZ"] = "----";
                    sitem["AVG_FF"] = "----";
                    sitem["AVG_MZ"] = "----";
                    sitem["AVG_MF"] = "----";
                    sitem["PD_QM"] = "----";
                    sitem["DJ_QZ"] = "----";
                    sitem["DJ_QF"] = "----";
                }
                if (jcxm.Contains("、水密性能、"))
                {
                    sum = 0;
                    nArr = new double[4];
                    for (xd = 1; xd <= 3; xd++)
                    {
                        md = Conversion.Val(sitem["SMXYL" + xd].Trim());
                        nArr[xd] = md;
                        sum = sum + md;
                    }
                    pjmd = sum / 3;
                    pjmd = Round(pjmd, 0);
                    sitem["AVG_SM"] = pjmd.ToString();
                    #region old
                    //flag = sitem["JCLB"] == "工程检测" ? true : false;
                    //if (flag)
                    //{
                    //    sitem["SMXSJYQ"] = sitem["SMXSJYQ"].Trim();
                    //    sitem["AVG_SM"] = sitem["SMXSJYQ"].Replace("≥", "");
                    //    var MS_MC_Filter = mrsMs;
                    //    //var MS_MC_Filter = mrsMs.Where(x => x["SYSJBRECID"].Equals(sitem["RECID"]));
                    //    var MS_MC = MS_MC_Filter[0];
                    //    for (xd = 1; xd <= 3; xd++)
                    //    {
                    //        if (MS_MC.Count == 0)
                    //        {
                    //            continue;
                    //        }
                    //        if (MS_MC["稳定渗漏12"] != "○" && MS_MC["稳定渗漏12"] != "" && MS_MC["稳定渗漏12"] != "△" && MS_MC["稳定渗漏12"] != "□")
                    //            sitem["PD_SM"] = "不符合";
                    //        else
                    //            sitem["PD_SM"] = "符合";
                    //        if (xd >= MS_MC_Filter.Count())
                    //            MS_MC = MS_MC_Filter[xd - 1];
                    //        else
                    //            MS_MC = MS_MC_Filter[xd];
                    //    }
                    //    //sitem["DJ_SM"] = "----";
                    //    if (sitem["PD_SM"] == "不符合")
                    //        sitem["AVG_SM"] = sitem["AVG_SM"] + " Pa 渗漏 ";
                    //    else
                    //        sitem["AVG_SM"] = sitem["AVG_SM"] + " Pa 未渗漏 ";
                    //    if (sitem["PD_SM"] == "不符合")
                    //        mbhgjg = mbhgjg + "、水密性能";
                    //    if (sitem["PD_SM"] == "符合")
                    //        mhgjg = mhgjg + "、水密性能";
                    //    if (sitem["PD_SM"] == "----")
                    //        djjg = djjg + "、水密性能";
                    //}
                    //else
                    //{
                    //    sitem["SMXSJYQ"] = sitem["SMXSJYQ"].Trim();
                    //    sitem["SMXSJYQ"] = IsNumeric(sitem["SMXSJYQ"]) ? sitem["SMXSJYQ"] + "≤" : sitem["SMXSJYQ"];
                    //    sitem["PD_SM"] = calc_PB(sitem["SMXSJYQ"], sitem["AVG_SM"], true);
                    //    mbHggs = sitem["PD_SM"] == "不符合" ? mbHggs + 1 : mbHggs;
                    //    if (sitem["PD_SM"] == "不符合")
                    //        mbhgjg = mbhgjg + "、水密性能";
                    //    if (sitem["PD_SM"] == "符合")
                    //        mhgjg = mhgjg + "、水密性能";
                    //    if (sitem["PD_SM"] == "----")
                    //        djjg = djjg + "、水密性能";
                    //    //var mrsDj_Filter = mrsDj[0];
                    //    //Gs = mrsDj.Count();
                    //    //for (xd = 1; xd <= Gs; xd++)
                    //    //{
                    //    //    if (calc_PB(mrsDj_Filter["SMYQ"], sitem["AVG_SM"], true) == "符合")
                    //    //    {
                    //    //        sitem["DJ_SM"] = mrsDj_Filter["SMJB"] + "级";
                    //    //        break;
                    //    //    }
                    //    //    if (xd >= mrsDj.Count())
                    //    //        mrsDj_Filter = mrsDj[xd - 1];
                    //    //    else
                    //    //        mrsDj_Filter = mrsDj[xd];
                    //    //}
                    //    //if (xd > Gs)
                    //    //    sitem["DJ_SM"] = "不符合任一级别";
                    //}
                    #endregion
                    sitem["PD_SM"] = "----";
                    var mrsDj_Filter = mrsDj[0];
                    Gs = mrsDj.Count();
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (calc_PB(mrsDj_Filter["SMYQ"], sitem["AVG_SM"], true) == "符合")
                        {
                            sitem["DJ_SM"] = mrsDj_Filter["SMJB"] + "级";
                            if (GetSafeDouble(mrsDj_Filter["SMJB"]) >= GetSafeDouble(sitem["SMXSJJB"]) )
                            {
                                sitem["PD_SM"] = "符合";
                            }
                            break;
                        }
                        if (xd >= mrsDj.Count())
                            mrsDj_Filter = mrsDj[xd - 1];
                        else
                            mrsDj_Filter = mrsDj[xd];
                    }
                    if (xd > Gs)
                    {
                        sitem["DJ_SM"] = "不符合任一级别";
                        sitem["PD_SM"] = "不符合";
                        mbHggs = sitem["PD_SM"] == "不符合" ? mbHggs + 1 : mbHggs;
                    }
                    if (sitem["PD_SM"] == "不符合")
                        sitem["AVG_SM"] = sitem["AVG_SM"] + " Pa 渗漏 ";
                    else
                        sitem["AVG_SM"] = sitem["AVG_SM"] + " Pa 未渗漏 ";
                    if (sitem["PD_SM"] == "不符合")
                        mbhgjg = mbhgjg + "、水密性能";
                    if (sitem["PD_SM"] == "符合")
                        mhgjg = mhgjg + "、水密性能";
                    if (sitem["PD_SM"] == "----")
                        djjg = djjg + "、水密性能";
                }
                else
                {
                    sitem["SMXSJYQ"] = "----";
                    sitem["PD_SM"] = "----";
                    sitem["DJ_SM"] = "----";
                    sitem["SMXSJJB"] = "----";
                }
                if (jcxm.Contains("、抗风压性能、"))
                {
                    nArr = new double[4];
                    //只要对p3处理即可了
                    if (sitem["SFDS"] == "是" && (sitem["DSLB"] == "外开单扇无受力杆件" || sitem["DSLB"] == "外开单扇有受力杆件"))
                    {
                        for (xd = 1; xd <= 3; xd++)
                            sitem["MIN_ZP" + xd] = "----";
                    }
                    else
                    {
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md = Conversion.Val(sitem["KFYP1Z" + xd].Trim());
                            nArr[xd] = Math.Abs(md);
                        }
                        Array.Sort(nArr);
                        md = nArr[1] / 1000;
                        md = Round(md, 1);
                        sitem["MIN_ZP1"] = md.ToString("F1");
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md = Conversion.Val(sitem["KFYP2Z" + xd].Trim());
                            nArr[xd] = Math.Abs(md);
                        }
                        Array.Sort(nArr);
                        md = nArr[1] / 1000;
                        md = Round(md, 1);
                        sitem["MIN_ZP2"] = md.ToString("F1");
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md = Conversion.Val(sitem["KFYP3Z" + xd].Trim());
                            nArr[xd] = Math.Abs(md);
                        }
                        Array.Sort(nArr);
                        md = nArr[1] / 1000;
                        md = Round(md, 1);
                        sitem["MIN_ZP3"] = md.ToString("F1");
                    }
                    if (sitem["SFDS"] == "是" && (sitem["DSLB"] == "内开单扇无受力杆件" || sitem["DSLB"] == "内开单扇有受力杆件"))
                    {
                        for (xd = 1; xd <= 3; xd++)
                            sitem["MIN_FP" + xd] = "----";
                    }
                    else
                    {
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md = Conversion.Val(sitem["KFYP1F" + xd].Trim());
                            nArr[xd] = Math.Abs(md);
                        }
                        Array.Sort(nArr);
                        md = nArr[1] / 1000;
                        md = Round(md, 1);
                        sitem["MIN_FP1"] = md.ToString("F1");
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md = Conversion.Val(sitem["KFYP2F" + xd].Trim());
                            nArr[xd] = Math.Abs(md);
                        }
                        Array.Sort(nArr);
                        md = nArr[1] / 1000;
                        md = Round(md, 1);
                        sitem["MIN_FP2"] = md.ToString("F1");
                        for (xd = 1; xd <= 3; xd++)
                        {
                            md = Conversion.Val(sitem["KFYP3F" + xd].Trim());
                            nArr[xd] = Math.Abs(md);
                        }
                        Array.Sort(nArr);
                        md = nArr[1] / 1000;
                        md = Round(md, 1);
                        sitem["MIN_FP3"] = md.ToString("F1");
                    }
                    if (sitem["SFDS"] == "是")
                    {
                        if (sitem["DSLB"] == "内开单扇无受力杆件" || sitem["DSLB"] == "内开单扇有受力杆件")
                            bl = sitem["MIN_ZP3"];
                        else
                            bl = sitem["MIN_FP3"];
                    }
                    else
                    {

                        md1 = Conversion.Val(sitem["MIN_FP3"]);
                        md2 = Conversion.Val(sitem["MIN_ZP3"]);
                        bl = md1 < md2 ? sitem["MIN_FP3"] : sitem["MIN_ZP3"];
                    }


                    flag = sitem["JCLB"] == "工程检测" ? true : false;
                    sitem["KFYSJYQ"] = sitem["KFYSJYQ"];
                    sitem["KFYSJYQ"] = IsNumeric(sitem["KFYSJYQ"]) ? sitem["KFYSJYQ"] + "≤" : sitem["KFYSJYQ"];
                    sitem["PD_KF"] = calc_PB(sitem["KFYSJYQ"], bl, true);
                    //功能障碍
                    if (sitem["GNZA1"] != "无功能障碍和损坏" || sitem["GNZA2"] != "无功能障碍和损坏" || sitem["GNZA3"] != "无功能障碍和损坏")
                    {
                        MItem[0]["YCQK"] = "有功能障碍和损坏";
                        sitem["PD_KF"] = "不符合";
                    }

                    mbHggs = sitem["PD_KF"] == "不符合" ? mbHggs + 1 : mbHggs;
                    if (sitem["PD_KF"] == "不符合") mbhgjg = mbhgjg + "、抗风压性能";
                    if (sitem["PD_KF"] == "符合") mhgjg = mhgjg + "、抗风压性能";
                    if (sitem["PD_KF"] == "----") djjg = djjg + "、抗风压性能";
                    //if (flag)
                    //    sitem["DJ_KF"] = "----";
                    //else
                    //{
                        var mrsDj_Filter = mrsDj[0];
                        Gs = mrsDj.Count();
                        for (xd = 1; xd <= Gs; xd++)
                        {
                            if (calc_PB(mrsDj_Filter["KFYQ"], bl, true) == "符合")
                            {
                                sitem["DJ_KF"] = mrsDj_Filter["KFJB"] + "级";
                                break;
                            }
                            if (xd >= mrsDj.Count())
                                mrsDj_Filter = mrsDj[xd - 1];
                            else
                                mrsDj_Filter = mrsDj[xd];

                        }
                        if (xd > Gs) sitem["DJ_KF"] = "不符合任一级别";
                    //}

                }
                else
                {
                    sitem["MIN_ZP1"] = "----";
                    sitem["MIN_FP1"] = "----";
                    sitem["MIN_ZP2"] = "----";
                    sitem["MIN_FP2"] = "----";
                    sitem["MIN_ZP3"] = "----";
                    sitem["MIN_FP3"] = "----";
                    sitem["PD_KF"] = "----";
                    sitem["KFYSJYQ"] = "----";
                    sitem["DJ_KF"] = "----";
                    sitem["KFYSJJB"] = "----";
                }
                MItem[0]["BEIZHU1"] = "窗形图见报告第二页。";
                if (MItem[0]["BEIZHU2"] != "----")
                    MItem[0]["BEIZHU1"] = MItem[0]["BEIZHU2"] + "窗形图见报告第二页。";
                if (sitem["SFDS"] == "是")
                {
                    if (sitem["DSLB"] == "内开单扇无受力杆件")
                    {
                        MItem[0]["BEIZHU1"] = "此窗为无受力杆件内开单扇平开窗，抗风压性能仅进行正压检测。" + MItem[0]["BEIZHU1"];
                    }
                    else
                    {
                        MItem[0]["BEIZHU1"] = "此窗为无受力杆件外开单扇平开窗，抗风压性能仅进行负压检测。" + MItem[0]["BEIZHU1"];
                    }

                    //if (sitem["DSLB"] == "内开单扇有受力杆件")
                    //{
                    //    MItem[0]["BEIZHU1"] = "此窗为有受力杆件内开单扇平开窗，抗风压性能仅进行正压检测。" + MItem[0]["BEIZHU1"];
                    //}
                    //else
                    //{
                    //    MItem[0]["BEIZHU1"] = "此窗为有受力杆件外开单扇平开窗，抗风压性能仅进行负压检测。" + MItem[0]["BEIZHU1"];
                    //}
                }
            }
            //综合判断
            string mjgsm = "";
            //主表总判断赋值
            if (mbhgjg.Length > 0)
                mbhgjg = mbhgjg.Substring(1);
            if (mhgjg.Length > 0)
                mhgjg = mhgjg.Substring(1);
            if (djjg.Length > 0)
                djjg = djjg.Substring(1);
            if (mbhgjg != "" && mhgjg != "")
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "，该组试样所检项目" + mhgjg + "符合工程设计要求，" + mbhgjg + "不符合设计要求。";
                MItem[0]["JCJG"] = "不合格";
            }
            if (mbhgjg != "" && mhgjg == "")
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "，该组试样所检项目" + mbhgjg + "不符合设计要求。";
                MItem[0]["JCJG"] = "不合格";
            }
            if (mbhgjg == "" && mhgjg != "")
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "，该组试样所检项目" + mhgjg + "符合设计要求。";
                MItem[0]["JCJG"] = "合格";
            }
            mjgsm = MItem[0]["JCJGMS"];
            if (djjg != "")
            {
                MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"].Length > 0 ? MItem[0]["JCJGMS"].Substring(0, MItem[0]["JCJGMS"].Length - 1) : MItem[0]["JCJGMS"];
                MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "," + djjg + "定级结果如上。";
                mjgsm = MItem[0]["JCJGMS"] + "," + djjg + "定级见报告。";
            }



            if (!flag)
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "，该组试样所检项目气密性能、水密性能、抗风压性能定级结果如上。";
                mjgsm = "该组试样为定级检测，结果见报告。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
