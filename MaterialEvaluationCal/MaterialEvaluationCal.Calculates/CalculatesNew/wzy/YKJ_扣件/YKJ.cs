using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class YKJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;
            double[] mkyqdArray = new double[3];
            double zj1, zj2;
            bool mAllHg;
            mAllHg = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_YKJ_DJ"];
            var MItem = data["M_YKJ"];
            var mitem = MItem[0];
            var SItem = data["S_YKJ"];
            var jcxm = "";
            var kjlb = "";

            var jcxmBhg = "";
            var jcxmCur = "";

            #endregion

            #region  计算开始
            mitem["JCJGMS"] = "";
            zj1 = 0;
            zj2 = 0;
            foreach (var sitem in SItem)
            {
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.Where(x => x["GGXH"].Contains(sitem["GGXH"].Trim())).ToList();
                if (mrsDj_Filter == null && mrsDj_Filter.Count() > 0)
                {
                    sitem["JCJG"] = "不下结论";
                    mitem["JCJGMS"] = "试件尺寸为空";
                    continue;
                }




                kjlb = "、" + sitem["KJLB"].Replace(',', '、') + "、";
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (sitem["ZJPH"] != "0")
                {
                    sitem["W_ZJKPH"] = "破坏";


                }
                else {

                    sitem["W_ZJKPH"] = "未破坏";
                }

                if (sitem["XZPH"] != "0")
                {
                    sitem["W_XZKPH"] = "破坏";


                }
                else
                {

                    sitem["W_XZKPH"] = "未破坏";
                }


                if (kjlb.Contains("、直角扣件、"))
                {
                    sitem["YPSL"] = sitem["YBDX"];

                    var mrsDj_Filter2 = mrsDj.FirstOrDefault(x => x["YBDX"].Contains(sitem["YPSL"].Trim()) && x["DJYB"].Contains(mitem["DJYB"]));
                    mitem["ZYH"] = mrsDj_Filter2["ZYH"];//主要项AC
                    mitem["ZYB"] = mrsDj_Filter2["ZYB"];//主要项RE
                    mitem["YBH"] = mrsDj_Filter2["YBH"];//一般项AC
                    mitem["YBB"] = mrsDj_Filter2["YBB"];//一般项RE

                    var ZJPJKH7_pj = Math.Round(GetSafeDecimal(sitem["ZJPJKH7"].ToString()), 2).ToString("0.00");
                    var ZJPJKH10_pj = Math.Round(GetSafeDecimal(sitem["ZJPJKH10"].ToString()), 2).ToString("0.00");

                    mitem["HG_ZJKH"] = IsQualified("≤7.00", ZJPJKH7_pj);
                    if (mitem["HG_ZJKH"] == "合格")
                    {
                        mitem["HG_ZJKH"] = IsQualified("≤0.50", ZJPJKH10_pj);
                    }
                    mitem["HG_ZJNZGD"] = IsQualified("≤70.0", sitem["W_ZJNZGD"]);

                    //直角主要项不合格数   
                    int zjzyb = 0;
                    #region 
                    if (sitem.Keys.Contains("ZJLW"))
                    {
                        zjzyb += GetSafeInt(sitem["ZJLW"]);
                    }
                    if (sitem.Keys.Contains("ZJGB"))
                    {
                        zjzyb += GetSafeInt(sitem["ZJGB"]);
                    }
                    if (sitem.Keys.Contains("ZJKH1"))
                    {
                        zjzyb += GetSafeInt(sitem["ZJKH1"]);
                    }
                    if (sitem.Keys.Contains("ZJPH"))
                    {
                        zjzyb += GetSafeInt(sitem["ZJPH"]);
                    }
                    if (sitem.Keys.Contains("ZJNLJ"))
                    {
                        zjzyb += GetSafeInt(sitem["ZJNLJ"]);
                    }
                    if (sitem.Keys.Contains("ZJNZGD"))
                    {
                        zjzyb += GetSafeInt(sitem["ZJNZGD"]);
                    }
                    #endregion
                    sitem["ZJZYB"] = zjzyb.ToString();
                    if (GetSafeDouble(sitem["ZJZYB"]) >= GetSafeDouble(mrsDj_Filter2["ZYB"]))
                        sitem["ZJZYPD"] = "不合格";
                    else
                    {
                        if (GetSafeDouble(sitem["ZJZYB"]) > GetSafeDouble(mrsDj_Filter2["ZYH"]))
                            sitem["ZJZYPD"] = "需取第二样本检测";
                        else
                            sitem["ZJZYPD"] = "合格";
                    }
                    //直角一般项不合格数
                    sitem["ZJYBPD"] = "----";
                    if (jcxm.Contains("、一般项、"))
                    {
                        if (GetSafeDouble(sitem["ZJYBBHGS"]) >= GetSafeDouble(mrsDj_Filter2["YBB"]))
                            sitem["ZJYBPD"] = "不合格";
                        else
                        {
                            if (GetSafeDouble(sitem["ZJYBBHGS"]) > GetSafeDouble(mrsDj_Filter2["YBH"]))
                                sitem["ZJYBPD"] = "需取第二样本检测";
                            else
                                sitem["ZJYBPD"] = "合格";
                        }
                    }
                    if (sitem["ZJZYPD"] == "不合格")
                        mitem["ZJZPD"] = "不合格";
                    else
                    {
                        if (sitem["ZJZYPD"] == "需取第二样本检测" || sitem["ZJYBPD"] == "需取第二样本检测")
                            mitem["ZJZPD"] = "需取第二样本检测";
                        else
                            mitem["ZJZPD"] = "合格";
                    }
                }
                else
                {
                    sitem["ZJZYPD"] = "----";
                    sitem["ZJYBPD"] = "----";
                    sitem["ZJYBSM"] = "----";
                    sitem["ZJZYB"] = "-1";
                    sitem["ZJLW"] = "-1";
                    sitem["ZJGB"] = "-1";
                    sitem["ZJKH1"] = "-1";
                    sitem["ZJKH2"] = "-1";
                    sitem["ZJPH"] = "-1";
                    sitem["ZJNLJ"] = "-1";
                    sitem["ZJNZGD"] = "-1";
                    sitem["ZJYBBHGS"] = "-1";
                    sitem["ZJLWSM"] = "----";
                    sitem["ZJGBSM"] = "----";
                    sitem["ZJKHSM"] = "----";
                    sitem["ZJPHSM"] = "----";
                    sitem["ZJNLJSM"] = "----";
                    sitem["ZJNZGSM"] = "----";
                    sitem["ZJYBSM"] = "----";
                }
                if (kjlb.Contains("、旋转扣件、"))
                {
                    sitem["YPSL1"] = sitem["YBDX"];
                    var mrsDj_Filter2 = mrsDj.FirstOrDefault(x => x["YBDX"].Contains(sitem["YPSL1"].Trim()) && x["DJYB"].Contains(mitem["DJYB"]));
                    mitem["ZYH"] = mrsDj_Filter2["ZYH"];
                    mitem["ZYB"] = mrsDj_Filter2["ZYB"];
                    mitem["YBH"] = mrsDj_Filter2["YBH"];
                    mitem["YBB"] = mrsDj_Filter2["YBB"];

                    var XZPJKH7_pj = Math.Round(GetSafeDecimal(sitem["XZPJKH7"].ToString()), 2).ToString("0.00");
                    var XZPJKH10_pj = Math.Round(GetSafeDecimal(sitem["XZPJKH10"].ToString()), 2).ToString("0.00");

                    mitem["HG_XZKH"] = IsQualified("≤7.00", XZPJKH7_pj);
                    if (mitem["HG_XZKH"] == "合格")
                    {
                        mitem["HG_XZKH"] = IsQualified("≤0.50", XZPJKH10_pj);
                    }
                    #region  旋转主要项不合格数
                    int zjzyb = 0;
                    if (sitem.Keys.Contains("XZLW"))
                    {
                        zjzyb += GetSafeInt(sitem["XZLW"]);
                    }
                    if (sitem.Keys.Contains("XZGB"))
                    {
                        zjzyb += GetSafeInt(sitem["XZGB"]);
                    }
                    if (sitem.Keys.Contains("XZKH1"))
                    {
                        zjzyb += GetSafeInt(sitem["XZKH1"]);
                    }
                    if (sitem.Keys.Contains("XZPH"))
                    {
                        zjzyb += GetSafeInt(sitem["XZPH"]);
                    }
                    if (sitem.Keys.Contains("XZNLJ"))
                    {
                        zjzyb += GetSafeInt(sitem["XZNLJ"]);
                    }
                    sitem["XZZYB"] = (zjzyb).ToString();
                    #endregion  
                    if (GetSafeDouble(sitem["XZZYB"]) >= GetSafeDouble(mrsDj_Filter2["ZYB"]))
                    {
                        sitem["XZZYPD"] = "不合格";
                        mitem["XZZPD"] = "不合格";
                    }
                    else
                    {
                        if (GetSafeDouble(sitem["XZZYB"]) > GetSafeDouble(mrsDj_Filter2["ZYH"]))
                            sitem["XZZYPD"] = "需取第二样本检测";
                        else
                            sitem["XZZYPD"] = "合格";
                    }
                    //旋转一般项不合格数
                    sitem["XZYBPD"] = "----";
                    if (jcxm.Contains("、一般项、"))
                    {
                        if (GetSafeDouble(sitem["XZYBBHGS"]) >= GetSafeDouble(mrsDj_Filter2["YBB"]))
                        {
                            sitem["XZYBPD"] = "不合格";
                            mitem["XZZPD"] = "不合格";
                        }
                        else
                        {
                            if (GetSafeDouble(sitem["XZYBBHGS"]) > GetSafeDouble(mrsDj_Filter2["YBH"]))
                                sitem["XZYBPD"] = "需取第二样本检测";
                            else
                                sitem["XZYBPD"] = "合格";
                        }
                    }

                    if (sitem["XZZYPD"] == "不合格" || sitem["XZYBPD"] == "不合格")
                        mitem["XZZPD"] = "不合格";
                    else
                    {
                        if (sitem["XZZYPD"] == "需取第二样本检测" || sitem["XZYBPD"] == "需取第二样本检测")
                            mitem["XZZPD"] = "需取第二样本检测";
                        else
                            mitem["XZZPD"] = "合格";
                    }
                }
                else
                {
                    sitem["XZZYPD"] = "----";
                    sitem["XZYBPD"] = "----";
                    sitem["XZYBSM"] = "----";
                    sitem["XZZYB"] = "-1";
                    sitem["XZLW"] = "-1";
                    sitem["XZGB"] = "-1";
                    sitem["XZKH1"] = "-1";
                    sitem["XZKH2"] = "-1";
                    sitem["XZPH"] = "-1";
                    sitem["XZNLJ"] = "-1";
                    sitem["XZYBBHGS"] = "-1";
                    sitem["XZLWSM"] = "----";
                    sitem["XZGBSM"] = "----";
                    sitem["XZKHSM"] = "----";
                    sitem["XZPHSM"] = "----";
                    sitem["XZNLJSM"] = "----";
                }
                if (kjlb.Contains("、对接扣件、"))
                {
                    sitem["YPSL2"] = sitem["YBDX"];
                    var mrsDj_Filter2 = mrsDj.FirstOrDefault(x => x["YBDX"].Contains(sitem["YPSL2"].Trim()) && x["DJYB"].Contains(mitem["DJYB"]));
                    mitem["ZYH"] = mrsDj_Filter2["ZYH"];
                    mitem["ZYB"] = mrsDj_Filter2["ZYB"];
                    mitem["YBH"] = mrsDj_Filter2["YBH"];
                    mitem["YBB"] = mrsDj_Filter2["YBB"];


                    #region  旋转主要项不合格数
                    int zjzyb = 0;
                    if (sitem.Keys.Contains("DJLW"))
                    {
                        zjzyb += GetSafeInt(sitem["DJLW"]);
                    }
                    if (sitem.Keys.Contains("DJGB"))
                    {
                        zjzyb += GetSafeInt(sitem["DJGB"]);
                    }
                    if (sitem.Keys.Contains("DJKL"))
                    {
                        zjzyb += GetSafeInt(sitem["DJKL"]);
                    }
                    if (sitem.Keys.Contains("DJNLJ"))
                    {
                        zjzyb += GetSafeInt(sitem["DJNLJ"]);
                    }
                    sitem["DJZYB"] = (zjzyb).ToString();
                    #endregion  

                    if (GetSafeDouble(sitem["DJZYB"]) >= GetSafeDouble(mrsDj_Filter2["ZYB"]))
                        sitem["DJZYPD"] = "不合格";
                    else
                    {
                        if (GetSafeDouble(sitem["DJZYB"]) > GetSafeDouble(mrsDj_Filter2["ZYH"]))
                            sitem["DJZYPD"] = "需取第二样本检测";
                        else
                            sitem["DJZYPD"] = "合格";
                    }
                    sitem["DJYBPD"] = "----";
                    if (jcxm.Contains("、一般项、"))
                    {
                        if (GetSafeDouble(sitem["DJYBBHGS"]) >= GetSafeDouble(mrsDj_Filter2["YBB"]))
                            sitem["DJYBPD"] = "不合格";
                        else
                        {
                            if (GetSafeDouble(sitem["DJYBBHGS"]) > GetSafeDouble(mrsDj_Filter2["YBH"]))
                                sitem["DJYBPD"] = "需取第二样本检测";
                            else
                                sitem["DJYBPD"] = "合格";
                        }
                    }

                    if (sitem["DJZYPD"] == "不合格" || sitem["DJYBPD"] == "不合格")
                        mitem["DJZPD"] = "不合格";
                    else
                    {
                        if (sitem["DJZYPD"] == "需取第二样本检测" || sitem["DJYBPD"] == "需取第二样本检测")
                            mitem["DJZPD"] = "需取第二样本检测";
                        else
                            mitem["DJZPD"] = "合格";
                    }
                }
                else
                {
                    sitem["DJZYPD"] = "----";
                    sitem["DJYBPD"] = "----";
                    sitem["DJYBSM"] = "----";
                    sitem["DJZYB"] = "-1";
                    sitem["DJLW"] = "-1";
                    sitem["DJGB"] = "-1";
                    sitem["DJKL"] = "-1";
                    sitem["DJNLJ"] = "-1";
                    sitem["DJYBBHGS"] = "-1";
                    sitem["DJYBSM"] = "----";
                    sitem["DJLWSM"] = "----";
                    sitem["DJGBSM"] = "----";
                    sitem["DJKLSM"] = "----";
                    sitem["DJNLJSM"] = "----";
                }
            }
            //综合判断
            mitem["JCJGMS"] = "";
            string mjgsm = "";
            if (mitem["ZJZPD"] == "不合格" || mitem["XZZPD"] == "不合格" || mitem["DJZPD"] == "不合格")  //判断是否合格
            {
                mAllHg = false;
                if (mitem["ZJZPD"] == "不合格")
                    mjgsm += "直角扣件、";
                if (mitem["XZZPD"] == "不合格")
                {
                    mjgsm += "旋转扣件、";
                }
                if (mitem["DJZPD"] == "不合格")
                {
                    mjgsm += "对接扣件、";
                }

                if (mitem["ZJZPD"] == "不合格" || mitem["XZZPD"] == "不合格" || mitem["DJZPD"] == "不合格")
                {
                    mitem["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + mjgsm.TrimEnd('、') + "不符合要求。";

                }
            }
            else
            {
                if (mitem["ZJZPD"] == "需取第二样本检测" || mitem["XZZPD"] == "需取第二样本检测" || mitem["DJZPD"] == "需取第二样本检测")  //判断是否合格
                {
                    mAllHg = false;
                    if (mitem["ZJZPD"] == "需取第二样本检测")
                        mjgsm = "直角扣件、";
                    if (mitem["XZZPD"] == "需取第二样本检测")
                    {
                        mjgsm += "旋转扣件、";
                    }
                    if (mitem["DJZPD"] == "需取第二样本检测")
                    {
                        mjgsm += "对接扣件、";
                    }

                    if (mitem["JCJGMS"].Trim().Length > 0)
                    {
                        mitem["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + mjgsm.TrimEnd('、') + "不符合要求，需取第二样本检测。";
                    }
                }
                else
                {
                    mAllHg = true;
                    mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目均符合要求。";
                }
            }
            //主表总判断赋值
            if (mAllHg)
                mitem["JCJG"] = "合格";
            else
                mitem["JCJG"] = "不合格";
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
