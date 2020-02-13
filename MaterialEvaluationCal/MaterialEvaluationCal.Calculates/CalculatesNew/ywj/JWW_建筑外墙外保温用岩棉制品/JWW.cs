using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JWW2 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_JWW_DJ"];

            var data = retData;
            var mJCJG = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_JWA"];
            var ZM_DRJL = data["ZM_DRJL"];

            if (!data.ContainsKey("M_JWW"))
            {
                data["M_JWA"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_JWA"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mJCJG;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";

            bool sign = true;
            bool mark = false;
            List<double> nArr = new List<double>();
            int mcd, mdwz, hgs = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;
            int xd, Gs = 0;
            int md1, md2, xd1, xd2, md, pjmd, sum, cd, kd, hd, zl, cl, length = 0;
            string CPMC, zpxs, bcz = "";

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mFlag_Hg = true;
                sign = true;
                hgs = 0;
                CPMC = sItem["CPMC"].Trim();
                zpxs = sItem["zpxs"].Trim();

                sign = true;
                mark = true;
                if (jcxm.Contains("、导热系数、"))
                {
                    if (mItem["devcode"].Contains("XCS17-067") || mItem["devcode"].Contains("XCS17-066"))
                    {
                        var mrsDrxs = ZM_DRJL.FirstOrDefault(u => u["sylb"] == "jww" && u["sybh"] == mItem["jydbh"]);
                        sItem["W_DRXS"] = mrsDrxs["drxs"];
                        mItem["Jcyj"] = mItem["Jcyj"].Replace("10294", "10295");
                    }


                    if (Conversion.Val(mItem["W_DRXS"]) == 0)
                    {
                        return false;
                    }
                    //1-棉,棉,1|板,板,0|带,带,0|毡,毡,0|缝毡,缝毡,0|贴面毡,贴面毡,0|管壳,管壳,0
                    if (sItem["BCDRXS"].Trim() != "----" && sItem["BCDRXS"].Trim() != "")
                    {
                        bcz = sItem["BCDRXS"];
                        mark = IsQualified(bcz, mItem["W_DRXS"], false) == "合格" ? mark : false;
                        mark = IsQualified(mItem["G_DRXS"], mItem["W_DRXS"], false) == "合格" ? mark : false;

                        mItem["W_DRXS1"] = mItem["W_DRXS"];

                        mcd = sItem["G_DRXS"].Length;
                        mdwz = sItem["G_DRXS"].IndexOf('.');
                        mcd = mcd - mdwz + 1;

                        mItem["W_DRXS1"] = Math.Round(double.Parse(mItem["W_DRXS"]), mcd).ToString();
                        mItem["G_DRXS"] = mItem["G_DRXS"] + "（标准要求值）, 且" + bcz + "标称值";

                    }
                    else
                    {
                        mark = IsQualified(mItem["G_DRXS"], mItem["W_DRXS"], false) == "合格" ? mark : false;
                        mItem["W_DRXS1"] = mItem["W_DRXS"];
                        mcd = sItem["G_DRXS"].Length;
                        mdwz = sItem["G_DRXS"].IndexOf('.');
                        mcd = mcd - mdwz + 1;
                        mItem["W_DRXS1"] = Math.Round(double.Parse(mItem["W_DRXS"]), mcd).ToString();

                    }
                    if (mark)
                    {
                        mItem["GH_DRXS"] = "合格";
                    }
                    else
                    {
                        mItem["GH_DRXS"] = "不合格";
                    }
                }
                else
                {
                    sign = false;
                }
                if (!sign)
                {
                    mItem["W_DRXS"] = "----";
                    mItem["G_DRXS"] = "----";
                    mItem["GH_DRXS"] = "----";
                }

                sign = true;
                mark = true;
                if (jcxm.Contains("、压缩强度、"))
                {
                    if (Conversion.Val(mItem["W_YSQD"]) == 0)
                    {
                        return false;
                    }
                    if (sItem["CPMC"].Contains("25975 - 2018"))
                    {
                        if (sItem["zpxs"] == "岩棉板" && Double.Parse(sItem["yphd"]) < 50)
                            mItem["G_YSQD"] = "≥20";
                    }

                    if (sItem["BCYSQD"].Trim() != "----")
                    {
                        bcz = sItem["BCYSQD"];
                        mark = IsQualified(bcz, mItem["W_YSQD"], false) == "合格" ? mark : false;

                        if (sItem["G_YSQD"].Trim() != "----" && sItem["G_YSQD"].Trim() != "")
                            mItem["G_YSQD"] = bcz + "标称值";
                        else
                        {
                            mark = IsQualified(mItem["G_YSQD"], mItem["W_YSQD"], false) == "合格" ? mark : false;
                            mItem["G_YSQD"] = mItem["G_YSQD"] + "（标准要求值）,且" + bcz + "标称值";
                        }
                    }
                    else
                        mark = IsQualified(mItem["G_YSQD"], mItem["W_YSQD"], false) == "合格" ? mark : false;
                    if (mark)
                    {
                        mItem["GH_YSQD"] = "合格";
                    }
                    else
                    {
                        mItem["GH_YSQD"] = "不合格";
                    }
                }
                else
                {
                    sign = false;
                }
                if (!sign)
                {
                    mItem["G_YSQD"] = "----";
                    mItem["GH_YSQD"] = "----";
                    mItem["W_YSQD"] = "----";
                }

                sign = true;

                if (jcxm.Contains("、质量吸湿率、"))
                {
                    sign = IsNumeric(mItem["W_ZLXSL"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["GH_ZLXSL"] = IsQualified(mItem["G_ZLXSL"], mItem["W_ZLXSL"], false);
                    }
                }
                else
                {
                    mItem["W_ZLXSL"] = "----";
                    mItem["G_ZLXSL"] = "----";
                    mItem["GH_ZLXSL"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、体积吸水率、"))
                {
                    mItem["G_XSL"] = "≤5";
                    sign = IsNumeric(mItem["W_XSL"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["GH_XSL"] = IsQualified(mItem["G_XSL"], mItem["W_XSL"], false);
                    }
                }
                else
                {
                    mItem["W_XSL"] = "----";
                    mItem["G_XSL"] = "----";
                    mItem["GH_XSL"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、密度、"))
                {
                    sign = IsNumeric(mItem["W_pjMD"]) ? sign : false;
                    sign = IsNumeric(mItem["W_MDPC"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["GH_MD"] = IsQualified("-10～10", mItem["W_MDPC"], false);
                    }

                    mItem["G_MD"] = "-10%～10%";
                }
                else
                {
                    mItem["W_pjMD"] = "----";
                    mItem["W_MDPC"] = "----";
                    mItem["G_MD"] = "----";
                    mItem["GH_MD"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、尺寸稳定性、"))
                {
                    sign = IsNumeric(mItem["W_CDWDX"]) ? sign : false;
                    sign = IsNumeric(mItem["W_KDWDX"]) ? sign : false;
                    sign = IsNumeric(mItem["W_HDWDX"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["GH_MD"] = IsQualified(mItem["G_CCWDX"], mItem["W_CDWDX"], false);
                        mItem["GH_MD"] = IsQualified(mItem["G_CCWDX"], mItem["W_KDWDX"], false);
                        mItem["GH_MD"] = IsQualified(mItem["G_CCWDX"], mItem["W_HDWDX"], false);
                    }
                }
                else
                {
                    mItem["W_CDWDX"] = "----";
                    mItem["W_KDWDX"] = "----";
                    mItem["W_HDWDX"] = "----";
                    mItem["G_CCWDX"] = "----";
                    mItem["GH_CDWDX"] = "----";
                    mItem["GH_KDWDX"] = "----";
                    mItem["GH_HDWDX"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、短期吸水量、"))
                {
                    sign = IsNumeric(mItem["W_DQXSL"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["GH_DQXSL"] = IsQualified(mItem["G_DQXSL"], mItem["W_DQXSL"], false);
                    }
                }
                else
                {
                    mItem["W_DQXSL"] = "----";
                    mItem["G_DQXSL"] = "----";
                    mItem["GH_DQXSL"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、憎水率、"))
                {
                    mItem["G_ZSL"] = "≥98.0";
                    sign = IsNumeric(mItem["W_ZSL"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        mItem["GH_ZSL"] = IsQualified(mItem["G_ZSL"], mItem["W_ZSL"], false);
                    }
                }
                else
                {
                    mItem["W_ZSL"] = "----";
                    mItem["G_ZSL"] = "----";
                    mItem["GH_ZSL"] = "----";
                }

                sign = true;
                mark = true;
                if (jcxm.Contains("、长期吸水量、"))
                {
                    sign = IsNumeric(mItem["W_CQXSL"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        if (sItem["CQXSLZB"].Trim() != "----" && sItem["CQXSLZB"] != "")
                        {
                            bcz = sItem["CQXSLZB"];
                            mark = IsQualified(bcz, mItem["W_CQXSL"], false) == "合格" ? mark : false;
                            mark = IsQualified(mItem["G_CQXSL"], mItem["W_CQXSL"], false) == "合格" ? mark : false;

                            mItem["G_CQXSL"] = mItem["G_CQXSL"] + "（标准要求值）,且" + bcz + "标称值";
                        }
                        else
                            mark = IsQualified(mItem["G_CQXSL"], mItem["W_CQXSL"], false) == "合格" ? mark : false;

                        if (mark)
                            mItem["GH_CQXSL"] = "合格";
                        else
                            mItem["GH_CQXSL"] = "不合格";
                    }
                }
                else
                {
                    mItem["W_CQXSL"] = "----";
                    mItem["G_CQXSL"] = "----";
                    mItem["GH_CQXSL"] = "----";
                }

                sign = true;
                mark = true;
                if (jcxm.Contains("、抗拉强度、")|| jcxm.Contains("、垂直于表面的抗拉强度、"))
                {
                    sign = IsNumeric(mItem["W_KLQD"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                   
                            mark = IsQualified(mItem["G_KLQD"], mItem["W_KLQD"], false) == "合格" ? mark : false;

                        if (mark)
                            mItem["GH_KLQD"] = "合格";
                        else
                            mItem["GH_KLQD"] = "不合格";
                    }
                }
                else
                {
                    mItem["W_KLQD"] = "----";
                    mItem["G_KLQD"] = "----";
                    mItem["GH_KLQD"] = "----";
                }

                if (MItem[0]["GH_DRXS"] == "不合格" ||
                MItem[0]["GH_YSQD"] == "不合格" ||
                MItem[0]["GH_CDWDX"] == "不合格" ||
                MItem[0]["GH_KDWDX"] == "不合格" ||
                MItem[0]["GH_HDWDX"] == "不合格" ||
                MItem[0]["GH_DQXSL"] == "不合格" ||
                MItem[0]["GH_ZLXSL"] == "不合格" ||
                MItem[0]["GH_CQXSL"] == "不合格" ||
                MItem[0]["GH_KLQD"] == "不合格" ||
                MItem[0]["GH_ZSL"] == "不合格")
                {
                    mbhggs += 1;
                    mAllHg = false;
                }
                if (mbhggs == 0)
                {
                    jsbeizhu = "该样品所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                    sItem["JCJG"] = "合格";
                    mAllHg = true;
                }

                if (mbhggs > 0)
                {
                    jsbeizhu = "该样品所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";

                    if (MItem[0]["GH_DRXS"] == "合格" ||
                   MItem[0]["GH_YSQD"] == "合格" ||
                   MItem[0]["GH_CDWDX"] == "合格" ||
                   MItem[0]["GH_KDWDX"] == "合格" ||
                   MItem[0]["GH_HDWDX"] == "合格" ||
                   MItem[0]["GH_DQXSL"] == "合格" ||
                   MItem[0]["GH_ZLXSL"] == "合格" ||
                   MItem[0]["GH_CQXSL"] == "合格" ||
                   MItem[0]["GH_KLQD"] == "合格" ||
                   MItem[0]["GH_ZSL"] == "合格")
                    {
                        jsbeizhu = "该样品所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                    }

                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                
                return mAllHg;
            };


            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                MItem[0]["G_CCWDX"] = "----";
                MItem[0]["G_CQXSL"] = "----";
                MItem[0]["G_DQXSL"] = "----";
                MItem[0]["G_DRXS"] = "----";
                MItem[0]["G_GQPXD"] = "----";
                MItem[0]["G_HDPC"] = "----";
                MItem[0]["G_JSBLL"] = "----";
                MItem[0]["G_KLQD"] = "----";
                MItem[0]["G_MD"] = "----";
                MItem[0]["G_XSL"] = "----";
                MItem[0]["G_YSQD"] = "----";
                MItem[0]["G_ZLXSL"] = "----";
                foreach (var mrsDj in extraDJ)
                {
                    if (null == mrsDj)
                    {
                        continue;
                    }

                    if (sItem["CPMC"].Trim() == "建筑外墙外保温用岩棉制品(GB/T 25975-2010)")
                    {
                        if (MItem[0]["QRBM"].Contains("90"))
                            MItem[0]["which"] = "bgjww_99、bgjww_88、bgjww";
                        else
                            MItem[0]["which"] = "bgjww_88、bgjww";

                        if (sItem["CPMC"].Trim() == mrsDj["YPYT"] && sItem["LX"].Trim() == mrsDj["LX"])
                        {
                            MItem[0]["G_ZLXSL"] = mrsDj["G_ZLXSL"];
                            MItem[0]["G_CCWDX"] = mrsDj["G_CCWDX"];
                            MItem[0]["G_CQXSL"] = mrsDj["G_CQXSL"];
                            MItem[0]["G_DQXSL"] = mrsDj["G_DQXSL"];
                            MItem[0]["G_GQPXD"] = mrsDj["G_GQPXD"];
                            MItem[0]["G_KLQD"] = mrsDj["G_KLQD"];
                            MItem[0]["G_DRXS"] = mrsDj["G_DRXS"];
                            MItem[0]["G_YSQD"] = mrsDj["G_YSQD"];
                        }
                    }

                    if (sItem["CPMC"].Trim() == "建筑外墙外保温用岩棉制品(GB/T 25975-2018)")
                    {
                        //if (MItem[0]["QRBM"].Contains("90"))
                        //    MItem[0]["which"] = "bgjww_99、bgjww_88、bgjww";
                        //else
                        //    MItem[0]["which"] = "bgjww_88、bgjww";

                        if (sItem["CPMC"].Trim() == mrsDj["YPYT"] && sItem["LX"].Trim() == mrsDj["LX"])
                        {
                            MItem[0]["G_ZLXSL"] = mrsDj["G_ZLXSL"];
                            MItem[0]["G_CCWDX"] = mrsDj["G_CCWDX"];
                            MItem[0]["G_CQXSL"] = mrsDj["G_CQXSL"];
                            MItem[0]["G_DQXSL"] = mrsDj["G_DQXSL"];
                            MItem[0]["G_GQPXD"] = mrsDj["G_GQPXD"];
                            MItem[0]["G_KLQD"] = mrsDj["G_KLQD"];
                            MItem[0]["G_DRXS"] = mrsDj["G_DRXS"];
                            MItem[0]["G_YSQD"] = mrsDj["G_YSQD"];
                        }
                    }

                    if (sItem["CPMC"].Trim() == "岩棉薄抹灰外墙外保温系统材料(JG/T 483-2015)")
                    {
                        //if (MItem[0]["QRBM"].Contains("90"))
                        //    MItem[0]["which"] = "bgjww_99、bgjww_88、bgjww";
                        //else
                        //    MItem[0]["which"] = "bgjww_88、bgjww";

                        if (sItem["CPMC"].Trim() == mrsDj["YPYT"] && sItem["zpxs"].Trim() == mrsDj["ZPXS"] && sItem["LX"].Trim() == mrsDj["LX"])
                        {
                            MItem[0]["G_ZLXSL"] = mrsDj["G_ZLXSL"];
                            MItem[0]["G_CCWDX"] = mrsDj["G_CCWDX"];
                            MItem[0]["G_CQXSL"] = mrsDj["G_CQXSL"];
                            MItem[0]["G_DQXSL"] = mrsDj["G_DQXSL"];
                            MItem[0]["G_GQPXD"] = mrsDj["G_GQPXD"];
                            MItem[0]["G_KLQD"] = mrsDj["G_KLQD"];
                            MItem[0]["G_DRXS"] = mrsDj["G_DRXS"];
                            MItem[0]["G_YSQD"] = mrsDj["G_YSQD"];
                        }
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

            if (mAllHg && mJCJG != "----")
            {
                mJCJG = "合格";
            }


            MItem[0]["JCJG"] = mJCJG;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}