using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class XHJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_XHJ_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_XHJ"];

            if (!data.ContainsKey("M_XHJ"))
            {
                data["M_XHJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_XHJ"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var mAllHg = false;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;
            var jcxm = "";
            #region 局部函数

            //返回值为每组每种指标不合格总数  ' mbzValue 是单前判断指标的标准值, count 是一组中的检测个数
            Func<IDictionary<string, string>, IDictionary<string, string>, string, double, int, int> find_singlezb_bhg =
                delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, string zbName, double mbzValue, int count)
                {
                    int mcnt = 0;//计算单组合格个数
                                 //int mCurBhg_qf;//计算单组不合格个数
                    int this_bhg = 0;//当前组单个指标不合格累加

                    switch (zbName)
                    {
                        case "kl":
                            for (int i = 1; i < count + 1; i++)
                            {
                                //If isZdVisible("s" & gCurSylb, "qfhz" & i) Then '判断该指标是否存在
                                if (Double.Parse(sItem["KLQD" + i]) - mbzValue > -0.00001)
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
                       
                    }

                    return this_bhg;
                };

   
            ///求屈服强度及抗拉强度
            Func<IDictionary<string, string>, int, int> calc_kl = delegate (IDictionary<string, string> sItem, int count)
            {
                double mMidVal = 0;
                double mMj = 0;
                double mkl = 0;
                double zj = 0;
                if (string.IsNullOrEmpty(sItem["ZJ"]))
                {
                    sItem["ZJ"] = "0";
                }
                zj = Convert.ToDouble(sItem["ZJ"]);
                mMidVal = zj / 2 * zj / 2;
                mMj = 3.14159 * mMidVal;

                if (sItem["SJDJ"].Contains("冷轧带肋"))
                {
                    mMj = Double.Parse(mMj.ToString("0.0"));
                }
                else
                {
                    mMj = Double.Parse(mMj.ToString("G4"));
                }

                if (sItem["SJDJ"].Contains("冷轧扭"))
                {
                    switch (sItem["SJDJ"])
                    {
                        case "冷轧扭CTB550Ⅰ":
                            if (zj == 6.5)
                            {
                                mMj = 29.50;
                            }
                            else if (zj == 8)
                            {
                                mMj = 45.30;
                            }
                            else if (zj == 10)
                            {
                                mMj = 68.30;
                            }
                            else if (zj == 12)
                            {
                                mMj = 96.14;
                            }
                            break;
                        case "冷轧扭CTB550Ⅱ":

                            if (zj == 6.5)
                            {
                                mMj = 29.20;
                            }
                            else if (zj == 8)
                            {
                                mMj = 42.30;
                            }
                            else if (zj == 10)
                            {
                                mMj = 66.10;
                            }
                            else if (zj == 12)
                            {
                                mMj = 92.74;
                            }
                            break;

                        case "冷轧扭CTB550Ⅲ":

                            if (zj == 6.5)
                            {
                                mMj = 29.86;
                            }
                            else if (zj == 8)
                            {
                                mMj = 45.24;
                            }
                            else if (zj == 10)
                            {
                                mMj = 70.69;
                            }
                            break;
                        case "冷轧扭CTB650Ⅲ":
                            if (zj == 6.5)
                            {
                                mMj = 28.20;
                            }
                            else if (zj == 8)
                            {
                                mMj = 42.73;
                            }
                            else if (zj == 10)
                            {
                                mMj = 66.76;
                            }
                            break;
                    }

                }
                sItem["MJ"] = mMj.ToString();

                if (Math.Abs(mMj) - 0 > 0.00001)
                {
                    for (int i = 1; i < count + 1; i++)
                    {
                        if (string.IsNullOrEmpty(sItem["KLHZ" + i]))
                        {
                            sItem["KLHZ" + i] = "0";
                        }
                        mkl = 1000 * Conversion.Val(sItem["KLHZ" + i]) / mMj;

                        if (mkl <= 200)
                        {
                            sItem["KLQD" + i] = mkl.ToString("0");
                        }
                        if (mkl > 200 && mkl <= 1000)
                        {
                            sItem["KLQD" + i] = (Math.Round(mkl / 5, 0) * 5).ToString();
                        }
                        if (mkl > 1000)
                        {
                            sItem["KLQD" + i] = (Math.Round(mkl / 10, 0) * 10).ToString();
                        }
                    }

                }
                else
                {
                    for (int i = 1; i < count + 1; i++)
                    {
                        sItem["KLQD" + i] = "0";
                    }
                }
                return 0;
            };

            //开始判定单项指标是否合格,根据单项指标再判定单组结论是否合格
            Func<IDictionary<string, string>, IDictionary<string, string>, double, double, double, bool> all_hj_zb_jl =
                delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, double mHggs_klqd2, double mHggs_scl2, double mHggs_lw2)
                {
                    var hg_kl = sItem["HG_KL"];
                    if (string.IsNullOrEmpty(hg_kl))
                    {
                        sItem["HG_KL"] = "0";
                    }

                    var hg_sc = sItem["HG_SC"];
                    if (string.IsNullOrEmpty(hg_sc))
                    {
                        sItem["HG_SC"] = "0";
                    }
                    var hg_lw = sItem["HG_LW"];
                    if (string.IsNullOrEmpty(hg_lw))
                    {
                        sItem["HG_LW"] = "0";
                    }
                    var jcxm2 = "、" + sItem["JCXM"] + "、";

                    if (jcxm2.Contains("、拉伸、"))
                    {
                        if (Convert.ToDouble(hg_kl) >= mHggs_klqd2 && Convert.ToDouble(hg_sc) >= mHggs_scl2)
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
                        sItem["DKJ1"] = "-1";
                        sItem["DKJ2"] = "-1";
                        sItem["DKJ3"] = "-1";
                    }


                    if (jcxm2.Contains("、冷弯、") || jcxm2.Contains("、弯曲、"))
                    {
                        if (Convert.ToDouble(hg_lw) > mHggs_lw2)
                        {
                            sItem["JCJG_LW"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LW"] = "不符合";
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["JCJG_LW"] = "----";
                        sItem["LW1"] = "-1";
                        sItem["LW2"] = "-1";
                        sItem["LW3"] = "-1";
                    }
                    if (sItem["JCJG_LS"] == "不符合" || sItem["JCJG_LW"] == "不符合")
                    {
                        sItem["JCJG"] = "不合格";
                        mFlag_Bhg = true;
                        return false;
                    }
                    else
                    {
                        sItem["JCJG"] = "合格";
                        mFlag_Hg = true;
                        return true;

                    }

                };

            //检查是否双倍复检有冷弯
            Func<IDictionary<string, string>, IDictionary<string, string>, double, double, double,
                 double, double, double, double, bool> check_hj_double_Fj = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, double mHggs_klqd2, double mHggs_scl2, double mHggs_lw2,
              double mFsgs_klqd2, double mFsgs_scl2, double mFsgs_lw2, double mZh2)
                 {
                     var jcxm2 = "、" + sItem["JCXM"] + "、";

                     if (jcxm2.Contains("、拉伸、") && (Convert.ToDouble(sItem["HG_KL"]) < mFsgs_klqd2 || Convert.ToDouble(sItem["HG_SC"]) < mFsgs_scl2) ||
                     ((jcxm2.Contains("、冷弯、") || jcxm2.Contains("、弯曲、")) && (Convert.ToDouble(sItem["hg_lw"]) < mFsgs_lw2)))
                     {
                         sItem["JCJG"] = "不合格";
                         mFlag_Bhg = true;
                         mItem["FJJJ2"] = mItem["FJJJ2"] + mZh2.ToString() + "#";
                     }
                     else
                     {
                         sItem["JCJG"] = "复试";
                         mFlag_Bhg = true;
                         mItem["FJJJ1"] = mItem["FJJJ1"] + mZh2.ToString() + "#";
                     }
                     return true;

                 };
            #endregion

            int mallbhg_kl = 0;
            int mallbhg_sc = 0;
            int mallbhg_lw = 0;

            MItem[0]["FJJJ1"] = "";
            MItem[0]["FJJJ2"] = "";
            MItem[0]["FJJJ3"] = "";

            string mwxzh = "";
            double mZh, mKlqd, mScl, mLw, mLwjd, mLwzj = 0;
            double mHggs_klqd, mHggs_scl, mHggs_lw, mXlgs, mXwgs = 0;
            double mFsgs_klqd, mFsgs_scl, mFsgs_lw = 0;
            string mGjlb = "";
            String LwBzyq = "";
            String SclBzyq = "";

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                sItem["FJ"] = false.ToString();
                mZh = Double.Parse(sItem["ZH_G"]);

                mGjlb = sItem["GJLB"];

                if (string.IsNullOrEmpty(mGjlb))
                {
                    mGjlb = "";
                }
                #region 从设计等级表中取得相应的计算数值、等级标准
                var mrsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["GJLB"] == mGjlb.Trim());

                if (null == mrsDj)
                {
                    jsbeizhu = "依据不详\r\n";
                    mFlag_Bhg = true;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                else
                {
                    sItem["SJDJ"] = mrsDj["MC"];
                    mKlqd = Double.Parse(mrsDj["KLQDBZZ"]);//单组标准值
                    mScl = Double.Parse(mrsDj["SCLBZZ"]);
                    mLw = Double.Parse(mrsDj["LWBZZ"]);
                    mLwjd = Double.Parse(mrsDj["LWJD"]);//冷弯角度和冷弯直径
                    mLwzj = Double.Parse(mrsDj["LWZJ"]);
                    mHggs_klqd = Double.Parse(mrsDj["ZHGGS_KLQD"]);//单组合格个数
                    mHggs_lw = Double.Parse(mrsDj["ZHGGS_LW"]);
                    mFsgs_klqd = Double.Parse(mrsDj["ZFSGS_KLQD"]);
                    mFsgs_lw = Double.Parse(mrsDj["ZFSGS_LW"]);
                    mLwzj = Double.Parse(mrsDj["LWZJ"]);//冷弯直径和角度
                    mLwjd = Double.Parse(mrsDj["LWJD"]);
                    mXlgs = Double.Parse(mrsDj["XLGS"]);
                    mXwgs = Double.Parse(mrsDj["XWGS"]);
                }
                #endregion

                if (MItem[0]["PDBZ"].Contains("18-2012"))
                {
                    SclBzyq = "3个试件均断于母材，呈延性断裂或其中一个试件断于焊缝，呈脆性断裂，其抗拉强度大于或等于钢筋母材抗拉强度标准值。";
                }
                LwBzyq = "弯心直径=" + mLwzj + "d弯曲" + mLwjd + "度，有两个或三个试件外侧裂纹宽度＜0.5mm";

                sItem["G_KLQD"] = mKlqd.ToString();
                //sItem["G_DLWZ"] = SclBzyq.ToString();
                sItem["G_LWWZ"] = LwBzyq.ToString();

                //求抗拉强度
                int count = (int)(mXlgs);
                calc_kl(sItem, count);

                mallbhg_lw = mallbhg_lw + find_singlezb_bhg(MItem[0], sItem, "lw", mLw, (int)(mXwgs));

                if (!MItem[0]["PDBZ"].Contains("18-2012"))
                {
                    mallbhg_kl = mallbhg_kl + find_singlezb_bhg(MItem[0], sItem, "kl", mKlqd, count);

                    //if (!all_hj_zb_jl(MItem[0], sItem, mHggs_klqd, mHggs_scl, mHggs_lw))
                    {
                        mFlag_Bhg = true;
                    }
                }

                mjcjg = sItem["JCJG"];
                #region//开始判定单项指标是否合格,根据单项指标再判定单组结论是否合格
                if (sItem["JCJG"] == "合格")
                {
                    MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + mZh.ToString().Trim() + "#";
             
                }
                if (sItem["JCJG"] == "不合格")
                {
                    jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";

                    if (string.Equals(MItem[0]["PDBZ"], "18-2012"))
                    {
                        if (sItem["JCJG_LS"] == "不符合" || sItem["JCJG_LW"] == "不符合")
                        {
                            sItem["JCJG"] = "不合格";

                            MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + mZh.ToString().Trim() + "#";
                        }
                        else
                        {
                            if (sItem["JCJG_LS"] == "无效")
                            {
                                mwxzh = mwxzh + mZh.ToString().Trim() + "#";
                            }
                            else
                            {
                                if (sItem["JCJG_LS"] == "复试" || sItem["JCJG_LW"] == "复试")
                                {
                                    sItem["JCJG"] = "不合格";
                                    MItem[0]["FJJJ1"] = MItem[0]["FJJJ1"] + mZh.ToString().Trim() + "#";
                                }
                            }
                        }
                    }
                    else
                    {
                        //check_hj_double_Fj(MItem[0], sItem, mHggs_klqd, mHggs_scl, mHggs_lw, mFsgs_klqd, mFsgs_scl, mFsgs_lw, mZh);
                    }
                }

                mAllHg = mAllHg && (sItem["JCJG"] == "合格");
                #endregion
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"]))
            {
                MItem[0]["FJJJ3"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";

                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"]))
            {
                MItem[0]["FJJJ2"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                if (mFlag_Hg && mFlag_Bhg)
                {
                    jsbeizhu = "该组试样部分符合" + MItem[0]["PDBZ"] + "标准要求，另取双倍样复试。";
                    MItem[0]["FJJJ2"] = "该组试样部分符合" + MItem[0]["PDBZ"] + "标准要求，另取双倍样复试。";
                }
            }

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ1"]))
            {
                MItem[0]["FJJJ1"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求，另取双倍样复试。";
                if (mFlag_Hg && mFlag_Bhg)
                {
                    jsbeizhu = "该组试样部分符合" + MItem[0]["PDBZ"] + "标准要求，另取双倍样复试。";
                    MItem[0]["FJJJ1"] = "该组试样部分符合" + MItem[0]["PDBZ"] + "标准要求，另取双倍样复试。";
                }
            }

            if (!string.IsNullOrEmpty(mwxzh))
            {
                mwxzh = "该组试样无效，应检验母材。";
                jsbeizhu = "该组试样无效，应检验母材。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
