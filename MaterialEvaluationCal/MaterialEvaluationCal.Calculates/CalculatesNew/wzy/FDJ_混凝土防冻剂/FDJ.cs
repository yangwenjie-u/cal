using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class FDJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 参数定义
            string mcalBh;

            double zj1, zj2;
            int mbhggs;
            int mFsgs_qfqd, mFsgs_klqd, mFsgs_scl, mFsgs_lw;
            string mSjdjbh, mSjdj;
            double mQfqd, mKlqd, mScl, mLw;
            int vp, mCnt_FjHg, mCnt_FjHg1, mxlgs, mxwgs;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            bool mSFwc;
            bool mFlag_Hg, mFlag_Bhg;
            mSFwc = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_FDJ_DJ"];
            var MItem = data["M_FDJ"];
            var mitem = MItem[0];
            var SItem = data["S_FDJ"];
            string jsbeizhu = "";
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            mitem["JCJGMS"] = "";
            zj1 = 0;
            zj2 = 0;
            foreach (var sItem in SItem)
            {
                mSjdj = sItem["WJJMC"];
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                IDictionary<string, string> mrsDj_item = new Dictionary<string, string>();
                //if (sItem["JCXM"].Trim().Contains("强度比"))
                //    mrsDj_item = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()) && x["DJ"].Contains(sItem["DJ"].Trim()) && x["KYQDBWD"].Contains(sItem["KYGDWD"]));
                //else
                mrsDj_item = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()) && x["DJ"].Contains(sItem["DJ"].Trim()));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    mitem["G_XD"] = mrsDj_item["XD"];
                    mitem["G_MD"] = mrsDj_item["MD"];
                    mitem["G_MSL"] = mrsDj_item["MSL"];
                    mitem["G_JSL"] = mrsDj_item["JSL"];
                    mitem["G_GTHL"] = mrsDj_item["GTHL"];
                    mitem["G_CNSJC"] = mrsDj_item["CNSJC"];
                    mitem["G_ZNSJC"] = mrsDj_item["ZNSJC"];
                    mitem["G_TLD"] = mrsDj_item["TLD"];
                    mitem["G_HQLBHL"] = mrsDj_item["HQLBHL"];
                    mitem["G_HQL"] = mrsDj_item["HQL"];
                    mitem["G_KYQD1D"] = mrsDj_item["KYQDB1D"];
                    mitem["G_KYQD3D"] = mrsDj_item["KYQDB3D"];
                    mitem["G_KYQD7D"] = mrsDj_item["KYQDB7D"];
                    mitem["G_KYQD28D"] = mrsDj_item["KYQDB28D"];
                    mitem["G_XDNJX"] = mrsDj_item["XDNJX"];
                    mitem["G_PH"] = mrsDj_item["PH"];
                    mitem["G_LLZHL"] = mrsDj_item["LLZHL"];
                    mitem["G_LDD"] = mrsDj_item["LDD"];
                    mitem["G_STGDB"] = mrsDj_item["STGDB"];
                    mitem["G_DRQDB"] = mrsDj_item["DRQDSSLB"];
                    mitem["G_SSLB"] = mrsDj_item["SSLB28D"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_item["JSFF"]) ? "" : mrsDj_item["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                mbhggs = 0;
                mbhggs = 0;
                int xd;
                double md1, md2, sum, pjmd;
                //bool mFlag_Hg, mFlag_Bhg;
                mFlag_Hg = false;
                mFlag_Bhg = false;
                double[] narr;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    if (jcxm.Contains("、细度、"))
                    {
                        mitem["HG_XD"] = IsQualified(sItem["XDKZZ"], sItem["XD"]);
                        mitem["G_XD"] = sItem["XDKZZ"];
                        if (mitem["HG_XD"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        sItem["XD"] = "----";
                        mitem["HG_XD"] = "----";
                        mitem["G_XD"] = "----";
                    }
                    if (jcxm.Contains("、密度、"))
                    {
                        if (Conversion.Val(sItem["MDKZZ"]) > 1.1)
                            mitem["G_MD"] = Round((Conversion.Val(sItem["MDKZZ"]) - 0.03), 3).ToString("0.000") + "~" + Round((Conversion.Val(sItem["MDKZZ"]) + 0.03), 3).ToString("0.000");
                        else
                            mitem["G_MD"] = Round((Conversion.Val(sItem["MDKZZ"]) - 0.02), 3).ToString("0.000") + "~" + Round((Conversion.Val(sItem["MDKZZ"]) + 0.02), 3).ToString("0.000");
                        if (sItem["MDKZZ"] == "----")
                            mitem["HG_MD"] = "----";
                        else
                            mitem["HG_MD"] = IsQualified(mitem["G_MD"], sItem["MD"]);


                        if (mitem["HG_MD"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["MD"] = "----";
                        mitem["HG_MD"] = "----";
                        mitem["G_MD"] = "----";
                    }
                    if (jcxm.Contains("、固体含量、"))
                    {
                        if (Conversion.Val(sItem["HGLKZZ"]) >= 20)
                            mitem["G_GTHL"] = Round((Conversion.Val(sItem["HGLKZZ"]) * 0.95), 2).ToString("0.00") + "~" + Round((Conversion.Val(sItem["HGLKZZ"]) * 1.05), 2).ToString("0.00");
                        else
                            mitem["G_GTHL"] = Round((Conversion.Val(sItem["HGLKZZ"]) * 0.9), 2).ToString("0.00") + "~" + Round((Conversion.Val(sItem["HGLKZZ"]) * 1.1), 2).ToString("0.00");
                        if (sItem["HGLKZZ"] == "----")
                            mitem["HG_GTHL"] = "----";
                        else
                            mitem["HG_GTHL"] = IsQualified(mitem["G_GTHL"], sItem["GTHL"]);
                        if (mitem["HG_GTHL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["GTHL_1"] = "----";
                        sItem["GTHL_2"] = "----";
                        sItem["GTHL"] = "----";
                        mitem["G_GTHL"] = "----";
                        mitem["HG_GTHL"] = "----";
                    }
                    if (jcxm.Contains("、含水率、"))
                    {
                        if (Conversion.Val(sItem["HSLKZZ"]) >= 5)
                            mitem["G_HSL"] = Round(Conversion.Val(sItem["HSLKZZ"]) * 0.9, 2).ToString("0.00") + "~" + Round(Conversion.Val(sItem["HSLKZZ"]) * 1.1, 2).ToString("0.00");
                        else
                            mitem["G_HSL"] = Round(Conversion.Val(sItem["HSLKZZ"]) * 0.8, 2).ToString("0.00") + "~" + Round(Conversion.Val(sItem["HSLKZZ"]) * 1.2, 2).ToString("0.00");

                        if (sItem["HSLKZZ"] == "----")
                            mitem["HG_HSL"] = "----";
                        else
                            mitem["HG_HSL"] = IsQualified(mitem["G_HSL"], sItem["HSL"]);
                        if (mitem["HG_HSL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["HSL"] = "----";
                        mitem["G_HSL"] = "----";
                        mitem["HG_HSL"] = "----";
                    }
                    if (jcxm.Contains("、泌水率比、"))
                    {
                        mitem["HG_MSL"] = IsQualified(mitem["G_MSL"], sItem["MSLB"]);
                        if (mitem["HG_MSL"] == "不符合")
                            mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        mitem["G_MSL"] = "----";
                        sItem["MSLB"] = "-----";
                        mitem["HG_MSL"] = "----";
                    }
                    if (jcxm.Contains("、减水率、"))
                    {
                        mitem["HG_JSL"] = IsQualified(mitem["G_JSL"], sItem["PJJSL"]);
                        if (mitem["HG_JSL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_JSL"] = "----";
                        sItem["PJJSL"] = "-----";
                        mitem["HG_JSL"] = "----";
                    }
                    if (jcxm.Contains("、含气量、"))
                    {
                        mitem["HG_HQL"] = IsQualified(mitem["G_HQL"], sItem["PJHQL"]);
                        if (mitem["HG_HQL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_HQL"] = "----";
                        sItem["PJHQL"] = "---- - ";
                        mitem["HG_HQL"] = "----";
                    }
                    if (jcxm.Contains("、初凝时间差、"))
                    {
                        mitem["HG_CNSJC"] = IsQualified(mitem["G_CNSJC"], sItem["CNPJSJC"]);
                        if (mitem["HG_CNSJC"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_CNSJC"] = "----";
                        sItem["CNPJSJC"] = "-----";
                        mitem["HG_CNSJC"] = "----";
                    }
                    if (jcxm.Contains("、终凝时间差、"))
                    {
                        mitem["HG_ZNSJC"] = IsQualified(mitem["G_ZNSJC"], sItem["ZNPJSJC"]);
                        if (mitem["HG_ZNSJC"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_ZNSJC"] = "----";
                        sItem["ZNPJSJC"] = "-----";
                        mitem["HG_ZNSJC"] = "----";
                    }
                    if (jcxm.Contains("、R-7抗压强度比、") || jcxm.Contains("、抗压强度比、"))
                    {
                        if (!string.IsNullOrEmpty(sItem["QDB1"]))
                        {
                            mitem["HG_KYQD1D"] = IsQualified(mitem["G_KYQD1D"], sItem["QDB1"]);
                            if (mitem["HG_KYQD1D"] == "不符合")
                            {
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                                mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        mitem["G_KYQD1D"] = "----";
                        sItem["QDB1"] = "-----";
                        mitem["HG_KYQD1D"] = "----";
                    }
                    if (jcxm.Contains("、R28抗压强度比、") || jcxm.Contains("、抗压强度比、"))
                    {
                        if (!string.IsNullOrEmpty(sItem["QDB2"]))
                        {
                            mitem["HG_KYQD3D"] = IsQualified(mitem["G_KYQD3D"], sItem["QDB2"]);
                            if (mitem["HG_KYQD3D"] == "不符合")
                            {
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                                mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        mitem["G_KYQD3D"] = "----";
                        sItem["QDB2"] = "-----";
                        mitem["HG_KYQD3D"] = "----";
                    }
                    if (jcxm.Contains("、R-7+28抗压强度比、") || jcxm.Contains("、R－7＋28抗压强度比、") || jcxm.Contains("、抗压强度比、"))
                    {
                        if (!string.IsNullOrEmpty(sItem["QDB3"]))
                        {
                            mitem["HG_KYQD7D"] = IsQualified(mitem["G_KYQD7D"], sItem["QDB3"]);
                            if (mitem["HG_KYQD7D"] == "不符合")
                            {
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                                mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        mitem["G_KYQD7D"] = "----";
                        sItem["QDB3"] = "-----";
                        mitem["HG_KYQD7D"] = "----";
                    }
                    if (jcxm.Contains("、R-7+56抗压强度比、") || jcxm.Contains("、R－7＋56抗压强度比、") || jcxm.Contains("、抗压强度比、"))
                    {
                        if (!string.IsNullOrEmpty(sItem["QDB4"]))
                        {
                            mitem["HG_KYQD28D"] = IsQualified(mitem["G_KYQD28D"], sItem["QDB4"]);
                            if (mitem["HG_KYQD28D"] == "不符合")
                            {
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                                mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        mitem["G_KYQD28D"] = "----";
                        sItem["QDB4"] = "-----";
                        mitem["HG_KYQD28D"] = "----";
                    }
                    if (jcxm.Contains("、收缩率比、"))
                    {
                        mitem["HG_SSLB"] = IsQualified(mitem["G_SSLB"], sItem["SSLB"]);
                        if (mitem["HG_SSLB"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["SSLB"] = "----";
                        mitem["HG_SSLB"] = "----";
                        mitem["G_SSLB"] = "----";
                    }
                    if (jcxm.Contains("、50次冻融强度损失率比、"))
                    {
                        mitem["HG_DRQDB"] = IsQualified(mitem["G_DRQDB"], sItem["DRQDSSLB"]);
                        if (mitem["HG_DRQDB"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["DRQDSSLB"] = "----";
                        mitem["HG_DRQDB"] = "----";
                        mitem["G_DRQDB"] = "----";
                    }
                    if (jcxm.Contains("、渗透高度比、"))
                    {
                        mitem["HG_STGDB"] = IsQualified(mitem["G_STGDB"], sItem["STGDB"]);
                        if (mitem["HG_STGDB"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["STGDB"] = "----";
                        mitem["HG_STGDB"] = "----";
                        mitem["G_STGDB"] = "----";
                    }
                    if (jcxm.Contains("、水泥净浆流动度、"))
                    {
                        if (sItem["LDDKZZ"].Contains("≥"))
                            sItem["LDDKZZ"] = sItem["LDDKZZ"].Substring(sItem["LDDKZZ"].Length - sItem["LDDKZZ"].IndexOf("≥"));
                        else
                            mitem["G_LDD"] = "≥" + Round(Conversion.Val(sItem["LDDKZZ"]) * 0.95, 1).ToString();
                        if (sItem["LDDKZZ"] == "----")
                            mitem["HG_LDD"] = "----";
                        else
                            mitem["HG_LDD"] = IsQualified(mitem["G_LDD"], sItem["LDD"]);
                        if (mitem["HG_LDD"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["LDD"] = "----";
                        mitem["HG_LDD"] = "----";
                        mitem["G_LDD"] = "----";
                    }
                    if (jcxm.Contains("、氯离子含量、"))
                    {
                        if (sItem["LLZKZZ"] == "----")
                            mitem["HG_LLZHL"] = "----";
                        else
                            mitem["HG_LLZHL"] = IsQualified(sItem["LLZKZZ"], sItem["LLZHL"]);
                        mitem["G_LLZHL"] = sItem["LLZKZZ"];
                        if (mitem["HG_LLZHL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["LLZHL"] = "----";
                        mitem["HG_LLZHL"] = "----";
                    }
                    if (jcxm.Contains("、碱含量、"))
                    {
                        mitem["G_ZJL"] = sItem["ZJLKZZ"].Trim();
                        if (sItem["ZJLKZZ"] == "----")
                            mitem["HG_ZJL"] = "----";
                        else
                            mitem["HG_ZJL"] = IsQualified(mitem["G_ZJL"], sItem["ZJL"]);
                        if (mitem["HG_ZJL"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["G_ZJL"] = "----";
                        sItem["ZJL"] = "----";
                        mitem["HG_ZJL"] = "----";
                    }

                }
                else
                {
                    List<double> mtmpArray = new List<double>();
                    double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd;
                    #region 细度
                    if (jcxm.Contains("、细度、"))
                    {
                        if (sItem["XDM1_1"] != "" && sItem["XDM1_1"] != "----")
                        {
                            sItem["XD_1"] = Round((GetSafeDouble(sItem["XDM1_1"]) / GetSafeDouble(sItem["XDM0_1"])) * 100, 2).ToString();
                            sItem["XD_2"] = Round((GetSafeDouble(sItem["XDM1_2"]) / GetSafeDouble(sItem["XDM0_2"])) * 100, 2).ToString();
                        }
                        if (sItem["XD_1"] != "" && sItem["XD_1"] != "----")
                        {
                            sItem["XD"] = Round((GetSafeDouble(sItem["XD_1"]) + GetSafeDouble(sItem["XD_2"])) / 2, 2).ToString();
                        }
                        if (sItem["XDKZZ"] == "----")
                        {
                            sItem["HG_XD"] = "----";
                        }
                        else
                        {
                            sItem["HG_XD"] = IsQualified(sItem["XDKZZ"], sItem["XD"]);
                        }
                        MItem[0]["G_XD"] = sItem["XDKZZ"];
                        if (MItem[0]["HG_XD"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
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
                    #endregion
                    #region 密度
                    if (jcxm.Contains("、密度、"))
                    {
                        if (sItem["MDBJWJJ_1"] != "" && sItem["MDBJWJJ_1"] != "----")
                        {
                            sItem["MD_1"] = Round(0.9982 * GetSafeDouble(sItem["MDBJWJJ_1"]) - GetSafeDouble(sItem["MDRLBZ_1"]) / GetSafeDouble(sItem["MDTJ_1"]), 3).ToString();
                            sItem["MD_2"] = Round(0.9982 * GetSafeDouble(sItem["MDBJWJJ_2"]) - GetSafeDouble(sItem["MDRLBZ_2"]) / GetSafeDouble(sItem["MDTJ_2"]), 3).ToString();
                        }
                        if (sItem["MD_1"] != "" && sItem["MD_1"] != "----")
                        {
                            sItem["MD"] = Round((GetSafeDouble(sItem["MD_1"]) + GetSafeDouble(sItem["MD_2"])) / 2, 3).ToString();
                        }
                        if (GetSafeDouble(sItem["MDKZZ"]) > 1.1)
                        {
                            MItem[0]["G_MD"] = Round(GetSafeDouble(sItem["MDKZZ"]) - 0.03, 3) + "~" + Round((GetSafeDouble(sItem["MDKZZ"]) + 0.03), 3);
                        }
                        else
                        {
                            MItem[0]["G_MD"] = Round(GetSafeDouble(sItem["MDKZZ"]) - 0.02, 3) + "~" + Round((GetSafeDouble(sItem["MDKZZ"]) + 0.02), 3);
                        }
                        if (sItem["MDKZZ"] == "----")
                        {
                            MItem[0]["HG_MD"] = "----";
                        }
                        else
                        {
                            MItem[0]["HG_MD"] = IsQualified(MItem[0]["G_MD"], sItem["MD"]);
                        }
                        if (MItem[0]["HG_MD"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
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
                    #endregion
                    #region 固体含量
                    if (jcxm.Contains("、固体含量、"))
                    {
                        if (sItem["GTHLM2_1"] != "" && sItem["GTHLM2_1"] != "----")
                        {
                            sItem["GTHL_1"] = Round((GetSafeDouble(sItem["GTHLM2_1"]) - GetSafeDouble(sItem["GTHLM0_1"])) / (GetSafeDouble(sItem["GTHLM1_1"]) - GetSafeDouble(sItem["GTHLM0_1"]) * 100), 2).ToString();
                            sItem["GTHL_2"] = Round((GetSafeDouble(sItem["GTHLM2_2"]) - GetSafeDouble(sItem["GTHLM0_2"])) / (GetSafeDouble(sItem["GTHLM1_2"]) - GetSafeDouble(sItem["GTHLM0_2"]) * 100), 2).ToString();
                        }
                        if (sItem["GTHL_1"] == "" && sItem["GTHL_1"] == "----")
                        {
                            sItem["GTHL"] = Round((GetSafeDouble(sItem["GTHL_1"]) + GetSafeDouble(sItem["GTHL_2"])) / 2, 2).ToString();
                        }
                        if (GetSafeDouble(sItem["HGLKZZ"]) > 20)
                        {
                            MItem[0]["G_GTHL"] = Round(Round(GetSafeDouble(sItem["HGLKZZ"]), 2) * 0.95, 2) + "~" + Round(Round(GetSafeDouble(sItem["HGLKZZ"]), 2) * 1.05, 2);
                        }
                        else
                        {
                            MItem[0]["G_GTHL"] = Round(Round(GetSafeDouble(sItem["HGLKZZ"]), 2) * 0.9, 2) + "~" + Round(Round(GetSafeDouble(sItem["HGLKZZ"]), 2) * 1.1, 2);
                        }
                        if (sItem["HGLKZZ"] == "----")
                        {
                            MItem[0]["HG_GTHL"] = "----";
                        }
                        else
                        {
                            MItem[0]["HG_GTHL"] = IsQualified(MItem[0]["G_GTHL"], sItem["GTHL"]);
                        }
                        if (MItem[0]["HG_GTHL"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        sItem["GTHL_1"] = "----";
                        sItem["GTHL_2"] = "----";
                        sItem["GTHL"] = "----";
                        MItem[0]["G_GTHL"] = "----";
                        MItem[0]["HG_GTHL"] = "----";
                    }
                    #endregion
                    #region 含水率
                    if (jcxm.Contains("、含水率、"))
                    {
                        if (sItem["HSLM1_1"] != "" && sItem["HSLM1_1"] != "----")
                        {
                            sItem["HSL_1"] = Round((GetSafeDouble(sItem["HSLM1_1"]) - GetSafeDouble(sItem["HSLM2_1"])) / (GetSafeDouble(sItem["HSLM2_1"]) - GetSafeDouble(sItem["HSLM0_1"])) * 100, 2).ToString();
                            sItem["HSL_2"] = Round((GetSafeDouble(sItem["HSLM1_2"]) - GetSafeDouble(sItem["HSLM2_2"])) / (GetSafeDouble(sItem["HSLM2_2"]) - GetSafeDouble(sItem["HSLM0_2"])) * 100, 2).ToString();
                            sItem["HSL_3"] = Round((GetSafeDouble(sItem["HSLM1_3"]) - GetSafeDouble(sItem["HSLM2_3"])) / (GetSafeDouble(sItem["HSLM2_3"]) - GetSafeDouble(sItem["HSLM0_3"])) * 100, 2).ToString();
                        }
                        if (sItem["HSL_1"] != "" && sItem["HSL_1"] != "----")
                        {
                            sItem["HSL"] = Round((GetSafeDouble(sItem["HSL_1"]) + GetSafeDouble(sItem["HSL_2"]) + GetSafeDouble(sItem["HSL_3"])) / 3, 1).ToString();
                        }
                        if (GetSafeDouble(sItem["HSLKZZ"]) >= 5)
                        {
                            MItem[0]["G_HSL"] = Round(GetSafeDouble(sItem["HSLKZZ"]) * 0.9, 2) + "~" + Round(GetSafeDouble(sItem["HSLKZZ"]) * 1.1, 2);
                        }
                        else
                        {
                            MItem[0]["G_HSL"] = Round(GetSafeDouble(sItem["HSLKZZ"]) * 0.9, 2) + "~" + Round(GetSafeDouble(sItem["HSLKZZ"]) * 1.1, 2);
                        }
                        if (sItem["HSLKZZ"] == "----")
                        {
                            MItem[0]["HG_HSL"] = "----";
                        }
                        else
                        {
                            MItem[0]["HG_HSL"] = IsQualified(MItem[0]["G_HSL"], sItem["HSL"]);
                        }
                        if (MItem[0]["HG_HSL"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        sItem["HSL_1"] = "----";
                        sItem["HSL_2"] = "----";
                        sItem["HSL_3"] = "----";
                        sItem["HSL"] = "----";
                        MItem[0]["G_HSL"] = "----";
                        MItem[0]["HG_HSL"] = "----";
                    }
                    #endregion
                    #region 泌水率
                    if (jcxm.Contains("、泌水率比、"))
                    {
                        if (sItem["PBSN1"] != "" && sItem["PBSN1"] != "----")
                        {
                            sItem["JPHWZL_1"] = Round(GetSafeDouble(sItem["PBSN1"]) + GetSafeDouble(sItem["PBS1"]) + GetSafeDouble(sItem["PBSA1"]) + GetSafeDouble(sItem["PBSZ1"]), 1).ToString();
                            sItem["JPHWZL_2"] = Round(GetSafeDouble(sItem["PBSN2"]) + GetSafeDouble(sItem["PBS2"]) + GetSafeDouble(sItem["PBSA2"]) + GetSafeDouble(sItem["PBSZ2"]), 1).ToString();
                            sItem["JPHWZL_3"] = Round(GetSafeDouble(sItem["PBSN3"]) + GetSafeDouble(sItem["PBS3"]) + GetSafeDouble(sItem["PBSA3"]) + GetSafeDouble(sItem["PBSZ3"]), 1).ToString();

                            sItem["MJBYS_1"] = (sItem["PBS1"]);
                            sItem["MJBYS_2"] = (sItem["PBS2"]);
                            sItem["MJBYS_3"] = (sItem["PBS3"]);
                        }
                        if (sItem["JMSZL_1"] != "" && sItem["JMSZL_1"] != "----")
                        {
                            sItem["JMSL_1"] = Round((GetSafeDouble(sItem["JMSZL_1"]) / (GetSafeDouble(sItem["MJBYS_1"]) / GetSafeDouble(sItem["JPHWZL_1"])) / GetSafeDouble(sItem["JSYZL_1"]) * 100), 2).ToString();
                            sItem["JMSL_2"] = Round((GetSafeDouble(sItem["JMSZL_2"]) / (GetSafeDouble(sItem["MJBYS_2"]) / GetSafeDouble(sItem["JPHWZL_2"])) / GetSafeDouble(sItem["JSYZL_2"]) * 100), 2).ToString();
                            sItem["JMSL_3"] = Round((GetSafeDouble(sItem["JMSZL_3"]) / (GetSafeDouble(sItem["MJBYS_3"]) / GetSafeDouble(sItem["JPHWZL_3"])) / GetSafeDouble(sItem["JSYZL_3"]) * 100), 2).ToString();
                        }
                        if (sItem["JMSL_1"] != "" && sItem["JMSL_1"] != "----")
                        {
                            string mlongStr = sItem["JMSL_1"] + "," + sItem["JMSL_2"] + "," + sItem["JMSL_3"];
                            string[] str = mlongStr.Split(',');
                            foreach (string s in str)
                            {
                                mtmpArray.Add(GetSafeDouble(s));
                            }
                            mtmpArray.Sort();
                            mMaxKyqd = mtmpArray[2];
                            mMinKyqd = mtmpArray[0];
                            mMidKyqd = mtmpArray[1];
                            mAvgKyqd = mtmpArray.Average();

                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                MItem[0]["HG_JSL"] = "重做";
                                sItem["PJJSL"] = "重做";
                            }
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["PJJSL"] = Round(mMidKyqd, 0).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["PJJSL"] = Round(mMidKyqd, 0).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["PJJSL"] = Round(mAvgKyqd, 1).ToString();
                            }
                        }

                        if (sItem["SPBSN1"] != "" && sItem["SPBSN1"] != "----")
                        {
                            sItem["SPHWZL_1"] = Round(GetSafeDouble(sItem["SPBSN1"]) + GetSafeDouble(sItem["SPBS1"]) + GetSafeDouble(sItem["SPBSA1"]) + GetSafeDouble(sItem["SPBSZ1"]) + GetSafeDouble(sItem["SPBWJJ11"]) + GetSafeDouble(sItem["SPBWJJ21"]), 1).ToString();
                            sItem["SPHWZL_2"] = Round(GetSafeDouble(sItem["SPBSN2"]) + GetSafeDouble(sItem["SPBS2"]) + GetSafeDouble(sItem["SPBSA2"]) + GetSafeDouble(sItem["SPBSZ2"]) + GetSafeDouble(sItem["SPBWJJ12"]) + GetSafeDouble(sItem["SPBWJJ22"]), 1).ToString();
                            sItem["SPHWZL_3"] = Round(GetSafeDouble(sItem["SPBSN3"]) + GetSafeDouble(sItem["SPBS3"]) + GetSafeDouble(sItem["SPBSA3"]) + GetSafeDouble(sItem["SPBSZ3"]) + GetSafeDouble(sItem["SPBWJJ13"]) + GetSafeDouble(sItem["SPBWJJ23"]), 1).ToString();
                            sItem["MSBYS_1"] = sItem["SPBS1"];
                            sItem["MSBYS_2"] = sItem["SPBS2"];
                            sItem["MSBYS_3"] = sItem["SPBS3"];
                        }
                        if (sItem["SMSZL_1"] != "" && sItem["SMSZL_1"] != "----")
                        {
                            sItem["SMSL_1"] = Round(GetSafeDouble(sItem["SMSZL_1"]) / (GetSafeDouble(sItem["MSBYS_1"]) / GetSafeDouble(sItem["SPHWZL_1"])) / GetSafeDouble(sItem["SSYZL_1"]) * 100, 2).ToString();
                            sItem["SMSL_2"] = Round(GetSafeDouble(sItem["SMSZL_2"]) / (GetSafeDouble(sItem["MSBYS_2"]) / GetSafeDouble(sItem["SPHWZL_2"])) / GetSafeDouble(sItem["SSYZL_2"]) * 100, 2).ToString();
                            sItem["SMSL_3"] = Round(GetSafeDouble(sItem["SMSZL_3"]) / (GetSafeDouble(sItem["MSBYS_3"]) / GetSafeDouble(sItem["SPHWZL_3"])) / GetSafeDouble(sItem["SSYZL_3"]) * 100, 2).ToString();
                        }
                        if (!string.IsNullOrEmpty(sItem["SMSL_1"]) && sItem["SMSL_1"] != "----")
                        {
                            string mlongStr = sItem["SMSL_1"] + "," + sItem["SMSL_2"] + "," + sItem["SMSL_3"];
                            string[] str = mlongStr.Split(',');
                            foreach (string s in str)
                            {
                                mtmpArray.Add(GetSafeDouble(s));
                            }
                            mtmpArray.Sort();
                            mMaxKyqd = mtmpArray[2];
                            mMinKyqd = mtmpArray[0];
                            mMidKyqd = mtmpArray[1];
                            mAvgKyqd = mtmpArray.Average();
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                MItem[0]["HG_MSL"] = "重做";
                                sItem["SPJMSL"] = "重做";
                            }
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["SPJMSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["SPJMSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["SPJMSL"] = Round(mAvgKyqd, 1).ToString();
                            }
                        }

                        if (sItem["JPJMSL"] == "重做" || sItem["SPJMSL"] == "重做")
                        {
                            mAllHg = false;
                            if (sItem["JPJMSL"] == "重做" && sItem["SPJMSL"] == "重做")
                            {
                                sItem["HG_MSL"] = "基准受检重做";
                            }
                            else
                            {
                                if (MItem[0]["JPJMSL"] == "重做")
                                {
                                    MItem[0]["HG_MSL"] = "基准重做";
                                }
                                else
                                {
                                    MItem[0]["HG_MSL"] = "受检重做";
                                }
                            }
                        }
                        else
                        {
                            if (sItem["SPJMSL"] != "" && sItem["SPJMSL"] != "----" && GetSafeDouble(sItem["SPJMSL"]) != 0)
                            {
                                sItem["MSLB"] = Round((GetSafeDouble(sItem["SPJMSL"]) / GetSafeDouble(sItem["JPJMSL"])) * 100, 0).ToString();
                            }
                            MItem[0]["HG_MSL"] = IsQualified(MItem[0]["G_MSL"], sItem["MSLB"], true);
                        }
                        if (MItem[0]["HG_MSL"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        MItem[0]["G_MSL"] = "----";
                        sItem["MSLB"] = "----";
                        MItem[0]["HG_MSL"] = "----";
                    }
                    #endregion
                    #region 减水率
                    if (jcxm.Contains("、减水率、"))
                    {
                        if (Conversion.Val(sItem["SYBHL"]) > 0)
                        {
                            sItem["JJDYS_1"] = Round((GetSafeDouble(sItem["PBS1"]) / GetSafeDouble(sItem["SYBHL"])) * 1000, 2).ToString();
                            sItem["JJDYS_2"] = Round((GetSafeDouble(sItem["PBS2"]) / GetSafeDouble(sItem["SYBHL"])) * 1000, 2).ToString();
                            sItem["JJDYS_3"] = Round((GetSafeDouble(sItem["PBS3"]) / GetSafeDouble(sItem["SYBHL"])) * 1000, 2).ToString();
                            sItem["JSDYS_1"] = Round((GetSafeDouble(sItem["SPBS1"]) / GetSafeDouble(sItem["SYBHL"])) * 1000, 2).ToString();
                            sItem["JSDYS_2"] = Round((GetSafeDouble(sItem["SPBS2"]) / GetSafeDouble(sItem["SYBHL"])) * 1000, 2).ToString();
                            sItem["JSDYS_3"] = Round((GetSafeDouble(sItem["SPBS3"]) / GetSafeDouble(sItem["SYBHL"])) * 1000, 2).ToString();
                        }
                        if (sItem["JJDYS_1"] != "" && sItem["JJDYS_1"] != "----")
                        {
                            sItem["JSL_1"] = Round((GetSafeDouble(sItem["JJDYS_1"]) - GetSafeDouble(sItem["JSDYS_1"])) / GetSafeDouble(sItem["JJDYS_1"]) * 100, 1).ToString();
                            sItem["JSL_2"] = Round((GetSafeDouble(sItem["JJDYS_2"]) - GetSafeDouble(sItem["JSDYS_2"])) / GetSafeDouble(sItem["JJDYS_2"]) * 100, 1).ToString();
                            sItem["JSL_3"] = Round((GetSafeDouble(sItem["JJDYS_3"]) - GetSafeDouble(sItem["JSDYS_3"])) / GetSafeDouble(sItem["JJDYS_3"]) * 100, 1).ToString();
                        }
                        if (sItem["JSL_1"] != "" && sItem["JSL_1"] != "----")
                        {
                            string mlongStr = sItem["JSL_1"] + "," + sItem["JSL_2"] + "," + sItem["JSL_3"];
                            string[] str = mlongStr.Split(',');
                            foreach (string s in str)
                            {
                                mtmpArray.Add(GetSafeDouble(s));
                            }
                            mtmpArray.Sort();
                            mMaxKyqd = mtmpArray[2];
                            mMinKyqd = mtmpArray[0];
                            mMidKyqd = mtmpArray[1];
                            mAvgKyqd = mtmpArray.Average();

                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                MItem[0]["HG_JSL"] = "重做";
                                sItem["PJJSL"] = "重做";
                            }
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["PJJSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["PJJSL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["PJJSL"] = Round(mAvgKyqd, 1).ToString();
                            }
                        }
                        if (sItem["PJJSL"] == "重做")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                            MItem[0]["HG_JSL"] = IsQualified(MItem[0]["G_JSL"], sItem["PJJSL"], true);
                        }
                        if (MItem[0]["HG_JSL"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        MItem[0]["G_JSL"] = "----";
                        sItem["PJJSL"] = "----";
                        MItem[0]["HG_JSL"] = "----";
                    }
                    #endregion
                    #region 含气量
                    if (jcxm.Contains("、含气量、"))
                    {
                        if (sItem["BHWHQL_1"] != "" && sItem["BHWHQL_1"] != "----")
                        {
                            sItem["HQL_1"] = (GetSafeDouble(sItem["BHWHQL_1"]) - GetSafeDouble(sItem["SSHQL_1"])).ToString();
                            sItem["HQL_2"] = (GetSafeDouble(sItem["BHWHQL_2"]) - GetSafeDouble(sItem["SSHQL_2"])).ToString();
                            sItem["HQL_3"] = (GetSafeDouble(sItem["BHWHQL_3"]) - GetSafeDouble(sItem["SSHQL_3"])).ToString();
                        }
                        if (sItem["HQL_1"] != "" && sItem["HQL_1"] != "----")
                        {
                            string mlongStr = sItem["HQL_1"] + "," + sItem["HQL_2"] + "," + sItem["HQL_3"];
                            string[] str = mlongStr.Split(',');
                            foreach (string s in str)
                            {
                                mtmpArray.Add(GetSafeDouble(s));
                            }
                            mtmpArray.Sort();
                            mMaxKyqd = mtmpArray[2];
                            mMinKyqd = mtmpArray[0];
                            mMidKyqd = mtmpArray[1];
                            mAvgKyqd = mtmpArray.Average();
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                MItem[0]["HG_HQL"] = "重做";
                                sItem["PJHQL"] = "重做";
                            }
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["PJHQL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["PJHQL"] = Round(mMidKyqd, 1).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["PJHQL"] = Round(mAvgKyqd, 1).ToString();
                            }
                        }
                        if (sItem["PJHQL"] == "重做")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                            MItem[0]["HG_HQL"] = IsQualified(MItem[0]["G_HQL"], sItem["PJHQL"]);
                        }
                        if (MItem[0]["HG_HQL"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        MItem[0]["G_HQL"] = "----";
                        sItem["PJHQL"] = "----";
                        MItem[0]["HG_HQL"] = "----";
                    }
                    #endregion

                    #region 初凝时间差
                    if (jcxm.Contains("、初凝时间差、"))
                    {
                        if (sItem["CNSJT_1"] != "" && sItem["CNSJT_1"] != "----")
                        {
                            sItem["CNSJC_1"] = (GetSafeDouble(sItem["CNSJT_1"]) - GetSafeDouble(sItem["CNJZT_1"])).ToString();
                            sItem["CNSJC_2"] = (GetSafeDouble(sItem["CNSJT_2"]) - GetSafeDouble(sItem["CNJZT_2"])).ToString();
                            sItem["CNSJC_3"] = (GetSafeDouble(sItem["CNSJT_3"]) - GetSafeDouble(sItem["CNJZT_3"])).ToString();
                        }
                        if (sItem["CNSJC_1"] != "" && sItem["CNSJC_1"] != "----")
                        {
                            string mlongStr = sItem["CNSJC_1"] + "," + sItem["CNSJC_2"] + "," + sItem["CNSJC_3"];
                            string[] str = mlongStr.Split(',');
                            foreach (string s in str)
                            {
                                mtmpArray.Add(GetSafeDouble(s));
                            }
                            mtmpArray.Sort();
                            mMaxKyqd = mtmpArray[2];
                            mMinKyqd = mtmpArray[0];
                            mMidKyqd = mtmpArray[1];
                            mAvgKyqd = mtmpArray.Average();
                            if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) > 30)
                            {
                                MItem[0]["HG_CNSJC"] = "重做";
                                sItem["CNPJSJC"] = "重做";
                            }
                            if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) <= 30)
                            {
                                sItem["CNPJSJC"] = Round(mMidKyqd, 0).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) > 30)
                            {
                                sItem["CNPJSJC"] = Round(mMidKyqd, 0).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) <= 30)
                            {
                                sItem["CNPJSJC"] = Round(mAvgKyqd, 0).ToString();
                            }
                        }
                        if (sItem["CNPJSJC"] == "重做")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                            MItem[0]["HG_CNSJC"] = IsQualified(MItem[0]["G_CNSJC"], sItem["CNPJSJC"]);
                        }
                        if (MItem[0]["HG_CNSJC"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        MItem[0]["G_CNSJC"] = "----";
                        sItem["CNPJSJC"] = "----";
                        MItem[0]["HG_CNSJC"] = "----";
                    }
                    #endregion
                    #region 终凝时间差
                    if (jcxm.Contains("、终凝时间差、"))
                    {
                        if (sItem["ZNSJT_1"] != "" && sItem["ZNSJT_1"] != "----")
                        {
                            sItem["ZNSJC_1"] = (GetSafeDouble(sItem["ZNSJT_1"]) - GetSafeDouble(sItem["ZNJZT_1"])).ToString();
                            sItem["ZNSJC_2"] = (GetSafeDouble(sItem["ZNSJT_2"]) - GetSafeDouble(sItem["ZNSJT_2"])).ToString();
                            sItem["ZNSJC_3"] = (GetSafeDouble(sItem["ZNSJT_3"]) - GetSafeDouble(sItem["ZNSJT_3"])).ToString();
                        }
                        if (sItem["ZNSJC_1"] != "" && sItem["ZNSJC_1"] != "----")
                        {
                            string mlongStr = sItem["ZNSJC_1"] + "," + sItem["ZNSJC_2"] + "," + sItem["ZNSJC_3"];
                            string[] str = mlongStr.Split(',');
                            foreach (string s in str)
                            {
                                mtmpArray.Add(GetSafeDouble(s));
                            }
                            mtmpArray.Sort();
                            mMaxKyqd = mtmpArray[2];
                            mMinKyqd = mtmpArray[0];
                            mMidKyqd = mtmpArray[1];
                            mAvgKyqd = mtmpArray.Average();
                            if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) > 30)
                            {
                                MItem[0]["HG_ZNSJC"] = "重做";
                                sItem["ZNPJSJC"] = "重做";
                            }
                            if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) <= 30)
                            {
                                sItem["ZNPJSJC"] = Round(mMidKyqd, 0).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) > 30)
                            {
                                sItem["ZNPJSJC"] = Round(mMidKyqd, 0).ToString();
                            }
                            if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) <= 30)
                            {
                                sItem["ZNPJSJC"] = Round(mAvgKyqd, 0).ToString();
                            }
                        }
                        if (sItem["ZNPJSJC"] == "重做")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                            MItem[0]["HG_ZNSJC"] = IsQualified(MItem[0]["G_ZNSJC"], sItem["ZNPJSJC"]);
                        }
                        if (MItem[0]["HG_ZNSJC"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        MItem[0]["G_ZNSJC"] = "----";
                        sItem["ZNPJSJC"] = "----";
                        MItem[0]["HG_ZNSJC"] = "----";
                    }
                    #endregion
                    #region 相对耐久性
                    if (jcxm.Contains("、相对耐久性、"))
                    {
                        if (sItem["XDRHJP_1"] == "" && sItem["XDRHJP_1"] == "----")
                        {
                            sItem["XDTXML_1"] = Round((GetSafeDouble(sItem["XDRHJP_1"]) * GetSafeDouble(sItem["XDRHJP_1"])) / (GetSafeDouble(sItem["XJPCZ_1"]) * GetSafeDouble(sItem["XJPCZ_1"])) * 100, 1).ToString();
                            sItem["XDTXML_2"] = Round((GetSafeDouble(sItem["XDRHJP_2"]) * GetSafeDouble(sItem["XDRHJP_2"])) / (GetSafeDouble(sItem["XJPCZ_2"]) * GetSafeDouble(sItem["XJPCZ_2"])) * 100, 1).ToString();
                            sItem["XDTXML_3"] = Round((GetSafeDouble(sItem["XDRHJP_3"]) * GetSafeDouble(sItem["XDRHJP_3"])) / (GetSafeDouble(sItem["XJPCZ_3"]) * GetSafeDouble(sItem["XJPCZ_3"])) * 100, 1).ToString();
                        }
                        if (sItem["XDTXML_1"] == "" && sItem["XDTXML_1"] == "----")
                        {
                            sItem["XPJDTXML"] = Round((GetSafeDouble(sItem["XDTXML_1"]) + GetSafeDouble(sItem["XDTXML_2"]) + GetSafeDouble(sItem["XDTXML_3"])) / 3, 0).ToString();
                        }
                        MItem[0]["HG_XDNJX"] = IsQualified(MItem[0]["G_XDNJX"], sItem["XPJDTXML"]);
                        if (MItem[0]["HG_XDNJX"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    #endregion

                    #region 抗压强度比
                    var mlq = "";
                    var mmlq = "";
                    double mhsxs = 0;
                    if (jcxm.Contains("、抗压强度比、"))
                    {
                        for (int i = 1; i <= 4; i++)
                        {
                            if (i == 1)
                            {
                                mlq = "1d";
                                mmlq = "R-7";
                            }
                            if (i == 2)
                            {
                                mlq = "3d";
                                mmlq = "R28";
                            }
                            if (i == 3)
                            {
                                mlq = "7d";
                                mmlq = "R-7+28";
                            }
                            if (i == 4)
                            {
                                mlq = "28d";
                                mmlq = "R-7+56";
                            }
                            var dff = sItem["SJCD"];
                            if (Round(GetSafeDouble(sItem["SJCD"]), 0) == 100)
                            {
                                mhsxs = 0.95;
                            }
                            if (Round(GetSafeDouble(sItem["SJCD"]), 0) == 150)
                            {
                                mhsxs = 1;
                            }
                            if (Round(GetSafeDouble(sItem["SJCD"]), 0) == 200)
                            {
                                mhsxs = 1.05;
                            }
                            List<double> Arrmd = new List<double>();
                            if (sItem["JHZ1D1_1"] != "" && sItem["JHZ1D1_1"] != "----")
                            {
                                for (int j = 1; j <= 3; j++)
                                {
                                    sum = 0;
                                    for (int k = 1; k <= 3; k++)
                                    {
                                        md1 = GetSafeDouble(sItem["JHZ" + "1d" + j + "_" + k]);
                                        md2 = Round(1000 * md1 / (100 * 100), 1);
                                        Arrmd.Add(md2);
                                        sum = sum + md2;
                                        sItem["JHZ" + "1d" + j + "_" + k] = Round(md2, 1).ToString();
                                    }
                                    string mlongStr = Arrmd[0] + "," + Arrmd[1] + "," + Arrmd[2];
                                    string[] str = mlongStr.Split(',');
                                    foreach (string s in str)
                                    {
                                        mtmpArray.Add(GetSafeDouble(s));
                                    }
                                    mtmpArray.Sort();
                                    mMaxKyqd = mtmpArray[2];
                                    mMinKyqd = mtmpArray[0];
                                    mMidKyqd = mtmpArray[1];
                                    mAvgKyqd = mtmpArray.Average();
                                    if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    {
                                        sItem["JQDDBZ" + "1D" + j] = "重做";
                                    }
                                    if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    {
                                        sItem["JQDDBZ" + "1D" + j] = Round(mMidKyqd * mhsxs, 1).ToString();
                                    }
                                    if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    {
                                        sItem["JQDDBZ" + "1D" + j] = Round(mMidKyqd * mhsxs, 1).ToString();
                                    }
                                    if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    {
                                        sItem["JQDDBZ" + "1D" + j] = Round(mAvgKyqd * mhsxs, 1).ToString();
                                    }
                                    sItem["JPJQD"] = "";
                                    if ("重做" == sItem["JQDDBZ" + "1d" + j])
                                    {
                                        sItem["JPJQD"] = "重做";
                                    }
                                }
                                if (sItem["JPJQD"] != "重做")
                                {
                                    sItem["JPJQD"] = Round((GetSafeDouble(sItem["JQDDBZ1D1"]) + GetSafeDouble(sItem["JQDDBZ1D2"]) + GetSafeDouble(sItem["JQDDBZ1D3"])) / 3, 1).ToString();
                                }
                            }
                            if ("" != sItem["SHZ" + mlq + "1_1"] && "----" != sItem["SHZ" + mlq + "1_1"])
                            {
                                for (int j = 1; j <= 3; j++)
                                {
                                    sum = 0;
                                    for (int k = 1; k <= 3; k++)
                                    {
                                        md1 = GetSafeDouble(sItem["SHZ" + mlq + j + "_" + k]);
                                        md2 = Round(1000 * md1 / (100 * 100), 1);
                                        Arrmd.Add(md2);
                                        sum = sum + md2;
                                        sItem["SQD" + mlq + j + "_" + k] = Round(md2, 1).ToString();
                                    }
                                    string mlongStr = Arrmd[0] + "," + Arrmd[1] + "," + Arrmd[2];
                                    string[] str = mlongStr.Split(',');
                                    foreach (string s in str)
                                    {
                                        mtmpArray.Add(GetSafeDouble(s));
                                    }
                                    mtmpArray.Sort();
                                    mMaxKyqd = mtmpArray[2];
                                    mMinKyqd = mtmpArray[0];
                                    mMidKyqd = mtmpArray[1];
                                    mAvgKyqd = mtmpArray.Average();
                                    if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    {
                                        sItem["SQDDBZ" + mlq + j] = "重做";
                                    }
                                    if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    {
                                        sItem["SQDDBZ" + mlq + j] = Round(mMidKyqd * mhsxs, 1).ToString();
                                    }
                                    if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    {
                                        sItem["SQDDBZ" + mlq + j] = Round(mMidKyqd * mhsxs, 1).ToString();
                                    }
                                    if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    {
                                        sItem["SQDDBZ" + mlq + j] = Round(mAvgKyqd * mhsxs, 1).ToString();
                                    }
                                    sItem["SPJQD" + i] = "";
                                    var JQDDBZ = sItem["SQDDBZ1D" + j];
                                    JQDDBZ = sItem["SQDDBZ3D" + j];
                                    JQDDBZ = sItem["SQDDBZ7D" + j];
                                    JQDDBZ = sItem["SQDDBZ28D" + j];
                                    if ("重做" == sItem["SQDDBZ" + mlq + j])
                                    {
                                        sItem["SPJQD" + i] = "重做";
                                    }
                                }
                                if (sItem["SPJQD" + i] != "重做")
                                {
                                    sItem["SPJQD" + i] = Round((GetSafeDouble(sItem["SQDDBZ" + mlq + "1"]) + GetSafeDouble(sItem["SQDDBZ" + mlq + "2"]) + GetSafeDouble(sItem["SQDDBZ" + mlq + "3"])) / 3, 1).ToString();
                                }
                            }
                            if (0 != GetSafeDouble(sItem["JPJQD"]))
                            {
                                sItem["QDB" + i] = Round(GetSafeDouble(sItem["SPJQD" + i]) / GetSafeDouble(sItem["JPJQD"]) * 100, 0).ToString();
                            }
                            if (sItem["SPJQD" + i] == "重做" || sItem["JPJQD" + i] == "重做")
                            {
                                mAllHg = false;
                                MItem[0]["HG_KYQD" + mlq] = "重做";
                                mFlag_Bhg = true;
                            }
                            else
                            {
                                mFlag_Hg = true;
                                var JQDDBZ = sItem["G_KYQD1D"];
                                JQDDBZ = sItem["G_KYQD3D"];
                                JQDDBZ = sItem["G_KYQD7D"];
                                JQDDBZ = sItem["G_KYQD28D"];
                                JQDDBZ = sItem["QDB" + i];
                                MItem[0]["HG_KYQD" + mlq] = IsQualified(MItem[0]["G_KYQD" + mlq], sItem["QDB" + i]);
                            }
                            if (MItem[0]["HG_KYQD" + mlq] == "不符合")
                            {
                                mAllHg = false;
                                mFlag_Bhg = true;
                            }
                            else
                            {
                                mFlag_Hg = true;
                            }
                        }
                    }
                    else
                    {
                        MItem[0]["G_KYQD1D"] = "----";
                        MItem[0]["G_KYQD3D"] = "----";
                        MItem[0]["G_KYQD7D"] = "----";
                        MItem[0]["G_KYQD28D"] = "----";
                        MItem[0]["HG_KYQD1D"] = "----";
                        MItem[0]["HG_KYQD3D"] = "----";
                        MItem[0]["HG_KYQD7D"] = "----";
                        MItem[0]["HG_KYQD28D"] = "----";
                        sItem["QDB1"] = "-----";
                        sItem["QDB2"] = "-----";
                        sItem["QDB3"] = "-----";
                        sItem["QDB4"] = "-----";

                    }
                    #endregion

                    #region 收缩率比
                    if (jcxm.Contains("、收缩率比、"))
                    {
                        sItem["SSLJ1"] = Round((GetSafeDouble(sItem["SSLJL0_1"]) - GetSafeDouble(sItem["SSLJLT_1"])) / GetSafeDouble(sItem["SSLJLB_1"]) * 1000000, 1).ToString();
                        sItem["SSLJ2"] = Round((GetSafeDouble(sItem["SSLJL0_2"]) - GetSafeDouble(sItem["SSLJLT_2"])) / GetSafeDouble(sItem["SSLJLB_2"]) * 1000000, 1).ToString();
                        sItem["SSLJ3"] = Round((GetSafeDouble(sItem["SSLJL0_3"]) - GetSafeDouble(sItem["SSLJLT_3"])) / GetSafeDouble(sItem["SSLJLB_3"]) * 1000000, 1).ToString();


                        sItem["SSLS1"] = Round((GetSafeDouble(sItem["SSLSL0_1"]) - GetSafeDouble(sItem["SSLSLT_1"])) / GetSafeDouble(sItem["SSLSLB_1"]) * 1000000, 1).ToString();
                        sItem["SSLS2"] = Round((GetSafeDouble(sItem["SSLSL0_2"]) - GetSafeDouble(sItem["SSLSLT_2"])) / GetSafeDouble(sItem["SSLSLB_2"]) * 1000000, 1).ToString();
                        sItem["SSLS3"] = Round((GetSafeDouble(sItem["SSLSL0_3"]) - GetSafeDouble(sItem["SSLSLT_3"])) / GetSafeDouble(sItem["SSLSLB_3"]) * 1000000, 1).ToString();
                        if (GetSafeDouble(sItem["SSLJ1"]) != 0 && GetSafeDouble(sItem["SSLJ2"]) != 0 && GetSafeDouble(sItem["SSLJ3"]) != 0)
                        {
                            sItem["SSLB1"] = Round(GetSafeDouble(sItem["SSLS1"]) / GetSafeDouble(sItem["SSLS1"]), 1).ToString();
                            sItem["SSLB2"] = Round(GetSafeDouble(sItem["SSLS2"]) / GetSafeDouble(sItem["SSLS2"]), 1).ToString();
                            sItem["SSLB3"] = Round(GetSafeDouble(sItem["SSLS3"]) / GetSafeDouble(sItem["SSLS3"]), 1).ToString();
                        }
                        sItem["SSLB"] = Round((GetSafeDouble(sItem["SSLB1"]) + GetSafeDouble(sItem["SSLB2"]) + GetSafeDouble(sItem["SSLB3"])) / 3, 0).ToString();
                        MItem[0]["HG_SSLB"] = IsQualified(MItem[0]["G_SSLB"], sItem["SSLB"]);
                        if (MItem[0]["HG_SSLB"] != "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    #region 50次冻融强度损失率比
                    #endregion
                    if (jcxm.Contains("、50次冻融强度损失率比、"))
                    {
                        mhsxs = 0;
                        mlq = "";
                        sum = 0;
                        if (Round(GetSafeDouble(sItem["SJCD"]), 0) == 100)
                        {
                            mhsxs = 0.95;
                        }
                        if (Round(GetSafeDouble(sItem["SJCD"]), 0) == 150)
                        {
                            mhsxs = 1;
                        }
                        if (Round(GetSafeDouble(sItem["SJCD"]), 0) == 200)
                        {
                            mhsxs = 1.05;
                        }
                        for (int i = 1; i <= 4; i++)
                        {
                            if (i == 1)
                            {
                                mlq = "DRQJ";
                            }
                            if (i == 2)
                            {
                                mlq = "DRHJ";
                            }
                            if (i == 3)
                            {
                                mlq = "DRQS";
                            }
                            if (i == 4)
                            {
                                mlq = "DRHS";
                            }
                            var mHZ1 = sItem["DRQJHZ1"];

                            List<double> Arrmd = new List<double>();
                            if (sItem[mlq + "HZ1"] != "" && sItem[mlq + "HZ1"] != "----")
                            {
                                for (int j = 1; j <= 3; j++)
                                {
                                    mHZ1 = sItem["DRHJHZ" + j];
                                    mHZ1 = sItem["DRQJHZ" + j];
                                    mHZ1 = sItem["DRQSHZ" + j];
                                    mHZ1 = sItem["DRHSHZ" + j];

                                    md1 = GetSafeDouble(sItem[mlq + "HZ" + j]);
                                    md2 = Round(1000 * md1 / (100 * 100), 1);
                                    Arrmd.Add(md2);
                                    sum = sum + md2;
                                    sItem[mlq + "QD" + j] = Round(md2, 1).ToString();
                                }
                                string mlongStr = Arrmd[0] + "," + Arrmd[1] + "," + Arrmd[2];
                                string[] str = mlongStr.Split(',');
                                foreach (string s in str)
                                {
                                    mtmpArray.Add(GetSafeDouble(s));
                                }
                                mtmpArray.Sort();
                                mMaxKyqd = mtmpArray[2];
                                mMinKyqd = mtmpArray[0];
                                mMidKyqd = mtmpArray[1];
                                mAvgKyqd = mtmpArray.Average();
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                {
                                    sItem[mlq + "QD"] = Round(mMidKyqd, 1).ToString();
                                }
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                {
                                    sItem[mlq + "QD"] = Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                }
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                {
                                    sItem[mlq + "QD"] = Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                                }
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                {
                                    sItem[mlq + "QD"] = Round(mAvgKyqd * mhsxs, 1).ToString();
                                }
                            }
                        }
                        sItem["DRJQDSSL"] = Round((GetSafeDouble(sItem["DRQJQD"]) - GetSafeDouble(sItem["DRHJQD"])) / GetSafeDouble(sItem["DRQJQD"]) * 100, 1).ToString();
                        sItem["DRSQDSSL"] = Round((GetSafeDouble(sItem["DRQSQD"]) - GetSafeDouble(sItem["DRHSQD"])) / GetSafeDouble(sItem["DRQSQD"]) * 100, 1).ToString();
                        sItem["DRQDSSLB"] = Round((GetSafeDouble(sItem["DRSQDSSL"]) / GetSafeDouble(sItem["DRJQDSSL"])) * 100, 0).ToString();
                        MItem[0]["HG_DRQDB"] = IsQualified(MItem[0]["G_DRQDB"], sItem["DRQDSSLB"]);
                        if (MItem[0]["HG_DRQDB"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        sItem["DRQDSSLB"] = "----";
                        MItem[0]["HG_DRQDB"] = "----";
                        MItem[0]["G_DRQDB"] = "----";
                    }
                    #endregion
                    #region 渗透高度比
                    if (jcxm.Contains("、渗透高度比、"))
                    {
                        MItem[0]["HG_STGDB"] = IsQualified(MItem[0]["G_STGDB"], sItem["STGDB"], true);
                        if (MItem[0]["HG_STGDB"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        sItem["STGDB"] = "----";
                        MItem[0]["HG_STGDB"] = "----";
                        MItem[0]["G_STGDB"] = "----";
                    }
                    #endregion
                    #region 水泥净浆流动度
                    if (jcxm.Contains("、水泥净浆流动度、"))
                    {
                        if (sItem["LDDKZZ"].Contains("≥"))
                        {
                            sItem["LDDKZZ"] = sItem["LDDKZZ"].Substring(0, sItem["LDDKZZ"].Length - (sItem["LDDKZZ"].Length - sItem["LDDKZZ"].IndexOf("≥")));
                        }
                        else
                        {
                            MItem[0]["G_LDD"] = "≥" + Round(GetSafeDouble(sItem["LDDKZZ"]) * 0.95, 1);
                        }
                        if (sItem["LDDKZZ"] == "----")
                        {
                            MItem[0]["HG_LDD"] = "----";
                        }
                        else
                        {
                            MItem[0]["HG_LDD"] = IsQualified(MItem[0]["G_LDD"], sItem["LDD"], true);
                        }
                        if (MItem[0]["HG_LDD"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        sItem["LDD"] = "----";
                        MItem[0]["HG_LDD"] = "----";
                        MItem[0]["G_LDD"] = "----";
                    }
                    #endregion
                    #region 氯离子含量
                    if (jcxm.Contains("、氯离子含量、"))
                    {
                        if (sItem["LLZKZZ"] == "----")
                        {
                            MItem[0]["HG_LLZHL"] = "----";
                        }
                        else
                        {
                            MItem[0]["HG_LLZHL"] = IsQualified(sItem["LLZKZZ"], sItem["LLZHL"]);
                        }
                        MItem[0]["G_LLZHL"] = sItem["LLZKZZ"];
                        if (MItem[0]["HG_LLZHL"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        sItem["LLZHL1"] = "0";
                        sItem["LLZHL2"] = "0";
                        sItem["LLZHL"] = "0";
                        MItem[0]["HG_LLZHL"] = "----";
                    }
                    #endregion
                    #region 碱含量
                    if (jcxm.Contains("、碱含量、"))
                    {
                        MItem[0]["G_ZJL"] = sItem["ZJLKZZ"].Trim();
                        if (sItem["ZJLKZZ"] == "----")
                        {
                            MItem[0]["HG_ZJL"] = "----";
                        }
                        else
                        {
                            MItem[0]["HG_ZJL"] = IsQualified(MItem[0]["G_ZJL"], sItem["ZJL"]);
                        }
                        if (MItem[0]["HG_ZJL"] == "不符合")
                        {
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        MItem[0]["G_ZJL"] = "----";
                        MItem[0]["HG_ZJL"] = "----";
                        sItem["ZJL"] = "----";
                    }
                    #endregion
                }
                if (sItem["SFFJ"] == "1")
                {
                    if (mbhggs > 0)
                    {
                        sItem["JCJG"] = "不合格";
                        mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
                        if (mFlag_Bhg && mFlag_Hg)
                            mitem["JCJGMS"] = "该组试样部分符合" + mitem["PDBZ"] + "标准要求";
                        else
                        {
                            mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                            sItem["JCJG"] = "合格";
                        }
                    }
                    else
                    {
                        if (mbhggs == 0)
                        {
                            mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求";
                            sItem["JCJG"] = "合格";
                        }
                        if (mbhggs > 0)
                        {
                            mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求";
                            sItem["JCJG"] = "不合格";
                            if (mFlag_Bhg && mFlag_Hg)
                                mitem["JCJGMS"] = "该组试样部分符合" + mitem["PDBZ"] + "标准要求";
                        }
                    }
                    mAllHg = (mAllHg && sItem["JCJG"] == "合格");
                    continue;
                }
            }
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
                if (mFlag_Bhg && mFlag_Hg)
                    mitem["JCJGMS"] = "该组试样所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/

        }
    }
}