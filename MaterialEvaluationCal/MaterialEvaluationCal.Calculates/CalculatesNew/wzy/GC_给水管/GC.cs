using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class GC : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mFlag_Hg, mFlag_Bhg;
            //当前项目的变量声明
            int mbhggs, mbhggs1, mhgpds, mbhgpds;
            string mGxl, mSjdj;
            bool mSFwc;
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
            var mrsDj = dataExtra["BZ_GC_DJ"];
            var mrscyfa = dataExtra["BZ_GCCYFA"];
            var mrslccj = dataExtra["BZ_GCLCCJ"];
            var mrsYysycs = dataExtra["BZ_GCYYSYCS"];
            var MItem = data["M_GC"];
            var mitem = MItem[0];
            var SItem = data["S_GC"];
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            mitem["JCJGMS"] = "";
            mFlag_Hg = false;
            mFlag_Bhg = false;
            foreach (var mrscyfa_item in mrscyfa)
            {
                var sitem = SItem[0];
                if (GetSafeDouble(sitem["DBSL"]) >= GetSafeDouble(mrscyfa_item["PFW1"]) && GetSafeDouble(sitem["DBSL"]) <= GetSafeDouble(mrscyfa_item["PFW2"]))
                {
                    mhgpds = GetSafeInt(mrscyfa_item["HGPDS"]);
                    mbhgpds = GetSafeInt(mrscyfa_item["BHGPDS"]);
                    break;
                }
            }
            foreach (var sitem in SItem)
            {
                mSjdj = sitem["SJDJ"]; //管材名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                mGxl = sitem["GXL"]; //环刚度(管系列)
                if (string.IsNullOrEmpty(mGxl))
                    mGxl = "";
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj) && x["HGDDH"].Contains(mGxl));
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    mitem["G_WG"] = string.IsNullOrEmpty(mrsDj_Filter["WG"]) ? "" : mrsDj_Filter["WG"].Trim();
                    mitem["G_BZ"] = string.IsNullOrEmpty(mrsDj_Filter["BZ"]) ? "" : mrsDj_Filter["BZ"].Trim();
                    mitem["G_DLSCL"] = string.IsNullOrEmpty(mrsDj_Filter["DLSCL"]) ? "0" : mrsDj_Filter["DLSCL"].Trim();
                    mitem["G_PJWJ"] = string.IsNullOrEmpty(mrsDj_Filter["PJWJ"]) ? "0" : mrsDj_Filter["PJWJ"].Trim();
                    mitem["G_PJWJPC"] = string.IsNullOrEmpty(mrsDj_Filter["PJWJPC"]) ? "0" : mrsDj_Filter["PJWJPC"].Trim();
                    mitem["G_BH"] = string.IsNullOrEmpty(mrsDj_Filter["LCBH"]) ? "0" : mrsDj_Filter["LCBH"].Trim();
                    mitem["G_BHPC"] = string.IsNullOrEmpty(mrsDj_Filter["BHPC"]) ? "0" : mrsDj_Filter["BHPC"].Trim();
                    mitem["G_BHPCL"] = string.IsNullOrEmpty(mrsDj_Filter["BHPCL"]) ? "0" : mrsDj_Filter["BHPCL"].Trim();
                    mitem["G_JXLL1"] = string.IsNullOrEmpty(mrsDj_Filter["JXLL1"]) ? "0" : mrsDj_Filter["JXLL1"].Trim();
                    mitem["G_JXLL2"] = string.IsNullOrEmpty(mrsDj_Filter["JXLL2"]) ? "0" : mrsDj_Filter["JXLL2"].Trim();
                    mitem["G_NHQD"] = string.IsNullOrEmpty(mrsDj_Filter["NHQD"]) ? "" : mrsDj_Filter["NHQD"].Trim();
                    mitem["G_JNYQD"] = string.IsNullOrEmpty(mrsDj_Filter["JNYQD"]) ? "" : mrsDj_Filter["JNYQD"].Trim();
                    mitem["G_YLSY"] = string.IsNullOrEmpty(mrsDj_Filter["YLSY"]) ? "" : mrsDj_Filter["YLSY"].Trim();
                    mitem["G_BPQD"] = string.IsNullOrEmpty(mrsDj_Filter["BPQD"]) ? "0" : mrsDj_Filter["BPQD"].Trim();
                    mitem["G_NJYXZX"] = string.IsNullOrEmpty(mrsDj_Filter["NJYXZX"]) ? "0" : mrsDj_Filter["NJYXZX"].Trim();
                    mitem["G_WJYXZX"] = string.IsNullOrEmpty(mrsDj_Filter["WJYXZX"]) ? "0" : mrsDj_Filter["WJYXZX"].Trim();
                    mitem["G_LCZXHD"] = string.IsNullOrEmpty(mrsDj_Filter["LCZXHD"]) ? "0" : mrsDj_Filter["LCZXHD"].Trim();
                    mitem["G_MD"] = string.IsNullOrEmpty(mrsDj_Filter["MD"]) ? "" : mrsDj_Filter["MD"].Trim();
                    mitem["G_MFSY"] = string.IsNullOrEmpty(mrsDj_Filter["MFSY"]) ? "" : mrsDj_Filter["MFSY"].Trim();
                    mitem["G_BTGX"] = string.IsNullOrEmpty(mrsDj_Filter["BTGX"]) ? "" : mrsDj_Filter["BTGX"].Trim();
                    //以下赤峰开展项目
                    mitem["G_BPSY"] = string.IsNullOrEmpty(mrsDj_Filter["BPSY"]) ? "" : mrsDj_Filter["BPSY"].Trim(); //扁平试验
                    mitem["G_RHWD"] = string.IsNullOrEmpty(mrsDj_Filter["RHWD"]) ? "0" : mrsDj_Filter["RHWD"].Trim(); //维卡软化温度
                    mitem["G_ZXHSL"] = string.IsNullOrEmpty(mrsDj_Filter["ZXHSL"]) ? "0" : mrsDj_Filter["ZXHSL"].Trim(); //纵向回缩率
                    mitem["G_LCCJ"] = string.IsNullOrEmpty(mrsDj_Filter["LCCJ"]) ? "0" : mrsDj_Filter["LCCJ"].Trim(); //落锤冲击试验
                    mitem["G_QFQD"] = string.IsNullOrEmpty(mrsDj_Filter["QFQD"]) ? "0" : mrsDj_Filter["QFQD"].Trim(); //拉伸屈服强度
                    mitem["G_YYSY1"] = string.IsNullOrEmpty(mrsDj_Filter["YYSY"]) ? "" : mrsDj_Filter["YYSY"].Trim(); //静液压试验
                    mitem["G_YYSY2"] = string.IsNullOrEmpty(mrsDj_Filter["YYSY"]) ? "" : mrsDj_Filter["YYSY"].Trim(); //静液压试验
                    mitem["G_YYSY3"] = string.IsNullOrEmpty(mrsDj_Filter["YYSY"]) ? "" : mrsDj_Filter["YYSY"].Trim(); //静液压试验
                    mitem["G_YYSY4"] = string.IsNullOrEmpty(mrsDj_Filter["YYSY"]) ? "" : mrsDj_Filter["YYSY"].Trim(); //静液压试验
                    mitem["G_ZLSY"] = string.IsNullOrEmpty(mrsDj_Filter["ZLSY"]) ? "" : mrsDj_Filter["ZLSY"].Trim(); //坠落试验
                    mitem["G_JWJZ"] = string.IsNullOrEmpty(mrsDj_Filter["JWJZ"]) ? "" : mrsDj_Filter["JWJZ"].Trim(); //甲烷浸渍
                    mitem["G_HXSY"] = string.IsNullOrEmpty(mrsDj_Filter["HXSY"]) ? "" : mrsDj_Filter["HXSY"].Trim(); //烘箱试验
                    mitem["G_HGD"] = string.IsNullOrEmpty(mrsDj_Filter["HGD"]) ? "" : mrsDj_Filter["HGD"].Trim(); //环刚度
                    mitem["G_HRX"] = string.IsNullOrEmpty(mrsDj_Filter["HRX"]) ? "" : mrsDj_Filter["HRX"].Trim(); //环柔度
                    mitem["G_JZLCJ"] = string.IsNullOrEmpty(mrsDj_Filter["JZLCJ"]) ? "" : mrsDj_Filter["JZLCJ"].Trim(); //简支梁冲击
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "获取标准要求出错，找不到对应项";
                    continue;
                }
                mbhggs = 0;
                mbhggs1 = 0;
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    bool sign;
                    int xd, Gs;
                    int md;
                    double md1, md2, sum;
                    double[] nArr;
                    mbhggs = 0;
                    string[] mtmpArray;
                    mtmpArray = sitem["JCXM"].Split('、');
                    int jcxmCount; //启用的检查项目个数
                    int curJcxmCount; //现在处理的是第几个检测项目
                    jcxmCount = mtmpArray.Length;
                    curJcxmCount = 0;
                    //以下初始化报告字段
                    for (xd = 0; xd <= 9; xd++)
                    {
                        sitem["BGJCXM" + xd] = "";
                        sitem["BGDW" + xd] = "";
                        sitem["BGBZYQ" + xd] = "";
                        sitem["BGSCJG" + xd] = "";
                        sitem["BGDXPD" + xd] = "";
                    }
                    if (sitem["JCXM"].Trim().Contains("落锤冲击"))
                    {
                        if (mitem["G_LCCJ"].Contains("≤10"))
                        {
                            IDictionary<string, string> mrslccj_Sel = new Dictionary<string, string>();
                            foreach (var mrslccj_item in mrslccj)
                            {
                                if (GetSafeDouble(mrslccj_item["LCCJCS"]) == GetSafeDouble(mitem["LCCJCS"]))
                                    break;
                                mrslccj_Sel = mrslccj_item;
                            }

                            if (mrslccj_Sel == mrslccj.First() || mrslccj_Sel == mrslccj.Last())
                            {
                                md1 = Conversion.Val(mitem["LCCJBHGS"].Trim());
                                md2 = Conversion.Val(mitem["LCCJCS"].Trim());
                                md = (int)(100 * md1 / md2);
                                mitem["LCCJ"] = Round(md, 0).ToString("0.0");
                                mitem["LCCJ_HG"] = calc_PB(mitem["G_LCCJ"], mitem["LCCJ"], false);
                                mbhggs = mitem["LCCJ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                                if (mitem["LCCJ_HG"] == "不合格")
                                    mFlag_Hg = true;
                                else
                                    mFlag_Bhg = true;
                                mitem["LCCJ"] = mitem["LCCJ"] + "%";
                            }
                            else
                            {
                                if (Conversion.Val(mitem["LCCJBHGS"]) <= Conversion.Val(mrslccj_Sel["AQPHCS"]))
                                {
                                    mitem["LCCJ_HG"] = "合格";
                                    mitem["LCCJ"] = "≤10%";
                                    mFlag_Hg = true;
                                }
                                if (Conversion.Val(mitem["LCCJBHGS"]) >= Conversion.Val(mrslccj_Sel["BQPHCS1"]) && Conversion.Val(mitem["LCCJBHGS"]) <= Conversion.Val(mrslccj_Sel["BQPHCS2"]))
                                {
                                    mitem["LCCJ_HG"] = "不判定";
                                    mitem["LCCJ"] = "根据现有冲击试样数不能作出判定";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                }
                                if (Conversion.Val(mitem["LCCJBHGS"]) >= Conversion.Val(mrslccj_Sel["CQPHCS"]))
                                {
                                    mitem["LCCJ_HG"] = "不合格";
                                    mitem["LCCJ"] = "＞10%";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                }
                            }
                        }
                        else
                        {
                            if (mitem["G_LCCJ"].Contains("≤"))
                            {
                                mitem["LCCJ"] = Round(100 * Conversion.Val(mitem["LCCJBHGS"]) / Conversion.Val(mitem["LCCJCS"]), 0).ToString("0.0");
                                mitem["LCCJ_HG"] = calc_PB(mitem["G_LCCJ"], mitem["LCCJ"], false);
                                mbhggs = mitem["LCCJ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                                if (mitem["LCCJ_HG"] != "不合格")
                                    mFlag_Hg = true;
                                else
                                    mFlag_Bhg = true;
                                mitem["LCCJ"] = mitem["LCCJ"] + "%";
                            }
                            else
                            {
                                md1 = Conversion.Val(mitem["LCCJCS"].Trim());
                                md2 = Conversion.Val(mitem["LCCJBHGS"].Trim());
                                md1 = Round(md1, 0);
                                md2 = Round(md2, 0);
                                if (mitem["G_LCCJ"].Contains("12次冲击，12次不破裂"))
                                {
                                    mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                    if (md2 == 0)
                                    {
                                        mitem["LCCJ_HG"] = "合格";
                                        mFlag_Hg = true;
                                    }
                                    else
                                    {
                                        mitem["LCCJ_HG"] = "不合格";
                                        mbhggs = mbhggs + 1;
                                        mFlag_Bhg = true;
                                    }
                                }
                                if (mitem["G_LCCJ"].Contains("10次冲击，9次不破裂"))
                                {
                                    mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                    if (md2 <= 1)
                                    {
                                        mitem["LCCJ_HG"] = "合格";
                                        mFlag_Hg = true;
                                    }
                                    else
                                    {
                                        mitem["LCCJ_HG"] = "不合格";
                                        mbhggs = mbhggs + 1;
                                        mFlag_Bhg = true;
                                    }
                                }
                                if (mitem["G_LCCJ"].Contains("9/10"))
                                {
                                    mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                    if (md2 <= 1)
                                    {
                                        mitem["LCCJ_HG"] = "合格";
                                        mFlag_Hg = true;
                                    }
                                    else
                                    {
                                        mitem["LCCJ_HG"] = "不合格";
                                        mbhggs = mbhggs + 1;
                                        mFlag_Bhg = true;
                                    }
                                }
                            }
                        }
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("落锤冲击"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_LCCJ"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["LCCJ"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["LCCJ_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["LCCJ"] = "----";
                        mitem["LCCJ_HG"] = "----";
                        mitem["G_LCCJ"] = "----";
                    }
                    if (sitem["JCXM"].Trim().Contains("简支梁"))
                    {
                        if (mitem["G_JZLCJ"].Contains("≤") || mitem["G_JZLCJ"].Contains("＜"))
                        {
                            md1 = Conversion.Val(mitem["JZLCJBHGS"].Trim());
                            md2 = Conversion.Val(mitem["JZLCJCS"].Trim());
                            md = (int)(100 * md1 / md2);
                            mitem["JZLCJ"] = Round(md, 0).ToString("0.0");
                            mitem["JZLCJ_HG"] = calc_PB(mitem["G_JZLCJ"], mitem["JZLCJ"], false);
                            mbhggs = mitem["JZLCJ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (mitem["JZLCJ_HG"] != "不合格")
                                mFlag_Hg = true;
                            else
                                mFlag_Bhg = true;
                            mitem["JZLCJ"] = mitem["JZLCJ"] + "%";
                        }
                        else
                        {
                            md1 = Conversion.Val(mitem["JZLCJCS"].Trim());
                            md2 = Conversion.Val(mitem["JZLCJBHGS"].Trim());
                            md1 = Round(md1, 0);
                            md2 = Round(md2, 0);
                            if (mitem["G_JZLCJ"].Contains("12次冲击，12次不破裂"))
                            {
                                mitem["JZLCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                if (md2 == 0)
                                {
                                    mitem["JZLCJ_HG"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    mitem["JZLCJ_HG"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                }
                            }
                            if (mitem["G_JZLCJ"].Contains("10次冲击，9次不破裂"))
                            {
                                mitem["JZLCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                if (md2 <= 1)
                                {
                                    mitem["JZLCJ_HG"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    mitem["JZLCJ_HG"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                }
                            }
                            if (mitem["G_JZLCJ"].Contains("9/10"))
                            {
                                mitem["JZLCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                if (md2 <= 1)
                                {
                                    mitem["JZLCJ_HG"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    mitem["JZLCJ_HG"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                }
                            }
                        }
                        //向报告用字段赋值
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("简支梁"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_JZLCJ"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["JZLCJ"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["JZLCJ_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["JZLCJ"] = "----";
                        mitem["JZLCJ_HG"] = "----";
                        mitem["G_JZLCJ"] = "----";
                    }
                    if (sitem["JCXM"].Trim().Contains("纵向回缩率"))
                    {
                        mitem["ZXHSL_HG"] = calc_PB(mitem["G_ZXHSL"], mitem["ZXHSL"], false);
                        mbhggs = mitem["ZXHSL_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("纵向回缩率"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_ZXHSL"] + "%";
                        sitem["BGSCJG" + curJcxmCount] = mitem["ZXHSL"] + "%";
                        sitem["BGDXPD" + curJcxmCount] = mitem["ZXHSL_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["ZXHSL"] = "----";
                        mitem["ZXHSL_HG"] = "----";
                        mitem["G_ZXHSL"] = "----";
                    }
                    if (sitem["JCXM"].Trim().Contains("扁平试验"))
                    {
                        mbhggs = mitem["BPSY_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (mitem["BPSY_HG"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("扁平试验"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_BPSY"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["BPSY"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["BPSY_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["BPSY"] = "----";
                        mitem["BPSY_HG"] = "----";
                        mitem["G_BPSY"] = "----";
                    }
                    if (sitem["JCXM"].Trim().Contains("环刚"))
                    {
                        mitem["HGD_HG"] = calc_PB(mitem["G_HGD"], mitem["HGD"], false);
                        mbhggs = mitem["HGD_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (mitem["HGD_HG"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("环刚"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "kN/m&scsup2&scend";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_HGD"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["HGD"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["HGD_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["HGD"] = "----";
                        mitem["HGD_HG"] = "----";
                        mitem["G_HGD"] = "----";
                    }
                    if (sitem["JCXM"].Trim().Contains("液压"))
                    {
                        string[] yysyArray;
                        string yysyCs;
                        yysyArray = sitem["YYSYCS"].Split('、');
                        Gs = yysyArray.Length;
                        for (xd = 1; xd <= Gs; xd++)
                        {
                            if (mitem["YYSY_HG" + xd] == "不合格")
                            {
                                mitem["YYSY" + xd] = "有破裂、有渗漏";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                            else
                            {
                                mitem["YYSY" + xd] = "无破裂、无渗漏";
                                mFlag_Hg = true;
                            }
                        }
                        //向报告用字段赋值
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("液压"))
                            {
                                for (md = 1; md <= Gs; md++)
                                {
                                    yysyCs = yysyArray[md - 1];
                                    var mrsYysycs_Where = mrsYysycs.Where(x => x["MC"].Contains(sitem["SJDJ"].Trim()));
                                    string syyl = "";
                                    foreach (var mrsYysycs_item in mrsYysycs_Where)
                                    {
                                        if (Conversion.Val(sitem["GCWJ"]) < 40)
                                        {
                                            if ((mrsYysycs_item["ZJFW"] == "＜40" || mrsYysycs_item["ZJFW"] == "----" || mrsYysycs_item["ZJFW"].Trim() == "") &&
                                              (mrsYysycs_item["GXL"] == sitem["GXL"] || mrsYysycs_item["GXL"] == "----" || mrsYysycs_item["GXL"].Trim() == "") &&
                                              (mrsYysycs_item["SYCS"] == yysyCs || mrsYysycs_item["SYCS"] == "----" || mrsYysycs_item["SYCS"].Trim() == "") &&
                                              (mrsYysycs_item["CLDJ"] == sitem["CLDJ"].Trim() || mrsYysycs_item["CLDJ"] == "----" || mrsYysycs_item["CLDJ"].Trim() == ""))
                                            {
                                                syyl = mrsYysycs_item["SYSL"];
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if ((mrsYysycs_item["ZJFW"] == "≥40" || mrsYysycs_item["ZJFW"] == "----" || mrsYysycs_item["ZJFW"].Trim() == "") &&
                                              (mrsYysycs_item["GXL"] == sitem["GXL"] || mrsYysycs_item["GXL"] == "----" || mrsYysycs_item["GXL"].Trim() == "") &&
                                              (mrsYysycs_item["SYCS"] == yysyCs || mrsYysycs_item["SYCS"] == "----" || mrsYysycs_item["SYCS"].Trim() == "") &&
                                              (mrsYysycs_item["CLDJ"] == sitem["CLDJ"].Trim() || mrsYysycs_item["CLDJ"] == "----" || mrsYysycs_item["CLDJ"].Trim() == ""))
                                            {
                                                syyl = mrsYysycs_item["SYSL"];
                                                break;
                                            }
                                        }
                                    }
                                    if (yysyCs.Contains("，"))
                                        yysyCs = yysyCs.Replace("，", " " + syyl + " ");
                                    else
                                        yysyCs = yysyCs.Replace(" ", " " + syyl + " ");
                                    sitem["BGJCXM" + (curJcxmCount + md - 1)] = mtmpArray[xd] + yysyCs;
                                }
                                break;
                            }
                        }
                        for (xd = 1; xd <= Gs; xd++)
                        {
                            sitem["BGDW" + (curJcxmCount)] = "----";
                            sitem["BGBZYQ" + (curJcxmCount)] = mitem["G_YYSY" + xd];
                            sitem["BGSCJG" + (curJcxmCount)] = mitem["YYSY" + xd];
                            sitem["BGDXPD" + (curJcxmCount)] = mitem["YYSY_HG" + xd];
                            curJcxmCount = curJcxmCount + 1;
                        }
                    }
                    else
                    {
                        mitem["YYSY1"] = "----";
                        mitem["YYSY_HG1"] = "----";
                        mitem["G_YYSY1"] = "----";
                        mitem["YYSY2"] = "----";
                        mitem["YYSY_HG2"] = "----";
                        mitem["G_YYSY2"] = "----";
                        mitem["YYSY3"] = "----";
                        mitem["YYSY_HG3"] = "----";
                        mitem["G_YYSY3"] = "----";
                        mitem["YYSY4"] = "----";
                        mitem["YYSY_HG4"] = "----";
                        mitem["G_YYSY4"] = "----";
                    }
                    if (curJcxmCount < 9)
                        sitem["BGBZYQ" + curJcxmCount] = "以下空白";
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
                            mitem["JCJGMS"] = "该组试件所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                    }
                    mAllHg = (mAllHg && sitem["JCJG"].Trim() == "合格");
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
                    mitem["JCJGMS"] = "该组试样所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
