using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.HPB_混凝土配合比
{
    public class HPB : BaseMethods
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
                var s_hpbtab = retData[jcxmitem]["S_HPB"];
                var s_rwtab = retData[jcxmitem]["S_BY_RW_XQ"];
                var m_hpbtab = retData[jcxmitem]["M_HPB"];
                int row = 0;
                foreach (var item in s_hpbtab)
                {
                    #region   公共部分

                    string mSjdj = item["SJDJ"];  //设计等级名称
                    if (string.IsNullOrEmpty(item["SJCC"]) || GetSafeDouble(item["SJCC"]) == 0)
                        item["SJCC"] = "150"; //受压面边长1（MM）
                    if (string.IsNullOrEmpty(item["SJCC1"]))
                        item["SJCC1"] = "150"; //受压面边长2（MM）
                    double mSjcc = GetSafeDouble(item["SJCC"]);
                    double mSjcc1 = GetSafeDouble(item["SJCC1"]);
                    if (mSjcc1 == 0 || string.IsNullOrEmpty(mSjcc1.ToString()))
                        mSjcc1 = mSjcc;
                    double mMj = mSjcc * mSjcc1;
                    item["HSXS"] = "1"; //换算系数
                    if (mSjcc == 100)
                        item["HSXS"] = "0.95";
                    if (mSjcc == 150)
                        item["HSXS"] = "1";
                    if (mSjcc == 200)
                        item["HSXS"] = "1.05";
                    double mHsxs = GetSafeDouble(item["HSXS"]);
                    if (string.IsNullOrEmpty(mSjdj))
                        mSjdj = "";
                    //从设计等级表中取得相应的计算数值、等级标准
                    var fieldsExtra = dataExtra["BZ_HPB_DJ"].FirstOrDefault(u => u.ContainsKey("MC") && u.Values.Any(p => p.Contains(mSjdj)));
                    //IDictionary<string, string> fieldsExtra = new Dictionary<string, string>();
                    //IList<IDictionary<string,string>> list_dic = dataExtra["BZ_HPB_DJ"];
                    //int a = 0;
                    //foreach (IDictionary<string, string> item1 in list_dic)
                    //{
                    //    if (a == 1)
                    //        break;
                    //    foreach (var item2 in item1)
                    //    {
                    //        if (item2.Value.Trim().Equals(mSjdj))
                    //        {
                    //            fieldsExtra = list_dic.FirstOrDefault(x => x.Values.Contains(item2.Value));
                    //            a = 1;
                    //            break;
                    //        }
                    //    }
                    //}
                    if (fieldsExtra.Count <= 0)
                    {
                        item["JCJG"] = "依据不详";
                        break;
                    }
                    item["LQ"] = (DateTime.Parse(m_hpbtab[row]["SYRQ"]) - DateTime.Parse(item["SYRQ"])).Days.ToString();
                    #endregion
                    if (jcxmitem == "配合比")
                    {
                        m_hpbtab[row]["T_CLSN"] = (GetSafeDouble(m_hpbtab[row]["YSL"]) / GetSafeDouble(m_hpbtab[row]["SHB"])).ToString();  //调整后水泥（KG/M3） = 用水量 / 水灰比
                        m_hpbtab[row]["T_CLS"] = m_hpbtab[row]["YSL"];  //调整后水（KG/M3） = 用水量
                        m_hpbtab[row]["T_CLCHL1"] = (GetSafeDouble(m_hpbtab[row]["T_CLSN"]) * GetSafeDouble(m_hpbtab[row]["CHLCL1"]) / 100).ToString();     //调整后掺合料1（KG/M3）= 调整后水泥（KG/M3） * 掺合料掺量1 / 100;
                        m_hpbtab[row]["T_CLCHL2"] = (GetSafeDouble(m_hpbtab[row]["T_CLSN"]) * GetSafeDouble(m_hpbtab[row]["CHLCL2"]) / 100).ToString();     //调整后掺合料2（KG/M3）= 调整后水泥（KG/M3） * 掺合料掺量2 / 100;
                        m_hpbtab[row]["T_CLWJJ1"] = (GetSafeDouble(m_hpbtab[row]["T_CLSN"]) * GetSafeDouble(m_hpbtab[row]["WJJCL1"]) / 100).ToString();     //调整后外加剂1（KG/M3）= 调整后水泥（KG/M3） * 外加剂掺量1 / 100;
                        m_hpbtab[row]["T_CLWJJ2"] = (GetSafeDouble(m_hpbtab[row]["T_CLSN"]) * GetSafeDouble(m_hpbtab[row]["WJJCL2"]) / 100).ToString();     //调整后外加剂1（KG/M3）= 调整后水泥（KG/M3） * 外加剂掺量1 / 100;
                        if (GetSafeDouble(m_hpbtab[row]["T_CLSN"]) == 0)
                            m_hpbtab[row]["T_CLSN"] = "1";
                        if (m_hpbtab[row]["CHL_LX"] == "1" && m_hpbtab[row]["WJJ_LX"] == "1")
                            m_hpbtab[row]["SNADD"] = (GetSafeDouble(m_hpbtab[row]["T_CLCHL1"]) + GetSafeDouble(m_hpbtab[row]["mrsmainTable!T_CLCHL2"]) + GetSafeDouble(m_hpbtab[row]["T_CLWJJ1"]) + GetSafeDouble(m_hpbtab[row]["T_CLWJJ2"])).ToString();  //临时字段（用于VALIDPROC计算）
                        if (m_hpbtab[row]["CHL_LX"] == "1" && m_hpbtab[row]["WJJ_LX"] == "0")
                            m_hpbtab[row]["SNADD"] = m_hpbtab[row]["T_CLCHL1"] + m_hpbtab[row]["mrsmainTable!T_CLCHL2"];
                        if (m_hpbtab[row]["CHL_LX"] == "0" && m_hpbtab[row]["WJJ_LX"] == "1")
                            m_hpbtab[row]["SNADD"] = m_hpbtab[row]["T_CLCHL1"] + m_hpbtab[row]["mrsmainTable!T_CLCHL2"];
                        if (m_hpbtab[row]["CHL_LX"] == "0" && m_hpbtab[row]["WJJ_LX"] == "0")
                            m_hpbtab[row]["SNADD"] = "0";
                        m_hpbtab[row]["T_CLSN"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLSN"]) - GetSafeDouble(m_hpbtab[row]["SNADD"]), 0).ToString(); //调整后水泥（KG / M3）
                        m_hpbtab[row]["T_CLSA"] = ((GetSafeDouble(m_hpbtab[row]["SL"]) / 100) * (GetSafeDouble(m_hpbtab[row]["RZ"]) - GetSafeDouble(m_hpbtab[row]["T_CLSN"]) - GetSafeDouble(m_hpbtab[row]["YSL"]) - GetSafeDouble(m_hpbtab[row]["SNADD"]))).ToString();  //调整后砂（KG/M3）
                        m_hpbtab[row]["T_CLSN"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLSN"]), 0).ToString();
                        m_hpbtab[row]["T_CLS"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLS"]), 0).ToString();
                        m_hpbtab[row]["T_CLCHL1"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLCHL1"]), 3).ToString();
                        m_hpbtab[row]["T_CLCHL2"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLCHL2"]), 3).ToString();
                        m_hpbtab[row]["T_CLWJJ1"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLWJJ1"]), 3).ToString();
                        m_hpbtab[row]["T_CLWJJ2"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLWJJ2"]), 3).ToString();
                        m_hpbtab[row]["T_CLSA"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLSA"]), 3).ToString();
                        m_hpbtab[row]["T_CLSI"] = Math.Round((GetSafeDouble(m_hpbtab[row]["RZ"]) - GetSafeDouble(m_hpbtab[row]["T_CLSN"]) - GetSafeDouble(m_hpbtab[row]["T_CLS"]) - GetSafeDouble(m_hpbtab[row]["SNADD"]) - GetSafeDouble(m_hpbtab[row]["T_CLSA"])), 0).ToString();
                        m_hpbtab[row]["T_PBSN"] = "1";
                        m_hpbtab[row]["T_PBS"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLS"]) / GetSafeDouble(m_hpbtab[row]["T_CLSN"]), 2).ToString();
                        m_hpbtab[row]["T_PBSA"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLSA"]) / GetSafeDouble(m_hpbtab[row]["T_CLSN"]), 2).ToString();
                        m_hpbtab[row]["T_PBSI"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLSI"]) / GetSafeDouble(m_hpbtab[row]["T_CLSN"]), 2).ToString();
                        m_hpbtab[row]["T_PBCHL1"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLCHL1"]) / GetSafeDouble(m_hpbtab[row]["T_CLSN"]), 4).ToString();
                        m_hpbtab[row]["T_PBCHL2"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLCHL2"]) / GetSafeDouble(m_hpbtab[row]["T_CLSN"]), 4).ToString();
                        m_hpbtab[row]["T_PBWJJ1"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLWJJ1"]) / GetSafeDouble(m_hpbtab[row]["T_CLSN"]), 4).ToString();
                        m_hpbtab[row]["T_PBWJJ2"] = Math.Round(GetSafeDouble(m_hpbtab[row]["T_CLWJJ2"]) / GetSafeDouble(m_hpbtab[row]["T_CLSN"]), 4).ToString();
                    }
                    if (jcxmitem == "7天强度")
                    {
                        //计算单组的抗压强度,并进行合格判断
                        item["KYQD1_7"] = Math.Round(1000 * GetSafeDouble(item["KYHZ1_7"]) / mMj, 1).ToString("0.0");  //7天抗压强度1
                        item["KYQD2_7"] = Math.Round(1000 * GetSafeDouble(item["KYHZ2_7"]) / mMj, 1).ToString("0.0");  //7天抗压强度2
                        item["KYQD3_7"] = Math.Round(1000 * GetSafeDouble(item["KYHZ3_7"]) / mMj, 1).ToString("0.0");  //7天抗压强度3
                        string mlongStr = item["KYQD1_7"] + "," + item["KYQD2_7"] + "," + item["KYQD3_7"];
                        string[] mtmpArray = mlongStr.Split(',');
                        double[] mkyqdArray = new double[3];
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
                            if ((mMaxKyqd - mMidKyqd) / mMidKyqd > 0.15 && (mMidKyqd - mMinKyqd) / mMidKyqd > 0.15)
                                item["KYPJ_7"] = "0";
                            if ((mMaxKyqd - mMidKyqd) / mMidKyqd > 0.15 && (mMidKyqd - mMinKyqd) / mMidKyqd <= 0.15)
                                item["KYPJ_7"] = Math.Round(mMidKyqd * mHsxs, 1).ToString();
                            if ((mMaxKyqd - mMidKyqd) / mMidKyqd <= 0.15 && (mMidKyqd - mMinKyqd) / mMidKyqd > 0.15)
                                item["KYPJ_7"] = Math.Round(mMidKyqd * mHsxs, 1).ToString();
                            if ((mMaxKyqd - mMidKyqd) / mMidKyqd <= 0.15 && (mMidKyqd - mMinKyqd) / mMidKyqd <= 0.15)
                                item["KYPJ_7"] = Math.Round((GetSafeDouble(item["KYQD1_7"]) + GetSafeDouble(item["KYQD2_7"]) + GetSafeDouble(item["KYQD3_7"])) / 3 * mHsxs, 1).ToString();
                        }
                        else
                            item["KYPJ_7"] = "-1";
                    }
                    if (jcxmitem == "28天强度")
                    {
                        //计算单组的抗压强度,并进行合格判断
                        item["KYQD1"] = Math.Round(1000 * GetSafeDouble(item["KYHZ1"]) / mMj, 1).ToString("0.0");  //7天抗压强度1
                        item["KYQD2"] = Math.Round(1000 * GetSafeDouble(item["KYHZ2"]) / mMj, 1).ToString("0.0");  //7天抗压强度2
                        item["KYQD3"] = Math.Round(1000 * GetSafeDouble(item["KYHZ3"]) / mMj, 1).ToString("0.0");  //7天抗压强度3
                        string mlongStr = item["KYQD1"] + "," + item["KYQD2"] + "," + item["KYQD3"];
                        string[] mtmpArray = mlongStr.Split(',');
                        double[] mkyqdArray = new double[3];
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
                            if ((mMaxKyqd - mMidKyqd) / mMidKyqd > 0.15 && (mMidKyqd - mMinKyqd) / mMidKyqd > 0.15)
                                item["KYPJ"] = "0";
                            if ((mMaxKyqd - mMidKyqd) / mMidKyqd > 0.15 && (mMidKyqd - mMinKyqd) / mMidKyqd <= 0.15)
                                item["KYPJ"] = Math.Round(mMidKyqd * mHsxs, 1).ToString();
                            if ((mMaxKyqd - mMidKyqd) / mMidKyqd <= 0.15 && (mMidKyqd - mMinKyqd) / mMidKyqd > 0.15)
                                item["KYPJ"] = Math.Round(mMidKyqd * mHsxs, 1).ToString();
                            if ((mMaxKyqd - mMidKyqd) / mMidKyqd <= 0.15 && (mMidKyqd - mMinKyqd) / mMidKyqd <= 0.15)
                                item["KYPJ"] = Math.Round((GetSafeDouble(item["KYQD1"]) + GetSafeDouble(item["KYQD2"]) + GetSafeDouble(item["KYQD3"])) / 3 * mHsxs, 1).ToString();
                        }
                        else
                            item["KYPJ"] = "-1";
                    }
                    if (jcxmitem == "表观密度")
                    {
                        //筒容积（L）
                        if (GetSafeDouble(item["TLJ"]) != 0)
                            item["BGMD"] = (Math.Round((1000 * (GetSafeDouble(item["THSJZZL"]) - GetSafeDouble(item["TZL"])) / GetSafeDouble(item["TLJ"])) / 10, 0) * 10).ToString();  //表观密度
                    }
                    else
                    {
                        item["TLJ"] = "0";
                        item["BGMD"] = "0";
                        item["THSJZZL"] = "0";
                        item["TZL"] = "0";
                    }
                    if (jcxmitem == "凝结时间")
                    {
                        item["CNSJ"] = (Math.Round(((GetSafeDouble(item["T1CN"]) + GetSafeDouble(item["T2CN"]) + GetSafeDouble(item["T3CN"])) / 3) / 5, 0) * 5).ToString();   //初凝时间
                        item["ZNSJ"] = (Math.Round(((GetSafeDouble(item["T1ZN"]) + GetSafeDouble(item["T2ZN"]) + GetSafeDouble(item["T3ZN"])) / 3) / 5, 0) * 5).ToString();  //终凝时间
                    }
                    else
                    {
                        item["CNSJ"] = "0";
                        item["ZNSJ"] = "0";
                        item["T1CN"] = "0";
                        item["T2CN"] = "0";
                        item["T3CN"] = "0";
                        item["T1ZN"] = "0";
                        item["T2ZN"] = "0";
                        item["T3ZN"] = "0";
                    }
                    s_rwtab[row]["JCJG"] = "符合";
                    s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                    row++;
                }
            }

            #region 更新主表检测结果
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            bgjgDic.Add("JCJG", (mAllHg ? "合格" : "不合格"));
            bgjgDic.Add("JCJGMS", $"该组试样所检项目{(mAllHg ? "" : "不")}符合{dataExtra["BZ_HPB_DJ"].FirstOrDefault()["JCYJ"]}标准要求。");
            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion

            return mAllHg;
            /************************ 代码结束 *********************/
        }
    }
}
