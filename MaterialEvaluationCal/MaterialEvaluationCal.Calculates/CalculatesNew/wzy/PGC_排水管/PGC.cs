using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


namespace Calculates
{
    public class PGC : BaseMethods
    {
        public void Calc()
        {
            #region 计算开始
            var data = retData;
            var mrsDj = dataExtra["BZ_PGC_DJ"];
            var mrscyfa = dataExtra["BZ_GCCYFA"];
            var mrslccj = dataExtra["BZ_GCLCCJ"];
            //var mrsYysycs = dataExtra["BZ_GCYYSYCS"];
            var mrsWgcc = dataExtra["BZ_GCWGCC"];

            var MItem = data["M_PGC"];
            var mitem = MItem[0];
            var SItem = data["S_PGC"];

            bool mFlag_Hg = false, mFlag_Bhg = false;
            bool mAllHg;
            bool realBhg = false;//标识直接不合格 。外观颜色不合格就不需要复试，直接不合格
            int mbhggs = 0;
            int mhgpds, mbhgpds = 0;
            string mGxl, mSjdj;
            bool GGCCBHG = false;//规格尺寸是否合格
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
                //  sitem["GGXH"] = sitem["GCWJ"] + sitem["GCBH"];
                mSjdj = sitem["SJDJ"]; //管材名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                mGxl = sitem["GXL"]; //环刚度(管系列)
                if (string.IsNullOrEmpty(mGxl))
                    mGxl = "";
                if (mGxl == "")
                    mGxl = "----";
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
                    mitem["G_RHWD"] = string.IsNullOrEmpty(mrsDj_Filter["RHWD"]) ? "0" : mrsDj_Filter["RHWD"].Trim(); //维卡软化软化温度
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
                    sitem["JCJG"] = "不下结论";
                    mitem["JCJGMS"] = "获取标准要求出错，找不到对应项";
                    continue;
                }
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
                    //双壁波纹管材 不做外观 其他都做的
                    MItem[0]["G_WG"] = "管材内外表面应清洁、光滑, 不应有气泡、明显的划伤、凹陷、杂质、颜色不均等缺陷。 管材两端应切割平整, 并与管材轴线垂直。";
                    MItem[0]["G_BZ"] = "管材应为黑色或蓝色, 黑色管材上应共挤出 至少三条蓝色条, 色条应沿管材圆周方向均匀分布。蓝色管材仅用于暗敷。";

                    if (mSjdj == "建筑排水用硬聚氯乙烯(PVC-U)管材")
                    {
                        MItem[0]["G_WG"] = "管材内外壁应光滑平整，不允许有气泡、裂口和明显划痕、凹陷、色差及分解变色。管材两端应切削整且与轴线垂直。螺棱应完整、光滑，无断棱、无变形等缺陷。螺棱旋转方向应为逆时针方向。";
                        MItem[0]["G_BZ"] = "通用型管材一般为白色，其他颜色由供需双方协商确定。";
                    }
                    else if (mSjdj == "建筑排水用硬聚氯乙烯(PVC-U)结构壁管材")
                    {
                        MItem[0]["G_WG"] = "管材内外表面应清洁、光滑, 无气泡、裂口和明显的划伤、凹陷、杂质、颜色不均及分解变色线。 管材应完整无缺损，浇口及溢边应修出平整。";
                        MItem[0]["G_BZ"] = "管件一般为白色或灰色，其他颜色可由供需双方协商确定。";
                    }
                    else if (mSjdj == "埋地用聚乙烯(PE)双壁波纹管材")
                    {
                        MItem[0]["G_WG"] = "管材内外壁不允许有气泡、凹陷、明显的杂质和不规则波纹等其他明显缺陷。管材两端应平整且与轴线垂直，插口端位于波谷取。管材波谷区应紧密熔断，不应出现脱开现象。";
                        MItem[0]["G_BZ"] = "管材的内外层各自的颜色应均匀一致，外层一般为黑色,其他颜色时可由供需双方协商。";
                    }
                    else if (mSjdj == "埋地排水用钢带增强聚乙烯(PE)螺旋波纹管")
                    {
                        MItem[0]["G_BZ"] = "管材颜色宜为黑色，色泽应均匀。当采用其他颜色时，可由供需双方协商。";
                        MItem[0]["G_WG"] = "管材内表面应光滑平整，外部波纹形应规整；管材内外壁应无气泡、无裂纹和可见杂质。管材采用螺旋形端口时，切口应选在管材波谷的无钢带处，且切口两端应在管材的同一纵向线。管材采用平面形端口时，切口应与管材轴线垂直。管材在切割后的断面应修整，无毛刺，管材端口及空腔部分应密封，不允许钢带外露。。";
                    }
                    else if (mSjdj == "排水用芯层发泡硬聚氯乙烯(PVC-U)管材")
                    {
                        MItem[0]["G_BZ"] = "管材内外表层一般为白色或者灰色，可由供需双方协商。";
                        MItem[0]["G_WG"] = "管材内表面应光滑平整，不允许有气泡、砂眼、裂口和明显的痕纹、杂质、色泽不均匀及分解变色线；管材端口应平整且与轴线垂直；管材芯层与外表面曾应紧密熔接，无分脱现象。";
                    }


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

