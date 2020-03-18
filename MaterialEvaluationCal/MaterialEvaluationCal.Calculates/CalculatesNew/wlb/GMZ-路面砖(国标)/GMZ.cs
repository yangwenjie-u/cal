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
            double mMj1, mMj2, mMj3, mMj4, mMj5, mMj6, mMj7, mMj8, mMj9, mMj10;
            string mlongStr = "";
            double[] mkyqdArray = new double[5];
            double[] mkyhzArray = new double[5];
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
                    if (GetSafeDouble(sItem["CHB"]) < 5)
                    {
                        #region 抗压强度、抗折强度
                        if (jcxm.Contains("、抗压强度、") || jcxm.Contains("、抗折强度、"))
                        {
                            double mMj = Round(GetSafeDouble(sItem["DBCD"]) * GetSafeDouble(sItem["DBKD"]), 1);
                            sItem["MJ1"] = mMj.ToString();
                            sItem["KYHZ1"] = Round(GetSafeDouble(sItem["KYHZ1"]), 1).ToString();
                            sItem["KYHZ2"] = GetSafeDouble(sItem["KYHZ2"]).ToString("0.0");
                            sItem["KYHZ3"] = GetSafeDouble(sItem["KYHZ3"]).ToString("0.0");
                            sItem["KYHZ4"] = GetSafeDouble(sItem["KYHZ4"]).ToString("0.0");
                            sItem["KYHZ5"] = GetSafeDouble(sItem["KYHZ5"]).ToString("0.0");
                            if (mMj != 0)
                                sItem["KYQD1"] = Round(1000 * GetSafeDouble(sItem["KYHZ1"]) / mMj, 1).ToString();
                            else
                                sItem["KYQD1"] = "0";
                            if (mMj != 0)
                                sItem["KYQD2"] = Round(1000 * GetSafeDouble(sItem["KYHZ2"]) / (mMj), 1).ToString();
                            else
                                sItem["KYQD2"] = "0";
                            if (mMj != 0)
                                sItem["KYQD3"] = Round(1000 * GetSafeDouble(sItem["KYHZ3"]) / (mMj), 1).ToString();
                            else
                                sItem["KYQD3"] = "0";
                            if (mMj != 0)
                                sItem["KYQD4"] = Round(1000 * GetSafeDouble(sItem["KYHZ4"]) / (mMj), 1).ToString();
                            else
                                sItem["KYQD4"] = "0";
                            if (mMj != 0)
                                sItem["KYQD5"] = Round(1000 * GetSafeDouble(sItem["KYHZ5"]) / (mMj), 1).ToString();
                            else
                                sItem["KYQD5"] = "0";
                            //抗压平均值
                            mPjz = (GetSafeDouble(sItem["KYQD1"]) + GetSafeDouble(sItem["KYQD2"]) + GetSafeDouble(sItem["KYQD3"]) + GetSafeDouble(sItem["KYQD4"]) + GetSafeDouble(sItem["KYQD5"])) / 5;
                            sItem["KYPJ"] = Round((mPjz), 1).ToString();
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["KYQD1"] + "," + sItem["KYQD2"] + "," + sItem["KYQD3"] + "," + sItem["KYQD4"] + "," + sItem["KYQD5"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 4; vp++)
                                mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray);
                            mMaxKyqd = mkyhzArray[4];
                            mMinKyqd = Round(mkyhzArray[0], 1);
                            sItem["KYQDMIN"] = mMinKyqd.ToString();
                            if (GetSafeDouble(sItem["KYPJ"]) >= GetSafeDouble(extraDJ_item["KYPJZ"]) && GetSafeDouble(sItem["KYQDMIN"]) >= GetSafeDouble(extraDJ_item["KYMIN"]))
                            {
                                sItem["KYPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["KYPD"] = "不合格";
                                mAllHg = false;
                                mFlag_Bhg = true;
                            }
                            sItem["KZPD"] = "----";
                        }
                        else
                            sItem["KYPD"] = "----";
                        #endregion

                        #region 抗冻性
                        if (jcxm.Contains("、抗冻性、"))
                        {
                            double mMj = Round(GetSafeDouble(sItem["DBCD"]) * GetSafeDouble(sItem["DBKD"]), 1);
                            sItem["MJ1"] = mMj.ToString();
                            sItem["DHKYHZ1"] = Round(GetSafeDouble(sItem["DHKYHZ1"]), 1).ToString();
                            sItem["DHKYHZ2"] = Round(GetSafeDouble(sItem["DHKYHZ2"]), 1).ToString();
                            sItem["DHKYHZ3"] = Round(GetSafeDouble(sItem["DHKYHZ3"]), 1).ToString();
                            sItem["DHKYHZ4"] = Round(GetSafeDouble(sItem["DHKYHZ4"]), 1).ToString();
                            sItem["DHKYHZ5"] = Round(GetSafeDouble(sItem["DHKYHZ5"]), 1).ToString();
                            if (mMj != 0)
                            {
                                sItem["DHKYQD1"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ1"]) / (mMj), 1).ToString();
                                sItem["DHKYQD2"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ2"]) / (mMj), 1).ToString();
                                sItem["DHKYQD3"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ3"]) / (mMj), 1).ToString();
                                sItem["DHKYQD4"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ4"]) / (mMj), 1).ToString();
                                sItem["DHKYQD5"] = Round(1000 * GetSafeDouble(sItem["DHKYHZ5"]) / (mMj), 1).ToString();
                            }
                            else
                            {
                                sItem["DHKYQD1"] = "0";
                                sItem["DHKYQD2"] = "0";
                                sItem["DHKYQD3"] = "0";
                                sItem["DHKYQD4"] = "0";
                                sItem["DHKYQD5"] = "0";
                            }
                            //抗压平均值
                            mPjz = (GetSafeDouble(sItem["DHKYQD1"]) + GetSafeDouble(sItem["DHKYQD2"]) + GetSafeDouble(sItem["DHKYQD3"]) + GetSafeDouble(sItem["DHKYQD4"]) + GetSafeDouble(sItem["DHKYQD5"])) / 5;
                            sItem["DHKYPJ"] = Round((mPjz), 1).ToString();
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["DHKYQD1"] + "," + sItem["DHKYQD2"] + "," + sItem["DHKYQD3"] + "," + sItem["DHKYQD4"] + "," + sItem["DHKYQD5"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 4; vp++)
                                mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray);
                            mMaxKyqd = mkyhzArray[4];
                            mMinKyqd = Round(mkyhzArray[0], 1);
                            sItem["KYQDMIN"] = mMinKyqd.ToString();
                            sItem["QDSSL"] = Round((GetSafeDouble(sItem["KYPJ"]) - GetSafeDouble(sItem["DHKYPJ"])) / GetSafeDouble(sItem["KYPJ"]) * 100, 1).ToString();

                            if (IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"]) == "符合")
                            {
                                sItem["QDSSLPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["QDSSLPD"] = "不合格";
                                mFlag_Bhg = true;
                            }
                        }

                        else
                            sItem["QDSSLPD"] = "----";
                        #endregion
                    }
                    else
                    {
                        #region 抗压强度、抗折强度
                        if (jcxm.Contains("、抗压强度、") || jcxm.Contains("、抗折强度、"))
                        {
                            sItem["KZHZ1"] = Round(GetSafeDouble(sItem["KZHZ1"]), 2).ToString();
                            sItem["KZHZ2"] = Round(GetSafeDouble(sItem["KZHZ2"]), 2).ToString();
                            sItem["KZHZ3"] = Round(GetSafeDouble(sItem["KZHZ3"]), 2).ToString();
                            sItem["KZHZ4"] = Round(GetSafeDouble(sItem["KZHZ4"]), 2).ToString();
                            sItem["KZHZ5"] = Round(GetSafeDouble(sItem["KZHZ5"]), 2).ToString();
                            sItem["KZQD1"] = Round((3000 * GetSafeDouble(sItem["KZHZ1"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD1"]) * GetSafeDouble(sItem["HD1"]) * GetSafeDouble(sItem["HD1"])), 2).ToString();
                            sItem["KZQD2"] = Round((3000 * GetSafeDouble(sItem["KZHZ2"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD2"]) * GetSafeDouble(sItem["HD2"]) * GetSafeDouble(sItem["HD2"])), 2).ToString();
                            sItem["KZQD3"] = Round((3000 * GetSafeDouble(sItem["KZHZ3"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD3"]) * GetSafeDouble(sItem["HD3"]) * GetSafeDouble(sItem["HD3"])), 2).ToString();
                            sItem["KZQD4"] = Round((3000 * GetSafeDouble(sItem["KZHZ4"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD4"]) * GetSafeDouble(sItem["HD4"]) * GetSafeDouble(sItem["HD4"])), 2).ToString();
                            sItem["KZQD5"] = Round((3000 * GetSafeDouble(sItem["KZHZ5"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["KD5"]) * GetSafeDouble(sItem["HD5"]) * GetSafeDouble(sItem["HD5"])), 2).ToString();
                            //抗压平均值
                            mPjz = (GetSafeDouble(sItem["KZQD1"]) + GetSafeDouble(sItem["KZQD2"]) + GetSafeDouble(sItem["KZQD3"]) + GetSafeDouble(sItem["KZQD4"]) + GetSafeDouble(sItem["KZQD5"])) / 5;
                            sItem["KZPJ"] = Round((mPjz), 2).ToString();
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["KZQD1"] + "," + sItem["KZQD2"] + "," + sItem["KZQD3"] + "," + sItem["KZQD4"] + "," + sItem["KZQD5"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 4; vp++)
                                mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray);
                            mMaxKyqd = mkyhzArray[4];
                            mMinKyqd = Round(mkyhzArray[0], 2);
                            sItem["KZQDMIN"] = mMinKyqd.ToString();
                            if (GetSafeDouble(sItem["KZPJ"]) >= GetSafeDouble(extraDJ_item["KZPJZ"]) && GetSafeDouble(sItem["KZQDMIN"]) >= GetSafeDouble(extraDJ_item["KZMIN"]))
                            {
                                sItem["JCJG"] = "合格";
                                sItem["KZPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["JCJG"] = "不合格";
                                sItem["KZPD"] = "不合格";
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                            sItem["KYPD"] = "----";
                        }
                        else
                            sItem["KZPD"] = "----";
                        #endregion

                        #region 抗冻性
                        if (jcxm.Contains("、抗冻性、"))
                        {
                            sign = true;
                            sign = IsNumeric(sItem["QDSSL"]) && !string.IsNullOrEmpty(sItem["QDSSL"]) ? sign : false;
                            if (sign)
                            {
                                sign = IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"], false) == "合格" ? sign : false;
                                if (sign)
                                {
                                    sItem["QDSSLPD"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    sItem["QDSSLPD"] = "不合格";
                                    mFlag_Bhg = true;
                                }
                            }
                            //sItem["DHKZHZ1"] = Round(GetSafeDouble(sItem["DHKZHZ1"]), 2).ToString();
                            //sItem["DHKZHZ2"] = Round(GetSafeDouble(sItem["DHKZHZ2"]), 2).ToString();
                            //sItem["DHKZHZ3"] = Round(GetSafeDouble(sItem["DHKZHZ3"]), 2).ToString();
                            //sItem["DHKZHZ4"] = Round(GetSafeDouble(sItem["DHKZHZ4"]), 2).ToString();
                            //sItem["DHKZHZ5"] = Round(GetSafeDouble(sItem["DHKZHZ5"]), 2).ToString();
                            //sItem["DHKZQD1"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ1"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD1"]) * GetSafeDouble(sItem["DHHD1"]) * GetSafeDouble(sItem["DHHD1"])), 2).ToString();
                            //sItem["DHKZQD2"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ2"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD2"]) * GetSafeDouble(sItem["DHHD2"]) * GetSafeDouble(sItem["DHHD2"])), 2).ToString();
                            //sItem["DHKZQD3"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ3"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD3"]) * GetSafeDouble(sItem["DHHD3"]) * GetSafeDouble(sItem["DHHD3"])), 2).ToString();
                            //sItem["DHKZQD4"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ4"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD4"]) * GetSafeDouble(sItem["DHHD4"]) * GetSafeDouble(sItem["DHHD4"])), 2).ToString();
                            //sItem["DHKZQD5"] = Round((3000 * GetSafeDouble(sItem["DHKZHZ5"]) * GetSafeDouble(sItem["ZZJL"])) / (2 * GetSafeDouble(sItem["DHKD5"]) * GetSafeDouble(sItem["DHHD5"]) * GetSafeDouble(sItem["DHHD5"])), 2).ToString();
                            ////抗压平均值
                            //mPjz = (GetSafeDouble(sItem["DHKZQD1"]) + GetSafeDouble(sItem["DHKZQD2"]) + GetSafeDouble(sItem["DHKZQD3"]) + GetSafeDouble(sItem["DHKZQD4"]) + GetSafeDouble(sItem["DHKZQD5"])) / 5;
                            //sItem["DHKZPJ"] = Round((mPjz), 2).ToString();
                            ////标准值计算、判定，平均值判定，单组合格判定
                            //mlongStr = sItem["DHKZQD1"] + "," + sItem["DHKZQD2"] + "," + sItem["DHKZQD3"] + "," + sItem["DHKZQD4"] + "," + sItem["DHKZQD5"];
                            //mtmpArray = mlongStr.Split(',');
                            //for (vp = 0; vp <= 4; vp++)
                            //    mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            //Array.Sort(mkyhzArray);
                            //mMaxKyqd = mkyhzArray[4];
                            //mMinKyqd = Round(mkyhzArray[0], 2);
                            //sItem["DHKZQDMIN"] = mMinKyqd.ToString();
                            //sItem["QDSSL"] = Round((GetSafeDouble(sItem["KZPJ"]) - GetSafeDouble(sItem["DHKZPJ"])) / GetSafeDouble(sItem["KZPJ"]) * 100, 1).ToString("0.0");
                            //sItem["QDSSLYQ"] = extraDJ_item["QDSSL"];
                            //if (IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"]) == "符合")
                            //{
                            //    sItem["QDSSLPD"] = "合格";
                            //    mFlag_Hg = true;
                            //}
                            //else
                            //{
                            //    sItem["QDSSLPD"] = "不合格";
                            //    mFlag_Bhg = true;
                            //}
                        }
                        else
                            sItem["QDSSLPD"] = "----";
                        #endregion
                    }

                    #region 磨坑长度
                    if (jcxm.Contains("、磨坑长度、"))
                    {
                        if (IsQualified(sItem["MKCDYQ"], sItem["MKCD"]) == "符合")
                        {
                            sItem["MKCDPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["MKCDPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                        sItem["MKCDPD"] = "----";
                    #endregion

                    #region 耐磨性
                    if (jcxm.Contains("、耐磨性、"))
                    {
                        if (IsQualified(sItem["NMDYQ"], sItem["NMD"]) == "符合")
                        {
                            sItem["NMDPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["NMDPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                        sItem["NMDPD"] = "----";
                    #endregion

                    #region 吸水率
                    if (jcxm.Contains("、吸水率、"))
                    {

                        sItem["XSL1"] = Round((Conversion.Val(sItem["XSLM1_1"]) - Conversion.Val(sItem["XSLM0_1"])) / (Conversion.Val(sItem["XSLM0_1"])) * 100, 2).ToString();
                        sItem["XSL2"] = Round((Conversion.Val(sItem["XSLM1_2"]) - Conversion.Val(sItem["XSLM0_2"])) / (Conversion.Val(sItem["XSLM0_2"])) * 100, 2).ToString();
                        sItem["XSL3"] = Round((Conversion.Val(sItem["XSLM1_3"]) - Conversion.Val(sItem["XSLM0_3"])) / (Conversion.Val(sItem["XSLM0_3"])) * 100, 2).ToString();
                        sItem["XSL4"] = Round((Conversion.Val(sItem["XSLM1_4"]) - Conversion.Val(sItem["XSLM0_4"])) / (Conversion.Val(sItem["XSLM0_4"])) * 100, 2).ToString();
                        sItem["XSL5"] = Round((Conversion.Val(sItem["XSLM1_5"]) - Conversion.Val(sItem["XSLM0_5"])) / (Conversion.Val(sItem["XSLM0_5"])) * 100, 2).ToString();
                        sItem["XSL"] = Round(((Conversion.Val(sItem["XSL1"])) + (Conversion.Val(sItem["XSL2"])) + (Conversion.Val(sItem["XSL3"])) + (Conversion.Val(sItem["XSL4"])) + (Conversion.Val(sItem["XSL5"]))) / 5, 1).ToString();


                        if (IsQualified(sItem["XSLYQ"], sItem["XSL"]) == "合格")
                        {
                            sItem["XSLPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["XSLPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                        sItem["XSLPD"] = "----";
                    #endregion

                    #region 防滑性能
                    if (jcxm.Contains("、防滑性能、"))
                    {
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
            if (mAllHg)
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                mjcjg = "不合格";
                jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                if (mFlag_Bhg && mFlag_Hg)
                    jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
