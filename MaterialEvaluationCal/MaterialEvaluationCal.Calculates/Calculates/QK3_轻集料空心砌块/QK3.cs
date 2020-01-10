using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class QK3 : BaseMethods
    {
        public void Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            #region
            /************************ 代码开始 *********************/
            List<double> mtmpArray = new List<double>();
            var extraDJ = dataExtra["BZ_QK3_DJ"];
            var extraGMDJB = dataExtra["BZ_QK3GMDJB"];
            int mbhggs = 0;//不合格数量
            string mJSFF = "";
            double mSz, mQdyq, mHsxs, mttjhsxs = 0;

            var jcxmItems = retData.Select(u => u.Key).ToArray();

            foreach (var jcxm in jcxmItems)
            {
                if (jcxm.ToUpper().Contains("BGJG"))
                {
                    continue;
                }

                var SItem = retData[jcxm]["S_QK3"];
                //var MItem = retData[jcxm]["M_QK3"];
                var XQData = retData[jcxm]["S_BY_RW_XQ"];

                double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;
                int index = -1;
                foreach (var sItem in SItem)
                {
                    index++;
                    XQData[index]["JCJG"] = "不合格";
                    XQData[index]["JCJGMS"] = "该组试样不合格。";
                    #region 数据准备工作
                    //试验员判断
                    //if (string.IsNullOrEmpty(sItem["SYR"]))
                    //{
                    //    mSFwc = false;
                    //    continue; 
                    //}

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
                        //MItem[0]["G_MIN"] = extraFieldsDj["dkbxy"];
                        //MItem[0]["G_PJZ"] = extraFieldsDj["pjbxy"];
                        //if (MItem[0]["PDBZ"].ToString().ToUpper().Contains("2011"))
                        //{
                        //    sItem["XSLYQ"] = "≤18";
                        //}
                        //else
                        //{
                        //    sItem["XSLYQ"] = extraFieldsDj["XSL"];
                        //}
                    }
                    else
                    {
                        mJSFF = "";
                        mSz = 0;
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "获取设计等级失败";
                        mbhggs++;
                        continue;
                    }

                    var extraFieldsGMDJB = extraGMDJB.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(sItem["GMDDJ"].Trim()));
                    if (null != extraFieldsGMDJB)
                    {
                        //MItem[0]["g_gmd"] = extraFieldsGMDJB["gmd2"];
                    }

                    sItem["KDYQ"] = "质量损失率≤5% \r\n 强度损失率≤25%";
                    sItem["ZLSSYQ"] = "≤5";
                    sItem["QDSSYQ"] = "≤25";

                    #endregion

                    #region 初始化变量

                    sItem["XSLPD"] = "----";
                    sItem["HXSW2_1"] = "----";
                    sItem["HXSW2_2"] = "----";
                    sItem["HXSW2_3"] = "----";
                    sItem["XSLYQ"] = "----";
                    sItem["HXSW2"] = "----";
                    sItem["GMDPD"] = "----";
                    sItem["QDPD"] = "----";
                    sItem["KYPJ"] = "0";

                    sItem["KDYQ"] = "----";
                    sItem["KDPD"] = "----";
                    sItem["W_ZLSS"] = "----";
                    sItem["W_QDSS"] = "----";
                    sItem["QDSSYQ"] = "----";
                    sItem["ZLSSYQ"] = "----";

                    //MItem["WHICH"] = "bgqk3";
                    double mtj1 = 0;
                    double mtj2 = 0;
                    double mtj3 = 0;
                    double md1, md2, md, sum, pjmd = 0;
                    #endregion

                    switch (jcxm)
                    {
                        #region 干密度
                        case "干密度":
                            mtj1 = 0;
                            mtj2 = 0;
                            mtj3 = 0;

                            sItem["CD1"] = Math.Round((GetSafeDouble(sItem["CD1_1"]) + GetSafeDouble(sItem["CD1_2"])) / 2, 0).ToString();
                            sItem["KD1"] = Math.Round((GetSafeDouble(sItem["KD1_1"]) + GetSafeDouble(sItem["KD1_2"])) / 2, 0).ToString();
                            sItem["GD1"] = Math.Round((GetSafeDouble(sItem["GD1_1"]) + GetSafeDouble(sItem["GD1_2"])) / 2, 0).ToString();
                            mtj1 = Math.Round(GetSafeDouble(sItem["CD1"]) / 1000 * GetSafeDouble(sItem["KD1"]) / 1000 * GetSafeDouble(sItem["GD1"]) / 1000, 3);

                            sItem["CD2"] = Math.Round((GetSafeDouble(sItem["CD2_1"]) + GetSafeDouble(sItem["CD2_2"])) / 2, 0).ToString();
                            sItem["KD2"] = Math.Round((GetSafeDouble(sItem["KD2_1"]) + GetSafeDouble(sItem["KD2_2"])) / 2, 0).ToString();
                            sItem["GD2"] = Math.Round((GetSafeDouble(sItem["GD2_1"]) + GetSafeDouble(sItem["GD2_2"])) / 2, 0).ToString();
                            mtj2 = Math.Round(GetSafeDouble(sItem["CD2"]) / 1000 * GetSafeDouble(sItem["KD2"]) / 1000 * GetSafeDouble(sItem["GD2"]) / 1000, 3);

                            sItem["CD3"] = Math.Round((GetSafeDouble(sItem["CD3_1"]) + GetSafeDouble(sItem["CD3_2"])) / 2, 0).ToString();
                            sItem["KD3"] = Math.Round((GetSafeDouble(sItem["KD3_1"]) + GetSafeDouble(sItem["KD3_2"])) / 2, 0).ToString();
                            sItem["GD3"] = Math.Round((GetSafeDouble(sItem["GD3_1"]) + GetSafeDouble(sItem["GD3_2"])) / 2, 0).ToString();
                            mtj3 = Math.Round(GetSafeDouble(sItem["CD3"]) / 1000 * GetSafeDouble(sItem["KD3"]) / 1000 * GetSafeDouble(sItem["GD3"]) / 1000, 3);

                            sItem["GMD1"] = (Math.Round((GetSafeDouble(sItem["HGHZL1"]) / mtj1) / 10, 0) * 10).ToString();
                            sItem["GMD2"] = (Math.Round((GetSafeDouble(sItem["HGHZL2"]) / mtj2) / 10, 0) * 10).ToString();
                            sItem["GMD3"] = (Math.Round((GetSafeDouble(sItem["HGHZL3"]) / mtj3) / 10, 0) * 10).ToString();

                            sItem["GMDPJ"] = (Math.Round((GetSafeDouble(sItem["GMD1"]) + GetSafeDouble(sItem["GMD2"]) + GetSafeDouble(sItem["GMD3"])) / 3 / 10, 0) * 10).ToString();

                            break;
                        #endregion
                        #region 抗压
                        case "抗压":
                            #region 与“干密度”相同
                            mtj1 = 0;
                            mtj2 = 0;
                            mtj3 = 0;

                            sItem["CD1"] = Math.Round((GetSafeDouble(sItem["CD1_1"]) + GetSafeDouble(sItem["CD1_2"])) / 2, 0).ToString();
                            sItem["KD1"] = Math.Round((GetSafeDouble(sItem["KD1_1"]) + GetSafeDouble(sItem["KD1_2"])) / 2, 0).ToString();
                            sItem["GD1"] = Math.Round((GetSafeDouble(sItem["GD1_1"]) + GetSafeDouble(sItem["GD1_2"])) / 2, 0).ToString();
                            mtj1 = Math.Round(GetSafeDouble(sItem["CD1"]) / 1000 * GetSafeDouble(sItem["KD1"]) / 1000 * GetSafeDouble(sItem["GD1"]) / 1000, 3);

                            sItem["CD2"] = Math.Round((GetSafeDouble(sItem["CD2_1"]) + GetSafeDouble(sItem["CD2_2"])) / 2, 0).ToString();
                            sItem["KD2"] = Math.Round((GetSafeDouble(sItem["KD2_1"]) + GetSafeDouble(sItem["KD2_2"])) / 2, 0).ToString();
                            sItem["GD2"] = Math.Round((GetSafeDouble(sItem["GD2_1"]) + GetSafeDouble(sItem["GD2_2"])) / 2, 0).ToString();
                            mtj2 = Math.Round(GetSafeDouble(sItem["CD2"]) / 1000 * GetSafeDouble(sItem["KD2"]) / 1000 * GetSafeDouble(sItem["GD2"]) / 1000, 3);

                            sItem["CD3"] = Math.Round((GetSafeDouble(sItem["CD3_1"]) + GetSafeDouble(sItem["CD3_2"])) / 2, 0).ToString();
                            sItem["KD3"] = Math.Round((GetSafeDouble(sItem["KD3_1"]) + GetSafeDouble(sItem["KD3_2"])) / 2, 0).ToString();
                            sItem["GD3"] = Math.Round((GetSafeDouble(sItem["GD3_1"]) + GetSafeDouble(sItem["GD3_2"])) / 2, 0).ToString();
                            mtj3 = Math.Round(GetSafeDouble(sItem["CD3"]) / 1000 * GetSafeDouble(sItem["KD3"]) / 1000 * GetSafeDouble(sItem["GD3"]) / 1000, 3);

                            sItem["GMD1"] = (Math.Round((GetSafeDouble(sItem["HGHZL1"]) / mtj1) / 10, 0) * 10).ToString();
                            sItem["GMD2"] = (Math.Round((GetSafeDouble(sItem["HGHZL2"]) / mtj2) / 10, 0) * 10).ToString();
                            sItem["GMD3"] = (Math.Round((GetSafeDouble(sItem["HGHZL3"]) / mtj3) / 10, 0) * 10).ToString();

                            sItem["GMDPJ"] = (Math.Round((GetSafeDouble(sItem["GMD1"]) + GetSafeDouble(sItem["GMD2"]) + GetSafeDouble(sItem["GMD3"])) / 3 / 10, 0) * 10).ToString();
                            #endregion

                            //if (GetSafeDouble(sItem["KYHZ1"]) == 0)
                            //{
                            //    sItem["SYR"] = "";
                            //}

                            sItem["QDYQ"] = "抗压强度平均值需≥" + extraFieldsDj["PJBXY"] + "MPa，\r\n 单块最小强度值需≥" + GetSafeDouble(extraFieldsDj["DKBXY"]).ToString("0.0") + "MPa。";

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
                                sItem["KYQD" + i] = Math.Round(1000 * GetSafeDouble(sItem["KYHZ" + i]) / GetSafeDouble(sItem["QMJ" + i]), 1).ToString("0.0");
                                mtmpArray.Add(GetSafeDouble(sItem["KYQD" + i]));
                            }

                            //计算平均值
                            //sItem["KYPJ"] = Math.Round((GetSafeDouble(sItem["KYQD1"]) + GetSafeDouble(sItem["KYQD2"]) + GetSafeDouble(sItem["KYQD3"]) + GetSafeDouble(sItem["KYQD4"]) + GetSafeDouble(sItem["KYQD5"])) / 5, 1).ToString();
                            sItem["KYPJ"] = mtmpArray.Average().ToString();
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
                                sItem["QDPD"] = "不合格";
                            }

                            if (GetSafeDouble(sItem["GMDPJ"]) <= GetSafeDouble(extraFieldsDj["MDDJFW"]))
                            {
                                sItem["GMDPD"] = "合格";  //干密度判定（是否合格）
                            }
                            else
                            {
                                sItem["GMDPD"] = "不合格";
                            }

                            sItem["MDDJFW"] = "密度等级范围≤" + sItem["MDDJFW"] + "kg/m&scsup3&scend。";


                            break;
                        #endregion
                        #region 吸水率
                        case "吸水率":
                            //if (MItem[0]["PDBZ"].ToString().ToUpper().Contains("2011"))
                            //{
                            //    sItem["XSLYQ"] = "≤18";
                            //}
                            //else
                            //{
                            //    sItem["XSLYQ"] = extraFieldsDj["XSL"];
                            //}

                            sItem["HXSW2_1"] = Math.Round((GetSafeDouble(sItem["HXSM2_1"]) - GetSafeDouble(sItem["HXSM_1"])) / GetSafeDouble(sItem["HXSM_1"]) * 100, 2).ToString("0.00");
                            sItem["HXSW2_2"] = Math.Round((GetSafeDouble(sItem["HXSM2_2"]) - GetSafeDouble(sItem["HXSM_2"])) / GetSafeDouble(sItem["HXSM_2"]) * 100, 2).ToString("0.00");
                            sItem["HXSW2_3"] = Math.Round((GetSafeDouble(sItem["HXSM2_3"]) - GetSafeDouble(sItem["HXSM_3"])) / GetSafeDouble(sItem["HXSM_3"]) * 100, 2).ToString("0.00");
                            sItem["HXSW2"] = Math.Round((GetSafeDouble(sItem["HXSW2_1"]) + GetSafeDouble(sItem["HXSW2_2"]) + GetSafeDouble(sItem["HXSW2_3"])) / 3, 1).ToString("0.00");

                            if (IsQualified(sItem["XSLYQ"], sItem["HXSW2"]).Equals("符合"))
                            {
                                sItem["XSLPD"] = "合格";
                            }
                            else
                            {
                                sItem["XSLPD"] = "不合格";
                            }

                            break;
                        #endregion
                        #region 抗冻性
                        case "抗冻性":
                            //sign = true;
                            sItem["KDYQ"] = "质量损失率≤5% \r\n 强度损失率≤25%";
                            sItem["ZLSSYQ"] = "≤5";
                            sItem["QDSSYQ"] = "≤25";

                            #region
                            //for (int i = 1; i < 6; i++)
                            //{
                            //    if (string.IsNullOrEmpty(sItem["KYPJ"]))
                            //    {
                            //        sign = false;
                            //        mbhggs++;
                            //        XQData[index]["JCJG"] = "不合格";
                            //        XQData[index]["JCJGMS"] = "该组试样不符合 抗压平均值 为空。";
                            //        break;
                            //    }
                            //    if (string.IsNullOrEmpty(sItem["DQZL" + i]))
                            //    {
                            //        sign = false;
                            //        mbhggs++;
                            //        XQData[index]["JCJG"] = "不合格";
                            //        XQData[index]["JCJGMS"] = "该组试样不符合 冻前质量" + i+" 为空。";
                            //        break;
                            //    }
                            //    if (string.IsNullOrEmpty(sItem["DHZL" + i]))
                            //    {
                            //        sign = false;
                            //        mbhggs++;
                            //        XQData[index]["JCJG"] = "不合格";
                            //        XQData[index]["JCJGMS"] = "该组试样不符合 冻后质量" + i + " 为空。";
                            //        break;
                            //    }
                            //    if (string.IsNullOrEmpty(sItem["DHCD" + i + "_1"]))
                            //    {
                            //        sign = false;
                            //        mbhggs++;
                            //        XQData[index]["JCJG"] = "不合格";
                            //        XQData[index]["JCJGMS"] = "该组试样不符合 冻后长度" + i + "_1 为空。";
                            //        break;
                            //    }
                            //    if (string.IsNullOrEmpty(sItem["DHCD" + i + "_2"]))
                            //    {
                            //        sign = false;
                            //        mbhggs++;
                            //        XQData[index]["JCJG"] = "不合格";
                            //        XQData[index]["JCJGMS"] = "该组试样不符合 冻后长度" + i + "_2 为空。";
                            //        break;
                            //    }
                            //    if (string.IsNullOrEmpty(sItem["DHKD" + i + "_1"]))
                            //    {
                            //        sign = false;
                            //        mbhggs++;
                            //        XQData[index]["JCJG"] = "不合格";
                            //        XQData[index]["JCJGMS"] = "该组试样不符合 冻后宽度" + i + "_1 为空。";
                            //        break;
                            //    }
                            //    if (string.IsNullOrEmpty(sItem["DHKD" + i + "_2"]))
                            //    {
                            //        sign = false;
                            //        mbhggs++;
                            //        XQData[index]["JCJG"] = "不合格";
                            //        XQData[index]["JCJGMS"] = "该组试样不符合 冻后宽度" + i + "_2 为空。";
                            //        break;
                            //    }
                            //    if (string.IsNullOrEmpty(sItem["DHKYHZ" + i]))
                            //    {
                            //        sign = false;
                            //        mbhggs++;
                            //        XQData[index]["JCJG"] = "不合格";
                            //        XQData[index]["JCJGMS"] = "该组试样不符合 冻后抗压荷重1" + i + " 为空。";
                            //        break;
                            //    }
                            //}
                            #endregion

                            //if (sign)
                            //{
                            sum = 0;
                            for (int i = 1; i < 6; i++)
                            {
                                md1 = GetSafeDouble(sItem["DQZL" + i]);
                                md2 = GetSafeDouble(sItem["DHZL" + i]);
                                md = Math.Round(100 * (md1 - md2) / md1, 1);
                                sum = md + sum;
                            }
                            pjmd = Math.Round(sum / 5, 1);
                            sItem["w_zlss"] = pjmd.ToString("0.0");
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
                            md1 = GetSafeDouble(sItem["kypj"]);
                            md2 = Math.Round(pjmd, 1);
                            md = Math.Round(100 * (md1 - md2) / md1, 0);
                            sItem["W_QDSS"] = md.ToString("0");

                            if (IsQualified(sItem["ZLSSYQ"], sItem["W_ZLSS"]).Equals("符合") && IsQualified(sItem["QDSSYQ"], sItem["W_QDSS"]).Equals("符合"))
                            {
                                sItem["KDPD"] = "合格";
                            }
                            else
                            {
                                sItem["KDPD"] = "不合格";
                            }

                            //MItem["WHICH"] = "bgqk3_1";
                            //}

                            break;
                            #endregion
                    }

                    if (sItem["XSLPD"] == "合格" || sItem["QDPD"] == "合格" || sItem["GMDPD"] == "合格" || sItem["KDPD"] == "合格")
                    {
                        sItem["JCJG"] = "合格";
                        XQData[index]["JCJG"] = "合格";
                        XQData[index]["JCJGMS"] = "该组试样合格。";
                    }
                    else
                    {
                        sItem["JCJG"] = "不合格";
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "该组试样不合格。";
                        mbhggs++;
                    }
                }
            }

            #region 添加最终报告

            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            if (mbhggs > 0)
            {
                //不合格
                bgjgDic.Add("JCJG", "不合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合不标准要求。");
            }
            else
            {
                bgjgDic.Add("JCJG", "合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。");
            }

            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion
            #endregion
        }
    }
}
