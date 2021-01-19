using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GTL:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_GTL_DJ"];
            var data = retData;

            var SItem = data["S_GTL"];
            var MItem = data["M_GTL"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim() && u["SJDJ"] == sItem["SJDJ"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_DRXS70"] = extraFieldsDj["G_DRXS70"];
                    sItem["G_DRXS350"] = extraFieldsDj["G_DRXS350"];
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 导热系数70℃
                if (jcxm.Contains("、导热系数70℃、"))
                {
                    jcxmCur = "导热系数70℃";
                    if (IsQualified(sItem["G_DRXS70"], sItem["W_DRXS70"], false) == "合格")
                    {
                        sItem["HG_DRXS70"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_DRXS70"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_DRXS70"] = "----";
                    sItem["HG_DRXS70"] = "----";
                    sItem["W_DRXS70"] = "----";
                }
                #endregion

                #region 导热系数350℃
                if (jcxm.Contains("、导热系数350℃、"))
                {
                    jcxmCur = "导热系数350℃";
                    if (IsQualified(sItem["G_DRXS350"], sItem["W_DRXS350"], false) == "合格")
                    {
                        sItem["HG_DRXS350"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_DRXS350"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_DRXS350"] = "----";
                    sItem["HG_DRXS350"] = "----";
                    sItem["W_DRXS350"] = "----";
                }
                #endregion

            }

            #region 最终结果
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion

            #endregion
        }
    }
}
