using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace Calculates
{
    public class TS : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_TS"];
            var MItem = data["M_TS"];
            var ET_JXH = data["ET_JXH"][0];
            int mbHggs = 0;
            if (!data.ContainsKey("M_TS"))
            {
                data["M_TS"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];

            foreach (var sItem in SItem)
            {
                #region 界限含水率
                //计算 平均入土深度（1.2.3）  含水率  平均含水率      
                for (int i = 1; i <= 3; i++)
                {

                    if (IsNumeric(ET_JXH["RTSD" + i + "_1"]) && IsNumeric(ET_JXH["RTSD" + i + "_2"]))
                    {
                        //平均入土深度
                        ET_JXH["PJRTSD" + i] = Round((GetSafeDouble(ET_JXH["RTSD" + i + "_1"].Trim()) + GetSafeDouble(ET_JXH["RTSD" + i + "_2"].Trim())) / 2, 2).ToString("0.00");
                    }

                    for (int j = 1; j <= 2; j++)
                    {
                        //湿土+盒质量    干土+盒质量    盒质量
                        if (IsNumeric(ET_JXH["STZL" + i + "_" + j]) && IsNumeric(ET_JXH["GTZL" + i + "_" + j]) && IsNumeric(ET_JXH["HZL" + i + "_" + j]))
                        {
                            //水质量
                            var szl = GetSafeDouble(ET_JXH["STZL" + i + "_" + j]) - GetSafeDouble(ET_JXH["GTZL" + i + "_" + j]);
                            //含水率  水质量/干土质量  取一位小数
                            ET_JXH["HSL" + i + "_" + j] = Round(szl * 100 / (GetSafeDouble(ET_JXH["GTZL" + i + "_" + j]) - GetSafeDouble(ET_JXH["HZL" + i + "_" + j])), 1).ToString("0.0");
                        }

                    }

                    //平均含水率
                    if (IsNumeric(ET_JXH["HSL" + i + "_1"]) && IsNumeric(ET_JXH["HSL" + i + "_2"]))
                    {
                        var pjhsl = Round((GetSafeDouble(ET_JXH["HSL" + i + "_1"]) + GetSafeDouble(ET_JXH["HSL" + i + "_1"])) / 2, 1);
                        ET_JXH["PJHSL" + i] = pjhsl.ToString("0.0");
                    }

                }
                #endregion

                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                if (jcxm.Contains("、界限含水率、"))
                {
                    sItem["GH_JXHSL"] = "符合";
                    string yx = IsQualified(sItem["SJ_YX"], sItem["YX"], true);
                    string sx = IsQualified(sItem["SJ_SX"], sItem["SX"], true);
                    string sxzb = IsQualified(sItem["SJ_SXZB"], sItem["SXZB"], true);
                    if (yx == "不符合" || sx == "不符合" || sxzb == "不符合")
                    {
                        sItem["GH_JXHSL"] = "不符合";
                        mAllHg = false;
                    }
                    else
                    {
                        string yx1 = IsQualified(sItem["SJ_YX"], sItem["YX"], true);
                        string sx1 = IsQualified(sItem["SJ_SX"], sItem["SX"], true);
                        string sxzb1 = IsQualified(sItem["SJ_SXZB"], sItem["SXZB"], true);
                        if (yx1 == "----" || sx1 == "----" || sxzb1 == "----")
                        {
                            sItem["GH_JXHSL"] = "----";
                        }
                    }
                }
                else
                {
                    sItem["YX"] = "----";
                    sItem["SX"] = "----";
                    sItem["SXZB"] = "----";
                    sItem["GH_JXHSL"] = "----";
                }

                mbHggs = sItem["GH_JXHSL"] == "不符合" ? mbHggs + 1 : mbHggs;
                sItem["JCJG"] = mbHggs == 0 ? "合格" : "不合格";

                mAllHg = mbHggs == 0 ? true : false;
                jsbeizhu = mbHggs == 0 ? "该组样品所检项目符合" + mItem["PDBZ"] + "标准要求。" : "该组所检项目样品不符合" + mItem["PDBZ"] + "标准要求。";
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
