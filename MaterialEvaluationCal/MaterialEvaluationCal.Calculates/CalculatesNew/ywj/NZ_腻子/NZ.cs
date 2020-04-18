using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
namespace Calculates
{
    public class NZ : BaseMethods
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
            bool mFlag_Hg = false, mFlag_Bhg = false;
            var data = retData;
            var mrsDj = dataExtra["BZ_NZ_DJ"];
            var MItem = data["M_NZ"];
            var mitem = MItem[0];
            var SItem = data["S_NZ"];

            mitem["JCJGMS"] = "";
            mSjdj = "";
            var jcxmBhg = "";
            var jcxmCur = "";

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
                    //要求初级干燥抗裂性（6h）
                    mitem["G_DWWZ"] = mrsDj_item["G_DWWZ"];

                    //施工性
                    mitem["G_SGX"] = mrsDj_item["G_SGX"];
                    //腻子膜柔韧性
                    mitem["G_NZMRRX"] = mrsDj_item["G_NZMRRX"];

                    //初期干燥抗裂性
                    //打磨性
                    mitem["G_DMX"] = mrsDj_item["G_DMX"];

                    mitem["G_DNJQD"] = mrsDj_item["G_DNJQD"];
                    //动态抗开裂性
                    mitem["G_DTKKLX"] = mrsDj_item["G_DTKKLX"];

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

                mitem["G_BGSJ"] = "≤" + mitem["G_BGSJ"];
                mitem["G_NJQD"] = "≥" + mitem["G_NJQD"];

                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                bool sign;
                int xd, Gs, Ws;
                double md1, md2, md, sum;
                mFlag_Hg = false;
                mFlag_Bhg = false;
                mbhggs = 0;

