using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class XGL : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            #region  参数定义
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            int mbhgs, zSalbs, Salbs = 0;
            bool mSFwc;
            bool mFlag_Hg = false, mFlag_Bhg = false;
            mSFwc = true;
            #endregion

            #region 自定义函数
            Func<string, bool, double> Lsign =
                delegate (string sj, bool flag)
                {
                    double ret_Lsign = 0;
                    sj = sj.Trim();
                    if (!sj.Contains("～"))
                    {
                        ret_Lsign = 0;
                    }
                    else
                    {
                        int length, dw;
                        length = sj.Length;
                        dw = sj.IndexOf("～") + 1;
                        ret_Lsign = flag ? GetSafeDouble(sj.Substring(0, dw - 1)) : GetSafeDouble(sj.Substring(dw));
                    }
                    return ret_Lsign;
                };

            #region 跳转代码
            //Func<IDictionary<string, string>, IDictionary<string, string>, IList<IDictionary<string, string>>, bool, bool> sjtabcalc =
            //    delegate (IDictionary<string, string> Mitem, IDictionary<string, string> sitem, IList<IDictionary<string, string>> mrsDj_Fun, bool MAllHg)
            //    {
            //        zSalbs = 1;
            //        mbhgs = 0;
            //        var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
            //        if (jcxm.Contains("、含泥量、"))
            //        {
            //            sitem["HNLPD"] = "";
            //            foreach (var mrsDj_Filter in mrsDj_Fun)
            //            {
            //                if (IsQualified(mrsDj_Filter["HNL"], sitem["HNL"]) == "符合")
            //                {
            //                    sitem["HNLPD"] = mrsDj_Filter["MC"].Trim().Substring(mrsDj_Filter["MC"].Trim().Length - 2);
            //                    break;
            //                }
            //            }
            //            if (sitem["HNLPD"] == "")
            //            {
            //                sitem["HNLPD"] = "不符合";
            //                mbhgs = mbhgs + 1;
            //                mFlag_Bhg = true;
            //            }
            //            else
            //                mFlag_Hg = true;

            //            switch (sitem["HNLPD"].Trim())
            //            {
            //                case "Ⅰ类":
            //                    zSalbs = zSalbs < 1 ? 1 : zSalbs;
            //                    break;
            //                case "Ⅱ类":
            //                    zSalbs = zSalbs < 2 ? 2 : zSalbs;
            //                    break;
            //                case "Ⅲ类":
            //                    zSalbs = zSalbs < 3 ? 3 : zSalbs;
            //                    break;
            //                default:
            //                    zSalbs = zSalbs < 4 ? 4 : zSalbs;
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            sitem["HNLPD"] = "----";
            //            sitem["HNL"] = "----";
            //        }
            //        if (jcxm.Contains("、泥块含量、"))
            //        {
            //            sitem["NKHLPD"] = "";
            //            foreach (var mrsDj_Filter in mrsDj_Fun)
            //            {
            //                if (IsQualified(mrsDj_Filter["NKHL"], sitem["NKHL"]) == "符合")
            //                {
            //                    sitem["NKHLPD"] = mrsDj_Filter["MC"].Trim().Substring(mrsDj_Filter["MC"].Trim().Length - 2);
            //                    break;
            //                }
            //            }
            //            if (sitem["NKHLPD"] == "")
            //            {
            //                sitem["NKHLPD"] = "不符合";
            //                mbhgs = mbhgs + 1;
            //                mFlag_Bhg = true;
            //            }
            //            else
            //                mFlag_Hg = true;
            //            switch (sitem["NKHLPD"].Trim())
            //            {
            //                case "Ⅰ类":
            //                    zSalbs = zSalbs < 1 ? 1 : zSalbs;
            //                    break;
            //                case "Ⅱ类":
            //                    zSalbs = zSalbs < 2 ? 2 : zSalbs;
            //                    break;
            //                case "Ⅲ类":
            //                    zSalbs = zSalbs < 3 ? 3 : zSalbs;
            //                    break;
            //                default:
            //                    zSalbs = zSalbs < 4 ? 4 : zSalbs;
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            sitem["NKHLPD"] = "----";
            //            sitem["NKHL"] = "----";
            //        }
            //        if (jcxm.Contains("、堆积密度、"))
            //        {
            //            sitem["DJMDPD"] = IsQualified(mrsDj_Fun[0]["DJMD"], sitem["DJMD"], true);
            //            mbhgs = sitem["DJMDPD"] == "不符合" ? mbhgs + 1 : mbhgs;
            //            if (sitem["DJMDPD"] != "不符合")
            //                mFlag_Hg = true;
            //            else
            //                mFlag_Bhg = true;
            //        }
            //        else
            //        {
            //            sitem["DJMDPD"] = "----";
            //            sitem["DJMD"] = "----";
            //        }
            //        if (jcxm.Contains("、紧密密度、"))
            //        {
            //            sitem["JMMDPD"] = IsQualified(mrsDj_Fun[0]["JMMD"], sitem["JMMD"], true);
            //            mbhgs = sitem["JMMDPD"] == "不符合" ? mbhgs + 1 : mbhgs;
            //            if (sitem["JMMDPD"] != "不符合")
            //                mFlag_Hg = true;
            //            else
            //                mFlag_Bhg = true;
            //        }
            //        else
            //        {
            //            sitem["JMMDPD"] = "----";
            //            sitem["JMMD"] = "----";
            //        }
            //        if (jcxm.Contains("、表观密度、"))
            //        {

            //            sitem["BGMDPD"] = IsQualified(mrsDj_Fun[0]["BGMD"], sitem["BGMD"], true);
            //            mbhgs = sitem["BGMDPD"] == "不符合" ? mbhgs + 1 : mbhgs;
            //            if (sitem["BGMDPD"] != "不符合")
            //                mFlag_Hg = true;
            //            else
            //                mFlag_Bhg = true;
            //        }
            //        else
            //        {
            //            sitem["BGMDPD"] = "----";
            //            sitem["BGMD"] = "----";
            //        }
            //        if (jcxm.Contains("、空隙率、"))
            //        {
            //            sitem["KXLPD"] = IsQualified(mrsDj_Fun[0]["KXL"], sitem["KXL"], true);
            //            mbhgs = sitem["KXLPD"] == "不符合" ? mbhgs + 1 : mbhgs;
            //            if (sitem["KXLPD"] != "不符合")
            //                mFlag_Hg = true;
            //            else
            //                mFlag_Bhg = true;
            //        }
            //        else
            //        {
            //            sitem["KXLPD"] = "----";
            //            sitem["KXL"] = "----";
            //        }
            //        if (jcxm.Contains("、氯离子含量、"))
            //        {
            //            sitem["LLZHLPD"] = "";
            //            foreach (var mrsDj_Filter in mrsDj_Fun)
            //            {
            //                if (IsQualified(mrsDj_Filter["LLZHL"], sitem["LLZHL"]) == "符合")
            //                {
            //                    sitem["LLZHLPD"] = mrsDj_Filter["MC"].Trim().Substring(mrsDj_Filter["MC"].Trim().Length - 2);
            //                    break;
            //                }
            //            }
            //            if (sitem["LLZHLPD"] == "")
            //            {
            //                sitem["LLZHLPD"] = "不符合";
            //                mbhgs = mbhgs + 1;
            //                mFlag_Bhg = true;
            //            }
            //            else
            //                mFlag_Hg = true;
            //            switch (sitem["LLZHLPD"].Trim())
            //            {
            //                case "Ⅰ类":
            //                    zSalbs = zSalbs < 1 ? 1 : zSalbs;
            //                    break;
            //                case "Ⅱ类":
            //                    zSalbs = zSalbs < 2 ? 2 : zSalbs;
            //                    break;
            //                case "Ⅲ类":
            //                    zSalbs = zSalbs < 3 ? 3 : zSalbs;
            //                    break;
            //                default:
            //                    zSalbs = zSalbs < 4 ? 4 : zSalbs;
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            sitem["LLZHLPD"] = "----";
            //            sitem["LLZHL"] = "----";
            //        }
            //        if (jcxm.Contains("、吸水率、"))
            //            sitem["XSLPD"] = "----";
            //        else
            //        {
            //            sitem["XSL"] = "----";
            //            sitem["XSLPD"] = "----";
            //        }
            //        if (jcxm.Contains("、云母含量、"))
            //        {
            //            sitem["YMPD"] = "";
            //            foreach (var mrsDj_Filter in mrsDj_Fun)
            //            {
            //                if (IsQualified(mrsDj_Filter["YM"], sitem["YM"]) == "符合")
            //                {
            //                    sitem["YMPD"] = mrsDj_Filter["MC"].Trim().Substring(mrsDj_Filter["MC"].Trim().Length - 2);
            //                    break;
            //                }
            //            }
            //            if (sitem["YMPD"] == "")
            //            {
            //                sitem["YMPD"] = "不符合";
            //                mbhgs = mbhgs + 1;
            //                mFlag_Bhg = true;
            //            }
            //            else
            //                mFlag_Hg = true;

            //            switch (sitem["YMPD"].Trim())
            //            {
            //                case "Ⅰ类":
            //                    zSalbs = zSalbs < 1 ? 1 : zSalbs;
            //                    break;
            //                case "Ⅱ类":
            //                    zSalbs = zSalbs < 2 ? 2 : zSalbs;
            //                    break;
            //                case "Ⅲ类":
            //                    zSalbs = zSalbs < 3 ? 3 : zSalbs;
            //                    break;
            //                default:
            //                    zSalbs = zSalbs < 4 ? 4 : zSalbs;
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            sitem["YMPD"] = "----";
            //            sitem["YM"] = "----";
            //        }
            //        if (jcxm.Contains("、含水率、"))
            //            sitem["HSLPD"] = "----";
            //        else
            //        {
            //            sitem["HSL"] = "----";
            //            sitem["HSLPD"] = "----";
            //        }
            //        if (jcxm.Contains("、有机物含量、"))
            //        {
            //            sitem["YJWHLPD"] = sitem["YJWHL"].Contains("不") ? "不符合" : "符合";
            //            mbhgs = sitem["YJWHLPD"].Contains("不") ? mbhgs + 1 : mbhgs;
            //            if (sitem["YJWHLPD"].Contains("不"))
            //                mFlag_Bhg = true;
            //            else
            //                mFlag_Hg = true;
            //        }
            //        else
            //            sitem["YJWHLPD"] = "----";
            //        if (jcxm.Contains("、碱活性、"))
            //        {
            //            if (IsQualified(mrsDj_Fun[0]["JHX"], sitem["JHX"]) == "符合")
            //            {
            //                sitem["JHXPD"] = "符合";
            //                mFlag_Hg = true;
            //            }
            //            else
            //            {
            //                mbhgs = mbhgs + 1;
            //                mFlag_Bhg = true;
            //            }
            //        }
            //        else
            //        {
            //            sitem["JHXPD"] = "----";
            //            sitem["JHX"] = "----";
            //        }
            //        if (jcxm.Contains("、轻物质含量、"))
            //        {
            //            if (IsQualified(mrsDj_Fun[0]["QWZ"], sitem["QWZ"]) == "符合")
            //            {
            //                sitem["QWZPD"] = "符合";
            //                mFlag_Hg = true;
            //            }
            //            else
            //            {
            //                mbhgs = mbhgs + 1;
            //                mFlag_Bhg = true;
            //            }
            //        }
            //        else
            //        {
            //            sitem["QWZPD"] = "----";
            //            sitem["QWZ"] = "----";
            //        }
            //        if (jcxm.Contains("、硫化物和硫酸盐含量、"))
            //        {
            //            if (IsQualified(mrsDj_Fun[0]["SO3"], sitem["SO3"]) == "符合")
            //            {
            //                sitem["SO3PD"] = "符合";
            //                mFlag_Hg = true;
            //            }
            //            else
            //            {
            //                sitem["SO3PD"] = "不符合";
            //                mbhgs = mbhgs + 1;
            //                mFlag_Bhg = true;
            //            }
            //        }
            //        else
            //        {
            //            sitem["SO3PD"] = "----";
            //            sitem["SO3"] = "----";
            //        }
            //        if (jcxm.Contains("、坚固性、"))
            //        {
            //            double sum = 0;
            //            int xd = 0;
            //            double md1 = 0;
            //            double md2 = 0;
            //            double md = 0;

            //            double[,] narr = new double[3, 5];
            //            for (xd = 1; xd <= 4; xd++)
            //            {
            //                md1 = Conversion.Val(sitem["JGXQG1_" + xd].Trim());
            //                sum = sum + md1;
            //                md2 = Conversion.Val(sitem["JGXHG2_" + xd].Trim());
            //                md = 100 * (md1 - md2) / md1;
            //                md = Round(md, 1);
            //                narr[1, xd] = md;
            //            }
            //            for (xd = 1; xd <= 4; xd++)
            //            {
            //                narr[2, xd] = 100 * narr[1, xd] / sum;
            //                narr[2, xd] = Round(narr[2, xd], 1);
            //            }
            //            sum = 0;
            //            for (xd = 1; xd <= 4; xd++)
            //            {
            //                md = narr[1, xd] * narr[2, xd];
            //                sum = sum + md;
            //            }
            //            md1 = sum;
            //            sum = 0;
            //            for (xd = 1; xd <= 4; xd++)
            //                sum = sum + narr[2, xd];
            //            md2 = sum;
            //            md = md1 / md2;
            //            md = Round(md, 0);

            //            sitem["JGX"] = md.ToString("F0");
            //            sitem["JGXPD"] = "";
            //            foreach (var mrsDj_Filter in mrsDj_Fun)
            //            {
            //                if (IsQualified(mrsDj_Filter["JGX"], sitem["JGX"]) == "符合")
            //                {
            //                    sitem["JGXPD"] = mrsDj_Filter["MC"].Trim().Substring(mrsDj_Filter["MC"].Trim().Length - 2);
            //                    break;
            //                }
            //            }
            //            if (sitem["JGXPD"] == "")
            //            {
            //                sitem["JGXPD"] = "不符合";
            //                mbhgs = mbhgs + 1;
            //                mFlag_Bhg = true;
            //            }
            //            else
            //                mFlag_Hg = true;

            //            switch (sitem["JGXPD"].Trim())
            //            {
            //                case "Ⅰ类":
            //                    zSalbs = zSalbs < 1 ? 1 : zSalbs;
            //                    break;
            //                case "Ⅱ类":
            //                    zSalbs = zSalbs < 2 ? 2 : zSalbs;
            //                    break;
            //                case "Ⅲ类":
            //                    zSalbs = zSalbs < 3 ? 3 : zSalbs;
            //                    break;
            //                default:
            //                    zSalbs = zSalbs < 4 ? 4 : zSalbs;
            //                    break;
            //            }
            //        }
            //        else
            //        {
            //            sitem["JGXPD"] = "----";
            //            sitem["JGX"] = "----";
            //        }
            //        switch (sitem["SJDJ"].Trim())
            //        {
            //            case "Ⅰ类":
            //                Salbs = 1;
            //                break;
            //            case "Ⅱ类":
            //                Salbs = 2;
            //                break;
            //            case "Ⅲ类":
            //                Salbs = 3;
            //                break;
            //        }
            //        sitem["JCJG"] = mbhgs == 0 && zSalbs <= Salbs && MAllHg ? "合格" : "不合格";
            //        MAllHg = (MAllHg && sitem["JCJG"] == "合格");
            //        if (sitem["SJDJ"] != "----")
            //        {
            //            if (MAllHg)
            //            {
            //                Mitem["JCJG"] = "合格";
            //                Mitem["JCJGMS"] = "该组试样所检项目依据" + Mitem["PDBZ"] + "符合" + sitem["SJDJ"] + "砂的标准要求。";
            //            }
            //            else
            //            {
            //                Mitem["JCJG"] = "不合格";
            //                Mitem["JCJGMS"] = "该组试样所检项目依据" + Mitem["PDBZ"] + "不符合" + sitem["SJDJ"] + "砂的标准要求。";
            //                if (mFlag_Bhg && mFlag_Hg)
            //                    Mitem["JCJGMS"] = "该组试样所检项目依据" + Mitem["PDBZ"] + "部分不符合" + sitem["SJDJ"] + "砂的标准要求。";
            //            }
            //        }
            //        else
            //            Mitem["JCJGMS"] = "----";
            //        if (sitem["JPPD"].Contains("不"))
            //        {
            //            Mitem["JCJGMS"] = "该组试样颗粒级配不符合标准要求。";
            //            Mitem["JCJG"] = "不合格";
            //            sitem["JCJG"] = "不合格";
            //        }
            //        return MAllHg;
            //    };
            #endregion
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_XGL_DJ"];
            var mrsHS = dataExtra["BZ_XGLHSB"];
            var MItem = data["M_XGL"];
            var mitem = MItem[0];
            var SItem = data["S_XGL"];
            #endregion

            #region  计算开始
            mSFwc = true;
            mGetBgbh = false;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            mAllHg = true;
            foreach (var sitem in SItem)
            {
                if (string.IsNullOrEmpty(sitem["SACY"]))
                    sitem["SACY"] = "天然砂";
                mbhgs = 0;
                bool flag, sign;
                double md1, md2, md, pjmd, sum;
                double cd1, cd2, kd1, kd2, gd1, gd2;
                double[,] narr;
                int xd, Gs, Ud;
                string bl;
                //抗压强度、抗折强度、冻后强度、质量损失
                zSalbs = 1;
                IDictionary<string, string> mrsDj_Filter = new Dictionary<string, string>();
                if (mitem["JCYJ"].Contains("14684-2011"))
                    mrsDj_Filter = mrsDj.FirstOrDefault(x => x["SACY"].Contains(sitem["SACY"]) && x["MC"].Contains("类") && x["JCYJ"].Contains("14684-2011"));
                else
                    mrsDj_Filter = mrsDj.FirstOrDefault(x => x["SACY"].Contains(sitem["SACY"]) && x["MC"].Contains("类") && x["JCYJ"].Contains("14684-2001"));
                sitem["JPPD"] = "";
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                narr = new double[3, 8];

                #region 级配
                if (jcxm.Contains("、级配、") || jcxm.Contains("、筛分析、"))
                {
                    for (xd = 1; xd <= 6; xd++)
                    {
                        narr[1, xd] = 0;
                        if (IsNumeric(sitem["SYZL" + xd]))
                            narr[1, xd] = Conversion.Val(sitem["SYZL" + xd].Trim());
                        md = 100 * narr[1, xd] / 500;
                        narr[1, xd] = Round(md, 1);


                        narr[2, xd] = 0;
                        if (IsNumeric(sitem["SYZL" + xd + "_2"]))
                            narr[2, xd] = Conversion.Val(sitem["SYZL" + xd + "_2"].Trim());
                        md = 100 * narr[2, xd] / 500;
                        narr[2, xd] = Round(md, 1);
                    }


                    for (xd = 7; xd <= 7; xd++)
                    {
                        narr[1, xd] = 0;
                        if (IsNumeric(sitem["DPZL"]))
                            narr[1, xd] = Conversion.Val(sitem["DPZL"].Trim());
                        md = 100 * narr[1, xd] / 500;
                        md = Round(md, 1);
                        narr[1, xd] = md;
                        narr[2, xd] = 0;
                        if (IsNumeric(sitem["DPZL_2"]))
                            narr[2, xd] = Conversion.Val(sitem["DPZL_2"].Trim());
                        md = 100 * narr[2, xd] / 500;
                        md = Round(md, 1);
                        narr[2, xd] = md;
                    }
                    //计算筛余质量
                    for (xd = 2; xd <= 7; xd++)
                    {
                        narr[1, xd] = narr[1, xd] + narr[1, xd - 1];
                        narr[2, xd] = narr[2, xd] + narr[2, xd - 1];
                    }
                    //计算平均值了
                    for (xd = 1; xd <= 7; xd++)
                    {
                        md1 = narr[1, xd];
                        md2 = narr[2, xd];
                        md = (md1 + md2) / 2;
                        md = Round(md, 0);
                        sitem["LJSYB" + xd + "_pj"] = md.ToString("F0");
                    }
                    IList<IDictionary<string, string>> mrsHS_Where = new List<IDictionary<string, string>>();
                    if (mitem["JCYJ"].Contains("14684-2011"))
                    {
                        mrsHS_Where = mrsHS.Where(x => x["SACY"].Contains(sitem["SACY"]) && x["JCYJ"].Contains("14684-2011")).ToList();
                    }
                    else
                    {
                        mrsHS_Where = mrsHS.Where(x => x["SACY"].Contains(sitem["SACY"]) && x["JCYJ"].Contains("14684-2001")).ToList();
                    }
                    Gs = mrsHS_Where.Count();
                    var mrsHS_Filter = mrsHS_Where[0];
                    for (xd = 1; xd <= Gs; xd++)
                    {
                        flag = true;
                        flag = IsQualified(mrsHS_Filter["MSKCC1"], sitem["LJSYB1_PJ"], true) == "符合" ? flag : false;
                        flag = IsQualified(mrsHS_Filter["MSKCC4"], sitem["LJSYB4_PJ"], true) == "符合" ? flag : false;
                        sum = 0;
                        for (Ud = 1; Ud <= 6; Ud++)
                        {
                            if (Ud != 1 && Ud != 4)
                            {
                                md1 = Lsign(mrsHS_Filter["MSKCC" + Ud], true);
                                md2 = Lsign(mrsHS_Filter["MSKCC" + Ud], false);
                                md = Conversion.Val(sitem["LJSYB" + Ud + "_pj"]);
                                md1 = md1 - md;
                                md2 = md - md2;
                                sum = md1 > 0 ? sum + md1 : sum;
                                sum = md2 > 0 ? sum + md2 : sum;
                            }
                        }
                        flag = sum > 5 ? false : flag;
                        if (flag)
                        {
                            sitem["JPPD"] = mrsHS_Filter["MC"];
                            break;
                        }
                        if (xd >= mrsHS_Where.Count())
                            mrsHS_Filter = mrsHS_Where[xd - 1];
                        else
                            mrsHS_Filter = mrsHS_Where[xd];
                    }
                    sitem["JPPD"] = sitem["JPPD"] == "" ? "不符合级配区" : sitem["JPPD"];


                    if (!mitem["JCYJ"].Contains("14684-2011"))
                    {
                        md = 100 - Conversion.Val(sitem["LJSYB7_PJ"].Trim());
                        sitem["JPPD"] = md > 1 ? "累计底盘筛余量低于试样总量的99%,试验需重做。" : sitem["JPPD"];
                        md = Conversion.Val(sitem["LJSYB7_PJ"].Trim()) - 100;
                        sitem["JPPD"] = md > 0 ? "累计底盘筛余量大于试样总量,试验需重做。" : sitem["JPPD"];
                    }



                    if (mitem["JCYJ"].Contains("14684-2011"))
                    {
                        if (sitem["SJDJ"].Trim() == "Ⅰ类")
                        {
                            if (sitem["JPPD"] != "2区" && sitem["JPPD"] != "不符合级配区")
                            {
                                mbhgs = mbhgs + 1;
                                sitem["JPPD"] = "在" + sitem["JPPD"] + "不符合Ⅰ类砂";
                                mFlag_Bhg = true;
                            }
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                        mFlag_Hg = true;

                }
                else
                {
                    sitem["JPPD"] = "----";
                    sitem["DPZL"] = "----";
                    sitem["DPZL_2"] = "----";
                    for (xd = 1; xd <= 6; xd++)
                    {
                        sitem["SYZL" + xd] = "----";
                        sitem["SYZL" + xd + "_2"] = "----";
                        sitem["LJSYB" + xd + "_pj"] = "----";
                    }
                }
                #endregion

                #region 细度模数
                if (jcxm.Contains("、细度模数、"))
                {
                    md1 = 0;
                    md2 = 0;
                    for (xd = 2; xd <= 6; xd++)
                    {
                        md1 = md1 + narr[1, xd] - narr[1, 1];
                        md2 = md2 + narr[2, xd] - narr[2, 1];
                    }
                    md1 = md1 / (100 - narr[1, 1]);
                    md1 = Round(md1, 2);
                    md2 = md2 / (100 - narr[2, 1]);
                    md2 = Round(md2, 2);
                    md = (md1 + md2) / 2;
                    md = Round(md, 1);
                    sitem["XDMS"] = md.ToString("F1");
                    if (md >= 3.1 && md <= 3.7)
                    {
                        sitem["XDMSPD"] = "粗砂";
                        mFlag_Hg = true;
                    }
                    else if (md >= 2.3 && md <= 3)
                    {
                        sitem["XDMSPD"] = "中砂";
                        mFlag_Hg = true;
                    }
                    else if (md >= 1.6 && md <= 2.2)
                    {
                        sitem["XDMSPD"] = "细砂";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["XDMSPD"] = "不符合";
                        mFlag_Bhg = true;
                        mbhgs = mbhgs + 1;
                    }
                    md = Math.Abs(md1 - md2);
                    sitem["XDMSPD"] = md > 0.2 ? "细度模数两试验数据差值大于0.2试验需重做" : sitem["XDMSPD"];
                    if (md > 0.2)
                        sitem["JPPD"] = sitem["JPPD"] + sitem["XDMSPD"];
                    mbhgs = md > 0.2 ? mbhgs + 1 : mbhgs;
                    if (md <= 0.2)
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sitem["XDMSPD"] = "----";
                    sitem["XDMS"] = "----";
                }
                #endregion

                #region 含泥量
                if (jcxm.Contains("、含泥量、"))
                {
                    cd1 = Conversion.Val(sitem["HNLG0"].Trim());
                    cd2 = Conversion.Val(sitem["HNLG1"].Trim());
                    md1 = 100 * (cd1 - cd2) / cd1;
                    md1 = Round(double.IsNaN(md1) ? 0 : md1, 1);
                    sitem["HNL1"] = md1.ToString();
                    cd1 = Conversion.Val(sitem["HNLG0_2"].Trim());
                    cd2 = Conversion.Val(sitem["HNLG1_2"].Trim());
                    md2 = 100 * (cd1 - cd2) / cd1;
                    md2 = Round(double.IsNaN(md2) ? 0 : md2, 1);
                    sitem["HNL2"] = md2.ToString();
                    md = (md1 + md2) / 2;
                    md = Round(md, 1);
                    sitem["HNL"] = md.ToString();
                    sitem["HNLPD"] = "";
                    foreach (var mrsDj_temp in mrsDj)
                    {
                        if (IsQualified(mrsDj_temp["HNL"], sitem["HNL"]) == "符合")
                        {
                            sitem["HNLPD"] = mrsDj_temp["MC"].Trim().Substring(mrsDj_temp["MC"].Trim().Length - 2);
                            break;
                        }
                    }
                    if (sitem["HNLPD"] == "")
                    {
                        sitem["HNLPD"] = "不符合";
                        mbhgs = mbhgs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;

                    switch (sitem["HNLPD"].Trim())
                    {
                        case "Ⅰ类":
                            zSalbs = zSalbs < 1 ? 1 : zSalbs;
                            break;
                        case "Ⅱ类":
                            zSalbs = zSalbs < 2 ? 2 : zSalbs;
                            break;
                        case "Ⅲ类":
                            zSalbs = zSalbs < 3 ? 3 : zSalbs;
                            break;
                        default:
                            zSalbs = zSalbs < 4 ? 4 : zSalbs;
                            break;
                    }
                }
                else
                {
                    sitem["HNLPD"] = "----";
                    sitem["HNL"] = "----";
                }
                #endregion

                #region 泥块含量
                if (jcxm.Contains("、泥块含量、"))
                {
                    cd1 = Conversion.Val(sitem["NKHLG1"].Trim());
                    cd2 = Conversion.Val(sitem["NKHLG2"].Trim());
                    md1 = 100 * (cd1 - cd2) / cd1;
                    md1 = Round(double.IsNaN(md1) ? 0 : md1, 1);
                    sitem["NKHL1"] = md1.ToString();
                    cd1 = Conversion.Val(sitem["NKHLG1_2"].Trim());
                    cd2 = Conversion.Val(sitem["NKHLG2_2"].Trim());
                    md2 = 100 * (cd1 - cd2) / cd1;
                    md2 = Round(double.IsNaN(md2) ? 0 : md2, 1);
                    sitem["NKHL2"] = md2.ToString();
                    md = (md1 + md2) / 2;
                    md = Round(md, 1);
                    sitem["NKHL"] = md.ToString("0.0");
                    sitem["NKHLPD"] = "";
                    foreach (var mrsDj_temp in mrsDj)
                    {
                        if (IsQualified(mrsDj_temp["NKHL"], sitem["NKHL"]) == "符合")
                        {
                            sitem["NKHLPD"] = mrsDj_temp["MC"].Trim().Substring(mrsDj_temp["MC"].Trim().Length - 2);
                            break;
                        }
                    }
                    if (sitem["NKHLPD"] == "")
                    {
                        sitem["NKHLPD"] = "不符合";
                        mbhgs = mbhgs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;

                    switch (sitem["NKHLPD"].Trim())
                    {
                        case "Ⅰ类":
                            zSalbs = zSalbs < 1 ? 1 : zSalbs;
                            break;
                        case "Ⅱ类":
                            zSalbs = zSalbs < 2 ? 2 : zSalbs;
                            break;
                        case "Ⅲ类":
                            zSalbs = zSalbs < 3 ? 3 : zSalbs;
                            break;
                        default:
                            zSalbs = zSalbs < 4 ? 4 : zSalbs;
                            break;
                    }
                }
                else
                {
                    sitem["NKHLPD"] = "----";
                    sitem["NKHL"] = "----";
                }
                #endregion

                #region 堆积密度
                if (jcxm.Contains("、堆积密度、"))
                {
                    cd1 = Conversion.Val(sitem["DJMDG1"].Trim());
                    cd2 = Conversion.Val(sitem["DJMDG2"].Trim());
                    md1 = Conversion.Val(sitem["DJMDV"].Trim());
                    md1 = (cd1 - cd2) / (10 * md1);
                    md1 = Round(md1, 0) * 10;
                    sitem["KXLP1"] = md1.ToString();


                    cd1 = Conversion.Val(sitem["DJMDG1_2"].Trim());
                    cd2 = Conversion.Val(sitem["DJMDG2_2"].Trim());
                    md2 = Conversion.Val(sitem["DJMDV"].Trim());
                    md2 = (cd1 - cd2) / (10 * md2);
                    md2 = Round(md2, 0) * 10;
                    sitem["KXLP1_2"] = md2.ToString();
                    md = (md1 + md2) / 20;
                    md = Round(md, 0) * 10;
                    sitem["DJMD"] = md.ToString();
                    sitem["DJMDPD"] = IsQualified(mrsDj[0]["DJMD"], sitem["DJMD"], true);
                    mbhgs = sitem["DJMDPD"] == "不符合" ? mbhgs + 1 : mbhgs;
                    if (sitem["DJMDPD"] != "不符合")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sitem["DJMDPD"] = "----";
                    sitem["DJMD"] = "----";
                }
                #endregion

                #region 紧密密度
                if (jcxm.Contains("、紧密密度、"))
                {
                    cd1 = Conversion.Val(sitem["JMMDG1"].Trim());
                    cd2 = Conversion.Val(sitem["JMMDG2"].Trim());
                    md1 = Conversion.Val(sitem["JMMDV"].Trim());
                    md1 = (cd1 - cd2) / (10 * md1);
                    md1 = Round(md1, 0);
                    md1 = 10 * md1;
                    sitem["JMMD1"] = md1.ToString();
                    cd1 = Conversion.Val(sitem["JMMDG1_2"].Trim());
                    cd2 = Conversion.Val(sitem["JMMDG2_2"].Trim());
                    md2 = Conversion.Val(sitem["JMMDV"].Trim());
                    md2 = (cd1 - cd2) / (10 * md2);
                    md2 = Round(md2, 0);
                    md2 = 10 * md2;
                    sitem["JMMD2"] = md2.ToString();
                    md = (md1 + md2) / 20;
                    md = Round(md, 0) * 10;
                    sitem["JMMD"] = md.ToString();
                    sitem["JMMDPD"] = IsQualified(mrsDj[0]["JMMD"], sitem["JMMD"], true);
                    mbhgs = sitem["JMMDPD"] == "不符合" ? mbhgs + 1 : mbhgs;
                    if (sitem["JMMDPD"] != "不符合")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sitem["JMMDPD"] = "----";
                    sitem["JMMD"] = "----";
                }
                #endregion

                #region 表观密度
                if (jcxm.Contains("、表观密度、"))
                {
                    cd1 = Conversion.Val(sitem["BGMDG0"].Trim());
                    cd2 = Conversion.Val(sitem["BGMDG2"].Trim());
                    md1 = Conversion.Val(sitem["BGMDG1"].Trim());
                    md1 = 100 * (cd1 / (cd1 + cd2 - md1)- GetSafeDouble(sitem["SWXZXS1"].Trim()));
                    md1 = Round(md1, 0);
                    md1 = md1 * 10;
                    sitem["KXLP2"] = md1.ToString();
                    cd1 = Conversion.Val(sitem["BGMDG0_2"].Trim());
                    cd2 = Conversion.Val(sitem["BGMDG2_2"].Trim());
                    md2 = Conversion.Val(sitem["BGMDG1_2"].Trim());
                    md2 = 100 * (cd1 / (cd1 + cd2 - md2) - GetSafeDouble(sitem["SWXZXS2"].Trim()));
                    md2 = Round(md2, 0);
                    md2 = md2 * 10;
                    sitem["KXLP2_2"] = md2.ToString();
                    md = (md1 + md2) / 20;
                    md = 10 * Round(md, 0);

                    sitem["BGMD"] = md.ToString();
                    sitem["BGMDPD"] = IsQualified(mrsDj[0]["BGMD"], sitem["BGMD"], true);
                    mbhgs = sitem["BGMDPD"] == "不符合" ? mbhgs + 1 : mbhgs;
                    md = Math.Abs(md1 - md2);
                    sitem["BGMDPD"] = md > 20 ? "两次结果差大于20，须重新试验" : sitem["BGMDPD"];
                    mbhgs = md > 20 ? mbhgs + 1 : mbhgs;
                    if (md <= 20)
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sitem["BGMDPD"] = "----";
                    sitem["BGMD"] = "----";
                }
                #endregion

                #region 空隙率
                if (jcxm.Contains("、空隙率、"))
                {
                    cd1 = Conversion.Val(sitem["KXLP1"].Trim());
                    cd2 = Conversion.Val(sitem["KXLP2"].Trim());
                    md1 = 100 * (1 - cd1 / cd2);
                    md1 = Round(md1, 0);
                    sitem["KXL1"] = md1.ToString();
                    cd1 = Conversion.Val(sitem["KXLP1_2"].Trim());
                    cd2 = Conversion.Val(sitem["KXLP2_2"].Trim());
                    md2 = 100 * (1 - cd1 / cd2);
                    md2 = Round(md2, 0);
                    sitem["KXL2"] = md2.ToString();
                    md = (md1 + md2) / 2;
                    md = Round(md, 0);
                    sitem["KXL"] = md.ToString();
                    sitem["KXLPD"] = IsQualified(mrsDj[0]["KXL"], sitem["KXL"], true);
                    mbhgs = sitem["KXLPD"] == "不符合" ? mbhgs + 1 : mbhgs;
                    if (sitem["KXLPD"] != "不符合")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sitem["KXLPD"] = "----";
                    sitem["KXL"] = "----";
                }
                #endregion

                #region 氯离子含量
                if (jcxm.Contains("、氯离子含量、") || jcxm.Contains("、氯化物含量、"))
                {
                    cd1 = Conversion.Val(sitem["LLZV"].Trim());
                    cd2 = Conversion.Val(sitem["LLZV0"].Trim());
                    md1 = 35.5 * (cd1 - cd2) * cd1 / 500;
                    md1 = Round(md1, 2);


                    cd1 = Conversion.Val(sitem["LLZV_2"].Trim());
                    cd2 = Conversion.Val(sitem["LLZV0_2"].Trim());
                    md2 = 35.5 * (cd1 - cd2) * cd1 / 500;
                    md2 = Round(md2, 2);
                    md = (md1 + md2) / 2;
                    md = Round(md, 2);
                    sitem["LLZHL"] = md.ToString("0.00");
                    sitem["LLZHLPD"] = "";
                    foreach (var mrsDj_temp in mrsDj)
                    {
                        if (IsQualified(mrsDj_temp["LLZHL"], sitem["LLZHL"]) == "符合")
                        {
                            sitem["LLZHLPD"] = mrsDj_temp["MC"].Trim().Substring(mrsDj_temp["MC"].Trim().Length - 2);
                            break;
                        }
                    }
                    if (sitem["LLZHLPD"] == "")
                    {
                        sitem["LLZHLPD"] = "不符合";
                        mbhgs = mbhgs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;

                    switch (sitem["LLZHLPD"].Trim())
                    {
                        case "Ⅰ类":
                            zSalbs = zSalbs < 1 ? 1 : zSalbs;
                            break;
                        case "Ⅱ类":
                            zSalbs = zSalbs < 2 ? 2 : zSalbs;
                            break;
                        case "Ⅲ类":
                            zSalbs = zSalbs < 3 ? 3 : zSalbs;
                            break;
                        default:
                            zSalbs = zSalbs < 4 ? 4 : zSalbs;
                            break;
                    }
                }
                else
                {
                    sitem["LLZHLPD"] = "----";
                    sitem["LLZHL"] = "----";
                }
                #endregion

                #region 吸水率
                if (jcxm.Contains("、吸水率、"))
                {
                    cd1 = Conversion.Val(sitem["XSLG1"].Trim());
                    cd2 = Conversion.Val(sitem["XSLG2"].Trim());
                    md1 = 100 * (500 - (cd2 - cd1)) / (cd2 - cd1);
                    md1 = Round(md1, 1);
                    sitem["XSL1"] = md1.ToString();
                    cd1 = Conversion.Val(sitem["XSLG1_2"].Trim());
                    cd2 = Conversion.Val(sitem["XSLG2_2"].Trim());
                    md2 = 100 * (500 - (cd2 - cd1)) / (cd2 - cd1);
                    md2 = Round(md2, 1);
                    sitem["XSL2"] = md2.ToString();
                    md = (md1 + md2) / 2;
                    md = Round(md, 1);
                    sitem["XSL"] = md.ToString("F1");
                    sitem["XSLPD"] = (Math.Abs(md1 - md2)) > 0.2 ? "两次结果差大于0.2，须重新试验" : "----";
                }
                else
                {
                    sitem["XSL"] = "----";
                    sitem["XSLPD"] = "----";
                }
                #endregion

                #region 云母含量
                if (jcxm.Contains("、云母含量、"))
                {
                    cd1 = Conversion.Val(sitem["YMG1"]);
                    cd2 = Conversion.Val(sitem["YMG2"].Trim());
                    md1 = 100 * cd2 / cd1;
                    md1 = Round(md1, 1);
                    cd1 = Conversion.Val(sitem["YMG1_2"].Trim());
                    cd2 = Conversion.Val(sitem["YMG2_2"].Trim());
                    md2 = 100 * cd2 / cd1;
                    md2 = Round(md2, 1);
                    md = (md1 + md2) / 2;
                    md = Round(md, 1);
                    sitem["YM"] = md.ToString("F1");
                    sitem["YMPD"] = "";
                    foreach (var mrsDj_temp in mrsDj)
                    {
                        if (IsQualified(mrsDj_temp["YM"], sitem["YM"]) == "符合")
                        {
                            sitem["YMPD"] = mrsDj_temp["MC"].Trim().Substring(mrsDj_temp["MC"].Trim().Length - 2);
                            break;
                        }
                    }
                    if (sitem["YMPD"] == "")
                    {
                        sitem["YMPD"] = "不符合";
                        mbhgs = mbhgs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;

                    switch (sitem["YMPD"].Trim())
                    {
                        case "Ⅰ类":
                            zSalbs = zSalbs < 1 ? 1 : zSalbs;
                            break;
                        case "Ⅱ类":
                            zSalbs = zSalbs < 2 ? 2 : zSalbs;
                            break;
                        case "Ⅲ类":
                            zSalbs = zSalbs < 3 ? 3 : zSalbs;
                            break;
                        default:
                            zSalbs = zSalbs < 4 ? 4 : zSalbs;
                            break;
                    }
                    sitem["YMPD"] = Math.Abs(md1 - md2) > 0.2 ? "两次结果差大于0.2，须重新试验" : sitem["YMPD"];
                }
                else
                {
                    sitem["YMPD"] = "----";
                    sitem["YM"] = "----";
                }
                #endregion

                #region 含水率
                if (jcxm.Contains("、含水率、"))
                {
                    cd1 = Conversion.Val(sitem["HSLG1"].Trim());
                    cd2 = Conversion.Val(sitem["HSLG2"].Trim());
                    md1 = 100 * (cd2 - cd1) / cd1;
                    md1 = Round(md1, 1);
                    sitem["HSL1"] = md1.ToString();
                    cd1 = Conversion.Val(sitem["HSLG1_2"].Trim());
                    cd2 = Conversion.Val(sitem["HSLG2_2"].Trim());
                    md2 = 100 * (cd2 - cd1) / cd1;
                    md2 = Round(md2, 1);
                    sitem["HSL2"] = md2.ToString();
                    md = (md1 + md2) / 2;
                    md = Round(md, 1);

                    sitem["HSL"] = md.ToString("F1");
                    sitem["HSLPD"] = Math.Abs(md1 - md2) > 20 ? "两次结果差大于0.2，须重新试验" : sitem["HSLPD"];
                }
                else
                {
                    sitem["HSL"] = "----";
                    sitem["HSLPD"] = "----";
                }
                #endregion

                #region 有机物含量
                if (jcxm.Contains("、有机物含量、"))
                {
                    sitem["YJWHLPD"] = sitem["YJWHLPD"].Contains("不") ? "不符合" : "符合";
                    mbhgs = sitem["YJWHLPD"].Contains("不") ? mbhgs + 1 : mbhgs;
                    if (sitem["YJWHLPD"].Contains("不"))
                        mFlag_Bhg = true;
                    else
                        mFlag_Hg = true;
                }
                else
                    sitem["YJWHLPD"] = "----";
                #endregion

                #region 碱活性
                if (jcxm.Contains("、碱活性、"))
                {
                    md = Conversion.Val(sitem["JHX"].Trim());
                    if (IsQualified(mrsDj[0]["JHX"], sitem["JHX"]) == "符合")
                    {
                        sitem["JHXPD"] = "符合";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhgs = mbhgs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sitem["JHXPD"] = "----";
                    sitem["JHX"] = "----";
                }
                #endregion

                #region 轻物质含量
                if (jcxm.Contains("、轻物质含量、"))
                {
                    cd1 = Conversion.Val(sitem["QWZG1_1"].Trim());
                    cd2 = Conversion.Val(sitem["QWZG2_1"].Trim());
                    md1 = Conversion.Val(sitem["QWZG3_1"].Trim());
                    md1 = 100 * (cd2 - md1) / cd1;
                    md1 = Round(md1, 1);
                    cd1 = Conversion.Val(sitem["QWZG1_2"].Trim());
                    cd2 = Conversion.Val(sitem["QWZG2_2"].Trim());
                    md2 = Conversion.Val(sitem["QWZG3_2"].Trim());
                    md2 = 100 * (cd2 - md2) / cd1;
                    md2 = Round(md2, 1);
                    md = (md1 + md2) / 2;
                    md = Round(md, 1);
                    sitem["QWZ"] = md.ToString("0.0");
                    if (IsQualified(mrsDj[0]["QWZ"], sitem["QWZ"]) == "符合")
                    {
                        sitem["QWZPD"] = "符合";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhgs = mbhgs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sitem["QWZPD"] = "----";
                    sitem["QWZ"] = "----";
                }
                #endregion

                #region 硫化物和硫酸盐含量
                if (jcxm.Contains("、硫化物和硫酸盐含量、"))
                {
                    cd1 = Conversion.Val(sitem["SO3G1_1"].Trim());
                    cd2 = Conversion.Val(sitem["SO3G2_1"].Trim());
                    md1 = 100 * 0.343 * cd2 / cd1;
                    cd1 = Conversion.Val(sitem["SO3G1_2"].Trim());
                    cd2 = Conversion.Val(sitem["SO3G2_2"].Trim());
                    md2 = 100 * 0.343 * cd2 / cd1;
                    sum = md1 + md2;
                    pjmd = sum / 2;
                    pjmd = Round(pjmd, 2);
                    sitem["SO3"] = pjmd.ToString("0.00");
                    if (IsQualified(mrsDj[0]["SO3"], sitem["SO3"]) == "符合")
                    {
                        sitem["SO3PD"] = "符合";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhgs = mbhgs + 1;
                        mFlag_Bhg = true;
                    }

                    sitem["SO3PD"] = Math.Abs(md1 - md2) > 0.15 ? "两次结果差大于0.15%，须重新试验" : sitem["SO3PD"];
                }
                else
                {
                    sitem["SO3PD"] = "----";
                    sitem["SO3"] = "----";
                }
                #endregion

                #region 坚固性
                if (jcxm.Contains("、坚固性、"))
                {
                    sum = 0;
                    narr = new double[3, 5];
                    for (xd = 1; xd <= 4; xd++)
                    {
                        md1 = Conversion.Val(sitem["JGXQG1_" + xd].Trim());
                        sum = sum + md1;
                        md2 = Conversion.Val(sitem["JGXHG2_" + xd].Trim());
                        md = 100 * (md1 - md2) / md1;
                        md = Round(md, 1);
                        narr[1, xd] = md;
                    }
                    for (xd = 1; xd <= 4; xd++)
                    {
                        narr[2, xd] = 100 * narr[1, xd] / sum;
                        narr[2, xd] = Round(narr[2, xd], 1);
                    }
                    sum = 0;
                    for (xd = 1; xd <= 4; xd++)
                    {
                        md = narr[1, xd] * narr[2, xd];
                        sum = sum + md;
                    }
                    md1 = sum;
                    sum = 0;
                    for (xd = 1; xd <= 4; xd++)
                        sum = sum + narr[2, xd];
                    md2 = sum;
                    md = md1 / md2;
                    md = Round(md, 0);
                    sitem["JGX"] = md.ToString();
                    sitem["JGXPD"] = "";
                    foreach (var mrsDj_temp in mrsDj)
                    {
                        if (IsQualified(mrsDj_temp["JGX"], sitem["JGX"]) == "符合")
                        {
                            sitem["JGXPD"] = mrsDj_temp["MC"].Trim().Substring(mrsDj_temp["MC"].Trim().Length - 2);
                            break;
                        }
                    }
                    if (sitem["JGXPD"] == "")
                    {
                        sitem["JGXPD"] = "不符合";
                        mbhgs = mbhgs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                        mFlag_Hg = true;

                    switch (sitem["JGXPD"].Trim())
                    {
                        case "Ⅰ类":
                            zSalbs = zSalbs < 1 ? 1 : zSalbs;
                            break;
                        case "Ⅱ类":
                            zSalbs = zSalbs < 2 ? 2 : zSalbs;
                            break;
                        case "Ⅲ类":
                            zSalbs = zSalbs < 3 ? 3 : zSalbs;
                            break;
                        default:
                            zSalbs = zSalbs < 4 ? 4 : zSalbs;
                            break;
                    }
                }
                else
                {
                    sitem["JGXPD"] = "----";
                    sitem["JGX"] = "----";
                }
                #endregion

                switch (sitem["SJDJ"].Trim())
                {
                    case "Ⅰ类":
                        Salbs = 1;
                        break;
                    case "Ⅱ类":
                        Salbs = 2;
                        break;
                    case "Ⅲ类":
                        Salbs = 3;
                        break;
                }
                sitem["JCJG"] = mbhgs == 0 && zSalbs <= Salbs ? "合格" : "不合格";
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");
                if (sitem["SJDJ"] != "----")
                {
                    if (mAllHg)
                    {
                        mitem["JCJG"] = "合格";
                        mitem["JCJGMS"] = "该组试样所检项目依据" + mitem["PDBZ"] + "符合" + sitem["SJDJ"] + "砂的标准要求。";
                    }
                    else
                    {
                        mitem["JCJG"] = "不合格";
                        mitem["JCJGMS"] = "该组试样所检项目依据" + mitem["PDBZ"] + "不符合" + sitem["SJDJ"] + "砂的标准要求。";
                        if (mFlag_Bhg && mFlag_Hg)
                            mitem["JCJGMS"] = "该组试样所检项目依据" + mitem["PDBZ"] + "部分不符合" + sitem["SJDJ"] + "砂的标准要求。";
                    }
                }
                else
                    mitem["JCJGMS"] = "----";
                if (sitem["JPPD"].Contains("不"))
                {
                    //mitem["JCJGMS"] = "该组试样颗粒级配不符合标准要求。";
                    mitem["JCJG"] = "不合格";
                    sitem["JCJG"] = "不合格";
                }
            }
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
