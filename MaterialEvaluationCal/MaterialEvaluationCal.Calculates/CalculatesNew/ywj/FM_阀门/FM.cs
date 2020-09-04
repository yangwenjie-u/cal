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
            bool jyGs = true;
            string fmdj = "";

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
                //var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["FMZL"].Trim());

                //if (null == mrsDj)
                //{
                //    mjcjg = "不下结论";
                //    sItem["JCJG"] = "不合格";
                //    mAllHg = false;
                //    jsbeizhu = jsbeizhu + "试件尺寸为空/r/n";
                //    continue;
                //}
                //mJSFF = string.IsNullOrEmpty(mrsDj["JSFF"]) ? "" : mrsDj["JSFF"].Trim().ToLower();
                mfmzl = sItem["FMZL"];

                if (!mfmzl.Trim().Contains("止回阀"))
                {
                    mfmzl = "其它类型阀";
                }
                zj1 = Conversion.Val(sItem["GGXH"]);
                zj1 =GetSafeDouble( GetNum(sItem["GGXH"]));
                if (zj1 == 63)
                {
                    zj1 = 50;
                }
                //种类名称中包含止回阀
                var mrsYlsjList = extraYLSJ.Where(u => mfmzl.Trim().Contains(u["MC"]));
                Dictionary<string, string> mrsYlsj = null;

                if (null == mrsYlsjList)
                {
                    mjcjg = "不下结论";
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
                    mjcjg = "不下结论";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "试件尺寸为空/r/n";
                    continue;
                }

                var mrsMfxll = extraMFXLL.FirstOrDefault(u => u["MC"] == sItem["SJDJ"].Trim());
                if ("----" == sItem["SJDJ"])
                {
                    //当等级为 ---- 时 根据密封材料来决定密封试验的最大泄露率   非金属弹性密封副阀 按A级要求  金属密封副阀门  按D级要求 
                    if ("非金属" == sItem["MFCL"])
                    {
                        mrsMfxll = extraMFXLL.FirstOrDefault(u => u["MC"] == "A级");
                        fmdj = "A级";
                    }
                    else
                    {
                        mrsMfxll = extraMFXLL.FirstOrDefault(u => u["MC"] == "D级");
                        fmdj = "D级";
                    }
                }
                else
                {
                    mrsMfxll = extraMFXLL.FirstOrDefault(u => u["MC"] == sItem["SJDJ"].Trim());
                    fmdj = sItem["SJDJ"];
                }

                if (null == mrsMfxll)
                {
                    mjcjg = "不下结论";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "试件尺寸为空/r/n";
                    continue;
                }

                #region  直接录合格数量的做法
                ////不合格数量
                //if (Conversion.Val(sItem["MFSY"]) > Conversion.Val(sItem["FMGS"]))
                //{
                //    sItem["MF_GS"] = "----";
                //    sItem["MFSY"] = "合格数量不能大于总阀门个数";
                //    jyGs = false;
                //}
                //else
                //{
                //    sItem["MF_GS"] = (Conversion.Val(sItem["FMGS"]) - Conversion.Val(sItem["MFSY"])).ToString();
                //}

                //if (Conversion.Val(sItem["KTSY"]) > Conversion.Val(sItem["FMGS"]))
                //{
                //    sItem["QT_GS"] = "----";
                //    sItem["KTSY"] = "合格数量不能大于总阀门个数";
                //    jyGs = false;
                //}
                //else
                //{
                //    sItem["QT_GS"] = (Conversion.Val(sItem["FMGS"]) - Conversion.Val(sItem["KTSY"])).ToString();
                //}
                //if (!jyGs)
                //{
                //    break;
                //}

                //bool hgKt = true;
                //bool hgMf = true;
                //if (GetSafeDouble(sItem["MF_GS"]) > 0)
                //{
                //    sItem["MFSYPD"] = "不合格";
                //    sign = false;
                //    hgMf = false;
                //}
                //else
                //{
                //    sItem["MFSYPD"] = "合格";
                //}

                //if (GetSafeDouble(sItem["QT_GS"]) > 0)
                //{
                //    sItem["KTSYPD"] = "不合格";
                //    sign = false;
                //    hgKt = false;
                //}
                //else
                //{
                //    sItem["KTSYPD"] = "合格";
                //}
                #endregion

                mgcyl = Math.Round(1.1 * Conversion.Val(sItem["GCYL"]), 2).ToString("0.00") + "MPa";
                sItem["SMFSYYQ"] = "20℃，" + mgcyl + "，持续≥" + mrsYlsj["SMFSY"] + "s，无渗漏。";
                sItem["SMF_YL"] = mgcyl.Replace("MPa", "");
                sItem["SMF_SJ"] = mrsYlsj["SMFSY"];
                sItem["MF_YL"] = mgcyl.Replace("MPa", "");
                sItem["MF_SJ"] = mrsYlsj["MFSY"];
                //mmfxll = Math.Round(Conversion.Val(sItem["FMZJ"]) * Conversion.Val(mrsMfxll["ZJBS"]), 2).ToString("0.00");
                
                if (fmdj == "A级")
                {
                    sItem["MFSYYQ"] = "20℃，" + mgcyl + "，持续≥" + mrsYlsj["MFSY"] + "s，无渗漏。";
                    sItem["MF_YX_SLL"] = "----";

                }
                else
                {
                    mmfxll = Math.Round(Conversion.Val(sItem["GGXH"]) * Conversion.Val(mrsMfxll["ZJBS"]), 2).ToString("0.00");
                    //密封试验要求
                    sItem["MFSYYQ"] = "20℃，" + mgcyl + "，持续≥" + mrsYlsj["MFSY"] + "s，最大允许渗漏量" + mmfxll + "mm&scsup3&scend/s。";
                    sItem["MF_YX_SLL"] = mmfxll;
                }


                mktsy = Math.Round(1.5 * Conversion.Val(sItem["GCYL"]), 2).ToString("0.00") + "MPa";
                sItem["QT_YL"] = mktsy.Replace("MPa", "");
                //壳体试验
                sItem["KTSYYQ"] = "20℃，" + mktsy + "，持续≥" + mrsYlsj["KTSY"] + "s，无渗漏。";
                sItem["QT_SJ"] = mrsYlsj["KTSY"];

                //判定试验时间是否符合标准
                if (GetSafeDouble(MItem[0]["KTSYCXSJ"]) < GetSafeDouble(mrsYlsj["KTSY"]))
                {
                    MItem[0]["KTSYCXSJ"] = "壳体试验时间小于要求时间";
                }
                if (GetSafeDouble(MItem[0]["MFSYCXSJ"]) < GetSafeDouble(mrsYlsj["MFSY"]))
                {
                    MItem[0]["MFSYCXSJ"] = "密封试验时间小于要求时间";
                }

                //判定 下结论
                if ("无渗漏" == MItem[0]["MFSYSCJG"] && "无渗漏" == MItem[0]["KTSYSCJG"])
                {
                    sItem["JCJG"] = "合格";
                    sItem["MFSYPD"] = "合格";
                    sItem["KTSYPD"] = "合格";
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    if ("无渗漏" == MItem[0]["MFSYSCJG"] && "无渗漏" != MItem[0]["KTSYSCJG"])
                    {
                        sItem["MFSYPD"] = "合格";
                        sItem["KTSYPD"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目壳体试验不符合要求。";
                    }
                    else if ("无渗漏" != MItem[0]["MFSYSCJG"] && "无渗漏" == MItem[0]["KTSYSCJG"])
                    {
                        sItem["MFSYPD"] = "不合格";
                        sItem["KTSYPD"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目密封试验不符合要求。";
                    }
                    else
                    {
                        sItem["KTSYPD"] = "不合格";
                        sItem["MFSYPD"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均不符合要求。";
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
            /************************ 代码结束 *********************/
        }
    }
}
