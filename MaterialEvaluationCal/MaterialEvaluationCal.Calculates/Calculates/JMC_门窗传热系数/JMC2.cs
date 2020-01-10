using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class JMC2 : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/

            var extraDJData = dataExtra["BZ_JMC_DJ"];
            var extraBWPJ = dataExtra["BZ_JMCBWFJ"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();
            int forEachFlag = 0;
            foreach (var jcxm in jcxmItems)
            {
                switch (jcxm)
                {
                    case "传热系数":
                        var CRXSItems = retData[jcxm]["S_JMC"];
                        var M_CRXSItems = retData[jcxm]["M_JMC"];
                        forEachFlag = 0;

                        foreach (var item in CRXSItems)
                        {
                            #region 转换试件传热系数
                            double crxs = 0;
                            string crxsStr = "";

                            if (IsNumeric(item["CRXSSJZ"]))
                            {
                                crxs = GetDouble(item["CRXS"]);
                            }
                            else
                            {

                                forEachFlag++;
                                break;
                            }

                            if (crxs == 0)
                            {
                            }
                            else if (crxs >= 5.0)
                            {
                                crxsStr = "K≥5.0";
                            }
                            else if (5.0 > crxs && crxs >= 4.0)
                            {
                                crxsStr = "5.0＞K≥4.0";
                            }
                            else if (4.0 > crxs && crxs >= 3.5)
                            {
                                crxsStr = "4.0＞K≥3.5";
                            }
                            else if (3.5 > crxs && crxs >= 3.0)
                            {
                                crxsStr = "3.5＞K≥3.0";
                            }
                            else if (3.0 > crxs && crxs >= 2.5)
                            {
                                crxsStr = "3.0＞K≥2.5";
                            }
                            else if (2.5 > crxs && crxs >= 2.0)
                            {
                                crxsStr = "2.5＞K≥2.0";
                            }
                            else if (2.0 > crxs && crxs >= 1.6)
                            {
                                crxsStr = "2.0＞K≥1.6";
                            }
                            else if (1.6 > crxs && crxs >= 1.3)
                            {
                                crxsStr = "1.6＞K≥1.3";
                            }
                            else if (1.3 > crxs && crxs >= 1.1)
                            {
                                crxsStr = "1.3＞K≥1.1";
                            }
                            else if (crxs < 1.1)
                            {
                                crxsStr = "K＜1.1";
                            }

                            #endregion

                            var extraLSFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("DJFW") && u.Values.Contains(crxsStr)));

                            // 修改主表数据
                            if (null == extraLSFields)//没有找个符合的级别
                            {
                                M_CRXSItems[forEachFlag]["SSFJ"] = "不符合任一级别"; //所属分级
                                M_CRXSItems[forEachFlag]["SSFW"] = "----"; //所属等级范围
                            }
                            else
                            {
                                M_CRXSItems[forEachFlag]["SSFJ"] = extraLSFields["DJBH"] + "级"; //所属分级
                                M_CRXSItems[forEachFlag]["SSFW"] = extraLSFields["DJFW"]; //所属等级范围
                            }

                            if (IsNumeric(item["CRXSSJZ"]))
                            {
                                item["GH_CRXS"] = GetDouble(item["CRXS"]) <= GetDouble(item["CRXSSJZ"]) ? "符合" : "不符合";
                                M_CRXSItems[forEachFlag]["JGSM"] = "该试样传热系数" + item["GH_CRXS"] + "设计要求。";
                            }
                            else
                            {
                                item["GH_CRXS"] = "----";
                                M_CRXSItems[forEachFlag]["JGSM"] = "该试样传热系数不符合设计要求。";
                            }

                            // DJBH  等级编号   5   标准表获取 extraLSFields["DJBH"]
                            // 所属分级指标  5.0＞K≥4.0 extraLSFields["DJFW"]
                            forEachFlag++;
                        }
                        break;
                    case "抗结露因子":
                        var JLYZItems = retData["传热系数"]["S_JMC"];
                        var M_JLYZItems = retData[jcxm]["M_JMC"];
                        forEachFlag = 0;

                        foreach (var item in JLYZItems)
                        {
                            #region 转换试件传热系数
                            string jlyzStr = "";
                            double jlyz = 0;

                            if (IsNumeric(item["CRXSSJZ"]))
                            {
                                jlyz = GetDouble(item["JLYZ"]); ;
                            }

                            if (jlyz == 0)
                            {

                            }
                            else if (jlyz <= 35)
                            {
                                jlyzStr = "K≤35";
                            }
                            else if (35 < jlyz && jlyz <= 40)
                            {
                                jlyzStr = "35＜K≤40";
                            }
                            else if (40 < jlyz && jlyz <= 45)
                            {
                                jlyzStr = "40＜K≤45";
                            }
                            else if (45 < jlyz && jlyz <= 50)
                            {
                                jlyzStr = "45＜K≤50";
                            }
                            else if (50 < jlyz && jlyz <= 55)
                            {
                                jlyzStr = "50＜K≤55";
                            }
                            else if (55 < jlyz && jlyz <= 60)
                            {
                                jlyzStr = "55＜K≤60";
                            }
                            else if (60 < jlyz && jlyz <= 65)
                            {
                                jlyzStr = "60＜K≤65";
                            }
                            else if (65 < jlyz && jlyz <= 70)
                            {
                                jlyzStr = "65＜K≤70";
                            }
                            else if (70 < jlyz && jlyz <= 75)
                            {
                                jlyzStr = "70＜K≤75";
                            }
                            else if (jlyz > 75)
                            {
                                jlyzStr = "K＞75";
                            }

                            #endregion
                            var extraJLYZFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("KJLDJFW") && u.Values.Contains(jlyzStr)));

                            if (null == extraJLYZFields)
                            {
                                //抗结露因子
                                M_JLYZItems[forEachFlag]["SSFJ"] = "不符合任一级别";
                                item["GH_JLYZ"] = "不符合";
                            }
                            else
                            {
                                M_JLYZItems[forEachFlag]["SSFJ"] = extraJLYZFields["DJBH"] + "级";
                                item["GH_JLYZ"] = "符合";
                            }
                            forEachFlag++;
                        }
                        break;

                    default:
                        break;
                }
            }

            //添加最终报告
            return true;
            /************************ 代码结束 *********************/
        }
    }
}
