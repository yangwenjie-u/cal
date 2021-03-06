﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    /*粉煤灰小型空心砌块*/
    public class FK : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_FK_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_FKS = data["S_FK"];
            if (!data.ContainsKey("M_FK"))
            {
                data["M_FK"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_FK"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            bool sign, flag = true;
            bool mSFwc = true;
            double sum = 0;
            int mbHggs = 0;//记录合格数量
            var jcxmBhg = "";
            var jcxmCur = "";
            foreach (var sItem in S_FKS)
            {

                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                //干密度、抗压、相对含水率
                #region 抗压
                if (jcxm.Contains("、抗压、"))
                {
                    jcxmCur = "抗压";
                    List<double> nArr = new List<double>();
                    nArr.Add(0);
                    string Qdavg = "";
                    string Qdmin = "";
                    //遍历等级表
                    int xd = 0;
                    foreach (var extraFieldsDj in extraDJ)
                    {
                        if (extraFieldsDj["QD_DJ"].Trim() == sItem["SJDJ"])
                        {
                            Qdavg = extraFieldsDj["QD_AVG"].Trim();
                            Qdmin = extraFieldsDj["QD_DKZ"].Trim();
                            break;
                        }
                        xd++;
                    }
                    if (xd > extraDJ.Count)
                    {
                        mAllHg = false;
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        continue;
                    }
                    sItem["GH_QD"] = "抗压强度平均值需" + Qdavg + "MPa," + "\r\n" + "单块最小强度值需" + Qdmin + "MPa。";
                    for (int i = 1; i < 6; i++)
                    {
                        flag = IsNumeric(sItem["QCD" + i + "_1"]) ? flag : false;
                        flag = IsNumeric(sItem["QCD" + i + "_2"]) ? flag : false;
                        flag = IsNumeric(sItem["QKD" + i + "_1"]) ? flag : false;
                        flag = IsNumeric(sItem["QKD" + i + "_2"]) ? flag : false;
                        flag = IsNumeric(sItem["KYHZ" + i]) ? flag : false;
                        if (!flag)
                        {
                            break;
                        }
                    }
                    if (!flag)
                    {
                        mSFwc = false;
                        sItem["KYPD"] = "----";
                    }
                    double cd1, cd2, md1, kd1, kd2, md2, md, pjmd = 0;
                    if (flag)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            cd1 = double.Parse(sItem["QCD" + i + "_1"].Trim());
                            cd2 = double.Parse(sItem["QCD" + i + "_2"].Trim());
                            md1 = Math.Round((cd1 + cd2) / 2, 0);

                            kd1 = double.Parse(sItem["QKD" + i + "_1"].Trim());
                            kd2 = double.Parse(sItem["QKD" + i + "_2"].Trim());
                            md2 = Math.Round((kd1 + kd2) / 2, 0);

                            md = double.Parse(sItem["KYHZ" + i].Trim());
                            md = Math.Round(1000 * md / (md1 * md2), 1);
                            sItem["KYQD" + i] = md.ToString("0.0");

                            nArr.Add(md);
                            sum = sum + md;
                        }
                        pjmd = Math.Round(sum / 5, 1);
                        sItem["QD_AVG"] = pjmd.ToString("0.0");
                        nArr.Sort();

                        md = Math.Round(nArr[1], 1);
                        sItem["QD_MIN"] = md.ToString("0.0");

                        //判定
                        if (IsQualified(Qdavg, sItem["QD_AVG"], true) == "不符合" || IsQualified(Qdmin, sItem["QD_MIN"], true) == "不符合")
                        {
                            sItem["KYPD"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            sItem["KYPD"] = "合格";
                            mbHggs++;
                        }
                    }
                }
                else
                {
                    sItem["KYPD"] = "----";
                    sItem["GH_QD"] = "----";
                    sItem["QD_AVG"] = "----";
                    sItem["QD_MIN"] = "----";
                }
                #endregion

                #region 干密度
                if (jcxm.Contains("、干密度、"))
                {
                    jcxmCur = "干密度";
                    List<double> nArr = new List<double>();
                    nArr.Add(0);
                    mSFwc = true;
                    //遍历等级表
                    int xd = 0;
                    foreach (var extraFieldsDj in extraDJ)
                    {
                        if (extraFieldsDj["MD_DJ"].Trim() == sItem["MDDJ"])
                        {
                            sItem["GH_MD"] = extraFieldsDj["MD_FW"].Trim();
                            break;
                        }
                        xd++;
                    }
                    if (xd > extraDJ.Count)
                    {
                        mAllHg = false;
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        continue;
                    }
                    flag = true;

                    for (int i = 1; i < 4; i++)
                    {
                        sItem["GMD" + i] = Math.Round(Conversion.Val(sItem["HGHZL" + i]) / (((Conversion.Val(sItem["CD" + i + "_1"]) + Conversion.Val(sItem["CD" + i + "_2"])) / 2 / 1000) * ((Conversion.Val(sItem["KD" + i + "_1"]) + Conversion.Val(sItem["KD" + i + "_2"])) / 2 / 1000) * ((Conversion.Val(sItem["GD" + i + "_1"]) + Conversion.Val(sItem["GD" + i + "_2"])) / 2 / 1000)), 0).ToString();
                        nArr.Add(Conversion.Val(sItem["GMD" + i]));
                    }
                    sItem["GMDPJ"] = nArr.Average().ToString();
                    //for (int i = 1; i < 4; i++)
                    //{
                    //    flag = IsNumeric(sItem["GMD" + i]) ? flag : false;
                    //    if (!flag)
                    //    {
                    //        break;
                    //    }
                    //}
                    //if (!flag)
                    //{
                    //    mSFwc = false;
                    //    sItem["MDPD"] = "----";
                    //}
                    //else
                    //{
                    //判定
                    if (IsQualified(sItem["GH_MD"], sItem["GMDPJ"], true) == "不符合")
                    {
                        sItem["MDPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        sItem["MDPD"] = "合格";
                        mbHggs++;
                    }
                    //}
                    sItem["GH_MD"] = sItem["GH_MD"].Trim() + "(kg/m&scsup3&scend)";
                }
                else
                {
                    sItem["MDPD"] = "----";
                    sItem["GH_MD"] = "----";
                    sItem["GMDPJ"] = "----";
                }
                #endregion

                #region 相对含水率
                //if (jcxm.Contains("、相对含水率、"))
                //{
                //    List<double> nArr = new List<double>();
                //    nArr.Add(0);
                //    //遍历等级表
                //    int xd = 0;
                //    foreach (var extraFieldsDj in extraDJ)
                //    {
                //        if (extraFieldsDj["SY_DQ"].Trim() == sItem["SYDQ"])
                //        {
                //            sItem["GH_HSL"] = extraFieldsDj["XD_HSL"].Trim();
                //            break;
                //        }
                //        xd++;
                //    }
                //    if (xd > extraDJ.Count)
                //    {
                //        mAllHg = false;
                //        sItem["JCJG"] = "不合格";
                //        continue;
                //    }
                //    flag = true;
                //    for (int i = 1; i < 4; i++)
                //    {
                //        flag = IsNumeric(sItem["H_QZL" + i]) ? flag : false;
                //        flag = IsNumeric(sItem["H_JZL" + i]) ? flag : false;
                //        flag = IsNumeric(sItem["X_GZL" + i]) ? flag : false;
                //        flag = IsNumeric(sItem["X_JZL" + i]) ? flag : false;
                //        if (!flag)
                //        {
                //            break;
                //        }
                //    }
                //    if (!flag)
                //    {
                //        mSFwc = false;
                //        sItem["PD_HSL"] = "----";
                //    }
                //    else
                //    {
                //        double cd1, cd2, md1, kd1, kd2, md2, md, pjmd = 0;
                //        sum = 0;
                //        for (int i = 1; i < 4; i++)
                //        {
                //            cd1 = double.Parse(sItem["H_QZL" + i].Trim());
                //            cd2 = double.Parse(sItem["H_JZL" + i].Trim());
                //            md1 = Math.Round(100 * (cd1 - cd2) / cd2, 1); ;
                //            sItem["HSL" + i] = md1.ToString("0.0");

                //            kd1 = double.Parse(sItem["X_GZL" + i].Trim());
                //            kd2 = double.Parse(sItem["X_JZL" + i].Trim());
                //            md2 = Math.Round(100 * (kd1 - kd2) / kd2, 1);
                //            sItem["XSL" + i] = md2.ToString("0.0");
                //        }
                //        sum = 0;
                //        for (int i = 1; i < 4; i++)
                //        {
                //            sum = sum + double.Parse(sItem["HSL" + i].Trim());
                //        }
                //        pjmd = Math.Round(sum / 3, 1);
                //        sItem["PJHSL"] = pjmd.ToString("0.0");

                //        sum = 0;
                //        for (int i = 1; i < 4; i++)
                //        {
                //            sum = sum + double.Parse(sItem["XSL" + i].Trim());
                //        }
                //        pjmd = Math.Round(sum / 3, 1);
                //        sItem["PJXSL"] = pjmd.ToString("0.0");

                //        md1 = double.Parse(sItem["PJHSL"].Trim());
                //        md2 = double.Parse(sItem["PJXSL"].Trim());
                //        md = Math.Round(100 * md1 / md2, 1);
                //        sItem["XDHSL"] = md.ToString("0.0");

                //        //判定
                //        if (IsQualified(sItem["GH_HSL"], sItem["XDHSL"], true) == "不符合")
                //        {
                //            sItem["PD_HSL"] = "不合格";
                //            itemHG = false;
                //            mAllHg = false;
                //        }
                //        else
                //        {
                //            sItem["PD_HSL"] = "合格";
                //            mbHggs++;
                //        }

                //    }


                //}
                //else
                //{
                //    sItem["PD_HSL"] = "----";
                //    sItem["GH_HSL"] = "----";
                //    sItem["XDHSL"] = "----";
                //    sItem["PJHSL"] = "----";
                //    sItem["PJXSL"] = "----";
                //}
                #endregion

                #region 含水率
                if (jcxm.Contains("、含水率、"))
                {
                    jcxmCur = "含水率";
                    List<double> nArr = new List<double>();
                    nArr.Add(0);
                    //遍历等级表
                    int xd = 0;
                    foreach (var extraFieldsDj in extraDJ)
                    {
                        if (extraFieldsDj["SY_DQ"].Trim() == sItem["SYDQ"])
                        {
                            sItem["GH_HSL"] = extraFieldsDj["XD_HSL"].Trim();
                            break;
                        }
                        xd++;
                    }
                    if (xd > extraDJ.Count)
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["JCJG"] = "不合格";
                        continue;
                    }
                    flag = true;
                    for (int i = 1; i < 4; i++)
                    {
                        flag = IsNumeric(sItem["H_QZL" + i]) ? flag : false;
                        flag = IsNumeric(sItem["H_JZL" + i]) ? flag : false;
                        if (!flag)
                        {
                            break;
                        }
                    }
                    if (!flag)
                    {
                        mSFwc = false;
                        sItem["PD_HSL"] = "----";
                    }
                    else
                    {
                        double cd1, cd2, md1, kd1, kd2, md2, md, pjmd = 0;
                        sum = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            cd1 = double.Parse(sItem["H_QZL" + i].Trim());
                            cd2 = double.Parse(sItem["H_JZL" + i].Trim());
                            md1 = Math.Round(100 * (cd1 - cd2) / cd2, 1); ;
                            sItem["HSL" + i] = md1.ToString("0.0");

                        }
                        sum = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            sum = sum + double.Parse(sItem["HSL" + i].Trim());
                        }
                        pjmd = Math.Round(sum / 3, 1);
                        sItem["PJHSL"] = pjmd.ToString("0.0");

                        //判定
                        if (IsQualified(sItem["GH_HSL"], sItem["PJHSL"], true) == "不符合")
                        {
                            sItem["PD_HSL"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            sItem["PD_HSL"] = "合格";
                            mbHggs++;
                        }

                    }


                }
                else
                {
                    sItem["PD_HSL"] = "----";
                    sItem["GH_HSL"] = "----";
                    //sItem["XDHSL"] = "----";
                    sItem["PJHSL"] = "----";
                }
                #endregion

                #region 吸水率
                if (jcxm.Contains("、吸水率、"))
                {
                    jcxmCur = "吸水率";
                    List<double> nArr = new List<double>();
                    nArr.Add(0);
                    //遍历等级表
                    int xd = 0;
                    foreach (var extraFieldsDj in extraDJ)
                    {
                        if (extraFieldsDj["SY_DQ"].Trim() == sItem["SYDQ"])
                        {
                            sItem["GH_HSL"] = extraFieldsDj["XD_HSL"].Trim();
                            break;
                        }
                        xd++;
                    }
                    if (xd > extraDJ.Count)
                    {
                        mAllHg = false;
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        continue;
                    }
                    flag = true;
                    for (int i = 1; i < 4; i++)
                    {
                        flag = IsNumeric(sItem["X_GZL" + i]) ? flag : false;
                        flag = IsNumeric(sItem["X_JZL" + i]) ? flag : false;
                        if (!flag)
                        {
                            break;
                        }
                    }
                    if (!flag)
                    {
                        mSFwc = false;
                        sItem["PD_XSL"] = "----";
                    }
                    else
                    {
                        double cd1, cd2, md1, kd1, kd2, md2, md, pjmd = 0;
                        sum = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            kd1 = double.Parse(sItem["X_GZL" + i].Trim());
                            kd2 = double.Parse(sItem["X_JZL" + i].Trim());
                            md2 = Math.Round(100 * (kd1 - kd2) / kd2, 1);
                            sItem["XSL" + i] = md2.ToString("0.0");
                        }

                        sum = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            sum = sum + double.Parse(sItem["XSL" + i].Trim());
                        }
                        pjmd = Math.Round(sum / 3, 1);
                        sItem["PJXSL"] = pjmd.ToString("0.0");

                        //判定
                        if (IsQualified(sItem["GH_HSL"], sItem["PJXSL"], true) == "不符合")
                        {
                            sItem["PD_XSL"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            sItem["PD_XSL"] = "合格";
                            mbHggs++;
                        }

                    }
                }
                else
                {
                    sItem["PD_XSL"] = "----";
                    sItem["GH_HSL"] = "----";
                    //sItem["XDHSL"] = "----";
                    sItem["PJXSL"] = "----";
                }
                #endregion

                #region 抗冻性 OLD
                //if (jcxm.Contains("、抗冻性、"))
                //{
                //    sItem["KDYQ"] = "质量损失率≤5%" + "\r\n" + "强度损失率≤25%";
                //    sItem["ZLSSYQ"] = "≤5";
                //    sItem["QDSSYQ"] = "≤25";

                //    sign = true;
                //    sign = IsNumeric(sItem["W_ZLSS"]) && !string.IsNullOrEmpty(sItem["W_ZLSS"]) ? sign : false;
                //    sign = IsNumeric(sItem["W_QDSS"]) && !string.IsNullOrEmpty(sItem["W_QDSS"]) ? sign : false;

                //    if (IsQualified(sItem["ZLSSYQ"], sItem["W_ZLSS"], false) == "合格" && IsQualified(sItem["QDSSYQ"], sItem["W_QDSS"], false) == "合格")
                //    {
                //        sItem["KDPD"] = "合格";
                //        mbHggs++;
                //    }
                //    else
                //    {
                //        sItem["KDPD"] = "不合格";
                //        itemHG = false;
                //        mAllHg = false;
                //    }
                //}
                //else
                //{
                //    sItem["KDYQ"] = "----";
                //    sItem["KDPD"] = "----";
                //    sItem["W_ZLSS"] = "----";
                //    sItem["W_QDSS"] = "----";
                //    sItem["QDSSYQ"] = "----";
                //    sItem["ZLSSYQ"] = "----";
                //}
                #endregion

                #region 抗冻性
                if (jcxm.Contains("、抗冻性、"))
                {
                    jcxmCur = "抗冻性";
                    #region 质量损失率
                    sItem["KDYQ"] = "质量损失率≤5%" + "\r\n" + "强度损失率≤25%";
                    sItem["ZLSSYQ"] = "≤5";
                    sItem["QDSSYQ"] = "≤25";
                    if (!string.IsNullOrEmpty(sItem["DRDQGZ1"]))
                    {
                        List<double> larray = new List<double>();
                        for (int i = 1; i < 6; i++)
                        {
                            sItem["DRZLSSL" + i] = Math.Round((Conversion.Val(sItem["DRDQGZ" + i]) - Conversion.Val(sItem["DRDHGZ" + i])) / Conversion.Val(sItem["DRDQGZ" + i]) * 100, 1).ToString();
                            larray.Add(Conversion.Val(sItem["DRZLSSL" + i]));
                        }
                        sItem["DRZLSSL"] = Math.Round(larray.Average(), 1).ToString();
                    }
                    #endregion

                    #region 抗压强度损失率

                    if (!string.IsNullOrEmpty(sItem["DRSYZCD1"]))
                    {
                        List<double> syarray = new List<double>();
                        List<double> dbarray = new List<double>();
                        for (int i = 1; i < 6; i++)
                        {
                            sItem["DRSYZKYQD" + i] = Math.Round(Conversion.Val(sItem["DRSYZHZ" + i]) * 1000 / (Conversion.Val(sItem["DRSYZCD" + i]) * Conversion.Val(sItem["DRSYZKD" + i])), 1).ToString();
                            sItem["DRDBZKYQD" + i] = Math.Round(Conversion.Val(sItem["DRDBZHZ" + i]) * 1000 / (Conversion.Val(sItem["DRDBZCD" + i]) * Conversion.Val(sItem["DRDBZKD" + i])), 1).ToString();
                            syarray.Add(Conversion.Val(sItem["DRSYZKYQD" + i]));
                            dbarray.Add(Conversion.Val(sItem["DRDBZKYQD" + i]));
                        }
                        sItem["DRSYZKYQD"] = Math.Round(Conversion.Val(syarray.Average()), 1).ToString();
                        sItem["DRDBZKYQD"] = Math.Round(Conversion.Val(dbarray.Average()), 1).ToString();
                        sItem["DRQDSSL"] = Math.Round((Conversion.Val(sItem["DRDBZKYQD"]) - Conversion.Val(sItem["DRSYZKYQD"])) / Conversion.Val(sItem["DRDBZKYQD"]) * 100, 0).ToString();
                    }
                    if (IsQualified(sItem["ZLSSYQ"], sItem["DRZLSSL"], false) == "合格" && IsQualified(sItem["QDSSYQ"], sItem["DRQDSSL"], false) == "合格")
                    {
                        sItem["KDPD"] = "合格";
                    }
                    else
                    {
                        sItem["KDPD"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    sItem["KDYQ"] = "质量损失" + sItem["ZLSSYQ"] + ",强度损失" + sItem["QDSSYQ"];
                    #endregion
                }
                else
                {
                    sItem["KDPD"] = "----";
                    sItem["KDYQ"] = "----";
                    sItem["DRQDSSL"] = "----";
                }
                #endregion

                //单组判断
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }

            //添加最终报告
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
            /************************ 代码结束 ********************/
        }
    }
}
