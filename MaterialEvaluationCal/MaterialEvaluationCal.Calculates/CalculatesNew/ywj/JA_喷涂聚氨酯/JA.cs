using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JA : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_JA_DJ"];

            var data = retData;
            var mJCJG = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_JA"];
            //var YZSKB = data["YZSKB"];

            if (!data.ContainsKey("M_JA"))
            {
                data["M_JA"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_JA"];

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
            List<double> nArr = new List<double>();
            int mcd, mdwz = 0;
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            int mbhggs = 0;

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                mFlag_Hg = true;
                sign = true;
                mbhggs = 0;
                if (jcxm.Contains("、密度、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["MD"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sign = IsQualified(sItem["G_MD"], sItem["MD"], false) == "合格" ? sign : false;
                        sItem["HG_MD"] = sign ? "合格" : "不合格";

                        if (sItem["HG_MD"] == "不符合")
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
                    sItem["MD"] = "----";
                    sItem["HG_MD"] = "----";
                    sItem["G_MD"] = "----";
                }

                if (jcxm.Contains("、抗压强度、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["KYQD"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sign = IsQualified(sItem["G_KYQD"], sItem["KYQD"], false) == "合格" ? sign : false;
                        sItem["HG_KYQD"] = sign ? "合格" : "不合格";

                        if (sItem["HG_KYQD"] == "不符合")
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
                    sItem["HG_KYQD"] = "----";
                    sItem["G_KYQD"] = "----";
                }

                if (jcxm.Contains("、导热系数、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["DRXS2"]) ? sign : false;

                    mcd = sItem["G_DRXS"].Length;
                    mdwz = sItem["G_DRXS"].IndexOf('.');
                    mcd = mcd - mdwz + 1;


                    string DEVCODE = String.IsNullOrEmpty(mItem["DEVCODE"]) ? "" : mItem["DEVCODE"];
                    if (DEVCODE == "" && DEVCODE.Contains("XCS17-067") || DEVCODE.Contains("XCS17-066"))
                    {
                        //var mrsDrxs = YZSKB.FirstOrDefault(u => u["SYLB"].ToUpper() == "JA" && u["SYBH"] == mItem["JYDBH"]);
                        //sItem["DRXS2"] = mrsDrxs["DRXS"];
                        //mItem["JCYJ"] = mItem["JCYJ"].Replace("10294", "10295");
                    }

                    sItem["DRXS2"] = Math.Round(double.Parse(sItem["DRXS2"]), mcd).ToString();
                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sign = IsQualified(sItem["G_DRXS"], sItem["DRXS2"], false) == "合格" ? sign : false;
                        sItem["HG_DRXS"] = sign ? "合格" : "不合格";

                        if (sItem["HG_DRXS"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                        }
                        if (sign)
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["DRXS2"] = "----";
                    sItem["G_DRXS"] = "----";
                    sItem["HG_DRXS"] = "----";
                }

                if (jcxm.Contains("、尺寸变化率、"))
                {
                    sign = true;

                    sign = IsNumeric(sItem["CCWDXC"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXK"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXH"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sItem["HG_CCWDXC"] = IsQualified(sItem["G_CCWDX"], sItem["CCWDXC"], false);
                        sItem["HG_CCWDXK"] = IsQualified(sItem["G_CCWDX"], sItem["CCWDXK"], false);
                        sItem["HG_CCWDXH"] = IsQualified(sItem["G_CCWDX"], sItem["CCWDXH"], false);

                        if (sItem["HG_CCWDXC"] == "不符合" || sItem["HG_CCWDXK"] == "不符合" || sItem["HG_CCWDXH"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mAllHg = false;
                            mFlag_Bhg = true;

                        }
                        else
                        {
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sItem["CCWDXC"] = "----";
                    sItem["CCWDXK"] = "----";
                    sItem["CCWDXH"] = "----";
                    sItem["G_CCWDX"] = "----";
                    sItem["HG_CCWDXC"] = "----";
                    sItem["HG_CCWDXK"] = "----";
                    sItem["HG_CCWDXH"] = "----";
                }

                if (jcxm.Contains("、燃烧性能(E级)、"))
                {
                    if ("符合" == sItem["RSFJ"])
                    {
                        sItem["HG_RSFJ"] = "合格";
                        sItem["RSFJJG"] = "符合E级";
                        mFlag_Hg = true;
                    }
                    else
                    {

                        sItem["HG_RSFJ"] = "不合格";
                        sItem["RSFJJG"] = "不符合";
                        mFlag_Bhg = true;
                        mbhggs = mbhggs + 1;
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["RSFJ"] = "----";
                    sItem["RSFJJG"] = "----";
                    sItem["HG_RSFJ"] = "----";
                    sItem["G_RSFJ"] = "----";
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


            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim() && u["LB"] == sItem["XH"].Trim());

                if (null == mrsDj)
                {
                    jsbeizhu = "依据不详\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }


                sItem["G_DRXS"] = mrsDj["DRXS"];
                sItem["G_KYQD"] = mrsDj["KYQD"];
                sItem["G_MD"] = mrsDj["MD"];
                sItem["G_CCWDX"] = mrsDj["CCWDX"];
                sItem["G_RSFJ"] = mrsDj["RSFJ"];


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