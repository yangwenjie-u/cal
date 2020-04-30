using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class MMJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_MMJ_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_MMJ"];

            if (!data.ContainsKey("M_MMJ"))
            {
                data["M_MMJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_MMJ"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";

            #region 局部函数
            Func<double, double> myint = delegate (double dataChar)
            {
                return Math.Round(Conversion.Val(dataChar) / 5, 0) * 5;
            };
            #endregion


            double g_Qd1, g_Qd2, g_Qd3, g_Qd4, g_Qd5, g_Qd6,g_Qd7, yZbbz = 0;
            bool sign = true;
            bool sign1 = true;

            bool flag = true;
            bool mSFwc = true;
            List<double> nArr = new List<double>();
            double xd, Gs = 0;
            var mJSFF = "";
            var mbhggs = 0;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;
            var jcxmBhg = "";
            var jcxmCur = "";

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
             delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
             {
                 jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                 mbhggs = 0;
                 mFlag_Hg = false;
                 mFlag_Bhg = false;


                 sign = false;
                 sign1 = mItem["YCQK"].Contains("破坏") ? false : true;

                 if (jcxm.Contains("、拉伸粘结强度(干燥状态)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD1"]) == 0)
                     {
                         return false;
                     }
                     var PHJM1 = sItem["PHJM1"].Trim();
                     if (PHJM1 == "EPS板破坏")
                     {
                         mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);
                     }
                     else
                     {
                         mItem["HG_QD1"] = "不合格";
                     }

                 }

                 if (jcxm.Contains("、压剪粘结强度(原强度)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD1"]) == 0)
                     {
                         return false;
                     }

                     mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);

                 }

                 //if (jcxm.Contains("、拉伸粘结强度(原强度)、") && sign1)
                 if (jcxm.Contains("、拉伸粘结原强度、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD1"]) == 0)
                     {
                         return false;
                     }

                     var PHJM1 = sItem["PHJM1"].Trim();
                     if (PHJM1 == "EPS板破坏")
                     {
                         mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);
                     }
                     else
                     {
                         mItem["HG_QD1"] = "不合格";
                     }

                     mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);

                 }

                 if (jcxm.Contains("、拉伸粘结强度(常温28d)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD1"]) == 0)
                     {
                         return false;
                     }

                     mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);
                 }

                 //mItem[0]["WHICH") = IIf(sign1, "bgmmj", "bgmmj_10")
                 if (sign)
                 {
                     mbhggs = mItem["HG_QD1"] == "不合格" ? mbhggs + 1 : mbhggs;
                 }
                 else
                 {
                     if (sign1)
                     {
                         sItem["QD1"] = "----";
                         sItem["PHJM1"] = "----";
                         sItem["PHJMPD1"] = "----";
                         mItem["HG_QD1"] = "----";
                         mItem["G_QD1"] = "----";
                         mItem["G_PHJM1"] = "----";
                     }
                 }

                 if (!sign1)
                 {
                     mItem["HG_QD1"] = "不合格";
                     mbhggs = mbhggs + 1;
                 }

                 //mItem[0]["YCQK") Like "*破坏*", false, true)
                 sign = false;
                 sign1 = mItem["YCQK"].Contains("破坏") ? false : true;

                 if (jcxm.Contains("、拉伸粘结强度(浸水48h后)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD2"]) == 0)
                     {
                         return false;
                     }
                     else
                     {

                         if ("EPS板破坏" == sItem["PHJM2"])
                         {
                             mItem["HG_QD2"] = IsQualified(mItem["G_QD2"], sItem["QD2"], false);
                         }
                         else
                         {
                             mItem["HG_QD2"] = "不合格";
                         }
                     }
                 }


                 //if (jcxm.Contains("、拉伸粘结强度(耐水)、") && sign1)
                 if (jcxm.Contains("、拉伸粘结耐水强度、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD2"]) == 0)
                     {
                         return false;
                     }
                     else
                     {
                         mItem["HG_QD2"] = IsQualified(mItem["G_QD2"], sItem["QD2"], false);
                     }
                 }

                 //if (jcxm.Contains("、拉伸粘结强度(耐水)、") && sign1)
                 if (jcxm.Contains("、拉伸粘结耐水强度、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD2"]) == 0)
                     {
                         return false;
                     }
                     else
                     {
                         if (sItem["PHJM2"] == "EPS板破坏")
                         {
                             mItem["HG_QD2"] = IsQualified(mItem["G_QD2"], sItem["QD2"], false);
                         }
                         else
                         {
                             mItem["HG_QD2"] = "不合格";
                         }
                     }
                 }

                 if (jcxm.Contains("、浸水拉伸粘结强度(常温28d，浸水7d)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD2"]) == 0)
                     {
                         return false;
                     }
                     else
                     {
                         mItem["HG_QD2"] = IsQualified(mItem["G_QD2"], sItem["QD2"], false);
                     }
                 }
                 //mItem[0]["WHICH") = IIf(sign1, "bgmmj", "bgmmj_10")

                 if (sign)
                 {
                     mbhggs = mItem["HG_QD2"] == "不合格" ? mbhggs + 1 : mbhggs;
                 }
                 else
                 {
                     if (sign1)
                     {
                         sItem["QD2"] = "----";
                         sItem["PHJM2"] = "----";
                         sItem["PHJMPD2"] = "----";
                         mItem["HG_QD2"] = "----";
                         mItem["G_QD2"] = "----";
                         mItem["G_PHJM2"] = "----";
                     }
                 }

                 if (!sign1)
                 {
                     mItem["HG_QD2"] = "不合格";
                     mbhggs = mbhggs + 1;
                 }


                 sign = false;
                 if (jcxm.Contains("、压剪粘结强度(耐冻融)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD3"]) == 0)
                     {
                         return false;
                     }

                     mItem["HG_QD3"] = IsQualified(mItem["G_QD3"], sItem["QD3"], false);
                 }

                 //if (jcxm.Contains("、拉伸粘结强度(耐冻融)、") && sign1)
                 if (jcxm.Contains("、拉伸粘结耐冻融强度、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD3"]) == 0)
                     {
                         return false;
                     }

                     if (sItem["PHJM3"] == "EPS板破坏")
                     {
                         mItem["HG_QD3"] = IsQualified(mItem["G_QD3"], sItem["QD3"], false);
                     }
                     else
                     {
                         mItem["HG_QD3"] = "不合格";
                     }
                 }

                 if (sign)
                 {
                     mbhggs = mItem["HG_QD3"] == "不合格" ? mbhggs + 1 : mbhggs;
                 }
                 else
                 {
                     sItem["QD3"] = "----";
                     sItem["PHJM3"] = "----";
                     sItem["PHJMPD3"] = "----";
                     mItem["HG_QD3"] = "----";
                     mItem["G_QD3"] = "----";
                     mItem["G_PHJM3"] = "----";
                 }



                 if (mbhggs == 0)
                 {
                     jsbeizhu = "该组试件所检项目符合" + mItem["PDBZ"] + "标准要求。";
                     sItem["JCJG"] = "合格";
                     mAllHg = true;
                 }

                 if (mbhggs > 0)
                 {
                     sItem["JCJG"] = "不合格";
                     mAllHg = false;
                     jsbeizhu = "冻融中试件破坏，该组试件不符合" + mItem["PDBZ"] + "标准要求。";
                 }

                 return mAllHg;
             };


            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["GGXH"] && u["JCYJ"] == sItem["JCBZH"]);

                if (null == mrsDj)
                {
                    mJSFF = "";
                    jsbeizhu = "依据不详";
                    mAllHg = false;
                    sItem["JCJG"] = "不下结论";
                    continue;
                }
                g_Qd1 = Conversion.Val(mrsDj["QD1"]);   //标准状态（与聚苯板）
                g_Qd2 = Conversion.Val(mrsDj["QD2"]);   //浸水（与聚苯板）
                g_Qd3 = Conversion.Val(mrsDj["QD3"]);   //冻融（与聚苯板）
                g_Qd4 = Conversion.Val(mrsDj["QD4"]);   //标准状态（与水泥砂浆）
                g_Qd5 = Conversion.Val(mrsDj["QD5"]);   //浸水（与水泥砂浆）
                g_Qd6 = Conversion.Val(mrsDj["QD6"]);   //冻融（与水泥砂浆）
                g_Qd7 = Conversion.Val(mrsDj["QD7"]);   //拉伸粘结强度(与模塑板)耐水强度浸水48h干燥2h 29906

                yZbbz = Conversion.Val(mrsDj["YZB"].Replace("≤", "")); 
                MItem[0]["G_QD1"] = "≥" + mrsDj["QD1"];
                MItem[0]["G_QD2"] = "≥" + mrsDj["QD2"];
                MItem[0]["G_QD3"] = "≥" + mrsDj["QD3"];
                MItem[0]["G_QD4"] = "≥" + mrsDj["QD4"];
                MItem[0]["G_QD5"] = "≥" + mrsDj["QD5"];
                MItem[0]["G_QD6"] = "≥" + mrsDj["QD6"];
                MItem[0]["G_QD7"] = "≥" + mrsDj["QD7"];

                MItem[0]["G_PHJM1"] = mrsDj["PHJM1"];
                MItem[0]["G_PHJM2"] = mrsDj["PHJM2"];
                MItem[0]["G_PHJM3"] = mrsDj["PHJM3"];
                MItem[0]["G_PHJM4"] = mrsDj["PHJM4"];
                MItem[0]["G_PHJM5"] = mrsDj["PHJM5"];
                MItem[0]["G_PHJM6"] = mrsDj["PHJM6"];
                MItem[0]["G_KCZSJ"] = mrsDj["KCZSJ"];
                MItem[0]["G_YZB"] = mrsDj["YZB"];
                //MItem[0]["WHICH"]=mrsDj["WHICH"];

                //MItem[0]["BGNAME"]=mrsDj["BGNAME"];

                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
                    if (sItem["JCBZH"].Contains("144") || (sItem["GGXH"].Contains("抗裂砂浆") || sItem["JCBZH"].Contains("158")))
                    {
                        #region 与水泥砂浆
                        if (jcxm.Contains("、拉伸粘结强度(原强度)、") || jcxm.Contains("、拉伸粘结强度(干燥状态)、") || jcxm.Contains("、拉伸粘结强度(常温28d)、") || jcxm.Contains("、拉伸粘结原强度、") || jcxm.Contains("、拉伸粘结强度(与水泥砂浆)标准状态、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结强度(原强度),拉伸粘结强度(干燥状态),拉伸粘结强度(常温28d),拉伸粘结原强度,拉伸粘结强度(与水泥砂浆)标准状态");
                            sItem["MJ41"] = (Conversion.Val(sItem["CD41"]) * Conversion.Val(sItem["KD41"])).ToString();
                            sItem["MJ42"] = (Conversion.Val(sItem["CD42"]) * Conversion.Val(sItem["KD42"])).ToString();
                            sItem["MJ43"] = (Conversion.Val(sItem["CD43"]) * Conversion.Val(sItem["KD43"])).ToString();
                            sItem["MJ44"] = (Conversion.Val(sItem["CD44"]) * Conversion.Val(sItem["KD44"])).ToString();
                            sItem["MJ45"] = (Conversion.Val(sItem["CD45"]) * Conversion.Val(sItem["KD45"])).ToString();
                            sItem["MJ46"] = (Conversion.Val(sItem["CD46"]) * Conversion.Val(sItem["KD46"])).ToString();

                            if (Conversion.Val(sItem["MJ41"]) == 0 || Conversion.Val(sItem["MJ42"]) == 0 || Conversion.Val(sItem["MJ43"]) == 0 || Conversion.Val(sItem["MJ44"]) == 0 || Conversion.Val(sItem["MJ45"]) == 0 || Conversion.Val(sItem["MJ46"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD41"] = Conversion.Val(sItem["MJ41"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ41"]) / Conversion.Val(sItem["MJ41"])).ToString("0.000");
                                sItem["QD42"] = Conversion.Val(sItem["MJ42"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ42"]) / Conversion.Val(sItem["MJ42"])).ToString("0.000");
                                sItem["QD43"] = Conversion.Val(sItem["MJ43"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ43"]) / Conversion.Val(sItem["MJ43"])).ToString("0.000");
                                sItem["QD44"] = Conversion.Val(sItem["MJ44"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ44"]) / Conversion.Val(sItem["MJ44"])).ToString("0.000");
                                sItem["QD45"] = Conversion.Val(sItem["MJ45"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ45"]) / Conversion.Val(sItem["MJ45"])).ToString("0.000");
                                sItem["QD46"] = Conversion.Val(sItem["MJ46"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ46"]) / Conversion.Val(sItem["MJ46"])).ToString("0.000");
                            }
                            if ((Conversion.Val(sItem["QD41"]) != 0) && (Conversion.Val(sItem["QD42"]) != 0) && (Conversion.Val(sItem["QD43"]) != 0) && (Conversion.Val(sItem["QD44"]) != 0) && (Conversion.Val(sItem["QD45"]) != 0) && (Conversion.Val(sItem["QD46"]) != 0))
                            {
                                List<double> lArray = new List<double>();
                                for (int i = 1; i < 7; i++)
                                {
                                    lArray.Add(Conversion.Val(sItem["QD4" + i]));
                                }
                                lArray.Sort();
                                lArray.Remove(lArray.Max());
                                lArray.Remove(lArray.Min());
                                sItem["QD4"] = Round(lArray.Average(), 2).ToString();
                            }
                            //if (Conversion.Val(sItem["QD4"]) >= g_Qd4 && sItem["PHJMPD4"].Trim() == "合格")
                            if (Conversion.Val(sItem["QD4"]) >= g_Qd4)
                            {
                                MItem[0]["HG_QD4"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD4"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD4"] = "----";
                            sItem["PHJM4"] = "----";
                            sItem["PHJMPD4"] = "----";
                            MItem[0]["HG_QD4"] = "----";
                            MItem[0]["G_QD4"] = "----";
                            MItem[0]["G_PHJM4"] = "----";
                        }

                        if (jcxm.Contains("、拉伸粘结强度(耐水)、") || jcxm.Contains("、拉伸粘结强度(浸水48h后)、") || jcxm.Contains("、浸水拉伸粘结强度(常温28d，浸水7d)、") || jcxm.Contains("、拉伸粘结耐水强度、") || jcxm.Contains("、拉伸粘结强度(与水泥砂浆)浸水处理、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结强度(耐水),拉伸粘结强度(浸水48h后),浸水拉伸粘结强度(常温28d，浸水7d),拉伸粘结耐水强度,拉伸粘结强度(与水泥砂浆)浸水处理");
                            sItem["MJ51"] = (Conversion.Val(sItem["CD51"]) * Conversion.Val(sItem["KD51"])).ToString();
                            sItem["MJ52"] = (Conversion.Val(sItem["CD52"]) * Conversion.Val(sItem["KD52"])).ToString();
                            sItem["MJ53"] = (Conversion.Val(sItem["CD53"]) * Conversion.Val(sItem["KD53"])).ToString();
                            sItem["MJ54"] = (Conversion.Val(sItem["CD54"]) * Conversion.Val(sItem["KD54"])).ToString();
                            sItem["MJ55"] = (Conversion.Val(sItem["CD55"]) * Conversion.Val(sItem["KD55"])).ToString();
                            sItem["MJ56"] = (Conversion.Val(sItem["CD56"]) * Conversion.Val(sItem["KD56"])).ToString();

                            if (Conversion.Val(sItem["MJ51"]) == 0 || Conversion.Val(sItem["MJ52"]) == 0 || Conversion.Val(sItem["MJ53"]) == 0 || Conversion.Val(sItem["MJ54"]) == 0 || Conversion.Val(sItem["MJ55"]) == 0 || Conversion.Val(sItem["MJ56"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD51"] = Conversion.Val(sItem["MJ51"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ51"]) / Conversion.Val(sItem["MJ51"])).ToString("0.000");
                                sItem["QD52"] = Conversion.Val(sItem["MJ52"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ52"]) / Conversion.Val(sItem["MJ52"])).ToString("0.000");
                                sItem["QD53"] = Conversion.Val(sItem["MJ53"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ53"]) / Conversion.Val(sItem["MJ53"])).ToString("0.000");
                                sItem["QD54"] = Conversion.Val(sItem["MJ54"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ54"]) / Conversion.Val(sItem["MJ54"])).ToString("0.000");
                                sItem["QD55"] = Conversion.Val(sItem["MJ55"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ55"]) / Conversion.Val(sItem["MJ55"])).ToString("0.000");
                                sItem["QD56"] = Conversion.Val(sItem["MJ56"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ56"]) / Conversion.Val(sItem["MJ56"])).ToString("0.000");
                            }
                            if ((Conversion.Val(sItem["QD51"]) != 0) && (Conversion.Val(sItem["QD52"]) != 0) && (Conversion.Val(sItem["QD53"]) != 0) && (Conversion.Val(sItem["QD54"]) != 0) && (Conversion.Val(sItem["QD55"]) != 0) && (Conversion.Val(sItem["QD56"]) != 0))
                            {
                                // sItem["QD5"] = Math.Round((Conversion.Val(sItem["QD51"]) + Conversion.Val(sItem["QD52"]) + Conversion.Val(sItem["QD53"]) + Conversion.Val(sItem["QD54"]) + Conversion.Val(sItem["QD55"])) / 5, 2).ToString("0.00");
                                List<double> lArray = new List<double>();
                                for (int i = 1; i < 7; i++)
                                {
                                    lArray.Add(Conversion.Val(sItem["QD5" + i]));
                                }
                                lArray.Sort();
                                lArray.Remove(lArray.Max());
                                lArray.Remove(lArray.Min());
                                sItem["QD5"] = Round(lArray.Average(), 2).ToString();

                            }

                            //if (Conversion.Val(sItem["QD5"]) >= g_Qd5 && sItem["PHJMPD5"].Trim() == "合格")
                            if (Conversion.Val(sItem["QD5"]) >= g_Qd5)
                            {
                                MItem[0]["HG_QD5"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD5"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD5"] = "----";
                            sItem["PHJM5"] = "----";
                            sItem["PHJMPD5"] = "----";
                            MItem[0]["HG_QD5"] = "----";
                            MItem[0]["G_QD5"] = "----";
                            MItem[0]["G_PHJM5"] = "----";
                        }

                        if (jcxm.Contains("、拉伸粘结强度(耐冻融)、") || jcxm.Contains("、拉伸粘结耐冻融强度、") || jcxm.Contains("、拉伸粘结强度(与水泥砂浆)冻融循环处理、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结强度(耐冻融),拉伸粘结耐冻融强度,拉伸粘结强度(与水泥砂浆)冻融循环处理");
                            sItem["MJ61"] = (Conversion.Val(sItem["CD61"]) * Conversion.Val(sItem["KD61"])).ToString();
                            sItem["MJ62"] = (Conversion.Val(sItem["CD62"]) * Conversion.Val(sItem["KD62"])).ToString();
                            sItem["MJ63"] = (Conversion.Val(sItem["CD63"]) * Conversion.Val(sItem["KD63"])).ToString();
                            sItem["MJ64"] = (Conversion.Val(sItem["CD64"]) * Conversion.Val(sItem["KD64"])).ToString();
                            sItem["MJ65"] = (Conversion.Val(sItem["CD65"]) * Conversion.Val(sItem["KD65"])).ToString();
                            sItem["MJ66"] = (Conversion.Val(sItem["CD66"]) * Conversion.Val(sItem["KD66"])).ToString();

                            if (Conversion.Val(sItem["MJ61"]) == 0 || Conversion.Val(sItem["MJ62"]) == 0 || Conversion.Val(sItem["MJ63"]) == 0 || Conversion.Val(sItem["MJ64"]) == 0 || Conversion.Val(sItem["MJ65"]) == 0 || Conversion.Val(sItem["MJ66"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD61"] = Conversion.Val(sItem["MJ61"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ61"]) / Conversion.Val(sItem["MJ61"])).ToString("0.000");
                                sItem["QD62"] = Conversion.Val(sItem["MJ62"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ62"]) / Conversion.Val(sItem["MJ62"])).ToString("0.000");
                                sItem["QD63"] = Conversion.Val(sItem["MJ63"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ63"]) / Conversion.Val(sItem["MJ63"])).ToString("0.000");
                                sItem["QD64"] = Conversion.Val(sItem["MJ64"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ64"]) / Conversion.Val(sItem["MJ64"])).ToString("0.000");
                                sItem["QD65"] = Conversion.Val(sItem["MJ65"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ65"]) / Conversion.Val(sItem["MJ65"])).ToString("0.000");
                                sItem["QD66"] = Conversion.Val(sItem["MJ66"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ66"]) / Conversion.Val(sItem["MJ66"])).ToString("0.000");
                            }

                            if ((Conversion.Val(sItem["QD61"]) != 0) && (Conversion.Val(sItem["QD62"]) != 0) && (Conversion.Val(sItem["QD63"]) != 0) && (Conversion.Val(sItem["QD64"]) != 0) && (Conversion.Val(sItem["QD65"]) != 0) && (Conversion.Val(sItem["QD66"]) != 0))
                            {
                                //sItem["QD6"] = Math.Round((Conversion.Val(sItem["QD61"]) + Conversion.Val(sItem["QD62"]) + Conversion.Val(sItem["QD63"]) + Conversion.Val(sItem["QD64"]) + Conversion.Val(sItem["QD65"])) / 5, 2).ToString("0.00");
                                List<double> lArray = new List<double>();
                                for (int i = 1; i < 7; i++)
                                {
                                    lArray.Add(Conversion.Val(sItem["QD6" + i]));
                                }
                                lArray.Sort();
                                lArray.Remove(lArray.Max());
                                lArray.Remove(lArray.Min());
                                sItem["QD6"] = Round(lArray.Average(), 2).ToString();
                            }
                            //if (Conversion.Val(sItem["QD6"]) >= g_Qd6 && sItem["PHJMPD6"].Trim() == "合格")
                            if (Conversion.Val(sItem["QD6"]) >= g_Qd6)
                            {
                                MItem[0]["HG_QD6"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD6"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }

                        }
                        else
                        {
                            sItem["QD6"] = "----";
                            sItem["PHJM6"] = "----";
                            sItem["PHJMPD6"] = "----";
                            MItem[0]["HG_QD6"] = "----";
                            //MItem[0]["g_Qd6"] = "----";
                            //MItem[0]["G_PHJM6"] = "----";
                        }
                        #endregion


                        #region 与聚苯板
                        if (jcxm.Contains("、拉伸粘结强度(原强度)、") || jcxm.Contains("、拉伸粘结强度(干燥状态)、") || jcxm.Contains("、拉伸粘结强度(常温28d)、") || jcxm.Contains("、拉伸粘结原强度、") || jcxm.Contains("、拉伸粘结强度(与胶粉聚苯颗粒浆料)标准状态、") || jcxm.Contains("、拉伸粘结强度(与聚苯板)标准状态、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结强度(原强度),拉伸粘结强度(干燥状态),拉伸粘结强度(常温28d),拉伸粘结原强度,拉伸粘结强度(与胶粉聚苯颗粒浆料)标准状态,拉伸粘结强度(与聚苯板)标准状态");
                            sItem["MJ11"] = (Conversion.Val(sItem["CD11"]) * Conversion.Val(sItem["KD11"])).ToString();
                            sItem["MJ12"] = (Conversion.Val(sItem["CD12"]) * Conversion.Val(sItem["KD12"])).ToString();
                            sItem["MJ13"] = (Conversion.Val(sItem["CD13"]) * Conversion.Val(sItem["KD13"])).ToString();
                            sItem["MJ14"] = (Conversion.Val(sItem["CD14"]) * Conversion.Val(sItem["KD14"])).ToString();
                            sItem["MJ15"] = (Conversion.Val(sItem["CD15"]) * Conversion.Val(sItem["KD15"])).ToString();
                            sItem["MJ16"] = (Conversion.Val(sItem["CD16"]) * Conversion.Val(sItem["KD16"])).ToString();

                            if (Conversion.Val(sItem["MJ11"]) == 0 || Conversion.Val(sItem["MJ12"]) == 0 || Conversion.Val(sItem["MJ13"]) == 0 || Conversion.Val(sItem["MJ14"]) == 0 || Conversion.Val(sItem["MJ15"]) == 0 || Conversion.Val(sItem["MJ16"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD11"] = Conversion.Val(sItem["MJ11"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ11"]) / Conversion.Val(sItem["MJ11"])).ToString("0.000");
                                sItem["QD12"] = Conversion.Val(sItem["MJ12"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ12"]) / Conversion.Val(sItem["MJ12"])).ToString("0.000");
                                sItem["QD13"] = Conversion.Val(sItem["MJ13"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ13"]) / Conversion.Val(sItem["MJ13"])).ToString("0.000");
                                sItem["QD14"] = Conversion.Val(sItem["MJ14"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ14"]) / Conversion.Val(sItem["MJ14"])).ToString("0.000");
                                sItem["QD15"] = Conversion.Val(sItem["MJ15"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ15"]) / Conversion.Val(sItem["MJ15"])).ToString("0.000");
                                sItem["QD16"] = Conversion.Val(sItem["MJ16"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ16"]) / Conversion.Val(sItem["MJ16"])).ToString("0.000");
                            }
                            if ((Conversion.Val(sItem["QD11"]) != 0) && (Conversion.Val(sItem["QD12"]) != 0) && (Conversion.Val(sItem["QD13"]) != 0) && (Conversion.Val(sItem["QD14"]) != 0) && (Conversion.Val(sItem["QD15"]) != 0) && (Conversion.Val(sItem["QD16"]) != 0))
                            {
                                //sItem["QD1"] = Math.Round((Conversion.Val(sItem["QD11"]) + Conversion.Val(sItem["QD12"]) + Conversion.Val(sItem["QD13"]) + Conversion.Val(sItem["QD14"]) + Conversion.Val(sItem["QD15"])) / 5, 2).ToString("0.00");
                                List<double> lArray = new List<double>();
                                for (int i = 1; i < 7; i++)
                                {
                                    lArray.Add(Conversion.Val(sItem["QD1" + i]));
                                }
                                lArray.Sort();
                                lArray.Remove(lArray.Max());
                                lArray.Remove(lArray.Min());
                                sItem["QD1"] = Round(lArray.Average(), 2).ToString();

                            }
                            //if (Conversion.Val(sItem["QD1"]) >= g_Qd1 && sItem["PHJMPD1"].Trim() == "合格")
                            if (Conversion.Val(sItem["QD1"]) >= g_Qd1)
                            {
                                MItem[0]["HG_QD1"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD1"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD1"] = "----";
                            sItem["PHJM1"] = "----";
                            sItem["PHJMPD1"] = "----";
                            MItem[0]["HG_QD1"] = "----";
                            MItem[0]["G_QD1"] = "----";
                            MItem[0]["G_PHJM1"] = "----";
                        }

                        if (jcxm.Contains("、拉伸粘结强度(耐水)、") || jcxm.Contains("、拉伸粘结强度(浸水48h后)、") || jcxm.Contains("、浸水拉伸粘结强度(常温28d，浸水7d)、") || jcxm.Contains("、拉伸粘结耐水强度、") || jcxm.Contains("、拉伸粘结强度(与胶粉聚苯颗粒浆料)浸水处理、") || jcxm.Contains("、拉伸粘结强度(与聚苯板)浸水处理、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结强度(耐水),拉伸粘结强度(浸水48h后),浸水拉伸粘结强度(常温28d，浸水7d),拉伸粘结耐水强度,拉伸粘结强度(与胶粉聚苯颗粒浆料)浸水处理,拉伸粘结强度(与聚苯板)浸水处理");
                            sItem["MJ21"] = (Conversion.Val(sItem["CD21"]) * Conversion.Val(sItem["KD21"])).ToString();
                            sItem["MJ22"] = (Conversion.Val(sItem["CD22"]) * Conversion.Val(sItem["KD22"])).ToString();
                            sItem["MJ23"] = (Conversion.Val(sItem["CD23"]) * Conversion.Val(sItem["KD23"])).ToString();
                            sItem["MJ24"] = (Conversion.Val(sItem["CD24"]) * Conversion.Val(sItem["KD24"])).ToString();
                            sItem["MJ25"] = (Conversion.Val(sItem["CD25"]) * Conversion.Val(sItem["KD25"])).ToString();
                            sItem["MJ26"] = (Conversion.Val(sItem["CD26"]) * Conversion.Val(sItem["KD26"])).ToString();

                            if (Conversion.Val(sItem["MJ21"]) == 0 || Conversion.Val(sItem["MJ22"]) == 0 || Conversion.Val(sItem["MJ23"]) == 0 || Conversion.Val(sItem["MJ24"]) == 0 || Conversion.Val(sItem["MJ25"]) == 0 || Conversion.Val(sItem["MJ26"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD21"] = Conversion.Val(sItem["MJ21"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ21"]) / Conversion.Val(sItem["MJ21"])).ToString("0.000");
                                sItem["QD22"] = Conversion.Val(sItem["MJ22"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ22"]) / Conversion.Val(sItem["MJ22"])).ToString("0.000");
                                sItem["QD23"] = Conversion.Val(sItem["MJ23"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ23"]) / Conversion.Val(sItem["MJ23"])).ToString("0.000");
                                sItem["QD24"] = Conversion.Val(sItem["MJ24"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ24"]) / Conversion.Val(sItem["MJ24"])).ToString("0.000");
                                sItem["QD25"] = Conversion.Val(sItem["MJ25"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ25"]) / Conversion.Val(sItem["MJ25"])).ToString("0.000");
                                sItem["QD26"] = Conversion.Val(sItem["MJ26"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ26"]) / Conversion.Val(sItem["MJ26"])).ToString("0.000");
                            }
                            if ((Conversion.Val(sItem["QD21"]) != 0) && (Conversion.Val(sItem["QD22"]) != 0) && (Conversion.Val(sItem["QD23"]) != 0) && (Conversion.Val(sItem["QD24"]) != 0) && (Conversion.Val(sItem["QD25"]) != 0) && (Conversion.Val(sItem["QD26"]) != 0))
                            {
                                //sItem["QD2"] = Math.Round((Conversion.Val(sItem["QD21"]) + Conversion.Val(sItem["QD22"]) + Conversion.Val(sItem["QD23"]) + Conversion.Val(sItem["QD24"]) + Conversion.Val(sItem["QD25"])) / 5, 2).ToString("0.00");
                                List<double> lArray = new List<double>();
                                for (int i = 1; i < 7; i++)
                                {
                                    lArray.Add(Conversion.Val(sItem["QD2" + i]));
                                }
                                lArray.Sort();
                                lArray.Remove(lArray.Max());
                                lArray.Remove(lArray.Min());
                                sItem["QD2"] = Round(lArray.Average(), 2).ToString();

                            }

                            //if (Conversion.Val(sItem["QD2"]) >= g_Qd2 && sItem["PHJMPD2"].Trim() == "合格")
                            if (Conversion.Val(sItem["QD2"]) >= g_Qd2)
                            {
                                MItem[0]["HG_QD2"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD2"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD2"] = "----";
                            sItem["PHJM2"] = "----";
                            sItem["PHJMPD2"] = "----";
                            MItem[0]["HG_QD2"] = "----";
                            MItem[0]["G_QD2"] = "----";
                            MItem[0]["G_PHJM2"] = "----";
                        }

                        if (jcxm.Contains("、拉伸粘结强度(耐冻融)、") || jcxm.Contains("、拉伸粘结耐冻融强度、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结强度(耐冻融),拉伸粘结耐冻融强度");
                            sItem["MJ31"] = (Conversion.Val(sItem["CD31"]) * Conversion.Val(sItem["KD31"])).ToString();
                            sItem["MJ32"] = (Conversion.Val(sItem["CD32"]) * Conversion.Val(sItem["KD32"])).ToString();
                            sItem["MJ33"] = (Conversion.Val(sItem["CD33"]) * Conversion.Val(sItem["KD33"])).ToString();
                            sItem["MJ34"] = (Conversion.Val(sItem["CD34"]) * Conversion.Val(sItem["KD34"])).ToString();
                            sItem["MJ35"] = (Conversion.Val(sItem["CD35"]) * Conversion.Val(sItem["KD35"])).ToString();
                            sItem["MJ36"] = (Conversion.Val(sItem["CD36"]) * Conversion.Val(sItem["KD36"])).ToString();

                            if (Conversion.Val(sItem["MJ31"]) == 0 || Conversion.Val(sItem["MJ32"]) == 0 || Conversion.Val(sItem["MJ33"]) == 0 || Conversion.Val(sItem["MJ34"]) == 0 || Conversion.Val(sItem["MJ35"]) == 0 || Conversion.Val(sItem["MJ36"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD31"] = Conversion.Val(sItem["MJ31"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ31"]) / Conversion.Val(sItem["MJ31"])).ToString("0.000");
                                sItem["QD32"] = Conversion.Val(sItem["MJ32"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ32"]) / Conversion.Val(sItem["MJ32"])).ToString("0.000");
                                sItem["QD33"] = Conversion.Val(sItem["MJ33"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ33"]) / Conversion.Val(sItem["MJ33"])).ToString("0.000");
                                sItem["QD34"] = Conversion.Val(sItem["MJ34"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ34"]) / Conversion.Val(sItem["MJ34"])).ToString("0.000");
                                sItem["QD35"] = Conversion.Val(sItem["MJ35"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ35"]) / Conversion.Val(sItem["MJ35"])).ToString("0.000");
                                sItem["QD36"] = Conversion.Val(sItem["MJ36"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ36"]) / Conversion.Val(sItem["MJ36"])).ToString("0.000");
                            }

                            if ((Conversion.Val(sItem["QD31"]) != 0) && (Conversion.Val(sItem["QD32"]) != 0) && (Conversion.Val(sItem["QD33"]) != 0) && (Conversion.Val(sItem["QD34"]) != 0) && (Conversion.Val(sItem["QD35"]) != 0) && (Conversion.Val(sItem["QD36"]) != 0))
                            {
                                //sItem["QD3"] = Math.Round((Conversion.Val(sItem["QD31"]) + Conversion.Val(sItem["QD32"]) + Conversion.Val(sItem["QD33"]) + Conversion.Val(sItem["QD34"]) + Conversion.Val(sItem["QD35"])) / 5, 2).ToString("0.00");
                                List<double> lArray = new List<double>();
                                for (int i = 1; i < 7; i++)
                                {
                                    lArray.Add(Conversion.Val(sItem["QD3" + i]));
                                }
                                lArray.Sort();
                                lArray.Remove(lArray.Max());
                                lArray.Remove(lArray.Min());
                                sItem["QD3"] = Round(lArray.Average(), 2).ToString();

                            }
                            //if (Conversion.Val(sItem["QD3"]) >= g_Qd3 && sItem["PHJMPD3"].Trim() == "合格")
                            if (Conversion.Val(sItem["QD3"]) >= g_Qd3)
                            {
                                MItem[0]["HG_QD3"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD3"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }

                        }
                        else
                        {
                            sItem["QD3"] = "----";
                            sItem["PHJM3"] = "----";
                            sItem["PHJMPD3"] = "----";
                            MItem[0]["HG_QD3"] = "----";
                            //MItem[0]["G_QD3"] = "----";
                            //MItem[0]["G_PHJM3"] = "----";
                        }
                        #endregion

                        #region 压折比
                        if (jcxm.Contains("、压折比、"))
                        {
                            jcxmCur = "压折比";
                            sItem["YZB"] = string.IsNullOrEmpty(sItem["KZQD"]) ? "----" : (double.Parse(sItem["KYQD"]) / double.Parse(sItem["KZQD"])).ToString("0.0");
                            if (sItem["YZB"] != "----" && double.Parse(sItem["YZB"]) <= yZbbz)
                            {
                                MItem[0]["HG_YZB"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_YZB"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["YZB"] = "----";
                            MItem[0]["HG_YZB"] = "----";
                            MItem[0]["G_YZB"] = "----";
                        }
                        #endregion

                        #region 可操作时间
                        if (jcxm.Contains("、可操作时间、"))
                        {
                            jcxmCur = "可操作时间";
                            //if (Conversion.Val(sItem["KCZSJ"]) >= 1.5 && Conversion.Val(sItem["KCZSJ"]) <= 4)
                            if (IsQualified(MItem[0]["G_KCZSJ"], sItem["KCZSJ"], false) == "合格")
                            {
                                MItem[0]["HG_KCZSJ"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KCZSJ"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["KCZSJ"] = "----";
                            MItem[0]["G_KCZSJ"] = "----";
                            MItem[0]["HG_KCZSJ"] = "----";
                        }
                        #endregion
                    }
                    else if (sItem["JCBZH"].Contains("29906"))
                    {
                        //GB/T 29906-2013
                        #region 拉伸粘结原强度
                        if (jcxm.Contains("、拉伸粘结原强度、") || jcxm.Contains("、拉伸粘结强度(与模塑板)原强度、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结原强度,拉伸粘结强度(与模塑板)原强度");
                            sItem["MJ11"] = (Conversion.Val(sItem["CD11"]) * Conversion.Val(sItem["KD11"])).ToString();
                            sItem["MJ12"] = (Conversion.Val(sItem["CD12"]) * Conversion.Val(sItem["KD12"])).ToString();
                            sItem["MJ13"] = (Conversion.Val(sItem["CD13"]) * Conversion.Val(sItem["KD13"])).ToString();
                            sItem["MJ14"] = (Conversion.Val(sItem["CD14"]) * Conversion.Val(sItem["KD14"])).ToString();
                            sItem["MJ15"] = (Conversion.Val(sItem["CD15"]) * Conversion.Val(sItem["KD15"])).ToString();
                            sItem["MJ16"] = (Conversion.Val(sItem["CD16"]) * Conversion.Val(sItem["KD16"])).ToString();

                            if ((Conversion.Val(sItem["MJ11"]) == 0 || Conversion.Val(sItem["MJ12"]) == 0 || Conversion.Val(sItem["MJ13"]) == 0 || Conversion.Val(sItem["MJ14"]) == 0) || Conversion.Val(sItem["MJ15"]) == 0 || Conversion.Val(sItem["MJ15"]) == 0 || Conversion.Val(sItem["MJ16"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD11"] = Conversion.Val(sItem["MJ11"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ11"]) / Conversion.Val(sItem["MJ11"])).ToString("0.000");
                                sItem["QD12"] = Conversion.Val(sItem["MJ12"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ12"]) / Conversion.Val(sItem["MJ12"])).ToString("0.000");
                                sItem["QD13"] = Conversion.Val(sItem["MJ13"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ13"]) / Conversion.Val(sItem["MJ13"])).ToString("0.000");
                                sItem["QD14"] = Conversion.Val(sItem["MJ14"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ14"]) / Conversion.Val(sItem["MJ14"])).ToString("0.000");
                                sItem["QD15"] = Conversion.Val(sItem["MJ15"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ15"]) / Conversion.Val(sItem["MJ15"])).ToString("0.000");
                                sItem["QD16"] = Conversion.Val(sItem["MJ16"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ16"]) / Conversion.Val(sItem["MJ16"])).ToString("0.000");
                            }

                            if (Conversion.Val(sItem["QD11"]) != 0 && Conversion.Val(sItem["QD12"]) != 0 && Conversion.Val(sItem["QD13"]) != 0 && Conversion.Val(sItem["QD14"]) != 0 && Conversion.Val(sItem["QD15"]) != 0 && Conversion.Val(sItem["QD16"]) != 0)
                            {
                                List<double> mtmpArray = new List<double>();
                                string mlongStr = sItem["QD11"] + "," + sItem["QD12"] + "," + sItem["QD13"] + "," + sItem["QD14"] + "," + sItem["QD15"] + "," + sItem["QD16"];
                                string[] str = mlongStr.Split(',');
                                foreach (string s in str)
                                {
                                    mtmpArray.Add(double.Parse(s));
                                }
                                mtmpArray.Sort();
                                sItem["QD1"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                            }

                            if (Conversion.Val(sItem["QD1"]) >= g_Qd1 && sItem["PHJMPD1"].Trim() == "合格")
                            {
                                MItem[0]["HG_QD1"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD1"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD1"] = "----";
                            sItem["PHJM1"] = "----";
                            sItem["PHJMPD1"] = "----";
                            MItem[0]["HG_QD1"] = "----";
                            MItem[0]["G_QD1"] = "----";
                            MItem[0]["G_PHJM1"] = "----";
                        }
                        #endregion

                        #region 拉伸粘结强度(与模塑板)耐水强度浸水48h干燥2h
                        if (jcxm.Contains("、拉伸粘结强度(与模塑板)耐水强度浸水48h干燥2h、"))
                        {
                            jcxmCur = "拉伸粘结强度(与模塑板)耐水强度浸水48h干燥2h";
                            sItem["MJ71"] = (Conversion.Val(sItem["CD71"]) * Conversion.Val(sItem["KD71"])).ToString();
                            sItem["MJ72"] = (Conversion.Val(sItem["CD72"]) * Conversion.Val(sItem["KD72"])).ToString();
                            sItem["MJ73"] = (Conversion.Val(sItem["CD73"]) * Conversion.Val(sItem["KD73"])).ToString();
                            sItem["MJ74"] = (Conversion.Val(sItem["CD74"]) * Conversion.Val(sItem["KD74"])).ToString();
                            sItem["MJ75"] = (Conversion.Val(sItem["CD75"]) * Conversion.Val(sItem["KD75"])).ToString();
                            sItem["MJ76"] = (Conversion.Val(sItem["CD76"]) * Conversion.Val(sItem["KD76"])).ToString();

                            if (Conversion.Val(sItem["MJ71"]) == 0 || Conversion.Val(sItem["MJ72"]) == 0 || Conversion.Val(sItem["MJ73"]) == 0 || Conversion.Val(sItem["MJ74"]) == 0 || Conversion.Val(sItem["MJ75"]) == 0 || Conversion.Val(sItem["MJ76"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD71"] = Conversion.Val(sItem["MJ71"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ71"]) / Conversion.Val(sItem["MJ71"])).ToString("0.000");
                                sItem["QD72"] = Conversion.Val(sItem["MJ72"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ72"]) / Conversion.Val(sItem["MJ72"])).ToString("0.000");
                                sItem["QD73"] = Conversion.Val(sItem["MJ73"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ73"]) / Conversion.Val(sItem["MJ73"])).ToString("0.000");
                                sItem["QD74"] = Conversion.Val(sItem["MJ74"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ74"]) / Conversion.Val(sItem["MJ74"])).ToString("0.000");
                                sItem["QD75"] = Conversion.Val(sItem["MJ75"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ75"]) / Conversion.Val(sItem["MJ75"])).ToString("0.000");
                                sItem["QD76"] = Conversion.Val(sItem["MJ76"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ76"]) / Conversion.Val(sItem["MJ76"])).ToString("0.000");
                            }
                            if (Conversion.Val(sItem["QD71"]) != 0 && Conversion.Val(sItem["QD72"]) != 0 && Conversion.Val(sItem["QD73"]) != 0 && Conversion.Val(sItem["QD74"]) != 0 && Conversion.Val(sItem["QD75"]) != 0 && Conversion.Val(sItem["QD76"]) != 0)
                            {
                                List<double> mtmpArray = new List<double>();
                                string mlongStr = sItem["QD71"] + "," + sItem["QD72"] + "," + sItem["QD73"] + "," + sItem["QD74"] + "," + sItem["QD75"] + "," + sItem["QD76"];
                                string[] str = mlongStr.Split(',');
                                foreach (string s in str)
                                {
                                    mtmpArray.Add(double.Parse(s));
                                }
                                mtmpArray.Sort();
                                sItem["QD77"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                            }

                            if (Conversion.Val(sItem["QD77"]) >= g_Qd7 && sItem["PHJMPD7"].Trim() == "合格")
                            {
                                MItem[0]["HG_QD7"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD7"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD77"] = "----";
                            sItem["PHJM7"] = "----";
                            sItem["PHJMPD7"] = "----";
                            MItem[0]["HG_QD7"] = "----";
                            MItem[0]["G_QD7"] = "----";
                            MItem[0]["G_PHJM7"] = "----";
                        }
                        #endregion

                        #region 拉伸粘结耐水强度
                        if (jcxm.Contains("、拉伸粘结耐水强度、") || jcxm.Contains("、拉伸粘结强度(与模塑板)耐水强度浸水48h干燥7d、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结耐水强度,拉伸粘结强度(与模塑板)耐水强度浸水48h干燥7d");
                            sItem["MJ21"] = (Conversion.Val(sItem["CD21"]) * Conversion.Val(sItem["KD21"])).ToString();
                            sItem["MJ22"] = (Conversion.Val(sItem["CD22"]) * Conversion.Val(sItem["KD22"])).ToString();
                            sItem["MJ23"] = (Conversion.Val(sItem["CD23"]) * Conversion.Val(sItem["KD23"])).ToString();
                            sItem["MJ24"] = (Conversion.Val(sItem["CD24"]) * Conversion.Val(sItem["KD24"])).ToString();
                            sItem["MJ25"] = (Conversion.Val(sItem["CD25"]) * Conversion.Val(sItem["KD25"])).ToString();
                            sItem["MJ26"] = (Conversion.Val(sItem["CD26"]) * Conversion.Val(sItem["KD26"])).ToString();

                            if (Conversion.Val(sItem["MJ21"]) == 0 || Conversion.Val(sItem["MJ22"]) == 0 || Conversion.Val(sItem["MJ23"]) == 0 || Conversion.Val(sItem["MJ24"]) == 0 || Conversion.Val(sItem["MJ25"]) == 0 || Conversion.Val(sItem["MJ26"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD21"] = Conversion.Val(sItem["MJ21"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ21"]) / Conversion.Val(sItem["MJ21"])).ToString("0.000");
                                sItem["QD22"] = Conversion.Val(sItem["MJ22"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ22"]) / Conversion.Val(sItem["MJ22"])).ToString("0.000");
                                sItem["QD23"] = Conversion.Val(sItem["MJ23"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ23"]) / Conversion.Val(sItem["MJ23"])).ToString("0.000");
                                sItem["QD24"] = Conversion.Val(sItem["MJ24"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ24"]) / Conversion.Val(sItem["MJ24"])).ToString("0.000");
                                sItem["QD25"] = Conversion.Val(sItem["MJ25"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ25"]) / Conversion.Val(sItem["MJ25"])).ToString("0.000");
                                sItem["QD26"] = Conversion.Val(sItem["MJ26"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ26"]) / Conversion.Val(sItem["MJ26"])).ToString("0.000");
                            }
                            if (Conversion.Val(sItem["QD21"]) != 0 && Conversion.Val(sItem["QD22"]) != 0 && Conversion.Val(sItem["QD23"]) != 0 && Conversion.Val(sItem["QD24"]) != 0 && Conversion.Val(sItem["QD25"]) != 0 && Conversion.Val(sItem["QD26"]) != 0)
                            {
                                List<double> mtmpArray = new List<double>();
                                string mlongStr = sItem["QD21"] + "," + sItem["QD22"] + "," + sItem["QD23"] + "," + sItem["QD24"] + "," + sItem["QD25"] + "," + sItem["QD26"];
                                string[] str = mlongStr.Split(',');
                                foreach (string s in str)
                                {
                                    mtmpArray.Add(double.Parse(s));
                                }
                                mtmpArray.Sort();
                                sItem["QD2"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                            }

                            if (Conversion.Val(sItem["QD2"]) >= g_Qd2 && sItem["PHJMPD2"].Trim() == "合格")
                            {
                                MItem[0]["HG_QD2"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD2"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD2"] = "----";
                            sItem["PHJM2"] = "----";
                            sItem["PHJMPD2"] = "----";
                            MItem[0]["HG_QD2"] = "----";
                            MItem[0]["G_QD2"] = "----";
                            MItem[0]["G_PHJM2"] = "----";
                        }
                        #endregion

                        #region 拉伸粘结耐冻融强度
                        if (jcxm.Contains("、拉伸粘结耐冻融强度、") || jcxm.Contains("、拉伸粘结强度(与模塑板)耐冻融强度、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结耐冻融强度,拉伸粘结强度(与模塑板)耐冻融强度");
                            sItem["MJ31"] = (Conversion.Val(sItem["CD31"]) * Conversion.Val(sItem["KD31"])).ToString();
                            sItem["MJ32"] = (Conversion.Val(sItem["CD32"]) * Conversion.Val(sItem["KD32"])).ToString();
                            sItem["MJ33"] = (Conversion.Val(sItem["CD33"]) * Conversion.Val(sItem["KD33"])).ToString();
                            sItem["MJ34"] = (Conversion.Val(sItem["CD34"]) * Conversion.Val(sItem["KD34"])).ToString();
                            sItem["MJ35"] = (Conversion.Val(sItem["CD35"]) * Conversion.Val(sItem["KD35"])).ToString();
                            sItem["MJ36"] = (Conversion.Val(sItem["CD36"]) * Conversion.Val(sItem["KD36"])).ToString();

                            if (Conversion.Val(sItem["MJ31"]) == 0 || Conversion.Val(sItem["MJ32"]) == 0 || Conversion.Val(sItem["MJ33"]) == 0 || Conversion.Val(sItem["MJ34"]) == 0 || Conversion.Val(sItem["MJ35"]) == 0 || Conversion.Val(sItem["MJ36"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD31"] = Conversion.Val(sItem["MJ31"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ31"]) / Conversion.Val(sItem["MJ31"])).ToString("0.000");
                                sItem["QD32"] = Conversion.Val(sItem["MJ32"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ32"]) / Conversion.Val(sItem["MJ32"])).ToString("0.000");
                                sItem["QD33"] = Conversion.Val(sItem["MJ33"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ33"]) / Conversion.Val(sItem["MJ33"])).ToString("0.000");
                                sItem["QD34"] = Conversion.Val(sItem["MJ34"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ34"]) / Conversion.Val(sItem["MJ34"])).ToString("0.000");
                                sItem["QD35"] = Conversion.Val(sItem["MJ35"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ35"]) / Conversion.Val(sItem["MJ35"])).ToString("0.000");
                                sItem["QD36"] = Conversion.Val(sItem["MJ36"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ36"]) / Conversion.Val(sItem["MJ36"])).ToString("0.000");
                            }

                            if (Conversion.Val(sItem["QD31"]) != 0 && Conversion.Val(sItem["QD32"]) != 0 && Conversion.Val(sItem["QD33"]) != 0 && Conversion.Val(sItem["QD34"]) != 0 && Conversion.Val(sItem["QD35"]) != 0 && Conversion.Val(sItem["QD36"]) != 0)
                            {
                                List<double> mtmpArray = new List<double>();
                                string mlongStr = sItem["QD31"] + "," + sItem["QD32"] + "," + sItem["QD33"] + "," + sItem["QD34"] + "," + sItem["QD35"] + "," + sItem["QD36"];
                                string[] str = mlongStr.Split(',');
                                foreach (string s in str)
                                {
                                    mtmpArray.Add(double.Parse(s));
                                }
                                mtmpArray.Sort();
                                sItem["QD3"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                            }


                            if (Conversion.Val(sItem["QD3"]) >= g_Qd3 && sItem["PHJMPD3"].Trim() == "合格")
                            {
                                MItem[0]["HG_QD3"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD3"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD3"] = "----";
                            sItem["PHJM3"] = "----";
                            sItem["PHJMPD3"] = "----";
                            MItem[0]["HG_QD3"] = "----";
                            //MItem[0]["G_QD3"] = "----";
                            //MItem[0]["G_PHJM3"] = "----";
                        }
                        #endregion

                        #region 压折比
                        if (jcxm.Contains("、压折比、"))
                        {
                            jcxmCur = "压折比";
                            sItem["YZB"] = string.IsNullOrEmpty(sItem["KZQD"]) ? "----" : (double.Parse(sItem["KYQD"]) / double.Parse(sItem["KZQD"])).ToString("0.0");
                            if (sItem["YZB"] != "----" && double.Parse(sItem["YZB"]) <= yZbbz)
                            {
                                MItem[0]["HG_YZB"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_YZB"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["YZB"] = "----";
                            MItem[0]["HG_YZB"] = "----";
                            MItem[0]["G_YZB"] = "----";
                        }
                        #endregion

                        #region 可操作时间
                        if (jcxm.Contains("、可操作时间、"))
                        {
                            jcxmCur = "可操作时间";
                            if (Conversion.Val(sItem["KCZSJ"]) >= 1.5 && Conversion.Val(sItem["KCZSJ"]) <= 4)
                            {
                                MItem[0]["HG_KCZSJ"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KCZSJ"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["KCZSJ"] = "----";
                            MItem[0]["G_KCZSJ"] = "----";
                            MItem[0]["HG_KCZSJ"] = "----";
                        }
                        #endregion

                    }
                    else if (sItem["JCBZH"].Contains("993"))
                    {
                        //JC/T 993-2006
                        #region 拉伸粘结原强度
                        if (jcxm.Contains("、拉伸粘结原强度、"))
                        {
                            jcxmCur = "拉伸粘结原强度";
                            sItem["MJ11"] = (Conversion.Val(sItem["CD11"]) * Conversion.Val(sItem["KD11"])).ToString();
                            sItem["MJ12"] = (Conversion.Val(sItem["CD12"]) * Conversion.Val(sItem["KD12"])).ToString();
                            sItem["MJ13"] = (Conversion.Val(sItem["CD13"]) * Conversion.Val(sItem["KD13"])).ToString();
                            sItem["MJ14"] = (Conversion.Val(sItem["CD14"]) * Conversion.Val(sItem["KD14"])).ToString();
                            sItem["MJ15"] = (Conversion.Val(sItem["CD15"]) * Conversion.Val(sItem["KD15"])).ToString();
                            //sItem["MJ16"] = (Conversion.Val(sItem["CD16"]) * Conversion.Val(sItem["KD16"])).ToString();

                            //if ((Conversion.Val(sItem["MJ11"]) == 0 || Conversion.Val(sItem["MJ12"]) == 0 || Conversion.Val(sItem["MJ13"]) == 0 || Conversion.Val(sItem["MJ14"]) == 0) || Conversion.Val(sItem["MJ15"]) == 0 || Conversion.Val(sItem["MJ15"]) == 0 || Conversion.Val(sItem["MJ16"]) == 0)
                            if ((Conversion.Val(sItem["MJ11"]) == 0 || Conversion.Val(sItem["MJ12"]) == 0 || Conversion.Val(sItem["MJ13"]) == 0 || Conversion.Val(sItem["MJ14"]) == 0) || Conversion.Val(sItem["MJ15"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD11"] = Conversion.Val(sItem["MJ11"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ11"]) / Conversion.Val(sItem["MJ11"])).ToString("0.000");
                                sItem["QD12"] = Conversion.Val(sItem["MJ12"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ12"]) / Conversion.Val(sItem["MJ12"])).ToString("0.000");
                                sItem["QD13"] = Conversion.Val(sItem["MJ13"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ13"]) / Conversion.Val(sItem["MJ13"])).ToString("0.000");
                                sItem["QD14"] = Conversion.Val(sItem["MJ14"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ14"]) / Conversion.Val(sItem["MJ14"])).ToString("0.000");
                                sItem["QD15"] = Conversion.Val(sItem["MJ15"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ15"]) / Conversion.Val(sItem["MJ15"])).ToString("0.000");
                                //sItem["QD16"] = Conversion.Val(sItem["MJ16"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ16"]) / Conversion.Val(sItem["MJ16"])).ToString("0.000");
                            }

                            //if (Conversion.Val(sItem["QD11"]) != 0 && Conversion.Val(sItem["QD12"]) != 0 && Conversion.Val(sItem["QD13"]) != 0 && Conversion.Val(sItem["QD14"]) != 0 && Conversion.Val(sItem["QD15"]) != 0 && Conversion.Val(sItem["QD16"]) != 0)
                            if (Conversion.Val(sItem["QD11"]) != 0 && Conversion.Val(sItem["QD12"]) != 0 && Conversion.Val(sItem["QD13"]) != 0 && Conversion.Val(sItem["QD14"]) != 0 && Conversion.Val(sItem["QD15"]) != 0)
                            {
                                List<double> mtmpArray = new List<double>();
                                //string mlongStr = sItem["QD11"] + "," + sItem["QD12"] + "," + sItem["QD13"] + "," + sItem["QD14"] + "," + sItem["QD15"] + "," + sItem["QD16"];
                                string mlongStr = sItem["QD11"] + "," + sItem["QD12"] + "," + sItem["QD13"] + "," + sItem["QD14"] + "," + sItem["QD15"];
                                string[] str = mlongStr.Split(',');
                                foreach (string s in str)
                                {
                                    mtmpArray.Add(double.Parse(s));
                                }
                                mtmpArray.Sort();
                                //sItem["QD1"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                                sItem["QD1"] = mtmpArray.Average().ToString("0.00");
                            }

                            if (Conversion.Val(sItem["QD1"]) >= g_Qd1 && sItem["PHJMPD1"].Trim() == "合格")
                            {
                                MItem[0]["HG_QD1"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD1"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD1"] = "----";
                            sItem["PHJM1"] = "----";
                            sItem["PHJMPD1"] = "----";
                            MItem[0]["HG_QD1"] = "----";
                            MItem[0]["G_QD1"] = "----";
                            MItem[0]["G_PHJM1"] = "----";
                        }
                        #endregion

                        #region 拉伸粘结耐水强度
                        if (jcxm.Contains("、拉伸粘结耐水强度、"))
                        {
                            jcxmCur = "拉伸粘结耐水强度";
                            sItem["MJ21"] = (Conversion.Val(sItem["CD21"]) * Conversion.Val(sItem["KD21"])).ToString();
                            sItem["MJ22"] = (Conversion.Val(sItem["CD22"]) * Conversion.Val(sItem["KD22"])).ToString();
                            sItem["MJ23"] = (Conversion.Val(sItem["CD23"]) * Conversion.Val(sItem["KD23"])).ToString();
                            sItem["MJ24"] = (Conversion.Val(sItem["CD24"]) * Conversion.Val(sItem["KD24"])).ToString();
                            sItem["MJ25"] = (Conversion.Val(sItem["CD25"]) * Conversion.Val(sItem["KD25"])).ToString();
                            //sItem["MJ26"] = (Conversion.Val(sItem["CD26"]) * Conversion.Val(sItem["KD26"])).ToString();

                            //if (Conversion.Val(sItem["MJ21"]) == 0 || Conversion.Val(sItem["MJ22"]) == 0 || Conversion.Val(sItem["MJ23"]) == 0 || Conversion.Val(sItem["MJ24"]) == 0 || Conversion.Val(sItem["MJ25"]) == 0 || Conversion.Val(sItem["MJ26"]) == 0)
                            if (Conversion.Val(sItem["MJ21"]) == 0 || Conversion.Val(sItem["MJ22"]) == 0 || Conversion.Val(sItem["MJ23"]) == 0 || Conversion.Val(sItem["MJ24"]) == 0 || Conversion.Val(sItem["MJ25"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD21"] = Conversion.Val(sItem["MJ21"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ21"]) / Conversion.Val(sItem["MJ21"])).ToString("0.000");
                                sItem["QD22"] = Conversion.Val(sItem["MJ22"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ22"]) / Conversion.Val(sItem["MJ22"])).ToString("0.000");
                                sItem["QD23"] = Conversion.Val(sItem["MJ23"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ23"]) / Conversion.Val(sItem["MJ23"])).ToString("0.000");
                                sItem["QD24"] = Conversion.Val(sItem["MJ24"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ24"]) / Conversion.Val(sItem["MJ24"])).ToString("0.000");
                                sItem["QD25"] = Conversion.Val(sItem["MJ25"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ25"]) / Conversion.Val(sItem["MJ25"])).ToString("0.000");
                                //sItem["QD26"] = Conversion.Val(sItem["MJ26"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ26"]) / Conversion.Val(sItem["MJ26"])).ToString("0.000");
                            }
                            //if (Conversion.Val(sItem["QD21"]) != 0 && Conversion.Val(sItem["QD22"]) != 0 && Conversion.Val(sItem["QD23"]) != 0 && Conversion.Val(sItem["QD24"]) != 0 && Conversion.Val(sItem["QD25"]) != 0 && Conversion.Val(sItem["QD26"]) != 0)
                            if (Conversion.Val(sItem["QD21"]) != 0 && Conversion.Val(sItem["QD22"]) != 0 && Conversion.Val(sItem["QD23"]) != 0 && Conversion.Val(sItem["QD24"]) != 0 && Conversion.Val(sItem["QD25"]) != 0)
                            {
                                List<double> mtmpArray = new List<double>();
                                //string mlongStr = sItem["QD21"] + "," + sItem["QD22"] + "," + sItem["QD23"] + "," + sItem["QD24"] + "," + sItem["QD25"] + "," + sItem["QD26"];
                                string mlongStr = sItem["QD21"] + "," + sItem["QD22"] + "," + sItem["QD23"] + "," + sItem["QD24"] + "," + sItem["QD25"];
                                string[] str = mlongStr.Split(',');
                                foreach (string s in str)
                                {
                                    mtmpArray.Add(double.Parse(s));
                                }
                                mtmpArray.Sort();
                                //sItem["QD2"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                                sItem["QD2"] = mtmpArray.Average().ToString("0.00");
                            }

                            if (Conversion.Val(sItem["QD2"]) >= g_Qd2 && sItem["PHJMPD2"].Trim() == "合格")
                            {
                                MItem[0]["HG_QD2"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD2"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD2"] = "----";
                            sItem["PHJM2"] = "----";
                            sItem["PHJMPD2"] = "----";
                            MItem[0]["HG_QD2"] = "----";
                            MItem[0]["G_QD2"] = "----";
                            MItem[0]["G_PHJM2"] = "----";
                        }
                        #endregion

                        #region 拉伸粘结耐冻融强度
                        if (jcxm.Contains("、拉伸粘结耐冻融强度、"))
                        {
                            jcxmCur = "拉伸粘结耐冻融强度";
                            sItem["MJ31"] = (Conversion.Val(sItem["CD31"]) * Conversion.Val(sItem["KD31"])).ToString();
                            sItem["MJ32"] = (Conversion.Val(sItem["CD32"]) * Conversion.Val(sItem["KD32"])).ToString();
                            sItem["MJ33"] = (Conversion.Val(sItem["CD33"]) * Conversion.Val(sItem["KD33"])).ToString();
                            sItem["MJ34"] = (Conversion.Val(sItem["CD34"]) * Conversion.Val(sItem["KD34"])).ToString();
                            sItem["MJ35"] = (Conversion.Val(sItem["CD35"]) * Conversion.Val(sItem["KD35"])).ToString();
                            //sItem["MJ36"] = (Conversion.Val(sItem["CD36"]) * Conversion.Val(sItem["KD36"])).ToString();

                            //if (Conversion.Val(sItem["MJ31"]) == 0 || Conversion.Val(sItem["MJ32"]) == 0 || Conversion.Val(sItem["MJ33"]) == 0 || Conversion.Val(sItem["MJ34"]) == 0 || Conversion.Val(sItem["MJ35"]) == 0 || Conversion.Val(sItem["MJ36"]) == 0)
                            if (Conversion.Val(sItem["MJ31"]) == 0 || Conversion.Val(sItem["MJ32"]) == 0 || Conversion.Val(sItem["MJ33"]) == 0 || Conversion.Val(sItem["MJ34"]) == 0 || Conversion.Val(sItem["MJ35"]) == 0)
                            {

                            }
                            else
                            {
                                sItem["QD31"] = Conversion.Val(sItem["MJ31"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ31"]) / Conversion.Val(sItem["MJ31"])).ToString("0.000");
                                sItem["QD32"] = Conversion.Val(sItem["MJ32"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ32"]) / Conversion.Val(sItem["MJ32"])).ToString("0.000");
                                sItem["QD33"] = Conversion.Val(sItem["MJ33"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ33"]) / Conversion.Val(sItem["MJ33"])).ToString("0.000");
                                sItem["QD34"] = Conversion.Val(sItem["MJ34"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ34"]) / Conversion.Val(sItem["MJ34"])).ToString("0.000");
                                sItem["QD35"] = Conversion.Val(sItem["MJ35"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ35"]) / Conversion.Val(sItem["MJ35"])).ToString("0.000");
                                //sItem["QD36"] = Conversion.Val(sItem["MJ36"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ36"]) / Conversion.Val(sItem["MJ36"])).ToString("0.000");
                            }

                            //if (Conversion.Val(sItem["QD31"]) != 0 && Conversion.Val(sItem["QD32"]) != 0 && Conversion.Val(sItem["QD33"]) != 0 && Conversion.Val(sItem["QD34"]) != 0 && Conversion.Val(sItem["QD35"]) != 0 && Conversion.Val(sItem["QD36"]) != 0)
                            if (Conversion.Val(sItem["QD31"]) != 0 && Conversion.Val(sItem["QD32"]) != 0 && Conversion.Val(sItem["QD33"]) != 0 && Conversion.Val(sItem["QD34"]) != 0 && Conversion.Val(sItem["QD35"]) != 0)
                            {
                                List<double> mtmpArray = new List<double>();
                                //string mlongStr = sItem["QD31"] + "," + sItem["QD32"] + "," + sItem["QD33"] + "," + sItem["QD34"] + "," + sItem["QD35"] + "," + sItem["QD36"];
                                string mlongStr = sItem["QD31"] + "," + sItem["QD32"] + "," + sItem["QD33"] + "," + sItem["QD34"] + "," + sItem["QD35"];
                                string[] str = mlongStr.Split(',');
                                foreach (string s in str)
                                {
                                    mtmpArray.Add(double.Parse(s));
                                }
                                mtmpArray.Sort();
                                //sItem["QD3"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                                sItem["QD3"] = mtmpArray.Average().ToString("0.00");
                            }


                            if (Conversion.Val(sItem["QD3"]) >= g_Qd3 && sItem["PHJMPD3"].Trim() == "合格")
                            {
                                MItem[0]["HG_QD3"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_QD3"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["QD3"] = "----";
                            sItem["PHJM3"] = "----";
                            sItem["PHJMPD3"] = "----";
                            MItem[0]["HG_QD3"] = "----";
                            //MItem[0]["G_QD3"] = "----";
                            //MItem[0]["G_PHJM3"] = "----";
                        }
                        #endregion

                        #region 压折比
                        if (jcxm.Contains("、压折比、"))
                        {
                            jcxmCur = "压折比";
                            sItem["YZB"] = string.IsNullOrEmpty(sItem["KZQD"]) ? "----" : (double.Parse(sItem["KYQD"]) / double.Parse(sItem["KZQD"])).ToString("0.0");
                            if (sItem["YZB"] != "----" && double.Parse(sItem["YZB"]) <= yZbbz)
                            {
                                MItem[0]["HG_YZB"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_YZB"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["YZB"] = "----";
                            MItem[0]["HG_YZB"] = "----";
                            MItem[0]["G_YZB"] = "----";
                        }
                        #endregion

                        #region 可操作时间
                        if (jcxm.Contains("、可操作时间、"))
                        {
                            jcxmCur = "可操作时间";
                            if (Conversion.Val(sItem["KCZSJ"]) >= 1.5 && Conversion.Val(sItem["KCZSJ"]) <= 4)
                            {
                                MItem[0]["HG_KCZSJ"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KCZSJ"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["KCZSJ"] = "----";
                            MItem[0]["G_KCZSJ"] = "----";
                            MItem[0]["HG_KCZSJ"] = "----";
                        }
                        #endregion

                    }
                    else
                    {
                        //JC 149-2003(JG 158)标准开始
                        #region 拉伸粘结强度(原强度)、拉伸粘结强度(干燥状态)、压剪粘结强度(原强度)、
                        if (jcxm.Contains("、拉伸粘结强度(原强度)、") || jcxm.Contains("、拉伸粘结强度(干燥状态)、") || jcxm.Contains("、压剪粘结强度(原强度)、") || jcxm.Contains("、拉伸粘结原强度、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结强度(原强度),拉伸粘结强度(干燥状态),压剪粘结强度(原强度),拉伸粘结原强度");
                            if (jcxm.Contains("、压剪粘结强度(原强度)、"))
                            {
                                sItem["MJ11"] = (Conversion.Val(sItem["CD11"]) * Conversion.Val(sItem["KD11"])).ToString();
                                sItem["MJ12"] = (Conversion.Val(sItem["CD12"]) * Conversion.Val(sItem["KD12"])).ToString();
                                sItem["MJ13"] = (Conversion.Val(sItem["CD13"]) * Conversion.Val(sItem["KD13"])).ToString();
                                sItem["MJ14"] = (Conversion.Val(sItem["CD14"]) * Conversion.Val(sItem["KD14"])).ToString();

                                if ((Conversion.Val(sItem["MJ11"]) == 0 || Conversion.Val(sItem["MJ12"]) == 0 || Conversion.Val(sItem["MJ13"]) == 0 || Conversion.Val(sItem["MJ14"]) == 0))
                                {

                                }
                                else
                                {
                                    sItem["QD11"] = Conversion.Val(sItem["MJ11"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ11"]) / Conversion.Val(sItem["MJ11"])).ToString("0.000");
                                    sItem["QD12"] = Conversion.Val(sItem["MJ12"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ12"]) / Conversion.Val(sItem["MJ12"])).ToString("0.000");
                                    sItem["QD13"] = Conversion.Val(sItem["MJ13"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ13"]) / Conversion.Val(sItem["MJ13"])).ToString("0.000");
                                    sItem["QD14"] = Conversion.Val(sItem["MJ14"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ14"]) / Conversion.Val(sItem["MJ14"])).ToString("0.000");
                                }

                                if (Conversion.Val(sItem["QD11"]) != 0 && Conversion.Val(sItem["QD12"]) != 0 && Conversion.Val(sItem["QD13"]) != 0 && Conversion.Val(sItem["QD14"]) != 0)
                                {
                                    List<double> mtmpArray = new List<double>();
                                    string mlongStr = sItem["QD11"] + "," + sItem["QD12"] + "," + sItem["QD13"] + "," + sItem["QD14"];
                                    string[] str = mlongStr.Split(',');
                                    foreach (string s in str)
                                    {
                                        mtmpArray.Add(double.Parse(s));
                                    }
                                    mtmpArray.Sort();

                                    if ((mtmpArray[1] - mtmpArray[0]) / (mtmpArray[3] - mtmpArray[0]) >= 0.765)
                                    {
                                        sItem["QD1"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3]) / 3).ToString("0.00");
                                    }
                                    else
                                    {
                                        if ((mtmpArray[3] - mtmpArray[2]) / (mtmpArray[3] - mtmpArray[0]) >= 0.765)
                                        {
                                            sItem["QD1"] = ((mtmpArray[0] + mtmpArray[1] + mtmpArray[2]) / 3).ToString("0.00");
                                        }
                                        else
                                        {
                                            sItem["QD1"] = ((mtmpArray[0] + mtmpArray[1] + mtmpArray[2] + mtmpArray[3]) / 4).ToString("0.00");
                                        }
                                    }
                                }
                                if (Conversion.Val(sItem["QD1"]) >= g_Qd1)
                                {
                                    MItem[0]["HG_QD1"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_QD1"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                sItem["MJ11"] = (Conversion.Val(sItem["CD11"]) * Conversion.Val(sItem["KD11"])).ToString();
                                sItem["MJ12"] = (Conversion.Val(sItem["CD12"]) * Conversion.Val(sItem["KD12"])).ToString();
                                sItem["MJ13"] = (Conversion.Val(sItem["CD13"]) * Conversion.Val(sItem["KD13"])).ToString();
                                sItem["MJ14"] = (Conversion.Val(sItem["CD14"]) * Conversion.Val(sItem["KD14"])).ToString();
                                sItem["MJ15"] = (Conversion.Val(sItem["CD15"]) * Conversion.Val(sItem["KD15"])).ToString();
                                sItem["MJ16"] = (Conversion.Val(sItem["CD16"]) * Conversion.Val(sItem["KD16"])).ToString();

                                if ((Conversion.Val(sItem["MJ11"]) == 0 || Conversion.Val(sItem["MJ12"]) == 0 || Conversion.Val(sItem["MJ13"]) == 0 || Conversion.Val(sItem["MJ14"]) == 0) || Conversion.Val(sItem["MJ15"]) == 0 || Conversion.Val(sItem["MJ15"]) == 0 || Conversion.Val(sItem["MJ16"]) == 0)
                                {

                                }
                                else
                                {
                                    sItem["QD11"] = Conversion.Val(sItem["MJ11"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ11"]) / Conversion.Val(sItem["MJ11"])).ToString("0.000");
                                    sItem["QD12"] = Conversion.Val(sItem["MJ12"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ12"]) / Conversion.Val(sItem["MJ12"])).ToString("0.000");
                                    sItem["QD13"] = Conversion.Val(sItem["MJ13"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ13"]) / Conversion.Val(sItem["MJ13"])).ToString("0.000");
                                    sItem["QD14"] = Conversion.Val(sItem["MJ14"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ14"]) / Conversion.Val(sItem["MJ14"])).ToString("0.000");
                                    sItem["QD15"] = Conversion.Val(sItem["MJ15"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ15"]) / Conversion.Val(sItem["MJ15"])).ToString("0.000");
                                    sItem["QD16"] = Conversion.Val(sItem["MJ16"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ16"]) / Conversion.Val(sItem["MJ16"])).ToString("0.000");
                                }

                                if (Conversion.Val(sItem["QD11"]) != 0 && Conversion.Val(sItem["QD12"]) != 0 && Conversion.Val(sItem["QD13"]) != 0 && Conversion.Val(sItem["QD14"]) != 0 && Conversion.Val(sItem["QD15"]) != 0 && Conversion.Val(sItem["QD16"]) != 0)
                                {
                                    List<double> mtmpArray = new List<double>();
                                    string mlongStr = sItem["QD11"] + "," + sItem["QD12"] + "," + sItem["QD13"] + "," + sItem["QD14"] + "," + sItem["QD15"] + "," + sItem["QD16"];
                                    string[] str = mlongStr.Split(',');
                                    foreach (string s in str)
                                    {
                                        mtmpArray.Add(double.Parse(s));
                                    }
                                    mtmpArray.Sort();
                                    sItem["QD1"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                                }

                                if (Conversion.Val(sItem["QD1"]) >= g_Qd1 && sItem["PHJMPD1"].Trim() == "合格")
                                {
                                    MItem[0]["HG_QD1"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_QD1"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                    mAllHg = false;
                                }
                            }
                        }
                        else
                        {
                            sItem["QD1"] = "----";
                            sItem["PHJM1"] = "----";
                            sItem["PHJMPD1"] = "----";
                            MItem[0]["HG_QD1"] = "----";
                            MItem[0]["G_QD1"] = "----";
                            MItem[0]["G_PHJM1"] = "----";
                        }
                        #endregion

                        #region 拉伸粘结强度(耐水)、拉伸粘结强度(浸水48h后)、压剪粘结强度(耐水)、
                        if (jcxm.Contains("、拉伸粘结强度(耐水)、") || jcxm.Contains("、拉伸粘结强度(浸水48h后)、") || jcxm.Contains("、压剪粘结强度(耐水)、") || jcxm.Contains("、拉伸粘结耐水强度、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结强度(耐水),拉伸粘结强度(浸水48h后),压剪粘结强度(耐水),拉伸粘结耐水强度");
                            if (jcxm.Contains("、压剪粘结强度(耐水)、"))
                            {
                                sItem["MJ21"] = (Conversion.Val(sItem["CD21"]) * Conversion.Val(sItem["KD21"])).ToString();
                                sItem["MJ22"] = (Conversion.Val(sItem["CD22"]) * Conversion.Val(sItem["KD22"])).ToString();
                                sItem["MJ23"] = (Conversion.Val(sItem["CD23"]) * Conversion.Val(sItem["KD23"])).ToString();
                                sItem["MJ24"] = (Conversion.Val(sItem["CD24"]) * Conversion.Val(sItem["KD24"])).ToString();

                                if (Conversion.Val(sItem["MJ21"]) == 0 || Conversion.Val(sItem["MJ22"]) == 0 || Conversion.Val(sItem["MJ23"]) == 0 || Conversion.Val(sItem["MJ24"]) == 0)
                                {

                                }
                                else
                                {
                                    sItem["QD21"] = Conversion.Val(sItem["MJ21"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ21"]) / Conversion.Val(sItem["MJ21"])).ToString("0.000");
                                    sItem["QD22"] = Conversion.Val(sItem["MJ22"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ22"]) / Conversion.Val(sItem["MJ22"])).ToString("0.000");
                                    sItem["QD23"] = Conversion.Val(sItem["MJ23"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ23"]) / Conversion.Val(sItem["MJ23"])).ToString("0.000");
                                    sItem["QD24"] = Conversion.Val(sItem["MJ24"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ24"]) / Conversion.Val(sItem["MJ24"])).ToString("0.000");
                                }
                                if (Conversion.Val(sItem["QD21"]) != 0 && Conversion.Val(sItem["QD22"]) != 0 && Conversion.Val(sItem["QD23"]) != 0 && Conversion.Val(sItem["QD24"]) != 0)
                                {
                                    List<double> mtmpArray = new List<double>();
                                    string mlongStr = sItem["QD21"] + "," + sItem["QD22"] + "," + sItem["QD23"] + "," + sItem["QD24"];
                                    string[] str = mlongStr.Split(',');
                                    foreach (string s in str)
                                    {
                                        mtmpArray.Add(double.Parse(s));
                                    }
                                    mtmpArray.Sort();
                                    if ((mtmpArray[1] - mtmpArray[0]) / (mtmpArray[3] - mtmpArray[0]) >= 0.765)
                                    {
                                        sItem["QD2"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3]) / 3).ToString("0.00");
                                    }
                                    else
                                    {
                                        if ((mtmpArray[3] - mtmpArray[2]) / (mtmpArray[3] - mtmpArray[0]) >= 0.765)
                                        {
                                            sItem["QD2"] = ((mtmpArray[0] + mtmpArray[1] + mtmpArray[2]) / 3).ToString("0.00");
                                        }
                                        else
                                        {
                                            sItem["QD2"] = ((mtmpArray[0] + mtmpArray[1] + mtmpArray[2] + mtmpArray[3]) / 4).ToString("0.00");
                                        }
                                    }
                                }

                                if (Conversion.Val(sItem["QD2"]) >= g_Qd2)
                                {
                                    MItem[0]["HG_QD2"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_QD2"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                sItem["MJ21"] = (Conversion.Val(sItem["CD21"]) * Conversion.Val(sItem["KD21"])).ToString();
                                sItem["MJ22"] = (Conversion.Val(sItem["CD22"]) * Conversion.Val(sItem["KD22"])).ToString();
                                sItem["MJ23"] = (Conversion.Val(sItem["CD23"]) * Conversion.Val(sItem["KD23"])).ToString();
                                sItem["MJ24"] = (Conversion.Val(sItem["CD24"]) * Conversion.Val(sItem["KD24"])).ToString();
                                sItem["MJ25"] = (Conversion.Val(sItem["CD25"]) * Conversion.Val(sItem["KD25"])).ToString();
                                sItem["MJ26"] = (Conversion.Val(sItem["CD26"]) * Conversion.Val(sItem["KD26"])).ToString();

                                if (Conversion.Val(sItem["MJ21"]) == 0 || Conversion.Val(sItem["MJ22"]) == 0 || Conversion.Val(sItem["MJ23"]) == 0 || Conversion.Val(sItem["MJ24"]) == 0 || Conversion.Val(sItem["MJ25"]) == 0 || Conversion.Val(sItem["MJ26"]) == 0)
                                {

                                }
                                else
                                {
                                    sItem["QD21"] = Conversion.Val(sItem["MJ21"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ21"]) / Conversion.Val(sItem["MJ21"])).ToString("0.000");
                                    sItem["QD22"] = Conversion.Val(sItem["MJ22"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ22"]) / Conversion.Val(sItem["MJ22"])).ToString("0.000");
                                    sItem["QD23"] = Conversion.Val(sItem["MJ23"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ23"]) / Conversion.Val(sItem["MJ23"])).ToString("0.000");
                                    sItem["QD24"] = Conversion.Val(sItem["MJ24"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ24"]) / Conversion.Val(sItem["MJ24"])).ToString("0.000");
                                    sItem["QD25"] = Conversion.Val(sItem["MJ25"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ25"]) / Conversion.Val(sItem["MJ25"])).ToString("0.000");
                                    sItem["QD26"] = Conversion.Val(sItem["MJ26"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ26"]) / Conversion.Val(sItem["MJ26"])).ToString("0.000");
                                }
                                if (Conversion.Val(sItem["QD21"]) != 0 && Conversion.Val(sItem["QD22"]) != 0 && Conversion.Val(sItem["QD23"]) != 0 && Conversion.Val(sItem["QD24"]) != 0 && Conversion.Val(sItem["QD25"]) != 0 && Conversion.Val(sItem["QD26"]) != 0)
                                {
                                    List<double> mtmpArray = new List<double>();
                                    string mlongStr = sItem["QD21"] + "," + sItem["QD22"] + "," + sItem["QD23"] + "," + sItem["QD24"] + "," + sItem["QD25"] + "," + sItem["QD26"];
                                    string[] str = mlongStr.Split(',');
                                    foreach (string s in str)
                                    {
                                        mtmpArray.Add(double.Parse(s));
                                    }
                                    mtmpArray.Sort();
                                    sItem["QD2"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                                }

                                if (Conversion.Val(sItem["QD2"]) >= g_Qd2 && sItem["PHJMPD2"].Trim() == "合格")
                                {
                                    MItem[0]["HG_QD2"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_QD2"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                    mAllHg = false;
                                }
                            }
                        }
                        else
                        {
                            sItem["QD2"] = "----";
                            sItem["PHJM2"] = "----";
                            sItem["PHJMPD2"] = "----";
                            MItem[0]["HG_QD2"] = "----";
                            MItem[0]["G_QD2"] = "----";
                            MItem[0]["G_PHJM2"] = "----";
                        }

                        #endregion

                        #region 拉伸粘结强度(耐冻融)、压剪粘结强度(耐冻融)、
                        if (jcxm.Contains("、拉伸粘结强度(耐冻融)、") || jcxm.Contains("、压剪粘结强度(耐冻融)、") || jcxm.Contains("、拉伸粘结耐冻融强度、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸粘结强度(耐冻融),压剪粘结强度(耐冻融),拉伸粘结耐冻融强度");
                            if (jcxm.Contains("、压剪粘结强度(耐冻融)、"))
                            {
                                sItem["MJ31"] = (Conversion.Val(sItem["CD31"]) * Conversion.Val(sItem["KD31"])).ToString();
                                sItem["MJ32"] = (Conversion.Val(sItem["CD32"]) * Conversion.Val(sItem["KD32"])).ToString();
                                sItem["MJ33"] = (Conversion.Val(sItem["CD33"]) * Conversion.Val(sItem["KD33"])).ToString();
                                sItem["MJ34"] = (Conversion.Val(sItem["CD34"]) * Conversion.Val(sItem["KD34"])).ToString();

                                if (Conversion.Val(sItem["MJ31"]) == 0 || Conversion.Val(sItem["MJ32"]) == 0 || Conversion.Val(sItem["MJ33"]) == 0 || Conversion.Val(sItem["MJ34"]) == 0)
                                {

                                }
                                else
                                {
                                    sItem["QD31"] = Conversion.Val(sItem["MJ31"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ31"]) / Conversion.Val(sItem["MJ31"])).ToString("0.000");
                                    sItem["QD32"] = Conversion.Val(sItem["MJ32"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ32"]) / Conversion.Val(sItem["MJ32"])).ToString("0.000");
                                    sItem["QD33"] = Conversion.Val(sItem["MJ33"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ33"]) / Conversion.Val(sItem["MJ33"])).ToString("0.000");
                                    sItem["QD34"] = Conversion.Val(sItem["MJ34"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ34"]) / Conversion.Val(sItem["MJ34"])).ToString("0.000");
                                }

                                if (Conversion.Val(sItem["QD31"]) != 0 && Conversion.Val(sItem["QD32"]) != 0 && Conversion.Val(sItem["QD33"]) != 0 && Conversion.Val(sItem["QD34"]) != 0)
                                {
                                    List<double> mtmpArray = new List<double>();
                                    string mlongStr = sItem["QD31"] + "," + sItem["QD32"] + "," + sItem["QD33"] + "," + sItem["QD34"];
                                    string[] str = mlongStr.Split(',');
                                    foreach (string s in str)
                                    {
                                        mtmpArray.Add(double.Parse(s));
                                    }
                                    mtmpArray.Sort();
                                    if ((mtmpArray[1] - mtmpArray[0]) / (mtmpArray[3] - mtmpArray[0]) >= 0.765)
                                    {
                                        sItem["QD3"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3]) / 3).ToString("0.00");
                                    }
                                    else
                                    {
                                        if ((mtmpArray[3] - mtmpArray[2]) / (mtmpArray[3] - mtmpArray[0]) >= 0.765)
                                        {
                                            sItem["QD3"] = ((mtmpArray[0] + mtmpArray[1] + mtmpArray[2]) / 3).ToString("0.00");
                                        }
                                        else
                                        {
                                            sItem["QD3"] = ((mtmpArray[0] + mtmpArray[1] + mtmpArray[2] + mtmpArray[3]) / 3).ToString("0.00");
                                        }
                                    }
                                }

                                if (Conversion.Val(sItem["QD3"]) >= g_Qd3)
                                {
                                    MItem[0]["HG_QD3"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_QD3"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                sItem["MJ31"] = (Conversion.Val(sItem["CD31"]) * Conversion.Val(sItem["KD31"])).ToString();
                                sItem["MJ32"] = (Conversion.Val(sItem["CD32"]) * Conversion.Val(sItem["KD32"])).ToString();
                                sItem["MJ33"] = (Conversion.Val(sItem["CD33"]) * Conversion.Val(sItem["KD33"])).ToString();
                                sItem["MJ34"] = (Conversion.Val(sItem["CD34"]) * Conversion.Val(sItem["KD34"])).ToString();
                                sItem["MJ35"] = (Conversion.Val(sItem["CD35"]) * Conversion.Val(sItem["KD35"])).ToString();
                                sItem["MJ36"] = (Conversion.Val(sItem["CD36"]) * Conversion.Val(sItem["KD36"])).ToString();

                                if (Conversion.Val(sItem["MJ31"]) == 0 || Conversion.Val(sItem["MJ32"]) == 0 || Conversion.Val(sItem["MJ33"]) == 0 || Conversion.Val(sItem["MJ34"]) == 0 || Conversion.Val(sItem["MJ35"]) == 0 || Conversion.Val(sItem["MJ36"]) == 0)
                                {

                                }
                                else
                                {
                                    sItem["QD31"] = Conversion.Val(sItem["MJ31"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ31"]) / Conversion.Val(sItem["MJ31"])).ToString("0.000");
                                    sItem["QD32"] = Conversion.Val(sItem["MJ32"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ32"]) / Conversion.Val(sItem["MJ32"])).ToString("0.000");
                                    sItem["QD33"] = Conversion.Val(sItem["MJ33"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ33"]) / Conversion.Val(sItem["MJ33"])).ToString("0.000");
                                    sItem["QD34"] = Conversion.Val(sItem["MJ34"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ34"]) / Conversion.Val(sItem["MJ34"])).ToString("0.000");
                                    sItem["QD35"] = Conversion.Val(sItem["MJ35"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ35"]) / Conversion.Val(sItem["MJ35"])).ToString("0.000");
                                    sItem["QD36"] = Conversion.Val(sItem["MJ36"]) == 0 ? "0.000" : (Conversion.Val(sItem["HZ36"]) / Conversion.Val(sItem["MJ36"])).ToString("0.000");
                                }

                                if (Conversion.Val(sItem["QD31"]) != 0 && Conversion.Val(sItem["QD32"]) != 0 && Conversion.Val(sItem["QD33"]) != 0 && Conversion.Val(sItem["QD34"]) != 0 && Conversion.Val(sItem["QD35"]) != 0 && Conversion.Val(sItem["QD36"]) != 0)
                                {
                                    List<double> mtmpArray = new List<double>();
                                    string mlongStr = sItem["QD31"] + "," + sItem["QD32"] + "," + sItem["QD33"] + "," + sItem["QD34"] + "," + sItem["QD35"] + "," + sItem["QD36"];
                                    string[] str = mlongStr.Split(',');
                                    foreach (string s in str)
                                    {
                                        mtmpArray.Add(double.Parse(s));
                                    }
                                    mtmpArray.Sort();
                                    sItem["QD3"] = ((mtmpArray[1] + mtmpArray[2] + mtmpArray[3] + mtmpArray[4]) / 4).ToString("0.00");
                                }


                                if (Conversion.Val(sItem["QD3"]) >= g_Qd3 && sItem["PHJMPD3"].Trim() == "合格")
                                {
                                    MItem[0]["HG_QD3"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_QD3"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                    mAllHg = false;
                                }
                            }
                        }
                        else
                        {
                            sItem["QD3"] = "----";
                            sItem["PHJM3"] = "----";
                            sItem["PHJMPD3"] = "----";
                            MItem[0]["HG_QD3"] = "----";
                            //MItem[0]["G_QD3"] = "----";
                            //MItem[0]["G_PHJM3"] = "----";
                        }
                        #endregion

                        #region 可操作时间、
                        if (jcxm.Contains("、可操作时间、"))
                        {
                            jcxmCur = "可操作时间";
                            if (Conversion.Val(sItem["KCZSJ"]) >= 1.5 && Conversion.Val(sItem["KCZSJ"]) <= 4)
                            {
                                MItem[0]["HG_KCZSJ"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KCZSJ"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["KCZSJ"] = "----";
                            MItem[0]["G_KCZSJ"] = "----";
                            MItem[0]["HG_KCZSJ"] = "----";
                        }
                        #endregion

                        #region 压折比
                        sItem["YZB"] = "----";
                        MItem[0]["G_YZB"] = "----";
                        MItem[0]["HG_YZB"] = "----";
                        #endregion
                    }
                }
            }

            #region 添加最终报告

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;


            //mrsmainTable!jsbeizhu = ""
            //            If CDec(mbhggs) = 0 Then
            //              mrsmainTable!jsbeizhu = "该组试样所检项目符合" + mrsmainTable!pdbz + "标准要求。"
            //              sitem["JCJG = "合格"
            //            End If


            //            If CDec(mbhggs) >= 1 Then
            //              mrsmainTable!jsbeizhu = "该组试样不符合" + mrsmainTable!pdbz + "标准要求。"
            //              sitem["JCJG = "不合格"
            //              If(mFlag_Bhg And mFlag_Hg) Then
            //               mrsmainTable!jsbeizhu = "该组试样所检项目部分符合" + mrsmainTable!pdbz + "标准要求。"
            //                End If
            //            End If



            //           mAllHg = IIf(mbhggs > 0, False, mAllHg)
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
