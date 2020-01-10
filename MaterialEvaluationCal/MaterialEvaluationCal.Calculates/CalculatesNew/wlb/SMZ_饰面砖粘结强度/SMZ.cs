using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
/*饰面砖粘结强度*/
namespace Calculates
{
    public class SMZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_SMZ_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_SMZS = data["S_SMZ"];
            if (!data.ContainsKey("M_SMZ"))
            {
                data["M_SMZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_SMZ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            MItem[0]["FJJJ1"] = "";
            MItem[0]["FJJJ2"] = "";
            MItem[0]["FJJJ3"] = "";
            string mJSFF = "";
            double mPjbxy, mDkbxy, mSz = 0;
            double mMaxKyqd, mMinKyqd = 0;
            string mlongStr = "";
            List<double> mkyhzArray = new List<double>();
            List<string> mtmpArray = new List<string>();
            string mypmc = "";
            //遍历从表数据
            foreach (var sItem in S_SMZS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                string mSjdj = sItem["SJDJ"];
                if (mSjdj == null)
                {
                    mSjdj = "";
                }
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == mSjdj.Trim());
                if (null != extraFieldsDj)
                {
                    mPjbxy = double.Parse(extraFieldsDj["PJBXY"]);
                    mSz = mPjbxy;
                    mDkbxy = double.Parse(extraFieldsDj["DKBXY"]);
                    mJSFF = extraFieldsDj["JSFF"] == null ? "" : extraFieldsDj["JSFF"].ToLower();
                }
                else
                {
                    mPjbxy = 0;
                    mDkbxy = 0;
                    mSz = 0;
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "不合格";
                    continue;
                }

                sItem["BZYQ"] = "每组试样平均粘结强度不应小于" + string.Format(extraFieldsDj["PJBXY"], "0.0") + "MPa，每组可有一个试样的粘结强度小于" + string.Format(extraFieldsDj["PJBXY"], "0.0") + "MPa，但不应小于" + string.Format(extraFieldsDj["DKBXY"], "0.0") + "MPa。";
                if (mJSFF == "")
                {
                    if (jcxm.Contains("、粘结强度、"))
                    {
                        double mMj1, mMj2, mMj3, mPjz = 0;
                        mMj1 = Math.Round(Conversion.Val(sItem["CD1"]) * Conversion.Val(sItem["KD1"]), 1);
                        mMj2 = Math.Round(Conversion.Val(sItem["CD2"]) * Conversion.Val(sItem["KD2"]), 1);
                        mMj3 = Math.Round(Conversion.Val(sItem["CD3"]) * Conversion.Val(sItem["KD3"]), 1);
                        sItem["MJ1"] = Math.Round(mMj1, 1).ToString("0.0");
                        sItem["MJ2"] = Math.Round(mMj2, 1).ToString("0.0");
                        sItem["MJ3"] = Math.Round(mMj3, 1).ToString("0.0");
                        if (mMj1 != 0)
                        {
                            sItem["KYQD1"] = Math.Round(1000 * Conversion.Val(sItem["KYHZ1"]) / mMj1, 1).ToString();
                        }
                        else
                        {
                            sItem["KYQD1"] = "0";
                        }
                        if (mMj2 != 0)
                        {
                            sItem["KYQD2"] = Math.Round(1000 * Conversion.Val(sItem["KYHZ2"]) / mMj1, 1).ToString();
                        }
                        else
                        {
                            sItem["KYQD2"] = "0";
                        }
                        if (mMj3 != 0)
                        {
                            sItem["KYQD3"] = Math.Round(1000 * Conversion.Val(sItem["KYHZ3"]) / mMj1, 1).ToString();
                        }
                        else
                        {
                            sItem["KYQD3"] = "0";
                        }
                        //计算平均值
                        mPjz = (Conversion.Val(sItem["KYQD1"]) + Conversion.Val(sItem["KYQD2"]) + Conversion.Val(sItem["KYQD3"])) / 3;
                        sItem["KYPJ"] = Math.Round(mPjz, 1).ToString();
                        mPjz = Math.Round(mPjz, 1);
                        //计算最大、最小值
                        mlongStr = sItem["KYQD1"].ToString() + "," + sItem["KYQD2"].ToString() + "," + sItem["KYQD3"].ToString();
                        mtmpArray = mlongStr.Split(',').ToList();
                        for (int vp = 0; vp < 3; vp++)
                        {
                            mkyhzArray.Add(double.Parse(mtmpArray[vp]));
                        }
                        mkyhzArray.Sort();
                        mMaxKyqd = mkyhzArray[2];
                        mMinKyqd = Math.Round(mkyhzArray[0], 1);

                        //单组合格判定
                        if (mPjz < mPjbxy)
                        {
                            sItem["PJZ_HG"] = "False";
                        }
                        else
                        {
                            sItem["PJZ_HG"] = "True";
                        }
                        if (mMinKyqd < mDkbxy)
                        {
                            sItem["MIN_HG"] = "False";
                        }
                        else
                        {
                            if (mkyhzArray[1] < mPjbxy)
                            {
                                sItem["MIN_HG"] = "False";
                            }
                            else
                            {
                                sItem["MIN_HG"] = "True";
                            }
                        }
                        if ("True" != sItem["PJZ_HG"] && "True" != sItem["MIN_HG"])
                        {
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                }
                string mZh = sItem["ZH_G"];
                //string mZh = "组号";
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                    MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + mZh.Trim() + "#";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + mZh.Trim() + "#";
                }
                if ("样板件" == sItem["GCBW1"])
                {
                    mypmc = "样板件";
                }
                else
                {
                    mypmc = "试样";
                }

            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            else
            {
                jsbeizhu = "不合格";
            }

            if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"].Trim()))
            {
                jsbeizhu = mypmc + MItem[0]["FJJJ3"] + "所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"].Trim()))
            {
                jsbeizhu = mypmc + MItem[0]["FJJJ2"] + "不符合标准要求。";
                MItem[0]["FJJJ2"] = mypmc + MItem[0]["FJJJ2"] + "不符合" + MItem[0]["PDBZ"] + "标准要求。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
