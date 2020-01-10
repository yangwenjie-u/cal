using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.SN_水泥_新_
{
    public class SN : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var jcxm_keys = retData.Select(u => u.Key).ToArray();

            foreach (var jcxmitem in jcxm_keys)
            {
                var s_sntab = retData[jcxmitem]["S_SN"];
                var s_rwtab = retData[jcxmitem]["S_BY_RW_XQ"];
                var m_sntab = retData[jcxmitem]["M_SN"];
                var mrsadx = dataExtra["BZ_NADXFF"];

                int row = 0;
                foreach (var item in s_sntab)
                {
                    #region   公共部分
                    string aaa = string.Empty; //编号
                    double mCdbz1 = 0;  //稠度标准起始
                    double mCdbz2 = 0;  //稠度标准终止
                    double mXdbz = 0;   //细度标准1
                    double mXdbz2 = 0;  //细度标准2
                    double mXdbz3 = 0;  //细度标准3
                    double mCnsj = 0;   //初凝时间
                    double mZnsj = 0;   //终凝时间
                    double mKy_3 = 0;   //抗压3天
                    double mKy_28 = 0;  //抗压28天
                    double mKy_7 = 0;   //抗压7天
                    double mKz_3 = 0;   //抗折3天
                    double mKz_7 = 0;   //抗折7天
                    double mKz_28 = 0;  //抗折28天
                    bool ky3_hg = true;  //3天抗压合格
                    bool ky28_hg = true;   //28天抗压合格
                    bool kz3_hg = true;   //3天抗折合格
                    bool kz28_hg = true;   //28天抗折合格
                    double mMj = 0;  //面积
                    double mKZPJ3z = 0;   //3天抗折强度平均
                    double mKZPJ28z = 0;  //28天抗折强度平均
                    bool adx_hg = true;   //安定性合格 
                    bool cd_hg = true;   //稠度合格
                    bool xd_hg = true;   //细度合格
                    bool cn_hg = true;   //初凝合格
                    bool zn_hg = true;   //终凝合格
                    string mDjMc = string.Empty;
                    string mSjddj = string.Empty;
                    string mjcxm = "、";
                    string mSjdj = item["SJDJ"];  //设计等级
                    if (string.IsNullOrEmpty(mSjdj))
                        mSjdj = "";
                    mjcxm = mjcxm + item["JCXM"] + "、";
                    //从设计等级表中取得相应的计算数值、等级标准
                    var fieldsExtra = dataExtra["BZ_SN_DJ"].FirstOrDefault(u => u.ContainsKey("MC") && u.Values.Any(p => p.Contains(mSjdj.Trim())));
                    if (fieldsExtra != null && fieldsExtra.Count > 0)
                    {
                        aaa = item["BH"];
                        mCdbz1 = GetSafeDouble(fieldsExtra["CDBZ1"]);
                        mCdbz2 = GetSafeDouble(fieldsExtra["CDBZ2"]);
                        mXdbz = GetSafeDouble(fieldsExtra["XDBZ"]);
                        mXdbz2 = GetSafeDouble(fieldsExtra["XDBZ2"]);
                        mXdbz3 = GetSafeDouble(fieldsExtra["XDBZ3"]);
                        mCnsj = GetSafeDouble(fieldsExtra["CNSJ"]);
                        mZnsj = GetSafeDouble(fieldsExtra["ZNSJ"]);
                        mKy_3 = GetSafeDouble(fieldsExtra["KY_3"]);
                        mKy_28 = GetSafeDouble(fieldsExtra["KY_28"]);
                        mKz_3 = GetSafeDouble(fieldsExtra["KZ_3"]);
                        mKz_28 = GetSafeDouble(fieldsExtra["KZ_28"]);
                    }
                    else
                    {
                        mCdbz1 = 0;
                        mCdbz2 = 0;
                        mCnsj = 0;
                        mZnsj = 0;
                        mKy_3 = 0;
                        mKy_7 = 0;
                        mKy_28 = 0;
                        mKz_3 = 0;
                        mKz_7 = 0;
                        mKz_28 = 0;
                        item["JCJG"] = "依据不详";
                        break;
                    }
                    m_sntab[row]["KY3_HG"] = "----";
                    m_sntab[row]["KZ3_HG"] = "----";
                    m_sntab[row]["KY28_HG"] = "----";
                    m_sntab[row]["KZ28_HG"] = "----";
                    #endregion
                    if (jcxmitem.Contains("强度") && GetSafeDouble(item["KYHZ3_1"]) > 0)
                    {
                        //3天
                        mMj = 0.625;
                        if (mMj != 0)
                        {
                            item["KYQD3_1"] = Math.Round(GetSafeDouble(item["KYHZ3_1"]) * mMj, 1).ToString("0.0");  //3天抗压强度1
                            item["KYQD3_2"] = Math.Round(GetSafeDouble(item["KYHZ3_2"]) * mMj, 1).ToString("0.0");  //3天抗压强度2
                            item["KYQD3_3"] = Math.Round(GetSafeDouble(item["KYHZ3_3"]) * mMj, 1).ToString("0.0");  //3天抗压强度3
                            item["KYQD3_4"] = Math.Round(GetSafeDouble(item["KYHZ3_4"]) * mMj, 1).ToString("0.0");  //3天抗压强度4
                            item["KYQD3_5"] = Math.Round(GetSafeDouble(item["KYHZ3_5"]) * mMj, 1).ToString("0.0");  //3天抗压强度5
                            item["KYQD3_6"] = Math.Round(GetSafeDouble(item["KYHZ3_6"]) * mMj, 1).ToString("0.0");  //3天抗压强度6
                            item["KYQD28_1"] = Math.Round(GetSafeDouble(item["KYHZ28_1"]) * mMj, 1).ToString("0.0");  //28天抗压强度1
                            item["KYQD28_2"] = Math.Round(GetSafeDouble(item["KYHZ28_2"]) * mMj, 1).ToString("0.0");  //28天抗压强度2
                            item["KYQD28_3"] = Math.Round(GetSafeDouble(item["KYHZ28_3"]) * mMj, 1).ToString("0.0");  //28天抗压强度3
                            item["KYQD28_4"] = Math.Round(GetSafeDouble(item["KYHZ28_4"]) * mMj, 1).ToString("0.0");  //28天抗压强度4
                            item["KYQD28_5"] = Math.Round(GetSafeDouble(item["KYHZ28_5"]) * mMj, 1).ToString("0.0");  //28天抗压强度5
                            item["KYQD28_6"] = Math.Round(GetSafeDouble(item["KYHZ28_6"]) * mMj, 1).ToString("0.0");  //28天抗压强度6
                        }
                        string mlongStr = item["KYQD3_1"] + "," + item["KYQD3_2"] + "," + item["KYQD3_3"] + "," + item["KYQD3_4"] + "," + item["KYQD3_5"] + "," + item["KYQD3_6"];
                        string[] mtmpArray = mlongStr.Split(',');
                        double[] mkyqdArray = new double[6];
                        for (int i = 0; i < 6; i++)
                            mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                        Array.Sort(mkyqdArray);
                        double mMaxKyqd = mkyqdArray[5];
                        double mMinKyqd = mkyqdArray[0];
                        double mavgKyqd;
                        //求数组的平均值
                        double mSum = 0;
                        for (int i = 0; i < mkyqdArray.Length; i++)
                        {
                            mSum += mkyqdArray[i];
                        }
                        mavgKyqd = Math.Round(mSum / mkyqdArray.Length, 1);
                        int mccgs = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            if (Math.Abs(mkyqdArray[i] - mavgKyqd) > mavgKyqd * 0.1)
                                mccgs = mccgs + 1;
                        }
                        if (mccgs > 1)
                            mavgKyqd = -1;
                        else
                        {
                            if (Math.Abs(mavgKyqd - mkyqdArray[0]) >= Math.Abs(mavgKyqd - mkyqdArray[5]))
                            {
                                if (Math.Abs(mavgKyqd - mkyqdArray[0]) > mavgKyqd * 0.1)
                                    mavgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5]) / 5;
                                if (Math.Abs(mavgKyqd - mkyqdArray[1]) > mavgKyqd * 0.1 || Math.Abs(mavgKyqd - mkyqdArray[5]) > mavgKyqd * 0.1)
                                    mavgKyqd = -1;
                            }
                            else
                            {
                                if (Math.Abs(mavgKyqd - mkyqdArray[5]) > mavgKyqd * 0.1)
                                    mavgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[0]) / 5;
                                if (Math.Abs(mavgKyqd - mkyqdArray[0]) > mavgKyqd * 0.1 || Math.Abs(mavgKyqd - mkyqdArray[4]) > mavgKyqd * 0.1)
                                    mavgKyqd = -1;
                            }
                        }
                        //计算抗压强度平均值:有超出平均值10%的首先剔除再平均,再有超出平均值10%的则作废
                        item["KYPJ3"] = Math.Round(mavgKyqd, 1).ToString("0.0");
                        //计算抗折平均值(要剔除其中超出平均值10%的抗折值再平均)
                        if (GetSafeDouble(item["KZHZ3_1"]) != 0)
                        {
                            item["KZQD3_1"] = Math.Round((1.5 * 100 * GetSafeDouble(item["KZHZ3_1"])) / 40 / 40 / 40, 1).ToString("0.0");
                            item["KZQD3_2"] = Math.Round((1.5 * 100 * GetSafeDouble(item["KZHZ3_2"])) / 40 / 40 / 40, 1).ToString("0.0");
                            item["KZQD3_3"] = Math.Round((1.5 * 100 * GetSafeDouble(item["KZHZ3_3"])) / 40 / 40 / 40, 1).ToString("0.0");
                        }
                        mKZPJ3z = Math.Round((GetSafeDouble(item["KZQD3_1"]) + GetSafeDouble(item["KZQD3_2"]) + GetSafeDouble(item["KZQD3_3"])) / 3, 1);
                        if (Math.Abs(GetSafeDouble(item["KZQD3_1"]) - mKZPJ3z) > Math.Abs(GetSafeDouble(item["KZQD3_2"]) - mKZPJ3z) && Math.Abs(GetSafeDouble(item["KZQD3_1"]) - mKZPJ3z) > Math.Abs(GetSafeDouble(item["KZQD3_3"]) - mKZPJ3z))
                        {
                            if (Math.Abs(GetSafeDouble(item["KZQD3_1"]) - mKZPJ3z) > Math.Round(mKZPJ3z * 0.1, 1))
                                mKZPJ3z = (GetSafeDouble(item["KZQD3_2"]) + GetSafeDouble(item["KZQD3_3"])) / 2;
                        }
                        if (Math.Abs(GetSafeDouble(item["KZQD3_2"]) - mKZPJ3z) > Math.Abs(GetSafeDouble(item["KZQD3_1"]) - mKZPJ3z) && Math.Abs(GetSafeDouble(item["KZQD3_2"]) - mKZPJ3z) > Math.Abs(GetSafeDouble(item["KZQD3_3"]) - mKZPJ3z))
                        {
                            if (Math.Abs(GetSafeDouble(item["KZQD3_2"]) - mKZPJ3z) > Math.Round(mKZPJ3z * 0.1, 1))
                                mKZPJ3z = (GetSafeDouble(item["KZQD3_1"]) + GetSafeDouble(item["KZQD3_3"])) / 2;
                        }
                        if (Math.Abs(GetSafeDouble(item["KZQD3_3"]) - mKZPJ3z) > Math.Abs(GetSafeDouble(item["KZQD3_1"]) - mKZPJ3z) && Math.Abs(GetSafeDouble(item["KZQD3_3"]) - mKZPJ3z) > Math.Abs(GetSafeDouble(item["KZQD3_2"]) - mKZPJ3z))
                        {
                            if (Math.Abs(GetSafeDouble(item["KZQD3_3"]) - mKZPJ3z) > Math.Round(mKZPJ3z * 0.1, 1))
                                mKZPJ3z = (GetSafeDouble(item["KZQD3_1"]) + GetSafeDouble(item["KZQD3_2"])) / 2;
                        }
                        item["KZPJ3"] = Math.Round(mKZPJ3z, 1).ToString("0.0");

                        //28天
                        mlongStr = item["KYQD28_1"] + "," + item["KYQD28_2"] + "," + item["KYQD28_3"] + "," + item["KYQD28_4"] + "," + item["KYQD28_5"] + "," + item["KYQD28_6"];
                        mtmpArray = mlongStr.Split(',');
                        mkyqdArray = new double[6];
                        for (int i = 0; i < 6; i++)
                            mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                        Array.Sort(mkyqdArray);
                        mMaxKyqd = mkyqdArray[5];
                        mMinKyqd = mkyqdArray[0];
                        //求数组的平均值
                        mSum = 0;
                        for (int i = 0; i < mkyqdArray.Length; i++)
                        {
                            mSum += mkyqdArray[i];
                        }
                        mavgKyqd = Math.Round(mSum / mkyqdArray.Length, 1);
                        mccgs = 0;
                        for (int i = 0; i < 6; i++)
                        {
                            if (Math.Abs(mkyqdArray[i] - mavgKyqd) > mavgKyqd * 0.1)
                                mccgs = mccgs + 1;
                        }
                        if (mccgs > 1)
                            mavgKyqd = -1;
                        else
                        {
                            if (Math.Abs(mavgKyqd - mkyqdArray[0]) >= Math.Abs(mavgKyqd - mkyqdArray[5]))
                            {
                                if (Math.Abs(mavgKyqd - mkyqdArray[0]) > mavgKyqd * 0.1)
                                    mavgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5]) / 5;
                                if (Math.Abs(mavgKyqd - mkyqdArray[1]) > mavgKyqd * 0.1 || Math.Abs(mavgKyqd - mkyqdArray[5]) > mavgKyqd * 0.1)
                                    mavgKyqd = -1;
                            }
                            else
                            {
                                if (Math.Abs(mavgKyqd - mkyqdArray[5]) > mavgKyqd * 0.1)
                                    mavgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[0]) / 5;
                                if (Math.Abs(mavgKyqd - mkyqdArray[0]) > mavgKyqd * 0.1 || Math.Abs(mavgKyqd - mkyqdArray[4]) > mavgKyqd * 0.1)
                                    mavgKyqd = -1;
                            }
                        }

                        item["KYPJ28"] = Math.Round(mavgKyqd, 1).ToString("0.0");
                        if (GetSafeDouble(item["KZHZ28_1"]) != 0)
                        {
                            item["KZQD28_1"] = Math.Round((1.5 * 100 * GetSafeDouble(item["KZHZ28_1"])) / 40 / 40 / 40, 1).ToString("0.0");
                            item["KZQD28_2"] = Math.Round((1.5 * 100 * GetSafeDouble(item["KZHZ28_2"])) / 40 / 40 / 40, 1).ToString("0.0");
                            item["KZQD28_3"] = Math.Round((1.5 * 100 * GetSafeDouble(item["KZHZ28_3"])) / 40 / 40 / 40, 1).ToString("0.0");
                        }
                        mKZPJ28z = Math.Round((GetSafeDouble(item["KZQD28_1"]) + GetSafeDouble(item["KZQD28_2"]) + GetSafeDouble(item["KZQD28_3"])) / 3, 1);
                        if (Math.Abs(GetSafeDouble(item["KZQD28_1"]) - mKZPJ28z) > Math.Abs(GetSafeDouble(item["KZQD28_2"]) - mKZPJ28z) && Math.Abs(GetSafeDouble(item["KZQD28_1"]) - mKZPJ28z) > Math.Abs(GetSafeDouble(item["KZQD28_3"]) - mKZPJ28z))
                        {
                            if (Math.Abs(GetSafeDouble(item["KZQD28_1"]) - mKZPJ28z) > Math.Round(mKZPJ28z * 0.1, 1))
                                mKZPJ28z = (GetSafeDouble(item["KZQD28_2"]) + GetSafeDouble(item["KZQD28_3"])) / 2;
                        }
                        if (Math.Abs(GetSafeDouble(item["KZQD28_2"]) - mKZPJ28z) > Math.Abs(GetSafeDouble(item["KZQD28_1"]) - mKZPJ28z) && Math.Abs(GetSafeDouble(item["KZQD28_2"]) - mKZPJ28z) > Math.Abs(GetSafeDouble(item["KZQD28_3"]) - mKZPJ28z))
                        {
                            if (Math.Abs(GetSafeDouble(item["KZQD28_2"]) - mKZPJ28z) > Math.Round(mKZPJ28z * 0.1, 1))
                                mKZPJ28z = (GetSafeDouble(item["KZQD28_1"]) + GetSafeDouble(item["KZQD28_3"])) / 2;
                        }
                        if (Math.Abs(GetSafeDouble(item["KZQD28_3"]) - mKZPJ28z) > Math.Abs(GetSafeDouble(item["KZQD28_1"]) - mKZPJ28z) && Math.Abs(GetSafeDouble(item["KZQD28_3"]) - mKZPJ28z) > Math.Abs(GetSafeDouble(item["KZQD28_2"]) - mKZPJ28z))
                        {
                            if (Math.Abs(GetSafeDouble(item["KZQD28_3"]) - mKZPJ28z) > Math.Round(mKZPJ28z * 0.1, 1))
                                mKZPJ28z = (GetSafeDouble(item["KZQD28_1"]) + GetSafeDouble(item["KZQD28_2"])) / 2;
                        }
                        item["KZPJ28"] = Math.Round(mKZPJ28z, 1).ToString("0.0");

                        //抗压合格判定
                        item["G_KYBZ3"] = mKy_3.ToString();   //抗折国标3
                        if (GetSafeDouble(item["KZPJ3"]) >= mKy_3)
                        {
                            item["KY3_HG"] = "符合";
                            ky3_hg = true;
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            if (GetSafeDouble(item["KYPJ3"]) == -1)
                                item["KY3_HG"] = "作废";
                            else
                                item["KY3_HG"] = "不符合";
                            ky3_hg = false;
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                        if (!jcxmitem.Contains("3天强度"))
                        {
                            item["KY3_HG"] = "----";
                            ky3_hg = true;
                        }
                        item["G_KYBZ28"] = mKy_28.ToString();   //抗压国标28
                        if (GetSafeDouble(item["KYPJ3"]) > 0 && GetSafeDouble(item["KYPJ28"]) == 0)
                        {
                            m_sntab[row]["SYZT"] = "0";    //试验全部完成标志
                            item["SYR"] = "";  //试验人
                            item["YQSYRQ"] = DateTime.Parse(item["ZZRQ"]).AddDays(28).ToString("yyyy-MM-dd HH:mm:ss.fff");    //要求试验日期
                        }
                        else
                            m_sntab[row]["SYZT"] = "1";
                        if (GetSafeDouble(item["KZPJ28"]) >= mKy_28)
                        {
                            item["KY28_HG"] = "符合";
                            ky28_hg = true;
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            if (GetSafeDouble(item["KYPJ28"]) == -1)
                                item["KY28_HG"] = "作废";
                            else
                                item["KY28_HG"] = "不符合";
                            ky28_hg = false;
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                        }

                        //抗折合格判定
                        item["G_KZBZ3"] = mKz_3.ToString();
                        if (GetSafeDouble(item["KZPJ3"]) > mKz_3)
                        {
                            item["KZ3_HG"] = "合格";
                            kz3_hg = true;
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            item["KZ3_HG"] = "不符合";
                            kz3_hg = false;
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                        if (!jcxmitem.Contains("3天强度"))
                        {
                            item["KZ3_HG"] = "----";
                            kz3_hg = true;
                        }
                        item["G_KZBZ28"] = mKz_28.ToString();
                        if (GetSafeDouble(item["KZPJ28"]) > mKz_28)
                        {
                            item["KZ28_HG"] = "合格";
                            kz28_hg = true;
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            item["KZ28_HG"] = "不符合";
                            kz28_hg = false;
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                        if (!jcxmitem.Contains("28天强度"))
                        {
                            item["KZ28_HG"] = "----";
                            kz28_hg = true;
                        }

                        if (GetSafeDouble(item["KZPJ3"]) == 0)
                        {
                            item["KZ3_HG"] = "----";
                            kz3_hg = true;
                        }
                        if (GetSafeDouble(item["KZPJ28"]) == 0)
                        {
                            item["KZ28_HG"] = "----";
                            kz28_hg = true;
                        }
                        //单组判定
                        //实际达到等级判定
                        var fieldsExtra1 = dataExtra["BZ_SN_DJ"].Where(u => u.ContainsKey("MC") && u.Values.Any(p => p.Contains(p.Substring(0, p.Length - 5))));
                        foreach (IDictionary<string, string> item2 in fieldsExtra1)
                        {
                            mDjMc = item2["MC"];
                            mCdbz1 = GetSafeDouble(item2["CDBZ1"]);
                            mCdbz2 = GetSafeDouble(item2["CDBZ2"]);
                            mXdbz = GetSafeDouble(item2["CDBZ3"]);
                            mCnsj = GetSafeDouble(item2["CNSJ"]);
                            mZnsj = GetSafeDouble(item2["ZNSJ"]);
                            mKy_3 = GetSafeDouble(item2["KY_3"]);
                            mKy_28 = GetSafeDouble(item2["KY_28"]);
                            mKz_3 = GetSafeDouble(item2["KZ_3"]);
                            mKz_28 = GetSafeDouble(item2["KZ_28"]);
                            if ((GetSafeDouble(item["KYPJ3"]) >= mKy_3 && GetSafeDouble(item["KYPJ3"]) == 0 && GetSafeDouble(item["KZPJ3"]) >= mKz_3) && (GetSafeDouble(item["KYPJ28"]) >= mKy_28 && GetSafeDouble(item["KYPJ28"]) == 0 && GetSafeDouble(item["KZPJ28"]) >= mKz_28))
                                mSjddj = mDjMc;
                            if (mDjMc == mSjdj)
                                break;
                        }
                        item["SJDDJ"] = mDjMc.Trim();
                    }
                    else
                    {
                        item["KYQD3_1"] = "0";
                        item["KYQD3_2"] = "0";
                        item["KYQD3_3"] = "0";
                        item["KYQD3_4"] = "0";
                        item["KYQD3_5"] = "0";
                        item["KYQD3_6"] = "0";
                        item["KYQD3_1"] = "0";
                        item["KYQD3_2"] = "0";
                        item["KYQD3_3"] = "0";
                        item["KZPJ3"] = "0";
                        item["KYPJ3"] = "0";
                        item["G_KYBZ3"] = "0";
                        item["G_KZBZ3"] = "0";
                        item["KZ3_HG"] = "----";
                        item["KY3_HG"] = "----";
                        item["KYQD28_1"] = "0";
                        item["KYQD28_2"] = "0";
                        item["KYQD28_3"] = "0";
                        item["KYQD28_4"] = "0";
                        item["KYQD28_5"] = "0";
                        item["KYQD28_6"] = "0";
                        item["KZQD28_1"] = "0";
                        item["KZQD28_2"] = "0";
                        item["KZQD28_3"] = "0";
                        item["KZPJ28"] = "0";
                        item["KYPJ28"] = "0";
                        item["G_KYBZ28"] = "0";
                        item["G_KZBZ28"] = "0";
                        item["KZ28_HG"] = "----";
                        item["KY28_HG"] = "----";
                    }

                    //安定性判定
                    if (!mjcxm.Contains("安定性"))
                    {
                        m_sntab[row]["ADXFF"] = "----";   //安定性方法
                        m_sntab[row]["AGX_HG"] = "----";  //安定性合格
                        m_sntab[row]["G_ADX"] = "----";   //安定性标准
                        m_sntab[row]["ADX"] = "----";     //代用法结果
                        m_sntab[row]["BZFPJ"] = "----";   //标准法平均值
                        m_sntab[row]["BZFXC"] = "----";   //标准法相差值
                        m_sntab[row]["G_BZFPJ"] = "----";  //标准法平均值要求
                        m_sntab[row]["G_BZFXC"] = "----";   //标准法相差值要求
                        adx_hg = true;
                    }
                    else
                    {
                        if (GetSafeDouble(item["KYPJ3"]) == 0 && m_sntab[row]["SFADX"] == "0")
                        {
                            item["SYR"] = "";
                            item["YQSYRQ"] = DateTime.Parse(item["zzrq"]).AddDays(3).ToString("yyyy-MM-dd HH:mm:ss.fff");
                        }
                        if (m_sntab[row]["ADXFF"] == "----")
                        {
                            m_sntab[row]["ADXFF"] = "代用法";
                        }
                        var mrsadx_filter = mrsadx.FirstOrDefault(x => x.Values.Contains(m_sntab[row]["ADXFF"].Trim()));
                        if (mrsadx_filter != null && mrsadx_filter.Count > 0)
                        {
                            m_sntab[row]["G_ADX"] = mrsadx_filter["G_ADX"].Trim();
                            m_sntab[row]["G_BZFPJ"] = mrsadx_filter["BZFPJ"].Trim();
                            m_sntab[row]["G_BZFXC"] = mrsadx_filter["BZFXC"].Trim();
                        }
                        //安定性
                        if (m_sntab[row]["ADXFF"].Trim() == "代用法")
                        {
                            if (m_sntab[row]["ADX"].Trim() == "合格")
                            {
                                adx_hg = true;
                                m_sntab[row]["ADX_HG"] = "符合";
                                s_rwtab[row]["JCJG"] = "符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                            }
                            else
                            {
                                m_sntab[row]["ADX_HG"] = "不符合";
                                adx_hg = false;
                                s_rwtab[row]["JCJG"] = "不符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                            }
                        }
                        else
                        {
                            m_sntab[row]["BZFPJ"] = Math.Round(((GetSafeDouble(m_sntab[row]["BZFC_1"]) - GetSafeDouble(m_sntab[row]["BZFA_1"])) + (GetSafeDouble(m_sntab[row]["BZFC_2"]) - GetSafeDouble(m_sntab[row]["BZFA_2"]))) / 2, 1).ToString("0.0");  //标准法平均值
                            m_sntab[row]["BZFXC"] = Math.Round(Math.Abs((GetSafeDouble(m_sntab[row]["BZFC_1"]) - GetSafeDouble(m_sntab[row]["BZFA_1"])) - (GetSafeDouble(m_sntab[row]["BZFC_2"]) - GetSafeDouble(m_sntab[row]["BZFA_2"]))), 1).ToString("0.0");  //标准法相差值
                            if (GetSafeDouble(m_sntab[row]["BZFPJ"]) <= GetSafeDouble(m_sntab[row]["G_BZFPJ"]) && GetSafeDouble(m_sntab[row]["BZFXC"]) <= GetSafeDouble(m_sntab[row]["G_BZFXC"]))
                            {
                                adx_hg = true;
                                m_sntab[row]["ADX_HG"] = "符合";
                                s_rwtab[row]["JCJG"] = "符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                            }
                            else
                            {
                                adx_hg = false;
                                m_sntab[row]["ADX_HG"] = "不符合";
                                s_rwtab[row]["JCJG"] = "不符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                            }
                        }
                    }

                    //稠度合格判定
                    if (!mjcxm.Contains("安定性") && !mjcxm.Contains("凝结时间"))
                    {
                        m_sntab[row]["CD_HG"] = "----";  //稠度合格
                        m_sntab[row]["G_CD"] = "----";   //稠度标准
                        m_sntab[row]["CD"] = "----";     //稠度
                        cd_hg = true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(m_sntab[row]["BZCDYSL"]))
                            m_sntab[row]["BZCDYSL"] = "0";
                        m_sntab[row]["CD"] = Math.Round(GetSafeDouble(m_sntab[row]["BZCDYSL"]) / 500 * 100, 1).ToString("0.0");
                        if (GetSafeDouble(m_sntab[row]["CD"]) > 0)
                        {
                            if (GetSafeDouble(m_sntab[row]["CD"]) >= mCdbz1 && GetSafeDouble(m_sntab[row]["CD"]) <= mCdbz2)
                            {
                                m_sntab[row]["CD_HG"] = "符合";
                                cd_hg = true;
                                s_rwtab[row]["JCJG"] = "符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                            }
                            else
                            {
                                m_sntab[row]["CD_HG"] = "不符合";
                                cd_hg = false;
                                s_rwtab[row]["JCJG"] = "不符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                            }
                            if ((mCdbz1 == 0 && mCdbz2 == 0) || string.IsNullOrEmpty(m_sntab[row]["CD"]) || GetSafeDouble(m_sntab[row]["CD"]) == 0)
                            {
                                m_sntab[row]["CD_HG"] = "----";
                                cd_hg = true;
                            }
                            else
                            {
                                m_sntab[row]["CD_HG"] = "----";
                                cd_hg = true;
                            }
                        }
                    }

                    //细度判定
                    if (!mjcxm.Contains("细度"))
                    {
                        m_sntab[row]["XD_HG"] = "----";
                        m_sntab[row]["XD"] = "----";
                        xd_hg = true;
                    }
                    else
                    {
                        if (mXdbz > 0)
                        {
                            if (GetSafeDouble(item["SYMD"]) == GetSafeDouble(item["BYMD"]) && GetSafeDouble(item["SYKXL"]) == GetSafeDouble(item["BYKXL"]))  //第一次被测试样密度 = 标准试样密度 ;  第一次被测试样空隙率 = 标准试样空隙率
                            {
                                if (Math.Abs(GetSafeDouble(item["SYSWD"]) - GetSafeDouble(item["XZSWD"])) <= 3)  //第一次试验时温度 - 校准温度
                                    m_sntab[row]["XD_1"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * Math.Sqrt(GetSafeDouble(item["SYT"])) / Math.Sqrt(GetSafeDouble(item["BYT"])), 0).ToString();   //第一次比表面积 = 标准试样比表面积 * 第一次被测试样压力计液面降落时间的平方根
                                else
                                    m_sntab[row]["XD_1"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * Math.Sqrt(GetSafeDouble(item["SYT"])) * Math.Sqrt(GetSafeDouble(item["BYKQYD"])) / Math.Sqrt(GetSafeDouble(item["BYT"])) / Math.Sqrt(GetSafeDouble(item["SYKQYD"])), 0).ToString();
                            }
                            if (GetSafeDouble(item["SYMD"]) == GetSafeDouble(item["BYMD"]) && GetSafeDouble(item["SYKXL"]) != GetSafeDouble(item["BYKXL"]))
                            {
                                if (Math.Abs(GetSafeDouble(item["SYSWD"]) - GetSafeDouble(item["XZSWD"])) <= 3)
                                    m_sntab[row]["XD_1"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * Math.Sqrt(GetSafeDouble(item["SYT"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"])) / (Math.Sqrt(GetSafeDouble(item["BYT"])) * (1 - GetSafeDouble(item["SYKXL"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                                else
                                    m_sntab[row]["XD_1"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * Math.Sqrt(GetSafeDouble(item["SYT"])) * Math.Sqrt(GetSafeDouble(item["BYKQYD"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"])) / (Math.Sqrt(GetSafeDouble(item["BYT"])) * Math.Sqrt(GetSafeDouble(item["SYKQYD"])) * (1 - GetSafeDouble(item["SYKXL"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                            }
                            if (GetSafeDouble(item["SYMD"]) != GetSafeDouble(item["BYMD"]) && GetSafeDouble(item["SYKXL"]) != GetSafeDouble(item["BYKXL"]))
                            {
                                if (Math.Abs(GetSafeDouble(item["SYSWD"]) - GetSafeDouble(item["XZSWD"])) <= 3)
                                    m_sntab[row]["XD_1"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * GetSafeDouble(item["BYMD"]) * Math.Sqrt(GetSafeDouble(item["SYT"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"])) / (GetSafeDouble(item["SYMD"]) * Math.Sqrt(GetSafeDouble(item["BYT"])) * (1 - GetSafeDouble(item["SYKXL"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                                else
                                    m_sntab[row]["XD_1"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * GetSafeDouble(item["BYMD"]) * Math.Sqrt(GetSafeDouble(item["SYT"])) * Math.Sqrt(GetSafeDouble(item["BYKQYD"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"])) / (GetSafeDouble(item["SYMD"]) * Math.Sqrt(GetSafeDouble(item["BYT"])) * Math.Sqrt(GetSafeDouble(item["SYKQYD"])) * (1 - GetSafeDouble(item["SYKXL"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                            }
                            if (GetSafeDouble(item["SYMD"]) != GetSafeDouble(item["BYMD"]) && GetSafeDouble(item["SYKXL"]) == GetSafeDouble(item["BYKXL"]))
                            {
                                if (Math.Abs(GetSafeDouble(item["SYSWD"]) - GetSafeDouble(item["XZSWD"])) <= 3)
                                    m_sntab[row]["XD_1"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * GetSafeDouble(item["BYMD"]) * Math.Sqrt(GetSafeDouble(item["SYT"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"])) / (GetSafeDouble(item["SYMD"]) * Math.Sqrt(GetSafeDouble(item["BYT"])) * (1 - GetSafeDouble(item["SYKXL"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                                else
                                    m_sntab[row]["XD_1"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * GetSafeDouble(item["BYMD"]) * Math.Sqrt(GetSafeDouble(item["SYT"])) * Math.Sqrt(GetSafeDouble(item["BYKQYD"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"]) * GetSafeDouble(item["SYKXL"])) / (GetSafeDouble(item["SYMD"]) * Math.Sqrt(GetSafeDouble(item["BYT"])) * Math.Sqrt(GetSafeDouble(item["SYKQYD"])) * (1 - GetSafeDouble(item["SYKXL"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                            }
                            m_sntab[row]["XD_1"] = Math.Round(GetSafeDouble(m_sntab[row]["XD_1"]), 0).ToString();

                            //第二次
                            if (GetSafeDouble(item["SYMD2"]) == GetSafeDouble(item["BYMD"]) && GetSafeDouble(item["SYKXL2"]) == GetSafeDouble(item["BYKXL"]))  //第一次被测试样密度 = 标准试样密度 ;  第一次被测试样空隙率 = 标准试样空隙率
                            {
                                if (Math.Abs(GetSafeDouble(item["SYSWD2"]) - GetSafeDouble(item["XZSWD"])) <= 3)  //第一次试验时温度 - 校准温度
                                    m_sntab[row]["XD_2"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * Math.Sqrt(GetSafeDouble(item["SYT2"])) / Math.Sqrt(GetSafeDouble(item["BYT"])), 0).ToString();   //第一次比表面积 = 标准试样比表面积 * 第一次被测试样压力计液面降落时间的平方根
                                else
                                    m_sntab[row]["XD_2"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * Math.Sqrt(GetSafeDouble(item["SYT2"])) * Math.Sqrt(GetSafeDouble(item["BYKQYD"])) / Math.Sqrt(GetSafeDouble(item["BYT"])) / Math.Sqrt(GetSafeDouble(item["SYKQYD2"])), 0).ToString();
                            }
                            if (GetSafeDouble(item["SYMD2"]) == GetSafeDouble(item["BYMD"]) && GetSafeDouble(item["SYKXL2"]) != GetSafeDouble(item["BYKXL"]))
                            {
                                if (Math.Abs(GetSafeDouble(item["SYSWD2"]) - GetSafeDouble(item["XZSWD"])) <= 3)
                                    m_sntab[row]["XD_2"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * Math.Sqrt(GetSafeDouble(item["SYT2"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"])) / (Math.Sqrt(GetSafeDouble(item["BYT"])) * (1 - GetSafeDouble(item["SYKXL2"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                                else
                                    m_sntab[row]["XD_2"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * Math.Sqrt(GetSafeDouble(item["SYT2"])) * Math.Sqrt(GetSafeDouble(item["BYKQYD"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"])) / (Math.Sqrt(GetSafeDouble(item["BYT"])) * Math.Sqrt(GetSafeDouble(item["SYKQYD2"])) * (1 - GetSafeDouble(item["SYKXL2"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                            }
                            if (GetSafeDouble(item["SYMD2"]) != GetSafeDouble(item["BYMD"]) && GetSafeDouble(item["SYKXL2"]) != GetSafeDouble(item["BYKXL"]))
                            {
                                if (Math.Abs(GetSafeDouble(item["SYSWD2"]) - GetSafeDouble(item["XZSWD"])) <= 3)
                                    m_sntab[row]["XD_2"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * GetSafeDouble(item["BYMD"]) * Math.Sqrt(GetSafeDouble(item["SYT2"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"])) / (GetSafeDouble(item["SYMD2"]) * Math.Sqrt(GetSafeDouble(item["BYT"])) * (1 - GetSafeDouble(item["SYKXL2"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                                else
                                    m_sntab[row]["XD_2"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * GetSafeDouble(item["BYMD"]) * Math.Sqrt(GetSafeDouble(item["SYT2"])) * Math.Sqrt(GetSafeDouble(item["BYKQYD"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"])) / (GetSafeDouble(item["SYMD2"]) * Math.Sqrt(GetSafeDouble(item["BYT"])) * Math.Sqrt(GetSafeDouble(item["SYKQYD2"])) * (1 - GetSafeDouble(item["SYKXL2"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                            }
                            if (GetSafeDouble(item["SYMD2"]) != GetSafeDouble(item["BYMD"]) && GetSafeDouble(item["SYKXL2"]) == GetSafeDouble(item["BYKXL"]))
                            {
                                if (Math.Abs(GetSafeDouble(item["SYSWD2"]) - GetSafeDouble(item["XZSWD"])) <= 3)
                                    m_sntab[row]["XD_2"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * GetSafeDouble(item["BYMD"]) * Math.Sqrt(GetSafeDouble(item["SYT2"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"])) / (GetSafeDouble(item["SYMD2"]) * Math.Sqrt(GetSafeDouble(item["BYT"])) * (1 - GetSafeDouble(item["SYKXL2"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                                else
                                    m_sntab[row]["XD_2"] = Math.Round(GetSafeDouble(item["BYBBMJ"]) * GetSafeDouble(item["BYMD"]) * Math.Sqrt(GetSafeDouble(item["SYT2"])) * Math.Sqrt(GetSafeDouble(item["BYKQYD"])) * (1 - GetSafeDouble(item["BYKXL"])) * Math.Sqrt(GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"]) * GetSafeDouble(item["SYKXL2"])) / (GetSafeDouble(item["SYMD2"]) * Math.Sqrt(GetSafeDouble(item["BYT"])) * Math.Sqrt(GetSafeDouble(item["SYKQYD2"])) * (1 - GetSafeDouble(item["SYKXL2"])) * Math.Sqrt(GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]) * GetSafeDouble(item["BYKXL"]))), 0).ToString();
                            }
                            m_sntab[row]["XD_2"] = Math.Round(GetSafeDouble(m_sntab[row]["XD_2"]), 0).ToString();
                            m_sntab[row]["XD"] = Math.Round((GetSafeDouble(m_sntab[row]["XD_1"]) + GetSafeDouble(m_sntab[row]["XD_2"])) / 2, 0).ToString();
                            if (Math.Abs(GetSafeDouble(m_sntab[row]["XD_1"]) - GetSafeDouble(m_sntab[row]["XD_2"])) / GetSafeDouble(m_sntab[row]["XD_1"]) > 0.02 || Math.Abs(GetSafeDouble(m_sntab[row]["XD_1"]) - GetSafeDouble(m_sntab[row]["XD_2"])) / GetSafeDouble(m_sntab[row]["XD_2"]) > 0.02)
                            {
                                m_sntab[row]["XD_HG"] = "需重做";
                                xd_hg = false;
                            }
                            else
                            {
                                if (GetSafeDouble(m_sntab[row]["XD"]) >= mXdbz)
                                {
                                    m_sntab[row]["XD_HG"] = "符合";
                                    xd_hg = true;
                                    s_rwtab[row]["JCJG"] = "符合";
                                    s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                                }
                                else
                                {
                                    m_sntab[row]["XD_HG"] = "不符合";
                                    xd_hg = false;
                                    s_rwtab[row]["JCJG"] = "不符合";
                                    s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                                }
                            }
                        }

                        if (mXdbz2 > 0 && GetSafeDouble(m_sntab[row]["XD2"]) > 0)
                        {
                            if (GetSafeDouble(m_sntab[row]["XD2"]) <= mXdbz2)
                            {
                                m_sntab[row]["XD_HG"] = "符合";
                                xd_hg = true;
                                s_rwtab[row]["JCJG"] = "符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                            }
                            else
                            {
                                m_sntab[row]["XD_HG"] = "不符合";
                                xd_hg = false;
                                s_rwtab[row]["JCJG"] = "不符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                            }
                        }
                        if (mXdbz3 > 0 && GetSafeDouble(m_sntab[row]["XD3"]) > 0)
                        {
                            if (GetSafeDouble(m_sntab[row]["XD3"]) <= mXdbz3)
                            {
                                m_sntab[row]["XD_HG"] = "符合";
                                xd_hg = true;
                                s_rwtab[row]["JCJG"] = "符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                            }
                            else
                            {
                                m_sntab[row]["XD_HG"] = "不符合";
                                xd_hg = false;
                                s_rwtab[row]["JCJG"] = "不符合";
                                s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                            }
                        }
                    }

                    //凝结时间符合判定
                    if (!mjcxm.Contains("凝结时间"))
                    {
                        m_sntab[row]["CN_HG"] = "----";    //初凝合格
                        m_sntab[row]["G_CNSJ"] = "----";   //初凝时间标准
                        m_sntab[row]["CNSJ"] = "----";     //初凝时间
                        cn_hg = true;
                        m_sntab[row]["ZN_HG"] = "----";    //终凝合格
                        m_sntab[row]["G_ZNSJ"] = "----";   //终凝时间标准
                        m_sntab[row]["ZNSJ"] = "----";     //终凝时间
                        zn_hg = true;
                    }
                    else
                    {
                        m_sntab[row]["CNSJ"] = (GetSafeDouble(m_sntab[row]["CNSJH"]) * 60 + GetSafeDouble(m_sntab[row]["CNSJM"]) - (GetSafeDouble(m_sntab[row]["JSSJH"]) * 60 + GetSafeDouble(m_sntab[row]["JSSJM"]))).ToString();
                        m_sntab[row]["ZNSJ"] = (GetSafeDouble(m_sntab[row]["ZNSJH"]) * 60 + GetSafeDouble(m_sntab[row]["ZNSJM"]) - (GetSafeDouble(m_sntab[row]["JSSJH"]) * 60 + GetSafeDouble(m_sntab[row]["JSSJM"]))).ToString();
                        //初凝判断
                        if (String.IsNullOrEmpty(m_sntab[row]["CNSJ"]))
                            m_sntab[row]["CNSJ"] = "0";
                        if (GetSafeDouble(m_sntab[row]["CNSJ"]) >= mCnsj)
                        {
                            m_sntab[row]["CN_HG"] = "符合";
                            cn_hg = true;
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            m_sntab[row]["CN_HG"] = "不符合";
                            cn_hg = false;
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                        if (m_sntab[row]["CNSJ"] == "0" || string.IsNullOrEmpty(m_sntab[row]["CNSJ"]))
                        {
                            m_sntab[row]["CN_HG"] = "____";
                            cn_hg = true;
                        }
                        //终凝判断
                        if (String.IsNullOrEmpty(m_sntab[row]["ZNSJ"]))
                            m_sntab[row]["ZNSJ"] = "0";
                        if (GetSafeDouble(m_sntab[row]["ZNSJ"]) <= mZnsj)
                        {
                            m_sntab[row]["ZN_HG"] = "符合";
                            zn_hg = true;
                            s_rwtab[row]["JCJG"] = "符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            m_sntab[row]["ZN_HG"] = "不符合";
                            zn_hg = false;
                            s_rwtab[row]["JCJG"] = "不符合";
                            s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                        if (m_sntab[row]["ZNSJ"] == "0" || string.IsNullOrEmpty(m_sntab[row]["ZNSJ"]))
                        {
                            m_sntab[row]["ZN_HG"] = "____";
                            zn_hg = true;
                        }
                    }

                    //总计算
                    if (GetSafeDouble(m_sntab[row]["CNSJ"]) >= GetSafeDouble(m_sntab[row]["ZNSJ"]))
                    {
                        m_sntab[row]["CN_HG"] = "____";
                        m_sntab[row]["ZN_HG"] = "____";
                        //凝结时间初凝时间大于终凝时间，请从新输入
                        zn_hg = true;
                        cn_hg = true;
                    }
                    if (mCnsj == 0 && mZnsj == 0 || m_sntab[row]["CNSJ"] == "0" || m_sntab[row]["ZNSJ"] == "0" || string.IsNullOrEmpty(m_sntab[row]["CNSJ"]) || string.IsNullOrEmpty(m_sntab[row]["ZNSJ"]))
                    {
                        m_sntab[row]["CN_HG"] = "____";
                        m_sntab[row]["ZN_HG"] = "____";
                        zn_hg = true;
                        cn_hg = true;
                    }
                    if (GetSafeDouble(m_sntab[row]["G_XD"]) > 0)
                        m_sntab[row]["WHICH"] = "0";
                    else
                    {
                        m_sntab[row]["WHICH"] = "2";
                        if (GetSafeDouble(m_sntab[row]["XD3"]) > 0)
                            m_sntab[row]["WHICH"] = (GetSafeInt(m_sntab[row]["WHICH"]) + 2).ToString();
                    }
                    if (m_sntab[row]["ADXFF"] == "标准法")
                        m_sntab[row]["WHICH"] = (GetSafeInt(m_sntab[row]["WHICH"]) + 1).ToString();
                    if (m_sntab[row]["SFADX"] == "1")
                        m_sntab[row]["SYZT"] = "1";
                    if (cd_hg & xd_hg && cn_hg && zn_hg && adx_hg)
                        m_sntab[row]["MISCJCJG"] = "合格";
                    else
                    {
                        m_sntab[row]["MISCJCJG"] = "不合格";
                        mAllHg = false;
                    }
                    if (m_sntab[row]["MISCJCJG"] == "合格" && ky3_hg && ky28_hg && kz3_hg && kz28_hg)
                    {
                        item["JCJG"] = "合格";
                        mAllHg = true;
                    }
                    else
                    {
                        item["JCJG"] = "不合格";
                        mAllHg = false;
                    }
                    //主表总判断赋值
                    if (mAllHg)
                        m_sntab[row]["JCJG"] = "合格";
                    else
                        m_sntab[row]["JCJG"] = "不合格";

                    if (m_sntab[row]["SYZT"] == "1")
                    {
                        if (mAllHg)
                            m_sntab[row]["JGSM"] = "该组试样所检项目符合上述标准之要求。";
                        else
                            m_sntab[row]["JGSM"] = "该组试样不符合上述标准之要求。";
                    }
                    row++;
                }
            }
            #region 更新主表检测结果
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            bgjgDic.Add("JCJG", (mAllHg ? "合格" : "不合格"));
            bgjgDic.Add("JCJGMS", $"该组试样所检项目{(mAllHg ? "" : "不")}符合{dataExtra["BZ_HPB_DJ"].FirstOrDefault()["PDBZ"]}标准要求。");
            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion
            return mAllHg;
            /************************ 代码结束 *********************/
        }
    }
}
