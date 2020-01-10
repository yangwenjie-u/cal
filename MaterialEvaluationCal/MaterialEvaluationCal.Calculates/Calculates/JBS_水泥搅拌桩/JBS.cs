using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.JBS_水泥搅拌桩
{
    public  class JBS: BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            var jcxm_keys = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            foreach (var jcxmitem in jcxm_keys)
            {
                var s_jbstab = retData[jcxmitem]["S_JBS"];
                var s_rwtab = retData[jcxmitem]["S_BY_RW_XQ"];
                var bz_jbgg = dataExtra["BZ_JBGG"];
                var bz_jbs = dataExtra["BZ_JBS_DJ"];
                var m_jbstab = retData[jcxmitem]["M_JBS"];
                int rw = 0;
                foreach (var item in s_jbstab)
                {
                    #region   公共区域
                    string mSjdj = item["SJDJ"];  //设计等级
                    if (string.IsNullOrEmpty(mSjdj))
                        mSjdj = "";
                    var mrsgg = bz_jbgg.FirstOrDefault(u => u.Values.Contains(item["GGXH"]));  //根据尺寸规格取帮助表数据
                    if (mrsgg != null && mrsgg.Count > 0)
                    {
                        item["SJCC"] = mrsgg["CD"];   //受压面边长1
                        item["HSXS"] = mrsgg["HSXS"];  //换算系数
                    }
                    else
                    {
                        item["SJCC"] = "0";
                        item["HSXS"] = "0";
                        m_jbstab[rw]["JSBEIZHU"] = m_jbstab[rw]["JSBEIZHU"] + "规格不祥。";
                        m_jbstab[rw]["BGBH"] = "";
                    }
                    string mSjcc = item["SJCC"];
                    string mSjcc1 = string.Empty;
                    if (string.IsNullOrEmpty(item["SJCC1"]) || GetSafeDouble(item["SJCC"]) == 0)
                        mSjcc1 = mSjcc;
                    else
                        mSjcc1 = item["SJCC1"];
                    if (item["ggxh"].Contains("Φ"))
                        m_jbstab[rw]["WHICH"] = "1";
                    double mMj = GetSafeDouble(mSjcc) * GetSafeDouble(mSjcc1);  //求面积
                    if (mMj < 0)
                    {
                        m_jbstab[rw]["JSBEIZHU"] = m_jbstab[rw]["JSBEIZHU"] + "单组流水号:" + item["DZBH"] + "试件尺寸为空";
                        m_jbstab[rw]["BGBH"] = "";
                    }
                    int mHsxs = 1;

                    var mrsDj = bz_jbs.FirstOrDefault(u => u.Values.Contains(mSjdj) && u.Values.Contains(item["YPMC"]));
                    double mSz = 0; //计算数值
                    double mQdyq = 0;   //强度标准
                    string mJSFF = string.Empty;  //计算方法
                    if (mrsDj != null && mrsDj.Count > 0)
                    {
                        mSz = GetSafeDouble(mrsDj["SZ"]);
                        mQdyq = GetSafeDouble(mrsDj["QDYQ"]);
                        mJSFF = mrsDj["JSFF"].Trim();
                    }
                    else
                    {
                        mSjdj = mSjdj.Replace("MPa", "").Replace("Mpa", "").Replace("MPA", "");
                        if (IsNumeric(mSjdj))
                            mSz = GetSafeDouble(mSjdj);
                    }

                    //计算龄期
                    if (DateTime.Compare(DateTime.Parse(item["ZZRQ"]), DateTime.Parse("1900-1-1")) <= 0)
                        item["LQ"] = "0";
                    else
                        item["LQ"] = (GetSafeDateTime(m_jbstab[rw]["SYRD"]) - GetSafeDateTime(item["zzrq"])).Days.ToString();
                        //item["LQ"] =  item["SYRQ"] - item["ZZRQ"]; //少了字段暂时不处理
                    double mMj1;
                    double mMj2;
                    double mMj3;
                    //求抗拉强度
                    if (item["GGXH"].IndexOf("Φ") > -1)
                    {
                        mMj1 = Math.Round((double)3.14159 * (GetSafeDouble(item["ZJ1"]) / 2) * (GetSafeDouble(item["ZJ1"]) / 2), 0);
                        mMj2 = Math.Round((double)3.14159 * (GetSafeDouble(item["ZJ2"]) / 2) * (GetSafeDouble(item["ZJ2"]) / 2), 0);
                        mMj3 = Math.Round((double)3.14159 * (GetSafeDouble(item["ZJ3"]) / 2) * (GetSafeDouble(item["ZJ3"]) / 2), 0);
                        item["KYQD1"] = Math.Round(1000 * (GetSafeDouble(item["KYHZ1"]) / mMj1), 2).ToString("0.00");
                        item["KYQD2"] = Math.Round(1000 * (GetSafeDouble(item["KYHZ2"]) / mMj2), 2).ToString("0.00");
                        item["KYQD3"] = Math.Round(1000 * (GetSafeDouble(item["KYHZ3"]) / mMj3), 2).ToString("0.00");
                    }
                    else
                    {
                        item["KYQD1"] = Math.Round(1000 * (GetSafeDouble(item["KYHZ1"]) / mMj), 2).ToString("0.00");
                        item["KYQD2"] = Math.Round(1000 * (GetSafeDouble(item["KYHZ2"]) / mMj), 2).ToString("0.00");
                        item["KYQD3"] = Math.Round(1000 * (GetSafeDouble(item["KYHZ3"]) / mMj), 2).ToString("0.00");
                    }
                    string mlongStr = item["KYQD1"] + "," + item["KYQD2"] + "," + item["KYQD3"];
                    string[] mkyqdArray = new string[3];
                    string[] mtmpArray = mlongStr.Split(',');
                    mtmpArray.CopyTo(mkyqdArray, 0);
                    Array.Sort(mkyqdArray);
                    double mMaxKyqd = GetSafeDouble(mkyqdArray[2]);  //最大抗压强度
                    double mMinKyqd = GetSafeDouble(mkyqdArray[0]);  //最小抗压强度
                    double mMidKyqd = GetSafeDouble(mkyqdArray[1]);  //中间值抗压强度
                    //求数组的平均值
                    double mSum = 0;
                    for (int i = 0; i < mkyqdArray.Length; i++)
                    {
                        mSum += GetSafeDouble(mkyqdArray[i]);
                    }
                    double mAvgKyqd = mSum / mkyqdArray.Length;   //平均抗压强度
                    //计算抗压平均、达到设计强度、及进行单组合格判定
                    if (mMidKyqd != 0)
                    {
                        if ((mMaxKyqd - mMidKyqd) > mMidKyqd * (double)0.15 && (mMidKyqd - mMinKyqd) > mMidKyqd * (double)0.15)
                        {
                            item["KYPJ"] = "0";
                            item["DDSJQD"] = "0";
                            item["HZCASE"] = "1";
                            item["JCJG"] = "不合格";
                            s_rwtab[rw]["JCJG"] = "不符合";
                            s_rwtab[rw]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                        if ((mMaxKyqd - mMidKyqd) > mMidKyqd * (double)0.15 && (mMidKyqd - mMinKyqd) <= mMidKyqd * (double)0.15)
                        {
                            item["HZCASE"] = "2";
                            if (GetSafeDouble(mSjcc) != 0 && GetSafeDouble(mSjcc1) != 0)
                            {
                                item["KYPJ"] = Math.Round((mMidKyqd * 1 * 1), 1).ToString();
                            }
                            if (mSz != 0)
                            {
                                item["DDSJQD"] = Math.Round(GetSafeDouble(item["KYPJ"]) / mSz * 100, 0).ToString(); //达到设计强度
                                if (GetSafeDouble(item["DDSJQD"]) > mQdyq)
                                {
                                    item["JCJG"] = "合格";
                                    s_rwtab[rw]["JCJG"] = "符合";
                                    s_rwtab[rw]["JCJGMS"] = "所检测项目符合标准要求";
                                }
                                else
                                {
                                    item["JCJG"] = "不合格";
                                    s_rwtab[rw]["JCJG"] = "不符合";
                                    s_rwtab[rw]["JCJGMS"] = "所检测项目不符合标准要求";
                                    mAllHg = false;
                                }
                            }
                            item["MIDAVG"] = "1";
                        }
                        if ((mMaxKyqd - mMidKyqd) <= mMidKyqd * (double)0.15 && (mMidKyqd - mMinKyqd) > mMidKyqd * (double)0.15)
                        {
                            item["HZCASE"] = "3";
                            if (GetSafeDouble(mSjcc) != 0 && GetSafeDouble(mSjcc1) != 0)
                                item["KYPJ"] = Math.Round((mMidKyqd * 1 * 1), 1).ToString();
                            if (mSz != 0)
                            {
                                item["DDSJQD"] = Math.Round(GetSafeDouble(item["KYPJ"]) / mSz * 100, 0).ToString(); //达到设计强度
                            }
                            if (GetSafeDouble(item["DDSJQD"]) > mQdyq)
                            {
                                item["JCJG"] = "合格";
                                s_rwtab[rw]["JCJG"] = "符合";
                                s_rwtab[rw]["JCJGMS"] = "所检测项目符合标准要求";
                            }
                            else
                            {
                                item["JCJG"] = "不合格";
                                s_rwtab[rw]["JCJG"] = "符合";
                                s_rwtab[rw]["JCJGMS"] = "所检测项目符合标准要求";
                                mAllHg = false;
                            }
                            item["MIDAVG"] = "1";
                        }
                        if ((mMaxKyqd - mMidKyqd) <= mMidKyqd * (double)0.15 && (mMidKyqd - mMinKyqd) > mMidKyqd * (double)0.15)
                        {
                            item["HZCASE"] = "4";
                            if (item["GGXH"].IndexOf("Φ") > 0)
                                item["KYPJ"] = Math.Round((GetSafeDouble(item["KYQD1"]) + GetSafeDouble(item["KYQD2"]) + GetSafeDouble(item["KYQD3"])) / 3, 2).ToString("0.00");
                            else
                                item["KYPJ"] = Math.Round((GetSafeDouble(item["KYQD1"]) + GetSafeDouble(item["KYQD2"]) + GetSafeDouble(item["KYQD3"])) / 3, 1).ToString("0.0");
                            if (mSz != 0)
                            {
                                item["DDSJQD"] = Math.Round(GetSafeDouble(item["KYPJ"]) / mSz * 100, 0).ToString();
                                if (GetSafeDouble(item["DDSJQD"]) > mQdyq)
                                {
                                    item["JCJG"] = "合格";
                                    s_rwtab[rw]["JCJG"] = "符合";
                                    s_rwtab[rw]["JCJGMS"] = "所检测项目符合标准要求";
                                }
                                else
                                {
                                    item["JCJG"] = "不合格";
                                    s_rwtab[rw]["JCJG"] = "不符合";
                                    s_rwtab[rw]["JCJGMS"] = "所检测项目不符合标准要求";
                                    mAllHg = false;
                                }
                            }
                        }
                        //主表总判断赋值
                        if (mAllHg)
                            m_jbstab[rw]["JCJG"] = "合格";
                        else
                            m_jbstab[rw]["JCJG"] = "不合格";
                        rw++;
                    }
                    #endregion
                }
            }

            #region 更新主表检测结果
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            bgjgDic.Add("JCJG", (mAllHg ? "合格" : "不合格"));
            bgjgDic.Add("JCJGMS", $"该组试样所检项目{(mAllHg ? "" : "不")}符合{dataExtra["BZ_JBS_DJ"].FirstOrDefault()["PDBZ"]}设计要求。");
            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion

            return true;
        }
    }
}
