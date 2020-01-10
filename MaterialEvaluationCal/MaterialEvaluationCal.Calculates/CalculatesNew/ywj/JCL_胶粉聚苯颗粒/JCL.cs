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

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mFlag_Hg = true;
                sign = true;
                mbhggs = 0;
                if (jcxm.Contains("、导热系数、"))
                {
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

                    sItem["DRXS"] = Math.Round(double.Parse(sItem["DRXS"]), mcd).ToString();

                    mItem["HG_DRXS"] = IsQualified(mItem["G_DRXS"], sItem["DRXS"], true);

                    if (mItem["HG_DRXS"] == "不符合")
                    {
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
                    if (Conversion.Val(sItem["KYQD"]) == 0)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["HG_KYQD"] = IsQualified(mItem["G_KYQD"], sItem["KYQD"], true);

                        if (mItem["HG_KYQD"] == "不符合")
                        {
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
                    if (Conversion.Val(sItem["YSQD"]) == 0)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["HG_YSQD"] = IsQualified(mItem["G_YSQD"], sItem["YSQD"], true);

                        if (mItem["HG_YSQD"] == "不符合")
                        {
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
                    mItem["HG_BGMD"] = IsQualified(mItem["G_BGMD"], sItem["BGMD"], true);

                    if (mItem["HG_BGMD"] == "不符合")
                    {
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
                    mItem["HG_SBGMD"] = IsQualified(mItem["G_SBGMD"], sItem["SBGMD"], true);

                    if (mItem["HG_SBGMD"] == "不符合")
                    {
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
                    mItem["HG_XXSSL"] = IsQualified(mItem["G_XXSSL"], sItem["XXSSL"], true);

                    if (mItem["HG_XXSSL"] == "不符合")
                    {
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
                    mItem["HG_RHXS"] = IsQualified(mItem["G_RHXS"], sItem["RHXS"], true);

                    if ("不符合" == mItem["HG_RHXS"])
                    {
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
                    mItem["HG_NRX"] = sItem["NRX"] == "符合" ? "合格" : "不合格";

                    if ("不符合" == mItem["HG_NRX"])
                    {
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
                    jsbeizhu = "该组试件所检项目符合" + mItem["PDBZ"] + "标准要求。";
                    sItem["JCJG"] = "合格";
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "该组试件不符合" + mItem["PDBZ"] + "标准要求。";
                    if (mFlag_Bhg && mFlag_Hg)
                    {
                        jsbeizhu = "该组试件所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                    }
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
                    //MItem[0]["WHICH"] = mrsDj["WHICH"];
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
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
                }
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

