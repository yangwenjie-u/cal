using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class PZJ2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_PZJ_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_PZJ"];

            if (!data.ContainsKey("M_PZJ"))
            {
                data["M_PZJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_PZJ"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mItemHg = true;
            var jcxm = "";
            string mJSFF = "";




            double mSjcc, mMj, mSjcc1 = 0;
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;
            double md = 0, md1 = 0, md2 = 0, pjmd, Gs, cd1 = 0, cd2 = 0, sum = 0;
            string mSjdjbh, mSjdj = "";
            double mSz, mQdyq, mHsxs, mttjhsxs = 0;
            bool mSFwc = true;
            bool flag = true;
            string pdStr = "";//范围值判断结果 合格/不合格
            mttjhsxs = 1;
            int mbhggs = 0;//不合格数量
            bool mFlag_Hg = true;
            bool mFlag_Bhg = true;
            bool sign = true;
            double[] narr;
            int xd;
            string bl;
            var bhgJcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";

            #region 跳转代码
            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
                delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
                {
                    mbhggs = 0;
                    md1 = 0;
                    md2 = 0;
                    sum = 0;
                    pjmd = 0;
                    flag = false; sign = false;
                    mFlag_Hg = false;
                    mFlag_Bhg = false;
                    List<double> list = new List<double>();

                    var pbResult = "";

                    if (jcxm.Contains("、氧化镁、"))
                    {
                        flag = true;
                        pbResult = IsQualified(sItem["G_YHM"], sItem["W_YHM"], true);
                        switch (pbResult)
                        {
                            case "不符合":
                                sItem["GH_YHM"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                break;
                            case "符合":
                                sItem["GH_YHM"] = "合格";
                                mFlag_Hg = true;
                                break;
                            default:
                                sItem["GH_YHM"] = "----";
                                break;
                        }
                    }
                    else
                    {
                        sItem["G_YHM"] = "----";
                        sItem["GH_YHM"] = "----";
                        sItem["W_YHM"] = "----";

                    }
                    if (jcxm.Contains("、碱含量、"))
                    {
                        flag = true;
                        pbResult = IsQualified(sItem["G_ZJL"], sItem["W_ZJL"], true);
                        switch (pbResult)
                        {
                            case "不符合":
                                sItem["GH_ZJL"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                if (string.IsNullOrEmpty(bhgJcxm))
                                {
                                    bhgJcxm = "碱含量";
                                }
                                else
                                {
                                    bhgJcxm = bhgJcxm + "碱含量";
                                }
                                break;
                            case "符合":
                                sItem["GH_ZJL"] = "合格";
                                mFlag_Hg = true;
                                break;
                            default:
                                sItem["GH_ZJL"] = "----";
                                break;
                        }
                    }
                    else
                    {
                        sItem["G_ZJL"] = "----";
                        sItem["GH_ZJL"] = "----";
                        sItem["W_ZJL"] = "----";
                    }

                    if (jcxm.Contains("、细度、"))
                    {
                        flag = true;
                        sum = 0;
                        list.Clear();
                        // '计算比表面积了
                        for (int i = 1; i < 3; i++)
                        {
                            md1 = Double.Parse(sItem["BBWDBZ"]);
                            md2 = Double.Parse(sItem["BBWDSY" + i]);
                            flag = Math.Abs(md1 - md2) > 3 ? true : false;

                            var ifFlag = (Conversion.Val(sItem["BBMD"]) == Conversion.Val(sItem["SYMD"])) && Conversion.Val(sItem["BBKXBZ"]) == Conversion.Val(sItem["BBKXSY"]);
                            if (ifFlag)
                            {
                                md1 = Conversion.Val(sItem["BBYLBZ"]);
                                md2 = Conversion.Val(sItem["BBYLSY" + i]);
                                md = Conversion.Val(sItem["BBMJBZ"]);
                                md = Math.Pow(md * (md2 / md1), 0.5);
                            }
                            else if (Conversion.Val(sItem["BBMD"]) == Conversion.Val(sItem["SYMD"]) && Conversion.Val(sItem["BBKXBZ"]) != Conversion.Val(sItem["BBKXSY"]))
                            {
                                md1 = Conversion.Val(sItem["BBYLBZ"]);
                                md2 = Conversion.Val(sItem["BBYLSY" + i]);
                                md = Conversion.Val(sItem["BBMJBZ"]);
                                md = Math.Pow(md * (md2 / md1), 0.5);

                                md1 = Conversion.Val(sItem["BBKXBZ"]);
                                md2 = Conversion.Val(sItem["BBKXSY"]);
                                //md = md * (1 - md1) / (1 - md2) * (md2 / md1), 3 / 2);
                                md = md * (1 - md1) / (1 - md2) * Math.Pow((md2 / md1), (3 / 2));
                            }
                            else
                            {

                                md1 = Conversion.Val(sItem["BBYLBZ"]);
                                md2 = Conversion.Val(sItem["BBYLSY" + i]);
                                md = Conversion.Val(sItem["BBMJBZ"]);
                                md = Math.Pow(md * (md2 / md1), 0.5);

                                md1 = Conversion.Val(sItem["BBKXBZ"]);
                                md2 = Conversion.Val(sItem["BBKXSY"]);
                                //md = md * (1 - md1) / (1 - md2) * (md2 / md1), 3 / 2);
                                md = md * (1 - md1) / (1 - md2) * Math.Pow((md2 / md1), (3 / 2));


                                md1 = Conversion.Val(sItem["BBMD"]);
                                md2 = Conversion.Val(sItem["SYMD"]);
                                md = md * md1 / md2;
                            }
                            if (flag)
                            {
                                md1 = Conversion.Val(sItem["BBKQBZ"]);
                                md2 = Conversion.Val(sItem["BBKQSY" + i]);
                                md = Math.Pow(md * (md2 / md1), 0.5);
                            }

                            md = Math.Round(md, 0);
                            list.Add(md);
                            sum += md;

                        }
                        pjmd = Math.Round(sum / 2, 0);
                        pjmd = Math.Round(pjmd / 10, 0) * 10;
                        sItem["BBMJSY"] = pjmd.ToString("0");

                        md = Math.Abs(list[0] - list[1]);

                        if (md / list[0] > 0.02 || md / list[1] > 0.02)
                        {
                            sItem["BBMJSY"] = "重做";
                        }
                        pbResult = IsQualified(sItem["G_XDBM"], sItem["BBMJSY"], true);

                        sign = pbResult == "不符合" ? true : false;

                        pbResult = IsQualified(sItem["G_XD118"], sItem["S5PJXD"], true);
                        sign = pbResult == "不符合" ? false : sign;


                        //'总括
                        sItem["GH_XD"] = sign ? "合格" : "不合格";
                        mbhggs = sign ? mbhggs : mbhggs + 1;

                        if (sign)
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["G_XDBM"] = "----";
                        sItem["BBMJSY"] = "----";
                        sItem["GH_XD"] = "----";
                        sItem["G_XD118"] = "----";
                        sItem["S5PJXD"] = "----";
                    }

                    if (jcxm.Contains("、凝结时间、"))
                    {
                        sign = true;
                        pbResult = IsQualified(sItem["G_CNSJ"], sItem["CNSJ"], true);
                        sign = pbResult == "不符合" ? false : true;
                        pbResult = IsQualified(sItem["G_ZNSJ"], sItem["ZNSJ"], true);
                        sign = pbResult == "不符合" ? false : true;

                        sItem["GH_NJSJ"] = sign ? mbhggs.ToString() : (mbhggs + 1).ToString();
                        if (sign)
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sItem["G_CNSJ"] = "----";
                        sItem["CNSJ"] = "----";
                        sItem["G_ZNSJ"] = "----";
                        sItem["ZNSJ"] = "----";
                        sItem["GH_NJSJ"] = "----";
                    }


                    if (jcxm.Contains("、限制膨胀率、"))
                    {
                        sign = true;
                        pbResult = IsQualified(sItem["G_7PZL"], sItem["PJ7PZL"], true);
                        sign = pbResult == "不符合" ? false : true;
                        pbResult = IsQualified(sItem["G_21PZL"], sItem["PJ21PZL"], true);
                        sign = pbResult == "不符合" ? false : true;

                        sItem["GH_PZL"] = sign ? "合格" : "不合格";
                        if (sign)
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sItem["G_7PZL"] = "----";
                        sItem["G_21PZL"] = "----";
                        sItem["GH_PZL"] = "----";
                        sItem["PJ7PZL"] = "----";
                        sItem["PJ21PZL"] = "----";
                    }

                    if (jcxm.Contains("、7d抗压强度、"))
                    {
                        sItem["GH_7KYQD"] = IsQualified(sItem["G_7KYQD"], sItem["PJKY7QD"], false);
                    }
                    else
                    {
                        sItem["G_7KYQD"] = "----";
                        sItem["GH_7KYQD"] = "----";
                        sItem["PJKY7QD"] = "----";
                    }

                    if (jcxm.Contains("、28d抗压强度、"))
                    {
                        sItem["GH_7KYQD"] = IsQualified(sItem["G_28KYQD"], sItem["PJKY28QD"], false);
                    }
                    else
                    {
                        sItem["G_28KYQD"] = "----";
                        sItem["PJKY28QD"] = "----";
                        sItem["GH_28KYQD"] = "----";
                    }
                    //'总括抗压强度
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        sign = true;

                        if (IsNumeric(sItem["PJKY7QD"]) || IsNumeric(sItem["PJKY28QD"]))
                        {
                            pbResult = IsQualified(sItem["G_7KYQD"], sItem["PJKY7QD"], true);
                            sign = pbResult == "不符合" ? false : true;
                            pbResult = IsQualified(sItem["G_28KYQD"], sItem["PJKY28QD"], true);
                            sign = pbResult == "不符合" ? false : true;
                        }
                        else
                        {
                            sign = false;
                        }
                        sItem["GH_KYQD"] = sign ? "合格" : "不合格";
                        mbhggs = sign ? mbhggs : mbhggs + 1;

                        if (sign)
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sItem["GH_KYQD"] = "----";
                    }

                    //单条数据是否合格
                    sItem["JCJG"] = mFlag_Hg ? "合格" : "不合格";

                    return mbhggs > 0 ? false : true;
                };
            #endregion

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                //sItem["FJ"] = false.ToString();

                var mrsDj = extraDJ.FirstOrDefault(u => u["LX"] == sItem["LX"]);

                if (null == mrsDj)
                {
                    jsbeizhu = "试件尺寸为空\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                else
                {
                    sItem["G_XDBM"] = mrsDj["XD1"];
                    sItem["G_XD118"] = mrsDj["XD2"];
                    sItem["G_CNSJ"] = mrsDj["CNSJ"];
                    sItem["G_ZNSJ"] = mrsDj["ZNSJ"];
                    sItem["G_7PZL"] = mrsDj["XZPZLS7D"];
                    sItem["G_21PZL"] = mrsDj["XZPZLK21D"];
                    sItem["G_7KYQD"] = mrsDj["KYQD7D"];
                    sItem["G_28KYQD"] = mrsDj["KYQD28D"];
                    sItem["G_YHM"] = mrsDj["YHM"];
                    sItem["G_ZJL"] = mrsDj["JHL"];
                    mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].ToLower();
                }

                var sjtabs = MItem[0]["SJTABS"];
                if (string.IsNullOrEmpty(sjtabs))
                {
                    #region 氧化镁
                    if (jcxm.Contains("、氧化镁、"))
                    {
                        jcxmCur = "氧化镁";
                        flag = true;
                        string is_fh = IsQualified(sItem["G_YHM"], sItem["W_YHM"], true);
                        switch (is_fh)
                        {
                            case "不符合":
                                sItem["GH_YHM"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                break;
                            case "符合":
                                sItem["GH_YHM"] = "合格";
                                mFlag_Hg = true;
                                break;
                            default:
                                sItem["GH_YHM"] = "----";
                                break;
                        }
                    }
                    else
                    {
                        sItem["G_YHM"] = "----";
                        sItem["GH_YHM"] = "----";
                        sItem["W_YHM"] = "----";
                    }
                    #endregion

                    #region 碱含量
                    if (jcxm.Contains("、碱含量、"))
                    {
                        jcxmCur = "碱含量";
                        flag = true;
                        string is_fh = IsQualified(sItem["G_ZJL"], sItem["W_ZJL"], true);
                        switch (is_fh)
                        {
                            case "不符合":
                                sItem["GH_ZJL"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                break;
                            case "符合":
                                sItem["GH_ZJL"] = "合格";
                                mFlag_Hg = true;
                                break;
                            default:
                                sItem["GH_ZJL"] = "----";
                                break;
                        }
                    }
                    else
                    {
                        sItem["G_ZJL"] = "----";
                        sItem["GH_ZJL"] = "----";
                        sItem["W_ZJL"] = "----";
                    }
                    #endregion

                    #region 细度
                    if (jcxm.Contains("、细度、"))
                    {
                        jcxmCur = "细度";
                        sign = true;
                        sum = 0;
                        narr = new double[3];
                        //初始化
                        sItem["BBMJSY"] = "----";
                        sItem["GH_XD"] = "----";
                        sItem["S5PJXD"] = "----";
                        //计算比表面积了
                        sum = 0;
                        for (xd = 1; xd <= 2; xd++)
                        {
                            md1 = Conversion.Val(sItem["BBWDBZ"]);
                            md2 = Conversion.Val(sItem["BBWDSY" + xd]);
                            flag = Math.Abs(md1 - md2) > 3 ? true : false;

                            if (Conversion.Val(sItem["BBMD"]) == Conversion.Val(sItem["SYMD"]) && Conversion.Val(sItem["BBKXBZ"]) == Conversion.Val(sItem["BBKXSY"]))
                            {
                                md1 = Conversion.Val(sItem["BBYLBZ"]);
                                md2 = Conversion.Val(sItem["BBYLSY" + xd]);
                                md = Conversion.Val(sItem["BBMJBZ"]);
                                //md = Math.Pow(md * (md2 / md1), (1 / 2));
                                md = md * Math.Pow((md2 / md1), (1 / 2));
                            }
                            else if (Conversion.Val(sItem["BBMD"]) == Conversion.Val(sItem["SYMD"]) && Conversion.Val(sItem["BBKXBZ"]) != Conversion.Val(sItem["BBKXSY"]))
                            {
                                md1 = Conversion.Val(sItem["BBYLBZ"]);
                                md2 = Conversion.Val(sItem["BBYLSY" + xd]);
                                md = Conversion.Val(sItem["BBMJBZ"]);
                                //md = Math.Pow(md * (md2 / md1), (1 / 2));
                                md = md * Math.Pow((md2 / md1), (1 / 2));
                                md1 = Conversion.Val(sItem["BBKXBZ"]);
                                md2 = Conversion.Val(sItem["BBKXSY"]);
                                //md = Math.Pow(md * (1 - md1) / (1 - md2) * (md2 / md1), (3 / 2));
                                md = md * (1 - md1) / (1 - md2) * Math.Pow((md2 / md1), (3 / 2));
                            }
                            else
                            {
                                md1 = Conversion.Val(sItem["BBYLBZ"]);
                                md2 = Conversion.Val(sItem["BBYLSY" + xd]);
                                md = Conversion.Val(sItem["BBMJBZ"]);
                                //md = Math.Pow(md * (md2 / md1), (1 / 2));
                                md = md * Math.Pow((md2 / md1), (1 / 2));
                                md1 = Conversion.Val(sItem["BBKXBZ"]);
                                md2 = Conversion.Val(sItem["BBKXSY"]);
                                //md = Math.Pow(md * (1 - md1) / (1 - md2) * (md2 / md1), (3 / 2));
                                md = md * (1 - md1) / (1 - md2) * Math.Pow((md2 / md1), (3 / 2));
                                md1 = Conversion.Val(sItem["BBMD"]);
                                md2 = Conversion.Val(sItem["SYMD"]);
                                md = md * md1 / md2;
                            }
                            if (flag)
                            {
                                md1 = Conversion.Val(sItem["BBKQBZ"]);
                                md2 = Conversion.Val(sItem["BBKQSY" + xd]);
                                //md = Math.Pow(md * (md1 / md2), (1 / 2));
                                md = md * Math.Pow((md1 / md2), (1 / 2));
                            }
                            md = Round(md, 0);
                            narr[xd] = md;
                            sum = sum + md;
                        }
                        pjmd = Round((sum / 2), 0);
                        pjmd = Round((pjmd / 10), 0) * 10;
                        sItem["BBMJSY"] = pjmd.ToString("0");
                        md = Math.Abs(narr[1] - narr[2]);
                        if (Conversion.Val(md / narr[1]) > 0.02 || Conversion.Val(md / narr[2]) > 0.02)
                            sItem["BBMJSY"] = "重做";
                        if (IsQualified(sItem["G_XDBM"], sItem["BBMJSY"], true) == "不符合")
                            sign = false;
                        else
                            sign = sign;
                        //计算1.18mm筛了
                        sum = 0;
                        if (!IsNumeric(sItem["S5XZXS"].Trim()))
                            sItem["S5XZXS"] = "1";
                        for (xd = 1; xd <= 2; xd++)
                        {
                            md1 = Conversion.Val(sItem["SY5ZL" + xd]);
                            md2 = Conversion.Val(sItem["YP5ZL" + xd]);
                            md = 100 * md1 / md2;
                            md = md * Conversion.Val(sItem["S5XZXS"]);
                            md = Round((md), 1);
                            sItem["S5XD" + xd] = md.ToString("0.0");
                            sum = sum + md;
                        }
                        pjmd = sum / 2;
                        pjmd = Round(pjmd, 1);
                        sItem["S5PJXD"] = pjmd.ToString("0.0");
                        sign = IsQualified(sItem["G_XD118"], sItem["S5PJXD"], true) == "不符合" ? false : sign;
                        //总括
                        sItem["GH_XD"] = sign ? "合格" : "不合格";
                        mbhggs = sign ? mbhggs : mbhggs + 1;
                        if (sign)
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                            

                    }
                    else
                    {
                        sItem["G_XDBM"] = "----";
                        sItem["BBMJSY"] = "----";
                        sItem["GH_XD"] = "----";
                        sItem["G_XD118"] = "----";
                        sItem["S5PJXD"] = "----";
                    }
                    #endregion

                    #region 凝结时间
                    if (jcxm.Contains("、凝结时间、"))
                    {
                        jcxmCur = "凝结时间";
                        flag = true;
                        sign = true;
                        flag = !string.IsNullOrEmpty(sItem["JSSJH"]) && IsNumeric(sItem["JSSJH"].Trim()) ? flag : false;
                        flag = !string.IsNullOrEmpty(sItem["JSSJM"]) && IsNumeric(sItem["JSSJM"].Trim()) ? flag : false;
                        flag = !string.IsNullOrEmpty(sItem["CNSJH"]) && IsNumeric(sItem["CNSJH"].Trim()) ? flag : false;
                        flag = !string.IsNullOrEmpty(sItem["CNSJM"]) && IsNumeric(sItem["CNSJM"].Trim()) ? flag : false;
                        flag = !string.IsNullOrEmpty(sItem["ZNSJH"]) && IsNumeric(sItem["ZNSJH"].Trim()) ? flag : false;
                        flag = !string.IsNullOrEmpty(sItem["ZNSJM"]) && IsNumeric(sItem["ZNSJM"].Trim()) ? flag : false;
                        if (flag)
                        {
                            sum = 0;
                            md = 60 * Conversion.Val(sItem["JSSJH"]) + Conversion.Val(sItem["JSSJM"]);
                            md = Round(md, 0);
                            md1 = 60 * Conversion.Val(sItem["CNSJH"]) + Conversion.Val(sItem["CNSJM"]);
                            md1 = Round(md1, 0);
                            md2 = 60 * Conversion.Val(sItem["ZNSJH"]) + Conversion.Val(sItem["ZNSJM"]);
                            md2 = Round(md2, 0);
                            md1 = md1 - md;
                            md2 = md2 - md;
                            md1 = Round(md1, 0);
                            md2 = Round(md2, 0);
                            sItem["CNSJ"] = md1.ToString("0");
                            sItem["ZNSJ"] = md2.ToString("0");
                            sign = IsQualified(sItem["G_CNSJ"], sItem["CNSJ"], true) == "不符合" ? false : sign;
                            sign = IsQualified(sItem["G_ZNSJ"], sItem["ZNSJ"], true) == "不符合" ? false : sign;
                            sItem["GH_NJSJ"] = sign ? "合格" : "不合格";
                            mbhggs = sign ? mbhggs : mbhggs + 1;
                            if (sign)
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }
                        }
                        else
                            mSFwc = false;
                    }
                    else
                    {
                        sItem["G_CNSJ"] = "----";
                        sItem["CNSJ"] = "----";
                        sItem["G_ZNSJ"] = "----";
                        sItem["ZNSJ"] = "----";
                        sItem["GH_NJSJ"] = "----";
                    }
                    #endregion

                    #region 限制膨胀率
                    if (jcxm.Contains("、限制膨胀率、"))
                    {
                        jcxmCur = "限制膨胀率";
                        List<double> day7Array = new List<double>();
                        List<double> day21Array = new List<double>();
                        flag = true;
                        sign = true;
                        //初始化
                        sItem["GH_PZL"] = "----";
                        sItem["PJ7PZL"] = "----";
                        sItem["PJ21PZL"] = "----";
                        //水中7d
                        for (xd = 1; xd <= 3; xd++)
                        {
                            flag = IsNumeric(sItem["P7CS" + xd].Trim()) ? flag : false;
                            flag = IsNumeric(sItem["P7XZ" + xd].Trim()) ? flag : false;
                        }
                        if (flag)
                        {
                            sum = 0;
                            for (xd = 1; xd <= 3; xd++)
                            {
                                md1 = Conversion.Val(sItem["P7XZ" + xd].Trim());
                                md2 = Conversion.Val(sItem["P7CS" + xd].Trim());
                                //限制膨胀率% = （所测龄期的试体长度测量值mm - 试体的初始长度mm）/试体的基准长度（144mm）
                                md = 100 * (md1 - md2) / 140;
                                md = Round(md, 3);
                                sItem["P7ZL" + xd] = md.ToString("0.000");
                                day7Array.Add(GetSafeDouble(sItem["P7ZL" + xd]));
                                //sum = sum + md;
                            }
                            day7Array.Sort();
                            //取相近的两个试体测定值的平均值作为限制膨胀率的测量结果，精确到0.001%
                            //最大最小值与中间值的差值绝对值比较
                            if (Math.Abs(day7Array[0] - day7Array[1]) > Math.Abs(day7Array[2] - day7Array[1]))
                            {
                                if (Math.Abs(day7Array[2] - day7Array[1]) > Math.Abs(day7Array[0] - day7Array[2]))
                                {
                                    pjmd = Round((day7Array[2] + day7Array[0]) / 2, 3);
                                }
                                else
                                {
                                    pjmd = Round((day7Array[1] + day7Array[2]) / 2, 3);
                                }
                                
                            }
                            else
                            {
                                if (Math.Abs(day7Array[0] - day7Array[1]) > Math.Abs(day7Array[0] - day7Array[2]))
                                {
                                    pjmd = Round((day7Array[0] + day7Array[2]) / 2, 3);
                                }
                                else
                                {
                                    pjmd = Round((day7Array[1] + day7Array[1]) / 2, 3);
                                }
                                
                            }
                            //pjmd = sum / 2;
                            //pjmd = Round(pjmd, 3);
                            sItem["PJ7PZL"] = pjmd.ToString("0.000");
                            sign = IsQualified(sItem["G_7PZL"], sItem["PJ7PZL"], true) == "不符合" ? false : sign;
                        }
                        else
                            mSFwc = false;
                        //空气中21d
                        for (xd = 1; xd <= 3; xd++)
                        {
                            flag = IsNumeric(sItem["P7CS" + xd].Trim()) ? flag : false;
                            flag = IsNumeric(sItem["P21XZ" + xd].Trim()) ? flag : false;
                        }
                        if (flag)
                        {
                            sum = 0;
                            for (xd = 1; xd <= 3; xd++)
                            {
                                md1 = Conversion.Val(sItem["P21XZ" + xd].Trim());
                                md2 = Conversion.Val(sItem["P7CS" + xd].Trim());
                                md = 100 * (md1 - md2) / 140;
                                md = Round(md, 3);
                                sItem["P21ZL" + xd] = md.ToString("0.000");
                                day21Array.Add(GetSafeDouble(sItem["P21ZL" + xd]));
                                //sum = sum + md;
                            }
                            day21Array.Sort();
                            //取相近的两个试体测定值的平均值作为限制膨胀率的测量结果，精确到0.001%
                            //最大最小值与中间值的差值绝对值比较
                            if (Math.Abs(day21Array[0] - day21Array[1]) > Math.Abs(day21Array[2] - day21Array[1]))
                            {
                                if (Math.Abs(day21Array[2] - day21Array[1]) > Math.Abs(day21Array[0] - day21Array[2]))
                                {
                                    pjmd = Round((day21Array[0] + day21Array[2]) / 2, 3);
                                }
                                else
                                {
                                    pjmd = Round((day21Array[2] + day21Array[1]) / 2, 3);
                                }
                               
                            }
                            else
                            {
                                if (Math.Abs(day21Array[0] - day21Array[1]) > Math.Abs(day21Array[0] - day21Array[2]))
                                {
                                    pjmd = Round((day21Array[0] + day21Array[2]) / 2, 3);
                                }
                                else
                                {
                                    pjmd = Round((day21Array[0] + day21Array[1]) / 2, 3);
                                }
                                
                            }
                            //pjmd = sum / 2;
                            //pjmd = Round(pjmd, 3);
                            sItem["PJ21PZL"] = pjmd.ToString("0.000");
                            sign = IsQualified(sItem["G_21PZL"], sItem["PJ21PZL"], true) == "不符合" ? false : sign;
                        }
                        else
                            mSFwc = false;
                        //总括
                        sItem["GH_PZL"] = sign ? "合格" : "不合格";
                        mbhggs = sign ? mbhggs : mbhggs + 1;
                        if (sign)
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        sItem["G_7PZL"] = "----";
                        sItem["G_21PZL"] = "----";
                        sItem["GH_PZL"] = "----";
                        sItem["PJ7PZL"] = "----";
                        sItem["PJ21PZL"] = "----";
                    }
                    #endregion

                    #region 7d抗压强度
                    //试件规格为40 * 40 * 160
                    if (jcxm.Contains("、7d抗压强度、"))
                    {
                        jcxmCur = "7d抗压强度";
                        flag = true;
                        //单位是kN
                        for (xd = 1; xd <= 6; xd++)
                            flag = IsNumeric(sItem["KY7HZ" + xd].Trim()) ? flag : false;
                        narr = new double[7];
                        if (flag)
                        {
                            sum = 0;
                            for (xd = 1; xd <= 6; xd++)
                            {
                                md1 = Conversion.Val(sItem["KY7HZ" + xd].Trim());
                                md = 10 * md1 / 16;
                                md = Round(md, 1);
                                sItem["KY7QD" + xd] = md.ToString("0.0");
                                narr[xd] = md;
                                sum = sum + md;
                            }
                            pjmd = sum / 6;
                            pjmd = Round(pjmd, 1);
                            Gs = 0;
                            sum = 0;
                            for (xd = 1; xd <= 6; xd++)
                            {
                                md = 100 * Math.Abs(narr[xd] - pjmd) / pjmd;
                                md = Round(md, 0);
                                if (md <= 10)
                                {
                                    Gs = Gs + 1;
                                    sum = sum + narr[xd];
                                }
                            }
                            if (Gs >= 5)
                            {
                                pjmd = sum / Gs;
                                pjmd = Round(pjmd, 1);
                                sItem["PJKY7QD"] = pjmd.ToString("0.0");
                                sign = IsQualified(sItem["G_7KYQD"], sItem["PJKY7QD"], true) == "不符合" ? false : sign;
                                if (sign)
                                {
                                    sItem["GH_7KYQD"] = "合格";
                                }
                                else
                                {
                                    sItem["GH_7KYQD"] = "不合格";
                                    mbhggs++;
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                }

                            }
                            else
                            {
                                sItem["PJKY7QD"] = "重做";
                            }
                                
                        }
                        else
                            mSFwc = false;
                    }
                    else
                    {
                        sItem["GH_7KYQD"] = "----";
                        sItem["G_7KYQD"] = "----";
                        sItem["PJKY7QD"] = "----";
                    }
                    #endregion

                    #region 28d抗压强度
                    if (jcxm.Contains("、28d抗压强度、"))
                    {
                        jcxmCur = "28d抗压强度";
                        flag = true;
                        //单位是kN
                        for (xd = 1; xd <= 6; xd++)
                            flag = IsNumeric(sItem["KY28HZ" + xd].Trim()) ? flag : false;
                        narr = new double[7];
                        if (flag)
                        {
                            sum = 0;
                            for (xd = 1; xd <= 6; xd++)
                            {
                                md1 = Conversion.Val(sItem["KY28HZ" + xd].Trim());
                                md = 10 * md1 / 16;
                                md = Round(md, 1);
                                sItem["KY28QD" + xd] = md.ToString("0.0");
                                narr[xd] = md;
                                sum = sum + md;
                            }
                            pjmd = sum / 6;
                            pjmd = Round(pjmd, 1);
                            Gs = 0;
                            sum = 0;
                            for (xd = 1; xd <= 6; xd++)
                            {
                                md = 100 * Math.Abs(narr[xd] - pjmd) / pjmd;
                                md = Round(md, 0);
                                if (md <= 10)
                                {
                                    Gs = Gs + 1;
                                    sum = sum + narr[xd];
                                }
                            }
                            if (Gs >= 5)
                            {
                                pjmd = sum / Gs;
                                pjmd = Round(pjmd, 1);
                                sItem["PJKY28QD"] = pjmd.ToString("0.0");
                                sign = IsQualified(sItem["G_28KYQD"], sItem["PJKY28QD"], true) == "不符合" ? false : sign;
                                if (sign)
                                {
                                    sItem["GH_28KYQD"] = "合格";
                                }
                                else
                                {
                                    sItem["GH_28KYQD"] = "不合格";
                                    mbhggs++;
                                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                }
                            }
                            else
                            {
                                sItem["PJKY28QD"] = "重做";
                            }
                        }
                        else
                        {
                            mSFwc = false;
                        }
                    }
                    else
                    {
                        sItem["GH_28KYQD"] = "----";
                        sItem["G_28KYQD"] = "----";
                        sItem["PJKY28QD"] = "----";
                    }
                    #endregion

                    #region
                    //总括抗压强度
                    //if (jcxm.Contains("抗压强度"))
                    //{
                    //    sign = true;
                    //    if (IsNumeric(sItem["PJKY7QD"]) || IsNumeric(sItem["PJKY28QD"]))
                    //    {
                    //        sign = IsQualified(sItem["G_7KYQD"], sItem["PJKY7QD"], true) == "不符合" ? false : sign;
                    //        sign = IsQualified(sItem["G_28KYQD"], sItem["PJKY28QD"], true) == "不符合" ? false : sign;
                    //    }
                    //    else
                    //    {
                    //        sign = false;
                    //    }
                    //    sItem["GH_KYQD"] = sign ? "合格" : "不合格";
                    //    mbhggs = sign ? mbhggs : mbhggs + 1;
                    //    if (sign)
                    //        mFlag_Hg = true;
                    //    else
                    //        mFlag_Bhg = true;
                    //}
                    //else
                    //{
                    //    sItem["GH_KYQD"] = "----";
                    //}
                    #endregion

                    MItem[0]["JCJGMS"] = "";
                    if (mbhggs == 0)
                    {
                        MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        sItem["JCJG"] = "合格";
                    }
                    if (mbhggs >= 1)
                    {
                        MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。"; 
                        //MItem[0]["JCJGMS"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                        //sItem["JCJG"] = "不合格";
                        //if (mFlag_Bhg && mFlag_Hg)
                        //    MItem[0]["JCJGMS"] = "该组试样所检项目"+bhgJcxm+"不符合" + MItem[0]["PDBZ"] + "标准要求。";
                    }

                    mAllHg = (mAllHg && sItem["JCJG"] == "合格");
                }
                else
                    mAllHg = sjtabcalc(MItem[0], sItem);
            }

            #region 添加最终报告
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            #endregion
            #endregion
            /************************ 代码结束 *********************/

        }
    }
}
