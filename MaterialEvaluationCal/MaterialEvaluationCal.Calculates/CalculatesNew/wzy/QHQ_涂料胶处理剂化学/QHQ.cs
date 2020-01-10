using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class QHQ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 参数定义
            String mcalBh;
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
            string which = "";
            #endregion

            #region  自定义函数
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
            var mrsDj = dataExtra["BZ_QHQ_DJ"];
            var MItem = data["M_QHQ"];
            var SItem = data["S_QHQ"];
            #endregion

            #region 计算开始
            MItem[0]["JCJGMS"] = "";
            zj1 = 0;
            zj2 = 0;
            foreach (var sitem in SItem)
            {
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(sitem["CPMC"]));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    MItem[0]["G_VOCS"] = string.IsNullOrEmpty(mrsDj_Filter["VOCS"]) ? "0" : mrsDj_Filter["VOCS"].Trim();
                    MItem[0]["G_YLJQHL"] = string.IsNullOrEmpty(mrsDj_Filter["YLJQHL"]) ? "0" : mrsDj_Filter["YLJQHL"].Trim();
                    MItem[0]["G_BHL"] = string.IsNullOrEmpty(mrsDj_Filter["BHL"]) ? "0" : mrsDj_Filter["BHL"].Trim();
                    MItem[0]["G_TDI"] = string.IsNullOrEmpty(mrsDj_Filter["TDI"]) ? "0" : mrsDj_Filter["TDI"].Trim();
                    which = string.IsNullOrEmpty(mrsDj_Filter["WHICH"]) ? "" : mrsDj_Filter["WHICH"].Trim();
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "试件尺寸为空";
                    break;
                }
                mbhggs = 0;
                string jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、VOCS检测、"))
                {
                    if (calc_pd(MItem[0]["G_VOCS"], sitem["VOCS"]) == "符合")
                    {
                        sitem["VOCSPD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["VOCSPD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sitem["VOCS"] = "----";
                    MItem[0]["G_VOCS"] = "----";
                    sitem["VOCSPD"] = "----";
                }
                if (jcxm.Contains("、甲醛检测、"))
                {
                    if (calc_pd(MItem[0]["G_YLJQHL"], sitem["YLJQHL"]) == "符合")
                    {
                        sitem["YLJQHLPD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["YLJQHLPD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sitem["YLJQHL"] = "----";
                    MItem[0]["G_YLJQHL"] = "----";
                    sitem["YLJQHLPD"] = "----";
                }
                if (jcxm.Contains("、TDI检测、"))
                {
                    if (calc_pd(MItem[0]["G_TDI"], sitem["TDI"]) == "符合")
                    {
                        sitem["TDIPD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sitem["TDIPD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sitem["TDI"] = "----";
                    MItem[0]["G_TDI"] = "----";
                    sitem["TDIPD"] = "----";
                }
                if (jcxm.Contains("、苯检测、"))
                {
                    if (calc_pd(MItem[0]["G_BHL"], sitem["BHL"]) == "符合")
                    {
                        sitem["BHLPD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {

                        sitem["BHLPD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sitem["BHL"] = "----";
                    MItem[0]["G_BHL"] = "----";
                    sitem["BHLPD"] = "----";
                }
                if (mbhggs > 0)
                    sitem["JCJG"] = "不合格";
                else
                    sitem["JCJG"] = "合格";
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");
            }
            //综合判断
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                if (mFlag_Bhg && mFlag_Hg)
                    MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
