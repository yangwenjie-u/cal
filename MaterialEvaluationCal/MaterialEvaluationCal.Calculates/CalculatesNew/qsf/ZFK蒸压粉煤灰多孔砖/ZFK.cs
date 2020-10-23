using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class ZFK:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_ZFK_DJ"];
            var data = retData;

            var SItem = data["S_ZFK"];
            var MItem = data["M_ZFK"];
            var jcxmBhg = "";
            var jcxmCur = "";
            string minKYQD = "",minKZQD="";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["QDDJ"] == sItem["SJDJ"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_KYQD"] = extraFieldsDj["QDYQ"];
                    minKYQD = extraFieldsDj["MINKYQD"];
                    sItem["G_KZQD"] = extraFieldsDj["KZQD"];
                    minKZQD = extraFieldsDj["MINKZQD"];
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                    if (IsQualified(sItem["G_KYQD"], sItem["W_KYQD"]) == "合格" && IsQualified(minKYQD, sItem["MINKYQD"], false) == "合格")
                    {
                        sItem["HG_KYQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KYQD"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_KYQD"] = "抗压强度" + sItem["G_KYQD"] + "MPa,单块最小值" + minKYQD + "MPa";
                }
                else
                {
                    sItem["HG_KYQD"] = "----";
                    sItem["G_KYQD"] = "----";
                    sItem["W_KYQD"] = "----";
                    sItem["MINKYQD"] = "----";
                }
                #endregion

                #region 抗折强度
                if (jcxm.Contains("、抗折强度、"))
                {
                    jcxmCur = "抗折强度";
                    if (IsQualified(sItem["G_KZQD"], sItem["W_KZQD"]) == "合格" && IsQualified(minKZQD, sItem["MINKZQD"], false) == "合格")
                    {
                        sItem["HG_KZQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KZQD"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_KZQD"] = "抗折强度" + sItem["G_KZQD"] + "MPa,单块最小值" + minKZQD + "MPa";
                }
                else
                {
                    sItem["HG_KZQD"] = "----";
                    sItem["G_KZQD"] = "----";
                    sItem["W_KZQD"] = "----";
                    sItem["MINKZQD"] = "----";
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
