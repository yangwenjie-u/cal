using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class QK1 : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            bool mGetBgbh = false;
            int mbhggs = 0;//不合格数量
            string mCpmc = "";
            var extraDJ = dataExtra["BZ_QK1_DJ"];
            var data = retData;

            var SItem = data["S_QK1"];
            var MItem = data["M_QK1"];
            if (!data.ContainsKey("M_QK1"))
            {
                data["M_QK1"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var jcxmBhg = "";
            var jcxmCur = "";
            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                #region 外观质量 尺寸偏差
                if (jcxm.Contains("、外观质量、") || jcxm.Contains("、尺寸偏差、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "外观质量,尺寸偏差");
                    if (GetSafeDouble(sItem["WCBHGS"]) > 7)
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        jsbeizhu = "外观质量、尺寸偏差不合格数量超过7个";
                        MItem[0]["HG_WGZL"] = "不合格";
                        MItem[0]["HG_CCPC"] = "不合格";
                        mAllHg = false;
                    }
                    else
                    {
                        MItem[0]["HG_WGZL"] = "合格";
                        MItem[0]["HG_CCPC"] = "合格";
                    }
                }
                else
                {
                    MItem[0]["HG_WGZL"] = "----";
                    MItem[0]["HG_CCPC"] = "----";
                }
                #endregion

                #region 强度等级
                if (jcxm.Contains("、强度等级、"))
                {
                    jcxmCur = "强度等级";
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJDJ"].Trim() && u["JCXM"] == "强度等级");
                    if (null != extraFieldsDj)
                    {
                        #region
                        //sItem["QCD1"] = (Math.Round((Conversion.Val(sItem["QCD1_1"]) + Conversion.Val(sItem["QCD1_2"])) / 2, 0)).ToString();
                        //sItem["QKD1"] = (Math.Round((Conversion.Val(sItem["QKD1_1"]) + Conversion.Val(sItem["QKD1_2"])) / 2, 0)).ToString();
                        //sItem["QMJ1"] = (Conversion.Val(sItem["QCD1"]) * Conversion.Val(sItem["QKD1"])).ToString();

                        //sItem["QCD2"] = (Math.Round((Conversion.Val(sItem["QCD2_1"]) + Conversion.Val(sItem["QCD2_2"])) / 2, 0)).ToString();
                        //sItem["QKD2"] = (Math.Round((Conversion.Val(sItem["QKD2_1"]) + Conversion.Val(sItem["QKD2_2"])) / 2, 0)).ToString();
                        //sItem["QMJ2"] = (Conversion.Val(sItem["QCD2"]) * Conversion.Val(sItem["QKD2"])).ToString();

                        //sItem["QCD3"] = (Math.Round((Conversion.Val(sItem["QCD3_1"]) + Conversion.Val(sItem["QCD3_2"])) / 2, 0)).ToString();
                        //sItem["QKD3"] = (Math.Round((Conversion.Val(sItem["QKD3_1"]) + Conversion.Val(sItem["QKD3_2"])) / 2, 0)).ToString();
                        //sItem["QMJ3"] = (Conversion.Val(sItem["QCD3"]) * Conversion.Val(sItem["QKD3"])).ToString();

                        //sItem["QCD4"] = (Math.Round((Conversion.Val(sItem["QCD4_1"]) + Conversion.Val(sItem["QCD4_2"])) / 2, 0)).ToString();
                        //sItem["QKD4"] = (Math.Round((Conversion.Val(sItem["QKD4_1"]) + Conversion.Val(sItem["QKD4_2"])) / 2, 0)).ToString();
                        //sItem["QMJ4"] = (Conversion.Val(sItem["QCD4"]) * Conversion.Val(sItem["QKD4"])).ToString();

                        //sItem["QCD5"] = (Math.Round((Conversion.Val(sItem["QCD5_1"]) + Conversion.Val(sItem["QCD5_2"])) / 2, 0)).ToString();
                        //sItem["QKD5"] = (Math.Round((Conversion.Val(sItem["QKD5_1"]) + Conversion.Val(sItem["QKD5_2"])) / 2, 0)).ToString();
                        //sItem["QMJ5"] = (Conversion.Val(sItem["QCD5"]) * Conversion.Val(sItem["QKD5"])).ToString();

                        //sItem["KYQD1"] = (1000 * Conversion.Val(sItem["KYHZ1"]) / Conversion.Val(sItem["QMJ1"])).ToString("0.0");
                        //sItem["KYQD2"] = (1000 * Conversion.Val(sItem["KYHZ2"]) / Conversion.Val(sItem["QMJ2"])).ToString("0.0");
                        //sItem["KYQD3"] = (1000 * Conversion.Val(sItem["KYHZ3"]) / Conversion.Val(sItem["QMJ3"])).ToString("0.0");
                        //sItem["KYQD4"] = (1000 * Conversion.Val(sItem["KYHZ4"]) / Conversion.Val(sItem["QMJ4"])).ToString("0.0");
                        //sItem["KYQD5"] = (1000 * Conversion.Val(sItem["KYHZ5"]) / Conversion.Val(sItem["QMJ5"])).ToString("0.0");
                        #endregion

                        List<double> kyqdArray = new List<double>();
                        for (int i = 1; i < 6; i++)
                        {
                            sItem["QCD" + i] = (Math.Round((Conversion.Val(sItem["QCD" + i + "_1"]) + Conversion.Val(sItem["QCD" + i + "_2"])) / 2, 0)).ToString();
                            sItem["QKD" + i] = (Math.Round((Conversion.Val(sItem["QKD" + i + "_1"]) + Conversion.Val(sItem["QKD" + i + "_2"])) / 2, 0)).ToString();
                            sItem["QMJ" + i] = (Conversion.Val(sItem["QCD" + i]) * Conversion.Val(sItem["QKD" + i])).ToString();
                            if (Conversion.Val(sItem["QMJ" + i]) != 0)
                            {
                                sItem["KYQD" + i] = (1000 * Conversion.Val(sItem["KYHZ" + i]) / Conversion.Val(sItem["QMJ" + i])).ToString("0.0");
                            }
                            else
                            {
                                sItem["KYQD" + i] = "0";
                            }

                            kyqdArray.Add(double.Parse(sItem["KYQD" + i]));
                        }
                        kyqdArray.Sort();
                        double mMaxKyqd = 0, mMinKyqd = 0, mAvgKyqd = 0;
                        mMaxKyqd = kyqdArray[4];
                        mMinKyqd = kyqdArray[0];
                        mAvgKyqd = kyqdArray.Average();

                        sItem["DKZX"] = mMinKyqd.ToString("0.0");
                        sItem["KYPJ"] = mAvgKyqd.ToString("0.0");
                        MItem[0]["G_MIN"] = "平均值≥" + GetSafeDouble(extraFieldsDj["DKBXY"]).ToString("0.0") + "MPa";
                        MItem[0]["G_PJZ"] = "单块最小值≥" + GetSafeDouble(extraFieldsDj["PJBXY"]).ToString("0.0") + "MPa";

                        if (Conversion.Val(sItem["KYPJ"]) >= GetSafeDouble(extraFieldsDj["PJBXY"]) && Conversion.Val(sItem["DKZX"]) >= GetSafeDouble(extraFieldsDj["DKBXY"]))
                        {
                            sItem["QDPD"] = "符合";
                            sItem["JCJG"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["QDPD"] = "不符合";
                            sItem["JCJG"] = "不合格";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        mAllHg = false;
                        jsbeizhu = jsbeizhu + "强度等级依据不详";
                        continue;
                    }
                }
                else
                {
                    sItem["QDPD"] = "----";
                    sItem["KYPJ"] = "0";
                }
                #endregion

                #region 干燥收缩率
                if (jcxm.Contains("、干燥收缩率、"))
                {
                    jcxmCur = "干燥收缩率";
                    double gzssl = 0;
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u["JCXM"] == "干燥收缩率" && u["QKCZLX"] == sItem["QKCZLX"]);
                    if (null != extraFieldsDj)
                    {
                        gzssl = GetSafeDouble(extraFieldsDj["XXGZSSZ"]);
                        MItem[0]["G_GZSSL"] = "≤" + extraFieldsDj["XXGZSSZ"]+ "mm/m";
                    }
                    else
                    {
                        mAllHg = false;
                        jsbeizhu = jsbeizhu + "干燥收缩率依据不详";
                        continue;
                    }

                    if (Conversion.Val(sItem["GZSSL1"]) <= gzssl && Conversion.Val(sItem["GZSSL2"]) <= gzssl && Conversion.Val(sItem["GZSSL3"]) <= gzssl)
                    {
                        MItem[0]["HG_GZSSL"] = "合格";
                        MItem[0]["W_GZSSL"] = "----";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        MItem[0]["HG_GZSSL"] = "不合格";
                        MItem[0]["W_GZSSL"] = "----";
                    }
                }
                else
                {
                    MItem[0]["HG_GZSSL"] = "----";
                    MItem[0]["W_GZSSL"] = "----";
                    MItem[0]["G_GZSSL"] = "----";
                }
                #endregion

                #region 相对含水率
                if (jcxm.Contains("、相对含水率、"))
                {
                    jcxmCur = "相对含水率";
                    double bzhsl = 0;//标准含水率
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u["JCXM"] == "相对含水率" && u["XDHSLLX"] == sItem["XDHSLLX"]);
                    if (extraFieldsDj != null)
                    {
                        bzhsl = GetSafeDouble(extraFieldsDj["XXGZSSZ"]);
                        MItem[0]["G_HSL"] = "≤" + bzhsl.ToString()+ "mm/m";
                    }
                    else
                    {
                        mAllHg = false;
                        jsbeizhu = jsbeizhu + "相对含水率依据不详";
                        continue;
                    }

                    MItem[0]["W_HSL"] = ((GetSafeDouble(sItem["HSL1"]) + GetSafeDouble(sItem["HSL2"]) + GetSafeDouble(sItem["HSL3"])) / 3).ToString("0");
                    if (GetSafeDouble(MItem[0]["W_HSL"]) <= bzhsl)
                    {
                        MItem[0]["HG_HSL"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_HSL"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["HG_HSL"] = "----";
                    MItem[0]["G_HSL"] = "----";
                    MItem[0]["W_HSL"] = "----";
                }
                #endregion

                #region 抗冻性
                if (jcxm.Contains("、抗冻性、") && GetSafeDouble(sItem["GKB"]) > 0)  //GKB 抗冻性高宽比
                {
                    jcxmCur = "抗冻性";
                    MItem[0]["G_ZLSSL"] = "平均值≤5% 单块最大值≤10%";   //质量损失率
                    MItem[0]["G_QDSSL"] = "平均值≤20% 单块最大值≤30%";  //强度损失率
                    int max = 0;
                    if (GetSafeDouble(sItem["GKB"]) >= 0.6)
                    {
                        max = 10;
                    }
                    else if (GetSafeDouble(sItem["GKB"]) < 0.6)
                    {
                        max = 20;
                    }

                    List<double> zlArray = new List<double>();
                    List<double> qdArray = new List<double>();
                    for (int i = 1; i <= max; i++)
                    {
                        zlArray.Add(Conversion.Val(sItem["KDXZLSS" + i]));
                        qdArray.Add(Conversion.Val(sItem["KDXQDSS" + i]));
                    }

                    zlArray.Sort();
                    qdArray.Sort();

                    double zlMax = 0, qdMax = 0, zlAverage = 0, qdAverage = 0;
                    if (max > 0)
                    {
                        zlMax = zlArray[max - 1];
                        qdMax = qdArray[max - 1];
                    }

                    zlAverage = zlArray.Average();
                    qdAverage = qdArray.Average();
                    MItem[0]["W_ZLSSL"] = "平均值" + zlAverage.ToString() + "% 单块最大值" + zlMax.ToString() + "%";
                    MItem[0]["W_QDSSL"] = "平均值" + qdAverage.ToString() + "% 单块最大值" + qdMax.ToString() + "%";

                    if (zlAverage <= 5 && qdAverage <= 20 && zlMax <= 10 && qdMax <= 30)
                    {
                        MItem[0]["HG_KDX"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_KDX"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["G_ZLSSL"] = "----";
                    MItem[0]["G_QDSSL"] = "----";
                    MItem[0]["W_ZLSSL"] = "----";
                    MItem[0]["W_QDSSL"] = "----";
                    MItem[0]["HG_KDX"] = "----";
                }
                #endregion

                #region 抗渗性
                if (jcxm.Contains("、抗渗性、"))
                {
                    jcxmCur = "抗渗性";
                    MItem[0]["G_KS"] = "≤10";
                    if (Conversion.Val(sItem["SMXJGD1"]) <= 10 && Conversion.Val(sItem["SMXJGD2"]) <= 10 && Conversion.Val(sItem["SMXJGD3"]) <= 10)
                    {
                        MItem[0]["HG_KS"] = "合格";
                        MItem[0]["W_KS"] = "----";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        MItem[0]["HG_KS"] = "不合格";
                        MItem[0]["W_KS"] = "----";
                    }
                }
                else
                {
                    MItem[0]["HG_KS"] = "----";
                    MItem[0]["W_KS"] = "----";
                    MItem[0]["G_KS"] = "----";
                }
                #endregion
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

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
