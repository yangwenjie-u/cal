using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class XGL2 : BaseMethods
    {
        //使用标准 GBT 14684-2011 建设用砂
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            var extraDJData = dataExtra["BZ_XGL_DJ"];
            var extraHSB = dataExtra["BZ_XGLHSB"];


            var jcxmItems = retData.Select(u => u.Key).ToArray();
            int forEachFlag = 0;
            int mbhggs = 0;//总报告合格数
            bool mFlag_Hg = false;
            bool mFlag_BHg = false;  //报告不合格
            double md = 0, md1 = 0, md2 = 0, cd1 = 0, cd2 = 0;

            foreach (var jcxm in jcxmItems)
            {
                switch (jcxm.Trim())
                {
                    case "级配":
                        var JPItems = retData[jcxm]["S_XGL"];
                        var XQ_ZXBHData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        break;
                    case "细度模数":
                        var WGItems = retData[jcxm]["S_XGL"];
                        var M_WGItems = retData[jcxm]["M_XGL"];
                        var XQ_WGData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_WGItems)
                        {
                            List<double> list1 = new List<double>();
                            List<double> list2 = new List<double>();
                            for (int i = 1; i < 6; i++)
                            {
                                list1.Add(Math.Round((100 * GetSafeDouble(item["syzl" + i]) / 500), 1));
                                list2.Add(Math.Round((100 * GetSafeDouble(item["syzl" + i + "_2"]) / 500), 1));
                            }
                            list1.Add(Math.Round((100 * GetSafeDouble(item["dpzl"]) / 500), 1));
                            list2.Add(Math.Round((100 * GetSafeDouble(item["dpzl_2"]) / 500), 1));

                            //计算筛余质量
                            for (int i = 1; i < 7; i++)
                            {
                                list1[i] = list1[i] + list1[i - 1];
                                list2[i] = list1[i] + list1[i - 1];
                            }

                            for (int i = 1; i < 7; i++)
                            {
                                md1 = md1 + list1[i] - list1[i - 1];
                                md2 = md2 + list2[i] - list2[i - 1];
                            }
                            md1 = Math.Round((md1 / (100 - list1[0])), 2);
                            md2 = Math.Round((md2 / (100 - list2[0])), 2);
                            md = (md1 + md2) / 2;
                            item["xdms"] = md.ToString("0.0");

                            if (md > 3.1 && md <= 3.7)
                            {
                                item["xdmspd"] = "粗砂";
                                mFlag_Hg = true;
                            }
                            else if (md > 2.3 && md <= 3.1)
                            {
                                item["xdmspd"] = "中砂";
                                mFlag_Hg = true;
                            }
                            else if (md > 1.6 && md <= 2.3)
                            {
                                item["xdmspd"] = "细砂";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                item["xdmspd"] = "不符合";
                                mbhggs += 1;
                                mFlag_BHg = false;
                            }

                            md = Math.Abs(md1 - md2);

                            if (md < 0.2)
                            {
                                mFlag_Hg = true;
                                item["xdmspd"] = "细度模数两试验数据差值大于0.2试验需重做";
                                item["jppd"] = item["jppd"] + item["xdmspd"];
                                mbhggs += 1;
                            }
                            else
                            {
                                mFlag_BHg = true;
                            }
                            forEachFlag++;
                        }
                        break;

                    case "含泥量":
                        var HNLItems = retData[jcxm]["S_XGL"];
                        var XQ_WQData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in HNLItems)
                        {
                            cd1 = GetSafeDouble(item["hnlg0"].Trim());
                            cd2 = GetSafeDouble(item["hnlg1"].Trim());
                            md1 = 100 * (cd1 - cd2) / cd1;

                            cd1 = GetSafeDouble(item["hnlg0_2"].Trim());
                            cd2 = GetSafeDouble(item["hnlg1_2"].Trim());
                            md2 = 100 * (cd1 - cd2) / cd1;

                            item["hnl"] = ((md1 + md2) / 2).ToString("0.0");

                            //获取标准
                            var extraFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("hnl") && u.Values.Contains(item["hnl"].Trim()) && u.Values.Contains(item["SJDJ"].Trim())));

                            if (extraFields.Count() == 0)
                            {
                                mFlag_BHg = true;
                            }
                            if (item["hnl"] == extraFields["hnl"])
                            {
                                item["hnlpd"] = extraFields["mc"];
                                mFlag_Hg = true;
                            }

                            forEachFlag++;
                        }
                        break;
                    case "泥块含量":
                        var NKHLItems = retData[jcxm]["S_XGL"];
                        var XQ_NKHLData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in NKHLItems)
                        {
                            cd1 = GetSafeDouble(item["nkhlg1"].Trim());
                            cd2 = GetSafeDouble(item["nkhlg2"].Trim());
                            md1 = 100 * (cd1 - cd2) / cd1;

                            cd1 = GetSafeDouble(item["nkhlg1_2"].Trim());
                            cd2 = GetSafeDouble(item["nkhlg2_2"].Trim());
                            md2 = 100 * (cd1 - cd2) / cd1;

                            item["nkhl"] = ((md1 + md2) / 2).ToString("0.0");

                            //获取标准
                            var extraFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("nkhl") && u.Values.Contains(item["nkhl"].Trim()) && u.Values.Contains(item["SJDJ"].Trim())));

                            if (extraFields.Count() == 0)
                            {
                                item["nkhlpd"] = "不符合";
                                mFlag_BHg = true;
                            }

                            if (item["nkhl"] == extraFields["nkhl"])
                            {
                                item["nkhlpd"] = extraFields["mc"];
                                mFlag_Hg = true;
                            }
                            else
                            {
                                //不合格
                            }

                            forEachFlag++;
                        }
                        break;
                    case "堆积密度":
                        var DJMDItems = retData[jcxm]["S_XGL"];
                        var XQ_DJMDData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in DJMDItems)
                        {
                            cd1 = GetSafeDouble(item["djmdg1"].Trim());
                            cd2 = GetSafeDouble(item["djmdg2"].Trim());
                            md1 = GetSafeDouble(item["djmdv"].Trim());
                            md1 = Math.Round(((cd1 - cd2) / (10 * md1)), 0) * 10;
                            item["kxlp1"] = md1.ToString("0");

                            cd1 = GetSafeDouble(item["djmdg1_2"].Trim());
                            cd2 = GetSafeDouble(item["djmdg2_2"].Trim());
                            md2 = GetSafeDouble(item["djmdv"].Trim());
                            md2 = Math.Round(((cd1 - cd2) / (10 * md2)), 0) * 10;
                            item["kxlp1_2"] = md2.ToString("0");

                            //获取标准
                            var extraFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("djmd") && u.Values.Contains(item["djmd"].Trim()) && u.Values.Contains(item["SJDJ"].Trim())));

                            if (extraFields.Count() == 0)
                            {
                                item["djmdpd"] = "不符合";
                                mFlag_BHg = true;
                            }

                            if (item["djmd"] == extraFields["djmd"])
                            {
                                item["nkhlpd"] = "符合";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                item["djmdpd"] = "不符合";
                                mFlag_BHg = true;
                                //不合格
                            }
                            forEachFlag++;
                        }
                        break;
                    case "紧密密度":
                        var JMDItems = retData[jcxm]["S_XGL"];
                        var XQ_JMDData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in JMDItems)
                        {
                            cd1 = GetSafeDouble(item["jmmdg1"].Trim());
                            cd2 = GetSafeDouble(item["jmmdg2"].Trim());
                            md1 = GetSafeDouble(item["jmmdv"].Trim());
                            md1 = Math.Round(((cd1 - cd2) / (10 * md1)), 0) * 10;

                            cd1 = GetSafeDouble(item["jmmdg1_2"].Trim());
                            cd2 = GetSafeDouble(item["jmmdg2_2"].Trim());
                            md2 = GetSafeDouble(item["jmmdv"].Trim());
                            md2 = Math.Round(((cd1 - cd2) / (10 * md2)), 0) * 10;
                            item["jmmd"] = (Math.Round(((md1 + md2) / 20), 0) * 10).ToString("0");

                            //获取标准
                            var extraFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("jmmd") && u.Values.Contains(item["jmmd"].Trim()) && u.Values.Contains(item["SJDJ"].Trim())));

                            if (extraFields.Count() == 0)
                            {
                                item["jmmdpd"] = "不符合";
                                mFlag_BHg = true;
                            }

                            if (item["jmmdpd"] == extraFields["jmmdpd"])
                            {
                                item["jmmdpd"] = "符合";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                item["jmmdpd"] = "不符合";
                                mFlag_BHg = true;
                                //不合格
                            }
                            forEachFlag++;
                        }
                        break;
                    case "表观密度":
                        var BGMDItems = retData[jcxm]["S_XGL"];
                        var XQ_BGMDData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in BGMDItems)
                        {
                            cd1 = GetSafeDouble(item["bgmdg0"].Trim());
                            cd2 = GetSafeDouble(item["bgmdg2"].Trim());
                            md1 = GetSafeDouble(item["bgmdg1"].Trim());
                            md1 = Math.Round(100 * cd1 / (cd1 + cd2 - md1), 0) * 10;
                            item["kxlp2"] = md1.ToString("0");

                            cd1 = GetSafeDouble(item["bgmdg0_2"].Trim());
                            cd2 = GetSafeDouble(item["bgmdg2_2"].Trim());
                            md2 = GetSafeDouble(item["bgmdg1_2"].Trim());
                            md2 = Math.Round(100 * cd1 / (cd1 + cd2 - md1), 0) * 10;
                            item["kxlp2_2"] = md2.ToString("0");

                            md = (md1 + md2) / 20;
                            item["bgmd"] = (Math.Round(md, 0) * 10).ToString("0");
                            //获取标准
                            var extraFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("bgmd") && u.Values.Contains(item["bgmd"].Trim()) && u.Values.Contains(item["SJDJ"].Trim())));

                            if (extraFields.Count() == 0)
                            {
                                item["bgmdpd"] = "不符合";
                                mFlag_BHg = true;
                            }

                            if (item["jmmdpd"] == extraFields["jmmdpd"])
                            {
                                item["bgmdpd"] = "符合";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                item["bgmdpd"] = "不符合";
                                mFlag_BHg = true;
                                //不合格
                            }

                            md = Math.Abs(md1 - md2);
                            if (md > 20)
                            {
                                item["bgmdpd"] = "两次结果差大于20，须重新试验";
                                mFlag_BHg = true;
                            }
                            else
                            {
                                mFlag_Hg = true;
                            }
                            forEachFlag++;
                        }
                        break;
                    case "空隙率":
                        var KXLItems = retData[jcxm]["S_XGL"];
                        var XQ_KXLData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in KXLItems)
                        {
                            md = 0;
                            cd1 = GetSafeDouble(item["kxlp1"].Trim());
                            cd2 = GetSafeDouble(item["kxlp2"].Trim());
                            md1 = Math.Round(100 * (1 - cd1 / cd2), 0);

                            cd1 = GetSafeDouble(item["kxlp1_2"].Trim());
                            cd2 = GetSafeDouble(item["kxlp2_2"].Trim());
                            md2 = Math.Round(100 * (1 - cd1 / cd2), 0);
                            md = Math.Round((md1 + md2) / 2, 0);
                            item["kxl"] = md.ToString("0");

                            item["bgmd"] = (Math.Round(md, 0) * 10).ToString("0");
                            //获取标准
                            var extraFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("kxl") && u.Values.Contains(item["kxl"].Trim()) && u.Values.Contains(item["SJDJ"].Trim())));

                            if (extraFields.Count() == 0)
                            {
                                item["kxlpd"] = "不符合";
                                mFlag_BHg = true;
                            }

                            if (item["kxl"] == extraFields["kxl"])
                            {
                                item["kxlpd"] = "符合";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                item["kxlpd"] = "不符合";
                                mFlag_BHg = true;
                                //不合格
                            }


                            forEachFlag++;
                        }
                        break;
                    case "氯离子含量":
                        var LLZItems = retData[jcxm]["S_XGL"];
                        var XQ_DLData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in LLZItems)
                        {
                            cd1 = GetSafeDouble(item["llzv"].Trim());
                            cd2 = GetSafeDouble(item["llzv0"].Trim());
                            md1 = 35.5 * (cd1 - cd2) * cd1 / 500;

                            cd1 = GetSafeDouble(item["llzv_2"].Trim());
                            cd2 = GetSafeDouble(item["llzv0_2"].Trim());
                            md2 = 35.5 * (cd1 - cd2) * cd1 / 500;

                            item["llzhl"] = ((md1 + md2) / 2).ToString("0.00");

                            //获取标准
                            var extraFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("LLZHL") && u.Values.Contains(item["llzhl"].Trim()) && u.Values.Contains(item["SJDJ"].Trim())));
                            if (extraFields.Count() > 0)
                            {
                                if (extraFields["MC"].Trim() == item["SJDJ"].Trim())
                                {
                                    item["LLZHLPD"] = "符合";
                                    mFlag_Hg = true;

                                    //item["G_LLZHL"] =extraFields["MC"];
                                }
                            }
                            else
                            {
                                item["LLZHLPD"] = "不符合";
                                mFlag_BHg = true;
                            }
                            forEachFlag++;
                        }
                        break;
                    case "吸水率":
                        var XSLItems = retData[jcxm]["S_XGL"];
                        var XQ_SXLData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in XSLItems)
                        {
                            cd1 = GetSafeDouble(item["xslg1"].Trim());
                            cd2 = GetSafeDouble(item["xslg2"].Trim());
                            md1 = Math.Round(100 * (500 - (cd2 - cd1)) / (cd2 - cd1), 1);

                            cd1 = GetSafeDouble(item["xslg1_2"].Trim());
                            cd2 = GetSafeDouble(item["xslg2_2"].Trim());
                            md2 = Math.Round(100 * (500 - (cd2 - cd1)) / (cd2 - cd1), 1);

                            md = (md1 + md2) / 2;
                            item["xsl"] = Math.Round(md, 1).ToString("0.0");
                            item["xslpd"] = Math.Abs(md1 - md2) > 0.2 ? "两次结果差大于0.2，须重新试验" : "----"; ;

                            //获取标准
                            var extraFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("LLZHL") && u.Values.Contains(item["llzhl"].Trim())));
                            if (extraFields.Count() > 0)
                            {
                                if (extraFields["MC"].Trim() == item["SJDJ"].Trim())
                                {
                                    item["LLZHLPD"] = "符合";
                                    //item["G_LLZHL"] =extraFields["MC"];
                                }
                            }
                            else
                            {
                                item["LLZHLPD"] = "不符合";
                            }
                            forEachFlag++;
                        }
                        break;
                    case "云母含量":
                        var YMHLItems = retData[jcxm]["S_XGL"];
                        var XQ_YMHLData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in YMHLItems)
                        {
                            cd1 = GetSafeDouble(item["ymg1"].Trim());
                            cd2 = GetSafeDouble(item["ymg2"].Trim());
                            md1 = Math.Round(100 * cd2 / cd1, 1);

                            cd1 = GetSafeDouble(item["ymg1_2"].Trim());
                            cd2 = GetSafeDouble(item["ymg2_2"].Trim());
                            md2 = Math.Round(100 * cd2 / cd1, 1);

                            md = (md1 + md2) / 2;
                            item["ym"] = Math.Round(md, 1).ToString("0.0");

                            //获取标准
                            var extraFields = extraDJData.FirstOrDefault(u => (u.ContainsKey("ym") && u.Values.Contains(item["ym"].Trim()) && u.Values.Contains(item["SJDJ"].Trim())));
                            if (extraFields.Count() > 0)
                            {
                                item["ympd"] = extraFields["MC"];
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_BHg = true;
                                item["ympd"] = "不符合";
                            }

                            if (Math.Abs(md1 - md2) > 0.2)
                            {
                                item["ympd"] = "两次结果差大于0.2，须重新试验";
                            }

                            forEachFlag++;
                        }
                        break;
                    case "含水率":
                        var HSLItems = retData[jcxm]["S_XGL"];
                        var XQ_HSLData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in HSLItems)
                        {
                            cd1 = GetSafeDouble(item["hslg1"].Trim());
                            cd2 = GetSafeDouble(item["hslg2"].Trim());
                            md1 = Math.Round(100 * (cd2 - cd1) / cd1, 1);

                            cd1 = GetSafeDouble(item["hslg1_2"].Trim());
                            cd2 = GetSafeDouble(item["hslg1_2"].Trim());
                            md2 = Math.Round(100 * (cd2 - cd1) / cd1, 1);

                            md = (md1 + md2) / 2;
                            item["hsl"] = Math.Round(md, 1).ToString("0.0");


                            if (Math.Abs(md1 - md2) > 20)
                            {
                                item["hslpd"] = "两次结果差大于0.2，须重新试验";
                            }

                            forEachFlag++;
                        }
                        break;
                    case "有机物含量":
                        break;
                    case "碱活性":
                        break;
                    case "硫化物和硫酸盐含量":
                        break;
                    case "坚固性":
                        break;
                    default:
                        break;
                }
            }

            #region 添加最终报告
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            if (mbhggs != 0)
            {
                //不合格
                bgjgDic.Add("JCJG", "不合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。");
            }
            else
            {
                bgjgDic.Add("JCJG", "合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。");
            }

            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion
            return true;
            /************************ 代码结束 *********************/
        }

    }
}
