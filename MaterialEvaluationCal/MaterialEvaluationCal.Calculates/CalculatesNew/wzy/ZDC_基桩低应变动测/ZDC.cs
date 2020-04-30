using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class ZDC : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_ZDC"];
            var MItem = data["M_ZDC"];
            int mbHggs = 0;
            if (!data.ContainsKey("M_ZDC"))
            {
                data["M_ZDC"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            else
            {
                MItem[0]["YS"] = "(报告正文共" + MItem[0]["ZWYS"] + "页)";
            }

            var mItem = MItem[0];
            string mJSFF;
            bool sign, mFlag_Bhg = false, mFlag_Hg = false;
            string BHGXM = "", Hgxm = "";

            double zCount = 0, iZgs = 0;
            foreach (var sItem in SItem)
            {
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                int mMinKyqd = 9999;
                zCount = zCount + 1;
                sItem["JCJG"] = "合格";
                iZgs = iZgs + 1;
                if (iZgs / 30 > (int)(iZgs / 30))
                {
                    mItem["MLYM"] = (8 + (int)(iZgs / 30) + 1).ToString();
                }
                else
                {
                    mItem["MLYM"] = (8 + (int)(iZgs / 30)).ToString();
                }
                if (iZgs / 8 > (int)(iZgs / 8))
                {
                    mItem["MLYM"] = (double.Parse(mItem["MLYM"]) + (int)(iZgs / 8) + 1).ToString();
                }
                else
                {
                    mItem["MLYM"] = (double.Parse(mItem["MLYM"]) + (int)(iZgs / 8)).ToString();
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
