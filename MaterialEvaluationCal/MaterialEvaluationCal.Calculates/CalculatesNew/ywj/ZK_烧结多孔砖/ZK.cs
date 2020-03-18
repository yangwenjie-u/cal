using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZK : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_ZK_DJ"];
            var extraWCDj = dataExtra["BZ_ZKWCDJ"];
            var extraKFHDJ = dataExtra["BZ_ZKKFHDJ"];
            var extraGMDJB = dataExtra["BZ_ZKGMDJB"];


            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_ZK"];

            if (!data.ContainsKey("M_ZK"))
            {
                data["M_ZK"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_ZK"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mItemHg = true;
            var mSjdj = "";
            var mJSFF = "";
            var jcxm = "";

            double mYqpjz, mXdy21, mDy21 = 0;
            bool sign = false;
            double md1, md2, md, sum, pjmd = 0;
            int xd = 0, Gs;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            bool flag;
            double[] nArray;
            List<double> nArr = new List<double>();
            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
                 delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
                 {

                     mbhggs = 0;
                     jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                     mFlag_Hg = true;

                     if (jcxm.Contains("、抗压强度、"))
                     {
                         sign = true;

                         for (xd = 1; xd < 11; xd++)
                         {
                             sign = IsNumeric(sItem["KYQD" + xd]) && !string.IsNullOrEmpty(sItem["KYQD" + xd]) ? sign : false;

                             if (!sign)
                             {
                                 break;
                             }
                         }

                         if (!sign)
                         {
                             mAllHg = false;
                             return false;
                         }
                         sum = 0;
                         nArr.Add(0);
                         for (xd = 1; xd < 11; xd++)
                         {
                             md = double.Parse(sItem["KYQD" + xd]);
                             nArr.Add(md);
                             sum += md;
                         }
                         pjmd = Math.Round(sum / 10, 1);
                         sItem["KYPJ"] = pjmd.ToString("0.0");

                         sum = 0;
                         for (xd = 1; xd < 11; xd++)
                         {
                             md = nArr[xd] - pjmd;
                             nArr.Add(md);
                             sum += Math.Pow(md, 2);
                         }

                         md1 = Math.Sqrt(sum / 9);
                         md1 = Math.Round(md1, 2);

                         sItem["BZC"] = md1.ToString("0.00");
                         sItem["QDYQ"] = "抗压强度平均值需≥" + string.Format(MItem[0]["G_PJZ"], 0) + "MPa。强度标准值需≥" + string.Format(MItem[0]["G_BZZ"], "0.0") + "MPa。";


                         md2 = md1 / pjmd;
                         md2 = Math.Round(md2, 2);
                         sItem["BYXS"] = md2.ToString("0.00");

                         nArr.Sort();

                         sItem["QDMIN"] = nArr[0].ToString("0.00");


                         md2 = Math.Round(pjmd - 1.83 * md1, 1);
                         sItem["BZZ"] = md2.ToString("0.0");

                         sign = IsQualified("≥" + double.Parse(MItem[0]["G_PJZ"]).ToString("0"), sItem["KYPJ"], false) == "合格" ? sign : false;
                         sign = IsQualified("≥" + double.Parse(MItem[0]["G_BZZ"]).ToString("0.0"), sItem["BZZ"], false) == "合格" ? sign : false;

                         sItem["QDPD"] = sign ? "合格" : "不合格";

                         if (sItem["QDPD"] == "合格")
                         {
                             mFlag_Hg = true;
                         }
                         else
                         {
                             mFlag_Bhg = true;
                             mbhggs++;
                         }

                     }
                     else
                     {
                         sItem["KYPJ"] = "----";
                         sItem["QDPD"] = "----";
                         sItem["BZC"] = "----";
                         sItem["BYXS"] = "----";
                         sItem["QDMIN"] = "----";
                         sItem["BZZ"] = "----";
                         sItem["QDYQ"] = "----";

                         for (int i = 1; i < 11; i++)
                         {
                             sItem["KYQD" + i] = "----";
                         }
                     }

                     if (jcxm.Contains("、密度等级、"))
                     {
                         sign = true;

                         for (xd = 1; xd < 4; xd++)
                         {
                             sign = IsNumeric(sItem["GMD" + xd]) && !string.IsNullOrEmpty(sItem["GMD" + xd]) ? sign : false;

                             if (!sign)
                             {
                                 break;
                             }
                         }
                         if (!sign)
                         {
                             mAllHg = false;
                             return false;
                         }
                         sum = 0;
                         for (xd = 1; xd < 4; xd++)
                         {
                             md = double.Parse(sItem["GMD" + xd]);
                             sum += md;
                         }

                         pjmd = Math.Round(sum / 3, 0);
                         sItem["GMDPJ"] = pjmd.ToString("0");
                         sItem["GMDPD"] = IsQualified(MItem[0]["G_GMD"], sItem["GMDPJ"], false);

                         if (sItem["GMDPD"] == "合格")
                         {
                             mFlag_Hg = true;
                         }
                         else
                         {
                             mFlag_Bhg = true;
                             mbhggs++;
                         }
                     }
                     else
                     {
                         mItem["G_GMD"] = "----";
                         sItem["GMDPD"] = "----";
                         for (int i = 1; i < 4; i++)
                         {
                             sItem["GMD" + i] = "----";
                         }
                         sItem["GMDPJ"] = "----";
                     }

                     if (jcxm.Contains("、抗风化性能、"))
                     {
                         //if (jcxm.Contains("、冻融、"))
                         //{
                         sign = true;

                         for (xd = 1; xd <= 5; xd++)
                         {
                             sign = sItem["SFDH" + xd] == "否" ? sign : false;
                             sItem["DRWG" + xd] = sItem["SFDH" + xd] == "否" ? "0" : "1";
                         }

                         sItem["DRPD"] = sign ? "合格" : "不合格";
                         if (sItem["DRPD"] == "合格")
                         {
                             mFlag_Hg = true;
                         }
                         else
                         {
                             mFlag_Bhg = true;
                             mbhggs++;
                         }
                         //}
                         //else
                         //{
                         //    sItem["DRPD"] = "----";
                         //    for (int i = 1; i <= 5; i++)
                         //    {
                         //        sItem["DRWG" + i] = "----";
                         //    }
                         //}
                     }
                     sItem["BHXSPD"] = "----";
                     sItem["FSPD"] = "----";
                     sItem["SHBLPD"] = "----";

                     if (mbhggs == 0)
                     {
                         jsbeizhu = "该组试件所检项目符合" + mItem["PDBZ"] + "标准要求。";
                         sItem["JCJG"] = "合格";
                     }

                     if (mbhggs > 0)
                     {
                         sItem["JCJG"] = "不合格";
                         mAllHg = false;
                         jsbeizhu = "该组试件不符合" + mItem["PDBZ"] + "标准要求。";
                     }


                     return mAllHg;
                 };

            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mSjdj = string.IsNullOrEmpty(sItem["SJDJ"]) ? "" : sItem["SJDJ"];


                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj.Trim() && u["PDBZ"].Contains("13544-2000"));

                // 从设计等级表中取得相应的计算数值、等级标准
                if (MItem[0]["PDBZ"].Trim().Contains("13544 - 2011"))
                {
                    mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj.Trim() && u["PDBZ"].Contains("13544-2011%"));
                }


                if (mrsDj != null)
                {
                    mYqpjz = double.Parse(mrsDj["PJZ"]);
                    mXdy21 = double.Parse(mrsDj["XDY21"]);
                    mDy21 = double.Parse(mrsDj["DY21"]);
                    MItem[0]["G_PJZ"] = (mYqpjz).ToString();
                    MItem[0]["G_BZZ"] = (mXdy21).ToString();
                    MItem[0]["G_MIN"] = (mDy21).ToString();
                    //MItem[0]["WHICH = MRSDJ["which
                    //MItem[0]["BGNAME = TRIM(MRSDJ["bgname)
                    mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"];
                }
                else
                {
                    mYqpjz = 0;
                    mXdy21 = 0;
                    mDy21 = 0;
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "试件尺寸为空";
                    mAllHg = false;
                }

                var mrskfhDj = extraKFHDJ.FirstOrDefault(u => u["MC"] == sItem["ZZL"].Trim() && u["PZ"] == sItem["WGDJ"].Trim());
                if (mrskfhDj != null)
                {
                    sItem["XSLPJZYQ"] = mrskfhDj["XSLPJ"];
                    sItem["XSLZDZYQ"] = mrskfhDj["XSLDKZD"];
                    sItem["BHXSPJZYQ"] = mrskfhDj["BHXSPJ"];
                    sItem["BHXSZDZYQ"] = mrskfhDj["BHXSZDZ"];
                    sItem["FSYQ"] = mrskfhDj["FSYQ"];
                    sItem["SHBLYQ"] = mrskfhDj["DHBLYQ"];
                    sItem["DRYQ"] = mrskfhDj["DRYQ"];
                }

                var mrsgmdjb = extraGMDJB.FirstOrDefault(u => u["MC"] == sItem["GMDDJ"].Trim());
                if (mrskfhDj != null)
                {
                    MItem[0]["G_GMD"] = mrsgmdjb["GMD"];
                }

                var mrswcdj = extraWCDj.FirstOrDefault(u => u["MC"].Equals(sItem["WGDJ"].Trim()));
                sItem["LQ"] = (GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime(sItem["ZZRQ"])).Days.ToString();

                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
                    if (jcxm.Contains("、密度等级、"))
                    {
                        sItem["GMDCD1"] = Round((Conversion.Val(sItem["GMDCD1_1"]) + Conversion.Val(sItem["GMDCD1_2"])) / 2, 0).ToString();
                        sItem["GMDKD1"] = Round((Conversion.Val(sItem["GMDKD1_1"]) + Conversion.Val(sItem["GMDKD1_2"])) / 2, 0).ToString();
                        sItem["GMDGD1"] = Round((Conversion.Val(sItem["GMDGD1_1"]) + Conversion.Val(sItem["GMDGD1_2"])) / 2, 0).ToString();
                        double mtj1 = (Conversion.Val(sItem["GMDCD1"]) / 1000 * Conversion.Val(sItem["GMDKD1"]) / 1000 * Conversion.Val(sItem["GMDGD1"]) / 1000);
                        sItem["GMDCD2"] = Round((Conversion.Val(sItem["GMDCD2_1"]) + Conversion.Val(sItem["GMDCD2_2"])) / 2, 0).ToString();
                        sItem["GMDKD2"] = Round((Conversion.Val(sItem["GMDKD2_1"]) + Conversion.Val(sItem["GMDKD2_2"])) / 2, 0).ToString();
                        sItem["GMDGD2"] = Round((Conversion.Val(sItem["GMDGD2_1"]) + Conversion.Val(sItem["GMDGD2_2"])) / 2, 0).ToString();
                        double mtj2 = (Conversion.Val(sItem["GMDCD2"]) / 1000 * Conversion.Val(sItem["GMDKD2"]) / 1000 * Conversion.Val(sItem["GMDGD2"]) / 1000);
                        sItem["GMDCD3"] = Round((Conversion.Val(sItem["GMDCD3_1"]) + Conversion.Val(sItem["GMDCD3_2"])) / 2, 0).ToString();
                        sItem["GMDKD3"] = Round((Conversion.Val(sItem["GMDKD3_1"]) + Conversion.Val(sItem["GMDKD3_2"])) / 2, 0).ToString();
                        sItem["GMDGD3"] = Round((Conversion.Val(sItem["GMDGD3_1"]) + Conversion.Val(sItem["GMDGD3_2"])) / 2, 0).ToString();
                        double mtj3 = (Conversion.Val(sItem["GMDCD3"]) / 1000 * Conversion.Val(sItem["GMDKD3"]) / 1000 * Conversion.Val(sItem["GMDGD3"]) / 1000);
                        sItem["GMD1"] = Round((Conversion.Val(sItem["HGHZL1"]) / (mtj1)), 1).ToString();
                        sItem["GMD2"] = Round((Conversion.Val(sItem["HGHZL2"]) / (mtj2)), 1).ToString();
                        sItem["GMD3"] = Round((Conversion.Val(sItem["HGHZL3"]) / (mtj3)), 1).ToString();
                        sItem["GMDPJ"] = Round((Conversion.Val(sItem["GMD1"]) + Conversion.Val(sItem["GMD2"]) + Conversion.Val(sItem["GMD3"])) / 3, 0).ToString();
                        if (IsQualified(MItem[9]["G_GMD"], sItem["GMDPJ"]) == "符合")
                        {
                            sItem["GMDPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["GMDPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                        sItem["GMDPD"] = "----";
                    sign = true;
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        for (Gs = 1; Gs <= 10; Gs++)
                        {
                            for (xd = 1; xd <= 2; xd++)
                            {
                                sign = IsNumeric(sItem["CD" + Gs + "_" + xd]) && !string.IsNullOrEmpty(sItem["CD" + Gs + "_" + xd]) ? sign : false;
                                sign = IsNumeric(sItem["KD" + Gs + "_" + xd]) && !string.IsNullOrEmpty(sItem["KD" + Gs + "_" + xd]) ? sign : false;
                            }
                            sign = IsNumeric(sItem["KYHZ" + Gs]) && !string.IsNullOrEmpty(sItem["KYHZ" + Gs]) ? sign : false;
                            if (!sign)
                                break;
                        }
                        if (sign)
                        {
                            sum = 0;
                            nArray = new double[11];
                            for (Gs = 1; Gs <= 10; Gs++)
                            {
                                md1 = 0;
                                md2 = 0;
                                for (xd = 1; xd <= 2; xd++)
                                {
                                    md1 = md1 + Conversion.Val(sItem["CD" + Gs + "_" + xd].Trim());
                                    md2 = md2 + Conversion.Val(sItem["KD" + Gs + "_" + xd].Trim());
                                }
                                md1 = md1 / 2;
                                md2 = md2 / 2;
                                md1 = Round((md1), 0);
                                md2 = Round((md2), 0);
                                sItem["CD" + Gs] = md1.ToString();
                                sItem["KD" + Gs] = md2.ToString();
                                md2 = md1 * md2;
                                sItem["MJ" + Gs] = md2.ToString();
                                md1 = Conversion.Val(sItem["KYHZ" + Gs].Trim());
                                md = 1000 * md1 / md2;
                                md = Round((md), 2);
                                sItem["KYQD" + Gs] = md.ToString("0.00");
                                sum = sum + md;
                                nArray[Gs] = md;
                            }
                            pjmd = sum / 10;
                            pjmd = Round((pjmd), 1);
                            sItem["KYPJ"] = pjmd.ToString("0.0");
                            sum = 0;
                            for (Gs = 1; Gs <= 10; Gs++)
                                sum = sum + Math.Pow((nArray[Gs] - pjmd), 2);
                            md = Math.Sqrt(sum / 9);
                            md = Round((md), 2);
                            sItem["BZC"] = md.ToString("0.00");
                            Array.Sort(nArray);
                            sItem["QDMIN"] = nArray[1].ToString("0.00");
                            MItem[0]["G_PJZ"] = mYqpjz.ToString();
                            MItem[0]["G_BZZ"] = mXdy21.ToString();
                            MItem[0]["G_MIN"] = mDy21.ToString();
                            sItem["QDYQ"] = "抗压强度平均值需≥" + Conversion.Val(MItem[0]["G_PJZ"]).ToString("0").Trim() + "MPa。强度标准值需≥" + Conversion.Val(MItem[0]["G_BZZ"]).ToString("0.0").Trim() + "MPa。";
                            //变异系数
                            sItem["BYXS"] = Round((md / pjmd), 2).ToString("0.00");
                            md = pjmd - 1.83 * md;
                            md = Round(md, 1);
                            sItem["BZZ"] = md.ToString("0.0");
                            if (IsQualified("≥" + Conversion.Val(MItem[0]["G_PJZ"]).ToString("0").Trim(), sItem["KYPJ"], false) == "不合格" ||
                                 IsQualified("≥" + Conversion.Val(MItem[0]["G_BZZ"]).ToString("0.0").Trim(), sItem["BZZ"], false) == "不合格")
                            {
                                sItem["QDPD"] = "不合格";
                                mAllHg = false;
                                mFlag_Bhg = true;
                            }
                            else
                            {
                                sItem["QDPD"] = "合格";
                                mFlag_Hg = true;
                            }
                        }
                    }
                    if (!sign)
                        sItem["QDPD"] = "----";
                    if (jcxm.Contains("、尺寸、"))
                    {
                        MItem[0]["G_PJPC"] = mrswcdj["PJPC"];
                        MItem[0]["G_CCJC"] = mrswcdj["CCJC"];
                        if (Math.Abs(Conversion.Val(MItem[0]["PJPC"])) <= GetSafeDouble(mrswcdj["PJPC"]) && GetSafeDouble(MItem[0]["CCJC"]) <= GetSafeDouble(mrswcdj["CCJC"]))
                        {
                            sItem["CCPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["CCPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                        sItem["CCPD"] = "----";
                    if (jcxm.Contains("、外观、"))
                    {
                        if (GetSafeDouble(MItem[0]["WGBHGS"]) <= GetSafeDouble(mrswcdj["WYBHGS"]))
                        {
                            sItem["WGPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            if (GetSafeDouble(MItem[0]["WGBHGS"]) >= GetSafeDouble(mrswcdj["WBYBHGS"]))
                            {
                                sItem["WGPD"] = "不合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                if (GetSafeDouble(MItem[0]["WGBHGS"]) + GetSafeDouble(MItem[0]["WGBHGS2"]) <= GetSafeDouble(mrswcdj["WYBHGS2"]))
                                {
                                    sItem["WGPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                                if (GetSafeDouble(MItem[0]["WGBHGS"]) + GetSafeDouble(MItem[0]["WGBHGS2"]) >= GetSafeDouble(mrswcdj["WBYBHGS2"]))
                                {
                                    sItem["WGPD"] = "不合格";
                                    mFlag_Hg = true;
                                }
                            }
                        }
                    }
                    else
                        sItem["WGPD"] = "----";
                    if (jcxm.Contains("、冻融、") || jcxm.Contains("、抗风化性能、"))
                    {
                        if (sItem["DRWG1"].Trim() == "1" || sItem["DRWG2"].Trim() == "1" || sItem["DRWG3"].Trim() == "1" || sItem["DRWG4"].Trim() == "1" || sItem["DRWG5"].Trim() == "1")
                        {
                            sItem["DRPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            if (sItem["DRWG1"].Trim() == "0" && sItem["DRWG2"].Trim() == "0" && sItem["DRWG3"].Trim() == "0" && sItem["DRWG4"].Trim() == "0" && sItem["DRWG5"].Trim() == "0")
                            {
                                sItem["DRPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                                sItem["DRPD"] = "----";
                        }
                    }
                    else
                        sItem["DRPD"] = "----";
                    if (sItem["QDPD"] == "合格")
                    {
                        if (jcxm.Contains("、吸水率和饱和系数、") || jcxm.Contains("、抗风化性能、"))
                        {
                            sItem["XSLBHXSYQ"] = "5h沸渚吸水率平均值需" + sItem["XSLPJZYQ"].Trim() + "%，单块最大值需" + sItem["XSLZDZYQ"].Trim() + "%；饱和系数平均值需" + sItem["BHXSPJZYQ"].Trim() + "%，单块最大值需" + sItem["BHXSZDZYQ"].Trim() + "%。";
                            double mxsl1 = Round((Conversion.Val(sItem["BHXSG5_1"]) - Conversion.Val(sItem["BHXSG0_1"])) / Conversion.Val(sItem["BHXSG0_1"]) * 100, 1);
                            double mxsl2 = Round((Conversion.Val(sItem["BHXSG5_2"]) - Conversion.Val(sItem["BHXSG0_2"])) / Conversion.Val(sItem["BHXSG0_2"]) * 100, 1);
                            double mxsl3 = Round((Conversion.Val(sItem["BHXSG5_3"]) - Conversion.Val(sItem["BHXSG0_3"])) / Conversion.Val(sItem["BHXSG0_3"]) * 100, 1);
                            double mxsl4 = Round((Conversion.Val(sItem["BHXSG5_4"]) - Conversion.Val(sItem["BHXSG0_4"])) / Conversion.Val(sItem["BHXSG0_4"]) * 100, 1);
                            double mxsl5 = Round((Conversion.Val(sItem["BHXSG5_5"]) - Conversion.Val(sItem["BHXSG0_5"])) / Conversion.Val(sItem["BHXSG0_5"]) * 100, 1);
                            sItem["XSLPJZ"] = Round((mxsl1 + mxsl2 + mxsl3 + mxsl4 + mxsl5) / 5, 0).ToString();
                            double mxslzdz = mxsl1;
                            if (mxslzdz < mxsl2)
                                mxslzdz = mxsl2;
                            if (mxslzdz < mxsl3)
                                mxslzdz = mxsl3;
                            if (mxslzdz < mxsl4)
                                mxslzdz = mxsl4;
                            if (mxslzdz < mxsl5)
                                mxslzdz = mxsl5;
                            sItem["XSLDKZD"] = mxslzdz.ToString("0.0");
                            mxsl1 = Round((Conversion.Val(sItem["BHXSG24_1"]) - Conversion.Val(sItem["BHXSG0_1"])) / (Conversion.Val(sItem["BHXSG5_1"]) - Conversion.Val(sItem["BHXSG0_1"])) * 100, 3);
                            mxsl2 = Round((Conversion.Val(sItem["BHXSG24_2"]) - Conversion.Val(sItem["BHXSG0_2"])) / (Conversion.Val(sItem["BHXSG5_2"]) - Conversion.Val(sItem["BHXSG0_2"])) * 100, 3);
                            mxsl3 = Round((Conversion.Val(sItem["BHXSG24_3"]) - Conversion.Val(sItem["BHXSG0_3"])) / (Conversion.Val(sItem["BHXSG5_3"]) - Conversion.Val(sItem["BHXSG0_3"])) * 100, 3);
                            mxsl4 = Round((Conversion.Val(sItem["BHXSG24_4"]) - Conversion.Val(sItem["BHXSG0_4"])) / (Conversion.Val(sItem["BHXSG5_4"]) - Conversion.Val(sItem["BHXSG0_4"])) * 100, 3);
                            mxsl5 = Round((Conversion.Val(sItem["BHXSG24_5"]) - Conversion.Val(sItem["BHXSG0_5"])) / (Conversion.Val(sItem["BHXSG5_5"]) - Conversion.Val(sItem["BHXSG0_5"])) * 100, 3);
                            sItem["BHXSPJZ"] = Round((mxsl1 + mxsl2 + mxsl3 + mxsl4 + mxsl5) / 5, 2).ToString("0.00");
                            mxslzdz = mxsl1;
                            if (mxslzdz < mxsl2)
                                mxslzdz = mxsl2;
                            if (mxslzdz < mxsl3)
                                mxslzdz = mxsl3;
                            if (mxslzdz < mxsl4)
                                mxslzdz = mxsl4;
                            if (mxslzdz < mxsl5)
                                mxslzdz = mxsl5;
                            sItem["BHXSZDZ"] = mxslzdz.ToString("0.000");
                            if (IsQualified(sItem["XSLPJZYQ"], sItem["XSLPJZ"]) == "不合格" || IsQualified(sItem["XSLZDZYQ"], sItem["XSLDKZD"]) == "不合格" || IsQualified(sItem["BHXSPJZYQ"], sItem["BHXSPJZ"]) == "不合格" || IsQualified(sItem["BHXSZDZYQ"], sItem["BHXSZDZ"]) == "不合格")
                            {
                                sItem["BHXSPD"] = "不合格";
                                mFlag_Bhg = true;
                            }
                            else
                            {
                                sItem["BHXSPD"] = "合格";
                                mFlag_Hg = true;
                            }
                        }
                        else
                            sItem["BHXSPD"] = "----";
                        if (jcxm.Contains("、泛霜、"))
                        {
                            int mfscnt = 0;
                            int mfscnt1 = 0;
                            int mfscnt2 = 0;
                            if (sItem["FSCD1"].Trim() == "轻度")
                                mfscnt = mfscnt + 1;
                            if (sItem["FSCD2"].Trim() == "轻度")
                                mfscnt = mfscnt + 1;
                            if (sItem["FSCD3"].Trim() == "轻度")
                                mfscnt = mfscnt + 1;
                            if (sItem["FSCD4"].Trim() == "轻度")
                                mfscnt = mfscnt + 1;
                            if (sItem["FSCD5"] == "轻度")
                                mfscnt = mfscnt + 1;
                            if (sItem["FSCD1"].Trim() == "中等")
                                mfscnt1 = mfscnt1 + 1;
                            if (sItem["FSCD2"].Trim() == "中等")
                                mfscnt1 = mfscnt1 + 1;
                            if (sItem["FSCD3"].Trim() == "中等")
                                mfscnt1 = mfscnt1 + 1;
                            if (sItem["FSCD4"].Trim() == "中等")
                                mfscnt1 = mfscnt1 + 1;
                            if (sItem["FSCD5"].Trim() == "中等")
                                mfscnt1 = mfscnt1 + 1;
                            if (sItem["FSCD1"].Trim() == "严重")
                                mfscnt2 = mfscnt2 + 1;
                            if (sItem["FSCD2"] == "严重")
                                mfscnt2 = mfscnt2 + 1;
                            if (sItem["FSCD3"].Trim() == "严重")
                                mfscnt2 = mfscnt2 + 1;
                            if (sItem["FSCD4"].Trim() == "严重")
                                mfscnt2 = mfscnt2 + 1;
                            if (sItem["FSCD5"].Trim() == "严重")
                                mfscnt2 = mfscnt2 + 1;
                            if (sItem["WGDJ"] == "合格品")
                            {
                                if (mfscnt2 > 0)
                                {
                                    sItem["FSPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    sItem["FSPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                            }
                            if (sItem["WGDJ"] == "一等品")
                            {
                                if (mfscnt1 > 0 || mfscnt2 > 0)
                                {
                                    sItem["FSPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    sItem["FSPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                            }
                            if (sItem["WGDJ"] == "优等品")
                            {
                                if (mfscnt > 0 || mfscnt1 > 0 || mfscnt2 > 0)
                                {
                                    sItem["FSPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    sItem["FSPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                            }
                        }
                        else
                            sItem["FSPD"] = "----";
                        if (jcxm.Contains("、石灰爆裂、"))
                        {
                            int mbhgs = 0;
                            if (sItem["WGDJ"].Trim() == "合格品")
                            {
                                if ((Conversion.Val(sItem["BLDS10_1"]) + (Conversion.Val(sItem["BLDS15_1"])) <= 15) && ((Conversion.Val(sItem["BLDS15_1"])) <= 7) && ((Conversion.Val(sItem["BLDS16_1"])) == 0))
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_2"]) + (Conversion.Val(sItem["BLDS15_2"])) <= 15) && ((Conversion.Val(sItem["BLDS15_2"])) <= 7) && ((Conversion.Val(sItem["BLDS16_2"])) == 0))
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (((Conversion.Val(sItem["BLDS10_3"])) + (Conversion.Val(sItem["BLDS15_3"])) <= 15) && ((Conversion.Val(sItem["BLDS15_3"])) <= 7) && ((Conversion.Val(sItem["BLDS16_3"])) == 0))
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (Conversion.Val(sItem["BLDS10_4"]) + (Conversion.Val(sItem["BLDS15_4"])) <= 15 && Conversion.Val(sItem["BLDS15_4"]) <= 7 && Conversion.Val(sItem["BLDS16_4"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (Conversion.Val(sItem["BLDS10_5"]) + Conversion.Val(sItem["BLDS15_5"]) <= 15 && Conversion.Val(sItem["BLDS15_5"]) <= 7 && Conversion.Val(sItem["BLDS16_5"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (mbhgs > 0)
                                {
                                    sItem["SHBLPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    sItem["SHBLPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                            }
                            if (sItem["WGDJ"].Trim() == "一等品")
                            {
                                mbhgs = 0;
                                if (15 >= Conversion.Val(sItem["BLDS10_1"]) && Conversion.Val(sItem["BLDS15_1"]) + Conversion.Val(sItem["BLDS16_1"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (15 >= Conversion.Val(sItem["BLDS10_2"]) && Conversion.Val(sItem["BLDS15_2"]) + Conversion.Val(sItem["BLDS16_2"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (15 >= Conversion.Val(sItem["BLDS10_3"]) && Conversion.Val(sItem["BLDS15_3"]) + Conversion.Val(sItem["BLDS16_3"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (15 >= Conversion.Val(sItem["BLDS10_4"]) && Conversion.Val(sItem["BLDS15_4"]) + Conversion.Val(sItem["BLDS16_4"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (15 >= Conversion.Val(sItem["BLDS10_5"]) && Conversion.Val(sItem["BLDS15_5"]) + Conversion.Val(sItem["BLDS16_5"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (mbhgs > 0)
                                {
                                    sItem["SHBLPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    sItem["SHBLPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                            }
                            if (sItem["WGDJ"] == "优等品")
                            {
                                mbhgs = 0;
                                if (Conversion.Val(sItem["BLDS10_1"]) + Conversion.Val(sItem["BLDS15_1"]) + Conversion.Val(sItem["BLDS16_1"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (Conversion.Val(sItem["BLDS10_2"]) + Conversion.Val(sItem["BLDS15_2"]) + Conversion.Val(sItem["BLDS16_2"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (Conversion.Val(sItem["BLDS10_3"]) + Conversion.Val(sItem["BLDS15_3"]) + Conversion.Val(sItem["BLDS16_3"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (Conversion.Val(sItem["BLDS10_4"]) + Conversion.Val(sItem["BLDS15_4"]) + Conversion.Val(sItem["BLDS16_4"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (Conversion.Val(sItem["BLDS10_5"]) + Conversion.Val(sItem["BLDS15_5"]) + Conversion.Val(sItem["BLDS16_5"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if (mbhgs > 0)
                                {
                                    sItem["SHBLPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    sItem["SHBLPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                            }
                        }
                        else
                            sItem["SHBLPD"] = "----";
                    }
                    else
                    {
                        sItem["DRPD"] = "----";
                        sItem["BHXSPD"] = "----";
                        sItem["FSPD"] = "----";
                        sItem["SHBLPD"] = "----";
                    }
                    if (sItem["GMDPD"] == "不合格" || sItem["QDPD"] == "不合格" || sItem["WGPD"] == "不合格" || sItem["CCPD"] == "不合格" || sItem["DRPD"] == "不合格" || sItem["BHXSPD"] == "不合格" || sItem["FSPD"] == "不合格" || sItem["SHBLPD"] == "不合格")
                        mAllHg = false;
                }
            }
            #region 添加最终报告
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            MItem[0]["JCJGMS"] = "";
            if (mAllHg)
                MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            else
            {
                MItem[0]["JCJGMS"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                if (mFlag_Bhg && mFlag_Hg)
                    MItem[0]["JCJGMS"] = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            #endregion
            #endregion
        }
    }
}

