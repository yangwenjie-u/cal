using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class HD2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItems = retData["S_HD"];
            var extraDJ = dataExtra["BZ_HD_DJ"];
            var extraGG = dataExtra["BZ_HDGG"];
            var extraCS = dataExtra["BZ_HDDRCS"];
            var extraFF = dataExtra["BZ_HDDRFF"];

            if (!retData.ContainsKey("M_HD"))
            {
                retData["M_HD"] = new List<IDictionary<string, string>>();
            }
            var MItem = retData["M_HD"];

            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            bool mAllHg = true;
            var mItemHg = true;
            string mSjdjbh, mSjdj = "";
            double mSjcc, mMj, mSjcc1 = 0;
            double mSz, mQdyq, mHsxs, mttjhsxs = 0;
            string mJSFF = "";
            string mlongStr = "";
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;
            int mbhggs = 0;//不合格数量
            List<double> mtmpArray = new List<double>();

            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mSjdj = sItem["SJDJ"];

                if (string.IsNullOrEmpty(mSjdj))
                {
                    sItem["SJDJ"] = "";
                }

                var mrsGg = extraGG.FirstOrDefault(u => u["MC"] == sItem["GGXH"]);

                if (null == mrsGg)
                {
                    sItem["SJCC"] = "0";
                    sItem["HSXS"] = "0";
                    jsbeizhu += "依据不详";
                    mAllHg = false;
                    sItem["JCJG"] = "不下结论";
                    continue;
                }
                sItem["SJCC"] = mrsGg["CD"];
                sItem["HSXS"] = mrsGg["HSXS"];


                var mrsdrFf = extraFF.FirstOrDefault(u => u["MC"] == sItem["DRFF"]);
                if (null == mrsdrFf)
                {
                }
                else
                {
                }

                mSjcc = Double.Parse(sItem["SJCC"]);

                if (null == sItem["SJCC1"] || sItem["SJCC1"] == "" || Double.Parse(sItem["SJCC1"]) == 0)
                    mSjcc1 = mSjcc;
                else
                    mSjcc1 = Double.Parse(sItem["SJCC1"]);

                mMj = mSjcc * mSjcc1;
                if (mMj <= 0)//试件面积<=0则不予计算
                {
                    jsbeizhu = jsbeizhu + "试件尺寸为空/r/n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }

                mHsxs = Double.Parse(sItem["HSXS"]);
                if (mHsxs <= 0)//'换算系数=0则不予计算
                {
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "换算系数为空/r/n";
                    sItem["JCJG"] = "不合格";
                    continue;
                }

                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj);

                if (null == mrsDj)
                {
                    mSz = 0;
                    mQdyq = 0;
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "设计等级为空或不存在/r/n";
                    continue;
                }

                mSz = string.IsNullOrEmpty(mrsDj["SZ"]) ? 0 : Double.Parse(mrsDj["SZ"]);
                mQdyq = string.IsNullOrEmpty(mrsDj["QDYQ"]) ? 0 : Double.Parse(mrsDj["QDYQ"]);
                mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "0" : mrsDj["JSFF"].ToLower();

                ///计算龄期
                if (DateTime.Compare(GetSafeDateTime(sItem["ZZRQ"]), GetSafeDateTime("1900-1-1")) <= 0 || DateTime.Compare(GetSafeDateTime(sItem["SYRQ"]), GetSafeDateTime("1900-1-1")) <= 0)
                {
                    sItem["LQ"] = "0";
                }
                else
                {
                    TimeSpan ts = GetSafeDateTime(sItem["SYRQ"]) - GetSafeDateTime(sItem["ZZRQ"]);
                    sItem["LQ"] = ts.Days.ToString();

                    if (ts.Days < 28 && "工地标准养护" == sItem["YHTJ"].Trim())
                    {
                        sItem["LQ"] = "28";
                        sItem["SYRQ"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(28).ToString();
                    }
                }
                double qdVal = 0;
                if (jcxm.Contains("、强度损失、") || jcxm.Contains("、抗冻试验、"))
                {
                    qdVal = 0;
                    mtmpArray.Clear();
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

                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["KYPJ28"] = Math.Round(mMidKyqd, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["KYPJ28"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["KYPJ28"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["KYPJ28"] = Math.Round(mAvgKyqd, 1).ToString();
                    }
                }
                else
                {
                    sItem["KYPJ28"] = "----";
                }
                // kyhz
                if (jcxm.Contains("、强度损失、") || jcxm.Contains("、抗冻试验、"))
                {
                    #region KYHZ
                    qdVal = 0;
                    mtmpArray.Clear();

                    for (int i = 1; i < 4; i++)
                    {
                        qdVal = Math.Round(1000 * Conversion.Val(sItem["KYHZ" + i]) / mMj, 1);
                        sItem["KYQD" + i] = qdVal.ToString();
                        mtmpArray.Add(qdVal);
                    }
                    mtmpArray.Sort();
                    mMaxKyqd = mtmpArray[2];
                    mMinKyqd = mtmpArray[0];
                    mMidKyqd = mtmpArray[1];
                    mAvgKyqd = mtmpArray.Average();

                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["KYPJ"] = Math.Round(mMidKyqd, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["KYPJ"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["KYPJ"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["KYPJ"] = Math.Round(mAvgKyqd, 1).ToString();
                    }
                    #endregion
                    #region DHHZ
                    mtmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        qdVal = Math.Round(1000 * Conversion.Val(sItem["DHHZ1_" + i]) / mMj, 1);
                        sItem["DHQD1_" + i] = qdVal.ToString();
                        mtmpArray.Add(qdVal);
                    }
                    mtmpArray.Sort();
                    mMaxKyqd = mtmpArray[2];
                    mMinKyqd = mtmpArray[0];
                    mMidKyqd = mtmpArray[1];
                    mAvgKyqd = mtmpArray.Average();

                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["DHPJ1"] = Math.Round(mMidKyqd, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["DHPJ1"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["DHPJ1"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["DHPJ1"] = Math.Round(mAvgKyqd, 1).ToString();
                    }

                    sItem["QDSSL1"] = (Math.Round(Conversion.Val(sItem["KYPJ"]) - Conversion.Val(sItem["DHPJ1"]) / Conversion.Val(sItem["KYPJ"]) * 100, 1)).ToString("0.0");
                    #endregion
                    #region KYHZ2 D25 D50
                    if ("D25" != sItem["KDBH"].Trim() && sItem["KDBH"].Trim() != "D50")
                    {
                        #region kyqd2
                        for (int i = 1; i < 4; i++)
                        {
                            qdVal = Math.Round(1000 * Conversion.Val(sItem["KYHZ2_" + i]) / mMj, 1);
                            sItem["KYQD2_" + i] = qdVal.ToString();
                            mtmpArray.Add(qdVal);
                        }
                        mtmpArray.Sort();
                        mMaxKyqd = mtmpArray[2];
                        mMinKyqd = mtmpArray[0];
                        mMidKyqd = mtmpArray[1];
                        mAvgKyqd = mtmpArray.Average();

                        if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                        {
                            sItem["KYPJ2"] = Math.Round(mMidKyqd, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                        {
                            sItem["KYPJ2"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                        {
                            sItem["KYPJ2"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                        {
                            sItem["KYPJ2"] = Math.Round(mAvgKyqd, 1).ToString();
                        }

                        #endregion

                        #region DHHZ2
                        for (int i = 1; i < 4; i++)
                        {
                            qdVal = Math.Round(1000 * Conversion.Val(sItem["DHHZ2_" + i]) / mMj, 1);
                            sItem["DHQD2_" + i] = qdVal.ToString();
                            mtmpArray.Add(qdVal);
                        }
                        mtmpArray.Sort();
                        mMaxKyqd = mtmpArray[2];
                        mMinKyqd = mtmpArray[0];
                        mMidKyqd = mtmpArray[1];
                        mAvgKyqd = mtmpArray.Average();

                        if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                        {
                            sItem["DHPJ2"] = Math.Round(mMidKyqd, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                        {
                            sItem["DHPJ2"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                        {
                            sItem["DHPJ2"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                        }
                        if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                        {
                            sItem["DHPJ2"] = Math.Round(mAvgKyqd, 1).ToString();
                        }

                        sItem["QDSSL2"] = (Math.Round(Conversion.Val(sItem["KYPJ2"]) - Conversion.Val(sItem["DHPJ2"]) / Conversion.Val(sItem["KYPJ2"]) * 100, 1)).ToString("0.0");
                        #endregion
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

                if (jcxm.Contains("、质量损失、") || jcxm.Contains("、抗冻试验、"))
                {

                    #region 质量损失
                    qdVal = 0;
                    mtmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["DQZL2_" + i] = (sItem["DQZL2_" + i] == "" || Double.Parse(sItem["DQZL2_" + i]) == 0) ? sItem["DQZL" + i] : sItem["DQZL2_" + i];
                        sItem["ZLSSL1_" + i] = Math.Round((Double.Parse(sItem["DQZL" + i]) - GetSafeDouble(sItem["DHZL1_" + i])) / GetSafeDouble(sItem["DQZL" + i]) * 100, 2).ToString("0.00");
                        qdVal = Double.Parse(sItem["ZLSSL1_" + i]) < 0 ? 0 : Double.Parse(sItem["ZLSSL1_" + i]);


                        mtmpArray.Add(qdVal);
                    }
                    mtmpArray.Sort();
                    mMaxKyqd = mtmpArray[2];
                    mMinKyqd = mtmpArray[0];
                    mMidKyqd = mtmpArray[1];
                    mAvgKyqd = mtmpArray.Average();

                    if ((mMaxKyqd - mMidKyqd) > 1 && (mMidKyqd - mMinKyqd) > 1)
                    {
                        sItem["ZLSSL1"] = Math.Round(mMidKyqd, 1).ToString("0.0");
                    }
                    if ((mMaxKyqd - mMidKyqd) > 1 && (mMidKyqd - mMinKyqd) <= 1)
                    {
                        sItem["ZLSSL1"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString("0.0");
                    }
                    if ((mMaxKyqd - mMidKyqd) <= 1 && (mMidKyqd - mMinKyqd) > 1)
                    {
                        sItem["ZLSSL1"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString("0.0");
                    }
                    if ((mMaxKyqd - mMidKyqd) <= 1 && (mMidKyqd - mMinKyqd) <= 1)
                    {
                        sItem["ZLSSL1"] = Math.Round(mAvgKyqd, 1).ToString("0.0");
                    }

                    mtmpArray.Clear();
                    if ("D25" != sItem["KDBH"].Trim() && sItem["KDBH"].Trim() != "D50" && sItem["KDBH"].Contains("D"))
                    {
                        #region ZLSSL2
                        for (int i = 1; i < 4; i++)
                        {
                            qdVal = Math.Round((Double.Parse(sItem["DQZL2_" + i]) - GetSafeDouble(sItem["DHZL2_" + i])) / GetSafeDouble(sItem["DQZL2_" + i]) * 100, 2);
                            sItem["ZLSSL2_" + i] = qdVal.ToString("0.00");
                            qdVal = Double.Parse(sItem["ZLSSL2_" + i]) < 0 ? 0 : Double.Parse(sItem["ZLSSL2_" + i]);
                            mtmpArray.Add(qdVal);
                        }
                        mtmpArray.Sort();
                        mMaxKyqd = mtmpArray[2];
                        mMinKyqd = mtmpArray[0];
                        mMidKyqd = mtmpArray[1];
                        mAvgKyqd = mtmpArray.Average();

                        if ((mMaxKyqd - mMidKyqd) > 1 && (mMidKyqd - mMinKyqd) > 1)
                        {
                            sItem["ZLSSL2"] = Math.Round(mMidKyqd, 1).ToString("0.0");
                        }
                        if ((mMaxKyqd - mMidKyqd) > 1 && (mMidKyqd - mMinKyqd) <= 1)
                        {
                            sItem["ZLSSL2"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString("0.0");
                        }
                        if ((mMaxKyqd - mMidKyqd) <= 1 && (mMidKyqd - mMinKyqd) > 1)
                        {
                            sItem["ZLSSL2"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString("0.0");
                        }
                        if ((mMaxKyqd - mMidKyqd) <= 1 && (mMidKyqd - mMinKyqd) <= 1)
                        {
                            sItem["ZLSSL2"] = Math.Round(mAvgKyqd, 1).ToString("0.0");
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

                if (jcxm.Contains("、相对动弹性模量、"))
                {
                    #region XDDTXML1
                    qdVal = 0;
                    mtmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        qdVal = Math.Round((Conversion.Val(sItem["HJDHZ1_" + i]) * Conversion.Val(sItem["HJDHZ1_" + i])) / (Conversion.Val(sItem["HJCZ" + i]) * Conversion.Val(sItem["HJCZ" + i])) * 100, 1);

                        sItem["XDDTXML1_" + i] = qdVal.ToString("0.0");
                        mtmpArray.Add(qdVal);
                    }
                    mtmpArray.Sort();
                    mMaxKyqd = mtmpArray[2];
                    mMinKyqd = mtmpArray[0];
                    mMidKyqd = mtmpArray[1];
                    mAvgKyqd = mtmpArray.Average();

                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["XDDTXML1"] = Math.Round(mMidKyqd, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["XDDTXML1"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["XDDTXML1"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["XDDTXML1"] = Math.Round(mAvgKyqd, 1).ToString();
                    }
                    #endregion

                    #region XDDTXML2
                    qdVal = 0;
                    mtmpArray.Clear();
                    for (int i = 1; i < 4; i++)
                    {
                        qdVal = Math.Round((GetSafeDouble(sItem["HJDHZ2_" + i]) * GetSafeDouble(sItem["HJDHZ2_" + i])) / (GetSafeDouble(sItem["HJCZ" + i]) * GetSafeDouble(sItem["HJCZ" + i])) * 100, 1);
                        sItem["XDDTXML2_" + i] = qdVal.ToString("0.0");
                        mtmpArray.Add(qdVal);
                    }
                    mtmpArray.Sort();
                    mMaxKyqd = mtmpArray[2];
                    mMinKyqd = mtmpArray[0];
                    mMidKyqd = mtmpArray[1];
                    mAvgKyqd = mtmpArray.Average();

                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["XDDTXML2"] = Math.Round(mMidKyqd, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["XDDTXML2"] = Math.Round((mMidKyqd + mMinKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["XDDTXML2"] = Math.Round((mMaxKyqd + mMidKyqd) / 2, 1).ToString();
                    }
                    if ((mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1)) && (mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1)))
                    {
                        sItem["XDDTXML2"] = Math.Round(mAvgKyqd, 1).ToString();
                    }
                    #endregion
                }

                #region 快冻法
                if (sItem["DRFF"] == "快冻法")
                {
                    #region old vb版本有错误，快冻法只做一组质量损失
                    ////MItem[0]["WHICH"] = "0";
                    //sItem["KDYQ"] = "相对动弹性模量应" + mrsdrFf["XDTXML"] + "% 或质量损失应" + mrsdrFf["ZLSSL"] + "%。";
                    //if (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL1"], true) == "符合" && IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML1"], true) == "符合")
                    //{
                    //    sItem["JCJG"] = "合格";
                    //    //jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "" + sItem["KDBH"].Trim() + "设计要求。";
                    //    jsbeizhu = "依据" + MItem[0]["PDBZ"] + sItem["KDBH"].Trim() + "的规定，所检项目均符合要求。";
                    //}

                    //if ((IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL1"], true) == "符合" && IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML1"], true) == "符合") && (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL2"], true) == "不符合" || IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML2"], true) == "不符合") && GetSafeDouble(sItem["DRCS2"]) >= GetSafeDouble(sItem["DRCS"]))
                    //{
                    //    sItem["JCJG"] = "合格";
                    //    string mkdbh = "";
                    //    var extraFieldsDj = extraCS.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(sItem["DRFF"].Trim()) && u.Keys.Contains("KDCS") && u.Values.Contains(sItem["DRCS1"].Trim()));
                    //    if (null == extraFieldsDj)
                    //    {
                    //        mkdbh = extraFieldsDj["KDBH"];
                    //    }
                    //    //jsbeizhu = "该试件抗冻标号为：" + mkdbh.Trim() + ",不符合设计标号" + sItem["KDBH"] + "设计要求。";
                    //    jsbeizhu = "依据设计标号" + sItem["KDBH"] + "的规定，该试件抗冻标号为：" + mkdbh.Trim() + "不符合要求。";
                    //}

                    //if (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL1"]) == "不合格" || IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL2"]) == "不合格")
                    //{
                    //    jcxmCur = "质量损失";
                    //    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    //    sItem["GH_ZLSS"] = "不合格";
                    //}
                    //else
                    //{
                    //    sItem["GH_ZLSS"] = "合格";
                    //}

                    //if (IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML1"]) == "不合格" || IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML2"]) == "不合格")
                    //{
                    //    jcxmCur = "相对动弹性模量";
                    //    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    //    sItem["GH_DTML"] = "不合格";
                    //}
                    //else
                    //{
                    //    sItem["GH_DTML"] = "合格";
                    //}

                    //if ((IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL1"]) == "不合格" || IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML1"]) == "不合格") && (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL2"]) == "不合格" || IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML2"]) == "不合格"))
                    //{
                    //    mItemHg = false;
                    //    //jsbeizhu = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "" + sItem["KDBH"].Trim() + "设计要求。";
                    //    jsbeizhu = "依据" + MItem[0]["PDBZ"] + sItem["KDBH"].Trim() + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                    //}
                    //else
                    //{
                    //    //jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "" + sItem["KDBH"].Trim() + "设计要求。";
                    //    jsbeizhu = "依据" + MItem[0]["PDBZ"] + sItem["KDBH"].Trim() + "的规定，所检项目均符合要求。";
                    //}
                    #endregion

                    sItem["KDYQ"] = "相对动弹性模量应" + mrsdrFf["XDTXML"] + "% 或质量损失应" + mrsdrFf["ZLSSL"] + "%。";
                    if (IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML1"]) == "不合格" || IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML2"]) == "不合格")
                    {
                        jcxmCur = "相对动弹性模量";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_DTML"] = "不合格";
                    }
                    else
                    {
                        sItem["GH_DTML"] = "合格";
                    }

                    if (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL1"]) == "不合格")
                    {
                        jcxmCur = "质量损失";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_ZLSS"] = "不合格";
                    }
                    else
                    {
                        sItem["GH_ZLSS"] = "合格";
                    }

                    if (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL1"], true) == "符合" && IsQualified(mrsdrFf["XDTXML"], sItem["XDDTXML1"], true) == "符合")
                    {
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + sItem["KDBH"].Trim() + "的规定，所检项目均符合要求。";
                    }
                    else
                    {
                        mItemHg = false;
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + sItem["KDBH"].Trim() + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                    }
                }
                #endregion

                #region 慢冻法
                if (sItem["DRFF"] == "慢冻法")
                {
                    sItem["KDYQ"] = "抗压强度损失率应" + mrsdrFf["QDSSL"] + "% 质量损失应" + mrsdrFf["ZLSSL"] + "%。";

                    if (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL1"], false) == "不合格" || (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL2"], false) == "不合格" && sItem["KDBH"].Trim() != "D25" && sItem["KDBH"].Trim() != "D50"))
                    {
                        jcxmCur = "质量损失";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                    if (IsQualified(mrsdrFf["QDSSL"], sItem["QDSSL1"], false) == "不合格" || (IsQualified(mrsdrFf["QDSSL"], sItem["QDSSL2"], false) == "不合格" && sItem["KDBH"].Trim() != "D25" && sItem["KDBH"].Trim() != "D50"))
                    {
                        jcxmCur = "强度损失";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                    if (sItem["KDBH"].Trim() != "D25" && sItem["KDBH"].Trim() != "D50")
                    {
                        if (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL1"]) == "合格" && IsQualified(mrsdrFf["QDSSL"], sItem["QDSSL1"]) == "合格" && IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL2"]) == "合格" && IsQualified(mrsdrFf["QDSSL"], sItem["QDSSL2"]) == "合格" && GetSafeDouble(sItem["DRCS2"]) >= GetSafeDouble(sItem["DRCS"]) - 50)
                        {
                            sItem["JCJG"] = "合格";
                            //jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "" + sItem["KDBH"].Trim() + "设计要求。";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + sItem["KDBH"].Trim() + "的规定，所检项目均符合要求。";
                        }
                        else
                        {
                            mItemHg = false;
                            //jsbeizhu = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "" + sItem["KDBH"].Trim() + "设计要求。";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + sItem["KDBH"].Trim() + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                        }
                    }
                    else
                    {
                        sItem["DRCS2"] = "----";
                        if (IsQualified(mrsdrFf["ZLSSL"], sItem["ZLSSL1"]) == "符合" && IsQualified(mrsdrFf["QDSSL"], sItem["QDSSL1"]) == "符合")
                        {
                            //jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "" + sItem["KDBH"].Trim() + "设计要求。";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + sItem["KDBH"].Trim() + "的规定，所检项目均符合要求。";
                        }
                        else
                        {
                            mItemHg = false;
                            //jsbeizhu = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "" + sItem["KDBH"].Trim() + "设计要求。";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + sItem["KDBH"].Trim() + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                        }
                    }
                }
                #endregion

                if (mItemHg)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
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
            /************************ 代码结束 *********************/
        }
    }
}
