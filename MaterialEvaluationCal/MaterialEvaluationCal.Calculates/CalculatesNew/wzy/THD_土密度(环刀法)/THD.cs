using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class THD : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            double[] mkyqdArray = new double[6];
            string[] mtmpArray;
            double mSjcc, mSjcc1, mMj;
            double mMaxKyqd, mMinKyqd, mAvgKyqd;
            double mskys, msktj, msyhsl, msymd, msygmd, msyzd;
            string mSjdjbh, mSjdj;
            double mSz, mQdyq, mhsxs;
            int vp, iZs, iHgs;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            int a, aa, aaa, I, i1;
            bool mSFwc;
            mSFwc = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_THD_DJ"];
            var MItem = data["M_THD"];
            var mitem = MItem[0];
            var SItem = data["S_THD"];
            #endregion

            #region  计算开始
            mAllHg = true;
            mitem["JCJGMS"] = "";
            iZs = 0;
            iHgs = 0;
            var Sitem = SItem[0];
            string bl, bl_sjysd;
            bl = Sitem["ZDGMD"];
            bl_sjysd = Sitem["SJYSD"];
            foreach (var sitem in SItem)
            {
                //计算龄期
                sitem["LQ"] = (GetSafeDateTime(mitem["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString("F0");
                sitem["ZDGMD"] = bl;
                sitem["SJYSD"] = bl_sjysd;
                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                    if (jcxm.Contains("、干密度、"))
                    {
                        sitem["SMD1"] = Round(Conversion.Val(sitem["TZL1"]) / Conversion.Val(sitem["HDRJ1"]), 2).ToString("0.00");
                        sitem["SMD2"] = Round(Conversion.Val(sitem["TZL2"]) / Conversion.Val(sitem["HDRJ1"]), 2).ToString("0.00");
                        if (Conversion.Val(sitem["HSL11"]) >= 40 && Conversion.Val(sitem["HSL12"]) >= 40)
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL11"]) - Conversion.Val(sitem["HSL12"])) > 2)
                                sitem["PJHSL1"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL11"]) - Conversion.Val(sitem["HSL12"])) > 1)
                                sitem["PJHSL1"] = "无效";
                        }


                        if (Conversion.Val(sitem["HSL21"]) >= 40 && Conversion.Val(sitem["HSL22"]) >= 40)
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL21"]) - Conversion.Val(sitem["HSL22"])) > 2)
                                sitem["PJHSL2"] = "无效";
                        }
                        else
                        {
                            if (Math.Abs(Conversion.Val(sitem["HSL21"]) - Conversion.Val(sitem["HSL22"])) > 1)
                                sitem["PJHSL2"] = "无效";
                        }
                        if (Math.Abs(Conversion.Val(sitem["GMD1"]) - Conversion.Val(sitem["GMD2"])) <= 0.03)
                            sitem["GMD"] = Round((Conversion.Val(sitem["GMD1"]) + Conversion.Val(sitem["GMD2"])) / 2, 2).ToString("0.00");
                        else
                            sitem["GMD"] = "无效";
                        if (Conversion.Val(sitem["ZDGMD"]) == 0)
                            sitem["PJYSD"] = "0";
                        else
                            sitem["PJYSD"] = Round(100 * Conversion.Val(sitem["GMD"]) / Conversion.Val(sitem["ZDGMD"]), 0).ToString();
                        if (Conversion.Val(sitem["SJYSD"]) <= Conversion.Val(sitem["PJYSD"]))
                            iHgs = iHgs + 1;
                        sitem["JCJG"] = "----";
                    }
                    else
                    {
                        sitem["GMD"] = "----";
                        sitem["PJYSD"] = "----";
                    }
                }
            }
            mitem["JCDS"] = SItem.Count().ToString();
            iZs = SItem.Count();
            if (iZs != 0)
                mitem["HGL"] = Round((100 * iHgs / iZs), 1).ToString("0.0");
            if (Conversion.Val(mitem["HGL"]) < 100)
                mAllHg = false;
            //主表总判断赋值
            if (mAllHg)
                mitem["JCJG"] = "合格";
            else
                mitem["JCJG"] = "不合格";
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
