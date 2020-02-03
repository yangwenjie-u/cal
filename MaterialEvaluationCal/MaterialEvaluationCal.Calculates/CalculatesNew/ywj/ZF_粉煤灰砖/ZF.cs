using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZF : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_ZF_DJ"];


            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_ZF"];

            if (!data.ContainsKey("M_ZF"))
            {
                data["M_ZF"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_ZF"];

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
                     if (jcxm.Contains("、抗压强度、"))
                     {
                         sign = true;

                         for (xd = 1; xd < 11; xd++)
                         {
                             sign = IsNumeric(sItem["QD_KYQD" + xd]) ? sign : false;
                             if (!sign)
                             {
                                 break;
                             }
                         }
                         if (sign)
                         {
                             var GH_KYAVG = IsQualified(sItem["GH_QD_KYAVG"], sItem["QD_KYAVG"], true);
                             var GH_KYDKZ = IsQualified(sItem["GH_QD_KYDKZ"], sItem["QD_KYMIN"], true);
                             if (GH_KYAVG == "不符合" || GH_KYDKZ == "不符合")
                             {
                                 sItem["PD_KYQD"] = "不合格";
                                 mbhggs = mbhggs + 1;
                                 mFlag_Bhg = false;
                             }
                             else
                             {
                                 sItem["PD_KYQD"] = "合格";
                                 mFlag_Hg = true;
                             }
                         }

                     }
                     else
                     {
                         sItem["PD_KYQD"] = "----";
                         sItem["GH_QD_KYAVG"] = "----";
                         sItem["GH_QD_KYDKZ"] = "----";
                         sItem["QD_KYAVG"] = "----";
                         sItem["QD_KYMIN"] = "----";
                     }


                     if (jcxm.Contains("、抗折强度、"))
                     {
                         sign = true;

                         for (xd = 1; xd < 11; xd++)
                         {
                             sign = IsNumeric(sItem["QD_KZQD" + xd]) ? sign : false;
                             if (!sign)
                             {
                                 break;
                             }
                         }
                         if (sign)
                         {
                             var GH_KYAVG = IsQualified(sItem["GH_QD_KZAVG"], sItem["QD_KZAVG"], true);
                             var GH_KYDKZ = IsQualified(sItem["GH_QD_KZDKZ"], sItem["QD_KZMIN"], true);
                             if (GH_KYAVG == "不符合" || GH_KYDKZ == "不符合")
                             {
                                 sItem["PD_KZQD"] = "不合格";
                                 mbhggs = mbhggs + 1;
                                 mFlag_Bhg = false;
                             }
                             else
                             {
                                 sItem["PD_KZQD"] = "合格";
                                 mFlag_Hg = true;
                             }
                         }

                     }
                     else
                     {
                         sItem["PD_KZQD"] = "----";
                         sItem["GH_QD_KZAVG"] = "----";
                         sItem["GH_QD_KZDKZ"] = "----";
                         sItem["QD_KZAVG"] = "----";
                         sItem["QD_KZMIN"] = "----";
                     }


                     if (jcxm.Contains("抗压强度") || jcxm.Contains("抗折强度"))
                     {
                         sItem["QDJL"] = sItem["PD_KYQD"] == "不合格" || sItem["PD_KZQD"] == "不合格" ? "不符合" : "符合";
                         sItem["QDJL"] = sItem["QDJL"] + sItem["sjdj"] + "强度等级";

                     }
                     else
                     {
                         sItem["QDJL"] = "----";
                     }

                     if (jcxm.Contains("、抗冻性能、"))
                     {
                         //if (jcxm.Contains("、冻后强度、"))
                         //{
                             sign = true;

                             sign = IsNumeric(sItem["KD_KYAVG"]) ? sign : false;
                             if (sign)
                             {
                                 var GH_KYAVG = IsQualified(sItem["GH_KD_KYAVG"], sItem["KD_KYAVG"], true);
                                 if (GH_KYAVG == "不符合")
                                 {
                                     sItem["PD_DHQD"] = "不合格";
                                     mbhggs = mbhggs + 1;
                                     mFlag_Bhg = false;
                                 }
                                 else
                                 {
                                     sItem["PD_DHQD"] = "合格";
                                     mFlag_Hg = true;
                                 }
                                 sItem["GH_KD_KYAVG"] = "平均值" + sItem["GH_KD_KYAVG"];
                             }

                         //}
                         //else
                         //{
                         //    sItem["PD_DHQD"] = "----";
                         //    sItem["GH_KD_KYAVG"] = "----";
                         //    sItem["KD_KYAVG"] = "----";
                         //}


                         //if (jcxm.Contains("、质量损失率、"))
                         //{
                             sign = true;

                             sign = IsNumeric(sItem["KD_SSMAX"]) ? sign : false;
                             if (sign)
                             {
                                 var GH_KYAVG = IsQualified(sItem["GH_KD_SSDKZ"], sItem["KD_SSMAX"], true);
                                 if (GH_KYAVG == "不符合")
                                 {
                                     sItem["PD_ZLSS"] = "不合格";
                                     mbhggs = mbhggs + 1;
                                     mFlag_Bhg = false;
                                 }
                                 else
                                 {
                                     sItem["PD_ZLSS"] = "合格";
                                     mFlag_Hg = true;
                                 }
                                 sItem["GH_KD_SSDKZ"] = "单块最大损失率" + sItem["GH_KD_SSDKZ"];
                             }
                         //}
                         //else
                         //{
                         //    sItem["PD_ZLSS"] = "----";
                         //    sItem["GH_KD_SSDKZ"] = "----";
                         //    sItem["KD_SSMAX"] = "----";
                         //}


                         //if (jcxm.Contains("冻后强度") || jcxm.Contains("质量损失率"))
                         //{
                             sItem["KDXJL"] = sItem["PD_DHQD"] == "不合格" || sItem["PD_ZLSS"] == "不合格" ? "不符合" : "符合";
                             sItem["KDXJL"] = sItem["KDXJL"] + sItem["sjdj"] + "强度等级";

                         //}
                         //else
                         //{
                         //    sItem["KDXJL"] = "----";
                         //}
                     }

                     if (mbhggs == 0)
                     {
                         jsbeizhu = "该组试样所检项目符合" + mItem["PDBZ"] + "标准要求。";
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

                Gs = extraDJ.Count;

                for (xd = 0; xd < Gs + 1; xd++)
                {
                    if (sItem["sjdj"] == extraDJ[xd]["MC"])
                    {
                        sItem["GH_QD_KYAVG"] = extraDJ[xd]["KYAVG_QD"];
                        sItem["GH_QD_KYDKZ"] = extraDJ[xd]["KYDKZ_QD"];
                        sItem["GH_QD_KZAVG"] = extraDJ[xd]["KZAVG_QD"];
                        sItem["GH_QD_KZDKZ"] = extraDJ[xd]["KZDKZ_QD"];
                        sItem["GH_KD_KYAVG"] = extraDJ[xd]["KYAVG_KD"];
                        sItem["GH_KD_SSDKZ"] = extraDJ[xd]["SSDKZ_KD"];
                        break;
                    }
                }
                xd = xd + 1;
                if (xd > Gs)
                {
                    continue;
                }

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

