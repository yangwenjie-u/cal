using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SMZ1 : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            /* 
             * 计算项目：饰面砖粘结强度
             * 参考标准：
             * JGJ/T 110-2017
             */


            var inspectionItems = retData.Select(u => u.Key).ToArray();
            var nonConformityNum = new Dictionary<string, int>();//单组不合格个数
            nonConformityNum["BHG"] = 0;
            var fistExtra = dataExtra["BZ_SMZ_DJ"].FirstOrDefault();
            foreach (var JCXMitem in inspectionItems)
            {
                var tableItems = retData[JCXMitem]["S_SMZ"];
                var s_tableItem = retData[JCXMitem]["S_BY_RW_XQ"];
                var ExtraTable = dataExtra["BZ_SMZ_DJ"];
                foreach (var rowData in tableItems)//单行数据进行验证 key 字段名 val值
                {
                    int index = tableItems.IndexOf(rowData);
                    if (index >= retData[JCXMitem]["S_BY_RW_XQ"].Count())
                        throw new Exception("未找到对应的任务详情数据");
                    var rwxqRow = retData[JCXMitem]["S_BY_RW_XQ"][index];

                    if (!dataExtra.ContainsKey("BZ_SMZ_DJ"))
                    {
                        throw new Exception("【BZ_SMZ_DJ】 表数据不存在");
                    }
                    var extraItem = ExtraTable.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(rowData["SJDJ"])));
                    if (extraItem == null)
                    {
                        //依据不详
                        rwxqRow["JCJG"] = "依据不详";
                        rwxqRow["JCJGMS"] = $"单组流水号[{rowData["RECID"]}]试件尺寸为空";
                        continue;
                    }
                    //
                    var mSz = GetSafeDouble(rowData["SZ"]); 
                    switch (JCXMitem)
                    {
                        case "粘结强度":
                            #region 粘结强度
                            double qdVal = 0, mj = 0,
                                mdkbxy = GetSafeDouble(extraItem["DKBXY"]),
                                mycbhgs = GetSafeDouble(extraItem["YCBHGS"]);
                            double[] arrays = { 0, 0, 0 };
                            /*
                             * 三个试件测值的算术平均值作为该组试件的强度值（精确至0.1%），不得小于标准值
                             * 三个测值中允许有一个小于标准值，但不得小于最低值
                             */

                            for (int i = 1; i < 4; i++)
                            {
                                //粘结强度=粘结力*1000 /(试件长度（MM）* 试件宽度（MM)
                                //精确到0.1MPa
                                mj = Convert.ToInt32(GetSafeInt(rowData["SJCD" + i]) * GetSafeInt(rowData["SJKD" + i]));
                                qdVal = mj == 0 ? 0 : Math.Round(GetSafeDouble(rowData["KYHZ" + i]) * 1000 / mj, 1);
                                rowData["MJ" + i] = mj.ToString();
                                rowData["LSQD" + i] = qdVal.ToString("0.0");
                                if (qdVal < mdkbxy)
                                    nonConformityNum["LSQD"]++;
                                arrays[i - 1] = qdVal;
                            }
                            Array.Sort(arrays);

                            rowData["DKZX"] = arrays[2].ToString("0.0");
                            #region 更新
                            if (nonConformityNum["LSQD"] <= mycbhgs && 
                                arrays.Average() > GetSafeDouble(extraItem["DKBXY"]))
                            {

                                rowData["MIN_HG"] = "合格";
                                rowData["PJZ_HG"] = "合格";
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {

                                rowData["MIN_HG"] = nonConformityNum["LSQD"] > mycbhgs ? "不合格": "合格";
                                rowData["PJZ_HG"] = arrays.Average() > mycbhgs ? "不合格" : "合格";
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样所检项目符合{extraItem["PDBZ"]}标准要求。";
                                nonConformityNum["BHG"]++;
                            }
                            #endregion
                            #endregion
                            break;
                        default:
                            break;
                    }
                }
            }

            #region 更新主表检测结果
            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            bgjgDic.Add("JCJG", (nonConformityNum["BHG"] > 0 ? "不合格" : "合格"));
            bgjgDic.Add("JCJGMS", $"该组试样所检项目{(nonConformityNum["BHG"] > 0 ? "不" : "")}符合{fistExtra["PDBZ"]}标准要求。");
            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG",bgjg);
            #endregion
            return true;
            /************************ 代码结束 *********************/
        }
    }
}
