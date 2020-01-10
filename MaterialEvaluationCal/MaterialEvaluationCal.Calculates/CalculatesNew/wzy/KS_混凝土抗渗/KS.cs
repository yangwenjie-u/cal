using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class KS : BaseMethods
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

            #region 自定义函数
            Func<IDictionary<string, string>, IDictionary<string, string>, bool, bool> sjtabcalc =
                delegate (IDictionary<string, string> mitem, IDictionary<string, string> sitem, bool mAllHg_fun)
                {
                    bool sign;
                    int xd, Gs;
                    double md1, md2, md, sum, pjmd;
                    double[] nArr;
                    int mbhggs_fun = 0;
                    int sum_fun = 0;
                    bool ret_sjtabcalc = true;
                    int mbhggs = 0;
                    pjmd = 0.6;
                    pjmd = sitem["SJDJ"].Contains("P4") ? 0.4 : pjmd;
                    pjmd = sitem["SJDJ"].Contains("P6") ? 0.6 : pjmd;
                    pjmd = sitem["SJDJ"].Contains("P8") ? 0.8 : pjmd;
                    pjmd = sitem["SJDJ"].Contains("P10") ? 1.0 : pjmd;
                    pjmd = sitem["SJDJ"].Contains("P12") ? 1.2 : pjmd;
                    pjmd = sitem["SJDJ"].Contains("P14") ? 1.4 : pjmd;
                    Gs = 0;
                    for (xd = 1; xd <= 6; xd++)
                    {
                        md = Conversion.Val(sitem["WSYL" + xd].Trim());
                        if (md >= pjmd)
                        {
                            Gs = Gs + 1;
                            sitem["SFBSS" + xd] = "1";
                            sitem["KYQD" + xd] = sitem["WSYL" + xd].Trim();
                        }
                        else
                        {
                            sitem["SFBSS" + xd] = "0";
                            sitem["KYQD" + xd] = (md + 0.1).ToString("F1");
                        }
                    }
                    if (Gs <= 3)
                    {
                        sitem["KS_HG"] = "0";
                        sitem["JCJG"] = "不合格";
                        mAllHg_fun = false;
                        mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        sitem["KS_HG"] = "1";
                        sitem["JCJG"] = "合格";
                        mAllHg_fun = true;
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
                    }
                    mAllHg_fun = (mAllHg_fun && sitem["JCJG"] == "合格");
                    return mAllHg_fun;
                };

            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_KS_DJ"];
            var MItem = data["M_KS"];
            var SItem = data["S_KS"];
            #endregion

            #region  计算开始
            string which = "1";
            MItem[0]["JCJGMS"] = "";
            string mhgzh = "";
            string mbhgzh = "";
            foreach (var sitem in SItem)
            {
                string mSz = "0";
                double mbzssgd = 0;
                mSjdj = sitem["SJDJ"];            //设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    mSz = mrsDj_Filter["SZ"];
                    mQdyq = GetSafeDouble(mrsDj_Filter["QDYQ"]);
                    mbzssgd = GetSafeDouble(mrsDj_Filter["GDYQ"]);
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mQdyq = 0;
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    break;
                }
                //计算龄期
                sitem["LQ"] = (GetSafeDateTime(MItem[0]["SYRQS"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString();
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    if (sjtabcalc(MItem[0], sitem, mAllHg))
                    {
                    }
                    continue;
                }
                vi = 0;
                vv = 0;
                if (which == "0")
                {
                    if (mbzssgd != 0)
                    {
                        if (GetSafeDouble(sitem["SSGD1"]) >= mbzssgd)
                            vi = vi + 1;
                        if (GetSafeDouble(sitem["SSGD2"]) >= mbzssgd)
                            vi = vi + 1;
                        if (GetSafeDouble(sitem["ssgd3"]) >= mbzssgd)
                            vi = vi + 1;
                        if (GetSafeDouble(sitem["SSGD4"]) >= mbzssgd)
                            vi = vi + 1;
                        if (GetSafeDouble(sitem["SSGD5"]) >= mbzssgd)
                            vi = vi + 1;
                        if (GetSafeDouble(sitem["SSGD6"]) >= mbzssgd)
                            vi = vi + 1;


                        if (vi >= 3)
                        {
                            sitem["KS_HG"] = "0";
                            sitem["JCJG"] = "不合格";
                            mAllHg = false;
                        }
                        else
                        {
                            sitem["KS_HG"] = "1";
                            sitem["JCJG"] = "合格";
                            mAllHg = true;
                        }
                    }
                }
                else
                {
                    if (Conversion.Val(sitem["KYQD1"]) > mQdyq || (Conversion.Val(sitem["KYQD1"]) >= mQdyq && sitem["SFBSS1"] == "1"))
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD2"]) > mQdyq || (Conversion.Val(sitem["KYQD2"]) >= mQdyq && sitem["SFBSS2"] == "1"))
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD3"]) > mQdyq || (Conversion.Val(sitem["KYQD3"]) >= mQdyq && sitem["SFBSS3"] == "1"))
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD4"]) > mQdyq || (Conversion.Val(sitem["KYQD4"]) >= mQdyq && sitem["SFBSS4"] == "1"))
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD5"]) > mQdyq || (Conversion.Val(sitem["KYQD5"]) >= mQdyq && sitem["SFBSS5"] == "1"))
                        vv = vv + 1;
                    if (Conversion.Val(sitem["KYQD6"]) > mQdyq || (Conversion.Val(sitem["KYQD6"]) >= mQdyq && sitem["SFBSS6"] == "1"))
                        vv = vv + 1;
                    if (vv <= 3)
                    {
                        sitem["KS_HG"] = "0";
                        sitem["JCJG"] = "不合格";
                        mAllHg = false;
                    }
                    else
                    {
                        sitem["KS_HG"] = "1";
                        sitem["JCJG"] = "合格";
                        mAllHg = true;
                    }
                }
                if (sitem["JCJG"] == "合格")
                {
                    if (mhgzh.Length > 0)
                        mhgzh = mhgzh + "、" + sitem["zh"].Trim();
                    else
                        mhgzh = sitem["zh"];
                }
                else
                {
                    if (mbhgzh.Length > 0)
                        mbhgzh = mbhgzh + "、" + sitem["zh"].Trim();
                    else
                        mbhgzh = sitem["zh"].Trim();
                }
            }
            //主表总判断赋值
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
