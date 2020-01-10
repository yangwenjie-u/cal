using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class PQ : BaseMethods
    {
        public void Calc()
        {
            #region
            var extraDJ = dataExtra["BZ_PQ_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var jgsm = "";
            var jcjg = "";
            var SItems = data["S_PQ"];
            //var ZM_DRJL = data["ZM_DRJL"];

            if (!data.ContainsKey("M_PQ"))
            {
                data["M_PQ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_PQ"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var mItemHg = true;
            var mSjdj = "";
            var mJSFF = "";
            var jcxm = "";

            double mYqpjz, mXdy21, mDy21 = 0;
            bool sign = false;
            double md1, md2, md, sum, Gs, pjmd = 0;
            int xd = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            List<double> nArr = new List<double>();

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mFlag_Hg = true;
                sign = true;
                if (jcxm.Contains("、抗压强度、"))
                {
                    if (0 == Conversion.Val(mItem["F_QD1"]))
                    {
                        mAllHg = false;
                        return false;
                    }

                    mItem["G_QD"] = "平均值" + mItem["G_QD_AVG"] + "\r" + "单组最小值" + mItem["G_QD_MIN"];

                    var QualifiedlVal = IsQualified(mItem["G_QD_AVG"], mItem["AVG_QD"], true) == "符合" && IsQualified(mItem["G_QD_MIN"], mItem["MIN_QD"], true) == "符合";

                    if (QualifiedlVal)
                    {
                        mItem["GH_QD"] = "合格";
                    }

                    QualifiedlVal = IsQualified(mItem["G_QD_AVG"], mItem["AVG_QD"], true) == "不符合" && IsQualified(mItem["G_QD_MIN"], mItem["MIN_QD"], true) == "不符合";
                    if (QualifiedlVal)
                    {
                        mItem["GH_QD"] = "不合格";
                    }

                    if (mItem["GH_QD"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    mItem["G_QD_MIN"] = "----";
                    mItem["G_QD_AVG"] = "----";
                    mItem["G_QD"] = "----";
                    mItem["GH_QD"] = "----";
                    mItem["AVG_QD"] = "----";
                    mItem["MIN_QD"] = "----";
                    for (xd = 1; xd < 4; xd++)
                    {
                        mItem["F_QD" + xd] = "----";
                    }
                }

                if (jcxm.Contains("、干表观密度、"))
                {
                    mItem["GH_MD"] = IsQualified(mItem["G_MD"], mItem["AVG_MD"], true);
                    if (mItem["GH_MD"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    mItem["G_MD"] = "----";
                    mItem["GH_MD"] = "----";
                    mItem["AVG_MD"] = "----";

                    for (xd = 1; xd < 4; xd++)
                    {
                        mItem["F_MD" + xd] = "----";
                    }
                }

                if (jcxm.Contains("、冻后强度、"))
                {
                    sign = true;
                    for (Gs = 1; Gs < 4; Gs++)
                    {
                        if (!IsNumeric(mItem["F_QD" + Gs]))
                        {
                            sign = false;
                            break;
                        }
                    }
                    for (Gs = 1; Gs < 4; Gs++)
                    {
                        if (!IsNumeric(mItem["F_DHQD" + Gs]))
                        {
                            sign = false;
                            break;
                        }
                    }

                    if (!sign)
                    {
                        mAllHg = false;
                        return false;
                    }
                    nArr.Clear();
                    for (Gs = 1; Gs < 4; Gs++)
                    {

                        sum = 0;
                        md1 = double.Parse(mItem["F_QD" + Gs].Trim());
                        md2 = double.Parse(mItem["F_DHQD" + Gs].Trim());

                        pjmd = Math.Round(100 * (md1 - md2) / md1, 1);

                        nArr.Add(pjmd);
                        mItem["F_KDQD" + Gs] = pjmd.ToString("0.0");
                    }
                    nArr.Sort();
                    mItem["MAX_KDQD"] = nArr[3].ToString("0.0");
                    mItem["GH_KDQD"] = IsQualified(mItem["G_KDQD"], mItem["MAX_KDQD"], false);

                    mItem["G_KDZL"] = "≤5";
                    mItem["GH_KDZL"] = IsQualified(mItem["G_KDZL"], mItem["MAX_KDZL"], false);


                    if (mItem["GH_KDQD"] == "不合格" || mItem["GH_KDZL"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    mItem["MAX_KDQD"] = "----";
                    mItem["G_KDQD"] = "----";
                    mItem["GH_KDQD"] = "----";
                    mItem["GH_KDQD"] = "----";
                    mItem["G_KDZL"] = "----";
                    mItem["MAX_KDZL"] = "----";
                    for (xd = 1; xd < 4; xd++)
                    {
                        mItem["F_KDQD" + xd] = "----";
                        mItem["F_KDZL" + xd] = "----";
                    }
                }

                if (jcxm.Contains("、干燥收缩值、"))
                {
                    mItem["GH_SS"] = IsQualified(mItem["G_SS"], mItem["MAX_SS"], false);
                    if (mItem["GH_SS"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    mItem["GH_SS"] = "----";
                    mItem["G_SS"] = "----";
                    mItem["MAX_SS"] = "----";
                    for (xd = 1; xd < 4; xd++)
                    {
                        mItem["F_SS" + xd] = "----";
                    }
                }


                if (jcxm.Contains("、导热系数、"))
                {
                    mItem["AVG_DR"] = sItem["DRXS"].Trim();
                    var mcd = mItem["G_DR"].Trim().Length;
                    var mdwz = mItem["G_DR"].Trim().IndexOf('.');
                    mcd = mcd - mdwz + 1;

                    //if (mItem["G_DR"].Contains("XCS17-067") || mItem["G_DR"].Contains("XCS17-066"))
                    string DEVCODE = String.IsNullOrEmpty(mItem["G_DR"]) ? "" : mItem["G_DR"];
                    if (DEVCODE == "" && DEVCODE.Contains("XCS17-067") || DEVCODE.Contains("XCS17-066"))
                    {
                        //var mrsDrxs = ZM_DRJL.FirstOrDefault(u => u["SYLB"] == "pq" && u["SYBH"] == mItem["JYDBH"]);
                        // mItem["DRXS"] = mrsDrxs["DRXS"];
                        // mItem["JCYJ"] = mItem["JCYJ"].Replace("10294", "10295");
                    }

                    sItem["DRXS"] = Math.Round(double.Parse(sItem["DRXS"]), mcd).ToString();
                    mItem["GH_DR"] = IsQualified(mItem["G_DR"], sItem["DRXS"], false);
                    if (mItem["GH_DR"] == "不合格")
                    {
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    mItem["GH_DR"] = "----";
                    mItem["G_DR"] = "----";
                    sItem["DRXS"] = "----";
                }

                if (mbhggs == 0)
                {
                    jsbeizhu = "该组试件所检项目符合上述标准要求。";
                    sItem["JCJG"] = "合格";
                }

                if (mbhggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "该组试件不符合标准要求。";
                    if (mFlag_Bhg && mFlag_Hg)
                    {
                        jsbeizhu = "该组试件所检项目符合上述标准要求。";
                    }
                }
                return mAllHg;
            };

            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mSjdj = string.IsNullOrEmpty(sItem["SJDJ"]) ? "" : sItem["SJDJ"];

                Gs = extraDJ.Count;
                for (xd = 0; xd < Gs; xd++)
                {
                    if (extraDJ[xd]["QDDJ"].Trim() == sItem["QDDJ"].Trim())
                    {
                        MItem[0]["G_QD_MIN"] = extraDJ[xd]["QDMIN"];
                        MItem[0]["G_QD_AVG"] = extraDJ[xd]["QDAVG"];
                        break;
                    }
                }

                for (xd = 0; xd < Gs; xd++)
                {
                    if (extraDJ[xd]["MDDJ"].Trim() == sItem["MDDJ"].Trim())
                    {
                        MItem[0]["G_MD"] = extraDJ[xd]["BGMD"];
                        MItem[0]["G_SS"] = extraDJ[xd]["GZSS"];
                        MItem[0]["G_DR"] = extraDJ[xd]["DRXS"];
                        break;
                    }
                }
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

