using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class GHF2 : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_GHF_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_GHF"];

            if (!data.ContainsKey("M_GHF"))
            {
                data["M_GHF"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GHF"];

            if (MItem.Count == 0)
            //if (M_HNT == default || M_HNT.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mItemHg = true;

            string mgjlb = "";////设计等级名称
            string mKlqd, mScl, mLw, mLwjd, mLwzj, mXlgs, mXwgs, mHggs_klqd, mHggs_scl, mHggs_lw = "";
            string SclBzyq, LwBzyq = "";
            bool msffs, mLsfs, mLwfs = true;
            int mallbhg_kl = 0;
            int mallbhg_sc = 0;
            int mallbhg_lw = 0;
            bool gIs2HforRyyq_cd = true;//   '标注2个试件在焊缝或热影响区脆断
            bool gIs3HforRyyq_cd = true;// '标注2个试件在焊缝或热影响区脆断
            int mWxgs = 0;
            int mbhggs = 0;//不合格数量

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

            #endregion

            MItem[0]["FJJJ1"] = "";
            MItem[0]["FJJJ2"] = "";
            MItem[0]["FJJJ3"] = "";

            var jcxm = "";
            foreach (var sItem in SItems)
            {
                mgjlb = sItem["GJLB"];
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                // 试验温度
                MItem[0]["SYWD"] = sItem["SYHJWD"] + "℃";

                #region 
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u.Keys.Contains("GJLB") && u.Values.Contains(mgjlb.Trim()) && u.Keys.Contains("PH") && u.Values.Contains(sItem["GCLX_PH"].Trim()));
                if (null == extraFieldsDj)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "依据不详";
                    continue;
                }
                else
                {
                    sItem["SJDJ"] = extraFieldsDj["MC"];
                    mKlqd = extraFieldsDj["KLQDBZZ"];//单组标准值

                    mScl = extraFieldsDj["SCLBZZ"];
                    mLw = extraFieldsDj["LWBZZ"];

                    mLwjd = extraFieldsDj["LWJD"];// '冷弯角度和冷弯直径
                    mLwzj = extraFieldsDj["LWZJ"];

                    mHggs_klqd = extraFieldsDj["ZHGGS_KLQD"]; //'单组合格个数
                    mHggs_scl = extraFieldsDj["ZHGGS_SCL"];
                    mHggs_lw = extraFieldsDj["ZHGGS_LW"];

                    mLwzj = extraFieldsDj["LWZJ"]; //'冷弯直径和角度
                    mLwjd = extraFieldsDj["LWJD"];
                    mXlgs = extraFieldsDj["XLGS"];
                    mXwgs = extraFieldsDj["XWGS"];


                    if (sItem["JCXM"].Trim().Contains("、拉伸、") || (sItem["JCXM"].Trim().Contains("、抗拉强度、")))
                    {
                        mLsfs = true;
                    }
                    else
                    {
                        mLsfs = false;
                    }
                    if (sItem["JCXM"].Trim().Contains("、冷弯、") || sItem["JCXM"].Trim().Contains("、弯曲、"))
                    {
                        mLwfs = true;
                    }
                    else
                    {
                        mLwfs = false;
                    }
                    // mItem["which"]= extraFieldsDj["which"];
                }
                #endregion

                if (MItem[0]["PDBZ"].Contains("18-2012"))
                {
                    //mItem["WHICH"] = extraFieldsDj["WHICH"];
                    SclBzyq = "若有4个或4个以上试件断于钢筋母材，呈延性断裂，另2个或2个以下试件断于焊缝，呈脆性断裂，其抗拉强度大于或等于钢筋母材抗拉强度标准值。";
                }
                else
                {
                    SclBzyq = "至少" + mHggs_scl + "个试件断于焊缝外，并应呈延性断裂。当发生脆断，抗拉强度≥1.10倍规定抗拉强度时，焊缝脆断按断于焊缝外并呈延性断裂处理。";
                }

                //if (Convert.ToInt32(MItem[0]["JYDBH"]) > 150700000)
                //{
                //mItem["which"] = extraFieldsDj["which"];
                LwBzyq = "弯心直径=" + mLwzj.ToString() + "d弯曲" + mLwjd.ToString() + "度，不超过两个试件外侧裂纹宽度≥0.5mm";
                //}
                //else
                //{
                //    LwBzyq = "弯心直径=" + mLwzj + "d弯曲" + mLwjd + "度，至少" + mHggs_lw + "个试件未发生破裂";
                //}

                sItem["G_KLQD"] = string.IsNullOrEmpty(mKlqd) ? "0" : mKlqd;
                sItem["G_DLWZ"] = SclBzyq;
                sItem["G_LWWZ"] = LwBzyq;

                if (jcxm.Contains("、冷弯、") || jcxm.Contains("、弯曲、"))
                {
                    sItem["G_LWWZ"] = "----";
                }
                //求抗拉强度
                int count = (int)GetSafeDouble(mXlgs);

                calc_kl(sItem, count);

                mallbhg_lw = mallbhg_lw + find_singlezb_bhg(MItem[0], sItem, "lw", GetSafeDouble(mLw), count);

                //求抗拉强度,断口,冷弯 合格个数,并且返回值为不同组不合格数的累加值
                if (!MItem[0]["PDBZ"].Contains("18-2012"))
                {
                    //GetSafeDouble(mLw), GetSafeInt(mXwgs));
                    mallbhg_kl = mallbhg_kl + find_singlezb_bhg(MItem[0], sItem, "kl", GetSafeDouble(mKlqd), count);
                    mallbhg_sc = mallbhg_sc + find_dkj_bhg(MItem[0], sItem, GetSafeDouble(mScl), count);
                    continue;
                }

                if (jcxm.Contains("、拉伸、") || jcxm.Contains("、抗拉强度、"))
                {
                    for (int i = 1; i < 7; i++)
                    {
                        switch (sItem["DKJ" + i])
                        {
                            case "1":
                                sItem["DLTZ" + i] = "断于焊缝之外，延性断裂";
                                break;
                            case "2":
                                sItem["DLTZ" + i] = "断于焊缝，延性断裂";
                                break;
                            case "3":
                                sItem["DLTZ" + i] = "断于焊缝之外，脆性断裂";
                                break;
                            case "4":
                                sItem["DLTZ" + i] = "断于焊缝，脆性断裂";
                                break;
                            case "5":
                                sItem["DLTZ" + i] = "既断于热影响区又脆断";
                                break;
                            case "6":
                                sItem["DLTZ" + i] = "断于热影响区，延性断裂";
                                break;
                            case "7":
                                sItem["DLTZ" + i] = "断于钢筋母材，延性断裂";
                                break;
                            case "8":
                                sItem["DLTZ" + i] = "断于钢筋母材，脆性断裂";
                                break;
                            case "9":
                                sItem["DLTZ" + i] = "断于焊缝，脆性断裂(焊口开裂)";
                                break;
                        }
                    }

                    int dkj, kl = 0;
                    int x = 0;
                    int y = 0;
                    for (int i = 1; i < 7; i++)
                    {
                        kl = GetSafeInt(sItem["KLQD" + i]);
                        dkj = GetSafeInt(sItem["DKJ" + i]);

                        dkj = dkj == 6 ? 7 : dkj;
                        dkj = dkj == 5 ? 4 : dkj;
                        dkj = dkj == 9 ? 4 : dkj;

                        if ((kl < GetSafeInt(mKlqd) && dkj == 7) || dkj == 8)
                        {
                            mWxgs = mWxgs + 1;
                        }
                        if (kl >= GetSafeInt(mKlqd) && (dkj == 5 || dkj == 4))
                        {
                            x = x + 1;
                        }
                        if (kl >= GetSafeInt(mKlqd) && (dkj == 7 || dkj == 6))
                        {
                            y = y + 1;
                        }
                    }

                    if (mWxgs >= 1)
                    {
                        sItem["JCJG_LS"] = "无效";
                    }
                    else
                    {
                        if (x <= 2 && y >= 4 && x + y == 6)
                        {
                            sItem["JCJG_LS"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LS"] = "不符合";

                        }
                    }
                }
                else
                {
                    sItem["JCJG_LS"] = "----";
                    sItem["DKJ1"] = "-1";
                    sItem["DKJ2"] = "-1";
                    sItem["DKJ3"] = "-1";
                    sItem["DKJ4"] = "-1";
                    sItem["DKJ5"] = "-1";
                    sItem["DKJ6"] = "-1";
                }

                if (jcxm.Contains("、冷弯、") || jcxm.Contains("、弯曲、"))
                {
                    //if (Convert.ToInt32(MItem[0]["JYDBH"]) > 150700000)
                    //{
                        var Gs = 0;
                        for (int i = 1; i < 7; i++)
                        {
                            if (Convert.ToDouble(sItem["LW" + i]) >= 0.5)
                            {
                                Gs = Gs + 1;
                            }
                        }

                        if (Gs <= 2)
                        {
                            sItem["JCJG_LW"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LW"] = "不符合";
                        }

                        if (GetSafeDouble(sItem["HG_LW"]) == 0)
                        {
                            sItem["HG_LW"] = "0";
                        }
                        if (GetSafeDouble(sItem["HG_LW"]) > GetSafeDouble(mHggs_lw))
                        {
                            sItem["JCJG_LW"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LW"] = "不符合";
                            mAllHg = false;
                        }

                        if (GetSafeDouble(sItem["HG_LW"]) >= GetSafeDouble(mHggs_lw))
                        {
                            sItem["JCJG_LW"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LW"] = "不符合";
                        }
                    //}
                }
                else
                {
                    sItem["JCJG_LW"] = "----";
                    sItem["LW1"] = "-1";
                    sItem["LW2"] = "-1";
                    sItem["LW3"] = "-1";
                    sItem["LW4"] = "-1";
                    sItem["LW5"] = "-1";
                    sItem["LW6"] = "-1";
                }

                if (sItem["JCJG_LS"] == "不符合" || sItem["JCJG_LW"] == "不符合")
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "该组试样的检测结果不合格";
                }
                else
                {
                    if (sItem["JCJG_LS"] == "无效")
                    {
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                        jsbeizhu = "该组试样的检测结果不合格";
                    }
                    else
                    {
                        sItem["JCJG"] = "合格";
                    }
                }
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                mjcjg = "不合格";
                if (mWxgs >= 1)
                {
                    jsbeizhu = "该组试样无效，应检验原材。";
                }
                else
                {
                    jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";

                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
        }
    }
}

