﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            string mMaxBgbh, sfhs;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            int mbHggs;
            mSFwc = true;
            #endregion

            #region 自定义函数

            //以下为对用设计值的判定方式
            //Func<string, string, string> calc_pd =
            //    delegate (string sj, string sc)
            //    {
            //        sj = sj.Trim();
            //        string calc_pd_ret = "";
            //        bool isStart = false;
            //        string tmpStr = string.Empty;
            //        if (string.IsNullOrEmpty(sc))
            //            sc = "";
            //        for (int i = 1; i <= sc.Length; i++)
            //        {
            //            if (IsNumeric(sc.Substring(i - 1, 1)) || sc.Substring(i - 1, 1) == "." || sc.Substring(i - 1, 1) == "-")
            //            {
            //                isStart = true;
            //                tmpStr = tmpStr + sc.Substring(i - 1, 1);
            //            }
            //            else
            //            {
            //                if (isStart == false && tmpStr != "")
            //                    break;
            //            }
            //        }
            //        sc = tmpStr;
            //        if (!IsNumeric(sc))
            //        {
            //            calc_pd_ret = "----";
            //        }
            //        else
            //        {
            //            double min_sjz, max_sjz, scz;
            //            int length, dw;
            //            if (sj.Contains("＞"))
            //            {
            //                length = sj.Length;
            //                dw = sj.IndexOf("＞") + 1;
            //                min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
            //                scz = Conversion.Val(sc);
            //                calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
            //            }
            //            if (sj.Contains(">"))
            //            {
            //                length = sj.Length;
            //                dw = sj.IndexOf(">") + 1;
            //                min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
            //                scz = Conversion.Val(sc);
            //                calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
            //            }
            //            if (sj.Contains("≥"))
            //            {
            //                length = sj.Length;
            //                dw = sj.IndexOf("≥") + 1;
            //                min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
            //                scz = Conversion.Val(sc);
            //                calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
            //            }
            //            if (sj.Contains("＜"))
            //            {
            //                length = sj.Length;
            //                dw = sj.IndexOf("＜") + 1;
            //                max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
            //                scz = Conversion.Val(sc);
            //                calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
            //            }
            //            if (sj.Contains("<"))
            //            {
            //                length = sj.Length;
            //                dw = sj.IndexOf("<") + 1;
            //                max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
            //                scz = Conversion.Val(sc);
            //                calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
            //            }
            //            if (sj.Contains("≤"))
            //            {
            //                length = sj.Length;
            //                dw = sj.IndexOf("≤") + 1;
            //                max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
            //                scz = Conversion.Val(sc);
            //                calc_pd_ret = scz <= max_sjz ? "符合" : "不符合";
            //            }
            //            if (sj.Contains("="))
            //            {
            //                length = sj.Length;
            //                dw = sj.IndexOf("=") + 1;
            //                max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
            //                scz = Conversion.Val(sc);
            //                calc_pd_ret = scz == max_sjz ? "符合" : "不符合";
            //            }
            //            if (sj.Contains("～"))
            //            {
            //                length = sj.Length;
            //                dw = sj.IndexOf("～") + 1;
            //                min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
            //                isStart = false;
            //                tmpStr = "";
            //                for (int i = 1; i <= min_sjz.ToString().Length; i++)
            //                {
            //                    if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
            //                    {
            //                        isStart = true;
            //                        tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
            //                    }
            //                    else
            //                    {
            //                        if (!isStart && tmpStr != "")
            //                            break;
            //                    }
            //                }
            //                min_sjz = Conversion.Val(tmpStr);
            //                max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
            //                isStart = false;
            //                tmpStr = "";
            //                for (int i = 1; i <= max_sjz.ToString().Length; i++)
            //                {
            //                    if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
            //                    {
            //                        isStart = true;
            //                        tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
            //                    }
            //                    else
            //                    {
            //                        if (!isStart && tmpStr != "")
            //                            break;
            //                    }
            //                }
            //                max_sjz = Conversion.Val(tmpStr);
            //                scz = Conversion.Val(sc);
            //                calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
            //            }
            //            if (sj.Contains("~"))
            //            {
            //                length = sj.Length;
            //                dw = sj.IndexOf("~") + 1;
            //                min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
            //                isStart = false;
            //                tmpStr = "";
            //                for (int i = 1; i <= min_sjz.ToString().Length; i++)
            //                {
            //                    if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
            //                    {
            //                        isStart = true;
            //                        tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
            //                    }
            //                    else
            //                    {
            //                        if (!isStart && tmpStr != "")
            //                            break;
            //                    }
            //                }
            //                min_sjz = Conversion.Val(tmpStr);
            //                max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
            //                isStart = false;
            //                tmpStr = "";
            //                for (int i = 1; i <= max_sjz.ToString().Length; i++)
            //                {
            //                    if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
            //                    {
            //                        isStart = true;
            //                        tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
            //                    }
            //                    else
            //                    {
            //                        if (!isStart && tmpStr != "")
            //                            break;
            //                    }
            //                }
            //                max_sjz = Conversion.Val(tmpStr);
            //                scz = Conversion.Val(sc);
            //                calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
            //            }
            //        }
            //        return calc_pd_ret;
            //    };

            //这个函数总括了对于判定(符合,不符合) 还是判断(合格,不合格)
            //Func<string, string, bool, string> calc_PB =
            //    delegate (string sj_fun, string sc_fun, bool flag_fun)
            //    {
            //        string calc_PB_fun = string.Empty;
            //        if (string.IsNullOrEmpty(sj_fun) || string.IsNullOrEmpty(sc_fun))
            //        {
            //            if (flag_fun)
            //            {
            //                return "不符合";
            //            }
            //            else
            //            {
            //                return "不合格";
            //            }
            //        }
            //        sj_fun = sj_fun.Trim();
            //        sc_fun = sc_fun.Trim();
            //        if (!IsNumeric(sc_fun))
            //        {
            //            calc_PB_fun = "----";
            //        }
            //        else
            //        {
            //            sj_fun = sj_fun.Replace("~", "～");
            //            string l_bl, r_bl;
            //            double min_sjz, max_sjz, scz;
            //            int length, dw;
            //            bool min_bl, max_bl, sign_fun;
            //            min_sjz = -99999;
            //            max_sjz = 99999;
            //            scz = GetSafeDouble(sc_fun);
            //            sign_fun = false;
            //            min_bl = false;
            //            max_bl = false;
            //            if (sj_fun.Contains("＞"))
            //            {
            //                length = sj_fun.Length;
            //                dw = sj_fun.IndexOf("＞") + 1;
            //                l_bl = sj_fun.Substring(0, dw - 1);
            //                r_bl = sj_fun.Substring(dw, length - dw);
            //                if (IsNumeric(l_bl))
            //                {
            //                    max_sjz = GetSafeDouble(l_bl);
            //                    max_bl = false;
            //                }
            //                if (IsNumeric(r_bl))
            //                {
            //                    min_sjz = GetSafeDouble(r_bl);
            //                    min_bl = false;
            //                }
            //                sign_fun = true;
            //            }
            //            if (sj_fun.Contains("≥"))
            //            {
            //                length = sj_fun.Length;
            //                dw = sj_fun.IndexOf("≥") + 1;
            //                l_bl = sj_fun.Substring(0, dw - 1);
            //                r_bl = sj_fun.Substring(dw, length - dw);
            //                if (IsNumeric(l_bl))
            //                {
            //                    max_sjz = GetSafeDouble(l_bl);
            //                    max_bl = true;
            //                }
            //                if (IsNumeric(r_bl))
            //                {
            //                    min_sjz = GetSafeDouble(r_bl);
            //                    min_bl = true;
            //                }
            //                sign_fun = true;
            //            }
            //            if (sj_fun.Contains("＜"))
            //            {
            //                length = sj_fun.Length;
            //                dw = sj_fun.IndexOf("＜") + 1;
            //                l_bl = sj_fun.Substring(0, dw - 1);
            //                r_bl = sj_fun.Substring(dw, length - dw);
            //                if (IsNumeric(l_bl))
            //                {
            //                    min_sjz = GetSafeDouble(l_bl);
            //                    min_bl = false;
            //                }
            //                if (IsNumeric(r_bl))
            //                {
            //                    max_sjz = GetSafeDouble(r_bl);
            //                    max_bl = false;
            //                }
            //                sign_fun = true;
            //            }
            //            if (sj_fun.Contains("≤"))
            //            {
            //                length = sj_fun.Length;
            //                dw = sj_fun.IndexOf("≤") + 1;
            //                l_bl = sj_fun.Substring(0, dw - 1);
            //                r_bl = sj_fun.Substring(dw, length - dw);
            //                if (IsNumeric(l_bl))
            //                {
            //                    min_sjz = GetSafeDouble(l_bl);
            //                    min_bl = true;
            //                }
            //                if (IsNumeric(r_bl))
            //                {
            //                    max_sjz = GetSafeDouble(r_bl);
            //                    max_bl = true;
            //                }
            //                sign_fun = true;
            //            }
            //            if (sj_fun.Contains("～"))
            //            {
            //                length = sj_fun.Length;
            //                dw = sj_fun.IndexOf("～") + 1;
            //                min_sjz = GetSafeDouble(sj_fun.Substring(0, dw - 1));
            //                max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
            //                min_bl = true;
            //                max_bl = true;
            //                sign_fun = true;
            //            }
            //            if (sj_fun.Contains("±"))
            //            {
            //                length = sj_fun.Length;
            //                dw = sj_fun.IndexOf("±") + 1;
            //                min_sjz = GetSafeDouble(sj_fun.Substring(0, dw - 1));
            //                max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
            //                min_sjz = min_sjz - max_sjz;
            //                max_sjz = min_sjz + 2 * max_sjz;
            //                min_bl = true;
            //                max_bl = true;
            //                sign_fun = true;
            //            }
            //            if (sj_fun == "0" && !string.IsNullOrEmpty(sj_fun))
            //            {
            //                sign_fun = true;
            //                min_bl = false;
            //                max_bl = false;
            //                max_sjz = 0;
            //            }
            //            if (!sign_fun)
            //            {
            //                calc_PB_fun = "----";
            //            }
            //            else
            //            {
            //                string hgjl, bhgjl;
            //                hgjl = flag_fun ? "符合" : "合格";
            //                bhgjl = flag_fun ? "不符合" : "不合格";
            //                sign_fun = true; //做为判定了
            //                if (min_bl)
            //                    sign_fun = scz >= min_sjz ? sign_fun : false;
            //                else
            //                    sign_fun = scz > min_sjz ? sign_fun : false;
            //                if (max_bl)
            //                    sign_fun = scz <= max_sjz ? sign_fun : false;
            //                else
            //                    sign_fun = scz < max_sjz ? sign_fun : false;
            //                calc_PB_fun = sign_fun ? hgjl : bhgjl;
            //            }
            //        }
            //        return calc_PB_fun;
            //    };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_JZ_DJ"];
            var MItem = data["M_JZ"];
            var mitem = MItem[0];
            var SItem = data["S_JZ"];
            //var mrsDrxs = data["ZM_DRJL"];
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            var jcxmBhg = "";
            var jcxmCur = "";
            foreach (var sitem in SItem)
            {
                string md11 = "", md12 = "";
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
                    string cpmc = sitem["CPMC"].Trim().Replace("—", "-");
                    switch (cpmc)
                    {
                        case "绝热用岩棉、矿渣棉及其制品(GB/T 11835-2016)":
                            if (sitem["CPMC"].Trim().Replace("—", "-") == mrsDj_item["YPYT"] && sitem["LX"].Trim() == mrsDj_item["LX"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"])
                            {
                                //mitem["G_ZLXSL"] = "≤5.0";
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"]; //要求质量吸湿率
                                mitem["G_GQPXD"] = "≤10";
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"]; //导热系数
                                mitem["G_CDPC"] = mrsDj_item["G_CDPC"];  //长度偏差要求
                                mitem["G_KDPC"] = mrsDj_item["G_KDPC"];  //宽度偏差要求
                                mitem["G_HDPC"] = mrsDj_item["G_HDPC"];  //厚度偏差要求
                                mitem["G_HDYXPC"] = mrsDj_item["G_HDPC"];  //厚度偏差要求
                                md11 = mrsDj_item["BCMD"];  //标称密度
                                mitem["G_MDYXPC"] = mrsDj_item["G_MD"];  //密度偏差要求
                                mitem["G_XSL"] = mrsDj_item["G_XSL"];  //吸水性要求
                            }
                            break;
                        //case "建筑用岩棉绝热制品(GB/T 19686-2015)":
                        case "建筑用岩棉绝热制品(GB/T 19686-2015)":
                            if (sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"])
                            {
                                if (sitem["ZPXS"].Trim() == "板" || sitem["ZPXS"].Trim() == "条") //GB/T 19686 只有板、条才有导热系数
                                {
                                    mitem["G_DRXS"] = mrsDj_item["G_DRXS"]; //导热系数
                                }
                                else
                                {
                                    mitem["G_DRXS"] = "----";
                                    mitem["W_DRXS"] = "----";
                                    mitem["GH_DRXS"] = "----";
                                }

                                if (sitem["YYLX"].Trim() == mrsDj_item["YYLX"])
                                {
                                    mitem["G_CDPC"] = mrsDj_item["G_CDPC"];  //长度偏差要求
                                    mitem["G_KDPC"] = mrsDj_item["G_KDPC"];  //宽度偏差要求
                                    mitem["G_HDPC"] = mrsDj_item["G_HDPC"];  //厚度偏差要求
                                    mitem["G_HDYXPC"] = mrsDj_item["G_HDPC"];  //厚度偏差要求
                                    md11 = mrsDj_item["BCMD"];  //密度≥80kg/m3
                                    md12 = mrsDj_item["G_MD"];  //密度<80kg/m3
                                    mitem["G_MDYXPC"] = mrsDj_item["G_MD"];  //密度偏差要求
                                    mitem["G_XSL"] = mrsDj_item["G_XSL"];  //吸水性要求
                                    mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"]; //要求质量吸湿率
                                }
                            }

                            //mitem["G_ZLXSL"] = "≤5.0";

                            break;
                        case "建筑外墙外保温用岩棉制品(GB/T 25975-2018)":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["YYLX"].Trim() == mrsDj_item["YYLX"])
                            {
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_CCWDX"] = mrsDj_item["G_CCWDX"];
                                mitem["G_CQXSL"] = mrsDj_item["G_CQXSL"];
                                mitem["G_DQXSL"] = mrsDj_item["G_DQXSL"];
                                mitem["G_GQPXD"] = mrsDj_item["G_GQPXD"];
                                mitem["G_KLQD"] = mrsDj_item["G_KLQD"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"]; //导热系数
                                mitem["G_YSQD"] = mrsDj_item["G_YSQD"];
                                mitem["G_CDPC"] = mrsDj_item["G_CDPC"];  //长度偏差要求
                                mitem["G_KDPC"] = mrsDj_item["G_KDPC"];  //宽度偏差要求
                                mitem["G_HDPC"] = mrsDj_item["G_HDPC"];  //厚度偏差要求
                                mitem["G_HDYXPC"] = mrsDj_item["G_HDPC"];  //厚度偏差要求
                                mitem["G_MDYXPC"] = mrsDj_item["BCMD"];  //密度偏差要求
                                mitem["G_XSL"] = mrsDj_item["G_XSL"];  //吸水性要求
                            }
                            break;
                        case "矿物棉喷涂绝热层GB/T26746-2011":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["LX"].Trim() == mrsDj_item["LX"])
                            {
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_MD"] = mrsDj_item["G_MD"];
                                mitem["G_JSBLL"] = mrsDj_item["G_JSBLL"];
                                mitem["G_HDPC"] = mrsDj_item["G_HDPC"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                mitem["G_XSL"] = mrsDj_item["G_XSL"];
                            }
                            break;
                        case "矿物棉喷涂绝热层JC/T909-2003":
                            if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["LX"].Trim() == mrsDj_item["LX"] && sitem["YYLX"].Trim() == mrsDj_item["YYLX"])
                            {
                                mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                            }
                            break;
                        case "绝热用玻璃棉及其制品(GB/T 13350-2008)":
                            switch (sitem["ZPXS"].Trim())
                            {
                                case "棉":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"] && sitem["LX"].Trim() == mrsDj_item["LX"])
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                    }
                                    break;
                                case "板":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"] && sitem["BCMD"].Trim() == mrsDj_item["BCMD"])
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                    }
                                    break;
                                case "带":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"])
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                    }
                                    break;
                                case "毯":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"] && sitem["LX"].Trim() == mrsDj_item["LX"])
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                    }
                                    break;
                                case "毡":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"])
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                    }
                                    break;
                                case "管壳":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"])
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                        mitem["G_GQPXD"] = mrsDj_item["G_GQPXD"];
                                    }
                                    break;
                            }
                            break;
                        case "绝热用玻璃棉及其制品(GB/T 13350-2017)":
                            sfhs = IsQualified(mrsDj_item["BCMD"], sitem["BCMD"], true);
                            switch (sitem["ZPXS"].Trim())
                            {
                                case "板":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"] && sfhs == "符合")
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                    }
                                    break;
                                case "毯":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"] && sfhs == "符合")
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                    }
                                    break;
                                case "毡":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"] && sfhs == "符合")
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                    }
                                    break;
                                case "管壳":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"])
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                        mitem["G_GQPXD"] = mrsDj_item["G_GQPXD"];
                                    }
                                    break;
                                case "条\"\"":
                                    if (sitem["CPMC"].Trim() == mrsDj_item["YPYT"] && sitem["ZPXS"].Trim() == mrsDj_item["ZPXS"])
                                    {
                                        mitem["G_MD"] = mrsDj_item["G_MD"];
                                        mitem["G_DRXS"] = mrsDj_item["G_DRXS"];
                                        mitem["G_ZLXSL"] = mrsDj_item["G_ZLXSL"];
                                        mitem["G_XSL"] = mrsDj_item["G_XSL"];
                                        mitem["G_GQPXD"] = mrsDj_item["G_GQPXD"];
                                    }
                                    break;
                            }
                            break;
                    }
                }
                //if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                //{
                mbHggs = 0;
                int mcd, mdwz, hgs;
                bool sign, mark;
                string CPMC, zpxs = "", bcz = "", lx = "";
                int cl, length;
                double md1, md2, xd1, xd2, md, pjmd, sum, cd, kd, hd, zl;
                string bl;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                hgs = 0;
                CPMC = sitem["CPMC"].Trim();
                zpxs = sitem["ZPXS"].Trim();
                lx = sitem["LX"].Trim();
                sign = false;
                mark = true;
                //密度、抗压、冻融、吸水率
                #region 密度
                sign = true;
                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";
                    //判定计算了≤≥±＜＞～
                    if (CPMC == "绝热用岩棉、矿渣棉及其制品(GB/T 11835-2016)" && zpxs == "棉")
                    {
                        mitem["G_MD"] = "≤150";
                        mitem["GH_MD"] = IsQualified("≤150", mitem["W_PJMD"], false);
                        mitem["G_MD"] = "平均值≤150";
                    }
                    else if (CPMC == "绝热用岩棉、矿渣棉及其制品(GB/T 11835-2016)" && zpxs != "棉")
                    {
                        mitem["G_MDDZ"] = "±10";
                        mitem["G_MDBC"] = "±10";
                        mark = true;
                        //单值的
                        List<double> mkyqdArray = new List<double>();
                        for (int i = 1; i <= 4; i++)
                        {
                            mkyqdArray.Add(GetSafeDouble(mitem["W_MD" + i]));
                        }
                        mkyqdArray.Sort();

                        md = Conversion.Val(mkyqdArray.Average());
                        md1 = md - md * 10 / 100;
                        md1 = Round(md1, 0);
                        md2 = md + md * 10 / 100;
                        md2 = Round(md2, 0);
                        bl = md1.ToString() + "～" + md2.ToString();
                        for (xd = 1; xd <= 4; xd++)
                        {
                            if (IsQualified(bl, mitem["W_MD" + xd], false) == "不合格")
                            {
                                mark = false;
                                break;
                            }
                        }
                        //md = Conversion.Val(sitem["BCMD"].Trim());
                        //md1 = md - md * 10 / 100;
                        //md1 = Round(md1, 0);
                        //md2 = md + md * 10 / 100;
                        //md2 = Round(md2, 0);
                        //bl = md1.ToString() + "～" + md2.ToString();
                        //if (IsQualified(bl, mitem["W_PJMD"], false) == "不合格")
                        //    mark = false;
                        mitem["GH_MD"] = mark ? "合格" : "不合格";
                        mitem["G_MD"] = "单值允许偏差±10%";
                    }
                    else if (CPMC == "矿物棉喷涂绝热层GB/T26746-2011")
                    {
                        mitem["G_MDDZ"] = "----";
                        mitem["G_MDBC"] = "±10";
                        md = Conversion.Val(sitem["BCMD"].Trim());
                        md1 = md - md * 10 / 100;
                        md1 = Round(md1, 0);
                        md2 = md + md * 10 / 100;
                        md2 = Round(md2, 0);
                        bl = md1.ToString() + "～" + md2.ToString();
                        if (IsQualified(bl, mitem["W_PJMD"], false) == "不合格")
                            mark = false;
                        mitem["GH_MD"] = mark ? "合格" : "不合格";
                        mitem["G_MD"] = "平均值与标称值允许偏差±10%";
                    }
                    else if (CPMC == "建筑用岩棉绝热制品(GB/T 19686-2015)" && zpxs == "毡")
                    {
                        mitem["G_MDDZ"] = "----";
                        mitem["G_MDBC"] = "±10";
                        md = Conversion.Val(sitem["BCMD"].Trim());
                        md1 = md - md * 10 / 100;
                        md1 = Round(md1, 0);
                        md2 = md + md * 10 / 100;
                        md2 = Round(md2, 0);
                        bl = md1.ToString() + "～" + md2.ToString();
                        for (xd = 1; xd <= 4; xd++)
                        {
                            if (IsQualified(bl, mitem["W_MD" + xd], false) == "不合格")
                                hgs = hgs + 1;
                        }
                        mitem["GH_MD"] = hgs <= 1 ? "合格" : "不合格";
                        mitem["G_MD"] = "平均值与标称值允许偏差±10%";
                    }
                    else if (CPMC == "建筑用岩棉绝热制品(GB/T 19686-2015)" && zpxs != "毡")
                    {
                        mitem["G_MDDZ"] = "----";
                        mitem["G_MDBC"] = "±15";
                        md = Conversion.Val(sitem["BCMD"].Trim());
                        md1 = md - md * 15 / 100;
                        md1 = Round(md1, 0);
                        md2 = md + md * 15 / 100;
                        md2 = Round(md2, 0);
                        bl = md1.ToString() + "～" + md2.ToString();
                        for (xd = 1; xd <= 4; xd++)
                        {
                            if (IsQualified(bl, mitem["W_MD" + xd], false) == "不合格")
                                hgs = hgs + 1;
                        }
                        mitem["GH_MD"] = hgs <= 1 ? "合格" : "不合格";
                        mitem["G_MD"] = "平均值与标称值允许偏差±15%";
                    }
                    else if (CPMC == "绝热用玻璃棉及其制品(GB/T 13350-2008)" && zpxs == "板")
                    {
                        if (Conversion.Val(sitem["BCMD"]) == 16)
                            mitem["G_MD"] = "15～17";
                        for (xd = 1; xd <= 4; xd++)
                        {
                            if (IsQualified(mitem["G_MD"], mitem["W_MD" + xd], false) == "不合格")
                                hgs = hgs + 1;
                        }
                        mitem["GH_MD"] = hgs <= 1 ? "合格" : "不合格";
                    }

                    else if (CPMC == "绝热用玻璃棉及其制品(GB/T 13350-2008)" && zpxs != "板")
                    {
                        for (xd = 1; xd <= 4; xd++)
                        {
                            if (IsQualified(mitem["G_MD"], (100 * (Conversion.Val(mitem["W_MD" + xd]) - Conversion.Val(sitem["BCMD"])) / Conversion.Val(sitem["BCMD"])).ToString(), false) == "不合格")
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
                    else if (CPMC == "绝热用玻璃棉及其制品(GB/T 13350-2017)")
                    {
                        for (xd = 1; xd <= 4; xd++)
                        {
                            if (IsQualified(mitem["G_MD"], (100 * (Conversion.Val(mitem["W_MD" + xd]) - Conversion.Val(sitem["BCMD"])) / Conversion.Val(sitem["BCMD"])).ToString(), false) == "不合格")
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
                    {
                        mitem["GH_MD"] = IsQualified(mitem["G_MD"], mitem["W_PJMD"], false);
                    }

                    if (mitem["GH_MD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
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
                    //mitem["W_PJMD"] = "----";
                }
                #endregion

                #region 热阻
                sign = true;
                mark = true;
                if (jcxm.Contains("、热阻、"))
                {
                    jcxmCur = "热阻";
                    sum = 0;
                    for (xd = 1; xd <= 4; xd++)
                    {
                        md = Conversion.Val(sitem["HD" + xd].Trim());
                        sum = sum + md;
                    }
                    hd = Round(sum / 4, 0);
                    md = Conversion.Val(mitem["W_PJMD"].Trim());
                    if (md <= 60)
                    {
                        if (hd <= 50)
                        {
                            xd1 = 30;
                            md1 = 0.71;
                            xd2 = 50;
                            md2 = 1.2;
                        }
                        else if (hd > 50 && hd <= 100)
                        {
                            xd1 = 50;
                            md1 = 1.2;
                            xd2 = 100;
                            md2 = 2.4;
                        }
                        else
                        {

                            xd1 = 100;
                            md1 = 2.4;
                            xd2 = 150;
                            md2 = 3.57;
                        }
                    }
                    else if (md > 60 && md <= 80)
                    {
                        if (hd <= 50)
                        {
                            xd1 = 30;
                            md1 = 0.75;
                            xd2 = 50;
                            md2 = 1.25;
                        }
                        else if (hd > 50 && hd <= 100)
                        {
                            xd1 = 50;
                            md1 = 1.25;
                            xd2 = 100;
                            md2 = 2.5;
                        }
                        else
                        {
                            xd1 = 100;
                            md1 = 2.5;
                            xd2 = 150;
                            md2 = 3.75;
                        }
                    }
                    else if (md > 80 && md <= 120)
                    {
                        if (hd <= 50)
                        {
                            xd1 = 30;
                            md1 = 0.79;
                            xd2 = 50;
                            md2 = 1.32;
                        }
                        else if (hd > 50 && hd <= 100)
                        {
                            xd1 = 50;
                            md1 = 1.32;
                            xd2 = 100;
                            md2 = 2.63;
                        }
                        else
                        {
                            xd1 = 100;
                            md1 = 2.63;
                            xd2 = 150;
                            md2 = 3.95;
                        }
                    }
                    else
                    {
                        if (hd <= 50)
                        {
                            xd1 = 30;
                            md1 = 0.75;
                            xd2 = 50;
                            md2 = 1.25;
                        }
                        else if (hd > 50 && hd <= 100)
                        {
                            xd1 = 50;
                            md1 = 1.25;
                            xd2 = 100;
                            md2 = 2.5;
                        }
                        else
                        {
                            xd1 = 100;
                            md1 = 2.5;
                            xd2 = 150;
                            md2 = 3.75;
                        }
                    }
                    pjmd = md2 - (md2 - md1) * (xd2 - hd) / (xd2 - xd1);
                    pjmd = Round(pjmd, 2);
                    mitem["G_RZ"] = "≥" + pjmd.ToString("0.00");
                    if (sitem["BCRZ"] != "----" && sitem["BCRZ"].Trim() != "")
                    {
                        bcz = sitem["BCRZ"];
                        mitem["G_RZ"] = bcz;
                        mark = IsQualified(bcz, mitem["W_RZ"], false) == "合格" ? mark : false;
                    }
                    else
                        mark = IsQualified(mitem["G_RZ"], mitem["W_RZ"], false) == "合格" ? mark : false;
                    mitem["G_RZ"] = mitem["G_RZ"] + "（标准要求值）,且" + bcz + "标称值";
                    if (mark)
                    {
                        mitem["GH_RZ"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mitem["GH_RZ"] = "不合格";
                    }
                }
                else
                    sign = false;

                if (!sign)
                {
                    mitem["W_RZ"] = "----";
                    mitem["G_RZ"] = "----";
                    mitem["GH_RZ"] = "----";
                }
                #endregion

                #region 燃烧性能
                sign = true;
                if (jcxm.Contains("、燃烧性能、"))
                {
                    jcxmCur = "燃烧性能";
                    mitem["GH_RSXN"] = "合格";
                    mitem["G_RSXN"] = "不燃材料";
                    if (mitem["W_RSXN"] == "不符合")
                    {
                        mitem["GH_RSXN"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    mitem["W_RSXN"] = "----";
                    mitem["GH_RSXN"] = "----";
                    mitem["G_RSXN"] = "----";
                }
                #endregion

                #region 导热系数OLD
                sign = true;
                mark = true;

                #region 导热系数 old

                //if (jcxm.Contains("、导热系数、"))
                //{
                //    //1-棉,棉,1|板,板,0|带,带,0|毡,毡,0|缝毡,缝毡,0|贴面毡,贴面毡,0|管壳,管壳,0
                //    if (CPMC == "绝热用岩棉、矿渣棉及其制品(GB/T 11835-2016)")
                //    {
                //        md = Conversion.Val(sitem["BCMD"].Trim());
                //        switch (zpxs)
                //        {
                //            case "棉":
                //                mitem["G_DRXS"] = "≤0.044";
                //                break;
                //            case "带":
                //                if (md <= 100)
                //                    mitem["G_DRXS"] = "≤0.052";
                //                else
                //                    mitem["G_DRXS"] = "≤0.049";
                //                break;
                //            case "管壳":
                //                mitem["G_DRXS"] = "≤0.044";
                //                break;
                //            default:
                //                if (md <= 100)
                //                    mitem["G_DRXS"] = "≤0.044";
                //                else if (md > 160)
                //                    mitem["G_DRXS"] = "≤0.044";
                //                else
                //                    mitem["G_DRXS"] = "≤0.043";
                //                break;
                //        }
                //        mitem["GH_DRXS"] = calc_PB(mitem["G_DRXS"], mitem["W_DRXS"], false);
                //    }
                //    else
                //    {
                //        if (CPMC == "绝热用玻璃棉及其制品(GB/T 13350-2008)" && zpxs == "毯" && Conversion.Val(sitem["BCMD"]) > 40)
                //            mitem["G_DRXS"] = "≤0.043";
                //        if (CPMC == "绝热用玻璃棉及其制品(GB/T 13350-2008)" && zpxs == "毯" && Conversion.Val(sitem["BCMD"]) <= 40)
                //            mitem["G_DRXS"] = "≤0.048";
                //        else if (CPMC == "绝热用玻璃棉及其制品(GB/T 13350-2008)" && zpxs == "板" && Conversion.Val(sitem["BCMD"]) == 16)
                //            mitem["G_DRXS"] = "≤0.052";
                //        //if (mitem["JYDBH"] == "180600002")
                //        //    mitem["G_DRXS"] = "≤0.056";
                //        if (sitem["BCDRXS"].Trim() != "----" && sitem["BCDRXS"].Trim() != "")
                //        {
                //            bcz = sitem["BCDRXS"];
                //            mark = calc_PB(bcz, mitem["W_DRXS"], false) == "合格" ? mark : false;
                //            mark = calc_PB(mitem["G_DRXS"], mitem["W_DRXS"], false) == "合格" ? mark : false;
                //            mitem["W_DRXS1"] = mitem["W_DRXS"];
                //            mcd = mitem["G_DRXS"].Length;
                //            mdwz = mitem["G_DRXS"].IndexOf(".") + 1;
                //            mcd = mcd - mdwz;
                //            mitem["W_DRXS1"] = Round(Conversion.Val(mitem["W_DRXS"]), mcd).ToString();
                //            mitem["G_DRXS"] = mitem["G_DRXS"] + "（标准要求值）,且" + bcz + "标称值";
                //        }
                //        else
                //        {
                //            mark = calc_PB(mitem["G_DRXS"], mitem["W_DRXS"], false) == "合格" ? mark : false;
                //            mitem["W_DRXS1"] = mitem["W_DRXS"];
                //            mcd = mitem["G_DRXS"].Length;
                //            mdwz = mitem["G_DRXS"].IndexOf(".");
                //            mcd = mcd - mdwz;
                //            mitem["W_DRXS1"] = Round(Conversion.Val(mitem["W_DRXS"]), mcd).ToString();
                //        }
                //        if (mark)
                //            mitem["GH_DRXS"] = "合格";
                //        else
                //            mitem["GH_DRXS"] = "不合格";
                //    }
                //}
                //else
                //    sign = false;
                //if (!sign)
                //{
                //    mitem["W_DRXS"] = "----";
                //    mitem["G_DRXS"] = "----";
                //    mitem["GH_DRXS"] = "----";
                //}
                #endregion
                #endregion

                #region 压缩强度
                sign = true;
                mark = true;
                if (jcxm.Contains("、压缩强度、"))
                {
                    jcxmCur = "压缩强度";
                    if (CPMC == "绝热用岩棉、矿渣棉及其制品(GB/T 11835-2016)")
                    {
                        md = Conversion.Val(mitem["W_PJMD"].Trim());
                        if (md <= 120)
                            mitem["G_YSQD"] = "≥10";
                        else if (md >= 160)
                            mitem["G_YSQD"] = "≥20";
                        else
                            mitem["G_YSQD"] = "≥40";
                    }


                    if (sitem["BCYSQD"].Trim() != "----")
                    {
                        bcz = sitem["BCYSQD"];
                        if (!bcz.Contains("≥") || !bcz.Contains("≤"))
                        {
                            bcz = "≥" + bcz;
                        }
                        mark = IsQualified(bcz, mitem["W_YSQD"], false) == "合格" ? mark : false;
                        if (mitem["G_YSQD"] == "----" || mitem["G_YSQD"] == "")
                            mitem["G_YSQD"] = bcz + "标称值";
                        else
                        {
                            mark = IsQualified(mitem["G_YSQD"], mitem["W_YSQD"], false) == "合格" ? mark : false;
                            mitem["G_YSQD"] = mitem["G_YSQD"] + "（标准要求值）,且" + bcz + "标称值";
                        }
                    }
                    else
                        mark = IsQualified(mitem["G_YSQD"], mitem["W_YSQD"], false) == "合格" ? mark : false;


                    if (mark == true)
                    {
                        mitem["GH_YSQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mitem["GH_YSQD"] = "不合格";
                    }
                }
                else
                    sign = false;

                if (!sign)
                {
                    mitem["G_YSQD"] = "----";
                    mitem["GH_YSQD"] = "----";
                    mitem["W_YSQD"] = "----";
                }

                #endregion

                #region 质量吸湿率
                sign = true;
                if (jcxm.Contains("、质量吸湿率、") || jcxm.Contains("、吸湿性、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "质量吸湿率,吸湿性");
                    sign = IsNumeric(mitem["W_ZLXSL"]) && !string.IsNullOrEmpty(mitem["W_ZLXSL"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_ZLXSL"] = IsQualified(mitem["G_ZLXSL"], mitem["W_ZLXSL"], false);
                        if (mitem["GH_ZLXSL"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        mitem["GH_ZLXSL"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                else
                {
                    mitem["W_ZLXSL"] = "----";
                    mitem["G_ZLXSL"] = "----";
                    mitem["GH_ZLXSL"] = "----";
                }
                #endregion

                #region 尺寸稳定性
                sign = true;
                if (jcxm.Contains("、尺寸稳定性、"))
                {
                    jcxmCur = "尺寸稳定性";
                    sign = IsNumeric(mitem["W_CDWDX"]) && !string.IsNullOrEmpty(mitem["W_CDWDX"]) ? sign : false;
                    sign = IsNumeric(mitem["W_KDWDX"]) && !string.IsNullOrEmpty(mitem["W_KDWDX"]) ? sign : false;
                    sign = IsNumeric(mitem["W_HDWDX"]) && !string.IsNullOrEmpty(mitem["W_HDWDX"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_CDWDX"] = IsQualified(mitem["G_CCWDX"], mitem["W_CDWDX"], false);
                        mitem["GH_KDWDX"] = IsQualified(mitem["G_CCWDX"], mitem["W_KDWDX"], false);
                        mitem["GH_HDWDX"] = IsQualified(mitem["G_CCWDX"], mitem["W_HDWDX"], false);
                        if (mitem["GH_CDWDX"] == "不合格" || mitem["GH_KDWDX"] == "不合格" || mitem["GH_HDWDX"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
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
                #endregion

                #region 短期吸水量
                sign = true;
                if (jcxm.Contains("、短期吸水量、"))
                {
                    jcxmCur = "短期吸水量";
                    sign = IsNumeric(mitem["W_DQXSL"]) && !string.IsNullOrEmpty(mitem["W_DQXSL"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_DQXSL"] = IsQualified(mitem["G_DQXSL"], mitem["W_DQXSL"], false);
                        if (mitem["GH_DQXSL"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        mitem["GH_DQXSL"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    mitem["W_DQXSL"] = "----";
                    mitem["G_DQXSL"] = "----";
                    mitem["GH_DQXSL"] = "----";
                }
                #endregion

                #region 长期吸水量
                sign = true;
                mark = true;
                if (jcxm.Contains("、长期吸水量、"))
                {
                    jcxmCur = "长期吸水量";
                    sign = IsNumeric(mitem["W_CQXSL"]) && !string.IsNullOrEmpty(mitem["W_CQXSL"]) ? sign : false;
                    if (sign)
                    {
                        if (sitem["CQXSLZB"] != "----" && sitem["CQXSLZB"] != "")
                        {
                            bcz = sitem["CQXSLZB"];
                            mark = IsQualified(bcz, mitem["W_CQXSL"], false) == "合格" ? mark : false;
                            mark = IsQualified(mitem["G_CQXSL"], mitem["W_CQXSL"], false) == "合格" ? mark : false;
                            mitem["G_CQXSL"] = mitem["G_CQXSL"] + "（标准要求值）,且" + bcz + "标称值";
                        }
                        else
                            mark = IsQualified(mitem["G_CQXSL"], mitem["W_CQXSL"], false) == "合格" ? mark : false;
                        if (mark == true)
                        {
                            mitem["GH_CQXSL"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mitem["GH_CQXSL"] = "不合格";
                        }
                    }
                }
                else
                {
                    mitem["W_CQXSL"] = "----";
                    mitem["G_CQXSL"] = "----";
                    mitem["GH_CQXSL"] = "----";
                }
                #endregion

                #region 抗拉强度
                sign = true;
                mark = true;
                if (jcxm.Contains("、抗拉强度、"))
                {
                    jcxmCur = "抗拉强度";
                    sign = IsNumeric(mitem["W_KLQD"]) && !string.IsNullOrEmpty(mitem["W_KLQD"]) ? sign : false;
                    if (sign)
                    {
                        if (sitem["BCKLQD"].Trim() != "----" && sitem["BCKLQD"] != "")
                        {
                            bcz = sitem["BCKLQD"];
                            mark = IsQualified(bcz, mitem["W_KLQD"], false) == "合格" ? mark : false;
                            mark = IsQualified(mitem["G_KLQD"], mitem["W_KLQD"], false) == "合格" ? mark : false;
                            mitem["G_KLQD"] = mitem["G_KLQD"] + "（标准要求值）,且" + bcz + "标称值";
                        }
                        else
                            mark = IsQualified(mitem["G_KLQD"], mitem["W_KLQD"], false) == "合格" ? mark : false;

                        if (mark)
                        {
                            mitem["GH_KLQD"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mitem["GH_KLQD"] = "不合格";
                        }
                    }
                }
                else
                {
                    mitem["W_KLQD"] = "----";
                    mitem["G_KLQD"] = "----";
                    mitem["GH_KLQD"] = "----";
                }
                #endregion

                #region 粘结强度
                sign = true;
                if (jcxm.Contains("、粘结强度、"))
                {
                    jcxmCur = "粘结强度";
                    sign = IsNumeric(mitem["W_NJQD"]) && !string.IsNullOrEmpty(mitem["W_NJQD"]) ? sign : false;
                    if (sign)
                    {
                        if (!mitem["G_NJQD"].Trim().Contains("≥"))
                            mitem["G_NJQD"] = "≥" + mitem["G_NJQD"];
                        mitem["GH_NJQD"] = IsQualified(mitem["G_NJQD"], mitem["W_NJQD"], false);
                        if (mitem["GH_NJQD"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                else
                {
                    mitem["W_NJQD"] = "----";
                    mitem["G_NJQD"] = "----";
                    mitem["GH_NJQD"] = "----";
                }
                #endregion

                #region 浸水粘结强度保留率
                sign = true;
                if (jcxm.Contains("、浸水粘结强度保留率、"))
                {
                    jcxmCur = "浸水粘结强度保留率";
                    sign = IsNumeric(mitem["W_JSBLL"]) && !string.IsNullOrEmpty(mitem["W_JSBLL"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_JSBLL"] = IsQualified(mitem["G_JSBLL"], mitem["W_JSBLL"], false);
                        if (mitem["GH_JSBLL"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                else
                {
                    mitem["W_JSBLL"] = "----";
                    mitem["G_JSBLL"] = "----";
                    mitem["GH_JSBLL"] = "----";
                }
                #endregion

                #region 管壳偏心度
                sign = true;
                if (jcxm.Contains("、管壳偏心度、"))
                {
                    jcxmCur = "管壳偏心度";
                    sign = IsNumeric(mitem["W_GQPXD"]) && !string.IsNullOrEmpty(mitem["W_GQPXD"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_GQPXD"] = IsQualified(mitem["G_GQPXD"], mitem["W_GQPXD"], false);
                        if (mitem["GH_GQPXD"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                else
                {
                    mitem["W_GQPXD"] = "----";
                    mitem["G_GQPXD"] = "----";
                    mitem["GH_GQPXD"] = "----";
                }
                #endregion

                #region 吸水率 吸水性
                sign = true;
                if (jcxm.Contains("、吸水率、") || jcxm.Contains("、吸水性、")|| jcxm.Contains("、体积吸水率(全浸)、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "吸水率,吸水性,体积吸水率(全浸)");
                    if (sitem["XSXZB"].Trim().Contains("千克每立方"))
                    {
                        mdwz = sitem["XSXZB"].Trim().IndexOf("千克每立方") + 1;
                        mitem["G_XSL"] = sitem["XSXZB"].Substring(0, mdwz - 1);
                        sitem["XSXZBDW"] = "kg/m&scsup3&scend";
                        if (IsNumeric(mitem["W_XSL2"]) && !string.IsNullOrEmpty(mitem["W_XSL2"]))
                            mitem["W_XSL"] = mitem["W_XSL2"];
                    }
                    else if (sitem["XSXZB"].Trim().Contains("千克每平方"))
                    {
                        mdwz = sitem["XSXZB"].Trim().IndexOf("千克每平方") + 1;
                        mitem["G_XSL"] = sitem["XSXZB"].Substring(0, mdwz - 1);
                        sitem["XSXZBDW"] = "kg/m&scsup2&scend";
                        if (IsNumeric(mitem["W_XSL2"]) && !string.IsNullOrEmpty(mitem["W_XSL2"]))
                            mitem["W_XSL"] = mitem["W_XSL2"];
                    }
                    else if (sitem["XSXZB"].Trim().Contains("%"))
                    {
                        mdwz = sitem["XSXZB"].IndexOf("%") + 1;
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
                        mitem["GH_XSL"] = IsQualified(mitem["G_XSL"], mitem["W_XSL"], false);
                        if (mitem["GH_XSL"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                else
                {
                    sitem["XSXZBDW"] = "----";
                    mitem["W_XSL"] = "----";
                    mitem["G_XSL"] = "----";
                    mitem["GH_XSL"] = "----";
                }
                #endregion

                #region 厚度偏差
                sign = true;
                if (jcxm.Contains("、厚度偏差、"))
                {
                    jcxmCur = "厚度偏差";
                    sign = IsNumeric(mitem["W_HDPC"]) && !string.IsNullOrEmpty(mitem["W_HDPC"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_HDPC"] = IsQualified(mitem["G_HDPC"], mitem["W_HDPC"], false);
                        if (mitem["GH_HDPC"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        mitem["GH_HDPC"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                else
                {
                    mitem["W_HDPC"] = "----";
                    mitem["G_HDPC"] = "----";
                    mitem["GH_HDPC"] = "----";
                }
                #endregion

                #region 长度允许偏差
                sign = true;
                if (jcxm.Contains("、长度允许偏差、"))
                {
                    #region GB/T11835—2016
                    //if (CPMC == "绝热用岩棉、矿渣棉及其制品（GB/T11835—2016）")
                    //{
                    //    if (lx == "岩棉")
                    //    {
                    //        switch (zpxs)
                    //        {
                    //            case "板":
                    //                mitem["G_CDPC"] = "-3~10";
                    //                break;
                    //            case "毡":
                    //                mitem["G_CDPC"] = "-2~2";
                    //                break;
                    //            case "缝毡":
                    //                mitem["G_CDPC"] = "-2~2";
                    //                break;
                    //            case "管壳":
                    //                mitem["G_CDPC"] = "-3~5";
                    //                break;
                    //            default:
                    //                break;
                    //        }

                    //    }
                    //    else if (lx == "矿渣棉")
                    //    {
                    //        switch (zpxs)
                    //        {
                    //            case "板":
                    //                mitem["G_CDPC"] = "-3~10";
                    //                break;
                    //            case "毡":
                    //                mitem["G_CDPC"] = "-2~2";
                    //                break;
                    //            case "缝毡":
                    //                mitem["G_CDPC"] = "-2~2";
                    //                break;
                    //            case "管壳":
                    //                mitem["G_CDPC"] = "-3~5";
                    //                break;
                    //            default:
                    //                break;
                    //        }
                    //    }
                    //}
                    #endregion

                    jcxmCur = "长度允许偏差";
                    sign = IsNumeric(mitem["W_CDPC"]) && !string.IsNullOrEmpty(mitem["W_CDPC"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_CDPC"] = IsQualified(mitem["G_CDPC"], mitem["W_CDPC"], false);
                        if (mitem["GH_CDPC"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        mitem["GH_CDPC"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                else
                {
                    mitem["W_CDPC"] = "----";
                    mitem["G_CDPC"] = "----";
                    mitem["GH_CDPC"] = "----";
                }
                #endregion

                #region 宽度允许偏差
                sign = true;
                if (jcxm.Contains("、宽度允许偏差、"))
                {
                    #region GB/T11835—2016
                    //if (CPMC == "绝热用岩棉、矿渣棉及其制品（GB/T11835—2016）")
                    //{
                    //    if (lx == "岩棉")
                    //    {
                    //        switch (zpxs)
                    //        {
                    //            case "板":
                    //                mitem["G_KDPC"] = "-3~5";
                    //                break;
                    //            case "毡":
                    //                mitem["G_KDPC"] = "-3~5";
                    //                break;
                    //            case "缝毡":
                    //                mitem["G_KDPC"] = "-3~5";
                    //                break;
                    //            case "管壳":
                    //                //mitem["G_KDPC"] = "-3~5";
                    //                break;
                    //            default:
                    //                break;
                    //        }

                    //    }
                    //    else if (lx == "矿渣棉")
                    //    {
                    //        switch (zpxs)
                    //        {
                    //            case "板":
                    //                mitem["G_KDPC"] = "-3~5";
                    //                break;
                    //            case "毡":
                    //                mitem["G_KDPC"] = "-3~5";
                    //                break;
                    //            case "缝毡":
                    //                mitem["G_KDPC"] = "-3~5";
                    //                break;
                    //            case "管壳":
                    //                //mitem["G_KDPC"] = "-3~5";
                    //                break;
                    //            default:
                    //                break;
                    //        }
                    //    }
                    //}
                    #endregion

                    jcxmCur = "宽度允许偏差";
                    sign = IsNumeric(mitem["W_KDPC"]) && !string.IsNullOrEmpty(mitem["W_KDPC"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_KDPC"] = IsQualified(mitem["G_KDPC"], mitem["W_KDPC"], false);
                        if (mitem["GH_KDPC"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        mitem["GH_KDPC"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                else
                {
                    mitem["W_KDPC"] = "----";
                    mitem["G_KDPC"] = "----";
                    mitem["GH_KDPC"] = "----";
                }
                #endregion

                #region 厚度允许偏差
                sign = true;
                if (jcxm.Contains("、厚度允许偏差、"))
                {
                    #region GB/T11835—2016
                    //if (CPMC == "绝热用岩棉、矿渣棉及其制品（GB/T11835—2016）")
                    //{
                    //    if (lx == "岩棉")
                    //    {
                    //        switch (zpxs)
                    //        {
                    //            case "板":
                    //                mitem["G_HDYXPC"] = "-3~3";
                    //                break;
                    //            case "毡":
                    //                mitem["G_HDYXPC"] = "-3";
                    //                break;
                    //            case "缝毡":
                    //                mitem["G_HDYXPC"] = "-3";
                    //                break;
                    //            case "管壳":
                    //                mitem["G_HDYXPC"] = "-2~4";
                    //                break;
                    //            default:
                    //                break;
                    //        }

                    //    }
                    //    else if (lx == "矿渣棉")
                    //    {
                    //        switch (zpxs)
                    //        {
                    //            case "板":
                    //                mitem["G_HDYXPC"] = "-3~3";
                    //                break;
                    //            case "毡":
                    //                mitem["G_HDYXPC"] = "-3";
                    //                break;
                    //            case "缝毡":
                    //                mitem["G_HDYXPC"] = "-3";
                    //                break;
                    //            case "管壳":
                    //                mitem["G_HDYXPC"] = "-3~5";
                    //                break;
                    //            default:
                    //                break;
                    //        }
                    //    }
                    //}
                    #endregion
                    jcxmCur = "厚度允许偏差";
                    sign = IsNumeric(mitem["W_HDYXPC"]) && !string.IsNullOrEmpty(mitem["W_HDYXPC"]) ? sign : false;
                    if (sign)
                    {
                        mitem["GH_HDYXPC"] = IsQualified(mitem["G_HDYXPC"], mitem["W_HDYXPC"], false);
                        if (mitem["GH_HDYXPC"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        mitem["GH_HDYXPC"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                else
                {
                    mitem["W_HDYXPC"] = "----";
                    mitem["G_HDYXPC"] = "----";
                    mitem["GH_HDYXPC"] = "----";
                }
                #endregion

                #region 密度允许偏差
                sign = true;
                if (jcxm.Contains("、密度允许偏差、"))
                {
                    if (IsNumeric(sitem["BCMD"])) //判断是否为数字
                    {
                        #region GB/T11835—2016
                        //if (CPMC == "绝热用岩棉、矿渣棉及其制品（GB/T11835—2016）")
                        //{
                        //    if (lx == "岩棉")
                        //    {
                        //        switch (zpxs)
                        //        {
                        //            case "板":
                        //                mitem["G_MDYXPC"] = "-10~10";
                        //                break;
                        //            case "毡":
                        //                mitem["G_MDYXPC"] = "-10~10";
                        //                break;
                        //            case "缝毡":
                        //                mitem["G_MDYXPC"] = "-10~10";
                        //                break;
                        //            case "管壳":
                        //                mitem["G_MDYXPC"] = "-10~10";
                        //                break;
                        //            default:
                        //                break;
                        //        }

                        //    }
                        //    else if (lx == "矿渣棉")
                        //    {
                        //        switch (zpxs)
                        //        {
                        //            case "板":
                        //                mitem["G_MDYXPC"] = "-10~10";
                        //                break;
                        //            case "毡":
                        //                mitem["G_MDYXPC"] = "-10~10";
                        //                break;
                        //            case "缝毡":
                        //                mitem["G_MDYXPC"] = "-10~10";
                        //                break;
                        //            case "管壳":
                        //                mitem["G_MDYXPC"] = "-10~10";
                        //                break;
                        //            default:
                        //                break;
                        //        }
                        //    }

                        //}
                        #endregion

                        jcxmCur = "密度允许偏差";
                        #region GB/T 19686-2015
                        if (CPMC.Contains("19686")) //建筑用岩棉绝热制品(GB/T 19686-2015)
                        {

                            switch (sitem["YYLX"].Trim())
                            {
                                case "屋面和地板":
                                    if (GetSafeDouble(sitem["BCMD"]) >= 80)
                                    {
                                        mitem["G_MDYXPC"] = md11;
                                    }
                                    else
                                    {
                                        mitem["G_MDYXPC"] = md12;
                                    }
                                    break;
                                case "幕墙":
                                    if (GetSafeDouble(sitem["BCMD"]) >= 80)
                                    {
                                        mitem["G_MDYXPC"] = md11;
                                    }
                                    else
                                    {
                                        mitem["G_MDYXPC"] = md12;
                                    }
                                    break;
                                case "金属面夹芯板":
                                    mitem["G_MDYXPC"] = md11;
                                    break;
                                case "钢结构及内保温":
                                    if (GetSafeDouble(sitem["BCMD"]) >= 80)
                                    {
                                        mitem["G_MDYXPC"] = md11;
                                    }
                                    else
                                    {
                                        mitem["G_MDYXPC"] = md12;
                                    }
                                    break;
                            }
                        }
                        #endregion

                        if (CPMC.Contains("11835")) //GB/T11835—2016
                        {
                            sign = IsNumeric(mitem["W_MDYXPC"]) && !string.IsNullOrEmpty(mitem["W_MDYXPC"]) ? sign : false;

                            //if (sign && IsQualified(md11, sitem["BCMD"], false) == "合格")  //判断标准密度是否符合要求
                            //{
                            mitem["GH_MDYXPC"] = IsQualified(mitem["G_MDYXPC"], mitem["W_MDYXPC"], false);
                            //}
                            //else
                            //{
                            //    mitem["GH_MDYXPC"] = "不合格";
                            //}
                        }
                        else
                        {
                            sign = IsNumeric(mitem["W_MDYXPC"]) && !string.IsNullOrEmpty(mitem["W_MDYXPC"]) ? sign : false;
                            if (sign)
                            {
                                mitem["GH_MDYXPC"] = IsQualified(mitem["G_MDYXPC"], mitem["W_MDYXPC"], false);
                                if (mitem["GH_MDYXPC"] == "不合格")
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                }
                            }
                            else
                            {
                                mitem["GH_MDYXPC"] = "不合格";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }
                        }
                    }
                    else
                    {
                        mitem["GH_MDYXPC"] = "----";
                        mitem["W_MDYXPC"] = "----";
                    }
                }
                else
                {
                    mitem["GH_MDYXPC"] = "----";
                    mitem["W_MDYXPC"] = "----";
                    //mitem["W_PJMD"] = "----";
                    //mitem["G_MD"] = "----";
                    //mitem["GH_MD"] = "----";
                }
                #endregion

                #region 导热系数
                sign = true;
                if (jcxm.Contains("、导热系数、"))
                {
                    #region GB/T11835
                    if (CPMC == "绝热用岩棉、矿渣棉及其制品(GB/T 11835-2016)")
                    {
                        if (lx == "岩棉")
                        {
                            switch (zpxs)
                            {
                                case "板":
                                    mitem["G_DRXS"] = "≤0.043";
                                    break;
                                case "毡":
                                    mitem["G_DRXS"] = "≤0.043";
                                    break;
                                case "缝毡":
                                    mitem["G_DRXS"] = "≤0.043";
                                    break;
                                case "管壳":
                                    mitem["G_DRXS"] = "≤0.044";
                                    break;
                                default:
                                    break;
                            }

                        }
                        else if (lx == "矿渣棉")
                        {
                            switch (zpxs)
                            {
                                case "板":
                                    mitem["G_DRXS"] = "≤0.043";
                                    break;
                                case "毡":
                                    mitem["G_DRXS"] = "≤0.043";
                                    break;
                                case "缝毡":
                                    mitem["G_DRXS"] = "≤0.043";
                                    break;
                                case "管壳":
                                    mitem["G_DRXS"] = "≤0.044";
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    #endregion

                    jcxmCur = "导热系数";
                    if (CPMC.Contains("25975")) //GB/T 25975-2018
                    {
                        bool sign1 = true;
                        sign = IsNumeric(mitem["W_DRXS"]) && !string.IsNullOrEmpty(mitem["W_DRXS"]) ? sign : false;
                        sign1 = IsNumeric(sitem["BCDRXS"]) && !string.IsNullOrEmpty(sitem["BCDRXS"]) ? sign1 : false;

                        if (sign1)
                        {
                            if (sign && IsQualified("≤" + sitem["BCDRXS"], mitem["W_DRXS"], false) == "合格") //有标准导热系数时还应不大于其标称值
                            {
                                mitem["GH_DRXS"] = IsQualified(mitem["G_DRXS"], mitem["W_DRXS"], false);
                                if (mitem["GH_DRXS"] == "不合格")
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                }
                            }
                            else
                            {
                                mitem["GH_DRXS"] = "不合格";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }
                        }
                        else
                        {
                            if (sign)
                            {
                                mitem["GH_DRXS"] = IsQualified(mitem["G_DRXS"], mitem["W_DRXS"], false);
                                if (mitem["GH_DRXS"] == "不合格")
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                }
                            }
                            else
                            {
                                mitem["GH_DRXS"] = "不合格";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }
                        }

                    }
                    else
                    {
                        sign = IsNumeric(mitem["W_DRXS"]) && !string.IsNullOrEmpty(mitem["W_DRXS"]) ? sign : false;
                        if (sign)
                        {
                            mitem["GH_DRXS"] = IsQualified(mitem["G_DRXS"], mitem["W_DRXS"], false);
                            if (mitem["GH_DRXS"] == "不合格")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }
                        }
                        else
                        {
                            mitem["GH_DRXS"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }

                    }

                }
                else
                {
                    mitem["W_DRXS"] = "----";
                    mitem["G_DRXS"] = "----";
                    mitem["GH_DRXS"] = "----";
                }
                #endregion

                #region 外观
                if (jcxm.Contains("、外观、"))
                {
                    jcxmCur = "外观";
                    mitem["G_WG"] = "喷涂绝热层表面平整，纤维分布均匀，不得有影响使用的分层、孔洞、裂缝等缺陷。";
                    if ("无缺陷" == mitem["W_WG"])
                    {
                        mitem["HG_WG"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mitem["HG_WG"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    mitem["G_WG"] = "----";
                    mitem["HG_WG"] = "----";
                    mitem["W_WG"] = "----";
                }
                #endregion

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

                mbHggs = mitem["GH_KDPC"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_CDPC"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_MDYXPC"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = mitem["GH_HDYXPC"] == "不合格" ? mbHggs + 1 : mbHggs;

                sitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                mitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                mitem["JCJGMS"] = mbHggs == 0 ? "依据" + mitem["PDBZ"] + "的规定，所检项目均符合要求。" : "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //if (mbHggs > 0 && (mitem["GH_MD"] == "合格" ||
                //mitem["GH_DRXS"] == "合格" ||
                //mitem["GH_RSXN"] == "合格" ||
                //mitem["GH_YSQD"] == "合格" ||
                //mitem["GH_XSL"] == "合格" ||
                //mitem["GH_RZ"] == "合格" ||
                //mitem["GH_CDWDX"] == "合格" ||
                //mitem["GH_KDWDX"] == "合格" ||
                //mitem["GH_HDWDX"] == "合格" ||
                //mitem["GH_ZLXSL"] == "合格" ||
                //mitem["GH_DQXSL"] == "合格" ||
                //mitem["GH_CQXSL"] == "合格" ||
                //mitem["GH_KLQD"] == "合格" ||
                //mitem["GH_NJQD"] == "合格" ||
                //mitem["GH_JSBLL"] == "合格" ||
                //mitem["GH_GQPXD"] == "合格" ||
                //mitem["GH_HDPC"] == "合格" ||
                //mitem["GH_KDPC"] == "合格" ||
                //mitem["GH_CDPC"] == "合格" ||
                //mitem["GH_MDYXPC"] == "合格" ||
                //mitem["GH_HDYXPC"] == "合格"
                //))
                //{
                //    mitem["JCJGMS"] = "该样品所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                //}
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");
                //}  }

                /************************ 代码结束 *********************/
            }
            #endregion
        }
    }
}