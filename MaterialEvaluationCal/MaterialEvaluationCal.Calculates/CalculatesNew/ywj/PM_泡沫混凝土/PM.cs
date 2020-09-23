using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class PM : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_PM_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_PM"];
           

            if (!data.ContainsKey("M_PM"))
            {
                data["M_PM"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_PM"];

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
            double md1, md2, md, sum, Gs, g_md,pjmd = 0;
            int xd = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            List<double> nArr = new List<double>();

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
                 delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
                 {
                     jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                     mFlag_Hg = true;
                     sign = true;
                     if (jcxm.Contains("、抗压强度、"))
                     {
                         if (Conversion.Val(mItem["GH_QD"]) == 0)
                         {
                             mAllHg = false;
                             return false;
                         }

                         mItem["G_QD"] = "平均值" + mItem["G_QD_AVG"] + "\r" + "单组最小值" + mItem["G_QD_MIN"];

                         var QualifiedlVal = IsQualified(mItem["G_QD_AVG"], mItem["AVG_QD"], true) == "符合" && IsQualified(mItem["G_QD_MIN"], mItem["AVGMIN_QD_QD"], true) == "符合";

                         if (QualifiedlVal)
                         {
                             mItem["GH_QD"] = "合格";
                         }

                         QualifiedlVal = IsQualified(mItem["G_QD_AVG"], mItem["AVG_QD"], true) == "不符合" && IsQualified(mItem["G_QD_MIN"], mItem["MIN_QD"], true) == "不符合";
                         if (QualifiedlVal)
                         {
                             mItem["GH_QD"] = "不合格";
                         }

                         if (mItem["GH_QD"] == "不合格")
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
                         mItem["G_QD_MIN"] = "----";
                         mItem["G_QD_AVG"] = "----";
                         mItem["G_QD"] = "----";
                         mItem["GH_QD"] = "----";
                         mItem["AVG_QD"] = "----";
                         mItem["MIN_QD"] = "----";
                         for (xd = 1; xd < 4; xd++)
                         {
                             mItem["F_QD" + xd] = "----";
                         }
                     }

                     if (jcxm.Contains("、干密度、"))
                     {
                         g_md = double.Parse(mItem["G_MD"]) * 1.05;
                         mItem["GH_MD"] = IsQualified("≤" + g_md, mItem["AVG_MD"], false);
                         if (mItem["GH_MD"] == "不合格")
                         {
                             mbhggs = mbhggs + 1;
                             mAllHg = false;
                             mFlag_Hg = true;
                         }
                         else
                         {
                             mFlag_Hg = true;
                         }
                         mItem["G_MD"] = "≤" + mItem["G_MD"] + "，且其容许误差为+5%";
                     }
                     else
                     {
                         mItem["G_MD"] = "----";
                         mItem["GH_MD"] = "----";
                         mItem["AVG_MD"] = "----";

                         for (xd = 1; xd < 4; xd++)
                         {
                             mItem["F_MD" + xd] = "----";
                         }
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
                         jsbeizhu = "该组试件所检项目不符合" + mItem["PDBZ"] + "标准要求。";
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
                for (xd = 0; xd < Gs; xd++)
                {
                    if (sItem["QDDJ"].Trim() == extraDJ[xd]["QDDJ"].Trim())
                    {
                        MItem[0]["G_QD_MIN"] = extraDJ[xd]["QDMIN"];
                        MItem[0]["G_QD_AVG"] = extraDJ[xd]["QDAVG"];
                        break;
                    }
                }

                for (xd = 0; xd < Gs; xd++)
                {
                    if (sItem["MDDJ"].Trim() == extraDJ[xd]["MDDJ"].Trim())
                    {
                        MItem[0]["G_MD"] = extraDJ[xd]["BGMD"];
                        MItem[0]["G_DR"] = extraDJ[xd]["DRXS"];
                        break;
                    }
                }
                for (xd = 0; xd < Gs; xd++)
                {
                    if (sItem["XSLDJ"].Trim() == extraDJ[xd]["XSLDJ"].Trim())
                    {
                        MItem[0]["G_XSL"] = extraDJ[xd]["XSL"];
                        break;
                    }
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

