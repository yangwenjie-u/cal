using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.HSA_砂_行标_
{
    public class HSA : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var jcxm_keys = retData.Select(u => u.Key).ToArray();

            foreach (var jcxmitem in jcxm_keys)
            {
                var s_hsatab = retData[jcxmitem]["S_HSA"];
                var s_rwtab = retData[jcxmitem]["S_BY_RW_XQ"];
                var m_hsatab = retData[jcxmitem]["M_HSA"];
                //var mrsHS = dataExtra["BZ_NADXFF"];
                var mrsDj = dataExtra["BZ_HSA_DJ"];
                var mrsHS = dataExtra["BZ_HSAHSB"];

                int row = 0;
                foreach (var item in s_hsatab)
                {
                    #region  公共部分
                    double msyzl = GetSafeDouble(m_hsatab[row]["SYZL"]);
                    double msyzl1 = GetSafeDouble(m_hsatab[row]["NI_SYZL"]);
                    if (string.IsNullOrEmpty(msyzl.ToString()) || msyzl == 0)
                        msyzl = 500;
                    if (m_hsatab[row]["JCYJ"].Contains("2006"))
                        m_hsatab[row]["WHICH"] = "1";
                    else
                        m_hsatab[row]["WHICH"] = "2";
                    #endregion
                    if (jcxmitem.Contains("级配"))
                    {

                    }
                }
            }

            return mAllHg;
        }
    }
}
