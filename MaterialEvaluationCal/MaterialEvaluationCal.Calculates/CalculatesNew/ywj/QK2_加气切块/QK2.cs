using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class QK2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/

            #region 计算方法
            System.Diagnostics.Debugger.Break();
            #region 
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItems = retData["S_QK2"];
            var extraGMD = dataExtra["BZ_QK2GMDJB"];
            var extraDJ = dataExtra["BZ_QK2_DJ"];
            var extraQK2QDJB = dataExtra["BZ_QK2QDJB"];
            //var ZM_DRJL = retData["ZM_DRJL"];

            if (!retData.ContainsKey("M_QK2"))
            {
                retData["M_QK2"] = new List<IDictionary<string, string>>();
            }
            var MItem = retData["M_QK2"];

            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;
            var jcxm = "";
            var mSjdj = "";
            var mJSFF = "";
            var mSz = 0.0;
            var mbhggs = 0;
            double md, md1, md2, sum, pjmd = 0;
            int mcd, mdwz = 0;
            bool sign = false;
            List<double> narr = new List<double>();

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc =
            delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mbhggs = 0;
                sum = 0;

                //Rs("select * from ZM_DRJL", adOpenStatic, adLockBatchOptimistic)
                if (jcxm.Contains("、干密度、"))
                {

                    sItem["GMDPD"] = IsQualified(sItem["GMDYQ"], sItem["GMDPJ"]);

                    if (sItem["GMDPD"] == "不合格")
                    {
                        mFlag_Bhg = true;
                        mbhggs += 1;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["GMDPJ"] = "----";
                    sItem["GMDPD"] = "----";
                    sItem["GMDYQ"] = "----";
                }

                if (jcxm.Contains("、抗压、") || jcxm.Contains("、抗压强度、"))
                {
                    if (0 == Conversion.Val(sItem["KYHZ1_1"]))
                    {
                        return false;
                    }

                    sItem["HSLPD"] = IsQualified(sItem["HSLYQ"], sItem["HSLPJ"]);

                    for (int i = 1; i < 4; i++)
                    {
                        sItem["KYQD1_" + i] = Math.Round(1000 * Conversion.Val(sItem["KYHZ1_" + i]) / Conversion.Val(sItem["QCD1_" + i]) / Conversion.Val(sItem["QKD1_" + i]), 1).ToString("0.0");
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["KYQD2_" + i] = Math.Round(1000 * Conversion.Val(sItem["KYHZ2_" + i]) / Conversion.Val(sItem["QCD2_" + i]) / Conversion.Val(sItem["QKD2_" + i]), 1).ToString("0.0");
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["KYQD3_" + i] = Math.Round(1000 * Conversion.Val(sItem["KYHZ3_" + i]) / Conversion.Val(sItem["QCD3_" + i]) / Conversion.Val(sItem["QKD3_" + i]), 1).ToString("0.0");
                    }
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["KYPJ" + i] = Math.Round(((Conversion.Val(sItem["KYQD" + i + "_1"]) + Conversion.Val(sItem["KYQD" + i + "_2"]) + Conversion.Val(sItem["KYQD" + i + "_3"])) / 3), 1).ToString("0.0");
                    }

                    sItem["KYPJ"] = Math.Round((Conversion.Val(sItem["KYPJ1"]) + Conversion.Val(sItem["KYPJ2"]) + Conversion.Val(sItem["KYPJ3"])) / 3, 1).ToString("0.0");
                    sItem["DKZX"] = sItem["KYPJ1"];

                    if (Conversion.Val(sItem["DKZX"]) > Conversion.Val(sItem["KYPJ2"]))
                    {
                        sItem["DKZX"] = sItem["KYPJ2"];
                    }
                    if (Conversion.Val(sItem["DKZX"]) > Conversion.Val(sItem["KYPJ3"]))
                    {
                        sItem["DKZX"] = sItem["KYPJ3"];
                    }

                    if (Conversion.Val(sItem["KYPJ"]) >= Conversion.Val(mItem["G_PJZ"]) && Conversion.Val(sItem["DKZX"]) >= Conversion.Val(mItem["G_MIN"]))
                    {
                        sItem["QDPD"] = "合格";
                        mFlag_Hg = true;
                    }
                    else
                    {
                        sItem["QDPD"] = "不合格";
                        mFlag_Bhg = true;
                        mbhggs += 1;
                    }
                }
                else
                {

                    sItem["DKZX"] = "----";
                    sItem["QDPD"] = "----";
                    sItem["QDYQ"] = "----";
                    sItem["KYPJ"] = "----";
                    sItem["HSLPJ"] = "----";
                    sItem["HSLPD"] = "----";
                }

                sign = string.IsNullOrEmpty(sItem["WGPD"]) ? false : sItem["WGPD"].Contains("试件明显破坏") ? false : true;

                if (jcxm.Contains("、冻后强度、") && sign)
                {
                    if (0 == Conversion.Val(sItem["DHKYHZ1_1"]))
                    {
                        return false;
                    }

                    for (int Gs = 1; Gs < 4; Gs++)
                    {
                        sum = 0;

                        for (int xd = 0; xd < 4; xd++)
                        {
                            if (IsNumeric(sItem["DHKYHZ" + Gs + "_" + xd]) && IsNumeric(sItem["DHKYHZ" + Gs + "_" + xd]))
                            {
                                md = Conversion.Val(sItem["DHKYHZ" + Gs + "_" + xd]);
                                md1 = Conversion.Val(sItem["DHQCD" + Gs + "_" + xd]);
                                md2 = Conversion.Val(sItem["DHQKD" + Gs + "_" + xd]);
                                md = Math.Round(1000 * md / md1 / md2, 1);
                                sItem["DHKYQD" + Gs + "_" + xd] = md.ToString("0.0");
                                sum = sum + md;
                            }
                            else
                            {
                                sItem["DHKYQD" + Gs + "_" + xd] = "----";
                            }

                        }

                        pjmd = Math.Round(sum / 3, 1);

                        if (pjmd == 0)
                        {
                            sItem["DHKYPJ" + Gs] = "----";
                        }
                        else
                        {
                            sItem["DHKYPJ" + Gs] = pjmd.ToString("0.0");
                        }
                    }

                    sItem["DHQDPD"] = IsQualified(sItem["DHQDYQ"], sItem["DHKYDZZX"], false);

                    if (sItem["DHQDPD"] == "不合格")
                    {
                        mFlag_Bhg = true;
                        mbhggs += 1;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["DHQDYQ"] = sign ? sItem["DHQDYQ"] : "----";
                    sItem["DHQDPD"] = "----";
                    sItem["DHKYDZZX"] = "----";
                    //mrsmainTablesItem["WHICH") = IIf(sign, "bgqk2", "bgqk2_1")
                }

                if (jcxm.Contains("、外观质量、") && sign)
                {
                    if (sign)
                    {
                        sItem["LLSSLPD"] = IsQualified(sItem["LLSSLYQ"], sItem["LLSSL"], false);

                        if (sItem["LLSSLPD"] == "不合格")
                        {
                            mFlag_Bhg = true;
                            mbhggs += 1;
                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sItem["LLSSLYQ"] = sign ? sItem["LLSSLYQ"] : "----";
                    sItem["LLSSL"] = "----";
                    sItem["LLSSLPD"] = "----";
                    //   'mrsmainTablesItem["WHICH")"]="bgqk2_2"
                }

                if (sign == false)
                {
                    sItem["LLSSLPD"] = "不合格";
                    sItem["DHQDPD"] = "不合格";
                    mFlag_Bhg = true;
                    mbhggs += 1;
                }
                else
                {
                    mFlag_Hg = true;
                }


                if (jcxm.Contains("、导热系数、"))
                {
                    mcd = sItem["DRXSYQ"].Length;
                    mdwz = sItem["DRXSYQ"].IndexOf(',');
                    mcd = mcd - mdwz + 1;

                    string DEVCODE = String.IsNullOrEmpty(mItem["DEVCODE"]) ? "" : mItem["DEVCODE"];
                    if (DEVCODE == "" && DEVCODE.Contains("XCS17-067") || DEVCODE.Contains("XCS17-066"))
                    {
                        //var mrsDrxs1= ZM_DRJL.FirstOrDefault(u => u["SYLB"] == "qk2" && u["SYBH"] == mItem["JYDBH"]);
                        //var mrsDrxs = ZM_DRJL;
                        //sItem["DRXS"] = ZM_DRJL[0]["DRXS"];
                        //mItem["JCYJ"] = mItem["JCYJ"].Replace("10294", "10295");
                    }

                    sItem["DRXS"] = Math.Round(Conversion.Val(sItem["DRXS"]), mcd).ToString();

                    sItem["DRXSPD"] = IsQualified(sItem["DRXSYQ"], sItem["DRXS"], false);

                    if (sItem["DRXSPD"] == "不合格")
                    {
                        mFlag_Bhg = true;
                        mbhggs += 1;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }

                }
                else
                {
                    sItem["DRXSYQ"] = "----";
                    sItem["DRXSPD"] = "----";
                    sItem["DRXS"] = "----";
                }

                if (mbhggs == 0)
                {
                    jsbeizhu = "该组试件所检项目符合" + mItem["PDBZ"] + "标准要求。";
                    sItem["JCJG"] = "合格";
                    mAllHg = true;
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;

                    if (sign)
                    {
                        jsbeizhu = "该组试件不符合" + mItem["PDBZ"] + "标准要求。";

                        if (mFlag_Bhg & mFlag_Hg)
                        {
                            jsbeizhu = "该组试件所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                        }
                    }
                    else
                    {
                        jsbeizhu = "冻融中试件破坏，该组试件不符合" + mItem["PDBZ"] + "标准要求。";
                    }
                }
                return mAllHg;
            };



            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mSjdj = sItem["SJDJ"];//设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                {
                    mSjdj = "";
                }

                #region
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj && u["PZ"] == sItem["PZ"]);

                if (null == mrsDj)
                {
                    jsbeizhu = "依据不详\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    mJSFF = "";
                    mSz = 0;
                    continue;
                }

                mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].ToLower();

                foreach (var mrsGmdjb in extraGMD)
                {
                    if (mrsGmdjb["MC"].Trim() == sItem["SJDJ"] && mrsGmdjb["PZ"].Trim() == sItem["PZ"])
                    {
                        MItem[0]["G_GMD"] = mrsGmdjb["GMD"];
                        break;
                    }
                }
                sItem["QDYQ"] = "平均值≥" + Conversion.Val(MItem[0]["G_PJZ"]).ToString("0.0") + "MPa " + "单组最小值≥" + Conversion.Val(MItem[0]["G_MIN"]).ToString("0.0") + "MPa " + "含水率控制在" + mrsDj["HSL"];

                //'含水率
                sItem["HSLYQ"] = "8～12";
                MItem[0]["WCYQ"] = mrsDj["WCYQ"];


                var sjtabs = MItem[0]["SJTABS"];
                if (!string.IsNullOrEmpty(sjtabs))
                {
                    mAllHg = sjtabcalc(MItem[0], sItem);
                }
                else
                {

                }
                #endregion
            }
            #endregion
            #endregion 计算方法

            /************************ 代码结束 *********************/

        }
    }
}