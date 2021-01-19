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
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJDJ"].Trim());
                if (null != extraFieldsDj)
                {
                    //sItem["G_GZSSZ"] = extraFieldsDj["GZSSZ"];
                    sItem["G_KYQD"] = extraFieldsDj["SZ"];
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
                        sItem["G_GZSSZ"] = "≤" + sItem["G_GZSSZ"].Trim().Replace("≤", "");
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

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                    List<double> iArray = new List<double>();
                    for (int i = 1; i < 4; i++)
                    {
                        iArray.Add(Conversion.Val(sItem["KYQD" + i]));
                    }
                    iArray.Sort();
                    if ((iArray.Max() - iArray.Average()) > iArray.Average() * 0.15 || (iArray.Average() - iArray.Min()) > iArray.Average() * 0.15)
                    {
                        sItem["W_KYQD"] = iArray[1].ToString("0.0");
                    }
                    else if ((iArray.Max() - iArray.Average()) > iArray.Average() * 0.15 && (iArray.Average() - iArray.Min()) > iArray.Average() * 0.15)
                    {
                        sItem["W_KYQD"] = "----";
                    }
                    else
                    {
                        sItem["W_KYQD"] = (iArray.Average() * 1.3).ToString("0.0");
                    }

                    if (sItem["W_KYQD"] == "----")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KYQD"] = "结果无效";
                        mAllHg = false;
                    }
                    else
                    {
                        sItem["G_KYQD"] = "≥" + Conversion.Val(sItem["G_KYQD"].Trim()).ToString("0").Replace("≥", "");
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
                }
                else
                {
                    sItem["G_KYQD"] = "----";
                    sItem["HG_KYQD"] = "----";
                    sItem["W_KYQD"] = "----";
                }
                #endregion

                #region 抗渗性能
                if (sItem["JCXM"].Contains("抗渗性能"))
                {
                    if (Conversion.Val(sItem["KSKYQD1"]) > 0)
                    {
                        sItem["W_KSXN"] = "P" + (Conversion.Val(sItem["KSKYQD1"]) * 10 - 1).ToString();
                    }
                }
                else
                {
                    sItem["W_KSXN"] = "----";
                }
                #endregion

                #region 拉伸粘结强度
                if (sItem["JCXM"].Contains("拉伸粘结强度"))
                {
                    List<double> larray = new List<double>();
                    for (int i = 1; i < 11; i++)
                    {
                        sItem["LSQDQD" + i] = Math.Round(Conversion.Val(sItem["LSQDHZ" + i]) / (Conversion.Val(sItem["LSQDCD" + i]) * Conversion.Val(sItem["LSQDKD" + i])), 3).ToString();
                        larray.Add(GetSafeDouble(sItem["LSQDQD" + i]));
                    }

                    int n = 0;
                    double avg = 0;
                    avg = larray.Average();
                    for (int i = 0; i < larray.Count; i++)
                    {
                        if (Math.Abs((larray[i] - avg) / avg * 100) > 20)
                        {
                            larray.Remove(larray[i]);
                            n++;

                        }
                    }
                    if (n < 4 && n != 0)
                    {
                        sItem["LSQDQD"] = "结果无效";
                    }
                    else
                    {
                        sItem["LSQDQD"] = Math.Round(larray.Average(), 2).ToString("0.00");
                    }
                    sItem["G_LSQDQD"] = "≤" + sItem["G_LSQDQD"].Trim().Replace("≤", "");
                    if (IsQualified(sItem["G_LSQDQD"], sItem["LSQDQD"], false) == "合格")
                    {
                        sItem["HG_LSQDQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_LSQDQD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["LSQDQD"] = "----";
                    sItem["G_LSQDQD"] = "----";
                    sItem["HG_LSQDQD"] = "----";
                }
                #endregion

                #region 2h稠度损失率
                if (jcxm.Contains("、2h稠度损失率、"))
                {
                    jcxmCur = "2h稠度损失率";
                    sItem["G_2HCDSSL"] = "≤" + sItem["G_2HCDSSL"].Trim().Replace("≤", "");
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

                #region 稠度
                if (jcxm.Contains("、稠度、"))
                {
                    jcxmCur = "稠度";
                    sItem["CD"] = sItem["CD"].Trim().Replace("(mm)", "");
                    if (IsQualified(sItem["CD"], sItem["W_CD"], false) == "合格")
                    {
                        sItem["HG_CD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_CD"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["CD"] = sItem["CD"] + "(mm)";
                }
                else
                {
                    sItem["CD"] = "----";
                    sItem["HG_CD"] = "----";
                    sItem["W_CD"] = "----";
                }
                #endregion

                #region 分层度
                if (jcxm.Contains("、分层度、"))
                {
                    jcxmCur = "分层度";
                    if (IsQualified(sItem["G_FCD"], sItem["FCD"], false) == "合格")
                    {
                        sItem["HG_FCD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_FCD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_FCD"] = "----";
                    sItem["HG_FCD"] = "----";
                    sItem["FCD"] = "----";
                }
                #endregion

                #region 保水性
                if (jcxm.Contains("、保水性、"))
                {
                    jcxmCur = "保水性";
                    sItem["G_BSX"] = "≥" + sItem["G_BSX"].Trim().Replace("≥", "");
                    if (IsQualified(sItem["G_BSX"], sItem["W_BSX"], false) == "合格")
                    {
                        sItem["HG_BSX"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_BSX"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_BSX"] = "----";
                    sItem["HG_BSX"] = "----";
                    sItem["W_BSX"] = "----";
                }
                #endregion

                #region 吸水率
                if (jcxm.Contains("、吸水率、"))
                {
                    jcxmCur = "吸水率";
                    sItem["G_XSL"] = "≤" + sItem["G_XSL"].Trim().Replace("≤", "");
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

        public void GxJCJGMS()
        {
            //富阳德浩
            #region
            //var extraDJ = dataExtra["BZ_ZS_DJ"].OrderBy(x => x["G_QDPJ"]).ToList();

            var data = retData;
            var jsbeizhu = "该组试样的检测结果全部合格";
            var SItems = data["S_JZJ"];
            var MItem = data["M_JZJ"];

            var mAllHg = true;
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            string sjdj = "";

            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                #region 干燥收缩值
                if (jcxm.Contains("、干燥收缩值、"))
                {
                    jcxmCur = "干燥收缩值";
                    if (string.IsNullOrEmpty(sItem["G_GZSSZ"]))
                    {
                        sItem["G_GZSSZ"] = "----";
                        sItem["HG_GZSSZ"] = "----";
                    }
                    if (sItem["HG_GZSSZ"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                #endregion

                #region 抗渗性能
                if (jcxm.Contains("、抗渗性能、"))
                {
                    jcxmCur = "抗渗性能";
                    if (string.IsNullOrEmpty(sItem["G_KSDJ"]))
                    {
                        sItem["G_KSDJ"] = "----";
                        sItem["HG_KSDJ"] = "----";
                    }
                    else
                    {
                        if (sItem["G_KSDJ"].Trim() == sItem["W_KSXN"].Trim())
                        {
                            sItem["HG_KSDJ"] = "符合";
                        }
                        else
                        {
                            sItem["HG_KSDJ"] = "不符合";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                        }
                    }
                }
                #endregion

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                    if (string.IsNullOrEmpty(sItem["G_KYQD"]))
                    {
                        sItem["G_KYQD"] = "----";
                        sItem["HG_KYQD"] = "----";
                    }
                    if (sItem["HG_KYQD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                #endregion

                #region 拉伸粘结强度
                if (jcxm.Contains("、拉伸粘结强度、"))
                {
                    jcxmCur = "拉伸粘结强度";
                    if (string.IsNullOrEmpty(sItem["G_LSQDQD"]))
                    {
                        sItem["G_LSQDQD"] = "----";
                        sItem["HG_LSQDQD"] = "----";
                    }
                    if (sItem["HG_LSQDQD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                #endregion

                #region 稠度
                if (jcxm.Contains("、稠度、"))
                {
                    jcxmCur = "稠度";
                    if (string.IsNullOrEmpty(sItem["CD"]))
                    {
                        sItem["CD"] = "----";
                        sItem["HG_CD"] = "----";
                    }
                    if (sItem["HG_CD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                #endregion

                #region 分层度
                if (jcxm.Contains("、分层度、"))
                {
                    jcxmCur = "分层度";
                    if (string.IsNullOrEmpty(sItem["G_FCD"]))
                    {
                        sItem["G_FCD"] = "----";
                        sItem["HG_FCD"] = "----";
                    }
                    if (sItem["HG_FCD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                #endregion

                #region 保水性
                if (jcxm.Contains("、保水性、"))
                {
                    jcxmCur = "保水性";
                    if (string.IsNullOrEmpty(sItem["G_BSX"]))
                    {
                        sItem["G_BSX"] = "----";
                        sItem["HG_BSX"] = "----";
                    }
                    if (sItem["HG_BSX"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                #endregion

                #region 吸水率
                if (jcxm.Contains("、吸水率、"))
                {
                    jcxmCur = "吸水率";
                    if (string.IsNullOrEmpty(sItem["G_XSL"]))
                    {
                        sItem["G_XSL"] = "----";
                        sItem["HG_XSL"] = "----";
                    }
                    if (sItem["HG_XSL"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                #endregion

                #region 2h稠度损失率
                if (jcxm.Contains("、2h稠度损失率、"))
                {
                    jcxmCur = "2h稠度损失率";
                    if (string.IsNullOrEmpty(sItem["G_2HCDSSL"]))
                    {
                        sItem["G_2HCDSSL"] = "----";
                        sItem["HG_2HCDSSL"] = "----";
                    }
                    if (sItem["HG_2HCDSSL"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                #endregion

            }
            if (!mAllHg)
            {
                MItem[0]["ZZJG"] = "不合格";
            }
            if (MItem[0]["ZZJG"] == "不合格")
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["JCYJ"] + "进行检测，所检项目" + jcxmBhg.TrimEnd('、') + "不符合委托方提供技术要求。";
            }
            else if (MItem[0]["ZZJG"] == "符合委托方提供技术要求")
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["JCYJ"] + "进行检测，所检项目符合委托方提供技术要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["JCYJ"] + "进行检测，所检项目只提供实测结果，不做判定。";
            }

            #endregion
        }
    }
}
