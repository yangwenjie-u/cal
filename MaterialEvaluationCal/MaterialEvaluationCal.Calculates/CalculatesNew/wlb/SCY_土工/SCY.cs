using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
/*土工*/
namespace Calculates
{
    public class SCY : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_AM_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_SCYS = data["S_SCY"];
            bool sign = false;
            if (!data.ContainsKey("M_SCY"))
            {
                data["M_SCY"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_SCY"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            string mJSFF = "";
            int mHggs = 0;//统计合格数量
            foreach (var sItem in S_SCYS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                #region 颗粒分析
                if (jcxm.Contains("、颗粒分析、"))
                {
                    sItem["GH_SF"] = IsQualified(sItem["SJ_SF1"], sItem["W_SF1"], true);
                    if (sItem["GH_SF"] == "不符合")
                    {
                        mAllHg = false;
                    }
                    sign = true;
                }
                else
                {
                    sItem["W_SF1"] = "----";
                    sItem["BZ_SF"] = "----";
                    sItem["GH_SF"] = "----";
                }
                #endregion

            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组样品所检项目符合标准要求。";
            }
            else
            {
                jsbeizhu = "该组样品所检项目不符合标准要求。";

            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
