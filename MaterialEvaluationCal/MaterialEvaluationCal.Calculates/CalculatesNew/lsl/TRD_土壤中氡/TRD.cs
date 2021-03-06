﻿using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class TRD : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            var data = retData;

            var SItem = data["SJ_TRD"];
            var MItem = data["M_TRD"];
            var mjcjg = "不合格";
            var jsbeizhu = "依据" + MItem[0]["JCYJ"] + "，需采取防氡工程措施。";

            int mbHggs = 0;
            if (!data.ContainsKey("M_TRD"))
            {
                data["M_TRD"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            string mJSFF;
            bool sign, mFlag_Bhg = false, mFlag_Hg = false;
            string BHGXM = "", Hgxm = "";
            double zddnd = 0, zxdnd = 99999;

            //计算覆盖面积
            if (mItem !=null && IsNumeric(mItem["CD"]) && IsNumeric(mItem["KD"]))
            {
                mItem["FGMJ"] = Round(GetSafeDouble(mItem["CD"]) * GetSafeDouble(mItem["KD"]), 2).ToString();
            }

            List<double> listArray = new List<double>();
            foreach (var sItem in SItem)
            {
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                double dnd =  GetSafeDouble(sItem["DND1"]);
                listArray.Add(dnd);
                //if (dnd > 0)
                //{
                //    if (zddnd < dnd) 
                //    {
                //        zddnd = dnd;
                //    }
                //    if (zxdnd > dnd)
                //    {
                //        zxdnd = dnd;
                //    }
                //}
                sItem["JCJG"] = "合格";
            }

            MItem[0]["DNDMAX"] = listArray.Max().ToString();
            MItem[0]["DNDMIN"] = listArray.Min().ToString();
            MItem[0]["DNDAVERAGE"] = listArray.Average().ToString();

            MItem[0]["G_DND"] = "≤20000";
            if (listArray.Max() > 20000)
            {
                mAllHg = false;
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["JCYJ"] + "，可不采取防氡工程措施。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
