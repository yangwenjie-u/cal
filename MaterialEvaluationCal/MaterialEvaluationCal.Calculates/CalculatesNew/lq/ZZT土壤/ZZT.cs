using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZZT : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_ZZT"];
            var SItem = data["S_ZZT"];
           // var extraDJ = dataExtra["BZ_GYC_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];

            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
             

                if (jcxm.Contains("、PH值、"))

                {
                    jcxmCur = "PH值";
                    if (GetSafeDouble(sitem["PHSCZ"]) <= GetSafeDouble(sitem["S_PHZ"]))
                    {
                        sitem["PHDXPD"] = "合格";
                    }
                    else
                    {
                        mAllHg = false;
                        sitem["PHDXPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                  


                }
                if (jcxm.Contains("、有机质、"))
                {
                    jcxmCur = "有机质";
                    if (GetSafeDouble(sitem["YJZSCZ"]) <= GetSafeDouble(sitem["S_YJZ"]))
                    {
                        sitem["YJZDXPD"] = "合格";
                    }
                    else
                    {

                        mAllHg = false;
                        sitem["YJZDXPD"] = "不合格";
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

