using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class NH : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_NH_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_NH"];

            if (!data.ContainsKey("M_NH"))
            {
                data["M_NH"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_NH"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";
            bool flag = true;
            List<double> nArr = new List<double>();
            double xd, Gs = 0;
            var mbhggs = 0;
            int mbHggs1, mbHggs2, mbHggs = 0;
            bool sign = true;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";


                if (extraDJ.Count < 1)
                {
                    jsbeizhu = "依据不详";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                var mrsDj = extraDJ.FirstOrDefault(u => u["JCYJ"] == sItem["JCYJ"]);

                sItem["G_SMC1"] = mrsDj["G_SMC1"];
                sItem["G_MMC1"] = mrsDj["G_MMC1"];
                sItem["G_BWC1"] = mrsDj["G_BWC1"];
                sItem["G_CK1"] = mrsDj["G_CK2"];
                sItem["G_SMC2"] = mrsDj["G_SMC2"];
                sItem["G_MMC2"] = mrsDj["G_MMC2"];
                sItem["G_BWC2"] = mrsDj["G_BWC2"];
                sItem["G_CK2"] = mrsDj["G_CK2"];
                sItem["G_PJQD1"] = mrsDj["G_NJQDPJ1"];
                sItem["G_PJQD2"] = mrsDj["G_NJQDPJ2"];
                sItem["G_QDMIN1"] = mrsDj["G_NJQDMIN1"];
                sItem["G_QDMIN2"] = mrsDj["G_NJQDMIN2"];
                MItem[0]["G_NJQD"] = mrsDj["G_NJQDPJ1"];
                MItem[0]["G_NJQD2"] = mrsDj["G_NJQDPJ2"];

                //mrsmainTable!which = "bgnh_99、bgnh、bgnh_1、bgnh_2、bgnh_3"

                mbHggs = 0;
                mbHggs1 = 0;
                mbHggs2 = 0;
                sign = true;

                if (jcxm.Contains("、高温淋水循环、") && jcxm.Contains("、加热冷冻循环、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "高温淋水循环,加热冷冻循环");
                    if (sItem["GH_SMC1"] == "合格")
                        sItem["W_SMC1"] = "无";
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "(饰面层)、";
                        mbHggs1 = mbHggs1 + 1;
                        sItem["W_SMC1"] = "有裂缝、起泡、剥落";
                    }
                    if (sItem["GH_SMC2"] == "合格")
                        sItem["W_SMC2"] = "无";
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "(饰面层)、";
                        mbHggs2 = mbHggs2 + 1;
                        sItem["W_SMC2"] = "有裂缝、起泡、剥落";
                    }
                    if (sItem["GH_MMC1"] == "合格")
                        sItem["W_MMC1"] = "无";
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "(抹面层)、";
                        mbHggs1 = mbHggs1 + 1;
                        sItem["W_MMC1"] = "有裂缝、起泡、剥落";
                    }

                    if (sItem["GH_MMC2"] == "合格")
                        sItem["W_MMC2"] = "无";
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "(抹面层)、";
                        mbHggs2 = mbHggs2 + 1;
                        sItem["W_MMC2"] = "有裂缝、起泡、剥落";
                    }
                    if (sItem["GH_BWC1"] == "合格" && sItem["GH_CK1"] == "合格")
                    {
                        sItem["W_BWC1"] = "无";
                        sItem["W_CK1"] = "无";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "(保温层或窗口)、";
                        mbHggs1 = mbHggs1 + 1;
                        sItem["W_BWC1"] = "有裂缝、起泡、剥落";
                        sItem["W_CK1"] = "有裂缝、起泡、剥落";
                    }
                    if (sItem["GH_BWC2"] == "合格" && sItem["GH_CK2"] == "合格")
                    {
                        sItem["W_BWC2"] = "无";
                        sItem["W_CK2"] = "无";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "(保温层或窗口)、";
                        mbHggs2 = mbHggs2 + 1;
                        sItem["W_BWC2"] = "有裂缝、起泡、剥落";
                        sItem["W_CK2"] = "有裂缝、起泡、剥落";
                    }
                    if (mbHggs1 == 0)
                    {
                        sItem["SYJG1"] = "饰面层及保护层未出现空鼓、裂缝、起泡、脱落等异常现象。";
                    }
                    else
                    {
                        sItem["SYJG1"] = "饰面层及保护层出现空鼓、裂缝、起泡、脱落等异常现象。";
                    }

                    if (mbHggs2 == 0)
                        sItem["SYJG3"] = "饰面层及保护层未出现空鼓、裂缝、起泡、脱落等异常现象。";
                    else
                    {
                        sItem["SYJG3"] = "饰面层及保护层出现空鼓、裂缝、起泡、脱落等异常现象。";
                    }
                }
                sign = true;

                if (jcxm.Contains("、拉伸粘结强度、"))
                {
                    sign = IsNumeric(sItem["W_PJQD1"]) ? sign : false;
                    sign = IsNumeric(sItem["W_PJQD2"]) ? sign : false;
                    sign = IsNumeric(sItem["W_MINQD1"]) ? sign : false;
                    sign = IsNumeric(sItem["W_MINQD2"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_PJQD1"] = IsQualified(sItem["G_PJQD1"], sItem["W_PJQD1"], false);
                        sItem["GH_PJQD2"] = IsQualified(sItem["G_PJQD2"], sItem["W_PJQD2"], false);
                        sItem["GH_QDMIN1"] = IsQualified(sItem["G_QDMIN1"], sItem["W_MINQD1"], false);
                        sItem["GH_QDMIN2"] = IsQualified(sItem["G_QDMIN2"], sItem["W_MINQD2"], false);
                    }
                }
                if (jcxm.Contains("、EPS聚苯板、") || jcxm.Contains("、玻纤网、") || jcxm.Contains("、胶粘剂、") || jcxm.Contains("、抹面胶浆、"))
                {
                    //MItem[0]["WHICH"] = "bgnh_99、bgnh、bgnh_1、bgnh_2、bgnh_3、bgnh_4";
                }

                #region 外保温拉拔
                if (jcxm.Contains("、外保温拉拔、"))
                {
                    jcxmCur = "外保温拉拔";
                    double mj1 = 0, mj2 = 0, mj3 = 0, mj4 = 0, mj5 = 0, mj6 = 0;
                    mj1 = GetSafeDouble(sItem["CD1"]) * GetSafeDouble(sItem["KD1"]);
                    mj2 = GetSafeDouble(sItem["CD2"]) * GetSafeDouble(sItem["KD2"]);
                    mj3 = GetSafeDouble(sItem["CD3"]) * GetSafeDouble(sItem["KD3"]);
                    mj4 = GetSafeDouble(sItem["CD4"]) * GetSafeDouble(sItem["KD4"]);
                    mj5 = GetSafeDouble(sItem["CD5"]) * GetSafeDouble(sItem["KD5"]);
                    mj6 = GetSafeDouble(sItem["CD6"]) * GetSafeDouble(sItem["KD6"]);
                    #region JGJ144
                    int zxz = 0;
                    if (mj1 != 0 && mj2 != 0 && mj3 != 0 && mj4 != 0 && mj5 != 0)
                    {
                        sItem["NJQD1"] = (GetSafeDouble(sItem["HZ1"]) / mj1).ToString("0.00");
                        sItem["NJQD2"] = (GetSafeDouble(sItem["HZ2"]) / mj2).ToString("0.00");
                        sItem["NJQD3"] = (GetSafeDouble(sItem["HZ3"]) / mj3).ToString("0.00");
                        sItem["NJQD4"] = (GetSafeDouble(sItem["HZ4"]) / mj4).ToString("0.00");
                        sItem["NJQD5"] = (GetSafeDouble(sItem["HZ5"]) / mj5).ToString("0.00");
                        sItem["NJQD"] = ((GetSafeDouble(sItem["NJQD1"]) + GetSafeDouble(sItem["NJQD2"]) + GetSafeDouble(sItem["NJQD3"]) + GetSafeDouble(sItem["NJQD4"]) + GetSafeDouble(sItem["NJQD5"])) / 5).ToString("0.00");
                        for (int i = 1; i < 6; i++)
                        {
                            if (GetSafeDouble(sItem["NJQD" + i]) >= double.Parse(MItem[0]["G_NJQD"].Replace("≥","")) * 0.75 && GetSafeDouble(sItem["NJQD" + i]) < double.Parse(MItem[0]["G_NJQD"].Replace("≥", "")))
                            {
                                zxz++;
                            }
                        }
                    }
                    else
                    {
                        sItem["NJQD"] = "0";
                        sItem["NJQD1"] = "0";
                        sItem["NJQD2"] = "0";
                        sItem["NJQD3"] = "0";
                        sItem["NJQD4"] = "0";
                        sItem["NJQD5"] = "0";
                    }
                    if (IsQualified(MItem[0]["G_NJQD"], sItem["NJQD"], false) == "合格" && zxz <= 1 && sItem["PHJMPD"] == "合格")
                    {
                        MItem[0]["HG_NJQD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_NJQD"] = "不合格";
                        mAllHg = false;
                    }

                    #endregion

                    #region GB/T29906 、JG158 
                    if (sItem["JCYJ"].Contains("29906") || sItem["JCYJ"].Contains("158"))
                    {
                        if (mj1 != 0 && mj2 != 0 && mj3 != 0 && mj4 != 0 && mj5 != 0 && mj6 != 0)
                        {
                            sItem["NJQD1"] = (GetSafeDouble(sItem["HZ1"]) / mj1).ToString("0.00");
                            sItem["NJQD2"] = (GetSafeDouble(sItem["HZ2"]) / mj2).ToString("0.00");
                            sItem["NJQD3"] = (GetSafeDouble(sItem["HZ3"]) / mj3).ToString("0.00");
                            sItem["NJQD4"] = (GetSafeDouble(sItem["HZ4"]) / mj4).ToString("0.00");
                            sItem["NJQD5"] = (GetSafeDouble(sItem["HZ5"]) / mj5).ToString("0.00");
                            sItem["NJQD6"] = (GetSafeDouble(sItem["HZ6"]) / mj5).ToString("0.00");

                            if (GetSafeDouble(sItem["NJQD1"]) != 0 && GetSafeDouble(sItem["NJQD2"]) != 0 && GetSafeDouble(sItem["NJQD3"]) != 0 && GetSafeDouble(sItem["NJQD4"]) != 0 && GetSafeDouble(sItem["NJQD5"]) != 0 && GetSafeDouble(sItem["NJQD6"]) != 0)
                            {
                                List<double> lArray = new List<double>();
                                for (int i = 1; i < 7; i++)
                                {
                                    lArray.Add(Conversion.Val(sItem["NJQD" + i]));
                                }
                                lArray.Sort();
                                lArray.Remove(lArray.Max());
                                lArray.Remove(lArray.Min());
                                sItem["NJQD"] = Round(lArray.Average(), 2).ToString();
                            }
                            if (IsQualified(MItem[0]["G_NJQD"], sItem["NJQD"], false) == "合格")
                            {
                                MItem[0]["HG_NJQD"] = "合格";
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_NJQD"] = "不合格";
                                mAllHg = false;
                            }

                        }
                    }
                    #endregion
                }
                else
                {
                    sItem["NJQD"] = "----";
                    MItem[0]["HG_NJQD"] = "----";
                    MItem[0]["G_NJQD"] = "----";
                }
                #endregion

                #region 饰面砖拉拔
                if (jcxm.Contains("、饰面砖拉拔、"))
                {
                    jcxmCur = "饰面砖拉拔";
                    double mj1 = 0, mj2 = 0, mj3 = 0;
                    mj1 = GetSafeDouble(sItem["CD21"]) * GetSafeDouble(sItem["KD21"]);
                    mj2 = GetSafeDouble(sItem["CD22"]) * GetSafeDouble(sItem["KD22"]);
                    mj3 = GetSafeDouble(sItem["CD23"]) * GetSafeDouble(sItem["KD23"]);

                    if (mj1 != 0 && mj2 != 0 && mj3 != 0)
                    {
                        sItem["NJQD21"] = (GetSafeDouble(sItem["HZ21"]) / mj1).ToString("0.00");
                        sItem["NJQD22"] = (GetSafeDouble(sItem["HZ22"]) / mj2).ToString("0.00");
                        sItem["NJQD23"] = (GetSafeDouble(sItem["HZ23"]) / mj3).ToString("0.00");
                        sItem["NJQD20"] = ((GetSafeDouble(sItem["NJQD21"]) + GetSafeDouble(sItem["NJQD22"]) + GetSafeDouble(sItem["NJQD23"])) / 3).ToString("0.00");

                    }
                    if (IsQualified(MItem[0]["G_NJQD2"], sItem["NJQD20"], false) == "合格" && sItem["PHJMPD2"] == "合格")
                    {
                        MItem[0]["HG_NJQD2"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_NJQD2"] = "不合格";
                        mAllHg = false;
                    }

                }
                else
                {
                    MItem[0]["HG_NJQD2"] = "----";
                    MItem[0]["G_NJQD2"] = "----";
                    sItem["NJQD20"] = "----";
                }
                #endregion

                mbHggs = mbHggs1 + mbHggs2;
                mbHggs = sItem["GH_PJQD1"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = sItem["GH_PJQD2"] == "不合格" ? mbHggs + 1 : mbHggs;


                if (mbHggs == 0)
                {
                    jsbeizhu = "该组样品所检项目符合上述标准要求";
                    sItem["JCJG"] = "合格";
                }

                if (mbhggs > 0)
                {
                    jsbeizhu = "该组所检项目样品不符合上述标准要求";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
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
