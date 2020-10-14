using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TGY : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_TGY"];
            var SItem = data["S_TGY"];
            var extraDJ = dataExtra["BZ_GYC_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];
            //GB/T9775-2008
            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(x => x["PH"].Contains(sitem["GCLX_PH"]));

                sitem["G_KLQD"]= ">"+ extraFieldsDj["KLQDBZZ"].Trim();


                if (jcxm.Contains("、抗拉强度、"))
                {
                    jcxmCur = "抗拉强度";
                    if (GetSafeDouble(sitem["KLQD1"]) >= GetSafeDouble(sitem["G_KLQD"])&&sitem["DLWZ1"]=="断于钢筋")
                    {
                        sitem["DXPD1"] = "合格";
                    }
                    else
                    {
                        sitem["DXPD1"] = "不合格";
                    }
                    if (GetSafeDouble(sitem["KLQD2"]) >= GetSafeDouble(sitem["G_KLQD"]) && sitem["DLWZ2"] == "断于钢筋")
                    {
                        sitem["DXPD2"] = "合格";
                    }
                    else
                    {
                        sitem["DXPD2"] = "不合格";
                    }
                    if (GetSafeDouble(sitem["KLQD3"]) >= GetSafeDouble(sitem["G_KLQD"]) && sitem["DLWZ3"] == "断于钢筋")
                    {
                        sitem["DXPD3"] = "合格";
                    }
                    else
                    {
                        sitem["DXPD3"] = "不合格";
                    }

                    if (sitem["DXPD1"] == "合格" && sitem["DXPD2"] == "合格" && sitem["DXPD3"] == "合格")
                    {
                        sitem["HG_DXPD"] = "合格";
                    }
                    else
                    {
                        mAllHg = false;
                        sitem["HG_DXPD"] = "不合格";
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

