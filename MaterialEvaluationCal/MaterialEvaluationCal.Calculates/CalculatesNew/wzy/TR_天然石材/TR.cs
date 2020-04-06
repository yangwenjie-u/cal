using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TR : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/

            #region 参数定义
            string mcalBh, mlongStr;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            int mbhggs;
            bool mFlag_Hg, mFlag_Bhg;
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            var bhgJcxm = "";
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
                            l_bl = sj_fun.Substring(0, dw - 1);
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
                            l_bl = sj_fun.Substring(0, dw - 1);
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
                            l_bl = sj_fun.Substring(0, dw - 1);
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
                            l_bl = sj_fun.Substring(0, dw - 1);
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
                            min_sjz = GetSafeDouble(sj_fun.Substring(0, dw - 1));
                            max_sjz = GetSafeDouble(sj_fun.Substring(dw, length - dw));
                            min_bl = true;
                            max_bl = true;
                            sign_fun = true;
                        }
                        if (sj_fun.Contains("±"))
                        {
                            length = sj_fun.Length;
                            dw = sj_fun.IndexOf("±") + 1;
                            min_sjz = GetSafeDouble(sj_fun.Substring(0, dw - 1));
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


            Func<IDictionary<string, string>, IDictionary<string, string>, bool, bool> sjtabcalc =
                delegate (IDictionary<string, string> mitem, IDictionary<string, string> sitem, bool mAllHg_fun)
                {
                    mbhggs = 0;
                    int xd;
                    int sum;
                    sum = 0;
                    mFlag_Hg = false;
                    mFlag_Bhg = false;
                    var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                    if (jcxm.Contains("、体积密度、"))
                    {
                        mitem["GH_TJMD"] = calc_PB(mitem["G_TJMD"], mitem["W_TJMD"], false);
                        mbhggs = mitem["GH_TJMD"] == "合格" ? mbhggs : mbhggs + 1;
                        if (mitem["GH_TJMD"] == "合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        mitem["W_TJMD"] = "----";
                        mitem["GH_TJMD"] = "----";
                        mitem["G_TJMD"] = "----";
                    }
                    if (jcxm.Contains("、吸水率、"))
                    {
                        mitem["GH_XSL"] = calc_PB(mitem["G_XSL"], mitem["W_XSL"], false);
                        mbhggs = mitem["GH_XSL"] == "合格" ? mbhggs : mbhggs + 1;
                        if (mitem["GH_XSL"] == "合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        mitem["W_XSL"] = "----";
                        mitem["GH_XSL"] = "----";
                        mitem["G_XSL"] = "----";
                    }
                    if (jcxm.Contains("、干燥压缩强度、"))
                    {
                        mitem["GH_YSQD"] = calc_PB(mitem["G_YSQD"], mitem["W_YSQD"], false);
                        mbhggs = mitem["GH_YSQD"] == "合格" ? mbhggs : mbhggs + 1;
                        if (mitem["GH_YSQD"] == "合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        mitem["W_YSQD"] = "----";
                        mitem["G_YSQD"] = "----";
                        mitem["GH_YSQD"] = "----";
                    }
                    if (jcxm.Contains("、干燥弯曲强度、"))
                    {
                        mitem["GH_WQQD"] = calc_PB(mitem["G_WQQD"], mitem["W_WQQD"], false);
                        mbhggs = mitem["GH_WQQD"] == "合格" ? mbhggs : mbhggs + 1;
                        if (mitem["GH_WQQD"] == "合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        mitem["GH_WQQD"] = "----";
                        mitem["G_WQQD"] = "----";
                        mitem["W_WQQD"] = "----";
                    }

                    if (jcxm.Contains("、水饱和压缩强度、"))
                    {
                        mitem["GH_SBHYSQD"] = calc_PB(mitem["G_SBHYSQD"], mitem["W_SBHYSQD"], false);
                        mbhggs = mitem["GH_SBHYSQD"] == "合格" ? mbhggs : mbhggs + 1;
                        if (mitem["GH_SBHYSQD"] == "合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        mitem["W_SBHYSQD"] = "----";
                        mitem["G_SBHYSQD"] = "----";
                        mitem["GH_SBHYSQD"] = "----";
                    }
                    if (jcxm.Contains("、水饱和弯曲强度、"))
                    {
                        mitem["GH_SBHWQQD"] = calc_PB(mitem["G_SBHWQQD"], mitem["W_SBHWQQD"], false);
                        mbhggs = mitem["GH_SBHWQQD"] == "合格" ? mbhggs : mbhggs + 1;
                        if (mitem["GH_SBHWQQD"] == "合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        mitem["GH_SBHWQQD"] = "----";
                        mitem["G_SBHWQQD"] = "----";
                        mitem["W_SBHWQQD"] = "----";
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
                    }

                    if (mFlag_Bhg && mFlag_Hg)
                    {
                        mitem["JCJGMS"] = "该组试件所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                    }
                    mitem["JCJG"] = mbhggs == 0 ? "合格" : "不合格";
                    mAllHg_fun = (mAllHg_fun && sitem["JCJG"] == "合格");
                    return mAllHg_fun;
                };
            #endregion

            #region 集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_TR_DJ"];
            var MItem = data["M_TR"];
            var SItem = data["S_TR"];
            #endregion

            #region 计算开始
            foreach (var sitem in SItem)
            {
                mbhggs = 0;
                mSFwc = true;
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["SCZL"].Contains(sitem["SCZL"]) && x["KWZCFL"].Contains(sitem["KWZCFL"]));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    MItem[0]["G_TJMD"] = mrsDj_Filter["TJMD"].Trim();
                    MItem[0]["G_XSL"] = mrsDj_Filter["XSL"].Trim();
                    MItem[0]["G_YSQD"] = mrsDj_Filter["GZYSQD"].Trim();
                    MItem[0]["G_SBHYSQD"] = mrsDj_Filter["SBHYSQD"].Trim();
                    MItem[0]["G_WQQD"] = mrsDj_Filter["GZWQQD"].Trim();
                    MItem[0]["G_SBHWQQD"] = mrsDj_Filter["SBHWQQD"].Trim();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                mbhggs = 0;
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    if (sjtabcalc(MItem[0], sitem, mAllHg))
                    { }
                    continue;
                }
                double md1, md2, md3, xd1, xd2, xd3, md, pjmd, sum;
                string bl;
                int xd, Gs;
                double[] nArr;
                bool flag, sign, mark;
                sign = true;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                #region 体积密度
                if (jcxm.Contains("、体积密度、"))
                {
                    for (xd = 1; xd <= 5; xd++)
                    {
                        if (!IsNumeric(sitem["S_GZKQ" + xd]) || !IsNumeric(sitem["S_SKQ" + xd]) || !IsNumeric(sitem["S_SS" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    sign = IsNumeric(sitem["S_ZSMD"]) ? sign : false;
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 5; xd++)
                        {
                            md = Conversion.Val(sitem["S_GZKQ" + xd].Trim());
                            md = md * Conversion.Val(sitem["S_ZSMD"].Trim());
                            md1 = Conversion.Val(sitem["S_SKQ" + xd].Trim());
                            md2 = Conversion.Val(sitem["S_SS" + xd].Trim());
                            md = md / (md1 - md2);
                            md = Round(md, 2);
                            sum = sum + md;
                        }


                        pjmd = sum / 5;
                        pjmd = Round(pjmd, 2);
                        MItem[0]["W_TJMD"] = pjmd.ToString("F2");
                        MItem[0]["GH_TJMD"] = calc_PB(MItem[0]["G_TJMD"], MItem[0]["W_TJMD"], false);
                        mbhggs = MItem[0]["GH_TJMD"] == "合格" ? mbhggs : mbhggs + 1;
                        if (MItem[0]["GH_TJMD"] == "合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                            if (string.IsNullOrEmpty(bhgJcxm))
                            {
                                bhgJcxm = "体积密度";
                            }
                            else
                            {
                                bhgJcxm = bhgJcxm + "、体积密度";
                            }
                        }
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    MItem[0]["W_TJMD"] = "----";
                    MItem[0]["GH_TJMD"] = "----";
                    MItem[0]["G_TJMD"] = "----";
                }
                #endregion

                #region 吸水率
                sign = true;
                if (jcxm.Contains("、吸水率、"))
                {
                    for (xd = 1; xd <= 5; xd++)
                    {
                        if (!IsNumeric(sitem["S_GZKQ" + xd]) || !IsNumeric(sitem["S_SKQ" + xd]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (xd = 1; xd <= 5; xd++)
                        {
                            md1 = Conversion.Val(sitem["S_GZKQ" + xd].Trim());
                            md2 = Conversion.Val(sitem["S_SKQ" + xd].Trim());
                            md = 100 * (md2 - md1) / md1;
                            md = Round(md, 2);
                            sum = sum + md;
                        }

                        pjmd = sum / 5;
                        pjmd = Round(pjmd, 2);
                        MItem[0]["W_XSL"] = pjmd.ToString("F2");
                        MItem[0]["GH_XSL"] = calc_PB(MItem[0]["G_XSL"], MItem[0]["W_XSL"], false);
                        mbhggs = MItem[0]["GH_XSL"] == "合格" ? mbhggs : mbhggs + 1;
                        if (MItem[0]["GH_XSL"] == "合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                            if (string.IsNullOrEmpty(bhgJcxm))
                            {
                                bhgJcxm = "吸水率";
                            }
                            else
                            {
                                bhgJcxm = bhgJcxm + "、吸水率";
                            }
                        }
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    MItem[0]["W_XSL"] = "----";
                    MItem[0]["GH_XSL"] = "----";
                    MItem[0]["G_XSL"] = "----";
                }
                #endregion

                #region 干燥压缩强度
                sign = true;
                if (jcxm.Contains("、干燥压缩强度、"))
                {
                    MItem[0]["GH_YSQD"] = calc_PB(MItem[0]["G_YSQD"], MItem[0]["W_YSQD"], false);
                    mbhggs = MItem[0]["GH_YSQD"] == "合格" ? mbhggs : mbhggs + 1;
                    if (MItem[0]["GH_YSQD"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Bhg = true;
                        if (string.IsNullOrEmpty(bhgJcxm))
                        {
                            bhgJcxm = "干燥压缩强度";
                        }
                        else
                        {
                            bhgJcxm = bhgJcxm + "、干燥压缩强度";
                        }
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    MItem[0]["W_YSQD"] = "----";
                    MItem[0]["G_YSQD"] = "----";
                    MItem[0]["GH_YSQD"] = "----";
                }
                #endregion

                #region 干燥弯曲强度
                sign = true;
                if (jcxm.Contains("、干燥弯曲强度、"))
                {
                    MItem[0]["GH_WQQD"] = calc_PB(MItem[0]["G_WQQD"], MItem[0]["W_WQQD"], false);
                    mbhggs = MItem[0]["GH_WQQD"] == "合格" ? mbhggs : mbhggs + 1;
                    if (MItem[0]["GH_WQQD"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Bhg = true;
                        if (string.IsNullOrEmpty(bhgJcxm))
                        {
                            bhgJcxm = "干燥弯曲强度";
                        }
                        else
                        {
                            bhgJcxm = bhgJcxm + "、干燥弯曲强度";
                        }
                    }
                }
                else
                    sign = false;


                if (!sign)
                {
                    MItem[0]["GH_WQQD"] = "----";
                    MItem[0]["G_WQQD"] = "----";
                    MItem[0]["W_WQQD"] = "----";
                }
                #endregion

                #region 水饱和压缩强度
                sign = true;
                if (jcxm.Contains("、水饱和压缩强度、"))
                {
                    MItem[0]["GH_SBHYSQD"] = calc_PB(MItem[0]["G_SBHYSQD"], MItem[0]["W_SBHYSQD"], false);
                    mbhggs = MItem[0]["GH_SBHYSQD"] == "合格" ? mbhggs : mbhggs + 1;
                    if (MItem[0]["GH_SBHYSQD"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Bhg = true;
                        if (string.IsNullOrEmpty(bhgJcxm))
                        {
                            bhgJcxm = "水饱和压缩强度";
                        }
                        else
                        {
                            bhgJcxm = bhgJcxm + "、水饱和压缩强度";
                        }
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    MItem[0]["W_SBHYSQD"] = "----";
                    MItem[0]["G_SBHYSQD"] = "----";
                    MItem[0]["GH_SBHYSQD"] = "----";
                }
                #endregion

                #region 水饱和弯曲强度
                sign = true;
                if (jcxm.Contains("、水饱和弯曲强度、"))
                {
                    MItem[0]["GH_SBHWQQD"] = calc_PB(MItem[0]["G_SBHWQQD"], MItem[0]["W_SBHWQQD"], false);
                    mbhggs = MItem[0]["GH_SBHWQQD"] == "合格" ? mbhggs : mbhggs + 1;
                    if (MItem[0]["GH_SBHWQQD"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Bhg = true;
                        if (string.IsNullOrEmpty(bhgJcxm))
                        {
                            bhgJcxm = "水饱和弯曲强度";
                        }
                        else
                        {
                            bhgJcxm = bhgJcxm + "、水饱和弯曲强度";
                        }
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    MItem[0]["GH_SBHWQQD"] = "----";
                    MItem[0]["G_SBHWQQD"] = "----";
                    MItem[0]["W_SBHWQQD"] = "----";
                }
                #endregion

                sitem["JCJG"] = mbhggs == 0 ? "合格" : "不合格";
                MItem[0]["JCJG"] = mbhggs == 0 ? "合格" : "不合格";
                mAllHg = mbhggs == 0 ? mAllHg : false;
            }
            MItem[0]["JCJGMS"] = mAllHg ? "该组样品所检项目符合" + MItem[0]["PDBZ"] + "标准要求。" : "该组样品所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
            if (mFlag_Bhg && mFlag_Hg)
            {
                MItem[0]["JCJGMS"] = "该组样品所检项目"+bhgJcxm+"不符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
               
            #endregion

            /************************ 代码结束 *********************/
        }
    }
}
