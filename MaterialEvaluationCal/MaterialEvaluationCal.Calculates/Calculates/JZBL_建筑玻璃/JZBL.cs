using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class JZBL : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/

            var extraData = dataExtra["BZ_ZBL_DJ"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();
            int forEachFlag = 0;
            foreach (var jcxm in jcxmItems)
            {
                switch (jcxm.Trim())
                {
                    case "露点试验1":
                        var JSItems = retData[jcxm]["S_ZBL"];
                        var M_JSItems = retData[jcxm]["M_ZBL"];
                        var rwxqData = retData[jcxm]["S_BY_RW_XQ"];

                        forEachFlag = 0;
                        foreach (var item in JSItems)
                        {

                            if (item["JSQK"] == "无结露或结霜")
                            {
                                //合格
                                M_JSItems[forEachFlag]["HG_LDSY"] = "合格" ;
                                rwxqData[forEachFlag]["JCJG"] = "合格";
                                rwxqData[forEachFlag]["JCJGMS"] = "该组试样所检项符合上述标准要求";
                            }
                            else
                            {
                                M_JSItems[forEachFlag]["HG_LDSY"] =  "不合格";
                                rwxqData[forEachFlag]["JCJG"] = "不合格";
                                rwxqData[forEachFlag]["JCJGMS"] = "该组试样不符合上述标准要求";
                            }

                            forEachFlag++;
                        }

                        break;
                    case "遮蔽系数":
                        //暂无数据
                        break;
                    case "可见光透射比":
                        //暂无数据
                        break;
                    default:
                        break;
                }
            }

            //添加最终报告
            return true;
            /************************ 代码结束 *********************/
        }

    }
}
