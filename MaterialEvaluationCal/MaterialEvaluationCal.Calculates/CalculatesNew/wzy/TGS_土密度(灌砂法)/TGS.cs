using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TGS:BaseMethods
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
                double md;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    int mbhggs = 0;
                    int xd;
                    double msmd;
                    if (jcxm.Contains("、干密度、"))
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
                    if (jcxm.Contains("、干密度、"))
                    {
                        //计算单组的抗压强度,并进行合格判断
                        if (Conversion.Val(sitem["zdgmd"]) == 0 || Conversion.Val(sitem["zdgmd"]) == -1 || string.IsNullOrEmpty(sitem["zdgmd"]))
                        {
                            sitem["zdgmd"] = "-1";
                            sitem["gmd"] = "0";
                        }
                        if (Conversion.Val(sitem["sjysd"]) == 0 || Conversion.Val(sitem["sjysd"]) == -1 || string.IsNullOrEmpty(sitem["sjysd"]))
                        {
                            sitem["sjysd"] = "-1";
                            sitem["ysd"] = "0";
                        }
                        //If (Val(sitem["zdgmd)) = -1 And (Val(sitem["sjysd)) = -1 Then MsgBox "错误：干密度设计值和压实度设计值不能同时为-1"
                        mskys = Round((Conversion.Val(sitem["rqys"]) - Conversion.Val(sitem["rqss"]) - Conversion.Val(sitem["zdysl"])), 0);
                        msktj = Round((mskys / (Conversion.Val(sitem["bzsmd"]))), 1);
                        sitem["skys"] = mskys.ToString();
                        sitem["sktj"] = msktj.ToString("0.0");
                        double msmd = Round((Conversion.Val(sitem["qbcl"]) / msktj), 2); //湿密度计算
                        sitem["smd"] = msmd.ToString("0.00");
                        md = Conversion.Val(sitem["hjst1"]) - Conversion.Val(sitem["hjgt1"]);
                        md = Round((md), 2);
                        sitem["szl1"] = md.ToString("0.00");

                        md = (Conversion.Val(sitem["hjst2"]) - Conversion.Val(sitem["hjgt2"]));
                        md = Round((md), 2);
                        sitem["szl2"] = md.ToString("0.00");
                        sitem["gtzl1"] = (Conversion.Val(sitem["hjgt1"]) - Conversion.Val(sitem["hzl1"])).ToString(); //干土质量计算
                        sitem["gtzl2"] = (Conversion.Val(sitem["hjgt2"]) - Conversion.Val(sitem["hzl2"])).ToString();
                        if (Conversion.Val(sitem["gtzl1"]) != 0 && Conversion.Val(sitem["gtzl2"]) != 0)
                        {
                            sitem["hsl1"] = Round((Conversion.Val(sitem["szl1"]) / Conversion.Val(sitem["gtzl1"])) * 100, 1).ToString("0.0"); //含水量计算
                            sitem["hsl2"] = Round((Conversion.Val(sitem["szl2"]) / Conversion.Val(sitem["gtzl2"])) * 100, 1).ToString("0.0");
                        }

                        sitem["pjhsl"] = Round((Conversion.Val(sitem["hsl1"]) + Conversion.Val(sitem["hsl2"])) / 2, 1).ToString("0.0"); //平均含水量

                        if (Conversion.Val(sitem["hsl1"]) >= 40 && Conversion.Val(sitem["hsl2"]) >= 40)
                        {
                            if (Math.Abs((Conversion.Val(sitem["hsl1"]) - Conversion.Val(sitem["hsl2"]))) > 2)
                                sitem["pjhsl"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs((Conversion.Val(sitem["hsl1"]) - Conversion.Val(sitem["hsl2"]))) > 1)
                                sitem["pjhsl"] = "无效";

                        }
                        double mgmd = Round((msmd / (1 + 0.01 * Conversion.Val(sitem["pjhsl"]))), 2); //干密度计算
                        sitem["gmd"] = mgmd.ToString("0.00");

                        if (Conversion.Val(sitem["zdgmd"]) != 0)
                        {
                            double mysd = Round(100 * (Conversion.Val(sitem["gmd"]) / Conversion.Val(sitem["zdgmd"])), 0);
                            sitem["ysd"] = mysd.ToString("0");
                        }
                        else
                            sitem["ysd"] = "0";
                        if (Conversion.Val(sitem["sjysd"]) == -1 && (Conversion.Val(sitem["zdgmd"])) > 0)
                        {
                            if (Conversion.Val(sitem["gmd"]) >= Conversion.Val(sitem["zdgmd"]))
                                vi = vi + 1;
                        }
                        else
                        {
                            if (Conversion.Val(sitem["ysd"]) >= Conversion.Val(sitem["sjysd"]))
                                vi = vi + 1;
                        }

                    }
                    else
                    {
                        sitem["pjhsl"] = "----";
                        sitem["ysd"] = "----";
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