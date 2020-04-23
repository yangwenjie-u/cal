using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SJX : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_SJX_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_SJXS = data["S_SJX"];
            if (!data.ContainsKey("M_SJX"))
            {
                data["M_SJX"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_SJX"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            List<double> mkyqdArray = new List<double>();
            List<string> mtmpArray = new List<string>();
            string mSjdj, mJSFF, mlongStr = "";
            double mSjcc, mSjcc1, mMj = 0;
            double mSz, mQdyq = 0;
            Double mMaxKyqd, mMinKyqd, mAvgKyqd, mMidKyqd = 0;
            foreach (var sItem in S_SJXS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                mSjdj = sItem["SJDJ"];//设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                {
                    mSjdj = "";
                }
                if ("70.7×70.7×70.7" == sItem["GGXH"])
                {
                    sItem["SJCC"] = "70.7";
                }
                mMj = 70.7 * 70.7;
                mSjcc = 70.7;
                mSjcc1 = 70.7;

                if (mMj <= 0)//试件面积<=0则不予计算
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "不合格";
                    continue;
                }

                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"].Trim() == mSjdj.Trim());
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "不合格";
                    continue;
                }
                else
                {
                    mSz = string.IsNullOrEmpty(extraFieldsDj["SZ"]) ? 0 : GetSafeDouble(extraFieldsDj["SZ"]);
                    mQdyq = string.IsNullOrEmpty(extraFieldsDj["QDYQ"]) ? 0 : GetSafeDouble(extraFieldsDj["QDYQ"]);
                    mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["QDYQ"].ToLower();
                }
                //if (0 != sItem["SYHJWD"].Length)
                //{
                //    MItem[0]["SYWD"] = sItem["SYHJWD"] + "℃";
                //}
                //计算龄期
                sItem["LQ"] = (DateTime.Parse(sItem["SYRQ"]) - DateTime.Parse(sItem["ZZRQ"])).Days.ToString();

                if ("" == mJSFF)
                {
                    #region 抗压
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        //FormatNumber(Round(CDec(sitem["drxs), mcd), mcd, vbTrue, , vbFalse)
                        //c#可以用    Math.Round(GetSafeDouble(sItem["DRXS"]), mcd).ToString();
                        sItem["KYQD1"] = Math.Round(1350 * GetSafeDouble(sItem["KYHZ1"].Trim()) / mMj, 1).ToString("0.0");
                        sItem["KYQD2"] = Math.Round(1350 * GetSafeDouble(sItem["KYHZ2"].Trim()) / mMj, 1).ToString("0.0");
                        sItem["KYQD3"] = Math.Round(1350 * GetSafeDouble(sItem["KYHZ3"].Trim()) / mMj, 1).ToString("0.0");
                        mlongStr = sItem["KYQD1"].ToString() + "," + sItem["KYQD2"].ToString() + "," + sItem["KYQD3"].ToString();
                        mtmpArray = mlongStr.Split(',').ToList();
                        for (int vp = 0; vp < 3; vp++)
                        {
                            mkyqdArray.Add(GetSafeDouble(mtmpArray[vp]));
                        }
                        mkyqdArray.Sort();
                        mMaxKyqd = mkyqdArray[2];
                        mMinKyqd = mkyqdArray[0];
                        mMidKyqd = mkyqdArray[1];
                        mAvgKyqd = Math.Round(mkyqdArray.Average(), 2);

                        jsbeizhu = "";
                        sItem["MIDAVG"] = "0";
                        //计算抗压平均、达到设计强度、及进行单组合格判定
                        if (0 != mMidKyqd)
                        {
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["KYPJ"] = "无效";
                                sItem["DDSJQD"] = "不作评定";
                                sItem["HZCASE"] = "1";
                                jsbeizhu = "最大最小强度值超出中间值的15%,试验不作评定";
                                itemHG = false;
                                mAllHg = false;
                            }
                            if (mMaxKyqd - mMidKyqd > Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["HZCASE"] = "2";
                                jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                                if (mSjcc != 0 && mSjcc1 != 0)
                                {
                                    sItem["KYPJ"] = Math.Round(mMidKyqd, 1).ToString();
                                }
                                if (mSz != 0)
                                {
                                    sItem["DDSJQD"] = Math.Round(GetSafeDouble(sItem["KYPJ"]) / mSz * 100, 0).ToString();
                                    if (GetSafeDouble(sItem["DDSJQD"]) <= mQdyq)
                                    {
                                        itemHG = false;
                                        mAllHg = false;
                                    }
                                }
                                sItem["MIDAVG"] = "1";
                            }
                            if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd > Math.Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["HZCASE"] = "3";
                                jsbeizhu = "最大最小强度值均未超出中间值的15%,试验结果取平均值";
                                if (mSjcc != 0 && mSjcc1 != 0)
                                {
                                    sItem["KYPJ"] = Math.Round(mMidKyqd, 1).ToString();
                                }
                                if (mSz != 0)
                                {
                                    sItem["DDSJQD"] = Math.Round(GetSafeDouble(sItem["KYPJ"]) / mSz * 100, 0).ToString();
                                    if (GetSafeDouble(sItem["DDSJQD"]) <= mQdyq)
                                    {
                                        itemHG = false;
                                        mAllHg = false;
                                    }
                                }
                                sItem["MIDAVG"] = "1";
                            }

                            if (mMaxKyqd - mMidKyqd <= Math.Round(mMidKyqd * 0.15, 1) && mMidKyqd - mMinKyqd <= Math.Round(mMidKyqd * 0.15, 1))
                            {
                                sItem["HZCASE"] = "4";
                                sItem["KYPJ"] = Math.Round(mAvgKyqd, 1).ToString();
                                if (mSz != 0)
                                {
                                    sItem["DDSJQD"] = Math.Round(GetSafeDouble(sItem["KYPJ"]) / mSz * 100, 0).ToString();
                                    if (GetSafeDouble(sItem["DDSJQD"]) <= mQdyq)
                                    {
                                        itemHG = false;
                                        mAllHg = false;
                                    }
                                }

                            }
                        }
                        else
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                    else
                    {
                        sItem["KYPJ"] = "----";
                    }
                    #endregion
                }

                if ("无效" == sItem["KYPJ"])
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样强度代表值无效。";
                }
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样强度代表值" + sItem["KYPJ"] + "MPa，" + "占设计强度" + sItem["DDSJQD"] + "%。";
                }
                //单组判断
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
