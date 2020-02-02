using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GYF : BaseMethods
    {
        public void Calc()
        {
            #region 计算代码
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "";



            var data = retData;
            List<double> mtmpArray = new List<double>();
            var extraDJ = dataExtra["BZ_GYF_DJ"];
            var ZlpcData = dataExtra["BZ_ZLPCB"];

            double mQfqd = 0, mKlqd = 0, mLw = 0;
            double sLlzl = 0, sZlpc = 0;
            double mHggs_qfqd = 0, mHggs_klqd = 0, mHggs_scl = 0, mHggs_lw = 0, mlwzj = 0, mLwjd = 0, mxlgs = 0, mxwgs = 0, mffwqcs = 0, mwhich = 0;
            string mJSFF = "", LwBzyq = "";
            int mbhggs = 0;

            var SItem = data["S_GYF"];
            var MItem = data["M_GYF"];
            if (!data.ContainsKey("M_GYF"))
            {
                data["M_GYF"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            //保留有效数字
            Func<double, int, string> mYxsz = delegate (double t_numeric, int rndto)
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
               if (Math.Abs(double.Parse(sItem["CD"]) - 0) > 0.00001)
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

            Func<IDictionary<string, string>, int, string> calc_qf = delegate (IDictionary<string, string> sItem, int count)
                 {
                     //求屈服强度及抗拉强度
                     double mMidVal = 0;
                     string mMj = "", mqf = "";

                     if (string.IsNullOrEmpty(sItem["ZJ"]))
                     {
                         sItem["ZJ"] = "0";
                     }
                     mMidVal = (double.Parse(sItem["ZJ"]) / 2) * (double.Parse(sItem["ZJ"]) / 2);
                     mMj = (3.14159 * mMidVal).ToString();
                     if (sItem["SJDJ"].Contains("冷轧带肋"))
                     {
                         mMj = (Math.Round(Conversion.Val(mMj), 1)).ToString("0.0");
                     }
                     else
                     {
                         mMj = mYxsz(double.Parse(mMj), 4);
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

            Func<IDictionary<string, string>, int, string> calc_kl = delegate (IDictionary<string, string> sItem, int count)
                 {
                    //求屈服强度及抗拉强度
                    double mMidVal = 0;
                     string mMj = "", mkl = "";
                     if (string.IsNullOrEmpty(sItem["ZJ"]))
                     {
                         sItem["ZJ"] = "0";
                     }
                     mMidVal = (double.Parse(sItem["ZJ"]) / 2) * (double.Parse(sItem["ZJ"]) / 2);
                     mMj = (3.14159 * mMidVal).ToString();

                     if (sItem["SJDJ"].Contains("冷轧带肋"))
                     {
                         mMj = (Math.Round(Conversion.Val(mMj), 1)).ToString("0.0");
                     }
                     else
                     {
                         mMj = mYxsz(double.Parse(mMj), 4);
                     }

                     if (sItem["SJDJ"].Contains("冷轧扭"))
                     {
                         switch (sItem["SJDJ"].Trim())
                         {
                             case "冷轧扭CTB550Ⅰ":
                                 switch (sItem["ZJ"].Trim())
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
                                 switch (sItem["ZJ"].Trim())
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
                                 switch (sItem["ZJ"].Trim())
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
                                 switch (sItem["ZJ"].Trim())
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
                                var LW = Conversion.Val(sItem["LW" + i]);
                                if (LW - mbzValue > -0.00001)
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

                    if (jcxm.Contains("、冷弯、"))
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
                        sItem["LW1"] = "-1";
                        sItem["LW2"] = "-1";
                        sItem["LW3"] = "-1";
                        sItem["LW4"] = "-1";
                    }

                    return "";
                };

            //check_double_Fj(mItem, sItem, mHggs_QFQD, mHggs_KLQD, mHggs_SCL, mHggs_LW, mFsgs_QFQD, mFsgs_KLQD, mFsgs_SCL, mFsgs_LW, mZh);

            Func<IDictionary<string, string>, IDictionary<string, string>, int, int, int, int, int, int, int, int, int, string> check_double_Fj =
                delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, int mHggs_QFQD, int mHggs_KLQD, int mHggs_SCL, int mHggs_LW, int mFsgs_QFQD, int mFsgs_KLQD, int mFsgs_SCL, int mFsgs_LW, int mZh)
                {
                    if (sItem["JCJG_LS"].Trim() == "不符合" && sItem["JCJG_LW"].Trim() == "不符合" || sItem["JCJG_ZLPC"].Trim() == "不符合")
                    {
                        sItem["JCJG"] = "不合格";
                        mItem["FJJJ2"] = mItem["FJJJ2"] + mZh + "#";
                    }
                    else
                    {
                        if (sItem["JCJG_LS"] == "不符合" || sItem["JCJG_LW"] == "不符合")
                        {
                            sItem["JCJG"] = "复试";
                            mItem["FJJJ1"] = mItem["FJJJ1"] + mZh + "#";
                        }
                    }
                    return "";
                };

            foreach (var sItem in SItem)
            {
                #region 数据准备工作
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //获取钢筋类别
                if (string.IsNullOrEmpty(sItem["GJLB"]))
                {
                    sItem["GJLB"] = "";
                }
                double mScl = 0;
                //以下代码有问题
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["PH"].Trim() == sItem["GCLX_PH"].Trim() && u["GJLB"].Trim() == sItem["GJLB"].Trim() && GetSafeDouble(u["ZJFW1"].Trim()) < GetSafeDouble(sItem["ZJ"].Trim()) && GetSafeDouble(u["ZJFW2"].Trim()) > GetSafeDouble(sItem["ZJ"].Trim()));
                if (null != extraFieldsDj)
                {
                    sItem["SJDJ"] = extraFieldsDj["MC"];
                    mQfqd = GetSafeDouble(extraFieldsDj["QFQDBZZ"]); //单组标准值
                    mKlqd = GetSafeDouble(extraFieldsDj["KLQDBZZ"]);
                    mScl = GetSafeDouble(extraFieldsDj["SCLBZZ"]);
                    mLw = GetSafeDouble(extraFieldsDj["LWBZZ"]);
                    mHggs_qfqd = GetSafeDouble(extraFieldsDj["ZHGGS_QFQD"]); //单组合格个数
                    mHggs_klqd = GetSafeDouble(extraFieldsDj["ZHGGS_KLQD"]);
                    mHggs_scl = GetSafeDouble(extraFieldsDj["ZHGGS_SCL"]);
                    mHggs_lw = GetSafeDouble(extraFieldsDj["ZHGGS_LW"]);
                    //MItem[0]["WHICH"] = extraFieldsDj["WHICH"];

                    mlwzj = GetSafeDouble(extraFieldsDj["LWZJ"]); //冷弯直径和角度
                    mLwjd = GetSafeDouble(extraFieldsDj["LWJD"]);
                    mxlgs = GetSafeDouble(extraFieldsDj["XLGS"]);
                    mxwgs = GetSafeDouble(extraFieldsDj["XWGS"]);
                    mffwqcs = GetSafeDouble(extraFieldsDj["FFWQCS"]);
                    mwhich = GetSafeDouble(extraFieldsDj["WHICH"].Trim());
                    mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"];

                    if (sItem["SFTZ"] == "是")
                    {
                        mScl = GetSafeDouble(extraFieldsDj["TZHSCLBZZ"]);
                    }

                }
                else
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                var extraFieldsZlpc = ZlpcData.FirstOrDefault(u => u["ZJ"].Trim() == sItem["ZJ"].Trim());
                if (extraFieldsZlpc != null)
                {
                    sLlzl = GetSafeDouble(extraFieldsZlpc["LLZL"]);
                    sZlpc = GetSafeDouble(extraFieldsZlpc["ZLPC"]);
                    sItem["G_ZLPC"] = sZlpc.ToString();
                }
                else
                {
                    sLlzl = 0;
                    sZlpc = 0;
                }

                //if (string.IsNullOrEmpty(sItem["SYHJWD"]))
                //{
                //    MItem[0]["SYWD"] = sItem["SYHJWD"] + "℃";
                //}

                #endregion

                #region 计算开始
                if (mlwzj == 0 && mffwqcs != 0)
                {
                    LwBzyq = "弯曲次数不小于" + mffwqcs.ToString() + "次，受弯曲部位表面无裂纹";
                }
                else
                {
                    LwBzyq = MItem[0]["JCYJ"].Trim().ToString().Contains("1999") ? "弯心直径d" : "弯曲压头直径D";

                    if (mlwzj < 1)
                    {
                        LwBzyq = LwBzyq + "=0" + mlwzj.ToString() + "a弯曲" + mLwjd.ToString() + "度后，受弯曲部位表面无裂纹";
                    }
                    else
                    {
                        LwBzyq = LwBzyq + "=" + mlwzj.ToString() + "a弯曲" + mLwjd.ToString() + "度后，受弯曲部位表面无裂纹";
                    }
                }

                if (extraFieldsDj["QFQDBZZ"] == "----")
                {
                    sItem["G_QFQD"] = "----";
                }
                else
                {
                    sItem["G_QFQD"] = "≥" + extraFieldsDj["QFQDBZZ"].Trim();
                }

                if (extraFieldsDj["KLQDBZZ"] == "----")
                {
                    sItem["G_KLQD"] = "----";
                }
                else
                {
                    sItem["G_KLQD"] = "≥" + extraFieldsDj["KLQDBZZ"].Trim();
                }

                if (mScl == 0)
                {
                    sItem["G_SCL"] = "----";
                }
                else
                {
                    sItem["G_SCL"] = "≥" + extraFieldsDj["SCLBZZ"].Trim();
                    if (sItem["SFTZ"] == "是")
                        sItem["G_SCL"] = "≥" + extraFieldsDj["TZHSCLBZZ"].Trim();
                }
                sItem["G_LWWZ"] = LwBzyq;
                sItem["G_ZSCL"] = "----";

                //求伸长率
                sItem["XGM"] = extraFieldsDj["XGM"];
                sItem["CD"] = (Math.Round((GetSafeDouble(sItem["XGM"]) * GetSafeDouble(sItem["ZJ"])) / 5 + 0.001, 0) * 5).ToString();

                if (GetSafeDouble(sItem["XGM"]) == 100)
                {
                    sItem["CD"] = "100";
                }

                #region 重量偏差
                if (jcxm.Contains("、重量偏差、"))
                {
                    Double zcd = 0;
                    zcd = GetSafeDouble(sItem["Z_CD1"]) + GetSafeDouble(sItem["Z_CD2"]) + GetSafeDouble(sItem["Z_CD3"]) + GetSafeDouble(sItem["Z_CD4"]) + GetSafeDouble(sItem["Z_CD5"]) + GetSafeDouble(sItem["Z_CD6"]) + GetSafeDouble(sItem["Z_CD7"]) + GetSafeDouble(sItem["Z_CD8"]) + GetSafeDouble(sItem["Z_CD9"]) + GetSafeDouble(sItem["Z_CD10"]);
                    sItem["ZLPC"] = Math.Round(100 * (GetSafeDouble(sItem["Z_ZZL"]) - sLlzl * zcd) / (sLlzl * zcd), 0).ToString();
                    if (GetSafeDouble(sItem["ZLPC"]) <= sZlpc)
                    {
                        sItem["JCJG_ZLPC"] = "符合";
                    }
                    else
                    {
                        sItem["JCJG_ZLPC"] = " 不符合";
                    }
                }
                else
                {
                    sItem["JCJG_ZLPC"] = "----";
                }
                #endregion

                //求伸长率
                calc_SCL(MItem[0], sItem, (int)mxlgs);

                //求屈服强度及抗拉强度
                calc_qf(sItem, (int)mxlgs);

                calc_kl(sItem, (int)mxlgs);

                //求单组屈服强度,抗拉强度,伸长率,冷弯 合格个数,并且返回值为不同组不合格数的累加值
                int mallBHG_QF = 0, mallBHG_KL = 0, mallBHG_SC = 0, mallBHG_LW = 0;
                mallBHG_QF = mallBHG_QF + find_singlezb_bhg(MItem[0], sItem, "qf", mQfqd, (int)mxlgs);
                mallBHG_KL = mallBHG_KL + find_singlezb_bhg(MItem[0], sItem, "kl", mKlqd, (int)mxlgs);
                mallBHG_SC = mallBHG_SC + find_singlezb_bhg(MItem[0], sItem, "SCL", mScl, (int)mxlgs);
                if (jcxm.Contains("、冷弯、"))
                {
                    mallBHG_LW = mallBHG_LW + find_singlezb_bhg(MItem[0], sItem, "LW", mLw, (int)mxlgs);
                }
                #region 抗震要求
                if (jcxm.Contains("、抗震要求、"))
                {
                    int mkzhggs = 0;
                    sItem["G_ZSCL"] = "≥" + extraFieldsDj["ZSCL"].Trim();
                    sItem["G_KZYQ"] = "实测强屈比≥" + extraFieldsDj["QDQFB"].Trim() + "，实测标准屈服比≤" + extraFieldsDj["QFQFB"].Trim() + "，最大力总伸长率≥" + extraFieldsDj["ZSCL"].Trim() + "%。";
                    mHggs_scl = 0;
                    sItem["G_SCL"] = "----";
                    sItem["SCL1"] = "----";
                    sItem["SCL2"] = "----";
                    sItem["SCL3"] = "----";
                    sItem["SCL4"] = "----";
                    if (MItem[0]["PDBZ"].Contains("1499.2") || MItem[0]["PDBZ"].Contains("1499.1"))
                    {
                        for (int i = 1; i <= mxlgs; i++)
                        {
                            double md1 = 0, md2 = 0, md = 0;
                            md1 = GetSafeDouble(sItem["DHJL" + i]);
                            md2 = GetSafeDouble(sItem["DQJL0" + i]);
                            md = md1 - md2;
                            md1 = GetSafeDouble(sItem["DQJL0" + i]);
                            md = md / md1;
                            md = md + GetSafeDouble(sItem["KLQD" + i]) / 200000;
                            md = md * 100;
                            md = Math.Round(md, 1);
                            sItem["ZSCL" + i] = md.ToString("0.0");

                            if (MItem[0]["PDBZ"].ToString().Contains("1499.1-2008"))
                            {
                                sItem["QDQFB" + i] = "----";
                                sItem["QFQFB" + i] = "----";
                            }
                            else
                            {
                                sItem["QDQFB" + i] = Math.Round(GetSafeDouble(sItem["KLQD" + i]) / GetSafeDouble(sItem["QFQD" + i]), 2).ToString("0.00");
                                sItem["QFQFB" + i] = Math.Round(GetSafeDouble(sItem["QFQD" + i]) / mQfqd, 2).ToString("0.00");
                            }

                            if ((GetSafeDouble(sItem["QDQFB" + i]) >= GetSafeDouble(extraFieldsDj["QDQFB"]) || GetSafeDouble(extraFieldsDj["QDQFB"]) == 0) && (GetSafeDouble(sItem["QFQFB" + i]) <= GetSafeDouble(extraFieldsDj["QFQFB"]) || GetSafeDouble(extraFieldsDj["QFQFB"]) == 0) && (GetSafeDouble(sItem["ZSCL" + i]) >= GetSafeDouble(extraFieldsDj["ZSCL"]) || GetSafeDouble(extraFieldsDj["ZSCL"]) == 0))
                            {
                                mkzhggs = mkzhggs + 1;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= mxlgs; i++)
                        {
                            sItem["QDQFB" + i] = Math.Round(GetSafeDouble(sItem["KLQD" + i]) / GetSafeDouble(sItem["QFQD" + i]), 2).ToString();
                            sItem["QFQFB" + i] = Math.Round(GetSafeDouble(sItem["QFQD" + i]) / mQfqd, 2).ToString();
                            //If(CDec(Val(mrssubTable.Fields("qdqfb" & i))) >= CDec(Val(mrsDj!qdqfb)) Or CDec(Val(mrsDj!qdqfb)) = 0) And(CDec(Val(mrssubTable.Fields("qfqfb" & i))) <= CDec(Val(mrsDj!qfqfb)) Or CDec(Val(mrsDj!qfqfb)) = 0) Then
                            if ((GetSafeDouble(sItem["QDQFB" + i]) >= GetSafeDouble(extraFieldsDj["QDQFB"]) || GetSafeDouble(extraFieldsDj["QDQFB"]) == 0) && (GetSafeDouble(sItem["QFQFB" + i]) <= GetSafeDouble(extraFieldsDj["QFQFB"]) || GetSafeDouble(extraFieldsDj["QFQFB"]) == 0))
                            {
                                mkzhggs = mkzhggs + 1;
                            }
                        }
                    }

                    if (mkzhggs == mxlgs)
                    {
                        sItem["JCJG_KZ"] = "符合";
                    }
                    else
                    {
                        sItem["JCJG_KZ"] = "不符合";
                    }
                }
                else
                {
                    sItem["jcjg_kz"] = "----";
                    sItem["g_kzyq"] = "----";
                }
                #endregion

                //'开始判定单项指标是否合格,根据单项指标再判定单组结论是否合格
                all_zb_jl(MItem[0], sItem, (int)mHggs_qfqd, (int)mHggs_klqd, (int)mHggs_scl, (int)mHggs_lw);
                #region 反向弯曲
                if (jcxm.Contains("、反向弯曲、"))
                {
                    //MItem[0]["WHICH"] = "6";
                    sItem["G_LWWZ"] = "弯心直径d=" + (mlwzj + 1).ToString() + "a弯曲90°后，反向弯曲20° 弯曲部位表面无裂纹。";
                    if (sItem["FXWQ1"] == "1" && sItem["FXWQ2"] == "1")
                    {
                        sItem["JCJG_LW"] = "符合";
                    }
                    else
                    {
                        sItem["JCJG_LW"] = "不符合";
                    }
                }
                #endregion


                if (sItem["JCJG_KZ"] == "不符合" || sItem["JCJG_LS"] == "不符合")
                {
                    sItem["JCJG_LS"] = "不符合";
                }

                if (sItem["JCJG_LS"] == "不符合" || sItem["JCJG_LW"] == "不符合" || sItem["JCJG_ZLPC"] == "不符合")
                {
                    sItem["JCJG"] = "不合格";
                    mbhggs++;
                }
                else
                {
                    sItem["JCJG"] = "合格";
                    MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + sItem["ZH_G"] + "#";
                }
                #endregion
                mAllHg = (mAllHg && (sItem["JCJG"] == "合格"));
                if (mAllHg)
                {
                    jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }


            MItem[0]["JCJG"] = mjcjg;

            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/

            #endregion
        }
    }
}
