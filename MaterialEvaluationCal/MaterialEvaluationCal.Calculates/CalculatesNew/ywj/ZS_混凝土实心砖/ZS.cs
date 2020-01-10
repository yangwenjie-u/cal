using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZS : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_ZS_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_ZS"];

            if (!data.ContainsKey("M_ZS"))
            {
                data["M_ZS"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_ZS"];

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
            int xd = 0;
            List<double> nArr = new List<double>();

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                mbhggs = 0;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mFlag_Hg = true;
                sign = true;
                if (jcxm.Contains("、抗压、"))
                {
                    sign = true;
                    for (xd = 1; xd < 11; xd++)
                    {
                        sign = IsNumeric(sItem["KYQD" + xd]) ? sign : false;
                        if (!sign)
                        {
                            break;
                        }
                    }
                    if (!sign)
                    {
                        return false;
                    }

                    sItem["qdyq"] = "抗压强度平均值需" + Double.Parse(mItem["G_PJZ"]).ToString("0") + "MPa,单块最小强度值需" + Double.Parse(mItem["G_MIN"]).ToString() + "MPa。";
                    sum = 0;
                    nArr.Clear();
                    for (xd = 0; xd < 10; xd++)
                    {
                        md = Double.Parse(sItem["KYQD" + xd]);
                        nArr[xd] = md;
                        sum += md;
                    }
                    pjmd = Math.Round(sum / 10, 2);
                    sItem["kypj"] = pjmd.ToString("0.00");

                    nArr.Sort();
                    sItem["DKZX"] = Math.Round(nArr[0], 1).ToString("0.0");


                    sign = IsQualified(Double.Parse(mItem["G_PJZ"]).ToString("0"), sItem["kypj"]) == "合格" ? sign : false;
                    sign = IsQualified(Double.Parse(mItem["G_MIN"]).ToString("0.0"), sItem["DKZX"]) == "合格" ? sign : false;
                    sItem["qdpd"] = sign ? "合格" : "不合格";
                    if (sItem["qdpd"] == "不合格")
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
                    sItem["kypj"] = "----";
                    sItem["qdpd"] = "----";
                    sItem["qdmin"] = "----";
                    sItem["qdyq"] = "----";
                    for (xd = 1; xd < 11; xd++)
                    {
                        sItem["KYQD" + xd] = "----";
                    }
                }

                if (jcxm.Contains("、干密度、"))
                {
                    sign = true;
                    for (xd = 1; xd < 4; xd++)
                    {
                        sign = IsNumeric(sItem["GMD" + xd]) ? sign : false;
                        if (!sign)
                        {
                            break;
                        }
                    }
                    if (!sign)
                    {
                        return false;
                    }

                    sign = IsQualified(Double.Parse(mItem["G_gmd"]).ToString("0"), sItem["gmdpj"]) == "合格" ? sign : false;
                    sItem["gmdpd"] = sign ? "合格" : "不合格";
                    if (sItem["gmdpd"] == "不合格")
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
                    sItem["gmdpj"] = "----";
                    sItem["gmdpd"] = "----";
                    sItem["g_gmd"] = "----";
                    for (xd = 1; xd < 4; xd++)
                    {
                        sItem["gmd" + xd] = "----";
                    }
                }


                if (jcxm.Contains("、吸水率、"))
                {
                    sign = true;
                    for (xd = 1; xd < 4; xd++)
                    {
                        sign = IsNumeric(sItem["HXSW2_" + xd]) ? sign : false;
                        if (!sign)
                        {
                            break;
                        }
                    }
                    if (!sign)
                    {
                        return false;
                    }

                    sign = IsQualified(Double.Parse(sItem["xslyq"]).ToString("0"), sItem["HXSW2"]) == "合格" ? sign : false;
                    sItem["xslpd"] = sign ? "合格" : "不合格";
                    if (sItem["xslpd"] == "不合格")
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
                    sItem["HXSW2"] = "----";
                    sItem["xslpd"] = "----";
                    sItem["xslyq"] = "----";
                    for (xd = 1; xd < 4; xd++)
                    {
                        sItem["HXSW2_" + xd] = "----";
                    }
                }

                if (jcxm.Contains("、相对含水率、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["HXSW1"]) ? sign : false;
                    sign = IsNumeric(sItem["HXSW"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }

                    sign = IsQualified(Double.Parse(sItem["XDHSLYQ"]).ToString("0"), sItem["HXSW2"]) == "合格" ? sign : false;
                    sItem["XDHSLPD"] = sign ? "合格" : "不合格";
                    if (sItem["XDHSLPD"] == "不合格")
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
                    sItem["HXSW1"] = "----";
                    sItem["xdxslpd"] = "----";
                    sItem["xdxslyq"] = "----";
                    sItem["HXSW"] = "----";
                }

                if (jcxm.Contains("、干缩率、"))
                {
                    sign = true;
                    for (xd = 1; xd < 4; xd++)
                    {
                        sign = IsNumeric(sItem["GSL" + xd]) ? sign : false;
                        if (!sign)
                        {
                            break;
                        }
                    }

                    if (!sign)
                    {
                        return false;
                    }

                    sign = IsQualified(Double.Parse(sItem["gslyq"]).ToString("0"), sItem["gsl"]) == "合格" ? sign : false;
                    sItem["gslpd"] = sign ? "合格" : "不合格";
                    if (sItem["gslpd"] == "不合格")
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
                    sItem["gsl"] = "----";
                    sItem["gslpd"] = "----";
                    sItem["gslyq"] = "----";
                    for (xd = 1; xd < 4; xd++)
                    {
                        sItem["gsl" + xd] = "----";
                    }
                }

                if (mbhggs == 0)
                {
                    jsbeizhu = "该组试件所检项目符合上述标准要求。";
                    sItem["JCJG"] = "合格";
                    if (mFlag_Bhg && mFlag_Hg)
                    {
                        jsbeizhu = "该组试件所检项目部分符合上述标准要求。";
                    }
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "该组试件不符合上述标准要求。";

                }

                return mAllHg;
            };

            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mItemHg = true;

                Gs = extraDJ.Count;
                MItem[0]["G_gmd"] = "----";
                MItem[0]["G_MIN"] = "----";
                MItem[0]["G_PJZ"] = "----";
                sItem["XDHSLYQ"] = "----";
                sItem["gslyq"] = "----";
                sItem["xslyq"] = "----";
                for (int i = 0; i < Gs + 1; i++)
                {
                    if (sItem["gmddj"].Trim() == extraDJ[i]["gmddj"])
                    {
                        MItem[0]["G_gmd"] = extraDJ[i]["G_gmd"];
                        sItem["xslyq"] = extraDJ[i]["G_xsl"];
                    }

                    if (sItem["sjdj"].Trim() == extraDJ[i]["qddj"])
                    {
                        MItem[0]["G_MIN"] = extraDJ[i]["g_qdmin"];
                        MItem[0]["G_PJZ"] = extraDJ[i]["g_qdpj"];
                    }

                    if (extraDJ[i]["sytj"].Trim() == "干燥")
                    {
                        sItem["XDHSLYQ"] = extraDJ[i]["G_xdhsl"];
                        sItem["gslyq"] = extraDJ[i]["G_gzssl"];
                    }
                }

                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
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

