using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SMB : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_SMB_DJ"];
            var data = retData;

            var SItem = data["S_SMB"];
            var MItem = data["M_SMB"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_YSQD"] = extraFieldsDj["G_YSQD"];
                    sItem["G_XSL"] = extraFieldsDj["G_XSL"];
                    sItem["G_MD"] = extraFieldsDj["G_MD"];
                    sItem["G_DRXS"] = extraFieldsDj["G_DRXS"];
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 压缩强度
                if (jcxm.Contains("、压缩强度、"))
                {
                    jcxmCur = "压缩强度";
                    if (IsQualified(sItem["G_YSQD"], sItem["W_YSQD"], false) == "合格")
                    {
                        sItem["HG_YSQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_YSQD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_YSQD"] = "----";
                    sItem["HG_YSQD"] = "----";
                    sItem["W_YSQD"] = "----";
                }
                #endregion

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
                    sItem["HG_XSL"] = "----";
                    sItem["W_XSL"] = "----";
                }
                #endregion

                #region 表观密度
                if (jcxm.Contains("、表观密度、"))
                {
                    jcxmCur = "表观密度";
                    if (IsQualified(sItem["G_MD"], sItem["W_MDPC"], false) == "合格")
                    {
                        sItem["GH_MD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_MD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_MD"] = "----";
                    sItem["GH_MD"] = "----";
                    sItem["W_MDPC"] = "----";
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
