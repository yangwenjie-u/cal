using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class WJJ2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_WJJ_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_WJJ"];
            var MItem = data["M_WJJ"];

            if (!data.ContainsKey("M_WJJ"))
            {
                data["M_WJJ"] = new List<IDictionary<string, string>>();
            }

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var itemHg = false;
            var jcxm = "";

            var mSjdj = "";
            var mJSFF = "";
            var mbhggs = 0;
            var xd = 0;
            double md1, md2, sum, pjmd = 0;
            var mFlag_Bhg = false;

            List<double> narr = new List<double>();
            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
             delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
             {

                 mbhggs = 0;
                 jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                 itemHg = true;

                 if (jcxm.Contains("、细度、"))
                 {
                     if (sItem["XDKZZ"] == "----")
                     {
                         mItem["HG_XD"] = "----";
                     }
                     else
                     {
                         mItem["HG_XD"] = IsQualified(sItem["XDKZZ"], sItem["XD"], true);
                     }

                     mItem["G_XD"] = sItem["XDKZZ"];
                     if (mItem["HG_XD"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["XD"] = "----";
                     mItem["HG_XD"] = "----";
                     mItem["G_XD"] = "----";
                 }

                 if (jcxm.Contains("、密度、"))
                 {
                     if (1.1 < Conversion.Val(sItem["MDKZZ"]))
                     {
                         mItem["G_MD"] = Math.Round(Conversion.Val(sItem["MDKZZ"]) - 0.03, 3).ToString("0.000") + "~" + Math.Round(Conversion.Val(sItem["MDKZZ"]) + 0.03, 3).ToString("0.000");
                     }
                     else
                     {
                         mItem["G_MD"] = Math.Round(Conversion.Val(sItem["MDKZZ"]) - 0.02, 3).ToString("0.000") + "~" + Math.Round(Conversion.Val(sItem["MDKZZ"]) + 0.03, 3).ToString("0.000");
                     }


                     if (sItem["MDKZZ"] == "----")
                     {
                         mItem["HG_MD"] = "----";
                         mItem["G_MD"] = "----";
                     }
                     else
                     {
                         mItem["HG_MD"] = IsQualified(mItem["G_MD"], sItem["MD"], true);
                     }

                     if (mItem["HG_MD"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["MD"] = "----";
                     mItem["HG_MD"] = "----";
                     mItem["G_MD"] = "----";
                 }

                 if (jcxm.Contains("、固体含量、"))
                 {
                     if (25 < Conversion.Val(sItem["HGLKZZ"]))
                     {
                         mItem["G_GTHL"] = Math.Round(Conversion.Val(sItem["HGLKZZ"]) * 0.95, 2).ToString("0.00") + "~" + Math.Round(Conversion.Val(sItem["HGLKZZ"]) * 1.05, 2).ToString("0.00");
                     }
                     else
                     {
                         mItem["G_GTHL"] = Math.Round(Conversion.Val(sItem["HGLKZZ"]) * 0.9, 2).ToString("0.00") + "~" + Math.Round(Conversion.Val(sItem["HGLKZZ"]) * 1.1, 2).ToString("0.00");
                     }

                     if (sItem["HGLKZZ"] == "----")
                     {
                         mItem["G_GTHL"] = "----";
                         mItem["HG_GTHL"] = "----";
                     }
                     else
                     {
                         mItem["HG_GTHL"] = IsQualified(mItem["G_GTHL"], sItem["GTHL"], true);
                     }

                     if (mItem["HG_GTHL"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["GTHL"] = "----";
                     mItem["HG_GTHL"] = "----";
                     mItem["G_GTHL"] = "----";
                 }

                 if (jcxm.Contains("、含水率、"))
                 {
                     if (25 < Conversion.Val(sItem["HSLKZZ"]))
                     {
                         mItem["G_HSL"] = Math.Round(Conversion.Val(sItem["HSLKZZ"]) * 0.9, 2).ToString("0.00") + "~" + Math.Round(Conversion.Val(sItem["HSLKZZ"]) * 1.1, 2).ToString("0.00");
                     }
                     else
                     {
                         mItem["G_HSL"] = Math.Round(Conversion.Val(sItem["HSLKZZ"]) * 0.8, 2).ToString("0.00") + "~" + Math.Round(Conversion.Val(sItem["HSLKZZ"]) * 1.2, 2).ToString("0.00");
                     }

                     if (sItem["HSLKZZ"] == "----")
                     {
                         mItem["G_HSL"] = "----";
                         mItem["HG_HSL"] = "----";
                     }
                     else
                     {
                         mItem["HG_HSL"] = IsQualified(mItem["G_HSL"], sItem["HSL"], true);
                     }

                     if (mItem["HG_HSL"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["HSL"] = "----";
                     mItem["G_HSL"] = "----";
                     mItem["HG_HSL"] = "----";
                 }

                 if (jcxm.Contains("、泌水率、"))
                 {
                     mItem["HG_MSL"] = IsQualified(mItem["G_MSL"], sItem["MSLB"], true);
                     if (mItem["HG_MSL"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["MSLB"] = "----";
                     mItem["G_MSL"] = "----";
                     mItem["HG_MSL"] = "----";
                 }

                 if (jcxm.Contains("、减水率、"))
                 {
                     mItem["HG_JSL"] = IsQualified(mItem["G_JSL"], sItem["PJJSL"], true);
                     if (mItem["HG_JSL"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["PJJSL"] = "----";
                     mItem["G_JSL"] = "----";
                     mItem["HG_JSL"] = "----";
                 }

                 if (jcxm.Contains("、含气量、"))
                 {
                     mItem["HG_HQL"] = IsQualified(mItem["G_HQL"], sItem["PJHQL"], true);
                     if (mItem["HG_HQL"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["PJHQL"] = "----";
                     mItem["G_HQL"] = "----";
                     mItem["HG_HQL"] = "----";
                 }

                 if (jcxm.Contains("、经1h后含气量变化量、"))
                 {
                     mItem["HG_HQLBHL"] = IsQualified(mItem["G_HQLBHL"], sItem["HQLBHL"], true);
                     if (mItem["HG_HQLBHL"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["HQLBHL"] = "----";
                     mItem["G_HQLBHL"] = "----";
                     mItem["HG_HQLBHL"] = "----";
                 }

                 if (jcxm.Contains("、经1h后坍落度变化量、"))
                 {
                     mItem["HG_TLD"] = IsQualified(mItem["G_TLD"], sItem["G_TLD"], true);
                     if (mItem["HG_TLD"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["G_TLD"] = "----";
                     mItem["G_TLD"] = "----";
                     mItem["HG_TLD"] = "----";
                 }

                 if (jcxm.Contains("、初凝时间差、"))
                 {
                     mItem["HG_CNSJC"] = IsQualified(mItem["G_CNSJC"], sItem["CNPJSJC"], true);
                     if (mItem["HG_CNSJC"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["CNPJSJC"] = "----";
                     mItem["G_CNSJC"] = "----";
                     mItem["HG_CNSJC"] = "----";
                 }

                 if (jcxm.Contains("、终凝时间差、"))
                 {
                     mItem["HG_ZNSJC"] = IsQualified(mItem["G_ZNSJC"], sItem["ZNPJSJC"], true);
                     if (mItem["HG_ZNSJC"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["ZNPJSJC"] = "----";
                     mItem["G_ZNSJC"] = "----";
                     mItem["HG_ZNSJC"] = "----";
                 }

                 if (jcxm.Contains("、相对耐久性、"))
                 {
                     mItem["HG_XDNJX"] = IsQualified(mItem["G_XDNJX"], sItem["XPJDTXML"], true);
                     if (mItem["HG_XDNJX"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["XPJDTXML"] = "----";
                     mItem["G_XDNJX"] = "----";
                     mItem["HG_XDNJX"] = "----";
                 }

                 if (jcxm.Contains("、1d抗压强度比、"))
                 {
                     mItem["HG_KYQD1D"] = IsQualified(mItem["G_KYQD1D"], sItem["PJQDB1D"], true);
                     if (mItem["HG_KYQD1D"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["PJQDB1D"] = "----";
                     mItem["G_KYQD1D"] = "----";
                     mItem["HG_KYQD1D"] = "----";
                 }

                 if (jcxm.Contains("、3d抗压强度比、"))
                 {
                     mItem["HG_KYQD3D"] = IsQualified(mItem["G_KYQD3D"], sItem["PJQDB3D"], true);
                     if (mItem["HG_KYQD3D"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["PJQDB3D"] = "----";
                     mItem["G_KYQD3D"] = "----";
                     mItem["HG_KYQD3D"] = "----";
                 }

                 if (jcxm.Contains("、7d抗压强度比、"))
                 {
                     mItem["HG_KYQD7D"] = IsQualified(mItem["G_KYQD7D"], sItem["PJQDB7D"], true);
                     if (mItem["HG_KYQD7D"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["PJQDB7D"] = "----";
                     mItem["G_KYQD7D"] = "----";
                     mItem["HG_KYQD7D"] = "----";
                 }

                 if (jcxm.Contains("、28d抗压强度比、"))
                 {
                     mItem["HG_KYQD28D"] = IsQualified(mItem["G_KYQD28D"], sItem["PJQDB28D"], true);
                     if (mItem["HG_KYQD28D"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["PJQDB28D"] = "----";
                     mItem["G_KYQD28D"] = "----";
                     mItem["HG_KYQD28D"] = "----";
                 }

                 if (jcxm.Contains("、收缩率比、"))
                 {
                     mItem["HG_SSLB"] = IsQualified(mItem["G_SSLB"], sItem["SSLB"], true);
                     if (mItem["HG_KYQD28D"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["SSLB"] = "----";
                     mItem["G_SSLB"] = "----";
                     mItem["HG_SSLB"] = "----";
                 }

                 if (jcxm.Contains("、PH值、"))
                 {
                     if (sItem["PHKZZ"] == "----")
                     {
                         mItem["G_PH"] = "----";
                     }
                     else
                     {
                         mItem["HG_PH"] = IsQualified(sItem["PHKZZ"], sItem["PH"], true);
                     }

                     if (mItem["HG_PH"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["PH"] = "----";
                     mItem["G_PH"] = "----";
                     mItem["HG_PH"] = "----";
                 }

                 if (jcxm.Contains("、氯离子含量、"))
                 {
                     if (sItem["LLZKZZ"] == "----")
                     {
                         mItem["HG_LLZHL"] = "----";
                     }
                     else
                     {
                         mItem["HG_LLZHL"] = IsQualified(sItem["LLZKZZ"], sItem["LLZHL"], true);
                     }

                     if (mItem["HG_LLZHL"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["LLZHL"] = "----";
                     mItem["HG_LLZHL"] = "----";
                 }

                 if (jcxm.Contains("、总碱量、"))
                 {
                     if (sItem["ZJLKZZ"] == "----")
                     {
                         mItem["HG_ZJL"] = "----";
                     }
                     else
                     {
                         mItem["HG_ZJL"] = IsQualified(sItem["ZJLKZZ"], sItem["ZJL"], true);
                     }

                     if (mItem["HG_ZJL"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["ZJL"] = "----";
                     mItem["HG_ZJL"] = "----";
                     mItem["G_ZJL"] = "----";
                 }

                 if (jcxm.Contains("、硫酸钠含量、"))
                 {
                     if (sItem["LSNHLKZZ"] == "----")
                     {
                         mItem["HG_LSNHL"] = "----";
                     }
                     else
                     {
                         mItem["HG_LSNHL"] = IsQualified(sItem["LSNHLKZZ"], sItem["LSNHL"], true);
                     }

                     if (mItem["HG_LSNHL"] == "不符合")
                     {
                         mFlag_Bhg = true;
                         mbhggs += 1;
                     }
                     else
                     {
                         itemHg = true;
                     }
                 }
                 else
                 {
                     sItem["LSNHL_1"] = "----";
                     sItem["LSNHL_2"] = "----";
                     sItem["LSNHL"] = "----";
                     mItem["HG_LSNHL"] = "----";
                 }

                 if (bool.Parse(sItem["SFFJ"]))
                 {
                     if (mbhggs > 0)
                     {
                         mAllHg = false;
                         sItem["JCJG"] = "不合格";
                         jsbeizhu = "该组试样不符合" + mItem["PDBZ"] + "标准要求。";

                         if (itemHg && mFlag_Bhg)
                         {
                             jsbeizhu = "该组试样所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                         }
                     }
                     else
                     {
                         mAllHg = true;
                         sItem["JCJG"] = "合格";
                         jsbeizhu = "该组试样所检项目全部符合" + mItem["PDBZ"] + "标准要求。";
                     }
                 }
                 else
                 {
                     //sitem["ybgbh = ""
                     if (mbhggs > 0)
                     {
                         mAllHg = false;
                         sItem["JCJG"] = "不合格";
                         jsbeizhu = "该组试样不符合" + mItem["PDBZ"] + "标准要求。";

                         if (itemHg && mFlag_Bhg)
                         {
                             jsbeizhu = "该组试样所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                         }
                     }
                     else
                     {
                         mAllHg = true;
                         sItem["JCJG"] = "合格";
                         jsbeizhu = "该组试样所检项目全部符合" + mItem["PDBZ"] + "标准要求。";
                     }
                 }

                 return mAllHg;
             };

            //遍历从表数据
            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mSjdj = sItem["WJJMC"];//设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                {
                    mSjdj = "";
                }

                #region
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj);

                if (null == mrsDj)
                {
                    jsbeizhu = "试件尺寸为空\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }

                MItem[0]["G_XD"] = mrsDj["XD"];
                MItem[0]["G_MD"] = mrsDj["MD"];
                MItem[0]["G_MSL"] = mrsDj["MSL"];
                MItem[0]["G_JSL"] = mrsDj["JSL"];
                MItem[0]["G_GTHL"] = mrsDj["GTHL"];
                MItem[0]["G_CNSJC"] = mrsDj["CNSJC"];
                MItem[0]["G_ZNSJC"] = mrsDj["ZNSJC"];
                MItem[0]["G_TLD"] = mrsDj["TLD"];
                MItem[0]["G_HQLBHL"] = mrsDj["HQLBHL"];
                MItem[0]["G_HQL"] = mrsDj["HQL"];
                MItem[0]["G_KYQD1D"] = mrsDj["KYQDB1D"];
                MItem[0]["G_KYQD3D"] = mrsDj["KYQDB3D"];
                MItem[0]["G_KYQD7D"] = mrsDj["KYQDB7D"];
                MItem[0]["G_KYQD28D"] = mrsDj["KYQDB28D"];
                MItem[0]["G_XDNJX"] = mrsDj["XDNJX"];
                MItem[0]["G_PH"] = mrsDj["PH"];
                MItem[0]["G_LLZHL"] = mrsDj["LLZHL"];
                MItem[0]["G_SSLB"] = mrsDj["SSLB28D"];

                mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].ToLower();
                #endregion
                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    sjtabcalc(MItem[0], sItem);

                }
                else
                {

                }
            }

            #region 添加最终报告

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
