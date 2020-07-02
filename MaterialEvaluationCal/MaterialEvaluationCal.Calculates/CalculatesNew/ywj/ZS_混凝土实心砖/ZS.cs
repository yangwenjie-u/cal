using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZS : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_ZS_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_ZS"];

            if (!data.ContainsKey("M_ZS"))
            {
                data["M_ZS"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_ZS"];

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
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            bool sign = false;
            double md1, md2, md, sum, Gs, pjmd = 0;
            int xd = 0;
            List<double> nArr = new List<double>();

            #region 跳转

            //Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            //{
            //    mbhggs = 0;
            //    jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
            //    mFlag_Hg = true;
            //    sign = true;
            //    if (jcxm.Contains("、抗压、"))
            //    {
            //        sign = true;
            //        for (xd = 1; xd < 11; xd++)
            //        {
            //            sign = IsNumeric(sItem["KYQD" + xd]) ? sign : false;
            //            if (!sign)
            //            {
            //                break;
            //            }
            //        }
            //        if (!sign)
            //        {
            //            return false;
            //        }

            //        sItem["QDYQ"] = "抗压强度平均值需" + Double.Parse(MItem[0]["G_PJZ"]).ToString("0") + "MPa,单块最小强度值需" + Double.Parse(MItem[0]["G_MIN"]).ToString() + "MPa。";
            //        sum = 0;
            //        nArr.Clear();
            //        for (xd = 0; xd < 10; xd++)
            //        {
            //            md = Double.Parse(sItem["KYQD" + xd]);
            //            nArr[xd] = md;
            //            sum += md;
            //        }
            //        pjmd = Math.Round(sum / 10, 2);
            //        sItem["KYPJ"] = pjmd.ToString("0.00");

            //        nArr.Sort();
            //        sItem["DKZX"] = Math.Round(nArr[0], 1).ToString("0.0");


            //        sign = IsQualified(Double.Parse(MItem[0]["G_PJZ"]).ToString("0"), sItem["KYPJ"]) == "合格" ? sign : false;
            //        sign = IsQualified(Double.Parse(MItem[0]["G_MIN"]).ToString("0.0"), sItem["DKZX"]) == "合格" ? sign : false;
            //        sItem["QDPD"] = sign ? "合格" : "不合格";
            //        if (sItem["QDPD"] == "不合格")
            //        {
            //            mbhggs = mbhggs + 1;
            //            mAllHg = false;
            //            mFlag_Hg = true;
            //        }
            //        else
            //        {
            //            mFlag_Hg = true;
            //        }

            //    }
            //    else
            //    {
            //        sItem["KYPJ"] = "----";
            //        sItem["QDPD"] = "----";
            //        sItem["QDMIN"] = "----";
            //        sItem["QDYQ"] = "----";
            //        for (xd = 1; xd < 11; xd++)
            //        {
            //            sItem["KYQD" + xd] = "----";
            //        }
            //    }

            //    if (jcxm.Contains("、干密度、"))
            //    {
            //        sign = true;
            //        for (xd = 1; xd < 4; xd++)
            //        {
            //            sign = IsNumeric(sItem["GMD" + xd]) ? sign : false;
            //            if (!sign)
            //            {
            //                break;
            //            }
            //        }
            //        if (!sign)
            //        {
            //            return false;
            //        }

            //        sign = IsQualified(Double.Parse(MItem[0]["G_GMD"]).ToString("0"), sItem["GMDPJ"]) == "合格" ? sign : false;
            //        sItem["GMDPD"] = sign ? "合格" : "不合格";
            //        if (sItem["GMDPD"] == "不合格")
            //        {
            //            mbhggs = mbhggs + 1;
            //            mAllHg = false;
            //            mFlag_Hg = true;
            //        }
            //        else
            //        {
            //            mFlag_Hg = true;
            //        }
            //    }
            //    else
            //    {
            //        sItem["GMDPJ"] = "----";
            //        sItem["GMDPD"] = "----";
            //        sItem["G_GMD"] = "----";
            //        for (xd = 1; xd < 4; xd++)
            //        {
            //            sItem["GMD" + xd] = "----";
            //        }
            //    }


            //    if (jcxm.Contains("、吸水率、"))
            //    {
            //        sign = true;
            //        for (xd = 1; xd < 4; xd++)
            //        {
            //            sign = IsNumeric(sItem["HXSW2_" + xd]) ? sign : false;
            //            if (!sign)
            //            {
            //                break;
            //            }
            //        }
            //        if (!sign)
            //        {
            //            return false;
            //        }

            //        sign = IsQualified(Double.Parse(sItem["XSLYQ"]).ToString("0"), sItem["HXSW2"]) == "合格" ? sign : false;
            //        sItem["XSLPD"] = sign ? "合格" : "不合格";
            //        if (sItem["XSLPD"] == "不合格")
            //        {
            //            mbhggs = mbhggs + 1;
            //            mAllHg = false;
            //            mFlag_Hg = true;
            //        }
            //        else
            //        {
            //            mFlag_Hg = true;
            //        }
            //    }
            //    else
            //    {
            //        sItem["HXSW2"] = "----";
            //        sItem["XSLPD"] = "----";
            //        sItem["XSLYQ"] = "----";
            //        for (xd = 1; xd < 4; xd++)
            //        {
            //            sItem["HXSW2_" + xd] = "----";
            //        }
            //    }

            //    if (jcxm.Contains("、相对含水率、"))
            //    {
            //        sign = true;
            //        sign = IsNumeric(sItem["HXSW1"]) ? sign : false;
            //        sign = IsNumeric(sItem["HXSW"]) ? sign : false;

            //        if (!sign)
            //        {
            //            return false;
            //        }

            //        sign = IsQualified(Double.Parse(sItem["XDHSLYQ"]).ToString("0"), sItem["HXSW2"]) == "合格" ? sign : false;
            //        sItem["XDHSLPD"] = sign ? "合格" : "不合格";
            //        if (sItem["XDHSLPD"] == "不合格")
            //        {
            //            mbhggs = mbhggs + 1;
            //            mAllHg = false;
            //            mFlag_Hg = true;
            //        }
            //        else
            //        {
            //            mFlag_Hg = true;
            //        }
            //    }
            //    else
            //    {
            //        sItem["HXSW1"] = "----";
            //        sItem["XDXSLPD"] = "----";
            //        sItem["XDXSLYQ"] = "----";
            //        sItem["HXSW"] = "----";
            //    }

            //    if (jcxm.Contains("、干缩率、"))
            //    {
            //        sign = true;
            //        for (xd = 1; xd < 4; xd++)
            //        {
            //            sign = IsNumeric(sItem["GSL" + xd]) ? sign : false;
            //            if (!sign)
            //            {
            //                break;
            //            }
            //        }

            //        if (!sign)
            //        {
            //            return false;
            //        }

            //        sign = IsQualified(Double.Parse(sItem["GSLYQ"]).ToString("0"), sItem["GSL"]) == "合格" ? sign : false;
            //        sItem["GSLPD"] = sign ? "合格" : "不合格";
            //        if (sItem["GSLPD"] == "不合格")
            //        {
            //            mbhggs = mbhggs + 1;
            //            mAllHg = false;
            //            mFlag_Hg = true;
            //        }
            //        else
            //        {
            //            mFlag_Hg = true;
            //        }
            //    }
            //    else
            //    {
            //        sItem["GSL"] = "----";
            //        sItem["GSLPD"] = "----";
            //        sItem["GSLYQ"] = "----";
            //        for (xd = 1; xd < 4; xd++)
            //        {
            //            sItem["GSL" + xd] = "----";
            //        }
            //    }

            //    if (mbhggs == 0)
            //    {
            //        jsbeizhu = "该组试件所检项目符合上述标准要求。";
            //        sItem["JCJG"] = "合格";
            //        if (mFlag_Bhg && mFlag_Hg)
            //        {
            //            jsbeizhu = "该组试件所检项目部分符合上述标准要求。";
            //        }
            //    }

            //    if (mbhggs > 0)
            //    {
            //        sItem["JCJG"] = "不合格";
            //        mAllHg = false;
            //        jsbeizhu = "该组试件不符合上述标准要求。";

            //    }

            //    return mAllHg;
            //};
            #endregion

            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mItemHg = true;

                Gs = extraDJ.Count;
                MItem[0]["G_GMD"] = "----";
                MItem[0]["G_MIN"] = "----";
                MItem[0]["G_PJZ"] = "----";
                sItem["XDHSLYQ"] = "----";
                sItem["GSLYQ"] = "----";
                sItem["XSLYQ"] = "----";
                for (int i = 0; i < Gs; i++)
                {
                    if (sItem["GMDDJ"].Trim() == extraDJ[i]["GMDDJ"])
                    {
                        MItem[0]["G_GMD"] = extraDJ[i]["G_GMD"];
                        sItem["XSLYQ"] = extraDJ[i]["G_XSL"];
                    }

                    if (sItem["SJDJ"].Trim() == extraDJ[i]["QDDJ"])
                    {
                        MItem[0]["G_MIN"] = extraDJ[i]["G_QDMIN"];
                        MItem[0]["G_PJZ"] = extraDJ[i]["G_QDPJ"];
                    }

                    if (extraDJ[i]["SYTJ"].Trim() == "干燥")
                    {
                        sItem["XDHSLYQ"] = extraDJ[i]["G_XDHSL"];
                        sItem["GSLYQ"] = extraDJ[i]["G_GZSSL"];
                    }
                }

                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    //mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
                    mbhggs = 0;
                    jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                    mFlag_Hg = true;
                    sign = true;

                    #region 抗压
                    if (jcxm.Contains("、抗压、"))
                    {
                        jcxmCur = "抗压";
                        if (MItem[0]["G_PJZ"] != "----" && MItem[0]["G_MIN"] != "----")
                        {
                            sItem["QDYQ"] = "抗压强度平均值需" + MItem[0]["G_PJZ"] + "MPa,单块最小强度值需" + MItem[0]["G_MIN"] + "MPa。";
                        }
                        else
                        {
                            sItem["QDYQ"] = "----";
                        }
                        if (sItem["QDYQ"] != "----")
                        {
                            string mlongStr = "";
                            List<double> listKYQD = new List<double>();
                            for (int i = 1; i <= 10; i++)
                            {
                                sItem["QCD" + i] = Round((GetSafeDouble(sItem["QCD" + i + "_1"]) + GetSafeDouble(sItem["QCD" + i + "_2"])) / 2, 0).ToString();
                                sItem["QKD" + i] = Round((GetSafeDouble(sItem["QKD" + i + "_1"]) + GetSafeDouble(sItem["QKD" + i + "_2"])) / 2, 0).ToString();
                                sItem["QMJ" + i] = (GetSafeDouble(sItem["QCD" + i]) * GetSafeDouble(sItem["QKD" + i])).ToString();
                                md1 = GetSafeDouble(sItem["KYHZ" + i]);
                                md2 = GetSafeDouble(sItem["QMJ" + i]);
                                md = Round(1000 * md1 / md2, 1);
                                sItem["KYQD" + i] = md.ToString("0.0");
                                listKYQD.Add(md);
                            }

                            listKYQD.Sort();
                            sItem["KYPJ"] = Round(listKYQD.Average(), 1).ToString("0.0");
                            sItem["DKZX"] = Round(listKYQD.Min(), 1).ToString("0.0");

                            if (IsQualified(MItem[0]["G_PJZ"], sItem["KYPJ"], false) == "合格" && IsQualified(MItem[0]["G_MIN"], sItem["DKZX"], false) == "合格")
                            {
                                sItem["QDPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["QDPD"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }
                        }
                    }
                    else
                    {
                        sItem["KYPJ"] = "----";
                        sItem["QDPD"] = "----";
                        sItem["QDMIN"] = "----";
                        sItem["QDYQ"] = "----";
                        for (xd = 1; xd < 11; xd++)
                        {
                            sItem["KYQD" + xd] = "----";
                        }
                    }
                    #endregion

                    #region 干密度
                    if (jcxm.Contains("、干密度、"))
                    {
                        jcxmCur = "干密度";
                        double mtj1 = 0, mtj2 = 0, mtj3 = 0;
                        sItem["CD1"] = Round((GetSafeDouble(sItem["CD1_1"]) + GetSafeDouble(sItem["CD1_2"])) / 2, 0).ToString();
                        sItem["KD1"] = Round((GetSafeDouble(sItem["KD1_1"]) + GetSafeDouble(sItem["KD1_2"])) / 2, 0).ToString();
                        sItem["GD1"] = Round((GetSafeDouble(sItem["GD1_1"]) + GetSafeDouble(sItem["GD1_2"])) / 2, 0).ToString();
                        mtj1 = Round(GetSafeDouble(sItem["CD1"]) / 1000 * GetSafeDouble(sItem["KD1"]) / 1000 * GetSafeDouble(sItem["GD1"]) / 1000, 3);

                        sItem["CD2"] = Round((GetSafeDouble(sItem["CD2_1"]) + GetSafeDouble(sItem["CD2_2"])) / 2, 0).ToString();
                        sItem["KD2"] = Round((GetSafeDouble(sItem["KD2_1"]) + GetSafeDouble(sItem["KD2_2"])) / 2, 0).ToString();
                        sItem["GD2"] = Round((GetSafeDouble(sItem["GD2_1"]) + GetSafeDouble(sItem["GD2_2"])) / 2, 0).ToString();
                        mtj2 = Round(GetSafeDouble(sItem["CD2"]) / 1000 * GetSafeDouble(sItem["KD2"]) / 1000 * GetSafeDouble(sItem["GD2"]) / 1000, 3);

                        sItem["CD3"] = Round((GetSafeDouble(sItem["CD3_1"]) + GetSafeDouble(sItem["CD3_2"])) / 2, 0).ToString();
                        sItem["KD3"] = Round((GetSafeDouble(sItem["KD3_1"]) + GetSafeDouble(sItem["KD3_2"])) / 2, 0).ToString();
                        sItem["GD3"] = Round((GetSafeDouble(sItem["GD3_1"]) + GetSafeDouble(sItem["GD3_2"])) / 2, 0).ToString();
                        mtj3 = Round(GetSafeDouble(sItem["CD3"]) / 1000 * GetSafeDouble(sItem["KD3"]) / 1000 * GetSafeDouble(sItem["GD3"]) / 1000, 3);

                        sItem["GMD1"] = (Round(GetSafeDouble(sItem["HGHZL1"]) / mtj1 / 10, 0) * 10).ToString();
                        sItem["GMD2"] = (Round(GetSafeDouble(sItem["HGHZL2"]) / mtj2 / 10, 0) * 10).ToString();
                        sItem["GMD3"] = (Round(GetSafeDouble(sItem["HGHZL3"]) / mtj3 / 10, 0) * 10).ToString();

                        sItem["GMDPJ"] = (Round((GetSafeDouble(sItem["GMD1"]) + GetSafeDouble(sItem["GMD2"]) + GetSafeDouble(sItem["GMD3"])) / 3 / 10, 0) * 10).ToString();

                        if (IsQualified(MItem[0]["G_GMD"],sItem["GMDPJ"],false) =="合格")
                        {
                            sItem["GMDPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["GMDPD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg =true;
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        sItem["GMDPJ"] = "----";
                        sItem["GMDPD"] = "----";
                        MItem[0]["G_GMD"] = "----";
                        for (xd = 1; xd < 4; xd++)
                        {
                            sItem["GMD" + xd] = "----";
                        }
                    }
                    #endregion

                    #region 吸水率
                    if (jcxm.Contains("、吸水率、"))
                    {
                        jcxmCur = "吸水率";
                        sItem["HXSW2_1"] = Round((GetSafeDouble(sItem["HXSM2_1"]) - GetSafeDouble(sItem["HXSM_1"])) / GetSafeDouble(sItem["HXSM_1"]) * 100, 2).ToString("0.00");
                        sItem["HXSW2_2"] = Round((GetSafeDouble(sItem["HXSM2_2"]) - GetSafeDouble(sItem["HXSM_2"])) / GetSafeDouble(sItem["HXSM_2"]) * 100, 2).ToString("0.00");
                        sItem["HXSW2_3"] = Round((GetSafeDouble(sItem["HXSM2_3"]) - GetSafeDouble(sItem["HXSM_3"])) / GetSafeDouble(sItem["HXSM_3"]) * 100, 2).ToString("0.00");

                        sItem["HXSW2"] = Round((GetSafeDouble(sItem["HXSW2_1"]) + GetSafeDouble(sItem["HXSW2_2"]) + GetSafeDouble(sItem["HXSW2_3"])) / 3, 1).ToString("0.0");

                        if (IsQualified(sItem["XSLYQ"],sItem["HXSW2"],false) == "合格")
                        {
                            sItem["XSLPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["XSLPD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        sItem["HXSW2"] = "----";
                        sItem["XSLPD"] = "----";
                        sItem["XSLYQ"] = "----";
                        for (xd = 1; xd < 4; xd++)
                        {
                            sItem["HXSW2_" + xd] = "----";
                        }
                    }
                    #endregion

                    #region 相对含水率
                    if (jcxm.Contains("、相对含水率、"))
                    {
                        jcxmCur = "相对含水率";
                        sItem["HXSW1_1"] = Round((GetSafeDouble(sItem["HXSM0_1"]) - GetSafeDouble(sItem["HXSM_1"])) / GetSafeDouble(sItem["HXSM_1"]) * 100, 2).ToString("0.00");
                        sItem["HXSW1_2"] = Round((GetSafeDouble(sItem["HXSM0_2"]) - GetSafeDouble(sItem["HXSM_2"])) / GetSafeDouble(sItem["HXSM_2"]) * 100, 2).ToString("0.00");
                        sItem["HXSW1_3"] = Round((GetSafeDouble(sItem["HXSM0_3"]) - GetSafeDouble(sItem["HXSM_3"])) / GetSafeDouble(sItem["HXSM_3"]) * 100, 2).ToString("0.00");

                        sItem["HXSW1"] = Round((GetSafeDouble(sItem["HXSW1_1"]) + GetSafeDouble(sItem["HXSW1_2"]) + GetSafeDouble(sItem["HXSW1_3"])) / 3, 1).ToString("0.0");
                        sItem["HXSW"] = Round(100 * (GetSafeDouble(sItem["HXSW1"]) / GetSafeDouble(sItem["HXSW2"])), 1).ToString("0.0");

                        if (IsQualified(sItem["XDHSLYQ"],sItem["HXSW"],false) == "合格")
                        {
                            sItem["XDHSLPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["XDHSLPD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        sItem["HXSW1"] = "----";
                        //sItem["XDXSLPD"] = "----";
                        //sItem["XDXSLYQ"] = "----";
                        sItem["XDHSLYQ"] = "----";
                        sItem["HXSW"] = "----";
                    }
                    #endregion

                    #region 干缩率
                    if (jcxm.Contains("、干缩率、"))
                    {
                        jcxmCur = "干缩率";
                        sItem["GSL1"] = Round((GetSafeDouble(sItem["GSLL_1"]) - GetSafeDouble(sItem["GSLL0_1"])) / GetSafeDouble(sItem["GSLL0_1"]) * 1000, 2).ToString("0.00");
                        sItem["GSL2"] = Round((GetSafeDouble(sItem["GSLL_2"]) - GetSafeDouble(sItem["GSLL0_2"])) / GetSafeDouble(sItem["GSLL0_2"]) * 1000, 2).ToString("0.00");
                        sItem["GSL3"] = Round((GetSafeDouble(sItem["GSLL_3"]) - GetSafeDouble(sItem["GSLL0_3"])) / GetSafeDouble(sItem["GSLL0_3"]) * 1000, 2).ToString("0.00");

                        sItem["GSL"] = Round((GetSafeDouble(sItem["GSL1"]) + GetSafeDouble(sItem["GSL2"]) + GetSafeDouble(sItem["GSL3"])) / 3, 2).ToString("0.00");

                        if (IsQualified(sItem["GSLYQ"],sItem["GSL"],false) == "合格")
                        {
                            sItem["GSLPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["GSLPD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        sItem["GSL"] = "----";
                        sItem["GSLPD"] = "----";
                        sItem["GSLYQ"] = "----";
                        for (xd = 1; xd < 4; xd++)
                        {
                            sItem["GSL" + xd] = "----";
                        }
                    }
                    #endregion

                    if (mbhggs == 0)
                    {
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        sItem["JCJG"] = "合格";
                        if (mFlag_Bhg && mFlag_Hg)
                        {
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        }
                    }

                    if (mbhggs > 0)
                    {
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";

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
                mjcjg = "不合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
        }
    }
}

