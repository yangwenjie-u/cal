using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


namespace Calculates
{
    public class TL : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;
            double[] mkyqdArray = new double[3];
            int mbhggs;
            int mbhggs2;
            string dCpmc, dLx, dZf, dDj, dBzh, mSjdj;
            int vp, mCnt_FjHg, mCnt_FjHg1, mxlgs, mxwgs;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            int QDJSFF;
            bool mSFwc;
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
            var mrsDj = dataExtra["BZ_TL_DJ"];
            var MItem = data["M_TL"];
            var mitem = MItem[0];
            var SItem = data["S_TL"];
            #endregion

            #region  计算开始
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            mitem["JCJGMS"] = "";
            mSjdj = "";
            foreach (var sitem in SItem)
            {
                dCpmc = string.IsNullOrEmpty(sitem["CPMC"]) ? "" : sitem["CPMC"].Trim();
                dLx = string.IsNullOrEmpty(sitem["LX"]) ? "" : sitem["LX"].Trim();
                dDj = string.IsNullOrEmpty(sitem["DJ"]) ? "" : sitem["DJ"].Trim();
                dZf = string.IsNullOrEmpty(sitem["ZF"]) ? "" : sitem["ZF"].Trim();
                dBzh = string.IsNullOrEmpty(sitem["BZH"]) ? "" : sitem["BZH"].Trim();
                if (dDj != "----")
                    mSjdj = mSjdj + dDj;
                if (dZf != "----")
                    mSjdj = mSjdj + dZf;
                if (dLx != "----")
                    mSjdj = mSjdj + dLx;
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_item = mrsDj.FirstOrDefault(x => x["MC"].Contains(dCpmc) && x["LX"].Contains(dLx) && x["DJ"].Contains(dDj) && x["ZF"].Contains(dZf) && x["BZH"].Contains(dBzh));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    mJSFF = string.IsNullOrEmpty(mrsDj_item["JSFF"]) ? "" : mrsDj_item["JSFF"].Trim().ToLower();
                    QDJSFF = string.IsNullOrEmpty(mrsDj_item["G_QDJSFF"]) ? 1 : GetSafeInt(mrsDj_item["G_QDJSFF"]);
                    sitem["CQS"] = string.IsNullOrEmpty(mrsDj_item["G_QDJSFF"]) ? "1" : mrsDj_item["G_QDJSFF"];
                    string which = mrsDj_item["WHICH"];
                    mitem["G_RQZZT"] = mrsDj_item["G_RQZZT"];
                    mitem["G_GTHL"] = mrsDj_item["G_GTHL"];
                    mitem["G_BGSJ"] = mrsDj_item["G_BGSJ"];
                    mitem["G_SGSJ"] = mrsDj_item["G_SGSJ"];
                    mitem["G_TMWG"] = mrsDj_item["G_TMWG"];
                    mitem["G_TCHD"] = mrsDj_item["G_TCHD"];
                    mitem["G_SGX"] = mrsDj_item["G_SGX"];
                    mitem["G_NJQD"] = mrsDj_item["G_NJQD"];
                    mitem["G_JNJQD"] = mrsDj_item["G_JNJQD"];
                    mitem["G_RSCL"] = mrsDj_item["G_RSCL"];
                    mitem["G_SCL"] = mrsDj_item["G_SCL"];
                    mitem["G_JRSSL"] = mrsDj_item["G_JRSSL"];
                    mitem["G_JRSSL2"] = mrsDj_item["G_JRSSL2"];
                    mitem["G_KYQD"] = mrsDj_item["G_KYQD"];
                    mitem["G_GMD"] = mrsDj_item["G_GMD"];
                    mitem["G_BTSX"] = mrsDj_item["G_BTSX"];
                    mitem["G_NSX"] = mrsDj_item["G_NSX"];
                    mitem["G_NJX"] = mrsDj_item["G_NJX"];
                    mitem["G_NXSX"] = mrsDj_item["G_NXSX"];
                    mitem["G_NWBX"] = mrsDj_item["G_NWBX"];
                    mitem["G_DWWDX"] = mrsDj_item["G_DWWDX"];
                    mitem["G_DWRD"] = mrsDj_item["G_DWRD"];
                    mitem["G_NRD"] = mrsDj_item["G_NRD"];
                    mitem["G_DWWZ"] = mrsDj_item["G_DWWZ"];
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJG,S"] = "找不到对应的等级";
                }
                if (dBzh == "JC/T 3049-1998")
                {
                    mitem["G_BGSJ"] = "＜" + mitem["G_BGSJ"];
                    mitem["G_NJQD"] = "＞" + mitem["G_NJQD"];
                }
                else
                {
                    mitem["G_BGSJ"] = "≤" + mitem["G_BGSJ"];
                    mitem["G_NJQD"] = "≥" + mitem["G_NJQD"];
                }
                mbhggs = 0;
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    bool sign;
                    int xd, Gs, Ws;
                    double md1, md2, md, sum;
                    double[] nArr;
                    mFlag_Hg = false;
                    mFlag_Bhg = false;
                    mbhggs = 0;
                    //以下初始化报告字段
                    var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                    if (jcxm.Contains("、干燥时间(表干)、") || jcxm.Contains("、表干时间、"))
                    {
                        sitem["HG_BGSJ"] = calc_PB(mitem["G_BGSJ"], sitem["BGSJ"], false);
                        mbhggs = sitem["HG_BGSJ"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_BGSJ"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["BGSJ"] = "----";
                        sitem["HG_BGSJ"] = "----";
                        mitem["G_BGSJ"] = "----";
                    }
                    if (jcxm.Contains("、干燥时间(实干)、") || jcxm.Contains("、实干时间、"))
                    {
                        mitem["G_SGSJ"] = "≤" + mitem["G_SGSJ"].Trim();
                        sitem["HG_SGSJ"] = calc_PB(mitem["G_SGSJ"], sitem["SGSJ"], false);
                        mbhggs = sitem["HG_SGSJ"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_SGSJ"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["SGSJ"] = "----";
                        sitem["HG_SGSJ"] = "----";
                        mitem["G_SGSJ"] = "----";
                    }
                    if (jcxm.Contains("、粘结强度(标准)、") || jcxm.Contains("、无处理拉伸强度、") || jcxm.Contains("、粘结强度、") || jcxm.Contains("、拉伸强度(无处理)、") || jcxm.Contains("、拉伸强度、"))
                    {
                        Gs = 0;
                        sum = 0;
                        Ws = string.IsNullOrEmpty(sitem["CQS"]) ? 1 : GetSafeInt(sitem["CQS"]);
                        for (xd = 1; xd <= 5; xd++)
                        {
                            sign = true;
                            sign = IsNumeric(sitem["NJQD_KD" + xd]) && !string.IsNullOrEmpty(sitem["NJQD_KD" + xd]) ? sign : false;
                            sign = IsNumeric(sitem["NJQD_HD" + xd]) && !string.IsNullOrEmpty(sitem["NJQD_HD" + xd]) ? sign : false;
                            sign = IsNumeric(sitem["NJQD_HZ" + xd]) && !string.IsNullOrEmpty(sitem["NJQD_HZ" + xd]) ? sign : false;
                            if (sign)
                            {
                                md1 = Conversion.Val(sitem["NJQD_KD" + xd].Trim());
                                md2 = Conversion.Val(sitem["NJQD_HD" + xd].Trim());
                                md = Conversion.Val(sitem["NJQD_HZ" + xd].Trim());
                                md = md / md1 / md2;
                                md = Round(md, Ws);

                                sitem["NJQD" + xd] = Ws == 1 ? md.ToString("0.0") : sitem["NJQD" + xd];
                                sitem["NJQD" + xd] = Ws == 2 ? md.ToString("0.00") : sitem["NJQD" + xd];
                                sitem["NJQD" + xd] = Ws == 3 ? md.ToString("0.000") : sitem["NJQD" + xd];
                                sum = sum + md;
                                Gs = Gs + 1;
                            }
                            else
                                sitem["NJQD" + xd] = "----";
                        }
                        if (sitem["CPMC"] == "聚氨酯防水涂料")
                        {
                            double pjmd = sum / Gs;
                            sum = 0;
                            for (xd = 1; xd <= 5; xd++)
                            {
                                md1 = Conversion.Val(sitem["NJQD" + xd].Trim());
                                if (Math.Abs(md1 - pjmd) <= pjmd * 0.15)
                                    sum = sum + md1;
                                else
                                    Gs = Gs - 1;
                            }
                        }
                        if (Gs >= 3)
                        {
                            double pjmd = Round((sum / Gs), Ws);
                            sitem["NJQD"] = Ws == 1 ? pjmd.ToString("0.0") : sitem["NJQD"];
                            sitem["NJQD"] = Ws == 2 ? pjmd.ToString("0.00") : sitem["NJQD"];
                            sitem["NJQD"] = Ws == 3 ? pjmd.ToString("0.000") : sitem["NJQD"];
                            sitem["HG_NJQD"] = calc_PB(mitem["G_NJQD"], sitem["NJQD"], false);
                            mbhggs = sitem["HG_NJQD"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sitem["HG_NJQD"] != "不合格")
                                mFlag_Hg = true;
                            else
                                mFlag_Bhg = true;
                        }
                        else if (Gs > 0 && Gs < 3)
                            sitem["NJQD"] = "重新试验";
                        else
                        {
                            sitem["HG_NJQD"] = "----";
                            sitem["NJQD"] = "----";
                        }
                    }
                    else
                    {
                        sitem["NJQD"] = "----";
                        sitem["HG_NJQD"] = "----";
                        mitem["G_NJQD"] = "----";
                    }
                    if (jcxm.Contains("、固体含量、"))
                    {
                        mitem["G_GTHL"] = "≥" + mitem["G_GTHL"].Trim();
                        sitem["HG_GTHL"] = calc_PB(mitem["G_GTHL"], sitem["GTHL"], false);
                        mbhggs = sitem["HG_GTHL"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_GTHL"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["GTHL"] = "----";
                        sitem["HG_GTHL"] = "----";
                        mitem["G_GTHL"] = "----";
                    }
                    if (jcxm.Contains("、断裂伸长率(无处理)、") || jcxm.Contains("、断裂伸长率、") || jcxm.Contains("、断裂延伸率、"))
                    {
                        mitem["G_SCL"] = "≥" + mitem["G_SCL"].Trim();
                        sitem["HG_SCL"] = calc_PB(mitem["G_SCL"], sitem["SCL"], false);
                        mbhggs = sitem["HG_SCL"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_SCL"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["SCL"] = "----";
                        sitem["HG_SCL"] = "----";
                        mitem["G_SCL"] = "----";
                    }
                    if (jcxm.Contains("、拉伸强度(热处理后)、"))
                    {
                        mitem["G_JNJQD"] = "≥" + mitem["G_JNJQD"].Trim();
                        sitem["HG_JNJQD"] = calc_PB(mitem["G_JNJQD"], sitem["JNJQD"], false);
                        mbhggs = sitem["HG_JNJQD"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_JNJQD"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["JNJQD"] = "----";
                        sitem["HG_JNJQD"] = "----";
                        mitem["G_JNJQD"] = "----";
                    }
                    if (jcxm.Contains("、断裂伸长率(热处理后)、"))
                    {
                        mitem["G_RSCL"] = "≥" + mitem["G_RSCL"].Trim();
                        sitem["HG_RSCL"] = calc_PB(mitem["G_RSCL"], sitem["RSCL"], false);
                        mbhggs = sitem["HG_RSCL"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_RSCL"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["RSCL"] = "----";
                        sitem["HG_RSCL"] = "----";
                        mitem["G_RSCL"] = "----";
                    }
                    if (jcxm.Contains("、低温弯折性、"))
                    {
                        if (sitem["HG_DWWZ"].Trim() != "符合" && sitem["HG_DWWZ"].Trim() != "合格")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["DWWZ"] = "----";
                        sitem["HG_DWWZ"] = "----";
                        mitem["G_DWWZ"] = "----";
                    }
                    if (jcxm.Contains("、低温柔度、") || jcxm.Contains("、低温柔性、"))
                    {
                        if (sitem["HG_DWRD"].Trim() != "符合" && sitem["HG_DWRD"].Trim() != "合格")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["DWRD"] = "----";
                        sitem["HG_DWRD"] = "----";
                        mitem["G_DWRD"] = "----";
                    }
                    if (jcxm.Contains("、耐热度、") || jcxm.Contains("、耐热性、"))
                    {
                        if (sitem["HG_NRD"].Trim() != "符合" && sitem["HG_NRD"].Trim() != "合格")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["NRD"] = "----";
                        sitem["HG_NRD"] = "----";
                        mitem["G_NRD"] = "----";
                    }
                    if (jcxm.Contains("、不透水性、"))
                    {
                        if (sitem["HG_BTSX"].Trim() != "符合" && sitem["HG_BTSX"].Trim() != "合格")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["BTSX"] = "----";
                        sitem["HG_BTSX"] = "----";
                        mitem["G_BTSX"] = "----";
                    }
                    if (mbhggs == 0)
                    {
                        mitem["JCJGMS"] = "该组试件所检项目符合" + mitem["PDBZ"] + "标准要求。";
                        sitem["JCJG"] = "合格";
                    }
                    if (mbhggs >= 1)
                    {
                        mitem["JCJGMS"] = "该组试件不符合" + mitem["PDBZ"] + "标准要求。";
                        sitem["JCJG"] = "不合格";
                        if (mFlag_Bhg && mFlag_Hg)
                            mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                    }
                    mAllHg = (mAllHg && sitem["JCJG"] == "合格");
                }
            }
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
                if (mFlag_Bhg && mFlag_Hg)
                    mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
