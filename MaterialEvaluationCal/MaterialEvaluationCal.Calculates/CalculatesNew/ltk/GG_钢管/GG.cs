﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class GG : BaseMethods
    {
        public void Calc()
        {
            #region  Code
            /************************ 代码开始 *********************/

            #region 参数定义
            
            double[] mkyqdArray = new double[3];
            double  mScl;
            int vp;
            string mJSFF;
            bool mAllHg;
            mAllHg = true;
            string which = "";
            #endregion

            #region  自定义函数
            Func<double, double> myint =
                 delegate (double dataChar)
                 {
                     return Round(dataChar / 5, 0) * 5;
                 };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_GG_DJ"];
            var MItem = data["M_GG"];
            var SItem = data["S_GG"];
            MItem[0]["JCJGMS"] = "";
          
            #endregion



            #region  计算开始
            foreach (var sitem in SItem)
            {
                #region 等级表处理
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(sitem["GGXH"]) && x["ZJM"].Contains(sitem["GGPH"]));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    sitem["GGGWJ"] = mrsDj_Filter["GGWJ"];
                    sitem["GWJPC"] = mrsDj_Filter["WJPC"];
                    sitem["GGGBH"] = mrsDj_Filter["GGBH"];
                    sitem["SJGGXH"] = mrsDj_Filter["GGXH"];
                    sitem["GBHPC"] = mrsDj_Filter["BHPC"];
                    sitem["GXSSD"] = mrsDj_Filter["XSSD"];
                    sitem["G_QFQD"] = mrsDj_Filter["G_QFQD"];
                    sitem["G_KLQD"] = mrsDj_Filter["G_KLQD"];
                    sitem["G_SCL"] = mrsDj_Filter["G_SCL"];
                    sitem["G_LW"] = mrsDj_Filter["G_LW"];
                    which = mrsDj_Filter["WHICH"];
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "试件尺寸为空";
                    break;
                }

                #region 尺寸标准
                if(sitem["YPMC"]== "低压流体输送用焊接钢管") {
                    if (GetSafeDouble(sitem["GCWJ"]) <= 48.3)
                    {
                        sitem["GWJPC"] = "0.50";
                    }
                    if (GetSafeDouble(sitem["GCWJ"]) > 48.3 && GetSafeDouble(sitem["GCWJ"]) <= 273.1)
                    {
                        sitem["GWJPC"] = Round(0.001 * GetSafeDouble(sitem["GCWJ"]), 2).ToString("0.00");
                    }
                    if (GetSafeDouble(sitem["GCWJ"]) > 273.1 && GetSafeDouble(sitem["GCWJ"]) <= 508)
                    {
                        sitem["GWJPC"] = Round(0.0075 * GetSafeDouble(sitem["GCWJ"]), 2).ToString("0.00");
                    }
                    if (GetSafeDouble(sitem["GCWJ"]) > 508)
                    {
                        if (0.001 * GetSafeDouble(sitem["GCWJ"]) < 10.0)
                        {
                            sitem["GWJPC"] = Round(0.001 * GetSafeDouble(sitem["GCWJ"]), 1).ToString("0.00");
                        }
                        else { sitem["GWJPC"] = "10.0"; }
                    }
                    sitem["GBHPC"] = "-1.0" + "～" + Round(GetSafeDouble(sitem["GCBH"]) * 0.1, 1);
                }

                sitem["GBHPC"] = "-"+ GetSafeDouble(sitem["GCBH"]) + "～" + GetSafeDouble(sitem["GCBH"]);
                #endregion

                #endregion

                if (string.IsNullOrEmpty(mJSFF))
                {
                    sitem["G_LW"] = "弯心d为6D弯曲角度90°受弯部位表面不得产生裂纹";
                    if (sitem["GGXH"].Contains("直缝电焊钢管"))
                        sitem["G_YB"] = "当试样压值两平板距离为2/3D时，试样不允许出现裂纹或裂缝";
                    else
                        sitem["G_YB"] = "压扁试验后不得出现裂纹、分层";
                    #region 外径
                    if (sitem["JCXM"].Contains("外径")|| sitem["JCXM"].Contains("尺寸允许偏差"))
                    {
                        double mwj1 = (Conversion.Val(sitem["GGWJ1_1"]) + Conversion.Val(sitem["GGWJ1_2"])) / 2;
                        double mwj2 = (Conversion.Val(sitem["GGWJ2_1"]) + Conversion.Val(sitem["GGWJ2_2"])) / 2;
                        sitem["GGWJ1"] = Round(mwj1, 2).ToString();
                        sitem["GGWJ2"] = Round(mwj2, 2).ToString();
                        sitem["GGWJPC1"] = Round((Conversion.Val(sitem["GGWJ1"]) - Conversion.Val(mrsDj_Filter["GGWJ"])),1).ToString();
                        sitem["GGWJPC2"] =Round( (Conversion.Val(sitem["GGWJ2"]) - Conversion.Val(mrsDj_Filter["GGWJ"])),1).ToString();
                        if ((int)GetSafeDouble(mrsDj_Filter["LSGS"]) == 2)
                        {
                            if (Math.Abs( Conversion.Val(sitem["GGWJPC1"]))<= Conversion.Val(sitem["GWJPC"])&& Math.Abs( Conversion.Val(sitem["GGWJPC2"]) )<= Conversion.Val(sitem["GWJPC"]))
                                sitem["WJPD"] = "合格";
                            else
                                sitem["WJPD"] = "不合格";
                        }
                        else
                        {
                            if (Math.Abs( Conversion.Val(sitem["GGWJPC1"]))<= Conversion.Val(sitem["GWJPC"]))
                                sitem["WJPD"] = "合格";
                            else
                                sitem["WJPD"] = "不合格";

                        }
                    }
                    else
                        sitem["WJPD"] = "----";
                    #endregion

                    #region 壁厚
                    if (sitem["JCXM"].Contains("壁厚") || sitem["JCXM"].Contains("尺寸允许偏差"))
                    {
                        double mbh1 = (Conversion.Val(sitem["GGBH1_1"]) + Conversion.Val(sitem["GGBH1_2"]) + Conversion.Val(sitem["GGBH1_3"]) + Conversion.Val(sitem["GGBH1_4"])) / 4;
                        double mbh2 = (Conversion.Val(sitem["GGBH2_1"]) + Conversion.Val(sitem["GGBH2_2"]) + Conversion.Val(sitem["GGBH2_3"]) + Conversion.Val(sitem["GGBH2_4"])) / 4;
                        sitem["GGBH1"] = Round(mbh1, 2).ToString();
                        sitem["GGBH2"] = Round(mbh2, 2).ToString();
                        sitem["GGBHPC1"] =Round( (Conversion.Val(sitem["GGBH1"]) - Conversion.Val(mrsDj_Filter["GGBH"])),1).ToString();
                        sitem["GGBHPC2"] =Round( (Conversion.Val(sitem["GGBH2"]) - Conversion.Val(mrsDj_Filter["GGBH"])),1).ToString();


                        if ((int)GetSafeDouble(mrsDj_Filter["LSGS"]) == 1)
                        { 
                            sitem["BHPD"] = IsQualified(sitem["GBHPC"] ,sitem["GGBHPC1"],false);
                        }
                        else
                        {
                           
                            if(IsQualified(sitem["GBHPC"], sitem["GGBHPC1"], false) == "合格")
                            {
                                sitem["BHPD"] = IsQualified(sitem["GBHPC"], sitem["GGBHPC2"], false);
                            }
                            else { sitem["BHPD"] = "不合格"; }
                        }
                    }
                    else
                        sitem["BHPD"] = "----";
                    #endregion

                    #region 锈蚀深度
                    if (sitem["JCXM"].Contains("锈蚀深度"))
                    {
                        if (sitem["YPXJ"] == "新")
                        {
                            sitem["XSSD"] = "----";
                            sitem["XSPD"] = "----";
                        }
                        else
                        {
                            string mlongStr = sitem["XSSD1"] + ", " + sitem["XSSD2"] + ", " + sitem["XSSD3"];
                            string[] mtmpArray = mlongStr.Split(',');
                           for(vp = 0; vp < 3; vp++)
                                mkyqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                            Array.Sort(mkyqdArray);
                            sitem["XSSD"] = mkyqdArray[2].ToString();
                            if (Conversion.Val(sitem["XSSD"]) > Conversion.Val(mrsDj_Filter["XSSD"]))
                                sitem["XSPD"] = "不合格";
                            else
                                sitem["XSPD"] = "合格";
                        }
                    }
                    else
                        sitem["XSPD"] = "----";
                    if (sitem["WJPD"] == "不合格" || sitem["BHPD"] == "不合格" || sitem["XSPD"] == "不合格")
                        sitem["WGPD"] = "不合格";
                    else
                    {
                        if (sitem["WJPD"] == "----" && sitem["BHPD"] == "----" && sitem["XSPD"] == "----")
                            sitem["WGPD"] = "----";
                        else
                            sitem["WGPD"] = "合格";
                    }
                    #endregion

                    #region 拉伸
                    if (sitem["JCXM"].Contains("拉伸")|| sitem["JCXM"].Contains("抗拉强度")||sitem["JCXM"].Contains("断后伸长率") && sitem["WGPD"] != "不合格")
                    { 
                        var WJ1 = GetSafeDouble(sitem["GGWJ1"]);
                        var BH1= GetSafeDouble(sitem["GGBH1"]);
                        var WJ2 = GetSafeDouble(sitem["GGWJ2"]);
                        var BH2 = GetSafeDouble(sitem["GGBH2"]);
                        var mMj1 =Round( 3.14159 * (Math.Pow((WJ1 / 2), 2) - Math.Pow((WJ1 - BH1) / 2, 2)),4);
                        var mMj2 =Round( 3.14159 * (Math.Pow((WJ2 / 2), 2) - Math.Pow((WJ2 - BH2) / 2, 2)),4);
                        sitem["JMJ1"] = mMj1.ToString();
                        sitem["JMJ2"] = mMj2.ToString();
                  
                        sitem["YSBJ"] = (Round(5.65 * Math.Sqrt(mMj1) / 5, 0) * 5).ToString();
                        sitem["YSBJ2"] = (Round(5.65 * Math.Sqrt(mMj2) / 5, 0) * 5).ToString();
                        if (Conversion.Val(sitem["KLHZ1"]) == 0)
                            break;
                        if (Conversion.Val(sitem["JMJ1"]) != 0)
                        {
                            sitem["QFQD1"] = myint(1000 * (Conversion.Val(sitem["QFHZ1"]) / Conversion.Val(sitem["JMJ1"]))).ToString();
                            sitem["KLQD1"] = myint(1000 * (Conversion.Val(sitem["KLHZ1"]) / Conversion.Val(sitem["JMJ1"]))).ToString();
                            mScl = (Conversion.Val(sitem["SCZ1"]) - Conversion.Val(sitem["YSBJ"])) * 100 / Conversion.Val(sitem["YSBJ"]);
                            if (mScl > 10)
                                sitem["SCL1"] = Round(mScl, 0).ToString();
                            else
                                sitem["SCL1"] = Round(mScl, 1).ToString();
                        }
                        if (GetSafeDouble(sitem["JMJ2"]) != 0)
                        {
                            sitem["QFQD2"] = myint(1000 * (Conversion.Val(sitem["QFHZ2"]) / Conversion.Val(sitem["JMJ2"]))).ToString();
                            sitem["KLQD2"] = myint(1000 * (Conversion.Val(sitem["KLHZ2"]) / Conversion.Val(sitem["JMJ2"]))).ToString();
                            mScl = Round((Conversion.Val(sitem["SCZ2"]) - Conversion.Val(sitem["YSBJ2"])) * 100 / Conversion.Val(sitem["YSBJ2"]), 0);
                            if (mScl > 10)
                                sitem["SCL2"] = Round(mScl, 0).ToString();
                            else
                                sitem["SCL2"] = Round(mScl, 1).ToString();
                        }
                        int mqfgs = 0;
                        if (Conversion.Val(sitem["QFQD1"]) >= Conversion.Val(sitem["G_QFQD"]) && Conversion.Val(sitem["QFQD1"]) != 0 && Conversion.Val(sitem["G_QFQD"]) != 0)
                            mqfgs = mqfgs + 1;
                        if (Conversion.Val(sitem["QFQD2"]) >= Conversion.Val(sitem["G_QFQD"]) && Conversion.Val(sitem["QFQD2"]) != 0 && Conversion.Val(sitem["G_QFQD"]) != 0)
                            mqfgs = mqfgs + 1;
                        int mklgs = 0;
                        if (Conversion.Val(sitem["KLQD1"]) >= Conversion.Val(sitem["G_KLQD"]) && Conversion.Val(sitem["KLQD1"]) != 0 && Conversion.Val(sitem["G_KLQD"]) != 0)
                            mklgs = mklgs + 1;
                        if (Conversion.Val(sitem["KLQD2"]) >= Conversion.Val(sitem["G_KLQD"]) && Conversion.Val(sitem["KLQD2"]) != 0 && Conversion.Val(sitem["G_KLQD"]) != 0)
                            mklgs = mklgs + 1;
                        int msclgs = 0;
                        if (Conversion.Val(sitem["SCL1"]) >= Conversion.Val(sitem["G_SCL"]) && Conversion.Val(sitem["SCL1"]) > 0 && Conversion.Val(sitem["G_SCL"]) != 0)
                            msclgs = msclgs + 1;
                        if (Conversion.Val(sitem["SCL2"]) >= Conversion.Val(sitem["G_SCL"]) && Conversion.Val(sitem["SCL2"]) > 0 && Conversion.Val(sitem["G_SCL"]) != 0)
                            msclgs = msclgs + 1;


                        if (mqfgs >=(int) GetSafeDouble(mrsDj_Filter["QFHGGS"]))
                            sitem["HG_QFQD"] = "合格";
                        else
                            sitem["HG_QFQD"] = "不合格";
                        if (Conversion.Val(mrsDj_Filter["G_QFQD"]) == 0 || Conversion.Val(sitem["QFQD1"]) == 0)
                            sitem["HG_QFQD"] = "----";
                        if (mklgs >= (int)GetSafeDouble(mrsDj_Filter["KLHGGS"]))
                            sitem["HG_KLQD"] = "合格";
                        else
                            sitem["HG_KLQD"] = "不合格";
                        if (Conversion.Val(mrsDj_Filter["G_KLQD"]) == 0 || Conversion.Val(sitem["KLQD1"]) == 0)
                            sitem["HG_KLQD"] = "----";
                        if (msclgs >=(int) GetSafeDouble(mrsDj_Filter["SCHGGS"]))
                            sitem["HG_SCL"] = "合格";
                        else
                            sitem["HG_SCL"] = "不合格";
                        if (Conversion.Val(mrsDj_Filter["G_SCL"]) == 0 || Conversion.Val(sitem["SCL1"]) == 0)
                            sitem["HG_SCL"] = "----";
                    }
                    else
                    {
                        sitem["HG_QFQD"] = "----";
                        sitem["HG_KLQD"] = "----";
                        sitem["HG_SCL"] = "----";
                    }
                    #endregion
                    if ((int)GetSafeDouble(mrsDj_Filter["LSGS"]) == 1)
                    {

                    }
                        #region 弯曲
                        if (sitem["JCXM"].Contains("弯曲") || sitem["JCXM"].Contains("管材弯曲试验"))
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (sitem["LW" + i] == "无裂纹")
                            {
                                sitem["LW" + i] = "1";
                            }
                            if (sitem["LW" + i] == "有裂纹")
                            {
                                sitem["LW" + i] = "0";
                            }
                            if (sitem["LW" + i] == "----")
                            {
                                sitem["LW" + i] = "-1";
                            }
                          
                        }
                        int mlwgs = 0;
                        if (Conversion.Val(sitem["LW1"]) == 1 || Conversion.Val(sitem["LW1"]) < 0)
                            mlwgs = mlwgs + 1;
                        if (Conversion.Val(sitem["LW2"]) == 1 || Conversion.Val(sitem["LW2"]) < 0)
                            mlwgs = mlwgs + 1;


                        if (mlwgs >= (int)GetSafeDouble(mrsDj_Filter["LWHGGS"]))
                            sitem["HG_LW"] = "合格";
                        else
                            sitem["HG_LW"] = "不合格";
                        if (Conversion.Val(mrsDj_Filter["G_LW"]) == 0)
                        {
                            sitem["LW1"] = "-1";
                            sitem["lW2"] = "-1";
                            sitem["HG_LW"] = "----";
                        }
                    }
                    else
                    {
                        sitem["LW1"] = "-1";
                        sitem["LW2"] = "-1";
                        sitem["HG_LW"] = "----";
                    }
                    #endregion
                    for (int i = 1; i < 3; i++)
                    {
                        if (sitem["LW" + i] == "1")
                        {
                            sitem["LW" + i] = "无裂纹";
                        }
                        if (sitem["LW" + i] == "0")
                        {
                            sitem["LW" + i] = "有裂纹";
                        }
                        if (sitem["LW" + i] == "-1")
                        {
                            sitem["LW" + i] = "----";
                        }

                    }
                    #region 压扁
                    if (sitem["JCXM"].Contains("压扁"))
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            if (sitem["YB" + i] == "无裂纹")
                            {
                                sitem["YB" + i] = "1";
                            }
                            if (sitem["YB" + i] == "有裂纹")
                            {
                                sitem["YB" + i] = "0";
                            }
                            if (sitem["YB" + i] == "----")
                            {
                                sitem["YB" + i] = "-1";
                            }

                        }
                        int mybgs = 0;
                        if (Conversion.Val(sitem["YB1"]) == 1 || Conversion.Val(sitem["YB1"]) < 0)
                            mybgs = mybgs + 1;
                        if (Conversion.Val(sitem["YB2"]) == 1 || Conversion.Val(sitem["YB2"]) < 0)
                            mybgs = mybgs + 1;
                        if (mybgs >= (int)GetSafeDouble(mrsDj_Filter["YBHGGS"]))
                            sitem["HG_YB"] = "合格";
                        else
                            sitem["HG_YB"] = "不合格";
                        if (Conversion.Val(mrsDj_Filter["G_YB"]) == 0)
                        {
                            sitem["YB1"] = "-1";
                            sitem["YB2"] = "-1";
                            sitem["HG_YB"] = "----";
                        }
                    }
                    else
                    {
                        sitem["YB1"] = "-1";
                        sitem["YB2"] = "-1";
                        sitem["HG_YB"] = "----";
                    }
                    #endregion
                    for (int i = 1; i < 3; i++)
                    {
                        if (sitem["YB" + i] == "1")
                        {
                            sitem["YB" + i] = "无裂纹";
                        }
                        if (sitem["YB" + i] == "0")
                        {
                            sitem["YB" + i] = "有裂纹";
                        }
                        if (sitem["YB" + i] == "-1")
                        {
                            sitem["YB" + i] = "----";
                        }

                    }
                    //单组判定
                    MItem[0]["JCJGMS"] = "";
                    if (sitem["WGPD"] == "不合格")
                    {
                        MItem[0]["JCJGMS"] = "外观需复检";
                        sitem["JCJG"] = "复试";
                        mAllHg = false;
                    }
                    if (sitem["HG_KLQD"] == "不合格" || sitem["HG_QFQD"] == "不合格" || sitem["HG_SCL"] == "不合格")
                        sitem["LXPD"] = "不合格";
                    else
                        sitem["LXPD"] = "合格";
                    if (sitem["HG_KLQD"] == "不合格" || sitem["HG_QFQD"] == "不合格" || sitem["HG_SCL"] == "不合格" || sitem["HG_LW"] == "不合格" || sitem["HG_YB"] == "不合格")
                    {
                        mAllHg = false;
                        sitem["JCJG"] = "不合格";
                        MItem[0]["JCJGMS"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                        //MItem[0]["JCJGMS"] = "该组试样不符合标准要求。";
                        if (sitem["HG_LW"] == "合格" || sitem["HG_YB"] == "合格")
                        {
                            MItem[0]["JCJGMS"] = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                            //MItem[0]["JCJGMS"] = "该组试样所检项目部分符合标准要求。";
                        }
                    }
                    else
                    {
                        if ((sitem["HG_QFQD"] == "合格" || sitem["HG_QFQD"] == "----") && (sitem["HG_KLQD"] == "合格" || sitem["HG_KLQD"] == "----") && (sitem["HG_SCL"] == "合格" || sitem["HG_SCL"] == "----") && (sitem["HG_LW"] == "合格" || sitem["HG_LW"] == "----") && (sitem["HG_YB"] == "合格" || sitem["HG_YB"] == "----"))
                        {
                            mAllHg = true;
                            sitem["JCJG"] = "合格";
                            sitem["LXPD"] = "合格";
                            MItem[0]["JCJGMS"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                            //MItem[0]["JCJGMS"] = "该组试样所检项目符合标准要求。";
                        }
                        else
                        {
                            MItem[0]["JCJGMS"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            //MItem[0]["JCJGMS"] = "该组试样不符合标准要求。";
                            mAllHg = false;
                            sitem["JCJG"] = "不合格";
                            sitem["LXPD"] = "不合格";
                            if (sitem["HG_KLQD"] == "合格" || sitem["HG_QFQD"] == "合格" || sitem["HG_SCL"] == "合格" || sitem["HG_LW"] == "合格" || sitem["HG_YB"] == "合格")
                            {
                                MItem[0]["JCJGMS"] = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                                //MItem[0]["JCJGMS"] = "该组试样所检项目部分符合标准要求。";
                            }
                        }
                    }
                    if (sitem["JCJG"] == "复试")
                        MItem[0]["JCJGMS"] = "该组试样" + MItem[0]["JCJGMS"].Trim() + "。";
                }
            }

            //主表总判断赋值
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            #endregion

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
