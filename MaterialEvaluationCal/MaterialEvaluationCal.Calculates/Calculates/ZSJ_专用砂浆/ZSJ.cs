using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.ZSJ_专用砂浆
{
    public class ZSJ : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var jcxm_keys = retData.Select(u => u.Key).ToArray();

            foreach (var jcxmitem in jcxm_keys)
            {
                var s_zsjtab = retData[jcxmitem]["S_ZSJ"];
                var s_rwtab = retData[jcxmitem]["S_BY_RW_XQ"];
                var m_zsjtab = retData[jcxmitem]["M_ZSJ"];
                //var mrsHS = dataExtra["BZ_NADXFF"];
                var mrsDj = dataExtra["BZ_ZSJ_DJ"];

                int row = 0;
                foreach (var item in s_zsjtab)
                {
                    #region  公共部分
                    string mSjlx = item["SJLX"];
                    string mSjdj = item["SJDJ"];
                    double mG_kyqd = 0;  //抗压强度要求
                    double mG_lsnjqd = 0;  //拉伸粘结强度
                    double mG_bsl = 0;  //保水率要求
                    double mG_cd = 0;   //稠度要求
                    double mG_md = 0;   //密度要求
                    double mG_fcd = 0;  //分层度要求
                    double mMj = 0;    //面积
                    bool cn_hg = true;

                    //从设计等级表中取得相应的计算数值、等级标准
                    var mrsDj_filter = m_zsjtab.FirstOrDefault(u => u.Values.Contains(mSjdj) && u.Values.Contains(mSjlx));
                    if (mrsDj_filter != null && mrsDj.Count > 0)
                    {
                        mG_kyqd = GetSafeDouble(mrsDj_filter["SZ"]);
                        mG_lsnjqd = GetSafeDouble(mrsDj_filter["LSNJQD"]);
                        item["NQD7BZ"] = mrsDj_filter["LSNJQD7D"];
                        item["NQD28BZ"] = mrsDj_filter["LSNJQD28D"];
                        item["CNSJBZ"] = mrsDj_filter["NJSJC"];
                        item["ZNSJBZ"] = mrsDj_filter["NJSJZ"];
                        item["CNSJBZ"] = mrsDj_filter["NJSJZ"];
                        item["TCKSBZ"] = mrsDj_filter["KSYL7D"];
                        item["KS7DBZ"] = mrsDj_filter["KSYL7D"];
                        item["KS28DBZ"] = mrsDj_filter["KSYL28D"];
                        item["KZBZ"] = mrsDj_filter["KZQD"];
                        item["SSLBZ"] = mrsDj_filter["SSL"];
                        mG_bsl = GetSafeDouble(mrsDj_filter["BSL"]);
                        mG_cd = GetSafeDouble(mrsDj_filter["CD"]);
                        mG_md = GetSafeDouble(mrsDj_filter["MD"]);
                        mG_fcd = GetSafeDouble(mrsDj_filter["FCD"]);
                    }
                    if (mSjlx == "混凝土小型空心砌块和混凝土砖砌筑砂浆")
                        m_zsjtab[row]["WHICH"] = "1";
                    else
                    {
                        if (m_zsjtab[row]["JCYJ"].Contains("890-2017"))
                            m_zsjtab[row]["WHICH"] = "2";
                        else
                            m_zsjtab[row]["WHICH"] = "0";
                    }
                    if (m_zsjtab[row]["PDBZ"] == "JC/T 984-2011《聚合物水泥防水砂浆》")
                        m_zsjtab[row]["WHICH"] = "3";
                    #endregion
                    if (jcxmitem.Contains("抗压强度"))
                    {
                        double mMaxKyqd = 0;
                        double mMinKyqd = 0;
                        double mMidKyqd = 0;
                        double mAvgKyqd = 0;

                        m_zsjtab[row]["G_KYQD"] = "≥" + mG_kyqd;
                        mMj = 70.7 * 70.7;
                        if (m_zsjtab[row]["PDBZ"] == "JC/T 984-2011《聚合物水泥防水砂浆》")
                        {
                            mMj = 40 * 40;
                            item["KYQD1"] = Math.Round(1000 * GetSafeDouble(item["KYHZ1"]) / mMj, 1).ToString("0.0");
                            item["KYQD2"] = Math.Round(1000 * GetSafeDouble(item["KYHZ2"]) / mMj, 1).ToString("0.0");
                            item["KYQD3"] = Math.Round(1000 * GetSafeDouble(item["KYHZ3"]) / mMj, 1).ToString("0.0");
                            item["KYQD4"] = Math.Round(1000 * GetSafeDouble(item["KYHZ4"]) / mMj, 1).ToString("0.0");
                            item["KYQD5"] = Math.Round(1000 * GetSafeDouble(item["KYHZ5"]) / mMj, 1).ToString("0.0");
                            item["KYQD6"] = Math.Round(1000 * GetSafeDouble(item["KYHZ6"]) / mMj, 1).ToString("0.0");
                            string mlongStr = item["KYQD1"] + "," + item["KYQD2"] + "," + item["KYQD3"] + "," + item["KYQD4"] + "," + item["KYQD5"] + "," + item["KYQD6"];
                            string[] mtmpArray = mlongStr.Split(',');
                            double[] mkyqdArray = new double[6];
                            for (int i = 0; i < 6; i++)
                                mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                            Array.Sort(mkyqdArray);
                            mMaxKyqd = mkyqdArray[5];
                            mMinKyqd = mkyqdArray[0];
                            mMidKyqd = mkyqdArray[3];
                            //求数组的平均值
                            double mSum = 0;
                            for (int i = 0; i < mkyqdArray.Length; i++)
                            {
                                mSum += mkyqdArray[i];
                            }
                            mAvgKyqd = Math.Round(mSum / mkyqdArray.Length, 1);
                            int mccgs = 0;
                            for (int i = 0; i < 6; i++)
                            {
                                if (Math.Abs(mkyqdArray[i] - mAvgKyqd) > (mAvgKyqd * 0.1))
                                    mccgs = mccgs + 1;
                            }
                            if (mccgs > 1)
                                mAvgKyqd = -1;
                            else
                            {
                                if (Math.Abs(mAvgKyqd - mkyqdArray[0]) >= Math.Abs(mAvgKyqd - mkyqdArray[5]))
                                {
                                    if (Math.Abs(mAvgKyqd - mkyqdArray[0]) > (mAvgKyqd * 0.1))
                                        mAvgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5]) / 5;
                                    if (Math.Abs(mAvgKyqd - mkyqdArray[1]) > (mAvgKyqd * 0.1) || Math.Abs(mAvgKyqd - mkyqdArray[5]) > (mAvgKyqd * 0.1))
                                        mAvgKyqd = -1;
                                }
                                else
                                {
                                    if (Math.Abs(mAvgKyqd - mkyqdArray[5]) > (mAvgKyqd * 0.1))
                                        mAvgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[0]) / 5;
                                    if (Math.Abs(mAvgKyqd - mkyqdArray[0]) > (mAvgKyqd * 0.1) || Math.Abs(mAvgKyqd - mkyqdArray[4]) > (mAvgKyqd * 0.1))
                                        mAvgKyqd = -1;
                                }
                            }
                        }
                        else
                        {
                            item["KYQD1"] = Math.Round(1350 * GetSafeDouble(item["KYHZ1"]) / mMj, 1).ToString("0.0");
                            item["KYQD2"] = Math.Round(1350 * GetSafeDouble(item["KYHZ2"]) / mMj, 1).ToString("0.0");
                            item["KYQD3"] = Math.Round(1350 * GetSafeDouble(item["KYHZ3"]) / mMj, 1).ToString("0.0");
                            string mlongStr = item["KYQD1"] + "," + item["KYQD2"] + "," + item["KYQD3"];

                            string[] mtmpArray = mlongStr.Split(',');
                            double[] mkyqdArray = new double[3];
                            for (int i = 0; i < 3; i++)
                                mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                            Array.Sort(mkyqdArray);
                            mMaxKyqd = mkyqdArray[2];
                            mMinKyqd = mkyqdArray[0];
                            mMidKyqd = mkyqdArray[1];
                            //求数组的平均值
                            double mSum = 0;
                            for (int i = 0; i < mkyqdArray.Length; i++)
                            {
                                mSum += mkyqdArray[i];
                            }
                            mAvgKyqd = Math.Round(mSum / mkyqdArray.Length, 1);
                        }
                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if (mMidKyqd != 0)
                        {
                            if ((mMaxKyqd - mMidKyqd) > Math.Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                item["KYPJ"] = "不作评定";
                                m_zsjtab[row]["GH_KYQD"] = "不作评定";
                                m_zsjtab[row]["JSBEIZHU"] = "最大最小强度值超出中间值的15 %,试验不作评定";
                                mAllHg = false;
                            }
                            else
                            {
                                if ((mMaxKyqd - mMidKyqd) > Math.Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Math.Round(mMidKyqd * 0.15, 1) || (mMaxKyqd - mMidKyqd) <= Math.Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Math.Round(mMidKyqd * 0.15, 1))
                                {
                                    m_zsjtab[row]["JSBEIZHU"] = "最大最小强度值其中一个超出中间值的15 %,试验结果取中间值";
                                    item["KYPJ"] = Math.Round(mMidKyqd, 1).ToString("0.0");
                                }
                                if ((mMaxKyqd - mMidKyqd) <= Math.Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Math.Round(mMidKyqd * 0.15, 1))
                                {
                                    m_zsjtab[row]["JSBEIZHU"] = "最大最小强度值均未超出中间值的15%,试验结果取平均值";
                                    item["KYPJ"] = Math.Round(mAvgKyqd, 1).ToString("0.0");
                                }
                                if (GetSafeDouble(item["KYPJ"]) >= mG_kyqd)
                                {
                                    m_zsjtab[row]["GH_KYQD"] = "符合";
                                    s_rwtab[row]["JCJG"] = "符合";
                                    s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                                }
                                else
                                {
                                    m_zsjtab[row]["GH_KYQD"] = "不符合";
                                    s_rwtab[row]["JCJG"] = "不符合";
                                    s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                                    mAllHg = false;
                                }
                            }
                        }
                        else
                        {
                            mAllHg = false;
                            m_zsjtab[row]["GH_KYQD"] = "不符合";
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                    }
                    else
                    {
                        m_zsjtab[row]["G_KYQD"] = "----";
                        item["KYPJ"] = "----";
                        m_zsjtab[row]["GH_KYQD"] = "----";
                    }
                    if (jcxmitem.Contains("保水率") || jcxmitem.Contains("保水性"))
                    {
                        m_zsjtab[row]["G_BSL"] = "≥" + mG_bsl;
                        if (GetSafeDouble(item["BSL"]) >= mG_bsl)
                        {
                            m_zsjtab[row]["GH_BSL"] = "符合";
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            m_zsjtab[row]["GH_BSL"] = "不符合";
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        m_zsjtab[row]["G_BSL"] = "----";
                        item["BSL"] = "----";
                        m_zsjtab[row]["GH_BSL"] = "----";
                    }
                    if (jcxmitem.Contains("稠度"))
                    {
                        string mCd1 = string.Empty;
                        string mCd2 = string.Empty;
                        int mPos = 0;
                        mPos = mG_cd.ToString().IndexOf('～');
                        mCd1 = mG_cd.ToString().Substring(0, mPos - 1);
                        mCd2 = mG_cd.ToString().Substring(mPos + 1, mG_cd.ToString().Length);
                        m_zsjtab[row]["G_CD"] = mG_cd.ToString();
                        if (GetSafeDouble(item["CD"]) <= GetSafeDouble(mCd2) && GetSafeDouble(item["CD"]) >= GetSafeDouble(mCd1))
                        {
                            m_zsjtab[row]["GH_CD"] = "符合";
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            m_zsjtab[row]["GH_CD"] = "不符合";
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        m_zsjtab[row]["G_CD"] = "----";
                        item["CD"] = "----";
                        m_zsjtab[row]["GH_CD"] = "----";
                    }
                    if (jcxmitem.Contains("抗折强度"))
                    { }
                    else
                    {
                        item["KZBZ"] = "----";
                        item["KZSC"] = "----";
                        item["KZPD"] = "----";
                    }
                    if (jcxmitem.Contains("收缩率"))
                    {
                    }
                    else
                    {
                        item["SSLBZ"] = "----";
                        item["SSLSC"] = "----";
                        item["SSLPD"] = "----";
                    }
                    if (jcxmitem.Contains("7d抗渗压力"))
                    {
                    }
                    else
                    {
                        item["TCKSBZ"] = "----";
                        item["TCKSSC"] = "----";
                        item["TCKSPD"] = "----";
                        item["KS7DBZ"] = "----";
                        item["KS7DSC"] = "----";
                        item["KS7DPD"] = "----";
                    }
                    if (jcxmitem.Contains("28d抗渗压力"))
                    {
                    }
                    else
                    {
                        item["KS28DBZ"] = "----";
                        item["KS28DSC"] = "----";
                        item["KS28DPD"] = "----";
                    }
                    if (jcxmitem.Contains("分层度"))
                    {
                        m_zsjtab[row]["G_FCD"] = "≤" + mG_fcd;
                        if (GetSafeDouble(item["FCD"]) <= mG_fcd)
                        {
                            m_zsjtab[row]["GH_FCD"] = "符合";
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            m_zsjtab[row]["GH_FCD"] = "不符合";
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        m_zsjtab[row]["G_FCD"] = "----";
                        item["FCD"] = "----";
                        m_zsjtab[row]["GH_FCD"] = "----";
                    }
                    if (jcxmitem.Contains("密度") || jcxmitem.Contains("干密度"))
                    {
                        if (mSjlx == "混凝土小型空心砌块和混凝土砖砌筑砂浆")
                        {
                            m_zsjtab[row]["G_MD"] = "≥" + mG_md;
                            if (GetSafeDouble(item["MD"]) >= mG_md)
                            {
                                m_zsjtab[row]["GH_MD"] = "符合";
                                s_rwtab[row]["JCJG"] = "符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                            }
                            else
                            {
                                m_zsjtab[row]["GH_MD"] = "不符合";
                                s_rwtab[row]["JCJG"] = "不符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            m_zsjtab[row]["G_MD"] = "≤" + mG_md;
                            if (GetSafeDouble(item["MD"]) <= mG_md)
                            {
                                m_zsjtab[row]["GH_MD"] = "符合";
                                s_rwtab[row]["JCJG"] = "符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                            }
                            else
                            {
                                m_zsjtab[row]["GH_MD"] = "不符合";
                                s_rwtab[row]["JCJG"] = "不符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                                mAllHg = false;
                            }
                        }
                    }
                    else
                    {
                        m_zsjtab[row]["G_MD"] = "----";
                        item["MD"] = "----";
                        m_zsjtab[row]["GH_MD"] = "----";
                    }
                    if (jcxmitem.Contains("凝结时间"))
                    {
                        if (string.IsNullOrEmpty(item["CNSJSC"]))
                            item["CNSJSC"] = "0";
                        if (GetSafeDouble(item["CNSJSC"]) >= GetSafeDouble(item["CNSJBZ"]))
                        {
                            item["CNSJPD"] = "符合";
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            item["CNSJPD"] = "不符合";
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                        if (item["CNSJSC"] == "0" || string.IsNullOrEmpty(item["CNSJSC"]))
                            item["CNSJPD"] = "----";
                    }
                    else
                    {
                        item["CNSJPD"] = "----";
                        item["CNSJBZ"] = "----";
                        item["CNSJSC"] = "----";
                        cn_hg = true;
                    }
                    if (jcxmitem.Contains("凝结时间"))
                    {
                        if (string.IsNullOrEmpty(item["ZNSJSC"]))
                            item["ZNSJSC"] = "0";
                        if (GetSafeDouble(item["ZNSJSC"]) <= GetSafeDouble(item["ZNSJBZ"]))
                        {
                            item["ZNSJPD"] = "符合";
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            item["ZNSJPD"] = "不符合";
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                        if (item["ZNSJSC"] == "0" || string.IsNullOrEmpty(item["ZNSJSC"]))
                            item["ZNSJPD"] = "----";
                    }
                    else
                    {
                        item["ZNSJPD"] = "----";
                        item["ZNSJSC"] = "----";
                    }
                    if (GetSafeDouble(item["CNSJBZ"]) == 0 && GetSafeDouble(item["ZNSJBZ"]) == 0 || item["CNSJSC"] == "0" || item["ZNSJSC"] == "0" || string.IsNullOrEmpty(item["CNSJSC"]) || string.IsNullOrEmpty(item["ZNSJSC"]))
                    {
                        item["CNSJPD"] = "----";
                        item["ZNSJPD"] = "----";
                    }
                    if (jcxmitem.Contains("粘结强度") || jcxmitem.Contains("拉伸粘结强度"))
                    {
                        bool canSetBgbh = true;
                        double mAvgNjqd = 0;
                        double mNjqdSum = 0;
                        m_zsjtab[row]["G_LSNJQD"] = "≥" + mG_lsnjqd;
                        if (GetSafeDouble(item["LSHZ7"]) == 0 || item["LSHZ7"] == "0" || item["LSHZ7"] == "----")
                        {
                            for (int i = 1; i < 7; i++)
                            {
                                item["LSQD" + i] = Math.Round(GetSafeDouble(item["LSHZ" + i]) / GetSafeDouble(item["LSCD" + i]) / GetSafeDouble(item["LSKD" + i]), 2).ToString("0.00");
                                mNjqdSum = mNjqdSum + GetSafeDouble(item["LSQD" + i]);
                                item["LSQD"] = Math.Round(mNjqdSum / 6, 2).ToString("0.00");
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 11; i++)
                            {
                                item["LSQD" + i] = Math.Round(GetSafeDouble(item["LSHZ" + i]) / GetSafeDouble(item["LSCD" + i]) / GetSafeDouble(item["LSKD" + i]), 2).ToString("0.00");
                                mNjqdSum = mNjqdSum + GetSafeDouble(item["LSQD" + i]);
                            }
                            string mlongStr = item["LSQD1"] + "," + item["LSQD2"] + "," + item["LSQD3"] + "," + item["LSQD4"] + "," + item["LSQD5"] + "," + item["LSQD6"] + "," + item["LSQD7"] + "," + item["LSQD8"] + "," + item["LSQD9"] + "," + item["LSQD10"];
                            double mSum = 0;
                            string[] mtmpArray = mlongStr.Split(',');
                            double[] mnjqdArray = new double[10];
                            for (int i = 0; i < 10; i++)
                            {
                                mnjqdArray[i] = GetSafeDouble(mtmpArray[i]);
                                mSum = mSum + mnjqdArray[i];
                            }
                            Array.Sort(mnjqdArray);
                            double mAvg = Math.Round(mSum / 10, 2);
                            double mMax = mnjqdArray[9];
                            double mMin = mnjqdArray[0];
                            int curMax = 9;
                            int curMin = 0;
                            int rr = 0;
                            for (int i = 0; i < 5; i++)
                            {
                                rr = i;
                                if (Math.Abs(mMin - mAvg) >= Math.Abs(mMax - mAvg))
                                {
                                    if (Math.Round(100 * Math.Abs(mMin - mAvg) / mAvg, 0) > 20)
                                    {
                                        mSum = mSum - mnjqdArray[curMin];
                                        curMin = curMin + 1;
                                        mMin = mnjqdArray[curMin];
                                        mAvg = Math.Round(mSum / (9 - i), 2);
                                    }
                                    else
                                        break;
                                }
                                else
                                {
                                    if (Math.Round(100 * Math.Abs(mMax - mAvg) / mAvg, 0) > 20)
                                    {
                                        mSum = mSum - mnjqdArray[curMax];
                                        curMax = curMax - 1;
                                        mMax = mnjqdArray[curMax];
                                        mAvg = Math.Round(mSum / (9 - i), 2);
                                    }
                                    else
                                        break;
                                }
                            }
                            if (rr <= 4)
                                item["LSQD"] = mAvg.ToString("0.00");
                            else
                            {
                                item["LSQD"] = "结果无效";
                                canSetBgbh = false;
                            }
                        }
                        if (GetSafeDouble(item["LSQD"]) >= mG_lsnjqd)
                        {
                            m_zsjtab[row]["GH_LSNJQD"] = "符合";
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            m_zsjtab[row]["GH_LSNJQD"] = "不符合";
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        m_zsjtab[row]["G_LSNJQD"] = "----";
                        item["LSQD"] = "----";
                        m_zsjtab[row]["GH_LSNJQD"] = "----";
                    }
                    if (jcxmitem.Contains("7d拉伸粘结强度"))
                    {
                    }
                    else
                    {
                        item["NQD7BZ"] = "----";
                        item["NQD7SC"] = "----";
                        item["NQD7PD"] = "----";
                    }
                    if (jcxmitem.Contains("28d拉伸粘结强度"))
                    { }
                    else
                    {
                        item["NQD28BZ"] = "----";
                        item["NQD28SC"] = "----";
                        item["NQD28PD"] = "----";
                    }
                    if (mAllHg)
                        item["JCJG"] = "合格";
                    else
                        item["JCJG"] = "不合格";
                    if (mAllHg)
                    {
                        m_zsjtab[row]["JCJG"] = "合格";
                        m_zsjtab[row]["JGSM"] = "该组样品所检项目符合标准要求。";
                    }
                    else
                    {
                        m_zsjtab[row]["JCJG"] = "不合格";
                        m_zsjtab[row]["JGSM"] = "该组样品所检项目有不符合项。";
                    }
                }
            }
            #region 更新主表检测结果
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            bgjgDic.Add("JCJG", (mAllHg ? "合格" : "不合格"));
            bgjgDic.Add("JCJGMS", $"该组试样所检项目{(mAllHg ? "" : "不")}符合标准要求。");
            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion
            return mAllHg;
            /************************ 代码结束 *********************/
        }
    }
}
