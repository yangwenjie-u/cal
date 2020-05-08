using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
/*土工*/
namespace Calculates
{
    public class SCY : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_AM_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_SCYS = data["S_SCY"];
            bool sign = false;
            if (!data.ContainsKey("M_SCY"))
            {
                data["M_SCY"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_SCY"];
            //var ET_SF = data["ET_SF"][0];
            double sum = 0;
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            string mJSFF = "";
            int mHggs = 0;//统计合格数量
            foreach (var sItem in S_SCYS)
            {
                #region 颗粒分析
                //sign = true;
                //sign = IsNumeric(sItem["SQZZL"].Trim());//筛前总质量
                //sign = IsNumeric(sItem["XSZZL"].Trim());//小于2mm取样质量
                //sign = IsNumeric(sItem["XTZL"].Trim());//小于2mm试样质量
                //if (sign)
                //{
                //    //小于2mm试样占总质量百分数
                //    sItem["XTBL"] = Round(GetSafeDouble(sItem["XTZL"].Trim()) / GetSafeDouble(sItem["SQZZL"].Trim()) * 100, 1).ToString("0.0");
                //    //累计留筛土
                //    sItem["CLJLSTZL60"] = sItem["CFJSTZL60"].Trim();
                //    sItem["CLJLSTZL40"] = Round(GetSafeDouble(sItem["CLJLSTZL60"]) + GetSafeDouble(sItem["CFJSTZL40"]), 1).ToString("0.0");
                //    sItem["CLJLSTZL20"] = Round(GetSafeDouble(sItem["CLJLSTZL40"]) + GetSafeDouble(sItem["CFJSTZL20"]), 1).ToString("0.0");
                //    sItem["CLJLSTZL10"] = Round(GetSafeDouble(sItem["CLJLSTZL20"]) + GetSafeDouble(sItem["CFJSTZL10"]), 1).ToString("0.0"); ;
                //    sItem["CLJLSTZL5"] = Round(GetSafeDouble(sItem["CLJLSTZL10"]) + GetSafeDouble(sItem["CFJSTZL5"]), 1).ToString("0.0");
                //    sItem["CLJLSTZL2"] = Round(GetSafeDouble(sItem["CLJLSTZL5"]) + GetSafeDouble(sItem["CFJSTZL2"]), 1).ToString("0.0");
                //    sItem["XLJLSTZL2"] = sItem["XFJSTZL2"].Trim();
                //    sItem["XLJLSTZL1"] = Round(GetSafeDouble(sItem["XLJLSTZL2"]) + GetSafeDouble(sItem["XFJSTZL1"]), 1).ToString("0.0");
                //    sItem["XLJLSTZL05"] = Round(GetSafeDouble(sItem["XLJLSTZL1"]) + GetSafeDouble(sItem["XFJSTZL05"]), 1).ToString("0.0");
                //    sItem["XLJLSTZL025"] = Round(GetSafeDouble(sItem["XLJLSTZL05"]) + GetSafeDouble(sItem["XFJSTZL025"]), 1).ToString("0.0");
                //    sItem["XLJLSTZL0075"] = Round(GetSafeDouble(sItem["XLJLSTZL025"]) + GetSafeDouble(sItem["XFJSTZL0075"]), 1).ToString("0.0");
                //    sItem["LJSYZLSD"] = Round(GetSafeDouble(sItem["XLJLSTZL0075"]) + GetSafeDouble(sItem["SDSY"]), 1).ToString("0.0");
                //    //小于该粒径土质量
                //    sItem["CXYLJSYZL60"] = Round(GetSafeDouble(sItem["SQZZL"].Trim()) - GetSafeDouble(sItem["CLJLSTZL60"]), 1).ToString("0.0");
                //    sItem["CXYLJSYZL40"] = Round(GetSafeDouble(sItem["SQZZL"].Trim()) - GetSafeDouble(sItem["CLJLSTZL40"]), 1).ToString("0.0");
                //    sItem["CXYLJSYZL20"] = Round(GetSafeDouble(sItem["SQZZL"].Trim()) - GetSafeDouble(sItem["CLJLSTZL20"]), 1).ToString("0.0");
                //    sItem["CXYLJSYZL10"] = Round(GetSafeDouble(sItem["SQZZL"].Trim()) - GetSafeDouble(sItem["CLJLSTZL10"]), 1).ToString("0.0");
                //    sItem["CXYLJSYZL5"] = Round(GetSafeDouble(sItem["SQZZL"].Trim()) - GetSafeDouble(sItem["CLJLSTZL5"]), 1).ToString("0.0");
                //    sItem["CXYLJSYZL2"] = Round(GetSafeDouble(sItem["SQZZL"].Trim()) - GetSafeDouble(sItem["CLJLSTZL2"]), 1).ToString("0.0");
                //    sItem["XXYLJSYZL2"] = Round(GetSafeDouble(sItem["XTZL"].Trim()) - GetSafeDouble(sItem["XLJLSTZL2"]), 1).ToString("0.0");
                //    sItem["XXYLJSYZL1"] = Round(GetSafeDouble(sItem["XTZL"].Trim()) - GetSafeDouble(sItem["XLJLSTZL1"]), 1).ToString("0.0");
                //    sItem["XXYLJSYZL05"] = Round(GetSafeDouble(sItem["XTZL"].Trim()) - GetSafeDouble(sItem["XLJLSTZL05"]), 1).ToString("0.0");
                //    sItem["XXYLJSYZL025"] = Round(GetSafeDouble(sItem["XTZL"].Trim()) - GetSafeDouble(sItem["XLJLSTZL025"]), 1).ToString("0.0");
                //    sItem["XXYLJSYZL0075"] = Round(GetSafeDouble(sItem["XTZL"].Trim()) - GetSafeDouble(sItem["XLJLSTZL0075"]), 1).ToString("0.0");
                //    sItem["XYLJSYZLSD"] = Round(GetSafeDouble(sItem["XTZL"].Trim()) - GetSafeDouble(sItem["LJSYZLSD"]), 1).ToString("0.0");
                //    //占总土质量百分比 
                //    sItem["CTGZBFL60"] = Round(GetSafeDouble(sItem["CXYLJSYZL60"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["CTGZBFL40"] = Round(GetSafeDouble(sItem["CXYLJSYZL40"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["CTGZBFL20"] = Round(GetSafeDouble(sItem["CXYLJSYZL20"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["CTGZBFL10"] = Round(GetSafeDouble(sItem["CXYLJSYZL10"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["CTGZBFL5"] = Round(GetSafeDouble(sItem["CXYLJSYZL5"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["CTGZBFL2"] = Round(GetSafeDouble(sItem["CXYLJSYZL2"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["XTGZBFL2"] = Round(GetSafeDouble(sItem["XXYLJSYZL2"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["XTGZBFL1"] = Round(GetSafeDouble(sItem["XXYLJSYZL1"]) / GetSafeDouble(sItem["XTZL"]) * GetSafeDouble(sItem["XTGZBFL2"]), 1).ToString("0.0");
                //    sItem["XTGZBFL05"] = Round(GetSafeDouble(sItem["XXYLJSYZL05"]) / GetSafeDouble(sItem["XTZL"]) * GetSafeDouble(sItem["XTGZBFL2"]), 1).ToString("0.0");
                //    sItem["XTGZBFL025"] = Round(GetSafeDouble(sItem["XXYLJSYZL025"]) / GetSafeDouble(sItem["XTZL"]) * GetSafeDouble(sItem["XTGZBFL2"]), 1).ToString("0.0");
                //    sItem["XTGZBFL0075"] = Round(GetSafeDouble(sItem["XXYLJSYZL0075"]) / GetSafeDouble(sItem["XTZL"]) * GetSafeDouble(sItem["XTGZBFL2"]), 1).ToString("0.0");
                //    sItem["TGZBFSD"] = Round(GetSafeDouble(sItem["XYLJSYZLSD"]) / GetSafeDouble(sItem["XTZL"]) * GetSafeDouble(sItem["XTGZBFL0075"]), 2).ToString("0.00");

                //    //小于该粒径土质量百分比
                //    sItem["CTGBFL60"] = Round(GetSafeDouble(sItem["CXYLJSYZL60"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");//y轴  大 > 小
                //    sItem["CTGBFL40"] = Round(GetSafeDouble(sItem["CXYLJSYZL40"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["CTGBFL20"] = Round(GetSafeDouble(sItem["CXYLJSYZL20"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["CTGBFL10"] = Round(GetSafeDouble(sItem["CXYLJSYZL10"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["CTGBFL5"] = Round(GetSafeDouble(sItem["CXYLJSYZL5"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["CTGBFL2"] = Round(GetSafeDouble(sItem["CXYLJSYZL2"]) / GetSafeDouble(sItem["SQZZL"]) * 100, 1).ToString("0.0");
                //    sItem["XTGBFL2"] = Round(GetSafeDouble(sItem["XXYLJSYZL2"]) / GetSafeDouble(sItem["XTZL"]) * 100, 1).ToString("0.0");
                //    sItem["XTGBFL1"] = Round(GetSafeDouble(sItem["XXYLJSYZL1"]) / GetSafeDouble(sItem["XTZL"]) * 100, 1).ToString("0.0");
                //    sItem["XTGBFL05"] = Round(GetSafeDouble(sItem["XXYLJSYZL05"]) / GetSafeDouble(sItem["XTZL"]) * 100, 1).ToString("0.0");
                //    sItem["XTGBFL025"] = Round(GetSafeDouble(sItem["XXYLJSYZL025"]) / GetSafeDouble(sItem["XTZL"]) * 100, 1).ToString("0.0");
                //    sItem["XTGBFL0075"] = Round(GetSafeDouble(sItem["XXYLJSYZL0075"]) / GetSafeDouble(sItem["XTZL"]) * 100, 1).ToString("0.0");
                //    sItem["TGBFSD"] = Round(GetSafeDouble(sItem["XYLJSYZLSD"]) / GetSafeDouble(sItem["XTZL"]) * 100, 1).ToString("0.0");

                //    //大于2mm砾粒占(%)
                //    sItem["DYBFL"] = Round(100 - GetSafeDouble(sItem["XTGZBFL2"]),1).ToString("0.0");
                //    //2～0.075mm砂粒占(%)
                //    sItem["QJBFL"] = Round(GetSafeDouble(sItem["XLJLSTZL0075"]) / GetSafeDouble(sItem["SQZZL"].Trim()) * 100, 1).ToString("0.0");
                //    //大于0.075mm粗粒占(%)
                //    sItem["DYBFL0075"] = Round((GetSafeDouble(sItem["XLJLSTZL0075"]) + GetSafeDouble(sItem["CLJLSTZL2"])) / GetSafeDouble(sItem["SQZZL"].Trim()) * 100, 1).ToString("0.0");
                //    #region
                //    /**
                //     * 根据级配曲线 获得 d60  d30 d10 的粒径值    
                //     * d60  占总土质量60%对应 的粒径
                //     * d30  占总土质量30%对应 的粒径
                //     * d10  占总土质量10%对应 的粒径
                //     * 
                //     * 不均匀系数  sItem["BJYXS"] = Round(d60 / d10,1).ToString("0.0"); 
                //     * 曲率系数    sItem["QLXS"] = Round((d30 * d30)/(d60 * d10),1).ToString("0.0");
                //     */
                //    #endregion
                //}
                //else
                //{
                //    throw new SystemException("筛前总质量、小于2mm取样质量或小于2mm试样质量数据录入有误。");
                //}
                #endregion

                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                #region 颗粒分析
                if (jcxm.Contains("、颗粒分析、"))
                {
                    if (GetSafeDouble(sItem["BJYXS"]) >=5 && GetSafeDouble(sItem["QLXS"])  >=1 && GetSafeDouble(sItem["QLXS"]) <= 3)
                    {
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目属级配良好砂砾。";
                    }
                    else
                    {
                        mAllHg = false;
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目属级配不良砂砾。";
                    }
                    //sItem["GH_SF"] = IsQualified(sItem["SJ_SF1"], sItem["W_SF1"], true);
                    //if (sItem["GH_SF"] == "不符合")
                    //{
                    //    mAllHg = false;
                    //}
                    //sign = true;
                }
                else
                {
                    sItem["W_SF1"] = "----";
                    sItem["BZ_SF"] = "----";
                    sItem["GH_SF"] = "----";
                }
                #endregion
            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                //jsbeizhu = "该组样品所检项目符合标准要求。";
            }
            else
            {
                //jsbeizhu = "该组样品所检项目不符合标准要求。";

            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
