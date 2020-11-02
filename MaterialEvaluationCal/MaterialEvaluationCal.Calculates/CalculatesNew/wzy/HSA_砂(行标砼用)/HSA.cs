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
            double[] mkyqdArray = new double[3];
            double[] mkyhzArray = new double[3];
            double[] ljsy = new double[7];
            //string[] mtmpArray;
            double[] XDMS1 = new double[2];
            string[] jpq = new string[3];
            bool mAllHg;
            double cczl;
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
            //商砼站用
            var stJcjg = "该批材料经检验，依据" + mitem["PDBZ"] + "标准规定的要求，";
            #endregion

            #region  计算开始
            double msyzl = GetSafeDouble(mitem["SYZL"]);
            double msyzl1 = GetSafeDouble(mitem["NI_SYZL"]);
            if (string.IsNullOrEmpty(msyzl.ToString()) || msyzl == 0)
                msyzl = 500;
            if (string.IsNullOrEmpty(msyzl1.ToString()) || msyzl1 == 0)
                msyzl1 = 500;
            mAllHg = true;

            double fjsy1 = 0;
            double fjsy2 = 0;
            double fjsy3 = 0;
            double fjsy4 = 0;
            //初始化商砼站检测结果描述
            mitem["STZJCJGMS"] = "";
            foreach (var sitem in SItem)
            {
                double mbhgs = 0;
                //计算龄期
                double md;
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
                        //以下四个分计筛余用于压碎值指标计算
                        //2.50mm
                        fjsy1 = Round((GetSafeDouble(sitem["FJSY1_2"]) + GetSafeDouble(sitem["FJSY2_2"])) / 2, 1);
                        //1.25mm
                        fjsy2 = Round((GetSafeDouble(sitem["FJSY1_3"]) + GetSafeDouble(sitem["FJSY2_3"])) / 2, 1);
                        //630um
                        fjsy3 = Round((GetSafeDouble(sitem["FJSY1_4"]) + GetSafeDouble(sitem["FJSY2_4"])) / 2, 1);
                        //315um
                        fjsy4 = Round((GetSafeDouble(sitem["FJSY1_5"]) + GetSafeDouble(sitem["FJSY2_5"])) / 2, 1);
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
                    sitem["LJSY1_6"] = mljsyb6.ToString("0.0");
                    sitem["LJSY1_7"] = mljsyb7.ToString("0.0");
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
                        //商砼
                        stJcjg = stJcjg + "该砂不符合级配区,";
                    }
                    else
                    {
                        //商砼
                        stJcjg = stJcjg + "该砂符合[" + sitem["JPPD"] + "]颗粒级配，";
                    }
                    //判断总和是否大于1
                    if (Math.Abs(100 - GetSafeDouble(sitem["LJSYB7_PJ"])) > 1)
                    {
                        sitem["JPPD"] = "累计底盘筛余量与筛分前的试样总量相比，相差不得超过1%。";
                        //throw new SystemException("累计底盘筛余量与筛分前的试样总量相比，相差不得超过1%。");
                    }
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
                    else
                    {
                        //商砼
                        if ("不符合" != sitem["XDMSPD"])
                        {
                            stJcjg = stJcjg + "判断该砂为" + sitem["XDMSPD"] + "，";
                        }
                        else
                        {
                            stJcjg = stJcjg + "判断该砂不符合任意砂，";
                        }
                    }
                }
                else
                {
                    sitem["XDMSPD"] = "----";
                }

                #region 坚固性
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
                        if (IsQualified(item["YQ"], sitem["JGX"], true) == "符合")
                        {
                            sitem["JGXPD"] = item["DJ"];
                            break;
                        }
                    }

                    if (sitem["JGXPD"] == "" || sitem["JGXPD"] == "不符合")
                    {
                        //商砼
                        stJcjg = stJcjg + "其坚固性不符合要求，";
                        sitem["JGXPD"] = "不符合";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhgs = mbhgs + 1;
                    }
                    else
                    {
                        stJcjg = stJcjg + "其坚固性符合要求，";
                    }
                }
                else
                {
                    sitem["JGX"] = "----";
                    sitem["JGXPD"] = "----";
                }
                #endregion

                mAllHg = mbhgs > 0 ? false : true;

                #region  跳转
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    mbhgs = 0;
                    // double[] narr;
                    #region 含泥量
                    if (jcxm.Contains("、含泥量、"))
                    {
                        sitem["HNLPD"] = "";
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("含泥量")).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            if (IsQualified(item["YQ"], sitem["HNL"], true) == "符合")
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
                            if (IsQualified(item["YQ"], sitem["NKHL"], true) == "符合")
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
                            if (IsQualified(item["YQ"], sitem["LLZHL"], true) == "符合")
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
                            if (IsQualified(item["YQ"], sitem["BKHL"], true) == "符合")
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
                            if (IsQualified(item["YQ"], sitem["YMHL"], true) == "符合")
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
                            if (IsQualified(item["YQ"], sitem["QWZHL"], true) == "符合")
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
                            if (IsQualified(item["YQ"], sitem["SO3"], true) == "符合")
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
                            sitem["HNL1"] = mhnl1.ToString("0.0");
                            sitem["HNL2"] = mhnl2.ToString("0.0");
                            md = Round((GetSafeDouble(sitem["HNL1"]) + GetSafeDouble(sitem["HNL2"])) / 2, 1);
                            sitem["HNL"] = md.ToString("0.0");
                            var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("含泥量")).ToList();
                            foreach (var item in mrsZbyq_where)
                            {
                                if (IsQualified(item["YQ"], sitem["HNL"], true) == "符合")
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
                                stJcjg = stJcjg + "其含泥量不符合任意等级砼的规定，";
                            }
                            else
                            {
                                //商砼
                                stJcjg = stJcjg + "其含泥量符合[" + sitem["HNLPD"] + "]等级砼的规定，";
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
                        sitem["HNL1"] = "----";
                        sitem["HNL2"] = "----";
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
                            sitem["NKHL1"] = mnkhl1.ToString("0.0");
                            sitem["NKHL2"] = mnkhl2.ToString("0.0");
                            md = Round((GetSafeDouble(sitem["NKHL1"]) + GetSafeDouble(sitem["NKHL2"])) / 2, 1);
                            sitem["NKHL"] = md.ToString("0.0");
                            var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("泥块含量")).ToList();
                            foreach (var item in mrsZbyq_where)
                            {
                                if (IsQualified(item["YQ"], sitem["NKHL"], true) == "符合")
                                {
                                    sitem["NKHLPD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (string.IsNullOrEmpty(sitem["NKHLPD"]))
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["NKHLPD"] = "不符合";
                                mbhgs = mbhgs + 1;
                                stJcjg = stJcjg + "其泥块含量不符合任意等级砼的规定，";
                            }
                            else
                            {
                                stJcjg = stJcjg + "其泥块含量符合[" + sitem["NKHLPD"] + "]等级砼的规定，";
                            }
                        }
                    }
                    else
                    {
                        sitem["NKHL1"] = "----";
                        sitem["NKHL2"] = "----";
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
                            mdjmd1 = Round((Conversion.Val(sitem["DJMDG1"]) - Conversion.Val(sitem["DJMDG2"])) / Conversion.Val(sitem["DJMDV"]) * 100, 0) * 10;//标准文档要求精确到10kg/m³
                            mdjmd2 = Round((Conversion.Val(sitem["DJMDG1_2"]) - Conversion.Val(sitem["DJMDG2_2"])) / Conversion.Val(sitem["DJMDV"]) * 100, 0) * 10;
                            sitem["DJMD1"] = mdjmd1.ToString();
                            sitem["DJMD2"] = mdjmd2.ToString();
                            sitem["DJMD"] = (Round((GetSafeDouble(sitem["DJMD1"]) + GetSafeDouble(sitem["DJMD2"])) / 20, 0) * 10).ToString();
                            sitem["DJMDPD"] = "----";
                        }
                    }
                    else
                    {
                        sitem["DJMD1"] = "----";
                        sitem["DJMD2"] = "----";
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
                            mjmmd1 = Round((Conversion.Val(sitem["JMMDG1"]) - Conversion.Val(sitem["JMMDG2"])) / Conversion.Val(sitem["JMMDV"]) * 100, 0) * 10;//标准文档要求精确到10kg/m³
                            mjmmd2 = Round((Conversion.Val(sitem["JMMDG1_2"]) - Conversion.Val(sitem["JMMDG2_2"])) / Conversion.Val(sitem["JMMDV"]) * 100, 0) * 10;
                            sitem["JMMD1"] = mjmmd1.ToString();
                            sitem["JMMD2"] = mjmmd2.ToString();
                            sitem["JMMD"] = (Round((GetSafeDouble(sitem["JMMD1"]) + GetSafeDouble(sitem["JMMD2"])) / 20, 0) * 10).ToString();
                            sitem["JMMDPD"] = "----";
                        }
                    }
                    else
                    {
                        sitem["JMMD1"] = "----";
                        sitem["JMMD2"] = "----";
                        sitem["JMMD"] = "----";
                        sitem["JMMDPD"] = "----";
                    }
                    #endregion

                    #region 表观密度
                    if (jcxm.Contains("、表观密度、"))
                    {   //标准法
                        if ((Conversion.Val(sitem["BGMDG0"]) + Conversion.Val(sitem["BGMDG2"]) - Conversion.Val(sitem["BGMDG1"])) != 0 && (Conversion.Val(sitem["BGMDG0_2"]) + Conversion.Val(sitem["BGMDG2_2"]) - Conversion.Val(sitem["BGMDG1_2"])) != 0)
                        {
                            sitem["BGMDJCFF"] = "标准法";
                            //修正系数直接录  表观密度 = （（试样的烘干质量/（试样的烘干质量g + 水及容量瓶总质量g - 试样、水及容量瓶总质量））-修正系数）*1000     精确至10kg/m³  m0
                            mbgmd1 = Round((Conversion.Val(sitem["BGMDG0"]) / (Conversion.Val(sitem["BGMDG0"]) + Conversion.Val(sitem["BGMDG2"]) - Conversion.Val(sitem["BGMDG1"])) - Conversion.Val(sitem["SWXZXS"])) * 100, 0) * 10;
                            mbgmd2 = Round(((Conversion.Val(sitem["BGMDG0_2"])) / (Conversion.Val(sitem["BGMDG0_2"]) + Conversion.Val(sitem["BGMDG2_2"]) - Conversion.Val(sitem["BGMDG1_2"])) - Conversion.Val(sitem["SWXZXS"])) * 100, 0) * 10;
                            md = Round((mbgmd1 + mbgmd2) / 20, 0) * 10;
                            sitem["BGMD1"] = mbgmd1.ToString("0");
                            sitem["BGMD2"] = mbgmd2.ToString("0");
                            sitem["BGMD"] = md.ToString("0");
                            sitem["BGMDPD"] = "----";
                            if (Math.Abs(mbgmd1 - mbgmd2) > 20)
                            {
                                sitem["BGMDPD"] = "两次结果差大于20，须重新取样试验";
                                sitem["BGMDPD"] = "重新试验";
                            }
                        }
                        else
                        {
                            //简易法
                            if ((Conversion.Val(sitem["BGMDV2_3"]) - Conversion.Val(sitem["BGMDV1_3"])) != 0 && Conversion.Val(sitem["BGMDV2_4"]) - Conversion.Val(sitem["BGMDV1_4"]) != 0)
                                mbgmd1 = Round(((Conversion.Val(sitem["BGMDG0_3"])) / ((Conversion.Val(sitem["BGMDV2_3"])) - (Conversion.Val(sitem["BGMDV1_3"]))) - (Conversion.Val(sitem["SWXZXS"]))) * 100, 0) * 10;
                            mbgmd2 = Round(((Conversion.Val(sitem["BGMDG0_4"])) / ((Conversion.Val(sitem["BGMDV2_4"])) - (Conversion.Val(sitem["BGMDV1_4"]))) - (Conversion.Val(sitem["SWXZXS"]))) * 100, 0) * 10;
                            md = Round((mbgmd1 + mbgmd2) / 20, 0) * 10;
                            sitem["BGMD1"] = mbgmd1.ToString("0");
                            sitem["BGMD2"] = mbgmd2.ToString("0");
                            sitem["BGMD"] = md.ToString("0");
                            sitem["BGMDPD"] = "----";
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
                        //sitem["KXLPD"] = "";
                        sitem["KXLPD"] = "----";
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
                        /**
                         * 氯离子含量 = [硝酸盐标准溶液浓度C AgNO3  * (样品滴定时消耗的硝酸银标准溶液的体积 V1 - 空白试验时消耗的硝酸银标准溶液的体积) * 0.0355 * 10 ] / 试样质量 m * 100
                         * 精确至0.001%
                         */
                        sitem["LLZC"] = "0.01";
                        md = Round((GetSafeDouble(sitem["LLZV"]) - GetSafeDouble(sitem["LLZV0"])) * 0.01 * 35.5 / 500, 3);
                        sitem["LLZHL"] = md.ToString("0.000");
                        var mrsZbyq_where = mrsZbyq.Where(x => x["MC"] == ("氯离子含量") && x["SPZ"] == sitem["SYT"].Trim()).ToList();
                        foreach (var item in mrsZbyq_where)
                        {
                            sitem["LLZHLYQ"] = item["YQ"];
                            if (IsQualified(item["YQ"], sitem["LLZHL"], true) == "符合")
                            {
                                sitem["LLZHLPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(sitem["LLZHLPD"]))
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["LLZHLPD"] = "不符合";
                            mbhgs = mbhgs + 1;
                            stJcjg = stJcjg + "其氯离子含量检测不合格，";
                        }
                        else
                        {
                            //商砼
                            stJcjg = stJcjg + "其氯离子含量检测合格，";
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
                            if (IsQualified(item["YQ"], sitem["JHX"], true) == "符合")
                            {
                                sitem["JHXPD"] = item["DJ"].Trim();
                                break;
                            }
                        }
                        if (sitem["JHXPD"] == "")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["JHXPD"] = "不符合";
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
                            sitem["XSL1"] = mxsl1.ToString("0.0");
                            sitem["XSL2"] = mxsl2.ToString("0.0");
                            md = Round((GetSafeDouble(sitem["XSL1"]) + GetSafeDouble(sitem["XSL2"])) / 2, 1);
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
                        //含水率% = （（未烘干的试样与容器总质量g（m2）- 烘干后的试样与容器总质量g（m3））  / （烘干后的试样与容器总质量g（m3） - 容器质量g））* 100%
                        double mhsl1 = 0;
                        double mhsl2 = 0;
                        if (GetSafeDouble(sitem["HSLG3"]) - GetSafeDouble(sitem["HSLG1"]) != 0 && (GetSafeDouble(sitem["HSLG3_2"]) - GetSafeDouble(sitem["HSLG1_2"])) != 0)
                        {
                            mhsl1 = Round((GetSafeDouble(sitem["HSLG2"]) - GetSafeDouble(sitem["HSLG3"])) / (GetSafeDouble(sitem["HSLG3"]) - GetSafeDouble(sitem["HSLG1"])) * 100, 1);
                            mhsl2 = Round((GetSafeDouble(sitem["HSLG2_2"]) - GetSafeDouble(sitem["HSLG3_2"])) / (GetSafeDouble(sitem["HSLG3_2"]) - GetSafeDouble(sitem["HSLG1_2"])) * 100, 1);
                            sitem["HSL1"] = mhsl1.ToString("0.0");
                            sitem["HSL2"] = mhsl2.ToString("0.0");
                            md = Round((GetSafeDouble(sitem["HSL1"]) + GetSafeDouble(sitem["HSL2"])) / 2, 1);
                            sitem["HSL"] = md.ToString("0.0");
                        }
                        sitem["HSLPD"] = "";
                    }
                    else
                    {
                        sitem["HSL1"] = "----";
                        sitem["HSL2"] = "----";
                        sitem["HSLPD"] = "----";
                        sitem["HSL"] = "----";
                    }
                    #endregion

                    #region 贝壳含量
                    sitem["BKHLPD"] = "";
                    if (jcxm.Contains("、贝壳含量、"))
                    {
                        jcxmCur = "贝壳含量";
                        /**
                         * 盐酸清洗法
                         * 贝壳含量 = （试样总量 m1 - 试样除去贝壳后的质量 m2）/ 试样总量 m1 * 100 - 含泥量
                         * 精确至0.1%
                         */
                        double mbkhl1 = 0;
                        double mbkhl2 = 0;
                        if (GetSafeDouble(sitem["BKHLM1"]) != 0 && GetSafeDouble(sitem["BKHLM1_2"]) != 0)
                        {
                            mbkhl1 = Round((GetSafeDouble(sitem["BKHLM1"]) - GetSafeDouble(sitem["BKHLM2"])) / GetSafeDouble(sitem["BKHLM1"]) * 100 - GetSafeDouble(sitem["HNL"]), 1);
                            mbkhl2 = Round((GetSafeDouble(sitem["BKHLM1_2"]) - GetSafeDouble(sitem["BKHLM2_2"])) / GetSafeDouble(sitem["BKHLM1_2"]) * 100 - GetSafeDouble(sitem["HNL"]), 1);
                            sitem["BKHL1"] = mbkhl1.ToString("0.0");
                            sitem["BKHL2"] = mbkhl2.ToString("0.0");
                            md = Round((mbkhl1 + mbkhl2) / 2, 1);
                            sitem["BKHL"] = md.ToString("0.0");
                            var mrsZbyq_where = mrsZbyq.Where(x => x["MC"].Equals("贝壳含量")).ToList();
                            foreach (var item in mrsZbyq_where)
                            {
                                sitem["BKHLYQ"] = item["YQ"];
                                if (IsQualified(item["YQ"], sitem["BKHL"], true) == "符合")
                                {
                                    sitem["BKHLPD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (string.IsNullOrEmpty(sitem["BKHLPD"]))
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["BKHLPD"] = "不符合";
                                mbhgs = mbhgs + 1;
                                stJcjg = stJcjg + "其贝壳含量不符合任意等级砼的规定，";
                            }
                            else
                            {
                                stJcjg = stJcjg + "其贝壳含量符合[" + sitem["BKHLPD"] + "]等级砼的规定，";
                            }
       
                            if (Math.Abs((mbkhl1 - mbkhl2)) > 0.5)
                            {
                                sitem["BKHLPD"] = "两次结果差大于0.5%，须重新试验";
                                sitem["BKHLPD"] = "重新试验";
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
                                if (IsQualified(item["YQ"], sitem["YMHL"], true) == "符合")
                                {
                                    sitem["YMHLPD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (sitem["YMHLPD"] == "")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["YMHLPD"] = "不符合";
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
                                if (IsQualified(item["YQ"], sitem["QWZHL"], true) == "符合")
                                {
                                    sitem["QWZHLPD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (sitem["QWZHLPD"] == "")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["QWZHLPD"] = "不符合";
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
                                if (IsQualified(item["YQ"], sitem["SO3"], true) == "符合")
                                {
                                    sitem["SO3PD"] = item["DJ"].Trim();
                                    break;
                                }
                            }
                            if (sitem["SO3PD"] == "")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["SO3PD"] = "不符合";
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

                    #region 石粉含量
                    bool sign = true;
                    if (jcxm.Contains("、石粉含量、"))
                    {
                        jcxmCur = "石粉含量";
                        /**
                         * 亚甲蓝试验    
                         * 亚加蓝MB值 = 加入的亚甲蓝溶液总量 / 试样质量 * 10    精确至 0.01    g/kg
                         */

                        if ("标准法" == sitem["SFHLSYFF"])
                        {
                            //亚甲蓝值 MB
                            sitem["YJLZ"] = Round(GetSafeDouble(sitem["YJLRYZL"].Trim()) / GetSafeDouble(sitem["YJLSYZL"].Trim()) * 10, 2).ToString("0.00");

                            //亚甲蓝试验判定   MB值 ＜1.4  石粉为主     MB值 ≥1.4 泥粉为主
                            var mrsZbyqDj = mrsZbyq.FirstOrDefault(x => x["MC"].Equals("石粉含量"));
                            if (Conversion.Val(sitem["YJLZ"]) < 1.4)
                            {
                                sitem["YJLPD"] = "石粉为主";
                                mrsZbyqDj = mrsZbyq.FirstOrDefault(x => x["MC"] == "石粉含量" && x["MB"] == "＜1.4" && x["DJ"] == sitem["NKHLPD"]);
                                if (mrsZbyqDj != null && mrsZbyqDj.Count > 0)
                                {
                                    sitem["SFHL1"] = Round((GetSafeDouble(sitem["SYQHGZL1"].Trim()) - GetSafeDouble(sitem["SYHHGZL1"].Trim())) / GetSafeDouble(sitem["SYQHGZL1"].Trim()) * 100, 1).ToString("0.0");
                                    sitem["SFHL2"] = Round((GetSafeDouble(sitem["SYQHGZL2"].Trim()) - GetSafeDouble(sitem["SYHHGZL2"].Trim())) / GetSafeDouble(sitem["SYQHGZL2"].Trim()) * 100, 1).ToString("0.0");
                                    sitem["SFHLPJZ"] = Round((Conversion.Val(sitem["SFHL1"]) + Conversion.Val(sitem["SFHL2"])) / 2, 1).ToString("0.0");
                                    sitem["SFHLYQ"] = mrsZbyqDj["YQ"];
                                    sitem["SFHLPD"] = IsQualified(sitem["SFHLYQ"], sitem["SFHLPJZ"]);
                                }
                                else
                                {
                                    sitem["SFHL1"] = "----";
                                    sitem["SFHL2"] = "----";
                                    sitem["SFHLPJZ"] = "----";
                                    sitem["SFHLYQ"] = "获取技术指标失败";
                                    sitem["SFHLPD"] = "----";
                                }
                            }
                            else
                            {
                                sitem["YJLPD"] = "泥粉为主";
                                mrsZbyqDj = mrsZbyq.FirstOrDefault(x => x["MC"] == "石粉含量" && x["MB"] == "≥1.4" && x["DJ"] == sitem["NKHLPD"]);
                                if (mrsZbyqDj != null && mrsZbyqDj.Count > 0)
                                {
                                    sitem["SFHL1"] = Round((GetSafeDouble(sitem["SYQHGZL1"].Trim()) - GetSafeDouble(sitem["SYHHGZL1"].Trim())) / GetSafeDouble(sitem["SYQHGZL1"].Trim()) * 100, 1).ToString("0.0");
                                    sitem["SFHL2"] = Round((GetSafeDouble(sitem["SYQHGZL2"].Trim()) - GetSafeDouble(sitem["SYHHGZL2"].Trim())) / GetSafeDouble(sitem["SYQHGZL2"].Trim()) * 100, 1).ToString("0.0");
                                    sitem["SFHLPJZ"] = Round((Conversion.Val(sitem["SFHL1"]) + Conversion.Val(sitem["SFHL2"])) / 2, 1).ToString("0.0");
                                    sitem["SFHLYQ"] = mrsZbyqDj["YQ"];
                                    sitem["SFHLPD"] = IsQualified(sitem["SFHLYQ"], sitem["SFHLPJZ"]);
                                }
                                else
                                {
                                    sitem["SFHL1"] = "----";
                                    sitem["SFHL2"] = "----";
                                    sitem["SFHLPJZ"] = "----";
                                    sitem["SFHLYQ"] = "获取技术指标失败";
                                    sitem["SFHLPD"] = "----";
                                }
                            }

                            if (sitem["SFHLPD"] != "----")
                            {
                                if (Math.Abs(Conversion.Val(sitem["SFHL1"]) - Conversion.Val(sitem["SFHL2"])) > 0.5)
                                {
                                    sitem["SFHLPD"] = "重新试验";
                                    sitem["SFHLPJZ"] = "重新试验";
                                    sitem["SFHLSYBGXS"] = "重新试验";
                                    stJcjg = stJcjg + "其石粉含量检测需重新试验，";
                                }
                                else
                                {
                                    if (sitem["SFHLPD"] == "不合格")
                                    {
                                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                        mbhgs = mbhgs + 1;
                                        stJcjg = stJcjg + "其石粉含量检测不合格，";
                                    }
                                    else
                                    {
                                        stJcjg = stJcjg + "其石粉含量检测合格，";
                                    }
                                }
                            }
                            sitem["SFHLSYBGXS"] = sitem["SFHLPJZ"];
                        }
                        else
                        {
                            sitem["SFHLSYBGXS"] = sitem["SFHLSYGC"];
                            //亚甲蓝快速法
                            if ("出现色晕" == sitem["SFHLSYGC"])
                            {
                                sitem["SFHLPD"] = "合格";
                                stJcjg = stJcjg + "其石粉含量检测合格，";
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhgs = mbhgs + 1;
                                sitem["SFHLPD"] = "不合格";
                                stJcjg = stJcjg + "其石粉含量检测不合格，";
                            }
                        }

                    }
                    else
                    {
                        sitem["SFHLSYBGXS"] = "----";
                        sitem["SFHLPJZ"] = "----";
                        sitem["SFHLYQ"] = "----";
                        sitem["SFHLPD"] = "----";
                    }
                    #endregion

                    #region 压碎值指标   
                    sign = true;
                    if (jcxm.Contains("、压碎值、") || jcxm.Contains("、压碎值指标、"))
                    {
                        var mrsZbyqDj = mrsZbyq.FirstOrDefault(x => x["MC"].Equals("压碎值指标"));
                        if (mrsZbyqDj != null && mrsZbyqDj.Count > 0)
                        {
                            sitem["YSZYQ"] = mrsZbyqDj["YQ"].Trim();
                        }
                        else
                        {
                            sitem["YSZYQ"] = "压碎值指标标准要求获取失败";
                            continue;
                        }
                        //人工砂才做压碎值指标试验
                        jcxmCur = "压碎值指标";
                        for (int i = 1; i < 3; i++)
                        {
                            sign = IsNumeric(sitem["YSZ1_1_" + i]);
                            sign = IsNumeric(sitem["YSZ2_1_" + i]);
                            sign = IsNumeric(sitem["YSZ3_1_" + i]);
                            sign = IsNumeric(sitem["YSZ4_1_" + i]);
                            sign = IsNumeric(sitem["YSZ1_2_" + i]);
                            sign = IsNumeric(sitem["YSZ2_2_" + i]);
                            sign = IsNumeric(sitem["YSZ3_2_" + i]);
                            sign = IsNumeric(sitem["YSZ4_2_" + i]);
                            sign = IsNumeric(sitem["YSZ1_3_" + i]);
                            sign = IsNumeric(sitem["YSZ2_3_" + i]);
                            sign = IsNumeric(sitem["YSZ3_3_" + i]);
                            sign = IsNumeric(sitem["YSZ4_3_" + i]);
                        }
                        if (sign)
                        {
                            //单个压碎值计算   试样质量  - 试样压碎后筛余质量 / 试样质量   *  100%
                            //第一单级 5.00 ~ 2.50
                            sitem["YSZSC1_1_1"] = Round((GetSafeDouble(sitem["YSZ1_1_1"].Trim()) - GetSafeDouble(sitem["YSZ1_1_2"].Trim())) / GetSafeDouble(sitem["YSZ1_1_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZSC1_2_1"] = Round((GetSafeDouble(sitem["YSZ1_2_1"].Trim()) - GetSafeDouble(sitem["YSZ1_2_2"].Trim())) / GetSafeDouble(sitem["YSZ1_2_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZSC1_3_1"] = Round((GetSafeDouble(sitem["YSZ1_3_1"].Trim()) - GetSafeDouble(sitem["YSZ1_3_2"].Trim())) / GetSafeDouble(sitem["YSZ1_3_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZPJ1"] = Round((GetSafeDouble(sitem["YSZSC1_1_1"]) + GetSafeDouble(sitem["YSZSC1_2_1"]) + GetSafeDouble(sitem["YSZSC1_3_1"])) / 3, 1).ToString("0.0");
                            //第二单级 2.50~1.25
                            sitem["YSZSC1_1_2"] = Round((GetSafeDouble(sitem["YSZ2_1_1"].Trim()) - GetSafeDouble(sitem["YSZ2_1_2"].Trim())) / GetSafeDouble(sitem["YSZ2_1_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZSC1_2_2"] = Round((GetSafeDouble(sitem["YSZ2_2_1"].Trim()) - GetSafeDouble(sitem["YSZ2_2_2"].Trim())) / GetSafeDouble(sitem["YSZ2_2_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZSC1_3_2"] = Round((GetSafeDouble(sitem["YSZ2_3_1"].Trim()) - GetSafeDouble(sitem["YSZ2_3_2"].Trim())) / GetSafeDouble(sitem["YSZ2_3_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZPJ2"] = Round((GetSafeDouble(sitem["YSZSC1_1_2"]) + GetSafeDouble(sitem["YSZSC1_2_2"]) + GetSafeDouble(sitem["YSZSC1_3_2"])) / 3, 1).ToString("0.0");
                            //第三单级
                            sitem["YSZSC1_1_3"] = Round((GetSafeDouble(sitem["YSZ3_1_1"].Trim()) - GetSafeDouble(sitem["YSZ3_1_2"].Trim())) / GetSafeDouble(sitem["YSZ3_1_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZSC1_2_3"] = Round((GetSafeDouble(sitem["YSZ3_2_1"].Trim()) - GetSafeDouble(sitem["YSZ3_2_2"].Trim())) / GetSafeDouble(sitem["YSZ3_2_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZSC1_3_3"] = Round((GetSafeDouble(sitem["YSZ3_3_1"].Trim()) - GetSafeDouble(sitem["YSZ3_3_2"].Trim())) / GetSafeDouble(sitem["YSZ3_3_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZPJ3"] = Round((GetSafeDouble(sitem["YSZSC1_1_3"]) + GetSafeDouble(sitem["YSZSC1_2_3"]) + GetSafeDouble(sitem["YSZSC1_3_3"])) / 3, 1).ToString("0.0");
                            //第四单级
                            sitem["YSZSC1_1_4"] = Round((GetSafeDouble(sitem["YSZ4_1_1"].Trim()) - GetSafeDouble(sitem["YSZ4_1_2"].Trim())) / GetSafeDouble(sitem["YSZ4_1_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZSC1_2_4"] = Round((GetSafeDouble(sitem["YSZ4_2_1"].Trim()) - GetSafeDouble(sitem["YSZ4_2_2"].Trim())) / GetSafeDouble(sitem["YSZ4_2_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZSC1_3_4"] = Round((GetSafeDouble(sitem["YSZ4_3_1"].Trim()) - GetSafeDouble(sitem["YSZ4_3_2"].Trim())) / GetSafeDouble(sitem["YSZ4_3_1"].Trim()) * 100, 1).ToString("0.0");
                            sitem["YSZPJ4"] = Round((GetSafeDouble(sitem["YSZSC1_1_4"]) + GetSafeDouble(sitem["YSZSC1_2_4"]) + GetSafeDouble(sitem["YSZSC1_3_4"])) / 3, 1).ToString("0.0");
                            //总压碎值指标 
                            //2.50mm 分计筛余
                            sitem["YSZZZZ"] = Round((fjsy1 * GetSafeDouble(sitem["YSZPJ1"]) + fjsy2 * GetSafeDouble(sitem["YSZPJ2"]) + fjsy3 * GetSafeDouble(sitem["YSZPJ3"]) + fjsy4 * GetSafeDouble(sitem["YSZPJ4"]))
                                / (fjsy1 + fjsy2 + fjsy3 + fjsy4), 1).ToString("0.0");

                            if ("合格" == IsQualified(sitem["YSZYQ"], sitem["YSZZZZ"]))
                            {
                                sitem["YSZPD"] = "合格";
                                stJcjg = stJcjg + "其压碎值指标检测合格，";
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhgs = mbhgs + 1;
                                sitem["YSZPD"] = "不合格";
                                stJcjg = stJcjg + "其压碎值指标检测不合格，";
                            }
                        }
                        else
                        {
                            throw new SystemException("压碎值指标试验数据录入有误");
                        }
                    }
                    else
                    {
                        sitem["YSZPD"] = "----";
                        sitem["YSZZZZ"] = "----";
                        sitem["YSZYQ"] = "----";
                        sitem["YSZPJ1"] = "----";
                        sitem["YSZPJ2"] = "----";
                        sitem["YSZPJ3"] = "----";
                        sitem["YSZPJ4"] = "----";
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
                mitem["STZJCJGMS"] = stJcjg.TrimEnd('，') + "。";
            }
            string dj = "";

            if (!string.IsNullOrEmpty(SItem[0]["NKHLPD"]) && SItem[0]["NKHLPD"] != "----" && SItem[0]["NKHLPD"] != "不符合"
                && !string.IsNullOrEmpty(SItem[0]["HNLPD"]) && SItem[0]["HNLPD"] != "----" && SItem[0]["HNLPD"] != "不符合")
            {
                if (SItem[0]["NKHLPD"] == SItem[0]["HNLPD"])
                {
                    dj = SItem[0]["NKHLPD"];
                }
                else if (SItem[0]["NKHLPD"].Contains("～") && !SItem[0]["HNLPD"].Contains("～"))
                {
                    if (GetSafeDouble(GetNum(SItem[0]["HNLPD"])) > 55)
                    {
                        //dj = SItem[0]["HNLPD"];
                        dj = SItem[0]["NKHLPD"];
                    }
                    else
                    {
                        dj = SItem[0]["HNLPD"];
                        //dj = SItem[0]["NKHLPD"];
                    }
                }
                else if (SItem[0]["HNLPD"].Contains("～") && !SItem[0]["NKHLPD"].Contains("～"))
                {
                    if (GetSafeDouble(GetNum(SItem[0]["NKHLPD"])) > 55)
                    {
                        //dj = SItem[0]["NKHLPD"];
                        dj = SItem[0]["HNLPD"];
                    }
                    else
                    {
                        //dj = SItem[0]["HNLPD"];
                        dj = SItem[0]["NKHLPD"];
                    }
                }
                else if (!SItem[0]["HNLPD"].Contains("～") && !SItem[0]["NKHLPD"].Contains("～"))
                {
                    if (GetSafeDouble(GetNum(SItem[0]["NKHLPD"])) > GetSafeDouble(GetNum(SItem[0]["HNLPD"])))
                    {
                        //dj = SItem[0]["NKHLPD"];
                        dj = SItem[0]["HNLPD"];
                    }
                    else
                    {
                        //dj = SItem[0]["HNLPD"];
                        dj = SItem[0]["NKHLPD"];
                    }
                }
            }
            else if (!string.IsNullOrEmpty(SItem[0]["HNLPD"]) && SItem[0]["HNLPD"] != "----" && SItem[0]["HNLPD"] != "不符合"
                && (string.IsNullOrEmpty(SItem[0]["NKHLPD"]) || SItem[0]["NKHLPD"] == "----" || SItem[0]["NKHLPD"] == "不符合"))
            {
                dj = SItem[0]["HNLPD"];
            }
            else if (string.IsNullOrEmpty(SItem[0]["NKHLPD"]) || SItem[0]["NKHLPD"] == "----" && (string.IsNullOrEmpty(SItem[0]["HNLPD"]) || SItem[0]["HNLPD"] == "----" || SItem[0]["HNLPD"] == "不符合"))
            {
                dj = "不符合级配";
            }

            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                //mitem["JCJGMS"] = "该组试样所检项目符合上述标准要求。";
                if (dj == "不符合级配")
                {
                    mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + ",所检项目属于" + SItem[0]["JPPD"] + SItem[0]["XDMSPD"] + ",不符合级配。";
                }
                else
                {
                    mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + ",所检项目属于" + SItem[0]["JPPD"] + SItem[0]["XDMSPD"] + ",符合" + dj + "的混凝土用砂。";
                }
            }
            else
            {
                mitem["JCJG"] = "不合格";
                //mitem["JCJGMS"] = "该组试样所检项目不符合上述标准要求。";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + ",所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //依据+判定标准，+所属项目属于+#F:s_HSA.JPPD#  +#F:s_HSA.XDMSPD#，符合≤25（强度等级）的混凝土用砂
            }
            #endregion
            /************************ 代码结束 *********************/
        }

        public void CalcGX()
        {
            var data = retData;
            var MItem = data["M_HSA"];
            var mitem = MItem[0];
            var SItem = data["S_HSA"];

            mitem["JCJGMS"] = mitem["STZJCJGMS"];
        }
    }
}
