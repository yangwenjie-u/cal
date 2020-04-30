
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class MA : BaseMethods
    {
        public void Calc(){
            #region 计算开始
            #region 变量定义
            var data = retData;
            var extraDJ = dataExtra["BZ_MA_DJ"];
            var MItem = data["M_MA"];
            var SItem = data["S_MA"];
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果不合格";


            int mbHggs = 0;
            #endregion
            #region 局部方法
           /* Func<string, string, bool, string> calc_PB = delegate (string sj, string sc, bool flag)
            {

                sj = sj.Trim();
                sc = sc.Trim();
                if (IsNumeric(sj) == false)
                {
                    return "----";

                }
                sj = sj.Replace("~", "～");
                String l_bl, r_bl;
                Single min_sjz, max_sjz, scz;
                int length, dw;
                Boolean min_bl, max_bl, sign;
                min_sjz = -99999; max_sjz = 99999;
                scz = Convert.ToSingle(sc);
                sign = false; min_bl = false; max_bl = false;
                if (sj.IndexOf("＞", 0) > 0)
                {
                    length = sj.Length;
                    dw = sj.IndexOf("＞", 0);
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw + 1, length);
                    if (IsNumeric(l_bl))
                    {
                        max_sjz = Convert.ToSingle(l_bl);
                        max_bl = false;
                    }
                    if (IsNumeric(r_bl))
                    {
                        min_sjz = Convert.ToSingle(r_bl);
                        min_bl = false;

                    }
                    sign = true;
                }
                if (sj.IndexOf("≥", 0) > 0)
                {
                    length = sj.Length;
                    dw = sj.IndexOf("＞", 0);
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw + 1, length);
                    if (IsNumeric(l_bl))
                    {
                        max_sjz = Convert.ToSingle(l_bl);
                        max_bl = true;
                    }
                    if (IsNumeric(r_bl))
                    {
                        min_sjz = Convert.ToSingle(r_bl);
                        min_bl = true;
                        sign = true;
                    }

                }
                if (sj.IndexOf("＜", 0) > 0)
                {
                    length = sj.Length;
                    dw = sj.IndexOf("＞", 0);
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw + 1, length);
                    if (IsNumeric(l_bl))
                    {
                        min_sjz = Convert.ToSingle(l_bl);
                        min_bl = false;


                    }
                    if (IsNumeric(r_bl))
                    {
                        max_sjz = Convert.ToSingle(r_bl);
                        max_bl = false;


                    }
                    sign = true;
                }
                if (sj.IndexOf("≤", 0) > 0)
                {
                    length = sj.Length;
                    dw = sj.IndexOf("＞", 0);
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw + 1, length);
                    if (IsNumeric(l_bl))
                    {
                        min_sjz = Convert.ToSingle(l_bl);
                        min_bl = true;


                    }
                    if (IsNumeric(r_bl))
                    {
                        max_sjz = Convert.ToSingle(r_bl);
                        max_bl = true;


                    }
                    sign = true;
                }
                if (sj.IndexOf("～", 0) > 0)
                {
                    length = sj.Length;
                    dw = sj.IndexOf("～", 0);
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw + 1, length);
                    if (IsNumeric(l_bl))
                    {
                        min_sjz = Convert.ToSingle(l_bl);
                        min_bl = true;


                    }
                    if (IsNumeric(r_bl))
                    {
                        max_sjz = Convert.ToSingle(r_bl);
                        max_bl = true;


                    }
                    sign = true;
                }
                if (sj.IndexOf("±", 0) > 0)
                {
                    length = sj.Length;
                    dw = sj.IndexOf("～", 0);
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw + 1, length);
                    if (IsNumeric(l_bl))
                    {
                        min_sjz = Convert.ToSingle(l_bl);

                    }
                    if (IsNumeric(r_bl))
                    {
                        max_sjz = Convert.ToSingle(r_bl);

                    }
                    min_sjz = min_sjz - max_sjz;
                    max_sjz = min_sjz + 2 * max_sjz;
                    min_bl = true;
                    max_bl = true;
                    sign = true;
                }
                if (sj == "0")
                {
                    sign = true;
                    min_bl = false;
                    max_bl = false;
                    max_sjz = 0;
                }
                if (sign == false)
                {
                    return "----";
                }
                string hgjl, bhgjl;
                hgjl = flag ? "符合" : "合格";
                bhgjl = flag ? "不符合" : "不合格";
                //做为判定了
                sign = true;
                if (min_bl == true)
                {
                    var a1 = scz >= min_sjz;
                    sign = a1 ? sign : false;
                }
                else
                {
                    var a1 = scz > min_sjz;
                    sign = a1 ? sign : false;
                }
                if (max_bl == true)
                {
                    var a2 = scz <= max_sjz;
                    sign = a2 ? sign : false;
                }
                else
                {
                    var a2 = scz < max_sjz;
                    sign = a2 ? sign : false;
                }

                return sign ? hgjl : bhgjl;
            };*/
            #endregion
            #region 主表处理
            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            #endregion
            #region 从表处理
            foreach (var sItem in SItem)
            {
                var jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                bool sign, flag;
                sign = true;
                double pjmd, md1, md2, md, sum;
                #region 等级表处理
                var mrsdj0 = extraDJ.Where(x => x["LB"].Trim().Equals(sItem["WJJLB"], StringComparison.OrdinalIgnoreCase)).ToList();
                var mrsdj = mrsdj0.Where(x => x["DJ"].Trim().Equals(sItem["WJJDJ"], StringComparison.OrdinalIgnoreCase)).ToList();
                if (mrsdj != null && mrsdj.Count() > 0)
                {
                    MItem[0]["G_YHM"] = mrsdj[0]["YHM"];
                    MItem[0]["G_SYHL"] = mrsdj[0]["SYHL"];
                    MItem[0]["G_SSL"] = mrsdj[0]["SSL"];
                    MItem[0]["G_LLZ"] = mrsdj[0]["LLZ"];
                    MItem[0]["G_YHG"] = mrsdj[0]["YHG"];
                    MItem[0]["G_HSL"] = mrsdj[0]["HSL"];
                    MItem[0]["G_BBMJ"] = mrsdj[0]["BBMJ"];
                    MItem[0]["G_XSLB"] = mrsdj[0]["XSLB"];
                    MItem[0]["G_HXZS3"] = mrsdj[0]["HXZS3"];
                    MItem[0]["G_HXZS7"] = mrsdj[0]["HXZS7"];
                    MItem[0]["G_HXZS2"] = mrsdj[0]["HXZS2"];
                    MItem[0]["G_XAZ"] = mrsdj[0]["XAZ"];
                }
                else
                {
                    sItem["JCJG"] = "依据不详";
                    MItem[0]["JCJGMS"] =  "试件尺寸为空";
                    continue;
                }
                #endregion

                #region 氧化镁
                
                    if (jcxm.Contains("、氧化镁、"))
                    {
                    flag = true;
                    flag = IsNumeric(sItem["YHM_ND"]) ? flag : false;
                    flag = IsNumeric(sItem["YHM_TJ"]) ? flag : false;
                    flag = IsNumeric(sItem["YHM_TJB"]) ? flag : false;
                    flag = IsNumeric(sItem["YHM_ZL"]) ? flag : false;

                    if (flag)
                    {
                        md1 = GetSafeDouble(sItem["YHM_ND"].Trim());
                        md2 = GetSafeDouble(sItem["YHM_TJ"].Trim());
                        md = md1 * md2;
                        md1 = GetSafeDouble(sItem["YHM_TJB"].Trim());
                        md2 = GetSafeDouble(sItem["YHM_ZL"].Trim());
                        md = GetSafeDouble("0.1") * md * md1 / md2;
                        md = Round(md, 0);
                        MItem[0]["W_YHM"] = string.Format(md.ToString(), "0");
                        MItem[0]["GH_YHM"] = IsQualified(MItem[0]["G_YHM"], MItem[0]["W_YHM"], false);

                    }
                    else { sign = false; }
                }
                else { sign = false; }
                if (!sign)
                {
                    MItem[0]["W_YHM"] = "----";
                    MItem[0]["G_YHM"] = "----";
                    MItem[0]["GH_YHM"] = "----";
                }
                #endregion

                #region 比表面积
                sign = true;
                bool mark1 = true;
                
                    if (jcxm.Contains("、比表面积、"))
                    {
                        double[] nArr = new double[2];
                        double sum1 = 0;
                        flag = true;
                        flag = IsNumeric(sItem["BB_BZWD"]) ? flag : false;
                        flag = IsNumeric(sItem["BB_SYWD1"]) ? flag : false;
                        flag = IsNumeric(sItem["BB_SYWD2"]) ? flag : false;
                        flag = IsNumeric(sItem["BB_BZKXL"]) ? flag : false;
                        flag = IsNumeric(sItem["BB_SYKXL"]) ? flag : false;
                        flag = IsNumeric(sItem["BB_BZMJ"]) ? flag : false;
                        for (int i = 0; i < 2; i++)
                        {
                            int a = i + 1;
                            md1 = GetSafeDouble(sItem["BB_BZWD"].Trim());
                            md2 = GetSafeDouble(sItem["BB_SYWD" + a].Trim());
                            mark1 = Math.Abs(md1 - md2) > 3 ? true : false;
                            if(sItem["BB_BZKXL"]== sItem["BB_SYKXL"])
                            {
                                md1= GetSafeDouble(sItem["BB_BZWD"].Trim());
                                md2 = GetSafeDouble(sItem["BB_SYWD" + a].Trim());
                                md= GetSafeDouble(sItem["BB_BZMJ"].Trim());
                                md = Math.Pow(md * (md2 / md1), (1 / 2));

                            }
                            else
                            {
                                md1 = GetSafeDouble(sItem["BB_BZWD"].Trim());
                                md2 = GetSafeDouble(sItem["BB_SYWD" + a].Trim());
                                md = GetSafeDouble(sItem["BB_BZMJ"].Trim());
                                md = Math.Pow(md * (md2 / md1), (1 / 2));
                                md1 = GetSafeDouble(sItem["BB_BZKXL"].Trim());
                                md2 = GetSafeDouble(sItem["BB_SYKXL"].Trim());
                                md = Math.Pow(md * (1 - md1) / (1 - md2) * (md2 / md1), (3 / 2));
                          
                            }
                            if (mark1)
                            {
                                md1 = GetSafeDouble(sItem["BB_BZND"].Trim());
                                md2 = GetSafeDouble(sItem["BB_SYND"+a].Trim());
                                md = Math.Pow(md * (md1 / md2), (1 / 2));
                            }
                            md = Round(md, 0);
                            nArr[i] = md;
                            sum1 = sum1 + md;
                            var ssum1 = Round(sum1 / 2, 0);
                            ssum1 = Round(ssum1 / 10, 0) * 10;
                            MItem[0]["W_BBMJ"] = string.Format(ssum1.ToString(), "0");
                            MItem[0]["GH_BBMJ"] = IsQualified(MItem[0]["G_BBMJ"], MItem[0]["W_BBMJ"], false);
                            md = Math.Abs(nArr[0] - nArr[1]);
                            if (md / nArr[0] > 0.02 | md / nArr[1] > 0.02)
                            {
                                MItem[0]["W_BBMJ"] = "重做";
                        }

                    }

                    }
                    else
                    {
                        sign = !sign;
                    }
                    if (!sign)
                    {
                        MItem[0]["W_BBMJ"] = "----";
                        MItem[0]["G_BBMJ"] = "----";
                        MItem[0]["GH_BBMJ"] = "----";
                    }

                #endregion

                #region 吸铵值
                sign = true;
                bool mark;
                
                    if (jcxm.Contains("、吸铵值、"))
                    {
                    flag = true;
                    for (int i = 1; i < 3; i++)
                    {
                        flag = IsNumeric(sItem["XAZ_ND" + i]) ? flag : false;
                        flag = IsNumeric(sItem["XAZ_TJ" + i]) ? flag : false;
                        flag = IsNumeric(sItem["XAZ_ZL" + i]) ? flag : false;
                    }
                    double [] nArr = new double[2];
                    if (flag)
                    {

                        sum = 0;
                        for (int i = 0; i < nArr.Length; i++)
                        {
                            int a = i+1;
                            md1 = GetSafeDouble(sItem["XAZ_ND" + a].Trim() );
                            md2 = GetSafeDouble(sItem["XAZ_TJ" + a].Trim());
                            md = GetSafeDouble(sItem["XAZ_ZL" + a].Trim() );
                            md = 100 * md1 * md2 / md;
                            md = Round(md, 1);
                            MItem[0]["W_XAZ" + a] = string.Format(md.ToString(), "0");
                            nArr[i] = md;
                            sum = sum + md;
                        }
                        var ssum = GetSafeDouble(sum.ToString()) / 2;
                        var sssum = Round(ssum, 1);
                        pjmd = sssum;
                        MItem[0]["W_PJXAZ"] = string.Format(pjmd.ToString(), "0,0");
                        MItem[0]["GH_XAZ"] = IsQualified(MItem[0]["G_XAZ"], MItem[0]["W_PJXAZ"], false);
                        var dd = Math.Abs(nArr[0] - nArr[1]) > GetSafeDouble("3");
                        if (dd)
                        {
                            sItem["W_PJXAZ"] = "重做";
                            MItem[0]["GH_XAZ"] = "----";
                        }

                    }
                    else { sign = false; }

                }
                else { sign = false; }
                if (!sign)
                {
                    MItem[0]["W_PJXAZ"] = "----";
                    MItem[0]["W_XAZ1"] = "----";
                    MItem[0]["W_XAZ2"] = "----";
                    MItem[0]["GH_XAZ"] = "----";
                    MItem[0]["G_XAZ"] = "----";

                }
                #endregion

                #region 活性指数
                sign = true;
              
                    if (jcxm.Contains("、活性指数、"))
                    {
                    flag = true;
                    flag = IsNumeric(sItem["HX_SYQD7"]) ? flag : false;
                    flag = IsNumeric(sItem["HX_DBQD7"]) ? flag : false;
                    flag = IsNumeric(sItem["HX_SYQD2"]) ? flag : false;
                    flag = IsNumeric(sItem["HX_DBQD2"]) ? flag : false;
                    flag = IsNumeric(sItem["HX_SYQD3"]) ? flag : false;
                    flag = IsNumeric(sItem["HX_DBQD3"]) ? flag : false;
                    if (flag)
                    {
                        md1 = GetSafeDouble(sItem["HX_SYQD7"].Trim());
                        md2 = GetSafeDouble(sItem["HX_DBQD7"].Trim());
                        md = 100 * md1 / md2;
                        md = Round(md, 0);
                        MItem[0]["W_HXZS7"] = string.Format(md.ToString(), "0");

                        md1 = GetSafeDouble(sItem["HX_SYQD2"].Trim());
                        md2 = GetSafeDouble(sItem["HX_DBQD2"].Trim());
                        md = 100 * md1 / md2;
                        md = Round(md, 0);
                        MItem[0]["W_HXZS2"] = string.Format(md.ToString(), "0");

                        md1 = GetSafeDouble(sItem["HX_SYQD3"].Trim());
                        md2 = GetSafeDouble(sItem["HX_DBQD3"].Trim());
                        md = 100 * md1 / md2;
                        md = Round(md, 0);
                        MItem[0]["W_HXZS3"] = string.Format(md.ToString(), "0");

                        var case1 = IsQualified(MItem[0]["G_HXZS7"], MItem[0]["W_HXZS7"], false) == "合格"
                                    && IsQualified(MItem[0]["G_HXZS2"], MItem[0]["W_HXZS2"], false) == "合格"
                                    && IsQualified(MItem[0]["G_HXZS3"], MItem[0]["W_HXZS3"], false) == "合格";
                        var case2 = IsQualified(MItem[0]["G_HXZS7"], MItem[0]["W_HXZS7"], false) == "不合格"
                                    || IsQualified(MItem[0]["G_HXZS2"], MItem[0]["W_HXZS2"], false) == "不合格"
                                    || IsQualified(MItem[0]["G_HXZS3"], MItem[0]["W_HXZS3"], false) == "不合格";
                        if (case1)
                        {
                            MItem[0]["GH_HXZS"] = "合格";

                        }
                        {
                            if (case2)
                            {
                                MItem[0]["GH_HXZS"] = "不合格";
                            }
                            else { MItem[0]["GH_HXZS"] = "合格"; }
                        }




                    }
                    else { sign = false; }
                }
                else { sign = false; }
                if (!sign)
                {
                    MItem[0]["GH_HXZS"] = "----";
                    MItem[0]["W_HXZS7"] = "----";
                    MItem[0]["W_HXZS2"] = "----";
                    MItem[0]["W_HXZS3"] = "----";
                    MItem[0]["G_HXZS7"] = "----";
                    MItem[0]["G_HXZS2"] = "----";
                    MItem[0]["G_HXZS3"] = "----";
                }
                #endregion

                #region 氯离子
                sign = true;
                
                    if (jcxm.Contains("、氯离子、"))
                    {
                    flag = true;
                    flag = IsNumeric(sItem["LL_HKS"]) ? flag : false;
                    for (int i = 1; i < 3; i++)
                    {
                        flag = IsNumeric(sItem["LL_XHBZ" + i]) ? flag : false;
                        flag = IsNumeric(sItem["LL_KBBZ" + i]) ? flag : false;
                        flag = IsNumeric(sItem["LL_SYZL" + i]) ? flag : false;
                    }
                    sum = 0;
                    if (flag)
                    {
                        sum = 0;
                        pjmd = GetSafeDouble(sItem["LL_HKS"].Trim());
                        for (int i = 1; i < 3; i++)
                        {
                            md1 = GetSafeDouble(sItem["LL_XHBZ" + i].Trim());
                            md2 = GetSafeDouble(sItem["LL_KBBZ" + i].Trim());
                            md = md1 - md2;
                            md = 100 * pjmd * md / (GetSafeDouble(sItem["LL_SYZL" + i].Trim()) * 1000);
                            md = Round(md, 3);
                            sum = sum + md;

                        }
                        pjmd = sum / 2;
                        pjmd = Round(pjmd, 3);
                        MItem[0]["W_LLZ"] = string.Format(pjmd.ToString(), "0.000");
                        MItem[0]["GH_LLZ"] = IsQualified(MItem[0]["G_LLZ"], MItem[0]["W_LLZ"], false);
                    }
                    else { sign = false; }
                }
                else { sign = false; }
                if (!sign)
                {
                    MItem[0]["G_LLZ"] = "----";
                    MItem[0]["W_LLZ"] = "----";
                    MItem[0]["GH_LLZ"] = "----";

                }
                #endregion

                #region 烧失量
                sign = true;
               
                    if (jcxm.Contains("、烧失量、"))
                    {
                    flag = true;
                    flag = IsNumeric(sItem["SS_GGZL"]) ? flag : false;
                    flag = IsNumeric(sItem["SS_GGSY"]) ? flag : false;
                    flag = IsNumeric(sItem["SS_GGZS"]) ? flag : false;
                    if (flag)
                    {
                        md1 = GetSafeDouble(sItem["SS_GGSY"].Trim());
                        md2 = GetSafeDouble(sItem["SS_GGZS"].Trim());
                        md = md1 - md2;
                        md1 = GetSafeDouble(sItem["SS_GGSY"].Trim());
                        md2 = GetSafeDouble(sItem["SS_GGZS"].Trim());
                        md = 100 * md / (md1 - md2);
                        mark = true;
                        mark = IsNumeric(sItem["SS_SHFS"]) ? mark : false;
                        mark = IsNumeric(sItem["SS_SQFS"]) ? mark : false;
                        if (mark)
                        {
                            md1 = GetSafeDouble(sItem["SS_SHFS"].Trim());
                            md2 = GetSafeDouble(sItem["SS_SQFS"].Trim());
                            md = md + 0.8 * (md1 - md2);
                        }
                        md = Round(md, 1);
                        MItem[0]["W_SSL"] = string.Format(md.ToString(), "0.0");
                        MItem[0]["GH_LLZ"] = IsQualified(MItem[0]["G_SSL"], MItem[0]["W_SSL"], false);
                    }
                    else { sign = false; }
                }
                else { sign = false; }
                if (!sign)
                {
                    MItem[0]["G_SSL"] = "----";
                    MItem[0]["W_SSL"] = "----";
                    MItem[0]["GH_SSL"] = "----";

                }
                #endregion

                #region 三氧化硫
                sign = true;
                
                    if (jcxm.Contains("、三氧化硫、"))
                    {
                    flag = true;
                    flag = IsNumeric(sItem["SY_SYZL"]) ? flag : false;
                    flag = IsNumeric(sItem["SY_SYZL"]) ? flag : false;
                    if (flag)
                    {
                        md1 = GetSafeDouble(sItem["SY_SYZL"].Trim());
                        md2 = GetSafeDouble(sItem["SY_ZSZL"].Trim());
                        md = md2 * 34.3 / md1;
                        md = Round(md, 1);
                        MItem[0]["W_SYHL"] = string.Format(md.ToString(), "0.0");
                        MItem[0]["GH_SYHL"] = IsQualified(MItem[0]["G_SYHL"], MItem[0]["W_SYHL"], false);
                    }
                    else { sign = false; }
                }
                else { sign = false; }
                if (!sign)
                {
                    MItem[0]["W_SYHL"] = "----";
                    MItem[0]["G_SYHL"] = "----";
                    MItem[0]["GH_SYHL"] = "----";

                }
                #endregion

                #region 含水率
                sign = true;
               
                    if (jcxm.Contains("、含水率、"))
                    {
                    flag = true;
                    flag = IsNumeric(sItem["HS_HQZL"]) ? flag : false;
                    flag = IsNumeric(sItem["HS_HQZL"]) ? flag : false;
                    if (flag)
                    {
                        md1 = GetSafeDouble(sItem["HS_HQZL"].Trim());
                        md2 = GetSafeDouble(sItem["HS_HQZL"].Trim());
                        md = 100 * (md1 - md2) / md1;
                        md =Round(md, 1);
                        MItem[0]["W_HSL"] = string.Format(md.ToString(), "0.0");
                        MItem[0]["GH_HSL"] = IsQualified(MItem[0]["G_HSL"], MItem[0]["W_HSL"], false);
                    }
                    else { sign = false; }
                }
                else { sign = false; }
                if (!sign)
                {
                    MItem[0]["W_HSL"] = "----";
                    MItem[0]["G_HSL"] = "----";
                    MItem[0]["GH_HSL"] = "----";

                }
                #endregion

                #region 需水量比
                sign = true;
              
                    if (jcxm.Contains("、需水量比、"))
                    {
                    flag = true;
                    flag = IsNumeric(sItem["XS_YSL"]) ? flag : false;
                    if (flag)
                    {
                        md1 = GetSafeDouble(sItem["XS_YSL"].Trim());
                        md = md1 * 100 / 225;
                        md = Round(md, 0);
                        MItem[0]["W_XSLB"] = string.Format(md.ToString(), "0");
                        MItem[0]["GH_XSLB"] = IsQualified(MItem[0]["G_XSLB"], MItem[0]["W_XSLB"], false);
                    }
                    else { sign = false; }
                }
                else { sign = false; }
                if (!sign)
                {
                    MItem[0]["W_XSLB"] = "----";
                    MItem[0]["G_XSLB"] = "----";
                    MItem[0]["GH_XSLB"] = "----";

                }
                #endregion

                #region 二氧化硅
                sign = true;
              
                    if (jcxm.Contains("、二氧化硅、"))
                    {
                    flag = true;
                    flag = IsNumeric(sItem["YHG_GGZL"]) ? flag : false;
                    flag = IsNumeric(sItem["YHG_GGSY"]) ? flag : false;
                    flag = IsNumeric(sItem["YHG_GGCZ"]) ? flag : false;
                    if (flag)
                    {
                        md1 = GetSafeDouble(sItem["YHG_GGSY"].Trim());
                        md2 = GetSafeDouble(sItem["YHG_GGCZ"].Trim());
                        md = GetSafeDouble(sItem["YHG_GGZL"].Trim());
                        md = 100 * (md1 - md2) / (md1 - md);
                        md = Round(md, 0);
                        MItem[0]["W_YHG"] = string.Format(md.ToString(), "0");
                        MItem[0]["GH_YHG"] = IsQualified(MItem[0]["G_YHG"], MItem[0]["W_YHG"], false);
                    }
                    else { sign = false; }
                }
                else { sign = false; }
                if (!sign)
                {
                    MItem[0]["W_YHG"] = "----";
                    MItem[0]["G_YHG"] = "----";
                    MItem[0]["GH_YHG"] = "----";

                }
                #endregion

                #region:mbHggs
                mbHggs = MItem[0]["GH_YHG"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_BBMJ"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_HXZS"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_HSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_SYHL"] == "不合格" ? mbHggs + 1 : mbHggs;

                mbHggs = MItem[0]["GH_LLZ"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_SSL"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_YHM"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_XAZ"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = MItem[0]["GH_XSLB"] == "不合格" ? mbHggs + 1 : mbHggs;
                sItem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                MItem[0]["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                MItem[0]["JCJGMS"] = mbHggs == 0 ? "该样品符合标准要求" : "该样品不符合标准要求";
                #endregion



            }

            #endregion
            #endregion
        }
    }
}
