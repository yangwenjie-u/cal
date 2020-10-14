using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JJR:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_JJR_DJ"];
            var data = retData;

            var SItem = data["S_JJR"];
            var MItem = data["M_JJR"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_YSQD"] = extraFieldsDj["YSQD"];
                    sItem["G_XSL"] = extraFieldsDj["XSL"];
                    sItem["G_CCWDX"] = extraFieldsDj["CCWDX"];
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

                #region 尺寸稳定性
                if (jcxm.Contains("、尺寸稳定性、"))
                {
                    jcxmCur = "尺寸稳定性";
                    if (IsQualified(MItem[0]["G_CCWDX"], sItem["W_CCWDX"], false) == "合格")
                    {
                        MItem[0]["HG_CCWDX"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_CCWDX"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["G_CCWDX"] = "----";
                    MItem[0]["HG_CCWDX"] = "----";
                    sItem["W_CCWDX"] = "----";
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
