using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.DXL_电线
{
    public class DXL : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var jcxm_keys = retData.Select(u => u.Key).ToArray();

            foreach (var jcxmitem in jcxm_keys)
            {
                var s_dxltab = retData[jcxmitem]["S_DXL"];
                var s_rwtab = retData[jcxmitem]["S_BY_RW_XQ"];
                var m_dxltab = retData[jcxmitem]["M_DXL"];
                var mrsDj = dataExtra["BZ_DXL_DJ"];
                int row = 0;
                foreach (var item in s_dxltab)
                {
                    #region   公共部分
                    string mJSFF = "";
                    double zj1 = 0;
                    double zj2 = 0;
                    m_dxltab[row]["JSBEIZHU"] = "";
                    var mrsDj_Filter = mrsDj.FirstOrDefault(u => u.ContainsKey("GGXH") && u.Values.Any(p => p.Contains(item["GGXH"].Trim())));
                    if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                    {
                        m_dxltab[row]["G_JYHD"] = string.IsNullOrEmpty(mrsDj_Filter["JYHD"]) ? "0" : mrsDj_Filter["JYHD"];
                        m_dxltab[row]["G_ZBHD"] = string.IsNullOrEmpty(mrsDj_Filter["ZBHD"]) ? "0" : mrsDj_Filter["ZBHD"];
                        m_dxltab[row]["g_WJX"] = string.IsNullOrEmpty(mrsDj_Filter["WJX"]) ? "0" : mrsDj_Filter["WJX"];
                        m_dxltab[row]["g_WJ"] = string.IsNullOrEmpty(mrsDj_Filter["WJ"]) ? "0" : mrsDj_Filter["WJ"];
                        m_dxltab[row]["G_DTDZ"] = string.IsNullOrEmpty(mrsDj_Filter["DTDZ"]) ? "0" : mrsDj_Filter["DTDZ"];
                        m_dxltab[row]["G_NY"] = string.IsNullOrEmpty(mrsDj_Filter["NY"]) ? "0" : mrsDj_Filter["NY"];
                        m_dxltab[row]["G_JYDZ"] = string.IsNullOrEmpty(mrsDj_Filter["JYDZ"]) ? "0" : mrsDj_Filter["JYDZ"];
                        m_dxltab[row]["G_QQD"] = string.IsNullOrEmpty(mrsDj_Filter["QQD"]) ? "0" : mrsDj_Filter["QQD"];
                        m_dxltab[row]["G_HQD"] = string.IsNullOrEmpty(mrsDj_Filter["HQD"]) ? "0" : mrsDj_Filter["HQD"];
                        m_dxltab[row]["G_QSCL"] = string.IsNullOrEmpty(mrsDj_Filter["QSCL"]) ? "0" : mrsDj_Filter["QSCL"];
                        m_dxltab[row]["G_HSCL"] = string.IsNullOrEmpty(mrsDj_Filter["HSCL"]) ? "0" : mrsDj_Filter["HSCL"];
                        m_dxltab[row]["G_KLBHL"] = string.IsNullOrEmpty(mrsDj_Filter["KLBHL"]) ? "0" : mrsDj_Filter["KLBHL"];
                        m_dxltab[row]["G_SCBHL"] = string.IsNullOrEmpty(mrsDj_Filter["SCBHL"]) ? "0" : mrsDj_Filter["SCBHL"];
                        m_dxltab[row]["G_BZ"] = string.IsNullOrEmpty(mrsDj_Filter["BZ"]) ? "0" : mrsDj_Filter["BZ"];
                        m_dxltab[row]["G_DTJG1"] = string.IsNullOrEmpty(mrsDj_Filter["DTJG1"]) ? "0" : mrsDj_Filter["DTJG1"];
                        m_dxltab[row]["G_DTJG2"] = string.IsNullOrEmpty(mrsDj_Filter["DTJG2"]) ? "0" : mrsDj_Filter["DTJG2"];
                        m_dxltab[row]["G_XXSB"] = string.IsNullOrEmpty(mrsDj_Filter["XXSB"]) ? "0" : mrsDj_Filter["XXSB"];
                        m_dxltab[row]["G_HTHD"] = string.IsNullOrEmpty(mrsDj_Filter["HTHD"]) ? "0" : mrsDj_Filter["HTHD"];
                        m_dxltab[row]["G_HZBHD"] = string.IsNullOrEmpty(mrsDj_Filter["HZBHD"]) ? "0" : mrsDj_Filter["HZBHD"];
                        m_dxltab[row]["G_HQQD"] = string.IsNullOrEmpty(mrsDj_Filter["HQQD"]) ? "0" : mrsDj_Filter["HQQD"];
                        m_dxltab[row]["G_HHQD"] = string.IsNullOrEmpty(mrsDj_Filter["HHQD"]) ? "0" : mrsDj_Filter["HHQD"];
                        m_dxltab[row]["G_HQSCL"] = string.IsNullOrEmpty(mrsDj_Filter["HQSCL"]) ? "0" : mrsDj_Filter["HQSCL"];
                        m_dxltab[row]["G_HHSCL"] = string.IsNullOrEmpty(mrsDj_Filter["HHSCL"]) ? "0" : mrsDj_Filter["HHSCL"];
                        m_dxltab[row]["G_HKLBHL"] = string.IsNullOrEmpty(mrsDj_Filter["HKLBHL"]) ? "0" : mrsDj_Filter["HKLBHL"];
                        m_dxltab[row]["G_HSCBHL"] = string.IsNullOrEmpty(mrsDj_Filter["HSCBHL"]) ? "0" : mrsDj_Filter["HSCBHL"];
                        mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "0" : mrsDj_Filter["JSFF"];
                    }
                    else
                    {
                        mJSFF = "";
                        m_dxltab[row]["BGBH"] = "";
                        item["JCJG"] = "依据不详";
                        m_dxltab[row]["JSBEIZHU"] = m_dxltab[row]["JSBEIZHU"] + "单组流水号:" + item["DZBH"] + "试件尺寸为空";
                    }
                    //旋转裂纹、旋转盖板、旋转缩松、旋转抗滑、旋转破坏、旋转力矩、旋转一般项、对接裂纹、对接盖板、对接缩松、对接抗拉、对接力矩、对接一般项
                    if (DateTime.Compare(GetSafeDateTime(m_dxltab[row]["SYRQ"]), GetSafeDateTime("2007 -05-1")) > 0)
                        m_dxltab[row]["WHICH"] = "1";
                    #endregion
                    int mbhggs = 0;
                    if (jcxmitem.Contains("标志"))
                    {
                        if (m_dxltab[row]["BZ_HG"].Trim() == "符合")
                        { }
                        else
                            mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        m_dxltab[row]["G_BZ"] = "----";
                        m_dxltab[row]["BZ"] = "----";
                        m_dxltab[row]["BZ_HG"] = "----";
                    }
                    if (jcxmitem.Contains("绝缘线芯识别"))
                    {
                        if (m_dxltab[row]["XXSB"].Trim() == "符合")
                            m_dxltab[row]["XXSB_HG"] = "符合";
                        else
                        {
                            m_dxltab[row]["XXSB_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                    }
                    else
                    {
                        m_dxltab[row]["G_XXSB"] = "----";
                        m_dxltab[row]["XXSB"] = "----";
                        m_dxltab[row]["XXSB_HG"] = "----";
                    }
                    if (jcxmitem.Contains("结构尺寸"))
                    {
                        if (m_dxltab[row]["DTJG1_HG"] == "" || m_dxltab[row]["DTJG1_HG"] == "----")
                        {
                            m_dxltab[row]["DTJG1"] = "----";
                            m_dxltab[row]["G_DTJG1"] = "----";
                            m_dxltab[row]["DTJG1_HG"] = "----";
                        }
                        if (m_dxltab[row]["WJ"] == "0")
                        {
                            m_dxltab[row]["WJ_HG"] = "----";
                            m_dxltab[row]["G_WJ"] = "0";
                            m_dxltab[row]["G_WJX"] = "0";
                        }
                        else
                        {
                            if (GetSafeDouble(m_dxltab[row]["WJ"]) < GetSafeDouble(m_dxltab[row]["G_WJX"]) || GetSafeDouble(m_dxltab[row]["WJ"]) > GetSafeDouble(m_dxltab[row]["G_WJ"]))
                            {
                                m_dxltab[row]["WJ_HG"] = "不符合";
                                mbhggs = mbhggs + 1;
                            }
                            else
                                m_dxltab[row]["WJ_HG"] = "符合";
                        }
                        if (item["SFFJ"] == "0")
                        {
                            m_dxltab[row]["DTJG1"] = "----";
                            m_dxltab[row]["G_DTJG1"] = "----";
                            m_dxltab[row]["DTJG1_HG"] = "----";
                            if (Math.Round(GetSafeDouble(m_dxltab[row]["JYHD"]), 1) < GetSafeDouble(m_dxltab[row]["G_JYHD"]))
                            {
                                m_dxltab[row]["JYHD_HG"] = "不符合";
                                mbhggs = mbhggs + 1;
                            }
                            else
                                m_dxltab[row]["JYHD_HG"] = "符合";
                            if (GetSafeDouble(m_dxltab[row]["ZBHD"]) < GetSafeDouble(m_dxltab[row]["G_ZBHD"]))
                            {
                                m_dxltab[row]["ZBHD_HG"] = "不符合";
                                mbhggs = mbhggs + 1;
                            }
                            else
                                m_dxltab[row]["ZBHD_HG"] = "符合";
                        }
                        else
                        {
                            m_dxltab[row]["G_JYHD"] = "0";
                            m_dxltab[row]["G_ZBHD"] = "0";
                            m_dxltab[row]["G_WJ"] = "0";
                            m_dxltab[row]["JYHD_HG"] = "----";
                            m_dxltab[row]["ZBHD_HG"] = "----";
                            m_dxltab[row]["HTHD_HG"] = "----";
                            m_dxltab[row]["HZBHD_HG"] = "----";
                            m_dxltab[row]["G_WJX"] = "0";
                            m_dxltab[row]["WJ_HG"] = "----";
                        }
                    }
                    else
                    {
                        m_dxltab[row]["G_JYHD"] = "0";
                        m_dxltab[row]["G_ZBHD"] = "0";
                        m_dxltab[row]["G_WJ"] = "0";
                        m_dxltab[row]["JYHD_HG"] = "----";
                        m_dxltab[row]["ZBHD_HG"] = "----";
                        m_dxltab[row]["HTHD_HG"] = "----";
                        m_dxltab[row]["HZBHD_HG"] = "----";
                        m_dxltab[row]["G_WJX"] = "0";
                        m_dxltab[row]["WJ_HG"] = "----";
                        m_dxltab[row]["DTJG1"] = "----";
                        m_dxltab[row]["G_DTJG1"] = "----";
                        m_dxltab[row]["DTJG1_HG"] = "----";
                    }
                    if (jcxmitem.Contains("护套结构尺寸"))
                    {
                        if (GetSafeDouble(m_dxltab[row]["HTHD"]) < GetSafeDouble(m_dxltab[row]["G_HTHD"]))
                        {
                            m_dxltab[row]["HTHD_HG"] = "不合格";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HTHD_HG"] = "合格";
                        if (GetSafeDouble(m_dxltab[row]["HZBHD"]) < GetSafeDouble(m_dxltab[row]["G_HZBHD"]))
                        {
                            m_dxltab[row]["HZBHD_HG"] = "不合格";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HZBHD_HG"] = "合格";
                    }
                    else
                    {
                        m_dxltab[row]["HTHD_HG"] = "----";
                        m_dxltab[row]["HZBHD_HG"] = "----";
                    }
                    if (jcxmitem.Contains("导体根数"))
                    {
                        if (GetSafeDouble(m_dxltab[row]["DTJG2"]) < GetSafeDouble(m_dxltab[row]["G_DTJG2"]))
                        {
                            m_dxltab[row]["DTJG2_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["DTJG2_HG"] = "符合";
                    }
                    else
                    {
                        m_dxltab[row]["DTJG2"] = "-1";
                        m_dxltab[row]["G_DTJG2"] = "0";
                        m_dxltab[row]["DTJG2_hg"] = "----";
                    }
                    if (jcxmitem.Contains("导体电阻"))
                    {
                        if (GetSafeDouble(m_dxltab[row]["DTDZl"]) != 0)
                            m_dxltab[row]["DTDZ"] = Math.Round(GetSafeDouble(m_dxltab[row]["DTDZRT"]) * (254.5 / (234.5 + GetSafeDouble(m_dxltab[row]["DTDZT"]))) / GetSafeDouble(m_dxltab[row]["DTDZL"]), 2).ToString();
                        if (GetSafeDouble(m_dxltab[row]["DTDZ"]) > GetSafeDouble(m_dxltab[row]["G_DTDZ"]))
                        {
                            m_dxltab[row]["DTDZ_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["DTDZ_HG"] = "符合";
                    }
                    else
                    {
                        m_dxltab[row]["DTDZ"] = "-1";
                        m_dxltab[row]["G_DTDZ"] = "0";
                        m_dxltab[row]["DTDZ_hg"] = "----";
                    }
                    if (jcxmitem.Contains("耐压试验"))
                    {
                        if (m_dxltab[row]["NY"].Trim() == "符合" || m_dxltab[row]["NY"].Trim() == "未击穿")
                            m_dxltab[row]["NY_HG"] = "符合";
                        else
                        {
                            m_dxltab[row]["NY_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                    }
                    else
                    {
                        m_dxltab[row]["G_NY"] = "----";
                        m_dxltab[row]["NY"] = "----";
                        m_dxltab[row]["BY_HG"] = "----";
                    }
                    if (jcxmitem.Contains("垂直燃烧试验"))
                    {
                        m_dxltab[row]["G_CZRS1"] = "上支架下缘和碳化部分起始点之间的距离＞50mm";
                        m_dxltab[row]["G_CZRS2"] = "且燃烧向下延伸至距离上支架下缘≤540mm";
                        if (GetSafeDouble(m_dxltab[row]["CZRSSY1"]) > 50 && GetSafeDouble(m_dxltab[row]["CZRSSY2"]) <= 540)
                            m_dxltab[row]["CZRS_HG"] = "符合";
                        else
                        {
                            m_dxltab[row]["CZRS_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                    }
                    else
                    {
                        m_dxltab[row]["G_CZRS1"] = "----";
                        m_dxltab[row]["G_CZRS2"] = "----";
                        m_dxltab[row]["CZRS_HG"] = "----";
                    }
                    if (jcxmitem.Contains("绝缘电阻"))
                    {
                        m_dxltab[row]["JYDZ"] = Math.Round(GetSafeDouble(m_dxltab[row]["JYDZ"]), 3).ToString("0.000");
                        if (GetSafeDouble(m_dxltab[row]["JYDZ"]) == 0)
                            m_dxltab[row]["JYDZ"] = m_dxltab[row]["JYDZDD"];
                        if (GetSafeDouble(m_dxltab[row]["JYDZ"]) < GetSafeDouble(m_dxltab[row]["G_JYDZ"]))
                        {
                            m_dxltab[row]["JYDZ_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["JYDZ_HG"] = "符合";
                    }
                    else
                    {
                        m_dxltab[row]["G_JYDZ"] = "0";
                        m_dxltab[row]["JYDZ_HG"] = "----";
                    }
                    if (jcxmitem.Contains("绝缘物理机械性能试验"))
                    {
                        if (GetSafeDouble(m_dxltab[row]["QQD"]) < GetSafeDouble(m_dxltab[row]["G_QQD"]))
                        {
                            m_dxltab[row]["QQD_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["QQD_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["HQD"]) < GetSafeDouble(m_dxltab[row]["G_HQD"]))
                        {
                            m_dxltab[row]["HQD_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HQD_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["QSCL"]) < GetSafeDouble(m_dxltab[row]["G_QSCL"]))
                        {
                            m_dxltab[row]["QSCL_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["QSCL_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["HSCL"]) < GetSafeDouble(m_dxltab[row]["G_HSCL"]))
                        {
                            m_dxltab[row]["HSCL_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HSCL_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["KLBHL"]) < GetSafeDouble(m_dxltab[row]["G_KLBHL"]))
                        {
                            m_dxltab[row]["KLBHL_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["KLBHL_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["SCBHL"]) < GetSafeDouble(m_dxltab[row]["G_SCBHL"]))
                        {
                            m_dxltab[row]["SCBHL_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["SCBHL_HG"] = "符合";
                    }
                    else
                    {
                        m_dxltab[row]["G_QQD"] = "0";
                        m_dxltab[row]["G_HQD"] = "0";
                        m_dxltab[row]["G_QSCL"] = "0";
                        m_dxltab[row]["G_HSCL"] = "0";
                        m_dxltab[row]["G_KLBHL"] = "0";
                        m_dxltab[row]["G_SCBHL"] = "0";
                        m_dxltab[row]["QQD_HG"] = "----";
                        m_dxltab[row]["HQD_HG"] = "----";
                        m_dxltab[row]["QSCL_HG"] = "----";
                        m_dxltab[row]["HSCL_HG"] = "----";
                        m_dxltab[row]["KLBHL_HG"] = "----";
                        m_dxltab[row]["SCBHL_HG"] = "----";
                    }
                    if (jcxmitem.Contains("护套物理机械性能试验"))
                    {
                        if (GetSafeDouble(m_dxltab[row]["HQQD"]) < GetSafeDouble(m_dxltab[row]["G_HQQD"]))
                        {
                            m_dxltab[row]["HQQD_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HQQD_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["HHQD"]) < GetSafeDouble(m_dxltab[row]["G_HHQD"]))
                        {
                            m_dxltab[row]["HHQD_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HHQD_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["HQSCL"]) < GetSafeDouble(m_dxltab[row]["G_HQSCL"]))
                        {
                            m_dxltab[row]["HQSCL_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HQSCL_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["HHSCL"]) < GetSafeDouble(m_dxltab[row]["G_HHSCL"]))
                        {
                            m_dxltab[row]["HHSCL_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HHSCL_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["HKLBHL"]) < GetSafeDouble(m_dxltab[row]["G_HKLBHL"]))
                        {
                            m_dxltab[row]["HKLBHL_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HKLBHL_HG"] = "符合";
                        if (GetSafeDouble(m_dxltab[row]["HSCBHL"]) < GetSafeDouble(m_dxltab[row]["G_HSCBHL"]))
                        {
                            m_dxltab[row]["HSCBHL_HG"] = "不符合";
                            mbhggs = mbhggs + 1;
                        }
                        else
                            m_dxltab[row]["HSCBHL_HG"] = "符合";
                    }
                    else
                    {
                        m_dxltab[row]["G_HQQD"] = "0";
                        m_dxltab[row]["G_HHQD"] = "0";
                        m_dxltab[row]["G_HQSCL"] = "0";
                        m_dxltab[row]["G_HHSCL"] = "0";
                        m_dxltab[row]["G_HKLBHL"] = "0";
                        m_dxltab[row]["G_HSCBHL"] = "0";
                        m_dxltab[row]["HQQD_HG"] = "----";
                        m_dxltab[row]["HHQD_HG"] = "----";
                        m_dxltab[row]["HQSCL_HG"] = "----";
                        m_dxltab[row]["HHSCL_HG"] = "----";
                        m_dxltab[row]["HKLBHL_HG"] = "----";
                        m_dxltab[row]["HSCBHL_HG"] = "----";
                    }

                    if (mbhggs > 0)
                    {
                        item["JCJG"] = "不合格";
                        s_rwtab[row]["JCJG"] = "不合格";
                        s_rwtab[row]["JCJGMS"] = "所检测项目不符合标准要求";
                    }
                    else
                    {
                        item["JCJG"] = "合格";
                        s_rwtab[row]["JCJG"] = "合格";
                        s_rwtab[row]["JCJGMS"] = "所检测项目符合标准要求";
                    }

                    mAllHg = (mAllHg && item["JCJG"].ToString() == "合格");

                    if (mAllHg)
                    {
                        m_dxltab[row]["JCJG"] = "合格";
                        m_dxltab[row]["JGSM"] = "该组试样所检项目符合标准要求。";
                    }
                    else
                    {
                        m_dxltab[row]["JCJG"] = "不合格";
                        m_dxltab[row]["JGSM"] = "该组试样不符合标准要求。";
                    }
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
        }
    }
}
