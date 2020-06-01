using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class WJJ2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_WJJ_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var SItems = data["S_WJJ"];
            var MItem = data["M_WJJ"];
            if (!data.ContainsKey("M_WJJ"))
            {
                data["M_WJJ"] = new List<IDictionary<string, string>>();
            }
            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = "";
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";
            var jcxmCur = "";
            var jcxmBhg = "";
            List<double> mTmpArray = new List<double>();

            var mSjdj = "";
            var mJSFF = "";
            var mbhggs = 0;
            var xd = 0;
            double md1, md2, md, sum, pjmd = 0;
            var mFlag_Bhg = false;
            var mFlag_Hg = false;
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;

            List<double> narr = new List<double>();

            //遍历从表数据
            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mSjdj = sItem["WJJMC"];//设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                {
                    mSjdj = "";
                }

                #region
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj);

                if (null == mrsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }

                MItem[0]["G_XD"] = mrsDj["XD"];
                MItem[0]["G_MD"] = mrsDj["MD"];
                MItem[0]["G_MSL"] = mrsDj["MSL"];
                MItem[0]["G_JSL"] = mrsDj["JSL"];
                MItem[0]["G_GTHL"] = mrsDj["GTHL"];
                MItem[0]["G_CNSJC"] = mrsDj["CNSJC"];
                MItem[0]["G_ZNSJC"] = mrsDj["ZNSJC"];
                MItem[0]["G_TLD"] = mrsDj["TLD"];
                MItem[0]["G_HQLBHL"] = mrsDj["HQLBHL"];
                MItem[0]["G_HQL"] = mrsDj["HQL"];
                MItem[0]["G_KYQD1D"] = mrsDj["KYQDB1D"];
                MItem[0]["G_KYQD3D"] = mrsDj["KYQDB3D"];
                MItem[0]["G_KYQD7D"] = mrsDj["KYQDB7D"];
                MItem[0]["G_KYQD28D"] = mrsDj["KYQDB28D"];
                MItem[0]["G_XDNJX"] = mrsDj["XDNJX"];
                MItem[0]["G_PH"] = mrsDj["PH"];
                MItem[0]["G_LLZHL"] = mrsDj["LLZHL"];
                MItem[0]["G_SSLB"] = mrsDj["SSLB28D"];

                mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].ToLower();
                #endregion
                if (jcxm.Contains("、细度、"))
                {
                    jcxmCur = "细度";
                    if (!IsNumeric(sItem["XDM1_1"]) || !IsNumeric(sItem["XDM1_2"]) || !IsNumeric(sItem["XDM0_1"]) || !IsNumeric(sItem["XDM0_2"]))
                    {
                        throw new Exception("请输入细度数据");
                    }
                    sItem["XD_1"] = Round(Conversion.Val(sItem["XDM1_1"]) / Conversion.Val(sItem["XDM0_1"]) * 100, 2).ToString();
                    sItem["XD_2"] = Round(Conversion.Val(sItem["XDM1_2"]) / Conversion.Val(sItem["XDM0_2"]) * 100, 2).ToString();

                    sItem["XD"] = Round((Conversion.Val(sItem["XD_1"]) + Conversion.Val(sItem["XD_2"])) / 2, 2).ToString();

                    if (sItem["XDKZZ"] == "----")
                    {
                        MItem[0]["HG_XD"] = "----";
                    }
                    else
                    {
                        MItem[0]["HG_XD"] = IsQualified(sItem["XDKZZ"], sItem["XD"]);
                    }
                    if (MItem[0]["HG_XD"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["XD_1"] = "----";
                    sItem["XD_2"] = "----";
                    sItem["XD"] = "----";
                    MItem[0]["HG_XD"] = "----";
                    MItem[0]["G_XD"] = "----";
                }

                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";

                    #region  //固体密度
                    if (!string.IsNullOrEmpty(sItem["MDBJWJJ_1"]))
                    {
                        if (!IsNumeric(sItem["MDBJWJJ_1"]) || !IsNumeric(sItem["MDBJWJJ_2"]) || !IsNumeric(sItem["MDRLBZ_1"]) || !IsNumeric(sItem["MDRLBZ_2"]) || !IsNumeric(sItem["MDTJ_1"]) || !IsNumeric(sItem["MDTJ_2"]))
                        {
                            throw new Exception("请输入密度数据");
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            sItem["MD_" + i] = Round((Conversion.Val(sItem["MDBJWJJ_" + i]) - Conversion.Val(sItem["MDRLBZ_" + i])) / Conversion.Val(sItem["MDTJ_" + i]) * 100, 3).ToString();
                        }

                        sItem["MD"] = Round((Conversion.Val(sItem["MD_1"]) + Conversion.Val(sItem["MD_2"])) / 2, 3).ToString();
                    }
                    #endregion
                    #region  //液体
                    else
                    {
                        sItem["MD"] = Round((Conversion.Val(sItem["MD2_1"]) + Conversion.Val(sItem["MD2_2"])) / 2, 3).ToString();
                    }
                    #endregion 
                    if (GetSafeDouble(sItem["MDKZZ"]) > 1.1)
                    {
                        MItem[0]["G_MD"] = Round(Conversion.Val(sItem["MDKZZ"]) - 0.03, 3).ToString() + "～" + Round(Conversion.Val(sItem["MDKZZ"]) + 0.03, 3).ToString();

                    }
                    else
                    {
                        MItem[0]["G_MD"] = Round(Conversion.Val(sItem["MDKZZ"]) - 0.02, 3).ToString() + "～" + Round(Conversion.Val(sItem["MDKZZ"]) + 0.02, 3).ToString();
                    }
                    if (sItem["MDKZZ"] == "----")
                    {
                        MItem[0]["HG_MD"] = "----";
                        MItem[0]["G_MD"] = "----";
                    }
                    else
                    {
                        MItem[0]["HG_MD"] = IsQualified(MItem[0]["G_MD"], sItem["MD"]);
                    }
                    if (MItem[0]["HG_MD"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["MD_1"] = "----";
                    sItem["MD_2"] = "----";
                    sItem["MD"] = "----";
                    MItem[0]["HG_MD"] = "----";
                    MItem[0]["G_MD"] = "----";
                }

                if (jcxm.Contains("、固体含量、"))
                {
                    jcxmCur = "固体含量";
                    if (!IsNumeric(sItem["GTHLM2_1"]) || !IsNumeric(sItem["GTHLM2_1"]) || !IsNumeric(sItem["GTHLM0_1"]) || !IsNumeric(sItem["GTHLM0_2"]) || !IsNumeric(sItem["GTHLM1_1"]) || !IsNumeric(sItem["GTHLM1_2"])
                        || !IsNumeric(sItem["GTHLM0_1"]) || !IsNumeric(sItem["GTHLM0_2"]))
                    {
                        throw new Exception("请输入固体含量数据");
                    }
                    for (int i = 1; i < 3; i++)
                    {
                        sItem["GTHL_" + i] = Round((Conversion.Val(sItem["GTHLM2_" + i]) - Conversion.Val(sItem["GTHLM0_" + i])) / (Conversion.Val(sItem["GTHLM1_" + i]) - Conversion.Val(sItem["GTHLM0_" + i])) * 100, 2).ToString();
                    }

                    sItem["GTHL"] = Round((Conversion.Val(sItem["GTHL_1"]) + Conversion.Val(sItem["GTHL_2"])) / 2, 2).ToString();

                    if (GetSafeDouble(sItem["HGLKZZ"]) > 25)
                    {
                        MItem[0]["G_GTHL"] = Round(Conversion.Val(sItem["HGLKZZ"]) * 0.95, 2).ToString() + "～" + Round(Conversion.Val(sItem["HGLKZZ"]) * 1.05, 2).ToString();
                    }
                    else
                    {
                        MItem[0]["G_GTHL"] = Round(Conversion.Val(sItem["HGLKZZ"]) * 0.9, 2).ToString() + "～" + Round(Conversion.Val(sItem["HGLKZZ"]) * 1.1, 2).ToString();
                    }
                    if (sItem["HGLKZZ"] == "----")
                    {
                        MItem[0]["HG_GTHL"] = "----";
                        MItem[0]["G_GTHL"] = "----"; ;
                    }
                    else
                    {
                        MItem[0]["HG_GTHL"] = IsQualified(MItem[0]["G_GTHL"], sItem["GTHL"]);
                    }
                    if (MItem[0]["HG_GTHL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["GTHL_1"] = "----";
                    sItem["GTHL_2"] = "----";
                    sItem["GTHL"] = "----";
                    MItem[0]["G_GTHL"] = "----";
                }

                if (jcxm.Contains("、含水率、"))
                {
                    jcxmCur = "含水率";
                    for (int i = 1; i < 3; i++)
                    {
                        if (!IsNumeric(sItem["GTHLM1_" + i]) || !IsNumeric(sItem["GTHLM2_" + i]) || !IsNumeric(sItem["GTHLM1_" + i]) || !IsNumeric(sItem["GTHLM0_" + i]))
                        {
                            throw new Exception("请输入含水率数据");
                        }
                    }

                    for (int i = 1; i < 3; i++)
                    {
                        sItem["HSL_" + i] = Round((Conversion.Val(sItem["GTHLM1_" + i]) - Conversion.Val(sItem["GTHLM2_" + i])) / (Conversion.Val(sItem["GTHLM1_" + i]) - Conversion.Val(sItem["GTHLM0_" + i])) * 100, 2).ToString();
                    }

                    sItem["HSL"] = Round((Conversion.Val(sItem["HSLL_1"]) + Conversion.Val(sItem["HSL_2"])) / 2, 2).ToString();

                    if (GetSafeDouble(sItem["HSLKZZ"]) > 5)
                    {
                        MItem[0]["G_HSL"] = Round(Conversion.Val(sItem["HSLKZZ"]) * 0.9, 2).ToString() + "～" + Round(Conversion.Val(sItem["HSLKZZ"]) * 1.1, 2).ToString();
                    }
                    else
                    {
                        MItem[0]["G_GTHL"] = Round(Conversion.Val(sItem["HSLKZZ"]) * 0.8, 2).ToString() + "～" + Round(Conversion.Val(sItem["HSLKZZ"]) * 1.1, 2).ToString();
                    }
                    if (sItem["HSLKZZ"] == "----")
                    {
                        MItem[0]["HG_HSL"] = "----";
                        MItem[0]["G_HSL"] = "----"; ;
                    }
                    else
                    {
                        MItem[0]["HG_HSL"] = IsQualified(MItem[0]["G_HSL"], sItem["HSL"]);
                    }
                    if (MItem[0]["HG_HSL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["HSL_1"] = "----";
                    sItem["HSL_2"] = "----";
                    sItem["HSL"] = "----";
                    MItem[0]["G_HSL"] = "----";
                    MItem[0]["HG_HSL"] = "----";
                }

                if (jcxm.Contains("、泌水率、"))
                {
                    jcxmCur = "泌水率";
                    #region 基准混凝土
                    if (!string.IsNullOrEmpty(sItem["PBSN1"]) || sItem["PBSN1"] != "----")
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            if (!IsNumeric(sItem["PBSN" + i]) || !IsNumeric(sItem["PBCHL1" + i]) || !IsNumeric(sItem["PBCHL2" + i]) || !IsNumeric(sItem["PBS" + i]) || !IsNumeric(sItem["PBSA" + i]) || !IsNumeric(sItem["PBSZ" + i]) || !IsNumeric(sItem["JTZL" + i]) || !IsNumeric(sItem["JTSYZL" + i]))
                            {
                                throw new Exception("请输入泌水率数据");
                            }
                        }
                        mTmpArray.Clear();
                        for (int i = 1; i < 4; i++)
                        {
                            //基准拌合物总质量
                            sItem["JPHWZL_" + i] = (Conversion.Val(sItem["PBSN" + i]) + Conversion.Val(sItem["PBCHL1" + i]) + Conversion.Val(sItem["PBCHL2" + i]) + Conversion.Val(sItem["PBS" + i]) + (Conversion.Val(sItem["PBSA" + i]) + Conversion.Val(sItem["PBSZ" + i]))).ToString();
                            //泌水基准拌合物用水量1
                            sItem["MJBYS_" + i] = sItem["PBS" + i];
                            sItem["JSYZL_" + i] = (Conversion.Val(sItem["JTSYZL" + i]) - Conversion.Val(sItem["JTZL" + i])).ToString();
                            if (IsNumeric(sItem["JMSZL_" + i]))
                            {
                                sItem["JMSL_" + i] = Round(Conversion.Val(sItem["JMSZL_" + i]) / ((Conversion.Val(sItem["MJBYS_" + i]) / Conversion.Val(sItem["JPHWZL_" + i])) * Conversion.Val(sItem["JSYZL_" + i])) * 100, 2).ToString();
                                mTmpArray.Add(GetSafeDouble(sItem["JMSL_" + i]));
                            }
                        }
                        mTmpArray.Sort();
                        if (mTmpArray.Count == 3)
                        {
                            mMaxKyqd = mTmpArray[2];
                            mMinKyqd = mTmpArray[0];
                            mMidKyqd = mTmpArray[1];
                            mAvgKyqd = mTmpArray.Average();

                            //计算抗压平均、达到设计强度、及进行单组合格判定
                            if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                MItem[0]["HG_MSL"] = "重做";
                                sItem["JPJMSL"] = "重做";
                            }
                            if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                sItem["JPJMSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                // 最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                sItem["JPJMSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                //最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                sItem["JPJMSL"] = Round(mAvgKyqd, 1).ToString();
                            }
                        }
                        else
                        {
                            MItem[0]["HG_MSL"] = "重做";
                            sItem["JPJMSL"] = "重做";
                        }

                    }
                    #endregion

                    #region 受检配比水泥用量
                    if (!string.IsNullOrEmpty(sItem["SPBSN1"]) || sItem["SPBSN1"] != "----")
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            if (!IsNumeric(sItem["SPBSN" + i]) || !IsNumeric(sItem["SPBCHL1" + i]) || !IsNumeric(sItem["SPBCHL2" + i]) || !IsNumeric(sItem["SPBS" + i]) || !IsNumeric(sItem["SPBSA" + i]) || !IsNumeric(sItem["SPBSZ" + i]) || !IsNumeric(sItem["SPBWJJ1" + i]) || !IsNumeric(sItem["SPBWJJ2" + i])
                                 || !IsNumeric(sItem["STSYZL" + i]) || !IsNumeric(sItem["STZL" + i]))
                            {
                                throw new Exception("请输入泌水率数据");
                            }
                        }
                        mTmpArray.Clear();
                        for (int i = 1; i < 4; i++)
                        {//
                            sItem["SPHWZL_" + i] = (Conversion.Val(sItem["SPBSN" + i]) + Conversion.Val(sItem["SPBCHL1" + i]) + Conversion.Val(sItem["SPBCHL2" + i]) + Conversion.Val(sItem["SPBSA" + i]) + Conversion.Val(sItem["SPBSZ" + i]) + Conversion.Val(sItem["SPBWJJ1" + i]) + Conversion.Val(sItem["SPBWJJ2" + i])).ToString();
                            sItem["MSBYS_" + i] = sItem["SPBS" + i];
                            sItem["SSYZL_" + i] = (Conversion.Val(sItem["STSYZL" + i]) - Conversion.Val(sItem["STZL" + i])).ToString();
                            if (IsNumeric(sItem["SMSZL_" + i]))
                            {

                                sItem["SMSL_" + i] = Round(Conversion.Val(sItem["SMSZL_" + i]) / ((Conversion.Val(sItem["MSBYS_" + i]) / Conversion.Val(sItem["SPHWZL_" + i])) * Conversion.Val(sItem["SSYZL_" + i])) * 100, 2).ToString();
                                mTmpArray.Add(GetSafeDouble(sItem["SMSL_" + i]));
                            }
                        }
                        mTmpArray.Sort();
                        if (mTmpArray.Count == 3)
                        {
                            mMaxKyqd = mTmpArray[2];
                            mMinKyqd = mTmpArray[0];
                            mMidKyqd = mTmpArray[1];
                            mAvgKyqd = mTmpArray.Average();

                            //计算抗压平均、达到设计强度、及进行单组合格判定
                            if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                MItem[0]["HG_MSL"] = "重做";
                                sItem["SPJMSL"] = "重做";
                            }
                            if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                sItem["SPJMSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                // 最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                sItem["SPJMSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                //最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                sItem["SPJMSL"] = Round(mAvgKyqd, 1).ToString();
                            }
                        }
                        else
                        {
                            MItem[0]["HG_MSL"] = "重做";
                            sItem["SPJMSL"] = "重做";
                        }

                    }
                    #endregion

                    if (sItem["JPJMSL"] == "重做" || sItem["SPJMSL"] == "重做")
                    {
                        mbhggs = mbhggs + 1; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                        if (sItem["JPJMSL"] == "重做" && sItem["SPJMSL"] == "重做")
                        {
                            MItem[0]["HG_MSL"] = "基准受检重做";
                        }
                        else
                        {
                            if (sItem["JPJMSL"] == "重做")
                                MItem[0]["HG_MSL"] = "基准重做";
                            else
                                MItem[0]["HG_MSL"] = "受检重做";
                        }
                    }
                    else
                    {
                        if (IsNumeric(sItem["SPJMSL"]))
                        {
                            sItem["MSLB"] = Round(Conversion.Val(sItem["SPJMSL"]) / Conversion.Val(sItem["JPJMSL"]) * 100, 0).ToString();
                        }
                        MItem[0]["HG_MSL"] = IsQualified(MItem[0]["G_MSL"], sItem["MSLB"]);
                    }

                    if (MItem[0]["HG_MSL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    MItem[0]["G_MSL"] = "----";
                    sItem["MSLB"] = "-----";
                    MItem[0]["HG_MSL"] = "----";
                }

                if (jcxm.Contains("、减水率、"))
                {
                    jcxmCur = "减水率";
                    mTmpArray.Clear();
                    if (IsNumeric(sItem["SYBHL"]) && GetSafeDouble(sItem["SYBHL"]) > 0)
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            if (!IsNumeric(sItem["PBS" + i]) || !IsNumeric(sItem["SPBS" + i]))
                            {
                                throw new Exception("请输入减水率数据");
                            }
                        }
                        for (int i = 1; i < 4; i++)
                        {
                            //sItem["JJDYS_" + i] = Round(Conversion.Val(sItem["PBS" + i]) / Conversion.Val(sItem["SYBHL"]) * 1000, 2).ToString();
                            //sItem["JSDYS_" + i] = Round(Conversion.Val(sItem["SPBS" + i]) / Conversion.Val(sItem["SYBHL"]) * 1000, 2).ToString();
                            sItem["JSL_" + i] = Round((Conversion.Val(sItem["JJDYS_" + i]) - Conversion.Val(sItem["JSDYS_" + i])) / Conversion.Val(sItem["JJDYS_" + i]) * 100, 1).ToString();
                            mTmpArray.Add(GetSafeDouble(sItem["JSL_" + i]));
                        }

                        mTmpArray.Sort();
                        if (mTmpArray.Count == 3)
                        {
                            mMaxKyqd = mTmpArray[2];
                            mMinKyqd = mTmpArray[0];
                            mMidKyqd = mTmpArray[1];
                            mAvgKyqd = mTmpArray.Average();

                            //计算抗压平均、达到设计强度、及进行单组合格判定
                            if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                MItem[0]["HG_JSL"] = "重做";
                                sItem["PJJSL"] = "重做";
                            }
                            if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                sItem["PJJSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                // 最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                sItem["PJJSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                //最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                sItem["PJJSL"] = Round(mAvgKyqd, 1).ToString();
                            }
                        }
                        else
                        {
                            MItem[0]["HG_JSL"] = "重做";
                            sItem["PJJSL"] = "重做";
                        }

                        if (sItem["PJJSL"] == "重做")
                        {
                            mbhggs = mbhggs + 1; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        else
                        {
                            MItem[0]["HG_JSL"] = IsQualified(MItem[0]["G_JSL"], sItem["PJJSL"]);
                        }

                        if (MItem[0]["HG_JSL"] == "合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mbhggs = mbhggs + 1;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mFlag_Bhg = true;
                        }
                    }
                }
                else
                {
                    MItem[0]["G_JSL"] = "----";
                    sItem["PJJSL"] = "----";
                    MItem[0]["HG_JSL"] = "----";
                }

                if (jcxm.Contains("、含气量、"))
                {
                    jcxmCur = "含气量";
                    sum = 0;
                    for (xd = 1; xd < 4; xd++)
                    {
                        md1 = Conversion.Val(sItem["BHWHQL_" + xd]);
                        md2 = Conversion.Val(sItem["SSHQL_" + xd]);
                        md = Round(md1 - md2, 1);
                        sum += md;
                    }
                    md = sum / 3;
                    sItem["PJHQL"] = Math.Round(md, 1).ToString();

                    MItem[0]["HG_HQL"] = IsQualified(MItem[0]["G_HQL"], sItem["PJHQL"]);
                    if (MItem[0]["HG_HQL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }

                }
                else
                {
                    MItem[0]["G_HQL"] = "----";
                    sItem["PJHQL"] = "----";
                    MItem[0]["HG_HQL"] = "----";
                }
                if (jcxm.ToLower().Contains("、经1h后含气量变化量、"))
                {
                    jcxmCur = "经1h后含气量变化量";
                    mTmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        if (!IsNumeric(sItem["CJHQL_" + i]))
                        {
                            throw new Exception("请输入经1h后含气量变化量参数");
                        }
                        mTmpArray.Add(GetSafeDouble(sItem["CJHQL_" + i]));
                    }
                    mTmpArray.Sort();
                    if (mTmpArray.Count == 3)
                    {
                        mMaxKyqd = mTmpArray[2];
                        mMinKyqd = mTmpArray[0];
                        mMidKyqd = mTmpArray[1];
                        mAvgKyqd = mTmpArray.Average();

                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_HQLBHL"] = "重做";
                            sItem["CJHQL"] = "重做";
                        }
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["CJHQL"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            // 最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["CJHQL"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sItem["CJHQL"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        MItem[0]["HG_HQLBHL"] = "重做";
                        sItem["CJHQL"] = "重做";
                    }

                    mTmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        if (!IsNumeric(sItem["HQL1HH_" + i]))
                        {
                            throw new Exception("请输入经1h后含气量变化量参数");
                        }
                        mTmpArray.Add(GetSafeDouble(sItem["HQL1HH_" + i]));
                    }
                    mTmpArray.Sort();
                    if (mTmpArray.Count == 3)
                    {
                        mMaxKyqd = mTmpArray[2];
                        mMinKyqd = mTmpArray[0];
                        mMidKyqd = mTmpArray[1];
                        mAvgKyqd = mTmpArray.Average();

                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_HQLBHL"] = "重做";
                            sItem["HQL1HH"] = "重做";
                        }
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["HQL1HH"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            // 最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["HQL1HH"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sItem["HQL1HH"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        MItem[0]["HG_HQLBHL"] = "重做";
                        sItem["HQL1HH"] = "重做";
                    }

                    if (sItem["HQL1HH"] == "重做" || sItem["CJHQL"] == "重做")
                    {
                        mbhggs = mbhggs + 1; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        if (IsNumeric(sItem["CJHQL"]))
                        {
                            sItem["HQLBHL"] = (Round(Conversion.Val(sItem["CJHQL"]) - Conversion.Val(sItem["HQL1HH"]), 1)).ToString();
                            MItem[0]["HG_HQLBHL"] = IsQualified(MItem[0]["G_HQLBHL"], sItem["HQLBHL"]);
                        }
                    }

                    if (MItem[0]["HG_HQLBHL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    MItem[0]["G_HQLBHL"] = "----";
                    sItem["HQLBHL"] = "----";
                    MItem[0]["HG_HQLBHL"] = "----";
                }

                if (jcxm.Contains("、经1h后坍落度变化量、"))
                {
                    jcxmCur = "经1h后坍落度变化量";
                    #region 基准
                    mTmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        if (!IsNumeric(sItem["JJTLD_" + i]) || !IsNumeric(sItem["TLD1HH_" + i]))
                        {
                            throw new Exception("请输入经基准1h后坍落度变化量参数");
                        }
                        sItem["TLDBHL" + i] = Round((Conversion.Val(sItem["JJTLD_" + i]) - Conversion.Val(sItem["TLD1HH_" + i])), 0).ToString();
                        mTmpArray.Add(GetSafeDouble(sItem["TLDBHL" + i]));
                    }

                    mTmpArray.Sort();
                    if (mTmpArray.Count == 3)
                    {
                        mMaxKyqd = mTmpArray[2];
                        mMinKyqd = mTmpArray[0];
                        mMidKyqd = mTmpArray[1];
                        mAvgKyqd = mTmpArray.Average();

                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_TLD"] = "重做";
                            sItem["PJTLDBHL"] = "重做";
                        }
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["PJTLDBHL"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            // 最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["PJTLDBHL"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sItem["PJTLDBHL"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        MItem[0]["HG_TLD"] = "重做";
                        sItem["PJTLDBHL"] = "重做";
                    }

                    #endregion
                    #region 受检
                    mTmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        if (!IsNumeric(sItem["JSTLD_" + i]) || !IsNumeric(sItem["TLD1HHSJ_" + i]))
                        {
                            throw new Exception("请输入经受检1h后坍落度变化量参数");
                        }
                        sItem["TLDBHLSJ_" + i] = Round((Conversion.Val(sItem["JSTLD_" + i]) - Conversion.Val(sItem["TLD1HHSJ_" + i])), 0).ToString();
                        mTmpArray.Add(GetSafeDouble(sItem["TLDBHL" + i]));
                    }

                    mTmpArray.Sort();
                    if (mTmpArray.Count == 3)
                    {
                        mMaxKyqd = mTmpArray[2];
                        mMinKyqd = mTmpArray[0];
                        mMidKyqd = mTmpArray[1];
                        mAvgKyqd = mTmpArray.Average();

                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_TLD"] = "重做";
                            sItem["PJTLDBHLSJ"] = "重做";
                        }
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["PJTLDBHLSJ"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            // 最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["PJTLDBHLSJ"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sItem["PJTLDBHLSJ"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        MItem[0]["HG_TLD"] = "重做";
                        sItem["PJTLDBHLSJ"] = "重做";
                    }

                    #endregion


                    if (sItem["PJTLDBHL"] == "重做" || sItem["PJTLDBHLSJ"] == "重做")
                    {
                        mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        MItem[0]["HG_TLD"] = IsQualified(MItem[0]["G_TLD"], sItem["PJTLDBHL"]);
                        if (MItem[0]["HG_TLD"] == "合格")
                        {
                            MItem[0]["HG_TLD"] = IsQualified(MItem[0]["G_TLD"], sItem["PJTLDBHLSJ"]);

                        }
                    }

                    if (MItem[0]["HG_TLD"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    MItem[0]["G_TLD"] = "----";
                    sItem["PJTLDBHL"] = "----";
                    MItem[0]["HG_TLD"] = "----";
                }

                if (jcxm.Contains("、初凝时间差、"))
                {
                    jcxmCur = "初凝时间差";
                    mTmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        if (!IsNumeric(sItem["CNSJT_" + i]) || !IsNumeric(sItem["CNJZT_" + i]))
                        {
                            throw new Exception("请输入初凝时间差参数");
                        }
                        sItem["CNSJC_" + i] = (Conversion.Val(sItem["CNSJT_" + i]) - Conversion.Val(sItem["CNJZT_" + i])).ToString("0");
                        mTmpArray.Add(GetSafeDouble(sItem["CNSJC_" + i]));
                    }
                    mTmpArray.Sort();
                    if (mTmpArray.Count == 3)
                    {
                        mMaxKyqd = mTmpArray[2];
                        mMinKyqd = mTmpArray[0];
                        mMidKyqd = mTmpArray[1];
                        mAvgKyqd = mTmpArray.Average();

                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_CNSJC"] = "重做";
                            sItem["CNPJSJC"] = "重做";
                        }
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["CNPJSJC"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            // 最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["CNPJSJC"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sItem["CNPJSJC"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        MItem[0]["HG_CNSJC"] = "重做";
                        sItem["CNPJSJC"] = "重做";
                    }

                    if (sItem["CNPJSJC"] == "重做")
                    {
                        mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        MItem[0]["HG_CNSJC"] = IsQualified(MItem[0]["G_CNSJC"], sItem["CNPJSJC"]);
                    }

                    if (MItem[0]["HG_CNSJC"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    MItem[0]["G_CNSJC"] = "----";
                    sItem["CNPJSJC"] = "----";
                    MItem[0]["HG_CNSJC"] = "----";
                }

                if (jcxm.Contains("、终凝时间差、"))
                {
                    jcxmCur = "终凝时间差";
                    mTmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        if (!IsNumeric(sItem["ZNSJT_" + i]) || !IsNumeric(sItem["ZNJZT_" + i]))
                        {
                            throw new Exception("请输入终凝时间差参数");
                        }
                        sItem["ZNSJC_" + i] = (Conversion.Val(sItem["ZNSJT_" + i]) - Conversion.Val(sItem["ZNJZT_" + i])).ToString("0");
                        mTmpArray.Add(GetSafeDouble(sItem["ZNSJC_" + i]));
                    }
                    mTmpArray.Sort();
                    if (mTmpArray.Count == 3)
                    {
                        mMaxKyqd = mTmpArray[2];
                        mMinKyqd = mTmpArray[0];
                        mMidKyqd = mTmpArray[1];
                        mAvgKyqd = mTmpArray.Average();

                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_ZNSJC"] = "重做";
                            sItem["ZNPJSJC"] = "重做";
                        }
                        if (mMaxKyqd - mMidKyqd > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["ZNPJSJC"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            // 最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sItem["ZNPJSJC"] = Round(mMidKyqd, 1).ToString();
                        }
                        if (mMaxKyqd - mMidKyqd <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sItem["ZNPJSJC"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        MItem[0]["HG_ZNSJC"] = "重做";
                        sItem["ZNPJSJC"] = "重做";
                    }

                    if (sItem["ZNPJSJC"] == "重做")
                    {
                        mbhggs = mbhggs + 1; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        MItem[0]["HG_ZNSJC"] = IsQualified(MItem[0]["G_ZNSJC"], sItem["ZNPJSJC"]);
                    }

                    if (MItem[0]["HG_ZNSJC"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    MItem[0]["G_ZNSJC"] = "----";
                    sItem["ZNPJSJC"] = "----";
                    MItem[0]["HG_ZNSJC"] = "----";
                }

                if (jcxm.Contains("、相对耐久性、"))
                {
                    jcxmCur = "相对耐久性";
                    sum = 0;
                    for (xd = 1; xd < 4; xd++)
                    {
                        if (!IsNumeric(sItem["XDRHJP_" + xd]) || !IsNumeric(sItem["XDRHJP_" + xd]) || !IsNumeric(sItem["XJPCZ_" + xd]) || !IsNumeric(sItem["XJPCZ_" + xd]))
                        {
                            sItem["XDTXML_" + xd] = Round((Conversion.Val(sItem["XDRHJP_" + xd]) * Conversion.Val(sItem["XDRHJP_" + xd])) / (Conversion.Val(sItem["XJPCZ_" + xd]) * Conversion.Val(sItem["XJPCZ_2" + xd])) * 100, 1).ToString();
                        }
                    }
                    sItem["XPJDTXML"] = Round((Conversion.Val(sItem["XDTXML_1"]) + Conversion.Val(sItem["XDTXML_1"]) + Conversion.Val(sItem["XDTXML_1"]) / 3), 0).ToString();

                    MItem[0]["HG_XDNJX"] = IsQualified(MItem[0]["G_XDNJX"], sItem["XPJDTXML"]);
                    if (MItem[0]["HG_XDNJX"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["XPJDTXML"] = "----";
                    MItem[0]["HG_XDNJX"] = "----";
                }

                double[] Arrmd = new double[4];
                for (int qdi = 1; qdi <= 4; qdi++)
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

                    int xd1 = 0;
                    int xd2 = 0;
                    int vp = 0;
                    string[] mtmpArray;
                    double[] mkyqdArray = new double[3];
                    if (jcxm.Contains("、" + mlq.ToLower() + "抗压强度比、"))
                    {
                        jcxmCur = mlq.ToLower() + "抗压强度比";
                        mlq = mlq.ToUpper();
                        if (Conversion.Val(sItem["SJCD" + mlq]) == 100)
                            mhsxs = 0.95;
                        if (Conversion.Val(sItem["SJCD" + mlq]) == 150)
                            mhsxs = 1;
                        if (Conversion.Val(sItem["SJCD" + mlq]) == 200)
                            mhsxs = 1.05;

                        if (sItem["JHZ" + mlq + "1_1"] != "" && sItem["JHZ" + mlq + "1_1"] != "----")
                        {
                            for (xd1 = 1; xd1 <= 3; xd1++)
                            {
                                sum = 0;
                                for (xd2 = 1; xd2 <= 3; xd2++)
                                {
                                    md1 = Conversion.Val(sItem["JHZ" + mlq + xd1 + "_" + xd2]);
                                    md2 = Round(1000 * md1 / (Conversion.Val(sItem["SJCD" + mlq]) * Conversion.Val(sItem["SJKD" + mlq])), 1);
                                    Arrmd[xd2] = md2;
                                    sum = sum + Arrmd[xd2];
                                    sItem["JQD" + mlq + xd1 + "_" + xd2] = md2.ToString("0.0");
                                }
                                string mlongStr = Arrmd[1] + "," + Arrmd[2] + "," + Arrmd[3];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                mMaxKyqd = mkyqdArray[2];
                                mMinKyqd = mkyqdArray[0];
                                mMidKyqd = mkyqdArray[1];
                                mAvgKyqd = mkyqdArray.Average();
                                MItem[0]["JCJGMS"] = "";
                                //计算抗压平均、达到设计强度、及进行单组合格判定

                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sItem["JQDDBZ" + mlq + xd1] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sItem["JQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sItem["JQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sItem["JQDDBZ" + mlq + xd1] = Round(mAvgKyqd * mhsxs, 1).ToString("0.0");
                            }
                        }
                        if (sItem["SHZ" + mlq + "1_1"] != "" && sItem["SHZ" + mlq + "1_1"] != "----")
                        {
                            for (xd1 = 1; xd1 <= 3; xd1++)
                            {
                                sum = 0;
                                for (xd2 = 1; xd2 <= 3; xd2++)
                                {
                                    md1 = Conversion.Val(sItem["SHZ" + mlq + xd1 + "_" + xd2]);
                                    md2 = Round(1000 * md1 / (Conversion.Val(sItem["SJCD" + mlq]) * Conversion.Val(sItem["SJKD" + mlq])), 1);
                                    Arrmd[xd2] = md2;
                                    sum = sum + Arrmd[xd2];
                                    sItem["SQD" + mlq + xd1 + "_" + xd2] = md2.ToString("0.0");
                                }
                                string mlongStr = Arrmd[1] + "," + Arrmd[2] + "," + Arrmd[3];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                mMaxKyqd = mkyqdArray[2];
                                mMinKyqd = mkyqdArray[0];
                                mMidKyqd = mkyqdArray[1];
                                mAvgKyqd = mkyqdArray.Average();
                                MItem[0]["JCJGMS"] = "";
                                //计算抗压平均、达到设计强度、及进行单组合格判定

                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sItem["SQDDBZ" + mlq + xd1] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sItem["SQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sItem["SQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sItem["SQDDBZ" + mlq + xd1] = Round(mAvgKyqd * mhsxs, 1).ToString("0.0");
                            }
                        }

                        if (sItem["JQDDBZ" + mlq + "1"] != "" && sItem["JQDDBZ" + mlq + "1"] != "----")
                        {
                            for (xd1 = 1; xd1 <= 3; xd1++)
                            {
                                if (sItem["JQDDBZ" + mlq + xd1] == "重做" || sItem["SQDDBZ" + mlq + xd1] == "重做")
                                    sItem["QDB" + mlq + xd1] = "重做";
                                else
                                    sItem["QDB" + mlq + xd1] = Round(Conversion.Val(sItem["SQDDBZ" + mlq + xd1]) / Conversion.Val(sItem["JQDDBZ" + mlq + xd1]) * 100, 0).ToString();
                            }
                        }
                        if (sItem["QDB" + mlq + "1"] != "" && sItem["QDB" + mlq + "1"] != "----")
                        {
                            if (sItem["QDB" + mlq + "1"] == "重做" || sItem["QDB" + mlq + "2"] == "重做" || sItem["QDB" + mlq + "3"] == "重做")
                                sItem["PJQDB" + mlq] = "重做";
                            else
                            {
                                string mlongStr = "";
                                //基准强度代表值 PJJQDDBZ
                                #region 基准强度代表值
                                mlongStr = sItem["JQDDBZ" + mlq + "1"] + "," + sItem["JQDDBZ" + mlq + "2"] + "," + sItem["JQDDBZ" + mlq + "3"];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                mMaxKyqd = mkyqdArray[2];
                                mMinKyqd = mkyqdArray[0];
                                mMidKyqd = mkyqdArray[1];
                                mAvgKyqd = mkyqdArray.Average();
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 0))
                                    sItem["PJJQDDBZ" + mlq] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 0))
                                    sItem["PJJQDDBZ" + mlq] = Round(mMidKyqd, 0).ToString();
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 0))
                                    sItem["PJJQDDBZ" + mlq] = Round(mMidKyqd, 0).ToString();
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 0))
                                    sItem["PJJQDDBZ" + mlq] = Round(mAvgKyqd, 0).ToString();
                                #endregion
                                
                                #region 受检强度代表值
                                mlongStr = sItem["SQDDBZ" + mlq + "1"] + "," + sItem["SQDDBZ" + mlq + "2"] + "," + sItem["SQDDBZ" + mlq + "3"];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                mMaxKyqd = mkyqdArray[2];
                                mMinKyqd = mkyqdArray[0];
                                mMidKyqd = mkyqdArray[1];
                                mAvgKyqd = mkyqdArray.Average();
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 0))
                                    sItem["PJQDDBZ" + mlq] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 0))
                                    sItem["PJQDDBZ" + mlq] = Round(mMidKyqd, 0).ToString();
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 0))
                                    sItem["PJQDDBZ" + mlq] = Round(mMidKyqd, 0).ToString();
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 0))
                                    sItem["PJQDDBZ" + mlq] = Round(mAvgKyqd, 0).ToString();

                                #endregion
                                sItem["PJQDB" + mlq] = Round(GetSafeDouble(sItem["PJQDDBZ" + mlq]) / GetSafeDouble(sItem["PJJQDDBZ" + mlq]) * 100, 0).ToString();

                            }
                        }
                        if (sItem["PJQDB" + mlq] == "重做")
                        {
                            mbhggs = mbhggs + 1;
                            MItem[0]["HG_KYQD" + mlq] = "重做";
                        }
                        else
                            MItem[0]["HG_KYQD" + mlq] = IsQualified(MItem[0]["G_KYQD" + mlq], sItem["PJQDB" + mlq]);


                        if (MItem[0]["HG_KYQD" + mlq] == "合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mbhggs = mbhggs + 1;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        MItem[0]["G_KYQD" + mlq] = "----";
                        sItem["PJQDB" + mlq] = "-----";
                        MItem[0]["HG_KYQD" + mlq] = "----";
                    }
                }

                if (jcxm.Contains("、收缩率比、"))
                {
                    jcxmCur = "收缩率比";
                    for (xd = 1; xd < 4; xd++)
                    {
                        sItem["SSLJ" + xd] = Round((Conversion.Val(sItem["SSLJL0_" + xd]) - Conversion.Val(sItem["SSLJLT_" + xd])) / (Conversion.Val(sItem["SSLJLB_" + xd])) * Math.Pow(10, 6), 1).ToString();
                        sItem["SSLS" + xd] = Round((Conversion.Val(sItem["SSLSL0_" + xd]) - Conversion.Val(sItem["SSLSLT_" + xd])) / (Conversion.Val(sItem["SSLSLB_" + xd])) * Math.Pow(10, 6), 1).ToString();
                        sItem["SSLB" + xd] = Round(100 * Conversion.Val(sItem["SSLS" + xd]) / Conversion.Val(sItem["SSLJ" + xd]), 1).ToString();
                    }
                    sItem["SSLB"] = Round((Conversion.Val(sItem["SSLB1"]) + Conversion.Val(sItem["SSLB2"]) + Conversion.Val(sItem["SSLB3"])) / 3, 0).ToString();
                    MItem[0]["G_SSLB"] = mrsDj["SSLB28D"];

                    MItem[0]["HG_SSLB"] = IsQualified(MItem[0]["G_SSLB"], sItem["SSLB"]);
                    if (MItem[0]["HG_SSLB"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["SSLB"] = "----";
                    MItem[0]["HG_SSLB"] = "----";
                    MItem[0]["G_SSLB"] = "----";
                }

                if (jcxm.Contains("、PH值、"))
                {
                    jcxmCur = "PH值";
                    if (sItem["PHKZZ"] == "----")
                    {
                        MItem[0]["G_PH"] = "----";
                    }
                    else
                    {
                        MItem[0]["HG_PH"] = IsQualified(sItem["PHKZZ"], sItem["PH"]);
                    }
                    MItem[0]["G_PH"] = sItem["PHKZZ"];
                    if (MItem[0]["HG_PH"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }

                }
                else
                {
                    sItem["PH"] = "----";
                    MItem[0]["HG_PH"] = "----";
                    MItem[0]["G_PH"] = "----";
                }

                if (jcxm.Contains("、氯离子含量、"))
                {
                    jcxmCur = "氯离子含量";
                    if (sItem["LLZKZZ"] == "----")
                        MItem[0]["HG_LLZHL"] = "----";
                    else
                        MItem[0]["HG_LLZHL"] = IsQualified(sItem["LLZKZZ"], sItem["LLZHL"]);
                    MItem[0]["G_LLZHL"] = sItem["LLZKZZ"];
                    if (MItem[0]["HG_LLZHL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["LLZHL1"] = "----";
                    sItem["LLZHL2"] = "----";
                    sItem["LLZHL"] = "----";
                    MItem[0]["HG_LLZHL"] = "----";
                }

                if (jcxm.Contains("、总碱量、"))
                {
                    jcxmCur = "总碱量";
                    if (sItem["ZJLKZZ"] == "----")
                    {
                        MItem[0]["G_ZJL"] = "----";
                    }
                    else
                    {
                        MItem[0]["HG_ZJL"] = IsQualified(sItem["ZJLKZZ"], sItem["ZJL"]);
                    }
                    MItem[0]["G_ZJL"] = sItem["ZJLKZZ"];
                    if (MItem[0]["HG_ZJL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["ZJL"] = "----";
                    MItem[0]["HG_ZJL"] = "----";
                    MItem[0]["G_ZJL"] = "----";
                }

                if (jcxm.Contains("、硫酸钠含量、"))
                {
                    jcxmCur = "硫酸钠含量";
                    for (xd = 1; xd < 3; xd++)
                    {
                        sItem["LSNHL_" + xd] = Round((Conversion.Val(sItem["LSNHLM2_" + xd]) - Conversion.Val(sItem["LSNHLM1_" + xd])) / Conversion.Val(sItem["LSNHLM_" + xd]) * 0.6086 * 100, 2).ToString();
                    }
                    sItem["LSNHL"] = Round((Conversion.Val(sItem["LSNHL_1"]) + Conversion.Val(sItem["LSNHL_2"])) / 2, 2).ToString();

                    if (sItem["LSNHLKZZ"] == "----")
                    {
                        MItem[0]["HG_LSNHL"] = "----";
                    }
                    else
                    {
                        MItem[0]["HG_LSNHL"] = IsQualified(sItem["LSNHLKZZ"], sItem["LSNHL"]);
                    }

                    if (MItem[0]["HG_LSNHL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["LSNHL_1"] = "----";
                    sItem["LSNHL_2"] = "----";
                    sItem["LSNHL"] = "----";
                    MItem[0]["HG_LSNHL"] = "----";
                }

                if (mbhggs == 0)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                mAllHg = (mAllHg && sItem["JCJG"] == "合格");
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }

            //主表总判断赋值
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
                //MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "标准，所检项目不符合标准要求。";
                //if (mFlag_Bhg && mFlag_Hg)
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }

            #endregion
            #endregion
            /************************ 代码结束 *********************/

        }
    }
}