using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    /* 路面砖(国标) */
    public class GMZ : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_GMZ_DJ"];
            var extraWLXN = dataExtra["BZ_GMZWLXN"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_GMZS = data["S_GMZ"];
            if (!data.ContainsKey("M_GMZ"))
            {
                data["M_GMZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GMZ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            int mHggs = 0;//统计合格数量
            bool sign = true;
            foreach (var sItem in S_GMZS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                MItem[0]["G_KYPJZ"] = "----";
                MItem[0]["G_KZPJZ"] = "----";
                MItem[0]["G_KYMIN"] = "----";
                MItem[0]["G_KZMIN"] = "----";
                sItem["XSLYQ"] = "----";
                foreach (var extraFieldsDj in extraDJ)
                {
                    if (extraFieldsDj["KYDJ"] == sItem["KYDJ"].Trim())
                    {
                        MItem[0]["G_KYPJZ"] = extraFieldsDj["KYPJZ"];
                        MItem[0]["G_KYMIN"] = extraFieldsDj["KYMIN"];
                    }
                    if (extraFieldsDj["KZDJ"] == sItem["KZDJ"].Trim())
                    {
                        MItem[0]["G_KZPJZ"] = extraFieldsDj["KZPJZ"];
                        MItem[0]["G_KZMIN"] = extraFieldsDj["KZMIN"];
                    }
                    if (extraFieldsDj["ZLDJ"] == sItem["ZLDJ"].Trim())
                    {
                        sItem["XSLYQ"] = extraFieldsDj["XSL"];
                    }
                }
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsWlxn = extraWLXN.FirstOrDefault(u => u["MC"].Trim() == sItem["ZLDJ"].Trim());
                if (null != extraFieldsWlxn)
                {
                    sItem["QDSSLYQ"] = extraFieldsWlxn["QDSSL"];
                    sItem["MKCDYQ"] = extraFieldsWlxn["MKCD"];
                    sItem["NMDYQ"] = extraFieldsWlxn["NMD"];
                    sItem["FHXYQ"] = extraFieldsWlxn["XSL"];
                }
                //跳转
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    #region 抗压
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        sign = true;
                        for (int i = 1; i < 11; i++)
                        {
                            sign = IsNumeric(sItem["KYQD" + i]) && !string.IsNullOrEmpty(sItem["KYQD" + i]) ? sign : false;
                            if (!sign)
                            {
                                break;
                            }
                        }
                        if (sign)
                        {
                            sItem["KYQDYQ"] = "抗压强度平均值需" + MItem[0]["G_KYPJZ"] + "MPa，单块最小值需" + MItem[0]["G_KYMIN"] + "MPa。";
                            sign = IsQualified(MItem[0]["G_KYPJZ"], sItem["KYPJ"], false) == "合格" ? sign : false;
                            sign = IsQualified(MItem[0]["G_KYMIN"], sItem["KYQDMIN"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["KYPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                sItem["KYPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["KYPJ"] = "----";
                        sItem["KYPD"] = "----";
                        sItem["KYQDMIN"] = "----";
                        sItem["KYQDYQ"] = "----";
                        for (int i = 1; i < 11; i++)
                        {
                            sItem["KYQD" + i] = "----";
                        }
                    }
                    #endregion

                    #region 抗折
                    if (jcxm.Contains("、抗折强度、"))
                    {
                        sign = true;
                        for (int i = 1; i < 11; i++)
                        {
                            sign = IsNumeric(sItem["KZQD" + i]) && !string.IsNullOrEmpty(sItem["KZQD" + i]) ? sign : false;
                            if (!sign)
                            {
                                break;
                            }
                        }
                        if (sign)
                        {
                            sItem["KYQDYQ"] = "抗折强度平均值需" + MItem[0]["G_KZPJZ"] + "MPa，单块最小值需" + MItem[0]["G_KZMIN"] + "MPa。";
                            sign = IsQualified(MItem[0]["G_KZPJZ"], sItem["KZPJ"], false) == "合格" ? sign : false;
                            sign = IsQualified(MItem[0]["G_KZMIN"], sItem["KZQDMIN"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["KZPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                sItem["KZPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["KZPJ"] = "----";
                        sItem["KZPD"] = "----";
                        sItem["KZQDMIN"] = "----";
                        sItem["KZQDYQ"] = "----";
                        for (int i = 1; i < 11; i++)
                        {
                            sItem["KZQD" + i] = "----";
                        }
                    }
                    #endregion


                    #region 抗冻性
                    if (jcxm.Contains("、抗冻性、"))
                    {
                        sign = true;
                        sign = IsNumeric(sItem["QDSSL"]) && !string.IsNullOrEmpty(sItem["QDSSL"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["QDSSLYQ"], sItem["QDSSL"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["QDSSLPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                sItem["QDSSLPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["QDSSL"] = "----";
                        sItem["QDSSLYQ"] = "----";
                        sItem["QDSSLPD"] = "----";
                    }
                    #endregion

                    #region 吸水率
                    if (jcxm.Contains("、吸水率、"))
                    {
                        sign = true;
                        sign = IsNumeric(sItem["XSL"]) && !string.IsNullOrEmpty(sItem["XSL"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["XSLYQ"], sItem["XSL"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["XSLPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                sItem["XSLPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["XSL"] = "----";
                        sItem["XSLYQ"] = "----";
                        sItem["XSLPD"] = "----";
                    }
                    #endregion

                    #region 磨坑长度
                    if (jcxm.Contains("、磨坑长度、"))
                    {
                        sign = true;
                        sign = IsNumeric(sItem["MKCD"]) && !string.IsNullOrEmpty(sItem["MKCD"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["MKCDYQ"], sItem["MKCD"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["MKCDPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                sItem["MKCDPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["MKCDYQ"] = "----";
                        sItem["MKCD"] = "----";
                        sItem["MKCDPD"] = "----";
                    }
                    #endregion

                    #region 耐磨度
                    if (jcxm.Contains("、耐磨性、"))
                    {
                        sign = true;
                        sign = IsNumeric(sItem["NMD"]) && !string.IsNullOrEmpty(sItem["NMD"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["NMDYQ"], sItem["NMD"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["NMDPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                sItem["NMDPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["NMDYQ"] = "----";
                        sItem["NMD"] = "----";
                        sItem["NMDPD"] = "----";
                    }
                    #endregion

                    #region 防滑性
                    if (jcxm.Contains("、防滑性能、"))
                    {
                        sign = true;
                        sign = IsNumeric(sItem["FHX"]) && !string.IsNullOrEmpty(sItem["FHX"]) ? sign : false;
                        if (sign)
                        {
                            sign = IsQualified(sItem["FHXYQ"], sItem["FHX"], false) == "合格" ? sign : false;
                            if (sign)
                            {
                                sItem["FHXPD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                sItem["FHXPD"] = "不合格";
                                mAllHg = false;
                                itemHG = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["FHX"] = "----";
                        sItem["FHXYQ"] = "----";
                        sItem["FHXPD"] = "----";
                    }
                    #endregion

                    if (sItem["GGXH"] == "大于4")
                    {
                        sItem["KYDJ"] = "----";
                    }
                    else
                    {
                        sItem["KZDJ"] = "----";
                    }
                }
                else
                {
                    //模板录入，代码不执行
                    itemHG = false;
                    mAllHg = false;
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
            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组样品所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                if (mHggs > 0)
                {
                    jsbeizhu = "该组样品所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "该组样品所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
