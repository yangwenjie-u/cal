using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class FQ : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_FQ_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_FQS = data["S_FQ"];
            if (!data.ContainsKey("M_FQ"))
            {
                data["M_FQ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_FQ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            int mcd, mdwz = 0;
            bool sign = true;
            int mbHggs = 0;//统计合格数量
            var jcxmBhg = "";
            var jcxmCur = "";
            foreach (var sItem in S_FQS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["YPLB"].Trim() == sItem["YPLB"]);
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不下结论";
                    jsbeizhu = "不合格";
                    continue;
                }
                else
                {
                    sItem["G_HDWDX"] = extraFieldsDj["G_CCWDX"].Trim();
                    sItem["G_CDWDX"] = extraFieldsDj["G_CCWDX"].Trim();
                    sItem["G_KDWDX"] = extraFieldsDj["G_CCWDX"].Trim();
                    sItem["G_YSQD"] = extraFieldsDj["G_YSQD"].Trim();
                    sItem["G_DRXS"] = extraFieldsDj["G_DRXS"].Trim();
                    sItem["G_HDWDX1"] = extraFieldsDj["G_CCWDX1"].Trim();
                    sItem["G_CDWDX1"] = extraFieldsDj["G_CCWDX1"].Trim();
                    sItem["G_KDWDX1"] = extraFieldsDj["G_CCWDX1"].Trim();
                    sItem["G_HDWDX2"] = extraFieldsDj["G_CCWDX2"].Trim();
                    sItem["G_CDWDX2"] = extraFieldsDj["G_CCWDX2"].Trim();
                    sItem["G_KDWDX2"] = extraFieldsDj["G_CCWDX2"].Trim();
                    sItem["G_LSQD"] = extraFieldsDj["G_LSQD"].Trim();
                }

                #region 表观密度
                if (jcxm.Contains("、表观密度、"))
                {
                    jcxmCur = "表观密度";
                    sign = true;
                    sign = IsNumeric(sItem["W_MDPC"]) && !string.IsNullOrEmpty(sItem["W_MDPC"]) ? sign : false;
                    sign = IsNumeric(sItem["BCMD"]) && !string.IsNullOrEmpty(sItem["BCMD"]) ? sign : false;
                    if (sign)
                    {
                        sItem["G_MD"] = "允许偏差为标称值的±10%以内";
                        sItem["GH_MD"] = 10 >= Math.Abs(double.Parse(sItem["W_MDPC"])) ? "合格" : "不合格";
                        if ("不合格" == sItem["GH_MD"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    sItem["G_MD"] = "----";
                    sItem["W_MD"] = "----";
                    sItem["W_MDPC"] = "----";
                    sItem["GH_MD"] = "----";
                }
                #endregion

                #region 导热系数
                if (jcxm.Contains("、导热系数、"))
                {
                    jcxmCur = "导热系数";
                    sign = true;
                    sign = IsNumeric(sItem["W_DRXS"]) && !string.IsNullOrEmpty(sItem["W_DRXS"]) ? sign : false;
                    mcd = sItem["G_DRXS"].Length;
                    mdwz = sItem["G_DRXS"].IndexOf(".");
                    mcd = mcd - mdwz + 1;
                    //宋工：先注释
                    //If InStr(1, mrsmainTable!devcode, "XCS17-067") > 0 Or InStr(1, mrsmainTable!devcode, "XCS17-066") > 0 Then
                    //mrsDrxs.Filter = "sylb='fq' and  sybh='" + mrsmainTable!jydbh + "'"
                    //sitem["W_DRXS = mrsDrxs!drxs
                    //mrsmainTable!Jcyj = Replace(mrsmainTable!Jcyj, "10294", "10295")
                    //End If
                    //FormatNumber(Round(CDec(sitem["drxs), mcd), mcd, vbTrue, , vbFalse)
                    //c#可以用    Math.Round(GetSafeDouble(sItem["DRXS"]), mcd).ToString();
                    sItem["W_DRXS"] = Math.Round(GetSafeDouble(sItem["W_DRXS"]), mcd).ToString();
                    if (sign)
                    {
                        sItem["GH_DRXS"] = IsQualified(sItem["G_DRXS"], sItem["W_DRXS"],false);
                        if ("不合格" == sItem["GH_DRXS"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    sItem["G_DRXS"] = "----";
                    sItem["W_DRXS"] = "----";
                    sItem["GH_DRXS"] = "----";
                }
                #endregion

                #region 压缩强度
                if (jcxm.Contains("、压缩强度、"))
                {
                    jcxmCur = "压缩强度";
                    sign = true;
                    sign = IsNumeric(sItem["W_YSQD"]) && !string.IsNullOrEmpty(sItem["W_YSQD"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_YSQD"] = IsQualified(sItem["G_YSQD"], sItem["W_YSQD"], false);
                        if ("不合格" == sItem["GH_YSQD"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    sItem["G_YSQD"] = "----";
                    sItem["W_YSQD"] = "----";
                    sItem["GH_YSQD"] = "----";
                }
                #endregion

                #region 垂直于板面的拉伸强度
                if (jcxm.Contains("、垂直于板面的拉伸强度、"))
                {
                    jcxmCur = "垂直于板面的拉伸强度";
                    sign = true;
                    sign = IsNumeric(sItem["W_LSQD"]) && !string.IsNullOrEmpty(sItem["W_LSQD"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_LSQD"] = IsQualified(sItem["G_LSQD"], sItem["W_LSQD"], false);
                        if ("不合格" == sItem["GH_LSQD"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    sItem["G_LSQD"] = "----";
                    sItem["W_LSQD"] = "----";
                    sItem["GH_LSQD"] = "----";
                }
                #endregion

                #region 尺寸稳定性(-40℃)
                if (jcxm.Contains("、尺寸稳定性(-40℃)、"))
                {
                    jcxmCur = "尺寸稳定性(-40℃)";
                    sign = true;
                    sign = IsNumeric(sItem["W_CDWDX"]) && !string.IsNullOrEmpty(sItem["W_CDWDX"]) ? sign : false;
                    sign = IsNumeric(sItem["W_KDWDX"]) && !string.IsNullOrEmpty(sItem["W_KDWDX"]) ? sign : false;
                    sign = IsNumeric(sItem["W_HDWDX"]) && !string.IsNullOrEmpty(sItem["W_HDWDX"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_CDWDX"] = IsQualified(sItem["G_CDWDX"], sItem["W_CDWDX"], false);
                        sItem["GH_KDWDX"] = IsQualified(sItem["G_KDWDX"], sItem["W_KDWDX"], false);
                        sItem["GH_HDWDX"] = IsQualified(sItem["G_HDWDX"], sItem["W_HDWDX"], false);
                        if ("不合格" == sItem["GH_CDWDX"] || "不合格" == sItem["GH_KDWDX"] || "不合格" == sItem["GH_HDWDX"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    sItem["G_CDWDX"] = "----";
                    sItem["G_KDWDX"] = "----";
                    sItem["G_HDWDX"] = "----";
                    sItem["W_CDWDX"] = "----";
                    sItem["W_KDWDX"] = "----";
                    sItem["W_HDWDX"] = "----";
                    sItem["GH_CDWDX"] = "----";
                    sItem["GH_KDWDX"] = "----";
                    sItem["GH_HDWDX"] = "----";
                }
                #endregion

                #region 尺寸稳定性(70℃)
                if (jcxm.Contains("、尺寸稳定性(70℃)、"))
                {
                    jcxmCur = "尺寸稳定性(70℃)";
                    sign = true;
                    sign = IsNumeric(sItem["W_CDWDX1"]) && !string.IsNullOrEmpty(sItem["W_CDWDX1"]) ? sign : false;
                    sign = IsNumeric(sItem["W_KDWDX1"]) && !string.IsNullOrEmpty(sItem["W_KDWDX1"]) ? sign : false;
                    sign = IsNumeric(sItem["W_HDWDX1"]) && !string.IsNullOrEmpty(sItem["W_HDWDX1"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_CDWDX1"] = IsQualified(sItem["G_CDWDX1"], sItem["W_CDWDX1"], false);
                        sItem["GH_KDWDX1"] = IsQualified(sItem["G_KDWDX1"], sItem["W_KDWDX1"], false);
                        sItem["GH_HDWDX1"] = IsQualified(sItem["G_HDWDX1"], sItem["W_HDWDX1"], false);
                        if ("不合格" == sItem["GH_CDWDX1"] || "不合格" == sItem["GH_KDWDX1"] || "不合格" == sItem["GH_HDWDX1"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    sItem["G_CDWDX1"] = "----";
                    sItem["G_KDWDX1"] = "----";
                    sItem["G_HDWDX1"] = "----";
                    sItem["W_CDWDX1"] = "----";
                    sItem["W_KDWDX1"] = "----";
                    sItem["W_HDWDX1"] = "----";
                    sItem["GH_CDWDX1"] = "----";
                    sItem["GH_KDWDX1"] = "----";
                    sItem["GH_HDWDX1"] = "----";
                }
                #endregion

                #region 尺寸稳定性(130℃)
                if (jcxm.Contains("、尺寸稳定性(130℃)、"))
                {
                    jcxmCur = "尺寸稳定性(130℃)";
                    sign = true;
                    sign = IsNumeric(sItem["W_CDWDX2"]) && !string.IsNullOrEmpty(sItem["W_CDWDX2"]) ? sign : false;
                    sign = IsNumeric(sItem["W_KDWDX2"]) && !string.IsNullOrEmpty(sItem["W_KDWDX2"]) ? sign : false;
                    sign = IsNumeric(sItem["W_HDWDX2"]) && !string.IsNullOrEmpty(sItem["W_HDWDX2"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_CDWDX2"] = IsQualified(sItem["G_CDWDX2"], sItem["W_CDWDX2"], false);
                        sItem["GH_KDWDX2"] = IsQualified(sItem["G_KDWDX2"], sItem["W_KDWDX2"], false);
                        sItem["GH_HDWDX2"] = IsQualified(sItem["G_HDWDX2"], sItem["W_HDWDX2"], false);
                        if ("不合格" == sItem["GH_CDWDX2"] || "不合格" == sItem["GH_KDWDX2"] || "不合格" == sItem["GH_HDWDX2"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    sItem["G_CDWDX2"] = "----";
                    sItem["G_KDWDX2"] = "----";
                    sItem["G_HDWDX2"] = "----";
                    sItem["W_CDWDX2"] = "----";
                    sItem["W_KDWDX2"] = "----";
                    sItem["W_HDWDX2"] = "----";
                    sItem["GH_CDWDX2"] = "----";
                    sItem["GH_KDWDX2"] = "----";
                    sItem["GH_HDWDX2"] = "----";
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
                //if (mbHggs > 0)
                //{
                //    jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                //}
                //else
                //{
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //}
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
