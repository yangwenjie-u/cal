using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class QN : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            int mbHggs;
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
            var mrsDj = dataExtra["BZ_QN_DJ"];
            var MItem = data["M_QN"];
            var mitem = MItem[0];
            var SItem = data["S_QN"];
            var mrsDrxs = data["ZM_DRJL"];
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            foreach (var sitem in SItem)
            {
                var mrsDj_item = mrsDj.FirstOrDefault(x => x["SJLB"].Equals(sitem["SJLB"].Trim()) && x["QDDJ"].Equals(sitem["GGXH"].Trim()));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    sitem["G_GMD"] = mrsDj_item["G_GMD"].Trim();
                    sitem["G_FCD"] = mrsDj_item["G_FCD"].Trim();
                    sitem["G_NJQD"] = mrsDj_item["G_NJQD"].Trim();
                    sitem["G_KYQD"] = mrsDj_item["G_KYQD"].Trim();
                    sitem["G_DRXS"] = mrsDj_item["G_DRXS"].Trim();
                    sitem["G_ZLSSL"] = mrsDj_item["G_ZLSSL"].Trim();
                    sitem["G_QDSSL"] = mrsDj_item["G_QDSSL"].Trim();
                    sitem["G_KZQD"] = mrsDj_item["G_KZQD"].Trim();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                int mcd, mdwz;
                bool sign, wghg;
                wghg = true;
                mbHggs = 0;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                sign = true;
                if (jcxm.Contains("、干密度、"))
                {
                    sign = IsNumeric(sitem["W_GMD"]) && !string.IsNullOrEmpty(sitem["W_GMD"]) ? sign : false;
                    if (sign)
                        sitem["GH_GMD"] = calc_PB(sitem["G_GMD"], sitem["W_GMD"], false);
                }
                else
                {
                    sitem["G_GMD"] = "----";
                    sitem["W_GMD"] = "----";
                    sitem["GH_GMD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、分层度、"))
                {
                    sign = IsNumeric(sitem["W_FCD"]) && !string.IsNullOrEmpty(sitem["W_FCD"]) ? sign : false;
                    if (sign)
                        sitem["GH_FCD"] = calc_PB(sitem["G_FCD"], sitem["W_FCD"], false);
                }
                else
                {
                    sitem["G_FCD"] = "----";
                    sitem["W_FCD"] = "----";
                    sitem["GH_FCD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、导热系数、"))
                {
                    sign = IsNumeric(sitem["W_DRXS"]) && !string.IsNullOrEmpty(sitem["W_DRXS"]) ? sign : false;
                    mcd = sitem["G_DRXS"].Length;
                    mdwz = sitem["G_DRXS"].IndexOf(".") + 1;
                    mcd = mcd - mdwz + 1;
                    if (mitem["DEVCODE"].Contains("XCS17-067") || mitem["DEVCODE"].Contains("XCS17-066"))
                    {
                        var ZM_DRJL = mrsDrxs.FirstOrDefault(x => x["SYSJBRECID"].Equals(x["RECID"]));
                        //var ZM_DRJL = mrsDrxs.FirstOrDefault(u => u["SYLB"] == "QN" && u["SYBH"] == mitem["JYDBH"]);
                        //var ZM_DRJL = mrsDrxs.FirstOrDefault();
                        sitem["W_DRXS"] = ZM_DRJL["DRXS"];
                        mitem["JCYJ"] = mitem["JCYJ"].Replace("10294", "10295");
                    }
                    sitem["W_DRXS"] = Round(Conversion.Val(sitem["W_DRXS"]), mcd).ToString();
                    if (sign)
                        sitem["GH_DRXS"] = calc_PB(sitem["G_DRXS"], sitem["W_DRXS"], false);
                }
                else
                {
                    sitem["G_DRXS"] = "----";
                    sitem["W_DRXS"] = "----";
                    sitem["GH_DRXS"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、粘结强度、"))
                {
                    sign = IsNumeric(sitem["W_NJQD"]) && !string.IsNullOrEmpty(sitem["W_NJQD"]) ? sign : false;
                    if (sign)
                        sitem["GH_NJQD"] = calc_PB(sitem["G_NJQD"], sitem["W_NJQD"], false);
                }
                else
                {
                    sitem["G_NJQD"] = "----";
                    sitem["W_NJQD"] = "----";
                    sitem["GH_NJQD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、抗压强度、"))
                {
                    sign = IsNumeric(sitem["W_KYQD"]) && !string.IsNullOrEmpty(sitem["W_KYQD"]) ? sign : false;
                    if (sign)
                        sitem["GH_KYQD"] = calc_PB(sitem["G_KYQD"], sitem["W_KYQD"], false);
                }
                else
                {
                    sitem["G_KYQD"] = "----";
                    sitem["W_KYQD"] = "----";
                    sitem["GH_KYQD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、抗折强度、"))
                {
                    sign = IsNumeric(sitem["W_KZQD"]) && !string.IsNullOrEmpty(sitem["W_KZQD"]) ? sign : false;
                    if (sign)
                        sitem["GH_KZQD"] = calc_PB(sitem["G_KZQD"], sitem["W_KZQD"], false);
                }
                else
                {
                    sitem["G_KZQD"] = "----";
                    sitem["W_KZQD"] = "----";
                    sitem["GH_KZQD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、抗冻性、"))
                {
                    if (sitem["WG"] == "明显破坏")
                    {
                        sitem["W_ZLSSL"] = "----";
                        sitem["GH_ZLSSL"] = "不合格";
                        sitem["W_QDSSL"] = "----";
                        sitem["GH_QDSSL"] = "不合格";
                        mbHggs = mbHggs + 1;
                        wghg = false;
                        sitem["G_ZLSSL"] = "质量损失率" + sitem["G_ZLSSL"];
                        sitem["G_QDSSL"] = "强度损失率" + sitem["G_QDSSL"];
                    }
                    else
                    {

                        sitem["GH_ZLSSL"] = calc_PB(sitem["G_ZLSSL"], sitem["W_ZLSSL"], false);
                        sitem["GH_QDSSL"] = calc_PB(sitem["G_QDSSL"], sitem["W_QDSSL"], false);
                        sitem["G_ZLSSL"] = "质量损失率" + sitem["G_ZLSSL"];
                        sitem["G_QDSSL"] = "强度损失率" + sitem["G_QDSSL"];
                    }
                }
                else
                {
                    sitem["G_ZLSSL"] = "----";
                    sitem["W_ZLSSL"] = "----";
                    sitem["GH_ZLSSL"] = "----";
                    sitem["G_QDSSL"] = "----";
                    sitem["W_QDSSL"] = "----";
                    sitem["GH_QDSSL"] = "----";
                }
                mbHggs = sitem["GH_GMD"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = sitem["GH_FCD"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = sitem["GH_NJQD"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = sitem["GH_DRXS"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = sitem["GH_KYQD"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = sitem["GH_KZQD"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = sitem["GH_ZLSSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = sitem["GH_QDSSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                sitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                mitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                mitem["JCJGMS"] = mbHggs == 0 ? "该组样品所检项目符合" + mitem["PDBZ"] + "标准要求。" : "该组所检项目样品不符合" + mitem["PDBZ"] + "标准要求。";
                if (mbHggs > 0 && (mitem["GH_GMD"] == "合格" ||
                mitem["GH_FCD"] == "合格" ||
                mitem["GH_NJQD"] == "合格" ||
                mitem["GH_DRXS"] == "合格" ||
                mitem["GH_KYQD"] == "合格" ||
                mitem["GH_KZQD"] == "合格" ||
                mitem["GH_ZLSSL"] == "合格" ||
                mitem["GH_QDSSL"] == "合格"))
                    mitem["JCJGMS"] = "该组样品所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                if (!wghg)
                    mitem["JCJGMS"] = "冻融试验过程中试件明显破坏,该组所检项目样品不符合" + mitem["PDBZ"] + "标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
