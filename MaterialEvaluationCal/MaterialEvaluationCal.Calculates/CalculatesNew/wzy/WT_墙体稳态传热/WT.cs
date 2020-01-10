using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class WT : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            int mbHggs;
            mSFwc = true;
            mGetBgbh = false;
            mAllHg = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_WT_DJ"];
            var MItem = data["M_WT"];
            var SItem = data["S_WT"];
            var Wt_Mst = data["WT_MST"];
            #endregion

            #region  计算开始
            foreach (var sitem in SItem)
            {
                mbHggs = 0;
                mSFwc = true;
                double md1, md2, md, pjmd, sum, Gd1, Gd2;
                string bl;
                int xd, Gs;
                bool flag, sign, mark;
                string dzbh, wtbh;
                var WT_MST = Wt_Mst;
                //var WT_MST = Wt_Mst.Where(x => x["SYSJBRECID"].Equals(sitem["RECID"]));
                Gs = WT_MST.Count();
                xd = Gs / 18;
                if (Gs % 18 >= 1)
                    xd = xd + 1;
                if (xd == 1)
                    sitem["YP_JCBZ"] = "检测数据详见报告第2页。";
                else
                    sitem["YP_JCBZ"] = "检测数据详见报告第2～" + (xd + 1) + "页。";
                sitem["QTXS"] = sitem["QTXS"].Trim();
                sitem["JCJG"] = "合格";
                MItem[0]["JCJG"] = "合格";
                sitem["YP_JCJG"] = "U=" + sitem["QTXS"] + "(W/㎡·K)";
                MItem[0]["JCJGMS"] = "该组墙体试样传热系数为" + sitem["QTXS"] + "(W/㎡·K)";
                if (IsNumeric(sitem["YP_SJZ"]) && IsNumeric(sitem["QTXS"]))
                {
                    md1 = GetSafeDouble(sitem["YP_SJZ"].Trim());
                    md2 = GetSafeDouble(sitem["QTXS"].Trim());
                    if (md2 <= md1)
                        MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + ",符合设计要求(" + sitem["YP_SJZ"] + ")";
                    else
                    {
                        MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + ",不符合设计要求(" + sitem["YP_SJZ"] + ")";
                        sitem["JCJG"] = "不合格";
                        MItem[0]["JCJG"] = "不合格";
                    }
                }
                MItem[0]["JCJGMS"] = MItem[0]["JCJGMS"] + "。";
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
