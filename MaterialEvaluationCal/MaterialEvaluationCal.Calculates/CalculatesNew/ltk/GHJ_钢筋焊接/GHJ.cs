using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GHJ2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_GHJ_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_GHJ"];

            if (!data.ContainsKey("M_GHJ"))
            {
                data["M_GHJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GHJ"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var mAllHg = true;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            var ggph = "";//钢筋牌号

            bool gIs2HforRyyq_cd = true;//   '标注2个试件在焊缝或热影响区脆断
            bool gIs3HforRyyq_cd = true;// '标注2个试件在焊缝或热影响区脆断
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
                        //case "qf":
                        //    for (int i = 0; i < count; i++)
                        //    {
                        //        //If isZdVisible("s" & gCurSylb, "qfhz" & i) Then '判断该指标是否存在
                        //        if (GetSafeDouble(sItem["QFQD" + i + 1]) - mbzValue > -0.00001)
                        //        {
                        //            mcnt = mcnt + 1;
                        //        }
                        //        else
                        //        {
                        //            this_bhg = this_bhg + 1;
                        //        }

                        //    }
                        //    sItem["HG_QF"] = mcnt.ToString();
                        //    break;
                        case "kl":
                            for (int i = 1; i < count + 1; i++)
                            {
                                //If isZdVisible("s" & gCurSylb, "qfhz" & i) Then '判断该指标是否存在
                                if (GetSafeDouble(sItem["KLQD" + i]) - mbzValue > -0.00001)
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
                        //case "scl":
                        //    for (int i = 0; i < count; i++)
                        //    {
                        //        //If isZdVisible("s" & gCurSylb, "qfhz" & i) Then '判断该指标是否存在
                        //        if (GetSafeDouble(sItem["SCL" + i+1]) - mbzValue > -0.00001)
                        //        {
                        //            mcnt = mcnt + 1;
                        //        }
                        //        else
                        //        {
                        //            this_bhg = this_bhg + 1;
                        //        }

                        //    }
                        //    sItem["HG_KL"] = mcnt.ToString();
                        //    break;
                        case "lw":
                            for (int i = 1; i < count + 1; i++)
                            {
                                //If isZdVisible("s" & gCurSylb, "qfhz" & i) Then '判断该指标是否存在
                                if (GetSafeDouble(sItem["LW" + i]) - mbzValue > -0.00001)
                                {
                                    mcnt = mcnt + 1;
                                }
                                else
                                {
                                    if (i > 1)
                                    {
                                        if (GetSafeDouble(sItem["LW1"]) - i * mbzValue < -0.00001)// 判断是否把冷弯值全部输在第一个值上
                                        {
                                            this_bhg = this_bhg + 1;
                                        }
                                        else
                                        {
                                            mcnt = mcnt + 1;
                                        }
                                    }
                                    this_bhg = this_bhg + 1;
                                }

                            }
                            sItem["HG_LW"] = mcnt.ToString();
                            break;
                    }

                    return this_bhg;
                };

            Func<IDictionary<string, string>, IDictionary<string, string>, double, int, int> find_dkj_bhg =
              delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, double dkj_mScl, int count)
              {
                  double dkj = 0;//断口值
                  int mcnt = 0;//计算单组合格个数
                  int this_bhg = 0;//计算不和格个数
                  int ryxq_and_cd_scl_cnt = 0;//既断于热影响区又脆断计数器
                  int hf_and_cd_scl_cnt = 0;//既断于焊缝又脆断计数器

                  gIs2HforRyyq_cd = false;
                  gIs3HforRyyq_cd = false;

                  for (int i = 1; i < count + 1; i++)
                  {
                      //if isZdVisible("s" & gCurSylb, "dkj" & i) Then 判断是否存在
                      if (sItem["DKJ" + i] == "5")
                      {
                          dkj = 2;
                      }
                      else if (sItem["DKJ" + i] == "6")
                      {
                          dkj = 1;
                      }
                      else
                          dkj = GetSafeDouble(sItem["DKJ" + i]);

                      if (i == 1)
                      {
                          if (GetSafeDouble(sItem["KLQD" + i]) >= 1.1 * GetSafeDouble(sItem["G_KLQD"]))
                          {
                              dkj = 1;
                              sItem["SFCG" + i] = "≥1.1倍";
                          }
                          else if (GetSafeDouble(sItem["KLQD" + i]) >= GetSafeDouble(sItem["G_KLQD"]) && GetSafeDouble(sItem["KLQD" + i]) < 1.1 * GetSafeDouble(sItem["G_KLQD"]))
                          {
                              sItem["SFCG" + i] = "1.1倍>实测强度≥标准强度";
                          }
                          else if (GetSafeDouble(sItem["KLQD" + i]) < GetSafeDouble(sItem["G_KLQD"]))
                          {
                              sItem["SFCG" + i] = "没达到标准强度";
                          }
                      }

                      //Public Const only_hf_scl = 2     '只断于焊缝
                      //Public Const only_cd_scl = 3     '只脆断
                      //Public Const only_ryxq_scl = 6   '只断于热影响区
                      //Public Const ryxq_and_cd_scl = 5 '既断于热影响区又脆断
                      //Public Const hf_and_cd_scl = 4    '既断于焊缝又脆断   1.0表示断于焊缝之外并呈延性断裂
                      //Public Const hfw_and_yd_scl = 1     '断于焊缝外并呈延断
                      switch (dkj.ToString())
                      {
                          case "1"://'断于焊缝外并呈延断
                              mcnt = mcnt + 1;
                              break;
                          case "2"://只断于焊缝
                              this_bhg = this_bhg + 1;
                              break;
                          case "3"://只脆断
                              this_bhg = this_bhg + 1;
                              break;
                          case "4"://既断于焊缝又脆断   1.0表示断于焊缝之外并呈延性断裂

                              hf_and_cd_scl_cnt = hf_and_cd_scl_cnt + 1;
                              this_bhg = this_bhg + 1;
                              break;

                          case "5"://'既断于热影响区又脆断
                              ryxq_and_cd_scl_cnt = ryxq_and_cd_scl_cnt + 1;
                              this_bhg = this_bhg + 1;
                              break;
                          case "6"://'只断于热影响区
                              this_bhg = this_bhg + 1;
                              break;
                          default:
                              //Case Is >= mScl
                              //mcnt = mcnt + 1
                              mcnt = mcnt + 1;
                              break;
                      }
                  }


                  if (ryxq_and_cd_scl_cnt == 2 || hf_and_cd_scl_cnt == 2)
                  {
                      gIs2HforRyyq_cd = true;
                  }
                  if (ryxq_and_cd_scl_cnt == 3 || hf_and_cd_scl_cnt == 3)
                  {
                      gIs3HforRyyq_cd = true;
                  }
                  sItem["HG_SC"] = mcnt.ToString();

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
                    mMj = GetSafeDouble(mMj.ToString("0.0"));
                }
                else
                {
                    mMj = GetSafeDouble(mMj.ToString("G4"));
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
                    jcxm2 = "、" + sItem["JCXM"].Replace(',', '、') + "、";

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
                        sItem["DKJ1"] = "----";
                        sItem["DKJ2"] = "----";
                        sItem["DKJ3"] = "----";
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
                        sItem["LW1"] = "----";
                        sItem["LW2"] = "----";
                        sItem["LW3"] = "----";
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
            #region 变量处理
            int mallbhg_kl = 0;
            int mallbhg_sc = 0;
            int mallbhg_lw = 0;

            MItem[0]["FJJJ1"] = "";
            MItem[0]["FJJJ2"] = "";
            MItem[0]["FJJJ3"] = "";

            string mJSFF, mwxzh = "";
            double mZh, mKlqd, mScl, mLw, mLwjd, mLwzj = 0;
            double mHggs_klqd, mHggs_scl, mHggs_lw, mXlgs = 0;
            int mXwgs = 0;
            double mFsgs_klqd, mFsgs_scl, mFsgs_lw = 0;
            string mGjlb = "";
            String LwBzyq = "";
            String SclBzyq = "";
            int kl1, kl2, kl3 = 0;
            int kj1, kj2, kj3 = 0;
            #endregion

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                ggph = sItem["GCLX_PH"];

                sItem["FJ"] = false.ToString();
                mZh = GetSafeDouble(sItem["ZH_G"]);//配置字段
                //mZh = 0;
                mGjlb = sItem["GJLB"];

                if (string.IsNullOrEmpty(mGjlb))
                {
                    mGjlb = "";
                }
                #region 从设计等级表中取得相应的计算数值、等级标准
                var mrsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["GJLB"] == mGjlb.Trim());

                if (null == mrsDj)
                {
                    jsbeizhu = "依据不详";
                    mFlag_Bhg = true;
                    sItem["JCJG"] = "不下结论";
                    mjcjg = "不下结论";
                    continue;
                }
                else
                {
                    sItem["SJDJ"] = mrsDj["MC"];
                    mKlqd = GetSafeDouble(mrsDj["KLQDBZZ"]);//单组标准值
                    mScl = GetSafeDouble(mrsDj["SCLBZZ"]);
                    mLw = GetSafeDouble(mrsDj["LWBZZ"]);//要求冷弯值
                    mLwjd = GetSafeDouble(mrsDj["LWJD"]);//冷弯角度和冷弯直径
                    mLwzj = GetSafeDouble(mrsDj["LWZJ"]);
                    mHggs_klqd = GetSafeDouble(mrsDj["ZHGGS_KLQD"]);//单组合格个数
                    mHggs_scl = GetSafeDouble(mrsDj["ZHGGS_SCL"]);
                    mHggs_lw = GetSafeDouble(mrsDj["ZHGGS_LW"]);
                    mFsgs_klqd = GetSafeDouble(mrsDj["ZFSGS_KLQD"]);
                    mFsgs_scl = GetSafeDouble(mrsDj["ZFSGS_SCL"]);
                    mFsgs_lw = GetSafeDouble(mrsDj["ZFSGS_LW"]);
                    mLwzj = GetSafeDouble(mrsDj["LWZJ"]);//冷弯直径和角度
                    mLwjd = GetSafeDouble(mrsDj["LWJD"]);
                    mXlgs = GetSafeDouble(mrsDj["XLGS"]);
                    mXwgs =(int) GetSafeDouble(mrsDj["XWGS"]);

                    mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].Trim().ToLower();
                }

                //试验温度
                #endregion

                if (MItem[0]["PDBZ"].Contains("18-2012"))
                {
                    SclBzyq = "3个试件均断于钢筋母材，呈延性断裂或其中一个试件断于焊缝，呈脆性断裂，其抗拉强度大于或等于钢筋母材抗拉强度标准值。";
                }
                else
                {
                    SclBzyq = "至少" + mHggs_scl.ToString() + "个试件断于焊缝外，并应呈延性断裂。当发生脆断，抗拉强度≥1.10倍规定抗拉强度时，焊缝脆断按断于焊缝外并呈延性断裂处理。";
                }
                LwBzyq = "弯心直径=" + mLwzj + "d弯曲" + mLwjd + "度，有两个或三个试件外侧裂纹宽度＜0.5mm";
                sItem["WXZJ"] = mLwzj + "d";
                sItem["WQJD"] = mLwjd + "°";

                sItem["G_KLQD"] = mKlqd.ToString();
                sItem["G_DLWZ"] = SclBzyq.ToString();
                sItem["G_LWWZ"] = LwBzyq.ToString();

                //求抗拉强度
                int count = (int)(mXlgs);
                calc_kl(sItem, count);
                //冷弯
                for(int wq=1; wq< mXwgs + 1; wq++)
                {
                    if (sItem["LW" + wq] == "无裂纹"){ sItem["LW" + wq] = "1"; }
                    if (sItem["LW" + wq] == "有裂纹"){ sItem["LW" + wq] = "0"; }
                    if (sItem["LW" + wq] == "----"){ sItem["LW" + wq] = "-1"; }

                }
                mallbhg_lw = mallbhg_lw + find_singlezb_bhg(MItem[0], sItem, "lw", mLw, (int)(mXwgs));
                for (int wq = 1; wq < mXwgs + 1; wq++)
                {
                    if (sItem["LW" + wq] == "1") { sItem["LW" + wq] = "无裂纹"; }
                    if (sItem["LW" + wq] =="0") { sItem["LW" + wq] = "有裂纹"; }
                    if (sItem["LW" + wq] =="-1") { sItem["LW" + wq] = "----"; }

                }

                #region 判断标准包含18-2012
                if (!MItem[0]["PDBZ"].Contains("18-2012"))
                {
                    mallbhg_kl = mallbhg_kl + find_singlezb_bhg(MItem[0], sItem, "kl", mKlqd, count);
                    mallbhg_sc = mallbhg_sc + find_dkj_bhg(MItem[0], sItem, mScl, count);

                    if (!all_hj_zb_jl(MItem[0], sItem, mHggs_klqd, mHggs_scl, mHggs_lw))
                    {
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    kl1 = (int)GetSafeDouble(sItem["KLQD1"]);
                    kl2 = (int)GetSafeDouble(sItem["KLQD2"]);
                    kl3 = (int)GetSafeDouble(sItem["KLQD3"]);
                    for (int a = 1; a < 4; a++)
                    {
                        if (sItem["DKJ" + a] == "断于钢筋母材，延性断裂") { sItem["DKJ" + a] = "1"; }
                        if (sItem["DKJ" + a] == "断于钢筋母材，脆性断裂") { sItem["DKJ" + a] = "2"; }
                        if (sItem["DKJ" + a] == "断于焊缝，脆性断裂") { sItem["DKJ" + a] = "3"; }
                        if (sItem["DKJ" + a] == "断于热影响区，延性断裂") { sItem["DKJ" + a] = "4"; }
                        if (sItem["DKJ" + a] == "断于热影响区，脆性断裂") { sItem["DKJ" + a] = "5"; }
                    }
                    kj1 = (int)GetSafeDouble(sItem["DKJ1"]);
                    kj2 = (int)GetSafeDouble(sItem["DKJ2"]);
                    kj3 = (int)GetSafeDouble(sItem["DKJ3"]);
                    for (int c = 1; c < 4; c++)
                    {
                        if (sItem["DKJ" + c] == "1") { sItem["DKJ" + c] = "断于钢筋母材，延性断裂"; }
                        if (sItem["DKJ" + c] == "2") { sItem["DKJ" + c] = "断于钢筋母材，脆性断裂"; }
                        if (sItem["DKJ" + c] == "3") { sItem["DKJ" + c] = "断于焊缝，脆性断裂"; }
                        if (sItem["DKJ" + c] == "4") { sItem["DKJ" + c] = "断于热影响区，延性断裂"; }
                        if (sItem["DKJ" + c] == "5") { sItem["DKJ" + c] = "断于热影响区，脆性断裂"; }

                    }


                  
                    // 旧值   valueFixed--断于焊缝之外，延性断裂,1,1 | 断于焊缝，延性断裂,2,0 | 断于焊缝之外，脆性断裂,3,0 | 断于焊缝，脆性断裂,4,0 | 既断于热影响区又脆断,5,0 | 断于热影响区，延性断裂,6,0 | 断于焊缝，脆性断裂(焊口开裂),7,0
                    //1为断于钢筋母材,延性断裂；2为断于母材,脆性断裂；3为断于焊缝,脆性断裂；4为断于热影响区,延性断裂；5为断于热影响区,脆性断裂
                    switch (kj1.ToString())
                    {
                        case "1":
                            sItem["DLTZ1"] = "断于钢筋母材,延性断裂";
                            break;
                        case "2":
                            sItem["DLTZ1"] = "断于钢筋母材,脆性断裂";
                            break;
                        case "3":
                            sItem["DLTZ1"] = "断于焊缝,脆性断裂";
                            break;
                        case "4":
                            sItem["DLTZ1"] = "断于热影响区,延性断裂";
                            break;
                        case "5":
                            sItem["DLTZ1"] = "断于热影响区,脆性断裂";
                            break;
                    }
                    switch (kj2.ToString())
                    {
                        case "1":
                            sItem["DLTZ2"] = "断于钢筋母材,延性断裂";
                            break;
                        case "2":
                            sItem["DLTZ2"] = "断于钢筋母材,脆性断裂";
                            break;
                        case "3":
                            sItem["DLTZ2"] = "断于焊缝,脆性断裂";
                            break;
                        case "4":
                            sItem["DLTZ2"] = "断于热影响区,延性断裂";
                            break;
                        case "5":
                            sItem["DLTZ2"] = "断于热影响区,脆性断裂";
                            break;

                    }
                    switch (kj3.ToString())
                    {
                        case "1":
                            sItem["DLTZ3"] = "断于钢筋母材,延性断裂";
                            break;
                        case "2":
                            sItem["DLTZ3"] = "断于钢筋母材,脆性断裂";
                            break;
                        case "3":
                            sItem["DLTZ3"] = "断于焊缝,脆性断裂";
                            break;
                        case "4":
                            sItem["DLTZ3"] = "断于热影响区,延性断裂";
                            break;
                        case "5":
                            sItem["DLTZ3"] = "断于热影响区,脆性断裂";
                            break;
                    }

                    #region  //应检测中心要求修改
                    //断于热影响区，延性断裂 等效 断于钢筋母材，延性断裂 ；
                    //断于热影响区，脆性断裂 等效 断于焊缝，脆性断裂  ；
                    kj1 = kj1 == 4 ? 1 : kj1;
                    kj1 = kj1 == 5 ? 3 : kj1;

                    kj2 = kj2 == 4 ? 1 : kj2;
                    kj2 = kj2 == 5 ? 3 : kj2;

                    kj3 = kj3 == 4 ? 1 : kj3;
                    kj3 = kj3 == 5 ? 3 : kj3;

                    #endregion
                    #endregion

                    #region 拉伸
                    if (jcxm.Contains("、拉伸、") || jcxm.Contains("、抗拉强度、"))
                    {
                        //合格 
                        // 3个断于母材延性 抗拉强度>=标准值
                        // 2个断于母材延性，抗拉强度>=标准值1倍，另一个断于焊缝脆性

                        //复验
                        // 2个断于母材延性，抗拉强度>=标准值1倍； 另一个断于焊缝/热影响区，脆性断裂  抗拉强度<标准值1倍
                        // 1个断于母材延性，抗拉强度>=标准值1倍； 另2个断于焊缝/热影响区，脆性断裂  
                        // 3个断于焊缝脆性， 抗拉强度>=标准值

                        //不合格
                        //1个抗拉强度<标准值1倍
                        var md = kj3 + kj2 + kj1;
                        if (kl1 < mKlqd || kl2 < mKlqd || kl3 < mKlqd)
                        {
                            jcxmCur = CurrentJcxm(jcxm, "拉伸,抗拉强度");
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["JCJG_LS"] = "不符合";
                            mFlag_Bhg = true;
                        }
                        else if (md == 3 && (kl1 >= mKlqd && kl2 >= mKlqd && kl3 >= mKlqd))
                        {
                            // 三个断于母材 抗拉强度>=标准值
                            sItem["JCJG_LS"] = "符合";
                            mFlag_Hg = true;

                        }
                        else if (md == 5 && (kl1 >= mKlqd && kl2 >= mKlqd && kl3 >= mKlqd))
                        {
                            //2个断于母材延性，抗拉强度>=标准值1倍
                            //1给断于焊缝脆性
                            sItem["JCJG_LS"] = "符合";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["JCJG_LS"] = "复试";
                            mFlag_Bhg = true;
                            jcxmCur = CurrentJcxm(jcxm, "拉伸,抗拉强度");
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        sItem["JCJG_LS"] = "----";
                        sItem["DKJ1"] = "----";
                        sItem["DKJ2"] = "----";
                        sItem["DKJ3"] = "----";
                    }

                    var hg_lw = sItem["HG_LW"];
                    if (string.IsNullOrEmpty(hg_lw))
                    {
                        sItem["HG_LW"] = "0";
                    }
                    #endregion
                    #region 弯曲
                    if (jcxm.Contains("、冷弯、") || jcxm.Contains("、弯曲、"))
                    {
                        //冷弯
                        for (int wq = 1; wq < mXwgs + 1; wq++)
                        {
                            if (sItem["LW" + wq] == "无裂纹") { sItem["LW" + wq] = "1"; }
                            if (sItem["LW" + wq] == "有裂纹") { sItem["LW" + wq] = "0"; }
                            if (sItem["LW" + wq] == "----") { sItem["LW" + wq] = "-1"; }

                        }
                        mallbhg_lw = mallbhg_lw + find_singlezb_bhg(MItem[0], sItem, "lw", mLw, (int)(mXwgs));
                      
                        int Gs = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            if (GetSafeInt(sItem["LW" + i]) == 0)
                            {

                                Gs = Gs + 1;
                            }
                        }
                        if (Gs <= 1)
                        {
                            sItem["JCJG_LW"] = "符合";
                            mFlag_Hg = true;

                        }
                        else if (Gs == 2)
                        {
                            jcxmCur = CurrentJcxm(jcxm, "冷弯,弯曲");
                            sItem["JCJG_LW"] = "复试";
                            mFlag_Bhg = true;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                        }
                        else
                        {
                            mFlag_Bhg = true;
                            jcxmCur = CurrentJcxm(jcxm, "冷弯,弯曲");
                            sItem["JCJG_LW"] = "不符合";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        for (int wq = 1; wq < mXwgs + 1; wq++)
                        {
                            if (sItem["LW" + wq] == "1") { sItem["LW" + wq] = "无裂纹"; }
                            if (sItem["LW" + wq] == "0") { sItem["LW" + wq] = "有裂纹"; }
                            if (sItem["LW" + wq] == "-1") { sItem["LW" + wq] = "----"; }

                        }
                    }
                    else
                    {
                        sItem["JCJG_LW"] = "----";
                        sItem["LW1"] = "----";
                        sItem["LW2"] = "----";
                        sItem["LW3"] = "----";
                        sItem["HG_LW"] = "0";
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
                #region//开始判定单项指标是否合格,根据单项指标再判定单组结论是否合格
                if (sItem["JCJG"] == "合格")
                {
                    MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + mZh.ToString().Trim() + "#";

                }
                if (sItem["JCJG"] == "不合格")
                {
                    mAllHg = false;
                    if (MItem[0]["PDBZ"].Contains("18-2012"))
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
                        check_hj_double_Fj(MItem[0], sItem, mHggs_klqd, mHggs_scl, mHggs_lw, mFsgs_klqd, mFsgs_scl, mFsgs_lw, mZh);
                    }
                }
                mAllHg = mAllHg && (sItem["JCJG"] == "合格");
                #endregion
            }

            #region 添加最终报告
            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合要求。";


            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合要求。";
            }

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"]))
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合要求。";
                MItem[0]["FJJJ3"] = jsbeizhu;
            }

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"]))
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                MItem[0]["FJJJ2"] = jsbeizhu;
                if (mFlag_Hg && mFlag_Bhg)
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，另取双倍样复试。";
                    MItem[0]["FJJJ2"] = jsbeizhu;
                }
            }

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ1"]))
            {
                //jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //MItem[0]["FJJJ1"] = jsbeizhu;
                //if (mFlag_Hg && mFlag_Bhg)
                //{
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，另取双倍样复试。"; ;
                MItem[0]["FJJJ1"] = jsbeizhu;
                //}
            }

            if (!string.IsNullOrEmpty(mwxzh))
            {
                mwxzh = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样无效，应检验母材。";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样无效，应检验母材。"; ;
            }
            MItem[0]["JCJG"] = mjcjg;
            if (mjcjg == "不下结论")
            {
                jsbeizhu = "";
            }
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
