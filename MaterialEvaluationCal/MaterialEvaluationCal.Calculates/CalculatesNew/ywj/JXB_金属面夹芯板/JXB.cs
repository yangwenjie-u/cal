using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JXB : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_JXB_DJ"];

            var data = retData;
            var mJCJG = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_JXB"];

            if (!data.ContainsKey("M_JXB"))
            {
                data["M_JXB"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_JXB"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mJCJG;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";

            bool sign = true;
            List<double> nArr = new List<double>();
            int mcd, mdwz = 0;

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["JXBLB"] == sItem["JXBLB"].Trim() && u["HD"] == sItem["DLDJ"].Trim());

                if (null == mrsDj)
                {
                    jsbeizhu = "试件尺寸为空\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }

                sItem["G_PHHZ"] = mrsDj["G_PHHZ"];
                sItem["G_NJQD"] = mrsDj["G_NJQD"];
                sItem["G_BLXN"] = mrsDj["G_BLXN"];
                sItem["G_CRXS"] = mrsDj["G_CRXS"];

                sign = true;
                if (jcxm.Contains("、抗弯承载力、"))
                {
                    sign = IsNumeric(sItem["W_PHHZ"]) && string.IsNullOrEmpty(sItem["W_PHHZ"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_PHHZ"] = IsQualified(sItem["G_PHHZ"], sItem["W_PHHZ"]);
                    }
                }
                if (!sign)
                {
                    sItem["G_PHHZ"] = "----";
                    sItem["W_PHHZ"] = "----";
                    sItem["GH_PHHZ"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、传热系数、"))
                {
                    sign = IsNumeric(sItem["W_CRXS"]) && !string.IsNullOrEmpty(sItem["W_CRXS"]) ? sign : false;

                    mcd = sItem["G_CRXS"].Trim().Length;
                    mdwz = sItem["G_CRXS"].IndexOf(',');
                    mcd = mcd - mdwz;

                    sItem["W_CRXS"] = Math.Round(Conversion.Val(sItem["W_CRXS"]), mcd).ToString();
                    sItem["W_CRXS1"] = sItem["W_CRXS"];
                    if (sign)
                    {
                        sItem["GH_CRXS"] = IsQualified(sItem["G_CRXS"], sItem["W_CRXS"]);
                    }
                }
                if (!sign)
                {

                    sItem["W_CRXS"] = "----";
                    sItem["G_CRXS"] = "----";
                    sItem["GH_CRXS"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、粘结强度、"))
                {
                    sign = IsNumeric(sItem["W_NJQD"]) && !string.IsNullOrEmpty(sItem["W_NJQD"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_NJQD"] = IsQualified(sItem["G_NJQD"], sItem["W_NJQD"]);
                    }
                }
                if (!sign)
                {
                    sItem["G_NJQD"] = "----"; ;
                    sItem["W_NJQD"] = "----";
                    sItem["GH_NJQD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、剥离性能、"))
                {
                    sign = IsNumeric(sItem["W_BLXN"]) && !string.IsNullOrEmpty(sItem["W_BLXN"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_BLXN"] = IsQualified(sItem["G_BLXN"], sItem["W_BLXN"]);
                    }
                }
                if (!sign)
                {
                    sItem["G_BLXN"] = "----";
                    sItem["W_BLXN"] = "----";
                    sItem["GH_BLXN"] = "----";
                }


                if (sItem["GH_PHHZ"] == "不合格" || sItem["GH_BLXN"] == "不合格" || sItem["GH_NJQD"] == "不合格" || sItem["GH_CRXS"] == "不合格")
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "该组样品所检项不符合上述标准要求。";

                }
                else
                {
                    sItem["JCJG"] = "合格";
                    mAllHg = true;
                    jsbeizhu = "该组样品所检项目符合上述标准要求。";
                }

            }

            #region 添加最终报告

            if (mAllHg && mJCJG != "----")
            {
                mJCJG = "合格";
            }


            MItem[0]["JCJG"] = mJCJG;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}