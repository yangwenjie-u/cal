using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class LNJ : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            var extraDJData = dataExtra["BZ_LNJ_DJ"];
            var jcxmItems = retData.Select(u => u.Key).ToArray();
            int forEachFlag = 0;

            foreach (var jcxm in jcxmItems)
            {
                switch (jcxm)
                {
                    case "垫圈洛氏硬度":
                        #region
                        var DQLSItems = retData[jcxm]["S_LNJ"];
                        forEachFlag = 0;
                        foreach (var item in DQLSItems)
                        {
                            var extraFields = extraDJData.FirstOrDefault(u => u.ContainsKey("LSXNDJ") && u.Values.Contains(item["LSXNDJ"]) && u.ContainsKey("GGXH") && u.Values.Contains(item["LWGG"]));

                            if (null == extraFields)
                            {
                                // 找不到对应标准
                                continue;
                            }
                            item["DQYDYQ"] = extraFields["DQLSYD"];

                            double sum = 0;
                            List<double> listItem = new List<double>();
                            for (int i = 0; i < 9; i++)
                            {
                                sum = 0;
                                for (int j = 0; j < 4; i++)
                                {
                                    sum += GetSafeDouble(item["DQYD" + i + "_" + j]);
                                }
                                item["DQYD" + i] = GetSafeInt((sum / 3).ToString()).ToString();
                                listItem.Add(GetSafeInt((sum / 3).ToString()));
                            }
                            listItem.Sort();//升序排序

                            //item["DQYD" + i] 41
                            //extraFields["DQLSYD"] 35HRC~45HRC
                            List<string> listLMLSYD = extraFields["DQLSYD"].ToUpper().Replace("HRC", "").Split('~').ToList();

                            if ((listItem[0] >= GetSafeDouble(listLMLSYD[0]) && listItem[0] <= GetSafeDouble(listLMLSYD[0])) && (listItem[7] >= GetSafeDouble(listLMLSYD[0]) && listItem[7] <= GetSafeDouble(listLMLSYD[0])))
                            {
                                item["DQYDPD"] = "合格";
                            }
                            else
                            {
                                item["DQYDPD"] = "不合格";
                            }

                            forEachFlag++;
                        }
                        #endregion
                        break;
                    case "紧固轴力":
                        #region 
                        var JGZLItems = retData["jcxm"]["S_LNJ"];
                        forEachFlag = 0;
                        foreach (var item in JGZLItems)
                        {
                            var extraJGZLFields = extraDJData.FirstOrDefault(u => u.ContainsKey("LSXNDJ") && u.Values.Contains(item["LSXNDJ"]) && u.ContainsKey("GGXH") && u.Values.Contains(item["LWGG"]));

                            if (null == extraJGZLFields)
                            {
                                // 找不到对应标准
                                continue;
                            }
                            item["KLHZYQ"] = string.Format(@"平均值{0}, 标准差{1}", extraJGZLFields["KPJ"], extraJGZLFields["KBZC"]);

                            double pjKlhz = 0,//平均荷重
                                    sumKlhz = 0;//总荷重
                            for (int i = 1; i < 9; i++)
                            {
                                sumKlhz += GetSafeDouble(item["KLHZ" + i]);
                            }
                            pjKlhz = sumKlhz / 8;
                            item["KLHZPJ"] = GetSafeDouble(pjKlhz.ToString("0.00")).ToString();
                            sumKlhz = 0;
                            for (int i = 1; i < 9; i++)
                            {
                                sumKlhz = sumKlhz + Math.Pow(GetSafeDouble(item["KLHZ" + i]) - pjKlhz, 2);
                            }

                            item["KLHZBZC"] = Math.Sqrt(sumKlhz / 7).ToString("0.00");  //VB代码是除7
                            item["KLHZPD"] = "----";

                            if (item["LSZL"].Contains("扭剪型"))
                            {
                                string extraKBZC = extraJGZLFields["KBZC"];  //≤0.0100
                                string extraKPJ = extraJGZLFields["KPJ"]; //0.110~0.150
                                List<string> listKPJ = extraKPJ.Split('~').ToList();

                                if (GetSafeDouble(item["KLHZBZC"]) <= GetSafeDouble(extraKBZC.Substring(1)) && GetSafeDouble(item["KLHZPJ"]) >= GetSafeDouble(listKPJ[0]) && GetSafeDouble(item["KLHZPJ"]) <= GetSafeDouble(listKPJ[1]))
                                {
                                    item["KLHZPD"] = "合格";
                                }
                                else
                                {
                                    item["KLHZPD"] = "不合格";
                                }
                                item["NJXSPD"] = item["KLHZPD"];
                            }
                        }
                        #endregion
                        break;
                    case "螺母洛氏硬度":
                        #region MyRegion
                        var LMLSItems = retData[jcxm]["S_LNJ"];
                        forEachFlag = 0;
                        foreach (var item in LMLSItems)
                        {
                            var extraJGZLFields = extraDJData.FirstOrDefault(u => u.ContainsKey("LSXNDJ") && u.Values.Contains(item["LSXNDJ"]) && u.ContainsKey("GGXH") && u.Values.Contains(item["LWGG"]));

                            if (null == extraJGZLFields)
                            {
                                // 找不到对应标准
                                continue;
                            }
                            item["LMYDYQ"] = string.Format(@"洛氏硬度应≥{0}, ≤{1}", extraJGZLFields["LMLSYDMIN"], extraJGZLFields["LMLSYDMAX"]);

                            double sum = 0;
                            List<double> listLMYD = new List<double>();
                            for (int i = 0; i < 9; i++)
                            {
                                sum = 0;
                                for (int j = 0; j < 4; i++)
                                {
                                    sum += GetSafeDouble(item["LMYD" + i + "_" + j]);
                                }
                                item["LMYD" + i] = GetSafeInt((sum / 3).ToString()).ToString();
                                listLMYD.Add(GetSafeInt((sum / 3).ToString()));
                            }
                            listLMYD.Sort();//升序排序


                            if (listLMYD[0] >= GetSafeInt(extraJGZLFields["LMLSYDMIN"]) || listLMYD[0] <= GetSafeInt(extraJGZLFields["LMLSYDMAX"]))
                            {
                                item["LMYDPD"] = "合格";
                            }
                            else
                            {
                                item["LMYDPD"] = "不合格";
                            }
                            forEachFlag++;
                        }
                        #endregion
                        break;
                    case "扭矩":
                    case "扭矩系数":
                        #region MyRegion
                        var NJItems = retData["jcxm"]["S_LNJ"];
                        foreach (var item in NJItems)
                        {
                            var extraNJFields = extraDJData.FirstOrDefault(u => u.ContainsKey("LSXNDJ") && u.Values.Contains(item["LSXNDJ"]) && u.ContainsKey("GGXH") && u.Values.Contains(item["LWGG"]));

                            if (null == extraNJFields)
                            {
                                // 找不到对应标准
                                continue;
                            }
                            item["NJXSYQ"] = string.Format(@"扭矩系数平均值为{0}, 标准偏差应{1}", extraNJFields["KPJ"], extraNJFields["KBZC"]);

                            double pjValue = 0,//平均荷重
                                  sum = 0;//总荷重
                            //klhz
                            //ZNNJ1
                            for (int i = 1; i < 9; i++)
                            {
                                sum += GetSafeDouble(item["ZNNJ" + i]) / GetSafeDouble(item["KLHZ" + i]) / GetSafeDouble(extraNJFields["ZJ"]);
                                item["NJXS"] = sum.ToString("0.000");
                            }
                            pjValue = GetSafeDouble((sum / 8).ToString("0.000"));
                            item["KPJ"] = pjValue.ToString();

                            sum = 0;
                            for (int i = 1; i < 9; i++)
                            {
                                sum = sum + Math.Pow(GetSafeDouble(item["ZNNJ" + i]) - pjValue, 2);
                                item["NJXS"] = sum.ToString("0.000");
                            }

                            item["KBZC"] = Math.Sqrt(sum / 7).ToString("0.0000");

                            string extraKBZC = extraNJFields["KBZC"];  //≤0.0100
                            string extraKPJ = extraNJFields["KPJ"]; //0.110~0.150
                            List<string> listKPJ = extraKPJ.Split('~').ToList();

                            if (GetSafeDouble(item["KLHZBZC"]) <= GetSafeDouble(extraKBZC.Substring(1)) && GetSafeDouble(item["KLHZPJ"]) >= GetSafeDouble(listKPJ[0]) && GetSafeDouble(item["KLHZPJ"]) <= GetSafeDouble(listKPJ[1]))
                            {
                                item["NJXSPD"] = "合格";
                            }
                            else
                            {
                                item["NJXSPD"] = "不合格";
                            }
                        }
                        #endregion
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
