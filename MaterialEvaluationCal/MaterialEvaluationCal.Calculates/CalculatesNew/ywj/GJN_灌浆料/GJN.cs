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


            var jcxm = "";

            int hgCount = 0;
            decimal KYQD1_1 = 0;
            decimal KYQD2_1 = 0;
            decimal KYQD3_1 = 0;
            decimal KYQD4_1 = 0;
            decimal KYQD5_1 = 0;
            decimal KYQD6_1 = 0;
            decimal KYPJ_1 = 0;
            //string lq = SItem[0]["LQ"];
            decimal KYPJMIN_1 = 0;
            decimal KYPJMAX_1 = 0;
            string lq_1 = "";
            string G_lq_1;

            decimal KYQD1_3 = 0;
            decimal KYQD2_3 = 0;
            decimal KYQD3_3 = 0;
            decimal KYQD4_3 = 0;
            decimal KYQD5_3 = 0;
            decimal KYQD6_3 = 0;
            decimal KYPJ_3 = 0;
            //string lq = SItem[0]["LQ"];
            decimal KYPJMIN_3 = 0;
            decimal KYPJMAX_3 = 0;
        

            decimal KYQD1_28 = 0;
            decimal KYQD2_28 = 0;
            decimal KYQD3_28 = 0;
            decimal KYQD4_28 = 0;
            decimal KYQD5_28 = 0;
            decimal KYQD6_28 = 0;
            decimal KYPJ_28 = 0;
            //string lq = SItem[0]["LQ"];
            decimal KYPJMIN_28 = 0;
            decimal KYPJMAX_28 = 0;

            decimal pzl1 = 0;
            decimal pzl2 = 0;
            decimal pzl3 = 0;

            decimal pzl1_1 = 0;
            decimal pzl2_1 = 0;
            decimal pzl3_1 = 0;

            mitem["G_KY1"] = "≥35";
            mitem["G_KY2"] = "≥60";
            mitem["G_KY3"] = "≥85";


            foreach (var sitem in SItem)
            {
                G_lq_1 = sitem["LQ"];


                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["lq"].Contains(G_lq_1));
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    mitem["G_HGQD"] = string.IsNullOrEmpty(mrsDj_Filter["HGQD"]) ? "" : mrsDj_Filter["HGQD"].Trim();
                    
                }
                else
                {
                    sitem["JCJG"] = "不下结论";
                    mitem["JCJGMS"] = "获取标准要求出错，找不到对应项";
                    continue;
                }


                lq_1 = sitem["LQ"];
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、流动度、"))
                {
                    sitem["G_LDD1"] = "300";
                    sitem["G_LDD2"] = "260";

                    decimal scldds = GetSafeDecimal(sitem["JZLDD_S"]);
                    decimal scldde = GetSafeDecimal(sitem["JZLDD_E"]);
                    if (scldds >= 300)
                    {
                        sitem["DXPD"] = "合格";
                    }
                    else
                    {
                        sitem["DXPD"] = "不合格";
                    }
                    if (scldde >= 260)
                    {
                        sitem["DXPD_30"] = "合格";
                    }
                    else
                    {
                        sitem["DXPD_30"] = "不合格";
                    }

                }
                if (jcxm.Contains("、氯离子含量、"))
                {
                    sitem["G_LLZHL"] = "≤0.03";
                    

                    sitem["LLZDXPD"] = IsQualified(sitem["G_LLZHL"], sitem["LLZHL"]);





                }
                if (jcxm.Contains("、泌水率、"))
                {
                    decimal msl = GetSafeDecimal(sitem["LJMSL"]) / GetSafeDecimal(sitem["WLBMJ"]);
                    decimal syzl = GetSafeDecimal(sitem["SYJSYZZL"]) - GetSafeDecimal(sitem["SYTZL"]);
                    decimal msll = msl / (GetSafeDecimal(sitem["BHWYSZL"]) / GetSafeDecimal(sitem["HNTBHWZL "]) * syzl) * 100;

                    decimal msl2 = GetSafeDecimal(sitem["LJMSL2"]) / GetSafeDecimal(sitem["WLBMJ2"]);
                    decimal syzl2 = GetSafeDecimal(sitem["SYJSYZZL2"]) - GetSafeDecimal(sitem["SYTZL2"]);
                    decimal msll2 = msl / (GetSafeDecimal(sitem["BHWYSZL2"]) / GetSafeDecimal(sitem["HNTBHWZL2"]) * syzl) * 100;

                    decimal msl3 = GetSafeDecimal(sitem["LJMSL3"]) / GetSafeDecimal(sitem["WLBMJ3"]);
                    decimal syzl3 = GetSafeDecimal(sitem["SYJSYZZL3"]) - GetSafeDecimal(sitem["SYTZL3"]);
                    decimal msll3 = msl / (GetSafeDecimal(sitem["BHWYSZL3"]) / GetSafeDecimal(sitem["HNTBHWZL3"]) * syzl) * 100;

                    decimal mslpj = Math.Round((msll + msl2 + msl3) / 3,2);
                    List<decimal> list = new List<decimal>();
                    list.Add(msll);
                    list.Add(msl2);
                    list.Add(msl3);
                   

                    list.Sort();

                    if (list[1] - list[0] >= list[1] * GetSafeDecimal("0.15"))
                    { 
                    }

                    




                }
                if (jcxm.Contains("、竖向膨胀率、"))
                {

                    pzl1 = (GetSafeDecimal(sitem["CSGD"]) - GetSafeDecimal(sitem["SGGD_3h"])) / 100 * 100;
                    pzl2 = (GetSafeDecimal(sitem["CSGD2"]) - GetSafeDecimal(sitem["SGGD2_3h"])) / 100 * 100;
                    pzl3 = (GetSafeDecimal(sitem["CSGD3"]) - GetSafeDecimal(sitem["SGGD3_3h"])) / 100 * 100;
                    decimal pz= (pzl1 + pzl2 + pzl3) / 3;
                    sitem["SXPZL"] = pz.ToString("0.00");

                    pzl1_1 = (GetSafeDecimal(sitem["CSGD"]) - GetSafeDecimal(sitem["SGGD1_24h"])) / 100 * 100;
                    pzl2_1 = (GetSafeDecimal(sitem["CSGD2"]) - GetSafeDecimal(sitem["SGGD2_24h"])) / 100 * 100;
                    pzl3_1 = (GetSafeDecimal(sitem["CSGD3"]) - GetSafeDecimal(sitem["SGGD3_24h"])) / 100 * 100;
                    decimal pz_1 = (pzl1_1 + pzl2_1 + pzl3_1) / 3;
                    sitem["SXPZL_24h"] = pz_1.ToString("0.00");

                    sitem["PZLCZ"] = (pz_1 - pz).ToString("0.00");

                }



                if (jcxm.Contains("、抗压强度、"))
                {
                    KYQD1_1 = Math.Round((GetSafeDecimal(sitem["KYHZ1"]) / 1600) * 1000, 1);
                    KYQD2_1 = Math.Round((GetSafeDecimal(sitem["KYHZ2"]) / 1600) * 1000, 1);
                    KYQD3_1 = Math.Round((GetSafeDecimal(sitem["KYHZ3"]) / 1600) * 1000, 1);
                    KYQD4_1 = Math.Round((GetSafeDecimal(sitem["KYHZ4"]) / 1600) * 1000, 1);
                    KYQD5_1 = Math.Round((GetSafeDecimal(sitem["KYHZ5"]) / 1600) * 1000, 1);
                    KYQD6_1 = Math.Round((GetSafeDecimal(sitem["KYHZ6"]) / 1600) * 1000, 1);

                    sitem["KYQD1"] = KYQD1_1.ToString();
                    sitem["KYQD2"] = KYQD2_1.ToString();
                    sitem["KYQD3"] = KYQD3_1.ToString();
                    sitem["KYQD4"] = KYQD4_1.ToString();
                    sitem["KYQD5"] = KYQD5_1.ToString();
                    sitem["KYQD6"] = KYQD6_1.ToString();
                    
                    KYPJ_1 = Math.Round((KYQD1_1 + KYQD2_1 + KYQD3_1 + KYQD4_1 + KYQD5_1 + KYQD6_1) / 6,2);
                    decimal sum = KYQD1_1 + KYQD2_1 + KYQD3_1 + KYQD4_1 + KYQD5_1 + KYQD6_1;

                    KYPJMIN_1 = KYPJ_1 * (GetSafeDecimal("0.9"));
                    KYPJMAX_1 = KYPJ_1 * (GetSafeDecimal("1.1"));

                    List<decimal> kypjcont = new List<decimal>();
                    kypjcont.Add(KYQD1_1);
                    kypjcont.Add(KYQD2_1);
                    kypjcont.Add(KYQD3_1);
                    kypjcont.Add(KYQD4_1);
                    kypjcont.Add(KYQD5_1);
                    kypjcont.Add(KYQD6_1);

                    for (int i = 0; i < kypjcont.Count; i++)
                    {
                        if (kypjcont[i] <= KYPJMAX_1 && kypjcont[i] >= KYPJMIN_1)
                        {
                            hgCount = hgCount + 1;

                            sum += kypjcont[i];
                        }
                    }

                    if (hgCount == 5 || hgCount == 6)
                    {
                        sitem["KYPJ"] = Math.Round(sum / hgCount, 2).ToString("0.00");
                        mitem["G_JG"] = IsQualified(mitem["G_HGQD"], sitem["KYPJ"], false);
                        if (mitem["G_JG"] == "合格")
                        {
                            mAllHg = true;
                            sitem["JCJG"] = "合格";
                        }
                        else
                        {
                            mAllHg = false;
                            sitem["JCJG"] = "不合格";
                        }
                    }
                    else
                    {
                        //作废
                        sitem["KYPJ"] = "作废";
                    }

                    KYQD1_3 = Math.Round((GetSafeDecimal(sitem["KYHZ3d1 "]) / 1600) * 1000, 1);
                    KYQD2_3 = Math.Round((GetSafeDecimal(sitem["KYHZ3d2 "]) / 1600) * 1000, 1);
                    KYQD3_3 = Math.Round((GetSafeDecimal(sitem["KYHZ3d3 "]) / 1600) * 1000, 1);
                    KYQD4_3 = Math.Round((GetSafeDecimal(sitem["KYHZ3d4 "]) / 1600) * 1000, 1);
                    KYQD5_3 = Math.Round((GetSafeDecimal(sitem["KYHZ3d5 "]) / 1600) * 1000, 1);
                    KYQD6_3 = Math.Round((GetSafeDecimal(sitem["KYHZ3d6 "]) / 1600) * 1000, 1);

                    sitem["KYQD1_3"] = KYQD1_3.ToString();
                    sitem["KYQD2_3"] = KYQD2_3.ToString();
                    sitem["KYQD3_3"] = KYQD3_3.ToString();
                    sitem["KYQD4_3"] = KYQD4_3.ToString();
                    sitem["KYQD5_3"] = KYQD5_3.ToString();
                    sitem["KYQD6_3"] = KYQD6_3.ToString();

                    KYPJ_3 = Math.Round((KYQD1_3 + KYQD2_3 + KYQD3_3 + KYQD4_3 + KYQD5_3 + KYQD6_3) / 6, 2);
                    decimal sum_3 = KYQD1_3 + KYQD2_3 + KYQD3_3 + KYQD4_3 + KYQD5_3 + KYQD6_3;

                    KYPJMIN_3 = KYPJ_3 * (GetSafeDecimal("0.9"));
                    KYPJMAX_3 = KYPJ_3 * (GetSafeDecimal("1.1"));

                    List<decimal> kypjcont3 = new List<decimal>();
                    kypjcont3.Add(KYQD1_3);
                    kypjcont3.Add(KYQD2_3);
                    kypjcont3.Add(KYQD3_3);
                    kypjcont3.Add(KYQD4_3);
                    kypjcont3.Add(KYQD5_3);
                    kypjcont3.Add(KYQD6_3);

                    for (int i = 0; i < kypjcont3.Count; i++)
                    {
                        if (kypjcont3[i] <= KYPJMAX_3 && kypjcont3[i] >= KYPJMIN_3)
                        {
                            hgCount = hgCount + 1;

                            sum_3 += kypjcont3[i];
                        }
                    }

                    if (hgCount == 5 || hgCount == 6)
                    {
                        sitem["KYPJ_3"] = Math.Round(sum_3 / hgCount, 2).ToString("0.00");
                        mitem["G_JG_3"] = IsQualified(mitem["G_HGQD"], sitem["KYPJ_3"], false);
                        if (mitem["G_JG_3"] == "合格")
                        {
                            mAllHg = true;
                            sitem["JCJG"] = "合格";
                        }
                        else
                        {
                            mAllHg = false;
                            sitem["JCJG"] = "不合格";
                        }
                    }
                    else
                    {
                        //作废
                        sitem["KYPJ3"] = "作废";
                    }


                    KYQD1_28 = Math.Round((GetSafeDecimal(sitem["KYHZ28d1 "]) / 1600) * 1000, 1);
                    KYQD2_28 = Math.Round((GetSafeDecimal(sitem["KYHZ28d1 "]) / 1600) * 1000, 1);
                    KYQD3_28 = Math.Round((GetSafeDecimal(sitem["KYHZ28d1 "]) / 1600) * 1000, 1);
                    KYQD4_28 = Math.Round((GetSafeDecimal(sitem["KYHZ28d1 "]) / 1600) * 1000, 1);
                    KYQD5_28 = Math.Round((GetSafeDecimal(sitem["KYHZ28d1 "]) / 1600) * 1000, 1);
                    KYQD6_28 = Math.Round((GetSafeDecimal(sitem["KYHZ28d1 "]) / 1600) * 1000, 1);

                    sitem["KYQD1_28"] = KYQD1_3.ToString();
                    sitem["KYQD2_28"] = KYQD2_3.ToString();
                    sitem["KYQD3_28"] = KYQD3_3.ToString();
                    sitem["KYQD4_28"] = KYQD4_3.ToString();
                    sitem["KYQD5_28"] = KYQD5_3.ToString();
                    sitem["KYQD6_28"] = KYQD6_3.ToString();

                    KYPJ_28 = Math.Round((KYQD1_3 + KYQD2_3 + KYQD3_3 + KYQD4_3 + KYQD5_3 + KYQD6_3) / 6, 2);
                    decimal sum_28 = KYQD1_3 + KYQD2_3 + KYQD3_3 + KYQD4_3 + KYQD5_3 + KYQD6_3;

                    KYPJMIN_3 = KYPJ_3 * (GetSafeDecimal("0.9"));
                    KYPJMAX_3 = KYPJ_3 * (GetSafeDecimal("1.1"));

                    List<decimal> kypjcont28 = new List<decimal>();
                    kypjcont28.Add(KYQD1_28);
                    kypjcont28.Add(KYQD2_28);
                    kypjcont28.Add(KYQD3_28);
                    kypjcont28.Add(KYQD4_28);
                    kypjcont28.Add(KYQD5_28);
                    kypjcont28.Add(KYQD6_28);

                    for (int i = 0; i < kypjcont28.Count; i++)
                    {
                        if (kypjcont28[i] <= KYPJMAX_28 && kypjcont28[i] >= KYPJMIN_28)
                        {
                            hgCount = hgCount + 1;

                            sum_28 += kypjcont28[i];
                        }
                    }

                    if (hgCount == 5 || hgCount == 6)
                    {
                        sitem["KYPJ_28"] = Math.Round(sum_28 / hgCount, 2).ToString("0.00");
                        mitem["G_JG_28"] = IsQualified(mitem["G_HGQD"], sitem["KYPJ_28"], false);
                        if (mitem["G_JG_28"] == "合格")
                        {
                            mAllHg = true;
                            sitem["JCJG"] = "合格";
                        }
                        else
                        {
                            mAllHg = false;
                            sitem["JCJG"] = "不合格";
                        }
                    }
                    else
                    {
                        //作废
                        sitem["KYPJ28"] = "作废";
                    }









                }

            }
            if (mAllHg == true)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目抗压强度符合要求";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目抗压强度不符合要求";
            }
            if (hgCount < 5)
            {
                mitem["JCJG"] = "作废";
                mitem["JCJGMS"] = "作废";
            }

            #endregion
        }
    }
}