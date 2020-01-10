using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SPA : BaseMethods
    {
        public void Calc()
        {

            bool mAllHg = true, sign = true, mSFwc = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            double md, md1, md2;
            var SItem = data["S_SPA"];
            var MItem = data["M_SPA"];
            var EItem = data["E_JLPB"];
            var ELQItem = data["E_LQ"];
            var mItem = MItem[0];
            string stemp, dzbh;
            foreach (var sItem in SItem)
            {
                jsbeizhu = "";

                if (!string.IsNullOrEmpty(sItem["JLMC8"]) && sItem["JLMC8"] != "----" && sItem["JLMC8"] != "")
                {
                    mItem["WHICH"] = "bgspa、bgspa_4、bgspa_2、bgspa_3";
                    stemp = "";
                    for (int i = 1;i <= 8;i++)
                    {
                        sItem["JLMC" + i] = sItem["JLMC" + i].Trim();
                        if (!string.IsNullOrEmpty(sItem["JLMC" + i]) && sItem["JLMC" + i] != "----" && sItem["JLMC" + i] != "")
                        {
                            stemp = stemp + sItem["JLMC" + i] + "(" + sItem["JLBH" + i] + "):";
                            stemp = stemp + sItem["JLGG" + i] + " ";
                            stemp = stemp + sItem["JLCD" + i] + ";\r\n";
                        }
                    }
                    stemp = stemp.Substring(0, stemp.Length - 2) + "。";
                    sItem["ZBH"] = stemp;
                    sign = EItem.Count > 11 ? true : false;
                    if (EItem.Count > 0)
                    {
                        double sum = 0;
                        for (int i = 1;i <= 8;i ++)
                        {
                            if (IsNumeric(sItem["MTJMD"+i]) && !string.IsNullOrEmpty(sItem["MTJMD" + i]) &&
                                IsNumeric(EItem[0]["klpb" + i]) && !string.IsNullOrEmpty(EItem[0]["klpb" + i]))
                            {
                                md1 = GetSafeDouble(sItem["MTJMD" + i]);
                                md2 = GetSafeDouble(EItem[0]["klpb" + i]);
                                md = md2 / md1;
                                sum = sum + md;
                                sItem["KLPB" + i] = EItem[0]["klpb" + i].Trim();
                            }
                            else
                            {
                                sItem["KLPB" + i] = "";
                            }
                        }
                        md = 100 / sum;
                        md = Round(md, 3);
                        sItem["HC_MTJMD"] = md.ToString();
                        sum = 0;
                        for (int i = 1; i <= 8; i++)
                        {
                            if (IsNumeric(sItem["BGMD" + i]) && !string.IsNullOrEmpty(sItem["BGMD" + i]) &&
                                IsNumeric(EItem[0]["klpb" + i]) && !string.IsNullOrEmpty(EItem[0]["klpb" + i]))
                            {
                                md1 = GetSafeDouble(sItem["BGMD" + i]);
                                md2 = GetSafeDouble(EItem[0]["klpb" + i]);
                                md = md2 / md1;
                                sum = sum + md;
                            }
                        }
                        md = 100 / sum;
                        md = Round(md, 3);
                        sItem["HC_BGMD"] = md.ToString();
                    }
                    else
                    {
                        sItem["HC_MTJMD"] = "----";
                        sItem["HC_BGMD"] = "----";
                        mSFwc = false;
                    }
                }
                else
                {
                    mItem["WHICH"] = "bgspa、bgspa_1、bgspa_2、bgspa_3";
                    stemp = "";
                    for (int i = 1;i<=7;i++)
                    {
                        sItem["JLMC" + i] = sItem["JLMC" + i].Trim();
                        if (!string.IsNullOrEmpty(sItem["JLMC" + i]) && sItem["JLMC" + i] != "----" && sItem["JLMC" + i] != "")
                        {
                            stemp = stemp + sItem["JLMC" + i] + "(" + sItem["JLBH" + i] + "):";
                            stemp = stemp + sItem["JLGG" + i] + " ";
                            stemp = stemp + sItem["JLCD" + i] + ";\r\n";
                        }
                    }
                    stemp = stemp.Substring(0, stemp.Length - 2) + "。";
                    sItem["ZBH"] = stemp;
                    //关于合成的
                    int gs = EItem.Count;
                    sign = gs > 11 ? true : false;
                    if (gs > 0)
                    {
                        double sum = 0;
                        for (int i = 1;i <= 7;i ++)
                        {
                            if (IsNumeric(sItem["MTJMD" + i]) && !string.IsNullOrEmpty(sItem["MTJMD" + i]) &&
                                IsNumeric(EItem[0]["klpb" + i]) && !string.IsNullOrEmpty(EItem[0]["klpb" + i]))
                            {
                                md1 = GetSafeDouble(sItem["MTJMD" + i]);
                                md2 = GetSafeDouble(EItem[0]["klpb" + i]);
                                md = md2 / md1;
                                sum = sum + md;
                                sItem["KLPB" + i] = EItem[0]["klpb" + i].Trim();
                            }else 
                            {
                                sItem["KLPB" + i] = "";
                            }
                        }
                        md = 100 / sum;
                        md = Round(md, 3);
                        sItem["HC_MTJMD"] = Round(md,3).ToString();
                        sum = 0;
                        for (int i = 1; i <= 7; i++)
                        {
                            if (IsNumeric(sItem["BGMD" + i]) && !string.IsNullOrEmpty(sItem["BGMD" + i]) &&
                                IsNumeric(EItem[0]["klpb" + i]) && !string.IsNullOrEmpty(EItem[0]["klpb" + i]))
                            {
                                md1 = GetSafeDouble(sItem["BGMD" + i]);
                                md2 = GetSafeDouble(EItem[0]["klpb" + i]);
                                md = md2 / md1;
                                sum = sum + md;
                            }
                        }
                        md = 100 / sum;
                        md = Round(md, 3);
                        sItem["HC_BGMD"] = Round(md, 3).ToString();
                    }
                    else
                    {
                        sItem["HC_MTJMD"] = "----";
                        sItem["HC_BGMD"] = "----";
                        mSFwc = false;
                    }
                }
                //关于沥青含量
                if (ELQItem.Count > 0)
                {
                    sItem["G_BHD"] = ELQItem[0]["g_bhd"];
                    sItem["G_JXL"] = ELQItem[0]["g_jxl"];
                    sItem["G_KXL"] = ELQItem[0]["g_kxl"];
                    sItem["G_LZ"] = ELQItem[0]["g_lz"];
                    sItem["G_MTJMD"] = ELQItem[0]["g_mtjmd"];
                    sItem["G_WDD"] = ELQItem[0]["g_wdd"];
                    sItem["OAC1"] = ELQItem[0]["oac1"];
                    sItem["OAC2"] = ELQItem[0]["oac2"];
                    sItem["OAC"] = ELQItem[0]["oac"];
                }
                else
                {
                    sItem["G_BHD"] = "----";
                    sItem["G_JXL"] = "----";
                    sItem["G_KXL"] = "----";
                    sItem["G_LZ"] = "----";
                    sItem["G_MTJMD"] = "----";
                    sItem["G_WDD"] = "----";
                    mSFwc = false;
                }

                if (mAllHg)
                {
                    sItem["JCJG"] = "合格";
                    mItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    mItem["JCJG"] = "不合格";
                }
            }
            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_SPA"))
            {
                data["M_SPA"] = new List<IDictionary<string, string>>();
            }
            var M_SPA = data["M_SPA"];
            if (M_SPA.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_SPA.Add(m);
            }
            else
            {
                M_SPA[0]["JCJG"] = mjcjg;
                M_SPA[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
