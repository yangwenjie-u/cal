using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JPH : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            int mbhggs;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool msffs;
            bool mGetBgbh;
            bool mSFwc;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_JPH_DJ"];
            var mrsWd = dataExtra["BZ_DXLWD"];
            var MItem = data["M_JPH"];
            var mitem = MItem[0];
            var SItem = data["S_JPH"];
            var E_JLPB_Temp = data["E_JLPB"];
            #endregion

            #region  计算开始
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            foreach (var sitem in SItem)
            {
                mbhggs = 0; //记录不合格数
                double md, md1, md2, sum;
                int xd;
                string stemp;
                bool sign;
                stemp = "";
                for (xd = 1; xd <= 4; xd++)
                {
                    if (!string.IsNullOrEmpty(sitem["KLMC" + xd]))
                    {
                        stemp = stemp + sitem["KLMC" + xd] + ":" + sitem["KLGG" + xd] + " " + sitem["KLCD" + xd] + ";";
                    }
                }
                mitem["GGCD"] = stemp;
                var E_JLPB_Filter = E_JLPB_Temp;
                //var E_JLPB_Filter = E_JLPB_Temp.Where(x => x["SYSJBRECID"].Equals(sitem["RECID"]));
                foreach (var E_JLPB in E_JLPB_Filter)
                {
                    stemp = E_JLPB["ZLPHB"];
                }
                //经检测，推荐级配组成为寸子:瓜子片:石屑(质量比)=44.2:27.3:28.5，检测结果详见报告第2、3页。
                mitem["JCJGMS"] = "经检测，推荐级配组成为";
                for (xd = 1; xd <= 4; xd++)
                {
                    sitem["KLMC" + xd] = sitem["KLMC" + xd].Trim();
                    if (!string.IsNullOrEmpty(sitem["KLMC" + xd]))
                        mitem["JCJGMS"] = mitem["JCJGMS"] + sitem["KLMC" + xd] + ":";
                }
                mitem["JCJGMS"] = mitem["JCJGMS"].Substring(0, mitem["JCJGMS"].Length - 1);
                mitem["JCJGMS"] = mitem["JCJGMS"] + "(质量比%)=" + stemp;
                mitem["JCJGMS"] = mitem["JCJGMS"] + "，检测结果详见报告第";
                sign = false;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                if (jcxm.Contains("、针片状含量、"))
                {
                    sitem["P_ZPZ"] = IsQualified(sitem["S_ZPZ"], sitem["ZPZ"], true);
                    if (sitem["P_ZPZ"] == "不符合")
                        mAllHg = false;
                    sign = true;
                }
                else
                {
                    sitem["P_ZPZ"] = "----";
                    sitem["ZPZ"] = "----";
                    sitem["S_ZPZ"] = "----";
                }
                if (jcxm.Contains("、压碎值、"))
                {
                    sitem["P_YSZ"] = IsQualified(sitem["S_YSZ"], sitem["YSZ"], true);
                    if (sitem["P_YSZ"] == "不符合")
                        mAllHg = false;
                    sign = true;
                }
                else
                {
                    sitem["P_YSZ"] = "----";
                    sitem["YSZ"] = "----";
                    sitem["S_YSZ"] = "----";
                }
                if (jcxm.Contains("、最大干密度、"))
                {
                    sitem["P_GMD"] = IsQualified(sitem["S_GMD"], sitem["GMD"], true);
                    if (sitem["P_GMD"] == "不符合")
                        mAllHg = false;
                    sign = true;
                }
                else
                {
                    sitem["P_GMD"] = "----";
                    sitem["GMD"] = "----";
                    sitem["S_GMD"] = "----";
                }

                if (sign)
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "2、3页。";
                else
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "2页。";
                if (mAllHg)
                {
                    sitem["JCJG"] = "合格";
                    mitem["JCJG"] = "合格";
                }
                else
                {
                    sitem["JCJG"] = "不合格";
                    mitem["JCJG"] = "不合格";
                }
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
