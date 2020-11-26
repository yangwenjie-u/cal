using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    /* 路面砖(国标) */
    public class GMZ : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_GMZ_DJ"];
            var extraWLXN = dataExtra["BZ_GMZWLXN"];
            string mlongStr = "";
            double[] mkyqdArray = new double[10];
            double[] mkyhzArray = new double[10];
            double[] mkyhzArray1 = new double[5];
            string[] mtmpArray;
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd;
            double mS, mBzz, mPjz;
            string mSjdjbh, mSjdj;
            double mYqpjz, mDy21, mXdy21;
            int vp;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            string mSjddj, mDjMc;
            bool mFlag_Hg = false, mFlag_Bhg = false;
            mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_GMZS = data["S_GMZ"];
            if (!data.ContainsKey("M_GMZ"))
            {
                data["M_GMZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GMZ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            int mHggs = 0;//统计合格数量
            bool sign = true;
            var jcxmBhg = "";
            var jcxmCur = "";
            foreach (var sItem in S_GMZS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                MItem[0]["G_KYPJZ"] = "----";
                MItem[0]["G_KZPJZ"] = "----";
                MItem[0]["G_KYMIN"] = "----";
                MItem[0]["G_KZMIN"] = "----";
                sItem["XSLYQ"] = "----";
                IDictionary<string, string> extraDJ_item = new Dictionary<string, string>();
                foreach (var extraFieldsDj in extraDJ)
                {
                    extraDJ_item = extraFieldsDj;
                    if (extraFieldsDj["KYDJ"] == sItem["KYDJ"].Trim())
                    {
                        MItem[0]["G_KYPJZ"] = extraFieldsDj["KYPJZ"];
                        MItem[0]["G_KYMIN"] = extraFieldsDj["KYMIN"];
                    }
                    if (extraFieldsDj["KZDJ"] == sItem["KZDJ"].Trim())
                    {
                        MItem[0]["G_KZPJZ"] = extraFieldsDj["KZPJZ"];
                        MItem[0]["G_KZMIN"] = extraFieldsDj["KZMIN"];
                    }
                    if (extraFieldsDj["ZLDJ"] == sItem["ZLDJ"].Trim())
                    {
                        sItem["XSLYQ"] = extraFieldsDj["XSL"];
                    }
                }
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsWlxn = extraWLXN.FirstOrDefault(u => u["MC"].Trim() == sItem["ZLDJ"].Trim());
                if (null != extraFieldsWlxn)
                {
                    sItem["QDSSLYQ"] = extraFieldsWlxn["QDSSL"];
                    sItem["MKCDYQ"] = extraFieldsWlxn["MKCD"];
                    sItem["NMDYQ"] = extraFieldsWlxn["NMD"];
                    sItem["FHXYQ"] = extraFieldsWlxn["XSL"];
                }
                //跳转
                #region 跳转
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    #region 抗压强度
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        jcxmCur = "抗压强度";
                        sign = true;
                        for (int i = 1; i < 11; i++)
                        {
                            sign = IsNumeric(sItem["KYQD" + i]) && !string.IsNullOrEmpty(sItem["KYQD" + i]) ? sign : false;
                            if (!sign)
                            {
                                break;
                            }
                        }
                        if (sign)
                        {
                            sItem["KYQDYQ"] = "抗压强度平均值需" + MItem[0]["G_KYPJZ"] + "MPa，单块最小值需" + MItem[0]["G_KYMIN"] + "MPa。";
                            sign = IsQualified(MItem[0]["G_KYPJZ"], sItem["KYPJ"], false) == "合格" ? sign : false;
                            sign = IsQualified(MItem[0]["G_KYMIN"], sItem["KYQDMIN"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["KYPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["KYPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["KYPJ"] = "----";
                        sItem["KYPD"] = "----";
                        sItem["KYQDMIN"] = "----";
                        sItem["KYQDYQ"] = "----";
                        for (int i = 1; i < 11; i++)
                        {
                            sItem["KYQD" + i] = "----";
                        }
                    }
                    #endregion

                    #region 抗折强度
                    if (jcxm.Contains("、抗折强度、"))
                    {
                        jcxmCur = "抗折强度";
                        sign = true;
                        for (int i = 1; i < 11; i++)
                        {
                            sign = IsNumeric(sItem["KZQD" + i]) && !string.IsNullOrEmpty(sItem["KZQD" + i]) ? sign : false;
                            if (!sign)
                            {
                                break;
                            }
                        }
                        if (sign)
                        {
                            sItem["KYQDYQ"] = "抗折强度平均值需" + MItem[0]["G_KZPJZ"] + "MPa，单块最小值需" + MItem[0]["G_KZMIN"] + "MPa。";
                            sign = IsQualified(MItem[0]["G_KZPJZ"], sItem["KZPJ"], false) == "合格" ? sign : false;
                            sign = IsQualified(MItem[0]["G_KZMIN"], sItem["KZQDMIN"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["KZPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["KZPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["KZPJ"] = "----";
                        sItem["KZPD"] = "----";
                        sItem["KZQDMIN"] = "----";
                        sItem["KZQDYQ"] = "----";
                        for (int i = 1; i < 11; i++)
                        {
                            sItem["KZQD" + i] = "----";
                        }
                    }
                    #endregion

                    #region 抗冻性
                    if (jcxm.Contains("、抗冻性、"))
                    {
                        jcxmCur = "抗冻性";
                        sign = true;
                        sign = IsNumeric(sItem["QDSSL"]) && !string.IsNullOrEmpty(sItem["QDSSL"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["QDSSLPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["QDSSLPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["QDSSL"] = "----";
                        sItem["QDSSLYQ"] = "----";
                        sItem["QDSSLPD"] = "----";
                    }
                    #endregion

                    #region 吸水率
                    if (jcxm.Contains("、吸水率、"))
                    {
                        jcxmCur = "吸水率";
                        sign = true;
                        sign = IsNumeric(sItem["XSL"]) && !string.IsNullOrEmpty(sItem["XSL"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["XSLYQ"], sItem["XSL"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["XSLPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["XSLPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["XSL"] = "----";
                        sItem["XSLYQ"] = "----";
                        sItem["XSLPD"] = "----";
                    }
                    #endregion

                    #region 磨坑长度
                    if (jcxm.Contains("、磨坑长度、"))
                    {
                        jcxmCur = "磨坑长度";
                        sign = true;
                        sign = IsNumeric(sItem["MKCD"]) && !string.IsNullOrEmpty(sItem["MKCD"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["MKCDYQ"], sItem["MKCD"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["MKCDPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["MKCDPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["MKCDYQ"] = "----";
                        sItem["MKCD"] = "----";
                        sItem["MKCDPD"] = "----";
                    }
                    #endregion

                    #region 耐磨度
                    if (jcxm.Contains("、耐磨性、"))
                    {
                        jcxmCur = "耐磨性";
                        sign = true;
                        sign = IsNumeric(sItem["NMD"]) && !string.IsNullOrEmpty(sItem["NMD"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["NMDYQ"], sItem["NMD"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["NMDPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["NMDPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["NMDYQ"] = "----";
                        sItem["NMD"] = "----";
                        sItem["NMDPD"] = "----";
                    }
                    #endregion

                    #region 防滑性能
                    if (jcxm.Contains("、防滑性能、"))
                    {
                        jcxmCur = "防滑性能";
                        sign = true;
                        sign = IsNumeric(sItem["FHX"]) && !string.IsNullOrEmpty(sItem["FHX"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["FHXYQ"], sItem["FHX"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["FHXPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["FHXPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["FHX"] = "----";
                        sItem["FHXYQ"] = "----";
                        sItem["FHXPD"] = "----";
                    }
                    #endregion

                    if (sItem["GGXH"] == "大于4")
                    {
                        sItem["KYDJ"] = "----";
                    }
                    else
                    {
                        sItem["KZDJ"] = "----";
                    }
                }
                #endregion
                #region  非跳转
                else
                {
                    sItem["KYQDYQ"] = "抗压强度平均值需" + string.Format("{0:N1}", MItem[0]["G_KYPJZ"]) + "MPa，单块最小值需" + string.Format("{0:N1}", MItem[0]["G_KYMIN"]) + "MPa。";

                    sItem["KZQDYQ"] = "抗折强度平均值需" + string.Format("{0:N2}", MItem[0]["G_KZPJZ"]) + "MPa，单块最小值需" + string.Format("{0:N1}", MItem[0]["G_KZMIN"]) + "MPa。";
                    //计算单组的抗压强度,并进行合格判断
                    if (sItem["GGXH"] == "小于等于4")   //抗压
                    {
                        #region 抗压强度、抗折强度
                        if (jcxm.Contains("、抗压强度、") || jcxm.Contains("、抗折强度、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "抗压强度,抗折强度");
                            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0, mMj9 = 0, mMj10 = 0;
                            //sItem["MJ1"] = mMj.ToString();
                            mMj1 = GetSafeDouble(sItem["KYCD1"]) * GetSafeDouble(sItem["KYKD1"]);
                            mMj2 = GetSafeDouble(sItem["KYCD2"]) * GetSafeDouble(sItem["KYKD2"]);
                            mMj3 = GetSafeDouble(sItem["KYCD3"]) * GetSafeDouble(sItem["KYKD3"]);
                            mMj4 = GetSafeDouble(sItem["KYCD4"]) * GetSafeDouble(sItem["KYKD4"]);
                            mMj5 = GetSafeDouble(sItem["KYCD5"]) * GetSafeDouble(sItem["KYKD5"]);
                            mMj6 = GetSafeDouble(sItem["KYCD6"]) * GetSafeDouble(sItem["KYKD6"]);
                            mMj7 = GetSafeDouble(sItem["KYCD7"]) * GetSafeDouble(sItem["KYKD7"]);
                            mMj8 = GetSafeDouble(sItem["KYCD8"]) * GetSafeDouble(sItem["KYKD8"]);
                            mMj9 = GetSafeDouble(sItem["KYCD9"]) * GetSafeDouble(sItem["KYKD9"]);
                            mMj10 = GetSafeDouble(sItem["KYCD10"]) * GetSafeDouble(sItem["KYKD10"]);
                            sItem["KYHZ1"] = Round(GetSafeDouble(sItem["KYHZ1"]), 1).ToString("0.0");
                            sItem["KYHZ2"] = GetSafeDouble(sItem["KYHZ2"]).ToString("0.0");
                            sItem["KYHZ3"] = GetSafeDouble(sItem["KYHZ3"]).ToString("0.0");
                            sItem["KYHZ4"] = GetSafeDouble(sItem["KYHZ4"]).ToString("0.0");
                            sItem["KYHZ5"] = GetSafeDouble(sItem["KYHZ5"]).ToString("0.0");
                            sItem["KYHZ6"] = GetSafeDouble(sItem["KYHZ6"]).ToString("0.0");
                            sItem["KYHZ7"] = GetSafeDouble(sItem["KYHZ7"]).ToString("0.0");
                            sItem["KYHZ8"] = GetSafeDouble(sItem["KYHZ8"]).ToString("0.0");
                            sItem["KYHZ9"] = GetSafeDouble(sItem["KYHZ9"]).ToString("0.0");
                            sItem["KYHZ10"] = GetSafeDouble(sItem["KYHZ10"]).ToString("0.0");

                            if (mMj1 != 0)
                                sItem["KYQD1"] = Round(1000 * GetSafeDouble(sItem["KYHZ1"]) / mMj1, 1).ToString("0.0");
                            else
                                sItem["KYQD1"] = "0";
                            if (mMj2 != 0)
                                sItem["KYQD2"] = Round(1000 * GetSafeDouble(sItem["KYHZ2"]) / (mMj2), 1).ToString("0.0");
                            else
                                sItem["KYQD2"] = "0";
                            if (mMj3 != 0)
                                sItem["KYQD3"] = Round(1000 * GetSafeDouble(sItem["KYHZ3"]) / (mMj3), 1).ToString("0.0");
                            else
                                sItem["KYQD3"] = "0";
                            if (mMj4 != 0)
                                sItem["KYQD4"] = Round(1000 * GetSafeDouble(sItem["KYHZ4"]) / (mMj4), 1).ToString("0.0");
                            else
                                sItem["KYQD4"] = "0";
                            if (mMj5 != 0)
                                sItem["KYQD5"] = Round(1000 * GetSafeDouble(sItem["KYHZ5"]) / (mMj5), 1).ToString("0.0");
                            else
                                sItem["KYQD5"] = "0";
                            if (mMj6 != 0)
                                sItem["KYQD6"] = Round(1000 * GetSafeDouble(sItem["KYHZ6"]) / (mMj6), 1).ToString("0.0");
                            else
                                sItem["KYQD6"] = "0";
                            if (mMj7 != 0)
                                sItem["KYQD7"] = Round(1000 * GetSafeDouble(sItem["KYHZ7"]) / (mMj7), 1).ToString("0.0");
                            else
                                sItem["KYQD7"] = "0";
                            if (mMj8 != 0)
                                sItem["KYQD8"] = Round(1000 * GetSafeDouble(sItem["KYHZ8"]) / (mMj8), 1).ToString("0.0");
                            else
                                sItem["KYQD8"] = "0";
                            if (mMj9 != 0)
                                sItem["KYQD9"] = Round(1000 * GetSafeDouble(sItem["KYHZ9"]) / (mMj9), 1).ToString("0.0");
                            else
                                sItem["KYQD9"] = "0";
                            if (mMj10 != 0)
                                sItem["KYQD10"] = Round(1000 * GetSafeDouble(sItem["KYHZ10"]) / (mMj10), 1).ToString("0.0");
                            else
                                sItem["KYQD10"] = "0";

                            //抗压平均值
                            mPjz = (GetSafeDouble(sItem["KYQD1"]) + GetSafeDouble(sItem["KYQD2"]) + GetSafeDouble(sItem["KYQD3"]) + GetSafeDouble(sItem["KYQD4"]) + GetSafeDouble(sItem["KYQD5"]) + GetSafeDouble(sItem["KYQD6"]) + GetSafeDouble(sItem["KYQD7"]) + GetSafeDouble(sItem["KYQD8"]) + GetSafeDouble(sItem["KYQD9"]) + GetSafeDouble(sItem["KYQD10"])) / 10;
                            sItem["KYPJ"] = Round((mPjz), 1).ToString("0.0");
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["KYQD1"] + "," + sItem["KYQD2"] + "," + sItem["KYQD3"] + "," + sItem["KYQD4"] + "," + sItem["KYQD5"] + "," + sItem["KYQD6"] + "," + sItem["KYQD7"] + "," + sItem["KYQD8"] + "," + sItem["KYQD9"] + "," + sItem["KYQD10"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 9; vp++)
                                mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray);
                            mMaxKyqd = mkyhzArray[9];
                            mMinKyqd = Round(mkyhzArray[0], 1);
                            sItem["KYQDMIN"] = mMinKyqd.ToString("0.0");
                            //if (GetSafeDouble(sItem["KYPJ"]) >= GetSafeDouble(extraDJ_item["KYPJZ"]) && GetSafeDouble(sItem["KYQDMIN"]) >= GetSafeDouble(extraDJ_item["KYMIN"]))
                            if (IsQualified(MItem[0]["G_KYPJZ"], sItem["KYPJ"], false) == "合格" && IsQualified(MItem[0]["G_KYMIN"], sItem["KYQDMIN"]) == "合格")
                            {
                                sItem["KYPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["KYPD"] = "不合格";
                                mAllHg = false;
                                mFlag_Bhg = true;
                            }
                            sItem["KZPD"] = "----";
                        }
                        else
                        {
                            sItem["KYPJ"] = "----";
                            sItem["KYPD"] = "----";
                            sItem["KYQDMIN"] = "----";
                            sItem["KYQDYQ"] = "----";
                            for (int i = 1; i < 11; i++)
                            {
                                sItem["KYQD" + i] = "----";
                            }

                        }
                        #endregion

                        #region 抗冻性
                        if (jcxm.Contains("、抗冻性、"))
                        {
                            jcxmCur = "抗冻性";
                            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0;
                            mMj1 = GetSafeDouble(sItem["KDCD1"]) * GetSafeDouble(sItem["KDKD1"]);
                            mMj2 = GetSafeDouble(sItem["KDCD2"]) * GetSafeDouble(sItem["KDKD2"]);
                            mMj3 = GetSafeDouble(sItem["KDCD3"]) * GetSafeDouble(sItem["KDKD3"]);
                            mMj4 = GetSafeDouble(sItem["KDCD4"]) * GetSafeDouble(sItem["KDKD4"]);
                            mMj5 = GetSafeDouble(sItem["KDCD5"]) * GetSafeDouble(sItem["KDKD5"]);

                            sItem["DHKYHZ1"] = Round(GetSafeDouble(sItem["DHKYHZ1"]), 1).ToString("0.0");
                            sItem["DHKYHZ2"] = Round(GetSafeDouble(sItem["DHKYHZ2"]), 1).ToString("0.0");
                            sItem["DHKYHZ3"] = Round(GetSafeDouble(sItem["DHKYHZ3"]), 1).ToString("0.0");
                            sItem["DHKYHZ4"] = Round(GetSafeDouble(sItem["DHKYHZ4"]), 1).ToString("0.0");
                            sItem["DHKYHZ5"] = Round(GetSafeDouble(sItem["DHKYHZ5"]), 1).ToString("0.0");

                            sItem["DQKYHZ1"] = Round(GetSafeDouble(sItem["DQKYHZ1"]), 1).ToString("0.0");
                            sItem["DQKYHZ2"] = Round(GetSafeDouble(sItem["DQKYHZ2"]), 1).ToString("0.0");
                            sItem["DQKYHZ3"] = Round(GetSafeDouble(sItem["DQKYHZ3"]), 1).ToString("0.0");
                            sItem["DQKYHZ4"] = Round(GetSafeDouble(sItem["DQKYHZ4"]), 1).ToString("0.0");
                            sItem["DQKYHZ5"] = Round(GetSafeDouble(sItem["DQKYHZ5"]), 1).ToString("0.0");

                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0)
                            {
                                sItem["DHKYQD1"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ1"]) / (mMj1), 1).ToString("0.0");
                                sItem["DHKYQD2"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ2"]) / (mMj2), 1).ToString("0.0");
                                sItem["DHKYQD3"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ3"]) / (mMj3), 1).ToString("0.0");
                                sItem["DHKYQD4"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ4"]) / (mMj4), 1).ToString("0.0");
                                sItem["DHKYQD5"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ5"]) / (mMj5), 1).ToString("0.0");

                                sItem["DQKYQD1"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ1"]) / (mMj1), 1).ToString("0.0");
                                sItem["DQKYQD2"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ2"]) / (mMj2), 1).ToString("0.0");
                                sItem["DQKYQD3"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ3"]) / (mMj3), 1).ToString("0.0");
                                sItem["DQKYQD4"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ4"]) / (mMj4), 1).ToString("0.0");
                                sItem["DQKYQD5"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ5"]) / (mMj5), 1).ToString("0.0");

                            }
                            else
                            {
                                sItem["DHKYQD1"] = "0";
                                sItem["DHKYQD2"] = "0";
                                sItem["DHKYQD3"] = "0";
                                sItem["DHKYQD4"] = "0";
                                sItem["DHKYQD5"] = "0";

                                sItem["DQKYQD1"] = "0";
                                sItem["DQKYQD2"] = "0";
                                sItem["DQKYQD3"] = "0";
                                sItem["DQKYQD4"] = "0";
                                sItem["DQKYQD5"] = "0";
                            }
                            //冻前抗压平均值 
                            double mDqky = 0;
                            mDqky = (GetSafeDouble(sItem["DQKYQD1"]) + GetSafeDouble(sItem["DQKYQD2"]) + GetSafeDouble(sItem["DQKYQD3"]) + GetSafeDouble(sItem["DQKYQD4"]) + GetSafeDouble(sItem["DQKYQD5"])) / 5;

                            //冻后抗压平均值
                            mPjz = (GetSafeDouble(sItem["DHKYQD1"]) + GetSafeDouble(sItem["DHKYQD2"]) + GetSafeDouble(sItem["DHKYQD3"]) + GetSafeDouble(sItem["DHKYQD4"]) + GetSafeDouble(sItem["DHKYQD5"])) / 5;
                            sItem["DHKYPJ"] = Round((mPjz), 1).ToString("0.0");
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["DHKYQD1"] + "," + sItem["DHKYQD2"] + "," + sItem["DHKYQD3"] + "," + sItem["DHKYQD4"] + "," + sItem["DHKYQD5"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 4; vp++)
                                mkyhzArray1[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray1);
                            mMaxKyqd = mkyhzArray1[4];
                            mMinKyqd = Round(mkyhzArray1[0], 1);
                            if (mDqky > 0)
                            {
                                sItem["QDSSL"] = Round((mDqky - GetSafeDouble(sItem["DHKYPJ"])) / mDqky * 100, 1).ToString("0.0");
                            }
                            else
                            {
                                sItem["QDSSL"] = "0";
                            }


                            if (IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"], false) == "合格")
                            {
                                sItem["QDSSLPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["QDSSLPD"] = "不合格";
                                mFlag_Bhg = true;
                            }
                        }
                        else
                        {
                            sItem["QDSSL"] = "----";
                            sItem["QDSSLYQ"] = "----";
                            sItem["QDSSLPD"] = "----";
                        }
                        #endregion
                    }
                    else //抗折
                    {
                        #region 抗压强度、抗折强度
                        if (jcxm.Contains("、抗压强度、") || jcxm.Contains("、抗折强度、"))
                        {
                            jcxmCur = CurrentJcxm(jcxm, "抗压强度,抗折强度");
                            sItem["KZHZ1"] = Round(GetSafeDouble(sItem["KZHZ1"]), 2).ToString("0.00");
                            sItem["KZHZ2"] = Round(GetSafeDouble(sItem["KZHZ2"]), 2).ToString("0.00");
                            sItem["KZHZ3"] = Round(GetSafeDouble(sItem["KZHZ3"]), 2).ToString("0.00");
                            sItem["KZHZ4"] = Round(GetSafeDouble(sItem["KZHZ4"]), 2).ToString("0.00");
                            sItem["KZHZ5"] = Round(GetSafeDouble(sItem["KZHZ5"]), 2).ToString("0.00");
                            sItem["KZHZ6"] = Round(GetSafeDouble(sItem["KZHZ6"]), 2).ToString("0.00");
                            sItem["KZHZ7"] = Round(GetSafeDouble(sItem["KZHZ7"]), 2).ToString("0.00");
                            sItem["KZHZ8"] = Round(GetSafeDouble(sItem["KZHZ8"]), 2).ToString("0.00");
                            sItem["KZHZ9"] = Round(GetSafeDouble(sItem["KZHZ9"]), 2).ToString("0.00");
                            sItem["KZHZ10"] = Round(GetSafeDouble(sItem["KZHZ10"]), 2).ToString("0.00");

                            sItem["KZQD1"] = Round((3000 * GetSafeDouble(sItem["KZHZ1"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD1"]) * GetSafeDouble(sItem["HD1"]) * GetSafeDouble(sItem["HD1"])), 2).ToString("0.00");
                            sItem["KZQD2"] = Round((3000 * GetSafeDouble(sItem["KZHZ2"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD2"]) * GetSafeDouble(sItem["HD2"]) * GetSafeDouble(sItem["HD2"])), 2).ToString("0.00");
                            sItem["KZQD3"] = Round((3000 * GetSafeDouble(sItem["KZHZ3"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD3"]) * GetSafeDouble(sItem["HD3"]) * GetSafeDouble(sItem["HD3"])), 2).ToString("0.00");
                            sItem["KZQD4"] = Round((3000 * GetSafeDouble(sItem["KZHZ4"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD4"]) * GetSafeDouble(sItem["HD4"]) * GetSafeDouble(sItem["HD4"])), 2).ToString("0.00");
                            sItem["KZQD5"] = Round((3000 * GetSafeDouble(sItem["KZHZ5"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD5"]) * GetSafeDouble(sItem["HD5"]) * GetSafeDouble(sItem["HD5"])), 2).ToString("0.00");
                            sItem["KZQD6"] = Round((3000 * GetSafeDouble(sItem["KZHZ6"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD6"]) * GetSafeDouble(sItem["HD6"]) * GetSafeDouble(sItem["HD6"])), 2).ToString("0.00");
                            sItem["KZQD7"] = Round((3000 * GetSafeDouble(sItem["KZHZ7"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD7"]) * GetSafeDouble(sItem["HD7"]) * GetSafeDouble(sItem["HD7"])), 2).ToString("0.00");
                            sItem["KZQD8"] = Round((3000 * GetSafeDouble(sItem["KZHZ8"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD8"]) * GetSafeDouble(sItem["HD8"]) * GetSafeDouble(sItem["HD8"])), 2).ToString("0.00");
                            sItem["KZQD9"] = Round((3000 * GetSafeDouble(sItem["KZHZ9"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD9"]) * GetSafeDouble(sItem["HD9"]) * GetSafeDouble(sItem["HD9"])), 2).ToString("0.00");
                            sItem["KZQD10"] = Round((3000 * GetSafeDouble(sItem["KZHZ10"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD10"]) * GetSafeDouble(sItem["HD10"]) * GetSafeDouble(sItem["HD10"])), 2).ToString("0.00");

                            //抗压平均值
                            mPjz = (GetSafeDouble(sItem["KZQD1"]) + GetSafeDouble(sItem["KZQD2"]) + GetSafeDouble(sItem["KZQD3"]) + GetSafeDouble(sItem["KZQD4"]) + GetSafeDouble(sItem["KZQD5"]) + GetSafeDouble(sItem["KZQD6"]) + GetSafeDouble(sItem["KZQD7"]) + GetSafeDouble(sItem["KZQD8"]) + GetSafeDouble(sItem["KZQD9"]) + GetSafeDouble(sItem["KZQD10"])) / 10;
                            sItem["KZPJ"] = Round((mPjz), 2).ToString("0.00");
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["KZQD1"] + "," + sItem["KZQD2"] + "," + sItem["KZQD3"] + "," + sItem["KZQD4"] + "," + sItem["KZQD5"] + "," + sItem["KZQD6"] + "," + sItem["KZQD7"] + "," + sItem["KZQD8"] + "," + sItem["KZQD9"] + "," + sItem["KZQD10"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 9; vp++)
                                mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray);
                            mMaxKyqd = mkyhzArray[9];
                            mMinKyqd = Round(mkyhzArray[0], 2);
                            sItem["KZQDMIN"] = mMinKyqd.ToString("0.00");
                            //if (GetSafeDouble(sItem["KZPJ"]) >= GetSafeDouble(extraDJ_item["KZPJZ"]) && GetSafeDouble(sItem["KZQDMIN"]) >= GetSafeDouble(extraDJ_item["KZMIN"]))
                            if (IsQualified(MItem[0]["G_KZPJZ"], sItem["KZPJ"], false) == "合格" && IsQualified(MItem[0]["G_KZMIN"], sItem["KZQDMIN"]) == "合格")
                            {
                                sItem["JCJG"] = "合格";
                                sItem["KZPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["JCJG"] = "不合格";
                                sItem["KZPD"] = "不合格";
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                            sItem["KYPD"] = "----";
                        }
                        else
                        {
                            sItem["KZPJ"] = "----";
                            sItem["KZPD"] = "----";
                            sItem["KZQDMIN"] = "----";
                            sItem["KZQDYQ"] = "----";
                            for (int i = 1; i < 11; i++)
                            {
                                sItem["KZQD" + i] = "----";
                            }

                        }
                        #endregion

                        #region 抗冻性
                        if (jcxm.Contains("、抗冻性、"))
                        {
                            jcxmCur = "抗冻性";
                            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0;
                            mMj1 = GetSafeDouble(sItem["KDCD1"]) * GetSafeDouble(sItem["KDKD1"]);
                            mMj2 = GetSafeDouble(sItem["KDCD2"]) * GetSafeDouble(sItem["KDKD2"]);
                            mMj3 = GetSafeDouble(sItem["KDCD3"]) * GetSafeDouble(sItem["KDKD3"]);
                            mMj4 = GetSafeDouble(sItem["KDCD4"]) * GetSafeDouble(sItem["KDKD4"]);
                            mMj5 = GetSafeDouble(sItem["KDCD5"]) * GetSafeDouble(sItem["KDKD5"]);

                            sItem["DHKYHZ1"] = Round(GetSafeDouble(sItem["DHKYHZ1"]), 1).ToString("0.0");
                            sItem["DHKYHZ2"] = Round(GetSafeDouble(sItem["DHKYHZ2"]), 1).ToString("0.0");
                            sItem["DHKYHZ3"] = Round(GetSafeDouble(sItem["DHKYHZ3"]), 1).ToString("0.0");
                            sItem["DHKYHZ4"] = Round(GetSafeDouble(sItem["DHKYHZ4"]), 1).ToString("0.0");
                            sItem["DHKYHZ5"] = Round(GetSafeDouble(sItem["DHKYHZ5"]), 1).ToString("0.0");

                            sItem["DQKYHZ1"] = Round(GetSafeDouble(sItem["DQKYHZ1"]), 1).ToString("0.0");
                            sItem["DQKYHZ2"] = Round(GetSafeDouble(sItem["DQKYHZ2"]), 1).ToString("0.0");
                            sItem["DQKYHZ3"] = Round(GetSafeDouble(sItem["DQKYHZ3"]), 1).ToString("0.0");
                            sItem["DQKYHZ4"] = Round(GetSafeDouble(sItem["DQKYHZ4"]), 1).ToString("0.0");
                            sItem["DQKYHZ5"] = Round(GetSafeDouble(sItem["DQKYHZ5"]), 1).ToString("0.0");

                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0)
                            {
                                sItem["DHKYQD1"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ1"]) / (mMj1), 1).ToString("0.0");
                                sItem["DHKYQD2"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ2"]) / (mMj2), 1).ToString("0.0");
                                sItem["DHKYQD3"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ3"]) / (mMj3), 1).ToString("0.0");
                                sItem["DHKYQD4"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ4"]) / (mMj4), 1).ToString("0.0");
                                sItem["DHKYQD5"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ5"]) / (mMj5), 1).ToString("0.0");

                                sItem["DQKYQD1"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ1"]) / (mMj1), 1).ToString("0.0");
                                sItem["DQKYQD2"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ2"]) / (mMj2), 1).ToString("0.0");
                                sItem["DQKYQD3"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ3"]) / (mMj3), 1).ToString("0.0");
                                sItem["DQKYQD4"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ4"]) / (mMj4), 1).ToString("0.0");
                                sItem["DQKYQD5"] = Round(1000 * GetSafeDouble(sItem["DQKYHZ5"]) / (mMj5), 1).ToString("0.0");
                            }
                            else
                            {
                                sItem["DHKYQD1"] = "0";
                                sItem["DHKYQD2"] = "0";
                                sItem["DHKYQD3"] = "0";
                                sItem["DHKYQD4"] = "0";
                                sItem["DHKYQD5"] = "0";

                                sItem["DQKYQD1"] = "0";
                                sItem["DQKYQD2"] = "0";
                                sItem["DQKYQD3"] = "0";
                                sItem["DQKYQD4"] = "0";
                                sItem["DQKYQD5"] = "0";
                            }
                            //冻前抗压平均值 
                            double mDqky = 0;
                            mDqky = (GetSafeDouble(sItem["DQKYQD1"]) + GetSafeDouble(sItem["DQKYQD2"]) + GetSafeDouble(sItem["DQKYQD3"]) + GetSafeDouble(sItem["DQKYQD4"]) + GetSafeDouble(sItem["DQKYQD5"])) / 5;

                            //冻后抗压平均值
                            mPjz = (GetSafeDouble(sItem["DHKYQD1"]) + GetSafeDouble(sItem["DHKYQD2"]) + GetSafeDouble(sItem["DHKYQD3"]) + GetSafeDouble(sItem["DHKYQD4"]) + GetSafeDouble(sItem["DHKYQD5"])) / 5;
                            sItem["DHKYPJ"] = Round((mPjz), 1).ToString("0.0");
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["DHKYQD1"] + "," + sItem["DHKYQD2"] + "," + sItem["DHKYQD3"] + "," + sItem["DHKYQD4"] + "," + sItem["DHKYQD5"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 4; vp++)
                                mkyhzArray1[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray1);
                            mMaxKyqd = mkyhzArray1[4];
                            mMinKyqd = Round(mkyhzArray1[0], 1);
                            //sItem["QDSSL"] = Round((GetSafeDouble(sItem["KZPJ"]) - GetSafeDouble(sItem["DHKYPJ"])) / GetSafeDouble(sItem["KZPJ"]) * 100, 1).ToString();
                            if (mDqky > 0)
                            {
                                sItem["QDSSL"] = Round((mDqky - GetSafeDouble(sItem["DHKYPJ"])) / mDqky * 100, 1).ToString("0.0");
                            }
                            else
                            {
                                sItem["QDSSL"] = "0";
                            }

                            if (IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"], false) == "合格")
                            {
                                sItem["QDSSLPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["QDSSLPD"] = "不合格";
                                mFlag_Bhg = true;
                            }
                        }
                        else
                        {
                            sItem["QDSSL"] = "----";
                            sItem["QDSSLYQ"] = "----";
                            sItem["QDSSLPD"] = "----";
                        }

                        #endregion
                        #region 抗冻性-old
                        //if (jcxm.Contains("、抗冻性、"))
                        //{
                        //    sign = true;
                        //    sign = IsNumeric(sItem["QDSSL"]) && !string.IsNullOrEmpty(sItem["QDSSL"]) ? sign : false;
                        //    if (sign)
                        //    {
                        //        sign = IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"], false) == "合格" ? sign : false;
                        //        if (sign)
                        //        {
                        //            sItem["QDSSLPD"] = "合格";
                        //            mFlag_Hg = true;
                        //        }
                        //        else
                        //        {
                        //            sItem["QDSSLPD"] = "不合格";
                        //            mFlag_Bhg = true;
                        //        }
                        //    }
                        //    //    sItem["DHKZHZ1"] = Round(GetSafeDouble(sItem["DHKZHZ1"]), 2).ToString();
                        //    //    sItem["DHKZHZ2"] = Round(GetSafeDouble(sItem["DHKZHZ2"]), 2).ToString();
                        //    //    sItem["DHKZHZ3"] = Round(GetSafeDouble(sItem["DHKZHZ3"]), 2).ToString();
                        //    //    sItem["DHKZHZ4"] = Round(GetSafeDouble(sItem["DHKZHZ4"]), 2).ToString();
                        //    //    sItem["DHKZHZ5"] = Round(GetSafeDouble(sItem["DHKZHZ5"]), 2).ToString();
                        //    //    sItem["DHKZQD1"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ1"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD1"]) * GetSafeDouble(sItem["DHHD1"]) * GetSafeDouble(sItem["DHHD1"])), 2).ToString();
                        //    //    sItem["DHKZQD2"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ2"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD2"]) * GetSafeDouble(sItem["DHHD2"]) * GetSafeDouble(sItem["DHHD2"])), 2).ToString();
                        //    //    sItem["DHKZQD3"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ3"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD3"]) * GetSafeDouble(sItem["DHHD3"]) * GetSafeDouble(sItem["DHHD3"])), 2).ToString();
                        //    //    sItem["DHKZQD4"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ4"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD4"]) * GetSafeDouble(sItem["DHHD4"]) * GetSafeDouble(sItem["DHHD4"])), 2).ToString();
                        //    //    sItem["DHKZQD5"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ5"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD5"]) * GetSafeDouble(sItem["DHHD5"]) * GetSafeDouble(sItem["DHHD5"])), 2).ToString();
                        //    //    //抗压平均值
                        //    //    mPjz = (GetSafeDouble(sItem["DHKZQD1"]) + GetSafeDouble(sItem["DHKZQD2"]) + GetSafeDouble(sItem["DHKZQD3"]) + GetSafeDouble(sItem["DHKZQD4"]) + GetSafeDouble(sItem["DHKZQD5"])) / 5;
                        //    //    sItem["DHKZPJ"] = Round((mPjz), 2).ToString();
                        //    //    //标准值计算、判定，平均值判定，单组合格判定
                        //    //    mlongStr = sItem["DHKZQD1"] + "," + sItem["DHKZQD2"] + "," + sItem["DHKZQD3"] + "," + sItem["DHKZQD4"] + "," + sItem["DHKZQD5"];
                        //    //    mtmpArray = mlongStr.Split(',');
                        //    //    for (vp = 0; vp <= 4; vp++)
                        //    //        mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        //    //    Array.Sort(mkyhzArray);
                        //    //    mMaxKyqd = mkyhzArray[4];
                        //    //    mMinKyqd = Round(mkyhzArray[0], 2);
                        //    //    sItem["DHKZQDMIN"] = mMinKyqd.ToString();
                        //    //    sItem["QDSSL"] = Round((GetSafeDouble(sItem["KZPJ"]) - GetSafeDouble(sItem["DHKZPJ"])) / GetSafeDouble(sItem["KZPJ"]) * 100, 1).ToString("0.0");
                        //    //    sItem["QDSSLYQ"] = extraDJ_item["QDSSL"];
                        //    //    if (IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"]) == "符合")
                        //    //    {
                        //    //        sItem["QDSSLPD"] = "合格";
                        //    //        mFlag_Hg = true;
                        //    //    }
                        //    //    else
                        //    //    {
                        //    //        sItem["QDSSLPD"] = "不合格";
                        //    //        mFlag_Bhg = true;
                        //    //    }
                        //}
                        //else
                        //    sItem["QDSSLPD"] = "----";
                        #endregion
                    }

                    #region 磨坑长度
                    if (jcxm.Contains("、磨坑长度、"))
                    {
                        jcxmCur = "磨坑长度";
                        if (IsQualified(sItem["MKCDYQ"], sItem["MKCD"]) == "符合")
                        {
                            sItem["MKCDPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["MKCDPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["MKCDYQ"] = "----";
                        sItem["MKCD"] = "----";
                        sItem["MKCDPD"] = "----";
                    }
                    #endregion

                    #region 耐磨性
                    if (jcxm.Contains("、耐磨性、"))
                    {
                        jcxmCur = "耐磨性";
                        if (IsQualified(sItem["NMDYQ"], sItem["NMD"]) == "符合")
                        {
                            sItem["NMDPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["NMDPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["NMDYQ"] = "----";
                        sItem["NMD"] = "----";
                        sItem["NMDPD"] = "----";
                    }
                    #endregion

                    #region 吸水率
                    if (jcxm.Contains("、吸水率、"))
                    {
                        jcxmCur = "吸水率";
                        sItem["XSL1"] = Round((Conversion.Val(sItem["XSLM1_1"]) - Conversion.Val(sItem["XSLM0_1"])) / (Conversion.Val(sItem["XSLM0_1"])) * 100, 2).ToString("0.00");
                        sItem["XSL2"] = Round((Conversion.Val(sItem["XSLM1_2"]) - Conversion.Val(sItem["XSLM0_2"])) / (Conversion.Val(sItem["XSLM0_2"])) * 100, 2).ToString("0.00");
                        sItem["XSL3"] = Round((Conversion.Val(sItem["XSLM1_3"]) - Conversion.Val(sItem["XSLM0_3"])) / (Conversion.Val(sItem["XSLM0_3"])) * 100, 2).ToString("0.00");
                        sItem["XSL4"] = Round((Conversion.Val(sItem["XSLM1_4"]) - Conversion.Val(sItem["XSLM0_4"])) / (Conversion.Val(sItem["XSLM0_4"])) * 100, 2).ToString("0.00");
                        sItem["XSL5"] = Round((Conversion.Val(sItem["XSLM1_5"]) - Conversion.Val(sItem["XSLM0_5"])) / (Conversion.Val(sItem["XSLM0_5"])) * 100, 2).ToString("0.00");
                        sItem["XSL"] = Round(((Conversion.Val(sItem["XSL1"])) + (Conversion.Val(sItem["XSL2"])) + (Conversion.Val(sItem["XSL3"])) + (Conversion.Val(sItem["XSL4"])) + (Conversion.Val(sItem["XSL5"]))) / 5, 1).ToString("0.0");


                        if (IsQualified(sItem["XSLYQ"], sItem["XSL"]) == "合格")
                        {
                            sItem["XSLPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["XSLPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["XSL"] = "----";
                        sItem["XSLYQ"] = "----";
                        sItem["XSLPD"] = "----";
                    }
                    #endregion

                    #region 防滑性能
                    if (jcxm.Contains("、防滑性能、"))
                    {
                        jcxmCur = "防滑性能";
                        sign = true;
                        sign = IsNumeric(sItem["FHX"]) && !string.IsNullOrEmpty(sItem["FHX"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["FHXYQ"], sItem["FHX"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["FHXPD"] = "合格";
                                mAllHg = true;
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["FHXPD"] = "不合格";
                                mAllHg = false;
                                mFlag_Bhg = true;
                            }
                        }
                    }
                    else
                    {
                        sItem["FHX"] = "----";
                        sItem["FHXYQ"] = "----";
                        sItem["FHXPD"] = "----";
                    }
                    #endregion

                    if (sItem["QDSSLPD"] == "不合格" || sItem["KYPD"] == "不合格" || sItem["KZPD"] == "不合格" || sItem["XSLPD"] == "不合格" || sItem["NMDPD"] == "不合格" || sItem["MKCDPD"] == "不合格")
                    {
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        sItem["JCJG"] = "合格";
                        mAllHg = true;
                        mFlag_Hg = true;
                    }
                }
                #endregion

                ////单组判断
                //if (itemHG)
                //{
                //    sItem["JCJG"] = "合格";
                //}
                //else
                //{
                //    sItem["JCJG"] = "不合格";
                //}
            }

            //添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                mjcjg = "不合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //if (mFlag_Bhg && mFlag_Hg)
                //    jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }

        public void GxJCJGMS()
        {
            //富阳德浩
            #region
            var extraDJ = dataExtra["BZ_GMZ_DJ"];

            var data = retData;
            var jsbeizhu = "该组试样的检测结果全部合格";
            var SItems = data["S_GMZ"];
            var MItem = data["M_GMZ"];

            var mAllHg = true;
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            string sjdj = "";

            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
              
                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    sjdj = sItem["KYDJ"];
                    jcxmCur = "抗压强度";
                    if (sItem["KYPD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                #endregion

                #region 抗折强度
                if (jcxm.Contains("、抗折强度、"))
                {
                    sjdj = sItem["KZDJ"];
                    jcxmCur = "抗折强度";
                    if (sItem["KZPD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                #endregion
             
            }
            if (MItem[0]["JCJG"] == "合格")
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合" + sjdj + "强度等级要求。";
            }
            else
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + sjdj + "强度等级要求。";
            }
            #endregion
        }
    }
}
