using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class YKJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;
            double[] mkyqdArray = new double[3];
            double zj1, zj2;
            int mHggs_qfqd, mHggs_klqd, mHggs_scl, mHggs_lw;
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
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_YKJ_DJ"];
            var MItem = data["M_YKJ"];
            var mitem = MItem[0];
            var SItem = data["S_YKJ"];
            #endregion

            #region  计算开始
            mitem["JCJGMS"] = "";
            zj1 = 0;
            zj2 = 0;
            foreach (var sitem in SItem)
            {
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.Where(x => x["GGXH"].Contains(sitem["GGXH"].Trim())).ToList();
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                    mJSFF = !string.IsNullOrEmpty(mrsDj_Filter[0]["JSFF"]) ? "" : mrsDj_Filter[0]["JSFF"].Trim().ToLower();
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "试件尺寸为空";
                }
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    sitem["XZKH1"] = Conversion.Val(sitem["KJKH"]).ToString();
                    sitem["ZJKH1"] = Conversion.Val(sitem["KJKH"]).ToString();
                    sitem["XZKHSM"] = sitem["KJKHSM"];
                    sitem["ZJKHSM"] = sitem["KJKHSM"];
                    sitem["XZPH"] = Conversion.Val(sitem["KJPH"]).ToString();
                    sitem["XZPHSM"] = sitem["KJPHSM"];
                    sitem["ZJPH"] = Conversion.Val(sitem["KJPH"]).ToString();
                    sitem["ZJPHSM"] = sitem["KJPHSM"];
                    sitem["ZJNZGD"] = Conversion.Val(sitem["KJNZG"]).ToString();
                    sitem["ZJNZGSM"] = sitem["KJNZGSM"];
                    sitem["DJKL"] = Conversion.Val(sitem["KJKL"]).ToString();
                    sitem["DJKLSM"] = sitem["KJKLSM"];
                    sitem["DJNLJ"] = Conversion.Val(sitem["KJNLJ"]).ToString();
                    sitem["DJNLJSM"] = sitem["KJNLJSM"];
                    sitem["XZNLJ"] = Conversion.Val(sitem["KJNLJ"]).ToString();
                    sitem["XZNLJSM"] = sitem["KJNLJSM"];
                    sitem["ZJNLJ"] = Conversion.Val(sitem["KJNLJ"]).ToString();
                    sitem["ZJNLJSM"] = sitem["KJNLJSM"];
                }
                var jcxm = "、" + sitem["KJLB"].Replace(',', '、') + "、";
                if (jcxm.Contains("、直角扣件、"))
                {
                    sitem["YPSL"] = sitem["YBDX"];
                    var mrsDj_Filter2 = mrsDj.FirstOrDefault(x => x["YBDX"].Contains(sitem["YPSL"].Trim()) && x["DJYB"].Contains(mitem["DJYB"]));
                    mitem["ZYH"] = mrsDj_Filter2["ZYH"];
                    mitem["ZYB"] = mrsDj_Filter2["ZYB"];
                    mitem["YBH"] = mrsDj_Filter2["YBH"];
                    mitem["YBB"] = mrsDj_Filter2["YBB"];
                    sitem["ZJZYB"] = (GetSafeDouble(sitem["ZJLW"]) + GetSafeDouble(sitem["ZJGB"]) + GetSafeDouble(sitem["ZJKH1"]) + GetSafeDouble(sitem["ZJPH"]) + GetSafeDouble(sitem["ZJNLJ"]) + GetSafeDouble(sitem["ZJNZGD"])).ToString();
                    if (GetSafeDouble(sitem["ZJZYB"]) >= GetSafeDouble(mrsDj_Filter2["ZYB"]))
                        sitem["ZJZYPD"] = "不合格";
                    else
                    {
                        if (GetSafeDouble(sitem["ZJZYB"]) > GetSafeDouble(mrsDj_Filter2["ZYH"]))
                            sitem["ZJZYPD"] = "需取第二样本检测";
                        else
                            sitem["ZJZYPD"] = "合格";
                    }
                    if (GetSafeDouble(sitem["ZJYBBHGS"]) >= GetSafeDouble(mrsDj_Filter2["YBB"]))
                        sitem["ZJYBPD"] = "不合格";
                    else
                    {
                        if (GetSafeDouble(sitem["ZJYBBHGS"]) > GetSafeDouble(mrsDj_Filter2["YBH"]))
                            sitem["ZJYBPD"] = "需取第二样本检测";
                        else
                            sitem["ZJYBPD"] = "合格";

                    }
                    if (sitem["ZJZYPD"] == "不合格" || sitem["ZJYBPD"] == "不合格")
                        mitem["ZJZPD"] = "不合格";
                    else
                    {
                        if (sitem["ZJZYPD"] == "需取第二样本检测" || sitem["ZJYBPD"] == "需取第二样本检测")
                            mitem["ZJZPD"] = "需取第二样本检测";
                        else
                            mitem["ZJZPD"] = "合格";
                    }
                }
                else
                {
                    sitem["ZJZYPD"] = "----";
                    sitem["ZJYBPD"] = "----";
                    sitem["ZJYBSM"] = "----";
                    sitem["ZJZYB"] = "-1";
                    sitem["ZJLW"] = "-1";
                    sitem["ZJGB"] = "-1";
                    sitem["ZJKH1"] = "-1";
                    sitem["ZJKH2"] = "-1";
                    sitem["ZJPH"] = "-1";
                    sitem["ZJNLJ"] = "-1";
                    sitem["ZJNZGD"] = "-1";
                    sitem["ZJYBBHGS"] = "-1";
                    sitem["ZJLWSM"] = "----";
                    sitem["ZJGBSM"] = "----";
                    sitem["ZJKHSM"] = "----";
                    sitem["ZJPHSM"] = "----";
                    sitem["ZJNLJSM"] = "----";
                    sitem["ZJNZGSM"] = "----";
                    sitem["ZJYBSM"] = "----";
                }
                if (jcxm.Contains("、旋转扣件、"))
                {
                    sitem["YPSL1"] = sitem["YBDX"];
                    var mrsDj_Filter2 = mrsDj.FirstOrDefault(x => x["YBDX"].Contains(sitem["YPSL1"].Trim()) && x["DJYB"].Contains(mitem["DJYB"]));
                    mitem["ZYH"] = mrsDj_Filter2["ZYH"];
                    mitem["ZYB"] = mrsDj_Filter2["ZYB"];
                    mitem["YBH"] = mrsDj_Filter2["YBH"];
                    mitem["YBB"] = mrsDj_Filter2["YBB"];
                    sitem["XZZYB"] = (GetSafeDouble(sitem["XZLW"]) + GetSafeDouble(sitem["XZGB"]) + GetSafeDouble(sitem["XZKH1"]) + GetSafeDouble(sitem["XZPH"]) + GetSafeDouble(sitem["XZNLJ"])).ToString();
                    if (GetSafeDouble(sitem["XZZYB"]) >= GetSafeDouble(mrsDj_Filter2["ZYB"]))
                    {
                        sitem["XZZYPD"] = "不合格";
                        mitem["XZZPD"] = "不合格";
                    }
                    else
                    {
                        if (GetSafeDouble(sitem["XZZYB"]) > GetSafeDouble(mrsDj_Filter2["ZYH"]))
                            sitem["XZZYPD"] = "需取第二样本检测";
                        else
                            sitem["XZZYPD"] = "合格";

                    }
                    if (GetSafeDouble(sitem["XZYBBHGS"]) >= GetSafeDouble(mrsDj_Filter2["YBB"]))
                    {
                        sitem["XZYBPD"] = "不合格";
                        mitem["XZZPD"] = "不合格";
                    }
                    else
                    {
                        if (GetSafeDouble(sitem["XZYBBHGS"]) > GetSafeDouble(mrsDj_Filter2["YBH"]))
                            sitem["XZYBPD"] = "需取第二样本检测";
                        else
                            sitem["XZYBPD"] = "合格";

                    }



                    if (sitem["XZZYPD"] == "不合格" || sitem["XZYBPD"] == "不合格")
                        mitem["XZZPD"] = "不合格";
                    else
                    {
                        if (sitem["XZZYPD"] == "需取第二样本检测" || sitem["XZYBPD"] == "需取第二样本检测")
                            mitem["XZZPD"] = "需取第二样本检测";
                        else
                            mitem["XZZPD"] = "合格";
                    }
                }
                else
                {
                    sitem["XZZYPD"] = "----";
                    sitem["XZYBPD"] = "----";
                    sitem["XZYBSM"] = "----";
                    sitem["XZZYB"] = "-1";
                    sitem["XZLW"] = "-1";
                    sitem["XZGB"] = "-1";
                    sitem["XZKH1"] = "-1";
                    sitem["XZKH2"] = "-1";
                    sitem["XZPH"] = "-1";
                    sitem["XZNLJ"] = "-1";
                    sitem["XZYBBHGS"] = "-1";
                    sitem["XZLWSM"] = "----";
                    sitem["XZGBSM"] = "----";
                    sitem["XZKHSM"] = "----";
                    sitem["XZPHSM"] = "----";
                    sitem["XZNLJSM"] = "----";
                }
                if (jcxm.Contains("、对接扣件、"))
                {
                    sitem["YPSL2"] = sitem["YBDX"];
                    var mrsDj_Filter2 = mrsDj.FirstOrDefault(x => x["YBDX"].Contains(sitem["YPSL2"].Trim()) && x["DJYB"].Contains(mitem["DJYB"]));
                    mitem["ZYH"] = mrsDj_Filter2["ZYH"];
                    mitem["ZYB"] = mrsDj_Filter2["ZYB"];
                    mitem["YBH"] = mrsDj_Filter2["YBH"];
                    mitem["YBB"] = mrsDj_Filter2["YBB"];
                    sitem["DJZYB"] = (GetSafeDouble(sitem["DJLW"]) + GetSafeDouble(sitem["DJGB"]) + GetSafeDouble(sitem["DJKL"]) + GetSafeDouble(sitem["DJNLJ"])).ToString();

                    if (GetSafeDouble(sitem["DJZYB"]) >= GetSafeDouble(mrsDj_Filter2["ZYB"]))
                        sitem["DJZYPD"] = "不合格";
                    else
                    {
                        if (GetSafeDouble(sitem["DJZYB"]) > GetSafeDouble(mrsDj_Filter2["ZYH"]))
                            sitem["DJZYPD"] = "需取第二样本检测";
                        else
                            sitem["DJZYPD"] = "合格";
                    }
                    if (GetSafeDouble(sitem["DJYBBHGS"]) >= GetSafeDouble(mrsDj_Filter2["YBB"]))
                        sitem["DJYBPD"] = "不合格";
                    else
                    {
                        if (GetSafeDouble(sitem["DJYBBHGS"]) > GetSafeDouble(mrsDj_Filter2["YBH"]))
                            sitem["DJYBPD"] = "需取第二样本检测";
                        else
                            sitem["DJYBPD"] = "合格";
                    }


                    if (sitem["DJZYPD"] == "不合格" || sitem["DJYBPD"] == "不合格")
                        mitem["DJZPD"] = "不合格";
                    else
                    {
                        if (sitem["DJZYPD"] == "需取第二样本检测" || sitem["DJYBPD"] == "需取第二样本检测")
                            mitem["DJZPD"] = "需取第二样本检测";
                        else
                            mitem["DJZPD"] = "合格";
                    }
                }
                else
                {
                    sitem["DJZYPD"] = "----";
                    sitem["DJYBPD"] = "----";
                    sitem["DJYBSM"] = "----";
                    sitem["DJZYB"] = "-1";
                    sitem["DJLW"] = "-1";
                    sitem["DJGB"] = "-1";
                    sitem["DJKL"] = "-1";
                    sitem["DJNLJ"] = "-1";
                    sitem["DJYBBHGS"] = "-1";
                    sitem["DJYBSM"] = "----";
                    sitem["DJLWSM"] = "----";
                    sitem["DJGBSM"] = "----";
                    sitem["DJKLSM"] = "----";
                    sitem["DJNLJSM"] = "----";
                }
            }
            //综合判断
            mitem["JCJGMS"] = "";
            string mjgsm = "";
            if (mitem["ZJZPD"] == "不合格" || mitem["XZZPD"] == "不合格" || mitem["DJZPD"] == "不合格")  //判断是否合格
            {
                mAllHg = false;
                if (mitem["ZJZPD"] == "不合格")
                    mitem["JCJGMS"] = "直角扣件";
                if (mitem["XZZPD"] == "不合格")
                {
                    if (mitem["JCJGMS"].Trim().Length > 0)
                        mitem["JCJGMS"] = mitem["JCJGMS"] + "、旋转扣件";
                    else
                        mitem["JCJGMS"] = "旋转扣件";
                }
                if (mitem["DJZPD"] == "不合格")
                {
                    if (mitem["JCJGMS"].Trim().Length > 0)
                        mitem["JCJGMS"] = mitem["JCJGMS"] + "、对接扣件";
                    else
                        mitem["JCJGMS"] = "对接扣件";
                }


                if (mitem["ZJZPD"] == "不合格" || mitem["XZZPD"] == "不合格" || mitem["DJZPD"] == "不合格")
                {
                    mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
                    mjgsm = "该组试样不符合标准要求。";
                }
            }
            else
            {
                if (mitem["ZJZPD"] == "需取第二样本检测" || mitem["XZZPD"] == "需取第二样本检测" || mitem["DJZPD"] == "需取第二样本检测")  //判断是否合格
                {
                    mAllHg = false;
                    if (mitem["ZJZPD"] == "需取第二样本检测")
                        mitem["JCJGMS"] = "直角扣件";
                    if (mitem["XZZPD"] == "需取第二样本检测")
                    {
                        if (mitem["JCJGMS"].Trim().Length > 0)
                            mitem["JCJGMS"] = mitem["JCJGMS"] + "、旋转扣件";
                        else
                            mitem["JCJGMS"] = "旋转扣件";
                    }
                    if (mitem["DJZPD"] == "需取第二样本检测")
                    {
                        if (mitem["JCJGMS"].Trim().Length > 0)
                            mitem["JCJGMS"] = mitem["JCJGMS"] + "、对接扣件";
                        else
                            mitem["JCJGMS"] = "对接扣件";
                    }
                    if (mitem["JCJGMS"].Trim().Length > 0)
                    {
                        mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求，需取第二样本检测。";
                        mjgsm = "该组试样不符合标准要求，需取第二样本检测。";
                    }
                }
                else
                {
                    mAllHg = true;
                    mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
                    mjgsm = "该组试样所检项目符合标准要求。";
                }
            }
            //主表总判断赋值
            if (mAllHg)
                mitem["JCJG"] = "合格";
            else
                mitem["JCJG"] = "不合格";
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
