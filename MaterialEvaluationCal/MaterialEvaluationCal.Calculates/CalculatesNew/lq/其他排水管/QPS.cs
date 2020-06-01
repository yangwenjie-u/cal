using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates.CalculatesNew.lq.PSG_排水管
{
    public class GSG : BaseMethods
    {
        public void Calc()
        {
            #region 计算开始
            var data = retData;
       
            var mrscyfa = dataExtra["BZ_GCCYFA"];
            var mrslccj = dataExtra["BZ_GCLCCJ"];
            //var mrsYysycs = dataExtra["BZ_GCYYSYCS"];
            var mrsWgcc = dataExtra["BZ_GCWGCC"];


            var MItem = data["M_QPS"];
            var mitem = MItem[0];
            var SItem = data["S_QPS"];

            bool mFlag_Hg = false, mFlag_Bhg = false;
            bool mAllHg;
            bool realBhg = false;//标识直接不合格 。外观颜色不合格就不需要复试，直接不合格
            int mbhggs = 0;
            int mhgpds, mbhgpds = 0;
            string mGxl, mSjdj;
            mAllHg = true;

            foreach (var mrscyfa_item in mrscyfa)
            {
                var sitem = SItem[0];
                var df = sitem["DBSL"];

                if (GetSafeDouble(sitem["DBSL"]) >= GetSafeDouble(mrscyfa_item["PFW1"]) && GetSafeDouble(sitem["DBSL"]) <= GetSafeDouble(mrscyfa_item["PFW2"]))
                {
                    mhgpds = GetSafeInt(mrscyfa_item["HGPDS"]);
                    mbhgpds = GetSafeInt(mrscyfa_item["BHGPDS"]);
                    break;
                }
            }
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            foreach (var sitem in SItem)
            {
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                mSjdj = sitem["SJDJ"]; //管材名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                mGxl = sitem["GXL"]; //环刚度(管系列)
                if (string.IsNullOrEmpty(mGxl))
                    mGxl = "";
                if (mGxl == "")
                    mGxl = "----";
             
                //具体检测项目处理
                mbhggs = 0;
                mbhggs = 0;
                int xd;
                double md1, md2, md;
                string[] mtmpArray;
                mtmpArray = sitem["JCXM"].Replace(',', '、').Split('、');
                int jcxmCount; //启用的检查项目个数
                int curJcxmCount; //现在处理的是第几个检测项目
                jcxmCount = mtmpArray.Length;
                curJcxmCount = 0;

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


                    if (MItem[0]["WG_HG"] == "合格" && MItem[0]["BZ_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        realBhg = true;
                        if (MItem[0]["WG_HG"] != "合格")
                        {
                            jcxmBhg += jcxmBhg.Contains("外观") ? "" : "外观" + "、";
                            mbhggs = mbhggs + 1;
                        }
                        if (MItem[0]["WG_HG"] != "合格")
                        {
                            jcxmBhg += jcxmBhg.Contains("颜色") ? "" : "颜色" + "、";
                            mbhggs = mbhggs + 1;
                        }
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
                curJcxmCount = 2;
                if (jcxm.Contains("、规格尺寸、"))
                {
                    jcxmCur = "规格尺寸";

                    double gc = GetSafeDouble(sitem["GCWJ"]);
                    if (gc <= 40)
                    {
                        sitem["ZJCLGS"] = "4";
                    }
                    else if (gc > 40 && gc <= 600)
                    {
                        sitem["ZJCLGS"] = "6";
                    }
                    else if (gc > 600 && gc <= 1600)
                    {
                        sitem["ZJCLGS"] = "8";
                    }
                    else if (gc > 1600)
                    {
                        sitem["ZJCLGS"] = "12";
                    }
                    else
                    {
                        sitem["ZJCLGS"] = "4";
                    }



                    //测试的数量4-12个
                    //1 长度 2.平均外径 3.不圆度 4.壁厚公差
                    int count = GetSafeInt(sitem["ZJCSGS"]) == 0 ? 4 : GetSafeInt(sitem["ZJCSGS"]);

                    if (count < 4)
                    {
                        throw new Exception("要求直径测量数量不能小于4个");
                    }

                    #region

                    //MItem[0]["G_PJWJ"] = mrsWgcc_Filter["WJMin"] + "～" + mrsWgcc_Filter["WJMax"];
                    //MItem[0]["G_GCBH"] = mrsWgcc_Filter["BHMIN"] + "～" + mrsWgcc_Filter["BHMAX"];

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
                        MItem[0]["PJWJ"] = MItem[0]["PJWJ2"] + "～" + MItem[0]["PJWJ1"];
                    }
                    else if (GetSafeDouble(MItem[0]["PJWJ1"]) < GetSafeDouble(MItem[0]["PJWJ2"]))
                    {
                        MItem[0]["PJWJ"] = MItem[0]["PJWJ1"] + "～" + MItem[0]["PJWJ2"];
                    }
                    else
                    {
                        MItem[0]["PJWJ"] = MItem[0]["PJWJ1"];
                    }
                    if (GetSafeDouble(sitem["PJBH1"]) > GetSafeDouble(sitem["PJBH2"]))
                    {
                        sitem["PJBH"] = sitem["PJBH2"] + "～" + sitem["PJBH1"];
                    }
                    else if (GetSafeDouble(sitem["PJBH1"]) < GetSafeDouble(sitem["PJBH2"]))
                    {
                        sitem["PJBH"] = sitem["PJBH1"] + "～" + sitem["PJBH2"];
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
                        if (MItem[0]["HG_GCBH"] != "合格")
                        {
                            jcxmBhg += jcxmBhg.Contains("壁厚") ? "" : "壁厚" + "、";
                            mbhggs = mbhggs + 1;
                        }
                        if (MItem[0]["PJWJ_HG"] != "合格")
                        {
                            jcxmBhg += jcxmBhg.Contains("尺寸") ? "" : "颜色" + "、";
                            mbhggs = mbhggs + 1;
                        }
                        mFlag_Bhg = true;
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

                curJcxmCount = 4;
                if (jcxm.Contains("、落锤冲击、") || jcxm.Contains("、落锤冲击试验、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "落锤冲击,落锤冲击试验");
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
                            md1 = GetSafeDouble(mitem["LCCJBHGS"]);
                            md2 = GetSafeDouble(mitem["LCCJCS"]); ;
                            md = md2 == 0 ? 0 : (100 * md1 / md2);
                            mitem["LCCJ"] = Round(md, 0).ToString("0");
                            mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);


                        }
                        else
                        {
                            if (GetSafeDouble(mitem["LCCJBHGS"]) <= GetSafeDouble(mrslccj_Sel["AQPHCS"]))
                            {
                                mitem["LCCJ_HG"] = "合格";
                                mitem["LCCJ"] = "IR值为：A(≤10%)";
                            }
                            if (GetSafeDouble(mitem["LCCJBHGS"]) >= GetSafeDouble(mrslccj_Sel["BQPHCS1"]) && GetSafeDouble(mitem["LCCJBHGS"]) <= GetSafeDouble(mrslccj_Sel["BQPHCS2"]))
                            {
                                mitem["LCCJ_HG"] = "不判定";
                                mitem["LCCJ"] = "根据现有冲击试样数不能作出判定";
                            }
                            if (GetSafeDouble(mitem["LCCJBHGS"]) >= GetSafeDouble(mrslccj_Sel["CQPHCS"]))
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
                            mitem["LCCJ"] = GetSafeDouble(mitem["LCCJCS"]) == 0 ? "0" : Round(100 * GetSafeDouble(mitem["LCCJBHGS"]) / GetSafeDouble(mitem["LCCJCS"]), 0).ToString("0");
                            mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
                            mitem["LCCJ"] = mitem["LCCJ"];
                        }
                        else
                        {
                            md1 = GetSafeDouble(mitem["LCCJCS"].Trim());
                            md2 = GetSafeDouble(mitem["LCCJBHGS"].Trim());
                            md1 = Round(md1, 0);
                            md2 = Round(md2, 0);
                            if (mitem["G_LCCJ"].Contains("12次冲击，12次不破裂"))
                            {
                                mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                if (md2 == 0)
                                    mitem["LCCJ_HG"] = "合格";
                                else
                                {
                                    mitem["LCCJ_HG"] = "不合格";
                                }
                            }
                            if (mitem["G_LCCJ"].Contains("10次冲击，9次不破裂"))
                            {
                                mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                if (md2 <= 1)
                                    mitem["LCCJ_HG"] = "合格";
                                else
                                {
                                    mitem["LCCJ_HG"] = "不合格";
                                }
                            }
                            if (mitem["G_LCCJ"].Contains("9/10"))
                            {
                                mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                if (md2 <= 1)
                                    mitem["LCCJ_HG"] = "合格";
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
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Hg = true;
                    }
                    for (xd = 0; xd <= jcxmCount; xd++)
                    {
                        if (mtmpArray[xd].Contains("落锤冲击") || mtmpArray[xd].Contains("冲击性能") || mtmpArray[xd].Contains("落锤冲击试验"))
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
                    sitem["BGDW" + curJcxmCount] = "----";
                    sitem["BGBZYQ" + curJcxmCount] = mitem["G_LCCJ"];
                    sitem["BGSCJG" + curJcxmCount] = mitem["LCCJ"];
                    sitem["BGDXPD" + curJcxmCount] = mitem["LCCJ_HG"];
                    curJcxmCount = curJcxmCount + 1;
                }
                curJcxmCount = 5;
                if (jcxm.Contains("、软化温度、") || jcxm.Contains("、维卡软化温度、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "软化温度,维卡软化温度");
                    decimal PJ = 0;
                    decimal PJ1 = 0;
                    var fj = GetSafeDouble(MItem[0]["SFFJ"]);

                    if (fj == 0)
                    {
                        PJ = Math.Round(((GetSafeDecimal(MItem[0]["RHWD1"]) + GetSafeDecimal(MItem[0]["RHWD2"])) / 2), 1);
                    }
                    else
                    {
                        PJ = Math.Round(((GetSafeDecimal(MItem[0]["RHWD1"]) + GetSafeDecimal(MItem[0]["RHWD2"])) / 2), 1);

                        PJ1 = Math.Round(((GetSafeDecimal(MItem[0]["RHWD3"]) + GetSafeDecimal(MItem[0]["RHWD4"])) / 2), 1);
                    }
                    MItem[0]["RHWD"] = Math.Round(PJ, 1).ToString();
                    MItem[0]["RHWD_F"] = Math.Round(PJ1, 1).ToString();
                    mitem["RHWD_HG"] = IsQualified(mitem["G_RHWD"], mitem["RHWD"], false);

                    if (mitem["RHWD_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Hg = true;
                    }
                    //向报告用字段赋值
                    for (xd = 0; xd <= jcxmCount; xd++)
                    {
                        if (mtmpArray[xd].Contains("软化温度") || mtmpArray[xd].Contains("维卡软化温度"))
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
                    mitem["RHWD"] = "0";
                    mitem["RHWD_HG"] = "----";
                    mitem["G_RHWD"] = "0";
                }

                if (jcxm.Contains("、环刚、") || jcxm.Contains("、环刚度、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "环刚,环刚度");


                    var fj = GetSafeDouble(MItem[0]["SFFJ"]);

                    double Yi, S1, S2, S3, S4, S5, S6 = 0;
                    if (fj == 0)
                    {
                        Yi = GetSafeDouble(sitem["GCWJ"]) * 0.03 / 1000;
                        if ((GetSafeDouble(MItem[0]["HGD_LI1"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI2"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI3"]) * Yi != 0))
                        {
                            S1 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI1"]) / ((GetSafeDouble(MItem[0]["HGD_LI1"]) / 1000) * Yi);
                            S2 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI2"]) / ((GetSafeDouble(MItem[0]["HGD_LI2"]) / 1000) * Yi);
                            S3 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI3"]) / ((GetSafeDouble(MItem[0]["HGD_LI3"]) / 1000) * Yi);
                            MItem[0]["HGD"] = ((S1 + S2 + S3) / 3).ToString("0.0");
                            MItem[0]["HGD_HG"] = IsQualified(MItem[0]["G_HGD"], MItem[0]["HGD"]);
                        }
                    }
                    else
                    {


                        Yi = GetSafeDouble(sitem["GCWJ"]) * 0.03 / 1000;
                        if ((GetSafeDouble(MItem[0]["HGD_LI1"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI2"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI3"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI4"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI5"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI6"]) * Yi != 0))
                        {
                            S1 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI1"]) / ((GetSafeDouble(MItem[0]["HGD_LI1"]) / 1000) * Yi);
                            S2 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI2"]) / ((GetSafeDouble(MItem[0]["HGD_LI2"]) / 1000) * Yi);
                            S3 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI3"]) / ((GetSafeDouble(MItem[0]["HGD_LI3"]) / 1000) * Yi);
                            MItem[0]["HGD"] = ((S1 + S2 + S3) / 3).ToString("0.0");


                            S4 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI4"]) / ((GetSafeDouble(MItem[0]["HGD_LI4"]) / 1000) * Yi);
                            S5 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI5"]) / ((GetSafeDouble(MItem[0]["HGD_LI5"]) / 1000) * Yi);
                            S6 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI6"]) / ((GetSafeDouble(MItem[0]["HGD_LI6"]) / 1000) * Yi);
                            MItem[0]["HGD_F"] = Math.Round((S4 + S5 + S6) / 3, 1).ToString("0.0");

                            if (MItem[0]["HGD_HG"] == "合格")
                            {
                                MItem[0]["HGD_HG"] = IsQualified(MItem[0]["G_HGD"], MItem[0]["HGD_F"]);

                            }

                        }
                    }






                    if (mitem["HGD_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Hg = true;
                    }
                    for (xd = 0; xd <= jcxmCount; xd++)
                    {
                        if (mtmpArray[xd].Contains("环刚") || mtmpArray[xd].Contains("环刚度"))
                        {
                            if (sitem["SJDJ"] == "埋地用聚乙烯(PE)双壁波纹管材" || sitem["SJDJ"] == "埋地用聚乙烯(PE)缠绕结构壁管材")
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd] + "(kN/㎡)";
                            else
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                            break;
                        }
                    }
                    sitem["BGDW" + curJcxmCount] = "kN/㎡";
                    sitem["BGBZYQ" + curJcxmCount] = mitem["G_HGD"];
                    sitem["BGSCJG" + curJcxmCount] = mitem["HGD"];
                    sitem["BGDXPD" + curJcxmCount] = mitem["HGD_HG"];
                    curJcxmCount = curJcxmCount + 1;
                }
                else
                {
                    mitem["HGD"] = "0";
                    mitem["HGD_HG"] = "----";
                    mitem["G_HGD"] = "0";
                }


                if (jcxm.Contains("、烘箱、") || jcxm.Contains("、烘箱试验、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "烘箱,烘箱试验");
                    if (mitem["HXSY_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Hg = true;
                    }

                    for (xd = 0; xd <= jcxmCount; xd++)
                    {
                        if (mtmpArray[xd].Contains("烘箱") || mtmpArray[xd].Contains("烘箱试验"))
                        {
                            sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                            break;
                        }
                    }
                    sitem["BGDW" + curJcxmCount] = "----";
                    sitem["BGBZYQ" + curJcxmCount] = mitem["G_HXSY"];
                    sitem["BGSCJG" + curJcxmCount] = mitem["HXSY"];
                    sitem["BGDXPD" + curJcxmCount] = mitem["HXSY_HG"];
                    curJcxmCount = curJcxmCount + 1;
                }
                else
                {
                    mitem["HXSY"] = "----";
                    mitem["HXSY_HG"] = "----";
                    mitem["G_HXSY"] = "----";
                }
                if (jcxm.Contains("、环柔、") || jcxm.Contains("、环柔性、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "环柔,环柔性");
                    if (mitem["HRX_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Hg = true;
                    }

                    //向报告用字段赋值
                    for (xd = 0; xd <= jcxmCount; xd++)
                    {
                        if (mtmpArray[xd].Contains("环柔") || mtmpArray[xd].Contains("环柔性"))
                        {
                            sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                            break;
                        }
                    }
                    sitem["BGDW" + curJcxmCount] = "----";
                    sitem["BGBZYQ" + curJcxmCount] = mitem["G_HRX"];
                    sitem["BGSCJG" + curJcxmCount] = mitem["HRX"];
                    sitem["BGDXPD" + curJcxmCount] = mitem["HRX_HG"];
                    curJcxmCount = curJcxmCount + 1;
                }
                else
                {
                    mitem["HRX"] = "";
                    mitem["HRX_HG"] = "----";
                    mitem["G_HRX"] = "";
                }


                if (jcxm.Contains("、纵向回缩率、"))
                {
                    jcxmCur = "纵向回缩率";

                    double zxhsl1, zxhsl2, zxhsl3 = 0.0;
                    if ((GetSafeDouble(MItem[0]["HSLL0_1"]) == 0 || GetSafeDouble(MItem[0]["HSLL0_2"]) == 0 || GetSafeDouble(MItem[0]["HSLL0_3"]) == 0))
                    {
                        MItem[0]["ZXHSL"] = "0.0%";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(MItem[0]["HSLL0_1"]))
                        {
                            zxhsl1 = 0;
                        }
                        else
                        {
                            zxhsl1 = Math.Abs(Double.Parse((100 * (GetSafeDouble(MItem[0]["HSLL0_1"]) - GetSafeDouble(MItem[0]["HSLLI_1"])) / GetSafeDouble(MItem[0]["HSLL0_1"])).ToString("0.00")));
                        }
                        if (string.IsNullOrEmpty(MItem[0]["HSLL0_2"]))
                        {
                            zxhsl2 = 0;
                        }
                        else
                        {
                            zxhsl2 = Math.Abs(Double.Parse((100 * (GetSafeDouble(MItem[0]["HSLL0_2"]) - GetSafeDouble(MItem[0]["HSLLI_2"])) / GetSafeDouble(MItem[0]["HSLL0_2"])).ToString("0.00")));
                        }
                        if (string.IsNullOrEmpty(MItem[0]["HSLL0_3"]))
                        {
                            zxhsl3 = 0;
                        }
                        else
                        {
                            zxhsl3 = Math.Abs(Double.Parse((100 * (GetSafeDouble(MItem[0]["HSLL0_3"]) - GetSafeDouble(MItem[0]["HSLLI_3"])) / GetSafeDouble(MItem[0]["HSLL0_3"])).ToString("0.00")));
                        }
                        MItem[0]["ZXHSL"] = ((zxhsl1 + zxhsl2 + zxhsl3) / 3).ToString("0.0") + "%";

                    }
                    mitem["ZXHSL_HG"] = IsQualified(mitem["G_ZXHSL"], mitem["ZXHSL"], false);
                    if (mitem["ZXHSL_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Hg = true;
                    }
                    //  '向报告用字段赋值
                    for (xd = 0; xd < jcxmCount; xd++)
                    {
                        if (mtmpArray[xd].Contains("纵向回缩率"))
                        {
                            sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd] + "%";
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
                    //if (mFlag_Bhg && mFlag_Hg)
                    //{
                    if (mbhggs == 1)
                    {
                        mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不合格，需复检，详情见下页。";

                    }
                    else
                    {
                        mitem["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，详情见下页。";
                    }
                    //}
                }
            }
            #endregion
        }

    }
}
