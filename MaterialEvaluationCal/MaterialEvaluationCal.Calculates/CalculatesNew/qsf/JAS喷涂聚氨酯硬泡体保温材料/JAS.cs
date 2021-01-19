using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
     public class JAS:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_JAS_DJ"];
            var data = retData;

            var SItem = data["S_JAS"];
            var MItem = data["M_JAS"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["LB"] == sItem["LB"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_MD"] = extraFieldsDj["G_MD"];
                    sItem["G_DRXS"] = extraFieldsDj["G_DRXS"];
                    sItem["G_NJQD"] = extraFieldsDj["G_NJQD"];
                    sItem["G_KYQD"] = extraFieldsDj["G_KYQD"];
                    sItem["G_XSL"] = extraFieldsDj["G_XSL"];
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

                #region 粘结强度
                if (jcxm.Contains("、粘结强度、"))
                {
                    jcxmCur = "粘结强度";
                    if (IsQualified(sItem["G_NJQD"], sItem["W_NJQD"]) == "合格")
                    {
                        sItem["HG_NJQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_NJQD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["HG_NJQD"] = "----";
                    sItem["G_NJQD"] = "----";
                    sItem["W_NJQD"] = "----";
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
                if (jcxm.Contains("、导热系数"))
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
                    sItem["W_DRXS"] = "----";
                    sItem["HG_DRXS"] = "----";
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
