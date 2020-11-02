using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class DJY : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_DJY"];
            var SItem = data["S_DJY"];
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
                    jcxmCur = "外观检查";
                    sitem["G_WGJC"] = "绳应光滑、干燥、无霉变、断股磨损、灼伤、缺口。";
                    if (sitem["WGDXPD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }


                }
                if (jcxm.Contains("、工频耐压、"))
                {
                    jcxmCur = "工频耐压";
                    sitem["G_GPNY"] = "试验中不应发生闪络或击穿，试验后无放电、灼伤痕迹及明显发热。";
                    if (sitem["DXPDGPNY"] == "不合格")
                    {
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

