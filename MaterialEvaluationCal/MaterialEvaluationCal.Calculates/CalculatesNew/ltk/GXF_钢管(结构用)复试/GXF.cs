﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GXF : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_GXF_DJ"];

            //var jcxmItems = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            var S_GXFS = data["S_GXF"];

            if (!data.ContainsKey("M_GXF"))
            {
                data["M_GXF"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GXF"];
            //if (M_GXF == default || M_HNT.Count == 0)
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            bool itemHG = true;//判断单组是否合格

            double md1, md2, md, pjmd = 0;
            double sum = 0;
            double sum2 = 0;
            double sum3 = 0;
            double sum4 = 0;
            bool flag, sign = false;
            bool SFlg = false;//是否两根
            int mbHggs = 0;//检测项目合格数量

            //遍历从表数据
            foreach (var sItem in S_GXFS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if (string.IsNullOrEmpty(sItem["GGWJ"]) || !IsNumeric(sItem["GGWJ"]))
                {
                    sItem["GGWJ"] = "48.3";
                }
                if (string.IsNullOrEmpty(sItem["GGBH"]) || !IsNumeric(sItem["GGBH"]))
                {
                    sItem["GGBH"] = "3.5";
                }
                switch (sItem["YPMC"])
                {
                    case "低合金高强度结构钢钢管":
                    case "合金钢钢管":
                        SFlg = true;
                        break;
                    case "结构用无缝钢管":
                    case "优质碳素结构钢钢管":
                    case "结构用无缝钢管(对接焊)":
                        SFlg = true;
                        break;
                    default:
                        SFlg = false;
                        break;
                }
                //mrsmainTable.Fields("which") = IIf(SFlg, "bggx_1", "bggx")

                //从设计等级表中取得相应的计算数值、等级标准
                //var extraFieldsDj = extraDJ;
                //if (null == extraFieldsDj)
                //{
                //    sItem["JCJG"] = "依据不详";
                //    //MItem["BGBH"] = "";
                //    //MItem["JSBEIZHU"] = M_GXFS[0]["JSBEIZHU"] + "单组流水号" + sItem["DZBH"] + "试件尺寸为空";                  
                //    continue;
                //}
                foreach (var extraFieldsDj in extraDJ)
                {
                    if (sItem["GGPH"].Trim() == extraFieldsDj["GGPH"].Trim() && sItem["YPMC"].Trim() == extraFieldsDj["GGLB"].Trim())
                    {
                        switch (sItem["YPMC"].Trim())
                        {
                            case "低压流体输送用焊接钢管":
                                if (IsQualified("≤16", sItem["GGBH"], false) == "合格")
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD1"].Trim();
                                }
                                else
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD2"].Trim();
                                }
                                if (IsQualified("≤168.3", sItem["GGWJ"], false) == "合格")
                                {
                                    sItem["G_SCL"] = extraFieldsDj["SCL1"].Trim();
                                }
                                else
                                {
                                    sItem["G_SCL"] = extraFieldsDj["SCL2"].Trim();
                                }
                                sItem["G_YB"] = "压扁试验后不得出现裂纹、分层";
                                break;

                            case "合金钢钢管":
                            case "直缝电焊钢管":
                                sItem["G_QFQD"] = extraFieldsDj["QFQD1"].Trim();
                                sItem["G_SCL"] = extraFieldsDj["SCL1"].Trim();
                                sItem["G_YB"] = "压扁试验后不得出现裂纹或裂口";
                                break;

                            default:
                                if (IsQualified("≤16", sItem["GGBH"], false) == "合格")
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD1"].Trim();
                                }
                                else if (IsQualified("＞30", sItem["GGBH"], false) == "合格")
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD3"].Trim();
                                }
                                else
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD2"].Trim();
                                }
                                sItem["G_SCL"] = extraFieldsDj["SCL1"].Trim();
                                sItem["G_YB"] = "压扁试验后不得出现裂纹或裂口";
                                break;
                        }
                        sItem["G_KLQD"] = extraFieldsDj["KLQD"].Trim();
                        sItem["G_LW"] = "弯心d为6D弯曲角度90°受弯部位表面不得产生裂纹";
                        break;
                    }
                }


                #region 压扁
                if (jcxm.Contains("、压扁、"))
                {
                    if (sItem["YB1"].Trim() != "0" && sItem["YB2"].Trim() != "0" && sItem["YB3"].Trim() != "0" && sItem["YB4"].Trim() != "0")
                    {
                        sItem["HG_YB"] = "合格";
                        mbHggs++;
                    }
                    else
                    {
                        sItem["HG_YB"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                }
                else
                {
                    sItem["HG_YB"] = "----";
                    sItem["YB1"] = "----";
                    sItem["YB2"] = "----";
                    sItem["G_YB"] = "----";
                    sItem["YB3"] = "----";
                    sItem["YB4"] = "----";
                }
                #endregion

                #region  弯曲
                if (jcxm.Contains("、弯曲、"))
                {
                    if (SFlg)
                    {
                        sItem["HG_LW"] = sItem["LW"].Trim() != "0" && sItem["LW2"].Trim() != "0" && sItem["LW3"].Trim() != "0" && sItem["LW4"].Trim() != "0" ? "合格" : "不合格";
                        if (sItem["HG_LW"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sItem["HG_LW"] = sItem["LW"].Trim() != "0" && sItem["LW2"].Trim() != "0" ? "合格" : "不合格";
                        if (sItem["HG_LW"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                }
                else
                {
                    sItem["HG_LW"] = "----";
                    sItem["LW"] = "----";
                    sItem["LW2"] = "----";
                    sItem["G_LW"] = "----";
                    sItem["LW3"] = "----";
                    sItem["LW4"] = "----";
                }
                #endregion

                #region  拉伸
                if (jcxm.Contains("、拉伸、"))
                {
                    sign = true;
                    flag = true;
                    flag = IsNumeric(sItem["QFHZ"]) ? flag : false;
                    flag = IsNumeric(sItem["KLHZ"]) ? flag : false;
                    flag = IsNumeric(sItem["SCZ"]) ? flag : false;
                    flag = IsNumeric(sItem["QFHZ2"]) ? flag : false;
                    flag = IsNumeric(sItem["KLHZ2"]) ? flag : false;
                    flag = IsNumeric(sItem["SCZ2"]) ? flag : false;
                    if (SFlg)
                    {
                        flag = IsNumeric(sItem["QFHZ3"]) ? flag : false;
                        flag = IsNumeric(sItem["KLHZ3"]) ? flag : false;
                        flag = IsNumeric(sItem["SCZ3"]) ? flag : false;
                        flag = IsNumeric(sItem["QFHZ4"]) ? flag : false;
                        flag = IsNumeric(sItem["KLHZ4"]) ? flag : false;
                        flag = IsNumeric(sItem["SCZ4"]) ? flag : false;
                    }

                    //通过截取试样
                    if (IsNumeric(sItem["SYHD"]) && IsNumeric(sItem["SYKD"]) && GetSafeDouble(sItem["SYHD"].Trim()) > 0 && GetSafeDouble(sItem["SYKD"].Trim()) > 0)
                    {
                        md1 = GetSafeDouble(sItem["SYHD"].Trim());//14.5
                        md2 = GetSafeDouble(sItem["SYKD"].Trim());//30
                        sum = Math.Round(md1 * md2, 2);//435
                    }
                    else
                    {
                        md1 = GetSafeDouble(sItem["GGWJ"].Trim());
                        md2 = GetSafeDouble(sItem["GGBH"].Trim());
                        sum = 3.14159 * (Math.Pow((md1 / 2), 2) - Math.Pow(md1 / 2 - md2, 2));
                        if (IsNumeric(sItem["GGWJ2"]) && IsNumeric(sItem["GGBH2"]))
                        {
                            md1 = GetSafeDouble(sItem["GGWJ2"].Trim());
                            md2 = GetSafeDouble(sItem["GGBH2"].Trim());
                            sum2 = 3.14159 * (Math.Pow((md1 / 2), 2) - Math.Pow(md1 / 2 - md2, 2));
                        }
                        else
                        {
                            sum2 = sum;
                        }
                        if (IsNumeric(sItem["GGWJ3"]) && IsNumeric(sItem["GGBH3"]))
                        {
                            md1 = GetSafeDouble(sItem["GGWJ3"].Trim());
                            md2 = GetSafeDouble(sItem["GGBH3"].Trim());
                            sum3 = 3.14159 * (Math.Pow((md1 / 2), 2) - Math.Pow(md1 / 2 - md2, 2));
                        }
                        else
                        {
                            sum3 = sum;
                        }
                        if (IsNumeric(sItem["GGWJ4"]) && IsNumeric(sItem["GGBH4"]))
                        {
                            md1 = GetSafeDouble(sItem["GGWJ4"].Trim());
                            md2 = GetSafeDouble(sItem["GGBH4"].Trim());
                            sum4 = 3.14159 * (Math.Pow((md1 / 2), 2) - Math.Pow(md1 / 2 - md2, 2));
                        }
                        else
                        {
                            sum4 = sum;
                        }
                    }

                    if (IsNumeric(sItem["SYHD2"]) && IsNumeric(sItem["SYKD2"]) && GetSafeDouble(sItem["SYHD2"].Trim()) > 0 && GetSafeDouble(sItem["SYKD2"].Trim()) > 0)
                    {
                        md1 = GetSafeDouble(sItem["SYHD2"].Trim());
                        md2 = GetSafeDouble(sItem["SYKD2"].Trim());
                        sum2 = Math.Round(md1 * md2, 2);
                    }
                    if (IsNumeric(sItem["SYHD3"]) && IsNumeric(sItem["SYKD3"]) && GetSafeDouble(sItem["SYHD3"].Trim()) > 0 && GetSafeDouble(sItem["SYKD3"].Trim()) > 0)
                    {
                        md1 = GetSafeDouble(sItem["SYHD3"].Trim());
                        md2 = GetSafeDouble(sItem["SYKD3"].Trim());
                        sum3 = Math.Round(md1 * md2, 2);
                    }
                    if (IsNumeric(sItem["SYHD4"]) && IsNumeric(sItem["SYKD4"]) && GetSafeDouble(sItem["SYHD4"].Trim()) > 0 && GetSafeDouble(sItem["SYKD4"].Trim()) > 0)
                    {
                        md1 = GetSafeDouble(sItem["SYHD4"].Trim());
                        md2 = GetSafeDouble(sItem["SYKD4"].Trim());
                        sum4 = Math.Round(md1 * md2, 2);
                    }

                    //flag = true;
                    if (flag)
                    {
                        md1 = GetSafeDouble(sItem["QFHZ"].Trim());
                        md2 = sum;
                        md = 1000 * md1 / md2;
                        md = Math.Round(md / 5, 0) * 5;
                        sItem["QFQD"] = md.ToString("0");

                        md1 = GetSafeDouble(sItem["QFHZ2"].Trim());
                        md2 = sum2;
                        md = 1000 * md1 / md2;
                        md = Math.Round(md / 5, 0) * 5;
                        sItem["QFQD2"] = md.ToString("0");
                        if (SFlg)
                        {
                            md1 = GetSafeDouble(sItem["QFHZ3"].Trim());
                            md2 = sum3;
                            md = 1000 * md1 / md2;
                            md = Math.Round(md / 5, 0) * 5;
                            sItem["QFQD3"] = md.ToString("0");
                            md1 = GetSafeDouble(sItem["QFHZ4"].Trim());
                            md2 = sum4;
                            md = 1000 * md1 / md2;
                            md = Math.Round(md / 5, 0) * 5;
                            sItem["QFQD4"] = md.ToString("0");
                            sItem["HG_QFQD"] = IsQualified(sItem["G_QFQD"], sItem["QFQD"], false) == "合格" && IsQualified(sItem["G_QFQD"], sItem["QFQD2"], false) == "合格"
                                 && IsQualified(sItem["G_QFQD"], sItem["QFQD3"], false) == "合格" && IsQualified(sItem["G_QFQD"], sItem["QFQD4"], false) == "合格" ? "合格" : "不合格";
                            if (sItem["HG_QFQD"] == "不合格")
                            {
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["HG_QFQD"] = IsQualified(sItem["G_QFQD"], sItem["QFQD"], false) == "合格" && IsQualified(sItem["G_QFQD"], sItem["QFQD2"], false) == "合格" ? "合格" : "不合格";
                            if (sItem["HG_QFQD"] == "不合格")
                            {
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }

                        // 抗拉强度
                        md1 = GetSafeDouble(sItem["KLHZ"]);//223.75
                        md2 = sum;
                        md = 1000 * md1 / md2;
                        md = Math.Round(md / 5, 0) * 5;
                        sItem["KLQD"] = md.ToString("0");

                        md1 = GetSafeDouble(sItem["KLHZ2"]);
                        md2 = sum2;
                        md = 1000 * md1 / md2;
                        md = Math.Round(md / 5, 0) * 5;
                        sItem["KLQD2"] = md.ToString("0");

                        //md1 = CDec(.Fields("klhz2"))
                        //     md2 = sum2
                        //     md = 1000 * md1 / md2
                        //     md = Round(CDec(md / 5), 0) * 5
                        //     .Fields("klqd2") = Format(md, "0")
                        if (SFlg)
                        {
                            md1 = GetSafeDouble(sItem["KLHZ3"].Trim());
                            md2 = sum3;
                            md = 1000 * md1 / md2;
                            md = Math.Round(md / 5, 0) * 5;
                            sItem["KLQD3"] = md.ToString("0");
                            md1 = GetSafeDouble(sItem["KLHZ4"].Trim());
                            md2 = sum4;
                            md = 1000 * md1 / md2;
                            md = Math.Round(md / 5, 0) * 5;
                            sItem["KLQD4"] = md.ToString("0");
                            sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false) == "合格" && IsQualified(sItem["G_KLQD"], sItem["KLQD2"], false) == "合格"
                                 && IsQualified(sItem["G_KLQD"], sItem["KLQD3"], false) == "合格" && IsQualified(sItem["G_KLQD"], sItem["KLQD4"], false) == "合格" ? "合格" : "不合格";
                            if (sItem["HG_KLQD"] == "不合格")
                            {
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false) == "合格" && IsQualified(sItem["G_KLQD"], sItem["KLQD2"], false) == "合格" ? "合格" : "不合格";
                            if (sItem["HG_KLQD"] == "不合格")
                            {
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }

                        //伸长率
                        md1 = sum;
                        md1 = Math.Sqrt(md1);
                        md1 = md1 * 5.65;
                        pjmd = Math.Round(md1, 0);
                        pjmd = 5 * (Math.Floor(0.5 + pjmd / 5));
                        pjmd = Math.Round(pjmd, 0);
                        sItem["YSBJ"] = pjmd.ToString("0");

                        md1 = GetSafeDouble(sItem["SCZ"].Trim());
                        md2 = GetSafeDouble(sItem["YSBJ"].Trim());
                        md = 100 * (md1 - md2) / md2;
                        if (md > 10)
                        {
                            md = Math.Round(0.5 * (Math.Floor(0.5 + md / 0.5)), 1);
                        }
                        sItem["SCL"] = Math.Round(md, 1).ToString("0.0");

                        md1 = sum2;
                        md1 = Math.Sqrt(md1);
                        md1 = md1 * 5.65;
                        pjmd = Math.Round(md1, 0);
                        pjmd = 5 * (Math.Floor(0.5 + pjmd / 5));
                        pjmd = Math.Round(pjmd, 0);
                        sItem["YSBJ2"] = pjmd.ToString("0");

                        md1 = GetSafeDouble(sItem["SCZ2"].Trim());
                        md2 = GetSafeDouble(sItem["YSBJ2"].Trim());
                        md = 100 * (md1 - md2) / md2;
                        if (md > 10)
                        {
                            md = Math.Round(0.5 * (Math.Floor(0.5 + md / 0.5)), 1);
                        }
                        sItem["SCL2"] = Math.Round(md, 1).ToString("0.0");
                        if (SFlg)
                        {
                            md1 = sum3;
                            md1 = Math.Sqrt(md1);
                            md1 = 5.65 * md1;
                            pjmd = Math.Round(md1, 0);
                            pjmd = Math.Round(5 * (Math.Floor(0.5 + pjmd / 5)), 0);
                            //sItem["YSBJ3"] = pjmd.ToString("0");

                            md1 = GetSafeDouble(sItem["SCZ3"].Trim());
                            //md2 = GetSafeDouble(sItem["YSBJ3"].Trim());
                            md = 100 * (md1 - md2) / md2;
                            if (md > 10)
                            {
                                md = Math.Round(0.5 * (Math.Floor(0.5 + md / 0.5)), 1);
                            }
                            sItem["SCL3"] = md.ToString("0.0");

                            md1 = sum4;
                            md1 = Math.Sqrt(md1);
                            md1 = 5.65 * md1;
                            pjmd = Math.Round(md1, 0);
                            pjmd = Math.Round(5 * (Math.Floor(0.5 + pjmd / 5)), 0);
                            //sItem["YSBJ4"] = pjmd.ToString("0");

                            md1 = GetSafeDouble(sItem["SCZ4"].Trim());
                            //md2 = GetSafeDouble(sItem["YSBJ4"].Trim());
                            md = 100 * (md1 - md2) / md2;
                            if (md > 10)
                            {
                                md = Math.Round(0.5 * (Math.Floor(0.5 + md / 0.5)), 1);
                            }
                            sItem["SCL4"] = md.ToString("0.0");

                            sItem["HG_SCL"] = IsQualified(sItem["G_SCL"], sItem["SCL"], false) == "合格"
                                && IsQualified(sItem["G_SCL"], sItem["SCL2"], false) == "合格"
                                && IsQualified(sItem["G_SCL"], sItem["SCL3"], false) == "合格"
                                && IsQualified(sItem["G_SCL"], sItem["SCL4"], false) == "合格" ? "合格" : "不合格";
                            if (sItem["HG_SCL"] == "不合格")
                            {
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["HG_SCL"] = IsQualified(sItem["G_SCL"], sItem["SCL"], false) == "合格"
                                && IsQualified(sItem["G_SCL"], sItem["SCL2"], false) == "合格" ? "合格" : "不合格";
                            if (sItem["HG_SCL"] == "不合格")
                            {
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                    }
                    else
                    {
                        sign = false;
                    }
                }
                else
                {
                    sign = false;
                }

                if (!sign)
                {
                    sItem["HG_SCL"] = "----";
                    sItem["SCL"] = "----";
                    sItem["SCL2"] = "----";
                    sItem["G_SCL"] = "----";
                    sItem["G_KLQD"] = "----";
                    sItem["KLQD"] = "----";
                    sItem["KLQD2"] = "----";
                    sItem["HG_KLQD"] = "----";
                    sItem["G_QFQD"] = "----";
                    sItem["QFQD"] = "----";
                    sItem["QFQD2"] = "----";
                    sItem["HG_QFQD"] = "----";
                }
                if (sItem["G_SCL"] == "----")
                {
                    //sItem["SCL1"] = "----";
                    sItem["SCL2"] = "----";
                }
                if (sItem["G_QFQD"] == "----")
                {
                    sItem["QFQD"] = "----";
                    sItem["QFQD2"] = "----";
                }
                #endregion
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }
            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                if (mbHggs > 0)
                {
                    jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
