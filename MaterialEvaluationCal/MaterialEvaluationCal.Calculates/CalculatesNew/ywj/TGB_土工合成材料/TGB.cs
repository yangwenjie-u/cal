using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TGB : BaseMethods
    {
        public void Calc()
        {
            #region 计算方法
            var extraDJ = dataExtra["BZ_TGB_DJ"];
            var extraNJSY = dataExtra["BZ_TGBNJSY"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var SItems = data["S_TGB"];

            if (!data.ContainsKey("M_TGB"))
            {
                data["M_TGB"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_TGB"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";
            var mCPMC = "";
            IDictionary<string, string> mrsNJSY = new Dictionary<string, string>();
            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                mCPMC = sItem["CPMC"];//设计等级名称
                if (string.IsNullOrEmpty(mCPMC))
                {
                    mCPMC = "";
                }

                var bcdlqd = GetSafeDouble(sItem["BCDLQD"]);
                var mrsDj = extraDJ.FirstOrDefault(u => u["MC"] == mCPMC && GetSafeDouble(u["BCDLQD"]) == bcdlqd);

                if (null == mrsDj || mrsNJSY == null)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";

                    throw new Exception("数据有误，找不到" + mCPMC + "对应标准。");
                    continue;
                }
                //断裂强度
                MItem[0]["G_HXDLQD"] = mrsDj["G_DLQD"];
                MItem[0]["G_ZXDLQD"] = mrsDj["G_DLQD"];
                //对应伸长率
                sItem["G_JXSCL"] = mrsDj["G_DYSCL"];
                sItem["G_WXSCL"] = mrsDj["G_DYSCL"];

                //撕破强力
                MItem[0]["G_ZXSPQL"] = mrsDj["G_SPQL"];
                MItem[0]["G_HXSPQL"] = mrsDj["G_SPQL"];

                //耐静水压

                if (mCPMC != "短纤针刺非织造土工布")
                {
                    mrsNJSY = extraNJSY.FirstOrDefault(u => u["MC"] == mCPMC && u["NJSYMHD"] == sItem["NJSYMHD"] && u["NJSYLX"] == sItem["NJSYLX"]);
                    MItem[0]["G_NJSY"] = mrsNJSY["G_NJSY"];
                }


                //断裂强度、断裂伸长率、撕破强力
                //断裂强度
                if (jcxm.Contains("、断裂强度、"))
                {
                    for (int i = 1; i<= 10; i++)
                    {
                        sItem["DLQD_" + i] = (GetSafeDouble(sItem["ZDFH" + i]) * (1 / GetSafeDouble(sItem["MYKD" + i]))).ToString("0.00");

                    }
                    sItem["H_SCDLPJZ"] = ((GetSafeDouble(sItem["DLQD_1"]) + GetSafeDouble(sItem["DLQD_2"]) + GetSafeDouble(sItem["DLQD_3"]) + GetSafeDouble(sItem["DLQD_4"]) + GetSafeDouble(sItem["DLQD_5"])) / 5).ToString("0.00");
                    sItem["Z_SCDLPJZ"] = ((GetSafeDouble(sItem["DLQD_6"]) + GetSafeDouble(sItem["DLQD_7"]) + GetSafeDouble(sItem["DLQD_8"]) + GetSafeDouble(sItem["DLQD_9"]) + GetSafeDouble(sItem["DLQD_10"])) / 5).ToString("0.00");



                    sItem["H_DXDLPD"] = IsQualified(MItem[0]["G_HXDLQD"], sItem["H_SCDLPJZ"], false);
                    sItem["Z_DXDLPD"] = IsQualified(MItem[0]["G_ZXDLQD"], sItem["Z_SCDLPJZ"], false);

                    mAllHg = sItem["H_DXDLPD"] == "合格" ? mAllHg : false;
                    mAllHg = sItem["Z_DXDLPD"] == "合格" ? mAllHg : false;
                }
                else
                {
                    //sItem["HG_ZXDLQD"] = "----";
                    //sItem["HG_ZXDLQD"] = "----";
                    //sItem["HXDLQD"] = "----";
                    //sItem["ZXDLQD"] = "----";
                    //MItem[0]["G_HXDLQD"] = "----";
                    //MItem[0]["G_ZXDLQD"] = "----";
                }

                //断裂伸长率
                if (jcxm.Contains("、断裂伸长率、"))
                {
                    //sItem["GH_HXQD"] = IsQualified(sItem["HXQDSJZ"], sItem["W_HXQD"], false);
                    //sItem["GH_ZXQD"] = IsQualified(sItem["ZXQDSJZ"], sItem["W_ZXQD"], false);

                    for (int i = 1; i <= 10; i++)
                    {
                        sItem["DLSCL" + i] = ((GetSafeDouble(sItem["ZDFHXSC" + i])- GetSafeDouble(sItem["YFHSC" + i]))/(GetSafeDouble(sItem["GJCD" + i])+ GetSafeDouble(sItem["YFHSC" + i]))).ToString("0.00");

                    }
                    sItem["H_SCLPJZ"] = (((GetSafeDouble(sItem["DLSCL1"]) + GetSafeDouble(sItem["DLSCL2"]) + GetSafeDouble(sItem["DLSCL3"]) + GetSafeDouble(sItem["DLSCL4"]) + GetSafeDouble(sItem["DLSCL5"])) / 5)*100).ToString("0.00");

                    sItem["Z_SCLPJZ"] = (((GetSafeDouble(sItem["DLSCL6"]) + GetSafeDouble(sItem["DLSCL7"]) + GetSafeDouble(sItem["DLSCL8"]) + GetSafeDouble(sItem["DLSCL9"]) + GetSafeDouble(sItem["DLSCL10"])) / 5)*100).ToString("0.00");



                    sItem["Z_DXSCLPD"] = IsQualified(sItem["G_JXSCL"], sItem["Z_SCLPJZ"], false);
                    sItem["H_DXSCLPD"] = IsQualified(sItem["G_WXSCL"], sItem["H_SCLPJZ"], false);

                    //mAllHg = sItem["GH_HXQD"] == "合格" ? mAllHg : false;
                    //mAllHg = sItem["GH_ZXQD"] == "合格" ? mAllHg : false;
                    mAllHg = sItem["Z_DXSCLPD"] == "合格" ? mAllHg : false;
                    mAllHg = sItem["H_DXSCLPD"] == "合格" ? mAllHg : false;
                }
                else
                {
                    sItem["GH_ZXQD"] = "----";
                    sItem["GH_ZSCL"] = "----";
                    sItem["GH_HXQD"] = "----";
                    sItem["GH_HSCL"] = "----";

                    sItem["ZXQDSJZ"] = "----";
                    sItem["ZSCLSJZ"] = "----";
                    sItem["ZSCLSJZ"] = "----";
                    sItem["HSCLSJZ"] = "----";

                    sItem["W_ZXQD"] = "----";
                    sItem["W_ZSCL"] = "----";
                    sItem["W_HXQD"] = "----";
                    sItem["W_HSCL"] = "----";
                }

                //撕破强力
                if (jcxm.Contains("、撕破强力、"))
                {



                    sItem["HG_ZXSPQL"] = IsQualified(MItem[0]["G_ZXSPQL"], sItem["ZXSPQL"], false);
                    sItem["HG_HXSPQL"] = IsQualified(MItem[0]["G_HXSPQL"], sItem["HXSPQL"], false);
                    mAllHg = sItem["HG_ZXSPQL"] == "合格" ? mAllHg : false;
                    mAllHg = sItem["HG_HXSPQL"] == "合格" ? mAllHg : false;
                }
                else
                {
                    sItem["HG_ZXSPQL"] = "----";
                    sItem["HG_HXSPQL"] = "----";
                    sItem["ZXSPQL"] = "----";
                    sItem["HXSPQL"] = "----";
                    MItem[0]["G_HXSPQL"] = "----";
                    MItem[0]["G_ZXSPQL"] = "----";

                }

                //耐静水压
                //if (jcxm.Contains("、耐静水压、"))
                //{
                //    sItem["HG_NJSY"] = IsQualified(MItem[0]["G_NJSY"], sItem["NJSY"], false);
                //    mAllHg = sItem["HG_NJSY"] == "合格" ? mAllHg : false;
                //}
                //else
                //{
                //    sItem["HG_NJSY"] = "----";
                //    MItem[0]["G_NJSY"] = "----";
                //    sItem["NJSY"] = "----";
                //}

                if (!mAllHg)
                {
                    sItem["JCJG"] = "不合格";
                }
                else
                {
                    sItem["JCJG"] = "合格";
                }
            }

            #region 添加最终报告
            jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";

            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
        }
    }
}

