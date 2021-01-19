using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SPB : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            double[] hnl = new double[2];
            string mcalBh, mlongStr;
            double[] ljsy = new double[7];
            List<string> mtmpArray = new List<string>();
            string mSjdjbh, mSjdj;
            double mSz, mQdyq, mHsxs;
            int vp;
            string mjlgs;
            string mMaxBgbh, mkljpq;
            string mJSFF;
            bool mAllHg = true;
            bool mGetBgbh = false;
            bool mSFwc;
            double mMj;
            mSFwc = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_SPB_DJ"];
            var MItem = data["M_SPB"];
            var SItem = data["S_SPB"];
            #endregion

            #region  计算开始
            foreach (var sitem in SItem)
            {

                mSjdj = sitem["SJDJ"];
                mMj = 70.7 * 70.7;
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                var mrsDj_filter = mrsDj.FirstOrDefault(u => u["MC"].Contains(mSjdj));
                if (mrsDj_filter == null || mrsDj_filter.Count == 0)
                {
                    sitem["JCJG"] = "不下结论";
                    mAllHg = false;
                    break;
                }
                //计算龄期
                sitem["LQ"] = (GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString();
                double md1, md2, md, pjmd, sum;
                int xd, Gs;
                bool sign;

                string jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                #region 用料配合比计算
                if (IsNumeric(MItem[0]["T_CLSN"]))
                {
                    //水
                    if (IsNumeric(MItem[0]["T_CLS"]))
                    {
                        MItem[0]["T_PBS"] = Round(GetSafeDouble(MItem[0]["T_CLS"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 2).ToString("0.00");
                    }
                    else
                    {
                        MItem[0]["T_PBS"] = "0";
                    }
                    //砂
                    if (IsNumeric(MItem[0]["T_CLSA"]))
                    {
                        MItem[0]["T_PBSA"] = Round(GetSafeDouble(MItem[0]["T_CLSA"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 2).ToString("0.00");
                    }
                    else
                    {
                        MItem[0]["T_PBSA"] = "0";
                    }
                    //外加剂1
                    if (IsNumeric(MItem[0]["T_CLWJJ1"]))
                    {
                        MItem[0]["T_PBWJJ1"] = Round(GetSafeDouble(MItem[0]["T_CLWJJ1"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 2).ToString("0.00");
                    }
                    else
                    {
                        MItem[0]["T_PBWJJ1"] = "0";
                    }
                    //外加剂2
                    if (IsNumeric(MItem[0]["T_CLWJJ2"]))
                    {
                        MItem[0]["T_PBWJJ2"] = Round(GetSafeDouble(MItem[0]["T_CLWJJ2"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 2).ToString("0.00");
                    }
                    else
                    {
                        MItem[0]["T_PBWJJ2"] = "0";
                    }
                    //掺合料1
                    if (IsNumeric(MItem[0]["T_CLCHL1"]))
                    {
                        MItem[0]["T_PBCHL1"] = Round(GetSafeDouble(MItem[0]["T_CLCHL1"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 2).ToString("0.00");
                    }
                    else
                    {
                        MItem[0]["T_PBCHL1"] = "0";
                    }
                    //掺合料2
                    if (IsNumeric(MItem[0]["T_CLCHL2"]))
                    {
                        MItem[0]["T_PBCHL2"] = Round(GetSafeDouble(MItem[0]["T_CLCHL2"]) / GetSafeDouble(MItem[0]["T_CLSN"]), 2).ToString("0.00");
                    }
                    else
                    {
                        MItem[0]["T_PBCHL2"] = "0";
                    }
                }
                #endregion
                if (sitem["JCXM"].Contains("7天强度") || jcxm.Contains("、配合比、"))
                {
                    if (!IsNumeric(sitem["KYHZ1_1"]) || Conversion.Val(sitem["KYHZ1_1"]) == 0)
                    {
                        DateTime dtnow = DateTime.Now;
                        if (DateTime.TryParse(sitem["ZZRQ"], out dtnow))
                            sitem["YQSYRQ"] = GetSafeDateTime(sitem["ZZRQ"]).AddDays(28).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    List<double> larray = new List<double>();
                    for (xd = 1; xd < 4; xd++)
                    {
                        if (IsNumeric(sitem["KYHZ" + xd + "_7"]) && Conversion.Val(sitem["KYHZ" + xd + "_7"]) != 0)
                        {
                            md = GetSafeDouble(sitem["KYHZ" + xd + "_7"].Trim());
                            md = 1350 * md / (70.7 * 70.7);
                            md = Round(md, 1);
                            sitem["KYQD" + xd + "_7"] = md.ToString("0.0");
                            larray.Add(md);
                        }
                        else
                            sitem["KYQD" + xd + "_7"] = "----";
                    }

                    if (larray.Count > 0)
                    {
                        sitem["KYPJ_7"] = larray.Average().ToString("0.0");
                    }
                    else
                    {
                        sitem["KYPJ_7"] = "----";
                    }
                }
                else
                {
                    for (xd = 1; xd < 4; xd++)
                        sitem["KYQD" + xd + "_7"] = "----";
                    sitem["KYPJ_7"] = "----";
                }
                if (sitem["JCXM"].Contains("28天强度") || jcxm.Contains("、配合比、"))
                {
                    if (!IsNumeric(sitem["KYHZ1_1"]) || Conversion.Val(sitem["KYHZ1_1"]) == 0)
                    {
                        DateTime dtnow = DateTime.Now;
                        if (DateTime.TryParse(sitem["ZZRQ"], out dtnow))
                        {
                            sitem["YQSYRQ"] = GetSafeDateTime(sitem["ZZRQ"]).AddDays(28).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            sitem["KYPJ"] = "----";
                        }
                    }
                    List<double> larray = new List<double>();
                    for (xd = 1; xd < 4; xd++)
                    {
                        if (IsNumeric(sitem["KYHZ" + xd]) && Conversion.Val(sitem["KYHZ" + xd]) != 0)
                        {
                            md = GetSafeDouble(sitem["KYHZ" + xd].Trim());
                            md = 1350 * md / (70.7 * 70.7);
                            md = Round(md, 1);
                            sitem["KYQD" + xd] = md.ToString("0.0");
                            larray.Add(md);
                        }
                        else
                            sitem["KYQD" + xd] = "----";
                    }
                    if (larray.Count>0)
                    {
                        sitem["KYPJ"] = larray.Average().ToString("0.0");
                    }
                    else
                    {
                        sitem["KYPJ"] = "----";
                    }
                }
                else
                {
                    for (xd = 1; xd < 4; xd++)
                        sitem["KYQD" + xd] = "----";
                    sitem["KYPJ"] = "----";
                }
                if (sitem["JCXM"].Contains("抗冻"))
                {

                }
                else
                {
                    sitem["QDSSL"] = "----";
                    sitem["ZLSSL"] = "----";
                }
                if (sitem["JCXM"].Contains("拉伸粘结强度"))
                {
                    List<double> larray = new List<double>();
                    for (int i = 1; i < 11; i++)
                    {
                        sitem["LSQDQD" + i] = Math.Round(Conversion.Val(sitem["LSQDHZ" + i]) / (Conversion.Val(sitem["LSQDCD" + i]) * Conversion.Val(sitem["LSQDKD" + i])), 3).ToString();
                        larray.Add(GetSafeDouble(sitem["LSQDQD" + i]));
                    }

                    int n = 0;
                    double avg = 0;
                    avg = larray.Average();
                    //foreach (var l in larray)
                    //{
                    //    if (Math.Abs((l-avg)/avg*100) > 20)
                    //    {
                    //        larray.Remove(l);
                    //        n++;
                    //    }
                    //}

                    for (int i = 0; i < larray.Count; i++)
                    {
                        if (Math.Abs((larray[i] - avg) / avg * 100) > 20)
                        {
                            larray.Remove(larray[i]);
                            n++;

                        }
                    }
                    if (n < 4 && n != 0)
                    {
                        sitem["LSQDQD"] = "结果无效";
                    }
                    else
                    {
                        sitem["LSQDQD"] = Math.Round(larray.Average(), 2).ToString("0.00");
                    }
                }
                else
                {
                    sitem["LSQDQD"] = "----";
                }
                if (sitem["JCXM"].Contains("凝结时间") || jcxm.Contains("、配合比、"))
                {
                    sitem["ZNSJ"] = (Round(((GetSafeDouble(sitem["T1ZN"]) + GetSafeDouble(sitem["T2ZN"])) / 2) / 5, 0) * 5).ToString();
                }
                else
                {
                    sitem["ZNSJ"] = "0";
                    sitem["T1ZN"] = "0";
                    sitem["T2ZN"] = "0";
                }

                #region 抗渗性能
                if (sitem["JCXM"].Contains("抗渗性能"))
                {
                    if (Conversion.Val(sitem["KSKYQD1"]) > 0)
                    {
                        sitem["W_KSXN"] = "P" + (Conversion.Val(sitem["KSKYQD1"]) * 10 - 1).ToString();
                    }
                }
                else
                {
                    sitem["W_KSXN"] = "----";
                }
                #endregion

            }

            //主表总判断赋值
            //if (mAllHg)
            //    MItem[0]["JCJG"] = "合格";
            //else
            //    MItem[0]["JCJG"] = "不合格";

            if (mAllHg)
                MItem[0]["JCJG"] = "----";
            else
                MItem[0]["JCJG"] = "----";

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
