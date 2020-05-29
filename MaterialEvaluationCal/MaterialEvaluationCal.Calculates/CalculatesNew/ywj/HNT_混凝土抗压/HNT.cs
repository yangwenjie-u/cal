using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class HNT : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 
            //var extraDJ = dataExtra["BZ_HNT_DJ"];
            //var data = retData;
            //bool mAllHg = false;
            //var mjcjg = "不合格";
            //var jsbeizhu = "";
            //var jgsm = "";
            //var S_HNTS = data["S_HNT"];

            //var len = S_HNTS.Count;
            //for (int i = 0; i < len; i++)
            //{
            //    if (i == 0)
            //    {
            //        mAllHg = true;
            //    }
            //    var S_HNT = S_HNTS.ElementAt(i);
            //    var Index = i;
            //    var InitDate = DateTime.Parse("1900-1-1");
            //    var ZZRQ = InitDate;
            //    var SYRQ = InitDate;
            //    DateTime.TryParse(S_HNT["ZZRQ"], out ZZRQ);
            //    DateTime.TryParse(S_HNT["SYRQ"], out SYRQ);
            //    var YHTJ = S_HNT["YHTJ"].Trim();
            //    var LQ = 0;
            //    if (ZZRQ <= InitDate || SYRQ <= InitDate)
            //    {
            //        LQ = 0;
            //    }
            //    else
            //    {
            //        LQ = (SYRQ - ZZRQ).Days;
            //        if (LQ != 28 && YHTJ.Equals("标准"))
            //        {
            //            LQ = 28;
            //        }
            //    }
            //    var KYHZ1 = GetSafeDouble(S_HNT["KYHZ1"]);
            //    var KYHZ2 = GetSafeDouble(S_HNT["KYHZ2"]);
            //    var KYHZ3 = GetSafeDouble(S_HNT["KYHZ3"]);
            //    var SJCC = S_HNT["SJCC"];
            //    var SJDJ = S_HNT["SJDJ"];

            //    var SJCCDouble = GetSafeDouble(SJCC);
            //    var SJCCMJ = SJCCDouble * SJCCDouble;

            //    var HSXS = 1.0;
            //    if (SJCC == "100")
            //    {
            //        HSXS = 0.95;
            //    }
            //    else if (SJCC == "200")
            //    {
            //        HSXS = 1.05;
            //    }

            //    var mttjhsxs = 1.0;

            //    var KYQD1 = Math.Round(KYHZ1 * 1000 * HSXS * mttjhsxs / SJCCMJ, 1);
            //    var KYQD2 = Math.Round(KYHZ2 * 1000 * HSXS * mttjhsxs / SJCCMJ, 1);
            //    var KYQD3 = Math.Round(KYHZ3 * 1000 * HSXS * mttjhsxs / SJCCMJ, 1);

            //    List<double> KYQDS = new List<double>();
            //    KYQDS.Add(KYQD1);
            //    KYQDS.Add(KYQD2);
            //    KYQDS.Add(KYQD3);
            //    KYQDS.Sort();
            //    var MinKYQD = KYQDS[0];
            //    var MiddleKYQD = KYQDS[1];
            //    var MaxKYQD = KYQDS[2];

            //    var KYPJ = "";
            //    var ddsjqd = "";
            //    var hzcase = 1;
            //    var jcjg = "";

            //    var midavg = false;

            //    var mSz = 0.0;
            //    var mQdyq = 0.0;
            //    if (!String.IsNullOrEmpty(SJDJ))
            //    {
            //        if (SJDJ.ToUpper()[0] != 'C')
            //        {
            //            SJDJ = "C" + SJDJ;
            //        }
            //        var DJ = extraDJ.FirstOrDefault(x => x["mc"].Trim().Equals(SJDJ, StringComparison.OrdinalIgnoreCase));
            //        if (DJ != default)
            //        {
            //            var sz = DJ["SZ"].Trim();
            //            var qdyq = DJ["QDYQ"].Trim();
            //            mSz = GetSafeDouble(sz);
            //            mQdyq = GetSafeDouble(qdyq);
            //        }
            //    }

            //    var BaiFenBi1 = ((MiddleKYQD - MinKYQD) / MiddleKYQD) * 100;
            //    var BaiFenBi2 = ((MaxKYQD - MiddleKYQD) / MiddleKYQD) * 100;

            //    if ((BaiFenBi2 > 15 && BaiFenBi1 <= 15))
            //    {
            //        hzcase = 2;
            //        jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
            //        KYPJ = MiddleKYQD.ToString("0.0");
            //        if (mSz > 0)
            //        {
            //            ddsjqd = Math.Round(GetSafeDouble(KYPJ) / mSz * 100, 0).ToString();
            //            if (mQdyq > 0)
            //            {
            //                if (GetSafeDouble(ddsjqd) > mQdyq)
            //                {
            //                    jcjg = "合格";
            //                }
            //                else
            //                {
            //                    jcjg = "不合格";
            //                    mAllHg = false;
            //                }
            //            }
            //            else
            //            {
            //                jcjg = "----";
            //                mjcjg = "----";
            //                mAllHg = false;
            //            }
            //        }
            //        else
            //        {
            //            jcjg = "----";
            //            mjcjg = "----";
            //            ddsjqd = "----";
            //            mAllHg = false;
            //        }
            //        midavg = true;
            //    }
            //    else if (BaiFenBi1 > 15 && BaiFenBi2 <= 15)
            //    {
            //        hzcase = 3;
            //        jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
            //        KYPJ = MiddleKYQD.ToString("0.0");
            //        if (mSz > 0)
            //        {
            //            ddsjqd = Math.Round(GetSafeDouble(KYPJ) / mSz * 100, 0).ToString();
            //            if (mQdyq > 0)
            //            {
            //                if (GetSafeDouble(ddsjqd) > mQdyq)
            //                {
            //                    jcjg = "合格";
            //                }
            //                else
            //                {
            //                    jcjg = "不合格";
            //                    mAllHg = false;
            //                }
            //            }
            //            else
            //            {
            //                jcjg = "----";
            //                mjcjg = "----";
            //                mAllHg = false;
            //            }
            //        }
            //        else
            //        {
            //            jcjg = "----";
            //            mjcjg = "----";
            //            ddsjqd = "----";
            //            mAllHg = false;
            //        }
            //        midavg = true;
            //    }
            //    else if (BaiFenBi1 > 15 && BaiFenBi2 > 15)
            //    {
            //        KYPJ = "试验结果无效";
            //        ddsjqd = "不作评定";
            //        hzcase = 1;
            //        jcjg = "不合格";
            //        jsbeizhu = "最大最小强度值超出中间值的15%,试验结果不作评定依据";
            //        mAllHg = false;
            //    }
            //    else
            //    {
            //        KYPJ = ((KYQD1 + KYQD2 + KYQD3) / 3).ToString("0.0");
            //        hzcase = 4;
            //        if (mSz > 0)
            //        {
            //            ddsjqd = Math.Round(GetSafeDouble(KYPJ) / mSz * 100, 0).ToString();
            //            if (mQdyq > 0)
            //            {
            //                if (GetSafeDouble(ddsjqd) > mQdyq)
            //                {
            //                    jcjg = "合格";
            //                }
            //                else
            //                {
            //                    jcjg = "不合格";
            //                    mAllHg = false;
            //                }
            //            }
            //            else
            //            {
            //                jcjg = "----";
            //                mjcjg = "----";
            //                mAllHg = false;
            //            }
            //        }
            //        else
            //        {
            //            jcjg = "----";
            //            mjcjg = "----";
            //            ddsjqd = "----";
            //            mAllHg = false;
            //        }
            //    }

            //    if (KYPJ == "试验结果无效")
            //    {
            //        jgsm = "该组试样强度代表值无效。";
            //    }
            //    else
            //    {
            //        jgsm = "该组试样强度代表值" + KYPJ + "MPa，" + "达到设计强度" + ddsjqd + "%。";
            //    }

            //    S_HNT["KYQD1"] = KYQD1.ToString("0.0");
            //    S_HNT["KYQD2"] = KYQD2.ToString("0.0");
            //    S_HNT["KYQD3"] = KYQD3.ToString("0.0");
            //    S_HNT["KYPJ"] = KYPJ;
            //    S_HNT["HSXS"] = HSXS.ToString();
            //    S_HNT["TTJHSXS"] = mttjhsxs.ToString();
            //    S_HNT["DDSJQD"] = ddsjqd;
            //    S_HNT["LQ"] = LQ.ToString();
            //    S_HNT["JCJG"] = jcjg;

            //}

            //if (mAllHg && mjcjg != "----")
            //{
            //    mjcjg = "合格";
            //}
            //if (!data.ContainsKey("M_HNT"))
            //{
            //    data["M_HNT"] = new List<IDictionary<string, string>>();
            //}
            //var M_HNT = data["M_HNT"];
            //if (M_HNT == default || M_HNT.Count == 0)
            //{
            //    IDictionary<string, string> m = new Dictionary<string, string>();
            //    m["JCJG"] = mjcjg;
            //    m["JCJGMS"] = jsbeizhu;
            //    M_HNT.Add(m);
            //}
            //else
            //{
            //    M_HNT[0]["JCJG"] = mjcjg;
            //    M_HNT[0]["JCJGMS"] = jsbeizhu;
            //}
            #endregion

            #region MyRegion
            var extraDJ = dataExtra["BZ_HNT_DJ"];
            var extraGG = dataExtra["BZ_HNTGG"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            var S_HNTS = data["S_HNT"];
            var mAllHg = true;
            var MItem = data["M_HNT"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            foreach (var sItem in S_HNTS)
            {
                var mrsgg = extraGG.FirstOrDefault(x => x["MC"].Trim().Equals(sItem["GGXH"], StringComparison.OrdinalIgnoreCase));
                if (mrsgg != null)
                {
                    sItem["SJCC"] = GetSafeDouble(mrsgg["CD"]).ToString("0");
                    sItem["HSXS"] = mrsgg["HSXS"];
                }
                else
                {
                    mjcjg = "不下结论";
                    sItem["SJCC"] = "0";
                    sItem["HSXS"] = "0";
                    jsbeizhu = "规格不祥。";
                    sItem["JCJG"] = "不下结论";
                    mAllHg = false;
                    continue;
                }

                var InitDate = DateTime.Parse("1900-1-1");
                var ZZRQ = InitDate;
                var SYRQ = InitDate;
                DateTime.TryParse(sItem["ZZRQ"], out ZZRQ);
                DateTime.TryParse(sItem["SYRQ"], out SYRQ);
                var YHTJ = sItem["YHTJ"].Trim();

                var LQ = 0;
                if (ZZRQ <= InitDate || SYRQ <= InitDate)
                {
                    LQ = 0;
                }
                else
                {
                    LQ = (SYRQ - ZZRQ).Days;
                    if (LQ != 28 && YHTJ.Equals("标准"))
                    {
                        LQ = 28;
                    }
                }

                var KYHZ1 = GetSafeDouble(sItem["KYHZ1"]);
                var KYHZ2 = GetSafeDouble(sItem["KYHZ2"]);
                var KYHZ3 = GetSafeDouble(sItem["KYHZ3"]);
                var SJCC = sItem["SJCC"];
                var SJDJ = sItem["SJDJ"];

                var SJCCDouble = GetSafeDouble(SJCC);
                var SJCCMJ = SJCCDouble * SJCCDouble;

                var HSXS = 1.0;
                if (SJCC == "100")
                {
                    HSXS = 0.95;
                }
                else if (SJCC == "200")
                {
                    HSXS = 1.05;
                }
                //var mttjhsxs = 1.0;
                //if (IsNumeric(sItem["TTJHSXS"]) && GetSafeDouble(sItem["TTJHSXS"]) != 0)
                //{
                //    mttjhsxs = GetSafeDouble(sItem["TTJHSXS"]);
                //}
                //else
                //{
                //    sItem["TTJHSXS"] = "----";
                //}

                var KYQD1 = Math.Round(KYHZ1 * 1000 / SJCCMJ * HSXS, 1);
                var KYQD2 = Math.Round(KYHZ2 * 1000 / SJCCMJ * HSXS, 1);
                var KYQD3 = Math.Round(KYHZ3 * 1000 / SJCCMJ * HSXS, 1);

                List<double> KYQDS = new List<double>();
                KYQDS.Add(KYQD1);
                KYQDS.Add(KYQD2);
                KYQDS.Add(KYQD3);
                KYQDS.Sort();
                var MinKYQD = KYQDS[0];
                var MiddleKYQD = KYQDS[1];
                var MaxKYQD = KYQDS[2];

                var KYPJ = "";
                var ddsjqd = "";
                var hzcase = 1;

                var midavg = false;

                var mSz = 0.0;
                var mQdyq = 0.0;
                if (!String.IsNullOrEmpty(SJDJ))
                {
                    if (SJDJ.ToUpper()[0] != 'C')
                    {
                        SJDJ = "C" + SJDJ;
                    }
                    var DJ = extraDJ.FirstOrDefault(x => x["MC"].Trim().Equals(SJDJ, StringComparison.OrdinalIgnoreCase));
                    if (DJ != null)
                    {
                        var sz = DJ["SZ"].Trim();
                        var qdyq = DJ["QDYQ"].Trim();
                        mSz = GetSafeDouble(sz);
                        mQdyq = GetSafeDouble(qdyq);
                    }
                    else
                    {
                        mjcjg = "不下结论";
                        mSz = 0;
                        mQdyq = 0;
                        jsbeizhu = "设计等级为空或不存在。";
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                        continue;
                    }
                }

                var BaiFenBi1 = ((MiddleKYQD - MinKYQD) / MiddleKYQD) * 100;
                var BaiFenBi2 = ((MaxKYQD - MiddleKYQD) / MiddleKYQD) * 100;

                if (MItem[0]["PDBZ"].Contains("2019"))
                {
                    if ((BaiFenBi2 > 15 || BaiFenBi1 > 15))
                    {
                        hzcase = 2;
                        jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                        KYPJ = MiddleKYQD.ToString("0.0");
                        if (mSz > 0)
                        {
                            ddsjqd = Math.Round(GetSafeDouble(KYPJ) / mSz * 100, 0).ToString();
                            if (mQdyq > 0)
                            {
                                if (GetSafeDouble(ddsjqd) > mQdyq)
                                {
                                    jcjg = "合格";
                                }
                                else
                                {
                                    jcjg = "不合格";
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                jcjg = "----";
                                mjcjg = "----";
                            }
                        }
                        else
                        {
                            jcjg = "----";
                            mjcjg = "----";
                            ddsjqd = "----";
                        }
                        midavg = true;
                    }
                    else if (BaiFenBi1 > 15 && BaiFenBi2 > 15)
                    {
                        mjcjg = "不下结论";
                        KYPJ = "试验结果无效";
                        ddsjqd = "不作评定";
                        hzcase = 1;
                        jcjg = "不下结论";
                        jsbeizhu = "最大最小强度值超出中间值的15%,试验结果不作评定依据";
                        mAllHg = false;
                    }
                    else
                    {
                        KYPJ = Math.Round((KYQD1 + KYQD2 + KYQD3) / 3, 1).ToString("0.0");
                        hzcase = 4;
                        if (mSz > 0)
                        {
                            ddsjqd = Math.Round(GetSafeDouble(KYPJ) / mSz * 100, 0).ToString();
                            if (mQdyq > 0)
                            {
                                if (GetSafeDouble(ddsjqd) > mQdyq)
                                {
                                    jcjg = "合格";
                                }
                                else
                                {
                                    jcjg = "不合格";
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                jcjg = "----";
                                mjcjg = "----";
                            }
                        }
                        else
                        {
                            jcjg = "----";
                            mjcjg = "----";
                            ddsjqd = "----";
                        }
                    }
                }
                else
                {
                    if ((BaiFenBi2 > 15 && BaiFenBi1 <= 15))
                    {
                        hzcase = 2;
                        jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                        KYPJ = MiddleKYQD.ToString("0.0");
                        if (mSz > 0)
                        {
                            ddsjqd = Math.Round(GetSafeDouble(KYPJ) / mSz * 100, 0).ToString();
                            if (mQdyq > 0)
                            {
                                if (GetSafeDouble(ddsjqd) > mQdyq)
                                {
                                    jcjg = "合格";
                                }
                                else
                                {
                                    jcjg = "不合格";
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                jcjg = "----";
                                mjcjg = "----";
                            }
                        }
                        else
                        {
                            jcjg = "----";
                            mjcjg = "----";
                            ddsjqd = "----";
                        }
                        midavg = true;
                    }
                    else if (BaiFenBi1 > 15 && BaiFenBi2 <= 15)
                    {
                        hzcase = 3;
                        jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                        KYPJ = MiddleKYQD.ToString("0.0");
                        if (mSz > 0)
                        {
                            ddsjqd = Math.Round(GetSafeDouble(KYPJ) / mSz * 100, 0).ToString();
                            if (mQdyq > 0)
                            {
                                if (GetSafeDouble(ddsjqd) > mQdyq)
                                {
                                    jcjg = "合格";
                                }
                                else
                                {
                                    jcjg = "不合格";
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                jcjg = "----";
                                mjcjg = "----";
                            }
                        }
                        else
                        {
                            jcjg = "----";
                            mjcjg = "----";
                            ddsjqd = "----";
                        }
                        midavg = true;
                    }
                    else if (BaiFenBi1 > 15 && BaiFenBi2 > 15)
                    {
                        mjcjg = "不下结论";
                        KYPJ = "试验结果无效";
                        ddsjqd = "不作评定";
                        hzcase = 1;
                        jcjg = "不下结论";
                        jsbeizhu = "最大最小强度值超出中间值的15%,试验结果不作评定依据";
                        mAllHg = false;
                    }
                    else
                    {
                        KYPJ = Math.Round((KYQD1 + KYQD2 + KYQD3) / 3, 1).ToString("0.0");
                        hzcase = 4;
                        if (mSz > 0)
                        {
                            ddsjqd = Math.Round(GetSafeDouble(KYPJ) / mSz * 100, 0).ToString();
                            if (mQdyq > 0)
                            {
                                if (GetSafeDouble(ddsjqd) > mQdyq)
                                {
                                    jcjg = "合格";
                                }
                                else
                                {
                                    jcjg = "不合格";
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                jcjg = "----";
                                mjcjg = "----";
                            }
                        }
                        else
                        {
                            jcjg = "----";
                            mjcjg = "----";
                            ddsjqd = "----";
                        }
                    }
                }

                sItem["KYQD1"] = Round(KYQD1, 1).ToString("0.0");
                sItem["KYQD2"] = Round(KYQD2, 1).ToString("0.0");
                sItem["KYQD3"] = Round(KYQD3, 1).ToString("0.0");
                //同条件换算 代表值除以 0.88
                if (sItem["YHTJ"] == "同条件养护(600℃ · d)")
                {

                    sItem["KYPJ"] = Round(GetSafeDouble(KYPJ) / 0.88, 1).ToString("0.0");
                    sItem["TTJHSXS"] = "0.88";

                }
                else
                {
                    sItem["KYPJ"] = KYPJ;
                }

                sItem["DDSJQD"] = Round(GetSafeDouble(sItem["KYPJ"]) / mSz * 100, 0).ToString("0");
                sItem["HSXS"] = HSXS.ToString();
                sItem["LQ"] = LQ.ToString();
               
                if (100 > GetSafeDouble(sItem["DDSJQD"]))
                {
                    jcjg = "不合格";
                    mAllHg = false;
                }

                sItem["JCJG"] = jcjg;
                if (KYPJ == "试验结果无效")
                {
                    mjcjg = "不下结论";
                    sItem["JCJG"] = "不下结论";
                    jgsm = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样强度代表值无效。";
                }
                else
                {
                    jgsm = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样强度代表值" + sItem["KYPJ"] + "MPa，" + "达到设计强度" + sItem["DDSJQD"] + "%。";
                }

            }

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_HNT"))
            {

                data["M_HNT"] = new List<IDictionary<string, string>>();
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jgsm;

            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
