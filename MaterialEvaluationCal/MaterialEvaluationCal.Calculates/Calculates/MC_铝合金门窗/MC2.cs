using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.MC_铝合金门窗
{
    public partial class MC: BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            var jcxm_keys = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            foreach (var jcxmitem in jcxm_keys)
            {
                var s_mctab = retData[jcxmitem]["S_MC"];
                var s_rwtab = retData[jcxmitem]["S_BY_RW_XQ"];
                var m_mctab = retData[jcxmitem]["M_MC"];
                int rw = 0;
                foreach (var item in s_mctab)
                {
                    #region  公共区域
                    double zj1 = 0;
                    double zj2 = 0;
                    var tab_kfyjb = dataExtra["BZ_KFYJB"];
                    var tab_qmxjb = dataExtra["BZ_QMXJB"];
                    var tab_smxjb = dataExtra["BZ_SMXJB"];
                    string mfcqmzyjb = string.Empty; //单位缝长气密性(正压)
                    string mfcqmfyjb = string.Empty;  //单位缝长气密性(负压)
                    string mmjqmzyjb = string.Empty;  //单位面积气密性(正压)
                    string mmjqmfyjb = string.Empty;  //单位面积气密性(负压)
                    string mjb = string.Empty;
                    string mjb1 = string.Empty;
                    #endregion
                    if (jcxmitem == "抗风压性能")
                    {
                        foreach (var kfy in tab_kfyjb)
                        {
                            if (GetSafeDouble(item["KFYP3"]) > GetSafeDouble(kfy["KFYXX"]) && GetSafeDouble(item["KFYP3"]) < GetSafeDouble(kfy["KFYSX"]))
                            {
                                item["KFYJB"] = kfy["KFYJB"].Trim();
                                item["KFYFW1"] = kfy["KFYXX"];
                                item["KFYFW2"] = kfy["KFYSX"];
                                break;
                            }
                            else
                                item["KFYJB"] = "0";
                        }
                        if (item["KFYJB"].Trim() == "×")
                            item["KFYJB"] = Math.Round(GetSafeDouble(item["KFYP3"]), 1).ToString("0.0");
                        if (GetSafeDouble(item["KFYP3"]) >= GetSafeDouble(item["KFYSJYQ"]))
                        {
                            m_mctab[rw]["KFYPD"] = "符合";
                            item["PD_KF"] = "符合";
                            s_rwtab[rw]["JCJG"] = "符合";
                            s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["KFYP3"] + ";达到要求：" + item["KFYSJYQ"] + ";所检测项目符合标准要求";
                        }
                        else
                        {
                            m_mctab[rw]["KFYPD"] = "不符合";
                            item["PD_KF"] = "不符合";
                            s_rwtab[rw]["JCJG"] = "不符合";
                            s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["KFYP3"] + ";达到要求：" + item["KFYSJYQ"] + ";所检测项目不符合标准要求";
                        }
                        if (string.IsNullOrEmpty(item["KFYSJYQ"]) || GetSafeDouble(item["KFYSJYQ"]) == 0)
                        {
                            item["PD_KF"] = "----";
                            s_rwtab[rw]["JCJG"] = "不做判断";
                        }
                    }
                    else
                    {
                        item["PD_KF"] = "----";
                        item["KFYJB"] = "----";
                        s_rwtab[rw]["JCJG"] = "不做判断";
                    }
                    if (jcxmitem == "水密性能")
                    {
                        if (jcxmitem == "抗风压性能")
                            item["GGXH"] = item["GGXH"].Trim() + "-ΔP" + item["SMXSJJB"].Trim();
                        else
                            item["GGXH"] = item["GGXH"].Trim() + "(ΔP" + item["SMXSJJB"].Trim();
                        foreach (var smx in tab_smxjb)
                        {
                            if (GetSafeDouble(item["SMXYL"]) >= GetSafeDouble(smx["SMXXX"]) && GetSafeDouble(item["SMXYL"]) < GetSafeDouble(smx["SMXSX"]))
                            {
                                item["JCJG"] = "合格";
                                item["SMXJB"] = smx["SMXJB"].Trim();
                                item["SMXFW1"] = smx["SMXXX"];
                                item["SMXFW2"] = smx["SMXSX"];
                                break;
                            }
                            else
                                item["SMXJB"] = "0";
                        }
                        if (item["SMXJB"].Trim() == "×")
                            item["SMXJB"] = Math.Round(GetSafeDouble(item["SMXYL"]), 0).ToString();
                        if (GetSafeDouble(item["SMXYL"]) >= GetSafeDouble(item["SMXSJYQ"]))
                        {
                            m_mctab[rw]["SMXPD"] = "符合";
                            item["PD_SM"] = "符合";
                            s_rwtab[rw]["JCJG"] = "符合";
                            s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["SMXYL"] + ";达到要求：" + item["SMXSJYQ"] + ";所检测项目符合标准要求";
                        }
                        else
                        {
                            m_mctab[rw]["SMXPD"] = "不符合";
                            item["PD_SM"] = "不符合";
                            s_rwtab[rw]["JCJG"] = "不符合";
                            s_rwtab[rw]["JCJGMS"] = "设计要求：" + item["SMXYL"] + ";达到要求：" + item["SMXSJYQ"] + ";所检测项目符合标准要求";
                        }
                        if (string.IsNullOrEmpty(item["SMXSJYQ"]) || GetSafeDouble(item["SMXSJYQ"]) == 0)
                            item["PD_KF"] = "----";
                    }
                    else
                    {
                        item["PD_SM"] = "----";
                        item["SMXJB"] = "----";
                        s_rwtab[rw]["JCJG"] = "不做判断";
                    }
                    if (jcxmitem == "气密性能")
                    {
                        if (jcxmitem == "抗风压性能" || jcxmitem == "水密性能")
                            item["GGXH"] = item["GGXH"] + "-q&scsub  1&scend" + item["QMXQ1JB"];
                        else
                            item["GGXH"] = item["GGXH"] + "(q&scsub  1&scend" + item["QMXQ1JB"];
                        //单位缝长气密性(正压)
                        foreach (var qmx in tab_qmxjb)
                        {
                            if (GetSafeDouble(item["FCQMZY"]) > GetSafeDouble(qmx["QMXFCXX"]) && GetSafeDouble(item["FCQMZY"]) <= GetSafeDouble(qmx["QMXFCSX"]))
                            {
                                mfcqmzyjb = qmx["QMXJB"].Trim();
                                item["QMXJB"] = qmx["QMXJB"].Trim();
                                break;
                            }
                            else
                            {
                                item["QMXJB"] = "0";
                                mfcqmzyjb = "0";
                            }
                        }
                        //单位缝长气密性(负压)
                        foreach (var qmx in tab_qmxjb)
                        {
                            if (GetSafeDouble(item["FCQMFY"]) > GetSafeDouble(qmx["QMXFCXX"]) && GetSafeDouble(item["FCQMFY"]) <= GetSafeDouble(qmx["QMXFCSX"]))
                            {
                                mfcqmfyjb = qmx["QMXJB"].Trim();
                                break;
                            }
                            else
                                mfcqmfyjb = "0";
                        }
                        //单位面积气密性(正压)
                        foreach (var qmx in tab_qmxjb)
                        {
                            if (GetSafeDouble(item["MJQMZY"]) > GetSafeDouble(qmx["QMXMJXX"]) && GetSafeDouble(item["MJQMZY"]) <= GetSafeDouble(qmx["QMXMJSX"]))
                            {
                                mmjqmzyjb = qmx["QMXJB"].Trim();
                                break;
                            }
                            else
                                mmjqmzyjb = "0";
                        }
                        //单位面积气密性(负压)
                        foreach (var qmx in tab_qmxjb)
                        {
                            if (GetSafeDouble(item["MJQMFY"]) > GetSafeDouble(qmx["QMXMJXX"]) && GetSafeDouble(item["MJQMFY"]) <= GetSafeDouble(qmx["QMXMJSX"]))
                            {
                                mmjqmfyjb = qmx["QMXJB"].Trim();
                                break;
                            }
                            else
                                mmjqmfyjb = "0";
                        }

                        if (GetSafeDouble(mmjqmzyjb) < GetSafeDouble(mfcqmzyjb))
                        {
                            mjb = "mj";
                            item["QMXZYJB"] = mmjqmzyjb;  //气密性正压级别
                        }
                        else
                        {
                            mjb = "fc";
                            item["QMXZYJB"] = mfcqmzyjb; //气密性正压级别
                        }
                        if (GetSafeDouble(mmjqmfyjb) < GetSafeDouble(mfcqmfyjb))
                        {
                            mjb1 = "mj";
                            item["QMXFYJB"] = mmjqmfyjb; //气密性负压级别
                        }
                        else
                        {
                            mjb1 = "fc";
                            item["QMXFYJB"] = mfcqmfyjb; //气密性负压级别
                        }
                        if (mjb == "mj" && mjb1 == "mj")
                        {
                            item["QMXFYJB"] = mmjqmfyjb;
                            item["QMXZYJB"] = mmjqmzyjb;
                            item["QMXDH"] = "2";
                            item["QMXDH1"] = "2";
                            foreach (var qmx in tab_qmxjb)
                            {
                                item["QMXFW3"] = qmx["QMXMJSX"];
                                item["QMXFW4"] = qmx["QMXMJXX"];
                                if (qmx["QMXJB"].Trim() == mmjqmfyjb)
                                    break;
                            }
                            foreach (var qmx in tab_qmxjb)
                            {
                                item["QMXFW1"] = qmx["QMXMJSX"];
                                item["QMXFW2"] = qmx["QMXMJXX"];
                                if (qmx["QMXJB"].Trim() == mmjqmzyjb)
                                    break;
                            }
                        }

                        if (mjb == "fc" && mjb1 == "fc")
                        {
                            //item["QMXFYJB"] = mmjqmfyjb;
                            //item["QMXZYJB"] = mmjqmzyjb;
                            item["QMXDH"] = "1";
                            item["QMXDH1"] = "1";
                            foreach (var qmx in tab_qmxjb)
                            {
                                item["QMXFW3"] = qmx["QMXFCSX"];
                                item["QMXFW4"] = qmx["QMXFCXX"];
                                if (qmx["QMXJB"].Trim() == mfcqmfyjb)
                                    break;
                            }
                            foreach (var qmx in tab_qmxjb)
                            {
                                item["QMXFW1"] = qmx["QMXFCSX"];
                                item["QMXFW2"] = qmx["QMXFCXX"];
                                if (qmx["QMXJB"].Trim() == mfcqmzyjb)
                                    break;
                            }
                        }

                        if (mjb == "mj" && mjb1 == "fc")
                        {
                            item["QMXDH"] = "2";
                            item["QMXDH1"] = "1";
                            foreach (var qmx in tab_qmxjb)
                            {
                                item["QMXFW1"] = qmx["QMXMJSX"];
                                item["QMXFW2"] = qmx["QMXMJXX"];
                                if (qmx["QMXJB"].Trim() == mmjqmzyjb)
                                    break;
                            }
                            foreach (var qmx in tab_qmxjb)
                            {
                                item["QMXFW3"] = qmx["QMXFCSX"];
                                item["QMXFW4"] = qmx["QMXFCXX"];
                                if (qmx["QMXJB"].Trim() == mmjqmfyjb)
                                    break;
                            }
                        }

                        if (mjb == "fc" && mjb1 == "mj")
                        {
                            item["QMXDH"] = "1";
                            item["QMXDH1"] = "2";
                            foreach (var qmx in tab_qmxjb)
                            {
                                item["QMXFW1"] = qmx["QMXFCSX"];
                                item["QMXFW2"] = qmx["QMXFCXX"];
                                if (qmx["QMXJB"].Trim() == mfcqmzyjb)
                                    break;
                            }
                            foreach (var qmx in tab_qmxjb)
                            {
                                item["QMXFW3"] = qmx["QMXMJSX"];
                                item["QMXFW4"] = qmx["QMXMJXX"];
                                if (qmx["QMXJB"].Trim() == mmjqmfyjb)
                                    break;
                            }
                        }

                        if (GetSafeDouble(item["FCQMZY"]) <= GetSafeDouble(item["QMXQ1YQ"]) && GetSafeDouble(item["FCQMFY"]) <= GetSafeDouble(item["QMXQ1YQ"]) && GetSafeDouble(item["MJQMZY"]) <= GetSafeDouble(item["QMXQ2YQ"]) && GetSafeDouble(item["MJQMFY"]) <= GetSafeDouble(item["QMXQ2YQ"]))
                        {
                            m_mctab[rw]["QMXPD"] = "符合";
                            item["PD_QM"] = "符合";
                            s_rwtab[rw]["JCJG"] = "符合";
                            s_rwtab[rw]["JCJGMS"] = "所检测项目符合标准要求";
                        }
                        else
                        {
                            m_mctab[rw]["QMXPD"] = "不符合";
                            item["PD_QM"] = "不符合";
                            s_rwtab[rw]["JCJG"] = "不符合";
                            s_rwtab[rw]["JCJGMS"] = "所检测项目不符合标准要求";
                        }
                        if ((string.IsNullOrEmpty(item["QMXQ1YQ"]) || GetSafeDouble(item["QMXQ1YQ"]) == 0) && (string.IsNullOrEmpty(item["QMXQ2YQ"]) || GetSafeDouble(item["QMXQ2YQ"]) == 0))
                            item["PD_QM"] = "----";
                    }
                    else
                    {
                        item["PD_QM"] = "----";
                        item["QMXZYJB"] = "----";
                        item["QMXFYJB"] = "----";
                    }
                    item["KFYJB1"] = item["KFYJB"];
                    item["QMXZYJB1"] = item["QMXZYJB"];
                    item["QMXFYJB1"] = item["QMXFYJB"];
                    item["SMXJB1"] = item["SMXJB"];
                    //抗风压
                    if (GetSafeDouble(item["KFYJB1"]) == 0)
                    {
                        item["KFYJB1"] = "1";
                        foreach (var kfy in tab_kfyjb)
                        {
                            if (GetSafeDouble(kfy["KFYJB"]) == 1)
                            {
                                item["KFYFW1"] = kfy["KFYXX"];
                                item["KFYFW2"] = kfy["KFYSX"];
                                break;
                            }
                        }
                    }
                    //气密性正压
                    if (GetSafeDouble(item["QMXZYJB1"]) == 0)
                    {
                        item["QMXZYJB1"] = "1";
                        foreach (var qmx in tab_qmxjb)
                        {
                            if (GetSafeDouble(qmx["QMXJB"]) == 1)
                            {
                                if (item["QMXDH"].Trim() == "2")
                                {
                                    item["QMXFW1"] = qmx["QMXMJSX"];
                                    item["QMXFW2"] = qmx["QMXMJXX"];
                                }
                                else
                                {
                                    item["QMXFW1"] = qmx["QMXFCSX"];
                                    item["QMXFW2"] = qmx["QMXFCXX"];
                                }
                                break;
                            }
                        }
                    }
                    //气密性负压
                    if (GetSafeDouble(item["QMXFYJB1"]) == 0)
                    {
                        item["QMXFYJB1"] = "1";
                        foreach (var qmx in tab_qmxjb)
                        {
                            if (GetSafeDouble(qmx["QMXJB"]) == 1)
                            {
                                if (item["QMXDH"].Trim() == "2")
                                {
                                    item["QMXFW3"] = qmx["QMXMJSX"];
                                    item["QMXFW4"] = qmx["QMXMJXX"];
                                }
                                else
                                {
                                    item["QMXFW3"] = qmx["QMXFCSX"];
                                    item["QMXFW4"] = qmx["QMXFCXX"];
                                }
                                break;
                            }
                        }
                    }
                    //水密性
                    if (GetSafeDouble(item["SMXJB1"]) == 0)
                    {
                        item["SMXJB1"] = "1";
                        foreach (var kfy in tab_kfyjb)
                        {
                            if (GetSafeDouble(kfy["SMXJB"]) == 1)
                            {
                                item["SMXFW1"] = kfy["SMXXX"];
                                item["SMXFW2"] = kfy["SMXSX"];
                                break;
                            }
                        }
                    }
                    if (item["PD_KF"].Trim() == "不符合" || item["PD_SM"] == "不符合" || item["PD_QM"].Trim() == "不符合")
                        mAllHg = false;
                    else
                        mAllHg = true;
                    bool mSfdjjc = true;
                    if (item["KFYSJYQ"] == "0" && item["QMXQ1YQ"] == "0" && item["QMXQ2YQ"] == "0" && item["SMXSJYQ"] == "0")
                        mSfdjjc = false;
                    else
                        mSfdjjc = true;

                    //主表总判断赋值
                    if (mAllHg)
                    {
                        m_mctab[rw]["JCJG"] = "合格";
                        if (mSfdjjc)
                            m_mctab[rw]["JGSM"] = "该组试件所检项目符合设计之要求。";
                        else
                            m_mctab[rw]["JGSM"] = "----";
                    }
                    else
                    {
                        m_mctab[rw]["JCJG"] = "不合格";
                        if (mSfdjjc)
                            m_mctab[rw]["JGSM"] = "该组试件不符合设计之要求。";
                        else
                            m_mctab[rw]["JGSM"] = "----";
                    }
                    rw++;
                }
            }

            #region 更新主表检测结果
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            bgjgDic.Add("JCJG", (mAllHg ? "合格" : "不合格"));
            bgjgDic.Add("JCJGMS", $"该组试样所检项目{(mAllHg ? "" : "不")}符合{dataExtra["BZ_MC_DJ"].FirstOrDefault()["PDBZ"]}设计要求。");
            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion

            return true;
            /************************ 代码结束 *********************/
        }
    }
}
