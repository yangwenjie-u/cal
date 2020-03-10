using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SZ : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_SZ_DJ"];
            var extraWCDJ = dataExtra["BZ_SZWCDJ"];
            var extraKFHDJ = dataExtra["BZ_SZKFHDJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_SZ"];

            if (!data.ContainsKey("M_SZ"))
            {
                data["M_SZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_SZ"];

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
            string mlongStr = "";
            string which = "";
            string mSjddj = "";
            string mDjMc = "";

            double mYqpjz, mXdy21, mDy21 = 0;
            bool sign = false;
            bool mSFwc = true;
            double md1, md2, md, sum, Gs, pjmd = 0;
            int xd = 0;
            int vp = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            double[] mkyqdArray = new double[10];
            double[] mkyhzArray = new double[10];
            double mMj1, mMj2, mMj3, mMj4, mMj5, mMj6, mMj7, mMj8, mMj9, mMj10;
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd;
            double mS, mBzz, mPjz;
            List<double> nArr = new List<double>();


            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
                 delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
                 {

                     mbhggs = 0;
                     jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                     mFlag_Hg = true;
                     sign = true;
                     if (jcxm.Contains("、抗压强度、"))
                     {
                         sign = true;
                         for (xd = 1; xd < 11; xd++)
                         {
                             sign = IsNumeric(sItem["KYQD" + xd]) ? sign : false;
                             if (!sign)
                             {
                                 break;
                             }
                         }
                         if (sign)
                         {
                             sum = 0;
                             nArr.Clear();
                             nArr.Add(0);
                             for (xd = 0; xd < 10; xd++)
                             {
                                 md = Double.Parse(sItem["KYQD" + xd]);
                                 nArr[xd] = md;
                                 sum += md;
                             }
                             pjmd = Math.Round(sum / 10, 2);
                             sItem["KYPJ"] = pjmd.ToString("0.00");
                             sum = 0;
                             for (xd = 0; xd < 10; xd++)
                             {
                                 md = nArr[xd] - pjmd;
                                 sum = sum + Math.Pow(md, 2);
                             }
                             md1 = Math.Sqrt(sum / 9);
                             md1 = Math.Round(md1, 2);
                             sItem["BZC"] = md1.ToString("0.00");

                             //If mItem["JYDBH >= "181100001" Then
                             //mItem["which = 1

                             sItem["QDYQ"] = "抗压强度平均值≥" + Double.Parse(mItem["G_PJZ"]).ToString("0") + "MPa。强度标准值≥" + Double.Parse(mItem["G_BZZ"]).ToString("0.0") + "MPa。";
                             md2 = md1 / pjmd;
                             md2 = Math.Round(md2, 2);
                             sItem["BYXS"] = md2.ToString("0.00");

                             nArr.Sort();
                             sItem["QDMIN"] = Math.Round(nArr[0], 1).ToString("0.0");


                             md2 = pjmd - 1.83 * md1;
                             md2 = Math.Round(md2, 1);
                             sItem["BZZ"] = md2.ToString("0.0");


                             sign = IsQualified("≥" + Double.Parse(mItem["G_PJZ"]).ToString("0"), sItem["KYPJ"]) == "合格" ? sign : false;
                             sign = IsQualified("≥" + Double.Parse(mItem["G_BZZ"]).ToString("0"), sItem["BZZ"]) == "合格" ? sign : false;

                             sItem["QDPD"] = sign ? "合格" : "不合格";

                             if (sItem["QDPD"] == "不合格")
                             {
                                 mbhggs = mbhggs + 1;
                                 mAllHg = false;
                                 mFlag_Hg = true;
                             }
                             else
                             {
                                 mFlag_Hg = true;
                             }
                         }
                         else
                         {
                             return false;
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
                         for (xd = 1; xd < 11; xd++)
                         {
                             sItem["KYQD" + xd] = "----";
                         }
                     }
                     if (jcxm.Contains("、抗风化性能、"))
                     {
                         //if (jcxm.Contains("、冻融、"))
                         //{
                         sign = true;
                         for (xd = 1; xd < 6; xd++)
                         {
                             sign = IsNumeric(sItem["DRGM" + xd]) ? sign : false;
                             if (!sign)
                             {
                                 break;
                             }
                         }

                         if (sign)
                         {
                             sum = 0;
                             nArr.Clear();
                             nArr.Add(0);
                             for (xd = 0; xd < 6; xd++)
                             {
                                 sign = sItem["SFDH" + xd] == "否" ? sign : false;
                                 sItem["DRWG" + xd] = sign ? "0" : "1";
                                 md = Double.Parse(sItem["DRGM" + xd]);
                                 sign = md <= 2.0 ? sign : false;


                             }
                             sItem["DRPD"] = sign ? "合格" : "不合格";


                             if (sItem["DRPD"] == "不合格")
                             {
                                 mbhggs = mbhggs + 1;
                                 mAllHg = false;
                                 mFlag_Hg = true;
                             }
                             else
                             {
                                 sItem["PD_KYQD"] = "合格";
                                 mFlag_Hg = true;
                             }
                         }
                         else
                         {
                             return false;
                         }
                         //}
                         //else
                         //{
                         //    for (xd = 1; xd < 6; xd++)
                         //    {
                         //        sItem["DRWG" + xd] = "----";
                         //        sItem["DRGM" + xd] = "----";
                         //    }
                         //    sItem["DRPD"] = "----";

                         //}

                         //if (jcxm.Contains("、吸水率和饱和系数、"))
                         //{
                         sign = true;
                         sign = IsNumeric(sItem["XSLPJZ"]) ? sign : false;
                         sign = IsNumeric(sItem["BHXSPJZ"]) ? sign : false;

                         if (sign)
                         {
                             sItem["XSLBHXSYQ"] = "5h沸煮吸水率平均值需" + sItem["XSLPJZYQ"].Trim() + " %，单块最大值需" + sItem["XSLZDZYQ"].Trim() + " %；饱和系数平均值需" + sItem["BHXSPJZYQ"].Trim() + "，单块最大值需" + sItem["BHXSZDZYQ"].Trim() + "。";

                             sign = IsQualified(sItem["XSLPJZYQ"], sItem["XSLPJZ"], false) == "合格" ? sign : false;
                             sign = IsQualified(sItem["XSLZDZYQ"], sItem["XSLDKZD"], false) == "合格" ? sign : false;
                             sign = IsQualified(sItem["BHXSPJZYQ"], sItem["BHXSPJZ"], false) == "合格" ? sign : false;
                             sign = IsQualified(sItem["BHXSZDZYQ"], sItem["BHXSZDZ"], false) == "合格" ? sign : false;
                             sItem["BHXSPD"] = sign ? "合格" : "不合格";


                             if (sItem["DRPD"] == "不合格")
                             {
                                 mbhggs = mbhggs + 1;
                                 mAllHg = false;
                                 mFlag_Hg = true;
                             }
                             else
                             {
                                 sItem["PD_KYQD"] = "合格";
                                 mFlag_Hg = true;
                             }
                         }
                         else
                         {
                             return false;
                         }
                         //}
                         //else
                         //{
                         //    sItem["BHXSPD"] = "----";
                         //    sItem["XSLPJZ"] = "----";
                         //    sItem["XSLDKZD"] = "----";
                         //    sItem["BHXSPJZ"] = "----";
                         //    sItem["BHXSZDZ"] = "----";
                         //    sItem["XSLBHXSYQ"] = "----";
                         //}
                     }

                     if (jcxm.Contains("、泛霜、"))
                     {
                         sign = true;

                         for (xd = 0; xd < 6; xd++)
                         {
                             sign = IsNumeric(sItem["FSCD" + xd]) ? sign : false;
                             if (!sign)
                             {
                                 break;
                             }
                         }
                         if (sign)
                         {
                             sItem["FSPD"] = "合格";

                             for (xd = 0; xd < 6; xd++)
                             {
                                 switch (sItem["FSCD" + xd].Trim())
                                 {
                                     case "轻度":
                                         sItem["FSPD"] = sItem["WGDJ"].Trim() == "优等品" ? "不合格" : sItem["FSPD"];
                                         break;
                                     case "中等":
                                         sItem["FSPD"] = sItem["WGDJ"].Trim() == "优等品" ? "不合格" : sItem["FSPD"];
                                         sItem["FSPD"] = sItem["WGDJ"].Trim() == "一等品" ? "不合格" : sItem["FSPD"];
                                         break;
                                     case "严重":
                                         sItem["FSPD"] = "不合格";
                                         break;
                                 }
                             }
                             sItem["FSPD"] = sign ? "合格" : "不合格";

                             if (sItem["DRPD"] == "不合格")
                             {
                                 mbhggs = mbhggs + 1;
                                 mAllHg = false;
                                 mFlag_Hg = true;
                             }
                             else
                             {
                                 sItem["PD_KYQD"] = "合格";
                                 mFlag_Hg = true;
                             }
                         }
                         else
                         {
                             return false;
                         }
                     }
                     else
                     {
                         for (xd = 0; xd < 6; xd++)
                         {
                             sItem["FSCD" + xd] = "----";
                         }
                         sItem["FSPD"] = "----";
                         sItem["FSYQ"] = "----";
                     }

                     if (jcxm.Contains("、石灰爆裂、"))
                     {
                         sign = true;

                         for (xd = 0; xd < 6; xd++)
                         {
                             sign = IsNumeric(sItem["BLDS10_" + xd]) ? sign : false;
                             sign = IsNumeric(sItem["BLDS15_" + xd]) ? sign : false;
                             sign = IsNumeric(sItem["BLDS16_" + xd]) ? sign : false;
                             if (!sign)
                             {
                                 break;
                             }
                         }
                         if (sign)
                         {
                             sItem["SHBLPD"] = "合格";

                             for (xd = 0; xd < 6; xd++)
                             {
                                 switch (sItem["WGDJ"].Trim())
                                 {
                                     case "优等品":
                                         sItem["FSPD"] = Conversion.Val(sItem["BLDS10_" + xd]) + Conversion.Val(sItem["BLDS15_" + xd]) + Conversion.Val(sItem["BLDS16_" + xd]) > 0 ? "不合格" : sItem["SHBLPD"];
                                         break;
                                     case "一等品":
                                         sItem["FSPD"] = Conversion.Val(sItem["BLDS10_" + xd]) > 15 || (Conversion.Val(sItem["BLDS15_" + xd]) + Conversion.Val(sItem["BLDS16_" + xd])) > 0 ? "不合格" : sItem["SHBLPD"];

                                         break;
                                     case "合格品":
                                         sItem["FSPD"] = (Conversion.Val(sItem["BLDS10_" + xd]) + Conversion.Val(sItem["BLDS15_" + xd])) > 15 || Conversion.Val(sItem["BLDS16_" + xd]) > 0 ? "不合格" : sItem["SHBLPD"];

                                         break;
                                 }
                             }
                             sItem["FSPD"] = sign ? "合格" : "不合格";

                             if (sItem["DRPD"] == "不合格")
                             {
                                 mbhggs = mbhggs + 1;
                                 mAllHg = false;
                                 mFlag_Hg = true;
                             }
                             else
                             {
                                 sItem["PD_KYQD"] = "合格";
                                 mFlag_Hg = true;
                             }
                         }
                         else
                         {
                             return false;
                         }
                     }
                     else
                     {
                         for (xd = 0; xd < 6; xd++)
                         {
                             sItem["BLDS10_" + xd] = "----";
                             sItem["BLDS16_" + xd] = "----";
                             sItem["BLDS15_" + xd] = "----";
                         }
                         sItem["SHBLPD"] = "----";
                         sItem["SHBLYQ"] = "----";

                     }

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
                         if (mFlag_Bhg && mFlag_Hg)
                         {
                             jsbeizhu = "该组试件所检项目部分符合" + mItem["PDBZ"] + "标准要求。";

                         }
                     }

                     return mAllHg;
                 };

            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mSjdj = string.IsNullOrEmpty(sItem["SJDJ"]) ? "" : sItem["SJDJ"];

                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj.Trim() && u["ZLB"].Contains(sItem["ZLB"]));
                if (mrsDj == null)
                {
                    mYqpjz = 0;
                    mXdy21 = 0;
                    mDy21 = 0;
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    continue;
                }
                else
                {
                    mYqpjz = Double.Parse(mrsDj["PJZ"]);
                    mXdy21 = Double.Parse(mrsDj["XDY21"]);
                    mDy21 = Double.Parse(mrsDj["DY21"]);
                    MItem[0]["G_PJZ"] = (mYqpjz).ToString();
                    MItem[0]["G_BZZ"] = (mXdy21).ToString();
                    MItem[0]["G_MIN"] = (mDy21).ToString();
                    which = mrsDj["WHICH"];
                    //MItem[0]["BGNAME"] = mrsDj["BGNAME"].ToString();
                    mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].ToLower();
                }

                var mrsKfhdj = extraKFHDJ.FirstOrDefault(u => u["MC"] == sItem["ZZL"] && u["PZ"] == sItem["WGDJ"]);
                if (mrsDj == null)
                {
                    sItem["DRYQ"] = "每块砖样不允许出现裂纹、分层、掉皮、缺棱、掉角等冻坏现象；质量损失不得大于2%。";
                }
                else
                {
                    sItem["XSLPJZYQ"] = mrsKfhdj["XSLPJ"];
                    sItem["XSLZDZYQ"] = mrsKfhdj["XSLDKZD"];
                    sItem["BHXSPJZYQ"] = mrsKfhdj["BHXSPJ"];
                    sItem["BHXSZDZYQ"] = mrsKfhdj["BHXSZDZ"];
                    sItem["FSYQ"] = mrsKfhdj["FSYQ"];
                    sItem["SHBLYQ"] = mrsKfhdj["DHBLYQ"];
                    sItem["DRYQ"] = mrsKfhdj["DRYQ"];
                }
                sItem["LQ"] = (GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime(sItem["ZZRQ"])).Days.ToString();


                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        if ((Conversion.Val(sItem["CD1_1"])) != 0)
                        {
                            sItem["CD1"] = Round((Conversion.Val(sItem["CD1_1"]) + Conversion.Val(sItem["CD1_2"])) / 2, 0).ToString();
                            sItem["CD2"] = Round((Conversion.Val(sItem["CD2_1"]) + Conversion.Val(sItem["CD2_2"])) / 2, 0).ToString();
                            sItem["CD3"] = Round((Conversion.Val(sItem["CD3_1"]) + Conversion.Val(sItem["CD3_2"])) / 2, 0).ToString();
                            sItem["CD4"] = Round((Conversion.Val(sItem["CD4_1"]) + Conversion.Val(sItem["CD4_2"])) / 2, 0).ToString();
                            sItem["CD5"] = Round((Conversion.Val(sItem["CD5_1"]) + Conversion.Val(sItem["CD5_2"])) / 2, 0).ToString();
                            sItem["CD6"] = Round((Conversion.Val(sItem["CD6_1"]) + Conversion.Val(sItem["CD6_2"])) / 2, 0).ToString();
                            sItem["CD7"] = Round((Conversion.Val(sItem["CD7_1"]) + Conversion.Val(sItem["CD7_2"])) / 2, 0).ToString();
                            sItem["CD8"] = Round((Conversion.Val(sItem["CD8_1"]) + Conversion.Val(sItem["CD8_2"])) / 2, 0).ToString();
                            sItem["CD9"] = Round((Conversion.Val(sItem["CD9_1"]) + Conversion.Val(sItem["CD9_2"])) / 2, 0).ToString();
                            sItem["CD10"] = Round((Conversion.Val(sItem["CD10_1"]) + Conversion.Val(sItem["CD10_2"])) / 2, 0).ToString();
                            sItem["KD1"] = Round((Conversion.Val(sItem["KD1_1"]) + Conversion.Val(sItem["KD1_2"])) / 2, 0).ToString();
                            sItem["KD2"] = Round((Conversion.Val(sItem["KD2_1"]) + Conversion.Val(sItem["KD2_2"])) / 2, 0).ToString();
                            sItem["KD3"] = Round((Conversion.Val(sItem["KD3_1"]) + Conversion.Val(sItem["KD3_2"])) / 2, 0).ToString();
                            sItem["KD4"] = Round((Conversion.Val(sItem["KD4_1"]) + Conversion.Val(sItem["KD4_2"])) / 2, 0).ToString();
                            sItem["KD5"] = Round((Conversion.Val(sItem["KD5_1"]) + Conversion.Val(sItem["KD5_2"])) / 2, 0).ToString();
                            sItem["KD6"] = Round((Conversion.Val(sItem["KD6_1"]) + Conversion.Val(sItem["KD6_2"])) / 2, 0).ToString();
                            sItem["KD7"] = Round((Conversion.Val(sItem["KD7_1"]) + Conversion.Val(sItem["KD7_2"])) / 2, 0).ToString();
                            sItem["KD8"] = Round((Conversion.Val(sItem["KD8_1"]) + Conversion.Val(sItem["KD8_2"])) / 2, 0).ToString();
                            sItem["KD9"] = Round((Conversion.Val(sItem["KD9_1"]) + Conversion.Val(sItem["KD9_2"])) / 2, 0).ToString();
                            sItem["KD10"] = Round((Conversion.Val(sItem["KD10_1"]) + Conversion.Val(sItem["KD10_2"])) / 2, 0).ToString();
                        }
                        else
                            mSFwc = false;
                        mMj1 = Conversion.Val(sItem["CD1"]) * Conversion.Val(sItem["KD1"]);
                        mMj2 = Conversion.Val(sItem["CD2"]) * Conversion.Val(sItem["KD2"]);
                        mMj3 = Conversion.Val(sItem["CD3"]) * Conversion.Val(sItem["KD3"]);
                        mMj4 = Conversion.Val(sItem["CD4"]) * Conversion.Val(sItem["KD4"]);
                        mMj5 = Conversion.Val(sItem["CD5"]) * Conversion.Val(sItem["KD5"]);
                        mMj6 = Conversion.Val(sItem["CD6"]) * Conversion.Val(sItem["KD6"]);
                        mMj7 = Conversion.Val(sItem["CD7"]) * Conversion.Val(sItem["KD7"]);
                        mMj8 = Conversion.Val(sItem["CD8"]) * Conversion.Val(sItem["KD8"]);
                        mMj9 = Conversion.Val(sItem["CD9"]) * Conversion.Val(sItem["KD9"]);
                        mMj10 = Conversion.Val(sItem["CD10"]) * Conversion.Val(sItem["KD10"]);

                        sItem["MJ1"] = mMj1.ToString();
                        sItem["MJ2"] = mMj2.ToString();
                        sItem["MJ3"] = mMj3.ToString();
                        sItem["MJ4"] = mMj4.ToString();
                        sItem["MJ5"] = mMj5.ToString();
                        sItem["MJ6"] = mMj6.ToString();
                        sItem["MJ7"] = mMj7.ToString();
                        sItem["MJ8"] = mMj8.ToString();
                        sItem["MJ9"] = mMj9.ToString();
                        sItem["MJ10"] = mMj10.ToString();
                        if (sItem["ZLB"].Contains("混凝土多孔砖"))
                        {
                            if (mMj1 != 0)
                                sItem["KYQD1"] = Round(1000 * (Conversion.Val(sItem["KYHZ1"])) / mMj1, 1).ToString();
                            else
                                sItem["KYQD1"] = "0";
                            if (mMj2 != 0)
                                sItem["KYQD2"] = Round(1000 * Conversion.Val(sItem["KYHZ2"]) / mMj2, 1).ToString();
                            else
                                sItem["KYQD2"] = "0";
                            if (mMj3 != 0)
                                sItem["KYQD3"] = Round(1000 * Conversion.Val(sItem["KYHZ3"]) / mMj3, 1).ToString();
                            else
                                sItem["KYQD3"] = "0";
                            if (mMj4 != 0)
                                sItem["KYQD4"] = Round(1000 * Conversion.Val(sItem["KYHZ4"]) / mMj4, 1).ToString();
                            else
                                sItem["KYQD4"] = "0";
                            if (mMj5 != 0)
                                sItem["KYQD5"] = Round(1000 * (Conversion.Val(sItem["KYHZ5"])) / mMj5, 1).ToString();
                            else
                                sItem["KYQD5"] = "0";
                            if (mMj6 != 0)
                                sItem["KYQD6"] = Round(1000 * Conversion.Val(sItem["KYHZ6"]) / mMj6, 1).ToString();
                            else
                                sItem["KYQD6"] = "0";
                            if (mMj7 != 0)
                                sItem["KYQD7"] = Round(1000 * Conversion.Val(sItem["KYHZ7"]) / mMj7, 1).ToString();
                            else
                                sItem["KYQD7"] = "0";
                            if (mMj8 != 0)
                                sItem["KYQD8"] = Round(1000 * Conversion.Val(sItem["KYHZ8"]) / mMj8, 1).ToString();
                            else
                                sItem["KYQD8"] = "0";
                            if (mMj9 != 0)
                                sItem["KYQD9"] = Round(1000 * Conversion.Val(sItem["KYHZ9"]) / mMj9, 1).ToString();
                            else
                                sItem["KYQD9"] = "0";
                            if (mMj10 != 0)
                                sItem["KYQD10"] = Round(1000 * Conversion.Val(sItem["KYHZ10"]) / mMj10, 1).ToString();
                            else
                                sItem["KYQD10"] = "0";
                        }
                        else
                        {
                            if (mMj1 != 0)
                                sItem["KYQD1"] = Round(1000 * Conversion.Val(sItem["KYHZ1"]) / mMj1, 2).ToString();
                            else
                                sItem["KYQD1"] = "0";
                            if (mMj2 != 0)
                                sItem["KYQD2"] = Round(1000 * Conversion.Val(sItem["KYHZ2"]) / mMj2, 2).ToString();
                            else
                                sItem["KYQD2"] = "0";
                            if (mMj3 != 0)
                                sItem["KYQD3"] = Round(1000 * Conversion.Val(sItem["KYHZ3"]) / mMj3, 2).ToString();
                            else
                                sItem["KYQD3"] = "0";
                            if (mMj4 != 0)
                                sItem["KYQD4"] = Round(1000 * Conversion.Val(sItem["KYHZ4"]) / mMj4, 2).ToString();
                            else
                                sItem["KYQD4"] = "0";
                            if (mMj5 != 0)
                                sItem["KYQD5"] = Round(1000 * Conversion.Val(sItem["KYHZ5"]) / mMj5, 2).ToString();
                            else
                                sItem["KYQD5"] = "0";
                            if (mMj6 != 0)
                                sItem["KYQD6"] = Round(1000 * Conversion.Val(sItem["KYHZ6"]) / mMj6, 2).ToString();
                            else
                                sItem["KYQD6"] = "0";
                            if (mMj7 != 0)
                                sItem["KYQD7"] = Round(1000 * Conversion.Val(sItem["KYHZ7"]) / mMj7, 2).ToString();
                            else
                                sItem["KYQD7"] = "0";
                            if (mMj8 != 0)
                                sItem["KYQD8"] = Round(1000 * Conversion.Val(sItem["KYHZ8"]) / mMj8, 2).ToString();
                            else
                                sItem["KYQD8"] = "0";
                            if (mMj9 != 0)
                                sItem["KYQD9"] = Round(1000 * Conversion.Val(sItem["KYHZ9"]) / mMj9, 2).ToString();
                            else
                                sItem["KYQD9"] = "0";
                            if (mMj10 != 0)
                                sItem["KYQD10"] = Round(1000 * Conversion.Val(sItem["KYHZ10"]) / mMj10, 2).ToString();
                            else
                                sItem["KYQD10"] = "0";
                        }
                        //抗压平均值
                        mPjz = Round((Conversion.Val(sItem["KYQD1"]) + Conversion.Val(sItem["KYQD2"]) + Conversion.Val(sItem["KYQD3"]) + Conversion.Val(sItem["KYQD4"]) + Conversion.Val(sItem["KYQD5"]) +
                           Conversion.Val(sItem["KYQD6"]) + Conversion.Val(sItem["KYQD7"]) + Conversion.Val(sItem["KYQD8"]) + Conversion.Val(sItem["KYQD9"]) + Conversion.Val(sItem["KYQD10"])) / 10, 2);
                        //均方差
                        mS = Math.Sqrt(((Conversion.Val(sItem["KYQD1"]) - mPjz) * (Conversion.Val(sItem["KYQD1"]) - mPjz) +
                            (Conversion.Val(sItem["KYQD2"]) - mPjz) * (Conversion.Val(sItem["KYQD2"]) - mPjz) +
                            (Conversion.Val(sItem["KYQD3"]) - mPjz) * (Conversion.Val(sItem["KYQD3"]) - mPjz) +
                            (Conversion.Val(sItem["KYQD4"]) - mPjz) * (Conversion.Val(sItem["KYQD4"]) - mPjz) +
                            (Conversion.Val(sItem["KYQD5"]) - mPjz) * (Conversion.Val(sItem["KYQD5"]) - mPjz) +
                            (Conversion.Val(sItem["KYQD6"]) - mPjz) * (Conversion.Val(sItem["KYQD6"]) - mPjz) +
                            (Conversion.Val(sItem["KYQD7"]) - mPjz) * (Conversion.Val(sItem["KYQD7"]) - mPjz) +
                            (Conversion.Val(sItem["KYQD8"]) - mPjz) * (Conversion.Val(sItem["KYQD8"]) - mPjz) +
                            (Conversion.Val(sItem["KYQD9"]) - mPjz) * (Conversion.Val(sItem["KYQD9"]) - mPjz) +
                            (Conversion.Val(sItem["KYQD10"]) - mPjz) * (Conversion.Val(sItem["KYQD10"]) - mPjz)) / 9);
                        mS = Round(mS, 2);
                        sItem["BZC"] = Round(mS, 2).ToString();
                        sItem["KYPJ"] = Round(mPjz, 2).ToString();


                        MItem[0]["G_PJZ"] = mYqpjz.ToString();
                        MItem[0]["G_BZZ"] = mXdy21.ToString();
                        MItem[0]["G_MIN"] = mDy21.ToString();
                        sItem["QDYQ"] = "抗压强度平均值需≥" + Conversion.Val(MItem[0]["G_PJZ"]).ToString("0").Trim() + "MPa。当变异系数δ≤0.21时，强度标准值需≥" + Conversion.Val(MItem[0]["G_BZZ"]).ToString("0.0").Trim() + "MPa，当变异系数δ＞0.21时，单块最小强度值需≥" + Conversion.Val(MItem[0]["G_MIN"]).ToString("0.0").Trim() + "MPa。";
                        //变异系数
                        if (mPjz != 0)
                            sItem["BYXS"] = Round(mS / mPjz, 2).ToString();
                        //标准值计算、判定，平均值判定，单组合格判定
                        mlongStr = sItem["KYQD1"] + "," +
                                sItem["KYQD2"] + "," +
                                sItem["KYQD3"] + "," +
                                sItem["KYQD4"] + "," +
                                sItem["KYQD5"] + "," +
                                sItem["KYQD6"] + "," +
                                sItem["KYQD7"] + "," +
                                sItem["KYQD8"] + "," +
                                sItem["KYQD9"] + "," +
                                sItem["KYQD10"];
                        var mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 9; vp++)
                            mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkyhzArray);
                        mMaxKyqd = mkyhzArray[9];
                        if (sItem["ZLB"].Contains("烧结普通砖") || sItem["ZLB"].Contains("混凝土普通砖"))
                            mMinKyqd = Round(mkyhzArray[0], 1);
                        else
                            mMinKyqd = Round(mkyhzArray[0], 1);
                        sItem["QDMIN"] = Round(mMinKyqd, 1).ToString();
                        mBzz = Round(mPjz - 1.8 * mS, 1);
                        sItem["BZZ"] = Round(mBzz, 1).ToString();
                        if (which == "1" || which == "11")
                        {
                            if (Conversion.Val(sItem["KYPJ"]) >= mYqpjz && mMinKyqd >= mDy21)
                            {
                                sItem["QDPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["QDPD"] = "不合格";
                                mFlag_Bhg = true;
                            }
                            //实际达到设计等级判定
                            var mrsDj_where = extraDJ.Where(x => x["MC"].Contains(mrsDj["MC"].Substring(0, mrsDj["MC"].Trim().Length - 2)));
                            mSjddj = "";
                            foreach (var item in mrsDj_where)
                            {
                                mDjMc = item["MC"];
                                mYqpjz = GetSafeDouble(item["PJZ"]);
                                mDy21 = GetSafeDouble(item["DY21"]);
                                if (Conversion.Val(sItem["KYPJ"]) >= mYqpjz && mMinKyqd >= mDy21)
                                    mSjddj = mDjMc;
                            }
                            sItem["SJDDJ"] = mSjddj.Trim();
                        }
                        else
                        {
                            if (Conversion.Val(sItem["BYXS"]) <= 0.21)
                            {
                                //一般合格判定
                                if (mBzz < mXdy21)
                                    sItem["BZZ_HG"] = "0";
                                else
                                    sItem["BZZ_HG"] = "1";
                                if (Conversion.Val(sItem["KYPJ"]) < mYqpjz)
                                    sItem["PJZ_HG"] = "0";
                                else
                                    sItem["PJZ_HG"] = "1";
                                if (Conversion.Val(sItem["KYPJ"]) >= mYqpjz && mBzz >= mXdy21)
                                {
                                    sItem["QDPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    sItem["QDPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                //报表TAG单元
                                sItem["TAG1"] = Round(mPjz, 1).ToString();
                                sItem["TAG2"] = "强度\r\n标准值";
                                if (sItem["BZZ_HG"] == "1")
                                {
                                    sItem["TAG3"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    sItem["TAG3"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                sItem["TAG4"] = "标准值\r\n" + "s\r\n" + Round(mBzz, 1).ToString();
                                //报表TAG单元
                                sItem["TAG_1"] = Round(mBzz, 1).ToString();
                                sItem["TAG_2"] = Round(mMinKyqd, 1).ToString();


                                //实际达到设计等级判定
                                var mrsDj_where = extraDJ.Where(u => u["ZLB"].Contains(sItem["ZLB"].Trim()));
                                foreach (var item in mrsDj_where)
                                {
                                    mDjMc = item["MC"];
                                    mYqpjz = GetSafeDouble(item["PJZ"]);
                                    mXdy21 = GetSafeDouble(item["XDY21"]);
                                    mDy21 = GetSafeDouble(item["DY21"]);
                                    if (Conversion.Val(sItem["KYPJ"]) >= mYqpjz && mBzz >= mXdy21)
                                        mSjddj = mDjMc;
                                }
                                sItem["SJDDJ"] = mSjddj.Trim();

                            }
                            else
                            {
                                //一般合格判定
                                sItem["KYPJ"] = Round(mPjz, 1).ToString();
                                if (mMinKyqd < mDy21)
                                    sItem["MIN_HG"] = "0";
                                else
                                    sItem["MIN_HG"] = "1";
                                if (Conversion.Val(sItem["KYPJ"]) < mYqpjz)
                                    sItem["PJZ_HG"] = "0";
                                else
                                    sItem["PJZ_HG"] = "1";
                                if (Conversion.Val(sItem["KYPJ"]) >= mYqpjz && mMinKyqd >= mDy21)
                                {
                                    sItem["QDPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    sItem["QDPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                //报表TAG单元
                                if (sItem["MIN_HG"] == "0")
                                    sItem["TAG1"] = "单块最小值不符合设计要求";
                                else
                                    sItem["TAG1"] = Round(mPjz, 1).ToString();
                                sItem["TAG2"] = "单块最小值";
                                if (sItem["MIN_HG"] == "1")
                                {
                                    sItem["TAG3"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    sItem["TAG3"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                sItem["TAG4"] = "";
                                //报表TAG单元
                                sItem["TAG_1"] = Round(mBzz, 1).ToString();
                                sItem["TAG_2"] = Round(mMinKyqd, 1).ToString();
                                //实际达到设计等级判定
                                var mrsDj_where = extraDJ.Where(x => x["ZLB"].Contains(sItem["ZLB"].Trim()));
                                mSjddj = "";
                                foreach (var item in mrsDj_where)
                                {
                                    mDjMc = item["MC"];
                                    mYqpjz = GetSafeDouble(item["PJZ"]);
                                    mXdy21 = GetSafeDouble(item["XDY21"]);
                                    mDy21 = GetSafeDouble(item["DY21"]);
                                    if (Conversion.Val(sItem["KYPJ"]) >= mYqpjz && mMinKyqd >= mDy21)
                                        mSjddj = mDjMc;
                                }
                                sItem["SJDDJ"] = mSjddj.Trim();
                            }
                        }
                    }
                    else
                        sItem["QDPD"] = "----";
                    if (jcxm.Contains("、尺寸、"))
                    {
                        if (GetSafeDouble(MItem[0]["CCJC"]) != 0)
                        {
                            MItem[0]["G_PJPC"] = extraWCDJ[0]["PJPC"];
                            MItem[0]["G_CCJC"] = extraWCDJ[0]["CCJC"];
                            if (Math.Abs(GetSafeDouble(MItem[0]["PJPC"])) <= GetSafeDouble(extraWCDJ[0]["PJPC"]) && GetSafeDouble(MItem[0]["CCJC"]) <= GetSafeDouble(extraWCDJ[0]["CCJC"]))
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
                            mSFwc = false;
                    }
                    else
                        sItem["CCPD"] = "----";
                    if (jcxm.Contains("、外观、"))
                    {
                        if (GetSafeDouble(MItem[0]["WGBHGS"]) <= GetSafeDouble(extraWCDJ[0]["WYBHGS"]))
                        {
                            MItem[0]["WGPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            if (GetSafeDouble(MItem[0]["WGBHGS"]) >= GetSafeDouble(extraWCDJ[0]["WBYBHGS"]))
                            {
                                sItem["WGPD"] = "不合格";
                                mFlag_Bhg = true;
                            }
                            else
                            {
                                if (GetSafeDouble(MItem[0]["WGBHGS"]) + GetSafeDouble(MItem[0]["WGBHGS2"]) <= GetSafeDouble(extraWCDJ[0]["WYBHGS2"]))
                                {
                                    sItem["WGPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                                if (GetSafeDouble(MItem[0]["WGBHGS"]) + GetSafeDouble(MItem[0]["WGBHGS2"]) > GetSafeDouble(extraWCDJ[0]["WBYBHGS2"]))
                                {
                                    sItem["WGPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                            }
                        }
                    }
                    else
                        sItem["WGPD"] = "----";
                    if (jcxm.Contains("、冻融、"))
                    {
                        if (Conversion.Val(sItem["DRG0_1"]) != 0)
                        {
                            sItem["DRGM1"] = Round((Conversion.Val(sItem["DRG0_1"]) - Conversion.Val(sItem["DRG1_1"])) / Conversion.Val(sItem["DRG0_1"]) * 100, 1).ToString();
                            sItem["DRGM2"] = Round((Conversion.Val(sItem["DRG0_2"]) - Conversion.Val(sItem["DRG1_2"])) / Conversion.Val(sItem["DRG0_2"]) * 100, 1).ToString();
                            sItem["DRGM3"] = Round((Conversion.Val(sItem["DRG0_3"]) - Conversion.Val(sItem["DRG1_3"])) / Conversion.Val(sItem["DRG0_3"]) * 100, 1).ToString();
                            sItem["DRGM4"] = Round((Conversion.Val(sItem["DRG0_4"]) - Conversion.Val(sItem["DRG1_4"])) / Conversion.Val(sItem["DRG0_4"]) * 100, 1).ToString();
                            sItem["DRGM5"] = Round((Conversion.Val(sItem["DRG0_5"]) - Conversion.Val(sItem["DRG1_5"])) / Conversion.Val(sItem["DRG0_5"]) * 100, 1).ToString();
                            if ((Conversion.Val(sItem["DRGM1"])) <= 2 && (Conversion.Val(sItem["DRGM2"])) <= 2 && (Conversion.Val(sItem["DRGM3"])) <= 2 && (Conversion.Val(sItem["DRGM4"])) <= 2 && (Conversion.Val(sItem["DRGM5"])) <= 2)
                            {
                                if (sItem["DRWG1"].Trim() == "1" || sItem["DRWG2"].Trim() == "1" || sItem["DRWG3"].Trim() == "1" || sItem["DRWG4"].Trim() == "1" || sItem["DRWG5"].Trim() == "1")
                                {
                                    sItem["DRPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    sItem["DRPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                            }
                            else
                            {
                                sItem["DRPD"] = "不合格";
                                mFlag_Bhg = true;
                            }
                            sItem["SYR"] = MItem[0]["SYR"];
                        }
                        else
                        {
                            if (sItem["QDPD"] == "合格")
                            {
                                mSFwc = false;
                            }
                            else
                                sItem["DRPD"] = "----";

                        }
                        if (jcxm.Contains("、吸水率和饱和系数、"))
                        {
                            if (Conversion.Val(sItem["BHXSG0_1"]) != 0)
                            {
                                sItem["XSLBHXSYQ"] = "5h沸煮吸水率平均值需" + sItem["XSLPJZYQ"].Trim() + "%，单块最大值需" + sItem["XSLZDZYQ"].Trim() + "%；饱和系数平均值需" + sItem["BHXSPJZYQ"].Trim() + "%，单块最大值需" + sItem["BHXSZDZYQ"].Trim() + "%。";
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
                                mxsl1 = Round((Conversion.Val(sItem["BHXSG24_1"]) - Conversion.Val(sItem["BHXSG0_1"])) / (Conversion.Val(sItem["BHXSG5_1"]) - Conversion.Val(sItem["BHXSG0_1"])), 3);
                                mxsl2 = Round((Conversion.Val(sItem["BHXSG24_2"]) - Conversion.Val(sItem["BHXSG0_2"])) / (Conversion.Val(sItem["BHXSG5_2"]) - Conversion.Val(sItem["BHXSG0_2"])), 3);
                                mxsl3 = Round((Conversion.Val(sItem["BHXSG24_3"]) - Conversion.Val(sItem["BHXSG0_3"])) / (Conversion.Val(sItem["BHXSG5_3"]) - Conversion.Val(sItem["BHXSG0_3"])), 3);
                                mxsl4 = Round((Conversion.Val(sItem["BHXSG24_4"]) - Conversion.Val(sItem["BHXSG0_4"])) / (Conversion.Val(sItem["BHXSG5_4"]) - Conversion.Val(sItem["BHXSG0_4"])), 3);
                                mxsl5 = Round((Conversion.Val(sItem["BHXSG24_5"]) - Conversion.Val(sItem["BHXSG0_5"])) / (Conversion.Val(sItem["BHXSG5_5"]) - Conversion.Val(sItem["BHXSG0_5"])), 3);
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
                                if (IsQualified(sItem["XSLPJZYQ"], sItem["XSLPJZ"]) == "不符合" || IsQualified(sItem["XSLZDZYQ"], sItem["XSLDKZD"]) == "不符合" || IsQualified(sItem["BHXSPJZYQ"], sItem["BHXSPJZ"]) == "不符合" || IsQualified(sItem["BHXSZDZYQ"], sItem["BHXSZDZ"]) == "不符合")
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
                                mSFwc = false;
                        }
                        else
                            sItem["BHXSPD"] = "----";
                        if (jcxm.Contains("、泛霜、"))
                        {
                            if (sItem["FSCD1"].Trim() == "----" || sItem["FSCD1"].Trim() == "")
                                mSFwc = false;
                            else
                            {
                                double mfscnt = 0;
                                double mfscnt1 = 0;
                                double mfscnt2 = 0;
                                if (sItem["FSCD1"].Trim() == "轻度")
                                    mfscnt = mfscnt + 1;

                                if (sItem["FSCD2"].Trim() == "轻度")
                                    mfscnt = mfscnt + 1;
                                if (sItem["FSCD3"].Trim() == "轻度")
                                    mfscnt = mfscnt + 1;
                                if (sItem["FSCD4"].Trim() == "轻度")
                                    mfscnt = mfscnt + 1;
                                if (sItem["FSCD5"].Trim() == "轻度")
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
                                if (sItem["FSCD3"] == "严重")
                                    mfscnt2 = mfscnt2 + 1;
                                if (sItem["FSCD4"] == "严重")
                                    mfscnt2 = mfscnt2 + 1;
                                if (sItem["FSCD5"] == "严重")
                                    mfscnt2 = mfscnt2 + 1;
                                if (sItem["WGDJ"].Trim() == "合格品")
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
                                if (sItem["WGDJ"].Trim() == "一等品")
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
                                if (sItem["WGDJ"].Trim() == "优等品")
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

                        }
                        else
                            sItem["FSPD"] = "----";
                        if (jcxm.Contains("、石灰爆裂、"))
                        {
                            int mbhgs = 0;
                            if (sItem["WGDJ"].Trim() == "合格品")
                            {
                                if ((Conversion.Val(sItem["BLDS10_1"]) + Conversion.Val(sItem["BLDS15_1"])) <= 15 && Conversion.Val(sItem["BLDS15_1"]) <= 7 && Conversion.Val(sItem["BLDS16_1"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_2"]) + Conversion.Val(sItem["BLDS15_2"])) <= 15 && Conversion.Val(sItem["BLDS15_2"]) <= 7 && Conversion.Val(sItem["BLDS16_2"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_3"]) + Conversion.Val(sItem["BLDS15_3"])) <= 15 && Conversion.Val(sItem["BLDS15_3"]) <= 7 && Conversion.Val(sItem["BLDS16_3"]) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_4"]) + Conversion.Val(sItem["BLDS15_4"])) <= 15 && Conversion.Val(sItem["BLDS15_4"]) <= 7 && Conversion.Val(sItem["BLDS16_4"]) == 0)
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
                                if ((Conversion.Val(sItem["BLDS10_1"])) <= 15 && ((Conversion.Val(sItem["BLDS15_1"])) + (Conversion.Val(sItem["BLDS16_1"])) == 0))
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_2"])) <= 15 && (Conversion.Val(sItem["BLDS15_2"]) + Conversion.Val(sItem["BLDS16_2"])) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_3"])) <= 15 && ((Conversion.Val(sItem["BLDS15_3"])) + (Conversion.Val(sItem["BLDS16_3"])) == 0))
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_4"])) <= 15 && ((Conversion.Val(sItem["BLDS15_4"])) + (Conversion.Val(sItem["BLDS16_4"])) == 0))
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_5"])) <= 15 && ((Conversion.Val(sItem["BLDS15_5"])) + (Conversion.Val(sItem["BLDS16_5"])) == 0))
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
                            if (sItem["WGDJ"].Trim() == "优等品")
                            {
                                mbhgs = 0;
                                if ((Conversion.Val(sItem["BLDS10_1"])) + (Conversion.Val(sItem["BLDS15_1"])) + (Conversion.Val(sItem["BLDS16_1"])) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_2"])) + (Conversion.Val(sItem["BLDS15_2"])) + (Conversion.Val(sItem["BLDS16_2"])) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_3"])) + (Conversion.Val(sItem["BLDS15_3"])) + (Conversion.Val(sItem["BLDS16_3"])) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_4"])) + (Conversion.Val(sItem["BLDS15_4"])) + (Conversion.Val(sItem["BLDS16_4"])) == 0)
                                { }
                                else
                                    mbhgs = mbhgs + 1;
                                if ((Conversion.Val(sItem["BLDS10_5"])) + (Conversion.Val(sItem["BLDS15_5"])) + (Conversion.Val(sItem["BLDS16_5"])) == 0)
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
                    if (sItem["QDPD"] == "不合格" || sItem["WGPD"] == "不合格" || sItem["CCPD"] == "不合格" || sItem["DRPD"] == "不合格" || sItem["BHXSPD"] == "不合格" || sItem["FSPD"] == "不合格" || sItem["SHBLPD"] == "不合格")
                    {
                        sItem["JCJG"] = "不合格";
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        sItem["JCJG"] = "合格";
                        mFlag_Hg = true;
                    }
                    mAllHg = (mAllHg && (sItem["JCJG"] == "合格"));
                }
            }

            #region 添加最终报告
            //主表总判断赋值
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

