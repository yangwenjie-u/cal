using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SHX : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;
            double[] mkyqdArray = new double[3];
            double zj1, zj2;
            int mbhggs;
            int mFsgs_qfqd, mFsgs_klqd, mFsgs_scl, mFsgs_lw;
            string mSjdjbh, mSjdj;
            double mQfqd, mKlqd, mScl, mLw;
            int vp, mCnt_FjHg, mCnt_FjHg1, mxlgs, mxwgs;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            bool mSFwc;
            bool mFlag_Hg, mFlag_Bhg;
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            #endregion

            #region 自定义函数
            //以下为对用设计值的判定方式
            Func<string, string, string> calc_pd =
                delegate (string sj, string sc)
                {
                    sj = sj.Trim();
                    string calc_pd_ret = "";
                    bool isStart = false;
                    string tmpStr = string.Empty;
                    if (string.IsNullOrEmpty(sc))
                        sc = "";
                    for (int i = 1; i <= sc.Length; i++)
                    {
                        if (IsNumeric(sc.Substring(i - 1, 1)) || sc.Substring(i - 1, 1) == "." || sc.Substring(i - 1, 1) == "-")
                        {
                            isStart = true;
                            tmpStr = tmpStr + sc.Substring(i - 1, 1);
                        }
                        else
                        {
                            if (isStart == false && tmpStr != "")
                                break;
                        }
                    }
                    sc = tmpStr;
                    if (!IsNumeric(sc))
                    {
                        calc_pd_ret = "----";
                    }
                    else
                    {
                        double min_sjz, max_sjz, scz;
                        int length, dw;
                        if (sj.Contains("＞"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("＞") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains(">"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf(">") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("≥"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("≥") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("＜"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("＜") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("<"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("<") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("≤"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("≤") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("="))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("=") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz == max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("～"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("～") + 1;
                            min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= min_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            min_sjz = Conversion.Val(tmpStr);
                            max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= max_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            max_sjz = Conversion.Val(tmpStr);
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("~"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("~") + 1;
                            min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= min_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            min_sjz = Conversion.Val(tmpStr);
                            max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= max_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            max_sjz = Conversion.Val(tmpStr);
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
                        }
                    }
                    return calc_pd_ret;
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_SHX_DJ"];
            var MItem = data["M_SHX"];
            var mitem = MItem[0];
            var SItem = data["S_SHX"];
            #endregion

            #region  计算开始
            mitem["JCJGMS"] = "";
            zj1 = 0;
            zj2 = 0;
            foreach (var sitem in SItem)
            {
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["PZ"].Contains(sitem["PZ"]));
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    mitem["G_XD1"] = mrsDj_Filter["XD1"];
                    mitem["G_XD2"] = mrsDj_Filter["XD2"];
                    mitem["G_CJM"] = mrsDj_Filter["CJM"];
                    mitem["G_YHM"] = mrsDj_Filter["YHMHLSM"];
                    mitem["G_SYHL"] = mrsDj_Filter["YHMHL2"];
                    mitem["G_EYHT"] = mrsDj_Filter["YHMHL1"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                //旋转裂纹、旋转盖板、旋转缩松、旋转抗滑、旋转破坏、旋转力矩、旋转一般项、对接裂纹、对接盖板、对接缩松、对接抗拉、对接力矩、对接一般项
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    int mcd, mdwz;
                    mbhggs = 0;
                    int xd;
                    mFlag_Hg = false;
                    mFlag_Bhg = false;
                    if (jcxm.Contains("、CaO和MgO含量、"))
                    {
                        sitem["PJMGO"] = Round((Conversion.Val(sitem["MGO_1"]) + Conversion.Val(sitem["MGO_2"])) / 2, 2).ToString("0.00");
                        if (IsQualified(mitem["G_YHM"], sitem["PJMGO"]) == "合格")
                        {
                            mitem["HG_MGO"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mitem["HG_MGO"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        if (IsQualified(mitem["G_CJM"], sitem["CJM"]) == "合格")
                        {
                            mitem["HG_CJM"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mitem["HG_CJM"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {

                        mitem["HG_MGO"] = "----";
                        mitem["HG_CJM"] = "----";
                    }
                    if (jcxm.Contains("、细度、"))
                    {
                        if (IsQualified(mitem["G_XD1"], sitem["XD0_90"]) == "合格")
                        {
                            mitem["HG_XD1"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mitem["HG_XD1"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        if (IsQualified(mitem["G_XD2"], sitem["XD0_125"]) == "合格")
                        {
                            mitem["HG_XD2"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mitem["HG_XD2"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {

                        mitem["HG_XD1"] = "----";
                        mitem["HG_XD2"] = "----";
                    }

                    if (jcxm.Contains("、灼烧失量、"))
                    { }
                    else
                        sitem["SSL"] = "----";
                    mitem["JCJGMS"] = "";
                    if (mbhggs == 0)
                    {
                        mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                        mitem["JCJG"] = "合格";
                    }
                    if (mbhggs >= 1)
                    {
                        mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
                        mitem["JCJG"] = "不合格";
                        if (mFlag_Bhg && mFlag_Hg)
                            mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                    }
                    mAllHg = (mAllHg && sitem["JCJG"] == "合格");
                }
                else
                {
                    mbhggs = 0;
                    double md1, md2, md, pjmd, sum, gmhl;
                    int xd, Gs;
                    double[] nArr;
                    bool falg;
                    falg = false;
                    if (jcxm.Contains("、有效氧化钙、") && jcxm.Contains("、氧化镁含量、"))
                    {
                        sum = 0;
                        gmhl = 0;
                        for (xd = 1; xd <= 2; xd++)
                        {
                            md = GetSafeDouble(sitem["TCAO_1"].Trim());
                            md1 = GetSafeDouble(sitem["CAOV1_" + xd].Trim());
                            md2 = GetSafeDouble(sitem["CAOM_" + xd].Trim());
                            md = 1.25 * md * md1 / md2;
                            md = Round(md, 2);
                            sum = md + sum;
                            sitem["CAO_" + xd] = md.ToString("0.00");
                        }
                        gmhl = sum + gmhl;
                        pjmd = sum / 2;
                        pjmd = Round(pjmd, 2);
                        sitem["PJCAO"] = pjmd.ToString("0.00");
                        sum = 0;
                        for (xd = 1; xd <= 2; xd++)
                        {
                            md = GetSafeDouble(sitem["TMGO_1"].Trim());
                            md1 = GetSafeDouble(sitem["MGOV2_" + xd].Trim());
                            md2 = GetSafeDouble(sitem["CAOV1_" + xd].Trim());
                            pjmd = GetSafeDouble(sitem["CAOM_" + xd].Trim());
                            md = 1.25 * md * (md1 - md2) / pjmd;
                            md = Round(md, 2);
                            sum = md + sum;
                            sitem["MGO_" + xd] = md.ToString("0.00");
                        }
                        gmhl = sum + gmhl;
                        pjmd = sum / 2;
                        pjmd = Round(pjmd, 2);
                        sitem["PJMGO"] = pjmd.ToString("0.00");
                        sitem["MGO_1"] = Round((Conversion.Val(sitem["TMGO_1"]) * (Conversion.Val(sitem["MGOV2_1"]) - Conversion.Val(sitem["CAOV1_1"])) * 12.5 / Conversion.Val(sitem["CAOM_1"]) / 1000 * 100), 2).ToString("0.00");
                        sitem["MGO_2"] = Round((Conversion.Val(sitem["TMGO_1"]) * (Conversion.Val(sitem["MGOV2_2"]) - Conversion.Val(sitem["CAOV1_2"])) * 12.5 / Conversion.Val(sitem["CAOM_2"]) / 1000 * 100), 2).ToString("0.00");
                        sitem["PJMGO"] = Round((Conversion.Val(sitem["MGO_1"]) + Conversion.Val(sitem["MGO_2"])) / 2, 2).ToString("0.00");
                        md1 = GetSafeDouble(sitem["PJCAO"]);
                        md2 = GetSafeDouble(sitem["PJMGO"]);
                        sum = gmhl / 2;
                        sum = Round(sum, 2);
                        sitem["CJM"] = sum.ToString("0.00");
                        mitem["HG_MGO"] = "";
                        //foreach (var item in mrsDj)
                        //{
                        //if (IsQualified(item["YHMHLSM"], sitem["PJMGO"]) == "合格")
                        if (IsQualified(mitem["G_YHM"], sitem["PJMGO"]) == "合格")
                        {
                            mitem["HG_MGO"] = "合格";
                            //mitem["G_CJM"] = item["CJM"];
                            //mitem["G_YHM"] = item["YHMHLSM"];
                            //mFlag_Hg = true;
                            //break;
                        }
                        //}
                        if (mitem["HG_MGO"] == "")
                        {
                            mitem["HG_MGO"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        if (IsQualified(mitem["G_CJM"], sitem["CJM"]) == "合格")
                        {
                            mitem["HG_CJM"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mitem["HG_CJM"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        mitem["HG_MGO"] = "----";
                        mitem["HG_CJM "] = "----";
                    }


                    if (jcxm.Contains("、有效氧化钙、") && !jcxm.Contains("、氧化镁含量、"))
                    {
                        falg = true;
                        sum = 0;
                        for (xd = 1; xd <= 2; xd++)
                        {
                            md = GetSafeDouble(sitem["TCAO_1"].Trim());
                            md1 = GetSafeDouble(sitem["CAOV1_" + xd].Trim());
                            md2 = GetSafeDouble(sitem["CAOM_" + xd].Trim());
                            md = 1.25 * md * md1 / md2;
                            md = Round(md, 2);
                            sum = md + sum;
                            sitem["CAO_" + xd] = md.ToString("0.00");
                        }
                        pjmd = sum / 2;
                        pjmd = Round(pjmd, 2);
                        sitem["PJCAO"] = pjmd.ToString("0.00");

                        mitem["G_YHM"] = "----";
                        sitem["PJMGO"] = "----";
                        mitem["HG_MGO"] = "----";
                    }
                    mitem["JCJGMS"] = "";
                    if (falg)
                    {
                        mitem["JCJGMS"] = "该组试样所检项依据" + sitem["PZ"].Trim() + "标准结果如上。";
                        sitem["JCJG"] = "合格";
                    }
                    else
                    {
                        if (mbhggs > 0)
                        {
                            sitem["JCJG"] = "不合格";
                            mitem["JCJGMS"] = "该组试样不符合" + sitem["PZ"].Trim() + "标准要求。";
                            if (mFlag_Bhg && mFlag_Hg)
                                mitem["JCJGMS"] = "该组试样所检项部分符合" + sitem["PZ"].Trim() + "标准要求。";
                        }
                        else
                        {
                            mitem["JCJGMS"] = "该组试样所检项符合" + sitem["PZ"].Trim() + "标准要求。";
                            sitem["JCJG"] = "合格";
                        }
                    }
                }
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}