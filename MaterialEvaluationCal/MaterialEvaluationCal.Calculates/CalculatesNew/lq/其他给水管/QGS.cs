using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates.CalculatesNew.lq.QPS_其它给水管
{
    public class GSG : BaseMethods
    {
        public void Calc()
        {
            #region 计算开始
            var data = retData;

            var MItem = data["M_GSG"];
            var mitem = MItem[0];
            var SItem = data["S_GSG"];

            bool mAllHg;
            bool mFlag_Hg, mFlag_Bhg;
            bool sffj = false;
            bool realBhg = false;//标识直接不合格 。外观颜色不合格就不需要复试，直接不合格
                                 //当前项目的变量声明
            int mbhggs = 0;
            int mbhgpds = 0;
            int mhgpds = 0;
            string mGxl, mSjdj;
            var mjcjg = "----";

            bool GGCCBHG = false;//规格尺寸是否合格

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

                //是否复检
                sffj = Convert.ToBoolean(mitem["SFFJ"]);

                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                mtmpArray = sitem["JCXM"].Replace(",", "、").Split('、').ToList();

                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                mGxl = sitem["GXL"]; //环刚度(管系列)
                if (string.IsNullOrEmpty(mGxl))
                    mGxl = "----";
                string gcwj = sitem["GCWJ"];
                string gcbh = sitem["GCBH"];
                String gxl = sitem["GXL"];

                if (gcbh == "----")
                {
                    gcbh = "";
                }

                if (sitem["CLDJ"] == "PP-R")
                {
                    sitem["GGXH"] = gxl + " " + "dn" + gcwj + "x" + "en" + gcbh + "mm";
                }
                else
                {
                    if (gcwj.Contains("DN"))
                    {
                        gcwj += " " + gcbh + "mm";
                    }
                    else
                    {
                        gcwj = "DN " + gcwj + "x" + gcbh + "mm";
                    }

                    if (sitem["CLDJ"] != "----")
                    {
                        gcwj += sitem["CLDJ"];
                    }

                    sitem["GGXH"] = gcwj;
                }
                mbhggs = 0;
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

                    double gc = GetSafeDouble(sitem["GCCC"]);
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
                    int count = GetSafeDouble(sitem["ZJCLGS"]) == 0 ? 4 : GetSafeInt(sitem["ZJCLGS"]);

                    if (count < 4)
                    {
                        throw new Exception("要求直径测量数量不能小于4个");
                    }

                    #region
                    //平均外径
                    var mrsWgcc_Filter = mrsWgcc.FirstOrDefault(x => x["MC"].Contains(mSjdj) && x["GCCC"] == sitem["GCCC"] && x["HGDBH"] == sitem["GXL"]);
                    if (mrsWgcc_Filter != null && mrsWgcc_Filter.Count() > 0)
                    {
                        MItem[0]["G_PJWJ"] = mrsWgcc_Filter["WJMin"] + "～" + mrsWgcc_Filter["WJMax"];
                        MItem[0]["G_GCBH"] = mrsWgcc_Filter["BHMIN"] + "～" + mrsWgcc_Filter["BHMAX"];
                    }
                    else
                    {
                        throw new Exception("获取规格尺寸信息失败");
                    }
                    List<string> listWJ_G = new List<string>();
                    listWJ_G = MItem[0]["G_PJWJ"].Split('～').ToList();
                    if (listWJ_G.Count != 2)
                    {
                        throw new Exception("请输入平均外径标准范围.");
                    }

                    //壁厚
                    List<string> listBH_G = new List<string>();
                    listBH_G = MItem[0]["G_GCBH"].Split('～').ToList();
                    if (listWJ_G.Count != 2)
                    {
                        throw new Exception("请输入平均壁厚标准范围.");
                    }
                    //如果壁厚《=10，修约0.05
                    //《=30，修约0.1
                    //>30，修约0.1
                    var bh = GetSafeDouble(sitem["GCBH"]);

                    //外径修约
                    //《=1600，修约0.2
                    //》1600，修约1
                    var zj = GetSafeDouble(sitem["GCWJ"]);
                    //excel 录入
                    if (mitem["SJTABS"] == "1")
                    {
                        var wjMin = GetSafeDecimal(listWJ_G[0]);
                        var wjMax = GetSafeDecimal(listWJ_G[1]);
                        var bhMin = GetSafeDecimal(listBH_G[0]);
                        var bhMax = GetSafeDecimal(listBH_G[1]);

                        List<decimal> arrWJ = new List<decimal>();
                        List<decimal> arrBH = new List<decimal>();
                        List<decimal> arrDZWJ = new List<decimal>();

                        //单组壁厚
                        List<decimal> arrDZBH = new List<decimal>();
                        decimal sum = 0;
                        double bhg = 0;
                        decimal flag = 0;

                        //计算外径
                        for (int i = 1; i < 9; i++)
                        {
                            bhg = 0;
                            sum = 0;
                            flag = 0;
                            arrDZWJ.Clear();
                            if (string.IsNullOrEmpty(sitem["WJ" + i + "_1"]))
                            {
                                break;
                            }
                            for (int j = 1; j < 13; j++)
                            {
                                if (string.IsNullOrEmpty(sitem["WJ" + i + "_" + j]))
                                {
                                    break;
                                }
                                arrDZWJ.Add(GetSafeDecimal(sitem["WJ" + i + "_" + j]));
                            }
                            var pjz = arrDZWJ.Average();
                            if (pjz < wjMin || pjz > wjMax) //该组外径合格，则去掉该组，如果大于1，尺寸不合格
                            {
                                bhg++;
                                //单组不合格
                                goto DZBHG_FLAG;
                            }

                            if (bhg > 1)
                            {
                                break;
                            }

                            arrWJ.AddRange(arrDZWJ);
                            DZBHG_FLAG:
                            continue;
                        }
                        if (bhg > 1)
                        {
                            //不合格
                            GGCCBHG = true;
                            goto CCBHG_FLAG;
                        }
                        arrWJ.Sort();
                        if (arrWJ.Count < 1)
                        {
                            throw new Exception("请输入外径信息！");
                        }
                        if (zj <= 600)
                        {
                            MItem[0]["PJWJ1"] = Math.Round(GetSafeDecimal(arrWJ[0].ToString()), 1).ToString("0.0");
                            MItem[0]["PJWJ2"] = Math.Round(GetSafeDecimal(arrWJ[arrWJ.Count - 1].ToString()), 1).ToString("0.0");
                        }
                        else if (zj <= 1600)
                        {
                            MItem[0]["PJWJ1"] = (Math.Round(GetSafeDecimal((arrWJ[0] * 5).ToString()), 0) / 5).ToString();
                            MItem[0]["PJWJ2"] = (Math.Round(GetSafeDecimal((arrWJ[arrWJ.Count - 1] * 5).ToString()), 0) / 5).ToString();
                        }
                        else
                        {
                            MItem[0]["PJWJ1"] = (Math.Round(GetSafeDecimal((arrWJ[0] * 5).ToString()), 0) / 5).ToString();
                            MItem[0]["PJWJ2"] = (Math.Round(GetSafeDecimal((arrWJ[arrWJ.Count - 1] * 5).ToString()), 0) / 5).ToString();
                        }

                        //壁厚

                        for (int i = 1; i < 9; i++)
                        {
                            bhg = 0;
                            sum = 0;
                            flag = 0;
                            if (string.IsNullOrEmpty(sitem["SCBH" + i + "_1"]))
                            {
                                break;
                            }
                            for (int j = 1; j < 13; j++)
                            {
                                if (string.IsNullOrEmpty(sitem["SCBH" + i + "_" + j]))
                                {
                                    break;
                                }

                                if (GetSafeDecimal(sitem["SCBH" + i + "_" + j]) < bhMin && GetSafeDecimal(sitem["SCBH" + i + "_" + j]) > bhMax) //该组不合格，则去掉该组，如果大于1，尺寸不合格
                                {
                                    bhg++;
                                    //单组不合格
                                    goto DZBHG_FLAG;
                                }
                                if (bhg > 1)
                                {
                                    break;
                                    //尺寸不合格
                                }
                                arrDZBH.Add(GetSafeDecimal(sitem["SCBH" + i + "_" + j]));
                                DZBHG_FLAG:
                                continue;
                            }
                            if (bhg > 1)
                            {
                                break;
                                //尺寸不合格
                            }
                            arrBH.AddRange(arrDZBH);
                        }
                        if (bhg > 1)
                        {
                            //不合格
                            GGCCBHG = true;
                            goto CCBHG_FLAG;
                        }

                        arrBH.Sort();
                        if (arrBH.Count < 1)
                        {
                            throw new Exception("请输入壁厚信息！");
                        }

                        sitem["PJBH1"] = (Round((double)arrBH[0] / 5.0, 2) * 5).ToString("0.00");
                        sitem["PJBH2"] = (Round((double)arrBH[arrBH.Count - 1] / 5.0, 2) * 5).ToString("0.00");

                    }
                    else
                    {
                        List<double> listBH = new List<double>();
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

                            if (zj <= 600)
                            {
                                MItem[0]["PJWJ" + i] = Round(GetDouble(pjVal.ToString()), 1).ToString("0.0");
                            }
                            else if (zj <= 1600)
                            {
                                MItem[0]["PJWJ" + i] = (Round(GetDouble(pjVal.ToString()) * 5, 0) / 5).ToString();
                            }
                            else
                            {
                                MItem[0]["PJWJ" + i] = (Round(GetDouble(pjVal.ToString()) * 5, 0) / 5).ToString();
                            }
                            #endregion

                            for (int j = 1; j <= count; j++)
                            {
                                md1 = GetSafeDouble(sitem["SCBH" + i + "_" + j]);
                                listBH.Add(md1);
                            }
                        }
                        listBH.Sort();
                        var listMin = listBH[0];
                        var listMax = listBH[listBH.Count - 1];

                        if (bh <= 10)
                        {
                            sitem["PJBH1"] = (Round(listMin / 5, 2) * 5).ToString("0.00");
                            sitem["PJBH2"] = (Round(listMax / 5, 2) * 5).ToString("0.00");
                        }
                        else if (bh > 10 && bh <= 30)
                        {
                            sitem["PJBH1"] = Round(listMin, 1).ToString("0.0");
                            sitem["PJBH2"] = Round(listMax, 1).ToString("0.0");
                        }
                        else
                        {
                            sitem["PJBH1"] = Round(listMin, 1).ToString("0.0");
                            sitem["PJBH2"] = Round(listMax, 1).ToString("0.0");
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
                            mitem["YYSY" + xd] = mitem["YYSY" + xd + "_1"];
                            //mitem["YYSY" + xd] = "有破裂、有渗漏";
                            mbhggs = mbhggs + 1;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            //mitem["YYSY" + xd] = "不破裂、不渗漏";
                            mitem["YYSY" + xd] = mitem["YYSY" + xd + "_1"];

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

                    //mitem["G_LCCJ"] = "";
                    //mitem["LCCJ"] = "TIR值为：A(≤10%)";
             

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
                    if (!sffj)
                    {
                        //初检
                        mitem["RHWD"] = Math.Round((GetSafeDecimal(sitem["RHWD1"]) + GetSafeDecimal(sitem["RHWD2"]) / 2), 1).ToString();
                        mitem["RHWD_HG"] = IsQualified(mitem["G_RHWD"], mitem["RHWD"], false);
                    }
                    else
                    {
                        mitem["RHWD"] = Math.Round((GetSafeDecimal(sitem["RHWD1"]) + GetSafeDecimal(sitem["RHWD2"]) / 2), 1).ToString();
                        mitem["RHWD_F"] = Math.Round((GetSafeDecimal(sitem["RHWD3"]) + GetSafeDecimal(sitem["RHWD4"]) / 2), 1).ToString();

                        mitem["RHWD_HG"] = IsQualified(mitem["G_RHWD"], mitem["RHWD"], false);

                        if (mitem["RHWD_HG"] == "合格")
                        {
                            mitem["RHWD_HG"] = IsQualified(mitem["G_RHWD"], mitem["RHWD_F"], false);
                        }
                    }

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
                    MItem[0]["HSLL0_1"] = "100";
                    MItem[0]["HSLL0_2"] = "100";
                    MItem[0]["HSLL0_3"] = "100";
                    MItem[0]["HSLL0_4"] = "100";
                    MItem[0]["HSLL0_5"] = "100";
                    MItem[0]["HSLL0_6"] = "100";

                    if (!sffj)
                    {
                        //初检  
                        decimal sum = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            sum += Math.Abs(GetSafeDecimal((100 * (Conversion.Val(MItem[0]["HSLL0_" + i]) - Conversion.Val(MItem[0]["HSLLI_" + i])) / Conversion.Val(MItem[0]["HSLL0_" + i])).ToString("0.00")));
                        }
                        MItem[0]["ZXHSL"] = Math.Round(sum / 3, 1).ToString("0.0");
                        mitem["ZXHSL_HG"] = IsQualified(mitem["G_ZXHSL"], mitem["ZXHSL"], false);
                    }
                    else
                    {
                        decimal sum = 0, sum2 = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            sum += Math.Abs(GetSafeDecimal((100 * (Conversion.Val(MItem[0]["HSLL0_" + i]) - Conversion.Val(MItem[0]["HSLLI_" + i])) / Conversion.Val(MItem[0]["HSLL0_" + i])).ToString("0.00")));
                            sum2 += Math.Abs(GetSafeDecimal((100 * (Conversion.Val(MItem[0]["HSLL0_" + i + 3]) - Conversion.Val(MItem[0]["HSLLI_" + i + 3])) / Conversion.Val(MItem[0]["HSLL0_" + i + 3])).ToString("0.00")));
                        }

                        MItem[0]["ZXHSL"] = Math.Round(sum / 3, 1).ToString("0.0");
                        MItem[0]["ZXHSL_F"] = Math.Round(sum2 / 3, 1).ToString("0.0");
                        mitem["ZXHSL_HG"] = IsQualified(mitem["G_ZXHSL"], mitem["ZXHSL"], false);

                        if (MItem[0]["ZXHSL_HG"] == "合格")
                        {
                            mitem["ZXHSL_HG"] = IsQualified(mitem["G_ZXHSL"], mitem["ZXHSL_F"], false);
                        }
                    }

                    if (MItem[0]["ZXHSL_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
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
                    sitem["BGJCXM" + curJcxmCount] = "以下空白";
                if (mbhggs == 0)
                {
                    sitem["JCJG"] = "合格";
                }
                if (mbhggs >= 1)
                {
                    sitem["JCJG"] = "不合格";
                }
                mAllHg = (mAllHg && sitem["JCJG"].Trim() == "合格");

                CCBHG_FLAG:
                if (GGCCBHG)
                {
                    mAllHg = false;
                    realBhg = true;
                    jcxmBhg = "规格尺寸";
                }
            }
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else if (mjcjg == "不下结论")
            {
                mitem["JCJG"] = "不下结论";
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
            #endregion
        }

    }
}
