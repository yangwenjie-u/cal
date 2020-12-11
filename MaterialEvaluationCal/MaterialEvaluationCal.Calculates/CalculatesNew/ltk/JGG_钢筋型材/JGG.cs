using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JGG2 : BaseMethods
    {
        public void Calc()
        {
            #region Code

            #region 数据准备
            var extraDJ = dataExtra["BZ_JGG_DJ"];
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var mJcjg = "";//记录最终报告是否不下结论
            var SItems = retData["S_JGG"];
            if (!retData.ContainsKey("M_JGG"))
            {
                retData["M_JGG"] = new List<IDictionary<string, string>>();
            }
            var MItem = retData["M_JGG"];

            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mFlag_Bhg = false;
            var mFlag_Hg = false;
            #endregion

            #region 局部函数
            Func<double, double> myint = delegate (double dataChar)
            {
                return Math.Round(Conversion.Val(dataChar) / 5, 0) * 5;
            };

            //'返回值为每组每种指标不合格总数  ' mbzValue 是单前判断指标的标准值,count 是一组中的检测个数
            Func<IDictionary<string, string>, IDictionary<string, string>, string, double, double, int> find_singlezb_bhg = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, string zbName, double mbzValue, double count)
            {
                var mcnt = 0;//计算单组合格个数
                var mCurBhg_qf = 0;//计算单组不合格个数
                var this_bhg = 0;//当前组单个指标不合格累加
                var sValue = 0.0;

                switch (zbName)
                {
                    case "qf":
                        for (int i = 1; i < count + 1; i++)
                        {
                            sValue = Conversion.Val(sItem["QFQD" + i]);
                            if (sValue - mbzValue > -0.00001)
                            {
                                mcnt = mcnt + 1;
                            }
                            else
                            {
                                this_bhg = this_bhg + 1;
                            }
                        }
                        sItem["HG_QF"] = mcnt.ToString(); ;

                        break;

                    case "lw":
                        for (int i = 1; i < count + 1; i++)
                        {
                            sValue = Conversion.Val(sItem["LW" + i]);
                            if (sValue - mbzValue > -0.00001)
                            {
                                mcnt = mcnt + 1;
                            }
                            else
                            {
                                if (i > 1)
                                {
                                    if (Conversion.Val(sItem["LW1"]) - i * mbzValue < -0.00001)

                                        this_bhg = this_bhg + 1;
                                    else
                                        mcnt = mcnt + 1;
                                }
                                else
                                {
                                    this_bhg = this_bhg + 1;
                                }
                            }
                        }
                        sItem["HG_LW"] = mcnt.ToString(); ;
                        break;
                }
                return 0;
            };

            Func<IDictionary<string, string>, IDictionary<string, string>, double, double, double, double, int> all_zb_jl = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, double mHggs_qfqd_f, double mHggs_klqd_f, double mHggs_scl_f, double mHggs_lw_f)
            {
                if (null == sItem["HG_QF"])
                {
                    sItem["HG_QF"] = "0";
                }

                if (null == sItem["HG_SC"])
                {
                    sItem["HG_SC"] = "0";
                }
                if (null == sItem["HG_LW"])
                {
                    sItem["HG_LW"] = "0";
                }
                if (null == sItem["HG_KL"])
                {
                    sItem["HG_KL"] = "0";
                }
                var jcxm2 = "";
                jcxm2 = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                if (jcxm2.Contains("拉伸"))
                {
                    if (GetSafeDouble(sItem["HG_QF"]) >= mHggs_klqd_f && GetSafeDouble(sItem["HG_KL"]) >= mHggs_klqd_f && GetSafeDouble(sItem["HG_SC"]) >= mHggs_scl_f)
                        sItem["JCJG_LS"] = "符合";
                    else
                    {
                        sItem["JCJG_LS"] = "不符合";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["JCJG_LS"] = "----";
                }

                if (jcxm2.Contains("冷弯")|| jcxm2.Contains("弯曲"))
                {
                    if (GetSafeDouble(sItem["HG_LW"]) - mHggs_lw_f > -0.00001)
                        sItem["JCJG_LW"] = "符合";
                    else
                    {
                        sItem["JCJG_LW"] = "不符合";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["JCJG_LW"] = "----";
                    sItem["LW1"] = "-1";
                    sItem["LW2"] = "-1";
                    sItem["LW3"] = "-1";

                }
                return 0;
            };
            #endregion

            #region 定义变量
            int mCnt_FjHg = 0;// '记录复试合格的组数
            int mCnt_FjHg1 = 0;
            MItem[0]["FJJJ"] = "";
            MItem[0]["FJJJ1"] = "";
            MItem[0]["FJJJ2"] = "";
            MItem[0]["FJJJ3"] = "";
            jsbeizhu = "";
            double zj1 = 0, zj2 = 0;
            bool doOther = false;

            double mQfqd, mLw, mcj = 0;
            string mHggs_qfqd, mHggs_klqd, mHggs_scl, mHggs_lw = "";
            string mFsgs_qfqd, mFsgs_klqd, mFsgs_scl, mFsgs_lw = "";
            string mlwjd, MFFWQCS = "";
            double mlwzj = 0;
            string mxwgs = "";
            string mJSFF = "";

            double mScl = 0;
            string mKlqd, mklqd1 = "";
            var LwBzyq = "";
            var mxlgs = 0.0;
            var mSjdj = "";
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";

            #endregion

            foreach (var sItem in SItems)
            {
                sItem["JCJLWZ"] = "检测结论";
                sItem["CJSYHZ"] = "冲击试验";
                #region 数据处理
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mSjdj = string.IsNullOrEmpty(sItem["SJDJ"]) ? "" : sItem["SJDJ"];
                //厚度或直径
                var hd_fw = "";
                var md = 0.0;
                var hd = sItem["HD"];
                if (IsNumeric(hd) && GetSafeDouble(hd) > 0)
                {
                    md = Conversion.Val(hd);
                }
                else
                {
                    md = Conversion.Val(sItem["ZJ"]);
                }

                if (mSjdj == "碳素结构钢")
                {
                    if (md <= 16)
                        hd_fw = "≤16";
                    else if (md <= 40)
                        hd_fw = "＞16≤40";
                    else if (md <= 60)
                        hd_fw = "＞40≤60";
                    else if (md <= 80)
                        hd_fw = "＞60≤80";
                    else if (md <= 100)
                        hd_fw = "＞80≤100";
                    else if (md <= 150)
                        hd_fw = "＞100≤150";
                    else if (md <= 200)
                        hd_fw = "＞150≤200";
                    else if (md <= 250)
                        hd_fw = "＞200≤250";
                    else
                        hd_fw = "";
                }
                else
                {
                    if (md <= 16)
                        hd_fw = "≤16";
                    else if (md <= 40)
                        hd_fw = "＞16≤40";
                    else if (md <= 63)
                        hd_fw = "＞40≤63";
                    else if (md <= 80)
                        hd_fw = "＞60≤80";
                    else if (md <= 100)
                        hd_fw = "＞80≤100";
                    else if (md <= 150)
                        hd_fw = "＞100≤150";
                    else if (md <= 200)
                        hd_fw = "＞150≤200";
                    else if (md <= 250)
                        hd_fw = "＞200≤250";
                    else
                        hd_fw = "";
                }
                #endregion

                #region  标准表
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["MC"] == mSjdj.Trim() && u["ZJM"] == hd_fw.Trim() && u["QYFX"] == sItem["QYFX"].Trim());
                if (null == extraFieldsDj)
                {
                    extraFieldsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["MC"] == mSjdj.Trim());
                }

                if (null == extraFieldsDj)
                {
                    sItem["JCJG"] = "不下结论";
                    mJcjg = "不下结论";
                    jsbeizhu = "牌号" + sItem["GCLX_PH"] + mSjdj + "试件尺寸为空\r\n";
                    mAllHg = false;
                    continue;
                }
                mQfqd = GetSafeDouble(extraFieldsDj["QFQDBZZ"]); //'单组标准值
                mKlqd = extraFieldsDj["KLQDBZZ"];
                mklqd1 = extraFieldsDj["KLQDBZZ1"];
                mScl = GetSafeDouble(extraFieldsDj["SCLBZZ"]);
                mLw = GetSafeDouble(extraFieldsDj["LWBZZ"]);
                mcj = GetSafeDouble(extraFieldsDj["CJBZZ"]);

                mHggs_qfqd = extraFieldsDj["ZHGGS_QFQD"];//'单组合格个数
                mHggs_klqd = extraFieldsDj["ZHGGS_KLQD"];
                mHggs_scl = extraFieldsDj["ZHGGS_SCL"];
                mHggs_lw = extraFieldsDj["ZHGGS_LW"];

                mFsgs_qfqd = extraFieldsDj["ZFSGS_QFQD"];// '单组复试不合格个数
                mFsgs_klqd = extraFieldsDj["ZFSGS_KLQD"];
                mFsgs_scl = extraFieldsDj["ZFSGS_SCL"];
                mFsgs_lw = extraFieldsDj["ZFSGS_LW"];

                mlwzj = GetSafeDouble(extraFieldsDj["LWZJ"]);// '冷弯直径和角度
                mlwjd = GetSafeDouble(extraFieldsDj["LWJD"]).ToString();
                MFFWQCS = extraFieldsDj["FFWQCS"];

                mxlgs = GetSafeDouble(extraFieldsDj["XLGS"]);
                mxwgs = extraFieldsDj["XWGS"];
                sItem["G_CJ"] = extraFieldsDj["CJBZZ"];

                mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"].Trim().ToLower();
                #endregion

                #region 检测项

                #region 标准值赋值到数据表
                var gclx_lb = sItem["GCLX_LB"];
                if ((!gclx_lb.Contains("板") && !gclx_lb.Contains("带")) && MItem[0]["PDBZ"].Contains("GB/T 700-2006《碳素结构钢》"))
                {
                    mlwzj = mlwzj - 0.5;
                }
                #region 伸长率标准值
                if ((gclx_lb.Contains("板") || gclx_lb.Contains("带")) && MItem[0]["PDBZ"].Contains("GB/T 700-2006《碳素结构钢》"))
                {
                    if (sItem["QYFX"] == "横向")
                    {
                        mScl = mScl - 2;
                    }
                }
                #endregion
                if (Conversion.Val(sItem["HD"]) > 100 && MItem[0]["PDBZ"].Contains("GB/T 700-2006《碳素结构钢》"))
                {
                    mKlqd = (GetSafeDouble(mKlqd) - 20).ToString();
                }
                #region 弯曲标准
                if (Conversion.Val(mlwzj) == 0 && Conversion.Val(MFFWQCS) != 0)
                {
                    LwBzyq = "弯曲次数不小于" + MFFWQCS + "次，受弯曲部位表面无裂纹。";//'至少"(mxwgs)  "个试件外侧未发生破裂";
                }
                else
                {
                    if (mlwzj == 0)
                    {
                        LwBzyq = "弯心直径d=0,弯曲" + mlwjd + "度后受弯曲部位表面无裂纹。"; //   '至少" & CStr(mxwgs) & "个试件外侧未发生破裂"
                    }
                    else
                    {
                        LwBzyq = MItem[0]["JCYJ"].Contains("1999") ? "弯心直径d" : "弯曲压头直径D";

                        if (Conversion.Val(mlwzj) < 1)
                        {
                            LwBzyq = LwBzyq + "=0" + mlwzj + "a弯曲" + mlwjd + "度后受弯曲部位表面无裂纹。";//   '至少" & CStr(mxwgs) & "个试件外侧未发生破裂"
                        }
                        else
                        {
                            LwBzyq = LwBzyq + "=" + mlwzj + "a弯曲" + mlwjd + "度后受弯曲部位表面无裂纹。";//   '至少" & CStr(mxwgs) & "个试件外侧未发生破裂"
                        }
                    }
                }
                #endregion
                sItem["G_LWZJ"] = mlwzj + "a";
                sItem["G_QFQD"] ="≥"+ Conversion.Val(mQfqd).ToString("0");
                sItem["G_KLQD"] = Conversion.Val(mKlqd).ToString("0");
                sItem["G_KLQD1"] = Conversion.Val(mklqd1).ToString("0");
                sItem["G_KLQD"] = Conversion.Val(mKlqd).ToString("0") + "～" + Conversion.Val(mklqd1).ToString("0");
                if (gclx_lb.Contains("管"))
                {
                    sItem["G_KLQD"] = "≥" + Conversion.Val(mKlqd).ToString("0");

                }
                sItem["G_SCL"] ="≥"+ mScl.ToString();
                //冷弯性能标准要求
                sItem["G_LWWZ"] = LwBzyq;
                #endregion

               
                #region  QFQD
                if (Math.Abs(GetSafeDouble(sItem["MJ"]) - 0) > 0.00001)
                {
                    for (int i = 1; i < mxlgs + 1; i++)
                    {
                        if (string.IsNullOrEmpty(sItem["QFHZ" + i]))
                        {
                            sItem["QFHZ" + i] = "0";
                        }
                        sItem["QFQD" + i] = myint(1000 * Conversion.Val(sItem["QFHZ" + i]) / Conversion.Val(sItem["MJ"])).ToString();
                    }
                }
                else
                {
                    for (int i = 1; i < mxlgs + 1; i++)
                    {
                        sItem["QFQD" + i] = "0";
                    }
                }
                #endregion

                #region  KLHZ
                if (Math.Abs(GetSafeDouble(sItem["MJ"]) - 0) > 0.00001)
                {
                    for (int i = 1; i < mxlgs + 1; i++)
                    {
                        if (string.IsNullOrEmpty(sItem["KLHZ" + i]))
                        {
                            sItem["KLHZ" + i] = "0";
                        }
                        sItem["KLQD" + i] = myint(1000 * Conversion.Val(sItem["KLHZ" + i]) / Conversion.Val(sItem["MJ"])).ToString();
                    }
                }
                else
                {
                    for (int i = 1; i < mxlgs + 1; i++)
                    {
                        sItem["KLQD" + i] = "0";
                    }
                }
                #endregion

                #region  SCZ
                if (Math.Abs(GetSafeDouble(sItem["MJ"]) - 0) > 0.00001)
                {
                    for (int i = 1; i < mxlgs + 1; i++)
                    {
                        if (string.IsNullOrEmpty(sItem["SCZ" + i]))
                        {
                            sItem["SCZ" + i] = "0";
                            sItem["SCL" + i] = "----";
                        }
                        else
                        {
                            sItem["SCL" + i] = (Math.Round(200 * (Conversion.Val(sItem["SCZ" + i]) - GetSafeDouble(sItem["CSBJ"])) / GetSafeDouble(sItem["CSBJ"]), 0) / 2).ToString();
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < mxlgs + 1; i++)
                    {
                        sItem["SCL" + i] = "0";
                    }
                }

                if ("0" == sItem["G_SCL"])
                {
                    sItem["SCL1"] = "----";
                }
                #endregion

                var mallBhg_qf = 0;
                var mallBhg_kl = 0;
                var mallBhg_lw = 0;

                //求单组屈服强度,抗拉强度,伸长率,冷弯 合格个数,并且返回值为不同组不合格数的累加值
                mallBhg_qf = mallBhg_qf + find_singlezb_bhg(MItem[0], sItem, "qf", mQfqd, (int)mxlgs);

                #region 抗拉强度
                sItem["HG_KL"] = "0";

                for (int i = 1; i < mxlgs + 1; i++)
                {
                    if (Conversion.Val(sItem["KLQD" + i]) >= GetSafeDouble(mKlqd) && Conversion.Val(sItem["KLQD" + i]) <= GetSafeDouble(mklqd1))
                        sItem["HG_KL"] = (Conversion.Val(sItem["HG_KL"]) + 1).ToString();
                    else
                        mallBhg_kl += 1;
                }
                #endregion

                #region 伸长率
                sItem["HG_SC"] = "0";
                var mallBhg_sc = 0;
                for (int i = 1; i < mxlgs + 1; i++)
                {
                    if (GetSafeDouble(sItem["SCL" + i]) - mScl >= 0)
                        sItem["HG_SC"] = (Conversion.Val(sItem["HG_SC"]) + 1).ToString();
                    else
                        mallBhg_sc += 1;
                }
                #endregion

                #region  弯曲
                if (jcxm.Contains("、冷弯、") || jcxm.Contains("、弯曲、"))
                {
                    for (int i = 1; i < GetSafeDouble(mxwgs) + 1; i++)
                    {
                        if (sItem["LW" + i] == "无裂纹")
                        {
                            sItem["LW" + i] = "1";
                        }
                        if (sItem["LW" + i] == "有裂纹")
                        {
                            sItem["LW" + i] = "0";
                        }
                        if (sItem["LW" + i] == "----")
                        {
                            sItem["LW" + i] = "-1";
                        }

                    }
                    mallBhg_lw = mallBhg_lw + find_singlezb_bhg(MItem[0], sItem, "lw", mLw, GetSafeDouble(mxwgs));
                    for (int i = 1; i < GetSafeDouble(mxwgs) + 1; i++)
                    {
                        if (sItem["LW" + i] == "1")
                        {
                            sItem["LW" + i] = "无裂纹";
                        }
                        if (sItem["LW" + i] == "0")
                        {
                            sItem["LW" + i] = "有裂纹";
                        }
                        if (sItem["LW" + i] == "-1")
                        {
                            sItem["LW" + i] = "----";
                        }

                    }
                }
                else
                {
                    sItem["LW1"] = "----";
                    sItem["LW2"] = "----";
                    
                }
                #endregion

                #region 冲击试验
                if (jcxm.Contains("、冲击试验、")&&GetSafeDouble( sItem["CJSY1"])!=0)
                {
                    jcxmCur = "冲击试验";
                    var mcjcnt = 0;
                    var mcjcnt7 = 0;
                    var mcjpj = 0.0;
                    if (extraFieldsDj["WHICH"] == "1")
                    {
                        var cjbzz = GetSafeDouble(extraFieldsDj["CJBZZ"]);
                        sItem["CJBZZ"] = "≥" + cjbzz;
                        var cjsy = Conversion.Val(sItem["CJSY1"]);
                        if (cjsy < cjbzz)
                        {
                            mcjcnt = mcjcnt + 1;
                        }
                        cjsy = Conversion.Val(sItem["CJSY2"]);

                        if (cjsy < cjbzz)
                        {
                            mcjcnt = mcjcnt + 1;
                        }

                        mcjpj = Math.Round((Conversion.Val(sItem["CJSY1"]) + Conversion.Val(sItem["CJSY2"])) / 2, 1);

                        if (mcjpj > cjbzz && mcjcnt < 1)
                        {
                            sItem["JCJG_CJ"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            sItem["JCJG_CJ"] = "复试";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        var cjbzz = GetSafeDouble(extraFieldsDj["CJBZZ"]);
                        sItem["CJBZZ"] = "≥" + cjbzz;
                        mcjpj = Math.Round((Conversion.Val(sItem["CJSY1"]) + Conversion.Val(sItem["CJSY2"]) + Conversion.Val(sItem["CJSY3"])) / 3, 1);

                        sItem["CJPJ"] = Math.Round(mcjpj, 1).ToString();

                        for (int i = 1; i < 4; i++)
                        {
                            if (Conversion.Val(sItem["CJSY" + i]) < GetSafeDouble(extraFieldsDj["CJBZZ"]) * 0.7)
                            {
                                mcjcnt7 = mcjcnt7 + 1;
                            }
                        }

                        if (mcjcnt7 >= 2)
                        {
                            jcxmCur = "冲击试验";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["JCJG_CJ"] = "不合格";
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                        else
                        {

                            sItem["JCJG_CJ"] = "合格";
                            mFlag_Hg = true;
                        }
                        sItem["CJDZHGGS"] = (3 - mcjcnt7).ToString();;
                        
                    }
                }
                else
                {
                    sItem["JCJG_CJ"] = "----";
                    sItem["CJSY1"] = "----";
                    sItem["CJSY2"] = "----";
                    sItem["CJSY3"] = "----";
                    sItem["CJPJ"] = "----";
                    sItem["CJBZZ"] = "----";
                    sItem["CJDZHGGS"] = "----";
                }
                #endregion

                all_zb_jl(MItem[0], sItem, GetSafeDouble(mHggs_qfqd), GetSafeDouble(mHggs_klqd), GetSafeDouble(mHggs_scl), GetSafeDouble(mHggs_lw));
                #endregion


                #region 拉根数==1，字段处理
                if (mxlgs == 1)
                {
                    sItem["QFHZ2"] = "----";
                    sItem["QFQD2"] = "----";
                    sItem["KLHZ2"] = "----";
                    sItem["KLQD2"] = "----";
                    sItem["LDWZ2"] = "----";
                    sItem["SCZ2"] = "----";
                    sItem["SCL2"] = "----";
                    sItem["LW2"] = "----";
                }
                #endregion

                #region 结论处理
                if (sItem["JCJG_LS"] == "不符合")
                {
                    if (GetSafeDouble(sItem["HG_QF"]) < GetSafeDouble(mHggs_qfqd))
                    {
                        jcxmCur = "屈服强度";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    if (GetSafeDouble(sItem["HG_KL"]) < GetSafeDouble(mHggs_klqd))
                    {
                        jcxmCur = "抗拉强度";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    if (GetSafeDouble(sItem["HG_SC"]) < GetSafeDouble(mHggs_lw))
                    {
                        jcxmCur = "伸长率";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    mFlag_Hg = true;
                }

                if (sItem["JCJG_LW"] == "不符合")
                {
                    jcxmCur = CurrentJcxm(jcxm, "弯曲,冷弯");
                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                }
                else
                {
                    mFlag_Hg = true;
                }

                if ("不符合" == sItem["JCJG_LS"] && "不符合" == sItem["JCJG_LW"] && "不合格" == sItem["JCJG_CJ"] && "不合格" == sItem["JCJG_YD"])
                {
                    sItem["JCJG"] = "不合格";
                    MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + "1#";
                    mAllHg = false;
                    mFlag_Bhg = true;
                }
                else
                {
                    if ("不符合" == sItem["JCJG_LS"] || "不符合" == sItem["JCJG_LW"] || "复试" == sItem["JCJG_CJ"] || "复试" == sItem["JCJG_YD"])
                    {
                        sItem["JCJG"] = "复试";
                        MItem[0]["FJJJ1"] = MItem[0]["FJJJ1"] + "1#";
                        mAllHg = false;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                        sItem["JCJG"] = "合格";
                        MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + "1#";
                    }
                }
                #region 检测结论处理
                if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"]))
                {
                    jsbeizhu ="试样"+ sItem["YPBH"]+ "依据" + MItem[0]["PDBZ"]+"中" + sItem["GCLX_PH"]+"的规定，所检项目均符合要求。";
                    MItem[0]["FJJJ3"] = "试样" + sItem["YPBH"] + "依据" + MItem[0]["PDBZ"] + "中" + sItem["GCLX_PH"] + "的规定，所检项目均符合要求。";
                }

                if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"]))
                {
                    jsbeizhu = "试样" + sItem["YPBH"] + "依据" + MItem[0]["PDBZ"] + "中" + sItem["GCLX_PH"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                    MItem[0]["FJJJ2"] = "试样" + sItem["YPBH"] + "依据" + MItem[0]["PDBZ"] + "中" + sItem["GCLX_PH"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";

                }

                if (!string.IsNullOrEmpty(MItem[0]["FJJJ1"]))
                {
                    jsbeizhu = "试样" + sItem["YPBH"] + "依据" + MItem[0]["PDBZ"] + "中" + sItem["GCLX_PH"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                    MItem[0]["FJJJ1"] = "试样" + sItem["YPBH"] + "依据" + MItem[0]["PDBZ"] + "中" + sItem["GCLX_PH"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";

                    if (mFlag_Bhg && mFlag_Hg)
                    {

                        jsbeizhu = "试样" + sItem["YPBH"] + "依据" + MItem[0]["PDBZ"] + "中" + sItem["GCLX_PH"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，另取双倍样复试。";
                        MItem[0]["FJJJ1"] = "试样" + sItem["YPBH"] + "依据" + MItem[0]["PDBZ"] + "中" + sItem["GCLX_PH"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，另取双倍样复试。";
                    }
                }
                sItem["JCJGMS"] = jsbeizhu;
                sItem["BGYPBH"] = sItem["YPBH"];
                sItem["DZJCJG"] = sItem["JCJG"];
                #endregion
            }


            #endregion

            #region 添加最终报告

            MItem[0]["JCJG"] = "----";
            MItem[0]["JCJGMS"] = "----";

            #endregion

            #endregion
        }
    }
}