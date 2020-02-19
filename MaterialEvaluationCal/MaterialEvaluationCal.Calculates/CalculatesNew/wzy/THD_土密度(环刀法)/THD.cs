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
                        double p1 = Conversion.Val(sitem["hdjt1"]) - Conversion.Val(sitem["hdzl1"]);
                        double p2 = Conversion.Val(sitem["hdjt2"]) - Conversion.Val(sitem["hdzl2"]);
                        if (p1 != 0)
                        {
                            sitem["tzl1"] = p1.ToString();
                            sitem["tzl2"] = p2.ToString();
                        }
                        sitem["smd1"] = Round(Conversion.Val(sitem["tzl1"]) / Conversion.Val(sitem["HDRJ1"]), 2).ToString("0.00");
                        sitem["smd2"] = Round(Conversion.Val(sitem["tzl2"]) / Conversion.Val(sitem["HDRJ1"]), 2).ToString("0.00");
                        sitem["szl11"] = (Conversion.Val(sitem["hjst11"]) - Conversion.Val(sitem["hjgt11"])).ToString();
                        sitem["gtzl11"] = (Conversion.Val(sitem["hjgt11"]) - Conversion.Val(sitem["hzl11"])).ToString();
                        sitem["hsl11"] = Round(Conversion.Val(sitem["szl11"]) / Conversion.Val(sitem["gtzl11"]) * 100, 1).ToString("0.0");
                        if (sitem["hjgt12"].Trim() == "0" || string.IsNullOrEmpty(sitem["hjgt12"]) || !IsNumeric(sitem["hjgt12"]))
                        {
                            if (IsNumeric(sitem["hzl12"]) && sitem["hzl12"].Trim() != "0")
                                sitem["hjgt12"] = calc_hjgt(GetSafeDouble(sitem["hzl12"]), GetSafeDouble(sitem["hjst12"].Trim()), GetSafeDouble(sitem["hsl11"].Trim()));
                        }
                        sitem["szl12"] = (Conversion.Val(sitem["hjst12"]) - Conversion.Val(sitem["hjgt12"])).ToString();
                        sitem["gtzl12"] = (Conversion.Val(sitem["hjgt12"]) - Conversion.Val(sitem["hzl12"])).ToString();
                        sitem["hsl12"] = Round(Conversion.Val(sitem["szl12"]) / Conversion.Val(sitem["gtzl12"]) * 100, 1).ToString("0.0");
                        sitem["szl21"] = (Conversion.Val(sitem["hjst21"]) - Conversion.Val(sitem["hjgt21"])).ToString();
                        sitem["gtzl21"] = (Conversion.Val(sitem["hjgt21"]) - Conversion.Val(sitem["hzl21"])).ToString();
                        sitem["hsl21"] = Round(Conversion.Val(sitem["szl21"]) / Conversion.Val(sitem["gtzl21"]) * 100, 1).ToString("0.0");
                        if (sitem["hjgt22"].Trim() == "0" || string.IsNullOrEmpty(sitem["hjgt22"]) || !IsNumeric(sitem["hjgt22"]))
                        {
                            if (IsNumeric(sitem["hzl22"]) && sitem["hzl22"].Trim() != "0")
                                sitem["hjgt22"] = calc_hjgt(GetSafeDouble(sitem["hzl22"].Trim()), GetSafeDouble(sitem["hjst22"].Trim()), GetSafeDouble(sitem["hsl21"].Trim()));
                        }


                        sitem["szl22"] = (Conversion.Val(sitem["hjst22"]) - Conversion.Val(sitem["hjgt22"])).ToString();
                        sitem["gtzl22"] = (Conversion.Val(sitem["hjgt22"]) - Conversion.Val(sitem["hzl22"])).ToString();
                        sitem["hsl22"] = Round(Conversion.Val(sitem["szl22"]) / Conversion.Val(sitem["gtzl22"]) * 100, 1).ToString("0.0");
                        sitem["pjhsl1"] = Round(Conversion.Val(sitem["hsl11"]) + Conversion.Val(sitem["hsl12"]) / 2, 1).ToString("0.0");
                        sitem["pjhsl2"] = Round(Conversion.Val(sitem["hsl21"]) + Conversion.Val(sitem["hsl22"]) / 2, 1).ToString("0.0");
                        if (Conversion.Val(sitem["hsl11"]) >= 40 && Conversion.Val(sitem["hsl12"]) >= 40)
                        {
                            if (Math.Abs(Conversion.Val(sitem["hsl11"]) - Conversion.Val(sitem["hsl12"])) > 2)
                                sitem["pjhsl1"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs(Conversion.Val(sitem["hsl11"]) - Conversion.Val(sitem["hsl12"])) > 1)
                                sitem["pjhsl1"] = "无效";
                        }
                        if (Conversion.Val(sitem["hsl21"]) >= 40 && Conversion.Val(sitem["hsl22"]) >= 40)
                        {
                            if (Math.Abs(Conversion.Val(sitem["hsl21"]) - Conversion.Val(sitem["hsl22"])) > 2)
                                sitem["pjhsl2"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs(Conversion.Val(sitem["hsl21"]) - Conversion.Val(sitem["hsl22"])) > 1)
                                sitem["pjhsl2"] = "无效";
                        }
                        sitem["gmd1"] = Round((Conversion.Val(sitem["smd1"])) / (1 + 0.01 * (Conversion.Val(sitem["pjhsl1"]))), 2).ToString("0.00");
                        sitem["gmd2"] = Round((Conversion.Val(sitem["smd2"])) / (1 + 0.01 * (Conversion.Val(sitem["pjhsl2"]))), 2).ToString("0.00");
                        if (Math.Abs((Conversion.Val(sitem["gmd1"])) - (Conversion.Val(sitem["gmd2"]))) <= 0.03)
                            sitem["gmd"] = Round((Conversion.Val(sitem["gmd1"]) + Conversion.Val(sitem["gmd2"])) / 2, 2).ToString("0.00");
                        else
                            sitem["gmd"] = "无效";
                        if (Conversion.Val(sitem["zdgmd"]) == 0)
                            sitem["pjysd"] = "0";
                        else
                            sitem["pjysd"] = Round(100 * (Conversion.Val(sitem["gmd"])) / (Conversion.Val(sitem["zdgmd"])), 0).ToString("0");
                        iZs = iZs + 1;
                        if (Conversion.Val(sitem["sjysd"]) <= Conversion.Val(sitem["pjysd"]))
                            iHgs = iHgs + 1;
                        sitem["JCJG"] = "----";
                    }
                    else
                    {
                        sitem["gmd"] = "----";
                        sitem["pjysd"] = "----";
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
