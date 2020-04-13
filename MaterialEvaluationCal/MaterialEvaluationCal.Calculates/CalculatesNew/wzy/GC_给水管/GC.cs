﻿using System;
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
            #region 计算开始
            var data = retData;
            var mrsDj = dataExtra["BZ_GC_DJ"];
            var mrscyfa = dataExtra["BZ_GCCYFA"];
            var mrslccj = dataExtra["BZ_GCLCCJ"];
            var mrsYysycs = dataExtra["BZ_GCYYSYCS"];
            var mrsWgcc = dataExtra["BZ_GCWGCC"];//外观尺寸
            var mrsBhgc = dataExtra["BZ_GCBHGC"];//壁厚公差
            var MItem = data["M_GC"];
            var mitem = MItem[0];
            var SItem = data["S_GC"];

            bool mAllHg;
            bool mFlag_Hg, mFlag_Bhg;
            bool realBhg = false;//标识直接不合格 。外观颜色不合格就不需要复试，直接不合格
            //当前项目的变量声明
            int mbhggs = 0;
            int mbhggs1, mbhgpds = 0;
            int mhgpds = 0;
            string mGxl, mSjdj;

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
            var jcxmBhg = "";
            var jcxmCur = "";
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;

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
                    //mitem["G_PJWJ"] = string.IsNullOrEmpty(mrsDj_Filter["PJWJ"]) ? "0" : mrsDj_Filter["PJWJ"].Trim();
                    //mitem["G_PJWJPC"] = string.IsNullOrEmpty(mrsDj_Filter["PJWJPC"]) ? "0" : mrsDj_Filter["PJWJPC"].Trim();
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
                    mitem["JCJGMS"] = "获取标准要求出错，找不到对应项";
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

                if (jcxm.Contains("、外观颜色、"))
                {
                    jcxmCur = "外观颜色";
                    MItem[0]["G_WG"] = "管材内外表面应清洁、光滑, 不应有气泡、明显的划伤、凹陷、杂质、颜色不均等缺陷。 管材两端应切割平整, 并与管材轴线垂直。";
                    MItem[0]["G_BZ"] = "管材应为黑色或蓝色, 黑色管材上应共挤出 至少三条蓝色条, 色条应沿管材圆周方向均匀分布。蓝色管材仅用于暗敷。";
                    if (mSjdj.Contains("给水用聚乙烯"))
                    {

                    }
                    else if (mSjdj.Contains("给水用低密度聚乙烯"))
                    {
                        MItem[0]["G_WG"] = "管材内外壁应光滑平整，不允许有气泡、裂纹、分解变色线及明显的沟槽、杂质等，管材切口应平整且与轴线垂直。";
                        MItem[0]["G_BZ"] = "管材颜色一般为黑色，其他颜色也可由供需双方商定。";
                    }
                    else if (mSjdj.Contains("冷热水用聚丁烯"))
                    {
                        MItem[0]["G_WG"] = "管材内外表面应平整、清洁、光滑, 不应有可能影响产品性能的明显的划伤、凹陷、杂质、颜色不均等缺陷。表面颜色应均匀一致，不允许有明显色差。";
                        MItem[0]["G_BZ"] = "由供需双方协商商定。";
                    }
                    else if (mSjdj.Contains("冷热水用耐热聚乙烯"))
                    {
                        MItem[0]["G_WG"] = "管材内外表面应平整、清洁、光滑, 不应有可能影响产品性能的明显的划伤、凹陷、杂质、颜色不均等缺陷。表面颜色应均匀一致，不允许有明显色差。管材切口应平整且与轴线垂直。";
                        MItem[0]["G_BZ"] = "地暖管材宜为本色，生活饮用水管材宜为灰色，其他颜色由供需双方协商确定。";
                    }
                    else if (mSjdj.Contains("冷热水用聚丙烯管道"))
                    {
                        MItem[0]["G_WG"] = "表面颜色应均匀一致，不允许有明显色差。管材内外表面应平整、清洁、光滑, 不应有可能影响产品性能的明显的划伤、凹陷、杂质、颜色不均等缺陷。管材切口应平整且与轴线垂直。";
                        MItem[0]["G_BZ"] = "一般为灰色，其他颜色由供需双方协商确定。";
                    }
                    else if (mSjdj.Contains("冷热水用耐热聚乙烯"))
                    {
                        MItem[0]["G_WG"] = "管材内外表面应平整、清洁、光滑, 不应有可能影响产品性能的明显的划伤、凹陷、杂质、颜色不均等缺陷。表面颜色应均匀一致，不允许有明显色差。管材切口应平整且与轴线垂直。";
                        MItem[0]["G_BZ"] = "地暖管材宜为本色，生活饮用水管材宜为灰色，其他颜色由供需双方协商确定。";
                    }
                    else if (mSjdj.Contains("建筑给水交联聚乙烯"))
                    {
                        MItem[0]["G_WG"] = "管材内外壁应光滑平整，不允许有气泡、裂口和明显划痕、凹陷、色差及分解变色。管材两端应切削整且与轴线垂直。";
                        MItem[0]["G_BZ"] = "通用型管材一般为白色，其他颜色由供需双方协商确定。";
                    }
                    else if (mSjdj.Contains("给水用硬聚氯乙烯"))
                    {
                        MItem[0]["G_WG"] = "管材内外表面应光滑，无明显划痕、凹陷、可见杂质和其他影响达到本部分要求的表面缺陷。管材两端应切削整且与轴线垂直。";
                        MItem[0]["G_BZ"] = "管材颜色由供需双方协商确定，色泽应均匀一致。";
                    }
                    if (MItem[0]["WG_HG"] == "合格" && MItem[0]["BZ_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        realBhg = true;
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    sitem["BGBZYQ1"] = "----";
                    sitem["BGSCJG1"] = "----";
                    sitem["BGDXPD1"] = "----";
                    sitem["BGBZYQ0"] = "----";
                    sitem["BGSCJG0"] = "----";
                    sitem["BGDXPD0"] = "----";
                }

                if (jcxm.Contains("、规格尺寸、"))
                {
                    jcxmCur = "规格尺寸";

                    //测试的数量4-12个
                    //1 长度 2.平均外径 3.不圆度 4.壁厚公差
                    int count = Convert.ToInt32(sitem["ZJCSGS"]);

                    if (count < 4)
                    {
                        throw new Exception("要求直径测量数量不能小于4个");
                    }
                    //长度
                    MItem[0]["G_GCCD"] = "长度一般为6m 、9m、12m,也可由供需双方商定。";
                    MItem[0]["HG_GCCD"] = "";
                    MItem[0]["HG_GCCD"] = MItem[0]["HG_GCCD"];

                    #region
                    ////平均外径
                    //var mrsWgcc_Filter = mrsWgcc.FirstOrDefault(x => x["MC"].Contains(mSjdj) && x["WJ"] == sitem["GCWJ"]);
                    //if (mrsWgcc_Filter != null && mrsWgcc_Filter.Count() > 0)
                    //{
                    List<string> listWJ_G = new List<string>();
                    listWJ_G = MItem[0]["G_PJWJ"].Split('~').ToList();
                    if (listWJ_G.Count != 2)
                    {
                        throw new Exception("请输入平均外径标准范围.");
                    }
                    //MItem[0]["G_PJWJ"] = listWJ_G[0];//平均外径最小
                    //MItem[0]["G_PJWJ1"] = listWJ_G[1];//平均外径最大 
                    double sum = 0;

                    //壁厚
                    List<string> listBH_G = new List<string>();
                    listBH_G = MItem[0]["G_GCBH"].Split('~').ToList();
                    if (listWJ_G.Count != 2)
                    {
                        throw new Exception("请输入平均壁厚标准范围.");
                    }

                    for (int i = 1; i < 3; i++)
                    {
                        #region 外径
                        List<double> listWJ = new List<double>();
                        // 2.平均外径
                        count = count >= 12 ? 12 : count;
                        for (int j = 1; j <= count; j++)
                        {
                            md1 = GetSafeDouble(sitem["WJ" + i + "_" + +j]);
                            listWJ.Add(md1);
                        }
                        listWJ.Sort();
                        var pjVal = listWJ.Average();
                        //《=1600，修约0.2
                        //》1600，修约1
                        var zj = GetSafeDouble(sitem["GCWJ"]);
                        if (zj <= 600)
                        {
                            MItem[0]["PJWJ" + i] = RoundEx(GetDouble(pjVal.ToString()), 1).ToString();
                        }
                        else if (zj <= 1600)
                        {
                            MItem[0]["PJWJ" + i] = (Round(GetDouble(pjVal.ToString()) * 5, 0) / 5).ToString("0.0");
                        }
                        else
                        {
                            MItem[0]["PJWJ" + i] = (Round(GetDouble(pjVal.ToString()) * 5, 0) / 5).ToString();
                        }
                        #endregion

                        List<double> listBH = new List<double>();
                        for (int j = 1; j <= count; j++)
                        {
                            md1 = GetSafeDouble(sitem["SCBH" + i + "_" + j]);
                            listBH.Add(md1);
                        }
                        listBH.Sort();
                        pjVal = listBH.Average();

                        var listMax = listBH[0];
                        var listMin = listBH[count - 1];
                        //如果直径《=10，修约0.05
                        //《=30，修约0.1
                        //>30，修约0.1
                        var bh = GetSafeDouble(sitem["GCBH"]);
                        if (bh <= 10)
                        {
                            sitem["PJBH" + i] = (Round(GetDouble(pjVal.ToString()) / 5, 2) * 5).ToString("0.00");
                        }
                        else if (bh > 10 && bh <= 30)
                        {
                            sitem["PJBH" + i] = Round(GetDouble(pjVal.ToString()), 1).ToString("0.0");
                        }
                        else
                        {
                            sitem["PJBH" + i] = Round(GetDouble(pjVal.ToString()), 1).ToString("0.0");
                        }
                    }
                    if (GetSafeDouble(MItem[0]["PJWJ1"]) > GetSafeDouble(MItem[0]["PJWJ2"]))
                    {
                        MItem[0]["PJWJ"] = MItem[0]["PJWJ2"] + "~" + MItem[0]["PJWJ1"];
                    }
                    else if (GetSafeDouble(MItem[0]["PJWJ1"]) < GetSafeDouble(MItem[0]["PJWJ2"]))
                    {
                        MItem[0]["PJWJ"] = MItem[0]["PJWJ1"] + "~" + MItem[0]["PJWJ2"];
                    }
                    else
                    {
                        MItem[0]["PJWJ"] = MItem[0]["PJWJ1"];
                    }
                    if (GetSafeDouble(sitem["PJBH1"]) > GetSafeDouble(sitem["PJBH2"]))
                    {
                        sitem["PJBH"] = sitem["PJBH2"] + "~" + sitem["PJBH1"];
                    }
                    else if (GetSafeDouble(sitem["PJBH1"]) < GetSafeDouble(sitem["PJBH2"]))
                    {
                        sitem["PJBH"] = sitem["PJBH1"] + "~" + sitem["PJBH2"];
                    }
                    else
                    {
                        sitem["PJBH"] = sitem["PJBH1"];
                    }


                    MItem[0]["PJWJ_HG"] = IsQualified(mitem["G_PJWJ"], MItem[0]["PJWJ1"]);

                    if (MItem[0]["PJWJ_HG"] == "合格")
                    {
                        MItem[0]["PJWJ_HG"] = IsQualified("≤" + mitem["G_PJWJ1"], MItem[0]["PJWJ2"]);
                    }


                    MItem[0]["HG_GCBH"] = IsQualified(MItem[0]["G_GCBH"], sitem["PJBH1"]);
                    if (MItem[0]["HG_GCBH"] == "合格")
                    {
                        MItem[0]["HG_GCBH"] = IsQualified(MItem[0]["G_GCBH"], sitem["PJBH2"]);
                    }

                    if (MItem[0]["HG_GCBH"] == "合格" && MItem[0]["PJWJ_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        realBhg = true;
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                    #endregion

                    for (xd = 0; xd < jcxmCount; xd++)
                    {
                        if (mtmpArray[xd].Contains("规格尺寸"))
                        {
                            sitem["BGJCXM" + curJcxmCount] = "平均外径(mm)";
                            sitem["BGDW" + curJcxmCount] = "----";
                            sitem["BGSCJG" + curJcxmCount] = MItem[0]["PJWJ"];
                            sitem["BGBZYQ" + curJcxmCount] = MItem[0]["G_PJWJ"];
                            sitem["BGDXPD" + curJcxmCount] = MItem[0]["PJWJ_HG"];
                            curJcxmCount = curJcxmCount + 1;

                            sitem["BGJCXM" + curJcxmCount] = "壁厚(mm)";
                            sitem["BGDW" + curJcxmCount] = "----";
                            sitem["BGSCJG" + curJcxmCount] = sitem["PJBH"];
                            sitem["BGBZYQ" + curJcxmCount] = MItem[0]["G_GCBH"];
                            sitem["BGDXPD" + curJcxmCount] = MItem[0]["HG_GCBH"];
                            curJcxmCount = curJcxmCount + 1;
                            break;
                        }
                    }
                }
                else
                {
                    sitem["BGBZYQ3"] = "----";
                    sitem["BGSCJG3"] = "----";
                    sitem["BGDXPD3"] = "----";
                    sitem["BGBZYQ2"] = "----";
                    sitem["BGSCJG2"] = "----";
                    sitem["BGDXPD2"] = "----";
                }
                if (jcxm.Contains("、液压试验、"))
                {
                    jcxmCur = "液压试验";
                    string[] yysyArray;
                    string yysyCs;
                    yysyArray = sitem["YYSYCS"].Replace(',', '、').Split('、');
                    Gs = yysyArray.Length;


                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (mitem["YYSY_HG" + xd] == "不合格")
                        {
                            //mitem["YYSY" + xd] = "有破裂、有渗漏";
                            mbhggs = mbhggs + 1;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            //mitem["YYSY" + xd] = "不破裂、不渗漏";
                            mFlag_Hg = true;
                        }
                    }

                    int yyCount = 1;
                    //向报告用字段赋值
                    //var mrsYysycs_Where = mrsYysycs.Where(x => x["MC"].Contains(sitem["SJDJ"].Trim()));没有效果

                    for (xd = 0; xd < jcxmCount; xd++)
                    {
                        if (mtmpArray[xd].Contains("液压试验"))
                        {
                            for (md = 1; md <= Gs; md++)
                            {
                                yysyCs = yysyArray[md - 1];
                                string syyl = "";

                                foreach (var mrsYysycs_item in mrsYysycs)
                                {
                                    if (mrsYysycs_item["MC"].Contains(sitem["SJDJ"].Trim()) && mrsYysycs_item["SYCS"].Contains(yysyCs))
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
                                }
                                if (yysyCs.Contains("，"))
                                    yysyCs = yysyCs.Replace("，", " " + syyl + " ");
                                else
                                    yysyCs = yysyCs.Replace(" ", " " + syyl + " ");
                                sitem["BGJCXM" + (curJcxmCount + md - 1)] = "液压试验";//检测项目  
                                var bh = Conversion.Val(sitem["GCBH"]);
                                var ztsj = "";
                                if (bh < 3)
                                {
                                    ztsj = "1h±5min;";
                                }
                                else if (bh >= 3 && bh < 8)
                                {
                                    ztsj = "3h±15min;";
                                }
                                else if (bh >= 8 && bh < 16)
                                {
                                    ztsj = "6h±30min;";
                                }
                                else if (bh >= 16 && bh < 32)
                                {
                                    ztsj = "10h±1h;";
                                }
                                else
                                {
                                    ztsj = "16h±1h;";
                                }
                                sitem["YYSYZT"] = "试验制备条件：状态调节时间：" + ztsj + "；试验环境：" + sitem["YYSYHJ"] + "；密封接头类型：" + sitem["YYSYJTLX"];
                                //sitem["BGJCXM" + (curJcxmCount + md - 1)] = mtmpArray[xd] + yysyCs;

                                if (!string.IsNullOrEmpty(yysyCs))
                                {
                                    sitem["BGDW" + (curJcxmCount)] = "----";
                                    //if (sitem["CLDJ"] == "")
                                    sitem["BGBZYQ" + (curJcxmCount)] = yysyCs + mitem["G_YYSY" + yyCount];
                                    sitem["BGSCJG" + (curJcxmCount)] = mitem["YYSY" + yyCount];
                                    sitem["BGDXPD" + (curJcxmCount)] = mitem["YYSY_HG" + yyCount];
                                    curJcxmCount = curJcxmCount + 1;
                                    yyCount++;
                                }
                            }
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 4; i < 8; i++)
                    {
                        sitem["BGBZYQ" + i] = "----";
                        sitem["BGSCJG" + i] = "----";
                        sitem["BGDXPD" + i] = "----";
                        sitem["BGBZYQ" + i] = "----";
                        sitem["BGSCJG" + i] = "----";
                        sitem["BGDXPD" + i] = "----";
                    }

                }
                curJcxmCount = 8;
                if (jcxm.Contains("、落锤冲击试验、"))
                {
                    jcxmCur = "落锤冲击试验";

                    if (string.IsNullOrEmpty(mitem["LCCJBHGS"]) || string.IsNullOrEmpty(mitem["LCCJCS"]))
                    {
                        throw new Exception("请输入落锤冲击试验统计信息。");
                    }

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
                            mitem["LCCJ"] = Round(md, 0).ToString("0");

                            if (IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false) == "符合")
                            {
                                MItem[0]["LCCJ_HG"] = "合格";
                            }
                            else
                            {
                                MItem[0]["LCCJ_HG"] = "不合格";
                            }
                        }
                        else
                        {
                            if (Conversion.Val(mitem["LCCJBHGS"]) <= Conversion.Val(mrslccj_Sel["AQPHCS"]))
                            {
                                mitem["LCCJ_HG"] = "合格";
                                mitem["LCCJ"] = "TIR值为：A(≤10%)";
                            }
                            if (Conversion.Val(mitem["LCCJBHGS"]) >= Conversion.Val(mrslccj_Sel["BQPHCS1"]) && Conversion.Val(mitem["LCCJBHGS"]) <= Conversion.Val(mrslccj_Sel["BQPHCS2"]))
                            {
                                mitem["LCCJ_HG"] = "不判定";
                                mitem["LCCJ"] = "根据现有冲击试样数不能作出判定";

                            }
                            if (Conversion.Val(mitem["LCCJBHGS"]) >= Conversion.Val(mrslccj_Sel["CQPHCS"]))
                            {
                                mitem["LCCJ_HG"] = "不合格";
                                mitem["LCCJ"] = "TIR值为：C(＞10%)";
                            }
                        }
                    }
                    else
                    {
                        if (mitem["G_LCCJ"].Contains("≤"))
                        {
                            mitem["LCCJ"] = Round(100 * Conversion.Val(mitem["LCCJBHGS"]) / Conversion.Val(mitem["LCCJCS"]), 0).ToString("0");
                            mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
                            mbhggs = mitem["LCCJ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                            mitem["LCCJ"] = mitem["LCCJ"];
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
                                }
                                else
                                {
                                    mitem["LCCJ_HG"] = "不合格";
                                }
                            }
                            if (mitem["G_LCCJ"].Contains("10次冲击，9次不破裂"))
                            {
                                mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                if (md2 <= 1)
                                {
                                    mitem["LCCJ_HG"] = "合格";
                                }
                                else
                                {
                                    mitem["LCCJ_HG"] = "不合格";
                                }
                            }
                            if (mitem["G_LCCJ"].Contains("9/10"))
                            {
                                mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                if (md2 <= 1)
                                {
                                    mitem["LCCJ_HG"] = "合格";
                                }
                                else
                                {
                                    mitem["LCCJ_HG"] = "不合格";
                                }
                            }
                        }
                    }

                    if (mitem["LCCJ_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs1 = mbhggs1 + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Hg = true;
                    }
                    for (xd = 0; xd < jcxmCount; xd++)
                    {
                        if (mtmpArray[xd].Contains("落锤冲击试验"))
                        {
                            sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd] + "(TIR)/%";
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
                curJcxmCount = 9;
                if (jcxm.Contains("、维卡软化温度、"))
                {
                    jcxmCur = "维卡软化温度";

                    if (string.IsNullOrEmpty(mitem["RHWD"]))
                    {
                        throw new Exception("请输入维卡软化温度");
                    }
                    mitem["RHWD_HG"] = IsQualified(mitem["G_RHWD"], mitem["RHWD"], false);
                    if (mitem["RHWD_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs1 = mbhggs1 + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Hg = true;
                    }
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
                    jcxmCur = "纵向回缩率";

                    double zxhsl1, zxhsl2, zxhsl3 = 0.0;
                    if ((Conversion.Val(MItem[0]["HSLL0_1"]) == 0 || Conversion.Val(MItem[0]["HSLL0_2"]) == 0 || Conversion.Val(MItem[0]["HSLL0_3"]) == 0))
                    {
                        MItem[0]["ZXHSL"] = "0.0";
                    }
                    else
                    {
                        zxhsl1 = Math.Abs(Double.Parse((100 * (Conversion.Val(MItem[0]["HSLL0_1"]) - Conversion.Val(MItem[0]["HSLLI_1"])) / Conversion.Val(MItem[0]["HSLL0_1"])).ToString("0.00")));
                        zxhsl2 = Math.Abs(Double.Parse((100 * (Conversion.Val(MItem[0]["HSLL0_2"]) - Conversion.Val(MItem[0]["HSLLI_2"])) / Conversion.Val(MItem[0]["HSLL0_2"])).ToString("0.00")));
                        zxhsl3 = Math.Abs(Double.Parse((100 * (Conversion.Val(MItem[0]["HSLL0_3"]) - Conversion.Val(MItem[0]["HSLLI_3"])) / Conversion.Val(MItem[0]["HSLL0_3"])).ToString("0.00")));
                        MItem[0]["ZXHSL"] = ((zxhsl1 + zxhsl2 + zxhsl3) / 3).ToString("0.0");

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
                            sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd] + "(%)";
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

                if (curJcxmCount < 11)
                    sitem["BGBZYQ" + curJcxmCount] = "以下空白";
                if (mbhggs == 0)
                {
                    sitem["JCJG"] = "合格";
                }
                if (mbhggs >= 1)
                {
                    sitem["JCJG"] = "不合格";
                }
                mAllHg = (mAllHg && sitem["JCJG"].Trim() == "合格");

            }
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目不符合要求，详情见下页";
                if (realBhg)
                {
                    mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，详情见下页。";
                }
                else
                {
                    if (mFlag_Bhg && mFlag_Hg)
                    {
                        if (mbhggs == 1)
                        {
                            mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不合格，需复检，详情见下页。";

                        }
                        else
                        {
                            mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，详情见下页。";
                        }
                    }
                }

                //if (mFlag_Bhg && mFlag_Hg)
                //    mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "标准，所检项目" + jcxmBhg.TrimEnd('、') + "不符合标准要求，详情见下页。";
                ////mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "标准，所检项目" + jcxmBhg.TrimEnd('、') + "不合格，需复检，详情见下页。";
            }
            #endregion
        }
    }
}