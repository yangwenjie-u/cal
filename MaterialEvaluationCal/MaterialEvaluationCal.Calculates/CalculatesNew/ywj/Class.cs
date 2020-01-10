using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JGG : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra[""];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data[""];

            if (!data.ContainsKey(""))
            {
                data[""] = new List<IDictionary<string, string>>();
            }
            var MItem = data[""];

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
            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"] + "、";


                #region 
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["YPMC"] == sItem["YPMC"] && u["HD"] == sItem["DLDJ"]);
                if (null == extraFieldsDj)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    mItemHg = false;

                    jsbeizhu = "依据不详";
                    continue;
                }
              
                #endregion


                if (!jcxm.Contains("、冷弯、"))
                {
                    sItem["G_LWWZ"] = "----";

                }

                if (jcxm.Contains("、拉伸、") || jcxm.Contains("、抗拉强度、"))
                {

                }
                else
                {
                    sItem["JCJG_LS"] = "----";
                    sItem["DKJ1"] = "-1";
                    sItem["DKJ2"] = "-1";
                    sItem["DKJ3"] = "-1";
                    sItem["DKJ4"] = "-1";
                    sItem["DKJ5"] = "-1";
                    sItem["DKJ6"] = "-1";
                }

                if (jcxm.Contains("、冷弯、"))
                {

                }
                else
                {
                    sItem["JCJG_LW"] = "----";
                    sItem["LW1"] = "-1";
                    sItem["LW2"] = "-1";
                    sItem["LW3"] = "-1";
                    sItem["LW4"] = "-1";
                    sItem["LW5"] = "-1";
                    sItem["LW6"] = "-1";
                }

                if (sItem["JCJG_LS"] == "不符合" || sItem["JCJG_LW"] == "不符合")
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "该组试样的检测结果不合格";
                }
                else
                {
                    if (sItem["JCJG_LS"] == "无效")
                    {
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                        jsbeizhu = "该组试样的检测结果不合格";
                    }
                    else
                    {
                        sItem["JCJG"] = "合格";
                    }
                }

            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGSM"] = jsbeizhu;

            #endregion
            #endregion
        }
    }
}
