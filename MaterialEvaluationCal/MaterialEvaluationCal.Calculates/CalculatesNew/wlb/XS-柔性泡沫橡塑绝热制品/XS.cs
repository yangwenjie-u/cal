using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class XS : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_XS_DJ"];
            var extraYZSKB = dataExtra["YZSKB"];

            //var jcxmItems = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            var S_XSS = data["S_XS"];
            if (!data.ContainsKey("M_XS"))
            {
                data["M_XS"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_XS"];
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            List<double> nArr = new List<double>();

            double md1, md2, md3, md, sum, pjmd = 0;
            int mbhggs = 0;//不合格数量
            bool mFlag_Hg = false;//个人认为应该定义为false
            bool mFlag_Bhg = false;//个人认为应该定义为false
            string mJSFF = "";
            double mcbhl1, mcbhl2, mcbhl3 = 0;
            double mkbhl1, mkbhl2, mkbhl3 = 0;
            double mhbhl1, mhbhl2, mhbhl3 = 0;
            double mcd1, mkd1, mhd1 = 0;
            double BGMDv1, BGMDv2, BGMDv3, BGMDv4, BGMDv5 = 0;
            double mcd2, mkd2, mhd2 = 0;
            double mcd3, mkd3, mhd3 = 0;
            double mcd4, mkd4, mhd4 = 0;
            double mcd5, mkd5, mhd5 = 0;
            bool itemHG = true;//单组判断
            int mHggs = 0;//合格数量
            foreach (var sItem in S_XSS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["LB"] == sItem["XH"]);
                if (null == extraFieldsDj)
                {
                    mJSFF = "";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "不合格";
                    continue;
                }
                else
                {
                    MItem[0]["G_DRXS1"] = extraFieldsDj["DRXS1"];
                    MItem[0]["G_DRXS2"] = extraFieldsDj["DRXS2"];
                    MItem[0]["G_DRXS3"] = extraFieldsDj["DRXS3"];

                    MItem[0]["G_BGMD"] = extraFieldsDj["BGMD"];
                    MItem[0]["G_CCWDX"] = extraFieldsDj["CCWDX"];
                    MItem[0]["G_XSL"] = extraFieldsDj["XSL"];
                    MItem[0]["G_RSYZS"] = extraFieldsDj["RSYZS"];
                    MItem[0]["G_RSFJ"] = extraFieldsDj["RSFJ"];
                    MItem[0]["G_YMD"] = extraFieldsDj["YMD"];

                    mJSFF = extraFieldsDj["JSFF"] == null ? "" : extraFieldsDj["JSFF"];
                }

                #region vb跳转代码
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    #region 导热系数(-20℃)
                    if (jcxm.Contains("、导热系数(-20℃)、"))
                    {
                        if (IsNumeric(sItem["DRXS3"]))
                        {
                            sItem["DRXS1"] = sItem["DRXS3"];
                        }
                        int mcd = MItem[0]["G_DRXS1"].Length;
                        int mdwz = MItem[0]["G_DRXS1"].IndexOf(".");
                        mcd = mcd - mdwz + 1;
                        //If InStr(1, mrsmainTable!devcode, "XCS17-067") > 0 Or InStr(1, mrsmainTable!devcode, "XCS17-066") > 0 Then
                        //mrsDrxs.Filter = "sylb='xs' and  sybh='" + mrsmainTable!jydbh + "'"
                        //sitem["DRXS1 = mrsDrxs!drxs
                        //mrsmainTable!Jcyj = Replace(mrsmainTable!Jcyj, "10294", "10295")
                        //End If
                        sItem["DRXS1"] = Math.Round(double.Parse(sItem["DRXS1"]), mcd).ToString();
                        MItem[0]["HG_DRXS1"] = IsQualified(MItem[0]["G_DRXS1"], sItem["DRXS1"], false);
                        if ("不合格" == MItem[0]["HG_DRXS1"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                    else
                    {
                        sItem["DRXS1"] = "----";
                        MItem[0]["hG_DRXS1"] = "----";
                        MItem[0]["G_DRXS1"] = "----";
                    }
                    #endregion

                    #region  导热系数(0℃)
                    if (jcxm.Contains("、导热系数(0℃)、"))
                    {
                        if (IsNumeric(sItem["DRXS3"]))
                        {
                            sItem["DRXS2"] = sItem["DRXS3"];
                        }
                        int mcd = MItem[0]["G_DRXS2"].Length;
                        int mdwz = MItem[0]["G_DRXS2"].IndexOf(".");
                        mcd = mcd - mdwz + 1;
                        //if instr(1, mrsmaintable!devcode, "xcs17-067") > 0 or instr(1, mrsmaintable!devcode, "xcs17-066") > 0 then
                        //mrsdrxs.filter = "sylb='xs' and  sybh='" + mrsmaintable!jydbh + "'"
                        //sitem["DRXS2 = mrsdrxs!drxs
                        //mrsmaintable!jcyj = replace(mrsmaintable!jcyj, "10294", "10295")
                        //end if
                        sItem["DRXS2"] = Math.Round(double.Parse(sItem["DRXS2"]), mcd).ToString();
                        MItem[0]["HG_DRXS2"] = IsQualified(MItem[0]["G_DRXS2"], sItem["DRXS2"], false);
                        if ("不合格" == MItem[0]["HG_DRXS2"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                    else
                    {
                        sItem["DRXS2"] = "----";
                        MItem[0]["HG_DRXS2"] = "----";
                        MItem[0]["G_DRXS2"] = "----";
                    }
                    #endregion

                    #region  导热系数(40℃)
                    if (jcxm.Contains("、导热系数(40℃)、"))
                    {
                        int mcd = MItem[0]["G_DRXS3"].Length;
                        int mdwz = MItem[0]["G_DRXS3"].IndexOf(".");
                        mcd = mcd - mdwz + 1;
                        //If InStr(1, mrsmainTable!devcode, "XCS17-067") > 0 Or InStr(1, mrsmainTable!devcode, "XCS17-066") > 0 Then
                        //mrsDrxs.Filter = "sylb='xs' and  sybh='" + mrsmainTable!jydbh + "'"
                        //sitem["DRXS3 = mrsDrxs!drxs
                        //mrsmainTable!Jcyj = Replace(mrsmainTable!Jcyj, "10294", "10295")
                        //End If
                        sItem["DRXS3"] = Math.Round(double.Parse(sItem["DRXS3"]), mcd).ToString();
                        MItem[0]["HG_DRXS3"] = IsQualified(MItem[0]["G_DRXS3"], sItem["DRXS3"], false);
                        if ("不合格" == MItem[0]["HG_DRXS3"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                    else
                    {
                        sItem["DRXS3"] = "----";
                        MItem[0]["HG_DRXS3"] = "----";
                        MItem[0]["G_DRXS3"] = "----";
                    }
                    #endregion

                    #region  尺寸稳定性
                    if (jcxm.Contains("、尺寸稳定性、"))
                    {
                        MItem[0]["HG_CCWDXC"] = IsQualified(MItem[0]["G_CCWDX"], sItem["CCWDXC"], false);
                        if ("不合格" == MItem[0]["HG_CCWDXC"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                        if ("管" != sItem["WGXZ"])
                        {
                            MItem[0]["HG_CCWDXK"] = IsQualified(MItem[0]["G_CCWDX"], sItem["CCWDXK"], false);
                            if ("不合格" == MItem[0]["HG_CCWDXK"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mHggs++;
                            }
                        }

                        MItem[0]["HG_CCWDXH"] = IsQualified(MItem[0]["G_CCWDX"], sItem["CCWDXH"], false);
                        if ("不合格" == MItem[0]["HG_CCWDXH"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                        MItem[0]["G_CCWDX"] = "105±3℃ 7d, " + MItem[0]["G_CCWDX"];
                        //if ("不合格" == MItem[0]["HG_DRXS3"])
                        //{
                        //    mAllHg = false;
                        //    itemHG = false;
                        //}
                        //else
                        //{
                        //    mHggs++;
                        //}
                    }
                    else
                    {
                        MItem[0]["CCWDXC"] = "----";
                        MItem[0]["CCWDXK"] = "----";
                        MItem[0]["CCWDXH"] = "----";
                        MItem[0]["HG_CCWDXC"] = "----";
                        MItem[0]["HG_CCWDXK"] = "----";
                        MItem[0]["HG_CCWDXH"] = "----";
                        MItem[0]["G_CCWDX"] = "----";
                    }
                    #endregion

                    #region  表观密度
                    if (jcxm.Contains("、表观密度、"))
                    {
                        MItem[0]["HG_BGMD"] = IsQualified(MItem[0]["G_BGMD"], sItem["BGMD"], false);
                        if ("不合格" == MItem[0]["HG_BGMD"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                    else
                    {
                        sItem["BGMD"] = "----";
                        MItem[0]["HG_BGMD"] = "----";
                        MItem[0]["G_BGMD"] = "----";
                    }
                    #endregion


                    #region  烟密度
                    if (jcxm.Contains("、烟密度、"))
                    {
                        MItem[0]["HG_YMD"] = IsQualified(MItem[0]["G_YMD"], sItem["YMD"], false);
                        if ("不合格" == MItem[0]["HG_YMD"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                    else
                    {
                        sItem["YMD"] = "----";
                        MItem[0]["HG_YMD"] = "----";
                        MItem[0]["G_YMD"] = "----";
                    }
                    #endregion

                    #region  吸水率
                    if (jcxm.Contains("、吸水率、"))
                    {
                        MItem[0]["HG_XSL"] = IsQualified(MItem[0]["G_XSL"], sItem["XSL"], false);
                        if ("不合格" == MItem[0]["HG_XSL"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                    else
                    {
                        sItem["XSL"] = "----";
                        MItem[0]["HG_XSL"] = "----";
                        MItem[0]["G_XSL"] = "----";
                    }
                    #endregion

                    #region  燃烧氧指数
                    if (jcxm.Contains("、燃烧氧指数、"))
                    {
                        if ("最小值法" == sItem["YZSSYFF"].Trim())
                        {
                            MItem[0]["G_RSYZS"] = "≥" + sItem["YZSZXZ"];
                            if ("不合格" == MItem[0]["HG_RSYZS"])
                            {
                                sItem["RSYZS"] = "低于" + sItem["YZSZXZ"] + "%";
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                sItem["RSYZS"] = "不低于" + sItem["YZSZXZ"] + "%";
                                mHggs++;
                            }
                        }
                        else
                        {
                            MItem[0]["HG_RSYZS"] = IsQualified(MItem[0]["G_RSYZS"], sItem["RSYZS"], false);
                            if (MItem[0]["HG_RSYZS"] == "不合格")
                            {
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                mHggs++;
                            }
                        }
                    }
                    else
                    {
                        sItem["RSYZS"] = "----";
                        MItem[0]["HG_RSYZS"] = "----";
                        MItem[0]["G_RSYZS"] = "----";
                    }
                    #endregion

                    #region  燃烧分级
                    if (jcxm.Contains("、燃烧分级、"))
                    {
                        sItem["RSFJJG"] = sItem["RSFJ"];
                        MItem[0]["HG_RSFJ"] = sItem["RSFJ"] == "符合" ? "合格" : "不合格";
                        if ("不合格" == MItem[0]["HG_RSFJ"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                    else
                    {
                        sItem["RSFJ"] = "----";
                        sItem["RSFJJG"] = "----";
                        MItem[0]["HG_RSFJ"] = "----";
                        MItem[0]["G_RSFJ"] = "----";
                    }
                    #endregion
                    //单组判断
                    if (itemHG)
                    {
                        sItem["JCJG"] = "合格";
                    }
                    else
                    {
                        sItem["JCJG"] = "不合格";
                    }
                    continue;
                }
                #endregion

                #region 不跳转代码
                #region 初始化
                //sItem["DRXS1"] = "----";
                //MItem[0]["HG_DRXS1"] = "----";
                //MItem[0]["G_DRXS1"] = "----";
                //sItem["DRXS2"] = "----";
                //MItem[0]["HG_DRXS2"] = "----";
                //MItem[0]["G_DRXS2"] = "----";
                //sItem["DRXS3"] = "----";
                //MItem[0]["HG_DRXS3"] = "----";
                //MItem[0]["G_DRXS3"] = "----";
                //sItem["CCWDXC"] = "----";
                //sItem["CCWDXK"] = "----";
                //sItem["CCWDXH"] = "----";
                //MItem[0]["HG_CCWDXC"] = "----";
                //MItem[0]["HG_CCWDXK"] = "----";
                //MItem[0]["HG_CCWDXH"] = "----";
                //MItem[0]["G_CCWDX"] = "----";
                //sItem["BGMD"] = "----";
                //MItem[0]["HG_BGMD"] = "----";
                //MItem[0]["G_BGMD"] = "----";
                //sItem["YMD"] = "----";
                //MItem[0]["HG_YMD"] = "----";
                //MItem[0]["G_YMD"] = "----";
                //sItem["XSL"] = "----";
                //MItem[0]["HG_XSL"] = "----";
                //MItem[0]["G_XSL"] = "----";
                //sItem["RSYSM"] = "----";
                //sItem["RSYZS"] = "----";
                //MItem[0]["HG_RSYZS"] = "----";
                //MItem[0]["G_RSYZS"] = "----";
                //sItem["RSFJ"] = "----";
                //sItem["RSFJJG"] = "----";
                //MItem[0]["HG_RSFJ"] = "----";
                //MItem[0]["G_RSFJ"] = "----";
                #endregion

                //switch (jcxm)
                //{
                //    #region 导热系数(-20℃)
                //    case "导热系数(-20℃)":
                //        if (IsQualified(MItem[0]["G_DRXS1"], sItem["DRXS1"], true) == "符合")
                //        {
                //            //MItem[index]["HG_DRXS1"] = "合格";
                //            mFlag_Hg = true;
                //        }
                //        else
                //        {
                //            //MItem[index]["HG_DRXS1"] = "不合格";
                //            mbhggs++;
                //            mFlag_Bhg = true;
                //        }
                //        break;
                //    #endregion

                //    #region 导热系数(0℃)
                //    case "导热系数(0℃)":
                //        if (IsQualified(MItem[0]["G_DRXS2"], sItem["DRXS2"], true) == "符合")
                //        {
                //            //MItem[0]["HG_DRXS2"] = "合格";
                //            mFlag_Hg = true;
                //        }
                //        else
                //        {
                //            //MItem[0]["HG_DRXS2"] = "不合格";
                //            mbhggs++;
                //            mFlag_Bhg = true;
                //        }
                //        break;
                //    #endregion

                //    #region 导热系数(40℃)
                //    case "导热系数(40℃)":
                //        if (IsQualified(MItem[0]["G_DRXS3"], sItem["DRXS3"], true) == "符合")
                //        {
                //            //MItem[index]["HG_DRXS3"] = "合格";
                //            mFlag_Hg = true;
                //        }
                //        else
                //        {
                //            //MItem[index]["HG_DRXS3"] = "不合格";
                //            mbhggs++;
                //            mFlag_Bhg = true;
                //        }
                //        break;
                //    #endregion

                //    #region 尺寸稳定性
                //    case "尺寸稳定性":
                //        if (sItem["WGXZ"] == "管")
                //        {
                //            sum = 0;
                //            for (int xd = 1; xd < 4; xd++)
                //            {
                //                md = 0;
                //                for (int Gs = 1; Gs < 3; Gs++)
                //                {
                //                    md = md + GetSafeDouble(sItem["CCWDXCQ" + xd + "_" + Gs]);
                //                }
                //                md1 = Math.Round(md / 2, 0);
                //                sItem["CCWDXCQ" + xd] = md1.ToString("0").Trim();

                //                md = 0;
                //                for (int Gs = 1; Gs < 3; Gs++)
                //                {
                //                    md = md + GetSafeDouble(sItem["CCWDXCH" + xd + "_" + Gs]);
                //                }
                //                md2 = Math.Round(md / 2, 0);
                //                sItem["CCWDXCH" + xd] = md2.ToString("0").Trim();

                //                md = Math.Round((md1 - md2) / md1 * 100, 1);
                //                sum = sum + md;
                //            }
                //            md = Math.Round(sum / 3, 1);
                //            sItem["CCWDXC"] = md.ToString("0.0").Trim();
                //            //MItem[0]["WHICH"] = "bgxs_1";
                //        }
                //        else
                //        {
                //            if (GetSafeDouble(sItem["CCWDXCQ1_1"]) != 0)
                //            {
                //                sItem["CCWDXCQ1"] = Math.Round((GetSafeDouble(sItem["CCWDXCQ1_1"]) + GetSafeDouble(sItem["CCWDXCQ1_2"]) + GetSafeDouble(sItem["CCWDXCQ1_3"])) / 3, 1).ToString();
                //                sItem["CCWDXCQ2"] = Math.Round((GetSafeDouble(sItem["CCWDXCQ2_1"]) + GetSafeDouble(sItem["CCWDXCQ2_2"]) + GetSafeDouble(sItem["CCWDXCQ2_3"])) / 3, 1).ToString();
                //                sItem["CCWDXCQ3"] = Math.Round((GetSafeDouble(sItem["CCWDXCQ3_1"]) + GetSafeDouble(sItem["CCWDXCQ3_2"]) + GetSafeDouble(sItem["CCWDXCQ3_3"])) / 3, 1).ToString();
                //                sItem["CCWDXCH1"] = Math.Round((GetSafeDouble(sItem["CCWDXCH1_1"]) + GetSafeDouble(sItem["CCWDXCH1_2"]) + GetSafeDouble(sItem["CCWDXCH1_3"])) / 3, 1).ToString();
                //                sItem["CCWDXCH2"] = Math.Round((GetSafeDouble(sItem["CCWDXCH2_1"]) + GetSafeDouble(sItem["CCWDXCH2_2"]) + GetSafeDouble(sItem["CCWDXCH2_3"])) / 3, 1).ToString();
                //                sItem["CCWDXCH3"] = Math.Round((GetSafeDouble(sItem["CCWDXCH3_1"]) + GetSafeDouble(sItem["CCWDXCH3_2"]) + GetSafeDouble(sItem["CCWDXCH3_3"])) / 3, 1).ToString();
                //            }
                //            mcbhl1 = Math.Round(Math.Abs(GetSafeDouble(sItem["CCWDXCQ1"]) - GetSafeDouble(sItem["CCWDXCH1"])) / GetSafeDouble(sItem["CCWDXCQ1"]) * 100, 1);
                //            mcbhl2 = Math.Round(Math.Abs(GetSafeDouble(sItem["CCWDXCQ2"]) - GetSafeDouble(sItem["CCWDXCH2"])) / GetSafeDouble(sItem["CCWDXCQ2"]) * 100, 1);
                //            mcbhl3 = Math.Round(Math.Abs(GetSafeDouble(sItem["CCWDXCQ3"]) - GetSafeDouble(sItem["CCWDXCH3"])) / GetSafeDouble(sItem["CCWDXCQ3"]) * 100, 1);
                //            sItem["CCWDXC"] = Math.Round((mcbhl1 + mcbhl2 + mcbhl3) / 3, 1).ToString("0.0");

                //            if (GetSafeDouble(sItem["CCWDXKQ1_1"]) != 0)
                //            {
                //                sItem["CCWDXKQ1"] = Math.Round((GetSafeDouble(sItem["CCWDXKQ1_1"]) + GetSafeDouble(sItem["CCWDXKQ1_2"]) + GetSafeDouble(sItem["CCWDXKQ1_3"])) / 3, 1).ToString();
                //                sItem["CCWDXKQ2"] = Math.Round((GetSafeDouble(sItem["CCWDXKQ2_1"]) + GetSafeDouble(sItem["CCWDXKQ2_2"]) + GetSafeDouble(sItem["CCWDXKQ2_3"])) / 3, 1).ToString();
                //                sItem["CCWDXKQ3"] = Math.Round((GetSafeDouble(sItem["CCWDXKQ3_1"]) + GetSafeDouble(sItem["CCWDXKQ3_2"]) + GetSafeDouble(sItem["CCWDXKQ3_3"])) / 3, 1).ToString();
                //                sItem["CCWDXKH1"] = Math.Round((GetSafeDouble(sItem["CCWDXKH1_1"]) + GetSafeDouble(sItem["CCWDXKH1_2"]) + GetSafeDouble(sItem["CCWDXKH1_3"])) / 3, 1).ToString();
                //                sItem["CCWDXKH2"] = Math.Round((GetSafeDouble(sItem["CCWDXKH2_1"]) + GetSafeDouble(sItem["CCWDXKH2_2"]) + GetSafeDouble(sItem["CCWDXKH2_3"])) / 3, 1).ToString();
                //                sItem["CCWDXKH3"] = Math.Round((GetSafeDouble(sItem["CCWDXKH3_1"]) + GetSafeDouble(sItem["CCWDXKH3_2"]) + GetSafeDouble(sItem["CCWDXKH3_3"])) / 3, 1).ToString();
                //            }
                //            mkbhl1 = Math.Round(Math.Abs(GetSafeDouble(sItem["CCWDXKQ1"]) - GetSafeDouble(sItem["CCWDXKH1"])) / GetSafeDouble(sItem["CCWDXKQ1"]) * 100, 1);
                //            mkbhl2 = Math.Round(Math.Abs(GetSafeDouble(sItem["CCWDXKQ2"]) - GetSafeDouble(sItem["CCWDXKH2"])) / GetSafeDouble(sItem["CCWDXKQ2"]) * 100, 1);
                //            mkbhl3 = Math.Round(Math.Abs(GetSafeDouble(sItem["CCWDXKQ3"]) - GetSafeDouble(sItem["CCWDXKH3"])) / GetSafeDouble(sItem["CCWDXKQ3"]) * 100, 1);
                //            sItem["CCWDXK"] = Math.Round((mkbhl1 + mkbhl2 + mkbhl3) / 3, 1).ToString("0.0");

                //            if (IsQualified(MItem[0]["G_CCWDX"], sItem["CCWDXK"], true) == "符合")
                //            {
                //                //MItem[0]["HG_CCWDXK"] = "合格";
                //                mFlag_Hg = true;
                //            }
                //            else
                //            {
                //                //MItem[0]["HG_CCWDXK"] = "不合格";
                //                mbhggs++;
                //                mFlag_Hg = true;
                //            }
                //        }

                //        if (GetSafeDouble(sItem["CCWDXHQ1_1"]) != 0)
                //        {
                //            sItem["CCWDXHQ1"] = Math.Round((GetSafeDouble(sItem["CCWDXHQ1_1"]) + GetSafeDouble(sItem["CCWDXHQ1_2"]) + GetSafeDouble(sItem["CCWDXHQ1_3"]) + GetSafeDouble(sItem["CCWDXHQ1_4"]) + GetSafeDouble(sItem["CCWDXHQ1_5"])) / 5, 1).ToString();
                //            sItem["CCWDXHQ2"] = Math.Round((GetSafeDouble(sItem["CCWDXHQ2_1"]) + GetSafeDouble(sItem["CCWDXHQ2_2"]) + GetSafeDouble(sItem["CCWDXHQ2_3"]) + GetSafeDouble(sItem["CCWDXHQ2_4"]) + GetSafeDouble(sItem["CCWDXHQ2_5"])) / 5, 1).ToString();
                //            sItem["CCWDXHQ3"] = Math.Round((GetSafeDouble(sItem["CCWDXHQ3_1"]) + GetSafeDouble(sItem["CCWDXHQ3_2"]) + GetSafeDouble(sItem["CCWDXHQ3_3"]) + GetSafeDouble(sItem["CCWDXHQ3_4"]) + GetSafeDouble(sItem["CCWDXHQ3_5"])) / 5, 1).ToString();
                //            sItem["CCWDXHH1"] = Math.Round((GetSafeDouble(sItem["CCWDXHH1_1"]) + GetSafeDouble(sItem["CCWDXHH1_2"]) + GetSafeDouble(sItem["CCWDXHH1_3"]) + GetSafeDouble(sItem["CCWDXHH1_4"]) + GetSafeDouble(sItem["CCWDXHH1_5"])) / 5, 1).ToString();
                //            sItem["CCWDXHH2"] = Math.Round((GetSafeDouble(sItem["CCWDXHH2_1"]) + GetSafeDouble(sItem["CCWDXHH2_2"]) + GetSafeDouble(sItem["CCWDXHH2_3"]) + GetSafeDouble(sItem["CCWDXHH2_4"]) + GetSafeDouble(sItem["CCWDXHH2_5"])) / 5, 1).ToString();
                //            sItem["CCWDXHH3"] = Math.Round((GetSafeDouble(sItem["CCWDXHH3_1"]) + GetSafeDouble(sItem["CCWDXHH3_2"]) + GetSafeDouble(sItem["CCWDXHH3_3"]) + GetSafeDouble(sItem["CCWDXHH3_4"]) + GetSafeDouble(sItem["CCWDXHH3_5"])) / 5, 1).ToString();
                //        }
                //        mhbhl1 = Math.Round(Math.Abs(GetSafeDouble(sItem["CCWDXHQ1"]) - GetSafeDouble(sItem["CCWDXHH1"])) / GetSafeDouble(sItem["CCWDXHQ1"]) * 100, 1);
                //        mhbhl2 = Math.Round(Math.Abs(GetSafeDouble(sItem["CCWDXHQ2"]) - GetSafeDouble(sItem["CCWDXHH2"])) / GetSafeDouble(sItem["CCWDXHQ2"]) * 100, 1);
                //        mhbhl3 = Math.Round(Math.Abs(GetSafeDouble(sItem["CCWDXHQ3"]) - GetSafeDouble(sItem["CCWDXHH3"])) / GetSafeDouble(sItem["CCWDXHQ3"]) * 100, 1);

                //        if (IsQualified(MItem[0]["G_CCWDX"], sItem["CCWDXC"], true) == "符合")
                //        {
                //            //MItem[0]["HG_CCWDXC"] = "合格";
                //            mFlag_Hg = true;
                //        }
                //        else
                //        {
                //            //MItem[0]["HG_CCWDXC"] = "不合格";
                //            mFlag_Bhg = true;
                //            mbhggs++;
                //        }

                //        if (IsQualified(MItem[0]["G_CCWDX"], sItem["CCWDXH"], true) == "符合")
                //        {
                //            //MItem[0]["HG_CCWDXH"] = "合格";
                //            mFlag_Hg = true;
                //        }
                //        else
                //        {
                //            //MItem[0]["HG_CCWDXH"] = "不合格";
                //            mFlag_Bhg = true;
                //            mbhggs++;
                //        }
                //        MItem[0]["G_CCWDX"] = "105±3℃ 7d, " + MItem[0]["G_CCWDX"];
                //        break;
                //    #endregion

                //    #region 表观密度
                //    case "表观密度":
                //        if (sItem["WGXZ"] == "管")
                //        {
                //            for (int xd = 1; xd < 6; xd++)
                //            {
                //                md = 0;
                //                for (int Gs = 1; Gs < 3; Gs++)
                //                {
                //                    md = md + GetSafeDouble(sItem["BGMDC" + xd + "_" + Gs]);
                //                }
                //                md1 = Math.Round(md / 2, 0);
                //                sItem["BGMDC" + xd] = md1.ToString("0");

                //                md = 0;
                //                for (int Gs = 1; Gs < 4; Gs++)
                //                {
                //                    md = md + GetSafeDouble(sItem["BGMDK" + xd + "_" + Gs]);
                //                }
                //                md2 = Math.Round(md / 3, 1);
                //                sItem["BGMDK" + xd] = md2.ToString("0.0");

                //                md = 0;
                //                for (int Gs = 1; Gs < 3; Gs++)
                //                {
                //                    md = md + GetSafeDouble(sItem["BGMDH" + xd + "_" + Gs]);
                //                }
                //                md3 = Math.Round(md / 2, 1);
                //                sItem["BGMDH" + xd] = md3.ToString("0.0");
                //                md = Math.Round(md1 * 3.1415926 * Math.Pow((md2 / 2), 2) - Math.Pow(md2 / 2 - md3, 2));
                //                sItem["BGMD" + xd] = Math.Round((GetSafeDouble(sItem["BGMDM" + xd]) / md) * Math.Pow(10, 6), 4).ToString();
                //            }
                //        }
                //        else
                //        {
                //            mcd1 = Math.Round((GetSafeDouble(sItem["BGMDC1_1"]) + GetSafeDouble(sItem["BGMDC1_2"]) + GetSafeDouble(sItem["BGMDC1_3"])) / 3, 1);
                //            mkd1 = Math.Round((GetSafeDouble(sItem["BGMDK1_1"]) + GetSafeDouble(sItem["BGMDK1_2"]) + GetSafeDouble(sItem["BGMDK1_3"])) / 3, 1);
                //            mhd1 = Math.Round((GetSafeDouble(sItem["BGMDH1_1"]) + GetSafeDouble(sItem["BGMDH1_2"]) + GetSafeDouble(sItem["BGMDH1_3"])) / 3, 1);

                //            if (mcd1 == 0 && GetSafeDouble(sItem["BGMDC1"]) > 0)
                //            {
                //            }
                //            else
                //            {
                //                sItem["BGMDC1"] = mcd1.ToString("0.0");
                //                sItem["BGMDK1"] = mkd1.ToString("0.0");
                //                sItem["BGMDH1"] = mhd1.ToString("0.0");
                //            }
                //            BGMDv1 = GetSafeDouble(sItem["BGMDC1"]) * GetSafeDouble(sItem["BGMDK1"]) * GetSafeDouble(sItem["BGMDH1"]);

                //            mcd2 = Math.Round((GetSafeDouble(sItem["BGMDC2_1"]) + GetSafeDouble(sItem["BGMDC2_2"]) + GetSafeDouble(sItem["BGMDC2_3"])) / 3, 1);
                //            mkd2 = Math.Round((GetSafeDouble(sItem["BGMDK2_1"]) + GetSafeDouble(sItem["BGMDK2_2"]) + GetSafeDouble(sItem["BGMDK2_3"])) / 3, 1);
                //            mhd2 = Math.Round((GetSafeDouble(sItem["BGMDH2_1"]) + GetSafeDouble(sItem["BGMDH2_2"]) + GetSafeDouble(sItem["BGMDH2_3"])) / 3, 1);
                //            if (mcd2 >= 0 && GetSafeDouble(sItem["BGMDC2"]) > 0)
                //            {
                //            }
                //            else
                //            {
                //                sItem["BGMDC2"] = mcd2.ToString("0.0");
                //                sItem["BGMDK2"] = mkd2.ToString("0.0");
                //                sItem["BGMDH2"] = mhd2.ToString("0.0");
                //            }

                //            BGMDv2 = Math.Round(GetSafeDouble(sItem["BGMDC2"]) * GetSafeDouble(sItem["BGMDK2"]) * GetSafeDouble(sItem["BGMDH2"]));
                //            mcd3 = Math.Round((GetSafeDouble(sItem["BGMDC3_1"]) + GetSafeDouble(sItem["BGMDC3_2"]) + GetSafeDouble(sItem["BGMDC3_3"])) / 3, 1);
                //            mkd3 = Math.Round((GetSafeDouble(sItem["BGMDK3_1"]) + GetSafeDouble(sItem["BGMDK3_2"]) + GetSafeDouble(sItem["BGMDK3_3"])) / 3, 1);
                //            mhd3 = Math.Round((GetSafeDouble(sItem["BGMDH3_1"]) + GetSafeDouble(sItem["BGMDH3_2"]) + GetSafeDouble(sItem["BGMDH3_3"])) / 3, 1);
                //            if (mcd3 == 0 && GetSafeDouble(sItem["BGMDC3"]) > 0)
                //            {
                //            }
                //            else
                //            {
                //                sItem["BGMDC3"] = mcd3.ToString("0.0");
                //                sItem["BGMDK3"] = mkd3.ToString("0.0");
                //                sItem["BGMDH3"] = mhd3.ToString("0.0");
                //            }
                //            BGMDv3 = Math.Round(GetSafeDouble(sItem["BGMDC3"]) * GetSafeDouble(sItem["BGMDK3"]) * GetSafeDouble(sItem["BGMDH3"]));
                //            mcd4 = Math.Round((GetSafeDouble(sItem["BGMDC4_1"]) + GetSafeDouble(sItem["BGMDC4_2"]) + GetSafeDouble(sItem["BGMDC4_3"])) / 3, 1);
                //            mkd4 = Math.Round((GetSafeDouble(sItem["BGMDK4_1"]) + GetSafeDouble(sItem["BGMDK4_2"]) + GetSafeDouble(sItem["BGMDK4_3"])) / 3, 1);
                //            mhd4 = Math.Round((GetSafeDouble(sItem["BGMDH4_1"]) + GetSafeDouble(sItem["BGMDH4_2"]) + GetSafeDouble(sItem["BGMDH4_3"])) / 3, 1);
                //            if (mcd4 == 0 && GetSafeDouble(sItem["BGMDC4"]) > 0)
                //            {
                //            }
                //            else
                //            {
                //                sItem["BGMDC4"] = mcd4.ToString("0.0");
                //                sItem["BGMDK4"] = mkd4.ToString("0.0");
                //                sItem["BGMDH4"] = mhd4.ToString("0.0");
                //            }
                //            BGMDv4 = Math.Round(GetSafeDouble(sItem["BGMDC4"]) * GetSafeDouble(sItem["BGMDK4"]) * GetSafeDouble(sItem["BGMDH4"]));
                //            mcd5 = Math.Round((GetSafeDouble(sItem["BGMDC5_1"]) + GetSafeDouble(sItem["BGMDC5_2"]) + GetSafeDouble(sItem["BGMDC5_3"])) / 3, 1);
                //            mkd5 = Math.Round((GetSafeDouble(sItem["BGMDK5_1"]) + GetSafeDouble(sItem["BGMDK5_2"]) + GetSafeDouble(sItem["BGMDK5_3"])) / 3, 1);
                //            mhd5 = Math.Round((GetSafeDouble(sItem["BGMDH5_1"]) + GetSafeDouble(sItem["BGMDH5_2"]) + GetSafeDouble(sItem["BGMDH5_3"])) / 3, 1);
                //            if (mcd5 == 0 && GetSafeDouble(sItem["BGMDC5"]) > 0)
                //            {
                //            }
                //            else
                //            {
                //                sItem["BGMDC5"] = mcd5.ToString("0.0");
                //                sItem["BGMDK5"] = mkd5.ToString("0.0");
                //                sItem["BGMDH5"] = mhd5.ToString("0.0");
                //            }
                //            BGMDv5 = Math.Round(GetSafeDouble(sItem["BGMDC5"]) * GetSafeDouble(sItem["BGMDK5"]) * GetSafeDouble(sItem["BGMDH5"]));
                //            sItem["BGMD1"] = Math.Round((GetSafeDouble(sItem["BGMDM1"]) / BGMDv1 * Math.Pow(10, 6)), 4).ToString();
                //            sItem["BGMD2"] = Math.Round((GetSafeDouble(sItem["BGMDM2"]) / BGMDv2 * Math.Pow(10, 6)), 4).ToString();
                //            sItem["BGMD3"] = Math.Round((GetSafeDouble(sItem["BGMDM3"]) / BGMDv3 * Math.Pow(10, 6)), 4).ToString();
                //            sItem["BGMD4"] = Math.Round((GetSafeDouble(sItem["BGMDM4"]) / BGMDv4 * Math.Pow(10, 6)), 4).ToString();
                //            sItem["BGMD5"] = Math.Round((GetSafeDouble(sItem["BGMDM5"]) / BGMDv5 * Math.Pow(10, 6)), 4).ToString();
                //        }

                //        if (GetSafeDouble(sItem["BGMDWD"]) == 23)
                //        {
                //            if (GetSafeDouble(sItem["BGMD1"]) < 15)
                //            {
                //                sItem["BGMD1"] = Math.Round(GetSafeDouble(sItem["BGMD1"]) + 1.22, 4).ToString();
                //            }
                //        }
                //        else
                //        {
                //            if (GetSafeDouble(sItem["BGMD1"]) < 15)
                //            {
                //                sItem["BGMD1"] = Math.Round(GetSafeDouble(sItem["BGMD1"]) + 1.1955, 4).ToString();
                //            }
                //        }
                //        if (GetSafeDouble(sItem["BGMDWD"]) == 23)
                //        {
                //            if (GetSafeDouble(sItem["BGMD2"]) < 15)
                //            {
                //                sItem["BGMD2"] = Math.Round(GetSafeDouble(sItem["BGMD2"]) + 1.22, 4).ToString();
                //            }
                //        }
                //        else
                //        {
                //            if (GetSafeDouble(sItem["BGMD2"]) < 15)
                //            {
                //                sItem["BGMD2"] = Math.Round(GetSafeDouble(sItem["BGMD2"]) + 1.1955, 4).ToString();
                //            }
                //        }
                //        if (GetSafeDouble(sItem["BGMDWD"]) == 23)
                //        {
                //            if (GetSafeDouble(sItem["BGMD3"]) < 15)
                //            {
                //                sItem["BGMD3"] = Math.Round(GetSafeDouble(sItem["BGMD3"]) + 1.22, 4).ToString();
                //            }
                //        }
                //        else
                //        {
                //            if (GetSafeDouble(sItem["BGMD3"]) < 15)
                //            {
                //                sItem["BGMD3"] = Math.Round(GetSafeDouble(sItem["BGMD3"]) + 1.1955, 4).ToString();
                //            }
                //        }

                //        if (GetSafeDouble(sItem["BGMDWD"]) == 23)
                //        {
                //            if (GetSafeDouble(sItem["BGMD4"]) < 15)
                //            {
                //                sItem["BGMD4"] = Math.Round(GetSafeDouble(sItem["BGMD4"]) + 1.22, 4).ToString();
                //            }
                //        }
                //        else
                //        {
                //            if (GetSafeDouble(sItem["BGMD4"]) < 15)
                //            {
                //                sItem["BGMD4"] = Math.Round(GetSafeDouble(sItem["BGMD4"]) + 1.1955, 4).ToString();
                //            }
                //        }

                //        if (GetSafeDouble(sItem["BGMDWD"]) == 23)
                //        {
                //            if (GetSafeDouble(sItem["BGMD5"]) < 15)
                //            {
                //                sItem["BGMD5"] = Math.Round(GetSafeDouble(sItem["BGMD5"]) + 1.22, 4).ToString();
                //            }
                //        }
                //        else
                //        {
                //            if (GetSafeDouble(sItem["BGMD5"]) < 15)
                //            {
                //                sItem["BGMD5"] = Math.Round(GetSafeDouble(sItem["BGMD5"]) + 1.1955, 4).ToString();
                //            }
                //        }

                //        sItem["BGMD"] = Math.Round((GetSafeDouble(sItem["BGMD1"]) + GetSafeDouble(sItem["BGMD2"]) + GetSafeDouble(sItem["BGMD3"]) + GetSafeDouble(sItem["BGMD4"]) + GetSafeDouble(sItem["BGMD5"])) / 5, 1).ToString("0.0");

                //        if (IsQualified(sItem["G_BGMD"], sItem["BGMD"], true) == "符合")
                //        {
                //            //MItem[0]["HG_BGMD"] = "合格";
                //            mFlag_Hg = true;
                //        }
                //        else
                //        {
                //            //MItem[0]["HG_BGMD"] = "不合格";
                //            mFlag_Bhg = true;
                //            mbhggs++;
                //        }
                //        break;
                //    #endregion

                //    #region 烟密度
                //    case "烟密度":
                //        MItem[index]["G_YMC"] = extraFieldsDj["YMD"];
                //        if (IsQualified(MItem[index]["G_YMD"], sItem["YMD"], true) == "符合")
                //        {
                //            //MItem[0]["HG_YMD"] = "合格";
                //            mFlag_Hg = true;
                //        }
                //        else
                //        {
                //            //MItem[0]["HG_YMD"] = "不合格";
                //            mFlag_Bhg = true;
                //            mbhggs++;
                //        }
                //        break;
                //    #endregion

                //    #region 吸水率
                //    case "吸水率":
                //        if (IsQualified(MItem[0]["G_XSL"], sItem["XSL"], true) == "符合")
                //        {
                //            //MItem[0]["HG_XSL"] = "合格";
                //            mFlag_Hg = true;
                //        }
                //        else
                //        {
                //            //MItem[0]["HG_XSL"] = "不合格";
                //            mbhggs++;
                //            mFlag_Bhg = true;
                //        }
                //        break;
                //    #endregion

                //    #region 燃烧氧指数
                //    case "燃烧氧指数":
                //        string mnlfy = "";
                //        if (sItem["RSNLFY1"] != "----")
                //        {
                //            mnlfy = mnlfy + sItem["RSNLFY1"].Trim();
                //        }
                //        if (sItem["RSNLFY2"] != "----")
                //        {
                //            mnlfy = mnlfy + sItem["RSNLFY2"].Trim();
                //        }
                //        if (sItem["RSNLFY3"] != "----")
                //        {
                //            mnlfy = mnlfy + sItem["RSNLFY3"].Trim();
                //        }
                //        if (sItem["RSNLFY4"] != "----")
                //        {
                //            mnlfy = mnlfy + sItem["RSNLFY4"].Trim();
                //        }
                //        string mntfy = "";
                //        if (sItem["RSNTFY1"] != "----")
                //        {
                //            mntfy = mntfy + sItem["RSNTFY1"].Trim();
                //        }
                //        if (sItem["RSNTFY2"] != "----")
                //        {
                //            mntfy = mntfy + sItem["RSNTFY2"].Trim();
                //        }
                //        if (sItem["RSNTFY3"] != "----")
                //        {
                //            mntfy = mntfy + sItem["RSNTFY3"].Trim();
                //        }
                //        if (sItem["RSNTFY4"] != "----")
                //        {
                //            mntfy = mntfy + sItem["RSNTFY4"].Trim();
                //        }
                //        if (sItem["RSNTFY5"] != "----")
                //        {
                //            mntfy = mntfy + sItem["RSNTFY5"].Trim();
                //        }

                //        var extraFieldsYzskb = extraYZSKB.FirstOrDefault(u => u["HWC"] == mntfy && u["QJC"] == mnlfy);
                //        //var extraFieldsYzskb = extraYZSKB.FirstOrDefault(u => u.Keys.Contains("HWC") && u.Values.Contains(mntfy) && u.Keys.Contains("QJC") && u.Values.Contains(mnlfy));
                //        double mkz, moi = 0;
                //        mkz = GetSafeDouble(extraFieldsYzskb["K"]);
                //        if (sItem["RSNTFY1"].Trim() == "0")
                //        {
                //            mkz = -mkz;
                //        }
                //        sItem["RSYZSKZ"] = mkz.ToString("0.00");
                //        moi = Math.Round(GetSafeDouble(sItem["RSNTYND5"]) + mkz * GetSafeDouble(sItem["RSYZSD"]), 2);
                //        sItem["RSYZS"] = Math.Round(Math.Floor(moi * 10) / 10, 1).ToString("0.0");
                //        string mnlzhynd = "";
                //        mnlzhynd = sItem["RSNLYND" + mnlfy.Length.ToString().Trim()];
                //        double mbzc = 0;
                //        mbzc = Math.Sqrt(((GetSafeDouble(mnlzhynd) - moi) * (GetSafeDouble(mnlzhynd) - moi)
                //             + (GetSafeDouble(sItem["RSNTYND1"]) - moi) * (GetSafeDouble(sItem["RSNTYND1"]) - moi)
                //             + (GetSafeDouble(sItem["RSNTYND2"]) - moi) * (GetSafeDouble(sItem["RSNTYND2"]) - moi)
                //             + (GetSafeDouble(sItem["RSNTYND3"]) - moi) * (GetSafeDouble(sItem["RSNTYND3"]) - moi)
                //             + (GetSafeDouble(sItem["RSNTYND4"]) - moi) * (GetSafeDouble(sItem["RSNTYND4"]) - moi)
                //             + (GetSafeDouble(sItem["RSNTYND5"]) - moi) * (GetSafeDouble(sItem["RSNTYND5"]) - moi)) / 5);
                //        sItem["RSBZPC"] = Math.Round(mbzc, 3).ToString("0.000");
                //        sItem["RSYSM"] = "";
                //        if (GetSafeDouble(sItem["RSYZSD"]) == 0.2 && 0.2 > Math.Round(3 / 2 * GetSafeDouble(sItem["RSYZSD"])) || Math.Round(2 / 3 * GetSafeDouble(sItem["RSBZPC"])) < GetSafeDouble(sItem["RSYZSD"]) && GetSafeDouble(sItem["RSYZSD"]) < Math.Round(3 / 2 * GetSafeDouble(sItem["RSBZPC"])))
                //        {
                //            sItem["RSYSM"] = "有效";
                //            if (IsQualified(sItem["G_RSYZS"], sItem["RSYZS"], true) == "符合")
                //            {
                //                //MItem[0]["HG_RSYZS"] = "合格";
                //                mFlag_Hg = true;
                //            }
                //            else
                //            {
                //                //MItem[0]["HG_RSYZS"] = "合格";
                //                mFlag_Bhg = true;
                //                mbhggs++;
                //            }
                //        }
                //        else
                //        {
                //            sItem["RSYSM"] = "无效";
                //        }
                //        break;
                //    #endregion

                //    #region 燃烧分级
                //    case "燃烧分级":
                //        if (sItem["RSFJ"] == "符合")
                //        {
                //            sItem["RSFJJG"] = "符合";
                //            mFlag_Hg = true;
                //        }
                //        else
                //        {
                //            //MItem[0]["HG_RSFJ"] = "不合格";
                //            sItem["RSFJJG"] = "不符合";
                //            mFlag_Bhg = true;
                //            mbhggs++;
                //        }
                //        break;
                //        #endregion
                //}
                #endregion

                //单组判断
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }
            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合标准要求。";
            }
            else
            {
                if (mHggs > 0)
                {
                    jsbeizhu = "该组试样所检项目部分符合标准要求。";
                }
                else
                {
                    jsbeizhu = "该组试样不符合标准要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}