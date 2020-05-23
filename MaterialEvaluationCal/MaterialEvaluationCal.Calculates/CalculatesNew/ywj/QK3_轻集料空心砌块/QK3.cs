using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class QK3 : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/
            var extraDJ = dataExtra["BZ_QK3_DJ"];
            var extraGMDJB = dataExtra["BZ_QK3GMDJB"];

            var data = retData;

            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var SItems = data["S_QK3"];

            if (!data.ContainsKey("M_QK3"))
            {
                data["M_QK3"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_QK3"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var mAllHg = true;

            string mJSFF = "";
            double mSz = 0;

            var jcxm = "";
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;
            List<double> mtmpArray = new List<double>();
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                #region 数据准备工作

                //获取设计等级
                if (string.IsNullOrEmpty(sItem["SJDJ"]))
                {
                    sItem["SJDJ"] = "";
                }

                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(sItem["SJDJ"].Trim()));
                if (null != extraFieldsDj)
                {
                    mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"];
                    sItem["MDDJFW"] = extraFieldsDj["MDDJFW"];
                    MItem[0]["G_MIN"] = extraFieldsDj["DKBXY"];
                    MItem[0]["G_PJZ"] = extraFieldsDj["PJBXY"];
                    if (MItem[0]["PDBZ"].ToString().ToUpper().Contains("2011"))
                    {
                        sItem["XSLYQ"] = "≤18";
                    }
                    else
                    {
                        sItem["XSLYQ"] = extraFieldsDj["XSL"];
                    }
                }
                else
                {
                    mJSFF = "";
                    mSz = 0;
                    mAllHg = false;
                    continue;
                }

                var extraFieldsGMDJB = extraGMDJB.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(sItem["GMDDJ"].Trim()));
                if (null != extraFieldsGMDJB)
                {
                    MItem[0]["G_GMD"] = extraFieldsGMDJB["GMD2"];
                }

                sItem["KDYQ"] = "质量损失率≤5% \r\n 强度损失率≤25%";
                sItem["ZLSSYQ"] = "≤5";
                sItem["QDSSYQ"] = "≤25";

                #endregion

                #region 初始化变量

                //MItem["WHICH"] = "bgqk3";
                double mtj1 = 0;
                double mtj2 = 0;
                double mtj3 = 0;
                double md1, md2, md, sum, pjmd = 0;
                #endregion

                if (jcxm.Contains("、干密度、"))
                {
                    jcxmCur = "干密度";
                    mtj1 = 0;
                    mtj1 = 0;
                    double mtj = 0;
                    double pj = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["CD" + i] = Math.Round((GetSafeDouble(sItem["CD" + i + "_1"]) + GetSafeDouble(sItem["CD" + i + "_2"])) / 2, 0).ToString();
                        sItem["KD" + i] = Math.Round((GetSafeDouble(sItem["KD" + i + "_1"]) + GetSafeDouble(sItem["KD" + i + "_2"])) / 2, 0).ToString();
                        sItem["GD" + i] = Math.Round((GetSafeDouble(sItem["GD" + i + "_1"]) + GetSafeDouble(sItem["GD" + i + "_2"])) / 2, 0).ToString();

                        mtj = GetSafeDouble(sItem["CD" + i]) / 1000 * GetSafeDouble(sItem["KD" + i]) / 1000 * GetSafeDouble(sItem["GD" + i]) / 1000;
                        if (mtj != 0)
                        {
                            //mtj = Math.Round(mtj, 3);
                            sItem["GMD" + i] = (Math.Round((GetSafeDouble(sItem["HGHZL" + i]) / mtj) / 10, 0) * 10).ToString();
                            pj += Math.Round((GetSafeDouble(sItem["HGHZL" + i]) / mtj) / 10, 0) * 10;
                        }
                        else
                        {
                            sItem["GMD" + i] = "0";
                        }
                    }

                    sItem["GMDPJ"] = (Math.Round(pj / 3 / 10, 0) * 10).ToString();

                    //sItem["GMDPJ"] = (Math.Round((GetSafeDouble(sItem["GMD1"]) + GetSafeDouble(sItem["GMD2"]) + GetSafeDouble(sItem["GMD3"])) / 3 / 10, 0) * 10).ToString();

                }
                else
                {
                    sItem["GMDPD"] = "----";
                }

                if (jcxm.Contains("、抗压强度、"))
                {
                    //if (Conversion.Val(sItem["KYHZ1"]) == 0)
                    //{
                    //    sItem["syr"] = "";
                    //}
                    jcxmCur = "抗压强度";
                    sItem["QDYQ"] = "抗压强度平均值需≥" + GetSafeDouble(extraFieldsDj["PJBXY"]).ToString("0.0") + "MPa,\r\n 单块最小强度值需≥" + GetSafeDouble(extraFieldsDj["DKBXY"]).ToString("0.0") + "MPa";

                    sItem["QCD1"] = Math.Round((GetSafeDouble(sItem["QCD1_1"]) + GetSafeDouble(sItem["QCD1_2"])) / 2, 0).ToString();
                    sItem["QKD1"] = Math.Round((GetSafeDouble(sItem["QKD1_1"]) + GetSafeDouble(sItem["QKD1_2"])) / 2, 0).ToString();
                    sItem["QMJ1"] = (GetSafeDouble(sItem["QCD1"]) * GetSafeDouble(sItem["QKD1"])).ToString();

                    sItem["QCD2"] = Math.Round((GetSafeDouble(sItem["QCD2_1"]) + GetSafeDouble(sItem["QCD2_2"])) / 2, 0).ToString();
                    sItem["QKD2"] = Math.Round((GetSafeDouble(sItem["QKD2_1"]) + GetSafeDouble(sItem["QKD2_2"])) / 2, 0).ToString();
                    sItem["QMJ2"] = (GetSafeDouble(sItem["QCD2"]) * GetSafeDouble(sItem["QKD2"])).ToString();

                    sItem["QCD3"] = Math.Round((GetSafeDouble(sItem["QCD3_1"]) + GetSafeDouble(sItem["QCD3_2"])) / 2, 0).ToString();
                    sItem["QKD3"] = Math.Round((GetSafeDouble(sItem["QKD3_1"]) + GetSafeDouble(sItem["QKD3_2"])) / 2, 0).ToString();
                    sItem["QMJ3"] = (GetSafeDouble(sItem["QCD3"]) * GetSafeDouble(sItem["QKD3"])).ToString();

                    sItem["QCD4"] = Math.Round((GetSafeDouble(sItem["QCD4_1"]) + GetSafeDouble(sItem["QCD4_2"])) / 2, 0).ToString();
                    sItem["QKD4"] = Math.Round((GetSafeDouble(sItem["QKD4_1"]) + GetSafeDouble(sItem["QKD4_2"])) / 2, 0).ToString();
                    sItem["QMJ4"] = (GetSafeDouble(sItem["QCD4"]) * GetSafeDouble(sItem["QKD4"])).ToString();

                    sItem["QCD5"] = Math.Round((GetSafeDouble(sItem["QCD5_1"]) + GetSafeDouble(sItem["QCD5_2"])) / 2, 0).ToString();
                    sItem["QKD5"] = Math.Round((GetSafeDouble(sItem["QKD5_1"]) + GetSafeDouble(sItem["QKD5_2"])) / 2, 0).ToString();
                    sItem["QMJ5"] = (GetSafeDouble(sItem["QCD5"]) * GetSafeDouble(sItem["QKD5"])).ToString();

                    for (int i = 1; i < 6; i++)
                    {
                        if (GetSafeDouble(sItem["QMJ" + i]) == 0)
                            sItem["KYQD" + i] = "0";
                        else
                            sItem["KYQD" + i] = Math.Round(1000 * GetSafeDouble(sItem["KYHZ" + i]) / GetSafeDouble(sItem["QMJ" + i]), 2).ToString("0.00");
                        mtmpArray.Add(GetSafeDouble(sItem["KYQD" + i]));
                    }

                    sItem["KYPJ"] = mtmpArray.Average().ToString("0.0");
                    mtmpArray.Sort();
                    mMaxKyqd = mtmpArray[4];
                    mMinKyqd = mtmpArray[0];

                    sItem["DKZX"] = mMinKyqd.ToString();

                    if (GetSafeDouble(sItem["KYPJ"]) >= GetSafeDouble(extraFieldsDj["PJBXY"]) && GetSafeDouble(sItem["DKZX"]) >= GetSafeDouble(extraFieldsDj["DKBXY"]) && GetSafeDouble(sItem["GMDPJ"]) <= GetSafeDouble(extraFieldsDj["MDDJFW"]))
                    {
                        sItem["QDPD"] = "合格";   //强度判定（是否合格）
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["QDPD"] = "不合格";
                    }

                    if (GetSafeDouble(sItem["GMDPJ"]) <= GetSafeDouble(extraFieldsDj["MDDJFW"]))
                    {
                        sItem["GMDPD"] = "合格";  //干密度判定（是否合格）
                    }
                    else
                    {
                        jcxmCur = "干密度";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GMDPD"] = "不合格";
                    }

                    sItem["MDDJFW"] = "密度等级范围≤" + GetSafeDouble(sItem["MDDJFW"]).ToString("0") + "kg/m&scsup3&scend。";


                }
                else
                {
                    sItem["QDPD"] = "----";
                    sItem["KYPJ"] = "0";
                }

                if (jcxm.Contains("、含水率、"))
                {
                    jcxmCur = "含水率";
                    if (MItem[0]["PDBZ"].ToString().ToUpper().Contains("2011"))
                    {
                        sItem["XSLYQ"] = "≤18";
                    }
                    else
                    {
                        sItem["XSLYQ"] = extraFieldsDj["XSL"];
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        if (GetSafeDouble(sItem["HXSM_" + i]) == 0)
                        {
                            sItem["HXSW2_" + i] = "0";
                        }
                        else
                        {
                            sItem["HXSW2_" + i] = Math.Round((GetSafeDouble(sItem["HXSM2_" + i]) - GetSafeDouble(sItem["HXSM_" + i])) / GetSafeDouble(sItem["HXSM_" + i]) * 100, 2).ToString("0.00"); ;

                        }
                    }
                    sItem["HXSW2"] = Math.Round((GetSafeDouble(sItem["HXSW2_1"]) + GetSafeDouble(sItem["HXSW2_2"]) + GetSafeDouble(sItem["HXSW2_3"])) / 3, 1).ToString("0.00");

                    if (IsQualified(sItem["XSLYQ"], sItem["HXSW2"]).Equals("合格"))
                    {
                        sItem["XSLPD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["XSLPD"] = "不合格";
                    }
                }
                else
                {
                    sItem["XSLPD"] = "----";
                    sItem["HXSW2_1"] = "----";
                    sItem["HXSW2_2"] = "----";
                    sItem["HXSW2_3"] = "----";
                    sItem["XSLYQ"] = "----";
                    sItem["HXSW2"] = "----";

                }

                var sign = true;

                if (jcxm.Contains("、抗冻性、"))
                {
                    jcxmCur = "抗冻性";
                    sItem["KDYQ"] = "质量损失率≤5% \r\n 强度损失率≤25%";
                    sItem["ZLSSYQ"] = "≤5";
                    sItem["QDSSYQ"] = "≤25";
                    sum = 0;

                    for (int i = 1; i < 6; i++)
                    {
                        if (!IsNumeric(sItem["KYPJ"]))
                        {
                            sign = false;
                            break;
                        }

                        if (!IsNumeric(sItem["DQZL" + i]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sItem["DHZL" + i]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sItem["DHCD" + i + "_1"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sItem["DHCD" + i + "_2"]))
                        {
                            sign = false;
                            break;
                        }

                        if (!IsNumeric(sItem["DHKD" + i + "_1"]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sItem["DHKD" + i + "_2"]))
                        {
                            sign = false;
                            break;
                        }

                        if (!IsNumeric(sItem["DHKYHZ" + i]))
                        {
                            sign = false;
                            break;
                        }
                    }

                    if (sign)
                    {
                        sum = 0;
                        for (int i = 1; i < 6; i++)
                        {
                            md1 = GetSafeDouble(sItem["DQZL" + i]);
                            md2 = GetSafeDouble(sItem["DHZL" + i]);
                            md = Math.Round(100 * (md1 - md2) / md1, 1);
                            sum = md + sum;
                        }
                        pjmd = Math.Round(sum / 5, 1);
                        sItem["W_ZLSS"] = pjmd.ToString("0.0");

                        sum = 0;
                        for (int i = 1; i < 6; i++)
                        {
                            md = 1;
                            md1 = GetSafeDouble(sItem["DHCD" + i + "_1"]);
                            md2 = GetSafeDouble(sItem["DHCD" + i + "_2"]);
                            pjmd = Math.Round((md1 + md2) / 2, 0);
                            md = md * pjmd;

                            md1 = GetSafeDouble(sItem["DHKD" + i + "_1"]);
                            md2 = GetSafeDouble(sItem["DHKD" + i + "_2"]);
                            pjmd = Math.Round((md1 + md2) / 2, 0);
                            md = md * pjmd;

                            md = Math.Round(1000 * GetSafeDouble(sItem["DHKYHZ" + i]) / md, 1);
                            sum = sum + md;
                        }

                        pjmd = sum / 5;
                        md1 = GetSafeDouble(sItem["KYPJ"]);
                        md2 = Math.Round(pjmd, 1);
                        md = Math.Round(100 * (md1 - md2) / md1, 0);
                        sItem["W_QDSS"] = md.ToString("0");


                        if (IsQualified(sItem["ZLSSYQ"], sItem["W_ZLSS"]).Equals("合格") && IsQualified(sItem["QDSSYQ"], sItem["W_QDSS"]).Equals("合格"))
                        {
                            sItem["KDPD"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["KDPD"] = "不合格";
                        }
                    }
                }
                else
                {
                    //mrsmainTablesItem["which") = "bgqk3"
                    sign = false;
                }

                if (jcxm.Contains("、外观质量、") || jcxm.Contains("、尺寸偏差、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "外观质量,尺寸偏差");
                    if (Conversion.Val(sItem["WCBHGS"]) < 7)
                    { 
                        sItem["WCPD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["WCPD"] = "不合格";
                    }
                        
                }
                else
                    sItem["WCPD"] = "----";

                if (!sign)
                {
                    sItem["KDYQ"] = "----";
                    sItem["KDPD"] = "----";
                    sItem["W_ZLSS"] = "----";
                    sItem["W_QDSS"] = "----";
                    sItem["QDSSYQ"] = "----";
                    sItem["ZLSSYQ"] = "----";
                }

                if (sItem["XSLPD"] == "不合格" || sItem["QDPD"] == "不合格" || sItem["GMDPD"] == "不合格" || sItem["KDPD"] == "不合格" || sItem["WCPD"] == "不合格")
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                }
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                    sItem["JCJG"] = "合格";
                }



            }
            #region 添加最终报告

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
        }
    }
}
