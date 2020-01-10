using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace Calculates
{
    public class BN : BaseMethods
    {
        public void Calc()
        {
            #region
            string mlongStr;
            string[] mfuncVal = new string[5];
            List<double> mkyqdArray = new List<double>();
            string[] mtmpArray = new string[5];
            bool mAllHg = true, mFlag_Hg = true, mFlag_Bhg = true, canSetBgbh = true;
            int mbhggs = 0;
            string mJSFF = "";
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_BN"];
            var mrsDj = dataExtra["BZ_BN_DJ"];
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

            double mKyqd1, mKYQD2, mKyqd3, mKyqd4, mKyqd5, mKyqd6;
            double G_LSNJQD1 = 0, G_LSNJQD2 = 0, G_LSNJQD3 = 0, G_LSNJQD4 = 0;
            var jcxm = "";


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
                        if (Conversion.Val(sItem["YQD1"]) == 0)
                        {
                            return false;

                        }

                        sItem["YQD1"] = Conversion.Val(sItem["YQD1"]).ToString("0.0");
                        mItem["HG_LSNJQD1"] = IsQualified(mItem["G_LSNJQD1"], sItem["YQD1"], false);

                        if (mItem["HG_LSNJQD1"] != "不合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
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
                        if (Conversion.Val(sItem["JSQD1"]) == 0)
                        {
                            return false;
                        }

                        sItem["JSQD1"] = Conversion.Val(sItem["JSQD1"]).ToString("0.0");
                        mItem["G_LSNJQD2"] = IsQualified(mItem["G_LSNJQD2"], sItem["JSQD1"], false);

                        if (mItem["G_LSNJQD2"] != "不合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
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
                        if (Conversion.Val(sItem["YQD2"]) == 0 && sItem["YQDPHJM2"].Trim() == "")
                        {
                            return false;
                        }

                        sItem["YQD2"] = Conversion.Val(sItem["YQD2"]).ToString("0.0");

                        if (sItem["YQDPHJM2)"].Trim() == "EPS板破坏")
                        {
                            mItem["HG_LSNJQD3"] = IsQualified(mItem["G_LSNJQD3"], sItem["YQD2"], false);
                        }
                        else
                        {
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

                        mItem["HG_KCZSJ"] = IsQualified(mItem["G_KCZSJ"], sItem["KCZSJ"], false);


                        if (mItem["HG_KCZSJ"] != "不合格")
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
                        sItem["KCZSJ"] = "----";
                        mItem["G_KCZSJ"] = "----";
                        mItem["HG_KCZSJ"] = "----";
                    }



                    if (mItem["QRBM"].Contains("90"))
                    {
                        mItem["HG_KCZSJ"] = IsQualified(mItem["G_KCZSJ"], sItem["KCZSJ"], false);


                        if (mItem["HG_KCZSJ"] != "不合格")
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
                        sItem["KCZSJ"] = "----";
                        mItem["G_KCZSJ"] = "----";
                        mItem["HG_KCZSJ"] = "----";
                    }

                    if (mbhggs == 0)
                    {
                        jsbeizhu = "该组试样所检项目符合" + mItem["PDBZ"] + "标准要求。";
                        sItem["JCJG"] = "合格";
                    }
                    if (mbhggs > 0)
                    {
                        jsbeizhu = "该组试样所检项目不符合" + mItem["PDBZ"] + "标准要求。";
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
                if (mrsDj != null)
                {
                    G_LSNJQD1 = GetSafeDouble(mrsDj[0]["LSNJQD1"]);
                    G_LSNJQD2 = GetSafeDouble(mrsDj[0]["LSNJQD2"]);
                    G_LSNJQD3 = GetSafeDouble(mrsDj[0]["LSNJQD3"]);
                    G_LSNJQD4 = GetSafeDouble(mrsDj[0]["LSNJQD4"]);
                    MItem[0]["G_LSNJQD1"] = "≥" + mrsDj[0]["LSNJQD1"];
                    MItem[0]["G_LSNJQD2"] = "≥" + mrsDj[0]["LSNJQD2"];
                    MItem[0]["G_LSNJQD3"] = "≥" + mrsDj[0]["LSNJQD3"];
                    MItem[0]["G_LSNJQD4"] = "≥" + mrsDj[0]["LSNJQD4"];
                    MItem[0]["G_PHJM3"] = "破坏界面在EPS板内";
                    MItem[0]["G_PHJM4"] = "破坏界面在EPS板内";
                    MItem[0]["G_KCZSJ"] = mrsDj[0]["KCZSJ"];
                    MItem[0]["BGNAME"] = mrsDj[0]["BGNAME"];
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "依据不详";
                    MItem[0]["JSBEIZHU"] = "依据不详";
                }
                if (!sItem["SJDJ"].Contains("144") && !sItem["SJDJ"].Contains("149"))
                {
                    sItem["JCJG"] = "依据不详";
                    MItem[0]["JSBEIZHU"] = "依据不详";
                    break;
                }

                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
                    //、、、、、旋转裂纹、旋转盖板、旋转缩松、旋转抗滑、旋转破坏、旋转力矩、旋转一般项、对接裂纹、对接盖板、对接缩松、对接抗拉、对接力矩、对接一般项
                    if (mJSFF == "")
                    {
                        if (sItem["SJDJ"].Contains("144"))
                        {
                            if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(干燥状态)、"))
                            {
                                if (0 == GetSafeDouble(sItem["YQD11"]))
                                {
                                    mSFwc = false;
                                }

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
                                    sItem["YQD1"] = Round(GetSafeDouble(sItem["NJQD11"]) + GetSafeDouble(sItem["NJQD12"]) + GetSafeDouble(sItem["NJQD13"]) +
                                        GetSafeDouble(sItem["NJQD14"]) + GetSafeDouble(sItem["NJQD15"]), 2).ToString();
                                }
                                if (GetSafeDouble(sItem["YQD1"]) >= G_LSNJQD1)
                                {
                                    MItem[0]["HG_LSNJQD1"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    MItem[0]["HG_LSNJQD1"] = "不合格";
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                sItem["YQD1"] = "----";
                                MItem[0]["HG_LSNJQD1"] = "----";
                                MItem[0]["G_LSNJQD1"] = "----";
                            }

                            if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(浸水48h)、"))
                            {
                                if (0 == GetSafeDouble(sItem["JSQD11"]))
                                {
                                    mSFwc = false;
                                }
                                sItem["MJ21"] = (GetSafeDouble(sItem["CD21"]) * GetSafeDouble(sItem["KD21"])).ToString();
                                sItem["MJ22"] = (GetSafeDouble(sItem["CD22"]) * GetSafeDouble(sItem["KD22"])).ToString();
                                sItem["MJ23"] = (GetSafeDouble(sItem["CD23"]) * GetSafeDouble(sItem["KD23"])).ToString();
                                sItem["MJ24"] = (GetSafeDouble(sItem["CD24"]) * GetSafeDouble(sItem["KD24"])).ToString();
                                sItem["MJ25"] = (GetSafeDouble(sItem["CD25"]) * GetSafeDouble(sItem["KD25"])).ToString();
                                if (0 != GetSafeDouble(sItem["MJ21"]) || 0 != GetSafeDouble(sItem["MJ22"]) || 0 != GetSafeDouble(sItem["MJ23"])
                                    || 0 != GetSafeDouble(sItem["MJ24"]) || 0 != GetSafeDouble(sItem["MJ25"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["JSQD11"]) / GetSafeDouble(sItem["MJ21"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["JSQD12"]) / GetSafeDouble(sItem["MJ22"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["JSQD13"]) / GetSafeDouble(sItem["MJ23"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["JSQD14"]) / GetSafeDouble(sItem["MJ24"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["JSQD15"]) / GetSafeDouble(sItem["MJ25"]), 3);

                                    sItem["NJQD21"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD22"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD23"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD24"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD25"] = Round(mKyqd5, 3).ToString();
                                }
                                //0  != GetSafeDouble(sItem["NJQD22"])|| 
                                if (0 != GetSafeDouble(sItem["NJQD21"]) || 0 != GetSafeDouble(sItem["NJQD23"])
                                    || 0 != GetSafeDouble(sItem["NJQD24"]) || 0 != GetSafeDouble(sItem["NJQD25"]))
                                {
                                    //GetSafeDouble(sItem["NJQD22"]) + 
                                    sItem["JSQD1"] = Round(GetSafeDouble(sItem["NJQD21"]) + GetSafeDouble(sItem["NJQD23"]) +
                                        GetSafeDouble(sItem["NJQD24"]) + GetSafeDouble(sItem["NJQD25"]), 2).ToString();
                                }
                                else
                                {
                                    canSetBgbh = false;
                                }
                                if (GetSafeDouble(sItem["JSQD1"]) >= G_LSNJQD2)
                                {
                                    MItem[0]["HG_LSNJQD2"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    MItem[0]["HG_LSNJQD2"] = "不合格";
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                sItem["JSQD1"] = "----";
                                MItem[0]["HG_LSNJQD2"] = "----";
                                MItem[0]["G_LSNJQD2"] = "----";
                            }

                            if (jcxm.Contains("、与EPS板拉伸粘结强度(干燥状态)、"))
                            {
                                if (0 == GetSafeDouble(sItem["YQD21"]))
                                {
                                    mSFwc = false;
                                }
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
                                if (0 != GetSafeDouble(sItem["NJQD31"]) || GetSafeDouble(sItem["NJQD32"]) != 0 || 0 != GetSafeDouble(sItem["NJQD33"])
                                    || 0 != GetSafeDouble(sItem["NJQD34"]) || 0 != GetSafeDouble(sItem["NJQD35"]))
                                {
                                    sItem["YQD2"] = Round(GetSafeDouble(sItem["NJQD31"]) + GetSafeDouble(sItem["NJQD32"]) + GetSafeDouble(sItem["NJQD33"]) +
                                        GetSafeDouble(sItem["NJQD34"]) + GetSafeDouble(sItem["NJQD35"]), 2).ToString();
                                }
                                //null 不进入
                                if (!string.IsNullOrEmpty(sItem["PHJMPD1"]))
                                {
                                    if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3 && sItem["PHJMPD1"] == "合格")//
                                    {
                                        MItem[0]["HG_LSNJQD3"] = "合格";
                                        mFlag_Hg = true;
                                    }
                                    else
                                    {
                                        MItem[0]["HG_LSNJQD3"] = "不合格";
                                        mbhggs++;
                                        mFlag_Bhg = true;
                                    }
                                }
                                else
                                {
                                    if (GetSafeDouble(sItem["YQD2"]) >= G_LSNJQD3)//
                                    {
                                        MItem[0]["HG_LSNJQD3"] = "合格";
                                        mFlag_Hg = true;
                                    }
                                    else
                                    {
                                        MItem[0]["HG_LSNJQD3"] = "不合格";
                                        mbhggs++;
                                        mFlag_Bhg = true;
                                    }
                                }

                            }
                            else
                            {
                                sItem["YQD2"] = "----";
                                MItem[0]["HG_LSNJQD3"] = "----";
                                MItem[0]["G_LSNJQD3"] = "----";
                                MItem[0]["G_PHJM3"] = "----";
                            }

                            //
                            if (jcxm.Contains("、与EPS板拉伸粘结强度(浸水48h)、"))
                            {
                                if (0 == GetSafeDouble(sItem["JSQD21"]))
                                {
                                    mSFwc = false;
                                }
                                sItem["MJ41"] = (GetSafeDouble(sItem["CD41"]) * GetSafeDouble(sItem["KD41"])).ToString();
                                sItem["MJ42"] = (GetSafeDouble(sItem["CD42"]) * GetSafeDouble(sItem["KD42"])).ToString();
                                sItem["MJ43"] = (GetSafeDouble(sItem["CD43"]) * GetSafeDouble(sItem["KD43"])).ToString();
                                sItem["MJ44"] = (GetSafeDouble(sItem["CD44"]) * GetSafeDouble(sItem["KD44"])).ToString();
                                sItem["MJ45"] = (GetSafeDouble(sItem["CD45"]) * GetSafeDouble(sItem["KD45"])).ToString();
                                if (0 != GetSafeDouble(sItem["MJ41"]) || 0 != GetSafeDouble(sItem["MJ42"]) || 0 != GetSafeDouble(sItem["MJ43"])
                                    || 0 != GetSafeDouble(sItem["MJ44"]) || 0 != GetSafeDouble(sItem["MJ45"]))
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["JSQD21"]) / GetSafeDouble(sItem["MJ41"]), 3);
                                    mKYQD2 = Round(GetSafeDouble(sItem["JSQD22"]) / GetSafeDouble(sItem["MJ42"]), 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["JSQD23"]) / GetSafeDouble(sItem["MJ43"]), 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["JSQD24"]) / GetSafeDouble(sItem["MJ44"]), 3);
                                    mKyqd5 = Round(GetSafeDouble(sItem["JSQD25"]) / GetSafeDouble(sItem["MJ45"]), 3);

                                    sItem["NJQD41"] = Round(mKyqd1, 3).ToString();
                                    sItem["NJQD42"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD43"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD44"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD45"] = Round(mKyqd5, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD41"]) || 0 != GetSafeDouble(sItem["NJQD42"]) || 0 != GetSafeDouble(sItem["NJQD43"])
                                    || 0 != GetSafeDouble(sItem["NJQD44"]) || 0 != GetSafeDouble(sItem["NJQD45"]))
                                {
                                    sItem["JSQD2"] = Round(GetSafeDouble(sItem["NJQD41"]) + GetSafeDouble(sItem["NJQD42"]) + GetSafeDouble(sItem["NJQD43"]) +
                                        GetSafeDouble(sItem["NJQD44"]) + GetSafeDouble(sItem["NJQD45"]), 2).ToString();
                                }
                                else
                                {
                                    canSetBgbh = false;
                                }
                                if (!string.IsNullOrEmpty(sItem["PHJMPD2"]))
                                {
                                    if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4 && sItem["PHJMPD2"] == "合格")
                                    {
                                        MItem[0]["HG_LSNJQD4"] = "合格";
                                        mFlag_Hg = true;
                                    }
                                    else
                                    {
                                        MItem[0]["HG_LSNJQD4"] = "不合格";
                                        mbhggs++;
                                        mFlag_Bhg = true;
                                    }
                                }
                                else
                                {
                                    if (GetSafeDouble(sItem["JSQD2"]) >= G_LSNJQD4)
                                    {
                                        MItem[0]["HG_LSNJQD4"] = "合格";
                                        mFlag_Hg = true;
                                    }
                                    else
                                    {
                                        MItem[0]["HG_LSNJQD4"] = "不合格";
                                        mbhggs++;
                                        mFlag_Bhg = true;
                                    }
                                }

                            }
                            else
                            {
                                sItem["JSQD2"] = "----";
                                MItem[0]["HG_LSNJQD4"] = "----";
                                MItem[0]["G_LSNJQD4"] = "----";
                                MItem[0]["G_PHJM3"] = "----";
                            }
                            sItem["KCZSJ"] = "----";
                            MItem[0]["G_KCZSJ"] = "----";
                            MItem[0]["HG_KCZSJ"] = "----";
                        }
                        else
                        {
                            if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(干燥状态)、"))
                            {
                                if (0 == GetSafeDouble(sItem["YQD11"]))
                                {
                                    mSFwc = false;
                                }
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
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD1"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                                }
                                if (GetSafeDouble(sItem["YQD1"]) >= G_LSNJQD1)
                                {
                                    MItem[0]["HG_LSNJQD1"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    MItem[0]["HG_LSNJQD1"] = "不合格";
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                sItem["YQD1"] = "----";
                                MItem[0]["HG_LSNJQD1"] = "----";
                                MItem[0]["G_LSNJQD1"] = "----";
                            }

                            if (jcxm.Contains("、与水泥砂浆拉伸粘结强度(浸水48h)、"))
                            {
                                if (0 == GetSafeDouble(sItem["JSQD11"]))
                                {
                                    mSFwc = false;
                                }
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
                                    //sItem["NJQD22"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD23"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD24"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD25"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD26"] = Round(mKyqd6, 3).ToString();
                                }
                                //|| 0 != GetSafeDouble(sItem["NJQD22"]) 
                                if (0 != GetSafeDouble(sItem["NJQD21"]) || 0 != GetSafeDouble(sItem["NJQD23"])
                                    || 0 != GetSafeDouble(sItem["NJQD24"]) || 0 != GetSafeDouble(sItem["NJQD25"]) || 0 != GetSafeDouble(sItem["NJQD26"]))
                                {
                                    //"," + sItem["NJQD22"] +
                                    mlongStr = sItem["NJQD21"] + "," + sItem["NJQD23"] + "," + sItem["NJQD24"] + "," + sItem["NJQD25"] + "," + sItem["NJQD26"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD1"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                                }
                                else
                                {
                                    canSetBgbh = false;
                                }
                                if (GetSafeDouble(sItem["JSQD1"]) >= G_LSNJQD2)
                                {
                                    MItem[0]["HG_LSNJQD2"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    MItem[0]["HG_LSNJQD2"] = "不合格";
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                sItem["JSQD1"] = "----";
                                MItem[0]["HG_LSNJQD2"] = "----";
                                MItem[0]["G_LSNJQD2"] = "----";
                            }

                            if (jcxm.Contains("、与EPS板拉伸粘结强度(干燥状态)、"))
                            {
                                if (0 == GetSafeDouble(sItem["YQD21"]))
                                {
                                    mSFwc = false;
                                }
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
                                    mlongStr = sItem["NJQD31"] + "," + sItem["NJQD32"] + "," + sItem["NJQD33"] + "," + sItem["NJQD34"] + "," + sItem["NJQD35"] + "," + sItem["NJQD36"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                                    }
                                    mkyqdArray.Sort();
                                    sItem["YQD2"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
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
                                        MItem[0]["HG_LSNJQD3"] = "不合格";
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
                                        MItem[0]["HG_LSNJQD3"] = "不合格";
                                        mbhggs++;
                                        mFlag_Bhg = true;
                                    }
                                }

                            }
                            else
                            {
                                sItem["YQD2"] = "----";
                                MItem[0]["HG_LSNJQD3"] = "----";
                                MItem[0]["G_LSNJQD3"] = "----";
                                MItem[0]["G_PHJM3"] = "----";
                            }

                            if (jcxm.Contains("、与EPS板拉伸粘结强度(浸水48h)、"))
                            {
                                if (0 == GetSafeDouble(sItem["JSQD11"]))
                                {
                                    mSFwc = false;
                                }
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

                                    sItem["NJQD21"] = Round(mKyqd1, 3).ToString();
                                    //sItem["NJQD22"] = Round(mKYQD2, 3).ToString();
                                    sItem["NJQD23"] = Round(mKyqd3, 3).ToString();
                                    sItem["NJQD24"] = Round(mKyqd4, 3).ToString();
                                    sItem["NJQD25"] = Round(mKyqd5, 3).ToString();
                                    sItem["NJQD26"] = Round(mKyqd6, 3).ToString();
                                }
                                if (0 != GetSafeDouble(sItem["NJQD41"]) || 0 != GetSafeDouble(sItem["NJQD42"]) || 0 != GetSafeDouble(sItem["NJQD43"])
                                    || 0 != GetSafeDouble(sItem["NJQD44"]) || 0 != GetSafeDouble(sItem["NJQD45"]) || 0 != GetSafeDouble(sItem["NJQD46"]))
                                {
                                    mlongStr = sItem["NJQD41"] + "," + sItem["NJQD42"] + "," + sItem["NJQD43"] + "," + sItem["NJQD44"] + "," + sItem["NJQD45"] + "," + sItem["NJQD46"];
                                    mtmpArray = mlongStr.Split(',');
                                    for (int i = 0; i < 5; i++)
                                    {
                                        mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                                    }
                                    mkyqdArray.Sort();
                                    sItem["JSQD2"] = Round((mkyqdArray[0] + mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4]) / 4, 2).ToString();
                                }
                                else
                                {
                                    canSetBgbh = false;
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
                                        MItem[0]["HG_LSNJQD4"] = "不合格";
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
                                        MItem[0]["HG_LSNJQD4"] = "不合格";
                                        mbhggs++;
                                        mFlag_Bhg = true;
                                    }
                                }
                            }
                            else
                            {
                                sItem["JSQD2"] = "----";
                                MItem[0]["HG_LSNJQD4"] = "----";
                                MItem[0]["G_LSNJQD4"] = "----";
                                MItem[0]["G_PHJM4"] = "----";
                            }

                            if (jcxm.Contains("、可操作时间、"))
                            {
                                if (GetSafeDouble(sItem["KCZSJ"]) >= 1.5 && GetSafeDouble(sItem["KCZSJ"]) <= 4)
                                {
                                    MItem[0]["HG_KCZSJ"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    MItem[0]["HG_KCZSJ"] = "不合格";
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                sItem["KCZSJ"] = "----";
                                MItem[0]["G_KCZSJ"] = "----";
                                MItem[0]["HG_KCZSJ"] = "----";
                            }
                        }
                    }
                    if (mbhggs == 0)
                    {
                        jsbeizhu = "该组试样所检项目符合上述标准要求。";
                        mAllHg = true;
                        sItem["JCJG"] = "合格";
                    }
                    else
                    {
                        jsbeizhu = "该组试样不符合上述标准要求。";
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                        if (mFlag_Bhg && mFlag_Hg)
                        {
                            jsbeizhu = "该组试样所检项目部分符合上述标准要求。";
                        }
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