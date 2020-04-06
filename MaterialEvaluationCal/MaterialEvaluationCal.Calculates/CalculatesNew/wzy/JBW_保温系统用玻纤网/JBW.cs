using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JBW : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;
            double[] mkyqdArray = new double[3];
            double zj1, zj2;
            int mbhggs = 0;
            int mFsgs_qfqd, mFsgs_klqd, mFsgs_scl, mFsgs_lw;
            string mCpmc, mGgxh;
            double mQfqd, mKlqd, mScl, mLw;
            int vp, mCnt_FjHg, mCnt_FjHg1, mxlgs, mxwgs;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            double mDwmjzl;
            string mWhich;
            bool mFlag_Hg, mFlag_Bhg;
            bool mSFwc;
            mSFwc = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_JBW_DJ"];
            var MItem = data["M_JBW"];
            var mitem = MItem[0];
            var SItem = data["S_JBW"];
            //var mrsDrxs = data["ZM_DRJL"];
            #endregion

            #region  计算开始
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            foreach (var sitem in SItem)
            {
                mCpmc = sitem["CPMC"];
                if (string.IsNullOrEmpty(mCpmc))
                    mCpmc = "";
                mGgxh = sitem["GGXH"];
                var mrsDj_item = mrsDj.FirstOrDefault(x => x["BZH"].Contains(sitem["BZH"].Trim()) && x["GGXH"].Contains(mGgxh.Trim()));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    mitem["G_DMJZL"] = mrsDj_item["DMJZL"];
                    mitem["G_LDQLJ"] = mrsDj_item["LDQLJ"];
                    mitem["G_LDQLW"] = mrsDj_item["LDQLW"];
                    mitem["G_LDQLBJ"] = mrsDj_item["LDQLBJ"];
                    mitem["G_LDQLBW"] = mrsDj_item["LDQLBW"];
                    mitem["G_DLQLJ"] = mrsDj_item["DLQLJ"];
                    mitem["G_DLQLW"] = mrsDj_item["DLQLW"];
                    mitem["G_DLYBJ"] = mrsDj_item["DLSCLJ"];
                    mitem["G_DLYBW"] = mrsDj_item["DLSCLW"];
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    continue;
                }
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    mbhggs = 0;
                    int xd;
                    int sum, pj1, pj2, I;
                    sum = 0;
                    if (jcxm.Contains("、耐碱断裂强力、"))
                    {
                        mitem["HG_LDQ"] = "合格";
                        if (Conversion.Val(sitem["LDQLJ"]) >= Conversion.Val(mitem["G_LDQLJ"]))
                            mitem["HG_LDQLJ"] = "合格";
                        else
                        {
                            mitem["HG_LDQLJ"] = "不合格";
                            mitem["HG_LDQ"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mAllHg = false;
                        }
                        if (Conversion.Val(sitem["LDQLW"]) >= Conversion.Val(mitem["G_LDQLW"]))
                            mitem["HG_LDQLW"] = "合格";
                        else
                        {
                            mitem["HG_LDQLW"] = "不合格";
                            mitem["HG_LDQ"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        mitem["HG_LDQ"] = "----";
                        sitem["LDQLJ"] = "----";
                        mitem["HG_LDQLJ"] = "----";
                        sitem["LDQLW"] = "----";
                        mitem["HG_LDQLW"] = "----";
                        mitem["G_LDQLJ"] = "----";
                        mitem["G_LDQLW"] = "----";
                    }
                    if (jcxm.Contains("、耐碱断裂强力保留率、") || jcxm.Contains("、耐碱强力保留率、"))
                    {
                        mitem["HG_LDQLB"] = "合格";
                        if (Conversion.Val(sitem["LDQLBJ"]) >= Conversion.Val(mitem["G_LDQLBJ"]))
                            mitem["HG_LDQLBJ"] = "合格";
                        else
                        {
                            mitem["HG_LDQLBJ"] = "不合格";
                            mitem["HG_LDQLB"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mAllHg = false;
                        }

                        if (Conversion.Val(sitem["LDQLBW"]) >= Conversion.Val(mitem["G_LDQLBW"]))
                            mitem["HG_LDQLBW"] = "合格";
                        else
                        {
                            mitem["HG_LDQLBW"] = "不合格";
                            mitem["HG_LDQLB"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        mitem["HG_LDQLB"] = "----";
                        sitem["LDQLBJ"] = "----";
                        mitem["HG_LDQLBJ"] = "----";
                        sitem["LDQLBW"] = "----";
                        mitem["HG_LDQLBW"] = "----";
                        mitem["G_LDQLBJ"] = "----";
                        mitem["G_LDQLBW"] = "----";
                    }
                    mitem["JCJGMS"] = "";
                    if (mbhggs == 0)
                    {
                        mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                        sitem["JCJG"] = "合格";
                    }
                    if (mbhggs >= 1)
                    {
                        mitem["JCJGMS"] = "该组试样所检项目不符合" + mitem["PDBZ"] + "标准要求。";
                        sitem["JCJG"] = "不合格";
                    }
                    mAllHg = (mAllHg && sitem["JCJG"] == "合格");
                    continue;
                }
                if (jcxm.Contains("、断裂强力、"))
                {
                    if ((Conversion.Val(sitem["CSQLJ1"]) + Conversion.Val(sitem["CSQLJ2"]) + Conversion.Val(sitem["CSQLJ3"]) + Conversion.Val(sitem["CSQLJ4"]) + Conversion.Val(sitem["CSQLJ5"])) != 0)
                    {
                        sitem["DLQLJ"] = Round((Conversion.Val(sitem["CSQLJ1"]) + Conversion.Val(sitem["CSQLJ2"]) + Conversion.Val(sitem["CSQLJ3"]) + Conversion.Val(sitem["CSQLJ4"]) + Conversion.Val(sitem["CSQLJ5"])) / 5, 0).ToString();
                        sitem["DLQLW"] = Round((Conversion.Val(sitem["CSQLW1"]) + Conversion.Val(sitem["CSQLW2"]) + Conversion.Val(sitem["CSQLW3"]) + Conversion.Val(sitem["CSQLW4"]) + Conversion.Val(sitem["CSQLW5"])) / 5, 0).ToString();
                    }
                    if (Conversion.Val(sitem["DLQLJ"]) >= Conversion.Val(mitem["G_DLQLJ"]))
                    {
                        mitem["HG_DLQLJ"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["HG_DLQLJ"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }


                    if (Conversion.Val(sitem["DLQLW"]) >= Conversion.Val(mitem["G_DLQLW"]))
                    {
                        mitem["HG_DLQLW"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["HG_DLQLW"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["DLQLJ"] = "----";
                    mitem["HG_DLQLJ"] = "----";
                    sitem["DLQLW"] = "----";
                    mitem["HG_DLQLW"] = "----";
                    mitem["G_DLQLJ"] = "----";
                    mitem["G_DLQLW"] = "----";
                }
                if (jcxm.Contains("、耐碱断裂强力、"))
                {
                    if ((Conversion.Val(sitem["LDQLJ1"]) + Conversion.Val(sitem["LDQLJ2"]) + Conversion.Val(sitem["LDQLJ3"]) + Conversion.Val(sitem["LDQLJ4"]) + Conversion.Val(sitem["LDQLJ5"])) != 0)
                    {
                        sitem["LDQLJ"] = Round((Conversion.Val(sitem["LDQLJ1"]) + Conversion.Val(sitem["LDQLJ2"]) + Conversion.Val(sitem["LDQLJ3"]) + Conversion.Val(sitem["LDQLJ4"]) + Conversion.Val(sitem["LDQLJ5"])) / 5, 0).ToString();
                        sitem["LDQLW"] = Round((Conversion.Val(sitem["LDQLW1"]) + Conversion.Val(sitem["LDQLW2"]) + Conversion.Val(sitem["LDQLW3"]) + Conversion.Val(sitem["LDQLW4"]) + Conversion.Val(sitem["LDQLW5"])) / 5, 0).ToString();
                    }


                    if (Conversion.Val(sitem["LDQLJ"]) >= Conversion.Val(mitem["G_LDQLJ"]))
                    {
                        mitem["HG_LDQLJ"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["HG_LDQLJ"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }


                    if (Conversion.Val(sitem["LDQLW"]) >= Conversion.Val(mitem["G_LDQLW"]))
                    {
                        mitem["HG_LDQLW"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["HG_LDQLW"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["LDQLJ"] = "----";
                    mitem["HG_LDQLJ"] = "----";
                    sitem["LDQLW"] = "----";
                    mitem["HG_LDQLW"] = "----";
                    mitem["G_LDQLJ"] = "----";
                    mitem["G_LDQLW"] = "----";
                }
                if (jcxm.Contains("、耐碱断裂强力保留率、") || jcxm.Contains("、耐碱强力保留率、"))
                {
                    if ((Conversion.Val(sitem["CSQLJ1"]) * Conversion.Val(sitem["CSQLJ2"]) * Conversion.Val(sitem["CSQLJ3"]) * Conversion.Val(sitem["CSQLJ4"]) * Conversion.Val(sitem["CSQLJ5"])) != 0)
                    {
                        #region old
                        //int sum, pj1, pj2, I;
                        //sum = 0;
                        //for (I = 1; I <= 5; I++)
                        //    sum = sum + int.Parse(sitem["LDQLJ" + I]);
                        //pj1 = (int)Round(sum / 5, 0);
                        //sitem["LDQLJ"] = pj1.ToString();
                        //sum = 0;
                        //for (I = 1; I <= 5; I++)
                        //    sum = sum + int.Parse(sitem["CSQLJ" + I]);
                        //pj2 = (int)Round(sum / 5, 0);
                        //sitem["DLQLJ"] = pj2.ToString();
                        //sitem["LDQLBJ"] = Round(100 * pj1 / pj2, 1).ToString("0.0");
                        //sum = 0;
                        //for (I = 1; I <= 5; I++)
                        //    sum = sum + int.Parse(sitem["LDQLW" + I]);
                        //pj1 = (int)Round(sum / 5, 0);
                        //sitem["LDQLW"] = pj1.ToString();
                        //sum = 0;
                        //for (I = 1; I <= 5; I++)
                        //    sum = sum + int.Parse(sitem["CSQLW" + I]);
                        //pj2 = (int)Round(sum / 5, 0);
                        //sitem["DLQLW"] = pj2.ToString();
                        //sitem["LDQLBW"] = Round(100 * pj1 / pj2, 1).ToString("0.0");
                        #endregion

                        double sum;
                        //径向 断裂强力保留率(%)
                        sum = 0;
                        for (int i = 1; i < 6; i++)
                        {
                            sum = sum + (GetSafeDouble(sitem["LDQLJ" + i]) / GetSafeDouble(sitem["CSQLJ" + i]));
                        }
                        sitem["LDQLBJ"] = Round(sum / 5 * 100, 1).ToString("0.0");


                        //纬向 断裂强力保留率(%)
                        sum = 0;
                        for (int i = 1; i < 6; i++)
                        {
                            sum = sum + (GetSafeDouble(sitem["LDQLW" + i]) / GetSafeDouble(sitem["CSQLW" + i]));
                        }
                        sitem["LDQLBW"] = Round(sum / 5 * 100, 1).ToString("0.0");

                    }


                    if (Conversion.Val(sitem["LDQLBJ"]) >= Conversion.Val(mitem["G_LDQLBJ"]))
                    {
                        mitem["HG_LDQLBJ"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["HG_LDQLBJ"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }


                    if (Conversion.Val(sitem["LDQLBW"]) >= Conversion.Val(mitem["G_LDQLBW"]))
                    {
                        mitem["HG_LDQLBW"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["HG_LDQLBW"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["LDQLBJ"] = "----";
                    mitem["HG_LDQLBJ"] = "----";
                    sitem["LDQLBW"] = "----";
                    mitem["HG_LDQLBW"] = "----";
                    mitem["G_LDQLBJ"] = "----";
                    mitem["G_LDQLBW"] = "----";
                }
                if (jcxm.Contains("、断裂应变、") || jcxm.Contains("、断裂伸长率、"))
                {
                    if (Conversion.Val(sitem["DLYBLJ01"]) == 0 || Conversion.Val(sitem["DLYBLJ02"]) == 0 || Conversion.Val(sitem["DLYBLJ03"]) == 0 || Conversion.Val(sitem["DLYBLJ04"]) == 0 || Conversion.Val(sitem["DLYBLJ05"]) == 0)
                        sitem["DLYBJ"] = "0";
                    else
                        sitem["DLYBJ"] = Round((Round(100 * Conversion.Val(sitem["DLYBLJ11"]) / Conversion.Val(sitem["DLYBLJ01"]), 1) + Round(100 * Conversion.Val(sitem["DLYBLJ12"]) / Conversion.Val(sitem["DLYBLJ02"]), 1) + Round(100 * Conversion.Val(sitem["DLYBLJ13"]) / Conversion.Val(sitem["DLYBLJ03"]), 1) + Round(100 * Conversion.Val(sitem["DLYBLJ14"]) / Conversion.Val(sitem["DLYBLJ04"]), 1) + Round(100 * Conversion.Val(sitem["DLYBLJ15"]) / Conversion.Val(sitem["DLYBLJ05"]), 1)) / 5, 1).ToString();
                    if (Conversion.Val(sitem["DLYBLW01"]) == 0 || Conversion.Val(sitem["DLYBLW02"]) == 0 || Conversion.Val(sitem["DLYBLW03"]) == 0 || Conversion.Val(sitem["DLYBLW04"]) == 0 || Conversion.Val(sitem["DLYBLW05"]) == 0)
                        sitem["DLYBW"] = "0";
                    else
                        sitem["DLYBW"] = Round((Round(100 * Conversion.Val(sitem["DLYBLW11"]) / Conversion.Val(sitem["DLYBLW01"]), 1) + Round(100 * Conversion.Val(sitem["DLYBLW12"]) / Conversion.Val(sitem["DLYBLW02"]), 1) + Round(100 * Conversion.Val(sitem["DLYBLW13"]) / Conversion.Val(sitem["DLYBLW03"]), 1) + Round(100 * Conversion.Val(sitem["DLYBLW14"]) / Conversion.Val(sitem["DLYBLW04"]), 1) + Round(100 * Conversion.Val(sitem["DLYBLW15"]) / Conversion.Val(sitem["DLYBLW05"]), 1)) / 5, 1).ToString();
                    if (Conversion.Val(sitem["DLYBJ"]) <= Conversion.Val(mitem["G_DLYBJ"]))
                    {
                        mitem["HG_DLYBJ"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["HG_DLYBJ"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }
                    if (Conversion.Val(sitem["DLYBW"]) <= Conversion.Val(mitem["G_DLYBW"]))
                    {
                        mitem["HG_DLYBW"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["HG_DLYBW"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }
                    mitem["G_DLYBJ"] = "≤" + mitem["G_DLYBJ"];
                    mitem["G_DLYBW"] = "≤" + mitem["G_DLYBW"];
                }
                else
                {
                    sitem["DLYBJ"] = "----";
                    sitem["DLYBW"] = "----";
                    mitem["HG_DLYBJ"] = "----";
                    mitem["HG_DLYBW"] = "----";
                    mitem["G_DLYBJ"] = "----";
                    mitem["G_DLYBW"] = "----";
                }
                if (jcxm.Contains("、单位面积质量、"))
                {
                    if (Conversion.Val(sitem["MJ1"]) == 0 || Conversion.Val(sitem["MJ2"]) == 0 || Conversion.Val(sitem["MJ3"]) == 0)
                        sitem["DMJZL"] = "0";
                    else
                    {
                        mDwmjzl = Math.Pow(10, 4) * (Conversion.Val(sitem["ZL1"]) / Conversion.Val(sitem["MJ1"]) + Conversion.Val(sitem["ZL2"]) / Conversion.Val(sitem["MJ2"]) + Conversion.Val(sitem["ZL3"]) / Conversion.Val(sitem["MJ3"])) / 3;
                        if (mDwmjzl >= 200)
                            sitem["DMJZL"] = Round(mDwmjzl, 0).ToString();
                        else
                            sitem["DMJZL"] = Round(mDwmjzl, 1).ToString();
                    }
                    if (Conversion.Val(sitem["DMJZL"]) >= Conversion.Val(mitem["G_DMJZL"]))
                    {
                        mitem["HG_DMJZL"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mitem["HG_DMJZL"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["DMJZL"] = "----";
                    mitem["HG_DMJZL"] = "----";
                }
                mitem["JCJGMS"] = "";
                //if (mbhggs == 0)
                //{
                //    mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                //    sitem["JCJG"] = "合格";
                //}
                //if (mbhggs >= 1)
                //{
                //    mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
                //    sitem["JCJG"] = "不合格";
                //    if (mFlag_Bhg && mFlag_Hg)
                //        mitem["JCJGMS"] = "该组试样所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                //}
                //mAllHg = (mAllHg && sitem["JCJG"] == "合格");
            }
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
