using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.GYC_钢筋原材料
{
    public class GYC : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
         IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = null;

        var bz_zlpcb = dataExtra["BZ_ZLPCB"];
            var jcxm_keys = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            foreach (var jcxmitem in jcxm_keys)
            {
                if (jcxmitem.ToUpper().Contains("BGJG"))
                {
                    continue;
                }
                var s_gyctab = retData[jcxmitem]["S_GYC"];
                var s_twtab = retData[jcxmitem]["S_BY_RW_XQ"];
                var m_gyctab = retData[jcxmitem]["M_GYC"];
                int row = 0;
                foreach (var sItem in s_gyctab)
                {
                    #region  公共数据
                    string LwBzyq = string.Empty;
                    string GCLX_PH = sItem["GCLX_PH"].ToString();  //钢材类型牌号
                    string GJLB = sItem["GJLB"].ToString();  //钢筋类别
                    string ZJ = sItem["ZJ"].ToString();   //直径
                    double mQfqd = 0;  //屈服强度标准值
                    double mKlqd = 0;  //抗拉强度标准值
                    double mScl = 0;  //伸长率标准值
                    double mLw = 0;   //冷弯标准值
                    int mHggs_qfqd = 0;    //单组屈服点几个合格
                    int mHggs_klqd = 0;    //单组抗拉强度几个合格
                    int mHggs_scl = 0;   //单组伸长率几个不合格复试
                    int mHggs_lw = 0;    //单组冷弯几个不合格复试
                    int mFsgs_qfqd = 0;  //单组屈服点几个不合格复试
                    int mFsgs_klqd = 0;    //单组抗拉强度几个不合格复试
                    int mFsgs_scl = 0;    //单组伸长率几个不合格复试
                    int mFsgs_lw = 0;     //单组冷弯几个不合格复试
                    double mlwzj = 0;    //弯心直径
                    double mLwjd = 0;    //冷弯角度
                    int MFFWQCS = 0;   //反复弯曲次数
                    int mxlgs = 0;      //拉根数
                    int mxwgs = 0;      //弯根数
                    string mJSFF = string.Empty;   //计算方法
                    int mkzhggs = 0;    //抗震合格数量
                    int mCnt_FjHg = 0; //记录复试合格的组数
                    int mCnt_FjHg1 = 0;
                    m_gyctab[row]["FJJJ"] = "";
                    m_gyctab[row]["FJJJ1"] = "";
                    m_gyctab[row]["FJJJ2"] = "";
                    m_gyctab[row]["FJJJ3"] = "";
                    m_gyctab[row]["JSBEIZHU"] = "";
                    double zj1 = 0;
                    double zj2 = 0;
                    double sLlzl;  //理论重量
                    double sZlpc;  //重量偏差

                    sItem["XGM"] = GetSafeDouble(sItem["XGM"]).ToString();
                    //sItem["G_QFQFB"] = GetSafeDouble(sItem["G_QFQFB"]).ToString();
                    //sItem["G_QDQFB"] = GetSafeDouble(sItem["G_QDQFB"]).ToString();
                    sItem["G_QFQD"] = GetSafeDouble(sItem["G_QFQD"]).ToString();
                    sItem["G_KLQD"] = GetSafeDouble(sItem["G_KLQD"]).ToString();
                    sItem["G_SCL"] = GetSafeDouble(sItem["G_SCL"]).ToString();
                    //sItem["G_LW"] = GetSafeDouble(sItem["G_LW"]).ToString();
                    //sItem["G_LWZJ"] = GetSafeDouble(sItem["G_LWZJ"]).ToString();
                    sItem["G_ZLPC"] = GetSafeDouble(sItem["G_ZLPC"]).ToString();
                    sItem["CD"] = GetSafeDouble(sItem["CD"]).ToString();
                    sItem["ZJ"] = GetSafeDouble(sItem["ZJ"]).ToString();
                    //sItem["SCZJ1"] = GetSafeDouble(sItem["SCZJ1"]).ToString();
                    //sItem["SCZJ2"] = GetSafeDouble(sItem["SCZJ2"]).ToString();
                    sItem["HG_QF"] = GetSafeDouble(sItem["HG_QF"]).ToString();
                    sItem["HG_KL"] = GetSafeDouble(sItem["HG_KL"]).ToString();
                    sItem["HG_SC"] = GetSafeDouble(sItem["HG_SC"]).ToString();
                    sItem["HG_LW"] = GetSafeDouble(sItem["HG_LW"]).ToString();
                    //sItem["LQ"] = GetSafeDouble(sItem["LQ"]).ToString();




                    var fieldsExtra = dataExtra["BZ_GYC_DJ"].FirstOrDefault(u =>u.Keys.Contains("GJLB") && u.Values.Contains(GJLB) && u.Keys.Contains("PH") && u.Values.Contains(GCLX_PH)); //从设计等级表中取得相应的计算数值、等级标准
                    if (fieldsExtra != null && fieldsExtra.Count > 0)
                    {
                        sItem["SJDJ"] = fieldsExtra["MC"].ToString();
                        mQfqd = GetSafeDouble(fieldsExtra["QFQDBZZ"]);
                        mKlqd = GetSafeDouble(fieldsExtra["KLQDBZZ"]);
                        mScl = GetSafeDouble(fieldsExtra["SCLBZZ"]);
                        mLw = GetSafeDouble(fieldsExtra["LWBZZ"]);
                        mHggs_qfqd = GetSafeInt(fieldsExtra["ZHGGS_QFQD"]);
                        mHggs_klqd = GetSafeInt(fieldsExtra["ZHGGS_KLQD"]);
                        mHggs_scl = GetSafeInt(fieldsExtra["ZHGGS_SCL"]);
                        mHggs_lw = GetSafeInt(fieldsExtra["ZHGGS_LW"]);
                        mFsgs_qfqd = GetSafeInt(fieldsExtra["ZFSGS_QFQD"]);
                        mFsgs_klqd = GetSafeInt(fieldsExtra["ZFSGS_KLQD"]);
                        mFsgs_scl = GetSafeInt(fieldsExtra["ZFSGS_SCL"]);
                        mFsgs_lw = GetSafeInt(fieldsExtra["ZFSGS_LW"]);
                        mlwzj = GetSafeDouble(fieldsExtra["LWZJ"]);
                        mLwjd = GetSafeDouble(fieldsExtra["LWJD"]);
                        MFFWQCS = GetSafeInt(fieldsExtra["FFWQCS"]);
                        mxlgs = GetSafeInt(fieldsExtra["XLGS"]);
                        mxwgs = GetSafeInt(fieldsExtra["XWGS"]);
                        mJSFF = fieldsExtra["JSFF"];
                    }
                    if (fieldsExtra == null || fieldsExtra.Count == 0)
                    {
                        mJSFF = "";
                        sItem["JCJG"] = "依据不详";
                        m_gyctab[row]["JSBEIZHU"] = m_gyctab[row]["JSBEIZHU"] + "单组流水号: " + sItem["DZBH"] + "试件尺寸为空";
                        s_twtab[row]["JCJG"] = "依据不详";
                        s_twtab[row]["JCJGMS"] = "BZ_GYC_DJ 中没有相关数据 钢筋类别:" + GJLB + " 直径、牌号:" + GCLX_PH;
                        row++;
                        continue;
                    }
                    var bz_zlpcb_filtet = bz_zlpcb.FirstOrDefault(x => x.Values.Contains(GJLB) && x.Values.Contains(ZJ));  //通过钢材类别和直径求出理论重量
                    if (bz_zlpcb_filtet != null && bz_zlpcb_filtet.Count > 0)
                    {
                        sLlzl = GetSafeDouble(bz_zlpcb_filtet["LLZL"].ToString());
                        sZlpc = GetSafeDouble(bz_zlpcb_filtet["ZLPC"].ToString());
                        sItem["G_ZLPC"] = sZlpc.ToString();
                    }
                    else
                    {
                        sLlzl = 0;
                        sZlpc = 0;
                    }
                    if (string.IsNullOrEmpty(fieldsExtra["WHICH"]))
                        m_gyctab[row]["WHICH"] = "0";
                    else
                        m_gyctab[row]["WHICH"] = fieldsExtra["WHICH"];
                    if (m_gyctab[row]["PDBZ"].Contains("1499.1-2017"))
                    {
                        m_gyctab[row]["WHICH"] = "10";
                        if (sItem["G_ZLPC"] == "7")
                        {
                            sItem["G_ZLPC"] = "6";
                            sZlpc = 6;
                        }
                    }
                    if (m_gyctab[row]["PDBZ"].Contains("1499.2-2018"))
                    {
                        m_gyctab[row]["WHICH"] = "15";
                        if (sItem["G_ZLPC"] == "7")
                        {
                            sItem["G_ZLPC"] = "6";
                            sZlpc = 6;
                        }
                        if (!sItem["GJLB"].Contains("调直"))
                            sItem["G_ZLPC"] = Math.Round(GetSafeDouble(sItem["G_ZLPC"]), 1).ToString();
                    }
                    if (mlwzj == 0 && MFFWQCS != 0)
                        LwBzyq = "弯曲次数不小于\" & " + MFFWQCS + " & \"次，受弯曲部位表面无裂纹。";
                    if (mlwzj < 1)
                        LwBzyq = "弯心直径d=0\" & " + mlwzj + " & \"a弯曲\" & " + mLwjd + " & \"度后受弯曲部位表面无裂纹。";
                    else
                        LwBzyq = "弯心直径d=\" & " + mlwzj + " & \"a弯曲\" & " + mLwjd + " & \"度后受弯曲部位表面无裂纹。";
                    sItem["G_QFQD"] = mQfqd.ToString();
                    sItem["G_KLQD"] = mKlqd.ToString();
                    sItem["G_SCL"] = mScl.ToString();
                    sItem["G_LWWZ"] = LwBzyq;
                    //求伸长率
                    sItem["XGM"] = fieldsExtra["XGM"];
                    sItem["CD"] = (Math.Round((GetSafeDouble(sItem["XGM"].ToLower()) * GetSafeDouble(sItem["ZJ"].ToString())) / 5 + 0.001, 0) * 5).ToString();
                    if (GetSafeDouble(sItem["XGM"].ToString()) - 100 < 0.00001)
                        sItem["cd"] = "100";

                    //面积计算公式
                    double mMidVal = (GetSafeDouble(sItem["ZJ"]) / 2) * (GetSafeDouble(sItem["ZJ"]) / 2);
                    double mMj = Math.Round(3.14159 * mMidVal, 2);
                    sItem["MJ"] = mMj.ToString("0.00");//冷轧扭
                    if (sItem["SJDJ"].Contains("冷轧扭"))
                    {
                        string sjdj = sItem["SJDJ"];
                        string zj = sItem["ZJ"].ToString();
                        switch (sjdj)
                        {
                            case "冷轧扭CTB550Ⅰ":
                                switch (zj)
                                {
                                    case "6.5":
                                        mMj = 29.50;
                                        break;
                                    case "8":
                                        mMj = 45.30;
                                        break;
                                    case "10":
                                        mMj = 68.30;
                                        break;
                                    case "12":
                                        mMj = 96.14;
                                        break;
                                }
                                break;
                            case "冷轧扭CTB550Ⅱ":
                                switch (zj)
                                {
                                    case "6.5":
                                        mMj = 29.20;
                                        break;
                                    case "8":
                                        mMj = 42.30;
                                        break;
                                    case "10":
                                        mMj = 66.10;
                                        break;
                                    case "12":
                                        mMj = 92.74;
                                        break;
                                }
                                break;
                            case "冷轧扭CTB550Ⅲ":
                                switch (zj)
                                {
                                    case "6.5":
                                        mMj = 29.86;
                                        break;
                                    case "8":
                                        mMj = 45.24;
                                        break;
                                    case "10":
                                        mMj = 70.69;
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "冷轧扭CTB650Ⅲ":
                                switch (zj)
                                {
                                    case "6.5":
                                        mMj = 28.20;
                                        break;
                                    case "8":
                                        mMj = 42.73;
                                        break;
                                    case "10":
                                        mMj = 66.76;
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    sItem["MJ"] = mMj.ToString("0.00");
                    //屈服强度
                    if (Math.Abs(mMj - 0) > 0.00001)
                    {
                        string mqf = string.Empty;
                        for (int i = 1; i <= mxlgs; i++)
                        {
                            if (string.IsNullOrEmpty(sItem["QFHZ" + i]))
                                sItem["QFHZ" + i] = "0";
                            mqf = (1000 * GetSafeDouble(sItem["QFHZ" + i]) / mMj).ToString();
                            if (GetSafeDouble(mqf) <= 200)
                                sItem["QFQD" + i] = Math.Round(GetSafeDouble(mqf), 0).ToString();
                            if (GetSafeDouble(mqf) > 200 && GetSafeDouble(mqf) <= 1000)
                                sItem["QFQD" + i] = (Math.Round(GetSafeDouble(mqf) / 5, 0) * 5).ToString();
                            if (GetSafeDouble(mqf) > 1000)
                                sItem["QFQD" + i] = (Math.Round(GetSafeDouble(mqf) / 10, 0) * 10).ToString();
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= mxlgs; i++)
                        {
                            sItem["QFQD" + i] = "0";
                        }
                    }
                    //抗拉强度
                    if (Math.Abs(mMj - 0) > 0.00001)
                    {
                        string mkl = string.Empty;
                        for (int i = 1; i <= mxlgs; i++)
                        {
                            if (string.IsNullOrEmpty(sItem["KLHZ" + i]))
                                sItem["KLHZ" + i] = "0";
                            mkl = (1000 * GetSafeDouble(sItem["KLHZ" + i]) / mMj).ToString();
                            if (GetSafeDouble(mkl) <= 200)
                                sItem["KLQD" + i] = Math.Round(GetSafeDouble(mkl), 0).ToString();
                            if (GetSafeDouble(mkl) > 200 && GetSafeDouble(mkl) <= 1000)
                                sItem["KLQD" + i] = (Math.Round(GetSafeDouble(mkl) / 5, 0) * 5).ToString();
                            if (GetSafeDouble(mkl) > 1000)
                                sItem["KLQD" + i] = (Math.Round(GetSafeDouble(mkl) / 10, 0) * 10).ToString();
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= mxlgs; i++)
                        {
                            sItem["KLQD" + i] = "0";
                        }
                    }
                    //伸长率
                    if (Math.Abs(GetSafeDouble(sItem["CD"]) - 0) > 0.00001)
                    {
                        string msc = string.Empty;
                        for (int i = 1; i <= mxlgs; i++)
                        {
                            double mscz = Math.Round(4 * GetSafeDouble(sItem["SCZ" + i]) - GetSafeDouble(sItem["CD"]), 4) / 4;
                            double ms = 100 * GetSafeDouble(sItem["SCZ" + i]) == 0 ? 0 : (GetSafeDouble(sItem["SCZ" + i]) - GetSafeDouble(sItem["CD"])) / GetSafeDouble(sItem["CD"]);
                            if (fieldsExtra["PDBZ"].Contains("带肋") || fieldsExtra["PDBZ"].Contains("1499.1-2008"))
                            {
                                if (ms > 10)
                                    ms = Math.Round(ms, 0);
                                else
                                    ms = Math.Round(Math.Round(ms * 2, 0) / 2, 1);
                            }
                            else
                                ms = Math.Round(Math.Round(ms * 2, 0) / 2, 1);
                            sItem["SCL" + i] = ms.ToString();
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= mxlgs; i++)
                        {
                            sItem["SCL" + i] = "0";
                        }
                    }

                    #region 不合格数叠加
                    //屈服强度
                    int mcnt = 0;
                    int this_bhg = 0;
                    int mallBhg_qf = 0;
                    int mallBhg_kl = 0;
                    int mallBhg_sc = 0;
                    int mallBhg_lw = 0;
                    for (int i = 1; i <= mxlgs; i++)
                    {
                        if (GetSafeDouble(sItem["QFQD" + i].ToString()) - mQfqd > -0.00001)
                            mcnt = mcnt + 1;
                        else
                            this_bhg = this_bhg + 1;
                    }
                    sItem["HG_QF"] = mcnt.ToString();
                    mallBhg_qf += this_bhg;
                    //抗拉强度
                    mcnt = 0;
                    this_bhg = 0;
                    for (int i = 1; i <= mxlgs; i++)
                    {
                        if (GetSafeDouble(sItem["KLQD" + i].ToString()) - mKlqd > -0.00001)
                            mcnt = mcnt + 1;
                        else
                            this_bhg = this_bhg + 1;
                    }
                    sItem["HG_KL"] = mcnt.ToString();
                    mallBhg_kl += this_bhg;
                    //伸长率
                    mcnt = 0;
                    this_bhg = 0;
                    for (int i = 1; i <= mxlgs; i++)
                    {
                        if (GetSafeDouble(sItem["SCL" + i].ToString()) - mScl > -0.00001)
                            mcnt = mcnt + 1;
                        else
                            this_bhg = this_bhg + 1;
                    }
                    sItem["HG_SC"] = mcnt.ToString();
                    mallBhg_sc += this_bhg;
                    //冷弯
                    mcnt = 0;
                    this_bhg = 0;
                    for (int i = 1; i <= mxwgs; i++)
                    {
                        if (GetSafeDouble(sItem["LW" + i].ToString()) - mLw > -0.00001)
                            mcnt = mcnt + 1;
                        else
                        {
                            if (i > 1)
                            {
                                if (GetSafeDouble(sItem["LW1"].ToString()) - i * mLw > -0.00001)  //判断是否把冷弯值全部输在第一个值上
                                    this_bhg = this_bhg + 1;
                                else
                                    mcnt = mcnt + 1;
                            }
                            else
                                this_bhg = this_bhg + 1;
                        }
                    }
                    sItem["HG_LW"] = mcnt.ToString();
                    mallBhg_lw += this_bhg;
                    #endregion
                    #endregion

                    if (jcxmitem.Contains("重量偏差"))
                    {
                        if (fieldsExtra["PDBZ"].ToString().IndexOf("1499.2-2018") > 0)
                        {
                            if (sItem["G_ZLPC"].ToString() == "7")
                            {
                                sItem["G_ZLPC"] = "6";
                                sZlpc = 6;
                            }
                            //if (fieldsExtra["GJLB"].ToString().IndexOf("调直") <= 0)
                            //    item["G_ZLPC"] = item["G_ZLPC"].ToString("0.0");
                        }

                        double zcd = GetSafeDouble((int.Parse(sItem["Z_CD1"].ToString()) + int.Parse(sItem["Z_CD2"].ToString()) + int.Parse(sItem["Z_CD3"].ToString()) + int.Parse(sItem["Z_CD4"].ToString()) + int.Parse(sItem["Z_CD5"].ToString())).ToString());
                        if (fieldsExtra["PDBZ"].ToString().IndexOf("1499.2-2018") > 0)
                        {

                            sItem["ZLPC"] = Math.Round(100 * (GetSafeDouble(sItem["Z_ZZL"].ToString()) - GetSafeDouble((sLlzl * zcd).ToString())) / (GetSafeDouble((sLlzl * zcd).ToString())), 1).ToString("0.0");
                        }
                        else
                        {
                            sItem["ZLPC"] = Math.Round(100 * (GetSafeDouble(sItem["Z_ZZL"].ToString()) - GetSafeDouble((sLlzl * zcd).ToString())) / (GetSafeDouble((sLlzl * zcd).ToString())), 0).ToString();
                        }
                        if (Math.Abs(GetSafeDouble(sItem["ZLPC"].ToString())) <= GetSafeDouble(sZlpc.ToString()))
                        {
                            sItem["jcjg_zlpc"] = "符合";
                            s_twtab[row]["JCJG"] = sItem["jcjg_zlpc"];
                            s_twtab[row]["JCJGMS"] = "该组试样重量偏差" + sItem["ZLPC"] + ",所检测项目符合" + sZlpc + "标准要求";
                        }
                        else
                        {
                            sItem["jcjg_zlpc"] = "不符合";
                            s_twtab[row]["JCJG"] = sItem["jcjg_zlpc"];
                            s_twtab[row]["JCJGMS"] = "该组试样重量偏差" + sItem["ZLPC"] + ",所检测项目不符合" + sZlpc + "标准要求";
                        }
                    }
                    if (jcxmitem.Contains("最大力总伸长率"))
                    {
                        if (GetSafeDouble(sItem["DHJL1"]) != 0)
                        {
                            for (int i = 1; i <= mxlgs; i++)
                            {
                                sItem["ZSCL" + i] = Math.Round(((GetSafeDouble(sItem["DHJL" + i].ToString()) - GetSafeDouble(sItem["DQJL0" + i].ToString())) / GetSafeDouble(sItem["DQJL0" + i].ToString()) + GetSafeDouble(sItem["KLQD" + i].ToString()) / 200000) * 100, 1).ToString("G3");
                                sItem["ZSCL" + i] = Math.Round(GetSafeDouble(sItem["ZSCL" + i].ToString()), 1).ToString("0.0");
                                if (GetSafeDouble(sItem["ZSCL" + i].ToString()) >= GetSafeDouble(fieldsExtra["ZSCL"].ToString()))
                                    mallBhg_sc = mallBhg_sc + 1;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= mxlgs; i++)
                        {
                            sItem["ZSCL" + i] = "0";
                        }
                    }
                    if (jcxmitem.Contains("抗震要求"))
                    {
                        if (m_gyctab[row]["PDBZ"].Contains("1499.1-2017"))
                            m_gyctab[row]["WHICH"] = "14";
                        if (m_gyctab[row]["PDBZ"].Contains("1499") && sItem["DHJL1"] != "0")
                        {
                            for (int i = 1; i <= mxlgs; i++)
                            {
                                sItem["ZSCL" + i] = Math.Round(((GetSafeDouble(sItem["DHJL" + i]) - GetSafeDouble(sItem["DQJL0"] + i)) / GetSafeDouble(sItem["DQJL0" + i]) + GetSafeDouble(sItem["KLQD"] + i) / 200000) * 100, 1).ToString("G3");
                                sItem["ZSCL" + i] = Math.Round(GetSafeDouble(sItem["ZSCL" + i].ToString()), 1).ToString("0.0");
                                sItem["QDQFB" + i] = Math.Round(GetSafeDouble(sItem["KLQD" + i]) / GetSafeDouble(sItem["QFQD" + i]), 2).ToString("0.00");
                                sItem["QFQFB" + i] = Math.Round(GetSafeDouble(sItem["QFQD" + i]) / mQfqd, 2).ToString();
                                if (GetSafeDouble(sItem["QDQFB" + i]) >= GetSafeDouble(fieldsExtra["QDQFB"]) && GetSafeDouble(sItem["QFQFB" + i]) <= GetSafeDouble(fieldsExtra["QFQFB"]) && GetSafeDouble(sItem["ZSCL" + i]) >= GetSafeDouble(fieldsExtra["ZSCL"]))
                                {
                                    mkzhggs = mkzhggs + 1;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 1; i <= mxlgs; i++)
                            {
                                sItem["ZSCL" + i] = "----";
                                sItem["QDQFB" + i] = Math.Round(GetSafeDouble(sItem["KLQD" + i]) / GetSafeDouble(sItem["QFQD" + i]), 2).ToString("0.00");
                                sItem["QFQFB" + i] = Math.Round(GetSafeDouble(sItem["QFQD" + i]) / mQfqd, 2).ToString();
                                if (GetSafeDouble(sItem["QDQFB" + i]) >= GetSafeDouble(fieldsExtra["QDQFB"]) && GetSafeDouble(sItem["QFQFB" + i]) <= GetSafeDouble(fieldsExtra["QFQFB"]) && GetSafeDouble(sItem["ZSCL" + i]) >= GetSafeDouble(fieldsExtra["ZSCL"]))
                                {
                                    mkzhggs = mkzhggs + 1;
                                }
                            }
                        }
                        if (mkzhggs == mxlgs)
                        {
                            sItem["JCJG_KZ"] = "符合";
                            s_twtab[row]["JCJG"] = sItem["JCJG_KZ"];
                            s_twtab[row]["JCJGMS"] = "该组试样实测强屈比" + sItem["QDQFB1"] + ",标准屈服比" + sItem["QFQFB1"] + ",最大总延伸率" + sItem["ZSCL"] + "所检测项目符合相关标准要求";
                        }
                        else
                        {
                            sItem["JCJG_KZ"] = "不符合";
                            s_twtab[row]["JCJG"] = sItem["JCJG_KZ"];
                            s_twtab[row]["JCJGMS"] = "该组试样实测强屈比" + sItem["QDQFB1"] + ",标准屈服比" + sItem["QFQFB1"] + ",最大总延伸率" + sItem["ZSCL"] + "所检测项目符合相关标准要求";
                        }
                    }
                    else
                    {
                        sItem["JCJG_KZ"] = "----";
                    }
                    if (jcxmitem.Contains("反向弯曲"))
                    {
                        m_gyctab[row]["WHICH"] = "100";
                        sItem["G_LWWZ"] = "经反向弯曲后，钢筋受弯曲部位不得产生裂纹。";
                        if (sItem["FXWQ"] == "1")
                        {
                            sItem["JCJG_LW"] = "符合";
                            s_twtab[row]["JCJG"] = sItem["JCJG_LW"];
                            s_twtab[row]["JCJGMS"] = "无裂缝";
                        }
                        else
                        {
                            sItem["JCJG_LW"] = "不符合";
                            s_twtab[row]["JCJG"] = sItem["JCJG_LW"];
                            s_twtab[row]["JCJGMS"] = "不符合";
                        }
                    }
                    if (jcxmitem.Contains("拉伸"))
                    {
                        if (m_gyctab[row]["PDBZ"].Contains("1499.2-2018") && sItem["GCLX_PH"].Contains("E"))
                        {
                            sItem["G_SCL"] = "0";
                            if (int.Parse(sItem["HG_QF"]) >= mHggs_qfqd && int.Parse(sItem["HG_KL"]) >= mHggs_klqd)
                            {
                                sItem["JCJG_LS"] = "符合";
                                s_twtab[row]["JCJG"] = sItem["JCJG_LS"];
                                s_twtab[row]["JCJGMS"] = "该组试样所检测项目符合标准要求";
                            }
                            else
                            {
                                sItem["JCJG_LS"] = "不符合";
                                s_twtab[row]["JCJG"] = sItem["JCJG_LS"];
                                s_twtab[row]["JCJGMS"] = "该组试样所检测项目不符合标准要求";
                            }
                        }
                        else
                        {
                            if (int.Parse(sItem["HG_QF"]) >= mHggs_qfqd && int.Parse(sItem["HG_KL"]) >= mHggs_klqd && int.Parse(sItem["HG_SC"]) >= mHggs_scl)
                            {
                                sItem["JCJG_LS"] = "符合";
                                s_twtab[row]["JCJG"] = sItem["JCJG_LS"];
                                s_twtab[row]["JCJGMS"] = "该组试样所检测项目符合标准要求";
                            }
                            else
                            {
                                sItem["JCJG_LS"] = "不符合";
                                s_twtab[row]["JCJG"] = sItem["JCJG_LS"];
                                s_twtab[row]["JCJGMS"] = "该组试样所检测项目不符合标准要求";
                            }
                        }
                    }
                    else
                        sItem["JCJG_LS"] = "----";
                    if (jcxmitem.Contains("冷弯"))
                    {
                        if (int.Parse(sItem["HG_LW"]) - mHggs_lw > -0.00001)
                        {
                            sItem["JCJG_LW"] = "符合";
                            s_twtab[row]["JCJG"] = sItem["JCJG_LW"];
                            s_twtab[row]["JCJGMS"] = "符合";
                        }
                        else
                        {
                            sItem["JCJG_LW"] = "不符合";
                            s_twtab[row]["JCJG"] = sItem["JCJG_LW"];
                            s_twtab[row]["JCJGMS"] = "不符合";
                        }
                    }
                    else
                    {
                        sItem["JCJG_LW"] = "----";
                        sItem["LW1"] = "-1";
                        sItem["LW2"] = "-1";
                        sItem["LW3"] = "-1";
                        sItem["LW3"] = "-1";
                    }
                    if (jcxmitem.Contains("屈服强度"))
                    {
                        if (int.Parse(sItem["HG_QF"]) >= mHggs_qfqd && int.Parse(sItem["HG_KL"]) >= mHggs_klqd)
                        {
                            sItem["JCJG_LS"] = "符合";
                            s_twtab[row]["JCJG"] = sItem["JCJG_LS"];
                            s_twtab[row]["JCJGMS"] = "该组试样屈服强度" + sItem["QFQD1"] + ",所检测项目符合" + mQfqd + "标准要求";
                        }
                        else
                        {
                            sItem["JCJG_LS"] = "不符合";
                            s_twtab[row]["JCJG"] = sItem["JCJG_LS"];
                            s_twtab[row]["JCJGMS"] = "该组试样屈服强度" + sItem["QFQD1"] + ",所检测项目不符合" + mQfqd + "标准要求";
                        }
                    }

                    //总逻辑判断
                    if (sItem["JCJG_KZ"] == "不符合" || sItem["JCJG_LS"] == "不符合")
                        sItem["JCJG_LS"] = "不符合";
                    if (sItem["JCJG_LS"] == "不符合" || sItem["JCJG_LW"] == "不符合" || sItem["JCJG_ZLPC"] == "不符合")
                        sItem["JCJG"] = "不符合";
                    else
                    {
                        sItem["JCJG"] = "符合";
                    }
                    if (sItem["ZJ"] == "6.5")
                        m_gyctab[row]["WHICH"] = "7";
  					mAllHg = mAllHg && sItem["JCJG"] == "符合";
                    string mZh = sItem["ZH"];                    //单组是否需双倍复检的判定
                    if (sItem["JCJG"] == "不合格")
                    {
                        if (sItem["JCJG_LS"].Trim() == "不符合" && sItem["JCJG_LW"].Trim() == "不符合")
                        {
                            sItem["JCJG"] = "不合格";
                            //m_gyctab[row]["FJJJ2"] = m_gyctab[row]["FJJJ2"] + mZh + "#";
                            m_gyctab[row]["FJJJ2"] = m_gyctab[row]["FJJJ2"] + "#";
                        }
                        else
                        {
                            if (sItem["JCJG_LS"].Trim() == "不符合" || sItem["JCJG_LW"].Trim() == "不符合" || sItem["JCJG_ZLPC"].Trim() == "不符合")
                            {
                                sItem["JCJG"] = "复试";
                                //m_gyctab[row]["FJJJ1"] = m_gyctab[row]["FJJJ1"] + mZh + "#";
                                m_gyctab[row]["FJJJ1"] = m_gyctab[row]["FJJJ1"] + "#";

                            }
                        }
                    }
                    if (sItem["JCJG_ZLPC"].Trim() == "不符合" || sItem["JCJG_KZ"].Trim() == "不符合")
                    {
                        if (!m_gyctab[row]["FJJJ1"].Contains(sItem["ZH"] + "#"))
                            m_gyctab[row]["FJJJ1"] = m_gyctab[row]["FJJJ1"] + sItem["ZH"] + "#";
                    }
                    if (sItem["JCJG"] == "复试")
                    {
                        if (sItem["JCJG_ZLPC"].Trim() == "不符合" || sItem["JCJG_KZ"].Trim() == "不符合")
                        { }
                        else
                        {
                            m_gyctab[row]["JSBEIZHU"] = "试样" + m_gyctab[row]["FJJJ1"] + "建议取双倍样复试";
                        }
                    }

                    //主表总判断赋值
                    //if (mAllHg)
                    //    m_gyctab[row]["JCJG"] = "合格";
                    //else
                    //    m_gyctab[row]["JCJG"] = "不合格";
                    if (m_gyctab[row]["FJJJ3"] != "")
                    {
                        m_gyctab[row]["FJJJ3"] = "试样" + m_gyctab[row]["FJJJ3"] + "所检项目符合标准要求。";
                    }
                    m_gyctab[row]["JGSM"] = m_gyctab[row]["FJJJ3"] + m_gyctab[row]["FJJJ1"];
                    row++;
                }
            }
            #region 更新主表检测结果
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            bgjgDic.Add("JCJG", (mAllHg ? "合格" : "不合格"));
            bgjgDic.Add("JCJGMS", $"该组试样所检项目{(mAllHg ? "" : "不")}符合{dataExtra["BZ_GYC_DJ"].FirstOrDefault()["PDBZ"]}标准要求。");
            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
