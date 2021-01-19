using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class HYZ:BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_HYZ_DJ"];
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            var SItems = retData["S_HYZ"];

            if (!retData.ContainsKey("M_HYZ"))
            {
                retData["M_HYZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = retData["M_HYZ"];

            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mItemHg = true;
            var jcxm = "";

            double md1, md2, md, pjmd = 0.0;
            int xd, Gs = 0;
            bool flag, sign = false;

            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                sItem["JCJG"] = "----";
                jsbeizhu = "----";
            }
            #region 更新主表检测结果
            if (mAllHg && mjcjg != "----")
            {
                //mjcjg = "合格";
                mjcjg = "----";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }

    }
}
