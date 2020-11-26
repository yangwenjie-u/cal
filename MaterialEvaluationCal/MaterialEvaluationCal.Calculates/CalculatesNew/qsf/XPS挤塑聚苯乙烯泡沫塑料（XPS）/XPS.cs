using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class XPS:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_XPS_DJ"];
            var drxsDJ = dataExtra["BZ_BBDRXS"];
            var data = retData;

            var SItem = data["S_XPS"];
            var MItem = data["M_XPS"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim() && u["SJDJ"] == sItem["SJDJ"].Trim());
                if (null != extraFieldsDj)
                {
                    MItem[0]["G_YSQD"] = extraFieldsDj["G_YSQD"];
                    MItem[0]["G_XSL"] = extraFieldsDj["G_XSL"];
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
                    if (IsQualified(MItem[0]["G_YSQD"], sItem["W_YSQD"], false) == "合格")
                    {
                        MItem[0]["HG_YSQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_YSQD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["G_YSQD"] = "----";
                    MItem[0]["HG_YSQD"] = "----";
                    sItem["W_YSQD"] = "----";
                }
                #endregion

                #region 导热系数
                if (jcxm.Contains("、导热系数、"))
                {
                    jcxmCur = "导热系数";
                    var drxsItem = drxsDJ.FirstOrDefault(x => x["MC"] == sItem["CPMC"].Trim() && x["DJ"] == sItem["DRXSDJ"].Trim());
                    if (drxsItem != null)
                    {
                        MItem[0]["G_DRXS"] = drxsItem["DRXS25"];
                    }
                    else
                    {
                        MItem[0]["G_DRXS"] = "";
                    }

                    if (IsQualified(MItem[0]["G_DRXS"], sItem["W_DRXS"], false) == "合格")
                    {
                        MItem[0]["HG_DRXS"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_DRXS"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["G_DRXS"] = "----";
                    MItem[0]["HG_DRXS"] = "----";
                    sItem["W_DRXS"] = "----";
                }
                #endregion

                #region 吸水率
                if (jcxm.Contains("、吸水率、"))
                {
                    jcxmCur = "吸水率";
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
