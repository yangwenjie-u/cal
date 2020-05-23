using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TS : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_TS"];
            var MItem = data["M_TS"];
            int mbHggs = 0;
            if (!data.ContainsKey("M_TS"))
            {
                data["M_TS"] = new List<IDictionary<string, string>>();
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


                if (jcxm.Contains("、界限含水率、") || jcxm.Contains("、液限、") || jcxm.Contains("、塑限、"))
                {
                    if (sItem["SXZB"] =="补做")
                    {
                        throw new SystemException("两个含水率的差值≥2%，应补做试验。");
                    }
                    sItem["GH_JXHSL"] = "符合";
                    string yx = IsQualified(sItem["SJ_YX"], sItem["YX"], true);
                    string sx = IsQualified(sItem["SJ_SX"], sItem["SX"], true);
                    string sxzb = IsQualified(sItem["SJ_SXZB"], sItem["SXZB"], true);
                    if (yx == "不符合" || sx == "不符合" || sxzb == "不符合")
                    {
                        sItem["GH_JXHSL"] = "不符合";
                        mAllHg = false;
                    }
                    else
                    {
                        string yx1 = IsQualified(sItem["SJ_YX"], sItem["YX"], true);
                        string sx1 = IsQualified(sItem["SJ_SX"], sItem["SX"], true);
                        string sxzb1 = IsQualified(sItem["SJ_SXZB"], sItem["SXZB"], true);
                        if (yx1 == "----" && sx1 == "----" && sxzb1 == "----")
                        {
                            sItem["GH_JXHSL"] = "----";
                        }

                    }
                }
                else
                {
                    sItem["YX"] = "----";
                    sItem["SX"] = "----";
                    sItem["SXZB"] = "----";
                    sItem["GH_JXHSL"] = "----";
                }

                mbHggs = sItem["GH_JXHSL"] == "不符合" ? mbHggs + 1 : mbHggs;
                sItem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";

                mAllHg = mbHggs == 0 ? true : false;
                jsbeizhu = mbHggs == 0 ? "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。" : "依据" + MItem[0]["PDBZ"] + "的规定，所检项目不符合要求。"; 
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
