using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class BKM : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_BKM"];
            var SItem = data["S_BKM"];
           // var extraDJ = dataExtra["BZ_GYC_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];

            foreach (var sitem in SItem)
            {


                mitem["JCJGMS"] = "依据《公路路基路面现场测试规程》JTG E60-2008进行检测，所检项目只提供实测数据，不做判定。";



            }
           

        }

    }
 }

