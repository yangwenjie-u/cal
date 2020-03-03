using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TGB : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_TGB_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var SItems = data["S_TGB"];

            if (!data.ContainsKey("M_TGB"))
            {
                data["M_TGB"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_TGB"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";

            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                //断裂强度、断裂伸长率、撕破强力
                //断裂强度
                if (jcxm.Contains("、断裂强度、"))
                {
                    sItem["HG_HXDLQD"] = IsQualified(MItem[0]["G_HXDLQD"], sItem["HXDLQD"], false);
                    sItem["HG_ZXDLQD"] = IsQualified(MItem[0]["G_ZXDLQD"], sItem["ZXDLQD"], false);

                    mAllHg = sItem["HG_ZXDLQD"] == "合格" ? mAllHg : false;
                    mAllHg = sItem["HG_HXDLQD"] == "合格" ? mAllHg : false;
                }
                else
                {
                    sItem["HG_ZXDLQD"] = "----";
                    sItem["HG_ZXDLQD"] = "----";
                    sItem["HXDLQD"] = "----";
                    sItem["ZXDLQD"] = "----";
                    MItem[0]["G_HXDLQD"] = "----";
                    MItem[0]["G_ZXDLQD"] = "----";
                }

                //断裂伸长率
                if (jcxm.Contains("、断裂伸长率、"))
                {
                    sItem["GH_ZXQD"] = IsQualified(sItem["ZXQDSJZ"], sItem["W_ZXQD"], false);
                    sItem["GH_ZSCL"] = IsQualified(sItem["ZSCLSJZ"], sItem["W_ZSCL"], false);
                    sItem["GH_HXQD"] = IsQualified(sItem["HXQDSJZ"], sItem["W_HXQD"], false);
                    sItem["GH_HSCL"] = IsQualified(sItem["HSCLSJZ"], sItem["W_HSCL"], false);

                    mAllHg = sItem["GH_ZXQD"] == "合格" ? mAllHg : false;
                    mAllHg = sItem["GH_ZSCL"] == "合格" ? mAllHg : false;
                    mAllHg = sItem["GH_HXQD"] == "合格" ? mAllHg : false;
                    mAllHg = sItem["GH_HSCL"] == "合格" ? mAllHg : false;
                }
                else
                {
                    sItem["GH_ZXQD"] = "----";
                    sItem["GH_ZSCL"] = "----";
                    sItem["GH_HXQD"] = "----";
                    sItem["GH_HSCL"] = "----";

                    sItem["ZXQDSJZ"] = "----";
                    sItem["ZSCLSJZ"] = "----";
                    sItem["ZSCLSJZ"] = "----";
                    sItem["HSCLSJZ"] = "----";

                    sItem["W_ZXQD"] = "----";
                    sItem["W_ZSCL"] = "----";
                    sItem["W_HXQD"] = "----";
                    sItem["W_HSCL"] = "----";
                }

                //撕破强力
                if (jcxm.Contains("、撕破强力、"))
                {
                    sItem["HG_ZXSPQL"] = IsQualified(MItem[0]["G_ZXSPQL"], sItem["ZXSPQL"], false);
                    sItem["HG_HXSPQL"] = IsQualified(MItem[0]["G_HXSPQL"], sItem["HXSPQL"], false);
                    mAllHg = sItem["HG_ZXSPQL"] == "合格" ? mAllHg : false;
                    mAllHg = sItem["HG_HXSPQL"] == "合格" ? mAllHg : false;
                }
                else
                {
                    sItem["HG_ZXSPQL"] = "----";
                    sItem["HG_HXSPQL"] = "----";
                    sItem["ZXSPQL"] = "----";
                    sItem["HXSPQL"] = "----";
                    MItem[0]["G_HXSPQL"] = "----";
                    MItem[0]["G_ZXSPQL"] = "----";

                }

                //耐静水压
                if (jcxm.Contains("、耐静水压、"))
                {
                    sItem["HG_NJSY"] = IsQualified(MItem[0]["G_NJSY"], sItem["NJSY"], false);
                    mAllHg = sItem["HG_NJSY"] == "合格" ? mAllHg : false;
                }
                else
                {
                    sItem["HG_NJSY"] = "----";
                    MItem[0]["G_NJSY"] = "----";
                    sItem["NJSY"] = "----";
                }

                if (!mAllHg)
                {
                    sItem["JCJG"] = "不合格";
                }
                else
                {
                    sItem["JCJG"] = "合格";
                }
            }

            #region 添加最终报告
            jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";

            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
        }
    }
}

