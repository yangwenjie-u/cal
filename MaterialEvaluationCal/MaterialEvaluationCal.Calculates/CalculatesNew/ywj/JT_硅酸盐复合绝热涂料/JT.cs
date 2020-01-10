using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JT : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_JT_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_JT"];
            //var YZSKB = data["YZSKB"];

            if (!data.ContainsKey("M_JT"))
            {
                data["M_JT"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_JT"];

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

            bool flag = true;
            List<double> nArr = new List<double>();
            double xd, Gs = 0;
            var mbhggs = 0;

            int mcd, mdwz = 0;
            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
             delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
             {
                 jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                 mbhggs = 0;

                 if (jcxm.Contains("、浆体密度、"))
                 {
                     sItem["HG_JTMD"] = IsQualified(mItem["G_JTMD"], sItem["JTMD"], false);
                     if (sItem["HG_JTMD"] == "不合格")
                     {
                         mbhggs += 1;
                         mAllHg = false;
                     }
                 }
                 else
                 {
                     sItem["JTMD"] = "----";
                     sItem["HG_JTMD"] = "----";
                     mItem["G_JTMD"] = "----";
                 }

                 if (jcxm.Contains("、干密度、"))
                 {
                     sItem["HG_GMD"] = IsQualified(mItem["G_GMD"], sItem["GMD"], false);
                     if (sItem["HG_GMD"] == "不合格")
                     {
                         mbhggs += 1;
                         mAllHg = false;
                     }
                 }
                 else
                 {
                     sItem["GMD"] = "----";
                     sItem["HG_GMD"] = "----";
                     mItem["G_GMD"] = "----";
                 }

                 if (jcxm.Contains("、体积收缩率、"))
                 {
                     sItem["HG_TJSSL"] = IsQualified(mItem["G_TJSSL"], sItem["TJSSL"], false);
                     if (sItem["HG_TJSSL"] == "不合格")
                     {
                         mbhggs += 1;
                         mAllHg = false;
                     }
                 }
                 else
                 {
                     sItem["TJSSL"] = "----";
                     sItem["HG_TJSSL"] = "----";
                     mItem["G_TJSSL"] = "----";
                 }

                 if (jcxm.Contains("、抗拉强度、"))
                 {
                     sItem["HG_KLQD"] = IsQualified(mItem["G_KLQD"], sItem["KLQD"], false);
                     if (sItem["HG_KLQD"] == "不合格")
                     {
                         mbhggs += 1;
                         mAllHg = false;
                     }
                 }
                 else
                 {
                     sItem["KLQD"] = "----";
                     sItem["HG_KLQD"] = "----";
                     mItem["G_KLQD"] = "----";
                 }

                 if (jcxm.Contains("、粘结强度、"))
                 {
                     sItem["HG_NJQD"] = IsQualified(mItem["G_NJQD"], sItem["NJQD"], false);
                     if (sItem["HG_NJQD"] == "不合格")
                     {
                         mbhggs += 1;
                         mAllHg = false;
                     }
                 }
                 else
                 {
                     sItem["NJQD"] = "----";
                     sItem["HG_NJQD"] = "----";
                     mItem["G_NJQD"] = "----";
                 }

                 if (jcxm.Contains("、导热系数、"))
                 {
                     mcd = mItem["G_DRXS"].Length;
                     mdwz = mItem["G_DRXS"].IndexOf('.');
                     mcd = mcd - mdwz + 1;
                     string DEVCODE = String.IsNullOrEmpty(mItem["DEVCODE"]) ? "" : mItem["DEVCODE"];
                     if (DEVCODE == "" && DEVCODE.Contains("XCS17-067") || DEVCODE.Contains("XCS17-066"))
                     {
                         //var mrsDrxs = YZSKB.FirstOrDefault(u => u["SYLB"].ToUpper() == "jt" && u["SYBH"] == mItem["JYDBH"]);
                         //sItem["DRXS"] = mrsDrxs["DRXS"];
                         //mItem["JCYJ"] = mItem["JCYJ"].Replace("10294", "10295");
                     }

                     sItem["DRXS"] = Math.Round(double.Parse(sItem["DRXS"]), mcd).ToString();

                     sItem["HG_DRXS"] = IsQualified(mItem["G_DRXS"], sItem["DRXS"], false);
                     if (sItem["HG_DRXS"] == "不合格")
                     {
                         mbhggs += 1;
                         mAllHg = false;
                     }
                 }
                 else
                 {
                     sItem["DRXS"] = "----";
                     sItem["HG_DRXS"] = "----";
                     mItem["G_DRXS"] = "----";
                     sItem["DRXS_WD"] = "";
                 }

                 if (mbhggs == 0)
                 {
                     jsbeizhu = "该组试件导热系数试验结果如上，其余所检项目符合" + mItem["PDBZ"] + "标准要求。";

                     if (sItem["DRXS_WD"] == "70℃")
                         jsbeizhu = "该组试件所检项目符合上述标准要求";
                     sItem["JCJG"] = "合格";
                     mAllHg = true;
                 }

                 if (mbhggs > 0)
                 {
                     jsbeizhu = "该组试件导热系数试验结果如上，其余所检项目不符合" + mItem["PDBZ"] + "标准要求。";

                     if (sItem["DRXS_WD"] == "70℃")
                         jsbeizhu = "该组试件不符合上述标准要求";
                     sItem["JCJG"] = "不合格";
                     mAllHg = false;
                 }

                 return mAllHg;
             };

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJDJ"]);

                if (null == mrsDj)
                {
                    jsbeizhu = "试件尺寸为空\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                MItem[0]["G_JTMD"] = mrsDj["G_JTMD"];
                MItem[0]["G_GMD"] = mrsDj["G_GMD"];
                MItem[0]["G_TJSSL"] = mrsDj["G_TJSSL"];
                MItem[0]["G_KLQD"] = mrsDj["G_KLQD"];
                MItem[0]["G_NJQD"] = mrsDj["G_NJQD"];

                //if (MItem[0]["QRBM"].Contains("90"))
                //{
                //    if (sItem["DRXS_WD"] == "70℃")
                //    {
                //        //MItem[0]["WHICH"] = "bgjt_99、bgjt";
                //    }
                //    else
                //    {
                //        //MItem[0]["WHICH"] = "bgjt_99、bgjt_1";
                //    }

                //}
                //else
                //{
                //    if (sItem["DRXS_WD"] == "70℃")
                //    {
                //        //MItem[0]["WHICH"] = "0";
                //    }
                //    else
                //    {
                //        //MItem[0]["WHICH"] = "1";
                //    }
                //}

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

            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}