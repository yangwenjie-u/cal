using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SW : BaseMethods
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
            bool mFlag_Hg, mFlag_Bhg;
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
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
            var mrsDj = dataExtra["BZ_SW_DJ"];
            var MItem = data["M_SW"];
            var SItem = data["S_SW"];
            #endregion

            #region  计算开始
            //循环从表
            foreach (var sitem in SItem)
            {
                double md1, md2, xd1, xd2, md, pjmd, sum, cd, kd, hd, zl;
                string bl;
                string mkdbhg;
                int xd, Gs;
                List<double> nArr = new List<double>();
                bool flag, sign, mark;
                mbHggs = 0;
                sign = true;
                if (sitem["JCXM"].Contains("抗弯曲性能"))
                {
                    switch (sitem["XZ_LB"])
                    {
                        case "三曲瓦":
                            mark = true;
                            break;
                        case "双筒瓦":
                            mark = true;
                            break;
                        case "鱼鳞瓦":
                            mark = true;
                            break;
                        case "牛舌瓦":
                            mark = true;
                            break;
                        default:
                            mark = false;
                            break;
                    }
                    for (int i = 1; i < 6; i++)
                    {
                        if (!IsNumeric(sitem["DLHZ" + i]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (mark)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            if (!IsNumeric(sitem["SJKJ" + i]))
                            {
                                sign = false;
                                break;
                            }
                        }
                        for (int i = 1; i < 6; i++)
                        {
                            if (!IsNumeric(sitem["SJKD" + i]))
                            {
                                sign = false;
                                break;
                            }
                        }
                        for (int i = 1; i < 6; i++)
                        {
                            if (!IsNumeric(sitem["SJHD" + i]))
                            {
                                sign = false;
                                break;
                            }
                        }
                    }
                    if (sign)
                    {
                        if (mark)
                        {
                            for (int i = 1; i < 6; i++)
                            {
                                md1 = GetSafeDouble(sitem["DLHZ" + i].Trim());
                                md2 = GetSafeDouble(sitem["SJKJ" + i].Trim());
                                xd1 = GetSafeDouble(sitem["SJKD" + i].Trim());
                                xd2 = GetSafeDouble(sitem["SJHD" + i].Trim());
                                md = 3 * md1 * md2 / 2 / xd1 / Math.Pow(xd2, 2);
                                md = Round(md, 1);
                                MItem[0]["W_KWXN" + i] = md.ToString("0.0");
                            }
                        }
                        else
                        {
                            for (int i = 1; i < 6; i++)
                            {
                                md = GetSafeDouble(sitem["DLHZ" + i].Trim());
                                md = Round((md / 10), 0) * 10;
                                MItem[0]["W_KWXN" + i] = md.ToString("F0");
                            }
                        }
                        switch (sitem["XZ_LB"].Trim())
                        {
                            case "平瓦":
                            case "脊瓦":
                            case "板瓦":
                            case "筒瓦":
                            case "滴水瓦":
                            case "沟头瓦":
                                if (sitem["XSL_LB"].Trim() == "青瓦")
                                {
                                    MItem[0]["G_WQXN"] = "弯曲破坏荷重不小于850N";
                                    bl = "≥850";
                                }
                                else
                                {
                                    MItem[0]["G_WQXN"] = "弯曲破坏荷重不小于1200N";
                                    bl = "≥1200";
                                }
                                break;
                            case "J形瓦":
                            case "S形瓦":
                            case "波形瓦":
                            case "其他异形瓦":
                                MItem[0]["G_WQXN"] = "弯曲破坏荷重不小于1600N";
                                bl = "≥1600";
                                break;
                            default:
                                MItem[0]["G_WQXN"] = "弯曲强度不小于8.0MPa";
                                bl = "≥8.0";
                                break;
                        }
                        MItem[0]["GH_WQXN"] = "合格";
                        for (int i = 1; i < 6; i++)
                        {
                            if (calc_PB(bl, MItem[0]["W_KWXN" + i], false) == "不合格")
                            {
                                MItem[0]["GH_WQXN"] = "不合格";
                                break;
                            }
                        }
                        for (int i = 1; i < 6; i++)
                            MItem[0]["W_KWXN" + i] = mark ? MItem[0]["W_KWXN" + i] + "  MPa" : MItem[0]["W_KWXN" + i] + "  N";
                        if (MItem[0]["GH_WQXN"] == "合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    for (int i = 1; i < 6; i++)
                        MItem[0]["W_KWXN" + i] = "----";
                    MItem[0]["GH_WQXN"] = "----";
                    MItem[0]["G_WQXN"] = "----";
                }
                sign = true;
                if (sitem["JCXM"].Contains("吸水率"))
                {
                    double max_xsl;
                    max_xsl = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        if (!IsNumeric(sitem["XSQZL" + i]))
                        {
                            sign = false;
                            break;
                        }
                        if (!IsNumeric(sitem["XSHZL" + i]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (int i = 1; i < 6; i++)
                        {
                            md1 = GetSafeDouble(sitem["XSQZL" + i].Trim());
                            md2 = GetSafeDouble(sitem["XSHZL" + i].Trim());
                            md = 100 * (md2 - md1) / md1;
                            md = Round(md, 1);
                            if (md > max_xsl)
                                max_xsl = md;
                            MItem[0]["W_XSL" + i] = md.ToString("0.0");
                        }
                        if (max_xsl <= 6)
                        {
                            sitem["XSL_LB"] = "Ⅰ类瓦";
                            MItem[0]["G_XSL"] = "不大于6.0%";
                        }
                        else
                        {
                            if (max_xsl <= 10)
                            {
                                sitem["XSL_LB"] = "Ⅱ类瓦";
                                MItem[0]["G_XSL"] = "大于6.0%、不大于10.0%";
                            }
                            else
                            {
                                if (max_xsl <= 18)
                                {
                                    sitem["XSL_LB"] = "Ⅲ类瓦";
                                    MItem[0]["G_XSL"] = "大于10.0%、不大于18.0%";

                                }
                                else if (max_xsl <= 21)
                                {
                                    sitem["XSL_LB"] = "青瓦";
                                    MItem[0]["G_XSL"] = "不大于21.0%";
                                }
                            }
                        }
                        MItem[0]["GH_XSL"] = sitem["XSL_LB"];

                        for (int i = 1; i < 6; i++)
                            MItem[0]["W_XSL" + i] = MItem[0]["W_XSL" + i] + "  %";
                    }
                }
                else
                    sign = false;
                if (!sign)
                {
                    for (int i = 1; i < 6; i++)
                        MItem[0]["W_XSL" + i] = "----";
                    MItem[0]["G_XSL"] = "----";
                    MItem[0]["GH_XSL"] = "----";
                }
                sign = true;
                if (sitem["JCXM"].Contains("抗冻性能"))
                {
                    if (MItem[0]["W_KDXN1"].Contains("无炸裂") && MItem[0]["W_KDXN2"].Contains("无炸裂") && MItem[0]["W_KDXN3"].Contains("无炸裂") && MItem[0]["W_KDXN4"].Contains("无炸裂") && MItem[0]["W_KDXN5"].Contains("无炸裂"))
                    {
                        MItem[0]["GH_KDXN"] = "合格";
                        MItem[0]["G_KDXN"] = "经15次冻融循环不出现炸裂、剥落及裂纹增加现象";
                        MItem[0]["W_KDXN"] = "无炸裂、剥落及裂纹增加";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        MItem[0]["GH_KDXN"] = "不合格";
                        MItem[0]["G_KDXN"] = "经15次冻融循环不出现炸裂、剥落及裂纹增加现象";
                        MItem[0]["W_KDXN"] = "有炸裂、剥落及裂纹增加";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    MItem[0]["W_KDXN"] = "----";
                    MItem[0]["G_KDXN"] = "----";
                    MItem[0]["GH_KDXN"] = "----";
                }
                if (sitem["JCXM"].Contains("抗渗性能"))
                {
                    if (MItem[0]["W_KSXN1"].Contains("无水滴") && MItem[0]["W_KSXN2"].Contains("无水滴") && MItem[0]["W_KSXN3"].Contains("无水滴"))
                    {
                        MItem[0]["GH_KSXN"] = "合格";
                        MItem[0]["G_KSXN"] = "经3h瓦背面无水滴产生";
                        MItem[0]["W_KSXN"] = "无水滴";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        MItem[0]["GH_KSXN"] = "不合格";
                        MItem[0]["G_KSXN"] = "经3h瓦背面无水滴产生";
                        MItem[0]["W_KSXN"] = "有水滴";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    MItem[0]["W_KSXN"] = "----";
                    MItem[0]["G_KSXN"] = "----";
                    MItem[0]["GH_KSXN"] = "----";
                }

                mbHggs = MItem[0]["GH_KDXN"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_KSXN"] == "不合格" ? mbHggs + 1 : mbHggs;

                mbHggs = MItem[0]["GH_XSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_WQXN"] == "不合格" ? mbHggs + 1 : mbHggs;
                sitem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                MItem[0]["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                MItem[0]["JCJGMS"] = mbHggs == 0 ? "该组样品所检项目符合" + MItem[0]["PDBZ"] + "标准要求。" : "该组样品所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                if (mFlag_Bhg && mFlag_Hg)
                    MItem[0]["JCJGMS"] = "该组样品所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
