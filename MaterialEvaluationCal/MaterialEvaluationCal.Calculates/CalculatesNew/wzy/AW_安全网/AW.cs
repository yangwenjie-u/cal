using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class AW : BaseMethods
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
            var mrsDj = dataExtra["BZ_AW_DJ"];
            var MItem = data["M_AW"];
            var mitem = MItem[0];
            var SItem = data["S_AW"];
            #endregion

            #region  计算开始
            mitem["JCJGMS"] = "";
            zj1 = 0;
            zj2 = 0;
            foreach (var sitem in SItem)
            {
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["YBDX"].Contains(sitem["YBDX"]) && x["MC"].Contains(sitem["WLX"]));
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    sitem["NCJYQ"] = mrsDj_Filter["NCJXN"];
                    sitem["NGCYQ"] = mrsDj_Filter["NGCXN"];
                    sitem["DLQDSCLYQ"] = mrsDj_Filter["DLQLSCL"];
                    sitem["JFBWKLYQ"] = mrsDj_Filter["JFKLQK"];
                    mitem["ZYH"] = mrsDj_Filter["ZYH"];
                    mitem["ZYB"] = mrsDj_Filter["ZYB"];
                    mitem["YBH"] = mrsDj_Filter["YBH"];
                    mitem["YBB"] = mrsDj_Filter["YBB"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                sitem["BPD"] = "----";
                sitem["BLB"] = "-1";
                if (sitem["WLX"].Contains("平网") || sitem["WLX"].Trim() == "立网")
                {
                    sitem["ALB"] = sitem["NCJB"];
                    sitem["BLB"] = "0";
                    if (GetSafeDouble(sitem["ALB"]) >= GetSafeDouble(mrsDj_Filter["ZYB"]))
                        sitem["APD"] = "不合格";
                    if (GetSafeDouble(sitem["ALB"]) <= GetSafeDouble(mrsDj_Filter["ZYH"]))
                        sitem["APD"] = "合格";
                    if (GetSafeDouble(sitem["BLB"]) >= GetSafeDouble(mrsDj_Filter["YBB"]))
                        sitem["BPD"] = "不合格";
                    if (GetSafeDouble(sitem["BLB"]) <= GetSafeDouble(mrsDj_Filter["YBH"]))
                        sitem["BPD"] = "合格";
                }
                if (sitem["WLX"].Contains("密目式安全立网"))
                {
                    sitem["ALB"] = (GetSafeDouble(sitem["NCJB"]) + GetSafeDouble(sitem["NGCB"]) + GetSafeDouble(sitem["DLQDSCLB"]) + GetSafeDouble(sitem["JFBWKLB"])).ToString();
                    sitem["BLB"] = "0";
                    if (GetSafeDouble(sitem["ALB"]) >= GetSafeDouble(mrsDj_Filter["ZYB"]))
                        sitem["APD"] = "不合格";
                    if (GetSafeDouble(sitem["ALB"]) <= GetSafeDouble(mrsDj_Filter["ZYH"]))
                        sitem["APD"] = "合格";
                    if (GetSafeDouble(sitem["BLB"]) >= GetSafeDouble(mrsDj_Filter["YBB"]))
                        sitem["BPD"] = "不合格";
                    if (GetSafeDouble(sitem["BLB"]) <= GetSafeDouble(mrsDj_Filter["YBH"]))
                        sitem["BPD"] = "合格";
                }

                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、耐冲击性能、"))
                { }
                else
                {
                    sitem["NCJYQ"] = "----";
                    sitem["NCJPD"] = "----";
                }
                if (jcxm.Contains("、耐贯穿性能、"))
                { }
                else
                {
                    sitem["NGCYQ"] = "----";
                    sitem["NGCPD"] = "----";
                }


                if (jcxm.Contains("、断裂强力*断裂伸长、"))
                { }
                else
                {
                    sitem["DLQDSCLYQ"] = "----";
                    sitem["DLQDSCLPD"] = "----";
                }
                if (jcxm.Contains("、接缝部位抗拉强力、"))
                { }
                else
                {
                    sitem["JFBWKLYQ"] = "----";
                    sitem["JFBWKLPD"] = "----";
                }

                #region 质量
                if (jcxm.Contains("、质量、"))
                {

                }
                else
                {

                }
                #endregion

                #region 外观尺寸
                if (jcxm.Contains("、外观尺寸、"))
                {

                }
                else
                {

                }
                #endregion

                #region 一般要求
                if (jcxm.Contains("、一般要求、"))
                {

                }
                else
                {

                }
                #endregion

                if (sitem["APD"] == "不合格" || sitem["BPD"] == "不合格")
                {
                    sitem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                else
                {
                    sitem["JCJG"] = "合格";
                    mAllHg = true;
                }
            }
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
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
