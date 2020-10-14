using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class LJM : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_LJM"];
            var SItem = data["S_LJM"];
            //var mrsDj = dataExtra["BZ_LJM_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";

            //GB/T9775-2008
            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

               
              
                if (jcxm.Contains("、厚度、"))
                {

                    sitem["SCPJZ"] = ((GetSafeDouble(sitem["SCZ1"]) + GetSafeDouble(sitem["SCZ2"]) + GetSafeDouble(sitem["SCZ3"]) + GetSafeDouble(sitem["SCZ4"])) / 4).ToString();

                    sitem["SJPC"] = (GetSafeDouble(sitem["SCPJZ"]) - GetSafeDouble(sitem["SJZ"])).ToString();
                    if (GetSafeDouble(sitem["SJPC"]) >= -5 && GetSafeDouble(sitem["SJPC"]) <= 10)
                    {
                        MItem[0]["HD_DXPD"] = "合格";
                    }
                    else
                    {

                        MItem[0]["HD_DXPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }


                }
          
            }

        }

    }
 }

