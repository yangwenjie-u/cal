using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class NLS : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_NLS_DJ"];
            var data = retData;

            var SItem = data["S_NLS"];
            var MItem = data["M_NLS"];
            bool sign = true;
            string mJSFF = "";
            double zj1 = 0, zj2 = 0, mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0;

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SHANGB"].Trim());
                if (null != extraFieldsDj)
                {
                    MItem[0]["G_KYQD"] = string.IsNullOrEmpty(extraFieldsDj["KYQD"]) ? extraFieldsDj["KYQD"] : extraFieldsDj["KYQD"].Trim();
                    mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"];
                }
                else
                {
                    mJSFF = "";
                    mAllHg = false;
                    mjcjg = "不合格";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                switch (mJSFF)
                {
                    case "":
                        mMj1 = double.Parse(sItem["SJCD1"]) * double.Parse(sItem["SJKD1"]);
                        sItem["SJMJ1"] = mMj1.ToString();

                        mMj2 = double.Parse(sItem["SJCD2"]) * double.Parse(sItem["SJKD2"]);
                        sItem["SJMJ2"] = mMj2.ToString();

                        mMj3 = double.Parse(sItem["SJCD3"]) * double.Parse(sItem["SJKD3"]);
                        sItem["SJMJ3"] = mMj3.ToString();

                        mMj4 = double.Parse(sItem["SJCD4"]) * double.Parse(sItem["SJKD4"]);
                        sItem["SJMJ4"] = mMj4.ToString();

                        mMj5 = double.Parse(sItem["SJCD5"]) * double.Parse(sItem["SJKD5"]);
                        sItem["SJMJ5"] = mMj5.ToString();

                        if ((mMj1 != 0) && (mMj2 != 0) && (mMj3 != 0) && (mMj4 != 0) && (mMj5 != 0))
                        {
                            sItem["LSQD1"] = (double.Parse(sItem["LSHZ1"]) / mMj1).ToString("0.00");
                            sItem["LSQD2"] = (double.Parse(sItem["LSHZ2"]) / mMj2).ToString("0.00");
                            sItem["LSQD3"] = (double.Parse(sItem["LSHZ3"]) / mMj3).ToString("0.00");
                            sItem["LSQD4"] = (double.Parse(sItem["LSHZ4"]) / mMj4).ToString("0.00");
                            sItem["LSQD5"] = (double.Parse(sItem["LSHZ5"]) / mMj5).ToString("0.00");
                            sItem["LSPJ"] = ((double.Parse(sItem["LSQD1"]) + double.Parse(sItem["LSQD2"]) + double.Parse(sItem["LSQD3"]) + double.Parse(sItem["LSQD4"]) + double.Parse(sItem["LSQD5"])) / 5).ToString("0.00");
                        }
                        else
                        {
                            sItem["LSQD1"] = "0";
                            sItem["LSQD2"] = "0";
                            sItem["LSQD3"] = "0";
                            sItem["LSQD4"] = "0";
                            sItem["LSQD5"] = "0";
                            sItem["LSPJ"] = "0";
                        }
                        if (double.Parse(sItem["LSPJ"]) >= double.Parse(MItem[0]["G_KYQD"]) && sItem["PHTZ1"].Trim() != "界面层破坏" && sItem["PHTZ2"].Trim() != "界面层破坏" && sItem["PHTZ3"].Trim() != "界面层破坏" && sItem["PHTZ4"].Trim() != "界面层破坏" && sItem["PHTZ5"].Trim() != "界面层破坏")
                        {
                            MItem[0]["HG_KYQD"] = "合格";
                            sItem["JCJG"] = "合格";
                            jsbeizhu = "该组试样所检项符合上述标准要求。";
                            mAllHg = true;
                        }
                        else
                        {
                            MItem[0]["HG_KYQD"] = "不合格";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "该组试样所检项不符合上述标准要求。";
                            mAllHg = false;
                        }
                        break;
                    case "1":
                        if (double.Parse(sItem["LSPJ"]) >= double.Parse(MItem[0]["G_KYQD"]))
                        {
                            MItem[0]["HG_KYQD"] = "合格";
                            sItem["JCJG"] = "合格";
                            jsbeizhu = "该组试样所检项符合上述标准要求。";
                            mAllHg = true;
                        }
                        else
                        {
                            MItem[0]["HG_KYQD"] = "不合格";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "该组试样所检项不符合上述标准要求。";
                            mAllHg = false;
                        }
                        break;
                }

            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_NLS"))
            {
                data["M_NLS"] = new List<IDictionary<string, string>>();
            }
            var M_NLS = data["M_NLS"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_NLS == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_NLS.Add(m);
            }
            else
            {
                M_NLS[0]["JCJG"] = mjcjg;
                M_NLS[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
