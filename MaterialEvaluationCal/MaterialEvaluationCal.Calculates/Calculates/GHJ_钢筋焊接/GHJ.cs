using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GHJ : BaseMethods
    {
        public void Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra,
            ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_GHJ_DJ"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();

            int mCnt_FjHg = 0;//记录复检合格的组数
            int mCnt_FjHg1 = 0;
            bool mFlag_Bhg = false;
            bool mFlag_Hg = false;
            bool mSFwc = true;
            int mbhggs = 0;//不合格数量
            bool mItemHg = true;//每一条检测项合格标识
            string mJSFF, mwxzh = "";
            int mZh, mKlqd, mScl, mLw, mLwjd, mLwzj = 0;
            int mHggs_klqd, mHggs_scl, mHggs_lw, mxlgs, mxwgs = 0;
            int mFsgs_klqd, mFsgs_scl, mFsgs_lw = 0;
            string mgjlb = "";
            String LwBzyq = "";
            String SclBzyq = "";
            int kl1, kl2, kl3 = 0;
            int kj1, kj2, kj3 = 0;

            // HG_LWG_KLQD
            #region 临时使用
            string jcxm2 = "拉伸,抗拉强度,冷弯";
            string zdmc = "HG_KL,HG_SC,HG_LW,G_KLQD,LQ,ZJ,MJ";
            List<string> zdList = zdmc.Split(',').ToList();

            #endregion
            foreach (var jcxm in jcxmItems)
            {
                if (jcxm.ToUpper().Contains("BGJG"))
                {
                    continue;
                }
                var s_dxltab = retData[jcxm]["S_GHJ"];
                foreach (var SGHFitem in s_dxltab)
                {
                    foreach (var zdItem in zdList)
                    {
                        if (!SGHFitem.Keys.Contains(zdItem))
                        {
                            continue;
                        }
                        SGHFitem[zdItem] = GetSafeDouble(SGHFitem[zdItem]).ToString();
                    }
                }
                //err += "jcxm =" + jcxm + "/r/n";
                //if (!jcxm.Keys.Contains(zdItem))
                //    sItem["XGM"] = GetSafeDouble(sItem["XGM"]).ToString();


                List<IDictionary<string, string>> bgjgXQ = new List<IDictionary<string, string>>();

                if (!retData[jcxm].Keys.Contains("S_BY_RW_XQ"))
                {
                    IList<IDictionary<string, string>> listXQDic = new List<IDictionary<string, string>>();
                    IDictionary<string, string> dicXQ = new Dictionary<string, string>();
                    dicXQ.Add("JCJG", "");
                    dicXQ.Add("JCJGMS", "");
                    listXQDic.Add(dicXQ);
                    retData[jcxm].Add("S_BY_RW_XQ", listXQDic);
                }

                var SItem = retData[jcxm]["S_GHJ"];
                var XQData = retData[jcxm]["S_BY_RW_XQ"];
                int index = 0;

                //遍历每条数据
                foreach (var sitem in SItem)
                {
                    //XQData[0]["RECID"] = item["RECID"];
                    //XQData[0]["SJWCJSSJ"] = DateTime.Now.ToString();
                    //if (null == MItem)
                    //{
                    //    mItemHg = false;
                    XQData[index]["JCJG"] = "不合格";
                    //    XQData[index]["JCJGMS"] = "获取不到主表数据";
                    //    mbhggs = mbhggs + 1;
                    //    mFlag_Bhg = true;
                    //    index++;
                    //    continue;
                    //}
                    //if (string.IsNullOrEmpty(item["SYR"]))
                    //{
                    //    mSFwc = false;
                    //    mItemHg = false;
                    //    XQData[index]["JCJG"] = "不合格";
                    //    XQData[index]["JCJGMS"] = "获取实验人信息失败";
                    //    index++;
                    //    continue;
                    //}
                    //MItem[index]["FJJJ1"] = "";
                    //MItem[index]["FJJJ2"] = "";
                    //MItem[index]["FJJJ3"] = "";

                    //if (sitem["SYR"] == null)
                    //{
                    //    mSFwc = false;
                    //    mItemHg = false;
                    //    XQData[index]["JCJG"] = "不合格";
                    //    XQData[index]["JCJGMS"] = "获取实验人信息失败";
                    //    index++;

                    //    continue;
                    //}
                    sitem["FJ"] = false.ToString();
                    //mZh = GetSafeInt(sitem["ZH"]);
                    mgjlb = sitem["GJLB"];
                    if (string.IsNullOrEmpty(mgjlb))
                    {
                        mgjlb = "";
                    }
                    #region 从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u.Keys.Contains("PH") && u.Values.Contains(sitem["GCLX_PH"].ToString().Trim()) && u.Keys.Contains("GJLB") && u.Values.Contains((sitem["GJLB"]).ToString().Trim()));
                    if (null == extraFieldsDj)
                    {
                        mJSFF = "";
                        //MItem[0]["BGBH"] = "";
                        //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "单组流水号" + sitem["DZBH"] + "试件尺寸为空";
                        mItemHg = false;
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "获取实验人信息失败";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = true;
                        index++;

                        continue;
                    }
                    else
                    {
                        sitem["SJDJ"] = extraFieldsDj["MC"];
                        mKlqd = GetSafeInt(extraFieldsDj["KLQDBZZ"]);//单组标准值
                        mScl = GetSafeInt(extraFieldsDj["SCLBZZ"]);
                        mLw = GetSafeInt(extraFieldsDj["LWBZZ"]);
                        mLwjd = GetSafeInt(extraFieldsDj["LWJD"]);//冷弯角度和冷弯直径
                        mLwzj = GetSafeInt(extraFieldsDj["LWZJ"]);
                        mHggs_klqd = GetSafeInt(extraFieldsDj["ZHGGS_KLQD"]);//单组合格个数
                        mHggs_scl = GetSafeInt(extraFieldsDj["ZHGGS_SCL"]);
                        mHggs_lw = GetSafeInt(extraFieldsDj["ZHGGS_LW"]);
                        mFsgs_klqd = GetSafeInt(extraFieldsDj["ZFSGS_KLQD"]);
                        mFsgs_scl = GetSafeInt(extraFieldsDj["ZFSGS_SCL"]);
                        mFsgs_lw = GetSafeInt(extraFieldsDj["ZFSGS_LW"]);
                        mLwzj = GetSafeInt(extraFieldsDj["LWZJ"]);//冷弯直径和角度
                        mLwjd = GetSafeInt(extraFieldsDj["LWJD"]);
                        mxlgs = GetSafeInt(extraFieldsDj["XLGS"]);
                        mxwgs = GetSafeInt(extraFieldsDj["XWGS"]);
                        //MItem[index]["WHICH"] = extraFieldsDj["WHICH"];
                        //if (MItem[index]["WHICH"] == "")
                        //{
                        //    MItem[index]["WHICH"] = "0";
                        //}
                        if (extraFieldsDj["JSFF"] == null)
                        {
                            mJSFF = "";
                        }
                        else
                        {
                            mJSFF = extraFieldsDj["JSFF"].Trim().ToLower();

                        }
                    }
                    #endregion
                    //开始计算
                    //if (string.Equals(MItem[index]["pdbz"], "18-2012"))
                    //{
                    //    MItem[index]["which"] = "1";
                    //    SclBzyq = "3个试件均断于母材，呈延性断裂或其中一个试件断于焊缝，呈脆性断裂，其抗拉强度大于或等于钢筋母材抗拉强度标准值。";
                    //}
                    //else
                    //{
                    //    SclBzyq = "至少" + mHggs_scl.ToString() + "个试件断于焊缝外，并应呈延性断裂。当发生脆断，抗拉强度≥1.10倍规定抗拉强度时，焊缝脆断按断于焊缝外并呈延性断裂处理。";
                    //}
                    //if (GetSafeInt(MItem[index]["jydbh"]) > 150700000 || GetSafeInt(MItem[index]["jydbh"]) == 150500270)
                    //{
                    //    MItem[index]["which"] = "2";
                    //    LwBzyq = "弯心直径=" + mLwzj + "d弯曲" + mLwjd + "度，有两个或三个试件外侧裂纹宽度＜0.5mm";

                    //}
                    //else
                    //{
                    //    LwBzyq = "弯心直径=" + mLwzj + "d弯曲" + mLwjd + "度，至少" + mHggs_lw + "个试件未发生破裂";
                    //}
                    sitem["G_KLQD"] = mKlqd.ToString();
                    //sitem["G_KLQD"] = SclBzyq.ToString();
                    //sitem["G_KLQD"] = LwBzyq.ToString();

                    #region//VB代码中求抗拉强度 下文未使用
                    //calc_kl mrssubTable, (mxlgs)
                    //mallbhg_lw = mallbhg_lw + find_singlezb_bhg(mrsmainTable, mrssubTable, "lw", mLw, (mxwgs))
                    #endregion
                    //if (string.Equals(MItem[index]["pdbz"], "18-2012"))
                    //{
                    kl1 = GetSafeInt(sitem["KLQD1"]);
                    kl2 = GetSafeInt(sitem["KLQD2"]);
                    kl3 = GetSafeInt(sitem["KLQD3"]);
                    kj1 = GetSafeInt(sitem["DKJ1"]);
                    kj2 = GetSafeInt(sitem["DKJ2"]);
                    kj3 = GetSafeInt(sitem["DKJ3"]);
                    switch (kl1)
                    {
                        case 1:
                            sitem["dltz1"] = "断于焊缝之外，延性断裂";
                            break;
                        case 2:
                            sitem["dltz1"] = "断于焊缝，延性断裂";
                            break;
                        case 3:
                            sitem["dltz1"] = "断于焊缝之外，脆性断裂";
                            break;
                        case 4:
                            sitem["dltz1"] = "断于焊缝，脆性断裂";
                            break;
                        case 5:
                            sitem["dltz1"] = "既断于热影响区又脆断";
                            break;
                        case 6:
                            sitem["dltz1"] = "断于热影响区，延性断裂";
                            break;
                        case 7:
                            sitem["dltz1"] = "断于钢筋母材，延性断裂";
                            break;
                        case 8:
                            sitem["dltz1"] = "断于钢筋母材，脆性断裂";
                            break;
                        case 9:
                            sitem["dltz1"] = "断于焊缝，脆性断裂(焊口开裂)";
                            break;
                    }

                    switch (kl2)
                    {
                        case 1:
                            sitem["dltz2"] = "断于焊缝之外，延性断裂";
                            break;
                        case 2:
                            sitem["dltz2"] = "断于焊缝，延性断裂";
                            break;
                        case 3:
                            sitem["dltz2"] = "断于焊缝之外，脆性断裂";
                            break;
                        case 4:
                            sitem["dltz2"] = "断于焊缝，脆性断裂";
                            break;
                        case 5:
                            sitem["dltz2"] = "既断于热影响区又脆断";
                            break;
                        case 6:
                            sitem["dltz2"] = "断于热影响区，延性断裂";
                            break;
                        case 7:
                            sitem["dltz2"] = "断于钢筋母材，延性断裂";
                            break;
                        case 8:
                            sitem["dltz2"] = "断于钢筋母材，脆性断裂";
                            break;
                        case 9:
                            sitem["dltz2"] = "断于焊缝，脆性断裂(焊口开裂)";
                            break;
                    }

                    switch (kl3)
                    {
                        case 1:
                            sitem["dltz3"] = "断于焊缝之外，延性断裂";
                            break;
                        case 2:
                            sitem["dltz3"] = "断于焊缝，延性断裂";
                            break;
                        case 3:
                            sitem["dltz3"] = "断于焊缝之外，脆性断裂";
                            break;
                        case 4:
                            sitem["dltz3"] = "断于焊缝，脆性断裂";
                            break;
                        case 5:
                            sitem["dltz3"] = "既断于热影响区又脆断";
                            break;
                        case 6:
                            sitem["dltz3"] = "断于热影响区，延性断裂";
                            break;
                        case 7:
                            sitem["dltz3"] = "断于钢筋母材，延性断裂";
                            break;
                        case 8:
                            sitem["dltz3"] = "断于钢筋母材，脆性断裂";
                            break;
                        case 9:
                            sitem["dltz3"] = "断于焊缝，脆性断裂(焊口开裂)";
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
                    #region 初始化
                    sitem["jcjg_ls"] = "----";
                    sitem["dkj1"] = "-1";
                    sitem["dkj2"] = "-1";
                    sitem["dkj3"] = "-1";

                    sitem["jcjg_lw"] = "----";
                    sitem["lw1"] = "-1";
                    sitem["lw2"] = "-1";
                    sitem["lw3"] = "-1";
                    sitem["hg_lw"] = "0";
                    #endregion
                    switch (jcxm)
                    {
                        #region 拉伸、抗拉强度
                        case "拉伸":
                        case "抗拉强度":

                            if (kl1 >= mKlqd && kl2 >= mKlqd && (kj1 == 7 || kj1 == 6) && (kj2 == 7 || kj2 == 6) && kl3 >= mKlqd && (kj3 == 7 || kj3 == 6) || (kl1 >= mKlqd && (kj1 == 7 || kj1 == 6) &&
                                kl2 >= mKlqd && (kj2 == 7 || kj2 == 6) && kl3 >= mKlqd && (kj3 == 4 || kj3 == 5))
                                || (kl3 >= mKlqd && (kj3 == 7 || kj3 == 6) && kl2 >= mKlqd && (kj2 == 7 || kj2 == 6) && kl1 >= mKlqd && (kj1 == 4 || kj1 == 5))
                                || (kl1 >= mKlqd && (kj1 == 7 || kj1 == 6) && kl3 >= mKlqd && (kj3 == 7 || kj3 == 6) && kl2 >= mKlqd && (kj2 == 4 || kj2 == 5)))
                            {
                                sitem["jcjg_ls"] = "符合";
                                mFlag_Hg = true;
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
                                    sitem["jcjg_ls"] = "复试";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    if ((kj1 == 8 || kj2 == 8 || kj3 == 8) || (kl1 < mKlqd && kj1 == 7) || (kl2 < mKlqd && kj2 == 7) || (kl3 < mKlqd && kj3 == 7))
                                    {
                                        sitem["jcjg_ls"] = "无效";
                                        mFlag_Bhg = true;
                                    }
                                    else
                                    {
                                        sitem["jcjg_ls"] = "不符合";
                                        mFlag_Bhg = true;
                                    }

                                }

                            }
                            break;
                        #endregion
                        #region 冷弯
                        case "冷弯":
                            int Gs = 0;
                            //if (GetSafeInt(MItem[index]["jydbh"]) > 150700000 || GetSafeInt(MItem[index]["jydbh"]) == 150500270)
                            //{
                            for (int i = 1; i < 4; i++)
                            {
                                if (GetSafeInt(sitem["lw" + i]) >= 0.5)
                                {
                                    Gs = Gs + 1;
                                }
                            }
                            if (Gs <= 1)
                            {
                                sitem["jcjg_lw"] = "符合";
                                mFlag_Hg = true;
                            }
                            else if (Gs == 2)
                            {
                                sitem["jcjg_lw"] = "复试";
                                mFlag_Bhg = true;
                            }
                            else
                            {
                                sitem["jcjg_lw"] = "不符合";
                                mFlag_Bhg = true;
                            }
                            //}
                            //else
                            //{
                            if (GetSafeInt(sitem["hg_lw"]) >= mHggs_lw)
                            {
                                sitem["jcjg_lw"] = "符合";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                if (GetSafeInt(sitem["hg_lw"]) > mFsgs_lw)
                                {
                                    sitem["jcjg_lw"] = "复试";
                                    mFlag_Bhg = true;
                                }
                                else
                                {
                                    sitem["jcjg_lw"] = "不符合";
                                    mFlag_Bhg = true;
                                }
                            }
                            //}
                            break;
                            #endregion
                    }
                    if ((sitem["jcjg_ls"] == "符合" || sitem["jcjg_ls"] == "----") && (sitem["jcjg_lw"] == "符合" || sitem["jcjg_lw"] == "----"))
                    {
                        //sitem["jcjg"] = "合格";
                    }
                    else
                    {
                        //sitem["jcjg"] = "不合格";
                        mbhggs++;
                    }
                    //}
                    #region//VB中代码，上下文没用到，暂时不管
                    //else
                    //{
                    //    mallbhg_kl = mallbhg_kl + find_singlezb_bhg(mrsmainTable, mrssubTable, "kl", mKlqd, (mxlgs))
                    //    mallbhg_sc = mallbhg_sc + find_dkj_bhg(mrsmainTable, mrssubTable, mScl, (mxlgs))
                    //}
                    #endregion
                    #region//开始判定单项指标是否合格,根据单项指标再判定单组结论是否合格
                    //    if (sitem["jcjg"] == "合格")
                    //    {
                    //        MItem[index]["fjjj3"] = MItem[index]["fjjj3"] + mZh.ToString().Trim() + "#";
                    //    }
                    //    if (sitem["jcjg"] == "不合格")
                    //    {
                    //        //if (string.Equals(MItem[index]["pdbz"], "18-2012"))
                    //        {
                    //            if (sitem["jcjg_ls"] == "不符合" || sitem["jcjg_lw"] == "不符合")
                    //            {
                    //                sitem["jcjg"] = "不符合";
                    //                MItem[index]["fjjj2"] = MItem[index]["fjjj2"] + mZh.ToString().Trim() + "#";
                    //            }
                    //            else
                    //            {
                    //                if (sitem["jcjg_ls"] == "无效")
                    //                {
                    //                    mwxzh = mwxzh + mZh.ToString().Trim() + "#";
                    //                }
                    //                else
                    //                {
                    //                    if (sitem["jcjg_ls"] == "复试" || sitem["jcjg_lw"] == "复试")
                    //                    {
                    //                        sitem["jcjg"] = "复试";
                    //                        MItem[index]["fjjj1"] = MItem[index]["fjjj1"] + mZh.ToString().Trim() + "#";
                    //                    }
                    //                }
                    //            }
                    //        //}
                    //    }
                    #endregion
                    //}
                    if (mbhggs == 0)
                    {
                        XQData[index]["JCJG"] = "合格";
                        XQData[index]["JCJGMS"] = "该组试样所检项目符合{}" + "标准要求。";
                        //MItem[index]["JSBEIZHU"] = "该组试样所检项目符合" + MItem[index]["PDBZ"] + "标准要求。";
                        //MItem[index]["JCJG"] = "合格";
                    }
                    else
                    {
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "该组试样不符合{}" + "标准要求。";
                        //MItem[index]["JSBEIZHU"] = "该组试样不符合" + MItem[index]["PDBZ"] + "标准要求。";
                        //MItem[index]["JCJG"] = "不合格";
                        //if (mFlag_Bhg && mFlag_Hg)
                        //{
                        //    XQData[index]["JCJGMS"] = "该组试样所检项目部分符合{}" + "标准要求。";
                        //    //MItem[index]["JSBEIZHU"] = "该组试样所检项目部分符合" + MItem[index]["PDBZ"] + "标准要求。";
                        //}
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
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
