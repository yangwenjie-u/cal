using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class HSZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            /* 
            * 计算项目：蒸压灰砂砖
            * 参考标准：
            * GB11945-99
            * GB/T2542-1992
            */

            #region  参数定义
            string mcalBh, mlongStr;
            double[] mkyqdArray = new double[5];
            double[] mkyhzArray = new double[5];
            List<string> mtmpArray = new List<string>();
            double mMj1, mMj2, mMj3, mMj4, mMj5;
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd;
            double mS, mBzz, mPjz;
            string mSjdjbh, mSjdj;
            double mYqpjz, mDy21, mXdy21;
            int vp;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            string mSjddj, mDjMc;
            bool mSFwc;
            mSFwc = true;
            #endregion

            #region 自定义函数
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
                        bool min_bl, max_bl, sign;
                        min_sjz = -99999;
                        max_sjz = 99999;
                        scz = GetSafeDouble(sc_fun);
                        sign = false;
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
                            sign = true;
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
                            sign = true;
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
                            sign = true;
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
                            sign = true;
                        }
                        if (sj_fun.Contains("～"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("～") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0,dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_bl = true;
                            max_bl = true;
                            sign = true;
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
                            sign = true;
                        }
                        if (sj_fun == "0" && !string.IsNullOrEmpty(sj_fun))
                        {
                            sign = true;
                            min_bl = false;
                            max_bl = false;
                            max_sjz = 0;
                        }
                        if (!sign)
                        {
                            calc_PB_fun = "----";
                        }
                        else
                        {
                            string hgjl, bhgjl;
                            hgjl = flag_fun ? "符合" : "合格";
                            bhgjl = flag_fun ? "不符合" : "不合格";
                            sign = true; //做为判定了
                            if (min_bl)
                                sign = scz >= min_sjz ? sign : false;
                            else
                                sign = scz > min_sjz ? sign : false;
                            if (max_bl)
                                sign = scz <= max_sjz ? sign : false;
                            else
                                sign = scz < max_sjz ? sign : false;
                            calc_PB_fun = sign ? hgjl : bhgjl;
                        }
                    }
                    return calc_PB_fun;
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_HSZ_DJ"];
            var MItem = data["M_HSZ"];
            var SItem = data["S_HSZ"];
            #endregion

            #region 计算开始
            //从表循环
            mAllHg = true;
            foreach (var sitem in SItem)
            {
                //设计等级名称
                mSjdj = sitem["SJDJ"];
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    MItem[0]["G_PJZ"] = mrsDj_Filter["KYPJ"];
                    MItem[0]["G_MIN"] = mrsDj_Filter["KYMIN"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mYqpjz = 0;
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    continue;
                }
                //计算龄期 = 实验日期-
                sitem["LQ"] = (GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString();
                double sum;
                if (string.IsNullOrEmpty(mJSFF))
                {
                    if (sitem["JCXM"].Trim().Contains("抗压"))
                    {
                        bool sign = true;
                        for (int i = 1; i < 6; i++)
                        {
                            sign = IsNumeric(sitem["KYQD" + i]) && !string.IsNullOrEmpty(sitem["KYQD" + i]) ? sign : false;
                            if (!sign)
                                break;
                        }
                        sum = 0;
                        double[] nArr = new double[6];
                        for (int i = 1; i < 6; i++)
                        {
                            double md = GetSafeDouble(sitem["KYQD" + i].Trim());
                            nArr[i] = md;
                            sum = sum + md;
                        }
                        double pjmd = sum / 5;
                        pjmd = Round(pjmd, 1);
                        sitem["KYPJ"] = pjmd.ToString("0.0");
                        //数组排序
                        int min;
                        for (int i = 0; i < nArr.Length; i++)
                        {
                            min = i;
                            for (int j = i + 1; j < nArr.Length; j++)
                            {
                                if (nArr[j] < nArr[min])
                                    min = j;
                            }
                            double t = nArr[min];
                            nArr[min] = nArr[i];
                            nArr[i] = t;
                        }
                        sitem["DKZX"] = Round(nArr[1], 1).ToString("0.0");


                        sign = calc_PB(MItem[0]["G_PJZ"], sitem["KYPJ"], false) == "合格" ? sign : false;
                        sign = calc_PB(MItem[0]["G_MIN"], sitem["DKZX"], false) == "合格" ? sign : false;
                        sitem["QDPD"] = sign ? "合格" : "不合格";
                        sitem["QDYQ"] = "抗压强度平均值" + MItem[0]["G_PJZ"] + "MPa，抗压强度最小值" + MItem[0]["G_MIN"] + "MPa，";
                        mAllHg = sitem["QDPD"] == "不合格" ? false : mAllHg;
                    }
                }
            }
            //主表总判断赋值
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
            }

            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
