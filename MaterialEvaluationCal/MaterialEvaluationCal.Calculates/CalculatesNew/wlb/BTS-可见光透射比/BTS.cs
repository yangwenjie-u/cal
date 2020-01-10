using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class BTS : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_BTS_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_BTSS = data["S_BTS"];
            if (!data.ContainsKey("M_BTS"))
            {
                data["M_BTS"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_BTS"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            bool sign = true;
            foreach (var sItem in S_BTSS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                sign = true;
                if (jcxm.Contains("、可见光透射比、"))
                {
                    string sJTSB = sItem["SJTSB"];
                    if (sJTSB.Length != 0 && sItem["SJTSB"] != "----")
                    {
                        MItem[0]["JCJG"] = "";
                        sItem["G_TSB"] = "≥" + sItem["SJTSB"].Trim();
                        sItem["GH_TSB"] = IsQualified(sItem["G_TSB"], sItem["W_TSB"], false);
                        if (sItem["GH_TSB"] == "不合格")
                        {
                            itemHG = false;
                            mAllHg = false;
                            jsbeizhu = "该组玻璃构件可见光透射比不符合设计要求";
                        }
                        else
                        {
                            jsbeizhu = "该组玻璃构件可见光透射比符合设计要求";
                        }

                    }
                    else
                    {
                        sItem["G_TSB"] = "----";
                        sItem["GH_TSB"] = "----";
                        jsbeizhu = "该组玻璃构件可见光透射比检测结果如上";
                    }
                }
                else
                {
                    sign = false;
                }

                if (!sign)
                {
                    sItem["W_TSB"] = "----";
                    sItem["GH_TSB"] = "----";
                    sItem["G_TSB"] = "----";
                }
                //单组判断
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
