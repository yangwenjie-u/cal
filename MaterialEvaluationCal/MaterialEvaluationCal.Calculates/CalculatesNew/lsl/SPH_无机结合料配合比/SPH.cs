using System.Collections.Generic;

namespace Calculates
{
    public class SPH : BaseMethods
    {
        public void Calc()
        {
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_SPH"];
            var EJL_SH = data["EJL_SH"];
            var E_JLPB = data["E_JLPB"];
            var MItem = data["M_SPH"];
            if (!data.ContainsKey("M_SPH"))
            {
                data["M_SPH"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null )
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];

            foreach (var sItem in SItem)
            {
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                bool flag = true;
                int gs = 1;
                string stemp;
                string[] sArray = new string[8];

                jsbeizhu = "";
                sItem["WCXSJQD"] = sItem["WCXSJQD"].Replace("MPa", "");

                for (int i = 1; i <= 5; i++)
                {
                    if (IsNumeric(sItem["RD" + i]) && !string.IsNullOrEmpty(sItem["RD" + i]))
                    {
                        sItem["WPD" + i] = IsQualified("≥" + sItem["WCXSJQD"], sItem["RD" + i], true);
                    }
                }

                for (int i = 1; i <= 4; i++)
                {
                    if (IsNumeric(sItem["W_YSZ" + i]) && !string.IsNullOrEmpty(sItem["W_YSZ" + i]))
                    {
                        sItem["JLMCBH" + gs] = sItem["JLMC" + i] + sItem["JLBH" + i];
                        sItem["JLJCXM" + gs] = "压碎值";
                        sItem["JLDW" + gs] = "%";
                        sItem["PD_YSZ" + gs] = IsQualified(sItem["G_YSZ" + i], sItem["W_YSZ" + i], true);
                        sItem["JLG_YSZ" + gs] = sItem["G_YSZ" + i];
                        sItem["JLW_YSZ" + gs] = sItem["W_YSZ" + i];
                        gs++;
                    }
                }

                for (int j = gs; j <= 4; j++)
                {
                    sItem["JLMCBH" + j] = "";
                    sItem["JLJCXM" + j] = "";
                    sItem["JLDW" + j] = "";
                    sItem["PD_YSZ" + j] = "";
                    sItem["JLG_YSZ" + j] = "";
                    sItem["JLW_YSZ" + j] = "";
                }

                stemp = "";

                if (sItem["SJLX"].Contains("三渣"))
                {
                    stemp = "石灰(" + sItem["SHBH"] + "):";
                    stemp = stemp + sItem["SHCD"] + " ";
                    stemp = stemp + sItem["SHGG"] + ";\r\n";

                    stemp = stemp + "粉煤灰(" + sItem["FMBH"] + "):";
                    stemp = stemp + sItem["FMCD"] + " ";
                    stemp = stemp + sItem["FMGG"] + ";\r\n";
                }
                else
                {
                    stemp = "水泥(" + sItem["SNBH"] + "):";
                    stemp = stemp + sItem["SNCD"] + " ";
                    stemp = stemp + sItem["SNGG"] + ";\r\n";
                }

                for (int i = 1; i <= 4; i++)
                {
                    sItem["JLMC" + i] = sItem["JLMC" + i].Trim();
                    if (!string.IsNullOrEmpty(sItem["JLMC" + i]) && sItem["JLMC" + i] != "----" && sItem["JLMC" + i] != "")
                    {
                        stemp = stemp + sItem["JLMC" + i] + "(" + sItem["JLBH" + i] + "):";
                        stemp = stemp + sItem["JLCD" + i] + " ";
                        stemp = stemp + sItem["JLGG" + i] + ";\r\n";
                    }
                }

                stemp = stemp.Substring(0, stemp.Length - 2) + "。";
                sItem["ZBH"] = stemp;

                if (sItem["SJLX"].Contains("三渣"))
                {
                    gs = EJL_SH.Count;
                    stemp = "----";
                    for (int i = 1; i <= gs; i++)
                    {
                        switch (EJL_SH[i]["shlx"])
                        {
                            case "钙质生石灰":
                                stemp = "≥70";
                                break;
                            case "镁质生石灰":
                                stemp = "≥65";
                                break;
                            case "钙质消石灰":
                                stemp = "≥55";
                                break;
                            case "镁质消石灰":
                                stemp = "≥50";
                                break;
                        }
                    }
                    sItem["G_SH"] = stemp;
                    sItem["PD_SH"] = IsQualified(sItem["G_SH"], sItem["W_SH"], true);
                    sItem["PD_SH"] = sItem["PD_SH"] == "符合" ? "可以用于三渣级配" : sItem["PD_SH"];
                    sItem["PD_SH"] = sItem["PD_SH"] == "不符合" ? "不可以用于三渣级配" : sItem["PD_SH"];

                    sItem["G_SSL"] = "≤10";
                    flag = true;
                    stemp = IsQualified(sItem["G_SSL"], sItem["W_SSL"], true);
                    flag = stemp == "符合" ? flag : false;

                    sItem["G_XD1"] = "70～100";
                    stemp = IsQualified(sItem["G_XD1"], sItem["W_XD1"], true);
                    flag = stemp == "符合" ? flag : false;

                    sItem["G_XD2"] = "90～100";
                    stemp = IsQualified(sItem["G_XD2"], sItem["W_XD2"], true);
                    flag = stemp == "符合" ? flag : false;

                    sItem["PD_FM"] = flag ? "可以用于三渣级配" : "不可以用于三渣级配";
                }

                stemp = "";
                for (int i = 1; i <= 4; i++)
                {
                    if (!string.IsNullOrEmpty(sItem["JLMC" + i]) && sItem["JLMC" + i] != "----")
                    {
                        sArray[0] = sArray[0] + sItem["JLMC" + i] + ":";
                    }
                }

                sArray[0] = sArray[0].Substring(0, sArray[0].Length - 1);
                gs = E_JLPB.Count;
                stemp = "----";
                for (int i = 1; i <= gs; i++)
                {
                    if (i == 1) sArray[1] = E_JLPB[i]["zlphb"];
                }
                if (sItem["SJLX"].Contains("三渣"))
                {
                    sArray[2] = "推荐二灰稳定碎石的碎石级配组成为" + sArray[0] + "(质量比)=" + sArray[1] + "。\r\n";
                }
                else
                {
                    sArray[2] = "推荐水泥稳定碎石的碎石级配组成为" + sArray[0] + "(质量比)=" + sArray[1] + "。\r\n";
                }
                sItem["XZPB"] = sItem["XZPB"].Trim();
                sItem["XZPB"] = sItem["XZPB"] == "" ? "1" : sItem["XZPB"];
                sArray[3] = sItem["ZLB" + sItem["XZPB"]].Trim();
                sArray[4] = sItem["HSL" + sItem["XZPB"]].Trim();
                sArray[5] = sItem["GMD" + sItem["XZPB"]].Trim();

                stemp = "";
                //mItem["which"] = "bgsph";
                if (jcxm.Contains("、集料配合比、"))
                {
                    //mItem["WHICH"] = mItem["WHICH"] + "、bgsph_1";
                }

                if (sItem["SJLX"].Contains("三渣") && sItem["SFYZ"] == "否")
                {
                    //if (jcxm.Contains("、集料压碎值、") || jcxm.Contains("、石灰钙镁含量、") || jcxm.Contains("、粉煤灰烧失量、") || jcxm.Contains("、粉煤灰细度、"))
                    //{
                    //mItem["WHICH"] = mItem["WHICH"] + "、bgsph_2";
                    //}
                    //mItem["WHICH"] = mItem["WHICH"] + "、bgsph_3";
                    for (int i = 1; i <= 5; i++)
                    {
                        if (sItem["WPD" + i] == "符合")
                        {
                            stemp = stemp + sItem["ZLB" + i].Trim() + "或";
                        }
                    }
                    stemp = stemp.Substring(0, stemp.Length - 1) + ",";
                    stemp = "经试验,石灰：粉煤灰：集料=" + stemp;
                    stemp = stemp + "由所送试样按JTG/T F20-2015要求配制的二灰稳定碎石无侧限平均抗压强度R≥Rd/(1-Za×Cv),均能满足设计要求。" + "\r\n";
                    sArray[6] = stemp + "综合考虑：推荐二灰稳定碎石最佳配合比为:" + "石灰：粉煤灰：集料=" + sArray[3] + "," + "\r\n" +
                        "其混合料的最佳含水量为" + sArray[4] + "%" + "," + "最大干密度为" + sArray[5] + "克每立方厘米。" + "\r\n";
                }

                if (sItem["SJLX"].Contains("三渣") && sItem["SFYZ"] == "是")
                {
                    //if (jcxm.Contains("、集料压碎值、") || jcxm.Contains("、石灰钙镁含量、") || jcxm.Contains("、粉煤灰烧失量、") || jcxm.Contains("、粉煤灰细度、"))
                    //{
                    //    mItem["WHICH"] = mItem["WHICH"] + "、bgsph_2";
                    //}
                    //mItem["WHICH"] = mItem["WHICH"] + "、bgsph_5";
                    stemp = sItem["ZLB1"] + ",";
                    stemp = "经试验,石灰：粉煤灰：集料=" + stemp;
                    if (sItem["WPD1"] == "符合")
                    {
                        stemp = stemp + "由所送试样按JTG/T F20-2015要求配制的三渣混合料无侧限平均抗压强度R≥Rd/(1-Za×Cv),能满足设计要求。";
                    }
                    else
                    {
                        stemp = stemp + "由所送试样按JTG/T F20-2015要求配制的三渣混合料无侧限平均抗压强度R≥Rd/(1-Za×Cv),不能满足设计要求。";
                    }
                    sArray[6] = stemp + "\r\n" + "其混合料的最佳含水量为" + sArray[4] + "%" + "," + "最大干密度为" + sArray[5] + "克每立方厘米。" + "\r\n";
                }

                if (sItem["SJLX"].Contains("水稳") && sItem["SFYZ"] == "否")
                {
                    //if (jcxm.Contains("、集料压碎值、") || jcxm.Contains("、石灰钙镁含量、") || jcxm.Contains("、粉煤灰烧失量、") || jcxm.Contains("、粉煤灰细度、"))
                    //{
                    //    mItem["WHICH"] = mItem["WHICH"] + "、bgsph_6";
                    //}
                    //mItem["WHICH"] = mItem["WHICH"] + "、bgsph_4";
                    for (int i = 1; i <= 5; i++)
                    {
                        if (sItem["WPD" + i] == "符合")
                        {
                            stemp = "≥" + sItem["ZLB" + i] + "%";
                            break;
                        }
                    }
                    stemp = "经试验，当水泥剂量" + stemp;
                    stemp = stemp + "时，由所送试样按JTG/T F20-2015要求配制的水泥稳定碎石无侧限平均抗压强度R*(1-Za×Cv)≥Rd,均能满足设计要求。" + "\r\n";
                    sArray[6] = stemp + "综合考虑：推荐符合设计要求的水泥稳定碎石最佳水泥剂量为" + sArray[3] + "%" +
                               "," + "\r\n" + "其混合料的最佳含水量为" + sArray[4] + "%" + "," + "最大干密度为" + sArray[5] + "克每立方厘米。" + "\r\n";
                }

                if (sItem["SJLX"].Contains("水稳") && sItem["SFYZ"] == "是")
                {
                    //if (jcxm.Contains("、集料压碎值、") || jcxm.Contains("、石灰钙镁含量、") || jcxm.Contains("、粉煤灰烧失量、") || jcxm.Contains("、粉煤灰细度、"))
                    //{
                    //    mItem["WHICH"] = mItem["WHICH"] + "、bgsph_6";
                    //}
                    //mItem["WHICH"] = mItem["WHICH"] + "、bgsph_5";
                    stemp = sItem["ZLB1"] + "%";
                    stemp = "经试验，当水泥剂量" + stemp;
                    if (sItem["WPD1"] == "符合")
                    {
                        stemp = stemp + "时，由所送试样按JTG/T F20-2015要求配制的水泥稳定碎石无侧限平均抗压强度R*(1-Za×Cv)≥Rd,能满足设计要求。";
                    }
                    else
                    {
                        stemp = stemp + "由所送试样按JTG/T F20-2015要求配制的三渣混合料无侧限平均抗压强度R*(1-Za×Cv)≥Rd,不能满足设计要求。";
                    }
                    sArray[6] = stemp + "\r\n" + "其混合料的最佳含水量为" + sArray[4] + "%" + "," + "最大干密度为" + sArray[5] + "克每立方厘米。" + "\r\n";
                }

                int iPages = 0;
                jsbeizhu = "";
                if (jcxm.Contains("、集料配合比、"))
                {
                    jsbeizhu = sArray[2];
                    ++iPages;
                }
                if (jcxm.Contains("、集料压碎值、") || jcxm.Contains("、石灰钙镁含量、") || jcxm.Contains("、粉煤灰烧失量、") || jcxm.Contains("、粉煤灰细度、"))
                {
                    iPages++;
                }
                if (jcxm.Contains("、击实、") || jcxm.Contains("、无侧限抗压强度、"))
                {
                    jsbeizhu = sArray[6];
                    iPages++;
                }
                if (iPages == 1) sArray[7] = "检测结果详见报告第2页。";
                if (iPages == 2) sArray[7] = "检测结果详见报告第2～3页。";
                if (iPages == 3) sArray[7] = "检测结果详见报告第2～4页。";
                jsbeizhu = jsbeizhu + sArray[7];
                
                sItem["JCJG"] = "合格";
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGSM"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
