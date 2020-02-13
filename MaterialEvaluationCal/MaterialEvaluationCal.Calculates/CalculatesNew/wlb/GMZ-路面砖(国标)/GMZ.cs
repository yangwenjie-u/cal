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
                else
                {
                    sItem["KYQDYQ"] = "抗压强度平均值需≥" + GetSafeDouble(MItem[0]["G_KYPJZ"]).ToString("0.0").Trim() + "MPa，单块最小值需≥" + GetSafeDouble(MItem[0]["G_KYMIN"]).ToString("0.0").Trim() + "MPa。";

                    sItem["KZQDYQ"] = "抗折强度平均值需≥" + GetSafeDouble(MItem[0]["G_KZPJZ"]).ToString("0.00").Trim() + "MPa，单块最小值需≥" + GetSafeDouble(MItem[0]["G_KZMIN"]).ToString("0.00").Trim() + "MPa。";
                    //计算单组的抗压强度,并进行合格判断
                    if (GetSafeDouble(sItem["chb"]) < 5)
                    {
                        if (jcxm.Contains("、抗压强度、") || jcxm.Contains("、抗折强度、"))
                        {
                            double mMj = Round(GetSafeDouble(sItem["dbcd"]) * GetSafeDouble(sItem["dbkd"]), 1);
                            sItem["mj1"] = mMj.ToString();
                            sItem["kyhz1"] = Round(GetSafeDouble(sItem["kyhz1"]), 1).ToString();
                            sItem["kyhz2"] = GetSafeDouble(sItem["kyhz2"]).ToString("0.0");
                            sItem["kyhz3"] = GetSafeDouble(sItem["kyhz3"]).ToString("0.0");
                            sItem["kyhz4"] = GetSafeDouble(sItem["kyhz4"]).ToString("0.0");
                            sItem["kyhz5"] = GetSafeDouble(sItem["kyhz5"]).ToString("0.0");
                            if (mMj != 0)
                                sItem["kyqd1"] = Round(1000 * GetSafeDouble(sItem["kyhz1"]) / mMj, 1).ToString();
                            else
                                sItem["kyqd1"] = "0";
                            if (mMj != 0)
                                sItem["kyqd2"] = Round(1000 * GetSafeDouble(sItem["kyhz2"]) / (mMj), 1).ToString();
                            else
                                sItem["kyqd2"] = "0";
                            if (mMj != 0)
                                sItem["kyqd3"] = Round(1000 * GetSafeDouble(sItem["kyhz3"]) / (mMj), 1).ToString();
                            else
                                sItem["kyqd3"] = "0";
                            if (mMj != 0)
                                sItem["kyqd4"] = Round(1000 * GetSafeDouble(sItem["kyhz4"]) / (mMj), 1).ToString();
                            else
                                sItem["kyqd4"] = "0";
                            if (mMj != 0)
                                sItem["kyqd5"] = Round(1000 * GetSafeDouble(sItem["kyhz5"]) / (mMj), 1).ToString();
                            else
                                sItem["kyqd5"] = "0";
                            //抗压平均值
                            mPjz = (GetSafeDouble(sItem["kyqd1"]) + GetSafeDouble(sItem["kyqd2"]) + GetSafeDouble(sItem["kyqd3"]) + GetSafeDouble(sItem["kyqd4"]) + GetSafeDouble(sItem["kyqd5"])) / 5;
                            sItem["kypj"] = Round((mPjz), 1).ToString();
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["kyqd1"] + "," + sItem["kyqd2"] + "," + sItem["kyqd3"] + "," + sItem["kyqd4"] + "," + sItem["kyqd5"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 4; vp++)
                                mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray);
                            mMaxKyqd = mkyhzArray[4];
                            mMinKyqd = Round(mkyhzArray[0], 1);
                            sItem["kyqdmin"] = mMinKyqd.ToString();
                            if (GetSafeDouble(sItem["kypj"]) >= GetSafeDouble(extraDJ_item["kypjz"]) && GetSafeDouble(sItem["kyqdmin"]) >= GetSafeDouble(extraDJ_item["KYMIN"]))
                            {
                                sItem["kypd"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["kypd"] = "不合格";
                                mAllHg = false;
                                mFlag_Bhg = true;
                            }
                            sItem["kzpd"] = "----";
                        }
                        else
                            sItem["kypd"] = "----";
                        if (jcxm.Contains("、抗冻性、"))
                        {
                            double mMj = Round(GetSafeDouble(sItem["dbcd"]) * GetSafeDouble(sItem["dbkd"]), 1);
                            sItem["mj1"] = mMj.ToString();
                            sItem["dhkyhz1"] = Round(GetSafeDouble(sItem["dhkyhz1"]), 1).ToString();
                            sItem["dhkyhz2"] = Round(GetSafeDouble(sItem["dhkyhz2"]), 1).ToString();
                            sItem["dhkyhz3"] = Round(GetSafeDouble(sItem["dhkyhz3"]), 1).ToString();
                            sItem["dhkyhz4"] = Round(GetSafeDouble(sItem["dhkyhz4"]), 1).ToString();
                            sItem["dhkyhz5"] = Round(GetSafeDouble(sItem["dhkyhz5"]), 1).ToString();
                            if (mMj != 0)
                            {
                                sItem["dhkyqd1"] = Round(1000 * GetSafeDouble(sItem["dhkyhz1"]) / (mMj), 1).ToString();
                                sItem["dhkyqd2"] = Round(1000 * GetSafeDouble(sItem["dhkyhz2"]) / (mMj), 1).ToString();
                                sItem["dhkyqd3"] = Round(1000 * GetSafeDouble(sItem["dhkyhz3"]) / (mMj), 1).ToString();
                                sItem["dhkyqd4"] = Round(1000 * GetSafeDouble(sItem["dhkyhz4"]) / (mMj), 1).ToString();
                                sItem["dhkyqd5"] = Round(1000 * GetSafeDouble(sItem["dhkyhz5"]) / (mMj), 1).ToString();
                            }
                            else
                            {
                                sItem["dhkyqd1"] = "0";
                                sItem["dhkyqd2"] = "0";
                                sItem["dhkyqd3"] = "0";
                                sItem["dhkyqd4"] = "0";
                                sItem["dhkyqd5"] = "0";
                            }
                            //抗压平均值
                            mPjz = (GetSafeDouble(sItem["dhkyqd1"]) + GetSafeDouble(sItem["dhkyqd2"]) + GetSafeDouble(sItem["dhkyqd3"]) + GetSafeDouble(sItem["dhkyqd4"]) + GetSafeDouble(sItem["dhkyqd5"])) / 5;
                            sItem["dhkypj"] = Round((mPjz), 1).ToString();
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["dhkyqd1"] + "," + sItem["dhkyqd2"] + "," + sItem["dhkyqd3"] + "," + sItem["dhkyqd4"] + "," + sItem["dhkyqd5"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 4; vp++)
                                mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray);
                            mMaxKyqd = mkyhzArray[4];
                            mMinKyqd = Round(mkyhzArray[0], 1);
                            sItem["kyqdmin"] = mMinKyqd.ToString();
                            sItem["QDSSL"] = Round((GetSafeDouble(sItem["kypj"]) - GetSafeDouble(sItem["dhkypj"])) / GetSafeDouble(sItem["kypj"]) * 100, 1).ToString();

                            if (IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"]) == "符合")
                            {
                                sItem["qdsslpd"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["qdsslpd"] = "不合格";
                                mFlag_Bhg = true;
                            }
                        }

                        else
                            sItem["qdsslpd"] = "----";
                    }
                    else
                    {
                        if (jcxm.Contains("、抗压强度、") || jcxm.Contains("、抗折强度、"))
                        {
                            sItem["kzhz1"] = Round(GetSafeDouble(sItem["kzhz1"]), 2).ToString();
                            sItem["kzhz2"] = Round(GetSafeDouble(sItem["kzhz2"]), 2).ToString();
                            sItem["kzhz3"] = Round(GetSafeDouble(sItem["kzhz3"]), 2).ToString();
                            sItem["kzhz4"] = Round(GetSafeDouble(sItem["kzhz4"]), 2).ToString();
                            sItem["kzhz5"] = Round(GetSafeDouble(sItem["kzhz5"]), 2).ToString();
                            sItem["kzqd1"] = Round((3000 * GetSafeDouble(sItem["kzhz1"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["kd1"]) * GetSafeDouble(sItem["hd1"]) * GetSafeDouble(sItem["hd1"])), 2).ToString();
                            sItem["kzqd2"] = Round((3000 * GetSafeDouble(sItem["kzhz2"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["kd2"]) * GetSafeDouble(sItem["hd2"]) * GetSafeDouble(sItem["hd2"])), 2).ToString();
                            sItem["kzqd3"] = Round((3000 * GetSafeDouble(sItem["kzhz3"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["kd3"]) * GetSafeDouble(sItem["hd3"]) * GetSafeDouble(sItem["hd3"])), 2).ToString();
                            sItem["kzqd4"] = Round((3000 * GetSafeDouble(sItem["kzhz4"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["kd4"]) * GetSafeDouble(sItem["hd4"]) * GetSafeDouble(sItem["hd4"])), 2).ToString();
                            sItem["kzqd5"] = Round((3000 * GetSafeDouble(sItem["kzhz5"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["kd5"]) * GetSafeDouble(sItem["hd5"]) * GetSafeDouble(sItem["hd5"])), 2).ToString();
                            //抗压平均值
                            mPjz = (GetSafeDouble(sItem["kzqd1"]) + GetSafeDouble(sItem["kzqd2"]) + GetSafeDouble(sItem["kzqd3"]) + GetSafeDouble(sItem["kzqd4"]) + GetSafeDouble(sItem["kzqd5"])) / 5;
                            sItem["kzpj"] = Round((mPjz), 2).ToString();
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["kzqd1"] + "," + sItem["kzqd2"] + "," + sItem["kzqd3"] + "," + sItem["kzqd4"] + "," + sItem["kzqd5"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 4; vp++)
                                mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray);
                            mMaxKyqd = mkyhzArray[4];
                            mMinKyqd = Round(mkyhzArray[0], 2);
                            sItem["kzqdmin"] = mMinKyqd.ToString();
                            if (GetSafeDouble(sItem["kzpj"]) >= GetSafeDouble(extraDJ_item["KZPJZ"]) && GetSafeDouble(sItem["kzqdmin"]) >= GetSafeDouble(extraDJ_item["KZMIN"]))
                            {
                                sItem["JCJG"] = "合格";
                                sItem["kzpd"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["JCJG"] = "不合格";
                                sItem["kzpd"] = "不合格";
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                            sItem["kypd"] = "----";
                        }
                        else
                            sItem["kzpd"] = "----";
                        if (jcxm.Contains("、抗冻性、"))
                        {
                            sItem["dhkzhz1"] = Round(GetSafeDouble(sItem["dhkzhz1"]), 2).ToString();
                            sItem["dhkzhz2"] = Round(GetSafeDouble(sItem["dhkzhz2"]), 2).ToString();
                            sItem["dhkzhz3"] = Round(GetSafeDouble(sItem["dhkzhz3"]), 2).ToString();
                            sItem["dhkzhz4"] = Round(GetSafeDouble(sItem["dhkzhz4"]), 2).ToString();
                            sItem["dhkzhz5"] = Round(GetSafeDouble(sItem["dhkzhz5"]), 2).ToString();
                            sItem["dhkzqd1"] = Round((3000 * GetSafeDouble(sItem["dhkzhz1"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["dhkd1"]) * GetSafeDouble(sItem["dhhd1"]) * GetSafeDouble(sItem["dhhd1"])), 2).ToString();
                            sItem["dhkzqd2"] = Round((3000 * GetSafeDouble(sItem["dhkzhz2"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["dhkd2"]) * GetSafeDouble(sItem["dhhd2"]) * GetSafeDouble(sItem["dhhd2"])), 2).ToString();
                            sItem["dhkzqd3"] = Round((3000 * GetSafeDouble(sItem["dhkzhz3"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["dhkd3"]) * GetSafeDouble(sItem["dhhd3"]) * GetSafeDouble(sItem["dhhd3"])), 2).ToString();
                            sItem["dhkzqd4"] = Round((3000 * GetSafeDouble(sItem["dhkzhz4"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["dhkd4"]) * GetSafeDouble(sItem["dhhd4"]) * GetSafeDouble(sItem["dhhd4"])), 2).ToString();
                            sItem["dhkzqd5"] = Round((3000 * GetSafeDouble(sItem["dhkzhz5"]) * GetSafeDouble(sItem["zzjl"])) / (2 * GetSafeDouble(sItem["dhkd5"]) * GetSafeDouble(sItem["dhhd5"]) * GetSafeDouble(sItem["dhhd5"])), 2).ToString();
                            //抗压平均值
                            mPjz = (GetSafeDouble(sItem["dhkzqd1"]) + GetSafeDouble(sItem["dhkzqd2"]) + GetSafeDouble(sItem["dhkzqd3"]) + GetSafeDouble(sItem["dhkzqd4"]) + GetSafeDouble(sItem["dhkzqd5"])) / 5;
                            sItem["dhkzpj"] = Round((mPjz), 2).ToString();
                            //标准值计算、判定，平均值判定，单组合格判定
                            mlongStr = sItem["dhkzqd1"] + "," + sItem["dhkzqd2"] + "," + sItem["dhkzqd3"] + "," + sItem["dhkzqd4"] + "," + sItem["dhkzqd5"];
                            mtmpArray = mlongStr.Split(',');
                            for (vp = 0; vp <= 4; vp++)
                                mkyhzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyhzArray);
                            mMaxKyqd = mkyhzArray[4];
                            mMinKyqd = Round(mkyhzArray[0], 2);
                            sItem["dhkzqdmin"] = mMinKyqd.ToString();
                            sItem["QDSSL"] = Round((GetSafeDouble(sItem["kzpj"]) - GetSafeDouble(sItem["dhkzpj"])) / GetSafeDouble(sItem["kzpj"]) * 100, 1).ToString("0.0");
                            sItem["QDSSLYQ"] = extraDJ_item["QDSSL"];
                            if (IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"]) == "符合")
                            {
                                sItem["qdsslpd"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["qdsslpd"] = "不合格";
                                mFlag_Bhg = true;
                            }
                        }
                        else
                            sItem["qdsslpd"] = "----";
                    }


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
                    if (jcxm.Contains("、耐磨度、"))
                    {
                        if (IsQualified(sItem["NMDYQ"], sItem["NMD"]) == "符合")
                        {
                            sItem["nmdpd"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["nmdpd"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                        sItem["nmdpd"] = "----";
                    if (jcxm.Contains("、吸水率、"))
                    {

                        sItem["xsl1"] = Round((Conversion.Val(sItem["XSLM1_1"]) - Conversion.Val(sItem["XSLM0_1"])) / (Conversion.Val(sItem["XSLM0_1"])) * 100, 2).ToString();
                        sItem["xsl2"] = Round((Conversion.Val(sItem["XSLM1_2"]) - Conversion.Val(sItem["XSLM0_2"])) / (Conversion.Val(sItem["XSLM0_2"])) * 100, 2).ToString();
                        sItem["xsl3"] = Round((Conversion.Val(sItem["XSLM1_3"]) - Conversion.Val(sItem["XSLM0_3"])) / (Conversion.Val(sItem["XSLM0_3"])) * 100, 2).ToString();
                        sItem["xsl4"] = Round((Conversion.Val(sItem["XSLM1_4"]) - Conversion.Val(sItem["XSLM0_4"])) / (Conversion.Val(sItem["XSLM0_4"])) * 100, 2).ToString();
                        sItem["xsl5"] = Round((Conversion.Val(sItem["XSLM1_5"]) - Conversion.Val(sItem["XSLM0_5"])) / (Conversion.Val(sItem["XSLM0_5"])) * 100, 2).ToString();
                        sItem["XSL"] = Round(((Conversion.Val(sItem["xsl1"])) + (Conversion.Val(sItem["xsl2"])) + (Conversion.Val(sItem["xsl3"])) + (Conversion.Val(sItem["xsl4"])) + (Conversion.Val(sItem["xsl5"]))) / 5, 1).ToString();


                        if (IsQualified(sItem["xslyq"], sItem["XSL"]) == "符合")
                        {
                            sItem["xslpd"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["xslpd"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                        sItem["xslpd"] = "----";
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
                    if (sItem["qdsslpd"] == "不合格" || sItem["kypd"] == "不合格" || sItem["kzpd"] == "不合格" || sItem["xslpd"] == "不合格" || sItem["nmdpd"] == "不合格" || sItem["MKCDPD"] == "不合格")
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
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["pdbz"] + "标准要求。";
            }
            else
            {
                mjcjg = "不合格";
                jsbeizhu = "该组试样不符合" + MItem[0]["pdbz"] + "标准要求。";
                if (mFlag_Bhg && mFlag_Hg)
                    jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["pdbz"] + "标准要求。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
