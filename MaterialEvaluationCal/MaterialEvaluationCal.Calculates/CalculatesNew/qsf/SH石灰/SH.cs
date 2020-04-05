using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SH : BaseMethods
    {
        public void Calc()
        {

            #region
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            bool mGetBgbh = false;
            int mbhggs = 0;//不合格数量
            string mCpmc = "";
            var extraDJ = dataExtra["BZ_SH_DJ"];
            var data = retData;

            var SItem = data["S_SH"];
            var MItem = data["M_SH"];
            if (!data.ContainsKey("M_SH"))
            {
                data["M_SH"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["GGXH"].Trim() && u["DJ"] == sItem["SJDJ"].Trim());
                if (null != extraFieldsDj)
                {
                    MItem[0]["G_YXGM"] = extraFieldsDj["CJM"].Trim();
                    MItem[0]["G_MXHCZHL"] = extraFieldsDj["WXHCZ"];
                }
                else
                {
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 有效钙加氧化镁含量
                if (jcxm.Contains("、有效钙加氧化镁含量、"))
                {
                    double x1 = 0, x2 = 0, x3 = 0, x4 = 0;
                    x1 = Math.Round(GetSafeDouble(sItem["GMYSHL1_1"]) * GetSafeDouble(sItem["GMYSND1_1"]) * 0.028 / (GetSafeDouble(sItem["GMPSZL1_1"]) - GetSafeDouble(sItem["GMKPZL1_1"])) * 100, 1);
                    x2 = Math.Round(GetSafeDouble(sItem["GMYSHL1_2"]) * GetSafeDouble(sItem["GMYSND1_2"]) * 0.028 / (GetSafeDouble(sItem["GMPSZL1_2"]) - GetSafeDouble(sItem["GMKPZL1_2"])) * 100, 1);
                    x3 = Math.Round(GetSafeDouble(sItem["GMYSHL2_1"]) * GetSafeDouble(sItem["GMYSND2_1"]) * 0.028 / (GetSafeDouble(sItem["GMPSZL2_1"]) - GetSafeDouble(sItem["GMKPZL2_1"])) * 100, 1);
                    x4 = Math.Round(GetSafeDouble(sItem["GMYSHL2_1"]) * GetSafeDouble(sItem["GMYSND2_1"]) * 0.028 / (GetSafeDouble(sItem["GMPSZL2_1"]) - GetSafeDouble(sItem["GMKPZL2_1"])) * 100, 1);
                    sItem["GMDZ1_1"] = x1.ToString("0.0");
                    sItem["GMDZ1_2"] = x2.ToString("0.0");
                    sItem["GMDZ2_1"] = x3.ToString("0.0");
                    sItem["GMDZ2_2"] = x4.ToString("0.0");
                    sItem["YXGM"] = Math.Round((x1 + x2 + x3 + x4) / 4, 1).ToString("0.0");
                    if (IsQualified(MItem[0]["G_YXGM"], sItem["YXGM"], false) == "合格")
                    {
                        MItem[0]["HG_YXGM"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_YXGM"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["YXGM"] = "----";
                    MItem[0]["G_YXGM"] = "----";
                    MItem[0]["HG_YXGM"] = "----";
                }
                #endregion

                #region 未消化残渣含量
                if (jcxm.Contains("、未消化残渣含量、") && MItem[0]["G_MXHCZHL"] != "----")
                {
                    double sum = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        sum = sum + Math.Round(GetSafeDouble(sItem["CZSYWZL" + i]) / GetSafeDouble(sItem["CZSYZL" + i]) * 100, 2);
                    }
                    sItem["MXHCZHL"] = (sum / 4).ToString("0.00");
                    if (IsQualified(MItem[0]["G_MXHCZHL"], sItem["MXHCZHL"],false) == "合格")
                    {
                        MItem[0]["HG_MXHCZHL"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HG_MXHCZHL"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["MXHCZHL"] = "----";
                    MItem[0]["G_MXHCZHL"] = "----";
                    MItem[0]["HG_MXHCZHL"] = "----";
                }
                #endregion


            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合标准要求。";
            }
            else
            {
                jsbeizhu = "该组试样不符合标准要求。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
