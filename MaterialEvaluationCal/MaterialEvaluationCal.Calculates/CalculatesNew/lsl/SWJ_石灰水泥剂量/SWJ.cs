using Microsoft.VisualBasic;
using System.Collections.Generic;

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

                sItem["S_PDJG"] = IsQualified(sItem["S_SJJL"],sItem["S_AVG"],true);
                if (sItem["S_PDJG"] == "不符合")
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                if (sItem["S_PDJG"] == "----")
                {
                    jsbeizhu = "经检测，该混合料中的"+sItem["JCXM"]+ "为"+sItem["S_AVG"]+ "%。";
                }
                else
                {
                    jsbeizhu = "经检测，该混合料中的" + sItem["JCXM"] + "为" + sItem["S_AVG"] + "%，";
                }

                if (sItem["S_PDJG"] == "符合")
                {
                    jsbeizhu = jsbeizhu+ "符合设计要求。";
                    sItem["JCJG"] = "合格";
                }

                if (sItem["S_PDJG"] == "不符合")
                {
                    jsbeizhu = jsbeizhu + "不符合设计要求。";
                    mAllHg = false;
                }
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
