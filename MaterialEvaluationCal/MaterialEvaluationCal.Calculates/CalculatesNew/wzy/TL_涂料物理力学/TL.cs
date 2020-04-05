using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
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
                    if ("符合" != sitem["HG_RQZZT"] && "合格" != sitem["HG_RQZZT"])
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    if ("符合" != sitem["HG_SGX"] && "合格" != sitem["HG_SGX"])
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    sitem["HG_BGSJ"] = IsQualified(mitem["G_BGSJ"], sitem["BGSJ"], false);
                    if (sitem["HG_BGSJ"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    sitem["HG_SGSJ"] = IsQualified(mitem["G_SGSJ"], sitem["SGSJ"], false);
                    mbhggs = sitem["HG_SGSJ"] == "不合格" ? mbhggs + 1 : mbhggs;
                    if (sitem["HG_SGSJ"] != "不合格")
                        mFlag_Hg = true;
                    else
                        mFlag_Bhg = true;
                }
                else
                {
                    sitem["SGSJ"] = "----";
                    sitem["HG_SGSJ"] = "----";
                    mitem["G_SGSJ"] = "----";
                }

                if (jcxm.Contains("、耐水性、") || jcxm.Contains("、耐水性(96h)、"))
                {
                    if ("符合" != sitem["HG_NSX"] && "合格" != sitem["HG_NSX"])
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    if ("合格" != sitem["HG_NJX"] && sitem["HG_NJX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    //sitem["HG_DWWDX"] = IsQualified(mitem["G_DWWDX"], sitem["DWWDX"]);
                    if ("合格" != sitem["HG_DWWDX"] && sitem["HG_DWWDX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    //如果Ⅰ型 《=0.3 Ⅱ型《=0.5
                    if ("合格" != sitem["HG_TMWG"] && sitem["HG_TMWG"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    if ("合格" != sitem["HG_NWBX"] && sitem["HG_NWBX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    if ("合格" != sitem["HG_NXSX"] && sitem["HG_NXSX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    sitem["HG_NZWX"] = IsQualified(mitem["G_NZWX"], sitem["NZWX"]);
                    if (sitem["HG_NZWX"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    sitem["HG_DBL"] = IsQualified(mitem["G_DBL"], sitem["DBL"]);
                    if (sitem["HG_DBL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sitem["DBL"] = "----";
                    sitem["HG_DBL"] = "----";
                    mitem["G_DBL"] = "----";
                }

                if (jcxm.Contains("、低温成膜性、"))
                {
                    if ("合格" != sitem["HG_DWCMX"] && sitem["HG_DWCMX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
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
                    mitem["JCJGMS"] = "该组试件所检项目符合" + mitem["PDBZ"] + "标准要求。";
                    sitem["JCJG"] = "合格";
                }
                if (mbhggs >= 1)
                {
                    mitem["JCJGMS"] = "该组试件不符合" + mitem["PDBZ"] + "标准要求。";
                    sitem["JCJG"] = "不合格";
                    if (mFlag_Bhg && mFlag_Hg)
                        mitem["JCJGMS"] = "该组试样所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                }
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");

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
                    if (mFlag_Bhg && mFlag_Hg)
                        mitem["JCJGMS"] = "该组试样所检项目部分符合" + mitem["PDBZ"] + "标准要求。";
                }
            }
            #endregion
        }
    }
}
