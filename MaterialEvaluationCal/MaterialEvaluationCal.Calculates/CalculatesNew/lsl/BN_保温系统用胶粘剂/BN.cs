using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class BN : BaseMethods
    {
        public void Calc()
        {
            #region
            string mlongStr;
            string[] mfuncVal = new string[5];
            string[] mtmpArray = new string[5];
            bool mAllHg = true, mFlag_Hg = true, mFlag_Bhg = true, canSetBgbh = true;
            int mbhggs = 0;
            string mJSFF = "";
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_BN"];
            var extraDJ = dataExtra["BZ_BN_DJ"];
            var MItem = data["M_BN"];
            if (!data.ContainsKey("M_BN"))
            {
                data["M_BN"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = "依据不详";
                m["JCJGSM"] = "依据不详";
                MItem.Add(m);
            }

            double mKyqd1, mKYQD2, mKyqd3, mKyqd4, mKyqd5, mKyqd6, mKyqd7, mKyqd8, mKyqd9, mKyqd10, mKyqd11, mKyqd12;
            double G_LSNJQD1 = 0, G_LSNJQD2 = 0, G_LSNJQD3 = 0, G_LSNJQD4 = 0, G_LSNJQD5 = 0, G_LSNJQD6 = 0, G_LSNJQD7 = 0, G_LSNJQD8 = 0;
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
                delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
                {
                    jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                    mbhggs = 0;
                    mFlag_Hg = false;
                    mFlag_Bhg = false;
                    var sign = false;
                    var WHICH = "";
                    sign = mItem["YCQK"].Contains("破坏") && mItem["YCQK"].Contains("水泥") ? false : true;

                    if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(干燥状态)、") && sign)
                    {
                        jcxmCur = "与水泥砂浆拉伸粘结强度(干燥状态)";
                        if (Conversion.Val(sItem["YQD1"]) == 0)
                        {
                            return false;

                        }

                        sItem["YQD1"] = Conversion.Val(sItem["YQD1"]).ToString("0.00");
                        mItem["HG_LSNJQD1"] = IsQualified(mItem["G_LSNJQD1"], sItem["YQD1"], false);

                        if (mItem["HG_LSNJQD1"] != "不合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mFlag_Bhg = true;
                            mbhggs = mbhggs + 1;

                        }
                    }
                    else
                    {
                        sItem["YQD1"] = "----";
                        mItem["HG_LSNJQD1"] = "----";
                        mItem["G_LSNJQD1"] = "----";
                    }

                    if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(浸水48h)、") && sign)
                    {
                        jcxmCur = "与水泥砂浆拉伸粘结强度(浸水48h)";
                        if (Conversion.Val(sItem["JSQD1"]) == 0)
                        {
                            return false;
                        }

                        sItem["JSQD1"] = Conversion.Val(sItem["JSQD1"]).ToString("0.0");
                        mItem["HG_LSNJQD2"] = IsQualified(mItem["G_LSNJQD2"], sItem["JSQD1"], false);

                        if (mItem["HG_LSNJQD2"] != "不合格")
                        {

                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mFlag_Bhg = true;
                            mbhggs = mbhggs + 1;

                        }
                    }
                    else
                    {
                        sItem["JSQD1"] = "----";
                        mItem["HG_LSNJQD2"] = "----";
                        mItem["G_LSNJQD2"] = "----";
                    }

                    WHICH = sign ? "bgbn" : "bgbn_10";
                    //mItem["WHICH") = IIf(sign, "bgbn", "bgbn_10")
                    if (!sign)
                    {
                        mItem["HG_LSNJQD1"] = "不合格";
                        mItem["HG_LSNJQD2"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }


                    if (jcxm.Contains("、与EPS板拉伸粘结强度(干燥状态)、"))
                    {
                        jcxmCur = "与EPS板拉伸粘结强度(干燥状态)";
                        if (Conversion.Val(sItem["YQD2"]) == 0 && sItem["YQDPHJM2"].Trim() == "")
                        {
                            return false;
                        }

                        sItem["YQD2"] = Conversion.Val(sItem["YQD2"]).ToString("0.00");

                        if (sItem["YQDPHJM2)"].Trim() == "EPS板破坏")
                        {
                            mItem["HG_LSNJQD3"] = IsQualified(mItem["G_LSNJQD3"], sItem["YQD2"], false);
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mItem["HG_LSNJQD3"] = "不合格";
                        }

                        if (mItem["HG_LSNJQD3"] != "不合格")
                        {
                            mbhggs += 1;
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["YQD2"] = "----";
                        mItem["HG_LSNJQD3"] = "----";
                        mItem["G_LSNJQD3"] = "----";
                        mItem["G_PHJM3"] = "----";
                    }


                    if (jcxm.Contains("、与EPS板拉伸粘结强度(浸水48h)、"))
                    {
                        jcxmCur = "与EPS板拉伸粘结强度(浸水48h)";
                        if (Conversion.Val(sItem["JSQD2"]) == 0 && sItem["JSPHJM2"].Trim() == "")
                        {
                            return false;

                        }

                        sItem["JSQD2"] = Conversion.Val(sItem["JSQD2"]).ToString("0.0");

                        if (sItem["JSPHJM2)"].Trim() == "EPS板破坏")
                        {
                            mItem["HG_LSNJQD4"] = IsQualified(mItem["G_LSNJQD4"], sItem["JSQD2"], false);
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mItem["HG_LSNJQD4"] = "不合格";
                        }

                        if (mItem["HG_LSNJQD4"] != "不合格")
                        {
                            mbhggs += 1;
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["JSQD2"] = "----";
                        mItem["HG_LSNJQD4"] = "----";
                        mItem["G_LSNJQD4"] = "----";
                        mItem["G_PHJM3"] = "----";
                    }


                    if (jcxm.Contains("、可操作时间、"))
                    {
                        jcxmCur = "可操作时间";
                        mItem["HG_KCZSJ"] = IsQualified(mItem["G_KCZSJ"], sItem["KCZSJ"], false);


                        if (mItem["HG_KCZSJ"] != "不合格")
                        {
                            mbhggs += 1;
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["KCZSJ"] = "----";
                        mItem["G_KCZSJ"] = "----";
                        mItem["HG_KCZSJ"] = "----";
                    }



                    //if (mItem["QRBM"].Contains("90"))
                    //{
                    //    mItem["HG_KCZSJ"] = IsQualified(mItem["G_KCZSJ"], sItem["KCZSJ"], false);


                    //    if (mItem["HG_KCZSJ"] != "不合格")
                    //    {
                    //        mbhggs += 1;
                    //        mFlag_Hg = true;
                    //    }
                    //    else
                    //    {
                    //        mFlag_Bhg = true;
                    //    }
                    //}
                    //else
                    //{
                    //    sItem["KCZSJ"] = "----";
                    //    mItem["G_KCZSJ"] = "----";
                    //    mItem["HG_KCZSJ"] = "----";
                    //}

                    if (mbhggs == 0)
                    {
                        jsbeizhu = "该组试样所检项目符合" + mItem["PDBZ"] + "标准要求。";
                        sItem["JCJG"] = "合格";
                    }
                    if (mbhggs > 0)
                    {
                        jsbeizhu = "该组试样所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + MItem[0]["PDBZ"] + "标准要求。";
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                    }

                    return mAllHg;
                };


            foreach (var sItem in SItem)
            {
                bool flag = true;
                bool mSFwc = true;
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                #region OLD
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJDJ"]);
                if (mrsDj != null)
                {
                    G_LSNJQD1 = GetSafeDouble(mrsDj["LSNJQD1"]);    //与水泥砂浆拉伸粘结原强度
                    G_LSNJQD2 = GetSafeDouble(mrsDj["LSNJQD2"]);    //与水泥砂浆拉伸粘结耐水强度(浸水48H,干燥2h)
                    G_LSNJQD3 = GetSafeDouble(mrsDj["LSNJQD3"]);    //与EPS板拉伸粘结原强度
                    G_LSNJQD4 = GetSafeDouble(mrsDj["LSNJQD4"]);    //与EPS板拉伸粘结耐水强度(浸水48H,干燥2h)
                    G_LSNJQD5 = GetSafeDouble(mrsDj["LSNJQD5"]);    //与水泥砂浆拉伸粘结耐冻融强度
                    G_LSNJQD6 = GetSafeDouble(mrsDj["LSNJQD6"]);    //与聚笨板拉伸粘结耐冻融强度
                    G_LSNJQD7 = GetSafeDouble(mrsDj["LSNJQD7"]);    //与水泥砂浆拉伸粘结耐水强度(浸水48H,干燥7d)
                    G_LSNJQD8 = GetSafeDouble(mrsDj["LSNJQD8"]);    //与模塑板拉伸粘结耐水强度(浸水48H,干燥7d)
                    MItem[0]["G_LSNJQD1"] = "≥" + mrsDj["LSNJQD1"];
                    MItem[0]["G_LSNJQD2"] = "≥" + mrsDj["LSNJQD2"];
                    MItem[0]["G_LSNJQD3"] = "≥" + mrsDj["LSNJQD3"];
                    MItem[0]["G_LSNJQD4"] = "≥" + mrsDj["LSNJQD4"];
                    MItem[0]["G_LSNJQD5"] = "≥" + mrsDj["LSNJQD5"];
                    MItem[0]["G_LSNJQD6"] = "≥" + mrsDj["LSNJQD6"];
                    MItem[0]["G_LSNJQD7"] = "≥" + mrsDj["LSNJQD7"];
                    MItem[0]["G_LSNJQD8"] = "≥" + mrsDj["LSNJQD8"];
                    MItem[0]["G_PHJM3"] = "破坏界面在EPS板内";
                    MItem[0]["G_PHJM4"] = "破坏界面在EPS板内";
                    MItem[0]["G_KCZSJ"] = G_LSNJQD3.ToString();
                    //MItem[0]["BGNAME"] = mrsDj["BGNAME"];
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "不下结论";
                    jsbeizhu = "依据不详";
                    mAllHg = false;
                }
                //if (!sItem["SJDJ"].Contains("144") && !sItem["SJDJ"].Contains("149"))
                //{
                //    sItem["JCJG"] = "依据不详";
                //    jsbeizhu = "依据不详";
                //    break;
                //}
                #endregion
                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    //mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
                    #region old
                    ////、、、、、旋转裂纹、旋转盖板、旋转缩松、旋转抗滑、旋转破坏、旋转力矩、旋转一般项、对接裂纹、对接盖板、对接缩松、对接抗拉、对接力矩、对接一般项
                    //if (mJSFF == "")
                    //{
                    //    if (sItem["SJDJ"].Contains("144"))
                    //    {
                    //        if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(干燥状态)、"))
                    //        {
                    //            if (0 == GetSafeDouble(sItem["YQD11"]))
                    //            {
                    //                mSFwc = false;
                    //            }

                    //            sItem["MJ11"] = (GetSafeDouble(sItem["CD11"]) * GetSafeDouble(sItem["KD11"])).ToString();
                    //            sItem["MJ12"] = (GetSafeDouble(sItem["CD12"]) * GetSafeDouble(sItem["KD12"])).ToString();
                    //            sItem["MJ13"] = (GetSafeDouble(sItem["CD13"]) * GetSafeDouble(sItem["KD13"])).ToString();
                    //            sItem["MJ14"] = (GetSafeDouble(sItem["CD14"]) * GetSafeDouble(sItem["KD14"])).ToString();
                    //            sItem["MJ15"] = (GetSafeDouble(sItem["CD15"]) * GetSafeDouble(sItem["KD15"])).ToString();
                    //            if (0 != GetSafeDouble(sItem["MJ11"]) || 0 != GetSafeDouble(sItem["MJ12"]) || 0 != GetSafeDouble(sItem["MJ13"])
                    //                || 0 != GetSafeDouble(sItem["MJ14"]) || 0 != GetSafeDouble(sItem["MJ15"]))
                    //            {
                    //                mKyqd1 = Round(GetSafeDouble(sItem["YQD11"]) / GetSafeDouble(sItem["MJ11"]), 3);
                    //                mKYQD2 = Round(GetSafeDouble(sItem["YQD12"]) / GetSafeDouble(sItem["MJ12"]), 3);
                    //                mKyqd3 = Round(GetSafeDouble(sItem["YQD13"]) / GetSafeDouble(sItem["MJ13"]), 3);
                    //                mKyqd4 = Round(GetSafeDouble(sItem["YQD14"]) / GetSafeDouble(sItem["MJ14"]), 3);
                    //                mKyqd5 = Round(GetSafeDouble(sItem["YQD15"]) / GetSafeDouble(sItem["MJ15"]), 3);

                    //                sItem["NJQD11"] = Round(mKyqd1, 3).ToString();
                    //                sItem["NJQD12"] = Round(mKYQD2, 3).ToString();
                    //                sItem["NJQD13"] = Round(mKyqd3, 3).ToString();
                    //                sItem["NJQD14"] = Round(mKyqd4, 3).ToString();
                    //                sItem["NJQD15"] = Round(mKyqd5, 3).ToString();
                    //            }
                    //            if (0 != GetSafeDouble(sItem["NJQD11"]) || 0 != GetSafeDouble(sItem["NJQD12"]) || 0 != GetSafeDouble(sItem["NJQD13"])
                    //                || 0 != GetSafeDouble(sItem["NJQD14"]) || 0 != GetSafeDouble(sItem["NJQD15"]))
                    //            {
                    //                sItem["YQD1"] = Round(GetSafeDouble(sItem["NJQD11"]) + GetSafeDouble(sItem["NJQD12"]) + GetSafeDouble(sItem["NJQD13"]) +
                    //                    GetSafeDouble(sItem["NJQD14"]) + GetSafeDouble(sItem["NJQD15"]), 2).ToString();
                    //            }
                    //            if (GetSafeDouble(sItem["YQD1"]) >= G_LSNJQD1)
                    //            {
                    //                MItem[0]["HG_LSNJQD1"] = "合格";
                    //                mFlag_Hg = true;
                    //            }
                    //            else
                    //            {
                    //                MItem[0]["HG_LSNJQD1"] = "不合格";
                    //                mbhggs++;
                    //                mFlag_Bhg = true;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            sItem["YQD1"] = "----";
                    //            MItem[0]["HG_LSNJQD1"] = "----";
                    //            MItem[0]["G_LSNJQD1"] = "----";
                    //        }

                    //        if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(浸水48h)、"))
                    //        {
                    //            if (0 == GetSafeDouble(sItem["JSQD11"]))
                    //            {
                    //                mSFwc = false;
                    //            }
                    //            sItem["MJ21"] = (GetSafeDouble(sItem["CD21"]) * GetSafeDouble(sItem["KD21"])).ToString();
                    //            sItem["MJ22"] = (GetSafeDouble(sItem["CD22"]) * GetSafeDouble(sItem["KD22"])).ToString();
                    //            sItem["MJ23"] = (GetSafeDouble(sItem["CD23"]) * GetSafeDouble(sItem["KD23"])).ToString();
                    //            sItem["MJ24"] = (GetSafeDouble(sItem["CD24"]) * GetSafeDouble(sItem["KD24"])).ToString();
                    //            sItem["MJ25"] = (GetSafeDouble(sItem["CD25"]) * GetSafeDouble(sItem["KD25"])).ToString();
                    //            if (0 != GetSafeDouble(sItem["MJ21"]) || 0 != GetSafeDouble(sItem["MJ22"]) || 0 != GetSafeDouble(sItem["MJ23"])
                    //                || 0 != GetSafeDouble(sItem["MJ24"]) || 0 != GetSafeDouble(sItem["MJ25"]))
                    //            {
                    //                mKyqd1 = Round(GetSafeDouble(sItem["JSQD11"]) / GetSafeDouble(sItem["MJ21"]), 3);
                    //                mKYQD2 = Round(GetSafeDouble(sItem["JSQD12"]) / GetSafeDouble(sItem["MJ22"]), 3);
                    //                mKyqd3 = Round(GetSafeDouble(sItem["JSQD13"]) / GetSafeDouble(sItem["MJ23"]), 3);
                    //                mKyqd4 = Round(GetSafeDouble(sItem["JSQD14"]) / GetSafeDouble(sItem["MJ24"]), 3);
                    //                mKyqd5 = Round(GetSafeDouble(sItem["JSQD15"]) / GetSafeDouble(sItem["MJ25"]), 3);

                    //                sItem["NJQD21"] = Round(mKyqd1, 3).ToString();
                    //                sItem["NJQD22"] = Round(mKYQD2, 3).ToString();
                    //                sItem["NJQD23"] = Round(mKyqd3, 3).ToString();
                    //                sItem["NJQD24"] = Round(mKyqd4, 3).ToString();
                    //                sItem["NJQD25"] = Round(mKyqd5, 3).ToString();
                    //            }
                    //            //0  != GetSafeDouble(sItem["NJQD22"])|| 
                    //            if (0 != GetSafeDouble(sItem["NJQD21"]) || 0 != GetSafeDouble(sItem["NJQD23"])
                    //                || 0 != GetSafeDouble(sItem["NJQD24"]) || 0 != GetSafeDouble(sItem["NJQD25"]))
                    //            {
                    //                //GetSafeDouble(sItem["NJQD22"]) + 
                    //                sItem["JSQD1"] = Round(GetSafeDouble(sItem["NJQD21"]) + GetSafeDouble(sItem["NJQD23"]) +
                    //                    GetSafeDouble(sItem["NJQD24"]) + GetSafeDouble(sItem["NJQD25"]), 2).ToString();
                    //            }
                    //            else
                    //            {
                    //                canSetBgbh = false;
                    //            }
                    //            if (GetSafeDouble(sItem["JSQD1"]) >= G_LSNJQD2)
                    //            {
                    //                MItem[0]["HG_LSNJQD2"] = "合格";
                    //                mFlag_Hg = true;
                    //            }
                    //            else
                    //            {
                    //                MItem[0]["HG_LSNJQD2"] = "不合格";
                    //                mbhggs++;
                    //                mFlag_Bhg = true;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            sItem["JSQD1"] = "----";
                    //            MItem[0]["HG_LSNJQD2"] = "----";
                    //            MItem[0]["G_LSNJQD2"] = "----";
                    //        }

                    //        if (jcxm.Contains("、与EPS板拉伸粘结强度(干燥状态)、"))
                    //        {
                    //            if (0 == GetSafeDouble(sItem["YQD21"]))
                    //            {
                    //                mSFwc = false;
                    //            }
                    //            sItem["MJ31"] = (GetSafeDouble(sItem["CD31"]) * GetSafeDouble(sItem["KD31"])).ToString();
                    //            sItem["MJ32"] = (GetSafeDouble(sItem["CD32"]) * GetSafeDouble(sItem["KD32"])).ToString();
                    //            sItem["MJ33"] = (GetSafeDouble(sItem["CD33"]) * GetSafeDouble(sItem["KD33"])).ToString();
                    //            sItem["MJ34"] = (GetSafeDouble(sItem["CD34"]) * GetSafeDouble(sItem["KD34"])).ToString();
                    //            sItem["MJ35"] = (GetSafeDouble(sItem["CD35"]) * GetSafeDouble(sItem["KD35"])).ToString();
                    //            if (0 != GetSafeDouble(sItem["MJ31"]) || 0 != GetSafeDouble(sItem["MJ32"]) || 0 != GetSafeDouble(sItem["MJ33"])
                    //                || 0 != GetSafeDouble(sItem["MJ34"]) || 0 != GetSafeDouble(sItem["MJ35"]))
                    //            {
                    //                mKyqd1 = Round(GetSafeDouble(sItem["YQD21"]) / GetSafeDouble(sItem["MJ31"]), 3);
                    //                mKYQD2 = Round(GetSafeDouble(sItem["YQD22"]) / GetSafeDouble(sItem["MJ32"]), 3);
                    //                mKyqd3 = Round(GetSafeDouble(sItem["YQD23"]) / GetSafeDouble(sItem["MJ33"]), 3);
                    //                mKyqd4 = Round(GetSafeDouble(sItem["YQD24"]) / GetSafeDouble(sItem["MJ34"]), 3);
                    //                mKyqd5 = Round(GetSafeDouble(sItem["YQD25"]) / GetSafeDouble(sItem["MJ35"]), 3);

                    //                sItem["NJQD31"] = Round(mKyqd1, 3).ToString();
                    //                sItem["NJQD32"] = Round(mKYQD2, 3).ToString();
                    //                sItem["NJQD33"] = Round(mKyqd3, 3).ToString();
                    //                sItem["NJQD34"] = Round(mKyqd4, 3).ToString();
                    //                sItem["NJQD35"] = Round(mKyqd5, 3).ToString();
                    //            }
                    //            if (0 != GetSafeDouble(sItem["NJQD31"]) || GetSafeDouble(sItem["NJQD32"]) != 0 || 0 != GetSafeDouble(sItem["NJQD33"])
                    //                || 0 != GetSafeDouble(sItem["NJQD34"]) || 0 != GetSafeDouble(sItem["NJQD35"]))
                    //            {
                    //                sItem["YQD2"] = Round(GetSafeDouble(sItem["NJQD31"]) + GetSafeDouble(sItem["NJQD32"]) + GetSafeDouble(sItem["NJQD33"]) +
                    //                    GetSafeDouble(sItem["NJQD34"]) + GetSafeDouble(sItem["NJQD35"]), 2).ToString();
                    //            }
                    //            //null 不进入
                    //            if (!string.IsNullOrEmpty(sItem["PHJMPD1"]))
                    //            {
                    //                if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3 && sItem["PHJMPD1"] == "合格")//
                    //                {
                    //                    MItem[0]["HG_LSNJQD3"] = "合格";
                    //                    mFlag_Hg = true;
                    //                }
                    //                else
                    //                {
                    //                    MItem[0]["HG_LSNJQD3"] = "不合格";
                    //                    mbhggs++;
                    //                    mFlag_Bhg = true;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3)//
                    //                {
                    //                    MItem[0]["HG_LSNJQD3"] = "合格";
                    //                    mFlag_Hg = true;
                    //                }
                    //                else
                    //                {
                    //                    MItem[0]["HG_LSNJQD3"] = "不合格";
                    //                    mbhggs++;
                    //                    mFlag_Bhg = true;
                    //                }
                    //            }

                    //        }
                    //        else
                    //        {
                    //            sItem["YQD2"] = "----";
                    //            MItem[0]["HG_LSNJQD3"] = "----";
                    //            MItem[0]["G_LSNJQD3"] = "----";
                    //            MItem[0]["G_PHJM3"] = "----";
                    //        }

                    //        //
                    //        if (jcxm.Contains("、与EPS板拉伸粘结强度(浸水48h)、"))
                    //        {
                    //            if (0 == GetSafeDouble(sItem["JSQD21"]))
                    //            {
                    //                mSFwc = false;
                    //            }
                    //            sItem["MJ41"] = (GetSafeDouble(sItem["CD41"]) * GetSafeDouble(sItem["KD41"])).ToString();
                    //            sItem["MJ42"] = (GetSafeDouble(sItem["CD42"]) * GetSafeDouble(sItem["KD42"])).ToString();
                    //            sItem["MJ43"] = (GetSafeDouble(sItem["CD43"]) * GetSafeDouble(sItem["KD43"])).ToString();
                    //            sItem["MJ44"] = (GetSafeDouble(sItem["CD44"]) * GetSafeDouble(sItem["KD44"])).ToString();
                    //            sItem["MJ45"] = (GetSafeDouble(sItem["CD45"]) * GetSafeDouble(sItem["KD45"])).ToString();
                    //            if (0 != GetSafeDouble(sItem["MJ41"]) || 0 != GetSafeDouble(sItem["MJ42"]) || 0 != GetSafeDouble(sItem["MJ43"])
                    //                || 0 != GetSafeDouble(sItem["MJ44"]) || 0 != GetSafeDouble(sItem["MJ45"]))
                    //            {
                    //                mKyqd1 = Round(GetSafeDouble(sItem["JSQD21"]) / GetSafeDouble(sItem["MJ41"]), 3);
                    //                mKYQD2 = Round(GetSafeDouble(sItem["JSQD22"]) / GetSafeDouble(sItem["MJ42"]), 3);
                    //                mKyqd3 = Round(GetSafeDouble(sItem["JSQD23"]) / GetSafeDouble(sItem["MJ43"]), 3);
                    //                mKyqd4 = Round(GetSafeDouble(sItem["JSQD24"]) / GetSafeDouble(sItem["MJ44"]), 3);
                    //                mKyqd5 = Round(GetSafeDouble(sItem["JSQD25"]) / GetSafeDouble(sItem["MJ45"]), 3);

                    //                sItem["NJQD41"] = Round(mKyqd1, 3).ToString();
                    //                sItem["NJQD42"] = Round(mKYQD2, 3).ToString();
                    //                sItem["NJQD43"] = Round(mKyqd3, 3).ToString();
                    //                sItem["NJQD44"] = Round(mKyqd4, 3).ToString();
                    //                sItem["NJQD45"] = Round(mKyqd5, 3).ToString();
                    //            }
                    //            if (0 != GetSafeDouble(sItem["NJQD41"]) || 0 != GetSafeDouble(sItem["NJQD42"]) || 0 != GetSafeDouble(sItem["NJQD43"])
                    //                || 0 != GetSafeDouble(sItem["NJQD44"]) || 0 != GetSafeDouble(sItem["NJQD45"]))
                    //            {
                    //                sItem["JSQD2"] = Round(GetSafeDouble(sItem["NJQD41"]) + GetSafeDouble(sItem["NJQD42"]) + GetSafeDouble(sItem["NJQD43"]) +
                    //                    GetSafeDouble(sItem["NJQD44"]) + GetSafeDouble(sItem["NJQD45"]), 2).ToString();
                    //            }
                    //            else
                    //            {
                    //                canSetBgbh = false;
                    //            }
                    //            if (!string.IsNullOrEmpty(sItem["PHJMPD2"]))
                    //            {
                    //                if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4 && sItem["PHJMPD2"] == "合格")
                    //                {
                    //                    MItem[0]["HG_LSNJQD4"] = "合格";
                    //                    mFlag_Hg = true;
                    //                }
                    //                else
                    //                {
                    //                    MItem[0]["HG_LSNJQD4"] = "不合格";
                    //                    mbhggs++;
                    //                    mFlag_Bhg = true;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4)
                    //                {
                    //                    MItem[0]["HG_LSNJQD4"] = "合格";
                    //                    mFlag_Hg = true;
                    //                }
                    //                else
                    //                {
                    //                    MItem[0]["HG_LSNJQD4"] = "不合格";
                    //                    mbhggs++;
                    //                    mFlag_Bhg = true;
                    //                }
                    //            }

                    //        }
                    //        else
                    //        {
                    //            sItem["JSQD2"] = "----";
                    //            MItem[0]["HG_LSNJQD4"] = "----";
                    //            MItem[0]["G_LSNJQD4"] = "----";
                    //            MItem[0]["G_PHJM3"] = "----";
                    //        }
                    //        sItem["KCZSJ"] = "----";
                    //        MItem[0]["G_KCZSJ"] = "----";
                    //        MItem[0]["HG_KCZSJ"] = "----";
                    //    }
                    //    else
                    //    {
                    //        if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(干燥状态)、"))
                    //        {
                    //            if (0 == GetSafeDouble(sItem["YQD11"]))
                    //            {
                    //                mSFwc = false;
                    //            }
                    //            sItem["MJ11"] = (GetSafeDouble(sItem["CD11"]) * GetSafeDouble(sItem["KD11"])).ToString();
                    //            sItem["MJ12"] = (GetSafeDouble(sItem["CD12"]) * GetSafeDouble(sItem["KD12"])).ToString();
                    //            sItem["MJ13"] = (GetSafeDouble(sItem["CD13"]) * GetSafeDouble(sItem["KD13"])).ToString();
                    //            sItem["MJ14"] = (GetSafeDouble(sItem["CD14"]) * GetSafeDouble(sItem["KD14"])).ToString();
                    //            sItem["MJ15"] = (GetSafeDouble(sItem["CD15"]) * GetSafeDouble(sItem["KD15"])).ToString();
                    //            sItem["MJ16"] = (GetSafeDouble(sItem["CD16"]) * GetSafeDouble(sItem["KD16"])).ToString();
                    //            if (0 != GetSafeDouble(sItem["MJ11"]) || 0 != GetSafeDouble(sItem["MJ12"]) || 0 != GetSafeDouble(sItem["MJ13"])
                    //                || 0 != GetSafeDouble(sItem["MJ14"]) || 0 != GetSafeDouble(sItem["MJ15"]) || 0 != GetSafeDouble(sItem["MJ16"]))
                    //            {
                    //                mKyqd1 = Round(GetSafeDouble(sItem["YQD11"]) / GetSafeDouble(sItem["MJ11"]), 3);
                    //                mKYQD2 = Round(GetSafeDouble(sItem["YQD12"]) / GetSafeDouble(sItem["MJ12"]), 3);
                    //                mKyqd3 = Round(GetSafeDouble(sItem["YQD13"]) / GetSafeDouble(sItem["MJ13"]), 3);
                    //                mKyqd4 = Round(GetSafeDouble(sItem["YQD14"]) / GetSafeDouble(sItem["MJ14"]), 3);
                    //                mKyqd5 = Round(GetSafeDouble(sItem["YQD15"]) / GetSafeDouble(sItem["MJ15"]), 3);
                    //                mKyqd6 = Round(GetSafeDouble(sItem["YQD16"]) / GetSafeDouble(sItem["MJ16"]), 3);

                    //                sItem["NJQD11"] = Round(mKyqd1, 3).ToString();
                    //                sItem["NJQD12"] = Round(mKYQD2, 3).ToString();
                    //                sItem["NJQD13"] = Round(mKyqd3, 3).ToString();
                    //                sItem["NJQD14"] = Round(mKyqd4, 3).ToString();
                    //                sItem["NJQD15"] = Round(mKyqd5, 3).ToString();
                    //                sItem["NJQD16"] = Round(mKyqd6, 3).ToString();
                    //            }
                    //            if (0 != GetSafeDouble(sItem["NJQD11"]) || 0 != GetSafeDouble(sItem["NJQD12"]) || 0 != GetSafeDouble(sItem["NJQD13"])
                    //                || 0 != GetSafeDouble(sItem["NJQD14"]) || 0 != GetSafeDouble(sItem["NJQD15"]) || 0 != GetSafeDouble(sItem["NJQD16"]))
                    //            {
                    //                mlongStr = sItem["NJQD11"] + "," + sItem["NJQD12"] + "," + sItem["NJQD13"] + "," + sItem["NJQD14"] + "," + sItem["NJQD15"] + "," + sItem["NJQD16"];
                    //                mtmpArray = mlongStr.Split(',');
                    //                for (int i = 0; i < 5; i++)
                    //                {
                    //                    mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                    //                }
                    //                mkyqdArray.Sort();
                    //                sItem["YQD1"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                    //            }
                    //            if (GetSafeDouble(sItem["YQD1"]) >= G_LSNJQD1)
                    //            {
                    //                MItem[0]["HG_LSNJQD1"] = "合格";
                    //                mFlag_Hg = true;
                    //            }
                    //            else
                    //            {
                    //                MItem[0]["HG_LSNJQD1"] = "不合格";
                    //                mbhggs++;
                    //                mFlag_Bhg = true;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            sItem["YQD1"] = "----";
                    //            MItem[0]["HG_LSNJQD1"] = "----";
                    //            MItem[0]["G_LSNJQD1"] = "----";
                    //        }

                    //        if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(浸水48h)、"))
                    //        {
                    //            if (0 == GetSafeDouble(sItem["JSQD11"]))
                    //            {
                    //                mSFwc = false;
                    //            }
                    //            sItem["MJ21"] = (GetSafeDouble(sItem["CD21"]) * GetSafeDouble(sItem["KD21"])).ToString();
                    //            sItem["MJ22"] = (GetSafeDouble(sItem["CD22"]) * GetSafeDouble(sItem["KD22"])).ToString();
                    //            sItem["MJ23"] = (GetSafeDouble(sItem["CD23"]) * GetSafeDouble(sItem["KD23"])).ToString();
                    //            sItem["MJ24"] = (GetSafeDouble(sItem["CD24"]) * GetSafeDouble(sItem["KD24"])).ToString();
                    //            sItem["MJ25"] = (GetSafeDouble(sItem["CD25"]) * GetSafeDouble(sItem["KD25"])).ToString();
                    //            sItem["MJ26"] = (GetSafeDouble(sItem["CD26"]) * GetSafeDouble(sItem["KD26"])).ToString();
                    //            if (0 != GetSafeDouble(sItem["MJ21"]) || 0 != GetSafeDouble(sItem["MJ22"]) || 0 != GetSafeDouble(sItem["MJ23"])
                    //                || 0 != GetSafeDouble(sItem["MJ24"]) || 0 != GetSafeDouble(sItem["MJ25"]) || 0 != GetSafeDouble(sItem["MJ26"]))
                    //            {
                    //                mKyqd1 = Round(GetSafeDouble(sItem["JSQD11"]) / GetSafeDouble(sItem["MJ21"]), 3);
                    //                mKYQD2 = Round(GetSafeDouble(sItem["JSQD12"]) / GetSafeDouble(sItem["MJ22"]), 3);
                    //                mKyqd3 = Round(GetSafeDouble(sItem["JSQD13"]) / GetSafeDouble(sItem["MJ23"]), 3);
                    //                mKyqd4 = Round(GetSafeDouble(sItem["JSQD14"]) / GetSafeDouble(sItem["MJ24"]), 3);
                    //                mKyqd5 = Round(GetSafeDouble(sItem["JSQD15"]) / GetSafeDouble(sItem["MJ25"]), 3);
                    //                mKyqd6 = Round(GetSafeDouble(sItem["JSQD16"]) / GetSafeDouble(sItem["MJ26"]), 3);

                    //                sItem["NJQD21"] = Round(mKyqd1, 3).ToString();
                    //                //sItem["NJQD22"] = Round(mKYQD2, 3).ToString();
                    //                sItem["NJQD23"] = Round(mKyqd3, 3).ToString();
                    //                sItem["NJQD24"] = Round(mKyqd4, 3).ToString();
                    //                sItem["NJQD25"] = Round(mKyqd5, 3).ToString();
                    //                sItem["NJQD26"] = Round(mKyqd6, 3).ToString();
                    //            }
                    //            //|| 0 != GetSafeDouble(sItem["NJQD22"]) 
                    //            if (0 != GetSafeDouble(sItem["NJQD21"]) || 0 != GetSafeDouble(sItem["NJQD23"])
                    //                || 0 != GetSafeDouble(sItem["NJQD24"]) || 0 != GetSafeDouble(sItem["NJQD25"]) || 0 != GetSafeDouble(sItem["NJQD26"]))
                    //            {
                    //                //"," + sItem["NJQD22"] +
                    //                mlongStr = sItem["NJQD21"] + "," + sItem["NJQD23"] + "," + sItem["NJQD24"] + "," + sItem["NJQD25"] + "," + sItem["NJQD26"];
                    //                mtmpArray = mlongStr.Split(',');
                    //                for (int i = 0; i < 5; i++)
                    //                {
                    //                    mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                    //                }
                    //                mkyqdArray.Sort();
                    //                sItem["JSQD1"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                    //            }
                    //            else
                    //            {
                    //                canSetBgbh = false;
                    //            }
                    //            if (GetSafeDouble(sItem["JSQD1"]) >= G_LSNJQD2)
                    //            {
                    //                MItem[0]["HG_LSNJQD2"] = "合格";
                    //                mFlag_Hg = true;
                    //            }
                    //            else
                    //            {
                    //                MItem[0]["HG_LSNJQD2"] = "不合格";
                    //                mbhggs++;
                    //                mFlag_Bhg = true;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            sItem["JSQD1"] = "----";
                    //            MItem[0]["HG_LSNJQD2"] = "----";
                    //            MItem[0]["G_LSNJQD2"] = "----";
                    //        }

                    //        if (jcxm.Contains("、与EPS板拉伸粘结强度(干燥状态)、"))
                    //        {
                    //            if (0 == GetSafeDouble(sItem["YQD21"]))
                    //            {
                    //                mSFwc = false;
                    //            }
                    //            sItem["MJ31"] = (GetSafeDouble(sItem["CD31"]) * GetSafeDouble(sItem["KD31"])).ToString();
                    //            sItem["MJ32"] = (GetSafeDouble(sItem["CD32"]) * GetSafeDouble(sItem["KD32"])).ToString();
                    //            sItem["MJ33"] = (GetSafeDouble(sItem["CD33"]) * GetSafeDouble(sItem["KD33"])).ToString();
                    //            sItem["MJ34"] = (GetSafeDouble(sItem["CD34"]) * GetSafeDouble(sItem["KD34"])).ToString();
                    //            sItem["MJ35"] = (GetSafeDouble(sItem["CD35"]) * GetSafeDouble(sItem["KD35"])).ToString();
                    //            sItem["MJ36"] = (GetSafeDouble(sItem["CD36"]) * GetSafeDouble(sItem["KD36"])).ToString();
                    //            if (0 != GetSafeDouble(sItem["MJ31"]) || 0 != GetSafeDouble(sItem["MJ32"]) || 0 != GetSafeDouble(sItem["MJ33"])
                    //                || 0 != GetSafeDouble(sItem["MJ34"]) || 0 != GetSafeDouble(sItem["MJ35"]) || 0 != GetSafeDouble(sItem["MJ36"]))
                    //            {
                    //                mKyqd1 = Round(GetSafeDouble(sItem["YQD21"]) / GetSafeDouble(sItem["MJ31"]), 3);
                    //                mKYQD2 = Round(GetSafeDouble(sItem["YQD22"]) / GetSafeDouble(sItem["MJ32"]), 3);
                    //                mKyqd3 = Round(GetSafeDouble(sItem["YQD23"]) / GetSafeDouble(sItem["MJ33"]), 3);
                    //                mKyqd4 = Round(GetSafeDouble(sItem["YQD24"]) / GetSafeDouble(sItem["MJ34"]), 3);
                    //                mKyqd5 = Round(GetSafeDouble(sItem["YQD25"]) / GetSafeDouble(sItem["MJ35"]), 3);
                    //                mKyqd6 = Round(GetSafeDouble(sItem["YQD26"]) / GetSafeDouble(sItem["MJ36"]), 3);

                    //                sItem["NJQD31"] = Round(mKyqd1, 3).ToString();
                    //                sItem["NJQD32"] = Round(mKYQD2, 3).ToString();
                    //                sItem["NJQD33"] = Round(mKyqd3, 3).ToString();
                    //                sItem["NJQD34"] = Round(mKyqd4, 3).ToString();
                    //                sItem["NJQD35"] = Round(mKyqd5, 3).ToString();
                    //                sItem["NJQD36"] = Round(mKyqd6, 3).ToString();
                    //            }
                    //            if (0 != GetSafeDouble(sItem["NJQD31"]) || 0 != GetSafeDouble(sItem["NJQD32"]) || 0 != GetSafeDouble(sItem["NJQD33"])
                    //                || 0 != GetSafeDouble(sItem["NJQD34"]) || 0 != GetSafeDouble(sItem["NJQD35"]) || 0 != GetSafeDouble(sItem["NJQD36"]))
                    //            {
                    //                mlongStr = sItem["NJQD31"] + "," + sItem["NJQD32"] + "," + sItem["NJQD33"] + "," + sItem["NJQD34"] + "," + sItem["NJQD35"] + "," + sItem["NJQD36"];
                    //                mtmpArray = mlongStr.Split(',');
                    //                for (int i = 0; i < 5; i++)
                    //                {
                    //                    mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                    //                }
                    //                mkyqdArray.Sort();
                    //                sItem["YQD2"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                    //            }
                    //            if (!string.IsNullOrEmpty(sItem["PHJMPD1"]))
                    //            {
                    //                if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3 && sItem["PHJMPD1"].Trim() == "合格")
                    //                {
                    //                    MItem[0]["HG_LSNJQD3"] = "合格";
                    //                    mFlag_Hg = true;
                    //                }
                    //                else
                    //                {
                    //                    MItem[0]["HG_LSNJQD3"] = "不合格";
                    //                    mbhggs++;
                    //                    mFlag_Bhg = true;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3)
                    //                {
                    //                    MItem[0]["HG_LSNJQD3"] = "合格";
                    //                    mFlag_Hg = true;
                    //                }
                    //                else
                    //                {
                    //                    MItem[0]["HG_LSNJQD3"] = "不合格";
                    //                    mbhggs++;
                    //                    mFlag_Bhg = true;
                    //                }
                    //            }

                    //        }
                    //        else
                    //        {
                    //            sItem["YQD2"] = "----";
                    //            MItem[0]["HG_LSNJQD3"] = "----";
                    //            MItem[0]["G_LSNJQD3"] = "----";
                    //            MItem[0]["G_PHJM3"] = "----";
                    //        }

                    //        if (jcxm.Contains("、与EPS板拉伸粘结强度(浸水48h)、"))
                    //        {
                    //            if (0 == GetSafeDouble(sItem["JSQD11"]))
                    //            {
                    //                mSFwc = false;
                    //            }
                    //            sItem["MJ41"] = (GetSafeDouble(sItem["CD41"]) * GetSafeDouble(sItem["KD41"])).ToString();
                    //            sItem["MJ42"] = (GetSafeDouble(sItem["CD42"]) * GetSafeDouble(sItem["KD42"])).ToString();
                    //            sItem["MJ43"] = (GetSafeDouble(sItem["CD43"]) * GetSafeDouble(sItem["KD43"])).ToString();
                    //            sItem["MJ44"] = (GetSafeDouble(sItem["CD44"]) * GetSafeDouble(sItem["KD44"])).ToString();
                    //            sItem["MJ45"] = (GetSafeDouble(sItem["CD45"]) * GetSafeDouble(sItem["KD45"])).ToString();
                    //            sItem["MJ46"] = (GetSafeDouble(sItem["CD46"]) * GetSafeDouble(sItem["KD46"])).ToString();
                    //            if (0 != GetSafeDouble(sItem["MJ41"]) || 0 != GetSafeDouble(sItem["MJ42"]) || 0 != GetSafeDouble(sItem["MJ43"])
                    //                || 0 != GetSafeDouble(sItem["MJ44"]) || 0 != GetSafeDouble(sItem["MJ45"]) || 0 != GetSafeDouble(sItem["MJ46"]))
                    //            {
                    //                mKyqd1 = Round(GetSafeDouble(sItem["JSQD21"]) / GetSafeDouble(sItem["MJ41"]), 3);
                    //                mKYQD2 = Round(GetSafeDouble(sItem["JSQD22"]) / GetSafeDouble(sItem["MJ42"]), 3);
                    //                mKyqd3 = Round(GetSafeDouble(sItem["JSQD23"]) / GetSafeDouble(sItem["MJ43"]), 3);
                    //                mKyqd4 = Round(GetSafeDouble(sItem["JSQD24"]) / GetSafeDouble(sItem["MJ44"]), 3);
                    //                mKyqd5 = Round(GetSafeDouble(sItem["JSQD25"]) / GetSafeDouble(sItem["MJ45"]), 3);
                    //                mKyqd6 = Round(GetSafeDouble(sItem["JSQD26"]) / GetSafeDouble(sItem["MJ46"]), 3);

                    //                sItem["NJQD21"] = Round(mKyqd1, 3).ToString();
                    //                //sItem["NJQD22"] = Round(mKYQD2, 3).ToString();
                    //                sItem["NJQD23"] = Round(mKyqd3, 3).ToString();
                    //                sItem["NJQD24"] = Round(mKyqd4, 3).ToString();
                    //                sItem["NJQD25"] = Round(mKyqd5, 3).ToString();
                    //                sItem["NJQD26"] = Round(mKyqd6, 3).ToString();
                    //            }
                    //            if (0 != GetSafeDouble(sItem["NJQD41"]) || 0 != GetSafeDouble(sItem["NJQD42"]) || 0 != GetSafeDouble(sItem["NJQD43"])
                    //                || 0 != GetSafeDouble(sItem["NJQD44"]) || 0 != GetSafeDouble(sItem["NJQD45"]) || 0 != GetSafeDouble(sItem["NJQD46"]))
                    //            {
                    //                mlongStr = sItem["NJQD41"] + "," + sItem["NJQD42"] + "," + sItem["NJQD43"] + "," + sItem["NJQD44"] + "," + sItem["NJQD45"] + "," + sItem["NJQD46"];
                    //                mtmpArray = mlongStr.Split(',');
                    //                for (int i = 0; i < 5; i++)
                    //                {
                    //                    mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                    //                }
                    //                mkyqdArray.Sort();
                    //                sItem["JSQD2"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                    //            }
                    //            else
                    //            {
                    //                canSetBgbh = false;
                    //            }
                    //            if (!string.IsNullOrEmpty(sItem["PHJMPD2"]))
                    //            {
                    //                if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4 && sItem["PHJMPD2"] == "合格")//
                    //                {
                    //                    MItem[0]["HG_LSNJQD4"] = "合格";
                    //                    mFlag_Hg = true;
                    //                }
                    //                else
                    //                {
                    //                    MItem[0]["HG_LSNJQD4"] = "不合格";
                    //                    mbhggs++;
                    //                    mFlag_Bhg = true;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4)//
                    //                {
                    //                    MItem[0]["HG_LSNJQD4"] = "合格";
                    //                    mFlag_Hg = true;
                    //                }
                    //                else
                    //                {
                    //                    MItem[0]["HG_LSNJQD4"] = "不合格";
                    //                    mbhggs++;
                    //                    mFlag_Bhg = true;
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            sItem["JSQD2"] = "----";
                    //            MItem[0]["HG_LSNJQD4"] = "----";
                    //            MItem[0]["G_LSNJQD4"] = "----";
                    //            MItem[0]["G_PHJM4"] = "----";
                    //        }

                    //        if (jcxm.Contains("、可操作时间、"))
                    //        {
                    //            if (GetSafeDouble(sItem["KCZSJ"]) >= 1.5 && GetSafeDouble(sItem["KCZSJ"]) <= 4)
                    //            {
                    //                MItem[0]["HG_KCZSJ"] = "合格";
                    //                mFlag_Hg = true;
                    //            }
                    //            else
                    //            {
                    //                MItem[0]["HG_KCZSJ"] = "不合格";
                    //                mbhggs++;
                    //                mFlag_Bhg = true;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            sItem["KCZSJ"] = "----";
                    //            MItem[0]["G_KCZSJ"] = "----";
                    //            MItem[0]["HG_KCZSJ"] = "----";
                    //        }
                    //    }
                    //}
                    #endregion

                    if (sItem["SJDJ"].Contains("GB/T 29906"))
                    {
                        #region 《模塑聚苯板薄抹灰外墙外保温系统材料》GB/T 29906-2013

                        #region 拉伸粘结原强度
                        if (jcxm.Contains("、拉伸粘结原强度、"))
                        {
                            jcxmCur = "拉伸粘结强度与水泥砂浆";
                            #region 与水泥砂浆
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ11"] = (GetSafeDouble(sItem["CD11"]) * GetSafeDouble(sItem["KD11"])).ToString();
                                sItem["MJ12"] = (GetSafeDouble(sItem["CD12"]) * GetSafeDouble(sItem["KD12"])).ToString();
                                sItem["MJ13"] = (GetSafeDouble(sItem["CD13"]) * GetSafeDouble(sItem["KD13"])).ToString();
                                sItem["MJ14"] = (GetSafeDouble(sItem["CD14"]) * GetSafeDouble(sItem["KD14"])).ToString();
                                sItem["MJ15"] = (GetSafeDouble(sItem["CD15"]) * GetSafeDouble(sItem["KD15"])).ToString();
                                sItem["MJ16"] = (GetSafeDouble(sItem["CD16"]) * GetSafeDouble(sItem["KD16"])).ToString();
                                sItem["MJ17"] = (GetSafeDouble(sItem["CD17"]) * GetSafeDouble(sItem["KD17"])).ToString();
                                sItem["MJ18"] = (GetSafeDouble(sItem["CD18"]) * GetSafeDouble(sItem["KD18"])).ToString();
                                sItem["MJ19"] = (GetSafeDouble(sItem["CD19"]) * GetSafeDouble(sItem["KD19"])).ToString();
                                sItem["MJ110"] = (GetSafeDouble(sItem["CD110"]) * GetSafeDouble(sItem["KD110"])).ToString();
                                sItem["MJ111"] = (GetSafeDouble(sItem["CD111"]) * GetSafeDouble(sItem["KD111"])).ToString();
                                sItem["MJ112"] = (GetSafeDouble(sItem["CD112"]) * GetSafeDouble(sItem["KD112"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ11"]) || 0 != GetSafeDouble(sItem["MJ12"]) || 0 != GetSafeDouble(sItem["MJ13"])
                                    || 0 != GetSafeDouble(sItem["MJ14"]) || 0 != GetSafeDouble(sItem["MJ15"]) || 0 != GetSafeDouble(sItem["MJ16"]) || 0 != GetSafeDouble(sItem["MJ17"]) || 0 != GetSafeDouble(sItem["MJ18"]) || 0 != GetSafeDouble(sItem["MJ19"]) || 0 != GetSafeDouble(sItem["MJ110"]) || 0 != GetSafeDouble(sItem["MJ111"]) || 0 != GetSafeDouble(sItem["MJ112"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD11"]) / GetSafeDouble(sItem["MJ11"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD12"]) / GetSafeDouble(sItem["MJ12"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD13"]) / GetSafeDouble(sItem["MJ13"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD14"]) / GetSafeDouble(sItem["MJ14"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD15"]) / GetSafeDouble(sItem["MJ15"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD16"]) / GetSafeDouble(sItem["MJ16"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD17"]) / GetSafeDouble(sItem["MJ17"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD18"]) / GetSafeDouble(sItem["MJ18"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD19"]) / GetSafeDouble(sItem["MJ19"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD110"]) / GetSafeDouble(sItem["MJ110"]), 3);
                                    mKyqd11 = Round(GetSafeDouble(sItem["YQD111"]) / GetSafeDouble(sItem["MJ111"]), 3);
                                    mKyqd12 = Round(GetSafeDouble(sItem["YQD112"]) / GetSafeDouble(sItem["MJ112"]), 3);

                                    sItem["NJQD11"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD12"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD13"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD14"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD15"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD16"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD17"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD18"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD19"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD110"] = Round(mKyqd10, 3).ToString();
                                    sItem["NJQD111"] = Round(mKyqd11, 3).ToString();
                                    sItem["NJQD112"] = Round(mKyqd12, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD11"]) || 0 != GetSafeDouble(sItem["NJQD12"]) || 0 != GetSafeDouble(sItem["NJQD13"])
                                    || 0 != GetSafeDouble(sItem["NJQD14"]) || 0 != GetSafeDouble(sItem["NJQD15"]) || 0 != GetSafeDouble(sItem["NJQD16"]) || 0 != GetSafeDouble(sItem["NJQD17"]) || 0 != GetSafeDouble(sItem["NJQD18"]) || 0 != GetSafeDouble(sItem["NJQD19"]) || 0 != GetSafeDouble(sItem["NJQD110"]) || 0 != GetSafeDouble(sItem["NJQD111"]) || 0 != GetSafeDouble(sItem["NJQD112"]))
                                {
                                    mlongStr = sItem["NJQD11"] + "," + sItem["NJQD12"] + "," + sItem["NJQD13"] + "," + sItem["NJQD14"] + "," + sItem["NJQD15"] + "," + sItem["NJQD16"] + "," + sItem["NJQD17"] + "," + sItem["NJQD18"] + "," + sItem["NJQD19"] + "," + sItem["NJQD110"] + "," + sItem["NJQD111"] + "," + sItem["NJQD112"];
                                    mtmpArray = mlongStr.Split(',');
                                    List<double> mkyqdArray = new List<double>();
                                    for (int i = 0; i < 12; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD1"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5] + mkyqdArray[6] + mkyqdArray[7] + mkyqdArray[8] + mkyqdArray[9] + mkyqdArray[10]) / 10, 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ11"] = (GetSafeDouble(sItem["CD11"]) * GetSafeDouble(sItem["KD11"])).ToString();
                                sItem["MJ12"] = (GetSafeDouble(sItem["CD12"]) * GetSafeDouble(sItem["KD12"])).ToString();
                                sItem["MJ13"] = (GetSafeDouble(sItem["CD13"]) * GetSafeDouble(sItem["KD13"])).ToString();
                                sItem["MJ14"] = (GetSafeDouble(sItem["CD14"]) * GetSafeDouble(sItem["KD14"])).ToString();
                                sItem["MJ15"] = (GetSafeDouble(sItem["CD15"]) * GetSafeDouble(sItem["KD15"])).ToString();
                                sItem["MJ16"] = (GetSafeDouble(sItem["CD16"]) * GetSafeDouble(sItem["KD16"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ11"]) || 0 != GetSafeDouble(sItem["MJ12"]) || 0 != GetSafeDouble(sItem["MJ13"])
                                    || 0 != GetSafeDouble(sItem["MJ14"]) || 0 != GetSafeDouble(sItem["MJ15"]) || 0 != GetSafeDouble(sItem["MJ16"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD11"]) / GetSafeDouble(sItem["MJ11"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD12"]) / GetSafeDouble(sItem["MJ12"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD13"]) / GetSafeDouble(sItem["MJ13"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD14"]) / GetSafeDouble(sItem["MJ14"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD15"]) / GetSafeDouble(sItem["MJ15"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD16"]) / GetSafeDouble(sItem["MJ16"]), 3);

                                    sItem["NJQD11"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD12"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD13"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD14"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD15"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD16"] = Round(mKyqd6, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD11"]) || 0 != GetSafeDouble(sItem["NJQD12"]) || 0 != GetSafeDouble(sItem["NJQD13"])
                                    || 0 != GetSafeDouble(sItem["NJQD14"]) || 0 != GetSafeDouble(sItem["NJQD15"]) || 0 != GetSafeDouble(sItem["NJQD16"]))
                                {
                                    mlongStr = sItem["NJQD11"] + "," + sItem["NJQD12"] + "," + sItem["NJQD13"] + "," + sItem["NJQD14"] + "," + sItem["NJQD15"] + "," + sItem["NJQD16"];
                                    mtmpArray = mlongStr.Split(',');
                                    List<double> mkyqdArray = new List<double>();
                                    for (int i = 0; i < 6; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD1"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                                }
                                #endregion
                            }
                            if (GetSafeDouble(sItem["YQD1"]) >= G_LSNJQD1)
                            {
                                MItem[0]["HG_LSNJQD1"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "(原强度)、";
                                MItem[0]["HG_LSNJQD1"] = "不合格";
                                mAllHg = false;
                                mbhggs++;
                                mFlag_Bhg = true;
                            }
                            #endregion

                            #region 与模塑板
                            jcxmCur = "拉伸粘结强度与模塑板";
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ31"] = (GetSafeDouble(sItem["CD31"]) * GetSafeDouble(sItem["KD31"])).ToString();
                                sItem["MJ32"] = (GetSafeDouble(sItem["CD32"]) * GetSafeDouble(sItem["KD32"])).ToString();
                                sItem["MJ33"] = (GetSafeDouble(sItem["CD33"]) * GetSafeDouble(sItem["KD33"])).ToString();
                                sItem["MJ34"] = (GetSafeDouble(sItem["CD34"]) * GetSafeDouble(sItem["KD34"])).ToString();
                                sItem["MJ35"] = (GetSafeDouble(sItem["CD35"]) * GetSafeDouble(sItem["KD35"])).ToString();
                                sItem["MJ36"] = (GetSafeDouble(sItem["CD36"]) * GetSafeDouble(sItem["KD36"])).ToString();
                                sItem["MJ37"] = (GetSafeDouble(sItem["CD37"]) * GetSafeDouble(sItem["KD37"])).ToString();
                                sItem["MJ38"] = (GetSafeDouble(sItem["CD38"]) * GetSafeDouble(sItem["KD38"])).ToString();
                                sItem["MJ39"] = (GetSafeDouble(sItem["CD39"]) * GetSafeDouble(sItem["KD39"])).ToString();
                                sItem["MJ310"] = (GetSafeDouble(sItem["CD310"]) * GetSafeDouble(sItem["KD310"])).ToString();
                                sItem["MJ311"] = (GetSafeDouble(sItem["CD311"]) * GetSafeDouble(sItem["KD311"])).ToString();
                                sItem["MJ312"] = (GetSafeDouble(sItem["CD312"]) * GetSafeDouble(sItem["KD312"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ31"]) || 0 != GetSafeDouble(sItem["MJ32"]) || 0 != GetSafeDouble(sItem["MJ33"])
                                    || 0 != GetSafeDouble(sItem["MJ34"]) || 0 != GetSafeDouble(sItem["MJ35"]) || 0 != GetSafeDouble(sItem["MJ36"]) || 0 != GetSafeDouble(sItem["MJ37"]) || 0 != GetSafeDouble(sItem["MJ38"]) || 0 != GetSafeDouble(sItem["MJ39"]) || 0 != GetSafeDouble(sItem["MJ310"]) || 0 != GetSafeDouble(sItem["MJ311"]) || 0 != GetSafeDouble(sItem["MJ312"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD21"]) / GetSafeDouble(sItem["MJ31"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD22"]) / GetSafeDouble(sItem["MJ32"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD23"]) / GetSafeDouble(sItem["MJ33"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD24"]) / GetSafeDouble(sItem["MJ34"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD25"]) / GetSafeDouble(sItem["MJ35"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD26"]) / GetSafeDouble(sItem["MJ36"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD27"]) / GetSafeDouble(sItem["MJ37"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD28"]) / GetSafeDouble(sItem["MJ38"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD29"]) / GetSafeDouble(sItem["MJ39"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD210"]) / GetSafeDouble(sItem["MJ310"]), 3);
                                    mKyqd11 = Round(GetSafeDouble(sItem["YQD211"]) / GetSafeDouble(sItem["MJ311"]), 3);
                                    mKyqd12 = Round(GetSafeDouble(sItem["YQD212"]) / GetSafeDouble(sItem["MJ312"]), 3);

                                    sItem["NJQD31"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD32"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD33"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD34"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD35"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD36"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD37"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD38"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD39"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD310"] = Round(mKyqd10, 3).ToString();
                                    sItem["NJQD311"] = Round(mKyqd11, 3).ToString();
                                    sItem["NJQD312"] = Round(mKyqd12, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD31"]) || 0 != GetSafeDouble(sItem["NJQD32"]) || 0 != GetSafeDouble(sItem["NJQD33"])
                                    || 0 != GetSafeDouble(sItem["NJQD34"]) || 0 != GetSafeDouble(sItem["NJQD35"]) || 0 != GetSafeDouble(sItem["NJQD36"]) || 0 != GetSafeDouble(sItem["NJQD37"]) || 0 != GetSafeDouble(sItem["NJQD38"]) || 0 != GetSafeDouble(sItem["NJQD39"]) || 0 != GetSafeDouble(sItem["NJQD310"]) || 0 != GetSafeDouble(sItem["NJQD311"]) || 0 != GetSafeDouble(sItem["NJQD312"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD31"] + "," + sItem["NJQD32"] + "," + sItem["NJQD33"] + "," + sItem["NJQD34"] + "," + sItem["NJQD35"] + "," + sItem["NJQD36"] + "," + sItem["NJQD37"] + "," + sItem["NJQD38"] + "," + sItem["NJQD39"] + "," + sItem["NJQD310"] + "," + sItem["NJQD311"] + "," + sItem["NJQD312"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 12; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD2"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5] + mkyqdArray[6] + mkyqdArray[7] + mkyqdArray[8] + mkyqdArray[9] + mkyqdArray[10]) / 10, 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ31"] = (GetSafeDouble(sItem["CD31"]) * GetSafeDouble(sItem["KD31"])).ToString();
                                sItem["MJ32"] = (GetSafeDouble(sItem["CD32"]) * GetSafeDouble(sItem["KD32"])).ToString();
                                sItem["MJ33"] = (GetSafeDouble(sItem["CD33"]) * GetSafeDouble(sItem["KD33"])).ToString();
                                sItem["MJ34"] = (GetSafeDouble(sItem["CD34"]) * GetSafeDouble(sItem["KD34"])).ToString();
                                sItem["MJ35"] = (GetSafeDouble(sItem["CD35"]) * GetSafeDouble(sItem["KD35"])).ToString();
                                sItem["MJ36"] = (GetSafeDouble(sItem["CD36"]) * GetSafeDouble(sItem["KD36"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ31"]) || 0 != GetSafeDouble(sItem["MJ32"]) || 0 != GetSafeDouble(sItem["MJ33"])
                                    || 0 != GetSafeDouble(sItem["MJ34"]) || 0 != GetSafeDouble(sItem["MJ35"]) || 0 != GetSafeDouble(sItem["MJ36"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD21"]) / GetSafeDouble(sItem["MJ31"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD22"]) / GetSafeDouble(sItem["MJ32"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD23"]) / GetSafeDouble(sItem["MJ33"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD24"]) / GetSafeDouble(sItem["MJ34"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD25"]) / GetSafeDouble(sItem["MJ35"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD26"]) / GetSafeDouble(sItem["MJ36"]), 3);

                                    sItem["NJQD31"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD32"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD33"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD34"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD35"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD36"] = Round(mKyqd6, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD31"]) || 0 != GetSafeDouble(sItem["NJQD32"]) || 0 != GetSafeDouble(sItem["NJQD33"])
                                    || 0 != GetSafeDouble(sItem["NJQD34"]) || 0 != GetSafeDouble(sItem["NJQD35"]) || 0 != GetSafeDouble(sItem["NJQD36"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD31"] + "," + sItem["NJQD32"] + "," + sItem["NJQD33"] + "," + sItem["NJQD34"] + "," + sItem["NJQD35"] + "," + sItem["NJQD36"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 6; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD2"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                                }
                                #endregion
                            }
                            if (!string.IsNullOrEmpty(sItem["PHJMPD1"]))
                            {
                                if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3 && sItem["PHJMPD1"].Trim() == "合格")
                                {
                                    MItem[0]["HG_LSNJQD3"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "(原强度)、";
                                    MItem[0]["HG_LSNJQD3"] = "不合格";
                                    mAllHg = false;
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3)
                                {
                                    MItem[0]["HG_LSNJQD3"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "(原强度)、";
                                    MItem[0]["HG_LSNJQD3"] = "不合格";
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }

                            MItem[0]["G_LSNJQD3"] = MItem[0]["G_LSNJQD3"] + "，破坏发生在模塑板中";
                            #endregion
                        }
                        else
                        {
                            sItem["YQD1"] = "----";
                            MItem[0]["HG_LSNJQD1"] = "----";
                            MItem[0]["G_LSNJQD1"] = "----";
                            sItem["YQD2"] = "----";
                            MItem[0]["HG_LSNJQD3"] = "----";
                            MItem[0]["G_LSNJQD3"] = "----";
                            //MItem[0]["G_PHJM3"] = "----";
                        }
                        #endregion

                        #region 拉伸粘结耐水强度(2h)
                        if (jcxm.Contains("、拉伸粘结耐水强度(2h)、"))
                        {
                            jcxmCur = "拉伸粘结强度与水泥砂浆耐水强度(浸水48h,干燥2h)";
                            #region 与水泥砂浆
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ21"] = (GetSafeDouble(sItem["CD21"]) * GetSafeDouble(sItem["KD21"])).ToString();
                                sItem["MJ22"] = (GetSafeDouble(sItem["CD22"]) * GetSafeDouble(sItem["KD22"])).ToString();
                                sItem["MJ23"] = (GetSafeDouble(sItem["CD23"]) * GetSafeDouble(sItem["KD23"])).ToString();
                                sItem["MJ24"] = (GetSafeDouble(sItem["CD24"]) * GetSafeDouble(sItem["KD24"])).ToString();
                                sItem["MJ25"] = (GetSafeDouble(sItem["CD25"]) * GetSafeDouble(sItem["KD25"])).ToString();
                                sItem["MJ26"] = (GetSafeDouble(sItem["CD26"]) * GetSafeDouble(sItem["KD26"])).ToString();
                                sItem["MJ27"] = (GetSafeDouble(sItem["CD27"]) * GetSafeDouble(sItem["KD27"])).ToString();
                                sItem["MJ28"] = (GetSafeDouble(sItem["CD28"]) * GetSafeDouble(sItem["KD28"])).ToString();
                                sItem["MJ29"] = (GetSafeDouble(sItem["CD29"]) * GetSafeDouble(sItem["KD29"])).ToString();
                                sItem["MJ210"] = (GetSafeDouble(sItem["CD210"]) * GetSafeDouble(sItem["KD210"])).ToString();
                                sItem["MJ211"] = (GetSafeDouble(sItem["CD211"]) * GetSafeDouble(sItem["KD211"])).ToString();
                                sItem["MJ212"] = (GetSafeDouble(sItem["CD212"]) * GetSafeDouble(sItem["KD212"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ21"]) || 0 != GetSafeDouble(sItem["MJ22"]) || 0 != GetSafeDouble(sItem["MJ23"])
                                    || 0 != GetSafeDouble(sItem["MJ24"]) || 0 != GetSafeDouble(sItem["MJ25"]) || 0 != GetSafeDouble(sItem["MJ26"]) || 0 != GetSafeDouble(sItem["MJ27"]) || 0 != GetSafeDouble(sItem["MJ28"]) || 0 != GetSafeDouble(sItem["MJ29"]) || 0 != GetSafeDouble(sItem["MJ210"]) || 0 != GetSafeDouble(sItem["MJ211"]) || 0 != GetSafeDouble(sItem["MJ212"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["JSQD11"]) / GetSafeDouble(sItem["MJ21"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["JSQD12"]) / GetSafeDouble(sItem["MJ22"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["JSQD13"]) / GetSafeDouble(sItem["MJ23"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["JSQD14"]) / GetSafeDouble(sItem["MJ24"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["JSQD15"]) / GetSafeDouble(sItem["MJ25"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["JSQD16"]) / GetSafeDouble(sItem["MJ26"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["JSQD17"]) / GetSafeDouble(sItem["MJ27"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["JSQD18"]) / GetSafeDouble(sItem["MJ28"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["JSQD19"]) / GetSafeDouble(sItem["MJ29"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["JSQD110"]) / GetSafeDouble(sItem["MJ210"]), 3);
                                    mKyqd11 = Round(GetSafeDouble(sItem["JSQD111"]) / GetSafeDouble(sItem["MJ211"]), 3);
                                    mKyqd12 = Round(GetSafeDouble(sItem["JSQD112"]) / GetSafeDouble(sItem["MJ212"]), 3);

                                    sItem["NJQD21"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD22"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD23"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD24"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD25"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD26"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD27"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD28"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD29"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD210"] = Round(mKyqd10, 3).ToString();
                                    sItem["NJQD211"] = Round(mKyqd11, 3).ToString();
                                    sItem["NJQD212"] = Round(mKyqd12, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD21"]) || 0 != GetSafeDouble(sItem["NJQD23"]) || 0 != GetSafeDouble(sItem["NJQD22"])
                                    || 0 != GetSafeDouble(sItem["NJQD24"]) || 0 != GetSafeDouble(sItem["NJQD25"]) || 0 != GetSafeDouble(sItem["NJQD26"]) || 0 != GetSafeDouble(sItem["NJQD27"]) || 0 != GetSafeDouble(sItem["NJQD28"]) || 0 != GetSafeDouble(sItem["NJQD29"]) || 0 != GetSafeDouble(sItem["NJQD210"]) || 0 != GetSafeDouble(sItem["NJQD211"]) || 0 != GetSafeDouble(sItem["NJQD212"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD21"] + "," + sItem["NJQD22"] + "," + sItem["NJQD23"] + "," + sItem["NJQD24"] + "," + sItem["NJQD25"] + "," + sItem["NJQD26"] + "," + sItem["NJQD27"] + "," + sItem["NJQD28"] + "," + sItem["NJQD29"] + "," + sItem["NJQD210"] + "," + sItem["NJQD211"] + "," + sItem["NJQD212"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 12; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD1"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5] + mkyqdArray[6] + mkyqdArray[7] + mkyqdArray[8] + mkyqdArray[9] + mkyqdArray[10]) / 10, 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ21"] = (GetSafeDouble(sItem["CD21"]) * GetSafeDouble(sItem["KD21"])).ToString();
                                sItem["MJ22"] = (GetSafeDouble(sItem["CD22"]) * GetSafeDouble(sItem["KD22"])).ToString();
                                sItem["MJ23"] = (GetSafeDouble(sItem["CD23"]) * GetSafeDouble(sItem["KD23"])).ToString();
                                sItem["MJ24"] = (GetSafeDouble(sItem["CD24"]) * GetSafeDouble(sItem["KD24"])).ToString();
                                sItem["MJ25"] = (GetSafeDouble(sItem["CD25"]) * GetSafeDouble(sItem["KD25"])).ToString();
                                sItem["MJ26"] = (GetSafeDouble(sItem["CD26"]) * GetSafeDouble(sItem["KD26"])).ToString();
                                if (0 != GetSafeDouble(sItem["MJ21"]) || 0 != GetSafeDouble(sItem["MJ22"]) || 0 != GetSafeDouble(sItem["MJ23"])
                                    || 0 != GetSafeDouble(sItem["MJ24"]) || 0 != GetSafeDouble(sItem["MJ25"]) || 0 != GetSafeDouble(sItem["MJ26"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["JSQD11"]) / GetSafeDouble(sItem["MJ21"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["JSQD12"]) / GetSafeDouble(sItem["MJ22"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["JSQD13"]) / GetSafeDouble(sItem["MJ23"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["JSQD14"]) / GetSafeDouble(sItem["MJ24"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["JSQD15"]) / GetSafeDouble(sItem["MJ25"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["JSQD16"]) / GetSafeDouble(sItem["MJ26"]), 3);

                                    sItem["NJQD21"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD22"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD23"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD24"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD25"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD26"] = Round(mKyqd6, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD21"]) || 0 != GetSafeDouble(sItem["NJQD23"]) || 0 != GetSafeDouble(sItem["NJQD22"])
                                    || 0 != GetSafeDouble(sItem["NJQD24"]) || 0 != GetSafeDouble(sItem["NJQD25"]) || 0 != GetSafeDouble(sItem["NJQD26"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD21"] + "," + sItem["NJQD22"] + "," + sItem["NJQD23"] + "," + sItem["NJQD24"] + "," + sItem["NJQD25"] + "," + sItem["NJQD26"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 6; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD1"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                                }
                                #endregion
                            }
                            if (GetSafeDouble(sItem["JSQD1"]) >= G_LSNJQD2)
                            {
                                MItem[0]["HG_LSNJQD2"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_LSNJQD2"] = "不合格";
                                mAllHg = false;
                                mbhggs++;
                                mFlag_Bhg = true;
                            }

                            #endregion

                            #region 与模塑板
                            jcxmCur = "拉伸粘结强度与模塑板耐水强度(浸水48h,干燥2h)";
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ41"] = (GetSafeDouble(sItem["CD41"]) * GetSafeDouble(sItem["KD41"])).ToString();
                                sItem["MJ42"] = (GetSafeDouble(sItem["CD42"]) * GetSafeDouble(sItem["KD42"])).ToString();
                                sItem["MJ43"] = (GetSafeDouble(sItem["CD43"]) * GetSafeDouble(sItem["KD43"])).ToString();
                                sItem["MJ44"] = (GetSafeDouble(sItem["CD44"]) * GetSafeDouble(sItem["KD44"])).ToString();
                                sItem["MJ45"] = (GetSafeDouble(sItem["CD45"]) * GetSafeDouble(sItem["KD45"])).ToString();
                                sItem["MJ46"] = (GetSafeDouble(sItem["CD46"]) * GetSafeDouble(sItem["KD46"])).ToString();
                                sItem["MJ47"] = (GetSafeDouble(sItem["CD47"]) * GetSafeDouble(sItem["KD47"])).ToString();
                                sItem["MJ48"] = (GetSafeDouble(sItem["CD48"]) * GetSafeDouble(sItem["KD48"])).ToString();
                                sItem["MJ49"] = (GetSafeDouble(sItem["CD49"]) * GetSafeDouble(sItem["KD49"])).ToString();
                                sItem["MJ410"] = (GetSafeDouble(sItem["CD410"]) * GetSafeDouble(sItem["KD410"])).ToString();
                                sItem["MJ411"] = (GetSafeDouble(sItem["CD411"]) * GetSafeDouble(sItem["KD411"])).ToString();
                                sItem["MJ412"] = (GetSafeDouble(sItem["CD412"]) * GetSafeDouble(sItem["KD412"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ41"]) || 0 != GetSafeDouble(sItem["MJ42"]) || 0 != GetSafeDouble(sItem["MJ43"])
                                    || 0 != GetSafeDouble(sItem["MJ44"]) || 0 != GetSafeDouble(sItem["MJ45"]) || 0 != GetSafeDouble(sItem["MJ46"]) || 0 != GetSafeDouble(sItem["MJ47"]) || 0 != GetSafeDouble(sItem["MJ48"]) || 0 != GetSafeDouble(sItem["MJ49"]) || 0 != GetSafeDouble(sItem["MJ410"]) || 0 != GetSafeDouble(sItem["MJ411"]) || 0 != GetSafeDouble(sItem["MJ412"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["JSQD21"]) / GetSafeDouble(sItem["MJ41"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["JSQD22"]) / GetSafeDouble(sItem["MJ42"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["JSQD23"]) / GetSafeDouble(sItem["MJ43"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["JSQD24"]) / GetSafeDouble(sItem["MJ44"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["JSQD25"]) / GetSafeDouble(sItem["MJ45"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["JSQD26"]) / GetSafeDouble(sItem["MJ46"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["JSQD27"]) / GetSafeDouble(sItem["MJ47"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["JSQD28"]) / GetSafeDouble(sItem["MJ48"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["JSQD29"]) / GetSafeDouble(sItem["MJ49"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["JSQD210"]) / GetSafeDouble(sItem["MJ410"]), 3);
                                    mKyqd11 = Round(GetSafeDouble(sItem["JSQD211"]) / GetSafeDouble(sItem["MJ411"]), 3);
                                    mKyqd12 = Round(GetSafeDouble(sItem["JSQD212"]) / GetSafeDouble(sItem["MJ412"]), 3);

                                    sItem["NJQD41"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD42"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD43"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD44"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD45"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD46"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD47"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD48"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD49"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD410"] = Round(mKyqd10, 3).ToString();
                                    sItem["NJQD411"] = Round(mKyqd11, 3).ToString();
                                    sItem["NJQD412"] = Round(mKyqd12, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD41"]) || 0 != GetSafeDouble(sItem["NJQD42"]) || 0 != GetSafeDouble(sItem["NJQD43"])
                                    || 0 != GetSafeDouble(sItem["NJQD44"]) || 0 != GetSafeDouble(sItem["NJQD45"]) || 0 != GetSafeDouble(sItem["NJQD46"]) || 0 != GetSafeDouble(sItem["NJQD47"]) || 0 != GetSafeDouble(sItem["NJQD48"]) || 0 != GetSafeDouble(sItem["NJQD49"]) || 0 != GetSafeDouble(sItem["NJQD410"]) || 0 != GetSafeDouble(sItem["NJQD411"]) || 0 != GetSafeDouble(sItem["NJQD412"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD41"] + "," + sItem["NJQD42"] + "," + sItem["NJQD43"] + "," + sItem["NJQD44"] + "," + sItem["NJQD45"] + "," + sItem["NJQD46"] + "," + sItem["NJQD47"] + "," + sItem["NJQD48"] + "," + sItem["NJQD49"] + "," + sItem["NJQD410"] + "," + sItem["NJQD411"] + "," + sItem["NJQD412"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 12; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD2"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5] + mkyqdArray[6] + mkyqdArray[7] + mkyqdArray[8] + mkyqdArray[9] + mkyqdArray[10]) / 10, 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ41"] = (GetSafeDouble(sItem["CD41"]) * GetSafeDouble(sItem["KD41"])).ToString();
                                sItem["MJ42"] = (GetSafeDouble(sItem["CD42"]) * GetSafeDouble(sItem["KD42"])).ToString();
                                sItem["MJ43"] = (GetSafeDouble(sItem["CD43"]) * GetSafeDouble(sItem["KD43"])).ToString();
                                sItem["MJ44"] = (GetSafeDouble(sItem["CD44"]) * GetSafeDouble(sItem["KD44"])).ToString();
                                sItem["MJ45"] = (GetSafeDouble(sItem["CD45"]) * GetSafeDouble(sItem["KD45"])).ToString();
                                sItem["MJ46"] = (GetSafeDouble(sItem["CD46"]) * GetSafeDouble(sItem["KD46"])).ToString();
                                if (0 != GetSafeDouble(sItem["MJ41"]) || 0 != GetSafeDouble(sItem["MJ42"]) || 0 != GetSafeDouble(sItem["MJ43"])
                                    || 0 != GetSafeDouble(sItem["MJ44"]) || 0 != GetSafeDouble(sItem["MJ45"]) || 0 != GetSafeDouble(sItem["MJ46"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["JSQD21"]) / GetSafeDouble(sItem["MJ41"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["JSQD22"]) / GetSafeDouble(sItem["MJ42"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["JSQD23"]) / GetSafeDouble(sItem["MJ43"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["JSQD24"]) / GetSafeDouble(sItem["MJ44"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["JSQD25"]) / GetSafeDouble(sItem["MJ45"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["JSQD26"]) / GetSafeDouble(sItem["MJ46"]), 3);

                                    sItem["NJQD41"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD42"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD43"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD44"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD45"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD46"] = Round(mKyqd6, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD41"]) || 0 != GetSafeDouble(sItem["NJQD42"]) || 0 != GetSafeDouble(sItem["NJQD43"])
                                    || 0 != GetSafeDouble(sItem["NJQD44"]) || 0 != GetSafeDouble(sItem["NJQD45"]) || 0 != GetSafeDouble(sItem["NJQD46"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD41"] + "," + sItem["NJQD42"] + "," + sItem["NJQD43"] + "," + sItem["NJQD44"] + "," + sItem["NJQD45"] + "," + sItem["NJQD46"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 6; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD2"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                                }
                                #endregion
                            }
                            if (!string.IsNullOrEmpty(sItem["PHJMPD2"]))
                            {
                                if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4 && sItem["PHJMPD2"] == "合格")//
                                {
                                    MItem[0]["HG_LSNJQD4"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_LSNJQD4"] = "不合格";
                                    mAllHg = false;
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4)//
                                {
                                    MItem[0]["HG_LSNJQD4"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_LSNJQD4"] = "不合格";
                                    mAllHg = false;
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            MItem[0]["G_LSNJQD4"] = MItem[0]["G_LSNJQD4"] + "，破坏发生在模塑板中";
                            #endregion
                        }
                        else
                        {
                            sItem["JSQD1"] = "----";
                            MItem[0]["HG_LSNJQD2"] = "----";
                            MItem[0]["G_LSNJQD2"] = "----";
                            sItem["JSQD2"] = "----";
                            MItem[0]["HG_LSNJQD4"] = "----";
                            MItem[0]["G_LSNJQD4"] = "----";
                        }
                        #endregion

                        #region 拉伸粘结耐水强度(7d)
                        if (jcxm.Contains("、拉伸粘结耐水强度(7d)、"))
                        {
                            jcxmCur = "拉伸粘结强度与水泥砂浆耐水强度(浸水48h,干燥7d)";
                            #region 与水泥砂浆
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ51"] = (GetSafeDouble(sItem["CD51"]) * GetSafeDouble(sItem["KD51"])).ToString();
                                sItem["MJ52"] = (GetSafeDouble(sItem["CD52"]) * GetSafeDouble(sItem["KD52"])).ToString();
                                sItem["MJ53"] = (GetSafeDouble(sItem["CD53"]) * GetSafeDouble(sItem["KD53"])).ToString();
                                sItem["MJ54"] = (GetSafeDouble(sItem["CD54"]) * GetSafeDouble(sItem["KD54"])).ToString();
                                sItem["MJ55"] = (GetSafeDouble(sItem["CD55"]) * GetSafeDouble(sItem["KD55"])).ToString();
                                sItem["MJ56"] = (GetSafeDouble(sItem["CD56"]) * GetSafeDouble(sItem["KD56"])).ToString();
                                sItem["MJ57"] = (GetSafeDouble(sItem["CD57"]) * GetSafeDouble(sItem["KD57"])).ToString();
                                sItem["MJ58"] = (GetSafeDouble(sItem["CD58"]) * GetSafeDouble(sItem["KD58"])).ToString();
                                sItem["MJ59"] = (GetSafeDouble(sItem["CD59"]) * GetSafeDouble(sItem["KD59"])).ToString();
                                sItem["MJ510"] = (GetSafeDouble(sItem["CD510"]) * GetSafeDouble(sItem["KD510"])).ToString();
                                sItem["MJ511"] = (GetSafeDouble(sItem["CD511"]) * GetSafeDouble(sItem["KD511"])).ToString();
                                sItem["MJ512"] = (GetSafeDouble(sItem["CD512"]) * GetSafeDouble(sItem["KD512"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ51"]) || 0 != GetSafeDouble(sItem["MJ52"]) || 0 != GetSafeDouble(sItem["MJ53"])
                                    || 0 != GetSafeDouble(sItem["MJ54"]) || 0 != GetSafeDouble(sItem["MJ55"]) || 0 != GetSafeDouble(sItem["MJ56"]) || 0 != GetSafeDouble(sItem["MJ57"]) || 0 != GetSafeDouble(sItem["MJ58"]) || 0 != GetSafeDouble(sItem["MJ59"]) || 0 != GetSafeDouble(sItem["MJ510"]) || 0 != GetSafeDouble(sItem["MJ511"]) || 0 != GetSafeDouble(sItem["MJ512"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD51"]) / GetSafeDouble(sItem["MJ51"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD52"]) / GetSafeDouble(sItem["MJ52"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD53"]) / GetSafeDouble(sItem["MJ53"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD54"]) / GetSafeDouble(sItem["MJ54"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD55"]) / GetSafeDouble(sItem["MJ55"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD56"]) / GetSafeDouble(sItem["MJ56"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD57"]) / GetSafeDouble(sItem["MJ57"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD58"]) / GetSafeDouble(sItem["MJ58"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD59"]) / GetSafeDouble(sItem["MJ59"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD510"]) / GetSafeDouble(sItem["MJ510"]), 3);
                                    mKyqd11 = Round(GetSafeDouble(sItem["YQD511"]) / GetSafeDouble(sItem["MJ511"]), 3);
                                    mKyqd12 = Round(GetSafeDouble(sItem["YQD512"]) / GetSafeDouble(sItem["MJ512"]), 3);

                                    sItem["NJQD51"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD52"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD53"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD54"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD55"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD56"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD57"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD58"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD59"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD510"] = Round(mKyqd10, 3).ToString();
                                    sItem["NJQD511"] = Round(mKyqd11, 3).ToString();
                                    sItem["NJQD512"] = Round(mKyqd12, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD51"]) || 0 != GetSafeDouble(sItem["NJQD53"]) || 0 != GetSafeDouble(sItem["NJQD52"])
                                    || 0 != GetSafeDouble(sItem["NJQD54"]) || 0 != GetSafeDouble(sItem["NJQD55"]) || 0 != GetSafeDouble(sItem["NJQD56"]) || 0 != GetSafeDouble(sItem["NJQD57"]) || 0 != GetSafeDouble(sItem["NJQD58"]) || 0 != GetSafeDouble(sItem["NJQD59"]) || 0 != GetSafeDouble(sItem["NJQD510"]) || 0 != GetSafeDouble(sItem["NJQD511"]) || 0 != GetSafeDouble(sItem["NJQD512"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD51"] + "," + sItem["NJQD52"] + "," + sItem["NJQD53"] + "," + sItem["NJQD54"] + "," + sItem["NJQD55"] + "," + sItem["NJQD56"] + "," + sItem["NJQD57"] + "," + sItem["NJQD58"] + "," + sItem["NJQD59"] + "," + sItem["NJQD510"] + "," + sItem["NJQD511"] + "," + sItem["NJQD512"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 12; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD5"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5] + mkyqdArray[6] + mkyqdArray[7] + mkyqdArray[8] + mkyqdArray[9] + mkyqdArray[10]) / 10, 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ51"] = (GetSafeDouble(sItem["CD51"]) * GetSafeDouble(sItem["KD51"])).ToString();
                                sItem["MJ52"] = (GetSafeDouble(sItem["CD52"]) * GetSafeDouble(sItem["KD52"])).ToString();
                                sItem["MJ53"] = (GetSafeDouble(sItem["CD53"]) * GetSafeDouble(sItem["KD53"])).ToString();
                                sItem["MJ54"] = (GetSafeDouble(sItem["CD54"]) * GetSafeDouble(sItem["KD54"])).ToString();
                                sItem["MJ55"] = (GetSafeDouble(sItem["CD55"]) * GetSafeDouble(sItem["KD55"])).ToString();
                                sItem["MJ56"] = (GetSafeDouble(sItem["CD56"]) * GetSafeDouble(sItem["KD56"])).ToString();
                                if (0 != GetSafeDouble(sItem["MJ51"]) || 0 != GetSafeDouble(sItem["MJ52"]) || 0 != GetSafeDouble(sItem["MJ53"])
                                    || 0 != GetSafeDouble(sItem["MJ54"]) || 0 != GetSafeDouble(sItem["MJ55"]) || 0 != GetSafeDouble(sItem["MJ56"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD51"]) / GetSafeDouble(sItem["MJ51"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD52"]) / GetSafeDouble(sItem["MJ52"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD53"]) / GetSafeDouble(sItem["MJ53"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD54"]) / GetSafeDouble(sItem["MJ54"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD55"]) / GetSafeDouble(sItem["MJ55"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD56"]) / GetSafeDouble(sItem["MJ56"]), 3);

                                    sItem["NJQD51"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD52"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD53"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD54"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD55"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD56"] = Round(mKyqd6, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD51"]) || 0 != GetSafeDouble(sItem["NJQD53"]) || 0 != GetSafeDouble(sItem["NJQD52"])
                                    || 0 != GetSafeDouble(sItem["NJQD54"]) || 0 != GetSafeDouble(sItem["NJQD55"]) || 0 != GetSafeDouble(sItem["NJQD56"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD51"] + "," + sItem["NJQD52"] + "," + sItem["NJQD53"] + "," + sItem["NJQD54"] + "," + sItem["NJQD55"] + "," + sItem["NJQD56"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 6; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD5"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                                }
                                #endregion
                            }
                            if (GetSafeDouble(sItem["YQD5"]) >= G_LSNJQD7)
                            {
                                MItem[0]["HG_LSNJQD5"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_LSNJQD5"] = "不合格";
                                mAllHg = false;
                                mbhggs++;
                                mFlag_Bhg = true;
                            }

                            #endregion

                            #region 与模塑板
                            jcxmCur = "拉伸粘结强度与模塑板耐水强度(浸水48h,干燥7d)";
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ71"] = (GetSafeDouble(sItem["CD71"]) * GetSafeDouble(sItem["KD71"])).ToString();
                                sItem["MJ72"] = (GetSafeDouble(sItem["CD72"]) * GetSafeDouble(sItem["KD72"])).ToString();
                                sItem["MJ73"] = (GetSafeDouble(sItem["CD73"]) * GetSafeDouble(sItem["KD73"])).ToString();
                                sItem["MJ74"] = (GetSafeDouble(sItem["CD74"]) * GetSafeDouble(sItem["KD74"])).ToString();
                                sItem["MJ75"] = (GetSafeDouble(sItem["CD75"]) * GetSafeDouble(sItem["KD75"])).ToString();
                                sItem["MJ76"] = (GetSafeDouble(sItem["CD76"]) * GetSafeDouble(sItem["KD76"])).ToString();
                                sItem["MJ77"] = (GetSafeDouble(sItem["CD77"]) * GetSafeDouble(sItem["KD77"])).ToString();
                                sItem["MJ78"] = (GetSafeDouble(sItem["CD78"]) * GetSafeDouble(sItem["KD78"])).ToString();
                                sItem["MJ79"] = (GetSafeDouble(sItem["CD79"]) * GetSafeDouble(sItem["KD79"])).ToString();
                                sItem["MJ710"] = (GetSafeDouble(sItem["CD710"]) * GetSafeDouble(sItem["KD710"])).ToString();
                                sItem["MJ711"] = (GetSafeDouble(sItem["CD711"]) * GetSafeDouble(sItem["KD711"])).ToString();
                                sItem["MJ712"] = (GetSafeDouble(sItem["CD712"]) * GetSafeDouble(sItem["KD712"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ71"]) || 0 != GetSafeDouble(sItem["MJ72"]) || 0 != GetSafeDouble(sItem["MJ73"])
                                    || 0 != GetSafeDouble(sItem["MJ74"]) || 0 != GetSafeDouble(sItem["MJ75"]) || 0 != GetSafeDouble(sItem["MJ76"]) || 0 != GetSafeDouble(sItem["MJ77"]) || 0 != GetSafeDouble(sItem["MJ78"]) || 0 != GetSafeDouble(sItem["MJ79"]) || 0 != GetSafeDouble(sItem["MJ710"]) || 0 != GetSafeDouble(sItem["MJ711"]) || 0 != GetSafeDouble(sItem["MJ712"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD71"]) / GetSafeDouble(sItem["MJ71"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD72"]) / GetSafeDouble(sItem["MJ72"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD73"]) / GetSafeDouble(sItem["MJ73"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD74"]) / GetSafeDouble(sItem["MJ74"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD75"]) / GetSafeDouble(sItem["MJ75"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD76"]) / GetSafeDouble(sItem["MJ76"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD77"]) / GetSafeDouble(sItem["MJ77"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD78"]) / GetSafeDouble(sItem["MJ78"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD79"]) / GetSafeDouble(sItem["MJ79"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD710"]) / GetSafeDouble(sItem["MJ710"]), 3);
                                    mKyqd11 = Round(GetSafeDouble(sItem["YQD711"]) / GetSafeDouble(sItem["MJ711"]), 3);
                                    mKyqd12 = Round(GetSafeDouble(sItem["YQD712"]) / GetSafeDouble(sItem["MJ712"]), 3);

                                    sItem["NJQD71"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD72"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD73"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD74"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD75"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD76"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD77"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD78"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD79"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD710"] = Round(mKyqd10, 3).ToString();
                                    sItem["NJQD711"] = Round(mKyqd11, 3).ToString();
                                    sItem["NJQD712"] = Round(mKyqd12, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD71"]) || 0 != GetSafeDouble(sItem["NJQD72"]) || 0 != GetSafeDouble(sItem["NJQD73"])
                                    || 0 != GetSafeDouble(sItem["NJQD74"]) || 0 != GetSafeDouble(sItem["NJQD75"]) || 0 != GetSafeDouble(sItem["NJQD76"]) || 0 != GetSafeDouble(sItem["NJQD77"]) || 0 != GetSafeDouble(sItem["NJQD78"]) || 0 != GetSafeDouble(sItem["NJQD79"]) || 0 != GetSafeDouble(sItem["NJQD710"]) || 0 != GetSafeDouble(sItem["NJQD711"]) || 0 != GetSafeDouble(sItem["NJQD712"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD71"] + "," + sItem["NJQD72"] + "," + sItem["NJQD73"] + "," + sItem["NJQD74"] + "," + sItem["NJQD75"] + "," + sItem["NJQD76"] + "," + sItem["NJQD77"] + "," + sItem["NJQD78"] + "," + sItem["NJQD79"] + "," + sItem["NJQD710"] + "," + sItem["NJQD711"] + "," + sItem["NJQD712"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 12; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD7"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5] + mkyqdArray[6] + mkyqdArray[7] + mkyqdArray[8] + mkyqdArray[9] + mkyqdArray[10]) / 10, 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ71"] = (GetSafeDouble(sItem["CD71"]) * GetSafeDouble(sItem["KD71"])).ToString();
                                sItem["MJ72"] = (GetSafeDouble(sItem["CD72"]) * GetSafeDouble(sItem["KD72"])).ToString();
                                sItem["MJ73"] = (GetSafeDouble(sItem["CD73"]) * GetSafeDouble(sItem["KD73"])).ToString();
                                sItem["MJ74"] = (GetSafeDouble(sItem["CD74"]) * GetSafeDouble(sItem["KD74"])).ToString();
                                sItem["MJ75"] = (GetSafeDouble(sItem["CD75"]) * GetSafeDouble(sItem["KD75"])).ToString();
                                sItem["MJ76"] = (GetSafeDouble(sItem["CD76"]) * GetSafeDouble(sItem["KD76"])).ToString();
                                if (0 != GetSafeDouble(sItem["MJ71"]) || 0 != GetSafeDouble(sItem["MJ72"]) || 0 != GetSafeDouble(sItem["MJ73"])
                                    || 0 != GetSafeDouble(sItem["MJ74"]) || 0 != GetSafeDouble(sItem["MJ75"]) || 0 != GetSafeDouble(sItem["MJ76"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD71"]) / GetSafeDouble(sItem["MJ71"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD72"]) / GetSafeDouble(sItem["MJ72"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD73"]) / GetSafeDouble(sItem["MJ73"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD74"]) / GetSafeDouble(sItem["MJ74"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD75"]) / GetSafeDouble(sItem["MJ75"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD76"]) / GetSafeDouble(sItem["MJ76"]), 3);

                                    sItem["NJQD71"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD72"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD73"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD74"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD75"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD76"] = Round(mKyqd6, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD71"]) || 0 != GetSafeDouble(sItem["NJQD72"]) || 0 != GetSafeDouble(sItem["NJQD73"])
                                    || 0 != GetSafeDouble(sItem["NJQD74"]) || 0 != GetSafeDouble(sItem["NJQD75"]) || 0 != GetSafeDouble(sItem["NJQD76"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD71"] + "," + sItem["NJQD72"] + "," + sItem["NJQD73"] + "," + sItem["NJQD74"] + "," + sItem["NJQD75"] + "," + sItem["NJQD76"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 6; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD7"] = Round((mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                                }
                                #endregion
                            }
                            if (!string.IsNullOrEmpty(sItem["PHJMPD7"]))
                            {
                                if (GetSafeDouble(sItem["JSQD7"]) >= G_LSNJQD8 && sItem["PHJMPD7"] == "合格")//
                                {
                                    MItem[0]["HG_LSNJQD7"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_LSNJQD7"] = "不合格";
                                    mAllHg = false;
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4)//
                                {
                                    MItem[0]["HG_LSNJQD7"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_LSNJQD7"] = "不合格";
                                    mAllHg = false;
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            MItem[0]["G_LSNJQD8"] = MItem[0]["G_LSNJQD8"] + "，破坏发生在模塑板中";
                            #endregion
                        }
                        else
                        {
                            sItem["YQD5"] = "----";
                            MItem[0]["HG_LSNJQD5"] = "----";
                            MItem[0]["G_LSNJQD7"] = "----";
                            sItem["JSQD7"] = "----";
                            MItem[0]["HG_LSNJQD7"] = "----";
                            MItem[0]["G_LSNJQD4"] = "----";
                        }
                        #endregion

                        #region 可操作时间
                        if (jcxm.Contains("、可操作时间、"))
                        {
                            jcxmCur = "可操作时间";
                            //if (GetSafeDouble(sItem["KCZSJ"]) >= 1.5 && GetSafeDouble(sItem["KCZSJ"]) <= 4)
                            if (GetSafeDouble(sItem["KCZSJ"]) >= GetSafeDouble(MItem[0]["G_KCZSJ"]))
                            {
                                MItem[0]["HG_KCZSJ"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KCZSJ"] = "不合格";
                                mAllHg = false;
                                mbhggs++;
                                mFlag_Bhg = true;
                            }
                            MItem[0]["G_KCZSJ"] = "≥" + MItem[0]["G_KCZSJ"];
                        }
                        else
                        {
                            sItem["KCZSJ"] = "----";
                            MItem[0]["G_KCZSJ"] = "----";
                            MItem[0]["HG_KCZSJ"] = "----";
                        }
                        #endregion


                        #endregion
                    }
                    else if (sItem["SJDJ"].Contains("JC/T 992"))
                    {

                        #region 《墙体保温用膨胀聚苯乙烯板胶粘剂》JC/T 992-2006
                        #region 拉伸粘结原强度
                        if (jcxm.Contains("、拉伸粘结原强度、"))
                        {
                            jcxmCur = "拉伸粘结强度与水泥砂浆(原强度)";
                            #region 与水泥砂浆
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ11"] = (GetSafeDouble(sItem["CD11"]) * GetSafeDouble(sItem["KD11"])).ToString();
                                sItem["MJ12"] = (GetSafeDouble(sItem["CD12"]) * GetSafeDouble(sItem["KD12"])).ToString();
                                sItem["MJ13"] = (GetSafeDouble(sItem["CD13"]) * GetSafeDouble(sItem["KD13"])).ToString();
                                sItem["MJ14"] = (GetSafeDouble(sItem["CD14"]) * GetSafeDouble(sItem["KD14"])).ToString();
                                sItem["MJ15"] = (GetSafeDouble(sItem["CD15"]) * GetSafeDouble(sItem["KD15"])).ToString();
                                sItem["MJ16"] = (GetSafeDouble(sItem["CD16"]) * GetSafeDouble(sItem["KD16"])).ToString();
                                sItem["MJ17"] = (GetSafeDouble(sItem["CD17"]) * GetSafeDouble(sItem["KD17"])).ToString();
                                sItem["MJ18"] = (GetSafeDouble(sItem["CD18"]) * GetSafeDouble(sItem["KD18"])).ToString();
                                sItem["MJ19"] = (GetSafeDouble(sItem["CD19"]) * GetSafeDouble(sItem["KD19"])).ToString();
                                sItem["MJ110"] = (GetSafeDouble(sItem["CD110"]) * GetSafeDouble(sItem["KD110"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ11"]) || 0 != GetSafeDouble(sItem["MJ12"]) || 0 != GetSafeDouble(sItem["MJ13"])
                                    || 0 != GetSafeDouble(sItem["MJ14"]) || 0 != GetSafeDouble(sItem["MJ15"]) || 0 != GetSafeDouble(sItem["MJ16"]) || 0 != GetSafeDouble(sItem["MJ17"]) || 0 != GetSafeDouble(sItem["MJ18"]) || 0 != GetSafeDouble(sItem["MJ19"]) || 0 != GetSafeDouble(sItem["MJ110"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD11"]) / GetSafeDouble(sItem["MJ11"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD12"]) / GetSafeDouble(sItem["MJ12"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD13"]) / GetSafeDouble(sItem["MJ13"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD14"]) / GetSafeDouble(sItem["MJ14"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD15"]) / GetSafeDouble(sItem["MJ15"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD16"]) / GetSafeDouble(sItem["MJ16"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD17"]) / GetSafeDouble(sItem["MJ17"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD18"]) / GetSafeDouble(sItem["MJ18"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD19"]) / GetSafeDouble(sItem["MJ19"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD110"]) / GetSafeDouble(sItem["MJ110"]), 3);

                                    sItem["NJQD11"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD12"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD13"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD14"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD15"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD16"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD17"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD18"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD19"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD110"] = Round(mKyqd10, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD11"]) || 0 != GetSafeDouble(sItem["NJQD12"]) || 0 != GetSafeDouble(sItem["NJQD13"])
                                    || 0 != GetSafeDouble(sItem["NJQD14"]) || 0 != GetSafeDouble(sItem["NJQD15"]) || 0 != GetSafeDouble(sItem["NJQD16"]) || 0 != GetSafeDouble(sItem["NJQD17"]) || 0 != GetSafeDouble(sItem["NJQD18"]) || 0 != GetSafeDouble(sItem["NJQD19"]) || 0 != GetSafeDouble(sItem["NJQD110"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD11"] + "," + sItem["NJQD12"] + "," + sItem["NJQD13"] + "," + sItem["NJQD14"] + "," + sItem["NJQD15"] + "," + sItem["NJQD16"] + "," + sItem["NJQD17"] + "," + sItem["NJQD18"] + "," + sItem["NJQD19"] + "," + sItem["NJQD110"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 10; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD1"] = Round(mkyqdArray.Average(), 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ11"] = (GetSafeDouble(sItem["CD11"]) * GetSafeDouble(sItem["KD11"])).ToString();
                                sItem["MJ12"] = (GetSafeDouble(sItem["CD12"]) * GetSafeDouble(sItem["KD12"])).ToString();
                                sItem["MJ13"] = (GetSafeDouble(sItem["CD13"]) * GetSafeDouble(sItem["KD13"])).ToString();
                                sItem["MJ14"] = (GetSafeDouble(sItem["CD14"]) * GetSafeDouble(sItem["KD14"])).ToString();
                                sItem["MJ15"] = (GetSafeDouble(sItem["CD15"]) * GetSafeDouble(sItem["KD15"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ11"]) || 0 != GetSafeDouble(sItem["MJ12"]) || 0 != GetSafeDouble(sItem["MJ13"])
                                    || 0 != GetSafeDouble(sItem["MJ14"]) || 0 != GetSafeDouble(sItem["MJ15"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD11"]) / GetSafeDouble(sItem["MJ11"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD12"]) / GetSafeDouble(sItem["MJ12"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD13"]) / GetSafeDouble(sItem["MJ13"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD14"]) / GetSafeDouble(sItem["MJ14"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD15"]) / GetSafeDouble(sItem["MJ15"]), 3);

                                    sItem["NJQD11"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD12"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD13"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD14"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD15"] = Round(mKyqd5, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD11"]) || 0 != GetSafeDouble(sItem["NJQD12"]) || 0 != GetSafeDouble(sItem["NJQD13"])
                                    || 0 != GetSafeDouble(sItem["NJQD14"]) || 0 != GetSafeDouble(sItem["NJQD15"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD11"] + "," + sItem["NJQD12"] + "," + sItem["NJQD13"] + "," + sItem["NJQD14"] + "," + sItem["NJQD15"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD1"] = Round(mkyqdArray.Average(), 2).ToString();
                                }
                                #endregion
                            }
                            if (GetSafeDouble(sItem["YQD1"]) >= G_LSNJQD1)
                            {
                                MItem[0]["HG_LSNJQD1"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_LSNJQD1"] = "不合格";
                                mAllHg = false;
                                mbhggs++;
                                mFlag_Bhg = true;
                            }
                            #endregion

                            #region 与聚苯板
                            jcxmCur = "拉伸粘结强度与聚苯板(原强度)";
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ31"] = (GetSafeDouble(sItem["CD31"]) * GetSafeDouble(sItem["KD31"])).ToString();
                                sItem["MJ32"] = (GetSafeDouble(sItem["CD32"]) * GetSafeDouble(sItem["KD32"])).ToString();
                                sItem["MJ33"] = (GetSafeDouble(sItem["CD33"]) * GetSafeDouble(sItem["KD33"])).ToString();
                                sItem["MJ34"] = (GetSafeDouble(sItem["CD34"]) * GetSafeDouble(sItem["KD34"])).ToString();
                                sItem["MJ35"] = (GetSafeDouble(sItem["CD35"]) * GetSafeDouble(sItem["KD35"])).ToString();
                                sItem["MJ36"] = (GetSafeDouble(sItem["CD36"]) * GetSafeDouble(sItem["KD36"])).ToString();
                                sItem["MJ37"] = (GetSafeDouble(sItem["CD37"]) * GetSafeDouble(sItem["KD37"])).ToString();
                                sItem["MJ38"] = (GetSafeDouble(sItem["CD38"]) * GetSafeDouble(sItem["KD38"])).ToString();
                                sItem["MJ39"] = (GetSafeDouble(sItem["CD39"]) * GetSafeDouble(sItem["KD39"])).ToString();
                                sItem["MJ310"] = (GetSafeDouble(sItem["CD310"]) * GetSafeDouble(sItem["KD310"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ31"]) || 0 != GetSafeDouble(sItem["MJ32"]) || 0 != GetSafeDouble(sItem["MJ33"])
                                    || 0 != GetSafeDouble(sItem["MJ34"]) || 0 != GetSafeDouble(sItem["MJ35"]) || 0 != GetSafeDouble(sItem["MJ36"]) || 0 != GetSafeDouble(sItem["MJ37"]) || 0 != GetSafeDouble(sItem["MJ38"]) || 0 != GetSafeDouble(sItem["MJ39"]) || 0 != GetSafeDouble(sItem["MJ310"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD21"]) / GetSafeDouble(sItem["MJ31"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD22"]) / GetSafeDouble(sItem["MJ32"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD23"]) / GetSafeDouble(sItem["MJ33"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD24"]) / GetSafeDouble(sItem["MJ34"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD25"]) / GetSafeDouble(sItem["MJ35"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD26"]) / GetSafeDouble(sItem["MJ36"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD27"]) / GetSafeDouble(sItem["MJ37"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD28"]) / GetSafeDouble(sItem["MJ38"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD29"]) / GetSafeDouble(sItem["MJ39"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD210"]) / GetSafeDouble(sItem["MJ310"]), 3);

                                    sItem["NJQD31"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD32"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD33"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD34"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD35"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD36"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD37"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD38"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD39"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD310"] = Round(mKyqd10, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD31"]) || 0 != GetSafeDouble(sItem["NJQD32"]) || 0 != GetSafeDouble(sItem["NJQD33"])
                                    || 0 != GetSafeDouble(sItem["NJQD34"]) || 0 != GetSafeDouble(sItem["NJQD35"]) || 0 != GetSafeDouble(sItem["NJQD36"]) || 0 != GetSafeDouble(sItem["NJQD37"]) || 0 != GetSafeDouble(sItem["NJQD38"]) || 0 != GetSafeDouble(sItem["NJQD39"]) || 0 != GetSafeDouble(sItem["NJQD310"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD31"] + "," + sItem["NJQD32"] + "," + sItem["NJQD33"] + "," + sItem["NJQD34"] + "," + sItem["NJQD35"] + "," + sItem["NJQD36"] + "," + sItem["NJQD37"] + "," + sItem["NJQD38"] + "," + sItem["NJQD39"] + "," + sItem["NJQD310"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 10; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD2"] = Round(mkyqdArray.Average(), 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ31"] = (GetSafeDouble(sItem["CD31"]) * GetSafeDouble(sItem["KD31"])).ToString();
                                sItem["MJ32"] = (GetSafeDouble(sItem["CD32"]) * GetSafeDouble(sItem["KD32"])).ToString();
                                sItem["MJ33"] = (GetSafeDouble(sItem["CD33"]) * GetSafeDouble(sItem["KD33"])).ToString();
                                sItem["MJ34"] = (GetSafeDouble(sItem["CD34"]) * GetSafeDouble(sItem["KD34"])).ToString();
                                sItem["MJ35"] = (GetSafeDouble(sItem["CD35"]) * GetSafeDouble(sItem["KD35"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ31"]) || 0 != GetSafeDouble(sItem["MJ32"]) || 0 != GetSafeDouble(sItem["MJ33"])
                                    || 0 != GetSafeDouble(sItem["MJ34"]) || 0 != GetSafeDouble(sItem["MJ35"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD21"]) / GetSafeDouble(sItem["MJ31"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD22"]) / GetSafeDouble(sItem["MJ32"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD23"]) / GetSafeDouble(sItem["MJ33"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD24"]) / GetSafeDouble(sItem["MJ34"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD25"]) / GetSafeDouble(sItem["MJ35"]), 3);

                                    sItem["NJQD31"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD32"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD33"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD34"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD35"] = Round(mKyqd5, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD31"]) || 0 != GetSafeDouble(sItem["NJQD32"]) || 0 != GetSafeDouble(sItem["NJQD33"])
                                    || 0 != GetSafeDouble(sItem["NJQD34"]) || 0 != GetSafeDouble(sItem["NJQD35"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD31"] + "," + sItem["NJQD32"] + "," + sItem["NJQD33"] + "," + sItem["NJQD34"] + "," + sItem["NJQD35"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD2"] = Round(mkyqdArray.Average(), 2).ToString();
                                }
                                #endregion
                            }
                            if (!string.IsNullOrEmpty(sItem["PHJMPD1"]))
                            {
                                if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3 && sItem["PHJMPD1"].Trim() == "合格")
                                {
                                    MItem[0]["HG_LSNJQD3"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_LSNJQD3"] = "不合格";
                                    mAllHg = false;
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3)
                                {
                                    MItem[0]["HG_LSNJQD3"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_LSNJQD3"] = "不合格";
                                    mAllHg = false;
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }

                            MItem[0]["G_LSNJQD3"] = MItem[0]["G_LSNJQD3"] + "，破坏发生在模塑板中";
                            #endregion
                        }
                        else
                        {
                            sItem["YQD1"] = "----";
                            MItem[0]["HG_LSNJQD1"] = "----";
                            MItem[0]["G_LSNJQD1"] = "----";
                            sItem["YQD2"] = "----";
                            MItem[0]["HG_LSNJQD3"] = "----";
                            MItem[0]["G_LSNJQD3"] = "----";
                            //MItem[0]["G_PHJM3"] = "----";
                        }
                        #endregion

                        #region 拉伸粘结耐水强度
                        //if (jcxm.Contains("、拉伸粘结耐水强度、"))
                        //{
                        //    jcxmCur = "拉伸粘结强度与水泥砂浆(耐水强度)";
                        //    #region 与水泥砂浆
                        //    sItem["MJ21"] = (GetSafeDouble(sItem["CD21"]) * GetSafeDouble(sItem["KD21"])).ToString();
                        //    sItem["MJ22"] = (GetSafeDouble(sItem["CD22"]) * GetSafeDouble(sItem["KD22"])).ToString();
                        //    sItem["MJ23"] = (GetSafeDouble(sItem["CD23"]) * GetSafeDouble(sItem["KD23"])).ToString();
                        //    sItem["MJ24"] = (GetSafeDouble(sItem["CD24"]) * GetSafeDouble(sItem["KD24"])).ToString();
                        //    sItem["MJ25"] = (GetSafeDouble(sItem["CD25"]) * GetSafeDouble(sItem["KD25"])).ToString();
                        //    if (0 != GetSafeDouble(sItem["MJ21"]) || 0 != GetSafeDouble(sItem["MJ22"]) || 0 != GetSafeDouble(sItem["MJ23"])
                        //        || 0 != GetSafeDouble(sItem["MJ24"]) || 0 != GetSafeDouble(sItem["MJ25"]))
                        //    {
                        //        mKyqd1 = Round(GetSafeDouble(sItem["JSQD11"]) / GetSafeDouble(sItem["MJ21"]), 3);
                        //        mKYQD2 = Round(GetSafeDouble(sItem["JSQD12"]) / GetSafeDouble(sItem["MJ22"]), 3);
                        //        mKyqd3 = Round(GetSafeDouble(sItem["JSQD13"]) / GetSafeDouble(sItem["MJ23"]), 3);
                        //        mKyqd4 = Round(GetSafeDouble(sItem["JSQD14"]) / GetSafeDouble(sItem["MJ24"]), 3);
                        //        mKyqd5 = Round(GetSafeDouble(sItem["JSQD15"]) / GetSafeDouble(sItem["MJ25"]), 3);

                        //        sItem["NJQD21"] = Round(mKyqd1, 3).ToString();
                        //        sItem["NJQD22"] = Round(mKYQD2, 3).ToString();
                        //        sItem["NJQD23"] = Round(mKyqd3, 3).ToString();
                        //        sItem["NJQD24"] = Round(mKyqd4, 3).ToString();
                        //        sItem["NJQD25"] = Round(mKyqd5, 3).ToString();
                        //    }

                        //    if (0 != GetSafeDouble(sItem["NJQD21"]) || 0 != GetSafeDouble(sItem["NJQD23"]) || 0 != GetSafeDouble(sItem["NJQD22"])
                        //        || 0 != GetSafeDouble(sItem["NJQD24"]) || 0 != GetSafeDouble(sItem["NJQD25"]))
                        //    {
                        //        List<double> mkyqdArray = new List<double>();
                        //        mlongStr = sItem["NJQD21"] + "," + sItem["NJQD22"] + "," + sItem["NJQD23"] + "," + sItem["NJQD24"] + "," + sItem["NJQD25"];
                        //        mtmpArray = mlongStr.Split(',');
                        //        for (int i = 0; i < 5; i++)
                        //        {
                        //            mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                        //        }
                        //        mkyqdArray.Sort();
                        //        sItem["JSQD1"] = Round(mkyqdArray.Average(), 2).ToString();
                        //    }

                        //    if (GetSafeDouble(sItem["JSQD1"]) >= G_LSNJQD2)
                        //    {
                        //        MItem[0]["HG_LSNJQD2"] = "合格";
                        //        mFlag_Hg = true;
                        //    }
                        //    else
                        //    {
                        //        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        //        MItem[0]["HG_LSNJQD2"] = "不合格";
                        //        mAllHg = false;
                        //        mbhggs++;
                        //        mFlag_Bhg = true;
                        //    }

                        //    #endregion

                        //    #region 与聚苯板
                        //    jcxmCur = "拉伸粘结强度与聚苯板(耐水强度)";
                        //    sItem["MJ41"] = (GetSafeDouble(sItem["CD41"]) * GetSafeDouble(sItem["KD41"])).ToString();
                        //    sItem["MJ42"] = (GetSafeDouble(sItem["CD42"]) * GetSafeDouble(sItem["KD42"])).ToString();
                        //    sItem["MJ43"] = (GetSafeDouble(sItem["CD43"]) * GetSafeDouble(sItem["KD43"])).ToString();
                        //    sItem["MJ44"] = (GetSafeDouble(sItem["CD44"]) * GetSafeDouble(sItem["KD44"])).ToString();
                        //    sItem["MJ45"] = (GetSafeDouble(sItem["CD45"]) * GetSafeDouble(sItem["KD45"])).ToString();
                        //    if (0 != GetSafeDouble(sItem["MJ41"]) || 0 != GetSafeDouble(sItem["MJ42"]) || 0 != GetSafeDouble(sItem["MJ43"])
                        //        || 0 != GetSafeDouble(sItem["MJ44"]) || 0 != GetSafeDouble(sItem["MJ45"]))
                        //    {
                        //        mKyqd1 = Round(GetSafeDouble(sItem["JSQD21"]) / GetSafeDouble(sItem["MJ41"]), 3);
                        //        mKYQD2 = Round(GetSafeDouble(sItem["JSQD22"]) / GetSafeDouble(sItem["MJ42"]), 3);
                        //        mKyqd3 = Round(GetSafeDouble(sItem["JSQD23"]) / GetSafeDouble(sItem["MJ43"]), 3);
                        //        mKyqd4 = Round(GetSafeDouble(sItem["JSQD24"]) / GetSafeDouble(sItem["MJ44"]), 3);
                        //        mKyqd5 = Round(GetSafeDouble(sItem["JSQD25"]) / GetSafeDouble(sItem["MJ45"]), 3);

                        //        sItem["NJQD41"] = Round(mKyqd1, 3).ToString();
                        //        sItem["NJQD42"] = Round(mKYQD2, 3).ToString();
                        //        sItem["NJQD43"] = Round(mKyqd3, 3).ToString();
                        //        sItem["NJQD44"] = Round(mKyqd4, 3).ToString();
                        //        sItem["NJQD45"] = Round(mKyqd5, 3).ToString();
                        //    }
                        //    if (0 != GetSafeDouble(sItem["NJQD41"]) || 0 != GetSafeDouble(sItem["NJQD42"]) || 0 != GetSafeDouble(sItem["NJQD43"])
                        //        || 0 != GetSafeDouble(sItem["NJQD44"]) || 0 != GetSafeDouble(sItem["NJQD45"]))
                        //    {
                        //        List<double> mkyqdArray = new List<double>();
                        //        mlongStr = sItem["NJQD41"] + "," + sItem["NJQD42"] + "," + sItem["NJQD43"] + "," + sItem["NJQD44"] + "," + sItem["NJQD45"];
                        //        mtmpArray = mlongStr.Split(',');
                        //        for (int i = 0; i < 5; i++)
                        //        {
                        //            mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                        //        }
                        //        mkyqdArray.Sort();
                        //        sItem["JSQD2"] = Round(mkyqdArray.Average(), 2).ToString();
                        //    }

                        //    if (!string.IsNullOrEmpty(sItem["PHJMPD2"]))
                        //    {
                        //        if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4 && sItem["PHJMPD2"] == "合格")//
                        //        {
                        //            MItem[0]["HG_LSNJQD4"] = "合格";
                        //            mFlag_Hg = true;
                        //        }
                        //        else
                        //        {
                        //            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        //            MItem[0]["HG_LSNJQD4"] = "不合格";
                        //            mAllHg = false;
                        //            mbhggs++;
                        //            mFlag_Bhg = true;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4)//
                        //        {
                        //            MItem[0]["HG_LSNJQD4"] = "合格";
                        //            mFlag_Hg = true;
                        //        }
                        //        else
                        //        {
                        //            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        //            MItem[0]["HG_LSNJQD4"] = "不合格";
                        //            mAllHg = false;
                        //            mbhggs++;
                        //            mFlag_Bhg = true;
                        //        }
                        //    }
                        //    MItem[0]["G_LSNJQD4"] = MItem[0]["G_LSNJQD4"] + "，破坏发生在模塑板中";
                        //    #endregion
                        //}
                        //else
                        //{
                        //    sItem["JSQD1"] = "----";
                        //    MItem[0]["HG_LSNJQD2"] = "----";
                        //    MItem[0]["G_LSNJQD2"] = "----";
                        //    sItem["JSQD2"] = "----";
                        //    MItem[0]["HG_LSNJQD4"] = "----";
                        //    MItem[0]["G_LSNJQD4"] = "----";
                        //}
                        #endregion

                        #region 拉伸粘结耐水强度
                        if (jcxm.Contains("、拉伸粘结耐水强度、"))
                        {
                            jcxmCur = "拉伸粘结强度与水泥砂浆(耐水强度)";
                            #region 与水泥砂浆
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ51"] = (GetSafeDouble(sItem["CD51"]) * GetSafeDouble(sItem["KD51"])).ToString();
                                sItem["MJ52"] = (GetSafeDouble(sItem["CD52"]) * GetSafeDouble(sItem["KD52"])).ToString();
                                sItem["MJ53"] = (GetSafeDouble(sItem["CD53"]) * GetSafeDouble(sItem["KD53"])).ToString();
                                sItem["MJ54"] = (GetSafeDouble(sItem["CD54"]) * GetSafeDouble(sItem["KD54"])).ToString();
                                sItem["MJ55"] = (GetSafeDouble(sItem["CD55"]) * GetSafeDouble(sItem["KD55"])).ToString();
                                sItem["MJ56"] = (GetSafeDouble(sItem["CD56"]) * GetSafeDouble(sItem["KD56"])).ToString();
                                sItem["MJ57"] = (GetSafeDouble(sItem["CD57"]) * GetSafeDouble(sItem["KD57"])).ToString();
                                sItem["MJ58"] = (GetSafeDouble(sItem["CD58"]) * GetSafeDouble(sItem["KD58"])).ToString();
                                sItem["MJ59"] = (GetSafeDouble(sItem["CD59"]) * GetSafeDouble(sItem["KD59"])).ToString();
                                sItem["MJ510"] = (GetSafeDouble(sItem["CD510"]) * GetSafeDouble(sItem["KD510"])).ToString();
                                //sItem["MJ511"] = (GetSafeDouble(sItem["CD511"]) * GetSafeDouble(sItem["KD511"])).ToString();
                                //sItem["MJ512"] = (GetSafeDouble(sItem["CD512"]) * GetSafeDouble(sItem["KD512"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ51"]) || 0 != GetSafeDouble(sItem["MJ52"]) || 0 != GetSafeDouble(sItem["MJ53"])
                                    || 0 != GetSafeDouble(sItem["MJ54"]) || 0 != GetSafeDouble(sItem["MJ55"]) || 0 != GetSafeDouble(sItem["MJ56"]) || 0 != GetSafeDouble(sItem["MJ57"]) || 0 != GetSafeDouble(sItem["MJ58"]) || 0 != GetSafeDouble(sItem["MJ59"]) || 0 != GetSafeDouble(sItem["MJ510"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD51"]) / GetSafeDouble(sItem["MJ51"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD52"]) / GetSafeDouble(sItem["MJ52"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD53"]) / GetSafeDouble(sItem["MJ53"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD54"]) / GetSafeDouble(sItem["MJ54"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD55"]) / GetSafeDouble(sItem["MJ55"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD56"]) / GetSafeDouble(sItem["MJ56"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD57"]) / GetSafeDouble(sItem["MJ57"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD58"]) / GetSafeDouble(sItem["MJ58"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD59"]) / GetSafeDouble(sItem["MJ59"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD510"]) / GetSafeDouble(sItem["MJ510"]), 3);

                                    sItem["NJQD51"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD52"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD53"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD54"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD55"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD56"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD57"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD58"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD59"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD510"] = Round(mKyqd10, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD51"]) || 0 != GetSafeDouble(sItem["NJQD53"]) || 0 != GetSafeDouble(sItem["NJQD52"])
                                    || 0 != GetSafeDouble(sItem["NJQD54"]) || 0 != GetSafeDouble(sItem["NJQD55"]) || 0 != GetSafeDouble(sItem["NJQD56"]) || 0 != GetSafeDouble(sItem["NJQD57"]) || 0 != GetSafeDouble(sItem["NJQD58"]) || 0 != GetSafeDouble(sItem["NJQD59"]) || 0 != GetSafeDouble(sItem["NJQD510"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD51"] + "," + sItem["NJQD52"] + "," + sItem["NJQD53"] + "," + sItem["NJQD54"] + "," + sItem["NJQD55"] + "," + sItem["NJQD56"] + "," + sItem["NJQD57"] + "," + sItem["NJQD58"] + "," + sItem["NJQD59"] + "," + sItem["NJQD510"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 10; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD5"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5] + mkyqdArray[6] + mkyqdArray[7] + mkyqdArray[8] + mkyqdArray[9]) / 10, 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ51"] = (GetSafeDouble(sItem["CD51"]) * GetSafeDouble(sItem["KD51"])).ToString();
                                sItem["MJ52"] = (GetSafeDouble(sItem["CD52"]) * GetSafeDouble(sItem["KD52"])).ToString();
                                sItem["MJ53"] = (GetSafeDouble(sItem["CD53"]) * GetSafeDouble(sItem["KD53"])).ToString();
                                sItem["MJ54"] = (GetSafeDouble(sItem["CD54"]) * GetSafeDouble(sItem["KD54"])).ToString();
                                sItem["MJ55"] = (GetSafeDouble(sItem["CD55"]) * GetSafeDouble(sItem["KD55"])).ToString();
                                //sItem["MJ56"] = (GetSafeDouble(sItem["CD56"]) * GetSafeDouble(sItem["KD56"])).ToString();
                                if (0 != GetSafeDouble(sItem["MJ51"]) || 0 != GetSafeDouble(sItem["MJ52"]) || 0 != GetSafeDouble(sItem["MJ53"])
                                    || 0 != GetSafeDouble(sItem["MJ54"]) || 0 != GetSafeDouble(sItem["MJ55"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD51"]) / GetSafeDouble(sItem["MJ51"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD52"]) / GetSafeDouble(sItem["MJ52"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD53"]) / GetSafeDouble(sItem["MJ53"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD54"]) / GetSafeDouble(sItem["MJ54"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD55"]) / GetSafeDouble(sItem["MJ55"]), 3);
                                    //mKyqd6 = Round(GetSafeDouble(sItem["YQD56"]) / GetSafeDouble(sItem["MJ56"]), 3);

                                    sItem["NJQD51"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD52"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD53"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD54"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD55"] = Round(mKyqd5, 3).ToString();
                                    //sItem["NJQD56"] = Round(mKyqd6, 3).ToString();
                                }

                                if (0 != GetSafeDouble(sItem["NJQD51"]) || 0 != GetSafeDouble(sItem["NJQD53"]) || 0 != GetSafeDouble(sItem["NJQD52"])
                                    || 0 != GetSafeDouble(sItem["NJQD54"]) || 0 != GetSafeDouble(sItem["NJQD55"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD51"] + "," + sItem["NJQD52"] + "," + sItem["NJQD53"] + "," + sItem["NJQD54"] + "," + sItem["NJQD55"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD5"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 5, 2).ToString();
                                }
                                #endregion
                            }

                            if (GetSafeDouble(sItem["YQD5"]) >= G_LSNJQD7)
                            {
                                MItem[0]["HG_LSNJQD5"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_LSNJQD5"] = "不合格";
                                mAllHg = false;
                                mbhggs++;
                                mFlag_Bhg = true;
                            }

                            #endregion

                            #region 与聚苯板
                            jcxmCur = "拉伸粘结强度与聚苯板(耐水强度)";
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ71"] = (GetSafeDouble(sItem["CD71"]) * GetSafeDouble(sItem["KD71"])).ToString();
                                sItem["MJ72"] = (GetSafeDouble(sItem["CD72"]) * GetSafeDouble(sItem["KD72"])).ToString();
                                sItem["MJ73"] = (GetSafeDouble(sItem["CD73"]) * GetSafeDouble(sItem["KD73"])).ToString();
                                sItem["MJ74"] = (GetSafeDouble(sItem["CD74"]) * GetSafeDouble(sItem["KD74"])).ToString();
                                sItem["MJ75"] = (GetSafeDouble(sItem["CD75"]) * GetSafeDouble(sItem["KD75"])).ToString();
                                sItem["MJ76"] = (GetSafeDouble(sItem["CD76"]) * GetSafeDouble(sItem["KD76"])).ToString();
                                sItem["MJ77"] = (GetSafeDouble(sItem["CD77"]) * GetSafeDouble(sItem["KD77"])).ToString();
                                sItem["MJ78"] = (GetSafeDouble(sItem["CD78"]) * GetSafeDouble(sItem["KD78"])).ToString();
                                sItem["MJ79"] = (GetSafeDouble(sItem["CD79"]) * GetSafeDouble(sItem["KD79"])).ToString();
                                sItem["MJ710"] = (GetSafeDouble(sItem["CD710"]) * GetSafeDouble(sItem["KD710"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ71"]) || 0 != GetSafeDouble(sItem["MJ72"]) || 0 != GetSafeDouble(sItem["MJ73"])
                                    || 0 != GetSafeDouble(sItem["MJ74"]) || 0 != GetSafeDouble(sItem["MJ75"]) || 0 != GetSafeDouble(sItem["MJ76"]) || 0 != GetSafeDouble(sItem["MJ77"]) || 0 != GetSafeDouble(sItem["MJ78"]) || 0 != GetSafeDouble(sItem["MJ79"]) || 0 != GetSafeDouble(sItem["MJ710"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD71"]) / GetSafeDouble(sItem["MJ71"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD72"]) / GetSafeDouble(sItem["MJ72"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD73"]) / GetSafeDouble(sItem["MJ73"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD74"]) / GetSafeDouble(sItem["MJ74"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD75"]) / GetSafeDouble(sItem["MJ75"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD76"]) / GetSafeDouble(sItem["MJ76"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD77"]) / GetSafeDouble(sItem["MJ77"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD78"]) / GetSafeDouble(sItem["MJ78"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD79"]) / GetSafeDouble(sItem["MJ79"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD710"]) / GetSafeDouble(sItem["MJ710"]), 3);

                                    sItem["NJQD71"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD72"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD73"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD74"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD75"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD76"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD77"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD78"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD79"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD710"] = Round(mKyqd10, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD71"]) || 0 != GetSafeDouble(sItem["NJQD72"]) || 0 != GetSafeDouble(sItem["NJQD73"])
                                    || 0 != GetSafeDouble(sItem["NJQD74"]) || 0 != GetSafeDouble(sItem["NJQD75"]) || 0 != GetSafeDouble(sItem["NJQD76"]) || 0 != GetSafeDouble(sItem["NJQD77"]) || 0 != GetSafeDouble(sItem["NJQD78"]) || 0 != GetSafeDouble(sItem["NJQD79"]) || 0 != GetSafeDouble(sItem["NJQD710"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD71"] + "," + sItem["NJQD72"] + "," + sItem["NJQD73"] + "," + sItem["NJQD74"] + "," + sItem["NJQD75"] + "," + sItem["NJQD76"] + "," + sItem["NJQD77"] + "," + sItem["NJQD78"] + "," + sItem["NJQD79"] + "," + sItem["NJQD710"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 10; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD7"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5] + mkyqdArray[6] + mkyqdArray[7] + mkyqdArray[8] + mkyqdArray[9]) / 10, 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ71"] = (GetSafeDouble(sItem["CD71"]) * GetSafeDouble(sItem["KD71"])).ToString();
                                sItem["MJ72"] = (GetSafeDouble(sItem["CD72"]) * GetSafeDouble(sItem["KD72"])).ToString();
                                sItem["MJ73"] = (GetSafeDouble(sItem["CD73"]) * GetSafeDouble(sItem["KD73"])).ToString();
                                sItem["MJ74"] = (GetSafeDouble(sItem["CD74"]) * GetSafeDouble(sItem["KD74"])).ToString();
                                sItem["MJ75"] = (GetSafeDouble(sItem["CD75"]) * GetSafeDouble(sItem["KD75"])).ToString();
                                //sItem["MJ76"] = (GetSafeDouble(sItem["CD76"]) * GetSafeDouble(sItem["KD76"])).ToString();
                                if (0 != GetSafeDouble(sItem["MJ71"]) || 0 != GetSafeDouble(sItem["MJ72"]) || 0 != GetSafeDouble(sItem["MJ73"])
                                    || 0 != GetSafeDouble(sItem["MJ74"]) || 0 != GetSafeDouble(sItem["MJ75"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD71"]) / GetSafeDouble(sItem["MJ71"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD72"]) / GetSafeDouble(sItem["MJ72"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD73"]) / GetSafeDouble(sItem["MJ73"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD74"]) / GetSafeDouble(sItem["MJ74"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD75"]) / GetSafeDouble(sItem["MJ75"]), 3);
                                    //mKyqd6 = Round(GetSafeDouble(sItem["YQD76"]) / GetSafeDouble(sItem["MJ76"]), 3);

                                    sItem["NJQD71"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD72"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD73"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD74"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD75"] = Round(mKyqd5, 3).ToString();
                                    //sItem["NJQD76"] = Round(mKyqd6, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD71"]) || 0 != GetSafeDouble(sItem["NJQD72"]) || 0 != GetSafeDouble(sItem["NJQD73"])
                                    || 0 != GetSafeDouble(sItem["NJQD74"]) || 0 != GetSafeDouble(sItem["NJQD75"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD71"] + "," + sItem["NJQD72"] + "," + sItem["NJQD73"] + "," + sItem["NJQD74"] + "," + sItem["NJQD75"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD7"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 5, 2).ToString();
                                }
                                #endregion
                            }
                            if (!string.IsNullOrEmpty(sItem["PHJMPD7"]))
                            {
                                if (GetSafeDouble(sItem["JSQD7"]) >= G_LSNJQD8 && sItem["PHJMPD7"] == "合格")//
                                {
                                    MItem[0]["HG_LSNJQD7"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_LSNJQD7"] = "不合格";
                                    mAllHg = false;
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4)//
                                {
                                    MItem[0]["HG_LSNJQD7"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                    MItem[0]["HG_LSNJQD7"] = "不合格";
                                    mAllHg = false;
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            MItem[0]["G_LSNJQD8"] = MItem[0]["G_LSNJQD8"] + "，破坏发生在模塑板中";
                            #endregion
                        }
                        else
                        {
                            sItem["YQD5"] = "----";
                            MItem[0]["HG_LSNJQD5"] = "----";
                            MItem[0]["G_LSNJQD7"] = "----";
                            sItem["JSQD7"] = "----";
                            MItem[0]["HG_LSNJQD7"] = "----";
                            MItem[0]["G_LSNJQD4"] = "----";
                        }
                        #endregion


                        #region 拉伸粘结耐冻融强度
                        if (jcxm.Contains("、拉伸粘结耐冻融强度、"))
                        {
                            jcxmCur = "拉伸粘结强度与水泥砂浆(耐冻融强度)";
                            #region 与水泥砂浆
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ61"] = (GetSafeDouble(sItem["CD61"]) * GetSafeDouble(sItem["KD61"])).ToString();
                                sItem["MJ62"] = (GetSafeDouble(sItem["CD62"]) * GetSafeDouble(sItem["KD62"])).ToString();
                                sItem["MJ63"] = (GetSafeDouble(sItem["CD63"]) * GetSafeDouble(sItem["KD63"])).ToString();
                                sItem["MJ64"] = (GetSafeDouble(sItem["CD64"]) * GetSafeDouble(sItem["KD64"])).ToString();
                                sItem["MJ65"] = (GetSafeDouble(sItem["CD65"]) * GetSafeDouble(sItem["KD65"])).ToString();
                                sItem["MJ66"] = (GetSafeDouble(sItem["CD66"]) * GetSafeDouble(sItem["KD66"])).ToString();
                                sItem["MJ67"] = (GetSafeDouble(sItem["CD67"]) * GetSafeDouble(sItem["KD67"])).ToString();
                                sItem["MJ68"] = (GetSafeDouble(sItem["CD68"]) * GetSafeDouble(sItem["KD68"])).ToString();
                                sItem["MJ69"] = (GetSafeDouble(sItem["CD69"]) * GetSafeDouble(sItem["KD69"])).ToString();
                                sItem["MJ610"] = (GetSafeDouble(sItem["CD610"]) * GetSafeDouble(sItem["KD610"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ61"]) || 0 != GetSafeDouble(sItem["MJ62"]) || 0 != GetSafeDouble(sItem["MJ63"])
                                    || 0 != GetSafeDouble(sItem["MJ64"]) || 0 != GetSafeDouble(sItem["MJ65"]) || 0 != GetSafeDouble(sItem["MJ66"]) || 0 != GetSafeDouble(sItem["MJ67"]) || 0 != GetSafeDouble(sItem["MJ68"]) || 0 != GetSafeDouble(sItem["MJ69"]) || 0 != GetSafeDouble(sItem["MJ610"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD61"]) / GetSafeDouble(sItem["MJ61"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD62"]) / GetSafeDouble(sItem["MJ62"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD63"]) / GetSafeDouble(sItem["MJ63"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD64"]) / GetSafeDouble(sItem["MJ64"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD65"]) / GetSafeDouble(sItem["MJ65"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD66"]) / GetSafeDouble(sItem["MJ66"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD67"]) / GetSafeDouble(sItem["MJ67"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD68"]) / GetSafeDouble(sItem["MJ68"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD69"]) / GetSafeDouble(sItem["MJ69"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD610"]) / GetSafeDouble(sItem["MJ610"]), 3);

                                    sItem["NJQD61"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD62"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD63"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD64"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD65"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD66"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD67"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD68"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD69"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD610"] = Round(mKyqd10, 3).ToString();

                                }
                                if (0 != GetSafeDouble(sItem["NJQD61"]) || 0 != GetSafeDouble(sItem["NJQD62"]) || 0 != GetSafeDouble(sItem["NJQD63"])
                                    || 0 != GetSafeDouble(sItem["NJQD64"]) || 0 != GetSafeDouble(sItem["NJQD65"]) || 0 != GetSafeDouble(sItem["NJQD66"]) || 0 != GetSafeDouble(sItem["NJQD67"]) || 0 != GetSafeDouble(sItem["NJQD68"]) || 0 != GetSafeDouble(sItem["NJQD69"]) || 0 != GetSafeDouble(sItem["NJQD610"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD61"] + "," + sItem["NJQD62"] + "," + sItem["NJQD63"] + "," + sItem["NJQD64"] + "," + sItem["NJQD65"] + "," + sItem["NJQD66"] + "," + sItem["NJQD67"] + "," + sItem["NJQD68"] + "," + sItem["NJQD69"] + "," + sItem["NJQD610"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 10; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["DRQD6"] = Round(mkyqdArray.Average(), 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ61"] = (GetSafeDouble(sItem["CD61"]) * GetSafeDouble(sItem["KD61"])).ToString();
                                sItem["MJ62"] = (GetSafeDouble(sItem["CD62"]) * GetSafeDouble(sItem["KD62"])).ToString();
                                sItem["MJ63"] = (GetSafeDouble(sItem["CD63"]) * GetSafeDouble(sItem["KD63"])).ToString();
                                sItem["MJ64"] = (GetSafeDouble(sItem["CD64"]) * GetSafeDouble(sItem["KD64"])).ToString();
                                sItem["MJ65"] = (GetSafeDouble(sItem["CD65"]) * GetSafeDouble(sItem["KD65"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ61"]) || 0 != GetSafeDouble(sItem["MJ62"]) || 0 != GetSafeDouble(sItem["MJ63"])
                                    || 0 != GetSafeDouble(sItem["MJ64"]) || 0 != GetSafeDouble(sItem["MJ65"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD61"]) / GetSafeDouble(sItem["MJ61"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD62"]) / GetSafeDouble(sItem["MJ62"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD63"]) / GetSafeDouble(sItem["MJ63"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD64"]) / GetSafeDouble(sItem["MJ64"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD65"]) / GetSafeDouble(sItem["MJ65"]), 3);

                                    sItem["NJQD61"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD62"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD63"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD64"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD65"] = Round(mKyqd5, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD61"]) || 0 != GetSafeDouble(sItem["NJQD62"]) || 0 != GetSafeDouble(sItem["NJQD63"])
                                    || 0 != GetSafeDouble(sItem["NJQD64"]) || 0 != GetSafeDouble(sItem["NJQD65"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD61"] + "," + sItem["NJQD62"] + "," + sItem["NJQD63"] + "," + sItem["NJQD64"] + "," + sItem["NJQD65"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["DRQD6"] = Round(mkyqdArray.Average(), 2).ToString();
                                }
                                #endregion
                            }
                            if (GetSafeDouble(sItem["DRQD6"]) >= G_LSNJQD5)
                            {
                                MItem[0]["HG_LSNJQD6"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_LSNJQD6"] = "不合格";
                                mAllHg = false;
                                mbhggs++;
                                mFlag_Bhg = true;
                            }
                            #endregion

                            #region 与聚苯板
                            jcxmCur = "拉伸粘结强度与聚苯板(耐冻融强度)";
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                sItem["MJ81"] = (GetSafeDouble(sItem["CD81"]) * GetSafeDouble(sItem["KD81"])).ToString();
                                sItem["MJ82"] = (GetSafeDouble(sItem["CD82"]) * GetSafeDouble(sItem["KD82"])).ToString();
                                sItem["MJ83"] = (GetSafeDouble(sItem["CD83"]) * GetSafeDouble(sItem["KD83"])).ToString();
                                sItem["MJ84"] = (GetSafeDouble(sItem["CD84"]) * GetSafeDouble(sItem["KD84"])).ToString();
                                sItem["MJ85"] = (GetSafeDouble(sItem["CD85"]) * GetSafeDouble(sItem["KD85"])).ToString();
                                sItem["MJ86"] = (GetSafeDouble(sItem["CD86"]) * GetSafeDouble(sItem["KD86"])).ToString();
                                sItem["MJ87"] = (GetSafeDouble(sItem["CD87"]) * GetSafeDouble(sItem["KD87"])).ToString();
                                sItem["MJ88"] = (GetSafeDouble(sItem["CD88"]) * GetSafeDouble(sItem["KD88"])).ToString();
                                sItem["MJ89"] = (GetSafeDouble(sItem["CD89"]) * GetSafeDouble(sItem["KD89"])).ToString();
                                sItem["MJ810"] = (GetSafeDouble(sItem["CD810"]) * GetSafeDouble(sItem["KD810"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ81"]) || 0 != GetSafeDouble(sItem["MJ82"]) || 0 != GetSafeDouble(sItem["MJ83"])
                                    || 0 != GetSafeDouble(sItem["MJ84"]) || 0 != GetSafeDouble(sItem["MJ85"]) || 0 != GetSafeDouble(sItem["MJ86"]) || 0 != GetSafeDouble(sItem["MJ87"]) || 0 != GetSafeDouble(sItem["MJ88"]) || 0 != GetSafeDouble(sItem["MJ89"]) || 0 != GetSafeDouble(sItem["MJ810"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD81"]) / GetSafeDouble(sItem["MJ81"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD82"]) / GetSafeDouble(sItem["MJ82"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD83"]) / GetSafeDouble(sItem["MJ83"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD84"]) / GetSafeDouble(sItem["MJ84"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD85"]) / GetSafeDouble(sItem["MJ85"]), 3);
                                    mKyqd6 = Round(GetSafeDouble(sItem["YQD86"]) / GetSafeDouble(sItem["MJ86"]), 3);
                                    mKyqd7 = Round(GetSafeDouble(sItem["YQD87"]) / GetSafeDouble(sItem["MJ87"]), 3);
                                    mKyqd8 = Round(GetSafeDouble(sItem["YQD88"]) / GetSafeDouble(sItem["MJ88"]), 3);
                                    mKyqd9 = Round(GetSafeDouble(sItem["YQD89"]) / GetSafeDouble(sItem["MJ89"]), 3);
                                    mKyqd10 = Round(GetSafeDouble(sItem["YQD810"]) / GetSafeDouble(sItem["MJ810"]), 3);

                                    sItem["NJQD81"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD82"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD83"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD84"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD85"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD86"] = Round(mKyqd6, 3).ToString();
                                    sItem["NJQD87"] = Round(mKyqd7, 3).ToString();
                                    sItem["NJQD88"] = Round(mKyqd8, 3).ToString();
                                    sItem["NJQD89"] = Round(mKyqd9, 3).ToString();
                                    sItem["NJQD810"] = Round(mKyqd10, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD81"]) || 0 != GetSafeDouble(sItem["NJQD82"]) || 0 != GetSafeDouble(sItem["NJQD83"])
                                    || 0 != GetSafeDouble(sItem["NJQD84"]) || 0 != GetSafeDouble(sItem["NJQD85"]) || 0 != GetSafeDouble(sItem["NJQD86"]) || 0 != GetSafeDouble(sItem["NJQD87"]) || 0 != GetSafeDouble(sItem["NJQD88"]) || 0 != GetSafeDouble(sItem["NJQD89"]) || 0 != GetSafeDouble(sItem["NJQD810"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD81"] + "," + sItem["NJQD82"] + "," + sItem["NJQD83"] + "," + sItem["NJQD84"] + "," + sItem["NJQD85"] + "," + sItem["NJQD86"] + "," + sItem["NJQD87"] + "," + sItem["NJQD88"] + "," + sItem["NJQD89"] + "," + sItem["NJQD810"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 10; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["DRQD8"] = Round(mkyqdArray.Average(), 2).ToString();
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                sItem["MJ81"] = (GetSafeDouble(sItem["CD81"]) * GetSafeDouble(sItem["KD81"])).ToString();
                                sItem["MJ82"] = (GetSafeDouble(sItem["CD82"]) * GetSafeDouble(sItem["KD82"])).ToString();
                                sItem["MJ83"] = (GetSafeDouble(sItem["CD83"]) * GetSafeDouble(sItem["KD83"])).ToString();
                                sItem["MJ84"] = (GetSafeDouble(sItem["CD84"]) * GetSafeDouble(sItem["KD84"])).ToString();
                                sItem["MJ85"] = (GetSafeDouble(sItem["CD85"]) * GetSafeDouble(sItem["KD85"])).ToString();

                                if (0 != GetSafeDouble(sItem["MJ81"]) || 0 != GetSafeDouble(sItem["MJ82"]) || 0 != GetSafeDouble(sItem["MJ83"])
                                    || 0 != GetSafeDouble(sItem["MJ84"]) || 0 != GetSafeDouble(sItem["MJ85"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["YQD81"]) / GetSafeDouble(sItem["MJ81"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["YQD82"]) / GetSafeDouble(sItem["MJ82"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["YQD83"]) / GetSafeDouble(sItem["MJ83"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["YQD84"]) / GetSafeDouble(sItem["MJ84"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["YQD85"]) / GetSafeDouble(sItem["MJ85"]), 3);

                                    sItem["NJQD81"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD82"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD83"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD84"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD85"] = Round(mKyqd5, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD81"]) || 0 != GetSafeDouble(sItem["NJQD82"]) || 0 != GetSafeDouble(sItem["NJQD83"])
                                    || 0 != GetSafeDouble(sItem["NJQD84"]) || 0 != GetSafeDouble(sItem["NJQD85"]))
                                {
                                    List<double> mkyqdArray = new List<double>();
                                    mlongStr = sItem["NJQD81"] + "," + sItem["NJQD82"] + "," + sItem["NJQD83"] + "," + sItem["NJQD84"] + "," + sItem["NJQD85"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray.Add(GetSafeDouble(mtmpArray[i]));
                                    }
                                    mkyqdArray.Sort();
                                    sItem["DRQD8"] = Round(mkyqdArray.Average(), 2).ToString();
                                }
                                #endregion
                            }
                            if (GetSafeDouble(sItem["DRQD8"]) >= G_LSNJQD6)
                            {
                                MItem[0]["HG_LSNJQD8"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_LSNJQD8"] = "不合格";
                                mAllHg = false;
                                mbhggs++;
                                mFlag_Bhg = true;
                            }
                            MItem[0]["G_LSNJQD6"] = MItem[0]["G_LSNJQD6"] + "，破坏发生在模塑板中";
                            #endregion
                        }
                        else
                        {
                            sItem["DRQD6"] = "----";
                            MItem[0]["HG_LSNJQD6"] = "----";
                            MItem[0]["G_LSNJQD6"] = "----";
                            sItem["DRQD8"] = "----";
                            MItem[0]["HG_LSNJQD8"] = "----";
                            MItem[0]["G_LSNJQD8"] = "----";
                        }
                        #endregion

                        #region 可操作时间
                        if (jcxm.Contains("、可操作时间、"))
                        {
                            jcxmCur = "可操作时间";
                            if (GetSafeDouble(sItem["KCZSJ"]) >= GetSafeDouble(MItem[0]["G_KCZSJ"]))
                            {
                                MItem[0]["HG_KCZSJ"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KCZSJ"] = "不合格";
                                mAllHg = false;
                                mbhggs++;
                                mFlag_Bhg = true;
                            }
                            MItem[0]["G_KCZSJ"] = "≥" + MItem[0]["G_KCZSJ"];
                        }
                        else
                        {
                            sItem["KCZSJ"] = "----";
                            MItem[0]["G_KCZSJ"] = "----";
                            MItem[0]["HG_KCZSJ"] = "----";
                        }
                        #endregion
                        #endregion
                    }
                    else
                    {

                    }

                    if (mbhggs == 0)
                    {
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目均符合要求。";
                        }
                        else
                        {
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        }
                           
                        mAllHg = true;
                        sItem["JCJG"] = "合格";
                    }
                    else
                    {
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        }
                        else
                        {
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，需双倍复检。";
                        }
                           
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                        //if (mFlag_Bhg && mFlag_Hg)
                        //{
                        //    jsbeizhu = "该组试样所检项目部分符合上述标准要求。";
                        //}
                    }
                }
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGSM"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}