using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Calculates
{
    public class FS : BaseMethods
    {
        public void Calc()
        {
            #region
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_FS"];
            var MItem = data["M_FS"];
            var mrsDj = dataExtra["BZ_FS_DJ"];

            string mJSFF = "", klqdJsff = "", klqdDw = "";
            bool mFlag_Hg = false, mFlag_Bhg = false;
            int mbhggs = 0;
            var jcxmBhg = "";
            var jcxmCur = "";
            if (!data.ContainsKey("M_FS"))
            {
                data["M_FS"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            Func<double, double> myint = delegate (double dataChar)
            {
                return Math.Round(Conversion.Val(dataChar) / 5, 0) * 5;
            };

            Func<string, string, string> FuncCurrentJcxm = delegate (string jcxm, string compareItems)
            {
                compareItems = compareItems.Replace(',', '、').Trim('、');
                if (compareItems.IndexOf('、') == -1)
                {
                    return compareItems;
                }
                List<string> listItems = compareItems.Split('、').ToList();

                foreach (var item in listItems)
                {
                    if (jcxm.Contains("、" + item + "、"))
                    {
                        return item;
                    }
                }

                return "";
            };
            foreach (var sItem in SItem)
            {
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                string dCpmc = string.IsNullOrEmpty(sItem["CPMC"]) ? "" : sItem["CPMC"].Trim();
                string dXs = string.IsNullOrEmpty(sItem["XS"]) ? "" : sItem["XS"].Trim();
                string dSygn = string.IsNullOrEmpty(sItem["SYGN"]) ? "" : sItem["SYGN"].Trim();
                string dHd = string.IsNullOrEmpty(sItem["SJHD"]) ? "" : sItem["SJHD"].Trim();
                string dGgxh = string.IsNullOrEmpty(sItem["GGXH"]) ? "" : sItem["GGXH"].Trim();
                string dTjlx = string.IsNullOrEmpty(sItem["TJLX"]) ? "" : sItem["TJLX"].Trim();
                string dXh = string.IsNullOrEmpty(sItem["XH"]) ? "" : sItem["XH"].Trim();
                string dSbmcl = string.IsNullOrEmpty(sItem["SBMCL"]) ? "" : sItem["SBMCL"].Trim();
                string dZyycl = string.IsNullOrEmpty(sItem["ZYYCL"]) ? "" : sItem["ZYYCL"].Trim();
                string dBzh = string.IsNullOrEmpty(sItem["BZH"]) ? "" : sItem["BZH"].Trim();

                switch (dCpmc)
                {
                    case "弹性体改性沥青防水卷材":
                        sItem["CPBJ"] = "SBS " + dXh + " " + dTjlx + " " + dSbmcl + dHd + " " + dBzh;
                        break;
                    case "塑性体改性沥青防水卷材":
                        sItem["CPBJ"] = "APP " + dXh + " " + dTjlx + " " + dSbmcl + dHd + " " + dBzh;
                        break;
                    case "聚氯乙烯防水卷材":
                        sItem["CPBJ"] = "PVC卷材 " + dSygn + " " + dXh + " " + dHd + "/" + dGgxh + " " + dBzh;
                        break;
                    case "聚氯乙烯(PVC)防水卷材材":
                        sItem["CPBJ"] = "PVC卷材 " + dSygn + " " + dXh + " " + dHd + "/" + dGgxh + " " + dBzh;
                        break;
                    case "热塑性聚烯烃(TPO)防水卷材检测报告":
                        sItem["CPBJ"] = "TPO卷材 " + " " + dXh + " " + dHd + "/" + dGgxh + " " + dBzh;
                        break;
                    case "氯化聚乙烯防水卷材":
                        sItem["CPBJ"] = "CPE卷材 " + dSygn + " " + dXh + " " + dHd + "/" + dGgxh + " " + dBzh;
                        break;
                    case "改性沥青聚乙烯胎防水卷材":
                        sItem["CPBJ"] = dXs + " " + dXh + "" + dTjlx + "" + dSbmcl + " " + dHd + " " + dBzh;
                        break;
                    case "高分子防水卷材":
                        sItem["CPBJ"] = dXh + "-" + dZyycl + "-" + dGgxh + "×" + dHd + "mm"; break;
                    case "沥青复合胎柔性防水卷材":
                        sItem["CPBJ"] = dTjlx + " " + dXh + " " + dSbmcl + dHd + " " + dGgxh + " " + dBzh; break;
                    case "胶粉改性沥青玻纤毡与玻纤网格布增强防水卷材":
                        sItem["CPBJ"] = dTjlx + " " + dXh + " " + dSbmcl + dHd + " " + dGgxh + " " + dBzh; break;
                    case "胶粉改性沥青玻纤毡与聚乙烯膜增强防水卷材":
                        sItem["CPBJ"] = dTjlx + " " + dXh + " " + dSbmcl + dHd + " " + dGgxh + " " + dBzh; break;
                    case "胶粉改性沥青聚酯毡与玻纤网格布增强防水卷材":
                        sItem["CPBJ"] = dTjlx + " " + dXh + " " + dSbmcl + dHd + " " + dGgxh + " " + dBzh; break;
                    case "氯化聚乙烯-橡胶共混防水卷材":
                        sItem["CPBJ"] = "CPBR " + dXh + " " + dHd + " " + dBzh; break;
                    case "自粘橡胶沥青防水卷材":
                        sItem["CPBJ"] = "自粘卷材 " + (dSygn == "外露" ? "O" : "I") + dSbmcl + dHd + " " + dBzh; break;
                    case "自粘聚合物改性沥青聚脂胎防水卷材":
                        sItem["CPBJ"] = "自粘聚脂胎卷材 " + dXh + " " + dSbmcl + " " + dHd + " " + dBzh; break;
                    case "玻纤胎沥青瓦":
                        sItem["CPBJ"] = "沥青瓦" + " " + dSbmcl + dXs + dTjlx + " " + dBzh; break;
                    case "自粘聚合物改性沥青防水卷材":
                        sItem["CPBJ"] = "自粘卷材 " + dXs + " " + dXh + " " + dSbmcl + " " + dHd + " " + dGgxh + " " + dBzh; break;
                    case "石油沥青玻璃纤维胎防水卷材":
                        sItem["CPBJ"] = "沥青璃纤胎卷材" + " " + dXh + " " + dXs + " " + dSbmcl + " " + dGgxh + " " + dBzh; break;
                    case "石油沥青纸胎油毡":
                        sItem["CPBJ"] = "油毡" + dXh + " " + dBzh; break;
                    case "地下用高聚物改性沥青类防水卷材":
                        sItem["CPBJ"] = dXh + dTjlx; break;
                    case "地下用合成高分子类防水卷材":
                        sItem["CPBJ"] = dXh; break;
                    case "预铺防水卷材":
                        sItem["CPBJ"] = "Y " + dXs + " " + dSbmcl + dHd + " " + dBzh; break;
                    case "预铺防水卷材(2017)":
                        sItem["CPBJ"] = "Y " + dXs + " " + dSbmcl + dHd + " " + dBzh; break;
                    case "湿铺防水卷材(2017)":
                        sItem["CPBJ"] = "W " + dXs + " " + dSbmcl + dHd + " " + dBzh; break;
                    case "湿铺防水卷材":
                        sItem["CPBJ"] = "W " + dXs + " " + dXh + " " + dSbmcl + dHd + " " + dBzh; break;
                    case "三元丁橡胶防水卷材":
                        sItem["CPBJ"] = "三元丁卷材 " + dHd + " ";
                        if (dXh == "一等品") sItem["CPBJ"] = sItem["CPBJ"] + "B ";
                        if (dXh == "合格品") sItem["CPBJ"] = sItem["CPBJ"] + "C ";
                        sItem["CPBJ"] = sItem["CPBJ"] + "JC/T645";
                        break;
                }

                var mrsdj = mrsDj.FirstOrDefault(u => (u["MC"].Contains(dCpmc) && u["JCBZ"].Contains(dBzh) && u["XS"].Contains(dXs) && u["HD"].Contains(dHd) && u["XH"].Contains(dXh) && u["TJLX"].Contains(dTjlx) && u["SBMCL"].Contains(dSbmcl))
                                || (u["MC"].Contains(dCpmc) && u["JCBZ"].Contains(dBzh) && u["XS"].Contains(dXs) && u["HD"].Contains(dHd + ".0") && u["XH"].Contains(dXh) && u["TJLX"].Contains(dTjlx) && u["SBMCL"].Contains(dSbmcl))
                                || (u["MC"].Contains(dCpmc) && u["JCBZ"].Contains(dBzh) && u["XS"].Contains(dXs) && u["HD"].Contains("----") && u["XH"].Contains(dXh) && u["TJLX"].Contains(dTjlx) && u["SBMCL"].Contains(dSbmcl))
                                || (u["MC"].Contains(dCpmc) && u["JCBZ"].Contains(dBzh) && u["XS"].Contains(dXs) && u["HD"].Contains("") && u["XH"].Contains(dXh) && u["TJLX"].Contains(dTjlx) && u["SBMCL"].Contains(dSbmcl)));

                switch (dCpmc)
                {
                    case "预铺防水卷材":
                        mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == dCpmc && u["XS"] == dXs); break;
                    case "预铺防水卷材(2017)":
                        sItem["YPMC"] = "预铺防水卷材";
                        mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == dCpmc && u["XS"] == dXs); break;
                    case "湿铺防水卷材(2017)":
                        sItem["YPMC"] = "湿铺防水卷材";
                        mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == dCpmc && u["XS"] == dXs); break;
                    case "湿铺防水卷材":
                        mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == dCpmc && u["XS"] == dXs && u["XH"] == dXh); break;
                    case "三元丁橡胶防水卷材":
                        mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == dCpmc && u["XH"] == dXh); break;
                    case "热塑性聚烯烃(TPO)防水卷材":
                        mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == dCpmc && u["XH"] == dXh); break;
                    case "高分子防水卷材":
                        mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == dCpmc && u["JCBZ"] == dBzh && u["XH"] == dXh);
                        break;
                }

                if (null != mrsdj)
                {
                    mJSFF = string.IsNullOrEmpty(mrsdj["JSFF"]) ? "" : mrsdj["JSFF"];
                    mItem["GV_SLQD"] = string.IsNullOrEmpty(mrsdj["V_SLQD"]) ? "" : mrsdj["V_SLQD"];
                    mItem["GH_SLQD"] = mrsdj["H_SLQD"] == null ? "" : mrsdj["H_SLQD"].Trim();
                    mItem["GV_KLQD"] = mrsdj["V_KLQD"] == null ? "" : mrsdj["V_KLQD"].Trim();
                    mItem["GH_KLQD"] = mrsdj["H_KLQD"] == null ? "" : mrsdj["H_KLQD"].Trim();
                    klqdJsff = mrsdj["KLQDJSFF"] == null ? "" : mrsdj["KLQDJSFF"].Trim();
                    klqdDw = mrsdj["KLQDDW"] == null ? "" : mrsdj["KLQDDW"].Trim();
                    mItem["GV_KLQD2"] = mrsdj["V_KLQD2"] == null ? "" : mrsdj["V_KLQD2"].Trim();
                    mItem["GH_KLQD2"] = mrsdj["H_KLQD2"] == null ? "" : mrsdj["H_KLQD2"].Trim();
                    mItem["GV_KLQD3"] = mrsdj["V_KLQD3"] == null ? "" : mrsdj["V_KLQD3"].Trim();
                    mItem["GH_KLQD3"] = mrsdj["H_KLQD3"] == null ? "" : mrsdj["H_KLQD3"].Trim();
                    mItem["GV_KLQD4"] = mrsdj["V_KLQD4"] == null ? "" : mrsdj["V_KLQD4"].Trim();
                    mItem["GH_KLQD4"] = mrsdj["H_KLQD4"] == null ? "" : mrsdj["H_KLQD4"].Trim();
                    mItem["G_KLSYXX"] = mrsdj["KLSYXX"] == null ? "" : mrsdj["KLSYXX"].Trim();
                    mItem["GV_SCL"] = mrsdj["V_SCL"] == null ? "" : mrsdj["V_SCL"].Trim();
                    mItem["GH_SCL"] = mrsdj["H_SCL"] == null ? "" : mrsdj["H_SCL"].Trim();
                    mItem["GV_SCL2"] = mrsdj["V_SCL2"] == null ? "" : mrsdj["V_SCL2"].Trim();
                    mItem["GH_SCL2"] = mrsdj["H_SCL2"] == null ? "" : mrsdj["H_SCL2"].Trim();
                    mItem["GV_SCL3"] = mrsdj["V_SCL3"] == null ? "" : mrsdj["V_SCL3"].Trim();
                    mItem["GH_SCL3"] = mrsdj["H_SCL3"] == null ? "" : mrsdj["H_SCL3"].Trim();
                    mItem["GV_SCL4"] = mrsdj["V_SCL4"] == null ? "" : mrsdj["V_SCL4"].Trim();
                    mItem["GH_SCL4"] = mrsdj["H_SCL4"] == null ? "" : mrsdj["H_SCL4"].Trim();
                    mItem["GV_CCBHL"] = mrsdj["V_CCBHL"] == null ? "" : mrsdj["V_CCBHL"].Trim();
                    mItem["GH_CCBHL"] = mrsdj["H_CCBHL"] == null ? "" : mrsdj["H_CCBHL"].Trim();
                    mItem["GV_CCBHL2"] = mrsdj["V_CCBHL2"] == null ? "" : mrsdj["V_CCBHL2"].Trim();
                    mItem["GH_CCBHL2"] = mrsdj["H_CCBHL2"] == null ? "" : mrsdj["H_CCBHL2"].Trim();
                    mItem["G_KRWHL"] = mrsdj["KRWHL"] == null ? "" : mrsdj["KRWHL"].Trim();
                    mItem["G_RCLWD"] = mrsdj["RCLWD"] == null ? "" : mrsdj["RCLWD"].Trim();
                    mItem["G_DWRD"] = mrsdj["DWRD"] == null ? "" : mrsdj["DWRD"].Trim();
                    mItem["G_DWWZX"] = mrsdj["DWWZX"] == null ? "" : mrsdj["DWWZX"].Trim();
                    mItem["G_NRD"] = mrsdj["NRD"] == null ? "" : mrsdj["NRD"].Trim();
                    mItem["G_KCKX"] = mrsdj["KCKX"] == null ? "" : mrsdj["KCKX"].Trim();
                    mItem["G_SZQTSL"] = mrsdj["SZQTSL"] == null ? "" : mrsdj["SZQTSL"].Trim();
                    mItem["G_BLX"] = mrsdj["BLX"] == null ? "" : mrsdj["BLX"].Trim();
                    mItem["G_LHX"] = mrsdj["LHX"] == null ? "" : mrsdj["LHX"].Trim();
                    mItem["G_BTSX"] = mrsdj["BTSX"] == null ? "" : mrsdj["BTSX"].Trim();
                    mItem["G_SYX"] = mrsdj["SYX"] == null ? "" : mrsdj["SYX"].Trim();

                    if ((dXh == "FS1" || dXh == "FS2") && Conversion.Val(dHd) < 1 && dBzh == "GB 18173.1-2006")
                    {
                        mItem["GV_SLQD"] = (Conversion.Val(mrsdj["V_SLQD"].Trim()) * 0.8).ToString();
                        mItem["GH_SLQD"] = (Conversion.Val(mrsdj["H_SLQD"].Trim()) * 0.8).ToString();
                        mItem["GV_KLQD"] = (Conversion.Val(mrsdj["V_KLQD"].Trim()) * 0.8).ToString();
                        mItem["GH_KLQD"] = (Conversion.Val(mrsdj["H_KLQD"].Trim()) * 0.8).ToString();
                        mItem["GV_KLQD2"] = (Conversion.Val(mrsdj["V_KLQD2"].Trim()) * 0.8).ToString();
                        mItem["GH_KLQD2"] = (Conversion.Val(mrsdj["H_KLQD2"].Trim()) * 0.8).ToString();
                        mItem["GV_KLQD3"] = (Conversion.Val(mrsdj["V_KLQD3"].Trim()) * 0.8).ToString();
                        mItem["GH_KLQD3"] = (Conversion.Val(mrsdj["H_KLQD3"].Trim()) * 0.8).ToString();
                        mItem["GV_SCL"] = "50";
                        mItem["GH_SCL"] = "50";
                        mItem["GV_SCL2"] = "50";
                        mItem["GH_SCL2"] = "50";
                        mItem["GV_SCL3"] = "50";
                        mItem["GH_SCL3"] = "50";
                        mItem["GV_CCBHL"] = (Conversion.Val(mrsdj["V_CCBHL"].Trim()) * 0.8).ToString();
                        mItem["GH_CCBHL"] = (Conversion.Val(mrsdj["H_CCBHL"].Trim()) * 0.8).ToString();
                        mItem["GV_CCBHL2"] = (Conversion.Val(mrsdj["V_CCBHL2"].Trim()) * 0.8).ToString();
                        mItem["GH_CCBHL2"] = (Conversion.Val(mrsdj["H_CCBHL2"].Trim()) * 0.8).ToString();
                    }
                    if ((dXh == "FS2") && Conversion.Val(dHd) < 1 && dBzh == "GB 18173.1-2012")
                    {
                        mItem["GV_KLQD"] = "50";
                        mItem["GH_KLQD"] = "50";
                        mItem["GV_KLQD2"] = "30";
                        mItem["GH_KLQD2"] = "30";
                        mItem["GV_SCL"] = "100";
                        mItem["GH_SCL"] = "100";
                        mItem["GV_SCL3"] = "80";
                        mItem["GH_SCL3"] = "80";
                    }
                    if (dXh == "FF" && dBzh == "GB 18173.1-2012")
                    {
                        if (dTjlx == "聚酯胎体" && dSbmcl == "三元乙丙橡胶")
                        {
                            mItem["GV_SCL"] = "100";
                            mItem["GH_SCL"] = "100";
                            mItem["GV_SCL3"] = "100";
                            mItem["GH_SCL3"] = "100";
                        }
                    }
                    sItem["KLQDDW"] = klqdDw;
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "依据不详";
                    jsbeizhu = "找不到对应的等级";
                    continue;
                }

                double md, md1, md2, sum;
                bool sign = false, mark, flag = false;

                flag = false;
                if (jcxm.Contains("拉力") || jcxm.Contains("、断裂拉伸强度、") || jcxm.Contains("拉伸性能") || jcxm.Contains("拉伸强度"))
                {
                    jcxmCur = FuncCurrentJcxm(jcxm, "拉力,断裂拉伸强度,拉伸性能,拉伸强度");
                    flag = true;
                    double sKlqd1 = 0, sKlqd2 = 0, sKlqd3 = 0, sKlqd4 = 0, sKlqd5 = 0, sKlqd6 = 0;
                    double sKlmj1 = 0, sKlmj2 = 0, sKlmj3 = 0, sKlmj4 = 0, sKlmj5 = 0, sKlmj6 = 0;
                    switch (klqdJsff)
                    {
                        case "1":
                            sKlmj1 = Conversion.Val(sItem["V_KLKD1"]) * Conversion.Val(sItem["V_KLHD1"]);
                            sKlmj2 = Conversion.Val(sItem["V_KLKD2"]) * Conversion.Val(sItem["V_KLHD2"]);
                            sKlmj3 = Conversion.Val(sItem["V_KLKD3"]) * Conversion.Val(sItem["V_KLHD3"]);
                            sKlmj4 = Conversion.Val(sItem["V_KLKD4"]) * Conversion.Val(sItem["V_KLHD4"]);
                            sKlmj5 = Conversion.Val(sItem["V_KLKD5"]) * Conversion.Val(sItem["V_KLHD5"]);
                            if (sKlmj1 == 0) sKlmj1 = Conversion.Val(sItem["V_KLKD1"]);
                            if (sKlmj2 == 0) sKlmj2 = Conversion.Val(sItem["V_KLKD2"]);
                            if (sKlmj3 == 0) sKlmj3 = Conversion.Val(sItem["V_KLKD3"]);
                            if (sKlmj4 == 0) sKlmj4 = Conversion.Val(sItem["V_KLKD4"]);
                            if (sKlmj5 == 0) sKlmj6 = Conversion.Val(sItem["V_KLKD5"]);


                            if (sKlmj1 != 0) sKlqd1 = Round(Conversion.Val(sItem["V_KLHZ1"]) / sKlmj1, 1);
                            if (sKlmj2 != 0) sKlqd2 = Round(Conversion.Val(sItem["V_KLHZ2"]) / sKlmj2, 1);
                            if (sKlmj3 != 0) sKlqd3 = Round(Conversion.Val(sItem["V_KLHZ3"]) / sKlmj3, 1);
                            if (sKlmj4 != 0) sKlqd4 = Round(Conversion.Val(sItem["V_KLHZ4"]) / sKlmj4, 1);
                            if (sKlmj5 != 0) sKlqd5 = Round(Conversion.Val(sItem["V_KLHZ5"]) / sKlmj5, 1);
                            string mlongStr = sKlqd1 + "," +
                                    sKlqd2 + "," +
                                    sKlqd3 + "," +
                                    sKlqd4 + "," +
                                    sKlqd5;
                            string[] mtmpArray = mlongStr.Split(',');
                            List<string> mfuncVal = mtmpArray.ToList();
                            sItem["V_KLQD"] = Round(Conversion.Val(mtmpArray[2]), 1).ToString();


                            sKlmj1 = Conversion.Val(sItem["H_KLKD1"]) * Conversion.Val(sItem["H_KLHD1"]);
                            sKlmj2 = Conversion.Val(sItem["H_KLKD2"]) * Conversion.Val(sItem["H_KLHD2"]);
                            sKlmj3 = Conversion.Val(sItem["H_KLKD3"]) * Conversion.Val(sItem["H_KLHD3"]);
                            sKlmj4 = Conversion.Val(sItem["H_KLKD4"]) * Conversion.Val(sItem["H_KLHD4"]);
                            sKlmj5 = Conversion.Val(sItem["H_KLKD5"]) * Conversion.Val(sItem["H_KLHD5"]);
                            if (sKlmj1 != 0) sKlqd1 = Round(Conversion.Val(sItem["H_KLHZ1"]) / sKlmj1, 1);
                            if (sKlmj2 != 0) sKlqd2 = Round(Conversion.Val(sItem["H_KLHZ2"]) / sKlmj2, 1);
                            if (sKlmj3 != 0) sKlqd3 = Round(Conversion.Val(sItem["H_KLHZ3"]) / sKlmj3, 1);
                            if (sKlmj4 != 0) sKlqd4 = Round(Conversion.Val(sItem["H_KLHZ4"]) / sKlmj4, 1);
                            if (sKlmj5 != 0) sKlqd5 = Round(Conversion.Val(sItem["H_KLHZ5"]) / sKlmj5, 1);
                            mlongStr = sKlqd1 + "," +
                                    sKlqd2 + "," +
                                    sKlqd3 + "," +
                                    sKlqd4 + "," +
                                    sKlqd5;
                            mtmpArray = mlongStr.Split(',');
                            mfuncVal = mtmpArray.ToList();
                            sItem["H_KLQD"] = Round(Conversion.Val(mtmpArray[2]), 1).ToString();
                            break;
                        case "2":
                            sKlmj1 = Conversion.Val(sItem["V_KLKD1"]);
                            sKlmj2 = Conversion.Val(sItem["V_KLKD2"]);
                            sKlmj3 = Conversion.Val(sItem["V_KLKD3"]);
                            sKlmj4 = Conversion.Val(sItem["V_KLKD4"]);
                            sKlmj5 = Conversion.Val(sItem["V_KLKD5"]);
                            if (sKlmj1 != 0) sKlqd1 = Round(10 * Conversion.Val(sItem["V_KLHZ1"]) / sKlmj1, 1);
                            if (sKlmj2 != 0) sKlqd2 = Round(10 * Conversion.Val(sItem["V_KLHZ2"]) / sKlmj2, 1);
                            if (sKlmj3 != 0) sKlqd3 = Round(10 * Conversion.Val(sItem["V_KLHZ3"]) / sKlmj3, 1);
                            if (sKlmj4 != 0) sKlqd4 = Round(10 * Conversion.Val(sItem["V_KLHZ4"]) / sKlmj4, 1);
                            if (sKlmj5 != 0) sKlqd5 = Round(10 * Conversion.Val(sItem["V_KLHZ5"]) / sKlmj5, 1);
                            mlongStr = sKlqd1 + "," +
                                    sKlqd2 + "," +
                                    sKlqd3 + "," +
                                    sKlqd4 + "," +
                                    sKlqd5;
                            mtmpArray = mlongStr.Split(',');
                            mfuncVal = mtmpArray.ToList();
                            sItem["V_KLQD"] = Round(Conversion.Val(mtmpArray[2]), 1).ToString();


                            sKlmj1 = Conversion.Val(sItem["H_KLKD1"]);
                            sKlmj2 = Conversion.Val(sItem["H_KLKD2"]);
                            sKlmj3 = Conversion.Val(sItem["H_KLKD3"]);
                            sKlmj4 = Conversion.Val(sItem["H_KLKD4"]);
                            sKlmj5 = Conversion.Val(sItem["H_KLKD5"]);
                            if (sKlmj1 != 0) sKlqd1 = Round(10 * Conversion.Val(sItem["H_KLHZ1"]) / sKlmj1, 1);
                            if (sKlmj2 != 0) sKlqd2 = Round(10 * Conversion.Val(sItem["H_KLHZ2"]) / sKlmj2, 1);
                            if (sKlmj3 != 0) sKlqd3 = Round(10 * Conversion.Val(sItem["H_KLHZ3"]) / sKlmj3, 1);
                            if (sKlmj4 != 0) sKlqd4 = Round(10 * Conversion.Val(sItem["H_KLHZ4"]) / sKlmj4, 1);
                            if (sKlmj5 != 0) sKlqd5 = Round(10 * Conversion.Val(sItem["H_KLHZ5"]) / sKlmj5, 1);
                            mlongStr = sKlqd1 + "," +
                                    sKlqd2 + "," +
                                    sKlqd3 + "," +
                                    sKlqd4 + "," +
                                    sKlqd5;
                            mtmpArray = mlongStr.Split(',');
                            mfuncVal = mtmpArray.ToList();
                            sItem["H_KLQD"] = Round(Conversion.Val(mtmpArray[2]), 1).ToString();
                            break;
                        case "3":
                            sKlqd1 = Conversion.Val(sItem["V_KLHZ1"]);
                            sKlqd2 = Conversion.Val(sItem["V_KLHZ2"]);
                            sKlqd3 = Conversion.Val(sItem["V_KLHZ3"]);
                            sKlqd4 = Conversion.Val(sItem["V_KLHZ4"]);
                            sKlqd5 = Conversion.Val(sItem["V_KLHZ5"]);
                            sItem["V_KLQD"] = myint((sKlqd1 + sKlqd2 + sKlqd3 + sKlqd4 + sKlqd5) / 5).ToString();

                            sKlqd1 = Conversion.Val(sItem["H_KLHZ1"]);
                            sKlqd2 = Conversion.Val(sItem["H_KLHZ2"]);
                            sKlqd3 = Conversion.Val(sItem["H_KLHZ3"]);
                            sKlqd4 = Conversion.Val(sItem["H_KLHZ4"]);
                            sKlqd5 = Conversion.Val(sItem["H_KLHZ5"]);
                            sItem["H_KLQD"] = myint((sKlqd1 + sKlqd2 + sKlqd3 + sKlqd4 + sKlqd5) / 5).ToString();
                            break;
                        case "4":
                            sKlqd1 = 2 * Conversion.Val(sItem["V_KLHZ1"]);
                            sKlqd2 = 2 * Conversion.Val(sItem["V_KLHZ2"]);
                            sKlqd3 = 2 * Conversion.Val(sItem["V_KLHZ3"]);
                            sKlqd4 = 2 * Conversion.Val(sItem["V_KLHZ4"]);
                            sKlqd5 = 2 * Conversion.Val(sItem["V_KLHZ5"]);
                            sItem["V_KLQD"] = myint((sKlqd1 + sKlqd2 + sKlqd3 + sKlqd4 + sKlqd5) / 5).ToString();

                            sKlqd1 = 2 * Conversion.Val(sItem["H_KLHZ1"]);
                            sKlqd2 = 2 * Conversion.Val(sItem["H_KLHZ2"]);
                            sKlqd3 = 2 * Conversion.Val(sItem["H_KLHZ3"]);
                            sKlqd4 = 2 * Conversion.Val(sItem["H_KLHZ4"]);
                            sKlqd5 = 2 * Conversion.Val(sItem["H_KLHZ5"]);
                            sItem["H_KLQD"] = myint((sKlqd1 + sKlqd2 + sKlqd3 + sKlqd4 + sKlqd5) / 5).ToString();
                            break;
                        case "5":
                            sum = 0;
                            for (int i = 1; i <= 5; i++)
                            {
                                md1 = Conversion.Val(sItem["V_KLKD" + i]);
                                md2 = Conversion.Val(sItem["V_KLHZ" + i]);
                                md = 10 * md2 / md1;
                                md = Round(md, 0);
                                sum = sum + md;
                            }
                            md = sum / 5;
                            md = Round(md, 0);
                            sItem["V_KLQD"] = md.ToString();
                            sum = 0;
                            for (int i = 1; i <= 5; i++)
                            {
                                md1 = Conversion.Val(sItem["V_KLKD" + i]);
                                md2 = Conversion.Val(sItem["H_KLHZ" + i]);
                                md = 10 * md2 / md1;
                                md = Round(md, 0);
                                sum = sum + md;
                            }
                            md = sum / 5;
                            md = Round(md, 0);
                            sItem["H_KLQD"] = md.ToString();
                            break;
                        case "7":
                            sum = 0;
                            for (int i = 1; i <= 6; i++)
                            {
                                md1 = Conversion.Val(sItem["V_KLKD" + i]);
                                md2 = Conversion.Val(sItem["V_KLHZ" + i]);
                                md = 10 * md2 / md1;
                                md = Round(md, 0);
                                sum = sum + md;
                            }
                            md = sum / 6;
                            md = Round(md, 0);
                            sItem["H_KLQD"] = md.ToString();
                            sum = 0;
                            for (int i = 1; i <= 6; i++)
                            {
                                md1 = Conversion.Val(sItem["H_KLKD" + i]);
                                md2 = Conversion.Val(sItem["H_KLHZ" + i]);
                                md = 10 * md2 / md1;
                                md = Round(md, 0);
                                sum = sum + md;
                            }
                            md = sum / 6;
                            md = Round(md, 0);
                            sItem["H_KLQD"] = md.ToString();
                            break;
                    }
                    if (dBzh == "GB/T 23457-2017" && dXs != "PY")
                    {
                        sKlmj1 = Conversion.Val(sItem["V_KLKD1"]) * Conversion.Val(sItem["V_KLHD1"]);
                        sKlmj2 = Conversion.Val(sItem["V_KLKD2"]) * Conversion.Val(sItem["V_KLHD2"]);
                        sKlmj3 = Conversion.Val(sItem["V_KLKD3"]) * Conversion.Val(sItem["V_KLHD3"]);
                        sKlmj4 = Conversion.Val(sItem["V_KLKD4"]) * Conversion.Val(sItem["V_KLHD4"]);
                        sKlmj5 = Conversion.Val(sItem["V_KLKD5"]) * Conversion.Val(sItem["V_KLHD5"]);
                        if (sKlmj1 == 0) sKlmj1 = Conversion.Val(sItem["V_KLKD1"]);
                        if (sKlmj2 == 0) sKlmj2 = Conversion.Val(sItem["V_KLKD2"]);
                        if (sKlmj3 == 0) sKlmj3 = Conversion.Val(sItem["V_KLKD3"]);
                        if (sKlmj4 == 0) sKlmj4 = Conversion.Val(sItem["V_KLKD4"]);
                        if (sKlmj5 == 0) sKlmj6 = Conversion.Val(sItem["V_KLKD5"]);


                        if (sKlmj1 != 0) sKlqd1 = Round(Conversion.Val(sItem["V_KLHZ21"]) / sKlmj1, 1);
                        if (sKlmj2 != 0) sKlqd2 = Round(Conversion.Val(sItem["V_KLHZ22"]) / sKlmj2, 1);
                        if (sKlmj3 != 0) sKlqd3 = Round(Conversion.Val(sItem["V_KLHZ23"]) / sKlmj3, 1);
                        if (sKlmj4 != 0) sKlqd4 = Round(Conversion.Val(sItem["V_KLHZ24"]) / sKlmj4, 1);
                        if (sKlmj5 != 0) sKlqd5 = Round(Conversion.Val(sItem["V_KLHZ25"]) / sKlmj5, 1);
                        sItem["V_KLQD2"] = Round(Conversion.Val(sItem["V_KLQD2"]), 1).ToString();


                        sKlmj1 = Conversion.Val(sItem["H_KLKD1"]) * Conversion.Val(sItem["H_KLHD1"]);
                        sKlmj2 = Conversion.Val(sItem["H_KLKD2"]) * Conversion.Val(sItem["H_KLHD2"]);
                        sKlmj3 = Conversion.Val(sItem["H_KLKD3"]) * Conversion.Val(sItem["H_KLHD3"]);
                        sKlmj4 = Conversion.Val(sItem["H_KLKD4"]) * Conversion.Val(sItem["H_KLHD4"]);
                        sKlmj5 = Conversion.Val(sItem["H_KLKD5"]) * Conversion.Val(sItem["H_KLHD5"]);
                        if (sKlmj1 != 0) sKlqd1 = Round(Conversion.Val(sItem["H_KLHZ21"]) / sKlmj1, 1);
                        if (sKlmj2 != 0) sKlqd2 = Round(Conversion.Val(sItem["H_KLHZ22"]) / sKlmj2, 1);
                        if (sKlmj3 != 0) sKlqd3 = Round(Conversion.Val(sItem["H_KLHZ23"]) / sKlmj3, 1);
                        if (sKlmj4 != 0) sKlqd4 = Round(Conversion.Val(sItem["H_KLHZ24"]) / sKlmj4, 1);
                        if (sKlmj5 != 0) sKlqd5 = Round(Conversion.Val(sItem["H_KLHZ25"]) / sKlmj5, 1);
                        sItem["H_KLQD2"] = Round((sKlqd1 + sKlqd2 + sKlqd3 + sKlqd4 + sKlqd5) / 5, 1).ToString();
                        sItem["H_KLQD2"] = Round(Conversion.Val(sItem["H_KLQD2"]), 1).ToString();
                        string klqd = IsQualified("≥" + mItem["GV_KLQD2"], sItem["V_KLQD2"], true);
                        string klqd1 = IsQualified("≥" + mItem["GH_KLQD2"], sItem["V_KLQD2"], true);
                        if (klqd == "符合" && klqd1 == "符合")
                        {
                            mItem["HG_KLQD2"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_KLQD2"] = "不合格";
                            mFlag_Bhg = true;
                        }
                    }
                    string v_klqd = IsQualified("≥" + mItem["GV_KLQD"], sItem["V_KLQD"], true);
                    string v_klqd1 = IsQualified("≥" + mItem["GH_KLQD"], sItem["H_KLQD"], true);
                    if ((v_klqd == "符合" || string.IsNullOrEmpty(sItem["V_KLQD"])) && (v_klqd1 == "符合" || string.IsNullOrEmpty(sItem["H_KLQD"]))
                        && (string.IsNullOrEmpty(sItem["KLSYXX"]) || sItem["KLSYXX"].Trim() == "合格"))
                    {
                        mItem["HG_KLQD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_KLQD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmCur + "、";
                    }
                }

                if (jcxm.Contains("纵向拉伸强度"))
                {
                    jcxmCur = "纵向拉伸强度";
                    flag = true;
                    sign = true;
                    for (int i = 1; i <= 6; i++)
                    {
                        sign = IsNumeric(sItem["V_KLHZ" + i]) && !string.IsNullOrEmpty(sItem["V_KLHZ" + i]) ? sign : false;
                        sign = IsNumeric(sItem["V_KLKD" + i]) && !string.IsNullOrEmpty(sItem["V_KLKD" + i]) ? sign : false;
                        sign = IsNumeric(sItem["V_KLHD" + i]) && !string.IsNullOrEmpty(sItem["V_KLHD" + i]) ? sign : false;
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (int i = 1; i <= 6; i++)
                        {
                            md1 = Conversion.Val(sItem["V_KLKD" + i].Trim());
                            md2 = Conversion.Val(sItem["V_KLHD" + i].Trim());
                            md = Conversion.Val(sItem["V_KLHZ" + i].Trim());
                            md = md / (md1 * md2);
                            md = Round(md, 1);
                            sum = sum + md;
                        }
                        double pjmd = sum / 5;
                        pjmd = Round(pjmd, 1);
                        sItem["V_KLQD"] = pjmd.ToString();
                        mItem["GV_KLQD"] = "≥" + mItem["GV_KLQD"];
                        mItem["HG_KLQD"] = IsQualified(mItem["GV_KLQD"], sItem["V_KLQD"], false);
                        mbhggs = mItem["HG_KLQD"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (mItem["HG_KLQD"] == "合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }

                if (!flag)
                {
                    sItem["V_KLQD"] = "----";
                    sItem["H_KLQD"] = "----";
                    mItem["HG_KLQD"] = "----";
                    mItem["GV_KLQD"] = "----";
                    mItem["GH_KLQD"] = "----";
                }
                flag = false;

                if (jcxm.Contains("延伸率") || jcxm.Contains("伸长率") || jcxm.Contains("拉断伸长率") || jcxm.Contains("断裂伸长率") || jcxm.Contains("拉伸性能"))
                {
                    flag = true;
                    jcxmCur = FuncCurrentJcxm(jcxm, "延伸率,伸长率,拉断伸长率,断裂伸长率,拉伸性能");

                    double sYsbj1 = 0, sYsbj2 = 0, sYsbj3 = 0, sYsbj4 = 0, sYsbj5 = 0, sScl1 = 0, sScl2 = 0, sScl3 = 0, sScl4 = 0, sScl5 = 0;
                    sYsbj1 = Conversion.Val(sItem["V_SCL_L01"]);
                    sYsbj2 = Conversion.Val(sItem["V_SCL_L02"]);
                    sYsbj3 = Conversion.Val(sItem["V_SCL_L03"]);
                    sYsbj4 = Conversion.Val(sItem["V_SCL_L04"]);
                    sYsbj5 = Conversion.Val(sItem["V_SCL_L05"]);
                    if (sYsbj1 != 0) sScl1 = Round(100 * (Conversion.Val(sItem["V_SCL_LB1"]) - sYsbj1) / sYsbj1, 0);
                    if (sYsbj2 != 0) sScl2 = Round(100 * (Conversion.Val(sItem["V_SCL_LB2"]) - sYsbj2) / sYsbj2, 0);
                    if (sYsbj3 != 0) sScl3 = Round(100 * (Conversion.Val(sItem["V_SCL_LB3"]) - sYsbj3) / sYsbj3, 0);
                    if (sYsbj4 != 0) sScl4 = Round(100 * (Conversion.Val(sItem["V_SCL_LB4"]) - sYsbj4) / sYsbj4, 0);
                    if (sYsbj5 != 0) sScl5 = Round(100 * (Conversion.Val(sItem["V_SCL_LB5"]) - sYsbj5) / sYsbj5, 0);
                    string mlongStr = sScl1 + "," +
                               sScl2 + "," +
                               sScl3 + "," +
                               sScl4 + "," +
                               sScl5;
                    string[] mtmpArray = mlongStr.Split(',');
                    List<double> mfuncVal = new List<double>();
                    foreach (var mt in mtmpArray)
                    {
                        mfuncVal.Add(Conversion.Val(mt));
                    }
                    switch (klqdJsff)
                    {
                        case "2":
                        case "1":
                            sItem["V_SCL"] = mfuncVal[2].ToString();
                            break;
                        case "3":
                        case "4":
                        case "5":
                            sItem["V_SCL"] = Round(mfuncVal.Average(), 0).ToString();
                            break;
                    }
                    sYsbj1 = Conversion.Val(sItem["H_SCL_L01"]);
                    sYsbj2 = Conversion.Val(sItem["H_SCL_L02"]);
                    sYsbj3 = Conversion.Val(sItem["H_SCL_L03"]);
                    sYsbj4 = Conversion.Val(sItem["H_SCL_L04"]);
                    sYsbj5 = Conversion.Val(sItem["H_SCL_L05"]);
                    if (sYsbj1 != 0) sScl1 = Round(100 * (Conversion.Val(sItem["H_SCL_LB1"]) - sYsbj1) / sYsbj1, 0);
                    if (sYsbj2 != 0) sScl2 = Round(100 * (Conversion.Val(sItem["H_SCL_LB2"]) - sYsbj2) / sYsbj2, 0);
                    if (sYsbj3 != 0) sScl3 = Round(100 * (Conversion.Val(sItem["H_SCL_LB3"]) - sYsbj3) / sYsbj3, 0);
                    if (sYsbj4 != 0) sScl4 = Round(100 * (Conversion.Val(sItem["H_SCL_LB4"]) - sYsbj4) / sYsbj4, 0);
                    if (sYsbj5 != 0) sScl5 = Round(100 * (Conversion.Val(sItem["H_SCL_LB5"]) - sYsbj5) / sYsbj5, 0);
                    mlongStr = sScl1 + "," +
                               sScl2 + "," +
                               sScl3 + "," +
                               sScl4 + "," +
                               sScl5;
                    mtmpArray = mlongStr.Split(',');
                    List<double> mfuncVal1 = new List<double>();
                    foreach (var mt in mtmpArray)
                    {
                        mfuncVal1.Add(Conversion.Val(mt));
                    }
                    switch (klqdJsff)
                    {
                        case "2":
                        case "1":
                            sItem["H_SCL"] = mfuncVal1[2].ToString();
                            break;
                        case "3":
                        case "4":
                        case "5":
                            sItem["H_SCL"] = Round(mfuncVal1.Average(), 0).ToString();
                            break;
                    }
                    string v_klqd = IsQualified("≥" + mItem["GV_SCL"], sItem["V_SCL"], true);
                    string v_klqd1 = IsQualified("≥" + mItem["GH_SCL"], sItem["H_SCL"], true);
                    if ((v_klqd == "符合" || string.IsNullOrEmpty(sItem["V_SCL"])) && (v_klqd1 == "符合" || string.IsNullOrEmpty(sItem["H_SCL"])))
                    {
                        mItem["HG_SCL"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_SCL"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }

                if (jcxm.Contains("纵向断裂伸长率"))
                {
                    flag = true;
                    sign = true;
                    jcxmCur = "纵向断裂伸长率";

                    for (int i = 1; i <= 6; i++)
                    {
                        sign = IsNumeric(sItem["V_SCL_L0" + i]) && !string.IsNullOrEmpty(sItem["V_SCL_L0" + i]) ? sign : false;
                        sign = IsNumeric(sItem["V_SCL_LB" + i]) && !string.IsNullOrEmpty(sItem["V_SCL_LB" + i]) ? sign : false;
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (int i = 1; i <= 6; i++)
                        {
                            md1 = Conversion.Val(sItem["V_SCL_L0" + i].Trim());
                            md2 = Conversion.Val(sItem["V_SCL_LB" + i].Trim());
                            md = 100 * (md2 - md1) / md1;
                            md = Round(md, 0);
                            sum = sum + md;
                        }
                        double pjmd = sum / 5;
                        pjmd = Round(pjmd, 0);
                        sItem["V_SCL"] = pjmd.ToString();
                        sItem["H_SCL"] = "";
                        mItem["GV_SCL"] = "≥" + mItem["GV_SCL"];
                        mItem["HG_SCL"] = IsQualified(mItem["GV_SCL"], sItem["V_SCL"], false);
                        mbhggs = mItem["HG_SCL"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (mItem["HG_SCL"] == "合格")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                if (!flag)
                {
                    sItem["V_SCL"] = "----";
                    sItem["H_SCL"] = "----";
                    mItem["HG_SCL"] = "----";
                    mItem["GV_SCL"] = "----";
                    mItem["GH_SCL"] = "----";
                    throw new Exception("计算纵向断裂伸长率时异常，请检测数据。");
                }

                if (jcxm.Contains("、不透水性、"))
                {
                    jcxmCur = "不透水性";
                    if (sItem["BTSX"] == "合格")
                    {
                        mItem["HG_BTSX"] = "合格";
                        mFlag_Hg = true;
                    }
                    else /*if (sItem["BTSX"] == "不合格")*/
                    {
                        mItem["HG_BTSX"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                    #region MyRegion
                    //if (sItem["CPMC"] == "高分子防水卷材")
                    //{
                    //    if (sItem["BTSX"] == "合格")
                    //    {
                    //        sItem["BTSXSM"] = "无渗漏";
                    //    }
                    //    else
                    //    {
                    //        sItem["BTSXSM"] = "有渗漏";
                    //    }
                    //}
                    //else
                    //{
                    //    if (sItem["BTSX"] == "合格")
                    //    {
                    //        sItem["BTSXSM"] = "不透水";
                    //    }
                    //    else
                    //    {
                    //        sItem["BTSXSM"] = "透水";
                    //    }
                    //} 
                    #endregion
                }
                else
                {
                    sItem["BTSX"] = "----";
                    sItem["BTSXMS"] = "----";
                    mItem["HG_BTSX"] = "----";
                    mItem["G_BTSX"] = "----";
                }

                if (jcxm.Contains("、低温柔性、") || jcxm.Contains("、低温柔度、") || jcxm.Contains("、柔度、"))
                {
                    jcxmCur = FuncCurrentJcxm(jcxm, "低温柔性,低温柔度,柔度");

                    if (string.IsNullOrEmpty(sItem["SBM1"]) && string.IsNullOrEmpty(sItem["XBM1"]))
                    {
                        if (sItem["DWRD"] == "合格")
                        {
                            mItem["HG_DWRD"] = "合格";
                        }
                        else if (sItem["DWRD"] == "不合格")
                        {
                            mItem["HG_DWRD"] = "不合格";
                            mbhggs = mbhggs + 1;
                        }
                        else
                        {
                            mItem["HG_DWRD"] = "----";
                        }
                    }
                    else
                    {
                        if (sItem["DWRD"] == "合格")
                        {
                            sItem["DWRDSM"] = "上表面：" + sItem["SBM1"] + "，下表面：" + sItem["XBM1"];
                            mItem["HG_DWRD"] = "合格";
                        }
                        else if (sItem["DWRD"] == "不合格")
                        {
                            sItem["DWRDSM"] = "上表面：" + sItem["SBM1"] + "，下表面：" + sItem["XBM1"];
                            mItem["HG_DWRD"] = "不合格";
                            mbhggs = mbhggs + 1;
                        }
                        else
                        {
                            mItem["HG_DWRD"] = "----";
                        }
                    }

                    if (mItem["HG_DWRD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["DWRD"] = "----";
                    mItem["HG_DWRD"] = "----";
                    mItem["G_DWRD"] = "----";
                }

                if (jcxm.Contains("、低温柔性(热老化)、"))
                {
                    jcxmCur = "低温柔性(热老化)";

                    if (mItem["G_DWRD"] == "-25℃，无裂纹")
                    {
                        mItem["G_DWRDR"] = "-20℃，无裂纹";
                    }
                    else if (mItem["G_DWRD"] == "-20℃，无裂纹")
                    {
                        mItem["G_DWRDR"] = "-15℃，无裂纹";
                    }
                    if (string.IsNullOrEmpty(sItem["SBM2"]) && string.IsNullOrEmpty(sItem["XBM2"]))
                    {
                        if (sItem["DWRDR"] == "合格")
                        {
                            mItem["HG_DWRDR"] = "合格";
                        }
                        else if (sItem["DWRDR"] == "不合格")
                        {
                            mItem["HG_DWRDR"] = "不合格";
                            mbhggs = mbhggs + 1;
                        }
                        else
                        {
                            mItem["HG_DWRDR"] = "----";
                        }
                    }
                    else
                    {
                        if (sItem["DWRDR"] == "合格")
                        {
                            sItem["DWRDRSM"] = "上表面：" + sItem["SBM2"] + "，下表面：" + sItem["XBM2"];
                            mItem["HG_DWRDR"] = "合格";
                        }
                        else if (sItem["DWRDR"] == "不合格")
                        {
                            sItem["DWRDRSM"] = "上表面：" + sItem["SBM2"] + "，下表面：" + sItem["XBM2"];
                            mItem["HG_DWRDR"] = "不合格";
                            mbhggs = mbhggs + 1;
                        }
                        else
                        {
                            mItem["HG_DWRDR"] = "----";
                        }
                    }

                    if (mItem["HG_DWRDR"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["DWRDR"] = "----";
                    sItem["DWRDRSM"] = "----";
                    mItem["HG_DWRDR"] = "----";
                    mItem["G_DWRDR"] = "----";
                }

                if (jcxm.Contains("、低温弯折、") || jcxm.Contains("、低温弯折温度、") || jcxm.Contains("脆性温度"))
                {
                    jcxmCur = FuncCurrentJcxm(jcxm, "低温弯折,低温弯折温度,脆性温度");
                    if ("合格" == sItem["DWWZX"])
                    {
                        mItem["HG_DWWZX"] = "合格";
                    }
                    else if (sItem["DWWZX"] == "不合格")
                    {
                        mItem["HG_DWWZX"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mItem["HG_DWWZX"] = "----";
                    }

                    #region
                    //if (sItem["CPMC"] == "高分子防水卷材" || sItem["CPMC"] == "聚氯乙烯(PVC)防水卷材")
                    //{
                    //    if (sItem["DWWZX"] == "合格")
                    //    {
                    //        sItem["DWWZXSM"] = "无裂纹";
                    //        mItem["HG_DWWZX"] = "合格";
                    //    }
                    //    else if (sItem["DWWZX"] == "不合格" && sItem["DWWZXSM_1"] == "有裂纹" || sItem["DWWZXSM_2"] == "有裂纹" && sItem["DWWZXSM_3"] == "无裂纹" && sItem["DWWZXSM_4"] == "无裂纹")
                    //    {
                    //        sItem["DWWZXSM"] = "纵向有裂纹";
                    //        mItem["HG_DWWZX"] = "合格";
                    //    }
                    //    else if (sItem["DWWZX"] == "不合格" && sItem["DWWZXSM_3"] == "有裂纹" || sItem["DWWZXSM_4"] == "有裂纹" && sItem["DWWZXSM_1"] == "无裂纹" && sItem["DWWZXSM_2"] == "无裂纹")
                    //    {
                    //        sItem["DWWZXSM"] = "横向有裂纹";
                    //        mItem["HG_DWWZX"] = "合格";
                    //    }
                    //    else if (sItem["DWWZX"] == "不合格" && sItem["DWWZXSM_3"] == "有裂纹" || sItem["DWWZXSM_4"] == "有裂纹" && sItem["DWWZXSM_1"] == "有裂纹" || sItem["DWWZXSM_2"] == "有裂纹")
                    //    {
                    //        sItem["DWWZXSM"] = "横纵都向有裂纹";
                    //        mItem["HG_DWWZX"] = "合格";
                    //    }
                    //}
                    //else
                    //{
                    //    if (sItem["DWWZX"] == "合格")
                    //    {
                    //        mItem["HG_DWWZX"] = "合格";
                    //        sItem["DWWZXSM"] = "无裂纹";
                    //        mFlag_Hg = true;
                    //    }
                    //    else if (sItem["DWWZX"] == "不合格")
                    //    {
                    //        sItem["DWWZXSM"] = "有裂纹";
                    //        mItem["HG_DWWZX"] = "不合格";
                    //        mbhggs = mbhggs + 1;
                    //        mFlag_Bhg = true;
                    //    }
                    //    else
                    //    {
                    //        mItem["HG_DWWZX"] = "----";
                    //    }
                    //}
                    #endregion
                }
                else
                {
                    sItem["DWWZX"] = "----";
                    sItem["DWWZXSM"] = "----";
                    mItem["HG_DWWZX"] = "----";
                    mItem["G_DWWZX"] = "----";
                }

                if (jcxm.Contains("、耐热度、") || jcxm.Contains("、耐热性、"))
                {
                    jcxmCur = FuncCurrentJcxm(jcxm, "耐热度,耐热性");

                    if (sItem["NRD"] == "合格")
                    {
                        mItem["HG_NRD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else if (sItem["NRD"] == "不合格")
                    {
                        mItem["HG_NRD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mItem["HG_NRD"] = "----";
                    }

                    #region MyRegion
                    //if ((sItem["CPMC"] == "自粘聚合物改性沥青防水卷材" && sItem["XS"] == "PY") || sItem["CPMC"] == "预铺防水卷材(2017)")
                    //{
                    //    //if (sItem["NRDSM_3"] == "无滑动、流淌、滴落" && sItem["NRDSM_4"] == "无滑动、流淌、滴落" && sItem["NRDSM_5"] == "无滑动、流淌、滴落")
                    //    if (sItem["NRDSM"] == "无滑动、流淌、滴落")
                    //    {
                    //        sItem["NRD"] = "合格";
                    //        sItem["NRDSM"] = "无滑动、流淌、滴落";
                    //    }
                    //    else
                    //    {
                    //        sItem["NRD"] = "不合格";
                    //        sItem["NRDSM"] = "无滑动、流淌、滴落";
                    //    }
                    //}
                    //else if (sItem["CPMC"] == "自粘聚合物改性沥青防水卷材" && sItem["XS"] == "N")
                    //{
                    //    //if (double.Parse(sItem["NRDSM_3"]) <= 2 && double.Parse(sItem["NRDSM_4"]) <= 2 && double.Parse(sItem["NRDSM_5"]) <= 2)
                    //    if (double.Parse(sItem["NRDSM"]) <= 2)
                    //    {
                    //        sItem["NRD"] = "合格";
                    //    }
                    //    else
                    //    {
                    //        sItem["NRD"] = "不合格";
                    //    }
                    //}
                    //else if (sItem["CPMC"] == "胶粉改性沥青聚酯毡与玻纤网格布增强防水卷材")
                    //{
                    //    //if (sItem["NRDSM_3"] == "无滑动、流淌、滴落" && sItem["NRDSM_4"] == "无滑动、流淌、滴落" && sItem["NRDSM_5"] == "无滑动、流淌、滴落" && Conversion.Val(sItem["NRDSM_1"]) < 2 && Conversion.Val(sItem["NRDSM_2"]) < 2)
                    //    if (sItem["NRDSM"] == "无滑动、流淌、滴落")
                    //    {
                    //        sItem["NRD"] = "合格";
                    //        sItem["NRDSM"] = "无滑动、流淌、滴落";
                    //    }
                    //    else
                    //    {
                    //        sItem["NRD"] = "不合格";
                    //        sItem["NRDSM"] = "无滑动、流淌、滴落";
                    //    }
                    //}
                    //else if (sItem["CPMC"] == "湿铺防水卷材(2017)")
                    //{
                    //    //if (sItem["NRDSM_3"] == "无流淌、滴落" && sItem["NRDSM_4"] == "无流淌、滴落" && sItem["NRDSM_5"] == "无流淌、滴落" && Conversion.Val(sItem["NRDSM_1"]) < 2)
                    //    if (sItem["NRDSM"] == "无流淌、滴落")
                    //    {
                    //        sItem["NRD"] = "合格";
                    //        sItem["NRDSM"] = "无流淌、滴落";
                    //    }
                    //    else
                    //    {
                    //        sItem["NRD"] = "不合格";
                    //        sItem["NRDSM"] = "无流淌、滴落";
                    //    }
                    //}
                    //else if (sItem["CPMC"] == "玻纤胎沥青瓦")
                    //{
                    //    //if (sItem["NRDSM_3"] == "无滑动、流淌、滴落、气泡" && sItem["NRDSM_4"] == "无滑动、流淌、滴落、气泡" && sItem["NRDSM_5"] == "无滑动、流淌、滴落、气泡")
                    //    if (sItem["NRDSM"] == "无滑动、流淌、滴落、气泡")
                    //    {
                    //        sItem["NRD"] = "合格";
                    //        sItem["NRDSM"] = "无流淌、滑动、滴落、气泡";
                    //    }
                    //    else
                    //    {
                    //        sItem["NRD"] = "不合格";
                    //        sItem["NRDSM"] = "无滑动、流淌、滴落";
                    //    }
                    //}
                    //else
                    //{
                    //    //if ((sItem["NRDSM_3"] == "无流淌、滴落" || sItem["NRDSM_3"] == "无流淌、无起泡" || sItem["NRDSM_3"] == "无位移、流淌、滴落") && (sItem["NRDSM_4"] == "无流淌、滴落" || sItem["NRDSM_4"] == "无流淌、无起泡" || sItem["NRDSM_4"] == "无位移、流淌、滴落") && (sItem["NRDSM_5"] == "无流淌、滴落" || sItem["NRDSM_5"] == "无流淌、无起泡" || sItem["NRDSM_5"] == "无位移、流淌、滴落") && Conversion.Val(sItem["NRDSM_1"]) < 2 && Conversion.Val(sItem["NRDSM_2"]) < 2)
                    //    if (sItem["NRDSM"] == "无流淌、滴落" || sItem["NRDSM"] == "无流淌、无起泡" || sItem["NRDSM"] == "无位移、流淌、滴落")
                    //    {
                    //        sItem["NRD"] = "合格";
                    //        //if (sItem["NRDSM_3"] == "无流淌、滴落" && sItem["NRDSM_4"] == "无流淌、滴落" && sItem["NRDSM_5"] == "无流淌、滴落")
                    //        if (sItem["NRDSM"] == "无流淌、滴落")
                    //        {
                    //            sItem["NRDSM"] = "无流淌、滴落";
                    //        }
                    //        else
                    //        {
                    //            sItem["NRDSM"] = "有流淌、滴落";
                    //        }
                    //        //if (sItem["NRDSM_3"] == "无流淌、无起泡" && sItem["NRDSM_4"] == "无流淌、无起泡" && sItem["NRDSM_5"] == "无流淌、无起泡")
                    //        if (sItem["NRDSM"] == "无流淌、无起泡")
                    //        {
                    //            sItem["NRDSM"] = "无流淌、无起泡";
                    //        }
                    //        //if(sItem["NRDSM_3"] == "无位移、流淌、滴落" && sItem["NRDSM_4"] == "无位移、流淌、滴落" && sItem["NRDSM_5"] == "无位移、流淌、滴落")
                    //        if (sItem["NRDSM"] == "无位移、流淌、滴落")
                    //        {
                    //            sItem["NRDSM"] = "无位移、流淌、滴落";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        sItem["NRD"] = "不合格";
                    //        //if (sItem["NRDSM_3"] == "无流淌、滴落" && sItem["NRDSM_4"] == "无流淌、滴落" && sItem["NRDSM_5"] == "无流淌、滴落")
                    //        if (sItem["NRDSM"] == "无流淌、滴落")
                    //        {
                    //            sItem["NRDSM"] = "无流淌、滴落";
                    //        }
                    //        else
                    //        {
                    //            sItem["NRDSM"] = "有流淌、滴落";
                    //        }
                    //        //if (sItem["NRDSM_3"] == "无流淌、无起泡" && sItem["NRDSM_4"] == "无流淌、无起泡" && sItem["NRDSM_5"] == "无流淌、无起泡")
                    //        if (sItem["NRDSM"] == "无流淌、无起泡")
                    //        {
                    //            sItem["NRDSM"] = "无流淌、无起泡";
                    //        }
                    //        //if (sItem["NRDSM_3"] == "无位移、流淌、滴落" && sItem["NRDSM_4"] == "无位移、流淌、滴落" && sItem["NRDSM_5"] == "无位移、流淌、滴落")
                    //        if (sItem["NRDSM"] == "无位移、流淌、滴落")
                    //        {
                    //            sItem["NRDSM"] = "无位移、流淌、滴落";
                    //        }
                    //    }
                    //} 
                    #endregion
                }
                else
                {
                    sItem["NRD"] = "----";
                    sItem["NRDSM"] = "----";
                    mItem["HG_NRD"] = "----";
                    mItem["G_NRD"] = "----";
                }

                if (jcxm.Contains("、渗油性、"))
                {
                    jcxmCur = "渗油性";

                    mItem["HG_SYX"] = IsQualified(mItem["G_SYX"], sItem["SYX"], false);
                    mbhggs = mItem["HG_SYX"] == "不合格" ? mbhggs++ : mbhggs;
                    if (mItem["HG_SYX"] == "合格")
                    {
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["SYX"] = "----";
                    mItem["HG_SYX"] = "----";
                    mItem["G_SYX"] = "----";
                }

                if (jcxm.Contains("、撕裂强度、") || jcxm.Contains("、撕裂力、") || jcxm.Contains("、钉杆撕裂强度、") || jcxm.Contains("、梯形撕裂强度、") || jcxm.Contains("、直角(梯形)撕裂强度、"))
                {
                    jcxmCur = FuncCurrentJcxm(jcxm, "撕裂强度,撕裂力,钉杆撕裂强度,梯形撕裂强度,直角(梯形)撕裂强度");

                    if (Conversion.Val(sItem["V_SLQD"]) >= Conversion.Val(mItem["GV_SLQD"]) && Conversion.Val(sItem["H_SLQD"]) >= Conversion.Val(mItem["GH_SLQD"]))
                    {
                        mItem["HG_SLQD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_SLQD"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["V_SLQD"] = "0";
                    sItem["H_SLQD"] = "0";
                    mItem["HG_SLQD"] = "----";
                    mItem["GV_SLQD"] = "0";
                    mItem["GH_SLQD"] = "0";
                }

                if (jcxm.Contains("、可溶物含量、"))
                {
                    jcxmCur = "可溶物含量";
                    if (Conversion.Val(sItem["KRWHL"]) >= Conversion.Val(mItem["G_KRWHL"]))
                    {
                        mItem["HG_KRWHL"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_KRWHL"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["KRWHL"] = "0";
                    mItem["HG_KRWHL"] = "----";
                    mItem["G_KRWHL"] = "0";
                }

                if (jcxm.Contains("、抗穿孔性、"))
                {
                    jcxmCur = "抗穿孔性";
                    if (sItem["KCKX"] == "合格")
                    {
                        mItem["HG_KCKX"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_KCKX"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["KCKX"] = "----";
                    mItem["HG_KCKX"] = "----";
                    mItem["G_KCKX"] = "----";
                }
                if (jcxm.Contains("、尺寸变化率、"))
                {
                    jcxmCur = "尺寸变化率";
                    double ccbh = !string.IsNullOrEmpty(sItem["V_CCBHL"]) ? 0 : Conversion.Val(sItem["V_CCBHL"]);
                    double ccbh1 = !string.IsNullOrEmpty(sItem["GV_CCBHL"]) ? 0 : Conversion.Val(sItem["GV_CCBHL"]);
                    double ccbh2 = !string.IsNullOrEmpty(sItem["GV_CCBHL"]) ? 0 : Conversion.Val(sItem["GV_CCBHL"]);
                    if (ccbh <= ccbh1 && ccbh <= ccbh2)
                    {
                        mItem["HG_CCBHL"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_CCBHL"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["V_CCBHL"] = "0";
                    sItem["H_CCBHL"] = "0";
                    mItem["HG_CCBHL"] = "----";
                    mItem["GV_CCBHL"] = "0";
                    mItem["GH_CCBHL"] = "0";
                }

                if (jcxm.Contains("、剪切粘合性、"))
                {
                    jcxmCur = "剪切粘合性";
                    if (sItem["JQLHX"] == "合格")
                    {
                        mItem["HG_JQLHX"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_JQLHX"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["JQLHX"] = "----";
                    mItem["HG_JQLHX"] = "----";
                    mItem["G_LHX"] = "----";
                }

                if (jcxm.Contains("、水蒸气透湿率、"))
                {
                    jcxmCur = "水蒸气透湿率";

                    if (Conversion.Val(sItem["SZQTSL"]) >= Conversion.Val(mItem["G_SZQTSL"]))
                    {
                        mItem["HG_SZQTSL"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_SZQTSL"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["SZQTSL"] = "0";
                    mItem["HG_SZQTSL"] = "----";
                    mItem["G_SZQTSL"] = "0";
                }

                if (jcxm.Contains("、剥离性能、"))
                {
                    jcxmCur = "剥离性能";

                    if (sItem["BLX"] == "合格")
                    {
                        mItem["HG_BLX"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mItem["HG_BLX"] = "不合格";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["BLX"] = "0";
                    mItem["HG_BLX"] = "----";
                    mItem["G_BLX"] = "0";
                }

                sign = true;
                if (jcxm.Contains("、膜断裂伸长率、") || jcxm.Contains("、拉伸性能、"))
                {
                    jcxmCur = FuncCurrentJcxm(jcxm, "膜断裂伸长率,拉伸性能");
                    for (int i = 1; i <= 5; i++)
                    {
                        sign = IsNumeric(sItem["HX_MD_LO" + i]) ? sign : false;
                        sign = IsNumeric(sItem["HX_MD_LB" + i]) ? sign : false;
                        sign = IsNumeric(sItem["ZX_MD_LO" + i]) ? sign : false;
                        sign = IsNumeric(sItem["ZX_MD_LB" + i]) ? sign : false;
                        if (!sign)
                        {
                            throw new Exception("膜断裂伸长率" + i + "数据录入异常，请检测。");
                        }
                    }

                    sum = 0;
                    for (int i = 1; i <= 5; i++)
                    {
                        md1 = Conversion.Val(sItem["HX_MD_LO" + i]);
                        md2 = Conversion.Val(sItem["HX_MD_LB" + i]);
                        md = 100 * (md2 - md1) / md1;
                        md = Round(md, 0);
                        sum = sum + md;
                    }
                    md = sum / 5;
                    md = Round(md, 0);
                    sItem["HX_MD"] = md.ToString();
                    sum = 0;
                    for (int i = 1; i <= 5; i++)
                    {
                        md1 = Conversion.Val(sItem["ZX_MD_LO" + i]);
                        md2 = Conversion.Val(sItem["ZX_MD_LB" + i]);
                        md = 100 * (md2 - md1) / md1;
                        md = Round(md, 0);
                        sum = sum + md;
                    }
                    md = sum / 5;
                    md = Round(md, 0);
                    sItem["ZX_MD"] = md.ToString();
                    mItem["GH_MD"] = "----";
                    mItem["GZ_MD"] = "----";
                    switch (dCpmc)
                    {
                        case "预铺防水卷材":
                            if (sItem["XS"] == "P")
                            {
                                mItem["GH_MD"] = "≥400";
                                mItem["GZ_MD"] = "≥400";
                            }
                            break;
                        case "预铺防水卷材(2017)":
                            if (sItem["XS"] == "P")
                            {
                                mItem["GH_MD"] = "≥400";
                                mItem["GZ_MD"] = "≥400";
                            }
                            else if (sItem["XS"] == "R")
                            {
                                mItem["GH_MD"] = "≥300";
                                mItem["GZ_MD"] = "≥300";
                            }
                            break;
                    }
                    mark = true;
                    mark = IsQualified(mItem["GH_MD"], sItem["HX_MD"], false) == "不合格" ? false : mark;
                    mark = IsQualified(mItem["GZ_MD"], sItem["ZX_MD"], false) == "不合格" ? false : mark;
                    if (!mark)
                    {
                        mItem["HG_MD"] = "不合格";
                    }
                    else
                    {
                        mItem["HG_MD"] = "合格";
                    }
                    string hgmd = IsQualified(mItem["GH_MD"], sItem["HX_MD"], false);
                    string zgmd = IsQualified(mItem["GZ_MD"], sItem["ZX_MD"], false);
                    if (hgmd == "----" && zgmd == "----")
                    {
                        mItem["GH_MD"] = "----";
                    }
                    mbhggs = mItem["HG_MD"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (mItem["HG_MD"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sign = false;
                }
                if (!sign)
                {
                    mItem["HG_MD"] = "----";
                    mItem["GH_MD"] = "----";
                    mItem["GZ_MD"] = "----";
                    sItem["HX_MD"] = "----";
                    sItem["ZX_MD"] = "----";
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
            //综合判断
            if (mbhggs == 0)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            if (mbhggs > 0)
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //if (mFlag_Bhg && mFlag_Hg)
                //    MItem[0]["JCJGMS"] = "依据标准" + MItem[0]["PDBZ"] + ",所检项目" + jcxmBhg.TrimEnd('、') + "不符合标准要求。";
            }
            #endregion
            #endregion
        }
    }
}
