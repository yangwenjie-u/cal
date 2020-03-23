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
            #region
            #region  参数定义
            bool mAllHg;
            bool mFlag_Hg, mFlag_Bhg;
            //当前项目的变量声明
            int mbhggs, mbhggs1, mbhgpds = 0;
            int mhgpds = 0;
            string mGxl, mSjdj;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_GC_DJ"];
            var mrscyfa = dataExtra["BZ_GCCYFA"];
            var mrslccj = dataExtra["BZ_GCLCCJ"];
            var mrsYysycs = dataExtra["BZ_GCYYSYCS"];
            //外观尺寸
            var mrsWgcc = dataExtra["BZ_GCWGCC"];
            //壁厚公差
            var mrsBhgc = dataExtra["BZ_GCBHGC"];

            var MItem = data["M_GC"];
            var mitem = MItem[0];
            var SItem = data["S_GC"];
            #endregion

            #region 计算开始
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
            List<string> mtmpArray = new List<string>();
            int jcxmCount; //启用的检查项目个数
            int curJcxmCount; //现在处理的是第几个检测项目

            var jcxm = "";
            foreach (var sitem in SItem)
            {
                mSjdj = sitem["SJDJ"]; //管材名称

                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                mtmpArray = sitem["JCXM"].Replace(",", "、").Split('、').ToList();

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
                }
                else
                {
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "获取标准要求出错，找不到对应项";
                    continue;
                }
                mbhggs = 0;
                mbhggs1 = 0;
                jcxmCount = mtmpArray.Count;
                curJcxmCount = 0;
                int xd, Gs;
                int md;
                double md1, md2;
                mbhggs = 0;
                //以下初始化报告字段
                for (xd = 0; xd <= 9; xd++)
                {
                    sitem["BGJCXM" + xd] = "";
                    sitem["BGDW" + xd] = "";
                    sitem["BGBZYQ" + xd] = "";
                    sitem["BGSCJG" + xd] = "";
                    sitem["BGDXPD" + xd] = "";
                }

                if (string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    //抽样检测
                    var dfd = GetSafeDouble(MItem[0]["WGBHGS"]);

                    if (dfd > 0 && mhgpds >= dfd)
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }


                    if (jcxm.Contains("、外观颜色、"))
                    {
                        //13663.2-2018
                        //MItem[0]["WG"] = "符合";
                        MItem[0]["G_WG"] = "管材内外表面应清洁、光滑, 不应有气泡、明显的划伤、凹陷、杂质、颜色不均等缺陷。 管材两端应切割平整, 并与管材轴线垂直。";
                        //MItem[0]["WG_HG"] = "合格";

                        //MItem[0]["BZ"] = "符合";
                        MItem[0]["G_BZ"] = "管材应为黑色或蓝色, 黑色管材上应共挤出 至少三条蓝色条, 色条应沿管材圆周方向均匀分布。蓝色管材仅用于暗敷。";
                        //MItem[0]["BZ_HG"] = "合格";

                        if (MItem[0]["WG_HG"] == "合格" && MItem[0]["BZ_HG"] == "合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }

                        for (xd = 0; xd < jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("外观颜色"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = "外观";
                                sitem["BGDW" + curJcxmCount] = "----";
                                sitem["BGBZYQ" + curJcxmCount] = mitem["G_WG"];
                                sitem["BGSCJG" + curJcxmCount] = mitem["WG"];
                                sitem["BGDXPD" + curJcxmCount] = mitem["WG_HG"];
                                curJcxmCount = curJcxmCount + 1;

                                sitem["BGJCXM" + curJcxmCount] = "颜色";
                                sitem["BGDW" + curJcxmCount] = "----";
                                sitem["BGBZYQ" + curJcxmCount] = mitem["G_BZ"];
                                sitem["BGSCJG" + curJcxmCount] = mitem["BZ"];
                                sitem["BGDXPD" + curJcxmCount] = mitem["BZ_HG"];
                                curJcxmCount = curJcxmCount + 1;
                                break;
                            }
                        }

                    }
                    else
                    {
                        MItem[0]["WG_HG"] = "----";
                        MItem[0]["G_WG"] = "----";
                        MItem[0]["G_BZ"] = "----";
                        MItem[0]["BZ_HG"] = "----";
                    }

                    if (jcxm.Contains("、规格尺寸、"))
                    {
                        //测试的数量4-12个
                        //1 长度 2.平均外径 3.不圆度 4.壁厚公差
                        int count = Convert.ToInt32(sitem["ZHCLSL"]);

                        //长度
                        MItem[0]["G_GCCD"] = "长度一般为6m 、9m、12m,也可由供需双方商定。";
                        MItem[0]["HG_GCCD"] = "";
                        MItem[0]["HG_GCCD"] = MItem[0]["HG_GCCD"];


                        //MItem[0]["HG_ZGBYD"] = "依据不详";
                        //MItem[0]["PJWJ_HG"] = "依据不详";
                        //MItem[0]["HG_BHGC"] = "依据不详";
                        var ddf = MItem[0]["HG_BHGC"];
                        ddf = MItem[0]["PJWJ_HG"];
                        ddf = MItem[0]["HG_ZGBYD"];
                        MItem[0]["HG_BHGC"] = MItem[0]["HG_BHGC"];
                        //MItem[0]["HG_ZGBYD"] = MItem[0]["HG_ZGBYD"];
                        MItem[0]["PJWJ_HG"] = MItem[0]["PJWJ_HG"];

                        for (xd = 0; xd < jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("规格尺寸"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = "平均外径";
                                sitem["BGDW" + curJcxmCount] = "----";
                                sitem["BGSCJG" + curJcxmCount] = MItem[0]["PJWJ"];
                                sitem["BGBZYQ" + curJcxmCount] = MItem[0]["G_PJWJ"];
                                sitem["BGDXPD" + curJcxmCount] = MItem[0]["PJWJ_HG"];
                                curJcxmCount = curJcxmCount + 1;

                                sitem["BGJCXM" + curJcxmCount] = "壁厚";
                                sitem["BGDW" + curJcxmCount] = "----";
                                sitem["BGSCJG" + curJcxmCount] = sitem["GCBH"];
                                sitem["BGBZYQ" + curJcxmCount] = MItem[0]["G_GCBH"];
                                sitem["BGDXPD" + curJcxmCount] = MItem[0]["HG_BHGC"];
                                curJcxmCount = curJcxmCount + 1;

                                break;
                            }
                        }

                        ////平均外径，不圆度
                        //var mrsWgcc_Filter = mrsWgcc.FirstOrDefault(x => x["MC"].Contains(mSjdj) && x["WJ"] == sitem["GCWJ"]);
                        //if (mrsWgcc_Filter != null && mrsWgcc_Filter.Count() > 0)
                        //{
                        //    double wjMax = 0;
                        //    double wjMin = 0;
                        //    double sum = 0;
                        //    List<double> listWJ = new List<double>();
                        //    // 2.平均外径
                        //    for (int i = 1; i <= count; i++)
                        //    {
                        //        md1 = GetSafeDouble(sitem["WJ" + i]);
                        //        listWJ.Add(md1);
                        //        sum += md1;
                        //    }
                        //    listWJ.Sort();

                        //    //如果直径《=600，修约0.1
                        //    //《=1600，修约0.2
                        //    //》1600，修约1

                        //    sitem["PJWJ"] = listWJ.Average().ToString("0.0");

                        //    MItem[0]["G_PJWJ"] = mrsWgcc_Filter["WJMin"];//平均外径最小
                        //    MItem[0]["G_PJWJ1"] = mrsWgcc_Filter["WJMax"];//平均外径最大 
                        //    MItem[0]["PJWJ_HG"] = IsQualified("≥" + mitem["G_PJWJ"], sitem["PJWJ"]);

                        //    if (MItem[0]["PJWJ_HG"] != "合格")
                        //    {
                        //        MItem[0]["PJWJ_HG"] = IsQualified("≤" + mitem["G_PJWJ1"], sitem["PJWJ"]);
                        //    }

                        //    wjMax = listWJ[0];
                        //    wjMin = listWJ[count - 1];

                        //    //不圆度
                        //    MItem[0]["G_ZGBYD"] = mrsWgcc_Filter["ZGBYD"];
                        //    MItem[0]["HG_ZGBYD"] = IsQualified("<" + mitem["G_ZGBYD"], sitem["ZGBYD"]);
                        //}
                        //else
                        //{
                        //    MItem[0]["HG_ZGBYD"] = "依据不详";
                        //    MItem[0]["PJWJ_HG"] = "依据不详";
                        //}
                        ////4.壁厚公差
                        //double bhgc = 0.0;//数据库获取
                        //double GCBH = GetSafeDouble(sitem["GCBH"]);//公称壁厚(mm)
                        //var mrsBhgc_Filter = mrsBhgc.FirstOrDefault(x => x["MC"].Contains(mSjdj) && GetSafeDouble(x["GCBHMin"]) < GCBH && GetSafeDouble(x["GCBHMax"]) >= GCBH);

                        //if (mrsBhgc_Filter != null && mrsBhgc_Filter.Count() > 0)
                        //{
                        //    bhgc = GetDouble(mrsBhgc_Filter["BHGC"]);
                        //    MItem[0]["HG_BHGC"] = IsQualified((Convert.ToDouble(mitem["G_GCBH"]) + bhgc).ToString() + (Convert.ToDouble(mitem["G_GCBH"]) - bhgc).ToString(), sitem["GCBH"]);
                        //}
                        //else
                        //{
                        //    MItem[0]["HG_BHGC"] = "依据不详";
                        //}
                    }
                    //if (jcxm.Contains("、平均外径、"))
                    //{
                    //    MItem[0]["G_PJWJ1"] = MItem[0]["G_PJWJ"] + MItem[0]["G_PJWJPC"];

                    //    if (mhgpds >= Convert.ToInt16(MItem[0]["WJBHGS"]))
                    //    {
                    //        MItem[0]["PJWJ_HG"] = "合格";
                    //        MItem[0]["WJJGSM"] = "符合";
                    //        mFlag_Hg = true;
                    //    }
                    //    if (Convert.ToInt16(MItem[0]["WJBHGS"]) >= mbhgpds)
                    //    {
                    //        MItem[0]["PJWJ_HG"] = "不合格";
                    //        MItem[0]["WJJGSM"] = "不符合";
                    //        mbhggs = mbhggs + 1;
                    //        mFlag_Bhg = true;
                    //    }
                    //}
                    //else
                    //{
                    //    MItem[0]["PJWJ_HG"] = "----";
                    //    MItem[0]["G_PJWJ"] = "0";
                    //    MItem[0]["G_PJWJPC"] = "0";
                    //}
                    if (jcxm.Contains("、落锤冲击试验、"))
                    {
                        mitem["LCCJ"] = "";
                        mitem["LCCJ_HG"] = "";
                        if (mitem["G_LCCJ"].Contains("≤10"))
                        {
                            IDictionary<string, string> mrslccj_Sel = new Dictionary<string, string>();
                            foreach (var mrslccj_item in mrslccj)
                            {
                                if (GetSafeDouble(mrslccj_item["LCCJCS"]) == GetSafeDouble(mitem["LCCJCS"]))
                                    break;
                                mrslccj_Sel = mrslccj_item;
                            }

                            if (mrslccj_Sel != null)
                            {
                                md1 = Conversion.Val(mitem["LCCJBHGS"].Trim());
                                md2 = Conversion.Val(mitem["LCCJCS"].Trim());
                                md = (int)(100 * md1 / md2);
                                mitem["LCCJ"] = Round(md, 0).ToString("0.0");

                                if (IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false) == "符合")
                                {
                                    MItem[0]["LCCJ_HG"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    MItem[0]["LCCJ_HG"] = "不合格";
                                    mbhggs1 = mbhggs1 + 1;
                                    mFlag_Hg = true;
                                }
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
                                mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
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
                        for (xd = 0; xd < jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("落锤冲击试验"))
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

                    if (jcxm.Contains("、液压试验、"))
                    {
                        string[] yysyArray;
                        string yysyCs;
                        yysyArray = sitem["YYSYCS"].Replace(',', '、').Split('、');
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
                                mitem["YYSY" + xd] = "不破裂、不渗漏";
                                mFlag_Hg = true;
                            }
                        }
                        //向报告用字段赋值
                        for (xd = 0; xd < jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("液压试验"))
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

                    if (jcxm.Contains("、维卡软化温度、"))
                    {
                        mitem["RHWD_HG"] = IsQualified(mitem["G_RHWD"], mitem["RHWD"], false);
                        mbhggs = mitem["RHWD_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        //向报告用字段赋值
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("维卡软化温度"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGJCXM" + curJcxmCount] = sitem["BGJCXM" + curJcxmCount] + "(℃)";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_RHWD"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["RHWD"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["RHWD_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["RHWD"] = "----";
                        mitem["RHWD_HG"] = "----";
                        mitem["G_RHWD"] = "----";
                    }

                    if (jcxm.Contains("、纵向回缩率、"))
                    {
                        double zxhsl1, zxhsl2, zxhsl3 = 0.0;
                        if ((Conversion.Val(MItem[0]["HSLL0_1"]) == 0 || Conversion.Val(MItem[0]["HSLL0_2"]) == 0 || Conversion.Val(MItem[0]["HSLL0_3"]) == 0))
                        {
                            MItem[0]["ZXHSL"] = "0.0%";
                        }
                        else
                        {
                            zxhsl1 = Math.Abs(Double.Parse((100 * (Conversion.Val(MItem[0]["HSLL0_1"]) - Conversion.Val(MItem[0]["HSLLI_1"])) / Conversion.Val(MItem[0]["HSLL0_1"])).ToString("0.00")));
                            zxhsl2 = Math.Abs(Double.Parse((100 * (Conversion.Val(MItem[0]["HSLL0_2"]) - Conversion.Val(MItem[0]["HSLLI_2"])) / Conversion.Val(MItem[0]["HSLL0_2"])).ToString("0.00")));
                            zxhsl3 = Math.Abs(Double.Parse((100 * (Conversion.Val(MItem[0]["HSLL0_3"]) - Conversion.Val(MItem[0]["HSLLI_3"])) / Conversion.Val(MItem[0]["HSLL0_3"])).ToString("0.00")));
                            MItem[0]["ZXHSL"] = ((zxhsl1 + zxhsl2 + zxhsl3) / 3).ToString("0.0") + "%";

                        }
                        mitem["ZXHSL_HG"] = IsQualified(mitem["G_ZXHSL"], mitem["ZXHSL"], false);
                        if (MItem[0]["ZXHSL_HG"] == "合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mbhggs1 = mbhggs1 + 1;
                            mFlag_Bhg = true;
                        }
                        //  '向报告用字段赋值
                        for (xd = 0; xd < jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("纵向回缩率"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_ZXHSL"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["ZXHSL"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["ZXHSL_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["ZXHSL"] = "0";
                        mitem["ZXHSL_HG"] = "----";
                        mitem["G_ZXHSL"] = "0";
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
                else
                {
                    if (sitem["JCXM"].Trim().Contains("、落锤冲击试验、"))
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
                                md1 = string.IsNullOrEmpty(mitem["LCCJBHGS"]) ? 0 : Conversion.Val(mitem["LCCJBHGS"]);
                                md2 = string.IsNullOrEmpty(mitem["LCCJCS"]) ? 0 : Conversion.Val(mitem["LCCJCS"]);
                                if (md2 == 0)
                                {
                                    md = 0;
                                }
                                else
                                {
                                    md = (int)(100 * md1 / md2);
                                }

                                //md1 = Conversion.Val(mitem["LCCJBHGS"].Trim());
                                //md2 = Conversion.Val(mitem["LCCJCS"].Trim());

                                //md = (int)(100 * md1 / md2);
                                mitem["LCCJ"] = Round(md, 0).ToString("0.0");
                                mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
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
                                mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
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
                        for (xd = 0; xd < jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("落锤冲击试验"))
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
                            mitem["JZLCJ_HG"] = IsQualified(mitem["G_JZLCJ"], mitem["JZLCJ"], false);
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
                        mitem["ZXHSL_HG"] = IsQualified(mitem["G_ZXHSL"], mitem["ZXHSL"], false);
                        mbhggs = mitem["ZXHSL_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd < jcxmCount; xd++)
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
                        mitem["HGD_HG"] = IsQualified(mitem["G_HGD"], mitem["HGD"], false);
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
                    if (sitem["JCXM"].Trim().Contains("、液压试验、"))
                    {
                        string[] yysyArray;
                        string yysyCs;
                        yysyArray = sitem["YYSYCS"].Replace(',', '、').Split('、');
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
                            if (mtmpArray[xd].Contains("液压试验"))
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
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}