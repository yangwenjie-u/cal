﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
namespace Calculates
{
    public class TL : BaseMethods
    {
        public void Calc()
        {
            #region  参数定义
            double[] mkyqdArray = new double[3];
            int mbhggs;
            string dCpmc, dLx, dZf, dDj, dBzh, mSjdj;
            string mJSFF;
            bool mAllHg = true;
            int QDJSFF = 0;
            bool mFlag_Hg, mFlag_Bhg = false;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_TL_DJ"];
            var MItem = data["M_TL"];
            var mitem = MItem[0];
            var SItem = data["S_TL"];
            #endregion

            #region  计算开始
            mitem["JCJGMS"] = "";
            mSjdj = "";
            //int QDJSFF = 0;
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
                    mitem["JCJG,S"] = "找不到对应的等级";
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

                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    //以下初始化报告字段

                    if (jcxm.Contains("、干燥时间(表干)、") || jcxm.Contains("、表干时间、"))
                    {
                        sitem["HG_BGSJ"] = IsQualified(mitem["G_BGSJ"], sitem["BGSJ"], false);
                        mbhggs = sitem["HG_BGSJ"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_BGSJ"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["BGSJ"] = "----";
                        sitem["HG_BGSJ"] = "----";
                        mitem["G_BGSJ"] = "----";
                    }
                    if (jcxm.Contains("、干燥时间(实干)、") || jcxm.Contains("、实干时间、"))
                    {
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
                    if (jcxm.Contains("、粘结强度(标准)、") || jcxm.Contains("、无处理拉伸强度、") || jcxm.Contains("、粘结强度、") || jcxm.Contains("、拉伸强度(无处理)、") || jcxm.Contains("、拉伸强度、"))
                    {
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
                        else if (Gs > 0 && Gs < 3)
                            sitem["NJQD"] = "重新试验";
                        else
                        {
                            sitem["HG_NJQD"] = "----";
                            sitem["NJQD"] = "----";
                        }
                    }
                    else
                    {
                        sitem["NJQD"] = "----";
                        sitem["HG_NJQD"] = "----";
                        mitem["G_NJQD"] = "----";
                    }
                    if (jcxm.Contains("、固体含量、"))
                    {
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
                    if (jcxm.Contains("、断裂伸长率(无处理)、") || jcxm.Contains("、断裂伸长率、") || jcxm.Contains("、断裂延伸率、"))
                    {
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
                        sitem["HG_SCL"] = "----";
                        mitem["G_SCL"] = "----";
                    }
                    if (jcxm.Contains("、拉伸强度(热处理后)、"))
                    {
                        mitem["G_JNJQD"] = "≥" + mitem["G_JNJQD"].Trim();
                        sitem["HG_JNJQD"] = IsQualified(mitem["G_JNJQD"], sitem["JNJQD"], false);
                        mbhggs = sitem["HG_JNJQD"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_JNJQD"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["JNJQD"] = "----";
                        sitem["HG_JNJQD"] = "----";
                        mitem["G_JNJQD"] = "----";
                    }
                    if (jcxm.Contains("、断裂伸长率(热处理后)、"))
                    {
                        mitem["G_RSCL"] = "≥" + mitem["G_RSCL"].Trim();
                        sitem["HG_RSCL"] = IsQualified(mitem["G_RSCL"], sitem["RSCL"], false);
                        mbhggs = sitem["HG_RSCL"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_RSCL"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;
                    }
                    else
                    {
                        sitem["RSCL"] = "----";
                        sitem["HG_RSCL"] = "----";
                        mitem["G_RSCL"] = "----";
                    }
                    if (jcxm.Contains("、低温弯折性、"))
                    {
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
                    if (jcxm.Contains("、低温柔度、") || jcxm.Contains("、低温柔性、"))
                    {
                        if (sitem["HG_DWRD"].Trim() != "符合" && sitem["HG_DWRD"].Trim() != "合格")
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
                    if (jcxm.Contains("、耐热度、") || jcxm.Contains("、耐热性、"))
                    {
                        if (sitem["HG_NRD"].Trim() != "符合" && sitem["HG_NRD"].Trim() != "合格")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["NRD"] = "----";
                        sitem["HG_NRD"] = "----";
                        mitem["G_NRD"] = "----";
                    }
                    if (jcxm.Contains("、不透水性、"))
                    {
                        if (sitem["HG_BTSX"].Trim() != "符合" && sitem["HG_BTSX"].Trim() != "合格")
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
                }
                else
                {
                    if (jcxm.Contains("、容器中状态、"))
                    {
                        if ("符合" != sitem["HG_RQZZT"] && "合格" != sitem["HG_RQZZT"])
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
                        sitem["RQZZT"] = "----";
                        sitem["HG_RQZZT"] = "----";
                        mitem["G_RQZZT"] = "----";
                    }

                    if (jcxm.Contains("、施工性、"))
                    {
                        if ("符合" != sitem["HG_SGX"] && "合格" != sitem["HG_SGX"])
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
                        sitem["SGX"] = "----";
                        sitem["HG_SGX"] = "----";
                        mitem["G_SGX"] = "----";
                    }

                    if (jcxm.Contains("、干燥时间（表干）、") || jcxm.Contains("、表干时间、"))
                    {
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

                    if (jcxm.Contains("、干燥时间（实干）、") || jcxm.Contains("、实干时间、"))
                    {
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
                    if (jcxm.Contains("、粘结强度(标准)、") || jcxm.Contains("、粘结强度（标准状态）、") || jcxm.Contains("、粘结强度、")
                        || jcxm.Contains("、拉伸强度(无处理)、") || jcxm.Contains("、拉伸强度、"))
                    {
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
                        else if (Gs > 0 && Gs < 3)
                            sitem["NJQD"] = "重新试验";
                        else
                        {
                            sitem["HG_NJQD"] = "----";
                            sitem["NJQD"] = "----";
                        }
                    }
                    else
                    {
                        sitem["NJQD"] = "----";
                        sitem["HG_NJQD"] = "----";
                        mitem["G_NJQD"] = "----";
                    }


                    if (jcxm.Contains("、粘结强度（浸水后）、") || jcxm.Contains("、粘结强度(浸水)、"))
                    {
                        var jnjqd = Conversion.Val(sitem["JNJQD"]);
                        if (mitem["G_JNJQD"].Trim() == "----")
                        {
                            sitem["JNJQD"] = "----";
                            sitem["HG_JNJQD"] = "----";
                            mitem["G_JNJQD"] = "----";
                        }
                        else
                        {
                            if (jnjqd >= Conversion.Val(MItem[0]["G_JNJQD"]))
                            {
                                sitem["HG_JNJQD"] = "符合";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sitem["HG_JNJQD"] = "不符合";
                                mFlag_Bhg = true;
                                mbhggs = mbhggs + 1;
                            }
                        }
                    }
                    else
                    {
                        sitem["JNJQD"] = "----";
                        sitem["HG_JNJQD"] = "----";
                        mitem["G_JNJQD"] = "----";
                    }

                    if (jcxm.Contains("、耐水性、") || jcxm.Contains("、耐水性（96h）、"))
                    {
                        if ("符合" != sitem["HG_NSX"] && "合格" != sitem["HG_NSX"])
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
                        sitem["NSX"] = "----";
                        sitem["HG_NSX"] = "----";
                        mitem["G_NSX"] = "----";
                    }

                    if (jcxm.Contains("、耐碱性（48h）、") || jcxm.Contains("、耐碱性、") || jcxm.Contains("、耐碱性（24h）、"))
                    {
                        if ("合格" != sitem["HG_NJX"] && sitem["HG_NJX"] != "符合")
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
                        sitem["NJX"] = "----";
                        sitem["HG_NJX"] = "----";
                        mitem["G_NJX"] = "----";
                    }

                    if (jcxm.Contains("、低温稳定性、") || jcxm.Contains("、低温稳定性(3次循环)、"))
                    {
                        //sitem["HG_DWWDX"] = IsQualified(mitem["G_DWWDX"], sitem["DWWDX"]);
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

                    if (jcxm.Contains("、腻子膜柔韧性、"))
                    {
                        if ("合格" != sitem["HG_NZMRRX"] && sitem["HG_NZMRRX"] != "符合")
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
                        sitem["NZMRRX"] = "----";
                        sitem["HG_NZMRRX"] = "----";
                        mitem["G_NZMRRX"] = "----";
                    }

                    if (jcxm.Contains("、初期干燥抗裂性（6h）、") || jcxm.Contains("、初期干燥抗裂性（3h）、"))
                    {
                        //判定
                        var gCJGZLX = GetSafeDouble(sitem["SGHD"]);

                        if (dCpmc == "建筑外墙用腻子")
                        {
                            if (1.5 >= gCJGZLX)
                            {
                                //单道施工厚度《= 1.5mm  1mm无裂缝
                                mitem["G_CHGZKLX"] = "1mm无裂纹";
                            }
                            else
                            {
                                mitem["G_CHGZKLX"] = "2mm无裂纹";
                            }
                        }
                        else
                        {
                            mitem["G_CHGZKLX"] = "无裂纹";
                        }

                        if (sitem["CHGZKLX"] == mitem["G_CHGZKLX"])
                        {
                            sitem["HG_CHGZKLX"] = "符合";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sitem["HG_CHGZKLX"] = "不符合";
                            mFlag_Bhg = true;
                            mbhggs = mbhggs + 1;
                        }
                    }
                    else
                    {
                        sitem["HG_CHGZKLX"] = "----";
                        sitem["SGHD"] = "----";
                        mitem["G_CHGZKLX"] = "----";
                    }

                    if (jcxm.Contains("、打磨性、"))
                    {
                        if ("合格" != mitem["HG_DMX"] && mitem["HG_DMX"] != "符合")
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
                        sitem["W_DMX"] = "----";
                        mitem["HG_DMX"] = "----";
                        mitem["G_DMX"] = "----";
                    }
                    if (jcxm.Contains("、粘结强度（冻融循环5次）、"))
                    {
                        List<double> listQd = new List<double>();
                        for (int i = 1; i < 7; i++)
                        {
                            listQd.Add(GetSafeDouble(sitem["DNJQD" + i]));
                        }
                        listQd.Sort();
                        listQd.RemoveAt(5);
                        listQd.RemoveAt(0);
                        double pjNum = listQd.Average();
                        sitem["PJDNJQD"] = pjNum.ToString();
                        var flag = true;
                        foreach (var item in listQd)
                        {
                            if (Math.Abs(pjNum - item) / pjNum > 0.2)
                            {
                                flag = false;
                                break;
                            }
                        }
                        mitem["HG_DNJQD"] = flag ? "合格" : "不合格";

                        if (mitem["HG_DNJQD"] == "不合格")
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
                        sitem["PJDNJQD"] = "----";
                        mitem["G_DNJQD"] = "----";
                        mitem["HG_DNJQD"] = "----";
                    }

                    if (jcxm.Contains("、动态抗开裂性（基层裂缝）"))
                    {
                        if (dLx == "T型")
                        {
                            sitem["HG_DTKKLX"] = IsQualified(sitem["W_DTKKLX"], mitem["G_DTKKLX"]);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(sitem["W_DTKKLX"]))
                            {
                                sitem["HG_DTKKLX"] = "----";
                            }
                            else
                            {
                                List<string> list = sitem["W_DTKKLX"].Split(',').ToList();
                                if (list.Count == 2)
                                {
                                    sitem["HG_DTKKLX"] = IsQualified(list[0], mitem["G_DTKKLX"]) == IsQualified(list[1], mitem["G_DTKKLX"]) ? "合格" : "不合格";
                                }
                            }
                        }
                        if (sitem["HG_DTKKLX"] == "合格")
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
                        sitem["HG_DTKKLX"] = "----";
                        mitem["G_DTKKLX"] = "----";
                        sitem["W_DTKKLX"] = "----";
                    }

                    if (jcxm.Contains("、吸水量"))
                    {
                        sitem["HG_XSL"] = IsQualified(mitem["G_XSL"], sitem["W_XSL"]);
                        if (sitem["HG_XSL"] == "合格")
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
                        sitem["W_XSL"] = "----";
                        mitem["G_XSL"] = "----";
                        mitem["HG_DNJQD"] = "----";
                    }

                    if (jcxm.Contains("、抗泛盐碱性、"))
                    {
                        //如果Ⅰ型 72h无异常  Ⅱ型48h无异常
                        //if ("合格" != mitem["HG_XSL"] && mitem["HG_XSL"] != "符合")
                        //{
                        //    mbhggs = mbhggs + 1;
                        //    mFlag_Bhg = true;
                        //}
                        //else
                        //{
                        //    mFlag_Hg = true;
                        //}
                    }
                    else
                    {

                    }

                    if (jcxm.Contains("、涂膜外观、"))
                    {
                        //如果Ⅰ型 《=0.3 Ⅱ型《=0.5
                        if ("合格" != sitem["HG_TMWG"] && sitem["HG_TMWG"] != "符合")
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
                        sitem["TMWG"] = "----";
                        sitem["HG_TMWG"] = "----";
                        mitem["G_TMWG"] = "----";
                    }

                    if (jcxm.Contains("、涂层耐温变性、") || jcxm.Contains("、涂层耐温变性（5次循环)、"))
                    {
                        if ("合格" != sitem["HG_NWBX"] && sitem["HG_NWBX"] != "符合")
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
                        sitem["NWBX"] = "----";
                        sitem["HG_NWBX"] = "----";
                        mitem["G_NWBX"] = "----";
                    }

                    if (jcxm.Contains("、耐洗涮性、"))
                    {
                        if ("合格" != sitem["HG_NXSX"] && sitem["HG_NXSX"] != "符合")
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
                        sitem["NXSX"] = "----";
                        sitem["HG_NXSX"] = "----";
                        mitem["G_NXSX"] = "----";
                    }

                    if (jcxm.Contains("、耐沾污性、"))
                    {
                        sitem["HG_NZWX"] = IsQualified(mitem["G_NZWX"], sitem["NZWX"]);
                        if (sitem["HG_NZWX"] == "合格")
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
                        sitem["NZWX"] = "----";
                        sitem["HG_NZWX"] = "----";
                        mitem["G_NZWX"] = "----";
                    }

                    if (jcxm.Contains("、对比率、"))
                    {
                        sitem["HG_DBL"] = IsQualified(mitem["G_DBL"], sitem["DBL"]);
                        if (sitem["HG_DBL"] == "合格")
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
                        sitem["DBL"] = "----";
                        sitem["HG_DBL"] = "----";
                        mitem["G_DBL"] = "----";
                    }

                    if (jcxm.Contains("、低温成膜性、"))
                    {
                        if ("合格" != sitem["HG_DWCMX"] && sitem["HG_DWCMX"] != "符合")
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
                        sitem["DWCMX"] = "----";
                        sitem["HG_DWCMX"] = "----";
                        mitem["G_DWCMX"] = "----";
                    }

                    if (jcxm.Contains("、固体含量、"))
                    {
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
                    if (jcxm.Contains("、断裂伸长率(无处理)、") || jcxm.Contains("、断裂伸长率、") || jcxm.Contains("、断裂延伸率、"))
                    {
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
                        sitem["HG_SCL"] = "----";
                        mitem["G_SCL"] = "----";
                    }
                    if (jcxm.Contains("、不透水性、"))
                    {
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
                    if (jcxm.Contains("、低温柔度、") || jcxm.Contains("、低温柔性、"))
                    {
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
                }

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
                        mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                }
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");

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
                        mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                }
            }
            #endregion
        }
    }
}
