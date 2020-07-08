using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


namespace Calculates
{
    public class GJN : BaseMethods
    {
        public void Calc()
        {
            #region 计算开始
            var data = retData;
            var mrsDj = dataExtra["BZ_GJN_DJ"];
            var MItem = data["M_GJN"];
            var mitem = MItem[0];
            var SItem = data["S_GJN"];
            bool mAllHg = true;
            bool dzpd = true;
            var jcxm = "";
            var jcxmCur = "";
            var jcxmBhg = "";
            int hgCount = 0;
            int hgCount_3 = 0;
            int hgCount_28 = 0;            
            decimal KYPJ_1 = 0;           
            decimal KYPJMIN_1 = 0;
            decimal KYPJMAX_1 = 0;                   
            decimal KYPJ_3 = 0;            
            decimal KYPJMIN_3 = 0;
            decimal KYPJMAX_3 = 0;           
            decimal KYPJ_28 = 0;           
            decimal KYPJMIN_28 = 0;
            decimal KYPJMAX_28 = 0;
            decimal pzl1 = 0;
            decimal pzl2 = 0;
            decimal pzl3 = 0;
            decimal pzl1_1 = 0;
            decimal pzl2_1 = 0;
            decimal pzl3_1 = 0;
            decimal sum1 = 0;
            decimal sum1_3 = 0;
            decimal sum1_28 = 0;



            foreach (var sitem in SItem)
            {               
                var mrsDj_Filter = mrsDj.FirstOrDefault();
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    mitem["G_KY1"] = string.IsNullOrEmpty(mrsDj_Filter["G_KY1"]) ? "" : mrsDj_Filter["G_KY1"].Trim();
                    mitem["G_KY2"] = string.IsNullOrEmpty(mrsDj_Filter["G_KY2"]) ? "" : mrsDj_Filter["G_KY2"].Trim();
                    mitem["G_KY3"] = string.IsNullOrEmpty(mrsDj_Filter["G_KY3"]) ? "" : mrsDj_Filter["G_KY3"].Trim();
                    sitem["G_LDD1"] = string.IsNullOrEmpty(mrsDj_Filter["G_LDD1"]) ? "" : mrsDj_Filter["G_LDD1"].Trim();
                    sitem["G_LDD2"] = string.IsNullOrEmpty(mrsDj_Filter["G_LDD2"]) ? "" : mrsDj_Filter["G_LDD2"].Trim();
                    sitem["G_LLZHL"] = string.IsNullOrEmpty(mrsDj_Filter["G_LLZHL"]) ? "" : mrsDj_Filter["G_LLZHL"].Trim();
                    sitem["G_bzmsl"] = string.IsNullOrEmpty(mrsDj_Filter["G_bzmsl"]) ? "" : mrsDj_Filter["G_bzmsl"].Trim();
                    sitem["G_SXPZL"] = string.IsNullOrEmpty(mrsDj_Filter["G_SXPZL"]) ? "" : mrsDj_Filter["G_SXPZL"].Trim();
                    sitem["G_PZLCZ"] = string.IsNullOrEmpty(mrsDj_Filter["G_PZLCZ"]) ? "" : mrsDj_Filter["G_PZLCZ"].Trim();                   
                }
                else
                {
                    sitem["JCJG"] = "不下结论";
                    mitem["JCJGMS"] = "获取标准要求出错，找不到对应项";
                    continue;
                }            
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、流动度、"))
                {
                    jcxmCur = "流动度";


                    decimal scldds = GetSafeDecimal(sitem["JZLDD_S"]);
                    decimal scldde = GetSafeDecimal(sitem["JZLDD_E"]);
                    sitem["DXPD"] = IsQualified(sitem["G_LDD1"], sitem["JZLDD_S"]);
                    sitem["DXPD_30"] = IsQualified(sitem["G_LDD2"], sitem["JZLDD_E"]);
                    if (sitem["DXPD"] == "不合格")
                    {
                        dzpd = false;
                    }
                    if (sitem["DXPD_30"] == "不合格")
                    {
                        dzpd = false;
                    }
                    if (sitem["DXPD"] == "不合格" || sitem["DXPD_30"] == "不合格")
                    {
                        mitem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["G_LDD1"] = "----";
                    sitem["G_LDD2"] = "----";
                    sitem["JZLDD_S"] = "----";
                    sitem["JZLDD_E"] = "----";


                }
                if (jcxm.Contains("、氯离子含量、"))
                {
                    jcxmCur = "氯离子含量";

                    sitem["LLZDXPD"] = IsQualified(sitem["G_LLZHL"], sitem["LLZHL"]);
                    if (sitem["LLZDXPD"] == "不合格")
                    {
                        mitem["JCJG"] = "不合格";
                        dzpd = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }




                }
                else
                {
                    sitem["G_LLZHL"] = "----";
                    sitem["LLZHL"] = "----";
                }
                if (jcxm.Contains("、泌水率、"))
                {
                    jcxmCur = "泌水率";


                    decimal msl = GetSafeDecimal(sitem["LJMSL"]) / GetSafeDecimal(sitem["WLBMJ"]);
                    decimal syzl = GetSafeDecimal(sitem["SYJSYZZL"]) - GetSafeDecimal(sitem["SYTZL"]);
                    decimal msll = msl / (GetSafeDecimal(sitem["BHWYSZL"]) / GetSafeDecimal(sitem["HNTBHWZL"]) * syzl) * 100;

                    decimal msl2 = GetSafeDecimal(sitem["LJMSL2"]) / GetSafeDecimal(sitem["WLBMJ2"]);
                    decimal syzl2 = GetSafeDecimal(sitem["SYJSYZZL2"]) - GetSafeDecimal(sitem["SYTZL2"]);
                    decimal msll2 = msl / (GetSafeDecimal(sitem["BHWYSZL2"]) / GetSafeDecimal(sitem["HNTBHWZL2"]) * syzl) * 100;

                    decimal msl3 = GetSafeDecimal(sitem["LJMSL3"]) / GetSafeDecimal(sitem["WLBMJ3"]);
                    decimal syzl3 = GetSafeDecimal(sitem["SYJSYZZL3"]) - GetSafeDecimal(sitem["SYTZL3"]);
                    decimal msll3 = msl / (GetSafeDecimal(sitem["BHWYSZL3"]) / GetSafeDecimal(sitem["HNTBHWZL3"]) * syzl) * 100;

                    decimal mslpj = Math.Round((msll + msl2 + msl3) / 3, 2);
                    List<decimal> list = new List<decimal>();
                    list.Add(msll);
                    list.Add(msl2);
                    list.Add(msl3);


                    list.Sort();

                    if (list[1] - list[0] >= list[1] * GetSafeDecimal("0.15") || list[2] - list[1] >= list[1] * GetSafeDecimal("0.15"))
                    {
                        sitem["SCMSL"] = list[1].ToString("0.00");
                        if (sitem["SCMSL"] == "0")
                        {
                            sitem["MSLDXPD"] = "合格";
                        }
                        else
                        {
                            sitem["MSLDXPD"] = "不合格";
                        }
                    }
                    if (list[1] - list[0] >= list[1] * GetSafeDecimal("0.15") && list[2] - list[1] >= list[1] * GetSafeDecimal("0.15"))
                    {
                        sitem["SCMSL"] = "试验无效";
                        sitem["MSLDXPD"] = "试验无效";
                    }

                    if (sitem["MSLDXPD"] == "不合格")
                    {
                        mitem["JCJG"] = "不合格";
                        dzpd = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }




                }
                else
                {
                    sitem["SCMSL"] = "----";
                    sitem["MSLDXPD"] = "----";
                }
                if (jcxm.Contains("、竖向膨胀率、"))
                {
                    jcxmCur = "竖向膨胀率";


                    pzl1 = (GetSafeDecimal(sitem["CSGD"]) - GetSafeDecimal(sitem["SGGD_3h"])) / 100 * 100;
                    pzl2 = (GetSafeDecimal(sitem["CSGD2"]) - GetSafeDecimal(sitem["SGGD2_3h"])) / 100 * 100;
                    pzl3 = (GetSafeDecimal(sitem["CSGD3"]) - GetSafeDecimal(sitem["SGGD3_3h"])) / 100 * 100;
                    decimal pz = (pzl1 + pzl2 + pzl3) / 3;
                    sitem["SXPZL"] = pz.ToString("0.00");

                    pzl1_1 = (GetSafeDecimal(sitem["CSGD"]) - GetSafeDecimal(sitem["SGGD1_24h"])) / 100 * 100;
                    pzl2_1 = (GetSafeDecimal(sitem["CSGD2"]) - GetSafeDecimal(sitem["SGGD2_24h"])) / 100 * 100;
                    pzl3_1 = (GetSafeDecimal(sitem["CSGD3"]) - GetSafeDecimal(sitem["SGGD3_24h"])) / 100 * 100;
                    decimal pz_1 = (pzl1_1 + pzl2_1 + pzl3_1) / 3;
                    sitem["SXPZL_24h"] = pz_1.ToString("0.00");

                    sitem["PZLCZ"] = (pz_1 - pz).ToString("0.00");

                    sitem["PZLDXPD"] = IsQualified(sitem["G_SXPZL"], sitem["SXPZL"]);
                    sitem["PZLDXPD1"] = IsQualified(sitem["G_PZLCZ"], sitem["PZLCZ"]);
                    if (sitem["PZLDXPD"] == "不合格")
                    {
                        dzpd = false;
                    }
                    if (sitem["PZLDXPD1"] == "不合格")
                    {
                        dzpd = false;
                    }

                    if (sitem["PZLDXPD"] == "不合格" || sitem["PZLDXPD1"] == "不合格")
                    {
                        mitem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }

                }
                else
                {
                    sitem["SXPZL"] = "----";
                    sitem["PZLCZ"] = "----";
                    sitem["PZLDXPD"] = "----";
                    sitem["PZLDXPD1"] = "----";


                }
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";

                    sitem["KYQD1"] = Math.Round((GetSafeDecimal(sitem["KYHZ1"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD2"] = Math.Round((GetSafeDecimal(sitem["KYHZ2"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD3"] = Math.Round((GetSafeDecimal(sitem["KYHZ3"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD4"] = Math.Round((GetSafeDecimal(sitem["KYHZ4"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD5"] = Math.Round((GetSafeDecimal(sitem["KYHZ5"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD6"] = Math.Round((GetSafeDecimal(sitem["KYHZ6"]) / 1600) * 1000, 1).ToString();
                    KYPJ_1 = Math.Round((GetSafeDecimal(sitem["KYQD1"]) + GetSafeDecimal(sitem["KYQD2"]) + GetSafeDecimal(sitem["KYQD3"]) + GetSafeDecimal(sitem["KYQD4"]) + GetSafeDecimal(sitem["KYQD5"]) + GetSafeDecimal(sitem["KYQD6"])) / 6, 1);

                    KYPJMIN_1 = KYPJ_1 * (GetSafeDecimal("0.9"));
                    KYPJMAX_1 = KYPJ_1 * (GetSafeDecimal("1.1"));

                    List<decimal> kypjcont = new List<decimal>();
                    kypjcont.Add(GetSafeDecimal(sitem["KYQD1"]));
                    kypjcont.Add(GetSafeDecimal(sitem["KYQD2"]));
                    kypjcont.Add(GetSafeDecimal(sitem["KYQD3"]));
                    kypjcont.Add(GetSafeDecimal(sitem["KYQD4"]));
                    kypjcont.Add(GetSafeDecimal(sitem["KYQD5"]));
                    kypjcont.Add(GetSafeDecimal(sitem["KYQD6"]));

                    for (int i = 0; i < kypjcont.Count; i++)
                    {
                        if (kypjcont[i] <= KYPJMAX_1 && kypjcont[i] >= KYPJMIN_1)
                        {
                            hgCount = hgCount + 1;
                            sum1 += kypjcont[i];
                        }
                    }
                    if (hgCount == 5 || hgCount == 6)
                    {
                        sitem["KYPJ"] = Math.Round(sum1 / hgCount, 1).ToString("0.0");
                        mitem["G_JG"] = IsQualified(mitem["G_HGQD"], sitem["KYPJ"]);
                        if (mitem["G_JG"] == "不合格")
                        {
                            
                            dzpd = false;
                        }
                    }
                    else
                    {
                        mAllHg = false;
                        //作废
                        sitem["KYPJ"] = "作废";
                        mitem["G_JG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    sitem["KYQD1_3"] = Math.Round((GetSafeDecimal(sitem["KYHZ3d1"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD2_3"] = Math.Round((GetSafeDecimal(sitem["KYHZ3d2"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD3_3"] = Math.Round((GetSafeDecimal(sitem["KYHZ3d3"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD4_3"] = Math.Round((GetSafeDecimal(sitem["KYHZ3d4"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD5_3"] = Math.Round((GetSafeDecimal(sitem["KYHZ3d5"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD6_3"] = Math.Round((GetSafeDecimal(sitem["KYHZ3d6"]) / 1600) * 1000, 1).ToString();

                    KYPJ_3 = Math.Round((GetSafeDecimal(sitem["KYQD1_3"]) + GetSafeDecimal(sitem["KYQD2_3"]) + GetSafeDecimal(sitem["KYQD3_3"]) + GetSafeDecimal(sitem["KYQD4_3"]) + GetSafeDecimal(sitem["KYQD5_3"]) + GetSafeDecimal(sitem["KYQD6_3"])) / 6, 1);

                    KYPJMIN_3 = KYPJ_3 * (GetSafeDecimal("0.9"));
                    KYPJMAX_3 = KYPJ_3 * (GetSafeDecimal("1.1"));

                    List<decimal> kypjcont3 = new List<decimal>();
                    kypjcont3.Add(GetSafeDecimal(sitem["KYQD1_3"]));
                    kypjcont3.Add(GetSafeDecimal(sitem["KYQD2_3"]));
                    kypjcont3.Add(GetSafeDecimal(sitem["KYQD3_3"]));
                    kypjcont3.Add(GetSafeDecimal(sitem["KYQD4_3"]));
                    kypjcont3.Add(GetSafeDecimal(sitem["KYQD5_3"]));
                    kypjcont3.Add(GetSafeDecimal(sitem["KYQD6_3"]));

                    for (int i = 0; i < kypjcont3.Count; i++)
                    {
                        if (kypjcont3[i] <= KYPJMAX_3 && kypjcont3[i] >= KYPJMIN_3)
                        {
                            hgCount_3 = hgCount_3 + 1;

                            sum1_3 += kypjcont3[i];
                        }
                    }

                    if (hgCount_3 == 5 || hgCount_3 == 6)
                    {
                        sitem["KYPJ_3"] = Math.Round(sum1_3 / hgCount_3, 1).ToString("0.0");
                        mitem["G_JG_3"] = IsQualified(mitem["G_HGQD"], sitem["KYPJ_3"]);
                        if (mitem["G_JG_3"] == "不合格")
                        {
                            dzpd = false;
                        }

                    }
                    else
                    {
                        mAllHg = false;
                        //单组作废
                        sitem["KYPJ3"] = "作废";
                        mitem["G_JG_3"] = "不合格";
                    }

                    sitem["KYQD1_28"] = Math.Round((GetSafeDecimal(sitem["KYHZ28d1"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD2_28"] = Math.Round((GetSafeDecimal(sitem["KYHZ28d2"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD3_28"] = Math.Round((GetSafeDecimal(sitem["KYHZ28d3"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD4_28"] = Math.Round((GetSafeDecimal(sitem["KYHZ28d4"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD5_28"] = Math.Round((GetSafeDecimal(sitem["KYHZ28d5"]) / 1600) * 1000, 1).ToString();
                    sitem["KYQD6_28"] = Math.Round((GetSafeDecimal(sitem["KYHZ28d6"]) / 1600) * 1000, 1).ToString();

                    KYPJ_28 = Math.Round((GetSafeDecimal(sitem["KYQD1_28"]) + GetSafeDecimal(sitem["KYQD2_28"]) + GetSafeDecimal(sitem["KYQD3_28"]) + GetSafeDecimal(sitem["KYQD4_28"]) + GetSafeDecimal(sitem["KYQD5_28"]) + GetSafeDecimal(sitem["KYQD6_28"])) / 6, 1);


                    KYPJMIN_28 = KYPJ_28 * (GetSafeDecimal("0.9"));
                    KYPJMAX_28 = KYPJ_28 * (GetSafeDecimal("1.1"));

                    List<decimal> kypjcont28 = new List<decimal>();
                    kypjcont28.Add(GetSafeDecimal(sitem["KYQD1_28"]));
                    kypjcont28.Add(GetSafeDecimal(sitem["KYQD2_28"]));
                    kypjcont28.Add(GetSafeDecimal(sitem["KYQD3_28"]));
                    kypjcont28.Add(GetSafeDecimal(sitem["KYQD4_28"]));
                    kypjcont28.Add(GetSafeDecimal(sitem["KYQD5_28"]));
                    kypjcont28.Add(GetSafeDecimal(sitem["KYQD6_28"]));

                    for (int i = 0; i < kypjcont28.Count; i++)
                    {
                        if (kypjcont28[i] <= KYPJMAX_28 && kypjcont28[i] >= KYPJMIN_28)
                        {
                            hgCount_28 = hgCount_28 + 1;

                            sum1_28 += kypjcont28[i];
                        }
                    }

                    if (hgCount_28 == 5 || hgCount_28 == 6)
                    {
                        sitem["KYPJ_28"] = Math.Round(sum1_28 / hgCount_28, 1).ToString("0.0");
                        mitem["G_JG_28"] = IsQualified(mitem["G_HGQD"], sitem["KYPJ_28"]);
                        if (mitem["G_JG_28"] == "不合格")
                        {
                            dzpd = false;
                        }
                    }
                    else
                    {
                        mAllHg = false;
                        //作废
                        sitem["KYPJ28"] = "作废";
                        mitem["G_JG_28"] = "不合格";
                    }
                }
                else
                {
                    sitem["KYPJ"] = "----";
                    mitem["G_JG"] = "----";
                    sitem["KYPJ_3"] = "----";
                    mitem["G_JG_3"] = "----";
                    sitem["KYPJ_28"] = "----";
                    mitem["G_JG_28"] = "----";
                }
            }
            if (dzpd == true)
            {
                mitem["JCJG"] = "合格";
            }
            if (mAllHg == true)
            {             
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目均符合要求";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求";
            }
         

            #endregion
        }
    }
}