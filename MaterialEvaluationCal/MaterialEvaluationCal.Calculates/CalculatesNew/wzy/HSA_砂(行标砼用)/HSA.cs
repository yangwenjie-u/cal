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
            #endregion

            #region  计算开始
            double msyzl = GetSafeDouble(mitem["SYZL"]);
            double msyzl1 = GetSafeDouble(mitem["NI_SYZL"]);
            if (string.IsNullOrEmpty(msyzl.ToString()) || msyzl == 0)
                msyzl = 500;
            if (string.IsNullOrEmpty(msyzl1.ToString()) || msyzl1 == 0)
                msyzl1 = 500;
            mAllHg = true;
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
                if (jcxm.Contains("、级配、"))
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
                    }
                    mljsyb1 = Round(mFJSYB1, 1);
                    mljsyb2 = Round((mFJSYB1 + mFJSYB2), 1);
                    mljsyb3 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3), 1);
                    mljsyb4 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3 + mFJSYB4), 1);
                    mljsyb5 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3 + mFJSYB4 + mFJSYB5), 1);
                    mljsyb6 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3 + mFJSYB4 + mFJSYB5 + mFJSYB6), 1);
                    mljsyb7 = Round((mFJSYB1 + mFJSYB2 + mFJSYB3 + mFJSYB4 + mFJSYB5 + mFJSYB6 + mFJSYB7), 1);
                    mljsyb1_2 = Round((mFJSYB1_2), 1);
                    mljsyb2_2 = Round((mFJSYB1_2 + mFJSYB2_2), 1);
                    mljsyb3_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2), 1);
                    mljsyb4_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2 + mFJSYB4_2), 1);
                    mljsyb5_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2 + mFJSYB4_2 + mFJSYB5_2), 1);
                    mljsyb6_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2 + mFJSYB4_2 + mFJSYB5_2 + mFJSYB6_2), 1);
                    mljsyb7_2 = Round((mFJSYB1_2 + mFJSYB2_2 + mFJSYB3_2 + mFJSYB4_2 + mFJSYB5_2 + mFJSYB6_2 + mFJSYB7_2), 1);
                    sitem["LJSYB1_PJ"] = Round((mljsyb1 + mljsyb1_2) / 2, 0).ToString();
                    sitem["LJSYB2_PJ"] = Round((mljsyb2 + mljsyb2_2) / 2, 0).ToString();
                    sitem["LJSYB3_PJ"] = Round((mljsyb3 + mljsyb3_2) / 2, 0).ToString();
                    sitem["LJSYB4_PJ"] = Round((mljsyb4 + mljsyb4_2) / 2, 0).ToString();
                    sitem["LJSYB5_PJ"] = Round((mljsyb5 + mljsyb5_2) / 2, 0).ToString();
                    sitem["LJSYB6_PJ"] = Round((mljsyb6 + mljsyb6_2) / 2, 0).ToString();
                    sitem["LJSYB7_PJ"] = Round((mljsyb7 + mljsyb7_2) / 2, 0).ToString();
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
                        mbhgs = mbhgs + 1;
                    }
                    //判断总和是否大于1
                    if (Math.Abs(100 - GetSafeDouble(sitem["LJSYB7_PJ"])) > 1)
                        sitem["JPPD"] = "累计底盘筛余量与筛分前的试样总量相比，相差不得超过1%。";
                }
                else
                    sitem["JPPD"] = "----";

                //细度模数计算
                if (jcxm.Contains("、级配、"))
                {
                    double mxdms1 = 0;
                    double mxdms2 = 0;
                    if ((100 - mljsyb1) != 0 && (100 - mljsyb1_2) != 0)
                    {
                        mxdms1 = Round((((mljsyb2 + mljsyb3 + mljsyb4 + mljsyb5 + mljsyb6) - mljsyb1 * 5) / (100 - mljsyb1)), 2);
                        mxdms2 = Round((((mljsyb2_2 + mljsyb3_2 + mljsyb4_2 + mljsyb5_2 + mljsyb6_2) - mljsyb1_2 * 5) / (100 - mljsyb1_2)), 2);
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
                        mbhgs = mbhgs + 1;
                    }


                    if (Math.Abs(mxdms1 - mxdms2) > 0.2)
                    {
                        sitem["XDMSPD"] = "细度模数两试验数据差值大于0.2试验需重做";
                        sitem["JPPD"] = sitem["JPPD"] + sitem["XDMSPD"];
                    }
                }
                else
                    sitem["XDMSPD"] = "----";
                if (jcxm.Contains("、坚固性、"))
                {
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
                        ma1 = Round((Conversion.Val(sitem["JGXHM5"])) / masum * 100, 1);
                        ma2 = Round((Conversion.Val(sitem["JGXHM4"])) / masum * 100, 1);
                        ma3 = Round((Conversion.Val(sitem["JGXHM3"])) / masum * 100, 1);
                        ma4 = Round((Conversion.Val(sitem["JGXHM2"])) / masum * 100, 1);
                        sitem["JGX"] = Round((ma1 * mfjzlss1 + ma2 * mfjzlss2 + ma3 * mfjzlss3 + ma4 * mfjzlss4) / (ma1 + ma2 + ma3 + ma4), 0).ToString();
                    }
                    else
                    {
                        mfjzlss4 = Round(((Conversion.Val(sitem["JGXQM3"])) - (Conversion.Val(sitem["JGXHM3"]))) / (Conversion.Val(sitem["JGXQM3"])) * 100, 1);
                        mfjzlss3 = Round(((Conversion.Val(sitem["JGXQM4"])) - (Conversion.Val(sitem["JGXHM4"]))) / (Conversion.Val(sitem["JGXQM4"])) * 100, 1);
                        mfjzlss2 = Round(((Conversion.Val(sitem["JGXQM5"])) - (Conversion.Val(sitem["JGXHM5"]))) / (Conversion.Val(sitem["JGXQM5"])) * 100, 1);
                        mfjzlss1 = Round(((Conversion.Val(sitem["JGXQM6"])) - (Conversion.Val(sitem["JGXHM6"]))) / (Conversion.Val(sitem["JGXQM6"])) * 100, 1);
                        masum = (Conversion.Val(sitem["JGXQM3"])) + (Conversion.Val(sitem["JGXQM4"])) + (Conversion.Val(sitem["JGXQM5"])) + (Conversion.Val(sitem["JGXQM6"]));
                        ma1 = Round((Conversion.Val(sitem["JGXHM6"])) / masum * 100, 1);
                        ma2 = Round((Conversion.Val(sitem["JGXHM5"])) / masum * 100, 1);
                        ma3 = Round((Conversion.Val(sitem["JGXHM4"])) / masum * 100, 1);
                        ma4 = Round((Conversion.Val(sitem["JGXHM3"])) / masum * 100, 1);
                        sitem["JGX"] = Round((ma1 * mfjzlss1 + ma2 * mfjzlss2 + ma3 * mfjzlss3 + ma4 * mfjzlss4) / (ma1 + ma2 + ma3 + ma4), 0).ToString();
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
                        mbhgs = mbhgs + 1;
                    }
                }
                else
                {
                    sitem["JGX"] = "----";
                    sitem["JGXPD"] = "----";
                }
                mAllHg = mbhgs > 0 ? false : true;
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    mbhgs = 0;
                    double[] narr;
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
                    if (jcxm.Contains("、堆积密度、"))
                        sitem["DJMDPD"] = "----";
                    else
                    {
                        sitem["DJMDPD"] = "----";
                        sitem["DJMD"] = "----";
                    }
                    if (jcxm.Contains("、紧密密度、"))
                        sitem["JMMDPD"] = "----";
                    else
                    {
                        sitem["JMMDPD"] = "----";
                        sitem["JMMD"] = "----";
                    }
                    if (jcxm.Contains("、表观密度、"))
                        sitem["BGMDPD"] = "----";
                    else
                    {
                        sitem["BGMD"] = "----";
                        sitem["BGMDPD"] = "----";
                    }
                    if (jcxm.Contains("、空隙率、"))
                        sitem["KXLPD"] = "----";
                    else
                    {
                        sitem["KXLPD"] = "----";
                        sitem["KXL"] = "----";
                    }
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
                    if (jcxm.Contains("、吸水率、"))
                        sitem["XSLPD"] = "----";
                    else
                    {
                        sitem["XSL"] = "----";
                        sitem["XSLPD"] = "----";
                    }
                    if (jcxm.Contains("、含水率、"))
                        sitem["HSLPD"] = "----";
                    else
                    {
                        sitem["HSLPD"] = "----";
                        sitem["HSL"] = "----";
                    }

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
                    sitem["JCJG"] = mbhgs == 0 && mAllHg ? "合格" : "不合格";
                    mAllHg = mAllHg && sitem["JCJG"].Trim() == "合格";
                }
            }
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "该组试样所检项目符合上述标准要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "该组试样所检项目不符合上述标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
