using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SS : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_SS"];
            var MItem = data["M_SS"];
            if (!data.ContainsKey("M_SS"))
            {
                data["M_SS"] = new List<IDictionary<string, string>>();
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
                
                bool mark;

                if (jcxm.Contains("、压碎值、"))
                {
                    mark = IsNumeric(sItem["SYZ"]);
                    if (mark)
                    {
                        mItem["W_YSZ"] = sItem["SYZ"].Trim();
                        mItem["GH_YSZ"] = IsQualified(mItem["G_YSZ"], mItem["W_YSZ"],true);
                    }
                    else
                    {
                        mItem["W_YSZ"] = "----";
                        mItem["GH_YSZ"] = "----";
                    }
                }
                else
                {
                    mItem["W_YSZ"] = "----";
                    mItem["GH_YSZ"] = "----";
                    mItem["G_YSZ"] = "----";
                }

                if (jcxm.Contains("、液限、"))
                {
                    mark = IsNumeric(sItem["YX"]);
                    if (mark)
                    {
                        mItem["W_YX"] = sItem["YX"].Trim();
                        mItem["GH_YX"] = IsQualified(mItem["G_YX"], mItem["W_YX"], true);
                    }
                    else
                    {
                        mItem["W_YX"] = "----";
                        mItem["GH_YX"] = "----";
                    }
                }
                else
                {
                    mItem["W_YX"] = "----";
                    mItem["GH_YX"] = "----";
                    mItem["G_YX"] = "----";
                }

                if (jcxm.Contains("、塑性指数、"))
                {
                    mark = IsNumeric(sItem["SXZS"]);
                    if (mark)
                    {
                        mItem["W_SXZS"] = sItem["SXZS"].Trim();
                        mItem["GH_SXZS"] = IsQualified(mItem["G_SXZS"], mItem["W_SXZS"], true);
                    }
                    else
                    {
                        mItem["W_SXZS"] = "----";
                        mItem["GH_SXZS"] = "----";
                    }
                }
                else
                {
                    mItem["W_SXZS"] = "----";
                    mItem["GH_SXZS"] = "----";
                    mItem["G_SXZS"] = "----";
                }

                if (jcxm.Contains("、有机质含量、"))
                {
                    mark = IsNumeric(sItem["YJZHL"]);
                    if (mark)
                    {
                        mItem["W_YJZHL"] = sItem["YJZHL"].Trim();
                        mItem["GH_YJZHL"] = IsQualified(mItem["G_YJZHL"], mItem["W_YJZHL"], true);
                    }
                    else
                    {
                        mItem["W_YJZHL"] = "----";
                        mItem["GH_YJZHL"] = "----";
                    }
                }
                else
                {
                    mItem["W_YJZHL"] = "----";
                    mItem["GH_YJZHL"] = "----";
                    mItem["G_YJZHL"] = "----";
                }

                sItem["JCJG"] = "合格";
                mAllHg = true;
                mItem["JGSM"] = "该组样品检测结果如上。";
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
