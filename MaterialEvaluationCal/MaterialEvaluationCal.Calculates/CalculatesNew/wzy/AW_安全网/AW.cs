using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class AW : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            double[] mkyqdArray = new double[3];
            string mJSFF;
            bool mAllHg;
            mAllHg = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_AW_DJ"];
            var MItem = data["M_AW"];
            var mitem = MItem[0];
            var SItem = data["S_AW"];
            #endregion

            #region  计算开始
            mitem["JCJGMS"] = "";
            foreach (var sitem in SItem)
            {
                string g_cd = ""; //密目网长度要求，不小于2M
                string kyhkkj = ""; //开眼环扣孔径
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["YBDX"] == sitem["YBDX"].Trim() && x["MC"] == sitem["WLX"].Trim());
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    sitem["NCJYQ"] = mrsDj_Filter["NCJXN"];
                    sitem["NGCYQ"] = mrsDj_Filter["NGCXN"];
                    sitem["DLQDSCLYQ"] = mrsDj_Filter["DLQLSCL"];
                    sitem["JFBWKLYQ"] = mrsDj_Filter["JFKLQK"];
                    mitem["ZYH"] = mrsDj_Filter["ZYH"];
                    mitem["ZYB"] = mrsDj_Filter["ZYB"];
                    mitem["YBH"] = mrsDj_Filter["YBH"];
                    mitem["YBB"] = mrsDj_Filter["YBB"];
                    mitem["G_ZL"] = mrsDj_Filter["ZL"]; //质量
                    mitem["G_CC"] = mrsDj_Filter["KD"]; //宽度
                    mitem["G_CCYXPC"] = mrsDj_Filter["CCYXPC"]; //宽高度允许偏差
                    g_cd = mrsDj_Filter["CD"]; //密目网长度要求，不小于2M
                    kyhkkj = mrsDj_Filter["KYHKKJ"];  //开眼环扣孔径
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                sitem["BPD"] = "----";
                sitem["BLB"] = "-1";
                if (sitem["WLX"].Trim() == "平网" || sitem["WLX"].Trim() == "立网")
                {
                    sitem["ALB"] = sitem["NCJB"];
                    sitem["BLB"] = "0";
                    if (GetSafeDouble(sitem["ALB"]) >= GetSafeDouble(mrsDj_Filter["ZYB"]))
                        sitem["APD"] = "不合格";
                    if (GetSafeDouble(sitem["ALB"]) <= GetSafeDouble(mrsDj_Filter["ZYH"]))
                        sitem["APD"] = "合格";
                    if (GetSafeDouble(sitem["BLB"]) >= GetSafeDouble(mrsDj_Filter["YBB"]))
                        sitem["BPD"] = "不合格";
                    if (GetSafeDouble(sitem["BLB"]) <= GetSafeDouble(mrsDj_Filter["YBH"]))
                        sitem["BPD"] = "合格";
                }
                if (sitem["WLX"].Contains("密目式安全立网"))
                {
                    sitem["ALB"] = (GetSafeDouble(sitem["NCJB"]) + GetSafeDouble(sitem["NGCB"]) + GetSafeDouble(sitem["DLQDSCLB"]) + GetSafeDouble(sitem["JFBWKLB"])).ToString();
                    sitem["BLB"] = "0";
                    if (GetSafeDouble(sitem["ALB"]) >= GetSafeDouble(mrsDj_Filter["ZYB"]))
                        sitem["APD"] = "不合格";
                    if (GetSafeDouble(sitem["ALB"]) <= GetSafeDouble(mrsDj_Filter["ZYH"]))
                        sitem["APD"] = "合格";
                    if (GetSafeDouble(sitem["BLB"]) >= GetSafeDouble(mrsDj_Filter["YBB"]))
                        sitem["BPD"] = "不合格";
                    if (GetSafeDouble(sitem["BLB"]) <= GetSafeDouble(mrsDj_Filter["YBH"]))
                        sitem["BPD"] = "合格";
                }

                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、耐冲击性能、"))
                {
                    if (sitem["NCJSM"].Trim() == "未破裂、孔洞满足要求")
                    {
                        MItem[0]["HG_NCJXN"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_NCJXN"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["NCJYQ"] = "----";
                    sitem["NCJPD"] = "----";
                }
                if (jcxm.Contains("、耐贯穿性能、"))
                {
                    if (sitem["NGCSM"].Trim() == "未贯穿")
                    {
                        MItem[0]["HG_NGCXN"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_NGCXN"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["NGCYQ"] = "----";
                    sitem["NGCPD"] = "----";
                }


                if (jcxm.Contains("、断裂强力*断裂伸长、"))
                { }
                else
                {
                    sitem["DLQDSCLYQ"] = "----";
                    sitem["DLQDSCLPD"] = "----";
                }
                if (jcxm.Contains("、接缝部位抗拉强力、"))
                { }
                else
                {
                    sitem["JFBWKLYQ"] = "----";
                    sitem["JFBWKLPD"] = "----";
                }

                #region 质量
                if (jcxm.Contains("、质量、"))
                {
                    if (IsQualified(mitem["G_ZL"], sitem["ZL"], false) == "合格")
                    {
                        MItem[0]["HG_ZL"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_ZL"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    mitem["G_ZL"] = "----";
                    mitem["HG_ZL"] = "----";
                    sitem["ZL"] = "----";
                }
                #endregion

                #region 外观尺寸
                if (jcxm.Contains("、外观尺寸、"))
                {
                    //double pc = 0; //尺寸偏差
                    //if (GetSafeDouble(sitem["BCGGCC"]) > 0)
                    //{
                    //    pc = (GetSafeDouble(sitem["KD"]) - GetSafeDouble(sitem["BCGGCC"])) / GetSafeDouble(sitem["BCGGCC"]) * 100;
                    //}
                    //else
                    //{
                    //    pc = 100;
                    //}


                    //if (IsQualified(mitem["G_CC"], sitem["KD"], false) == "合格" && IsQualified(mitem["G_CCYXPC"], pc.ToString(), false) == "合格")
                    //{
                    //    MItem[0]["HG_ZL"] = "合格";
                    //}
                    //else
                    //{
                    //    MItem[0]["HG_ZL"] = "不合格";
                    //    mAllHg = false;
                    //}
                    if (MItem[0]["HG_CC"] == "不合格")
                    {
                        mAllHg = false;
                    }
                }
                else
                {
                    sitem["KD"] = "----";
                    mitem["G_CCYXPC"] = "----";
                    sitem["BCGGCC"] = "----";
                }
                #endregion

                #region 一般要求
                if (jcxm.Contains("、一般要求、"))
                {
                    bool hg = true; //是否合格（一般要求）
                    if (sitem["YBYQ1"].Trim() != "无漏缝、缝均匀")
                    {
                        hg = false;
                    }
                    if (sitem["YBYQ2"].Trim() != "无缝接")
                    {
                        hg = false;
                    }
                    if (sitem["YBYQ3"].Trim() != "无断纱、破洞")
                    {
                        hg = false;
                    }
                    if (sitem["YBYQ4"].Trim() != "开眼环牢固")
                    {
                        hg = false;
                    }

                    //密目网宽度和长度
                    if (IsQualified(mitem["G_CC"], sitem["MMWKD"], false) != "合格" && IsQualified(g_cd, sitem["CD"], false) != "合格")
                    {
                        hg = false;
                    }

                    //开眼环扣孔径
                    if (IsQualified(kyhkkj, sitem["KYHKKJ"]) != "合格")
                    {
                        hg = false;
                    }

                    if (hg)
                    {
                        mitem["HG_YBYQ"] = "合格";
                    }
                    else
                    {
                        mitem["HG_YBYQ"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    mitem["HG_YBYQ"] = "----";
                }
                #endregion

                //if (sitem["APD"] == "不合格" || sitem["BPD"] == "不合格")
                //{
                //    sitem["JCJG"] = "不合格";
                //    mAllHg = false;
                //}
                //else
                //{
                //    sitem["JCJG"] = "合格";
                //    mAllHg = true;
                //}
            }
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
