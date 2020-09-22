using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class WBC : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_WBC_DJ"];
            var data = retData;

            var SItem = data["S_WBC"];
            var MItem = data["M_WBC"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim());
                if (null != extraFieldsDj)
                {
                    MItem[0]["G_LSQD"] = extraFieldsDj["LSQD"];
                    MItem[0]["G_XSL"] = extraFieldsDj["XSL"];
                    MItem[0]["G_KCJXSC"] = extraFieldsDj["KCJXSC"];
                    MItem[0]["G_KCJXEC"] = extraFieldsDj["KCJXEC"];
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 吸水量
                if (jcxm.Contains("、吸水量、"))
                {
                    jcxmCur = "吸水量";
                    if (IsQualified(MItem[0]["G_XSL"], sItem["W_XSL"], false) == "合格")
                    {
                        MItem[0]["HG_XSL"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_XSL"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["G_XSL"] = "----";
                    MItem[0]["HG_XSL"] = "----";
                    sItem["W_XSL"] = "----";
                }
                #endregion

                #region 抗冲击性首层
                if (jcxm.Contains("、抗冲击性首层、"))
                {
                    jcxmCur = "抗冲击性首层";
                    int n = 10;
                    for (int i = 1; i < 11; i++)
                    {
                        if (sItem["KCJSC" + i] == "是")
                        {
                            n--;
                        }
                    }
                    if (Conversion.Val(MItem[0]["G_KCJXSC"]) > 0 && n < Conversion.Val(MItem[0]["G_KCJXSC"]))
                    {
                        MItem[0]["HG_KCJSC"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_KCJSC"] = "不合格";
                        mAllHg = false;
                    }
                    MItem[0]["G_KCJXSC"] = "<" + MItem[0]["G_KCJXSC"];
                }
                else
                {
                    MItem[0]["G_KCJXSC"] = "----";
                    MItem[0]["HG_KCJSC"] = "----";
                }
                #endregion

                #region 抗冲击性二层及以上
                if (jcxm.Contains("、抗冲击性二层及以上、"))
                {
                    jcxmCur = "抗冲击性二层及以上";
                    int n = 10;
                    for (int i = 1; i < 11; i++)
                    {
                        if (sItem["KCJEC" + i] == "是")
                        {
                            n--;
                        }
                    }
                    if (Conversion.Val(MItem[0]["G_KCJXEC"]) > 0 && n < Conversion.Val(MItem[0]["G_KCJXEC"]))
                    {
                        MItem[0]["HG_KCJEC"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_KCJEC"] = "不合格";
                        mAllHg = false;
                    }
                    MItem[0]["G_KCJXEC"] = "<" + MItem[0]["G_KCJXEC"];
                }
                else
                {
                    MItem[0]["G_KCJXEC"] = "----";
                    MItem[0]["HG_KCJEC"] = "----";
                }
                #endregion

                #region 拉伸粘结强度
                if (jcxm.Contains("、拉伸粘结强度、"))
                {
                    jcxmCur = "拉伸粘结强度";
                    List<double> iArray = new List<double>();
                    for (int i = 1; i < 7; i++)
                    {
                        sItem["NJQD" + i] = Math.Round(Conversion.Val(sItem["HZ" + i]) / (Conversion.Val(sItem["CD" + i]) * Conversion.Val(sItem["KD" + i])), 2).ToString("0.00");
                        iArray.Add(Conversion.Val(sItem["NJQD" + i]));
                    }
                    iArray.Sort();
                    iArray.Remove(iArray.Max());
                    iArray.Remove(iArray.Min());
                    sItem["W_NJQD"] = Round(iArray.Average(), 2).ToString();
                    if (IsQualified(MItem[0]["G_LSQD"], sItem["W_NJQD"],false) == "合格")
                    {
                        MItem[0]["HG_NJQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_NJQD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["HG_NJQD"] = "----";
                    MItem[0]["G_LSQD"] = "----";
                    sItem["W_NJQD"] = "----";
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
