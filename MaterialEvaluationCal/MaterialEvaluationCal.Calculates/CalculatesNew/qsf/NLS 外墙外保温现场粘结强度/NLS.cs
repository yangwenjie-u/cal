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
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SHANGB"].Trim() && u["JCYJ"] == sItem["SYBZ"].Trim());
                if (null != extraFieldsDj)
                {
                    MItem[0]["G_KYQD"] = string.IsNullOrEmpty(extraFieldsDj["KYQD"]) ? extraFieldsDj["KYQD"] : GetSafeDouble(extraFieldsDj["KYQD"].Trim()).ToString("0.0");
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
                    case "": //外墙外保温系统拉伸粘结强度  《外墙外保温工程技术规程》JGJ 144-2019
                        int zxz = 0; //每组最小粘结强度不小于规定值的75% 合格数
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
                            sItem["LSPJ"] = ((double.Parse(sItem["LSQD1"]) + double.Parse(sItem["LSQD2"]) + double.Parse(sItem["LSQD3"]) + double.Parse(sItem["LSQD4"]) + double.Parse(sItem["LSQD5"])) / 5).ToString("0.0");

                            for (int i = 1; i < 6; i++)
                            {
                                if (GetSafeDouble(sItem["LSQD" + i]) >= double.Parse(MItem[0]["G_KYQD"]) * 0.75 && GetSafeDouble(sItem["LSQD" + i])< double.Parse(MItem[0]["G_KYQD"]))
                                {
                                    zxz++;
                                }
                            }
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
                        if (double.Parse(sItem["LSPJ"]) >= double.Parse(MItem[0]["G_KYQD"]) && zxz<=1 && sItem["PHTZ1"].Trim() == "粘结砂浆破坏" && sItem["PHTZ2"].Trim() == "粘结砂浆破坏" && sItem["PHTZ3"].Trim() == "粘结砂浆破坏" && sItem["PHTZ4"].Trim() == "粘结砂浆破坏" && sItem["PHTZ5"].Trim() == "粘结砂浆破坏")
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
                        MItem[0]["G_KYQD"] = "≥" + MItem[0]["G_KYQD"].ToString() + "MPa，且粘接界面脱开面积不应大于50%。破坏荷载每组可有有一个试样的粘接强度小于本标准规定值，但不应小于规定值的75 %。";

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
                    case "2":   //建筑工程外墙饰面砖粘结强度 《建筑工程饰面砖粘结强度检验标准》JGJ/T 110-2017
                        zxz = 0; //每组允许有一个试样的粘结强度小于0.4Mpa,但不应小于0.3Mpa 合格数
                        mMj1 = double.Parse(sItem["SJCD1"]) * double.Parse(sItem["SJKD1"]);
                        sItem["SJMJ1"] = mMj1.ToString();

                        mMj2 = double.Parse(sItem["SJCD2"]) * double.Parse(sItem["SJKD2"]);
                        sItem["SJMJ2"] = mMj2.ToString();

                        mMj3 = double.Parse(sItem["SJCD3"]) * double.Parse(sItem["SJKD3"]);
                        sItem["SJMJ3"] = mMj3.ToString();

                        if ((mMj1 != 0) && (mMj2 != 0) && (mMj3 != 0))
                        {
                            sItem["LSQD1"] = (double.Parse(sItem["LSHZ1"]) / mMj1).ToString("0.00");
                            sItem["LSQD2"] = (double.Parse(sItem["LSHZ2"]) / mMj2).ToString("0.00");
                            sItem["LSQD3"] = (double.Parse(sItem["LSHZ3"]) / mMj3).ToString("0.00");

                            sItem["LSPJ"] = ((double.Parse(sItem["LSQD1"]) + double.Parse(sItem["LSQD2"]) + double.Parse(sItem["LSQD3"])) / 3).ToString("0.0");

                            //每组允许有一个试样的粘结强度小于0.4Mpa,但不应小于0.3Mpa
                            for (int i = 1; i < 4; i++)
                            {
                                if (GetSafeDouble(sItem["LSQD" + i]) < double.Parse(MItem[0]["G_KYQD"]) && GetSafeDouble(sItem["LSQD" + i]) >= 0.3)
                                {
                                    zxz++;
                                }
                            }
                        }
                        else
                        {
                            sItem["LSQD1"] = "0";
                            sItem["LSQD2"] = "0";
                            sItem["LSQD3"] = "0";
                            sItem["LSPJ"] = "0";
                        }

                        if (double.Parse(sItem["LSPJ"]) >= double.Parse(MItem[0]["G_KYQD"]) && zxz <= 1 && sItem["PHTZ1"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ2"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ3"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ4"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ5"].Trim() == "饰面砖与粘结层界面破坏")
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
                        MItem[0]["G_KYQD"] = "≥" + MItem[0]["G_KYQD"].ToString() + "MPa，每组允许有一个试样的粘结强度小于0.4Mpa,但不应小于0.3Mpa。";

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
