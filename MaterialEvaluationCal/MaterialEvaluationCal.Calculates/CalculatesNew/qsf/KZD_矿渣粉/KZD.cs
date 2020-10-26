using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class KZD : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_KZD_DJ"];
            var mrsKqnd = dataExtra["BZ_SNKQND"];
            var data = retData;

            var SItem = data["S_KZD"];
            var MItem = data["M_KZD"];
            bool sign = true;
            string mJSFF = "";
            double zj1 = 0, zj2 = 0, mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim() && u["D_JB"] == sItem["JB"]);
                if (null != extraFieldsDj)
                {
                    sItem["G_MD"] = string.IsNullOrEmpty(extraFieldsDj["D_MD"]) ? extraFieldsDj["D_MD"] : extraFieldsDj["D_MD"].Trim();
                    sItem["G_HXZS7"] = string.IsNullOrEmpty(extraFieldsDj["D_HXXS7"]) ? extraFieldsDj["D_HXXS7"] : extraFieldsDj["D_HXXS7"].Trim();
                    sItem["G_HXZS28"] = string.IsNullOrEmpty(extraFieldsDj["D_HXXS28"]) ? extraFieldsDj["D_HXXS28"] : extraFieldsDj["D_HXXS28"].Trim();
                    sItem["G_LDDB"] = string.IsNullOrEmpty(extraFieldsDj["D_LDDB"]) ? extraFieldsDj["D_LDDB"] : extraFieldsDj["D_LDDB"].Trim();
                    sItem["G_HSL"] = string.IsNullOrEmpty(extraFieldsDj["D_HSL"]) ? extraFieldsDj["D_HSL"] : extraFieldsDj["D_HSL"].Trim();
                    sItem["G_SSL"] = string.IsNullOrEmpty(extraFieldsDj["D_SSL"]) ? extraFieldsDj["D_SSL"] : extraFieldsDj["D_SSL"].Trim();
                    sItem["G_BBMJ"] = string.IsNullOrEmpty(extraFieldsDj["D_BBMJ"]) ? extraFieldsDj["D_BBMJ"] : extraFieldsDj["D_BBMJ"].Trim();
                    sItem["G_CNSJB"] = string.IsNullOrEmpty(extraFieldsDj["D_CNSJB"]) ? extraFieldsDj["D_CNSJB"] : extraFieldsDj["D_CNSJB"].Trim();
                    sItem["G_BRW"] = string.IsNullOrEmpty(extraFieldsDj["D_BRW"]) ? extraFieldsDj["D_BRW"] : extraFieldsDj["D_BRW"].Trim();
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 活性指数
                if (jcxm.Contains("、活性指数、") && !string.IsNullOrEmpty(sItem["G_HXZS7"]) && !string.IsNullOrEmpty(sItem["G_HXZS28"]))
                {
                    double md1 = 0, md2 = 0, md = 0, xd1 = 0, xd2 = 0, xd = 0, ba = 0, kz = 0, b1 = 0;

                    jcxmCur = "活性指数";
                    if (!string.IsNullOrEmpty(sItem["S_JZQD7"]))  //计算
                    {
                        md1 = GetSafeDouble(sItem["S_JZQD7"]);
                        md2 = GetSafeDouble(sItem["S_SJQD7"]);
                        md = md1 != 0 ? Math.Round(md2 * 100 / md1, 1) : 0;

                        xd1 = GetSafeDouble(sItem["S_JZQD28"]);
                        xd2 = GetSafeDouble(sItem["S_SJQD28"]);
                        xd = xd1 != 0 ? Math.Round(xd2 * 100 / xd1) : 0;
                    }
                    else   //直接输入结果
                    {
                        md = GetSafeDouble(sItem["W_HXZS7"]);
                        xd = GetSafeDouble(sItem["W_HXZS28"]);
                    }
                    b1 = GetSafeDouble(sItem["G_HXZS7"]);
                    kz = GetSafeDouble(sItem["G_HXZS28"]);

                    if (md >= b1)
                    {
                        sItem["HXZS7D_GH"] = "合格";
                    }
                    else
                    {
                        sItem["HXZS7D_GH"] = "不合格";
                    }

                    if (xd != 0)  //考虑只做7天的情况
                    {
                        if (xd >= kz)
                        {
                            sItem["HXZS28D_GH"] = "合格";
                        }
                        else
                        {
                            sItem["HXZS28D_GH"] = "不合格";
                        }
                        if (md >= b1 && xd >= kz)
                        {
                            sItem["HXZS_GH"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HXZS_GH"] = "不合格";
                            mAllHg = false;
                        }
                        sItem["G_HXZS7"] = "≥" + sItem["G_HXZS7"];
                        sItem["G_HXZS28"] = "≥" + sItem["G_HXZS28"];
                        sItem["W_HXZS7"] = md.ToString("0");
                        sItem["W_HXZS28"] = xd.ToString("0");
                    }
                    else
                    {
                        if (md >= b1)
                        {
                            sItem["HXZS_GH"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HXZS_GH"] = "不合格";
                            mAllHg = false;
                        }
                        sItem["G_HXZS7"] = "≥" + sItem["G_HXZS7"];
                        sItem["G_HXZS28"] = "----";
                        sItem["W_HXZS7"] = md.ToString("0");
                        sItem["W_HXZS28"] = "----";
                        sItem["HXZS28D_GH"] = "----";
                    }
                }
                else
                {
                    sItem["HXZS_GH"] = "----";
                    sItem["G_HXZS7"] = "----";
                    sItem["G_HXZS28"] = "----";
                    sItem["W_HXZS7"] = "----";
                    sItem["W_HXZS28"] = "----";
                }
                #endregion

                #region 流动度比
                if (jcxm.Contains("、流动度比、") && !string.IsNullOrEmpty(sItem["G_LDDB"]))
                {
                    double md1 = 0, md2 = 0, md = 0, xd1 = 0, xd2 = 0, xd = 0, ba = 0, kz = 0, b1 = 0;

                    jcxmCur = "流动度比";
                    if (!string.IsNullOrEmpty(sItem["S_JZLD1"]))    //计算
                    {
                        md1 = GetSafeDouble(sItem["S_JZLD1"]);
                        md2 = GetSafeDouble(sItem["S_SJLD1"]);
                        md = md1 != 0 ? Math.Round(md2 * 100 / md1, 1) : 0;

                        xd1 = GetSafeDouble(sItem["S_JZLD2"]);
                        xd2 = GetSafeDouble(sItem["S_SJLD2"]);
                        xd = xd1 != 0 ? Math.Round(xd2 * 100 / xd1) : 0;

                        md = Math.Round((md + xd) / 2, 0);
                    }
                    else   //直接输入结果
                    {
                        md = GetSafeDouble(sItem["W_LDDB"]);
                    }
                    b1 = GetSafeDouble(sItem["G_LDDB"]);
                    if (md >= b1)
                    {
                        sItem["LDDB_GH"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["LDDB_GH"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_LDDB"] = "≥" + sItem["G_LDDB"];
                    sItem["W_LDDB"] = md.ToString("0");

                }
                else
                {
                    sItem["LDDB_GH"] = "----";
                    sItem["G_LDDB"] = "----";
                    sItem["W_LDDB"] = "----";
                }
                #endregion

                #region 含水量
                if (jcxm.Contains("、含水量、") && !string.IsNullOrEmpty(sItem["G_HSL"]))
                {

                    double md1 = 0, md2 = 0, md = 0, xd1 = 0, xd2 = 0, b1 = 0, xd = 0;
                    jcxmCur = "含水量";
                    if (!string.IsNullOrEmpty(sItem["S_HGQ1"]))  //计算
                    {
                        md1 = GetSafeDouble(sItem["S_HGQ1"]) - GetSafeDouble(sItem["S_GGZL1"]);
                        md2 = GetSafeDouble(sItem["S_HGH1"]) - GetSafeDouble(sItem["S_GGZL1"]);
                        md = md1 - md2;
                        md = md1 != 0 ? Math.Round(md * 100 / md1, 2) : 0;

                        xd1 = GetSafeDouble(sItem["S_HGQ2"]) - GetSafeDouble(sItem["S_GGZL2"]);
                        xd2 = GetSafeDouble(sItem["S_HGH2"]) - GetSafeDouble(sItem["S_GGZL2"]);
                        xd = xd1 - xd2;
                        xd = xd1 != 0 ? Math.Round(xd * 100 / xd1, 2) : 0;

                        md = Math.Round((md + xd) / 2, 1);
                    }
                    else  //直接输入结果
                    {
                        md = GetSafeDouble(sItem["W_HSL"]);
                    }
                    b1 = GetSafeDouble(sItem["G_HSL"]);
                    if (md <= b1)
                    {
                        sItem["HSL_GH"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HSL_GH"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_HSL"] = "≤" + sItem["G_HSL"];
                    sItem["W_HSL"] = md.ToString("0.0");
                }
                else
                {
                    sItem["HSL_GH"] = "----";
                    sItem["G_HSL"] = "----";
                    sItem["W_HSL"] = "----";
                }
                #endregion

                #region 烧失量
                if (jcxm.Contains("、烧失量、"))
                {
                    jcxmCur = "烧失量";
                    if (Conversion.Val(sItem["W_SSL"]) <= Conversion.Val(sItem["G_SSL"]))
                    {
                        sItem["SSL_GH"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["SSL_GH"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_SSL"] = "≤" + sItem["G_SSL"];
                }
                else
                {
                    sItem["SSL_GH"] = "----";
                    sItem["G_SSL"] = "----";
                    sItem["W_SSL"] = "----";
                }
                #endregion

                #region 比表面积
                if (jcxm.Contains("、比表面积、"))
                {
                    jcxmCur = "比表面积";

                    #region 比表面积计算
                    sItem["MD1"] = ((Round(Conversion.Val(sItem["MDSYZL1"]) / (Conversion.Val(sItem["MDYYTJ1"]) - Conversion.Val(sItem["MDYTJ1"])), 4) * 1000) / 1000).ToString("0.00");
                    sItem["MD2"] = ((Round(Conversion.Val(sItem["MDSYZL2"]) / (Conversion.Val(sItem["MDYYTJ2"]) - Conversion.Val(sItem["MDYTJ2"])), 4) * 1000) / 1000).ToString("0.00");
                    sItem["W_MD"] = ((Conversion.Val(sItem["MD1"]) + Conversion.Val(sItem["MD2"])) / 2).ToString("0.00");

                    if (Conversion.Val(sItem["W_MD"]) != 0 && !string.IsNullOrEmpty(sItem["SYT1"]))
                    {
                        var mrsKqnd_Filter = mrsKqnd.FirstOrDefault(x => x["MC"].Contains(sItem["XZSWD"].Trim()));
                        if (mrsKqnd_Filter != null && mrsKqnd_Filter.Count() > 0)
                            sItem["BYKQYD"] = mrsKqnd_Filter["KQND"];
                        mrsKqnd_Filter = mrsKqnd.FirstOrDefault(x => x["MC"].Contains(sItem["XZSWD2"].Trim()));
                        if (mrsKqnd_Filter != null && mrsKqnd_Filter.Count() > 0)
                            sItem["BYKQYD2"] = mrsKqnd_Filter["KQND"];
                        mrsKqnd_Filter = mrsKqnd.FirstOrDefault(x => x["MC"].Contains(sItem["SYSWD1"].Trim()));
                        if (mrsKqnd_Filter != null && mrsKqnd_Filter.Count() > 0)
                            sItem["SYKQYD1"] = mrsKqnd_Filter["KQND"];
                        mrsKqnd_Filter = mrsKqnd.FirstOrDefault(x => x["MC"].Contains(sItem["SYSWD2"].Trim()));
                        if (mrsKqnd_Filter != null && mrsKqnd_Filter.Count() > 0)
                            sItem["SYKQYD2"] = mrsKqnd_Filter["KQND"];
                        if (Conversion.Val(sItem["W_MD"]) == Conversion.Val(sItem["BYMD"]) && Conversion.Val(sItem["SYKXL1"]) == Conversion.Val(sItem["BYKXL"]))
                        {
                            if (Math.Abs(Conversion.Val(sItem["SYSWD1"]) - Conversion.Val(sItem["XZSWD"])) <= 3)
                                sItem["SYBBMJ1"] = (Conversion.Val(sItem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sItem["SYT1"])) / Math.Sqrt(Conversion.Val(sItem["BYT1"]))).ToString();
                            else
                                sItem["SYBBMJ1"] = (Conversion.Val(sItem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sItem["SYT1"])) * Math.Sqrt(Conversion.Val(sItem["BYKQYD"])) / Math.Sqrt(Conversion.Val(sItem["BYT1"])) / Math.Sqrt(Conversion.Val(sItem["SYKQYD1"]))).ToString();
                        }
                        if (Conversion.Val(sItem["W_MD"]) == Conversion.Val(sItem["BYMD"]) && Conversion.Val(sItem["SYKXL1"]) != Conversion.Val(sItem["BYKXL"]))
                        {
                            if (Math.Abs(Conversion.Val(sItem["SYSWD1"]) - Conversion.Val(sItem["XZSWD"])) <= 3)
                            {
                                double md1 = 0;
                                md1 = Conversion.Val(sItem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sItem["SYT1"])) * (1 - Conversion.Val(sItem["BYKXL"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"])) / (Math.Sqrt(Conversion.Val(sItem["BYT1"])) * (1 - Conversion.Val(sItem["SYKXL1"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"])));
                                sItem["SYBBMJ1"] = Round(md1, 5).ToString();
                            }
                            else
                                sItem["SYBBMJ1"] = (Conversion.Val(sItem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sItem["SYT1"])) * Math.Sqrt(Conversion.Val(sItem["BYKQYD"])) * (1 - Conversion.Val(sItem["BYKXL"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"])) / (Math.Sqrt(Conversion.Val(sItem["BYT1"])) * Math.Sqrt(Conversion.Val(sItem["SYKQYD1"])) * (1 - Conversion.Val(sItem["SYKXL1"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"])))).ToString();

                        }
                        if (Conversion.Val(sItem["W_MD"]) != Conversion.Val(sItem["BYMD"]) && Conversion.Val(sItem["SYKXL1"]) != Conversion.Val(sItem["BYKXL"]))
                        {
                            if (Math.Abs(Conversion.Val(sItem["SYSWD1"]) - Conversion.Val(sItem["XZSWD"])) <= 3)
                                sItem["SYBBMJ1"] = Round(Conversion.Val(sItem["BYBBMJ"]) * Conversion.Val(sItem["BYMD"]) * Math.Sqrt(Conversion.Val(sItem["SYT1"])) * (1 - Conversion.Val(sItem["BYKXL"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"])) / (Conversion.Val(sItem["W_MD"]) * Math.Sqrt(Conversion.Val(sItem["BYT1"])) * (1 - Conversion.Val(sItem["SYKXL1"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]))), 0).ToString();
                            else
                                sItem["SYBBMJ1"] = Round(Conversion.Val(sItem["BYBBMJ"]) * Conversion.Val(sItem["BYMD"]) * Math.Sqrt(Conversion.Val(sItem["SYT1"])) * Math.Sqrt(Conversion.Val(sItem["BYKQYD"])) * (1 - Conversion.Val(sItem["BYKXL"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"])) / (Conversion.Val(sItem["W_MD"]) * Math.Sqrt(Conversion.Val(sItem["BYT1"])) * Math.Sqrt(Conversion.Val(sItem["SYKQYD1"])) * (1 - Conversion.Val(sItem["SYKXL1"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]))), 0).ToString();
                        }
                        //温州
                        if (Conversion.Val(sItem["W_MD"]) != Conversion.Val(sItem["BYMD"]) && Conversion.Val(sItem["SYKXL1"]) == Conversion.Val(sItem["BYKXL"]))
                        {
                            if (Math.Abs(Conversion.Val(sItem["SYSWD1"]) - Conversion.Val(sItem["XZSWD"])) <= 3)
                                sItem["SYBBMJ1"] = Round(Conversion.Val(sItem["BYBBMJ"]) * Conversion.Val(sItem["BYMD"]) * Math.Sqrt(Conversion.Val(sItem["SYT1"])) * (1 - Conversion.Val(sItem["BYKXL"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"])) / (Conversion.Val(sItem["W_MD"]) * Math.Sqrt(Conversion.Val(sItem["BYT1"])) * (1 - Conversion.Val(sItem["SYKXL1"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]))), 0).ToString();
                            else
                                sItem["SYBBMJ1"] = Round(Conversion.Val(sItem["BYBBMJ"]) * Conversion.Val(sItem["BYMD"]) * Math.Sqrt(Conversion.Val(sItem["SYT1"])) * Math.Sqrt(Conversion.Val(sItem["BYKQYD"])) * (1 - Conversion.Val(sItem["BYKXL"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"]) * Conversion.Val(sItem["SYKXL1"])) / (Conversion.Val(sItem["W_MD"]) * Math.Sqrt(Conversion.Val(sItem["BYT1"])) * Math.Sqrt(Conversion.Val(sItem["SYKQYD1"])) * (1 - Conversion.Val(sItem["SYKXL1"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]) * Conversion.Val(sItem["BYKXL"]))), 0).ToString();
                        }
                        //温州
                        //第二次
                        if (Conversion.Val(sItem["W_MD"]) == Conversion.Val(sItem["BYMD2"]) && Conversion.Val(sItem["SYKXL2"]) == Conversion.Val(sItem["BYKXL2"]))
                        {
                            if (Math.Abs(Conversion.Val(sItem["SYSWD2"]) - Conversion.Val(sItem["XZSWD2"])) <= 3)
                                sItem["SYBBMJ2"] = Round(Conversion.Val(sItem["BYBBMJ2"]) * Math.Sqrt(Conversion.Val(sItem["SYT2"])) / Math.Sqrt(Conversion.Val(sItem["BYT2"])), 0).ToString();
                            else
                                sItem["SYBBMJ2"] = Round(Conversion.Val(sItem["BYBBMJ2"]) * Math.Sqrt(Conversion.Val(sItem["SYT2"])) * Math.Sqrt(Conversion.Val(sItem["BYKQYD2"])) / Math.Sqrt(Conversion.Val(sItem["BYT2"])) / Math.Sqrt(Conversion.Val(sItem["SYKQYD2"])), 0).ToString();

                        }
                        if (Conversion.Val(sItem["W_MD"]) == Conversion.Val(sItem["BYMD2"]) && Conversion.Val(sItem["SYKXL2"]) != Conversion.Val(sItem["BYKXL2"]))
                        {
                            if (Math.Abs(Conversion.Val(sItem["SYSWD2"]) - Conversion.Val(sItem["XZSWD2"])) <= 3)
                                sItem["SYBBMJ2"] = Round(Conversion.Val(sItem["BYBBMJ2"]) * Math.Sqrt(Conversion.Val(sItem["SYT2"])) * (1 - Conversion.Val(sItem["BYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"])) / (Math.Sqrt(Conversion.Val(sItem["BYT2"])) * (1 - Conversion.Val(sItem["SYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]))), 0).ToString();
                            else
                                sItem["SYBBMJ2"] = Round(Conversion.Val(sItem["BYBBMJ2"]) * Math.Sqrt(Conversion.Val(sItem["SYT2"])) * Math.Sqrt(Conversion.Val(sItem["BYKQYD2"])) * (1 - Conversion.Val(sItem["BYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"])) / (Math.Sqrt(Conversion.Val(sItem["BYT2"])) * Math.Sqrt(Conversion.Val(sItem["SYKQYD2"])) * (1 - Conversion.Val(sItem["SYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]))), 0).ToString();
                        }
                        if (Conversion.Val(sItem["W_MD"]) != Conversion.Val(sItem["BYMD2"]) && Conversion.Val(sItem["SYKXL2"]) != Conversion.Val(sItem["BYKXL2"]))
                        {
                            if (Math.Abs(Conversion.Val(sItem["SYSWD2"]) - Conversion.Val(sItem["XZSWD2"])) <= 3)
                                sItem["SYBBMJ2"] = Round(Conversion.Val(sItem["BYBBMJ2"]) * Conversion.Val(sItem["BYMD2"]) * Math.Sqrt(Conversion.Val(sItem["SYT2"])) * (1 - Conversion.Val(sItem["BYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"])) / (Conversion.Val(sItem["W_MD"]) * Math.Sqrt(Conversion.Val(sItem["BYT2"])) * (1 - Conversion.Val(sItem["SYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]))), 0).ToString();
                            else
                                sItem["SYBBMJ2"] = Round(Conversion.Val(sItem["BYBBMJ2"]) * Conversion.Val(sItem["BYMD2"]) * Math.Sqrt(Conversion.Val(sItem["SYT2"])) * Math.Sqrt(Conversion.Val(sItem["BYKQYD2"])) * (1 - Conversion.Val(sItem["BYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"])) / (Conversion.Val(sItem["W_MD"]) * Math.Sqrt(Conversion.Val(sItem["BYT2"])) * Math.Sqrt(Conversion.Val(sItem["SYKQYD2"])) * (1 - Conversion.Val(sItem["SYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]))), 0).ToString();
                        }

                        //温州

                        if (Conversion.Val(sItem["W_MD"]) != Conversion.Val(sItem["BYMD2"]) && Conversion.Val(sItem["SYKXL2"]) == Conversion.Val(sItem["BYKXL2"]))
                        {
                            if (Math.Abs(Conversion.Val(sItem["SYSWD2"]) - Conversion.Val(sItem["XZSWD2"])) <= 3)
                                sItem["SYBBMJ2"] = Round(Conversion.Val(sItem["BYBBMJ2"]) * Conversion.Val(sItem["BYMD2"]) * Math.Sqrt(Conversion.Val(sItem["SYT2"])) * (1 - Conversion.Val(sItem["BYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"])) / (Conversion.Val(sItem["W_MD"]) * Math.Sqrt(Conversion.Val(sItem["BYT2"])) * (1 - Conversion.Val(sItem["SYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]))), 0).ToString();
                            else
                                sItem["SYBBMJ2"] = Round(Conversion.Val(sItem["BYBBMJ2"]) * Conversion.Val(sItem["BYMD2"]) * Math.Sqrt(Conversion.Val(sItem["SYT2"])) * Math.Sqrt(Conversion.Val(sItem["BYKQYD2"])) * (1 - Conversion.Val(sItem["BYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"]) * Conversion.Val(sItem["SYKXL2"])) / (Conversion.Val(sItem["W_MD"]) * Math.Sqrt(Conversion.Val(sItem["BYT2"])) * Math.Sqrt(Conversion.Val(sItem["SYKQYD2"])) * (1 - Conversion.Val(sItem["SYKXL2"])) * Math.Sqrt(Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]) * Conversion.Val(sItem["BYKXL2"]))), 0).ToString();
                        }
                        //温州
                        sItem["SYBBMJ2"] = Round(Conversion.Val(sItem["SYBBMJ2"]), 0).ToString();
                        sItem["W_BBMJ"] = (Round((Conversion.Val(sItem["SYBBMJ1"]) + Conversion.Val(sItem["SYBBMJ2"])) / 2 / 10, 0) * 10).ToString();
                        //if (Conversion.Val(sItem["SYBBMJ1"]) != 0 && Conversion.Val(sItem["SYBBMJ2"]) != 0)
                        //{
                        //    if (Math.Abs(Conversion.Val(sItem["SYBBMJ1"]) - Conversion.Val(sItem["SYBBMJ2"])) / Conversion.Val(sItem["SYBBMJ1"]) > 0.02 || Math.Abs(Conversion.Val(sItem["SYBBMJ1"]) - Conversion.Val(sItem["SYBBMJ2"])) / Conversion.Val(sItem["SYBBMJ2"]) > 0.02)
                        //    {
                        //        sItem["W_BBMJ"] = "无效";
                        //        mitem["XD_HG"] = "需重做";
                        //        xd_hg = false;
                        //    }
                        //else
                        //{
                        //    if (IsQualified(mitem["G_XD"], sItem["W_BBMJ"], true) == "符合")
                        //    {
                        //        mitem["XD_HG"] = "合格";
                        //        xd_hg = true;
                        //    }
                        //    else
                        //    {
                        //        mitem["XD_HG"] = "不合格";
                        //        if (string.IsNullOrEmpty(bhgJcxm))
                        //        {
                        //            bhgJcxm = bhgJcxm + "比表面积";
                        //        }
                        //        else
                        //        {
                        //            bhgJcxm = bhgJcxm + "、比表面积";
                        //        }
                        //        xd_hg = false;
                        //    }
                        //}
                        //}
                    }
                    #endregion
                    if (Math.Abs(Conversion.Val(sItem["SYBBMJ1"]) - Conversion.Val(sItem["SYBBMJ2"])) / Conversion.Val(sItem["SYBBMJ1"]) > 0.02 || Math.Abs(Conversion.Val(sItem["SYBBMJ1"]) - Conversion.Val(sItem["SYBBMJ2"])) / Conversion.Val(sItem["SYBBMJ2"]) > 0.02)
                    {
                        sItem["W_BBMJ"] = "无效";
                        sItem["BBMJ_GH"] = "需重做";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                    else
                    {
                        if (Conversion.Val(sItem["W_BBMJ"]) >= Conversion.Val(sItem["G_BBMJ"]))
                        {
                            sItem["BBMJ_GH"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["BBMJ_GH"] = "不合格";
                            mAllHg = false;
                        }
                    }
                    sItem["G_BBMJ"] = "≥" + sItem["G_BBMJ"];
                }
                else
                {
                    sItem["BBMJ_GH"] = "----";
                    sItem["G_BBMJ"] = "----";
                    sItem["W_BBMJ"] = "----";
                }
                #endregion

                #region 密度
                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";
                    sItem["MD1"] = ((Round(Conversion.Val(sItem["MDSYZL1"]) / (Conversion.Val(sItem["MDYYTJ1"]) - Conversion.Val(sItem["MDYTJ1"])), 4) * 1000) / 1000).ToString("0.00");
                    sItem["MD2"] = ((Round(Conversion.Val(sItem["MDSYZL2"]) / (Conversion.Val(sItem["MDYYTJ2"]) - Conversion.Val(sItem["MDYTJ2"])), 4) * 1000) / 1000).ToString("0.00");
                    sItem["W_MD"] = ((Conversion.Val(sItem["MD1"]) + Conversion.Val(sItem["MD2"])) / 2).ToString("0.00");
                    if (Conversion.Val(sItem["W_MD"]) >= Conversion.Val(sItem["G_MD"]))
                    {
                        sItem["MD_GH"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["MD_GH"] = "不合格";
                        mAllHg = false;
                    }
                    sItem["G_MD"] = "≥" + sItem["G_MD"];
                }
                else
                {
                    sItem["MD_GH"] = "----";
                    sItem["G_MD"] = "----";
                    sItem["W_MD"] = "----";
                }
                #endregion

                #region 初凝时间比
                if (jcxm.Contains("、初凝时间比、"))
                {
                    jcxmCur = "初凝时间比";
                    if (IsQualified(sItem["G_CNSJB"], sItem["W_CNSJB"], false) == "合格")
                    {
                        sItem["HG_CNSJB"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_CNSJB"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["HG_CNSJB"] = "----";
                    sItem["W_CNSJB"] = "----";
                    sItem["G_CNSJB"] = "----";
                }
                #endregion

                #region 不溶物
                if (jcxm.Contains("、不溶物、"))
                {
                    jcxmCur = "不溶物";
                    List<double> lArray = new List<double>();
                    for (int i = 1; i < 3; i++)
                    {
                        sItem["BRWZLFS" + i] = Math.Round((Conversion.Val(sItem["BRWSHZL" + i]) - Conversion.Val(sItem["BRWKBSHZL" + i])) / Conversion.Val(sItem["BRWSYZL" + i]) * 100, 1).ToString("0.0");
                        lArray.Add(Conversion.Val(sItem["BRWZLFS" + i]));
                    }

                    if (lArray != null)
                    {
                        sItem["W_BRW"] = lArray.Average().ToString();
                    }

                    if (IsQualified(sItem["G_BRW"], sItem["W_BRW"], false) == "合格")
                    {
                        sItem["HG_BRW"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_BRW"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["W_BRW"] = "----";
                    sItem["HG_BRW"] = "----";
                    sItem["G_BRW"] = "----";
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
            if (!data.ContainsKey("M_KZD"))
            {
                data["M_KZD"] = new List<IDictionary<string, string>>();
            }
            var M_KZD = data["M_KZD"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_KZD == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_KZD.Add(m);
            }
            else
            {
                M_KZD[0]["JCJG"] = mjcjg;
                M_KZD[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
