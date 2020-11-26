using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class CG:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            //var extraDJ = dataExtra["BZ_CG_DJ"];
            var data = retData;

            var SItem = data["S_CG"];
            var MItem = data["M_CG"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim());
                //if (null != extraFieldsDj)
                //{
                //    sItem["G_MD"] = extraFieldsDj["MD"];
                //    sItem["G_XSL"] = extraFieldsDj["XSL"];
                //    sItem["G_HSL"] = extraFieldsDj["HSL"];
                //    sItem["G_KZQD"] = extraFieldsDj["KZQD"];
                //    sItem["G_KYQD"] = extraFieldsDj["KYQD"];
                //}
                //else
                //{
                //    mAllHg = false;
                //    mjcjg = "不下结论";
                //    jsbeizhu = jsbeizhu + "依据不详";
                //    continue;
                //}

                #region 层高
                if (jcxm.Contains("、层高、"))
                {
                    jcxmCur = "层高";
                    sItem["G_GD"] = "±10";
                    if (IsQualified(sItem["G_GD"], sItem["PJGD"], false) == "合格")
                    {
                        sItem["HG_GD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_GD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_GD"] = "----";
                    sItem["HG_GD"] = "----";
                    sItem["PJGD"] = "----";
                }
                #endregion

                #region 厚度
                if (jcxm.Contains("、厚度、"))
                {
                    jcxmCur = "厚度";
                    sItem["G_HD"] = "-5～+10";
                    if (IsQualified(sItem["G_HD"], sItem["HDPC"], false) == "合格")
                    {
                        sItem["HG_HD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_HD"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_HD"] = "+10,-5";
                }
                else
                {
                    sItem["G_HD"] = "----";
                    sItem["PJHD"] = "----";
                    sItem["HG_HD"] = "----";
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
