using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class TRD : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_TRD"];
            var MItem = data["M_TRD"];
            int mbHggs = 0;
            if (!data.ContainsKey("M_TRD"))
            {
                data["M_TRD"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            string mJSFF;
            bool sign, mFlag_Bhg = false, mFlag_Hg = false;
            string BHGXM = "", Hgxm = "";

            foreach (var sItem in SItem)
            {
                double zddnd = 0,zxdnd = 99999;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                double dnd = double.Parse(sItem["DND1"]);
                if (dnd > 0)
                {
                    if (zddnd < dnd) 
                    {
                        zddnd = dnd;
                    }
                    if (zxdnd > dnd)
                    {
                        zxdnd = dnd;
                    }
                }
                sItem["JCJG"] = "合格";
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
