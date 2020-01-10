using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.SJX_砂浆
{
    public partial class JGL_70_2009 : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            /*  参考标准：
                * JGJ 70-2009 建筑砂浆基本性能试验方法.pdf
             */
            var jcxm_keys = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            foreach (var jcxmitem in jcxm_keys)
            {
                var s_sjxtab = retData[jcxmitem]["S_SJX"];
                var s_rwtab = retData[jcxmitem]["S_BY_RW_XQ"];
                int rw = 0;
                foreach (var item in s_sjxtab)
                {
                    #region  公共区域
                    string mSjdj = item["SJDJ"];  //设计等级
                    if (string.IsNullOrEmpty(mSjdj))
                        mSjdj = "";
                    double mSjcc = GetSafeDouble(item["SJCC"]);  //立方体边长1
                    double mSjcc1 = GetSafeDouble(item["SJCC1"]);  //立方体边长2
                    if (mSjcc1 == 0 || string.IsNullOrEmpty(mSjcc1.ToString()))
                        mSjcc1 = mSjcc;
                    double mMj = mSjcc * mSjcc1;
                    if (mMj < 0)
                    {
                        item["JCJG"] = "不合格";
                        s_rwtab[rw]["JCJG"] = "不符合";
                        s_rwtab[rw]["JCJGMS"] = item["DZBH"] + "试样尺寸为空";
                        break;
                    }
                    var fieldsExtra = dataExtra["BZ_SJX_DJ"].FirstOrDefault(u => u.Values.Contains(mSjdj)); //从设计等级表中取得相应的计算数值、等级标准
                    double mSz = 0;
                    double mQdyq = 0;
                    string mJSFF = "";
                    if (fieldsExtra != null && fieldsExtra.Count > 0)
                    {
                        mSz = String.IsNullOrEmpty(fieldsExtra["SZ"]) ? 0 : GetSafeDouble(fieldsExtra["SZ"]);  //计算数值
                        mQdyq = String.IsNullOrEmpty(fieldsExtra["QDYQ"]) ? 0 : GetSafeDouble(fieldsExtra["QDYQ"]);  //达到要求
                        mJSFF = String.IsNullOrEmpty(fieldsExtra["JSFF"]) ? "" : fieldsExtra["JSFF"];  //计算方法
                    }
                    else
                    {
                        item["JCJG"] = "依据不详";
                        s_rwtab[rw]["JCJG"] = "依据不详";
                        s_rwtab[rw]["JCJGMS"] = item["DZBH"] + "等级等级为空或不存在";
                        break;
                    }
                    //计算龄期
                    if (int.Parse(item["LQ"]) < 28 && item["YHTJ"] == "标准")
                    {
                        item["LQ"] = "28";
                        item["SYRQ"] = DateTime.Parse(item["ZZRQ"]).AddDays(double.Parse(item["LQ"])).ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    //抗压强度
                    item["KYQD1"] = Math.Round(1350 * GetSafeDouble(item["KYHZ1"]) / mMj, 1).ToString("0.0");
                    item["KYQD2"] = Math.Round(1350 * GetSafeDouble(item["KYHZ2"]) / mMj, 1).ToString("0.0");
                    item["KYQD3"] = Math.Round(1350 * GetSafeDouble(item["KYHZ3"]) / mMj, 1).ToString("0.0");
                    string mlongStr = item["KYQD1"] + "," + item["KYQD2"] + "," + item["KYQD3"];
                    double[] mkyqdArray = new double[3];
                    string[] mtmpArray = mlongStr.Split(',');
                    for (int i = 0; i < 3; i++)
                        mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                    Array.Sort(mkyqdArray);
                    double mMaxKyqd = mkyqdArray[2];
                    double mMinKyqd = mkyqdArray[0];
                    double mMidKyqd = mkyqdArray[1];
                    double mAvgKyqd;
                    //求数组的平均值
                    double mSum = 0;
                    for (int i = 0; i < mkyqdArray.Length; i++)
                    {
                        mSum += mkyqdArray[i];
                    }
                    mAvgKyqd = mSum / mkyqdArray.Length;
                    //计算抗压平均、达到设计强度、及进行单组合格判定
                    if (mMidKyqd != 0)
                    {
                        if ((mMaxKyqd - mMidKyqd) > Math.Round(mMidKyqd * (double)0.15, 1) && (mMidKyqd - mMinKyqd) > Math.Round(mMidKyqd * (double)0.15, 1))
                        {
                            item["KYPJ"] = "不作评定";  //抗压强度平均值
                            item["DDSJQD"] = "不作评定"; //达到设计强度
                            item["HZCASE"] = "1"; //最大荷重之关系
                            item["JCJG"] = "不合格";  //检测结果（是否合格）
                            s_rwtab[rw]["JCJG"] = "不合格";
                            s_rwtab[rw]["JCJGMS"] = "最大最小强度值超出中间值的15%,试验不作评定";
                        }
                        if ((mMaxKyqd - mMidKyqd) > Math.Round(mMidKyqd * (double)0.15, 1) && (mMidKyqd - mMinKyqd) <= Math.Round(mMidKyqd * (double)0.15, 1))
                        {
                            item["HZCASE"] = "2"; //最大荷重之关系
                            s_rwtab[rw]["JCJGMS"] = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                            if (mSjcc != 0 && mSjcc1 != 0)
                                item["KYPJ"] = Math.Round(mMidKyqd, 1).ToString("0.0");
                            if (mSz != 0)
                            {
                                item["DDSJQD"] = Math.Round((GetSafeDouble(item["KYPJ"]) / mSz) * 100, 0).ToString();
                                if (GetSafeDouble(item["DDSJQD"]) > mQdyq)
                                {
                                    item["JCJG"] = "合格";
                                    s_rwtab[rw]["JCJG"] = "合格";
                                    s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["DDSJQD"] + ";达到要求：" + mQdyq + ";所检测项目符合标准要求";
                                }
                                else
                                {
                                    item["JCJG"] = "不合格";
                                    s_rwtab[rw]["JCJG"] = "不合格";
                                    s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["DDSJQD"] + ";达到要求：" + mQdyq + ";所检测项目不符合标准要求";
                                    mAllHg = false;
                                }
                            }
                            item["MIDAVG"] = "1";
                        }
                        if ((mMaxKyqd - mMidKyqd) <= Math.Round(mMidKyqd * (double)0.15, 1) && (mMidKyqd - mMinKyqd) > Math.Round(mMidKyqd * (double)0.15, 1))
                        {
                            item["hzcase"] = "3";
                            s_rwtab[rw]["JCJGMS"] = "最大最小强度值均未超出中间值的15%,试验结果取平均值";
                            if (mSjcc != 0 && mSjcc1 != 0)
                                item["KYPJ"] = Math.Round(mMidKyqd, 1).ToString("0.0");
                            if (mSz != 0)
                            {
                                item["DDSJQD"] = Math.Round((GetSafeDouble(item["KYPJ"]) / mSz) * 100, 0).ToString();
                                if (GetSafeDouble(item["DDSJQD"]) > mQdyq)
                                {
                                    item["JCJG"] = "合格";
                                    s_rwtab[rw]["JCJG"] = "合格";
                                    s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["DDSJQD"] + ";达到要求：" + mQdyq + ";所检测项目符合标准要求";
                                }
                                else
                                {
                                    item["JCJG"] = "不合格";
                                    s_rwtab[rw]["JCJG"] = "不合格";
                                    s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["DDSJQD"] + ";达到要求：" + mQdyq + ";所检测项目不符合标准要求";
                                    mAllHg = false;
                                }
                            }
                            item["MIDAVG"] = "1";
                        }
                        if ((mMaxKyqd - mMidKyqd) <= Math.Round(mMidKyqd * (double)0.15, 1) && (mMidKyqd - mMinKyqd) <= Math.Round(mMidKyqd * (double)0.15, 1))
                        {
                            item["hzcase"] = "4";
                            item["KYPJ"] = Math.Round(mAvgKyqd, 1).ToString("0.0");
                            if (mSz != 0)
                            {
                                item["DDSJQD"] = Math.Round((GetSafeDouble(item["KYPJ"]) / mSz) * 100, 0).ToString();
                                if (GetSafeDouble(item["DDSJQD"]) > mQdyq)
                                {
                                    item["JCJG"] = "合格";
                                    s_rwtab[rw]["JCJG"] = "合格";
                                    s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["DDSJQD"] + ";达到要求：" + mQdyq + ";所检测项目符合标准要求";
                                }
                                else
                                {
                                    item["JCJG"] = "不合格";
                                    s_rwtab[rw]["JCJG"] = "不合格";
                                    s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["DDSJQD"] + ";达到要求：" + mQdyq + ";所检测项目不符合标准要求";
                                    mAllHg = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        mAllHg = false;
                    }
                    #endregion
                    rw++;
                }
                #region 更新主表检测结果
                IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
                IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
                bgjgDic.Add("JCJG", (mAllHg ? "合格" : "不合格"));
                bgjgDic.Add("JCJGMS", $"该组试样所检项目{(mAllHg ? "" : "不")}符合{dataExtra["BZ_SJX_DJ"].FirstOrDefault()["PDBZ"]}标准要求。");
                bgjg.Add(bgjgDic);
                retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
                retData["BGJG"].Add("BGJG", bgjg);
                #endregion

            }

            return mAllHg;
            /************************ 代码结束 *********************/
        }
    }
}
