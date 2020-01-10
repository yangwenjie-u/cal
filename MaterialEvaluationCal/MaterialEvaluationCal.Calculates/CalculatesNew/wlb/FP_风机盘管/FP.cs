using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class FP : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_FP_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_FPS = data["S_FP"];
            if (!data.ContainsKey("M_FP"))
            {
                data["M_FP"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_FP"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            double md = 0;
            string bl = "";
            bool mark = true;
            int mbHggs = 0;//检测项目合格数量

            foreach (var sItem in S_FPS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                sItem["G_SL"] = "----";
                sItem["GH_SL"] = "----";
                if (IsNumeric(sItem["SC_SL"]))
                {
                    sItem["JY_SL"] = sItem["ED_GDJY"].Trim();
                }
                else
                {
                    sItem["JY_SL"] = "----";
                }

                if (jcxm.Contains("、水阻、"))
                {
                    if (IsNumeric(sItem["SC_SL"]))
                    {
                        sItem["JY_SL"] = sItem["ED_GDJY"].Trim();
                    }
                }

                if (IsNumeric(sItem["SC_GLL"]))
                {
                    sItem["G_GLL"] = "≥95%额定值";
                    if (IsNumeric(sItem["ED_GDGLL"]))
                    {
                        md = double.Parse(sItem["ED_GDGLL"].Trim());
                        md = md * 0.95;
                        md = Math.Round(md, 1);
                        bl = md.ToString("0.0");
                        sItem["G_GLL"] = sItem["G_GLL"] + "(" + bl + ")";
                        bl = "≥" + bl;
                        sItem["GH_GLL"] = IsQualified(bl, sItem["SC_GLL"],false);
                        if ("不合格" == sItem["GH_GLL"])
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
                        sItem["GH_GLL"] = "----";
                    }
                    sItem["JY_GLL"] = sItem["ED_GDJY"].Trim();
                }
                else
                {
                    sItem["GH_GLL"] = "----";
                    sItem["G_GLL"] = "----";
                    sItem["JY_GLL"] = "----";
                }


                if (IsNumeric(sItem["SC_ZS"]))
                {
                    sItem["G_ZS"] = "≤额定值";
                    if (IsNumeric(sItem["ED_ZS"]))
                    {
                        bl = sItem["ED_ZS"].Trim(); ;
                        sItem["G_ZS"] = sItem["G_ZS"] + "(" + bl + ")";
                        bl = "≤" + bl;
                        sItem["GH_ZS"] = IsQualified(bl, sItem["SC_ZS"], false);
                        if ("不合格" == sItem["GH_ZS"])
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
                        sItem["GH_ZS"] = "----";
                    }
                    sItem["JY_ZS"] = sItem["ED_GDJY"].Trim();
                }
                else
                {
                    sItem["GH_ZS"] = "----";
                    sItem["G_ZS"] = "----";
                    sItem["JY_ZS"] = "----";
                }

                mark = true;
                if (IsNumeric(sItem["SC_FLGD"]))
                {
                    sItem["G_FLGD"] = "≥95%额定值";
                    if (IsNumeric(sItem["ED_GDGLL"]))
                    {
                        md = double.Parse(sItem["ED_GDFL"].Trim());
                        md = md * 0.95;
                        md = Math.Round(md, 1);
                        bl = md.ToString("0.0");
                        sItem["G_FLGD"] = sItem["G_FLGD"] + "(" + bl + ")";
                        bl = "≥" + bl;
                        if ("不合格" == IsQualified(bl,sItem["SC_FLGD"],false))
                        {
                            mark = false;
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                        sItem["JY_FLGD"] = sItem["ED_GDJY"].Trim();
                    }
                }
                else
                {
                    sItem["G_FLGD"] = "----";
                    sItem["JY_FLGD"] = "----";
                }

                if (IsNumeric(sItem["SC_FLZD"]))
                {
                    sItem["G_FLZD"] = "≥95%额定值";
                    if (IsNumeric(sItem["ED_ZDFL"]))
                    {
                        md = double.Parse(sItem["ED_ZDFL"].Trim());
                        md = md * 0.95;
                        md = Math.Round(md, 1);
                        bl = md.ToString("0.0");
                        sItem["G_FLZD"] = sItem["G_FLZD"] + "(" + bl + ")";
                        bl = "≥" + bl;
                        if ("不合格" == IsQualified(bl, sItem["SC_FLZD"], false))
                        {
                            mark = false;
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                        sItem["JY_FLZD"] = sItem["ED_GDJY"].Trim();
                    }
                }
                else
                {
                    sItem["G_FLZD"] = "----";
                    sItem["JY_FLZD"] = "----";
                }

                if (IsNumeric(sItem["SC_FLDD"]))
                {
                    sItem["G_FLDD"] = "≥95%额定值";
                    if (IsNumeric(sItem["ED_ZDFL"]))
                    {
                        md = double.Parse(sItem["ED_DDFL"].Trim());
                        md = md * 0.95;
                        md = Math.Round(md, 1);
                        bl = md.ToString("0.0");
                        sItem["G_FLDD"] = sItem["G_FLDD"] + "(" + bl + ")";
                        bl = "≥" + bl;
                        if ("不合格" == IsQualified(bl, sItem["SC_FLDD"], false))
                        {
                            mark = false;
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                        sItem["JY_FLDD"] = sItem["ED_GDJY"].Trim();
                    }
                }
                else
                {
                    sItem["G_FLDD"] = "----";
                    sItem["JY_FLDD"] = "----";
                }
                if (mark == false)
                {
                    sItem["GH_FL"] = "不合格";
                    mAllHg = false;
                    itemHG = false;
                }
                else
                {
                    sItem["GH_FL"] = "合格";
                }

                if (IsNumeric(sItem["SC_GRL"]))
                {
                    sItem["G_GRL"] = "≥95%额定值";
                    if (IsNumeric(sItem["ED_GDGRL"]))
                    {
                        md = double.Parse(sItem["ED_GDGRL"].Trim());
                        md = md * 0.95;
                        md = Math.Round(md, 1);
                        bl = md.ToString("0.0");
                        sItem["G_GRL"] = sItem["G_GRL"] + "(" + bl + ")";
                        bl = "≥" + bl;
                        sItem["GH_GRL"] = IsQualified(bl, sItem["SC_GRL"], false);
                        if ("不合格" == sItem["GH_GRL"])
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
                        sItem["GH_GRL"] = "----";
                    }
                    sItem["JY_GRL"] = sItem["ED_GDJY"].Trim();
                }
                else
                {
                    sItem["GH_GRL"] = "----";
                    sItem["G_GRL"] = "----";
                    sItem["JY_GRL"] = "----";
                }

                if (IsNumeric(sItem["SC_SRGL"]))
                {
                    sItem["G_SRGL"] = "≤110%额定值";
                    if (IsNumeric(sItem["ED_GDGRL"]))
                    {
                        md = double.Parse(sItem["ED_SJGL"].Trim());
                        md = md * 1.1;
                        md = Math.Round(md, 1);
                        bl = md.ToString("0.0");
                        sItem["G_SRGL"] = sItem["G_SRGL"] + "(" + bl + ")";
                        bl = "≤" + bl;
                        sItem["GH_SRGL"] = IsQualified(bl, sItem["SC_SRGL"], false);
                        if ("不合格" == sItem["GH_GRL"])
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
                        sItem["GH_SRGL"] = "----";
                    }
                    sItem["JY_SRGL"] = sItem["ED_GDJY"].Trim();
                }
                else
                {
                    sItem["GH_SRGL"] = "----";
                    sItem["G_SRGL"] = "----";
                    sItem["JY_SRGL"] = "----";
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
            /************************ 代码结束 ********************/
        }
    }
}
