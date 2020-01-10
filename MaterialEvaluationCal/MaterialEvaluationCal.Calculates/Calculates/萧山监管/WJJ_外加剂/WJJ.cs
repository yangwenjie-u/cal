using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class WJJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            var extraDJData = dataExtra["BZ_WJJ_DJ"];

            //var jcxmItems = retData.Select(u => u.Key).ToArray();
            int forEachFlag = 0;
            int mbhggs = 0;//总报告合格数
            bool mFlag_Hg = false;
            bool mFlag_BHg = false;  //报告不合格
            double md = 0, md1 = 0, md2 = 0, cd1 = 0, cd2 = 0, sum = 0;
            List<double> mtmpList = new List<double>();
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;
            int index = -1;

            bool mAllHg = true;
            var data = retData;

            var S_HNTS = data["S_WJJ"];
            var M_HNTS = data["M_WJJ"];

            //遍历从表数据
            foreach (var sItem in S_HNTS)
            {
                string dfd = sItem["RECID"];

                #region 细度
                if (sItem["JCXM"].Trim().Contains("细度"))
                {
                    if (string.IsNullOrEmpty(sItem["xdm1_1"]) || sItem["xdm1_1"] != "----")
                    {

                    }
                }
                else
                {
                    //sitem["xd_1 = "----"
                    //sitem["xd_2 = "----"
                    //sitem["xd = "----"
                    //mrsmainTable!hg_xd = "----"
                    //mrsmainTable!g_xd = "----"
                }
                #endregion

                #region 密度
                if (sItem["JCXM"].Trim().Contains("密度"))
                {
                    if (string.IsNullOrEmpty(sItem["xdm1_1"]) || sItem["xdm1_1"] != "----")
                    {

                    }
                }
                else
                {
                    //sitem["xd_1 = "----"
                    //sitem["xd_2 = "----"
                    //sitem["xd = "----"
                    //mrsmainTable!hg_xd = "----"
                    //mrsmainTable!g_xd = "----"
                }
                #endregion

            }
            //    foreach (var jcxm in jcxmItems)
            //{
            //    var SItem = retData[jcxm]["S_WJJ"];
            //    var MItem = retData[jcxm]["M_WJJ"];
            //    var XQData = retData[jcxm]["S_BY_RW_XQ"];
            //    index++;

            //    switch (jcxm.Trim())
            //    {
            //        case "细度":
            //            foreach (var sItem in SItem)
            //            {
            //                if (GetSafeDouble(sItem["XDM0_1"]) == 0 || GetSafeDouble(sItem["XDM0_1"]) == 0)
            //                {
            //                    XQData[index]["JCJG"] = "不合格";
            //                    XQData[index]["JCJGMS"] = "获取细度试样质量1失败";
            //                    mbhggs = mbhggs + 1;
            //                    continue;
            //                }
            //                sItem["XD_1"] = (Math.Round(GetSafeDouble(sItem["XDM1_1"]) / GetSafeDouble(sItem["XDM0_1"]) * 100, 2)).ToString("0.00");
            //                sItem["XD_2"] = (Math.Round(GetSafeDouble(sItem["XDM1_2"]) / GetSafeDouble(sItem["xdm0_2"]) * 100, 2)).ToString("0.00");

            //                sItem["XD"] = (Math.Round((GetSafeDouble(sItem["XD_1"]) + GetSafeDouble(sItem["XD_2"])) / 2, 2)).ToString("0.00");

            //                sItem.FirstOrDefault(u => u.Key.Contains("XDKZZ"));
            //                //细度厂家控制范围
            //                if (sItem["XDKZZ"] == "----")
            //                {
            //                    MItem[forEachFlag]["HG_XD"] = "----";
            //                }
            //                else
            //                {
            //                    MItem[forEachFlag]["HG_XD"] = IsQualified(sItem["XDKZZ"], sItem["md"]);
            //                }
            //                MItem[forEachFlag]["G_XD"] = sItem["XDKZZ"];

            //                if (MItem[forEachFlag]["HG_XD"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                }
            //            }
            //            break;
            //        case "密度":
            //            foreach (var sItem in SItem)
            //            {
            //                if (GetSafeDouble(sItem["XDM0_1"]) == 0 || GetSafeDouble(sItem["XDM0_1"]) == 0)
            //                {
            //                    XQData[index]["JCJG"] = "不合格";
            //                    XQData[index]["JCJGMS"] = "获取细度试样质量1失败";
            //                    mbhggs = mbhggs + 1;
            //                    continue;
            //                }
            //                sItem["MD_1"] = (Math.Round(0.9982 * (GetSafeDouble(sItem["MDBJWJJ_1"]) - GetSafeDouble(sItem["MDRLBZ_1"])) / GetSafeDouble(sItem["MDTJ_1"]), 3)).ToString("0.000");
            //                sItem["MD_2"] = (Math.Round(0.9982 * (GetSafeDouble(sItem["MDBJWJJ_2"]) - GetSafeDouble(sItem["MDRLBZ_2"])) / GetSafeDouble(sItem["MDTJ_2"]), 3)).ToString("0.000");

            //                if (string.IsNullOrEmpty(sItem["MD_1"]) && sItem["MD_1"] != "----")
            //                {
            //                    sItem["md"] = (Math.Round((GetSafeDouble(sItem["MD_1"]) + GetSafeDouble(sItem["MD_2"])) / 2, 3)).ToString("0.000");
            //                }

            //                if (GetSafeDouble(sItem["MDKZZ"]) > 1.1)
            //                {
            //                    MItem[forEachFlag]["G_MD"] = (Math.Round(GetSafeDouble(sItem["MDKZZ"]) - 0.03, 3)).ToString("0.000") + "~" + (Math.Round(GetSafeDouble(sItem["MDKZZ"]) + 0.03, 3)).ToString("0.000");
            //                }
            //                else
            //                {
            //                    MItem[forEachFlag]["G_MD"] = (Math.Round(GetSafeDouble(sItem["MDKZZ"]) - 0.02, 3)).ToString("0.000") + "~" + (Math.Round(GetSafeDouble(sItem["mdkzz"]) + 0.02, 3)).ToString("0.000");

            //                }

            //                if (sItem["MDKZZ"] == "----")
            //                {
            //                    sItem["HG_MD"] = "----";
            //                    sItem["G_MD"] = "----";
            //                }
            //                else
            //                {
            //                    //判断符合不符合
            //                    MItem[forEachFlag]["HG_MD"] = IsQualified(MItem[forEachFlag]["G_MD"], sItem["MD"]);
            //                }

            //                if (MItem[forEachFlag]["HG_MD"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                }
            //            }
            //            break;
            //        case "固体含量":
            //            foreach (var sItem in SItem)
            //            {
            //                sItem["GTHL_1"] = (Math.Round((GetSafeDouble(sItem["GTHLM2_1"]) - GetSafeDouble(sItem["GTHLM0_1"])) / (GetSafeDouble(sItem["GTHLM1_1"]) - GetSafeDouble(sItem["GTHLM0_1"])) * 100, 2)).ToString("0.00");
            //                sItem["GTHL_2"] = (Math.Round((GetSafeDouble(sItem["GTHLM2_2"]) - GetSafeDouble(sItem["GTHLM0_2"])) / (GetSafeDouble(sItem["GTHLM1_2"]) - GetSafeDouble(sItem["GTHLM0_2"])) * 100, 2)).ToString("0.00");

            //                if (!string.IsNullOrEmpty(sItem["GTHL_1"]) || sItem["GTHL_1"] != "----")
            //                {
            //                    sItem["gthl"] = (Math.Round((GetSafeDouble(sItem["gthl_1"]) + GetSafeDouble(sItem["gthl_2"])) / 2, 2)).ToString("0.00");
            //                }

            //                if (GetSafeDouble(sItem["HGLKZZ"]) > 25)
            //                {
            //                    MItem[forEachFlag]["g_gthl"] = (Math.Round(GetSafeDouble(sItem["hglkzz"]) * 0.95, 2)).ToString("0.00") + "~" + (Math.Round(GetSafeDouble(sItem["hglkzz"]) * 1.05, 2)).ToString("0.00");
            //                }
            //                else
            //                {
            //                    MItem[forEachFlag]["G_GTHL"] = (Math.Round(GetSafeDouble(sItem["MDKZZ"]) * 0.9, 2)).ToString("0.00") + "~" + (Math.Round(GetSafeDouble(sItem["hglkzz"]) * 1.1, 2)).ToString("0.00");

            //                }

            //                if (sItem["HGLKZZ"] == "----")
            //                {
            //                    sItem["HG_GTHL"] = "----";
            //                    sItem["G_GTHL"] = "----";
            //                }
            //                else
            //                {
            //                    //判断符合不符合
            //                    MItem[forEachFlag]["HG_GTHL"] = IsQualified(MItem[forEachFlag]["G_GTHL"], sItem["GTHL"]);
            //                }

            //                if (MItem[forEachFlag]["HG_GTHL"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                }

            //            }
            //            break;
            //        case "含水率":
            //            forEachFlag = 0;
            //            foreach (var sItem in SItem)
            //            {
            //                sItem["HSL_1"] = (Math.Round((GetSafeDouble(sItem["GTHLM1_1"]) - GetSafeDouble(sItem["GTHLM2_1"])) / (GetSafeDouble(sItem["gthlm1_1"]) - GetSafeDouble(sItem["gthlm0_1"])) * 100, 2)).ToString("0.00");
            //                sItem["HSL_2"] = (Math.Round((GetSafeDouble(sItem["GTHLM1_2"]) - GetSafeDouble(sItem["GTHLM2_2"])) / (GetSafeDouble(sItem["gthlm1_2"]) - GetSafeDouble(sItem["gthlm0_2"])) * 100, 2)).ToString("0.00");

            //                if (!string.IsNullOrEmpty(sItem["GTHL_1"]) || sItem["GTHL_1"] != "----")
            //                {
            //                    sItem["hsl"] = (Math.Round((GetSafeDouble(sItem["gthl_1"]) + GetSafeDouble(sItem["hsl_2"])) / 2, 2)).ToString("0.00");
            //                }

            //                if (GetSafeDouble(sItem["HSLKZZ"]) > 5)
            //                {
            //                    MItem[forEachFlag]["G_HSL"] = (Math.Round(GetSafeDouble(sItem["HSLKZZ"]) * 0.9, 2)).ToString("0.00") + "~" + (Math.Round(GetSafeDouble(sItem["hslkzz"]) * 1.1, 2)).ToString("0.00");
            //                }
            //                else
            //                {
            //                    MItem[forEachFlag]["G_HSL"] = (Math.Round(GetSafeDouble(sItem["HSLKZZ"]) * 0.8, 2)).ToString("0.00") + "~" + (Math.Round(GetSafeDouble(sItem["hslkzz"]) * 1.2, 2)).ToString("0.00");

            //                }

            //                if (sItem["HSLKZZ"] == "----")
            //                {
            //                    sItem["HG_HSL"] = "----";
            //                    sItem["G_HSL"] = "----";
            //                }
            //                else
            //                {
            //                    MItem[forEachFlag]["HG_HSL"] = IsQualified(MItem[forEachFlag]["G_HSL"], sItem["hsl"]);
            //                }

            //                if (MItem[forEachFlag]["HG_HSL"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                }
            //            }
            //            break;
            //        case "泌水率":
            //            forEachFlag = 0;
            //            foreach (var sItem in SItem)
            //            {
            //                for (int i = 1; i < 4; i++)
            //                {
            //                    sItem["JPHWZL_" + i] = (GetSafeDouble(sItem["PBSN" + i]) + GetSafeDouble(sItem["PBS" + i]) + GetSafeDouble(sItem["PBSA" + i]) + GetSafeDouble(sItem["PBSZ" + i])).ToString();
            //                    sItem["MJBYS_" + i] = sItem["PBS" + i];
            //                }

            //                if (!string.IsNullOrEmpty(sItem["JMSZL_1"]) || sItem["JMSZL_1"] != "----")
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        sItem["JMSL_" + i] = Math.Round(GetSafeDouble(sItem["JMSZL_" + i]) / GetSafeDouble(sItem["MJBYS_" + i]) / GetSafeDouble(sItem["jphwzl_" + i]) / GetSafeDouble(sItem["jsyzl_" + i]), 2).ToString("0.00");
            //                        mtmpList.Add(GetSafeDouble(sItem["JMSL_" + i]));
            //                    }
            //                }

            //                if (!string.IsNullOrEmpty(sItem["JMSL_1"]) || sItem["JMSL_1"] != "----")
            //                {
            //                    mtmpList.Sort();
            //                    mMaxKyqd = mtmpList[2];
            //                    mMinKyqd = mtmpList[0];
            //                    mMidKyqd = mtmpList[1];
            //                    mAvgKyqd = mtmpList.Average();

            //                    //计算抗压平均、达到设计强度、及进行单组合格判定
            //                    if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        sItem["HG_MSL"] = "重做";
            //                        sItem["JPJMSL"] = "重做";
            //                    }
            //                    if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                        sItem["JPJMSL"] = Math.Round(mMidKyqd, 1).ToString("0.0");
            //                    }
            //                    if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                        sItem["JPJMSL"] = Math.Round(mMidKyqd, 1).ToString("0.0");
            //                    }
            //                    if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        //最大最小强度值均未超出中间值的15 %,试验结果取平均值
            //                        sItem["jpjmsl"] = Math.Round(mAvgKyqd, 1).ToString("0.0");
            //                    }
            //                }

            //                if (!string.IsNullOrEmpty(sItem["SPBSN1"]) || sItem["SPBSN1"] != "----")
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        sItem["SPHWZL_" + i] = (GetSafeDouble(sItem["SPBSN" + i]) + GetSafeDouble(sItem["SPBS" + i]) + GetSafeDouble(sItem["SPBSA" + i]) + GetSafeDouble(sItem["SPBSZ" + i]) + GetSafeDouble(sItem["SPBWJJ1" + i]) + GetSafeDouble(sItem["SPBWJJ21" + i])).ToString();
            //                        sItem["MSBYS_" + i] = sItem["SPBS" + i];
            //                    }
            //                }

            //                if (!string.IsNullOrEmpty(sItem["SMSZL_1"]) || sItem["SMSZL_1"] != "----")
            //                {
            //                    mtmpList.Clear();
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        sItem["SMSL_" + i] = Math.Round(GetSafeDouble(sItem["SMSZL_" + i]) / GetSafeDouble(sItem["MSBYS_" + i]) / GetSafeDouble(sItem["sphwzl_" + i]) / GetSafeDouble(sItem["ssyzl_" + i]) * 100, 2).ToString();
            //                        mtmpList.Add(GetSafeDouble(sItem["SMSL_" + i]));
            //                    }
            //                }

            //                if (!string.IsNullOrEmpty(sItem["SMSL_1"]) || sItem["SMSL_1"] != "----")
            //                {
            //                    mtmpList.Sort();
            //                    mMaxKyqd = mtmpList[2];
            //                    mMinKyqd = mtmpList[0];
            //                    mMidKyqd = mtmpList[1];
            //                    mAvgKyqd = mtmpList.Average();
            //                    //计算抗压平均、达到设计强度、及进行单组合格判定


            //                    if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        MItem[forEachFlag]["HG_MSL"] = "重做";
            //                        sItem["SPJMSL"] = "重做";
            //                    }
            //                    if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                        sItem["SPJMSL"] = Math.Round(mMidKyqd, 1).ToString("0.0");
            //                    }
            //                    if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                        sItem["SPJMSL"] = Math.Round(mMidKyqd, 1).ToString("0.0");
            //                    }
            //                    if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        //最大最小强度值均未超出中间值的15 %,试验结果取平均值
            //                        sItem["SPJMSL"] = Math.Round(mAvgKyqd, 1).ToString("0.0");
            //                    }
            //                }

            //                if (sItem["JPJMSL"].Equals("重做") || sItem["SPJMSL"].Equals("重做"))
            //                {
            //                    mbhggs = mbhggs + 1;
            //                    if (sItem["JPJMSL"].Equals("重做") && sItem["SPJMSL"].Equals("重做"))
            //                    {
            //                        MItem[forEachFlag]["HG_MSL"] = "基准受检重做";
            //                    }
            //                    else
            //                    {
            //                        MItem[forEachFlag]["HG_MSL"] = sItem["JPJMSL"].Equals("重做") ? "基准重做" : "受检重做";
            //                    }
            //                }
            //                else
            //                {
            //                    if (!string.IsNullOrEmpty(sItem["SPJMSL"]) || sItem["SPJMSL"] != "----")
            //                    {
            //                        sItem["MSLB"] = Round(GetSafeDouble(sItem["SPJMSL"]) / GetSafeDouble(sItem["JPJMSL"]) * 100, 0).ToString("0");
            //                    }
            //                    MItem[forEachFlag]["G_MSL"] = IsQualified(MItem[forEachFlag]["G_MSL"], sItem["MSLB"]);
            //                }

            //                if (MItem[forEachFlag]["HG_MSL"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                    mFlag_BHg = true;
            //                }
            //                else
            //                {
            //                    mFlag_Hg = true;
            //                }
            //            }
            //            break;
            //        case "减水率":
            //            forEachFlag = 0;
            //            foreach (var sItem in SItem)
            //            {
            //                mtmpList.Clear();
            //                if (GetSafeDouble(sItem["SYBHL"]) > 0)
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        sItem["JJDYS_" + i] = Math.Round(GetSafeDouble(sItem["PBS" + i]) / GetSafeDouble(sItem["SYBHL"]), 2).ToString();
            //                        sItem["JSDYS_" + i] = Math.Round(GetSafeDouble(sItem["SPBS" + i]) / GetSafeDouble(sItem["SYBHL"]), 2).ToString();
            //                    }
            //                }

            //                if (!string.IsNullOrEmpty(sItem["JJDYS_1"]) && sItem["jmszl_1"] != "----")
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        sItem["JSL_" + i] = Math.Round((GetSafeDouble(sItem["JJDYS_" + i]) - GetSafeDouble(sItem["JSDYS_" + i])) / GetSafeDouble(sItem["JJDYS_" + i]) * 100, 1).ToString();

            //                        mtmpList.Add(GetSafeDouble(sItem["jsl_" + i]));
            //                    }
            //                }

            //                if (!string.IsNullOrEmpty(sItem["JMSL_1"]) || sItem["JMSL_1"] != "----")
            //                {
            //                    mtmpList.Sort();
            //                    mMaxKyqd = mtmpList[2];
            //                    mMinKyqd = mtmpList[0];
            //                    mMidKyqd = mtmpList[1];
            //                    mAvgKyqd = mtmpList.Average();

            //                    //计算抗压平均、达到设计强度、及进行单组合格判定
            //                    if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        MItem[forEachFlag]["HG_JSL"] = "重做";
            //                        sItem["PJJSL"] = "重做";
            //                    }
            //                    if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                        sItem["PJJSL"] = Math.Round(mMidKyqd, 0).ToString("0");
            //                    }
            //                    if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                        sItem["PJJSL"] = Math.Round(mMidKyqd, 0).ToString("0");
            //                    }
            //                    if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
            //                    {
            //                        //最大最小强度值均未超出中间值的15 %,试验结果取平均值
            //                        sItem["PJJSL"] = Math.Round(mAvgKyqd, 0).ToString("0");
            //                    }
            //                }

            //                if (sItem["PJJSL"] == "重做")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                }
            //                else
            //                {
            //                    //判断是否合格
            //                    MItem[forEachFlag]["HG_JSL"] = IsQualified(MItem[forEachFlag]["G_JSL"], sItem["PJJSL"]);
            //                }

            //                if (MItem[forEachFlag]["HG_JSL"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                    mFlag_BHg = true;
            //                }
            //                else
            //                {
            //                    mFlag_Hg = true;
            //                }


            //            }
            //            break;
            //        case "含气量":
            //            forEachFlag = 0;
            //            foreach (var sItem in SItem)
            //            {
            //                sum = 0;
            //                for (int i = 1; i < 3; i++)
            //                {
            //                    md1 = GetSafeDouble(sItem["BHWHQL_" + i]);
            //                    md2 = GetSafeDouble(sItem["SSHQL_" + i]);

            //                    md = Math.Round(md1 - md2, 1);
            //                    sum = sum + md;
            //                }
            //                sItem["PJHQL"] = Math.Round(sum / 2, 1).ToString();

            //                MItem[forEachFlag]["HG_HQL"] = IsQualified(MItem[forEachFlag]["G_HQL"], sItem["PJHQL"]);

            //                if (MItem[forEachFlag]["HG_HQL"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                    mFlag_BHg = true;
            //                }
            //                else
            //                {
            //                    mFlag_Hg = true;
            //                }


            //            }
            //            break;
            //        case "经1h后含气量变化量":
            //            forEachFlag = 0;

            //            foreach (var sItem in SItem)
            //            {
            //                mtmpList.Clear();
            //                if (GetSafeDouble(sItem["CJHQL_1"]) > 0)
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        mtmpList.Add(GetSafeDouble(sItem["CJHQL_" + i]));
            //                    }
            //                }
            //                mtmpList.Sort();
            //                mMaxKyqd = mtmpList[2];
            //                mMinKyqd = mtmpList[0];
            //                mMidKyqd = mtmpList[1];
            //                mAvgKyqd = mtmpList.Average();

            //                //计算抗压平均、达到设计强度、及进行单组合格判定
            //                if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.5, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.5, 1))
            //                {
            //                    MItem[forEachFlag]["hg_hqlBHL"] = "重做";
            //                    sItem["CJHQL"] = "重做";
            //                }
            //                if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.5, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.5, 1))
            //                {
            //                    //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["CJHQL"] = Math.Round(mMidKyqd, 1).ToString("0.0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.5, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.5, 1))
            //                {
            //                    //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["CJHQL"] = Math.Round(mMidKyqd, 0).ToString("0.0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.5, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.5, 1))
            //                {
            //                    //最大最小强度值均未超出中间值的15 %,试验结果取平均值
            //                    sItem["CJHQL"] = Math.Round(mAvgKyqd, 0).ToString("0.0");
            //                }

            //                mtmpList.Clear();
            //                if (GetSafeDouble(sItem["HQL1HH_1"]) > 0)
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        mtmpList.Add(GetSafeDouble(sItem["HQL1HH_" + i]));
            //                    }
            //                }
            //                mtmpList.Sort();
            //                mMaxKyqd = mtmpList[2];
            //                mMinKyqd = mtmpList[0];
            //                mMidKyqd = mtmpList[1];
            //                mAvgKyqd = mtmpList.Average();

            //                //计算抗压平均、达到设计强度、及进行单组合格判定
            //                if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.5, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.5, 1))
            //                {
            //                    MItem[forEachFlag]["hg_hqlBHL"] = "重做";
            //                    sItem["HQL1HH"] = "重做";
            //                }
            //                if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.5, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.5, 1))
            //                {
            //                    //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["HQL1HH"] = Math.Round(mMidKyqd, 1).ToString("0.0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.5, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.5, 1))
            //                {
            //                    //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["HQL1HH"] = Math.Round(mMidKyqd, 0).ToString("0.0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.5, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.5, 1))
            //                {
            //                    //最大最小强度值均未超出中间值的15 %,试验结果取平均值
            //                    sItem["HQL1HH"] = Math.Round(mAvgKyqd, 0).ToString("0.0");
            //                }

            //                if (sItem["pjjsl"] == "重做")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                }
            //                else
            //                {
            //                    if (!string.IsNullOrEmpty(sItem["CJHQL"]) && sItem["CJHQL"] != "----")
            //                    {  //判断是否合格
            //                        sItem["HQLBHL"] = Round(GetSafeDouble(sItem["CJHQL"]) - GetSafeDouble(sItem["HQL1HH"]), 1).ToString("0.0");
            //                    }
            //                    MItem[forEachFlag]["HG_HQLBHL"] = IsQualified(MItem[forEachFlag]["G_HQLBHL"], sItem["HQLBHL"]);
            //                }

            //                if (MItem[forEachFlag]["HG_HQLBHL"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                    mFlag_BHg = true;
            //                }
            //                else
            //                {
            //                    mFlag_Hg = true;
            //                }


            //            }
            //            break;
            //        case "经1h后坍落度变化量":
            //            forEachFlag = 0;

            //            foreach (var sItem in SItem)
            //            {
            //                mtmpList.Clear();
            //                if (GetSafeDouble(sItem["CJHQL_1"]) > 0)
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        sItem["TLDBHL_" + i] = Math.Round((GetSafeDouble(sItem["CJTLD_" + i]) - GetSafeDouble(sItem["TLD1HH_"] + i) / 5) * 5, 0).ToString("0");
            //                        mtmpList.Add(GetSafeDouble(sItem["TLDBHL_" + i]));
            //                    }
            //                }
            //                mtmpList.Sort();
            //                mMaxKyqd = mtmpList[2];
            //                mMinKyqd = mtmpList[0];
            //                mMidKyqd = mtmpList[1];
            //                mAvgKyqd = mtmpList.Average();

            //                //计算抗压平均、达到设计强度、及进行单组合格判定
            //                if (mMaxKyqd - mMidKyqd > 10 && mMidKyqd - mMinKyqd > 10)
            //                {
            //                    MItem[forEachFlag]["HG_TLD"] = "重做";
            //                    sItem["PJTLDBHL"] = "重做";
            //                }
            //                if (mMaxKyqd - mMidKyqd > 10 && mMidKyqd - mMinKyqd <= 10)
            //                {
            //                    //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["PJTLDBHL"] = (Round(mMidKyqd / 5, 0) * 5).ToString("0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= 10 && mMidKyqd - mMinKyqd > 10)
            //                {
            //                    //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["PJTLDBHL"] = (Round(mMidKyqd / 5, 0) * 5).ToString("0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= 10 && mMidKyqd - mMinKyqd <= 10)
            //                {
            //                    //最大最小强度值均未超出中间值的15 %,试验结果取平均值
            //                    sItem["PJTLDBHL"] = (Round(mAvgKyqd / 5, 0) * 5).ToString("0");
            //                }

            //                if (sItem["PJTLDBHL"] == "重做")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                }
            //                else
            //                {
            //                    MItem[forEachFlag]["HG_TLD"] = IsQualified(MItem[forEachFlag]["G_TLD"], sItem["PJTLDBHL"]);
            //                }

            //                if (MItem[forEachFlag]["HG_TLD"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                    mFlag_BHg = true;
            //                }
            //                else
            //                {
            //                    mFlag_Hg = true;
            //                }

            //            }
            //            break;
            //        case "初凝时间差":
            //            forEachFlag = 0;

            //            foreach (var sItem in SItem)
            //            {
            //                mtmpList.Clear();
            //                if (GetSafeDouble(sItem["CNSJT_1"]) > 0)
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        sItem["CNSJT_" + i] = (GetSafeDouble(sItem["CNSJT_" + i]) - GetSafeDouble(sItem["CNSJT_"] + i)).ToString();
            //                        mtmpList.Add(GetSafeDouble(sItem["CNSJT_" + i]));
            //                    }
            //                }
            //                mtmpList.Sort();
            //                mMaxKyqd = mtmpList[2];
            //                mMinKyqd = mtmpList[0];
            //                mMidKyqd = mtmpList[1];
            //                mAvgKyqd = mtmpList.Average();

            //                //计算抗压平均、达到设计强度、及进行单组合格判定
            //                if (mMaxKyqd - mMidKyqd > 30 && mMidKyqd - mMinKyqd > 30)
            //                {
            //                    MItem[forEachFlag]["HG_CNSJC"] = "重做";
            //                    sItem["CNPJSJC"] = "重做";
            //                }
            //                if (mMaxKyqd - mMidKyqd > 30 && mMidKyqd - mMinKyqd <= 30)
            //                {
            //                    //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["CNPJSJC"] = Round(mMidKyqd, 0).ToString("0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= 30 && mMidKyqd - mMinKyqd > 30)
            //                {
            //                    //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["CNPJSJC"] = Round(mMidKyqd, 0).ToString("0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= 30 && mMidKyqd - mMinKyqd <= 30)
            //                {
            //                    //最大最小强度值均未超出中间值的15 %,试验结果取平均值
            //                    sItem["CNPJSJC"] = Round(mAvgKyqd, 0).ToString("0");
            //                }

            //                if (sItem["重做"] == "重做")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                }
            //                else
            //                {
            //                    MItem[forEachFlag]["HG_CNSJC"] = IsQualified(MItem[forEachFlag]["G_CNSJC"], sItem["CNPJSJC"]);
            //                }

            //                if (MItem[forEachFlag]["HG_CNSJC"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                    mFlag_BHg = true;
            //                }
            //                else
            //                {
            //                    mFlag_Hg = true;
            //                }


            //            }
            //            break;
            //        case "终凝时间差":
            //            forEachFlag = 0;

            //            foreach (var sItem in SItem)
            //            {
            //                mtmpList.Clear();
            //                if (GetSafeDouble(sItem["ZNSJT_1"]) > 0)
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        sItem["ZNSJT_" + i] = (GetSafeDouble(sItem["ZNSJT_" + i]) - GetSafeDouble(sItem["CNSJT_"] + i)).ToString();
            //                        mtmpList.Add(GetSafeDouble(sItem["ZNSJT_" + i]));
            //                    }
            //                }
            //                mtmpList.Sort();
            //                mMaxKyqd = mtmpList[2];
            //                mMinKyqd = mtmpList[0];
            //                mMidKyqd = mtmpList[1];
            //                mAvgKyqd = mtmpList.Average();

            //                //计算抗压平均、达到设计强度、及进行单组合格判定
            //                if (mMaxKyqd - mMidKyqd > 30 && mMidKyqd - mMinKyqd > 30)
            //                {
            //                    MItem[forEachFlag]["hg_znsjc"] = "重做";
            //                    sItem["ZNPJSJC"] = "重做";
            //                }
            //                if (mMaxKyqd - mMidKyqd > 30 && mMidKyqd - mMinKyqd <= 30)
            //                {
            //                    //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["ZNPJSJC"] = Round(mMidKyqd, 0).ToString("0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= 30 && mMidKyqd - mMinKyqd > 30)
            //                {
            //                    //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
            //                    sItem["ZNPJSJC"] = Round(mMidKyqd, 0).ToString("0");
            //                }
            //                if (mMaxKyqd - mMidKyqd <= 30 && mMidKyqd - mMinKyqd <= 30)
            //                {
            //                    //最大最小强度值均未超出中间值的15 %,试验结果取平均值
            //                    sItem["PJTLDBHL"] = Round(mAvgKyqd, 0).ToString("0");
            //                }

            //                if (sItem["ZNPJSJC"] == "重做")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                }
            //                else
            //                {
            //                    MItem[forEachFlag]["HG_ZNSJC"] = IsQualified(MItem[forEachFlag]["G_ZNSJC"], sItem["ZNPJSJC"]);
            //                }

            //                if (MItem[forEachFlag]["HG_ZNSJC"] == "不符合")
            //                {
            //                    mbhggs = mbhggs + 1;
            //                    mFlag_BHg = true;
            //                }
            //                else
            //                {
            //                    mFlag_Hg = true;
            //                }


            //            }
            //            break;
            //        case "相对耐久性":
            //            forEachFlag = 0;
            //            foreach (var sItem in SItem)
            //            {
            //                mtmpList.Clear();
            //                if (GetSafeDouble(sItem["XDTXML_"]) > 0)
            //                {
            //                    for (int i = 1; i < 4; i++)
            //                    {
            //                        sItem["XDTXML_" + i] = Round(GetSafeDouble(sItem["XDTXML_" + i]) * GetSafeDouble(sItem["XDRHJP_"] + i) / GetSafeDouble(sItem["XJPCZ_" + i]) / GetSafeDouble(sItem["XJPCZ_" + i]) * 100, 1).ToString();

            //                        mtmpList.Add(GetSafeDouble(sItem["XDTXML_" + i]));
            //                    }
            //                }

            //                sItem["XPJDTXML"] = Round(mtmpList.Average(), 0).ToString(); ;



            //                MItem[forEachFlag]["HG_XDNJX"] = IsQualified(MItem[forEachFlag]["g_xdnjx"], sItem["XPJDTXML"]);


            //            }
            //            break;
            //        default:
            //            break;
            //    }

            //    if (mbhggs == 0)
            //    {
            //        XQData[index]["JCJG"] = "合格";
            //        XQData[index]["JCJGMS"] = "该组试样所检项目符合{}" + "标准要求。";
            //    }
            //    else
            //    {
            //        XQData[index]["JCJG"] = "不合格";
            //        XQData[index]["JCJGMS"] = "该组试样不符合" + "标准要求。";
            //    }
            //}

            #region 添加最终报告
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            if (mbhggs != 0)
            {
                //不合格
                bgjgDic.Add("JCJG", "不合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。");
            }
            else
            {
                bgjgDic.Add("JCJG", "合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。");
            }

            //bgjg.Add(bgjgDic);
            //retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            //retData["BGJG"].Add("BGJG", bgjg);
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
