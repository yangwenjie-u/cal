using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class CJL : BaseMethods
    {
        public void Calc()
        {

            string mSjdj = "";
            string errorMsg = "";
            string mJSFF = "";
            bool mSFwc;
            bool mGetBgbh = false,
            mAllHg = true,
            mFlag_Hg = false, mItemHg = true,
            mFlag_Bhg = false;
            int mbhggs = 0;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            var SItem = data["S_CJL"];
            var MItem = data["M_CJL"];
            //var E_SF = data["E_SF"];
            var mItem = MItem[0];
            foreach (var sItem in SItem)
            {
                bool sign, flag;
                double md1, md2, md, sum;
                string sql;
                var jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                #region 表观密度  ||  密度
                if (jcxm.Contains("、表观密度、") || jcxm.Contains("、密度、"))
                {
                    sItem["BGMD_GH"] = IsQualified(sItem["G_BGMD"], sItem["W_BGMD"], true);
                    if (sItem["BGMD_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["W_BGMD"] = "----";
                    sItem["BGMD_GH"] = "----";
                    sItem["G_BGMD"] = "----";
                }
                #endregion

                #region 堆积密度
                if (jcxm.Contains("、堆积密度、"))
                {
                    sItem["DJMD_GH"] = IsQualified(sItem["G_DJMD"], sItem["W_DJMD"], true);
                    if (sItem["DJMD_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["W_DJMD"] = "----";
                    sItem["DJMD_GH"] = "----";
                    sItem["G_DJMD"] = "----";
                }
                #endregion

                #region 振实密度
                if (jcxm.Contains("、振实密度、"))
                {
                    sItem["ZSMD_GH"] = IsQualified(sItem["G_ZSMD"], sItem["W_ZSMD"], true);
                    if (sItem["ZSMD_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["W_ZSMD"] = "----";
                    sItem["G_ZSMD"] = "----";
                    sItem["ZSMD_GH"] = "----";
                }
                #endregion

                #region 含泥量
                if (jcxm.Contains("、含泥量、"))
                {
                    sItem["HNL_GH"] = IsQualified(sItem["G_HNL"], sItem["W_HNL"], true);
                    if (sItem["HNL_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["HNL_GH"] = "----";
                    sItem["G_HNL"] = "----";
                    sItem["W_HNL"] = "----";
                }
                #endregion

                #region 泥块含量
                if (jcxm.Contains("、泥块含量、"))
                {
                    sItem["NKHL_GH"] = IsQualified(sItem["G_ZSMD"], sItem["W_ZSMD"], true);
                    if (sItem["HNL_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["NKHL_GH"] = "----";
                    sItem["G_NKHL"] = "----";
                    sItem["W_NKHL"] = "----";
                }
                #endregion

                #region 针状和片状颗粒总含量  || 针片状
                if (jcxm.Contains("、针状和片状颗粒总含量、") || jcxm.Contains("、针片状、"))
                {
                    sItem["ZZKL_GH"] = IsQualified(sItem["G_ZZKL"], sItem["W_ZZKL"], true);
                    if (sItem["ZZKL_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["ZZKL_GH"] = "----";
                    sItem["G_ZZKL"] = "----";
                    sItem["W_ZZKL"] = "----";
                }
                #endregion

                #region 压碎值指标 || 压碎值
                if (jcxm.Contains("、压碎值指标、") || jcxm.Contains("、压碎值、"))
                {
                    sItem["YSZ_GH"] = IsQualified(sItem["G_YSZ"], sItem["W_YSZ"], true);
                    if (sItem["YSZ_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["YSZ_GH"] = "----";
                    sItem["G_YSZ"] = "----";
                    sItem["W_YSZ"] = "----";
                }
                #endregion

                #region 含水率
                if (jcxm.Contains("、含水率、"))
                {
                    sItem["HSL_GH"] = IsQualified(sItem["G_HSL"], sItem["W_HSL"], true);
                    if (sItem["HSL_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["HSL_GH"] = "----";
                    sItem["G_HSL"] = "----";
                    sItem["W_HSL"] = "----";
                }
                #endregion

                #region 吸水率
                if (jcxm.Contains("、吸水率、"))
                {
                    sItem["XSL_GH"] = IsQualified(sItem["G_XSL"], sItem["W_XSL"], true);
                    if (sItem["HSL_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["XSL_GH"] = "----";
                    sItem["G_XSL"] = "----";
                    sItem["W_XSL"] = "----";
                }
                #endregion

                #region 空隙率
                if (jcxm.Contains("、空隙率、") && IsNumeric(sItem["W_BGMD"]) && IsNumeric(sItem["W_DJMD"]))
                {
                    md1 = GetSafeDouble(sItem["W_BGMD"]);
                    md2 = GetSafeDouble(sItem["W_DJMD"]);
                    md = (1 - md2 / md1) * 100;
                    md = Round(md, 1);
                    sItem["W_KXL"] = Round(md, 1).ToString();
                    sItem["KXL_GH"] = IsQualified(sItem["G_KXL"], sItem["W_KXL"], true);
                }
                else
                {
                    sItem["W_KXL"] = "----";
                    sItem["KXL_GH"] = "----";
                    sItem["G_KXL"] = "----";
                }
                
                jsbeizhu = "";
                #endregion

                if (jcxm == "、筛分析、" || jcxm == "、筛分、")
                {
                    jsbeizhu = "该组试样的检测结果详见报告第1～2页。";
                }
                else
                {
                    if (jcxm.Contains("筛分析") || jcxm.Contains("筛分"))
                    {
                        jsbeizhu = "该组试样的检测结果详见报告第1～2页。";
                    }
                    else
                    {
                        jsbeizhu = "该组试样的检测结果详见报告第1页。";
                    }
                }
                if (mAllHg)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }
            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_CJL"))
            {
                data["M_CJL"] = new List<IDictionary<string, string>>();
            }
            var M_CJL = data["M_CJL"];

            if (M_CJL.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_CJL.Add(m);
            }
            else
            {
                M_CJL[0]["JCJG"] = mjcjg;
                M_CJL[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
