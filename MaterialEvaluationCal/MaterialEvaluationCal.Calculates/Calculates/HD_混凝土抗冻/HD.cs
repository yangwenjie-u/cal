using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class HD : BaseMethods
    {
        public void Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string mSjdjbh, mSjdj = "";
            double mSjcc, mMj, mSjcc1 = 0;
            double mSz, mQdyq, mHsxs, mttjhsxs = 0;
            string mJSFF = "";
            string mlongStr = "";
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;
            int mbhggs = 0;//不合格数量
            List<double> mtmpArray = new List<double>();
            var extraDJ = dataExtra["BZ_HD_DJ"];
            var extraGG = dataExtra["BZ_HD_GG"];
            var extraCS = dataExtra["BZ_HD_CS"];
            var extraFF = dataExtra["BZ_HD_FF"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();
            try
            {
                int eachCount = -1;

                foreach (var jcxm in jcxmItems)
                {
                    if (jcxm.ToUpper().Contains("BGJG"))
                    {
                        continue;
                    }
                    eachCount++;
                    var SItem = retData[jcxm]["S_HD"];
                    //var MItem = retData[jcxm]["M_HD"];
                    var XQData = retData[jcxm]["S_BY_RW_XQ"];

                    foreach (var sItem in SItem)
                    {
                        mSjdj = sItem["SJDJ"];

                        //if (null == MItem)
                        //{
                        //    mAllHg = false;
                        //    XQData[eachCount]["JCJG"] = "不合格";
                        //    XQData[eachCount]["JCJGMS"] = "获取不到主表数据";
                        //    continue;
                        //}

                        if (string.IsNullOrEmpty(mSjdj))
                        {
                            sItem["SJCC"] = "0";
                            sItem["HSXS"] = "0";
                            mbhggs = mbhggs + 1;
                            //MItem[0]["BGBH"] = "0";
                            //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "规格不祥。";

                            mAllHg = false;
                            XQData[eachCount]["JCJG"] = "不合格";
                            XQData[eachCount]["JCJGMS"] = "获取不到设计等级{SJDJ}";
                            continue;
                        }

                        var extraFieldsGG = extraGG.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(sItem["GGXH"]));

                        if (null == extraFieldsGG)
                        {
                            sItem["SJCC"] = "0";
                            sItem["HSXS"] = "0";
                            //MItem[eachCount]["BGBH"] = "0";
                            //MItem[eachCount]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "规格不祥。";

                            mAllHg = false;
                            mbhggs = mbhggs + 1;
                            XQData[eachCount]["JCJG"] = "不合格";
                            XQData[eachCount]["JCJGMS"] = @"帮助表HDGG中获取不到" + sItem["GGXH"] + "数据";
                            continue;
                        }

                        sItem["SJCC"] = extraFieldsGG["CD"];
                        sItem["HSXS"] = extraFieldsGG["HSXS"];

                        mSjcc = GetSafeDouble(sItem["SJCC"]);
                        mSjcc1 = 0;
                        if (sItem["SJCC1"] == null || sItem["SJCC1"] == "0")
                            mSjcc1 = mSjcc;
                        else
                            mSjcc1 = GetSafeDouble(sItem["SJCC1"]);

                        mMj = mSjcc * mSjcc1;
                        if (mMj <= 0)//试件面积<=0则不予计算
                        {
                            // MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "单组流水号:" + item["DZBH"] + "试件尺寸为空";
                            // MItem[0]["BGBH"] = "";

                            mAllHg = false;
                            mbhggs = mbhggs + 1;
                            XQData[0]["JCJG"] = "不合格";
                            XQData[0]["JCJGMS"] = "试件面积<=0则不予计算";
                            continue;
                        }

                        mHsxs = GetSafeDouble(sItem["HSXS"]);
                        if (mHsxs <= 0)//'换算系数=0则不予计算
                        {
                            //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "单组流水号:" + item["DZBH"] + "换算系数为空";
                            //MItem[0]["BGBH"] = "";
                            mAllHg = false;
                            mbhggs = mbhggs + 1;
                            XQData[0]["JCJG"] = "不合格";
                            XQData[0]["JCJGMS"] = "换算系数=0则不予计算";
                            continue;
                        }

                        //从设计等级表中取得相应的计算数值、等级标准
                        var extraFieldsDJ = extraDJ.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(mSjdj));

                        if (null == extraFieldsDJ)
                        {
                            mSz = 0;
                            mQdyq = 0;
                            mJSFF = "";
                            sItem["JCJG"] = "依据不详";
                            //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "单组流水号:" + item["DZBH"] + "设计等级为空或不存在";
                            //MItem[0]["BGBH"] = "";
                            mbhggs = mbhggs + 1;
                            mAllHg = false;
                            XQData[0]["JCJG"] = "不合格";
                            XQData[0]["JCJGMS"] = "单组流水号: " + sItem["DZBH"] + "设计等级为空或不存在";
                            continue;
                        }

                        var extraFieldsFF = extraDJ.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(sItem["DRFF"]));

                        ////计算龄期
                        if (DateTime.Compare(GetSafeDateTime(sItem["ZZRQ"]), GetSafeDateTime("1900-1-1")) <= 0 || DateTime.Compare(GetSafeDateTime(sItem["SYRQ"]), GetSafeDateTime("1900-1-1")) <= 0)
                        {
                            sItem["LQ"] = "0";
                        }
                        else
                        {
                            TimeSpan ts = GetSafeDateTime(sItem["SYRQ"]) - GetSafeDateTime(sItem["ZZRQ"]);
                            sItem["lq"] = ts.Days.ToString();

                            if (ts.Days < 28 && sItem["YHTJ"].Trim() == "工地标准养护")
                            {
                                sItem["LQ"] = "28";
                                sItem["SYRQ"] = GetSafeDateTime(sItem["ZZRQ"]).AddDays(28).ToString();
                            }
                        }

                        //初始化kypj28
                        sItem["KYPJ28"] = "----";
                        switch (jcxm)
                        {
                            #region 强度损失
                            case "强度损失":

                                #region 强度损失 - kypj28

                                double qdVal = 0;
                                for (int i = 1; i < 4; i++)
                                {
                                    qdVal = Math.Round(1000 * GetSafeDouble(sItem["KYHZ28_" + i]) / mMj, 1);
                                    sItem["KYQD28_" + i] = qdVal.ToString();
                                    mtmpArray.Add(qdVal);
                                }
                                mtmpArray.Sort();
                                mMaxKyqd = mtmpArray[2];
                                mMinKyqd = mtmpArray[0];
                                mMidKyqd = mtmpArray[1];
                                mAvgKyqd = mtmpArray.Average();

                                if (mMidKyqd == 0)
                                {
                                    mAllHg = false;
                                    mbhggs = mbhggs + 1;
                                }
                                else
                                {
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["KYPJ28"] = Math.Round(mMidKyqd, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["KYPJ28"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["KYPJ28"] = Math.Round((mMidKyqd + mMaxKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["KYPJ28"] = Math.Round(mAvgKyqd, 1).ToString();
                                    }
                                }

                                #endregion

                                #region 强度损失 - kypj
                                for (int i = 1; i < 4; i++)
                                {
                                    qdVal = Math.Round(1000 * GetSafeDouble(sItem["kyhz" + i]) / mMj, 1);
                                    sItem["kyqd" + i] = qdVal.ToString();
                                    mtmpArray.Add(qdVal);
                                }
                                mtmpArray.Sort();
                                mMaxKyqd = mtmpArray[2];
                                mMinKyqd = mtmpArray[0];
                                mMidKyqd = mtmpArray[1];
                                mAvgKyqd = mtmpArray.Average();

                                if (mMidKyqd == 0)
                                {
                                    mAllHg = false;
                                    mbhggs = mbhggs + 1;
                                }
                                else
                                {
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["KYPJ"] = Math.Round(mMidKyqd, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["KYPJ"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["KYPJ"] = Math.Round((mMidKyqd + mMaxKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["KYPJ"] = Math.Round(mAvgKyqd, 1).ToString();
                                    }
                                }
                                #endregion

                                #region 强度损失 - DHPJ
                                for (int i = 1; i < 4; i++)
                                {
                                    qdVal = Math.Round(1000 * GetSafeDouble(sItem["dhhz1_" + i]) / mMj, 1);
                                    sItem["dhqd1_" + i] = qdVal.ToString();
                                    mtmpArray.Add(qdVal);
                                }
                                mtmpArray.Sort();
                                mMaxKyqd = mtmpArray[2];
                                mMinKyqd = mtmpArray[0];
                                mMidKyqd = mtmpArray[1];
                                mAvgKyqd = mtmpArray.Average();

                                if (mMidKyqd == 0)
                                {
                                    mAllHg = false;
                                    mbhggs = mbhggs + 1;
                                }
                                else
                                {

                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["DHPJ1"] = Math.Round(mMidKyqd, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["DHPJ1"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["DHPJ1"] = Math.Round((mMidKyqd + mMaxKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["DHPJ1"] = Math.Round(mAvgKyqd, 1).ToString();
                                    }

                                    sItem["QDSSL1"] = Math.Round((GetSafeDouble(sItem["KYPJ"], 1) - GetSafeDouble(sItem["DHPJ1"], 1)) / GetSafeDouble(sItem["KYPJ"], 1) * 100, 1).ToString();
                                }

                                #endregion

                                #region kdbh D25 D50

                                if (sItem["KDBH"].Trim() != "D25" && sItem["KDBH"].Trim() != "D50")
                                {
                                    #region kyqd2
                                    for (int i = 1; i < 4; i++)
                                    {
                                        qdVal = Math.Round(1000 * GetSafeDouble(sItem["KYHZ2_" + i]) / mMj, 1);
                                        sItem["KYQD2_" + i] = qdVal.ToString();
                                        mtmpArray.Add(qdVal);
                                    }
                                    mtmpArray.Sort();
                                    mMaxKyqd = mtmpArray[2];
                                    mMinKyqd = mtmpArray[0];
                                    mMidKyqd = mtmpArray[1];
                                    mAvgKyqd = mtmpArray.Average();

                                    if (mMidKyqd == 0)
                                    {
                                        mAllHg = false;
                                        mbhggs = mbhggs + 1;
                                    }
                                    else
                                    {
                                        if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                        {
                                            sItem["KYPJ2"] = Math.Round(mMidKyqd, 1).ToString();
                                        }
                                        if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                        {
                                            sItem["KYPJ2"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                        }
                                        if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                        {
                                            sItem["KYPJ2"] = Math.Round((mMidKyqd + mMaxKyqd) / 2, 1).ToString();
                                        }
                                        if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                        {
                                            sItem["KYPJ2"] = Math.Round(mAvgKyqd, 1).ToString();
                                        }
                                    }

                                    #endregion

                                    #region dhqd2
                                    for (int i = 1; i < 4; i++)
                                    {
                                        qdVal = Math.Round(1000 * GetSafeDouble(sItem["dhhz2_" + i]) / mMj, 1);
                                        sItem["DHQD2_" + i] = qdVal.ToString();
                                        mtmpArray.Add(qdVal);
                                    }
                                    mtmpArray.Sort();
                                    mMaxKyqd = mtmpArray[2];
                                    mMinKyqd = mtmpArray[0];
                                    mMidKyqd = mtmpArray[1];
                                    mAvgKyqd = mtmpArray.Average();

                                    if (mMidKyqd == 0)
                                    {
                                        mAllHg = false;
                                        mbhggs = mbhggs + 1;
                                    }
                                    else
                                    {
                                        if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                        {
                                            sItem["DHPJ2"] = Math.Round(mMidKyqd, 1).ToString();
                                        }
                                        if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                        {
                                            sItem["DHPJ2"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                        }
                                        if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                        {
                                            sItem["DHPJ2"] = Math.Round((mMidKyqd + mMaxKyqd) / 2, 1).ToString();
                                        }
                                        if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                        {
                                            sItem["DHPJ2"] = Math.Round(mAvgKyqd, 1).ToString();
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    sItem["KYQD2_1"] = "----";
                                    sItem["KYQD2_2"] = "----";
                                    sItem["KYQD2_3"] = "----";
                                    sItem["KYPJ2"] = "----";
                                    sItem["DHQD2_1"] = "----";
                                    sItem["DHQD2_2"] = "----";
                                    sItem["DHQD2_3"] = "----";
                                    sItem["QDSSL2"] = "----";
                                    sItem["DHPJ2"] = "----";
                                }
                                #endregion
                                break;
                            #endregion

                            #region 质量损失
                            case "质量损失":
                                for (int i = 1; i < 4; i++)
                                {
                                    sItem["DQZL2_" + i] = (sItem["DQZL2_" + i] == "0" || sItem["DQZL2_" + i] == "") ? sItem["DQZL" + i] : sItem["DQZL2_" + i];
                                    sItem["ZLSSL1_" + i] = Math.Round((GetSafeDouble(sItem["DQZL" + i]) - GetSafeDouble(sItem["DHZL1_" + i])) / GetSafeDouble(sItem["DQZL" + i]), 1).ToString();
                                    qdVal = GetSafeDouble(sItem["ZLSSL1_" + i]) < 0 ? 0 : GetSafeDouble(sItem["ZLSSL1_" + i]);

                                    mtmpArray.Add(qdVal);
                                }
                                mtmpArray.Sort();
                                mMaxKyqd = mtmpArray[2];
                                mMinKyqd = mtmpArray[0];
                                mMidKyqd = mtmpArray[1];
                                mAvgKyqd = mtmpArray.Average();

                                if (mMidKyqd == 0)
                                {
                                    mAllHg = false;
                                    mbhggs = mbhggs + 1;
                                }
                                else
                                {
                                    if (mMaxKyqd - mMidKyqd > 1 && mMidKyqd - mMinKyqd > 1)
                                    {
                                        sItem["ZLSSL1"] = Math.Round(mMidKyqd, 1).ToString();
                                    }
                                    if (mMaxKyqd - mMidKyqd > 1 && mMidKyqd - mMinKyqd <= 1)
                                    {
                                        sItem["ZLSSL1"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                    }
                                    if (mMaxKyqd - mMidKyqd <= 1 && mMidKyqd - mMinKyqd > 1)
                                    {
                                        sItem["ZLSSL1"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                                    }
                                    if (mMaxKyqd - mMidKyqd <= 1 && mMidKyqd - mMinKyqd <= 1)
                                    {
                                        sItem["ZLSSL1"] = Math.Round(mAvgKyqd, 1).ToString();
                                    }
                                }

                                #region KDBH D25 D50

                                if (sItem["KDBH"].Trim() != "D25" && sItem["KDBH"].Trim() != "D50")
                                {
                                    #region ZLSSL2
                                    for (int i = 1; i < 4; i++)
                                    {
                                        qdVal = Math.Round((GetSafeDouble(sItem["DQZL2_" + i]) - GetSafeDouble(sItem["DHZL2_" + i])) / GetSafeDouble(sItem["DQZL2_" + i]), 1);
                                        sItem["ZLSSL2_" + i] = qdVal.ToString();
                                        qdVal = GetSafeDouble(sItem["ZLSSL2_" + i]) < 0 ? 0 : GetSafeDouble(sItem["ZLSSL2_" + i]);
                                        mtmpArray.Add(qdVal);
                                    }
                                    mtmpArray.Sort();
                                    mMaxKyqd = mtmpArray[2];
                                    mMinKyqd = mtmpArray[0];
                                    mMidKyqd = mtmpArray[1];
                                    mAvgKyqd = mtmpArray.Average();

                                    if (mMidKyqd == 0)
                                    {
                                        mAllHg = false;
                                        mbhggs = mbhggs + 1;
                                    }
                                    else
                                    {
                                        if (mMaxKyqd - mMidKyqd > 1 && mMidKyqd - mMinKyqd > 1)
                                        {
                                            sItem["ZLSSL2"] = Math.Round(mMidKyqd, 1).ToString();
                                        }
                                        if (mMaxKyqd - mMidKyqd > 1 && mMidKyqd - mMinKyqd <= 1)
                                        {
                                            sItem["ZLSSL2"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                        }
                                        if (mMaxKyqd - mMidKyqd <= 1 && mMidKyqd - mMinKyqd > 1)
                                        {
                                            sItem["ZLSSL2"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                                        }
                                        if (mMaxKyqd - mMidKyqd <= 1 && mMidKyqd - mMinKyqd <= 1)
                                        {
                                            sItem["ZLSSL2"] = Math.Round(mAvgKyqd, 1).ToString();
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    sItem["DQZL2_1"] = "----";
                                    sItem["DQZL2_2"] = "----";
                                    sItem["DQZL2_3"] = "----";
                                    sItem["DHZL2_1"] = "----";
                                    sItem["DHZL2_2"] = "----";
                                    sItem["DHZL2_3"] = "----";
                                    sItem["ZLSSL2_1"] = "----";
                                    sItem["ZLSSL2_2"] = "----";
                                    sItem["ZLSSL2_3"] = "----";
                                    sItem["ZLSSL2"] = "----";
                                }
                                #endregion

                                break;
                            #endregion

                            #region 相对动弹性模量
                            case "相对动弹性模量":
                                #region XDDTXML1

                                qdVal = 0;
                                for (int i = 1; i < 4; i++)
                                {
                                    qdVal = Math.Round((GetSafeDouble(sItem["HJDHZ1_" + i]) * GetSafeDouble(sItem["HJDHZ1_" + i])) / (GetSafeDouble(sItem["HJCZ" + i]) * GetSafeDouble(sItem["HJCZ" + i])) * 100, 1);
                                    sItem["XDDTXML1_" + i] = qdVal.ToString();
                                    mtmpArray.Add(qdVal);
                                }
                                mtmpArray.Sort();
                                mMaxKyqd = mtmpArray[2];
                                mMinKyqd = mtmpArray[0];
                                mMidKyqd = mtmpArray[1];
                                mAvgKyqd = mtmpArray.Average();

                                if (mMidKyqd == 0)
                                {
                                    mAllHg = false;
                                    mbhggs = mbhggs + 1;
                                }
                                else
                                {
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["XDDTXML1"] = Math.Round(mMidKyqd, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["XDDTXML1"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["XDDTXML1"] = Math.Round((mMidKyqd + mMaxKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["XDDTXML1"] = Math.Round(mAvgKyqd, 1).ToString();
                                    }
                                }
                                #endregion

                                #region XDDTXML2
                                qdVal = 0;
                                for (int i = 1; i < 4; i++)
                                {
                                    qdVal = Math.Round((GetSafeDouble(sItem["HJDHZ2_" + i]) * GetSafeDouble(sItem["HJDHZ2_" + i])) / (GetSafeDouble(sItem["HJCZ" + i]) * GetSafeDouble(sItem["HJCZ" + i])) * 100, 1);
                                    sItem["XDDTXML2_" + i] = qdVal.ToString();
                                    mtmpArray.Add(qdVal);
                                }
                                mtmpArray.Sort();
                                mMaxKyqd = mtmpArray[2];
                                mMinKyqd = mtmpArray[0];
                                mMidKyqd = mtmpArray[1];
                                mAvgKyqd = mtmpArray.Average();

                                if (mMidKyqd == 0)
                                {
                                    mAllHg = false;
                                    mbhggs = mbhggs + 1;
                                }
                                else
                                {
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["XDDTXML2"] = Math.Round(mMidKyqd, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["XDDTXML2"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                                    {
                                        sItem["XDDTXML2"] = Math.Round((mMidKyqd + mMaxKyqd) / 2, 1).ToString();
                                    }
                                    if (Math.Round((mMaxKyqd - mMidKyqd) / mMidKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                                    {
                                        sItem["XDDTXML2"] = Math.Round(mAvgKyqd, 1).ToString();
                                    }
                                }
                                #endregion

                                break;
                                #endregion

                        }

                        #region 快冻法
                        if (sItem["DRFF"] == "快冻法")
                        {
                            //MItem[0]["WHICH"] = "0";
                            sItem["KDYQ"] = "相对动弹性模量应" + extraFieldsFF["XDTXML"] + "% 或质量损失应" + extraFieldsFF["ZLSSL"] + "%。";
                            if (IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL1"]) == "符合" && IsQualified(extraFieldsFF["XDTXML"], sItem["XDDTXML1"]) == "符合" && GetSafeDouble(sItem["DRCS2"]) >= GetSafeDouble(sItem["DRCS"]))
                            {
                                sItem["JCJG"] = "合格";
                                //MItem[0]["JGSM"] = "该组试样所检项目依据" + MItem[0]["PDBZ"] + "符合" + item["KDBH"].Trim() + "设计要求。";
                            }

                            if ((IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL1"]) == "符合" && IsQualified(extraFieldsFF["XDTXML"], sItem["XDDTXML1"]) == "符合") && (IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL2"]) == "不符合" || IsQualified(extraFieldsFF["XDTXML"], sItem["XDDTXML2"]) == "不符合") && GetSafeDouble(sItem["DRCS2"]) >= GetSafeDouble(sItem["DRCS"]))
                            {
                                sItem["JCJG"] = "合格";
                                string mkdbh = "";
                                var extraFieldsDj = extraCS.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(sItem["DRFF"].Trim()) && u.Keys.Contains("KDCS") && u.Values.Contains(sItem["DRCS1"].Trim()));
                                if (null == extraFieldsDj)
                                {
                                    mkdbh = extraFieldsDj["KDBH"];
                                }
                                //MItem[0]["JGSM"] = "该试件抗冻标号为：" + mkdbh.Trim() + ",不符合设计标号" + item["KDBH"] + "设计要求。";

                            }

                            if (IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL1"]) == "不符合" || IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL2"]) == "不符合")
                            {
                                sItem["GH_ZLSS"] = "不合格";
                            }
                            else
                            {
                                sItem["GH_ZLSS"] = "合格";
                            }

                            if (IsQualified(extraFieldsFF["XDTXML"], sItem["XDDTXML1"]) == "不符合" || IsQualified(extraFieldsFF["XDTXML"], sItem["XDDTXML2"]) == "不符合")
                            {
                                sItem["GH_DTML"] = "不合格";
                            }
                            else
                            {
                                sItem["GH_DTML"] = "合格";
                            }

                            if ((IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL1"]) == "不符合" || IsQualified(extraFieldsFF["XDTXML"], sItem["XDDTXML1"]) == "不符合") && (IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL2"]) == "不符合" || IsQualified(extraFieldsFF["XDTXML"], sItem["XDDTXML2"]) == "不符合"))
                            {
                                //MItem[0]["JGSM"] = "该组试样所检项目依据" + MItem[0]["PDBZ"] + "符合" + item["KDBH"].Trim() + "设计要求。";
                            }



                        }
                        #endregion

                        #region 慢冻法
                        if (sItem["DRFF"] == "慢冻法")
                        {
                            //MItem[0]["WHICH"] = "1";
                            sItem["KDYQ"] = "抗压强度损失率应" + extraFieldsFF["QDSSL"] + "% 质量损失应" + extraFieldsFF["ZLSSL"] + "%。";

                            if (sItem["KDBH"].Trim() != "D25" && sItem["KDBH"].Trim() != "D50")
                            {
                                if (IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL1"]) == "符合" && IsQualified(extraFieldsFF["QDSSL"], sItem["QDSSL1"]) == "符合" && IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL2"]) == "符合" && IsQualified(extraFieldsFF["QDSSL"], sItem["QDSSL2"]) == "符合" && GetSafeDouble(sItem["DRCS2"]) >= GetSafeDouble(sItem["DRCS"]))
                                {
                                    sItem["JCJG"] = "合格";
                                    //MItem[0]["JGSM"] = "该组试样所检项目依据" + MItem[0]["PDBZ"] + "符合" + item["KDBH"].Trim() + "设计要求。";
                                }
                                else
                                {
                                    //MItem[0]["JGSM"] = "该组试样所检项目依据" + MItem[0]["PDBZ"] + "不符合" + item["KDBH"].Trim() + "设计要求。";
                                }
                            }
                            else
                            {
                                sItem["DRCS2"] = "----";
                                if (IsQualified(extraFieldsFF["ZLSSL"], sItem["ZLSSL1"]) == "符合" && IsQualified(extraFieldsFF["QDSSL"], sItem["QDSSL1"]) == "符合")
                                {
                                    //MItem[0]["JGSM"] = "该组试样所检项目依据" + MItem[0]["PDBZ"] + "符合" + item["KDBH"].Trim() + "设计要求。";
                                }
                                else
                                {
                                    //MItem[0]["JGSM"] = "该组试样所检项目依据" + MItem[0]["PDBZ"] + "不符合" + item["KDBH"].Trim() + "设计要求。";
                                }
                            }
                        }
                        #endregion

                        if (mbhggs == 0)
                        {
                            XQData[eachCount]["JCJG"] = "合格";
                            XQData[eachCount]["JCJGMS"] = "该组试样所检项目符合{}" + "标准要求。";
                            //MItem[0]["JSBEIZHU"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                            //MItem[0]["JCJG"] = "合格";
                        }
                        else
                        {
                            XQData[eachCount]["JCJG"] = "不合格";
                            XQData[eachCount]["JCJGMS"] = "该组试样不符合{}" + "标准要求。";
                            //MItem[0]["JSBEIZHU"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            //MItem[0]["JCJG"] = "不合格";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                err += ex.Message.Replace("'", "");
            }

            #region 添加最终报告
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            if (mbhggs > 0)
            {
                //不合格
                bgjgDic.Add("JCJG", "不合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合不标准要求。" + err);
            }
            else
            {
                bgjgDic.Add("JCJG", "合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。" + err);
            }

            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion
        }
    }
}
