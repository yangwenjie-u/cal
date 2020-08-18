using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculates
{
    public class TL : BaseMethods
    {
        public void Calc()
        {
            #region  参数定义
            double[] mkyqdArray = new double[3];
            int mbhggs;
            string dCpmc, dLx, dZf, dDj, dBzh, mSjdj;
            string mJSFF;
            bool mAllHg = true;
            int QDJSFF = 0;
            bool mFlag_Hg, mFlag_Bhg = false;
            var data = retData;
            var mrsDj = dataExtra["BZ_TL_DJ"];
            var MItem = data["M_TL"];
            var mitem = MItem[0];
            var SItem = data["S_TL"];
            //  计算开始
            mitem["JCJGMS"] = "";
            mSjdj = "";
            var jcxmBhg = "";
            var jcxmCur = "";

            //int QDJSFF = 0;
            foreach (var sitem in SItem)
            {
                dCpmc = string.IsNullOrEmpty(sitem["CPMC"]) ? "" : sitem["CPMC"].Trim();
                dLx = string.IsNullOrEmpty(sitem["LX"]) ? "" : sitem["LX"].Trim();
                dDj = string.IsNullOrEmpty(sitem["DJ"]) ? "" : sitem["DJ"].Trim();
                dZf = string.IsNullOrEmpty(sitem["ZF"]) ? "" : sitem["ZF"].Trim();
                dBzh = string.IsNullOrEmpty(sitem["BZH"]) ? "" : sitem["BZH"].Trim();
                if (dDj != "----")
                    mSjdj = mSjdj + dDj;
                if (dZf != "----")
                    mSjdj = mSjdj + dZf;
                if (dLx != "----")
                    mSjdj = mSjdj + dLx;
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_item = mrsDj.FirstOrDefault(x => x["MC"].Contains(dCpmc) && x["LX"].Contains(dLx) && x["DJ"].Contains(dDj) && x["ZF"].Contains(dZf) && x["BZH"].Contains(dBzh));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    mJSFF = string.IsNullOrEmpty(mrsDj_item["JSFF"]) ? "" : mrsDj_item["JSFF"].Trim().ToLower();
                    QDJSFF = string.IsNullOrEmpty(mrsDj_item["G_QDJSFF"]) ? 1 : GetSafeInt(mrsDj_item["G_QDJSFF"]);
                    sitem["CQS"] = string.IsNullOrEmpty(mrsDj_item["G_QDJSFF"]) ? "1" : mrsDj_item["G_QDJSFF"];
                    string which = mrsDj_item["WHICH"];
                    mitem["G_RQZZT"] = mrsDj_item["G_RQZZT"];
                    mitem["G_GTHL"] = mrsDj_item["G_GTHL"];
                    mitem["G_BGSJ"] = mrsDj_item["G_BGSJ"];
                    mitem["G_SGSJ"] = mrsDj_item["G_SGSJ"];
                    mitem["G_TMWG"] = mrsDj_item["G_TMWG"];
                    mitem["G_TCHD"] = mrsDj_item["G_TCHD"];
                    mitem["G_SGX"] = mrsDj_item["G_SGX"];
                    mitem["G_NJQD"] = mrsDj_item["G_NJQD"];
                    mitem["G_JNJQD"] = mrsDj_item["G_JNJQD"];
                    mitem["G_RSCL"] = mrsDj_item["G_RSCL"];
                    mitem["G_SCL"] = mrsDj_item["G_SCL"];
                    mitem["G_JRSSL"] = mrsDj_item["G_JRSSL"];
                    mitem["G_JRSSL2"] = mrsDj_item["G_JRSSL2"];
                    mitem["G_KYQD"] = mrsDj_item["G_KYQD"];
                    mitem["G_GMD"] = mrsDj_item["G_GMD"];
                    mitem["G_BTSX"] = mrsDj_item["G_BTSX"];
                    mitem["G_NSX"] = mrsDj_item["G_NSX"];
                    mitem["G_NJX"] = mrsDj_item["G_NJX"];
                    mitem["G_NXSX"] = mrsDj_item["G_NXSX"];
                    mitem["G_NWBX"] = mrsDj_item["G_NWBX"];
                    mitem["G_DWWDX"] = mrsDj_item["G_DWWDX"];
                    mitem["G_DWRD"] = mrsDj_item["G_DWRD"];
                    mitem["G_NRD"] = mrsDj_item["G_NRD"];
                    mitem["G_DWWZ"] = mrsDj_item["G_DWWZ"];

                    //施工性
                    mitem["G_SGX"] = mrsDj_item["G_SGX"];

                    //打磨性
                    mitem["G_DMX"] = mrsDj_item["G_DMX"];

                    mitem["G_DNJQD"] = mrsDj_item["G_DNJQD"];

                    //吸水量
                    mitem["G_XSL"] = mrsDj_item["G_XSL"];
                    //低温成膜性
                    mitem["G_DWCMX"] = mrsDj_item["G_DWCMX"];
                    //耐碱性
                    mitem["G_NJX"] = mrsDj_item["G_NJX"];
                    //对比率
                    mitem["G_DBL"] = mrsDj_item["G_DBL"];
                    //耐沾污性
                    mitem["G_NZWX"] = mrsDj_item["G_NZWX"];
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = "找不到对应的等级";
                }

                mbhggs = 0;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                mFlag_Hg = false;
                mFlag_Bhg = false;
                mbhggs = 0;


                if (jcxm.Contains("、容器中状态、"))
                {
                    jcxmCur = "容器中状态";
                    if ("符合" != sitem["HG_RQZZT"] && "合格" != sitem["HG_RQZZT"])
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["RQZZT"] = "----";
                    sitem["HG_RQZZT"] = "----";
                    mitem["G_RQZZT"] = "----";
                }

                if (jcxm.Contains("、施工性、"))
                {
                    jcxmCur = "施工性";
                    if ("符合" != sitem["HG_SGX"] && "合格" != sitem["HG_SGX"])
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["SGX"] = "----";
                    sitem["HG_SGX"] = "----";
                    mitem["G_SGX"] = "----";
                }

                if (jcxm.Contains("、干燥时间(表干)、") || jcxm.Contains("、表干时间、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "表干时间,干燥时间(表干)");

                    sitem["BGSJ"] = GetSafeDouble(sitem["BGSJ"]).ToString("0.0");

                    sitem["HG_BGSJ"] = IsQualified(mitem["G_BGSJ"], sitem["BGSJ"], false);
                    if (sitem["HG_BGSJ"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; 
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["BGSJ"] = "----";
                    sitem["HG_BGSJ"] = "----";
                    mitem["G_BGSJ"] = "----";
                }

                if (jcxm.Contains("、干燥时间(实干)、") || jcxm.Contains("、实干时间、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "实干时间,干燥时间(表干)");

                    sitem["HG_SGSJ"] = IsQualified(mitem["G_SGSJ"], sitem["SGSJ"], false);
                    mbhggs = sitem["HG_SGSJ"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (sitem["HG_SGSJ"] != "不合格")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                }
                else
                {
                    sitem["SGSJ"] = "----";
                    sitem["HG_SGSJ"] = "----";
                    mitem["G_SGSJ"] = "----";
                }

                if (jcxm.Contains("、耐水性、") || jcxm.Contains("、耐水性(96h)、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "耐水性,耐水性(96h)");
                    if ("符合" != sitem["HG_NSX"] && "合格" != sitem["HG_NSX"])
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["NSX"] = "----";
                    sitem["HG_NSX"] = "----";
                    mitem["G_NSX"] = "----";
                }

                if (jcxm.Contains("、耐碱性(48h)、") || jcxm.Contains("、耐碱性、") || jcxm.Contains("、耐碱性(24h)、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "耐碱性,耐碱性(96h),耐碱性(24h)");

                    if ("合格" != sitem["HG_NJX"] && sitem["HG_NJX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["NJX"] = "----";
                    sitem["HG_NJX"] = "----";
                    mitem["G_NJX"] = "----";
                }

                if (jcxm.Contains("、低温稳定性、") || jcxm.Contains("、低温稳定性(3次循环)、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "低温稳定性,低温稳定性(3次循环)");

                    //sitem["HG_DWWDX"] = IsQualified(mitem["G_DWWDX"], sitem["DWWDX"]);
                    if ("合格" != sitem["HG_DWWDX"] && sitem["HG_DWWDX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["DWWDX"] = "----";
                    sitem["HG_DWWDX"] = "----";
                    mitem["G_DWWDX"] = "----";
                }


                if (jcxm.Contains("、涂膜外观、"))
                {
                    jcxmCur = "涂膜外观";
                    //如果Ⅰ型 《=0.3 Ⅱ型《=0.5
                    if ("合格" != sitem["HG_TMWG"] && sitem["HG_TMWG"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["TMWG"] = "----";
                    sitem["HG_TMWG"] = "----";
                    mitem["G_TMWG"] = "----";
                }

                if (jcxm.Contains("、涂层耐温变性、") || jcxm.Contains("、涂层耐温变性(5次循环)、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "涂层耐温变性,涂层耐温变性(5次循环)");

                    if ("合格" != sitem["HG_NWBX"] && sitem["HG_NWBX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["NWBX"] = "----";
                    sitem["HG_NWBX"] = "----";
                    mitem["G_NWBX"] = "----";
                }

                if (jcxm.Contains("、耐洗涮性、"))
                {
                    jcxmCur = "耐洗涮性";

                    if ("合格" != sitem["HG_NXSX"] && sitem["HG_NXSX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["NXSX"] = "----";
                    sitem["HG_NXSX"] = "----";
                    mitem["G_NXSX"] = "----";
                }

                if (jcxm.Contains("、耐沾污性、"))
                {
                    jcxmCur = "耐沾污性";

                    double ys1 = (GetSafeDouble(sitem["ysfsxs1"]) + GetSafeDouble(sitem["ysfsxs2"]) + GetSafeDouble(sitem["ysfsxs3"]) / 3);
                    double ys2 = (GetSafeDouble(sitem["ysfsxs4"]) + GetSafeDouble(sitem["ysfsxs5"]) + GetSafeDouble(sitem["ysfsxs6"]) / 3);
                    double ys3 = (GetSafeDouble(sitem["ysfsxs7"]) + GetSafeDouble(sitem["ysfsxs8"]) + GetSafeDouble(sitem["ysfsxs9"]) / 3);

                    double zw1 = (GetSafeDouble(sitem["zwhfsxs1"]) + GetSafeDouble(sitem["zwhfsxs2"]) + GetSafeDouble(sitem["zwhfsxs3"])/3);
                    double zw2 = (GetSafeDouble(sitem["zwhfsxs4"]) + GetSafeDouble(sitem["zwhfsxs5"]) + GetSafeDouble(sitem["zwhfsxs6"]) / 3);
                    double zw3 = (GetSafeDouble(sitem["zwhfsxs7"]) + GetSafeDouble(sitem["zwhfsxs8"]) + GetSafeDouble(sitem["zwhfsxs9"]) / 3);


                    sitem["NZWX"] = (((((ys1 - zw1) / ys1) + ((ys2 - zw2) / ys2) + ((ys3 - zw3) / ys3)) / 3) * 100).ToString();

                    sitem["NZWX"] = GetSafeDouble(sitem["NZWX"]).ToString("0.0");


                    sitem["HG_NZWX"] = IsQualified(mitem["G_NZWX"], sitem["NZWX"]);
                    if (sitem["HG_NZWX"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }


                }
                else
                {
                    sitem["NZWX"] = "----";
                    sitem["HG_NZWX"] = "----";
                    mitem["G_NZWX"] = "----";
                }

                //只有面漆做对比率
                if (jcxm.Contains("、对比率、") && sitem["LX"] == "面漆")
                {
                    jcxmCur = "对比率";
                    //  sitem["DJ"]
                    double fsl1 = (GetSafeDouble(sitem["hbfsl6"]) + GetSafeDouble(sitem["hbfsl1"])) / (GetSafeDouble(sitem["bbfsl6"]) + GetSafeDouble(sitem["bbfsl1"]));
                    double fsl2 = (GetSafeDouble(sitem["hbfsl7"]) + GetSafeDouble(sitem["hbfsl2"])) / (GetSafeDouble(sitem["bbfsl7"]) + GetSafeDouble(sitem["bbfsl2"]));
                    double fsl3 = (GetSafeDouble(sitem["hbfsl8"]) + GetSafeDouble(sitem["hbfsl3"])) / (GetSafeDouble(sitem["bbfsl8"]) + GetSafeDouble(sitem["bbfsl3"]));
                    double fsl4 = (GetSafeDouble(sitem["hbfsl9"]) + GetSafeDouble(sitem["hbfsl4"])) / (GetSafeDouble(sitem["bbfsl9"]) + GetSafeDouble(sitem["bbfsl4"]));
                    double fsl5 = (GetSafeDouble(sitem["hbfsl10"]) + GetSafeDouble(sitem["hbfsl5"])) / (GetSafeDouble(sitem["bbfsl10"]) + GetSafeDouble(sitem["bbfsl5"]));

                    sitem["DBL"] = Math.Round((fsl1 + fsl2 + fsl3 + fsl4 + fsl5) / 5, 2).ToString();




                    sitem["HG_DBL"] = IsQualified(mitem["G_DBL"], sitem["DBL"]);
                    if (sitem["HG_DBL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sitem["DBL"] = "----";
                    sitem["HG_DBL"] = "----";
                    mitem["G_DBL"] = "----";
                }
                //
                if (jcxm.Contains("、低温成膜性、") && dCpmc.Contains("内墙涂料"))
                {
                    jcxmCur = "低温成膜性";
                    if ("合格" != sitem["HG_DWCMX"] && sitem["HG_DWCMX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["DWCMX"] = "----";
                    sitem["HG_DWCMX"] = "----";
                    mitem["G_DWCMX"] = "----";
                }

                if (mbhggs == 0)
                {
                    sitem["JCJG"] = "合格";
                }
                if (mbhggs >= 1)
                {
                    sitem["JCJG"] = "不合格";
                }
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");

            }
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            #endregion
        }
    }
}
