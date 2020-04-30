using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class DCL:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            //word导入不需要计算
            var data = retData;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            var MItem = data["M_DCL"];
            if (!data.ContainsKey("M_DCL"))
            {
                data["M_DCL"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> mItem = new Dictionary<string, string>();
                mItem["JCJG"] = mjcjg;
                mItem["JCJGMS"] = jsbeizhu;
                MItem.Add(mItem);
            }
            else
            {
                MItem[0]["YS"] = "(报告正文共"+MItem[0]["ZWYS"]+"页，附图表"+MItem[0]["FTYS"]+"页)";
            }

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
