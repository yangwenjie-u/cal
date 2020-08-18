using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JCL : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_JCL_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_JCL"];
            //var ZM_DRJL = data["ZM_DRJL"];

            if (!data.ContainsKey("M_JCL"))
            {
                data["M_JCL"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_JCL"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mItemHg = true;
            var mJSFF = "";
            var jcxm = "";

            double mYqpjz, mXdy21, mDy21 = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            bool sign = false;
            double md1, md2, md, sum, Gs, pjmd = 0;
            List<double> nArr = new List<double>();
            int mcd, mdwz, xd = 0;
            var jcxmBhg = "";
            var jcxmCur = "";

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mFlag_Hg = true;
                sign = true;
                mbhggs = 0;
                if (jcxm.Contains("、导热系数、"))
                {
                    jcxmCur = "导热系数";
                    mcd = mItem["G_DRXS"].Length;
                    mdwz = mItem["G_DRXS"].IndexOf('.');
                    mcd = mcd - mdwz + 1;

                    string DEVCODE = String.IsNullOrEmpty(mItem["DEVCODE"]) ? "" : mItem["DEVCODE"];
                    if (DEVCODE == "" && DEVCODE.Contains("XCS17-067") || DEVCODE.Contains("XCS17-066"))
                    {
                        //var mrsDrxs = ZM_DRJL.FirstOrDefault(u => u["SYLB"].ToUpper() == "JA" && u["SYBH"] == mItem["JYDBH"]);
                        //sItem["DRXS2"] = mrsDrxs["DRXS"];
                        //mItem["JCYJ"] = mItem["JCYJ"].Replace("10294", "10295");
                    }

                    sItem["DRXS"] = Math.Round(GetSafeDouble(sItem["DRXS"]), mcd).ToString();

                    mItem["HG_DRXS"] = IsQualified(mItem["G_DRXS"], sItem["DRXS"], true);

                    if (mItem["HG_DRXS"] == "不符合")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }

                }
                else
                {
                    sItem["DRXS"] = "----";
                    mItem["HG_DRXS"] = "----";
                    mItem["G_DRXS"] = "----";
                }

                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                    if (Conversion.Val(sItem["KYQD"]) == 0)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["HG_KYQD"] = IsQualified(mItem["G_KYQD"], sItem["KYQD"], true);

                        if (mItem["HG_KYQD"] == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sItem["KYQD"] = "----";
                    mItem["HG_KYQD"] = "----";
                    mItem["G_KYQD"] = "----";
                }


                if (jcxm.Contains("、抗拉强度、"))
                {
                    jcxmCur = "抗拉强度";
                    if ("无效" == mItem["HG_KLQD"].Trim())
                    {
                        sItem["KLQD"] = "夹具与胶粘剂界面破坏   " + sItem["KLQD"];
                    }
                    else
                    {
                        if (Conversion.Val(sItem["KLQD"]) == 0)
                        {
                            return false;
                        }
                        mItem["HG_KLQD"] = IsQualified(mItem["G_KLQD"], sItem["KLQD"], true);

                        if (mItem["HG_KLQD"] == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sItem["KLQD"] = "----";
                    mItem["HG_KLQD"] = "----";
                    mItem["G_KLQD"] = "----";
                }

                if (jcxm.Contains("、拉伸粘结强度、"))
                {
                    jcxmCur = "拉伸粘结强度";
                    for (xd = 1; xd < 5; xd++)
                    {
                        if ("无效" == mItem["HG_YSQD" + xd])
                        {
                            sItem["PHJM" + xd] = "夹具界面破坏  ";
                        }
                        else if (mItem["HG_YSQD" + xd] == "砂浆界面破坏,不合格" || sItem["PHJM" + xd].Trim() == "砂浆界面破坏")
                        {
                            sItem["PHJM" + xd] = "砂浆界面破坏  ";
                            mItem["HG_YSQD" + xd] = "不合格";
                        }
                        else if (mItem["HG_YSQD" + xd] == "聚苯板界面破坏,不合格" || sItem["PHJM" + xd].Trim() == "聚苯板界面破坏")
                        {
                            sItem["PHJM" + xd] = "聚苯板界面破坏  ";
                            mItem["HG_YSQD" + xd] = "不合格";
                        }
                        else
                        {
                            if (sItem["YSQD" + xd] == "----")
                                sItem["PHJM" + xd] = "";
                            else
                            {
                                sItem["PHJM" + xd] = "浆料破坏";
                                mItem["HG_YSQD" + xd] = IsQualified(mItem["G_YSQD" + xd], sItem["YSQD" + xd], false);
                            }
                        }

                        if (mItem["HG_YSQD" + xd] != "不合格" && mItem["HG_YSQD" + xd] != "无效")
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = false;
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    for (xd = 1; xd < 5; xd++)
                    {
                        sItem["YSQD" + xd] = "----";
                        mItem["HG_YSQD" + xd] = "----";
                        mItem["G_YSQD" + xd] = "----";
                    }
                }


                if (jcxm.Contains("、压剪粘结强度、"))
                {
                    jcxmCur = "压剪粘结强度";
                    if (Conversion.Val(sItem["YSQD"]) == 0)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["HG_YSQD"] = IsQualified(mItem["G_YSQD"], sItem["YSQD"], true);

                        if (mItem["HG_YSQD"] == "不符合")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = false;
                            mAllHg = false;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sItem["YSQD"] = "----";
                    mItem["HG_YSQD"] = "----";
                    mItem["G_YSQD"] = "----";
                }

                if (jcxm.Contains("、干表观密度、"))
                {
                    jcxmCur = "干表观密度";
                    mItem["HG_BGMD"] = IsQualified(mItem["G_BGMD"], sItem["BGMD"], true);

                    if (mItem["HG_BGMD"] == "不符合")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["BGMD"] = "----";
                    mItem["HG_BGMD"] = "----";
                    mItem["G_BGMD"] = "----";
                }

                if (jcxm.Contains("、湿表观密度、"))
                {
                    jcxmCur = "湿表观密度";
                    mItem["HG_SBGMD"] = IsQualified(mItem["G_SBGMD"], sItem["SBGMD"], true);

                    if (mItem["HG_SBGMD"] == "不符合")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["SBGMD"] = "----";
                    mItem["HG_SBGMD"] = "----";
                    mItem["G_SBGMD"] = "----";
                }

                if (jcxm.Contains("、线性收缩率、"))
                {
                    jcxmCur = "线性收缩率";
                    mItem["HG_XXSSL"] = IsQualified(mItem["G_XXSSL"], sItem["XXSSL"], true);

                    if (mItem["HG_XXSSL"] == "不符合")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["XXSSL"] = "----";
                    mItem["HG_XXSSL"] = "----";
                    mItem["G_XXSSL"] = "----";
                }


                if (jcxm.Contains("、软化系数、"))
                {
                    jcxmCur = "软化系数";
                    mItem["HG_RHXS"] = IsQualified(mItem["G_RHXS"], sItem["RHXS"], true);

                    if ("不符合" == mItem["HG_RHXS"])
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["RHXS"] = "----";
                    mItem["HG_RHXS"] = "----";
                    mItem["G_RHXS"] = "----";
                }

                if (jcxm.Contains("、难燃性、"))
                {
                    jcxmCur = "难燃性";
                    mItem["HG_NRX"] = sItem["NRX"] == "符合" ? "合格" : "不合格";

                    if ("不符合" == mItem["HG_NRX"])
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mbhggs = mbhggs + 1;
                        mFlag_Bhg = false;
                        mAllHg = false;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["NRX"] = "----";
                    mItem["HG_NRX"] = "----";
                    mItem["G_NRX"] = "----";
                }

                if (mbhggs == 0)
                {
                    jsbeizhu = "依据" + mItem["PDBZ"] + "的规定，所检项目均符合要求。";
                    sItem["JCJG"] = "合格";
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                    //if (mFlag_Bhg && mFlag_Hg)
                    //{
                    //    jsbeizhu = "该组试件所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                    //}
                }
                return mAllHg;
            };

            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"].ToUpper() == sItem["CPMC"].Trim().ToUpper());

                if (mrsDj != null)
                {
                    MItem[0]["G_DRXS"] = mrsDj["DRXS"];
                    MItem[0]["G_KYQD"] = mrsDj["KYQD"];
                    MItem[0]["G_YSQD"] = mrsDj["YSQD"];
                    MItem[0]["G_BGMD"] = mrsDj["BGMD"];
                    MItem[0]["G_SBGMD"] = mrsDj["SBGMD"];
                    MItem[0]["G_XXSSL"] = mrsDj["XXSSL"];
                    MItem[0]["G_RHXS"] = mrsDj["RHXS"];
                    MItem[0]["G_NRX"] = mrsDj["NRX"];
                    MItem[0]["G_YSQD1"] = mrsDj["YSQD"];
                    MItem[0]["G_YSQD2"] = mrsDj["YSQD2"];
                    MItem[0]["G_YSQD3"] = mrsDj["YSQD3"];
                    MItem[0]["G_YSQD4"] = mrsDj["YSQD4"];
                    MItem[0]["G_KLQD"] = mrsDj["KLQD"];
                    MItem[0]["G_LSZJQDSJBZ"] = mrsDj["YSQD"];
                    MItem[0]["G_LSZJQDSJQS"] = mrsDj["YSQD2"];
                    MItem[0]["G_LSZJQDJBBBZ"] = mrsDj["YSQD3"];
                    MItem[0]["G_LSZJQDJBBQS"] = mrsDj["YSQD4"];
                    //MItem[0]["WHICH"] = mrsDj["WHICH"];
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "不下结论";
                    jsbeizhu = "依据不详";
                    mAllHg = false;
                    continue;
                }
                //    If InStr(1, Trim(mrsmainTable!QRBM), "90") > 0 Then
                // mrsmainTable!which = "bgjcl_99、" + mrsmainTable!which
                //End If

                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {
                    #region  导热系数
                    if (jcxm.Contains("、导热系数、"))
                    {
                        jcxmCur = "导热系数";
                        if (IsQualified(MItem[0]["G_DRXS"], sItem["DRXS"], false) == "合格")
                        {
                            MItem[0]["HG_DRXS"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_DRXS"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["DRXS"] = "----";
                        MItem[0]["HG_DRXS"] = "----";
                        MItem[0]["G_DRXS"] = "----";
                    }
                    #endregion

                    #region 抗压强度
                    if (jcxm.Contains("、抗压强度、") && Conversion.Val(sItem["KYHZ1"]) > 0)
                    {
                        jcxmCur = "抗压强度";
                        List<double> lArray = new List<double>();
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            #region 复检
                            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0, mMj9 = 0, mMj10 = 0, mMj11 = 0, mMj12 = 0;
                            double mKyqd1 = 0, mKyqd2 = 0, mKyqd3 = 0, mKyqd4 = 0, mKyqd5 = 0, mKyqd6 = 0, mKyqd7 = 0, mKyqd8 = 0, mKyqd9 = 0, mKyqd10 = 0, mKyqd11 = 0, mKyqd12 = 0;
                            mMj1 = (Conversion.Val(sItem["KYCD1"])) * (Conversion.Val(sItem["KYKD1"]));
                            mMj2 = (Conversion.Val(sItem["KYCD2"])) * (Conversion.Val(sItem["KYKD2"]));
                            mMj3 = (Conversion.Val(sItem["KYCD3"])) * (Conversion.Val(sItem["KYKD3"]));
                            mMj4 = (Conversion.Val(sItem["KYCD4"])) * (Conversion.Val(sItem["KYKD4"]));
                            mMj5 = (Conversion.Val(sItem["KYCD5"])) * (Conversion.Val(sItem["KYKD5"]));
                            mMj6 = (Conversion.Val(sItem["KYCD6"])) * (Conversion.Val(sItem["KYKD6"]));
                            mMj7 = (Conversion.Val(sItem["KYCD7"])) * (Conversion.Val(sItem["KYKD7"]));
                            mMj8 = (Conversion.Val(sItem["KYCD8"])) * (Conversion.Val(sItem["KYKD8"]));
                            mMj9 = (Conversion.Val(sItem["KYCD9"])) * (Conversion.Val(sItem["KYKD9"]));
                            mMj10 = (Conversion.Val(sItem["KYCD10"])) * (Conversion.Val(sItem["KYKD10"]));
                            mMj11 = (Conversion.Val(sItem["KYCD11"])) * (Conversion.Val(sItem["KYKD11"]));
                            mMj12 = (Conversion.Val(sItem["KYCD12"])) * (Conversion.Val(sItem["KYKD12"]));

                            sItem["KYMJ1"] = mMj1.ToString();
                            sItem["KYMJ2"] = mMj2.ToString();
                            sItem["KYMJ3"] = mMj3.ToString();
                            sItem["KYMJ4"] = mMj4.ToString();
                            sItem["KYMJ5"] = mMj5.ToString();
                            sItem["KYMJ6"] = mMj6.ToString();
                            sItem["KYMJ7"] = mMj7.ToString();
                            sItem["KYMJ8"] = mMj8.ToString();
                            sItem["KYMJ9"] = mMj9.ToString();
                            sItem["KYMJ10"] = mMj10.ToString();
                            sItem["KYMJ11"] = mMj11.ToString();
                            sItem["KYMJ12"] = mMj12.ToString();

                            //if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0)
                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0 && mMj6 != 0 && mMj7 != 0 && mMj8 != 0 && mMj9 != 0 && mMj10 != 0 && mMj11 != 0 && mMj12 != 0)
                            {
                                mKyqd1 = Round(Conversion.Val(sItem["KYHZ1"]) / mMj1, 3);
                                mKyqd2 = Round(Conversion.Val(sItem["KYHZ2"]) / mMj2, 3);
                                mKyqd3 = Round(Conversion.Val(sItem["KYHZ3"]) / mMj3, 3);
                                mKyqd4 = Round(Conversion.Val(sItem["KYHZ4"]) / mMj4, 3);
                                mKyqd5 = Round(Conversion.Val(sItem["KYHZ5"]) / mMj5, 3);
                                mKyqd6 = Round(Conversion.Val(sItem["KYHZ6"]) / mMj6, 3);
                                mKyqd7 = Round(Conversion.Val(sItem["KYHZ7"]) / mMj7, 3);
                                mKyqd8 = Round(Conversion.Val(sItem["KYHZ8"]) / mMj8, 3);
                                mKyqd9 = Round(Conversion.Val(sItem["KYHZ9"]) / mMj9, 3);
                                mKyqd10 = Round(Conversion.Val(sItem["KYHZ10"]) / mMj10, 3);
                                mKyqd11 = Round(Conversion.Val(sItem["KYHZ11"]) / mMj11, 3);
                                mKyqd12 = Round(Conversion.Val(sItem["KYHZ12"]) / mMj12, 3);
                            }
                            else
                            {
                                mKyqd1 = 0;
                                mKyqd2 = 0;
                                mKyqd3 = 0;
                                mKyqd4 = 0;
                                mKyqd5 = 0;
                                mKyqd6 = 0;
                                mKyqd7 = 0;
                                mKyqd8 = 0;
                                mKyqd9 = 0;
                                mKyqd10 = 0;
                                mKyqd11 = 0;
                                mKyqd12 = 0;
                            }
                            sItem["KYQD1"] = mKyqd1.ToString();
                            sItem["KYQD2"] = mKyqd2.ToString();
                            sItem["KYQD3"] = mKyqd3.ToString();
                            sItem["KYQD4"] = mKyqd4.ToString();
                            sItem["KYQD5"] = mKyqd5.ToString();
                            sItem["KYQD6"] = mKyqd6.ToString();
                            sItem["KYQD7"] = mKyqd6.ToString();
                            sItem["KYQD8"] = mKyqd6.ToString();
                            sItem["KYQD9"] = mKyqd6.ToString();
                            sItem["KYQD10"] = mKyqd6.ToString();
                            sItem["KYQD11"] = mKyqd6.ToString();
                            sItem["KYQD12"] = mKyqd6.ToString();

                            lArray.Add(mKyqd1);
                            lArray.Add(mKyqd2);
                            lArray.Add(mKyqd3);
                            lArray.Add(mKyqd4);
                            lArray.Add(mKyqd5);
                            lArray.Add(mKyqd6);
                            lArray.Add(mKyqd7);
                            lArray.Add(mKyqd8);
                            lArray.Add(mKyqd9);
                            lArray.Add(mKyqd10);
                            lArray.Add(mKyqd11);
                            lArray.Add(mKyqd12);
                            lArray.Sort();
                            #endregion
                        }
                        else
                        {
                            #region 初检
                            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0;
                            double mKyqd1 = 0, mKyqd2 = 0, mKyqd3 = 0, mKyqd4 = 0, mKyqd5 = 0, mKyqd6 = 0;
                            mMj1 = (Conversion.Val(sItem["KYCD1"])) * (Conversion.Val(sItem["KYKD1"]));
                            mMj2 = (Conversion.Val(sItem["KYCD2"])) * (Conversion.Val(sItem["KYKD2"]));
                            mMj3 = (Conversion.Val(sItem["KYCD3"])) * (Conversion.Val(sItem["KYKD3"]));
                            mMj4 = (Conversion.Val(sItem["KYCD4"])) * (Conversion.Val(sItem["KYKD4"]));
                            mMj5 = (Conversion.Val(sItem["KYCD5"])) * (Conversion.Val(sItem["KYKD5"]));
                            mMj6 = (Conversion.Val(sItem["KYCD6"])) * (Conversion.Val(sItem["KYKD6"]));
                            sItem["KYMJ1"] = mMj1.ToString();
                            sItem["KYMJ2"] = mMj2.ToString();
                            sItem["KYMJ3"] = mMj3.ToString();
                            sItem["KYMJ4"] = mMj4.ToString();
                            sItem["KYMJ5"] = mMj5.ToString();
                            sItem["KYMJ6"] = mMj6.ToString();

                            //if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0)
                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0 && mMj6 != 0)
                            {
                                mKyqd1 = Round(Conversion.Val(sItem["KYHZ1"]) / mMj1, 3);
                                mKyqd2 = Round(Conversion.Val(sItem["KYHZ2"]) / mMj2, 3);
                                mKyqd3 = Round(Conversion.Val(sItem["KYHZ3"]) / mMj3, 3);
                                mKyqd4 = Round(Conversion.Val(sItem["KYHZ4"]) / mMj4, 3);
                                mKyqd5 = Round(Conversion.Val(sItem["KYHZ5"]) / mMj5, 3);
                                mKyqd6 = Round(Conversion.Val(sItem["KYHZ6"]) / mMj6, 3);
                            }
                            else
                            {
                                mKyqd1 = 0;
                                mKyqd2 = 0;
                                mKyqd3 = 0;
                                mKyqd4 = 0;
                                mKyqd5 = 0;
                                mKyqd6 = 0;
                            }
                            sItem["KYQD1"] = mKyqd1.ToString();
                            sItem["KYQD2"] = mKyqd2.ToString();
                            sItem["KYQD3"] = mKyqd3.ToString();
                            sItem["KYQD4"] = mKyqd4.ToString();
                            sItem["KYQD5"] = mKyqd5.ToString();
                            sItem["KYQD6"] = mKyqd6.ToString();


                            lArray.Add(mKyqd1);
                            lArray.Add(mKyqd2);
                            lArray.Add(mKyqd3);
                            lArray.Add(mKyqd4);
                            lArray.Add(mKyqd5);
                            lArray.Add(mKyqd6);
                            lArray.Sort();
                            #endregion
                        }
                        double mMaxKyqd = 0, mMinKyqd = 0, mAvgKyqd = 0;
                        mMaxKyqd = lArray.Max();
                        mMinKyqd = lArray.Min();
                        mAvgKyqd = lArray.Average();

                        //if (Math.Abs(mAvgKyqd - 0) > 0.00001 && Math.Abs(mMaxKyqd - 0) > 0.000001)
                        //{
                        //    if ((mMaxKyqd - mAvgKyqd) > mAvgKyqd * 0.2 || (mAvgKyqd - mMinKyqd) > mAvgKyqd * 0.2)
                        //    {
                        //        lArray.Remove(lArray.Max());
                        //        lArray.Remove(lArray.Min());
                        //        mAvgKyqd = lArray.Average();
                        //    }
                        //}
                        sItem["KYQD"] = mAvgKyqd.ToString("0.00");

                        if (IsQualified(MItem[0]["G_KYQD"], sItem["KYQD"], false) == "合格")
                        {
                            MItem[0]["HG_KYQD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_KYQD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["KYQD"] = "----";
                        MItem[0]["HG_KYQD"] = "----";
                        MItem[0]["G_KYQD"] = "----";
                    }
                    #endregion

                    #region 拉伸粘结强度
                    if (jcxm.Contains("、拉伸粘结强度、"))
                    {
                        jcxmCur = "拉伸粘结强度";
                        List<double> lArray = new List<double>();
                        int n = 7; //循环次数
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            n = 13;
                        }

                        #region 与水泥砂浆

                        #region 标准状态
                        for (int i = 1; i < n; i++)
                        {
                            lArray.Add(GetSafeDouble(sItem["LSZJQDSJBZ" + i]));
                        }
                        lArray.Sort();
                        lArray.Remove(lArray.Max());
                        lArray.Remove(lArray.Min());

                        if (sItem["CPMC"].Contains("抹灰浆料"))
                        {
                            sItem["LSZJQDSJBZ"] = lArray.Average().ToString("0.0");
                            if (IsQualified(MItem[0]["G_LSZJQDSJBZ"], sItem["LSZJQDSJBZ"]) == "合格")
                            {
                                MItem[0]["HG_LSZJQDSJBZ"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                MItem[0]["HG_LSZJQDSJBZ"] = "不合格";
                                jcxmCur = "拉伸粘结强度(与水泥砂浆标准状态)";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else if (sItem["CPMC"].Contains("贴砌浆料"))
                        {
                            sItem["LSZJQDSJBZ"] = lArray.Average().ToString("0.00");
                            if (IsQualified(MItem[0]["G_LSZJQDSJBZ"], sItem["LSZJQDSJBZ"]) == "合格" && sItem["LSZJQDSJBZPHDW"] == "破坏部位不位于界面")
                            {
                                MItem[0]["HG_LSZJQDSJBZ"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                MItem[0]["HG_LSZJQDSJBZ"] = "不合格";
                                jcxmCur = "拉伸粘结强度(与水泥砂浆标准状态)";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }


                        #endregion

                        #region 浸水处理
                        lArray.Clear();
                        for (int i = 1; i < n; i++)
                        {
                            lArray.Add(GetSafeDouble(sItem["LSZJQDSJQS" + i]));
                        }
                        lArray.Sort();
                        lArray.Remove(lArray.Max());
                        lArray.Remove(lArray.Min());

                        if (sItem["CPMC"].Contains("抹灰浆料"))
                        {
                            sItem["LSZJQDSJQS"] = lArray.Average().ToString("0.0");
                            if (IsQualified(MItem[0]["G_LSZJQDSJQS"], sItem["LSZJQDSJQS"]) == "合格")
                            {
                                MItem[0]["HG_LSZJQDSJQS"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                MItem[0]["HG_LSZJQDSJQS"] = "不合格";
                                jcxmCur = "拉伸粘结强度(与水泥砂浆浸水处理)";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else if (sItem["CPMC"].Contains("贴砌浆料"))
                        {
                            sItem["LSZJQDSJQS"] = lArray.Average().ToString("0.00");
                            if (IsQualified(MItem[0]["G_LSZJQDSJQS"], sItem["LSZJQDSJQS"]) == "合格" && sItem["LSZJQDSJQSPHDW"] == "破坏部位不位于界面")
                            {
                                MItem[0]["HG_LSZJQDSJQS"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                MItem[0]["HG_LSZJQDSJQS"] = "不合格";
                                jcxmCur = "拉伸粘结强度(与水泥砂浆浸水处理)";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }



                        #endregion


                        #endregion

                        #region 与聚苯板
                        if (sItem["CPMC"].Contains("抹灰浆料"))
                        {
                            sItem["LSZJQDJBBBZ"] = "----";
                            MItem[0]["G_LSZJQDJBBBZ"] = "----";
                            MItem[0]["HG_LSZJQDJBBBZ"] = "----";
                            sItem["LSZJQDJBBQS"] = "----";
                            MItem[0]["G_LSZJQDJBBQS"] = "----";
                            MItem[0]["HG_LSZJQDJBBQS"] = "----";
                        }
                        else if (sItem["CPMC"].Contains("贴砌浆料"))
                        {
                            #region 标准状态
                            lArray.Clear();
                            for (int i = 1; i < n; i++)
                            {
                                lArray.Add(GetSafeDouble(sItem["LSZJQDJBBBZ" + i]));
                            }
                            lArray.Sort();
                            lArray.Remove(lArray.Max());
                            lArray.Remove(lArray.Min());
                            sItem["LSZJQDJBBBZ"] = lArray.Average().ToString("0.00");
                            if (IsQualified(MItem[0]["G_LSZJQDJBBBZ"], sItem["LSZJQDJBBBZ"], false) == "合格" && sItem["LSZJQDJBBBZPHDW"] == "破坏部位不位于界面")
                            {
                                MItem[0]["HG_LSZJQDJBBBZ"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                MItem[0]["HG_LSZJQDJBBBZ"] = "不合格";
                                jcxmCur = "拉伸粘结强度(与聚苯板标准状态)";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                            #endregion

                            #region 浸水处理
                            lArray.Clear();
                            for (int i = 1; i < n; i++)
                            {
                                lArray.Add(GetSafeDouble(sItem["LSZJQDJBBQS" + i]));
                            }
                            lArray.Sort();
                            lArray.Remove(lArray.Max());
                            lArray.Remove(lArray.Min());
                            sItem["LSZJQDJBBQS"] = lArray.Average().ToString("0.00");
                            if (IsQualified(MItem[0]["G_LSZJQDJBBQS"], sItem["LSZJQDJBBQS"], false) == "合格" && sItem["LSZJQDJBBQSPHDW"] == "破坏部位不位于界面")
                            {
                                MItem[0]["HG_LSZJQDJBBQS"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                MItem[0]["HG_LSZJQDJBBQS"] = "不合格";
                                jcxmCur = "拉伸粘结强度(与聚苯板浸水处理)";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        sItem["LSZJQDSJBZ"] = "----";
                        MItem[0]["G_LSZJQDSJBZ"] = "----";
                        MItem[0]["HG_LSZJQDSJBZ"] = "----";
                        sItem["LSZJQDSJQS"] = "----";
                        MItem[0]["G_LSZJQDSJQS"] = "----";
                        MItem[0]["HG_LSZJQDSJQS"] = "----";

                        sItem["LSZJQDJBBBZ"] = "----";
                        MItem[0]["G_LSZJQDJBBBZ"] = "----";
                        MItem[0]["HG_LSZJQDJBBBZ"] = "----";
                        sItem["LSZJQDJBBQS"] = "----";
                        MItem[0]["G_LSZJQDJBBQS"] = "----";
                        MItem[0]["HG_LSZJQDJBBQS"] = "----";
                    }
                    #endregion

                    #region 干表观密度
                    if (jcxm.Contains("、干表观密度、"))
                    {
                        jcxmCur = "干表观密度";
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            #region 复检
                            if (Conversion.Val(sItem["GSJC1_1"]) != 0)
                            {
                                sItem["GSJC1"] = Round((Conversion.Val(sItem["GSJC1_1"]) + Conversion.Val(sItem["GSJC1_2"]) + Conversion.Val(sItem["GSJC1_3"]) + Conversion.Val(sItem["GSJC1_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC2"] = Round((Conversion.Val(sItem["GSJC2_1"]) + Conversion.Val(sItem["GSJC2_2"]) + Conversion.Val(sItem["GSJC2_3"]) + Conversion.Val(sItem["GSJC2_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC3"] = Round((Conversion.Val(sItem["GSJC3_1"]) + Conversion.Val(sItem["GSJC3_2"]) + Conversion.Val(sItem["GSJC3_3"]) + Conversion.Val(sItem["GSJC3_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC4"] = Round((Conversion.Val(sItem["GSJC4_1"]) + Conversion.Val(sItem["GSJC4_2"]) + Conversion.Val(sItem["GSJC4_3"]) + Conversion.Val(sItem["GSJC4_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC5"] = Round((Conversion.Val(sItem["GSJC5_1"]) + Conversion.Val(sItem["GSJC5_2"]) + Conversion.Val(sItem["GSJC5_3"]) + Conversion.Val(sItem["GSJC5_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC6"] = Round((Conversion.Val(sItem["GSJC6_1"]) + Conversion.Val(sItem["GSJC6_2"]) + Conversion.Val(sItem["GSJC6_3"]) + Conversion.Val(sItem["GSJC6_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC7"] = Round((Conversion.Val(sItem["GSJC7_1"]) + Conversion.Val(sItem["GSJC7_2"]) + Conversion.Val(sItem["GSJC7_3"]) + Conversion.Val(sItem["GSJC7_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC8"] = Round((Conversion.Val(sItem["GSJC8_1"]) + Conversion.Val(sItem["GSJC8_2"]) + Conversion.Val(sItem["GSJC8_3"]) + Conversion.Val(sItem["GSJC8_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC9"] = Round((Conversion.Val(sItem["GSJC9_1"]) + Conversion.Val(sItem["GSJC9_2"]) + Conversion.Val(sItem["GSJC9_3"]) + Conversion.Val(sItem["GSJC9_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC10"] = Round((Conversion.Val(sItem["GSJC10_1"]) + Conversion.Val(sItem["GSJC10_2"]) + Conversion.Val(sItem["GSJC10_3"]) + Conversion.Val(sItem["GSJC10_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC11"] = Round((Conversion.Val(sItem["GSJC11_1"]) + Conversion.Val(sItem["GSJC11_2"]) + Conversion.Val(sItem["GSJC11_3"]) + Conversion.Val(sItem["GSJC11_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC12"] = Round((Conversion.Val(sItem["GSJC12_1"]) + Conversion.Val(sItem["GSJC12_2"]) + Conversion.Val(sItem["GSJC12_3"]) + Conversion.Val(sItem["GSJC12_4"])) / 4, 1).ToString("0.0");
                            }

                            if (Conversion.Val(sItem["GSJK1_1"]) != 0)
                            {
                                sItem["GSJK1"] = Round((Conversion.Val(sItem["GSJK1_1"]) + Conversion.Val(sItem["GSJK1_2"]) + Conversion.Val(sItem["GSJK1_3"]) + Conversion.Val(sItem["GSJK1_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK2"] = Round((Conversion.Val(sItem["GSJK2_1"]) + Conversion.Val(sItem["GSJK2_2"]) + Conversion.Val(sItem["GSJK2_3"]) + Conversion.Val(sItem["GSJK2_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK3"] = Round((Conversion.Val(sItem["GSJK3_1"]) + Conversion.Val(sItem["GSJK3_2"]) + Conversion.Val(sItem["GSJK3_3"]) + Conversion.Val(sItem["GSJK3_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK4"] = Round((Conversion.Val(sItem["GSJK4_1"]) + Conversion.Val(sItem["GSJK4_2"]) + Conversion.Val(sItem["GSJK4_3"]) + Conversion.Val(sItem["GSJK4_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK5"] = Round((Conversion.Val(sItem["GSJK5_1"]) + Conversion.Val(sItem["GSJK5_2"]) + Conversion.Val(sItem["GSJK5_3"]) + Conversion.Val(sItem["GSJK5_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK6"] = Round((Conversion.Val(sItem["GSJK6_1"]) + Conversion.Val(sItem["GSJK6_2"]) + Conversion.Val(sItem["GSJK6_3"]) + Conversion.Val(sItem["GSJK6_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK7"] = Round((Conversion.Val(sItem["GSJK7_1"]) + Conversion.Val(sItem["GSJK7_2"]) + Conversion.Val(sItem["GSJK7_3"]) + Conversion.Val(sItem["GSJK7_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK8"] = Round((Conversion.Val(sItem["GSJK8_1"]) + Conversion.Val(sItem["GSJK8_2"]) + Conversion.Val(sItem["GSJK8_3"]) + Conversion.Val(sItem["GSJK8_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK9"] = Round((Conversion.Val(sItem["GSJK9_1"]) + Conversion.Val(sItem["GSJK9_2"]) + Conversion.Val(sItem["GSJK9_3"]) + Conversion.Val(sItem["GSJK9_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK10"] = Round((Conversion.Val(sItem["GSJK10_1"]) + Conversion.Val(sItem["GSJK10_2"]) + Conversion.Val(sItem["GSJK10_3"]) + Conversion.Val(sItem["GSJK10_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK11"] = Round((Conversion.Val(sItem["GSJK11_1"]) + Conversion.Val(sItem["GSJK11_2"]) + Conversion.Val(sItem["GSJK11_3"]) + Conversion.Val(sItem["GSJK11_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK12"] = Round((Conversion.Val(sItem["GSJK12_1"]) + Conversion.Val(sItem["GSJK12_2"]) + Conversion.Val(sItem["GSJK12_3"]) + Conversion.Val(sItem["GSJK12_4"])) / 4, 1).ToString("0.0");
                            }

                            if (Conversion.Val(sItem["GSJG1_1"]) != 0)
                            {
                                sItem["GSJG1"] = Round((Conversion.Val(sItem["GSJG1_1"]) + Conversion.Val(sItem["GSJG1_2"]) + Conversion.Val(sItem["GSJG1_3"]) + Conversion.Val(sItem["GSJG1_4"]) + Conversion.Val(sItem["GSJG1_5"]) + Conversion.Val(sItem["GSJG1_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG2"] = Round((Conversion.Val(sItem["GSJG2_1"]) + Conversion.Val(sItem["GSJG2_2"]) + Conversion.Val(sItem["GSJG2_3"]) + Conversion.Val(sItem["GSJG2_4"]) + Conversion.Val(sItem["GSJG2_5"]) + Conversion.Val(sItem["GSJG2_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG3"] = Round((Conversion.Val(sItem["GSJG3_1"]) + Conversion.Val(sItem["GSJG3_2"]) + Conversion.Val(sItem["GSJG3_3"]) + Conversion.Val(sItem["GSJG3_4"]) + Conversion.Val(sItem["GSJG3_5"]) + Conversion.Val(sItem["GSJG3_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG4"] = Round((Conversion.Val(sItem["GSJG4_1"]) + Conversion.Val(sItem["GSJG4_2"]) + Conversion.Val(sItem["GSJG4_3"]) + Conversion.Val(sItem["GSJG4_4"]) + Conversion.Val(sItem["GSJG4_5"]) + Conversion.Val(sItem["GSJG4_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG5"] = Round((Conversion.Val(sItem["GSJG5_1"]) + Conversion.Val(sItem["GSJG5_2"]) + Conversion.Val(sItem["GSJG5_3"]) + Conversion.Val(sItem["GSJG5_4"]) + Conversion.Val(sItem["GSJG5_5"]) + Conversion.Val(sItem["GSJG5_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG6"] = Round((Conversion.Val(sItem["GSJG6_1"]) + Conversion.Val(sItem["GSJG6_2"]) + Conversion.Val(sItem["GSJG6_3"]) + Conversion.Val(sItem["GSJG6_4"]) + Conversion.Val(sItem["GSJG6_5"]) + Conversion.Val(sItem["GSJG6_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG7"] = Round((Conversion.Val(sItem["GSJG7_1"]) + Conversion.Val(sItem["GSJG7_2"]) + Conversion.Val(sItem["GSJG7_3"]) + Conversion.Val(sItem["GSJG7_4"]) + Conversion.Val(sItem["GSJG7_5"]) + Conversion.Val(sItem["GSJG7_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG8"] = Round((Conversion.Val(sItem["GSJG8_1"]) + Conversion.Val(sItem["GSJG8_2"]) + Conversion.Val(sItem["GSJG8_3"]) + Conversion.Val(sItem["GSJG8_4"]) + Conversion.Val(sItem["GSJG8_5"]) + Conversion.Val(sItem["GSJG8_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG9"] = Round((Conversion.Val(sItem["GSJG9_1"]) + Conversion.Val(sItem["GSJG9_2"]) + Conversion.Val(sItem["GSJG9_3"]) + Conversion.Val(sItem["GSJG9_4"]) + Conversion.Val(sItem["GSJG9_5"]) + Conversion.Val(sItem["GSJG9_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG10"] = Round((Conversion.Val(sItem["GSJG10_1"]) + Conversion.Val(sItem["GSJG10_2"]) + Conversion.Val(sItem["GSJG10_3"]) + Conversion.Val(sItem["GSJG10_4"]) + Conversion.Val(sItem["GSJG10_5"]) + Conversion.Val(sItem["GSJG10_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG11"] = Round((Conversion.Val(sItem["GSJG11_1"]) + Conversion.Val(sItem["GSJG11_2"]) + Conversion.Val(sItem["GSJG11_3"]) + Conversion.Val(sItem["GSJG11_4"]) + Conversion.Val(sItem["GSJG11_5"]) + Conversion.Val(sItem["GSJG11_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG12"] = Round((Conversion.Val(sItem["GSJG12_1"]) + Conversion.Val(sItem["GSJG12_2"]) + Conversion.Val(sItem["GSJG12_3"]) + Conversion.Val(sItem["GSJG12_4"]) + Conversion.Val(sItem["GSJG12_5"]) + Conversion.Val(sItem["GSJG12_6"])) / 6, 1).ToString("0.0");
                            }

                            sItem["GSJTJ1"] = Round(Conversion.Val(sItem["GSJC1"]) * Conversion.Val(sItem["GSJK1"]) * Conversion.Val(sItem["GSJG1"]), 0).ToString("0.0");
                            sItem["GSJTJ2"] = Round(Conversion.Val(sItem["GSJC2"]) * Conversion.Val(sItem["GSJK2"]) * Conversion.Val(sItem["GSJG2"]), 0).ToString("0.0");
                            sItem["GSJTJ3"] = Round(Conversion.Val(sItem["GSJC3"]) * Conversion.Val(sItem["GSJK3"]) * Conversion.Val(sItem["GSJG3"]), 0).ToString("0.0");
                            sItem["GSJTJ4"] = Round(Conversion.Val(sItem["GSJC4"]) * Conversion.Val(sItem["GSJK4"]) * Conversion.Val(sItem["GSJG4"]), 0).ToString("0.0");
                            sItem["GSJTJ5"] = Round(Conversion.Val(sItem["GSJC5"]) * Conversion.Val(sItem["GSJK5"]) * Conversion.Val(sItem["GSJG5"]), 0).ToString("0.0");
                            sItem["GSJTJ6"] = Round(Conversion.Val(sItem["GSJC6"]) * Conversion.Val(sItem["GSJK6"]) * Conversion.Val(sItem["GSJG6"]), 0).ToString("0.0");
                            sItem["GSJTJ7"] = Round(Conversion.Val(sItem["GSJC7"]) * Conversion.Val(sItem["GSJK7"]) * Conversion.Val(sItem["GSJG7"]), 0).ToString("0.0");
                            sItem["GSJTJ8"] = Round(Conversion.Val(sItem["GSJC8"]) * Conversion.Val(sItem["GSJK8"]) * Conversion.Val(sItem["GSJG8"]), 0).ToString("0.0");
                            sItem["GSJTJ9"] = Round(Conversion.Val(sItem["GSJC9"]) * Conversion.Val(sItem["GSJK9"]) * Conversion.Val(sItem["GSJG9"]), 0).ToString("0.0");
                            sItem["GSJTJ10"] = Round(Conversion.Val(sItem["GSJC10"]) * Conversion.Val(sItem["GSJK10"]) * Conversion.Val(sItem["GSJG10"]), 0).ToString("0.0");
                            sItem["GSJTJ11"] = Round(Conversion.Val(sItem["GSJC11"]) * Conversion.Val(sItem["GSJK11"]) * Conversion.Val(sItem["GSJG11"]), 0).ToString("0.0");
                            sItem["GSJTJ12"] = Round(Conversion.Val(sItem["GSJC12"]) * Conversion.Val(sItem["GSJK12"]) * Conversion.Val(sItem["GSJG12"]), 0).ToString("0.0");

                            sItem["BGMD1"] = Round((Conversion.Val(sItem["GSJZL1"]) / 1000) / (Conversion.Val(sItem["GSJC1"]) / 1000) / (Conversion.Val(sItem["GSJK1"]) / 1000) / (Conversion.Val(sItem["GSJG1"]) / 1000), 4).ToString();
                            sItem["BGMD2"] = Round((Conversion.Val(sItem["GSJZL2"]) / 1000) / (Conversion.Val(sItem["GSJC2"]) / 1000) / (Conversion.Val(sItem["GSJK2"]) / 1000) / (Conversion.Val(sItem["GSJG2"]) / 1000), 4).ToString();
                            sItem["BGMD3"] = Round((Conversion.Val(sItem["GSJZL3"]) / 1000) / (Conversion.Val(sItem["GSJC3"]) / 1000) / (Conversion.Val(sItem["GSJK3"]) / 1000) / (Conversion.Val(sItem["GSJG3"]) / 1000), 4).ToString();
                            sItem["BGMD4"] = Round((Conversion.Val(sItem["GSJZL4"]) / 1000) / (Conversion.Val(sItem["GSJC4"]) / 1000) / (Conversion.Val(sItem["GSJK4"]) / 1000) / (Conversion.Val(sItem["GSJG4"]) / 1000), 4).ToString();
                            sItem["BGMD5"] = Round((Conversion.Val(sItem["GSJZL5"]) / 1000) / (Conversion.Val(sItem["GSJC5"]) / 1000) / (Conversion.Val(sItem["GSJK5"]) / 1000) / (Conversion.Val(sItem["GSJG5"]) / 1000), 4).ToString();
                            sItem["BGMD6"] = Round((Conversion.Val(sItem["GSJZL6"]) / 1000) / (Conversion.Val(sItem["GSJC6"]) / 1000) / (Conversion.Val(sItem["GSJK6"]) / 1000) / (Conversion.Val(sItem["GSJG6"]) / 1000), 4).ToString();
                            sItem["BGMD7"] = Round((Conversion.Val(sItem["GSJZL7"]) / 1000) / (Conversion.Val(sItem["GSJC7"]) / 1000) / (Conversion.Val(sItem["GSJK7"]) / 1000) / (Conversion.Val(sItem["GSJG7"]) / 1000), 4).ToString();
                            sItem["BGMD8"] = Round((Conversion.Val(sItem["GSJZL8"]) / 1000) / (Conversion.Val(sItem["GSJC8"]) / 1000) / (Conversion.Val(sItem["GSJK8"]) / 1000) / (Conversion.Val(sItem["GSJG8"]) / 1000), 4).ToString();
                            sItem["BGMD9"] = Round((Conversion.Val(sItem["GSJZL9"]) / 1000) / (Conversion.Val(sItem["GSJC9"]) / 1000) / (Conversion.Val(sItem["GSJK9"]) / 1000) / (Conversion.Val(sItem["GSJG9"]) / 1000), 4).ToString();
                            sItem["BGMD10"] = Round((Conversion.Val(sItem["GSJZL10"]) / 1000) / (Conversion.Val(sItem["GSJC10"]) / 1000) / (Conversion.Val(sItem["GSJK10"]) / 1000) / (Conversion.Val(sItem["GSJG10"]) / 1000), 4).ToString();
                            sItem["BGMD11"] = Round((Conversion.Val(sItem["GSJZL11"]) / 1000) / (Conversion.Val(sItem["GSJC11"]) / 1000) / (Conversion.Val(sItem["GSJK11"]) / 1000) / (Conversion.Val(sItem["GSJG11"]) / 1000), 4).ToString();
                            sItem["BGMD12"] = Round((Conversion.Val(sItem["GSJZL12"]) / 1000) / (Conversion.Val(sItem["GSJC12"]) / 1000) / (Conversion.Val(sItem["GSJK12"]) / 1000) / (Conversion.Val(sItem["GSJG12"]) / 1000), 4).ToString();
                            sItem["BGMD"] = Round((Conversion.Val(sItem["BGMD1"]) + Conversion.Val(sItem["BGMD2"]) + Conversion.Val(sItem["BGMD3"]) + Conversion.Val(sItem["BGMD4"]) + Conversion.Val(sItem["BGMD5"]) + Conversion.Val(sItem["BGMD6"]) + Conversion.Val(sItem["BGMD7"]) + Conversion.Val(sItem["BGMD8"]) + Conversion.Val(sItem["BGMD9"]) + Conversion.Val(sItem["BGMD10"]) + Conversion.Val(sItem["BGMD11"]) + Conversion.Val(sItem["BGMD12"])) / 12, 0).ToString();
                            #endregion
                        }
                        else
                        {
                            #region 初检
                            if (Conversion.Val(sItem["GSJC1_1"]) != 0)
                            {
                                sItem["GSJC1"] = Round((Conversion.Val(sItem["GSJC1_1"]) + Conversion.Val(sItem["GSJC1_2"]) + Conversion.Val(sItem["GSJC1_3"]) + Conversion.Val(sItem["GSJC1_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC2"] = Round((Conversion.Val(sItem["GSJC2_1"]) + Conversion.Val(sItem["GSJC2_2"]) + Conversion.Val(sItem["GSJC2_3"]) + Conversion.Val(sItem["GSJC2_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC3"] = Round((Conversion.Val(sItem["GSJC3_1"]) + Conversion.Val(sItem["GSJC3_2"]) + Conversion.Val(sItem["GSJC3_3"]) + Conversion.Val(sItem["GSJC3_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC4"] = Round((Conversion.Val(sItem["GSJC4_1"]) + Conversion.Val(sItem["GSJC4_2"]) + Conversion.Val(sItem["GSJC4_3"]) + Conversion.Val(sItem["GSJC4_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC5"] = Round((Conversion.Val(sItem["GSJC5_1"]) + Conversion.Val(sItem["GSJC5_2"]) + Conversion.Val(sItem["GSJC5_3"]) + Conversion.Val(sItem["GSJC5_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJC6"] = Round((Conversion.Val(sItem["GSJC6_1"]) + Conversion.Val(sItem["GSJC6_2"]) + Conversion.Val(sItem["GSJC6_3"]) + Conversion.Val(sItem["GSJC6_4"])) / 4, 1).ToString("0.0");
                            }

                            if (Conversion.Val(sItem["GSJK1_1"]) != 0)
                            {
                                sItem["GSJK1"] = Round((Conversion.Val(sItem["GSJK1_1"]) + Conversion.Val(sItem["GSJK1_2"]) + Conversion.Val(sItem["GSJK1_3"]) + Conversion.Val(sItem["GSJK1_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK2"] = Round((Conversion.Val(sItem["GSJK2_1"]) + Conversion.Val(sItem["GSJK2_2"]) + Conversion.Val(sItem["GSJK2_3"]) + Conversion.Val(sItem["GSJK2_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK3"] = Round((Conversion.Val(sItem["GSJK3_1"]) + Conversion.Val(sItem["GSJK3_2"]) + Conversion.Val(sItem["GSJK3_3"]) + Conversion.Val(sItem["GSJK3_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK4"] = Round((Conversion.Val(sItem["GSJK4_1"]) + Conversion.Val(sItem["GSJK4_2"]) + Conversion.Val(sItem["GSJK4_3"]) + Conversion.Val(sItem["GSJK4_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK5"] = Round((Conversion.Val(sItem["GSJK5_1"]) + Conversion.Val(sItem["GSJK5_2"]) + Conversion.Val(sItem["GSJK5_3"]) + Conversion.Val(sItem["GSJK5_4"])) / 4, 1).ToString("0.0");
                                sItem["GSJK6"] = Round((Conversion.Val(sItem["GSJK6_1"]) + Conversion.Val(sItem["GSJK6_2"]) + Conversion.Val(sItem["GSJK6_3"]) + Conversion.Val(sItem["GSJK6_4"])) / 4, 1).ToString("0.0");
                            }

                            if (Conversion.Val(sItem["GSJG1_1"]) != 0)
                            {
                                sItem["GSJG1"] = Round((Conversion.Val(sItem["GSJG1_1"]) + Conversion.Val(sItem["GSJG1_2"]) + Conversion.Val(sItem["GSJG1_3"]) + Conversion.Val(sItem["GSJG1_4"]) + Conversion.Val(sItem["GSJG1_5"]) + Conversion.Val(sItem["GSJG1_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG2"] = Round((Conversion.Val(sItem["GSJG2_1"]) + Conversion.Val(sItem["GSJG2_2"]) + Conversion.Val(sItem["GSJG2_3"]) + Conversion.Val(sItem["GSJG2_4"]) + Conversion.Val(sItem["GSJG2_5"]) + Conversion.Val(sItem["GSJG2_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG3"] = Round((Conversion.Val(sItem["GSJG3_1"]) + Conversion.Val(sItem["GSJG3_2"]) + Conversion.Val(sItem["GSJG3_3"]) + Conversion.Val(sItem["GSJG3_4"]) + Conversion.Val(sItem["GSJG3_5"]) + Conversion.Val(sItem["GSJG3_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG4"] = Round((Conversion.Val(sItem["GSJG4_1"]) + Conversion.Val(sItem["GSJG4_2"]) + Conversion.Val(sItem["GSJG4_3"]) + Conversion.Val(sItem["GSJG4_4"]) + Conversion.Val(sItem["GSJG4_5"]) + Conversion.Val(sItem["GSJG4_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG5"] = Round((Conversion.Val(sItem["GSJG5_1"]) + Conversion.Val(sItem["GSJG5_2"]) + Conversion.Val(sItem["GSJG5_3"]) + Conversion.Val(sItem["GSJG5_4"]) + Conversion.Val(sItem["GSJG5_5"]) + Conversion.Val(sItem["GSJG5_6"])) / 6, 1).ToString("0.0");
                                sItem["GSJG6"] = Round((Conversion.Val(sItem["GSJG6_1"]) + Conversion.Val(sItem["GSJG6_2"]) + Conversion.Val(sItem["GSJG6_3"]) + Conversion.Val(sItem["GSJG6_4"]) + Conversion.Val(sItem["GSJG6_5"]) + Conversion.Val(sItem["GSJG6_6"])) / 6, 1).ToString("0.0");
                            }

                            sItem["GSJTJ1"] = Round(Conversion.Val(sItem["GSJC1"]) * Conversion.Val(sItem["GSJK1"]) * Conversion.Val(sItem["GSJG1"]), 0).ToString("0.0");
                            sItem["GSJTJ2"] = Round(Conversion.Val(sItem["GSJC2"]) * Conversion.Val(sItem["GSJK2"]) * Conversion.Val(sItem["GSJG2"]), 0).ToString("0.0");
                            sItem["GSJTJ3"] = Round(Conversion.Val(sItem["GSJC3"]) * Conversion.Val(sItem["GSJK3"]) * Conversion.Val(sItem["GSJG3"]), 0).ToString("0.0");
                            sItem["GSJTJ4"] = Round(Conversion.Val(sItem["GSJC4"]) * Conversion.Val(sItem["GSJK4"]) * Conversion.Val(sItem["GSJG4"]), 0).ToString("0.0");
                            sItem["GSJTJ5"] = Round(Conversion.Val(sItem["GSJC5"]) * Conversion.Val(sItem["GSJK5"]) * Conversion.Val(sItem["GSJG5"]), 0).ToString("0.0");
                            sItem["GSJTJ6"] = Round(Conversion.Val(sItem["GSJC6"]) * Conversion.Val(sItem["GSJK6"]) * Conversion.Val(sItem["GSJG6"]), 0).ToString("0.0");

                            sItem["BGMD1"] = Round((Conversion.Val(sItem["GSJZL1"]) / 1000) / (Conversion.Val(sItem["GSJC1"]) / 1000) / (Conversion.Val(sItem["GSJK1"]) / 1000) / (Conversion.Val(sItem["GSJG1"]) / 1000), 4).ToString();
                            sItem["BGMD2"] = Round((Conversion.Val(sItem["GSJZL2"]) / 1000) / (Conversion.Val(sItem["GSJC2"]) / 1000) / (Conversion.Val(sItem["GSJK2"]) / 1000) / (Conversion.Val(sItem["GSJG2"]) / 1000), 4).ToString();
                            sItem["BGMD3"] = Round((Conversion.Val(sItem["GSJZL3"]) / 1000) / (Conversion.Val(sItem["GSJC3"]) / 1000) / (Conversion.Val(sItem["GSJK3"]) / 1000) / (Conversion.Val(sItem["GSJG3"]) / 1000), 4).ToString();
                            sItem["BGMD4"] = Round((Conversion.Val(sItem["GSJZL4"]) / 1000) / (Conversion.Val(sItem["GSJC4"]) / 1000) / (Conversion.Val(sItem["GSJK4"]) / 1000) / (Conversion.Val(sItem["GSJG4"]) / 1000), 4).ToString();
                            sItem["BGMD5"] = Round((Conversion.Val(sItem["GSJZL5"]) / 1000) / (Conversion.Val(sItem["GSJC5"]) / 1000) / (Conversion.Val(sItem["GSJK5"]) / 1000) / (Conversion.Val(sItem["GSJG5"]) / 1000), 4).ToString();
                            sItem["BGMD6"] = Round((Conversion.Val(sItem["GSJZL6"]) / 1000) / (Conversion.Val(sItem["GSJC6"]) / 1000) / (Conversion.Val(sItem["GSJK6"]) / 1000) / (Conversion.Val(sItem["GSJG6"]) / 1000), 4).ToString();

                            sItem["BGMD"] = Round((Conversion.Val(sItem["BGMD1"]) + Conversion.Val(sItem["BGMD2"]) + Conversion.Val(sItem["BGMD3"]) + Conversion.Val(sItem["BGMD4"]) + Conversion.Val(sItem["BGMD5"]) + Conversion.Val(sItem["BGMD6"])) / 6, 0).ToString();
                            #endregion
                        }

                        if (IsQualified(MItem[0]["G_BGMD"], sItem["BGMD"]) == "合格")
                        {
                            MItem[0]["HG_BGMD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_BGMD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                    }
                    else
                    {
                        sItem["BGMD"] = "----";
                        MItem[0]["HG_BGMD"] = "----";
                        MItem[0]["G_BGMD"] = "----";
                    }
                    #endregion

                    #region 抗拉强度
                    if (jcxm.Contains("、抗拉强度、") && Conversion.Val(sItem["KLCD1"]) > 0)
                    {
                        jcxmCur = "抗拉强度";
                        double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0, mMj9 = 0, mMj10 = 0, mMj11 = 0, mMj12 = 0;
                        double mKLQD1 = 0, mKLQD2 = 0, mKLQD3 = 0, mKLQD4 = 0, mKLQD5 = 0, mKLQD6 = 0, mKLQD7 = 0, mKLQD8 = 0, mKLQD9 = 0, mKLQD10 = 0, mKLQD11 = 0, mKLQD12 = 0;
                        List<double> lArray = new List<double>();
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            #region 复检
                            mMj1 = (Conversion.Val(sItem["KLCD1"])) * (Conversion.Val(sItem["KLKD1"]));
                            mMj2 = (Conversion.Val(sItem["KLCD2"])) * (Conversion.Val(sItem["KLKD2"]));
                            mMj3 = (Conversion.Val(sItem["KLCD3"])) * (Conversion.Val(sItem["KLKD3"]));
                            mMj4 = (Conversion.Val(sItem["KLCD4"])) * (Conversion.Val(sItem["KLKD4"]));
                            mMj5 = (Conversion.Val(sItem["KLCD5"])) * (Conversion.Val(sItem["KLKD5"]));
                            mMj6 = (Conversion.Val(sItem["KLCD6"])) * (Conversion.Val(sItem["KLKD6"]));
                            mMj7 = (Conversion.Val(sItem["KLCD7"])) * (Conversion.Val(sItem["KLKD7"]));
                            mMj8 = (Conversion.Val(sItem["KLCD8"])) * (Conversion.Val(sItem["KLKD8"]));
                            mMj9 = (Conversion.Val(sItem["KLCD9"])) * (Conversion.Val(sItem["KLKD9"]));
                            mMj10 = (Conversion.Val(sItem["KLCD10"])) * (Conversion.Val(sItem["KLKD10"]));
                            mMj11 = (Conversion.Val(sItem["KLCD11"])) * (Conversion.Val(sItem["KLKD11"]));
                            mMj12 = (Conversion.Val(sItem["KLCD12"])) * (Conversion.Val(sItem["KLKD12"]));

                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0 && mMj6 != 0 && mMj7 != 0 && mMj8 != 0 && mMj9 != 0 && mMj10 != 0 && mMj11 != 0 && mMj12 != 0)
                            {
                                mKLQD1 = Round(Conversion.Val(sItem["KLHZ1"]) / mMj1, 3);
                                mKLQD2 = Round(Conversion.Val(sItem["KLHZ2"]) / mMj2, 3);
                                mKLQD3 = Round(Conversion.Val(sItem["KLHZ3"]) / mMj3, 3);
                                mKLQD4 = Round(Conversion.Val(sItem["KLHZ4"]) / mMj4, 3);
                                mKLQD5 = Round(Conversion.Val(sItem["KLHZ5"]) / mMj5, 3);
                                mKLQD6 = Round(Conversion.Val(sItem["KLHZ6"]) / mMj6, 3);
                                mKLQD7 = Round(Conversion.Val(sItem["KLHZ7"]) / mMj7, 3);
                                mKLQD8 = Round(Conversion.Val(sItem["KLHZ8"]) / mMj8, 3);
                                mKLQD9 = Round(Conversion.Val(sItem["KLHZ9"]) / mMj9, 3);
                                mKLQD10 = Round(Conversion.Val(sItem["KLHZ10"]) / mMj10, 3);
                                mKLQD11 = Round(Conversion.Val(sItem["KLHZ11"]) / mMj11, 3);
                                mKLQD12 = Round(Conversion.Val(sItem["KLHZ12"]) / mMj12, 3);
                            }
                            else
                            {
                                mKLQD1 = 0;
                                mKLQD2 = 0;
                                mKLQD3 = 0;
                                mKLQD4 = 0;
                                mKLQD5 = 0;
                                mKLQD6 = 0;
                                mKLQD7 = 0;
                                mKLQD8 = 0;
                                mKLQD9 = 0;
                                mKLQD10 = 0;
                                mKLQD11 = 0;
                                mKLQD12 = 0;
                            }
                            sItem["KLQD1"] = mKLQD1.ToString();
                            sItem["KLQD2"] = mKLQD2.ToString();
                            sItem["KLQD3"] = mKLQD3.ToString();
                            sItem["KLQD4"] = mKLQD4.ToString();
                            sItem["KLQD5"] = mKLQD5.ToString();
                            sItem["KLQD6"] = mKLQD6.ToString();
                            sItem["KLQD7"] = mKLQD7.ToString();
                            sItem["KLQD8"] = mKLQD8.ToString();
                            sItem["KLQD9"] = mKLQD9.ToString();
                            sItem["KLQD10"] = mKLQD10.ToString();
                            sItem["KLQD11"] = mKLQD11.ToString();
                            sItem["KLQD12"] = mKLQD12.ToString();

                            lArray.Add(mKLQD1);
                            lArray.Add(mKLQD2);
                            lArray.Add(mKLQD3);
                            lArray.Add(mKLQD4);
                            lArray.Add(mKLQD5);
                            lArray.Add(mKLQD6);
                            lArray.Add(mKLQD7);
                            lArray.Add(mKLQD8);
                            lArray.Add(mKLQD9);
                            lArray.Add(mKLQD10);
                            lArray.Add(mKLQD11);
                            lArray.Add(mKLQD12);
                            lArray.Sort();
                            #endregion
                        }
                        else
                        {
                            #region 初检
                            mMj1 = (Conversion.Val(sItem["KLCD1"])) * (Conversion.Val(sItem["KLKD1"]));
                            mMj2 = (Conversion.Val(sItem["KLCD2"])) * (Conversion.Val(sItem["KLKD2"]));
                            mMj3 = (Conversion.Val(sItem["KLCD3"])) * (Conversion.Val(sItem["KLKD3"]));
                            mMj4 = (Conversion.Val(sItem["KLCD4"])) * (Conversion.Val(sItem["KLKD4"]));
                            mMj5 = (Conversion.Val(sItem["KLCD5"])) * (Conversion.Val(sItem["KLKD5"]));
                            //mMj6 = (Conversion.Val(sItem["KLCD6"])) * (Conversion.Val(sItem["KLKD6"]));

                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0)
                            {
                                mKLQD1 = Round(Conversion.Val(sItem["KLHZ1"]) / mMj1, 3);
                                mKLQD2 = Round(Conversion.Val(sItem["KLHZ2"]) / mMj2, 3);
                                mKLQD3 = Round(Conversion.Val(sItem["KLHZ3"]) / mMj3, 3);
                                mKLQD4 = Round(Conversion.Val(sItem["KLHZ4"]) / mMj4, 3);
                                mKLQD5 = Round(Conversion.Val(sItem["KLHZ5"]) / mMj5, 3);
                                //mKLQD6 = Round(Conversion.Val(sItem["KLHZ6"]) / mMj6, 3);
                            }
                            else
                            {
                                mKLQD1 = 0;
                                mKLQD2 = 0;
                                mKLQD3 = 0;
                                mKLQD4 = 0;
                                mKLQD5 = 0;
                                //mKLQD6 = 0;
                            }
                            sItem["KLQD1"] = mKLQD1.ToString();
                            sItem["KLQD2"] = mKLQD2.ToString();
                            sItem["KLQD3"] = mKLQD3.ToString();
                            sItem["KLQD4"] = mKLQD4.ToString();
                            sItem["KLQD5"] = mKLQD5.ToString();
                            //sItem["KLQD6"] = mKLQD6.ToString();


                            lArray.Add(mKLQD1);
                            lArray.Add(mKLQD2);
                            lArray.Add(mKLQD3);
                            lArray.Add(mKLQD4);
                            lArray.Add(mKLQD5);
                            //lArray.Add(mKLQD6);
                            lArray.Sort();
                            #endregion
                        }
                        if (sItem["CPMC"].Contains("抹灰浆料"))
                        {
                            sItem["KLQD"] = lArray.Average().ToString("0.0");
                        }
                        else if (sItem["CPMC"].Contains("贴砌浆料"))
                        {
                            sItem["KLQD"] = lArray.Average().ToString("0.00");
                        }

                        if (IsQualified(MItem[0]["G_KLQD"], sItem["KLQD"], false) == "合格")
                        {
                            MItem[0]["HG_KLQD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_KLQD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }

                    }
                    else
                    {
                        sItem["KLQD"] = "----";
                        MItem[0]["G_KLQD"] = "----";
                        MItem[0]["HG_KLQD"] = "----";
                    }
                    #endregion

                    #region 软化系数
                    if (jcxm.Contains("、软化系数、"))
                    {
                        jcxmCur = "软化系数";
                        if (string.IsNullOrEmpty(sItem["KYQD"]))
                        {
                            throw new SystemException("没有抗压强度值。");
                        }
                        else
                        {
                            List<double> lArray = new List<double>();
                            if (MItem[0]["SFFJ"] == "复检")   //复检
                            {
                                #region 复检
                                for (int i = 1; i < 13; i++)
                                {
                                    sItem["KYCD" + i] = Math.Round((Conversion.Val(sItem["JSKYQDCD" + i + "_1"]) + Conversion.Val(sItem["JSKYQDCD" + i + "_2"]) + Conversion.Val(sItem["JSKYQDCD" + i + "_3"]) + Conversion.Val(sItem["JSKYQDCD" + i + "_4"])) / 4, 2).ToString();
                                    sItem["KYKD" + i] = Math.Round((Conversion.Val(sItem["JSKYQDKD" + i + "_1"]) + Conversion.Val(sItem["JSKYQDKD" + i + "_2"]) + Conversion.Val(sItem["JSKYQDKD" + i + "_3"]) + Conversion.Val(sItem["JSKYQDKD" + i + "_4"])) / 4, 2).ToString();
                                    sItem["JSKYQD" + i] = Math.Round(Conversion.Val(sItem["KYHZ" + i]) / (Conversion.Val(sItem["KYCD" + i]) * Conversion.Val(sItem["KYKD" + i])), 2).ToString();
                                    lArray.Add(GetSafeDouble(sItem["JSKYQD" + i]));
                                }
                                #endregion
                            }
                            else
                            {
                                #region 初检
                                for (int i = 1; i < 7; i++)
                                {
                                    sItem["KYCD" + i] = Math.Round((Conversion.Val(sItem["JSKYQDCD" + i + "_1"]) + Conversion.Val(sItem["JSKYQDCD" + i + "_2"]) + Conversion.Val(sItem["JSKYQDCD" + i + "_3"]) + Conversion.Val(sItem["JSKYQDCD" + i + "_4"])) / 4, 2).ToString();
                                    sItem["KYKD" + i] = Math.Round((Conversion.Val(sItem["JSKYQDKD" + i + "_1"]) + Conversion.Val(sItem["JSKYQDKD" + i + "_2"]) + Conversion.Val(sItem["JSKYQDKD" + i + "_3"]) + Conversion.Val(sItem["JSKYQDKD" + i + "_4"])) / 4, 2).ToString();
                                    sItem["JSKYQD" + i] = Math.Round(Conversion.Val(sItem["KYHZ" + i]) / (Conversion.Val(sItem["KYCD" + i]) * Conversion.Val(sItem["KYKD" + i])), 2).ToString();
                                    lArray.Add(GetSafeDouble(sItem["JSKYQD" + i]));
                                }
                                #endregion
                            }
                            sItem["JSKYQD"] = lArray.Average().ToString();
                            sItem["RHXS"] = Math.Round(GetSafeDouble(sItem["JSKYQD"]) / GetSafeDouble(sItem["KYQD"]), 2).ToString();
                            MItem[0]["HG_RHXS"] = IsQualified(MItem[0]["G_RHXS"], sItem["RHXS"], false);

                            if ("不合格" == MItem[0]["HG_RHXS"])
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                mbhggs = mbhggs + 1;
                                mAllHg = false;
                            }
                        }
                    }
                    else
                    {
                        MItem[0]["HG_RHXS"] = "----";
                        MItem[0]["G_RHXS"] = "----";
                        sItem["RHXS"] = "----";
                    }
                    #endregion

                    #region 线性收缩率
                    if (jcxm.Contains("、线性收缩率、"))
                    {
                        jcxmCur = "线性收缩率";
                        List<double> lArray = new List<double>();
                        int n = 2; //与平均值超20%的数量
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            #region 复检
                            for (int i = 1; i < 7; i++)
                            {
                                sItem["SSL" + i] = Math.Round((Conversion.Val(sItem["CSCD" + i]) - Conversion.Val(sItem["GZCD" + i])) / (Conversion.Val(sItem["SJCD" + i]) - Conversion.Val(sItem["SMCD" + i])), 5).ToString();
                                lArray.Add(GetSafeDouble(sItem["SSL" + i]));
                            }
                            n = 3;
                            #endregion
                        }
                        else
                        {
                            #region 初检
                            for (int i = 1; i < 4; i++)
                            {
                                sItem["SSL" + i] = Math.Round((Conversion.Val(sItem["CSCD" + i]) - Conversion.Val(sItem["GZCD" + i])) / (Conversion.Val(sItem["SJCD" + i]) - Conversion.Val(sItem["SMCD" + i])), 5).ToString();
                                lArray.Add(GetSafeDouble(sItem["SSL" + i]));
                            }
                            #endregion
                        }
                        double avg = 0.00;
                        int count = 0;
                        avg = lArray.Average();
                        for (int i = 0; i < lArray.Count; i++)
                        {
                            double cz = Math.Abs((lArray[i] - avg) / avg);
                            if (cz > 0.2)
                            {
                                count++;
                                lArray.Remove(lArray[i]);
                            }
                        }

                        sItem["SSL"] = Math.Round(lArray.Average(), 2).ToString();
                        if (count < n && IsQualified(MItem[0]["G_XXSSL"], sItem["SSL"], false) == "合格")
                        {
                            MItem[0]["HG_SSL"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_SSL"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["SSL"] = "----";
                        MItem[0]["G_XXSSL"] = "----";
                        MItem[0]["HG_SSL"] = "----";
                    }
                    #endregion
                }
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                if (MItem[0]["SFFJ"] == "复检")   //复检
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目均符合要求。";
                }
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                }
            }
            else
            {
                mjcjg = "不合格";
                if (MItem[0]["SFFJ"] == "复检")   //复检    
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                }
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，需双倍复检。";
                }
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
        }
    }
}

