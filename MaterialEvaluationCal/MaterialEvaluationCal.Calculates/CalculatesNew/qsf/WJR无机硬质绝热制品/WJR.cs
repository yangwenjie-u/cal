using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class WJR:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_WJR_DJ"];
            var data = retData;

            var SItem = data["S_WJR"];
            var MItem = data["M_WJR"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_MD"] = extraFieldsDj["MD"];
                    sItem["G_XSL"] = extraFieldsDj["XSL"];
                    sItem["G_HSL"] = extraFieldsDj["HSL"];
                    sItem["G_KZQD"] = extraFieldsDj["KZQD"];
                    sItem["G_KYQD"] = extraFieldsDj["KYQD"];
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
                    List<double> iArray = new List<double>();
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["MD" + i] = Math.Round(Conversion.Val(sItem["ZL" + i]) / ((Conversion.Val(sItem["CD" + i]) / 1000 * Conversion.Val(sItem["KD" + i]) / 1000 * Conversion.Val(sItem["HD" + i]) / 1000)), 0).ToString("0");
                        iArray.Add(Conversion.Val(sItem["MD" + i]));
                    }
                    sItem["W_MD"] = iArray.Average().ToString();
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

                #region 含水率
                if (jcxm.Contains("、含水率、"))
                {
                    jcxmCur = "含水率";
                    if (IsQualified(sItem["G_HSL"],sItem["W_HSL"],false)== "合格")
                    {
                        sItem["HG_HSL"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_HSL"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_HSL"] = "----";
                    sItem["W_HSL"] = "----";
                    sItem["HG_HSL"] = "----";
                }
                #endregion

                #region 抗折强度
                if (jcxm.Contains("、抗折强度、"))
                {
                    jcxmCur = "抗折强度";
                    if (IsQualified(sItem["G_KZQD"],sItem["W_KZQD"])== "合格")
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

                #region 吸水率
                if (jcxm.Contains("、吸水率、"))
                {
                    jcxmCur = "吸水率";
                    if (IsQualified(sItem["G_XSL"], sItem["W_XSL"], false) == "合格")
                    {
                        sItem["HG_XSL"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_XSL"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_XSL"] = "----";
                    sItem["W_XSL"] = "----";
                    sItem["HG_XSL"] = "----";
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
