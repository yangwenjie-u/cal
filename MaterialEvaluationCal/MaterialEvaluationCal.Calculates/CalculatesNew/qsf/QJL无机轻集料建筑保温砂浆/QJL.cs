using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class QJL:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_QJL_DJ"];
            var data = retData;

            var SItem = data["S_QJL"];
            var MItem = data["M_QJL"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim() && u["LX"] == sItem["LX"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_MD"] = extraFieldsDj["MD"];
                    sItem["G_KYQD"] = extraFieldsDj["KYQD"];
                    sItem["G_DRXS"] = extraFieldsDj["DRXS"];
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 干密度
                if (jcxm.Contains("、干密度、"))
                {
                    jcxmCur = "干密度";
                    List<double> iArray = new List<double>();
                    for (int i = 1; i < 7; i++)
                    {
                        sItem["MD" + i] = Math.Round(Conversion.Val(sItem["ZL" + i])/1000 / ((Conversion.Val(sItem["CD" + i]) / 1000 * Conversion.Val(sItem["KD" + i]) / 1000 * Conversion.Val(sItem["HD" + i]) / 1000)), 0).ToString("0");
                        iArray.Add(Conversion.Val(sItem["MD" + i]));
                    }
                    sItem["W_MD"] = iArray.Average().ToString("0.0");
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

                #region 导热系数
                if (jcxm.Contains("、导热系数、"))
                {
                    jcxmCur = "导热系数";
                    if (IsQualified(sItem["G_DRXS"], sItem["W_DRXS"]) == "合格")
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
                    sItem["HG_DRXS"] = "----";
                    sItem["G_DRXS"] = "----";
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
