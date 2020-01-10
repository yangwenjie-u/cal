using System.Collections.Generic;

namespace Calculates
{
    public class BL : BaseMethods
    {
        public void Calc()
        {
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_BL"];
            var MItem = data["M_BL"];
            if (!data.ContainsKey("M_BL"))
            {
                data["M_BL"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];

            foreach (var sItem in SItem)
            {
                double md1, md2, md3, xd1, xd2, xd3, md, pjmd, sum;
                bool flag = true, sign = true, mark = true;
                int mbHggs = 0;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                if (jcxm.Contains("、中空玻璃露点、"))
                {
                    if (mItem["JCYJ"].Contains("11944-2012"))
                    {
                        mark = false;
                        for (int i = 1; i <= 15; i++)
                        {
                            if (GetSafeDouble(sItem["LDWD" + i]) < -40)
                            {
                                mark = true;
                                sItem["DDPD" + i] = "合格";
                            }
                            else
                            {
                                sItem["DDPD" + i] = "不合格";
                            }
                        }
                        mItem["G_BLLD"] = "中空玻璃的露点应＜－40℃";
                        mItem["GH_BLLD"] = mark ? "合格" : "不合格";
                    }
                    else
                    {
                        for (int i = 1; i <= 20; i++)
                        {
                            if (!IsNumeric(sItem["LDWD" + i]))
                            {
                                sign = false;
                                break;
                            }
                        }
                        if (sign)
                        {
                            mark = true;
                            for (int i = 1; i <= 20; i++)
                            {
                                if (!IsNumeric(sItem["LDWD" + i]))
                                {
                                    md = GetSafeDouble(sItem["LDWD" + i]);
                                    if (md >= 0) md = -1 * md;
                                    sItem["LDWD" + i] = Round(md, 1).ToString();
                                    sItem["JLQK" + i] = string.IsNullOrEmpty(sItem["JLQK" + i]) ? "" : sItem["JLQK" + i];
                                    flag = sItem["JLQK" + i] == "结露" ? false : true;
                                    flag = IsQualified("＞-40", sItem["LDWD" + i], true) != "符合" ? true : flag;
                                    mark = flag ? mark : false;
                                    sItem["DDPD" + i] = flag ? "合格" : "不合格";
                                    mItem["W_LDWD" + i] = Round(md, 1).ToString();
                                    mark = md > -40 ? false : mark;
                                }
                                mItem["G_BLLD"] = "中空玻璃的露点应＜－40℃";
                                mItem["GH_BLLD"] = mark ? "合格" : "不合格";
                            }
                        }
                    }
                }
                else
                {
                    sign = false;
                }
                if (!sign)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        mItem["W_LDWD" + i] = "----";
                        sItem["DDPD" + i] = "----";
                        sItem["JLQK" + i] = "----";
                    }
                    mItem["G_BLLD"] = "----";
                    mItem["GH_BLLD"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、可见光透射比、"))
                {
                    if (IsNumeric(sItem["SJTSB"]))
                    {
                        mItem["G_TSB"] = "≤" + sItem["SJTSB"].Trim();
                    }
                    else
                    {
                        mItem["G_TSB"] = "----";
                    }
                    int i;
                    for (i = 1; i <= 3; i++)
                    {
                        if (!IsNumeric(sItem["TSB"+i]))
                        {
                            sign = false;
                        }
                        if (!IsNumeric(sItem["FSB" + i]))
                        {
                            sign = false;
                        }
                        if (!sign)
                        {
                            i = i - 1;
                            break;
                        }
                    }

                    if (i == 0 || i == 1)
                    {
                        if (IsNumeric(sItem["TSB1"]))
                        {
                            sign = true;
                            md = Round(GetSafeDouble(sItem["TSB1"]), 1);
                            mItem["W_TSB"] = md.ToString();
                        }
                        else
                        {
                            sign = false;
                        }
                    }else if (i == 2)
                    {
                        sign = true;
                        md1 = GetSafeDouble(sItem["TSB1"]);
                        md2 = GetSafeDouble(sItem["TSB2"]);
                        xd1 = GetSafeDouble(sItem["FSB1"]);
                        xd2 = GetSafeDouble(sItem["FSB2"]);
                        md1 = md1 / 100;
                        md2 = md2 / 100;
                        xd1 = xd1 / 100;
                        xd2 = xd2 / 100;
                        md = md1 * md2 / (1 - xd1 * xd2);
                        md = md * 100;
                        md = Round(md, 1);
                        mItem["W_TSB"] = md.ToString();
                    }
                    else if (i == 3)
                    {
                        sign = true;
                        md1 = GetSafeDouble(sItem["TSB1"]);
                        md2 = GetSafeDouble(sItem["TSB2"]);
                        md3 = GetSafeDouble(sItem["TSB3"]);
                        xd1 = GetSafeDouble(sItem["FSB1"]);
                        xd2 = GetSafeDouble(sItem["FSB2"]);
                        xd3 = GetSafeDouble(sItem["FSB3"]);
                        md1 = md1 / 100;
                        md2 = md2 / 100;
                        md3 = md3 / 100;
                        xd1 = xd1 / 100;
                        xd2 = xd2 / 100;
                        xd3 = xd3 / 100;

                        md = md1 * md2 * md3 / ((1 - xd1 * xd2) * (1 - xd2 * xd3) - md2 * md2 * xd1 * xd2);
                        md = md * 100;
                        md = Round(md, 1);
                        mItem["W_TSB"] = md.ToString();
                    }
                    else
                    {
                        sign = false;
                    }
                    mItem["GH_TSB"] = IsQualified(mItem["G_TSB"],sItem["W_TSB"],true);
                }
                else
                {
                    sign = false;
                }
                if (!sign)
                {
                    for (int i = 1;i <= 3; i ++)
                    {
                        sItem["TSB" + i] = "----";
                        sItem["FSB" + i] = "----";
                    }
                    mItem["W_TSB"] = "----";
                    mItem["GH_TSB"] = "----";
                    mItem["G_TSB"] = "----";
                }

                mbHggs = mItem["GH_TSB"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = mItem["GH_BLLD"] == "不合格" ? mbHggs + 1 : mbHggs;
                sItem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";
                //可见光透射比?中空玻璃露点
                if (jcxm.Contains("中空玻璃露点"))
                {
                    jsbeizhu = jsbeizhu + "该组样品所检中空玻璃露点";
                    if (mItem["GH_TSB"] == "不合格" || mItem["GH_BLLD"] == "不合格")
                    {
                        jsbeizhu = jsbeizhu + "不符合GB/T 11944一2012《中空玻璃》标准要求";
                        mAllHg = false;
                    }
                    else
                    {
                        jsbeizhu = jsbeizhu + "符合GB/T 11944一2012《中空玻璃》标准要求";
                    }
                }

                if (jcxm.Contains("中空玻璃露点"))
                {
                    if (jsbeizhu.Length > 0) jsbeizhu = jsbeizhu+ ",";
                    jsbeizhu = jsbeizhu + "可见光透射比";
                    if (mItem["GH_TSB"] == "不合格")
                    {
                        jsbeizhu = jsbeizhu + "不符合设计要求";
                        mAllHg = false;
                    }else if (mItem["GH_TSB"] == "合格")
                    {
                        jsbeizhu = jsbeizhu + "符合设计要求";
                    }
                    else
                    {
                        jsbeizhu = jsbeizhu + "检测结果如上";
                    }
                }
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
