using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class QX : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            double[] mkyqdArray = new double[5];
            double[] mkyhzArray = new double[5];
            List<string> mtmpArray = new List<string>();
            double mZj, mMj, mGjb, mXzxs;
            string mcGjb;
            double mMaxKyqd, mMinKyqd;
            double mPjz;
            string mSjdjbh, mSjdj;
            double mSz = 0;
            int vp;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            double mSum;
            mAllHg = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_QX_DJ"];
            var mrsHsb = dataExtra["BZ_QXHSB"];
            var mrsQjb = dataExtra["BZ_QXQJXS"];
            var MItem = data["M_QX"];
            var SItem = data["S_QX"];
            #endregion

            #region  计算开始
            mMinKyqd = 9999;
            mSum = 0;
            MItem[0]["BGZS_G"] = "0";
            //循环从表
            foreach (var sitem in SItem)
            {
                mSjdj = sitem["SJDJ"];            //设计等级名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    mSz = GetSafeDouble(mrsDj_Filter["SZ"]);
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"];
                }
                else
                {
                    mSz = 0;
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    continue;
                }
                //计算龄期
                if (sitem["ZZRQ"] == "----" || sitem["ZZRQ"] == "")
                { }
                else
                {
                    DateTime dtnow = new DateTime();
                    if (DateTime.TryParse(sitem["ZZRQ"], out dtnow))
                        sitem["LQ"] = (GetSafeDateTime(MItem[0]["SYRQQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString();

                    if (GetSafeDouble(sitem["LQ"]) > 10000)
                        sitem["LQ"] = "0";
                }
                sitem["ZJ"] = (Round(Conversion.Val(sitem["ZJ1"]) + Conversion.Val(sitem["ZJ2"]), 0) / 2).ToString("0.0");
                mZj = Conversion.Val(sitem["ZJ"]);
                mMj = 3.14159 * (mZj / 2) * (mZj / 2);
                if (string.IsNullOrEmpty(sitem["KYHZ1"]) || GetSafeDouble(sitem["KYHZ1"]) == 0)
                {
                    mSFwc = false;
                }
                if (mMj != 0)
                {
                    sitem["MJ"] = Round(mMj, 0).ToString();
                    //计算单组的抗压强度,并进行合格判断
                    sitem["KYHZ1"] = sitem["KYHZ1"];
                    sitem["KYQD1"] = Round(1000 * GetSafeDouble(sitem["KYHZ1"]) / mMj, 1).ToString("0.0");
                }
                else
                    sitem["KYQD1"] = "0";
                sitem["KYPJ"] = (GetSafeDouble(sitem["KYQD1"]) / 1).ToString();
                if (GetSafeDouble(sitem["KYPJ"]) < mMinKyqd)
                    mMinKyqd = GetSafeDouble(sitem["KYPJ"]);
                mSum = mSum + GetSafeDouble(sitem["KYQD1"]);
                MItem[0]["BGZS_G"] = ((int)GetSafeDouble(MItem[0]["BGZS_G"]) + 1).ToString();
            }
            //综合判断
            int count = 0;
            int i;
            string gjmc = string.Empty;
            double minkypj = 0;
            double[] tj;
            double md = 0;
            int mrecn = SItem.Count();
            int a = 0;
            var mrssubTable = SItem[a];
            gjmc = mrssubTable["GJMC"].Trim();
            for (int iii = 0; iii <= mrecn + 3000; iii++)
            {
                if (iii+1 <= SItem.Count())
                {
                    if (gjmc == mrssubTable["GJMC"].Trim())
                    {
                        count = count + 1;
                        if (iii + 1 <= SItem.Count())
                        {
                            a++;
                            if (a >= SItem.Count())
                                a--;
                            else
                                mrssubTable = SItem[a];
                        }
                    }
                    else
                    {
                        tj = new double[count + 1];
                        if (count >= 1)
                        {
                            for (i = count; i >= 1; i--)
                            {
                                a--;
                                if (a < 0)
                                    a++;
                                else
                                    mrssubTable = SItem[a];
                                tj[i] = GetSafeDouble(mrssubTable["KYPJ"]);
                            }
                        }
                        minkypj = tj[1];
                        var mrsDj_Filter2 = mrsDj.FirstOrDefault(x => x["MC"].Contains(mrssubTable["SJDJ"].Trim()));
                        if (mrsDj_Filter2 != null && mrsDj_Filter2.Count > 0)
                        {
                            mSz = GetSafeDouble(mrsDj_Filter2["SZ"]);
                        }
                        else
                        {
                            mSz = 0;
                            mJSFF = "";
                            mrssubTable["JCJG"] = "依据不详";
                            break;
                        }
                        for (i = 1; i <= count; i++)
                        {
                            if (minkypj > tj[i])
                                minkypj = tj[i];
                        }
                        for (i = 1; i <= count; i++)
                        {

                            if (count == 1)
                            {
                                minkypj = 0;
                                mrssubTable["GJKYPJ"] = "0";
                            }
                            else
                                mrssubTable["GJKYPJ"] = minkypj.ToString();
                            mrssubTable["GJKYPJ"] = minkypj.ToString();
                            if (mSz != 0 && minkypj != 0)
                            {
                                md = Round(100 * minkypj / mSz, 1);
                                mrssubTable["DDSJQD"] = md.ToString("0.0");
                            }
                            else
                                mrssubTable["DDSJQD"] = "----";
                            if (bool.Parse(MItem[0]["SFPL"]))
                            {
                                md = Round(100 * GetSafeDouble(mrssubTable["KYQD1"]) / mSz, 1);
                                mrssubTable["DDSJQD"] = md.ToString("0.0");
                            }


                            if (Conversion.Val(mrssubTable["DDSJQD"]) < 100 && mrssubTable["DDSJQD"] != "----")
                                mAllHg = false;
                            if (iii + 1 <= SItem.Count())
                            {
                                a++;
                                if (a >= SItem.Count())
                                    a--;
                                else
                                    mrssubTable = SItem[a];
                            }
                        }
                        gjmc = mrssubTable["GJMC"].Trim();
                        count = 0;
                    }
                }
                else
                {
                    tj = new double[count + 1];
                    if (count >= 1)
                    {
                        for (i = count; i >= 1; i--)
                        {
                            a--;
                            if (a < 0)
                                a++;
                            else
                                mrssubTable = SItem[a];
                            tj[i] = GetSafeDouble(mrssubTable["KYPJ"]);
                        }
                    }
                    minkypj = tj[1];
                    var mrsDj_Filter2 = mrsDj.FirstOrDefault(x => x["MC"].Contains(mrssubTable["SJDJ"].Trim()));
                    if (mrsDj_Filter2 != null && mrsDj_Filter2.Count > 0)
                    {
                        mSz = GetSafeDouble(mrsDj_Filter2["SZ"]);
                    }
                    else
                    {
                        mSz = 0;
                        mJSFF = "";
                        mrssubTable["JCJG"] = "依据不详";
                        break;
                    }
                    for (i = 1; i <= count; i++)
                    {
                        if (minkypj > tj[i])
                            minkypj = tj[i];
                    }
                    for (i = 1; i <= count; i++)
                    {
                        if (count == 1)
                        {
                            minkypj = 0;
                            mrssubTable["GJKYPJ"] = "0";
                        }
                        else
                            mrssubTable["GJKYPJ"] = minkypj.ToString();
                        if (mSz != 0 && minkypj != 0)
                        {
                            md = Round(100 * minkypj / mSz, 1);
                            mrssubTable["DDSJQD"] = md.ToString("0.0");
                        }
                        else
                            mrssubTable["DDSJQD"] = "----";
                        if (bool.Parse(MItem[0]["SFPL"]))
                        {
                            md = Round(100 * GetSafeDouble(mrssubTable["KYQD1"]) / mSz, 1);
                            mrssubTable["DDSJQD"] = md.ToString("0.0");
                        }
                        if (Conversion.Val(mrssubTable["DDSJQD"]) < 100 && mrssubTable["DDSJQD"] != "----")
                            mAllHg = false;
                        if (iii + 1 <= SItem.Count())
                        {
                            a++;
                            if (a >= SItem.Count())
                                a--;
                            else
                                mrssubTable = SItem[a];
                        }
                    }
                }
            }

            MItem[0]["JCJGMS"] = "";
            if (bool.Parse(MItem[0]["SFPL"]))
            {
                MItem[0]["PJZ"] = Round(mSum / GetSafeDouble(MItem[0]["BGZS_G"]), 1).ToString();
                double mJfc = 0;
                foreach (var sitem in SItem)
                {
                    mJfc = mJfc + (GetSafeDouble(sitem["KYQD1"]) - GetSafeDouble(MItem[0]["PJZ"])) * (GetSafeDouble(sitem["KYQD1"]) - GetSafeDouble(MItem[0]["PJZ"]));
                }
                if (GetSafeDouble(MItem[0]["BGZS_G"]) - 1 != 0)
                    MItem[0]["BZC"] = Round(Math.Sqrt(mJfc / (GetSafeDouble(MItem[0]["BGZS_G"]) - 1)), 1).ToString();
                var mrsQjb_Filter = mrsQjb.FirstOrDefault(x => x["GS"].Contains(MItem[0]["BGZS_G"]));
                if (mrsQjb_Filter != null && mrsQjb_Filter.Count > 0)
                {
                    MItem[0]["SXZ"] = Round(GetSafeDouble(MItem[0]["PJZ"]) - GetSafeDouble(mrsQjb_Filter["K1"]) * GetSafeDouble(MItem[0]["BZC"]), 1).ToString();
                    MItem[0]["XXZ"] = Round(GetSafeDouble(MItem[0]["PJZ"]) - GetSafeDouble(mrsQjb_Filter["K2"]) * GetSafeDouble(MItem[0]["BZC"]), 1).ToString();
                    double mcxxc = GetSafeDouble(MItem[0]["SXZ"]) - GetSafeDouble(MItem[0]["XXZ"]);
                    if (mcxxc > 5 && mcxxc > 0.1 * GetSafeDouble(MItem[0]["PJZ"]))
                    {
                        MItem[0]["JCJGMS"] = "推定上下限值之差大于5.0MPa 和0.10平均值的较大值。";
                        MItem[0]["KYPJ"] = "0";
                    }
                    else
                    {
                        MItem[0]["KYPJ"] = MItem[0]["SXZ"];
                        MItem[0]["JCJGMS"] = "本报告对该批构件混凝土强度负责。";
                    }
                }
                MItem[0]["SJDDJ"] = Round(100 * GetSafeDouble(MItem[0]["KYPJ"]) / mSz, 0).ToString();
                if (Conversion.Val(MItem[0]["SJDDJ"]) < 100)
                    mAllHg = false;
                else
                    mAllHg = true;
                MItem[0]["JCJGMS"] = "该批构件混凝土强度" + MItem[0]["KYPJ"] + "MPa，" + "占设计强度" + MItem[0]["SJDDJ"] + "%。";
            }
            else
            {
                if (mAllHg)
                    MItem[0]["JCJGMS"] = "该次检测混凝土强度全部大于等于设计强度。";
                else
                    MItem[0]["JCJGMS"] = "该次检测混凝土强度部分小于设计强度。";
            }
            //主表总判断赋值
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            #endregion

            /************************ 代码结束 *********************/
        }
    }
}
