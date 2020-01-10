using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class LBG : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 参数定义
            string mcalBh;
            string mSjdjbh, mSjdj;
            int mKlqd, mScl, mLw;
            int mHggs_klqd, mHggs_scl, mHggs_lw, mxlgs, mxwgs, mZh = 0;
            int mFsgs_klqd, mFsgs_scl, mFsgs_lw;
            int mLwzj, mLwjd;
            string QdBzyq, LwBzyq, SclBzyq;
            int vp, mCnt_FjHg, mCnt_FjHg1;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs, mLsfs, mLwfs;
            bool mGetBgbh;
            bool mSFwc;
            bool mFlag_Hg, mFlag_Bhg;
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_LBG_DJ"];
            var MItem = data["M_LBG"];
            var SItem = data["S_LBG"];
            #endregion

            #region 计算开始
            mCnt_FjHg = 0;
            mCnt_FjHg1 = 0;
            foreach (var sitem in SItem)
            {
                sitem["FJ"] = "0";
                mZh = GetSafeInt(sitem["ZH_G"]);
                var mrsDj_Filter = mrsDj.FirstOrDefault();
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                else
                {
                    sitem["JCJG"] = "依据不详";
                    break;
                }
                int mbhgs = 0;
                sitem["JYHZ"] = Round(Conversion.Val(sitem["SJZDLBL"]), 2).ToString("F2");
                if (Conversion.Val(sitem["KLHZ"]) < Conversion.Val(sitem["SJZDLBL"]))
                    mbhgs = mbhgs + 1;


                if (mbhgs > 0)
                {
                    sitem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                else
                {
                    sitem["JCJG"] = "合格";
                }
            }
            if (mZh < 2)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "----";
            }
            else
            {
                if (mAllHg)
                {
                    MItem[0]["JCJG"] = "合格";
                    MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "，该组试样检验荷载实测值符合设计要求。";
                }
                else
                {
                    MItem[0]["JCJG"] = "不合格";
                    MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "，该组试样检验荷载实测值不符合设计要求。";
                }
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
