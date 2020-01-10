using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class FSX1 : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            /* 
             * 计算项目：建筑材料放射性核素限量
             * 参考标准：
             * GB 6566-2010《建筑材料放射性核素限量》
             */


            var inspectionItems = retData.Select(u => u.Key).ToArray();
            var nonConformityNum = new Dictionary<string, int>();//单组不合格个数
            nonConformityNum["BHG"] = 0;
            foreach (var JCXMitem in inspectionItems)
            {
                var tableItems = retData[JCXMitem]["S_FSX"];
                var s_tableItem = retData[JCXMitem]["S_BY_RW_XQ"];
                var ExtraTable = dataExtra["BZ_FSX_DJ"];
                int index = 0;
                foreach (var rowData in tableItems)//单行数据进行验证 key 字段名 val值
                {
                    var rwxqRow = retData[JCXMitem]["S_BY_RW_XQ"][index];

                    if (!dataExtra.ContainsKey("BZ_FSX_DJ"))
                    {
                        throw new Exception("【BZ_FSX_DJ】 表数据不存在");
                    }
                    var extraItem = ExtraTable.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(rowData["SJDJ"])));
                    if (extraItem == null)
                    {
                        //依据不详
                        rwxqRow["JCJG"] = "依据不详";
                        rwxqRow["JCJGMS"] = $"单组流水号[{rowData["RECID"]}]试件尺寸为空";
                        continue;
                    }

                    nonConformityNum["hg"]= 0;
                    switch (JCXMitem)
                    {
                        case "内照射指数":
                            #region 内照射指数 IRA 精确到0.1
                            if(GetSafeDouble(extraItem["IRA"]) == 0)
                            {
                                rowData["IRAPD"] = "-----";
                                rowData["JCJG"] = "-----";
                                rowData["JCJGMS"] = "-----";
                                rwxqRow["JCJG"] = "-----";
                                rwxqRow["JCJGMS"] = "-----";
                                break;
                            }
                            if (GetSafeDouble(rowData["IRA"]) > GetSafeDouble(extraItem["IRA"]))
                            {
                                rowData["IRAPD"] = "不符合";
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目不符合{extraItem["PDBZ"]}标准要求。";
                                nonConformityNum["BHG"]++;
                                break;
                            }
                            else
                            {
                                rowData["IRAPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目符合{extraItem["PDBZ"]}标准要求。";
                            }
                            
                            #endregion
                            break;

                        case "外照射指数":
                            #region 外照射指数 IR 精确到0.1
                            if (GetSafeDouble(rowData["IR"]) > GetSafeDouble(extraItem["IR"]))
                            {
                                rowData["IRPD"] = "不符合";
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目不符合{extraItem["PDBZ"]}标准要求。";
                                nonConformityNum["BHG"]++;
                                break;
                            }
                            else
                            {
                                rowData["IRPD"] = "符合";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目符合{extraItem["PDBZ"]}标准要求。";
                            }

                            #endregion
                            break;
                        default:
                            break;
                    }

                    index++;
                }
            }

            #region 更新主表检测结果
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            bgjgDic.Add("JCJG", (nonConformityNum["BHG"] > 0 ? "不合格" : "合格"));
            bgjgDic.Add("JCJGMS", $"该组试样所检项目{(nonConformityNum["BHG"] > 0 ? "不" : "")}符合{dataExtra["BZ_FSX_DJ"].FirstOrDefault()["PDBZ"]}标准要求。");
            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG",bgjg);
            #endregion
            return true;
            /************************ 代码结束 *********************/
        }
    }
}
