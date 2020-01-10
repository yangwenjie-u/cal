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


            var extraDJ = dataExtra["BZ_JGF_DJ"];
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


                if (null == sItem["ZZRQ"])
                {
                    MItem[0]["SYRQQ1"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(7).ToShortDateString();
                    MItem[0]["SYRQQ2"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(28).ToShortDateString();
                }

                var dateTime = new DateTime();
                if (jcxm.Contains("、7天强度、"))
                {
                    if (DateTime.TryParse(sItem["ZZRQ"], out dateTime))
                    {
                        sItem["YQSYRQ"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(28).ToShortDateString();
                    }
                }

                if (jcxm.Contains("、7天强度、") && Conversion.Val(sItem["KYPJ_71"]) > 0)
                {
                    if (DateTime.TryParse(sItem["ZZRQ"], out dateTime))
                    {
                        MItem[0]["SYRQQ1"] = DateTime.Parse(sItem["ZZRQ"]).AddDays(7).ToShortDateString();
                    }
                    MItem[0]["SYRQQ1"] = "";
                }

                if (jcxm.Contains("、28天强度、"))
                {
                    if ((-0.001 <= Conversion.Val(sItem["KYPJ_7"]) && -0.001 <= Conversion.Val(sItem["KYPJ"])) && (null == sItem["TOMARK"] || Conversion.Val(sItem["TOMARK"]) <= 0))
                    {
                        sItem["TOMARK"] = "1";
                    }
                }

                if (jcxm.Contains("、28天强度、"))
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
                }

                if (jcxm.Contains("、28天强度、"))
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

                if (jcxm.Contains("、抗渗、"))
                {
                    sItem["DDKSDJ"] = "P" + (Conversion.Val(sItem["KSQD1"]) * 10 - 2).ToString();
                }
                else
                {
                    sItem["DDKSDJ"] = "----";
                }

                if (jcxm.Contains("、泌水率、"))
                {
                    sItem["MSL"] = (Math.Round((100 * Conversion.Val(sItem["MSL1"]) / Conversion.Val(sItem["MSL2"])) / 10, 0) * 10).ToString(); ;
                }
                else
                {
                    sItem["MSL"] = "----";
                    sItem["MSL1"] = "----";
                    sItem["MSL2"] = "----";
                }

                if (jcxm.Contains("、含气量、"))
                {
                    sItem["HQL"] = (Math.Round(Conversion.Val(sItem["HQL1"]) - Conversion.Val(sItem["HQL2"]), 1)).ToString("0.0"); ;
                }
                else
                {
                    sItem["HQL"] = "----";
                    sItem["HQL1"] = "----";
                    sItem["HQL2"] = "----";
                }

                if (jcxm.Contains("、表观密度、"))
                {
                }
                else
                {
                    sItem["TLJ"] = "----";
                    sItem["BGMD"] = "----";
                    sItem["THSJZZL"] = "----";
                    sItem["TZL"] = "----";
                }

                if (jcxm.Contains("、凝结时间、"))
                {
                    sItem["CNSJ"] = (Math.Round((Conversion.Val(sItem["T1CN"]) + Conversion.Val(sItem["T2CN"]) + Conversion.Val(sItem["T3CN"]) / 3 / 5), 0) * 5).ToString("0"); ;
                }
                else {
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
