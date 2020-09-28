using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JZJ : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_JZJ_DJ"];
            var data = retData;

            var SItem = data["S_JZJ"];
            var MItem = data["M_JZJ"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_GZSSZ"] = extraFieldsDj["GZSSZ"];
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 干燥收缩值
                if (jcxm.Contains("、干燥收缩值、"))
                {
                    jcxmCur = "干燥收缩值";
                    List<double> iArray = new List<double>();
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["GZSSZ" + i] = Math.Round((Conversion.Val(sItem["D7SSZ" + i]) + Conversion.Val(sItem["D14SSZ" + i]) + Conversion.Val(sItem["D21SSZ" + i]) + Conversion.Val(sItem["D28SSZ" + i]) + Conversion.Val(sItem["D56SSZ" + i]) + Conversion.Val(sItem["D90SSZ" + i])) / 6, 5).ToString("0.00000");
                        iArray.Add(Conversion.Val(sItem["GZSSZ" + i]));
                    }
                    sItem["W_GZSSZ"] = iArray.Average().ToString("0.00000");

                    int j = 3;
                    for (int i = 0; i < j; i++)
                    {
                        if (Math.Abs(iArray[i] - Conversion.Val(sItem["W_GZSSZ"])) > Conversion.Val(sItem["W_GZSSZ"]) * 0.2)
                        {
                            iArray.Remove(iArray[i]);
                            i--;
                            j--;

                        }
                    }
                    if (iArray.Count >= 2)
                    {
                        sItem["W_GZSSZ"] = iArray.Average().ToString("0.00000");
                        if (IsQualified(sItem["G_GZSSZ"], sItem["W_GZSSZ"], false) == "合格")
                        {
                            sItem["HG_GZSSZ"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_GZSSZ"] = "不合格";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["HG_GZSSZ"] = "试件无效";
                        sItem["W_GZSSZ"] = "----";
                    }

                }
                else
                {
                    sItem["G_GZSSZ"] = "----";
                    sItem["HG_GZSSZ"] = "----";
                    sItem["W_GZSSZ"] = "----";
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
