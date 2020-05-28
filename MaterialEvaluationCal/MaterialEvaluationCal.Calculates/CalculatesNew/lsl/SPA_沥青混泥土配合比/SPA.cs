using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SPA : BaseMethods
    {
        public void Calc()
        {

            bool mAllHg = true, sign = true, mSFwc = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "";
            double md, md1, md2;
            var SItem = data["S_SPA"];
            var MItem = data["M_SPA"];
            var EItem = data["E_JLPB"];
            var ELQItem = data["E_LQ"];
            var mrsDj = dataExtra["BZ_CT_DJ"];

            if (!data.ContainsKey("M_SPA"))
            {
                data["M_SPA"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            var mItem = MItem[0];
            string stemp, dzbh;
            double zjSum = 0;
            double gdSum = 0;
            int count = 0;
            bool gjkBool = false;
            string gjkTgl = "";
            foreach (var sItem in SItem)
            {
                jsbeizhu = "";
                #region 吸水率   表干法
                sign = true;
                for (int i = 1; i < 7; i++)
                {
                    sign = IsNumeric(sItem["GZSJKZZL" + i].Trim());
                    sign = IsNumeric(sItem["SJSZZL" + i].Trim());
                    sign = IsNumeric(sItem["SJBGZL" + i].Trim());
                }
                if (sign)
                {
                    //吸水率 = （试件的表干质量 - 干燥试件的空中质量）/ （试件的表干质量 - 试件的水中质量）  * 100  取一位小数
                    for (int i = 1; i < 7; i++)
                    {
                        sItem["SJXSL" + i] = Round((GetSafeDouble(sItem["SJBGZL" + i].Trim()) - GetSafeDouble(sItem["GZSJKZZL" + i].Trim()))
                            / (GetSafeDouble(sItem["SJBGZL" + i].Trim()) - GetSafeDouble(sItem["SJSZZL" + i].Trim())) * 100, 1).ToString("0.0");
                    }
                }
                else
                {
                    throw new SystemException("密度测定（干燥）试件的空中质量、试件的水中质量或试件的表干质量试验数据录入有误。");
                }
                #endregion

                #region 马歇尔试件直径 高度代表值计算
                sign = true;
                for (int i = 1; i < 7; i++)
                {
                    sign = IsNumeric(sItem["SJZJ1_" + i].Trim());
                    sign = IsNumeric(sItem["SJZJ2_" + i].Trim());
                    sign = IsNumeric(sItem["SJGD1_" + i].Trim());
                    sign = IsNumeric(sItem["SJGD2_" + i].Trim());
                    sign = IsNumeric(sItem["SJGD3_" + i].Trim());
                    sign = IsNumeric(sItem["SJGD4_" + i].Trim());
                }
                if (sign)
                {
                    //平均值
                    for (int i = 1; i < 7; i++)
                    {
                        sItem["SJZJPJ" + i] = Round((GetSafeDouble(sItem["SJZJ1_" + i].Trim()) + GetSafeDouble(sItem["SJZJ2_" + i].Trim())) / 2, 1).ToString("0.0");
                        sItem["SJGDPJ" + i] = Round((GetSafeDouble(sItem["SJGD1_" + i].Trim()) + GetSafeDouble(sItem["SJGD2_" + i].Trim())
                            + GetSafeDouble(sItem["SJGD3_" + i].Trim()) + GetSafeDouble(sItem["SJGD4_" + i].Trim())) / 4, 1).ToString("0.0");
                        zjSum = zjSum + GetSafeDouble(sItem["SJZJPJ" + i]);
                        gdSum = gdSum + GetSafeDouble(sItem["SJGDPJ" + i]);
                        count++;
                    }
                    //代表值
                    sItem["SJZJDBZ"] = Round(zjSum / count, 1).ToString("0.0");
                    sItem["SJGDDBZ"] = Round(gdSum / count, 1).ToString("0.0");
                }
                else
                {
                    throw new SystemException("马歇尔试件直径与高度试验数据录入有误");
                }
                #endregion

                #region  生成曲线数据 及相关计算
                double hcjp_53 = 0;
                double hcjp_375 = 0;
                double hcjp_315 = 0;
                double hcjp_265 = 0;
                double hcjp_19 = 0;
                double hcjp_16 = 0;
                double hcjp_132 = 0;
                double hcjp_95 = 0;
                double hcjp_475 = 0;
                double hcjp_236 = 0;
                double hcjp_118 = 0;
                double hcjp_06 = 0;
                double hcjp_03 = 0;
                double hcjp_015 = 0;
                double hcjp_0075 = 0;
                double hcjp_SD = 0;
                double blzh = 0;
                #region 合成级配百分率
                for (int i = 1; i < 8; i++)
                {
                    //矿料比例值
                    if (!string.IsNullOrEmpty(sItem["BL_" + i].Trim()))
                    {
                        hcjp_53 = hcjp_53 + GetSafeDouble(sItem["TGBF53_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_375 = hcjp_375 + GetSafeDouble(sItem["TGBF375_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_315 = hcjp_315 + GetSafeDouble(sItem["TGBF315_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_265 = hcjp_265 + GetSafeDouble(sItem["TGBF265_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_19 = hcjp_19 + GetSafeDouble(sItem["TGBF19_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_16 = hcjp_16 + GetSafeDouble(sItem["TGBF16_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_132 = hcjp_132 + GetSafeDouble(sItem["TGBF132_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_95 = hcjp_95 + GetSafeDouble(sItem["TGBF95_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_475 = hcjp_475 + GetSafeDouble(sItem["TGBF475_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_236 = hcjp_236 + GetSafeDouble(sItem["TGBF236_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_118 = hcjp_118 + GetSafeDouble(sItem["TGBF118_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_06 = hcjp_06 + GetSafeDouble(sItem["TGBF06_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_03 = hcjp_03 + GetSafeDouble(sItem["TGBF03_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_015 = hcjp_015 + GetSafeDouble(sItem["TGBF015_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_0075 = hcjp_0075 + GetSafeDouble(sItem["TGBF0075_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        hcjp_SD = hcjp_SD + GetSafeDouble(sItem["TGBFSD_" + i].Trim()) * GetSafeDouble(sItem["BL_" + i].Trim());
                        blzh = blzh + GetSafeDouble(sItem["BL_" + i].Trim());
                    }
                }
                if (blzh < 1)
                {
                    throw new SystemException("矿料级配组成比例总和小于100%");
                }
                sItem["HCJP_53"] = Round(hcjp_53, 1).ToString("0.0");
                sItem["HCJP_375"] = Round(hcjp_375, 1).ToString("0.0");
                sItem["HCJP_315"] = Round(hcjp_315, 1).ToString("0.0");
                sItem["HCJP_265"] = Round(hcjp_265, 1).ToString("0.0");
                sItem["HCJP_19"] = Round(hcjp_19, 1).ToString("0.0");
                sItem["HCJP_16"] = Round(hcjp_16, 1).ToString("0.0");
                sItem["HCJP_132"] = Round(hcjp_132, 1).ToString("0.0");
                sItem["HCJP_95"] = Round(hcjp_95, 1).ToString("0.0");
                sItem["HCJP_475"] = Round(hcjp_475, 1).ToString("0.0");
                sItem["HCJP_236"] = Round(hcjp_236, 1).ToString("0.0");
                sItem["HCJP_118"] = Round(hcjp_118, 1).ToString("0.0");
                sItem["HCJP_06"] = Round(hcjp_06, 1).ToString("0.0");
                sItem["HCJP_03"] = Round(hcjp_03, 1).ToString("0.0");
                sItem["HCJP_015"] = Round(hcjp_015, 1).ToString("0.0");
                sItem["HCJP_0075"] = Round(hcjp_0075, 1).ToString("0.0");
                sItem["HCJP_SD"] = Round(hcjp_SD, 1).ToString("0.0");
                #endregion

                #region 获取标准值
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["KLJPLX"] == sItem["KLJPLX"]);
                if (sItem["KLJPLX"].Contains("AC"))
                {
                    //包含矿料级配类型数据 如果包含AC可能会进行关键孔判定  
                    mrsDj_Filter = mrsDj.FirstOrDefault(x => x["KLJPLX"] == sItem["KLJPLX"] && x["HHLMC"] == sItem["KLJPLX"]);
                    if (null != mrsDj_Filter)
                    {
                        gjkBool = true;
                        //取关键孔判定标准值
                        gjkTgl = mrsDj_Filter["GJSKTGL"];
                        //取标准值
                        mrsDj_Filter = mrsDj.FirstOrDefault(x => x["KLJPLX"].Contains(sItem["KLJPLX"]) && x["HHLMC"] != sItem["KLJPLX"]);
                    }
                }
                if (null == mrsDj_Filter)
                {
                    sItem["JCJG"] = "不下结论";
                    mAllHg = false;
                    sItem["JPDXPD"] = "标准范围值获取失败，请联系开发人员";
                    continue;
                }
                sItem["BZFW_53"] = mrsDj_Filter["BZFW53"];
                sItem["BZFW_375"] = mrsDj_Filter["BZFW375"];
                sItem["BZFW_315"] = mrsDj_Filter["BZFW315"];
                sItem["BZFW_265"] = mrsDj_Filter["BZFW265"];
                sItem["BZFW_19"] = mrsDj_Filter["BZFW19"];
                sItem["BZFW_16"] = mrsDj_Filter["BZFW16"];
                sItem["BZFW_132"] = mrsDj_Filter["BZFW132"];
                sItem["BZFW_95"] = mrsDj_Filter["BZFW95"];
                sItem["BZFW_475"] = mrsDj_Filter["BZFW475"];
                sItem["BZFW_236"] = mrsDj_Filter["BZFW236"];
                sItem["BZFW_118"] = mrsDj_Filter["BZFW118"];
                sItem["BZFW_06"] = mrsDj_Filter["BZFW06"];
                sItem["BZFW_03"] = mrsDj_Filter["BZFW03"];
                sItem["BZFW_015"] = mrsDj_Filter["BZFW015"];
                sItem["BZFW_0075"] = mrsDj_Filter["BZFW0075"];
                #endregion

                #region 一般性判定
                string sfpd1, sfpd2, sfpd3, sfpd4, sfpd5, sfpd6, sfpd7, sfpd8, sfpd9, sfpd10, sfpd11, sfpd12, sfpd13, sfpd14, sfpd15;
                if (100 == GetSafeDouble(sItem["BZFW_53"]))
                {
                    sfpd1 = GetSafeDouble(sItem["HCJP_53"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_53"] && 100 != GetSafeDouble(sItem["BZFW_53"]))
                {
                    sfpd1 = IsQualified(sItem["BZFW_53"], sItem["HCJP_53"], false);
                }
                else
                {
                    sItem["BZFW_53"] = "----";
                    sfpd1 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_375"]))
                {
                    sfpd2 = GetSafeDouble(sItem["HCJP_375"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_375"] && 100 != GetSafeDouble(sItem["BZFW_375"]))
                {
                    sfpd2 = IsQualified(sItem["BZFW_375"], sItem["HCJP_375"], false);
                }
                else
                {
                    sItem["BZFW_375"] = "----";
                    sfpd2 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_315"]))
                {
                    sfpd3 = GetSafeDouble(sItem["HCJP_315"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_315"] && 100 != GetSafeDouble(sItem["BZFW_315"]))
                {
                    sfpd3 = IsQualified(sItem["BZFW_315"], sItem["HCJP_315"], false);
                }
                else
                {
                    sItem["BZFW_315"] = "----";
                    sfpd3 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_265"]))
                {
                    sfpd4 = GetSafeDouble(sItem["HCJP_265"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_265"] && 100 != GetSafeDouble(sItem["BZFW_265"]))
                {
                    sfpd4 = IsQualified(sItem["BZFW_265"], sItem["HCJP_265"], false);
                }
                else
                {
                    sItem["BZFW_265"] = "----";
                    sfpd4 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_19"]))
                {
                    sfpd5 = GetSafeDouble(sItem["HCJP_19"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_19"] && 100 != GetSafeDouble(sItem["BZFW_19"]))
                {
                    sfpd5 = IsQualified(sItem["BZFW_19"], sItem["HCJP_19"], false);
                }
                else
                {
                    sItem["BZFW_19"] = "----";
                    sfpd5 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_16"]))
                {
                    sfpd6 = GetSafeDouble(sItem["HCJP_16"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_16"] && 100 != GetSafeDouble(sItem["BZFW_16"]))
                {
                    sfpd6 = IsQualified(sItem["BZFW_16"], sItem["HCJP_16"], false);
                }
                else
                {
                    sItem["BZFW_16"] = "----";
                    sfpd6 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_132"]))
                {
                    sfpd7 = GetSafeDouble(sItem["HCJP_132"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_132"] && 100 != GetSafeDouble(sItem["BZFW_132"]))
                {
                    sfpd7 = IsQualified(sItem["BZFW_132"], sItem["HCJP_132"], false);
                }
                else
                {
                    sItem["BZFW_132"] = "----";
                    sfpd7 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_95"]))
                {
                    sfpd8 = GetSafeDouble(sItem["HCJP_95"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_95"] && 100 != GetSafeDouble(sItem["BZFW_95"]))
                {
                    sfpd8 = IsQualified(sItem["BZFW_95"], sItem["HCJP_95"], false);
                }
                else
                {
                    sItem["BZFW_95"] = "----";
                    sfpd8 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_475"]))
                {
                    sfpd9 = GetSafeDouble(sItem["HCJP_475"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_475"] && 100 != GetSafeDouble(sItem["BZFW_475"]))
                {
                    sfpd9 = IsQualified(sItem["BZFW_475"], sItem["HCJP_475"], false);
                }
                else
                {
                    sItem["BZFW_475"] = "----";
                    sfpd9 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_236"]))
                {
                    sfpd10 = GetSafeDouble(sItem["HCJP_236"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_236"] && 100 != GetSafeDouble(sItem["BZFW_236"]))
                {
                    sfpd10 = IsQualified(sItem["BZFW_236"], sItem["HCJP_236"], false);
                }
                else
                {
                    sItem["BZFW_236"] = "----";
                    sfpd10 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_118"]))
                {
                    sfpd11 = GetSafeDouble(sItem["HCJP_118"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_118"] && 100 != GetSafeDouble(sItem["BZFW_118"]))
                {
                    sfpd11 = IsQualified(sItem["BZFW_118"], sItem["HCJP_118"], false);
                }
                else
                {
                    sItem["BZFW_118"] = "----";
                    sfpd11 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_06"]))
                {
                    sfpd12 = GetSafeDouble(sItem["HCJP_06"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_06"] && 100 != GetSafeDouble(sItem["BZFW_06"]))
                {
                    sfpd12 = IsQualified(sItem["BZFW_06"], sItem["HCJP_06"], false);
                }
                else
                {
                    sItem["BZFW_06"] = "----";
                    sfpd12 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_03"]))
                {
                    sfpd13 = GetSafeDouble(sItem["HCJP_03"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_03"] && 100 != GetSafeDouble(sItem["BZFW_03"]))
                {
                    sfpd13 = IsQualified(sItem["BZFW_03"], sItem["HCJP_03"], false);
                }
                else
                {
                    sItem["BZFW_03"] = "----";
                    sfpd13 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW_015"]))
                {
                    sfpd14 = GetSafeDouble(sItem["HCJP_015"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_015"] && 100 != GetSafeDouble(sItem["BZFW_015"]))
                {
                    sfpd14 = IsQualified(sItem["BZFW_015"], sItem["HCJP_015"], false);
                }
                else
                {
                    sItem["BZFW_015"] = "----";
                    sfpd14 = "----";
                }

                if (100 == GetSafeDouble(sItem["BZFW0075"]))
                {
                    sfpd15 = GetSafeDouble(sItem["HCJP_0075"]) == 100 ? "合格" : "不合格";
                }
                else if (null != sItem["BZFW_0075"] && 100 != GetSafeDouble(sItem["BZFW_0075"]))
                {
                    sfpd15 = IsQualified(sItem["BZFW_0075"], sItem["HCJP_0075"], false);
                }
                else
                {
                    sItem["BZFW_0075"] = "----";
                    sfpd15 = "----";
                }
                #endregion

                #region 关键孔判定
                if (gjkBool)
                {
                    //关键性筛孔 4.75 mm
                    if (sItem["KLJPLX"].Contains("AC-25") || sItem["KLJPLX"].Contains("AC-20"))
                    {
                        if (sfpd9 == "合格")
                        {
                            if ("合格" != IsQualified(gjkTgl, sItem["TGBFL475"], false))
                            {
                                sfpd9 = "不合格";
                            }
                        }
                    }
                    //关键性筛孔 2.36 mm
                    else if (sItem["KLJPLX"].Contains("AC-16") || sItem["KLJPLX"].Contains("AC-13") || sItem["KLJPLX"].Contains("AC-10"))
                    {
                        if (sfpd10 == "合格")
                        {
                            if ("合格" != IsQualified(gjkTgl, sItem["TGBFL236"], false))
                            {
                                sfpd10 = "不合格";
                            }
                        }
                    }
                }
                #endregion

                #region 合成级配综合判定
                if (sfpd1 == "不合格" || sfpd2 == "不合格" || sfpd3 == "不合格" || sfpd4 == "不合格" || sfpd5 == "不合格" || sfpd6 == "不合格"
                     || sfpd7 == "不合格" || sfpd8 == "不合格" || sfpd9 == "不合格" || sfpd10 == "不合格" || sfpd11 == "不合格"
                     || sfpd12 == "不合格" || sfpd13 == "不合格" || sfpd14 == "不合格" || sfpd15 == "不合格")
                {
                    sItem["JPDXPD"] = "不符合标准要求";
                }
                else
                {
                    sItem["JPDXPD"] = "符合标准要求";
                }
                #endregion
                #endregion

                #region 马歇尔试验
                double mtjxdmd = 0;
                for (int i = 1; i < 8; i++)
                {
                    if (!string.IsNullOrEmpty(sItem["BL_" + i].Trim()))
                    {
                        mtjxdmd = mtjxdmd + GetSafeDouble(sItem["BL_" + i].Trim()) * GetSafeDouble(sItem["KLXDMD" + i].Trim());
                    }
                }
                //矿料的合成毛体积相对密度
                sItem["KLHCMTJXDMD"] = Round(mtjxdmd, 3).ToString("0.000");
                //矿料的有效的相对密度

                
                #endregion


                #region  原代码
                if (!string.IsNullOrEmpty(sItem["JLMC8"]) && sItem["JLMC8"] != "----" && sItem["JLMC8"] != "")
                {
                    //mItem["WHICH"] = "bgspa、bgspa_4、bgspa_2、bgspa_3";
                    stemp = "";
                    for (int i = 1; i <= 8; i++)
                    {
                        sItem["JLMC" + i] = sItem["JLMC" + i].Trim();
                        if (!string.IsNullOrEmpty(sItem["JLMC" + i]) && sItem["JLMC" + i] != "----" && sItem["JLMC" + i] != "")
                        {
                            if (string.IsNullOrEmpty(sItem["JLGG" + i]))
                            {
                                sItem["JLGG" + i] = "----";
                            }
                            stemp = stemp + sItem["JLMC" + i] + "(" + sItem["JLBH" + i] + "):";
                            stemp = stemp + sItem["JLGG" + i] + " ";
                            stemp = stemp + sItem["JLCD" + i] + ";\r\n";
                        }
                    }
                    stemp = stemp.Substring(0, stemp.Length - 2) + "。";
                    sItem["ZBH"] = stemp;
                    sign = EItem.Count > 11 ? true : false;
                    if (EItem.Count > 0)
                    {
                        double sum = 0;
                        for (int i = 1; i <= 8; i++)
                        {
                            if (IsNumeric(sItem["MTJMD" + i]) && !string.IsNullOrEmpty(sItem["MTJMD" + i]) &&
                                //IsNumeric(EItem[0]["klpb" + i]) && !string.IsNullOrEmpty(EItem[0]["klpb" + i]))
                                IsNumeric(sItem["klpb" + i]) && !string.IsNullOrEmpty(sItem["klpb" + i]))
                            {
                                md1 = GetSafeDouble(sItem["MTJMD" + i]);
                                //md2 = GetSafeDouble(EItem[0]["klpb" + i]);
                                md2 = GetSafeDouble(sItem["KLPB" + i]);
                                md = md2 / md1;
                                sum = sum + md;
                                //sItem["KLPB" + i] = EItem[0]["klpb" + i].Trim();
                            }
                            else
                            {
                                sItem["KLPB" + i] = "";
                            }
                        }
                        md = 100 / sum;
                        md = Round(md, 3);
                        sItem["HC_MTJMD"] = md.ToString();
                        sum = 0;
                        for (int i = 1; i <= 8; i++)
                        {
                            if (IsNumeric(sItem["BGMD" + i]) && !string.IsNullOrEmpty(sItem["BGMD" + i]) &&
                                //IsNumeric(EItem[0]["klpb" + i]) && !string.IsNullOrEmpty(EItem[0]["klpb" + i]))
                                IsNumeric(sItem["KLPB" + i]) && !string.IsNullOrEmpty(sItem["KLPB" + i]))
                            {
                                md1 = GetSafeDouble(sItem["BGMD" + i]);
                                //md2 = GetSafeDouble(EItem[0]["klpb" + i]);
                                md2 = GetSafeDouble(sItem["KLPB" + i]);
                                md = md2 / md1;
                                sum = sum + md;
                            }
                        }
                        md = 100 / sum;
                        md = Round(md, 3);
                        sItem["HC_BGMD"] = md.ToString();
                    }
                    else
                    {
                        sItem["HC_MTJMD"] = "----";
                        sItem["HC_BGMD"] = "----";
                        mSFwc = false;
                    }
                }
                else
                {
                    //mItem["WHICH"] = "bgspa、bgspa_1、bgspa_2、bgspa_3";
                    stemp = "";
                    for (int i = 1; i <= 7; i++)
                    {
                        sItem["JLMC" + i] = sItem["JLMC" + i].Trim();
                        if (!string.IsNullOrEmpty(sItem["JLMC" + i]) && sItem["JLMC" + i] != "----" && sItem["JLMC" + i] != "")
                        {
                            stemp = stemp + sItem["JLMC" + i] + "(" + sItem["JLBH" + i] + "):";
                            stemp = stemp + sItem["JLGG" + i] + " ";
                            stemp = stemp + sItem["JLCD" + i] + ";\r\n";
                        }
                    }
                    stemp = stemp.Substring(0, stemp.Length - 2) + "。";
                    sItem["ZBH"] = stemp;
                    //关于合成的
                    int gs = EItem.Count;
                    sign = gs > 11 ? true : false;
                    if (gs > 0)
                    {
                        double sum = 0;
                        for (int i = 1; i <= 7; i++)
                        {
                            if (IsNumeric(sItem["MTJMD" + i]) && !string.IsNullOrEmpty(sItem["MTJMD" + i]) &&
                                //IsNumeric(EItem[0]["klpb" + i]) && !string.IsNullOrEmpty(EItem[0]["klpb" + i]))
                                IsNumeric(sItem["KLPB" + i]) && !string.IsNullOrEmpty(sItem["KLPB" + i]))
                            {
                                md1 = GetSafeDouble(sItem["MTJMD" + i]);
                                //md2 = GetSafeDouble(EItem[0]["klpb" + i]);
                                md2 = GetSafeDouble(sItem["KLPB" + i]);
                                md = md2 / md1;
                                sum = sum + md;
                                //sItem["KLPB" + i] = EItem[0]["klpb" + i].Trim();
                            }
                            else
                            {
                                sItem["KLPB" + i] = "";
                            }
                        }
                        md = 100 / sum;
                        md = Round(md, 3);
                        sItem["HC_MTJMD"] = Round(md, 3).ToString();
                        sum = 0;
                        for (int i = 1; i <= 7; i++)
                        {
                            if (IsNumeric(sItem["BGMD" + i]) && !string.IsNullOrEmpty(sItem["BGMD" + i]) &&
                                //IsNumeric(EItem[0]["klpb" + i]) && !string.IsNullOrEmpty(EItem[0]["klpb" + i]))
                                IsNumeric(sItem["klpb" + i]) && !string.IsNullOrEmpty(sItem["klpb" + i]))
                            {
                                md1 = GetSafeDouble(sItem["BGMD" + i]);
                                //md2 = GetSafeDouble(EItem[0]["klpb" + i]);
                                md2 = GetSafeDouble(sItem["klpb" + i]);
                                md = md2 / md1;
                                sum = sum + md;
                            }
                        }
                        md = 100 / sum;
                        md = Round(md, 3);
                        sItem["HC_BGMD"] = Round(md, 3).ToString();
                    }
                    else
                    {
                        sItem["HC_MTJMD"] = "----";
                        sItem["HC_BGMD"] = "----";
                        mSFwc = false;
                    }
                }

                //关于沥青含量

                if (mAllHg)
                {
                    sItem["JCJG"] = "合格";
                    mItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    mItem["JCJG"] = "不合格";
                }
                #endregion
            }
            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_SPA"))
            {
                data["M_SPA"] = new List<IDictionary<string, string>>();
            }

            mItem["JCJG"] = mjcjg;
            mItem["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