                                if (GetSafeDecimal(sitem["WJ" + i + "_" + j]) < wjMin || GetSafeDecimal(sitem["WJ" + i + "_" + j]) > wjMax) //该组外径合格，则去掉该组，如果大于1，尺寸不合格
                                {
                                    bhg++;
                                    //单组不合格
                                    goto DZBHG_FLAG;
                                }

                                flag++;
                                sum += GetSafeDecimal(sitem["WJ" + i + "_" + j]);
                            }

                            if (bhg > 1)
                            {
                                break;
                            }

                            arrWJ.Add(Math.Round(sum / flag, 1));
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
                            MItem[0]["PJWJ1"] = Round((double)arrWJ[0], 1).ToString("0.0");
                            MItem[0]["PJWJ2"] = Round((double)arrWJ[arrWJ.Count - 1], 1).ToString("0.0");
                        }
                        else if (zj <= 1600)
                        {
                            MItem[0]["PJWJ1"] = (Round((double)arrWJ[0] * 5, 0) / 5).ToString();
                            MItem[0]["PJWJ2"] = (Round((double)arrWJ[arrWJ.Count - 1] * 5, 0) / 5).ToString();
                        }
                        else
                        {
                            MItem[0]["PJWJ1"] = (Round((double)arrWJ[0] * 5, 0) / 5).ToString();
                            MItem[0]["PJWJ2"] = (Round((double)arrWJ[arrWJ.Count - 1] * 5, 0) / 5).ToString();
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
                        if (bh <= 10)
                        {
                            sitem["PJBH1"] = (Round((double)arrBH[0] / 5.0, 2) * 5).ToString("0.00");
                            sitem["PJBH2"] = (Round((double)arrBH[arrBH.Count - 1] / 5.0, 2) * 5).ToString("0.00");
                        }
                        else if (bh > 10 && bh <= 30)
                        {
                            sitem["PJBH1"] = Round((double)arrBH[0], 1).ToString("0.0");
                            sitem["PJBH2"] = Round((double)arrBH[arrBH.Count - 1], 1).ToString("0.0");
                        }
                        else
                        {
                            sitem["PJBH1"] = Round((double)arrBH[0], 1).ToString("0.0");
                            sitem["PJBH2"] = Round((double)arrBH[arrBH.Count - 1], 1).ToString("0.0");
                        }
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
                    sitem["BGBZYQ" + curJcxmCount] = "----";
                    sitem["BGSCJG" + curJcxmCount] = "----";
                    sitem["BGDXPD" + curJcxmCount] = "----";
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
                        PJ = Math.Round(((GetSafeDecimal(sitem["RHWD1"]) + GetSafeDecimal(sitem["RHWD2"])) / 2), 1);
                        mitem["RHWD"] = Math.Round(PJ, 1).ToString();
                        mitem["RHWD_HG"] = IsQualified(mitem["G_RHWD"], mitem["RHWD"], false);
                    }
                    else
                    {
                        PJ = Math.Round(((GetSafeDecimal(sitem["RHWD1"]) + GetSafeDecimal(sitem["RHWD2"])) / 2), 1);

                        PJ1 = Math.Round(((GetSafeDecimal(sitem["RHWD3"]) + GetSafeDecimal(sitem["RHWD4"])) / 2), 1);

                        mitem["RHWD"] = Math.Round(PJ, 1).ToString();
                        mitem["RHWD_F"] = Math.Round(PJ1, 1).ToString();
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

                    Yi = GetSafeDouble(sitem["GCWJ"]) * 0.03 / 1000;
                    if ((GetSafeDouble(MItem[0]["HGD_LI1"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI2"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI3"]) * Yi != 0))
                    {
                        S1 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI1"]) / ((GetSafeDouble(MItem[0]["HGD_LI1"]) / 1000) * Yi);
                        S2 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI2"]) / ((GetSafeDouble(MItem[0]["HGD_LI2"]) / 1000) * Yi);
                        S3 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI3"]) / ((GetSafeDouble(MItem[0]["HGD_LI3"]) / 1000) * Yi);
                        MItem[0]["HGD"] = ((S1 + S2 + S3) / 3).ToString("0.0");
                        MItem[0]["HGD_HG"] = IsQualified(MItem[0]["G_HGD"], MItem[0]["HGD"]);

                        if (fj != 0)
                        {
                            S4 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(sitem["HGD_FI4"]) / ((GetSafeDouble(sitem["HGD_LI4"]) / 1000) * Yi);
                            S5 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(sitem["HGD_FI5"]) / ((GetSafeDouble(sitem["HGD_LI5"]) / 1000) * Yi);
                            S6 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(sitem["HGD_FI6"]) / ((GetSafeDouble(sitem["HGD_LI6"]) / 1000) * Yi);
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
                if (jcxm.Contains("、二氯甲烷浸渍、"))
                {
                    jcxmCur = "二氯甲烷浸渍";
                    if (mitem["JWJZ_HG"] == "合格")
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
                        if (mtmpArray[xd].Contains("二氯甲烷浸渍"))
                        {
                            sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                            break;
                        }
                    }
                    sitem["BGDW" + curJcxmCount] = "----";
                    sitem["BGBZYQ" + curJcxmCount] = mitem["G_JWJZ"];
                    sitem["BGSCJG" + curJcxmCount] = mitem["JWJZ"];
                    sitem["BGDXPD" + curJcxmCount] = mitem["JWJZ_HG"];
                    curJcxmCount = curJcxmCount + 1;
                }
                else
                {
                    mitem["JWJZ"] = "----";
                    mitem["JWJZ_HG"] = "----";
                    mitem["G_JWJZ"] = "----";
                }
                if (jcxm.Contains("、扁平试验、"))
                {
                    jcxmCur = "扁平试验";
                    if (mitem["BPSY_HG"] == "合格")
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
                if (jcxm.Contains("、纵向回缩率、"))
                {
                    jcxmCur = "纵向回缩率";

                    double zxhsl1, zxhsl2, zxhsl3 = 0.0;
                    MItem[0]["HSLL0_1"] = "100";
                    MItem[0]["HSLL0_2"] = "100";
                    MItem[0]["HSLL0_3"] = "100";

                    zxhsl1 = Math.Abs(Double.Parse((100 * (GetSafeDouble(MItem[0]["HSLL0_1"]) - GetSafeDouble(MItem[0]["HSLLI_1"])) / GetSafeDouble(MItem[0]["HSLL0_1"])).ToString("0.00")));
                    zxhsl2 = Math.Abs(Double.Parse((100 * (GetSafeDouble(MItem[0]["HSLL0_2"]) - GetSafeDouble(MItem[0]["HSLLI_2"])) / GetSafeDouble(MItem[0]["HSLL0_2"])).ToString("0.00")));
                    zxhsl3 = Math.Abs(Double.Parse((100 * (GetSafeDouble(MItem[0]["HSLL0_3"]) - GetSafeDouble(MItem[0]["HSLLI_3"])) / GetSafeDouble(MItem[0]["HSLL0_3"])).ToString("0.00")));

                    MItem[0]["ZXHSL"] = ((zxhsl1 + zxhsl2 + zxhsl3) / 3).ToString("0.0") + "%";
                    mitem["ZXHSL_HG"] = IsQualified(mitem["G_ZXHSL"], mitem["ZXHSL"], false);

                    if (mSjdj == "排水用芯层发泡硬聚氯乙烯(PVC-U)管材")
                    {
                        mitem["G_ZXHSL"] += mitem["G_ZXHSL"] + "，且不分脱、不破裂";
                    }
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

                string CC = sitem["GCCC"];
                string HG = sitem["GXL"];
                if (HG == "----")
                {
                    HG = null;

                }
                if (CC.Contains("DN"))
                {
                    CC += " " + HG;
                }
                else
                {
                    CC = "DN " + sitem["GCWJ"] + HG;
                }

                sitem["GGXH"] = CC;

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