using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TBH : BaseMethods
    {
        public void Calc()
        {
            #region   参数定义
            string mgjlb = "";
            string jcjg = "";
            string mjcjg = "";
            var jsbeizhu = "";
            bool mGetBgbh = false;
            bool mAllHg = true;
            bool mMore = false;
            double zj1 = 0;
            double zj2 = 0;
            bool falg_b = true;
            bool falg_l = true;
            bool falg_z = true;
            bool falg_j = true;
            bool sign_l = false;
            bool sign_b = false;
            bool mSFwc = true;
            bool mmp = false;
            bool sign = false;
            bool flag = true;
            int yxpc1 = 0;
            int yxpc2 = 0;
            int sjhd = 0;
            int bgzs = 0;
            bool gjjjpd = true;
            #endregion

            #region  集合取值
            var data = retData;
            var extraDJ = dataExtra["BZ_TBH_DJ"];
            var MItem = data["M_TBH"];
            var SItem = data["S_TBH"];
            //var XQData = data["S_BY_RW_XQ"];
            var jcxmItems = retData.Select(u => u.Key).ToArray();
            #endregion

            #region 计算开始
            //主表赋值
            MItem[0]["ZCGJL"] = "0";
            MItem[0]["ZCGJB"] = "0";
            MItem[0]["ZJGSL"] = "0";
            MItem[0]["ZJGSB"] = "0";
            MItem[0]["ZJCDS"] = "0";
            MItem[0]["ZHGDS"] = "0";
            MItem[0]["ZCCDS"] = "0";
            MItem[0]["LJCDS"] = "0";
            MItem[0]["LHGDS"] = "0";
            MItem[0]["LCCDS"] = "0";
            MItem[0]["BJCDS"] = "0";
            MItem[0]["BHGDS"] = "0";
            MItem[0]["BCCDS"] = "0";
            MItem[0]["QJCDS"] = "0";
            MItem[0]["QHGDS"] = "0";
            MItem[0]["QCCDS"] = "0";
            List<double> gjjjArray = new List<double>();
            //从表循环
            foreach (var sitem in SItem)
            {

                string jcxm = '、' + sitem["JCXM"].Trim().Replace(",", "、") + "、";
                double mmin = 0;
                double mmax = 0;
                double mpj = 0;
                double mhgds = 0;
                double mccds = 0;
                double mcc1_5ds = 0;
                int xd = 0;
                double md = 0;
                double sum = 0;
                double Gs = 0;

                if (jcxm.Contains("、墙柱梁板保护层实测厚度、"))
                {
                    if (Conversion.Val(sitem["ZJGS"]) > 6)
                        mMore = true;
                    if (Conversion.Val(sitem["ZJGS"]) > 12)
                        mmp = true;
                    sign = sitem["SFFJ"] == "是" ? true : false;
                    //原监管中录委托单时只有~符号，数值可能会忘记录入，原代码此处校验
                    //flag = sitem["YXPC"].IndexOf("～") > 0 ? true : false;
                    if (flag)
                    {
                        //yxpc1 = int.Parse(sitem["YXPC"].Substring(0, (sitem["YXPC"].IndexOf("～"))));
                        //yxpc2 = int.Parse(sitem["YXPC"].Substring(sitem["YXPC"].IndexOf("～") + 2));
                        yxpc2 = int.Parse(sitem["YXPC"].Substring(0, (sitem["YXPC"].IndexOf("，"))));
                        yxpc1 = int.Parse(sitem["YXPC"].Substring(sitem["YXPC"].IndexOf("，") + 1));
                    }
                    sitem["SJHD"] = sitem["SJHD"].Replace("mm", "");
                    if (!string.IsNullOrEmpty(sitem["SJHD"]) && IsNumeric(sitem["SJHD"]))
                        sjhd = (int)Convert.ToDecimal(sitem["SJHD"].Trim());
                    else
                        flag = false;
                    if (flag)
                        MItem[0]["BEIZHU2"] = "加*表示超出规范允许偏差，加#表示超出规范最大允许偏差的1.5倍";
                    else
                        MItem[0]["BEIZHU2"] = "----";
                    mmin = 0;
                    mmax = 0;
                    mpj = 0;
                    mhgds = 0;
                    mccds = 0;
                    mcc1_5ds = 0;
                    int Min = 0;
                    sitem["SMDS"] = "0";
                    if (!string.IsNullOrEmpty(sitem["SCHD1"]) && IsNumeric(sitem["SCHD1"]) && sitem["SCHD4"] != "0")
                        sitem["PJHD1"] = Round((GetSafeDouble(sitem["SCHD1"].Trim()) + GetSafeDouble(sitem["SCHD2"].Trim()) + GetSafeDouble(sitem["SCHD3"])) / 3, 0).ToString();
                    else
                        sitem["PJHD1"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD4"]) && IsNumeric(sitem["SCHD4"]) && sitem["SCHD4"] != "0")
                        sitem["PJHD2"] = Round((GetSafeDouble(sitem["SCHD4"].Trim()) + GetSafeDouble(sitem["SCHD5"].Trim()) + GetSafeDouble(sitem["SCHD6"])) / 3, 0).ToString();
                    else
                        sitem["PJHD2"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD7"]) && IsNumeric(sitem["SCHD7"]) && sitem["SCHD7"] != "0")
                        sitem["PJHD3"] = Round((GetSafeDouble(sitem["SCHD7"].Trim()) + GetSafeDouble(sitem["SCHD8"].Trim()) + GetSafeDouble(sitem["SCHD9"])) / 3, 0).ToString();
                    else
                        sitem["PJHD3"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD10"]) && IsNumeric(sitem["SCHD10"]) && sitem["SCHD10"] != "0")
                        sitem["PJHD4"] = Round((GetSafeDouble(sitem["SCHD10"].Trim()) + GetSafeDouble(sitem["SCHD11"].Trim()) + GetSafeDouble(sitem["SCHD12"])) / 3, 0).ToString();
                    else
                        sitem["PJHD4"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD13"]) && IsNumeric(sitem["SCHD13"]) && sitem["SCHD13"] != "0")
                        sitem["PJHD5"] = Round((GetSafeDouble(sitem["SCHD13"].Trim()) + GetSafeDouble(sitem["SCHD14"].Trim()) + GetSafeDouble(sitem["SCHD15"])) / 3, 0).ToString();
                    else
                        sitem["PJHD5"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD16"]) && IsNumeric(sitem["SCHD16"]) && sitem["SCHD16"] != "0")
                        sitem["PJHD6"] = Round((GetSafeDouble(sitem["SCHD16"].Trim()) + GetSafeDouble(sitem["SCHD17"].Trim()) + GetSafeDouble(sitem["SCHD18"])) / 3, 0).ToString();
                    else
                        sitem["PJHD6"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD19"]) && IsNumeric(sitem["SCHD19"]) && sitem["SCHD19"] != "0")
                        sitem["PJHD7"] = Round((GetSafeDouble(sitem["SCHD19"].Trim()) + GetSafeDouble(sitem["SCHD20"].Trim()) + GetSafeDouble(sitem["SCHD21"])) / 3, 0).ToString();
                    else
                        sitem["PJHD7"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD22"]) && IsNumeric(sitem["SCHD22"]) && sitem["SCHD22"] != "0")
                        sitem["PJHD8"] = Round((GetSafeDouble(sitem["SCHD22"].Trim()) + GetSafeDouble(sitem["SCHD23"].Trim()) + GetSafeDouble(sitem["SCHD24"])) / 3, 0).ToString();
                    else
                        sitem["PJHD8"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD25"]) && IsNumeric(sitem["SCHD25"]) && sitem["SCHD25"] != "0")
                        sitem["PJHD9"] = Round((GetSafeDouble(sitem["SCHD25"].Trim()) + GetSafeDouble(sitem["SCHD26"].Trim()) + GetSafeDouble(sitem["SCHD27"])) / 3, 0).ToString();
                    else
                        sitem["PJHD9"] = "----";

                    if (!string.IsNullOrEmpty(sitem["SCHD28"]) && IsNumeric(sitem["SCHD28"]) && sitem["SCHD28"] != "0")
                        sitem["PJHD10"] = Round((GetSafeDouble(sitem["SCHD28"].Trim()) + GetSafeDouble(sitem["SCHD29"].Trim()) + GetSafeDouble(sitem["SCHD30"])) / 3, 0).ToString();
                    else
                        sitem["PJHD10"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD31"]) && IsNumeric(sitem["SCHD31"]) && sitem["SCHD31"] != "0")
                        sitem["PJHD11"] = Round((GetSafeDouble(sitem["SCHD31"].Trim()) + GetSafeDouble(sitem["SCHD32"].Trim()) + GetSafeDouble(sitem["SCHD33"])) / 3, 0).ToString();
                    else
                        sitem["PJHD11"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD34"]) && IsNumeric(sitem["SCHD34"]) && sitem["SCHD34"] != "0")
                        sitem["PJHD12"] = Round((GetSafeDouble(sitem["SCHD34"].Trim()) + GetSafeDouble(sitem["SCHD35"].Trim()) + GetSafeDouble(sitem["SCHD36"])) / 3, 0).ToString();
                    else
                        sitem["PJHD12"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD37"]) && IsNumeric(sitem["SCHD37"]) && sitem["SCHD37"] != "0")
                        sitem["PJHD13"] = Round((GetSafeDouble(sitem["SCHD37"].Trim()) + GetSafeDouble(sitem["SCHD38"].Trim()) + GetSafeDouble(sitem["SCHD39"])) / 3, 0).ToString();
                    else
                        sitem["PJHD13"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD40"]) && IsNumeric(sitem["SCHD40"]) && sitem["SCHD40"] != "0")
                        sitem["PJHD14"] = Round((GetSafeDouble(sitem["SCHD40"].Trim()) + GetSafeDouble(sitem["SCHD41"].Trim()) + GetSafeDouble(sitem["SCHD42"])) / 3, 0).ToString();
                    else
                        sitem["PJHD14"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD43"]) && IsNumeric(sitem["SCHD43"]) && sitem["SCHD43"] != "0")
                        sitem["PJHD15"] = Round((GetSafeDouble(sitem["SCHD43"].Trim()) + GetSafeDouble(sitem["SCHD44"].Trim()) + GetSafeDouble(sitem["SCHD45"])) / 3, 0).ToString();
                    else
                        sitem["PJHD15"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD46"]) && IsNumeric(sitem["SCHD46"]) && sitem["SCHD46"] != "0")
                        sitem["PJHD16"] = Round((GetSafeDouble(sitem["SCHD46"].Trim()) + GetSafeDouble(sitem["SCHD47"].Trim()) + GetSafeDouble(sitem["SCHD48"])) / 3, 0).ToString();
                    else
                        sitem["PJHD16"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD49"]) && IsNumeric(sitem["SCHD49"]) && sitem["SCHD49"] != "0")
                        sitem["PJHD17"] = Round((GetSafeDouble(sitem["SCHD49"].Trim()) + GetSafeDouble(sitem["SCHD50"].Trim()) + GetSafeDouble(sitem["SCHD51"])) / 3, 0).ToString();
                    else
                        sitem["PJHD17"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD52"]) && IsNumeric(sitem["SCHD52"]) && sitem["SCHD52"] != "0")
                        sitem["PJHD18"] = Round((GetSafeDouble(sitem["schd52"].Trim()) + GetSafeDouble(sitem["SCHD53"].Trim()) + GetSafeDouble(sitem["SCHD54"])) / 3, 0).ToString();
                    else
                        sitem["PJHD18"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD55"]) && IsNumeric(sitem["SCHD55"]) && sitem["SCHD55"] != "0")
                        sitem["PJHD19"] = Round((GetSafeDouble(sitem["SCHD55"].Trim()) + GetSafeDouble(sitem["SCHD56"].Trim()) + GetSafeDouble(sitem["SCHD57"])) / 3, 0).ToString();
                    else
                        sitem["PJHD19"] = "----";
                    if (!string.IsNullOrEmpty(sitem["SCHD58"]) && IsNumeric(sitem["SCHD58"]) && sitem["SCHD58"] != "0")
                        sitem["PJHD20"] = Round((GetSafeDouble(sitem["SCHD58"].Trim()) + GetSafeDouble(sitem["SCHD59"].Trim()) + GetSafeDouble(sitem["SCHD60"])) / 3, 0).ToString();
                    else
                        sitem["PJHD20"] = "----";

                    for (int i = 1; i <= 20; i++)
                    {
                        if (!string.IsNullOrEmpty(sitem["PJHD" + i]) && IsNumeric(sitem["PJHD" + i]))
                        {
                            sitem["SMDS"] = (GetSafeDouble(sitem["SMDS"]) + 3).ToString();
                            md = GetSafeDouble(sitem["PJHD" + i].Trim());
                            mmin = mmin == 0 ? md : mmin;
                            mmin = Min > md ? md : mmin;
                            mmax = mmax < md ? md : mmax;
                            sum = sum + md;
                            Gs = Gs + 1;
                            sitem["SFCC" + i] = sitem["PJHD" + i].Trim();
                            if (flag)
                            {
                                if (md >= sjhd + yxpc1 && md <= sjhd + yxpc2)
                                    mhgds = mhgds + 3;
                                else
                                {
                                    mccds = mccds + 3;
                                    sitem["SFCC" + i] = sitem["PJHD" + i].Trim() + "*";
                                }
                                if (md < sjhd + 1.5 * yxpc1 || md > sjhd + 1.5 * yxpc2)
                                {
                                    mcc1_5ds = mcc1_5ds + 1;
                                    sitem["SFCC" + i] = sitem["PJHD" + i].Trim() + "#";
                                    if (sitem["BHCLB"].Contains("*梁*"))
                                        falg_l = false;
                                    if (sitem["BHCLB"].Contains("*板*"))
                                        falg_b = false;
                                }
                            }
                        }
                        else
                            sitem["SFCC" + i] = "----";
                    }
                    if (Gs != 0)
                        mpj = Round(sum / Gs, 0);
                    sitem["SCMIN"] = mmin.ToString();
                    sitem["SCMAX"] = mmax.ToString();
                    sitem["SCPJ"] = mpj.ToString();
                    if (flag)
                    {
                        if (sitem["BHCLB"].Contains("柱"))
                        {
                            MItem[0]["zjcds"] = (GetSafeDouble(MItem[0]["ZJCDS"]) + GetSafeDouble(sitem["SMDS"])).ToString();
                            MItem[0]["ZHGDS"] = (GetSafeDouble(MItem[0]["ZHGDS"]) + mhgds).ToString();
                            MItem[0]["ZCCDS"] = (GetSafeDouble(MItem[0]["ZCCDS"]) + mcc1_5ds).ToString();
                        }
                        if (sitem["BHCLB"].Contains("梁"))
                        {
                            MItem[0]["LJCDS"] = (GetSafeDouble(MItem[0]["LJCDS"]) + GetSafeDouble(sitem["SMDS"])).ToString();
                            MItem[0]["LHGDS"] = (GetSafeDouble(MItem[0]["LHGDS"]) + mhgds).ToString();
                            MItem[0]["LCCDS"] = (GetSafeDouble(MItem[0]["LCCDS"]) + mcc1_5ds).ToString();
                            MItem[0]["ZCGJL"] = (GetSafeDouble(MItem[0]["ZCGJL"]) + 1).ToString();
                            MItem[0]["ZJGSL"] = (GetSafeDouble(MItem[0]["ZJGSL"]) + GetSafeDouble(sitem["ZJGS"])).ToString();
                        }
                        if (sitem["BHCLB"].Contains("剪力墙板"))
                        {
                            MItem[0]["QJCDS"] = (GetSafeDouble(MItem[0]["QJCDS"]) + GetSafeDouble(sitem["SMDS"])).ToString();
                            MItem[0]["QHGDS"] = (GetSafeDouble(MItem[0]["QHGDS"]) + mhgds).ToString();
                            MItem[0]["QCCDS"] = (GetSafeDouble(MItem[0]["QCCDS"]) + mcc1_5ds).ToString();
                        }
                        if (sitem["BHCLB"].Contains("板"))
                        {
                            MItem[0]["BJCDS"] = (GetSafeDouble(MItem[0]["BJCDS"]) + GetSafeDouble(sitem["smds"])).ToString();
                            MItem[0]["BHGDS"] = (GetSafeDouble(MItem[0]["BHGDS"]) + mhgds).ToString();
                            MItem[0]["BCCDS"] = (GetSafeDouble(MItem[0]["BCCDS"]) + mcc1_5ds).ToString();
                            MItem[0]["ZCGJB"] = (GetSafeDouble(MItem[0]["ZCGJB"]) + 1).ToString();
                            MItem[0]["ZJGSB"] = (GetSafeDouble(MItem[0]["ZJGSB"]) + GetSafeDouble(sitem["zjgs"])).ToString();
                        }
                    }
                    bgzs = bgzs + 1;
                    //综合判断
                    if (GetSafeDouble(MItem[0]["ZJCDS"]) != 0)
                    {
                        MItem[0]["ZHGL"] = Round((GetSafeDouble(MItem[0]["ZHGDS"]) / GetSafeDouble(MItem[0]["ZJCDS"])) * 100, 1).ToString();
                        MItem[0]["ZCCL"] = Round((GetSafeDouble(MItem[0]["ZCCDS"]) / GetSafeDouble(MItem[0]["ZJCDS"])) * 100, 1).ToString();
                    }
                    if (GetSafeDouble(MItem[0]["LJCDS"]) != 0)
                    {
                        MItem[0]["LHGL"] = Round((GetSafeDouble(MItem[0]["LHGDS"]) / GetSafeDouble(MItem[0]["LJCDS"])) * 100, 1).ToString();
                        MItem[0]["LCCL"] = Round((GetSafeDouble(MItem[0]["LCCDS"]) / GetSafeDouble(MItem[0]["LJCDS"])) * 100, 1).ToString();
                    }
                    if (GetSafeDouble(MItem[0]["BJCDS"]) != 0)
                    {
                        MItem[0]["BHGL"] = Round((GetSafeDouble(MItem[0]["BHGDS"]) / GetSafeDouble(MItem[0]["BJCDS"])) * 100, 1).ToString();
                        MItem[0]["BCCL"] = Round((GetSafeDouble(MItem[0]["BCCDS"]) / GetSafeDouble(MItem[0]["BJCDS"])) * 100, 1).ToString();
                    }
                    if (GetSafeDouble(MItem[0]["QJCDS"]) != 0)
                    {
                        MItem[0]["QHGL"] = Round((GetSafeDouble(MItem[0]["QHGDS"]) / GetSafeDouble(MItem[0]["QJCDS"])) * 100, 1).ToString();
                        MItem[0]["QCCL"] = Round((GetSafeDouble(MItem[0]["QCCDS"]) / GetSafeDouble(MItem[0]["QJCDS"])) * 100, 1).ToString();
                    }
                    MItem[0]["ZGJHGL"] = MItem[0]["ZHGL"];
                    MItem[0]["LGJHGL"] = MItem[0]["LHGL"];
                    MItem[0]["BGJHGL"] = MItem[0]["BHGL"];
                    MItem[0]["QGJHGL"] = MItem[0]["QHGL"];
                    if (GetSafeDouble(MItem[0]["ZJCDS"]) + GetSafeDouble(MItem[0]["LJCDS"]) + GetSafeDouble(MItem[0]["BJCDS"]) + GetSafeDouble(MItem[0]["QJCDS"]) != 0)
                        MItem[0]["ZTHGL"] = Round((GetSafeDouble(MItem[0]["ZHGDS"]) + GetSafeDouble(MItem[0]["LHGDS"]) + GetSafeDouble(MItem[0]["BHGDS"]) + GetSafeDouble(MItem[0]["QHGDS"])) / (GetSafeDouble(MItem[0]["ZJCDS"]) + GetSafeDouble(MItem[0]["LJCDS"]) + GetSafeDouble(MItem[0]["BJCDS"]) + GetSafeDouble(MItem[0]["QJCDS"])) * 100, 1).ToString();
                    mAllHg = true;
                    if (!sign)
                    {
                        if (falg_b && !falg_l)
                        {
                            if (GetSafeDouble(MItem[0]["BHGL"]) >= 90)
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_B"] = "合格";
                                MItem[0]["JCJG_L"] = "不合格";
                                jsbeizhu = "以上所检项中，梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                            }
                            else if (GetSafeDouble(MItem[0]["BHGL"]) >= 80)
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_B"] = "复试";
                                MItem[0]["JCJG_L"] = "不合格";
                                jsbeizhu = "以上所检项中，梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                            }
                            else
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_B"] = "不合格";
                                MItem[0]["JCJG_L"] = "不合格";
                                //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            }
                        }
                        else if (!falg_b && falg_l)
                        {
                            if (GetSafeDouble(MItem[0]["LHGL"]) >= 90)
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_B"] = "合格";
                                MItem[0]["JCJG_L"] = "不合格";
                                jsbeizhu = "以上所检项中，板不符合" + MItem[0]["PDBZ"] + "标准要求";
                            }
                            else if (GetSafeDouble(MItem[0]["BHGL"]) >= 80)
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_B"] = "复试";
                                MItem[0]["JCJG_L"] = "不合格";
                                //jsbeizhu = "以上所检项中，板不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            }
                            else
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_B"] = "不合格";
                                MItem[0]["JCJG_L"] = "不合格";
                                //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            }
                        }
                        else if (!falg_b && !falg_l)
                        {
                            mAllHg = false;
                            MItem[0]["JCJG_B"] = "不合格";
                            MItem[0]["JCJG_L"] = "不合格";
                            //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                        }
                        else
                        {
                            if (GetSafeDouble(MItem[0]["ZCGJB"]) > 0 && GetSafeDouble(MItem[0]["ZCGJL"]) > 0)
                            {
                                if (GetSafeDouble(MItem[0]["BHGL"]) >= 90 && GetSafeDouble(MItem[0]["LHGL"]) >= 90)
                                {
                                    mAllHg = true;
                                    MItem[0]["JCJG_B"] = "合格";
                                    MItem[0]["JCJG_L"] = "合格";
                                    //jsbeizhu = "以上所检项符合" + MItem[0]["PDBZ"] + "标准要求。";
                                }
                                else if (GetSafeDouble(MItem[0]["BHGL"]) >= 90 && GetSafeDouble(MItem[0]["LHGL"]) >= 80)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "合格";
                                    MItem[0]["JCJG_L"] = "复试";
                                    jsbeizhu = "以上所检项中，梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                                else if (GetSafeDouble(MItem[0]["BHGL"]) >= 80 && GetSafeDouble(MItem[0]["LHGL"]) >= 90)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "复试";
                                    MItem[0]["JCJG_L"] = "合格";
                                    jsbeizhu = "以上所检项中，板不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                                else if (GetSafeDouble(MItem[0]["BHGL"]) >= 80 && GetSafeDouble(MItem[0]["LHGL"]) >= 80)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "复试";
                                    MItem[0]["JCJG_L"] = "复试";
                                    jsbeizhu = "以上所检项中，板、梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                                else if (GetSafeDouble(MItem[0]["BHGL"]) < 80 && GetSafeDouble(MItem[0]["LHGL"]) >= 90)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "不合格";
                                    MItem[0]["JCJG_L"] = "合格";
                                    jsbeizhu = "以上所检项中，板不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                                else if (GetSafeDouble(MItem[0]["BHGL"]) < 80 && GetSafeDouble(MItem[0]["LHGL"]) >= 80)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "不合格";
                                    MItem[0]["JCJG_L"] = "复试";

                                    jsbeizhu = "以上所检项中，板、梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                                else if (GetSafeDouble(MItem[0]["BHGL"]) >= 90 && GetSafeDouble(MItem[0]["LHGL"]) < 80)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "合格";
                                    MItem[0]["JCJG_L"] = "不合格";
                                    jsbeizhu = "以上所检项中，梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                                else if (GetSafeDouble(MItem[0]["BHGL"]) >= 80 && GetSafeDouble(MItem[0]["LHGL"]) < 80)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "复试";
                                    MItem[0]["JCJG_L"] = "不合格";
                                    jsbeizhu = "以上所检项中，板、梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                                else
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "不合格";
                                    MItem[0]["JCJG_L"] = "不合格";
                                    //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                                }
                            }
                            else if (GetSafeDouble(MItem[0]["ZCGJB"]) == 0)
                            {
                                if (GetSafeDouble(MItem[0]["LHGL"]) >= 90)
                                {
                                    mAllHg = true;
                                    MItem[0]["JCJG_B"] = "----";
                                    MItem[0]["JCJG_L"] = "合格";
                                    //jsbeizhu = "以上所检项符合" + MItem[0]["PDBZ"] + "标准要求。";
                                }
                                else if (GetSafeDouble(MItem[0]["LHGL"]) >= 80)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "----";
                                    MItem[0]["JCJG_L"] = "复试";
                                    jsbeizhu = "以上所检项中，梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                                else if (GetSafeDouble(MItem[0]["LHGL"]) < 80)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "----";
                                    MItem[0]["JCJG_L"] = "不合格";
                                    jsbeizhu = "以上所检项中，梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                            }
                            else if (GetSafeDouble(MItem[0]["ZCGJL"]) == 0)
                            {
                                if (GetSafeDouble(MItem[0]["BHGL"]) >= 90)
                                {
                                    mAllHg = true;
                                    MItem[0]["JCJG_L"] = "----";
                                    MItem[0]["JCJG_B"] = "合格";
                                }
                                else if (GetSafeDouble(MItem[0]["BHGL"]) >= 80)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_L"] = "----";
                                    MItem[0]["JCJG_B"] = "复试";
                                    jsbeizhu = "以上所检项中，板不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                                else if (GetSafeDouble(MItem[0]["BHGL"]) < 80)
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_L"] = "----";
                                    MItem[0]["JCJG_B"] = "不合格";
                                    jsbeizhu = "以上所检项中，板不符合" + MItem[0]["PDBZ"] + "标准要求";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (falg_b && !falg_l)
                        {
                            if (GetSafeDouble(MItem[0]["BHGL"]) >= 90)
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_B"] = "合格";
                                MItem[0]["JCJG_L"] = "不合格";
                                jsbeizhu = "以上所检项中，梁不符合" + MItem[0]["PDBZ"] + "标准要求";
                            }
                            else
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_B"] = "不合格";
                                MItem[0]["JCJG_L"] = "不合格";
                                //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            }
                        }
                        else if (!falg_b && falg_l)
                        {
                            if (GetSafeDouble(MItem[0]["LHGL"]) >= 90)
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_L"] = "合格";
                                MItem[0]["JCJG_B"] = "不合格";
                                //jsbeizhu = "以上所检项中，板不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            }
                            else
                            {
                                mAllHg = false;
                                MItem[0]["JCJG_B"] = "不合格";
                                MItem[0]["JCJG_L"] = "不合格";
                                //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            }
                        }
                        else if (!falg_b && !falg_l)
                        {
                            mAllHg = false;
                            MItem[0]["JCJG_B"] = "不合格";
                            MItem[0]["JCJG_L"] = "不合格";
                            //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                        }
                        else
                        {
                            if (GetSafeDouble(MItem[0]["BJCDS"]) == 0)
                            {
                                if (GetSafeDouble(MItem[0]["LHGL"]) >= 90)
                                {
                                    mAllHg = true;
                                    MItem[0]["JCJG_B"] = "合格";
                                    MItem[0]["JCJG_L"] = "合格";
                                    //jsbeizhu = "以上所检项符合" + MItem[0]["PDBZ"] + "标准要求。";
                                }
                                else
                                {
                                    mAllHg = true;
                                    MItem[0]["JCJG_B"] = "不合格";
                                    MItem[0]["JCJG_L"] = "不合格";
                                    //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                                }
                            }
                            if (GetSafeDouble(MItem[0]["LJCDS"]) == 0)
                            {
                                if (GetSafeDouble(MItem[0]["BHGL"]) >= 90)
                                {
                                    mAllHg = true;
                                    MItem[0]["JCJG_B"] = "合格";
                                    MItem[0]["JCJG_L"] = "合格";
                                    //jsbeizhu = "以上所检项符合" + MItem[0]["PDBZ"] + "标准要求。";
                                }
                                else
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "不合格";
                                    MItem[0]["JCJG_L"] = "不合格";
                                    //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                                }
                            }
                            if (GetSafeDouble(MItem[0]["LJCDS"]) != 0 && GetSafeDouble(MItem[0]["BJCDS"]) != 0)
                            {
                                if (GetSafeDouble(MItem[0]["BHGL"]) >= 90 && GetSafeDouble(MItem[0]["LHGL"]) >= 90)
                                {
                                    mAllHg = true;
                                    MItem[0]["JCJG_B"] = "合格";
                                    MItem[0]["JCJG_L"] = "合格";
                                    //jsbeizhu = "以上所检项符合" + MItem[0]["PDBZ"] + "标准要求。";
                                }
                                else
                                {
                                    mAllHg = false;
                                    MItem[0]["JCJG_B"] = "不合格";
                                    MItem[0]["JCJG_L"] = "不合格";
                                    //jsbeizhu = "以上所检项不符合" + MItem[0]["PDBZ"] + "标准要求。";
                                }
                            }
                        }
                    }
                    jsbeizhu = flag ? jsbeizhu : "各构件实测结果见后页。";
                    string mjgsm = string.Empty;
                    mjgsm = flag ? mjgsm : "各构件实测结果见后页。";

                }

                if (jcxm.Contains("、钢筋间距、"))
                {
                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u["JCBZ"] == "GB 50204-2015");
                    gjjjArray.Add(double.Parse(sitem["GJJJ1"]));//间距1
                    gjjjArray.Add(double.Parse(sitem["GJJJ2"]));//间距2
                    gjjjArray.Add(double.Parse(sitem["GJJJ3"]));//间距3
                    gjjjArray.Sort();
                    double gjjjMax = gjjjArray[0];
                    double gjjjSmall = gjjjArray[2];
                    double gjjjpc = gjjjMax - gjjjSmall;//最大间距偏差
                    sitem["GJJJ_ZDPC"] = Math.Abs(gjjjpc).ToString();
                    if (gjjjpc <= double.Parse(extraFieldsDj["GJJJ"]))
                    {
                        MItem[0]["HG_GJJJ"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_GJJJ"] = "不合格";
                        mAllHg = false;
                        gjjjpd = false;
                    }

                }

            }

            //主表总判断赋值
            if (mAllHg)
            {
                mjcjg = "合格";
                jsbeizhu = "以上所检项符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                if (GetSafeDouble(MItem[0]["ZCGJB"]) < 0 || GetSafeDouble(MItem[0]["ZCGJL"]) <= 0 && !gjjjpd)
                {
                    jsbeizhu = jsbeizhu + "，钢筋间距不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "以上所检项部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
        }
    }
}
