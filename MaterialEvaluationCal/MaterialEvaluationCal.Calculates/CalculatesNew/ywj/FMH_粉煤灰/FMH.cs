﻿using System;
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

                #region 细度 - WH
                if (jcxm.Contains("、细度、"))
                {
                    MItem[0]["G_XD"] = mrsDj["XD"];
                    /*
                     * 标准文档中需同组连续试验2次 此代码暂时按平均值来处理
                     * sy1 = Round(CDec(Val(mrssubTable!XDG1) / Val(mrssubTable!XDG)) * 100, 1) 
                     * sy2 = Round(CDec(Val(mrssubTable!XDG3) / Val(mrssubTable!XDG2)) * 100, 1) 
                     * sy1 = sy1 * Val(mrssubTable!xzxs)
                     * sy2 = sy2 * Val(mrssubTable!xzxs)
                     * mrssubTable!XD = Format(Round((sy1 + sy2) / 2, 1), "0.0") 
                     */
                    // 筛余 = 细度筛余质量 / 细度试样质量 * 100 精确到一位小数
                    sItem["SY"] = Round(Conversion.Val(sItem["XDG1"]) / Conversion.Val(sItem["XDG"]) * 100, 1).ToString("0.0");
                    // 细度 = 筛余 * 修正系数  精确到一位小数
                    sItem["XD"] = Round(Conversion.Val(sItem["SY"]) * Conversion.Val(sItem["XZXS"]), 1).ToString("0.0");
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
                #endregion

                #region 安定性 - WH
                if (jcxm.Contains("、安定性、"))
                {
                    MItem[0]["G_ADX"] = mrsDj["ADX"];
                    var mzcj1 = Conversion.Val(sItem["ADXC1"]) - Conversion.Val(sItem["ADXA1"]);
                    var mzcj2 = Conversion.Val(sItem["ADXC2"]) - Conversion.Val(sItem["ADXA2"]);
                    sItem["ADX"] = Round((mzcj1 + mzcj2) / 2, 1).ToString("0.0");
                    var isQ = IsQualified(MItem[0]["G_ADX"], sItem["ADX"], true);
                    //MItem[0]["HG_ADX"] = isQ;
                    if (isQ == "符合")
                    {
                        MItem[0]["HG_ADX"] = "合格";
                        mFlag_Hg = true;
                    }
                    else if (isQ == "不符合")
                    {
                        MItem[0]["HG_ADX"] = "不合格";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                        mAllHg = false;

                    }
                }
                else
                {
                    MItem[0]["G_ADX"] = "----";
                    MItem[0]["HG_ADX"] = "----";
                    sItem["ADX"] = "----";
                }
                #endregion

                #region 需水量比 - WH 
                if (jcxm.Contains("、需水量比、"))
                {
                    MItem[0]["G_XSLB"] = mrsDj["XSLB"];
                    /*
                     * 需水量比% = 试验胶砂流动度达到对比胶砂流动度（L0）的+-2mm时的加水量（g）/ 125（对比胶砂加水量[g]）* 100 
                     */
                    sItem["XSLB"] = Round(Conversion.Val(sItem["SYXSLL1"]) / 125 * 100, 0).ToString("0");
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
                #endregion

                #region 活性指数 - WH
                if (jcxm.Contains("、活性指数、"))
                {
                    MItem[0]["G_HXZS"] = mrsDj["HXZS"];
                    //文档中取一组6个数据作为对照组 ，一组6个作为试验组，取28天抗压强度做平均值
                    sItem["HXZS"] = Round(100 * Conversion.Val(sItem["HXZSR"]) / Conversion.Val(sItem["HXZSR0"]), 0).ToString("0");
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
                #endregion

                #region 含水量 - WH
                if (jcxm.Contains("、含水量、"))
                {
                    MItem[0]["G_HSL"] = mrsDj["HSL"];
                    sItem["HSHSY1"] = (Conversion.Val(sItem["HSHGJSY1"]) - Conversion.Val(sItem["HGGZL1"])).ToString();
                    sItem["HSHSY2"] = (Conversion.Val(sItem["HSHGJSY2"]) - Conversion.Val(sItem["HGGZL2"])).ToString();
                    sItem["HSL1"] = Round((GetSafeDouble(sItem["HSYZL1"]) - GetSafeDouble(sItem["HSHSY1"])) / GetSafeDouble(sItem["HSYZL1"]) * 100, 1).ToString("0.0");
                    sItem["HSL2"] = Round((GetSafeDouble(sItem["HSYZL2"]) - GetSafeDouble(sItem["HSHSY2"])) / GetSafeDouble(sItem["HSYZL2"]) * 100, 1).ToString("0.0");
                    sItem["HSL"] = Round((GetSafeDouble(sItem["HSL1"]) + GetSafeDouble(sItem["HSL2"])) / 2, 1).ToString("0.0");
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
                #endregion

                #region 烧失量 - WH
                if (jcxm.Contains("、烧失量、"))
                {
                    MItem[0]["G_SSL"] = mrsDj["SSL"];
                    //mrssubTable!SSLXLOI_1 = Format(Round(CDec(Val(mrssubTable!SSLM1) - (Val(mrssubTable!SSLM3_1) - Val(mrssubTable!SSLM4_1))) / CDec(Val(mrssubTable!SSLM1)) * 100, 2), "0.00")
                    //mrssubTable!SSLXLOI_2 = Format(Round(CDec(Val(mrssubTable!SSLM2) - (Val(mrssubTable!SSLM3_2) - Val(mrssubTable!SSLM4_2))) / CDec(Val(mrssubTable!SSLM2)) * 100, 2), "0.00")
                    sItem["SSLXLOI_1"] = Round((GetSafeDouble(sItem["SSLM1"]) - (GetSafeDouble(sItem["SSLM3_1"]) - GetSafeDouble(sItem["SSLM4_1"]))) / GetSafeDouble(sItem["SSLM1"]) * 100, 2).ToString("0.00");
                    sItem["SSLXLOI_2"] = Round((GetSafeDouble(sItem["SSLM2"]) - (GetSafeDouble(sItem["SSLM3_2"]) - GetSafeDouble(sItem["SSLM4_2"]))) / GetSafeDouble(sItem["SSLM2"]) * 100, 2).ToString("0.00");
                    //sItem["SSLXLOI_2"] = Round((GetSafeDouble(sItem["SSLM4_2"]) - GetSafeDouble(sItem["SSLM3_2"])) / (GetSafeDouble(sItem["SSLM4_2"]) - GetSafeDouble(sItem["SSLM2"])) * 100, 2).ToString("0.00");
                    sItem["SSL"] = Round((GetSafeDouble(sItem["SSLXLOI_1"]) + GetSafeDouble(sItem["SSLXLOI_2"])) / 2, 2).ToString("0.00");

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
                #endregion

                #region 三氧化硫
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
                #endregion

                #region 游离氧化钙
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
                #endregion

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

