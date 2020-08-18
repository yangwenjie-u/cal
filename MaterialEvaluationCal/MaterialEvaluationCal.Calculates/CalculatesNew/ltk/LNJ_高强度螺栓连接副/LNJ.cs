using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class LNJ2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_LNJ_DJ"];
            //螺栓预拉力
            var extraYLL = dataExtra["BZ_LNJLSYLL"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_LNJ"];

            if (!data.ContainsKey("M_LNJ"))
            {
                data["M_LNJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_LNJ"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";

            #region 局部函数
            Func<double, double> myint = delegate (double dataChar)
            {
                return Math.Round(Conversion.Val(dataChar) / 5, 0) * 5;
            };
            #endregion


            double md, md1, md2, md3, sum, pjmd = 0;
            bool sign = true;
            bool flag = true;
            bool mSFwc = true;
            List<double> nArr = new List<double>();
            double xd, Gs = 0;

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["LSZL"] && u["LSXNDJ"] == sItem["LSXNDJ"] && u["GGXH"] == sItem["LWGG"]);
                var mrsYLL = extraYLL.FirstOrDefault(u => u["LSXNDJ"] == sItem["LSXNDJ"] && u["GGXH"] == sItem["LWGG"]);

                if (null == mrsDj || mrsYLL == null)
                {
                    jsbeizhu = "依据不详\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                #region 紧固轴力
                if (jcxm.Contains("、紧固轴力、"))
                {
                    jcxmCur = "紧固轴力";
                    var klhzVal = "0";
                    for (int i = 1; i < 9; i++)
                    {
                        klhzVal = sItem["KLHZ" + i];
                        if (!IsNumeric(klhzVal) || string.IsNullOrEmpty(klhzVal))
                        {
                            sign = false;
                            mSFwc = false;
                            throw new Exception("紧固轴力" + i + "数据异常，请检查。");
                        }
                    }
                    sItem["KLHZYQ"] = "平均值" + mrsDj["KPJ"] + ",标准差" + mrsDj["KBZC"];

                    nArr.Clear();

                    sum = 0;
                    for (int i = 1; i < 9; i++)
                    {
                        md = Conversion.Val(sItem["KLHZ" + i]);
                        sum += md;
                        nArr.Add(md);
                    }
                    pjmd = Math.Round(sum / 8, 2);

                    sum = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        md = nArr[i] - pjmd;
                        sum += Math.Pow(md, 2);
                    }
                    md1 = Math.Round(Math.Sqrt(sum / 7), 2);

                    sItem["KLHZBZC"] = md1.ToString("0.00");
                    sItem["KLHZPJ"] = pjmd.ToString("0.00");
                    sItem["KLHZPD"] = "----";

                    if (sItem["LSZL"].Contains("扭剪型"))
                    {
                        if ("符合" == IsQualified(mrsDj["KBZC"], sItem["KLHZBZC"], true) && "符合" == IsQualified(mrsDj["KPJ"], sItem["KLHZPJ"], true))
                        {
                            sItem["KLHZPD"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["KLHZPD"] = "不合格";
                        }

                        sItem["NJXSPD"] = sItem["KLHZPD"];
                    }
                }
                else
                {
                    sItem["KLHZPD"] = "----";
                    sItem["KLHZBZC"] = "----";
                    sItem["KLHZPJ"] = "----";
                    sItem["KLHZYQ"] = "----";
                    sItem["NJXSPD"] = "----";
                }
                #endregion

                #region 扭矩系数
                sign = true;
                if (jcxm.Contains("、扭矩系数、"))
                {
                    jcxmCur = "扭矩系数";
                    sItem["NJXSYQ"] = "扭矩系数平均值为" + mrsDj["KPJ"].Trim() + ",标准偏差应" +mrsDj["KBZC"];

                    nArr.Clear();
                    sum = 0;
                    for (int i = 1; i < 9; i++)
                    {
                        if (IsNumeric(sItem["ZNNJ" + i]) == false || IsNumeric(sItem["LSYLL" + i]) == false)
                        {
                            throw new Exception("螺栓预拉力" + i + "或终拧扭矩" + i + "数据异常，请检查。");
                        }
                        md1 = Conversion.Val(sItem["ZNNJ" + i]);
                        md2 = Conversion.Val(sItem["LSYLL" + i]);
                        md = Math.Round(md1 / md2 / Conversion.Val(mrsDj["ZJ"]), 3);
                        sItem["NJXS" + i] = md.ToString("0.000");
                        sum += md;
                        nArr.Add(md);
                    }
                    pjmd = Math.Round(sum / 8, 2);
                    sItem["KPJ"] = pjmd.ToString("0.00");

                    sum = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        md = nArr[i] - pjmd;
                        sum += Math.Pow(md, 2);
                    }

                    md1 = Math.Round(Math.Sqrt(sum / 7), 3);

                    sItem["KBZC"] = md1.ToString("0.000");

                    if ("符合" == IsQualified(mrsDj["KPJ"], sItem["KPJ"], true) && "符合" == IsQualified(mrsDj["KBZC"], sItem["KBZC"], true))
                    {
                        sItem["NJXSPD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["NJXSPD"] = "不合格";
                    }

                }
                else
                {
                    sItem["KPJ"] = "----";
                    sItem["NJXSYQ"] = "----";
                    sItem["KBZC"] = "----";

                    if (sItem["LSZL"].Contains("扭剪型"))
                    {
                        sItem["NJXSPD"] = sItem["KLHZPD"];
                    }
                    else
                    {
                        sItem["NJXSPD"] = "----";
                    }
                }
                #endregion

                #region 螺母洛氏硬度
                if (jcxm.Contains("、螺母洛氏硬度、"))
                {
                    jcxmCur = "螺母洛氏硬度";
                    nArr.Clear();
                    sItem["LMYDYQ"] = "洛氏硬度应≥" + mrsDj["LMLSYDMIN"].Trim() + "，≤" + mrsDj["LMLSYDMAX"].Trim() + "。";

                    for (int i = 1; i < 9; i++)
                    {
                        sItem["LMYD" + i] = Math.Round(myint((Conversion.Val(sItem["LMYD" + i + "_1"]) + Conversion.Val(sItem["LMYD" + i + "_2"]) + Conversion.Val(sItem["LMYD" + i + "_3"])) / 3 * 10) / 10, 1).ToString("0.0");

                        nArr.Add(Double.Parse(sItem["LMYD" + i]));

                    }

                    //calc_sort 从小到大排序
                    nArr.Sort();
                    if (nArr[0] >= Conversion.Val(mrsDj["LMLSYDMIN"]) || nArr[7] <= Conversion.Val(mrsDj["LMLSYDMAX"]))
                        sItem["LMYDPD"] = "合格";
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["LMYDPD"] = "不合格";
                    }
                }
                else
                {
                    sItem["LMYDPD"] = "----";
                }

                #endregion

                #region 垫圈洛氏硬度
                if (jcxm.Contains("、垫圈洛氏硬度、"))
                {
                    jcxmCur = "垫圈洛氏硬度";
                    nArr.Clear();
                    sItem["DQYDYQ"] = mrsDj["DQLSYD"].Trim();

                    for (int i = 1; i < 9; i++)
                    {
                        sItem["DQYD" + i] = Math.Round(myint((Conversion.Val(sItem["DQYD" + i + "_1"]) + Conversion.Val(sItem["DQYD" + i + "_2"]) + Conversion.Val(sItem["DQYD" + i + "_3"])) / 3 * 10) / 10, 1).ToString("0.0");
                        nArr.Add(Double.Parse(sItem["DQYD" + i]));
                    }
                    //calc_sort 从小到大排序
                    nArr.Sort();
                    if ("合格" == IsQualified(mrsDj["DQLSYD"], nArr[0].ToString()) && "合格" == IsQualified(mrsDj["DQLSYD"], nArr[7].ToString()))
                        sItem["DQYDPD"] = "合格";
                    else
                    {
                        sItem["DQYDPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["DQYDPD"] = "----";
                }
                #endregion

                #region 螺母保证载荷
                if (jcxm.Contains("、螺母保证载荷、"))
                {
                    jcxmCur = "螺母保证载荷";
                    sItem["LMBZHZYQ"] = "不应脱扣或断裂";
                    if ("符合" == sItem["LMBZHZ1"] && "符合" == sItem["LMBZHZ2"] && "符合" == sItem["LMBZHZ3"] && "符合" == sItem["LMBZHZ4"] && "符合" == sItem["LMBZHZ8"] && "符合" == sItem["LMBZHZ6"] && "符合" == sItem["LMBZHZ7"] && "符合" == sItem["LMBZHZ8"])
                        sItem["LMBZHZPD"] = "合格";
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["LMBZHZPD"] = "不合格";
                    }
                }
                else
                {
                    sItem["LMBZHZPD"] = "----";
                }
                #endregion

                #region 螺栓楔负载
                if (jcxm.Contains("、螺栓楔负载、"))
                {
                    jcxmCur = "螺栓楔负载";
                    sItem["LSQFZYQ"] = mrsDj["LSQFZ"].Trim();
                    if ("合格" == IsQualified(mrsDj["LSQFZ"], sItem["LSQFZ1"]) && "合格" == IsQualified(mrsDj["LSQFZ"], sItem["LSQFZ2"]))
                        sItem["LSQFZPD"] = "合格";
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["LSQFZPD"] = "不合格";
                    }
                }
                else
                {
                    sItem["LSQFZPD"] = "----";
                }
                #endregion

                if (sItem["LSQFZPD"] == "不合格" || sItem["LMBZHZPD"] == "不合格" || sItem["DQYDPD"] == "不合格" || sItem["LMYDPD"] == "不合格" || sItem["NJXSPD"] == "不合格" || sItem["KLHZPD"] == "不合格")
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                }
                else
                {
                    sItem["JCJG"] = "合格";
                }
            }

            #region 添加最终报告

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
            /************************ 代码结束 *********************/
        }
    }
}
