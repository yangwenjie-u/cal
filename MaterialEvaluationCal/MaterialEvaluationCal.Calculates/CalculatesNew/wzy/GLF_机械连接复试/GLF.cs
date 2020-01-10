using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    class F_JGJ_107_2016 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh;

            string mSjdjbh, mSjdj;
            int mKlqd, mScl, mLw;
            int mHggs_klqd, mHggs_scl, mHggs_lw, mxlgs, mxwgs, mZh;
            int mFsgs_klqd, mFsgs_scl, mFsgs_lw;
            int mLwzj, mLwjd;
            string QdBzyq, LwBzyq = "", SclBzyq = "";
            int vp, mCnt_FjHg, mCnt_FjHg1;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs, mLsfs, mLwfs;
            bool mGetBgbh;
            bool mSFwc;
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_GLF_DJ"];
            var mrsbxdj = dataExtra["BZ_GLJXBDJ"];
            var MItem = data["M_GLF"];
            var SItem = data["S_GLF"];
            #endregion

            #region  自定义函数
            //以下为对用设计值的判定方式
            Func<string, string, string> calc_pd =
                delegate (string sj, string sc)
                {
                    sj = sj.Trim();
                    string calc_pd_ret = "";
                    bool isStart = false;
                    string tmpStr = string.Empty;
                    if (string.IsNullOrEmpty(sc))
                        sc = "";
                    for (int i = 1; i <= sc.Length; i++)
                    {
                        if (IsNumeric(sc.Substring(i - 1, 1)) || sc.Substring(i - 1, 1) == "." || sc.Substring(i - 1, 1) == "-")
                        {
                            isStart = true;
                            tmpStr = tmpStr + sc.Substring(i - 1, 1);
                        }
                        else
                        {
                            if (isStart == false && tmpStr != "")
                                break;
                        }
                    }
                    sc = tmpStr;
                    if (!IsNumeric(sc))
                    {
                        calc_pd_ret = "----";
                    }
                    else
                    {
                        double min_sjz, max_sjz, scz;
                        int length, dw;
                        if (sj.Contains("＞"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("＞") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains(">"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf(">") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("≥"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("≥") + 1;
                            min_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz > min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("＜"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("＜") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("<"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("<") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz < max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("≤"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("≤") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("="))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("=") + 1;
                            max_sjz = Conversion.Val(sj.Substring(dw, length - dw));
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz == max_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("～"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("～") + 1;
                            min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= min_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            min_sjz = Conversion.Val(tmpStr);
                            max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= max_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            max_sjz = Conversion.Val(tmpStr);
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
                        }
                        if (sj.Contains("~"))
                        {
                            length = sj.Length;
                            dw = sj.IndexOf("~") + 1;
                            min_sjz = GetSafeDouble(sj.Substring(0, dw - 1));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= min_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(min_sjz.ToString().Substring(i - 1, 1)) || min_sjz.ToString().Substring(i - 1, 1) == "." || min_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + min_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            min_sjz = Conversion.Val(tmpStr);
                            max_sjz = GetSafeDouble(sj.Substring(dw, length - dw));
                            isStart = false;
                            tmpStr = "";
                            for (int i = 1; i <= max_sjz.ToString().Length; i++)
                            {
                                if (IsNumeric(max_sjz.ToString().Substring(i - 1, 1)) || max_sjz.ToString().Substring(i - 1, 1) == "." || max_sjz.ToString().Substring(i - 1, 1) == "-")
                                {
                                    isStart = true;
                                    tmpStr = tmpStr + max_sjz.ToString().Substring(i - 1, 1);
                                }
                                else
                                {
                                    if (!isStart && tmpStr != "")
                                        break;
                                }
                            }
                            max_sjz = Conversion.Val(tmpStr);
                            scz = Conversion.Val(sc);
                            calc_pd_ret = scz <= max_sjz && scz >= min_sjz ? "符合" : "不符合";
                        }
                    }
                    return calc_pd_ret;
                };

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

            #region  计算开始
            mCnt_FjHg = 0;  //记录复检合格的组数
            mCnt_FjHg1 = 0;
            MItem[0]["FJJJ1"] = "";
            MItem[0]["FJJJ2"] = "";
            MItem[0]["FJJJ3"] = "";

            foreach (var sitem in SItem)//单行数据进行验证 key 字段名 val值
            {
                sitem["FJ"] = "0";
                mZh = 1;
                string mgjlb = sitem["GJLB"];            //设计等级名称
                if (string.IsNullOrEmpty(mgjlb))
                    mgjlb = "";

                if (mrsbxdj != null)
                {
                    foreach (var bxitem in mrsbxdj)
                    {
                        if (sitem["GCLX_JB"] == bxitem["MC"] && calc_pd(bxitem["ZJFW"], sitem["ZJ"]) == "符合")
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
                var extraFieldsDj = mrsDj.FirstOrDefault(u => u["JB"].Contains(sitem["GCLX_JB"]) && u["GJLB"].Contains(mgjlb) && u["PH"].Contains(sitem["GCLX_PH"]) && u["YSLX"].Contains(sitem["YSLX"]));
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
                    mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"].Trim().ToLower();
                }
                if (string.IsNullOrEmpty(mJSFF) || mJSFF == "new")
                {
                    sitem["G_KLQD"] = mKlqd.ToString();
                    sitem["G_DLWZ"] = SclBzyq;
                    sitem["G_LWWZ"] = LwBzyq;
                }
                //求抗拉强度
                //求抗拉强度,断口,冷弯 合格个数,并且返回值为不同组不合格数的累加值
                int mcnt = 0;
                int this_bhg = 0;
                string bhggsbj = "";
                if (sitem["YPSL"] == "4根")
                    mxlgs = 4;
                calc_kl(sitem, mxlgs);

                if (extraFieldsDj["JB"].Contains("Ⅰ"))
                {
                    mcnt = 0;
                    sitem["G_JXLJ"] = "钢筋拉断时，接头试件实际抗拉强度大于等于钢筋抗拉强度标准值；连接件破坏时，接头试件实际抗拉强度大于等于1.10倍钢筋抗拉强度标准值";
                    for (int i = 1; i <= mxlgs; i++)
                    {
                        if (Conversion.Val(sitem["DKJ" + i]) == 1 || Conversion.Val(sitem["DKJ" + i]) == 2)
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
                    mcnt = 0;
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
                    mcnt = 0;
                    for (int i = 1; i <= mxlgs; i++)
                    {
                        if (Conversion.Val(sitem["KLQD" + i]) >= GetSafeDouble(extraFieldsDj["QFQDBZZ"]) * 1.35)
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
                int mbxbhgs = 0;
                int mbxhgs = 0;
                //单向拉伸残余形变
                if (sitem["JCXM"].Contains("单向拉伸残余形变"))
                {
                    sitem["DXLS"] = Math.Round((Conversion.Val(sitem["DXLS1"]) + Conversion.Val(sitem["DXLS2"]) + Conversion.Val(sitem["DXLS3"])) / 3, 2).ToString("0.00");
                    if (calc_pd(sitem["G_DXLS"], sitem["DXLS"]) == "不符合")
                    {
                        mbxbhgs = mbxbhgs + 1;
                        sitem["JCJG_DXLS"] = "不符合";
                    }
                    else
                    {
                        mbxhgs = mbxhgs + 1;
                        sitem["JCJG_DXLS"] = "符合";
                    }
                }
                else
                {
                    sitem["DXLS"] = "----";
                    sitem["JCJG_DXLS"] = "----";
                }

                //最大力总伸长率
                if (sitem["JCXM"].Contains("最大力总伸长率"))
                {
                    sitem["ZDLZSCL"] = Math.Round((Conversion.Val(sitem["ZDLZSCL1"]) + Conversion.Val(sitem["ZDLZSCL2"]) + Conversion.Val(sitem["ZDLZSCL3"])) / 3, 1).ToString("0.0");
                    if (calc_pd(sitem["g_ZDLZSCL"], sitem["ZDLZSCL"]) == "不符合")
                    {
                        mbxbhgs = mbxbhgs + 1;
                        sitem["JCJG_ZSCL"] = "不符合";
                    }
                    else
                    {
                        mbxhgs = mbxhgs + 1;
                        sitem["JCJG_ZSCL"] = "符合";
                    }
                }
                else
                {
                    sitem["JCJG_ZSCL"] = "----";
                    sitem["ZDLZSCL"] = "----";
                }

                //高应力反复拉压残余形变
                if (sitem["JCXM"].Contains("高应力反复拉压残余形变"))
                {
                    sitem["GYLFFLY"] = Math.Round((Conversion.Val(sitem["GYLFFLY1"]) + Conversion.Val(sitem["GYLFFLY2"]) + Conversion.Val(sitem["GYLFFLY3"])) / 3, 1).ToString("0.0");
                    if (calc_pd(sitem["G_GYLFFLY"], sitem["GYLFFLY"]) == "不符合")
                    {
                        mbxbhgs = mbxbhgs + 1;
                        sitem["JCJG_GYL"] = "不符合";
                    }
                    else
                    {
                        mbxhgs = mbxhgs + 1;
                        sitem["JCJG_GYL"] = "符合";
                    }
                }
                else
                {
                    sitem["JCJG_GYL"] = "----";
                    sitem["GYLFFLY"] = "----";
                }
                //大变形反复拉压残余形变
                if (sitem["JCXM"].Contains("大变形反复拉压残余形变"))
                {
                    sitem["DBXFFLY4"] = Math.Round((Conversion.Val(sitem["DBXLY4_1"]) + Conversion.Val(sitem["DBXLY4_2"]) + Conversion.Val(sitem["DBXLY4_3"])) / 3, 1).ToString();
                    sitem["DBXFFLY8"] = Math.Round((Conversion.Val(sitem["DBXLY8_1"]) + Conversion.Val(sitem["DBXLY8_2"]) + Conversion.Val(sitem["DBXLY8_3"])) / 3, 1).ToString();
                    if (sitem["g_DBXFFLY8"] != "----")
                    {
                        if ((calc_pd(sitem["G_DBXFFLY4"], sitem["DBXFFLY4"]) == "不符合") || (calc_pd(sitem["G_DBXFFLY8"], sitem["DBXFFLY8"]) == "不符合"))
                        {
                            mbxbhgs = mbxbhgs + 1;
                            sitem["JCJG_DBX"] = "不符合";
                        }
                        else
                        {
                            mbxhgs = mbxhgs + 1;
                            sitem["JCJG_DBX"] = "符合";
                        }
                    }
                    else
                    {
                        if (calc_pd(sitem["G_DBXFFLY4"], sitem["DBXFFLY4"]) == "不符合")
                        {
                            mbxbhgs = mbxbhgs + 1;
                            sitem["JCJG_DBX"] = "不符合";
                        }
                        else
                        {
                            mbxhgs = mbxhgs + 1;
                            sitem["JCJG_DBX"] = "符合";
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
                    }
                }
                if (mbxbhgs == 0 && mbxhgs == 0)
                {
                    sitem["JCJG_BX"] = "----";
                }
                if (mbxbhgs == 0 && mbxhgs > 0)
                {
                    sitem["JCJG_BX"] = "符合";
                }
                if (mbxbhgs > 0)
                {
                    sitem["JCJG_BX"] = "不符合";
                }
                if (sitem["JCJG_BX"] == "符合" || sitem["JCJG_BX"] == "----")
                {
                    if (this_bhg == 0)
                    {
                        sitem["JCJG"] = "合格";
                        MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + "1#";
                    }
                    if (this_bhg > 0)
                    {
                        sitem["JCJG"] = "不合格";
                        MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + "1#";
                    }
                }
                else
                {
                    sitem["JCJG_BX"] = "不符合";
                    MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + "1#";
                }
                mAllHg = mAllHg && (sitem["JCJG"] == "合格");
            }
            //主表总判断赋值
            string mjgsm = string.Empty;
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            if (MItem[0]["FJJJ3"].Trim() != "")
            {
                MItem[0]["FJJJ3"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                mjgsm = "该组试样所检项目符合标准要求。";
            }
            if (MItem[0]["FJJJ2"] != "")
            {
                MItem[0]["FJJJ2"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                mjgsm = "该组试样不符合标准要求。";
            }
            MItem[0]["JCJGMS"] = MItem[0]["FJJJ3"] + MItem[0]["FJJJ2"];
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
