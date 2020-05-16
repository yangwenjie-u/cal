using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class DXL : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  计算开始
            var data = retData;
            var mrsDj = dataExtra["BZ_DXL_DJ"];
            var mrsWd = dataExtra["BZ_DXLWD"];
            var MItem = data["M_DXL"];
            var SItem = data["S_DXL"];
            var mrssjTable = data["Y_DXL"];
            double[] mkyqdArray = new double[3];
            int mbhggs;
            bool mAllHg;
            string mjgsm;
            bool mFlag_Hg, mFlag_Bhg;
            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            MItem[0]["JCJGMS"] = "";
            if (GetSafeDouble(MItem[0]["SYRQ1"]) <= GetSafeDouble(MItem[0]["JYRQ"]))
                MItem[0]["SYRQ1"] = MItem[0]["YPJSRQ_SY"];
            mbhggs = 0;
            string sd, sm = "";
            int Gs, xd;
            double md, md1, md2, sum, sqz;
            int row = 0;
            string[] strArray;
            mjgsm = "";
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";


            foreach (var sitem in SItem)
            {
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                //if (jcxm.Contains("、截面积试验、"))
                //{
                //    row = mrssjTable.Count();
                //    foreach (var Y_DXL in mrssjTable)
                //    {
                //        Y_DXL["E_CYFF"] = IsNumeric(Y_DXL["E_ZL"]) && IsNumeric(Y_DXL["E_CD"]) ? "称重法" : "计算法";
                //        if (Y_DXL["E_CYFF"] == "计算法")
                //        {
                //            if (Conversion.Val(Y_DXL["E_ZJ1"]) > 0)
                //                Y_DXL["E_ZJ"] = Round(0.2 * (Conversion.Val(Y_DXL["E_ZJ1"]) + Conversion.Val(Y_DXL["E_ZJ2"]) + Conversion.Val(Y_DXL["E_ZJ3"]) + Conversion.Val(Y_DXL["E_ZJ4"]) + Conversion.Val(Y_DXL["E_ZJ5"])), 2).ToString("F2");
                //            md1 = Conversion.Val(Y_DXL["  "]);
                //            if (IsNumeric(Y_DXL["E_GS"]))
                //                md2 = Conversion.Val(Y_DXL["E_GS"]);
                //            else
                //                md2 = 1;
                //            md = md2 * 3.1416 * (Math.Pow(md1, 2)) / 4;
                //        }
                //        else
                //        {
                //            md1 = Conversion.Val(Y_DXL["E_ZL"]);
                //            md2 = Conversion.Val(Y_DXL["E_CD"]);
                //            xd = (int)Conversion.Val(Y_DXL["E_MD"]);
                //            md = 1000 * md1 / (md2 * xd);
                //        }
                //        md = Round(md, 2);
                //        Y_DXL["E_JMJSC"] = md.ToString("F2");
                //    }
                //    mjgsm += "截面积试验结果如上,";
                //    mjgsm += "截面积结果见报告,";
                //}
                //else
                //{
                //    foreach (var Y_DXL in mrssjTable)
                //        Y_DXL["E_JMJSC"] = "----";
                //}

                if (jcxm.Contains("、直流电阻试验、") || jcxm.Contains("、导体试验、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "直流电阻试验,导体试验");
                    //计算修正系数
                    if (MItem[0]["H_SFXZ"] == "是")
                    {
                        md = Conversion.Val(MItem[0]["SYWD"]);
                        md = Round(md, 0);
                        xd = 1;
                        foreach (var mrsWd_Filter in mrsWd)
                        {
                            if (GetSafeDouble(mrsWd_Filter["WD"]) == md)
                            {
                                xd = (int)Conversion.Val(mrsWd_Filter["XZXS"]);
                                break;
                            }
                        }

                        MItem[0]["H_XZXS"] = xd.ToString("F3");
                    }
                    else
                    {
                        xd = 1;
                        MItem[0]["H_XZXS"] = "检测设备自动修正";
                    }
                    row = mrssjTable.Count();
                    var yIndex = 1;
                    foreach (var Y_DXL in mrssjTable)
                    {
                        if (Y_DXL == null || Y_DXL.Count == 0)
                        {
                            throw new Exception("请输入导体试验数据");

                        }

                        #region 实测20℃电阻值(Ω/km)
                        //md = Conversion.Val(MItem[0]["H_SYBC"]);  //试样标长
                        //md1 = Conversion.Val(Y_DXL["E_ZDZ"]);  //正电阻
                        //md2 = Conversion.Val(Y_DXL["E_FDZ"]);//负电阻
                        //sum = (md1 + md2) / 2;
                        //if (MItem[0]["H_DZDW"].Trim() == "mΩ")
                        //    sum = sum / 1000;
                        //if (MItem[0]["H_DZDW"].Trim() == "μΩ")
                        //    sum = sum / 1000000;
                        //sqz = 1000 * sum * xd / md;


                        sqz = GetSafeDouble(Y_DXL["E_SCDZ"]);//实测20℃电阻值(Ω/km)
                        Y_DXL["ZH"] = yIndex.ToString();
                        yIndex++;
                        #endregion

                        // 获取设计20℃电阻值(Ω/km)
                        var mrsDj_Filter = mrsDj.Where(x => x["ZL"].Contains(sitem["S_ZL"].Trim()) && x["LX"].Contains(sitem["S_LX"].Trim()));
                        sd = "";

                        foreach (var item in mrsDj_Filter)
                        {
                            if (Conversion.Val(item["BCJMJ"]) == Conversion.Val(sitem["S_BCJMJ"]))//标称截面积(mm<sup>2</sup>)
                            {
                                sd = item["ZDDZ"].Trim();
                                break;
                            }
                        }
                        if (sd == "")
                            sm = "--";
                        else
                            sm = sd.Substring(sd.IndexOf(".") + 1);

                        Y_DXL["E_SJDZ"] = sd == "" ? "----" : sd;  //设计20℃电阻值(Ω/km)
                        if (sm.Length == 1)
                        {
                            sqz = Round(sqz, 1);
                            Y_DXL["E_SCDZ"] = sqz.ToString("F1");//实测20℃电阻值(Ω/km)
                        }
                        else if (sm.Length == 2)
                        {
                            sqz = Round(sqz, 2);
                            Y_DXL["E_SCDZ"] = sqz.ToString("F2");
                        }
                        else if (sm.Length == 3)
                        {
                            sqz = Round(sqz, 3);
                            Y_DXL["E_SCDZ"] = sqz.ToString("F3");
                        }
                        else
                        {
                            sqz = Round(sqz, 4);
                            Y_DXL["E_SCDZ"] = sqz.ToString("F4");
                        }

                        md = Conversion.Val(sd);
                        if (sm == "--")
                            Y_DXL["E_DZPD"] = "----";
                        else
                        {
                            Y_DXL["E_DZPD"] = sqz <= md ? "合格" : "不合格";
                            mbhggs = sqz <= md ? mbhggs : mbhggs + 1;
                            if (Y_DXL["E_DZPD"] == "合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                            }
                        }
                    }
                    if (sm == "--")
                    {
                        MItem[0]["DTDZ_HG"] = "----";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        MItem[0]["DTDZ_HG"] = "合格";

                        if (mbhggs > 0)
                        {
                            MItem[0]["DTDZ_HG"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                else
                {
                    foreach (var Y_DXL in mrssjTable)
                    {
                        Y_DXL["E_DZPD"] = "----";
                        Y_DXL["E_SJDZ"] = "----";
                        Y_DXL["E_SCDZ"] = "----";
                    }
                }

                if (jcxm.Contains("、标志、"))
                {
                    jcxmCur = "标志";
                    MItem[0]["HG_BZLXX"] = IsQualified("≤550", sitem["BZLXX"]);

                    if (MItem[0]["HG_QXD"] == "合格" && MItem[0]["HG_BZLXX"] == "合格" && MItem[0]["HG_NCX"] == "合格")
                    {
                        MItem[0]["BZ_HG"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    MItem[0]["BZ_HG"] = "----";
                    MItem[0]["HG_QXD"] = "----";
                    MItem[0]["HG_BZLXX"] = "----";
                    MItem[0]["HG_NCX"] = "----";
                }
                if (jcxm.Contains("、阻燃试验、"))
                {
                    jcxmCur = "阻燃试验";

                    //燃烧向下延伸至距离上支架的下缘大于540mm，判定不合格

                    MItem[0]["ZR_HG"] = IsQualified("≤540", sitem["XYJSZJ"]);

                    if (MItem[0]["ZR_HG"] == "合格")
                    {
                        //上支架下缘距离碳化起点
                        MItem[0]["ZR_HG"] = IsQualified("＞50", sitem["ZJJSTHQD"]);
                    }
                    if (MItem[0]["ZR_HG"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    MItem[0]["ZR_HG"] = "----";
                }
                MItem[0]["CF_SUBROWS"] = row.ToString();
                #region 新增部分用于原始记录

                //strArray = new string[20];
                //strArray[1] = "E_BEIZHU";
                //strArray[2] = "E_CD";
                //strArray[3] = "E_DZPD";
                //strArray[4] = "E_FDZ";
                //strArray[5] = "E_GS";
                //strArray[6] = "E_JMJSC";
                //strArray[7] = "E_JMJSJ";
                //strArray[8] = "E_MD";
                //strArray[9] = "E_SCDZ";
                //strArray[10] = "E_SJDZ";
                //strArray[11] = "E_ZDZ";
                //strArray[12] = "E_ZJ";
                //strArray[13] = "E_ZL";
                //strArray[14] = "E_ZJ1";
                //strArray[15] = "E_ZJ2";
                //strArray[16] = "E_ZJ3";
                //strArray[17] = "E_ZJ4";
                //strArray[18] = "E_ZJ5";
                //strArray[19] = "E_YS";
                //for (xd = 1; xd <= strArray.Length - 1; xd++)
                //{
                //    sitem[strArray[xd]] = "";
                //    for (Gs = 1; Gs <= row; Gs++)
                //    {
                //        var Y_DXL = mrssjTable[Gs - 1];
                //        if (strArray[xd] == "E_BEIZHU")
                //        {
                //            //备注
                //            Y_DXL["beizhu"] = string.IsNullOrEmpty(Y_DXL["BEIZHU"]) ? "" : Y_DXL["BEIZHU"].Trim();
                //            sitem[strArray[xd]] = sitem[strArray[xd]] + Y_DXL["BEIZHU"] + "|";
                //        }
                //        else
                //        {
                //            Y_DXL[strArray[xd]] = string.IsNullOrEmpty(Y_DXL[strArray[xd]]) ? "" : Y_DXL[strArray[xd]].Trim();
                //            sitem[strArray[xd]] = sitem[strArray[xd]] + Y_DXL[strArray[xd]] + "|";
                //        }
                //    }
                //    sitem[strArray[xd]] = sitem[strArray[xd]].Length > 1 ? sitem[strArray[xd]].Substring(0, sitem[strArray[xd]].Length - 1) : "";
                //} 
                #endregion
                if (mbhggs > 0)
                {
                    sitem["JCJG"] = "不合格";
                }
                else
                {
                    sitem["JCJG"] = "合格";
                }

                mAllHg = (mAllHg && sitem["JCJG"] == "合格");
            }
            //主表总判断赋值
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                mjgsm = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
                mjgsm = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            MItem[0]["JCJGMS"] = mjgsm;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
