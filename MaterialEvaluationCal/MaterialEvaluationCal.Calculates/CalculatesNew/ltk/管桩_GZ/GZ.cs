using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Calculates
{
    public class GZ : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/
            
            #region 参数定义

            string jcxm,  gh_bh, stemp, stravg,  strmin;
            gh_bh = "";
            bool mAllHg, mGetBgbh, flag, isOk;
            int mbhggs, mbhggs2, sign, Gs;
            mbhggs2 = 0;sign = 0;mbhggs = 0;

            var czs = 0;
            Double md1, md2, md3, md4, md5, md6, md,  pcbhgs,  mGJZJ, pjmd, sum;
            int xd = 0;
            mGJZJ =0;md2 = 0;md3 = 0;md4 = 0;md5 = 0;md6 = 0;
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
                        //S_JX极限弯矩检测结果;S_KL抗裂弯矩检测结果
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
                    #region 等级表
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

                    #endregion                    
                    
                    //1
                    #region 左端
                    if (( GetSafeDouble( sitem["L1_JMQLJ1"]) > GetSafeDouble(sitem["G_JMQLJ"]) + GetSafeDouble(sitem["G_JMQLJPC"]))| (GetSafeDouble(sitem["L1_JMQLJ2"]) > GetSafeDouble(sitem["G_JMQLJ"]) - GetSafeDouble(sitem["G_JMQLJPC"])))
                    {
                        sitem["L1_JMQLJ3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["L1_JMQLJ3"] = "合格";
                    }
                    if(GetSafeDouble(sitem["L1_JMQGJCD2"]) < GetSafeDouble(sitem["G_JMQGJCD"])){
                        sitem["L1_JMQGJCD3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["L1_JMQGJCD3"] = "合格";
                    }
                    switch (mGJZJ)
                    {
                        case 7.1:
                            if (GetSafeDouble(sitem["L1_GJZJ1"]) >7.4 | GetSafeDouble(sitem["L1_GJZJ1"])<7.1 | GetSafeDouble(sitem["L1_GJZJ2"])>7.4 | GetSafeDouble(sitem["L1_GJZJ2"])< 7.1)
                            {
                                sitem["L1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["L1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "7.25±0.15";
                            break;
                        case 9.0:
                            if (GetSafeDouble(sitem["L1_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["L1_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["L1_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["L1_GJZJ2"]) < 8.95)
                            {
                                sitem["L1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["L1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "9.15±0.20";
                            break;
                        case 10.7:
                            if (GetSafeDouble(sitem["L1_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["L1_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["L1_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["L1_GJZJ2"]) < 10.9)
                            {
                                sitem["L1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["L1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "11.10±0.20";
                            break;
                        case 12.6:
                            if (GetSafeDouble(sitem["L1_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["L1_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["L1_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["L1_GJZJ2"]) < 12.9)
                            {
                                sitem["L1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["L1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "13.10±0.20";
                            break;
                    }
                    if (GetSafeDouble(sitem["L1_GJJJPC1"])!= GetSafeDouble(sitem["G_GJJJPC"])){
                        sitem["L1_GJJJPC3"] = "不合格";
                        isOk = false;

                    }
                    else
                    {
                        sitem["L1_GJJJPC3"] = "合格";
                    }
                    #endregion
                    #region 中间
                    if (GetSafeDouble(sitem["M1_LJ1"]) > GetSafeDouble(sitem["G_LJ"])+ GetSafeDouble(sitem["G_LJPC"]) | GetSafeDouble(sitem["G_LJ"]) <  GetSafeDouble(sitem["G_LJ"]) - GetSafeDouble(sitem["G_LJPC"]))
                    {
                        sitem["M1_LJ3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["M1_LJ3"] = "合格";
                    }
                    switch (mGJZJ)
                    {
                        case 7.1:
                            if (GetSafeDouble(sitem["M1_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["M1_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["M1_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["M1_GJZJ2"]) < 7.1)
                            {
                                sitem["M1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["M1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "7.25±0.15";
                            break;
                        case 9.0:
                            if (GetSafeDouble(sitem["M1_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["M1_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["M1_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["M1_GJZJ2"]) < 8.95)
                            {
                                sitem["M1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["M1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "9.15±0.20";
                            break;
                        case 10.7:
                            if (GetSafeDouble(sitem["M1_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["M1_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["M1_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["M1_GJZJ2"]) < 10.9)
                            {
                                sitem["M1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["M1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "11.10±0.20";
                            break;
                        case 12.6:
                            if (GetSafeDouble(sitem["M1_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["M1_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["M1_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["M1_GJZJ2"]) < 12.9)
                            {
                                sitem["M1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["M1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "13.10±0.20";
                            break;
                   
                    }
                    if (GetSafeDouble(sitem["M1_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                    {
                        sitem["M1_GJJJPC3"] = "不合格";
                        isOk = false;

                    }
                    else
                    {
                        sitem["M1_GJJJPC3"] = "合格";
                    }
                    #endregion
                    #region 右端

                    if ((GetSafeDouble(sitem["R1_JMQLJ1"]) > GetSafeDouble(sitem["G_JMQLJ"]) + GetSafeDouble(sitem["G_JMQLJPC"])) | (GetSafeDouble(sitem["R1_JMQLJ2"]) > GetSafeDouble(sitem["G_JMQLJ"]) - GetSafeDouble(sitem["G_JMQLJPC"])))
                    {
                        sitem["R1_JMQLJ3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["R1_JMQLJ3"] = "合格";
                    }
                    if (GetSafeDouble(sitem["R1_JMQGJCD2"]) < GetSafeDouble(sitem["G_JMQGJCD"]))
                    {
                        sitem["R1_JMQGJCD3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["R1_JMQGJCD3"] = "合格";
                    }
                    switch (mGJZJ)
                    {
                        case 7.1:
                            if (GetSafeDouble(sitem["R1_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["R1_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["R1_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["R1_GJZJ2"]) < 7.1)
                            {
                                sitem["R1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["R1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "7.25±0.15";
                            break;
                        case 9.0:
                            if (GetSafeDouble(sitem["R1_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["R1_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["R1_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["R1_GJZJ2"]) < 8.95)
                            {
                                sitem["R1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["R1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "9.15±0.20";
                            break;
                        case 10.7:
                            if (GetSafeDouble(sitem["R1_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["R1_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["R1_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["R1_GJZJ2"]) < 10.9)
                            {
                                sitem["R1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["R1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "11.10±0.20";
                            break;
                        case 12.6:
                            if (GetSafeDouble(sitem["R1_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["R1_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["R1_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["R1_GJZJ2"]) < 12.9)
                            {
                                sitem["R1_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["R1_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "13.10±0.20";
                            break;
                    }
                    if (GetSafeDouble(sitem["R1_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                    {
                        sitem["R1_GJJJPC3"] = "不合格";
                        isOk = false;

                    }
                    else
                    {
                        sitem["R1_GJJJPC3"] = "合格";
                    }
                    if (!isOk)
                    {
                        mbhggs2 = mbhggs2 + 1;
                    }


                    #endregion

                    //2
                    #region 左端
                    if ((GetSafeDouble(sitem["L2_JMQLJ1"]) > GetSafeDouble(sitem["G_JMQLJ"]) + GetSafeDouble(sitem["G_JMQLJPC"])) | (GetSafeDouble(sitem["L2_JMQLJ2"]) > GetSafeDouble(sitem["G_JMQLJ"]) - GetSafeDouble(sitem["G_JMQLJPC"])))
                    {
                        sitem["L2_JMQLJ3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["L2_JMQLJ3"] = "合格";
                    }
                    if (GetSafeDouble(sitem["L2_JMQGJCD2"]) < GetSafeDouble(sitem["G_JMQGJCD"]))
                    {
                        sitem["L2_JMQGJCD3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["L2_JMQGJCD3"] = "合格";
                    }
                    switch (mGJZJ)
                    {
                        case 7.1:
                            if (GetSafeDouble(sitem["L2_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["L2_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["L2_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["L2_GJZJ2"]) < 7.1)
                            {
                                sitem["L2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["L2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "7.25±0.15";
                            break;
                        case 9.0:
                            if (GetSafeDouble(sitem["L2_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["L2_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["L2_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["L2_GJZJ2"]) < 8.95)
                            {
                                sitem["L2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["L2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "9.15±0.20";
                            break;
                        case 10.7:
                            if (GetSafeDouble(sitem["L2_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["L2_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["L2_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["L2_GJZJ2"]) < 10.9)
                            {
                                sitem["L2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["L2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "11.10±0.20";
                            break;
                        case 12.6:
                            if (GetSafeDouble(sitem["L2_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["L2_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["L2_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["L2_GJZJ2"]) < 12.9)
                            {
                                sitem["L2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["L2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "13.10±0.20";
                            break;
                    }
                    if (GetSafeDouble(sitem["L2_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                    {
                        sitem["L2_GJJJPC3"] = "不合格";
                        isOk = false;

                    }
                    else
                    {
                        sitem["L2_GJJJPC3"] = "合格";
                    }
                    #endregion
                    #region 中间
                    if (GetSafeDouble(sitem["M2_LJ1"]) > GetSafeDouble(sitem["G_LJ"]) + GetSafeDouble(sitem["G_LJPC"]) | GetSafeDouble(sitem["G_LJ"]) < GetSafeDouble(sitem["G_LJ"]) - GetSafeDouble(sitem["G_LJPC"]))
                    {
                        sitem["M2_LJ3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["M2_LJ3"] = "合格";
                    }
                    switch (mGJZJ)
                    {
                        case 7.1:
                            if (GetSafeDouble(sitem["M2_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["M2_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["M2_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["M2_GJZJ2"]) < 7.1)
                            {
                                sitem["M2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["M2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "7.25±0.15";
                            break;
                        case 9.0:
                            if (GetSafeDouble(sitem["M2_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["M2_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["M2_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["M2_GJZJ2"]) < 8.95)
                            {
                                sitem["M2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["M2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "9.15±0.20";
                            break;
                        case 10.7:
                            if (GetSafeDouble(sitem["M2_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["M2_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["M2_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["M2_GJZJ2"]) < 10.9)
                            {
                                sitem["M2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["M2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "11.10±0.20";
                            break;
                        case 12.6:
                            if (GetSafeDouble(sitem["M2_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["M2_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["M2_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["M2_GJZJ2"]) < 12.9)
                            {
                                sitem["M2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["M2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "13.10±0.20";
                            break;

                    }
                    if (GetSafeDouble(sitem["M2_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                    {
                        sitem["M2_GJJJPC3"] = "不合格";
                        isOk = false;

                    }
                    else
                    {
                        sitem["M2_GJJJPC3"] = "合格";
                    }
                    #endregion
                    #region 右端

                    if ((GetSafeDouble(sitem["R2_JMQLJ1"]) > GetSafeDouble(sitem["G_JMQLJ"]) + GetSafeDouble(sitem["G_JMQLJPC"])) | (GetSafeDouble(sitem["R2_JMQLJ2"]) > GetSafeDouble(sitem["G_JMQLJ"]) - GetSafeDouble(sitem["G_JMQLJPC"])))
                    {
                        sitem["R2_JMQLJ3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["R2_JMQLJ3"] = "合格";
                    }
                    if (GetSafeDouble(sitem["R2_JMQGJCD2"]) < GetSafeDouble(sitem["G_JMQGJCD"]))
                    {
                        sitem["R2_JMQGJCD3"] = "不合格";
                        isOk = false;
                    }
                    else
                    {
                        sitem["R2_JMQGJCD3"] = "合格";
                    }
                    switch (mGJZJ)
                    {
                        case 7.1:
                            if (GetSafeDouble(sitem["R2_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["R2_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["R2_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["R2_GJZJ2"]) < 7.1)
                            {
                                sitem["R2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["R2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "7.25±0.15";
                            break;
                        case 9.0:
                            if (GetSafeDouble(sitem["R2_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["R2_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["R2_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["R2_GJZJ2"]) < 8.95)
                            {
                                sitem["R2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["R2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "9.15±0.20";
                            break;
                        case 10.7:
                            if (GetSafeDouble(sitem["R2_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["R2_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["R2_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["R2_GJZJ2"]) < 10.9)
                            {
                                sitem["R2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["R2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "11.10±0.20";
                            break;
                        case 12.6:
                            if (GetSafeDouble(sitem["R2_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["R2_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["R2_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["R2_GJZJ2"]) < 12.9)
                            {
                                sitem["R2_GJZJ3"] = "不合格";
                                isOk = false;
                            }
                            else
                            {
                                sitem["R2_GJZJ3"] = "合格";
                            }
                            sitem["G_GJZJ"] = "13.10±0.20";
                            break;
                    }
                    if (GetSafeDouble(sitem["R2_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                    {
                        sitem["R2_GJJJPC3"] = "不合格";
                        isOk = false;

                    }
                    else
                    {
                        sitem["R2_GJJJPC3"] = "合格";
                    }
                    if (!isOk)
                    {
                        mbhggs2 = mbhggs2 + 1;
                    }


                    #endregion
                    if (sitem["SFFJ"] == "是")
                    { 
                        //3
                        #region 左端
                        if ((GetSafeDouble(sitem["L3_JMQLJ1"]) > GetSafeDouble(sitem["G_JMQLJ"]) + GetSafeDouble(sitem["G_JMQLJPC"])) | (GetSafeDouble(sitem["L3_JMQLJ2"]) > GetSafeDouble(sitem["G_JMQLJ"]) - GetSafeDouble(sitem["G_JMQLJPC"])))
                        {
                            sitem["L3_JMQLJ3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["L3_JMQLJ3"] = "合格";
                        }
                        if (GetSafeDouble(sitem["L3_JMQGJCD2"]) < GetSafeDouble(sitem["G_JMQGJCD"]))
                        {
                            sitem["L3_JMQGJCD3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["L3_JMQGJCD3"] = "合格";
                        }
                        switch (mGJZJ)
                        {
                            case 7.1:
                                if (GetSafeDouble(sitem["L3_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["L3_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["L3_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["L3_GJZJ2"]) < 7.1)
                                {
                                    sitem["L3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["L3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "7.25±0.15";
                                break;
                            case 9.0:
                                if (GetSafeDouble(sitem["L3_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["L3_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["L3_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["L3_GJZJ2"]) < 8.95)
                                {
                                    sitem["L3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["L3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "9.15±0.20";
                                break;
                            case 10.7:
                                if (GetSafeDouble(sitem["L3_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["L3_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["L3_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["L3_GJZJ2"]) < 10.9)
                                {
                                    sitem["L3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["L3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "11.10±0.20";
                                break;
                            case 12.6:
                                if (GetSafeDouble(sitem["L3_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["L3_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["L3_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["L3_GJZJ2"]) < 12.9)
                                {
                                    sitem["L3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["L3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "13.10±0.20";
                                break;
                        }
                        if (GetSafeDouble(sitem["L3_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                        {
                            sitem["L3_GJJJPC3"] = "不合格";
                            isOk = false;

                        }
                        else
                        {
                            sitem["L3_GJJJPC3"] = "合格";
                        }
                        #endregion
                        #region 中间
                        if (GetSafeDouble(sitem["M3_LJ1"]) > GetSafeDouble(sitem["G_LJ"]) + GetSafeDouble(sitem["G_LJPC"]) | GetSafeDouble(sitem["G_LJ"]) < GetSafeDouble(sitem["G_LJ"]) - GetSafeDouble(sitem["G_LJPC"]))
                        {
                            sitem["M3_LJ3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["M3_LJ3"] = "合格";
                        }
                        switch (mGJZJ)
                        {
                            case 7.1:
                                if (GetSafeDouble(sitem["M3_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["M3_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["M3_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["M3_GJZJ2"]) < 7.1)
                                {
                                    sitem["M3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["M3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "7.25±0.15";
                                break;
                            case 9.0:
                                if (GetSafeDouble(sitem["M3_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["M3_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["M3_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["M3_GJZJ2"]) < 8.95)
                                {
                                    sitem["M3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["M3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "9.15±0.20";
                                break;
                            case 10.7:
                                if (GetSafeDouble(sitem["M3_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["M3_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["M3_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["M3_GJZJ2"]) < 10.9)
                                {
                                    sitem["M3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["M3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "11.10±0.20";
                                break;
                            case 12.6:
                                if (GetSafeDouble(sitem["M3_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["M3_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["M3_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["M3_GJZJ2"]) < 12.9)
                                {
                                    sitem["M3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["M3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "13.10±0.20";
                                break;

                        }
                        if (GetSafeDouble(sitem["M3_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                        {
                            sitem["M3_GJJJPC3"] = "不合格";
                            isOk = false;

                        }
                        else
                        {
                            sitem["M3_GJJJPC3"] = "合格";
                        }
                        #endregion
                        #region 右端

                        if ((GetSafeDouble(sitem["R3_JMQLJ1"]) > GetSafeDouble(sitem["G_JMQLJ"]) + GetSafeDouble(sitem["G_JMQLJPC"])) | (GetSafeDouble(sitem["R3_JMQLJ2"]) > GetSafeDouble(sitem["G_JMQLJ"]) - GetSafeDouble(sitem["G_JMQLJPC"])))
                        {
                            sitem["R3_JMQLJ3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["R3_JMQLJ3"] = "合格";
                        }
                        if (GetSafeDouble(sitem["R3_JMQGJCD2"]) < GetSafeDouble(sitem["G_JMQGJCD"]))
                        {
                            sitem["R3_JMQGJCD3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["R3_JMQGJCD3"] = "合格";
                        }
                        switch (mGJZJ)
                        {
                            case 7.1:
                                if (GetSafeDouble(sitem["R3_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["R3_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["R3_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["R3_GJZJ2"]) < 7.1)
                                {
                                    sitem["R3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["R3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "7.25±0.15";
                                break;
                            case 9.0:
                                if (GetSafeDouble(sitem["R3_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["R3_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["R3_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["R3_GJZJ2"]) < 8.95)
                                {
                                    sitem["R3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["R3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "9.15±0.20";
                                break;
                            case 10.7:
                                if (GetSafeDouble(sitem["R3_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["R3_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["R3_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["R3_GJZJ2"]) < 10.9)
                                {
                                    sitem["R3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["R3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "11.10±0.20";
                                break;
                            case 12.6:
                                if (GetSafeDouble(sitem["R3_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["R3_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["R3_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["R3_GJZJ2"]) < 12.9)
                                {
                                    sitem["R3_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["R3_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "13.10±0.20";
                                break;
                        }
                        if (GetSafeDouble(sitem["R3_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                        {
                            sitem["R3_GJJJPC3"] = "不合格";
                            isOk = false;

                        }
                        else
                        {
                            sitem["R3_GJJJPC3"] = "合格";
                        }
                        if (!isOk)
                        {
                            mbhggs2 = mbhggs2 + 1;
                        }


                        #endregion

                        //4
                        #region 左端
                        if ((GetSafeDouble(sitem["L4_JMQLJ1"]) > GetSafeDouble(sitem["G_JMQLJ"]) + GetSafeDouble(sitem["G_JMQLJPC"])) | (GetSafeDouble(sitem["L4_JMQLJ2"]) > GetSafeDouble(sitem["G_JMQLJ"]) - GetSafeDouble(sitem["G_JMQLJPC"])))
                        {
                            sitem["L4_JMQLJ3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["L4_JMQLJ3"] = "合格";
                        }
                        if (GetSafeDouble(sitem["L4_JMQGJCD2"]) < GetSafeDouble(sitem["G_JMQGJCD"]))
                        {
                            sitem["L4_JMQGJCD3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["L4_JMQGJCD3"] = "合格";
                        }
                        switch (mGJZJ)
                        {
                            case 7.1:
                                if (GetSafeDouble(sitem["L4_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["L4_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["L4_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["L4_GJZJ2"]) < 7.1)
                                {
                                    sitem["L4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["L4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "7.25±0.15";
                                break;
                            case 9.0:
                                if (GetSafeDouble(sitem["L4_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["L4_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["L4_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["L4_GJZJ2"]) < 8.95)
                                {
                                    sitem["L4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["L4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "9.15±0.20";
                                break;
                            case 10.7:
                                if (GetSafeDouble(sitem["L4_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["L4_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["L4_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["L4_GJZJ2"]) < 10.9)
                                {
                                    sitem["L4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["L4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "11.10±0.20";
                                break;
                            case 12.6:
                                if (GetSafeDouble(sitem["L4_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["L4_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["L4_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["L4_GJZJ2"]) < 12.9)
                                {
                                    sitem["L4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["L4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "13.10±0.20";
                                break;
                        }
                        if (GetSafeDouble(sitem["L4_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                        {
                            sitem["L4_GJJJPC3"] = "不合格";
                            isOk = false;

                        }
                        else
                        {
                            sitem["L4_GJJJPC3"] = "合格";
                        }
                        #endregion
                        #region 中间
                        if (GetSafeDouble(sitem["M4_LJ1"]) > GetSafeDouble(sitem["G_LJ"]) + GetSafeDouble(sitem["G_LJPC"]) | GetSafeDouble(sitem["G_LJ"]) < GetSafeDouble(sitem["G_LJ"]) - GetSafeDouble(sitem["G_LJPC"]))
                        {
                            sitem["M4_LJ3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["M4_LJ3"] = "合格";
                        }
                        switch (mGJZJ)
                        {
                            case 7.1:
                                if (GetSafeDouble(sitem["M4_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["M4_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["M4_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["M4_GJZJ2"]) < 7.1)
                                {
                                    sitem["M4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["M4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "7.25±0.15";
                                break;
                            case 9.0:
                                if (GetSafeDouble(sitem["M4_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["M4_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["M4_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["M4_GJZJ2"]) < 8.95)
                                {
                                    sitem["M4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["M4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "9.15±0.20";
                                break;
                            case 10.7:
                                if (GetSafeDouble(sitem["M4_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["M4_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["M4_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["M4_GJZJ2"]) < 10.9)
                                {
                                    sitem["M4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["M4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "11.10±0.20";
                                break;
                            case 12.6:
                                if (GetSafeDouble(sitem["M4_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["M4_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["M4_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["M4_GJZJ2"]) < 12.9)
                                {
                                    sitem["M4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["M4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "13.10±0.20";
                                break;

                        }
                        if (GetSafeDouble(sitem["M4_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                        {
                            sitem["M4_GJJJPC3"] = "不合格";
                            isOk = false;

                        }
                        else
                        {
                            sitem["M4_GJJJPC3"] = "合格";
                        }
                        #endregion
                        #region 右端

                        if ((GetSafeDouble(sitem["R4_JMQLJ1"]) > GetSafeDouble(sitem["G_JMQLJ"]) + GetSafeDouble(sitem["G_JMQLJPC"])) | (GetSafeDouble(sitem["R4_JMQLJ2"]) > GetSafeDouble(sitem["G_JMQLJ"]) - GetSafeDouble(sitem["G_JMQLJPC"])))
                        {
                            sitem["R4_JMQLJ3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["R4_JMQLJ3"] = "合格";
                        }
                        if (GetSafeDouble(sitem["R4_JMQGJCD2"]) < GetSafeDouble(sitem["G_JMQGJCD"]))
                        {
                            sitem["R4_JMQGJCD3"] = "不合格";
                            isOk = false;
                        }
                        else
                        {
                            sitem["R4_JMQGJCD3"] = "合格";
                        }
                        switch (mGJZJ)
                        {
                            case 7.1:
                                if (GetSafeDouble(sitem["R4_GJZJ1"]) > 7.4 | GetSafeDouble(sitem["R4_GJZJ1"]) < 7.1 | GetSafeDouble(sitem["R4_GJZJ2"]) > 7.4 | GetSafeDouble(sitem["R4_GJZJ2"]) < 7.1)
                                {
                                    sitem["R4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["R4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "7.25±0.15";
                                break;
                            case 9.0:
                                if (GetSafeDouble(sitem["R4_GJZJ1"]) > 9.35 | GetSafeDouble(sitem["R4_GJZJ1"]) < 8.95 | GetSafeDouble(sitem["R4_GJZJ2"]) > 9.35 | GetSafeDouble(sitem["R4_GJZJ2"]) < 8.95)
                                {
                                    sitem["R4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["R4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "9.15±0.20";
                                break;
                            case 10.7:
                                if (GetSafeDouble(sitem["R4_GJZJ1"]) > 11.3 | GetSafeDouble(sitem["R4_GJZJ1"]) < 10.9 | GetSafeDouble(sitem["R4_GJZJ2"]) > 11.3 | GetSafeDouble(sitem["R4_GJZJ2"]) < 10.9)
                                {
                                    sitem["R4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["R4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "11.10±0.20";
                                break;
                            case 12.6:
                                if (GetSafeDouble(sitem["R4_GJZJ1"]) > 13.3 | GetSafeDouble(sitem["R4_GJZJ1"]) < 12.9 | GetSafeDouble(sitem["R4_GJZJ2"]) > 13.3 | GetSafeDouble(sitem["R4_GJZJ2"]) < 12.9)
                                {
                                    sitem["R4_GJZJ3"] = "不合格";
                                    isOk = false;
                                }
                                else
                                {
                                    sitem["R4_GJZJ3"] = "合格";
                                }
                                sitem["G_GJZJ"] = "13.10±0.20";
                                break;
                        }
                        if (GetSafeDouble(sitem["R4_GJJJPC1"]) != GetSafeDouble(sitem["G_GJJJPC"]))
                        {
                            sitem["R4_GJJJPC3"] = "不合格";
                            isOk = false;

                        }
                        else
                        {
                            sitem["R4_GJJJPC3"] = "合格";
                        }
                        if (!isOk)
                        {
                            mbhggs2 = mbhggs2 + 1;
                        }


                        #endregion
                    }

                    }
                #endregion
                
                #region 尺寸偏差
                if (jcxm.Contains("、尺寸偏差、"))
                {
                    jcxmCur = "尺寸偏差";
                    var ssign = true;
                    pcbhgs = 0;
                    if (sitem["SFFJ"] == "否")
                    {
                        #region 混凝土管桩
                        if (sitem["CPMC"].Contains("混凝土管桩"))
                        {
                            
                            for (md1 = 1; md1<11; md1++)
                            {
                                ssign = true;
                                md2 = GetSafeInt (sitem["S_WJ" + md1]);
                                md3 = GetSafeInt(sitem["S_BH" + md1]);
                                md4 = GetSafeInt(sitem["S_CD" + md1]);
                                md5 = GetSafeInt(sitem["S_PMD" + md1]);
                                md6 = GetSafeInt(sitem["S_DBHD" + md1]);
                                if (md2 > GetSafeInt(sitem["S_WJ"]) + 5 |md2< GetSafeInt(sitem["S_WJ"]) -2)
                                {
                                    ssign = false;
                                }
                                if (md3 > GetSafeInt(sitem["BCBH"]) + 10 | md3 < GetSafeInt(sitem["BCBH"]))
                                {
                                    ssign = false;
                                }
                                if (md4 > GetSafeInt(sitem["CD"])*1.005 | md3 < GetSafeInt(sitem["CD"])*0.995)
                                {
                                    ssign = false;
                                }
                                if( md5> 0.5){
                                    ssign = false;
                                }
                                if (md6 < GetSafeInt(sitem["SJDBHD"]))
                                {
                                    ssign = false;
                                }
                                if (!ssign)
                                {
                                    pcbhgs = pcbhgs + 1;
                                }

                            }
                            if (pcbhgs > 2)
                            {
                                sitem["GH_CCPC"] = "不合格";
                            }else if(pcbhgs<=2 && pcbhgs > 0)
                            {
                                sitem["GH_CCPC"] = "复检";
                            }
                            else { sitem["GH_CCPC"] = "合格"; }

                        }
                        #endregion
                        #region 非混凝土管桩
                        else
                        {
                            for (md1 = 1; md1 < 11; md1++)
                            {
                                ssign = true;
                                md2 = GetSafeInt(sitem["S_WJ" + md1]);
                                md3 = GetSafeInt(sitem["S_BH" + md1]);
                                md4 = GetSafeInt(sitem["S_CD" + md1]);
                                md5 = GetSafeInt(sitem["S_PMD" + md1]);
                                md6 = GetSafeInt(sitem["S_DBHD" + md1]);
                                if (GetSafeDouble(sitem["WJ"]) < 600)
                                {
                                    sitem["WJPC"] = "+4，-2";
                                    if (md2 > GetSafeInt("WJ") + 4 | md2 < GetSafeInt(sitem["WJ"]) - 2)
                                    {
                                        ssign = false;
                                    }
                                }
                                else
                                {
                                    sitem["WJPC"] = "+5，-2";
                                    if (md2 > GetSafeInt("WJ") + 5 | md2 < GetSafeInt(sitem["WJ"]) - 2)
                                    {
                                        ssign = false;
                                    }
                                }

                                if (md3 < GetSafeInt(sitem["BCBH"]))
                                {
                                    ssign = false;
                                }

                                if (md4 > GetSafeInt(sitem["CD"]) * 1.005 | md4 < GetSafeInt(sitem["CD"]) * 0.996)
                                {
                                    ssign = false;
                                }
                                if (md5 > 0.2)
                                {
                                    ssign = false;
                                }
                                if (md6 < GetSafeInt(sitem["SJDBHD"]))
                                {
                                    ssign = false;
                                }
                                if (!ssign)
                                {
                                    pcbhgs = pcbhgs + 1;
                                }
                            }   
                        }
                        #endregion
                        sitem["S_CCPC"] = "不合格数为" + pcbhgs;
                    }
                    #region 复检
                    else
                    {
                        #region 混凝土管桩
                        if (sitem["CPMC"].Contains("混凝土管桩"))
                        {

                            for (md1 = 1; md1 < 21; md1++)
                            {
                                ssign = true;
                                md2 = GetSafeInt(sitem["S_WJ" + md1]);
                                md3 = GetSafeInt(sitem["S_BH" + md1]);
                                md4 = GetSafeInt(sitem["S_CD" + md1]);
                                md5 = GetSafeInt(sitem["S_PMD" + md1]);
                                md6 = GetSafeInt(sitem["S_DBHD" + md1]);
                                if (md2 > GetSafeInt(sitem["S_WJ"]) + 5 | md2 < GetSafeInt(sitem["S_WJ"]) - 2)
                                {
                                    ssign = false;
                                }
                                if (md3 > GetSafeInt(sitem["BCBH"]) + 10 | md3 < GetSafeInt(sitem["BCBH"]))
                                {
                                    ssign = false;
                                }
                                if (md4 > GetSafeInt(sitem["CD"]) * 1.005 | md3 < GetSafeInt(sitem["CD"]) * 0.995)
                                {
                                    ssign = false;
                                }
                                if (md5 > 0.5)
                                {
                                    ssign = false;
                                }
                                if (md6 < GetSafeInt(sitem["SJDBHD"]))
                                {
                                    ssign = false;
                                }
                                if (!ssign)
                                {
                                    pcbhgs = pcbhgs + 1;
                                }

                            }
                           
                        }
                        #endregion

                        #region 非混凝土管桩
                        else
                        {
                            for (md1 = 1; md1 < 21; md1++)
                            {
                                ssign = true;
                                md2 = GetSafeInt(sitem["S_WJ" + md1]);
                                md3 = GetSafeInt(sitem["S_BH" + md1]);
                                md4 = GetSafeInt(sitem["S_CD" + md1]);
                                md5 = GetSafeInt(sitem["S_PMD" + md1]);
                                md6 = GetSafeInt(sitem["S_DBHD" + md1]);
                                if (GetSafeDouble(sitem["WJ"]) < 600)
                                {
                                    sitem["WJPC"] = "+4，-2";
                                    if (md2 > GetSafeInt("WJ") + 4 | md2 < GetSafeInt(sitem["WJ"]) - 2)
                                    {
                                        ssign = false;
                                    }
                                }
                                else
                                {
                                    sitem["WJPC"] = "+5，-2";
                                    if (md2 > GetSafeInt("WJ") + 5 | md2 < GetSafeInt(sitem["WJ"]) - 2)
                                    {
                                        ssign = false;
                                    }
                                }

                                if (md3 < GetSafeInt(sitem["BCBH"]))
                                {
                                    ssign = false;
                                }

                                if (md4 > GetSafeInt(sitem["CD"]) * 1.005 | md4 < GetSafeInt(sitem["CD"]) * 0.996)
                                {
                                    ssign = false;
                                }
                                if (md5 > 0.2)
                                {
                                    ssign = false;
                                }
                                if (md6 < GetSafeInt(sitem["SJDBHD"]))
                                {
                                    ssign = false;
                                }
                                if (!ssign)
                                {
                                    pcbhgs = pcbhgs + 1;
                                }
                            }
                            

                        }
                        #endregion

                        #region 偏差判断
                        if (pcbhgs > 0)
                        {
                            sitem["S_CCPC"] = "不合格";
                        }
                        else
                        {
                            sitem["S_CCPC"] = "合格";
                        }
                       
                        #endregion
                    }
                    #endregion
                    sitem["S_CCPC"] = "不合格数为" + pcbhgs;
                }
                #region 如果不是尺寸偏差
                else
                {
                    sitem["GH_CCPC"] = "----";
                }
                #endregion
                #endregion

                #region 混凝土强度
                if (jcxm.Contains("、混凝土强度、"))
                {
                    jcxmCur = "混凝土强度";
                    if (sitem["SFFJ"] == "否")
                    {
                        dArray = new double[3];

                    }
                    else { dArray = new double[12]; }
                    sum = 0;
                    for (xd = 1;  xd < dArray.Length+1; xd++)
                    {
                        dArray[xd] = GetSafeDouble(sitem["S_KYQD"] + xd);
                        sum = sum + dArray[xd];
                    }
                    pjmd = Round( sum / dArray.Length,1);
                    Array.Sort(dArray);
                    md = Round( dArray[1],1);
                    sitem["MINQD"] = md.ToString();
                    sitem["AVGQD"] = md.ToString();
                    xd = sitem["SIGD"].IndexOf("C") == -1 ? 0 : sitem["SIGD"].IndexOf("C");
                    if (xd + 2 < GetSafeDouble(sitem["SIGD"]))
                    {
                        stemp = (xd + 2).ToString();
                    }else if(xd + 1 > GetSafeDouble(sitem["SIGD"]))
                    {
                        stemp = (xd + 1).ToString();
                       
                    }
                    else
                    {
                        stemp = sitem["SIGD"];
                    }
                    stravg = "----";
                    strmin = "----";
                    if (String.IsNullOrEmpty(stemp))
                    {
                          md = Conversion.Val(stemp);
                       
                            md1 = Round( md,0);
                            md2 =Round( md1 * 0.85,0);
                        stravg = "≥"+md1.ToString();
                        strmin = "≥"+md2.ToString();

                        sitem["G_KYQD"] = "平均值" +stravg + "MPa" +"\n";
                       


                    }
                    flag = true;
                    flag = IsQualified(stravg, sitem["AVGQD"], true) == "符合" ? flag : false;
                    sitem["GH_KYQD"] = flag ? "合格" : "不合格";
                    sitem["GH_KYQD"] = "----";
                    if (!flag)
                    {
                       if( IsQualified(stravg,sitem["AVGQD"],true)== "符合" | IsQualified(stravg, sitem["MINQD"], true) == "符合")
                        {
                            sitem["GH_KYQD"] = "复试";
                        }
                    }



                }
                #endregion


            }
            #region 判定
            if (SItem[0]["SFFJ"] == "是")
            {
                #region 抗弯性能
                if (SItem[0]["JCXM"].Contains("、抗弯性能、"))
                {
                    if (mbhggs == 0)
                    {
                        MItem[0]["JCJGMS"] = "符合";
                    }
                    else
                    {
                        MItem[0]["JCJGMS"] = "不符合";
                        mAllHg = false;
                    }
                   
               
                }
                #endregion 抗弯性能
                #region 钢筋尺寸
                if (SItem[0]["JCXM"].Contains("、钢筋尺寸、"))
                {
                    if(mbhggs2 == 0)
                    {
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "") { MItem[0]["JCJGMS"]= "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。"; }

                        if(MItem[0]["JCJGMS"] == "不符合" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定" + "抗弯性能不合格；钢筋尺寸合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定" + "抗弯性能不合格；钢筋尺寸、壁厚合格。"; }
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定" + "钢筋尺寸合格；壁厚不合格。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定" + "抗弯性能、钢筋尺寸合格；壁厚不合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定" + "抗弯性能、壁厚不合格；钢筋尺寸不合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定" + "抗弯性能、钢筋尺寸不合格；、壁厚合格。"; }

                    }
                    else
                    {
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均不符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能合格；钢筋尺寸不合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均不符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目钢筋尺寸不合格;壁厚不合格。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能、壁厚合格；钢筋尺寸不合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能、钢筋尺寸不合格；壁厚合格。"; }
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均不符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定" + "抗弯性能不合格；钢筋尺寸、壁厚合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均不符合要求。"; }
                        mAllHg = false;
                    }

                }
                #endregion 钢筋尺寸
            }
            else
            {
                #region 抗弯性能
                if (SItem[0]["JCXM"].Contains("、抗弯性能、"))
                {
                    if (mbhggs == 0){MItem[0]["JCJGMS"] = "符合"; }else{mAllHg = false;}
                    if (mbhggs == 1) { MItem[0]["JCJGMS"] = "复检"; }
                    if (mbhggs == 2) { MItem[0]["JCJGMS"] = "不符合"; }


                }
                #endregion 抗弯性能
                #region 钢筋尺寸
                if (SItem[0]["JCXM"].Contains("、钢筋尺寸、"))
                {
                    if (mbhggs2 == 0)
                    {
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "复检" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格，需取加倍量进行复验；钢筋尺寸合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格；钢筋尺寸合格。"; }
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "复检" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格，需取加倍量进行复验；钢筋尺寸、壁厚合格"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格；钢筋尺寸、壁厚合格"; }
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目钢筋尺寸合格；壁厚不合格"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目钢筋尺寸、抗弯性能合格；壁厚不合格"; }
                        if (MItem[0]["JCJGMS"] == "复检" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能、壁厚不合格，需取加倍量进行复验；钢筋尺寸合格"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能、壁厚不合格；钢筋尺寸合格"; }

                    }
                    else
                    {
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目不符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能合格；钢筋尺寸不合格。"; }
                        if (MItem[0]["JCJGMS"] == "复检" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格，需取加倍量进行复验；钢筋尺寸不合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能、钢筋尺寸不合格。"; }
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目钢筋尺寸不合格；壁厚合格。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能、壁厚合格；钢筋尺寸不合格。"; }
                        if (MItem[0]["JCJGMS"] == "复检" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格，需取加倍量进行复验；钢筋尺寸不合格；壁厚合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目钢筋尺寸、抗弯性能不合格；壁厚合格。"; }
                        if (MItem[0]["JCJGMS"] == "" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目不符合要求。"; }
                        if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能合格；钢筋尺寸、壁厚不合格。"; }
                        if (MItem[0]["JCJGMS"] == "复检" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能、壁厚不合格，需取加倍量进行复验；钢筋尺寸不合格。"; }
                        if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能合格；钢筋尺寸不合格。"; }

                    }

                }
                #endregion 钢筋尺寸
            }
            if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能合格。"; }
            if (MItem[0]["JCJGMS"] == "复检" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格，需取加倍量进行复验。"; }
            if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格"; }
            if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能、壁厚合格"; }
            if (MItem[0]["JCJGMS"] == "复检" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格，需取加倍量进行复验；壁厚合格。"; }
            if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格；壁厚合格。"; }
            if (MItem[0]["JCJGMS"] == "符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能合格；壁厚不合格。"; }
            if (MItem[0]["JCJGMS"] == "复检" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格，需取加倍量进行复验；壁厚不合格。"; }
            if (MItem[0]["JCJGMS"] == "不符合" && gh_bh == "不合格") { MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗弯性能不合格；壁厚不合格。"; }
            if (SItem[0]["JCXM"].Contains("、混凝土强度、"))
            {
                if(MItem[0]["JCJGMS"].Length>2){
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"].Substring(0, MItem[0]["JCJGMS"].Length - 1);
                }
                if(SItem[0]["GH_KYQD"]== "合格")
                {
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "，该组样品所检混凝土强度符合标准要求。";
                }
                if (SItem[0]["GH_KYQD"] == "不合格")
                {
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "，该组样品所检混凝土强度不符合标准要求。";
                }
                if (SItem[0]["GH_KYQD"] == "复试")
                {
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "，该组样品所检混凝土强度不符合标准要求，需要再钻取9个芯样进行试验。";
                }
            }
            if(MItem[0]["JCJGMS"].Contains("不符合") | MItem[0]["JCJGMS"].Contains("复试")){
                mAllHg = false;
                SItem[0]["JCJG"] = "不合格";
                if(SItem[0]["JCPDLX"] == "以委托判定")
                {
                    MItem[0]["JCJGMS"] = "该组样品所检指标不符合委托要求";
                    if(SItem[0]["KWL_GH"] == "合格")
                    {

                    }
                }
            }
            else
            {
                SItem[0]["JCJG"] = "合格";
                if (SItem[0]["JCPDLX"] == "以委托判定")
                {
                    MItem[0]["JCJGMS"] = "该组样品所检指标符合委托要求";
                }
                mAllHg = true ;

            }


         

            #endregion 判定

            #endregion 计算结束

            /************************ 代码结束 *********************/
                #endregion
        }
    }
}