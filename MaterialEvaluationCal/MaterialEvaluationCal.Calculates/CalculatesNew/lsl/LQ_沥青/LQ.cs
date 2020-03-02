using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class LQ : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_LQ"];
            var MItem = data["M_LQ"];
            var mrsDj = dataExtra["BZ_LQ_DJ"];
            int mbHggs = 0;
            if (!data.ContainsKey("M_LQ"))
            {
                data["M_LQ"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            string mJSFF;
            bool sign, mFlag_Bhg = false, mFlag_Hg = false;
            string BHGXM = "", Hgxm = "";

            foreach (var sItem in SItem)
            {
                double md1, md2, pjmd, md, sum;
                var mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == sItem["SJDJ"]);
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if (mrsdj != null)
                {
                    sItem["G_ZRD"] = mrsdj["ZRD"].Trim();
                    sItem["G_RHD"] = mrsdj["RHD"].Trim();
                    sItem["G_YD"] = mrsdj["YD"].Trim();
                    sItem["G_RJD"] = mrsdj["RJD"].Trim();
                }
                else
                {
                    mJSFF = "";
                    mItem["bgbh"] = "";
                    sItem["JCJG"] = "依据不详";
                    jsbeizhu = jsbeizhu + "单组流水号:" + sItem["dzbh"] + "试件尺寸为空" + "\r\n ";
                    continue;
                }

                if (jcxm.Contains("、针入度、"))
                {
                    //sItem["TJ_ZRD"] = sItem["SY_ZRD"];
                    sign = true;
                    sItem["HG_ZRD"] = IsQualified(sItem["G_ZRD"], sItem["W_ZRD"], false);
                    if (sItem["HG_ZRD"] == "不合格")
                    {
                        mAllHg = false;
                        BHGXM = BHGXM + "、针入度";
                        mbHggs = mbHggs + 1;
                        mFlag_Bhg = true;
                    }
                    if (sItem["HG_ZRD"] == "合格")
                    {
                        Hgxm = Hgxm + "、针入度";
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["HG_ZRD"] = "----";
                    sItem["G_ZRD"] = "----";
                    sItem["W_ZRD"] = "----";
                    sItem["B_ZRD"] = "----";
                    //sItem["TJ_ZRD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、软化点、"))
                {
                    sign = IsNumeric(sItem["RHD1"]) && !string.IsNullOrEmpty(sItem["RHD1"]) ? sign : false;
                    sign = IsNumeric(sItem["RHD2"]) && !string.IsNullOrEmpty(sItem["RHD2"]) ? sign : false;
                    if (sign)
                    {
                        //sItem["TJ_RHD"] = sItem["SY_RHD"];
                        sum = 0;
                        for (int i = 1;i <= 2;i ++)
                        {
                            md = Conversion.Val(sItem["RHD" + i]);
                            sum = md + sum;
                        }
                        pjmd = sum / 2 / 5;
                        if ("蒸馏水" == sItem["JRJZ"])
                        {
                            pjmd = Round(pjmd, 1);
                            pjmd = pjmd * 5;
                            sItem["W_RHD"] = Round(pjmd, 1).ToString();
                        }
                        else
                        {
                            pjmd = Round(pjmd, 0);
                            pjmd = pjmd * 5;
                            sItem["W_RHD"] = Round(pjmd, 0).ToString();
                        }
                        sItem["HG_RHD"] = IsQualified(sItem["G_RHD"], sItem["W_RHD"], false);
                        if (sItem["HG_RHD"] == "不合格")
                        {
                            mAllHg = false;
                            BHGXM = BHGXM + "、软化点";
                            mbHggs = mbHggs + 1;
                            mFlag_Bhg = true;
                        }
                        if (sItem["HG_RHD"] == "合格")
                        {
                            Hgxm = Hgxm + "、软化点";
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sign = false;
                }

                if (!sign)
                {
                    //sItem["TJ_RHD"] = "----";
                    sItem["HG_RHD"] = "----";
                    sItem["G_RHD"] = "----";
                    sItem["W_RHD"] = "----";
                    sItem["B_RHD"] = "----";
                }
                sign = true;
                if (jcxm.Contains("、延度、"))
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        sign = IsNumeric(sItem["YD" + i]) & !string.IsNullOrEmpty(sItem["YD" + i]) ? sign : false;
                    }
                    if (sign)
                    {
                        //sItem["TJ_YD"] = sItem["SY_YD"];
                        sum = 0;
                        for (int i = 1; i <= 3; i++)
                        {
                            md = Conversion.Val(sItem["YD" + i]);
                            sum = md + sum;
                        }
                        pjmd = sum / 3;
                        pjmd = Round(pjmd, 0);
                        sItem["W_YD"] = pjmd.ToString();
                        string yd = IsQualified(sItem["G_YD"], sItem["W_YD"], false);
                        sItem["HG_YD"] = yd;
                        sItem["W_YD"] = pjmd > 100 ? "＞100" : sItem["W_YD"];
                        if (sItem["HG_YD"] == "不合格")
                        {
                            mAllHg = false;
                            BHGXM = BHGXM + "、延度";
                            mbHggs = mbHggs + 1;
                            mFlag_Bhg = true;
                        }
                        if (sItem["HG_YD"] == "合格")
                        {
                            Hgxm = Hgxm + "、延度";
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sign = false;
                }

                if (!sign)
                {
                    sItem["HG_YD"] = "----";
                    sItem["G_YD"] = "----";
                    sItem["W_YD"] = "----";
                    sItem["B_YD"] = "----";
                   // sItem["TJ_YD"] = "----";
                }

                if (jcxm.Contains("、溶解度、"))
                {
                    sItem["HG_RJD"] = IsQualified(sItem["G_RJD"], sItem["W_RJD"], false);
                    if (sItem["HG_RJD"] == "不合格")
                    {
                        mAllHg = false;
                        BHGXM = BHGXM + "、溶解度";
                        mbHggs = mbHggs + 1;
                        mFlag_Bhg = true;
                    }
                    if (sItem["HG_RJD"] == "合格")
                    {
                        Hgxm = Hgxm + "、溶解度";
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["HG_RJD"] = "----";
                    sItem["G_RJD"] = "----";
                    sItem["W_RJD"] = "----";
                    sItem["B_RJD"] = "----";
                }

                if (jcxm.Contains("、黏附性、"))
                {
                    sItem["HG_NFX"] = IsQualified(sItem["G_NFX"], sItem["W_NFX"], false);
                    if (sItem["HG_NFX"] == "不合格")
                    {
                        mAllHg = false;
                        BHGXM = BHGXM + "、黏附性";
                        mbHggs = mbHggs + 1;
                        mFlag_Bhg = true;
                    }
                    if (sItem["HG_NFX"] == "合格")
                    {
                        Hgxm = Hgxm + "、黏附性";
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["HG_NFX"] = "----";
                    sItem["G_NFX"] = "----";
                    sItem["W_NFX"] = "----";
                    sItem["B_NFX"] = "----";
                }

                if (jcxm.Contains("、闪点、"))
                {
                    sItem["HG_SD"] = IsQualified(sItem["G_SD"], sItem["W_SD"], false);
                    if (sItem["HG_SD"] == "不合格")
                    {
                        mAllHg = false;
                        BHGXM = BHGXM + "、闪点";
                        mbHggs = mbHggs + 1;
                        mFlag_Bhg = true;
                    }
                    if (sItem["HG_SD"] == "合格")
                    {
                        Hgxm = Hgxm + "、闪点";
                        mFlag_Hg = true;
                    }
                }
                else
                {
                    sItem["HG_SD"] = "----";
                    sItem["G_SD"] = "----";
                    sItem["W_SD"] = "----";
                    sItem["B_SD"] = "----";
                }

                if (jcxm.Contains("、密度、"))
                {
                    if (IsQualified(sItem["G_MD"],sItem["W_MD"],false) == "不合格" || IsQualified(sItem["G_XDMD"], sItem["W_XDMD"], false) == "不合格")
                    {
                        mAllHg = false;
                        sItem["HG_MD"] = "不合格";
                        BHGXM = BHGXM + "、密度";
                        mbHggs = mbHggs + 1;
                        mFlag_Bhg = true;
                    }
                    else
                    {
                        if (IsQualified(sItem["G_MD"], sItem["W_MD"], false) == "----" || IsQualified(sItem["G_XDMD"], sItem["W_XDMD"], false) == "----")
                        {
                            sItem["HG_MD"] = "----";
                        }
                        else
                        {
                            sItem["HG_MD"] = "合格";
                            Hgxm = Hgxm + "、密度";
                            mFlag_Hg = true;
                        }
                    }
                }
                else
                {
                    sItem["HG_MD"] = "----";
                    sItem["G_MD"] = "----";
                    sItem["W_MD"] = "----";
                    sItem["G_XDMD"] = "----";
                    sItem["W_XDMD"] = "----";
                    sItem["B_MD"] = "----";
                }
                //sItem["TJ_RJD"] = "----";
                //sItem["TJ_MD"] = "----";

                if(mbHggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                else
                {
                    sItem["JCJG"] = "合格";
                }
                jsbeizhu = mbHggs == 0 ? "该组样品符合" + mItem["PDBZ"] + "标准要求。" : "该组样品不符合" + mItem["PDBZ"] + "标准要求。";
                if (mFlag_Bhg & mFlag_Hg)
                {
                    jsbeizhu = "该组样品部分符合" + mItem["PDBZ"] + "标准要求。";
                }
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
