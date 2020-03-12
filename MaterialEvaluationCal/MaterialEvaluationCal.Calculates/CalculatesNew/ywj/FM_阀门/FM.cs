using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class FM : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItems = retData["S_FM"];
            var extraDJ = dataExtra["BZ_FM_DJ"];
            var extraMFXLL = dataExtra["BZ_FMMFXLL"];
            var extraYLSJ = dataExtra["BZ_FMYLSJ"];

            if (!retData.ContainsKey("M_FM"))
            {
                retData["M_FM"] = new List<IDictionary<string, string>>();
            }
            var MItem = retData["M_FM"];

            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            bool mAllHg = true;
            int mbhggs = 0;//不合格数量
            bool mFlag_Hg = false;
            bool mFlag_Bhg = false;
            bool sign = true;
            var jcxm = "";
            string mJSFF = "";
            double zj1, zj2 = 0;
            double md1, md2, md, pjmd = 0;

            Func<IDictionary<string, string>, IDictionary<string, string>, bool> sjtabcalc = delegate (IDictionary<string, string> mItem, IDictionary<string, string> sItem)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                sign = true;
                mbhggs = 0;
                if (jcxm.Contains("、上密封试验、"))
                {
                    md = Conversion.Val(sItem["SMF_GS"]);

                    sItem["SMFSYPD"] = md > 0 ? "不合格" : "合格";

                    if (sItem["SMFSYPD"] == "不合格")
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
                    sItem["SMF_GS"] = "----";
                    sItem["SMF_YL"] = "----";
                    sItem["SMF_SJ"] = "----";
                    sItem["SMFSYPD"] = "----";
                    sItem["SMFSY"] = "----";
                    sItem["SMFSYYQ"] = "----";
                }
                if (jcxm.Contains("、密封试验、"))
                {
                    md = 0;
                    if (sItem["MF_GS"] == "----")
                        md = Conversion.Val(sItem["MF_BGS"]);
                    //MItem[0]["WHICH"] = "1"
                    else
                        md = Conversion.Val(sItem["MF_GS"]);
                    sItem["MFSYPD"] = md > 0 ? "不合格" : "合格";

                    if (sItem["MFSYPD"] == "不合格")
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
                    sItem["MFSYYQ"] = "----";
                    sItem["MFSYPD"] = "----";
                    sItem["MFSY"] = "----";
                    sItem["MF_ZD_SLL"] = "----";
                    sItem["MF_YX_SLL"] = "----";
                    sItem["MF_SJ"] = "----";
                    sItem["MF_GS"] = "----";
                    sItem["MF_YL"] = "----";
                }

                if (jcxm.Contains("、壳体试验、"))
                {
                    md = Conversion.Val(sItem["QT_GS"]);
                    sItem["KTSYPD"] = md > 0 ? "不合格" : "合格";

                    if (sItem["KTSYPD"] == "不合格")
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
                    sItem["KTSY"] = "----";
                    sItem["KTSYYQ"] = "----";
                    sItem["KTSYPD"] = "----";
                    sItem["QT_YL"] = "----";
                    sItem["QT_SJ"] = "----";
                    sItem["QT_GS"] = "----";
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
                        jsbeizhu = "该组试样所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                    }
                }
                return mAllHg;
            };


            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                string mfmzl, mgcyl, mmfxll, mktsy = "";

                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["FMZL"].Trim());

                if (null == mrsDj)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "试件尺寸为空/r/n";
                    continue;
                }

                mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].Trim().ToLower();
                mfmzl = sItem["FMZL"];

                if (mfmzl.Trim() != "止回阀")
                {
                    mfmzl = "其它类型阀";
                }
                zj1 = Convert.ToDouble(sItem["FMZJ"]);
                if (zj1 == 63)
                {
                    zj1 = 50;
                }
                var mrsYlsjList = extraYLSJ.Where(u => u["MC"] == mfmzl.Trim());
                Dictionary<string, string> mrsYlsj = null;

                if (null == mrsYlsjList)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "试件尺寸为空/r/n";
                    continue;
                }
                foreach (var item in mrsYlsjList)
                {
                    if (IsQualified(item["ZJFW"], zj1.ToString(), true) == "符合")
                    {
                        mrsYlsj = (Dictionary<string, string>)item;
                        break;
                    }
                }
                if (null == mrsYlsj)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "试件尺寸为空/r/n";
                    continue;
                }

                var mrsMfxll = extraMFXLL.FirstOrDefault(u => u["MC"] == sItem["SJDJ"].Trim());

                if (null == mrsMfxll)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "试件尺寸为空/r/n";
                    continue;
                }

                mgcyl = Math.Round(1.1 * Conversion.Val(sItem["GCYL"]), 2).ToString("0.00") + "MPa";
                sItem["SMFSYYQ"] = "20℃ " + mgcyl + "持续" + mrsYlsj["SMFSY"] + "s 无渗漏。";
                sItem["SMF_YL"] = mgcyl.Replace("MPa", "");
                sItem["SMF_SJ"] = mrsYlsj["SMFSY"];
                sItem["MF_YL"] = mgcyl.Replace("MPa", "");
                sItem["MF_SJ"] = mrsYlsj["MFSY"];

                mmfxll = Math.Round(Double.Parse(sItem["FMZJ"]) * Conversion.Val(mrsMfxll["ZJBS"]), 2).ToString("0.00");
                if (sItem["SJDJ"] == "A级")
                {
                    sItem["MFSYYQ"] = "20℃ " + mgcyl + "持续" + mrsYlsj["MFSY"] + "s 无渗漏。";
                    sItem["MF_YX_SLL"] = "----";
                }
                else
                {
                    sItem["MFSYYQ"] = "20℃ " + mgcyl + "持续" + mrsYlsj["MFSY"] + "s 最大允许渗漏量" + mmfxll + "mm+scsup3+scend/s。";
                    sItem["MF_YX_SLL"] = mmfxll;
                }


                mktsy = Math.Round(1.5 * Conversion.Val(sItem["GCYL"]), 2).ToString("0.00") + "MPa";
                sItem["QT_YL"] = mktsy.Replace("MPa", "");
                sItem["KTSYYQ"] = "20℃ " + mktsy + "持续" + mrsYlsj["KTSY"] + "s 无渗漏。";
                sItem["QT_SJ"] = mrsYlsj["KTSY"];

                var sjtabs = MItem[0]["SJTABS"];
                if (string.IsNullOrEmpty(sjtabs))
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
            /************************ 代码结束 *********************/
        }
    }
}
