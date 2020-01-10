using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class WB : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_WB_DJ"];
            //var extraZM_DRJL = dataExtra["ZM_DRJL"];
            
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_WBS = data["S_WB"];
            if (!data.ContainsKey("M_WB"))
            {
                data["M_WB"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_WB"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            List<double> nArr = new List<double>();
            bool sjtabcalc = true;
            int mbHggs = 0;//统计合格数量

            foreach (var sItem in S_WBS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    sjtabcalc = true;
                    #region 干密度
                    if (jcxm.Contains("、干密度、"))
                    {
                        sItem["HG_GMD"] = IsQualified(sItem["G_GMD"], sItem["GMD"], false);
                        if ("不合格" == sItem["HG_GMD"])
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
                        sItem["GMD"] = "----";
                        sItem["HG_GMD"] = "----";
                        sItem["G_GMD"] = "----";
                    }
                    #endregion

                    #region 堆积密度
                    if (jcxm.Contains("、堆积密度、"))
                    {
                        sItem["HG_DJMD"] = IsQualified(sItem["G_DJMD"], sItem["DJMD"], false);
                        if ("不合格" == sItem["HG_DJMD"])
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
                        sItem["DJMD"] = "----";
                        sItem["HG_DJMD"] = "----";
                        sItem["G_DJMD"] = "----";
                    }
                    #endregion

                    #region 抗压强度
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        if (GetSafeDouble(sItem["KYQD"]) == 0)
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_KYQD"] = IsQualified(sItem["G_KYQD"], sItem["KYQD"], false);
                        if ("不合格" == sItem["HG_KYQD"])
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
                        sItem["KYQD"] = "----";
                        sItem["HG_KYQD"] = "----";
                        sItem["G_KYQD"] = "----";
                    }
                    #endregion

                    #region 软化系数
                    if (jcxm.Contains("、软化系数、"))
                    {
                        if (GetSafeDouble(sItem["RHXS"]) == 0)
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_RHXS"] = IsQualified(sItem["G_RHXS"], sItem["RHXS"], false);
                        if ("不合格" == sItem["HG_RHXS"])
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
                        sItem["RHXS"] = "----";
                        sItem["HG_RHXS"] = "----";
                        sItem["G_RHXS"] = "----";
                    }
                    #endregion

                    #region 抗拉强度
                    if (jcxm.Contains("、抗拉强度、"))
                    {
                        if (0 == GetSafeDouble(sItem["KLQD"]))
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false);
                        if ("不合格" == sItem["HG_KLQD"])
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
                        sItem["KLQD"] = "----";
                        sItem["HG_KLQD"] = "----";
                        sItem["G_KLQD"] = "----";
                    }
                    #endregion

                    #region 导热系数
                    if (jcxm.Contains("、导热系数、"))
                    {
                        int mcd = sItem["G_DRXS"].Length;
                        int mdwz = sItem["G_DRXS"].IndexOf(".");
                        mcd = mcd - mdwz + 1;
                        //if (0 < MItem[0]["DEVCODE"].IndexOf("XCS17-067") || 0< MItem[0]["DEVCODE"].IndexOf("XCS17-066"))
                        //{
                        //    //从表zm_drjl筛选出数据
                        //    var mrsDrxs = extraZM_DRJL.FirstOrDefault(u => u["SYLB"] == "WB" && u["SYBH"] ==  MItem[0]["JYDBH"]);
                        //    //var mrsDrxs = extraZM_DRJL.Where(extraZM_DRJL_Filter => extraZM_DRJL_Filter["SYSJBRECID"].Equals(sitem["RECID"]));
                        //    sItem["DRXS"] = mrsDrxs["DRXS"];
                        //    MItem[0]["JCYJ"] = MItem[0]["JCYJ"].Replace("10294", "10295");//字符串替换
                        //}
                        sItem["DRXS"] = Math.Round(GetSafeDouble(sItem["DRXS"]), mcd).ToString();
                        sItem["HG_DRXS"] = IsQualified(sItem["G_DRXS"], sItem["DRXS"], false);
                        if ("不合格" == sItem["HG_DRXS"])
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
                        sItem["DRXS"] = "----";
                        sItem["HG_DRXS"] = "----";
                        sItem["G_DRXS"] = "----";
                    }
                    #endregion

                    #region 线性收缩率
                    if (jcxm.Contains("、线性收缩率、"))
                    {
                        sItem["HG_XXSSL"] = IsQualified(sItem["G_XXSSL"], sItem["XXSSL"], false);
                        if ("不合格" == sItem["HG_XXSSL"])
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
                        sItem["XXSSL"] = "----";
                        sItem["HG_XXSSL"] = "----";
                        sItem["G_XXSSL"] = "----";
                    }
                #endregion

                #region 压剪粘结强度(耐水)
                if (jcxm.Contains("、压剪粘结强度(耐水)、"))
                    {
                        if (GetSafeDouble(sItem["NJQD_YQD"]) == 0)
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_NJQD_NS"] = IsQualified(sItem["G_NJQD_NS"], sItem["NJQD_NS"], false);
                        if ("不合格" == sItem["HG_NJQD_NS"])
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
                        sItem["NJQD_NS"] = "----";
                        sItem["HG_NJQD_NS"] = "----";
                        sItem["G_NJQD_NS"] = "----";
                    }
                    #endregion

                    #region 压剪粘结强度(原强度)
                    if (jcxm.Contains("、压剪粘结强度(原强度)、"))
                    {
                        if (0 == GetSafeDouble(sItem["NJQD_YQD"]))
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_NJQD_YQD"] = IsQualified(sItem["G_NJQD_YQD"], sItem["NJQD_YQD"], false);
                        if ("不合格" == sItem["HG_NJQD_YQD"])
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
                        sItem["NJQD_YQD"] = "----";
                        sItem["HG_NJQD_YQD"] = "----";
                        sItem["G_NJQD_YQD"] = "----";
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
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试件所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                if (mbHggs > 0)
                {
                    jsbeizhu = "该组试件所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "该组试件所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/


        }

    }
}
