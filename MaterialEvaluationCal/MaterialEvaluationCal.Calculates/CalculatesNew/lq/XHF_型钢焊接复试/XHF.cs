using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class XHF : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_XHF_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_XHF"];
            var jcxmBhg = "";
            var jcxmCur = "";

            var mFlag_Hg = false;
            var mFlag_Bhg = false;

            if (!data.ContainsKey("M_XHF"))
            {
                data["M_XHF"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_XHF"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var mAllHg = true;
            var jcxm = "";

            string mGjlb, mSjdj = "";
            double mKlqd, mQfqd, mklqd1 = 0;

            Func<double, double> myint = delegate (double dataChar)
            {
                return Math.Round(Conversion.Val(dataChar) / 5, 0) * 5;
            };

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mSjdj = string.IsNullOrEmpty(sItem["SJDJ"]) ? "" : sItem["SJDJ"];

                mGjlb = sItem["GJLB"];
                var LwBzyq = "";
                var hd_fw = "";
                var md = 0.0;
                double mlwzj = 0;
                string mxwgs = "";

                if (string.IsNullOrEmpty(mGjlb))
                {
                    mGjlb = "";
                }

                #region 从设计等级表中取得相应的计算数值、等级标准
                if (mSjdj == "碳素结构钢")
                {
                    if (md <= 16)
                        hd_fw = "≤16";
                    else if (md <= 40)
                        hd_fw = "＞16≤40";
                    else if (md <= 60)
                        hd_fw = "＞40≤60";
                    else if (md <= 80)
                        hd_fw = "＞60≤80";
                    else if (md <= 100)
                        hd_fw = "＞80≤100";
                    else if (md <= 150)
                        hd_fw = "＞100≤150";
                    else if (md <= 200)
                        hd_fw = "＞150≤200";
                    else if (md <= 250)
                        hd_fw = "＞200≤250";
                    else
                        hd_fw = "";
                }
                else
                {
                    if (md <= 16)
                        hd_fw = "≤16";
                    else if (md <= 40)
                        hd_fw = "＞16≤40";
                    else if (md <= 63)
                        hd_fw = "＞40≤63";
                    else if (md <= 80)
                        hd_fw = "＞60≤80";
                    else if (md <= 100)
                        hd_fw = "＞80≤100";
                    else if (md <= 150)
                        hd_fw = "＞100≤150";
                    else if (md <= 200)
                        hd_fw = "＞150≤200";
                    else if (md <= 250)
                        hd_fw = "＞200≤250";
                    else
                        hd_fw = "";
                }

                //var mrsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["GJLB"] == mGjlb.Trim());
                var mrsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["MC"] == mSjdj.Trim() && u["ZJM"] == hd_fw.Trim());
                if (null == mrsDj)
                {
                    jsbeizhu = "依据不详\r\n";
                    sItem["JCJG"] = "不合格";
                    mjcjg = "不下结论";
                    continue;
                }
                else
                {
                    mQfqd = Double.Parse(mrsDj["QFQDBZZ"]); //'单组标准值
                    mKlqd = Double.Parse(mrsDj["KLQDBZZ"]);
                    mklqd1 = Double.Parse(mrsDj["KLQDBZZ1"]);
                }
                #endregion
                //对接接头弯曲试验
                if (mGjlb == "对接接头")
                {
                    LwBzyq = "试验弯曲至180度后，各试验任何方向裂纹及其他欠缺单个长度不应大于3mm；各试样任何方向不大于3mm的裂纹及其他欠缺的总长不应大于7mm；四个试样各种欠缺总长不应大于24mm。";
                }
                //栓钉焊接头弯曲试验
                if (mGjlb == "栓钉焊接头")
                {
                    LwBzyq = "试验弯曲至30度后焊接部位无裂纹。";
                }
                sItem["G_KLQD"] ="≥"+ mKlqd.ToString();
                //sItem["G_LWWZ"] = LwBzyq.ToString(); //

                //求抗拉强度
                for(int i = 1; i < 5; i++)
                {
                    sItem["GG"+i]= sItem["HD"+i] + "×" + sItem["KD"+i];
                }
               
                if (0 == Conversion.Val(sItem["ZJ"]))
                {
                    sItem["MJ"] = (Conversion.Val(sItem["HD1"]) * Conversion.Val(sItem["KD1"])).ToString("0.000");
                    //sItem["GG"] = "宽:" + sItem["KD1"] + "\n厚:" + sItem["HD1"];
                }
                else
                {
                    md = Conversion.Val(sItem["ZJ"].Trim()) / 2;
                    md = Math.Round(3.14159 * Math.Pow(md, 2), 3);
                    sItem["MJ"] = md.ToString("0.000");
                    // = "Φ:" + sItem["ZJ"].Trim();
                }
                decimal kl1, kl2 = 0;
                string kj1, kj2 = "";
                {
                    kl1 = GetSafeDecimal(sItem["KLQD1"]);
                    kl2 = GetSafeDecimal(sItem["KLQD2"]);
                  


                    #region 拉伸
                    if (jcxm.Contains("、拉伸、"))
                    {
                        jcxmCur = "拉伸";
                        var mj = 0.0;
                        for (int i = 1; i < 5; i++)
                        {
                            if (Conversion.Val(sItem["HD1"]) * Conversion.Val(sItem["KD1"]) == 0)
                            {
                                sItem["KLQD" + i] = "0";
                                continue;
                            }
                            mj = Conversion.Val(sItem["HD" + i]) * Conversion.Val(sItem["KD" + i]);

                            if (string.IsNullOrEmpty(sItem["KLHZ" + i]))
                            {
                                sItem["KLHZ" + i] = "0";
                            }
                            sItem["KLQD" + i] = myint(1000 * Conversion.Val(sItem["KLHZ" + i]) / mj).ToString();
                            if(Conversion.Val(sItem["KLQD" + i]) == 0)
                            {
                                sItem["KLQD" + i] = "----";
                            }
                        }

                        var mallBhg_kl = 0;
                        for (int i = 1; i < 5; i++)
                        {
                            if (GetSafeDouble(sItem["KLQD" + i]) >= mKlqd)
                                sItem["HG_KL"] = (Conversion.Val(sItem["HG_KL"]) + 1).ToString();
                            else
                                mallBhg_kl += 1;
                        }

                        if (mallBhg_kl == 0)
                        {
                            sItem["JCJG_LS"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LS"] = "不符合";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        sItem["JCJG_LS"] = "----";
                        sItem["DKJ1"] = "----";
                        sItem["DKJ2"] = "----";
                    }
                    if (sItem["WQJD"] == "")
                    {
                        sItem["WQJD"] = "----";
                    }
                    if (!jcxm.Contains("、拉伸、"))
                    {
                        for (int i = 1; i < 5; i++)
                        {


                            sItem["KLQD" + i] = "----";
                            sItem["HD" + i] = "--";
                            sItem["KD" + i] = "--";
                            sItem["G_KLQD"] = "----";
                           
                                sItem["GG" + i] = "----";



                        }
                    }
                    #endregion

                    #region 弯曲
                    if (jcxm.Contains("、弯曲、"))
                    {
                        jcxmCur = "弯曲";
                        int Gs = 0;
                        decimal sum = 0;

                        if (sItem["GJLB"] == "对接接头")
                        {
                            for (int i = 1; i < 5; i++)
                            {
                               
                                    sItem["MWJG" + i] = sItem["LW" + i];
                              
                                    
                             

                            }
                            for(int i = 5; i < 9; i++)
                            {
                                sItem["BWJG" + (i - 4)] = sItem["LW" + i];
                            }
                            for(int i = 1; i < 9; i++)
                            {
                                if (sItem["SYLX" + i] == "侧弯")
                                {
                                    sItem["CWJG" + i] = sItem["LW" + i];
                                }
                                //弯曲最大单值 
                                if (GetSafeDecimal(sItem["WQZDDZ" + i]) > 3)
                                {
                                    Gs = Gs + 1;
                                }
                                //总和值
                                if (GetSafeDecimal(sItem["WQZHZ" + i]) > 7)
                                {
                                    Gs = Gs + 1;
                                }
                                sum += GetSafeDecimal(sItem["WQZHZ" + i]);
                            }
                            sItem["SYZHZ"] = sum.ToString();
                            if (sum > 24 )
                            {
                                Gs++;
                            }
                        }
                        else
                        {
                            //栓钉焊接头
                            for (int i = 1; i < 9; i++)
                            {
                                if (sItem["LW" + i] != "无裂纹")
                                {
                                    Gs++;
                                }
                            }
                        }

                        if (Gs < 1)
                        {
                            sItem["JCJG_LW"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LW"] = "不符合";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                      
                        sItem["JCJG_LW"] = "----";
                        sItem["LW1"] = "----";
                        sItem["LW2"] = "----";
                        sItem["LW3"] = "----";
                        sItem["LW4"] = "----";
                    }
                    #endregion
                    if ((sItem["JCJG_LS"] == "符合" || sItem["JCJG_LS"] == "----") && (sItem["JCJG_LW"] == "符合" || sItem["JCJG_LW"] == "----"))
                    {
                        sItem["JCJG"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["JCJG"] = "不合格";
                        mFlag_Bhg = true;
                    }
                }
                mjcjg = sItem["JCJG"];

                mAllHg = mAllHg && (sItem["JCJG"] == "合格");
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求";
            }

            if (mjcjg == "不下结论")
            {
                jsbeizhu = "找不到对应指标";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
