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
            double zj1 = 0, zj2 = 0, mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0, mMj9 = 0, mMj10 = 0;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxmCur = "粘结强度";
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
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                switch (mJSFF)
                {
                    case "": //外墙外保温系统拉伸粘结强度  《外墙外保温工程技术规程》JGJ 144-2019
                        int zxz = 0; //每组最小粘结强度不小于规定值的75% 合格数
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            #region 复检
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

                            mMj6 = double.Parse(sItem["SJCD6"]) * double.Parse(sItem["SJKD6"]);
                            sItem["SJMJ6"] = mMj6.ToString();

                            mMj7 = double.Parse(sItem["SJCD7"]) * double.Parse(sItem["SJKD7"]);
                            sItem["SJMJ7"] = mMj7.ToString();

                            mMj8 = double.Parse(sItem["SJCD8"]) * double.Parse(sItem["SJKD8"]);
                            sItem["SJMJ8"] = mMj8.ToString();

                            mMj9 = double.Parse(sItem["SJCD9"]) * double.Parse(sItem["SJKD9"]);
                            sItem["SJMJ9"] = mMj9.ToString();

                            mMj10 = double.Parse(sItem["SJCD10"]) * double.Parse(sItem["SJKD10"]);
                            sItem["SJMJ10"] = mMj10.ToString();

                            if ((mMj1 != 0) && (mMj2 != 0) && (mMj3 != 0) && (mMj4 != 0) && (mMj5 != 0) && (mMj6 != 0) && (mMj7 != 0) && (mMj8 != 0) && (mMj9 != 0) && (mMj10 != 0))
                            {
                                sItem["LSQD1"] = Round(double.Parse(sItem["LSHZ1"]) / mMj1 * 1000, 2).ToString("0.00");
                                sItem["LSQD2"] = Round(double.Parse(sItem["LSHZ2"]) / mMj2 * 1000, 2).ToString("0.00");
                                sItem["LSQD3"] = Round(double.Parse(sItem["LSHZ3"]) / mMj3 * 1000, 2).ToString("0.00");
                                sItem["LSQD4"] = Round(double.Parse(sItem["LSHZ4"]) / mMj4 * 1000, 2).ToString("0.00");
                                sItem["LSQD5"] = Round(double.Parse(sItem["LSHZ5"]) / mMj5 * 1000, 2).ToString("0.00");
                                sItem["LSQD6"] = Round(double.Parse(sItem["LSHZ6"]) / mMj6 * 1000, 2).ToString("0.00");
                                sItem["LSQD7"] = Round(double.Parse(sItem["LSHZ7"]) / mMj7 * 1000, 2).ToString("0.00");
                                sItem["LSQD8"] = Round(double.Parse(sItem["LSHZ8"]) / mMj8 * 1000, 2).ToString("0.00");
                                sItem["LSQD9"] = Round(double.Parse(sItem["LSHZ9"]) / mMj9 * 1000, 2).ToString("0.00");
                                sItem["LSQD10"] = Round(double.Parse(sItem["LSHZ10"]) / mMj10 * 1000, 2).ToString("0.00");
                                sItem["LSPJ"] = Round((double.Parse(sItem["LSQD1"]) + double.Parse(sItem["LSQD2"]) + double.Parse(sItem["LSQD3"]) + double.Parse(sItem["LSQD4"]) + double.Parse(sItem["LSQD5"]) + double.Parse(sItem["LSQD6"]) + double.Parse(sItem["LSQD7"]) + double.Parse(sItem["LSQD8"]) + double.Parse(sItem["LSQD9"]) + double.Parse(sItem["LSQD10"])) / 10, 1).ToString("0.0");

                                for (int i = 1; i < 11; i++)
                                {
                                    if (GetSafeDouble(sItem["LSQD" + i]) >= double.Parse(MItem[0]["G_KYQD"]) * 0.75 && GetSafeDouble(sItem["LSQD" + i]) < double.Parse(MItem[0]["G_KYQD"]))
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
                                sItem["LSQD6"] = "0";
                                sItem["LSQD7"] = "0";
                                sItem["LSQD8"] = "0";
                                sItem["LSQD9"] = "0";
                                sItem["LSQD10"] = "0";
                                sItem["LSPJ"] = "0";
                            }

                            if (double.Parse(sItem["LSPJ"]) >= double.Parse(MItem[0]["G_KYQD"]) && zxz <= 1 && sItem["PHTZ1"].Trim() == "粘结砂浆破坏" && sItem["PHTZ2"].Trim() == "粘结砂浆破坏" && sItem["PHTZ3"].Trim() == "粘结砂浆破坏" && sItem["PHTZ4"].Trim() == "粘结砂浆破坏" && sItem["PHTZ5"].Trim() == "粘结砂浆破坏" && sItem["PHTZ6"].Trim() == "粘结砂浆破坏" && sItem["PHTZ7"].Trim() == "粘结砂浆破坏" && sItem["PHTZ8"].Trim() == "粘结砂浆破坏" && sItem["PHTZ9"].Trim() == "粘结砂浆破坏" && sItem["PHTZ10"].Trim() == "粘结砂浆破坏")
                            {
                                MItem[0]["HG_KYQD"] = "合格";
                                sItem["JCJG"] = "合格";
                                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目均符合要求。";
                                mAllHg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KYQD"] = "不合格";
                                sItem["JCJG"] = "不合格";
                                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                                mAllHg = false;
                            }
                            #endregion
                        }
                        else
                        {
                            #region 初检
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
                                sItem["LSQD1"] = Round(double.Parse(sItem["LSHZ1"]) / mMj1 * 1000, 2).ToString("0.00");
                                sItem["LSQD2"] = Round(double.Parse(sItem["LSHZ2"]) / mMj2 * 1000, 2).ToString("0.00");
                                sItem["LSQD3"] = Round(double.Parse(sItem["LSHZ3"]) / mMj3 * 1000, 2).ToString("0.00");
                                sItem["LSQD4"] = Round(double.Parse(sItem["LSHZ4"]) / mMj4 * 1000, 2).ToString("0.00");
                                sItem["LSQD5"] = Round(double.Parse(sItem["LSHZ5"]) / mMj5 * 1000, 2).ToString("0.00");
                                sItem["LSPJ"] = Round((double.Parse(sItem["LSQD1"]) + double.Parse(sItem["LSQD2"]) + double.Parse(sItem["LSQD3"]) + double.Parse(sItem["LSQD4"]) + double.Parse(sItem["LSQD5"])) / 5, 1).ToString("0.0");

                                for (int i = 1; i < 6; i++)
                                {
                                    if (GetSafeDouble(sItem["LSQD" + i]) >= double.Parse(MItem[0]["G_KYQD"]) * 0.75 && GetSafeDouble(sItem["LSQD" + i]) < double.Parse(MItem[0]["G_KYQD"]))
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

                            if (double.Parse(sItem["LSPJ"]) >= double.Parse(MItem[0]["G_KYQD"]) && zxz <= 1 && sItem["PHTZ1"].Trim() == "粘结砂浆破坏" && sItem["PHTZ2"].Trim() == "粘结砂浆破坏" && sItem["PHTZ3"].Trim() == "粘结砂浆破坏" && sItem["PHTZ4"].Trim() == "粘结砂浆破坏" && sItem["PHTZ5"].Trim() == "粘结砂浆破坏")
                            {
                                MItem[0]["HG_KYQD"] = "合格";
                                sItem["JCJG"] = "合格";
                                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                                mAllHg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KYQD"] = "不合格";
                                sItem["JCJG"] = "不合格";
                                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，需双倍复检。";
                                mAllHg = false;
                            }
                            #endregion
                        }

                        MItem[0]["G_KYQD"] = "≥" + MItem[0]["G_KYQD"].ToString() + "MPa，且粘接界面脱开面积不应大于50%。破坏荷载每组可有一个试样的粘接强度小于本标准规定值，但不应小于规定值的75 %。";

                        break;
                    case "1":
                        if (double.Parse(sItem["LSPJ"]) >= double.Parse(MItem[0]["G_KYQD"]))
                        {
                            MItem[0]["HG_KYQD"] = "合格";
                            sItem["JCJG"] = "合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                            mAllHg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            MItem[0]["HG_KYQD"] = "不合格";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，需双倍复检。";
                            mAllHg = false;
                        }
                        break;
                    case "2":   //建筑工程外墙饰面砖粘结强度 《建筑工程饰面砖粘结强度检验标准》JGJ/T 110-2017
                        zxz = 0; //每组允许有一个试样的粘结强度小于0.4Mpa,但不应小于0.3Mpa 合格数
                        if (MItem[0]["SFFJ"] == "复检")   //复检
                        {
                            #region 复检
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

                            mMj6 = double.Parse(sItem["SJCD6"]) * double.Parse(sItem["SJKD6"]);
                            sItem["SJMJ6"] = mMj6.ToString();

                            if ((mMj1 != 0) && (mMj2 != 0) && (mMj3 != 0) && (mMj4 != 0) && (mMj5 != 0) && (mMj6 != 0))
                            {
                                sItem["LSQD1"] = Round(double.Parse(sItem["LSHZ1"]) / mMj1, 2).ToString("0.00");
                                sItem["LSQD2"] = Round(double.Parse(sItem["LSHZ2"]) / mMj2, 2).ToString("0.00");
                                sItem["LSQD3"] = Round(double.Parse(sItem["LSHZ3"]) / mMj3, 2).ToString("0.00");
                                sItem["LSQD4"] = Round(double.Parse(sItem["LSHZ4"]) / mMj4, 2).ToString("0.00");
                                sItem["LSQD5"] = Round(double.Parse(sItem["LSHZ5"]) / mMj5, 2).ToString("0.00");
                                sItem["LSQD6"] = Round(double.Parse(sItem["LSHZ6"]) / mMj6, 2).ToString("0.00");

                                sItem["LSPJ"] = Round((double.Parse(sItem["LSQD1"]) + double.Parse(sItem["LSQD2"]) + double.Parse(sItem["LSQD3"]) + double.Parse(sItem["LSQD4"]) + double.Parse(sItem["LSQD5"]) + double.Parse(sItem["LSQD6"])) / 6, 1).ToString("0.0");

                                //每组允许有一个试样的粘结强度小于0.4Mpa,但不应小于0.3Mpa
                                for (int i = 1; i < 7; i++)
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
                                sItem["LSQD4"] = "0";
                                sItem["LSQD5"] = "0";
                                sItem["LSQD6"] = "0";
                                sItem["LSPJ"] = "0";
                            }

                            if (double.Parse(sItem["LSPJ"]) >= double.Parse(MItem[0]["G_KYQD"]) && zxz <= 1 && sItem["PHTZ1"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ2"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ3"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ4"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ5"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ6"].Trim() == "饰面砖与粘结层界面破坏")
                            {
                                MItem[0]["HG_KYQD"] = "合格";
                                sItem["JCJG"] = "合格";
                                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目均符合要求。";
                                mAllHg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KYQD"] = "不合格";
                                sItem["JCJG"] = "不合格";
                                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                                mAllHg = false;
                            }
                            #endregion
                        }
                        else
                        {
                            #region 初检
                            mMj1 = double.Parse(sItem["SJCD1"]) * double.Parse(sItem["SJKD1"]);
                            sItem["SJMJ1"] = mMj1.ToString();

                            mMj2 = double.Parse(sItem["SJCD2"]) * double.Parse(sItem["SJKD2"]);
                            sItem["SJMJ2"] = mMj2.ToString();

                            mMj3 = double.Parse(sItem["SJCD3"]) * double.Parse(sItem["SJKD3"]);
                            sItem["SJMJ3"] = mMj3.ToString();

                            if ((mMj1 != 0) && (mMj2 != 0) && (mMj3 != 0))
                            {
                                sItem["LSQD1"] = Round(double.Parse(sItem["LSHZ1"]) / mMj1, 2).ToString("0.00");
                                sItem["LSQD2"] = Round(double.Parse(sItem["LSHZ2"]) / mMj2, 2).ToString("0.00");
                                sItem["LSQD3"] = Round(double.Parse(sItem["LSHZ3"]) / mMj3, 2).ToString("0.00");

                                sItem["LSPJ"] = Round((double.Parse(sItem["LSQD1"]) + double.Parse(sItem["LSQD2"]) + double.Parse(sItem["LSQD3"])) / 3, 1).ToString("0.0");

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

                            if (double.Parse(sItem["LSPJ"]) >= double.Parse(MItem[0]["G_KYQD"]) && zxz <= 1 && sItem["PHTZ1"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ2"].Trim() == "饰面砖与粘结层界面破坏" && sItem["PHTZ3"].Trim() == "饰面砖与粘结层界面破坏")
                            {
                                MItem[0]["HG_KYQD"] = "合格";
                                sItem["JCJG"] = "合格";
                                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                                mAllHg = true;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                MItem[0]["HG_KYQD"] = "不合格";
                                sItem["JCJG"] = "不合格";
                                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，需双倍复检。";
                                mAllHg = false;
                            }
                            #endregion
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
