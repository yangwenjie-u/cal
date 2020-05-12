using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class GLJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/

            #region  计算开始
            #region  参数定义
            int mKlqd, mScl, mLw;
            int mHggs_klqd, mHggs_scl, mHggs_lw, mxlgs, mxwgs, mZh;
            int mFsgs_klqd, mFsgs_scl, mFsgs_lw;
            int mLwzj, mLwjd;
            string LwBzyq = "", SclBzyq = "";

            int mCnt_FjHg, mCnt_FjHg1;
            bool mAllHg;
            bool mFlag_Hg, mFlag_Bhg;
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";

            mAllHg = true;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            #endregion

            #region  自定义函数

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

            //求屈服强度及抗拉强度(无返回值)
            Action<IDictionary<string, string>, int> calc_kl =
                delegate (IDictionary<string, string> sitem, int count)
                {
                    double mMidVal;
                    string mMj, mkl;
                    if (string.IsNullOrEmpty(sitem["ZJ"]))
                        sitem["ZJ"] = "0";
                    mMidVal = (GetSafeDouble(sitem["ZJ"]) / 2) * (GetSafeDouble(sitem["ZJ"]) / 2);
                    mMj = (3.14159 * mMidVal).ToString();
                    if (sitem["SJDJ"].Trim().Contains("冷轧带肋"))
                        mMj = Round(Conversion.Val(mMj), 1).ToString("0.0");
                    else
                        mMj = mYxsz(GetSafeDouble(mMj), 4);



                    if (sitem["SJDJ"].Trim().Contains("冷轧扭"))
                    {
                        string zj = sitem["ZJ"];
                        switch (sitem["SJDJ"].Trim())
                        {
                            case "冷轧扭CTB550Ⅰ":
                                switch (zj)
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
                                switch (zj)
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
                                switch (zj)
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
                                switch (zj)
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
                    sitem["MJ"] = mMj;
                    if (Math.Abs(Conversion.Val(mMj) - 0) > 0.00001)
                    {
                        for (int i = 1; i <= count; i++)
                        {
                            if (string.IsNullOrEmpty(sitem["KLHZ" + i]))
                                sitem["KLHZ" + i] = "0";
                            mkl = (1000 * Conversion.Val(sitem["KLHZ" + i]) / Conversion.Val(mMj)).ToString();
                            if (Conversion.Val(mkl) <= 200)
                                sitem["KLQD" + i] = Round(Conversion.Val(mkl), 0).ToString();
                            if (Conversion.Val(mkl) > 200 && Conversion.Val(mkl) <= 1000)
                                sitem["KLQD" + i] = (Round(Conversion.Val(mkl) / 5, 0) * 5).ToString();
                            if (Conversion.Val(mkl) > 1000)
                                sitem["KLQD" + i] = (Round(Conversion.Val(mkl) / 10, 0) * 10).ToString();

                        }
                    }
                    else
                    {
                        for (int i = 1; i <= count; i++)
                            sitem["KLQD" + i] = "0";
                    }
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_GLJ_DJ"];
            var mrsbxdj = dataExtra["BZ_GLJXBDJ"];
            var MItem = data["M_GLJ"];
            var SItem = data["S_GLJ"];
            var ggph = "";//钢筋牌号
            #endregion

            //循环从表
            foreach (var sitem in SItem)
            {
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                ggph = sitem["GCLX_PH"];

                mCnt_FjHg = 0; //记录复检合格的组数
                mCnt_FjHg1 = 0;
                MItem[0]["FJJJ1"] = "";
                MItem[0]["FJJJ2"] = "";
                MItem[0]["FJJJ3"] = "";
                sitem["FJ"] = "0";
                string mgjlb = sitem["GJLB"];//设计等级名称
                if (mgjlb == null)
                {
                    mgjlb = "";
                }
                if (mrsbxdj != null)
                {
                    foreach (var bxitem in mrsbxdj)
                    {
                        if (sitem["GCLX_JB"] == bxitem["MC"] && IsQualified(bxitem["ZJFW"], sitem["ZJ"]) == "合格")
                        {
                            sitem["G_DXLS"] = bxitem["DXLS"];
                            sitem["G_ZDLZSCL"] = bxitem["ZDLZSCL"];
                            sitem["G_GYLFFLY"] = bxitem["GYLFFLY"];
                            sitem["G_DBXFFLY4"] = bxitem["DBXFFLY4"];
                            sitem["G_DBXFFLY8"] = bxitem["DBXFFLY8"];
                        }
                    }
                }
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = mrsDj.FirstOrDefault(u => u["PH"].Contains(sitem["GCLX_PH"]) && u["GJLB"].Contains(mgjlb.Trim()) && u["JB"].Contains(sitem["GCLX_JB"]) && u["YSLX"].Contains(sitem["YSLX"]));

                if (null == extraFieldsDj)
                {
                    sitem["JCJG"] = "依据不详";
                    continue;
                }
                else
                {
                    sitem["SJDJ"] = extraFieldsDj["MC"];
                    sitem["G_JXLJ"] = extraFieldsDj["JXLJBZ"].Trim();
                    sitem["G_QFQD"] = extraFieldsDj["QFQDBZZ"];
                    string mqfqd = extraFieldsDj["QFQDBZZ"];
                    mKlqd = (int)Conversion.Val(extraFieldsDj["KLQDBZZ"]);//单组标准值
                    mScl = (int)Conversion.Val(extraFieldsDj["SCLBZZ"]);
                    mLw = (int)Conversion.Val(extraFieldsDj["LWBZZ"]);
                    mLwjd = (int)Conversion.Val(extraFieldsDj["LWJD"]);//冷弯角度和冷弯直径
                    mLwzj = (int)Conversion.Val(extraFieldsDj["LWZJ"]);

                    mHggs_klqd = (int)Conversion.Val(extraFieldsDj["ZHGGS_KLQD"]); //单组合格个数
                    mHggs_scl = (int)Conversion.Val(extraFieldsDj["ZHGGS_SCL"]);
                    mHggs_lw = (int)Conversion.Val(extraFieldsDj["ZHGGS_LW"]);
                    mFsgs_klqd = (int)Conversion.Val(extraFieldsDj["ZFSGS_KLQD"]);
                    mFsgs_scl = (int)Conversion.Val(extraFieldsDj["ZFSGS_SCL"]);
                    mFsgs_lw = (int)Conversion.Val(extraFieldsDj["ZFSGS_LW"]);
                    mLwzj = (int)Conversion.Val(extraFieldsDj["LWZJ"]); //冷弯直径和角度
                    mLwjd = (int)Conversion.Val(extraFieldsDj["LWJD"]);
                    mxlgs = (int)Conversion.Val(extraFieldsDj["XLGS"]);
                    mxwgs = (int)Conversion.Val(extraFieldsDj["XWGS"]);
                }
                sitem["G_KLQD"] = mKlqd.ToString();
                sitem["G_DLWZ"] = SclBzyq;
                sitem["G_LWWZ"] = LwBzyq;

                //求抗拉强度
                //求抗拉强度,断口,冷弯 合格个数,并且返回值为不同组不合格数的累加值
                int mcnt = 0;
                int this_bhg = 0;
                string bhggsbj = "";
                if (sitem["YPSL"].Trim() == "2根")
                {
                    mxlgs = 2;
                    sitem["KLHZ3"] = "----";
                    sitem["DKJ3"] = "----";
                    sitem["KLQD3"] = "----";
                    sitem["DXLS3"] = "----";
                    sitem["ZDLZSCL3"] = "----";
                    sitem["GYLFFLY3"] = "----";
                    sitem["DBXLY4_3"] = "----";
                    sitem["DBXLY8_3"] = "----";
                }
                //求屈服强度及抗拉强度(自定义函数)
                calc_kl(sitem, mxlgs);
                if (extraFieldsDj["JB"].Contains("Ⅰ"))
                {
                    mcnt = 0;
                    sitem["G_JXLJ"] = "钢筋拉断时，接头试件实际抗拉强度大于等于钢筋抗拉强度标准值；连接件破坏时，接头试件实际抗拉强度大于等于1.10倍钢筋抗拉强度标准值";
                    for (int i = 1; i <= mxlgs; i++)
                    {
                        if (sitem["DKJ" + i] == "1" || sitem["DKJ" + i] == "2" || sitem["DKJ" + i] == "3")
                        {
                            if (Conversion.Val(sitem["KLQD" + i]) >= GetSafeDouble(extraFieldsDj["KLQDBZZ"]))
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
                            if (Conversion.Val(sitem["KLQD" + i]) >= GetSafeDouble(extraFieldsDj["KLQDBZZ"]) * 1.1)
                            {
                                mcnt++;
                            }
                            else
                            {
                                bhggsbj = bhggsbj + i;
                            }

                        }
                    }
                    sitem["HG_KL"] = mcnt.ToString();
                }
                if (extraFieldsDj["JB"].Contains("Ⅱ"))
                {
                    for (int i = 1; i <= mxlgs; i++)
                    {
                        if (Conversion.Val(sitem["KLQD" + i]) >= GetSafeDouble(extraFieldsDj["QFQDBZZ"]))
                        {
                            mcnt++;
                        }
                        else
                        {
                            bhggsbj = bhggsbj + i;
                        }
                    }
                    sitem["HG_KL"] = mcnt.ToString();
                }
                if (extraFieldsDj["JB"].Contains("Ⅲ"))
                {
                    for (int i = 1; i <= mxlgs; i++)
                    {
                        if (Conversion.Val(sitem["KLQD" + i]) >= GetSafeDouble(extraFieldsDj["QFQDBZZ"]) * 1.25)
                        {
                            mcnt++;
                        }
                        else
                        {
                            bhggsbj = bhggsbj + i;
                        }

                    }
                    sitem["HG_KL"] = mcnt.ToString();
                }
                //拉伸
                if (jcxm.Contains("、拉伸、"))
                {
                    jcxmCur = "拉伸";

                    if (bhggsbj == "")
                    {
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains("拉伸") ? "" : "拉伸" + "、";
                    }
                }
                int mbxbhgs = 0;
                int mbxhg = 0;
                //单向拉伸残余形变
                if (jcxm.Contains("、单向拉伸残余形变、"))
                {
                    jcxmCur = "单向拉伸残余形变";
                    if (mxlgs == 2)
                    {
                        sitem["DXLS"] = Math.Round((Conversion.Val(sitem["DXLS1"]) + Conversion.Val(sitem["DXLS2"])) / 2, 2).ToString("0.00");
                    }
                    else
                    {
                        sitem["DXLS"] = Math.Round((Conversion.Val(sitem["DXLS1"]) + Conversion.Val(sitem["DXLS2"]) + Conversion.Val(sitem["DXLS3"])) / 3, 2).ToString("0.00");
                    }
                    if (IsQualified(sitem["G_DXLS"], sitem["DXLS"]) == "合格")
                    {
                        mbxhg = mbxhg + 1;
                        sitem["JCJG_DXLS"] = "符合";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbxbhgs = mbxbhgs + 1;
                        sitem["JCJG_DXLS"] = "不符合";
                        mFlag_Bhg = true;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sitem["DXLS"] = "----";
                    sitem["JCJG_DXLS"] = "----";
                }

                //最大力总伸长率
                if (jcxm.Contains("、最大力总伸长率、"))
                {
                    jcxmCur = "最大力总伸长率";
                    if (mxlgs == 2)
                    {
                        sitem["ZDLZSCL"] = Math.Round((Conversion.Val(sitem["ZDLZSCL1"]) + Conversion.Val(sitem["ZDLZSCL2"])) / 2, 1).ToString("0.0");
                    }
                    else
                    {
                        sitem["ZDLZSCL"] = Math.Round((Conversion.Val(sitem["ZDLZSCL1"]) + Conversion.Val(sitem["ZDLZSCL2"]) + Conversion.Val(sitem["ZDLZSCL3"])) / 3, 1).ToString("0.0");
                    }
                    if (IsQualified(sitem["G_ZDLZSCL"], sitem["ZDLZSCL"]) == "合格")
                    {
                        mbxhg = mbxhg + 1;
                        sitem["JCJG_ZSCL"] = "符合";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbxbhgs = mbxbhgs + 1;
                        sitem["JCJG_ZSCL"] = "不符合";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sitem["JCJG_ZSCL"] = "----";
                    sitem["ZDLZSCL"] = "----";
                }

                //高应力反复拉压残余形变
                if (jcxm.Contains("、高应力反复拉压残余形变、"))
                {
                    jcxmCur = "高应力反复拉压残余形变";
                    if (mxlgs == 2)
                    {
                        sitem["GYLFFLY"] = Math.Round((Conversion.Val(sitem["GYLFFLY1"]) + Conversion.Val(sitem["GYLFFLY2"])) / 2, 1).ToString("0.0");
                    }
                    else
                    {
                        sitem["GYLFFLY"] = Math.Round((Conversion.Val(sitem["GYLFFLY1"]) + Conversion.Val(sitem["GYLFFLY2"]) + Conversion.Val(sitem["GYLFFLY3"])) / 3, 1).ToString("0.0");
                    }
                    if (IsQualified(sitem["G_GYLFFLY"], sitem["GYLFFLY"]) == "合格")
                    {
                        mbxhg = mbxhg + 1;
                        sitem["JCJG_GYL"] = "符合";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbxbhgs = mbxbhgs + 1;
                        sitem["JCJG_GYL"] = "不符合";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sitem["JCJG_GYL"] = "----";
                    sitem["GYLFFLY"] = "----";
                }
                //大变形反复拉压残余形变
                if (jcxm.Contains("大变形反复拉压残余形变"))
                {
                    jcxmCur = "大变形反复拉压残余形变";
                    if (mxlgs == 2)
                    {
                        sitem["DBXFFLY4"] = Math.Round((Conversion.Val(sitem["DBXLY4_1"]) + Conversion.Val(sitem["DBXLY4_2"])) / 2, 1).ToString();
                        sitem["DBXFFLY8"] = Math.Round((Conversion.Val(sitem["DBXLY8_1"]) + Conversion.Val(sitem["DBXLY8_2"])) / 2, 1).ToString();
                    }
                    else
                    {
                        sitem["DBXFFLY4"] = Math.Round((Conversion.Val(sitem["DBXLY4_1"]) + Conversion.Val(sitem["DBXLY4_2"]) + Conversion.Val(sitem["DBXLY4_3"])) / 3, 1).ToString();
                        sitem["DBXFFLY8"] = Math.Round((Conversion.Val(sitem["DBXLY8_1"]) + Conversion.Val(sitem["DBXLY8_2"]) + Conversion.Val(sitem["DBXLY8_3"])) / 3, 1).ToString();
                    }
                    if (sitem["G_DBXFFLY8"] != "----")
                    {
                        if ((IsQualified(sitem["G_DBXFFLY4"], sitem["DBXFFLY4"]) == "合格") && (IsQualified(sitem["G_DBXFFLY8"], sitem["DBXFFLY8"]) == "合格"))
                        {
                            mbxhg = mbxhg + 1;
                            sitem["JCJG_DBX"] = "符合";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mbxbhgs = mbxbhgs + 1;
                            sitem["JCJG_DBX"] = "不符合";
                            mFlag_Bhg = true;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        if (IsQualified(sitem["G_DBXFFLY4"], sitem["DBXFFLY4"]) == "合格")
                        {
                            mbxhg = mbxhg + 1;
                            sitem["JCJG_DBX"] = "符合";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mbxbhgs = mbxbhgs + 1;
                            sitem["JCJG_DBX"] = "不符合";
                            mFlag_Bhg = true;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                else
                {
                    sitem["DBXFFLY4"] = "----";
                    sitem["DBXFFLY8"] = "----";
                    sitem["JCJG_DBX"] = "----";
                }
                //-----------------------单组检测结果判定------------------------------------------
                this_bhg = 0;
                for (int i = 0; i < mxlgs; i++)
                {
                    if (bhggsbj.Trim().Contains(i.ToString()))
                    {
                        this_bhg++;
                        jcxmBhg += jcxmBhg.Contains("拉伸") ? "" : "拉伸" + "、";
                    }
                }
                if (mbxbhgs == 0 && mbxhg == 0)
                {
                    sitem["JCJG_BX"] = "----";
                }
                if (mbxbhgs == 0 && mbxhg > 0)
                {
                    sitem["JCJG_BX"] = "符合";
                    mFlag_Hg = true;
                }
                if (mbxbhgs > 0)
                {
                    sitem["JCJG_BX"] = "不符合";
                    mFlag_Bhg = true;
                }
                if (sitem["JCJG_BX"] == "符合" || sitem["JCJG_BX"] == "----")
                {
                    if (this_bhg == 0)
                    {
                        sitem["JCJG"] = "合格";
                        MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + "1#";
                    }
                    if (this_bhg >= 2)
                    {
                        sitem["JCJG"] = "不合格";
                        MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + "1#";
                    }
                    if (this_bhg == 1)
                    {
                        sitem["JCJG"] = "不合格";
                        MItem[0]["FJJJ1"] = MItem[0]["FJJJ1"] + "1#";
                    }

                }
                if (sitem["JCJG_BX"] == "不符合")
                {
                    if (this_bhg == 0)
                    {
                        sitem["JCJG"] = "不合格";
                        MItem[0]["FJJJ1"] = MItem[0]["FJJJ1"] + "1#";
                    }
                    else
                    {
                        sitem["JCJG"] = "不合格";
                        MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + "1#";
                    }
                }
                mAllHg = mAllHg && (sitem["JCJG"] == "合格");
            }

            //综合判断
            string mjgsm = string.Empty;
            if (mAllHg)
            {
                mjgsm = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合" + ggph + "要求。";
                MItem[0]["JCJG"] = "合格";
            }
            else
                MItem[0]["JCJG"] = "不合格";

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"].Trim()))
            {
                mjgsm = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + ggph + "要求。";
                MItem[0]["FJJJ3"] = mjgsm;
            }
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"].Trim()))
            {
                mjgsm = "依据" + MItem[0]["PDBZ"] + "的规定，，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + ggph + "要求。";
                MItem[0]["FJJJ2"] = mjgsm;

            }
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ1"].Trim()))
            {
                mjgsm = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + ggph + "要求，需要复试。";
                MItem[0]["FJJJ1"] = mjgsm;
                if (mFlag_Bhg && mFlag_Hg)
                {
                    MItem[0]["FJJJ1"] = mjgsm;
                }
            }
            MItem[0]["JCJGMS"] = MItem[0]["FJJJ3"] + MItem[0]["FJJJ2"] + MItem[0]["FJJJ1"];
            #endregion
            /************************ 代码结束 *********************/
        }

    }
}
