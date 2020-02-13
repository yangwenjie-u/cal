using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class XJL : BaseMethods
    {
        public void Calc()
        {
            bool mAllHg = true, mItemHg = true, mFlag_Bhg = true;
            int mbhggs = 0;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            string mJSFF;
            var SItem = data["S_XJL"];
            var EItem = data["E_SF"];
            var MItem = data["M_XJL"];
            var mItem = MItem[0];
                
            foreach (var sItem in SItem)
            {
                bool jcjgHg = true;
                bool sign;
                double md1, md2, md, sum;
                string sql;

                #region 含水率
                if (sItem["JCXM"].Contains("含水率"))
                {
                    sItem["HSL_GH"] = IsQualified(sItem["G_HSL"], sItem["W_HSL"], true);
                    if (sItem["HSL_GH"] == "不符合")
                    {
                        mAllHg = false; jcjgHg = false;
                    }
                }
                else
                {
                    sItem["W_HSL"] = "----";
                    sItem["HSL_GH"] = "----";
                    sItem["G_HSL"] = "----";
                }
                #endregion

                #region 吸水率
                if (sItem["JCXM"].Contains("吸水率"))
                {
                    sItem["XSL_GH"] = IsQualified(sItem["G_XSL"], sItem["W_XSL"], true);
                    if (sItem["XSL_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["W_XSL"] = "----";
                    sItem["XSL_GH"] = "----";
                    sItem["G_XSL"] = "----";
                }
                #endregion

                #region 表观密度
                if (sItem["JCXM"].Contains("表观密度") || sItem["JCXM"].Contains("密度"))
                {
                    sItem["BGMD_GH"] = IsQualified(sItem["G_BGMD"], sItem["W_BGMD"], true);
                    if (sItem["BGMD_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["W_BGMD"] = "----";
                    sItem["BGMD_GH"] = "----";
                    sItem["G_BGMD"] = "----";
                }
                #endregion

                #region 堆积密度
                if (sItem["JCXM"].Contains("堆积密度"))
                {
                sItem["DJMD_GH"] = IsQualified(sItem["G_DJMD"], sItem["W_DJMD"], true);
                    if (sItem["DJMD_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["DJMD_GH"] = "----";
                    sItem["W_DJMD"] = "----";
                    sItem["G_DJMD"] = "----";
                }
                #endregion

                #region 紧密密度
                if (sItem["JCXM"].Contains("紧密密度"))
                {
                    sItem["JMMD_GH"] = IsQualified(sItem["G_JMMD"], sItem["W_JMMD"], true);
                    if (sItem["JMMD_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["W_JMMD"] = "----";
                    sItem["JMMD_GH"] = "----";
                    sItem["G_JMMD"] = "----";
                }
                #endregion

                #region 含泥量
                if (sItem["JCXM"].Contains("含泥量"))
                {
                    sItem["HNL_GH"] = IsQualified(sItem["G_HNL"], sItem["W_HNL"], true);
                    if (sItem["HNL_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["HNL_GH"] = "----";
                    sItem["W_HNL"] = "----";
                    sItem["G_HNL"] = "----";
                }
                #endregion

                #region 泥块含量
                if (sItem["JCXM"].Contains("泥块含量"))
                {
                    sItem["NKHL_GH"] = IsQualified(sItem["G_NKHL"], sItem["W_NKHL"], true);
                    if (sItem["NKHL_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["W_NKHL"] = "----";
                    sItem["NKHL_GH"] = "----";
                    sItem["G_NKHL"] = "----";
                }
                #endregion

                #region 砂当量
                if (sItem["JCXM"].Contains("砂当量"))
                {
                    sItem["SDL_GH"] = IsQualified(sItem["G_SDL"], sItem["W_SDL"], true);
                    if (sItem["SDL_GH"] == "不符合") { mAllHg = false; jcjgHg = false; }
                }
                else
                {
                    sItem["W_SDL"] = "----";
                    sItem["SDL_GH"] = "----";
                    sItem["G_SDL"] = "----";
                }
                #endregion

                #region 筛分
                if (sItem["JCXM"].Contains("筛分"))
                {
                    foreach (var e in EItem)
                    {
                        e["SysjbRecid"] = sItem["recid"];
                    }
                    //sql = "update e_sf set bgbh='" + sItem["wtbh"] + "' where csylb='" + "XJL" + "' and dzbh='" + sItem["dzbh"] + "'";
                }
                #endregion

                #region 细度模数 
                if (sItem["JCXM"].Contains("细度模数") || sItem["JCXM"].Contains("筛分"))
                {
                    //var E_SF = "select * from E_SF where csylb='" + "XJL" + "' and dzbh='" + sItem["dzbh"] + "'";//, adOpenStatic, adLockBatchOptimistic);
                    sItem["W_XDMS"] = "----";
                    sign = false;
                    if (EItem.Count > 0)
                    {
                        var eItem = EItem.FirstOrDefault(); 
                        sign = string.IsNullOrEmpty(eItem["sfzdy"]) || eItem["sfzdy"] == "否" ? true : false;
                        sItem["W_XDMS"] = eItem["xdms"];
                        //E_SF.Close
                    }

                    if (IsNumeric(sItem["W_XDMS"]) && !string.IsNullOrEmpty(sItem["W_XDMS"]) && sign)
                    {
                        md = GetSafeDouble(sItem["W_XDMS"]);
                        if (md > 3.8)
                        {
                            sItem["XDMS_GH"] = "----";
                            sItem["G_XDMS"] = "----";
                        }
                        else if (md >= 3.1)
                        {
                            sItem["XDMS_GH"] = "粗砂";
                            sItem["G_XDMS"] = "3.1～3.7";
                        }
                        else if (md >= 2.3)
                        {
                            sItem["XDMS_GH"] = "中砂";
                            sItem["G_XDMS"] = "2.3～3.0";
                        }
                        else if (md >= 1.6)
                        {
                            sItem["XDMS_GH"] = "细砂";
                            sItem["G_XDMS"] = "1.6～2.2";
                        }
                        else
                        {
                            sItem["XDMS_GH"] = "----";
                            sItem["G_XDMS"] = "----";
                        }
                    }
                    else
                    {
                        sItem["XDMS_GH"] = "----";
                        sItem["G_XDMS"] = "----";
                    }
                }
                else
                {
                    sItem["W_XDMS"] = "----";
                    sItem["XDMS_GH"] = "----";
                    sItem["G_XDMS"] = "----";
                }
                #endregion

                jsbeizhu = "";
                if (sItem["JCXM"] == "筛分")
                {
                    jsbeizhu = "该组试样的检测结果详见报告第1～2页。";
                }
                else
                {
                    if (sItem["JCXM"].Contains("筛分"))
                    {
                        jsbeizhu = "该组试样的检测结果详见报告第1～2页。";
                    }
                    else
                    {
                        jsbeizhu = "该组试样的检测结果详见报告第1页。";
                    }
                }
                if (jcjgHg)
                {
                    sItem["JCJG"] = "合格";
                    MItem[0]["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    MItem[0]["JCJG"] = "不合格";
                }
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_XJL"))
            {
                data["M_XJL"] = new List<IDictionary<string, string>>();
            }
            var M_XJL = data["M_XJL"];
            
            if (M_XJL.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_XJL.Add(m);
            }
            else
            {
                MItem[0]["JCJG"] = mjcjg;
                MItem[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
