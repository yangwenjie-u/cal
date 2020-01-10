using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class LKH : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            int mbhggs = 0;//不合格数量
            var extraDJ = dataExtra["BZ_LKH_DJ"];

            var data = retData;

            var SItem = data["S_LKH"];
            var MItem = data["M_LKH"];
            if (!data.ContainsKey("M_LKH"))
            {
                data["M_LKH"] = new List<IDictionary<string, string>>();
            }
            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            bool sign = true;
            bool mFlag_Hg = false, mFlag_Bhg = false;

            foreach (var sItem in SItem)
            {
                string mSjdjbh = "", mSjdj = "", mJSFF = "";
                double mYqpjz = 0, mXdy21 = 0, mDy21 = 0, mklhz1 = 0, mklhz2 = 0, mklhz3 = 0, mPjz = 0;

                mSjdj = !string.IsNullOrEmpty(sItem["SJDJ"]) ? sItem["SJDJ"].Trim() : "";

                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["GGXH"] == sItem["GGXH"].Trim() && u["MC"] == mSjdj);
                if (extraFieldsDj != null)
                {
                    mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"].ToLower();
                }
                else
                {
                    mYqpjz = 0;
                    mXdy21 = 0;
                    mDy21 = 0;
                    mJSFF = "";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                switch (mJSFF)
                {
                    case "": //通常(缺省)的计算方法
                             // 计算单组的抗压强度,并进行合格判断
                        mklhz1 = GetSafeDouble(sItem["KLHZ1_1"].Trim()) + GetSafeDouble(sItem["KLHZ1_2"].Trim());
                        if (mklhz1 >= 100)
                        {
                            mklhz1 = Math.Round(mklhz1, 0);
                        }
                        if (mklhz1 >= 10 && mklhz1 < 100)
                        {
                            mklhz1 = Math.Round(mklhz1, 1);
                        }
                        if (mklhz1 >= 1 && mklhz1 < 10)
                        {
                            mklhz1 = Math.Round(mklhz1, 2);
                        }
                        if (mklhz1 < 1)
                        {
                            mklhz1 = Math.Round(mklhz1, 3);
                        }

                        mklhz2 = GetSafeDouble(sItem["KLHZ2_1"].Trim()) + GetSafeDouble(sItem["KLHZ2_2"].Trim());
                        if (mklhz2 >= 100)
                        {
                            mklhz2 = Math.Round(mklhz2, 0);
                        }
                        if (mklhz2 >= 10 && mklhz2 < 100)
                        {
                            mklhz2 = Math.Round(mklhz2, 1);
                        }
                        if (mklhz2 >= 1 && mklhz2 < 10)
                        {
                            mklhz2 = Math.Round(mklhz2, 2);
                        }
                        if (mklhz2 < 1)
                        {
                            mklhz2 = Math.Round(mklhz2, 3);
                        }

                        mklhz3 = GetSafeDouble(sItem["KLHZ3_1"].Trim()) + GetSafeDouble(sItem["KLHZ3_2"].Trim());
                        if (mklhz3 >= 100)
                        {
                            mklhz3 = Math.Round(mklhz3, 0);
                        }
                        if (mklhz3 >= 10 && mklhz3 < 100)
                        {
                            mklhz3 = Math.Round(mklhz3, 1);
                        }
                        if (mklhz3 >= 1 && mklhz3 < 10)
                        {
                            mklhz3 = Math.Round(mklhz3, 2);
                        }
                        if (mklhz3 < 1)
                        {
                            mklhz3 = Math.Round(mklhz3, 3);
                        }

                        sItem["HYXS1"] = Math.Round(GetSafeDouble(sItem["HYHZ1"]) / (2 * mklhz1), 2).ToString("0.00");
                        sItem["HYXS2"] = Math.Round(GetSafeDouble(sItem["HYHZ2"]) / (2 * mklhz2), 2).ToString("0.00");
                        sItem["HYXS3"] = Math.Round(GetSafeDouble(sItem["HYHZ3"]) / (2 * mklhz3), 2).ToString("0.00");

                        //抗压平均值
                        mPjz = Math.Round((GetSafeDouble(sItem["HYXS1"].Trim()) + GetSafeDouble(sItem["HYXS2"].Trim()) + GetSafeDouble(sItem["HYXS3"].Trim())) / 3, 2);
                        sItem["KPJ"] = mPjz.ToString("0.00");

                        if (GetSafeDouble(sItem["HYXS1"].Trim()) >= GetSafeDouble(sItem["YQHYXS"].Trim()))
                        {
                            sItem["HYXS1PD"] = "合格";
                        }
                        else
                        {
                            sItem["HYXS1PD"] = "不合格";
                        }

                        if (GetSafeDouble(sItem["HYXS2"].Trim()) >= GetSafeDouble(sItem["YQHYXS"].Trim()))
                        {
                            sItem["HYXS2PD"] = "合格";
                        }
                        else
                        {
                            sItem["HYXS2PD"] = "不合格";
                        }

                        if (GetSafeDouble(sItem["HYXS3"].Trim()) >= GetSafeDouble(sItem["YQHYXS"].Trim()))
                        {
                            sItem["HYXS3PD"] = "合格";
                        }
                        else
                        {
                            sItem["HYXS3PD"] = "不合格";
                        }

                        if (sItem["HYXS1PD"] == "不合格" || sItem["HYXS2PD"] == "不合格" || sItem["HYXS3PD"] == "不合格")
                        {
                            mbhggs++;
                            sItem["JCJG"] = "不合格";
                            sItem["HYXSPD"] = "不合格";
                            jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            if (sItem["HYXS1PD"] == "合格" || sItem["HYXS2PD"] == "合格" || sItem["HYXS3PD"] == "合格")
                            {
                                mFlag_Hg = true;
                                mFlag_Bhg = true;
                                jsbeizhu = "该组试样所检项部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                            }
                        }
                        else
                        {
                            sItem["JCJG"] = "合格";
                            sItem["HYXSPD"] = "合格";
                            jsbeizhu = "该组试样所检项符合" + MItem[0]["PDBZ"] + "标准要求。";
                            mAllHg = true;
                        }

                        mAllHg = mAllHg && (sItem["JCJG"] == "合格");
                        break;
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
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
