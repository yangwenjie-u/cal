using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class HSA : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            double[] hnl = new double[2];
            string mcalBh, mlongStr;
            double[] mkyqdArray = new double[3];
            double[] mkyhzArray = new double[3];
            double[] ljsy = new double[7];
            string[] mtmpArray;
            double[] XDMS1 = new double[2];
            string[] jpq = new string[3];
            double mSjcc, mMj, mSjcc1;
            string mgccc;
            double mMaxKyhz, mMinKyhz, mMidKyhz, mAvgKyhz;
            string mSjdjbh, mSjdj;
            double mSz, mQdyq, mHsxs;
            int vp;
            string mjlgs;
            string mMaxBgbh, mkljpq;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            double ZONG, ZONG0, ZONG1, ZONG2;
            double cczl;
            bool mSFwc;
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
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_HSA_DJ"];
            var mrsHS = dataExtra["BZ_HSAHSB"];
            var mrsZbyq = dataExtra["BZ_HSAZBYQ"];
            var MItem = data["M_HSA"];
            var mitem = MItem[0];
            var SItem = data["S_HSA"];
            var jcxmBhg = "";
            var jcxmCur = "";
            #endregion

            #region  计算开始
            double msyzl = GetSafeDouble(mitem["SYZL"]);
            double msyzl1 = GetSafeDouble(mitem["NI_SYZL"]);
            if (string.IsNullOrEmpty(msyzl.ToString()) || msyzl == 0)
                msyzl = 500;
            if (string.IsNullOrEmpty(msyzl1.ToString()) || msyzl1 == 0)
                msyzl1 = 500;
            mAllHg = true;
            //不符合检测项目记录
            var bhgJcxm = "";
            foreach (var sitem in SItem)
            {
                double mbhgs = 0;
                //计算龄期
                double md1, md2, md, pjmd, sum;
                int xd, Gs;
                string which = string.Empty;
                if (mitem["JCYJ"].Contains("2006"))
                    which = "1";
                else
                    which = "2";
                sitem["JPPD"] = "";
                var jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                double mFJSYB1 = 0;
                double mFJSYB2 = 0;
                double mFJSYB3 = 0;
                double mFJSYB4 = 0;
                double mFJSYB5 = 0;
                double mFJSYB6 = 0;
                double mFJSYB7 = 0;
                double mFJSYB1_2 = 0;
                double mFJSYB2_2 = 0;
                double mFJSYB3_2 = 0;
                double mFJSYB4_2 = 0;
                double mFJSYB5_2 = 0;
                double mFJSYB6_2 = 0;
                double mFJSYB7_2 = 0;
                double mljsyb1 = 0;
                double mljsyb2 = 0;
                double mljsyb3 = 0;
                double mljsyb4 = 0;
                double mljsyb5 = 0;
                double mljsyb6 = 0;
                double mljsyb7 = 0;
                double mljsyb1_2 = 0;
                double mljsyb2_2 = 0;
                double mljsyb3_2 = 0;
                double mljsyb4_2 = 0;
                double mljsyb5_2 = 0;
                double mljsyb6_2 = 0;
                double mljsyb7_2 = 0;
                if (jcxm.Contains("、筛分析、"))
                {

                    if (msyzl != 0)
                    {
                        mFJSYB1 = Round(GetSafeDouble(sitem["SYZL1"]) / msyzl * 100, 1);
                        mFJSYB2 = Round(GetSafeDouble(sitem["SYZL2"]) / msyzl * 100, 1);
                        mFJSYB3 = Round(GetSafeDouble(sitem["SYZL3"]) / msyzl * 100, 1);
                        mFJSYB4 = Round(GetSafeDouble(sitem["SYZL4"]) / msyzl * 100, 1);
                        mFJSYB5 = Round(GetSafeDouble(sitem["SYZL5"]) / msyzl * 100, 1);
                        mFJSYB6 = Round(GetSafeDouble(sitem["SYZL6"]) / msyzl * 100, 1);
                        mFJSYB7 = Round(GetSafeDouble(sitem["DPZL"]) / msyzl * 100, 1);
                        mFJSYB1_2 = Round(GetSafeDouble(sitem["SYZL1_2"]) / msyzl * 100, 1);
                        mFJSYB2_2 = Round(GetSafeDouble(sitem["SYZL2_2"]) / msyzl * 100, 1);
                        mFJSYB3_2 = Round(GetSafeDouble(sitem["SYZL3_2"]) / msyzl * 100, 1);
                        mFJSYB4_2 = Round(GetSafeDouble(sitem["SYZL4_2"]) / msyzl * 100, 1);
                        mFJSYB5_2 = Round(GetSafeDouble(sitem["SYZL5_2"]) / msyzl * 100, 1);
                        mFJSYB6_2 = Round(GetSafeDouble(sitem["SYZL6_2"]) / msyzl * 100, 1);
                        mFJSYB7_2 = Round(GetSafeDouble(sitem["DPZL_2"]) / msyzl * 100, 1);
                        sitem["FJSY1_1"] = mFJSYB1.ToString("0.0");
                        sitem["FJSY1_2"] = mFJSYB2.ToString("0.0");
                        sitem["FJSY1_3"] = mFJSYB3.ToString("0.0");
                        sitem["FJSY1_4"] = mFJSYB4.ToString("0.0");
                        sitem["FJSY1_5"] = mFJSYB5.ToString("0.0");
                        sitem["FJSY1_6"] = mFJSYB6.ToString("0.0");
                        sitem["FJSY1_7"] = mFJSYB7.ToString("0.0");
                        sitem["FJSY2_1"] = mFJSYB1_2.ToString("0.0");
                        sitem["FJSY2_2"] = mFJSYB2_2.ToString("0.0");
                        sitem["FJSY2_3"] = mFJSYB3_2.ToString("0.0");
                        sitem["FJSY2_4"] = mFJSYB4_2.ToString("0.0");
                        sitem["FJSY2_5"] = mFJSYB5_2.ToString("0.0");
                        sitem["FJSY2_6"] = mFJSYB6_2.ToString("0.0");
                        sitem["FJSY2_7"] = mFJSYB7_2.ToString("0.0");
                    }
                    mljsyb1 = Round(mFJSYB1, 1);
                    mljsyb2 = Round((mFJSYB1 + mFJSYB2), 1);
                    mljsyb3 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3), 1);
                    mljsyb4 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3 + mFJSYB4), 1);
                    mljsyb5 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3 + mFJSYB4 + mFJSYB5), 1);
                    mljsyb6 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3 + mFJSYB4 + mFJSYB5 + mFJSYB6), 1);
                    mljsyb7 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3 + mFJSYB4 + mFJSYB5 + mFJSYB6 + mFJSYB7), 1);
                    sitem["LJSY1_1"] = mljsyb1.ToString("0.0");
                    sitem["LJSY1_2"] = mljsyb2.ToString("0.0");
                    sitem["LJSY1_3"] = mljsyb3.ToString("0.0");
                    sitem["LJSY1_4"] = mljsyb4.ToString("0.0");
                    sitem["LJSY1_5"] = mljsyb5.ToString("0.0");
                    sitem["LJSY1_6"] = mFJSYB6_2.ToString("0.0");
                    sitem["LJSY1_7"] = mFJSYB7_2.ToString("0.0");
                    mljsyb1_2 = Round((mFJSYB1_2), 1);
                    mljsyb2_2 = Round((mFJSYB1_2 + mFJSYB2_2), 1);
                    mljsyb3_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2), 1);
                    mljsyb4_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2 + mFJSYB4_2), 1);
                    mljsyb5_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2 + mFJSYB4_2 + mFJSYB5_2), 1);
                    mljsyb6_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2 + mFJSYB4_2 + mFJSYB5_2 + mFJSYB6_2), 1);
                    mljsyb7_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2 + mFJSYB4_2 + mFJSYB5_2 + mFJSYB6_2 + mFJSYB7_2), 1);
                    sitem["LJSY2_1"] = mljsyb1_2.ToString("0.0");
                    sitem["LJSY2_2"] = mljsyb2_2.ToString("0.0");
                    sitem["LJSY2_3"] = mljsyb3_2.ToString("0.0");
                    sitem["LJSY2_4"] = mljsyb4_2.ToString("0.0");
                    sitem["LJSY2_5"] = mljsyb5_2.ToString("0.0");
                    sitem["LJSY2_6"] = mljsyb6_2.ToString("0.0");
                    sitem["LJSY2_7"] = mljsyb7_2.ToString("0.0");
                    //最终筛余
                    sitem["LJSYB1_PJ"] = Round((mljsyb1 + mljsyb1_2) / 2, 0).ToString("0");
                    sitem["LJSYB2_PJ"] = Round((mljsyb2 + mljsyb2_2) / 2, 0).ToString("0");
                    sitem["LJSYB3_PJ"] = Round((mljsyb3 + mljsyb3_2) / 2, 0).ToString("0");
                    sitem["LJSYB4_PJ"] = Round((mljsyb4 + mljsyb4_2) / 2, 0).ToString("0");
                    sitem["LJSYB5_PJ"] = Round((mljsyb5 + mljsyb5_2) / 2, 0).ToString("0");
                    sitem["LJSYB6_PJ"] = Round((mljsyb6 + mljsyb6_2) / 2, 0).ToString("0");
                    sitem["LJSYB7_PJ"] = Round((mljsyb7 + mljsyb7_2) / 2, 0).ToString("0");
                    //判断累计筛余率是否满足标准
                    foreach (var mrsHS_item in mrsHS)
                    {
                        double mSKCC1 = GetSafeDouble(mrsHS_item["MSKCC1"]);
                        double mSKCC2 = GetSafeDouble(mrsHS_item["MSKCC2"]);
                        double mSKCC3 = GetSafeDouble(mrsHS_item["MSKCC3"]);
                        double mSKCC4 = GetSafeDouble(mrsHS_item["MSKCC4"]);
                        double mSKCC5 = GetSafeDouble(mrsHS_item["MSKCC5"]);
                        double mSKCC6 = GetSafeDouble(mrsHS_item["MSKCC6"]);
                        double nSKCC1 = GetSafeDouble(mrsHS_item["NSKCC1"]);
                        double nskcc2 = GetSafeDouble(mrsHS_item["NSKCC2"]);
                        double nskcc3 = GetSafeDouble(mrsHS_item["NSKCC3"]);
                        double nSKCC4 = GetSafeDouble(mrsHS_item["NSKCC4"]);
                        double nskcc5 = GetSafeDouble(mrsHS_item["NSKCC5"]);
                        double nskcc6 = GetSafeDouble(mrsHS_item["NSKCC6"]);
                        cczl = 0;
                        bool jpq_hg = true;
                        if (GetSafeDouble(sitem["LJSYB1_PJ"]) > mSKCC1 || GetSafeDouble(sitem["LJSYB1_PJ"]) < nSKCC1)
                            jpq_hg = false;
                        if (GetSafeDouble(sitem["LJSYB4_PJ"]) > mSKCC4 || GetSafeDouble(sitem["LJSYB4_PJ"]) < nSKCC4)
                            jpq_hg = false;
                        if (GetSafeDouble(sitem["LJSYB2_PJ"]) > mSKCC2)
                            cczl = cczl + GetSafeDouble(sitem["LJSYB2_PJ"]) - mSKCC2;
                        if (GetSafeDouble(sitem["LJSYB2_PJ"]) < nskcc2)
                            cczl = cczl + nskcc2 - GetSafeDouble(sitem["LJSYB2_PJ"]);
                        if (GetSafeDouble(sitem["LJSYB3_PJ"]) > mSKCC3)
                            cczl = cczl + GetSafeDouble(sitem["LJSYB3_PJ"]) - mSKCC3;
                        if (GetSafeDouble(sitem["LJSYB3_PJ"]) < nskcc3)
                            cczl = cczl + nskcc3 - GetSafeDouble(sitem["LJSYB3_PJ"]);
                        if (GetSafeDouble(sitem["LJSYB5_PJ"]) > mSKCC5)
                            cczl = cczl + GetSafeDouble(sitem["LJSYB5_PJ"]) - mSKCC5;
                        if (GetSafeDouble(sitem["LJSYB5_PJ"]) < nskcc5)
                            cczl = cczl + nskcc5 - GetSafeDouble(sitem["LJSYB5_PJ"]);
                        if (GetSafeDouble(sitem["LJSYB6_PJ"]) > mSKCC6)
                            cczl = cczl + GetSafeDouble(sitem["LJSYB6_PJ"]) - mSKCC6;
                        if (GetSafeDouble(sitem["LJSYB6_PJ"]) < nskcc6)
                            cczl = cczl + nskcc6 - GetSafeDouble(sitem["LJSYB6_PJ"]);
                        if (cczl > 5)
                            jpq_hg = false;
                        if (jpq_hg)
                        {
                            sitem["JPPD"] = mrsHS_item["MC"];
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(sitem["JPPD"]))
                    {
                        sitem["JPPD"] = "不符合级配区";
                    }
                    //判断总和是否大于1
                    if (Math.Abs(100 - GetSafeDouble(sitem["LJSYB7_PJ"])) > 1)
                        sitem["JPPD"] = "累计底盘筛余量与筛分前的试样总量相比，相差不得超过1%。";
                }
                else
                    sitem["JPPD"] = "----";

                //细度模数计算
                if (jcxm.Contains("、筛分析、"))
                {
                    double mxdms1 = 0;
                    double mxdms2 = 0;
                    if ((100 - mljsyb1) != 0 && (100 - mljsyb1_2) != 0)
                    {
                        mxdms1 = Round((((mljsyb2 + mljsyb3 + mljsyb4 + mljsyb5 + mljsyb6) - mljsyb1 * 5) / (100 - mljsyb1)), 2);
                        mxdms2 = Round((((mljsyb2_2 + mljsyb3_2 + mljsyb4_2 + mljsyb5_2 + mljsyb6_2) - mljsyb1_2 * 5) / (100 - mljsyb1_2)), 2);
                        sitem["XDMS1"] = mxdms1.ToString("0.00");
                        sitem["XDMS2"] = mxdms2.ToString("0.00");
                        sitem["XDMS"] = Round(((mxdms1 + mxdms2) / 2), 1).ToString();
                    }
                    if (GetSafeDouble(sitem["XDMS"]) >= 3.1 && GetSafeDouble(sitem["XDMS"]) <= 3.7)
                        sitem["XDMSPD"] = "粗砂";
                    else if (GetSafeDouble(sitem["XDMS"]) >= 2.3 && GetSafeDouble(sitem["XDMS"]) <= 3)
                        sitem["XDMSPD"] = "中砂";
                    else if (GetSafeDouble(sitem["XDMS"]) >= 1.6 && GetSafeDouble(sitem["XDMS"]) <= 2.2)
                        sitem["XDMSPD"] = "细砂";
                    else if (GetSafeDouble(sitem["XDMS"]) >= 0.7 && GetSafeDouble(sitem["XDMS"]) <= 1.5)
                        sitem["XDMSPD"] = "特细砂";
                    else
                    {
                        sitem["XDMSPD"] = "不符合";
                    }

                    if (Math.Abs(mxdms1 - mxdms2) > 0.2)
                    {
                        sitem["XDMSPD"] = "细度模数两试验数据差值大于0.2试验需重做";
                        sitem["XDMSPD"] = "重做试验";
                        sitem["JPPD"] = sitem["JPPD"] + sitem["XDMSPD"];
                    }
                }
                else
                    sitem["XDMSPD"] = "----";
                if (jcxm.Contains("、坚固性、"))
                {
                    jcxmCur = "坚固性";
                    double mfjzlss4 = 0;
                    double mfjzlss3 = 0;
                    double mfjzlss2 = 0;
                    double mfjzlss1 = 0;
                    double masum = 0;
                    double ma1 = 0;
                    double ma2 = 0;
                    double ma3 = 0;
                    double ma4 = 0;
                    if (sitem["XDMSPD"] != "特细砂")
                    {
                        mfjzlss4 = Round(((Conversion.Val(sitem["JGXQM2"])) - (Conversion.Val(sitem["JGXHM2"]))) / (Conversion.Val(sitem["JGXQM2"])) * 100, 1);
                        mfjzlss3 = Round(((Conversion.Val(sitem["JGXQM3"])) - (Conversion.Val(sitem["JGXHM3"]))) / (Conversion.Val(sitem["JGXQM3"])) * 100, 1);
                        mfjzlss2 = Round(((Conversion.Val(sitem["JGXQM4"])) - (Conversion.Val(sitem["JGXHM4"]))) / (Conversion.Val(sitem["JGXQM4"])) * 100, 1);
                        mfjzlss1 = Round(((Conversion.Val(sitem["JGXQM5"])) - (Conversion.Val(sitem["JGXHM5"]))) / (Conversion.Val(sitem["JGXQM5"])) * 100, 1);
                        masum = (Conversion.Val(sitem["JGXQM2"])) + (Conversion.Val(sitem["JGXQM3"])) + (Conversion.Val(sitem["JGXQM4"])) + (Conversion.Val(sitem["JGXQM5"]));
                        if (masum != 0)
                        {
                            ma1 = Round((Conversion.Val(sitem["JGXHM5"])) / masum * 100, 1);
                            ma2 = Round((Conversion.Val(sitem["JGXHM4"])) / masum * 100, 1);
                            ma3 = Round((Conversion.Val(sitem["JGXHM3"])) / masum * 100, 1);
                            ma4 = Round((Conversion.Val(sitem["JGXHM2"])) / masum * 100, 1);
                        }
                        if (ma1 + ma2 + ma3 + ma4 != 0)
                            sitem["JGX"] = Round((ma1 * mfjzlss1 + ma2 * mfjzlss2 + ma3 * mfjzlss3 + ma4 * mfjzlss4) / (ma1 + ma2 + ma3 + ma4), 0).ToString();
                        else
                            sitem["JGX"] = "0";
                    }
                    else
                    {
                        mfjzlss4 = Round(((Conversion.Val(sitem["JGXQM3"])) - (Conversion.Val(sitem["JGXHM3"]))) / (Conversion.Val(sitem["JGXQM3"])) * 100, 1);
                        mfjzlss3 = Round(((Conversion.Val(sitem["JGXQM4"])) - (Conversion.Val(sitem["JGXHM4"]))) / (Conversion.Val(sitem["JGXQM4"])) * 100, 1);
                        mfjzlss2 = Round(((Conversion.Val(sitem["JGXQM5"])) - (Conversion.Val(sitem["JGXHM5"]))) / (Conversion.Val(sitem["JGXQM5"])) * 100, 1);
                        mfjzlss1 = Round(((Conversion.Val(sitem["JGXQM6"])) - (Conversion.Val(sitem["JGXHM6"]))) / (Conversion.Val(sitem["JGXQM6"])) * 100, 1);
                        masum = (Conversion.Val(sitem["JGXQM3"])) + (Conversion.Val(sitem["JGXQM4"])) + (Conversion.Val(sitem["JGXQM5"])) + (Conversion.Val(sitem["JGXQM6"]));
                        if (masum != 0)
                        {
                            ma1 = Round((Conversion.Val(sitem["JGXHM6"])) / masum * 100, 1);
                            ma2 = Round((Conversion.Val(sitem["JGXHM5"])) / masum * 100, 1);
                            ma3 = Round((Conversion.Val(sitem["JGXHM4"])) / masum * 100, 1);
                            ma4 = Round((Conversion.Val(sitem["JGXHM3"])) / masum * 100, 1);
                        }
                        if (ma1 + ma2 + ma3 + ma4 != 0)
                            sitem["JGX"] = Round((ma1 * mfjzlss1 + ma2 * mfjzlss2 + ma3 * mfjzlss3 + ma4 * mfjzlss4) / (ma1 + ma2 + ma3 + ma4), 0).ToString();
                        else
                            sitem["JGX"] = "0";
                    }

                    var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("坚固性")).ToList();
                    foreach (var item in mrsZbyq_where)
                    {
                        if (calc_pd(item["YQ"], sitem["JGX"]) == "符合")
                        {
                            sitem["JGXPD"] = item["DJ"];
                            break;
                        }
                    }
                    if (sitem["JGXPD"] == "")
                    {
                        sitem["JGXPD"] = "不符合";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        bhgJcxm = bhgJcxm + "坚固性,";
                        mbhgs = mbhgs + 1;
                    }
                }
                else
                {
                    sitem["JGX"] = "----";
                    sitem["JGXPD"] = "----";
                }
                mAllHg = mbhgs > 0 ? false : true;
                #region  跳转
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    mbhgs = 0;
                    double[] narr;
                    #region 含泥量
                    if (jcxm.Contains("、含泥量、"))
                    {
                        sitem["HNLPD"] = "";
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("含泥量")).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (calc_pd(item["YQ"], sitem["HNL"]) == "符合")
                            {
                                sitem["HNLPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sitem["HNLPD"]))
                        {
                            sitem["HNLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["HNL"] = "----";
                        sitem["HNLPD"] = "----";
                    }
                    #endregion

                    #region 泥块含量
                    if (jcxm.Contains("、泥块含量、"))
                    {
                        sitem["NKHLPD"] = "";
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("泥块含量")).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (calc_pd(item["YQ"], sitem["NKHL"]) == "符合")
                            {
                                sitem["NKHLPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sitem["NKHLPD"]))
                        {
                            sitem["NKHLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["NKHLPD"] = "----";
                        sitem["NKHL"] = "----";
                    }
                    #endregion

                    #region 堆积密度
                    if (jcxm.Contains("堆积密度、"))
                        sitem["DJMDPD"] = "----";
                    else
                    {
                        sitem["DJMDPD"] = "----";
                        sitem["DJMD"] = "----";
                    }
                    #endregion

                    #region 紧密密度
                    if (jcxm.Contains("、紧密密度"))
                        sitem["JMMDPD"] = "----";
                    else
                    {
                        sitem["JMMDPD"] = "----";
                        sitem["JMMD"] = "----";
                    }
                    #endregion

                    #region 表观密度
                    if (jcxm.Contains("、表观密度、"))
                        sitem["BGMDPD"] = "----";
                    else
                    {
                        sitem["BGMD"] = "----";
                        sitem["BGMDPD"] = "----";
                    }
                    #endregion

                    #region 空隙率
                    if (jcxm.Contains("、空隙率、"))
                        sitem["KXLPD"] = "----";
                    else
                    {
                        sitem["KXLPD"] = "----";
                        sitem["KXL"] = "----";
                    }
                    #endregion

                    #region 氯离子含量
                    if (jcxm.Contains("、氯离子含量、"))
                    {
                        sitem["LLZHLPD"] = "";
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("氯离子含量") && x["SPZ"].Equals(sitem["SYT"].Trim())).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (calc_pd(item["YQ"], sitem["LLZHL"]) == "符合")
                            {
                                sitem["LLZHLPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sitem["LLZHLPD"]))
                        {
                            sitem["LLZHLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["LLZHLPD"] = "----";
                        sitem["LLZHL"] = "----";
                    }
                    #endregion

                    #region 碱活性
                    if (jcxm.Contains("、碱活性、"))
                    {
                        if (Conversion.Val(sitem["JHX"]) < 0.1)
                            sitem["JHXPD"] = "无潜在危害";
                        else
                        {
                            if (Conversion.Val(sitem["JHX"]) > 0.2)
                            {
                                sitem["JHXPD"] = "有潜在危害";
                                mbhgs = mbhgs + 1;
                            }
                            else
                                sitem["JHXPD"] = "需按7.17节进行复试";
                        }
                    }
                    else
                    {
                        sitem["JHX"] = "----";
                        sitem["JHXPD"] = "----";
                    }
                    #endregion

                    #region 吸水率
                    if (jcxm.Contains("、吸水率、"))
                        sitem["XSLPD"] = "----";
                    else
                    {
                        sitem["XSL"] = "----";
                        sitem["XSLPD"] = "----";
                    }
                    #endregion

                    #region 含水率
                    if (jcxm.Contains("、含水率、"))
                        sitem["HSLPD"] = "----";
                    else
                    {
                        sitem["HSLPD"] = "----";
                        sitem["HSL"] = "----";
                    }
                    #endregion

                    #region 贝壳含量
                    if (jcxm.Contains("、贝壳含量、"))
                    {
                        sitem["BKHLPD"] = "";
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("贝壳含量")).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (calc_pd(item["YQ"], sitem["BKHL"]) == "符合")
                            {
                                sitem["BKHLPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sitem["BKHLPD"]))
                        {
                            sitem["BKHLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["BKHL"] = "----";
                        sitem["BKHLPD"] = "----";
                    }
                    #endregion

                    #region 云母含量
                    if (jcxm.Contains("、云母含量、"))
                    {
                        sitem["YMHLPD"] = "";
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("云母含量")).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (calc_pd(item["YQ"], sitem["YMHL"]) == "符合")
                            {
                                sitem["YMHLPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sitem["YMHLPD"]))
                        {
                            sitem["YMHLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["YMHL"] = "----";
                        sitem["YMHLPD"] = "----";
                    }
                    #endregion

                    #region 有机物含量
                    if (jcxm.Contains("、有机物含量、"))
                    {
                        if (sitem["YJWHLPD"].Contains("不"))
                        {
                            sitem["YJWHLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                        sitem["YJWHLPD"] = "----";
                    #endregion

                    #region 轻物质含量
                    if (jcxm.Contains("、轻物质含量、"))
                    {
                        sitem["QWZHLPD"] = "";
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("轻物质含量")).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (calc_pd(item["YQ"], sitem["QWZHL"]) == "符合")
                            {
                                sitem["QWZHLPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sitem["QWZHLPD"]))
                        {
                            sitem["QWZHLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["QWZHL"] = "----";
                        sitem["QWZHLPD"] = "----";
                    }
                    #endregion

                    #region 硫化物和硫酸盐含量
                    if (jcxm.Contains("、硫化物和硫酸盐含量、"))
                    {
                        sitem["SO3PD"] = "";
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("硫化物和硫酸盐含量")).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (calc_pd(item["YQ"], sitem["SO3"]) == "符合")
                            {
                                sitem["SO3PD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (sitem["SO3PD"] == "")
                        {
                            sitem["SO3PD"] = "不符合";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["SO3"] = "----";
                        sitem["SO3PD"] = "----";
                    }
                    #endregion
                    sitem["JCJG"] = mbhgs == 0 && mAllHg ? "合格" : "不合格";
                    mAllHg = mAllHg && sitem["JCJG"].Trim() == "合格";
                }
                #endregion

                #region 不跳转代码
                else
                {
                    double mdjmd1 = 0;
                    double mdjmd2 = 0;
                    double mbgmd1 = 0;
                    double mbgmd2 = 0;
                    #region 含泥量
                    sitem["HNLPD"] = "";
                    if (jcxm.Contains("、含泥量、"))
                    {
                        jcxmCur = "含泥量";
                        double mhnl1 = 0;
                        double mhnl2 = 0;
                        if (GetSafeDouble(sitem["HNLG0"]) != 0 && GetSafeDouble(sitem["HNLG0_2"]) != 0)
                        {
                            //含泥量% = 试验前烘干试样的质量g/试验后烘干试样的质量g
                            mhnl1 = Round((GetSafeDouble(sitem["HNLG0"]) - GetSafeDouble(sitem["HNLG1"])) / GetSafeDouble(sitem["HNLG0"]) * 100, 1);
                            mhnl2 = Round((GetSafeDouble(sitem["HNLG0_2"]) - GetSafeDouble(sitem["HNLG1_2"])) / GetSafeDouble(sitem["HNLG0_2"]) * 100, 1);
                            md = Round((mhnl1 + mhnl2) / 2, 1);
                            sitem["HNL"] = md.ToString("0.0");
                            var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("含泥量")).ToList();
                            foreach (var item in mrsZbyq_where)
                            {
                                if (calc_pd(item["YQ"], sitem["HNL"]) == "符合")
                                {
                                    sitem["HNLPD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (string.IsNullOrEmpty(sitem["HNLPD"]))
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["HNLPD"] = "不符合";
                                mbhgs = mbhgs + 1;
                                bhgJcxm = bhgJcxm + "含泥量,";

                            }
                            if (Math.Abs(mhnl1 - mhnl2) > 0.5)
                            {
                                sitem["HNLPD"] = "两次结果之差超过0.5%需重新取样试验";
                                sitem["HNLPD"] = "重新试验";
                            }

                        }
                    }
                    else
                    {
                        sitem["HNL"] = "----";
                        sitem["HNLPD"] = "----";
                    }
                    #endregion

                    #region 泥块含量
                    sitem["NKHL"] = "0";
                    sitem["NKHLPD"] = "";
                    if (jcxm.Contains("、泥块含量、"))
                    {
                        jcxmCur = "泥块含量";
                        double mnkhl1 = 0;
                        double mnkhl2 = 0;
                        if (GetSafeDouble(sitem["NKHLG1"]) != 0 && GetSafeDouble(sitem["NKHLG1_2"]) != 0)
                        {
                            mnkhl1 = Round((GetSafeDouble(sitem["NKHLG1"]) - GetSafeDouble(sitem["NKHLG2"])) / GetSafeDouble(sitem["NKHLG1"]) * 100, 1);
                            mnkhl2 = Round((GetSafeDouble(sitem["NKHLG1_2"]) - GetSafeDouble(sitem["NKHLG2_2"])) / GetSafeDouble(sitem["NKHLG1_2"]) * 100, 1);
                            md = Round((mnkhl1 + mnkhl2) / 2, 1);
                            sitem["NKHL"] = md.ToString("0.0");
                            var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("泥块含量")).ToList();
                            foreach (var item in mrsZbyq_where)
                            {
                                if (calc_pd(item["YQ"], sitem["NKHL"]) == "符合")
                                {
                                    sitem["NKHLPD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (sitem["NKHLPD"] == "")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["NKHLPD"] = "不符合";
                                bhgJcxm = bhgJcxm + "泥块含量,";
                                mbhgs = mbhgs + 1;
                            }
                        }
                    }
                    else
                    {
                        sitem["NKHLPD"] = "----";
                        sitem["NKHL"] = "----";
                    }
                    #endregion

                    #region 堆积密度
                    if (jcxm.Contains("、堆积密度、"))
                    {

                        //紧密密度|| 堆积密度 = （容量桶和试样总质量m2(kg)-容量桶的质量m1(kg) ）/容量桶体积L  * 1000 精确到10kg/m³
                        if (Conversion.Val(sitem["DJMDV"]) != 0)
                        {
                            mdjmd1 = Round((Conversion.Val(sitem["DJMDG1"]) - Conversion.Val(sitem["DJMDG2"])) / Conversion.Val(sitem["DJMDV"]) * 100, 0) * 10;//标准文档要求精确到10kg/m3
                            mdjmd2 = Round((Conversion.Val(sitem["DJMDG1_2"]) - Conversion.Val(sitem["DJMDG2_2"])) / Conversion.Val(sitem["DJMDV"]) * 100, 0) * 10;
                            sitem["DJMD"] = (Round((mdjmd1 + mdjmd2) / 20, 0) * 10).ToString();
                            sitem["DJMDPD"] = "";
                        }
                    }
                    else
                    {
                        sitem["DJMD"] = "----";
                        sitem["DJMDPD"] = "----";

                    }
                    #endregion

                    #region 紧密密度
                    if (jcxm.Contains("、紧密密度、"))
                    {
                        double mjmmd1 = 0;
                        double mjmmd2 = 0;
                        if ((Conversion.Val(sitem["JMMDV"]) != 0))
                        {
                            mjmmd1 = Round((Conversion.Val(sitem["JMMDG1"]) - Conversion.Val(sitem["JMMDG2"])) / Conversion.Val(sitem["JMMDV"]) * 100, 0) * 10;//标准文档要求精确到10kg/m3
                            mjmmd2 = Round((Conversion.Val(sitem["JMMDG1_2"]) - Conversion.Val(sitem["JMMDG2_2"])) / Conversion.Val(sitem["JMMDV"]) * 100, 0) * 10;
                            sitem["JMMD"] = (Round((mjmmd1 + mjmmd2) / 20, 0) * 10).ToString();
                            sitem["JMMDPD"] = "";
                        }
                    }
                    else
                    {
                        sitem["JMMD"] = "----";
                        sitem["JMMDPD"] = "----";
                    }
                    #endregion

                    #region 表观密度
                    if (jcxm.Contains("、表观密度、"))
                    {
                        if ((Conversion.Val(sitem["BGMDG0"]) + Conversion.Val(sitem["BGMDG2"]) - Conversion.Val(sitem["BGMDG1"])) != 0 && (Conversion.Val(sitem["BGMDG0_2"]) + Conversion.Val(sitem["BGMDG2_2"]) - Conversion.Val(sitem["BGMDG1_2"])) != 0)
                        {
                            //修正系数直接录  表观密度 = （（试样的烘干质量/（试样的烘干质量g + 水及容量瓶总质量g - 试样、水及容量瓶总质量））-修正系数）*1000     精确至10kg/m³  m0
                            mbgmd1 = Round((Conversion.Val(sitem["BGMDG0"]) / (Conversion.Val(sitem["BGMDG0"]) + Conversion.Val(sitem["BGMDG2"]) - Conversion.Val(sitem["BGMDG1"])) - Conversion.Val(sitem["SWXZXS"])) * 100, 0) * 10;
                            mbgmd2 = Round(((Conversion.Val(sitem["BGMDG0_2"])) / (Conversion.Val(sitem["BGMDG0_2"]) + Conversion.Val(sitem["BGMDG2_2"]) - Conversion.Val(sitem["BGMDG1_2"])) - Conversion.Val(sitem["SWXZXS"])) * 100, 0) * 10;
                            md = Round((mbgmd1 + mbgmd2) / 20, 0) * 10;
                            sitem["BGMD"] = md.ToString("0");
                            sitem["BGMDPD"] = "";
                            if (Math.Abs(mbgmd1 - mbgmd2) > 20)
                            {
                                sitem["BGMDPD"] = "两次结果差大于20，须重新取样试验";
                                sitem["BGMDPD"] = "重新试验";
                            }
                        }
                        else
                        {
                            if ((Conversion.Val(sitem["BGMDV2_3"]) - Conversion.Val(sitem["BGMDV1_3"])) != 0 && Conversion.Val(sitem["BGMDV2_4"]) - Conversion.Val(sitem["BGMDV1_4"]) != 0)
                                mbgmd1 = Round(((Conversion.Val(sitem["BGMDG0_3"])) / ((Conversion.Val(sitem["BGMDV2_3"])) - (Conversion.Val(sitem["BGMDV1_3"]))) - (Conversion.Val(sitem["SWXZXS"]))) * 100, 0) * 10;
                            mbgmd2 = Round(((Conversion.Val(sitem["BGMDG0_4"])) / ((Conversion.Val(sitem["BGMDV2_4"])) - (Conversion.Val(sitem["BGMDV1_4"]))) - (Conversion.Val(sitem["SWXZXS"]))) * 100, 0) * 10;
                            md = Round((mbgmd1 + mbgmd2) / 20, 0) * 10;
                            sitem["BGMD"] = md.ToString("0");
                            sitem["BGMDPD"] = "";
                            if (Math.Abs((mbgmd1 - mbgmd2)) > 20)
                            {
                                sitem["BGMDPD"] = "两次结果差大于20，须重新取样试验";
                                sitem["BGMDPD"] = "重新试验";
                            }
                        }
                    }
                    else
                    {
                        sitem["BGMD"] = "----";
                        sitem["BGMDPD"] = "----";
                    }
                    #endregion

                    #region 空隙率
                    if (jcxm.Contains("、空隙率、"))
                    {
                        double mkxl1 = 0;
                        double mkxl2 = 0;
                        sitem["KXLP1"] = mdjmd1.ToString();//堆积密度
                        sitem["KXLP1_2"] = mdjmd2.ToString();
                        sitem["KXLP2"] = mbgmd1.ToString();//表观密度1
                        sitem["KXLP2_2"] = mbgmd2.ToString();
                        if (GetSafeDouble(sitem["KXLP2"]) != 0 && GetSafeDouble(sitem["KXLP2_2"]) != 0)
                        {
                            mkxl1 = (1 - GetSafeDouble(sitem["KXLP1"]) / GetSafeDouble(sitem["KXLP2"])) * 100;
                            mkxl2 = (1 - GetSafeDouble(sitem["KXLP1_2"]) / GetSafeDouble(sitem["KXLP2_2"])) * 100;
                            sitem["KXL"] = Round((mkxl1 + mkxl2) / 2, 0).ToString();
                        }
                        sitem["KXLPD"] = "";
                    }
                    else
                    {
                        sitem["KXL"] = "----";
                        sitem["KXLPD"] = "----";
                    }
                    #endregion

                    #region 氯离子含量
                    sitem["LLZHLPD"] = "";
                    if (jcxm.Contains("、氯离子含量、"))
                    {
                        jcxmCur = "氯离子含量";
                        md = Round((GetSafeDouble(sitem["LLZV"]) - GetSafeDouble(sitem["LLZV0"])) * GetSafeDouble(sitem["LLZC"]) * 35.5 / 500, 3);
                        sitem["LLZHL"] = md.ToString("0.000");
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("氯离子含量") && x["SPZ"].Equals(sitem["SYT"].Trim())).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (calc_pd(item["YQ"], sitem["LLZHL"]) == "符合")
                            {
                                sitem["LLZHLPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (sitem["LLZHLPD"] == "")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["LLZHLPD"] = "不符合";
                            bhgJcxm = bhgJcxm + "氯离子含量,";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["LLZHLPD"] = "----";
                        sitem["LLZHL"] = "----";
                    }
                    #endregion

                    #region 碱活性
                    sitem["JHXPD"] = "";
                    if (jcxm.Contains("、碱活性、"))
                    {
                        jcxmCur = "碱活性";
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("碱活性")).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (calc_pd(item["YQ"], sitem["JHX"]) == "符合")
                            {
                                sitem["JHXPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (sitem["JHXPD"] == "")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["JHXPD"] = "不符合";
                            bhgJcxm = bhgJcxm + "碱活性,";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["JHX"] = "----";
                        sitem["JHXPD"] = "----";
                    }
                    #endregion

                    #region 吸水率
                    sitem["XSLPD"] = "";
                    if (jcxm.Contains("、吸水率、"))
                    {

                        //吸水率% = 500 -（烘干烧杯与试样总质量m2（g） -烧杯质量m1（g））/烘干烧杯与试样总质量m2（g） -烧杯质量m1（g）  *  100%
                        double mxsl1 = 0;
                        double mxsl2 = 0;
                        if (GetSafeDouble(sitem["XSLG2"]) - GetSafeDouble(sitem["XSLG1"]) != 0 && GetSafeDouble(sitem["XSLG2_2"]) - GetSafeDouble(sitem["XSLG1_2"]) != 0)
                        {
                            mxsl1 = Round((500 - (GetSafeDouble(sitem["XSLG2"]) - GetSafeDouble(sitem["XSLG1"]))) / (GetSafeDouble(sitem["XSLG2"]) - GetSafeDouble(sitem["XSLG1"])) * 100, 1);
                            mxsl2 = Round((500 - (GetSafeDouble(sitem["XSLG2_2"]) - GetSafeDouble(sitem["XSLG1_2"]))) / (GetSafeDouble(sitem["XSLG2_2"]) - GetSafeDouble(sitem["XSLG1_2"])) * 100, 1);
                            md = Round((mxsl1 + mxsl2) / 2, 1);
                            sitem["XSL"] = md.ToString("0.0");
                            if (Math.Abs((mxsl1 - mxsl2)) > 0.2)
                            {
                                //sitem["XSLPD"] = "两次结果差大于0.2，须重新试验";
                                sitem["XSLPD"] = "重新试验";
                            }
                        }
                    }
                    else
                    {
                        sitem["XSL"] = "----";
                        sitem["XSLPD"] = "----";
                    }
                    #endregion

                    #region 含水率
                    sitem["HSLPD"] = "";
                    if (jcxm.Contains("、含水率、"))
                    {
                        //含水率% = （（未烘干的试样与容器总质量g（m2）- 烘干后的试样与容器总质量g（m3））  / （烘干后的试样与容器总质量g（m3） - 容器质量g））*100%
                        double mhsl1 = 0;
                        double mhsl2 = 0;
                        if (GetSafeDouble(sitem["HSLG3"]) - GetSafeDouble(sitem["HSLG1"]) != 0 && (GetSafeDouble(sitem["HSLG3_2"]) - GetSafeDouble(sitem["HSLG1_2"])) != 0)
                        {
                            mhsl1 = Round((GetSafeDouble(sitem["HSLG2"]) - GetSafeDouble(sitem["HSLG3"])) / (GetSafeDouble(sitem["HSLG3"]) - GetSafeDouble(sitem["HSLG1"])) * 100, 1);
                            mhsl2 = Round((GetSafeDouble(sitem["HSLG2_2"]) - GetSafeDouble(sitem["HSLG3_2"])) / (GetSafeDouble(sitem["HSLG3_2"]) - GetSafeDouble(sitem["HSLG1_2"])) * 100, 1);
                            md = Round((mhsl1 + mhsl2) / 2, 1);
                            sitem["HSL"] = md.ToString("0.0");
                        }
                        sitem["HSLPD"] = "";
                    }
                    else
                    {
                        sitem["HSLPD"] = "----";
                        sitem["HSL"] = "----";
                    }
                    #endregion

                    #region 贝壳含量
                    sitem["BKHLPD"] = "";
                    if (jcxm.Contains("、贝壳含量、"))
                    {
                        jcxmCur = "贝壳含量";
                        double mbkhl1 = 0;
                        double mbkhl2 = 0;
                        if (GetSafeDouble(sitem["BKHLM1"]) != 0 && GetSafeDouble(sitem["BKHLM1_2"]) != 0)
                        {
                            mbkhl1 = Round((GetSafeDouble(sitem["BKHLM1"]) - GetSafeDouble(sitem["BKHLM2"])) / GetSafeDouble(sitem["BKHLM1"]) * 100 - GetSafeDouble(sitem["HNL"]), 1);
                            mbkhl2 = Round((GetSafeDouble(sitem["BKHLM1_2"]) - GetSafeDouble(sitem["BKHLM2_2"])) / GetSafeDouble(sitem["BKHLM1_2"]) * 100 - GetSafeDouble(sitem["HNL"]), 1);
                            md = Round((mbkhl1 + mbkhl2) / 2, 1);
                            sitem["BKHL"] = md.ToString("0.0");
                            var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("贝壳含量")).ToList();
                            foreach (var item in mrsZbyq_where)
                            {
                                if (calc_pd(item["YQ"], sitem["BKHL"]) == "符合")
                                {
                                    sitem["BKHLPD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (sitem["BKHLPD"] == "")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["BKHLPD"] = "不符合";
                                bhgJcxm = bhgJcxm + "贝壳含量,";
                                mbhgs = mbhgs + 1;
                            }
                            if (Math.Abs((mbkhl1 - mbkhl2)) > 0.5)
                            {
                                sitem["BKHKPD"] = "两次结果差大于0.5%，须重新试验";
                                sitem["BKHKPD"] = "重新试验";

                            }
                        }
                    }
                    else
                    {
                        sitem["BKHL"] = "----";
                        sitem["BKHLPD"] = "----";
                    }
                    #endregion

                    #region 云母含量
                    sitem["YMHLPD"] = "";
                    if (jcxm.Contains("、云母含量、"))
                    {
                        jcxmCur = "云母含量";
                        if (GetSafeDouble(sitem["YMHLM0"]) != 0)
                        {
                            sitem["YMHL"] = Round(GetSafeDouble(sitem["YMHLM"]) / GetSafeDouble(sitem["YMHLM0"]) * 100, 1).ToString();
                            var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("云母含量")).ToList();
                            foreach (var item in mrsZbyq_where)
                            {
                                if (calc_pd(item["YQ"], sitem["YMHL"]) == "符合")
                                {
                                    sitem["YMHLPD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (sitem["YMHLPD"] == "")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["YMHLPD"] = "不符合";
                                bhgJcxm = bhgJcxm + "云母含量,";
                                mbhgs = mbhgs + 1;
                            }
                        }
                    }
                    else
                    {
                        sitem["YMHLPD"] = "----";
                        sitem["YMHL"] = "----";
                    }
                    #endregion

                    #region 有机物含量
                    if (jcxm.Contains("、有机物含量、"))
                    {
                        jcxmCur = "有机物含量";
                        if (sitem["YJWHLPD"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["YJWHLPD"] = "不符合";
                            bhgJcxm = bhgJcxm + "有机物含量,";
                            mbhgs = mbhgs + 1;
                        }
                    }
                    else
                    {
                        sitem["YJWHLPD"] = "----";
                    }
                    #endregion

                    #region 轻物质含量
                    if (jcxm.Contains("、轻物质含量、"))
                    {
                        jcxmCur = "轻物质含量";
                        double mqwzhl1 = 0;
                        double mqwzhl2 = 0;
                        if ((Conversion.Val(sitem["QWZM1_1"])) != 0 && (Conversion.Val(sitem["QWZM1_2"])) != 0)
                        {
                            mqwzhl1 = Round(((Conversion.Val(sitem["QWZM1_1"])) - (Conversion.Val(sitem["QWZM2_1"]))) / (Conversion.Val(sitem["QWZM0_1"])) * 100, 1);
                            mqwzhl2 = Round(((Conversion.Val(sitem["QWZM1_2"])) - (Conversion.Val(sitem["QWZM2_2"]))) / (Conversion.Val(sitem["QWZM0_2"])) * 100, 1);
                            sitem["QWZHL"] = Round((mqwzhl1 + mqwzhl2) / 2, 1).ToString();
                            var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("轻物质含量")).ToList();
                            foreach (var item in mrsZbyq_where)
                            {
                                if (calc_pd(item["YQ"], sitem["QWZHL"]) == "符合")
                                {
                                    sitem["QWZHLPD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (sitem["QWZHLPD"] == "")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["QWZHLPD"] = "不符合";
                                bhgJcxm = bhgJcxm + "轻物质含量,";
                                mbhgs = mbhgs + 1;
                            }
                        }
                    }
                    else
                    {
                        sitem["QWZHL"] = "----";
                        sitem["QWZHLPD"] = "----";
                    }
                    #endregion

                    #region 硫化物和硫酸盐含量
                    if (jcxm.Contains("、硫化物和硫酸盐含量、"))
                    {
                        jcxmCur = "硫化物和硫酸盐含量";
                        double mso31 = 0;
                        double mso32 = 0;
                        if (Conversion.Val(sitem["SO3M_1"]) != 0 && Conversion.Val(sitem["SO3M_2"]) != 0)
                        {
                            mso31 = Round(((Conversion.Val(sitem["SO3M2_1"])) - (Conversion.Val(sitem["SO3M1_1"]))) / (Conversion.Val(sitem["SO3M_1"])) * 0.343 * 100, 2);
                            mso32 = Round(((Conversion.Val(sitem["SO3M2_2"])) - (Conversion.Val(sitem["SO3M1_2"]))) / (Conversion.Val(sitem["SO3M_2"])) * 0.343 * 100, 2);
                            sitem["SO3"] = Round((mso31 + mso32) / 2, 2).ToString();
                            var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("硫化物和硫酸盐含量")).ToList();
                            foreach (var item in mrsZbyq_where)
                            {
                                if (calc_pd(item["YQ"], sitem["SO3"]) == "符合")
                                {
                                    sitem["SO3PD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (sitem["SO3PD"] == "")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["SO3PD"] = "不符合";
                                bhgJcxm = bhgJcxm + "硫化物和硫酸盐含量,";
                                mbhgs = mbhgs + 1;
                            }
                            if (Math.Abs((mso31 - mso32)) > 0.15)
                            {
                                sitem["SO3PD"] = "两次结果差大于0.15%，须重新试验";
                                sitem["SO3PD"] = "重新试验";
                            }

                        }
                    }
                    else
                    {
                        sitem["SO3"] = "----";
                        sitem["SO3PD"] = "----";
                    }
                    #endregion

                    if (mbhgs > 0)
                    {
                        mAllHg = false;
                        sitem["JCJG"] = "不合格";
                    }
                    else
                        sitem["JCJG"] = "合格";
                }
                #endregion
            }
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                //mitem["JCJGMS"] = "该组试样所检项目符合上述标准要求。";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + ",所属项目属于" + SItem[0]["JPPD"] + SItem[0]["XDMSPD"] + ",符合≤25的混凝土用砂。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                //mitem["JCJGMS"] = "该组试样所检项目不符合上述标准要求。";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + ",所属项目属于" + SItem[0]["JPPD"] + SItem[0]["XDMSPD"] + "检测项目" + jcxmBhg.TrimEnd('、') + ",不符合≤25的混凝土用砂。";
                //依据+判定标准，+所属项目属于+#F:s_HSA.JPPD#  +#F:s_HSA.XDMSPD#，符合≤25（强度等级）的混凝土用砂
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
