using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SS : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_SS"];
            var MItem = data["M_SS"];
            var mrsDj = dataExtra["BZ_SS_DJ"];
            if (!data.ContainsKey("M_SS"))
            {
                data["M_SS"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var mItem = MItem[0];
            bool sign, itemHg = true;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                string sfpd1, sfpd2, sfpd3, sfpd4, sfpd5, sfpd6, sfpd7, sfpd8, sfpd9, sfpd10, sfpd11, sfpd12;
                bool mark;

                #region 筛分析  颗粒级配
                if (jcxm.Contains("、筛分析、") || jcxm.Contains("、颗粒级配、"))
                {
                    jcxmCur = "筛分析";
                    sign = true;
                    for (int i = 1; i < 3; i++)
                    {
                        sign = IsNumeric(sItem["GZSYZL" + i].Trim());
                        sign = IsNumeric(sItem["SXHSSZL" + i].Trim());
                        sign = IsNumeric(sItem["SXHSXL" + i].Trim());
                    }
                    if (sign)
                    {
                        //0.075mm通过率
                        sItem["TGBFL00751"] = Round(GetSafeDouble(sItem["SXHSXL1"].Trim()) / GetSafeDouble(sItem["GZSYZL1"].Trim()) * 100, 1).ToString("0.0");
                        sItem["TGBFL00752"] = Round(GetSafeDouble(sItem["SXHSXL2"].Trim()) / GetSafeDouble(sItem["GZSYZL2"].Trim()) * 100, 1).ToString("0.0");
                        sItem["PJTGBFL00751"] = Round((GetSafeDouble(sItem["TGBFL00751"]) + GetSafeDouble(sItem["TGBFL00752"])) / 2, 1).ToString("0.0");
                        if (Math.Abs(GetSafeDouble(sItem["TGBFL00751"]) - GetSafeDouble(sItem["TGBFL00752"])) >1)
                        {
                            throw new SystemException("筛分析0.075mm通过率平行试验差值超过1%，试验应重新进行。");
                        }
                        #region 分计筛余 累计筛余
                        for (int i = 1; i < 3; i++)
                        {
                            #region 分计筛余
                            sItem["FJSY53_" + i] = Round(GetSafeDouble(sItem["SYL53_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY375_" + i] = Round(GetSafeDouble(sItem["SYL375_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY315_" + i] = Round(GetSafeDouble(sItem["SYL315_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY265_" + i] = Round(GetSafeDouble(sItem["SYL265_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY19_" + i] = Round(GetSafeDouble(sItem["SYL19_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY95_" + i] = Round(GetSafeDouble(sItem["SYL95_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY475_" + i] = Round(GetSafeDouble(sItem["SYL475_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY236_" + i] = Round(GetSafeDouble(sItem["SYL236_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY118_" + i] = Round(GetSafeDouble(sItem["SYL118_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY06_" + i] = Round(GetSafeDouble(sItem["SYL06_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY0075_" + i] = Round(GetSafeDouble(sItem["SYL0075_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            sItem["FJSY0002_" + i] = Round(GetSafeDouble(sItem["SYL0002_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                            #endregion

                            #region 累计筛余 
                            sItem["LJSY53_" + i] = sItem["FJSY53_" + i];
                            sItem["LJSY375_" + i] = Round(GetSafeDouble(sItem["LJSY53_" + i]) + GetSafeDouble(sItem["FJSY375_" + i]), 1).ToString("0.0");
                            sItem["LJSY315_" + i] = Round(GetSafeDouble(sItem["LJSY375_" + i]) + GetSafeDouble(sItem["FJSY315_" + i]), 1).ToString("0.0");
                            sItem["LJSY265_" + i] = Round(GetSafeDouble(sItem["LJSY315_" + i]) + GetSafeDouble(sItem["FJSY265_" + i]), 1).ToString("0.0");
                            sItem["LJSY19_" + i] = Round(GetSafeDouble(sItem["LJSY265_" + i]) + GetSafeDouble(sItem["FJSY19_" + i]), 1).ToString("0.0");
                            sItem["LJSY95_" + i] = Round(GetSafeDouble(sItem["LJSY19_" + i]) + GetSafeDouble(sItem["FJSY95_" + i]), 1).ToString("0.0");
                            sItem["LJSY475_" + i] = Round(GetSafeDouble(sItem["LJSY95_" + i]) + GetSafeDouble(sItem["FJSY475_" + i]), 1).ToString("0.0");
                            sItem["LJSY236_" + i] = Round(GetSafeDouble(sItem["LJSY475_" + i]) + GetSafeDouble(sItem["FJSY236_" + i]), 1).ToString("0.0");
                            sItem["LJSY118_" + i] = Round(GetSafeDouble(sItem["LJSY236_" + i]) + GetSafeDouble(sItem["FJSY118_" + i]), 1).ToString("0.0");
                            sItem["LJSY06_" + i] = Round(GetSafeDouble(sItem["LJSY118_" + i]) + GetSafeDouble(sItem["FJSY06_" + i]), 1).ToString("0.0");
                            sItem["LJSY0075_" + i] = Round(GetSafeDouble(sItem["LJSY06_" + i]) + GetSafeDouble(sItem["FJSY0075_" + i]), 1).ToString("0.0");
                            sItem["LJSY0002_" + i] = Round(GetSafeDouble(sItem["LJSY0075_" + i]) + GetSafeDouble(sItem["FJSY0002_" + i]), 1).ToString("0.0");
                            #endregion

                            #region 通过百分率
                            sItem["TGBF53_" + i] = Round(100 - GetSafeDouble(sItem["LJSY53_" + i]), 1).ToString("0.0");
                            sItem["TGBF375_" + i] = Round(100 - GetSafeDouble(sItem["LJSY375_" + i]), 1).ToString("0.0");
                            sItem["TGBF315_" + i] = Round(100 - GetSafeDouble(sItem["LJSY315_" + i]), 1).ToString("0.0");
                            sItem["TGBF265_" + i] = Round(100 - GetSafeDouble(sItem["LJSY265_" + i]), 1).ToString("0.0");
                            sItem["TGBF19_" + i] = Round(100 - GetSafeDouble(sItem["LJSY19_" + i]), 1).ToString("0.0");
                            sItem["TGBF95_" + i] = Round(100 - GetSafeDouble(sItem["LJSY95_" + i]), 1).ToString("0.0");
                            sItem["TGBF475_" + i] = Round(100 - GetSafeDouble(sItem["LJSY475_" + i]), 1).ToString("0.0");
                            sItem["TGBF236_" + i] = Round(100 - GetSafeDouble(sItem["LJSY236_" + i]), 1).ToString("0.0");
                            sItem["TGBF118_" + i] = Round(100 - GetSafeDouble(sItem["LJSY118_" + i]), 1).ToString("0.0");
                            sItem["TGBF06_" + i] = Round(100 - GetSafeDouble(sItem["LJSY06_" + i]), 1).ToString("0.0");
                            sItem["TGBF0075_" + i] = Round(100 - GetSafeDouble(sItem["LJSY0075_" + i]), 1).ToString("0.0");
                            sItem["TGBF0002_" + i] = Round(100 - GetSafeDouble(sItem["LJSY0002_" + i]), 1).ToString("0.0");
                            #endregion
                        }
                        #endregion

                        #region 平均分计筛余
                        sItem["PJFJSY53"] = Round((GetSafeDouble(sItem["FJSY53_1"]) + GetSafeDouble(sItem["FJSY53_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY375"] = Round((GetSafeDouble(sItem["FJSY375_1"]) + GetSafeDouble(sItem["FJSY375_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY315"] = Round((GetSafeDouble(sItem["FJSY315_1"]) + GetSafeDouble(sItem["FJSY315_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY265"] = Round((GetSafeDouble(sItem["FJSY265_1"]) + GetSafeDouble(sItem["FJSY265_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY19"] = Round((GetSafeDouble(sItem["FJSY19_1"]) + GetSafeDouble(sItem["FJSY19_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY95"] = Round((GetSafeDouble(sItem["FJSY95_1"]) + GetSafeDouble(sItem["FJSY95_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY475"] = Round((GetSafeDouble(sItem["FJSY475_1"]) + GetSafeDouble(sItem["FJSY475_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY236"] = Round((GetSafeDouble(sItem["FJSY236_1"]) + GetSafeDouble(sItem["FJSY236_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY118"] = Round((GetSafeDouble(sItem["FJSY118_1"]) + GetSafeDouble(sItem["FJSY118_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY06"] = Round((GetSafeDouble(sItem["FJSY06_1"]) + GetSafeDouble(sItem["FJSY06_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY0075"] = Round((GetSafeDouble(sItem["FJSY0075_1"]) + GetSafeDouble(sItem["FJSY0075_2"])) / 2, 1).ToString("0.0");
                        sItem["PJFJSY0002"] = Round((GetSafeDouble(sItem["FJSY0002_1"]) + GetSafeDouble(sItem["FJSY0002_2"])) / 2, 1).ToString("0.0");
                        #endregion

                        #region 平均累计筛余
                        sItem["PJLJSY53"] = Round((GetSafeDouble(sItem["LJSY53_1"]) + GetSafeDouble(sItem["LJSY53_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY375"] = Round((GetSafeDouble(sItem["LJSY375_1"]) + GetSafeDouble(sItem["LJSY375_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY315"] = Round((GetSafeDouble(sItem["LJSY315_1"]) + GetSafeDouble(sItem["LJSY315_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY265"] = Round((GetSafeDouble(sItem["LJSY265_1"]) + GetSafeDouble(sItem["LJSY265_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY19"] = Round((GetSafeDouble(sItem["LJSY19_1"]) + GetSafeDouble(sItem["LJSY19_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY95"] = Round((GetSafeDouble(sItem["LJSY95_1"]) + GetSafeDouble(sItem["LJSY95_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY475"] = Round((GetSafeDouble(sItem["LJSY475_1"]) + GetSafeDouble(sItem["LJSY475_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY236"] = Round((GetSafeDouble(sItem["LJSY236_1"]) + GetSafeDouble(sItem["LJSY236_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY118"] = Round((GetSafeDouble(sItem["LJSY118_1"]) + GetSafeDouble(sItem["LJSY118_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY06"] = Round((GetSafeDouble(sItem["LJSY06_1"]) + GetSafeDouble(sItem["LJSY06_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY0075"] = Round((GetSafeDouble(sItem["LJSY0075_1"]) + GetSafeDouble(sItem["LJSY0075_2"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSY0002"] = Round((GetSafeDouble(sItem["LJSY0002_1"]) + GetSafeDouble(sItem["LJSY0002_2"])) / 2, 1).ToString("0.0");
                        #endregion

                        #region 平均通过百分率
                        sItem["PJTGBF53"] = Round((GetSafeDouble(sItem["TGBF53_1"]) + GetSafeDouble(sItem["TGBF53_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF375"] = Round((GetSafeDouble(sItem["TGBF375_1"]) + GetSafeDouble(sItem["TGBF375_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF315"] = Round((GetSafeDouble(sItem["TGBF315_1"]) + GetSafeDouble(sItem["TGBF315_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF265"] = Round((GetSafeDouble(sItem["TGBF265_1"]) + GetSafeDouble(sItem["TGBF265_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF19"] = Round((GetSafeDouble(sItem["TGBF19_1"]) + GetSafeDouble(sItem["TGBF19_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF95"] = Round((GetSafeDouble(sItem["TGBF95_1"]) + GetSafeDouble(sItem["TGBF95_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF475"] = Round((GetSafeDouble(sItem["TGBF475_1"]) + GetSafeDouble(sItem["TGBF475_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF236"] = Round((GetSafeDouble(sItem["TGBF236_1"]) + GetSafeDouble(sItem["TGBF236_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF118"] = Round((GetSafeDouble(sItem["TGBF118_1"]) + GetSafeDouble(sItem["TGBF118_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF06"] = Round((GetSafeDouble(sItem["TGBF06_1"]) + GetSafeDouble(sItem["TGBF06_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF0075"] = Round((GetSafeDouble(sItem["TGBF0075_1"]) + GetSafeDouble(sItem["TGBF0075_2"])) / 2, 1).ToString("0.0");
                        sItem["PJTGBF0002"] = Round((GetSafeDouble(sItem["TGBF0002_1"]) + GetSafeDouble(sItem["TGBF0002_2"])) / 2, 1).ToString("0.0");
                        #endregion

                        #region 获取标准值
                        var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["JLZLJDLDJ"] == sItem["JCZX"]);
                        if (null == mrsDj_Filter)
                        {
                            sItem["JCJG"] = "不下结论";
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            sItem["BZFW53"] = mrsDj_Filter["BZFW53"];
                            sItem["BZFW375"] = mrsDj_Filter["BZFW375"];
                            sItem["BZFW315"] = mrsDj_Filter["BZFW315"];
                            sItem["BZFW265"] = mrsDj_Filter["BZFW265"];
                            sItem["BZFW19"] = mrsDj_Filter["BZFW19"];
                            sItem["BZFW95"] = mrsDj_Filter["BZFW95"];
                            sItem["BZFW475"] = mrsDj_Filter["BZFW475"];
                            sItem["BZFW236"] = mrsDj_Filter["BZFW236"];
                            sItem["BZFW118"] = mrsDj_Filter["BZFW118"];
                            sItem["BZFW06"] = mrsDj_Filter["BZFW06"];
                            sItem["BZFW0075"] = mrsDj_Filter["BZFW0075"];
                            sItem["BZFW0002"] = mrsDj_Filter["BZFW0002"];
                        }
                        #endregion

                        #region 合格判定
                        if (100 == GetSafeDouble(sItem["BZFW53"]))
                        {
                            sfpd1 = GetSafeDouble(sItem["PJTGBF53"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd1 = IsQualified(sItem["BZFW53"], sItem["PJTGBF53"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW375"]))
                        {
                            sfpd2 = GetSafeDouble(sItem["PJTGBF375"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd2 = IsQualified(sItem["BZFW375"], sItem["PJTGBF375"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW315"]))
                        {
                            sfpd3 = GetSafeDouble(sItem["PJTGBF315"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd3 = IsQualified(sItem["BZFW315"], sItem["PJTGBF315"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW265"]))
                        {
                            sfpd4 = GetSafeDouble(sItem["PJTGBF265"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd4 = IsQualified(sItem["BZFW265"], sItem["PJTGBF265"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW19"]))
                        {
                            sfpd5 = GetSafeDouble(sItem["PJTGBF19"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd5 = IsQualified(sItem["BZFW19"], sItem["PJTGBF19"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW95"]))
                        {
                            sfpd6 = GetSafeDouble(sItem["PJTGBF95"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd6 = IsQualified(sItem["BZFW95"], sItem["PJTGBF95"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW475"]))
                        {
                            sfpd7 = GetSafeDouble(sItem["PJTGBF475"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd7 = IsQualified(sItem["BZFW475"], sItem["PJTGBF475"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW236"]))
                        {
                            sfpd8 = GetSafeDouble(sItem["PJTGBF236"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd8 = IsQualified(sItem["BZFW236"], sItem["PJTGBF236"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW118"]))
                        {
                            sfpd9 = GetSafeDouble(sItem["PJTGBF118"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd9 = IsQualified(sItem["BZFW118"], sItem["PJTGBF118"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW06"]))
                        {
                            sfpd10 = GetSafeDouble(sItem["PJTGBF06"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd10 = IsQualified(sItem["BZFW06"], sItem["PJTGBF06"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW0075"]))
                        {
                            sfpd11 = GetSafeDouble(sItem["PJTGBF0075"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd11 = IsQualified(sItem["BZFW0075"], sItem["PJTGBF0075"], false);
                        }


                        if (100 == GetSafeDouble(sItem["BZFW0002"]))
                        {
                            sfpd12 = GetSafeDouble(sItem["PJTGBF0002"]) == 100 ? "合格" : "不合格";
                        }
                        else
                        {
                            sfpd12 = IsQualified(sItem["BZFW0002"], sItem["PJTGBF0002"], false);
                        }
                        #endregion

                        #region 综合判定
                        if (sfpd1 == "不合格" || sfpd2 == "不合格" || sfpd3 == "不合格" || sfpd4 == "不合格" || sfpd5 == "不合格" || sfpd6 == "不合格" || sfpd7 == "不合格"
                            || sfpd8 == "不合格" || sfpd9 == "不合格" || sfpd10 == "不合格" || sfpd11 == "不合格" || sfpd12 == "不合格")
                        {
                            itemHg = false;
                            sItem["SFPD"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        else
                        {
                            sItem["SFPD"] = "合格";
                        }
                        #endregion
                    }
                    else
                    {
                        throw new SystemException("筛分(水洗法)数据录入有误");
                    }
                }
                else
                {
                    sItem["SFPD"] = "----";
                    sItem["PJTGBF53"] = "----";
                    sItem["PJTGBF375"] = "----";
                    sItem["PJTGBF315"] = "----";
                    sItem["PJTGBF265"] = "----";
                    sItem["PJTGBF19"] = "----";
                    sItem["PJTGBF95"] = "----";
                    sItem["PJTGBF475"] = "----";
                    sItem["PJTGBF236"] = "----";
                    sItem["PJTGBF118"] = "----";
                    sItem["PJTGBF06"] = "----";
                    sItem["PJTGBF0075"] = "----";
                    sItem["PJTGBF0002"] = "----";
                }
                #endregion

                #region 压碎值
                if (jcxm.Contains("、压碎值、"))
                {
                    jcxmCur = "压碎值";
                    sign = true;
                    for (int i = 1; i < 4; i++)
                    {
                        sign = IsNumeric(sItem["SYZL" + i].Trim());
                        sign = IsNumeric(sItem["XLZL" + i].Trim());
                    }

                    if (sign)
                    {
                        var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["DLDJ"] == sItem["DLDJ"] && x["JGCC"] == sItem["JGCC"]);
                        if (null == mrsDj_Filter)
                        {
                            sItem["JCJG"] = "不下结论";
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            mItem["G_YSZ"] = mrsDj_Filter["G_YSZ"];
                        }
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["YSZB" + i] = Round(GetSafeDouble(sItem["XLZL" + i].Trim()) / GetSafeDouble(sItem["SYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        }
                        sItem["YSZBPJ"] = Round((GetSafeDouble(sItem["YSZB1"]) + GetSafeDouble(sItem["YSZB2"]) + GetSafeDouble(sItem["YSZB3"])) / 3, 1).ToString("0.0");

                        sItem["YSZPD"] = IsQualified(mItem["G_YSZ"], sItem["YSZBPJ"], false);

                        if ("不合格" == sItem["YSZPD"])
                        {
                            mAllHg = false;
                            itemHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        throw new SystemException("压碎指标值数据录入有误");
                    }
                }
                else
                {
                    mItem["W_YSZ"] = "----";
                    mItem["GH_YSZ"] = "----";
                    mItem["G_YSZ"] = "----";
                }
                #endregion

                #region 液限
                if (jcxm.Contains("、液限、"))
                {
                    mark = IsNumeric(sItem["YX"]);
                    if (mark)
                    {
                        mItem["W_YX"] = sItem["YX"].Trim();
                        mItem["GH_YX"] = IsQualified(mItem["G_YX"], mItem["W_YX"], true);
                    }
                    else
                    {
                        mItem["W_YX"] = "----";
                        mItem["GH_YX"] = "----";
                    }
                }
                else
                {
                    mItem["W_YX"] = "----";
                    mItem["GH_YX"] = "----";
                    mItem["G_YX"] = "----";
                }
                #endregion

                #region 塑性指数
                if (jcxm.Contains("、塑性指数、"))
                {
                    mark = IsNumeric(sItem["SXZS"]);
                    if (mark)
                    {
                        mItem["W_SXZS"] = sItem["SXZS"].Trim();
                        mItem["GH_SXZS"] = IsQualified(mItem["G_SXZS"], mItem["W_SXZS"], true);
                    }
                    else
                    {
                        mItem["W_SXZS"] = "----";
                        mItem["GH_SXZS"] = "----";
                    }
                }
                else
                {
                    mItem["W_SXZS"] = "----";
                    mItem["GH_SXZS"] = "----";
                    mItem["G_SXZS"] = "----";
                }
                #endregion

                #region 有机质含量
                if (jcxm.Contains("、有机质含量、"))
                {
                    mark = IsNumeric(sItem["YJZHL"]);
                    if (mark)
                    {
                        mItem["W_YJZHL"] = sItem["YJZHL"].Trim();
                        mItem["GH_YJZHL"] = IsQualified(mItem["G_YJZHL"], mItem["W_YJZHL"], true);
                    }
                    else
                    {
                        mItem["W_YJZHL"] = "----";
                        mItem["GH_YJZHL"] = "----";
                    }
                }
                else
                {
                    mItem["W_YJZHL"] = "----";
                    mItem["GH_YJZHL"] = "----";
                    mItem["G_YJZHL"] = "----";
                }
                #endregion

                //单项判定
                if (itemHg)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }

            }

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
            /************************ 代码结束 *********************/
        }
    }
}
