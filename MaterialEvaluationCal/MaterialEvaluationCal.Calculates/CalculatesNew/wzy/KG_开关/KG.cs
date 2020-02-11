using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class KG : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;
            double[] mkyqdArray;
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
            #endregion

            #region 自定义函数
            Func<IDictionary<string, string>, IDictionary<string, string>, bool, bool> sjtabcalc =
                delegate (IDictionary<string, string> mitem, IDictionary<string, string> sitem, bool mAllHg_fun)
                {
                    mbhggs = 0;
                    int xd;
                    bool ret_sjtabcalc = true;
                    bool mFlag_Hg_fun, mFlag_Bhg_fun;
                    mFlag_Hg_fun = false;
                    mFlag_Bhg_fun = false;

                    var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                    if (jcxm.Contains("、正常操作1、") || jcxm.Contains("、开关正常操作次数、"))
                    {
                        if (GetSafeDouble(mitem["ZCCZ1"]) >= 0 && GetSafeDouble(mitem["ZCCZ1"]) < 3)
                        {
                            mitem["ZCCZ1_HG"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg_fun = true;
                        }
                        else
                        {
                            mitem["ZCCZ1_HG"] = "合格";
                            mFlag_Hg_fun = true;
                        }
                        if (GetSafeDouble(mitem["ZCCZ1"]) == -1)
                            mitem["ZCCZ1_HG"] = "----";
                    }
                    else
                    {
                        mitem["ZCCZ1"] = "-1";
                        mitem["ZCCZ1_HG"] = "----";
                    }
                    if (jcxm.Contains("、正常操作2、") || jcxm.Contains("、开关正常操作次数、"))
                    {
                        if (GetSafeDouble(mitem["ZCCZ2"]) >= 0 && GetSafeDouble(mitem["ZCCZ2"]) < 3)
                        {
                            mitem["ZCCZ2_HG"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg_fun = true;
                        }
                        else
                        {
                            mitem["ZCCZ2_HG"] = "合格";
                            mFlag_Hg_fun = true;
                        }
                        if (GetSafeDouble(mitem["ZCCZ2"]) == -1)
                            mitem["ZCCZ2_HG"] = "----";
                    }
                    else
                    {
                        mitem["ZCCZ2"] = "-1";
                        mitem["ZCCZ2_HG"] = "----";
                    }
                    if (jcxm.Contains("、正常操作3、") || jcxm.Contains("、开关正常操作次数、"))
                    {
                        if (IsNumeric(mitem["ZCCZ3"]) && !string.IsNullOrEmpty(mitem["ZCCZ3"]))
                        {
                            if (Conversion.Val(mitem["ZCCZ3"]) >= 0 && Conversion.Val(mitem["ZCCZ3"]) < 3)
                            {
                                mitem["ZCCZ3_HG"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg_fun = true;
                            }
                            else
                            {
                                mitem["ZCCZ3_HG"] = "合格";
                                mFlag_Hg_fun = true;
                            }
                        }
                        else
                            mitem["ZCCZ3_HG"] = "----";
                    }
                    else
                    {
                        mitem["ZCCZ3"] = "----";
                        mitem["ZCCZ3_HG"] = "----";
                    }

                    if (jcxm.Contains("、通断能力1、") || jcxm.Contains("、开关通断能力、") || jcxm.Contains("、插座通断能力、"))//开关通断能力？
                    {
                        if (GetSafeDouble(mitem["TDNL1"]) >= 0 && GetSafeDouble(mitem["TDNL1"]) < 3)
                        {
                            mitem["TDNL1_HG"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg_fun = true;
                        }
                        else
                        {
                            mitem["TDNL1_HG"] = "合格";
                            mFlag_Hg_fun = true;
                        }
                        if (GetSafeDouble(mitem["TDNL1"]) == -1)
                            mitem["TDNL1_HG"] = "----";
                    }
                    else
                    {
                        mitem["TDNL1"] = "-1";
                        mitem["TDNL1_HG"] = "----";
                    }

                    if (jcxm.Contains("、通断能力2、") || jcxm.Contains("、插座通断能力、") || jcxm.Contains("、开关通断能力、"))//插座通断能力？
                    {
                        if (GetSafeDouble(mitem["TDNL2"]) >= 0 && GetSafeDouble(mitem["TDNL2"]) < 3)
                        {
                            mitem["TDNL2_HG"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg_fun = true;
                        }
                        else
                        {
                            mitem["TDNL2_HG"] = "合格";
                            mFlag_Hg_fun = true;
                        }
                        if (GetSafeDouble(mitem["TDNL2"]) == -1)
                            mitem["TDNL2_HG"] = "----";
                    }
                    else
                    {
                        mitem["TDNL2"] = "-1";
                        mitem["TDNL2_HG"] = "----";
                    }

                    if (mbhggs > 0)
                        sitem["JCJG"] = "不合格";
                    else
                        sitem["JCJG"] = "合格";


                    mAllHg_fun = (mAllHg_fun && sitem["JCJG"].Trim() == "合格");
                    return mAllHg_fun;
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_KG_DJ"];
            var MItem = data["M_KG"];
            var SItem = data["S_KG"];
            //var tempTable = data["MS_BW"];
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            MItem[0]["JCJGMS"] = "";
            zj1 = 0;
            zj2 = 0;
            foreach (var sitem in SItem)
            {
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["GGXH"].Contains(sitem["GGXH"]));
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    MItem[0]["G_BZ"] = string.IsNullOrEmpty(mrsDj_Filter["BZ"]) ? "" : mrsDj_Filter["BZ"].Trim();
                    MItem[0]["G_DBH"] = string.IsNullOrEmpty(mrsDj_Filter["DBH"]) ? "" : mrsDj_Filter["DBH"].Trim();
                    MItem[0]["G_JXG"] = string.IsNullOrEmpty(mrsDj_Filter["JXG"]) ? "" : mrsDj_Filter["JXG"].Trim();
                    MItem[0]["G_FC"] = string.IsNullOrEmpty(mrsDj_Filter["FC"]) ? "" : mrsDj_Filter["FC"].Trim();
                    MItem[0]["G_JYDZ"] = string.IsNullOrEmpty(mrsDj_Filter["JYDZ"]) ? "" : mrsDj_Filter["JYDZ"].Trim();
                    MItem[0]["G_JG1"] = string.IsNullOrEmpty(mrsDj_Filter["JG1"]) ? "" : mrsDj_Filter["JG1"].Trim();
                    MItem[0]["G_JG2"] = string.IsNullOrEmpty(mrsDj_Filter["JG2"]) ? "" : mrsDj_Filter["JG2"].Trim();
                    MItem[0]["G_JG3"] = string.IsNullOrEmpty(mrsDj_Filter["JG3"]) ? "" : mrsDj_Filter["JG3"].Trim();
                    MItem[0]["G_TDNL1"] = string.IsNullOrEmpty(mrsDj_Filter["TDNL1"]) ? "" : mrsDj_Filter["TDNL1"].Trim();
                    MItem[0]["G_TDNL2"] = string.IsNullOrEmpty(mrsDj_Filter["TDNL2"]) ? "" : mrsDj_Filter["TDNL2"].Trim();
                    MItem[0]["G_DQD"] = string.IsNullOrEmpty(mrsDj_Filter["DQD"]) ? "" : mrsDj_Filter["DQD"].Trim();
                    MItem[0]["G_WS"] = string.IsNullOrEmpty(mrsDj_Filter["WS"]) ? "" : mrsDj_Filter["WS"].Trim();
                    MItem[0]["G_PDJL"] = string.IsNullOrEmpty(mrsDj_Filter["PDJL"]) ? "" : mrsDj_Filter["PDJL"].Trim();
                    MItem[0]["G_JYQH"] = string.IsNullOrEmpty(mrsDj_Filter["JYQH"]) ? "" : mrsDj_Filter["JYQH"].Trim();
                    MItem[0]["G_NR1"] = string.IsNullOrEmpty(mrsDj_Filter["NR1"]) ? "" : mrsDj_Filter["NR1"].Trim();
                    MItem[0]["G_NR2"] = string.IsNullOrEmpty(mrsDj_Filter["NR2"]) ? "" : mrsDj_Filter["NR2"].Trim();
                    MItem[0]["G_NR3"] = string.IsNullOrEmpty(mrsDj_Filter["NR3"]) ? "" : mrsDj_Filter["NR3"].Trim();
                    MItem[0]["G_JXQD"] = string.IsNullOrEmpty(mrsDj_Filter["JXQD"]) ? "" : mrsDj_Filter["JXQD"].Trim();
                    MItem[0]["G_LD1"] = string.IsNullOrEmpty(mrsDj_Filter["LD1"]) ? "" : mrsDj_Filter["LD1"].Trim();
                    MItem[0]["G_LD2"] = string.IsNullOrEmpty(mrsDj_Filter["LD2"]) ? "" : mrsDj_Filter["LD2"].Trim();
                    MItem[0]["G_ZCCZ1"] = string.IsNullOrEmpty(mrsDj_Filter["ZCCZ1"]) ? "" : mrsDj_Filter["ZCCZ1"].Trim();
                    MItem[0]["G_ZCCZ2"] = string.IsNullOrEmpty(mrsDj_Filter["ZCCZ2"]) ? "" : mrsDj_Filter["ZCCZ2"].Trim();
                    MItem[0]["G_ZCCZ3"] = string.IsNullOrEmpty(mrsDj_Filter["ZCCZ3"]) ? "" : mrsDj_Filter["ZCCZ3"].Trim();
                    MItem[0]["G_NLH"] = string.IsNullOrEmpty(mrsDj_Filter["NLH"]) ? "" : mrsDj_Filter["NLH"].Trim();
                    MItem[0]["G_DZ"] = string.IsNullOrEmpty(mrsDj_Filter["DZ"]) ? "" : mrsDj_Filter["DZ"].Trim();
                    MItem[0]["G_JX"] = string.IsNullOrEmpty(mrsDj_Filter["JX"]) ? "" : mrsDj_Filter["JX"].Trim();
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "试件尺寸为空";
                    break;
                }
                //旋转裂纹、旋转盖板、旋转缩松、旋转抗滑、旋转破坏、旋转力矩、旋转一般项、对接裂纹、对接盖板、对接缩松、对接抗拉、对接力矩、对接一般项
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    if (sjtabcalc(MItem[0], sitem, mAllHg))
                    { }
                    continue;
                }
                mbhggs = 0;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、标志、"))
                {
                    if (GetSafeDouble(MItem[0]["BZ"]) < 3 && GetSafeDouble(MItem[0]["BZ"]) >= 0)
                    {
                        MItem[0]["BZ_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["BZ_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["BZ"]) == -1)
                        MItem[0]["BZ_HG"] = "----";
                }
                else
                {
                    MItem[0]["BZ"] = "-1";
                    MItem[0]["BZ_HG"] = "----";
                }
                if (jcxm.Contains("、防潮、"))
                {
                    if (GetSafeDouble(MItem[0]["FC"]) < 3 && GetSafeDouble(MItem[0]["FC"]) >= 0)
                    {
                        MItem[0]["FC_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["FC_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["FC"]) == -1)
                        MItem[0]["FC_HG"] = "----";
                }
                else
                {
                    MItem[0]["FC"] = "-1";
                    MItem[0]["FC_HG"] = "----";
                }
                if (jcxm.Contains("、端子、"))
                {
                    if (GetSafeDouble(MItem[0]["DZ"]) < 3 && GetSafeDouble(MItem[0]["DZ"]) >= 0)
                    {
                        MItem[0]["DZ_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["DZ_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["DZ"]) == -1)
                        MItem[0]["DZ_HG"] = "----";
                }
                else
                {
                    MItem[0]["DZ"] = "-1";
                    MItem[0]["DZ_HG"] = "----";
                }
                if (jcxm.Contains("、电气间隙、"))
                {
                    if (GetSafeDouble(MItem[0]["JX"]) < 3 && GetSafeDouble(MItem[0]["JX"]) >= 0)
                    {
                        MItem[0]["JX_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["JX_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["JX"]) == -1)
                        MItem[0]["JX_HG"] = "----";
                }
                else
                {
                    MItem[0]["JX"] = "-1";
                    MItem[0]["JX_HG"] = "----";
                }
                if (jcxm.Contains("、机构、"))
                {
                    if (GetSafeDouble(MItem[0]["JXG"]) < 3 && GetSafeDouble(MItem[0]["JXG"]) >= 0)
                    {
                        MItem[0]["JXG_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["JXG_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["JXG"]) == -1)
                        MItem[0]["JXG_HG"] = "----";
                }
                else
                {
                    MItem[0]["JXG"] = "-1";
                    MItem[0]["JXG_HG"] = "----";
                }
                if (jcxm.Contains("、结构、"))
                {
                    if (GetSafeDouble(MItem[0]["JG1"]) >= 0 && GetSafeDouble(MItem[0]["JG1"]) < 3)
                    {
                        MItem[0]["JG1_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["JG1_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["JG2"]) >= 0 && GetSafeDouble(MItem[0]["JG2"]) < 3)
                    {
                        MItem[0]["JG2_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["JG2_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["JG3"]) >= 0 && GetSafeDouble(MItem[0]["JG3"]) < 3)
                    {
                        MItem[0]["JG3_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["JG3_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["JG1"]) == -1)
                        MItem[0]["JG1_HG"] = "----";
                    if (GetSafeDouble(MItem[0]["JG2"]) == -1)
                        MItem[0]["JG2_HG"] = "----";
                    if (GetSafeDouble(MItem[0]["JG3"]) == -1)
                        MItem[0]["JG3_HG"] = "----";
                }
                else
                {
                    MItem[0]["JG1"] = "-1";
                    MItem[0]["JG2"] = "-1";
                    MItem[0]["JG3"] = "-1";
                    MItem[0]["JG1_HG"] = "----";
                    MItem[0]["JG2_HG"] = "----";
                    MItem[0]["JG3_HG"] = "----";
                }
                if (jcxm.Contains("、电气强度、"))
                {
                    if (GetSafeDouble(MItem[0]["DQD"]) < 3 && GetSafeDouble(MItem[0]["DQD"]) >= 0)
                    {
                        MItem[0]["DQD_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["DQD_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["DQD"]) == -1)
                        MItem[0]["DQD_HG"] = "----";
                }
                else
                {
                    MItem[0]["DQD"] = "-1";
                    MItem[0]["DQD_HG"] = "----";
                }
                if (jcxm.Contains("、温升、"))
                {
                    if (GetSafeDouble(MItem[0]["WS"]) < 3 && GetSafeDouble(MItem[0]["WS"]) >= 0)
                    {
                        MItem[0]["WS_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["WS_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["WS"]) == -1)
                        MItem[0]["WS_HG"] = "----";
                }
                else
                {
                    MItem[0]["WS"] = "-1";
                    MItem[0]["WS_HG"] = "----";
                }
                if (jcxm.Contains("、耐老化、"))
                {
                    if (GetSafeDouble(MItem[0]["NLH"]) < 3 && GetSafeDouble(MItem[0]["NLH"]) >= 0)
                    {
                        MItem[0]["NLH_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["NLH_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["NLH"]) == -1)
                        MItem[0]["NLH_HG"] = "----";
                }
                else
                {
                    MItem[0]["NLH"] = "-1";
                    MItem[0]["NLH_HG"] = "----";
                }
                if (jcxm.Contains("、绝缘电阻、"))
                {
                    if (GetSafeDouble(MItem[0]["JYDZ"]) < 3 && GetSafeDouble(MItem[0]["JYDZ"]) >= 0)
                    {
                        MItem[0]["JYDZ_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["JYDZ_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["JYDZ"]) == -1)
                        MItem[0]["JYDZ_HG"] = "----";
                }
                else
                {
                    MItem[0]["JYDZ"] = "-1";
                    MItem[0]["JYDZ_HG"] = "----";

                }

                if (jcxm.Contains("、正常操作1、") || jcxm.Contains("、开关正常操作次数、"))

                {
                    if (GetSafeDouble(MItem[0]["ZCCZ1"]) >= 0 && GetSafeDouble(MItem[0]["ZCCZ1"]) < 3)
                    {
                        MItem[0]["ZCCZ1_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["ZCCZ1_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["ZCCZ1"]) == -1)
                        MItem[0]["ZCCZ1_HG"] = "----";
                }
                else
                {
                    MItem[0]["ZCCZ1"] = "-1";
                    MItem[0]["ZCCZ1_HG"] = "----";
                }
                if (jcxm.Contains("、正常操作2、") || jcxm.Contains("、开关正常操作次数、"))
                {
                    if (GetSafeDouble(MItem[0]["ZCCZ2"]) >= 0 && GetSafeDouble(MItem[0]["ZCCZ2"]) < 3)
                    {
                        MItem[0]["ZCCZ2_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["ZCCZ2_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["ZCCZ2"]) == -1)
                        MItem[0]["ZCCZ2_HG"] = "----";
                }
                else
                {
                    MItem[0]["ZCCZ2"] = "-1";
                    MItem[0]["ZCCZ2_HG"] = "----";
                }
                if (jcxm.Contains("、正常操作3、") || jcxm.Contains("、开关正常操作次数、"))
                {
                    if (GetSafeDouble(MItem[0]["ZCCZ3"]) >= 0 && GetSafeDouble(MItem[0]["ZCCZ3"]) < 3)
                    {
                        MItem[0]["ZCCZ3_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["ZCCZ3_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["ZCCZ3"]) == -1)
                        MItem[0]["ZCCZ3_HG"] = "----";
                }
                else
                {
                    MItem[0]["ZCCZ3"] = "-1";
                    MItem[0]["ZCCZ3_HG"] = "----";
                }
                if (jcxm.Contains("、爬电距离、"))
                {
                    if (GetSafeDouble(MItem[0]["PDJL1"]) >= 0 && GetSafeDouble(MItem[0]["PDJL1"]) < 3)
                    {
                        MItem[0]["PDJL1_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["PDJL1_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["PDJL2"]) >= 0 && GetSafeDouble(MItem[0]["PDJL2"]) < 3)
                    {
                        MItem[0]["PDJL2_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["PDJL2_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["PDJL1"]) == -1)
                        MItem[0]["PDJL1_HG"] = "----";
                    if (GetSafeDouble(MItem[0]["PDJL2"]) == -1)
                        MItem[0]["PDJL2_HG"] = "----";
                }
                else
                {
                    MItem[0]["PDJL1"] = "-1";
                    MItem[0]["PDJL2"] = "-1";
                    MItem[0]["PDJL1_HG"] = "----";
                    MItem[0]["PDJL2_HG"] = "----";
                }
                if (jcxm.Contains("、耐热、"))
                {
                    if (GetSafeDouble(MItem[0]["NR1"]) >= 0 && GetSafeDouble(MItem[0]["NR1"]) < 3)
                    {
                        MItem[0]["NR1_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["NR1_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["NR2"]) >= 0 && GetSafeDouble(MItem[0]["NR2"]) < 3)
                    {
                        MItem[0]["NR2_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["NR2_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["NR3"]) >= 0 && GetSafeDouble(MItem[0]["NR3"]) < 3)
                    {
                        MItem[0]["NR3_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["NR3_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["NR1"]) == -1)
                        MItem[0]["NR1_HG"] = "----";
                    if (GetSafeDouble(MItem[0]["NR2"]) == -1)
                        MItem[0]["NR2_HG"] = "----";
                    if (GetSafeDouble(MItem[0]["NR3"]) == -1)
                        MItem[0]["NR3_HG"] = "----";
                }
                else
                {
                    MItem[0]["NR1"] = "-1";
                    MItem[0]["NR2"] = "-1";
                    MItem[0]["NR3"] = "-1";
                    MItem[0]["NR1_HG"] = "----";
                    MItem[0]["NR2_HG"] = "----";
                    MItem[0]["NR3_HG"] = "----";
                }

                if (jcxm.Contains("、通断能力1、") || jcxm.Contains("、插座通断能力、"))//开关通断能力？
                {
                    if (GetSafeDouble(MItem[0]["TDNL1"]) >= 0 && GetSafeDouble(MItem[0]["TDNL1"]) < 3)
                    {
                        MItem[0]["TDNL1_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["TDNL1_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["TDNL1"]) == -1)
                        MItem[0]["TDNL1_HG"] = "----";
                }
                else
                {
                    MItem[0]["TDNL1"] = "-1";
                    MItem[0]["TDNL1_HG"] = "----";
                }
                if (jcxm.Contains("、通断能力2、") || jcxm.Contains("、插座通断能力、"))//插座通断能力？
                {
                    if (GetSafeDouble(MItem[0]["TDNL2"]) >= 0 && GetSafeDouble(MItem[0]["TDNL2"]) < 3)
                    {
                        MItem[0]["TDNL2_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["TDNL2_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["TDNL2"]) == -1)
                        MItem[0]["TDNL2_HG"] = "----";
                }
                else
                {
                    MItem[0]["TDNL2"] = "-1";
                    MItem[0]["TDNL2_HG"] = "----";
                }
                if (jcxm.Contains("、截流部件及连接、"))
                {
                    if (GetSafeDouble(MItem[0]["LD1"]) >= 0 && GetSafeDouble(MItem[0]["LD1"]) < 3)
                    {
                        MItem[0]["LD1_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["LD1_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["LD2"]) >= 0 && GetSafeDouble(MItem[0]["LD2"]) < 3)
                    {
                        MItem[0]["LD2_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["LD2_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["LD1"]) == -1)
                        MItem[0]["LD1_HG"] = "----";
                    if (GetSafeDouble(MItem[0]["LD2"]) == -1)
                        MItem[0]["LD2_HG"] = "----";
                }
                else
                {
                    MItem[0]["LD1"] = "-1";
                    MItem[0]["LD2"] = "-1";
                    MItem[0]["LD1_HG"] = "----";
                    MItem[0]["LD2_HG"] = "----";
                }
                if (jcxm.Contains("、耐漏电起痕、"))
                {
                    if (GetSafeDouble(MItem[0]["JYQH"]) < 3 && GetSafeDouble(MItem[0]["JYQH"]) >= 0)
                    {
                        MItem[0]["JYQH_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["JYQH_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["JYQH"]) == -1)
                        MItem[0]["JYQH_HG"] = "----";
                }
                else
                {
                    MItem[0]["JYQH"] = "-1";
                    MItem[0]["JYQH_HG"] = "----";
                }
                if (jcxm.Contains("、机械强度、"))
                {
                    if (GetSafeDouble(MItem[0]["JXQD"]) < 3 && GetSafeDouble(MItem[0]["JXQD"]) >= 0)
                    {
                        MItem[0]["JXQD_HG"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        MItem[0]["JXQD_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    if (GetSafeDouble(MItem[0]["JXQD"]) == -1)
                        MItem[0]["JXQD_HG"] = "----";
                }
                else
                {
                    MItem[0]["JXQD"] = "-1";
                    MItem[0]["JXQD_HG"] = "----";
                }

                if (mbhggs > 0)
                {
                    sitem["JCJG"] = "不合格";
                    mFlag_Bhg = true;
                }
                else
                {
                    sitem["JCJG"] = "合格";
                    mFlag_Hg = true;
                }
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");
            }
            //主表总判断赋值
            string mjgsm;
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                mjgsm = "该组试样所检项目符合标准要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                mjgsm = "该组试样不符合标准要求。";
                if (mFlag_Bhg && mFlag_Hg)
                {
                    MItem[0]["JCJGMS"] = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                    mjgsm = "该组试样所检项目部分符合标准要求。";
                }
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}