using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class KZD : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_KZD_DJ"];
            var data = retData;

            var SItem = data["S_KZD"];
            var MItem = data["M_KZD"];
            bool sign = true;
            string mJSFF = "";
            double zj1 = 0, zj2 = 0, mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim() && u["D_JB"] == sItem["JB"]);
                if (null != extraFieldsDj)
                {
                    sItem["G_MD"] = string.IsNullOrEmpty(extraFieldsDj["D_MD"]) ? extraFieldsDj["D_MD"] : extraFieldsDj["D_MD"].Trim();
                    sItem["G_HXZS7"] = string.IsNullOrEmpty(extraFieldsDj["D_HXXS7"]) ? extraFieldsDj["D_HXXS7"] : extraFieldsDj["D_HXXS7"].Trim();
                    sItem["G_HXZS28"] = string.IsNullOrEmpty(extraFieldsDj["D_HXXS28"]) ? extraFieldsDj["D_HXXS28"] : extraFieldsDj["D_HXXS28"].Trim();
                    sItem["G_LDDB"] = string.IsNullOrEmpty(extraFieldsDj["D_LDDB"]) ? extraFieldsDj["D_LDDB"] : extraFieldsDj["D_LDDB"].Trim();
                    sItem["G_HSL"] = string.IsNullOrEmpty(extraFieldsDj["D_HSL"]) ? extraFieldsDj["D_HSL"] : extraFieldsDj["D_HSL"].Trim();
                    sItem["G_SSL"] = string.IsNullOrEmpty(extraFieldsDj["D_SSL"]) ? extraFieldsDj["D_SSL"] : extraFieldsDj["D_SSL"].Trim();
                    sItem["G_BBMJ"] = string.IsNullOrEmpty(extraFieldsDj["D_BBMJ"]) ? extraFieldsDj["D_BBMJ"] : extraFieldsDj["D_BBMJ"].Trim();
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 活性指数
                if (jcxm.Contains("、活性指数、") && !string.IsNullOrEmpty(sItem["G_HXZS7"]) && !string.IsNullOrEmpty(sItem["G_HXZS28"]))
                {
                    double md1 = 0, md2 = 0, md = 0, xd1 = 0, xd2 = 0, xd = 0, ba = 0, kz = 0, b1 = 0;

                    jcxmCur = "活性指数";
                    md1 = GetSafeDouble(sItem["S_JZQD7"]);
                    md2 = GetSafeDouble(sItem["S_SJQD7"]);
                    md = md1 != 0 ? Math.Round(md2 * 100 / md1, 1) : 0;

                    xd1 = GetSafeDouble(sItem["S_JZQD28"]);
                    xd2 = GetSafeDouble(sItem["S_SJQD28"]);
                    xd = xd1 != 0 ? Math.Round(xd2 * 100 / xd1) : 0;

                    b1 = GetSafeDouble(sItem["G_HXZS7"]);
                    kz = GetSafeDouble(sItem["G_HXZS28"]);
                    if (md >= b1 && xd >= kz)
                    {
                        sItem["HXZS_GH"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HXZS_GH"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_HXZS7"] = "≥" + sItem["G_HXZS7"];
                    sItem["G_HXZS28"] = "≥" + sItem["G_HXZS28"];
                    sItem["W_HXZS7"] = md.ToString("0");
                    sItem["W_HXZS28"] = xd.ToString("0");
                }
                else
                {
                    sItem["HXZS_GH"] = "----";
                    sItem["G_HXZS7"] = "----";
                    sItem["G_HXZS28"] = "----";
                    sItem["W_HXZS7"] = "----";
                    sItem["W_HXZS28"] = "----";
                }
                #endregion

                #region 流动度比
                if (jcxm.Contains("、流动度比、") && !string.IsNullOrEmpty(sItem["G_LDDB"]))
                {
                    double md1 = 0, md2 = 0, md = 0, xd1 = 0, xd2 = 0, xd = 0, ba = 0, kz = 0, b1 = 0;

                    jcxmCur = "流动度比";
                    md1 = GetSafeDouble(sItem["S_JZLD1"]);
                    md2 = GetSafeDouble(sItem["S_SJLD1"]);
                    md = md1 != 0 ? Math.Round(md2 * 100 / md1, 1) : 0;

                    xd1 = GetSafeDouble(sItem["S_JZLD2"]);
                    xd2 = GetSafeDouble(sItem["S_SJLD2"]);
                    xd = xd1 != 0 ? Math.Round(xd2 * 100 / xd1) : 0;

                    md = Math.Round((md + xd) / 2, 0);

                    b1 = GetSafeDouble(sItem["G_LDDB"]);
                    if (md >= b1)
                    {
                        sItem["LDDB_GH"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["LDDB_GH"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_LDDB"] = "≥" + sItem["G_LDDB"];
                    sItem["W_LDDB"] = md.ToString("0");

                }
                else
                {
                    sItem["LDDB_GH"] = "----";
                    sItem["G_LDDB"] = "----";
                    sItem["W_LDDB"] = "----";
                }
                #endregion

                #region 含水量
                if (jcxm.Contains("、含水量、") && !string.IsNullOrEmpty(sItem["G_HSL"]))
                {

                    double md1 = 0, md2 = 0, md = 0, xd1 = 0, xd2 = 0, b1 = 0, xd = 0;
                    jcxmCur = "含水量";
                    md1 = GetSafeDouble(sItem["S_HGQ1"]) - GetSafeDouble(sItem["S_GGZL1"]);
                    md2 = GetSafeDouble(sItem["S_HGH1"]) - GetSafeDouble(sItem["S_GGZL1"]);
                    md = md1 - md2;
                    md = md1 != 0 ? Math.Round(md * 100 / md1, 2) : 0;

                    xd1 = GetSafeDouble(sItem["S_HGQ2"]) - GetSafeDouble(sItem["S_GGZL2"]);
                    xd2 = GetSafeDouble(sItem["S_HGH2"]) - GetSafeDouble(sItem["S_GGZL2"]);
                    xd = xd1 - xd2;
                    xd = xd1 != 0 ? Math.Round(xd * 100 / xd1, 2) : 0;

                    md = Math.Round((md + xd) / 2, 1);
                    b1 = GetSafeDouble(sItem["G_HSL"]);
                    if (md <= b1)
                    {
                        sItem["HSL_GH"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HSL_GH"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_HSL"] = "≥" + sItem["G_HSL"];
                    sItem["W_HSL"] = md.ToString("0.0");

                }
                else
                {
                    sItem["HSL_GH"] = "----";
                    sItem["G_HSL"] = "----";
                    sItem["W_HSL"] = "----";
                }
                #endregion

                #region 烧失量
                if (jcxm.Contains("、烧失量、"))
                {
                    jcxmCur = "烧失量";
                    if (Conversion.Val(sItem["W_SSL"]) <= Conversion.Val(sItem["G_SSL"]))
                    {
                        sItem["SSL_GH"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["SSL_GH"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_SSL"] = "≤" + sItem["G_SSL"];
                }
                else
                {
                    sItem["SSL_GH"] = "----";
                    sItem["G_SSL"] = "----";
                    sItem["W_SSL"] = "----";
                }
                #endregion

            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            if (!data.ContainsKey("M_KZD"))
            {
                data["M_KZD"] = new List<IDictionary<string, string>>();
            }
            var M_KZD = data["M_KZD"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_KZD == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_KZD.Add(m);
            }
            else
            {
                M_KZD[0]["JCJG"] = mjcjg;
                M_KZD[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
