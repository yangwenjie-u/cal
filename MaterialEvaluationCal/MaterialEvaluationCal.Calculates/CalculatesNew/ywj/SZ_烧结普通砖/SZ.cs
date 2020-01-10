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

            double mYqpjz, mXdy21, mDy21 = 0;
            bool sign = false;
            double md1, md2, md, sum, Gs, pjmd = 0;
            int xd = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            List<double> nArr = new List<double>();


            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
                 delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
                 {

                     mbhggs = 0;
                     jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                     mFlag_Hg = true;
                     sign = true;
                     if (jcxm.Contains("、抗压、"))
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
                             sItem["kypj"] = pjmd.ToString("0.00");
                             sum = 0;
                             for (xd = 0; xd < 10; xd++)
                             {
                                 md = nArr[xd] - pjmd;
                                 sum = sum + Math.Pow(md, 2);
                             }
                             md1 = Math.Sqrt(sum / 9);
                             md1 = Math.Round(md1, 2);
                             sItem["bzc"] = md1.ToString("0.00");

                             //If mItem["jydbh >= "181100001" Then
                             //mItem["which = 1

                             sItem["qdyq"] = "抗压强度平均值≥" + Double.Parse(mItem["G_PJZ"]).ToString("0") + "MPa。强度标准值≥" + Double.Parse(mItem["G_BZZ"]).ToString("0.0") + "MPa。";
                             md2 = md1 / pjmd;
                             md2 = Math.Round(md2, 2);
                             sItem["BYXS"] = md2.ToString("0.00");

                             nArr.Sort();
                             sItem["qdmin"] = Math.Round(nArr[0], 1).ToString("0.0");


                             md2 = pjmd - 1.83 * md1;
                             md2 = Math.Round(md2, 1);
                             sItem["bzz"] = md2.ToString("0.0");


                             sign = IsQualified("≥" + Double.Parse(mItem["G_PJZ"]).ToString("0"), sItem["kypj"]) == "合格" ? sign : false;
                             sign = IsQualified("≥" + Double.Parse(mItem["G_BZZ"]).ToString("0"), sItem["bzz"]) == "合格" ? sign : false;

                             sItem["qdpd"] = sign ? "合格" : "不合格";

                             if (sItem["qdpd"] == "不合格")
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
                         sItem["kypj"] = "----";
                         sItem["qdpd"] = "----";
                         sItem["bzc"] = "----";
                         sItem["BYXS"] = "----";
                         sItem["qdmin"] = "----";
                         sItem["bzz"] = "----";
                         sItem["qdyq"] = "----";
                         for (xd = 1; xd < 11; xd++)
                         {
                             sItem["KYQD" + xd] = "----";
                         }
                     }

                     if (jcxm.Contains("、冻融、"))
                     {
                         sign = true;
                         for (xd = 1; xd < 6; xd++)
                         {
                             sign = IsNumeric(sItem["drgm" + xd]) ? sign : false;
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
                                 sItem["drwg" + xd] = sign ? "0" : "1";
                                 md = Double.Parse(sItem["drgm" + xd]);
                                 sign = md <= 2.0 ? sign : false;


                             }
                             sItem["drpd"] = sign ? "合格" : "不合格";


                             if (sItem["drpd"] == "不合格")
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
                         for (xd = 1; xd < 6; xd++)
                         {
                             sItem["drwg" + xd] = "----";
                             sItem["drgm" + xd] = "----";
                         }
                         sItem["drpd"] = "----";

                     }

                     if (jcxm.Contains("、吸水率和饱和系数、"))
                     {
                         sign = true;
                         sign = IsNumeric(sItem["XSLPJZ"]) ? sign : false;
                         sign = IsNumeric(sItem["BHXSPJZ"]) ? sign : false;

                         if (sign)
                         {
                             sItem["xslbhxsyq"] = "5h沸煮吸水率平均值需" + sItem["XSLPJZYQ"].Trim() + " %，单块最大值需" + sItem["XSLZDZYQ"].Trim() + " %；饱和系数平均值需" + sItem["BHXSPJZYQ"].Trim() + "，单块最大值需" + sItem["BHXSZDZYQ"].Trim() + "。";

                             sign = IsQualified(sItem["XSLPJZYQ"], sItem["XSLPJZ"], false) == "合格" ? sign : false;
                             sign = IsQualified(sItem["XSLZDZYQ"], sItem["XSLDKZD"], false) == "合格" ? sign : false;
                             sign = IsQualified(sItem["BHXSPJZYQ"], sItem["BHXSPJZ"], false) == "合格" ? sign : false;
                             sign = IsQualified(sItem["BHXSZDZYQ"], sItem["BHXSZDZ"], false) == "合格" ? sign : false;
                             sItem["bhxspd"] = sign ? "合格" : "不合格";


                             if (sItem["drpd"] == "不合格")
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
                         sItem["bhxspd"] = "----";
                         sItem["XSLPJZ"] = "----";
                         sItem["XSLDKZD"] = "----";
                         sItem["BHXSPJZ"] = "----";
                         sItem["BHXSZDZ"] = "----";
                         sItem["xslbhxsyq"] = "----";
                     }

                     if (jcxm.Contains("、泛霜、"))
                     {
                         sign = true;

                         for (xd = 0; xd < 6; xd++)
                         {
                             sign = IsNumeric(sItem["fscd" + xd]) ? sign : false;
                             if (!sign)
                             {
                                 break;
                             }
                         }
                         if (sign)
                         {
                             sItem["fspd"] = "合格";

                             for (xd = 0; xd < 6; xd++)
                             {
                                 switch (sItem["fscd" + xd].Trim())
                                 {
                                     case "轻度":
                                         sItem["fspd"] = sItem["wgdj"].Trim() == "优等品" ? "不合格" : sItem["fspd"];
                                         break;
                                     case "中等":
                                         sItem["fspd"] = sItem["wgdj"].Trim() == "优等品" ? "不合格" : sItem["fspd"];
                                         sItem["fspd"] = sItem["wgdj"].Trim() == "一等品" ? "不合格" : sItem["fspd"];
                                         break;
                                     case "严重":
                                         sItem["fspd"] = "不合格";
                                         break;
                                 }
                             }
                             sItem["fspd"] = sign ? "合格" : "不合格";

                             if (sItem["drpd"] == "不合格")
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
                             sItem["fscd" + xd] = "----";
                         }
                         sItem["fspd"] = "----";
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
                             sItem["shblpd"] = "合格";

                             for (xd = 0; xd < 6; xd++)
                             {
                                 switch (sItem["wgdj"].Trim())
                                 {
                                     case "优等品":
                                         sItem["fspd"] = Conversion.Val(sItem["BLDS10_" + xd]) + Conversion.Val(sItem["BLDS15_" + xd]) + Conversion.Val(sItem["BLDS16_" + xd]) > 0 ? "不合格" : sItem["shblpd"];
                                         break;
                                     case "一等品":
                                         sItem["fspd"] = Conversion.Val(sItem["BLDS10_" + xd]) > 15 || (Conversion.Val(sItem["BLDS15_" + xd]) + Conversion.Val(sItem["BLDS16_" + xd])) > 0 ? "不合格" : sItem["shblpd"];

                                         break;
                                     case "合格品":
                                         sItem["fspd"] = (Conversion.Val(sItem["BLDS10_" + xd]) + Conversion.Val(sItem["BLDS15_" + xd])) > 15 || Conversion.Val(sItem["BLDS16_" + xd]) > 0 ? "不合格" : sItem["shblpd"];

                                         break;
                                 }
                             }
                             sItem["fspd"] = sign ? "合格" : "不合格";

                             if (sItem["drpd"] == "不合格")
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
                         sItem["shblpd"] = "----";
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
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj.Trim() && u["zlb"].Contains("zlb"));
                if (mrsDj == null)
                {
                    mYqpjz = 0;
                    mXdy21 = 0;
                    mDy21 = 0;
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    return;
                }
                else
                {
                    mYqpjz = Double.Parse(mrsDj["PJZ"]);
                    mXdy21 = Double.Parse(mrsDj["XDY21"]);
                    mDy21 = Double.Parse(mrsDj["DY21"]);
                    MItem[0]["G_PJZ"] = (mYqpjz).ToString();
                    MItem[0]["G_BZZ"] = (mXdy21).ToString();
                    MItem[0]["G_MIN"] = (mDy21).ToString();
                    //MItem[0]["which"] = mrsDj["which"];
                    MItem[0]["bgname"] = mrsDj["bgname"].ToString();
                    mJSFF = string.IsNullOrEmpty(mrsDj["jsff"]) ? "" : mrsDj["jsff"].ToLower();
                }

                var mrsKfhdj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["ZZL"] && u["PZ"] == sItem["wgdj"]);
                if (mrsDj == null)
                {
                    sItem["dryq"] = "每块砖样不允许出现裂纹、分层、掉皮、缺棱、掉角等冻坏现象；质量损失不得大于2%。";
                }
                else
                {
                    sItem["XSLPJZYQ"] = mrsKfhdj["xslpj"];
                    sItem["XSLZDZYQ"] = mrsKfhdj["XSLDKZD"];
                    sItem["BHXSPJZYQ"] = mrsKfhdj["BHXSPJ"];
                    sItem["BHXSZDZYQ"] = mrsKfhdj["BHXSzdz"];
                    sItem["FSYQ"] = mrsKfhdj["FSYQ"];
                    sItem["SHBLYQ"] = mrsKfhdj["DHBLYQ"];
                    sItem["DRYQ"] = mrsKfhdj["DRYQ"];
                }
                sItem["lq"] = (DateTime.Parse(MItem[0]["syrq"]) - DateTime.Parse(sItem["zzrq"])).Days.ToString();


                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    mAllHg = sjtabcalc(MItem[0], sItem);
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
        }
    }
}

