using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class KG : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var data = retData;
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_KG_DJ"];
            //获取主表数据
            var MItem = data["M_KG"];
            //获取从表表数据
            var SItem = data["S_KG"];
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            string mJSFF = "";
            bool itemHG = true;//判断单组是否合格
            int mbHggs = 0;//记录合格数量
            bool mAllHg = true;

            #region 获取等级表中标准要求
            foreach (var sItem in SItem)
            {
                itemHG = true;
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["GGXH"].Trim() == sItem["GGXH"]);

                if (null != extraFieldsDj)
                {
                    MItem[0]["G_BZ"] = string.IsNullOrEmpty(extraFieldsDj["BZ"]) ? "" : extraFieldsDj["BZ"].Trim();
                    MItem[0]["G_DBH"] = string.IsNullOrEmpty(extraFieldsDj["DBH"]) ? "" : extraFieldsDj["DBH"].Trim();
                    MItem[0]["G_JXG"] = string.IsNullOrEmpty(extraFieldsDj["JXG"]) ? "" : extraFieldsDj["JXG"].Trim();
                    MItem[0]["G_FC"] = string.IsNullOrEmpty(extraFieldsDj["FC"]) ? "" : extraFieldsDj["FC"].Trim();
                    MItem[0]["G_JYDZ"] = string.IsNullOrEmpty(extraFieldsDj["JYDZ"]) ? "" : extraFieldsDj["JYDZ"].Trim();
                    MItem[0]["G_JG1"] = string.IsNullOrEmpty(extraFieldsDj["JG1"]) ? "" : extraFieldsDj["JG1"].Trim();
                    MItem[0]["G_JG2"] = string.IsNullOrEmpty(extraFieldsDj["JG2"]) ? "" : extraFieldsDj["JG2"].Trim();
                    MItem[0]["G_JG3"] = string.IsNullOrEmpty(extraFieldsDj["JG3"]) ? "" : extraFieldsDj["JG3"].Trim();
                    MItem[0]["G_TDNL1"] = string.IsNullOrEmpty(extraFieldsDj["TDNL1"]) ? "" : extraFieldsDj["TDNL1"].Trim();
                    MItem[0]["G_TDNL2"] = string.IsNullOrEmpty(extraFieldsDj["TDNL2"]) ? "" : extraFieldsDj["TDNL2"].Trim();
                    MItem[0]["G_DQD"] = string.IsNullOrEmpty(extraFieldsDj["DQD"]) ? "" : extraFieldsDj["DQD"].Trim();
                    MItem[0]["G_WS"] = string.IsNullOrEmpty(extraFieldsDj["WS"]) ? "" : extraFieldsDj["WS"].Trim();
                    MItem[0]["G_PDJL"] = string.IsNullOrEmpty(extraFieldsDj["PDJL"]) ? "" : extraFieldsDj["PDJL"].Trim();
                    MItem[0]["G_JYQH"] = string.IsNullOrEmpty(extraFieldsDj["JYQH"]) ? "" : extraFieldsDj["JYQH"].Trim();
                    MItem[0]["G_NR1"] = string.IsNullOrEmpty(extraFieldsDj["NR1"]) ? "" : extraFieldsDj["NR1"].Trim();
                    MItem[0]["G_NR2"] = string.IsNullOrEmpty(extraFieldsDj["NR2"]) ? "" : extraFieldsDj["NR2"].Trim();
                    MItem[0]["G_NR3"] = string.IsNullOrEmpty(extraFieldsDj["NR3"]) ? "" : extraFieldsDj["NR3"].Trim();
                    MItem[0]["G_JXQD"] = string.IsNullOrEmpty(extraFieldsDj["JXQD"]) ? "" : extraFieldsDj["JXQD"].Trim();
                    MItem[0]["G_LD1"] = string.IsNullOrEmpty(extraFieldsDj["LD1"]) ? "" : extraFieldsDj["LD1"].Trim();
                    MItem[0]["G_LD2"] = string.IsNullOrEmpty(extraFieldsDj["LD2"]) ? "" : extraFieldsDj["LD2"].Trim();
                    MItem[0]["G_ZCCZ1"] = string.IsNullOrEmpty(extraFieldsDj["ZCCZ1"]) ? "" : extraFieldsDj["ZCCZ1"].Trim();
                    MItem[0]["G_ZCCZ2"] = string.IsNullOrEmpty(extraFieldsDj["ZCCZ2"]) ? "" : extraFieldsDj["ZCCZ2"].Trim();
                    MItem[0]["G_ZCCZ3"] = string.IsNullOrEmpty(extraFieldsDj["ZCCZ3"]) ? "" : extraFieldsDj["ZCCZ3"].Trim();
                    MItem[0]["G_NLH"] = string.IsNullOrEmpty(extraFieldsDj["NLH"]) ? "" : extraFieldsDj["NLH"].Trim();
                    MItem[0]["G_DZ"] = string.IsNullOrEmpty(extraFieldsDj["DZ"]) ? "" : extraFieldsDj["DZ"].Trim();
                    MItem[0]["G_JX"] = string.IsNullOrEmpty(extraFieldsDj["JX"]) ? "" : extraFieldsDj["JX"].Trim();
                    mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"].Trim().ToLower();
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不合格";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }
                #endregion
                //开始计算
                if (mJSFF == "")
                {
                    #region  标志
                    if (jcxm.Contains("、标志、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["BZ"]) && 0 <= GetSafeDouble(MItem[0]["BZ"]))
                        {
                            MItem[0]["BZ_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["BZ_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["BZ"])
                        {
                            MItem[0]["BZ_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["BZ"] = "-1";
                        MItem[0]["BZ_HG"] = "----";
                    }
                    #endregion

                    #region  防潮
                    if (jcxm.Contains("、防潮、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["FC"]) && 0 <= GetSafeDouble(MItem[0]["FC"]))
                        {
                            MItem[0]["FC_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["FC_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["FC"])
                        {
                            MItem[0]["FC_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["FC"] = "-1";
                        MItem[0]["FC_HG"] = "----";
                    }
                    #endregion

                    #region  端子
                    if (jcxm.Contains("、端子、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["DZ"]) && 0 <= GetSafeDouble(MItem[0]["DZ"]))
                        {
                            MItem[0]["DZ_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["DZ_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["DZ"])
                        {
                            MItem[0]["DZ_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["DZ"] = "-1";
                        MItem[0]["DZ_HG"] = "----";
                    }
                    #endregion

                    #region  电气间隙
                    if (jcxm.Contains("、电气间隙、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["JX"]) && 0 <= GetSafeDouble(MItem[0]["JX"]))
                        {
                            MItem[0]["JX_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["JX_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["JX"])
                        {
                            MItem[0]["JX_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["JX"] = "-1";
                        MItem[0]["JX_HG"] = "----";
                    }
                    #endregion

                    #region  结构
                    if (jcxm.Contains("、结构、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["JG1"]) && 0 <= GetSafeDouble(MItem[0]["JG1"]))
                        {
                            MItem[0]["JG1_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["JG1_HG"] = "合格";
                            mbHggs++;
                        }

                        if (3 > GetSafeDouble(MItem[0]["JG2"]) && 0 <= GetSafeDouble(MItem[0]["JG2"]))
                        {
                            MItem[0]["JG2_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["JG2_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["JG1"])
                        {
                            MItem[0]["JG1_HG"] = "----";
                        }

                        if ("-1" == MItem[0]["JG2"])
                        {
                            MItem[0]["JG2_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["JG1"] = "-1";
                        MItem[0]["JG1_HG"] = "----";
                        MItem[0]["JG2"] = "-1";
                        MItem[0]["JG2_HG"] = "----";
                    }
                    #endregion

                    #region  电气强度
                    /*if (jcxm.Contains("、电气强度、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["DQD1"]) && 0 <= GetSafeDouble(MItem[0]["DQD1"]))
                        {
                            MItem[0]["DQD1_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["DQD1_HG"] = "合格";
                            mbHggs++;
                        }

                        if (3 > GetSafeDouble(MItem[0]["DQD2"]) && 0 <= GetSafeDouble(MItem[0]["DQD2"]))
                        {
                            MItem[0]["DQD2_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["DQD2_HG"] = "合格";
                            mbHggs++;
                        }

                        if (3 > GetSafeDouble(MItem[0]["DQD3"]) && 0 <= GetSafeDouble(MItem[0]["DQD3"]))
                        {
                            MItem[0]["DQD3_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["DQD3_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["DQD1"])
                        {
                            MItem[0]["DQD1_HG"] = "----";
                        }

                        if ("-1" == MItem[0]["DQD2"])
                        {
                            MItem[0]["DQD2_HG"] = "----";
                        }

                        if ("-1" == MItem[0]["DQD3"])
                        {
                            MItem[0]["DQD3_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["DQD1"] = "-1";
                        MItem[0]["DQD1_HG"] = "----";
                        MItem[0]["DQD2"] = "-1";
                        MItem[0]["DQD2_HG"] = "----";
                        MItem[0]["DQD3"] = "-1";
                        MItem[0]["DQD3_HG"] = "----";
                    }*/
                    #endregion

                    #region  耐老化
                    if (jcxm.Contains("、耐老化、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["NLH"]) && 0 <= GetSafeDouble(MItem[0]["NLH"]))
                        {
                            MItem[0]["NLH_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["NLH_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["NLH"])
                        {
                            MItem[0]["NLH_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["NLH"] = "-1";
                        MItem[0]["NLH_HG"] = "----";
                    }
                    #endregion

                    #region  绝缘电阻
                    /*if (jcxm.Contains("、绝缘电阻、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["JYDZ1"]) && 0 <= GetSafeDouble(MItem[0]["JYDZ1"]))
                        {
                            MItem[0]["JYDZ1_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["JYDZ1_HG"] = "合格";
                            mbHggs++;
                        }

                        if (3 > GetSafeDouble(MItem[0]["JYDZ2"]) && 0 <= GetSafeDouble(MItem[0]["JYDZ2"]))
                        {
                            MItem[0]["JYDZ2_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["JYDZ2_HG"] = "合格";
                            mbHggs++;
                        }

                        if (3 > GetSafeDouble(MItem[0]["JYDZ3"]) && 0 <= GetSafeDouble(MItem[0]["JYDZ3"]))
                        {
                            MItem[0]["JYDZ3_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["JYDZ3_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["JYDZ1"])
                        {
                            MItem[0]["JYDZ1_HG"] = "----";
                        }

                        if ("-1" == MItem[0]["JYDZ2"])
                        {
                            MItem[0]["JYDZ2_HG"] = "----";
                        }

                        if ("-1" == MItem[0]["JYDZ3"])
                        {
                            MItem[0]["JYDZ3_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["JYDZ1"] = "-1";
                        MItem[0]["JYDZ1_HG"] = "----";
                        MItem[0]["JYDZ2"] = "-1";
                        MItem[0]["JYDZ2_HG"] = "----";
                        MItem[0]["JYDZ3"] = "-1";
                        MItem[0]["JYDZ3_HG"] = "----";
                    }*/
                    #endregion

                    #region  截流部件及连接
                    if (jcxm.Contains("、截流部件及连接、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["LD1"]) && 0 <= GetSafeDouble(MItem[0]["LD1"]))
                        {
                            MItem[0]["LD1_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["LD1_HG"] = "合格";
                            mbHggs++;
                        }

                        if (3 > GetSafeDouble(MItem[0]["LD2"]) && 0 <= GetSafeDouble(MItem[0]["LD2"]))
                        {
                            MItem[0]["LD2_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["LD2_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["LD1"])
                        {
                            MItem[0]["LD1_HG"] = "----";
                        }

                        if ("-1" == MItem[0]["LD2"])
                        {
                            MItem[0]["LD2_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["LD1"] = "-1";
                        MItem[0]["LD1_HG"] = "----";
                        MItem[0]["LD2"] = "-1";
                        MItem[0]["LD2_HG"] = "----";
                    }
                    #endregion

                    #region  耐漏电起痕
                    if (jcxm.Contains("、耐漏电起痕、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["JYQH"]) && 0 <= GetSafeDouble(MItem[0]["JYQH"]))
                        {
                            MItem[0]["JYQH_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["JYQH_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["JYQH"])
                        {
                            MItem[0]["JYQH_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["JYQH"] = "-1";
                        MItem[0]["JYQH_HG"] = "----";
                    }
                    #endregion

                    #region  机械强度
                    if (jcxm.Contains("、机械强度、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["JXQD"]) && 0 <= GetSafeDouble(MItem[0]["JXQD"]))
                        {
                            MItem[0]["JXQD_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["JXQD_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["JXQD"])
                        {
                            MItem[0]["JXQD_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["JXQD"] = "-1";
                        MItem[0]["JXQD_HG"] = "----";
                    }
                    #endregion

                    #region  爬电距离
                    /*if (jcxm.Contains("、爬电距离、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["PDJL"]) && 0 <= GetSafeDouble(MItem[0]["PDJL"]))
                        {
                            MItem[0]["PDJL_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["PDJL_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["PDJL"])
                        {
                            MItem[0]["PDJL_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["PDJL"] = "-1";
                        MItem[0]["PDJL_HG"] = "----";
                    }*/
                    #endregion

                    #region  耐热
                    if (jcxm.Contains("、耐热、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["NR1"]) && 0 <= GetSafeDouble(MItem[0]["NR1"]))
                        {
                            MItem[0]["NR1_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["NR1_HG"] = "合格";
                            mbHggs++;
                        }

                        if (3 > GetSafeDouble(MItem[0]["NR2"]) && 0 <= GetSafeDouble(MItem[0]["NR2"]))
                        {
                            MItem[0]["NR2_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["NR2_HG"] = "合格";
                            mbHggs++;
                        }

                        if (3 > GetSafeDouble(MItem[0]["NR3"]) && 0 <= GetSafeDouble(MItem[0]["NR3"]))
                        {
                            MItem[0]["NR3_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["NR3_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["NR1"])
                        {
                            MItem[0]["NR1_HG"] = "----";
                        }

                        if ("-1" == MItem[0]["NR2"])
                        {
                            MItem[0]["NR2_HG"] = "----";
                        }

                        if ("-1" == MItem[0]["NR3"])
                        {
                            MItem[0]["NR3_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["NR1"] = "-1";
                        MItem[0]["NR1_HG"] = "----";
                        MItem[0]["NR2"] = "-1";
                        MItem[0]["NR2_HG"] = "----";
                        MItem[0]["NR3"] = "-1";
                        MItem[0]["NR3_HG"] = "----";
                    }
                    #endregion

                    #region  正常操作1
                    if (jcxm.Contains("、正常操作1、") || jcxm.Contains("、开关正常操作次数、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["ZCCZ1"]) && 0 <= GetSafeDouble(MItem[0]["ZCCZ1"]))
                        {
                            MItem[0]["ZCCZ1_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["ZCCZ1_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["ZCCZ1"])
                        {
                            MItem[0]["ZCCZ1_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["ZCCZ1"] = "-1";
                        MItem[0]["ZCCZ1_HG"] = "----";
                    }
                    #endregion

                    #region  正常操作2
                    if (jcxm.Contains("、正常操作2、") || jcxm.Contains("、开关正常操作次数、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["ZCCZ2"]) && 0 <= GetSafeDouble(MItem[0]["ZCCZ2"]))
                        {
                            MItem[0]["ZCCZ2_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["ZCCZ2_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["ZCCZ2"])
                        {
                            MItem[0]["ZCCZ2_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["ZCCZ2"] = "-1";
                        MItem[0]["ZCCZ2_HG"] = "----";
                    }
                    #endregion

                    #region  正常操作3
                    if (jcxm.Contains("、正常操作3、") || jcxm.Contains("、开关正常操作次数、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["ZCCZ3"]) && 0 <= GetSafeDouble(MItem[0]["ZCCZ3"]))
                        {
                            MItem[0]["ZCCZ3_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["ZCCZ3_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["ZCCZ3"])
                        {
                            MItem[0]["ZCCZ3_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["ZCCZ3"] = "-1";
                        MItem[0]["ZCCZ3_HG"] = "----";
                    }
                    #endregion

                    #region  通断能力1
                    if (jcxm.Contains("、通断能力1、") || jcxm.Contains("、开关通断能力、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["TDNL1"]) && 0 <= GetSafeDouble(MItem[0]["TDNL1"]))
                        {
                            MItem[0]["TDNL1_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["TDNL1_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["TDNL1"])
                        {
                            MItem[0]["TDNL1_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["TDNL1"] = "-1";
                        MItem[0]["TDNL1_HG"] = "----";
                    }
                    #endregion

                    #region  通断能力2
                    if (jcxm.Contains("、通断能力2、") || jcxm.Contains("、开关通断能力、"))
                    {
                        if (3 > GetSafeDouble(MItem[0]["TDNL2"]) && 0 <= GetSafeDouble(MItem[0]["TDNL2"]))
                        {
                            MItem[0]["TDNL2_HG"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            MItem[0]["TDNL2_HG"] = "合格";
                            mbHggs++;
                        }

                        if ("-1" == MItem[0]["TDNL2"])
                        {
                            MItem[0]["TDNL2_HG"] = "----";
                        }
                    }
                    else
                    {
                        MItem[0]["TDNL2"] = "-1";
                        MItem[0]["TDNL2_HG"] = "----";
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