using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class TG : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            var extraData = dataExtra["BZ_TG_DJ"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();
            int forEachFlag = 0;
            int mbhggs = 0;
            foreach (var jcxm in jcxmItems)
            {
                switch (jcxm.Trim())
                {
                    case "最小壁厚":
                        var ZXBHItems = retData[jcxm]["S_TG"];
                        var M_ZXBHItems = retData[jcxm]["M_TG"];
                        var XQ_ZXBHData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_ZXBHItems)
                        {
                            if (GetSafeDouble(item["ZXBH"]) < GetSafeDouble(item["G_ZXBH"]))
                            {
                                //合格
                                item["ZXBH_HG"] = "合格";
                                M_ZXBHItems[forEachFlag]["JCJG"] = "合格";
                                M_ZXBHItems[forEachFlag]["JCJGMS"] = "合格";
                            }
                            else
                            {
                                item["ZXBH_HG"] = "不合格";
                                M_ZXBHItems[forEachFlag]["JCJG"] = "不合格";
                                M_ZXBHItems[forEachFlag]["JCJGMS"] = "不合格";
                                mbhggs++;
                            }
                            
                            forEachFlag++;
                        }
                        break;

                    case "跌落试验":
                        var DLItems = retData[jcxm]["S_TG"];
                        var M_DLItems = retData[jcxm]["M_TG"];
                        var XQ_DLData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_DLItems)
                        {
                            if (item["DLSY"] == "符合")
                            {
                                //合格
                                item["DLSY_HG"] = "合格";
                                XQ_DLData[forEachFlag]["JCJG"] = "合格";
                                XQ_DLData[forEachFlag]["JCJGMS"] = "合格";
                            }
                            else
                            {
                                item["DLSY_HG"] = "不合格";
                                XQ_DLData[forEachFlag]["JCJG"] = "不合格";
                                XQ_DLData[forEachFlag]["JCJGMS"] = "不合格";
                                mbhggs++;
                            }
                            forEachFlag++;
                        }
                        break;
                    case "弯曲性能":
                        var WQItems = retData[jcxm]["S_TG"];
                        var M_WQItems = retData[jcxm]["M_TG"];
                        var XQ_WQData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_WQItems)
                        {
                            if (item["WQ1"] == "符合")
                            {
                                //合格
                                item["WQ1_HG"] = "合格";
                            }
                            else
                            {
                                item["WQ1_HG"] = "不合格";
                                mbhggs++;
                            }
                            if (item["WQ2"] == "符合")
                            {
                                //合格
                                item["WQ2_HG"] = "合格";
                            }
                            else
                            {
                                item["WQ2_HG"] = "不合格";
                                mbhggs++;
                            }


                            if (item["WQ1"] == "符合" && item["WQ2"] == "符合")
                            {
                                XQ_WQData[forEachFlag]["JCJG"] = "合格";
                                XQ_WQData[forEachFlag]["JCJGMS"] = "合格";
                            }
                            else
                            {
                                XQ_WQData[forEachFlag]["JCJG"] = "不合格";
                                XQ_WQData[forEachFlag]["JCJGMS"] = "弯曲性能常温" + item["WQ1_HG"]+ ",弯曲性能低温" + item["WQ2_HG"];
                            }
                            forEachFlag++;
                        }
                        break;
                    case "抗压性能":
                        var KYItems = retData[jcxm]["S_TG"];
                        var M_KYItems = retData[jcxm]["M_TG"];
                        var XQ_KYData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_KYItems)
                        {
                            for (int i = 1; i < 3; i++)
                            {
                                for (int j = 1; j < 4; j++) //保留一位小数
                                {
                                    item["KYJBX" + i + "_" + "j"] = ((GetSafeDouble(item["KYXN0_" + "j"]) - GetSafeDouble(item["KYXN1_" + "j"])) / GetSafeDouble(item["KYXN0_" + "j"]) * 100).ToString("0.0");
                                }
                                // VB中有两次判断，数据库中没有相应字段
                                //mrsmainTable!kyxbx2_1 = Round((mrsmainTable!kyxn0_1 - mrsmainTable!kyxn2_1) / mrsmainTable!kyxn0_1 * 100, 1)
                                //mrsmainTable!kyxbx2_2 = Round((mrsmainTable!kyxn0_2 - mrsmainTable!kyxn2_2) / mrsmainTable!kyxn0_2 * 100, 1)
                                //mrsmainTable!kyxbx2_3 = Round((mrsmainTable!kyxn0_3 - mrsmainTable!kyxn2_3) / mrsmainTable!kyxn0_3 * 100, 1)
                                continue;
                            }

                            if (GetSafeDouble(item["KYJBX1_1"]) > GetSafeDouble(item["G_KYXN1"]) || GetSafeDouble(item["KYJBX1_2"]) > GetSafeDouble(item["G_KYXN1"]) || GetSafeDouble(item["KYJBX1_3"]) > GetSafeDouble(item["G_KYXN1"]))
                            {
                                item["KYJBX_HG"] = "不合格";
                                XQ_KYData[forEachFlag]["JCJG"] = "不合格";
                                XQ_KYData[forEachFlag]["JCJGMS"] = "压力试验加载后变形(D2)1" + (GetSafeDouble(item["KYJBX1_1"]) > GetSafeDouble(item["G_KYXN1"]) ? "不合格" : "合格") + (GetSafeDouble(item["KYJBX1_2"]) > GetSafeDouble(item["G_KYXN1"]) ? "不合格" : "合格") + (GetSafeDouble(item["KYJBX1_3"]) > GetSafeDouble(item["G_KYXN1"]) ? "不合格" : "合格");
                                mbhggs++;
                            }
                            else
                            {
                                item["KYJBX_HG"] = "合格";
                                XQ_KYData[forEachFlag]["JCJG"] = "合格";
                                XQ_KYData[forEachFlag]["JCJGMS"] = "合格";
                            }

                            //if (GetSafeDouble(item["KYJBX2_1"]) > GetSafeDouble(item["G_KYXN2"]) || GetSafeDouble(item["KYJBX2_2"]) > GetSafeDouble(item["G_KYXN2"]) || GetSafeDouble(item["KYJBX2_3"]) > GetSafeDouble(item["G_KYXN2"]))
                            //{
                            //    item["KYJBX_HG"] = "不合格";
                            //    mbhggs++;
                            //}
                            //else
                            //{
                            //    item["KYJBX_HG"] = "合格";
                            //}

                            forEachFlag++;
                        }
                        break;
                    case "外观":
                        var WGItems = retData[jcxm]["S_TG"];
                        var M_WGItems = retData[jcxm]["M_TG"];
                        var XQ_WGData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_WGItems)
                        {
                            if (item["WG"] == "符合")
                            {
                                //合格
                                item["WG_HG"] = "合格";
                                XQ_WGData[forEachFlag]["JCJG"] = "合格";
                                XQ_WGData[forEachFlag]["JCJGMS"] = "合格";
                            }
                            else
                            {
                                item["WG_HG"] = "不合格";
                                XQ_WGData[forEachFlag]["JCJG"] = "不合格";
                                XQ_WGData[forEachFlag]["JCJGMS"] = "不合格";
                                mbhggs++;
                            }
                            forEachFlag++;
                        }
                        break;
                    case "阻燃性能":
                        var ZYItems = retData[jcxm]["S_TG"];
                        var M_ZYItems = retData[jcxm]["M_TG"];
                        var XQ_ZYData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_ZYItems)
                        {
                            if (item["ZRXN"] == "符合")
                            {
                                //合格
                                item["ZRXN_HG"] = "合格";
                                XQ_ZYData[forEachFlag]["JCJG"] = "合格";
                                XQ_ZYData[forEachFlag]["JCJGMS"] = "合格";
                            }
                            else
                            {
                                item["ZRXN_HG"] = "不合格";
                                XQ_ZYData[forEachFlag]["JCJG"] = "不合格";
                                XQ_ZYData[forEachFlag]["JCJGMS"] = "不合格";
                                mbhggs++;
                            }
                            forEachFlag++;
                        }
                        break;
                    case "最大外径":
                        var ZDWJItems = retData[jcxm]["S_TG"];
                        var M_ZDWJItems = retData[jcxm]["M_TG"];
                        var XQ_ZDWJData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_ZDWJItems)
                        {
                            if (item["ZDWJ"] == "符合")
                            {
                                //合格
                                item["ZDWJ_HG"] = "合格";
                                XQ_ZDWJData[forEachFlag]["JCJG"] = "合格";
                                XQ_ZDWJData[forEachFlag]["JCJGMS"] = "合格";
                            }
                            else
                            {
                                item["ZDWJ_HG"] = "不合格";
                                XQ_ZDWJData[forEachFlag]["JCJG"] = "不合格";
                                XQ_ZDWJData[forEachFlag]["JCJGMS"] = "不合格";
                                mbhggs++;
                            }
                            forEachFlag++;
                        }

                        break;
                    case "最小内径":

                        var ZXNJItems = retData[jcxm]["S_TG"];
                        var M_ZXNJItems = retData[jcxm]["M_TG"];
                        var XQ_ZXNJData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_ZXNJItems)
                        {
                            if (item["ZXNJ"] == "符合")
                            {
                                //合格
                                item["ZXNJ_HG"] = "合格";
                                XQ_ZXNJData[forEachFlag]["JCJG"] = "合格";
                                XQ_ZXNJData[forEachFlag]["JCJGMS"] = "合格";
                            }
                            else
                            {
                                item["ZXNJ_HG"] = "不合格";
                                XQ_ZXNJData[forEachFlag]["JCJG"] = "不合格";
                                XQ_ZXNJData[forEachFlag]["JCJGMS"] = "不合格";
                                mbhggs++;
                            }


                            forEachFlag++;
                        }

                        break;
                    case "最小外径":
                        var ZXWJItems = retData[jcxm]["S_TG"];
                        var M_ZXWJItems = retData[jcxm]["M_TG"];
                        var XQ_ZXWJData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in M_ZXWJItems)
                        {
                            if (item["ZXWJ"] == "符合")
                            {
                                //合格
                                item["ZXWJ_HG"] = "合格";
                                XQ_ZXWJData[forEachFlag]["JCJG"] = "合格";
                                XQ_ZXWJData[forEachFlag]["JCJGMS"] = "合格";
                            }
                            else
                            {
                                item["ZXWJ_HG"] = "不合格";
                                XQ_ZXWJData[forEachFlag]["JCJG"] = "不合格";
                                XQ_ZXWJData[forEachFlag]["JCJGMS"] = "不合格";
                                mbhggs++;
                            }

                            forEachFlag++;
                        }
                        break;
                    default:
                        break;
                }
            }

            #region 添加最终报告
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            if (mbhggs != 0)
            {
                //不合格
                bgjgDic.Add("JCJG", "不合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。");
            }
            else
            {
                bgjgDic.Add("JCJG", "合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。");
            }

            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion
            return true;
            /************************ 代码结束 *********************/
        }

    }
}
