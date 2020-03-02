using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class KF : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/

            string bhg_jcxm = "";
            bool mAllHg = true, sign = true, mSFwc = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jcjg = "";
            double md, md1, md2;
            var SItem = data["S_KF"];
            var MItem = data["M_KF"];
            var mItem = MItem[0];
            double sum;
            foreach (var sItem in SItem)
            {
                bool jcjgHg = true;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                if (jcxm.Contains("、密度、"))
                {
                    sItem["MD_GH"] = IsQualified(sItem["G_MD"], sItem["W_MD"], true);
                    if (sItem["MD_GH"] == "不符合") mAllHg = false;
                    if (sItem["MD_GH"] == "不符合")
                    {
                        jcjgHg = false;
                        if (bhg_jcxm == "")
                            bhg_jcxm = "密度";
                        else
                            bhg_jcxm = bhg_jcxm + "、密度";
                    }
                }
                else
                {
                    sItem["G_MD"] = "----";
                    sItem["MD_GH"] = "----";
                    sItem["W_MD"] = "----";
                }

                if (jcxm.Contains("、塑性指数、"))
                {
                    sItem["SXZS_GH"] = IsQualified(sItem["G_SXZS"], sItem["W_SXZS"], true);
                    if (sItem["SXZS_GH"] == "不符合")
                    {
                        mAllHg = false;
                        jcjgHg = false;
                        if (bhg_jcxm == "") bhg_jcxm = "塑性指数";
                        else bhg_jcxm = bhg_jcxm + "塑性指数";
                    }
                }
                else
                {
                    sItem["G_SXZS"] = "----";
                    sItem["SXZS_GH"] = "----";
                    sItem["W_SXZS"] = "----";
                }

                MItem[0]["BHG_JCXM"] = bhg_jcxm;
                jsbeizhu = "";
                if (jcxm == "、筛分、")
                {
                    jsbeizhu = "该试样的检测结果详见报告。";
                }
                else
                {
                    if (jcxm.Contains("、筛分、"))
                    {
                        jsbeizhu = "该试样的检测结果详见报告。";
                    }
                }
                if (jcjgHg)
                {
                    sItem["JCJG"] = "合格";
                    MItem[0]["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    MItem[0]["JCJG"] = "不合格";
                }
                mAllHg = mAllHg & sItem["JCJG"] == "合格" ? true : false;
            }
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_KF"))
            {
                data["M_KF"] = new List<IDictionary<string, string>>();
            }
            var M_KF = data["M_KF"];
            if (M_KF.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_KF.Add(m);
            }
            else
            {
                M_KF[0]["JCJG"] = mjcjg;
                M_KF[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            #endregion
        }
    }
}
