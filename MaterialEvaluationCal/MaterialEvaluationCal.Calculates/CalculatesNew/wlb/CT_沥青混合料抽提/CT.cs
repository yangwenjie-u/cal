using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class CT : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            string mSjdj = "";
            string errorMsg = "";
            string mJSFF = "";
            bool mSFwc;
            bool mGetBgbh = false,
            mAllHg = true,
            mFlag_Hg = false, mItemHg = true,
            mFlag_Bhg = false;
            int mbhggs = 0;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            bool sign = true;
            bool itemHg = true;
            var SItem = data["S_CT"];
            if (!data.ContainsKey("M_CT"))
            {
                data["M_CT"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_CT"];
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var mrsDj = dataExtra["BZ_CT_DJ"];
            var mItem = MItem[0];
            var jcxmBhg = "";
            var jcxmCur = "";
            string gjkTgl = "";
            bool gjkBool = false;
            double pjsySum = 0;
            foreach (var sItem in SItem)
            {
                var jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                List<double> lqhlList = new List<double>();
                #region 沥青含量
                if (jcxm.Contains("、沥青含量、"))
                {
                    if ("是" == sItem["SFRSF"])
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            sign = IsNumeric(sItem["RSGZCZZL" + i].Trim());
                            sign = IsNumeric(sItem["CTYZL" + i].Trim());
                            sign = IsNumeric(sItem["RSGZCTYZL" + i].Trim());
                            sign = IsNumeric(sItem["RSGZCZZL" + i].Trim());
                            sign = IsNumeric(sItem["LQHHLZZL" + i].Trim());
                            sign = IsNumeric(sItem["JLGZZL" + i].Trim());
                        }
                        sItem["KLBFZZL1"] = Round(GetSafeDouble(sItem["JLGZZL1"].Trim()) + GetSafeDouble(sItem["YZSYQHZZ1"].Trim()) + GetSafeDouble(sItem["XLCTYKFZL1"].Trim()), 1).ToString("0.0");
                        sItem["KLBFZZL2"] = Round(GetSafeDouble(sItem["JLGZZL2"].Trim()) + GetSafeDouble(sItem["YZSYQHZZ2"].Trim()) + GetSafeDouble(sItem["XLCTYKFZL2"].Trim()), 1).ToString("0.0");
                        if (sign)
                        {
                            //使用燃烧法  
                            for (int i = 1; i < 3; i++)
                            {
                                //泄漏入抽提液中的矿粉质量 = 坩埚中燃烧干燥的残渣质量  * （抽提液总量 /取出的燃烧干燥的抽提液数量）
                                sItem["XLCTYKFZL" + i] = Round(GetSafeDouble(sItem["RSGZCZZL" + i].Trim()) * (GetSafeDouble(sItem["CTYZL" + i].Trim()) / GetSafeDouble(sItem["RSGZCTYZL" + i].Trim())), 1).ToString("0.0");
                                //沥青含量  = （沥青混合料总质量 - 沥青混合料中矿料部分总质量）/ 沥青混合料总质量
                                sItem["LQHL" + i] = Round((GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) - (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                    + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim()))) / GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) * 100, 2).ToString("0.00");
                                //油石比 = 
                                sItem["YSB" + i] = Round((GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) - (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                   + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim()))) / (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                   + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim())) * 100, 2).ToString("0.00");
                            }
                            //两次试验差值 大于0.3% 且小于0.5%  并且沥青混合料总质量3有值时 ，补充平行试验一次
                            if (Math.Abs(GetSafeDouble(sItem["LQHL1"]) - GetSafeDouble(sItem["LQHL2"])) > 0.3 && Math.Abs(GetSafeDouble(sItem["LQHL1"]) - GetSafeDouble(sItem["LQHL2"])) < 0.5)
                            {
                                if (IsNumeric(sItem["LQHHLZZL3"].Trim()) && IsNumeric(sItem["RSGZCZZL3"].Trim()) && IsNumeric(sItem["CTYZL3"].Trim()) && IsNumeric(sItem["RSGZCTYZL3"].Trim())
                                     && IsNumeric(sItem["JLGZZL3"].Trim()) && IsNumeric(sItem["YZSYQHZZ3"].Trim()))
                                {
                                    for (int i = 1; i < 4; i++)
                                    {
                                        //泄漏入抽提液中的矿粉质量 = 坩埚中燃烧干燥的残渣质量  * （抽提液总量 /取出的燃烧干燥的抽提液数量）
                                        sItem["XLCTYKFZL" + i] = Round(GetSafeDouble(sItem["RSGZCZZL" + i].Trim()) * (GetSafeDouble(sItem["CTYZL" + i].Trim()) / GetSafeDouble(sItem["RSGZCTYZL" + i].Trim())), 1).ToString("0.0");
                                        //沥青含量  = （沥青混合料总质量 - 沥青混合料中矿料部分总质量）/ 沥青混合料总质量
                                        sItem["LQHL" + i] = Round((GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) - (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                            + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim()))) / GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) * 100, 2).ToString("0.00");
                                        //油石比 = 
                                        sItem["YSB" + i] = Round((GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) - (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                           + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim()))) / (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                           + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim())) * 100, 2).ToString("0.00");
                                    }
                                    //3次试验最大值与最小值之差不得大于5%
                                    lqhlList.Add(GetSafeDouble(sItem["LQHL1"]));
                                    lqhlList.Add(GetSafeDouble(sItem["LQHL2"]));
                                    lqhlList.Add(GetSafeDouble(sItem["LQHL3"]));
                                    lqhlList.Sort();
                                    if (Math.Abs(lqhlList[0] - lqhlList[2]) <= 5)
                                    {
                                        //计算平均沥青含量  平均油石比
                                        sItem["PJLQHL"] = Round((lqhlList[0] + lqhlList[1] + lqhlList[2]) / 3, 2).ToString("0.00");
                                        sItem["PJYSB"] = Round((GetSafeDouble(sItem["YSB1"]) + GetSafeDouble(sItem["YSB2"]) + GetSafeDouble(sItem["YSB3"])) / 3, 2).ToString("0.00");
                                    }
                                    else
                                    {
                                        //不知道该情况如何处理
                                        throw new SystemException("3次试验最大值与最小值之差大于5%");
                                    }
                                }
                                else
                                {
                                    throw new SystemException("两次试验差值大于0.3%且小于0.5% ，需补充平行试验一次，或补充平行试验所录数据有误。");
                                }
                            }
                            else
                            {
                                //无需追加试验计算平均值
                                sItem["PJLQHL"] = Round((GetSafeDouble(sItem["LQHL1"]) + GetSafeDouble(sItem["LQHL2"])) / 2, 2).ToString("0.00");
                                sItem["PJYSB"] = Round((GetSafeDouble(sItem["YSB1"]) + GetSafeDouble(sItem["YSB2"])) / 2, 2).ToString("0.00");
                            }
                        }
                        else
                        {
                            throw new SystemException("沥青含量数据录入有误");
                        }
                    }
                    else
                    {
                        sign = true;
                        for (int i = 1; i < 3; i++)
                        {
                            sign = IsNumeric(sItem["LQHHLZZL" + i].Trim());
                            sign = IsNumeric(sItem["JLGZZL" + i].Trim());
                            sign = IsNumeric(sItem["YZSYQHZZ" + i].Trim());
                            sign = IsNumeric(sItem["XLCTYKFZL" + i].Trim());
                        }
                        sItem["KLBFZZL1"] = Round(GetSafeDouble(sItem["JLGZZL1"].Trim()) + GetSafeDouble(sItem["YZSYQHZZ1"].Trim()) + GetSafeDouble(sItem["XLCTYKFZL1"].Trim()), 1).ToString("0.0");
                        sItem["KLBFZZL2"] = Round(GetSafeDouble(sItem["JLGZZL2"].Trim()) + GetSafeDouble(sItem["YZSYQHZZ2"].Trim()) + GetSafeDouble(sItem["XLCTYKFZL2"].Trim()), 1).ToString("0.0");
                        if (sign)
                        {
                            //不用燃烧法  
                            for (int i = 1; i < 3; i++)
                            {
                                //沥青含量  = （沥青混合料总质量 - 沥青混合料中矿料部分总质量）/ 沥青混合料总质量
                                sItem["LQHL" + i] = Round((GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) - (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                    + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim()))) / GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) * 100, 2).ToString("0.00");
                                //油石比 = 
                                sItem["YSB" + i] = Round((GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) - (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                   + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim()))) / (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                   + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim())) * 100, 2).ToString("0.00");
                            }
                            //两次试验差值 大于0.3% 且小于0.5%  并且沥青混合料总质量3有值时 ，补充平行试验一次
                            if (Math.Abs(GetSafeDouble(sItem["LQHL1"]) - GetSafeDouble(sItem["LQHL2"])) > 0.3 && Math.Abs(GetSafeDouble(sItem["LQHL1"]) - GetSafeDouble(sItem["LQHL2"])) < 0.5)
                            {
                                if (IsNumeric(sItem["LQHHLZZL3"].Trim()) && IsNumeric(sItem["JLGZZL3"].Trim()) && IsNumeric(sItem["YZSYQHZZ3"].Trim()) && IsNumeric(sItem["XLCTYKFZL3"].Trim()))
                                {
                                    for (int i = 1; i < 4; i++)
                                    {
                                        //沥青含量  = （沥青混合料总质量 - 沥青混合料中矿料部分总质量）/ 沥青混合料总质量
                                        sItem["LQHL" + i] = Round((GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) - (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                            + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim()))) / GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) * 100, 2).ToString("0.00");
                                        //油石比 = 
                                        sItem["YSB" + i] = Round((GetSafeDouble(sItem["LQHHLZZL" + i].Trim()) - (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                           + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim()))) / (GetSafeDouble(sItem["JLGZZL" + i].Trim()) + GetSafeDouble(sItem["YZSYQHZZ" + i].Trim())
                                           + GetSafeDouble(sItem["XLCTYKFZL" + i].Trim())) * 100, 2).ToString("0.00");
                                    }
                                    //3次试验最大值与最小值之差不得大于5%
                                    lqhlList.Add(GetSafeDouble(sItem["LQHL1"]));
                                    lqhlList.Add(GetSafeDouble(sItem["LQHL2"]));
                                    lqhlList.Add(GetSafeDouble(sItem["LQHL3"]));
                                    lqhlList.Sort();
                                    if (Math.Abs(lqhlList[0] - lqhlList[2]) <= 5)
                                    {
                                        //计算平均沥青含量  平均油石比
                                        sItem["PJLQHL"] = Round((lqhlList[0] + lqhlList[1] + lqhlList[2]) / 3, 2).ToString("0.00");
                                        sItem["PJYSB"] = Round((GetSafeDouble(sItem["YSB1"]) + GetSafeDouble(sItem["YSB2"]) + GetSafeDouble(sItem["YSB3"])) / 3, 2).ToString("0.00");
                                    }
                                    else
                                    {
                                        //不知道该情况如何处理
                                        throw new SystemException("3次试验最大值与最小值之差大于5%");
                                    }
                                }
                                else
                                {
                                    throw new SystemException("两次试验差值大于0.3%且小于0.5% ，需补充平行试验一次，或补充平行试验所录数据有误。");
                                }
                            }
                            else
                            {
                                //无需追加试验计算平均值
                                sItem["PJLQHL"] = Round((GetSafeDouble(sItem["LQHL1"]) + GetSafeDouble(sItem["LQHL2"])) / 2, 2).ToString("0.00");
                                sItem["PJYSB"] = Round((GetSafeDouble(sItem["YSB1"]) + GetSafeDouble(sItem["YSB2"])) / 2, 2).ToString("0.00");
                            }
                        }
                        else
                        {
                            throw new SystemException("沥青含量数据录入有误");
                        }
                    }

                }
                else
                {
                    sItem["LQHL"] = "----";
                    sItem["YSB"] = "----";
                }
                #endregion

                #region 矿料级配
                if (jcxm.Contains("、矿料级配、"))
                {
                    string sfpd1, sfpd2, sfpd3, sfpd4, sfpd5, sfpd6, sfpd7, sfpd8, sfpd9, sfpd10, sfpd11, sfpd12, sfpd13, sfpd14, sfpd15;
                    jcxmCur = "矿料级配";

                    sItem["SFZL1"] = sItem["KLBFZZL1"];
                    sItem["SFZL2"] = sItem["KLBFZZL2"];

                    #region  平均筛余量
                    sItem["PJSYL53"] = Round((GetSafeDouble(sItem["SYL53_1"].Trim()) + GetSafeDouble(sItem["SYL53_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL375"] = Round((GetSafeDouble(sItem["SYL375_1"].Trim()) + GetSafeDouble(sItem["SYL375_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL315"] = Round((GetSafeDouble(sItem["SYL315_1"].Trim()) + GetSafeDouble(sItem["SYL315_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL265"] = Round((GetSafeDouble(sItem["SYL265_1"].Trim()) + GetSafeDouble(sItem["SYL265_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL19"] = Round((GetSafeDouble(sItem["SYL19_1"].Trim()) + GetSafeDouble(sItem["SYL19_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL16"] = Round((GetSafeDouble(sItem["SYL16_1"].Trim()) + GetSafeDouble(sItem["SYL16_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL132"] = Round((GetSafeDouble(sItem["SYL132_1"].Trim()) + GetSafeDouble(sItem["SYL132_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL95"] = Round((GetSafeDouble(sItem["SYL95_1"].Trim()) + GetSafeDouble(sItem["SYL95_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL475"] = Round((GetSafeDouble(sItem["SYL475_1"].Trim()) + GetSafeDouble(sItem["SYL475_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL236"] = Round((GetSafeDouble(sItem["SYL236_1"].Trim()) + GetSafeDouble(sItem["SYL236_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL118"] = Round((GetSafeDouble(sItem["SYL118_1"].Trim()) + GetSafeDouble(sItem["SYL118_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL06"] = Round((GetSafeDouble(sItem["SYL06_1"].Trim()) + GetSafeDouble(sItem["SYL06_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL03"] = Round((GetSafeDouble(sItem["SYL03_1"].Trim()) + GetSafeDouble(sItem["SYL03_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL015"] = Round((GetSafeDouble(sItem["SYL015_1"].Trim()) + GetSafeDouble(sItem["SYL015_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYL0075"] = Round((GetSafeDouble(sItem["SYL0075_1"].Trim()) + GetSafeDouble(sItem["SYL0075_2"].Trim())) / 2, 1).ToString("0.0");
                    sItem["PJSYLSD"] = Round((GetSafeDouble(sItem["SD1"].Trim()) + GetSafeDouble(sItem["SD2"].Trim())) / 2, 1).ToString("0.0");
                    #endregion

                    //平均筛余总量
                    pjsySum = Round(GetSafeDouble(sItem["PJSYL53"]) + GetSafeDouble(sItem["PJSYL375"]) + GetSafeDouble(sItem["PJSYL315"]) + GetSafeDouble(sItem["PJSYL265"])
                        + GetSafeDouble(sItem["PJSYL19"]) + GetSafeDouble(sItem["PJSYL16"]) + GetSafeDouble(sItem["PJSYL132"]) + GetSafeDouble(sItem["PJSYL95"])
                        + GetSafeDouble(sItem["PJSYL475"]) + GetSafeDouble(sItem["PJSYL236"]) + GetSafeDouble(sItem["PJSYL118"]) + GetSafeDouble(sItem["PJSYL06"])
                        + GetSafeDouble(sItem["PJSYL03"]) + GetSafeDouble(sItem["PJSYL015"]) + GetSafeDouble(sItem["PJSYL0075"]) + GetSafeDouble(sItem["PJSYLSD"]), 1);


                    #region 平均分计筛余 新
                    sItem["PJFJSY53"] = Round(GetSafeDouble(sItem["PJSYL53"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY375"] = Round(GetSafeDouble(sItem["PJSYL375"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY315"] = Round(GetSafeDouble(sItem["PJSYL315"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY265"] = Round(GetSafeDouble(sItem["PJSYL265"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY19"] = Round(GetSafeDouble(sItem["PJSYL19"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY16"] = Round(GetSafeDouble(sItem["PJSYL16"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY132"] = Round(GetSafeDouble(sItem["PJSYL132"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY95"] = Round(GetSafeDouble(sItem["PJSYL95"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY475"] = Round(GetSafeDouble(sItem["PJSYL475"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY236"] = Round(GetSafeDouble(sItem["PJSYL236"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY118"] = Round(GetSafeDouble(sItem["PJSYL118"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY06"] = Round(GetSafeDouble(sItem["PJSYL06"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY03"] = Round(GetSafeDouble(sItem["PJSYL03"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY015"] = Round(GetSafeDouble(sItem["PJSYL015"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSY0075"] = Round(GetSafeDouble(sItem["PJSYL0075"]) / pjsySum * 100, 1).ToString("0.0");
                    sItem["PJFJSYSD"] = Round(GetSafeDouble(sItem["PJSYLSD"]) / pjsySum * 100, 1).ToString("0.0");
                    #endregion

                    //筛余量统计
                    for (int i = 1; i < 3; i++)
                    {
                        sItem["SYLTJ" + i] = Round(GetSafeDouble(sItem["SYL53_" + i].Trim()) + GetSafeDouble(sItem["SYL375_" + i].Trim()) + GetSafeDouble(sItem["SYL315_" + i].Trim()) + GetSafeDouble(sItem["SYL265_" + i].Trim())
                                            + GetSafeDouble(sItem["SYL19_" + i].Trim()) + GetSafeDouble(sItem["SYL16_" + i].Trim()) + GetSafeDouble(sItem["SYL132_" + i].Trim()) + GetSafeDouble(sItem["SYL95_" + i].Trim())
                                            + GetSafeDouble(sItem["SYL475_" + i].Trim()) + GetSafeDouble(sItem["SYL236_" + i].Trim()) + GetSafeDouble(sItem["SYL118_" + i].Trim())
                                            + GetSafeDouble(sItem["SYL06_" + i].Trim()) + GetSafeDouble(sItem["SYL03_" + i].Trim()) + GetSafeDouble(sItem["SYL015_" + i].Trim()) + GetSafeDouble(sItem["SYL0075_" + i].Trim())
                                            + GetSafeDouble(sItem["SD" + i].Trim()), 1).ToString("0.0");
                    }

                    #region 分计筛余
                    for (int i = 1; i < 3; i++)
                    {
                        //分计筛余  = 筛余量 / 筛分质量   * 100  保留一位小数
                        sItem["FJSY53_" + i] = Round((GetSafeDouble(sItem["SYL53_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY375_" + i] = Round((GetSafeDouble(sItem["SYL375_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY315_" + i] = Round((GetSafeDouble(sItem["SYL315_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY265_" + i] = Round((GetSafeDouble(sItem["SYL265_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY19_" + i] = Round((GetSafeDouble(sItem["SYL19_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY16_" + i] = Round((GetSafeDouble(sItem["SYL16_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY132_" + i] = Round((GetSafeDouble(sItem["SYL132_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY95_" + i] = Round((GetSafeDouble(sItem["SYL95_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY475_" + i] = Round((GetSafeDouble(sItem["SYL475_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY236_" + i] = Round((GetSafeDouble(sItem["SYL236_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY118_" + i] = Round((GetSafeDouble(sItem["SYL118_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY06_" + i] = Round((GetSafeDouble(sItem["SYL06_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY03_" + i] = Round((GetSafeDouble(sItem["SYL03_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY015_" + i] = Round((GetSafeDouble(sItem["SYL015_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSY0075_" + i] = Round((GetSafeDouble(sItem["SYL0075_" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                        sItem["FJSYSD_" + i] = Round((GetSafeDouble(sItem["SD" + i].Trim()) / GetSafeDouble(sItem["SFZL" + i].Trim())) * 100, 1).ToString("0.0");
                    }
                    #endregion

                    #region 平均分计筛余 旧 数据失真
                    //sItem["PJFJSY53"] = Round((GetSafeDouble(sItem["FJSY53_1"]) + GetSafeDouble(sItem["FJSY53_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY375"] = Round((GetSafeDouble(sItem["FJSY375_1"]) + GetSafeDouble(sItem["FJSY375_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY315"] = Round((GetSafeDouble(sItem["FJSY315_1"]) + GetSafeDouble(sItem["FJSY315_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY265"] = Round((GetSafeDouble(sItem["FJSY265_1"]) + GetSafeDouble(sItem["FJSY265_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY19"] = Round((GetSafeDouble(sItem["FJSY19_1"]) + GetSafeDouble(sItem["FJSY19_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY16"] = Round((GetSafeDouble(sItem["FJSY16_1"]) + GetSafeDouble(sItem["FJSY16_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY132"] = Round((GetSafeDouble(sItem["FJSY132_1"]) + GetSafeDouble(sItem["FJSY132_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY95"] = Round((GetSafeDouble(sItem["FJSY95_1"]) + GetSafeDouble(sItem["FJSY95_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY475"] = Math.Round((Convert.ToDecimal(sItem["FJSY475_1"]) + Convert.ToDecimal(sItem["FJSY475_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY236"] = Round((GetSafeDouble(sItem["FJSY236_1"]) + GetSafeDouble(sItem["FJSY236_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY118"] = Round((GetSafeDouble(sItem["FJSY118_1"]) + GetSafeDouble(sItem["FJSY118_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY06"] = Round((GetSafeDouble(sItem["FJSY06_1"]) + GetSafeDouble(sItem["FJSY06_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY03"] = Round((GetSafeDouble(sItem["FJSY03_1"]) + GetSafeDouble(sItem["FJSY03_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY015"] = Round((GetSafeDouble(sItem["FJSY015_1"]) + GetSafeDouble(sItem["FJSY015_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSY0075"] = Round((GetSafeDouble(sItem["FJSY0075_1"]) + GetSafeDouble(sItem["FJSY0075_2"])) / 2, 1).ToString("0.0");
                    //sItem["PJFJSYSD"] = Round((GetSafeDouble(sItem["FJSYSD_1"]) + GetSafeDouble(sItem["FJSYSD_2"])) / 2, 1).ToString("0.0");
                    #endregion

                    #region 累计筛余
                    sItem["LJSY53"] = sItem["PJFJSY53"];
                    sItem["LJSY375"] = Round(GetSafeDouble(sItem["LJSY53"]) + GetSafeDouble(sItem["PJFJSY375"]), 1).ToString("0.0");
                    sItem["LJSY315"] = Round(GetSafeDouble(sItem["LJSY375"]) + GetSafeDouble(sItem["PJFJSY315"]), 1).ToString("0.0");
                    sItem["LJSY265"] = Round(GetSafeDouble(sItem["LJSY315"]) + GetSafeDouble(sItem["PJFJSY265"]), 1).ToString("0.0");
                    sItem["LJSY19"] = Round(GetSafeDouble(sItem["LJSY265"]) + GetSafeDouble(sItem["PJFJSY19"]), 1).ToString("0.0");
                    sItem["LJSY16"] = Round(GetSafeDouble(sItem["LJSY19"]) + GetSafeDouble(sItem["PJFJSY16"]), 1).ToString("0.0");
                    sItem["LJSY132"] = Round(GetSafeDouble(sItem["LJSY16"]) + GetSafeDouble(sItem["PJFJSY132"]), 1).ToString("0.0");
                    sItem["LJSY95"] = Round(GetSafeDouble(sItem["LJSY132"]) + GetSafeDouble(sItem["PJFJSY95"]), 1).ToString("0.0");
                    sItem["LJSY475"] = Round(GetSafeDouble(sItem["LJSY95"]) + GetSafeDouble(sItem["PJFJSY475"]), 1).ToString("0.0");
                    sItem["LJSY236"] = Round(GetSafeDouble(sItem["LJSY475"]) + GetSafeDouble(sItem["PJFJSY236"]), 1).ToString("0.0");
                    sItem["LJSY118"] = Round(GetSafeDouble(sItem["LJSY236"]) + GetSafeDouble(sItem["PJFJSY118"]), 1).ToString("0.0");
                    sItem["LJSY06"] = Round(GetSafeDouble(sItem["LJSY118"]) + GetSafeDouble(sItem["PJFJSY06"]), 1).ToString("0.0");
                    sItem["LJSY03"] = Round(GetSafeDouble(sItem["LJSY06"]) + GetSafeDouble(sItem["PJFJSY03"]), 1).ToString("0.0");
                    sItem["LJSY015"] = Round(GetSafeDouble(sItem["LJSY03"]) + GetSafeDouble(sItem["PJFJSY015"]), 1).ToString("0.0");
                    sItem["LJSY0075"] = Round(GetSafeDouble(sItem["LJSY015"]) + GetSafeDouble(sItem["PJFJSY0075"]), 1).ToString("0.0");
                    sItem["LJSYSD"] = Round(GetSafeDouble(sItem["LJSY0075"]) + GetSafeDouble(sItem["PJFJSYSD"]), 1).ToString("0.0");
                    #endregion

                    #region 通过百分率
                    sItem["TGBFL53"] = Round(100 - GetSafeDouble(sItem["LJSY53"]), 1).ToString("0.0");
                    sItem["TGBFL375"] = Round(100 - GetSafeDouble(sItem["LJSY375"]), 1).ToString("0.0");
                    sItem["TGBFL315"] = Round(100 - GetSafeDouble(sItem["LJSY315"]), 1).ToString("0.0");
                    sItem["TGBFL265"] = Round(100 - GetSafeDouble(sItem["LJSY265"]), 1).ToString("0.0");
                    sItem["TGBFL19"] = Round(100 - GetSafeDouble(sItem["LJSY19"]), 1).ToString("0.0");
                    sItem["TGBFL16"] = Round(100 - GetSafeDouble(sItem["LJSY16"]), 1).ToString("0.0");
                    sItem["TGBFL132"] = Round(100 - GetSafeDouble(sItem["LJSY132"]), 1).ToString("0.0");
                    sItem["TGBFL95"] = Round(100 - GetSafeDouble(sItem["LJSY95"]), 1).ToString("0.0");
                    sItem["TGBFL475"] = Round(100 - GetSafeDouble(sItem["LJSY475"]), 1).ToString("0.0");
                    sItem["TGBFL236"] = Round(100 - GetSafeDouble(sItem["LJSY236"]), 1).ToString("0.0");
                    sItem["TGBFL118"] = Round(100 - GetSafeDouble(sItem["LJSY118"]), 1).ToString("0.0");
                    sItem["TGBFL06"] = Round(100 - GetSafeDouble(sItem["LJSY06"]), 1).ToString("0.0");
                    sItem["TGBFL03"] = Round(100 - GetSafeDouble(sItem["LJSY03"]), 1).ToString("0.0");
                    sItem["TGBFL015"] = Round(100 - GetSafeDouble(sItem["LJSY015"]), 1).ToString("0.0");
                    sItem["TGBFL0075"] = Round(100 - GetSafeDouble(sItem["LJSY0075"]), 1).ToString("0.0");
                    sItem["TGBFLSD"] = Round(100 - GetSafeDouble(sItem["LJSYSD"]), 1).ToString("0.0");
                    #endregion

                    var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["KLJPLX"] == sItem["KLJPLX"]);
                    #region 获取标准值 判定
                    if (sItem["KLJPLX"].Contains("AC"))
                    {
                        //包含矿料级配类型数据 如果包含AC可能会进行关键孔判定  
                        mrsDj_Filter = mrsDj.FirstOrDefault(x => x["KLJPLX"] == sItem["KLJPLX"] && x["HHLMC"] == sItem["KLJPLX"]);
                        if (null != mrsDj_Filter)
                        {
                            gjkBool = true;
                            //取关键孔判定标准值
                            gjkTgl = mrsDj_Filter["GJSKTGL"];
                            //取标准值
                            mrsDj_Filter = mrsDj.FirstOrDefault(x => sItem["KLJPLX"].Contains(x["KLJPLX"]) && x["HHLMC"] != sItem["KLJPLX"]);
                        }
                        else
                        {
                            //不判定关键孔
                            mrsDj_Filter = mrsDj.FirstOrDefault(x => x["KLJPLX"] == sItem["KLJPLX"]);
                        }
                    }
                    else
                    {
                        mrsDj_Filter = mrsDj.FirstOrDefault(x => x["KLJPLX"] == sItem["KLJPLX"]);
                    }

                    if (null == mrsDj_Filter)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    sItem["BZFW53"] = mrsDj_Filter["BZFW53"];
                    sItem["BZFW375"] = mrsDj_Filter["BZFW375"];
                    sItem["BZFW315"] = mrsDj_Filter["BZFW315"];
                    sItem["BZFW265"] = mrsDj_Filter["BZFW265"];
                    sItem["BZFW19"] = mrsDj_Filter["BZFW19"];
                    sItem["BZFW16"] = mrsDj_Filter["BZFW16"];
                    sItem["BZFW132"] = mrsDj_Filter["BZFW132"];
                    sItem["BZFW95"] = mrsDj_Filter["BZFW95"];
                    sItem["BZFW475"] = mrsDj_Filter["BZFW475"];
                    sItem["BZFW236"] = mrsDj_Filter["BZFW236"];
                    sItem["BZFW118"] = mrsDj_Filter["BZFW118"];
                    sItem["BZFW06"] = mrsDj_Filter["BZFW06"];
                    sItem["BZFW03"] = mrsDj_Filter["BZFW03"];
                    sItem["BZFW015"] = mrsDj_Filter["BZFW015"];
                    sItem["BZFW0075"] = mrsDj_Filter["BZFW0075"];
                    #endregion

                    #region 一般性判定
                    if (100 == GetSafeDouble(sItem["BZFW53"]))
                    {
                        sfpd1 = GetSafeDouble(sItem["TGBFL53"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW53"] && 100 != GetSafeDouble(sItem["BZFW53"]))
                    {
                        sfpd1 = IsQualified(sItem["BZFW53"], sItem["TGBFL53"], false);
                    }
                    else
                    {
                        sItem["BZFW53"] = "----";
                        sfpd1 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW375"]))
                    {
                        sfpd2 = GetSafeDouble(sItem["TGBFL375"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW375"] && 100 != GetSafeDouble(sItem["BZFW375"]))
                    {
                        sfpd2 = IsQualified(sItem["BZFW375"], sItem["TGBFL375"], false);
                    }
                    else
                    {
                        sItem["BZFW375"] = "----";
                        sfpd2 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW315"]))
                    {
                        sfpd3 = GetSafeDouble(sItem["TGBFL315"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW315"] && 100 != GetSafeDouble(sItem["BZFW315"]))
                    {
                        sfpd3 = IsQualified(sItem["BZFW315"], sItem["TGBFL315"], false);
                    }
                    else
                    {
                        sItem["BZFW315"] = "----";
                        sfpd3 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW265"]))
                    {
                        sfpd4 = GetSafeDouble(sItem["TGBFL265"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW265"] && 100 != GetSafeDouble(sItem["BZFW265"]))
                    {
                        sfpd4 = IsQualified(sItem["BZFW265"], sItem["TGBFL265"], false);
                    }
                    else
                    {
                        sItem["BZFW265"] = "----";
                        sfpd4 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW19"]))
                    {
                        sfpd5 = GetSafeDouble(sItem["TGBFL19"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW19"] && 100 != GetSafeDouble(sItem["BZFW19"]))
                    {
                        sfpd5 = IsQualified(sItem["BZFW19"], sItem["TGBFL19"], false);
                    }
                    else
                    {
                        sItem["BZFW19"] = "----";
                        sfpd5 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW16"]))
                    {
                        sfpd6 = GetSafeDouble(sItem["TGBFL16"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW16"] && 100 != GetSafeDouble(sItem["BZFW16"]))
                    {
                        sfpd6 = IsQualified(sItem["BZFW16"], sItem["TGBFL16"], false);
                    }
                    else
                    {
                        sItem["BZFW16"] = "----";
                        sfpd6 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW132"]))
                    {
                        sfpd7 = GetSafeDouble(sItem["TGBFL132"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW132"] && 100 != GetSafeDouble(sItem["BZFW132"]))
                    {
                        sfpd7 = IsQualified(sItem["BZFW132"], sItem["TGBFL132"], false);
                    }
                    else
                    {
                        sItem["BZFW132"] = "----";
                        sfpd7 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW95"]))
                    {
                        sfpd8 = GetSafeDouble(sItem["TGBFL95"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW95"] && 100 != GetSafeDouble(sItem["BZFW95"]))
                    {
                        sfpd8 = IsQualified(sItem["BZFW95"], sItem["TGBFL95"], false);
                    }
                    else
                    {
                        sItem["BZFW95"] = "----";
                        sfpd8 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW475"]))
                    {
                        sfpd9 = GetSafeDouble(sItem["TGBFL475"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW475"] && 100 != GetSafeDouble(sItem["BZFW475"]))
                    {
                        sfpd9 = IsQualified(sItem["BZFW475"], sItem["TGBFL475"], false);
                    }
                    else
                    {
                        sItem["BZFW475"] = "----";
                        sfpd9 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW236"]))
                    {
                        sfpd10 = GetSafeDouble(sItem["TGBFL236"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW236"] && 100 != GetSafeDouble(sItem["BZFW236"]))
                    {
                        sfpd10 = IsQualified(sItem["BZFW236"], sItem["TGBFL236"], false);
                    }
                    else
                    {
                        sItem["BZFW236"] = "----";
                        sfpd10 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW118"]))
                    {
                        sfpd11 = GetSafeDouble(sItem["TGBFL118"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW118"] && 100 != GetSafeDouble(sItem["BZFW118"]))
                    {
                        sfpd11 = IsQualified(sItem["BZFW118"], sItem["TGBFL118"], false);
                    }
                    else
                    {
                        sItem["BZFW118"] = "----";
                        sfpd11 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW06"]))
                    {
                        sfpd12 = GetSafeDouble(sItem["TGBFL06"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW06"] && 100 != GetSafeDouble(sItem["BZFW06"]))
                    {
                        sfpd12 = IsQualified(sItem["BZFW06"], sItem["TGBFL06"], false);
                    }
                    else
                    {
                        sItem["BZFW06"] = "----";
                        sfpd12 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW03"]))
                    {
                        sfpd13 = GetSafeDouble(sItem["TGBFL03"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW03"] && 100 != GetSafeDouble(sItem["BZFW03"]))
                    {
                        sfpd13 = IsQualified(sItem["BZFW03"], sItem["TGBFL03"], false);
                    }
                    else
                    {
                        sItem["BZFW03"] = "----";
                        sfpd13 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW015"]))
                    {
                        sfpd14 = GetSafeDouble(sItem["TGBFL015"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW015"] && 100 != GetSafeDouble(sItem["BZFW015"]))
                    {
                        sfpd14 = IsQualified(sItem["BZFW015"], sItem["TGBFL015"], false);
                    }
                    else
                    {
                        sItem["BZFW015"] = "----";
                        sfpd14 = "----";
                    }

                    if (100 == GetSafeDouble(sItem["BZFW0075"]))
                    {
                        sfpd15 = GetSafeDouble(sItem["TGBFL0075"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != sItem["BZFW0075"] && 100 != GetSafeDouble(sItem["BZFW0075"]))
                    {
                        sfpd15 = IsQualified(sItem["BZFW0075"], sItem["TGBFL0075"], false);
                    }
                    else
                    {
                        sItem["BZFW0075"] = "----";
                        sfpd15 = "----";
                    }
                    #endregion

                    #region 关键孔判定
                    if (gjkBool)
                    {
                        //关键性筛孔 4.75 mm
                        if (sItem["KLJPLX"].Contains("AC-25") || sItem["KLJPLX"].Contains("AC-20"))
                        {
                            if (sfpd9 == "合格")
                            {
                                if ("合格" != IsQualified(gjkTgl, sItem["TGBFL475"], false))
                                {
                                    sfpd9 = "不合格";
                                }
                            }
                        }
                        //关键性筛孔 2.36 mm
                        else if (sItem["KLJPLX"].Contains("AC-16") || sItem["KLJPLX"].Contains("AC-13") || sItem["KLJPLX"].Contains("AC-10"))
                        {
                            if (sfpd10 == "合格")
                            {
                                if ("合格" != IsQualified(gjkTgl, sItem["TGBFL236"], false))
                                {
                                    sfpd10 = "不合格";
                                }
                            }
                        }
                    }
                    #endregion

                    #region 综合判定
                    if (sfpd1 == "不合格" || sfpd2 == "不合格" || sfpd3 == "不合格" || sfpd4 == "不合格" || sfpd5 == "不合格" || sfpd6 == "不合格"
                         || sfpd7 == "不合格" || sfpd8 == "不合格" || sfpd9 == "不合格" || sfpd10 == "不合格" || sfpd11 == "不合格"
                         || sfpd12 == "不合格" || sfpd13 == "不合格" || sfpd15 == "不合格" || sfpd14 == "不合格")
                    {
                        sItem["JPDXPD"] = "不符合标准要求";
                        mAllHg = false;
                        itemHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        sItem["JPDXPD"] = "符合标准要求";
                    }
                    #endregion
                }
                else
                {
                    sItem["JPDXPD"] = "----";
                }
                #endregion

                if (itemHg)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }

            }
            #region 添加最终报告
            if (mAllHg)
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
            /************************ 代码结束 *********************/
        }
    }
}
