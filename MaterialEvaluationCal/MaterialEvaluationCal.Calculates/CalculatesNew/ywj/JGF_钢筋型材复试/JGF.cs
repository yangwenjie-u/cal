using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JGF : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_JGF_DJ"];
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            var mJcjg = "";//记录最终报告是否不下结论
            var SItems = retData["S_JGF"];

            if (!retData.ContainsKey("M_JGF"))
            {
                retData["M_JGF"] = new List<IDictionary<string, string>>();
            }
            var MItem = retData["M_JGF"];

            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mItemHg = true;

            #region 局部函数
            Func<double, double> myint = delegate (double dataChar)
            {
                return Math.Round(Conversion.Val(dataChar) / 5, 0) * 5;
            };

            //'返回值为每组每种指标不合格总数  ' mbzValue 是单前判断指标的标准值,count 是一组中的检测个数
            Func<IDictionary<string, string>, IDictionary<string, string>, string, double, int, int> find_singlezb_bhg = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem, string zbName, double mbzValue, int count)
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
                    if (Double.Parse(sItem["HG_QF"]) >= mHggs_klqd_f && Double.Parse(sItem["HG_KL"]) >= mHggs_klqd_f && Double.Parse(sItem["HG_SC"]) >= mHggs_scl_f)
                        sItem["JCJG_LS"] = "符合";
                    else
                    {
                        sItem["JCJG_LS"] = "不符合";
                        mAllHg = false;
                        jsbeizhu += "检测项 拉伸 不合格";
                    }
                }
                else
                {
                    sItem["JCJG_LS"] = "----";
                }

                if (jcxm2.Contains("、冷弯、") || jcxm2.Contains("、弯曲、"))
                {
                    if (Double.Parse(sItem["HG_LW"]) - mHggs_lw_f > -0.00001)
                        sItem["JCJG_LW"] = "符合";
                    else
                    {
                        sItem["JCJG_LW"] = "不符合";
                        mAllHg = false;
                        jsbeizhu += "检测项 冷弯 不合格";
                    }
                }
                else
                {

                    sItem["JCJG_LW"] = "----";
                    sItem["lW1"] = "-1";
                    sItem["lW2"] = "-1";
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

            double mQfqd, mklqd1, mLw, mcj = 0;
            string mHggs_qfqd, mHggs_klqd, mHggs_scl, mHggs_lw = "";
            string mFsgs_qfqd, mFsgs_klqd, mFsgs_scl, mFsgs_lw = "";
            string mlwjd, MFFWQCS = "";
            double mlwzj = 0;
            string mxwgs = "";

            double mScl = 0;
            double mKlqd = 0;
            var LwBzyq = "";
            var mxlgs = 0;
            var mSjdj = "";
            var jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            #endregion

            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mSjdj = string.IsNullOrEmpty(sItem["SJDJ"]) ? "" : sItem["SJDJ"];

                var hd_fw = "";
                var md = 0.0;
                var hd = Conversion.Val(sItem["HD1"]);

                md = hd;
                if (md == 0)
                {
                    md = Conversion.Val(sItem["ZJ1"]);
                }

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

                #region  标准表
                var mrsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["MC"] == mSjdj.Trim() && u["ZJM"] == hd_fw.Trim());
                if (null == mrsDj)
                {
                    mrsDj = extraDJ.FirstOrDefault(u => u["PH"] == sItem["GCLX_PH"] && u["MC"] == mSjdj.Trim());
                }

                if (null == mrsDj)
                {
                    sItem["JCJG"] = "不下结论";
                    mJcjg = "不下结论";
                    jsbeizhu = "牌号" + sItem["GCLX_PH"] + mSjdj + "试件尺寸为空\r\n";
                    mAllHg = false;
                    continue;
                }
                mQfqd = Double.Parse(mrsDj["QFQDBZZ"]); //'单组标准值
                mKlqd = Double.Parse(mrsDj["KLQDBZZ"]);
                mklqd1 = Double.Parse(mrsDj["KLQDBZZ1"]);
                mScl = Double.Parse(mrsDj["SCLBZZ"]);
                mLw = Double.Parse(mrsDj["LWBZZ"]);
                mcj = Double.Parse(mrsDj["CJBZZ"]);

                mHggs_qfqd = mrsDj["ZHGGS_QFQD"];//'单组合格个数
                mHggs_klqd = mrsDj["ZHGGS_KLQD"];
                mHggs_scl = mrsDj["ZHGGS_SCL"];
                mHggs_lw = mrsDj["ZHGGS_LW"];

                mFsgs_qfqd = mrsDj["ZFSGS_QFQD"];// '单组复试不合格个数
                mFsgs_klqd = mrsDj["ZFSGS_KLQD"];
                mFsgs_scl = mrsDj["ZFSGS_SCL"];
                mFsgs_lw = mrsDj["ZFSGS_LW"];

                mlwzj = Double.Parse(mrsDj["LWZJ"]);// '冷弯直径和角度
                mlwjd = mrsDj["LWJD"];
                MFFWQCS = mrsDj["FFWQCS"];

                mxlgs = (int)Double.Parse(mrsDj["XLGS"]);
                mxwgs = mrsDj["XWGS"];
                sItem["G_CJ"] = mrsDj["CJBZZ"];

                #endregion

                #region 检测项
                var gclx_lb = sItem["GCLX_LB"];
                if ((!gclx_lb.Contains("板") && !gclx_lb.Contains("带")) && MItem[0]["PDBZ"].Contains("GB/T 700-2006《碳素结构钢》"))
                {
                    mlwzj = mlwzj - 0.5;
                }
                if ((gclx_lb.Contains("板") || gclx_lb.Contains("带")) && MItem[0]["PDBZ"].Contains("GB/T 700-2006《碳素结构钢》"))
                {
                    if (sItem["QYFX"] == "横向")
                    {
                        mScl = Math.Round(mScl * 0.98, 0);
                    }
                }

                if (Conversion.Val(sItem["HD1"]) > 100 && MItem[0]["PDBZ"].Contains("GB/T 700-2006《碳素结构钢》"))
                {
                    mKlqd = mKlqd - 20;
                }

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

                sItem["G_QFQD"] = Conversion.Val(mQfqd).ToString();
                sItem["G_KLQD"] = mKlqd.ToString();
                sItem["G_KLQD1"] = Conversion.Val(mklqd1).ToString();
                sItem["G_SCL"] = mScl.ToString();
                sItem["G_LWWZ"] = LwBzyq;


                #region 求伸长率
                sItem["XGM"] = mrsDj["XGM"];

                for (int i = 1; i < mxlgs + 1; i++)
                {
                    if (0 == Conversion.Val(sItem["ZJ" + i]))
                    {
                        sItem["MJ" + i] = (double.Parse(sItem["HD" + i]) * double.Parse(sItem["KD" + i])).ToString("0.0000");
                        sItem["GG" + i] = "宽:" + sItem["KD" + i] + "\n厚:" + sItem["HD" + i];
                        sItem["CD" + i] = myint(5.65 * Math.Sqrt(Conversion.Val(sItem["MJ" + i]))).ToString();
                    }
                    else
                    {
                        sItem["MJ" + i] = Math.Round(3.14159 * Conversion.Val(sItem["ZJ" + i]) / 2 * Conversion.Val(sItem["ZJ" + i]) / 2, 5).ToString();
                        sItem["GG" + i] = "Φ:" + sItem["ZJ" + i];
                        sItem["CD" + i] = myint(Conversion.Val(sItem["XGM"]) * Conversion.Val(sItem["ZJ" + i])).ToString();

                    }

                    if (Math.Abs(Conversion.Val(sItem["XGM"]) - 100) < 0.00001)
                    {
                        sItem["CD" + i] = "100";
                    }
                }
                #endregion

                #region  QFQD

                for (int i = 1; i < mxlgs + 1; i++)
                {
                    if (string.IsNullOrEmpty(sItem["QFHZ" + i]))
                    {
                        sItem["QFHZ" + i] = "0";
                    }
                    if (0 == Conversion.Val(sItem["MJ" + i]))
                    {
                        sItem["QFHZ" + i] = "0";
                    }
                    else
                    {
                        sItem["QFQD" + i] = myint(1000 * Conversion.Val(sItem["QFHZ" + i]) / Conversion.Val(sItem["MJ" + i])).ToString();
                    }
                }
                #endregion

                #region  KLHZ

                for (int i = 1; i < mxlgs + 1; i++)
                {
                    if (string.IsNullOrEmpty(sItem["KLHZ" + i]))
                    {
                        sItem["KLHZ" + i] = "0";
                    }
                    if (0 == Conversion.Val(sItem["MJ" + i]))
                    {
                        sItem["KLQD" + i] = "0";
                    }
                    else
                    {
                        sItem["KLQD" + i] = myint(1000 * Conversion.Val(sItem["KLHZ" + i]) / Conversion.Val(sItem["MJ" + i])).ToString();
                    }
                }

                #endregion
                #region  SCZ

                for (int i = 1; i < mxlgs + 1; i++)
                {
                    if (string.IsNullOrEmpty(sItem["SCZ" + i]))
                    {
                        sItem["SCZ" + i] = "0";
                    }
                    else
                    {
                        if (0 == Conversion.Val(sItem["CD" + i]))
                        {
                            sItem["SCL" + i] = "0";
                        }
                        else
                        {
                            sItem["SCL" + i] = (Math.Round(200 * (Conversion.Val(sItem["SCZ" + i]) - double.Parse(sItem["CD" + i])) / double.Parse(sItem["CD" + i]), 0) / 2).ToString();
                        }
                    }
                }

                #endregion

                var mallBhg_qf = 0;
                var mallBhg_kl = 0;
                var mallBhg_lw = 0;

                //求单组屈服强度,抗拉强度,伸长率,冷弯 合格个数,并且返回值为不同组不合格数的累加值
                mallBhg_qf = mallBhg_qf + find_singlezb_bhg(MItem[0], sItem, "qf", mQfqd, mxlgs);
                sItem["HG_KL"] = "0";

                for (int i = 1; i < mxlgs + 1; i++)
                {
                    if (Conversion.Val(sItem["KLQD" + i]) >= mKlqd && Conversion.Val(sItem["KLQD" + i]) <= mklqd1)
                        sItem["HG_KL"] = (Conversion.Val(sItem["HG_KL"]) + 1).ToString();
                    else
                        mallBhg_kl += 1;
                }

                //伸长率
                sItem["HG_SC"] = "0";
                var mallBhg_sc = 0;
                for (int i = 1; i < mxlgs + 1; i++)
                {
                    if (Conversion.Val(sItem["SCL" + i]) - mScl > 0.00001)
                        sItem["HG_SC"] = (Conversion.Val(sItem["HG_SC"]) + 1).ToString();
                    else
                        mallBhg_sc += 1;
                }

                if (jcxm.Contains("、冷弯、") || jcxm.Contains("、弯曲、"))
                {
                    mallBhg_lw = mallBhg_lw + find_singlezb_bhg(MItem[0], sItem, "lw", mLw, (int)Double.Parse(mxwgs));
                }

                if (jcxm.Contains("、冲击试验、"))
                {
                    var mcjcnt = 0;
                    var mcjcnt7 = 0;
                    var mcjpj = 0.0;
                    if (mrsDj["WHICH"] == "1")
                    {
                        var cjbzz = double.Parse(mrsDj["CJBZZ"]);
                        if (Conversion.Val(sItem["CJSY1"]) < cjbzz)
                        {
                            mcjcnt = mcjcnt + 1;
                        }
                        if (Conversion.Val(sItem["CJSY2"]) < cjbzz)
                        {
                            mcjcnt = mcjcnt + 1;
                        }
                        if (Conversion.Val(sItem["CJSY3"]) < cjbzz)
                        {
                            mcjcnt = mcjcnt + 1;
                        }
                        if (Conversion.Val(sItem["CJSY4"]) < cjbzz)
                        {
                            mcjcnt = mcjcnt + 1;
                        }

                        mcjpj = Math.Round((Conversion.Val(sItem["CJSY1"]) + Conversion.Val(sItem["CJSY2"]) + Conversion.Val(sItem["CJSY3"]) + Conversion.Val(sItem["CJSY4"])) / 4, 1);

                        if (mcjpj > cjbzz && mcjcnt < 1)
                        {
                            sItem["JCJG_CJ"] = "合格";
                        }
                        else
                        {
                            sItem["JCJG_CJ"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        mcjpj = Math.Round((Conversion.Val(sItem["CJSY1"]) + Conversion.Val(sItem["CJSY2"]) + Conversion.Val(sItem["CJSY3"]) + Conversion.Val(sItem["CJSY4"]) + Conversion.Val(sItem["CJSY5"]) + Conversion.Val(sItem["CJSY6"])) / 6, 1);
                        sItem["CJPJ"] = Math.Round(mcjpj, 1).ToString();


                        for (int i = 1; i < 7; i++)
                        {
                            if (Conversion.Val(sItem["CJSY" + i]) < double.Parse(mrsDj["CJBZZ"]) * 0.7)
                            {
                                mcjcnt7 = mcjcnt7 + 1;
                            }

                            if (Conversion.Val(sItem["CJSY" + i]) < double.Parse(mrsDj["CJBZZ"]))
                            {
                                mcjcnt = mcjcnt + 1;
                            }
                        }

                        if (mcjcnt >= double.Parse(mrsDj["CJBZZ"]) && mcjcnt7 < 2 && mcjcnt < 3)
                        {
                            sItem["JCJG_CJ"] = "合格";
                        }
                        else
                        {
                            sItem["JCJG_CJ"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                }
                else
                {
                    sItem["JCJG_CJ"] = "----";
                    sItem["CJSY1"] = "----";
                    sItem["CJSY2"] = "----";
                    sItem["CJSY3"] = "----";
                    sItem["CJPJ"] = "----";
                }

                all_zb_jl(MItem[0], sItem, double.Parse(mHggs_qfqd), double.Parse(mHggs_klqd), double.Parse(mHggs_scl), double.Parse(mHggs_lw));

                #endregion

                if (sItem["JCJG_LS"] == "不符合")
                {
                    if (double.Parse(sItem["HG_QF"]) < double.Parse(mHggs_qfqd))
                    {
                        jcxmCur = "屈服强度";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    if (double.Parse(sItem["HG_KL"]) < double.Parse(mHggs_klqd))
                    {
                        jcxmCur = "抗拉强度";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    if (double.Parse(sItem["HG_SC"]) < double.Parse(mHggs_lw))
                    {
                        jcxmCur = "伸长率";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                if (sItem["JCJG_LW"] == "不符合")
                {
                    jcxmCur = CurrentJcxm(jcxm, "弯曲,冷弯");
                    jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                }



                if (sItem["JCJG_LS"].Contains("不") || sItem["JCJG_LW"].Contains("不") || sItem["JCJG_CJ"].Contains("不") || sItem["JCJG_YD"].Contains("不"))
                {
                    sItem["JCJG"] = "不合格";
                    MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + "1#";
                    mAllHg = false;
                    jsbeizhu += "该组试样的检测结果不合格\r\n";
                }
                else
                {
                    sItem["JCJG"] = "合格";
                    MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + "1#";

                }
            }

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"]))
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目复试均符合要求。";
                MItem[0]["FJJJ3"] = jsbeizhu;
            }

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"]))
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "复试不符合要求。";
                MItem[0]["FJJJ2"] = jsbeizhu;
            }
            #region 添加最终报告


            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
        }
    }
}
