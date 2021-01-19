using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Calculates
{
    public class GSG : BaseMethods
    {
        public void Calc()
        {
            #region Code
            
            #region 参数定义

            var extraDJ = dataExtra["BZ_GSG_DJ"];
            var data = retData;
            var mAllHg = true;
            var mjcjg = "不合格";

            var SItems = data["S_GSG"];
            var MItem = data["M_GSG"];
            var jcxm = "";
            var jcxmCur = "";
            var jcxmBhg = "";
            var jsbeizhu = "";
            var ggxh = "";

            #endregion

            #region 计算
            foreach (var sItem in SItems){
                
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                sItem["CPBJ"] = sItem["CPMC"] + sItem["ZGWDLX"] +" "+ sItem["CPWX"] + sItem["CPCD"] + "×" + sItem["CPKD"] + "×" + sItem["CPHD"] + " " + sItem["PDBZ"];
                #region 获取等级表，指标标准值
                var mrsDj = extraDJ.FirstOrDefault(u => u["CPMC"] == sItem["CPMC"] && u["ZGWDLX"] == sItem["ZGWDLX"] && u["CPMD"] == sItem["CPMD"]);
                if (null == mrsDj)
                {
                    throw new Exception("获取指标标准值失败");
                }
                #endregion

                #region 将标准值从等级表赋值到S_QFS

                sItem["G_MD"] = mrsDj["MD"];
                sItem["G_ZLHSL"] = mrsDj["ZLHSL"];
                sItem["G_CCWDX"] = mrsDj["CCWDX"];
                sItem["G_DKKYQD"] = mrsDj["DKKYQD"];
                sItem["G_PJKYQD"] = mrsDj["PJKYQD"];
                sItem["G_DKKZQD"] = mrsDj["DKKZQD"];
                sItem["G_PJKZQD"] = mrsDj["PJKZQD"];
                double wd =GetSafeDouble( sItem["DRYQWD"])/100;
                wd = (int)wd;
                sItem["G_DSXS"] = mrsDj["dsxs" + wd];
                #endregion

                #region 密度
                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";
                    double Sum = 0;
                    for(int i = 1; i < 4; i++)
                    {
                        sItem["SCMD" + i] = Round(GetSafeDouble(sItem["HGHZLMD" + i]) / GetSafeDouble(sItem["SJTJ" + i]), 1).ToString();
                        Sum = Sum + GetSafeDouble(sItem["SCMD" + i]);
                    }
                    sItem["PJMD"] = Round(Sum / 3, 0).ToString("0");
                    sItem["HG_MD"] = IsQualified(sItem["G_MD"], sItem["PJMD"], false);
                    if (sItem["HG_MD"] == "不合格")
                    {
                        mjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }
                }
                else
                {
                    sItem["PJMD"] = "---";
                    sItem["G_MD"] = "---";
                    sItem["HG_MD"] = "---";

                }
                #endregion

                #region 质量含水率
                if (jcxm.Contains("、质量含水率、")){
                    jcxmCur = "质量含水率";
                    double Sum = 0;
                    for(int i = 1; i < 4; i++)
                    {
                        sItem["ZLHSL" + i] = Round((GetSafeDouble(sItem["ZRZL" + i]) - GetSafeDouble(sItem["HSHGZL" + i])) / GetSafeDouble(sItem["HSHGZL" + i]), 2).ToString();
                        Sum = Sum + GetSafeDouble(sItem["ZLHSL" + i]);
                    }
                    sItem["PJZLHSL"] = Round(Sum / 3, 1).ToString("0.0");
                    sItem["HG_ZLHSL"] = IsQualified(sItem["G_ZLHSL"], sItem["PJZLHSL"], false);
                    if (sItem["HG_ZLHSL"] == "不合格")
                    {
                        mjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }
                }
                else
                {
                    sItem["PJZLHSL"] = "---";
                    sItem["G_ZLHSL"] = "---";
                    sItem["HG_ZLHSL"] = "---";

                }
                #endregion
                
                #region 尺寸稳定性
                if (jcxm.Contains("、尺寸稳定性、")){
                    jcxmCur = "尺寸稳定性";
                    #region 长度
                    double cd1, cd2, cd3;
                    cd1 = cd2 = cd3 = 0;
                    for (int i = 1; i < 4; i++){
                        cd1 = cd1 + GetDouble(sItem["SYHCD1_"+i]);
                        cd2 = cd2 + GetDouble(sItem["SYHCD2_" + i]);
                        cd3 = cd3 + GetDouble(sItem["SYHCD3_" + i]);
                    }
                    sItem["PJCD"] = Round((cd1 + cd2 + cd3) / 3, 1).ToString("0.0");
                    sItem["CDBHL"] = Round((GetSafeDouble(sItem["PJCD"]) - GetSafeDouble(sItem["CSCD"])) / GetSafeDouble(sItem["CSCD"]), 1).ToString("0.0");
                   

                    #endregion

                    #region 宽度
                    double KD1, KD2, KD3;
                    KD1 = KD2 = KD3 = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        KD1 = KD1 + GetDouble(sItem["SYHKD1_" + i]);
                        KD2 = KD2 + GetDouble(sItem["SYHKD2_" + i]);
                        KD3 = KD3 + GetDouble(sItem["SYHKD3_" + i]);
                    }
                    sItem["PJKD"] = Round((KD1 + KD2 + KD3) / 3, 1).ToString("0.0");
                    sItem["KDBHL"] = Round((GetSafeDouble(sItem["PJKD"]) - GetSafeDouble(sItem["CSKD"])) / GetSafeDouble(sItem["CSKD"]), 1).ToString("0.0");


                    #endregion

                    #region 厚度
                    double HD1, HD2, HD3;
                    HD1 = HD2 = HD3 = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        HD1 = HD1 + GetDouble(sItem["SYHHD1_" + i]);
                        HD2 = HD2 + GetDouble(sItem["SYHHD2_" + i]);
                        HD3 = HD3 + GetDouble(sItem["SYHHD3_" + i]);
                    }
                    sItem["PJHD"] = Round((HD1 + HD2 + HD3) / 3, 1).ToString("0.0");
                    sItem["HDBHL"] = Round((GetSafeDouble(sItem["PJHD"]) - GetSafeDouble(sItem["CSHD"])) / GetSafeDouble(sItem["CSHD"]), 1).ToString("0.0");


                    #endregion

                    if( IsQualified(sItem["G_CCWDX"], sItem["CDBHL"], false)=="不合格"|| IsQualified(sItem["G_CCWDX"], sItem["KDBHL"], false) == "不合格" || IsQualified(sItem["G_CCWDX"], sItem["HDBHL"], false) == "不合格")
                    {
                        sItem["HG_CCWDX"] = "不合格";
                        mjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["CDBHL"] = "---";
                    sItem["KDBHL"] = "---";
                    sItem["HDBHL"] = "---";
                    sItem["G_CCWDX"] = "---";
                    sItem["HG_CCWDX"] = "---";
                }
                #endregion

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、")){
                    jcxmCur = "抗压强度";
                    var HG_DKKYQD = "";
                    double Sum = 0;
                    double MJ = GetSafeDouble(sItem["SJCD"]) * GetSafeDouble(sItem["SJKD"]);
                    for(int i = 1; i < 5; i++)
                    {
                        sItem["KYQD" + i] =Round( GetSafeDouble(sItem["KYHZ" + i]) / MJ,3).ToString();
                        Sum = Sum + GetSafeDouble(sItem["KYQD" + i]);
                        HG_DKKYQD = IsQualified(sItem["G_DKKYQD"], sItem["KYQD" + i] , false);
                    }
                    sItem["PJKYQD"] = Round(Sum / 4, 2).ToString();
                    if(IsQualified(sItem["G_PJKYQD"], sItem["PJKYQD"], false)=="不合格"&& HG_DKKYQD == "不合格")
                    {
                        sItem["HG_KYQD"] = "不合格";
                        mjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                else
                {
                    sItem["PJKYQD"] = "---";
                    sItem["G_DKKYQD"] = "---";
                    sItem["G_PJKYQD"] = "---";
                    sItem["HG_KYQD"] = "---";



                }
                #endregion

                #region 抗折强度
                if (jcxm.Contains("、抗折强度、"))
                {
                    jcxmCur = "抗折强度";
                    var kzcd = GetSafeDouble(sItem["KZCD"]);
                    var kzKd = GetSafeDouble(sItem["KZKD"]);
                    var HG_DKKZQD = "";
                    double Sum = 0;
                    double MJ = GetSafeDouble(sItem["SJCD"]) * GetSafeDouble(sItem["SJKD"]);
                    for (int i = 1; i < 5; i++)
                    {
                        sItem["KZQD" + i] = Round(3*GetSafeDouble(sItem["ZXJJ"])* GetSafeDouble(sItem["KZHZ" + i]) / 2/kzcd/kzKd/kzKd, 3).ToString();
                        Sum = Sum + GetSafeDouble(sItem["KZQD" + i]);
                        HG_DKKZQD = IsQualified(sItem["G_DKKZQD"], sItem["KZQD" + i], false);
                    }
                    sItem["PJKZQD"] = Round(Sum / 4, 2).ToString();
                    if (IsQualified(sItem["G_PJKZQD"], sItem["PJKZQD"], false) == "不合格" && HG_DKKZQD == "不合格")
                    {
                        sItem["HG_KZQD"] = "不合格";
                        mjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                else
                {
                    sItem["PJKZQD"] = "---";
                    sItem["G_DKKZQD"] = "---";
                    sItem["G_PJKZQD"] = "---";
                    sItem["HG_KZQD"] = "---";



                }
                #endregion


                #region 导热系数
                if (jcxm.Contains("、导热系数、"))
                {
                    jcxmCur = "导热系数";
                    if (IsQualified(sItem["G_DSXS"], sItem["SCDRXS"], false) == "不合格")
                    {
                        sItem["HG_DRXS"] = "不合格";
                        mjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                else
                {
                    sItem["G_DSXS"] = "---";
                    sItem["SCDRXS"] = "---";
                    sItem["HG_DRXS"] = "---";
                }
                #endregion
            }

            #endregion

            #region 主表判定
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";

                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }

            else {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            #endregion

            #endregion
        }
    }
}
