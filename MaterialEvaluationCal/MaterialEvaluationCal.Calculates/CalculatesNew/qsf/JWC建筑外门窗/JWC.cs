using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class JWC : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_JWC_DJ"];
            var data = retData;

            var SItem = data["S_JWC"];
            var MItem = data["M_JWC"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["CRXSJB"] == sItem["CRXSJB"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_CRXS"] = extraFieldsDj["G_CRXS"];
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 保温性能（传热系数)
                if (jcxm.Contains("、保温性能（传热系数)、"))
                {
                    jcxmCur = "保温性能（传热系数)";
                    var mrsDj_Filter = extraDJ[0];
                    int Gs = extraDJ.Count();
                    string bl = "";
                    int xd = 0;
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (IsQualified(mrsDj_Filter["G_CRXS"], sItem["W_CRXS"], true) == "符合")
                        {
                            bl = mrsDj_Filter["CRXSJB"];
                            sItem["W_CRXSJB"] = mrsDj_Filter["CRXSJB"] + "级";
                            break;
                        }
                        if (xd >= extraDJ.Count())
                            mrsDj_Filter = extraDJ[xd - 1];
                        else
                            mrsDj_Filter = extraDJ[xd];

                    }
                    if (xd > Gs) sItem["W_CRXSJB"] = "不符合任一级别";

                    if (bl != sItem["CRXSJB"])
                    {
                        sItem["HG_CRXS"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                    else
                    {
                        sItem["HG_CRXS"] = "合格";
                    }

                }
                else
                {
                    sItem["G_CRXS"] = "----";
                    sItem["HG_CRXS"] = "----";
                    sItem["W_CRXS"] = "----";
                    sItem["W_CRXSJB"] = "----";
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
