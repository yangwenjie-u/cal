using Microsoft.VisualBasic;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SWJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_SWJ"];
            var MItem = data["M_SWJ"];
            if (!data.ContainsKey("M_SWJ"))
            {
                data["M_SWJ"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];

            foreach (var sItem in SItem)
            {
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                //sItem["S_AVG"] = Round((GetSafeDouble(sItem["SYDDL1"]) + GetSafeDouble(sItem["SYDDL2"])) / 2,1).ToString("0.0");

                if (jcxm.Contains("、标准曲线、"))
                {
                    sItem["JCJG"] = "合格";
                    jsbeizhu = "标准曲线";
                }

                #region EDTA滴定法
                if (jcxm.Contains("、EDTA滴定法、"))
                {
                    double S_SJJL = Conversion.Val(sItem["S_SJJL"]);
                    string sjjl = "≥" + S_SJJL;
                    sItem["S_PDJG"] = IsQualified(sjjl, sItem["S_AVG"], true);
                    if (sItem["S_PDJG"] == "不符合")
                    {
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                    }
                    if (sItem["S_PDJG"] == "----")
                    {
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经检测该混合料中的" + sItem["JCXM"] + "为" + sItem["S_AVG"] + "%。";
                    }
                    else
                    {
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经检测该混合料中的" + sItem["JCXM"] + "为" + sItem["S_AVG"] + "%。";
                    }

                    if (sItem["S_PDJG"] == "符合")
                    {
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        sItem["JCJG"] = "合格";
                    }

                    if (sItem["S_PDJG"] == "不符合")
                    {
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目EDTA滴定法不符合要求。";
                        mAllHg = false;
                    }
                }
                #endregion

            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
