using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class DBA : BaseMethods
    {
        public void Calc()
        {

            var data = retData;
            var MItem = data["M_DBA"];
            var SItem = data["S_DBA"];
            // var extraDJ = dataExtra["BZ_GYC_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];

            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";


                if (jcxm.Contains("、外观检查、"))
                {
                    jcxmCur = "外观检测";
                    sitem["G_WGJC"] = "线夹完整、无损坏、线夹与电力设备及接地体的接触面无毛刺，导线无裸露部分，导线外覆透明护层应均匀、无龟裂";
                    if (sitem["WGDXPD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }


                }
                if (jcxm.Contains("、成组直流电阻、"))
                {
                    jcxmCur = "成组直流电阻";
                    if (sitem["xjmj"] == "10")
                    {
                        if (sitem["SCXJMJ"] == "10" && GetSafeDouble(sitem["SCZLDZ"]) < 1.98)
                        {
                            sitem["DXPDZLDZ"] = "合格";
                        }
                        else
                        {
                            sitem["DXPDZLDZ"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    if (sitem["xjmj"] == "16")
                    {
                        if (sitem["SCXJMJ"] == "16" && GetSafeDouble(sitem["SCZLDZ"]) < 1.24)
                        {
                            sitem["DXPDZLDZ"] = "合格";
                        }
                        else
                        {
                            sitem["DXPDZLDZ"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }

                    }
                    if (sitem["xjmj"] == "25")
                    {
                        if (sitem["SCXJMJ"] == "25" && GetSafeDouble(sitem["SCZLDZ"]) < 0.79)
                        {
                            sitem["DXPDZLDZ"] = "合格";
                        }
                        else
                        {
                            sitem["DXPDZLDZ"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }

                    }

                }
                if (mAllHg)
                {
                    mitem["JCJG"] = "合格";
                    mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目均符合要求。";
                }
                else
                {
                    mitem["JCJG"] = "不合格";
                    mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求";
                }

            }

        }
    }

}
