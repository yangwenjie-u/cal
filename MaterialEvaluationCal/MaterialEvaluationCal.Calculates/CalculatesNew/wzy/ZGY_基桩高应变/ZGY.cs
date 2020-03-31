using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZGY : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_ZGY_DJ"];
            var MItem = data["M_ZGY"];
            var SItem = data["S_ZGY"];
            #endregion

            #region 计算开始
            bool mGetBgbh = false;
            bool mAllHg = true;
            int iZgs = 0;
            double mMinKyqd = 9999;
            int zCount = 0;
            string zWhich = "";
            MItem[0]["JCRQ"] = GetSafeDateTime(MItem[0]["SYRQQ"]).ToString("yyyy-MM-dd") + "至" + GetSafeDateTime(MItem[0]["SYRQ"]).ToString("yyyy-MM-dd");
            foreach (var sitem in SItem)
            {
                if (zCount % 8 == 0)
                    zWhich = zWhich + "、bgzdc_" + (zCount / 8 + 3);
                zCount = zCount + 1;
                sitem["JCJG"] = "合格";
                iZgs = iZgs + 1;
                mAllHg = (mAllHg && sitem["jcjg"] == "合格");
            }
            //综合判断
            //mrsmainTable!which = mrsmainTable!which + zWhich
            if ((iZgs / 30) > (int)(iZgs / 30))
                MItem[0]["MLYM"] = (8 + (int)(iZgs / 30) + 1).ToString();
            else
                MItem[0]["MLYM"] = (8 + (int)(iZgs / 30)).ToString();
            if ((iZgs / 8) > (int)(iZgs / 8))
                MItem[0]["MLYM2"] = MItem[0]["MLYM"] + (int)(iZgs / 8) + 1;
            else
                MItem[0]["MLYM2"] = MItem[0]["MLYM"] + (int)(iZgs / 8);
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            #endregion

            /************************ 代码结束 *********************/
        }
    }
}