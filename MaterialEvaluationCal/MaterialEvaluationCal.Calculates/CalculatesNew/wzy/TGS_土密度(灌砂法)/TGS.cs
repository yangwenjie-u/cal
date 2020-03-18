using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TGS : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            double[] mkyqdArray = new double[6];
            string[] mtmpArray;
            double mSjcc, mSjcc1, mMj;
            double mMaxKyqd, mMinKyqd, mAvgKyqd;
            double mskys, msktj, msyhsl, msymd, msygmd, msyzd;
            string mSjdjbh, mSjdj;
            double mSz, mQdyq, mhsxs;
            int vp, vi;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            int a, aa, aaa, I, i1;
            bool mSFwc;
            mSFwc = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_TGS_DJ"];
            var MItem = data["M_TGS"];
            var mitem = MItem[0];
            var SItem = data["S_TGS"];
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            vi = 0;
            mitem["JCJGMS"] = "";
            foreach (var sitem in SItem)
            {
                sitem["GCBW"] = mitem["GCBW"];//报告展示用
                double md;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    int mbhggs = 0;
                    int xd;
                    double msmd;
                    if (jcxm.Contains("、干密度、") || jcxm.Contains("、压实度、"))
                    {
                        if (Conversion.Val(sitem["ZDGMD"]) == 0)
                            sitem["YSD"] = "0";
                        else
                            sitem["YSD"] = Round(100 * Conversion.Val(sitem["GMD"]) / Conversion.Val(sitem["ZDGMD"]), 0).ToString("");
                        mskys = Round(Conversion.Val(sitem["RQYS"]) - Conversion.Val(sitem["RQSS"]) - Conversion.Val(sitem["ZDYSL"]), 0);
                        msktj = Round(mskys / Conversion.Val(sitem["BZSMD"]), 1);
                        sitem["SKYS"] = mskys.ToString();
                        sitem["SKTJ"] = msktj.ToString("0.0");
                        msmd = Round(Conversion.Val(sitem["QBCL"]) / msktj, 2);   //湿密度计算
                        sitem["SMD"] = msmd.ToString("0.00");
                        if (Conversion.Val(sitem["SJYSD"]) == -1 && Conversion.Val(sitem["ZDGMD"]) > 0)
                        {
                            if (Conversion.Val(sitem["GMD"]) >= Conversion.Val(sitem["ZDGMD"]))
                                vi = vi + 1;
                        }
                        else
                        {
                            if (Conversion.Val(sitem["YSD"]) >= Conversion.Val(sitem["SJYSD"]))
                                vi = vi + 1;
                        }
                        sitem["JCJG"] = "----";
                    }
                    else
                    {
                        sitem["PJHSL"] = "----";
                        sitem["GMD"] = "----";
                        sitem["YSD"] = "----";
                    }
                    sitem["JCJG"] = "----";
                }
                else
                {
                    if (jcxm.Contains("、干密度、") || jcxm.Contains("、压实度、"))
                    {
                        //计算单组的抗压强度,并进行合格判断
                        if (Conversion.Val(sitem["ZDGMD"]) == 0 || Conversion.Val(sitem["ZDGMD"]) == -1 || string.IsNullOrEmpty(sitem["ZDGMD"]))
                        {
                            sitem["ZDGMD"] = "-1";
                            sitem["GMD"] = "0";
                        }
                        if (Conversion.Val(sitem["SJYSD"]) == 0 || Conversion.Val(sitem["SJYSD"]) == -1 || string.IsNullOrEmpty(sitem["SJYSD"]))
                        {
                            sitem["SJYSD"] = "-1";
                            sitem["YSD"] = "0";
                        }
                        //If (Val(sitem["ZDGMD)) = -1 AND (VAL(SITEM["sjysd)) = -1 Then MsgBox "错误：干密度设计值和压实度设计值不能同时为-1"
                        mskys = Round((Conversion.Val(sitem["rqys"]) - Conversion.Val(sitem["RQSS"]) - Conversion.Val(sitem["ZDYSL"])), 0);
                        msktj = Round((mskys / (Conversion.Val(sitem["BZSMD"]))), 1);
                        sitem["SKYS"] = mskys.ToString();
                        sitem["SKTJ"] = msktj.ToString("0.0");
                        double msmd = Round((Conversion.Val(sitem["QBCL"]) / msktj), 2); //湿密度计算
                        sitem["SMD"] = msmd.ToString("0.00");
                        md = Conversion.Val(sitem["HJST1"]) - Conversion.Val(sitem["HJGT1"]);
                        md = Round((md), 2);
                        sitem["SZL1"] = md.ToString("0.00");

                        md = (Conversion.Val(sitem["HJST2"]) - Conversion.Val(sitem["HJGT2"]));
                        md = Round((md), 2);
                        sitem["SZL2"] = md.ToString("0.00");
                        sitem["GTZL1"] = (Conversion.Val(sitem["HJGT1"]) - Conversion.Val(sitem["HZL1"])).ToString("0"); //干土质量计算
                        sitem["GTZL2"] = (Conversion.Val(sitem["HJGT2"]) - Conversion.Val(sitem["HZL2"])).ToString("0");
                        if (Conversion.Val(sitem["GTZL1"]) != 0 && Conversion.Val(sitem["GTZL2"]) != 0)
                        {
                            sitem["HSL1"] = Round((Conversion.Val(sitem["SZL1"]) / Conversion.Val(sitem["GTZL1"])) * 100, 1).ToString("0.0"); //含水量计算
                            sitem["HSL2"] = Round((Conversion.Val(sitem["SZL2"]) / Conversion.Val(sitem["GTZL2"])) * 100, 1).ToString("0.0");
                        }

                        sitem["PJHSL"] = Round((Conversion.Val(sitem["HSL1"]) + Conversion.Val(sitem["HSL2"])) / 2, 1).ToString("0.0"); //平均含水量

                        if (Conversion.Val(sitem["HSL1"]) >= 40 && Conversion.Val(sitem["HSL2"]) >= 40)
                        {
                            if (Math.Abs((Conversion.Val(sitem["HSL1"]) - Conversion.Val(sitem["HSL2"]))) > 2)
                                sitem["PJHSL"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs((Conversion.Val(sitem["HSL1"]) - Conversion.Val(sitem["HSL2"]))) > 1)
                                sitem["PJHSL"] = "无效";

                        }
                        //double mgmd = Round((msmd / (1 + 0.01 * Conversion.Val(sitem["PJHSL"]))), 2); //干密度计算
                        //sitem["GMD"] = mgmd.ToString("0.00");

                        if (Conversion.Val(sitem["ZDGMD"]) != 0)
                        {
                            double mysd = Round(100 * (Conversion.Val(sitem["GMD"]) / Conversion.Val(sitem["ZDGMD"])), 0);
                            sitem["YSD"] = mysd.ToString("0");
                        }
                        else
                            sitem["YSD"] = "0";
                        if (Conversion.Val(sitem["SJYSD"]) == -1 && (Conversion.Val(sitem["ZDGMD"])) > 0)
                        {
                            if (Conversion.Val(sitem["GMD"]) >= Conversion.Val(sitem["ZDGMD"]))
                                vi = vi + 1;
                        }
                        else
                        {
                            if (Conversion.Val(sitem["YSD"]) >= Conversion.Val(sitem["SJYSD"]))
                                vi = vi + 1;
                        }

                    }
                    else
                    {
                        sitem["PJHSL"] = "----";
                        sitem["YSD"] = "----";
                    }
                }
            }
            int c_Ht = SItem.Count();
            mitem["HGDS"] = vi.ToString();
            mitem["JCDS"] = c_Ht.ToString();
            mitem["HGL"] = Round((vi / c_Ht) * 100, 1).ToString();
            string mhgl = Round(GetSafeDouble(mitem["HGL"]), 1).ToString("0.0");
            if (vi == c_Ht)
                mAllHg = true;
            else
                mAllHg = false;
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