using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class AQW : BaseMethods
    {
        public void Calc()
        {

            var extraDJ = dataExtra["BZ_AQW_DJ"];
            var jcxmItems = retData.Select(u => u.Key).ToArray();

            string errorMsg = "";
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var data = retData;
            double mKyqd1, mKyqd2, mKyqd3, mKyqd4, mKyqd5, mKyqd6;
            bool mGetBgbh = false, mAllHg = true;
            bool mSFwc =true;
            string mSjdj = "";
            string mJSFF;
            int mbhggs = 0;
            var SItem = data["S_AQW"];
            var MItem = data["M_AQW"];
            foreach (var sItem in SItem)
            {
                var jcxm = sItem["JCXM"];
                var extraFieldsDJ = extraDJ.FirstOrDefault();
                bool sign = false;
                double ydmax, ydmin;
                int dw;
                foreach (var aqmDJ in extraDJ)
                {
                    if (sItem["CPMC"].Trim() == extraFieldsDJ["MC"].Trim())
                    {
                        sItem["G_WG"] = extraFieldsDJ["D_WG"].Trim();
                        sItem["G_KD"] = extraFieldsDJ["D_KD"].Trim();
                        sItem["G_GC"] = extraFieldsDJ["D_PC"].Trim();
                        sItem["G_HK"] = extraFieldsDJ["D_HK"].Trim();
                        sItem["G_ZL"] = extraFieldsDJ["D_ZL"].Trim();
                        sItem["G_KC"] = extraFieldsDJ["D_KCJ"].Trim();
                        sItem["G_NG"] = extraFieldsDJ["D_NGC"].Trim();
                        sign = true;
                    }
                }
                if (!sign)
                {
                    mJSFF = "";
                    MItem[0]["BGBH"] = "";
                    sItem["JCJG"] = "依据不详";
                    MItem[0]["JSBEIZHU"] = "找不到对应的等级";
                    continue;
                }
                if (sItem["CPMC"] == "密目式安全立网")
                {
                    sItem["G_KD"] = "----";
                }else
                {
                    sItem["G_HK"] = "----";
                }
                mbhggs = 0;
                string bhgxm = "", hgxm = "";
                switch (jcxm)
                {
                    case "外观":
                        #region 外观
                        if (sItem["S_WGJG"].Trim() == "符合")
                        {
                            sItem["WG_GH"] = "合格";
                            hgxm = hgxm + "、外观";
                        }
                        else
                        {
                            sItem["WG_GH"] = "不合格";
                            bhgxm = bhgxm + "、外观";
                            mbhggs = mbhggs + 1;
                        }
                        sItem["W_WG"] = sItem["S_WGJG"].Trim();
                        sItem["G_WG"] = sItem["G_WG"].Trim();
                        #endregion
                        break;
                    case "规格":
                        #region 规格
                        if (sItem["S_GGJG"].Trim() == "符合")
                        {
                            hgxm = hgxm + "、规格";
                            sItem["GG_GH"] = "合格";
                        }
                        else
                        {
                            sItem["GG_GH"] = "不合格";
                            bhgxm = bhgxm + "、规格";
                            mbhggs = mbhggs + 1;
                        }
                        sItem["W_GG"] = sItem["S_GGJG"].Trim();
                        sItem["G_KD"] = sItem["G_KD"].Trim();
                        sItem["G_HK"] = sItem["G_HK"].Trim();
                        sItem["G_GC"] = sItem["G_GC"].Trim();
                            #endregion
                        break;
                    case "重量":
                        #region 重量
                        if (sItem["S_ZL"].Trim() == "符合")
                        {
                            sItem["ZL_GH"] = "合格";
                            hgxm = hgxm + "、重量";
                        }
                        else
                        {
                            sItem["ZL_GH"] = "不合格";
                            bhgxm = bhgxm + "、重量";
                            mbhggs = mbhggs + 1;
                        }
                        sItem["W_ZL"] = sItem["S_ZL"].Trim();
                        sItem["G_ZL"] = sItem["G_ZL"];
                        #endregion
                        break;
                    case "抗冲击性":
                        #region 抗冲击性
                        if (sItem["S_KCJG"].Trim() == "符合")
                        {
                            sItem["KC_GH"] = "合格";
                            hgxm = hgxm + "、抗冲击性";
                        }
                        else
                        {
                            sItem["KC_GH"] = "不合格";
                            bhgxm = bhgxm + "、抗冲击性";
                            mbhggs = mbhggs + 1;
                        }
                        sItem["W_KC"] = sItem["S_KCMS"].Trim();
                        sItem["G_KC"] = sItem["G_KC"];
                        #endregion
                        break;
                    case "耐贯穿性":
                        #region 耐贯穿性
                        if (sItem["S_NGJG"].Trim() == "符合")
                        {
                            sItem["NG_GH"] = "合格";
                            hgxm = hgxm + "、耐贯穿性";
                        }
                        else
                        {
                            sItem["NG_GH"] = "不合格";
                            bhgxm = bhgxm + "、抗冲击性";
                            mbhggs = mbhggs + 1;
                        }
                        sItem["W_NG"] = sItem["S_NGMS"].Trim();
                        sItem["G_NG"] = sItem["G_NG"];
                        #endregion
                        break;
                }
                MItem[0]["JSBEIZHU"] = "";
                if (mbhggs == 0)
                {
                    MItem[0]["JSBEIZHU"] = "该组样品所检指标符合" + MItem[0]["JCYJ"].Trim() + "标准要求。";
                    sItem["jcjg"] = "合格";
                }
                if (mbhggs > 0)
                {
                    MItem[0]["JSBEIZHU"] = "该组样品所检指标中有不符合" + MItem[0]["JCYJ"].Trim() + "标准要求的指标。";
                    sItem["JCJG"] = "不合格";
                }
                if (bhgxm.Length > 1)
                {
                    bhgxm = bhgxm.Substring(bhgxm.Length-(bhgxm.Length - 1), bhgxm.Length - 1);
                }
                if (hgxm.Length >1)
                {
                    hgxm = hgxm.Substring(hgxm.Length - (hgxm.Length - 1), hgxm.Length - 1);
                }
                MItem[0]["JGSM"] = "经检测，该样品所检项目中" + hgxm + "，符合标准要求；" + bhgxm + "不符合" + MItem[0]["JCYJ"].Trim() + "标准要求。";
                if (bhgxm == "")
                {
                    if (hgxm.Contains("、"))
                    {
                        MItem[0]["JGSM"] = "经检测，该样品所检项目：" + sItem["JCXM"] + "，均符合" + MItem[0]["JCYJ"].Trim() + "标准要求。";
                    }
                    else
                    {
                        MItem[0]["JGSM"] = "经检测，该样品所检项目：" + sItem["JCXM"] + "符合" + MItem[0]["JCYJ"].Trim() + "标准要求。";
                    }
                }
                if (hgxm == "")
                {
                    if (bhgxm.Contains("、"))
                    {
                        MItem[0]["JGSM"] = "经检测，该样品所检项目：" + sItem["JCXM"] + "，均不符合" + MItem[0]["JCYJ"].Trim() + "标准要求。";
                    }
                    else
                    {
                        MItem[0]["JGSM"] = "经检测，该样品所检项目：" + sItem["JCXM"] + "不符合" + MItem[0]["JCYJ"].Trim() + "标准要求。";
                    }
                }
                MItem[0]["JGSM"] = MItem[0]["JGSM"] + " 结果详见报告第2页。";
                if (mbhggs == 0)
                {
                    MItem[0]["JCJG"] = "合格";
                }
                else
                {
                    MItem[0]["JCJG"] = "不合格";
                }
            }
            #region 添加最终报告
            if (mbhggs > 0)
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_AQW"))
            {
                data["M_AQW"] = new List<IDictionary<string, string>>();
            }
            var M_AQW = data["M_AQW"];
            if (M_AQW.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_AQW.Add(m);
            }
            else
            {
                M_AQW[0]["JCJG"] = mjcjg;
                M_AQW[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
        }
    }
}
