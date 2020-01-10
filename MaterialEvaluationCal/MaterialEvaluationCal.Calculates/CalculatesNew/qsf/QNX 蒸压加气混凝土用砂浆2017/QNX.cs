using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class QNX : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            bool mGetBgbh = false;
            int mbhggs = 0;//不合格数量
            string mCpmc = "";
            var extraDJ = dataExtra["BZ_QNX_DJ"];
            var data = retData;

            var SItem = data["S_QNX"];
            var MItem = data["M_QNX"];
            if (!data.ContainsKey("M_QNX"))
            {
                data["M_QNX"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                mCpmc = sItem["CPMC"];
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim() && u["DJLX"] == sItem["DJLX"].Trim());
                if (null != extraFieldsDj)
                {
                    sItem["G_KYQD"] = extraFieldsDj["G_KYQD"].Trim();
                    sItem["G_BSL"] = extraFieldsDj["G_BSL"].Trim();
                    sItem["G_NJQD"] = extraFieldsDj["G_NJQD"].Trim();
                    sItem["G_SSL"] = extraFieldsDj["G_SSL"].Trim();
                    sItem["G_QDSSL"] = extraFieldsDj["G_QDSSL"].Trim();
                    sItem["G_ZLSSL"] = extraFieldsDj["G_ZLSSL"].Trim();

                    sItem["G_NJQD1"] = extraFieldsDj["G_NJQD1"].Trim();
                    sItem["G_NJQD2"] = extraFieldsDj["G_NJQD2"].Trim();
                    sItem["G_NJQD3"] = extraFieldsDj["G_NJQD3"].Trim();
                    sItem["G_NJQD4"] = extraFieldsDj["G_NJQD4"].Trim();

                    sItem["G_LZSJ"] = extraFieldsDj["G_LZSJ"].Trim();
                    sItem["G_KSYL"] = extraFieldsDj["G_KSYL"].Trim();
                    sItem["G_CNSJ"] = extraFieldsDj["G_CNSJ"].Trim();
                    sItem["G_ZNSJ"] = extraFieldsDj["G_ZNSJ"].Trim();
                    sItem["G_KZQD"] = extraFieldsDj["G_KZQD"].Trim();
                    sItem["G_TJMD"] = extraFieldsDj["G_TJMD"].Trim();
                }
                else
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                bool sign = true, wghg = true;

                #region 14d拉伸粘结强度(与蒸压加气混凝土粘结)
                if (jcxm.Contains("、14d拉伸粘结强度(与蒸压加气混凝土粘结)、"))
                {
                    sign = (IsNumeric(sItem["SC_NJQD"]) && !string.IsNullOrEmpty(sItem["SC_NJQD"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_NJQD"] = IsQualified(sItem["G_NJQD"], sItem["SC_NJQD"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_NJQD"] = "----";
                    sItem["SC_NJQD"] = "----";
                    sItem["PD_NJQD"] = "----";
                }
                #endregion

                #region 拉伸粘结强度(与水泥砂浆粘结)常温常态
                sign = true;
                if (jcxm.Contains("、拉伸粘结强度(与水泥砂浆粘结)常温常态、"))
                {
                    sign = (IsNumeric(sItem["SC_NJQD1"]) && !string.IsNullOrEmpty(sItem["SC_NJQD1"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_NJQD1"] = IsQualified(sItem["G_NJQD1"], sItem["SC_NJQD1"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_NJQD1"] = "----";
                    sItem["SC_NJQD1"] = "----";
                    sItem["PD_NJQD1"] = "----";
                }
                #endregion

                #region  拉伸粘结强度(与水泥砂浆粘结)耐水
                sign = true;
                if (jcxm.Contains("、拉伸粘结强度(与水泥砂浆粘结)耐水、"))
                {
                    sign = (IsNumeric(sItem["SC_NJQD2"]) && !string.IsNullOrEmpty(sItem["SC_NJQD2"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_NJQD2"] = IsQualified(sItem["G_NJQD2"], sItem["SC_NJQD2"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_NJQD2"] = "----";
                    sItem["SC_NJQD2"] = "----";
                    sItem["PD_NJQD2"] = "----";
                }
                #endregion

                #region 拉伸粘结强度(与水泥砂浆粘结)耐热
                sign = true;
                if (jcxm.Contains("、拉伸粘结强度(与水泥砂浆粘结)耐热、"))
                {
                    sign = (IsNumeric(sItem["SC_NJQD3"]) && !string.IsNullOrEmpty(sItem["SC_NJQD3"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_NJQD3"] = IsQualified(sItem["G_NJQD3"], sItem["SC_NJQD3"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_NJQD3"] = "----";
                    sItem["SC_NJQD3"] = "----";
                    sItem["PD_NJQD3"] = "----";
                }
                #endregion

                #region 拉伸粘结强度(与水泥砂浆粘结)耐冻融
                sign = true;
                if (jcxm.Contains("、拉伸粘结强度(与水泥砂浆粘结)耐冻融、"))
                {
                    sign = (IsNumeric(sItem["SC_NJQD4"]) && !string.IsNullOrEmpty(sItem["SC_NJQD4"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_NJQD4"] = IsQualified(sItem["G_NJQD4"], sItem["SC_NJQD4"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_NJQD4"] = "----";
                    sItem["SC_NJQD4"] = "----";
                    sItem["PD_NJQD4"] = "----";
                }

                #endregion 抗压强度 or 28d抗压强度
                sign = true;
                if (jcxm.Contains("、抗压强度、") || jcxm.Contains("、28d抗压强度、"))
                {
                    sign = (IsNumeric(sItem["SC_KYQD"]) && !string.IsNullOrEmpty(sItem["SC_KYQD"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_KYQD"] = IsQualified(sItem["G_KYQD"], sItem["SC_KYQD"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_KYQD"] = "----";
                    sItem["SC_KYQD"] = "----";
                    sItem["PD_KYQD"] = "----";
                }

                #region 抗折强度
                sign = true;
                if (jcxm.Contains("、抗折强度、"))
                {
                    sign = (IsNumeric(sItem["SC_KZQD"]) && !string.IsNullOrEmpty(sItem["SC_KZQD"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_KZQD"] = IsQualified(sItem["G_KZQD"], sItem["SC_KZQD"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_KZQD"] = "----";
                    sItem["SC_KZQD"] = "----";
                    sItem["PD_KZQD"] = "----";
                }
                #endregion

                #region 抗冻性
                sign = true;
                if (jcxm.Contains("、抗冻性、"))
                {
                    if (sItem["SC_KDXWG"].Trim() == "明显破坏")
                    {
                        sItem["SC_ZLSSL"] = "----";
                        sItem["PD_ZLSSL"] = "不合格";
                        sItem["SC_QDSSL"] = "----";
                        sItem["PD_QDSSL"] = "不合格";
                        mbhggs++;
                        wghg = false;
                        sItem["G_ZLSSL"] = "质量损失率" + sItem["G_ZLSSL"];
                        sItem["G_QDSSL"] = "强度损失率" + sItem["G_QDSSL"];
                    }
                    else
                    {
                        sItem["PD_ZLSSL"] = IsQualified(sItem["G_ZLSSL"], sItem["SC_ZLSSL"], false);
                        sItem["PD_QDSSL"] = IsQualified(sItem["G_QDSSL"], sItem["SC_QDSSL"], false);
                        sItem["G_ZLSSL"] = "质量损失率" + sItem["G_ZLSSL"];
                        sItem["G_QDSSL"] = "强度损失率" + sItem["G_QDSSL"];
                        //sItem["SYR"] = "";
                    }
                }
                else
                {
                    sItem["G_ZLSSL"] = "----";
                    sItem["SC_ZLSSL"] = "----";
                    sItem["PD_ZLSSL"] = "----";
                    sItem["G_QDSSL"] = "----";
                    sItem["SC_QDSSL"] = "----";
                    sItem["PD_QDSSL"] = "----";
                }
                #endregion

                #region  收缩率
                sign = true;
                if (jcxm.Contains("、收缩率、"))
                {
                    sign = (IsNumeric(sItem["SC_SSL"]) && !string.IsNullOrEmpty(sItem["SC_SSL"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_SSL"] = IsQualified(sItem["G_SSL"], sItem["SC_SSL"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_SSL"] = "----";
                    sItem["SC_SSL"] = "----";
                    sItem["PD_SSL"] = "----";
                }
                #endregion

                #region 保水率
                sign = true;
                if (jcxm.Contains("、保水率、"))
                {
                    sign = (IsNumeric(sItem["SC_BSL"]) && !string.IsNullOrEmpty(sItem["SC_BSL"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_BSL"] = IsQualified(sItem["G_BSL"], sItem["SC_BSL"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_BSL"] = "----";
                    sItem["SC_BSL"] = "----";
                    sItem["PD_BSL"] = "----";
                }
                #endregion

                #region 晾置时间
                sign = true;
                if (jcxm.Contains("、晾置时间、"))
                {
                    sign = (IsNumeric(sItem["SC_LZSJ"]) && !string.IsNullOrEmpty(sItem["SC_LZSJ"])) ? sign : false;
                    if (sign)
                    {
                        sItem["PD_LZSJ"] = IsQualified(sItem["G_LZSJ"], sItem["SC_LZSJ"], false);
                    }
                    //else
                    //{
                    //    sItem["SYR"] = "";
                    //}
                }
                else
                {
                    sItem["G_LZSJ"] = "----";
                    sItem["SC_LZSJ"] = "----";
                    sItem["PD_LZSJ"] = "----";
                }

                mbhggs = sItem["PD_NJQD"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_NJQD1"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_NJQD2"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_NJQD3"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_NJQD4"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_ZLSSL"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_QDSSL"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_KYQD"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_KZQD"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_SSL"].Trim() == "不合格" ? mbhggs+1 : mbhggs;
                mbhggs = sItem["PD_LZSJ"].Trim() == "不合格" ? mbhggs+1 : mbhggs;

                sItem["JCJG"] = mbhggs == 0 ? "合格" : "不合格";
                mAllHg = mbhggs == 0 ? true : false;
                jsbeizhu += mbhggs == 0 ? "该组样品所检项目符合" + MItem[0]["PDBZ"] + "中" + mCpmc + "标准要求。" : "该组所检项目样品不符合" + MItem[0]["PDBZ"] + "中" + mCpmc + "标准要求。";
                #endregion
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

