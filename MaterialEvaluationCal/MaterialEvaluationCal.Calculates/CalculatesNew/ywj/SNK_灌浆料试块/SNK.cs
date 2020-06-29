using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


namespace Calculates
{
    public class SNK : BaseMethods
    {
        public void Calc()
        {
            #region 计算开始
            var data = retData;
            var mrsDj = dataExtra["BZ_SNK_DJ"];
            var MItem = data["M_SNK"];
            var mitem = MItem[0];
            var SItem = data["S_SNK"];
            bool mAllHg = true;


            var jcxm = "";

            int bhg = 0;
            decimal KYQD1 = 0;
            decimal KYQD2 = 0;
            decimal KYQD3 = 0;
            decimal KYQD4 = 0;
            decimal KYQD5 = 0;
            decimal KYQD6 = 0;
            decimal KYPJ = 0;
            //string lq = SItem[0]["LQ"];
            decimal KYPJMIN = 0;
            decimal KYPJMAX = 0;
            string lq = "";
            string G_lq;


            foreach (var sitem in SItem)
            {
                G_lq = sitem["LQ"];


                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["lq"].Contains(G_lq));
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


                lq = sitem["LQ"];
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                if (jcxm.Contains("、抗压强度、"))
                {
                    KYQD1 = Math.Round((GetSafeDecimal(sitem["SCKYHZ1"]) / 1600) * 1000, 1);
                    KYQD2 = Math.Round((GetSafeDecimal(sitem["SCKYHZ2"]) / 1600) * 1000, 1);
                    KYQD3 = Math.Round((GetSafeDecimal(sitem["SCKYHZ3"]) / 1600) * 1000, 1);
                    KYQD4 = Math.Round((GetSafeDecimal(sitem["SCKYHZ4"]) / 1600) * 1000, 1);
                    KYQD5 = Math.Round((GetSafeDecimal(sitem["SCKYHZ5"]) / 1600) * 1000, 1);
                    KYQD6 = Math.Round((GetSafeDecimal(sitem["SCKYHZ6"]) / 1600) * 1000, 1);

                    sitem["KYQD1"] = KYQD1.ToString();
                    sitem["KYQD2"] = KYQD2.ToString();
                    sitem["KYQD3"] = KYQD3.ToString();
                    sitem["KYQD4"] = KYQD4.ToString();
                    sitem["KYQD5"] = KYQD5.ToString();
                    sitem["KYQD6"] = KYQD6.ToString();
                    //1
                    KYPJ = (KYQD1 + KYQD2 + KYQD3 + KYQD4 + KYQD5 + KYQD6) / 6;

                    KYPJMIN = KYPJ * (GetSafeDecimal("0.9"));
                    KYPJMAX = KYPJ * (GetSafeDecimal("1.1"));

                    if (KYQD1 < KYPJMIN || KYQD1 > KYPJMAX)
                    {
                        bhg = bhg + 1;
                        KYPJ = (KYQD2 + KYQD3 + KYQD4 + KYQD5 + KYQD6) / 5;
                        KYPJMIN = KYPJ * (GetSafeDecimal("0.9"));
                        KYPJMAX = KYPJ * (GetSafeDecimal("1.1"));

                    }

                    if (KYQD2 < KYPJMIN || KYQD2 > KYPJMAX)
                    {
                        bhg = bhg + 1;
                        if (bhg > 1)
                        {
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            KYPJ = (KYQD1 + KYQD3 + KYQD4 + KYQD5 + KYQD6) / 5;
                            KYPJMIN = (decimal)0.9 * KYPJ  ;
                            KYPJMAX = KYPJ * (GetSafeDecimal("1.1"));
                        }
                    }
                    if (KYQD3 < KYPJMIN || KYQD3 > KYPJMAX)
                    {
                        bhg = bhg + 1;
                        if (bhg > 1)
                        {
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            KYPJ = (KYQD1 + KYQD2 + KYQD4 + KYQD5 + KYQD6) / 5;
                            KYPJMIN = KYPJ * (GetSafeDecimal("0.9"));
                            KYPJMAX = KYPJ * (GetSafeDecimal("1.1"));
                        }
                    }
                    if (KYQD4 < KYPJMIN || KYQD4 > KYPJMAX)
                    {
                        bhg = bhg + 1;
                        if (bhg > 1)
                        {
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            KYPJ = (KYQD1 + KYQD2 + KYQD3 + KYQD5 + KYQD6) / 5;
                            KYPJMIN = KYPJ * (GetSafeDecimal("0.9"));
                            KYPJMAX = KYPJ * (GetSafeDecimal("1.1"));
                        }
                    }
                    if (KYQD5 < KYPJMIN || KYQD5 > KYPJMAX)
                    {
                        bhg = bhg + 1;
                        if (bhg > 1)
                        {
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            KYPJ = (KYQD1 + KYQD2 + KYQD3 + KYQD4 + KYQD6) / 5;
                            KYPJMIN = KYPJ * (GetSafeDecimal("0.9"));
                            KYPJMAX = KYPJ * (GetSafeDecimal("1.1"));
                        }
                    }
                    if (KYQD6 < KYPJMIN || KYQD6 > KYPJMAX)
                    {
                        bhg = bhg + 1;
                        if (bhg > 1)
                        {
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            KYPJ = (KYQD1 + KYQD2 + KYQD3 + KYQD4 + KYQD5) / 5;
                            KYPJMIN = KYPJ * (GetSafeDecimal("0.9"));
                            KYPJMAX = KYPJ * (GetSafeDecimal("1.1"));
                        }
                    }

                    sitem["KYPJ"] = KYPJ.ToString("0.00");
                    mitem["G_JG"] = IsQualified(mitem["G_HGQD"], sitem["KYPJ"], false);
                    if (mitem["G_JG"] == "合格")
                    {
                        sitem["JCJG"] = "合格";
                    }
                    else
                    {
                        sitem["JCJG"] = "不合格";
                    }

                }

            }


            if (bhg > 1)
            {
                mitem["JCJG"] = "作废";
                mitem["JCJGMS"] = "作废，不下结论";
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
            #endregion
        }
    }
}