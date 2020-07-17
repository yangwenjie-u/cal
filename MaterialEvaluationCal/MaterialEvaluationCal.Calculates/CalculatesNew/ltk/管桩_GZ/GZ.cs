using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GZ : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/
            #region 参数定义

            string jcxm, mcalBh, mlongStr, mMaxBgbh, mJSFF, gh_bh, stemp, stravg, strmin;
            gh_bh = "";
            bool mAllHg, msffs, mGetBgbh, flag, isOk;
            int mbhggs, mbhggs2, sign, kybhg, Gs, cnpd, znpd;
            sign = 0;
            mbhggs = 0;

            var czs = 0;
            Double md1, md2, md3, md4, md5, md6, md, xd1, xd2, xd, llz, pcbhgs, kz, bl, mGJZJ, pjmd, sum;
            double[] nmd = new double[5];
            double[] dArray;
            var jcxmBhg = "";
            var jcxmCur = "";
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_GZ_DJ"]; 
            var mrsPj = dataExtra["BZ_GZPJQK"];
            var MItem = data["M_GZ"];
            var SItem = data["S_GZ"];
            #endregion

            #region  计算开始
            MItem[0]["JCJGMS"] = "";
            foreach (var sitem in SItem)
            {
                IDictionary<String, String> mrsDj_Filter;
                jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                #region 等级表处理
                if (sitem["CPMC"].Contains("预应力混凝土空心方桩"))
                {

                    mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"] == (sitem["CPMC"]) && x["WJ"] == (sitem["WJ"]) && x["ZJM"] == "2006");

                }
                else
                {
                    mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"] == (sitem["CPMC"]) && x["WJ"] == (sitem["WJ"]) && x["ZJM"] == (sitem["BZH"]) && x["XH"] == (sitem["XH"]) && x["BCBH"] == (sitem["BCBH"]));

                }
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    sitem["G_KL"] = mrsDj_Filter["KLWJ"];
                    sitem["G_JX"] = mrsDj_Filter["JXWJ"];
                    sign = sign + 1;
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "试件尺寸为空";
                    break;
                }
                #endregion

                #region 抗弯性能
                if (jcxm.Contains("、抗弯性能、"))
                {
                    jcxmCur = "抗弯性能";
                    if (sitem["SFFJ"] == "否")
                    {
                        #region S_JX1 S_KL1
                        if (GetSafeDouble(sitem["S_JX1"]) > GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX11"] = sitem["S_JX1"] + ">";
                            sitem["KW1_GH"] = "合格";
                        }
                        else if (GetSafeDouble(sitem["S_JX1"]) == GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX11"] = sitem["S_JX1"] + "=";
                            sitem["KW1_GH"] = "合格";
                        }
                        else 
                        {
                            sitem["S_JX11"] = sitem["S_JX1"] + "<";
                            sitem["KW1_GH"] = "不合格";
                            mbhggs += 1;
                        }
                       
                        if (GetSafeDouble(sitem["S_KL1"]) <= GetSafeDouble(sitem["G_KL"]))
                        {
                            sitem["KW1_GH"] = "不合格";
                            mbhggs += 1;

                        }
                        #endregion
                        #region S_JX2 S_KL2
                        if (GetSafeDouble(sitem["S_JX2"]) > GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX22"] = sitem["S_JX2"] + ">";
                           
                            sitem["KW2_GH"] = "合格";
                         

                        }
                        else if (GetSafeDouble(sitem["S_JX2"]) == GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX22"] = sitem["S_JX2"] + "=";
                           
                            sitem["KW2_GH"] = "合格";
                          

                        }
                        else
                        {
                            sitem["S_JX22"] = sitem["S_JX2"] + "<";

                            sitem["KW2_GH"] = "不合格";
                            mbhggs += 1;
                        }
                        if (GetSafeDouble(sitem["S_KL2"]) <= GetSafeDouble(sitem["G_KL"]))
                        {
                            sitem["KW2_GH"] = "不合格";
                            mbhggs += 1;
                        }
                        #endregion
                    }

                    if (sitem["SFFJ"] == "是")
                    {
                        #region S_JX1 S_KL1
                        if (GetSafeDouble(sitem["S_JX1"]) > GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX11"] = sitem["S_JX1"] + ">";
                            sitem["KW1_GH"] = "合格";
                        }
                        else if (GetSafeDouble(sitem["S_JX1"]) == GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX11"] = sitem["S_JX1"] + "=";
                            sitem["KW1_GH"] = "合格";
                        }
                        else
                        {
                            sitem["S_JX11"] = sitem["S_JX1"] + "<";
                            sitem["KW1_GH"] = "不合格";
                            mbhggs += 1;
                        }

                        if (GetSafeDouble(sitem["S_KL1"]) <= GetSafeDouble(sitem["G_KL"]))
                        {
                            sitem["KW1_GH"] = "不合格";
                            mbhggs += 1;

                        }
                        #endregion
                        #region S_JX2 S_KL2
                        if (GetSafeDouble(sitem["S_JX2"]) > GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX22"] = sitem["S_JX2"] + ">";

                            sitem["KW2_GH"] = "合格";
                           

                        }
                        else if (GetSafeDouble(sitem["S_JX2"]) == GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX22"] = sitem["S_JX2"] + "=";

                            sitem["KW2_GH"] = "合格";
                       

                        }
                        else
                        {
                            sitem["S_JX22"] = sitem["S_JX2"] + "<";

                            sitem["KW2_GH"] = "不合格";
                            mbhggs += 1;
                        }
                        if (GetSafeDouble(sitem["S_KL2"]) <= GetSafeDouble(sitem["G_KL"]))
                        {
                            sitem["KW2_GH"] = "不合格";
                            mbhggs += 1;
                        }
                        #endregion
                        #region S_JX3 S_KL3
                        if (GetSafeDouble(sitem["S_JX3"]) > GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX33"] = sitem["S_JX3"] + ">";
                            sitem["KW3_GH"] = "合格";
                        }
                        else if (GetSafeDouble(sitem["S_JX3"]) == GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX33"] = sitem["S_JX3"] + "=";
                            sitem["KW3_GH"] = "合格";
                        }
                        else
                        {
                            sitem["S_JX33"] = sitem["S_JX3"] + "<";
                            sitem["KW3_GH"] = "不合格";
                            mbhggs += 1;
                        }

                        if (GetSafeDouble(sitem["S_KL3"]) <= GetSafeDouble(sitem["G_KL"]))
                        {
                            sitem["KW3_GH"] = "不合格";
                            mbhggs += 1;

                        }
                        #endregion
                        #region S_JX4 S_KL4
                        if (GetSafeDouble(sitem["S_JX4"]) > GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX44"] = sitem["S_JX4"] + ">";

                            sitem["KW4_GH"] = "合格";


                        }
                        else if (GetSafeDouble(sitem["S_JX4"]) == GetSafeDouble(sitem["G_JX"]))
                        {
                            sitem["S_JX44"] = sitem["S_JX4"] + "=";

                            sitem["KW4_GH"] = "合格";


                        }
                        else
                        {
                            sitem["S_JX44"] = sitem["S_JX4"] + "<";

                            sitem["KW4_GH"] = "不合格";
                            mbhggs += 1;
                        }
                        if (GetSafeDouble(sitem["S_KL4"]) <= GetSafeDouble(sitem["G_KL"]))
                        {
                            sitem["KW4_GH"] = "不合格";
                            mbhggs += 1;
                        }
                        #endregion
                    }


                }
                #endregion
                
                #region 钢筋尺寸
                if (jcxm.Contains("、钢筋尺寸、"))
                {
                    jcxmCur = "钢筋尺寸";
                    isOk = true; 
                    var mrsPj_Filter = mrsDj.FirstOrDefault(x => x["MC"] == (sitem["GGXH"]) );
                    if (mrsPj_Filter != null && mrsPj_Filter.Count > 0)
                    {
                        sitem["G_JMQLJ"] = mrsPj_Filter["JMQLJ"];
                        sitem["G_JMQLJPC"] = mrsPj_Filter["JMQLJPC"];
                        sitem["G_LJ"] = mrsPj_Filter["LJ"];
                        sitem["G_LJPC"] = mrsPj_Filter["LJPC"];
                        sitem["G_JMQGJCD"] = mrsPj_Filter["JMQGJCD"];
                        mGJZJ = GetSafeDouble(mrsPj_Filter["JMQGJCD"]);
                        sitem["G_GJJJPC"] = mrsPj_Filter["GJJJPC"];

                    }
                    //左端
                    if(( GetSafeDouble( sitem["L1_JMQLJ1"]) > GetSafeDouble(sitem["G_JMQLJ"]) + GetSafeDouble(sitem["G_JMQLJPC"]))| (GetSafeDouble(sitem["L1_JMQLJ2"]) > GetSafeDouble(sitem["G_JMQLJ"]) - GetSafeDouble(sitem["G_JMQLJPC"])))
                    {
                        sitem["L1_JMQLJ3"] = "不合格";
                        isOk = false;
                    }
                }
                #endregion

            }

            #endregion
            /************************ 代码结束 *********************/
#endregion
        }
    }
}