using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class TSZ : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_TSZ_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_TSZS = data["S_TSZ"];
            if (!data.ContainsKey("M_TSZ"))
            {
                data["M_TSZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_TSZ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            double mS, mBzz, mPjz = 0;
            string mSjdjbh, mSjdj = "";
            bool sign = true;
            foreach (var sItem in S_TSZS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if ("砖" == sItem["GGXH"].Trim())
                {
                    mSjdj = sItem["KYDJ"];
                }
                else
                {
                    mSjdj = sItem["KZDJ"];
                }
                if (mSjdj == null)
                {
                    mSjdj = "";
                }
                mPjz = 0;

                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["KYDJ"].Trim() == mSjdj.Trim());
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "不合格";
                    continue;
                }
                else
                {
                    sItem["G_KYPJ"] = extraFieldsDj["KYPJZ"];
                    sItem["G_KZPJ"] = extraFieldsDj["KZPJZ"];
                    sItem["G_KYMIN"] = extraFieldsDj["KYMIN"];
                    sItem["G_KZMIN"] = extraFieldsDj["KZMIN"];
                    sItem["G_QDSS"] = extraFieldsDj["QDSS"];
                    sItem["G_ZLSS"] = extraFieldsDj["ZLSS"];
                }
                //计算龄期
                sItem["LQ"] = (DateTime.Parse(sItem["SYRQ"]) - DateTime.Parse(sItem["ZZRQ"])).Days.ToString();
                //sItem["LQ"] = "0";

                #region 劈裂抗拉强度
                if (jcxm.Contains("、劈裂抗拉强度、"))
                {
                    sign = true;
                    for (int i = 1; i < 6; i++)
                    {
                        sign = IsNumeric(sItem["KYQD" + i]) && !string.IsNullOrEmpty(sItem["KYQD" + i]) ? sign : false;
                        sign = IsNumeric(sItem["XXPHHZ" + i]) && !string.IsNullOrEmpty(sItem["XXPHHZ" + i]) ? sign : false;
                        if (!sign)
                        {
                            break;
                        }
                    }
                    if (sign)
                    {
                        sItem["KYQDYQ"] = "抗压强度平均值需" + sItem["G_KYPJ"] + "MPa，单块最小值需" + sItem["G_KYMIN"] + "MPa，单块线性破坏荷载应不小于200N/mm。";
                        sign = IsQualified(sItem["G_KYPJ"], sItem["KYPJ"], false) == "合格" ? sign : false;
                        sign = IsQualified(sItem["G_KYMIN"], sItem["KYQDMIN"], false) == "合格" ? sign : false;
                        for (int i = 1; i < 6; i++)
                        {
                            sign = IsQualified("≥200", sItem["XXPHHZ" + i], false) == "合格" ? sign : false;
                        }

                        if (sign)
                        {
                            sItem["KYPD"] = "合格";
                        }
                        else
                        {
                            sItem["KYPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["KYPJ"] = "----";
                    sItem["KYPD"] = "----";
                    sItem["KYQDMIN"] = "----";
                    sItem["KYQDYQ"] = "----";
                    for (int i = 1; i < 6; i++)
                    {
                        sItem["KYQD" + i] = "----";
                        sItem["XXPHHZ" + i] = "----";
                    }
                }
                #endregion

                #region 抗折强度
                if (jcxm.Contains("、抗折强度、"))
                {
                    sign = true;
                    for (int i = 1; i < 6; i++)
                    {
                        sign = IsNumeric(sItem["KZQD" + i]) && !string.IsNullOrEmpty(sItem["KZQD" + i]) ? sign : false;
                        if (!sign)
                        {
                            break;
                        }
                    }
                    if (sign)
                    {
                        sItem["KZQDYQ"] = "抗压强度平均值需" + sItem["G_KZPJ"] + "MPa，单块最小值需" + sItem["G_KZMIN"] + "MPa。";
                        sign = IsQualified(sItem["G_KZPJ"], sItem["KZPJ"], false) == "合格" ? sign : false;
                        sign = IsQualified(sItem["G_KZMIN"], sItem["KZQDMIN"], false) == "合格" ? sign : false;
                        if (sign)
                        {
                            sItem["KZPD"] = "合格";
                        }
                        else
                        {
                            sItem["KZPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["KZPJ"] = "----";
                    sItem["KZPD"] = "----";
                    sItem["KZQDMIN"] = "----";
                    sItem["KZQDYQ"] = "----";
                    for (int i = 1; i < 6; i++)
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
                    sign = IsNumeric(sItem["ZLSSL"]) && !string.IsNullOrEmpty(sItem["ZLSSL"]) ? sign : false;
                    if (sign)
                    {
                        sItem["KDXYQ"] = "质量损失率" + sItem["G_ZLSS"] + "%，强度损失率" + sItem["G_QDSS"] + "%。";
                        sign = IsQualified(sItem["G_QDSS"], sItem["QDSSL"], false) == "合格" ? sign : false;
                        sign = IsQualified(sItem["G_ZLSS"], sItem["ZLSSL"], false) == "合格" ? sign : false;
                        if (sign)
                        {
                            sItem["KDXPD"] = "合格";
                        }
                        else
                        {
                            sItem["KDXPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["QDSSL"] = "----";
                    sItem["ZLSSL"] = "----";
                    sItem["KDXYQ"] = "----";
                    sItem["KDXPD"] = "----";
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
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"]+ "标准要求。";
            }
            else
            {
                jsbeizhu = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
