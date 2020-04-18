using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
/*土工*/
namespace Calculates
{
    public class SCY : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_AM_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_SCYS = data["S_SCY"];
            bool sign = false;
            if (!data.ContainsKey("M_SCY"))
            {
                data["M_SCY"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_SCY"];
            var ET_SF = data["ET_SF"][0];
            double sum = 0;
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            string mJSFF = "";
            int mHggs = 0;//统计合格数量
            foreach (var sItem in S_SCYS)
            {
                #region 颗粒分析
                //筛前总土质量   小于2mm土质量
                if (IsNumeric(ET_SF["SQZTZL"].Trim()) && IsNumeric(ET_SF["XTZL"].Trim()))
                {
                    //小于2mm土占总土质量 %
                    ET_SF["XTBL"] = Round(GetSafeDouble(ET_SF["XTZL"].Trim()) / GetSafeDouble(ET_SF["SQZTZL"].Trim()), 1).ToString("0.0");
                }
                sign = true;
                //细筛总质量（小于2mm取样质量）
                sign = IsNumeric(ET_SF["XSFZL"]) && GetSafeDouble(ET_SF["XSFZL"]) != 0 ? sign : false;
                if (sign)
                {
                    ET_SF["XSFZL"] = "数据不合法";
                    break;
                }
                //如果2mm筛下的土小于试验总质量的10% 可省略细筛筛析
                if (GetSafeDouble(ET_SF["XTBL"]) >= 10)
                {
                    //细筛
                    sign = true;
                    for (int i = 1; i <= 7; i++)
                    {
                        if (IsNumeric(ET_SF["XLSZL" + i]))
                        {
                            sign = true;
                        }
                        else
                        {
                            sign = false;
                        }
                    }
                    if (sign)
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            //细筛余通过占总土质量百分   小于该孔径的土质量占总土质量百分比 =  小于该孔径的土质量/土总质量
                            ET_SF["TGBF" + (i + 6)] = Round(GetSafeDouble(ET_SF["XTGBF" + i]) * GetSafeDouble(ET_SF["XTZL"]) / GetSafeDouble(ET_SF["SQZTZL"].Trim()), 1).ToString("0.0");
                        }
                        for (int i = 1; i < 6; i++)
                        {

                            sum = 0;
                            for (int j = i + 1; j <= 6; j++)
                            {
                                sum = sum + GetSafeDouble(ET_SF["XLSZL" + j]);
                            }
                            //细筛余通过质量  小于该筛孔的土质量y
                            ET_SF["XTGZL" + i] = sum.ToString("0.0");
                           
                        }
                        for (int i = 1; i < 6; i++)
                        {
                            //细筛余通过百分 小于该孔径的土质量百分比 = 小于该孔径的土质量 / 小于2mm的试样质量 * 0.075mm处占总土质量百分比
                            ET_SF["XTGBF" + i] = Round(100 * sum / GetSafeDouble(ET_SF["XSFZL"].Trim()) * GetSafeDouble(ET_SF["TGBF11"]), 1).ToString("0.0");
                        }
                    }
                    else
                    {
                        throw new SystemException("细筛余数据录入有误");
                    }
                }
                else
                {
                    for (int i = 1; i < 6; i++)
                    {
                        ET_SF["XTGZL" + i] = "----";
                        ET_SF["XTGBF" + i] = "----";
                        ET_SF["TGBF" + (i + 6)] = "----";
                    }
                }

                //如果2mm筛下的土大于试验总质量的90% 即2mm筛上的土小于试验总质量的10%  可省略粗筛筛析
                if (GetSafeDouble(ET_SF["XTBL"]) > 90)
                {
                    //粗筛
                    for (int i = 1; i <= 7; i++)
                    {
                        if (IsNumeric(ET_SF["CLSZL" + i]))
                        {
                            sign = true;
                        }
                        else
                        {
                            sign = false;
                        }
                    }
                    if (sign)
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            sum = 0;
                            for (int j = i + 1; j <= 7; j++)
                            {
                                sum = sum + GetSafeDouble(ET_SF["CLSZL" + j]);
                            }
                            //粗筛余通过质量 小于该筛孔的土质量g
                            ET_SF["CTGZL" + i] = sum.ToString("0.0");
                            //粗筛余通过百分 小于该筛孔土质量百分率
                            ET_SF["CTGBF" + i] = Round(100 * sum / GetSafeDouble(ET_SF["SQZTZL"].Trim()), 1).ToString("0.0");
                           
                        }
                        for (int i = 1; i < 7; i++)
                        {
                            //筛余通过百分   小于该孔径的土质量占总土质量百分比 =  小于该孔径的土质量/土总质量 * 2mm处占总土质量百分比
                            ET_SF["TGBF" + i] = Round(100 * sum / GetSafeDouble(ET_SF["SQZTZL"].Trim()) * GetSafeDouble(ET_SF["TGBF6"].Trim()), 1).ToString("0.0");
                        }
                    }
                    else
                    {
                        throw new SystemException("粗筛余数据录入有误");
                    }
                }
                else
                {
                    for (int i = 1; i < 6; i++)
                    {
                        ET_SF["CTGZL" + i] = "----";
                        ET_SF["CTGBF" + i] = "----";
                        ET_SF["TGBF" + i] = "----";
                    }
                }
                #endregion

                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                #region 颗粒分析
                if (jcxm.Contains("、颗粒分析、"))
                {
                    sItem["GH_SF"] = IsQualified(sItem["SJ_SF1"], sItem["W_SF1"], true);
                    if (sItem["GH_SF"] == "不符合")
                    {
                        mAllHg = false;
                    }
                    sign = true;
                }
                else
                {
                    sItem["W_SF1"] = "----";
                    sItem["BZ_SF"] = "----";
                    sItem["GH_SF"] = "----";
                }
                #endregion

            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组样品所检项目符合标准要求。";
            }
            else
            {
                jsbeizhu = "该组样品所检项目不符合标准要求。";

            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
