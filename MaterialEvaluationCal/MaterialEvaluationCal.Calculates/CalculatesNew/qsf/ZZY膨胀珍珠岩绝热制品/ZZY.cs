using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class ZZY : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_ZZY_DJ"];
            var data = retData;

            var SItem = data["S_ZZY"];
            var MItem = data["M_ZZY"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MDFL"] == sItem["MDFL"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_MD"] = extraFieldsDj["G_MD"];
                    sItem["G_DRXS25"] = extraFieldsDj["G_DRXS25"];
                    sItem["G_DRXS350"] = extraFieldsDj["G_DRXS350"];
                    sItem["G_KZQD"] = extraFieldsDj["G_KZQD"];
                    sItem["G_KYQD"] = extraFieldsDj["G_KYQD"];
                    sItem["G_ZLHSL"] = extraFieldsDj["G_ZLHSL"];
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 密度
                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";
                    if (IsQualified(sItem["G_MD"], sItem["W_MD"], false) == "合格")
                    {
                        sItem["HG_MD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_MD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_MD"] = "----";
                    sItem["HG_MD"] = "----";
                    sItem["W_MD"] = "----";
                }
                #endregion

                #region 质量含水率
                if (jcxm.Contains("、质量含水率、"))
                {
                    jcxmCur = "质量含水率";
                    if (IsQualified(sItem["G_ZLHSL"], sItem["W_ZLHSL"], false) == "合格")
                    {
                        sItem["HG_ZLHSL"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_ZLHSL"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_ZLHSL"] = "----";
                    sItem["W_ZLHSL"] = "----";
                    sItem["HG_ZLHSL"] = "----";
                }
                #endregion

                #region 抗折强度
                if (jcxm.Contains("、抗折强度、"))
                {
                    jcxmCur = "抗折强度";
                    if (IsQualified(sItem["G_KZQD"], sItem["W_KZQD"]) == "合格")
                    {
                        sItem["HG_KZQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KZQD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["HG_KZQD"] = "----";
                    sItem["G_KZQD"] = "----";
                    sItem["W_KZQD"] = "----";
                }
                #endregion

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                    if (IsQualified(sItem["G_KYQD"], sItem["W_KYQD"]) == "合格")
                    {
                        sItem["HG_KYQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KYQD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["HG_KYQD"] = "----";
                    sItem["G_KYQD"] = "----";
                    sItem["W_KYQD"] = "----";
                }
                #endregion

                #region 导热系数25℃
                if (jcxm.Contains("、导热系数25℃、"))
                {
                    jcxmCur = "导热系数25℃";
                    if (IsQualified(sItem["G_DRXS25"], sItem["W_DRXS25"], false) == "合格")
                    {
                        sItem["HG_DRXS25"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_DRXS25"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_DRXS25"] = "----";
                    sItem["W_DRXS25"] = "----";
                    sItem["HG_DRXS25"] = "----";
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
                    sItem["W_DRXS350"] = "----";
                    sItem["HG_DRXS350"] = "----";
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
