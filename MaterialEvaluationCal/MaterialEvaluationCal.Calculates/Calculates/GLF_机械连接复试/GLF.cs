using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GLF : BaseMethods
    {
        public void Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_GLF_DJ"];
            var extraXBDJ = dataExtra["BZ_GLJ_XBDJ"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();

            bool mItemHg = true;//每一条检测项合格标识
            string mJSFF = "";
            int mbhggs = 0;//不合格数量
            bool mFlag_Bhg = true;
            bool mFlag_Hg = true;
            string mgjlb = "";
            int mZh, mLwzj, mLwjd = 0;
            int mKlqd, mScl, mLw = 0;
            int mxlgs, mxwgs = 0;
            int mHggs_klqd, mHggs_scl, mHggs_lw = 0;
            int mFsgs_klqd, mFsgs_scl, mFsgs_lw = 0;
            int this_bhg = 0;
            string bhggsbj = "";
            int mcnt = 0;
            int mbxbhgs = 0; //不合格数量
            int mbxhg = 0;
            string SclBzyq = "";
            string LwBzyq = "";
            foreach (var jcxm in jcxmItems)
            {
                if (jcxm.ToUpper().Contains("BGJG"))
                {
                    continue;
                }

                //mItemHg = true;
                var SItem = retData[jcxm]["S_GLF"];
                //var MItem = retData[jcxm]["M_GLF"];
                var XQData = retData[jcxm]["S_BY_RW_XQ"];
                int index = 0;
                //MItem[index]["fjjj1"] = "";
                //MItem[index]["fjjj2"] = "";
                //MItem[index]["fjjj3"] = "";

                foreach (var sItem in SItem)
                {
                    #region
                    //XQData[0]["RECID"] = item["RECID"];
                    //XQData[0]["SJWCJSSJ"] = DateTime.Now.ToString();

                    //if (null == MItem)
                    //{
                    //    mItemHg = false;
                    //    XQData[index]["JCJG"] = "不合格";
                    //    XQData[index]["JCJGMS"] = "获取不到主表数据";
                    //    mbxbhgs = mbxbhgs + 1;
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
                    #endregion
                    sItem["FJ"] = "false";
                    //mZh = GetSafeInt(sitem["zh"]);
                    mgjlb = sItem["GJLB"];//设计等级名称
                    if (mgjlb == null)
                    {
                        mgjlb = "";
                    }

                    var extraFieldsXbdj = extraXBDJ.FirstOrDefault();
                    if (extraFieldsXbdj != null)
                    {
                        if (sItem["GCLX_JB"] == extraFieldsXbdj["MC"] && (IsQualified(extraFieldsXbdj["ZJFW"], sItem["ZJ"]) == "不符合" ? false : true))
                        {
                            sItem["g_DXLS"] = extraFieldsXbdj["DXLS"];
                            sItem["g_ZDLZSCL"] = extraFieldsXbdj["ZDLZSCL"];
                            sItem["g_GYLFFLY"] = extraFieldsXbdj["GYLFFLY"];
                            sItem["g_DBXFFLY4"] = extraFieldsXbdj["DBXFFLY4"];
                            sItem["g_DBXFFLY8"] = extraFieldsXbdj["DBXFFLY8"];
                        }
                    }
                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u.Keys.Contains("PH")
                    && u.Values.Contains(sItem["GCLX_PH"].ToString().Trim())
                    && u.Keys.Contains("GJLB") && u.Values.Contains(mgjlb.ToString().Trim())
                    && u.Keys.Contains("JB") && u.Values.Contains(sItem["GCLX_JB"].ToString().Trim())
                    && u.Keys.Contains("YSLX") && u.Values.Contains(sItem["YSLX"].ToString().Trim()));
                    if (null == extraFieldsDj)
                    {
                        sItem["JCJG"] = "依据不详";
                        mJSFF = "";
                        //MItem[index]["BGBH"] = "";
                        //MItem[index]["JSBEIZHU"] = MItem[index]["JSBEIZHU"] + "单组流水号" + item["DZBH"] + "试件尺寸为空";
                        mItemHg = false;
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "获取实验人信息失败";
                        mbxbhgs = mbxbhgs + 1;
                        mFlag_Bhg = true;
                        index++;

                        continue;
                    }
                    else
                    {
                        sItem["SJDJ"] = extraFieldsDj["MC"];
                        sItem["G_JXLJ"] = extraFieldsDj["JXLJBZ"].Trim();
                        sItem["G_QFQD"] = extraFieldsDj["QFQDBZZ"];
                        //sitem["BGNAME"] = extraFieldsDj["BGNAME"].Trim();
                        //mqfqd = mrsDj!qfQDBZZ
                        mKlqd = GetSafeInt(extraFieldsDj["KLQDBZZ"]);//单组标准值
                        mScl = GetSafeInt(extraFieldsDj["SCLBZZ"]);
                        mLw = GetSafeInt(extraFieldsDj["LWBZZ"]);

                        mLwjd = GetSafeInt(extraFieldsDj["LWJD"]);//冷弯角度和冷弯直径
                        mLwzj = GetSafeInt(extraFieldsDj["LWZJ"]);

                        mHggs_klqd = GetSafeInt(extraFieldsDj["ZHGGS_KLQD"]); //单组合格个数
                        mHggs_scl = GetSafeInt(extraFieldsDj["ZHGGS_SCL"]);
                        mHggs_lw = GetSafeInt(extraFieldsDj["ZHGGS_LW"]);
                        mFsgs_klqd = GetSafeInt(extraFieldsDj["ZFSGS_KLQD"]);
                        mFsgs_scl = GetSafeInt(extraFieldsDj["ZFSGS_SCL"]);
                        mFsgs_lw = GetSafeInt(extraFieldsDj["ZFSGS_LW"]);
                        mLwzj = GetSafeInt(extraFieldsDj["LWZJ"]); //冷弯直径和角度
                        mLwjd = GetSafeInt(extraFieldsDj["LWJD"]);
                        mxlgs = GetSafeInt(extraFieldsDj["XLGS"]);
                        mxwgs = GetSafeInt(extraFieldsDj["XWGS"]);
                        //MItem[index]["which"] = extraFieldsDj["which"];
                        //if (MItem[index]["which"] == "")
                        //{
                        //    MItem[index]["which"] = "0";
                        //}
                        mJSFF = extraFieldsDj["XWGS"] == null ? "" : extraFieldsDj["JSFF"];
                    }

                    sItem["G_KLQD"] = mKlqd.ToString();
                    sItem["G_DLWZ"] = SclBzyq;
                    sItem["G_LWWZ"] = LwBzyq;

                    //    '求抗拉强度,断口,冷弯 合格个数,并且返回值为不同组不合格数的累加值
                    if (sItem["YPSL"].Trim() == "2根")
                    {
                        mxlgs = 2;
                    }

                    switch (extraFieldsDj["JB"])
                    #region jb
                    {
                        case "I":
                            sItem["G_JXLJ"] = "钢筋拉断时，接头试件实际抗拉强度大于等于钢筋抗拉强度标准值；连接件破坏时，接头试件实际抗拉强度大于等于1.10倍钢筋抗拉强度标准值";
                            for (int i = 1; i <= mxlgs; i++)
                            {
                                if (GetSafeInt(sItem["DKJ" + i]) == 1 || GetSafeInt(sItem["DKJ" + i]) == 2)
                                {
                                    if (GetSafeInt(sItem["KLQD" + i]) >= GetSafeInt(extraFieldsDj["KLQDBZZ"]))
                                    {
                                        mcnt++;
                                    }
                                    else
                                    {
                                        bhggsbj = bhggsbj + i;
                                    }
                                }
                                else
                                {
                                    if (GetSafeInt(sItem["KLQD" + i]) >= Math.Round(GetSafeInt(extraFieldsDj["KLQDBZZ"]) * 1.1))
                                    {
                                        mcnt++;
                                    }
                                    else
                                    {
                                        bhggsbj = bhggsbj + i;
                                    }

                                }
                            }
                            sItem["HG_KL"] = mcnt.ToString();
                            break;
                        case "Ⅱ":
                            for (int i = 1; i <= mxlgs; i++)
                            {
                                if (GetSafeInt(sItem["KLQD" + i]) >= GetSafeInt(extraFieldsDj["QFQDBZZ"]))
                                {
                                    mcnt++;
                                }
                                else
                                {
                                    bhggsbj = bhggsbj + i;
                                }

                            }
                            sItem["HG_KL"] = mcnt.ToString();

                            break;
                        case "Ⅲ":
                            for (int i = 1; i <= mxlgs; i++)
                            {
                                if (GetSafeInt(sItem["KLQD" + i]) >= Math.Round(GetSafeInt(extraFieldsDj["QFQDBZZ"]) * 1.35))
                                {
                                    mcnt++;
                                }
                                else
                                {
                                    bhggsbj = bhggsbj + i;
                                }

                            }
                            sItem["HG_KL"] = mcnt.ToString();
                            break;
                    }
                    #endregion

                    //初始化
                    sItem["DXLS"] = "----";
                    sItem["JCJG_DXLS"] = "----";
                    sItem["JCJG_ZSCL"] = "----";
                    sItem["ZDLZSCL"] = "----";
                    sItem["JCJG_GYL"] = "----";
                    sItem["GYLFFLY"] = "----";
                    sItem["DBXFFLY4"] = "----";
                    sItem["DBXFFLY8"] = "----";
                    sItem["JCJG_DBX"] = "----";

                    switch (jcxm)
                    {
                        #region 单向拉伸残余形变
                        case "单向拉伸残余形变":
                            sItem["DXLS"] = Math.Round((GetSafeDouble(sItem["DXLS1"]) + GetSafeDouble(sItem["DXLS2"]) + GetSafeDouble(sItem["DXLS3"])) / 3, 2).ToString();
                            if (IsQualified(sItem["G_DXLS"], sItem["DXLS"]) == "不符合")
                            {
                                mbxbhgs = mbxbhgs + 1;
                                sItem["JCJG_DXLS"] = "不符合";
                            }
                            else
                            {
                                mbxhg = mbxhg + 1;
                                sItem["JCJG_DXLS"] = "符合";
                            }
                            break;
                        #endregion

                        #region 最大力总伸长率
                        case "最大力总伸长率":
                            sItem["ZDLZSCL"] = Math.Round((GetSafeDouble(sItem["ZDLZSCL1"]) + GetSafeDouble(sItem["ZDLZSCL2"]) + GetSafeDouble(sItem["ZDLZSCL3"])) / 3, 1).ToString();
                            if (IsQualified(sItem["g_ZDLZSCL"], sItem["ZDLZSCL"]) == "不符合")
                            {
                                mbxbhgs = mbxbhgs + 1;
                                sItem["JCJG_ZSCL"] = "不符合";
                            }
                            else
                            {
                                mbxhg = mbxhg + 1;
                                sItem["JCJG_ZSCL"] = "符合";
                            }
                            break;
                        #endregion

                        #region 高应力反复拉压残余形变
                        case "高应力反复拉压残余形变":
                            sItem["GYLFFLY"] = Math.Round((GetSafeDouble(sItem["GYLFFLY1"]) + GetSafeDouble(sItem["GYLFFLY2"]) + GetSafeDouble(sItem["GYLFFLY3"])) / 3, 1).ToString();
                            if (IsQualified(sItem["G_GYLFFLY"], sItem["GYLFFLY"]) == "不符合")
                            {
                                mbxbhgs = mbxbhgs + 1;
                                sItem["JCJG_GYL"] = "不符合";
                            }
                            else
                            {
                                mbxhg = mbxhg + 1;
                                sItem["JCJG_GYL"] = "符合";
                            }
                            break;
                        #endregion

                        #region 大变形反复拉压残余形变
                        case "大变形反复拉压残余形变":
                            sItem["DBXFFLY4"] = Math.Round((GetSafeDouble(sItem["DBXLY4_1"]) + GetSafeDouble(sItem["DBXLY4_2"]) + GetSafeDouble(sItem["DBXLY4_3"])) / 3, 1).ToString();
                            sItem["DBXFFLY8"] = Math.Round((GetSafeDouble(sItem["DBXLY8_1"]) + GetSafeDouble(sItem["DBXLY8_2"]) + GetSafeDouble(sItem["DBXLY8_3"])) / 3, 1).ToString();
                            if (sItem["g_DBXFFLY8"] != "----")
                            {
                                if ((IsQualified(sItem["G_DBXFFLY4"], sItem["DBXFFLY4"]) == "不符合") || (IsQualified(sItem["G_DBXFFLY8"], sItem["DBXFFLY8"]) == "不符合"))
                                {
                                    mbxbhgs = mbxbhgs + 1;
                                    sItem["JCJG_DBX"] = "不符合";
                                }
                                else
                                {
                                    mbxhg = mbxhg + 1;
                                    sItem["JCJG_DBX"] = "符合";
                                }
                            }
                            else
                            {
                                if (IsQualified(sItem["G_DBXFFLY4"], sItem["DBXFFLY4"]) == "不符合")
                                {
                                    mbxbhgs = mbxbhgs + 1;
                                    sItem["JCJG_DBX"] = "不符合";
                                }
                                else
                                {
                                    mbxhg = mbxhg + 1;
                                    sItem["JCJG_DBX"] = "符合";
                                }
                            }
                            break;
                            #endregion
                    }

                    //-----------------------单组检测结果判定------------------------------------------
                    for (int i = 0; i < mxlgs; i++)
                    {
                        if (string.Equals(bhggsbj, i.ToString()))
                        {
                            this_bhg++;
                        }
                    }
                    if (mbxbhgs == 0 && mbxhg == 0)
                    {
                        sItem["JCJG_BX"] = "----";
                    }
                    if (mbxbhgs == 0 && mbxhg > 0)
                    {
                        sItem["JCJG_BX"] = "符合";
                        mFlag_Hg = true;
                    }
                    if (mbxbhgs > 0)
                    {
                        sItem["JCJG_BX"] = "不符合";
                        mFlag_Bhg = true;
                    }
                    if (sItem["JCJG_BX"] == "符合" || sItem["JCJG_BX"] == "----")
                    {
                        if (this_bhg == 0)
                        {
                            sItem["JCJG"] = "合格";
                            //MItem[index]["fjjj3"] = MItem[index]["fjjj3"] + mZh.ToString().Trim() + "#";
                        }
                        if (this_bhg >= 2)
                        {
                            sItem["JCJG"] = "不合格";
                            //MItem[index]["fjjj2"] = MItem[index]["fjjj2"] + mZh.ToString().Trim() + "#";
                        }
                        if (this_bhg == 1)
                        {
                            sItem["JCJG"] = "不合格";
                            //MItem[index]["fjjj1"] = MItem[index]["fjjj1"] + mZh.ToString().Trim() + "#";
                        }

                    }
                    if (sItem["JCJG_BX"] == "不符合")
                    {
                        if (this_bhg == 0)
                        {
                            sItem["JCJG"] = "不合格";
                            //MItem[index]["fjjj1"] = MItem[index]["fjjj1"] + mZh.ToString().Trim() + "#";
                        }
                        else
                        {
                            sItem["JCJG"] = "不合格";
                            //MItem[index]["fjjj2"] = MItem[index]["fjjj2"] + mZh.ToString().Trim() + "#";
                        }
                    }
                    //每条
                    if (mbxbhgs == 0)
                    {
                        XQData[index]["JCJG"] = "合格";
                        XQData[index]["JCJGMS"] = "该组试样所检项目符合{}" + "标准要求。";
                        //MItem[0]["JSBEIZHU"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                        //MItem[0]["JCJG"] = "合格";
                    }
                    else
                    {
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "该组试样不符合{}" + "标准要求。";
                        //MItem[0]["JSBEIZHU"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                        //MItem[0]["JCJG"] = "不合格";
                        if (mFlag_Bhg && mFlag_Hg)
                        {
                            XQData[index]["JCJGMS"] = "该组试样所检项目部分符合{}" + "标准要求。";
                            //MItem[0]["JSBEIZHU"] = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                        }
                    }

                }
            }

            #region 添加最终报告

            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            if (mbxbhgs > 0)
            {
                //不合格
                bgjgDic.Add("JCJG", "不合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合不标准要求。" + err);
            }
            else
            {
                bgjgDic.Add("JCJG", "合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。" + err);
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
