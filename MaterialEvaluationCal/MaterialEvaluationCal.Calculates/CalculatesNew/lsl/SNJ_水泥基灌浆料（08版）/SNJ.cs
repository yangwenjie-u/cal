using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class SNJ : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_SNJ"];
            var MItem = data["M_SNJ"];
            var extraDJ = dataExtra["BZ_SNJ_DJ"];
            if (!data.ContainsKey("M_SNJ"))
            {
                data["M_SNJ"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            bool mFlag_Hg = false, mFlag_Bhg = false;

            foreach (var sItem in SItem)
            {
                double md1, md2, md3, xd1, xd2, xd3, md, pjmd, sum, mSjcc, mSjcc1, mMj, mHsxs;
                bool flag = true, sign = true, mark = true;
                int mbhggs = 0;
                string mJSFF;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["CPMC"] == sItem["CPMC"].Trim() && u["XH"] == sItem["XH"]);

                if (mrsDj != null)
                {
                    sItem["G_ZDJLLJ"] = mrsDj["ZDJLLJ"];
                    sItem["G_LDDCSZ"] = mrsDj["LDDCSZ"];
                    sItem["G_LDDBLZ"] = mrsDj["LDDBLZ"];
                    sItem["G_TLDCSZ"] = mrsDj["TLDCSZ"];
                    sItem["G_TLDBLZ"] = mrsDj["TLDBLZ"];
                    sItem["G_TLKZDC"] = mrsDj["TLKZDC"];
                    sItem["G_TLKZDB"] = mrsDj["TLKZDB"];
                    sItem["G_KYQD1"] = mrsDj["KYQD1"];
                    sItem["G_KYQD3"] = mrsDj["KYQD3"];
                    sItem["G_KYQD28"] = mrsDj["KYQD28"];
                    sItem["G_PZL3"] = mrsDj["PZL3"];
                    sItem["G_PZL24"] = mrsDj["PZL24"];
                    sItem["G_MSL"] = mrsDj["MSL"];
                    sItem["G_SJB"] = mrsDj["SJB"];
                    sItem["G_CNSJ"] = mrsDj["CNSJ"];
                    sItem["G_ZNSJ"] = mrsDj["ZNSJ"];
                    sItem["G_LDD60"] = mrsDj["LDD60"];
                    sItem["G_MSL24"] = mrsDj["MSL24"];
                    sItem["G_MSL3"] = mrsDj["MSL3"];
                    sItem["G_YLMSL"] = mrsDj["YLMSL"];
                    sItem["G_KYQD7"] = mrsDj["KYQD7"];
                    sItem["G_KZQD3"] = mrsDj["KZQD3"];
                    sItem["G_KZQD7"] = mrsDj["KZQD7"];
                    sItem["G_KZQD28"] = mrsDj["KZQD28"];
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "依据不详";
                    continue;
                }

                //跳转
                if (!string.IsNullOrEmpty(mItem["SJTABS"]))
                {
                    if (sItem["CPMC"] == "后张预应力孔道压浆浆液")
                    {
                        if (jcxm.Contains("、水胶比、"))
                        {
                            sItem["GH_SJB"] = IsQualified(sItem["G_SJB"], sItem["SJB"], false);
                            mbhggs = sItem["GH_SJB"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["GH_SJB"] == "合格")mFlag_Hg = true;
                            else mFlag_Bhg =true;
                        }
                        else
                        {
                            sItem["G_SJB"] = "----";
                            sItem["GH_SJB"] = "----";
                            sItem["SJB"] = "----";
                        }

                        if (jcxm.Contains("、凝结时间、"))
                        {
                            sign = true;
                            string cnsj = IsQualified(sItem["G_CNSJ"], sItem["CNSJ"], true);
                            sign =  cnsj == "不符合" ? false : sign;
                            string cnsj1 = IsQualified(sItem["G_ZNSJ"], sItem["ZNSJ"], true);
                            sign = cnsj1 == "不符合" ? false : sign;
                            sItem["GH_NJSJ"] = sign?"合格": "不合格";
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_CNSJ"] = "----";
                            sItem["CNSJ"] = "----";
                            sItem["G_ZNSJ"] = "----";
                            sItem["ZNSJ"] = "----";
                            sItem["GH_NJSJ"] = "----";
                        }

                        if (jcxm.Contains("、流动度、"))
                        {
                            sign = true;
                            string cnsj = IsQualified(sItem["G_LDDCSZ"], sItem["LDDCSZ"], true);
                            sign = cnsj == "不符合" ? false : sign;

                            string cnsj1 = IsQualified(sItem["G_LDDBLZ"], sItem["LDDBLZ"], true);
                            sign = cnsj1 == "不符合" ? false : sign;

                            string cnsj2 = IsQualified(sItem["G_LDD60"], sItem["LDD60"], true);
                            sign = cnsj2 == "不符合" ? false : sign;

                            sItem["HG_LDD"] = sign ? "合格" : "不合格";
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["HG_LDD"] = "----";
                            sItem["G_LDDCSZ"] = "----";
                            sItem["G_LDDBLZ"] = "----";
                            sItem["G_LDD60"] = "----";
                            sItem["LDDCSZ"] = "----";
                            sItem["LDDBLZ"] = "----";
                            sItem["LDD60"] = "----";
                        }

                        if (jcxm.Contains("、压力泌水率、"))
                        {
                            sItem["GH_YLMSL"] = IsQualified(sItem["G_YLMSL"], sItem["YLMSL"], false);
                            mbhggs = sItem["GH_YLMSL"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["GH_YLMSL"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_YLMSL"] = "----";
                            sItem["GH_YLMSL"] = "----";
                            sItem["YLMSL"] = "----";
                        }

                        if (jcxm.Contains("、泌水率、"))
                        {
                            flag = true;
                            sign = true;
                            string cnsj = IsQualified(sItem["G_MSL3"], sItem["MSL3"], true);
                            sign = cnsj == "不符合" ? false : sign;
                            string cnsj1 = IsQualified(sItem["G_MSL24"], sItem["MSL24"], true);
                            sign = cnsj1 == "不符合" ? false : sign;
                            sItem["HG_MSL"] = sign ? "合格" : "不合格";
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_MSL3"] = "----";
                            sItem["G_MSL24"] = "----";
                            sItem["HG_MSL"] = "----";
                            sItem["MSL3"] = "----";
                            sItem["MSL24"] = "----";
                        }

                        if (jcxm.Contains("、自由膨胀率、"))
                        {
                            flag = true;
                            sign = true;
                            string cnsj = IsQualified(sItem["G_PZL3"], sItem["PZL3"], true);
                            sign = cnsj == "不符合" ? false : sign;
                            string cnsj1 = IsQualified(sItem["G_PZL24"], sItem["PZL24"], true);
                            sign = cnsj1 == "不符合" ? false : sign;
                            sItem["HG_PZL"] = sign ? "合格" : "不合格";
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_PZL3"] = "----";
                            sItem["G_PZL24"] = "----";
                            sItem["HG_PZL"] = "----";
                            sItem["PZL3"] = "----";
                            sItem["PZL24"] = "----";
                        }

                        if (jcxm.Contains("、3天抗压强度、"))
                        {
                            sItem["HG_KYQD3"] = IsQualified(sItem["G_KYQD3"], sItem["KYPJ_3"], false);
                            mbhggs = sItem["HG_KYQD3"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["HG_KYQD3"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["HG_KYQD3"] = "----";
                            sItem["G_KYQD3"] = "----";
                            sItem["KYPJ_3"] = "----";
                        }

                        if (jcxm.Contains("、7天抗压强度、"))
                        {
                            sItem["HG_KYQD7"] = IsQualified(sItem["G_KYQD7"], sItem["KYPJ_7"], false);
                            mbhggs = sItem["HG_KYQD7"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["HG_KYQD7"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["HG_KYQD7"] = "----";
                            sItem["KYPJ_7"] = "----";
                            sItem["G_KYQD7"] = "----";
                        }

                        if (jcxm.Contains("、28天抗压强度、"))
                        {
                            sItem["HG_KYQD28"] = IsQualified(sItem["G_KYQD28"], sItem["KYPJ"], false);
                            mbhggs = sItem["HG_KYQD28"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["HG_KYQD28"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["HG_KYQD28"] = "----";
                            sItem["G_KYQD28"] = "----";
                            sItem["KYPJ"] = "----";
                        }

                        if (jcxm.Contains("、3天抗折强度、"))
                        {
                            sItem["GH_KZQD3"] = IsQualified(sItem["G_KZQD3"], sItem["KZQD3"], false);
                            mbhggs = sItem["GH_KZQD3"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["GH_KZQD3"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["GH_KZQD3"] = "----";
                            sItem["G_KZQD3"] = "----";
                            sItem["KZQD3"] = "----";
                        }

                        if (jcxm.Contains("、7天抗折强度、"))
                        {
                            sItem["GH_KZQD7"] = IsQualified(sItem["G_KZQD7"], sItem["KZQD7"], false);
                            mbhggs = sItem["GH_KZQD7"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["GH_KZQD7"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["GH_KZQD7"] = "----";
                            sItem["G_KZQD7"] = "----";
                            sItem["KZQD7"] = "----";
                        }

                        if (jcxm.Contains("、28天抗折强度、"))
                        {
                            sItem["GH_KZQD28"] = IsQualified(sItem["G_KZQD28"], sItem["KZQD28"], false);
                            mbhggs = sItem["GH_KZQD28"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["GH_KZQD28"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["GH_KZQD28"] = "----";
                            sItem["G_KZQD28"] = "----";
                            sItem["KZQD28"] = "----";
                        }

                        if (jcxm.Contains("、对钢筋锈蚀作用、"))
                        {
                            sItem["HG_XSZY"] = sItem["XSZY"] == "无" ? "合格" : "不合格";
                            mbhggs = sItem["HG_XSZY"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["HG_XSZY"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_XSZY"] = "----";
                            sItem["XSZY"] = "----";
                            sItem["HG_XSZY"] = "----";
                        }

                        if (jcxm.Contains("、充盈度、"))
                        {
                            sItem["GH_CYD"] = sItem["CYD"] == "无" ? "合格" : "不合格";
                            mbhggs = sItem["HG_XSZY"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["HG_XSZY"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_CYD"] = "----";
                            sItem["CYD"] = "----";
                            sItem["GH_CYD"] = "----";
                        }
                    }
                    else
                    {
                        if (jcxm.Contains("、泌水率、"))
                        {
                            flag = true;
                            sign = true;
                            string cnsj = IsQualified(sItem["G_MSL"], sItem["MSL"], true);
                            sign = cnsj == "不符合" ? false : sign;
                            sItem["HG_MSL"] = sign ? "合格" : "不合格";
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_MSL"] = "----";
                            sItem["HG_MSL"] = "----";
                            sItem["MSL"] = "----";
                        }

                        if (jcxm.Contains("、竖向膨胀率、"))
                        {
                            flag = true;
                            sign = true;
                            string cnsj = IsQualified(sItem["G_PZL3"], sItem["PZL3"], true);
                            sign = cnsj == "不符合" ? false : sign;
                            string cnsj1 = IsQualified(sItem["G_PZL24"], sItem["PZL24"], true);
                            sign = cnsj1 == "不符合" ? false : sign;
                            sItem["HG_PZL"] = sign ? "合格" : "不合格";
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_PZL3"] = "----";
                            sItem["G_PZL24"] = "----";
                            sItem["HG_PZL"] = "----";
                            sItem["PZL3"] = "----";
                            sItem["PZL24"] = "----";
                        }

                        if (jcxm.Contains("、流动度、"))
                        {
                            sign = true;
                            string cnsj = IsQualified(sItem["G_LDDCSZ"], sItem["LDDCSZ"], true);
                            sign = cnsj == "不符合" ? false : sign;
                            string cnsj1 = IsQualified(sItem["G_LDDBLZ"], sItem["LDDBLZ"], true);
                            sign = cnsj1 == "不符合" ? false : sign;
                            sItem["HG_PZL"] = sign ? "合格" : "不合格";
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_LDDCSZ"] = "----";
                            sItem["LDDCSZ"] = "----";
                            sItem["G_LDDBLZ"] = "----";
                            sItem["LDDBLZ"] = "----";
                            sItem["HG_LDD"] = "----";
                        }

                        if (jcxm.Contains("、坍落度、"))
                        {
                            flag = true;
                            sign = true;
                            string cnsj = IsQualified(sItem["G_TLDCSZ"], sItem["TLDCSZ"], false);
                            sign = cnsj == "不合格" ? false : sign;
                            string cnsj1 = IsQualified(sItem["G_TLDBLZ"], sItem["TLDBLZ"], false);
                            sign = cnsj1 == "不合格" ? false : sign;

                            string cnsj2 = IsQualified(sItem["G_TLDCSZ"], sItem["TLDCSZ"], false);
                            flag = cnsj2 == "----" ? false : sign;
                            string cnsj3 = IsQualified(sItem["G_TLDBLZ"], sItem["TLDBLZ"], false);
                            flag = cnsj3 == "----" ? false : sign;
                            if (sign)
                            {
                                sItem["HG_TLD"] = flag ? "合格" : "----";
                            }
                            else
                            {
                                sItem["HG_TLD"] = "不合格";
                                mbhggs = mbhggs + 1;
                            }

                            flag = true;
                            sign = true;
                            string cnsj4 = IsQualified(sItem["G_TLKZDC"], sItem["TLKZDC"], false);
                            sign = cnsj4 == "不合格" ? false : sign;
                            string cnsj5 = IsQualified(sItem["G_TLKZDB"], sItem["TLKZDB"], false);
                            sign = cnsj5 == "不合格" ? false : sign;

                            string cnsj6 = IsQualified(sItem["G_TLKZDC"], sItem["TLKZDC"], false);
                            flag = cnsj6 == "----" ? false : sign;
                            string cnsj7 = IsQualified(sItem["G_TLKZDB"], sItem["TLKZDB"], false);
                            flag = cnsj7 == "----" ? false : sign;
                            if (sign)
                            {
                                sItem["HG_TLKZD"] = flag ? "合格" : "----";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["HG_TLKZD"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                            }
                           
                        }
                        else
                        {
                            sItem["G_TLDCSZ"] = "----";
                            sItem["HG_TLD"] = "----";
                            sItem["G_TLDBLZ"] = "----";
                            sItem["TLDCSZ"] = "----";
                            sItem["TLDBLZ"] = "----";

                            sItem["G_TLKZDC"] = "----";
                            sItem["G_TLKZDB"] = "----";
                            sItem["TLKZDC"] = "----";
                            sItem["TLKZDB"] = "----";
                            sItem["HG_TLKZD"] = "----";
                        }

                        if (jcxm.Contains("、对钢筋锈蚀作用、"))
                        {
                            sItem["HG_XSZY"] = sItem["XSZY"] == "无" ? "合格" : "不合格";
                            mbhggs = sItem["HG_XSZY"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (sItem["HG_XSZY"] == "合格") mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["HG_XSZY"] = "----";
                            sItem["XSZY"] = "----";
                            sItem["G_XSZY"] = "----";
                        }

                        if (jcxm.Contains("、最大集料粒径、"))
                        {
                            if (sItem["xh"].Contains("Ⅳ类"))
                            {
                                string cnsj = IsQualified("＞4.75", sItem["ZDJLLJ"], true);
                                string cnsj1 = IsQualified("≤16", sItem["ZDJLLJ"], true);
                                if (cnsj == "符合" && cnsj1== "符合")
                                {
                                    sItem["HG_ZDJLLJ"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    sItem["HG_ZDJLLJ"] = "不合格";
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                            else
                            {
                                string cnsj = IsQualified("G_ZDJLLJ", sItem["ZDJLLJ"], true);
                                if (cnsj == "符合")
                                {
                                    sItem["HG_ZDJLLJ"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    sItem["HG_ZDJLLJ"] = "不合格";
                                    mbhggs++;
                                    mFlag_Bhg = true;
                                }
                            }
                        }
                        else
                        {
                            sItem["G_ZDJLLJ"] = "----";
                            sItem["HG_ZDJLLJ"] = "----";
                            sItem["ZDJLLJ"] = "----";
                        }

                        if (jcxm.Contains("、1天抗压强度、"))
                        {
                            flag = true;
                            sign = true;
                            string kypj = IsQualified(sItem["G_KYQD1"], sItem["KYPJ_1"], true);
                            sign = kypj == "不符合" ? false : true;
                            sItem["HG_KYQD1"] = sign ? "合格" : "不合格";
                            mbhggs = sign ? mbhggs : ++mbhggs;
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_KYQD1"] = "----";
                            sItem["HG_KYQD1"] = "----";
                            sItem["KYPJ_1"] = "----";
                        }

                        if (jcxm.Contains("、3天抗压强度、"))
                        {
                            flag = true;
                            sign = true;
                            string kypj = IsQualified(sItem["G_KYQD3"], sItem["KYPJ_3"], true);
                            sign = kypj == "不符合" ? false : true;
                            sItem["HG_KYQD3"] = sign ? "合格" : "不合格";
                            mbhggs = sign ? mbhggs : ++mbhggs;
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["G_KYQD3"] = "----";
                            sItem["HG_KYQD3"] = "----";
                            sItem["KYPJ_3"] = "----";
                        }

                        if (jcxm.Contains("、28天抗压强度、"))
                        {
                            flag = true;
                            sign = true;
                            string kypj = IsQualified(sItem["G_KYQD28"], sItem["KYPJ"], true);
                            sign = kypj == "不符合" ? false : true;
                            sItem["HG_KYQD28"] = sign ? "合格" : "不合格";
                            mbhggs = sign ? mbhggs : ++mbhggs;
                            if (sign) mFlag_Hg = true;
                            else mFlag_Bhg = true;
                        }
                        else
                        {
                            sItem["HG_KYQD28"] = "----";
                            sItem["G_KYQD28"] = "----";
                            sItem["KYPJ"] = "----";
                        }
                    }

                    if (mbhggs == 0)
                    {
                        sItem["JCJG"] = "合格";
                    }
                    else
                    {
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                    }
                    continue;
                }

                if (jcxm.Contains("、最大集料粒径、"))
                {
                    if (sItem["xh"].Contains("Ⅳ类"))
                    {
                        var zdjllj = IsQualified("＞4.75", sItem["ZDJLLJ"], true);
                        var zdjllj1 = IsQualified("≤16", sItem["ZDJLLJ"], true);
                        if (zdjllj == "符合" && zdjllj1 == "符合")
                        {
                            sItem["HG_ZDJLLJ"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["HG_ZDJLLJ"] = "不合格";
                            mbhggs++;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        var zdjllj1 = IsQualified(sItem["G_ZDJLLJ"], sItem["ZDJLLJ"], true);
                        if (zdjllj1 == "符合")
                        {
                            sItem["HG_ZDJLLJ"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["HG_ZDJLLJ"] = "不合格";
                            mbhggs++;
                            mFlag_Bhg = true;
                        }
                    }
                }
                else
                {
                    sItem["G_ZDJLLJ"] = "----";
                    sItem["HG_ZDJLLJ"] = "----";
                    sItem["ZDJLLJ"] = "----";
                }

                if (jcxm.Contains("、流动度、"))
                {
                    sign = true;
                    flag = true;
                    sign = IsQualified(sItem["G_LDDCSZ"], sItem["LDDCSZ"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_LDDBLZ"], sItem["LDDBLZ"], false) == "不合格" ? false : sign;
                    flag = IsQualified(sItem["G_LDDCSZ"], sItem["LDDCSZ"], false) == "----" ? false : flag;
                    flag = IsQualified(sItem["G_LDDBLZ"], sItem["LDDBLZ"], false) == "----" ? false : flag;
                    if (sign)
                    {
                        sItem["HG_LDD"] = flag ? "合格" : "----";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["HG_LDD"] = "不合格";
                        mbhggs++;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["G_LDDCSZ"] = "----";
                    sItem["HG_LDD"] = "----";
                    sItem["G_LDDBLZ"] = "----";
                    sItem["LDDCSZ"] = "----";
                    sItem["LDDBLZ"] = "----";
                }

                if (jcxm.Contains("、坍落度、"))
                {
                    sign = true;
                    flag = true;
                    sign = IsQualified(sItem["G_TLDCSZ"], sItem["TLDCSZ"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_TLDBLZ"], sItem["TLDBLZ"], false) == "不合格" ? false : sign;
                    flag = IsQualified(sItem["G_TLDCSZ"], sItem["TLDCSZ"], false) == "----" ? false : flag;
                    flag = IsQualified(sItem["G_TLDBLZ"], sItem["TLDBLZ"], false) == "----" ? false : flag;
                    if (sign)
                    {
                        sItem["HG_TLD"] = flag ? "合格" : "----";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["HG_TLD"] = "不合格";
                        mbhggs++;
                        mFlag_Bhg = true;
                    }

                    sign = true;
                    flag = true;
                    sign = IsQualified(sItem["G_TLKZDC"], sItem["TLKZDC"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_TLKZDB"], sItem["TLKZDB"], false) == "不合格" ? false : sign;
                    flag = IsQualified(sItem["G_TLKZDC"], sItem["TLKZDC"], false) == "----" ? false : flag;
                    flag = IsQualified(sItem["G_TLKZDB"], sItem["TLKZDB"], false) == "----" ? false : flag;
                    if (sign)
                    {
                        sItem["HG_TLKZD"] = flag ? "合格" : "----";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["HG_TLKZD"] = "不合格";
                        mbhggs++;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["G_TLDCSZ"] = "----";
                    sItem["HG_TLD"] = "----";
                    sItem["G_TLDBLZ"] = "----";
                    sItem["TLDCSZ"] = "----";
                    sItem["TLDBLZ"] = "----";

                    sItem["G_TLKZDC"] = "----";
                    sItem["G_TLKZDB"] = "----";
                    sItem["TLKZDC"] = "----";
                    sItem["TLKZDB"] = "----";
                    sItem["HG_TLKZD"] = "----";
                }

                if (jcxm.Contains("、竖向膨胀率、"))
                {
                    sign = true;
                    flag = true;
                    sign = IsQualified(sItem["G_PZL24"], sItem["PZL24"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_PZL3"], sItem["PZL3"], false) == "不合格" ? false : sign;
                    flag = IsQualified(sItem["G_PZL24"], sItem["PZL24"], false) == "----" ? false : flag;
                    flag = IsQualified(sItem["G_PZL3"], sItem["PZL3"], false) == "----" ? false : flag;
                    if (sign)
                    {
                        sItem["HG_PZL"] = flag ? "合格" : "----";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["HG_PZL"] = "不合格";
                        mbhggs++;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["G_PZL3"] = "----";
                    sItem["HG_PZL"] = "----";
                    sItem["G_PZL24"] = "----";
                    sItem["PZL3"] = "----";
                    sItem["PZL24"] = "----";
                }

                if (jcxm.Contains("、1天抗压强度、"))
                {
                    double mmj;
                    if (sItem["SJCC"].Trim() == "40")
                    {
                        mmj = 0.625;
                        for (int i = 0; i <= 6; i++)
                        {
                            sItem["KYQD" + i + "_1"] = Round(Conversion.Val(sItem["KYHZ" + i + "_1"]) * mmj, 1).ToString();
                        }
                        string mlongStr = sItem["KYQD1_1"] + "," + sItem["KYQD2_1"] + "," + sItem["KYQD3_1"] + "," + sItem["KYQD4_1"] + "," + sItem["KYQD5_1"] + "," + sItem["KYQD6_1"];
                        string[] mtmpArray = mlongStr.Split(',');
                        List<double> mtmpList = new List<double>();
                        foreach (string str in mtmpArray)
                        {
                            mtmpList.Add(Conversion.Val(str));
                        }
                        mtmpList.Sort();
                        double mMaxKyqd = mtmpList[5];
                        double mMinKyqd = mtmpList[0];
                        double mAvgKyqd = mtmpList.Average();
                        int mccgs = 0;
                        for (int i = 0; i <= 5; i++)
                        {
                            if (mtmpList[i] - mAvgKyqd > mAvgKyqd * 0.1)
                            {
                                mccgs = mccgs + 1;
                            }
                        }
                        if (mccgs > 1)
                        {
                            mAvgKyqd = -1;
                        }
                        else
                        {
                            if (mAvgKyqd - mtmpList[0] >= (mAvgKyqd - mtmpList[5]))
                            {
                                if (mAvgKyqd - mtmpList[0] > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = (mtmpList[1] + mtmpList[2] + mtmpList[3] + mtmpList[4] + mtmpList[5]) / 5;
                                }
                                if (mAvgKyqd - mtmpList[1] > mAvgKyqd * 0.1 || mAvgKyqd - mtmpList[5] > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = -1;
                                }
                            }
                            else
                            {
                                if (mAvgKyqd - mtmpList[5] > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = (mtmpList[1] + mtmpList[2] + mtmpList[3] + mtmpList[4] + mtmpList[0]) / 5;
                                }
                                if (mAvgKyqd - mtmpList[0] > mAvgKyqd * 0.1 || mAvgKyqd - mtmpList[4] > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = -1;
                                }
                            }
                        }
                        if (mAvgKyqd == -1)
                        {
                            sItem["KYPJ_1"] = "作废";
                        }
                        else
                        {
                            sItem["KYPJ_1"] = Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        mmj = 10000;
                        mHsxs = 0.95;
                        sItem["KYQD1_1"] = Round(1000 * (Conversion.Val(sItem["KYHZ1_1"]) / mmj), 1).ToString();
                        sItem["KYQD2_1"] = Round(1000 * (Conversion.Val(sItem["KYHZ2_1"]) / mmj), 1).ToString();
                        sItem["KYQD3_1"] = Round(1000 * (Conversion.Val(sItem["KYHZ3_1"]) / mmj), 1).ToString();
                        string mlongStr = sItem["KYQD1_1"] + "," + sItem["KYQD2_1"] + "," + sItem["KYQD3_1"];
                        string[] mtmpArray = mlongStr.Split(',');
                        List<double> mtmpList = new List<double>();
                        foreach (string str in mtmpArray)
                        {
                            mtmpList.Add(Conversion.Val(str));
                        }
                        mtmpList.Sort();
                        double mMaxKyqd = mtmpList[5];
                        double mMinKyqd = mtmpList[3];
                        double mMidKyqd = mtmpList[4];
                        double mAvgKyqd = Round((mMaxKyqd + mMinKyqd + mMidKyqd) / 3, 1);
                        if (mMidKyqd != 0)
                        {
                            md = 0;
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["KYPJ"] = "无效";
                            }
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                            {
                                md = mMidKyqd;
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                            {
                                md = mMidKyqd;
                            }
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                            {
                                md = mAvgKyqd;
                            }
                            if (md != 0)
                            {
                                if (Round(md, 0) <= 55)
                                {
                                    mHsxs = 0.95;
                                } else if (Round(md, 0) <= 65)
                                    mHsxs = 0.94;
                                else if (Round(md, 0) <= 75)
                                    mHsxs = 0.93;
                                else if (Round(md, 0) <= 85)
                                    mHsxs = 0.92;
                                else if (Round(md, 0) <= 95)
                                    mHsxs = 0.91;
                                else if (Round(md, 0) >= 96)
                                    mHsxs = 0.9;
                                md = md * mHsxs;
                                md = Round(md, 1);
                                sItem["KYPJ_1"] = md.ToString();
                            }
                        }
                    }
                    sItem["HG_KYQD1"] = IsQualified(sItem["G_KYQD1"], sItem["KYPJ_1"], false);
                    mbhggs = sItem["HG_KYQD1"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (sItem["HG_KYQD1"] == "合格")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sItem["KYPJ_1"] = "----";
                    sItem["HG_KYQD1"] = "----";
                    sItem["G_KYQD1"] = "----";
                }

                if (jcxm.Contains("、3天抗压强度、"))
                {
                    double mmj, mccgs;
                    if ( "40" == sItem["SJCC"])
                    {
                        mmj = 0.625;
                        for (int i = 1; i <= 6; i++)
                        {
                            sItem["KYQD" + i + "_3"] = Round(Conversion.Val(sItem["KYHZ" + i + "_3"]) * mmj, 1).ToString();
                        }
                        string mlongStr = sItem["KYQD1_3"] + "," + sItem["KYQD2_3"] + "," + sItem["KYQD3_3"] + "," + sItem["KYQD4_3"] + "," + sItem["KYQD5_3"] + "," + sItem["KYQD6_3"];
                        string[] mtmpArray = mlongStr.Split(',');
                        List<double> mtmpList = new List<double>();
                        foreach (string str in mtmpArray)
                        {
                            mtmpList.Add(Conversion.Val(str));
                        }
                        mtmpList.Sort();
                        double mMaxKyqd = mtmpList[5];
                        double mMinKyqd = mtmpList[0];
                        double mAvgKyqd = mtmpList.Average();
                        mccgs = 0;
                        for (int i = 0; i <= 5; i++)
                        {
                            if ((mtmpList[i] - mAvgKyqd) > mAvgKyqd * 0.1)
                            {
                                mccgs = mccgs + 1;
                            }
                        }
                        if (mccgs > 1)
                        {
                            mAvgKyqd = -1;
                        }
                        else
                        {
                            if (System.Math.Abs(mAvgKyqd - mtmpList[0]) >= System.Math.Abs(mAvgKyqd - mtmpList[5]))
                            {
                                if (System.Math.Abs(mAvgKyqd - mtmpList[0]) > (mAvgKyqd * 0.1))
                                    mAvgKyqd = (mtmpList[1] + mtmpList[2] + mtmpList[3] + mtmpList[4] + mtmpList[5]) / 5;
                                if (System.Math.Abs(mAvgKyqd - mtmpList[1]) > (mAvgKyqd * 0.1) || System.Math.Abs(mAvgKyqd - mtmpList[5]) > (mAvgKyqd * 0.1))
                                    mAvgKyqd = -1;
                            }
                            else
                            {
                                if (System.Math.Abs(mAvgKyqd - mtmpList[5]) > (mAvgKyqd * 0.1))
                                    mAvgKyqd = (mtmpList[1] + mtmpList[2] + mtmpList[3] + mtmpList[4] + mtmpList[0]) / 5;
                                if (System.Math.Abs(mAvgKyqd - mtmpList[0]) > (mAvgKyqd * 0.1) || System.Math.Abs(mAvgKyqd - mtmpList[4]) > (mAvgKyqd * 0.1))
                                    mAvgKyqd = -1;
                            }
                        }
                        if (mAvgKyqd == -1)
                            sItem["KYPJ_3"] = "作废";
                        else
                            sItem["KYPJ_3"] = Round(mAvgKyqd, 1).ToString();
                    }
                    else
                    {
                        mmj = 10000;
                        mHsxs = 0.95;
                        sItem["KYQD1_3"] = Round(1000 * (Conversion.Val(sItem["KYHZ1_3"]) / mmj), 1).ToString();
                        sItem["KYQD2_3"] = Round(1000 * (Conversion.Val(sItem["KYHZ2_3"]) / mmj), 1).ToString();
                        sItem["KYQD3_3"] = Round(1000 * (Conversion.Val(sItem["KYHZ3_3"]) / mmj), 1).ToString();
                        string mlongStr = sItem["KYQD1_3"] + "," + sItem["KYQD2_3"] + "," + sItem["KYQD3_3"];
                        string[] mtmpArray = mlongStr.Split(',');
                        List<double> mtmpList = new List<double>();
                        foreach (string str in mtmpArray)
                        {
                            mtmpList.Add(Conversion.Val(str));
                        }
                        mtmpList.Sort();
                        double mMaxKyqd = mtmpList[5];
                        double mMinKyqd = mtmpList[3];
                        double mMidKyqd = mtmpList[4];
                        double mAvgKyqd = Round((mMaxKyqd + mMinKyqd + mMidKyqd) / 3, 1);
                        if (mMidKyqd != 0)
                        {
                            md = 0;
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                sItem["KYPJ"] = "无效";
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                md = mMidKyqd;
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                md = mMidKyqd;
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                md = mAvgKyqd;
                            if (md != 0)
                            {
                                if (Round(md, 0) <= 55)
                                    mHsxs = 0.95;
                                else if (Round(md, 0) <= 65)
                                    mHsxs = 0.94;
                                else if (Round(md, 0) <= 75)
                                    mHsxs = 0.93;
                                else if (Round(md, 0) <= 85)
                                    mHsxs = 0.92;
                                else if (Round(md, 0) <= 95)
                                    mHsxs = 0.91;
                                else if (Round(md, 0) >= 96)
                                    mHsxs = 0.9;
                                md = md * mHsxs;
                                md = Round(md, 1);
                                sItem["KYPJ_3"] = md.ToString();
                            }
                        }
                    }
                    sItem["HG_KYQD3"] = IsQualified(sItem["G_KYQD3"], sItem["KYPJ_3"], false);
                    mbhggs = sItem["HG_KYQD3"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (sItem["HG_KYQD3"] == "合格")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sItem["KYPJ_3"] = "----";
                    sItem["HG_KYQD3"] = "----";
                    sItem["G_KYQD3"] = "----";
                }

                if (jcxm.Contains("、28天抗压强度、"))
                {
                    double mmj, mccgs;
                    if (sItem["SJCC"] == "40")
                    {
                        mmj = 0.625;
                        for (int i = 1; i <= 6; i++)
                        {
                            sItem["KYQD" + i] = Round(Conversion.Val(sItem["KYHZ" + i]) * mmj, 1).ToString();
                        }
                        string mlongStr = sItem["KYQD1"] + "," + sItem["KYQD2"] + "," + sItem["KYQD3"] + "," + sItem["KYQD4"] + "," + sItem["KYQD5"] + "," + sItem["KYQD6"];
                        string[] mtmpArray = mlongStr.Split(',');
                        List<double> mtmpList = new List<double>();
                        foreach (string str in mtmpArray)
                        {
                            mtmpList.Add(Conversion.Val(str));
                        }
                        mtmpList.Sort();
                        double mMaxKyqd = mtmpList[5];
                        double mMinKyqd = mtmpList[0];
                        double mAvgKyqd = mtmpList.Average();
                        mccgs = 0;
                        for (int i = 0; i <= 5; i++)
                        {
                            if ((mtmpList[i] - mAvgKyqd) > mAvgKyqd * 0.1)
                            {
                                mccgs = mccgs + 1;
                            }
                        }
                        if (mccgs > 1)
                        {
                            mAvgKyqd = -1;
                        }
                        else
                        {
                            if (System.Math.Abs(mAvgKyqd - mtmpList[0]) >= System.Math.Abs(mAvgKyqd - mtmpList[5]))
                            {
                                if (System.Math.Abs(mAvgKyqd - mtmpList[0]) > (mAvgKyqd * 0.1))
                                    mAvgKyqd = (mtmpList[1] + mtmpList[2] + mtmpList[3] + mtmpList[4] + mtmpList[5]) / 5;
                                if (System.Math.Abs(mAvgKyqd - mtmpList[1]) > (mAvgKyqd * 0.1) || System.Math.Abs(mAvgKyqd - mtmpList[5]) > (mAvgKyqd * 0.1))
                                    mAvgKyqd = -1;
                            }
                            else
                            {
                                if (System.Math.Abs(mAvgKyqd - mtmpList[5]) > (mAvgKyqd * 0.1))
                                    mAvgKyqd = (mtmpList[1] + mtmpList[2] + mtmpList[3] + mtmpList[4] + mtmpList[0]) / 5;
                                if (System.Math.Abs(mAvgKyqd - mtmpList[0]) > (mAvgKyqd * 0.1) || System.Math.Abs(mAvgKyqd - mtmpList[4]) > (mAvgKyqd * 0.1))
                                    mAvgKyqd = -1;
                            }
                        }
                        if (mAvgKyqd == -1)
                            sItem["KYPJ"] = "作废";
                        else
                            sItem["KYPJ"] = Round(mAvgKyqd, 1).ToString();
                    }
                    else
                    {
                        mmj = 10000;
                        mHsxs = 0.95;
                        sItem["KYQD1"] = Round(1000 * (Conversion.Val(sItem["KYHZ1"]) / mmj), 1).ToString();
                        sItem["KYQD2"] = Round(1000 * (Conversion.Val(sItem["KYHZ2"]) / mmj), 1).ToString();
                        sItem["KYQD3"] = Round(1000 * (Conversion.Val(sItem["KYHZ3"]) / mmj), 1).ToString();
                        string mlongStr = sItem["KYQD1"] + "," + sItem["KYQD2"] + "," + sItem["KYQD3"];
                        string[] mtmpArray = mlongStr.Split(',');
                        List<double> mtmpList = new List<double>();
                        foreach (string str in mtmpArray)
                        {
                            mtmpList.Add(Conversion.Val(str));
                        }
                        mtmpList.Sort();
                        double mMaxKyqd = mtmpList[5];
                        double mMinKyqd = mtmpList[3];
                        double mMidKyqd = mtmpList[4];
                        double mAvgKyqd = Round((mMaxKyqd + mMinKyqd + mMidKyqd) / 3, 1);
                        if (mMidKyqd != 0)
                        {
                            md = 0;
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                sItem["KYPJ"] = "无效";
                            if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                md = mMidKyqd;
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                md = mMidKyqd;
                            if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                md = mAvgKyqd;
                            if (md != 0)
                            {
                                if (Round(md, 0) <= 55)
                                    mHsxs = 0.95;
                                else if (Round(md, 0) <= 65)
                                    mHsxs = 0.94;
                                else if (Round(md, 0) <= 75)
                                    mHsxs = 0.93;
                                else if (Round(md, 0) <= 85)
                                    mHsxs = 0.92;
                                else if (Round(md, 0) <= 95)
                                    mHsxs = 0.91;
                                else if (Round(md, 0) >= 96)
                                    mHsxs = 0.9;
                                md = md * mHsxs;
                                md = Round(md, 1);
                                sItem["kypj"] = md.ToString();
                            }
                        }
                    }
                    sItem["HG_KYQD28"] = IsQualified(sItem["G_KYQD28"], sItem["KYPJ"], false);
                    mbhggs = sItem["HG_KYQD28"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (sItem["HG_KYQD28"] == "合格")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sItem["KYPJ"] = "----";
                    sItem["HG_KYQD28"] = "----";
                    sItem["G_KYQD28"] = "----";
                }

                if (jcxm.Contains("、对钢筋锈蚀作用、"))
                {
                    sItem["HG_XSZY"] = sItem["XSZY"] == "无" ? "合格" : "不合格";
                    mbhggs = sItem["HG_XSZY"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (sItem["HG_XSZY"] == "合格")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else {
                    sItem["G_XSZY"] = "----";
                    sItem["XSZY"] = "----";
                    sItem["HG_XSZY"] = "----";
                }

                if (jcxm.Contains("、泌水率、"))
                {
                    sItem["HG_MSL"] = IsQualified(sItem["G_MSL"], sItem["MSL"], false);
                    mbhggs = sItem["HG_MSL"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (sItem["HG_MSL"] == "合格")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sItem["G_MSL"] = "----";
                    sItem["MSL"] = "----";
                    sItem["HG_MSL"] = "----";
                }

                if (mbhggs == 0) {
                    sItem["JCJG"] = "合格";
                    mFlag_Hg = true;
                }
                else {
                    sItem["JCJG"] = "不合格";
                    mFlag_Hg = true;
                }

                mAllHg = mAllHg && sItem["JCJG"] == "合格";
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
