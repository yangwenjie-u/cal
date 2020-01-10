using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class HB : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region 
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItems = retData["S_HB"];
            var extraDJ = dataExtra["BZ_HB_DJ"];

            if (!retData.ContainsKey("M_HB"))
            {
                retData["M_HB"] = new List<IDictionary<string, string>>();
            }
            var MItem = retData["M_HB"];

            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGSM"] = jsbeizhu;
                MItem.Add(m);
            }

            bool mAllHg = true;
            var mItemHg = true;
            string mSjdjbh, mSjdj = "";
            double mSjcc, mMj, mSjcc1 = 0;
            double mSz, mQdyq, mHsxs, mttjhsxs = 0;
            string mJSFF = "";
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;
            int mbhggs = 0;//不合格数量
            List<double> mtmpArray = new List<double>();
            bool sign = true;
            var jcxm = "";
            foreach (var sItem in SItems)
            {
                mItemHg = true;
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj = extraDJ.FirstOrDefault(u => u["TBLB"] == sItem["TBLB"].Trim() && u["HD"] == sItem["DLDJ"].Trim());

                if (null == mrsDj)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = jsbeizhu + "试件尺寸为空/r/n";
                    continue;
                }
                MItem[0]["G_PHHZ"] = mrsDj["G_PHHZ"].Trim();
                MItem[0]["G_KYQD"] = mrsDj["G_KYQD"].Trim();
                MItem[0]["G_MMD"] = mrsDj["G_MMD"].Trim();
                MItem[0]["G_HSL"] = mrsDj["G_HSL"].Trim();
                MItem[0]["G_KDX"] = mrsDj["G_KDX"].Trim();
                MItem[0]["G_RHXS"] = mrsDj["G_RHXS"].Trim();


                sign = true;
                if (jcxm.Contains("、抗弯承载、"))
                {
                    sign = IsNumeric(MItem[0]["W_PHHZ"]) ? sign : false;
                    if (sign)
                        MItem[0]["GH_PHHZ"] = IsQualified(MItem[0]["G_PHHZ"], MItem[0]["W_PHHZ"], false);
                    else
                        sign = false;
                }
                if (!sign)
                {
                    MItem[0]["G_PHHZ"] = "----";
                    MItem[0]["W_PHHZ"] = "----";
                    MItem[0]["GH_PHHZ"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、抗压强度、"))
                    MItem[0]["GH_PHHZ"] = IsQualified(MItem[0]["G_KYQD"], MItem[0]["W_KYQD"], false);
                else
                    sign = false;

                if (!sign)
                {
                    MItem[0]["G_PHHZ"] = "----";
                    MItem[0]["W_PHHZ"] = "----";
                    MItem[0]["GH_PHHZ"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、软化系数、"))
                    MItem[0]["GH_RHXS"] = IsQualified(MItem[0]["G_RHXS"], MItem[0]["W_RHXS"], false);
                else
                    sign = false;
                if (!sign)
                {
                    MItem[0]["W_RHXS"] = "----";
                    MItem[0]["G_RHXS"] = "----";
                    MItem[0]["GH_RHXS"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、面密度、"))
                    MItem[0]["GH_MMD"] = IsQualified(MItem[0]["G_MMD"], MItem[0]["W_MMD"], false);
                else
                {
                    MItem[0]["W_MMD"] = "----";
                    MItem[0]["GH_MMD"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、含水率、"))
                    MItem[0]["GH_HSL"] = IsQualified(MItem[0]["G_HSL"], MItem[0]["W_HSL"], false);
                else
                {
                    MItem[0]["W_HSL"] = "----";
                    MItem[0]["G_HSL"] = "----";
                    MItem[0]["GH_HSL"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、抗冻性、"))
                {
                    if (MItem[0]["GH_KDX"] == "合格")
                    {
                        MItem[0]["W_KDX"] = "无可见裂纹或表面无变化";
                    }
                    else
                    {
                        MItem[0]["W_KDX"] = "出现可见裂纹或表面胶落";
                    }
                }
                else
                {
                    MItem[0]["W_KDX"] = "----";
                    MItem[0]["G_KDX"] = "----";
                    MItem[0]["W_KDX"] = "----";
                }

                if (MItem[0]["GH_PHHZ"] == "不合格" || MItem[0]["GH_KYQD"] == "不合格" || MItem[0]["GH_MMD"] == "不合格" || MItem[0]["GH_HSL"] == "不合格" || MItem[0]["GH_KDX"] == "不合格" || MItem[0]["GH_RHXS"] == "不合格")
                {
                    mbhggs += 1;
                    mAllHg = false;
                }
                if (mbhggs == 0)
                {
                    jsbeizhu = "该组样品所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                    sItem["JCJG"] = "合格";
                    mAllHg = true;
                }

                if (mbhggs > 0)
                {
                    jsbeizhu = "该组所检项目样品不符合" + MItem[0]["PDBZ"] + "标准要求。";

                    if (MItem[0]["GH_PHHZ"] == "合格" || MItem[0]["GH_KYQD"] == "合格" || MItem[0]["GH_MMD"] == "合格" || MItem[0]["GH_HSL"] == "合格" || MItem[0]["GH_KDX"] == "合格" || MItem[0]["GH_RHXS"] == "合格")
                    {
                        jsbeizhu = "该组样品所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                    }

                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
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
