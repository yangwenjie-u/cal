﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class QK2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItems = retData["S_QK2"];
            var extraGMD = dataExtra["BZ_QK2GMDJB"];
            var extraDJ = dataExtra["BZ_QK2_DJ"];
            var extraQK2QDJB = dataExtra["BZ_QK2QDJB"];
            //var ZM_DRJL = retData["ZM_DRJL"];

            if (!retData.ContainsKey("M_QK2"))
            {
                retData["M_QK2"] = new List<IDictionary<string, string>>();
            }
            var MItem = retData["M_QK2"];

            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;
            var jcxm = "";
            var mSjdj = "";
            var mJSFF = "";
            var mSz = 0.0;
            var mbhggs = 0;
            double md, md1, md2, sum, pjmd = 0;
            int mcd, mdwz = 0;
            bool sign = false;
            int xd, Gs;
            double[] nArray;
            bool mSFwc = true;
            var jcxmBhg = "";
            var jcxmCur = "";
            string drzlsslyq = "", drkyqdsslyq = "";
            List<double> narr = new List<double>();
            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
            delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mbhggs = 0;
                sum = 0;

                //Rs("select * from ZM_DRJL", adOpenStatic, adLockBatchOptimistic)
                if (jcxm.Contains("、干密度、"))
                {
                    jcxmCur = "干密度";
                    sItem["GMDPD"] = IsQualified(sItem["GMDYQ"], sItem["GMDPJ"]);

                    if (sItem["GMDPD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                        mbhggs += 1;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["GMDPJ"] = "----";
                    sItem["GMDPD"] = "----";
                    sItem["GMDYQ"] = "----";
                }

                if (jcxm.Contains("、抗压、") || jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "抗压,抗压强度");
                    if (0 == Conversion.Val(sItem["KYHZ1_1"]))
                    {
                        return false;
                    }

                    sItem["HSLPD"] = IsQualified(sItem["HSLYQ"], sItem["HSLPJ"]);

                    for (int i = 1; i < 4; i++)
                    {
                        sItem["KYQD1_" + i] = Math.Round(1000 * Conversion.Val(sItem["KYHZ1_" + i]) / Conversion.Val(sItem["QCD1_" + i]) / Conversion.Val(sItem["QKD1_" + i]), 1).ToString("0.0");
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["KYQD2_" + i] = Math.Round(1000 * Conversion.Val(sItem["KYHZ2_" + i]) / Conversion.Val(sItem["QCD2_" + i]) / Conversion.Val(sItem["QKD2_" + i]), 1).ToString("0.0");
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["KYQD3_" + i] = Math.Round(1000 * Conversion.Val(sItem["KYHZ3_" + i]) / Conversion.Val(sItem["QCD3_" + i]) / Conversion.Val(sItem["QKD3_" + i]), 1).ToString("0.0");
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["KYPJ" + i] = Math.Round(((Conversion.Val(sItem["KYQD" + i + "_1"]) + Conversion.Val(sItem["KYQD" + i + "_2"]) + Conversion.Val(sItem["KYQD" + i + "_3"])) / 3), 1).ToString("0.0");
                    }

                    sItem["KYPJ"] = Math.Round((Conversion.Val(sItem["KYPJ1"]) + Conversion.Val(sItem["KYPJ2"]) + Conversion.Val(sItem["KYPJ3"])) / 3, 1).ToString("0.0");
                    sItem["DKZX"] = sItem["KYPJ1"];

                    if (Conversion.Val(sItem["DKZX"]) > Conversion.Val(sItem["KYPJ2"]))
                    {
                        sItem["DKZX"] = sItem["KYPJ2"];
                    }
                    if (Conversion.Val(sItem["DKZX"]) > Conversion.Val(sItem["KYPJ3"]))
                    {
                        sItem["DKZX"] = sItem["KYPJ3"];
                    }

                    if (Conversion.Val(sItem["KYPJ"]) >= Conversion.Val(mItem["G_PJZ"]) && Conversion.Val(sItem["DKZX"]) >= Conversion.Val(mItem["G_MIN"]))
                    {
                        sItem["QDPD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["QDPD"] = "不合格";
                        mFlag_Bhg = true;
                        mbhggs += 1;
                    }
                }
                else
                {

                    sItem["DKZX"] = "----";
                    sItem["QDPD"] = "----";
                    sItem["QDYQ"] = "----";
                    sItem["KYPJ"] = "----";
                    sItem["HSLPJ"] = "----";
                    sItem["HSLPD"] = "----";
                }

                sign = string.IsNullOrEmpty(sItem["WGPD"]) ? false : sItem["WGPD"].Contains("试件明显破坏") ? false : true;

                if (jcxm.Contains("、冻后强度、") && sign)
                {
                    jcxmCur = "冻后强度";
                    if (0 == Conversion.Val(sItem["DHKYHZ1_1"]))
                    {
                        return false;
                    }

                    for (Gs = 1; Gs < 4; Gs++)
                    {
                        sum = 0;
                        for (xd = 0; xd < 4; xd++)
                        {
                            if (IsNumeric(sItem["DHKYHZ" + Gs + "_" + xd]) && IsNumeric(sItem["DHKYHZ" + Gs + "_" + xd]))
                            {
                                md = Conversion.Val(sItem["DHKYHZ" + Gs + "_" + xd]);
                                md1 = Conversion.Val(sItem["DHQCD" + Gs + "_" + xd]);
                                md2 = Conversion.Val(sItem["DHQKD" + Gs + "_" + xd]);
                                md = Math.Round(1000 * md / md1 / md2, 1);
                                sItem["DHKYQD" + Gs + "_" + xd] = md.ToString("0.0");
                                sum = sum + md;
                            }
                            else
                            {
                                sItem["DHKYQD" + Gs + "_" + xd] = "----";
                            }

                        }

                        pjmd = Math.Round(sum / 3, 1);

                        if (pjmd == 0)
                        {
                            sItem["DHKYPJ" + Gs] = "----";
                        }
                        else
                        {
                            sItem["DHKYPJ" + Gs] = pjmd.ToString("0.0");
                        }
                    }

                    sItem["DHQDPD"] = IsQualified(sItem["DHQDYQ"], sItem["DHKYDZZX"], false);

                    if (sItem["DHQDPD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                        mbhggs += 1;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["DHQDYQ"] = sign ? sItem["DHQDYQ"] : "----";
                    sItem["DHQDPD"] = "----";
                    sItem["DHKYDZZX"] = "----";
                    //mrsmainTablesItem["WHICH") = IIf(sign, "bgqk2", "bgqk2_1")
                }

                if (jcxm.Contains("、外观质量、") && sign)
                {
                    jcxmCur = "外观质量";
                    if (sign)
                    {
                        sItem["LLSSLPD"] = IsQualified(sItem["LLSSLYQ"], sItem["LLSSL"], false);

                        if (sItem["LLSSLPD"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mFlag_Bhg = true;
                            mbhggs += 1;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sItem["LLSSLYQ"] = sign ? sItem["LLSSLYQ"] : "----";
                    sItem["LLSSL"] = "----";
                    sItem["LLSSLPD"] = "----";
                    //   'mrsmainTablesItem["WHICH")"]="bgqk2_2"
                }

                if (sign == false)
                {
                    sItem["LLSSLPD"] = "不合格";
                    sItem["DHQDPD"] = "不合格";
                    mFlag_Bhg = true;
                    mbhggs += 1;
                }
                else
                {
                    mFlag_Hg = true;
                }


                if (jcxm.Contains("、导热系数、"))
                {
                    jcxmCur = "导热系数";
                    mcd = sItem["DRXSYQ"].Length;
                    mdwz = sItem["DRXSYQ"].IndexOf(',');
                    mcd = mcd - mdwz + 1;

                    string DEVCODE = String.IsNullOrEmpty(mItem["DEVCODE"]) ? "" : mItem["DEVCODE"];
                    if (DEVCODE == "" && DEVCODE.Contains("XCS17-067") || DEVCODE.Contains("XCS17-066"))
                    {
                        //var mrsDrxs1= ZM_DRJL.FirstOrDefault(u => u["SYLB"] == "qk2" && u["SYBH"] == mItem["JYDBH"]);
                        //var mrsDrxs = ZM_DRJL;
                        //sItem["DRXS"] = ZM_DRJL[0]["DRXS"];
                        //mItem["JCYJ"] = mItem["JCYJ"].Replace("10294", "10295");
                    }

                    sItem["DRXS"] = Math.Round(Conversion.Val(sItem["DRXS"]), mcd).ToString();

                    sItem["DRXSPD"] = IsQualified(sItem["DRXSYQ"], sItem["DRXS"], false);

                    if (sItem["DRXSPD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                        mbhggs += 1;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }

                }
                else
                {
                    sItem["DRXSYQ"] = "----";
                    sItem["DRXSPD"] = "----";
                    sItem["DRXS"] = "----";
                }

                if (mbhggs == 0)
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                    sItem["JCJG"] = "合格";
                    mAllHg = true;
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;

                    if (sign)
                    {
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";

                        //if (mFlag_Bhg & mFlag_Hg)
                        //{
                        //    jsbeizhu = "该组试件所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                        //}
                    }
                    else
                    {
                        jsbeizhu = "冻融中试件破坏，该组试件不符合" + mItem["PDBZ"] + "标准要求。";
                    }
                }
                return mAllHg;
            };



            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mSjdj = sItem["SJDJ"];//设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                {
                    mSjdj = "";
                }

                #region
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj && u["PZ"] == sItem["PZ"]);

                if (null == mrsDj)
                {
                    jsbeizhu = "依据不详\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不下结论";
                    mJSFF = "";
                    mSz = 0;
                    continue;
                }
                else
                {
                    drzlsslyq = mrsDj["DRZLSSL"];
                    drkyqdsslyq = mrsDj["DRKYQDSSL"];
                    sItem["DRXSYQ"] = mrsDj["G_DRXS"];
                }

                mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].ToLower();

                foreach (var mrsGmdjb in extraGMD)
                {
                    if (mrsGmdjb["MC"].Trim() == sItem["SJDJ"] && mrsGmdjb["PZ"].Trim() == sItem["PZ"])
                    {
                        MItem[0]["G_GMD"] = GetSafeDouble(mrsGmdjb["GMD"]).ToString("0");
                        break;
                    }
                }
                sItem["QDYQ"] = "平均值≥" + Conversion.Val(MItem[0]["G_PJZ"]).ToString("0.0") + "MPa " + "，单组最小值≥" + Conversion.Val(MItem[0]["G_MIN"]).ToString("0.0") + "MPa ";

                //'含水率
                sItem["HSLYQ"] = "8～12";
                sItem["XSLYQ"] = "8～12";
                MItem[0]["WCYQ"] = mrsDj["WCYQ"];


                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
                    if (jcxm.Contains("、干密度、"))
                    {
                        jcxmCur = "干密度";
                        sItem["GMD1_1"] = Round((double.Parse(sItem["HGHZL1_1"]) / double.Parse(sItem["CD1_1"]) / double.Parse(sItem["KD1_1"]) / double.Parse(sItem["GD1_1"])) * Math.Pow(10, 6), 2).ToString("0.00");
                        sItem["GMD1_2"] = Round((double.Parse(sItem["HGHZL1_2"]) / double.Parse(sItem["CD1_2"]) / double.Parse(sItem["KD1_2"]) / double.Parse(sItem["GD1_2"])) * Math.Pow(10, 6), 2).ToString("0.00");
                        sItem["GMD1_3"] = Round((double.Parse(sItem["HGHZL1_3"]) / double.Parse(sItem["CD1_3"]) / double.Parse(sItem["KD1_3"]) / double.Parse(sItem["GD1_3"])) * Math.Pow(10, 6), 2).ToString("0.00");
                        sItem["GMD2_1"] = Round((double.Parse(sItem["HGHZL2_1"]) / double.Parse(sItem["CD2_1"]) / double.Parse(sItem["KD2_1"]) / double.Parse(sItem["GD2_1"])) * Math.Pow(10, 6), 2).ToString("0.00");
                        sItem["GMD2_2"] = Round((double.Parse(sItem["HGHZL2_2"]) / double.Parse(sItem["CD2_2"]) / double.Parse(sItem["KD2_2"]) / double.Parse(sItem["GD2_2"])) * Math.Pow(10, 6), 2).ToString("0.00");
                        sItem["GMD2_3"] = Round((double.Parse(sItem["HGHZL2_3"]) / double.Parse(sItem["CD2_3"]) / double.Parse(sItem["KD2_3"]) / double.Parse(sItem["GD2_3"])) * Math.Pow(10, 6), 2).ToString("0.00");
                        sItem["GMD3_1"] = Round((double.Parse(sItem["HGHZL3_1"]) / double.Parse(sItem["CD3_1"]) / double.Parse(sItem["KD3_1"]) / double.Parse(sItem["GD3_1"])) * Math.Pow(10, 6), 2).ToString("0.00");
                        sItem["GMD3_2"] = Round((double.Parse(sItem["HGHZL3_2"]) / double.Parse(sItem["CD3_2"]) / double.Parse(sItem["KD3_2"]) / double.Parse(sItem["GD3_2"])) * Math.Pow(10, 6), 2).ToString("0.00");
                        sItem["GMD3_3"] = Round((double.Parse(sItem["HGHZL3_3"]) / double.Parse(sItem["CD3_3"]) / double.Parse(sItem["KD3_3"]) / double.Parse(sItem["GD3_3"])) * Math.Pow(10, 6), 2).ToString("0.00");
                        sItem["GMD1"] = Round((double.Parse(sItem["GMD1_1"]) + double.Parse(sItem["GMD1_2"]) + double.Parse(sItem["GMD1_3"])) / 3, 0).ToString("0");
                        sItem["GMD2"] = Round((double.Parse(sItem["GMD2_1"]) + double.Parse(sItem["GMD2_2"]) + double.Parse(sItem["GMD2_3"])) / 3, 0).ToString("0");
                        sItem["GMD3"] = Round((double.Parse(sItem["GMD3_1"]) + double.Parse(sItem["GMD3_2"]) + double.Parse(sItem["GMD3_3"])) / 3, 0).ToString("0");
                        sItem["GMDPJ"] = Round((double.Parse(sItem["GMD1"]) + double.Parse(sItem["GMD2"]) + double.Parse(sItem["GMD3"])) / 3, 0).ToString("0");
                        if (Conversion.Val(sItem["GMDPJ"]) == 0)
                        { }
                        if (extraGMD == null)
                            break;
                        IDictionary<string, string> extraGMD_item = new Dictionary<string, string>();
                        for (int i = 0; i < extraGMD.Count(); i++)
                        {
                            if (extraGMD[i]["MC"].Trim() == sItem["SJDJ"].Trim() && extraGMD[i]["PZ"] == sItem["PZ"].Trim())
                            {
                                MItem[0]["G_GMD"] = GetSafeDouble(extraGMD[i]["GMD"]).ToString("0");
                                extraGMD_item = extraGMD[i];
                                break;
                            }
                            if (i >= extraGMD.Count())
                                extraGMD_item = extraGMD[extraGMD.Count() - 1];
                            else
                                extraGMD_item = extraGMD[i + 1];
                        }
                        if (Conversion.Val(sItem["GMDPJ"]) <= Conversion.Val(extraGMD_item["GMD"]))
                        {
                            sItem["GMDPD"] = "合格";
                        }
                        else
                        {
                            sItem["GMDPD"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        sItem["GMDYQ"] = "≤" + MItem[0]["G_GMD"].Trim();
                    }
                    else
                        sItem["GMDPD"] = "----";
                    IDictionary<string, string> extraQK2QDJB_item = new Dictionary<string, string>();
                    for (int i = 0; i < extraQK2QDJB.Count(); i++)
                    {
                        if (extraQK2QDJB[i]["MC"] == sItem["QDJB"])
                        {
                            MItem[0]["G_PJZ"] = extraQK2QDJB[i]["PJBXY"];
                            MItem[0]["G_MIN"] = extraQK2QDJB[i]["DKBXY"];
                            extraQK2QDJB_item = extraQK2QDJB[i];
                            break;
                        }
                        if (i >= extraQK2QDJB.Count())
                            extraQK2QDJB_item = extraQK2QDJB[extraQK2QDJB.Count() - 1];
                        else
                            extraQK2QDJB_item = extraQK2QDJB[i + 1];
                    }
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        jcxmCur = "抗压强度";
                        sItem["QDYQ"] = "平均值≥" + Conversion.Val(extraQK2QDJB_item["PJBXY"]).ToString("0.0").Trim() + "MPa，单组最小值≥" + Conversion.Val(extraQK2QDJB_item["DKBXY"]).ToString("0.0").Trim() + "MPa";
                        if (Conversion.Val(sItem["HGQZL1_1"]) > 0)
                        {
                            sItem["HSL1_1"] = Round((Conversion.Val(sItem["HGQZL1_1"]) - Conversion.Val(sItem["HHGHZL1_1"])) / (Conversion.Val(sItem["HHGHZL1_1"])) * 100, 2).ToString("0.00");
                            sItem["HSL1_2"] = Round((Conversion.Val(sItem["HGQZL1_2"]) - Conversion.Val(sItem["HHGHZL1_2"])) / (Conversion.Val(sItem["HHGHZL1_2"])) * 100, 2).ToString("0.00");
                            sItem["HSL1_3"] = Round((Conversion.Val(sItem["HGQZL1_3"]) - Conversion.Val(sItem["HHGHZL1_3"])) / (Conversion.Val(sItem["HHGHZL1_3"])) * 100, 2).ToString("0.00");
                            sItem["HSL2_1"] = Round((Conversion.Val(sItem["HGQZL2_1"]) - Conversion.Val(sItem["HHGHZL2_1"])) / (Conversion.Val(sItem["HHGHZL2_1"])) * 100, 2).ToString("0.00");
                            sItem["HSL2_2"] = Round((Conversion.Val(sItem["HGQZL2_2"]) - Conversion.Val(sItem["HHGHZL2_2"])) / (Conversion.Val(sItem["HHGHZL2_2"])) * 100, 2).ToString("0.00");
                            sItem["HSL2_3"] = Round((Conversion.Val(sItem["HGQZL2_3"]) - Conversion.Val(sItem["HHGHZL2_3"])) / (Conversion.Val(sItem["HHGHZL2_3"])) * 100, 2).ToString("0.00");
                            sItem["HSL3_1"] = Round((Conversion.Val(sItem["HGQZL3_1"]) - Conversion.Val(sItem["HHGHZL3_1"])) / (Conversion.Val(sItem["HHGHZL3_1"])) * 100, 2).ToString("0.00");
                            sItem["HSL3_2"] = Round((Conversion.Val(sItem["HGQZL3_2"]) - Conversion.Val(sItem["HHGHZL3_2"])) / (Conversion.Val(sItem["HHGHZL3_2"])) * 100, 2).ToString("0.00");
                            sItem["HSL3_3"] = Round((Conversion.Val(sItem["HGQZL3_3"]) - Conversion.Val(sItem["HHGHZL3_3"])) / (Conversion.Val(sItem["HHGHZL3_3"])) * 100, 2).ToString("0.00");
                        }
                        if (Conversion.Val(sItem["HSL1_1"]) > 0)
                        {
                            sItem["HSL1"] = Round((Conversion.Val(sItem["HSL1_1"]) + Conversion.Val(sItem["HSL1_2"]) + Conversion.Val(sItem["HSL1_3"])) / 3, 1).ToString("0.0");
                            sItem["HSL2"] = Round((Conversion.Val(sItem["HSL2_1"]) + Conversion.Val(sItem["HSL2_2"]) + Conversion.Val(sItem["HSL2_3"])) / 3, 1).ToString("0.0");
                            sItem["HSL3"] = Round((Conversion.Val(sItem["HSL3_1"]) + Conversion.Val(sItem["HSL3_2"]) + Conversion.Val(sItem["HSL3_3"])) / 3, 1).ToString("0.0");
                        }
                        if (Conversion.Val(sItem["HSL1"]) > 0)
                            sItem["HSLPJ"] = Round((Conversion.Val(sItem["HSL1"]) + Conversion.Val(sItem["HSL2"]) + Conversion.Val(sItem["HSL3"])) / 3, 1).ToString("0.0");
                        if ((Conversion.Val(sItem["KYHZ1_1"])) == 0)
                            mSFwc = false;
                        sItem["KYQD1_1"] = Round(1000 * (Conversion.Val(sItem["KYHZ1_1"])) / (Conversion.Val(sItem["QCD1_1"])) / (Conversion.Val(sItem["QKD1_1"])), 1).ToString("0.0");
                        sItem["KYQD1_2"] = Round(1000 * (Conversion.Val(sItem["KYHZ1_2"])) / (Conversion.Val(sItem["QCD1_2"])) / (Conversion.Val(sItem["QKD1_2"])), 1).ToString("0.0");
                        sItem["KYQD1_3"] = Round(1000 * (Conversion.Val(sItem["KYHZ1_3"])) / (Conversion.Val(sItem["QCD1_3"])) / (Conversion.Val(sItem["QKD1_3"])), 1).ToString("0.0");
                        sItem["KYQD2_1"] = Round(1000 * (Conversion.Val(sItem["KYHZ2_1"])) / (Conversion.Val(sItem["QCD2_1"])) / (Conversion.Val(sItem["QKD2_1"])), 1).ToString("0.0");
                        sItem["KYQD2_2"] = Round(1000 * (Conversion.Val(sItem["KYHZ2_2"])) / (Conversion.Val(sItem["QCD2_2"])) / (Conversion.Val(sItem["QKD2_2"])), 1).ToString("0.0");
                        sItem["KYQD2_3"] = Round(1000 * (Conversion.Val(sItem["KYHZ2_3"])) / (Conversion.Val(sItem["QCD2_3"])) / (Conversion.Val(sItem["QKD2_3"])), 1).ToString("0.0");
                        sItem["KYQD3_1"] = Round(1000 * (Conversion.Val(sItem["KYHZ3_1"])) / (Conversion.Val(sItem["QCD3_1"])) / (Conversion.Val(sItem["QKD3_1"])), 1).ToString("0.0");
                        sItem["KYQD3_2"] = Round(1000 * (Conversion.Val(sItem["KYHZ3_2"])) / (Conversion.Val(sItem["QCD3_2"])) / (Conversion.Val(sItem["QKD3_2"])), 1).ToString("0.0");
                        sItem["KYQD3_3"] = Round(1000 * (Conversion.Val(sItem["KYHZ3_3"])) / (Conversion.Val(sItem["QCD3_3"])) / (Conversion.Val(sItem["QKD3_3"])), 1).ToString("0.0");
                        //计算平均值
                        sItem["KYPJ1"] = Round((Conversion.Val(sItem["KYQD1_1"]) + Conversion.Val(sItem["KYQD1_2"]) + Conversion.Val(sItem["KYQD1_3"])) / 3, 1).ToString("0.0");
                        sItem["KYPJ2"] = Round((Conversion.Val(sItem["KYQD2_1"]) + Conversion.Val(sItem["KYQD2_2"]) + Conversion.Val(sItem["KYQD2_3"])) / 3, 1).ToString("0.0");
                        sItem["KYPJ3"] = Round((Conversion.Val(sItem["KYQD3_1"]) + Conversion.Val(sItem["KYQD3_2"]) + Conversion.Val(sItem["KYQD3_3"])) / 3, 1).ToString("0.0");
                        sItem["KYPJ"] = Round(((Conversion.Val(sItem["KYPJ1"])) + (Conversion.Val(sItem["KYPJ2"])) + (Conversion.Val(sItem["KYPJ3"]))) / 3, 1).ToString("0.0");
                        //计算最大、最小值
                        sItem["DKZX"] = sItem["KYPJ1"];
                        if (Conversion.Val(sItem["DKZX"]) > Conversion.Val(sItem["KYPJ2"]))
                            sItem["DKZX"] = sItem["KYPJ2"];
                        if (Conversion.Val(sItem["DKZX"]) > Conversion.Val(sItem["KYPJ3"]))
                            sItem["DKZX"] = sItem["KYPJ3"];
                        for (int i = 0; i < extraQK2QDJB.Count(); i++)
                        {
                            if (extraQK2QDJB[i]["MC"] == sItem["QDJB"].Trim())
                            {
                                MItem[0]["G_PJZ"] = extraQK2QDJB[i]["PJBXY"];
                                MItem[0]["G_MIN"] = extraQK2QDJB[i]["DKBXY"];
                                break;
                            }
                            if (i >= extraQK2QDJB.Count())
                                extraQK2QDJB_item = extraQK2QDJB[extraQK2QDJB.Count() - 1];
                            else
                                extraQK2QDJB_item = extraQK2QDJB[i + 1];
                        }

                        if (Conversion.Val(sItem["KYPJ"]) >= Conversion.Val(extraQK2QDJB_item["PJBXY"]) && (Conversion.Val(sItem["DKZX"])) >= Conversion.Val(extraQK2QDJB_item["DKBXY"]))
                        {
                            sItem["QDPD"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["QDPD"] = "不合格";
                        }
                    }
                    else
                        sItem["QDPD"] = "----";
                    if (jcxm.Contains("、外观质量、") || jcxm.Contains("、尺寸偏差、"))
                    {
                        jcxmCur = CurrentJcxm(jcxm, "外观质量,尺寸偏差");
                        if (Conversion.Val(sItem["WCBHGS"]) <= Conversion.Val(mrsDj["WCYQ"]))
                        {
                            sItem["WCPD"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["WCPD"] = "不合格";
                        }
                    }
                    else
                        sItem["WCPD"] = "----";
                    if (string.IsNullOrEmpty(sItem["SFPL"]))
                        sItem["SFPL"] = "否";
                    sign = sItem["SFPL"] == "否" ? true : false;
                    if (jcxm.Contains("、冻后强度、") && sign)
                    {
                        jcxmCur = "冻后强度";
                        if (Conversion.Val(sItem["DHKYHZ1_1"]) == 0 && Conversion.Val(sItem["DHZD1_1"]) == 0)
                        {
                            mSFwc = false;
                        }
                        for (Gs = 1; Gs <= 3; Gs++)
                        {
                            sum = 0;
                            for (xd = 1; xd <= 3; xd++)
                            {
                                if (IsNumeric(sItem["DHKYHZ" + Gs + "_" + xd]) && (Conversion.Val(sItem["DHKYHZ" + Gs + "_" + xd])) > 0)
                                {
                                    md = (Conversion.Val(sItem["DHKYHZ" + Gs + "_" + xd]));
                                    md1 = (Conversion.Val(sItem["DHQCD" + Gs + "_" + xd]));
                                    md2 = (Conversion.Val(sItem["DHQKD" + Gs + "_" + xd]));
                                    md = 1000 * md / md1 / md2;
                                    md = Round(md, 1);
                                    sItem["DHKYQD" + Gs + "_" + xd] = md.ToString("0.0");
                                    sum = sum + md;
                                }
                                else
                                    sItem["DHKYQD" + Gs + "_" + xd] = "----";
                            }
                            pjmd = sum / 3;
                            pjmd = Round(pjmd, 1);
                            if (pjmd == 0)
                                sItem["DHKYPJ" + Gs] = "----";
                            else
                                sItem["DHKYPJ" + Gs] = pjmd.ToString("0.0");
                        }
                        sItem["DHKYPJ"] = Round(Conversion.Val(sItem["DHKYPJ1"]) + Conversion.Val(sItem["DHKYPJ2"]) + Conversion.Val(sItem["DHKYPJ3"]) / 3, 1).ToString("0.0");
                        //计算最大、最小值
                        sItem["DHKYDZZX"] = sItem["DHKYPJ1"];
                        if ((Conversion.Val(sItem["DHKYDZZX"])) > (Conversion.Val(sItem["DHKYPJ2"])))
                            sItem["DHKYDZZX"] = sItem["DHKYPJ2"];
                        if ((Conversion.Val(sItem["DHKYDZZX"])) > (Conversion.Val(sItem["DHKYPJ3"])))
                            sItem["DHKYDZZX"] = sItem["DHKYPJ3"];
                        if (IsQualified(sItem["DHQDYQ"], sItem["DHKYDZZX"]) == "符合")
                        {
                            sItem["DHQDPD"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["DHQDPD"] = "不合格";
                        }
                    }
                    else
                        sItem["DHQDPD"] = "----";
                    if (jcxm.Contains("、质量损失、"))
                    {
                        jcxmCur = "质量损失";
                        if (sign)
                        {
                            double mzlss1_1 = Round((Conversion.Val(sItem["DQZD1_1"]) - Conversion.Val(sItem["DHZD1_1"])) / (Conversion.Val(sItem["DQZD1_1"])) * 100, 1);
                            double mzlss1_2 = Round((Conversion.Val(sItem["DQZD1_2"]) - Conversion.Val(sItem["DHZD1_2"])) / (Conversion.Val(sItem["DQZD1_2"])) * 100, 1);
                            double mzlss1_3 = Round((Conversion.Val(sItem["DQZD1_3"]) - Conversion.Val(sItem["DHZD1_3"])) / (Conversion.Val(sItem["DQZD1_3"])) * 100, 1);
                            double mzlss2_1 = Round((Conversion.Val(sItem["DQZD2_1"]) - Conversion.Val(sItem["DHZD2_1"])) / (Conversion.Val(sItem["DQZD2_1"])) * 100, 1);
                            double mzlss2_2 = Round((Conversion.Val(sItem["DQZD2_2"]) - Conversion.Val(sItem["DHZD2_2"])) / (Conversion.Val(sItem["DQZD2_2"])) * 100, 1);
                            double mzlss2_3 = Round((Conversion.Val(sItem["DQZD2_3"]) - Conversion.Val(sItem["DHZD2_3"])) / (Conversion.Val(sItem["DQZD2_3"])) * 100, 1);
                            double mzlss3_1 = Round((Conversion.Val(sItem["DQZD3_1"]) - Conversion.Val(sItem["DHZD3_1"])) / (Conversion.Val(sItem["DQZD3_1"])) * 100, 1);
                            double mzlss3_2 = Round((Conversion.Val(sItem["DQZD3_2"]) - Conversion.Val(sItem["DHZD3_2"])) / (Conversion.Val(sItem["DQZD3_2"])) * 100, 1);
                            double mzlss3_3 = Round((Conversion.Val(sItem["DQZD3_3"]) - Conversion.Val(sItem["DHZD3_3"])) / (Conversion.Val(sItem["DQZD3_3"])) * 100, 1);
                            sItem["LLSSL1"] = Round((mzlss1_1 + mzlss1_2 + mzlss1_3) / 3, 1).ToString("0.0");
                            sItem["LLSSL2"] = Round((mzlss2_1 + mzlss2_2 + mzlss2_3) / 3, 1).ToString("0.0");
                            sItem["LLSSL3"] = Round((mzlss3_1 + mzlss3_2 + mzlss3_3) / 3, 1).ToString("0.0");
                            sItem["LLSSL"] = sItem["LLSSL1"];
                            if (Conversion.Val(sItem["LLSSL"]) < Conversion.Val(sItem["LLSSL2"]))
                                sItem["LLSSL"] = sItem["LLSSL2"];
                            if (Conversion.Val(sItem["LLSSL"]) < Conversion.Val(sItem["LLSSL3"]))
                                sItem["LLSSL"] = sItem["LLSSL3"];
                            if (IsQualified(sItem["LLSSLYQ"], sItem["LLSSL"]) == "符合")
                            {
                                sItem["LLSSLPD"] = "合格";
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["LLSSLPD"] = "不合格";
                            }
                            //mrsmainTablesItem["WHICH") = "bgqk2"
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["LLSSLPD"] = "不合格";
                            sItem["DHQDPD"] = "不合格";
                            //mrsmainTablesItem["WHICH") = "bgqk2_1"
                        }
                    }
                    else
                    {
                        sItem["LLSSLPD"] = "----";
                        //mrsmainTablesItem["WHICH") = "bgqk2_2"
                    }
                    if (jcxm.Contains("、导热系数、"))
                    {
                        jcxmCur = "导热系数";
                        if (Conversion.Val(sItem["DRXS"]) == 0)
                        { }
                        if (IsQualified(sItem["DRXSYQ"], sItem["DRXS"], false) == "合格")
                        {
                            sItem["DRXSPD"] = "合格";
                        }
                        else
                        {
                            sItem["DRXSPD"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }

                    }
                    else
                        sItem["DRXSPD"] = "----";

                    #region 含水率和吸水率
                    if (jcxm.Contains("、含水率和吸水率、") || jcxm.Contains("、含水率、"))
                    {
                        jcxmCur = CurrentJcxm(jcxm, "含水率和吸水率,含水率");
                        if (Conversion.Val(sItem["HSL1_1"]) > 0)
                        {
                            sItem["HSL1"] = Round((Conversion.Val(sItem["HSL1_1"]) + Conversion.Val(sItem["HSL1_2"]) + Conversion.Val(sItem["HSL1_3"])) / 3, 1).ToString("0.0");
                            sItem["HSL2"] = Round((Conversion.Val(sItem["HSL2_1"]) + Conversion.Val(sItem["HSL2_2"]) + Conversion.Val(sItem["HSL2_3"])) / 3, 1).ToString("0.0");
                            sItem["HSL3"] = Round((Conversion.Val(sItem["HSL3_1"]) + Conversion.Val(sItem["HSL3_2"]) + Conversion.Val(sItem["HSL3_3"])) / 3, 1).ToString("0.0");
                        }
                        if (Conversion.Val(sItem["HSL1"]) > 0)
                            sItem["HSLPJ"] = Round((Conversion.Val(sItem["HSL1"]) + Conversion.Val(sItem["HSL2"]) + Conversion.Val(sItem["HSL3"])) / 3, 1).ToString("0.0");

                        sItem["HSLPD"] = IsQualified(sItem["HSLYQ"], sItem["HSLPJ"], false);
                        if (sItem["HSLPD"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        sItem["HSLPD"] = "----";
                    }
                    #endregion 

                    #region 吸水率
                    if (jcxm.Contains("、吸水率、"))
                    {
                        jcxmCur = "吸水率";
                        //if (Conversion.Val(sItem["XSL1_1"]) > 0)
                        //{
                        //    sItem["XSL1"] = Round((Conversion.Val(sItem["XSL1_1"]) + Conversion.Val(sItem["XSL1_2"]) + Conversion.Val(sItem["XSL1_3"])) / 3, 1).ToString("0.0");
                        //    sItem["XSL2"] = Round((Conversion.Val(sItem["XSL2_1"]) + Conversion.Val(sItem["XSL2_2"]) + Conversion.Val(sItem["XSL2_3"])) / 3, 1).ToString("0.0");
                        //    sItem["XSL3"] = Round((Conversion.Val(sItem["XSL3_1"]) + Conversion.Val(sItem["XSL3_2"]) + Conversion.Val(sItem["XSL3_3"])) / 3, 1).ToString("0.0");
                        //}
                        if (Conversion.Val(sItem["XSL1"]) > 0)
                            sItem["XSLPJ"] = Round((Conversion.Val(sItem["XSL1"]) + Conversion.Val(sItem["XSL2"]) + Conversion.Val(sItem["XSL3"])) / 3, 1).ToString("0.0");

                        sItem["XSLPD"] = IsQualified(sItem["XSLYQ"], sItem["XSLPJ"], false);
                        if (sItem["XSLPD"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        sItem["XSLPD"] = "----";
                    }
                    #endregion 

                    #region 抗冻性
                    if (jcxm.Contains("、抗冻性、"))
                    {
                        jcxmCur = "抗冻性";
                        #region 质量损失率
                        if (!string.IsNullOrEmpty(sItem["DRDQGZ1"]))
                        {
                            List<double> larray = new List<double>();
                            for (int i = 1; i < 6; i++)
                            {
                                sItem["DRZLSSL" + i] = Math.Round((Conversion.Val(sItem["DRDQGZ" + i]) - Conversion.Val(sItem["DRDHGZ" + i])) / Conversion.Val(sItem["DRDQGZ" + i]) * 100, 1).ToString();
                                larray.Add(Conversion.Val(sItem["DRZLSSL" + i]));
                            }
                            sItem["DRZLSSL"] = Math.Round(larray.Average(), 1).ToString();
                        }
                        #endregion

                        #region 抗压强度损失率

                        if (!string.IsNullOrEmpty(sItem["DRSYZCD1"]))
                        {
                            List<double> syarray = new List<double>();
                            List<double> dbarray = new List<double>();
                            for (int i = 1; i < 6; i++)
                            {
                                sItem["DRSYZKYQD" + i] = Math.Round(Conversion.Val(sItem["DRSYZHZ" + i]) * 1000 / (Conversion.Val(sItem["DRSYZCD" + i]) * Conversion.Val(sItem["DRSYZKD" + i])), 1).ToString();
                                sItem["DRDBZKYQD" + i] = Math.Round(Conversion.Val(sItem["DRDBZHZ" + i]) * 1000 / (Conversion.Val(sItem["DRDBZCD" + i]) * Conversion.Val(sItem["DRDBZKD" + i])), 1).ToString();
                                syarray.Add(Conversion.Val(sItem["DRSYZKYQD" + i]));
                                dbarray.Add(Conversion.Val(sItem["DRDBZKYQD" + i]));
                            }
                            sItem["DRSYZKYQD"] = Math.Round(Conversion.Val(syarray.Average()), 1).ToString();
                            sItem["DRDBZKYQD"] = Math.Round(Conversion.Val(dbarray.Average()), 1).ToString();
                            sItem["DRQDSSL"] = Math.Round((Conversion.Val(sItem["DRDBZKYQD"]) - Conversion.Val(sItem["DRSYZKYQD"])) / Conversion.Val(sItem["DRDBZKYQD"]) * 100, 0).ToString();
                        }
                        if (IsQualified(drzlsslyq, sItem["DRZLSSL"], false) == "合格" && IsQualified(drkyqdsslyq, sItem["DRQDSSL"], false) == "合格")
                        {
                            sItem["DRPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["DRPD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        sItem["DRYQ"] = "质量损失" + drzlsslyq + ",强度损失" + drkyqdsslyq;
                        #endregion
                    }
                    else
                    {
                        sItem["DRPD"] = "----";
                        sItem["DRYQ"] = "----";
                        sItem["DRQDSSL"] = "----";
                    }
                    #endregion

                    #region 抗折强度
                    if (jcxm.Contains("、抗折强度、"))
                    {
                        jcxmCur = "抗折强度";
                        if (!string.IsNullOrEmpty(sItem["KZQDKD1"]))
                        {
                            List<double> kzqdArray = new List<double>();
                            for (int i = 1; i < 4; i++)
                            {
                                sItem["W_KZQD" + i] = Math.Round((Conversion.Val(sItem["KZQDPHHZ" + i]) * 1000 * Conversion.Val(sItem["KZQDJJ" + i])) / (Conversion.Val(sItem["KZQDKD" + i]) * Conversion.Val(sItem["KZQDGD" + i]) * Conversion.Val(sItem["KZQDGD" + i])), 1).ToString("0.0");
                                kzqdArray.Add(Conversion.Val(sItem["W_KZQD" + i]));
                            }
                            sItem["W_KZQD"] = kzqdArray.Average().ToString();
                        }
                        MItem[0]["G_KZQD"] = "≥" + Conversion.Val(sItem["G_KZQD"]).ToString();
                        if (IsQualified(MItem[0]["G_KZQD"], sItem["W_KZQD"], false) == "合格")
                        {
                            MItem[0]["GH_KZQD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            MItem[0]["GH_KZQD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        MItem[0]["GH_KZQD"] = "----";
                        MItem[0]["G_KZQD"] = "----";
                        sItem["W_KZQD"] = "----";
                    }
                    #endregion

                    #region 传热系数
                    if (jcxm.Contains("、传热系数、"))
                    {
                        jcxmCur = "传热系数";
                        sItem["G_CRXS"] = "≤" + sItem["G_CRXS"].Trim().Replace("≤", "");
                        if (IsQualified(sItem["G_CRXS"], sItem["KZ"], false) == "合格")
                        {
                            sItem["HG_CRXS"] = "合格";
                        }
                        else
                        {
                            sItem["HG_CRXS"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["HG_CRXS"] = "----";
                        sItem["G_CRXS"] = "----";
                        sItem["KZ"] = "----";
                    }
                    #endregion

                    if (sItem["GMDPD"].Trim() == "不合格" || sItem["HSLPD"] == "不合格" || sItem["QDPD"].Trim() == "不合格" || sItem["WCPD"].Trim() == "不合格" || sItem["DRXSPD"] == "不合格" || sItem["LLSSLPD"] == "不合格" || sItem["DHQDPD"] == "不合格" || sItem["DRPD"] == "不合格" || sItem["HG_CRXS"] == "不合格")
                    {
                        sItem["JCJG"] = "不合格";
                        if (sItem["GMDPD"].Trim() == "合格" || sItem["QDPD"].Trim() == "合格" || sItem["WCPD"].Trim() == "合格" || sItem["DRXSPD"] == "合格" || sItem["LLSSLPD"] == "合格" || sItem["DHQDPD"] == "合格" || sItem["DRPD"] == "合格")
                        {
                            mFlag_Hg = true;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                        sItem["JCJG"] = "合格";
                    mAllHg = (mAllHg && sItem["JCJG"] == "合格");

                }
                #endregion
            }

            if (mAllHg && mjcjg != "----")
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }

            #endregion
            /************************ 代码结束 *********************/

        }
    }
}