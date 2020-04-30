using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class HJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 参数定义
            string mlongStr;
            string mJSFF;
            bool mAllHg = true;
            bool mSFwc;
            int mbHggs;
            mSFwc = true;
            #endregion

            #region  自定义函数
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
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_HJ_DJ"];
            var MItem = data["M_HJ"];
            var SItem = data["S_HJ"];
            #endregion

            #region  计算开始
            int mCount = SItem.Count();
            var sitem = SItem.FirstOrDefault();
            mbHggs = 0;
            mSFwc = true;
            double md1, md2, xd1, xd2, md = 0, pjmd, sum, cd, kd, hd, zl;
            string bl, gclb;
            int xd, Gs = 0, Itemp;
            double[] nArr;
            int bHggs_A, bHggs_J, bHggs_B, bHggs_T, bHggs_D;
            string[,] sArr = new string[3, mCount + 1];
            bool flag, sign, mark;
            mbHggs = 0;
            string which = "";
            gclb = sitem["GCLB"].Trim();
            which = "bghj、bghj_1";
            //if ((GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime("2015-01-01")).Days >= 0)
            //{
            //Ifsitem["jcxm") Like "*氨*" Then mrsmainTablesitem["which") = mrsmainTablesitem["which") & "、bghj_21"
            // Ifsitem["jcxm") Like "*甲醛*" Then mrsmainTablesitem["which") = mrsmainTablesitem["which") & "、bghj_31"
            //}
            //else
            //{
            //Ifsitem["jcxm") Like "*氨*" Then mrsmainTablesitem["which") = mrsmainTablesitem["which") & "、bghj_2"
            //Ifsitem["jcxm") Like "*甲醛*" Then mrsmainTablesitem["which") = mrsmainTablesitem["which") & "、bghj_3"
            //}
            //Ifsitem["jcxm") Like "*苯*" Then mrsmainTablesitem["which") = mrsmainTablesitem["which") & "、bghj_4"
            //Ifsitem["jcxm") Like "*TVOC*" Then mrsmainTablesitem["which") = mrsmainTablesitem["which") & "、bghj_5"
            //Ifsitem["jcxm") Like "*氡*" Then mrsmainTablesitem["which") = mrsmainTablesitem["which") & "、bghj_6"
            bHggs_A = 0;
            bHggs_J = 0;
            bHggs_B = 0;
            bHggs_T = 0;
            bHggs_D = 0;
            IDictionary<string, string> mrsDj_Filter = new Dictionary<string, string>();
            for (xd = 1; xd <= mrsDj.Count(); xd++)
            {
                if (mrsDj[xd - 1]["GCLB"] == gclb && MItem[0]["JCYJ"].Contains(mrsDj[xd - 1]["JCYJBH"]))
                {
                    mrsDj_Filter = mrsDj[xd - 1];
                    break;
                }
            }
            if (xd >= mrsDj.Count)
            {
                mrsDj_Filter = mrsDj.FirstOrDefault(x => x["GCLB"].Contains(gclb) && x["JCYJBH"].Contains("2010%"));
            }
            //对 氨处理
            for (xd = 1; xd <= mCount; xd++)
            {
                sitem = SItem[xd - 1];
                sArr[1, xd] = sitem["FJWZ"];
                bl = sitem["ND_A"];
                sitem["W_ND_A"] = IsNumeric(bl) ? Conversion.Val(bl).ToString("F2") : bl;
                sArr[2, xd] = IsNumeric(bl) ? sitem["W_ND_A"] : bl;
                sitem["G_A_ND"] = mrsDj_Filter["G_A_ND"];
            }
            for (xd = 1; xd <= mCount; xd++)
            {
                bl = sArr[1, xd];
                for (Gs = xd + 1; Gs <= mCount; Gs++)
                {
                    if (bl != sArr[1, Gs])
                    {
                        Gs = Gs - 1;
                        break;
                    }
                }
                if (Gs > mCount)
                    Gs = mCount;
                sum = 0;
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    md = GetSafeDouble(sArr[2, Itemp]);
                    if (md.ToString().Contains("<"))
                        sum = md;
                    else if (md.ToString().Contains("＜"))
                        sum = md;
                    else
                        sum = sum + md;
                }
                if (sum.ToString().Contains("＜"))
                    md = sum;
                else if (md.ToString().Contains("<"))
                    md = sum;
                else
                {
                    md = sum / (Gs - xd + 1);
                    md = Round(md, 3);
                }
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    sitem = SItem[Itemp - 1];
                    if (md.ToString().Contains("＜"))
                    {
                        sitem["AVG_A"] = md.ToString();
                        sitem["PD_A"] = "合格";
                    }
                    else if (md.ToString().Contains("<"))
                    {
                        sitem["AVG_A"] = md.ToString();
                        sitem["PD_A"] = "合格";
                    }
                    else
                    {
                        sitem["AVG_A"] = md.ToString("0.00");
                        sitem["PD_A"] = calc_PB(sitem["G_A_ND"], sitem["AVG_A"], false);
                        bHggs_A = sitem["PD_A"] == "不合格" ? bHggs_A + 1 : bHggs_A;
                        sitem["AVG_A"] = sitem["AVG_A"] == "0.00" ? "未检出" : sitem["AVG_A"];
                    }
                }
                xd = Gs;

                #region 处理未做氨检测的相关记录
                for (int i = 1; i <= mCount; i++)
                {
                    string jcxm = "";
                    sitem = SItem[i - 1];
                    jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                    if (!jcxm.Contains("、氨、"))
                    {
                        sitem["G_A_ND"] = "----";
                        sitem["W_ND_A"] = "----";
                        sitem["AVG_A"] = "----";
                        sitem["PD_A"] = "----";
                    }
                }
                #endregion
            }
            //对 甲醛处理
            for (xd = 1; xd <= mCount; xd++)
            {
                sitem = SItem[xd - 1];
                sArr[1, xd] = sitem["FJWZ"].Trim();
                bl = sitem["ND_J"].Trim();
                sitem["W_ND_J"] = IsNumeric(bl) ? Conversion.Val(bl).ToString("F3") : bl;
                sArr[2, xd] = IsNumeric(bl) ? sitem["W_ND_J"] : bl;
                sitem["G_J_ND"] = mrsDj_Filter["G_J_ND"];
            }
            for (xd = 1; xd <= mCount; xd++)
            {
                bl = sArr[1, xd];
                for (Gs = xd + 1; Gs <= mCount; Gs++)
                {
                    if (bl != sArr[1, Gs])
                    {
                        Gs = Gs - 1;
                        break;
                    }
                }
                if (Gs > mCount)
                    Gs = mCount;
                sum = 0;
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    md = GetSafeDouble(sArr[2, Itemp]);
                    if (md.ToString().Contains("<"))
                        sum = md;
                    else if (md.ToString().Contains("＜"))
                        sum = md;
                    else
                        sum = sum + md;
                }
                if (sum.ToString().Contains("＜"))
                    md = sum;
                else if (md.ToString().Contains("<"))
                    md = sum;
                else
                {
                    md = sum / (Gs - xd + 1);
                    md = Round(md, 3);
                }
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    sitem = SItem[Itemp - 1];
                    if (md.ToString().Contains("＜"))
                    {
                        sitem["AVG_J"] = md.ToString();
                        sitem["PD_J"] = "合格";
                    }
                    else if (md.ToString().Contains("<"))
                    {
                        sitem["AVG_J"] = md.ToString();
                        sitem["PD_J"] = "合格";
                    }
                    else
                    {
                        sitem["AVG_J"] = md.ToString("0.000");
                        sitem["PD_J"] = calc_PB(sitem["G_J_ND"], sitem["AVG_J"], false);
                        bHggs_J = sitem["PD_J"] == "不合格" ? bHggs_J + 1 : bHggs_J;
                        sitem["AVG_J"] = sitem["AVG_J"] == "0.000" ? "未检出" : sitem["AVG_J"];
                    }
                }
                xd = Gs;

                #region 处理未做甲醛检测的相关记录
                for (int i = 1; i <= mCount; i++)
                {
                    string jcxm = "";
                    sitem = SItem[i - 1];
                    jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                    if (!jcxm.Contains("、甲醛、"))
                    {
                        sitem["G_J_ND"] = "----";
                        sitem["W_ND_J"] = "----";
                        sitem["AVG_J"] = "----";
                        sitem["PD_J"] = "----";
                    }
                }
                #endregion
            }
            //对 苯处理
            for (xd = 1; xd <= mCount; xd++)
            {
                sitem = SItem[xd - 1];
                sArr[1, xd] = sitem["FJWZ"].Trim();
                bl = sitem["ND_B"].Trim();
                sitem["W_ND_B"] = IsNumeric(bl) ? Conversion.Val(bl).ToString("F3") : bl;
                sArr[2, xd] = IsNumeric(bl) ? sitem["W_ND_B"] : bl;
                sitem["G_B_ND"] = mrsDj_Filter["G_B_ND"];
            }
            for (xd = 1; xd <= mCount; xd++)
            {
                bl = sArr[1, xd];
                for (Gs = xd + 1; Gs <= mCount; Gs++)
                {
                    if (bl != sArr[1, Gs])
                    {
                        Gs = Gs - 1;
                        break;
                    }
                }
                if (Gs > mCount)
                    Gs = mCount;
                sum = 0;
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    md = GetSafeDouble(sArr[2, Itemp]);
                    if (md.ToString().Contains("<"))
                        sum = md;
                    else if (md.ToString().Contains("＜"))
                        sum = md;
                    else
                        sum = sum + md;
                }
                if (sum.ToString().Contains("＜"))
                    md = sum;
                else if (md.ToString().Contains("<"))
                    md = sum;
                else
                {
                    md = sum / (Gs - xd + 1);
                    md = Round(md, 3);
                }
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    sitem = SItem[Itemp - 1];
                    if (md.ToString().Contains("＜"))
                    {
                        sitem["AVG_B"] = md.ToString();
                        sitem["PD_B"] = "合格";
                    }
                    else if (md.ToString().Contains("<"))
                    {
                        sitem["AVG_B"] = md.ToString();
                        sitem["PD_B"] = "合格";
                    }
                    else
                    {
                        sitem["AVG_B"] = md.ToString("F3");
                        sitem["PD_B"] = calc_PB(sitem["G_B_ND"], sitem["AVG_B"], false);
                        bHggs_B = sitem["PD_B"] == "不合格" ? bHggs_B + 1 : bHggs_B;
                        sitem["AVG_B"] = sitem["AVG_B"] == "0.000" ? "未检出" : sitem["AVG_B"];
                    }


                }
                xd = Gs;

                #region 处理未做苯检测的相关记录
                for (int i = 1; i <= mCount; i++)
                {
                    string jcxm = "";
                    sitem = SItem[i - 1];
                    jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                    if (!jcxm.Contains("、苯、"))
                    {
                        sitem["G_B_ND"] = "----";
                        sitem["W_ND_B"] = "----";
                        sitem["AVG_B"] = "----";
                        sitem["PD_B"] = "----";
                    }
                }
                #endregion
            }
            //对 TVOC处理
            for (xd = 1; xd <= mCount; xd++)
            {
                sitem = SItem[xd - 1];
                sArr[1, xd] = sitem["FJWZ"].Trim();
                bl = sitem["ND_T"].Trim();
                sitem["W_ND_T"] = IsNumeric(bl) ? Conversion.Val(bl).ToString("F2") : bl;
                sArr[2, xd] = IsNumeric(bl) ? sitem["W_ND_T"] : bl;
                sitem["G_T_ND"] = mrsDj_Filter["G_T_ND"];
            }

            for (xd = 1; xd <= mCount; xd++)
            {
                bl = sArr[1, xd];
                for (Gs = xd + 1; Gs <= mCount; Gs++)
                {
                    if (bl != sArr[1, Gs])
                    {
                        Gs = Gs - 1;
                        break;
                    }
                }
                if (Gs > mCount)
                    Gs = mCount;
                sum = 0;
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    md = GetSafeDouble(sArr[2, Itemp]);
                    if (md.ToString().Contains("<"))
                        sum = md;
                    else if (md.ToString().Contains("＜"))
                        sum = md;
                    else
                        sum = sum + md;
                }
                if (sum.ToString().Contains("＜"))
                    md = sum;
                else if (md.ToString().Contains("<"))
                    md = sum;
                else
                {
                    md = sum / (Gs - xd + 1);
                    md = Round(md, 3);
                }
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    sitem = SItem[Itemp - 1];
                    if (md.ToString().Contains("＜"))
                    {
                        sitem["AVG_T"] = md.ToString();
                        sitem["PD_T"] = "合格";
                    }
                    else if (md.ToString().Contains("<"))
                    {
                        sitem["AVG_T"] = md.ToString();
                        sitem["PD_T"] = "合格";
                    }
                    else
                    {
                        sitem["AVG_T"] = md.ToString("0.00");
                        sitem["PD_T"] = calc_PB(sitem["G_T_ND"], sitem["AVG_T"], false);
                        bHggs_T = sitem["PD_T"] == "不合格" ? bHggs_T + 1 : bHggs_T;
                        sitem["AVG_T"] = sitem["AVG_T"] == "0.00" ? "未检出" : sitem["AVG_T"];

                    }
                }
                xd = Gs;

                #region 处理未做TVOC检测的相关记录
                for (int i = 1; i <= mCount; i++)
                {
                    string jcxm = "";
                    sitem = SItem[i - 1];
                    jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                    if (!jcxm.Contains("、TVOC、"))
                    {
                        sitem["G_T_ND"] = "----";
                        sitem["W_ND_T"] = "----";
                        sitem["AVG_T"] = "----";
                        sitem["PD_T"] = "----";
                    }
                }
                #endregion
            }
            //对 氡处理



            for (xd = 1; xd <= mCount; xd++)
            {
                sitem = SItem[xd - 1];
                sArr[1, xd] = sitem["FJWZ"].Trim();
                bl = sitem["ND_D"].Trim();
                //sitem["W_ND_D"] = IsNumeric(bl) ? Conversion.Val(bl).ToString("F0") : bl;
                sitem["W_ND_D"] = IsNumeric(bl) ? Conversion.Val(bl).ToString() : bl;
                sArr[2, xd] = IsNumeric(bl) ? sitem["W_ND_D"] : "0";
                sitem["G_D_ND"] = mrsDj_Filter["G_D_ND"];
            }

            for (xd = 1; xd <= mCount; xd++)
            {
                bl = sArr[1, xd];
                for (Gs = xd + 1; Gs <= mCount; Gs++)
                {
                    if (bl != sArr[1, Gs])
                    {
                        Gs = Gs - 1;
                        break;
                    }
                }
                if (Gs > mCount)
                    Gs = mCount;
                sum = 0;
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    md = GetSafeDouble(sArr[2, Itemp]);
                    sum = sum + md;
                }
                md = sum / (Gs - xd + 1);
                //md = Round(md, 0);
                for (Itemp = xd; Itemp <= Gs; Itemp++)
                {
                    sitem = SItem[Itemp - 1];
                    //sitem["AVG_D"] = md.ToString("F0");
                    sitem["AVG_D"] = md.ToString();
                    sitem["PD_D"] = calc_PB(sitem["G_D_ND"], sitem["AVG_D"], false);
                    bHggs_D = sitem["PD_D"] == "不合格" ? bHggs_D + 1 : bHggs_D;
                    sitem["AVG_D"] = sitem["AVG_D"] == "0" ? "未检出" : sitem["AVG_D"];
                }
                xd = Gs;

                #region 处理未做氡检测的相关记录
                for (int i = 1; i <= mCount; i++)
                {
                    string jcxm = "";
                    sitem = SItem[i - 1];
                    jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                    if (!jcxm.Contains("、氡、"))
                    {
                        sitem["G_D_ND"] = "----";
                        sitem["W_ND_D"] = "----";
                        sitem["AVG_D"] = "----";
                        sitem["PD_D"] = "----";
                    }
                }
                #endregion
            }
            mbHggs = bHggs_A + bHggs_J + bHggs_B + bHggs_T + bHggs_D;
            MItem[0]["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
            //MItem[0]["JCJGMS"] = mbHggs == 0 ? "所检项目符合" + MItem[0]["JCYJ"] + "标准要求。" : "所检";
            //MItem[0]["JCJGMS"] = bHggs_A > 0 ? MItem[0]["JCJGMS"] + "氨、" : MItem[0]["JCJGMS"];
            //MItem[0]["JCJGMS"] = bHggs_J > 0 ? MItem[0]["JCJGMS"] + "甲醛、" : MItem[0]["JCJGMS"];
            //MItem[0]["JCJGMS"] = bHggs_B > 0 ? MItem[0]["JCJGMS"] + "苯、" : MItem[0]["JCJGMS"];
            //MItem[0]["JCJGMS"] = bHggs_T > 0 ? MItem[0]["JCJGMS"] + "TVOC、" : MItem[0]["JCJGMS"];
            //MItem[0]["JCJGMS"] = bHggs_D > 0 ? MItem[0]["JCJGMS"] + "氡、" : MItem[0]["JCJGMS"];
            //MItem[0]["JCJGMS"] = mbHggs == 0 ? MItem[0]["JCJGMS"] : MItem[0]["JCJGMS"].Substring(0, MItem[0]["JCJGMS"].Length - 1);
            //MItem[0]["JCJGMS"] = mbHggs == 0 ? MItem[0]["JCJGMS"] : MItem[0]["JCJGMS"] + "不符合" + MItem[0]["JCYJ"] + "标准要求。";

            MItem[0]["JCJGMS"] = mbHggs == 0 ? "依据" + MItem[0]["JCYJ"] + "标准，所检项目符合" + sitem["GCLB"] + "民用建筑工程验收时室内污染物浓度限量规定。" : "依据" + MItem[0]["JCYJ"] + "标准，所检项目中";
            MItem[0]["JCJGMS"] = bHggs_A > 0 ? MItem[0]["JCJGMS"] + "氨、" : MItem[0]["JCJGMS"];
            MItem[0]["JCJGMS"] = bHggs_J > 0 ? MItem[0]["JCJGMS"] + "甲醛、" : MItem[0]["JCJGMS"];
            MItem[0]["JCJGMS"] = bHggs_B > 0 ? MItem[0]["JCJGMS"] + "苯、" : MItem[0]["JCJGMS"];
            MItem[0]["JCJGMS"] = bHggs_T > 0 ? MItem[0]["JCJGMS"] + "TVOC、" : MItem[0]["JCJGMS"];
            MItem[0]["JCJGMS"] = bHggs_D > 0 ? MItem[0]["JCJGMS"] + "氡、" : MItem[0]["JCJGMS"];
            MItem[0]["JCJGMS"] = mbHggs == 0 ? MItem[0]["JCJGMS"] : MItem[0]["JCJGMS"].Substring(0, MItem[0]["JCJGMS"].Length - 1);
            MItem[0]["JCJGMS"] = mbHggs == 0 ? MItem[0]["JCJGMS"] : MItem[0]["JCJGMS"] + "不符合" + sitem["GCLB"] + "民用建筑工程验收时室内污染物浓度限量规定，需双倍复检。";


            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
