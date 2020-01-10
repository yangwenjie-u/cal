using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GHF : BaseMethods
    {
        public void Calc()
        {
            #region
            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = null;

            var extraDJ = dataExtra["BZ_GHF_DJ"];
            var jcxmItems = retData.Select(u => u.Key).ToArray();
            //检测项 ：拉伸，抗拉强度，冷弯
            string jcxm = "拉伸,抗拉强度,冷弯";
            string mgjlb = "";//
            string mKlqd, mScl, mLw, mLwjd, mLwzj, mXlgs, mXwgs, mHggs_klqd, mHggs_scl, mHggs_lw = "";
            string SclBzyq, LwBzyq = "";
            bool msffs, mLsfs, mLwfs = true;
            int mallbhg_kl, mallbhg_sc = 0;
            int mallbhg_lw = 0;
            bool gIs2HforRyyq_cd = true;//   '标注2个试件在焊缝或热影响区脆断
            bool gIs3HforRyyq_cd = true;// '标注2个试件在焊缝或热影响区脆断
            int mWxgs = 0;
            int mbhggs = 0;//不合格数量

            #region 局部函数
            bool result = true; //调用公工代码（FunCommon）后返回的结果

            //返回值为每组每种指标不合格总数  ' mbzValue 是单前判断指标的标准值, count 是一组中的检测个数
            Func<IDictionary<string, string>, IDictionary<string, string>, string, double, int, int> find_singlezb_bhg =
                delegate (IDictionary<string, string> mItem , IDictionary<string, string> sItem, string zbName, double mbzValue, int count)
                {
                    int mcnt = 0;//计算单组合格个数
                                 //int mCurBhg_qf;//计算单组不合格个数
                    int this_bhg = 0;//当前组单个指标不合格累加

                    switch (zbName)
                    {
                        case "qf":
                            for (int i = 0; i < count; i++)
                            {
                                //If isZdVisible("s" & gCurSylb, "qfhz" & i) Then '判断该指标是否存在
                                if (GetSafeDouble(sItem["QFQD" + i]) - mbzValue > -0.00001)
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
                            for (int i = 0; i < count; i++)
                            {
                                //If isZdVisible("s" & gCurSylb, "qfhz" & i) Then '判断该指标是否存在
                                if (GetSafeDouble(sItem["klqd" + i]) - mbzValue > -0.00001)
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
                        case "scl":
                            for (int i = 0; i < count; i++)
                            {
                                //If isZdVisible("s" & gCurSylb, "qfhz" & i) Then '判断该指标是否存在
                                if (GetSafeDouble(sItem["SCL" + i]) - mbzValue > -0.00001)
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
                        case "lw":
                            for (int i = 0; i < count; i++)
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
                            sItem["HG_KL"] = mcnt.ToString();
                            break;
                    }

                    sItem["HG_LW"] = mcnt.ToString();
                    return this_bhg;
                };

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> FunCommon = delegate (IDictionary<string, string> sItem, IDictionary<string, string> mItem)
            {
                bool hg = true;
                mItem["FJJJ1"] = "";
                mItem["FJJJ2"] = "";
                mItem["FJJJ3"] = "";

                //if (string.IsNullOrEmpty(sItem["syr"]))
                //{
                //}
                //mZh = sItem["ZH"];
                mgjlb = sItem["GJLB"];//            '设计等级名称

                if (string.IsNullOrEmpty(mgjlb))
                {
                    mgjlb = "";
                }

                //试验温度
                if (sItem["SYHJWD"].Length != 0)
                {
                    sItem["SYWD"] = sItem["SYHJWD"] + "℃";
                }

                var extraFieldsDj = extraDJ.FirstOrDefault(u => u.Keys.Contains("GJLB") && u.Values.Contains(mgjlb.Trim()) && u.Keys.Contains("PH") && u.Values.Contains(sItem["GCLX_PH"].Trim()));
                if (null == extraFieldsDj)
                {
                    sItem["JCJG"] = "依据不详";
                    return false;
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


                    if (sItem["JCXM"].Trim().Contains("拉伸") || (sItem["JCXM"].Trim().Contains("抗拉强度")))
                    {
                        mLsfs = true;
                    }
                    else
                    {
                        mLsfs = false;
                    }
                    if (sItem["JCXM"].Trim().Contains("冷弯"))
                    {
                        mLwfs = true;
                    }
                    else
                    {
                        mLwfs = false;
                    }
                    // mItem["which"]= extraFieldsDj["which"];

                }


                //if (mItem["PDBZ"].Contains("18-2012"))
                //{
                //    //mItem["WHICH"] = extraFieldsDj["WHICH"];
                //    SclBzyq = "若有4个或4个以上试件断于钢筋母材，呈延性断裂，另2个或2个以下试件断于焊缝，呈脆性断裂，其抗拉强度大于或等于钢筋母材抗拉强度标准值。";
                //}
                //else
                //{
                SclBzyq = "至少" + mHggs_scl + "个试件断于焊缝外，并应呈延性断裂。当发生脆断，抗拉强度≥1.10倍规定抗拉强度时，焊缝脆断按断于焊缝外并呈延性断裂处理。";
                //}

                //if (mItem["jydbh"] > 150700000)
                //{
                //    mItem["which"] = extraFieldsDj["which"];
                //    LwBzyq = "弯心直径=" & CStr(mLwzj) & "d弯曲" & CStr(mLwjd) & "度，不超过两个试件外侧裂纹宽度≥0.5mm";
                //}
                //else {
                //    LwBzyq = "弯心直径="+mLwzj+  "d弯曲" & CStr(mLwjd) & "度，至少" + CStr(mHggs_lw) + "个试件未发生破裂"

                sItem["G_KLQD"] = string.IsNullOrEmpty(mKlqd)?"0" : mKlqd;
                sItem["G_DLWZ"] = SclBzyq;
                sItem["G_LWWZ"] = LwBzyq;

                if (sItem["JCXM"].Contains("冷弯"))
                {
                    sItem["G_LWWZ"] = "----";

                }
                //求抗拉强度
                //calc_kl mrssubTable, (mxlgs)


                mallbhg_lw = mallbhg_lw + find_singlezb_bhg(mItem, sItem, "lw", GetSafeDouble(mLw), GetSafeInt(mXwgs));
                return hg;
            };

            Func<IDictionary<string, string>, IDictionary<string, string>, double, int, int> find_dkj_bhg =
              delegate (IDictionary<string, string> sItem, IDictionary<string, string> mItem, double dkj_mScl, int count)
              {
                  double dkj = 0;//断口值
                  int mcnt = 0;//计算单组合格个数
                  int this_bhg = 0;//计算不和格个数
                  int ryxq_and_cd_scl_cnt = 0;//既断于热影响区又脆断计数器
                  int hf_and_cd_scl_cnt = 0;//既断于焊缝又脆断计数器

                  gIs2HforRyyq_cd = false;
                  gIs3HforRyyq_cd = false;

                  for (int i = 0; i < count; i++)
                  {
                      //if isZdVisible("s" & gCurSylb, "dkj" & i) Then 判断是否存在
                      if (sItem["DKJ"] + i == "5")
                      {
                          dkj = 2;
                      }
                      else if (sItem["DKJ"] + i == "6")
                      {
                          dkj = 1;
                      }
                      else
                          dkj = GetSafeDouble(sItem["DKJ"] + i);

                      if (i == 1)
                      {
                          if (GetSafeDouble(sItem["KLQD"] + i) >= 1.1 * GetSafeDouble(sItem["G_KLQD"]))
                          {
                              dkj = 1;
                              sItem["SFCG" + i] = "≥1.1倍";
                          }
                          else if (GetSafeDouble(sItem["KLQD"] + i) >= GetSafeDouble(sItem["G_KLQD"]) && GetSafeDouble(sItem["KLQD"] + i) < 1.1 * GetSafeDouble(sItem["G_KLQD"]))
                          {
                              sItem["SFCG" + i] = "1.1倍>实测强度≥标准强度";
                          }
                          else if (GetSafeDouble(sItem["KLQD"] + i) < GetSafeDouble(sItem["G_KLQD"]))
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
            #endregion
            int index = -1;
            string jcxmKey = "";
            if (retData.ContainsKey("拉伸") || retData.ContainsKey("抗拉强度"))
            {
                jcxmKey = "抗拉强度";

                if (retData[jcxmKey]["M_GHF"].Count != 1)
                {
                    //数据不完整
                    throw new Exception("主表数据不完整");
                }

                foreach (var sItem in retData[jcxmKey]["S_GHF"])
                {
                    mKlqd = "";
                    mXlgs = "";
                    mScl = "";
                    mallbhg_kl = 0;
                    mallbhg_sc = 0;
                    int x = 0;
                    int y = 0;
                    int dkj, kl = 0;
                    index++;
                    var mItem = retData[jcxmKey]["M_GHF"][0];

                    result = FunCommon(sItem, mItem);
                    var XQData = retData[jcxmKey]["S_BY_RW_XQ"];
                    if (!result)//单条数据异常，记录日志
                    {
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "获取不到主表数据";
                        mbhggs = mbhggs + 1;
                        continue;
                    }
                    //if (!mItem["PDBZ"].Contains("18-2012"))
                    //{
                    //    //GetSafeDouble(mLw), GetSafeInt(mXwgs));
                    //    mallbhg_kl = mallbhg_kl + find_singlezb_bhg(mItem, sItem, "kl", GetSafeDouble(mKlqd), GetSafeInt(mXlgs));
                    //    mallbhg_sc = mallbhg_sc + find_dkj_bhg(mItem, sItem, GetSafeDouble(mScl), GetSafeInt(mXlgs));
                    //    continue;
                    //}



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
                        if (x <= 2 && y >= 4 && x + y > 6)
                        {
                            sItem["JCJG_LS"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LS"] = "不符合";
                        }
                    }
                    if (mbhggs == 0)
                    {
                        XQData[index]["JCJG"] = "合格";
                        XQData[index]["JCJGMS"] = "该组试样所检项目符合{}" + "标准要求。";
                    }
                    else
                    {
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "该组试样不符合{}" + "标准要求。";
                    }
                }
            }
            else
            {
                //sitem["jcjg_ls = "----"
                //sitem["dkj1 = -1
                //sitem["dkj2 = -1
                //sitem["dkj3 = -1
                //sitem["dkj4 = -1
                //sitem["dkj5 = -1
                //sitem["dkj6 = -1
            }
            //遍历每一条数据 如果hg_lw不合格，赋值hg_lw = 0
            //if ()
            if (retData.ContainsKey("冷弯"))
            {
                jcxmKey = "冷弯";
                var mItem = retData[jcxmKey]["M_GHF"][0];
                if (retData[jcxmKey]["M_GHF"].Count != 1)
                {
                    //数据不完整
                    throw new Exception("主表数据不完整");
                }

                foreach (var sItem in retData[jcxmKey]["S_GHF"])
                {
                    FunCommon(sItem, mItem);

                    var XQData = retData[jcxmKey]["S_BY_RW_XQ"];
                    if (!result)//单条数据异常，记录日志
                    {
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "获取不到主表数据";
                        mbhggs = mbhggs + 1;
                        continue;
                    }

                    if (GetSafeDouble(sItem["HG_LW"]) >= GetSafeDouble(mHggs_lw))
                    {
                        sItem["JCJG_LW"] = "符合";
                    }
                    else
                    {
                        sItem["JCJG_LW"] = "不符合";
                        mbhggs = mbhggs + 1;
                    }

                    if (mbhggs == 0)
                    {
                        XQData[index]["JCJG"] = "合格";
                        XQData[index]["JCJGMS"] = "该组试样所检项目符合{}" + "标准要求。";
                    }
                    else
                    {
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "该组试样不符合{}" + "标准要求。";
                    }
                }

            }

            #region 添加最终报告
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            if (mbhggs > 0)
            {
                //不合格
                bgjgDic.Add("JCJG", "不合格");
                bgjgDic.Add("JCJGMS", "该组试样所检项目符合不标准要求。");
            }
            else
            {
                bgjgDic.Add("JCJG", "合格");
                bgjgDic.Add("JCJGMS", "该组试样所检项目符合标准要求。");
            }

            bgjg.Add(bgjgDic);
            //retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            //retData["BGJG"].Add("BGJG", bgjg);
            #endregion

            #endregion
        }
    }
}
