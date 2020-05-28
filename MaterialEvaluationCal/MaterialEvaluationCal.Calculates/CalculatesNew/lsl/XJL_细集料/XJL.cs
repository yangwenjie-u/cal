using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class XJL : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            bool mAllHg = true, mItemHg = true, mFlag_Bhg = true;
            int mbhggs = 0;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            string mJSFF;
            var SItem = data["S_XJL"];
            //var EItem = data["E_SF"];
            if (!data.ContainsKey("M_XJL"))
            {
                data["M_XJL"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_XJL"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var mrsDj = dataExtra["BZ_XJL_DJ"];
            var mItem = MItem[0];
            string tgbflsfHg1, tgbflsfHg2, tgbflsfHg3, tgbflsfHg4, tgbflsfHg5, tgbflsfHg6, tgbflsfHg7, tgbflsfHg8;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                bool jcjgHg = true;
                bool sign;
                double md1, md2, md, sum;
                string sql;
                var jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                #region 筛分
                if (jcxm.Contains("、筛分、"))
                {
                    jcxmCur = "筛分";
                    #region 获取筛分标准值
                    if ("人工砂" == sItem["CPMC"] || "石屑" == sItem["CPMC"])
                    {
                        var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["GCLJ"] == sItem["GGXH"] && x["CPMC"] == sItem["CPMC"]);
                        if (null == mrsDj_Filter)
                        {
                            sItem["JCJG"] = "不下结论";
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            sItem["TGBFLBZ1"] = mrsDj_Filter["SYGG_95"];
                            sItem["TGBFLBZ2"] = mrsDj_Filter["SYGG_475"];
                            sItem["TGBFLBZ3"] = mrsDj_Filter["SYGG_236"];
                            sItem["TGBFLBZ4"] = mrsDj_Filter["SYGG_118"];
                            sItem["TGBFLBZ5"] = mrsDj_Filter["SYGG_06"];
                            sItem["TGBFLBZ6"] = mrsDj_Filter["SYGG_03"];
                            sItem["TGBFLBZ7"] = mrsDj_Filter["SYGG_015"];
                            sItem["TGBFLBZ8"] = mrsDj_Filter["SYGG_0075"];
                        }
                    }
                    else
                    {
                        var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["CPMC"] == sItem["CPMC"]);
                        if (null == mrsDj_Filter)
                        {
                            sItem["JCJG"] = "不下结论";
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            sItem["TGBFLBZ1"] = mrsDj_Filter["SYGG_95"];
                            sItem["TGBFLBZ2"] = mrsDj_Filter["SYGG_475"];
                            sItem["TGBFLBZ3"] = mrsDj_Filter["SYGG_236"];
                            sItem["TGBFLBZ4"] = mrsDj_Filter["SYGG_118"];
                            sItem["TGBFLBZ5"] = mrsDj_Filter["SYGG_06"];
                            sItem["TGBFLBZ6"] = mrsDj_Filter["SYGG_03"];
                            sItem["TGBFLBZ7"] = mrsDj_Filter["SYGG_015"];
                            sItem["TGBFLBZ8"] = mrsDj_Filter["SYGG_0075"];
                        }
                    }
                    #endregion
                    sign = true;
                    for (int i = 1; i < 9; i++)
                    {
                        sign = IsNumeric(sItem["SY1_" + i].Trim());
                        sign = IsNumeric(sItem["SY2_" + i].Trim());
                    }
                    sign = IsNumeric(sItem["HGSYZL1"].Trim());
                    sign = IsNumeric(sItem["HGSYZL2"].Trim());
                    sign = IsNumeric(sItem["SYZL0075_1"].Trim());
                    sign = IsNumeric(sItem["SYZL0075_2"].Trim());
                    if (sign)
                    {
                        //通过0.075mm筛质量
                        sItem["TGZL0075_1"] = Round(GetSafeDouble(sItem["HGSYZL1"].Trim()) - GetSafeDouble(sItem["SYZL0075_1"].Trim()), 1).ToString("0.0");
                        sItem["TGZL0075_2"] = Round(GetSafeDouble(sItem["HGSYZL2"].Trim()) - GetSafeDouble(sItem["SYZL0075_2"].Trim()), 1).ToString("0.0");
                        //通过0.075mm筛含量%
                        sItem["TGZLBFB0075_1"] = Round(GetSafeDouble(sItem["TGZL0075_1"]) / GetSafeDouble(sItem["HGSYZL1"].Trim()) * 100, 1).ToString("0.0");
                        sItem["TGZLBFB0075_2"] = Round(GetSafeDouble(sItem["TGZL0075_2"]) / GetSafeDouble(sItem["HGSYZL2"].Trim()) * 100, 1).ToString("0.0");
                        sItem["TGZLBFBPJ0075"] = Round((GetSafeDouble(sItem["TGZLBFB0075_1"]) + GetSafeDouble(sItem["TGZLBFB0075_2"])) / 2, 1).ToString("0.0");
                        double msyzl1 = GetSafeDouble(sItem["HGSYZL1"].Trim());
                        double msyzl2 = GetSafeDouble(sItem["HGSYZL2"].Trim());
                        if (string.IsNullOrEmpty(msyzl1.ToString()) || msyzl1 == 0)
                        {
                            msyzl1 = 500;
                        }
                        if (string.IsNullOrEmpty(msyzl2.ToString()) || msyzl2 == 0)
                        {
                            msyzl2 = 500;
                        }
                        //分计筛余1
                        sItem["FJSY1_1"] = Round(GetSafeDouble(sItem["SY1_1"].Trim()) / msyzl1 * 100, 1).ToString("0.0");
                        sItem["FJSY1_2"] = Round(GetSafeDouble(sItem["SY1_2"].Trim()) / msyzl1 * 100, 1).ToString("0.0");
                        sItem["FJSY1_3"] = Round(GetSafeDouble(sItem["SY1_3"].Trim()) / msyzl1 * 100, 1).ToString("0.0");
                        sItem["FJSY1_4"] = Round(GetSafeDouble(sItem["SY1_4"].Trim()) / msyzl1 * 100, 1).ToString("0.0");
                        sItem["FJSY1_5"] = Round(GetSafeDouble(sItem["SY1_5"].Trim()) / msyzl1 * 100, 1).ToString("0.0");
                        sItem["FJSY1_6"] = Round(GetSafeDouble(sItem["SY1_6"].Trim()) / msyzl1 * 100, 1).ToString("0.0");
                        sItem["FJSY1_7"] = Round(GetSafeDouble(sItem["SY1_7"].Trim()) / msyzl1 * 100, 1).ToString("0.0");
                        sItem["FJSY1_8"] = Round(GetSafeDouble(sItem["SY1_8"].Trim()) / msyzl1 * 100, 1).ToString("0.0");
                        //分计筛余2
                        sItem["FJSY2_1"] = Round(GetSafeDouble(sItem["SY2_1"].Trim()) / msyzl2 * 100, 1).ToString("0.0");
                        sItem["FJSY2_2"] = Round(GetSafeDouble(sItem["SY2_2"].Trim()) / msyzl2 * 100, 1).ToString("0.0");
                        sItem["FJSY2_3"] = Round(GetSafeDouble(sItem["SY2_3"].Trim()) / msyzl2 * 100, 1).ToString("0.0");
                        sItem["FJSY2_4"] = Round(GetSafeDouble(sItem["SY2_4"].Trim()) / msyzl2 * 100, 1).ToString("0.0");
                        sItem["FJSY2_5"] = Round(GetSafeDouble(sItem["SY2_5"].Trim()) / msyzl2 * 100, 1).ToString("0.0");
                        sItem["FJSY2_6"] = Round(GetSafeDouble(sItem["SY2_6"].Trim()) / msyzl2 * 100, 1).ToString("0.0");
                        sItem["FJSY2_7"] = Round(GetSafeDouble(sItem["SY2_7"].Trim()) / msyzl2 * 100, 1).ToString("0.0");
                        sItem["FJSY2_8"] = Round(GetSafeDouble(sItem["SY2_8"].Trim()) / msyzl2 * 100, 1).ToString("0.0");
                        //累计筛余1
                        sItem["LJSY1_1"] = sItem["FJSY1_1"];

                        sItem["LJSY1_2"] = Round(GetSafeDouble(sItem["FJSY1_1"]) + GetSafeDouble(sItem["FJSY1_2"]), 1).ToString("0.0");

                        sItem["LJSY1_3"] = Round(GetSafeDouble(sItem["FJSY1_1"]) + GetSafeDouble(sItem["FJSY1_2"]) + GetSafeDouble(sItem["FJSY1_3"]), 1).ToString("0.0");

                        sItem["LJSY1_4"] = Round(GetSafeDouble(sItem["FJSY1_1"]) + GetSafeDouble(sItem["FJSY1_2"]) + GetSafeDouble(sItem["FJSY1_3"])
                            + GetSafeDouble(sItem["FJSY1_4"]), 1).ToString("0.0");

                        sItem["LJSY1_5"] = Round(GetSafeDouble(sItem["FJSY1_1"]) + GetSafeDouble(sItem["FJSY1_2"]) + GetSafeDouble(sItem["FJSY1_3"])
                            + GetSafeDouble(sItem["FJSY1_4"]) + GetSafeDouble(sItem["FJSY1_5"]), 1).ToString("0.0");

                        sItem["LJSY1_6"] = Round(GetSafeDouble(sItem["FJSY1_1"]) + GetSafeDouble(sItem["FJSY1_2"]) + GetSafeDouble(sItem["FJSY1_3"])
                            + GetSafeDouble(sItem["FJSY1_4"]) + GetSafeDouble(sItem["FJSY1_5"]) + GetSafeDouble(sItem["FJSY1_6"]), 1).ToString("0.0");

                        sItem["LJSY1_7"] = Round(GetSafeDouble(sItem["FJSY1_1"]) + GetSafeDouble(sItem["FJSY1_2"]) + GetSafeDouble(sItem["FJSY1_3"])
                            + GetSafeDouble(sItem["FJSY1_4"]) + GetSafeDouble(sItem["FJSY1_5"]) + GetSafeDouble(sItem["FJSY1_6"]) + GetSafeDouble(sItem["FJSY1_7"]), 1).ToString("0.0");

                        sItem["LJSY1_8"] = Round(GetSafeDouble(sItem["FJSY1_1"]) + GetSafeDouble(sItem["FJSY1_2"]) + GetSafeDouble(sItem["FJSY1_3"])
                            + GetSafeDouble(sItem["FJSY1_4"]) + GetSafeDouble(sItem["FJSY1_5"]) + GetSafeDouble(sItem["FJSY1_6"]) + GetSafeDouble(sItem["FJSY1_7"]) + GetSafeDouble(sItem["FJSY1_8"]), 1).ToString("0.0");
                        //累计筛余2
                        sItem["LJSY2_1"] = sItem["FJSY2_1"];

                        sItem["LJSY2_2"] = Round(GetSafeDouble(sItem["FJSY2_1"]) + GetSafeDouble(sItem["FJSY2_2"]), 1).ToString("0.0");

                        sItem["LJSY2_3"] = Round(GetSafeDouble(sItem["FJSY2_1"]) + GetSafeDouble(sItem["FJSY2_2"]) + GetSafeDouble(sItem["FJSY2_3"]), 1).ToString("0.0");

                        sItem["LJSY2_4"] = Round(GetSafeDouble(sItem["FJSY2_1"]) + GetSafeDouble(sItem["FJSY2_2"]) + GetSafeDouble(sItem["FJSY2_3"])
                            + GetSafeDouble(sItem["FJSY2_4"]), 1).ToString("0.0");

                        sItem["LJSY2_5"] = Round(GetSafeDouble(sItem["FJSY2_1"]) + GetSafeDouble(sItem["FJSY2_2"]) + GetSafeDouble(sItem["FJSY2_3"])
                            + GetSafeDouble(sItem["FJSY2_4"]) + GetSafeDouble(sItem["FJSY2_5"]), 1).ToString("0.0");

                        sItem["LJSY2_6"] = Round(GetSafeDouble(sItem["FJSY2_1"]) + GetSafeDouble(sItem["FJSY2_2"]) + GetSafeDouble(sItem["FJSY2_3"])
                            + GetSafeDouble(sItem["FJSY2_4"]) + GetSafeDouble(sItem["FJSY2_5"]) + GetSafeDouble(sItem["FJSY2_6"]), 1).ToString("0.0");

                        sItem["LJSY2_7"] = Round(GetSafeDouble(sItem["FJSY2_1"]) + GetSafeDouble(sItem["FJSY2_2"]) + GetSafeDouble(sItem["FJSY2_3"])
                            + GetSafeDouble(sItem["FJSY2_4"]) + GetSafeDouble(sItem["FJSY2_5"]) + GetSafeDouble(sItem["FJSY2_6"]) + GetSafeDouble(sItem["FJSY2_7"]), 1).ToString("0.0");

                        sItem["LJSY2_8"] = Round(GetSafeDouble(sItem["FJSY2_1"]) + GetSafeDouble(sItem["FJSY2_2"]) + GetSafeDouble(sItem["FJSY2_3"])
                            + GetSafeDouble(sItem["FJSY2_4"]) + GetSafeDouble(sItem["FJSY2_5"]) + GetSafeDouble(sItem["FJSY2_6"]) + GetSafeDouble(sItem["FJSY2_7"]) + GetSafeDouble(sItem["FJSY2_8"]), 1).ToString("0.0");
                        //通过百分率1
                        sItem["TGBFL1_1"] = Round(100 - GetSafeDouble(sItem["LJSY1_1"]), 1).ToString("0.0");
                        sItem["TGBFL1_2"] = Round(100 - GetSafeDouble(sItem["LJSY1_2"]), 1).ToString("0.0");
                        sItem["TGBFL1_3"] = Round(100 - GetSafeDouble(sItem["LJSY1_3"]), 1).ToString("0.0");
                        sItem["TGBFL1_4"] = Round(100 - GetSafeDouble(sItem["LJSY1_4"]), 1).ToString("0.0");
                        sItem["TGBFL1_5"] = Round(100 - GetSafeDouble(sItem["LJSY1_5"]), 1).ToString("0.0");
                        sItem["TGBFL1_6"] = Round(100 - GetSafeDouble(sItem["LJSY1_6"]), 1).ToString("0.0");
                        sItem["TGBFL1_7"] = Round(100 - GetSafeDouble(sItem["LJSY1_7"]), 1).ToString("0.0");
                        sItem["TGBFL1_8"] = Round(100 - GetSafeDouble(sItem["LJSY1_8"]), 1).ToString("0.0");
                        //通过百分率2       
                        sItem["TGBFL2_1"] = Round(100 - GetSafeDouble(sItem["LJSY2_1"]), 1).ToString("0.0");
                        sItem["TGBFL2_2"] = Round(100 - GetSafeDouble(sItem["LJSY2_2"]), 1).ToString("0.0");
                        sItem["TGBFL2_3"] = Round(100 - GetSafeDouble(sItem["LJSY2_3"]), 1).ToString("0.0");
                        sItem["TGBFL2_4"] = Round(100 - GetSafeDouble(sItem["LJSY2_4"]), 1).ToString("0.0");
                        sItem["TGBFL2_5"] = Round(100 - GetSafeDouble(sItem["LJSY2_5"]), 1).ToString("0.0");
                        sItem["TGBFL2_6"] = Round(100 - GetSafeDouble(sItem["LJSY2_6"]), 1).ToString("0.0");
                        sItem["TGBFL2_7"] = Round(100 - GetSafeDouble(sItem["LJSY2_7"]), 1).ToString("0.0");
                        sItem["TGBFL2_8"] = Round(100 - GetSafeDouble(sItem["LJSY2_8"]), 1).ToString("0.0");
                        //最终累计筛余、通过百分率
                        for (int i = 1; i < 9; i++)
                        {
                            sItem["LJSYPJ" + i] = Round((GetSafeDouble(sItem["LJSY1_" + i]) + GetSafeDouble(sItem["LJSY2_" + i])) / 2, 1).ToString("0.0");
                            sItem["TGBFLPJ" + i] = Round((GetSafeDouble(sItem["TGBFL1_" + i]) + GetSafeDouble(sItem["TGBFL2_" + i])) / 2, 1).ToString("0.0");
                        }
                        #region 判断筛余通过百分是否符合标准
                        if ("----" == sItem["TGBFLBZ1"])
                        {
                            tgbflsfHg1 = "----";
                        }
                        else
                        {
                            tgbflsfHg1 = GetSafeDouble(sItem["TGBFLBZ1"]) == GetSafeDouble(sItem["TGBFLPJ1"]) ? "合格" : "不合格";
                        }
                        if ("100" == sItem["TGBFLBZ2"])
                        {
                            tgbflsfHg2 = GetSafeDouble(sItem["TGBFLBZ2"]) == GetSafeDouble(sItem["TGBFLPJ2"]) ? "合格" : "不合格";
                        }
                        else
                        {
                            tgbflsfHg2 = IsQualified(sItem["TGBFLBZ2"], sItem["TGBFLPJ2"], false);
                        }
                        tgbflsfHg3 = IsQualified(sItem["TGBFLBZ3"], sItem["TGBFLPJ3"], false);
                        tgbflsfHg4 = IsQualified(sItem["TGBFLBZ4"], sItem["TGBFLPJ4"], false);
                        tgbflsfHg5 = IsQualified(sItem["TGBFLBZ5"], sItem["TGBFLPJ5"], false);
                        tgbflsfHg6 = IsQualified(sItem["TGBFLBZ6"], sItem["TGBFLPJ6"], false);
                        tgbflsfHg7 = IsQualified(sItem["TGBFLBZ7"], sItem["TGBFLPJ7"], false);
                        tgbflsfHg8 = IsQualified(sItem["TGBFLBZ8"], sItem["TGBFLPJ8"], false);
                        if ("不合格" == tgbflsfHg1 || "不合格" == tgbflsfHg2 || "不合格" == tgbflsfHg3 || "不合格" == tgbflsfHg4
                            || "不合格" == tgbflsfHg5 || "不合格" == tgbflsfHg6 || "不合格" == tgbflsfHg7 || "不合格" == tgbflsfHg8)
                        {
                            if ("人工砂" == sItem["CPMC"] || "石屑" == sItem["CPMC"])
                            {
                                sItem["SFXPD"] = "筛分结果不符合" + sItem["CPMC"] + sItem["GGXH"] + "技术要求";
                            }
                            else
                            {
                                sItem["SFXPD"] = "筛分结果不符合" + sItem["CPMC"] + "技术要求";
                            }
                            mAllHg = false;
                            jcjgHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        else
                        {
                            if ("人工砂" == sItem["CPMC"] || "石屑" == sItem["CPMC"])
                            {
                                sItem["SFXPD"] = "筛分结果符合" + sItem["CPMC"] + sItem["GGXH"] + "技术要求";
                            }
                            else
                            {
                                sItem["SFXPD"] = "筛分结果符合" + sItem["CPMC"] + "技术要求";
                            }

                        }
                        #endregion
                        //细度模数计算  累计筛余百分率  (（A0.15 + A0.3 + A0.6 + A0.18 +  A2.36）- 5 * A4.75) / (100 - A4.75)
                        sItem["XDMS1"] = Round((GetSafeDouble(sItem["LJSY1_3"]) + GetSafeDouble(sItem["LJSY1_4"]) + GetSafeDouble(sItem["LJSY1_5"]) + GetSafeDouble(sItem["LJSY1_6"]) + GetSafeDouble(sItem["LJSY1_7"])
                             - 5 * GetSafeDouble(sItem["LJSY1_2"])) / (100 - GetSafeDouble(sItem["LJSY1_2"])), 2).ToString("0.00");
                        sItem["XDMS2"] = Round((GetSafeDouble(sItem["LJSY2_3"]) + GetSafeDouble(sItem["LJSY2_4"]) + GetSafeDouble(sItem["LJSY2_5"]) + GetSafeDouble(sItem["LJSY2_6"]) + GetSafeDouble(sItem["LJSY2_7"])
                             - 5 * GetSafeDouble(sItem["LJSY2_2"])) / (100 - GetSafeDouble(sItem["LJSY2_2"])), 2).ToString("0.00");
                        sItem["W_XDMS"] = Round((GetSafeDouble(sItem["XDMS1"]) + GetSafeDouble(sItem["XDMS2"])) / 2, 2).ToString("0.00");

                        if (GetSafeDouble(sItem["W_XDMS"]) >= 3.1 && GetSafeDouble(sItem["W_XDMS"]) <= 3.7)
                            sItem["XDMS_GH"] = "粗砂";
                        else if (GetSafeDouble(sItem["W_XDMS"]) >= 2.3 && GetSafeDouble(sItem["W_XDMS"]) <= 3)
                            sItem["XDMS_GH"] = "中砂";
                        else if (GetSafeDouble(sItem["W_XDMS"]) >= 1.6 && GetSafeDouble(sItem["W_XDMS"]) <= 2.2)
                            sItem["XDMS_GH"] = "细砂";
                        else if (GetSafeDouble(sItem["W_XDMS"]) >= 0.7 && GetSafeDouble(sItem["W_XDMS"]) <= 1.5)
                            sItem["XDMS_GH"] = "特细砂";
                        else
                        {
                            sItem["XDMS_GH"] = "不符合";
                        }

                        if (Math.Abs(GetSafeDouble(sItem["XDMS1"]) - GetSafeDouble(sItem["XDMS2"])) > 0.2)
                        {
                            sItem["XDMS_GH"] = "细度模数两试验数据差值大于0.2试验需重做";
                            sItem["XDMS_GH"] = "重做试验";
                            sItem["W_XDMS"] = "重做试验";
                            //sItem["JPPD"] = sItem["JPPD"] + sItem["XDMS_GH"];
                        }
                    }
                    else
                    {
                        throw new SystemException("筛分析数据录入有误");
                    }

                }
                else
                {
                    sItem["XDMS_GH"] = "----";
                    sItem["W_XDMS"] = "----";
                    sItem["XDMS1"] = "----";
                    sItem["XDMS2"] = "----";
                }
                #endregion

                #region 含水率
                if (jcxm.Contains("、含水率、"))
                {
                    sItem["HSL_GH"] = IsQualified(sItem["G_HSL"], sItem["W_HSL"], true);
                    if (sItem["HSL_GH"] == "不符合")
                    {
                        mAllHg = false; jcjgHg = false;
                    }
                }
                else
                {
                    sItem["W_HSL"] = "----";
                    sItem["HSL_GH"] = "----";
                    sItem["G_HSL"] = "----";
                }
                #endregion

                #region 吸水率
                if (jcxm.Contains("、吸水率、"))
                {
                    jcxmCur = "吸水率";
                    sign = true;
                    sign = IsNumeric(sItem["RQZLXSL1"].Trim());
                    sign = IsNumeric(sItem["RQZLXSL2"].Trim());
                    sign = IsNumeric(sItem["HGQSYRQZL1"].Trim());
                    sign = IsNumeric(sItem["HGQSYRQZL2"].Trim());
                    sign = IsNumeric(sItem["HGSYRQZL1"].Trim());
                    sign = IsNumeric(sItem["HGSYRQZL2"].Trim());
                    if (sign)
                    {
                        //饱和面干试样质量g
                        double bhmgsyzl1 = GetSafeDouble(sItem["HGQSYRQZL1"].Trim()) - GetSafeDouble(sItem["RQZLXSL1"].Trim());
                        double bhmgsyzl2 = GetSafeDouble(sItem["HGQSYRQZL2"].Trim()) - GetSafeDouble(sItem["RQZLXSL2"].Trim());
                        //烘干试样质量
                        double hgsyzl1 = GetSafeDouble(sItem["HGSYRQZL1"].Trim()) - GetSafeDouble(sItem["RQZLXSL1"].Trim());
                        double hgsyzl2 = GetSafeDouble(sItem["HGSYRQZL2"].Trim()) - GetSafeDouble(sItem["RQZLXSL2"].Trim());
                        //吸水率%   = （（饱和面干试样质量g - 烘干试样质量g） / 烘干试样质量g  ） * 100    精确至0.01%
                        sItem["XSL1"] = Round(((bhmgsyzl1 - hgsyzl1) / hgsyzl1) * 100, 2).ToString("0.00");
                        sItem["XSL2"] = Round(((bhmgsyzl2 - hgsyzl2) / hgsyzl2) * 100, 2).ToString("0.00");
                        sItem["PJXSL"] = Round((GetSafeDouble(sItem["XSL1"]) + GetSafeDouble(sItem["XSL2"])) / 2, 2).ToString("0.00");
                        if (Math.Abs(GetSafeDouble(sItem["PJXSL"]) - GetSafeDouble(sItem["XSL1"])) > 0.02 || Math.Abs(GetSafeDouble(sItem["PJXSL"]) - GetSafeDouble(sItem["XSL2"])) > 0.02)
                        {
                            sItem["XSL_GH"] = "重做试验";
                        }
                        else
                        {
                            sItem["XSL_GH"] = IsQualified(sItem["G_XSL"], sItem["PJXSL"], true);
                            if (sItem["XSL_GH"] == "不符合")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mAllHg = false;
                                jcjgHg = false;
                            }
                        }
                    }
                    else
                    {
                        throw new SystemException("吸水率试验数据录入有误");
                    }

                }
                else
                {
                    sItem["W_XSL"] = "----";
                    sItem["XSL_GH"] = "----";
                    sItem["G_XSL"] = "----";
                    sItem["PJXSL"] = "----";
                }
                #endregion

                #region 表观密度
                if (jcxm.Contains("、表观密度、") || sItem["JCXM"].Contains("、密度、"))
                {
                    jcxmCur = "表观密度";
                    //不同温度水时的密度及水温修正系数
                    string sw = Conversion.Val(sItem["BGMDSW"]).ToString();
                    var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["SW"] == sw);
                    if (null == mrsDj_Filter)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    //根据道路等级 获取对应表观相对密度标准值
                    if ("其它等级道路" == sItem["DLDJ"])
                    {
                        sItem["G_BGMD"] = "≥2.45";
                    }
                    else if ("城市快速路、主干路" == sItem["DLDJ"])
                    {
                        sItem["G_BGMD"] = "≥2.50";
                    }
                    sign = true;
                    sign = IsNumeric(sItem["SYGZL1"].Trim());
                    sign = IsNumeric(sItem["SYGZL2"].Trim());
                    sign = IsNumeric(sItem["SJRQZL1"].Trim());
                    sign = IsNumeric(sItem["SJRQZL2"].Trim());
                    sign = IsNumeric(sItem["SYSJRQZL1"].Trim());
                    sign = IsNumeric(sItem["SYSJRQZL2"].Trim());
                    if (sign)
                    {
                        //表观相对密度 = 试样烘干质量(g) / (试样烘干质量(g) + 水及容量瓶质量(g) - 试样、水及容量瓶总质量(g))  保留3位小数   g/cm³
                        sItem["BGXDMD1"] = Round(GetSafeDouble(sItem["SYGZL1"]) / (GetSafeDouble(sItem["SYGZL1"]) + GetSafeDouble(sItem["SJRQZL1"]) - GetSafeDouble(sItem["SYSJRQZL1"])), 3).ToString("0.000");
                        sItem["BGXDMD2"] = Round(GetSafeDouble(sItem["SYGZL2"]) / (GetSafeDouble(sItem["SYGZL2"]) + GetSafeDouble(sItem["SJRQZL2"]) - GetSafeDouble(sItem["SYSJRQZL2"])), 3).ToString("0.000");
                        sItem["BGXDMDPJ"] = Round((GetSafeDouble(sItem["BGXDMD1"]) + GetSafeDouble(sItem["BGXDMD2"])) / 2, 3).ToString("0.000");
                        //表观密度  =  表观相对密度 * 试验温度时的水密度
                        sItem["BGMD1"] = Round(GetSafeDouble(sItem["BGXDMD1"]) * GetSafeDouble(mrsDj_Filter["SMD"]), 3).ToString("0.000");
                        sItem["BGMD2"] = Round(GetSafeDouble(sItem["BGXDMD2"]) * GetSafeDouble(mrsDj_Filter["SMD"]), 3).ToString("0.000");
                        sItem["BGMDPJ"] = Round((GetSafeDouble(sItem["BGMD1"]) + GetSafeDouble(sItem["BGMD2"])) / 2, 3).ToString("0.000");
                        //
                        if (Math.Abs(GetSafeDouble(sItem["BGMD1"]) - GetSafeDouble(sItem["BGMD2"])) > 0.01)
                        {
                            sItem["BGMD_GH"] = "重做试验";
                        }
                        else
                        {
                            sItem["BGMD_GH"] = IsQualified(sItem["G_BGMD"], sItem["BGXDMDPJ"], true);
                            if (sItem["BGMD_GH"] == "不符合")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mAllHg = false;
                                jcjgHg = false;
                            }
                        }
                    }
                    else
                    {
                        throw new SystemException("表观密度试验数据录入有误");
                    }
                }
                else
                {
                    sItem["W_BGMD"] = "----";
                    sItem["BGMD_GH"] = "----";
                    sItem["G_BGMD"] = "----";
                    sItem["BGXDMDPJ"] = "----";
                }
                #endregion

                #region 堆积密度
                if (jcxm.Contains("、堆积密度、"))
                {
                    sItem["DJMD_GH"] = IsQualified(sItem["G_DJMD"], sItem["W_DJMD"], true);
                    if (sItem["DJMD_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["DJMD_GH"] = "----";
                    sItem["W_DJMD"] = "----";
                    sItem["G_DJMD"] = "----";
                }
                #endregion

                #region 紧密密度
                if (jcxm.Contains("、紧密密度、"))
                {
                    sItem["JMMD_GH"] = IsQualified(sItem["G_JMMD"], sItem["W_JMMD"], true);
                    if (sItem["JMMD_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["W_JMMD"] = "----";
                    sItem["JMMD_GH"] = "----";
                    sItem["G_JMMD"] = "----";
                }
                #endregion

                #region 含泥量
                if (jcxm.Contains("、含泥量、"))
                {
                    sItem["HNL_GH"] = IsQualified(sItem["G_HNL"], sItem["W_HNL"], true);
                    if (sItem["HNL_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["HNL_GH"] = "----";
                    sItem["W_HNL"] = "----";
                    sItem["G_HNL"] = "----";
                }
                #endregion

                #region 泥块含量
                if (jcxm.Contains("、泥块含量、"))
                {
                    sItem["NKHL_GH"] = IsQualified(sItem["G_NKHL"], sItem["W_NKHL"], true);
                    if (sItem["NKHL_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["W_NKHL"] = "----";
                    sItem["NKHL_GH"] = "----";
                    sItem["G_NKHL"] = "----";
                }
                #endregion

                #region 砂当量
                if (jcxm.Contains("、砂当量、"))
                {
                    sItem["SDL_GH"] = IsQualified(sItem["G_SDL"], sItem["W_SDL"], true);
                    if (sItem["SDL_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["W_SDL"] = "----";
                    sItem["SDL_GH"] = "----";
                    sItem["G_SDL"] = "----";
                }
                #endregion

                #region 筛分
                //if (sItem["JCXM"].Contains("、筛分、"))
                //{
                //    foreach (var e in EItem)
                //    {
                //        e["SysjbRecid"] = sItem["recid"];
                //    }
                //    //sql = "update e_sf set bgbh='" + sItem["wtbh"] + "' where csylb='" + "XJL" + "' and dzbh='" + sItem["dzbh"] + "'";
                //}
                #endregion

                #region 细度模数 
                //if (sItem["JCXM"].Contains("细度模数"))
                //{
                //    //var E_SF = "select * from E_SF where csylb='" + "XJL" + "' and dzbh='" + sItem["dzbh"] + "'";//, adOpenStatic, adLockBatchOptimistic);
                //    sItem["W_XDMS"] = "----";
                //    sign = false;
                //    if (EItem.Count > 0)
                //    {
                //        var eItem = EItem.FirstOrDefault(); 
                //        sign = string.IsNullOrEmpty(eItem["sfzdy"]) || eItem["sfzdy"] == "否" ? true : false;
                //        sItem["W_XDMS"] = eItem["xdms"];
                //        //E_SF.Close
                //    }

                //    if (IsNumeric(sItem["W_XDMS"]) && !string.IsNullOrEmpty(sItem["W_XDMS"]) && sign)
                //    {
                //        md = GetSafeDouble(sItem["W_XDMS"]);
                //        if (md > 3.8)
                //        {
                //            sItem["XDMS_GH"] = "----";
                //            sItem["G_XDMS"] = "----";
                //        }
                //        else if (md >= 3.1)
                //        {
                //            sItem["XDMS_GH"] = "粗砂";
                //            sItem["G_XDMS"] = "3.1～3.7";
                //        }
                //        else if (md >= 2.3)
                //        {
                //            sItem["XDMS_GH"] = "中砂";
                //            sItem["G_XDMS"] = "2.3～3.0";
                //        }
                //        else if (md >= 1.6)
                //        {
                //            sItem["XDMS_GH"] = "细砂";
                //            sItem["G_XDMS"] = "1.6～2.2";
                //        }
                //        else
                //        {
                //            sItem["XDMS_GH"] = "----";
                //            sItem["G_XDMS"] = "----";
                //        }
                //    }
                //    else
                //    {
                //        sItem["XDMS_GH"] = "----";
                //        sItem["G_XDMS"] = "----";
                //    }
                //}
                //else
                //{
                //    sItem["W_XDMS"] = "----";
                //    sItem["XDMS_GH"] = "----";
                //    sItem["G_XDMS"] = "----";
                //}
                #endregion

                //jsbeizhu = "";
                //if (jcxm == "、筛分、")
                //{
                //    jsbeizhu = "该组试样的检测结果详见报告第1～2页。";
                //}
                //else
                //{
                //    if (jcxm.Contains("、筛分、"))
                //    {
                //        jsbeizhu = "该组试样的检测结果详见报告第1～2页。";
                //    }
                //    else
                //    {
                //        jsbeizhu = "该组试样的检测结果详见报告第1页。";
                //    }
                //}
                if (jcjgHg)
                {
                    sItem["JCJG"] = "合格";
                    MItem[0]["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    MItem[0]["JCJG"] = "不合格";
                }
            }
            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求	。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
