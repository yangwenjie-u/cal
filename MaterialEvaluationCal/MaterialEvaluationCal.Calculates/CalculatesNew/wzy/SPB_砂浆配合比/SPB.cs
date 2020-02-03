﻿using System;
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
                    sitem["JCJG"] = "依据不详";
                    break;
                }
                //计算龄期
                sitem["LQ"] = (GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString();
                double md1, md2, md, pjmd, sum;
                int xd, Gs;
                bool sign;

                string jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、理论配合比、") || jcxm.Contains("、配合比、"))
                {
                    //if (sitem["JCXM"].Contains("7天强度"))
                    //{

                        if (!IsNumeric(sitem["KYHZ1_1"]) || Conversion.Val(sitem["KYHZ1_1"]) == 0)
                        {
                            DateTime dtnow = DateTime.Now;
                            if (DateTime.TryParse(sitem["ZZRQ"], out dtnow))
                                sitem["YQSYRQ"] = GetSafeDateTime(sitem["ZZRQ"]).AddDays(28).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        for (xd = 1; xd < 4; xd++)
                        {
                            if (IsNumeric(sitem["KYHZ" + xd + "_7"]) && Conversion.Val(sitem["KYHZ" + xd + "_7"]) != 0)
                            {
                                md = GetSafeDouble(sitem["KYHZ" + xd + "_7"].Trim());
                                md = 1350 * md / (70.7 * 70.7);
                                md = Round(md, 1);
                                sitem["KYQD" + xd + "_7"] = md.ToString("0.0");
                            }
                            else
                                sitem["KYQD" + xd + "_7"] = "----";
                        }
                    //}
                    //else
                    //{
                    //    for (xd = 1; xd < 4; xd++)
                    //        sitem["KYQD" + xd + "_7"] = "----";
                    //    sitem["KYPJ_7"] = "----";
                    //}
                    //if (sitem["JCXM"].Contains("28天强度"))
                    //{
                        if (!IsNumeric(sitem["KYHZ1_1"]) || Conversion.Val(sitem["KYHZ1_1"]) == 0)
                        {
                            DateTime dtnow = DateTime.Now;
                            if (DateTime.TryParse(sitem["ZZRQ"], out dtnow))
                                sitem["YQSYRQ"] = GetSafeDateTime(sitem["ZZRQ"]).AddDays(28).ToString("yyyy-MM-dd HH:mm:ss");
                            sitem["KYPJ"] = "----";
                        }
                        for (xd = 1; xd < 4; xd++)
                        {
                            if (IsNumeric(sitem["KYHZ" + xd]) && Conversion.Val(sitem["KYHZ" + xd]) != 0)
                            {
                                md = GetSafeDouble(sitem["KYHZ" + xd].Trim());
                                md = 1350 * md / (70.7 * 70.7);
                                md = Round(md, 1);
                                sitem["KYQD" + xd] = md.ToString("0.0");
                            }
                            else
                                sitem["KYQD" + xd] = "----";
                        }
                    //}
                    //else
                    //{
                    //    for (xd = 1; xd < 4; xd++)
                    //        sitem["KYQD" + xd] = "----";
                    //    sitem["KYPJ"] = "----";
                    //}
                    //if (sitem["JCXM"].Contains("抗冻"))
                    //{

                    //}
                    //else
                    //{
                        sitem["QDSSL"] = "----";
                        sitem["ZLSSL"] = "----";
                    //}
                    //if (sitem["JCXM"].Contains("拉伸粘结强度"))
                    //{

                    //}
                    //else
                    //{
                        sitem["NJQD"] = "----";
                    //}
                    //if (sitem["JCXM"].Contains("凝结时间"))
                    //{
                        sitem["ZNSJ"] = (Round(((GetSafeDouble(sitem["T1ZN"]) + GetSafeDouble(sitem["T2ZN"])) / 2) / 5, 0) * 5).ToString();
                    //}
                    //else
                    //{
                    //    sitem["ZNSJ"] = "0";
                    //    sitem["T1ZN"] = "0";
                    //    sitem["T2ZN"] = "0";
                    //}
                }

                //主表总判断赋值
                if (mAllHg)
                    MItem[0]["JCJG"] = "合格";
                else
                    MItem[0]["JCJG"] = "不合格";
            }

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
