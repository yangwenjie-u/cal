using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates.HPB_混凝土配合比
{
    public class HPB2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            /*  参考标准：
                * JGJ 70-2009 建筑砂浆基本性能试验方法.pdf
             */


            var extraDJ = dataExtra["BZ_HPB_DJ"];
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            var SItems = retData["S_HPB"];

            if (!retData.ContainsKey("M_HPB"))
            {
                retData["M_HPB"] = new List<IDictionary<string, string>>();
            }
            var MItem = retData["M_HPB"];

            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mItemHg = true;
            var jcxm = "";

            double md1, md2, md, pjmd = 0.0;
            int xd, Gs = 0;
            bool flag, sign = false;

            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                #region 用料计算
                if (IsNumeric(MItem[0]["RZ"]) && IsNumeric(MItem[0]["YSL"]) && IsNumeric(MItem[0]["SHB"]) && IsNumeric(MItem[0]["SL"]))
                {
                    //水
                    MItem[0]["T_CLS"] = MItem[0]["YSL"];
                    //灰
                    MItem[0]["T_CLSN"] = Round(GetSafeDouble(MItem[0]["YSL"]) / GetSafeDouble(MItem[0]["SHB"]), 0).ToString("0");
                    //砂
                    MItem[0]["T_CLSA"] = Round((GetSafeDouble(MItem[0]["RZ"]) - GetSafeDouble(MItem[0]["T_CLS"]) - GetSafeDouble(MItem[0]["T_CLSN"])) * GetSafeDouble(MItem[0]["SL"]) / 100, 0).ToString("0");
                    //石子
                    MItem[0]["T_CLSI"] = Round(GetSafeDouble(MItem[0]["RZ"]) - GetSafeDouble(MItem[0]["T_CLS"]) - GetSafeDouble(MItem[0]["T_CLSN"]) - GetSafeDouble(MItem[0]["T_CLSA"]), 0).ToString("0");
                }
                #endregion

                #region 用料配合比计算
                if (IsNumeric(MItem[0]["T_CLSN"]))
                {
                    //水
                    if (IsNumeric(MItem[0]["T_CLS"]))
                    {
                        MItem[0]["T_PBS"] = Round(GetSafeDouble(MItem[0]["T_CLS"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 2).ToString("0.00");
                    }
                    else
                    {
                        MItem[0]["T_PBS"] = "0";
                    }
                    //砂
                    if (IsNumeric(MItem[0]["T_CLSA"]))
                    {
                        MItem[0]["T_PBSA"] = Round(GetSafeDouble(MItem[0]["T_CLSA"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 2).ToString("0.00");
                    }
                    else
                    {
                        MItem[0]["T_PBSA"] = "0";
                    }
                    //石子
                    if (IsNumeric(MItem[0]["T_CLSI"]))
                    {
                        MItem[0]["T_PBSI"] = Round(GetSafeDouble(MItem[0]["T_CLSI"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 2).ToString("0.00");
                    }
                    else
                    {
                        MItem[0]["T_PBSI"] = "0";
                    }

                    //外加剂1
                    if (IsNumeric(MItem[0]["T_CLWJJ1"]))
                    {
                        MItem[0]["T_PBWJJ1"] = Round(GetSafeDouble(MItem[0]["T_CLWJJ1"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 3).ToString("0.000");
                    }
                    else
                    {
                        MItem[0]["T_PBWJJ1"] = "0";
                    }
                    //外加剂2
                    if (IsNumeric(MItem[0]["T_CLWJJ2"]))
                    {
                        MItem[0]["T_PBWJJ2"] = Round(GetSafeDouble(MItem[0]["T_CLWJJ2"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 3).ToString("0.000");
                    }
                    else
                    {
                        MItem[0]["T_PBWJJ2"] = "0";
                    }

                    //外加剂3
                    if (IsNumeric(MItem[0]["T_CLWJJ3"]))
                    {
                        MItem[0]["T_PBWJJ3"] = Round(GetSafeDouble(MItem[0]["T_CLWJJ3"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 3).ToString("0.000");
                    }
                    else
                    {
                        MItem[0]["T_PBWJJ3"] = "0";
                    }

                    //掺合料1
                    if (IsNumeric(MItem[0]["T_CLCHL1"]))
                    {
                        MItem[0]["T_PBCHL1"] = Round(GetSafeDouble(MItem[0]["T_CLCHL1"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 3).ToString("0.000");
                    }
                    else
                    {
                        MItem[0]["T_PBCHL1"] = "0";
                    }
                    //掺合料2
                    if (IsNumeric(MItem[0]["T_CLCHL2"]))
                    {
                        MItem[0]["T_PBCHL2"] = Round(GetSafeDouble(MItem[0]["T_CLCHL2"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 3).ToString("0.000");
                    }
                    else
                    {
                        MItem[0]["T_PBCHL2"] = "0";
                    }
                    //掺合料3
                    if (IsNumeric(MItem[0]["T_CLCHL3"]))
                    {
                        MItem[0]["T_PBCHL3"] = Round(GetSafeDouble(MItem[0]["T_CLCHL3"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 3).ToString("0.000");
                    }
                    else
                    {
                        MItem[0]["T_PBCHL3"] = "0";
                    }

                }
                #endregion

                if (!string.IsNullOrEmpty(sItem["ZZRQ"]))
                {
                    MItem[0]["SYRQQ1"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(7).ToShortDateString();
                    MItem[0]["SYRQQ2"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(28).ToShortDateString();
                }
                
                var dateTime = new DateTime();
                if (jcxm.Contains("、7天强度、") || jcxm.Contains("、配合比、"))
                {
                    if (DateTime.TryParse(sItem["ZZRQ"], out dateTime))
                    {
                        sItem["YQSYRQ"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(28).ToShortDateString();
                    }
                }
                else
                {
                    sItem["KYPJ_7"] = "----";
                }

                if (Conversion.Val(sItem["KYPJ_71"]) > 0)
                {
                    if (DateTime.TryParse(sItem["ZZRQ"], out dateTime))
                    {
                        MItem[0]["SYRQQ1"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(7).ToShortDateString();
                    }
                    MItem[0]["SYRQQ1"] = "";
                }

                if (jcxm.Contains("、28天强度、") || jcxm.Contains("、配合比、"))
                {
                    if ((-0.001 <= Conversion.Val(sItem["KYPJ_7"]) && -0.001 <= Conversion.Val(sItem["KYPJ"])) && (null == sItem["TOMARK"] || Conversion.Val(sItem["TOMARK"]) <= 0))
                    {
                        sItem["TOMARK"] = "1";
                    }
                }

                if (jcxm.Contains("、28天强度、") || jcxm.Contains("、配合比、"))
                {
                    if (0 == Conversion.Val(sItem["KYPJ1"]))
                    {
                        if (DateTime.TryParse(sItem["ZZRQ"], out dateTime))
                        {
                            sItem["YQSYRQ"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(28).ToShortDateString();
                        }
                        else
                        {
                            sItem["KYPJ"] = "----";
                        }
                    }
                    if (DateTime.TryParse(sItem["ZZRQ"], out dateTime))
                    {
                        MItem[0]["SYRQQ2"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(28).ToShortDateString();
                    }
                }
                else
                {
                    MItem[0]["SYRQQ2"] = "";
                    sItem["KYPJ"] = "----";
                }

                if (jcxm.Contains("、28天强度、") || jcxm.Contains("、配合比、"))
                {
                    if (-0 <= Conversion.Val(sItem["KYPJ_7"]) && (null == sItem["KYPJ"] || Conversion.Val(sItem["KYPJ"]) <= 0))
                    {
                        if (DateTime.TryParse(sItem["ZZRQ"], out dateTime))
                        {
                            sItem["YQSYRQ"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(28).ToShortDateString();
                        }
                        else
                        {
                            sItem["KYPJ"] = "----";
                        }
                    }
                }

                if (jcxm.Contains("、抗渗、") || jcxm.Contains("、配合比、"))
                {
                    if (!string.IsNullOrEmpty(sItem["KYPJ"]) && sItem["KYPJ"] != "----" && Conversion.Val(sItem["KSQD1"]) != 0)
                    {
                        sItem["DDKSDJ"] = "P" + (Conversion.Val(sItem["KSQD1"]) * 10 - 2).ToString();
                    }
                    else
                    {
                        sItem["DDKSDJ"] = "----";
                    }

                }
                else
                {
                    sItem["DDKSDJ"] = "----";
                }

                if (jcxm.Contains("、泌水率、") || jcxm.Contains("、配合比、"))
                {
                    //加压
                    sItem["MSL"] = (Math.Round((100 * Conversion.Val(sItem["MSL1"]) / Conversion.Val(sItem["MSL2"])) / 10, 0) * 10).ToString();

                    //常压
                    List<double> larray = new List<double>();
                    for (int i = 1; i <= 3; i++)
                    {
                        sItem["CYMSL" + i] = Math.Round(Conversion.Val(sItem["MSLMSZL"+i])/(Conversion.Val(sItem["MSLBHYSL"+i])/Conversion.Val(sItem["MSLBHWZZL"+i]))*(Conversion.Val(sItem["MSLTSYZZL"+i])-Conversion.Val(sItem["MSLTZL"+i]))*100 ,0).ToString();
                        larray.Add(Conversion.Val(sItem["CYMSL" + i]));
                    }
                    if ((larray.Max()-larray.Average())>larray.Average()*0.15 || larray.Average()-larray.Min() > larray.Average() * 0.15)
                    {
                        sItem["CYMSL"] = "重新试验";
                    }
                    else
                    {
                        sItem["CYMSL"] = larray.Average().ToString();
                    }
                }
                else
                {
                    sItem["MSL"] = "----";
                    sItem["MSL1"] = "----";
                    sItem["MSL2"] = "----";
                    sItem["CYMSL"] = "----";
                }

                if (jcxm.Contains("、含气量、") || jcxm.Contains("、配合比、"))
                {
                    sItem["HQL"] = (Math.Round(Conversion.Val(sItem["HQL1"]) - Conversion.Val(sItem["HQL2"]), 1)).ToString("0.0"); ;
                }
                else
                {
                    sItem["HQL"] = "----";
                    sItem["HQL1"] = "----";
                    sItem["HQL2"] = "----";
                }

                if (jcxm.Contains("、表观密度、") || jcxm.Contains("、配合比、"))
                {
                }
                else
                {
                    sItem["TLJ"] = "----";
                    sItem["BGMD"] = "----";
                    sItem["THSJZZL"] = "----";
                    sItem["TZL"] = "----";
                }

                if (jcxm.Contains("、凝结时间、") || jcxm.Contains("、配合比、"))
                {
                    sItem["CNSJ"] = (Math.Round((Conversion.Val(sItem["T1CN"]) + Conversion.Val(sItem["T2CN"]) + Conversion.Val(sItem["T3CN"])) / 3 / 5, 0) * 5).ToString("0");
                    sItem["ZNSJ"] = (Math.Round((Conversion.Val(sItem["T1ZN"]) + Conversion.Val(sItem["T2ZN"]) + Conversion.Val(sItem["T3ZN"])) / 3 / 5, 0) * 5).ToString("0");
                }
                else
                {
                    sItem["CNSJ"] = "----";
                    sItem["ZNSJ"] = "----";
                    sItem["T1CN"] = "----";
                    sItem["T2CN"] = "----";
                    sItem["T3CN"] = "----";
                    sItem["T1ZN"] = "----";
                    sItem["T2ZN"] = "----";
                    sItem["T3ZN"] = "----";
                }

                sItem["JCJG"] = "合格";
                jsbeizhu = "该组试样的检测结果合格";
            }
            #region 更新主表检测结果
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
