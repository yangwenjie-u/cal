using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class FSJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            double[] mkyqdArray = new double[3];
            string[] mtmpArray;
            int mbhggs = 0;
            string mSjdj;
            int vp = 0;
            string mJSFF;
            bool mAllHg = true;
            bool msffs;
            bool mFlag_Hg = false, mFlag_Bhg = false;
            var data = retData;
            var mrsDj = dataExtra["BZ_FSJ_DJ"];
            var mrsadx = dataExtra["BZ_NADXFF"];
            var MItem = data["M_FSJ"];
            var SItem = data["S_FSJ"];
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";

            string which = string.Empty;

            foreach (var sitem in SItem)
            {
                mSjdj = sitem["WJJMC"];            //设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()) && x["PZ"].Contains(sitem["PZ"].Trim()));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    MItem[0]["G_XD"] = mrsDj_Filter["XD"];
                    MItem[0]["G_MD"] = mrsDj_Filter["MD"];
                    MItem[0]["G_MSL"] = mrsDj_Filter["MSL"];
                    MItem[0]["G_GTHL"] = mrsDj_Filter["GTHL"];
                    MItem[0]["G_CNSJC"] = mrsDj_Filter["CNSJC"];
                    MItem[0]["G_ZNSJC"] = mrsDj_Filter["ZNSJC"];
                    MItem[0]["G_STGDB"] = mrsDj_Filter["STGDB"];
                    MItem[0]["G_TSYLB"] = mrsDj_Filter["TSYLB"];
                    MItem[0]["G_CNSJ"] = mrsDj_Filter["CNSJ"];
                    MItem[0]["G_ZNSJ"] = mrsDj_Filter["ZNSJ"];
                    which = mrsDj_Filter["WHICH"];
                    MItem[0]["G_KYQD1D"] = mrsDj_Filter["KYQDB1D"];
                    MItem[0]["G_KYQD3D"] = mrsDj_Filter["KYQDB3D"];
                    MItem[0]["G_KYQD7D"] = mrsDj_Filter["KYQDB7D"];
                    MItem[0]["G_KYQD28D"] = mrsDj_Filter["KYQDB28D"];
                    MItem[0]["G_SSLB"] = mrsDj_Filter["SSLB28D"];
                    MItem[0]["G_XSLB"] = mrsDj_Filter["XSLB"];
                    MItem[0]["G_LLZHL"] = mrsDj_Filter["LLZHL"];
                    MItem[0]["G_ADX"] = mrsDj_Filter["ADX"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "试件尺寸为空";
                    continue;
                }
                mbhggs = 0;

                double md, md1, md2, pjmd, sum;
                int xd, xd1, xd2;
                double[] Arrmd = new double[4];
                string sd;
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、细度、"))
                {
                    jcxmCur = "细度";

                    if (sitem["XDM1_1"] != "" && sitem["XDM1_1"] != "----")
                    {
                        sitem["XD_1"] = Round((Conversion.Val(sitem["XDM1_1"]) / Conversion.Val(sitem["XDM0_1"])) * 100, 2).ToString("F2");
                        sitem["XD_2"] = Round((Conversion.Val(sitem["XDM1_2"]) / Conversion.Val(sitem["XDM0_2"])) * 100, 2).ToString("F2");
                    }
                    if (sitem["XD_1"] != "" && sitem["XD_1"] != "----")
                        sitem["XD"] = Round((Conversion.Val(sitem["XD_1"]) + Conversion.Val(sitem["XD_2"])) / 2, 2).ToString("F2");
                    if (sitem["XDKZZ"] == "----")
                        MItem[0]["HG_XD"] = "---";
                    else
                        MItem[0]["HG_XD"] = IsQualified(sitem["XDKZZ"], sitem["XD"]);
                    MItem[0]["G_XD"] = sitem["XDKZZ"];
                    if (MItem[0]["HG_XD"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["XD_1"] = "----";
                    sitem["XD_2"] = "----";
                    sitem["XD"] = "----";
                    MItem[0]["HG_XD"] = "----";
                    MItem[0]["G_XD"] = "----";
                }
                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";
                    if (sitem["MDBJWJJ_1"] != "" && sitem["MDBJWJJ_1"] != "----")
                    {
                        sitem["MD_1"] = Round(0.9982 * ((Conversion.Val(sitem["MDBJWJJ_1"]) - Conversion.Val(sitem["MDRLBZ_1"])) / Conversion.Val(sitem["MDTJ_1"])), 3).ToString("F3");
                        sitem["MD_2"] = Round(0.9982 * ((Conversion.Val(sitem["MDBJWJJ_2"]) - Conversion.Val(sitem["MDRLBZ_2"])) / Conversion.Val(sitem["MDTJ_2"])), 3).ToString("F3");
                    }
                    if (sitem["MD_1"] != "" && sitem["MD_1"] != "----")
                        sitem["MD"] = Round((Conversion.Val(sitem["MD_1"]) + Conversion.Val(sitem["MD_2"])) / 2, 3).ToString("F3");


                    if (Conversion.Val(sitem["MDKZZ"]) > 1.1)
                        MItem[0]["G_MD"] = Round((Conversion.Val(sitem["MDKZZ"]) - 0.03), 3).ToString("F3") + "~" + Round((Conversion.Val(sitem["MDKZZ"]) + 0.03), 3).ToString("F3");
                    else
                        MItem[0]["G_MD"] = Round((Conversion.Val(sitem["MDKZZ"]) - 0.02), 3).ToString("F3") + "~" + Round((Conversion.Val(sitem["MDKZZ"]) + 0.02), 3).ToString("F3");
                    if (sitem["MDKZZ"] == "----")
                        MItem[0]["HG_MD"] = "----";
                    else
                        MItem[0]["HG_MD"] = IsQualified(MItem[0]["G_MD"], sitem["MD"]);
                    if (MItem[0]["HG_MD"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["MD_1"] = "----";
                    sitem["MD_2"] = "----";
                    sitem["MD"] = "----";
                    MItem[0]["HG_MD"] = "----";
                    MItem[0]["G_MD"] = "----";
                }

                if (jcxm.Contains("、固体含量、"))
                {
                    jcxmCur = "固体含量";
                    if (sitem["GTHLM2_1"] != "" && sitem["GTHLM2_1"] != "----")
                    {
                        sitem["GTHL_1"] = Round((Conversion.Val(sitem["GTHLM2_1"]) - Conversion.Val(sitem["GTHLM0_1"])) / (Conversion.Val(sitem["GTHLM1_1"]) - Conversion.Val(sitem["GTHLM0_1"])) * 100, 2).ToString("F2");
                        sitem["GTHL_2"] = Round((Conversion.Val(sitem["GTHLM2_2"]) - Conversion.Val(sitem["GTHLM0_2"])) / (Conversion.Val(sitem["GTHLM1_2"]) - Conversion.Val(sitem["GTHLM0_2"])) * 100, 2).ToString("F2");
                    }
                    if (sitem["GTHL_1"] != "" && sitem["GTHL_1"] != "----")
                        sitem["GTHL"] = Round((Conversion.Val(sitem["GTHL_1"]) + Conversion.Val(sitem["GTHL_2"])) / 2, 2).ToString("F2");
                    if (Conversion.Val(sitem["HGLKZZ"]) > 25)
                        MItem[0]["G_GTHL"] = Round(Conversion.Val(sitem["HGLKZZ"]) * 0.95, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HGLKZZ"]) * 1.05, 2).ToString("F2");
                    else
                        MItem[0]["G_GTHL"] = Round(Conversion.Val(sitem["HGLKZZ"]) * 0.9, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HGLKZZ"]) * 1.1, 2).ToString("F2");
                    if (sitem["HGLKZZ"] == "----")
                        MItem[0]["HG_GTHL"] = "----";
                    else
                        MItem[0]["HG_GTHL"] = IsQualified(MItem[0]["G_GTHL"], sitem["GTHL"]);


                    if (MItem[0]["HG_GTHL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["GTHL_1"] = "----";
                    sitem["GTHL_2"] = "----";
                    sitem["GTHL"] = "----";
                    MItem[0]["G_GTHL"] = "----";
                    MItem[0]["HG_GTHL"] = "----";
                }
                if (jcxm.Contains("、含水率、"))
                {
                    jcxmCur = "含水率";
                    if (sitem["HSLM1_1"] != "" && sitem["HSLM1_1"] != "----")
                    {
                        sitem["HSL_1"] = Round((Conversion.Val(sitem["HSLM1_1"]) - Conversion.Val(sitem["HSLM2_1"])) / (Conversion.Val(sitem["HSLM2_1"]) - Conversion.Val(sitem["HSLM0_1"])) * 100, 2).ToString("F2");
                        sitem["HSL_2"] = Round((Conversion.Val(sitem["HSLM1_2"]) - Conversion.Val(sitem["HSLM2_2"])) / (Conversion.Val(sitem["HSLM2_2"]) - Conversion.Val(sitem["HSLM0_2"])) * 100, 2).ToString("F2");
                        sitem["HSL_3"] = Round((Conversion.Val(sitem["HSLM1_3"]) - Conversion.Val(sitem["HSLM2_3"])) / (Conversion.Val(sitem["HSLM2_3"]) - Conversion.Val(sitem["HSLM0_3"])) * 100, 2).ToString("F2");
                    }
                    if (sitem["HSL_1"] != "" && sitem["HSL_1"] != "----")
                        sitem["HSL"] = Round((Conversion.Val(sitem["HSL_1"]) + Conversion.Val(sitem["HSL_2"]) + Conversion.Val(sitem["HSL_3"])) / 3, 1).ToString("F1");
                    if (Conversion.Val(sitem["HSLKZZ"]) > 5)
                        MItem[0]["G_HSL"] = Round(Conversion.Val(sitem["HSLKZZ"]) * 0.9, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HSLKZZ"]) * 1.1, 2).ToString("F2");
                    else
                        MItem[0]["G_HSL"] = Round(Conversion.Val(sitem["HSLKZZ"]) * 0.8, 2).ToString("F2") + "~" + Round(Conversion.Val(sitem["HSLKZZ"]) * 1.2, 2).ToString("F2");
                    if (sitem["HSLKZZ"] == "----")
                        MItem[0]["HG_HSL"] = "----";
                    else
                        MItem[0]["HG_HSL"] = IsQualified(MItem[0]["G_HSL"], sitem["HSL"]);
                    if (MItem[0]["HG_HSL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["HSL_1"] = "----";
                    sitem["HSL_2"] = "----";
                    sitem["HSL"] = "----";
                    MItem[0]["G_HSL"] = "----";
                    MItem[0]["HG_HSL"] = "----";
                }
                if (jcxm.Contains("、总碱量、"))
                {
                    jcxmCur = "总碱量";
                    MItem[0]["G_ZJL"] = sitem["ZJLKZZ"].Trim();
                    if (sitem["ZJLKZZ"] == "----")
                        MItem[0]["HG_ZJL"] = "----";
                    else
                        MItem[0]["HG_ZJL"] = IsQualified(MItem[0]["G_ZJL"], sitem["ZJL"]);
                    if (MItem[0]["HG_ZJL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    MItem[0]["G_ZJL"] = "----";
                    sitem["ZJL"] = "----";
                    MItem[0]["HG_ZJL"] = "----";
                }

                if (jcxm.Contains("、氯离子含量、"))
                {
                    jcxmCur = "氯离子含量";
                    if (sitem["LLZKZZ"] == "----")
                        MItem[0]["HG_LLZHL"] = "----";
                    else
                        MItem[0]["HG_LLZHL"] = IsQualified(sitem["LLZKZZ"], sitem["LLZHL"]);
                    MItem[0]["G_LLZHL"] = sitem["LLZKZZ"];
                    if (MItem[0]["HG_LLZHL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["LLZHL1"] = "----";
                    sitem["LLZHL2"] = "----";
                    sitem["LLZHL"] = "----";
                    MItem[0]["HG_LLZHL"] = "----";
                }
                string mg_adx = "";
                string mBZFpj = "";
                string mBZFxc = "";
                string mg_bzfpj = "";
                string mg_bzfxc = "";
                bool adx_hg = true;
                if (jcxm.Contains("、安定性、"))
                {
                    jcxmCur = "安定性";
                    if (sitem["ADXFF"] == "----" || string.IsNullOrEmpty(sitem["ADXFF"]))
                        sitem["ADXFF"] = "代用法";
                    var mrsadx_Filter = mrsadx.FirstOrDefault(x => x["MC"].Contains(sitem["ADXFF"].Trim()));
                    if (mrsadx_Filter != null && mrsadx_Filter.Count > 0)
                    {
                        mg_adx = mrsadx_Filter["G_ADX"];
                        mg_bzfpj = mrsadx_Filter["BZFPJ"];
                        mg_bzfxc = mrsadx_Filter["BZFXC"];
                    }
                    //安定性
                    if (sitem["ADXFF"] == "代用法")
                    {
                        if (sitem["ADX"] == "完整")
                        {
                            MItem[0]["HG_ADX"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            MItem[0]["HG_ADX"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        mBZFpj = Round(((Conversion.Val(sitem["BZFC_1"]) - Conversion.Val(sitem["BZFA_1"])) + (Conversion.Val(sitem["BZFC_2"]) - Conversion.Val(sitem["BZFA_2"]))) / 2, 1).ToString();
                        mBZFxc = Round(Math.Abs((Conversion.Val(sitem["BZFC_1"]) - Conversion.Val(sitem["BZFA_1"])) - (Conversion.Val(sitem["BZFC_2"]) - Conversion.Val(sitem["BZFA_2"]))), 1).ToString();
                        if (Conversion.Val(mBZFpj) <= Conversion.Val(mg_bzfpj) && Conversion.Val(mBZFxc) <= Conversion.Val(mg_bzfxc))
                        {
                            MItem[0]["HG_ADX"] = "符合";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            MItem[0]["HG_ADX"] = "不符合";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                else
                {
                    sitem["ADXFF"] = "----";
                    MItem[0]["HG_ADX"] = "----";
                    mg_adx = "----";
                    sitem["ADX"] = "----";
                    mBZFpj = "----";
                    mBZFxc = "----";
                    mg_bzfpj = "----";
                    mg_bzfxc = "----";
                    adx_hg = true;

                }
                if (jcxm.Contains("、渗透高度比、"))
                {
                    jcxmCur = "渗透高度比";

                    sitem["STGDB"] = Round((Conversion.Val(sitem["SSTGD"]) / Conversion.Val(sitem["JSTGD"])) * 100, 0).ToString();
                    MItem[0]["HG_STGDB"] = IsQualified(MItem[0]["G_STGDB"], sitem["STGDB"]);
                    if (MItem[0]["HG_STGDB"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["STGDB"] = "----";
                    MItem[0]["HG_STGDB"] = "----";
                    MItem[0]["G_STGDB"] = "----";
                }
                if (jcxm.Contains("、透水压力比、"))
                {
                    jcxmCur = "透水压力比";
                    sitem["TSYLB"] = Round(Conversion.Val(sitem["STSYL"]) / Conversion.Val(sitem["JTSYL"]) * 100, 0).ToString();
                    MItem[0]["HG_TSYLB"] = IsQualified(MItem[0]["G_TSYLB"], sitem["TSYLB"]);
                    if (MItem[0]["HG_TSYLB"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["TSYLB"] = "----";
                    MItem[0]["HG_TSYLB"] = "----";
                    MItem[0]["G_TSYLB"] = "----";
                }
                if (jcxm.Contains("、凝结时间、"))
                {
                    jcxmCur = "凝结时间";

                    sitem["CNSJ"] = (Conversion.Val(sitem["CNSJH"]) * 60 + Conversion.Val(sitem["CNSJM"]) - (Conversion.Val(sitem["JSSJH"]) * 60 + Conversion.Val(sitem["JSSJM"]))).ToString();
                    sitem["ZNSJ"] = (Conversion.Val(sitem["ZNSJH"]) * 60 + Conversion.Val(sitem["ZNSJM"]) - (Conversion.Val(sitem["JSSJH"]) * 60 + Conversion.Val(sitem["JSSJM"]))).ToString();
                    MItem[0]["HG_CNSJ"] = IsQualified(MItem[0]["G_CNSJ"], sitem["CNSJ"]);
                    MItem[0]["HG_ZNSJ"] = IsQualified(MItem[0]["G_ZNSJ"], sitem["ZNSJ"]);
                    if (MItem[0]["HG_CNSJ"] == "不合格" || MItem[0]["HG_ZNSJ"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["CNSJ"] = "----";
                    sitem["ZNSJ"] = "----";
                    MItem[0]["HG_CNSJ"] = "----";
                    MItem[0]["HG_ZNSJ"] = "----";
                    MItem[0]["G_CNSJ"] = "----";
                    MItem[0]["G_ZNSJ"] = "----";
                }
                if (jcxm.Contains("、泌水率比、"))
                {
                    jcxmCur = "泌水率比";

                    //基准混凝土

                    //基准配比水泥用量1
                    if (sitem["PBSN1"] != "" && sitem["PBSN1"] != "----")
                    {
                        //基准拌合物总质量1         // //基准配比水泥用量1              //基准配比水用量1              //基准配比砂用量1                  //配比石子用量1
                        sitem["JPHWZL_1"] = (Conversion.Val(sitem["PBSN1"]) + Conversion.Val(sitem["PBS1"]) + Conversion.Val(sitem["PBSA1"]) + Conversion.Val(sitem["PBSZ1"])).ToString();
                        sitem["JPHWZL_2"] = (Conversion.Val(sitem["PBSN2"]) + Conversion.Val(sitem["PBS2"]) + Conversion.Val(sitem["PBSA2"]) + Conversion.Val(sitem["PBSZ2"])).ToString();
                        sitem["JPHWZL_3"] = (Conversion.Val(sitem["PBSN3"]) + Conversion.Val(sitem["PBS3"]) + Conversion.Val(sitem["PBSA3"]) + Conversion.Val(sitem["PBSZ3"])).ToString();
                        //泌水基准拌合物用水量1
                        sitem["MJBYS_1"] = Conversion.Val(sitem["PBS1"]).ToString();//基准配比水用量1
                        sitem["MJBYS_2"] = Conversion.Val(sitem["PBS2"]).ToString();
                        sitem["MJBYS_3"] = Conversion.Val(sitem["PBS3"]).ToString();
                    }
                    //基准泌水总质量1
                    if (sitem["JMSZL_1"] != "" && sitem["JMSZL_1"] != "----")
                    {
                        sitem["JSYZL_1"] = (Conversion.Val(sitem["JTSYZL1"]) - Conversion.Val(sitem["JTZL1"])).ToString();
                        sitem["JSYZL_2"] = (Conversion.Val(sitem["JTSYZL2"]) - Conversion.Val(sitem["JTZL2"])).ToString();
                        sitem["JSYZL_3"] = (Conversion.Val(sitem["JTSYZL3"]) - Conversion.Val(sitem["JTZL3"])).ToString();

                        //基准泌水率1                                                        //泌水基准拌合物用水量1               。。基准拌合物总质量1             //基准试样质量1
                        sitem["JMSL_1"] = Round(Conversion.Val(sitem["JMSZL_1"]) / (Conversion.Val(sitem["MJBYS_1"]) / Conversion.Val(sitem["JPHWZL_1"])) / Conversion.Val(sitem["JSYZL_1"]) * 100, 2).ToString("0.00");
                        sitem["JMSL_2"] = Round(Conversion.Val(sitem["JMSZL_2"]) / (Conversion.Val(sitem["MJBYS_2"]) / Conversion.Val(sitem["JPHWZL_2"])) / Conversion.Val(sitem["JSYZL_2"]) * 100, 2).ToString("0.00");
                        sitem["JMSL_3"] = Round(Conversion.Val(sitem["JMSZL_3"]) / (Conversion.Val(sitem["MJBYS_3"]) / Conversion.Val(sitem["JPHWZL_3"])) / Conversion.Val(sitem["JSYZL_3"]) * 100, 2).ToString("0.00");
                    }
                    if (sitem["JMSL_1"] != "" && sitem["JMSL_1"] != "----")
                    {
                        string mlongStr = sitem["JMSL_1"] + ", " + sitem["JMSL_2"] + ", " + sitem["JMSL_3"];
                        mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 2; vp++)
                            mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkyqdArray);
                        double mMaxKyqd = mkyqdArray[2];
                        double mMinKyqd = mkyqdArray[0];
                        double mMidKyqd = mkyqdArray[1];
                        double mAvgKyqd = mkyqdArray.Average();
                        MItem[0]["JCJGMS"] = "";
                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_MSL"] = "重做";
                            sitem["JPJMSL"] = "重做";
                        }

                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sitem["JPJMSL"] = Round(mMidKyqd, 1).ToString("0.0");
                        }

                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sitem["JPJMSL"] = Round(mMidKyqd, 1).ToString("0.0");
                        }

                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sitem["JPJMSL"] = Round(mAvgKyqd, 1).ToString("0.0");

                        }
                    }

                    //受检配比水泥用量1
                    if (sitem["SPBSN1"] != "" && sitem["SPBSN1"] != "----")
                    {
                        //受检拌合物总质量1                                                      //受检配比水用量1             //受检配比砂用量1                //受检配比石子用量1            受检配比外加剂1用量1                   //受检配比外加剂2用量1                                     
                        sitem["SPHWZL_1"] = (Conversion.Val(sitem["SPBSN1"]) + Conversion.Val(sitem["SPBS1"]) + Conversion.Val(sitem["SPBSA1"]) + Conversion.Val(sitem["SPBSZ1"]) + Conversion.Val(sitem["SPBWJJ11"]) + Conversion.Val(sitem["SPBWJJ21"])).ToString();
                        sitem["SPHWZL_2"] = (Conversion.Val(sitem["SPBSN2"]) + Conversion.Val(sitem["SPBS2"]) + Conversion.Val(sitem["SPBSA2"]) + Conversion.Val(sitem["SPBSZ2"]) + Conversion.Val(sitem["SPBWJJ12"]) + Conversion.Val(sitem["SPBWJJ22"])).ToString();
                        sitem["SPHWZL_3"] = (Conversion.Val(sitem["SPBSN3"]) + Conversion.Val(sitem["SPBS3"]) + Conversion.Val(sitem["SPBSA3"]) + Conversion.Val(sitem["SPBSZ3"]) + Conversion.Val(sitem["SPBWJJ13"]) + Conversion.Val(sitem["SPBWJJ23"])).ToString();

                        //泌水受检拌合物用水量1
                        sitem["MSBYS_1"] = Conversion.Val(sitem["SPBS1"]).ToString();
                        sitem["MSBYS_2"] = Conversion.Val(sitem["SPBS2"]).ToString();
                        sitem["MSBYS_3"] = Conversion.Val(sitem["SPBS3"]).ToString();
                    }

                    //受检混凝土
                    if (sitem["SMSZL_1"] != "" && sitem["SMSZL_1"] != "----")
                    {
                        sitem["SSYZL_1"] = (Conversion.Val(sitem["STSYZL1"]) - Conversion.Val(sitem["STZL1"])).ToString();
                        sitem["SSYZL_2"] = (Conversion.Val(sitem["STSYZL2"]) - Conversion.Val(sitem["STZL2"])).ToString();
                        sitem["SSYZL_3"] = (Conversion.Val(sitem["STSYZL3"]) - Conversion.Val(sitem["STZL3"])).ToString();

                        //受检泌水率1                    受检泌水总质量1                   泌水受检拌合物用水量1                 //受检拌合物总质量1                // 受检试样质量1
                        sitem["SMSL_1"] = Round(Conversion.Val(sitem["SMSZL_1"]) / (Conversion.Val(sitem["MSBYS_1"]) / Conversion.Val(sitem["SPHWZL_1"])) / Conversion.Val(sitem["SSYZL_1"]) * 100, 2).ToString();
                        sitem["SMSL_2"] = Round(Conversion.Val(sitem["SMSZL_2"]) / (Conversion.Val(sitem["MSBYS_2"]) / Conversion.Val(sitem["SPHWZL_2"])) / Conversion.Val(sitem["SSYZL_2"]) * 100, 2).ToString();
                        sitem["SMSL_3"] = Round(Conversion.Val(sitem["SMSZL_3"]) / (Conversion.Val(sitem["MSBYS_3"]) / Conversion.Val(sitem["SPHWZL_3"])) / Conversion.Val(sitem["SSYZL_3"]) * 100, 2).ToString();
                    }
                    if (sitem["SMSL_1"] != "" && sitem["SMSL_1"] != "----")
                    {
                        string mlongStr = sitem["SMSL_1"] + "," + sitem["SMSL_2"] + "," + sitem["SMSL_3"];
                        mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 2; vp++)
                            mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkyqdArray);
                        double mMaxKyqd = mkyqdArray[2];
                        double mMinKyqd = mkyqdArray[0];
                        double mMidKyqd = mkyqdArray[1];
                        double mAvgKyqd = mkyqdArray.Average();
                        MItem[0]["JCJGMS"] = "";
                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                        {
                            MItem[0]["HG_MSL"] = "重做";
                            sitem["SPJMSL"] = "重做";
                        }
                        if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                        {
                            //最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sitem["SPJMSL"] = Round(mMidKyqd, 1).ToString("0.0");
                        }

                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                            sitem["SPJMSL"] = Round(mMidKyqd, 1).ToString("0.0");
                        }

                        if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                        {
                            //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                            sitem["SPJMSL"] = Round(mAvgKyqd, 1).ToString("0.0");
                        }
                    }
                    if (sitem["JPJMSL"] == "重做" || sitem["SPJMSL"] == "重做")
                    {
                        sitem["MSLB"] = "-----";

                        mbhggs = mbhggs + 1;
                        if (sitem["JPJMSL"] == "重做" && sitem["SPJMSL"] == "重做")
                            MItem[0]["HG_MSL"] = "基准受检重做";
                        else
                        {
                            if (sitem["JPJMSL"] == "重做")
                                MItem[0]["HG_MSL"] = "基准重做";
                            else
                                MItem[0]["HG_MSL"] = "受检重做";
                        }
                    }
                    else
                    {
                        if (sitem["SPJMSL"] != "" && sitem["SPJMSL"] != "----")
                            sitem["MSLB"] = Round((Conversion.Val(sitem["SPJMSL"]) / Conversion.Val(sitem["JPJMSL"])) * 100, 0).ToString();
                        MItem[0]["HG_MSL"] = IsQualified(MItem[0]["G_MSL"], sitem["MSLB"]);
                    }
                    if (MItem[0]["HG_MSL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    MItem[0]["G_MSL"] = "----";
                   // sitem["MSLB"] = "-----";
                    MItem[0]["HG_MSL"] = "----";
                }
                if (jcxm.Contains("、初凝时间差、"))
                {
                    jcxmCur = "初凝时间差";
                    if (sitem["CNSJT_1"] != "" && sitem["CNSJT_1"] != "----")
                    {
                        sitem["CNSJC_1"] = (Conversion.Val(sitem["CNSJT_1"]) - Conversion.Val(sitem["CNJZT_1"])).ToString();
                        sitem["CNSJC_2"] = (Conversion.Val(sitem["CNSJT_2"]) - Conversion.Val(sitem["CNJZT_2"])).ToString();
                        sitem["CNSJC_3"] = (Conversion.Val(sitem["CNSJT_3"]) - Conversion.Val(sitem["CNJZT_3"])).ToString();
                    }
                    if (sitem["CNSJC_1"] != "" && sitem["CNSJC_1"] != "----")
                    {
                        string mlongStr = sitem["CNSJC_1"] + ", " + sitem["CNSJC_2"] + ", " + sitem["CNSJC_3"];
                        mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 2; vp++)
                            mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkyqdArray);
                        double mMaxKyqd = mkyqdArray[2];
                        double mMinKyqd = mkyqdArray[0];
                        double mMidKyqd = mkyqdArray[1];
                        double mAvgKyqd = mkyqdArray.Average();
                        MItem[0]["JCJGMS"] = "";
                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) > 30)
                        {
                            MItem[0]["HG_CNSJC"] = "重做";
                            sitem["CNPJSJC"] = "重做";
                        }
                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) <= 30)
                            sitem["CNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) > 30)
                            sitem["CNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) <= 30)
                            sitem["CNPJSJC"] = Round(mAvgKyqd, 0).ToString();
                    }
                    if (sitem["CNPJSJC"] == "重做")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                        MItem[0]["HG_CNSJC"] = IsQualified(MItem[0]["G_CNSJC"], sitem["CNPJSJC"]);
                    }
                    if (MItem[0]["HG_CNSJC"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    MItem[0]["G_CNSJC"] = "----";
                    sitem["CNPJSJC"] = "-----";
                    MItem[0]["HG_CNSJC"] = "----";
                }
                if (jcxm.Contains("、终凝时间差、"))
                {
                    jcxmCur = "终凝时间差";

                    if (sitem["ZNSJT_1"] != "" && sitem["ZNSJT_1"] != "----")
                    {
                        sitem["ZNSJC_1"] = (Conversion.Val(sitem["ZNSJT_1"]) - Conversion.Val(sitem["ZNJZT_1"])).ToString();
                        sitem["ZNSJC_2"] = (Conversion.Val(sitem["ZNSJT_2"]) - Conversion.Val(sitem["ZNJZT_2"])).ToString();
                        sitem["ZNSJC_3"] = (Conversion.Val(sitem["ZNSJT_3"]) - Conversion.Val(sitem["ZNJZT_3"])).ToString();
                    }
                    if (sitem["ZNSJC_1"] != "" && sitem["ZNSJC_1"] != "----")
                    {
                        string mlongStr = sitem["ZNSJC_1"] + "," + sitem["ZNSJC_2"] + "," + sitem["ZNSJC_3"];
                        mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 2; vp++)
                            mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkyqdArray);
                        double mMaxKyqd = mkyqdArray[2];
                        double mMinKyqd = mkyqdArray[0];
                        double mMidKyqd = mkyqdArray[1];
                        double mAvgKyqd = mkyqdArray.Average();


                        MItem[0]["JCJGMS"] = "";
                        //计算抗压平均、达到设计强度、及进行单组合格判定


                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) > 30)
                        {
                            MItem[0]["HG_ZNSJC"] = "重做";
                            sitem["ZNPJSJC"] = "重做";
                        }
                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                        if ((mMaxKyqd - mMidKyqd) > 30 && (mMidKyqd - mMinKyqd) <= 30)
                            sitem["ZNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) > 30)
                            sitem["ZNPJSJC"] = Round(mMidKyqd, 0).ToString();
                        //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                        if ((mMaxKyqd - mMidKyqd) <= 30 && (mMidKyqd - mMinKyqd) <= 30)
                            sitem["ZNPJSJC"] = Round(mAvgKyqd, 0).ToString();
                    }
                    if (sitem["ZNPJSJC"] == "重做")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true; jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        mFlag_Hg = true;
                        MItem[0]["HG_ZNSJC"] = IsQualified(MItem[0]["G_ZNSJC"], sitem["ZNPJSJC"]);
                    }

                    if (MItem[0]["HG_ZNSJC"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    MItem[0]["G_ZNSJC"] = "----";
                    sitem["ZNPJSJC"] = "---- - ";
                    MItem[0]["HG_ZNSJC"] = "----";
                }

                for (int qdi = 2; qdi <= 4; qdi++)
                {
                    double mhsxs = 0;
                    string mlq = string.Empty;
                    if (qdi == 1)
                        mlq = "1d";
                    if (qdi == 2)
                        mlq = "3d";
                    if (qdi == 3)
                        mlq = "7d";
                    if (qdi == 4)
                        mlq = "28d";
                    mhsxs = 1;
                    if (jcxm.Contains("、" + mlq.ToLower() + "抗压强度比、"))
                    {
                        jcxmCur = mlq + "抗压强度比";
                        mlq = mlq.ToUpper();

                        if (Conversion.Val(sitem["SJCD" + mlq]) == 100)
                            mhsxs = 0.95;
                        if (Conversion.Val(sitem["SJCD" + mlq]) == 150)
                            mhsxs = 1;
                        if (Conversion.Val(sitem["SJCD" + mlq]) == 200)
                            mhsxs = 1.05;

                        if (sitem["JHZ" + mlq + "1_1"] != "" && sitem["JHZ" + mlq + "1_1"] != "----")
                        {
                            for (xd1 = 1; xd1 <= 3; xd1++)
                            {
                                sum = 0;
                                for (xd2 = 1; xd2 <= 3; xd2++)
                                {
                                    md1 = Conversion.Val(sitem["JHZ" + mlq + xd1 + "_" + xd2]);
                                    md2 = Round(1000 * md1 / (Conversion.Val(sitem["SJCD" + mlq]) * Conversion.Val(sitem["SJKD" + mlq])), 1);
                                    Arrmd[xd2] = md2;
                                    sum = sum + Arrmd[xd2];
                                    sitem["JQD" + mlq + xd1 + "_" + xd2] = md2.ToString("0.0");
                                }
                                string mlongStr = Arrmd[1] + "," + Arrmd[2] + "," + Arrmd[3];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                double mMaxKyqd = mkyqdArray[2];
                                double mMinKyqd = mkyqdArray[0];
                                double mMidKyqd = mkyqdArray[1];
                                double mAvgKyqd = mkyqdArray.Average();
                                MItem[0]["JCJGMS"] = "";
                                //计算抗压平均、达到设计强度、及进行单组合格判定

                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sitem["JQDDBZ" + mlq + xd1] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sitem["JQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sitem["JQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sitem["JQDDBZ" + mlq + xd1] = Round(mAvgKyqd * mhsxs, 1).ToString("0.0");
                            }
                        }
                        if (sitem["SHZ" + mlq + "1_1"] != "" && sitem["SHZ" + mlq + "1_1"] != "----")
                        {
                            for (xd1 = 1; xd1 <= 3; xd1++)
                            {
                                sum = 0;
                                for (xd2 = 1; xd2 <= 3; xd2++)
                                {
                                    md1 = Conversion.Val(sitem["SHZ" + mlq + xd1 + "_" + xd2]);
                                    md2 = Round(1000 * md1 / (Conversion.Val(sitem["SJCD" + mlq]) * Conversion.Val(sitem["SJKD" + mlq])), 1);
                                    Arrmd[xd2] = md2;
                                    sum = sum + Arrmd[xd2];
                                    sitem["SQD" + mlq + xd1 + "_" + xd2] = md2.ToString("0.0");
                                }
                                string mlongStr = Arrmd[1] + "," + Arrmd[2] + "," + Arrmd[3];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                double mMaxKyqd = mkyqdArray[2];
                                double mMinKyqd = mkyqdArray[0];
                                double mMidKyqd = mkyqdArray[1];
                                double mAvgKyqd = mkyqdArray.Average();
                                MItem[0]["JCJGMS"] = "";
                                //计算抗压平均、达到设计强度、及进行单组合格判定

                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sitem["SQDDBZ" + mlq + xd1] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sitem["SQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 1))
                                    sitem["SQDDBZ" + mlq + xd1] = Round(mMidKyqd * mhsxs, 1).ToString("0.0");
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 1) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 1))
                                    sitem["SQDDBZ" + mlq + xd1] = Round(mAvgKyqd * mhsxs, 1).ToString("0.0");
                            }
                        }
                        if (sitem["JQDDBZ" + mlq + "1"] != "" && sitem["JQDDBZ" + mlq + "1"] != "----")
                        {
                            for (xd1 = 1; xd1 <= 3; xd1++)
                            {
                                if (sitem["JQDDBZ" + mlq + xd1] == "重做" || sitem["SQDDBZ" + mlq + xd1] == "重做")
                                    sitem["QDB" + mlq + xd1] = "重做";
                                else
                                    sitem["QDB" + mlq + xd1] = Round(Conversion.Val(sitem["SQDDBZ" + mlq + xd1]) / Conversion.Val(sitem["JQDDBZ" + mlq + xd1]) * 100, 0).ToString();
                            }
                        }
                        if (sitem["QDB" + mlq + "1"] != "" && sitem["QDB" + mlq + "1"] != "----")
                        {
                            if (sitem["QDB" + mlq + "1"] == "重做" || sitem["QDB" + mlq + "2"] == "重做" || sitem["QDB" + mlq + "3"] == "重做")
                                sitem["PJQDB" + mlq] = "重做";
                            else
                            {
                                string mlongStr = sitem["QDB" + mlq + "1"] + "," + sitem["QDB" + mlq + "2"] + "," + sitem["QDB" + mlq + "3"];
                                mtmpArray = mlongStr.Split(',');
                                for (vp = 0; vp <= 2; vp++)
                                    mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                                Array.Sort(mkyqdArray);
                                double mMaxKyqd = mkyqdArray[2];
                                double mMinKyqd = mkyqdArray[0];
                                double mMidKyqd = mkyqdArray[1];
                                double mAvgKyqd = mkyqdArray.Average();
                                MItem[0]["JCJGMS"] = "";
                                //计算抗压平均、达到设计强度、及进行单组合格判定

                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 0))
                                    sitem["PJQDB" + mlq] = "重做";
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) > Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 0))
                                    sitem["PJQDB" + mlq] = Round(mMidKyqd, 0).ToString();
                                //"最大最小强度值其中一个超出中间值的15%,试验结果取中间值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) > Round(mMidKyqd * 0.15, 0))
                                    sitem["PJQDB" + mlq] = Round(mMidKyqd, 0).ToString();
                                //"最大最小强度值均未超出中间值的15%,试验结果取平均值"
                                if ((mMaxKyqd - mMidKyqd) <= Round(mMidKyqd * 0.15, 0) && (mMidKyqd - mMinKyqd) <= Round(mMidKyqd * 0.15, 0))
                                    sitem["PJQDB" + mlq] = Round(mAvgKyqd, 0).ToString();

                            }

                        }
                        if (sitem["PJQDB" + mlq] == "重做")
                        {
                            mbhggs = mbhggs + 1;
                            MItem[0]["HG_KYQD" + mlq] = "重做";
                        }
                        else
                            MItem[0]["HG_KYQD" + mlq] = IsQualified(MItem[0]["G_KYQD" + mlq], sitem["PJQDB" + mlq]);

                        if (MItem[0]["HG_KYQD" + mlq] == "不合格")
                        {
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        else
                            mFlag_Hg = true;
                    }
                    else
                    {
                        MItem[0]["G_KYQD" + mlq] = "----";
                        sitem["PJQDB" + mlq] = "-----";
                        MItem[0]["HG_KYQD" + mlq] = "----";
                    }
                }
                if (jcxm.Contains("、收缩率比、"))
                {
                    jcxmCur = "收缩率比";
                    sitem["SSLJ1"] = Round((Conversion.Val(sitem["SSLJL0_1"]) - Conversion.Val(sitem["SSLJLT_1"])) / Conversion.Val(sitem["SSLJLB_1"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLJ2"] = Round((Conversion.Val(sitem["SSLJL0_2"]) - Conversion.Val(sitem["SSLJLT_2"])) / Conversion.Val(sitem["SSLJLB_2"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLJ3"] = Round((Conversion.Val(sitem["SSLJL0_3"]) - Conversion.Val(sitem["SSLJLT_3"])) / Conversion.Val(sitem["SSLJLB_3"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLS1"] = Round((Conversion.Val(sitem["SSLSL0_1"]) - Conversion.Val(sitem["SSLSLT_1"])) / Conversion.Val(sitem["SSLSLB_1"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLS2"] = Round((Conversion.Val(sitem["SSLSL0_2"]) - Conversion.Val(sitem["SSLSLT_2"])) / Conversion.Val(sitem["SSLSLB_2"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLS3"] = Round((Conversion.Val(sitem["SSLSL0_3"]) - Conversion.Val(sitem["SSLSLT_3"])) / Conversion.Val(sitem["SSLSLB_3"]) * Math.Pow(10, 6), 1).ToString();
                    sitem["SSLB1"] = Round(Conversion.Val(sitem["SSLS1"]) / Conversion.Val(sitem["SSLJ1"]) * 100, 1).ToString();
                    sitem["SSLB2"] = Round(Conversion.Val(sitem["SSLS2"]) / Conversion.Val(sitem["SSLJ2"]) * 100, 1).ToString();
                    sitem["SSLB3"] = Round(Conversion.Val(sitem["SSLS3"]) / Conversion.Val(sitem["SSLJ3"]) * 100, 1).ToString();
                    sitem["SSLB"] = Round((Conversion.Val(sitem["SSLB1"]) + Conversion.Val(sitem["SSLB2"]) + Conversion.Val(sitem["SSLB3"])) / 3, 0).ToString();

                    MItem[0]["HG_SSLB"] = IsQualified(MItem[0]["G_SSLB"], sitem["SSLB"]);
                    if (MItem[0]["HG_SSLB"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["SSLB"] = "----";
                    MItem[0]["HG_SSLB"] = "----";
                    MItem[0]["G_SSLB"] = "----";
                }
                if (jcxm.Contains("、吸水量比、"))
                {
                    jcxmCur = "吸水量比";
                    double mjxsl1 = Conversion.Val(sitem["JXSLM1_1"]) - Conversion.Val(sitem["JXSLM0_1"]);
                    double mjxsl2 = Conversion.Val(sitem["JXSLM1_2"]) - Conversion.Val(sitem["JXSLM0_2"]);
                    double mjxsl3 = Conversion.Val(sitem["JXSLM1_3"]) - Conversion.Val(sitem["JXSLM0_3"]);
                    double mjxsl4 = Conversion.Val(sitem["JXSLM1_4"]) - Conversion.Val(sitem["JXSLM0_4"]);
                    double mjxsl5 = Conversion.Val(sitem["JXSLM1_5"]) - Conversion.Val(sitem["JXSLM0_5"]);
                    double mjxsl6 = Conversion.Val(sitem["JXSLM1_6"]) - Conversion.Val(sitem["JXSLM0_6"]);
                    double msxsl1 = Conversion.Val(sitem["SXSLM1_1"]) - Conversion.Val(sitem["SXSLM0_1"]);
                    double msxsl2 = Conversion.Val(sitem["SXSLM1_2"]) - Conversion.Val(sitem["SXSLM0_2"]);
                    double msxsl3 = Conversion.Val(sitem["SXSLM1_3"]) - Conversion.Val(sitem["SXSLM0_3"]);
                    double msxsl4 = Conversion.Val(sitem["SXSLM1_4"]) - Conversion.Val(sitem["SXSLM0_4"]);
                    double msxsl5 = Conversion.Val(sitem["SXSLM1_5"]) - Conversion.Val(sitem["SXSLM0_5"]);
                    double msxsl6 = Conversion.Val(sitem["SXSLM1_6"]) - Conversion.Val(sitem["SXSLM0_6"]);
                    sitem["JXSL"] = Round((mjxsl1 + mjxsl2 + mjxsl3 + mjxsl4 + mjxsl5 + mjxsl6) / 6, 0).ToString();
                    sitem["SXSL"] = Round((msxsl1 + msxsl2 + msxsl3 + msxsl4 + msxsl5 + msxsl6) / 6, 0).ToString();
                    sitem["XSLB"] = Round(100 * (Conversion.Val(sitem["SXSL"]) / Conversion.Val(sitem["JXSL"])), 0).ToString();
                    MItem[0]["HG_XSLB"] = IsQualified(MItem[0]["G_XSLB"], sitem["XSLB"]);

                    if (MItem[0]["HG_XSLB"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                        mFlag_Hg = true;
                }
                else
                {
                    sitem["XSLB"] = "----";
                    MItem[0]["HG_XSLB"] = "----";
                    MItem[0]["G_XSLB"] = "----";
                }
                MItem[0]["JCJGMS"] = "";

                if (mbhggs == 0)
                {
                    sitem["JCJG"] = "合格";
                }
                else
                {
                    sitem["JCJG"] = "不合格";

                }
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");
            }
           
            //综合判断
            if (mbhggs == 0)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            if (mbhggs > 0)
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //if (mFlag_Bhg && mFlag_Hg)
                //    MItem[0]["JCJGMS"] = "依据标准" + MItem[0]["PDBZ"] + ",所检项目" + jcxmBhg.TrimEnd('、') + "不符合标准要求。";
            }
            #endregion
            /************************ 代码结束 *********************/

        }
    }
}
