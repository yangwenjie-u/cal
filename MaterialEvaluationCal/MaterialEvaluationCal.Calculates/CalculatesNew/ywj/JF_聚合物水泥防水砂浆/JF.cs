using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JF : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_JFDJB"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_JF"];

            if (!data.ContainsKey("M_JF"))
            {
                data["M_JF"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_JF"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mItemHg = true;
            var mSjdj = "";
            var mJSFF = "";
            var jcxm = "";

            double mYqpjz, mXdy21, mDy21 = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            bool sign = false;
            double md1, md2, md, sum, Gs, pjmd = 0;
            List<double> nArr = new List<double>();

            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mSjdj = string.IsNullOrEmpty(sItem["SJDJ"]) ? "" : sItem["SJDJ"];


                var mrsDj = extraDJ.FirstOrDefault(u => u["CPLX"] == sItem["CPLX"].Trim());

                if (mrsDj != null)
                {
                    sItem["G_CNSJ"] = mrsDj["CNSJ"];
                    sItem["G_ZNSJ"] = mrsDj["ZNSJ"];
                    sItem["G_KYQD"] = mrsDj["KYQD"];
                    sItem["G_KZQD"] = mrsDj["KZQD"];
                    sItem["G_NJQD_28"] = mrsDj["NJQDB_28"];
                    sItem["G_KSQD_7"] = mrsDj["KSYL_7"];
                    //MItem[0]["WHICH"] = "bgjf";
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "依据不详";
                    mAllHg = false;
                    continue;
                }

                mbhggs = 0;
                if (jcxm.Contains("、含水率、"))
                {
                    sItem["GH_HSL"] = IsQualified(sItem["G_HSL"], sItem["W_HSL"], false);

                    if (sItem["GH_HSL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["GH_HSL"] = "合格";
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_HSL"] = "----";
                    sItem["GH_HSL"] = "----";
                    sItem["G_HSL"] = "----";
                }
                if (jcxm.Contains("、氯离子含量、"))
                {
                    sItem["GH_LLZ"] = IsQualified(sItem["G_LLZ"], sItem["W_LLZ"], false);

                    if (sItem["GH_LLZ"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["GH_HSL"] = "合格";
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_LLZ"] = "----";
                    sItem["GH_LLZ"] = "----";
                    sItem["G_LLZ"] = "----";
                }

                if (jcxm.Contains("、细度、"))
                {
                    sItem["GH_XD"] = IsQualified(sItem["G_XD"], sItem["W_XD"], false);

                    if (sItem["GH_XD"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["GH_HSL"] = "合格";
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_XD"] = "----";
                    sItem["GH_XD"] = "----";
                    sItem["G_XD"] = "----";
                }

                if (jcxm.Contains("、凝结时间、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_CNSJ"], sItem["W_CNSJ"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_NJSJ"] = "合格";
                        sign = IsQualified(sItem["G_ZNSJ"], sItem["W_ZNSJ"], false) == "不合格" ? false : sign;
                        if (sign)
                        {
                            sItem["GH_NJSJ"] = "合格";
                        }
                        else
                        {
                            sItem["GH_NJSJ"] = "不合格";
                        }
                    }
                    else
                    {
                        sItem["GH_NJSJ"] = "不合格";
                    }

                    if (sItem["GH_NJSJ"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["G_CNSJ"] = "----";
                    sItem["W_CNSJ"] = "----";
                    sItem["G_ZNSJ"] = "----";
                    sItem["W_ZNSJ"] = "----";
                    sItem["GH_NJSJ"] = "----";
                }

                if (jcxm.Contains("、抗折强度、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_KZQD"], sItem["W_KZQD"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_KZQD"] = "合格";
                    }
                    else
                    {
                        sItem["GH_KZQD"] = "不合格";
                    }

                    if (sItem["GH_KZQD"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["G_KZQD"] = "----";
                    sItem["W_KZQD"] = "----";
                    sItem["GH_KZQD"] = "----";
                }

                if (jcxm.Contains("、抗压强度、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_KYQD"], sItem["W_KYQD"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_KYQD"] = "合格";
                    }
                    else
                    {
                        sItem["GH_KYQD"] = "不合格";
                    }

                    if (sItem["GH_KYQD"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["G_KYQD"] = "----";
                    sItem["W_KYQD"] = "----";
                    sItem["GH_KYQD"] = "----";
                }

                if (jcxm.Contains("、28d粘接强度、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_NJQD_28"], sItem["W_NJQD_28"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_NJQD_28"] = "合格";
                    }
                    else
                    {
                        sItem["GH_NJQD_28"] = "不合格";
                    }

                    if (sItem["GH_NJQD_28"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["G_NJQD_28"] = "----";
                    sItem["W_NJQD_28"] = "----";
                    sItem["GH_NJQD_28"] = "----";
                }

                if (jcxm.Contains("、7d抗渗压力、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_KSQD_7"], sItem["W_KSQD_7"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_KSQD_7"] = "合格";
                    }
                    else
                    {
                        sItem["GH_KSQD_7"] = "不合格";
                    }

                    if (sItem["GH_KSQD_7"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["G_KSQD_7"] = "----";
                    sItem["W_KSQD_7"] = "----";
                    sItem["GH_KSQD_7"] = "----";
                }


                if (jcxm.Contains("、减水率、"))
                {
                    sItem["GH_JSL"] = IsQualified(sItem["G_JSL"], sItem["W_JSL"], false);

                    if (sItem["GH_JSL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_JSL"] = "----";
                    sItem["GH_JSL"] = "----";
                    sItem["G_JSL"] = "----";
                }

                if (jcxm.Contains("、抗压强度比、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_KYQDB7"], sItem["W_KYQDB7"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_KYQDB28"], sItem["W_KYQDB28"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_KYQDB"] = "合格";
                    }
                    else
                    {
                        sItem["GH_KYQDB"] = "不合格";
                    }

                    if (sItem["GH_KYQDB"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["G_KYQDB7"] = "----";
                    sItem["W_KYQDB7"] = "----";
                    sItem["G_KYQDB28"] = "----";
                    sItem["W_KYQDB28"] = "----";
                    sItem["GH_KYQDB"] = "----";
                }

                if (jcxm.Contains("、含气量、"))
                {
                    sItem["GH_HQL"] = IsQualified(sItem["G_HQL"], sItem["W_HQL"], false);

                    if (sItem["GH_HQL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_HQL"] = "----";
                    sItem["GH_HQL"] = "----";
                    sItem["G_HQL"] = "----";
                }

                if (jcxm.Contains("、收缩率比、"))
                {
                    sItem["GH_SSLB"] = IsQualified(sItem["G_SSLB"], sItem["W_SSLB"], false);

                    if (sItem["GH_SSLB"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_SSLB"] = "----";
                    sItem["GH_SSLB"] = "----";
                    sItem["G_SSLB"] = "----";
                }

                if (jcxm.Contains("、施工性、"))
                {
                    if (sItem["W_SGX_1"].Trim() == "刮涂无障碍" && sItem["W_SGX_1"].Trim() == "刮涂无障碍")
                    {
                        sItem["GH_SGX"] = "合格";
                        mFlag_Hg = true;

                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_SGX_1"] = "----";
                    sItem["W_SGX_2"] = "----";
                    sItem["G_SGX_2"] = "----";
                    sItem["GH_SGX"] = "----";
                    sItem["G_SGX_1"] = "----";
                }


                if (jcxm.Contains("、湿基面粘结强度、"))
                {
                    sItem["GH_NJQD"] = IsQualified(sItem["G_NJQD"], sItem["W_NJQD"], false);

                    if (sItem["GH_NJQD"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_NJQD"] = "----";
                    sItem["GH_NJQD"] = "----";
                    sItem["G_NJQD"] = "----";
                }

                if (jcxm.Contains("、总碱量、"))
                {
                    sItem["GH_ZJL"] = IsQualified(sItem["G_ZJL"], sItem["W_ZJL"], false);

                    if (sItem["GH_ZJL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_ZJL"] = "----";
                    sItem["GH_ZJL"] = "----";
                    sItem["G_ZJL"] = "----";
                }

                if (jcxm.Contains("、砂浆抗渗性能、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_SJDTC"], sItem["W_SJDTCB"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_SJQTC"], sItem["W_SJQTCB"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_SJKS"] = "合格";
                    }
                    else
                    {
                        sItem["GH_SJKS"] = "不合格";
                    }

                    if (sItem["GH_SJKS"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_SJDTCB"] = "----";
                    sItem["W_SJQTCB"] = "----";
                    sItem["W_SJDTC"] = "----";
                    sItem["W_SJQTC"] = "----";
                    sItem["GH_SJKS"] = "----";
                    sItem["G_SJDTC"] = "----";
                    sItem["G_SJQTC"] = "----";
                }

                if (jcxm.Contains("、水泥基渗透结晶型防水涂料、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_TDTC"], sItem["W_TDTCB"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_TQTC"], sItem["W_TQTCB"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_TKS"] = "合格";
                    }
                    else
                    {
                        sItem["GH_TKS"] = "不合格";
                    }

                    if (sItem["GH_TKS"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_TDTCB"] = "----";
                    sItem["W_TQTCB"] = "----";
                    sItem["W_TDTC"] = "----";
                    sItem["W_TQTC"] = "----";
                    sItem["GH_TKS"] = "----";
                    sItem["G_TDTC"] = "----";
                    sItem["G_TQTC"] = "----";
                }

                if (jcxm.Contains("、混凝土抗渗性能56d、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_ECKSYL"], sItem["W_ECKSYL"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_ECKSYL"] = "合格";
                    }
                    else
                    {
                        sItem["GH_ECKSYL"] = "不合格";
                    }

                    if (sItem["GH_ECKSYL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_ECKSYL"] = "----";
                    sItem["GH_ECKSYL"] = "----";
                    sItem["G_ECKSYL"] = "----";
                }

                if (jcxm.Contains("、混凝土抗渗性能28d、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_TKSYLB"], sItem["W_TKSYLB"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_KSYL"] = "合格";
                    }
                    else
                    {
                        sItem["GH_KSYL"] = "不合格";
                    }

                    if (sItem["GH_KSYL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_TKSYLB"] = "----";
                    sItem["W_TKSYL"] = "----";
                    sItem["GH_KSYL"] = "----";
                    sItem["G_TKSYLB"] = "----";
                }

                if (jcxm.Contains("、混凝土抗渗性能56d、"))
                {
                    sign = true;
                    sign = IsQualified(sItem["G_TKSYLB_2"], sItem["W_TKSYLB_2"], false) == "不合格" ? false : sign;

                    if (sign)
                    {
                        sItem["GH_KSYL1"] = "合格";
                    }
                    else
                    {
                        sItem["GH_KSYL1"] = "不合格";
                    }

                    if (sItem["GH_KSYL1"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["W_TKSYLB_2"] = "----";
                    sItem["W_TKSYL_2"] = "----";
                    sItem["GH_KSYL1"] = "----";
                    sItem["G_TKSYLB_2"] = "----";
                }

                if (mbhggs == 0)
                {
                    jsbeizhu = "该组试件所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                    sItem["JCJG"] = "合格";
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "该组试件不符合" + MItem[0]["PDBZ"] + "标准要求。";
                    if (mFlag_Bhg && mFlag_Hg)
                    {
                        jsbeizhu = "该组试件所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";

                    }
                }
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
        }
    }
}

