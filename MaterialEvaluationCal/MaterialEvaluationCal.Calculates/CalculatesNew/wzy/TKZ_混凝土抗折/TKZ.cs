﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TKZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/

            #region  参数定义
            string mcalBh, mlongStr;
            double[] mkzqdArray = new double[3];
            double[] mkzhzArray = new double[3];
            string[] mtmpArray;
            double mSjcc, mMj, mSjcc1;
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd;
            double mMaxKyhz, mMinKyhz, mMidKyhz, mAvgKyhz;
            string mSjdjbh, mSjdj;
            double mSz, mQdyq, mHsxs;
            int vp;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_TKZ_DJ"];
            var mrsGg = dataExtra["BZ_TKZCC"];
            var MItem = data["M_TKZ"];
            var mitem = MItem[0];
            var SItem = data["S_TKZ"];
            #endregion

            #region  计算开始
            mGetBgbh = false;
            mAllHg = true;
            mitem["JCJGMS"] = "";
            foreach (var sitem in SItem)
            {
                mSjdj = sitem["SJDJ"];
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                var mrsGg_Filter = mrsGg.FirstOrDefault(x => x["MC"].Contains(sitem["CCMC"]));
                if (mrsGg_Filter != null && mrsGg_Filter.Count > 0)
                {
                    sitem["SJKD"] = mrsGg_Filter["SJKD"];
                    sitem["SJGD"] = mrsGg_Filter["SJGD"];
                    sitem["SJCD"] = mrsGg_Filter["SJCD"];
                    sitem["HSXS"] = mrsGg_Filter["HSXS"];
                }
                else
                {
                    sitem["SJCC"] = "0";
                    sitem["HSXS"] = "0";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "规格不祥。";
                    continue;
                }
                mHsxs = GetSafeDouble(sitem["HSXS"]);
                if (mHsxs <= 0) //换算系数=0则不予计算
                {
                    continue;
                }
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_item = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()));
                if (mrsDj_item != null && mrsDj_item.Count() > 0)
                {
                    mSz = GetSafeDouble(mrsDj_item["SZ"]);
                    mJSFF = string.IsNullOrEmpty(mrsDj_item["JSFF"]) ? "" : mrsDj_item["JSFF"].Trim().ToLower();
                }
                else
                {
                    mSz = 0;
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "设计等级为空或不存在";
                    continue;
                }
                sitem["LQ"] = (GetSafeDateTime(mitem["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString();
                sitem["DDSJQD"] = "0";
                sitem["KZQD1"] = Round(1000 * Conversion.Val(sitem["KZHZ1"]) * Conversion.Val(sitem["SJCD"]) / (Conversion.Val(sitem["SJKD"]) * Conversion.Val(sitem["SJGD"]) * Conversion.Val(sitem["SJGD"])) * mHsxs, 1).ToString();
                sitem["KZQD2"] = Round(1000 * Conversion.Val(sitem["KZHZ2"]) * Conversion.Val(sitem["SJCD"]) / (Conversion.Val(sitem["SJKD"]) * Conversion.Val(sitem["SJGD"]) * Conversion.Val(sitem["SJGD"])) * mHsxs, 1).ToString();
                sitem["KZQD3"] = Round(1000 * Conversion.Val(sitem["KZHZ3"]) * Conversion.Val(sitem["SJCD"]) / (Conversion.Val(sitem["SJKD"]) * Conversion.Val(sitem["SJGD"]) * Conversion.Val(sitem["SJGD"])) * mHsxs, 1).ToString();
                if (sitem["DYZJW1"] == "1" || sitem["DYZJW2"] == "1" || sitem["DYZJW3"] == "1")
                    mJSFF = "special";
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (string.IsNullOrEmpty(mJSFF))
                {
                    if (jcxm.Contains("、抗折、"))
                    {
                        mlongStr = sitem["KZQD1"] + "," + sitem["KZQD2"] + "," + sitem["KZQD3"];
                        mtmpArray = mlongStr.Split(',');
                        for (vp = 0; vp <= 2; vp++)
                            mkzqdArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        Array.Sort(mkzqdArray);
                        double mMaxkzqd = mkzqdArray[2];
                        double mMinkzqd = mkzqdArray[0];
                        double mMidkzqd = mkzqdArray[1];
                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if (mMidkzqd != 0)
                        {
                            if (mMaxkzqd - mMidkzqd > Round(mMidkzqd * 0.15, 1) && mMidkzqd - mMinkzqd > Round(mMidkzqd * 0.15, 1))
                            {
                                sitem["KZPJ"] = "无效";
                                sitem["DDSJQD"] = "不作评定";
                                sitem["HZCASE"] = "1";
                                sitem["JCJG"] = "不作评定";
                            }
                            if ((mMaxkzqd - mMidkzqd) > Round(mMidkzqd * 0.15, 1) && (mMidkzqd - mMinkzqd) <= Round(mMidkzqd * 0.15, 1))
                            {
                                sitem["HZCASE"] = "2";
                                sitem["KZPJ"] = mMidkzqd.ToString("0.0");
                                if (mSz != 0)
                                {
                                    sitem["DDSJQD"] = Round(Conversion.Val(sitem["KZPJ"]) / mSz * 100, 0).ToString();
                                    if (Conversion.Val(sitem["KZPJ"]) - mSz >= 0)
                                        sitem["JCJG"] = "合格";
                                    else
                                        sitem["JCJG"] = "不合格";
                                }
                                sitem["MIDAVG"] = "1";
                            }
                            if ((mMaxkzqd - mMidkzqd) <= Round(mMidkzqd * 0.15, 1) && (mMidkzqd - mMinkzqd) > Round(mMidkzqd * 0.15, 1))
                            {
                                sitem["HZCASE"] = "3";
                                sitem["KZPJ"] = mMidkzqd.ToString("0.0");
                                if (mSz != 0)
                                {
                                    sitem["DDSJQD"] = Round(Conversion.Val(sitem["KZPJ"]) / mSz * 100, 0).ToString();
                                    if (Conversion.Val(sitem["KZPJ"]) - mSz >= 0)
                                        sitem["JCJG"] = "合格";
                                    else
                                        sitem["JCJG"] = "不合格";
                                }
                                sitem["MIDAVG"] = "1";
                            }
                            if ((mMaxkzqd - mMidkzqd) <= Round(mMidkzqd * 0.15, 1) && (mMidkzqd - mMinkzqd) <= Round(mMidkzqd * 0.15, 1))
                            {
                                sitem["HZCASE"] = "4";
                                if (mHsxs != 0)
                                    sitem["KZPJ"] = Round((Conversion.Val(sitem["KZQD1"]) + Conversion.Val(sitem["KZQD2"]) + Conversion.Val(sitem["KZQD3"])) / 3, 1).ToString();
                                if (mSz != 0)
                                {
                                    sitem["DDSJQD"] = Round(Conversion.Val(sitem["KZPJ"]) / mSz * 100, 0).ToString();
                                    if (Conversion.Val(sitem["KZPJ"]) - mSz >= 0)
                                        sitem["JCJG"] = "合格";
                                    else
                                        sitem["JCJG"] = "不合格";
                                }
                            }
                        }
                    }
                    else
                    {
                        sitem["KZPJ"] = "----";
                        sitem["JCJG"] = "----";
                    }
                }
                else if (mJSFF == "special")
                {
                    if (jcxm.Contains("、抗折、"))
                    {
                        if (sitem["DYZJW1"] == "1" && sitem["DYZJW2"] == "0" && sitem["DYZJW3"] == "0")
                        {
                            sitem["KZQD2"] = Round(1000 * Conversion.Val(sitem["KZHZ2"]) * Conversion.Val(sitem["SJCD"]) / (Conversion.Val(sitem["SJKD"]) * Conversion.Val(sitem["SJGD"]) * Conversion.Val(sitem["SJGD"])) * mHsxs, 1).ToString();
                            sitem["KZQD3"] = Round(1000 * Conversion.Val(sitem["KZHZ3"]) * Conversion.Val(sitem["SJCD"]) / (Conversion.Val(sitem["SJKD"]) * Conversion.Val(sitem["SJGD"]) * Conversion.Val(sitem["SJGD"])) * mHsxs, 1).ToString();
                            if (Math.Abs(Conversion.Val(sitem["KZQD3"]) - Conversion.Val(sitem["KZQD2"])) <= Round(Conversion.Val(sitem["KZQD2"]) * 0.15, 1) && Math.Abs(Conversion.Val(sitem["KZQD3"]) - Conversion.Val(sitem["KZQD2"])) <= Round(Conversion.Val(sitem["KZQD3"]) * 0.15, 1))
                            {
                                sitem["KZPJ"] = Round((Conversion.Val(sitem["KZQD2"]) + Conversion.Val(sitem["KZQD3"])) / 2, 1).ToString();
                                if (mSz != 0)
                                {
                                    sitem["DDSJQD"] = Round(Conversion.Val(sitem["KZPJ"]) / mSz * 100, 0).ToString();
                                    if (Conversion.Val(sitem["KZPJ"]) - mSz >= 0)
                                        sitem["JCJG"] = "合格";
                                    else
                                        sitem["JCJG"] = "不合格";
                                }
                            }
                            else
                            {
                                sitem["KZPJ"] = "无效";
                                sitem["JCJG"] = "不作评定";
                            }
                        }
                        if (sitem["DYZJW2"] == "1" && sitem["DYZJW1"] == "0" && sitem["DYZJW3"] == "0")
                        {
                            sitem["KZQD1"] = Round(1000 * Conversion.Val(sitem["KZHZ1"]) * Conversion.Val(sitem["SJCD"]) / (Conversion.Val(sitem["SJKD"]) * Conversion.Val(sitem["SJGD"]) * Conversion.Val(sitem["SJGD"])) * mHsxs, 1).ToString();
                            sitem["KZQD3"] = Round(1000 * Conversion.Val(sitem["KZHZ3"]) * Conversion.Val(sitem["SJCD"]) / (Conversion.Val(sitem["SJKD"]) * Conversion.Val(sitem["SJGD"]) * Conversion.Val(sitem["SJGD"])) * mHsxs, 1).ToString();
                            if (Math.Abs(Conversion.Val(sitem["KZQD3"]) - Conversion.Val(sitem["KZQD1"])) <= Round(Conversion.Val(sitem["KZQD1"]) * 0.15, 1) && Math.Abs(Conversion.Val(sitem["KZQD3"]) - Conversion.Val(sitem["KZQD1"])) <= Round(Conversion.Val(sitem["KZQD3"]) * 0.15, 1))
                            {
                                sitem["KZPJ"] = Round((Conversion.Val(sitem["KZQD1"]) + Conversion.Val(sitem["KZQD3"])) / 2, 1).ToString();
                                if (mSz != 0)
                                {
                                    sitem["DDSJQD"] = Round(Conversion.Val(sitem["KZPJ"]) / mSz * 100, 0).ToString();
                                    if (Conversion.Val(sitem["KZPJ"]) - mSz >= 0)
                                        sitem["JCJG"] = "合格";
                                    else
                                        sitem["JCJG"] = "不合格";
                                }
                            }
                            else
                            {
                                sitem["KZPJ"] = "无效";
                                sitem["JCJG"] = "不作评定";
                            }
                        }
                        if (sitem["DYZJW3"] == "1" && sitem["DYZJW1"] == "0" && sitem["DYZJW2"] == "0")
                        {
                            sitem["KZQD1"] = Round(1000 * Conversion.Val(sitem["KZHZ1"]) * Conversion.Val(sitem["SJCD"]) / (Conversion.Val(sitem["SJKD"]) * Conversion.Val(sitem["SJGD"]) * Conversion.Val(sitem["SJGD"])) * mHsxs, 1).ToString();
                            sitem["KZQD2"] = Round(1000 * Conversion.Val(sitem["KZHZ2"]) * Conversion.Val(sitem["SJCD"]) / (Conversion.Val(sitem["SJKD"]) * Conversion.Val(sitem["SJGD"]) * Conversion.Val(sitem["SJGD"])) * mHsxs, 1).ToString();
                            if (Math.Abs(Conversion.Val(sitem["KZQD2"]) - Conversion.Val(sitem["KZQD1"])) <= Round(Conversion.Val(sitem["KZQD1"]) * 0.15, 1) && Math.Abs(Conversion.Val(sitem["KZQD2"]) - Conversion.Val(sitem["KZQD1"])) <= Round(Conversion.Val(sitem["KZQD2"]) * 0.15, 1))
                            {
                                sitem["KZPJ"] = Round((Conversion.Val(sitem["KZQD1"]) + Conversion.Val(sitem["KZQD2"])) / 2, 1).ToString();
                                if (mSz != 0)
                                {
                                    sitem["DDSJQD"] = Round(Conversion.Val(sitem["KZPJ"]) / mSz * 100, 0).ToString();
                                    if (Conversion.Val(sitem["KZPJ"]) - mSz >= 0)
                                        sitem["JCJG"] = "合格";
                                    else
                                        sitem["JCJG"] = "不合格";
                                }
                            }
                            else
                            {
                                sitem["KZPJ"] = "无效";
                                sitem["JCJG"] = "不作评定";
                            }
                        }
                        if (sitem["DYZJW1"] == "1" && sitem["DYZJW2"] == "1")
                        {
                            sitem["KZPJ"] = "无效";
                            sitem["JCJG"] = "不作评定";
                        }
                        if (sitem["DYZJW1"] == "1" && sitem["DYZJW3"] == "1")
                        {
                            sitem["KZPJ"] = "无效";
                            sitem["JCJG"] = "不作评定";
                        }
                        if (sitem["DYZJW3"] == "1" && sitem["DYZJW2"] == "1")
                        {
                            sitem["KZPJ"] = "无效";
                            sitem["JCJG"] = "不作评定";
                        }
                    }
                    else
                    {
                        sitem["KZPJ"] = "----";
                        sitem["JCJG"] = "----";
                    }
                }
                if (sitem["KZPJ"] == "无效")
                    mitem["JCJGMS"] = "该组试样强度代表值无效。";
                else
                    mitem["JCJGMS"] = "该组试样强度代表值" + sitem["KZPJ"] + "MPa，" + "占设计强度" + sitem["DDSJQD"] + "%。";
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");
            }
            //主表总判断赋值
            if (mAllHg)
                mitem["JCJG"] = "合格";
            else
                mitem["JCJG"] = "不合格";
            #endregion;
            /************************ 代码结束 *********************/
        }
    }
}
