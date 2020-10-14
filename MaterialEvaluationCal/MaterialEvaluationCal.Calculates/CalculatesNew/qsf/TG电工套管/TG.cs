using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TGL : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_TG_DJ"];
            var data = retData;

            var SItem = data["S_TG"];
            var MItem = data["M_TG"];
            bool sign = true;
            string mJSFF = "";
            double zj1 = 0, zj2 = 0, mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["GGXH"] == sItem["GGXH"].Trim());
                if (null != extraFieldsDj)
                {
                    MItem[0]["G_WG"] = string.IsNullOrEmpty(extraFieldsDj["WG"]) ? extraFieldsDj["WG"] : extraFieldsDj["WG"].Trim();
                    MItem[0]["G_ZDWJ"] = string.IsNullOrEmpty(extraFieldsDj["ZDWJ"]) ? extraFieldsDj["ZDWJ"] : extraFieldsDj["ZDWJ"].Trim();
                    MItem[0]["G_ZXWJ"] = string.IsNullOrEmpty(extraFieldsDj["ZXWJ"]) ? extraFieldsDj["ZXWJ"] : extraFieldsDj["ZXWJ"].Trim();
                    MItem[0]["G_ZXNJ"] = string.IsNullOrEmpty(extraFieldsDj["ZXNJ"]) ? extraFieldsDj["ZXNJ"] : extraFieldsDj["ZXNJ"].Trim();
                    MItem[0]["G_ZXBH"] = string.IsNullOrEmpty(extraFieldsDj["ZXBH"]) ? extraFieldsDj["ZXBH"] : extraFieldsDj["ZXBH"].Trim();
                    //MItem[0]["G_WQ1"] = string.IsNullOrEmpty(extraFieldsDj["WQ1"]) ? extraFieldsDj["WQ1"] : extraFieldsDj["WQ1"].Trim();
                    //MItem[0]["G_WQ2"] = string.IsNullOrEmpty(extraFieldsDj["WQ2"]) ? extraFieldsDj["WQ2"] : extraFieldsDj["WQ2"].Trim();
                    MItem[0]["G_KYXN1"] = string.IsNullOrEmpty(extraFieldsDj["KYXN1"]) ? extraFieldsDj["KYXN1"] : extraFieldsDj["KYXN1"].Trim();
                    MItem[0]["G_KYXN2"] = string.IsNullOrEmpty(extraFieldsDj["KYXN2"]) ? extraFieldsDj["KYXN2"] : extraFieldsDj["KYXN2"].Trim();
                    MItem[0]["G_ZRXN"] = string.IsNullOrEmpty(extraFieldsDj["ZRXN"]) ? extraFieldsDj["ZRXN"] : extraFieldsDj["ZRXN"].Trim();
                    MItem[0]["G_DLSY"] = string.IsNullOrEmpty(extraFieldsDj["DLSY"]) ? extraFieldsDj["DLSY"] : extraFieldsDj["DLSY"].Trim();
                    //MItem[0]["G_DQSY"] = string.IsNullOrEmpty(extraFieldsDj["DQXN"]) ? extraFieldsDj["DQXN"] : extraFieldsDj["DQXN"].Trim();
                    //MItem[0]["G_JYQD"] = string.IsNullOrEmpty(extraFieldsDj["JYQD"]) ? extraFieldsDj["JYQD"] : extraFieldsDj["JYQD"].Trim();
                    MItem[0]["G_KCJ"] = string.IsNullOrEmpty(extraFieldsDj["KCJ"]) ? extraFieldsDj["KCJ"] : extraFieldsDj["KCJ"].Trim();
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 外观要求
                if (jcxm.Contains("、外观要求、"))
                {
                    jcxmCur = "外观要求";
                    if (MItem[0]["WG_HG"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["WG_HG"] = "----";
                    MItem[0]["WG"] = "----";
                    MItem[0]["G_WG"] = "----";
                }
                #endregion

                #region 最大外径
                if (jcxm.Contains("、最大外径、"))
                {
                    jcxmCur = "最大外径";
                    if (MItem[0]["ZDWJ"] == "量规能通过套管")
                    {
                        MItem[0]["ZDWJ_HG"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["ZDWJ_HG"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["ZDWJ_HG"] = "----";
                    MItem[0]["ZDWJ"] = "----";
                    MItem[0]["G_ZDWJ"] = "----";
                }
                #endregion

                #region 最小外径
                if (jcxm.Contains("、最小外径、"))
                {
                    jcxmCur = "最小外径";
                    if (MItem[0]["ZXWJ"] == "不能通过量规")
                    {
                        MItem[0]["ZXWJ_HG"] = "合格";
                    }
                    else
                    {
                        MItem[0]["ZXWJ_HG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["ZXWJ_HG"] = "----";
                    MItem[0]["ZXWJ"] = "----";
                    MItem[0]["G_ZXWJ"] = "----";
                }
                #endregion

                #region 最小内径
                if (jcxm.Contains("、最小内径、"))
                {
                    jcxmCur = "最小内径";
                    if (MItem[0]["ZXNJ"] == "能通过量规")
                    {
                        MItem[0]["ZXNJ_HG"] = "合格";
                    }
                    else
                    {
                        MItem[0]["ZXNJ_HG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["ZXNJ_HG"] = "----";
                    MItem[0]["ZXNJ"] = "----";
                    MItem[0]["G_ZXNJ"] = "----";
                }
                #endregion

                #region 最小壁厚
                if (jcxm.Contains("、最小壁厚、"))
                {
                    jcxmCur = "最小壁厚";

                    MItem[0]["ZXBH"] = Round((GetSafeDouble(sItem["BH1"]) + GetSafeDouble(sItem["BH2"]) + GetSafeDouble(sItem["BH3"]) + GetSafeDouble(sItem["BH4"])) / 4, 1).ToString();

                    if (!string.IsNullOrEmpty(MItem[0]["ZXBH"]) && !string.IsNullOrEmpty(MItem[0]["G_ZXBH"]) && (double.Parse(MItem[0]["ZXBH"]) >= double.Parse(MItem[0]["G_ZXBH"])))
                    {
                        MItem[0]["ZXBH_HG"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["ZXBH_HG"] = "不合格";
                        mAllHg = false;
                    }

                    if (!string.IsNullOrEmpty(MItem[0]["G_ZXBH"]))
                    {
                        MItem[0]["G_ZXBH"] = "" + MItem[0]["G_ZXBH"];
                    }

                    //壁厚均匀度
                    jcxmCur = "壁厚均匀度";
                    List<double> LArray = new List<double>();
                    for (int i = 1; i < 13; i++)
                    {
                        LArray.Add(GetSafeDouble(sItem["BHJYD" + i]));
                    }
                    double avg = 0;
                    avg = LArray.Average();
                    MItem[0]["G_BHJYD"] = "-" + (0.1 + 0.1 * avg).ToString("0.0") + "~" + (0.1 + 0.1 * avg).ToString("0.0");

                    LArray.Clear();
                    for (int i = 1; i < 13; i++)
                    {
                        LArray.Add(GetSafeDouble(sItem["BHJYD" + i]) - avg);
                    }
                    MItem[0]["BHJYD"] = LArray.Min().ToString("0.0") + "~" + LArray.Max().ToString("0.0");

                    string g_BHJYD = MItem[0]["BHJYD"];
                    List<string> listBHJYD = new List<string>();
                    if (g_BHJYD.Contains("~") && (g_BHJYD.Split('~').Count() == 2))
                    {
                        listBHJYD = g_BHJYD.Split('~').ToList();

                        if (listBHJYD.Count() == 2)
                        {
                            var flag = IsQualified(MItem[0]["G_BHJYD"], listBHJYD[0]);

                            if (IsQualified(MItem[0]["G_BHJYD"], listBHJYD[0], false) == "合格" && IsQualified(MItem[0]["G_BHJYD"], listBHJYD[1], false) == "合格")
                            {
                                MItem[0]["BHJYD_HG"] = "合格";
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["BHJYD_HG"] = "不合格";
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["BHJYD_HG"] = "不合格";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["BHJYD_HG"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["ZXBH"] = "----";
                    MItem[0]["G_ZXBH"] = "----";
                    MItem[0]["ZXBH_HG"] = "----";
                    MItem[0]["BHJYD"] = "----";
                    MItem[0]["G_BHJYD"] = "----";
                    MItem[0]["BHJYD_HG"] = "----";
                }
                #endregion

                #region 抗压性能
                if (jcxm.Contains("、抗压性能、"))
                {
                    jcxmCur = "抗压性能";
                    double kyxn0_1 = 0, kyxn0_2 = 0, kyxn0_3 = 0, b1 = 0;
                    kyxn0_1 = GetSafeDouble(MItem[0]["KYXN0_1"]);
                    kyxn0_2 = GetSafeDouble(MItem[0]["KYXN0_2"]);
                    kyxn0_3 = GetSafeDouble(MItem[0]["KYXN0_3"]);

                    if (kyxn0_1 != 0 && kyxn0_2 != 0 && kyxn0_3 != 0)
                    {
                        MItem[0]["KYJBX1_1"] = ((kyxn0_1 - GetSafeDouble(MItem[0]["KYXN1_1"])) / kyxn0_1 * 100).ToString("0.0");
                        MItem[0]["KYJBX1_2"] = ((kyxn0_2 - GetSafeDouble(MItem[0]["KYXN1_2"])) / kyxn0_2 * 100).ToString("0.0");
                        MItem[0]["KYJBX1_3"] = ((kyxn0_3 - GetSafeDouble(MItem[0]["KYXN1_3"])) / kyxn0_3 * 100).ToString("0.0");

                        MItem[0]["KYXBX2_1"] = ((kyxn0_1 - GetSafeDouble(MItem[0]["KYXN2_1"])) / kyxn0_1 * 100).ToString("0.0");
                        MItem[0]["KYXBX2_2"] = ((kyxn0_2 - GetSafeDouble(MItem[0]["KYXN2_2"])) / kyxn0_2 * 100).ToString("0.0");
                        MItem[0]["KYXBX2_3"] = ((kyxn0_3 - GetSafeDouble(MItem[0]["KYXN2_3"])) / kyxn0_3 * 100).ToString("0.0");

                        b1 = GetSafeDouble(MItem[0]["KYJBX1_1"]);
                        //b1 = GetSafeDouble(MItem[0]["KYJBX1_2"]) > b1 ? GetSafeDouble(MItem[0]["KYXBX1_2"]) : b1;
                        //b1 = GetSafeDouble(MItem[0]["KYXBX1_3"]) > b1 ? GetSafeDouble(MItem[0]["KYXBX1_3"]) : b1;
                        MItem[0]["G_KYXN"] = "荷载1 min 时，Df ≤" + Conversion.Val(MItem[0]["G_KYXN1"]).ToString("0") + "% 卸载1 min 时，Df ≤" + Conversion.Val(MItem[0]["G_KYXN2"]).ToString("0") + "%";
                        //MItem[0]["KYXN1"] = b1.ToString("0.0") + "&";

                        b1 = GetSafeDouble(MItem[0]["KYXBX2_1"]);
                        b1 = GetSafeDouble(MItem[0]["KYXBX2_2"]) > b1 ? GetSafeDouble(MItem[0]["KYXBX2_2"]) : b1;
                        b1 = GetSafeDouble(MItem[0]["KYXBX2_3"]) > b1 ? GetSafeDouble(MItem[0]["KYXBX2_3"]) : b1;
                        //MItem[0]["KYXN2"] = b1.ToString("0.0") + "&";

                        if (GetSafeDouble(MItem[0]["KYJBX1_1"]) > GetSafeDouble(MItem[0]["G_KYXN1"]) || GetSafeDouble(MItem[0]["KYJBX1_2"]) > GetSafeDouble(MItem[0]["G_KYXN1"]) || GetSafeDouble(MItem[0]["KYJBX1_3"]) > GetSafeDouble(MItem[0]["G_KYXN1"]))
                        {
                            MItem[0]["KYXN1_HG"] = "不合格";
                            mAllHg = false;
                        }
                        else
                        {
                            MItem[0]["KYXN1_HG"] = "合格";
                        }

                        if (GetSafeDouble(MItem[0]["KYXBX2_1"]) > GetSafeDouble(MItem[0]["G_KYXN2"]) || GetSafeDouble(MItem[0]["KYXBX2_2"]) > GetSafeDouble(MItem[0]["G_KYXN2"]) || GetSafeDouble(MItem[0]["KYXBX2_3"]) > GetSafeDouble(MItem[0]["G_KYXN2"]))
                        {
                            MItem[0]["KYXN2_HG"] = "不合格";
                            mAllHg = false;
                        }
                        else
                        {
                            MItem[0]["KYXN2_HG"] = "合格";
                        }
                        if (MItem[0]["KYXN1_HG"] == "合格" && MItem[0]["KYXN2_HG"] == "合格")
                        {
                            MItem[0]["HG_KYXN"] = "合格";
                        }
                        else
                        {
                            MItem[0]["HG_KYXN"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    MItem[0]["KYXN1_1"] = "0";
                    MItem[0]["KYXN1_2"] = "0";
                    MItem[0]["KYXN1_3"] = "0";
                    MItem[0]["G_KYXN1"] = "0";
                    MItem[0]["KYXN2_1"] = "0";
                    MItem[0]["KYXN2_2"] = "0";
                    MItem[0]["KYXN2_3"] = "0";
                    MItem[0]["G_KYXN2"] = "0";
                    MItem[0]["KYXN1_HG"] = "----";
                    MItem[0]["KYXN2_HG"] = "----";
                }
                #endregion

                #region 抗冲击性能
                if (jcxm.Contains("、抗冲击性能、"))
                {
                    jcxmCur = "抗冲击性能";
                    if (MItem[0]["KCJ_HG"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["KCJ"] = "----";
                    MItem[0]["G_KCJ"] = "----";
                    MItem[0]["KCJ_HG"] = "----";
                }
                #endregion

                #region 弯曲性能
                if (jcxm.Contains("、弯曲性能、"))
                {
                    jcxmCur = "弯曲性能";
                    MItem[0]["G_WQXN"] = "无可见裂纹";
                    MItem[0]["HG_WQXN"] = "";
                    for (int i = 1; i < 4; i++)
                    {
                        if (sItem["WQXNCW" + i] == "否" || sItem["WQXNDW" + i] == "否")
                        {
                            MItem[0]["HG_WQXN"] = "不合格";
                        }
                    }
                    if (MItem[0]["HG_WQXN"] != "不合格")
                    {
                        MItem[0]["HG_WQXN"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_WQXN"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["HG_WQXN"] = "----";
                    MItem[0]["G_WQXN"] = "----";
                }
                #endregion

                #region 弯扁性能
                if (jcxm.Contains("、弯扁性能、"))
                {
                    jcxmCur = "弯扁性能";
                    MItem[0]["G_WBXN"] = "量规自重通过 ";
                    MItem[0]["HG_WBXN"] = "";
                    for (int i = 1; i < 4; i++)
                    {
                        if (sItem["WBXN" + i] == "否")
                        {
                            MItem[0]["HG_WBXN"] = "不合格";
                        }
                    }
                    if (MItem[0]["HG_WBXN"] != "不合格")
                    {
                        MItem[0]["HG_WBXN"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_WBXN"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["HG_WBXN"] = "----";
                    MItem[0]["G_WBXN"] = "----";
                }
                #endregion

                #region 跌落性能
                if (jcxm.Contains("、跌落性能、"))
                {
                    jcxmCur = "跌落性能";
                    MItem[0]["G_DLXN"] = "无震裂、破碎";
                    MItem[0]["HG_DLXN"] = "";
                    for (int i = 1; i < 4; i++)
                    {
                        if (sItem["DLXN" + i] == "否")
                        {
                            MItem[0]["HG_DLXN"] = "不合格";
                        }
                    }
                    if (MItem[0]["HG_DLXN"] != "不合格")
                    {
                        MItem[0]["HG_DLXN"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_DLXN"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["HG_DLXN"] = "----";
                    MItem[0]["G_DLXN"] = "----";
                }
                #endregion

                #region 耐热性能
                if (jcxm.Contains("、耐热性能、"))
                {
                    jcxmCur = "耐热性能";
                    MItem[0]["G_NRXN"] = "≤2mm";
                    MItem[0]["HG_NRXN"] = "";
                    for (int i = 1; i < 4; i++)
                    {
                        if (Conversion.Val(sItem["NRXN" + i]) > 2)
                        {
                            MItem[0]["HG_NRXN"] = "不合格";
                        }
                    }
                    if (MItem[0]["HG_NRXN"] != "不合格")
                    {
                        MItem[0]["HG_NRXN"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_NRXN"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                else
                {
                    MItem[0]["HG_NRXN"] = "----";
                    MItem[0]["G_NRXN"] = "----";
                }
                #endregion
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            if (!data.ContainsKey("M_TG"))
            {
                data["M_TG"] = new List<IDictionary<string, string>>();
            }
            var M_TG = data["M_TG"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_TG == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_TG.Add(m);
            }
            else
            {
                M_TG[0]["JCJG"] = mjcjg;
                M_TG[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion


            /************************ 代码结束 *********************/
            #endregion
        }
    }
}

