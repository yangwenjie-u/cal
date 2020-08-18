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
            bool mFlag_Hg = false, mFlag_Bhg = false;

            foreach (var sItem in SItem)
            {
                string  mSjdj = "", mJSFF = "";
                    double mklhz1 = 0, mklhz2 = 0, mklhz3 = 0, mPjz = 0;

                mSjdj = !string.IsNullOrEmpty(sItem["SJDJ"]) ? sItem["SJDJ"].Trim() : "";

                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["GGXH"] == sItem["GGXH"].Trim() && u["MC"] == mSjdj);
                if (extraFieldsDj == null)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }
                //KLHZ预拉力
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
                //抗滑移系数
                sItem["HYXS1"] = Math.Round(GetSafeDouble(sItem["HYHZ1"]) / (2 * mklhz1), 2).ToString("0.00");
                sItem["HYXS2"] = Math.Round(GetSafeDouble(sItem["HYHZ2"]) / (2 * mklhz2), 2).ToString("0.00");
                sItem["HYXS3"] = Math.Round(GetSafeDouble(sItem["HYHZ3"]) / (2 * mklhz3), 2).ToString("0.00");

                //K平均值
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
                    if (sItem["HYXS1PD"] == "合格" || sItem["HYXS2PD"] == "合格" || sItem["HYXS3PD"] == "合格")
                    {
                        mFlag_Hg = true;
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["JCJG"] = "合格";
                    sItem["HYXSPD"] = "合格";
                    mAllHg = true;
                }

                mAllHg = mAllHg && (sItem["JCJG"] == "合格");
            }

    
            #region 添加最终报告
            //综合判断
            if (mbhggs == 0)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            if (mbhggs > 0)
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目不符合要求。";
                //if (mFlag_Bhg && mFlag_Hg)
                //    MItem[0]["JCJGMS"] = "依据标准" + MItem[0]["PDBZ"] + ",所检项目" + jcxmBhg.TrimEnd('、') + "不符合标准要求。";
            }
            #endregion
            
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
