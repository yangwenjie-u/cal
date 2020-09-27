using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JWA2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_JWA_DJ"];

            var data = retData;
            var mJCJG = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_JWA"];
            var yzskb = dataExtra["BZ_YZSKB"];

            if (!data.ContainsKey("M_JWA"))
            {
                data["M_JWA"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_JWA"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mJCJG;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";

            bool sign = true;
            List<double> nArr = new List<double>();
            int mcd, mdwz = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            var jcxmBhg = "";
            var jcxmCur = "";

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mFlag_Hg = true;
                sign = true;
                if (jcxm.Contains("、芯密度、"))
                {
                    jcxmCur = "芯密度";
                    sign = true;
                    sign = IsNumeric(sItem["XMD"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sign = IsQualified(sItem["G_XMD"], sItem["XMD"], false) == "合格" ? sign : false;
                        sItem["HG_XMD"] = sign ? "合格" : "不合格";

                        if (sItem["HG_XMD"] == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sItem["XMD"] = "----";
                    sItem["HG_XMD"] = "----";
                    sItem["G_XMD"] = "----";
                }

                if (jcxm.Contains("、压缩强度、"))
                {
                    jcxmCur = "压缩强度";
                    sign = true;
                    sign = IsNumeric(sItem["YSQD"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sign = IsQualified(sItem["G_YSQD"], sItem["YSQD"], false) == "合格" ? sign : false;
                        sItem["HG_YSQD"] = sign ? "合格" : "不合格";

                        if (sItem["HG_YSQD"] == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sItem["YSQD"] = "----";
                    sItem["HG_YSQD"] = "----";
                    sItem["G_YSQD"] = "----";
                }


                if (jcxm.Contains("、导热系数(23℃)、"))
                {
                    jcxmCur = "导热系数(23℃)";
                    sign = true;
                    sign = IsNumeric(sItem["DRXS1"]) ? sign : false;

                    mcd = sItem["G_DRXS"].Length;
                    mdwz = sItem["G_DRXS"].IndexOf('.');
                    mcd = mcd - mdwz + 1;

                    string DEVCODE = String.IsNullOrEmpty(mItem["DEVCODE"]) ? "" : mItem["DEVCODE"];
                    if (DEVCODE == "" && DEVCODE.Contains("XCS17-067") || DEVCODE.Contains("XCS17-066"))
                    {
                        //var mrsDrxs = YZSKB.FirstOrDefault(u => u["SYLB"] == "jww" && u["SYBH"] == mItem["JYDBH"]);
                        //sItem["DRXS"] = mrsDrxs["DRXS"];
                        //mItem["JCYJ"] = mItem["JCYJ"].Replace("10294", "10295");
                    }

                    sItem["DRXS1"] = Math.Round(GetSafeDouble(sItem["DRXS1"]), mcd).ToString();
                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sign = IsQualified(sItem["G_DRXS"], sItem["DRXS1"], false) == "合格" ? sign : false;
                        sItem["HG_DRXS"] = sign ? "合格" : "不合格";

                        if (sItem["HG_DRXS"] == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mbhggs = mbhggs + 1;
                        }
                        if (sign)
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["DRXS1"] = "----";
                    sItem["G_DRXS"] = "----";
                    sItem["HG_DRXS"] = "----";
                }

                if (jcxm.Contains("、尺寸稳定性(70℃,48h)、"))
                {
                    jcxmCur = "尺寸稳定性(70℃,48h)";
                    sign = true;
                    sign = IsNumeric(sItem["CCWDXC1"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXK1"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXH1"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sItem["HG_CCWDXC1"] = IsQualified(sItem["G_CCWDX1"], sItem["CCWDXC1"], false);
                        sItem["HG_CCWDXK1"] = IsQualified(sItem["G_CCWDX1"], sItem["CCWDXK1"], false);
                        sItem["HG_CCWDXH1"] = IsQualified(sItem["G_CCWDX1"], sItem["CCWDXH1"], false);

                        if (sItem["HG_CCWDXC1"] == "不符合" || sItem["HG_CCWDXK1"] == "不符合" || sItem["HG_CCWDXH1"] == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mbhggs = mbhggs + 1;
                            mAllHg = false;
                        }

                        if (sign)
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;

                        }
                    }
                }
                else
                {
                    sItem["CCWDXC1"] = "----";
                    sItem["CCWDXK1"] = "----";
                    sItem["CCWDXH1"] = "----";
                    sItem["G_CCWDX1"] = "----";
                    sItem["HG_CCWDXC1"] = "----";
                    sItem["HG_CCWDXK1"] = "----";
                    sItem["HG_CCWDXH1"] = "----";
                }

                if (jcxm.Contains("、尺寸稳定性(-30℃,48h)、"))
                {
                    jcxmCur = "尺寸稳定性(-30℃,48h)";
                    sign = true;
                    sign = IsNumeric(sItem["CCWDXC2"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXK2"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXH2"]) ? sign : false;


                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sItem["CCWDXC2"] = IsQualified(sItem["G_CCWDX2"], sItem["CCWDXC2"], false);
                        sItem["CCWDXK2"] = IsQualified(sItem["G_CCWDX2"], sItem["CCWDXK2"], false);
                        sItem["CCWDXH2"] = IsQualified(sItem["G_CCWDX2"], sItem["CCWDXH2"], false);

                        if (sItem["HG_CCWDXC2"] == "不符合" || sItem["HG_CCWDXK2"] == "不符合" || sItem["HG_CCWDXH2"] == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mbhggs = mbhggs + 1;

                        }
                        if (sign)
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = false;
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["CCWDXC2"] = "----";
                    sItem["CCWDXK2"] = "----";
                    sItem["CCWDXH2"] = "----";
                    sItem["G_CCWDX2"] = "----";
                    sItem["HG_CCWDXC2"] = "----";
                    sItem["HG_CCWDXK2"] = "----";
                    sItem["HG_CCWDXH2"] = "----";
                }

                if (jcxm.Contains("、燃烧性能(E级)、"))
                {
                    jcxmCur = "燃烧性能(E级)";
                    if ("符合" == sItem["RSFJ"])
                    {
                        sItem["HG_RSFJ"] = "合格";
                        sItem["RSFJJG"] = "符合E级";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_RSFJ"] = "不合格";
                        sItem["RSFJJG"] = "不符合";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["RSFJ"] = "----";
                    sItem["RSFJJG"] = "----";
                    sItem["HG_RSFJ"] = "----";
                    sItem["G_RSFJ"] = "----";
                }

                if (mbhggs == 0)
                {
                    jsbeizhu = "该组试件所检项目符合" + mItem["PDBZ"] + "标准要求。";
                    sItem["JCJG"] = "合格";
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "该组试件不符合" + mItem["PDBZ"] + "标准要求。";
                    if (mFlag_Bhg && mFlag_Hg)
                    {
                        jsbeizhu = "该组试件所检项目部分符合" + mItem["PDBZ"] + "标准要求。";

                    }
                }
                return mAllHg;
            };


            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["LB"] == sItem["XH"].Trim());

                if (null == mrsDj)
                {
                    jsbeizhu = "依据不详";
                    mAllHg = false;
                    sItem["JCJG"] = "不下结论";
                    continue;
                }

                MItem[0]["G_DRXS2"] = mrsDj["DRXS"];
                MItem[0]["G_BGMD"] = mrsDj["XMD"];
                sItem["G_YSQD"] = mrsDj["YSQD"];
                sItem["G_XMD"] = mrsDj["XMD"];
                sItem["G_CCWDX1"] = mrsDj["CCWDX1"];
                MItem[0]["G_CCWDX"] = mrsDj["CCWDX1"];
                sItem["G_CCWDX2"] = mrsDj["CCWDX2"];
                sItem["G_RSFJ"] = mrsDj["RSFJ"];


                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    //mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
                    #region  导热系数(10℃)
                    if (jcxm.Contains("、导热系数(10℃)、"))
                    {
                        jcxmCur = "导热系数(10℃)";
                        if (IsQualified(MItem[0]["G_DRXS1"], sItem["DRXS1"], false) == "合格")
                        {
                            MItem[0]["HG_DRXS1"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_DRXS1"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }

                    }
                    else
                    {
                        sItem["DRXS1"] = "----";
                        MItem[0]["HG_DRXS1"] = "----";
                        MItem[0]["G_DRXS1"] = "----";
                    }
                    #endregion

                    #region 导热系数(23℃)
                    if (jcxm.Contains("、导热系数(23℃)、"))
                    {
                        jcxmCur = "导热系数(23℃)";
                        if (IsQualified(MItem[0]["G_DRXS2"], sItem["DRXS2"], true) == "符合")
                        {
                            MItem[0]["HG_DRXS2"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_DRXS2"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["DRXS2"] = "----";
                        MItem[0]["HG_DRXS2"] = "----";
                        MItem[0]["G_DRXS2"] = "----";
                    }
                    #endregion

                    #region 尺寸稳定性
                    if (jcxm.Contains("、尺寸稳定性、"))
                    {
                        jcxmCur = "尺寸稳定性";

                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            #region 复检
                            if (Conversion.Val(sItem["CCWDXCQ1_1"]) != 0)
                            {
                                sItem["CCWDXCQ1"] = Round((Conversion.Val(sItem["CCWDXCQ1_1"]) + Conversion.Val(sItem["CCWDXCQ1_2"]) + Conversion.Val(sItem["CCWDXCQ1_3"])) / 3, 1).ToString();
                                sItem["CCWDXCQ2"] = Round((Conversion.Val(sItem["CCWDXCQ2_1"]) + Conversion.Val(sItem["CCWDXCQ2_2"]) + Conversion.Val(sItem["CCWDXCQ2_3"])) / 3, 1).ToString();
                                sItem["CCWDXCQ3"] = Round((Conversion.Val(sItem["CCWDXCQ3_1"]) + Conversion.Val(sItem["CCWDXCQ3_2"]) + Conversion.Val(sItem["CCWDXCQ3_3"])) / 3, 1).ToString();
                                sItem["CCWDXCQ4"] = Round((Conversion.Val(sItem["CCWDXCQ4_1"]) + Conversion.Val(sItem["CCWDXCQ4_2"]) + Conversion.Val(sItem["CCWDXCQ4_3"])) / 3, 1).ToString();
                                sItem["CCWDXCQ5"] = Round((Conversion.Val(sItem["CCWDXCQ5_1"]) + Conversion.Val(sItem["CCWDXCQ5_2"]) + Conversion.Val(sItem["CCWDXCQ5_3"])) / 3, 1).ToString();
                                sItem["CCWDXCQ6"] = Round((Conversion.Val(sItem["CCWDXCQ6_1"]) + Conversion.Val(sItem["CCWDXCQ6_2"]) + Conversion.Val(sItem["CCWDXCQ6_3"])) / 3, 1).ToString();

                                sItem["CCWDXCH1"] = Round((Conversion.Val(sItem["CCWDXCH1_1"]) + Conversion.Val(sItem["CCWDXCH1_2"]) + Conversion.Val(sItem["CCWDXCH1_3"])) / 3, 1).ToString();
                                sItem["CCWDXCH2"] = Round((Conversion.Val(sItem["CCWDXCH2_1"]) + Conversion.Val(sItem["CCWDXCH2_2"]) + Conversion.Val(sItem["CCWDXCH2_3"])) / 3, 1).ToString();
                                sItem["CCWDXCH3"] = Round((Conversion.Val(sItem["CCWDXCH3_1"]) + Conversion.Val(sItem["CCWDXCH3_2"]) + Conversion.Val(sItem["CCWDXCH3_3"])) / 3, 1).ToString();
                                sItem["CCWDXCH4"] = Round((Conversion.Val(sItem["CCWDXCH4_1"]) + Conversion.Val(sItem["CCWDXCH4_2"]) + Conversion.Val(sItem["CCWDXCH4_3"])) / 3, 1).ToString();
                                sItem["CCWDXCH5"] = Round((Conversion.Val(sItem["CCWDXCH5_1"]) + Conversion.Val(sItem["CCWDXCH5_2"]) + Conversion.Val(sItem["CCWDXCH5_3"])) / 3, 1).ToString();
                                sItem["CCWDXCH6"] = Round((Conversion.Val(sItem["CCWDXCH6_1"]) + Conversion.Val(sItem["CCWDXCH6_2"]) + Conversion.Val(sItem["CCWDXCH6_3"])) / 3, 1).ToString();
                            }

                            if (Conversion.Val(sItem["CCWDXKQ1_1"]) != 0)
                            {
                                sItem["CCWDXKQ1"] = Round((Conversion.Val(sItem["CCWDXKQ1_1"]) + Conversion.Val(sItem["CCWDXKQ1_2"]) + Conversion.Val(sItem["CCWDXKQ1_3"])) / 3, 1).ToString();
                                sItem["CCWDXKQ2"] = Round((Conversion.Val(sItem["CCWDXKQ2_1"]) + Conversion.Val(sItem["CCWDXKQ2_2"]) + Conversion.Val(sItem["CCWDXKQ2_3"])) / 3, 1).ToString();
                                sItem["CCWDXKQ3"] = Round((Conversion.Val(sItem["CCWDXKQ3_1"]) + Conversion.Val(sItem["CCWDXKQ3_2"]) + Conversion.Val(sItem["CCWDXKQ3_3"])) / 3, 1).ToString();
                                sItem["CCWDXKQ4"] = Round((Conversion.Val(sItem["CCWDXKQ4_1"]) + Conversion.Val(sItem["CCWDXKQ4_2"]) + Conversion.Val(sItem["CCWDXKQ4_3"])) / 3, 1).ToString();
                                sItem["CCWDXKQ5"] = Round((Conversion.Val(sItem["CCWDXKQ5_1"]) + Conversion.Val(sItem["CCWDXKQ5_2"]) + Conversion.Val(sItem["CCWDXKQ5_3"])) / 3, 1).ToString();
                                sItem["CCWDXKQ6"] = Round((Conversion.Val(sItem["CCWDXKQ6_1"]) + Conversion.Val(sItem["CCWDXKQ6_2"]) + Conversion.Val(sItem["CCWDXKQ6_3"])) / 3, 1).ToString();

                                sItem["CCWDXKH1"] = Round((Conversion.Val(sItem["CCWDXKH1_1"]) + Conversion.Val(sItem["CCWDXKH1_2"]) + Conversion.Val(sItem["CCWDXKH1_3"])) / 3, 1).ToString();
                                sItem["CCWDXKH2"] = Round((Conversion.Val(sItem["CCWDXKH2_1"]) + Conversion.Val(sItem["CCWDXKH2_2"]) + Conversion.Val(sItem["CCWDXKH2_3"])) / 3, 1).ToString();
                                sItem["CCWDXKH3"] = Round((Conversion.Val(sItem["CCWDXKH3_1"]) + Conversion.Val(sItem["CCWDXKH3_2"]) + Conversion.Val(sItem["CCWDXKH3_3"])) / 3, 1).ToString();
                                sItem["CCWDXKH4"] = Round((Conversion.Val(sItem["CCWDXKH4_1"]) + Conversion.Val(sItem["CCWDXKH4_2"]) + Conversion.Val(sItem["CCWDXKH4_3"])) / 3, 1).ToString();
                                sItem["CCWDXKH5"] = Round((Conversion.Val(sItem["CCWDXKH5_1"]) + Conversion.Val(sItem["CCWDXKH5_2"]) + Conversion.Val(sItem["CCWDXKH5_3"])) / 3, 1).ToString();
                                sItem["CCWDXKH6"] = Round((Conversion.Val(sItem["CCWDXKH6_1"]) + Conversion.Val(sItem["CCWDXKH6_2"]) + Conversion.Val(sItem["CCWDXKH6_3"])) / 3, 1).ToString();
                            }

                            if (Conversion.Val(sItem["CCWDXHQ1_1"]) != 0)
                            {
                                sItem["CCWDXHQ1"] = Round((Conversion.Val(sItem["CCWDXHQ1_1"]) + Conversion.Val(sItem["CCWDXHQ1_2"]) + Conversion.Val(sItem["CCWDXHQ1_3"]) + Conversion.Val(sItem["CCWDXHQ1_4"]) + Conversion.Val(sItem["CCWDXHQ1_5"])) / 5, 1).ToString();
                                sItem["CCWDXHQ2"] = Round((Conversion.Val(sItem["CCWDXHQ2_1"]) + Conversion.Val(sItem["CCWDXHQ2_2"]) + Conversion.Val(sItem["CCWDXHQ2_3"]) + Conversion.Val(sItem["CCWDXHQ2_4"]) + Conversion.Val(sItem["CCWDXHQ2_5"])) / 5, 1).ToString();
                                sItem["CCWDXHQ3"] = Round((Conversion.Val(sItem["CCWDXHQ3_1"]) + Conversion.Val(sItem["CCWDXHQ3_2"]) + Conversion.Val(sItem["CCWDXHQ3_3"]) + Conversion.Val(sItem["CCWDXHQ3_4"]) + Conversion.Val(sItem["CCWDXHQ3_5"])) / 5, 1).ToString();
                                sItem["CCWDXHQ4"] = Round((Conversion.Val(sItem["CCWDXHQ4_1"]) + Conversion.Val(sItem["CCWDXHQ4_2"]) + Conversion.Val(sItem["CCWDXHQ4_3"]) + Conversion.Val(sItem["CCWDXHQ4_4"]) + Conversion.Val(sItem["CCWDXHQ4_5"])) / 5, 1).ToString();
                                sItem["CCWDXHQ5"] = Round((Conversion.Val(sItem["CCWDXHQ5_1"]) + Conversion.Val(sItem["CCWDXHQ5_2"]) + Conversion.Val(sItem["CCWDXHQ5_3"]) + Conversion.Val(sItem["CCWDXHQ5_4"]) + Conversion.Val(sItem["CCWDXHQ5_5"])) / 5, 1).ToString();
                                sItem["CCWDXHQ6"] = Round((Conversion.Val(sItem["CCWDXHQ6_1"]) + Conversion.Val(sItem["CCWDXHQ6_2"]) + Conversion.Val(sItem["CCWDXHQ6_3"]) + Conversion.Val(sItem["CCWDXHQ6_4"]) + Conversion.Val(sItem["CCWDXHQ6_5"])) / 5, 1).ToString();

                                sItem["CCWDXHH1"] = Round((Conversion.Val(sItem["CCWDXHH1_1"]) + Conversion.Val(sItem["CCWDXHH1_2"]) + Conversion.Val(sItem["CCWDXHH1_3"]) + Conversion.Val(sItem["CCWDXHH1_4"]) + Conversion.Val(sItem["CCWDXHH1_5"])) / 5, 1).ToString();
                                sItem["CCWDXHH2"] = Round((Conversion.Val(sItem["CCWDXHH2_1"]) + Conversion.Val(sItem["CCWDXHH2_2"]) + Conversion.Val(sItem["CCWDXHH2_3"]) + Conversion.Val(sItem["CCWDXHH2_4"]) + Conversion.Val(sItem["CCWDXHH2_5"])) / 5, 1).ToString();
                                sItem["CCWDXHH3"] = Round((Conversion.Val(sItem["CCWDXHH3_1"]) + Conversion.Val(sItem["CCWDXHH3_2"]) + Conversion.Val(sItem["CCWDXHH3_3"]) + Conversion.Val(sItem["CCWDXHH3_4"]) + Conversion.Val(sItem["CCWDXHH3_5"])) / 5, 1).ToString();
                                sItem["CCWDXHH4"] = Round((Conversion.Val(sItem["CCWDXHH4_1"]) + Conversion.Val(sItem["CCWDXHH4_2"]) + Conversion.Val(sItem["CCWDXHH4_3"]) + Conversion.Val(sItem["CCWDXHH4_4"]) + Conversion.Val(sItem["CCWDXHH4_5"])) / 5, 1).ToString();
                                sItem["CCWDXHH5"] = Round((Conversion.Val(sItem["CCWDXHH5_1"]) + Conversion.Val(sItem["CCWDXHH5_2"]) + Conversion.Val(sItem["CCWDXHH5_3"]) + Conversion.Val(sItem["CCWDXHH5_4"]) + Conversion.Val(sItem["CCWDXHH5_5"])) / 5, 1).ToString();
                                sItem["CCWDXHH6"] = Round((Conversion.Val(sItem["CCWDXHH6_1"]) + Conversion.Val(sItem["CCWDXHH6_2"]) + Conversion.Val(sItem["CCWDXHH6_3"]) + Conversion.Val(sItem["CCWDXHH6_4"]) + Conversion.Val(sItem["CCWDXHH6_5"])) / 5, 1).ToString();
                            }

                            double mcbhl1 = 0, mcbhl2 = 0, mcbhl3 = 0, mcbhl4 = 0, mcbhl5 = 0, mcbhl6 = 0;
                            mcbhl1 = Round(Math.Abs(Conversion.Val(sItem["CCWDXCQ1"]) - Conversion.Val(sItem["CCWDXCH1"])) / (Conversion.Val(sItem["CCWDXCQ1"])) * 100, 1);
                            mcbhl2 = Round(Math.Abs(Conversion.Val(sItem["CCWDXCQ2"]) - Conversion.Val(sItem["CCWDXCH2"])) / (Conversion.Val(sItem["CCWDXCQ2"])) * 100, 1);
                            mcbhl3 = Round(Math.Abs(Conversion.Val(sItem["CCWDXCQ3"]) - Conversion.Val(sItem["CCWDXCH3"])) / (Conversion.Val(sItem["CCWDXCQ3"])) * 100, 1);
                            mcbhl4 = Round(Math.Abs(Conversion.Val(sItem["CCWDXCQ4"]) - Conversion.Val(sItem["CCWDXCH4"])) / (Conversion.Val(sItem["CCWDXCQ4"])) * 100, 1);
                            mcbhl5 = Round(Math.Abs(Conversion.Val(sItem["CCWDXCQ5"]) - Conversion.Val(sItem["CCWDXCH5"])) / (Conversion.Val(sItem["CCWDXCQ5"])) * 100, 1);
                            mcbhl6 = Round(Math.Abs(Conversion.Val(sItem["CCWDXCQ6"]) - Conversion.Val(sItem["CCWDXCH6"])) / (Conversion.Val(sItem["CCWDXCQ6"])) * 100, 1);
                            sItem["CCWDXC"] = Round((mcbhl1 + mcbhl2 + mcbhl3 + mcbhl4 + mcbhl5 + mcbhl6) / 6, 1).ToString("0.0");

                            double mkbhl1 = 0, mkbhl2 = 0, mkbhl3 = 0, mkbhl4 = 0, mkbhl5 = 0, mkbhl6 = 0;
                            mkbhl1 = Round(Math.Abs(Conversion.Val(sItem["CCWDXKQ1"]) - Conversion.Val(sItem["CCWDXKH1"])) / (Conversion.Val(sItem["CCWDXKQ1"])) * 100, 1);
                            mkbhl2 = Round(Math.Abs(Conversion.Val(sItem["CCWDXKQ2"]) - Conversion.Val(sItem["CCWDXKH2"])) / (Conversion.Val(sItem["CCWDXKQ2"])) * 100, 1);
                            mkbhl3 = Round(Math.Abs(Conversion.Val(sItem["CCWDXKQ3"]) - Conversion.Val(sItem["CCWDXKH3"])) / (Conversion.Val(sItem["CCWDXKQ3"])) * 100, 1);
                            mkbhl4 = Round(Math.Abs(Conversion.Val(sItem["CCWDXKQ4"]) - Conversion.Val(sItem["CCWDXKH4"])) / (Conversion.Val(sItem["CCWDXKQ4"])) * 100, 1);
                            mkbhl5 = Round(Math.Abs(Conversion.Val(sItem["CCWDXKQ5"]) - Conversion.Val(sItem["CCWDXKH5"])) / (Conversion.Val(sItem["CCWDXKQ5"])) * 100, 1);
                            mkbhl6 = Round(Math.Abs(Conversion.Val(sItem["CCWDXKQ6"]) - Conversion.Val(sItem["CCWDXKH6"])) / (Conversion.Val(sItem["CCWDXKQ6"])) * 100, 1);
                            sItem["CCWDXK"] = Round((mkbhl1 + mkbhl2 + mkbhl3 + mkbhl4 + mkbhl5 + mkbhl6) / 6, 1).ToString("0.0");

                            double mhbhl1 = 0, mhbhl2 = 0, mhbhl3 = 0, mhbhl4 = 0, mhbhl5 = 0, mhbhl6 = 0;
                            mhbhl1 = Round(Math.Abs(Conversion.Val(sItem["CCWDXHQ1"]) - Conversion.Val(sItem["CCWDXHH1"])) / (Conversion.Val(sItem["CCWDXHQ1"])) * 100, 1);
                            mhbhl2 = Round(Math.Abs(Conversion.Val(sItem["CCWDXHQ2"]) - Conversion.Val(sItem["CCWDXHH2"])) / (Conversion.Val(sItem["CCWDXHQ2"])) * 100, 1);
                            mhbhl3 = Round(Math.Abs(Conversion.Val(sItem["CCWDXHQ3"]) - Conversion.Val(sItem["CCWDXHH3"])) / (Conversion.Val(sItem["CCWDXHQ3"])) * 100, 1);
                            mhbhl4 = Round(Math.Abs(Conversion.Val(sItem["CCWDXHQ4"]) - Conversion.Val(sItem["CCWDXHH4"])) / (Conversion.Val(sItem["CCWDXHQ4"])) * 100, 1);
                            mhbhl5 = Round(Math.Abs(Conversion.Val(sItem["CCWDXHQ5"]) - Conversion.Val(sItem["CCWDXHH5"])) / (Conversion.Val(sItem["CCWDXHQ5"])) * 100, 1);
                            mhbhl6 = Round(Math.Abs(Conversion.Val(sItem["CCWDXHQ6"]) - Conversion.Val(sItem["CCWDXHH6"])) / (Conversion.Val(sItem["CCWDXHQ6"])) * 100, 1);
                            sItem["CCWDXH"] = Round((mhbhl1 + mhbhl2 + mhbhl3 + mhbhl4 + mhbhl5 + mhbhl6) / 6, 1).ToString("0.0");
                            #endregion
                        }
                        else
                        {
                            #region 初检
                            if (Conversion.Val(sItem["CCWDXCQ1_1"]) != 0)
                            {
                                sItem["CCWDXCQ1"] = Round((Conversion.Val(sItem["CCWDXCQ1_1"]) + Conversion.Val(sItem["CCWDXCQ1_2"]) + Conversion.Val(sItem["CCWDXCQ1_3"])) / 3, 1).ToString();
                                sItem["CCWDXCQ2"] = Round((Conversion.Val(sItem["CCWDXCQ2_1"]) + Conversion.Val(sItem["CCWDXCQ2_2"]) + Conversion.Val(sItem["CCWDXCQ2_3"])) / 3, 1).ToString();
                                sItem["CCWDXCQ3"] = Round((Conversion.Val(sItem["CCWDXCQ3_1"]) + Conversion.Val(sItem["CCWDXCQ3_2"]) + Conversion.Val(sItem["CCWDXCQ3_3"])) / 3, 1).ToString();
                                sItem["CCWDXCH1"] = Round((Conversion.Val(sItem["CCWDXCH1_1"]) + Conversion.Val(sItem["CCWDXCH1_2"]) + Conversion.Val(sItem["CCWDXCH1_3"])) / 3, 1).ToString();
                                sItem["CCWDXCH2"] = Round((Conversion.Val(sItem["CCWDXCH2_1"]) + Conversion.Val(sItem["CCWDXCH2_2"]) + Conversion.Val(sItem["CCWDXCH2_3"])) / 3, 1).ToString();
                                sItem["CCWDXCH3"] = Round((Conversion.Val(sItem["CCWDXCH3_1"]) + Conversion.Val(sItem["CCWDXCH3_2"]) + Conversion.Val(sItem["CCWDXCH3_3"])) / 3, 1).ToString();
                            }

                            if (Conversion.Val(sItem["CCWDXKQ1_1"]) != 0)
                            {
                                sItem["CCWDXKQ1"] = Round((Conversion.Val(sItem["CCWDXKQ1_1"]) + Conversion.Val(sItem["CCWDXKQ1_2"]) + Conversion.Val(sItem["CCWDXKQ1_3"])) / 3, 1).ToString();
                                sItem["CCWDXKQ2"] = Round((Conversion.Val(sItem["CCWDXKQ2_1"]) + Conversion.Val(sItem["CCWDXKQ2_2"]) + Conversion.Val(sItem["CCWDXKQ2_3"])) / 3, 1).ToString();
                                sItem["CCWDXKQ3"] = Round((Conversion.Val(sItem["CCWDXKQ3_1"]) + Conversion.Val(sItem["CCWDXKQ3_2"]) + Conversion.Val(sItem["CCWDXKQ3_3"])) / 3, 1).ToString();
                                sItem["CCWDXKH1"] = Round((Conversion.Val(sItem["CCWDXKH1_1"]) + Conversion.Val(sItem["CCWDXKH1_2"]) + Conversion.Val(sItem["CCWDXKH1_3"])) / 3, 1).ToString();
                                sItem["CCWDXKH2"] = Round((Conversion.Val(sItem["CCWDXKH2_1"]) + Conversion.Val(sItem["CCWDXKH2_2"]) + Conversion.Val(sItem["CCWDXKH2_3"])) / 3, 1).ToString();
                                sItem["CCWDXKH3"] = Round((Conversion.Val(sItem["CCWDXKH3_1"]) + Conversion.Val(sItem["CCWDXKH3_2"]) + Conversion.Val(sItem["CCWDXKH3_3"])) / 3, 1).ToString();
                            }

                            if (Conversion.Val(sItem["CCWDXHQ1_1"]) != 0)
                            {
                                sItem["CCWDXHQ1"] = Round((Conversion.Val(sItem["CCWDXHQ1_1"]) + Conversion.Val(sItem["CCWDXHQ1_2"]) + Conversion.Val(sItem["CCWDXHQ1_3"]) + Conversion.Val(sItem["CCWDXHQ1_4"]) + Conversion.Val(sItem["CCWDXHQ1_5"])) / 5, 1).ToString();
                                sItem["CCWDXHQ2"] = Round((Conversion.Val(sItem["CCWDXHQ2_1"]) + Conversion.Val(sItem["CCWDXHQ2_2"]) + Conversion.Val(sItem["CCWDXHQ2_3"]) + Conversion.Val(sItem["CCWDXHQ2_4"]) + Conversion.Val(sItem["CCWDXHQ2_5"])) / 5, 1).ToString();
                                sItem["CCWDXHQ3"] = Round((Conversion.Val(sItem["CCWDXHQ3_1"]) + Conversion.Val(sItem["CCWDXHQ3_2"]) + Conversion.Val(sItem["CCWDXHQ3_3"]) + Conversion.Val(sItem["CCWDXHQ3_4"]) + Conversion.Val(sItem["CCWDXHQ3_5"])) / 5, 1).ToString();
                                sItem["CCWDXHH1"] = Round((Conversion.Val(sItem["CCWDXHH1_1"]) + Conversion.Val(sItem["CCWDXHH1_2"]) + Conversion.Val(sItem["CCWDXHH1_3"]) + Conversion.Val(sItem["CCWDXHH1_4"]) + Conversion.Val(sItem["CCWDXHH1_5"])) / 5, 1).ToString();
                                sItem["CCWDXHH2"] = Round((Conversion.Val(sItem["CCWDXHH2_1"]) + Conversion.Val(sItem["CCWDXHH2_2"]) + Conversion.Val(sItem["CCWDXHH2_3"]) + Conversion.Val(sItem["CCWDXHH2_4"]) + Conversion.Val(sItem["CCWDXHH2_5"])) / 5, 1).ToString();
                                sItem["CCWDXHH3"] = Round((Conversion.Val(sItem["CCWDXHH3_1"]) + Conversion.Val(sItem["CCWDXHH3_2"]) + Conversion.Val(sItem["CCWDXHH3_3"]) + Conversion.Val(sItem["CCWDXHH3_4"]) + Conversion.Val(sItem["CCWDXHH3_5"])) / 5, 1).ToString();
                            }

                            double mcbhl1 = 0, mcbhl2 = 0, mcbhl3 = 0;
                            mcbhl1 = Round(Math.Abs(Conversion.Val(sItem["CCWDXCQ1"]) - Conversion.Val(sItem["CCWDXCH1"])) / (Conversion.Val(sItem["CCWDXCQ1"])) * 100, 1);
                            mcbhl2 = Round(Math.Abs(Conversion.Val(sItem["CCWDXCQ2"]) - Conversion.Val(sItem["CCWDXCH2"])) / (Conversion.Val(sItem["CCWDXCQ2"])) * 100, 1);
                            mcbhl3 = Round(Math.Abs(Conversion.Val(sItem["CCWDXCQ3"]) - Conversion.Val(sItem["CCWDXCH3"])) / (Conversion.Val(sItem["CCWDXCQ3"])) * 100, 1);
                            sItem["CCWDXC"] = Round((mcbhl1 + mcbhl2 + mcbhl3) / 3, 1).ToString("0.0");

                            double mkbhl1 = 0, mkbhl2 = 0, mkbhl3 = 0;
                            mkbhl1 = Round(Math.Abs(Conversion.Val(sItem["CCWDXKQ1"]) - Conversion.Val(sItem["CCWDXKH1"])) / (Conversion.Val(sItem["CCWDXKQ1"])) * 100, 1);
                            mkbhl2 = Round(Math.Abs(Conversion.Val(sItem["CCWDXKQ2"]) - Conversion.Val(sItem["CCWDXKH2"])) / (Conversion.Val(sItem["CCWDXKQ2"])) * 100, 1);
                            mkbhl3 = Round(Math.Abs(Conversion.Val(sItem["CCWDXKQ3"]) - Conversion.Val(sItem["CCWDXKH3"])) / (Conversion.Val(sItem["CCWDXKQ3"])) * 100, 1);
                            sItem["CCWDXK"] = Round((mkbhl1 + mkbhl2 + mkbhl3) / 3, 1).ToString("0.0");

                            double mhbhl1 = 0, mhbhl2 = 0, mhbhl3 = 0;
                            mhbhl1 = Round(Math.Abs(Conversion.Val(sItem["CCWDXHQ1"]) - Conversion.Val(sItem["CCWDXHH1"])) / (Conversion.Val(sItem["CCWDXHQ1"])) * 100, 1);
                            mhbhl2 = Round(Math.Abs(Conversion.Val(sItem["CCWDXHQ2"]) - Conversion.Val(sItem["CCWDXHH2"])) / (Conversion.Val(sItem["CCWDXHQ2"])) * 100, 1);
                            mhbhl3 = Round(Math.Abs(Conversion.Val(sItem["CCWDXHQ3"]) - Conversion.Val(sItem["CCWDXHH3"])) / (Conversion.Val(sItem["CCWDXHQ3"])) * 100, 1);
                            sItem["CCWDXH"] = Round((mhbhl1 + mhbhl2 + mhbhl3) / 3, 1).ToString("0.0");
                            #endregion
                        }

                        if (IsQualified(MItem[0]["G_CCWDX"], sItem["CCWDXC"], true) == "符合")
                        {
                            MItem[0]["HG_CCWDXC"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_CCWDXC"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }

                        if (IsQualified(MItem[0]["G_CCWDX"], sItem["CCWDXK"], true) == "符合")
                        {
                            MItem[0]["HG_CCWDXK"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_CCWDXK"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }

                        if (IsQualified(MItem[0]["G_CCWDX"], sItem["CCWDXH"], true) == "符合")
                        {
                            MItem[0]["HG_CCWDXH"] = "合格";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_CCWDXH"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["CCWDXC"] = "----";
                        sItem["CCWDXK"] = "----";
                        sItem["CCWDXH"] = "----";
                        MItem[0]["HG_CCWDXC"] = "----";
                        MItem[0]["HG_CCWDXK"] = "----";
                        MItem[0]["HG_CCWDXH"] = "----";
                        MItem[0]["G_CCWDX"] = "----";
                    }
                    #endregion

                    #region 芯密度
                    if (jcxm.Contains("、芯密度、"))
                    {
                        jcxmCur = "芯密度";
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            #region 复检
                            double mcd1 = 0, mkd1 = 0, mhd1 = 0, bgmdv1 = 0;
                            mcd1 = Round((Conversion.Val(sItem["BGMDC1_1"]) + Conversion.Val(sItem["BGMDC1_2"]) + Conversion.Val(sItem["BGMDC1_3"])) / 3, 1);
                            mkd1 = Round((Conversion.Val(sItem["BGMDK1_1"]) + Conversion.Val(sItem["BGMDK1_2"]) + Conversion.Val(sItem["BGMDK1_3"])) / 3, 1);
                            mhd1 = Round((Conversion.Val(sItem["BGMDH1_1"]) + Conversion.Val(sItem["BGMDH1_2"]) + Conversion.Val(sItem["BGMDH1_3"]) + Conversion.Val(sItem["BGMDH1_4"]) + Conversion.Val(sItem["BGMDH1_5"])) / 5, 1);

                            if (mcd1 == 0 && Conversion.Val(sItem["BGMDC1"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC1"] = mcd1.ToString("0.0");
                                sItem["BGMDK1"] = mkd1.ToString("0.0");
                                sItem["BGMDH1"] = mhd1.ToString("0.0");
                            }
                            bgmdv1 = Conversion.Val(sItem["BGMDC1"]) * Conversion.Val(sItem["BGMDK1"]) * Conversion.Val(sItem["BGMDH1"]);

                            double mcd2 = 0, mkd2 = 0, mhd2 = 0, bgmdv2 = 0;

                            mcd2 = Round((Conversion.Val(sItem["BGMDC2_1"]) + Conversion.Val(sItem["BGMDC2_2"]) + Conversion.Val(sItem["BGMDC2_3"])) / 3, 1);
                            mkd2 = Round((Conversion.Val(sItem["BGMDK2_1"]) + Conversion.Val(sItem["BGMDK2_2"]) + Conversion.Val(sItem["BGMDK2_3"])) / 3, 1);
                            mhd2 = Round((Conversion.Val(sItem["BGMDH2_1"]) + Conversion.Val(sItem["BGMDH2_2"]) + Conversion.Val(sItem["BGMDH2_3"]) + Conversion.Val(sItem["BGMDH2_4"]) + Conversion.Val(sItem["BGMDH2_5"])) / 5, 1);

                            if (mcd2 == 0 && Conversion.Val(sItem["BGMDC2"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC2"] = mcd2.ToString("0.0");
                                sItem["BGMDK2"] = mkd2.ToString("0.0");
                                sItem["BGMDH2"] = mhd2.ToString("0.0");
                            }
                            bgmdv2 = Conversion.Val(sItem["BGMDC2"]) * Conversion.Val(sItem["BGMDK2"]) * Conversion.Val(sItem["BGMDH2"]);


                            double mcd3 = 0, mkd3 = 0, mhd3 = 0, bgmdv3 = 0;

                            mcd3 = Round((Conversion.Val(sItem["BGMDC3_1"]) + Conversion.Val(sItem["BGMDC3_2"]) + Conversion.Val(sItem["BGMDC3_3"])) / 3, 1);
                            mkd3 = Round((Conversion.Val(sItem["BGMDK3_1"]) + Conversion.Val(sItem["BGMDK3_2"]) + Conversion.Val(sItem["BGMDK3_3"])) / 3, 1);
                            mhd3 = Round((Conversion.Val(sItem["BGMDH3_1"]) + Conversion.Val(sItem["BGMDH3_2"]) + Conversion.Val(sItem["BGMDH3_3"]) + Conversion.Val(sItem["BGMDH3_4"]) + Conversion.Val(sItem["BGMDH3_5"])) / 5, 1);
                            if (mcd3 == 0 && Conversion.Val(sItem["BGMDC3"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC3"] = mcd3.ToString("0.0");
                                sItem["BGMDK3"] = mkd3.ToString("0.0");
                                sItem["BGMDH3"] = mhd3.ToString("0.0");
                            }
                            bgmdv3 = Conversion.Val(sItem["BGMDC3"]) * Conversion.Val(sItem["BGMDK3"]) * Conversion.Val(sItem["BGMDH3"]);

                            double mcd4 = 0, mkd4 = 0, mhd4 = 0, bgmdv4 = 0;
                            mcd4 = Round((Conversion.Val(sItem["BGMDC4_1"]) + Conversion.Val(sItem["BGMDC4_2"]) + Conversion.Val(sItem["BGMDC4_3"])) / 3, 1);
                            mkd4 = Round((Conversion.Val(sItem["BGMDK4_1"]) + Conversion.Val(sItem["BGMDK4_2"]) + Conversion.Val(sItem["BGMDK4_3"])) / 3, 1);
                            mhd4 = Round((Conversion.Val(sItem["BGMDH4_1"]) + Conversion.Val(sItem["BGMDH4_2"]) + Conversion.Val(sItem["BGMDH4_3"]) + Conversion.Val(sItem["BGMDH4_4"]) + Conversion.Val(sItem["BGMDH4_5"])) / 5, 1);
                            if (mcd4 == 0 && Conversion.Val(sItem["BGMDC4"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC4"] = mcd4.ToString("0.0");
                                sItem["BGMDK4"] = mkd4.ToString("0.0");
                                sItem["BGMDH4"] = mhd4.ToString("0.0");
                            }
                            bgmdv4 = Conversion.Val(sItem["BGMDC4"]) * Conversion.Val(sItem["BGMDK4"]) * Conversion.Val(sItem["BGMDH4"]);

                            double mcd5 = 0, mkd5 = 0, mhd5 = 0, bgmdv5 = 0;
                            mcd5 = Round((Conversion.Val(sItem["BGMDC5_1"]) + Conversion.Val(sItem["BGMDC5_2"]) + Conversion.Val(sItem["BGMDC5_3"])) / 3, 1);
                            mkd5 = Round((Conversion.Val(sItem["BGMDK5_1"]) + Conversion.Val(sItem["BGMDK5_2"]) + Conversion.Val(sItem["BGMDK5_3"])) / 3, 1);
                            mhd5 = Round((Conversion.Val(sItem["BGMDH5_1"]) + Conversion.Val(sItem["BGMDH5_2"]) + Conversion.Val(sItem["BGMDH5_3"]) + Conversion.Val(sItem["BGMDH5_4"]) + Conversion.Val(sItem["BGMDH5_5"])) / 5, 1);
                            if (mcd5 == 0 && Conversion.Val(sItem["BGMDC5"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC5"] = mcd5.ToString("0.0");
                                sItem["BGMDK5"] = mkd5.ToString("0.0");
                                sItem["BGMDH5"] = mhd5.ToString("0.0");
                            }
                            bgmdv5 = Conversion.Val(sItem["BGMDC5"]) * Conversion.Val(sItem["BGMDK5"]) * Conversion.Val(sItem["BGMDH5"]);

                            double mcd6 = 0, mkd6 = 0, mhd6 = 0, bgmdv6 = 0;
                            mcd6 = Round((Conversion.Val(sItem["BGMDC6_1"]) + Conversion.Val(sItem["BGMDC6_2"]) + Conversion.Val(sItem["BGMDC6_3"])) / 3, 1);
                            mkd6 = Round((Conversion.Val(sItem["BGMDK6_1"]) + Conversion.Val(sItem["BGMDK6_2"]) + Conversion.Val(sItem["BGMDK6_3"])) / 3, 1);
                            mhd6 = Round((Conversion.Val(sItem["BGMDH6_1"]) + Conversion.Val(sItem["BGMDH6_2"]) + Conversion.Val(sItem["BGMDH6_3"]) + Conversion.Val(sItem["BGMDH6_4"]) + Conversion.Val(sItem["BGMDH6_5"])) / 5, 1);
                            if (mcd6 == 0 && Conversion.Val(sItem["BGMDC6"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC6"] = mcd6.ToString("0.0");
                                sItem["BGMDK6"] = mkd6.ToString("0.0");
                                sItem["BGMDH6"] = mhd6.ToString("0.0");
                            }
                            bgmdv6 = Conversion.Val(sItem["BGMDC6"]) * Conversion.Val(sItem["BGMDK6"]) * Conversion.Val(sItem["BGMDH6"]);

                            double mcd7 = 0, mkd7 = 0, mhd7 = 0, bgmdv7 = 0;
                            mcd7 = Round((Conversion.Val(sItem["BGMDC7_1"]) + Conversion.Val(sItem["BGMDC7_2"]) + Conversion.Val(sItem["BGMDC7_3"])) / 3, 1);
                            mkd7 = Round((Conversion.Val(sItem["BGMDK7_1"]) + Conversion.Val(sItem["BGMDK7_2"]) + Conversion.Val(sItem["BGMDK7_3"])) / 3, 1);
                            mhd7 = Round((Conversion.Val(sItem["BGMDH7_1"]) + Conversion.Val(sItem["BGMDH7_2"]) + Conversion.Val(sItem["BGMDH7_3"]) + Conversion.Val(sItem["BGMDH7_4"]) + Conversion.Val(sItem["BGMDH7_5"])) / 5, 1);
                            if (mcd7 == 0 && Conversion.Val(sItem["BGMDC7"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC7"] = mcd7.ToString("0.0");
                                sItem["BGMDK7"] = mkd7.ToString("0.0");
                                sItem["BGMDH7"] = mhd7.ToString("0.0");
                            }
                            bgmdv7 = Conversion.Val(sItem["BGMDC7"]) * Conversion.Val(sItem["BGMDK7"]) * Conversion.Val(sItem["BGMDH7"]);

                            double mcd8 = 0, mkd8 = 0, mhd8 = 0, bgmdv8 = 0;
                            mcd8 = Round((Conversion.Val(sItem["BGMDC8_1"]) + Conversion.Val(sItem["BGMDC8_2"]) + Conversion.Val(sItem["BGMDC8_3"])) / 3, 1);
                            mkd8 = Round((Conversion.Val(sItem["BGMDK8_1"]) + Conversion.Val(sItem["BGMDK8_2"]) + Conversion.Val(sItem["BGMDK8_3"])) / 3, 1);
                            mhd8 = Round((Conversion.Val(sItem["BGMDH8_1"]) + Conversion.Val(sItem["BGMDH8_2"]) + Conversion.Val(sItem["BGMDH8_3"]) + Conversion.Val(sItem["BGMDH8_4"]) + Conversion.Val(sItem["BGMDH8_5"])) / 5, 1);
                            if (mcd8 == 0 && Conversion.Val(sItem["BGMDC8"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC8"] = mcd8.ToString("0.0");
                                sItem["BGMDK8"] = mkd8.ToString("0.0");
                                sItem["BGMDH8"] = mhd8.ToString("0.0");
                            }
                            bgmdv8 = Conversion.Val(sItem["BGMDC8"]) * Conversion.Val(sItem["BGMDK8"]) * Conversion.Val(sItem["BGMDH8"]);

                            double mcd9 = 0, mkd9 = 0, mhd9 = 0, bgmdv9 = 0;
                            mcd9 = Round((Conversion.Val(sItem["BGMDC9_1"]) + Conversion.Val(sItem["BGMDC9_2"]) + Conversion.Val(sItem["BGMDC9_3"])) / 3, 1);
                            mkd9 = Round((Conversion.Val(sItem["BGMDK9_1"]) + Conversion.Val(sItem["BGMDK9_2"]) + Conversion.Val(sItem["BGMDK9_3"])) / 3, 1);
                            mhd9 = Round((Conversion.Val(sItem["BGMDH9_1"]) + Conversion.Val(sItem["BGMDH9_2"]) + Conversion.Val(sItem["BGMDH9_3"]) + Conversion.Val(sItem["BGMDH9_4"]) + Conversion.Val(sItem["BGMDH9_5"])) / 5, 1);
                            if (mcd9 == 0 && Conversion.Val(sItem["BGMDC9"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC9"] = mcd9.ToString("0.0");
                                sItem["BGMDK9"] = mkd9.ToString("0.0");
                                sItem["BGMDH9"] = mhd9.ToString("0.0");
                            }
                            bgmdv9 = Conversion.Val(sItem["BGMDC9"]) * Conversion.Val(sItem["BGMDK9"]) * Conversion.Val(sItem["BGMDH9"]);


                            double mcd10 = 0, mkd10 = 0, mhd10 = 0, bgmdv10 = 0;
                            mcd10 = Round((Conversion.Val(sItem["BGMDC10_1"]) + Conversion.Val(sItem["BGMDC10_2"]) + Conversion.Val(sItem["BGMDC10_3"])) / 3, 1);
                            mkd10 = Round((Conversion.Val(sItem["BGMDK10_1"]) + Conversion.Val(sItem["BGMDK10_2"]) + Conversion.Val(sItem["BGMDK10_3"])) / 3, 1);
                            mhd10 = Round((Conversion.Val(sItem["BGMDH10_1"]) + Conversion.Val(sItem["BGMDH10_2"]) + Conversion.Val(sItem["BGMDH10_3"]) + Conversion.Val(sItem["BGMDH10_4"]) + Conversion.Val(sItem["BGMDH10_5"])) / 5, 1);
                            if (mcd10 == 0 && Conversion.Val(sItem["BGMDC10"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC10"] = mcd10.ToString("0.0");
                                sItem["BGMDK10"] = mkd10.ToString("0.0");
                                sItem["BGMDH10"] = mhd10.ToString("0.0");
                            }
                            bgmdv10 = Conversion.Val(sItem["BGMDC10"]) * Conversion.Val(sItem["BGMDK10"]) * Conversion.Val(sItem["BGMDH10"]);


                            sItem["BGMD1"] = Round((Conversion.Val(sItem["BGMDM1"]) / bgmdv1) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD2"] = Round((Conversion.Val(sItem["BGMDM2"]) / bgmdv2) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD3"] = Round((Conversion.Val(sItem["BGMDM3"]) / bgmdv3) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD4"] = Round((Conversion.Val(sItem["BGMDM4"]) / bgmdv4) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD5"] = Round((Conversion.Val(sItem["BGMDM5"]) / bgmdv5) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD6"] = Round((Conversion.Val(sItem["BGMDM6"]) / bgmdv6) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD7"] = Round((Conversion.Val(sItem["BGMDM7"]) / bgmdv7) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD8"] = Round((Conversion.Val(sItem["BGMDM8"]) / bgmdv8) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD9"] = Round((Conversion.Val(sItem["BGMDM9"]) / bgmdv9) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD10"] = Round((Conversion.Val(sItem["BGMDM10"]) / bgmdv10) * Math.Pow(10, 6), 4).ToString();

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD1"]) < 15)
                                {
                                    sItem["BGMD1"] = Round(Conversion.Val(sItem["BGMD1"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD1"]) < 15)
                                {
                                    sItem["BGMD1"] = Round(Conversion.Val(sItem["BGMD1"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD2"]) < 15)
                                {
                                    sItem["BGMD2"] = Round(Conversion.Val(sItem["BGMD2"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD2"]) < 15)
                                {
                                    sItem["BGMD2"] = Round(Conversion.Val(sItem["BGMD2"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD3"]) < 15)
                                {
                                    sItem["BGMD3"] = Round(Conversion.Val(sItem["BGMD3"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD3"]) < 15)
                                {
                                    sItem["BGMD3"] = Round(Conversion.Val(sItem["BGMD3"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD4"]) < 15)
                                {
                                    sItem["BGMD4"] = Round(Conversion.Val(sItem["BGMD4"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD4"]) < 15)
                                {
                                    sItem["BGMD4"] = Round(Conversion.Val(sItem["BGMD4"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD5"]) < 15)
                                {
                                    sItem["BGMD5"] = Round(Conversion.Val(sItem["BGMD5"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD5"]) < 15)
                                {
                                    sItem["BGMD5"] = Round(Conversion.Val(sItem["BGMD5"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD6"]) < 15)
                                {
                                    sItem["BGMD6"] = Round(Conversion.Val(sItem["BGMD6"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD6"]) < 15)
                                {
                                    sItem["BGMD6"] = Round(Conversion.Val(sItem["BGMD6"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD7"]) < 15)
                                {
                                    sItem["BGMD7"] = Round(Conversion.Val(sItem["BGMD7"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD7"]) < 15)
                                {
                                    sItem["BGMD7"] = Round(Conversion.Val(sItem["BGMD7"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD8"]) < 15)
                                {
                                    sItem["BGMD8"] = Round(Conversion.Val(sItem["BGMD8"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD8"]) < 15)
                                {
                                    sItem["BGMD8"] = Round(Conversion.Val(sItem["BGMD8"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD9"]) < 15)
                                {
                                    sItem["BGMD9"] = Round(Conversion.Val(sItem["BGMD9"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD9"]) < 15)
                                {
                                    sItem["BGMD9"] = Round(Conversion.Val(sItem["BGMD9"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD10"]) < 15)
                                {
                                    sItem["BGMD10"] = Round(Conversion.Val(sItem["BGMD10"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD10"]) < 15)
                                {
                                    sItem["BGMD10"] = Round(Conversion.Val(sItem["BGMD10"]) + 1.1955, 4).ToString();
                                }
                            }

                            sItem["BGMD"] = Round((Conversion.Val(sItem["BGMD1"]) + Conversion.Val(sItem["BGMD2"]) + Conversion.Val(sItem["BGMD3"]) + Conversion.Val(sItem["BGMD4"]) + Conversion.Val(sItem["BGMD5"]) + Conversion.Val(sItem["BGMD6"]) + Conversion.Val(sItem["BGMD7"]) + Conversion.Val(sItem["BGMD8"]) + Conversion.Val(sItem["BGMD9"]) + Conversion.Val(sItem["BGMD10"])) / 10, 1).ToString("0.0");
                            #endregion
                        }
                        else
                        {
                            #region 初检
                            double mcd1 = 0, mkd1 = 0, mhd1 = 0, bgmdv1 = 0;
                            mcd1 = Round((Conversion.Val(sItem["BGMDC1_1"]) + Conversion.Val(sItem["BGMDC1_2"]) + Conversion.Val(sItem["BGMDC1_3"])) / 3, 1);
                            mkd1 = Round((Conversion.Val(sItem["BGMDK1_1"]) + Conversion.Val(sItem["BGMDK1_2"]) + Conversion.Val(sItem["BGMDK1_3"])) / 3, 1);
                            mhd1 = Round((Conversion.Val(sItem["BGMDH1_1"]) + Conversion.Val(sItem["BGMDH1_2"]) + Conversion.Val(sItem["BGMDH1_3"]) + Conversion.Val(sItem["BGMDH1_4"]) + Conversion.Val(sItem["BGMDH1_5"])) / 5, 1);

                            if (mcd1 == 0 && Conversion.Val(sItem["BGMDC1"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC1"] = mcd1.ToString("0.0");
                                sItem["BGMDK1"] = mkd1.ToString("0.0");
                                sItem["BGMDH1"] = mhd1.ToString("0.0");
                            }
                            bgmdv1 = Conversion.Val(sItem["BGMDC1"]) * Conversion.Val(sItem["BGMDK1"]) * Conversion.Val(sItem["BGMDH1"]);

                            double mcd2 = 0, mkd2 = 0, mhd2 = 0, bgmdv2 = 0;

                            mcd2 = Round((Conversion.Val(sItem["BGMDC2_1"]) + Conversion.Val(sItem["BGMDC2_2"]) + Conversion.Val(sItem["BGMDC2_3"])) / 3, 1);
                            mkd2 = Round((Conversion.Val(sItem["BGMDK2_1"]) + Conversion.Val(sItem["BGMDK2_2"]) + Conversion.Val(sItem["BGMDK2_3"])) / 3, 1);
                            mhd2 = Round((Conversion.Val(sItem["BGMDH2_1"]) + Conversion.Val(sItem["BGMDH2_2"]) + Conversion.Val(sItem["BGMDH2_3"]) + Conversion.Val(sItem["BGMDH2_4"]) + Conversion.Val(sItem["BGMDH2_5"])) / 5, 1);

                            if (mcd2 == 0 && Conversion.Val(sItem["BGMDC2"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC2"] = mcd2.ToString("0.0");
                                sItem["BGMDK2"] = mkd2.ToString("0.0");
                                sItem["BGMDH2"] = mhd2.ToString("0.0");
                            }
                            bgmdv2 = Conversion.Val(sItem["BGMDC2"]) * Conversion.Val(sItem["BGMDK2"]) * Conversion.Val(sItem["BGMDH2"]);


                            double mcd3 = 0, mkd3 = 0, mhd3 = 0, bgmdv3 = 0;

                            mcd3 = Round((Conversion.Val(sItem["BGMDC3_1"]) + Conversion.Val(sItem["BGMDC3_2"]) + Conversion.Val(sItem["BGMDC3_3"])) / 3, 1);
                            mkd3 = Round((Conversion.Val(sItem["BGMDK3_1"]) + Conversion.Val(sItem["BGMDK3_2"]) + Conversion.Val(sItem["BGMDK3_3"])) / 3, 1);
                            mhd3 = Round((Conversion.Val(sItem["BGMDH3_1"]) + Conversion.Val(sItem["BGMDH3_2"]) + Conversion.Val(sItem["BGMDH3_3"]) + Conversion.Val(sItem["BGMDH3_4"]) + Conversion.Val(sItem["BGMDH3_5"])) / 5, 1);
                            if (mcd3 == 0 && Conversion.Val(sItem["BGMDC3"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC3"] = mcd3.ToString("0.0");
                                sItem["BGMDK3"] = mkd3.ToString("0.0");
                                sItem["BGMDH3"] = mhd3.ToString("0.0");
                            }
                            bgmdv3 = Conversion.Val(sItem["BGMDC3"]) * Conversion.Val(sItem["BGMDK3"]) * Conversion.Val(sItem["BGMDH3"]);

                            double mcd4 = 0, mkd4 = 0, mhd4 = 0, bgmdv4 = 0;
                            mcd4 = Round((Conversion.Val(sItem["BGMDC4_1"]) + Conversion.Val(sItem["BGMDC4_2"]) + Conversion.Val(sItem["BGMDC4_3"])) / 3, 1);
                            mkd4 = Round((Conversion.Val(sItem["BGMDK4_1"]) + Conversion.Val(sItem["BGMDK4_2"]) + Conversion.Val(sItem["BGMDK4_3"])) / 3, 1);
                            mhd4 = Round((Conversion.Val(sItem["BGMDH4_1"]) + Conversion.Val(sItem["BGMDH4_2"]) + Conversion.Val(sItem["BGMDH4_3"]) + Conversion.Val(sItem["BGMDH4_4"]) + Conversion.Val(sItem["BGMDH4_5"])) / 5, 1);
                            if (mcd4 == 0 && Conversion.Val(sItem["BGMDC4"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC4"] = mcd4.ToString("0.0");
                                sItem["BGMDK4"] = mkd4.ToString("0.0");
                                sItem["BGMDH4"] = mhd4.ToString("0.0");
                            }
                            bgmdv4 = Conversion.Val(sItem["BGMDC4"]) * Conversion.Val(sItem["BGMDK4"]) * Conversion.Val(sItem["BGMDH4"]);

                            double mcd5 = 0, mkd5 = 0, mhd5 = 0, bgmdv5 = 0;
                            mcd5 = Round((Conversion.Val(sItem["BGMDC5_1"]) + Conversion.Val(sItem["BGMDC5_2"]) + Conversion.Val(sItem["BGMDC5_3"])) / 3, 1);
                            mkd5 = Round((Conversion.Val(sItem["BGMDK5_1"]) + Conversion.Val(sItem["BGMDK5_2"]) + Conversion.Val(sItem["BGMDK5_3"])) / 3, 1);
                            mhd5 = Round((Conversion.Val(sItem["BGMDH5_1"]) + Conversion.Val(sItem["BGMDH5_2"]) + Conversion.Val(sItem["BGMDH5_3"]) + Conversion.Val(sItem["BGMDH5_4"]) + Conversion.Val(sItem["BGMDH5_5"])) / 5, 1);
                            if (mcd5 == 0 && Conversion.Val(sItem["BGMDC5"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC5"] = mcd5.ToString("0.0");
                                sItem["BGMDK5"] = mkd5.ToString("0.0");
                                sItem["BGMDH5"] = mhd5.ToString("0.0");
                            }
                            bgmdv5 = Conversion.Val(sItem["BGMDC5"]) * Conversion.Val(sItem["BGMDK5"]) * Conversion.Val(sItem["BGMDH5"]);

                            sItem["BGMD1"] = Round((Conversion.Val(sItem["BGMDM1"]) / bgmdv1) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD2"] = Round((Conversion.Val(sItem["BGMDM2"]) / bgmdv2) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD3"] = Round((Conversion.Val(sItem["BGMDM3"]) / bgmdv3) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD4"] = Round((Conversion.Val(sItem["BGMDM4"]) / bgmdv4) * Math.Pow(10, 6), 4).ToString();
                            sItem["BGMD5"] = Round((Conversion.Val(sItem["BGMDM5"]) / bgmdv5) * Math.Pow(10, 6), 4).ToString();

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD1"]) < 15)
                                {
                                    sItem["BGMD1"] = Round(Conversion.Val(sItem["BGMD1"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD1"]) < 15)
                                {
                                    sItem["BGMD1"] = Round(Conversion.Val(sItem["BGMD1"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD2"]) < 15)
                                {
                                    sItem["BGMD2"] = Round(Conversion.Val(sItem["BGMD2"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD2"]) < 15)
                                {
                                    sItem["BGMD2"] = Round(Conversion.Val(sItem["BGMD2"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD3"]) < 15)
                                {
                                    sItem["BGMD3"] = Round(Conversion.Val(sItem["BGMD3"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD3"]) < 15)
                                {
                                    sItem["BGMD3"] = Round(Conversion.Val(sItem["BGMD3"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD4"]) < 15)
                                {
                                    sItem["BGMD4"] = Round(Conversion.Val(sItem["BGMD4"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD4"]) < 15)
                                {
                                    sItem["BGMD4"] = Round(Conversion.Val(sItem["BGMD4"]) + 1.1955, 4).ToString();
                                }
                            }

                            if (Conversion.Val(sItem["BGMDWD"]) == 23)
                            {
                                if (Conversion.Val(sItem["BGMD5"]) < 15)
                                {
                                    sItem["BGMD5"] = Round(Conversion.Val(sItem["BGMD5"]) + 1.22, 4).ToString();
                                }
                            }
                            else
                            {
                                if (Conversion.Val(sItem["BGMD5"]) < 15)
                                {
                                    sItem["BGMD5"] = Round(Conversion.Val(sItem["BGMD5"]) + 1.1955, 4).ToString();
                                }
                            }

                            sItem["BGMD"] = Round((Conversion.Val(sItem["BGMD1"]) + Conversion.Val(sItem["BGMD2"]) + Conversion.Val(sItem["BGMD3"]) + Conversion.Val(sItem["BGMD4"]) + Conversion.Val(sItem["BGMD5"])) / 5, 1).ToString("0.0");
                            #endregion
                        }
                        if (IsQualified(MItem[0]["G_BGMD"], sItem["BGMD"], true) == "符合")
                        {
                            MItem[0]["HG_BGMD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_BGMD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["BGMD"] = "----";
                        MItem[0]["HG_BGMD"] = "----";
                        MItem[0]["G_BGMD"] = "----";
                    }
                    #endregion

                    #region 抗拉强度
                    if (jcxm.Contains("、抗拉强度、"))
                    {
                        jcxmCur = "抗拉强度";

                        #region 初检
                        double KLMJ1 = 0, KLMJ2 = 0, KLMJ3 = 0, KLMJ4 = 0, KLMJ5 = 0;
                        KLMJ1 = Conversion.Val(sItem["KLC1"]) * Conversion.Val(sItem["KLK1"]);
                        KLMJ2 = Conversion.Val(sItem["KLC2"]) * Conversion.Val(sItem["KLK2"]);
                        KLMJ3 = Conversion.Val(sItem["KLC3"]) * Conversion.Val(sItem["KLK3"]);
                        KLMJ4 = Conversion.Val(sItem["KLC4"]) * Conversion.Val(sItem["KLK4"]);
                        KLMJ5 = Conversion.Val(sItem["KLC5"]) * Conversion.Val(sItem["KLK5"]);

                        sItem["KLQD1"] = Round(Conversion.Val(sItem["KLYZ1"]) * 1000 / KLMJ1, 2).ToString();
                        sItem["KLQD2"] = Round(Conversion.Val(sItem["KLYZ2"]) * 1000 / KLMJ2, 2).ToString();
                        sItem["KLQd3"] = Round(Conversion.Val(sItem["KLYZ3"]) * 1000 / KLMJ3, 2).ToString();
                        sItem["KLQD4"] = Round(Conversion.Val(sItem["KLYZ4"]) * 1000 / KLMJ4, 2).ToString();
                        sItem["KLQD5"] = Round(Conversion.Val(sItem["KLYZ5"]) * 1000 / KLMJ5, 2).ToString();
                        sItem["KLQD"] = Round((Conversion.Val(sItem["KLQD1"]) + Conversion.Val(sItem["KLQD2"]) + Conversion.Val(sItem["KLQD3"]) + Conversion.Val(sItem["KLQD4"]) + Conversion.Val(sItem["KLQD5"])) / 5, 2).ToString();
                        #endregion

                        if (IsQualified(MItem[0]["G_KLQD"], sItem["KLQD"], true) == "符合")
                        {
                            MItem[0]["HG_KLQD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_KLQD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["KLQD"] = "----";
                        MItem[0]["HG_KLQD"] = "----";
                        MItem[0]["G_KLQD"] = "----";
                    }
                    #endregion

                    #region 热阻(10℃)、热阻(25℃)
                    if (jcxm.Contains("、热阻(10℃)、") || jcxm.Contains("、热阻(25℃)、"))
                    {
                        jcxmCur = CurrentJcxm(jcxm, "热阻(10℃),热阻(25℃)");
                        if (IsQualified(MItem[0]["G_RZ1"], sItem["RZ1"], true) == "符合")
                        {
                            MItem[0]["HG_RZ1"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_RZ1"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }

                        if (IsQualified(MItem[0]["G_RZ2"], sItem["RZ2"], true) == "符合")
                        {
                            MItem[0]["HG_RZ2"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_RZ2"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["RZ1"] = "----";
                        sItem["RZ2"] = "----";
                        MItem[0]["HG_RZ1"] = "----";
                        MItem[0]["HG_RZ2"] = "----";
                        MItem[0]["G_RZ1"] = "----";
                        MItem[0]["G_RZ2"] = "----";
                    }
                    #endregion

                    #region 压缩强度
                    if (jcxm.Contains("、压缩强度、"))
                    {
                        jcxmCur = "压缩强度";
                        double mcd1 = 0, mkd1 = 0, mcd2 = 0, mkd2 = 0, mcd3 = 0, mkd3 = 0, mcd4 = 0, mkd4 = 0, mcd5 = 0, mkd5 = 0, mcd6 = 0, mkd6 = 0, mcd7 = 0, mkd7 = 0, mcd8 = 0, mkd8 = 0, mcd9 = 0, mkd9 = 0, mcd10 = 0, mkd10 = 0;
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            #region 初检
                            mcd1 = Round((Conversion.Val(sItem["BGMDC1_1"]) + Conversion.Val(sItem["BGMDC1_2"]) + Conversion.Val(sItem["BGMDC1_3"])) / 3, 1);
                            mkd1 = Round((Conversion.Val(sItem["BGMDK1_1"]) + Conversion.Val(sItem["BGMDK1_2"]) + Conversion.Val(sItem["BGMDK1_3"])) / 3, 1);
                            mcd2 = Round((Conversion.Val(sItem["BGMDC2_1"]) + Conversion.Val(sItem["BGMDC2_2"]) + Conversion.Val(sItem["BGMDC2_3"])) / 3, 1);
                            mkd2 = Round((Conversion.Val(sItem["BGMDK2_1"]) + Conversion.Val(sItem["BGMDK2_2"]) + Conversion.Val(sItem["BGMDK2_3"])) / 3, 1);
                            mcd3 = Round((Conversion.Val(sItem["BGMDC3_1"]) + Conversion.Val(sItem["BGMDC3_2"]) + Conversion.Val(sItem["BGMDC3_3"])) / 3, 1);
                            mkd3 = Round((Conversion.Val(sItem["BGMDK3_1"]) + Conversion.Val(sItem["BGMDK3_2"]) + Conversion.Val(sItem["BGMDK3_3"])) / 3, 1);
                            mcd4 = Round((Conversion.Val(sItem["BGMDC4_1"]) + Conversion.Val(sItem["BGMDC4_2"]) + Conversion.Val(sItem["BGMDC4_3"])) / 3, 1);
                            mkd4 = Round((Conversion.Val(sItem["BGMDK4_1"]) + Conversion.Val(sItem["BGMDK4_2"]) + Conversion.Val(sItem["BGMDK4_3"])) / 3, 1);
                            mcd5 = Round((Conversion.Val(sItem["BGMDC5_1"]) + Conversion.Val(sItem["BGMDC5_2"]) + Conversion.Val(sItem["BGMDC5_3"])) / 3, 1);
                            mkd5 = Round((Conversion.Val(sItem["BGMDK5_1"]) + Conversion.Val(sItem["BGMDK5_2"]) + Conversion.Val(sItem["BGMDK5_3"])) / 3, 1);
                            mcd6 = Round((Conversion.Val(sItem["BGMDC6_1"]) + Conversion.Val(sItem["BGMDC6_2"]) + Conversion.Val(sItem["BGMDC6_3"])) / 3, 1);
                            mkd6 = Round((Conversion.Val(sItem["BGMDK6_1"]) + Conversion.Val(sItem["BGMDK6_2"]) + Conversion.Val(sItem["BGMDK6_3"])) / 3, 1);
                            mcd7 = Round((Conversion.Val(sItem["BGMDC7_1"]) + Conversion.Val(sItem["BGMDC7_2"]) + Conversion.Val(sItem["BGMDC7_3"])) / 3, 1);
                            mkd7 = Round((Conversion.Val(sItem["BGMDK7_1"]) + Conversion.Val(sItem["BGMDK7_2"]) + Conversion.Val(sItem["BGMDK7_3"])) / 3, 1);
                            mcd8 = Round((Conversion.Val(sItem["BGMDC8_1"]) + Conversion.Val(sItem["BGMDC8_2"]) + Conversion.Val(sItem["BGMDC8_3"])) / 3, 1);
                            mkd8 = Round((Conversion.Val(sItem["BGMDK8_1"]) + Conversion.Val(sItem["BGMDK8_2"]) + Conversion.Val(sItem["BGMDK8_3"])) / 3, 1);
                            mcd9 = Round((Conversion.Val(sItem["BGMDC9_1"]) + Conversion.Val(sItem["BGMDC9_2"]) + Conversion.Val(sItem["BGMDC9_3"])) / 3, 1);
                            mkd9 = Round((Conversion.Val(sItem["BGMDK9_1"]) + Conversion.Val(sItem["BGMDK9_2"]) + Conversion.Val(sItem["BGMDK9_3"])) / 3, 1);
                            mcd10 = Round((Conversion.Val(sItem["BGMDC10_1"]) + Conversion.Val(sItem["BGMDC10_2"]) + Conversion.Val(sItem["BGMDC10_3"])) / 3, 1);
                            mkd10 = Round((Conversion.Val(sItem["BGMDK10_1"]) + Conversion.Val(sItem["BGMDK10_2"]) + Conversion.Val(sItem["BGMDK10_3"])) / 3, 1);

                            if (mcd1 == 0 && Conversion.Val(sItem["BGMDC1"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC1"] = mcd1.ToString("0.0");
                                sItem["BGMDK1"] = mkd1.ToString("0.0");
                            }

                            if (mcd2 == 0 && Conversion.Val(sItem["BGMDC2"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC2"] = mcd2.ToString("0.0");
                                sItem["BGMDK2"] = mkd2.ToString("0.0");
                            }

                            if (mcd3 == 0 && Conversion.Val(sItem["BGMDC3"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC3"] = mcd3.ToString("0.0");
                                sItem["BGMDK3"] = mkd3.ToString("0.0");
                            }

                            if (mcd4 == 0 && Conversion.Val(sItem["BGMDC4"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC4"] = mcd4.ToString("0.0");
                                sItem["BGMDK4"] = mkd4.ToString("0.0");
                            }

                            if (mcd5 == 0 && Conversion.Val(sItem["BGMDC5"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC5"] = mcd5.ToString("0.0");
                                sItem["BGMDK5"] = mkd5.ToString("0.0");
                            }

                            if (mcd6 == 0 && Conversion.Val(sItem["BGMDC6"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC6"] = mcd6.ToString("0.0");
                                sItem["BGMDK6"] = mkd6.ToString("0.0");
                            }

                            if (mcd7 == 0 && Conversion.Val(sItem["BGMDC7"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC7"] = mcd7.ToString("0.0");
                                sItem["BGMDK7"] = mkd7.ToString("0.0");
                            }

                            if (mcd8 == 0 && Conversion.Val(sItem["BGMDC8"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC8"] = mcd8.ToString("0.0");
                                sItem["BGMDK8"] = mkd8.ToString("0.0");
                            }

                            if (mcd9 == 0 && Conversion.Val(sItem["BGMDC9"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC9"] = mcd9.ToString("0.0");
                                sItem["BGMDK9"] = mkd9.ToString("0.0");
                            }

                            if (mcd10 == 0 && Conversion.Val(sItem["BGMDC10"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC10"] = mcd10.ToString("0.0");
                                sItem["BGMDK10"] = mkd10.ToString("0.0");
                            }

                            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0, mMj9 = 0, mMj10 = 0;
                            mMj1 = Conversion.Val(sItem["BGMDC1"]) * Conversion.Val(sItem["BGMDK1"]);
                            mMj2 = Conversion.Val(sItem["BGMDC2"]) * Conversion.Val(sItem["BGMDK2"]);
                            mMj3 = Conversion.Val(sItem["BGMDC3"]) * Conversion.Val(sItem["BGMDK3"]);
                            mMj4 = Conversion.Val(sItem["BGMDC4"]) * Conversion.Val(sItem["BGMDK4"]);
                            mMj5 = Conversion.Val(sItem["BGMDC5"]) * Conversion.Val(sItem["BGMDK5"]);
                            mMj6 = Conversion.Val(sItem["BGMDC6"]) * Conversion.Val(sItem["BGMDK6"]);
                            mMj7 = Conversion.Val(sItem["BGMDC7"]) * Conversion.Val(sItem["BGMDK7"]);
                            mMj8 = Conversion.Val(sItem["BGMDC8"]) * Conversion.Val(sItem["BGMDK8"]);
                            mMj9 = Conversion.Val(sItem["BGMDC9"]) * Conversion.Val(sItem["BGMDK9"]);
                            mMj10 = Conversion.Val(sItem["BGMDC10"]) * Conversion.Val(sItem["BGMDK10"]);

                            double mKyqd1 = 0, mKyqd2 = 0, mKyqd3 = 0, mKyqd4 = 0, mKyqd5 = 0, mKyqd6 = 0, mKyqd7 = 0, mKyqd8 = 0, mKyqd9 = 0, mKyqd10 = 0;
                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0 && mMj6 != 0 && mMj7 != 0 && mMj8 != 0 && mMj9 != 0 && mMj10 != 0)
                            {
                                mKyqd1 = Round(1000 * Conversion.Val(sItem["KYHZ1"]) / (mMj1), 2);
                                mKyqd2 = Round(1000 * Conversion.Val(sItem["KYHZ2"]) / (mMj2), 2);
                                mKyqd3 = Round(1000 * Conversion.Val(sItem["KYHZ3"]) / (mMj3), 2);
                                mKyqd4 = Round(1000 * Conversion.Val(sItem["KYHZ4"]) / (mMj4), 2);
                                mKyqd5 = Round(1000 * Conversion.Val(sItem["KYHZ5"]) / (mMj5), 2);
                                mKyqd6 = Round(1000 * Conversion.Val(sItem["KYHZ6"]) / (mMj6), 2);
                                mKyqd7 = Round(1000 * Conversion.Val(sItem["KYHZ7"]) / (mMj7), 2);
                                mKyqd8 = Round(1000 * Conversion.Val(sItem["KYHZ8"]) / (mMj8), 2);
                                mKyqd9 = Round(1000 * Conversion.Val(sItem["KYHZ9"]) / (mMj9), 2);
                                mKyqd10 = Round(1000 * Conversion.Val(sItem["KYHZ10"]) / (mMj10), 2);
                            }
                            else
                            {
                                mKyqd1 = 0;
                                mKyqd2 = 0;
                                mKyqd3 = 0;
                                mKyqd4 = 0;
                                mKyqd5 = 0;
                                mKyqd6 = 0;
                                mKyqd7 = 0;
                                mKyqd8 = 0;
                                mKyqd9 = 0;
                                mKyqd10 = 0;
                            }

                            List<double> lArray = new List<double>();
                            lArray.Add(mKyqd1);
                            lArray.Add(mKyqd2);
                            lArray.Add(mKyqd3);
                            lArray.Add(mKyqd4);
                            lArray.Add(mKyqd5);
                            lArray.Add(mKyqd6);
                            lArray.Add(mKyqd7);
                            lArray.Add(mKyqd8);
                            lArray.Add(mKyqd9);
                            lArray.Add(mKyqd10);
                            lArray.Sort();
                            sItem["YSQD"] = Round(lArray.Average(), 0).ToString();

                            #endregion
                        }
                        else
                        {
                            #region 初检
                            mcd1 = Round((Conversion.Val(sItem["BGMDC1_1"]) + Conversion.Val(sItem["BGMDC1_2"]) + Conversion.Val(sItem["BGMDC1_3"])) / 3, 1);
                            mkd1 = Round((Conversion.Val(sItem["BGMDK1_1"]) + Conversion.Val(sItem["BGMDK1_2"]) + Conversion.Val(sItem["BGMDK1_3"])) / 3, 1);
                            mcd2 = Round((Conversion.Val(sItem["BGMDC2_1"]) + Conversion.Val(sItem["BGMDC2_2"]) + Conversion.Val(sItem["BGMDC2_3"])) / 3, 1);
                            mkd2 = Round((Conversion.Val(sItem["BGMDK2_1"]) + Conversion.Val(sItem["BGMDK2_2"]) + Conversion.Val(sItem["BGMDK2_3"])) / 3, 1);
                            mcd3 = Round((Conversion.Val(sItem["BGMDC3_1"]) + Conversion.Val(sItem["BGMDC3_2"]) + Conversion.Val(sItem["BGMDC3_3"])) / 3, 1);
                            mkd3 = Round((Conversion.Val(sItem["BGMDK3_1"]) + Conversion.Val(sItem["BGMDK3_2"]) + Conversion.Val(sItem["BGMDK3_3"])) / 3, 1);
                            mcd4 = Round((Conversion.Val(sItem["BGMDC4_1"]) + Conversion.Val(sItem["BGMDC4_2"]) + Conversion.Val(sItem["BGMDC4_3"])) / 3, 1);
                            mkd4 = Round((Conversion.Val(sItem["BGMDK4_1"]) + Conversion.Val(sItem["BGMDK4_2"]) + Conversion.Val(sItem["BGMDK4_3"])) / 3, 1);
                            mcd5 = Round((Conversion.Val(sItem["BGMDC5_1"]) + Conversion.Val(sItem["BGMDC5_2"]) + Conversion.Val(sItem["BGMDC5_3"])) / 3, 1);
                            mkd5 = Round((Conversion.Val(sItem["BGMDK5_1"]) + Conversion.Val(sItem["BGMDK5_2"]) + Conversion.Val(sItem["BGMDK5_3"])) / 3, 1);


                            if (mcd1 == 0 && Conversion.Val(sItem["BGMDC1"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC1"] = mcd1.ToString("0.0");
                                sItem["BGMDK1"] = mkd1.ToString("0.0");
                            }

                            if (mcd2 == 0 && Conversion.Val(sItem["BGMDC2"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC2"] = mcd2.ToString("0.0");
                                sItem["BGMDK2"] = mkd2.ToString("0.0");
                            }

                            if (mcd3 == 0 && Conversion.Val(sItem["BGMDC3"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC3"] = mcd3.ToString("0.0");
                                sItem["BGMDK3"] = mkd3.ToString("0.0");
                            }

                            if (mcd4 == 0 && Conversion.Val(sItem["BGMDC4"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC4"] = mcd4.ToString("0.0");
                                sItem["BGMDK4"] = mkd4.ToString("0.0");
                            }

                            if (mcd5 == 0 && Conversion.Val(sItem["BGMDC5"]) > 0)
                            {

                            }
                            else
                            {
                                sItem["BGMDC5"] = mcd5.ToString("0.0");
                                sItem["BGMDK5"] = mkd5.ToString("0.0");
                            }

                            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0;
                            mMj1 = Conversion.Val(sItem["BGMDC1"]) * Conversion.Val(sItem["BGMDK1"]);
                            mMj2 = Conversion.Val(sItem["BGMDC2"]) * Conversion.Val(sItem["BGMDK2"]);
                            mMj3 = Conversion.Val(sItem["BGMDC3"]) * Conversion.Val(sItem["BGMDK3"]);
                            mMj4 = Conversion.Val(sItem["BGMDC4"]) * Conversion.Val(sItem["BGMDK4"]);
                            mMj5 = Conversion.Val(sItem["BGMDC5"]) * Conversion.Val(sItem["BGMDK5"]);

                            double mKyqd1 = 0, mKyqd2 = 0, mKyqd3 = 0, mKyqd4 = 0, mKyqd5 = 0;
                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0)
                            {
                                mKyqd1 = Round(1000 * Conversion.Val(sItem["KYHZ1"]) / (mMj1), 2);
                                mKyqd2 = Round(1000 * Conversion.Val(sItem["KYHZ2"]) / (mMj2), 2);
                                mKyqd3 = Round(1000 * Conversion.Val(sItem["KYHZ3"]) / (mMj3), 2);
                                mKyqd4 = Round(1000 * Conversion.Val(sItem["KYHZ4"]) / (mMj4), 2);
                                mKyqd5 = Round(1000 * Conversion.Val(sItem["KYHZ5"]) / (mMj5), 2);
                            }
                            else
                            {
                                mKyqd1 = 0;
                                mKyqd2 = 0;
                                mKyqd3 = 0;
                                mKyqd4 = 0;
                                mKyqd5 = 0;
                            }

                            List<double> lArray = new List<double>();
                            lArray.Add(mKyqd1);
                            lArray.Add(mKyqd2);
                            lArray.Add(mKyqd3);
                            lArray.Add(mKyqd4);
                            lArray.Add(mKyqd5);
                            lArray.Sort();
                            sItem["YSQD"] = Round(lArray.Average(), 0).ToString();
                            #endregion
                        }

                        if (IsQualified(sItem["G_YSQD"], sItem["YSQD"], true) == "符合")
                        {
                            sItem["HG_YSQD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_YSQD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["YSQD"] = "----";
                        sItem["HG_YSQD"] = "----";
                        sItem["G_YSQD"] = "----";
                    }

                    #endregion

                    #region 吸水率
                    if (jcxm.Contains("、吸水率、"))
                    {
                        jcxmCur = "吸水率";
                        if (IsQualified(MItem[0]["G_XSL"], sItem["XSL"], true) == "符合")
                        {
                            MItem[0]["HG_XSL"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_XSL"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["XSL"] = "----";
                        MItem[0]["HG_XSL"] = "----";
                        MItem[0]["G_XSL"] = "----";
                    }
                    #endregion

                    #region 燃烧氧指数
                    if (jcxm.Contains("、燃烧氧指数、"))
                    {
                        jcxmCur = "燃烧氧指数";
                        string mnlfy = "", mntfy = "";
                        if (sItem["RSNLFY1"] != "----")
                        {
                            mnlfy = mnlfy + sItem["RSNLFY1"].Trim();
                        }
                        if (sItem["RSNLFY2"] != "----")
                        {
                            mnlfy = mnlfy + sItem["RSNLFY2"].Trim();
                        }
                        if (sItem["RSNLFY3"] != "----")
                        {
                            mnlfy = mnlfy + sItem["RSNLFY3"].Trim();
                        }
                        if (sItem["RSNLFY4"] != "----")
                        {
                            mnlfy = mnlfy + sItem["RSNLFY4"].Trim();
                        }

                        if (sItem["RSNTFY1"] != "----")
                        {
                            mntfy = mntfy + sItem["RSNTFY1"].Trim();
                        }
                        if (sItem["RSNTFY2"] != "----")
                        {
                            mntfy = mntfy + sItem["RSNTFY2"].Trim();
                        }
                        if (sItem["RSNTFY3"] != "----")
                        {
                            mntfy = mntfy + sItem["RSNTFY3"].Trim();
                        }
                        if (sItem["RSNTFY4"] != "----")
                        {
                            mntfy = mntfy + sItem["RSNTFY4"].Trim();
                        }
                        if (sItem["RSNTFY5"] != "----")
                        {
                            mntfy = mntfy + sItem["RSNTFY5"].Trim();
                        }

                        double mkz = 0, moi = 0, mbzc = 0;
                        string mnlzhynd = "";
                        var mrsYzskb = yzskb.FirstOrDefault(u => u["HWC"] == mntfy.Trim() && u["QJC"] == mnlfy.Trim());
                        if (mrsYzskb != null)
                        {
                            mkz = Conversion.Val(mrsYzskb["k"]);
                        }

                        if (sItem["RSNTFY1"] == "0")
                        {
                            mkz = -mkz;
                        }

                        sItem["RSYZSKZ"] = mkz.ToString("0.00");
                        moi = Round(Conversion.Val(sItem["RSNTYND5"]) + mkz * Conversion.Val(sItem["RSYZSD"]), 2);
                        sItem["RSYZS"] = (int.Parse((moi * 10).ToString()) / 10).ToString("0.0");
                        mnlzhynd = sItem["RSNLYND" + mnlfy.Length];

                        mbzc = Math.Sqrt(((Conversion.Val(mnlzhynd) - moi) * (Conversion.Val(mnlzhynd) - moi) +
                                (Conversion.Val(sItem["RSNTYND1"]) - moi) * (Conversion.Val(sItem["RSNTYND1"]) - moi) +
                                (Conversion.Val(sItem["RSNTYND2"]) - moi) * (Conversion.Val(sItem["RSNTYND2"]) - moi) +
                                (Conversion.Val(sItem["RSNTYND3"]) - moi) * (Conversion.Val(sItem["RSNTYND3"]) - moi) +
                                (Conversion.Val(sItem["RSNTYND4"]) - moi) * (Conversion.Val(sItem["RSNTYND4"]) - moi) +
                                (Conversion.Val(sItem["RSNTYND5"]) - moi) * (Conversion.Val(sItem["RSNTYND5"]) - moi)) / 5);
                        sItem["RSBZPC"] = (Round(mbzc, 3)).ToString("0.000");
                        sItem["RSYSM"] = "";

                        if ((Conversion.Val(sItem["RSYZSD"]) == 0.2 && 0.2 > 3 / 2 * Conversion.Val(sItem["RSBZPC"])) || (2 / 3 * Conversion.Val(sItem["RSBZPC"]) < Conversion.Val(sItem["RSYZSD"]) && Conversion.Val(sItem["RSYZSD"]) < 3 / 2 * Conversion.Val(sItem["RSBZPC"])))
                        {
                            sItem["RSYSM"] = "有效";
                            if (IsQualified(MItem[0]["G_RSYZS"], sItem["RSYZS"], true) == "符合")
                            {
                                MItem[0]["HG_RSYZS"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_RSYZS"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["RSYSM"] = "无效";
                        }
                    }
                    else
                    {
                        sItem["RSYSM"] = "----";
                        sItem["RSYZS"] = "----";
                        MItem[0]["HG_RSYZS"] = "----";
                        MItem[0]["G_RSYZS"] = "----";
                    }
                    #endregion

                    #region 燃烧分级
                    if (jcxm.Contains("、燃烧分级、"))
                    {
                        jcxmCur = "燃烧分级";
                        if (sItem["RSFJ"] == "合格")
                        {
                            MItem[0]["HG_RSFJ"] = "合格";
                            sItem["RSFJJG"] = "符合E级";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_RSFJ"] = "不合格";
                            sItem["RSFJJG"] = "不符合";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["RSFJ"] = "----";
                        MItem[0]["HG_RSFJ"] = "----";
                        sItem["G_RSFJ"] = "----";
                    }
                    #endregion
                }


            }

            #region 添加最终报告

            if (mAllHg && mJCJG != "----")
            {
                mJCJG = "合格";
                if (MItem[0]["SFFJ"] == "复检")   //复检
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目均符合要求。";
                }
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                }
            }
            else
            {
                mJCJG = "不合格";
                if (MItem[0]["SFFJ"] == "复检")   //复检
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                }
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，需双倍复检。";
                }
                    
            }


            MItem[0]["JCJG"] = mJCJG;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}