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
            var mAllHg = false;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;
            var jcxm = "";
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
                        //        if (Double.Parse(sItem["QFQD" + i + 1]) - mbzValue > -0.00001)
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
                        //case "scl":
                        //    for (int i = 0; i < count; i++)
                        //    {
                        //        //If isZdVisible("s" & gCurSylb, "qfhz" & i) Then '判断该指标是否存在
                        //        if (Double.Parse(sItem["SCL" + i+1]) - mbzValue > -0.00001)
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
                                if (Double.Parse(sItem["LW" + i]) - mbzValue > -0.00001)
                                {
                                    mcnt = mcnt + 1;
                                }
                                else
                                {
                                    if (i > 1)
                                    {
                                        if (Double.Parse(sItem["LW1"]) - i * mbzValue < -0.00001)// 判断是否把冷弯值全部输在第一个值上
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
                          dkj = Double.Parse(sItem["DKJ" + i]);

                      if (i == 1)
                      {
                          if (Double.Parse(sItem["KLQD" + i]) >= 1.1 * Double.Parse(sItem["G_KLQD"]))
                          {
                              dkj = 1;
                              sItem["SFCG" + i] = "≥1.1倍";
                          }
                          else if (Double.Parse(sItem["KLQD" + i]) >= Double.Parse(sItem["G_KLQD"]) && Double.Parse(sItem["KLQD" + i]) < 1.1 * Double.Parse(sItem["G_KLQD"]))
                          {
                              sItem["SFCG" + i] = "1.1倍>实测强度≥标准强度";
                          }
                          else if (Double.Parse(sItem["KLQD" + i]) < Double.Parse(sItem["G_KLQD"]))
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

            string mJSFF, mwxzh = "";
            double mZh, mKlqd, mScl, mLw, mLwjd, mLwzj = 0;
            double mHggs_klqd, mHggs_scl, mHggs_lw, mXlgs, mXwgs = 0;
            double mFsgs_klqd, mFsgs_scl, mFsgs_lw = 0;
            string mGjlb = "";
            String LwBzyq = "";
            String SclBzyq = "";
            int kl1, kl2, kl3 = 0;
            int kj1, kj2, kj3 = 0;

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                sItem["FJ"] = false.ToString();
                mZh = Double.Parse(sItem["ZH_G"]);
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
                    mHggs_scl = Double.Parse(mrsDj["ZHGGS_SCL"]);
                    mHggs_lw = Double.Parse(mrsDj["ZHGGS_LW"]);
                    mFsgs_klqd = Double.Parse(mrsDj["ZFSGS_KLQD"]);
                    mFsgs_scl = Double.Parse(mrsDj["ZFSGS_SCL"]);
                    mFsgs_lw = Double.Parse(mrsDj["ZFSGS_LW"]);
                    mLwzj = Double.Parse(mrsDj["LWZJ"]);//冷弯直径和角度
                    mLwjd = Double.Parse(mrsDj["LWJD"]);
                    mXlgs = Double.Parse(mrsDj["XLGS"]);
                    mXwgs = Double.Parse(mrsDj["XWGS"]);

                    mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].Trim().ToLower();
                }

                //试验温度
                //MItem[0]["SYWD"] = sItem["SYHJWD"] + "℃";
                #endregion

                if (MItem[0]["PDBZ"].Contains("18-2012"))
                {
                    SclBzyq = "3个试件均断于母材，呈延性断裂或其中一个试件断于焊缝，呈脆性断裂，其抗拉强度大于或等于钢筋母材抗拉强度标准值。";
                }
                else
                {
                    SclBzyq = "至少" + mHggs_scl.ToString() + "个试件断于焊缝外，并应呈延性断裂。当发生脆断，抗拉强度≥1.10倍规定抗拉强度时，焊缝脆断按断于焊缝外并呈延性断裂处理。";
                }
                LwBzyq = "弯心直径=" + mLwzj + "d弯曲" + mLwjd + "度，有两个或三个试件外侧裂纹宽度＜0.5mm";

                sItem["G_KLQD"] = mKlqd.ToString();
                sItem["G_DLWZ"] = SclBzyq.ToString();
                sItem["G_LWWZ"] = LwBzyq.ToString();

                //求抗拉强度
                int count = (int)(mXlgs);
                calc_kl(sItem, count);

                mallbhg_lw = mallbhg_lw + find_singlezb_bhg(MItem[0], sItem, "lw", mLw, (int)(mXwgs));

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
                    kl1 = (int)Double.Parse(sItem["KLQD1"]);
                    kl2 = (int)Double.Parse(sItem["KLQD2"]);
                    kl3 = (int)Double.Parse(sItem["KLQD3"]);
                    kj1 = (int)Double.Parse(sItem["DKJ1"]);
                    kj2 = (int)Double.Parse(sItem["DKJ2"]);
                    kj3 = (int)Double.Parse(sItem["DKJ3"]);

                    switch (kj1.ToString())
                    {
                        case "1":
                            sItem["DLTZ1"] = "断于焊缝之外，延性断裂";
                            break;
                        case "2":
                            sItem["DLTZ1"] = "断于焊缝，延性断裂";
                            break;
                        case "3":
                            sItem["DLTZ1"] = "断于焊缝之外，脆性断裂";
                            break;
                        case "4":
                            sItem["DLTZ1"] = "断于焊缝，脆性断裂";
                            break;
                        case "5":
                            sItem["DLTZ1"] = "既断于热影响区又脆断";
                            break;
                        case "6":
                            sItem["DLTZ1"] = "断于热影响区，延性断裂";
                            break;
                        case "7":
                            sItem["DLTZ1"] = "断于钢筋母材，延性断裂";
                            break;
                        case "8":
                            sItem["DLTZ1"] = "断于钢筋母材，脆性断裂";
                            break;
                        case "9":
                            sItem["DLTZ1"] = "断于焊缝，脆性断裂(焊口开裂)";
                            break;
                    }

                    switch (kj2.ToString())
                    {
                        case "1":
                            sItem["DLTZ2"] = "断于焊缝之外，延性断裂";
                            break;
                        case "2":
                            sItem["DLTZ2"] = "断于焊缝，延性断裂";
                            break;
                        case "3":
                            sItem["DLTZ2"] = "断于焊缝之外，脆性断裂";
                            break;
                        case "4":
                            sItem["DLTZ2"] = "断于焊缝，脆性断裂";
                            break;
                        case "5":
                            sItem["DLTZ2"] = "既断于热影响区又脆断";
                            break;
                        case "6":
                            sItem["DLTZ2"] = "断于热影响区，延性断裂";
                            break;
                        case "7":
                            sItem["DLTZ2"] = "断于钢筋母材，延性断裂";
                            break;
                        case "8":
                            sItem["DLTZ2"] = "断于钢筋母材，脆性断裂";
                            break;
                        case "9":
                            sItem["DLTZ2"] = "断于焊缝，脆性断裂(焊口开裂)";
                            break;
                    }

                    switch (kj3.ToString())
                    {
                        case "1":
                            sItem["DLTZ3"] = "断于焊缝之外，延性断裂";
                            break;
                        case "2":
                            sItem["DLTZ3"] = "断于焊缝，延性断裂";
                            break;
                        case "3":
                            sItem["DLTZ3"] = "断于焊缝之外，脆性断裂";
                            break;
                        case "4":
                            sItem["DLTZ3"] = "断于焊缝，脆性断裂";
                            break;
                        case "5":
                            sItem["DLTZ3"] = "既断于热影响区又脆断";
                            break;
                        case "6":
                            sItem["DLTZ3"] = "断于热影响区，延性断裂";
                            break;
                        case "7":
                            sItem["DLTZ3"] = "断于钢筋母材，延性断裂";
                            break;
                        case "8":
                            sItem["DLTZ3"] = "断于钢筋母材，脆性断裂";
                            break;
                        case "9":
                            sItem["DLTZ3"] = "断于焊缝，脆性断裂(焊口开裂)";
                            break;
                    }

                    #region  //应检测中心要求修改
                    //断于热影响区，延性断裂 等效 断于钢筋母材，延性断裂 ；
                    //断于热影响区，脆性断裂 等效 断于焊缝，脆性断裂  ；
                    //断于焊缝，脆性断裂(焊口开裂) 等效 断于焊缝，脆性断裂；
                    kj1 = kj1 == 6 ? 7 : kj1;
                    kj1 = kj1 == 5 ? 4 : kj1;
                    kj1 = kj1 == 9 ? 4 : kj1;

                    kj2 = kj2 == 6 ? 7 : kj2;
                    kj2 = kj2 == 5 ? 4 : kj2;
                    kj2 = kj2 == 9 ? 4 : kj2;

                    kj3 = kj3 == 6 ? 7 : kj3;
                    kj3 = kj3 == 5 ? 4 : kj3;
                    kj3 = kj3 == 9 ? 4 : kj3;
                    #endregion

                    if (jcxm.Contains("、拉伸、") || jcxm.Contains("、抗拉强度、"))
                    {
                        if (kl1 >= mKlqd && kl2 >= mKlqd && (kj1 == 7 || kj1 == 6) && (kj2 == 7 || kj2 == 6) && kl3 >= mKlqd && (kj3 == 7 || kj3 == 6) || (kl1 >= mKlqd && (kj1 == 7 || kj1 == 6) &&
                    kl2 >= mKlqd && (kj2 == 7 || kj2 == 6) && kl3 >= mKlqd && (kj3 == 4 || kj3 == 5))
                    || (kl3 >= mKlqd && (kj3 == 7 || kj3 == 6) && kl2 >= mKlqd && (kj2 == 7 || kj2 == 6) && kl1 >= mKlqd && (kj1 == 4 || kj1 == 5))
                    || (kl1 >= mKlqd && (kj1 == 7 || kj1 == 6) && kl3 >= mKlqd && (kj3 == 7 || kj3 == 6) && kl2 >= mKlqd && (kj2 == 4 || kj2 == 5)))
                        {
                            sItem["JCJG_LS"] = "符合";
                        }
                        else
                        {
                            if ((kl1 >= mKlqd && (kj1 == 5 || kj1 == 4) && kl2 >= mKlqd && (kj2 == 5 || kj2 == 4) && kl3 >= mKlqd && (kj3 == 5 || kj3 == 4))
                                || ((kl1 >= mKlqd && (kj1 == 7 || kj1 == 6) && kl2 >= mKlqd && (kj2 == 7 || kj2 == 6) && kl3 < mKlqd && (kj3 == 4 || kj3 == 5))
                                || (kl1 >= mKlqd && (kj1 == 6 || kj1 == 7) && kl2 < mKlqd && (kj2 == 4 || kj2 == 5) && kl3 >= mKlqd && (kj3 == 6 || kj3 == 7))
                                || (kl3 >= mKlqd && (kj3 == 6 || kj3 == 7) && kl2 >= mKlqd && (kj2 == 6 || kj2 == 7) && kl1 < mKlqd && (kj1 == 5 || kj1 == 4)))
                                || ((kl1 >= mKlqd && (kj1 == 6 || kj1 == 7) && (kj2 == 5 || kj2 == 4) && (kj3 == 5 || kj3 == 4))
                                || (kl2 >= mKlqd && (kj2 == 6 || kj2 == 7) && (kj1 == 5 || kj1 == 4) && (kj3 == 5 || kj3 == 4))
                                || (kl3 >= mKlqd && (kj3 == 6 || kj3 == 7) && (kj2 == 5 || kj2 == 4) && (kj1 == 5 || kj1 == 4))))
                            {
                                sItem["JCJG_LS"] = "复试";
                                mFlag_Bhg = true;
                            }
                            else
                            {
                                if ((kj1 == 8 || kj2 == 8 || kj3 == 8) || (kl1 < mKlqd && kj1 == 7) || (kl2 < mKlqd && kj2 == 7) || (kl3 < mKlqd && kj3 == 7))
                                {
                                    sItem["JCJG_LS"] = "无效";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    sItem["JCJG_LS"] = "不符合";
                                    mFlag_Bhg = true;
                                }

                            }

                        }
                    }
                    else
                    {
                        sItem["JCJG_LS"] = "----";
                        sItem["DKJ1"] = "-1";
                        sItem["DKJ2"] = "-1";
                        sItem["DKJ3"] = "-1";
                    }

                    var hg_lw = sItem["HG_LW"];
                    if (string.IsNullOrEmpty(hg_lw))
                    {
                        sItem["HG_LW"] = "0";
                    }

                    if (jcxm.Contains("、冷弯、")|| jcxm.Contains("、弯曲、"))
                    {
                        int Gs = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            if (GetSafeInt(sItem["LW" + i]) >= 0.5)
                            {
                                Gs = Gs + 1;
                            }
                        }
                        if (Gs <= 1)
                        {
                            sItem["JCJG_LW"] = "符合";

                        }
                        else if (Gs == 2)
                        {
                            sItem["JCJG_LW"] = "复试";
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
                        sItem["HG_LW"] = "0";
                    }

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
                        check_hj_double_Fj(MItem[0], sItem, mHggs_klqd, mHggs_scl, mHggs_lw, mFsgs_klqd, mFsgs_scl, mFsgs_lw, mZh);
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
