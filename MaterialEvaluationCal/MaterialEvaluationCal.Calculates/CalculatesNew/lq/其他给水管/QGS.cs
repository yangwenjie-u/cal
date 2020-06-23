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

            var MItem = data["M_QGS"];
            var mitem = MItem[0];
            var SItem = data["S_QGS"];
            var mrslccj = dataExtra["BZ_GCLCCJ"];
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
                    double gc = GetSafeDouble(sitem["GCWJ"]);
                    if (gc <= 40)
                    {
                        sitem["ZJCSGS"] = "4";
                    }
                    else if (gc > 40 && gc <= 600)
                    {
                        sitem["ZJCSGS"] = "6";
                    }
                    else if (gc > 600 && gc <= 1600)
                    {
                        sitem["ZJCSGS"] = "8";
                    }
                    else if (gc > 1600)
                    {
                        sitem["ZJCSGS"] = "12";
                    }
                    else
                    {
                        sitem["ZJCSGS"] = "4";
                    }
                    int count = GetSafeInt(sitem["ZJCSGS"]) == 0 ? 4 : GetSafeInt(sitem["ZJCSGS"]);
                    #region
                    //平均外径

                    if (string.IsNullOrEmpty(MItem[0]["G_PJWJ"]) || string.IsNullOrEmpty(MItem[0]["G_GCBH"]))
                    {
                        throw new Exception("请输入平均外径/壁厚标准范围.");
                    }
                    List<string> listWJ_G = new List<string>();
                    listWJ_G = MItem[0]["G_PJWJ"].Replace('~', '～').Split('～').ToList();
                    //壁厚
                    List<string> listBH_G = new List<string>();
                    listBH_G = MItem[0]["G_GCBH"].Replace('~', '～').Split('～').ToList();
                    if (listWJ_G.Count != 2 || listBH_G.Count != 2)
                    {
                        throw new Exception("请输入平均外径/壁厚标准范围.");
                    }
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
                        List<decimal> arrWJHG = new List<decimal>();
                        List<decimal> arrWJBHG = new List<decimal>();
                        List<decimal> arrBH = new List<decimal>();
                        List<decimal> arrDZWJ = new List<decimal>();

                        //单组壁厚
                        List<decimal> arrDZBH = new List<decimal>();
                        double wj_bhg = 0;
                        double bh_bhg = 0;

                        //计算外径
                        for (int i = 1; i < 9; i++)
                        {
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
                                //单组不合格
                                wj_bhg++;
                                arrWJBHG.Add(pjz);
                            }
                            else
                            {
                                arrWJHG.Add(pjz);
                            }
                            arrWJ.Add(pjz);

                            if (wj_bhg > 1)
                            {
                                break;
                            }
                            continue;
                        }

                        if (wj_bhg < 2)
                        {
                            arrWJ = arrWJHG;
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
                        MItem[0]["PJWJ"] = MItem[0]["PJWJ1"] + "～" + MItem[0]["PJWJ2"];

                        //壁厚
                        bh_bhg = 0;
                        for (int i = 1; i < 9; i++)
                        {
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
                                    bh_bhg++;
                                    //单组不合格
                                }

                                arrDZBH.Add(GetSafeDecimal(sitem["SCBH" + i + "_" + j]));
                                continue;
                            }
                            if (bh_bhg > 1)
                            {
                                break;
                                //尺寸不合格
                            }
                            arrBH.AddRange(arrDZBH);
                        }

                        arrBH.Sort();
                        if (arrBH.Count < 1)
                        {
                            throw new Exception("请输入壁厚信息！");
                        }

                        sitem["PJBH1"] = (arrBH[0]).ToString();
                        sitem["PJBH2"] = (arrBH[arrBH.Count - 1]).ToString();
                        sitem["PJBH"] = sitem["PJBH1"] + "～" + sitem["PJBH2"];

                        #region 判定如果尺寸或者壁厚有两个不合格，则不合格
                        if (wj_bhg > 1)
                        {
                            MItem[0]["PJWJ_HG"] = "不合格";
                            //不合格
                            GGCCBHG = true;
                            goto CCBHG_FLAG;
                        }
                        else
                        {
                            MItem[0]["PJWJ_HG"] = IsQualified(mitem["G_PJWJ"], MItem[0]["PJWJ1"]);

                            if (MItem[0]["PJWJ_HG"] == "合格")
                            {
                                MItem[0]["PJWJ_HG"] = IsQualified("≤" + mitem["G_PJWJ1"], MItem[0]["PJWJ2"]);
                            }
                        }

                        if (bh_bhg > 1)
                        {
                            //不合格
                            MItem[0]["HG_GCBH"] = "不合格";
                            GGCCBHG = true;
                            goto CCBHG_FLAG;
                        }
                        else
                        {
                            MItem[0]["HG_GCBH"] = IsQualified(MItem[0]["G_GCBH"], sitem["PJBH1"]);
                            if (MItem[0]["HG_GCBH"] == "合格")
                            {
                                MItem[0]["HG_GCBH"] = IsQualified(MItem[0]["G_GCBH"], sitem["PJBH2"]);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 数据录入 暂时不加
                        //List<decimal> listBH = new List<decimal>();
                        //for (int i = 1; i < 3; i++)
                        //{
                        //    #region 外径
                        //    List<decimal> listWJ = new List<decimal>();
                        //    // 2.平均外径
                        //    count = count >= 12 ? 12 : count;

                        //    for (int j = 1; j <= count; j++)
                        //    {
                        //        listWJ.Add(GetSafeDecimal(sitem["WJ" + i + "_" + +j]));
                        //    }
                        //    listWJ.Sort();
                        //    var pjVal = listWJ.Average();

                        //    if (zj <= 600)
                        //    {
                        //        MItem[0]["PJWJ" + i] = Math.Round(GetSafeDecimal(pjVal.ToString()), 1).ToString("0.0");
                        //    }
                        //    else if (zj <= 1600)
                        //    {
                        //        MItem[0]["PJWJ" + i] = (Math.Round(GetSafeDecimal(pjVal.ToString()) * 5, 0) / 5).ToString();
                        //    }
                        //    else
                        //    {
                        //        MItem[0]["PJWJ" + i] = (Math.Round(GetSafeDecimal(pjVal.ToString()) * 5, 0) / 5).ToString();
                        //    }
                        //    #endregion

                        //    for (int j = 1; j <= count; j++)
                        //    {
                        //        listBH.Add(GetSafeDecimal(sitem["SCBH" + i + "_" + j]));
                        //    }
                        //}

                        //if (GetSafeDecimal(MItem[0]["PJWJ1"]) > GetSafeDecimal(MItem[0]["PJWJ2"]))
                        //{
                        //    MItem[0]["PJWJ"] = MItem[0]["PJWJ2"] + "～" + MItem[0]["PJWJ1"];
                        //}
                        //else if (GetSafeDecimal(MItem[0]["PJWJ1"]) < GetSafeDecimal(MItem[0]["PJWJ2"]))
                        //{
                        //    MItem[0]["PJWJ"] = MItem[0]["PJWJ1"] + "～" + MItem[0]["PJWJ2"];
                        //}
                        //else
                        //{
                        //    MItem[0]["PJWJ"] = MItem[0]["PJWJ1"];
                        //}

                        //listBH.Sort();
                        //var listMin = listBH[0];
                        //var listMax = listBH[listBH.Count - 1];

                        //sitem["PJBH"] = listMin.ToString() + "～" + listMax.ToString(); 
                        #endregion
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
                            jcxmBhg += jcxmBhg.Contains("平均外径") ? "" : "平均外径" + "、";
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
                    if (Gs == 1)
                    {
                        mitem["YYSY2_1"] = "----";
                        mitem["YYSY_HG2"] = "----";
                    }
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        if (mitem["YYSY_HG" + xd] == "不合格")
                        {
                            mitem["YYSY" + xd] = mitem["YYSY" + xd + "_1"];
                            mbhggs = mbhggs + 1;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mFlag_Bhg = true;
                        }
                        else
                        {
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

                                var cs = yysyCs.Replace("，", "").Split('℃');
                                if (cs.Length == 2)
                                {
                                    sitem["YYSYWD" + md] = cs[0].Trim() + "℃";
                                    sitem["YYSYSJ" + md] = cs[1].Trim();
                                }
                                sitem["YYSYHYL" + md] = System.Text.RegularExpressions.Regex.Replace(syyl, @"[^0-9]+", "") + "MPa";

                                if (yysyCs.Contains("，"))
                                    yysyCs = yysyCs.Replace("，", " " + syyl + " ");
                                else
                                    yysyCs = yysyCs.Replace(" ", " " + syyl + " ");
                                sitem["BGJCXM" + (curJcxmCount + md - 1)] = "液压试验";//检测项目  


                                sitem["YYSYZT"] = "试验制备条件：状态调节时间：" + sitem["YYSYHJ"] + "；试验环境：" + sitem["YYSYHJ"] + "；密封接头类型：" + sitem["YYSYJTLX"];

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

                        if (mrslccj_Sel == mrslccj.First() || mrslccj_Sel == mrslccj.Last())
                        {
                            md1 = GetSafeDouble(mitem["LCCJBHGS"]);
                            md2 = GetSafeDouble(mitem["LCCJCS"]); ;
                            var md3 = md2 == 0 ? 0 : (100 * md1 / md2);
                            mitem["LCCJ"] = Round(md3, 0).ToString("0");
                            mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
                        }
                        else
                        {
                            if (GetSafeDouble(mitem["LCCJBHGS"]) <= GetSafeDouble(mrslccj_Sel["AQPHCS"]))
                            {
                                mitem["LCCJ_HG"] = "合格";
                                mitem["LCCJ"] = "TIR值为：A(≤10%)";
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
                    else if (mitem["G_LCCJ"].Contains("≤"))
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

                    if (!mitem["G_RHWD"].Contains("≥"))
                    {
                        mitem["G_RHWD"] = "≥" + mitem["G_RHWD"];
                    }
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

                    if (!mitem["G_ZXHSL"].Contains("≤"))
                    {
                        mitem["G_ZXHSL"] = "≤" + mitem["G_ZXHSL"];
                    }

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
            var fjpd = sffj ? "复检" : "";
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + fjpd + "均符合要求。";
            }
            else if (mjcjg == "不下结论")
            {
                mitem["JCJG"] = "不下结论";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + fjpd + "不符合要求，详情见下页";
                if (realBhg)
                {
                    mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + fjpd + "不符合要求，详情见下页。";
                }
                else
                {
                    if (mbhggs == 1)
                    {
                        if (sffj)
                        {
                            mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + fjpd + "不符合要求，详情见下页。";
                        }
                        else
                        {
                            mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，需复检，详情见下页。";
                        }
                    }
                    else
                    {
                        mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + fjpd + "不符合要求，详情见下页。";
                    }
                }

            }
            #endregion
        }

    }
}
