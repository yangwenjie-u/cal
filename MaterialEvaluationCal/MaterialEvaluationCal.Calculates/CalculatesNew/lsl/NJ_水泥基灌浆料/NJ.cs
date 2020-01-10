using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class NJ : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_NJ"];
            var MItem = data["M_NJ"];
            var mrsGg = dataExtra["BZ_NJGG"];
            if (!data.ContainsKey("M_NJ"))
            {
                data["M_NJ"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];

            foreach (var sItem in SItem)
            {
                double md1, md2, md3, xd1, xd2, xd3, md, pjmd, sum, mSjcc, mSjcc1, mMj, mHsxs;
                bool flag = true, sign = true, mark = true;
                int mbHggs = 0;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                var mrsgg = mrsGg.FirstOrDefault(u => u["MC"] == sItem["GGXH"].Trim());
                if (mrsgg != null)
                {
                    sItem["SJCC"] = mrsgg["CD"];
                    sItem["HSXS"] = 1.ToString();
                }
                else
                {
                    sItem["SJCC"] = 0.ToString();
                    jsbeizhu = "规格不祥。 \r\n";
                    continue;
                }
                mSjcc = Conversion.Val(sItem["SJCC"]);
                double sds = Conversion.Val(sItem["SJCC1"]);
                if (string.IsNullOrEmpty(sds.ToString()) || 0 == sds)
                {
                    mSjcc1 = mSjcc;
                }
                else
                {
                    mSjcc1 = Conversion.Val(sItem["SJCC1"]);
                }
                mMj = mSjcc * mSjcc1;

                if (mMj <= 0)
                {
                    continue;
                }
                double mSz = 30;
                sItem["QDYQ"] = "≥30";
                mHsxs = Conversion.Val(sItem["HSXS"]);
                if (mHsxs <= 0)
                {
                    continue;
                }
                if (jcxm.Contains("、抗压、"))
                {
                    string mlongStr = "";
                    sItem["KYQD1"] = Round(1000 * (Conversion.Val(sItem["KYHZ1"]) / mMj), 1).ToString();
                    sItem["KYQD2"] = Round(1000 * (Conversion.Val(sItem["KYHZ2"]) / mMj), 1).ToString();
                    sItem["KYQD3"] = Round(1000 * (Conversion.Val(sItem["KYHZ3"]) / mMj), 1).ToString();
                    sItem["KYQD4"] = Round(1000 * (Conversion.Val(sItem["KYHZ4"]) / mMj), 1).ToString();
                    sItem["KYQD5"] = Round(1000 * (Conversion.Val(sItem["KYHZ5"]) / mMj), 1).ToString();
                    sItem["KYQD6"] = Round(1000 * (Conversion.Val(sItem["KYHZ6"]) / mMj), 1).ToString();
                    mlongStr = sItem["KYQD1"] + "," + sItem["KYQD2"] + "," + sItem["KYQD3"] + "," + sItem["KYQD4"] + "," + sItem["KYQD5"] + "," + sItem["KYQD6"];
                    string[] mtmpArray = mlongStr.Split(',');
                    List<double> mtmpList = new List<double>();
                    foreach (string str in mtmpArray)
                    {
                        mtmpList.Add(Conversion.Val(str));
                    }
                    mtmpList.Sort();
                    double mMaxKyqd = mtmpList[5];
                    double mMinKyqd = mtmpList[0];
                    double mAvgKyqd = Round(mtmpList.Average(),1);
                    double mpj;
                    if (mMaxKyqd - mAvgKyqd > Round(mAvgKyqd * 0.2, 1) || mAvgKyqd - mMinKyqd > Round(mAvgKyqd * 0.2, 1))
                    {
                        mpj = Round((Conversion.Val(sItem["KYQD1"]) + Conversion.Val(sItem["KYQD2"]) + Conversion.Val(sItem["KYQD3"]) + Conversion.Val(sItem["KYQD4"]) + Conversion.Val(sItem["KYQD5"]) + Conversion.Val(sItem["KYQD6"]) - mMaxKyqd - mMinKyqd) / 4, 1);
                    }
                    else
                    {
                        mpj = mAvgKyqd;
                    }
                    sItem["KYPJ"] = Round(mpj / Conversion.Val(sItem["HSXS"]), 1).ToString();
                    var ky = IsQualified(sItem["QDYQ"], sItem["KYPJ"], true);
                    if (ky == "符合")
                        sItem["JCJG"] = "合格";
                    else
                        sItem["JCJG"] = "不合格";
                }
                else
                {
                    sItem["KYPJ"] = "----";
                    sItem["JCJG"] = "----";
                }
                mAllHg = mAllHg && sItem["JCJG"] == "合格" ? true : false;
                if (mAllHg)
                {
                    jsbeizhu = "该组试样所检项目符合" + mItem["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "该组试样不符合" + mItem["PDBZ"] + "标准要求。";
                }
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGSM"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
