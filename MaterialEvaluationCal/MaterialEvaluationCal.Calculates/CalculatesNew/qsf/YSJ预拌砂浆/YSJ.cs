using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class YSJ : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_YSJ_DJ"];
            var data = retData;

            var SItem = data["S_YSJ"];
            var MItem = data["M_YSJ"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJLX"].Trim());
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                    extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJLX"].Trim() && u["QDDJ"] == sItem["SJDJ"].Trim());
                    if (extraFieldsDj != null)
                    {
                        sItem["G_KYQD"] = extraFieldsDj["G_28DKYQD"];
                    }
                    else
                    {
                        sItem["G_KYQD"] = "----";
                    }
                    List<double> iArray = new List<double>();
                    for (int i = 1; i < 4; i++)
                    {
                        iArray.Add(Conversion.Val(sItem["KYQD" + i]));
                    }
                    iArray.Sort();
                    if ((iArray.Max() - iArray[1]) > iArray[1] * 0.15 || (iArray[1] - iArray.Min()) > iArray[1] * 0.15)
                    {
                        sItem["W_KYQD"] = (Round(iArray[1], 1) * 1.5).ToString();
                    }
                    else
                    {
                        sItem["W_KYQD"] = (Round(iArray.Average(), 1) * 1.5).ToString();
                    }

                    if (IsQualified(sItem["G_KYQD"], sItem["W_KYQD"], false) == "合格")
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
                    sItem["G_KYQD"] = "----";
                    sItem["HG_KYQD"] = "----";
                    sItem["W_KYQD"] = "----";
                }
                #endregion

                #region 稠度
                if (jcxm.Contains("、稠度、"))
                {
                    jcxmCur = "稠度";
                    if (Conversion.Val(sItem["GDCD"]) < 100)
                    {
                        sItem["G_CD"] = "±10";
                    }
                    else
                    {
                        sItem["G_CD"] = "-10～+5";
                    }

                    if (IsQualified(sItem["G_CD"], (Conversion.Val(sItem["GDCD"]) - Conversion.Val(sItem["W_CD"])).ToString(), false) == "合格")
                    {
                        sItem["HG_CD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_CD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_CD"] = "----";
                    sItem["HG_CD"] = "----";
                    sItem["W_CD"] = "----";
                }
                #endregion

                #region 2h稠度损失率
                if (jcxm.Contains("、2h稠度损失率、"))
                {
                    jcxmCur = "2h稠度损失率";
                    extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJLX"].Trim());
                    if (extraFieldsDj != null)
                    {
                        sItem["G_2HCDSSL"] = extraFieldsDj["G_2HCDSSL"];
                    }
                    else
                    {
                        sItem["G_2HCDSSL"] = "----";
                    }

                    if (IsQualified(sItem["G_2HCDSSL"], sItem["EHCDSSL"], false) == "合格")
                    {
                        sItem["HG_2HCDSSL"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_2HCDSSL"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_2HCDSSL"] = "----";
                    sItem["HG_2HCDSSL"] = "----";
                    sItem["EHCDSSL"] = "----";
                }
                #endregion

                #region 保水率
                if (jcxm.Contains("、保水率、"))
                {
                    jcxmCur = "保水率";
                    extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJLX"].Trim());
                    if (extraFieldsDj != null)
                    {
                        sItem["G_BSL"] = extraFieldsDj["G_BSL"];
                    }
                    else
                    {
                        sItem["G_BSL"] = "----";
                    }

                    if (IsQualified(sItem["G_BSL"], sItem["W_BSL"], false) == "合格")
                    {
                        sItem["HG_BSL"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_BSL"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_BSL"] = "----";
                    sItem["HG_BSL"] = "----";
                    sItem["W_BSL"] = "----";
                }
                #endregion

                #region 14d拉伸粘结强度
                if (jcxm.Contains("、14d拉伸粘结强度、"))
                {
                    jcxmCur = "14d拉伸粘结强度";
                    extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJLX"].Trim());
                    if (extraFieldsDj != null)
                    {
                        sItem["G_14DLSNJQD"] = extraFieldsDj["G_14LSNJQD"];
                    }
                    else
                    {
                        sItem["G_14DLSNJQD"] = "----";
                    }
                    List<double> iArray = new List<double>();
                    for (int i = 1; i < 11; i++)
                    {
                        iArray.Add(Conversion.Val(sItem["LSNJQD" + i]));
                    }
                    int n = 0;
                    double avg = 0;
                    avg = iArray.Average();
                    for (int i = 0; i < iArray.Count; i++)
                    {
                        if (Math.Abs((iArray[i] - avg) / avg * 100) > 20)
                        {
                            iArray.Remove(iArray[i]);
                            n++;

                        }
                    }
                    if (n > 4)
                    {
                        sItem["W_14DLSNJQD"] = "结果无效";
                    }
                    else
                    {
                        sItem["W_14DLSNJQD"] = Math.Round(iArray.Average(), 2).ToString("0.00");
                    }

                    if (IsQualified(sItem["G_14DLSNJQD"], sItem["W_14DLSNJQD"], false) == "合格")
                    {
                        sItem["HG_14DLSNJQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_14DLSNJQD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["HG_14DLSNJQD"] = "----";
                    sItem["G_14DLSNJQD"] = "----";
                    sItem["W_14DLSNJQD"] = "----";
                }
                #endregion

                #region 28d抗渗压力
                if (jcxm.Contains("、28d抗渗压力、"))
                {
                    jcxmCur = "28d抗渗压力";
                    extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJLX"].Trim() && u["KSDJ"] == sItem["KSDJ"].Trim());
                    if (extraFieldsDj != null)
                    {
                        sItem["G_KSXN"] = extraFieldsDj["G_28DKSYL"];
                    }
                    else
                    {
                        sItem["G_KSXN"] = "----";
                    }
                    if (Conversion.Val(sItem["KSKYQD1"]) > 0)
                    {
                        sItem["W_KSXN"] = (Conversion.Val(sItem["KSKYQD1"])-0.1).ToString();
                    }
                    if (IsQualified(sItem["G_KSXN"], sItem["W_KSXN"], false) == "合格")
                    {
                        sItem["HG_KSXN"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KSXN"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["W_KSXN"] = "----";
                    sItem["G_KSXN"] = "----";
                    sItem["HG_KSXN"] = "----";
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
