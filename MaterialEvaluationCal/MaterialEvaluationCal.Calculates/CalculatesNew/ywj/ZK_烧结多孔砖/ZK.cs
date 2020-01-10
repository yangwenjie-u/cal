using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZK : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_ZK_DJ"];
            var extraWCDj = dataExtra["ZKWCDJ"];
            var extraKFHDJ = dataExtra["ZKKFHDJ"];
            var extraGMDJB = dataExtra["ZKGMDJB "];


            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_ZK"];

            if (!data.ContainsKey("M_ZK"))
            {
                data["M_ZK"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_ZK"];

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

                     if (jcxm.Contains("、抗压、"))
                     {
                         sign = true;

                         for (xd = 1; xd < 11; xd++)
                         {
                             sign = IsNumeric(sItem["KYQD" + xd]) && !string.IsNullOrEmpty(sItem["KYQD" + xd]) ? sign : false;

                             if (!sign)
                             {
                                 break;
                             }
                         }

                         if (!sign)
                         {
                             mAllHg = false;
                             return false;
                         }
                         sum = 0;
                         nArr.Add(0);
                         for (xd = 1; xd < 11; xd++)
                         {
                             md = double.Parse(sItem["KYQD" + xd]);
                             nArr.Add(md);
                             sum += md;
                         }
                         pjmd = Math.Round(sum / 10, 1);
                         sItem["kypj"] = pjmd.ToString("0.0");

                         sum = 0;
                         for (xd = 1; xd < 11; xd++)
                         {
                             md = nArr[xd] - pjmd;
                             nArr.Add(md);
                             sum += Math.Pow(md, 2);
                         }

                         md1 = Math.Sqrt(sum / 9);
                         md1 = Math.Round(md1, 2);

                         sItem["bzc"] = md1.ToString("0.00");
                         sItem["qdyq"] = "抗压强度平均值需≥" + string.Format(MItem[0]["G_PJZ"], 0) + "MPa。强度标准值需≥" + string.Format(MItem[0]["G_BZZ"], "0.0") + "MPa。";


                         md2 = md1 / pjmd;
                         md2 = Math.Round(md2, 2);
                         sItem["BYXS"] = md2.ToString("0.00");

                         nArr.Sort();

                         sItem["qdmin"] = nArr[0].ToString("0.00");


                         md2 = Math.Round(pjmd - 1.83 * md1, 1);
                         sItem["bzz"] = md2.ToString("0.0");

                         sign = IsQualified("≥" + double.Parse(MItem[0]["G_PJZ"]).ToString("0"), sItem["kypj"], false) == "合格" ? sign : false;
                         sign = IsQualified("≥" + double.Parse(MItem[0]["G_BZZ"]).ToString("0.0"), sItem["bzz"], false) == "合格" ? sign : false;

                         sItem["qdpd"] = sign ? "合格" : "不合格";

                         if (sItem["qdpd"] == "合格")
                         {
                             mFlag_Hg = true;
                         }
                         else
                         {
                             mFlag_Bhg = true;
                             mbhggs++;
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

                         for (int i = 1; i < 11; i++)
                         {
                             sItem["KYQD" + i] = "----";
                         }
                     }

                     if (jcxm.Contains("、干密度、"))
                     {
                         sign = true;

                         for (xd = 1; xd < 4; xd++)
                         {
                             sign = IsNumeric(sItem["gmd" + xd]) && !string.IsNullOrEmpty(sItem["gmd" + xd]) ? sign : false;

                             if (!sign)
                             {
                                 break;
                             }
                         }
                         if (!sign)
                         {
                             mAllHg = false;
                             return false;
                         }
                         sum = 0;
                         for (xd = 1; xd < 4; xd++)
                         {
                             md = double.Parse(sItem["gmd" + xd]);
                             sum += md;
                         }

                         pjmd = Math.Round(sum / 3, 0);
                         sItem["gmdpj"] = pjmd.ToString("0");
                         sItem["gmdpd"] = IsQualified(MItem[0]["g_gmd"], sItem["gmdpj"], false);

                         if (sItem["gmdpd"] == "合格")
                         {
                             mFlag_Hg = true;
                         }
                         else
                         {
                             mFlag_Bhg = true;
                             mbhggs++;
                         }
                     }
                     else
                     {
                         mItem["g_gmd"] = "----";
                         sItem["gmdpd"] = "----";
                         for (int i = 1; i < 4; i++)
                         {
                             sItem["gmd" + i] = "----";
                         }
                         sItem["gmdpj"] = "----";
                     }


                     if (jcxm.Contains("、冻融、"))
                     {
                         sign = true;

                         for (xd = 1; xd <= 5; xd++)
                         {
                             sign = sItem["SFDH" + xd] == "否" ? sign : false;
                             sItem["drwg" + xd] = sItem["SFDH" + xd] == "否" ? "0" : "1";
                         }

                         sItem["drpd"] = sign ? "合格" : "不合格";
                         if (sItem["drpd"] == "合格")
                         {
                             mFlag_Hg = true;
                         }
                         else
                         {
                             mFlag_Bhg = true;
                             mbhggs++;
                         }
                     }
                     else
                     {
                         sItem["drpd"] = "----";
                         for (int i = 1; i <=5; i++)
                         {
                             sItem["drwg" + i] = "----";
                         }
                     }
                     sItem["bhxspd"] = "----";
                     sItem["fspd"] = "----";
                     sItem["shblpd"] = "----";

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


                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj.Trim() && u["pdbz"].Contains("13544-2000"));

                // 从设计等级表中取得相应的计算数值、等级标准
                if (MItem[0]["pdbz"].Trim().Contains("13544 - 2011"))
                {
                    mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj.Trim() && u["pdbz"].Contains("13544-2011%"));
                }


                if (mrsDj != null)
                {
                    mYqpjz = double.Parse(mrsDj["PJZ"]);
                    mXdy21 = double.Parse(mrsDj["XDY21"]);
                    mDy21 = double.Parse(mrsDj["DY21"]);
                    MItem[0]["G_PJZ"] = (mYqpjz).ToString();
                    MItem[0]["G_BZZ"] = (mXdy21).ToString();
                    MItem[0]["G_MIN"] = (mDy21).ToString();
                    //MItem[0]["which = mrsDj["which
                    //MItem[0]["bgname = Trim(mrsDj["bgname)
                    mJSFF = string.IsNullOrEmpty(mrsDj["jsff"]) ? "" : mrsDj["jsff"];
                }
                else
                {
                    mYqpjz = 0;
                    mXdy21 = 0;
                    mDy21 = 0;
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "试件尺寸为空";
                    mAllHg = false;
                }

                var mrskfhDj = extraWCDj.FirstOrDefault(u => u["MC"] == sItem["ZZL"].Trim() && u["PZ"] == sItem["wgdj"].Trim());
                if (mrskfhDj != null)
                {
                    sItem["XSLPJZYQ"] = mrskfhDj["xslpj"];
                    sItem["XSLZDZYQ"] = mrskfhDj["XSLDKZD"];
                    sItem["BHXSPJZYQ"] = mrskfhDj["BHXSPJ"];
                    sItem["BHXSZDZYQ"] = mrskfhDj["BHXSzdz"];
                    sItem["FSYQ"] = mrskfhDj["FSYQ"];
                    sItem["SHBLYQ"] = mrskfhDj["DHBLYQ"];
                    sItem["DRYQ"] = mrskfhDj["DRYQ"];
                }

                var mrsgmdjb = extraGMDJB.FirstOrDefault(u => u["MC"] == sItem["gmddj"].Trim());
                if (mrskfhDj != null)
                {
                    MItem[0]["g_gmd"] = mrsgmdjb["gmd"];
                }

                sItem["lq"] = (Convert.ToDateTime(MItem[0]["syrq"]) - Convert.ToDateTime(sItem["zzrq"])).Days.ToString();





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

