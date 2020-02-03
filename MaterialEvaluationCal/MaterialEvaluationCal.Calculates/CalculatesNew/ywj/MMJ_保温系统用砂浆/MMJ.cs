using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class MMJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_MMJ_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_MMJ"];

            if (!data.ContainsKey("M_MMJ"))
            {
                data["M_MMJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_MMJ"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";

            #region 局部函数
            Func<double, double> myint = delegate (double dataChar)
            {
                return Math.Round(Conversion.Val(dataChar) / 5, 0) * 5;
            };
            #endregion


            double g_Qd1, g_Qd2, g_Qd3, g_Qd4 = 0;
            bool sign = true;
            bool sign1 = true;

            bool flag = true;
            bool mSFwc = true;
            List<double> nArr = new List<double>();
            double xd, Gs = 0;
            var mJSFF = "";
            var mbhggs = 0;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
             delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
             {
                 jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                 mbhggs = 0;
                 mFlag_Hg = false;
                 mFlag_Bhg = false;


                 sign = false;
                 sign1 = mItem["YCQK"].Contains("破坏") ? false : true;

                 if (jcxm.Contains("、拉伸粘结强度(干燥状态)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD1"]) == 0)
                     {
                         return false;
                     }
                     var PHJM1 = sItem["PHJM1"].Trim();
                     if (PHJM1 == "EPS板破坏")
                     {
                         mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);
                     }
                     else
                     {
                         mItem["HG_QD1"] = "不合格";
                     }

                 }

                 if (jcxm.Contains("、压剪粘结强度(原强度)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD1"]) == 0)
                     {
                         return false;
                     }

                     mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);

                 }

                 //if (jcxm.Contains("、拉伸粘结强度(原强度)、") && sign1)
                 if (jcxm.Contains("、拉伸粘结原强度、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD1"]) == 0)
                     {
                         return false;
                     }

                     var PHJM1 = sItem["PHJM1"].Trim();
                     if (PHJM1 == "EPS板破坏")
                     {
                         mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);
                     }
                     else
                     {
                         mItem["HG_QD1"] = "不合格";
                     }

                     mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);

                 }

                 if (jcxm.Contains("、拉伸粘结强度(常温28d)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD1"]) == 0)
                     {
                         return false;
                     }

                     mItem["HG_QD1"] = IsQualified(mItem["G_QD1"], sItem["QD1"], false);
                 }

                 //mItem[0]["WHICH") = IIf(sign1, "bgmmj", "bgmmj_10")
                 if (sign)
                 {
                     mbhggs = mItem["HG_QD1"] == "不合格" ? mbhggs + 1 : mbhggs;
                 }
                 else
                 {
                     if (sign1)
                     {
                         sItem["QD1"] = "----";
                         sItem["PHJM1"] = "----";
                         sItem["PHJMPD1"] = "----";
                         mItem["HG_QD1"] = "----";
                         mItem["G_QD1"] = "----";
                         mItem["G_PHJM1"] = "----";
                     }
                 }

                 if (!sign1)
                 {
                     mItem["HG_QD1"] = "不合格";
                     mbhggs = mbhggs + 1;
                 }

                 //mItem[0]["YCQK") Like "*破坏*", false, true)
                 sign = false;
                 sign1 = mItem["YCQK"].Contains("破坏") ? false : true;

                 if (jcxm.Contains("、拉伸粘结强度(浸水48h后)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD2"]) == 0)
                     {
                         return false;
                     }
                     else
                     {

                         if ("EPS板破坏" == sItem["PHJM2"])
                         {
                             mItem["HG_QD2"] = IsQualified(mItem["G_QD2"], sItem["QD2"], false);
                         }
                         else
                         {
                             mItem["HG_QD2"] = "不合格";
                         }
                     }
                 }


                 //if (jcxm.Contains("、拉伸粘结强度(耐水)、") && sign1)
                 if (jcxm.Contains("、拉伸粘结耐水强度、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD2"]) == 0)
                     {
                         return false;
                     }
                     else
                     {
                         mItem["HG_QD2"] = IsQualified(mItem["G_QD2"], sItem["QD2"], false);
                     }
                 }

                 //if (jcxm.Contains("、拉伸粘结强度(耐水)、") && sign1)
                 if (jcxm.Contains("、拉伸粘结耐水强度、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD2"]) == 0)
                     {
                         return false;
                     }
                     else
                     {
                         if (sItem["PHJM2"] == "EPS板破坏")
                         {
                             mItem["HG_QD2"] = IsQualified(mItem["G_QD2"], sItem["QD2"], false);
                         }
                         else
                         {
                             mItem["HG_QD2"] = "不合格";
                         }
                     }
                 }

                 if (jcxm.Contains("、浸水拉伸粘结强度(常温28d，浸水7d)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD2"]) == 0)
                     {
                         return false;
                     }
                     else
                     {
                         mItem["HG_QD2"] = IsQualified(mItem["G_QD2"], sItem["QD2"], false);
                     }
                 }
                 //mItem[0]["WHICH") = IIf(sign1, "bgmmj", "bgmmj_10")

                 if (sign)
                 {
                     mbhggs = mItem["HG_QD2"] == "不合格" ? mbhggs + 1 : mbhggs;
                 }
                 else
                 {
                     if (sign1)
                     {
                         sItem["QD2"] = "----";
                         sItem["PHJM2"] = "----";
                         sItem["PHJMPD2"] = "----";
                         mItem["HG_QD2"] = "----";
                         mItem["G_QD2"] = "----";
                         mItem["G_PHJM2"] = "----";
                     }
                 }

                 if (!sign1)
                 {
                     mItem["HG_QD2"] = "不合格";
                     mbhggs = mbhggs + 1;
                 }


                 sign = false;
                 if (jcxm.Contains("、压剪粘结强度(耐冻融)、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD3"]) == 0)
                     {
                         return false;
                     }

                     mItem["HG_QD3"] = IsQualified(mItem["G_QD3"], sItem["QD3"], false);
                 }

                 //if (jcxm.Contains("、拉伸粘结强度(耐冻融)、") && sign1)
                 if (jcxm.Contains("、拉伸粘结耐冻融强度、") && sign1)
                 {
                     sign = true;
                     if (Conversion.Val(sItem["QD3"]) == 0)
                     {
                         return false;
                     }

                     if (sItem["PHJM3"] == "EPS板破坏")
                     {
                         mItem["HG_QD3"] = IsQualified(mItem["G_QD3"], sItem["QD3"], false);
                     }
                     else
                     {
                         mItem["HG_QD3"] = "不合格";
                     }
                 }

                 if (sign)
                 {
                     mbhggs = mItem["HG_QD3"] == "不合格" ? mbhggs + 1 : mbhggs;
                 }
                 else
                 {
                     sItem["QD3"] = "----";
                     sItem["PHJM3"] = "----";
                     sItem["PHJMPD3"] = "----";
                     mItem["HG_QD3"] = "----";
                     mItem["G_QD3"] = "----";
                     mItem["G_PHJM3"] = "----";
                 }



                 if (mbhggs == 0)
                 {
                     jsbeizhu = "该组试件所检项目符合" + mItem["PDBZ"] + "标准要求。";
                     sItem["JCJG"] = "合格";
                     mAllHg = true;
                 }

                 if (mbhggs > 0)
                 {
                     sItem["JCJG"] = "不合格";
                     mAllHg = false;
                     jsbeizhu = "冻融中试件破坏，该组试件不符合" + mItem["PDBZ"] + "标准要求。";
                 }

                 return mAllHg;
             };


            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["GGXH"] && u["JCYJ"] == sItem["JCBZH"]);

                if (null == mrsDj)
                {
                    mJSFF = "";
                    jsbeizhu = "依据不详\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                g_Qd1 = Conversion.Val(mrsDj["QD1"]);
                g_Qd2 = Conversion.Val(mrsDj["QD2"]);
                g_Qd3 = Conversion.Val(mrsDj["QD3"]);
                g_Qd4 = Conversion.Val(mrsDj["QD4"]);
                MItem[0]["G_QD1"] = "≥" + mrsDj["QD1"];
                MItem[0]["G_QD2"] = "≥" + mrsDj["QD2"];
                MItem[0]["G_QD3"] = "≥" + mrsDj["QD3"];
                MItem[0]["G_QD4"] = "≥" + mrsDj["QD4"];
                MItem[0]["G_PHJM1"] = mrsDj["PHJM1"];
                MItem[0]["G_PHJM2"] = mrsDj["PHJM2"];
                MItem[0]["G_PHJM3"] = mrsDj["PHJM3"];
                MItem[0]["G_PHJM4"] = mrsDj["PHJM4"];
                MItem[0]["G_KCZSJ"] = mrsDj["KCZSJ"];
                //MItem[0]["WHICH"]=mrsDj["WHICH"];

                //MItem[0]["BGNAME"]=mrsDj["BGNAME"];

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
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;


            //mrsmainTable!jsbeizhu = ""
            //            If CDec(mbhggs) = 0 Then
            //              mrsmainTable!jsbeizhu = "该组试样所检项目符合" + mrsmainTable!pdbz + "标准要求。"
            //              sitem["JCJG = "合格"
            //            End If


            //            If CDec(mbhggs) >= 1 Then
            //              mrsmainTable!jsbeizhu = "该组试样不符合" + mrsmainTable!pdbz + "标准要求。"
            //              sitem["JCJG = "不合格"
            //              If(mFlag_Bhg And mFlag_Hg) Then
            //               mrsmainTable!jsbeizhu = "该组试样所检项目部分符合" + mrsmainTable!pdbz + "标准要求。"
            //                End If
            //            End If



            //           mAllHg = IIf(mbhggs > 0, False, mAllHg)
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
