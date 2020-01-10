using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class NH : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_NH_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_NH"];

            if (!data.ContainsKey("M_NH"))
            {
                data["M_NH"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_NH"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";
            bool flag = true;
            List<double> nArr = new List<double>();
            double xd, Gs = 0;
            var mbhggs = 0;
            int mbHggs1, mbHggs2, mbHggs = 0;
            bool sign = true;

            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";


                if (extraDJ.Count < 1)
                {
                    jsbeizhu = "试件尺寸为空\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                var mrsDj = extraDJ[0];

                sItem["G_SMC1"] = mrsDj["G_SMC1"];
                sItem["G_MMC1"] = mrsDj["G_MMC1"];
                sItem["G_BWC1"] = mrsDj["G_BWC1"];
                sItem["G_CK1"] = mrsDj["G_CK2"];
                sItem["G_SMC2"] = mrsDj["G_SMC2"];
                sItem["G_MMC2"] = mrsDj["G_MMC2"];
                sItem["G_BWC2"] = mrsDj["G_BWC2"];
                sItem["G_CK2"] = mrsDj["G_CK2"];
                sItem["G_PJQD1"] = mrsDj["G_NJQDPJ1"];
                sItem["G_PJQD2"] = mrsDj["G_NJQDPJ2"];
                sItem["G_QDMIN1"] = mrsDj["G_NJQDMIN1"];
                sItem["G_QDMIN2"] = mrsDj["G_NJQDMIN2"];

                //mrsmainTable!which = "bgnh_99、bgnh、bgnh_1、bgnh_2、bgnh_3"

                mbHggs = 0;
                mbHggs1 = 0;
                mbHggs2 = 0;
                sign = true;

                if (jcxm.Contains("、高温淋水循环、") && jcxm.Contains("、加热冷冻循环、"))
                {
                    if (sItem["GH_SMC1"] == "合格")
                        sItem["W_SMC1"] = "无";
                    else
                    {
                        mbHggs1 = mbHggs1 + 1;
                        sItem["W_SMC1"] = "有裂缝、起泡、剥落";
                    }
                    if (sItem["GH_SMC2"] == "合格")
                        sItem["W_SMC2"] = "无";
                    else
                    {
                        mbHggs2 = mbHggs2 + 1;
                        sItem["W_SMC2"] = "有裂缝、起泡、剥落";
                    }
                    if (sItem["GH_MMC1"] == "合格")
                        sItem["W_MMC1"] = "无";
                    else
                    {
                        mbHggs1 = mbHggs1 + 1;
                        mbHggs1 = mbHggs1 + 1;
                        sItem["W_MMC1"] = "有裂缝、起泡、剥落";
                    }

                    if (sItem["GH_MMC2"] == "合格")
                        sItem["W_MMC2"] = "无";
                    else
                    {
                        mbHggs2 = mbHggs2 + 1;
                        sItem["W_MMC2"] = "有裂缝、起泡、剥落";
                    }
                    if (sItem["GH_BWC1"] == "合格" && sItem["GH_CK1"] == "合格")
                    { }
                    else
                    {
                        mbHggs1 = mbHggs1 + 1;
                    }
                    if (sItem["GH_BWC2"] == "合格" && sItem["GH_CK2"] == "合格") { }
                    else
                    {
                        mbHggs2 = mbHggs2 + 1;
                    }
                    if (mbHggs1 == 0)
                    {
                        sItem["SYJG1"] = "饰面层及保护层未出现空鼓、裂缝、起泡、脱落等异常现象。";
                    }
                    else
                    {
                        sItem["SYJG1"] = "饰面层及保护层出现空鼓、裂缝、起泡、脱落等异常现象。";
                    }

                    if (mbHggs2 == 0)
                        sItem["SYJG3"] = "饰面层及保护层未出现空鼓、裂缝、起泡、脱落等异常现象。";
                    else
                    {
                        sItem["SYJG3"] = "饰面层及保护层出现空鼓、裂缝、起泡、脱落等异常现象。";
                    }
                }
                sign = true;

                if (jcxm.Contains("、拉伸粘结强度、"))
                {
                    sign = IsNumeric(sItem["W_PJQD1"]) ? sign : false;
                    sign = IsNumeric(sItem["W_PJQD2"]) ? sign : false;
                    sign = IsNumeric(sItem["W_MINQD1"]) ? sign : false;
                    sign = IsNumeric(sItem["W_MINQD2"]) ? sign : false;
                    if (sign)
                    {
                        sItem["GH_PJQD1"] = IsQualified(sItem["G_PJQD1"], sItem["W_PJQD1"], false);
                        sItem["GH_PJQD2"] = IsQualified(sItem["G_PJQD2"], sItem["W_PJQD2"], false);
                        sItem["GH_QDMIN1"] = IsQualified(sItem["G_QDMIN1"], sItem["W_MINQD1"], false);
                        sItem["GH_QDMIN2"] = IsQualified(sItem["G_QDMIN2"], sItem["W_MINQD2"], false);
                    }
                }
                if (jcxm.Contains("、EPS聚苯板、") || jcxm.Contains("、玻纤网、") || jcxm.Contains("、胶粘剂、") || jcxm.Contains("、抹面胶浆、"))
                {
                    //MItem[0]["WHICH"] = "bgnh_99、bgnh、bgnh_1、bgnh_2、bgnh_3、bgnh_4";
                }
                mbHggs = mbHggs1 + mbHggs2;
                mbHggs = sItem["GH_PJQD1"] == "不合格" ? mbHggs + 1 : mbHggs;
                mbHggs = sItem["GH_PJQD2"] == "不合格" ? mbHggs + 1 : mbHggs;


                if (mbHggs == 0)
                {
                    jsbeizhu = "该组样品所检项目符合上述标准要求";
                    sItem["JCJG"] = "合格";
                }

                if (mbhggs > 0)
                {
                    jsbeizhu = "该组所检项目样品不符合上述标准要求";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }

            }

            #region 添加最终报告

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
