using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class NCL : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_NCL_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_NCLS = data["S_NCL"];
            if (!data.ContainsKey("M_NCL"))
            {
                data["M_NCL"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_NCL"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            List<double> mkyqdArray = new List<double>();
            List<string> mtmpArray = new List<string>();
            bool sign, flag = true;
            string mlongStr = "";
            double mMaxKyqd, mMinKyqd, mAvgKyqd, mMidKyqd = 0;
            int mbHggs = 0;//检测项目合格数量

            foreach (var sItem in S_NCLS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["XH"].Trim() == sItem["XH"]);
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "不合格";
                    continue;
                }
                else
                {
                    sItem["G_ZDJLLJ"] = extraFieldsDj["ZDJLLJ"];
                    sItem["G_LDDCSZ"] = extraFieldsDj["LDDCSZ"];
                    sItem["G_LDDBLZ"] = extraFieldsDj["LDDBLZ"];
                    sItem["G_TLDCSZ"] = extraFieldsDj["TLDCSZ"];
                    sItem["G_TLDBLZ"] = extraFieldsDj["TLDBLZ"];
                    sItem["G_TLKZDC"] = extraFieldsDj["TLKZDC"];
                    sItem["G_TLKZDB"] = extraFieldsDj["TLKZDB"];
                    sItem["G_KYQD1"] = extraFieldsDj["KYQD1"];
                    sItem["G_KYQD3"] = extraFieldsDj["KYQD3"];
                    sItem["G_KYQD28"] = extraFieldsDj["KYQD28"];
                    sItem["G_PZL3"] = extraFieldsDj["PZL3"];
                    sItem["G_PZL24"] = extraFieldsDj["PZL24"];
                    sItem["G_MSL"] = extraFieldsDj["MSL"];
                    sItem["G_SJB"] = extraFieldsDj["SJB"];
                    sItem["G_CNSJ"] = extraFieldsDj["CNSJ"];
                    sItem["G_ZNSJ"] = extraFieldsDj["ZNSJ"];
                    sItem["G_LDD60"] = extraFieldsDj["LDD60"];
                    sItem["G_MSL24"] = extraFieldsDj["MSL24"];
                    sItem["G_MSL3"] = extraFieldsDj["MSL3"];
                    sItem["G_YLMSL"] = extraFieldsDj["YLMSL"];
                    sItem["G_KYQD7"] = extraFieldsDj["KYQD7"];
                    sItem["G_KZQD3"] = extraFieldsDj["KZQD3"];
                    sItem["G_KZQD7"] = extraFieldsDj["KZQD7"];
                    sItem["G_KZQD28"] = extraFieldsDj["KZQD28"];
                }

                #region 满足条件进入vb跳转代码
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    if ("后张预应力孔道压浆浆液" == sItem["CPMC"])
                    {
                        #region 水胶比
                        if (jcxm.Contains("、水胶比、"))
                        {
                            sItem["GH_SJB"] = IsQualified(sItem["G_SJB"], sItem["SJB"], false);
                            if ("不合格" == sItem["GH_SJB"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_SJB"] = "----";
                            sItem["GH_SJB"] = "----";
                            sItem["SJB"] = "----";
                        }
                        #endregion

                        #region 凝结时间
                        if (jcxm.Contains("、凝结时间、"))
                        {
                            sign = true;
                            sign = IsQualified(sItem["G_CNSJ"], sItem["CNSJ"], true) == "不符合" ? false : sign;
                            sign = IsQualified(sItem["G_ZNSJ"], sItem["ZNSJ"], true) == "不符合" ? false : sign;
                            sItem["GH_NJSJ"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["GH_NJSJ"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_CNSJ"] = "----";
                            sItem["CNSJ"] = "----";
                            sItem["G_ZNSJ"] = "----";
                            sItem["ZNSJ"] = "----";
                            sItem["GH_NJSJ"] = "----";
                        }
                        #endregion

                        #region 流动度
                        if (jcxm.Contains("、流动度、"))
                        {
                            sign = true;
                            sign = IsQualified(sItem["G_LDDCSZ"], sItem["LDDCSZ"], true) == "不符合" ? false : sign;
                            sign = IsQualified(sItem["G_LDDBLZ"], sItem["LDDBLZ"], true) == "不符合" ? false : sign;
                            sign = IsQualified(sItem["G_LDD60"], sItem["LDD60"], true) == "不符合" ? false : sign;
                            sItem["HG_LDD"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_LDD"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["HG_LDD"] = "----";
                            sItem["G_LDDCSZ"] = "----";
                            sItem["G_LDDBLZ"] = "----";
                            sItem["G_LDD60"] = "----";
                            sItem["LDDCSZ"] = "----";
                            sItem["LDDBLZ"] = "----";
                            sItem["LDD60"] = "----";
                        }
                        #endregion

                        #region 压力泌水率
                        if (jcxm.Contains("、压力泌水率、"))
                        {
                            sItem["GH_YLMSL"] = IsQualified(sItem["G_YLMSL"], sItem["YLMSL"], false);
                            if ("不合格" == sItem["GH_YLMSL"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_YLMSL"] = "----";
                            sItem["GH_YLMSL"] = "----";
                            sItem["YLMSL"] = "----";
                        }
                        #endregion

                        #region 泌水率
                        if (jcxm.Contains("、泌水率、"))
                        {
                            flag = true;
                            sign = true;
                            sign = IsQualified(sItem["G_MSL3"], sItem["MSL3"], true) == "不符合" ? false : sign;
                            sign = IsQualified(sItem["G_MSL24"], sItem["MSL24"], true) == "不符合" ? false : sign;
                            sItem["HG_MSL"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_MSL"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_MSL3"] = "----";
                            sItem["G_MSL24"] = "----";
                            sItem["HG_MSL"] = "----";
                            sItem["MSL3"] = "----";
                            sItem["MSL24"] = "----";
                        }
                        #endregion

                        #region 自由膨胀率
                        if (jcxm.Contains("、自由膨胀率、"))
                        {
                            sign = true;
                            sign = IsQualified(sItem["G_PZL3"], sItem["PZL3"], true) == "不符合" ? false : sign;
                            sign = IsQualified(sItem["G_PZL24"], sItem["PZL24"], true) == "不符合" ? false : sign;
                            sItem["HG_PZL"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_PZL"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_PZL3"] = "----";
                            sItem["G_PZL24"] = "----";
                            sItem["HG_PZL"] = "----";
                            sItem["PZL3"] = "----";
                            sItem["PZL24"] = "----";
                        }
                        #endregion

                        #region 3天抗压强度
                        if (jcxm.Contains("、3天抗压强度、"))
                        {
                            sItem["HG_KYQD3"] = IsQualified(sItem["G_KYQD3"], sItem["KYPJ_3"], false);
                            if ("不合格" == sItem["HG_KYQD3"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_KYQD3"] = "----";
                            sItem["HG_KYQD3"] = "----";
                            sItem["KYPJ_3"] = "----";
                        }
                        #endregion

                        #region 7天抗压强度
                        if (jcxm.Contains("、7天抗压强度、"))
                        {
                            sItem["HG_KYQD7"] = IsQualified(sItem["G_KYQD7"], sItem["KYPJ_7"], false);
                            if ("不合格" == sItem["HG_KYQD7"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["G_KYQD7"] = "----";
                            sItem["HG_KYQD7"] = "----";
                            sItem["KYPJ_7"] = "----";
                        }
                        #endregion

                        #region 28天抗压强度
                        if (jcxm.Contains("、28天抗压强度、"))
                        {
                            sItem["HG_KYQD28"] = IsQualified(sItem["G_KYQD28"], sItem["KYPJ"], false);
                            if ("不合格" == sItem["HG_KYQD28"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_KYQD28"] = "----";
                            sItem["KYPJ"] = "----";
                            sItem["HG_KYQD28"] = "----";
                        }
                        #endregion


                        #region 3天抗折强度
                        if (jcxm.Contains("、3天抗折强度、"))
                        {
                            sItem["GH_KZQD3"] = IsQualified(sItem["G_KZQD3"], sItem["KZQD3"], false);
                            if ("不合格" == sItem["GH_KZQD3"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_KZQD3"] = "----";
                            sItem["GH_KZQD3"] = "----";
                            sItem["KZQD3"] = "----";
                        }
                        #endregion

                        #region 7天抗折强度
                        if (jcxm.Contains("、7天抗折强度、"))
                        {
                            sItem["GH_KZQD7"] = IsQualified(sItem["G_KZQD7"], sItem["KZQD7"], false);
                            if ("不合格" == sItem["GH_KZQD7"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_KZQD7"] = "----";
                            sItem["GH_KZQD7"] = "----";
                            sItem["KZQD7"] = "----";
                        }
                        #endregion

                        #region 28天抗折强度
                        if (jcxm.Contains("、28天抗折强度、"))
                        {
                            sItem["GH_KZQD28"] = IsQualified(sItem["G_KZQD28"], sItem["KZQD28"], false);
                            if ("不合格" == sItem["GH_KZQD28"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_KZQD28"] = "----";
                            sItem["GH_KZQD28"] = "----";
                            sItem["KZQD28"] = "----";
                        }
                        #endregion


                        #region 对钢筋锈蚀作用
                        if (jcxm.Contains("、对钢筋锈蚀作用、"))
                        {
                            sItem["G_XSZY"] = "无";
                            sItem["HG_XSZY"] = "无" == sItem["XSZY"] ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_XSZY"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_XSZY"] = "----";
                            sItem["XSZY"] = "----";
                            sItem["HG_XSZY"] = "----";
                        }
                        #endregion

                        #region 充盈度
                        if (jcxm.Contains("、充盈度、"))
                        {

                            sItem["GH_CYD"] = "合格" == sItem["CYD"] ? "合格" : "不合格";
                            //mbhggs = IIf(.Fields("HG_XSZY") = "不合格", mbhggs + 1, mbhggs)
                            //If.Fields("HG_XSZY") = "合格" Then
                            if ("不合格" == sItem["GH_CYD"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_CYD"] = "----";
                            sItem["CYD"] = "----";
                            sItem["GH_CYD"] = "----";
                        }
                        #endregion
                    }
                    else
                    {
                        #region 泌水率
                        if (jcxm.Contains("、泌水率、"))
                        {
                            flag = true;
                            sign = true;
                            sign = IsQualified(sItem["G_MSL"], sItem["MSL"], true) == "不符合" ? false : sign;
                            sItem["HG_MSL"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_MSL"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_MSL"] = "----";
                            sItem["HG_MSL"] = "----";
                            sItem["MSL"] = "----";
                        }
                        #endregion

                        #region 竖向膨胀率
                        if (jcxm.Contains("、竖向膨胀率、"))
                        {
                            flag = true;
                            sign = true;
                            sign = IsQualified(sItem["G_PZL3"], sItem["PZL3"], true) == "不符合" ? false : sign;
                            sign = IsQualified(sItem["G_PZL24"], sItem["PZL24"], true) == "不符合" ? false : sign;
                            sItem["HG_PZL"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_PZL"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_PZL3"] = "----";
                            sItem["G_PZL24"] = "----";
                            sItem["HG_PZL"] = "----";
                            sItem["PZL3"] = "----";
                            sItem["PZL24"] = "----";
                        }
                        #endregion

                        #region 流动度
                        if (jcxm.Contains("、流动度、"))
                        {
                            sign = true;
                            sign = IsQualified(sItem["G_LDDCSZ"], sItem["LDDCSZ"], true) == "不符合" ? false : sign;
                            sign = IsQualified(sItem["G_LDDBLZ"], sItem["LDDBLZ"], true) == "不符合" ? false : sign;
                            sItem["HG_LDD"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_LDD"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["HG_LDD"] = "----";
                            sItem["G_LDDCSZ"] = "----";
                            sItem["G_LDDBLZ"] = "----";
                            sItem["LDDCSZ"] = "----";
                            sItem["LDDBLZ"] = "----";
                        }
                        #endregion

                        #region 坍落度
                        if (jcxm.Contains("、坍落度、"))
                        {
                            flag = true;
                            sign = true;
                            sign = IsQualified(sItem["G_TLDCSZ"], sItem["TLDCSZ"], false) == "不合格" ? false : sign;
                            sign = IsQualified(sItem["G_TLDBLZ"], sItem["TLDBLZ"], false) == "不合格" ? false : sign;
                            flag = IsQualified(sItem["G_TLDCSZ"], sItem["TLDCSZ"], false) == "----" ? false : flag;
                            flag = IsQualified(sItem["G_TLDBLZ"], sItem["TLDBLZ"], false) == "----" ? false : flag;
                            if (sign)
                            {
                                sItem["HG_TLD"] = flag ? "合格" : "----";
                                if (!flag)
                                {
                                    itemHG = false;
                                    mAllHg = false;
                                }
                                else
                                {
                                    mbHggs++;
                                }
                            }

                            flag = true;
                            sign = true;
                            sign = IsQualified(sItem["G_TLKZDC"], sItem["TLKZDC"], false) == "不合格" ? false : sign;
                            sign = IsQualified(sItem["G_TLKZDB"], sItem["TLKZDB"], false) == "不合格" ? false : sign;
                            flag = IsQualified(sItem["G_TLKZDC"], sItem["TLKZDC"], false) == "----" ? false : flag;
                            flag = IsQualified(sItem["G_TLKZDB"], sItem["TLKZDB"], false) == "----" ? false : flag;
                            if (sign)
                            {
                                sItem["HG_TLKZD"] = flag ? "合格" : "----";
                                if (!flag)
                                {
                                    itemHG = false;
                                    mAllHg = false;
                                }
                                else
                                {
                                    mbHggs++;
                                }

                            }

                        }
                        else
                        {
                            sItem["G_TLDCSZ"] = "----";
                            sItem["HG_TLD"] = "----";
                            sItem["G_TLDBLZ"] = "----";
                            sItem["TLDCSZ"] = "----";
                            sItem["TLDBLZ"] = "----";
                            sItem["G_TLKZDC"] = "----";
                            sItem["G_TLKZDB"] = "----";
                            sItem["TLKZDC"] = "----";
                            sItem["TLKZDB"] = "----";
                            sItem["HG_TLKZD"] = "----";

                        }
                        #endregion

                        #region 对钢筋锈蚀作用
                        if (jcxm.Contains("、对钢筋锈蚀作用、"))
                        {
                            sItem["G_XSZY"] = "无";
                            sItem["HG_XSZY"] = "无" == sItem["XSZY"] ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_XSZY"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_XSZY"] = "----";
                            sItem["XSZY"] = "----";
                            sItem["HG_XSZY"] = "----";
                        }
                        #endregion

                        #region 最大集料粒径
                        if (jcxm.Contains("、最大集料粒径、"))
                        {
                            if ("Ⅳ类" == sItem["XH"])
                            {
                                if (IsQualified("＞4.75", sItem["ZDJLLJ"], true) == "符合" && IsQualified("≤16", sItem["ZDJLLJ"], true) == "符合")
                                {
                                    sItem["HG_ZDJLLJ"] = "合格";
                                    mbHggs++;
                                }
                                else
                                {
                                    itemHG = false;
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                if (IsQualified(sItem["G_ZDJLLJ"], sItem["ZDJLLJ"], true) == "符合")
                                {
                                    sItem["HG_ZDJLLJ"] = "合格";
                                    mbHggs++;
                                }
                                else
                                {
                                    itemHG = false;
                                    mAllHg = false;
                                }
                            }
                        }
                        else
                        {
                            sItem["G_ZDJLLJ"] = "----";
                            sItem["HG_ZDJLLJ"] = "----";
                            sItem["ZDJLLJ"] = "----";
                        }
                        #endregion

                        #region 1天抗压强度
                        if (jcxm.Contains("、1天抗压强度、"))
                        {
                            sign = true;
                            flag = true;
                            sign = IsQualified(sItem["G_KYQD1"], sItem["KYPJ_1"], true) == "不符合" ? false : sign;
                            sItem["HG_KYQD1"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_KYQD1"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_KYQD1"] = "----";
                            sItem["HG_KYQD1"] = "----";
                            sItem["KYPJ_1"] = "----";
                        }
                        #endregion

                        #region 3天抗压强度
                        if (jcxm.Contains("、3天抗压强度、"))
                        {
                            sign = true;
                            flag = true;
                            sign = IsQualified(sItem["G_KYQD3"], sItem["KYPJ_3"], true) == "不符合" ? false : sign;
                            sItem["HG_KYQD3"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_KYQD3"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_KYQD3"] = "----";
                            sItem["HG_KYQD3"] = "----";
                            sItem["KYPJ_3"] = "----";
                        }
                        #endregion


                        #region 28天抗压强度
                        if (jcxm.Contains("、28天抗压强度、"))
                        {
                            sign = true;
                            flag = true;
                            sign = IsQualified(sItem["G_KYQD28"], sItem["KYPJ"], true) == "不符合" ? false : sign;
                            sItem["HG_KYQD28"] = sign ? "合格" : "不合格";
                            if ("不合格" == sItem["HG_KYQD28"])
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["G_KYQD28"] = "----";
                            sItem["HG_KYQD28"] = "----";
                            sItem["KYPJ"] = "----";
                        }
                        #endregion
                    }
                    //单组判断
                    if (itemHG)
                    {
                        sItem["JCJG"] = "合格";
                    }
                    else
                    {
                        sItem["JCJG"] = "不合格";
                    }
                    continue;
                }
                #endregion

                #region 最大集料粒径
                if (jcxm.Contains("、最大集料粒径、"))
                {
                    if ("Ⅳ类" == sItem["XH"])
                    {
                        if (IsQualified("＞4.75", sItem["ZDJLLJ"], true) == "符合" && IsQualified("≤16", sItem["ZDJLLJ"], true) == "符合")
                        {
                            sItem["HG_ZDJLLJ"] = "合格";
                            mbHggs++;
                        }
                        else
                        {
                            sItem["HG_ZDJLLJ"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        if (IsQualified(sItem["G_ZDJLLJ"], sItem["ZDJLLJ"], true) == "符合")
                        {
                            sItem["HG_ZDJLLJ"] = "合格";
                            mbHggs++;
                        }
                        else
                        {
                            sItem["HG_ZDJLLJ"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["G_ZDJLLJ"] = "----";
                    sItem["HG_ZDJLLJ"] = "----";
                    sItem["ZDJLLJ"] = "----";
                }
                #endregion

                #region 流动度
                if (jcxm.Contains("、流动度、"))
                {
                    flag = true;
                    sign = true;
                    sign = IsQualified(sItem["G_LDDCSZ"], sItem["LDDCSZ"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_LDDBLZ"], sItem["LDDBLZ"], false) == "不合格" ? false : sign;
                    flag = IsQualified(sItem["G_LDDCSZ"], sItem["LDDCSZ"], false) == "----" ? false : flag;
                    flag = IsQualified(sItem["G_LDDBLZ"], sItem["LDDBLZ"], false) == "----" ? false : flag;
                    if (sign)
                    {
                        sItem["HG_LDD"] = sign ? "合格" : "不合格";
                        if ("不合格" == sItem["HG_LDD"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sItem["HG_LDD"] = "不合格";
                        itemHG = false;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_LDDCSZ"] = "----";
                    sItem["HG_LDD"] = "----";
                    sItem["G_LDDBLZ"] = "----";
                    sItem["LDDCSZ"] = "----";
                    sItem["LDDBLZ"] = "----";
                }
                #endregion


                #region 坍落度
                if (jcxm.Contains("、坍落度、"))
                {
                    flag = true;
                    sign = true;
                    sign = IsQualified(sItem["G_TLDCSZ"], sItem["TLDCSZ"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_TLDBLZ"], sItem["TLDBLZ"], false) == "不合格" ? false : sign;
                    flag = IsQualified(sItem["G_TLDCSZ"], sItem["TLDCSZ"], false) == "----" ? false : flag;
                    flag = IsQualified(sItem["G_TLDBLZ"], sItem["TLDBLZ"], false) == "----" ? false : flag;
                    if (sign)
                    {
                        sItem["HG_TLD"] = flag ? "合格" : "----";
                        if (!flag)
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sItem["HG_TLD"] = "不合格";
                        itemHG = false;
                        mAllHg = false;
                    }

                    flag = true;
                    sign = true;
                    sign = IsQualified(sItem["G_TLKZDC"], sItem["TLKZDC"], false) == "不合格" ? false : sign;
                    sign = IsQualified(sItem["G_TLKZDB"], sItem["TLKZDB"], false) == "不合格" ? false : sign;
                    flag = IsQualified(sItem["G_TLKZDC"], sItem["TLKZDC"], false) == "----" ? false : flag;
                    flag = IsQualified(sItem["G_TLKZDB"], sItem["TLKZDB"], false) == "----" ? false : flag;
                    if (sign)
                    {
                        sItem["HG_TLKZD"] = flag ? "合格" : "----";
                        if (!flag)
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sItem["HG_TLKZD"] = "不合格";
                        itemHG = false;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_TLDCSZ"] = "----";
                    sItem["HG_TLD"] = "----";
                    sItem["G_TLDBLZ"] = "----";
                    sItem["TLDCSZ"] = "----";
                    sItem["TLDBLZ"] = "----";
                    sItem["G_TLKZDC"] = "----";
                    sItem["G_TLKZDB"] = "----";
                    sItem["TLKZDC"] = "----";
                    sItem["TLKZDB"] = "----";
                    sItem["HG_TLKZD"] = "----";

                }
                #endregion

                #region 竖向膨胀率
                if (jcxm.Contains("、竖向膨胀率、"))
                {
                    flag = true;
                    sign = true;
                    sign = IsQualified(sItem["G_PZL24"], sItem["PZL24"], true) == "不符合" ? false : sign;
                    sign = IsQualified(sItem["G_PZL3"], sItem["PZL3"], true) == "不符合" ? false : sign;
                    flag = IsQualified(sItem["G_PZL24"], sItem["PZL24"], true) == "不符合" ? false : flag;
                    flag = IsQualified(sItem["G_PZL3"], sItem["PZL3"], true) == "不符合" ? false : flag;

                    if (sign)
                    {
                        sItem["HG_PZL"] = flag ? "合格" : "----";
                        if (!flag)
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sItem["HG_PZL"] = "不合格";
                        itemHG = false;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_PZL3"] = "----";
                    sItem["G_PZL24"] = "----";
                    sItem["HG_PZL"] = "----";
                    sItem["PZL3"] = "----";
                    sItem["PZL24"] = "----";
                }
                #endregion

                #region 1天抗压强度
                if (jcxm.Contains("、1天抗压强度、"))
                {
                    //水泥算法
                    if ("40" == sItem["SJCC"])
                    {
                        double mmj = 0.625;
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["KYQD" + i + "_1"] = Math.Round(double.Parse(sItem["KYHZ" + i + "_1"]) * mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1_1"] + "," + sItem["KYQD2_1"] + "," + sItem["KYQD3_1"] + ","
                            + sItem["KYQD4_1"] + "," + sItem["KYQD5_1"] + "," + sItem["KYQD6_1"];
                        mtmpArray = mlongStr.Split(',').ToList();

                        for (int vp = 0; vp < 6; vp++)
                        {
                            mkyqdArray.Add(double.Parse(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[mkyqdArray.Count - 1];
                        mMinKyqd = mkyqdArray[0];
                        mMidKyqd = mkyqdArray[1];
                        mAvgKyqd = Math.Round(mkyqdArray.Average(), 1);
                        double mccgs = 0;
                        for (int vp = 0; vp < 6; vp++)
                        {
                            if (Math.Abs(mkyqdArray[vp]) - mAvgKyqd > mAvgKyqd * 0.1)
                            {
                                mccgs++;
                            }
                        }
                        //超过两个超出平均值的10 % 直接作废处理
                        if (mccgs > 1)
                        {
                            mAvgKyqd = -1;
                        }
                        else
                        {
                            if (Math.Abs(mAvgKyqd - mkyqdArray[0]) >= Math.Abs(mAvgKyqd - mkyqdArray[5]))
                            {
                                if (Math.Abs(mAvgKyqd - mkyqdArray[0]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5]) / 5;
                                }
                                if (Math.Abs(mAvgKyqd - mkyqdArray[1]) > mAvgKyqd * 0.1 || Math.Abs(mAvgKyqd - mkyqdArray[5]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = -1;
                                }
                            }
                            else
                            {
                                if (Math.Abs(mAvgKyqd - mkyqdArray[5]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[0]) / 5;
                                }
                                if (Math.Abs(mAvgKyqd - mkyqdArray[0]) > mAvgKyqd * 0.1 || Math.Abs(mAvgKyqd - mkyqdArray[4]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = -1;
                                }
                            }
                        }
                        //计算抗压强度平均值: 有超出平均值10 % 的首先剔除再平均,再有超出平均值10 % 的则作废
                        if (mAvgKyqd == -1)
                        {
                            sItem["KYPJ_1"] = "作废";
                        }
                        else
                        {
                            sItem["KYPJ_1"] = Math.Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        //混凝土算法
                        double mmj = 10000;
                        double mHsxs = 0.95;
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["KYQD" + i + "_1"] = Math.Round(1000 * double.Parse(sItem["KYHZ" + i + "_1"]) / mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1_1"] + "," + sItem["KYQD2_1"] + "," + sItem["KYQD3_1"];
                        mtmpArray = mlongStr.Split(',').ToList();
                        for (int vp = 0; vp < 3; vp++)
                        {
                            mkyqdArray.Add(double.Parse(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[5];
                        mMinKyqd = mkyqdArray[3];
                        mMidKyqd = mkyqdArray[4];
                        mAvgKyqd = Math.Round((mMaxKyqd + mMidKyqd + mMinKyqd) / 3, 1);
                        if (mMidKyqd != 0)
                        {
                            double md = 0;
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["KYPJ"] = "无效";
                            }
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
                            {
                                md = mMidKyqd;
                            }
                            if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                md = mMidKyqd;
                            }
                            if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
                            {
                                md = mAvgKyqd;
                            }
                            if (md != 0)
                            {
                                if (Math.Round(md,0)<=55)
                                {
                                    mHsxs = 0.95;
                                }
                                else if (Math.Round(md, 0) <= 65)
                                {
                                    mHsxs = 0.94;
                                }
                                else if (Math.Round(md, 0) <= 75)
                                {
                                    mHsxs = 0.93;
                                }
                                else if (Math.Round(md, 0) <= 85)
                                {
                                    mHsxs = 0.92;
                                }
                                else if (Math.Round(md, 0) <= 95)
                                {
                                    mHsxs = 0.91;
                                }
                                else if (Math.Round(md, 0) >= 96)
                                {
                                    mHsxs = 0.9;
                                }
                                md = Math.Round(md * mHsxs, 1);
                                sItem["KYPJ_1"] = md.ToString("0.0");
                            }
                        }
                    }

                    sItem["HG_KYQD1"] = IsQualified(sItem["G_KYQD1"], sItem["KYPJ_1"], false);
                    if ("不合格" == sItem["HG_KYQD1"])
                    {
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                }
                else
                {
                    sItem["KYPJ_1"] = "----";
                    sItem["HG_KYQD1"] = "----";
                    sItem["G_KYQD1"] = "----";
                }
                #endregion


                #region 3天抗压强度
                if (jcxm.Contains("、3天抗压强度、"))
                {
                    //水泥算法
                    if ("40" == sItem["SJCC"])
                    {
                        double mmj = 0.625;
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["KYQD" + i + "_3"] = Math.Round(double.Parse(sItem["KYHZ" + i + "_3"]) * mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1_3"] + "," + sItem["KYQD2_3"] + "," + sItem["KYQD3_3"] + ","
                            + sItem["KYQD4_3"] + "," + sItem["KYQD5_3"] + "," + sItem["KYQD6_3"];
                        mtmpArray = mlongStr.Split(',').ToList();

                        for (int vp = 0; vp < 6; vp++)
                        {
                            mkyqdArray.Add(double.Parse(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[mkyqdArray.Count - 1];
                        mMinKyqd = mkyqdArray[0];
                        mMidKyqd = mkyqdArray[1];
                        mAvgKyqd = Math.Round(mkyqdArray.Average(), 1);
                        double mccgs = 0;
                        for (int vp = 0; vp < 6; vp++)
                        {
                            if (Math.Abs(mkyqdArray[vp]) - mAvgKyqd > mAvgKyqd * 0.1)
                            {
                                mccgs++;
                            }
                        }
                        //超过两个超出平均值的10 % 直接作废处理
                        if (mccgs > 1)
                        {
                            mAvgKyqd = -1;
                        }
                        else
                        {
                            if (Math.Abs(mAvgKyqd - mkyqdArray[0]) >= Math.Abs(mAvgKyqd - mkyqdArray[5]))
                            {
                                if (Math.Abs(mAvgKyqd - mkyqdArray[0]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5]) / 5;
                                }
                                if (Math.Abs(mAvgKyqd - mkyqdArray[1]) > mAvgKyqd * 0.1 || Math.Abs(mAvgKyqd - mkyqdArray[5]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = -1;
                                }
                            }
                            else
                            {
                                if (Math.Abs(mAvgKyqd - mkyqdArray[5]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[0]) / 5;
                                }
                                if (Math.Abs(mAvgKyqd - mkyqdArray[0]) > mAvgKyqd * 0.1 || Math.Abs(mAvgKyqd - mkyqdArray[4]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = -1;
                                }
                            }
                        }
                        //计算抗压强度平均值: 有超出平均值10 % 的首先剔除再平均,再有超出平均值10 % 的则作废
                        if (mAvgKyqd == -1)
                        {
                            sItem["KYPJ_3"] = "作废";
                        }
                        else
                        {
                            sItem["KYPJ_3"] = Math.Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        //混凝土算法
                        double mmj = 10000;
                        double mHsxs = 0.95;
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["KYQD" + i + "_3"] = Math.Round(1000 * double.Parse(sItem["KYHZ" + i + "_3"]) / mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1_3"] + "," + sItem["KYQD2_3"] + "," + sItem["KYQD3_3"];
                        mtmpArray = mlongStr.Split(',').ToList();
                        for (int vp = 0; vp < 3; vp++)
                        {
                            mkyqdArray.Add(double.Parse(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[5];
                        mMinKyqd = mkyqdArray[3];
                        mMidKyqd = mkyqdArray[4];
                        mAvgKyqd = Math.Round((mMaxKyqd + mMidKyqd + mMinKyqd) / 3, 1);
                        if (mMidKyqd != 0)
                        {
                            double md = 0;
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["KYPJ"] = "无效";
                            }
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
                            {
                                md = mMidKyqd;
                            }
                            if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                md = mMidKyqd;
                            }
                            if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
                            {
                                md = mAvgKyqd;
                            }
                            if (md != 0)
                            {
                                if (Math.Round(md, 0) <= 55)
                                {
                                    mHsxs = 0.95;
                                }
                                else if (Math.Round(md, 0) <= 65)
                                {
                                    mHsxs = 0.94;
                                }
                                else if (Math.Round(md, 0) <= 75)
                                {
                                    mHsxs = 0.93;
                                }
                                else if (Math.Round(md, 0) <= 85)
                                {
                                    mHsxs = 0.92;
                                }
                                else if (Math.Round(md, 0) <= 95)
                                {
                                    mHsxs = 0.91;
                                }
                                else if (Math.Round(md, 0) >= 96)
                                {
                                    mHsxs = 0.9;
                                }
                                md = Math.Round(md * mHsxs, 1);
                                sItem["KYPJ_3"] = md.ToString("0.0");
                            }
                        }
                    }

                    sItem["HG_KYQD3"] = IsQualified(sItem["G_KYQD3"], sItem["KYPJ_3"], false);
                    if ("不合格" == sItem["HG_KYQD3"])
                    {
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                }
                else
                {
                    sItem["KYPJ_3"] = "----";
                    sItem["HG_KYQD3"] = "----";
                    sItem["G_KYQD3"] = "----";
                }
                #endregion

                #region 28天抗压强度
                if (jcxm.Contains("、28天抗压强度、"))
                {
                    //水泥算法
                    if ("40" == sItem["SJCC"])
                    {
                        double mmj = 0.625;
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["KYQD"+ i] = Math.Round(double.Parse(sItem["KYHZ" + i]) * mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1"] + "," + sItem["KYQD2"] + "," + sItem["KYQD3"] + ","
                            + sItem["KYQD4"] + "," + sItem["KYQD5"] + "," + sItem["KYQD6"];
                        mtmpArray = mlongStr.Split(',').ToList();

                        for (int vp = 0; vp < 6; vp++)
                        {
                            mkyqdArray.Add(double.Parse(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[mkyqdArray.Count - 1];
                        mMinKyqd = mkyqdArray[0];
                        mMidKyqd = mkyqdArray[1];
                        mAvgKyqd = Math.Round(mkyqdArray.Average(), 1);
                        double mccgs = 0;
                        for (int vp = 0; vp < 6; vp++)
                        {
                            if (Math.Abs(mkyqdArray[vp]) - mAvgKyqd > mAvgKyqd * 0.1)
                            {
                                mccgs++;
                            }
                        }
                        //超过两个超出平均值的10 % 直接作废处理
                        if (mccgs > 1)
                        {
                            mAvgKyqd = -1;
                        }
                        else
                        {
                            if (Math.Abs(mAvgKyqd - mkyqdArray[0]) >= Math.Abs(mAvgKyqd - mkyqdArray[5]))
                            {
                                if (Math.Abs(mAvgKyqd - mkyqdArray[0]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[5]) / 5;
                                }
                                if (Math.Abs(mAvgKyqd - mkyqdArray[1]) > mAvgKyqd * 0.1 || Math.Abs(mAvgKyqd - mkyqdArray[5]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = -1;
                                }
                            }
                            else
                            {
                                if (Math.Abs(mAvgKyqd - mkyqdArray[5]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = (mkyqdArray[1] + mkyqdArray[2] + mkyqdArray[3] + mkyqdArray[4] + mkyqdArray[0]) / 5;
                                }
                                if (Math.Abs(mAvgKyqd - mkyqdArray[0]) > mAvgKyqd * 0.1 || Math.Abs(mAvgKyqd - mkyqdArray[4]) > mAvgKyqd * 0.1)
                                {
                                    mAvgKyqd = -1;
                                }
                            }
                        }
                        //计算抗压强度平均值: 有超出平均值10 % 的首先剔除再平均,再有超出平均值10 % 的则作废
                        if (mAvgKyqd == -1)
                        {
                            sItem["KYPJ"] = "作废";
                        }
                        else
                        {
                            sItem["KYPJ"] = Math.Round(mAvgKyqd, 1).ToString();
                        }
                    }
                    else
                    {
                        //混凝土算法
                        double mmj = 10000;
                        double mHsxs = 0.95;
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["KYQD" + i] = Math.Round(1000 * double.Parse(sItem["KYHZ" + i]) / mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1"] + "," + sItem["KYQD2"] + "," + sItem["KYQD3"];
                        mtmpArray = mlongStr.Split(',').ToList();
                        for (int vp = 0; vp < 3; vp++)
                        {
                            mkyqdArray.Add(double.Parse(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[5];
                        mMinKyqd = mkyqdArray[3];
                        mMidKyqd = mkyqdArray[4];
                        mAvgKyqd = Math.Round((mMaxKyqd + mMidKyqd + mMinKyqd) / 3, 1);
                        if (mMidKyqd != 0)
                        {
                            double md = 0;
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["KYPJ"] = "无效";
                            }
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
                            {
                                md = mMidKyqd;
                            }
                            if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                md = mMidKyqd;
                            }
                            if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
                            {
                                md = mAvgKyqd;
                            }
                            if (md != 0)
                            {
                                if (Math.Round(md, 0) <= 55)
                                {
                                    mHsxs = 0.95;
                                }
                                else if (Math.Round(md, 0) <= 65)
                                {
                                    mHsxs = 0.94;
                                }
                                else if (Math.Round(md, 0) <= 75)
                                {
                                    mHsxs = 0.93;
                                }
                                else if (Math.Round(md, 0) <= 85)
                                {
                                    mHsxs = 0.92;
                                }
                                else if (Math.Round(md, 0) <= 95)
                                {
                                    mHsxs = 0.91;
                                }
                                else if (Math.Round(md, 0) >= 96)
                                {
                                    mHsxs = 0.9;
                                }
                                md = Math.Round(md * mHsxs, 1);
                                sItem["KYPJ"] = md.ToString("0.0");
                            }
                        }
                    }

                    sItem["HG_KYQD28"] = IsQualified(sItem["G_KYQD28"], sItem["KYPJ"], false);
                    if ("不合格" == sItem["HG_KYQD28"])
                    {
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                }
                else
                {
                    sItem["KYPJ"] = "----";
                    sItem["HG_KYQD28"] = "----";
                    sItem["G_KYQD28"] = "----";
                }
                #endregion


                #region 对钢筋锈蚀作用
                if (jcxm.Contains("、对钢筋锈蚀作用、"))
                {
                    sItem["HG_XSZY"] = "无" == sItem["XSZY"] ? "合格" : "不合格";

                    if ("不合格" == sItem["HG_XSZY"])
                    {
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                }
                else
                {
                    sItem["G_XSZY"] = "----";
                    sItem["XSZY"] = "----";
                    sItem["HG_XSZY"] = "----";
                }
                #endregion

                #region 泌水率
                if (jcxm.Contains("、泌水率、"))
                {
                    sItem["HG_MSL"] = IsQualified(sItem["G_MSL"], sItem["MSL"], false);
                    if ("不合格" == sItem["HG_MSL"])
                    {
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                }
                else
                {
                    sItem["G_MSL"] = "----";
                    sItem["MSL"] = "----";
                    sItem["HG_MSL"] = "----";
                }
                #endregion

                //单组判断
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
                if (mbHggs >0)
                {
                    jsbeizhu = "该组试样所检项目部分不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
