
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZBL:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_ZBL_DJ"];
            var data = retData;

            var SItem = data["S_ZBL"];
            var MItem = data["M_ZBL"];
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

                #region 露点
                if (jcxm.Contains("、露点、"))
                {
                    jcxmCur = "露点";
                    sItem["G_LD"] = "<-40℃";
                    sItem["HG_LD"] = "合格";
                    List<double> iArray = new List<double>();
                    for (int i = 1; i < 16; i++)
                    {
                        if (Conversion.Val(sItem["LDWD" + i]) > -40)
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_LD"] = "不合格";
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["G_LD"] = "----";
                    sItem["HG_LD"] = "----";
                    //sItem["W_LD"] = "----";
                }
                #endregion

                #region 耐紫外线辐照
                if (jcxm.Contains("、耐紫外线辐照、"))
                {
                    jcxmCur = "耐紫外线辐照";
                    sItem["G_NZWXFZ"] = "无结雾、水气凝结或污染的痕迹且密封胶无明显变形";
                    sItem["HG_NZWXFZ"] = "合格";
                    for (int i = 1; i < 5; i++)
                    {
                        if (sItem["NZWXFZ"+i] == "否" )
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_NZWXFZ"] = "不合格";
                            mAllHg = false;
                        }
                        else if (string.IsNullOrEmpty(sItem["NZWXFZ" + i]))
                        {
                            sItem["NZWXFZ" + i] = "----";
                        }
                    }
                }
                else
                {
                    sItem["G_NZWXFZ"] = "----";
                    //sItem["W_NZWXFZ"] = "----";
                    sItem["HG_NZWXFZ"] = "----";
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
