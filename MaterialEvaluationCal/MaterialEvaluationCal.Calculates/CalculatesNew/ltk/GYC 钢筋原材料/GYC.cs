using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class GYC : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", MJcjg = "不合格", jsbeiZHu = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            var ggph = "";//钢筋牌号
            var mJCJG = "";
           
            int mbhggs = 0;//不合格数量
            var extraDJ = dataExtra["BZ_GYC_DJ"];
            var extraZLPCB = dataExtra["BZ_ZLPCB"];
            var data = retData;

            var SItem = data["S_GYC"];
            var MItem = data["M_GYC"];
            if (!data.ContainsKey("M_GYC"))
            {
                data["M_GYC"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = MJcjg;
                m["JCJGMS"] = jsbeiZHu;
                MItem.Add(m);
            }

            string mGJLB = "";
            bool mFlag_Hg = false, mFlag_Bhg = false, mSFwc = true;
            MItem[0]["FJJJ"] = "";
            MItem[0]["FJJJ1"] = "";
            MItem[0]["FJJJ2"] = "";
            MItem[0]["FJJJ3"] = "";
            MItem[0]["ZLPCXS"] = "重量偏差";

            //保留有效数字
            Func<double, int, string> mYxsz =
                  delegate (double t_numeric, int rndto)
                  {
                      string mYxsz_ret = string.Empty;
                      int d_pos;
                      d_pos = -5;
                      while (true)
                      {
                          if (rndto - d_pos < 0)
                          {
                              mYxsz_ret = Round(t_numeric, 0).ToString();
                              break;
                          }
                          if (t_numeric >= Math.Pow(10, (d_pos - 1)) && t_numeric < Math.Pow(10, d_pos))
                          {
                              mYxsz_ret = Round(t_numeric, rndto - d_pos).ToString();
                              break;
                          }
                          d_pos = d_pos + 1;
                      }
                      return mYxsz_ret;
                  };

            Func<IDictionary<string, string>, IDictionary<string, string>, int, string> calc_SCL =
           delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, int count)
           {
               //求伸长率
               string mScl = "";
               if (Math.Abs(GetSafeDouble(sItem["CD"]) - 0) > 0.00001)
               {
                   for (int i = 1; i <= count; i++)
                   {
                       mScl = (100 * (Conversion.Val(sItem["SCZ" + i]) == 0 ? 0 : ((Conversion.Val(sItem["SCZ" + i]) - Conversion.Val(sItem["CD"])) / Conversion.Val(sItem["CD"])))).ToString();
                       if (mItem["PDBZ"].Contains("带肋") || mItem["PDBZ"].Contains("1499.1"))
                       {
                           if (Conversion.Val(mScl) >= 10)
                           {
                               mScl = Conversion.Val(mScl).ToString("0");
                           }
                           else
                           {
                               mScl = (Math.Round(Conversion.Val(mScl) * 2, 0) / 2).ToString("0.0");
                           }
                       }
                       else
                       {
                           mScl = (Math.Round(Conversion.Val(mScl) * 2, 0) / 2).ToString("0.0");
                       }
                       sItem["SCL" + i] = mScl;
                   }
               }
               else
               {
                   for (int i = 1; i <= count; i++)
                   {
                       sItem["SCL" + i] = "0";
                   }
               }
               return "";
           };

            Func<IDictionary<string, string>, int, string> calc_qf =
                delegate (IDictionary<string, string> sItem, int count)
                {
                    //求屈服强度及抗拉强度
                    double mMidVal = 0;
                    string mMj = "", mqf = "";

                    if (string.IsNullOrEmpty(sItem["ZJ"]))
                    {
                        sItem["ZJ"] = "0";
                    }
                    mMidVal = (GetSafeDouble(sItem["ZJ"]) / 2) * (GetSafeDouble(sItem["ZJ"]) / 2);
                    mMj = (3.14159 * mMidVal).ToString();
                    if (sItem["SJDJ"].Contains("冷轧带肋"))
                    {
                        mMj = (Math.Round(Conversion.Val(mMj), 1)).ToString("0.0");
                    }
                    else
                    {
                        mMj = mYxsz(GetSafeDouble(mMj), 4);
                    }
                    sItem["MJ"] = mMj;
                    if (Math.Abs(Conversion.Val(mMj) - 0) > 0.00001)
                    {
                        for (int i = 1; i <= count; i++)
                        {
                            if (string.IsNullOrEmpty(sItem["QFHZ" + i]))
                            {
                                sItem["QFHZ" + i] = "0";
                            }

                            mqf = (1000 * Conversion.Val(sItem["QFHZ" + i]) / Conversion.Val(mMj)).ToString();

                            if (Conversion.Val(mqf) <= 200)
                            {
                                sItem["QFQD" + i] = (Math.Round(Conversion.Val(mqf), 0)).ToString();
                            }

                            if (Conversion.Val(mqf) > 200 && Conversion.Val(mqf) <= 1000)
                            {
                                sItem["QFQD" + i] = (Math.Round(Conversion.Val(mqf) / 5, 0) * 5).ToString();
                            }

                            if (Conversion.Val(mqf) > 1000)
                            {
                                sItem["QFQD" + i] = (Math.Round(Conversion.Val(mqf) / 10, 0) * 10).ToString();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= count; i++)
                        {
                            sItem["QFQD" + i] = "0";
                        }
                    }

                    return "";
                };

            Func<IDictionary<string, string>, int, string> calc_kl =
                delegate (IDictionary<string, string> sItem, int count)
                {
                    //求抗拉强度
                    double mMidVal = 0;
                    string mMj = "", mkl = "";
                    if (string.IsNullOrEmpty(sItem["ZJ"]))
                    {
                        sItem["ZJ"] = "0";
                    }
                    mMidVal = (GetSafeDouble(sItem["ZJ"]) / 2) * (GetSafeDouble(sItem["ZJ"]) / 2);
                    mMj = (3.14159 * mMidVal).ToString();

                    if (sItem["SJDJ"].Contains("冷轧带肋"))
                    {
                        mMj = (Math.Round(Conversion.Val(mMj), 1)).ToString("0.0");
                    }
                    else
                    {
                        mMj = mYxsz(GetSafeDouble(mMj), 4);
                    }

                    if (sItem["SJDJ"].Contains("冷轧扭"))
                    {
                        switch (sItem["SJDJ"])
                        {
                            case "冷轧扭CTB550Ⅰ":
                                switch (sItem["ZJ"])
                                {
                                    case "6.5":
                                        mMj = "29.50";
                                        break;
                                    case "8":
                                        mMj = "45.30";
                                        break;
                                    case "10":
                                        mMj = "68.30";
                                        break;
                                    case "12":
                                        mMj = "96.14";
                                        break;
                                }
                                break;

                            case "冷轧扭CTB550Ⅱ":
                                switch (sItem["ZJ"])
                                {
                                    case "6.5":
                                        mMj = "29.20";
                                        break;
                                    case "8":
                                        mMj = "42.30";
                                        break;
                                    case "10":
                                        mMj = "66.10";
                                        break;
                                    case "12":
                                        mMj = "92.74";
                                        break;
                                }
                                break;

                            case "冷轧扭CTB550Ⅲ":
                                switch (sItem["ZJ"])
                                {
                                    case "6.5":
                                        mMj = "29.86";
                                        break;
                                    case "8":
                                        mMj = "45.24";
                                        break;
                                    case "10":
                                        mMj = "70.69";
                                        break;
                                }
                                break;
                            case "冷轧扭CTB650Ⅲ":
                                switch (sItem["ZJ"])
                                {
                                    case "6.5":
                                        mMj = "28.20";
                                        break;
                                    case "8":
                                        mMj = "42.73";
                                        break;
                                    case "10":
                                        mMj = "66.76";
                                        break;
                                }
                                break;
                        }
                    }
                    sItem["MJ"] = mMj;

                    if (Math.Abs(Conversion.Val(mMj) - 0) > 0.00001)
                    {
                        for (int i = 1; i <= count; i++)
                        {
                            if (string.IsNullOrEmpty(sItem["KLHZ" + i]))
                            {
                                sItem["KLHZ" + i] = "0";
                            }

                            mkl = (1000 * Conversion.Val(sItem["KLHZ" + i]) / Conversion.Val(mMj)).ToString();

                            if (Conversion.Val(mkl) <= 200)
                            {
                                sItem["KLQD" + i] = (Math.Round(Conversion.Val(mkl), 0)).ToString();
                            }

                            if (Conversion.Val(mkl) > 200 && Conversion.Val(mkl) <= 1000)
                            {
                                sItem["KLQD" + i] = (Math.Round(Conversion.Val(mkl) / 5, 0) * 5).ToString();
                            }

                            if (Conversion.Val(mkl) > 1000)
                            {
                                sItem["KLQD" + i] = (Math.Round(Conversion.Val(mkl) / 10, 0) * 10).ToString();
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= count; i++)
                        {
                            sItem["KLQD" + i] = "0";
                        }
                    }
                    return "";
                };
            //单项检测项目不合格
            Func<IDictionary<string, string>, IDictionary<string, string>, string, double, int, int> find_singlezb_bhg =
                delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, string zbName, double mbzValue, int count)
                {
                    //返回值为每组每种指标不合格总数  ' mbzValue 是单前判断指标的标准值, count 是一组中的检测个数

                    int mcnt = 0, mCurBHG_QF = 0, this_bhg = 0;// '计算单组合格个数,计算单组不合格个数,当前组单个指标不合格累加
                    switch (zbName)
                    {
                        case "qf":
                            for (int i = 1; i <= count; i++)
                            {
                                if (Conversion.Val(sItem["QFQD" + i]) - mbzValue > -0.00001)
                                {
                                    mcnt = mcnt + 1;
                                }
                                else
                                {
                                    this_bhg = this_bhg + 1;
                                }
                            }
                            sItem["HG_QF"] = mcnt.ToString();
                            break;
                        case "kl":
                            for (int i = 1; i <= count; i++)
                            {
                                if (Conversion.Val(sItem["KLQD" + i]) - mbzValue > -0.00001)
                                {
                                    mcnt = mcnt + 1;
                                }
                                else
                                {
                                    this_bhg = this_bhg + 1;
                                }
                            }
                            sItem["HG_KL"] = mcnt.ToString();
                            break;
                        case "SCL":
                            for (int i = 1; i <= count; i++)
                            {
                                if (Conversion.Val(sItem["SCL" + i]) - mbzValue > -0.00001)
                                {
                                    mcnt = mcnt + 1;
                                }
                                else
                                {
                                    this_bhg = this_bhg + 1;

                                }
                            }
                            sItem["HG_SC"] = mcnt.ToString();
                            break;
                        case "LW":
                            for (int i = 1; i <= count; i++)
                            {
                                if (Conversion.Val(sItem["LW" + i]) - mbzValue > -0.00001)
                                {
                                    mcnt = mcnt + 1;
                                }
                                else
                                {
                                    if (i > 1)
                                    {
                                        if (Conversion.Val(sItem["LW1"]) - i * mbzValue < -0.00001) //' 判断是否把冷弯值全部输在第一个值上
                                        {
                                            this_bhg = this_bhg + 1;
                                        }
                                        else
                                        {
                                            mcnt = mcnt + 1;
                                        }
                                    }
                                    else
                                    {
                                        this_bhg = this_bhg + 1;
                                    }
                                }
                            }
                            sItem["HG_LW"] = mcnt.ToString();
                            break;
                    }

                    return this_bhg;
                };

            Func<IDictionary<string, string>, IDictionary<string, string>, int, int, int, int, string> all_zb_jl =
                delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, int mHggs_QFQD, int mHggs_KLQD, int mHggs_SCL, int mHggs_LW)
                {
                    if (string.IsNullOrEmpty(sItem["HG_QF"])) sItem["HG_QF"] = "0";
                    if (string.IsNullOrEmpty(sItem["HG_SC"])) sItem["HG_SC"] = "0";
                    if (string.IsNullOrEmpty(sItem["HG_LW"])) sItem["HG_LW"] = "0";
                    if (string.IsNullOrEmpty(sItem["HG_KL"])) sItem["HG_KL"] = "0";

                    if (jcxm.Contains("、拉伸、"))
                    {
                        if (Conversion.Val(sItem["HG_QF"]) >= mHggs_QFQD && Conversion.Val(sItem["HG_KL"]) >= mHggs_KLQD && Conversion.Val(sItem["HG_SC"]) >= mHggs_SCL)
                        {
                            sItem["JCJG_LS"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LS"] = "不符合";
                        }
                    }
                    else
                    {
                        sItem["JCJG_LS"] = "----";
                    }

                    if (jcxm.Contains("、冷弯、") || jcxm.Contains("、弯曲、"))
                    {
                        if (Conversion.Val(sItem["HG_LW"]) - mHggs_LW > -0.00001)
                        {
                            sItem["JCJG_LW"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LW"] = "不符合";
                        }
                    }
                    else
                    {
                        sItem["JCJG_LW"] = "----";
                        sItem["LW1"] = "----";
                        sItem["LW2"] = "----";
                        sItem["LW3"] = "----";
                        //if (gCurSylb == "ghf" || gCurSylb == "GHF")
                        //{
                        //    sItem["LW4"] ="----";
                        //    sItem["LW5"] ="----";
                        //    sItem["LW6"] ="----";
                        //}
                        //if (gCurSylb == "gyf" || gCurSylb == "GYF")
                        //{
                        //    sItem["LW4"] ="----";
                        //}
                    }

                    return "";
                };

            Func<IDictionary<string, string>, IDictionary<string, string>, int, int, int, int, int, int, int, int, string> check_double_Fj =
                delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, int mHggs_QFQD, int mHggs_KLQD, int mHggs_SCL, int mHggs_LW, int mFsgs_QFQD, int mFsgs_KLQD, int mFsgs_SCL, int mFsgs_LW)
                {
                    if (sItem["JCJG_LS"] == "不符合" && sItem["JCJG_LW"] == "不符合" || sItem["JCJG_ZLPC"] == "不符合")
                    {
                        sItem["JCJG"] = "不合格";
                        mItem["FJJJ2"] = mItem["FJJJ2"] + "1#";
                    }
                    else
                    {
                        if (sItem["JCJG_LS"] == "不符合" || sItem["JCJG_LW"] == "不符合")
                        {
                            sItem["JCJG"] = "复试";
                            mItem["FJJJ1"] = mItem["FJJJ1"] + "1#";
                        }
                    }
                    return "";
                };

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Replace(",", "、") + "、";
                ggph = sItem["GCLX_PH"];

                double SLLZL = 0, mQfqd = 0, mKlqd = 0, mScl = 0, mLw = 0;
                double mHggs_QFQD = 0, mHggs_KLQD = 0, mHggs_SCL = 0, mHggs_LW = 0, mXLGS = 0, mXWGS = 0;
                double mFsgs_QFQD = 0, mFsgs_KLQD = 0, mFsgs_SCL = 0, mFsgs_LW = 0, MFFWQCS = 0;
                double mLWZJ = 0, mLwjd = 0;
                string mJSFF = "", sZlpc = "", LwBzyq = "";
                double md = 0, md1 = 0, md2 = 0;
                mGJLB = string.IsNullOrEmpty(sItem["GJLB"]) ? "----" : sItem["GJLB"];//钢材类别

                #region BZ_GYC_DJ处理
                //'从设计等级表中取得相应的计算数值、等级标准                   钢材牌号
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["GJLB"] == mGJLB && GetSafeDouble(u["ZJFW1"]) < GetSafeDouble(sItem["ZJ"]) && GetSafeDouble(u["ZJFW2"]) > GetSafeDouble(sItem["ZJ"]));
                if (null != extraFieldsDj)
                {
                    sItem["SJDJ"] = extraFieldsDj["MC"];
                    mQfqd = GetSafeDouble(Conversion.Val(extraFieldsDj["QFQDBZZ"]).ToString());//屈服点标准值
                    mKlqd = GetSafeDouble(Conversion.Val(extraFieldsDj["KLQDBZZ"]).ToString());//抗拉强度标准值
                    mScl = GetSafeDouble(Conversion.Val(extraFieldsDj["SCLBZZ"]).ToString());//伸长率标准值
                    mLw = GetSafeDouble(Conversion.Val(extraFieldsDj["LWBZZ"]).ToString());//冷弯值标准值

                    mHggs_QFQD = GetSafeDouble(extraFieldsDj["ZHGGS_QFQD"]);//'单组合格个数
                    mHggs_KLQD = GetSafeDouble(extraFieldsDj["ZHGGS_KLQD"]);//单组抗拉强度几个合格
                    mHggs_SCL = GetSafeDouble(extraFieldsDj["ZHGGS_SCL"]);//单组屈服点几个合格
                    mHggs_LW = GetSafeDouble(extraFieldsDj["ZHGGS_LW"]);//单组冷弯几个合格

                    mFsgs_QFQD = GetSafeDouble(extraFieldsDj["ZFSGS_QFQD"]); // 单组屈服点几个不合格复试
                    mFsgs_KLQD = GetSafeDouble(extraFieldsDj["ZFSGS_KLQD"]); //单组抗拉强度几个不合格复试
                    mFsgs_SCL = GetSafeDouble(extraFieldsDj["ZFSGS_SCL"]);//单组伸长率几个不合格复试
                    mFsgs_LW = GetSafeDouble(extraFieldsDj["ZFSGS_LW"]);//单组冷弯几个不合格复试

                    mLWZJ = GetSafeDouble(extraFieldsDj["LWZJ"]); // 冷弯直径
                    mLwjd = GetSafeDouble(extraFieldsDj["LWJD"]);//冷弯角度
                    MFFWQCS = GetSafeDouble(extraFieldsDj["FFWQCS"]);//反复弯曲次数

                    mXLGS = GetSafeDouble(extraFieldsDj["XLGS"]);//拉根数
                    mXWGS = GetSafeDouble(extraFieldsDj["XWGS"]);//弯根数
                    MItem[0]["G_WGZZ"] = extraFieldsDj["WGZZ"];//微观组织
                    MItem[0]["G_HGJX"] = extraFieldsDj["HGJX"];//宏观金相
                    MItem[0]["G_JMWSYD"] = extraFieldsDj["JMWSYD"];//截面维氏硬度

                    if (sItem["SFTZ"] == "是")//是否机械调直
                    {
                        mScl = GetSafeDouble(extraFieldsDj["TZHSCLBZZ"]);//调直后伸长率标准值
                    }
                    mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"].ToLower();//计算方法
                }
                else
                {
                    mJSFF = "";
                    mAllHg = false;
                    sItem["JCJG"] = "不下结论";
                    mJCJG = "不下结论";
                    continue;
                }
                #endregion
                
                #region BZ_ZLPCB
                var extraFieldsZLPCB = extraZLPCB.FirstOrDefault(u => u["MC"] == sItem["GJLB"] && GetSafeDouble(u["ZJ"]) == GetSafeDouble(sItem["ZJ"]));
                if (extraFieldsZLPCB != null)
                {
                    //理论重量
                    SLLZL = GetSafeDouble(extraFieldsZLPCB["LLZL"]);
                    sZlpc = extraFieldsZLPCB["ZLPC"];
                    sItem["G_ZLPC"] = "±" + sZlpc.ToString();
                    if (sItem["SFTZ"] == "是")
                    {
                        //                                     调直后重量偏差
                        sItem["G_ZLPC"] = "≥-" + extraFieldsZLPCB["TZHZLPC"];
                        sZlpc = "-" + extraFieldsZLPCB["TZHZLPC"];
                    }
                }
                else
                {
                    SLLZL = 0;
                    sZlpc = "0";
                }
                #endregion

                #region 字段处理
                if (Conversion.Val(sItem["DBSL"]) <= 60)
                {
                    mXLGS = 2;
                    sItem["SCZJ3"] = "----";
                    sItem["QFHZ3"] = "----";
                    sItem["KLHZ3"] = "----";
                    sItem["DLWZ3"] = "----";
                    sItem["SCZ3"] = "----";
                    sItem["ZSCL3"] = "----";
                    sItem["QFQD3"] = "----";
                    sItem["KLQD3"] = "----";
                    sItem["SCL3"] = "----";
                    sItem["QDQFB3"] = "----";
                    sItem["QFQFB3"] = "----";
                    sItem["DHJL3"] = "----";
                }
                else
                {
                    mXLGS = 3;
                }
                //弯心直径
                if (string.IsNullOrEmpty(sItem["LWZJ"]))
                {
                    sItem["LWZJ"] = mLWZJ.ToString();
                }
                //弯曲角度
                if (string.IsNullOrEmpty(sItem["LWJD"]))
                {
                    sItem["LWJD"] = mLwjd.ToString();
                }
                //反向弯曲直径
                if (string.IsNullOrEmpty(sItem["FXWQZJ"]))
                {
                    //sItem["FXWQZJ"] = MFFWQCS.ToString();
                }
                //                                                                                                               直径                      是否机械调直
                if ((MItem[0]["PDBZ"].Contains("1499.2-2018") || MItem[0]["PDBZ"].Contains("1499.1-2017")) && GetSafeDouble(sItem["ZJ"]) <= 12 && sItem["SFTZ"] == "否")
                {
                    //要求重量偏差
                    sItem["G_ZLPC"] = "±6";
                    sZlpc = "6";
                }

                if (MItem[0]["PDBZ"].Contains("1499.2-2018") && sItem["SFTZ"] == "否")
                {
                    sItem["G_ZLPC"] = sItem["G_ZLPC"] + ".0";
                }

                if (mLWZJ == 0 && MFFWQCS != 0)
                {
                    LwBzyq = "弯曲次数不小于" + MFFWQCS + "次，受弯曲部位表面无裂纹";
                }
                else
                {
                    LwBzyq = MItem[0]["JCYJ"].Contains("1999") ? "弯心直径d" : "弯曲压头直径D";

                    if (mLWZJ < 1)
                    {
                        LwBzyq = LwBzyq + "=0" + mLWZJ.ToString() + "a弯曲" + mLwjd.ToString() + "度后，受弯曲部位表面无裂纹";
                    }
                    else
                    {
                        LwBzyq = LwBzyq + "=" + mLWZJ.ToString() + "a弯曲" + mLwjd.ToString() + "度后，受弯曲部位表面无裂纹";
                    }
                }

                if (extraFieldsDj["QFQDBZZ"] == "----")
                {
                    sItem["G_QFQD"] = "----";
                }
                else
                {
                    sItem["G_QFQD"] = "≥" + extraFieldsDj["QFQDBZZ"];
                }

                if (extraFieldsDj["KLQDBZZ"] == "----")
                {
                    sItem["G_KLQD"] = "----";
                }
                else
                {
                    sItem["G_KLQD"] = "≥" + extraFieldsDj["KLQDBZZ"];
                }

                if (mScl == 0)
                {
                    sItem["G_SCL"] = "----";
                }
                else
                {
                    sItem["G_SCL"] = "≥" + extraFieldsDj["SCLBZZ"];
                    if (sItem["SFTZ"] == "是")
                    {
                        sItem["G_SCL"] = "≥" + extraFieldsDj["TZHSCLBZZ"];
                    }
                }

                sItem["G_LWWZ"] = LwBzyq;
                sItem["G_ZSCL"] = "----";//要求最大拉力总伸长率
 
                //'求伸长率
                sItem["XGM"] = extraFieldsDj["XGM"];
                sItem["CD"] = (Math.Round((GetSafeDouble(sItem["XGM"]) * GetSafeDouble(sItem["ZJ"])) / 5 + 0.001, 0) * 5).ToString();

                if (Math.Abs(GetSafeDouble(sItem["XGM"]) - 100) < 0.00001)
                {
                    sItem["CD"] = "100";
                }
                #endregion

                #region 重量偏差
                if (jcxm.Contains("、重量偏差、"))
                {
                    jcxmCur = "重量偏差";
                    double zCD = 0;
                   
                    if (Conversion.Val(sItem["Z_ZZL"]) < 0.1) //总重量(G)
                    {
                        sItem["JCJG_ZLPC"] = "不符合";
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                    }
                    else
                    {
                        if (sItem["SFTZ"] == "是")
                        {
                            MItem[0]["ZLPCXS"] = "重量偏差";//重量偏差显示
                            for (int i = 1; i < 6; i++)
                            {
                                zCD += Conversion.Val(sItem["Z_CD" + i]);//重量偏差长度(MM)1-6
                            }
                            //zCD = Conversion.Val(sItem["Z_CD1"]) + Conversion.Val(sItem["Z_CD2"]) + Conversion.Val(sItem["Z_CD3"]);

                            sItem["ZLPC"] = (Math.Round(100 * (Conversion.Val(sItem["Z_ZZL"]) - Conversion.Val(SLLZL) * zCD) / (Conversion.Val(SLLZL) * zCD), 0)).ToString();
                            if (jcxm.Contains("、拉伸、") && sItem["KLHZ1"] == "0")
                            {
                                sItem["LW1"] = "0";
                                sItem["LW2"] = "0";
                                sItem["LW3"] = "0";
                                //sItem["SYR"] = "";
                            }

                            if (Math.Abs(Conversion.Val(sItem["ZLPC"])) <= Math.Abs(Conversion.Val(sZlpc)))
                            {
                                sItem["JCJG_ZLPC"] = "符合";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["JCJG_ZLPC"] = "不符合";
                                mFlag_Bhg = true;
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhggs = mbhggs + 1;
                            }
                        }
                        else
                        {
                            zCD = Conversion.Val(sItem["Z_CD1"]) + Conversion.Val(sItem["Z_CD2"]) + Conversion.Val(sItem["Z_CD3"]) + Conversion.Val(sItem["Z_CD4"]) + Conversion.Val(sItem["Z_CD5"]);
                            if (MItem[0]["PDBZ"].Contains("1499.2-2018"))
                            {
                                sItem["ZLPC"] = (Math.Round(100 * (Conversion.Val(sItem["Z_ZZL"]) - (SLLZL * zCD)) / (SLLZL * zCD), 1)).ToString("0.0");
                                if (Math.Round(100 * (Conversion.Val(sItem["Z_ZZL"]) - (SLLZL * zCD)) / (SLLZL * zCD), 1) == 0)
                                {
                                    sItem["ZLPC"] = "0";
                                }
                            }
                            else
                            {
                                sItem["ZLPC"] = (Math.Round(100 * (Conversion.Val(sItem["Z_ZZL"]) - (SLLZL * zCD)) / (SLLZL * zCD))).ToString();
                                if (Math.Round(100 * (Conversion.Val(sItem["Z_ZZL"]) - (SLLZL * zCD)) / (SLLZL * zCD)) == 0)
                                {
                                    sItem["ZLPC"] = "0";
                                }
                            }
                            if (jcxm.Contains("、拉伸、") && Conversion.Val(sItem["KLHZ1"]) == 0)
                            {
                            }
                            if (Math.Abs(Conversion.Val(sItem["ZLPC"])) <= Math.Abs(Conversion.Val(sZlpc)))
                            {
                                sItem["JCJG_ZLPC"] = "符合";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                sItem["JCJG_ZLPC"] = "不符合";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mFlag_Bhg = true;
                                mbhggs = mbhggs + 1;
                            }
                        }
                    }
                }
                else
                {
                    sItem["JCJG_ZLPC"] = "----";
                    sItem["ZLPC"] = "----";
                    sItem["G_ZLPC"] = "----";
                }
                #endregion



                //求伸长率
                calc_SCL(MItem[0], sItem, (int)mXLGS);

                //求屈服强度
                calc_qf(sItem, (int)mXLGS);
                if (ggph.Contains("E"))
                {
                    mHggs_SCL = 0;
                    sItem["G_SCL"] = "----";
                    sItem["SCL1"] = "----";
                    sItem["SCL2"] = "----";
                    sItem["SCL3"] = "----";
                }
                //求抗拉强度
                calc_kl(sItem, (int)mXLGS);

                //求单组屈服强度,抗拉强度,伸长率,冷弯 合格个数,并且返回值为不同组不合格数的累加值
                int mallBHG_QF = 0, mallBHG_KL = 0, mallBHG_SC = 0, mallBHG_LW = 0;
                mallBHG_QF = mallBHG_QF + find_singlezb_bhg(MItem[0], sItem, "qf", mQfqd, (int)mXLGS);
                mallBHG_KL = mallBHG_KL + find_singlezb_bhg(MItem[0], sItem, "kl", mKlqd, (int)mXLGS);
                mallBHG_SC = mallBHG_SC + find_singlezb_bhg(MItem[0], sItem, "SCL", mScl, (int)mXLGS);
                if (jcxm.Contains("、冷弯、") || jcxm.Contains("、弯曲、"))
                {
                    mallBHG_LW = mallBHG_LW + find_singlezb_bhg(MItem[0], sItem, "LW", mLw, (int)mXWGS);
                }
                else
                {
                    sItem["LW1"] = "----";
                    sItem["LW2"] = "----";
                    sItem["LW3"] = "----";
                }

                //  做拉伸时默认要最最大力总伸长率 带E的钢筋不做屈服比
                
                //断前距L0(MM)1
                sItem["DQJL01"] = (GetSafeDouble(sItem["ZJ"]) * 5).ToString();
                #region 抗震要求
                int mkZHggs = 0;
                if (jcxm.Contains("、抗震要求、"))
                {
                    sItem["G_ZSCL"] = "≥" + extraFieldsDj["ZSCL"];

                    if (sItem["GCLX_PH"].ToUpper().EndsWith("E"))
                    {
                        sItem["G_KZYQ"] = "实测强屈比≥" + extraFieldsDj["QDQFB"] + "，实测标准屈服比≤" + extraFieldsDj["QFQFB"] + "，最大力总伸长率≥" + extraFieldsDj["ZSCL"] + "%。";
                        sItem["DQJL01"] = "100";
                    }
                    else
                    {

                        sItem["DQJL01"] = (GetSafeDouble(sItem["ZJ"]) * 5).ToString();
                        sItem["G_KZYQ"] = "最大力总伸长率≥" + extraFieldsDj["ZSCL"] + "%。";
                    }
                    mHggs_SCL = 0;
                    sItem["G_SCL"] = "----";
                    sItem["SCL1"] = "----";
                    sItem["SCL2"] = "----";
                    sItem["SCL3"] = "----";


                    if (MItem[0]["PDBZ"].Contains("1499.2") || MItem[0]["PDBZ"].Contains("1499.1"))
                    {
                        for (int i = 1; i <= mXLGS; i++)
                        {
                            md1 = GetSafeDouble(sItem["DHJL" + i]);
                            md2 = GetSafeDouble(sItem["DQJL01"]);
                            md = md1 - md2;
                            md1 = GetSafeDouble(sItem["DQJL01"]);
                            md = md / md1;
                            md = md + GetSafeDouble(sItem["KLQD" + i]) / 200000;
                            md = md * 100;
                            md = Math.Round(md*2, 0)/2;
                            sItem["ZSCL" + i] = md.ToString("0.0");

                            if (MItem[0]["PDBZ"].Contains("1499.1"))
                            {
                                sItem["QDQFB" + i] = "----";
                                sItem["QFQFB" + i] = "----";
                            }
                            else
                            {
                                sItem["QDQFB" + i] = (Math.Round(Conversion.Val(sItem["KLQD" + i]) / Conversion.Val(sItem["QFQD" + i]), 2)).ToString("0.00");
                                sItem["QFQFB" + i] = (Math.Round(Conversion.Val(sItem["QFQD" + i]) / mQfqd, 2)).ToString("0.00");
                            }
                            if (sItem["GCLX_PH"].ToUpper().EndsWith("E"))
                            {
                                if ((Conversion.Val(sItem["QDQFB" + i]) < Conversion.Val(extraFieldsDj["QDQFB"])))
                                {
                                    jcxmBhg += jcxmBhg.Contains("抗震等级") ? "" : "抗震等级" + "、";
                                    mkZHggs++;
                                }
                                if ((Conversion.Val(sItem["QFQFB" + i]) > Conversion.Val(extraFieldsDj["QFQFB"])))
                                {
                                    jcxmBhg += jcxmBhg.Contains("抗震等级") ? "" : "抗震等级" + "、";
                                    mkZHggs++;

                                }

                                if ((Conversion.Val(sItem["ZSCL" + i]) < Conversion.Val(extraFieldsDj["ZSCL"])))
                                {
                                    jcxmBhg += jcxmBhg.Contains("抗震等级") ? "" : "抗震等级" + "、";
                                    mkZHggs++;
                                }
                            }
                            else
                            {
                                if ((Conversion.Val(sItem["ZSCL" + i]) < Conversion.Val(extraFieldsDj["ZSCL"])))
                                {
                                    jcxmBhg += jcxmBhg.Contains("最大力总伸长率") ? "" : "最大力总伸长率" + "、";
                                    mkZHggs++;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= mXLGS; i++)
                        {
                            sItem["QDQFB" + i] = (Math.Round(Conversion.Val(sItem["KLQD" + i]) / Conversion.Val(sItem["QFQD" + i]), 2)).ToString("0.00");
                            sItem["QFQFB" + i] = (Math.Round(Conversion.Val(sItem["QFQD" + i]) / mQfqd, 2)).ToString("0.00");
                            if ((Conversion.Val(sItem["QDQFB" + i]) >= Conversion.Val(extraFieldsDj["QDQFB"]) || Conversion.Val(extraFieldsDj["QDQFB"]) == 0) && (Conversion.Val(sItem["QFQFB" + i]) <= Conversion.Val(extraFieldsDj["QFQFB"]) || Conversion.Val(extraFieldsDj["QFQFB"]) == 0))
                            {
                            }
                            else
                            {
                                mkZHggs++;
                            }
                        }
                    }

                    if (mkZHggs == 0)
                    {
                        sItem["JCJG_KZ"] = "符合";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["JCJG_KZ"] = "不符合";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                    }
                }
                else
                {
                    sItem["JCJG_KZ"] = "----";
                    sItem["G_KZYQ"] = "----";
                }
                //'开始判定单项指标是否合格,根据单项指标再判定单组结论是否合格
                all_zb_jl(MItem[0], sItem, (int)mHggs_QFQD, (int)mHggs_KLQD, (int)mHggs_SCL, (int)mHggs_LW);


                if (Conversion.Val(sItem["HG_QF"]) < mHggs_QFQD)
                {
                    jcxmBhg += jcxmBhg.Contains("屈服强度") ? "" : "屈服强度" + "、";
                    mFlag_Bhg = true;
                    mbhggs = mbhggs + 1;
                }
                if (Conversion.Val(sItem["HG_KL"]) < mHggs_KLQD)
                {
                    jcxmBhg += jcxmBhg.Contains("抗拉强度") ? "" : "抗拉强度" + "、";
                    mFlag_Bhg = true;
                    mbhggs = mbhggs + 1;
                }
                if (Conversion.Val(sItem["HG_SC"]) < mHggs_SCL)
                {
                    jcxmBhg += jcxmBhg.Contains("断后伸长率") ? "" : "断后伸长率" + "、";
                    mFlag_Bhg = true;
                    mbhggs = mbhggs + 1;
                }
                #endregion

                #region 反向弯曲
                if (jcxm.Contains("、反向弯曲、"))
                {
                    jcxmCur = "反向弯曲";
                    sItem["G_LWWZ"] = "弯曲压头D=" + (mLWZJ + 1) + "a,弯曲90度后，反向弯曲20度,弯曲部位表面无裂纹。";
                    if (sItem["FXWQ1"] == "1")
                    {
                        mFlag_Hg = true;
                        sItem["JCJG_LW"] = "符合";
                    }
                    else
                    {
                        sItem["JCJG_LW"] = "不符合";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                    }
                }
                else
                {
                    sItem["FXWQ1"] = "----";
                }
                #endregion
                
                //宏观金相,截面维氏硬度,微观组织 是2018新增
                #region 宏观金相
                if (jcxm.Contains("、宏观金相、") && MItem[0]["PDBZ"].Contains("1499.2-2018"))
                {
                    jcxmCur = "宏观金相";
                    if (MItem[0]["HG_HGJX"] == "合格" || MItem[0]["HG_HGJX"] == "符合")
                    {
                        MItem[0]["HG_HGJX"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        MItem[0]["HG_HGJX"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                    }
                }
                else
                {
                    MItem[0]["HG_HGJX"] = "----";
                }
                #endregion
                #region 截面维氏硬度
                if (jcxm.Contains("、截面维氏硬度、") && MItem[0]["PDBZ"].Contains("1499.2-2018"))
                {
                    jcxmCur = "截面维氏硬度";

                    double JMWSYD1 = GetSafeDouble(sItem["JMWSYD1"]);
                    double JMWSYD2 = GetSafeDouble(sItem["JMWSYD2"]);

                    if (IsQualified(MItem[0]["G_JMWSYD"], Math.Abs(JMWSYD1 - JMWSYD2).ToString(), true) == "符合")
                    {
                        MItem[0]["HG_JMWSYD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        MItem[0]["HG_JMWSYD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                    }
                }
                #endregion
                #region 微观组织
                if (jcxm.Contains("、微观组织、") && MItem[0]["PDBZ"].Contains("1499.2-2018"))
                {
                    jcxmCur = "微观组织";
                    if (MItem[0]["HG_WGZZ"] == "合格" || MItem[0]["HG_WGZZ"] == "符合")
                    {
                        MItem[0]["HG_WGZZ"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        MItem[0]["HG_WGZZ"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                    }
                }
                #endregion

                //如果只做2根，报告上需要修改
                if (mXLGS == 2)
                {
                    sItem["LW3"] = "----";
                    sItem["KLHZ3"] = "----";
                    sItem["QFHZ3"] = "----";
                    sItem["DLWZ3"] = "----";
                    sItem["SCZ3"] = "----";
                }
                if (sItem["JCJG_KZ"] == "不符合" || sItem["JCJG_LS"] == "不符合")
                {
                    sItem["JCJG_LS"] = "不符合";
                }

                if (sItem["JCJG_LS"] == "不符合" || sItem["JCJG_LW"] == "不符合" || sItem["JCJG_ZLPC"] == "不符合")
                {
                    sItem["JCJG"] = "不合格";
                }
                else
                {
                    sItem["JCJG"] = "合格";
                    if (MItem[0]["FJJJ3"].Contains("1#"))
                    {
                        MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + "1#";
                    }
                }

                if (sItem["JCJG_LW"] == "不符合")
                {
                    jcxmCur = jcxm.Contains("反向弯曲") ? "反向弯曲" : "弯曲";
                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    mFlag_Bhg = true;
                    mbhggs = mbhggs + 1;
                }
                if (sItem["JCJG_ZLPC"] == "不符合")
                {
                    jcxmCur = "重量偏差";
                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    mFlag_Bhg = true;
                    mbhggs = mbhggs + 1;

                }
                if (sItem["JCJG_LS"] == "不符合" && sItem["JCJG_LW"] == "不符合" && sItem["JCJG_ZLPC"] == "不符合")
                {
                    mFlag_Bhg = true;
                    mFlag_Hg = false;
                }
                else if (sItem["JCJG_LS"] == "符合" && sItem["JCJG_LW"] == "符合" && sItem["JCJG_ZLPC"] == "符合")
                {
                    mFlag_Bhg = false;
                    mFlag_Hg = true;
                }
                else
                {
                    mFlag_Hg = true;
                    mFlag_Bhg = true;
                }
                mAllHg = mAllHg && (sItem["JCJG"] == "合格");

                //------------------------单组是否需双倍复检的判定------------------------------------------
                if (sItem["JCJG"] == "不合格")
                {
                    check_double_Fj(MItem[0], sItem, (int)mHggs_QFQD, (int)mHggs_KLQD, (int)mHggs_SCL, (int)mHggs_LW, (int)mFsgs_QFQD, (int)mFsgs_KLQD, (int)mFsgs_SCL, (int)mFsgs_LW);
                }

                //重量偏差
                if (sItem["JCJG_ZLPC"] == "不符合")
                {
                    if (!MItem[0]["FJJJ2"].Contains("1#"))
                    {
                        MItem[0]["FJJJ1"] = MItem[0]["FJJJ1"] + "1#";
                    }
                }
            }

            #region 添加最终报告

            if (mFlag_Bhg && mFlag_Hg)
            {
                jsbeiZHu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合" + ggph + "要求。";
            }
            else
            {
                jsbeiZHu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + ggph + "要求。";
            }

            if (mAllHg && MJcjg != "----")
            {
                MJcjg = "合格";
                jsbeiZHu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合" + ggph + "要求。";
            }


            if (MItem[0]["FJJJ3"] != "")
            {
                jsbeiZHu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合" + ggph + "要求。";

                MItem[0]["FJJJ3"] = jsbeiZHu;
            }
            if (MItem[0]["FJJJ2"] != "")
            {
                //if (mFlag_Bhg && mFlag_Hg)
                //{
                jsbeiZHu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + ggph + "要求。";
                MItem[0]["FJJJ2"] = jsbeiZHu;
                //}
                //else
                //{
                //    MItem[0]["FJJJ2"] = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                //    jsbeiZHu = "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                //}
            }
            if (MItem[0]["FJJJ1"] != "")
            {
                if (jcxmBhg.TrimEnd('、') == "抗震等级")
                {
                    jsbeiZHu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + ggph + "要求。";
                }
                else
                {
                    jsbeiZHu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + ggph + "要求，另取双倍样复试。";
                }
                MItem[0]["FJJJ1"] = jsbeiZHu;
            }

            MItem[0]["JCJG"] = MJcjg;
            if (mJCJG == "不下结论")
            {
                MItem[0]["JCJG"] = mJCJG;
                jsbeiZHu = "";
            }
            MItem[0]["JCJGMS"] = jsbeiZHu;
            //MItem[0]["MSGINFO"] = "合同号：" + MItem[0]["HTBH"] + "，委托编号：" + MItem[0]["WTDBH"] + "的钢筋原材料" + jsbeiZHu;
            #endregion

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
