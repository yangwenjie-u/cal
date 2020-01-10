
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GBT_11969_2008 : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/

            var extraGMD = dataExtra["BZ_QK2_GMDJB"];
            var extraQK2DJ = dataExtra["BZ_QK2_DJ"];
            var extraQK2QDJB = dataExtra["BZ_QK2_QDJB"];

            //var dataItems = new Dictionary<string, Dictionary<string, List<Dictionary<string, string>>>>();
            List<Dictionary<string, double>> gmdList = new List<Dictionary<string, double>>();

            var jcxmItems = retData.Select(u => u.Key).ToArray();
            foreach (var jcxm in jcxmItems)
            {
                //GB/T 11973-1997  导热系数、冻后强度、干密度、抗压、质量损失
                //GB/T 11969-2008  干密度，含水率，吸水率 ，抗压 ，抗折，干燥收缩,抗冻性，碳化，干湿循环
                switch (jcxm)
                {
                    case "导热系数":
                        break;
                    case "冻后强度":
                        break;
                    case "干密度": // 公式计算 烘干后质量（g）÷体积（mm3）  要求精确到 1kg / m3
                        var gmdItems = retData["干密度"]["S_QK2"];
                        foreach (var item in gmdItems)
                        {
                            //计算每一块试件干密度值
                            for (int i = 1; i < 4; i++)
                            {
                                for (int j = 1; j < 4; j++)
                                {
                                    //公式计算 烘干后质量（g）÷体积（mm3）  要求精确到 1kg/m3
                                    item["GMD" + i + "_" + j] = (GetDouble(item["HGHZL" + i + "_" + j].ToString()) / (GetDouble(item["CD" + i + "_" + j].ToString()) * GetDouble(item["KD" + i + "_" + j].ToString()) * GetDouble(item["GD" + i + "_" + j].ToString())) * 1000000).ToString("0");
                                }
                            }

                            //计算每组试件平均干密度
                            item["GMD1"] = (item.Where(u => u.Key.Contains("GMD1_")).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0");
                            item["GMD2"] = (item.Where(u => u.Key.Contains("GMD2_")).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0");
                            item["GMD3"] = (item.Where(u => u.Key.Contains("GMD3_")).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0");

                            //计算每组（三个）试件的平均干密度值
                            item["GMDPJ"] = (item.Where(u => u.Key.Contains("GMD") && u.Key.Length == 4).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0");

                            //SJDJ, --干密度等级  PZ,--砌块等级 优等品（A）/合格品（B）
                            var extraFieldsGMD = extraGMD.FirstOrDefault(u => u.Values.Contains(item["SJDJ"]) && u.Values.Contains(item["PZ"]));

                            //干密度要求 (<=标准)
                            item["GMDYQ"] = "<=" + extraFieldsGMD["GMD"];

                            //是否合格
                            item["GMDPD"] = GetSafeDouble(item["GMDPJ"]) <= GetSafeDouble(extraFieldsGMD["GMD"]) ? "合格" : "不合格";
                        }

                        var rwxqData = retData["干密度"]["S_BY_RW_XQ"];
                        for (int i = 0; i < rwxqData.Count; i++)
                        {
                            if (gmdItems[i]["GMDPD"] == "合格")
                            {
                                rwxqData[i]["JCJG"] = "合格";
                                rwxqData[i]["JCJGMS"] = string.Format("干密度报告说明：第1组试件平均干密度为{0},第2组试件平均干密度为{1},第3组试件平均干密度为{2},平均干密度为{3}，符合GB/T_11969_2008规定的平均干密度{4}", gmdItems[i]["GMD1"], gmdItems[i]["GMD2"], gmdItems[i]["GMD3"], gmdItems[i]["GMDPJ"], gmdItems[i]["GMDYQ"]);
                            }
                            else
                            {
                                rwxqData[i]["JCJG"] = "不合格";
                                rwxqData[i]["JCJGMS"] = string.Format("干密度报告说明：第1组试件平均干密度为{0},第2组试件平均干密度为{1},第3组试件平均干密度为{2},平均干密度为{3}，不符合GB/T_11969_2008规定的平均干密度{4}", gmdItems[i]["GMD1"], gmdItems[i]["GMD2"], gmdItems[i]["GMD3"], gmdItems[i]["GMDPJ"], gmdItems[i]["GMDYQ"]);
                            }
                        }

                        break;
                    case "抗压":
                        //抗压=荷重(N)/面积(mm2) 精确到0.01MPa
                        var kyItems = retData["抗压"]["S_QK2"];
                        foreach (var item in kyItems)
                        {
                            //计算每一块试件干密度值
                            for (int i = 1; i < 4; i++)
                            {
                                for (int j = 1; j < 4; j++)
                                {
                                    //公式计算 抗压荷重（N）÷受力面积（mm2）  要求精确到 
                                    item["KYHZ" + i + "_" + j] = string.Format(Convert.ToString(GetDouble(item["KYHZ" + i + "_" + j].ToString()) * 1000 / (GetDouble(item["CD" + i + "_" + j].ToString()) * GetDouble(item["KD" + i + "_" + j].ToString()))), "0.01");
                                }
                            }

                            //计算每组试件抗压平均值
                            item["KYPJ1"] = (item.Where(u => u.Key.Contains("KYHZ1_")).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0.00");
                            item["KYPJ2"] = (item.Where(u => u.Key.Contains("KYHZ2_")).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0.00");
                            item["KYPJ3"] = (item.Where(u => u.Key.Contains("KYHZ3_")).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0.00");

                            //计算每组（三个）试件的抗压平均值
                            item["KYPJ"] = Convert.ToString(item.Where(u => u.Key.Contains("KYPJ") && u.Key.Length == 4).Sum(x => GetSafeDouble(x.Value)) / 3);

                            //获取抗压级别
                            //SJDJ, --干密度等级                 PZ,--砌块等级 优等品（A）/合格品（B）
                            var extraFieldsQK2D = extraQK2DJ.FirstOrDefault(u => u.Values.Contains(item["SJDJ"]) && u.Values.Contains(item["PZ"]));

                            //获取强度级别
                            string interestingLevel = extraFieldsQK2D["QDJB"].ToString();
                            //获取强度等级对应标准   PJBXY 平均值不小于  DKBXY  单块最小值不小于
                            var extraFieldsQK2QDJB = extraQK2QDJB.FirstOrDefault(u => u.Values.Contains(item["QDJB"]));

                            //是否合格   三组平均值都大于最小值 合格   （暂无保存）
                            //    item["KY"] = retData.Count(u => u.Key.Contains("KYPJ") && u.Key.Length == 4 && GetSafeDouble(u.Value.ToString()) > GetSafeDouble(extraFieldsQK2QDJB["DKBXY"])) == 3 ? "合格" : "不合格";

                            var kyRWXQData = retData["抗压"]["S_BY_RW_XQ"];
                            for (int i = 0; i < kyRWXQData.Count; i++)
                            {

                                if (kyItems[i]["GMDPD"] == "合格")
                                {
                                    kyRWXQData[i]["JCJG"] = "合格";
                                    kyRWXQData[i]["JCJGMS"] = string.Format("抗压报告说明：第1组试件平均抗压值为{0},第2组试件平均抗压值为{1},第3组试件平均抗压值为{2},平均抗压值为{3}，符合GB/T_11969_2008规定的平均抗压值最小值{4}，符合相应等级", kyItems[i]["KYPJ1"], kyItems[i]["KYPJ2"], kyItems[i]["KYPJ3"], kyItems[i]["KYPJ"], extraFieldsQK2QDJB["DKBXY"]);
                                }
                                else
                                {
                                    kyRWXQData[i]["JCJG"] = "不合格";
                                    kyRWXQData[i]["JCJGMS"] = string.Format("抗压报告说明：第1组试件平均抗压值为{0},第2组试件平均抗压值为{1},第3组试件平均抗压值为{2},平均抗压值为{3}，不符合GB/T_11969_2008规定的平均抗压值最小值{4}，最终不符合相应等级", kyItems[i]["KYPJ1"], kyItems[i]["KYPJ2"], kyItems[i]["KYPJ3"], kyItems[i]["KYPJ"], extraFieldsQK2QDJB["DKBXY"]);
                                }
                            }
                           
                        }
                        break;
                    case "质量损失":  //含水率??  //含水率=烘干前-烘干后） /烘干后   *100 精确到0.1% 精确到0.1%
                        var hslItems = retData["质量损失"]["S_QK2"];
                        foreach (var item in hslItems)
                        {
                            //计算每一块试件含水率
                            for (int i = 1; i < 4; i++)
                            {
                                for (int j = 1; j < 4; j++)
                                {
                                    //公式计算 烘干前-烘干后） /烘干后   *100  要求精确到 0.1%  HHGHZL-含水率烘干后  HGQZL-烘干前
                                    item["HSL" + i + "_" + j] = (((GetDouble(item["HGQZL" + i + "_" + j].ToString()) - GetDouble(item["HHGHZL" + i + "_" + j].ToString())) / GetDouble(item["HHGHZL" + i + "_" + j].ToString())) * 100).ToString("0.0");
                                }
                            }

                            //计算每组试件平均干密度
                            item["HSL1"] = (item.Where(u => u.Key.Contains("HSL1_")).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0.0");
                            item["HSL2"] = (item.Where(u => u.Key.Contains("HSL2_")).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0.0");
                            item["HSL3"] = (item.Where(u => u.Key.Contains("HSL3_")).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0.0");

                            //计算平均含水率
                            item["HSLPJ"] = (item.Where(u => u.Key.Contains("HSL") && u.Key.Length == 4).Sum(x => GetSafeDouble(x.Value)) / 3).ToString("0.0");

                            var extraFieldsQK2D = extraQK2DJ.FirstOrDefault(u => u.Values.Contains(item["SJDJ"]) && u.Values.Contains(item["PZ"]));

                            //含水率标准
                            item["HSLYQ"] = extraFieldsQK2D["HSL"];

                            //HSLPD 含水率判定
                        }
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
