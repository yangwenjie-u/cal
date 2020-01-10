using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
/*安全帽*/
namespace Calculates
{
    public class AM : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_AM_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_AMS = data["S_AM"];
            if (!data.ContainsKey("M_AM"))
            {
                data["M_AM"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_AM"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            string mJSFF = "";
            int mHggs = 0;//统计合格数量
            foreach (var sItem in S_AMS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["MLX"].Trim());
                if (extraFieldsDj != null)
                {
                    mJSFF = (string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"]).ToLower();
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "不合格";
                    continue;
                }

                if (mJSFF == "")
                {
                    #region  冲击吸收性能
                    if (jcxm.Contains("、冲击吸收性能、"))
                    {
                        sItem["CJXSYQ"] = extraFieldsDj["CJXS"];
                        if (IsQualified(extraFieldsDj["CJXS"], sItem["CJXS1"],true) == "符合")
                        {
                            sItem["CJXS1PD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["CJXS1PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }

                        if (IsQualified(extraFieldsDj["CJXS"], sItem["CJXS2"], true) == "符合")
                        {
                            sItem["CJXS2PD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["CJXS2PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }

                        if (IsQualified(extraFieldsDj["CJXS"], sItem["CJXS3"], true) == "符合")
                        {
                            sItem["CJXS3PD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["CJXS3PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["CJXSYQ"] = "----";
                        sItem["CJXS1"] = "----";
                        sItem["CJXS2"] = "----";
                        sItem["CJXS3"] = "----";
                        sItem["CJXS1PD"] = "----";
                        sItem["CJXS2PD"] = "----";
                        sItem["CJXS3PD"] = "----";
                    }
                    #endregion

                    #region  耐穿刺性能
                    if (jcxm.Contains("、耐穿刺性能、"))
                    {
                        sItem["NCCYQ"] = extraFieldsDj["NCC"];

                        if ("符合" == sItem["NCC1"])
                        {
                            sItem["NCC1PD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["NCC1PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }

                        if ("符合" == sItem["NCC2"])
                        {
                            sItem["NCC2PD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["NCC2PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }

                        if ("符合" == sItem["NCC3"])
                        {
                            sItem["NCC3PD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["NCC3PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["NCC1"] = "----";
                        sItem["NCC2"] = "----";
                        sItem["NCC3"] = "----";
                        sItem["NCCYQ"] = "----";
                        sItem["NCC1PD"] = "----";
                        sItem["NCC2PD"] = "----";
                        sItem["NCC3PD"] = "----";
                    }
                    #endregion

                    #region  下颏带的强度
                    if (jcxm.Contains("、下颏带的强度、"))
                    {
                        sItem["XEDDQDYQ"] = extraFieldsDj["XEDDQD"];
                        if (IsQualified(extraFieldsDj["XEDDQD"],sItem["XEDDQD"],true) == "符合")
                        {
                            sItem["XEDDQDPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["XEDDQDPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["XEDDQDPD"] = "----";
                        sItem["XEDDQD"] = "----";
                        sItem["XEDDQDYQ"] = "----";
                    }
                    #endregion

                    #region  电绝缘性能
                    if (jcxm.Contains("、电绝缘性能、"))
                    {
                        sItem["DJYYQ"] = extraFieldsDj["DJY"];
                        if (sItem["DJY"] == "符合")
                        {
                            sItem["DJYPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["DJYPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["DJYPD"] = "----";
                        sItem["DJYYQ"] = "----";
                        sItem["DJY"] = "----";
                    }
                    #endregion

                    #region  阻燃性能
                    if (jcxm.Contains("、阻燃性能、"))
                    {
                        sItem["ZRYQ"] = extraFieldsDj["ZR"];
                        if (sItem["ZR"] == "符合")
                        {
                            sItem["ZRPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["ZRPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["ZRPD"] = "----";
                        sItem["ZRYQ"] = "----";
                        sItem["ZR"] = "----";
                    }
                    #endregion

                    #region  防静电性能
                    if (jcxm.Contains("、防静电性能、"))
                    {
                        sItem["FJDYQ"] = extraFieldsDj["FJD"];
                        if (sItem["FJD"] == "符合")
                        {
                            sItem["FJDPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["FJDPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["FJDPD"] = "----";
                        sItem["FJD"] = "----";
                        sItem["FJDYQ"] = "----";
                    }
                    #endregion

                    #region  侧向刚性
                    if (jcxm.Contains("、侧向刚性、"))
                    {
                        sItem["CXGXYQ"] = extraFieldsDj["CXGX"];
                        if (sItem["CXGX"] == "符合")
                        {
                            sItem["CXGXPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            sItem["CXGXPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["CXGXPD"] = "----";
                        sItem["CXGXYQ"] = "----";
                        sItem["CXGX"] = "----";
                    }
                    #endregion
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
