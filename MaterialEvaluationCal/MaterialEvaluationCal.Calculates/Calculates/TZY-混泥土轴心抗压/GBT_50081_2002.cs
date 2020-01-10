using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class TZY : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            /* 
             * 计算项目：混泥土轴心抗压
             * 参考标准：
             * GB/T 50081-2002
             */


            var inspectionItems = retData.Select(u => u.Key).ToArray();
            var nonConformityNum = new Dictionary<string, int>() { { "BHG", 0 } };//单组不合格个数
            var fistExtra = dataExtra["BZ_TZY_DJ"].FirstOrDefault();
            foreach (var JCXMitem in inspectionItems)
            {
                var tableItems = retData[JCXMitem]["S_TZY"];
                var s_tableItem = retData[JCXMitem]["S_BY_RW_XQ"];
                var ExtraTable = dataExtra["BZ_TZY_DJ"];
                foreach (var rowData in tableItems)//单行数据进行验证 key 字段名 val值
                {
                    int index = tableItems.IndexOf(rowData);
                    if (index >= retData[JCXMitem]["S_BY_RW_XQ"].Count())
                        throw new Exception("未找到对应的任务详情数据");
                    var rwxqRow = retData[JCXMitem]["S_BY_RW_XQ"][index];

                    if (!dataExtra.ContainsKey("BZ_TZY_DJ"))
                    {
                        throw new Exception("【BZ_TZY_DJ】 表数据不存在");
                    }
                    var extraItem = ExtraTable.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(rowData["SJDJ"])));
                    if (extraItem == null)
                    {
                        //依据不详
                        rwxqRow["JCJG"] = "依据不详";
                        rwxqRow["JCJGMS"] = $"单组流水号[{rowData["RECID"]}]规格不祥";
                        continue;
                    }
                    //轴心抗压
                    var mSz = GetSafeDouble(extraItem["SZ"]);
                    switch (JCXMitem)
                    {
                        case "轴心抗压":
                            double Sjcc = GetSafeDouble(rowData["SJCC"]), 
                                qdVal = 0, 
                                Sjcc1 = GetSafeDouble(rowData["SJCC1"]) == 0 ? Sjcc : GetSafeDouble(rowData["SJCC1"]),
                                mj = Sjcc* Sjcc1,
                                hsxs = GetSafeDouble(rowData["HSXS"]);

                            if(mj == 0 || hsxs == 0)
                            {
                                rowData["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"单组流水号[{rowData["RECID"]}]{(mj == 0 ? "试件尺寸为空" : "换算系数为空")}";
                                nonConformityNum["BHG"]++;
                                break;
                            }

                            double[] arrays = { 0, 0, 0 };
                            for (int i = 1;i<4;i++)
                            {
                                //抗折强度=抗折荷重*1000*试件跨度（MM）/(试件高度（MM）* 试件宽度（MM）*试件宽度（MM）) * 换算系数
                                //精确到0.01MPa

                                qdVal = Math.Round(GetSafeDouble(rowData["KYHZ" + i]) * 1000 * hsxs / mj, 2);
                                rowData["KYQD" + i] = qdVal.ToString("0.00");
                                arrays[i - 1] = qdVal;
                            }
                            /*
                             * 三个试件测值的算术平均值作为该组试件的强度值（精确至0.1%）
                             * 三个测值中的最大值或最小值中如有一个与中间值的差值超过中间值的  15% 时， 则把最大及最小值一并舍除， 取中间值作为该组试件的抗压强度值
                             */
                            Array.Sort(arrays);
                            //断于支架外的需要单独验证

                            if (arrays[1] - arrays[0] > arrays[1] * 0.15 && arrays[2] - arrays[1] > arrays[1] * 1.15)
                            {
                                rowData["MIDAVG"] = "1";
                                rowData["KYPJ"] = "无效";
                                rowData["DDSJQD"] = "不作评定";
                                rowData["JCJG"] = "不作评定";
                                rowData["HZCASE"] = "1";
                                rowData["JCJGMS"] = "最大最小强度值超出中间值的15%,试验结果不作评定依据";
                                nonConformityNum["BHG"]++;
                            }
                            else
                            {
                                if (arrays[1] - arrays[0] > arrays[1] * 0.15 && arrays[2] - arrays[1] <= arrays[1] * 1.15)
                                {
                                    rowData["HZCASE"] = "2";
                                    rowData["KYPJ"] = Math.Round(arrays[1], 1).ToString("0.0");
                                    rowData["JCJGMS"] = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                                    rowData["MIDAVG"] = "true";
                                }
                                else if (arrays[1] - arrays[0] <= arrays[1] * 0.15 && arrays[2] - arrays[1] > arrays[1] * 1.15)
                                {
                                    rowData["HZCASE"] = "3";
                                    rowData["KYPJ"] = Math.Round(arrays[1], 1).ToString("0.0");
                                    rowData["JCJGMS"] = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                                    rowData["MIDAVG"] = "true";
                                }
                                else
                                {
                                    rowData["HZCASE"] = "4";
                                    rowData["KYPJ"] = Math.Round(arrays.Sum() / 3, 1).ToString("0.0");
                                    rowData["JCJGMS"] = "最大最小强度值未超出中间值的15%,试验结果取平均值";
                                }
                                
                                if (mSz != 0)
                                    rowData["DDSJQD"] = Math.Round((GetSafeDouble(rowData["KYPJ"]) / mSz) * 100, 0).ToString("0");
                                if(GetSafeDouble(rowData["DDSJQD"]) > GetSafeDouble(extraItem["QDYQ"]))
                                {
                                    rowData["JCJG"] = "合格";
                                }
                                else
                                {
                                    rowData["JCJG"] = "不合格";
                                    nonConformityNum["BHG"]++;
                                }
                            }




                            #region 更新
                            if (rowData["JCJG"] == "合格")
                            {
                                rwxqRow["JCJG"] = "合格";
                                rwxqRow["JCJGMS"] = $"该组试样强度代表值{rowData["KYPJ"]}MPa，占设计强度{rowData["DDSJQD"]} %，所检项目符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else if(rowData["JCJG"] == "不作评定")
                            {
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样强度代表值{rowData["KYPJ"]}，占设计强度{rowData["DDSJQD"]} ，所检项目不符合{extraItem["PDBZ"]}标准要求。";
                            }
                            else
                            {
                                rwxqRow["JCJG"] = "不合格";
                                rwxqRow["JCJGMS"] = $"该组试样强度代表值{rowData["KYPJ"]}MPa，占设计强度{rowData["DDSJQD"]} %，所检项目不符合{extraItem["PDBZ"]}标准要求。";
                                nonConformityNum["BHG"]++;
                            }
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
