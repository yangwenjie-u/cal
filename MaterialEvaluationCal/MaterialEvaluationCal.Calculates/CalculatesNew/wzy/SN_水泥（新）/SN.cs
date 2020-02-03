using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SN : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            double[] mkyqdArray = new double[6];
            double[] mkyhzArray = new double[6];
            string[] mtmpArray;
            double mMj;
            double mMaxKyqd, mMinKyqd, mavgkyqd;
            double mKYPJ3z, mKZPJ3z;
            double mXdbz, mXdbz2, mXdbz3, mCdbz1, mCdbz2, mCnsj, mZnsj, mKy_3, mKy_7, mKy_28 = 0, mKz_3, mKz_7, mKz_28;
            string mSjdjbh, mSjdj = "", mSnzl;
            int vp;
            string mjlgs;
            string mMaxBgbh;
            string mJSFF, mjcxm;
            bool mAllHg;
            bool mGetBgbh;
            string mSjddj, mDjMc;
            bool cd_hg, xd_hg, cn_hg, zn_hg, ky3_hg, ky28_hg, kz3_hg, kz28_hg, adx_hg;
            bool mSFwc;
            bool canUpdate;
            bool mSFcjs;
            bool mFlag_Hg, mFlag_Bhg;
            mSFwc = true;
            #endregion

            #region 自定义函数

            //以下为对用设计值的判定方式
            Func<string, string, string> calc_pd =
                delegate (string sj, string sc)
                {
                    sj = sj.Trim();
                    string calc_pd_ret = "";
                    bool isStart = false;
                    string tmpStr = string.Empty;
                    if (string.IsNullOrEmpty(sc))
                        sc = "";
                    for (int i = 1; i <= sc.Length; i++)
                    {
                        if (IsNumeric(sc.Substring(i - 1, 1)) || sc.Substring(i - 1, 1) == "." || sc.Substring(i - 1, 1) == "-")
                        {
                            isStart = true;
                            tmpStr = tmpStr + sc.Substring(i - 1, 1);
                        }
                        else
                        {
                            if (isStart == false && tmpStr != "")
                                break;
                        }
                    }
                    sc = tmpStr;
                    if (!IsNumeric(sc))
                    {
                        calc_pd_ret = "----";
                    }
                    else
                    {
                        double min_sjz, max_sjz, scz;
                        int length, dw;
                        if (sj.Contains("＞"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("＞") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains(">"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf(">") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("≥"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("≥") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("＜"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("＜") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("<"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("<") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("≤"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("≤") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("="))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("=") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz == max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("～"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("～") + 1;
                            min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= min_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            min_sjz = Conversion.Val(tmpStr);
                            max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= max_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            max_sjz = Conversion.Val(tmpStr);
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("~"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("~") + 1;
                            min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= min_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            min_sjz = Conversion.Val(tmpStr);
                            max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= max_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            max_sjz = Conversion.Val(tmpStr);
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
                        }
                    }
                    return calc_pd_ret;
                };

            //这个函数总括了对于判定(符合,不符合) 还是判断(合格,不合格)
            Func<string, string, bool, string> calc_PB =
                delegate (string sj_fun, string sc_fun, bool flag_fun)
                {
                    string calc_PB_fun = string.Empty;
                    sj_fun = sj_fun.Trim();
                    sc_fun = sc_fun.Trim();
                    if (!IsNumeric(sc_fun))
                    {
                        calc_PB_fun = "----";
                    }
                    else
                    {
                        sj_fun = sj_fun.Replace("~", "～");
                        string l_bl, r_bl;
                        double min_sjz, max_sjz, scz;
                        int length, dw;
                        bool min_bl, max_bl, sign_fun;
                        min_sjz = -99999;
                        max_sjz = 99999;
                        scz = GetSafeDouble(sc_fun);
                        sign_fun = false;
                        min_bl = false;
                        max_bl = false;
                        if (sj_fun.Contains("＞"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("＞") + 1;
                            l_bl = sj_fun.Substring(0,dw - 1);
                            r_bl = sj_fun.Substring(dw, length - dw);
                            if (IsNumeric(l_bl))
                            {
                                max_sjz = GetSafeDouble(l_bl);
                                max_bl = false;
                            }
                            if (IsNumeric(r_bl))
                            {
                                min_sjz = GetSafeDouble(r_bl);
                                min_bl = false;
                            }
                            sign_fun = true;
                        }
                        if (sj_fun.Contains("≥"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("≥") + 1;
                            l_bl = sj_fun.Substring(0,dw - 1);
                            r_bl = sj_fun.Substring(dw, length - dw);
                            if (IsNumeric(l_bl))
                            {
                                max_sjz = GetSafeDouble(l_bl);
                                max_bl = true;
                            }
                            if (IsNumeric(r_bl))
                            {
                                min_sjz = GetSafeDouble(r_bl);
                                min_bl = true;
                            }
                            sign_fun = true;
                        }
                        if (sj_fun.Contains("＜"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("＜") + 1;
                            l_bl = sj_fun.Substring(0,dw - 1);
                            r_bl = sj_fun.Substring(dw, length - dw);
                            if (IsNumeric(l_bl))
                            {
                                min_sjz = GetSafeDouble(l_bl);
                                min_bl = false;
                            }
                            if (IsNumeric(r_bl))
                            {
                                max_sjz = GetSafeDouble(r_bl);
                                max_bl = false;
                            }
                            sign_fun = true;
                        }
                        if (sj_fun.Contains("≤"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("≤") + 1;
                            l_bl = sj_fun.Substring(0,dw - 1);
                            r_bl = sj_fun.Substring(dw, length - dw);
                            if (IsNumeric(l_bl))
                            {
                                min_sjz = GetSafeDouble(l_bl);
                                min_bl = true;
                            }
                            if (IsNumeric(r_bl))
                            {
                                max_sjz = GetSafeDouble(r_bl);
                                max_bl = true;
                            }
                            sign_fun = true;
                        }
                        if (sj_fun.Contains("～"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("～") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0,dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_bl = true;
                            max_bl = true;
                            sign_fun = true;
                        }
                        if (sj_fun.Contains("±"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("±") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0,dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_sjz = min_sjz - max_sjz;
                            max_sjz = min_sjz + 2 * max_sjz;
                            min_bl = true;
                            max_bl = true;
                            sign_fun = true;
                        }
                        if (sj_fun == "0" && !string.IsNullOrEmpty(sj_fun))
                        {
                            sign_fun = true;
                            min_bl = false;
                            max_bl = false;
                            max_sjz = 0;
                        }
                        if (!sign_fun)
                        {
                            calc_PB_fun = "----";
                        }
                        else
                        {
                            string hgjl, bhgjl;
                            hgjl = flag_fun ? "符合" : "合格";
                            bhgjl = flag_fun ? "不符合" : "不合格";
                            sign_fun = true; //做为判定了
                            if (min_bl)
                                sign_fun = scz >= min_sjz ? sign_fun : false;
                            else
                                sign_fun = scz > min_sjz ? sign_fun : false;
                            if (max_bl)
                                sign_fun = scz <= max_sjz ? sign_fun : false;
                            else
                                sign_fun = scz < max_sjz ? sign_fun : false;
                            calc_PB_fun = sign_fun ? hgjl : bhgjl;
                        }
                    }
                    return calc_PB_fun;
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_SN_DJ"];
            var mrsAdx = dataExtra["BZ_NADXFF"];
            var mrsKqnd = dataExtra["BZ_SNKQND"];
            var MItem = data["M_SN"];
            var mitem = MItem[0];
            var SItem = data["S_SN"];
            #endregion

            #region  计算开始
            mGetBgbh = false;
            mAllHg = true;
            adx_hg = true;
            cd_hg = true;
            xd_hg = true;
            cn_hg = true;
            zn_hg = true;
            ky3_hg = true;
            kz3_hg = true;
            ky28_hg = true;
            kz28_hg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            mSFcjs = false;
            bool force = true;
            var SITEM = SItem[0];
            //if (string.IsNullOrEmpty(SITEM["MAKEREP_BZ"]))
            //    SITEM["MAKEREP_BZ"] = "0";
            //foreach (var sitem in SItem)
            //{
            //    mSFcjs = (mSFcjs || sitem["MAKEREP_BZ"] == "1");
            //}
            mitem["SYZT"] = "0";
            mjcxm = "、";
            foreach (var sitem in SItem)
            {
                mSjdj = sitem["SJDJ"];            //设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                mjcxm = mjcxm + sitem["JCXM"] + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj));
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    string aaa = mrsDj_Filter["BH"];
                    mCdbz1 = Conversion.Val(mrsDj_Filter["CDBZ1"]);
                    mCdbz2 = Conversion.Val(mrsDj_Filter["CDBZ2"]);
                    mXdbz = Conversion.Val(mrsDj_Filter["XDBZ"]);
                    mXdbz2 = Conversion.Val(mrsDj_Filter["XDBZ2"]);
                    mXdbz3 = Conversion.Val(mrsDj_Filter["XDBZ3"]);
                    mCnsj = Conversion.Val(mrsDj_Filter["CNSJ"]);
                    mZnsj = Conversion.Val(mrsDj_Filter["ZNSJ"]);
                    sitem["G_KYBZ3"] = Conversion.Val(mrsDj_Filter["KY_3"]).ToString();
                    sitem["G_KYBZ28"] = Conversion.Val(mrsDj_Filter["KY_28"]).ToString();
                    sitem["G_KZBZ3"] = Conversion.Val(mrsDj_Filter["KZ_3"]).ToString();
                    sitem["G_KZBZ28"] = Conversion.Val(mrsDj_Filter["KZ_28"]).ToString();
                }
                else
                {
                    mCdbz1 = 0;
                    mCdbz2 = 0;
                    mCnsj = 0;
                    mZnsj = 0;
                    mKy_3 = 0;
                    mKy_7 = 0;
                    mKy_28 = 0;
                    mKz_3 = 0;
                    mKz_7 = 0;
                    mKz_28 = 0;
                    sitem["JCJG"] = "依据不详";
                    continue;
                }
                if (string.IsNullOrEmpty(mXdbz.ToString()))
                    mXdbz = 0;
                if (string.IsNullOrEmpty(mCdbz1.ToString()))
                    mCdbz1 = 0;
                if (string.IsNullOrEmpty(mCdbz2.ToString()))
                    mCdbz2 = 0;
                mitem["G_XD"] = mrsDj_Filter["XDBZ"];
                mitem["G_XD2"] = mrsDj_Filter["XDBZ2"];
                mitem["G_CNSJ"] = mrsDj_Filter["CNSJ"];
                mitem["G_ZNSJ"] = mrsDj_Filter["ZNSJ"];
                double md1, md2, pjmd, md, sum;
                int xd, Gs;
                double[] nArr;
                bool flag, sign;
                if (sitem["JCXM"].Contains("抗压快速法"))
                {
                    md1 = Conversion.Val(sitem["KS_DDXSA"].Trim());
                    md2 = Conversion.Val(sitem["KS_DDXSB"].Trim());
                    sum = 0;
                    nArr = new double[7];
                    for (xd = 1; xd <= 6; xd++)
                    {
                        md = Conversion.Val(sitem["KS_KYHZ" + xd].Trim());
                        md = md / 0.625;
                        md = Round(md, 1);
                        md = md1 * md + md2;
                        md = Round(md, 1);
                        nArr[xd] = md;
                        sitem["KS_KYQD" + xd] = md.ToString("0.0");
                        sum = sum + md;
                    }
                    Array.Sort(nArr);
                    pjmd = sum / 6;
                    pjmd = Round(pjmd, 1);
                    sitem["KS_PJQD"] = pjmd.ToString("0.0");
                    sitem["KS_YQQD"] = "≥" + mKy_28.ToString("0.0");
                    sitem["KS_PDQD"] = calc_PB(sitem["KS_YQQD"], sitem["KS_PJQD"], false);
                    //抗压合格判定
                    //if(mitem["JCYJ"].Contains("2011"))
                    // mrsmainTablesitem["WHICH") = "bgsn_3"
                    //Else
                    // mrsmainTablesitem["WHICH") = "bgsn_1"
                    //End If
                }
                else
                {
                    sitem["KS_PJQD"] = "----";
                    sitem["KS_YQQD"] = "----";
                    sitem["KS_PDQD"] = "----";
                    for (xd = 1; xd <= 6; xd++)
                        sitem["KS_KYQD" + xd] = "----";
                    //If InStr(1, mitem["JCYJ, "2011") > 0 Then
                    // mrsmainTablesitem["WHICH") = "bgsn_2"
                    //Else
                    // mrsmainTablesitem["WHICH") = "bgsn"
                    //End If
                }
                mitem["KY3_HG"] = "----";
                mitem["KZ3_HG"] = "----";
                mitem["KY28_HG"] = "----";
                mitem["KZ28_HG"] = "----";
                if (!sitem["JCXM"].Contains("流动度") && !string.IsNullOrEmpty(mitem["LDD1"]))
                {
                    if (Conversion.Val(mitem["LDD1"]) > 10)
                        sitem["JCXM"] = sitem["JCXM"] + "、流动度";
                }
                if (!sitem["JCXM"].Contains("流动度"))
                    mitem["LDD"] = Round((Conversion.Val(mitem["LDD1"]) + Conversion.Val(mitem["LDD2"])) / 2, 0).ToString();
                else
                    mitem["LDD"] = "----";
                if (sitem["JCXM"].Contains("强度（3天）") && Conversion.Val(sitem["KYHZ3_1"]) > 0)
                {
                    mMj = 0.625;
                    sitem["KYQD3_1"] = Round(Conversion.Val(sitem["KYHZ3_1"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD3_2"] = Round(Conversion.Val(sitem["KYHZ3_2"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD3_3"] = Round(Conversion.Val(sitem["KYHZ3_3"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD3_4"] = Round(Conversion.Val(sitem["KYHZ3_4"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD3_5"] = Round(Conversion.Val(sitem["KYHZ3_5"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD3_6"] = Round(Conversion.Val(sitem["KYHZ3_6"]) * mMj, 1).ToString("0.0");
                    mlongStr = sitem["KYQD3_1"] + "," + sitem["KYQD3_2"] + "," + sitem["KYQD3_3"] + "," + sitem["KYQD3_4"] + "," + sitem["KYQD3_5"] + "," + sitem["KYQD3_6"];
                    mtmpArray = mlongStr.Split(',');
                    for (vp = 0; vp <= 5; vp++)
                        mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                    Array.Sort(mkyqdArray);
                    mMaxKyqd = mkyqdArray.Max();
                    mMinKyqd = mkyqdArray.Min();
                    mavgkyqd = Round(mkyqdArray.Average(), 1);
                    int mccgs = 0;
                    for (vp = 0; vp <= 5; vp++)
                    {
                        if ((Math.Abs(mkyqdArray[vp] - mavgkyqd)) > (mavgkyqd * 0.1))
                            mccgs = mccgs + 1;
                    }

                    if (mccgs > 1)
                        mavgkyqd = -1;
                    else
                    {

                        if (Math.Abs(mavgkyqd - mkyqdArray[0]) >= Math.Abs(mavgkyqd - mkyqdArray[5]))
                        {
                            if (Math.Abs(mavgkyqd - mkyqdArray[0]) > (mavgkyqd * 0.1))
                                mavgkyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5]) / 5;
                            if (Math.Abs(mavgkyqd - mkyqdArray[1]) > (mavgkyqd * 0.1) || Math.Abs(mavgkyqd - mkyqdArray[5]) > (mavgkyqd * 0.1))
                                mavgkyqd = -1;
                        }
                        else
                        {
                            if (Math.Abs(mavgkyqd - mkyqdArray[5]) > (mavgkyqd * 0.1))
                                mavgkyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[0]) / 5;
                            if (Math.Abs(mavgkyqd - mkyqdArray[0]) > (mavgkyqd * 0.1) || Math.Abs(mavgkyqd - mkyqdArray[4]) > (mavgkyqd * 0.1))
                                mavgkyqd = -1;
                        }
                    }
                    //计算抗压强度平均值:有超出平均值10%的首先剔除再平均,再有超出平均值10%的则作废
                    sitem["KYPJ3"] = Round(mavgkyqd, 1).ToString("0.0");
                    //计算抗折平均值(要剔除其中超出平均值10%的抗折值再平均)
                    if (Conversion.Val(sitem["KZHZ3_1"]) != 0)
                    {
                        sitem["KZQD3_1"] = Round((1.5 * 100 * Conversion.Val(sitem["KZHZ3_1"])) / 40 / 40 / 40, 1).ToString("0.0");
                        sitem["KZQD3_2"] = Round((1.5 * 100 * Conversion.Val(sitem["KZHZ3_2"])) / 40 / 40 / 40, 1).ToString("0.0");
                        sitem["KZQD3_3"] = Round((1.5 * 100 * Conversion.Val(sitem["KZHZ3_3"])) / 40 / 40 / 40, 1).ToString("0.0");
                    }
                    mKZPJ3z = Round((Conversion.Val(sitem["KZQD3_1"]) + Conversion.Val(sitem["KZQD3_2"]) + Conversion.Val(sitem["KZQD3_3"])) / 3, 1);
                    if (Math.Abs(Conversion.Val(sitem["KZQD3_1"]) - mKZPJ3z) > Math.Abs(Conversion.Val(sitem["KZQD3_2"]) - mKZPJ3z) && Math.Abs(Conversion.Val(sitem["KZQD3_1"]) - mKZPJ3z) > Math.Abs(Conversion.Val(sitem["KZQD3_3"]) - mKZPJ3z))
                    {
                        if (Math.Abs(Conversion.Val(sitem["KZQD3_1"]) - mKZPJ3z) > Round((mKZPJ3z * 0.1), 1))
                            mKZPJ3z = (Conversion.Val(sitem["KZQD3_2"]) + Conversion.Val(sitem["KZQD3_3"])) / 2;
                    }
                    if (Math.Abs(Conversion.Val(sitem["KZQD3_2"]) - mKZPJ3z) > Math.Abs(Conversion.Val(sitem["KZQD3_1"]) - mKZPJ3z) && Math.Abs(Conversion.Val(sitem["KZQD3_2"]) - mKZPJ3z) > Math.Abs(Conversion.Val(sitem["KZQD3_3"]) - mKZPJ3z))
                    {
                        if (Math.Abs(Conversion.Val(sitem["KZQD3_2"]) - mKZPJ3z) > Round((mKZPJ3z * 0.1), 1))
                            mKZPJ3z = (Conversion.Val(sitem["KZQD3_1"]) + Conversion.Val(sitem["KZQD3_3"])) / 2;
                    }
                    if (Math.Abs(Conversion.Val(sitem["KZQD3_3"]) - mKZPJ3z) > Math.Abs(Conversion.Val(sitem["KZQD3_1"]) - mKZPJ3z) && Math.Abs(Conversion.Val(sitem["KZQD3_3"]) - mKZPJ3z) > Math.Abs(Conversion.Val(sitem["KZQD3_2"]) - mKZPJ3z))
                    {
                        if (Math.Abs(Conversion.Val(sitem["KZQD3_3"]) - mKZPJ3z) > Round((mKZPJ3z * 0.1), 1))
                            mKZPJ3z = (Conversion.Val(sitem["KZQD3_1"]) + Conversion.Val(sitem["KZQD3_2"])) / 2;
                    }
                    sitem["KZPJ3"] = Round(mKZPJ3z, 1).ToString("0.0");
                    //抗压合格判定
                    if (Conversion.Val(sitem["KYPJ3"]) >= Conversion.Val(sitem["G_KYBZ3"]))
                    {
                        sitem["KY3_HG"] = "合格";
                        ky3_hg = true;
                    }
                    else
                    {
                        if (Conversion.Val(sitem["KYPJ3"]) == -1)
                            sitem["KY3_HG"] = "作废";
                        else
                            sitem["KY3_HG"] = "不合格";
                        ky3_hg = false;
                    }
                    if (!("、" + sitem["JCXM"] + "、").Contains("、强度（3天）、"))
                    {
                        sitem["KY3_HG"] = "----";
                        ky3_hg = true;
                    }
                    else
                    {
                        if (Conversion.Val(sitem["KYHZ3_1"]) == 0)
                        {
                            mitem["SYZT"] = "0";
                            mSFwc = false;
                        }
                    }
                    if (Conversion.Val(sitem["KYPJ3"]) == 0)
                    {
                        sitem["KY3_HG"] = "----";
                        ky3_hg = true;
                    }
                    if (Conversion.Val(sitem["KZPJ3"]) >= Conversion.Val(sitem["G_KZBZ3"]))
                    {
                        sitem["KZ3_HG"] = "合格";
                        kz3_hg = true;
                    }
                    else
                    {
                        sitem["KZ3_HG"] = "不合格";
                        kz3_hg = false;
                    }
                    if (!("、" + sitem["JCXM"] + "、").Contains("、强度（3天）、"))
                    {
                        sitem["KZ3_HG"] = "----";
                        kz3_hg = true;
                    }


                    if (Conversion.Val(sitem["KZPJ3"]) == 0)
                    {
                        sitem["KZ3_HG"] = "----";
                        kz3_hg = true;
                    }
                }
                else
                {
                    sitem["KYQD3_1"] = "0";
                    sitem["KYQD3_2"] = "0";
                    sitem["KYQD3_3"] = "0";
                    sitem["KYQD3_4"] = "0";
                    sitem["KYQD3_5"] = "0";
                    sitem["KYQD3_6"] = "0";
                    sitem["KZQD3_1"] = "0";
                    sitem["KZQD3_2"] = "0";
                    sitem["KZQD3_3"] = "0";
                    sitem["KZPJ3"] = "0";
                    sitem["KYPJ3"] = "0";
                    sitem["G_KYBZ3"] = "0";
                    sitem["G_KZBZ3"] = "0";
                    sitem["KZ3_HG"] = "----";
                    sitem["KY3_HG"] = "----";
                }
                if (sitem["JCXM"].Contains("强度（28天）") && Conversion.Val(sitem["KYHZ28_1"]) > 0)
                {
                    if (Conversion.Val(sitem["KYPJ28"]) == 0)
                    {
                        mitem["BGSHR"] = "";
                        mitem["BGQFR"] = "";
                        mitem["BGDYR"] = "";
                        mitem["BGJDZT"] = mitem["BGJDZT"].Replace("S", "");
                        mitem["BGJDZT"] = mitem["BGJDZT"].Replace("R", "");
                    }
                    mMj = 0.625;
                    sitem["KYQD28_1"] = Round(Conversion.Val(sitem["KYHZ28_1"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD28_2"] = Round(Conversion.Val(sitem["KYHZ28_2"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD28_3"] = Round(Conversion.Val(sitem["KYHZ28_3"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD28_4"] = Round(Conversion.Val(sitem["KYHZ28_4"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD28_5"] = Round(Conversion.Val(sitem["KYHZ28_5"]) * mMj, 1).ToString("0.0");
                    sitem["KYQD28_6"] = Round(Conversion.Val(sitem["KYHZ28_6"]) * mMj, 1).ToString("0.0");
                    mlongStr = sitem["KYQD28_1"] + "," + sitem["KYQD28_2"] + "," + sitem["KYQD28_3"] + "," + sitem["KYQD28_4"] + "," + sitem["KYQD28_5"] + "," + sitem["KYQD28_6"];
                    mtmpArray = mlongStr.Split(',');
                    for (vp = 0; vp <= 5; vp++)
                        mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                    Array.Sort(mkyqdArray);
                    mMaxKyqd = mkyqdArray.Max();
                    mMinKyqd = mkyqdArray.Min();
                    mavgkyqd = Round(mkyqdArray.Average(), 1);
                    int mccgs = 0;
                    for (vp = 0; vp <= 5; vp++)
                    {
                        if (Math.Abs(mkyqdArray[vp] - mavgkyqd) > (mavgkyqd * 0.1))
                            mccgs = mccgs + 1;
                    }

                    if (mccgs > 1)
                        mavgkyqd = -1;
                    else
                    {
                        if (Math.Abs(mavgkyqd - mkyqdArray[0]) >= Math.Abs(mavgkyqd - mkyqdArray[5]))
                        {
                            if (Math.Abs(mavgkyqd - mkyqdArray[0]) > (mavgkyqd * 0.1))
                                mavgkyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5]) / 5;
                            if (Math.Abs(mavgkyqd - mkyqdArray[1]) > (mavgkyqd * 0.1) || Math.Abs(mavgkyqd - mkyqdArray[5]) > (mavgkyqd * 0.1))
                                mavgkyqd = -1;
                        }
                        else
                        {
                            if (Math.Abs(mavgkyqd - mkyqdArray[5]) > (mavgkyqd * 0.1))
                                mavgkyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[0]) / 5;
                            if (Math.Abs(mavgkyqd - mkyqdArray[0]) > (mavgkyqd * 0.1) || Math.Abs(mavgkyqd - mkyqdArray[4]) > (mavgkyqd * 0.1))
                                mavgkyqd = -1;
                        }
                    }


                    if ((string.IsNullOrEmpty(sitem["KYPJ28"]) || sitem["KYPJ28"] == "" || Conversion.Val(sitem["KYPJ28"]) == 0) && Conversion.Val(sitem["KYHZ28_1"]) > 0)
                    {
                        mitem["SHWCRQ"] = DateTime.Now.ToString();
                        mitem["QFWCRQ"] = DateTime.Now.ToString();
                    }
                    sitem["KYPJ28"] = Round(mavgkyqd, 1).ToString("0.0");



                    //计算抗折平均值(要剔除其中超出平均值10%的抗折值再平均)
                    if (Conversion.Val(sitem["KZHZ28_1"]) != 0)
                    {
                        sitem["KZQD28_1"] = Round((1.5 * 100 * Conversion.Val(sitem["KZHZ28_1"])) / 40 / 40 / 40, 1).ToString("0.0");
                        sitem["KZQD28_2"] = Round((1.5 * 100 * Conversion.Val(sitem["KZHZ28_2"])) / 40 / 40 / 40, 1).ToString("0.0");
                        sitem["KZQD28_3"] = Round((1.5 * 100 * Conversion.Val(sitem["KZHZ28_3"])) / 40 / 40 / 40, 1).ToString("0.0");
                    }
                    double mKZPJ28z = Round((Conversion.Val(sitem["KZQD28_1"]) + Conversion.Val(sitem["KZQD28_2"]) + Conversion.Val(sitem["KZQD28_3"])) / 3, 1);
                    if (Math.Abs(Conversion.Val(sitem["KZQD28_1"]) - mKZPJ28z) > Math.Abs(Conversion.Val(sitem["KZQD28_2"]) - mKZPJ28z) && Math.Abs(Conversion.Val(sitem["KZQD28_1"]) - mKZPJ28z) > Math.Abs(Conversion.Val(sitem["KZQD28_3"]) - mKZPJ28z))
                    {
                        if (Math.Abs(Conversion.Val(sitem["KZQD28_1"]) - mKZPJ28z) > Round((mKZPJ28z * 0.1), 1))
                            mKZPJ28z = (Conversion.Val(sitem["KZQD28_2"]) + Conversion.Val(sitem["KZQD28_3"])) / 2;
                    }
                    if (Math.Abs(Conversion.Val(sitem["KZQD28_2"]) - mKZPJ28z) > Math.Abs(Conversion.Val(sitem["KZQD28_1"]) - mKZPJ28z) && Math.Abs(Conversion.Val(sitem["KZQD28_2"]) - mKZPJ28z) > Math.Abs(Conversion.Val(sitem["KZQD28_3"]) - mKZPJ28z))
                    {
                        if (Math.Abs(Conversion.Val(sitem["KZQD28_2"]) - mKZPJ28z) > Round((mKZPJ28z * 0.1), 1))
                            mKZPJ28z = (Conversion.Val(sitem["KZQD28_1"]) + Conversion.Val(sitem["KZQD28_3"])) / 2;
                    }
                    if (Math.Abs(Conversion.Val(sitem["KZQD28_3"]) - mKZPJ28z) > Math.Abs(Conversion.Val(sitem["KZQD28_1"]) - mKZPJ28z) && Math.Abs(Conversion.Val(sitem["KZQD28_3"]) - mKZPJ28z) > Math.Abs(Conversion.Val(sitem["KZQD28_2"]) - mKZPJ28z))
                    {
                        if (Math.Abs(Conversion.Val(sitem["KZQD28_3"]) - mKZPJ28z) > Round((mKZPJ28z * 0.1), 1))
                            mKZPJ28z = (Conversion.Val(sitem["KZQD28_1"]) + Conversion.Val(sitem["KZQD28_2"])) / 2;
                    }
                    sitem["KZPJ28"] = Round((mKZPJ28z), 1).ToString("0.0");
                    //抗压合格判定
                    if (Conversion.Val(sitem["KYPJ28"]) >= Conversion.Val(sitem["G_KYBZ28"]))
                    {
                        sitem["KY28_HG"] = "合格";
                        ky28_hg = true;
                    }
                    else
                    {
                        if (Conversion.Val(sitem["KYPJ28"]) == -1)
                            sitem["KY28_HG"] = "作废";
                        else
                            sitem["KY28_HG"] = "不合格";
                        ky28_hg = false;
                    }
                    if (!("、" + sitem["JCXM"] + "、").Contains("、28天强度、"))
                    {
                        sitem["KY28_HG"] = "----";
                        ky28_hg = true;
                    }
                    if (Conversion.Val(sitem["KYPJ28"]) == 0)
                    {
                        sitem["KY28_HG"] = "----";
                        ky28_hg = true;
                    }
                    //抗折合格判定
                    if (Conversion.Val(sitem["KZPJ28"]) >= Conversion.Val(sitem["G_KZBZ28"]))
                    {
                        sitem["KZ28_HG"] = "合格";
                        kz28_hg = true;
                    }
                    else
                    {
                        sitem["KZ28_HG"] = "不合格";
                        kz28_hg = false;
                    }
                    if (!("、" + sitem["JCXM"] + "、").Contains("、28天强度、"))
                    {
                        sitem["KZ28_HG"] = "----";
                        kz28_hg = true;
                    }


                    if (Conversion.Val(sitem["KZPJ28"]) == 0)
                    {
                        sitem["KZ28_HG"] = "----";
                        kz28_hg = true;
                    }
                }
                else
                {
                    sitem["KYQD28_1"] = "0";
                    sitem["KYQD28_2"] = "0";
                    sitem["KYQD28_3"] = "0";
                    sitem["KYQD28_4"] = "0";
                    sitem["KYQD28_5"] = "0";
                    sitem["KYQD28_6"] = "0";
                    sitem["KZQD28_1"] = "0";
                    sitem["KZQD28_2"] = "0";
                    sitem["KZQD28_3"] = "0";
                    sitem["KZPJ28"] = "0";
                    sitem["KYPJ28"] = "0";
                    sitem["G_KYBZ28"] = "0";
                    sitem["G_KZBZ28"] = "0";
                    sitem["KZ28_HG"] = "----";
                    sitem["KY28_HG"] = "----";
                }
                if (sitem["JCXM"].Contains("强度（28天）"))
                {
                    if (Conversion.Val(sitem["KYPJ3"]) != 0)
                    {
                        if (Conversion.Val(sitem["KYPJ28"]) == 0)
                        {
                            mitem["SYZT"] = "0";
                            DateTime dtnow = DateTime.Now;
                            if (DateTime.TryParse(sitem["ZZRQ"], out dtnow))
                                sitem["YQSYRQ"] = GetSafeDateTime(sitem["ZZRQ"]).AddDays(28).ToString();
                        }
                        else
                            mitem["SYZT"] = "1";
                    }
                }
                if (string.IsNullOrEmpty(mitem["SFADX"]))
                    mitem["SFADX"] = "0";
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (!jcxm.Contains("、安定性、"))
                {
                    mitem["ADXFF"] = "----";
                    mitem["ADX_HG"] = "----";
                    mitem["G_ADX"] = "----";
                    mitem["ADX"] = "----";
                    mitem["BZFPJ"] = "----";
                    mitem["BZFXC"] = "----";
                    mitem["G_BZFPJ"] = "----";
                    mitem["G_BZFXC"] = "----";
                    adx_hg = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(mitem["ADXFF"]) || mitem["ADXFF"] == "----" || mitem["ADXFF"] == "")
                        mSFwc = false;
                    else
                    {
                        if (Conversion.Val(sitem["KYPJ3"]) == 0 && mitem["SFADX"] == "0" && jcxm.Contains("、3天强度、"))
                            sitem["YQSYRQ"] = GetSafeDateTime(sitem["ZZRQ"]).AddDays(3).ToString();
                        if (mitem["ADXFF"] == "----")
                            mitem["ADXFF"] = "代用法";
                        var mrsAdx_Filter = mrsAdx.FirstOrDefault(x => x["MC"].Contains(mitem["ADXFF"].Trim()));
                        if (mrsAdx_Filter != null && mrsAdx_Filter.Count() > 0)
                        {
                            mitem["G_ADX"] = mrsAdx_Filter["G_ADX"].Trim();
                            mitem["G_BZFPJ"] = mrsAdx_Filter["BZFPJ"];
                            mitem["G_BZFXC"] = mrsAdx_Filter["BZFXC"];
                        }
                        //安定性
                        if (mitem["ADXFF"].Trim() == "代用法")
                        {
                            if (mitem["ADX"].Trim() == "完整" || mitem["ADX"].Trim() == "无裂缝，无弯曲")
                            {
                                adx_hg = true;
                                mitem["ADX_HG"] = "合格";
                            }
                            else
                            {
                                mitem["ADX_HG"] = "不合格";
                                adx_hg = false;
                            }
                        }
                        else
                        {
                            mitem["BZFPJ"] = Round((((Conversion.Val(mitem["BZFC_1"]) - Conversion.Val(mitem["BZFA_1"])) + (Conversion.Val(mitem["BZFC_2"]) - Conversion.Val(mitem["BZFA_2"]))) / 2), 1).ToString();
                            mitem["BZFXC"] = Round((Math.Abs((Conversion.Val(mitem["BZFC_1"]) - Conversion.Val(mitem["BZFA_1"])) - (Conversion.Val(mitem["BZFC_2"]) - Conversion.Val(mitem["BZFA_2"])))), 1).ToString();
                            if (IsNumeric(mitem["FBZFC_1"]))
                            {
                                mitem["FBZFPJ"] = Round((((Conversion.Val(mitem["FBZFC_1"]) - Conversion.Val(mitem["FBZFA_1"])) + (Conversion.Val(mitem["FBZFC_2"]) - Conversion.Val(mitem["FBZFA_2"]))) / 2), 1).ToString();
                                if ((Conversion.Val(mitem["FBZFPJ"])) <= (Conversion.Val(mitem["G_BZFPJ"])))
                                {
                                    adx_hg = true;
                                    mitem["ADX_HG"] = "合格";
                                }
                                else
                                {
                                    adx_hg = false;
                                    mitem["ADX_HG"] = "不合格";
                                }
                            }
                            else
                            {
                                mitem["FBZFC_1"] = "----";
                                mitem["FBZFA_1"] = "----";
                                mitem["FBZFC_2"] = "----";
                                mitem["FBZFA_2"] = "----";
                                mitem["FBZFPJ"] = "----";
                                if (mitem["JCYJ"].Contains("2011"))
                                {
                                    if (Conversion.Val(mitem["BZFPJ"]) <= Conversion.Val(mitem["G_BZFPJ"]))
                                    {
                                        adx_hg = true;
                                        mitem["ADX_HG"] = "合格";
                                    }
                                    else
                                    {
                                        adx_hg = false;
                                        mitem["ADX_HG"] = "不合格";
                                    }
                                }
                                else
                                {
                                    if (Conversion.Val(mitem["BZFPJ"]) <= Conversion.Val(mitem["G_BZFPJ"]) && Conversion.Val(mitem["BZFXC"]) <= Conversion.Val(mitem["G_BZFXC"]))
                                    {
                                        adx_hg = true;
                                        mitem["ADX_HG"] = "合格";
                                    }
                                    else
                                    {
                                        adx_hg = false;
                                        mitem["ADX_HG"] = "不合格";
                                    }
                                }
                            }
                        }
                    }
                }
                //稠度合格判定
                if (!jcxm.Contains("、安定性、") && !jcxm.Contains("、凝结时间、"))
                {
                    mitem["CD_HG"] = "----";
                    mitem["G_CD"] = "----";
                    mitem["CD"] = "----";
                    cd_hg = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(mitem["BZCDYSL"]))
                        mitem["BZCDYSL"] = "0";
                    mitem["CD"] = Round(Conversion.Val(mitem["BZCDYSL"]) / 500 * 100, 1).ToString();
                    if (Conversion.Val(mitem["CD"]) > 0)
                    {
                        if (Conversion.Val(mitem["CD"]) >= mCdbz1 && Conversion.Val(mitem["CD"]) <= mCdbz2)
                        {
                            mitem["CD_HG"] = "合格";
                            cd_hg = true;
                        }
                        else
                        {
                            mitem["CD_HG"] = "不合格";
                            cd_hg = false;
                        }
                        if ((mCdbz1 == 0 && mCdbz2 == 0) || string.IsNullOrEmpty(mitem["CD"]) || Conversion.Val(mitem["CD"]) == 0)
                        {
                            mitem["CD_HG"] = "----";
                            cd_hg = true;
                        }
                    }
                    else
                    {
                        mitem["CD_HG"] = "----";
                        cd_hg = true;
                    }

                }
                //细度判定
                if (jcxm.Contains("、细度、"))
                {
                    sitem["MD1"] = ((int)(Round(Conversion.Val(sitem["MDSYZL1"]) / (Conversion.Val(sitem["MDYYTJ1"]) - Conversion.Val(sitem["MDYTJ1"])), 4) * 1000) / 1000).ToString("0.00");
                    sitem["MD2"] = ((int)(Round(Conversion.Val(sitem["MDSYZL2"]) / (Conversion.Val(sitem["MDYYTJ2"]) - Conversion.Val(sitem["MDYTJ2"])), 4) * 1000) / 1000).ToString("0.00");
                    if (Math.Abs(Conversion.Val(sitem["MD1"]) - Conversion.Val(sitem["MD2"])) > 0.02)
                        sitem["MD"] = "无效";
                    else
                        sitem["MD"] = ((Conversion.Val(sitem["MD1"]) + Conversion.Val(sitem["MD2"])) / 2).ToString("0.00");
                }
                if (jcxm.Contains("、比表面积、") && Conversion.Val(sitem["MD"]) != 0)
                {
                    var mrsKqnd_Filter = mrsKqnd.FirstOrDefault(x => x["MC"].Contains(sitem["XZSWD"].Trim()));
                    if (mrsKqnd_Filter != null && mrsKqnd_Filter.Count() > 0)
                        sitem["BYKQYD"] = mrsKqnd_Filter["KQND"];
                    mrsKqnd_Filter = mrsKqnd.FirstOrDefault(x => x["MC"].Contains(sitem["SYSWD"].Trim()));
                    if (mrsKqnd_Filter != null && mrsKqnd_Filter.Count() > 0)
                        sitem["SYKQYD"] = mrsKqnd_Filter["KQND"];
                    mrsKqnd_Filter = mrsKqnd.FirstOrDefault(x => x["MC"].Contains(sitem["SYSWD2"].Trim()));
                    if (mrsKqnd_Filter != null && mrsKqnd_Filter.Count() > 0)
                        sitem["SYKQYD2"] = mrsKqnd_Filter["KQND"];
                    if (Conversion.Val(sitem["MD"]) == Conversion.Val(sitem["BYMD"]) && Conversion.Val(sitem["SYKXL"]) == Conversion.Val(sitem["BYKXL"]))
                    {
                        if (Math.Abs(Conversion.Val(sitem["SYSWD"]) - Conversion.Val(sitem["XZSWD"])) <= 3)
                            mitem["XD_1"] = (Conversion.Val(sitem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sitem["SYT"])) / Math.Sqrt(Conversion.Val(sitem["BYT"]))).ToString();
                        else
                            mitem["XD_1"] = (Conversion.Val(sitem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sitem["SYT"])) * Math.Sqrt(Conversion.Val(sitem["BYKQYD"])) / Math.Sqrt(Conversion.Val(sitem["BYT"])) / Math.Sqrt(Conversion.Val(sitem["SYKQYD"]))).ToString();
                    }
                    if (Conversion.Val(sitem["MD"]) == Conversion.Val(sitem["BYMD"]) && Conversion.Val(sitem["SYKXL"]) != Conversion.Val(sitem["BYKXL"]))
                    {
                        if (Math.Abs(Conversion.Val(sitem["SYSWD"]) - Conversion.Val(sitem["XZSWD"])) <= 3)
                        {
                            md1 = Conversion.Val(sitem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sitem["SYT"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Math.Sqrt(Conversion.Val(sitem["BYT"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"])));
                            mitem["XD_1"] = Round(md1, 5).ToString();
                        }
                        else
                            mitem["XD_1"] = (Conversion.Val(sitem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sitem["SYT"])) * Math.Sqrt(Conversion.Val(sitem["BYKQYD"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Math.Sqrt(Conversion.Val(sitem["BYT"])) * Math.Sqrt(Conversion.Val(sitem["SYKQYD"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"])))).ToString();

                    }
                    if (Conversion.Val(sitem["MD"]) != Conversion.Val(sitem["BYMD"]) && Conversion.Val(sitem["SYKXL"]) != Conversion.Val(sitem["BYKXL"]))
                    {
                        if (Math.Abs(Conversion.Val(sitem["SYSWD"]) - Conversion.Val(sitem["XZSWD"])) <= 3)
                            mitem["XD_1"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Conversion.Val(sitem["BYMD"]) * Math.Sqrt(Conversion.Val(sitem["SYT"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Conversion.Val(sitem["MD"]) * Math.Sqrt(Conversion.Val(sitem["BYT"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                        else
                            mitem["XD_1"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Conversion.Val(sitem["BYMD"]) * Math.Sqrt(Conversion.Val(sitem["SYT"])) * Math.Sqrt(Conversion.Val(sitem["BYKQYD"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Conversion.Val(sitem["MD"]) * Math.Sqrt(Conversion.Val(sitem["BYT"])) * Math.Sqrt(Conversion.Val(sitem["SYKQYD"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                    }
                    //温州
                    if (Conversion.Val(sitem["MD"]) != Conversion.Val(sitem["BYMD"]) && Conversion.Val(sitem["SYKXL"]) == Conversion.Val(sitem["BYKXL"]))
                    {
                        if (Math.Abs(Conversion.Val(sitem["SYSWD"]) - Conversion.Val(sitem["XZSWD"])) <= 3)
                            mitem["XD_1"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Conversion.Val(sitem["BYMD"]) * Math.Sqrt(Conversion.Val(sitem["SYT"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Conversion.Val(sitem["MD"]) * Math.Sqrt(Conversion.Val(sitem["BYT"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                        else
                            mitem["XD_1"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Conversion.Val(sitem["BYMD"]) * Math.Sqrt(Conversion.Val(sitem["SYT"])) * Math.Sqrt(Conversion.Val(sitem["BYKQYD"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Conversion.Val(sitem["MD"]) * Math.Sqrt(Conversion.Val(sitem["BYT"])) * Math.Sqrt(Conversion.Val(sitem["SYKQYD"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                    }
                    //温州
                    //第二次
                    if (Conversion.Val(sitem["MD"]) == Conversion.Val(sitem["BYMD"]) && Conversion.Val(sitem["SYKXL"]) == Conversion.Val(sitem["BYKXL"]))
                    {
                        if (Math.Abs(Conversion.Val(sitem["SYSWD2"]) - Conversion.Val(sitem["XZSWD"])) <= 3)
                            mitem["XD_2"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sitem["SYT2"])) / Math.Sqrt(Conversion.Val(sitem["BYT"])), 0).ToString();
                        else
                            mitem["XD_2"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sitem["SYT2"])) * Math.Sqrt(Conversion.Val(sitem["BYKQYD"])) / Math.Sqrt(Conversion.Val(sitem["BYT"])) / Math.Sqrt(Conversion.Val(sitem["SYKQYD2"])), 0).ToString();

                    }
                    if (Conversion.Val(sitem["MD"]) == Conversion.Val(sitem["BYMD"]) && Conversion.Val(sitem["SYKXL"]) != Conversion.Val(sitem["BYKXL"]))
                    {
                        if (Math.Abs(Conversion.Val(sitem["SYSWD2"]) - Conversion.Val(sitem["XZSWD"])) <= 3)
                            mitem["XD_2"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sitem["SYT2"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Math.Sqrt(Conversion.Val(sitem["BYT"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                        else
                            mitem["XD_2"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Math.Sqrt(Conversion.Val(sitem["SYT2"])) * Math.Sqrt(Conversion.Val(sitem["BYKQYD"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Math.Sqrt(Conversion.Val(sitem["BYT"])) * Math.Sqrt(Conversion.Val(sitem["SYKQYD2"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                    }
                    if (Conversion.Val(sitem["MD"]) != Conversion.Val(sitem["BYMD"]) && Conversion.Val(sitem["SYKXL"]) != Conversion.Val(sitem["BYKXL"]))
                    {
                        if (Math.Abs(Conversion.Val(sitem["SYSWD2"]) - Conversion.Val(sitem["XZSWD"])) <= 3)
                            mitem["XD_2"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Conversion.Val(sitem["BYMD"]) * Math.Sqrt(Conversion.Val(sitem["SYT2"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Conversion.Val(sitem["MD"]) * Math.Sqrt(Conversion.Val(sitem["BYT"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                        else
                            mitem["XD_2"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Conversion.Val(sitem["BYMD"]) * Math.Sqrt(Conversion.Val(sitem["SYT2"])) * Math.Sqrt(Conversion.Val(sitem["BYKQYD"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Conversion.Val(sitem["MD"]) * Math.Sqrt(Conversion.Val(sitem["BYT"])) * Math.Sqrt(Conversion.Val(sitem["SYKQYD2"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                    }

                    //温州

                    if (Conversion.Val(sitem["MD"]) != Conversion.Val(sitem["BYMD"]) && Conversion.Val(sitem["SYKXL"]) == Conversion.Val(sitem["BYKXL"]))
                    {
                        if (Math.Abs(Conversion.Val(sitem["SYSWD2"]) - Conversion.Val(sitem["XZSWD"])) <= 3)
                            mitem["XD_2"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Conversion.Val(sitem["BYMD"]) * Math.Sqrt(Conversion.Val(sitem["SYT2"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Conversion.Val(sitem["MD"]) * Math.Sqrt(Conversion.Val(sitem["BYT"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                        else
                            mitem["XD_2"] = Round(Conversion.Val(sitem["BYBBMJ"]) * Conversion.Val(sitem["BYMD"]) * Math.Sqrt(Conversion.Val(sitem["SYT2"])) * Math.Sqrt(Conversion.Val(sitem["BYKQYD"])) * (1 - Conversion.Val(sitem["BYKXL"])) * Math.Sqrt(Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"]) * Conversion.Val(sitem["SYKXL"])) / (Conversion.Val(sitem["MD"]) * Math.Sqrt(Conversion.Val(sitem["BYT"])) * Math.Sqrt(Conversion.Val(sitem["SYKQYD2"])) * (1 - Conversion.Val(sitem["SYKXL"])) * Math.Sqrt(Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]) * Conversion.Val(sitem["BYKXL"]))), 0).ToString();
                    }
                    //温州
                    mitem["XD_2"] = Round(Conversion.Val(mitem["XD_2"]), 0).ToString();
                    mitem["XD"] = Round((Conversion.Val(mitem["XD_1"]) + Conversion.Val(mitem["XD_2"])) / 2, 0).ToString();
                    if (Conversion.Val(mitem["XD_1"]) != 0 && Conversion.Val(mitem["XD_2"]) != 0)
                    {
                        if (Math.Abs(Conversion.Val(mitem["XD_1"]) - Conversion.Val(mitem["XD_2"])) / Conversion.Val(mitem["XD_1"]) > 0.02 || Math.Abs(Conversion.Val(mitem["XD_1"]) - Conversion.Val(mitem["XD_2"])) / Conversion.Val(mitem["XD_2"]) > 0.02)
                        {
                            mitem["XD"] = "无效";
                            mitem["XD_HG"] = "需重做";
                            xd_hg = false;
                        }
                        else
                        {
                            if (calc_pd(mitem["G_XD"], mitem["XD"]) == "符合")
                            {
                                mitem["XD_HG"] = "合格";
                                xd_hg = true;
                            }
                            else
                            {
                                mitem["XD_HG"] = "不合格";
                                xd_hg = false;
                            }
                        }
                    }
                }
                else
                {
                    mitem["XD"] = "----";
                    mitem["G_XD"] = "----";
                }
                if (jcxm.Contains("、80μm筛余、") || jcxm.Contains("、45μm筛余、"))
                {
                    mitem["XDSY1"] = Round(Conversion.Val(mitem["XDSHZL1"]) / Conversion.Val(mitem["XDSYZL1"]) * 100, 2).ToString("0.00");
                    mitem["XDSY2"] = Round(Conversion.Val(mitem["XDSHZL2"]) / Conversion.Val(mitem["XDSYZL2"]) * 100, 2).ToString("0.00");
                    if (Conversion.Val(mitem["XDSY1"]) <= 5 && Conversion.Val(mitem["XDSY2"]) <= 5)
                    {
                        if (Math.Abs(Conversion.Val(mitem["XDSY1"]) - Conversion.Val(mitem["XDSY1"])) <= 0.5)
                        {
                            mitem["XDPJSY"] = Round((Conversion.Val(mitem["XDSY1"]) + Conversion.Val(mitem["XDSY2"])) / 2, 1).ToString("0.0");
                            mitem["XDSY"] = Round(Conversion.Val(mitem["XDXZXS"]) * Conversion.Val(mitem["XDPJSY"]), 1).ToString("0.0");
                        }
                        else
                        {
                            mitem["XDPJSY"] = "无效";
                            mitem["XDSY"] = "无效";
                        }
                    }
                    else
                    {
                        if (Math.Abs(Conversion.Val(mitem["XDSY1"]) - Conversion.Val(mitem["XDSY1"])) <= 1)
                        {
                            mitem["XDPJSY"] = Round((Conversion.Val(mitem["XDSY1"]) + Conversion.Val(mitem["XDSY2"])) / 2, 1).ToString("0.0");
                            mitem["XDSY"] = Round(Conversion.Val(mitem["XDXZXS"]) * Conversion.Val(mitem["XDPJSY"]), 1).ToString("0.0");
                        }
                        else
                        {
                            mitem["XDPJSY"] = "无效";
                            mitem["XDSY"] = "无效";
                        }
                    }
                    if (jcxm.Contains("、80μm筛余、"))
                    {
                        mitem["XDSK"] = "80";
                        mitem["G_XD2"] = mrsDj_Filter["XDBZ2"];
                        if (calc_pd(mitem["G_XD2"], mitem["XDSY"]) == "符合")
                        {
                            mitem["XD_HG"] = "合格";
                            xd_hg = true;
                        }
                        else
                        {
                            mitem["XD_HG"] = "不合格";
                            xd_hg = false;
                        }
                    }
                    else
                    {
                        mitem["XDSK"] = "45";
                        mitem["G_XD2"] = mrsDj_Filter["XDBZ3"];
                        if (calc_pd(mitem["G_XD2"], mitem["XDSY"]) == "符合")
                        {
                            mitem["XD_HG"] = "合格";
                            xd_hg = true;
                        }
                        else
                        {
                            mitem["XD_HG"] = "不合格";
                            xd_hg = false;
                        }
                    }
                }
                else
                {
                    mitem["XDSK"] = "----";
                    mitem["XDSY"] = "----";
                    mitem["G_XD2"] = "----";
                }
                if (!jcxm.Contains("、80μm筛余、") && !jcxm.Contains("、45μm筛余、") && !jcxm.Contains("、比表面积、"))
                {
                    xd_hg = true;
                    mitem["XD_HG"] = "----";
                }
                //凝结时间合格判定

                if (!jcxm.Contains("、凝结时间、"))
                {
                    mitem["CN_HG"] = "----";
                    mitem["G_CNSJ"] = "----";
                    mitem["CNSJ"] = "----";
                    cn_hg = true;
                    mitem["NJSJ_HG"] = "----";
                }
                else
                {
                    mitem["CNSJ"] = (Conversion.Val(mitem["CNSJH"]) * 60 + Conversion.Val(mitem["CNSJM"]) - (Conversion.Val(mitem["JSSJH"]) * 60 + Conversion.Val(mitem["JSSJM"]))).ToString();
                    mitem["ZNSJ"] = (Conversion.Val(mitem["ZNSJH"]) * 60 + Conversion.Val(mitem["ZNSJM"]) - (Conversion.Val(mitem["JSSJH"]) * 60 + Conversion.Val(mitem["JSSJM"]))).ToString();
                    if (string.IsNullOrEmpty(mitem["CNSJ"]))
                        mitem["CNSJ"] = "0";
                    if (Conversion.Val(mitem["CNSJ"]) >= mCnsj)
                    {
                        mitem["CN_HG"] = "合格";
                        cn_hg = true;
                    }
                    else
                    {
                        mitem["CN_HG"] = "不合格";
                        cn_hg = false;
                    }

                    if (mitem["CNSJ"] == "0" || string.IsNullOrEmpty(mitem["CNSJ"]))
                    {
                        mitem["CN_HG"] = "----";
                        cn_hg = true;
                    }
                }
                if (!jcxm.Contains("、凝结时间、"))
                {
                    mitem["ZN_HG"] = "----";
                    mitem["G_ZNSJ"] = "0";
                    mitem["ZNSJ"] = "0";
                    zn_hg = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(mitem["ZNSJ"]))
                        mitem["ZNSJ"] = "0";
                    if (Conversion.Val(mitem["ZNSJ"]) <= mZnsj)
                    {
                        mitem["ZN_HG"] = "合格";
                        zn_hg = true;
                    }
                    else
                    {
                        mitem["ZN_HG"] = "不合格";
                        zn_hg = false;
                    }
                    if (mitem["ZNSJ"] == "0" || string.IsNullOrEmpty(mitem["ZNSJ"]))
                    {
                        mitem["ZN_HG"] = "----";
                        zn_hg = true;

                    }
                }

                if (Conversion.Val(mitem["CNSJ"]) >= Conversion.Val(mitem["ZNSJ"]))
                {
                    mitem["ZN_HG"] = "----";
                    mitem["CN_HG"] = "----";
                    //MsgBox "凝结时间初凝时间大于终凝时间，请从新输入"
                    zn_hg = true;
                    cn_hg = true;
                }
                if (mCnsj == 0 && mZnsj == 0 || mitem["CNSJ"] == "0" || mitem["ZNSJ"] == "0" || string.IsNullOrEmpty(mitem["CNSJ"]) || string.IsNullOrEmpty(mitem["ZNSJ"]))
                {
                    mitem["CN_HG"] = "----";
                    mitem["ZN_HG"] = "----";
                    cn_hg = true;
                    zn_hg = true;
                    mitem["NJSJ_HG"] = "----";
                }
                //杂项cd,xd,cn,zn综合判定
                if (mitem["CN_HG"] != "----" && mitem["ZN_HG"] != "----")
                {
                    if (cn_hg && zn_hg)
                        mitem["NJSJ_HG"] = "合格";
                    else
                        mitem["NJSJ_HG"] = "不合格";
                }
                else
                    mitem["NJSJ_HG"] = "----";
                if (cd_hg && xd_hg && cn_hg && zn_hg && adx_hg)
                    mitem["MISCJCJG"] = "合格";
                else
                {
                    mitem["MISCJCJG"] = "不合格";
                    mAllHg = false;
                }



                if (mitem["MISCJCJG"] == "合格" && ky3_hg && ky28_hg && kz3_hg && kz28_hg)
                    sitem["JCJG"] = "合格";
                else
                    sitem["JCJG"] = "不合格";
                if (sitem["KS_PDQD"] == "不合格")
                    sitem["JCJG"] = "不合格";
                mAllHg = (mAllHg && (sitem["JCJG"] == "合格"));

                if (cd_hg || xd_hg || cn_hg || zn_hg || adx_hg || ky3_hg || ky28_hg || kz3_hg || kz28_hg)
                    mFlag_Hg = true;
                if (!mAllHg)
                    mFlag_Bhg = true;
            }
            //主表总判断赋值
            if (mAllHg)
                mitem["JCJG"] = "合格";
            else
                mitem["JCJG"] = "不合格";
            mitem["JCJGMS"] = "";
            int mLQ = (GetSafeDateTime(mitem["SYRQ"]) - GetSafeDateTime(mitem["SYRQQ"])).Days;
            if (mitem["SYZT"] == "1")
            {
                if (mLQ == 3)
                {
                    if (mAllHg)
                        mitem["JCJGMS"] = "----";
                    else
                    {
                        mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + mSjdj.Trim() + "标准要求。";
                        if (mFlag_Bhg && mFlag_Hg)
                            mitem["JCJGMS"] = "该组试样所检项目部分符合" + mitem["PDBZ"] + mSjdj.Trim() + "标准要求。";
                    }
                }
                else
                {
                    if (mAllHg)
                        mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + mSjdj.Trim() + "标准要求。";
                    else
                    {
                        mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + mSjdj.Trim() + "标准要求。";
                        if (mFlag_Bhg && mFlag_Hg)
                            mitem["JCJGMS"] = "该组试样所检项目部分符合" + mitem["PDBZ"] + mSjdj.Trim() + "标准要求。";
                    }
                }
            }
            else
            {
                if (mAllHg)
                    mitem["JCJGMS"] = "28天强度检测前，该组试样所检项目符合标准要求。";
                else
                {
                    mitem["JCJGMS"] = "28天强度检测前，该组试样不符合标准要求。";
                    if (mFlag_Bhg && mFlag_Hg)
                        mitem["JCJGMS"] = "28天强度检测前，该组试样所检项目部分符合标准要求。";
                }
            }
            #endregion
        }
    }
}
