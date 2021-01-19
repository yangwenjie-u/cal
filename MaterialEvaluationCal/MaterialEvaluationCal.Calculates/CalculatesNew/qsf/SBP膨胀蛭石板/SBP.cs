using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SBP:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_SBP_DJ"];
            var data = retData;

            var SItem = data["S_SBP"];
            var MItem = data["M_SBP"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim() && u["SJDJ"] == sItem["SJDJ"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_DRXS"] = extraFieldsDj["G_DRXS"];
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 导热系数
                if (jcxm.Contains("、导热系数、"))
                {
                    jcxmCur = "导热系数";
                    if (IsQualified(sItem["G_DRXS"], sItem["W_DRXS"], false) == "合格")
                    {
                        sItem["HG_DRXS"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_DRXS"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_DRXS"] = "----";
                    sItem["HG_DRXS"] = "----";
                    sItem["W_DRXS"] = "----";
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
