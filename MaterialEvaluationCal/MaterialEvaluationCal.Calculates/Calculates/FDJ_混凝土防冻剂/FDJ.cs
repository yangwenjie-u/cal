using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class FDJ1 : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_TS"];
            var MItem = data["M_TS"];
            var extraDJ = dataExtra["BZ_FDJ_DJ"];
            int mbHggs = 0;
            string mJSFF = "";
            bool sfhg = true;
            if (!data.ContainsKey("M_TS"))
            {
                data["M_TS"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var jcxm = "";

            double mYqpjz, mXdy21, mDy21 = 0;
            bool sign = false;
            double md1, md2, md, sum, Gs, pjmd = 0;
            int xd = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            List<double> nArr = new List<double>();
            #region  局部方法
            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
            delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {

                mbhggs = 0;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mFlag_Hg = true;
                sign = true;

                if (jcxm.Contains("、细度、"))
                {
                    mItem["hg_xd"] = IsQualified(sItem["xdkzz"], sItem["xd"]);
                    mItem["g_xd"] = sItem["xdkzz"];
                    if (mItem["hg_xd"] == "不符合")
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
                    sItem["xd"] = "----";
                    mItem["hg_xd"] = "----";
                    mItem["g_xd"] = "----";
                }

                if (jcxm.Contains("、密度、"))
                {
                    if (Conversion.Val(sItem["mdkzz"]) > 1.1)
                    {
                        mItem["g_md"] = Math.Round(Conversion.Val(sItem["mdkzz"]) - 0.03, 3).ToString("0.000") + "~" + Math.Round(Conversion.Val(sItem["mdkzz"]) + 0.03, 3).ToString("0.000");
                    }
                    else
                    {
                        mItem["g_md"] = Math.Round(Conversion.Val(sItem["mdkzz"]) - 0.02, 3).ToString("0.000") + "~" + Math.Round(Conversion.Val(sItem["mdkzz"]) + 0.02, 3).ToString("0.000");
                    }
                    if (sItem["mdkzz"] == "----")
                    {
                        mItem["hg_md"] = "----";
                    }
                    else
                    {
                        mItem["hg_md"] = IsQualified(mItem["g_md"], sItem["md"]);
                    }

                    if (mItem["hg_md"] == "不符合")
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
                    sItem["md"] = "----";
                    mItem["hg_md"] = "----";
                    mItem["g_md"] = "----";
                }
                if (jcxm.Contains("、固体含量、"))
                {
                    if (Conversion.Val(sItem["hglkzz"]) >= 20)
                    {
                        mItem["g_gthl"] = Math.Round(Conversion.Val(sItem["hglkzz"]) * 0.95, 2).ToString("0.00") + "~" + Math.Round(Conversion.Val(sItem["hglkzz"]) * 1.05, 2).ToString("0.00");
                    }
                    else
                    {
                        mItem["g_gthl"] = Math.Round(Conversion.Val(sItem["hglkzz"]) * 0.9, 2).ToString("0.00") + "~" + Math.Round(Conversion.Val(sItem["hglkzz"]) * 1.1, 2).ToString("0.00");

                    }
                    if (sItem["hglkzz"] == "----")
                    {
                        mItem["hg_gthl"] = "----";
                    }
                    else
                    {
                        mItem["hg_gthl"] = IsQualified(mItem["g_gthl"], sItem["gthl"]);
                    }

                    if (mItem["hg_gthl"] == "不符合")
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
                    sItem["gthl_1"] = "----";
                    sItem["gthl_2"] = "----";
                    sItem["gthl"] = "----";
                    mItem["g_gthl"] = "----";
                    mItem["hg_gthl"] = "----";
                }

                if (jcxm.Contains("、含水率、"))
                {
                    if (Conversion.Val(sItem["hslkzz"]) >= 5)
                    {
                        mItem["g_hsl"] = Math.Round(Conversion.Val(sItem["hslkzz"]) * 0.9, 2).ToString("0.00") + "~" + Math.Round(Conversion.Val(sItem["hslkzz"]) * 1.1, 2).ToString("0.00");
                    }
                    else
                    {
                        mItem["g_hsl"] = Math.Round(Conversion.Val(sItem["hslkzz"]) * 0.8, 2).ToString("0.00") + "~" + Math.Round(Conversion.Val(sItem["hslkzz"]) * 1.2, 2).ToString("0.00");

                    }
                    if (sItem["hslkzz"] == "----")
                    {
                        mItem["hg_hsl"] = "----";
                    }
                    else
                    {
                        mItem["hg_hsl"] = IsQualified(mItem["g_hsl"], sItem["hsl"]);
                    }

                    if (mItem["hg_gthl"] == "不符合")
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
                    sItem["hsl"] = "----";
                    mItem["g_hsl"] = "----";
                    mItem["hg_hsl"] = "----";
                }

                if (jcxm.Contains("、泌水率、"))
                {
                    mItem["hg_msl"] = IsQualified(mItem["g_msl"], sItem["mslb"]);
                    if (mItem["hg_msl"] == "不符合")
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
                    mItem["g_msl"] = "----";
                    sItem["mslb"] = "-----";
                    mItem["hg_msl"] = "----";
                }
                if (jcxm.Contains("、减水率、"))
                {
                    mItem["hg_jsl"] = IsQualified(mItem["g_msl"], sItem["mslb"]);
                    if (mItem["hg_jsl"] == "不符合")
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
                    mItem["g_jsl"] = "----";
                    sItem["pjjsl"] = "-----";
                    mItem["hg_jsl"] = "----";
                }

                if (jcxm.Contains("、含气量、"))
                {
                    mItem["hg_hql"] = IsQualified(mItem["g_hql"], sItem["pjhql"]);
                    if (mItem["hg_hql"] == "不符合")
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
                    mItem["g_hql"] = "----";
                    sItem["pjhql"] = "-----";
                    mItem["hg_hql"] = "----";
                }

                if (jcxm.Contains("、初凝时间差、"))
                {
                    mItem["hg_cnsjc"] = IsQualified(mItem["g_cnsjc"], sItem["CNPJSJC"]);
                    if (mItem["hg_cnsjc"] == "不符合")
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
                    mItem["g_cnsjc"] = "----";
                    sItem["CNPJSJC"] = "-----";
                    mItem["hg_cnsjc"] = "----";
                }

                if (jcxm.Contains("、终凝时间差、"))
                {
                    mItem["hg_znsjc"] = IsQualified(mItem["g_znsjc"], sItem["zNPJSJC"]);
                    if (mItem["hg_znsjc"] == "不符合")
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
                    mItem["g_znsjc"] = "----";
                    sItem["zNPJSJC"] = "-----";
                    mItem["hg_znsjc"] = "----";
                }

                if (jcxm.Contains("、R-7抗压强度比、"))
                {
                    mItem["HG_KYQD1d"] = IsQualified(mItem["G_KYQD1d"], sItem["QDB1"]);
                    if (mItem["HG_KYQD1d"] == "不符合")
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
                    mItem["G_KYQD1d"] = "----";
                    sItem["QDB1"] = "-----";
                    mItem["HG_KYQD1d"] = "----";
                }

                if (jcxm.Contains("、R28抗压强度比、"))
                {
                    mItem["HG_KYQD3d"] = IsQualified(mItem["G_KYQD3d"], sItem["QDB2"]);
                    if (mItem["HG_KYQD3d"] == "不符合")
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
                    mItem["G_KYQD3d"] = "----";
                    sItem["QDB2"] = "-----";
                    mItem["HG_KYQD3d"] = "----";
                }
                if (jcxm.Contains("、R-7+28抗压强度比、") || jcxm.Contains("、R－7＋28抗压强度比、"))
                {
                    mItem["HG_KYQD7d"] = IsQualified(mItem["G_KYQD7d"], sItem["QDB3"]);
                    if (mItem["HG_KYQD7d"] == "不符合")
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
                    mItem["G_KYQD7d"] = "----";
                    sItem["QDB3"] = "-----";
                    mItem["HG_KYQD7d"] = "----";
                }

                if (jcxm.Contains("、R-7+56抗压强度比、") || jcxm.Contains("、R－7＋56抗压强度比、"))
                {
                    mItem["HG_KYQD28d"] = IsQualified(mItem["G_KYQD28d"], sItem["QDB4"]);
                    if (mItem["HG_KYQD28d"] == "不符合")
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
                    mItem["G_KYQD28d"] = "----";
                    sItem["QDB4"] = "-----";
                    mItem["HG_KYQD28d"] = "----";
                }
                if (jcxm.Contains("、收缩率比、"))
                {
                    mItem["HG_SSLB"] = IsQualified(mItem["G_SSLB"], sItem["sslb"]);
                    if (mItem["HG_SSLB"] == "不符合")
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
                    sItem["sslb"] = "----";
                    mItem["HG_SSLB"] = "----";
                    mItem["G_SSLB"] = "----";
                }

                if (jcxm.Contains("、50次冻融强度损失率比、"))
                {
                    mItem["HG_DRQDB"] = IsQualified(mItem["G_DRQDB"], sItem["DRQDSSLB"]);
                    if (mItem["HG_DRQDB"] == "不符合")
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
                    sItem["DRQDSSLB"] = "----";
                    mItem["HG_DRQDB"] = "----";
                    mItem["G_DRQDB"] = "----";
                }


                if (jcxm.Contains("、渗透高度比、"))
                {
                    mItem["HG_STGDB"] = IsQualified(mItem["G_STGDB"], sItem["STGDB"]);
                    if (mItem["HG_STGDB"] == "不符合")
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
                    sItem["STGDB"] = "----";
                    mItem["HG_STGDB"] = "----";
                    mItem["G_STGDB"] = "----";
                }

                if (jcxm.Contains("、水泥净浆流动度、"))
                {
                    var length = sItem["lddkzz"].IndexOf("≥");
                    if (length > -1)
                    {
                        //Right(sitem["lddkzz, Len(sitem["lddkzz) - InStr(1, sitem["lddkzz, "≥"))
                        mItem["lddkzz"] = mItem["lddkzz"].Substring(length);
                    }
                    else
                    {
                        mItem["G_ldd"] = "≥" + Math.Round(Conversion.Val(sItem["lddkzz"]) * 0.95, 1).ToString();
                    }
                    if (sItem["lddkzz"] == "----")
                    {
                        mItem["HG_ldd"] = "----";
                    }
                    else
                    {
                        mItem["HG_ldd"] = IsQualified(mItem["G_ldd"], sItem["ldd"]);

                    }
                    if (mItem["HG_ldd"] == "不符合")
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
                    sItem["ldd"] = "----";
                    mItem["HG_ldd"] = "----";
                    mItem["G_ldd"] = "----";
                }

                if (jcxm.Contains("、氯离子含量、"))
                {
                    if (sItem["llzkzz"] == "----")
                    {
                        mItem["HG_llzhl"] = "----";
                    }
                    else
                    {
                        mItem["HG_llzhl"] = IsQualified(mItem["llzkzz"], sItem["llzhl"]);

                    }
                    mItem["G_llzhld"] = sItem["llzkzzd"];
                    if (mItem["HG_llzhl"] == "不符合")
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
                    sItem["llzhl"] = "----";
                    mItem["HG_llzhl"] = "----";
                }

                if (jcxm.Contains("、碱含量、"))
                {
                    mItem["G_zjl"] = sItem["zjlkzz"].Trim();
                    if (sItem["zjlkzz"] == "----")
                    {
                        mItem["hg_zjl"] = "----";
                    }
                    else
                    {
                        mItem["hg_zjl"] = IsQualified(mItem["G_zjl"], sItem["zjl"]);

                    }
                    if (mItem["hg_zjl"] == "不符合")
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
                    mItem["G_zjl"] = "----";
                    sItem["zjl"] = "----";
                    mItem["hg_zjl"] = "----";
                }

                //If sItem["sffj"] Then
                if (mbhggs == 0)
                {
                    jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                    sItem["JCJG"] = "合格";
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                    if (mFlag_Bhg && mFlag_Hg)
                    {
                        jsbeizhu = "该组试样部分符合" + MItem[0]["PDBZ"] + "标准要求。";

                    }
                }

                return mAllHg;
            };
            #endregion

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                string mSjdj = sItem["wjjmc"];
                List<double> mtmpArray = new List<double>();
                double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd;
                sfhg = true;
                if (string.IsNullOrEmpty(mSjdj))
                {
                    mSjdj = "";
                }
                var extraFieldsDJ = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj && u["DJ"] == sItem["DJ"]);
                if (jcxm.Trim().Contains("强度比"))
                {
                    extraFieldsDJ = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj && u["DJ"] == sItem["DJ"] && u["KYQDBWD"] == sItem["KYGDWD"]);
                }
                if (null == extraFieldsDJ)
                {
                    MItem[0]["G_XD"] = extraFieldsDJ["XD"];
                    MItem[0]["G_MD"] = extraFieldsDJ["MD"];
                    MItem[0]["G_MSL"] = extraFieldsDJ["MSL"];
                    MItem[0]["G_JSL"] = extraFieldsDJ["JSL"];
                    MItem[0]["G_GTHL"] = extraFieldsDJ["GTHL"];
                    MItem[0]["G_CNSJC"] = extraFieldsDJ["CNSJC"];
                    MItem[0]["G_ZNSJC"] = extraFieldsDJ["ZNSJC"];
                    MItem[0]["G_TLD"] = extraFieldsDJ["TLD"];
                    MItem[0]["G_HQLBHL"] = extraFieldsDJ["HQLBHL"];
                    MItem[0]["G_HQL"] = extraFieldsDJ["HQL"];
                    MItem[0]["G_KYQD1D"] = extraFieldsDJ["KYQDB1D"];
                    MItem[0]["G_KYQD3D"] = extraFieldsDJ["KYQDB3D"];
                    MItem[0]["G_KYQD7D"] = extraFieldsDJ["KYQDB7D"];
                    MItem[0]["G_KYQD28D"] = extraFieldsDJ["KYQDB28D"];
                    MItem[0]["G_XDNJX"] = extraFieldsDJ["XDNJX"];
                    MItem[0]["G_PH"] = extraFieldsDJ["PH"];
                    MItem[0]["G_LLZHL"] = extraFieldsDJ["LLZHL"];
                    MItem[0]["G_LDD"] = extraFieldsDJ["LDD"];
                    MItem[0]["G_STGDB"] = extraFieldsDJ["STGDB"];
                    MItem[0]["G_DRQDB"] = extraFieldsDJ["DRQDSSLB"];
                    MItem[0]["G_SSLB"] = extraFieldsDJ["SSLB28D"];
                    if (extraFieldsDJ["JSFF"].Trim() == "")
                    {
                        mJSFF = "";
                    }
                    else
                    {
                        mJSFF = extraFieldsDJ["JSFF"].Trim();
                    }
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "依据不详";
                    jsbeizhu = jsbeizhu + "单组流水号:" + MItem[0]["DZBH"] + "试件尺寸为空。";
                    continue;
                }
                //跳转
                if (!string.IsNullOrEmpty(sItem["sjtabs"]))
                {
                    //杨文杰
                    mAllHg = sjtabcalc(MItem[0], sItem);
                    continue;
                }

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
                        sfhg = false;
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
                        sfhg = false;
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
                        sfhg = false;
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
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                    #endregion
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
                #region 泌水率
                if (jcxm.Contains("、泌水率、"))
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
                        jsbeizhu = "";

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
                    if (sItem["smszl_1"] != "" && sItem["smszl_1"] != "----")
                    {
                        sItem["smsl_1"] = Round(GetSafeDouble(sItem["smszl_1"]) / (GetSafeDouble(sItem["MSBYS_1"]) / GetSafeDouble(sItem["sphwzl_1"])) / GetSafeDouble(sItem["ssyzl_1"]) * 100, 2).ToString();
                        sItem["smsl_2"] = Round(GetSafeDouble(sItem["smszl_2"]) / (GetSafeDouble(sItem["MSBYS_2"]) / GetSafeDouble(sItem["sphwzl_2"])) / GetSafeDouble(sItem["ssyzl_2"]) * 100, 2).ToString();
                        sItem["smsl_3"] = Round(GetSafeDouble(sItem["smszl_3"]) / (GetSafeDouble(sItem["MSBYS_3"]) / GetSafeDouble(sItem["sphwzl_3"])) / GetSafeDouble(sItem["ssyzl_3"]) * 100, 2).ToString();
                    }
                    if (!string.IsNullOrEmpty(sItem["smsl_1"]) && sItem["smsl_1"] != "----")
                    {
                        string mlongStr = sItem["smsl_1"] + "," + sItem["smsl_2"] + "," + sItem["smsl_3"];
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
                        jsbeizhu = "";
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["hg_msl"] = "重做";
                            sItem["spjmsl"] = "重做";
                        }
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            sItem["spjmsl"] = Round(mMidKyqd, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            sItem["spjmsl"] = Round(mMidKyqd, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            sItem["spjmsl"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }

                    if (sItem["jpjmsl"] == "重做" || sItem["spjmsl"] == "重做")
                    {
                        sfhg = false;
                        if (sItem["jpjmsl"] == "重做" && sItem["spjmsl"] == "重做")
                        {
                            sItem["hg_msl"] = "基准受检重做";
                        }
                        else
                        {
                            if (MItem[0]["jpjmsl"] == "重做")
                            {
                                MItem[0]["hg_msl"] = "基准重做";
                            }
                            else
                            {
                                MItem[0]["hg_msl"] = "受检重做";
                            }
                        }
                    }
                    else
                    {
                        if (sItem["spjmsl"] != "" && sItem["spjmsl"] != "----" && GetSafeDouble(sItem["spjmsl"]) != 0)
                        {
                            sItem["mslb"] = Round((GetSafeDouble(sItem["spjmsl"]) / GetSafeDouble(sItem["jpjmsl"])) * 100, 0).ToString();
                        }
                        MItem[0]["hg_msl"] = IsQualified(MItem[0]["g_msl"], sItem["mslb"], true);
                    }
                    if (MItem[0]["hg_msl"] == "不符合")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    MItem[0]["g_msl"] = "----";
                    sItem["mslb"] = "----";
                    MItem[0]["hg_msl"] = "----";
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
                        sItem["jsl_1"] = Round((GetSafeDouble(sItem["JJDYS_1"]) - GetSafeDouble(sItem["JSDYS_1"])) / GetSafeDouble(sItem["JJDYS_1"]) * 100, 1).ToString();
                        sItem["jsl_2"] = Round((GetSafeDouble(sItem["JJDYS_2"]) - GetSafeDouble(sItem["JSDYS_2"])) / GetSafeDouble(sItem["JJDYS_2"]) * 100, 1).ToString();
                        sItem["jsl_3"] = Round((GetSafeDouble(sItem["JJDYS_3"]) - GetSafeDouble(sItem["JSDYS_3"])) / GetSafeDouble(sItem["JJDYS_3"]) * 100, 1).ToString();
                    }
                    if (sItem["jsl_1"] != "" && sItem["jsl_1"] != "----")
                    {
                        string mlongStr = sItem["jsl_1"] + "," + sItem["jsl_2"] + "," + sItem["jsl_3"];
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
                        jsbeizhu = "";

                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["hg_jsl"] = "重做";
                            sItem["pjjsl"] = "重做";
                        }
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            sItem["pjjsl"] = Round(mMidKyqd, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            sItem["pjjsl"] = Round(mMidKyqd, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            sItem["pjjsl"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    if (sItem["pjjsl"] == "重做")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                        MItem[0]["hg_jsl"] = IsQualified(MItem[0]["g_jsl"], sItem["g_jsl"], true);
                    }
                    if (MItem[0]["hg_jsl"] == "不符合")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    MItem[0]["g_jsl"] = "----";
                    sItem["pjjsl"] = "----";
                    MItem[0]["hg_jsl"] = "----";
                }
                #endregion
                #region 含气量
                if (jcxm.Contains("、含气量、"))
                {
                    if (sItem["BHWHQL_1"] != "" && sItem["BHWHQL_1"] != "----")
                    {
                        sItem["hql_1"] = (GetSafeDouble(sItem["BHWHQL_1"]) - GetSafeDouble(sItem["SSHQL_1"])).ToString();
                        sItem["hql_2"] = (GetSafeDouble(sItem["BHWHQL_2"]) - GetSafeDouble(sItem["SSHQL_2"])).ToString();
                        sItem["hql_3"] = (GetSafeDouble(sItem["BHWHQL_3"]) - GetSafeDouble(sItem["SSHQL_3"])).ToString();
                    }
                    if (sItem["hql_1"] != "" && sItem["hql_1"] != "----")
                    {
                        string mlongStr = sItem["hql_1"] + "," + sItem["hql_2"] + "," + sItem["hql_3"];
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
                        jsbeizhu = "";
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["hg_hql"] = "重做";
                            sItem["pjhql"] = "重做";
                        }
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            sItem["pjhql"] = Round(mMidKyqd, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Round(mMidKyqd * 0.15, 1))
                        {
                            sItem["pjhql"] = Round(mMidKyqd, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Round(mMidKyqd * 0.15, 1))
                        {
                            sItem["pjhql"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    if (sItem["pjhql"] == "重做")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                        MItem[0]["hg_hql"] = IsQualified(MItem[0]["g_hql"], sItem["pjhql"]);
                    }
                    if (MItem[0]["hg_hql"] == "不符合")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    MItem[0]["g_hql"] = "----";
                    sItem["pjhql"] = "----";
                    MItem[0]["hg_hql"] = "----";
                }
                #endregion

                #region 初凝时间差
                if (jcxm.Contains("、初凝时间差、"))
                {
                    if (sItem["CNSJT_1"] != "" && sItem["CNSJT_1"] != "----")
                    {
                        sItem["cnsjc_1"] = (GetSafeDouble(sItem["CNSJT_1"]) - GetSafeDouble(sItem["CNJZT_1"])).ToString();
                        sItem["cnsjc_2"] = (GetSafeDouble(sItem["CNSJT_2"]) - GetSafeDouble(sItem["CNJZT_2"])).ToString();
                        sItem["cnsjc_3"] = (GetSafeDouble(sItem["CNSJT_3"]) - GetSafeDouble(sItem["CNJZT_3"])).ToString();
                    }
                    if (sItem["cnsjc_1"] != "" && sItem["cnsjc_1"] != "----")
                    {
                        string mlongStr = sItem["cnsjc_1"] + "," + sItem["cnsjc_2"] + "," + sItem["cnsjc_3"];
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
                        MItem[0]["jsbeizhu"] = "";
                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) > 30)
                        {
                            MItem[0]["hg_cnsjc"] = "重做";
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
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                        MItem[0]["hg_cnsjc"] = IsQualified(MItem[0]["g_cnsjc"], sItem["CNPJSJC"]);
                    }
                    if (MItem[0]["hg_cnsjc"] == "不符合")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    MItem[0]["g_cnsjc"] = "----";
                    sItem["CNPJSJC"] = "----";
                    MItem[0]["hg_cnsjc"] = "----";
                }
                #endregion
                #region 终凝时间差
                if (jcxm.Contains("、终凝时间差、"))
                {
                    if (sItem["zNSJT_1"] != "" && sItem["zNSJT_1"] != "----")
                    {
                        sItem["znsjc_1"] = (GetSafeDouble(sItem["zNSJT_1"]) - GetSafeDouble(sItem["zNJZT_1"])).ToString();
                        sItem["znsjc_2"] = (GetSafeDouble(sItem["zNSJT_2"]) - GetSafeDouble(sItem["zNSJT_2"])).ToString();
                        sItem["znsjc_3"] = (GetSafeDouble(sItem["zNSJT_3"]) - GetSafeDouble(sItem["zNSJT_3"])).ToString();
                    }
                    if (sItem["znsjc_1"] != "" && sItem["znsjc_1"] != "----")
                    {
                        string mlongStr = sItem["znsjc_1"] + "," + sItem["znsjc_2"] + "," + sItem["znsjc_3"];
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
                        MItem[0]["jsbeizhu"] = "";
                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) > 30)
                        {
                            MItem[0]["hg_znsjc"] = "重做";
                            sItem["zNPJSJC"] = "重做";
                        }
                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) <= 30)
                        {
                            sItem["zNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) > 30)
                        {
                            sItem["zNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) <= 30)
                        {
                            sItem["zNPJSJC"] = Round(mAvgKyqd, 0).ToString();
                        }
                    }
                    if (sItem["zNPJSJC"] == "重做")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                        MItem[0]["hg_znsjc"] = IsQualified(MItem[0]["g_znsjc"], sItem["zNPJSJC"]);
                    }
                    if (MItem[0]["hg_znsjc"] == "不符合")
                    {
                        sfhg = false;
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
                    MItem[0]["hg_xdnjx"] = IsQualified(MItem[0]["g_xdnjx"], sItem["XPJDTXML"]);
                    if (MItem[0]["hg_xdnjx"] == "不符合")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                #endregion
                var mlq = "";
                var mmlq = "";
                double mhsxs = 0;
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
                    if (jcxm.Contains(mmlq + "抗压强度比、"))
                    {
                        if (sItem["jhz1d1_1"] != "" && sItem["jhz1d1_1"] != "----")
                        {
                            for (int j = 1; j <= 3; j++)
                            {
                                sum = 0;
                                for (int k = 1; k <= 3; k++)
                                {
                                    md1 = GetSafeDouble(sItem["jhz" + "1d" + j + "_" + k]);
                                    md2 = Round(1000 * md1 / (100 * 100), 1);
                                    Arrmd.Add(md2);
                                    sum = sum + md2;
                                    sItem["jhz" + "1d" + j + "_" + k] = Round(md2, 1).ToString();
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
                                jsbeizhu = "";
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
                                if (sItem["JQDDBZ" + "1d" + j] == "重做")
                                {
                                    sItem["JPJQD"] = "重做";
                                }
                            }
                            if (sItem["JPJQD"] != "重做")
                            {
                                sItem["JPJQD"] = Round((GetSafeDouble(sItem["JQDDBZ1D1"]) + GetSafeDouble(sItem["JQDDBZ1D2"]) + GetSafeDouble(sItem["JQDDBZ1D3"])) / 3, 1).ToString();
                            }
                        }
                        if (sItem["shz" + mlq + "1_1"] != "" && sItem["shz" + mlq + "1_1"] != "----")
                        {
                            for (int j = 1; j <= 3; j++)
                            {
                                sum = 0;
                                for (int k = 1; k <= 3; k++)
                                {
                                    md1 = GetSafeDouble(sItem["shz" + mlq + j + "_" + k]);
                                    md2 = Round(1000 * md1 / (100 * 100), 1);
                                    Arrmd.Add(md2);
                                    sum = sum + md2;
                                    sItem["sqd" + mlq + j + "_" + k] = Round(md2, 1).ToString();
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
                                jsbeizhu = "";
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
                                if (sItem["JQDDBZ" + mlq + j] == "重做")
                                {
                                    sItem["SPJQD" + i] = "重做";
                                }
                            }
                            if (sItem["SPJQD" + i] != "重做")
                            {
                                sItem["SPJQD" + i] = Round((GetSafeDouble(sItem["SQDDBZ" + mlq + "1"]) + GetSafeDouble(sItem["SQDDBZ" + mlq + "2"]) + GetSafeDouble(sItem["SQDDBZ" + mlq + "3"])) / 3, 1).ToString();
                            }
                        }
                        if (GetSafeDouble(sItem["JPJQD"]) != 0)
                        {
                            sItem["QDB" + i] = Round(GetSafeDouble(sItem["SPJQD" + i]) / GetSafeDouble(sItem["JPJQD"]) * 100, 0).ToString();
                        }
                        if (sItem["SPJQD" + i] == "重做" || sItem["JPJQD" + i] == "重做")
                        {
                            sfhg = false;
                            MItem[0]["HG_KYQD" + mlq] = "重做";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                            MItem[0]["HG_KYQD" + mlq] = IsQualified(MItem[0]["G_KYQD" + mlq], sItem["QDB" + i]);
                        }
                        if (MItem[0]["HG_KYQD" + mlq] == "不符合")
                        {
                            sfhg = false;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                    else
                    {
                        MItem[0]["G_KYQD" + mlq] = "----";
                        sItem["QDB" + i] = "-----";
                        MItem[0]["HG_KYQD" + mlq] = "----";
                    }
                }
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
                        sItem["sslb1"] = Round(GetSafeDouble(sItem["SSLS1"]) / GetSafeDouble(sItem["SSLS1"]), 1).ToString();
                        sItem["sslb2"] = Round(GetSafeDouble(sItem["SSLS2"]) / GetSafeDouble(sItem["SSLS2"]), 1).ToString();
                        sItem["sslb3"] = Round(GetSafeDouble(sItem["SSLS3"]) / GetSafeDouble(sItem["SSLS3"]), 1).ToString();
                    }
                    sItem["sslb"] = Round((GetSafeDouble(sItem["sslb1"]) + GetSafeDouble(sItem["sslb2"]) + GetSafeDouble(sItem["sslb3"])) / 3, 0).ToString();
                    MItem[0]["HG_SSLB"] = IsQualified(MItem[0]["G_SSLB"], sItem["sslb"]);
                    if (MItem[0]["HG_SSLB"] != "不符合")
                    {
                        sfhg = false;
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
                        List<double> Arrmd = new List<double>();
                        if (sItem[mlq + "HZ1"] != "" && sItem[mlq + "HZ1"] != "----")
                        {
                            for (int j = 1; j <= 3; j++)
                            {
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
                            jsbeizhu = "";
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
                        sfhg = false;
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
                        sfhg = false;
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
                    if (sItem["lddkzz"].Contains("≥"))
                    {
                        sItem["lddkzz"] = sItem["lddkzz"].Substring(0, sItem["lddkzz"].Length - (sItem["lddkzz"].Length - sItem["lddkzz"].IndexOf("≥")));
                    }
                    else
                    {
                        MItem[0]["G_ldd"] = "≥" + Round(GetSafeDouble(sItem["lddkzz"]) * 0.95, 1);
                    }
                    if (sItem["lddkzz"] == "----")
                    {
                        MItem[0]["HG_ldd"] = "----";
                    }
                    else
                    {
                        MItem[0]["HG_ldd"] = IsQualified(MItem[0]["G_ldd"], sItem["ldd"], true);
                    }
                    if (MItem[0]["HG_ldd"] == "不符合")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["ldd"] = "----";
                    MItem[0]["HG_ldd"] = "----";
                    MItem[0]["G_ldd"] = "----";
                }
                #endregion
                #region 氯离子含量
                if (jcxm.Contains("、氯离子含量、"))
                {
                    if (sItem["llzkzz"] == "----")
                    {
                        MItem[0]["HG_llzhl"] = "----";
                    }
                    else
                    {
                        MItem[0]["HG_llzhl"] = IsQualified(sItem["llzkzz"], sItem["llzhl"]);
                    }
                    MItem[0]["G_llzhl"] = sItem["llzkzz"];
                    if (MItem[0]["HG_llzhl"] == "不符合")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["llzhl1"] = 0.ToString();
                    sItem["llzhl2"] = 0.ToString();
                    sItem["llzhl"] = 0.ToString();
                    MItem[0]["HG_llzhl"] = "----";
                }
                #endregion
                #region 碱含量
                if (jcxm.Contains("、碱含量、"))
                {
                    MItem[0]["G_zjl"] = sItem["zjlkzz"].Trim();
                    if (sItem["zjlkzz"] == "----")
                    {
                        MItem[0]["hg_zjl"] = "----";
                    }
                    else
                    {
                        MItem[0]["hg_zjl"] = IsQualified(MItem[0]["G_zjl"], sItem["zjl"]);
                    }
                    if (MItem[0]["hg_zjl"] == "不符合")
                    {
                        sfhg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    MItem[0]["G_zjl"] = "----";
                    MItem[0]["hg_zjl"] = "----";
                    sItem["zjl"] = "----";
                }
                #endregion
                jsbeizhu = "";
                if (Convert.ToBoolean(sItem["sffj"]))
                {
                    if (sfhg)
                    {
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "该组试样不符合" + MItem[0]["pdbz"] + "标准要求";
                    }
                    else
                    {
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "该组试样所检项目符合" + MItem[0]["pdbz"] + "标准要求";
                    }
                }
                else
                {
                    sItem["ybgbh"] = "";
                    if (sfhg)
                    {
                        jsbeizhu = "该组试样所检项目符合" + MItem[0]["pdbz"] + "标准要求";
                        sItem["JCJG"] = "合格";
                    }
                    if (!sfhg)
                    {
                        mAllHg = false;
                        jsbeizhu = "该组试样不符合" + MItem[0]["pdbz"] + "标准要求";
                        sItem["JCJG"] = "不合格";
                    }
                    if (mFlag_Bhg && mFlag_Hg)
                    {
                        mAllHg = false;
                        jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["pdbz"] + "标准要求";
                        sItem["JCJG"] = "不合格";
                    }
                }
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
        }
    }
}