using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class PGD : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_PGD"];
            var SItem = data["S_PGD"];
           // var extraDJ = dataExtra["BZ_GYC_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];

            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
             

                if (jcxm.Contains("、电视检测、"))

                {
                    jcxmCur = "电视检测";
                    if (sitem["CCTVdxpd"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }



                }
                if (jcxm.Contains("、潜望镜检测、"))
                {
                    jcxmCur = "潜望镜检测";

                    if (sitem["QWJdxpd"] == "不合格")
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

