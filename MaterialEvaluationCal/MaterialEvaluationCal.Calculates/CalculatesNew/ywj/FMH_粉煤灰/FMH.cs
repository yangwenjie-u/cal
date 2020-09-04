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
            var jcxmBhg = "";
            var jcxmCur = "";
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
                    mjcjg = "不下结论";
                    mJSFF = "";
                    sItem["JCJG"] = "不下结论";
                    jsbeizhu = "试件尺寸为空";
                    mFlag_Bhg = false;
                    mbhggs += 1;
                    continue;
                }

                #region 细度 - WH
                if (jcxm.Contains("、细度、"))
                {
                    jcxmCur = "细度";
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
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    jcxmCur = "安定性";
                    //没有试饼法
                    //if ("试饼法" == sItem["ADXFF"])
                    //{
                    //    if ("无裂缝，无弯曲" == sItem["SBFJG"])
                    //    {
                    //        MItem[0]["HG_ADX"] = "合格";
                    //        mFlag_Hg = true;
                    //    }
                    //    else
                    //    {
                    //        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    //        MItem[0]["HG_ADX"] = "不合格";
                    //        mFlag_Bhg = true;
                    //        mbhggs = mbhggs + 1;
                    //        mAllHg = false;
                    //    }
                    //}
                    //else
                    //{
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
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_ADX"] = "不合格";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                    //}

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
                    jcxmCur = "需水量比";
                    MItem[0]["G_XSLB"] = mrsDj["XSLB"];
                    /*
                     * 需水量比% = 试验胶砂流动度达到对比胶砂流动度（L0）的+-2mm时的加水量（g）/ 125（对比胶砂加水量[g]）* 100 
                     * 实际检测机构要求  145 -155的计算公式   需水量比% = 试验胶砂流动度达到对比胶砂流动度（L0）的+-2mm时的加水量（g）/ （对比胶砂加水量[g]）* 100 
                     */
                    //sItem["XSLB"] = Round(Conversion.Val(sItem["SYXSLL1"]) / 125 * 100, 0).ToString("0");
                    sItem["XSLB"] = Round(Conversion.Val(sItem["SYXSLL1"]) / Conversion.Val(sItem["DBXSL"]) * 100, 0).ToString("0");
                    var isQ = IsQualified(MItem[0]["G_XSLB"], sItem["XSLB"], true);

                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_XSLB"] = "合格";
                        mFlag_Hg = true;
                    }
                    else if (isQ == "不符合")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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

                    int tcsyzGs = 0;
                    int tcdbzGs = 0;
                    double sykyqdSum = 0;
                    double dbkyqdSum = 0;
                    string syPjkyqd = "";
                    string dbPjkyqd = "";
                    double tchsykyqdSum = 0;
                    double tchdbkyqdSum = 0;
                    jcxmCur = "活性指数";
                    MItem[0]["G_HXZS"] = mrsDj["HXZS"];
                    //文档中取一组6个数据作为对照组 ，一组6个作为试验组，取28天抗压强度做平均值   面积为1600平方毫米
                    if (IsNumeric(sItem["SYZKYHZ1"].Trim()))
                    {
                        //试验组28天抗压荷载1 有值  非直接录结果
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["SYZKYQD" + i] = Round(GetSafeDouble(sItem["SYZKYHZ" + i].Trim()) * 1000 / 1600, 1).ToString("0.0");
                            sItem["DBZKYQD" + i] = Round(GetSafeDouble(sItem["DBZKYHZ" + i].Trim()) * 1000 / 1600, 1).ToString("0.0");
                            //试验组kyqd总和
                            sykyqdSum = sykyqdSum + GetSafeDouble(sItem["SYZKYHZ" + i].Trim()) * 1000 / 1600;
                            dbkyqdSum = dbkyqdSum + GetSafeDouble(sItem["DBZKYHZ" + i].Trim()) * 1000 / 1600;
                        }
                        //试验组平均抗压强度
                        syPjkyqd = Round(sykyqdSum / 6, 1).ToString("0.0");
                        //对比组平均抗压强度
                        dbPjkyqd = Round(dbkyqdSum / 6, 1).ToString("0.0");

                        //剔除超过平均值10%的数据
                        for (int i = 1; i < 7; i++)
                        {
                            if ((GetSafeDouble(sItem["SYZKYHZ" + i].Trim()) * 1000 / 1600 - GetSafeDouble(syPjkyqd)) / GetSafeDouble(syPjkyqd) * 100 > 10)
                            {
                                tcsyzGs++;
                            }
                            else
                            {
                                tchsykyqdSum = tchsykyqdSum + GetSafeDouble(sItem["SYZKYHZ" + i].Trim()) * 1000 / 1600;
                            }

                            if ((GetSafeDouble(sItem["DBZKYHZ" + i].Trim()) * 1000 / 1600 - GetSafeDouble(dbPjkyqd)) / GetSafeDouble(dbPjkyqd) * 100 > 10)
                            {
                                tcdbzGs++;
                            }
                            else
                            {
                                tchdbkyqdSum = tchdbkyqdSum + GetSafeDouble(sItem["DBZKYHZ" + i].Trim()) * 1000 / 1600;
                            }

                        }
                        if (tcsyzGs > 1 && tcdbzGs > 1)
                        {
                            //sItem["HXZSR"] = "作废";
                            //sItem["HXZSR0"] = "作废";
                            dbPjkyqd = "作废";
                            syPjkyqd = "作废";
                        }
                        else if (tcsyzGs > 1 && tcdbzGs <= 1)
                        {
                            //sItem["HXZSR"] = "作废";
                            syPjkyqd = "作废";
                        }
                        else if (tcsyzGs <= 1 && tcdbzGs > 1)
                        {
                            //对比组
                            //sItem["HXZSR0"] = "作废";
                            dbPjkyqd = "作废";
                        }
                        else if (tcsyzGs == 1 && tcdbzGs < 1)
                        {
                            syPjkyqd = Round(tchsykyqdSum / 5, 1).ToString("0.0");
                        }
                        else if (tcsyzGs < 1 && tcdbzGs == 1)
                        {
                            dbPjkyqd = Round(tchdbkyqdSum / 5, 1).ToString("0.0");
                        }
                        else if (tcsyzGs == 1 && tcdbzGs == 1)
                        {
                            syPjkyqd = Round(tchsykyqdSum / 5, 1).ToString("0.0");
                            dbPjkyqd = Round(tchdbkyqdSum / 5, 1).ToString("0.0");
                        }

                        sItem["HXZSR"] = syPjkyqd;
                        sItem["HXZSR0"] = dbPjkyqd;
                    }
                    if ("作废" != sItem["HXZSR"] && "作废" != sItem["HXZSR0"])
                    {
                        sItem["HXZS"] = Round(100 * Conversion.Val(sItem["HXZSR"]) / Conversion.Val(sItem["HXZSR0"]), 0).ToString("0");
                        var isQ = IsQualified(MItem[0]["G_HXZS"], sItem["HXZS"], true);
                        if ("符合" == isQ)
                        {
                            MItem[0]["HG_HXZS"] = "合格";
                            mFlag_Hg = true;
                        }
                        else if (isQ == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    jcxmCur = "含水量";
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
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    jcxmCur = "烧失量";
                    MItem[0]["G_SSL"] = mrsDj["SSL"];
                    //mrssubTable!SSLXLOI_1 = Format(Round(CDec(Val(mrssubTable!SSLM1) - (Val(mrssubTable!SSLM3_1) - Val(mrssubTable!SSLM4_1))) / CDec(Val(mrssubTable!SSLM1)) * 100, 2), "0.00")
                    //mrssubTable!SSLXLOI_2 = Format(Round(CDec(Val(mrssubTable!SSLM2) - (Val(mrssubTable!SSLM3_2) - Val(mrssubTable!SSLM4_2))) / CDec(Val(mrssubTable!SSLM2)) * 100, 2), "0.00")
                    sItem["SSLXLOI_1"] = Round((GetSafeDouble(sItem["SSLM1"]) - (GetSafeDouble(sItem["SSLM3_1"]) - GetSafeDouble(sItem["SSLM4_1"]))) / GetSafeDouble(sItem["SSLM1"]) * 100, 1).ToString("0.0");
                    sItem["SSLXLOI_2"] = Round((GetSafeDouble(sItem["SSLM2"]) - (GetSafeDouble(sItem["SSLM3_2"]) - GetSafeDouble(sItem["SSLM4_2"]))) / GetSafeDouble(sItem["SSLM2"]) * 100, 1).ToString("0.0");
                    //sItem["SSLXLOI_2"] = Round((GetSafeDouble(sItem["SSLM4_2"]) - GetSafeDouble(sItem["SSLM3_2"])) / (GetSafeDouble(sItem["SSLM4_2"]) - GetSafeDouble(sItem["SSLM2"])) * 100, 2).ToString("0.00");
                    sItem["SSL"] = Round((GetSafeDouble(sItem["SSLXLOI_1"]) + GetSafeDouble(sItem["SSLXLOI_2"])) / 2, 1).ToString("0.0");

                    var isQ = IsQualified(MItem[0]["G_SSL"], sItem["SSL"], true);

                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_SSL"] = "合格";
                        mFlag_Hg = true;

                    }
                    else if (isQ == "不符合")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["HG_SSL"] = "不合格";
                        mFlag_Bhg = true;
                        mAllHg = false;
                        mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        MItem[0]["HG_SSL"] = "----";
                    }
                }
                else
                {
                    MItem[0]["HG_SSL"] = "----";
                    sItem["SSL"] = "----";
                    MItem[0]["G_SSL"] = "----";
                }
                #endregion

                #region 三氧化硫
                if (jcxm.Contains("、三氧化硫、"))
                {
                    jcxmCur = "三氧化硫";
                    MItem[0]["G_SO3HL"] = mrsDj["SO3HL"];
                    //硫酸钡重量法   硫酸盐三氧化硫的质量分数 % = （灼烧后沉淀质量 g - 空白试验灼烧后沉淀的质量 g）* 0.343  / 试料的质量 g  * 100 
                    if (IsNumeric(sItem["SO3M6_1"].Trim()) && IsNumeric(sItem["SO3M5_1"].Trim()) && IsNumeric(sItem["SO3M4_1"].Trim())
                        && IsNumeric(sItem["SO3M6_2"].Trim()) && IsNumeric(sItem["SO3M5_2"].Trim()) && IsNumeric(sItem["SO3M4_2"].Trim()))
                    {
                        sItem["XSO3_1"] = Round((GetSafeDouble(sItem["SO3M6_1"].Trim()) - GetSafeDouble(sItem["SO3M5_1"].Trim())) * 0.343 / GetSafeDouble(sItem["SO3M4_1"].Trim()) * 100, 1).ToString("0.0");
                        sItem["XSO3_2"] = Round((GetSafeDouble(sItem["SO3M6_2"].Trim()) - GetSafeDouble(sItem["SO3M5_2"].Trim())) * 0.343 / GetSafeDouble(sItem["SO3M4_2"].Trim()) * 100, 1).ToString("0.0");
                        sItem["SO3HL"] = Round((GetSafeDouble(sItem["XSO3_1"]) + GetSafeDouble(sItem["XSO3_2"])) / 2, 1).ToString("0.0");
                    }
                    else
                    {
                        throw new SystemException("三氧化硫试验数据录入有误");
                    }

                    var isQ = IsQualified(MItem[0]["G_SO3HL"], sItem["SO3HL"], true);

                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_SO3HL"] = "合格";
                        mFlag_Hg = true;

                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    jcxmCur = "游离氧化钙";
                    MItem[0]["G_YHG"] = mrsDj["YHG"];
                    //该代码只为区分数据录入提示内容，无其它含义
                    //游离氧化钙质量分数 （%） = （滴定度（mg/mL） *  苯甲酸-无水乙醇标准溶液消耗体积（mL） * 0.1） / 试料质量（g）
                    if ("甘油酒精法" == sItem["YHGSYFF"])
                    {
                        if (IsNumeric(sItem["TCAO"].Trim()) && IsNumeric(sItem["CAOV1_1"].Trim()) && IsNumeric(sItem["CAOM_1"].Trim()) && IsNumeric(sItem["CAOV1_2"].Trim()) && IsNumeric(sItem["CAOM_2"].Trim()))
                        {
                            sItem["CAO_1"] = Round(GetSafeDouble(sItem["TCAO"].Trim()) * GetSafeDouble(sItem["CAOV1_1"].Trim()) * 0.1 / GetSafeDouble(sItem["CAOM_1"].Trim()), 1).ToString("0.0");
                            sItem["CAO_2"] = Round(GetSafeDouble(sItem["TCAO"].Trim()) * GetSafeDouble(sItem["CAOV1_2"].Trim()) * 0.1 / GetSafeDouble(sItem["CAOM_2"].Trim()), 1).ToString("0.0");
                            sItem["PJCAO"] = Round((GetSafeDouble(sItem["CAO_1"].Trim()) + GetSafeDouble(sItem["CAO_2"].Trim())) / 2, 1).ToString("0.0");
                        }
                        else
                        {
                            throw new SystemException("游离氧化钙试验甘油酒精法数据录入有误");
                        }
                    }
                    else
                    {
                        //乙二醇法
                        if (IsNumeric(sItem["TCAO"].Trim()) && IsNumeric(sItem["CAOV1_1"].Trim()) && IsNumeric(sItem["CAOM_1"].Trim()) && IsNumeric(sItem["CAOV1_2"].Trim()) && IsNumeric(sItem["CAOM_2"].Trim()))
                        {
                            sItem["CAO_1"] = Round(GetSafeDouble(sItem["TCAO"].Trim()) * GetSafeDouble(sItem["CAOV1_1"].Trim()) * 0.1 / GetSafeDouble(sItem["CAOM_1"].Trim()), 1).ToString("0.0");
                            sItem["CAO_2"] = Round(GetSafeDouble(sItem["TCAO"].Trim()) * GetSafeDouble(sItem["CAOV1_2"].Trim()) * 0.1 / GetSafeDouble(sItem["CAOM_2"].Trim()), 1).ToString("0.0");
                            sItem["PJCAO"] = Round((GetSafeDouble(sItem["CAO_1"].Trim()) + GetSafeDouble(sItem["CAO_2"].Trim())) / 2, 1).ToString("0.0");
                        }
                        else
                        {
                            throw new SystemException("游离氧化钙试验乙二醇法数据录入有误");
                        }
                    }

                    var isQ = IsQualified(MItem[0]["G_YHG"], sItem["PJCAO"], true);

                    if ("符合" == isQ)
                    {
                        MItem[0]["HG_YHG"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求	。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
        }
    }
}