                if (jcxm.Contains("、容器中状态、"))
                {
                    jcxmCur = "容器中状态";

                    if ("符合" != sitem["HG_RQZZT"] && "合格" != sitem["HG_RQZZT"])
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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

                if (jcxm.Contains("、干燥时间(表干)、"))
                {
                    jcxmCur = "干燥时间(表干)";
                    if (dCpmc == " 建筑室内用腻子")
                    {
                        if (string.IsNullOrEmpty(sitem["SGHD"]))
                        {
                            throw new Exception("请输入要求的施工厚度.");
                        }
                        mitem["G_BGSJ"] = GetSafeDouble(sitem["SGHD"]) >= 2 ? "≤5" : "≤2";
                    }
                    sitem["HG_BGSJ"] = IsQualified(mitem["G_BGSJ"], sitem["BGSJ"], false);
                    if (sitem["HG_BGSJ"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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

                if (jcxm.Contains("、粘结强度(标准状态)、"))
                {
                    jcxmCur = "粘结强度(标准状态)";

                    List<double> listQd = new List<double>();
                    for (xd = 1; xd <= 6; xd++)
                    {
                        sign = true;
                        if (!IsNumeric(sitem["NJQD" + xd]))
                        {
                            throw new Exception("粘结强度" + xd + "数据不合法，请检查。"); ;
                        }
                        listQd.Add(GetSafeDouble(sitem["NJQD" + xd]));
                    }
                    listQd.Sort();
                    sum = listQd[1] + listQd[2] + listQd[3] + listQd[4];
                    var pjmd = sum / 4;
                    sitem["NJQD"] = pjmd.ToString("0.00");
                    if (Math.Abs(listQd[0] - pjmd) <= pjmd * 0.2 || Math.Abs(listQd[0] - pjmd) <= pjmd * 0.2)
                    {
                        sitem["HG_NJQD"] = IsQualified(mitem["G_NJQD"], sitem["NJQD"], false);
                        mbhggs = sitem["HG_NJQD"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_NJQD"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        sitem["HG_NJQD"] = "重新试验";
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                    }
                }
                else
                {
                    sitem["NJQD"] = "----";
                    sitem["HG_NJQD"] = "----";
                    mitem["G_NJQD"] = "----";
                }

                if (jcxm.Contains("、粘结强度(浸水后)、"))
                {
                    jcxmCur = "粘结强度(浸水后)";

                    List<double> listQd = new List<double>();
                    for (xd = 1; xd <= 6; xd++)
                    {
                        sign = true;
                        if (!IsNumeric(sitem["JNJQD" + xd]))
                        {
                            throw new Exception("粘结强度(浸水后)" + xd + "数据不合法，请检查。"); ;
                        }
                        listQd.Add(GetSafeDouble(sitem["JNJQD" + xd]));
                    }
                    listQd.Sort();
                    sum = listQd[1] + listQd[2] + listQd[3] + listQd[4];
                    var pjmd = sum / 4;
                    sitem["JNJQD"] = pjmd.ToString("0.00");
                    if (Math.Abs(listQd[0] - pjmd) <= pjmd * 0.2 || Math.Abs(listQd[0] - pjmd) <= pjmd * 0.2)
                    {
                        sitem["HG_JNJQD"] = IsQualified(mitem["G_JNJQD"], sitem["JNJQD"], false);
                        mbhggs = sitem["HG_JNJQD"] == "不合格" ? mbhggs + 1 : mbhggs;
                        if (sitem["HG_JNJQD"] != "不合格")
                            mFlag_Hg = true;
                        else
                            mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        sitem["HG_JNJQD"] = "重新试验";
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                    }
                }
                else
                {
                    sitem["JNJQD"] = "----";
                    sitem["HG_JNJQD"] = "----";
                    mitem["G_JNJQD"] = "----";
                }

                if (jcxm.Contains("、粘结强度(冻融循环5次)、"))
                {
                    jcxmCur = "粘结强度(冻融循环5次)";

                    List<double> listQd = new List<double>();
                    for (int i = 1; i < 7; i++)
                    {
                        if (!IsNumeric(sitem["DNJQD" + i]))
                        {
                            throw new Exception("粘结强度(冻融循环5次)" + i + "数据不合法，请检查。"); ;
                        }
                        listQd.Add(GetSafeDouble(sitem["DNJQD" + i]));
                    }
                    listQd.Sort();
                    listQd.RemoveAt(5);
                    listQd.RemoveAt(0);
                    double pjNum = listQd.Average();
                    sitem["PJDNJQD"] = pjNum.ToString("0.00");
                    var flag = true;
                    foreach (var item in listQd)
                    {
                        if (Math.Abs(pjNum - item) / pjNum > 0.2)
                        {
                            flag = false;
                            break;
                        }
                    }
                    mitem["HG_DNJQD"] = flag ? "合格" : "不合格";

                    if (mitem["HG_DNJQD"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["PJDNJQD"] = "----";
                    mitem["G_DNJQD"] = "----";
                    mitem["HG_DNJQD"] = "----";
                }

                if (jcxm.Contains("、耐水性、"))
                {
                    jcxmCur = "耐水性";

                    if ("符合" != sitem["HG_NSX"] && "合格" != sitem["HG_NSX"])
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                    jcxmCur = CurrentJcxm(jcxm, "耐碱性(48h),耐碱性(24h),耐碱性");
                    if ("合格" != sitem["HG_NJX"] && sitem["HG_NJX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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

                if (jcxm.Contains("、低温储存稳定性、"))
                {
                    jcxmCur = "低温储存稳定性";

                    if ("合格" != sitem["HG_DWWDX"] && sitem["HG_DWWDX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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

                if (jcxm.Contains("、腻子膜柔韧性、") || jcxm.Contains("、柔韧性、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "腻子膜柔韧性,柔韧性");

                    if ("合格" != sitem["HG_NZMRRX"] && sitem["HG_NZMRRX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["NZMRRX"] = "----";
                    sitem["HG_NZMRRX"] = "----";
                    mitem["G_NZMRRX"] = "----";
                }

                if (jcxm.Contains("、初期干燥抗裂性(6h)、") || jcxm.Contains("、初期干燥抗裂性(3h)、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "初期干燥抗裂性(6h),初期干燥抗裂性(3h)");

                    //判定
                    if (string.IsNullOrEmpty(sitem["SGHD"]))
                    {
                        throw new Exception("请输入要求的施工厚度.");
                    }
                    var gCJGZLX = GetSafeDouble(sitem["SGHD"]);

                    if (dCpmc == "建筑外墙用腻子")
                    {
                        if (1.5 >= gCJGZLX)
                        {
                            //单道施工厚度《= 1.5mm  1mm无裂缝
                            mitem["G_CHGZKLX"] = "1mm无裂纹";
                        }
                        else
                        {
                            mitem["G_CHGZKLX"] = "2mm无裂纹";
                        }
                    }
                    else
                    {
                        mitem["G_CHGZKLX"] = "无裂纹";
                    }

                    if (sitem["HG_CHGZKLX"] =="合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                    }
                }
                else
                {
                    sitem["HG_CHGZKLX"] = "----";
                    sitem["SGHD"] = "----";
                    mitem["G_CHGZKLX"] = "----";
                }

                if (jcxm.Contains("、打磨性、"))
                {
                    jcxmCur = "打磨性";
                    if ("合格" != mitem["HG_DMX"] && mitem["HG_DMX"] != "符合")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sitem["W_DMX"] = "----";
                    mitem["HG_DMX"] = "----";
                    mitem["G_DMX"] = "----";
                }

                if (jcxm.Contains("、动态抗开裂性、"))
                {
                    jcxmCur = "动态抗开裂性";
                    if (dLx == "T型")
                    {
                        sitem["HG_DTKKLX"] = IsQualified(mitem["G_DTKKLX"], sitem["W_DTKKLX"]);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(sitem["W_DTKKLX"]))
                        {
                            sitem["HG_DTKKLX"] = "----";
                        }
                        else
                        {
                            List<string> list = sitem["W_DTKKLX"].Split(',').ToList();
                            if (list.Count == 2)
                            {
                                sitem["HG_DTKKLX"] = IsQualified(list[0], mitem["G_DTKKLX"]) == IsQualified(list[1], mitem["G_DTKKLX"]) ? "合格" : "不合格";
                            }
                        }
                    }
                    if (sitem["HG_DTKKLX"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;     jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                else
                {
                    sitem["HG_DTKKLX"] = "----";
                    mitem["G_DTKKLX"] = "----";
                    sitem["W_DTKKLX"] = "----";
                }

                if (jcxm.Contains("、吸水量"))
                {
                    jcxmCur = "吸水量";
                    mitem["HG_XSL"] = IsQualified(mitem["G_XSL"], sitem["W_XSL"]);
                    if (mitem["HG_XSL"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;    
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sitem["W_XSL"] = "----";
                    mitem["G_XSL"] = "----";
                    mitem["HG_XSL"] = "----";
                }

                if (mbhggs == 0)
                {
                    sitem["JCJG"] = "合格";
                }
                else
                {
                    sitem["JCJG"] = "不合格";
                    mAllHg = false;
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