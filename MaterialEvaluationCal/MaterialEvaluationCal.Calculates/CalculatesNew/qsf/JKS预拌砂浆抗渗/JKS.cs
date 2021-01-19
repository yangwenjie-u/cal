using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class JKS:BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            /* 
             * 计算项目：混泥土抗渗
             *  参考标准：
             * GB/T 50082-2009
             */
            #region 参数定义
            string mcalBh, mlongStr;
            string mSjdjbh, mSjdj;
            double mQdyq, mGdyq;
            int vp, vi, vv;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mSFwc;
            bool mGetBgbh;
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            #endregion
           
            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_KS_DJ"];
            var MItem = data["M_JKS"];
            var SItem = data["S_JKS"];
            #endregion

            #region  计算开始
            string which = "1";
            MItem[0]["JCJGMS"] = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            foreach (var sitem in SItem)
            {
                string mSz = "0";
                double mbzssgd = 0;
                mSjdj = sitem["SJDJ"];            //设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"] == mSjdj.Trim());
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    mSz = mrsDj_Filter["SZ"];
                    mQdyq = GetSafeDouble(mrsDj_Filter["QDYQ"]);
                    //mbzssgd = GetSafeDouble(mrsDj_Filter["GDYQ"]);
                    //mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mQdyq = 0;
                    mJSFF = "";
                    sitem["JCJG"] = "不下结论";
                    break;
                }
                //计算龄期
                //sitem["LQ"] = (GetSafeDateTime(sitem["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString();
                vi = 0;
                vv = 0;
                //if (which == "0")
                //{
                //    if (mbzssgd != 0)
                //    {
                //        if (GetSafeDouble(sitem["SSGD1"]) >= mbzssgd)
                //            vi = vi + 1;
                //        if (GetSafeDouble(sitem["SSGD2"]) >= mbzssgd)
                //            vi = vi + 1;
                //        if (GetSafeDouble(sitem["ssgd3"]) >= mbzssgd)
                //            vi = vi + 1;
                //        if (GetSafeDouble(sitem["SSGD4"]) >= mbzssgd)
                //            vi = vi + 1;
                //        if (GetSafeDouble(sitem["SSGD5"]) >= mbzssgd)
                //            vi = vi + 1;
                //        if (GetSafeDouble(sitem["SSGD6"]) >= mbzssgd)
                //            vi = vi + 1;


                //        if (vi >= 3)
                //        {
                //            sitem["JKS_HG"] = "0";
                //            sitem["JCJG"] = "不合格";
                //            mAllHg = false;
                //        }
                //        else
                //        {
                //            sitem["JKS_HG"] = "1";
                //            sitem["JCJG"] = "合格";
                //            mAllHg = true;
                //        }
                //    }
                //}
                //else
                //{
                    if (Conversion.Val(sitem["KYQD1"]) >= mQdyq && sitem["SFBSS1"] == "1")
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD2"]) >= mQdyq && sitem["SFBSS2"] == "1")
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD3"]) >= mQdyq && sitem["SFBSS3"] == "1")
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD4"]) >= mQdyq && sitem["SFBSS4"] == "1")
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD5"]) >= mQdyq && sitem["SFBSS5"] == "1")
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD6"]) >= mQdyq && sitem["SFBSS6"] == "1")
                        vv = vv + 1;
                    if (vv <= 3)
                    {
                        sitem["JKS_HG"] = "0";
                        sitem["JCJG"] = "不合格";
                        mAllHg = false;
                    }
                    else
                    {
                        sitem["JKS_HG"] = "1";
                        sitem["JCJG"] = "合格";
                        mAllHg = true;
                    }
                //}
            }
            //主表总判断赋值
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jcxmCur = "抗水渗透性能";
                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
