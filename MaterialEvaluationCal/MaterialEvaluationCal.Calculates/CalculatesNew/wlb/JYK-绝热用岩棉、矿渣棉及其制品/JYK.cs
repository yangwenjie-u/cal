using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    /* 绝热用岩棉、矿渣棉及其制品 */
    public class JYK : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_JYK_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_JYKS = data["S_JYK"];
            if (!data.ContainsKey("M_JYK"))
            {
                data["M_JYK"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_JYK"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            int mHggs = 0;//统计合格数量
            bool sign = true;
            foreach (var sItem in S_JYKS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                switch (sItem["CPMC"])
                {
                    case "岩棉管壳":
                        MItem[0]["G_DRXS"] = "≤0.044";
                        break;

                    case "矿渣棉管壳":
                        MItem[0]["G_DRXS"] = "≤0.044";
                        break;
                    default:
                        MItem[0]["G_DRXS"] = "≤0.043";
                        break;
                }

                MItem[0]["G_XSL"] = "≤10";
                MItem[0]["G_ZSL"] = "≥98.0";
                MItem[0]["G_ZLXSL"] = "≤1.0";
                MItem[0]["G_GQPXD"] = "≤10";
                MItem[0]["G_MD"] = "与标称密度（" + sItem["BCMD"].Trim() + "）的允许偏差为±10%";

                #region 质量吸湿率
                if (jcxm.Contains("、质量吸湿率、"))
                {
                    sign = true;
                    sign = IsNumeric(MItem[0]["W_ZLXSL"]) && !string.IsNullOrEmpty(MItem[0]["W_ZLXSL"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_ZLXSL"] = IsQualified(MItem[0]["G_ZLXSL"], MItem[0]["W_ZLXSL"], false);
                        if ("不合格" == MItem[0]["GH_ZLXSL"])
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                }
                else
                {
                    MItem[0]["W_ZLXSL"] = "----";
                    MItem[0]["G_ZLXSL"] = "----";
                    MItem[0]["GH_ZLXSL"] = "----";
                }
                #endregion

                #region 体积吸水率
                if (jcxm.Contains("、体积吸水率、"))
                {
                    sign = true;
                    sign = IsNumeric(MItem[0]["W_XSL"]) && !string.IsNullOrEmpty(MItem[0]["W_XSL"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_XSL"] = IsQualified(MItem[0]["G_XSL"], MItem[0]["W_XSL"], false);
                        if ("不合格" == MItem[0]["GH_XSL"])
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                }
                else
                {
                    MItem[0]["W_XSL"] = "----";
                    MItem[0]["G_XSL"] = "----";
                    MItem[0]["GH_XSL"] = "----";
                }
                #endregion

                #region 导热系数
                if (jcxm.Contains("、导热系数、"))
                {
                    //    If InStr(1, mrsmainTable!devcode, "XCS17-067") > 0 Or InStr(1, mrsmainTable!devcode, "XCS17-066") > 0 Then
                    //        mrsDrxs.Filter = "sylb='bb' and  sybh='" + mrsmainTable!jydbh + "'"
                    //        sitem["drxs2 = mrsDrxs!drxs
                    //        mrsmainTable!Jcyj = Replace(mrsmainTable!Jcyj, "10294", "10295")
                    //    End If
                    sign = true;
                    sign = IsNumeric(MItem[0]["W_DRXS"]) && !string.IsNullOrEmpty(MItem[0]["W_DRXS"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_DRXS"] = IsQualified(MItem[0]["G_DRXS"], MItem[0]["W_DRXS"], false);
                        if ("不合格" == MItem[0]["GH_DRXS"])
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                }
                else
                {
                    MItem[0]["W_DRXS"] = "----";
                    MItem[0]["G_DRXS"] = "----";
                    MItem[0]["GH_DRXS"] = "----";
                }
                #endregion

                #region 密度
                if (jcxm.Contains("、密度、"))
                {
                    sign = true;
                    sign = IsNumeric(MItem[0]["W_MDPC"]) && !string.IsNullOrEmpty(MItem[0]["W_MDPC"]) ? sign : false;
                    if (sign)
                    {
                        if (10 >= Math.Abs(double.Parse(MItem[0]["W_MDPC"])))
                        {
                            MItem[0]["GH_MD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            MItem[0]["GH_MD"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                }
                else
                {
                    //MItem[0]["W_MD"] = "----";
                    MItem[0]["W_MDPC"] = "----";
                    MItem[0]["GH_MD"] = "----";
                    MItem[0]["G_MD"] = "----";
                }
                #endregion

                #region 憎水率
                if (jcxm.Contains("、憎水率、"))
                {
                    sign = true;
                    sign = IsNumeric(MItem[0]["W_ZSL"]) && !string.IsNullOrEmpty(MItem[0]["W_ZSL"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_ZSL"] = IsQualified(MItem[0]["G_ZSL"], MItem[0]["W_ZSL"], false);
                        if ("不合格" == MItem[0]["GH_ZSL"])
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mHggs++;
                        }
                    }
                }
                else
                {
                    MItem[0]["W_ZSL"] = "----";
                    MItem[0]["GH_ZSL"] = "----";
                    MItem[0]["G_ZSL"] = "----";
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
