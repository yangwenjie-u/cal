using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
namespace Calculates
{
    public class FST : BaseMethods
    {
        public void Calc()
        {
            #region  参数定义
            double[] mkyqdArray = new double[3];
            int mbhggs = 0;
            string dCpmc, dLx, dZf, dDj, dBzh, mSjdj;
            string mJSFF;
            bool mAllHg = true;
            int QDJSFF = 0;
            bool mFlag_Hg = false, mFlag_Bhg = false;

            var data = retData;
            var mrsDj = dataExtra["BZ_FST_DJ"];
            var MItem = data["M_FST"];
            var mitem = MItem[0];
            var SItem = data["S_FST"];
            #endregion

            #region  计算开始
            mitem["JCJGMS"] = "";
            mSjdj = "";
            //int QDJSFF = 0;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sitem in SItem)
            {
                dCpmc = string.IsNullOrEmpty(sitem["CPMC"]) ? "" : sitem["CPMC"].Trim();
                dLx = string.IsNullOrEmpty(sitem["LX"]) ? "" : sitem["LX"].Trim();
                dDj = string.IsNullOrEmpty(sitem["DJ"]) ? "" : sitem["DJ"].Trim();
                dZf = string.IsNullOrEmpty(sitem["ZF"]) ? "" : sitem["ZF"].Trim();
                dBzh = string.IsNullOrEmpty(sitem["BZH"]) ? "" : sitem["BZH"].Trim();
                if (dDj != "----")
                    mSjdj = mSjdj + dDj;
                if (dZf != "----")
                    mSjdj = mSjdj + dZf;
                if (dLx != "----")
                    mSjdj = mSjdj + dLx;
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_item = mrsDj.FirstOrDefault(x => x["MC"].Contains(dCpmc) && x["LX"].Contains(dLx) && x["DJ"].Contains(dDj) && x["ZF"].Contains(dZf) && x["BZH"].Contains(dBzh));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    mJSFF = string.IsNullOrEmpty(mrsDj_item["JSFF"]) ? "" : mrsDj_item["JSFF"].Trim().ToLower();
                    QDJSFF = string.IsNullOrEmpty(mrsDj_item["G_QDJSFF"]) ? 1 : GetSafeInt(mrsDj_item["G_QDJSFF"]);
                    sitem["CQS"] = string.IsNullOrEmpty(mrsDj_item["G_QDJSFF"]) ? "1" : mrsDj_item["G_QDJSFF"];
                    string which = mrsDj_item["WHICH"];
                    mitem["G_RQZZT"] = mrsDj_item["G_RQZZT"];
                    mitem["G_GTHL"] = mrsDj_item["G_GTHL"];
                    mitem["G_BGSJ"] = mrsDj_item["G_BGSJ"];
                    mitem["G_SGSJ"] = mrsDj_item["G_SGSJ"];
                    mitem["G_TMWG"] = mrsDj_item["G_TMWG"];
                    mitem["G_TCHD"] = mrsDj_item["G_TCHD"];
                    mitem["G_SGX"] = mrsDj_item["G_SGX"];
                    mitem["G_NJQD"] = mrsDj_item["G_NJQD"];
                    mitem["G_JNJQD"] = mrsDj_item["G_JNJQD"];
                    mitem["G_RSCL"] = mrsDj_item["G_RSCL"];
                    mitem["G_SCL"] = mrsDj_item["G_SCL"];
                    mitem["G_JRSSL"] = mrsDj_item["G_JRSSL"];
                    mitem["G_JRSSL2"] = mrsDj_item["G_JRSSL2"];
                    mitem["G_KYQD"] = mrsDj_item["G_KYQD"];
                    mitem["G_GMD"] = mrsDj_item["G_GMD"];
                    mitem["G_BTSX"] = mrsDj_item["G_BTSX"];
                    mitem["G_NSX"] = mrsDj_item["G_NSX"];
                    mitem["G_NJX"] = mrsDj_item["G_NJX"];
                    mitem["G_NXSX"] = mrsDj_item["G_NXSX"];
                    mitem["G_NWBX"] = mrsDj_item["G_NWBX"];
                    mitem["G_DWWDX"] = mrsDj_item["G_DWWDX"];
                    mitem["G_DWRD"] = mrsDj_item["G_DWRD"];
                    mitem["G_NRD"] = mrsDj_item["G_NRD"];
                    mitem["G_DWWZ"] = mrsDj_item["G_DWWZ"];

                    mitem["G_SLQD"] = mrsDj_item["G_SLQD"];

                    //要求初级干燥抗裂性（6h）
                    mitem["G_DWWZ"] = mrsDj_item["G_DWWZ"];

                    //施工性
                    mitem["G_SGX"] = mrsDj_item["G_SGX"];
                    //腻子膜柔韧性
                    mitem["G_NZMRRX"] = mrsDj_item["G_NZMRRX"];

                    //初期干燥抗裂性
                    //打磨性
                    mitem["G_DMX"] = mrsDj_item["G_DMX"];

                    mitem["G_DNJQD"] = mrsDj_item["G_DNJQD"];
                    //动态抗开裂性
                    mitem["G_DTKKLX"] = mrsDj_item["G_DTKKLX"];

                    //吸水量
                    mitem["G_XSL"] = mrsDj_item["G_XSL"];
                    //低温成膜性
                    mitem["G_DWCMX"] = mrsDj_item["G_DWCMX"];
                    //耐碱性
                    mitem["G_NJX"] = mrsDj_item["G_NJX"];
                    //对比率
                    mitem["G_DBL"] = mrsDj_item["G_DBL"];
                    //耐沾污性
                    mitem["G_NZWX"] = mrsDj_item["G_NZWX"];


                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = "找不到对应的等级";
                }
                if (dBzh == "JC/T 3049-1998")
                {
                    mitem["G_BGSJ"] = "＜" + mitem["G_BGSJ"];
                    mitem["G_NJQD"] = "＞" + mitem["G_NJQD"];
                }
                else
                {
                    mitem["G_BGSJ"] = "≤" + mitem["G_BGSJ"];
                    mitem["G_NJQD"] = "≥" + mitem["G_NJQD"];
                }
                mbhggs = 0;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                bool sign;
                int xd, Gs, Ws;
                double md1, md2, md, sum;
                mFlag_Hg = false;
                mFlag_Bhg = false;
                mbhggs = 0;

                if (jcxm.Contains("、拉伸强度、"))
                {
                    jcxmCur = "拉伸强度";
                    Gs = 0;
                    sum = 0;
                    Ws = string.IsNullOrEmpty(sitem["CQS"]) ? 1 : GetSafeInt(sitem["CQS"]);
                    for (xd = 1; xd <= 5; xd++)
                    {
                        sign = true;
                        sign = IsNumeric(sitem["NJQD_KD" + xd]) && !string.IsNullOrEmpty(sitem["NJQD_KD" + xd]) ? sign : false;
                        sign = IsNumeric(sitem["NJQD_HD" + xd]) && !string.IsNullOrEmpty(sitem["NJQD_HD" + xd]) ? sign : false;
                        sign = IsNumeric(sitem["NJQD_HZ" + xd]) && !string.IsNullOrEmpty(sitem["NJQD_HZ" + xd]) ? sign : false;
                        if (sign)
                        {
                            md1 = Conversion.Val(sitem["NJQD_KD" + xd].Trim());
                            md2 = Conversion.Val(sitem["NJQD_HD" + xd].Trim());
                            md = Conversion.Val(sitem["NJQD_HZ" + xd].Trim());
                            md = md / md1 / md2;
                            md = Round(md, Ws);

                            sitem["NJQD" + xd] = Ws == 1 ? md.ToString("0.0") : sitem["NJQD" + xd];
                            sitem["NJQD" + xd] = Ws == 2 ? md.ToString("0.00") : sitem["NJQD" + xd];
                            sitem["NJQD" + xd] = Ws == 3 ? md.ToString("0.000") : sitem["NJQD" + xd];
                            sum = sum + md;
                            Gs = Gs + 1;
                        }
                        else
                            sitem["NJQD" + xd] = "----";
                    }
                    if (sitem["CPMC"] == "聚氨酯防水涂料")
                    {
                        double pjmd = sum / Gs;
                        sum = 0;
                        for (xd = 1; xd <= 5; xd++)
                        {
                            md1 = Conversion.Val(sitem["NJQD" + xd].Trim());
                            if (Math.Abs(md1 - pjmd) <= pjmd * 0.15)
                                sum = sum + md1;
                            else
                                Gs = Gs - 1;
                        }
                    }
                    if (Gs >= 3)
                    {
                        double pjmd = Round((sum / Gs), Ws);
                        sitem["NJQD"] = Ws == 1 ? pjmd.ToString("0.0") : sitem["NJQD"];
                        sitem["NJQD"] = Ws == 2 ? pjmd.ToString("0.00") : sitem["NJQD"];
                        sitem["NJQD"] = Ws == 3 ? pjmd.ToString("0.000") : sitem["NJQD"];
                        sitem["HG_NJQD"] = IsQualified(mitem["G_NJQD"], sitem["NJQD"], false);
                        mbhggs = sitem["HG_NJQD"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_NJQD"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["HG_NJQD"] = "重新试验";
                        sitem["NJQD"] = "----";
                    }
                }
                else
                {
                    sitem["NJQD"] = "----";
                    sitem["HG_NJQD"] = "----";
                    mitem["G_NJQD"] = "----";
                }
                if (jcxm.Contains("、干燥时间(表干)、") || jcxm.Contains("、表干时间、") || jcxm.Contains("、干燥时间、"))
                {
                    if (string.IsNullOrEmpty(sitem["SGSJ1"]) || string.IsNullOrEmpty(sitem["SGSJ2"]))
                    {
                        throw new Exception("请输入有效的表干时间");
                    }
                    jcxmCur = "表干时间";

                    md1 = Conversion.Val(sitem["BGSJ1"]);
                    md2 = Conversion.Val(sitem["BGSJ2"]);
                    md = md1 + md2;
                    if (md < 10)
                    {
                        //精确到小数点1位
                        sitem["BGSJ"] = (Round((md1 + md2) / 2, 1)).ToString();
                    }
                    else if (md < 100)
                    {
                        //精确到个位
                        sitem["BGSJ"] = (Round((md1 + md2) / 2, 0)).ToString();
                    }
                    else if (md < 1000)
                    {
                        //精确到十位
                        sitem["BGSJ"] = (Round((md1 + md2) / 2 / 10, 0) * 10).ToString();
                    }
                    else
                    {
                        sitem["BGSJ"] = (Round((md1 + md2) / 2 / 100, 0) * 100).ToString();
                    }

                    sitem["HG_BGSJ"] = IsQualified(mitem["G_BGSJ"], sitem["BGSJ"], false);
                    if (sitem["HG_BGSJ"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["BGSJ"] = "----";
                    sitem["HG_BGSJ"] = "----";
                    mitem["G_BGSJ"] = "----";
                }

                if (jcxm.Contains("、干燥时间(实干)、") || jcxm.Contains("、实干时间、") || jcxm.Contains("、干燥时间、"))
                {
                    if (string.IsNullOrEmpty(sitem["SGSJ1"]) || string.IsNullOrEmpty(sitem["SGSJ2"]))
                    {
                        throw new Exception("请输入有效的实干时间");
                    }
                    jcxmCur = "实干时间";

                    md1 = Conversion.Val(sitem["SGSJ1"]);
                    md2 = Conversion.Val(sitem["SGSJ2"]);
                    md = md1 + md2;
                    if (md < 10)
                    {
                        //精确到小数点1位
                        sitem["SGSJ"] = (Round((md1 + md2) / 2, 1)).ToString();
                    }
                    else if (md < 100)
                    {
                        //精确到个位
                        sitem["SGSJ"] = (Round((md1 + md2) / 2, 0)).ToString();
                    }
                    else if (md < 1000)
                    {
                        //精确到十位
                        sitem["SGSJ"] = (Round((md1 + md2) / 2 / 10, 0) * 10).ToString();
                    }
                    else
                    {
                        sitem["SGSJ"] = (Round((md1 + md2) / 2 / 100, 0) * 100).ToString();
                    }
                    mitem["G_SGSJ"] = "≤" + mitem["G_SGSJ"].Trim();
                    sitem["HG_SGSJ"] = IsQualified(mitem["G_SGSJ"], sitem["SGSJ"], false);
                    mbhggs = sitem["HG_SGSJ"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (sitem["HG_SGSJ"] != "不合格")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sitem["SGSJ"] = "----";
                    sitem["HG_SGSJ"] = "----";
                    mitem["G_SGSJ"] = "----";
                }

                if (jcxm.Contains("、低温稳定性、") || jcxm.Contains("、低温稳定性(3次循环)、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "低温稳定性,低温稳定性(3次循环)");

                    if ("合格" != sitem["HG_DWWDX"] && sitem["HG_DWWDX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["DWWDX"] = "----";
                    sitem["HG_DWWDX"] = "----";
                    mitem["G_DWWDX"] = "----";
                }

                if (jcxm.Contains("、固体含量、"))
                {
                    sum = 0;
                    jcxmCur = "固体含量";
                    for (int i = 1; i < 3; i++)
                    {
                        if ((Conversion.Val(sitem["GTHL_M1" + i]) - Conversion.Val(sitem["GTHL_PYM"]) <= 0))
                        {
                            throw new Exception("固体含量指标数据不合法。");
                        }
                        md1 = 100 * (Conversion.Val(sitem["GTHL_M0" + i]) - Conversion.Val(sitem["GTHL_PYM"])) / (Conversion.Val(sitem["GTHL_M1" + i]) - Conversion.Val(sitem["GTHL_PYM"]));
                        sum += md1;
                    }

                    sitem["GTHL"] = (sum / 2).ToString("0");
                    mitem["G_GTHL"] = "≥" + mitem["G_GTHL"].Trim();
                    sitem["HG_GTHL"] = IsQualified(mitem["G_GTHL"], sitem["GTHL"], false);
                    mbhggs = sitem["HG_GTHL"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (sitem["HG_GTHL"] != "不合格")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sitem["GTHL"] = "----";
                    sitem["HG_GTHL"] = "----";
                    mitem["G_GTHL"] = "----";
                }
                if (jcxm.Contains("、断裂延伸率、") || jcxm.Contains("、断裂伸长率、"))
                {
                    sum = 0;
                    Gs = 0;
                    jcxmCur = CurrentJcxm(jcxm, "断裂延伸率,断裂伸长率");

                    List<double> lsScl = new List<double>();
                    for (xd = 1; xd <= 5; xd++)
                    {
                        md = 100 * (GetSafeDouble(sitem["SCL_L0" + xd]) - GetSafeDouble(sitem["SCL_L1" + xd])) / GetSafeDouble(sitem["SCL_L1" + xd]);
                        lsScl.Add(md);
                        sum += md;
                    }

                    double pjmd = sum / 5;
                    for (int i = 1; i <= 5; i++)
                    {
                        md1 = lsScl[i - 1];
                        if (Math.Abs(md1 - pjmd) <= pjmd * 0.15)
                            sum = sum + md1;
                        else
                            Gs++;
                    }
                    if (Gs >= 3)
                    {
                        sitem["SCL"] = (sum / Gs).ToString("0");
                        mitem["G_SCL"] = "≥" + mitem["G_SCL"].Trim();
                        sitem["HG_SCL"] = IsQualified(mitem["G_SCL"], sitem["SCL"], false);
                        mbhggs = sitem["HG_SCL"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_SCL"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["SCL"] = "----";
                        sitem["HG_SCL"] = "需复试";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }

                }
                else
                {
                    sitem["SCL"] = "----";
                    sitem["HG_SCL"] = "----";
                    mitem["G_SCL"] = "----";
                }

                if (jcxm.Contains("、不透水性、"))
                {
                    jcxmCur = "不透水性";
                    if ("合格" != sitem["HG_BTSX"] && sitem["HG_BTSX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["BTSX"] = "----";
                    sitem["HG_BTSX"] = "----";
                    mitem["G_BTSX"] = "----";
                }
                if (jcxm.Contains("、低温柔性、"))
                {
                    jcxmCur = "低温柔性";
                    if ("合格" != sitem["HG_DWRD"] && sitem["HG_DWRD"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["DWRD"] = "----";
                    sitem["HG_DWRD"] = "----";
                    mitem["G_DWRD"] = "----";
                }

                if (jcxm.Contains("、低温弯折性、"))
                {
                    jcxmCur = "低温弯折性";
                    if (sitem["HG_DWWZ"].Trim() != "符合" && sitem["HG_DWWZ"].Trim() != "合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["DWWZ"] = "----";
                    sitem["HG_DWWZ"] = "----";
                    mitem["G_DWWZ"] = "----";
                }

                if (jcxm.Contains("、撕裂强度、"))
                {
                    sum = 0;
                    md2 = 0;

                    jcxmCur = "撕裂强度";
                    int hgsl = 0;
                    for (int i = 1; i <= 5; i++)
                    {
                        sum += GetSafeDouble(sitem["SLQD" + i]);
                    }
                    md = Round(sum / 5, 0);
                    for (int i = 1; i <= 5; i++)
                    {
                        md1 = Math.Abs(md - GetSafeDouble(sitem["SLQD" + i])) / md;

                        if (md1 > 0.15)
                        {
                            continue;
                        }
                        md2 += GetSafeDouble(sitem["SLQD" + i]);
                        hgsl++;

                    }

                    if (hgsl >= 3)
                    {
                        sitem["SLQDPJ"] = Round(md2 / hgsl, 0).ToString();
                        mitem["HG_SLQD"] = IsQualified(mitem["G_SLQD"], sitem["SLQDPJ"]);
                        if (mitem["HG_SLQD"] == "合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        mitem["HG_SLQD"] = "需复试";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        sitem["SLQDPJ"] = "----";
                    }
                }
                else
                {
                    mitem["G_SLQD"] = "----";
                    mitem["HG_SLQD"] = "----";
                }


                //if (mbhggs == 0)
                //{
                //    mitem["JCJGMS"] = "该组试件所检项目符合" + mitem["PDBZ"] + "标准要求。";
                //    sitem["JCJG"] = "合格";
                //}
                //if (mbhggs >= 1)
                //{
                //    mitem["JCJGMS"] = "该组试件不符合" + mitem["PDBZ"] + "标准要求。";
                //    sitem["JCJG"] = "不合格";
                //    if (mFlag_Bhg && mFlag_Hg)
                //        mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                //}
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");

            }

            //主表总判断赋值
            //综合判断
            if (mbhggs == 0)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            if (mbhggs > 0)
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //if (mFlag_Bhg && mFlag_Hg)
                //    MItem[0]["JCJGMS"] = "依据标准" + MItem[0]["PDBZ"] + ",所检项目" + jcxmBhg.TrimEnd('、') + "不符合标准要求。";
            }
            #endregion
        }
    }
}
