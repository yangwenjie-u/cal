﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class RF : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            int mbhggs = 0;//不合格数量
            var extraDJ = dataExtra["BZ_RF_DJ"];

            var data = retData;

            var SItem = data["S_RF"];
            var MItem = data["M_RF"];
            if (!data.ContainsKey("M_RF"))
            {
                data["M_RF"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            int mbHggs1 = 0, mbHggs2 = 0, mbHggs3 = 0;
            bool sign = true, mark = true;
            string djjg = "";


            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if (sItem["RSDJ"].Contains("A"))
                {
                    IDictionary<string, string> extraFieldsDj = null;
                    if (sItem["YPXZ"].Trim() == "管状")
                    {
                        if (GetSafeDouble(sItem["GZWJ"].Trim()) < 300)
                        {
                            extraFieldsDj = extraDJ.FirstOrDefault(u => u["RSDJ"] == sItem["RSCDJ"].Trim() && u["YPXZ"] == "管状");
                        }
                        else
                        {
                            extraFieldsDj = extraDJ.FirstOrDefault(u => u["RSDJ"] == sItem["RSCDJ"].Trim() && u["YPXZ"] == "平板状");
                        }
                    }
                    else
                    {
                        extraFieldsDj = extraDJ.FirstOrDefault(u => u["RSDJ"] == sItem["RSCDJ"].Trim() && u["YPXZ"] == "平板状");
                    }
                    if (null != extraFieldsDj)
                    {
                        sItem["G_LNWS"] = extraFieldsDj["G_LNWS"].Trim();
                        sItem["G_ZLSS"] = extraFieldsDj["G_ZLSS"].Trim();
                        sItem["G_RSSJ"] = extraFieldsDj["G_RSSJ"].Trim();
                        sItem["G_ZRZ"] = extraFieldsDj["G_ZRZ1"].Trim();

                        if (sItem["RSCDJ"] == "A1")
                        {
                            for (int i = 1; i <= 5; i++)
                            {
                                if (sItem["JGLX" + i].Trim() == "主要组分" || sItem["JGLX" + i].Trim() == "外部次要组分")
                                {
                                    sItem["G_JGRZ" + i] = extraFieldsDj["G_ZRZ1"].Trim();
                                    sItem["JGDW" + i] = "MJ/kg";
                                }
                                else if (sItem["JGLX" + i].Trim() == "内部次要组分")
                                {
                                    sItem["G_JGRZ" + i] = extraFieldsDj["G_ZRZ2"].Trim();
                                    sItem["JGDW" + i] = "MJ/m&scsup2&scend";
                                }
                                else
                                {
                                    sItem["G_JGRZ" + i] = "----";
                                    //sItem["G_JGDW" + i] = "----";
                                }
                            }
                        }

                        if (sItem["RSCDJ"] == "A2")
                        {
                            for (int i = 1; i <= 5; i++)
                            {
                                if (sItem["JGLX" + i].Trim() == "主要组分")
                                {
                                    sItem["G_JGRZ" + i] = extraFieldsDj["G_ZRZ1"].Trim();
                                    sItem["JGDW" + i] = "MJ/kg";
                                }
                                else if (sItem["JGLX" + i].Trim() == "内部次要组分" || sItem["JGLX" + i].Trim() == "外部次要组分")
                                {
                                    sItem["G_JGRZ" + i] = extraFieldsDj["G_ZRZ2"].Trim();
                                    sItem["JGDW" + i] = "MJ/m&scsup2&scend";
                                }
                                else
                                {
                                    sItem["G_JGRZ" + i] = "----";
                                    //sItem["G_JGDW" + i] = "----";
                                    sItem["JGDW" + i] = "----";
                                    sItem["W_PCS" + i] = "----";
                                }
                            }
                        }

                        sItem["G_FIGRA"] = extraFieldsDj["G_FIGRA"].Trim();
                        sItem["G_SMOGRA"] = extraFieldsDj["G_SMOGRA"].Trim();
                        sItem["G_THR"] = extraFieldsDj["G_THR"].Trim();
                        sItem["G_TSP"] = extraFieldsDj["G_TSP"].Trim();
                    }

                    #region 不燃性试验
                    if (jcxm.Contains("、不燃性试验、"))
                    {
                        if (!MItem[0]["JCYJ"].Contains("GB/T 5464-2010"))
                        {
                            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 5464-2010";
                        }
                        sign = true;
                        sign = (IsNumeric(sItem["W_LNWS"].Trim()) && !string.IsNullOrEmpty(sItem["W_LNWS"].Trim())) ? sign : false;
                        if (sign)
                        {
                            sItem["GH_LNWS"] = IsQualified(sItem["G_LNWS"], sItem["W_LNWS"], false);
                        }

                        sign = true;
                        sign = (IsNumeric(sItem["W_ZLSS"].Trim()) && !string.IsNullOrEmpty(sItem["W_ZLSS"])) ? sign : false;
                        if (sign)
                        {
                            sItem["GH_ZLSS"] = IsQualified(sItem["G_ZLSS"], sItem["W_ZLSS"], false);
                        }

                        sign = true;
                        sign = (IsNumeric(sItem["W_RSSJ"].Trim()) && !string.IsNullOrEmpty(sItem["W_RSSJ"])) ? sign : false;
                        if (sign)
                        {
                            if (sItem["G_RSSJ"].Trim() == "=0")
                            {
                                if (sItem["W_RSSJ"].Trim() == "0")
                                {
                                    sItem["GH_RSSJ"] = "合格";
                                }
                                else
                                {
                                    sItem["GH_RSSJ"] = "不合格";
                                }
                            }
                            else
                            {
                                sItem["GH_RSSJ"] = IsQualified(sItem["G_RSSJ"], sItem["W_RSSJ"], false);
                            }
                        }

                        mbHggs1 = sItem["GH_LNWS"] == "不合格" ? ++mbHggs1 : mbHggs1;
                        mbHggs1 = sItem["GH_ZLSS"] == "不合格" ? ++mbHggs1 : mbHggs1;
                        mbHggs1 = sItem["GH_RSSJ"] == "不合格" ? ++mbHggs1 : mbHggs1;
                        if (mbHggs1 == 0)
                        {
                            sItem["GH_BRX"] = "符合" + sItem["RSCDJ"] + "级";
                        }
                        else
                        {
                            sItem["GH_BRX"] = "不符合" + sItem["RSCDJ"] + "级";
                        }
                    }
                    else
                    {
                        sItem["G_LNWS"] = "----";
                        sItem["W_LNWS"] = "----";
                        sItem["GH_LNWS"] = "----";
                        sItem["G_RSSJ"] = "----";
                        sItem["W_RSSJ"] = "----";
                        sItem["GH_RSSJ"] = "----";
                        sItem["G_ZLSS"] = "----";
                        sItem["W_ZLSS"] = "----";
                        sItem["GH_ZLSS"] = "----";
                    }
                    #endregion

                    #region 燃烧热值试验
                    if (jcxm.Contains("、燃烧热值试验、"))
                    {
                        if (!MItem[0]["JCYJ"].Contains("GB/T 14402-2007"))
                        {
                            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 14402-2007";
                        }

                        sign = true;
                        sign = (IsNumeric(sItem["PCS"].Trim()) && !string.IsNullOrEmpty(sItem["PCS"])) ? sign : false;
                        if (sign)
                        {
                            sItem["GH_ZRZ"] = IsQualified(sItem["G_ZRZ"], sItem["PCS"], false);
                        }

                        for (int i = 1; i < 6; i++)
                        {
                            if (sItem["JGDW" + i].Trim() == "MJ/kg")
                            {
                                sItem["W_PCS" + i] = sItem["PCS1_" + i];
                            }
                            else if (sItem["JGDW" + i].Trim() == "MJ/m&scsup2&scend")
                            {
                                sItem["W_PCS" + i] = sItem["PCS2_" + i];
                            }
                            else
                            {
                                sItem["W_PCS" + i] = "----";
                            }
                            sItem["GH_JGRZ" + i] = IsQualified(sItem["G_JGRZ" + i], sItem["W_PCS" + i], false);
                        }

                        mbHggs2 = sItem["GH_ZRZ"].Trim() == "不合格" ? mbHggs2++ : mbHggs2;
                        for (int i = 1; i < 6; i++)
                        {
                            mbHggs2 = sItem["GH_JGRZ" + i].Trim() == "不合格" ? mbHggs2++ : mbHggs2;
                        }
                        if (mbHggs2 == 0)
                        {
                            sItem["GH_RSRZ"] = "符合" + sItem["RSCDJ"] + "级";
                        }
                        else
                        {
                            sItem["GH_RSRZ"] = "不符合" + sItem["RSCDJ"] + "级";
                        }
                    }
                    else
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            sItem["GH_JGRZ" + i] = "----";
                            sItem["G_JGRZ" + i] = "----";
                            sItem["W_PCS" + i] = "----";
                        }
                        sItem["GH_ZRZ"] = "----";
                        sItem["PCS"] = "----";
                        sItem["G_ZRZ"] = "----";
                    }
                    #endregion

                    #region 单体燃烧试验
                    if (jcxm.Contains("、单体燃烧试验、"))
                    {
                        if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                        {
                            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                        }

                        sign = true;
                        sItem["FIGRA"] = sItem["FIGRA2"];
                        sign = (IsNumeric(sItem["FIGRA"]) && !string.IsNullOrEmpty(sItem["FIGRA"])) ? sign : false;

                        if (sign)
                        {
                            sItem["GH_FIGRA"] = IsQualified(sItem["G_FIGRA"], sItem["FIGRA"], false);
                        }

                        sign = true;
                        sign = (IsNumeric(sItem["THR"]) && !string.IsNullOrEmpty(sItem["THR"])) ? sign : false;
                        if (sign)
                        {
                            sItem["GH_THR"] = IsQualified(sItem["G_THR"], sItem["THR"], false);
                        }

                        if (GetSafeDouble(sItem["HGS3"].Trim()) <= 1)
                        {
                            sItem["W_HXMY"] = "是";
                            sItem["GH_HXMY"] = "合格";
                        }
                        else
                        {
                            sItem["W_HXMY"] = "否";
                            sItem["GH_HXMY"] = "不合格";
                        }

                        mbHggs3 = sItem["GH_HXMY"] == "不合格" ? mbHggs3++ : mbHggs3;
                        mbHggs3 = sItem["GH_THR"] == "不合格" ? mbHggs3++ : mbHggs3;
                        mbHggs3 = sItem["GH_FIGRA"] == "不合格" ? mbHggs3++ : mbHggs3;

                        if (mbHggs3 == 0)
                        {
                            sItem["GH_DTRS"] = "符合" + sItem["RSCDJ"].Trim() + "级";
                        }
                        else
                        {
                            sItem["GH_DTRS"] = "不符合" + sItem["RSCDJ"].Trim() + "级";
                        }
                    }
                    else
                    {
                        sItem["GH_THR"] = "----";
                        sItem["THR"] = "----";
                        sItem["G_THR"] = "----";
                        sItem["GH_FIGRA"] = "----";
                        sItem["FIGRA"] = "----";
                        sItem["G_FIGRA"] = "----";
                        sItem["GH_HXMY"] = "----";
                        sItem["W_HXMY"] = "----";
                    }
                    #endregion

                    if (mbHggs3 == 0 && mbHggs2 == 0 && mbHggs1 == 0)
                    {
                        sItem["JCJG"] = "符合" + sItem["RSDJ"] + "级";
                        jsbeizhu = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中" + sItem["RSDJ"] + "级要求";
                    }
                    else
                    {
                        sItem["JCJG"] = "不符合" + sItem["RSDJ"] + "级";
                        jsbeizhu = "该试样燃烧性能等级不符合" + MItem[0]["PDBZ"] + "中" + sItem["RSDJ"] + "级要求";
                    }

                    if (sItem["RSCDJ"].Trim() == "A1" && mbHggs2 != 0 && mbHggs1 == 0)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            if (sItem["JGLX" + i] == "外部次要组分" && GetSafeDouble(sItem["PCS2_" + i]) < 2)
                            {
                                mark = false;
                            }
                        }

                        for (int i = 1; i < 4; i++)
                        {
                            if (sItem["DLW" + i] == "无")
                            {
                                sItem["HGS6"] = (GetSafeDouble(sItem["HGS6"].Trim()) - 1).ToString();
                            }
                        }

                        if (sItem["HGS6"] == "0")
                        {
                            sItem["W_RSDR"] = "无";
                        }

                        if (mark == false)
                        {
                            #region 单体燃烧试验
                            if (jcxm.Contains("、单体燃烧试验、"))
                            {
                                sItem["JCXM"] = sItem["JCXM"] + "、单体燃烧";
                                sItem["BEIZHU"] = "外部次要组分≤2.0MJ/m&scsup2&scend,继续进行单组试验";
                            }
                            else if (mbHggs3 == 0)
                            {
                                if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" && IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) == 0)
                                {
                                    sItem["GH_S1"] = "符合s1级";
                                    sItem["GH_D0"] = "符合d0级";
                                    sItem["JCJG"] = "符合A1级";
                                    mAllHg = true;
                                    jsbeizhu = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中A1级要求";
                                }
                                else if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" && IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) > 0)
                                {
                                    sItem["GH_S1"] = "符合s1级";
                                    sItem["GH_D0 "] = "不符合d0级";
                                    sItem["JCJG"] = "不符合A1级";
                                }
                                else if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" || IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) == 0)
                                {
                                    sItem["GH_S1"] = "不符合s1级";
                                    sItem["GH_D0"] = "符合d0级";
                                    sItem["JCJG"] = "不符合A1级";
                                }
                                else
                                {
                                    sItem["GH_S1"] = "不符合s1级";
                                    sItem["GH_D0"] = "不符合d0级";
                                    sItem["JCJG"] = "不符合A1级";
                                }
                            }
                            else
                            {
                                if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" && IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) == 0)
                                {
                                    sItem["GH_S1"] = "符合s1级";
                                    sItem["GH_D0"] = "符合d0级";
                                    sItem["JCJG"] = "不符合A1级";
                                }
                                else if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" && IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) > 0)
                                {
                                    sItem["GH_S1"] = "符合s1级";
                                    sItem["GH_D0"] = "不符合d0级";
                                    sItem["JCJG"] = "不符合A1级";
                                }
                                else if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" || IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) == 0)
                                {
                                    sItem["GH_S1"] = "不符合s1级";
                                    sItem["GH_D0"] = "符合d0级";
                                    sItem["JCJG"] = "不符合A1级";
                                }
                                else
                                {
                                    sItem["GH_S1"] = "不符合s1级";
                                    sItem["GH_D0"] = "不符合d0级";
                                    sItem["JCJG"] = "不符合A1级";
                                }
                            }
                            #endregion
                        }
                    }


                }

                if (sItem["RSDJ"].Contains("B1"))
                {
                    if (!MItem[0]["JCYJ"].Contains("GB/T 8626-2007"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 8626-2007";
                    }
                    if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                    }

                    IList<IDictionary<string, string>> extraFieldsDj = null;
                    if (sItem["YPXZ"] == "管状")
                    {
                        if (GetSafeDouble(sItem["GZWJ"].Trim()) < 300)
                        {
                            extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "管状" && u["RFDJ"] == "B1").ToList();
                        }
                        else
                        {
                            extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "平板状" && u["RFDJ"] == "B1").ToList();
                        }
                    }
                    else
                    {
                        extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "平板状" && u["RFDJ"] == "B1").ToList();
                    }
                    int xd = 1;
                    foreach (var ef in extraFieldsDj)
                    {
                        if (xd == 1)
                        {
                            if (IsQualified(sItem["G_FIGRA"], sItem["FIGRA2"], false) == "合格" && IsQualified(sItem["G_THR"], sItem["THR"], false) == "合格")
                            {
                                MItem[0]["WHICH"] = "bgrf、bgrf_3";
                                sItem["GH_DTRS"] = "符合B级";
                                sItem["G_FIGRA"] = ef["G_FIGRA"].Trim();
                                sItem["G_THR"] = ef["G_THR"].Trim();
                                sItem["GH_FIGRA"] = "合格";
                                sItem["GH_THR"] = "合格";
                                djjg = "B";
                                sItem["FIGRA"] = sItem["FIGRA2"];
                            }
                        }
                        else
                        {
                            MItem[0]["WHICH"] = "bgrf、bgrf_4";
                            sItem["G_FIGRA"] = ef["G_FIGRA"].Trim();
                            sItem["G_THR"] = ef["G_THR"].Trim();
                            sItem["FIGRA"] = sItem["FIGRA4"];
                            sItem["GH_FIGRA"] = IsQualified(sItem["G_FIGRA"], sItem["FIGRA4"], false);
                            sItem["GH_THR"] = IsQualified(sItem["GH_THR"], sItem["THR"], false);
                            djjg = ef["RSDJ"].Trim();
                            if (!(IsQualified(sItem["G_FIGRA"], sItem["FIGRA4"], false) == "不合格" || IsQualified(sItem["G_THR"], sItem["THR"], false) == "不合格"))
                            {
                                sItem["GH_DTRS"] = "符合" + ef["RSDJ"].Trim() + "级";
                            }
                            else
                            {
                                sItem["GH_DTRS"] = "不符合B&scsub1&scend级";
                                mbHggs1 = mbHggs1 + 1;
                            }
                        }
                    }

                    if (GetSafeDouble(sItem["HGS3"].Trim()) <= 1)
                    {
                        sItem["W_HXMY"] = "是";
                        sItem["GH_HXMY"] = "合格";
                    }
                    else
                    {
                        sItem["W_HXMY"] = "否";
                        sItem["GH_HXMY"] = "不合格";
                        mbHggs1 = mbHggs1 + 1;
                    }

                    if (GetSafeDouble(sItem["HGS4"].Trim()) <= 1)
                    {
                        sItem["G_YJGD"] = "60s内，≤150 mm";
                        sItem["W_YJGD"] = "是";
                        sItem["GH_YJGD"] = "合格";
                    }
                    else
                    {
                        sItem["G_YJGD"] = "60s内，≤150 mm";
                        sItem["W_YJGD"] = "否";
                        sItem["GH_YJGD"] = "不合格";
                        mbHggs1 = mbHggs1 + 1;
                    }

                    if (GetSafeDouble(sItem["HGS1"]) <= 1)
                    {
                        sItem["G_SFYR"] = "60s内无燃烧滴落物引燃滤纸现象";
                        sItem["W_SFYR"] = "否";
                        sItem["GH_SFYR"] = "合格";
                    }
                    else
                    {
                        sItem["G_SFYR"] = "60s内无燃烧滴落物引燃滤纸现象";
                        sItem["W_SFYR"] = "是";
                        sItem["GH_SFYR"] = "不合格";
                        mbHggs1 = mbHggs1 + 1;
                    }

                    if (jcxm.Contains("、氧指数、"))
                    {
                        if (!MItem[0]["JCYJ"].Contains("GB/T 2406.2-2009"))
                        {
                            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 2406.2-2009";
                        }
                        sItem["G_RSYZZ"] = "≥30";

                        sign = true;
                        if (sItem["GH_RSYZZ"].Trim() == "合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                        {
                            sItem["W_RSYZZ"] = "不低于30";
                        }
                        else if (sItem["GH_RSYZZ"].Trim() == "不合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                        {
                            sItem["W_RSYZZ"] = "低于30";
                        }
                        else
                        {
                            sign = IsNumeric(sItem["W_RSYZZ"]) && !string.IsNullOrEmpty(sItem["W_RSYZZ"]) ? sign : false;
                            if (sign)
                            {
                                sItem["GH_RSYZZ"] = IsQualified(sItem["G_RSYZZ"], sItem["W_RSYZZ"], false);
                            }

                        }
                        mbHggs1 = sItem["GH_RSYZZ"].Trim() == "不合格" ? mbHggs1++ : mbHggs1;
                    }
                    else
                    {
                        sItem["GH_RSYZZ"] = "----";
                        sItem["G_RSYZZ"] = "----";
                        sItem["W_RSYZZ"] = "----";
                    }

                    sItem["RSCDJ"] = djjg;
                    if (mbHggs1 > 0)
                    {
                        sItem["JCJG"] = "不符合B&scsub1&scend级";
                        mAllHg = false;
                        jsbeizhu = "该试样燃烧性能等级不符合" + MItem[0]["PDBZ"] + "中B &scsub1&scend级要求";
                    }
                    else
                    {
                        sItem["JCJG"] = "符合B&scsub1&scend级";
                        jsbeizhu = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中B &scsub1&scend级要求";
                    }

                }

                if (sItem["RSDJ"].Contains("B2") && !sItem["RSDJ"].Contains("E"))
                {
                    if (MItem[0]["JCYJ"].Contains("GB/T 8626-2007"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 8626-2007";
                    }
                    if (MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                    }

                    IList<IDictionary<string, string>> extraFieldsDj = null;
                    if (sItem["YPXZ"] == "管状")
                    {
                        if (GetSafeDouble("GZWJ") < 300)
                        {
                            extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "管状" && u["RFDJ"] == "B2").ToList();
                        }
                        else
                        {
                            extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "平板状" && u["RFDJ"] == "B2").ToList();
                        }
                    }
                    else
                    {
                        extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "平板状" && u["RFDJ"] == "B2").ToList();
                    }

                    int y = 1;
                    int efCount = extraFieldsDj.Count;
                    foreach (var ef in extraFieldsDj)
                    {
                        //MItem[0]["WHICH"] = "bgrf、bgrf_6";
                        sItem["G_FIGRA"] = ef["G_FIGRA"];
                        sItem["G_THR"] = ef["G_THR"];
                        sItem["FIGRA"] = sItem["FIGRA4"];

                        sItem["GH_FIGRA"] = IsQualified(sItem["G_FIGRA"], sItem["FIGRA4"], false);
                        sItem["GH_THR"] = IsQualified(sItem["G_THR"], sItem["THR"], false);
                        if (!(IsQualified(ef["G_FIGRA"], sItem["FIGRA4"], false) == "不合格" || IsQualified(ef["G_THR"], sItem["THR"], false) == "不合格"))
                        {
                            sItem["GH_DTRS"] = "符合" + ef["RSDJ"] + "级";
                            djjg = ef["RSDJ"];
                            break;
                        }
                        y++;
                    }

                    if (y > efCount)
                    {
                        sItem["GH_DTRS"] = "不符合B&scsub2&scend级";
                        mbHggs1 = mbHggs1 + 1;
                    }

                    if (GetSafeDouble(sItem["HGS4"]) <= 1)
                    {
                        if (djjg == "E")
                        {
                            MItem[0]["WHICH"] = "bgrf、bgrf_7";
                            sItem["G_YJGD"] = "20s内，≤150 mm";
                        }
                        else
                        {
                            sItem["G_YJGD"] = "60s内，≤150 mm";
                        }
                        sItem["W_YJGD"] = "是";
                        sItem["GH_YJGD"] = "合格";
                    }
                    else if (GetSafeDouble(sItem["HGS5"]) <= 1)
                    {
                        sItem["G_YJGD"] = "20s内，≤150 mm";
                        sItem["W_YJGD"] = "是";
                        sItem["GH_YJGD"] = "合格";
                        djjg = "E";
                    }
                    else
                    {
                        sItem["G_YJGD"] = "20s内，≤150 mm";
                        sItem["W_YJGD"] = "否";
                        sItem["GH_YJGD"] = "不合格";
                        mbHggs1 = mbHggs1 + 1;
                    }

                    if (GetSafeDouble(sItem["HGS1"].Trim()) <= 1)
                    {
                        if (djjg == "E")
                        {
                            sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                        }
                        else
                        {
                            sItem["G_SFYR"] = "60s内无燃烧滴落物引燃滤纸现象";
                        }
                        sItem["W_SFYR"] = "是";
                        sItem["GH_SFYR"] = "合格";
                    }
                    else if (GetSafeDouble(sItem["HGS2"].Trim()) <= 1)
                    {
                        sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                        sItem["W_SFYR"] = "是";
                        sItem["GH_SFYR"] = "合格";
                        djjg = "E";
                    }
                    else
                    {
                        sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                        sItem["W_SFYR"] = "否";
                        sItem["GH_SFYR"] = "不合格";
                        mbHggs1 = mbHggs1 + 1;
                    }

                    if (jcxm.Contains("、氧指数、"))
                    {
                        if (!MItem[0]["JCYJ"].Contains("GB/T 2406.2-2009"))
                        {
                            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 2406.2-2009";
                        }
                        sItem["G_RSYZZ"] = "≥26";
                        sign = true;
                        if (sItem["GH_RSYZZ"].Trim() == "合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                        {
                            sItem["W_RSYZZ"] = "不低于26";
                        }
                        else if (sItem["GH_RSYZZ"].Trim() == "不合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                        {
                            sItem["W_RSYZZ"] = "低于26";
                        }
                        else
                        {
                            sign = IsNumeric(sItem["W_RSYZZ"].Trim()) && !string.IsNullOrEmpty(sItem["W_RSYZZ"]) ? sign : false;
                            if (sign)
                            {
                                sItem["GH_RSYZZ"] = IsQualified(sItem["G_RSYZZ"], sItem["W_RSYZZ"], false);
                            }
                        }
                        mbHggs1 = sItem["GH_RSYZZ"] == "不合格" ? mbHggs1++ : mbHggs1;
                    }
                    else
                    {
                        sItem["GH_RSYZZ"] = "----";
                        sItem["G_RSYZZ"] = "----";
                        sItem["W_RSYZZ"] = "----";
                    }

                    sItem["RSCDJ"] = djjg;
                    if (mbHggs1 > 0)
                    {
                        sItem["JCJG"] = "不符合B&scsub2&scend级";
                        jsbeizhu = "该试样燃烧性能等级不符合" + MItem[0]["PDBZ"] + "中B & scsub2 & scend级要求";
                    }
                    else
                    {
                        sItem["JCJG"] = "符合B&scsub2&scend级";
                        jsbeizhu = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中B & scsub2 & scend级要求";
                    }
                }

                if (sItem["RSDJ"].Contains("B2") && sItem["RSDJ"].Contains("E"))
                {
                    //MItem[0]["WHICH"] = "bgrf、bgrf_5";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 8626-2007"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 8626-2007";
                    }

                    if (GetSafeDouble(sItem["HGS5"].Trim()) <= 1)
                    {
                        sItem["G_YJGD"] = "20s内，≤150 mm";
                        sItem["W_YJGD"] = "是";
                        sItem["GH_YJGD"] = "合格";
                        djjg = "E";
                    }
                    else
                    {
                        sItem["G_YJGD"] = "20s内，≤150 mm";
                        sItem["W_YJGD"] = "否";
                        sItem["GH_YJGD"] = "不合格";
                        mbHggs1 = mbHggs1 + 1;
                    }

                    if (GetSafeDouble(sItem["HGS2"]) <= 1)
                    {
                        sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                        sItem["W_SFYR"] = "是";
                        sItem["GH_SFYR"] = "合格";
                        djjg = "E";
                    }
                    else
                    {
                        sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                        sItem["W_SFYR"] = "否";
                        sItem["GH_SFYR"] = "不合格";
                        mbHggs1 = mbHggs1 + 1;
                    }

                    if (jcxm.Contains("、氧指数、"))
                    {
                        if (!MItem[0]["JCYJ"].Trim().Contains("GB/T 2406.2-2009"))
                        {
                            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 2406.2-2009";
                        }

                        sItem["G_RSYZZ"] = "≥26";
                        sign = true;
                        if (sItem["GH_RSYZZ"].Trim() == "合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                        {
                            sItem["W_RSYZZ"] = "不低于26";
                        }
                        else if (sItem["GH_RSYZZ"].Trim() == "不合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                        {
                            sItem["W_RSYZZ"] = "低于26";
                        }
                        else
                        {
                            sign = IsNumeric(sItem["W_RSYZZ"].Trim()) && !string.IsNullOrEmpty(sItem["W_RSYZZ"].Trim()) ? sign : false;
                            if (sign)
                            {
                                sItem["GH_RSYZZ"] = IsQualified(sItem["G_RSYZZ"], sItem["W_RSYZZ"], false);
                            }
                        }
                        mbHggs1 = sItem["GH_RSYZZ"].Trim() == "不合格" ? mbHggs1++ : mbHggs1;
                    }
                    else
                    {
                        sItem["GH_RSYZZ"] = "----";
                        sItem["G_RSYZZ"] = "----";
                        sItem["W_RSYZZ"] = "----";
                    }
                    sItem["RSCDJ"] = "E";
                    if (mbHggs1 > 0)
                    {
                        sItem["JCJG"] = "不符合任何级别";
                        mAllHg = false;
                        jsbeizhu = "该试样不符合" + MItem[0]["PDBZ"] + "中任何级别要求";
                        mbhggs++;
                    }
                    else
                    {
                        sItem["JCJG"] = "符合B&scsub2&scend(E)级";
                        jsbeizhu = "该试样符合" + MItem[0]["PDBZ"] + "中B&scsub2&scend级要求,燃烧性能细化分级为E级";
                    }
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
            /************************ 代码结束 *********************/
            #endregion
        }

    }
}
