using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JWA : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_JWA_DJ"];

            var data = retData;
            var mJCJG = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_JWA"];
            //var YZSKB = data["YZSKB"];

            if (!data.ContainsKey("M_JWA"))
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
                if (jcxm.Contains("、芯密度、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["XMD"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sign = IsQualified(sItem["G_XMD"], sItem["XMD"], false) == "合格" ? sign : false;
                        sItem["HG_XMD"] = sign ? "合格" : "不合格";

                        if (sItem["HG_XMD"] == "不符合")
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
                    sItem["XMD"] = "----";
                    sItem["HG_XMD"] = "----";
                    sItem["G_XMD"] = "----";
                }

                if (jcxm.Contains("、压缩强度、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["YSQD"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sign = IsQualified(sItem["G_YSQD"], sItem["YSQD"], false) == "合格" ? sign : false;
                        sItem["HG_YSQD"] = sign ? "合格" : "不合格";

                        if (sItem["HG_YSQD"] == "不符合")
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
                    sItem["HG_YSQD"] = "----";
                    sItem["G_YSQD"] = "----";
                }


                if (jcxm.Contains("、导热系数(23℃)、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["DRXS1"]) ? sign : false;

                    mcd = sItem["G_DRXS"].Length;
                    mdwz = sItem["G_DRXS"].IndexOf('.');
                    mcd = mcd - mdwz + 1;

                    string DEVCODE = String.IsNullOrEmpty(mItem["DEVCODE"]) ? "" : mItem["DEVCODE"];
                    if (DEVCODE == "" && DEVCODE.Contains("XCS17-067") || DEVCODE.Contains("XCS17-066"))
                    {
                        //var mrsDrxs = YZSKB.FirstOrDefault(u => u["SYLB"] == "jww" && u["SYBH"] == mItem["JYDBH"]);
                        //sItem["DRXS"] = mrsDrxs["DRXS"];
                        //mItem["JCYJ"] = mItem["JCYJ"].Replace("10294", "10295");
                    }

                    sItem["DRXS1"] = Math.Round(double.Parse(sItem["DRXS1"]), mcd).ToString();
                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sign = IsQualified(sItem["G_DRXS"], sItem["DRXS1"], false) == "合格" ? sign : false;
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
                    sItem["DRXS1"] = "----";
                    sItem["G_DRXS"] = "----";
                    sItem["HG_DRXS"] = "----";
                }

                if (jcxm.Contains("、尺寸稳定性(70℃,48h)、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["CCWDXC1"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXK1"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXH1"]) ? sign : false;

                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sItem["HG_CCWDXC1"] = IsQualified(sItem["G_CCWDX1"], sItem["CCWDXC1"], false);
                        sItem["HG_CCWDXK1"] = IsQualified(sItem["G_CCWDX1"], sItem["CCWDXK1"], false);
                        sItem["HG_CCWDXH1"] = IsQualified(sItem["G_CCWDX1"], sItem["CCWDXH1"], false);

                        if (sItem["HG_CCWDXC1"] == "不符合" || sItem["HG_CCWDXK1"] == "不符合" || sItem["HG_CCWDXH1"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;
                            mAllHg = false;
                        }

                        if (sign)
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = true;

                        }
                    }
                }
                else
                {
                    sItem["CCWDXC1"] = "----";
                    sItem["CCWDXK1"] = "----";
                    sItem["CCWDXH1"] = "----";
                    sItem["G_CCWDX1"] = "----";
                    sItem["HG_CCWDXC1"] = "----";
                    sItem["HG_CCWDXK1"] = "----";
                    sItem["HG_CCWDXH1"] = "----";
                }

                if (jcxm.Contains("、尺寸稳定性(-30℃,48h)、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["CCWDXC2"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXK2"]) ? sign : false;
                    sign = IsNumeric(sItem["CCWDXH2"]) ? sign : false;


                    if (!sign)
                    {
                        return false;
                    }
                    else
                    {
                        sItem["CCWDXC2"] = IsQualified(sItem["G_CCWDX2"], sItem["CCWDXC2"], false);
                        sItem["CCWDXK2"] = IsQualified(sItem["G_CCWDX2"], sItem["CCWDXK2"], false);
                        sItem["CCWDXH2"] = IsQualified(sItem["G_CCWDX2"], sItem["CCWDXH2"], false);

                        if (sItem["HG_CCWDXC2"] == "不符合" || sItem["HG_CCWDXK2"] == "不符合" || sItem["HG_CCWDXH2"] == "不符合")
                        {
                            mbhggs = mbhggs + 1;

                        }
                        if (sign)
                        {
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mFlag_Bhg = false;
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["CCWDXC2"] = "----";
                    sItem["CCWDXK2"] = "----";
                    sItem["CCWDXH2"] = "----";
                    sItem["G_CCWDX2"] = "----";
                    sItem["HG_CCWDXC2"] = "----";
                    sItem["HG_CCWDXK2"] = "----";
                    sItem["HG_CCWDXH2"] = "----";
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

                var mrsDj = extraDJ.FirstOrDefault(u => u["LB"] == sItem["XH"].Trim());

                if (null == mrsDj)
                {
                    jsbeizhu = "试件尺寸为空\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }

                sItem["G_DRXS"] = mrsDj["DRXS"];
                sItem["G_YSQD"] = mrsDj["YSQD"];
                sItem["G_XMD"] = mrsDj["XMD"];
                sItem["G_CCWDX1"] = mrsDj["CCWDX1"];
                sItem["G_CCWDX2"] = mrsDj["CCWDX2"];
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