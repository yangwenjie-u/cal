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
            var extraLDD = dataExtra["BZ_NCLLDD"];
            var extraNCLDJ = dataExtra["BZ_NCLDJ"];
            var extraNCLKYQD = dataExtra["BZ_NCLKYQD"];

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
            List<string> qslArray = new List<string>();

            bool sign, flag = true;
            string mlongStr = "";
            double mMaxKyqd, mMinKyqd, mAvgKyqd, mMidKyqd = 0;
            double mMaxQsl, mMinQsl, mAvgQsl, mMidQsl = 0;
            int mbHggs = 0;//检测项目合格数量
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in S_NCLS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

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

                #region 最大集料粒径 细度
                if (jcxm.Contains("、最大集料粒径、") || jcxm.Contains("、细度、"))
                {
                    jcxmCur = "细度";
                    if ("Ⅳ类" == sItem["XH"])
                    {
                        sItem["XDYQ"] = "最大粒径＞4.75mm 并且≤25mm";
                        if (IsQualified("＞4.75", sItem["ZDLJ"], true) == "符合" && IsQualified("≤25", sItem["ZDLJ"], true) == "符合")
                        {
                            sItem["XDPD"] = "符合要求";
                        }
                        else
                        {
                            itemHG = false;
                            mAllHg = false;
                            sItem["XDPD"] = "不符合要求";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        sItem["XDDW"] = "mm";
                        sItem["XDJCJG"] = sItem["ZDLJ"].Trim();
                    }
                    else
                    {
                        //细度
                        if (IsNumeric(sItem["SYZL"].Trim()) && IsNumeric(sItem["SYWZL"].Trim()))
                        {
                            sItem["SYBFS"] = Round(GetSafeDouble(sItem["SYWZL"].Trim()) / GetSafeDouble(sItem["SYZL"].Trim()) * 100, 1).ToString("0.0");
                            sItem["XDYQ"] = "0";
                            if (GetSafeDouble(sItem["SYBFS"]) > 0)
                            {
                                itemHG = false;
                                mAllHg = false;
                                sItem["XDPD"] = "不符合要求";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }
                            else
                            {
                                sItem["XDPD"] = "符合要求";
                            }
                        }
                        else
                        {
                            throw new SystemException("细度试验数据录入有误");
                        }
                        sItem["XDDW"] = "%";
                        sItem["XDJCJG"] = sItem["SYBFS"];
                    }
                }
                else
                {
                    sItem["XDPD"] = "----";
                    sItem["SYBFS"] = "----";
                    sItem["XDYQ"] = "----";
                    sItem["XDDW"] = "----";
                    sItem["XDJCJG"] = "----";
                }
                #endregion

                #region 流动度
                if (jcxm.Contains("、流动度、"))
                {
                    jcxmCur = "流动度";
                    //获取标准值
                    var extraFieldsLDD = extraLDD.FirstOrDefault(u => u["LDDXM"].Trim() == sItem["LDDLB"] && u["LDDLB"] == sItem["XH"]);
                    if (null == extraFieldsLDD)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    sign = true;
                    if ("截锥流动度" == sItem["LDDLB"])
                    {
                        sign = IsNumeric(sItem["CSLDHX"].Trim());
                        sign = IsNumeric(sItem["LDDHX"].Trim());
                        sign = IsNumeric(sItem["CSLDZX"].Trim());
                        sign = IsNumeric(sItem["LDDZX"].Trim());
                        if (sign)
                        {
                            sItem["CSLDDB"] = Round((GetSafeDouble(sItem["CSLDHX"].Trim()) + GetSafeDouble(sItem["CSLDZX"].Trim())) / 2, 0).ToString("0.0");
                            sItem["LDDDB"] = Round((GetSafeDouble(sItem["LDDHX"].Trim()) + GetSafeDouble(sItem["LDDZX"].Trim())) / 2, 0).ToString("0.0");

                            if ("符合" == IsQualified(extraFieldsLDD["CSLDDYQ"], sItem["CSLDDB"], true) && "符合" == IsQualified(extraFieldsLDD["LDDYQ"], sItem["LDDDB"], true))
                            {
                                sItem["LDDPD"] = "符合要求";
                            }
                            else if ("不符合" == IsQualified(extraFieldsLDD["CSLDDYQ"], sItem["CSLDDB"], true) && "不符合" == IsQualified(extraFieldsLDD["LDDYQ"], sItem["LDDDB"], true))
                            {
                                itemHG = false;
                                mAllHg = false;
                                sItem["LDDPD"] = "不符合要求";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }
                            else
                            {
                                sItem["LDDPD"] = "----";
                            }
                        }
                        else
                        {
                            throw new SystemException("流动度试验数据录入有误");
                        }
                        sItem["CSLDDYQ"] = extraFieldsLDD["CSLDDYQ"];
                        sItem["LDDYQ"] = extraFieldsLDD["LDDYQ"];
                        sItem["CSZDW"] = "mm";
                        sItem["DWBLZ"] = "mm";
                        sItem["CSLDDJG"] = sItem["CSLDDB"];
                        sItem["LDDJG"] = sItem["LDDDB"];
                    }
                    else
                    {
                        //流锥流动度
                        sign = IsNumeric(sItem["CSLCSJ"].Trim());
                        sign = IsNumeric(sItem["LCSJ"].Trim());
                        if (sign)
                        {
                            if ("符合" == IsQualified(extraFieldsLDD["CSLDDYQ"], sItem["CSLCSJ"], true) && "符合" == IsQualified(extraFieldsLDD["LDDYQ"], sItem["LCSJ"], true))
                            {
                                sItem["LDDPD"] = "符合要求";
                            }
                            else
                            {
                                itemHG = false;
                                mAllHg = false;
                                sItem["LDDPD"] = "不符合要求";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }
                        }
                        else
                        {
                            throw new SystemException("流动度试验数据录入有误");
                        }
                        sItem["CSLDDYQ"] = extraFieldsLDD["CSLDDYQ"];
                        sItem["LDDYQ"] = extraFieldsLDD["LDDYQ"];
                        sItem["CSZDW"] = "s";
                        sItem["DWBLZ"] = "s";
                        sItem["CSLDDJG"] = sItem["CSLCSJ"];
                        sItem["LDDJG"] = sItem["LCSJ"];
                    }
                }
                else
                {
                    sItem["CSLDDYQ"] = "----";
                    sItem["LDDYQ"] = "----";
                    sItem["LDDPD"] = "----";
                    sItem["CSZDW"] = "----";
                    sItem["DWBLZ"] = "----";
                    sItem["CSLDDJG"] = "----";
                    sItem["LDDJG"] = "----";
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
                    jcxmCur = "竖向膨胀率";
                    sign = true;
                    for (int i = 1; i < 4; i++)
                    {
                        sign = IsNumeric(sItem["GD3_" + i].Trim());
                        sign = IsNumeric(sItem["GD24_" + i].Trim());
                        sign = IsNumeric(sItem["CSGD" + i].Trim());
                    }
                    if (sign)
                    {
                        //竖向膨胀率 = (试件龄期为t时的高度读数 - 试件高度的初始读数) / 试件基准高度  *100
                        for (int i = 1; i < 4; i++)
                        {
                            //3h竖向膨胀率
                            sItem["SXPZL" + i] = Round((GetSafeDouble(sItem["GD3_" + i].Trim()) - GetSafeDouble(sItem["CSGD" + i].Trim())) / GetSafeDouble(sItem["JZGD" + i].Trim()) * 100, 3).ToString("0.000");
                            //24h竖向膨胀率
                            sItem["SXPZL24_" + i] = Round((GetSafeDouble(sItem["GD24_" + i].Trim()) - GetSafeDouble(sItem["CSGD" + i].Trim())) / GetSafeDouble(sItem["JZGD" + i].Trim()) * 100, 3).ToString("0.000");
                        }
                        //3h竖向膨胀率平均值
                        sItem["SXPZL"] = Round((GetSafeDouble(sItem["SXPZL1"]) + GetSafeDouble(sItem["SXPZL2"]) + GetSafeDouble(sItem["SXPZL3"])) / 3, 3).ToString("0.000");
                        //24h竖向膨胀率平均值
                        sItem["SXPZL24"] = Round((GetSafeDouble(sItem["SXPZL24_1"]) + GetSafeDouble(sItem["SXPZL24_2"]) + GetSafeDouble(sItem["SXPZL24_3"])) / 3, 3).ToString("0.000");
                        //3h与24h竖向膨胀率之差
                        sItem["PZLZC"] = Math.Abs(Round(GetSafeDouble(sItem["SXPZL"]) - GetSafeDouble(sItem["SXPZL24"]), 3)).ToString("0.000");
                        //获取标准值
                        var extraFieldsNCLDJ = extraNCLDJ.FirstOrDefault();

                        if (null == extraFieldsNCLDJ)
                        {
                            sItem["JCJG"] = "不下结论";
                            mAllHg = false;
                            continue;
                        }
                        else
                        {
                            //3h竖向膨胀率要求
                            sItem["SXPZLYQ3H"] = extraFieldsNCLDJ["PZL3H"];
                            //3h与24h膨胀率之差要求
                            sItem["SXPZLYQ"] = extraFieldsNCLDJ["PZLC"];
                        }
                        if ("A85" == sItem["KYQDDJ"])
                        {
                            sItem["SXPZLYQ3H"] = "0.02～3.5";
                        }
                        if ("符合" == IsQualified(sItem["SXPZLYQ3H"], sItem["SXPZL"], true) && "符合" == IsQualified(sItem["SXPZLYQ"], sItem["PZLZC"], true))
                        {
                            sItem["PZLDXPD"] = "符合要求";
                        }
                        else
                        {
                            itemHG = false;
                            mAllHg = false;
                            sItem["PZLDXPD"] = "不符合要求";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        throw new SystemException("竖向膨胀率试验数据录入有误");
                    }

                }
                else
                {
                    sItem["PZLDXPD"] = "----";
                    sItem["SXPZLYQ"] = "----";
                    sItem["SXPZLYQ3H"] = "----";
                    sItem["SXPZL"] = "----";
                    sItem["PZLZC"] = "----";
                }
                #endregion

                #region 1天抗压强度
                if (jcxm.Contains("、1天抗压强度、"))
                {
                    jcxmCur = "1天抗压强度";
                    //获取标准值
                    var extraFieldsNCLKYQD = extraNCLKYQD.FirstOrDefault(u => u["JCXMMC"] == "1天抗压强度" && u["KYQDDJ"] == sItem["KYQDDJ"]);
                    if (null == extraFieldsNCLKYQD)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    else
                    {
                        sItem["G_KYQD1"] = extraFieldsNCLKYQD["KYQDYQ"];
                    }
                    //水泥算法  
                    if ("Ⅳ类" != sItem["XH"])
                    {
                        //double mmj = 0.625;
                        double mmj = 1600;
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["KYQD" + i + "_1"] = Math.Round(GetSafeDouble(sItem["KYHZ" + i + "_1"].Trim()) * 1000 / mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1_1"] + "," + sItem["KYQD2_1"] + "," + sItem["KYQD3_1"] + ","
                            + sItem["KYQD4_1"] + "," + sItem["KYQD5_1"] + "," + sItem["KYQD6_1"];
                        mtmpArray = mlongStr.Split(',').ToList();

                        for (int vp = 0; vp < 6; vp++)
                        {
                            mkyqdArray.Add(GetSafeDouble(mtmpArray[vp]));
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
                        //混凝土算法  Ⅳ类灌浆材料
                        //double mmj = 10000;
                        double mmj = 22500;
                        //double mHsxs = 0.95;
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["KYQD" + i + "_1"] = Math.Round(1000 * GetSafeDouble(sItem["KYHZ" + i + "_1"].Trim()) / mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1_1"] + "," + sItem["KYQD2_1"] + "," + sItem["KYQD3_1"];
                        mtmpArray = mlongStr.Split(',').ToList();
                        for (int vp = 0; vp < 3; vp++)
                        {
                            mkyqdArray.Add(GetSafeDouble(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[2];
                        mMinKyqd = mkyqdArray[0];
                        mMidKyqd = mkyqdArray[1];
                        mAvgKyqd = Math.Round((mMaxKyqd + mMidKyqd + mMinKyqd) / 3, 1);
                        if (mMidKyqd != 0)
                        {
                            double md = 0;
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                //sItem["KYPJ"] = "无效";
                                sItem["KYPJ_1"] = "无效";
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
                            //if (md != 0)
                            //{
                            //    if (Math.Round(md,0)<=55)
                            //    {
                            //        mHsxs = 0.95;
                            //    }
                            //    else if (Math.Round(md, 0) <= 65)
                            //    {
                            //        mHsxs = 0.94;
                            //    }
                            //    else if (Math.Round(md, 0) <= 75)
                            //    {
                            //        mHsxs = 0.93;
                            //    }
                            //    else if (Math.Round(md, 0) <= 85)
                            //    {
                            //        mHsxs = 0.92;
                            //    }
                            //    else if (Math.Round(md, 0) <= 95)
                            //    {
                            //        mHsxs = 0.91;
                            //    }
                            //    else if (Math.Round(md, 0) >= 96)
                            //    {
                            //        mHsxs = 0.9;
                            //    }
                            //    md = Math.Round(md * mHsxs, 1);
                            //    sItem["KYPJ_1"] = md.ToString("0.0");
                            //}
                            sItem["KYPJ_1"] = md.ToString("0.0");
                        }
                    }

                    sItem["HG_KYQD1"] = IsQualified(sItem["G_KYQD1"], sItem["KYPJ_1"], false);
                    if ("不合格" == sItem["HG_KYQD1"])
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KYQD1"] = "不符合要求";
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        sItem["HG_KYQD1"] = "符合要求";
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
                    mkyqdArray = new List<double>();
                    jcxmCur = "3天抗压强度";
                    //获取标准值
                    var extraFieldsNCLKYQD = extraNCLKYQD.FirstOrDefault(u => u["JCXMMC"] == "3天抗压强度" && u["KYQDDJ"] == sItem["KYQDDJ"]);
                    if (null == extraFieldsNCLKYQD)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    else
                    {
                        sItem["G_KYQD3"] = extraFieldsNCLKYQD["KYQDYQ"];
                    }
                    //水泥算法
                    if ("Ⅳ类" != sItem["XH"])
                    {
                        //double mmj = 0.625;
                        double mmj = 1600;
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["KYQD" + i + "_3"] = Math.Round(GetSafeDouble(sItem["KYHZ" + i + "_3"].Trim()) * 1000 / mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1_3"] + "," + sItem["KYQD2_3"] + "," + sItem["KYQD3_3"] + ","
                            + sItem["KYQD4_3"] + "," + sItem["KYQD5_3"] + "," + sItem["KYQD6_3"];
                        mtmpArray = mlongStr.Split(',').ToList();

                        for (int vp = 0; vp < 6; vp++)
                        {
                            mkyqdArray.Add(GetSafeDouble(mtmpArray[vp]));
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
                        double mmj = 22500;
                        //double mHsxs = 0.95;
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["KYQD" + i + "_3"] = Math.Round(1000 * GetSafeDouble(sItem["KYHZ" + i + "_3"].Trim()) / mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1_3"] + "," + sItem["KYQD2_3"] + "," + sItem["KYQD3_3"];
                        mtmpArray = mlongStr.Split(',').ToList();
                        for (int vp = 0; vp < 3; vp++)
                        {
                            mkyqdArray.Add(GetSafeDouble(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[2];
                        mMinKyqd = mkyqdArray[0];
                        mMidKyqd = mkyqdArray[1];
                        mAvgKyqd = Math.Round((mMaxKyqd + mMidKyqd + mMinKyqd) / 3, 1);
                        if (mMidKyqd != 0)
                        {
                            double md = 0;
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                //sItem["KYPJ"] = "无效";
                                sItem["KYPJ_3"] = "无效";
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
                            //if (md != 0)
                            //{
                            //    if (Math.Round(md, 0) <= 55)
                            //    {
                            //        mHsxs = 0.95;
                            //    }
                            //    else if (Math.Round(md, 0) <= 65)
                            //    {
                            //        mHsxs = 0.94;
                            //    }
                            //    else if (Math.Round(md, 0) <= 75)
                            //    {
                            //        mHsxs = 0.93;
                            //    }
                            //    else if (Math.Round(md, 0) <= 85)
                            //    {
                            //        mHsxs = 0.92;
                            //    }
                            //    else if (Math.Round(md, 0) <= 95)
                            //    {
                            //        mHsxs = 0.91;
                            //    }
                            //    else if (Math.Round(md, 0) >= 96)
                            //    {
                            //        mHsxs = 0.9;
                            //    }
                            //    md = Math.Round(md * mHsxs, 1);
                            //    sItem["KYPJ_3"] = md.ToString("0.0");
                            //}
                            sItem["KYPJ_3"] = md.ToString("0.0");
                        }
                    }

                    sItem["HG_KYQD3"] = IsQualified(sItem["G_KYQD3"], sItem["KYPJ_3"], false);
                    if ("不合格" == sItem["HG_KYQD3"])
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KYQD3"] = "不符合要求";
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        sItem["HG_KYQD3"] = "符合要求";
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
                    mkyqdArray = new List<double>();
                    jcxmCur = "28天抗压强度";
                    //获取标准值
                    var extraFieldsNCLKYQD = extraNCLKYQD.FirstOrDefault(u => u["JCXMMC"] == "28天抗压强度" && u["KYQDDJ"] == sItem["KYQDDJ"]);
                    if (null == extraFieldsNCLKYQD)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    else
                    {
                        sItem["G_KYQD28"] = extraFieldsNCLKYQD["KYQDYQ"];
                    }
                    //水泥算法
                    //if ("40" == sItem["SJCC"])
                    if ("Ⅳ类" != sItem["XH"])
                    {
                        //double mmj = 0.625;
                        double mmj = 1600;
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["KYQD" + i] = Math.Round(GetSafeDouble(sItem["KYHZ" + i].Trim()) * 1000 / mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1"] + "," + sItem["KYQD2"] + "," + sItem["KYQD3"] + ","
                            + sItem["KYQD4"] + "," + sItem["KYQD5"] + "," + sItem["KYQD6"];
                        mtmpArray = mlongStr.Split(',').ToList();

                        for (int vp = 0; vp < 6; vp++)
                        {
                            mkyqdArray.Add(GetSafeDouble(mtmpArray[vp]));
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
                        double mmj = 22500;
                        //double mHsxs = 0.95;
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["KYQD" + i] = Math.Round(1000 * GetSafeDouble(sItem["KYHZ" + i]) / mmj, 1).ToString();
                        }
                        mlongStr = sItem["KYQD1"] + "," + sItem["KYQD2"] + "," + sItem["KYQD3"];
                        mtmpArray = mlongStr.Split(',').ToList();
                        for (int vp = 0; vp < 3; vp++)
                        {
                            mkyqdArray.Add(GetSafeDouble(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[2];
                        mMinKyqd = mkyqdArray[0];
                        mMidKyqd = mkyqdArray[1];
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
                            //if (md != 0)
                            //{
                            //    if (Math.Round(md, 0) <= 55)
                            //    {
                            //        mHsxs = 0.95;
                            //    }
                            //    else if (Math.Round(md, 0) <= 65)
                            //    {
                            //        mHsxs = 0.94;
                            //    }
                            //    else if (Math.Round(md, 0) <= 75)
                            //    {
                            //        mHsxs = 0.93;
                            //    }
                            //    else if (Math.Round(md, 0) <= 85)
                            //    {
                            //        mHsxs = 0.92;
                            //    }
                            //    else if (Math.Round(md, 0) <= 95)
                            //    {
                            //        mHsxs = 0.91;
                            //    }
                            //    else if (Math.Round(md, 0) >= 96)
                            //    {
                            //        mHsxs = 0.9;
                            //    }
                            //    md = Math.Round(md * mHsxs, 1);
                            //    sItem["KYPJ"] = md.ToString("0.0");
                            //}
                            sItem["KYPJ"] = md.ToString("0.0");
                        }
                    }

                    sItem["HG_KYQD28"] = IsQualified(sItem["G_KYQD28"], sItem["KYPJ"], false);
                    if ("不合格" == sItem["HG_KYQD28"])
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KYQD28"] = "不符合要求";
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        sItem["HG_KYQD28"] = "符合要求";
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
                    jcxmCur = "泌水率";
                    sign = true;
                    for (int i = 1; i < 4; i++)
                    {
                        sign = IsNumeric(sItem["QSZL" + i].Trim());
                        sign = IsNumeric(sItem["SYBHYSL" + i].Trim());
                        sign = IsNumeric(sItem["SYBHWZZL" + i].Trim());
                        sign = IsNumeric(sItem["TJSYZL" + i].Trim());
                        sign = IsNumeric(sItem["RLTZL" + i].Trim());
                    }

                    if (sign)
                    {
                        //混凝土拌合物试样质量
                        for (int i = 1; i < 4; i++)
                        {
                            //容量筒及试样总质量 - 容量筒质量
                            sItem["HNTSYZL" + i] = Round(GetSafeDouble(sItem["TJSYZL" + i].Trim()) - GetSafeDouble(sItem["RLTZL" + i].Trim()), 0).ToString("0");
                            //泌水率 = 泌水总量(mL) /[ (试验拌制混凝土拌合物拌合用水量mL / 试验拌制混凝土拌合物的总质量g) * 混凝土拌合物试样质量]   * 100
                            sItem["QSL" + i] = Round(GetSafeDouble(sItem["QSZL" + i].Trim()) / (GetSafeDouble(sItem["SYBHYSL" + i].Trim())
                                / GetSafeDouble(sItem["SYBHWZZL" + i].Trim()) * GetSafeDouble(sItem["HNTSYZL" + i])) * 100, 0).ToString("0");
                            qslArray.Add(sItem["QSL" + i]);
                        }
                        qslArray.Sort();
                        mMaxQsl = GetSafeDouble(qslArray[2]);
                        mMidQsl = GetSafeDouble(qslArray[1]);
                        mMinQsl = GetSafeDouble(qslArray[0]);
                        if (mMaxQsl - mMidQsl <= mMidQsl * 0.15 && mMidQsl - mMinQsl <= mMidQsl * 0.15)
                        {
                            //取平均值
                            sItem["QSL"] = Round((mMaxQsl + mMidQsl + mMinQsl) / 3, 0).ToString("0");
                        }
                        else if (mMaxQsl - mMidQsl > mMidQsl * 0.15 && mMidQsl - mMinQsl > mMidQsl * 0.15)
                        {
                            throw new SystemException("沁水率试验中三个测值的最大值与最小值与中间值之差均超过中间值的15%，应重新试验。");
                        }
                        else
                        {
                            //以中间值为试验结果
                            sItem["QSL"] = mMidQsl.ToString("0");
                        }
                        //判定  设计值
                        sItem["QSLYQ"] = "0";
                        if (GetSafeDouble(sItem["QSL"]) > 0)
                        {
                            itemHG = false;
                            sItem["QSLPD"] = "不符合要求";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        else
                        {
                            sItem["QSLPD"] = "符合要求";
                        }
                    }
                    else
                    {
                        throw new SystemException("沁水率试验数据录入有误");
                    }
                }
                else
                {
                    sItem["QSLPD"] = "----";
                    sItem["QSL"] = "----";
                    sItem["QSLYQ"] = "----";
                    //    sItem["G_MSL"] = "----";
                    //    sItem["MSL"] = "----";
                    //    sItem["HG_MSL"] = "----";
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
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求	。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
