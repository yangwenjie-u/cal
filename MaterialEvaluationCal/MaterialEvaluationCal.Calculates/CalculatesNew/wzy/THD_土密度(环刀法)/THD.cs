using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class THD : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            double[] mkyqdArray = new double[6];
            string[] mtmpArray;
            double mSjcc, mSjcc1, mMj;
            double mMaxKyqd, mMinKyqd, mAvgKyqd;
            double mskys, msktj, msyhsl, msymd, msygmd, msyzd;
            string mSjdjbh, mSjdj;
            double mSz, mQdyq, mhsxs;
            int vp, iZs, iHgs;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            int a, aa, aaa, I, i1;
            bool mSFwc;
            mSFwc = true;
            #endregion

            #region 自定义函数
            Func<double, double, double, string> calc_hjgt =
                delegate (double hzl, double hjst, double hsl1)
                {
                    string calc_hjgt_ret = string.Empty;
                    double md;
                    double pc;
                    int xd, Gs;
                    double hsl2;
                    double hjgt;
                    pc = 1;
                    Random r = new Random();
                    int i = r.Next(0, 1000);
                    double x = (float)i / 1000;
                    if (x > 0.5)
                        pc = -1;
                    if (hsl1 > 40)
                        pc = pc * Round(x * 2, 1);
                    else
                        pc = pc * Round(x, 1);
                    hsl2 = hsl1 + pc;
                    hjgt = (100 * hjst + hsl2 * hzl) / (100 + hsl2);
                    hjgt = Round((hjgt), 2);
                    calc_hjgt_ret = hjgt.ToString("0.00");
                    return calc_hjgt_ret;
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_THD_DJ"];
            var MItem = data["M_THD"];
            var mitem = MItem[0];
            var SItem = data["S_THD"];
            #endregion

            #region  计算开始
            mAllHg = true;
            mitem["JCJGMS"] = "";
            iZs = 0;
            iHgs = 0;
            var Sitem = SItem[0];
            string bl, bl_sjysd;
            bl = Sitem["ZDGMD"];
            bl_sjysd = Sitem["SJYSD"];
            foreach (var sitem in SItem)
            {
                //计算龄期
                sitem["LQ"] = (GetSafeDateTime(mitem["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString("F0");
                sitem["ZDGMD"] = bl;
                sitem["SJYSD"] = bl_sjysd;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    if (jcxm.Contains("、干密度、"))
                    {
                        sitem["SMD1"] = Round(Conversion.Val(sitem["TZL1"]) / Conversion.Val(sitem["HDRJ1"]), 2).ToString("0.00");
                        sitem["SMD2"] = Round(Conversion.Val(sitem["TZL2"]) / Conversion.Val(sitem["HDRJ1"]), 2).ToString("0.00");
                        if (Conversion.Val(sitem["HSL11"]) >= 40 && Conversion.Val(sitem["HSL12"]) >= 40)
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL11"]) - Conversion.Val(sitem["HSL12"])) > 2)
                                sitem["PJHSL1"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL11"]) - Conversion.Val(sitem["HSL12"])) > 1)
                                sitem["PJHSL1"] = "无效";
                        }


                        if (Conversion.Val(sitem["HSL21"]) >= 40 && Conversion.Val(sitem["HSL22"]) >= 40)
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL21"]) - Conversion.Val(sitem["HSL22"])) > 2)
                                sitem["PJHSL2"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL21"]) - Conversion.Val(sitem["HSL22"])) > 1)
                                sitem["PJHSL2"] = "无效";
                        }
                        if (Math.Abs(Conversion.Val(sitem["GMD1"]) - Conversion.Val(sitem["GMD2"])) <= 0.03)
                            sitem["GMD"] = Round((Conversion.Val(sitem["GMD1"]) + Conversion.Val(sitem["GMD2"])) / 2, 2).ToString("0.00");
                        else
                            sitem["GMD"] = "无效";
                        if (Conversion.Val(sitem["ZDGMD"]) == 0)
                            sitem["PJYSD"] = "0";
                        else
                            sitem["PJYSD"] = Round(100 * Conversion.Val(sitem["GMD"]) / Conversion.Val(sitem["ZDGMD"]), 0).ToString();
                        if (Conversion.Val(sitem["SJYSD"]) <= Conversion.Val(sitem["PJYSD"]))
                            iHgs = iHgs + 1;
                        sitem["JCJG"] = "----";
                    }
                    else
                    {
                        sitem["GMD"] = "----";
                        sitem["PJYSD"] = "----";
                    }
                }
                else
                {
                    if (jcxm.Contains("、干密度、"))
                    {
                        //计算单组的抗压强度,并进行合格判断
                        double p1 = Conversion.Val(sitem["HDJT1"]) - Conversion.Val(sitem["HDZL1"]);
                        double p2 = Conversion.Val(sitem["HDJT2"]) - Conversion.Val(sitem["HDZL2"]);
                        if (p1 != 0)
                        {
                            sitem["TZL1"] = p1.ToString();
                            sitem["TZL2"] = p2.ToString();
                        }
                        sitem["SMD1"] = Round(Conversion.Val(sitem["TZL1"]) / Conversion.Val(sitem["HDRJ1"]), 2).ToString("0.00");
                        sitem["SMD2"] = Round(Conversion.Val(sitem["TZL2"]) / Conversion.Val(sitem["HDRJ1"]), 2).ToString("0.00");
                        sitem["SZL11"] = (Conversion.Val(sitem["HJST11"]) - Conversion.Val(sitem["HJGT11"])).ToString();
                        sitem["GTZL11"] = (Conversion.Val(sitem["HJGT11"]) - Conversion.Val(sitem["HZL11"])).ToString();
                        sitem["HSL11"] = Round(Conversion.Val(sitem["SZL11"]) / Conversion.Val(sitem["GTZL11"]) * 100, 1).ToString("0.0");
                        if (sitem["HJGT12"].Trim() == "0" || string.IsNullOrEmpty(sitem["HJGT12"]) || !IsNumeric(sitem["HJGT12"]))
                        {
                            if (IsNumeric(sitem["HZL12"]) && sitem["HZL12"].Trim() != "0")
                                sitem["HJGT12"] = calc_hjgt(GetSafeDouble(sitem["HZL12"]), GetSafeDouble(sitem["HJST12"].Trim()), GetSafeDouble(sitem["HSL11"].Trim()));
                        }
                        sitem["SZL12"] = (Conversion.Val(sitem["HJST12"]) - Conversion.Val(sitem["HJGT12"])).ToString();
                        sitem["GTZL12"] = (Conversion.Val(sitem["HJGT12"]) - Conversion.Val(sitem["HZL12"])).ToString();
                        sitem["HSL12"] = Round(Conversion.Val(sitem["SZL12"]) / Conversion.Val(sitem["GTZL12"]) * 100, 1).ToString("0.0");
                        sitem["SZL21"] = (Conversion.Val(sitem["HJST21"]) - Conversion.Val(sitem["HJGT21"])).ToString();
                        sitem["GTZL21"] = (Conversion.Val(sitem["HJGT21"]) - Conversion.Val(sitem["HZL21"])).ToString();
                        sitem["HSL21"] = Round(Conversion.Val(sitem["SZL21"]) / Conversion.Val(sitem["GTZL21"]) * 100, 1).ToString("0.0");
                        if (sitem["HJGT22"].Trim() == "0" || string.IsNullOrEmpty(sitem["HJGT22"]) || !IsNumeric(sitem["HJGT22"]))
                        {
                            if (IsNumeric(sitem["HZL22"]) && sitem["HZL22"].Trim() != "0")
                                sitem["HJGT22"] = calc_hjgt(GetSafeDouble(sitem["HZL22"].Trim()), GetSafeDouble(sitem["HJST22"].Trim()), GetSafeDouble(sitem["HSL21"].Trim()));
                        }


                        sitem["SZL22"] = (Conversion.Val(sitem["HJST22"]) - Conversion.Val(sitem["HJGT22"])).ToString();
                        sitem["GTZL22"] = (Conversion.Val(sitem["HJGT22"]) - Conversion.Val(sitem["HZL22"])).ToString();
                        sitem["HSL22"] = Round(Conversion.Val(sitem["SZL22"]) / Conversion.Val(sitem["GTZL22"]) * 100, 1).ToString("0.0");
                        sitem["PJHSL1"] = Round(Conversion.Val(sitem["HSL11"]) + Conversion.Val(sitem["HSL12"]) / 2, 1).ToString("0.0");
                        sitem["PJHSL2"] = Round(Conversion.Val(sitem["HSL21"]) + Conversion.Val(sitem["HSL22"]) / 2, 1).ToString("0.0");
                        if (Conversion.Val(sitem["HSL11"]) >= 40 && Conversion.Val(sitem["HSL12"]) >= 40)
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL11"]) - Conversion.Val(sitem["HSL12"])) > 2)
                                sitem["PJHSL1"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL11"]) - Conversion.Val(sitem["HSL12"])) > 1)
                                sitem["PJHSL1"] = "无效";
                        }
                        if (Conversion.Val(sitem["HSL21"]) >= 40 && Conversion.Val(sitem["HSL22"]) >= 40)
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL21"]) - Conversion.Val(sitem["HSL22"])) > 2)
                                sitem["PJHSL2"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL21"]) - Conversion.Val(sitem["HSL22"])) > 1)
                                sitem["PJHSL2"] = "无效";
                        }
                        sitem["GMD1"] = Round((Conversion.Val(sitem["SMD1"])) / (1 + 0.01 * (Conversion.Val(sitem["PJHSL1"]))), 2).ToString("0.00");
                        sitem["GMD2"] = Round((Conversion.Val(sitem["SMD2"])) / (1 + 0.01 * (Conversion.Val(sitem["PJHSL2"]))), 2).ToString("0.00");
                        if (Math.Abs((Conversion.Val(sitem["GMD1"])) - (Conversion.Val(sitem["GMD2"]))) <= 0.03)
                            sitem["GMD"] = Round((Conversion.Val(sitem["GMD1"]) + Conversion.Val(sitem["GMD2"])) / 2, 2).ToString("0.00");
                        else
                            sitem["GMD"] = "无效";
                        if (Conversion.Val(sitem["ZDGMD"]) == 0)
                            sitem["PJYSD"] = "0";
                        else
                            sitem["PJYSD"] = Round(100 * (Conversion.Val(sitem["GMD"])) / (Conversion.Val(sitem["ZDGMD"])), 0).ToString("0");
                        iZs = iZs + 1;
                        if (Conversion.Val(sitem["SJYSD"]) <= Conversion.Val(sitem["PJYSD"]))
                            iHgs = iHgs + 1;
                        sitem["JCJG"] = "----";
                    }
                    else
                    {
                        sitem["GMD"] = "----";
                        sitem["PJYSD"] = "----";
                    }
                }
            }
            mitem["JCDS"] = SItem.Count().ToString();
            iZs = SItem.Count();
            if (iZs != 0)
                mitem["HGL"] = Round((100 * iHgs / iZs), 1).ToString("0.0");
            if (Conversion.Val(mitem["HGL"]) < 100)
                mAllHg = false;
            //主表总判断赋值
            if (mAllHg)
                mitem["JCJG"] = "合格";
            else
                mitem["JCJG"] = "不合格";
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
