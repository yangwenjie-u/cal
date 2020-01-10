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

                        pbResult = IsQualified(sItem["G_7KYQD"], sItem["PJKY7QD"], true);
                        sign = pbResult == "不符合" ? false : true;
                        pbResult = IsQualified(sItem["G_28KYQD"], sItem["PJKY28QD"], true);
                        sign = pbResult == "不符合" ? false : true;

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

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"] + "、";
                sItem["FJ"] = false.ToString();

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
                    //不要跳转
                    continue;
                }
                mAllHg = sjtabcalc(MItem[0], sItem);
            }

            #region 添加最终报告

            if (mbhggs == 0)
            {
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";

                if (mFlag_Bhg && mFlag_Hg)
                {
                    jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                }

            }

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;


            //mrsmainTable!jsbeizhu = ""
            //            If CDec(mbhggs) = 0 Then
            //              mrsmainTable!jsbeizhu = "该组试样所检项目符合" + mrsmainTable!pdbz + "标准要求。"
            //              sitem["JCJG = "合格"
            //            End If


            //            If CDec(mbhggs) >= 1 Then
            //              mrsmainTable!jsbeizhu = "该组试样不符合" + mrsmainTable!pdbz + "标准要求。"
            //              sitem["JCJG = "不合格"
            //              If(mFlag_Bhg And mFlag_Hg) Then
            //               mrsmainTable!jsbeizhu = "该组试样所检项目部分符合" + mrsmainTable!pdbz + "标准要求。"
            //                End If
            //            End If



            //           mAllHg = IIf(mbhggs > 0, False, mAllHg)
            #endregion
            #endregion
            /************************ 代码结束 *********************/

        }
    }
}
