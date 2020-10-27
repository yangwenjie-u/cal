using System;
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
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                #region old code
                //if (sItem["RSDJ"].Contains("A"))
                //{
                //    IDictionary<string, string> extraFieldsDj = null;
                //    if (sItem["YPXZ"].Trim() == "管状")
                //    {
                //        if (GetSafeDouble(sItem["GZWJ"].Trim()) < 300)
                //        {
                //            extraFieldsDj = extraDJ.FirstOrDefault(u => u["RSDJ"] == sItem["RSCDJ"].Trim() && u["YPXZ"] == "管状");
                //        }
                //        else
                //        {
                //            extraFieldsDj = extraDJ.FirstOrDefault(u => u["RSDJ"] == sItem["RSCDJ"].Trim() && u["YPXZ"] == "平板状");
                //        }
                //    }
                //    else
                //    {
                //        extraFieldsDj = extraDJ.FirstOrDefault(u => u["RSDJ"] == sItem["RSCDJ"].Trim() && u["YPXZ"] == "平板状");
                //    }
                //    if (null != extraFieldsDj)
                //    {
                //        sItem["G_LNWS"] = extraFieldsDj["G_LNWS"].Trim();
                //        sItem["G_ZLSS"] = extraFieldsDj["G_ZLSS"].Trim();
                //        sItem["G_RSSJ"] = extraFieldsDj["G_RSSJ"].Trim();
                //        sItem["G_ZRZ"] = extraFieldsDj["G_ZRZ1"].Trim();

                //        if (sItem["RSCDJ"] == "A1")
                //        {
                //            for (int i = 1; i <= 5; i++)
                //            {
                //                if (sItem["JGLX" + i].Trim() == "主要组分" || sItem["JGLX" + i].Trim() == "外部次要组分")
                //                {
                //                    sItem["G_JGRZ" + i] = extraFieldsDj["G_ZRZ1"].Trim();
                //                    sItem["JGDW" + i] = "MJ/kg";
                //                }
                //                else if (sItem["JGLX" + i].Trim() == "内部次要组分")
                //                {
                //                    sItem["G_JGRZ" + i] = extraFieldsDj["G_ZRZ2"].Trim();
                //                    sItem["JGDW" + i] = "MJ/m&scsup2&scend";
                //                }
                //                else
                //                {
                //                    sItem["G_JGRZ" + i] = "----";
                //                    //sItem["G_JGDW" + i] = "----";
                //                }
                //            }
                //        }

                //        if (sItem["RSCDJ"] == "A2")
                //        {
                //            for (int i = 1; i <= 5; i++)
                //            {
                //                if (sItem["JGLX" + i].Trim() == "主要组分")
                //                {
                //                    sItem["G_JGRZ" + i] = extraFieldsDj["G_ZRZ1"].Trim();
                //                    sItem["JGDW" + i] = "MJ/kg";
                //                }
                //                else if (sItem["JGLX" + i].Trim() == "内部次要组分" || sItem["JGLX" + i].Trim() == "外部次要组分")
                //                {
                //                    sItem["G_JGRZ" + i] = extraFieldsDj["G_ZRZ2"].Trim();
                //                    sItem["JGDW" + i] = "MJ/m&scsup2&scend";
                //                }
                //                else
                //                {
                //                    sItem["G_JGRZ" + i] = "----";
                //                    //sItem["G_JGDW" + i] = "----";
                //                    sItem["JGDW" + i] = "----";
                //                    sItem["W_PCS" + i] = "----";
                //                }
                //            }
                //        }

                //        sItem["G_FIGRA"] = extraFieldsDj["G_FIGRA"].Trim();
                //        sItem["G_SMOGRA"] = extraFieldsDj["G_SMOGRA"].Trim();
                //        sItem["G_THR"] = extraFieldsDj["G_THR"].Trim();
                //        sItem["G_TSP"] = extraFieldsDj["G_TSP"].Trim();
                //    }

                #region 不燃性试验
                //    if (jcxm.Contains("、不燃性试验、"))
                //    {
                //        if (!MItem[0]["JCYJ"].Contains("GB/T 5464-2010"))
                //        {
                //            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 5464-2010";
                //        }
                //        sign = true;
                //        sign = (IsNumeric(sItem["W_LNWS"].Trim()) && !string.IsNullOrEmpty(sItem["W_LNWS"].Trim())) ? sign : false;
                //        if (sign)
                //        {
                //            sItem["GH_LNWS"] = IsQualified(sItem["G_LNWS"], sItem["W_LNWS"], false);
                //        }

                //        sign = true;
                //        sign = (IsNumeric(sItem["W_ZLSS"].Trim()) && !string.IsNullOrEmpty(sItem["W_ZLSS"])) ? sign : false;
                //        if (sign)
                //        {
                //            sItem["GH_ZLSS"] = IsQualified(sItem["G_ZLSS"], sItem["W_ZLSS"], false);
                //        }

                //        sign = true;
                //        sign = (IsNumeric(sItem["W_RSSJ"].Trim()) && !string.IsNullOrEmpty(sItem["W_RSSJ"])) ? sign : false;
                //        if (sign)
                //        {
                //            if (sItem["G_RSSJ"].Trim() == "=0")
                //            {
                //                if (sItem["W_RSSJ"].Trim() == "0")
                //                {
                //                    sItem["GH_RSSJ"] = "合格";
                //                }
                //                else
                //                {
                //                    sItem["GH_RSSJ"] = "不合格";
                //                }
                //            }
                //            else
                //            {
                //                sItem["GH_RSSJ"] = IsQualified(sItem["G_RSSJ"], sItem["W_RSSJ"], false);
                //            }
                //        }

                //        mbHggs1 = sItem["GH_LNWS"] == "不合格" ? ++mbHggs1 : mbHggs1;
                //        mbHggs1 = sItem["GH_ZLSS"] == "不合格" ? ++mbHggs1 : mbHggs1;
                //        mbHggs1 = sItem["GH_RSSJ"] == "不合格" ? ++mbHggs1 : mbHggs1;
                //        if (mbHggs1 == 0)
                //        {
                //            sItem["GH_BRX"] = "符合" + sItem["RSCDJ"] + "级";
                //        }
                //        else
                //        {
                //            sItem["GH_BRX"] = "不符合" + sItem["RSCDJ"] + "级";
                //        }
                //    }
                //    else
                //    {
                //        sItem["G_LNWS"] = "----";
                //        sItem["W_LNWS"] = "----";
                //        sItem["GH_LNWS"] = "----";
                //        sItem["G_RSSJ"] = "----";
                //        sItem["W_RSSJ"] = "----";
                //        sItem["GH_RSSJ"] = "----";
                //        sItem["G_ZLSS"] = "----";
                //        sItem["W_ZLSS"] = "----";
                //        sItem["GH_ZLSS"] = "----";
                //    }
                #endregion

                #region 燃烧热值试验
                //    if (jcxm.Contains("、燃烧热值试验、"))
                //    {
                //        if (!MItem[0]["JCYJ"].Contains("GB/T 14402-2007"))
                //        {
                //            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 14402-2007";
                //        }

                //        sign = true;
                //        sign = (IsNumeric(sItem["PCS"].Trim()) && !string.IsNullOrEmpty(sItem["PCS"])) ? sign : false;
                //        if (sign)
                //        {
                //            sItem["GH_ZRZ"] = IsQualified(sItem["G_ZRZ"], sItem["PCS"], false);
                //        }

                //        for (int i = 1; i < 6; i++)
                //        {
                //            if (sItem["JGDW" + i].Trim() == "MJ/kg")
                //            {
                //                sItem["W_PCS" + i] = sItem["PCS1_" + i];
                //            }
                //            else if (sItem["JGDW" + i].Trim() == "MJ/m&scsup2&scend")
                //            {
                //                sItem["W_PCS" + i] = sItem["PCS2_" + i];
                //            }
                //            else
                //            {
                //                sItem["W_PCS" + i] = "----";
                //            }
                //            sItem["GH_JGRZ" + i] = IsQualified(sItem["G_JGRZ" + i], sItem["W_PCS" + i], false);
                //        }

                //        mbHggs2 = sItem["GH_ZRZ"].Trim() == "不合格" ? mbHggs2++ : mbHggs2;
                //        for (int i = 1; i < 6; i++)
                //        {
                //            mbHggs2 = sItem["GH_JGRZ" + i].Trim() == "不合格" ? mbHggs2++ : mbHggs2;
                //        }
                //        if (mbHggs2 == 0)
                //        {
                //            sItem["GH_RSRZ"] = "符合" + sItem["RSCDJ"] + "级";
                //        }
                //        else
                //        {
                //            sItem["GH_RSRZ"] = "不符合" + sItem["RSCDJ"] + "级";
                //        }
                //    }
                //    else
                //    {
                //        for (int i = 1; i < 6; i++)
                //        {
                //            sItem["GH_JGRZ" + i] = "----";
                //            sItem["G_JGRZ" + i] = "----";
                //            sItem["W_PCS" + i] = "----";
                //        }
                //        sItem["GH_ZRZ"] = "----";
                //        sItem["PCS"] = "----";
                //        sItem["G_ZRZ"] = "----";
                //    }
                #endregion

                #region 单体燃烧试验
                //    if (jcxm.Contains("、单体燃烧试验、"))
                //    {
                //        if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                //        {
                //            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                //        }

                //        sign = true;
                //        sItem["FIGRA"] = sItem["FIGRA2"];
                //        sign = (IsNumeric(sItem["FIGRA"]) && !string.IsNullOrEmpty(sItem["FIGRA"])) ? sign : false;

                //        if (sign)
                //        {
                //            sItem["GH_FIGRA"] = IsQualified(sItem["G_FIGRA"], sItem["FIGRA"], false);
                //        }

                //        sign = true;
                //        sign = (IsNumeric(sItem["THR"]) && !string.IsNullOrEmpty(sItem["THR"])) ? sign : false;
                //        if (sign)
                //        {
                //            sItem["GH_THR"] = IsQualified(sItem["G_THR"], sItem["THR"], false);
                //        }

                //        if (GetSafeDouble(sItem["HGS3"].Trim()) <= 1)
                //        {
                //            sItem["W_HXMY"] = "是";
                //            sItem["GH_HXMY"] = "合格";
                //        }
                //        else
                //        {
                //            sItem["W_HXMY"] = "否";
                //            sItem["GH_HXMY"] = "不合格";
                //        }

                //        mbHggs3 = sItem["GH_HXMY"] == "不合格" ? mbHggs3++ : mbHggs3;
                //        mbHggs3 = sItem["GH_THR"] == "不合格" ? mbHggs3++ : mbHggs3;
                //        mbHggs3 = sItem["GH_FIGRA"] == "不合格" ? mbHggs3++ : mbHggs3;

                //        if (mbHggs3 == 0)
                //        {
                //            sItem["GH_DTRS"] = "符合" + sItem["RSCDJ"].Trim() + "级";
                //        }
                //        else
                //        {
                //            sItem["GH_DTRS"] = "不符合" + sItem["RSCDJ"].Trim() + "级";
                //        }
                //    }
                //    else
                //    {
                //        sItem["GH_THR"] = "----";
                //        sItem["THR"] = "----";
                //        sItem["G_THR"] = "----";
                //        sItem["GH_FIGRA"] = "----";
                //        sItem["FIGRA"] = "----";
                //        sItem["G_FIGRA"] = "----";
                //        sItem["GH_HXMY"] = "----";
                //        sItem["W_HXMY"] = "----";
                //    }
                #endregion

                //    if (mbHggs3 == 0 && mbHggs2 == 0 && mbHggs1 == 0)
                //    {
                //        sItem["JCJG"] = "符合" + sItem["RSDJ"] + "级";
                //        jsbeizhu = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中" + sItem["RSDJ"] + "级要求";
                //    }
                //    else
                //    {
                //        sItem["JCJG"] = "不符合" + sItem["RSDJ"] + "级";
                //        jsbeizhu = "该试样燃烧性能等级不符合" + MItem[0]["PDBZ"] + "中" + sItem["RSDJ"] + "级要求";
                //    }

                //    if (sItem["RSCDJ"].Trim() == "A1" && mbHggs2 != 0 && mbHggs1 == 0)
                //    {
                //        for (int i = 1; i < 6; i++)
                //        {
                //            if (sItem["JGLX" + i] == "外部次要组分" && GetSafeDouble(sItem["PCS2_" + i]) < 2)
                //            {
                //                mark = false;
                //            }
                //        }

                //        for (int i = 1; i < 4; i++)
                //        {
                //            if (sItem["DLW" + i] == "无")
                //            {
                //                sItem["HGS6"] = (GetSafeDouble(sItem["HGS6"].Trim()) - 1).ToString();
                //            }
                //        }

                //        if (sItem["HGS6"] == "0")
                //        {
                //            sItem["W_RSDR"] = "无";
                //        }

                //        if (mark == false)
                //        {
                //            #region 单体燃烧试验
                //            if (jcxm.Contains("、单体燃烧试验、"))
                //            {
                //                sItem["JCXM"] = sItem["JCXM"] + "、单体燃烧";
                //                sItem["BEIZHU"] = "外部次要组分≤2.0MJ/m&scsup2&scend,继续进行单组试验";
                //            }
                //            else if (mbHggs3 == 0)
                //            {
                //                if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" && IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) == 0)
                //                {
                //                    sItem["GH_S1"] = "符合s1级";
                //                    sItem["GH_D0"] = "符合d0级";
                //                    sItem["JCJG"] = "符合A1级";
                //                    mAllHg = true;
                //                    jsbeizhu = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中A1级要求";
                //                }
                //                else if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" && IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) > 0)
                //                {
                //                    sItem["GH_S1"] = "符合s1级";
                //                    sItem["GH_D0 "] = "不符合d0级";
                //                    sItem["JCJG"] = "不符合A1级";
                //                }
                //                else if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" || IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) == 0)
                //                {
                //                    sItem["GH_S1"] = "不符合s1级";
                //                    sItem["GH_D0"] = "符合d0级";
                //                    sItem["JCJG"] = "不符合A1级";
                //                }
                //                else
                //                {
                //                    sItem["GH_S1"] = "不符合s1级";
                //                    sItem["GH_D0"] = "不符合d0级";
                //                    sItem["JCJG"] = "不符合A1级";
                //                }
                //            }
                //            else
                //            {
                //                if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" && IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) == 0)
                //                {
                //                    sItem["GH_S1"] = "符合s1级";
                //                    sItem["GH_D0"] = "符合d0级";
                //                    sItem["JCJG"] = "不符合A1级";
                //                }
                //                else if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" && IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) > 0)
                //                {
                //                    sItem["GH_S1"] = "符合s1级";
                //                    sItem["GH_D0"] = "不符合d0级";
                //                    sItem["JCJG"] = "不符合A1级";
                //                }
                //                else if (IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], false) == "合格" || IsQualified(sItem["G_TSP"], sItem["TSP"], false) == "合格" && GetSafeDouble(sItem["HGS6"]) == 0)
                //                {
                //                    sItem["GH_S1"] = "不符合s1级";
                //                    sItem["GH_D0"] = "符合d0级";
                //                    sItem["JCJG"] = "不符合A1级";
                //                }
                //                else
                //                {
                //                    sItem["GH_S1"] = "不符合s1级";
                //                    sItem["GH_D0"] = "不符合d0级";
                //                    sItem["JCJG"] = "不符合A1级";
                //                }
                //            }
                //            #endregion
                //        }
                //    }


                //}

                //if (sItem["RSDJ"].Contains("B1"))
                //{
                //    if (!MItem[0]["JCYJ"].Contains("GB/T 8626-2007"))
                //    {
                //        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 8626-2007";
                //    }
                //    if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                //    {
                //        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                //    }

                //    IList<IDictionary<string, string>> extraFieldsDj = null;
                //    if (sItem["YPXZ"] == "管状")
                //    {
                //        if (GetSafeDouble(sItem["GZWJ"].Trim()) < 300)
                //        {
                //            extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "管状" && u["RFDJ"] == "B1").ToList();
                //        }
                //        else
                //        {
                //            extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "平板状" && u["RFDJ"] == "B1").ToList();
                //        }
                //    }
                //    else
                //    {
                //        extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "平板状" && u["RFDJ"] == "B1").ToList();
                //    }
                //    int xd = 1;
                //    foreach (var ef in extraFieldsDj)
                //    {
                //        if (xd == 1)
                //        {
                //            if (IsQualified(sItem["G_FIGRA"], sItem["FIGRA2"], false) == "合格" && IsQualified(sItem["G_THR"], sItem["THR"], false) == "合格")
                //            {
                //                MItem[0]["WHICH"] = "bgrf、bgrf_3";
                //                sItem["GH_DTRS"] = "符合B级";
                //                sItem["G_FIGRA"] = ef["G_FIGRA"].Trim();
                //                sItem["G_THR"] = ef["G_THR"].Trim();
                //                sItem["GH_FIGRA"] = "合格";
                //                sItem["GH_THR"] = "合格";
                //                djjg = "B";
                //                sItem["FIGRA"] = sItem["FIGRA2"];
                //            }
                //        }
                //        else
                //        {
                //            MItem[0]["WHICH"] = "bgrf、bgrf_4";
                //            sItem["G_FIGRA"] = ef["G_FIGRA"].Trim();
                //            sItem["G_THR"] = ef["G_THR"].Trim();
                //            sItem["FIGRA"] = sItem["FIGRA4"];
                //            sItem["GH_FIGRA"] = IsQualified(sItem["G_FIGRA"], sItem["FIGRA4"], false);
                //            sItem["GH_THR"] = IsQualified(sItem["GH_THR"], sItem["THR"], false);
                //            djjg = ef["RSDJ"].Trim();
                //            if (!(IsQualified(sItem["G_FIGRA"], sItem["FIGRA4"], false) == "不合格" || IsQualified(sItem["G_THR"], sItem["THR"], false) == "不合格"))
                //            {
                //                sItem["GH_DTRS"] = "符合" + ef["RSDJ"].Trim() + "级";
                //            }
                //            else
                //            {
                //                sItem["GH_DTRS"] = "不符合B&scsub1&scend级";
                //                mbHggs1 = mbHggs1 + 1;
                //            }
                //        }
                //    }

                //    if (GetSafeDouble(sItem["HGS3"].Trim()) <= 1)
                //    {
                //        sItem["W_HXMY"] = "是";
                //        sItem["GH_HXMY"] = "合格";
                //    }
                //    else
                //    {
                //        sItem["W_HXMY"] = "否";
                //        sItem["GH_HXMY"] = "不合格";
                //        mbHggs1 = mbHggs1 + 1;
                //    }

                //    if (GetSafeDouble(sItem["HGS4"].Trim()) <= 1)
                //    {
                //        sItem["G_YJGD"] = "60s内，≤150 mm";
                //        sItem["W_YJGD"] = "是";
                //        sItem["GH_YJGD"] = "合格";
                //    }
                //    else
                //    {
                //        sItem["G_YJGD"] = "60s内，≤150 mm";
                //        sItem["W_YJGD"] = "否";
                //        sItem["GH_YJGD"] = "不合格";
                //        mbHggs1 = mbHggs1 + 1;
                //    }

                //    if (GetSafeDouble(sItem["HGS1"]) <= 1)
                //    {
                //        sItem["G_SFYR"] = "60s内无燃烧滴落物引燃滤纸现象";
                //        sItem["W_SFYR"] = "否";
                //        sItem["GH_SFYR"] = "合格";
                //    }
                //    else
                //    {
                //        sItem["G_SFYR"] = "60s内无燃烧滴落物引燃滤纸现象";
                //        sItem["W_SFYR"] = "是";
                //        sItem["GH_SFYR"] = "不合格";
                //        mbHggs1 = mbHggs1 + 1;
                //    }

                //    if (jcxm.Contains("、氧指数、"))
                //    {
                //        if (!MItem[0]["JCYJ"].Contains("GB/T 2406.2-2009"))
                //        {
                //            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 2406.2-2009";
                //        }
                //        sItem["G_RSYZZ"] = "≥30";

                //        sign = true;
                //        if (sItem["GH_RSYZZ"].Trim() == "合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                //        {
                //            sItem["W_RSYZZ"] = "不低于30";
                //        }
                //        else if (sItem["GH_RSYZZ"].Trim() == "不合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                //        {
                //            sItem["W_RSYZZ"] = "低于30";
                //        }
                //        else
                //        {
                //            sign = IsNumeric(sItem["W_RSYZZ"]) && !string.IsNullOrEmpty(sItem["W_RSYZZ"]) ? sign : false;
                //            if (sign)
                //            {
                //                sItem["GH_RSYZZ"] = IsQualified(sItem["G_RSYZZ"], sItem["W_RSYZZ"], false);
                //            }

                //        }
                //        mbHggs1 = sItem["GH_RSYZZ"].Trim() == "不合格" ? mbHggs1++ : mbHggs1;
                //    }
                //    else
                //    {
                //        sItem["GH_RSYZZ"] = "----";
                //        sItem["G_RSYZZ"] = "----";
                //        sItem["W_RSYZZ"] = "----";
                //    }

                //    sItem["RSCDJ"] = djjg;
                //    if (mbHggs1 > 0)
                //    {
                //        sItem["JCJG"] = "不符合B&scsub1&scend级";
                //        mAllHg = false;
                //        jsbeizhu = "该试样燃烧性能等级不符合" + MItem[0]["PDBZ"] + "中B &scsub1&scend级要求";
                //    }
                //    else
                //    {
                //        sItem["JCJG"] = "符合B&scsub1&scend级";
                //        jsbeizhu = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中B &scsub1&scend级要求";
                //    }

                //}

                //if (sItem["RSDJ"].Contains("B2") && !sItem["RSDJ"].Contains("E"))
                //{
                //    if (MItem[0]["JCYJ"].Contains("GB/T 8626-2007"))
                //    {
                //        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 8626-2007";
                //    }
                //    if (MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                //    {
                //        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                //    }

                //    IList<IDictionary<string, string>> extraFieldsDj = null;
                //    if (sItem["YPXZ"] == "管状")
                //    {
                //        if (GetSafeDouble("GZWJ") < 300)
                //        {
                //            extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "管状" && u["RFDJ"] == "B2").ToList();
                //        }
                //        else
                //        {
                //            extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "平板状" && u["RFDJ"] == "B2").ToList();
                //        }
                //    }
                //    else
                //    {
                //        extraFieldsDj = extraDJ.Where(u => u["YPXZ"] == "平板状" && u["RFDJ"] == "B2").ToList();
                //    }

                //    int y = 1;
                //    int efCount = extraFieldsDj.Count;
                //    foreach (var ef in extraFieldsDj)
                //    {
                //        //MItem[0]["WHICH"] = "bgrf、bgrf_6";
                //        sItem["G_FIGRA"] = ef["G_FIGRA"];
                //        sItem["G_THR"] = ef["G_THR"];
                //        sItem["FIGRA"] = sItem["FIGRA4"];

                //        sItem["GH_FIGRA"] = IsQualified(sItem["G_FIGRA"], sItem["FIGRA4"], false);
                //        sItem["GH_THR"] = IsQualified(sItem["G_THR"], sItem["THR"], false);
                //        if (!(IsQualified(ef["G_FIGRA"], sItem["FIGRA4"], false) == "不合格" || IsQualified(ef["G_THR"], sItem["THR"], false) == "不合格"))
                //        {
                //            sItem["GH_DTRS"] = "符合" + ef["RSDJ"] + "级";
                //            djjg = ef["RSDJ"];
                //            break;
                //        }
                //        y++;
                //    }

                //    if (y > efCount)
                //    {
                //        sItem["GH_DTRS"] = "不符合B&scsub2&scend级";
                //        mbHggs1 = mbHggs1 + 1;
                //    }

                //    if (GetSafeDouble(sItem["HGS4"]) <= 1)
                //    {
                //        if (djjg == "E")
                //        {
                //            MItem[0]["WHICH"] = "bgrf、bgrf_7";
                //            sItem["G_YJGD"] = "20s内，≤150 mm";
                //        }
                //        else
                //        {
                //            sItem["G_YJGD"] = "60s内，≤150 mm";
                //        }
                //        sItem["W_YJGD"] = "是";
                //        sItem["GH_YJGD"] = "合格";
                //    }
                //    else if (GetSafeDouble(sItem["HGS5"]) <= 1)
                //    {
                //        sItem["G_YJGD"] = "20s内，≤150 mm";
                //        sItem["W_YJGD"] = "是";
                //        sItem["GH_YJGD"] = "合格";
                //        djjg = "E";
                //    }
                //    else
                //    {
                //        sItem["G_YJGD"] = "20s内，≤150 mm";
                //        sItem["W_YJGD"] = "否";
                //        sItem["GH_YJGD"] = "不合格";
                //        mbHggs1 = mbHggs1 + 1;
                //    }

                //    if (GetSafeDouble(sItem["HGS1"].Trim()) <= 1)
                //    {
                //        if (djjg == "E")
                //        {
                //            sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //        }
                //        else
                //        {
                //            sItem["G_SFYR"] = "60s内无燃烧滴落物引燃滤纸现象";
                //        }
                //        sItem["W_SFYR"] = "是";
                //        sItem["GH_SFYR"] = "合格";
                //    }
                //    else if (GetSafeDouble(sItem["HGS2"].Trim()) <= 1)
                //    {
                //        sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //        sItem["W_SFYR"] = "是";
                //        sItem["GH_SFYR"] = "合格";
                //        djjg = "E";
                //    }
                //    else
                //    {
                //        sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //        sItem["W_SFYR"] = "否";
                //        sItem["GH_SFYR"] = "不合格";
                //        mbHggs1 = mbHggs1 + 1;
                //    }

                //    if (jcxm.Contains("、氧指数、"))
                //    {
                //        if (!MItem[0]["JCYJ"].Contains("GB/T 2406.2-2009"))
                //        {
                //            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 2406.2-2009";
                //        }
                //        sItem["G_RSYZZ"] = "≥26";
                //        sign = true;
                //        if (sItem["GH_RSYZZ"].Trim() == "合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                //        {
                //            sItem["W_RSYZZ"] = "不低于26";
                //        }
                //        else if (sItem["GH_RSYZZ"].Trim() == "不合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                //        {
                //            sItem["W_RSYZZ"] = "低于26";
                //        }
                //        else
                //        {
                //            sign = IsNumeric(sItem["W_RSYZZ"].Trim()) && !string.IsNullOrEmpty(sItem["W_RSYZZ"]) ? sign : false;
                //            if (sign)
                //            {
                //                sItem["GH_RSYZZ"] = IsQualified(sItem["G_RSYZZ"], sItem["W_RSYZZ"], false);
                //            }
                //        }
                //        mbHggs1 = sItem["GH_RSYZZ"] == "不合格" ? mbHggs1++ : mbHggs1;
                //    }
                //    else
                //    {
                //        sItem["GH_RSYZZ"] = "----";
                //        sItem["G_RSYZZ"] = "----";
                //        sItem["W_RSYZZ"] = "----";
                //    }

                //    sItem["RSCDJ"] = djjg;
                //    if (mbHggs1 > 0)
                //    {
                //        sItem["JCJG"] = "不符合B&scsub2&scend级";
                //        jsbeizhu = "该试样燃烧性能等级不符合" + MItem[0]["PDBZ"] + "中B & scsub2 & scend级要求";
                //    }
                //    else
                //    {
                //        sItem["JCJG"] = "符合B&scsub2&scend级";
                //        jsbeizhu = "该试样燃烧性能等级符合" + MItem[0]["PDBZ"] + "中B & scsub2 & scend级要求";
                //    }
                //}

                //if (sItem["RSDJ"].Contains("B2") && sItem["RSDJ"].Contains("E"))
                //{
                //    //MItem[0]["WHICH"] = "bgrf、bgrf_5";
                //    if (!MItem[0]["JCYJ"].Contains("GB/T 8626-2007"))
                //    {
                //        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 8626-2007";
                //    }

                //    if (GetSafeDouble(sItem["HGS5"].Trim()) <= 1)
                //    {
                //        sItem["G_YJGD"] = "20s内，≤150 mm";
                //        sItem["W_YJGD"] = "是";
                //        sItem["GH_YJGD"] = "合格";
                //        djjg = "E";
                //    }
                //    else
                //    {
                //        sItem["G_YJGD"] = "20s内，≤150 mm";
                //        sItem["W_YJGD"] = "否";
                //        sItem["GH_YJGD"] = "不合格";
                //        mbHggs1 = mbHggs1 + 1;
                //    }

                //    if (GetSafeDouble(sItem["HGS2"]) <= 1)
                //    {
                //        sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //        sItem["W_SFYR"] = "是";
                //        sItem["GH_SFYR"] = "合格";
                //        djjg = "E";
                //    }
                //    else
                //    {
                //        sItem["G_SFYR"] = "20s内无燃烧滴落物引燃滤纸现象";
                //        sItem["W_SFYR"] = "否";
                //        sItem["GH_SFYR"] = "不合格";
                //        mbHggs1 = mbHggs1 + 1;
                //    }

                //    if (jcxm.Contains("、氧指数、"))
                //    {
                //        if (!MItem[0]["JCYJ"].Trim().Contains("GB/T 2406.2-2009"))
                //        {
                //            MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 2406.2-2009";
                //        }

                //        sItem["G_RSYZZ"] = "≥26";
                //        sign = true;
                //        if (sItem["GH_RSYZZ"].Trim() == "合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                //        {
                //            sItem["W_RSYZZ"] = "不低于26";
                //        }
                //        else if (sItem["GH_RSYZZ"].Trim() == "不合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                //        {
                //            sItem["W_RSYZZ"] = "低于26";
                //        }
                //        else
                //        {
                //            sign = IsNumeric(sItem["W_RSYZZ"].Trim()) && !string.IsNullOrEmpty(sItem["W_RSYZZ"].Trim()) ? sign : false;
                //            if (sign)
                //            {
                //                sItem["GH_RSYZZ"] = IsQualified(sItem["G_RSYZZ"], sItem["W_RSYZZ"], false);
                //            }
                //        }
                //        mbHggs1 = sItem["GH_RSYZZ"].Trim() == "不合格" ? mbHggs1++ : mbHggs1;
                //    }
                //    else
                //    {
                //        sItem["GH_RSYZZ"] = "----";
                //        sItem["G_RSYZZ"] = "----";
                //        sItem["W_RSYZZ"] = "----";
                //    }
                //    sItem["RSCDJ"] = "E";
                //    if (mbHggs1 > 0)
                //    {
                //        sItem["JCJG"] = "不符合任何级别";
                //        mAllHg = false;
                //        jsbeizhu = "该试样不符合" + MItem[0]["PDBZ"] + "中任何级别要求";
                //        mbhggs++;
                //    }
                //    else
                //    {
                //        sItem["JCJG"] = "符合B&scsub2&scend(E)级";
                //        jsbeizhu = "该试样符合" + MItem[0]["PDBZ"] + "中B&scsub2&scend级要求,燃烧性能细化分级为E级";
                //    }
                //}
                #endregion

                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["RFDJ"] == sItem["RSDJ"].Trim() && u["RSDJ"] == sItem["RSCDJ"].Trim() && u["YPXZ"] == sItem["YPXZ"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_LNWS"] = string.IsNullOrEmpty(extraFieldsDj["G_LNWS"]) ? extraFieldsDj["G_LNWS"] : extraFieldsDj["G_LNWS"].Trim();
                    sItem["G_ZLSS"] = string.IsNullOrEmpty(extraFieldsDj["G_ZLSS"]) ? extraFieldsDj["G_ZLSS"] : extraFieldsDj["G_ZLSS"].Trim();
                    sItem["G_RSSJ"] = string.IsNullOrEmpty(extraFieldsDj["G_RSSJ"]) ? extraFieldsDj["G_RSSJ"] : extraFieldsDj["G_RSSJ"].Trim();
                    sItem["G_FIGRA"] = string.IsNullOrEmpty(extraFieldsDj["G_FIGRA"]) ? extraFieldsDj["G_FIGRA"] : extraFieldsDj["G_FIGRA"].Trim();
                    sItem["G_RSYZZ"] = string.IsNullOrEmpty(extraFieldsDj["G_RSYZZ"]) ? extraFieldsDj["G_RSYZZ"] : extraFieldsDj["G_RSYZZ"].Trim();
                    sItem["G_SFYR"] = string.IsNullOrEmpty(extraFieldsDj["G_SFYR"]) ? extraFieldsDj["G_SFYR"] : extraFieldsDj["G_SFYR"].Trim();
                    sItem["G_SMOGRA"] = string.IsNullOrEmpty(extraFieldsDj["G_SMOGRA"]) ? extraFieldsDj["G_SMOGRA"] : extraFieldsDj["G_SMOGRA"].Trim();
                    sItem["G_TSP"] = string.IsNullOrEmpty(extraFieldsDj["G_TSP"]) ? extraFieldsDj["G_TSP"] : extraFieldsDj["G_TSP"].Trim();
                    sItem["G_YJGD"] = string.IsNullOrEmpty(extraFieldsDj["G_YJGD"]) ? extraFieldsDj["G_YJGD"] : extraFieldsDj["G_YJGD"].Trim();
                    sItem["G_THR"] = string.IsNullOrEmpty(extraFieldsDj["G_THR"]) ? extraFieldsDj["G_THR"] : extraFieldsDj["G_THR"].Trim();
                    sItem["G_ZRZ"] = string.IsNullOrEmpty(extraFieldsDj["G_ZRZ1"]) ? extraFieldsDj["G_ZRZ1"] : extraFieldsDj["G_ZRZ1"].Trim();

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

                }
                else
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 火焰横向蔓延长度
                if (jcxm.Contains("、火焰横向蔓延长度、"))
                {
                    jcxmCur = "火焰横向蔓延长度";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                    }
                    //火焰横向蔓延长度
                    if (sItem["GH_HXMY"] == "合格")
                    {
                        sItem["W_HXMY"] = "是";
                        sItem["HXMY1"] = "是";
                        sItem["HXMY2"] = "是";
                        sItem["HXMY3"] = "是";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["W_HXMY"] = "否";
                        sItem["HXMY1"] = "否";
                        sItem["HXMY2"] = "否";
                        sItem["HXMY3"] = "否";
                        mAllHg = false;
                    }

                    //mbHggs3 = sItem["GH_HXMY"] == "不合格" ? mbHggs3++ : mbHggs3;
                }
                else
                {
                    sItem["GH_HXMY"] = "----";
                    sItem["W_HXMY"] = "----";
                }
                #endregion

                #region 燃烧增长速率指数FIGRA
                if (jcxm.Contains("、燃烧增长速率指数FIGRA、"))
                {
                    jcxmCur = "燃烧增长速率指数FIGRA";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                    }

                    sign = true;
                    //sItem["FIGRA"] = sItem["FIGRA2"];
                    sign = (IsNumeric(sItem["FIGRA"]) && !string.IsNullOrEmpty(sItem["FIGRA"])) ? sign : false;

                    if (sign)
                    {
                        sItem["GH_FIGRA"] = IsQualified(sItem["G_FIGRA"], sItem["FIGRA"], false);
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_FIGRA"] = "不合格";
                    }
                    //mbHggs3 = sItem["GH_FIGRA"] == "不合格" ? mbHggs3++ : mbHggs3;
                    mAllHg = sItem["GH_FIGRA"] == "不合格" ? false : mAllHg;
                }
                else
                {
                    sItem["GH_FIGRA"] = "----";
                    sItem["FIGRA"] = "----";
                    sItem["G_FIGRA"] = "----";
                }
                #endregion

                #region 燃烧滴落物/微粒
                if (jcxm.Contains("、燃烧滴落物/微粒、"))
                {
                    jcxmCur = "燃烧滴落物/微粒";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                    }

                    //for (int i = 1; i < 4; i++)
                    //{
                    //    if (sItem["DLW" + i] == "无")
                    //    {
                    //        sItem["HGS6"] = (GetSafeDouble(sItem["HGS6"].Trim()) - 1).ToString();
                    //    }
                    //}

                    //if (sItem["GH_D0"] == "合格")
                    //{
                    //    sItem["W_RSDR"] = "无";
                    //}
                    //else
                    //{
                    //    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    //    sItem["W_RSDR"] = "有";
                    //    mAllHg = false;
                    //}

                    switch (sItem["GH_D0"])
                    {
                        case "d0":
                            sItem["W_RSDR"] = "600s内无燃烧滴落物/微粒";
                            sItem["RSDR1"] = "600s内无燃烧滴落物/微粒";
                            sItem["RSDR2"] = "600s内无燃烧滴落物/微粒";
                            sItem["RSDR3"] = "600s内无燃烧滴落物/微粒";
                            break;
                        case "d1":
                            sItem["W_RSDR"] = "600s内无燃烧滴落物/微粒，持续时间不超过10s";
                            sItem["RSDR1"] = "600s内无燃烧滴落物/微粒，持续时间不超过10s";
                            sItem["RSDR2"] = "600s内无燃烧滴落物/微粒，持续时间不超过10s";
                            sItem["RSDR3"] = "600s内无燃烧滴落物/微粒，持续时间不超过10s";
                            break;
                        case "d2":
                            sItem["W_RSDR"] = "未达到600s内无燃烧滴落物/微粒，持续时间不超过10s";
                            sItem["RSDR1"] = "未达到600s内无燃烧滴落物/微粒，持续时间不超过10s";
                            sItem["RSDR2"] = "未达到600s内无燃烧滴落物/微粒，持续时间不超过10s";
                            sItem["RSDR3"] = "未达到600s内无燃烧滴落物/微粒，持续时间不超过10s";
                            break;
                    }
                }
                else
                {
                    sItem["W_RSDR"] = "----";
                    sItem["GH_D0"] = "----";
                }
                #endregion

                #region 总放热量
                if (jcxm.Contains("、总放热量、"))
                {
                    jcxmCur = "总放热量";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                    }

                    sign = true;
                    sign = (IsNumeric(sItem["THR"]) && !string.IsNullOrEmpty(sItem["THR"])) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_THR"] = IsQualified(sItem["G_THR"], sItem["THR"], false);
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_THR"] = "不合格";
                    }

                    //mbHggs3 = sItem["GH_THR"] == "不合格" ? mbHggs3++ : mbHggs3;
                    mAllHg = sItem["GH_THR"] == "不合格" ? false : mAllHg;
                }
                else
                {
                    sItem["GH_THR"] = "----";
                    sItem["THR"] = "----";
                    sItem["G_THR"] = "----";
                }
                #endregion

                #region 引燃滤纸现象
                if (jcxm.Contains("、引燃滤纸现象、"))
                {
                    jcxmCur = "引燃滤纸现象";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 8626-2007"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 8626-2007";
                    }

                    //if (GetSafeDouble(sItem["HGS1"].Trim()) <= 1)
                    //{
                    //    sItem["W_SFYR"] = "是";
                    //    sItem["GH_SFYR"] = "合格";
                    //}
                    //else if (GetSafeDouble(sItem["HGS2"].Trim()) <= 1)
                    //{
                    //    sItem["W_SFYR"] = "是";
                    //    sItem["GH_SFYR"] = "合格";
                    //}
                    if (sItem["GH_SFYR"] == "合格")
                    {
                        sItem["W_SFYR"] = "是";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["W_SFYR"] = "否";
                        sItem["GH_SFYR"] = "不合格";
                        //mbHggs1 = mbHggs1 + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["W_SFYR"] = "----";
                    sItem["GH_SFYR"] = "----";
                    sItem["G_SFYR"] = "----";
                }
                #endregion

                #region 焰尖高度

                if (jcxm.Contains("、焰尖高度、"))
                {
                    jcxmCur = "焰尖高度";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 8626-2007"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 8626-2007";
                    }

                    //if (GetSafeDouble(sItem["HGS4"].Trim()) <= 1)
                    //{
                    //    sItem["W_YJGD"] = "是";
                    //    sItem["GH_YJGD"] = "合格";
                    //}
                    //else
                    //{
                    //    sItem["W_YJGD"] = "否";
                    //    sItem["GH_YJGD"] = "不合格";
                    //    //mbHggs1 = mbHggs1 + 1;
                    //    mAllHg = false;
                    //}

                    //if (GetSafeDouble(sItem["HGS5"].Trim()) <= 1)
                    //{
                    //    sItem["W_YJGD"] = "是";
                    //    sItem["GH_YJGD"] = "合格";
                    //}
                    //else
                    //{
                    //    sItem["W_YJGD"] = "否";
                    //    sItem["GH_YJGD"] = "不合格";
                    //    //mbHggs1 = mbHggs1 + 1;
                    //    mAllHg = false;
                    //}
                    if (IsQualified("≤150", sItem["W_YJGD"], false) == "合格")
                    {
                        sItem["GH_YJGD"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_YJGD"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_YJGD"] = "----";
                    sItem["W_YJGD"] = "----";
                    sItem["GH_YJGD"] = "----";
                }
                #endregion

                #region 持续燃烧时间
                if (jcxm.Contains("、持续燃烧时间、"))
                {
                    jcxmCur = "持续燃烧时间";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 5464-2010"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 5464-2010";
                    }
                    string drxs = IsQualified(sItem["G_RSSJ"].Replace("=", ""), sItem["W_RSSJ"], true);
                    if (drxs == "符合")
                    {
                        sItem["GH_RSSJ"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_RSSJ"] = "不合格";
                        //mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["W_RSSJ"] = "----";
                    sItem["GH_RSSJ"] = "----";
                    sItem["G_RSSJ"] = "----";
                }
                #endregion

                #region 质量损失率
                if (jcxm.Contains("、质量损失率、"))
                {
                    jcxmCur = "质量损失率";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 5464-2010"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 5464-2010";
                    }
                    string drxs = IsQualified(sItem["G_ZLSS"], sItem["W_ZLSS"], true);
                    if (drxs == "符合")
                    {
                        sItem["GH_ZLSS"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_ZLSS"] = "不合格";
                        //mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["W_ZLSS"] = "----";
                    sItem["GH_ZLSS"] = "----";
                    sItem["G_ZLSS"] = "----";
                }
                #endregion

                #region 炉内温升
                if (jcxm.Contains("、炉内温升、"))
                {
                    jcxmCur = "质量损失率";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 5464-2010"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 5464-2010";
                    }
                    string drxs = IsQualified(sItem["G_LNWS"], sItem["W_LNWS"], true);
                    if (drxs == "符合")
                    {
                        sItem["GH_LNWS"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_LNWS"] = "不合格";
                        //mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["W_LNWS"] = "----";
                    sItem["GH_LNWS"] = "----";
                    sItem["G_LNWS"] = "----";
                }
                #endregion

                #region 总热值
                if (jcxm.Contains("、总热值、"))
                {
                    jcxmCur = "总热值";
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
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["GH_RSRZ"] = "不符合" + sItem["RSCDJ"] + "级";
                        mAllHg = false;
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

                #region 氧指数
                if (jcxm.Contains("、氧指数、"))
                {
                    jcxmCur = "氧指数";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 2406.2-2009"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 2406.2-2009";
                    }
                    //sItem["G_RSYZZ"] = "≥26";
                    sign = true;
                    if (sItem["GH_RSYZZ"].Trim() == "合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                    {
                        if (sItem["G_RSYZZ"].Contains("30"))
                        {
                            sItem["W_RSYZZ"] = "不低于30";
                        }
                        else if (sItem["G_RSYZZ"].Contains("26"))
                        {
                            sItem["W_RSYZZ"] = "不低于26";
                        }
                    }
                    else if (sItem["GH_RSYZZ"].Trim() == "不合格" && (sItem["W_RSYZZ"].Trim() == "" || sItem["W_RSYZZ"].Contains("低于")))
                    {
                        if (sItem["G_RSYZZ"].Contains("30"))
                        {
                            sItem["W_RSYZZ"] = "不低于30";
                        }
                        else if (sItem["G_RSYZZ"].Contains("26"))
                        {
                            sItem["W_RSYZZ"] = "不低于26";
                        }
                    }
                    else
                    {
                        sign = IsNumeric(sItem["W_RSYZZ"].Trim()) && !string.IsNullOrEmpty(sItem["W_RSYZZ"]) ? sign : false;
                        if (sign)
                        {
                            sItem["GH_RSYZZ"] = IsQualified(sItem["G_RSYZZ"], sItem["W_RSYZZ"], false);
                        }
                    }
                    if (sItem["GH_RSYZZ"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    //mbHggs1 = sItem["GH_RSYZZ"] == "不合格" ? mbHggs1++ : mbHggs1;
                    mAllHg = sItem["GH_RSYZZ"] == "不合格" ? false : mAllHg;
                }
                else
                {
                    sItem["GH_RSYZZ"] = "----";
                    sItem["G_RSYZZ"] = "----";
                    sItem["W_RSYZZ"] = "----";
                }
                #endregion

                #region 烟气生成速率SMOGRA
                if (jcxm.Contains("、烟气生成速率、"))
                {
                    jcxmCur = "烟气生成速率SMOGRA";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                    }
                    //string drxs = IsQualified(sItem["G_SMOGRA"], sItem["SMOGRA"], true);
                    //if (drxs == "符合")
                    //{
                    //    sItem["GH_SMOGRA"] = "合格";
                    //}
                    //else
                    //{
                    //    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    //    sItem["GH_SMOGRA"] = "不合格";
                    //    mAllHg = false;
                    //}

                    //switch (sItem["GH_SMOGRA"])
                    //{
                    //    case "s1":
                    //        if (sItem["YPXZ"].Contains("平板状建筑材料"))
                    //        {
                    //            sItem["G_SMOGRA"] = "烟气生成速率指数SMOGRA≤30";
                    //        }
                    //        else if (sItem["YPXZ"].Contains("管状绝热材料"))
                    //        {
                    //            sItem["G_SMOGRA"] = "烟气生成速率指数SMOGRA≤105";
                    //        }
                    //        break;
                    //    case "s2":
                    //        if (sItem["YPXZ"].Contains("平板状建筑材料"))
                    //        {
                    //            sItem["G_SMOGRA"] = "烟气生成速率指数SMOGRA≤180";
                    //        }
                    //        else if (sItem["YPXZ"].Contains("管状绝热材料"))
                    //        {
                    //            sItem["G_SMOGRA"] = "烟气生成速率指数SMOGRA≤580";
                    //        }
                    //        break;
                    //    case "s3":
                    //        sItem["G_SMOGRA"] = "未达到烟气生成速率指数SMOGRA≤580";
                    //        break;

                    //}     

                    switch (sItem["YPXZ"])
                    {
                        case "平板状建筑材料":
                            if (GetSafeDouble(sItem["SMOGRA"]) <= 30)
                            {
                                sItem["G_SMOGRA"] = "≤30";
                                sItem["GH_SMOGRA"] = "s1";
                            }
                            else if (GetSafeDouble(sItem["SMOGRA"]) <= 180)
                            {
                                sItem["G_SMOGRA"] = "≤180";
                                sItem["GH_SMOGRA"] = "s2";
                            }
                            else
                            {
                                sItem["G_SMOGRA"] = ">180";
                                sItem["GH_SMOGRA"] = "s3";
                            }
                            break;
                        case "管状绝热材料":
                            if (GetSafeDouble(sItem["SMOGRA"]) <= 105)
                            {
                                sItem["G_SMOGRA"] = "≤105";
                                sItem["GH_SMOGRA"] = "s1";
                            }
                            else if (GetSafeDouble(sItem["SMOGRA"]) <= 580)
                            {
                                sItem["G_SMOGRA"] = "≤580";
                                sItem["GH_SMOGRA"] = "s2";
                            }
                            else
                            {
                                sItem["G_SMOGRA"] = ">580";
                                sItem["GH_SMOGRA"] = "s3";
                            }
                            break;
                    }

                }
                else
                {
                    sItem["GH_SMOGRA"] = "----";
                    sItem["G_SMOGRA"] = "----";
                    sItem["SMOGRA"] = "----";
                }
                #endregion

                #region 烟气产生量TSP600S
                if (jcxm.Contains("、总烟气生成量TSP600S、"))
                {
                    jcxmCur = "总烟气生成量TSP600S";
                    if (!MItem[0]["JCYJ"].Contains("GB/T 20284-2006"))
                    {
                        MItem[0]["JCYJ"] = MItem[0]["JCYJ"] + "、GB/T 20284-2006";
                    }
                    //string drxs = IsQualified(sItem["G_TSP"], sItem["TSP"], true);
                    //if (drxs == "符合")
                    //{
                    //    sItem["GH_TSP"] = "合格";
                    //}
                    //else
                    //{
                    //    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    //    sItem["GH_TSP"] = "不合格";
                    //    mAllHg = false;
                    //}

                    switch (sItem["YPXZ"])
                    {
                        case "平板状建筑材料":
                            if (GetSafeDouble(sItem["TSP"]) <= 50)
                            {
                                sItem["GH_TSP"] = "s1";
                                sItem["G_TSP"] = "≤50";
                            }
                            else if (GetSafeDouble(sItem["TSP"]) <= 200)
                            {
                                sItem["GH_TSP"] = "s2";
                                sItem["G_TSP"] = "≤200";
                            }
                            else
                            {
                                sItem["GH_TSP"] = "s3";
                                sItem["G_TSP"] = ">200";
                            }
                            break;
                        case "管状绝热材料":
                            if (GetSafeDouble(sItem["TSP"]) <= 250)
                            {
                                sItem["GH_TSP"] = "s1";
                                sItem["G_TSP"] = "≤250";
                            }
                            else if (GetSafeDouble(sItem["TSP"]) <= 1600)
                            {
                                sItem["GH_TSP"] = "s2";
                                sItem["G_TSP"] = "≤1600";
                            }
                            else
                            {
                                sItem["GH_TSP"] = "s3";
                                sItem["G_TSP"] = ">1600";
                            }
                            break;
                    }
                }
                else
                {
                    sItem["GH_TSP"] = "----";
                    sItem["TSP"] = "----";
                    sItem["G_TSP"] = "----";
                }
                #endregion

                //#region 判断单体燃烧结果
                //if (mbHggs3 == 0)
                //{
                //    sItem["GH_DTRS"] = "符合" + sItem["RSCDJ"].Trim() + "级";
                //}
                //else
                //{
                //    sItem["GH_DTRS"] = "不符合" + sItem["RSCDJ"].Trim() + "级";
                //}
                //#endregion

                string sdj = ""; //产烟特性等级
                if (sItem["GH_SMOGRA"].Contains("s") && sItem["GH_TSP"].Contains("s"))
                {
                    if (GetSafeDouble(sItem["GH_SMOGRA"].Replace("s", "")) > GetSafeDouble(sItem["GH_TSP"].Replace("s", "")))
                    {
                        sdj = "，产烟特性等级为 " + sItem["GH_SMOGRA"] + " 级";
                    }
                    else
                    {
                        sdj = "，产烟特性等级为 " + sItem["GH_TSP"] + " 级";
                    }
                }

                string dlw = "";    //燃烧滴落物/微粒等级
                if (sItem["GH_D0"].Contains("d"))
                {
                    dlw = "，燃烧滴落物/微粒等级为 " + sItem["GH_D0"] + " 级";
                }

                if (mAllHg)
                {
                    if (sItem["RSCDJ"] == "----")
                    {
                        sItem["JCJG"] = "符合" + sItem["RSCDJ"] + "级";
                        //jsbeizhu = "该试样符合" + sItem["RSDJ"] + "级要求";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合" + sItem["RSDJ"] + "级材料要求" + sdj + dlw + "。";
                    }
                    else
                    {
                        sItem["JCJG"] = "符合" + sItem["RSCDJ"] + "级";
                        //jsbeizhu = "该试样符合" + sItem["RSDJ"] + "中" + sItem["RSCDJ"] + "级要求";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合" + sItem["RSDJ"] + "级材料要求，燃烧性能细化分级为" + sItem["RSCDJ"] + "级" + sdj + dlw + "。";
                    }
                }
                else
                {
                    if (sItem["RSCDJ"] == "----")
                    {
                        sItem["JCJG"] = "不符合" + sItem["RSCDJ"] + "级";
                        //jsbeizhu = "该试样不符合" + sItem["RSDJ"] + "级要求";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + sItem["RSDJ"] + "级要求。";
                    }
                    else
                    {
                        sItem["JCJG"] = "不符合" + sItem["RSCDJ"] + "级";
                        //jsbeizhu = "该试样不符合" + sItem["RSDJ"] + "中" + sItem["RSCDJ"] + "级要求";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + sItem["RSDJ"] + "级材料，燃烧性能细化分级" + sItem["RSCDJ"] + "级要求。";
                    }
                }

            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            else
            {
                mjcjg = "不合格";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }

    }
}
