using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GGH : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_GGH_DJ"];

            //var jcxmItems = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            var S_GGHS = data["S_GGH"];
            if (!data.ContainsKey("M_GGH"))
            {
                data["M_GGH"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GGH"];
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            List<double> nArr = new List<double>();
            double md1, md2, md, pjmd, sum, sum3 = 0;
            double sum2 = 0;
            bool flag, sign = true;
            bool SFlg = true;//是否两根
            bool itemHG = true;//判断单组是否合格
            int mbHggs = 0;//记录合格数量
            foreach (var sItem in S_GGHS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                foreach (var extraFieldsDj in extraDJ)
                {
                    if (sItem["GGPH"].Trim() == extraFieldsDj["GGPH"].Trim() && sItem["YPMC"].Trim() == extraFieldsDj["GGLB"].Trim())
                    {
                        sItem["G_KLQD"] = extraFieldsDj["KLQD"].Trim();
                        sItem["G_LW"] = "弯曲试验为弯心直径=4δ，弯曲180度表面无裂纹。";
                        break;
                    }
                }

                #region 弯曲
                if (jcxm.Contains("、弯曲、"))
                {
                    sItem["HG_LW"] = GetSafeDouble(sItem["LW"]) != 0 && GetSafeDouble(sItem["LW2"]) != 0 ? "合格" : "不合格";
                    if (sItem["HG_LW"] == "不合格")
                    {
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                }
                else
                {
                    sItem["HG_LW"] = "----";
                    sItem["LW"] = "----";
                    sItem["LW2"] = "----";
                    sItem["LW3"] = "----";
                    sItem["G_LW"] = "----";
                }
                #endregion

                #region 拉伸、压扁、弯曲
                if (jcxm.Contains("、拉伸、"))
                {
                    sign = true;
                    flag = true;
                    flag = IsNumeric(sItem["KLHZ"].ToString()) ? flag : false;
                    flag = IsNumeric(sItem["KLHZ2"].ToString()) ? flag : false;
                    //flag = IsNumeric(sItem["KLHZ3"].ToString()) ? flag : false;

                    if (IsNumeric(sItem["SYHD"].ToString()) && IsNumeric(sItem["SYKD"].ToString())
                        && GetSafeDouble(sItem["SYHD"]) > 0 && GetSafeDouble(sItem["SYKD"].Trim()) > 0)
                    {
                        md1 = GetSafeDouble(sItem["SYHD"].Trim());
                        md2 = GetSafeDouble(sItem["SYKD"].Trim());
                        sum = Math.Round(md1 * md2, 2);
                    }
                    else
                    {
                        md1 = GetSafeDouble(sItem["GGWJ"].Trim());
                        md2 = GetSafeDouble(sItem["GGBH"].Trim());
                        sum = 3.14159 * (Math.Pow(md1 / 2, 2) - Math.Pow((md1 / 2 - md2), 2));
                        if (IsNumeric(sItem["GGWJ2"].ToString()) && IsNumeric(sItem["GGBH2"].ToString()))
                        {
                            md1 = GetSafeDouble(sItem["GGWJ2"].Trim());
                            md2 = GetSafeDouble(sItem["GGBH2"].Trim());
                            sum2 = 3.14159 * (Math.Pow(md1 / 2, 2) - Math.Pow((md1 / 2 - md2), 2));
                        }
                        else
                        {
                            sum2 = sum;
                        }
                    }

                    if (!string.IsNullOrEmpty(sItem["SYHD2"]) && !string.IsNullOrEmpty(sItem["SYKD2"]))
                    {
                        if (IsNumeric(sItem["SYHD2"]) && IsNumeric(sItem["SYKD2"])
                            && GetSafeDouble(sItem["SYHD2"].Trim()) > 0 && GetSafeDouble(sItem["SYKD2"].Trim()) > 0)
                        {
                            md1 = GetSafeDouble(sItem["SYHD2"].Trim());
                            md2 = GetSafeDouble(sItem["SYKD2"].Trim());
                            sum2 = Math.Round(md1 * md2, 2);
                        }
                    }
                    else
                    {
                        sum2 = Math.Round(md1 * md2, 2);
                    }
                    //if (IsNumeric(sItem["SYHD2"].ToString()) && IsNumeric(sItem["SYKD2"].ToString())
                    //    && GetSafeDouble(sItem["SYHD2"]) > 0 && GetSafeDouble(sItem["SYKD2"].Trim()) > 0)
                    //{
                    //    md1 = GetSafeDouble(sItem["SYHD2"].Trim());
                    //    md2 = GetSafeDouble(sItem["SYKD2"].Trim());
                    //    sum2 = Math.Round(md1 * md2, 2);
                    //}

                    if (!string.IsNullOrEmpty(sItem["SYHD3"]) && !string.IsNullOrEmpty(sItem["SYKD3"]))
                    {
                        if (IsNumeric(sItem["SYHD3"]) && IsNumeric(sItem["SYKD3"])
                            && GetSafeDouble(sItem["SYHD3"].Trim()) > 0 && GetSafeDouble(sItem["SYKD3"].Trim()) > 0)
                        {
                            md1 = GetSafeDouble(sItem["SYHD3"].Trim());
                            md2 = GetSafeDouble(sItem["SYKD3"].Trim());
                            sum3 = Math.Round(md1 * md2, 2);
                        }
                    }
                    else
                    {
                        sum3 = Math.Round(md1 * md2, 2);
                    }

                    if (flag)
                    {
                        //屈服强度 抗拉强度
                        md1 = GetSafeDouble(sItem["KLHZ"]);
                        md2 = sum;
                        md = 1000 * md1 / md2;
                        md = Math.Round(md / 5, 0) * 5;
                        sItem["KLQD"] = md.ToString("0");

                        md1 = GetSafeDouble(sItem["KLHZ2"]);
                        md2 = sum2;
                        md = 1000 * md1 / md2;
                        md = Math.Round(md / 5, 0) * 5;
                        sItem["KLQD2"] = md.ToString("0");
                        if (sItem["KLHZ3"] != null)
                        {
                            md1 = GetSafeDouble(sItem["KLHZ3"]);
                        }
                        md2 = sum3;
                        md = 1000 * md1 / md2;
                        md = Math.Round(md / 5, 0) * 5;
                        sItem["KLQD3"] = md.ToString("0");

                        if (sItem["YPMC"] == "直缝电焊")
                        {
                            sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false) == "合格" ? "合格" : "不合格";
                            if (sItem["HG_KLQD"] == "不合格")
                            {
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                        else
                        {
                            sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false) == "合格" && IsQualified(sItem["G_KLQD"], sItem["KLQD2"], false) == "合格" ? "合格" : "不合格";
                            if (sItem["HG_KLQD"] == "不合格")
                            {
                                mAllHg = false;
                                itemHG = false;
                            }
                            else
                            {
                                mbHggs++;
                            }
                        }
                    }
                    else
                    {
                        sign = false;
                    }

                }
                else
                {
                    sign = false;
                }
                #endregion

                if (!sign)
                {
                    sItem["G_KLQD"] = "----";
                    sItem["KLQD"] = "----";
                    sItem["KLQD2"] = "----";
                    sItem["KLQD3"] = "----";
                    sItem["HG_KLQD"] = "----";
                }

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
                if (mbHggs > 0)
                {
                    jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
