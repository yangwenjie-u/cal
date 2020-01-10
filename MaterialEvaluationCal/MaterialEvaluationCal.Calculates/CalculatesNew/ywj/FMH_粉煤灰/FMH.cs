using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class FMH : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_FMH_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_FMH"];

            if (!data.ContainsKey("M_FMH"))
            {
                data["M_FMH"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_FMH"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;
            var mbhggs = 0;
            var mJSFF = "";
            var jcxm = "";
            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"] && u["LB"] == sItem["LB"] && u["DJ"] == sItem["DJ"]);

                if (mrsDj != null)
                {
                    mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].ToLower();
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "试件尺寸为空";
                    mFlag_Bhg = false;
                    mbhggs += 1;
                }

                if (jcxm.Contains("、细度、"))
                {
                    MItem[0]["G_XD"] = mrsDj["XD"];

                    var isQ = IsQualified(MItem[0]["G_XD"], sItem["XD"], true);
                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_XD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else if (isQ == "不符合")
                    {
                        MItem[0]["HG_XD"] = "不合格";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                    else
                    {
                        MItem[0]["HG_XD"] = "----";
                    }
                }
                else
                {
                    MItem[0]["G_XD"] = "----";
                    MItem[0]["HG_XD"] = "----";
                }

                if (jcxm.Contains("、安定性、"))
                {
                    MItem[0]["G_ADX"] = mrsDj["ADX"];

                    var isQ = IsQualified(MItem[0]["G_XD"], sItem["XD"], true);
                    MItem[0]["HG_ADX"] = isQ;

                    if (isQ == "不符合")
                    {
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    MItem[0]["G_ADX"] = "----";
                    MItem[0]["HG_ADX"] = "----";
                    sItem["ADX"] = "----";
                }

                if (jcxm.Contains("、需水量比、"))
                {
                    MItem[0]["G_XSLB"] = mrsDj["XSLB"];

                    var isQ = IsQualified(MItem[0]["G_XSLB"], sItem["XSLB"], true);


                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_XSLB"] = "合格";
                        mFlag_Hg = true;
                    }
                    else if (isQ == "不符合")
                    {
                        MItem[0]["HG_XSLB"] = "不合格";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                    else
                    {
                        MItem[0]["HG_XSLB"] = "----";
                    }
                }
                else
                {
                    sItem["XSLB"] = "----";
                    MItem[0]["G_XSLB"] = "----";
                    MItem[0]["G_XSLB"] = "----";
                }

                if (jcxm.Contains("、强度活性指数、"))
                {
                    MItem[0]["G_HXZS"] = mrsDj["HXZS"];

                    var isQ = IsQualified(MItem[0]["G_HXZS"], sItem["HXZS"], true);

                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_HXZS"] = "合格";
                        mFlag_Hg = true;
                    }
                    else if (isQ == "不符合")
                    {
                        MItem[0]["HG_HXZS"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        mAllHg = false;
                    }
                    else
                    {
                        MItem[0]["HG_HXZS"] = "----";
                    }
                }
                else
                {
                    MItem[0]["G_HXZS"] = "----";
                    sItem["HXZS"] = "----";
                    MItem[0]["HG_HXZS"] = "----";
                }

                if (jcxm.Contains("、含水量、"))
                {
                    MItem[0]["G_HSL"] = mrsDj["HSL"];

                    var isQ = IsQualified(MItem[0]["G_HSL"], sItem["HSL"], true);

                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_HSL"] = "合格";
                        mFlag_Hg = true;
                    }
                    else if (isQ == "不符合")
                    {
                        MItem[0]["HG_HSL"] = "不合格";
                        mFlag_Bhg = true;
                        mAllHg = false;
                        mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        MItem[0]["HG_HSL"] = "----";
                    }
                }
                else
                {
                    MItem[0]["G_HSL"] = "----";
                    sItem["HSL"] = "----";
                    MItem[0]["HG_HSL"] = "----";
                }

                if (jcxm.Contains("、烧失量、"))
                {
                    MItem[0]["G_SSL"] = mrsDj["SSL"];

                    var isQ = IsQualified(MItem[0]["G_SSL"], sItem["SSL"], true);

                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_SSL"] = "合格";
                        mFlag_Hg = true;

                    }
                    else if (isQ == "不符合")
                    {
                        MItem[0]["HG_SSL"] = "不合格";
                        mFlag_Bhg = true;
                        mAllHg = false;
                        mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        MItem[0]["HG_HSL"] = "----";
                    }
                }
                else
                {
                    MItem[0]["HG_HSL"] = "----";
                    sItem["SSL"] = "----";
                    MItem[0]["G_SSL"] = "----";
                }

                if (jcxm.Contains("、三氧化硫、"))
                {
                    MItem[0]["G_SO3HL"] = mrsDj["SO3HL"];

                    var isQ = IsQualified(MItem[0]["G_SO3HL"], sItem["SO3HL"], true);

                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_SO3HL"] = "合格";
                        mFlag_Hg = true;

                    }
                    else
                    {
                        MItem[0]["HG_SO3HL"] = "不合格";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["SO3HL"] = "----";
                    sItem["SO3M6_1"] = "----";
                    sItem["SO3M5_1"] = "----";
                    sItem["SO3M4_1"] = "----";
                    sItem["SO3M6_2"] = "----";
                    sItem["SO3M5_2"] = "----";
                    sItem["SO3M4_2"] = "----";
                    sItem["XSO3_1"] = "----";
                    sItem["XSO3_2"] = "----";

                    MItem[0]["HG_SO3HL"] = "----";
                    MItem[0]["G_SO3HL"] = "----";
                }

                if (jcxm.Contains("、游离氧化钙、"))
                {
                    MItem[0]["G_YHG"] = mrsDj["YHG"];

                    var isQ = IsQualified(MItem[0]["G_YHG"], sItem["PJCAO"], true);

                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_YHG"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        MItem[0]["HG_YHG"] = "不合格";
                        mFlag_Bhg = true;
                        mAllHg = false;
                        mbhggs = mbhggs + 1;
                    }
                }
                else
                {
                    sItem["PJCAO"] = "----";
                    MItem[0]["HG_YHG"] = "----";
                    MItem[0]["G_YHG"] = "----";
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                }
                else
                {
                    sItem["JCJG"] = "合格";
                }
            }

            #region 添加最终报告
            jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";

            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
        }
    }
}

