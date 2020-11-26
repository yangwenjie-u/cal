using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZX : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/

            #region  参数定义
            string mcalBh = string.Empty;
            string mlongStr = string.Empty;
            double[] mkyqdArray = new double[10];
            double[] mkyhzArray = new double[10];
            List<string> mtmpArray = new List<string>();
            double mfuncVal = 0;
            double mMj1, mMj2, mMj3, mMj4, mMj5, mMj6, mMj7, mMj8, mMj9, mMj10;
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd;
            double mS, mBzz, mPjz;
            string mSjdjbh, mSjdj;
            double mYqpjz, mDy21, mXdy21;
            int vp;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg = false;
            bool mGetBgbh = false;
            string mSjddj, mDjMc;
            bool mSFwc = true;
            bool mFlag_Hg, mFlag_Bhg;
            string which = "0";
            #endregion

            #region  自定义函数
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

            Func<IDictionary<string, string>, IDictionary<string, string>, bool, bool> sjtabcalc =
                delegate (IDictionary<string, string> mrsmainTable_filter, IDictionary<string, string> mrssubTable_filter, bool mAllHg_fun)
                {
                    int mbhggs_fun;
                    bool sign_fun;
                    int xd_fun, Gs_fun;
                    double md1_fun, md2_fun, md_fun, sum_fun;
                    List<decimal> nArr_fun = new List<decimal>();
                    bool mFlag_Hg_fun = false, mFlag_Bhg_fun = false;
                    mbhggs_fun = 0;
                    bool sjtabcalc_fun = true;
                    if (("、" + mrssubTable_filter["JCXM"] + "、").Contains("、密度、"))
                    {
                        sign_fun = true;
                        for (xd_fun = 1; xd_fun <= 5; xd_fun++)
                        {
                            if (IsNumeric(mrssubTable_filter["MD" + xd_fun]) && !string.IsNullOrEmpty(mrssubTable_filter["MD" + xd_fun]))
                                sign_fun = true;
                            else
                                sign_fun = false;
                            if (!sign_fun)
                                break;
                        }
                        if (!sign_fun)
                            sjtabcalc_fun = false;
                        sum_fun = 0;
                        for (xd_fun = 1; xd_fun <= 5; xd_fun++)
                        {
                            md_fun = GetSafeDouble(mrssubTable_filter["MD" + xd_fun].Trim());
                            sum_fun = sum_fun + md_fun;
                        }
                        double pjmd = sum_fun / 5;
                        pjmd = Math.Round(pjmd, 0);
                        mrssubTable_filter["MD"] = pjmd.ToString("0");
                        mrssubTable_filter["MDPD"] = calc_PB(mrssubTable_filter["MDYQ"], mrssubTable_filter["MD"], false);
                        mrssubTable_filter["MDYQ"] = "密度等级范围" + mrssubTable_filter["MDYQ"] + "kg/m&scsup3&scend。";
                        mbhggs_fun = mrssubTable_filter["MDPD"] == "不合格" ? mbhggs_fun + 1 : mbhggs_fun;
                        if (mrssubTable_filter["MDPD"] != "不合格")
                            mFlag_Hg_fun = true;
                        else
                            mFlag_Bhg_fun = true;
                    }
                    else
                    {
                        mrssubTable_filter["MDYQ"] = "----";
                        mrssubTable_filter["MDPD"] = "----";
                        for (int i = 1; i <= 5; i++)
                        {
                            mrssubTable_filter["MD" + i] = "----";
                        }
                        mrssubTable_filter["MD"] = "----";
                    }
                    if (("、" + mrssubTable_filter["JCXM"] + "、").Contains("、抗压、"))
                    {
                        sign_fun = true;
                        for (int i = 1; i <= 10; i++)
                        {
                            if (IsNumeric(mrssubTable_filter["KYQD" + i]) && !string.IsNullOrEmpty(mrssubTable_filter["KYQD" + i]))
                                sign_fun = true;
                            else
                                sign_fun = false;
                            if (!sign_fun)
                                break;
                        }
                        if (!sign_fun)
                            sjtabcalc_fun = false;
                        sum_fun = 0;
                        double[] nArr = new double[11];
                        for (int i = 1; i <= 10; i++)
                        {
                            md_fun = GetSafeDouble(mrssubTable_filter["KYQD" + i].Trim());
                            nArr[i] = md_fun;
                            sum_fun = sum_fun + md_fun;
                        }
                        double pjmd = sum_fun / 10;
                        pjmd = Math.Round(pjmd, 2);
                        mrssubTable_filter["KYPJ"] = pjmd.ToString("0.00");
                        sum_fun = 0;
                        for (int i = 1; i <= 10; i++)
                        {
                            md_fun = nArr[i] - pjmd;
                            sum_fun = sum_fun + Math.Pow((double)md_fun, 2);
                        }
                        md1_fun = Math.Sqrt(sum_fun / 9);
                        md1_fun = Round(md1_fun, 2);
                        mrssubTable_filter["BZC"] = md1_fun.ToString("0.00");


                        mrssubTable_filter["QDYQ"] = "抗压强度平均值需≥" + GetSafeDouble(mrsmainTable_filter["G_PJZ"]).ToString("0.0") +
                                        "MPa。当变异系数δ≤0.21时，强度标准值需≥" + GetSafeDouble(mrsmainTable_filter["G_BZZ"]).ToString("0.0") +
                                        "MPa，当变异系数δ＞0.21时，单块最小强度值需≥" + GetSafeDouble(mrsmainTable_filter["G_MIN"]).ToString("0.0") +
                                        "MPa。";


                        md2_fun = md1_fun / pjmd;
                        md2_fun = Round(md2_fun, 2);
                        mrssubTable_filter["BYXS"] = md2_fun.ToString("0.00");
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
                        mrssubTable_filter["QDMIN"] = Round(nArr[1], 1).ToString("0.0");
                        if (mrsmainTable_filter["PDBZ"].Trim() == "GB 13545-2014《烧结空心砖和空心砌块》")
                            md2_fun = pjmd - 1.83 * md1_fun;
                        else
                            md2_fun = pjmd - 1.8 * md1_fun;


                        md2_fun = Round(md2_fun, 1);
                        mrssubTable_filter["BZZ"] = md2_fun.ToString("0.0");
                        sign_fun = calc_PB("≥" + GetSafeDouble(mrsmainTable_filter["G_PJZ"]).ToString("0.0"), mrssubTable_filter["KYPJ"], false) == "合格" ? sign_fun : false;
                        if (sign_fun)
                        {
                            if (calc_PB("≤0.21", mrssubTable_filter["BYXS"], false) == "合格")
                                sign_fun = calc_PB("≥" + GetSafeDouble(mrsmainTable_filter["G_BZZ"]).ToString("0.0"), mrssubTable_filter["BZZ"], false) == "合格" ? sign_fun : false;
                            else
                                sign_fun = calc_PB("≥" + GetSafeDouble(mrsmainTable_filter["G_MIN"]).ToString("0.0"), mrssubTable_filter["QDMIN"], false) == "合格" ? sign_fun : false;

                        }

                        mrssubTable_filter["QDPD"] = sign_fun ? "合格" : "不合格";




                        mbhggs_fun = mrssubTable_filter["QDPD"] == "不合格" ? mbhggs_fun + 1 : mbhggs_fun;
                        if (sign_fun)
                            mFlag_Hg_fun = true;
                        else
                            mFlag_Bhg_fun = false;
                    }
                    else
                    {
                        mrssubTable_filter["KYPJ"] = "----";
                        mrssubTable_filter["QDPD"] = "----";
                        mrssubTable_filter["BZC"] = "----";
                        mrssubTable_filter["BYXS"] = "----";
                        mrssubTable_filter["QDMIN"] = "----";
                        mrssubTable_filter["BZZ"] = "----";
                        mrssubTable_filter["QDYQ"] = "----";
                        for (int i = 1; i <= 10; i++)
                        {
                            mrssubTable_filter["KYQD" + i] = "----";
                        }
                    }
                    if (("、" + mrssubTable_filter["JCXM"] + "、").Contains("、冻融、"))
                    {
                        sign_fun = true;
                        for (int i = 1; i < 6; i++)
                        {
                            sign_fun = mrssubTable_filter["DRWG" + i].Trim() == "否" ? sign_fun : false;
                        }
                        mrssubTable_filter["DRPD"] = sign_fun ? "合格" : "不合格";
                        mbhggs_fun = mrssubTable_filter["DRPD"] == "不合格" ? mbhggs_fun + 1 : mbhggs_fun;
                        if (sign_fun)
                            mFlag_Hg_fun = true;
                        else
                            mFlag_Bhg_fun = true;
                    }
                    else
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            mrssubTable_filter["DRWG" + i] = "----";
                        }
                        mrssubTable_filter["DRPD"] = "----";
                    }
                    mrssubTable_filter["FSPD"] = "----";
                    mrssubTable_filter["SHBLPD"] = "----";
                    mrssubTable_filter["BHXSPD"] = "----";
                    if (mbhggs_fun == 0)
                    {
                        mrsmainTable_filter["JCJGSM"] = "该组试件所检项目符合" + mrsmainTable_filter["PDBZ"] + "标准要求。";
                        mrssubTable_filter["JCJG"] = "合格";
                    }


                    if (mbhggs_fun >= 1)
                    {
                        mrsmainTable_filter["JCJGSM"] = "该组试件不符合" + mrsmainTable_filter["PDBZ"] + "标准要求。";
                        mrssubTable_filter["JCJG"] = "不合格";
                        if (mFlag_Bhg_fun && mFlag_Hg_fun)
                        {
                            mrsmainTable_filter["JCJGSM"] = "该组试件所检项目部分符合" + mrsmainTable_filter["PDBZ"] + "标准要求。";
                        }
                    }
                    mAllHg_fun = (mAllHg_fun && mrssubTable_filter["JCJG"] == "合格");
                    //赋值从表的报告编号

                    return mAllHg_fun;
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_ZX_DJ"];
            var mrswcDj = dataExtra["BZ_ZXWCDJ"];
            var mrskfhDj = dataExtra["BZ_ZXKFHDJ"];
            var mrsmdDj = dataExtra["BZ_ZXMDDJ"];
            var MItem = data["M_ZX"];
            var SItem = data["S_ZX"];
            #endregion

            #region  计算开始
            mGetBgbh = false;
            mAllHg = true;
            mSFwc = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";

            //循环从表数据
            foreach (var sitem in SItem)
            {
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                if (string.IsNullOrEmpty(sitem["SJDJ"]))
                    mSjdj = "";
                else
                    mSjdj = sitem["SJDJ"];      //设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["ZLB"].Contains(sitem["ZLB"]) && x["MC"].Contains(mSjdj));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    mYqpjz = GetSafeDouble(mrsDj_Filter["PJZ"]);
                    mXdy21 = GetSafeDouble(mrsDj_Filter["XDY21"]);
                    mDy21 = GetSafeDouble(mrsDj_Filter["DY21"]);
                    which = mrsDj_Filter["WHICH"].ToString();
                    MItem[0]["G_PJZ"] = mYqpjz.ToString();
                    MItem[0]["G_BZZ"] = mXdy21.ToString();
                    MItem[0]["G_MIN"] = mDy21.ToString();
                    sitem["MDYQ"] = mrsDj_Filter["ZJM"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mYqpjz = 0;
                    mXdy21 = 0;
                    mDy21 = 0;
                    mJSFF = "";
                    sitem["JCJG"] = "不下结论";
                    continue;
                }
                var mrskfhDj_Filter = mrskfhDj.FirstOrDefault(x => x["MC"].Contains(sitem["ZZL"].Trim()) && x["PZ"].Contains(sitem["WGDJ"]));
                if (mrskfhDj_Filter != null && mrskfhDj_Filter.Count > 0)
                {
                    sitem["XSLPJZYQ"] = mrskfhDj_Filter["XSLPJ"];
                    sitem["XSLZDZYQ"] = mrskfhDj_Filter["XSLDKZD"];
                    sitem["BHXSPJZYQ"] = mrskfhDj_Filter["BHXSPJ"];
                    sitem["BHXSZDZYQ"] = mrskfhDj_Filter["BHXSZDZ"];
                    sitem["FSYQ"] = mrskfhDj_Filter["FSYQ"];
                    sitem["SHBLYQ"] = mrskfhDj_Filter["DHBLYQ"];
                    sitem["DRYQ"] = mrskfhDj_Filter["DRYQ"];
                }
                if (MItem[0]["PDBZ"].Contains("13545-2014"))
                {
                    var mrsmdDj_Filter = mrsmdDj.FirstOrDefault(x => x["MC"].Contains(sitem["MDDJ"]));
                    if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                        sitem["MDYQ"] = mrsmdDj_Filter["MD"];
                }
                //计算龄期
                sitem["LQ"] = (GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString();
                if (!(string.IsNullOrEmpty(MItem[0]["SJTABS"]) || MItem[0]["SJTABS"].Trim() == ""))
                {
                    if (sjtabcalc(MItem[0], sitem, mAllHg))
                    { }
                    else
                        mSFwc = false;
                    continue;
                }
                //计算单组的抗压强度,并进行合格判断
                if (jcxm.Contains("、抗压、"))
                {
                    jcxmCur = "抗压强度";
                    if (GetSafeDouble(sitem["CD1_1"]) != 0)
                    {
                        sitem["CD1"] = Round((Conversion.Val(sitem["CD1_1"]) + Conversion.Val(sitem["CD1_2"])) / 2, 0).ToString();
                        sitem["CD2"] = Round((Conversion.Val(sitem["CD2_1"]) + Conversion.Val(sitem["CD2_2"])) / 2, 0).ToString();
                        sitem["CD3"] = Round((Conversion.Val(sitem["CD3_1"]) + Conversion.Val(sitem["CD3_2"])) / 2, 0).ToString();
                        sitem["CD4"] = Round((Conversion.Val(sitem["CD4_1"]) + Conversion.Val(sitem["CD4_2"])) / 2, 0).ToString();
                        sitem["CD5"] = Round((Conversion.Val(sitem["CD5_1"]) + Conversion.Val(sitem["CD5_2"])) / 2, 0).ToString();
                        sitem["CD6"] = Round((Conversion.Val(sitem["CD6_1"]) + Conversion.Val(sitem["CD6_2"])) / 2, 0).ToString();
                        sitem["CD7"] = Round((Conversion.Val(sitem["CD7_1"]) + Conversion.Val(sitem["CD7_2"])) / 2, 0).ToString();
                        sitem["CD8"] = Round((Conversion.Val(sitem["CD8_1"]) + Conversion.Val(sitem["CD8_2"])) / 2, 0).ToString();
                        sitem["CD9"] = Round((Conversion.Val(sitem["CD9_1"]) + Conversion.Val(sitem["CD9_2"])) / 2, 0).ToString();
                        sitem["CD10"] = Round((Conversion.Val(sitem["CD10_1"]) + Conversion.Val(sitem["CD10_2"])) / 2, 0).ToString();
                        sitem["KD1"] = Round((Conversion.Val(sitem["KD1_1"]) + Conversion.Val(sitem["KD1_2"])) / 2, 0).ToString();
                        sitem["KD2"] = Round((Conversion.Val(sitem["KD2_1"]) + Conversion.Val(sitem["KD2_2"])) / 2, 0).ToString();
                        sitem["KD3"] = Round((Conversion.Val(sitem["KD3_1"]) + Conversion.Val(sitem["KD3_2"])) / 2, 0).ToString();
                        sitem["KD4"] = Round((Conversion.Val(sitem["KD4_1"]) + Conversion.Val(sitem["KD4_2"])) / 2, 0).ToString();
                        sitem["KD5"] = Round((Conversion.Val(sitem["KD5_1"]) + Conversion.Val(sitem["KD5_2"])) / 2, 0).ToString();
                        sitem["KD6"] = Round((Conversion.Val(sitem["KD6_1"]) + Conversion.Val(sitem["KD6_2"])) / 2, 0).ToString();
                        sitem["KD7"] = Round((Conversion.Val(sitem["KD7_1"]) + Conversion.Val(sitem["KD7_2"])) / 2, 0).ToString();
                        sitem["KD8"] = Round((Conversion.Val(sitem["KD8_1"]) + Conversion.Val(sitem["KD8_2"])) / 2, 0).ToString();
                        sitem["KD9"] = Round((Conversion.Val(sitem["KD9_1"]) + Conversion.Val(sitem["KD9_2"])) / 2, 0).ToString();
                        sitem["KD10"] = Round((Conversion.Val(sitem["KD10_1"]) + Conversion.Val(sitem["KD10_2"])) / 2, 0).ToString();
                    }
                    else
                        mSFwc = false;
                    mMj1 = Conversion.Val(sitem["CD1"]) * Conversion.Val(sitem["KD1"]);
                    mMj2 = Conversion.Val(sitem["CD2"]) * Conversion.Val(sitem["KD2"]);
                    mMj3 = Conversion.Val(sitem["CD3"]) * Conversion.Val(sitem["KD3"]);
                    mMj4 = Conversion.Val(sitem["CD4"]) * Conversion.Val(sitem["KD4"]);
                    mMj5 = Conversion.Val(sitem["CD5"]) * Conversion.Val(sitem["KD5"]);
                    mMj6 = Conversion.Val(sitem["CD6"]) * Conversion.Val(sitem["KD6"]);
                    mMj7 = Conversion.Val(sitem["CD7"]) * Conversion.Val(sitem["KD7"]);
                    mMj8 = Conversion.Val(sitem["CD8"]) * Conversion.Val(sitem["KD8"]);
                    mMj9 = Conversion.Val(sitem["CD9"]) * Conversion.Val(sitem["KD9"]);
                    mMj10 = Conversion.Val(sitem["CD10"]) * Conversion.Val(sitem["KD10"]);

                    sitem["MJ1"] = mMj1.ToString();
                    sitem["MJ2"] = mMj2.ToString();
                    sitem["MJ3"] = mMj3.ToString();
                    sitem["MJ4"] = mMj4.ToString();
                    sitem["MJ5"] = mMj5.ToString();
                    sitem["MJ6"] = mMj6.ToString();
                    sitem["MJ7"] = mMj7.ToString();
                    sitem["MJ8"] = mMj8.ToString();
                    sitem["MJ9"] = mMj9.ToString();
                    sitem["MJ10"] = mMj10.ToString();

                    if (sitem["ZLB"].Contains("混凝土多孔砖") || sitem["ZLB"].Contains("烧结空心砖与空心砌块"))
                    {
                        if (mMj1 != 0)
                            sitem["KYQD1"] = Round(1000 * Conversion.Val(sitem["KYHZ1"]) / mMj1, 1).ToString("0.0");
                        else
                            sitem["KYQD1"] = "0";
                        if (mMj2 != 0)
                            sitem["KYQD2"] = Round(1000 * Conversion.Val(sitem["KYHZ2"]) / mMj2, 1).ToString("0.0");
                        else
                            sitem["KYQD2"] = "0";
                        if (mMj3 != 0)
                            sitem["KYQD3"] = Round(1000 * Conversion.Val(sitem["KYHZ3"]) / mMj3, 1).ToString("0.0");
                        else
                            sitem["KYQD3"] = "0";
                        if (mMj4 != 0)
                            sitem["KYQD4"] = Round(1000 * Conversion.Val(sitem["KYHZ4"]) / mMj4, 1).ToString("0.0");
                        else
                            sitem["KYQD4"] = "0";
                        if (mMj5 != 0)
                            sitem["KYQD5"] = Round(1000 * Conversion.Val(sitem["KYHZ5"]) / mMj5, 1).ToString("0.0");
                        else
                            sitem["KYQD5"] = "0";
                        if (mMj6 != 0)
                            sitem["KYQD6"] = Round(1000 * Conversion.Val(sitem["KYHZ6"]) / mMj6, 1).ToString("0.0");
                        else
                            sitem["KYQD6"] = "0";
                        if (mMj7 != 0)
                            sitem["KYQD7"] = Round(1000 * Conversion.Val(sitem["KYHZ7"]) / mMj7, 1).ToString("0.0");
                        else
                            sitem["KYQD7"] = "0";
                        if (mMj8 != 0)
                            sitem["KYQD8"] = Round(1000 * Conversion.Val(sitem["KYHZ8"]) / mMj8, 1).ToString("0.0");
                        else
                            sitem["KYQD8"] = "0";
                        if (mMj9 != 0)
                            sitem["KYQD9"] = Round(1000 * Conversion.Val(sitem["KYHZ9"]) / mMj9, 1).ToString("0.0");
                        else
                            sitem["KYQD9"] = "0";
                        if (mMj10 != 0)
                            sitem["KYQD10"] = Round(1000 * Conversion.Val(sitem["KYHZ10"]) / mMj10, 1).ToString("0.0");
                        else
                            sitem["KYQD10"] = "0";
                    }
                    else
                    {
                        if (mMj1 != 0)
                            sitem["KYQD1"] = Round(1000 * Conversion.Val(sitem["KYHZ1"]) / mMj1, 2).ToString("0.00");
                        else
                            sitem["KYQD1"] = "0";
                        if (mMj2 != 0)
                            sitem["KYQD2"] = Round(1000 * Conversion.Val(sitem["KYHZ2"]) / mMj2, 2).ToString("0.00");
                        else
                            sitem["KYQD2"] = "0";
                        if (mMj3 != 0)
                            sitem["KYQD3"] = Round(1000 * Conversion.Val(sitem["KYHZ3"]) / mMj3, 2).ToString("0.00");
                        else
                            sitem["KYQD3"] = "0";
                        if (mMj4 != 0)
                            sitem["KYQD4"] = Round(1000 * Conversion.Val(sitem["KYHZ4"]) / mMj4, 2).ToString("0.00");
                        else
                            sitem["KYQD4"] = "0";
                        if (mMj5 != 0)
                            sitem["KYQD5"] = Round(1000 * Conversion.Val(sitem["KYHZ5"]) / mMj5, 2).ToString("0.00");
                        else
                            sitem["KYQD5"] = "0";
                        if (mMj6 != 0)
                            sitem["KYQD6"] = Round(1000 * Conversion.Val(sitem["KYHZ6"]) / mMj6, 2).ToString("0.00");
                        else
                            sitem["KYQD6"] = "0";
                        if (mMj7 != 0)
                            sitem["KYQD7"] = Round(1000 * Conversion.Val(sitem["KYHZ7"]) / mMj7, 2).ToString("0.00");
                        else
                            sitem["KYQD7"] = "0";
                        if (mMj8 != 0)
                            sitem["KYQD8"] = Round(1000 * Conversion.Val(sitem["KYHZ8"]) / mMj8, 2).ToString("0.00");
                        else
                            sitem["KYQD8"] = "0";
                        if (mMj9 != 0)
                            sitem["KYQD9"] = Round(1000 * Conversion.Val(sitem["KYHZ9"]) / mMj9, 2).ToString("0.00");
                        else
                            sitem["KYQD9"] = "0";
                        if (mMj10 != 0)
                            sitem["KYQD10"] = Round(1000 * Conversion.Val(sitem["KYHZ10"]) / mMj10, 2).ToString("0.00");
                        else
                            sitem["KYQD10"] = "0";
                    }

                    //抗压平均值
                    mPjz = Round((Conversion.Val(sitem["KYQD1"]) + Conversion.Val(sitem["KYQD2"]) + Conversion.Val(sitem["KYQD3"]) + Conversion.Val(sitem["KYQD4"]) + Conversion.Val(sitem["KYQD5"]) +
                    Conversion.Val(sitem["KYQD6"]) + Conversion.Val(sitem["KYQD7"]) + Conversion.Val(sitem["KYQD8"]) + Conversion.Val(sitem["KYQD9"]) + Conversion.Val(sitem["KYQD10"])) / 10, 2);
                    //均方差
                    mS = Math.Sqrt(((Conversion.Val(sitem["KYQD1"]) - mPjz) * (Conversion.Val(sitem["KYQD1"]) - mPjz) +
                     (Conversion.Val(sitem["KYQD2"]) - mPjz) * (Conversion.Val(sitem["KYQD2"]) - mPjz) +
                     (Conversion.Val(sitem["KYQD3"]) - mPjz) * (Conversion.Val(sitem["KYQD3"]) - mPjz) +
                     (Conversion.Val(sitem["KYQD4"]) - mPjz) * (Conversion.Val(sitem["KYQD4"]) - mPjz) +
                     (Conversion.Val(sitem["KYQD5"]) - mPjz) * (Conversion.Val(sitem["KYQD5"]) - mPjz) +
                     (Conversion.Val(sitem["KYQD6"]) - mPjz) * (Conversion.Val(sitem["KYQD6"]) - mPjz) +
                     (Conversion.Val(sitem["KYQD7"]) - mPjz) * (Conversion.Val(sitem["KYQD7"]) - mPjz) +
                     (Conversion.Val(sitem["KYQD8"]) - mPjz) * (Conversion.Val(sitem["KYQD8"]) - mPjz) +
                     (Conversion.Val(sitem["KYQD9"]) - mPjz) * (Conversion.Val(sitem["KYQD9"]) - mPjz) +
                     (Conversion.Val(sitem["KYQD10"]) - mPjz) * (Conversion.Val(sitem["KYQD10"]) - mPjz)) / 9);
                    mS = Round(mS, 2);
                    sitem["BZC"] = Round(mS, 2).ToString("0.00");
                    sitem["KYPJ"] = Round(mPjz, 1).ToString("0.0");
                    sitem["QDYQ"] = "抗压强度平均值需≥" + GetSafeDouble(MItem[0]["G_PJZ"]).ToString("0.0").Trim() + "MPa。当变异系数δ≤0.21时，强度标准值需≥" + GetSafeDouble(MItem[0]["G_BZZ"]).ToString("0.0").Trim() + "MPa，当变异系数δ＞0.21时，单块最小强度值需≥" + GetSafeDouble(MItem[0]["G_MIN"]).ToString("0.0").Trim() + "MPa。";
                    //变异系数
                    if (mPjz != 0)
                        sitem["BYXS"] = Round(mS / mPjz, 2).ToString("0.00");
                    //标准值计算、判定，平均值判定，单组合格判定
                    mtmpArray.Add(sitem["KYQD1"]);
                    mtmpArray.Add(sitem["KYQD2"]);
                    mtmpArray.Add(sitem["KYQD3"]);
                    mtmpArray.Add(sitem["KYQD4"]);
                    mtmpArray.Add(sitem["KYQD5"]);
                    mtmpArray.Add(sitem["KYQD6"]);
                    mtmpArray.Add(sitem["KYQD7"]);
                    mtmpArray.Add(sitem["KYQD8"]);
                    mtmpArray.Add(sitem["KYQD9"]);
                    mtmpArray.Add(sitem["KYQD10"]);
                    for (int i = 0; i < mtmpArray.Count; i++)
                    {
                        mkyhzArray[i] = GetSafeDouble(mtmpArray[i]);
                    }
                    //数组排序
                    int min;
                    for (int i = 0; i < mkyhzArray.Length; i++)
                    {
                        min = i;
                        for (int j = i + 1; j < mkyhzArray.Length; j++)
                        {
                            if (mkyhzArray[j] < mkyhzArray[min])
                                min = j;
                        }
                        double t = mkyhzArray[min];
                        mkyhzArray[min] = mkyhzArray[i];
                        mkyhzArray[i] = t;
                    }
                    mMaxKyqd = mkyhzArray[9];
                    if (sitem["ZLB"].Contains("烧结普通砖") || sitem["ZLB"].Contains("混凝土普通砖"))
                        mMinKyqd = Round(mkyhzArray[0], 1);
                    else
                        mMinKyqd = Round(mkyhzArray[0], 1);
                    sitem["QDMIN"] = mMinKyqd.ToString("0.0");
                    mBzz = Round(mPjz - 1.8 * mS, 1);
                    sitem["BZZ"] = mBzz.ToString("0.0");

                    if (which == "1" || which == "11")
                    {
                        if (Conversion.Val(sitem["KYPJ"]) >= mYqpjz && mMinKyqd >= mDy21)
                        {
                            sitem["QDPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sitem["QDPD"] = "不合格";
                            mAllHg = false;
                            mFlag_Bhg = true;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        var mrsDj_Filter2 = mrsDj.Where(x => x["MC"].Contains(mrsDj_Filter["MC"].Substring(mrsDj_Filter["MC"].Length - 2)));
                        mSjddj = "";
                        foreach (var item in mrsDj_Filter2)
                        {
                            mDjMc = item["MC"];
                            mYqpjz = GetSafeDouble(item["PJZ"]);
                            mDy21 = GetSafeDouble(item["DY21"]);
                            if (Conversion.Val(sitem["KYPJ"]) >= mYqpjz && mMinKyqd >= mDy21)
                                mSjddj = mDjMc;
                        }
                        sitem["SJDDJ"] = mSjddj.Trim();
                    }
                    else
                    {
                        mSjddj = "";
                        if (Conversion.Val(sitem["BYXS"]) <= 0.21)
                        {
                            //一般合格判定
                            if (mBzz < mXdy21)
                                sitem["BZZ_HG"] = "0";
                            else
                                sitem["BZZ_HG"] = "1";
                            if (Conversion.Val(sitem["KYPJ"]) < mYqpjz)
                                sitem["PJZ_HG"] = "0";
                            else
                                sitem["PJZ_HG"] = "1";
                            if (Conversion.Val(sitem["KYPJ"]) >= mYqpjz && mBzz >= mXdy21)
                            {
                                sitem["QDPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["QDPD"] = "不合格";
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                            //报表TAG单元
                            sitem["TAG1"] = mPjz.ToString("0.0");
                            sitem["TAG2"] = "强度标准值";
                            if (sitem["BZZ_HG"] == "1")
                            {
                                sitem["TAG3"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sitem["TAG3"] = "不合格";
                                mFlag_Bhg = true;
                            }
                            sitem["TAG4"] = "标准值" + "s" + mBzz.ToString("0.0");
                            //报表TAG单元
                            sitem["TAG_1"] = mBzz.ToString("0.0");
                            sitem["TAG_2"] = mMinKyqd.ToString("0.0");
                            //实际达到设计等级判定
                            var mrsDj_filter3 = mrsDj.Where(x => x["ZLB"].Contains(sitem["ZLB"].Trim()));
                            foreach (var item in mrsDj_filter3)
                            {
                                mDjMc = item["MC"];
                                mYqpjz = GetSafeDouble(item["PJZ"]);
                                mXdy21 = GetSafeDouble(item["XDY21"]);
                                mDy21 = GetSafeDouble(item["DY21"]);
                                if (Conversion.Val(sitem["KYPJ"]) >= mYqpjz && mBzz >= mXdy21)
                                    mSjddj = mDjMc;
                            }
                            sitem["SJDDJ"] = mSjddj.Trim();
                        }
                        else
                        {
                            //一般合格判定
                            sitem["KYPJ"] = Round(mPjz, 1).ToString("0.0");
                            if (mMinKyqd < mDy21)
                                sitem["MIN_HG"] = "0";
                            else
                                sitem["MIN_HG"] = "1";
                            if (Conversion.Val(sitem["KYPJ"]) < mYqpjz)
                                sitem["PJZ_HG"] = "0";
                            else
                                sitem["PJZ_HG"] = "1";
                            if (Conversion.Val(sitem["KYPJ"]) >= mYqpjz && mMinKyqd >= mDy21)
                            {
                                sitem["QDPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["QDPD"] = "不合格";
                                mFlag_Bhg = true;
                            }
                            //报表TAG单元
                            if (sitem["MIN_HG"] == "0")
                                sitem["TAG1"] = "单块最小值不符合设计要求";
                            else
                                sitem["TAG1"] = mPjz.ToString("0.0");
                            sitem["TAG2"] = "单块最小值";
                            if (sitem["MIN_HG"] == "1")
                            {
                                sitem["TAG3"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sitem["TAG3"] = "不合格";

                                mFlag_Bhg = true;
                            }
                            sitem["TAG4"] = "";
                            //报表TAG单元
                            sitem["TAG_1"] = mBzz.ToString("0.0");
                            sitem["TAG_2"] = mMinKyqd.ToString("0.0");


                            //实际达到设计等级判定
                            var mrsDj_filter3 = mrsDj.Where(x => x["ZLB"].Contains(sitem["ZLB"].Trim()));
                            mSjddj = "";
                            foreach (var item in mrsDj_filter3)
                            {
                                mDjMc = item["MC"];
                                mYqpjz = GetSafeDouble(item["PJZ"]);
                                mXdy21 = GetSafeDouble(item["XDY21"]);
                                mDy21 = GetSafeDouble(item["DY21"]);
                                if (Conversion.Val(sitem["KYPJ"]) >= mYqpjz && mMinKyqd >= mDy21)
                                    mSjddj = mDjMc;
                            }
                            sitem["SJDDJ"] = mSjddj.Trim();
                        }
                    }
                }
                else
                    sitem["QDPD"] = "----";
                if (jcxm.Contains("、尺寸、"))
                {
                    jcxmCur = "尺寸";
                    if (GetSafeDouble(MItem[0]["CCJC"]) != 0)
                    {
                        MItem[0]["G_PJPC"] = mrswcDj[0]["PJPC"];
                        MItem[0]["G_CCJC"] = mrswcDj[0]["CCJC"];
                        if (Math.Abs(GetSafeDouble(MItem[0]["PJPC"])) <= GetSafeDouble(mrswcDj[0]["PJPC"]) && GetSafeDouble(MItem[0]["CCJC"]) <= GetSafeDouble(mrswcDj[0]["CCJC"]))
                        {
                            sitem["CCPD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["CCPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                        mSFwc = false;
                }
                else
                    sitem["CCPD"] = "----";
                if (jcxm.Contains("、外观、"))
                {
                    jcxmCur = "外观";
                    if (GetSafeDouble(MItem[0]["WGBHGS"]) <= GetSafeDouble(mrswcDj[0]["WYBHGS"]))
                    {
                        sitem["WGPD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        if (GetSafeDouble(MItem[0]["WGBHGS"]) >= GetSafeDouble(mrswcDj[0]["WBYBHGS"]))
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["WGPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            if (GetSafeDouble(MItem[0]["WGBHGS"]) + GetSafeDouble(MItem[0]["WGBHGS2"]) <= GetSafeDouble(mrswcDj[0]["WYBHGS2"]))
                            {
                                sitem["WGPD"] = "合格";
                                mFlag_Hg = true;
                            }
                            if (GetSafeDouble(MItem[0]["WGBHGS"]) + GetSafeDouble(MItem[0]["WGBHGS2"]) >= GetSafeDouble(mrswcDj[0]["WYBHGS2"]))
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sitem["WGPD"] = "不合格";
                                mFlag_Bhg = true;
                            }
                        }
                    }
                }
                else
                    sitem["WGPD"] = "----";
                if (jcxm.Contains("、冻融、"))
                {
                    jcxmCur = "冻融";
                    if (sitem["DRWG1"].Trim() == "否" || sitem["DRWG1"].Trim() == "是")
                    {
                        if (sitem["DRWG1"].Trim() == "是" || sitem["DRWG2"].Trim() == "是" || sitem["DRWG3"].Trim() == "是" || sitem["DRWG4"].Trim() == "是" || sitem["DRWG5"].Trim() == "是")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["DRPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            sitem["DRPD"] = "合格";
                            mFlag_Hg = true;
                        }
                    }
                    else
                        mSFwc = false;
                }
                else
                {
                    sitem["DRPD"] = "----";
                    sitem["DRWG1"] = "----";
                    sitem["DRWG2"] = "----";
                    sitem["DRWG3"] = "----";
                    sitem["DRWG4"] = "----";
                    sitem["DRWG5"] = "----";
                }
                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";
                    if (Conversion.Val(sitem["MDCD1_1"]) == 0)
                        mSFwc = false;
                    else
                    {
                        double mcd1 = Round((Conversion.Val(sitem["MDCD1_1"]) + Conversion.Val(sitem["MDCD1_2"])) / 2, 0);
                        double mcd2 = Round((Conversion.Val(sitem["MDCD2_1"]) + Conversion.Val(sitem["MDCD2_2"])) / 2, 0);
                        double mcd3 = Round((Conversion.Val(sitem["MDCD3_1"]) + Conversion.Val(sitem["MDCD3_2"])) / 2, 0);
                        double mcd4 = Round((Conversion.Val(sitem["MDCD4_1"]) + Conversion.Val(sitem["MDCD4_2"])) / 2, 0);
                        double mcd5 = Round((Conversion.Val(sitem["MDCD5_1"]) + Conversion.Val(sitem["MDCD5_2"])) / 2, 0);
                        double mkd1 = Round((Conversion.Val(sitem["MDKD1_1"]) + Conversion.Val(sitem["MDKD1_2"])) / 2, 0);
                        double mkd2 = Round((Conversion.Val(sitem["MDKD2_1"]) + Conversion.Val(sitem["MDKD2_2"])) / 2, 0);
                        double mkd3 = Round((Conversion.Val(sitem["MDKD3_1"]) + Conversion.Val(sitem["MDKD3_2"])) / 2, 0);
                        double mkd4 = Round((Conversion.Val(sitem["MDKD4_1"]) + Conversion.Val(sitem["MDKD4_2"])) / 2, 0);
                        double mkd5 = Round((Conversion.Val(sitem["MDKD5_1"]) + Conversion.Val(sitem["MDKD5_2"])) / 2, 0);
                        double mgd1 = Round((Conversion.Val(sitem["MDGD1_1"]) + Conversion.Val(sitem["MDGD1_2"])) / 2, 0);
                        double mgd2 = Round((Conversion.Val(sitem["MDGD2_1"]) + Conversion.Val(sitem["MDGD2_2"])) / 2, 0);
                        double mgd3 = Round((Conversion.Val(sitem["MDGD3_1"]) + Conversion.Val(sitem["MDGD3_2"])) / 2, 0);
                        double mgd4 = Round((Conversion.Val(sitem["MDGD4_1"]) + Conversion.Val(sitem["MDGD4_2"])) / 2, 0);
                        double mgd5 = Round((Conversion.Val(sitem["MDGD5_1"]) + Conversion.Val(sitem["MDGD5_2"])) / 2, 0);
                        sitem["MD1"] = Round(Conversion.Val(sitem["MDG0_1"]) / (mcd1 / 1000 * mkd1 / 1000 * mgd1 / 1000), 1).ToString("0.0");
                        sitem["MD2"] = Round(Conversion.Val(sitem["MDG0_2"]) / (mcd2 / 1000 * mkd2 / 1000 * mgd2 / 1000), 1).ToString("0.0");
                        sitem["MD3"] = Round(Conversion.Val(sitem["MDG0_3"]) / (mcd3 / 1000 * mkd3 / 1000 * mgd3 / 1000), 1).ToString("0.0");
                        sitem["MD4"] = Round(Conversion.Val(sitem["MDG0_4"]) / (mcd4 / 1000 * mkd4 / 1000 * mgd4 / 1000), 1).ToString("0.0");
                        sitem["MD5"] = Round(Conversion.Val(sitem["MDG0_5"]) / (mcd5 / 1000 * mkd5 / 1000 * mgd5 / 1000), 1).ToString("0.0");
                        sitem["MD"] = Round((Conversion.Val(sitem["MD1"]) + Conversion.Val(sitem["MD2"]) + Conversion.Val(sitem["MD3"]) + Conversion.Val(sitem["MD4"]) + Conversion.Val(sitem["MD5"])) / 5, 0).ToString();
                        if (calc_pd(sitem["MDYQ"], sitem["MD"]) == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["MDPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            sitem["MDPD"] = "合格";
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                    sitem["MDPD"] = "----";
                if (jcxm.Contains("、吸水率和饱和系数、"))
                {
                    jcxmCur = "吸水率和饱和系数";
                    sitem["xslbhxsyq"] = "吸水率平均值需" + sitem["XSLPJZYQ"].Trim() + "%；饱和系数平均值需" + sitem["BHXSPJZYQ"].Trim() + "%，单块最大值需" + sitem["BHXSZDZYQ"].Trim() + "%。";
                    double mxsl1 = Round((Conversion.Val(sitem["BHXSG5_1"]) - Conversion.Val(sitem["BHXSG0_1"])) / Conversion.Val(sitem["BHXSG0_1"]) * 100, 1);
                    double mxsl2 = Round((Conversion.Val(sitem["BHXSG5_2"]) - Conversion.Val(sitem["BHXSG0_2"])) / Conversion.Val(sitem["BHXSG0_2"]) * 100, 1);
                    double mxsl3 = Round((Conversion.Val(sitem["BHXSG5_3"]) - Conversion.Val(sitem["BHXSG0_3"])) / Conversion.Val(sitem["BHXSG0_3"]) * 100, 1);
                    double mxsl4 = Round((Conversion.Val(sitem["BHXSG5_4"]) - Conversion.Val(sitem["BHXSG0_4"])) / Conversion.Val(sitem["BHXSG0_4"]) * 100, 1);
                    double mxsl5 = Round((Conversion.Val(sitem["BHXSG5_5"]) - Conversion.Val(sitem["BHXSG0_5"])) / Conversion.Val(sitem["BHXSG0_5"]) * 100, 1);
                    sitem["XSLPJZ"] = Round((mxsl1 + mxsl2 + mxsl3 + mxsl4 + mxsl5) / 5, 0).ToString();
                    double mxslzdz = mxsl1;
                    if (mxslzdz < mxsl2)
                        mxslzdz = mxsl2;
                    if (mxslzdz < mxsl3)
                        mxslzdz = mxsl3;
                    if (mxslzdz < mxsl4)
                        mxslzdz = mxsl4;
                    if (mxslzdz < mxsl5)
                        mxslzdz = mxsl5;
                    sitem["XSLDKZD"] = mxslzdz.ToString("0.0");
                    mxsl1 = Round((Conversion.Val(sitem["BHXSG24_1"]) - Conversion.Val(sitem["BHXSG0_1"])) / (Conversion.Val(sitem["BHXSG5_1"]) - Conversion.Val(sitem["BHXSG0_1"])) * 100, 3);
                    mxsl2 = Round((Conversion.Val(sitem["BHXSG24_2"]) - Conversion.Val(sitem["BHXSG0_2"])) / (Conversion.Val(sitem["BHXSG5_2"]) - Conversion.Val(sitem["BHXSG0_2"])) * 100, 3);
                    mxsl3 = Round((Conversion.Val(sitem["BHXSG24_3"]) - Conversion.Val(sitem["BHXSG0_3"])) / (Conversion.Val(sitem["BHXSG5_3"]) - Conversion.Val(sitem["BHXSG0_3"])) * 100, 3);
                    mxsl4 = Round((Conversion.Val(sitem["BHXSG24_4"]) - Conversion.Val(sitem["BHXSG0_4"])) / (Conversion.Val(sitem["BHXSG5_4"]) - Conversion.Val(sitem["BHXSG0_4"])) * 100, 3);
                    mxsl5 = Round((Conversion.Val(sitem["BHXSG24_5"]) - Conversion.Val(sitem["BHXSG0_5"])) / (Conversion.Val(sitem["BHXSG5_5"]) - Conversion.Val(sitem["BHXSG0_5"])) * 100, 3);
                    sitem["BHXSPJZ"] = Round((mxsl1 + mxsl2 + mxsl3 + mxsl4 + mxsl5) / 5, 2).ToString("0.00");
                    mxslzdz = mxsl1;
                    if (mxslzdz < mxsl2)
                        mxslzdz = mxsl2;
                    if (mxslzdz < mxsl3)
                        mxslzdz = mxsl3;
                    if (mxslzdz < mxsl4)
                        mxslzdz = mxsl4;
                    if (mxslzdz < mxsl5)
                        mxslzdz = mxsl5;
                    sitem["BHXSZDZ"] = mxslzdz.ToString("0.000");
                    if (calc_pd(sitem["XSLPJZYQ"], sitem["XSLPJZ"]) == "不符合" || calc_pd(sitem["BHXSPJZYQ"], sitem["BHXSPJZ"]) == "不符合" || calc_pd(sitem["BHXSZDZYQ"], sitem["BHXSZDZ"]) == "不符合")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sitem["BHXSPD"] = "不合格";
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["BHXSPD"] = "合格";
                        mFlag_Hg = true;
                    }
                }
                else
                    sitem["BHXSPD"] = "----";
                if (jcxm.Contains("、泛霜、"))
                {
                    jcxmCur = "泛霜";
                    double mfscnt = 0;
                    double mfscnt1 = 0;
                    double mfscnt2 = 0;
                    if (sitem["FSCD1"].Trim() == "轻度")
                        mfscnt = mfscnt + 1;
                    if (sitem["FSCD2"].Trim() == "轻度")
                        mfscnt = mfscnt + 1;
                    if (sitem["FSCD3"].Trim() == "轻度")
                        mfscnt = mfscnt + 1;
                    if (sitem["FSCD4"].Trim() == "轻度")
                        mfscnt = mfscnt + 1;
                    if (sitem["FSCD5"].Trim() == "轻度")
                        mfscnt = mfscnt + 1;
                    if (sitem["FSCD1"].Trim() == "中等")
                        mfscnt1 = mfscnt1 + 1;
                    if (sitem["FSCD2"].Trim() == "中等")
                        mfscnt1 = mfscnt1 + 1;
                    if (sitem["FSCD3"].Trim() == "中等")
                        mfscnt1 = mfscnt1 + 1;
                    if (sitem["FSCD4"].Trim() == "中等")
                        mfscnt1 = mfscnt1 + 1;
                    if (sitem["FSCD5"].Trim() == "中等")
                        mfscnt1 = mfscnt1 + 1;
                    if (sitem["FSCD1"].Trim() == "严重")
                        mfscnt2 = mfscnt2 + 1;
                    if (sitem["FSCD2"].Trim() == "严重")
                        mfscnt2 = mfscnt2 + 1;
                    if (sitem["FSCD3"].Trim() == "严重")
                        mfscnt2 = mfscnt2 + 1;
                    if (sitem["FSCD4"].Trim() == "严重")
                        mfscnt2 = mfscnt2 + 1;
                    if (sitem["FSCD5"].Trim() == "严重")
                        mfscnt2 = mfscnt2 + 1;
                    if (sitem["WGDJ"].Trim() == "合格品")
                    {
                        if (mfscnt2 > 0)
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["FSPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            sitem["FSPD"] = "合格";
                            mFlag_Hg = true;
                        }
                    }

                    if (sitem["WGDJ"].Trim() == "一等品")
                    {
                        if (mfscnt1 > 0 || mfscnt2 > 0)
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["FSPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            sitem["FSPD"] = "合格";
                            mFlag_Hg = true;
                        }
                    }
                    if (sitem["WGDJ"].Trim() == "优等品")
                    {
                        if (mfscnt > 0 || mfscnt1 > 0 || mfscnt2 > 0)
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["FSPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            sitem["FSPD"] = "合格";
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                    sitem["FSPD"] = "----";
                if (jcxm.Contains("、石灰爆裂、"))
                {
                    jcxmCur = "石灰爆裂";
                    double mbhgs = 0;
                    if (sitem["WGDJ"].Trim() == "合格品")
                    {
                        if (Conversion.Val(sitem["BLDS10_1"]) + Conversion.Val(sitem["BLDS15_1"]) <= 15 && Conversion.Val(sitem["BLDS15_1"]) <= 7 && Conversion.Val(sitem["BLDS16_1"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_2"]) + Conversion.Val(sitem["BLDS15_2"]) <= 15 && Conversion.Val(sitem["BLDS15_2"]) <= 7 && Conversion.Val(sitem["BLDS16_2"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_3"] + Conversion.Val(sitem["BLDS15_3"])) <= 15 && Conversion.Val(sitem["BLDS15_3"]) <= 7 && Conversion.Val(sitem["BLDS16_3"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_4"]) + Conversion.Val(sitem["BLDS15_4"]) <= 15 && Conversion.Val(sitem["BLDS15_4"]) <= 7 && Conversion.Val(sitem["BLDS16_4"]) == 0)
                        { }

                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_5"]) + Conversion.Val(sitem["BLDS15_5"]) <= 15 && Conversion.Val(sitem["BLDS15_5"]) <= 7 && Conversion.Val(sitem["BLDS16_5"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (mbhgs > 0)
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["SHBLPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            sitem["SHBLPD"] = "合格";
                            mFlag_Hg = true;
                        }
                    }


                    if (sitem["WGDJ"].Trim() == "一等品")
                    {
                        mbhgs = 0;
                        if (Conversion.Val(sitem["BLDS10_1"]) <= 15 && Conversion.Val(sitem["BLDS15_1"]) + Conversion.Val(sitem["BLDS16_1"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_2"]) <= 15 && Conversion.Val(sitem["BLDS15_2"]) + Conversion.Val(sitem["BLDS16_2"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_3"]) <= 15 && Conversion.Val(sitem["BLDS15_3"]) + Conversion.Val(sitem["BLDS16_3"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_4"]) <= 15 && Conversion.Val(sitem["BLDS15_4"]) + Conversion.Val(sitem["BLDS16_4"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_5"]) <= 15 && Conversion.Val(sitem["BLDS15_5"]) + Conversion.Val(sitem["BLDS16_5"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (mbhgs > 0)
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["SHBLPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            sitem["SHBLPD"] = "合格";
                            mFlag_Hg = true;
                        }
                    }


                    if (sitem["WGDJ"].Trim() == "优等品")
                    {
                        mbhgs = 0;
                        if (Conversion.Val(sitem["BLDS10_1"]) + Conversion.Val(sitem["BLDS15_1"]) + Conversion.Val(sitem["BLDS16_1"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_2"]) + Conversion.Val(sitem["BLDS15_2"]) + Conversion.Val(sitem["BLDS16_2"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_3"]) + Conversion.Val(sitem["BLDS15_3"]) + Conversion.Val(sitem["BLDS16_3"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_4"]) + Conversion.Val(sitem["BLDS15_4"]) + Conversion.Val(sitem["BLDS16_4"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (Conversion.Val(sitem["BLDS10_5"]) + Conversion.Val(sitem["BLDS15_5"]) + Conversion.Val(sitem["BLDS16_5"]) == 0)
                        { }
                        else
                            mbhgs = mbhgs + 1;
                        if (mbhgs > 0)
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sitem["SHBLPD"] = "不合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            sitem["SHBLPD"] = "合格";
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sitem["SHBLPD"] = "----";
                    sitem["SHBLYQ"] = "----";
                }


                if (sitem["MDPD"] == "不合格" || sitem["QDPD"] == "不合格" || sitem["WGPD"] == "不合格" || sitem["CCPD"] == "不合格" || sitem["DRPD"] == "不合格" || sitem["BHXSPD"] == "不合格" || sitem["FSPD"] == "不合格" || sitem["shblpd"] == "不合格")
                {
                    mAllHg = false;
                    sitem["JCJG"] = "不合格";
                }
                else
                    sitem["JCJG"] = "合格";

            }

            //-----------综合判断------------
            //主表总判断赋值
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGSM"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGSM"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //if (mFlag_Bhg && mFlag_Hg)
                //    MItem[0]["JCJGSM"] = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            #endregion

            /************************ 代码结束 *********************/
        }

        public void GxJCJGMS()
        {
            //富阳德浩
            #region
            var extraDJ = dataExtra["BZ_ZX_DJ"];
            var mrswcDj = dataExtra["BZ_ZXWCDJ"];
            var mrskfhDj = dataExtra["BZ_ZXKFHDJ"];
            var mrsmdDj = dataExtra["BZ_ZXMDDJ"];

            var data = retData;
            var jsbeizhu = "该组试样的检测结果全部合格";
            var SItems = data["S_ZX"];
            var MItem = data["M_ZX"];

            var mAllHg = true;
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            string sjdj = "";

            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                sjdj = sItem["SJDJ"];

                #region 抗压强度
                if (jcxm.Contains("、抗压、"))
                {
                    jcxmCur = "抗压强度";
                    if (sItem["QDPD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                #endregion

            }
            if (MItem[0]["JCJG"] == "合格")
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合" + sjdj + "强度等级要求。";
                MItem[0]["JCJGSM"] = MItem[0]["JCJGMS"];
            }
            else
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + sjdj + "强度等级要求。";
                MItem[0]["JCJGSM"] = MItem[0]["JCJGMS"];
            }
            #endregion
        }
    }
}
