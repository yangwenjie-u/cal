using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class HNT1 : BaseMethods
    {
        //使用标准 GBT 14684-2011 建设用砂
        public void Calc()
        {
            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData = null;

            /************************ 代码开始 *********************/
            var extraDJ = dataExtra["BZ_HNT_DJ"];
            var extraGG = dataExtra["BZ_HNT_GG"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();

            List<double> mtmpList = new List<double>();
            string mcalBh, mlongStr = "";
            List<double> mkyqdArray = new List<double>();
            List<double> mkyhzArray = new List<double>();
            List<double> mtmpArray = new List<double>();

            double mSjcc, mMj, mSjcc1 = 0;
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;
            string mSjdjbh, mSjdj = "";
            double mSz, mQdyq, mHsxs, mttjhsxs = 0;
            string mJSFF = "";
            bool mAllHg = true;
            mttjhsxs = 1;
            string errorMsg = "";
            try
            {
                foreach (var jcxm in jcxmItems)
                {
                    if (jcxm.ToUpper().Contains("BGJG"))
                    {
                        continue;
                    }

                    var SItem = retData[jcxm]["S_HNT"];
                    var XQData = retData[jcxm]["S_BY_RW_XQ"];
                    int index = 0;
                    foreach (var item in SItem)
                    {
                        //XQData[index]["RECID"] = item["RECID"];
                        //XQData[index]["SJWCJSSJ"] = DateTime.Now.ToString();

                        mSjdj = item["SJDJ"];

                        //if (null == MItem)
                        //{
                        //    mAllHg = false;
                        //    XQData[index]["JCJG"] = "不合格";
                        //    XQData[index]["JCJGMS"] = "获取不到主表数据";
                        //    index++;
                        //    continue;
                        //}
                        if (string.IsNullOrEmpty(mSjdj))
                        {
                            item["SJCC"] = "0";
                            item["HSXS"] = "0";
                            //MItem[0]["BGBH"] = "0";
                            //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "规格不祥。";

                            mAllHg = false;
                            XQData[index]["JCJG"] = "不合格";
                            XQData[index]["JCJGMS"] = "获取不到设计等级{SJDJ}";
                            index++;
                            continue;
                        }

                        var extraFieldsGG = extraGG.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(item["GGXH"]));

                        if (null == extraFieldsGG)
                        {
                            item["SJCC"] = "0";
                            item["HSXS"] = "0";
                            //MItem[0]["BGBH"] = "0";
                            //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "规格不祥。";

                            mAllHg = false;
                            XQData[index]["JCJG"] = "不合格";
                            XQData[index]["JCJGMS"] = @"帮助表HNTGG中获取不到" + item["GGXH"] + "数据";
                            index++;
                            continue;
                        }

                        item["SJCC"] = extraFieldsGG["CD"];
                        item["HSXS"] = extraFieldsGG["HSXS"];

                        mSjcc = GetSafeDouble(item["SJCC"]);
                        mSjcc1 = 0;
                        if (item["SJCC1"] == null || item["SJCC1"] == "0")
                            mSjcc1 = mSjcc;
                        else
                            mSjcc1 = GetSafeDouble(item["SJCC1"]);

                        mMj = mSjcc * mSjcc1;
                        if (mMj <= 0)//试件面积<=0则不予计算
                        {
                            //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "单组流水号:" + item["DZBH"] + "试件尺寸为空";
                            //MItem[0]["BGBH"] = "";

                            mAllHg = false;
                            XQData[index]["JCJG"] = "不合格";
                            XQData[index]["JCJGMS"] = "试件面积<=0则不予计算";
                            index++;
                            continue;
                        }

                        mHsxs = GetSafeDouble(item["HSXS"]);
                        if (mHsxs <= 0)//'换算系数=0则不予计算
                        {
                            //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "单组流水号:" + item["DZBH"] + "换算系数为空";
                            //MItem[0]["BGBH"] = "";
                            mAllHg = false;
                            XQData[index]["JCJG"] = "不合格";
                            XQData[index]["JCJGMS"] = "换算系数=0则不予计算";
                            index++;
                            continue;
                        }

                        //从设计等级表中取得相应的计算数值、等级标准
                        var extraFieldsDJ = extraDJ.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(mSjdj));

                        if (null == extraFieldsDJ)
                        {
                            mSz = 0;
                            mQdyq = 0;
                            mJSFF = "";
                            item["JCJG"] = "依据不详";
                            //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "单组流水号:" + item["DZBH"] + "设计等级为空或不存在";
                            //MItem[0]["BGBH"] = "";

                            mAllHg = false;
                            XQData[index]["JCJG"] = "不合格";
                            //XQData[index]["JCJGMS"] = "单组流水号: " + item["DZBH"] + "设计等级为空或不存在";
                            index++;
                            continue;
                        }

                        mSz = string.IsNullOrEmpty(extraFieldsDJ["SZ"]) ? 0 : GetSafeInt(extraFieldsDJ["SZ"]);
                        mQdyq = string.IsNullOrEmpty(extraFieldsDJ["QDYQ"]) ? 0 : GetSafeInt(extraFieldsDJ["QDYQ"]);
                        mJSFF = string.IsNullOrEmpty(extraFieldsDJ["JSFF"]) ? "" : extraFieldsDJ["QDYQ"].Trim().ToLower();

                        //string SJWCKSSJ = item["SYRQ"];
                        string SJWCKSSJ = XQData[index]["SJWCJSSJ"]; //数据来源暂时不定

                        //"龄期"://计算龄期
                        if (GetSafeDate(item["ZZRQ"]) <= Date19000101 || GetSafeDate(SJWCKSSJ) <= Date19000101)
                        {
                            item["LQ"] = "0";
                        }
                        else
                        {
                            item["LQ"] = (GetSafeDate(item["ZZRQ"]) - GetSafeDate(SJWCKSSJ)).Days.ToString();
                        }
                        // "抗压强度": //抗压强度

                        double qdVal = 0;
                        for (int i = 1; i < 4; i++)
                        {
                            qdVal = Math.Round(1000 * GetSafeDouble(item["KYHZ" + i]) / mMj, 1);
                            //item["KYQD_" + i] = qdVal.ToString();
                            //mlongStr += item["KYQD_" + i].ToString() + ",";
                            //新版
                            item["KYQD" + i] = qdVal.ToString();
                            mlongStr += item["KYQD" + i].ToString() + ",";
                            mtmpArray.Add(qdVal);
                        }
                        mtmpArray.Sort();
                        mMaxKyqd = mtmpArray[2];
                        mMinKyqd = mtmpArray[0];
                        mMidKyqd = mtmpArray[1];
                        mAvgKyqd = mtmpArray.Average();

                        if (mMidKyqd == 0)
                        {
                            mAllHg = false;
                        }
                        else
                        {
                            if (Math.Round((mMaxKyqd - mMinKyqd) / mMinKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                            {
                                item["KYPJ"] = "不作评定";
                                item["DDSJQD"] = "不作评定";
                                item["HZCASE"] = "1";
                                //item["JCJG"] = "不合格";
                                //MItem[0]["JSBEIZHU"] = "最大最小强度值超出中间值的15%,试验结果不作评定依据";
                            }
                            if (Math.Round((mMaxKyqd - mMinKyqd) / mMinKyqd, 2) > 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                            {
                                item["hzcase"] = "2";
                                //MItem[0]["JSBEIZHU"] = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";

                                if (mSjcc != 0 && mSjcc1 != 0)
                                {
                                    item["KYPJ"] = (mMidKyqd * mHsxs * mttjhsxs).ToString("0");
                                }

                                if (mSz != 0)
                                {
                                    item["DDSJQD"] = (GetSafeDouble(item["KYPJ"]) / mSz * 100).ToString("0");
                                    if (GetSafeDouble(item["DDSJQD"]) > mQdyq)
                                    {
                                        //item["JCJG"] = "合格";
                                    }
                                    else
                                    {
                                        //item["JCJG"] = "不合格";
                                        mAllHg = false;
                                    }
                                }

                                item["MIDAVG"] = "1";
                            }
                            if (Math.Round((mMaxKyqd - mMinKyqd) / mMinKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) > 0.15)
                            {
                                item["hzcase"] = "3";
                                //MItem[0]["JSBEIZHU"] = "最大最小强度值均未超出中间值的15%,试验结果取平均值";

                                if (mSjcc != 0 && mSjcc1 != 0)
                                {
                                    item["KYPJ"] = (mMidKyqd * mHsxs * mttjhsxs).ToString("0");
                                }

                                if (mSz != 0)
                                {
                                    item["DDSJQD"] = (GetSafeDouble(item["KYPJ"]) / mSz * 100).ToString("0");
                                    if (GetSafeDouble(item["DDSJQD"]) > mQdyq)
                                    {
                                        //item["JCJG"] = "合格";
                                    }
                                    else
                                    {
                                        //item["JCJG"] = "不合格";
                                        mAllHg = false;
                                    }
                                }

                                item["MIDAVG"] = "1";
                            }
                            if (Math.Round((mMaxKyqd - mMinKyqd) / mMinKyqd, 2) <= 0.15 && Math.Round((mMidKyqd - mMinKyqd) / mMidKyqd, 2) <= 0.15)
                            {
                                item["hzcase"] = "4";
                                //MItem[0]["JSBEIZHU"] = "最大最小强度值均未超出中间值的15%,试验结果取平均值";

                                if (mSjcc != 0 && mSjcc1 != 0)
                                {
                                    item["KYPJ"] = (mMidKyqd * mHsxs * mttjhsxs).ToString("0");
                                }

                                if (mSz != 0)
                                {
                                    item["DDSJQD"] = (GetSafeDouble(item["KYPJ"]) / mSz * 100).ToString("0");
                                    if (GetSafeDouble(item["DDSJQD"]) > mQdyq)
                                    {
                                        //item["JCJG"] = "合格";
                                    }
                                    else
                                    {
                                        //item["JCJG"] = "不合格";
                                        mAllHg = false;
                                    }
                                }

                                item["MIDAVG"] = "1";
                            }
                        }

                        if (item["YHTJ"] == "标准" && GetSafeDouble(item["LQ"]) > 30)
                        {
                            item["KYPJ"] = "不作评定";
                            item["DDSJQD"] = "不作评定";
                            //MItem[0]["JSBEIZHU"] = "龄期超范围,试验结果不作评定依据";
                        }

                        if (mAllHg)
                        {
                            XQData[index]["JCJG"] = "合格";
                            XQData[index]["JCJGMS"] = "该组试样所检项目符合" + "标准要求。";
                            //MItem[0]["JSBEIZHU"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                            //MItem[0]["JCJG"] = "合格";
                        }
                        else
                        {
                            XQData[index]["JCJG"] = "不合格";
                            XQData[index]["JCJGMS"] = "该组试样不符合" + "标准要求。";
                            //MItem[0]["JSBEIZHU"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            //MItem[0]["JCJG"] = "不合格";
                        }

                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg += ex.Message + ex.StackTrace;
            }
            #region 添加最终报告


            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            if (!mAllHg)
            {
                //不合格
                bgjgDic.Add("JCJG", "不合格");
                bgjgDic.Add("JCJGMS", string.IsNullOrEmpty(errorMsg) ? $"该组试样所检项目符合不标准要求。" : errorMsg);
            }
            else
            {
                bgjgDic.Add("JCJG", "合格");
                bgjgDic.Add("JCJGMS", string.IsNullOrEmpty(errorMsg) ? $"该组试样所检项目符合标准要求。" : errorMsg);
            }

            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion

            /************************ 代码结束 *********************/

        }
    }
}

//public void Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra,
//    ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
//{
//    /************************ 代码开始 *********************/
//    var Len = GetDataCount("抗压.S_HNT");
//    bool mAllHg = false;
//    var mjcjg = "不合格";
//    var jsbeizhu = "";
//    var jgsm = "";
//    for (int i = 0; i < Len; i++)
//    {
//        if (i == 0)
//        {
//            mAllHg = true;
//        }
//        var Index = i;

//        var mSjdj = GetSafeDate(GetData("抗压.M_HNT.sjdj"));


//        var ZZRQ = GetSafeDate(GetData("抗压.S_HNT.ZZRQ", Index));
//        var MSYRQ = GetSafeDate(GetData("抗压.M_HNT.SYRQ"));
//        var yhtj = GetData("抗压.S_HNT.YHTJ", Index);

//        var LQ = 0;
//        if (ZZRQ <= Date19000101 || MSYRQ <= Date19000101)
//        {
//            LQ = 0;
//        }
//        else
//        {
//            LQ = (MSYRQ - ZZRQ).Days;
//            if (LQ != 28 && yhtj.Trim() == "标准")
//            {
//                LQ = 28;
//            }
//        }

//        var a = GetData("抗压.S_HNT.KYHZ1", Index);
//        CheckEmpty(a, "抗压荷重1 不能为空");
//        var b = GetData("抗压.S_HNT.KYHZ2", Index);
//        CheckEmpty(b, "抗压荷重2 不能为空");
//        var c = GetData("抗压.S_HNT.KYHZ3", Index);
//        CheckEmpty(c, "抗压荷重3 不能为空");
//        var d = GetData("抗压.S_HNT.SJCC", Index);
//        CheckEmpty(d, "受压面边长1 不能为空");
//        var SJDJ = GetData("抗压.S_HNT.SJDJ", Index);
//        CheckEmpty(SJDJ, "设计等级 不能为空");
//        var e = GetDouble(d);

//        var HSXS = 1.0;
//        if (d == "100")
//        {
//            HSXS = 0.95;
//        }
//        else if (d == "200")
//        {
//            HSXS = 1.05;
//        }

//        var mttjhsxs = 1.0;

//        var KYQD1 = Round(GetDouble(a) * 1000 * HSXS * mttjhsxs / (e * e), 1);
//        var KYQD2 = Round(GetDouble(b) * 1000 * HSXS * mttjhsxs / (e * e), 1);
//        var KYQD3 = Round(GetDouble(c) * 1000 * HSXS * mttjhsxs / (e * e), 1);

//        var MinKYQD = GetMin(KYQD1, KYQD2, KYQD3);
//        var MiddleKYQD = GetMiddle(KYQD1, KYQD2, KYQD3);
//        var MaxKYQD = GetMax(KYQD1, KYQD2, KYQD3);

//        var KYPJ = "";
//        var ddsjqd = "";
//        var hzcase = 1;
//        var jcjg = "";

//        var midavg = false;

//        var mSz = 0.0;
//        var mQdyq = 0.0;
//        if (!String.IsNullOrEmpty(SJDJ))
//        {
//            if (SJDJ.ToUpper()[0] != 'C')
//            {
//                SJDJ = "C" + SJDJ;
//            }
//            var sz = GetExtraData("BZ_HNT_DJ.SZ", x => x["MC"].Trim().Equals(SJDJ, StringComparison.OrdinalIgnoreCase)).Trim();
//            var qdyq = GetExtraData("BZ_HNT_DJ.QDYQ", x => x["MC"].Trim().Equals(SJDJ, StringComparison.OrdinalIgnoreCase)).Trim();
//            if (!String.IsNullOrEmpty(sz))
//                mSz = GetDouble(sz);
//            if (!String.IsNullOrEmpty(qdyq))
//                mQdyq = GetDouble(qdyq);
//        }

//        var BaiFenBi1 = ((MiddleKYQD - MinKYQD) / MiddleKYQD) * 100;
//        var BaiFenBi2 = ((MaxKYQD - MiddleKYQD) / MiddleKYQD) * 100;

//        if ((BaiFenBi2 > 15 && BaiFenBi1 <= 15))
//        {
//            hzcase = 2;
//            jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
//            KYPJ = RoundEx(MiddleKYQD, 1);
//            if (mSz > 0)
//            {
//                ddsjqd = Round(GetDouble(KYPJ) / mSz * 100, 0).ToString();
//                if (mQdyq > 0)
//                {
//                    if (GetDouble(ddsjqd) > mQdyq)
//                    {
//                        jcjg = "合格";
//                    }
//                    else
//                    {
//                        jcjg = "不合格";
//                        mAllHg = false;
//                    }
//                }
//                else
//                {
//                    jcjg = "----";
//                    mjcjg = "----";
//                    mAllHg = false;
//                }
//            }
//            else
//            {
//                jcjg = "----";
//                mjcjg = "----";
//                ddsjqd = "----";
//                mAllHg = false;
//            }
//            midavg = true;
//        }
//        else if (BaiFenBi1 > 15 && BaiFenBi2 <= 15)
//        {
//            hzcase = 3;
//            jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
//            KYPJ = RoundEx(MiddleKYQD, 1);
//            if (mSz > 0)
//            {
//                ddsjqd = Round(GetDouble(KYPJ) / mSz * 100, 0).ToString();
//                if (mQdyq > 0)
//                {
//                    if (GetDouble(ddsjqd) > mQdyq)
//                    {
//                        jcjg = "合格";
//                    }
//                    else
//                    {
//                        jcjg = "不合格";
//                        mAllHg = false;
//                    }
//                }
//                else
//                {
//                    jcjg = "----";
//                    mjcjg = "----";
//                    mAllHg = false;
//                }
//            }
//            else
//            {
//                jcjg = "----";
//                mjcjg = "----";
//                ddsjqd = "----";
//                mAllHg = false;
//            }
//            midavg = true;
//        }
//        else if (BaiFenBi1 > 15 && BaiFenBi2 > 15)
//        {
//            KYPJ = "试验结果无效";
//            ddsjqd = "不作评定";
//            hzcase = 1;
//            jcjg = "不合格";
//            jsbeizhu = "最大最小强度值超出中间值的15%,试验结果不作评定依据";
//            mAllHg = false;
//        }
//        else
//        {
//            KYPJ = (RoundEx((KYQD1 + KYQD2 + KYQD3) / 3, 1)).ToString();
//            hzcase = 4;
//            if (mSz > 0)
//            {
//                ddsjqd = Round(GetDouble(KYPJ) / mSz * 100, 0).ToString();
//                if (mQdyq > 0)
//                {
//                    if (GetDouble(ddsjqd) > mQdyq)
//                    {
//                        jcjg = "合格";
//                    }
//                    else
//                    {
//                        jcjg = "不合格";
//                        mAllHg = false;
//                    }
//                }
//                else
//                {
//                    jcjg = "----";
//                    mjcjg = "----";
//                    mAllHg = false;
//                }
//            }
//            else
//            {
//                jcjg = "----";
//                mjcjg = "----";
//                ddsjqd = "----";
//                mAllHg = false;
//            }
//        }

//        if (KYPJ == "试验结果无效")
//        {
//            jgsm = "该组试样强度代表值无效。";
//        }
//        else
//        {
//            jgsm = "该组试样强度代表值" + KYPJ + "MPa，" + "达到设计强度" + ddsjqd + "%。";
//        }

//        SetData("抗压.S_HNT.KYQD1", RoundEx(KYQD1, 1), Index);
//        SetData("抗压.S_HNT.KYQD2", RoundEx(KYQD2, 1), Index);
//        SetData("抗压.S_HNT.KYQD3", RoundEx(KYQD3, 1), Index);
//        SetData("抗压.S_HNT.KYPJ", KYPJ, Index);
//        SetData("抗压.S_HNT.HSXS", HSXS.ToString(), Index);
//        SetData("抗压.S_HNT.TTJHSXS", mttjhsxs.ToString(), Index);
//        SetData("抗压.S_HNT.DDSJQD", ddsjqd, Index);
//        SetData("抗压.S_HNT.LQ", LQ.ToString(), Index);
//        SetData("抗压.S_BY_RW_XQ.JCJG", jcjg, Index);
//    }
//    if (mAllHg && mjcjg != "----")
//    {
//        mjcjg = "合格";
//    }

//    //"BGJG":{ "M_BY_BG":[{"JCJG":"不合格","JCJGMS":""}]}
//    SetData("BGJG.BGJG.JCJG", mjcjg);
//    SetData("BGJG.BGJG.JCJGMS", jsbeizhu);

//    //GetData("抗压.S_HNT.YHTJ", Index);
//}
