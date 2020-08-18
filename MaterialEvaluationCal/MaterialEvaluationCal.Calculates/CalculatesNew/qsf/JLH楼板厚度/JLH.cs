using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JLH : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            //var extraDJ = dataExtra["BZ_KZD_DJ"];
            var data = retData;

            var SItem = data["S_JLH"];
            var MItem = data["M_JLH"];
            bool sign = true;
            var jcxmBhg = "";
            var jcxmCur = "";
            int zs = 0;
            zs = SItem.Count;

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim() && u["D_JB"] == sItem["JB"]);
                //if (null != extraFieldsDj)
                //{
                //    sItem["G_MD"] = string.IsNullOrEmpty(extraFieldsDj["D_MD"]) ? extraFieldsDj["D_MD"] : extraFieldsDj["D_MD"].Trim();
                //    sItem["G_HXZS7"] = string.IsNullOrEmpty(extraFieldsDj["D_HXXS7"]) ? extraFieldsDj["D_HXXS7"] : extraFieldsDj["D_HXXS7"].Trim();
                //    sItem["G_HXZS28"] = string.IsNullOrEmpty(extraFieldsDj["D_HXXS28"]) ? extraFieldsDj["D_HXXS28"] : extraFieldsDj["D_HXXS28"].Trim();
                //    sItem["G_LDDB"] = string.IsNullOrEmpty(extraFieldsDj["D_LDDB"]) ? extraFieldsDj["D_LDDB"] : extraFieldsDj["D_LDDB"].Trim();
                //    sItem["G_HSL"] = string.IsNullOrEmpty(extraFieldsDj["D_HSL"]) ? extraFieldsDj["D_HSL"] : extraFieldsDj["D_HSL"].Trim();
                //    sItem["G_SSL"] = string.IsNullOrEmpty(extraFieldsDj["D_SSL"]) ? extraFieldsDj["D_SSL"] : extraFieldsDj["D_SSL"].Trim();
                //    sItem["G_BBMJ"] = string.IsNullOrEmpty(extraFieldsDj["D_BBMJ"]) ? extraFieldsDj["D_BBMJ"] : extraFieldsDj["D_BBMJ"].Trim();
                //}
                //else
                //{
                //    mAllHg = false;
                //    mjcjg = "不下结论";
                //    jsbeizhu = jsbeizhu + "依据不详";
                //    continue;
                //}

                #region 厚度
                if (jcxm.Contains("、厚度、"))
                {
                    jcxmCur = "厚度";
                    double mmin = 0, mmax = 0, mpj = 0, zmhgds = 0, mcc1_5ds = 0;
                    int mhgds = 0, mccds = 0;

                    sItem["SMDS"] = "0";
                    for (int i = 1; i <= 10; i++)
                    {
                        if (string.IsNullOrEmpty(sItem["SCHD" + i]) || Conversion.Val(sItem["SCHD" + i]) == 0)
                        {

                        }
                        else
                        {
                            sItem["SMDS"] = (Conversion.Val(sItem["SMDS"]) + 1).ToString();
                            sItem["SFCC" + i] = "";
                            if (Conversion.Val(sItem["SCHD" + i]) <= Conversion.Val(sItem["SJHD"]) + Conversion.Val(sItem["YXPC2"]) && Conversion.Val(sItem["SCHD" + i]) >= Conversion.Val(sItem["SJHD"]) + Conversion.Val(sItem["YXPC1"]))
                            {
                                mhgds = mhgds + 1;
                            }
                            else
                            {
                                sItem["SFCC" + i] = "*";
                                mccds = mccds + 1;
                                if (Conversion.Val(sItem["SCHD" + i]) - Conversion.Val(sItem["SJHD"]) > 1.5 * Conversion.Val(sItem["YXPC2"]) || Conversion.Val(sItem["SCHD" + i]) - Conversion.Val(sItem["SJHD"]) < 1.5 * Conversion.Val(sItem["YXPC1"]))
                                {
                                    mcc1_5ds = mcc1_5ds + 1;
                                    sItem["SFCC" + i] = "*";
                                }
                            }
                            if (MItem[0]["JCYJ"].Contains("50204"))
                            {
                                sItem["SFCC" + i] = "";
                            }
                            if (mmin == 0)
                            {
                                mmin = Conversion.Val(sItem["SCHD" + i]);
                            }
                            else
                            {
                                if (mmin > Conversion.Val(sItem["SCHD" + i]))
                                {
                                    mmin = Conversion.Val(sItem["SCHD" + i]);
                                }
                            }
                            if (mmax < Conversion.Val(sItem["SCHD" + i]))
                            {
                                mmax = Conversion.Val(sItem["SCHD" + i]);
                            }
                            mpj = mpj + Conversion.Val(sItem["SCHD" + i]);
                        }
                    }

                    mpj = Math.Round(mpj / Conversion.Val(sItem["SMDS"]), 0);
                    sItem["SCMIN"] = mmin.ToString();
                    sItem["SCMAX"] = mmax.ToString();
                    sItem["SCPJ"] = mpj.ToString();

                    if (MItem[0]["JCYJ"].Contains("50204"))
                    {
                        if (Conversion.Val(sItem["SCPJ"]) <= Conversion.Val(sItem["SJHD"]) + Conversion.Val(sItem["YXPC2"]) && Conversion.Val(sItem["SCPJ"]) >= Conversion.Val(sItem["SJHD"]) + Conversion.Val(sItem["YXPC1"]))
                        {
                            zmhgds = zmhgds + 1;
                            sItem["JCJG"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["JCJG"] = "不合格";
                            mAllHg = false;
                        }
                        MItem[0]["ZJCDS"] = zs.ToString();
                        MItem[0]["ZHGDS"] = (Conversion.Val(MItem[0]["ZHGDS"]) + zmhgds).ToString();
                    }
                    else
                    {
                        MItem[0]["ZJCDS"] = (Conversion.Val(MItem[0]["ZJCDS"] + Conversion.Val(sItem["SMDS"]))).ToString();
                        MItem[0]["ZHGDS"] = (Conversion.Val(MItem[0]["ZHGDS"]) + mhgds).ToString();
                    }
                }
                else
                {

                }
                #endregion
            }

            if (Conversion.Val(MItem[0]["ZJCDS"]) != 0)
            {
                MItem[0]["ZHGL"] = Math.Round(Conversion.Val(MItem[0]["ZHGDS"]) / Conversion.Val(MItem[0]["ZJCDS"]) * 100, 1).ToString();
                MItem[0]["ZTHGL"] = MItem[0]["ZHGL"];
            }
            MItem[0]["ZGJHGL"] = MItem[0]["ZHGL"];
            //MItem[0]["JSBEIZHU"] = "";

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
        }

    }
}
