using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GBT3091_2015 : BaseMethods
    {
        public static bool Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/

            var extraData = dataExtra["BZ_GG_DJ"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();
            foreach (var jcxm in jcxmItems)
            {
                switch (jcxm)
                {
                    case "拉伸":
                        var LSItems = retData[jcxm]["S_GG"];
                        foreach (var item in LSItems)
                        {
                            string zjm = item["SJGGXH"].Split(' ').ToList()[1];
                            var extraLSFields = extraData.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(item["GGXH"])) && (u.ContainsKey("ZJM") && u.Values.Contains(zjm)));

                            if (extraLSFields["LSGS"] == "1")  //拉几根
                            {
                                continue;
                            }

                            item["JMJ1"] = ((3.14159 * GetDouble(item["GGWJ1"]) / 2 * GetDouble(item["GGWJ1"]) / 2) - 3.14159 * GetDouble(item["GGWJ1"]) / 2 - GetDouble(item["GGBH1"]) * GetDouble(item["GGWJ1"]) / 2 - GetDouble(item["GGBH1"])).ToString("0.00");
                            item["JMJ2"] = ((3.14159 * GetDouble(item["GGWJ2"]) / 2 * GetDouble(item["GGWJ2"]) / 2) - 3.14159 * GetDouble(item["GGWJ2"]) / 2 - GetDouble(item["GGBH2"]) * GetDouble(item["GGWJ2"]) / 2 - GetDouble(item["GGBH1"])).ToString("0.00");

                            //原始标距 ysbj
                            //sitem["ysbj = Round(5.65 * Sqr(mMj1) / 5, 0) * 5
                            //sitem["ysbj2 = Round(5.65 * Sqr(mMj2) / 5, 0) * 5

                            item["YSBJ1"] = (Math.Round(Math.Sqrt(5.65 * GetDouble(item["JMJ1"]) / 5), 0) * 5).ToString();
                            item["YSBJ2"] = (Math.Round(Math.Sqrt(5.65 * GetDouble(item["JMJ2"]) / 5), 0) * 5).ToString();

                            if (string.IsNullOrEmpty(item["KLHZ1"]))
                            {
                                //输出结果
                                continue;
                            }

                            //屈服
                            item["QFQD1"] = (1000 * GetDouble(item["QFZH1"]) / GetDouble(item["JMJ1"])).ToString();
                            item["QFQD2"] = (1000 * GetDouble(item["QFZH2"]) / GetDouble(item["JMJ1"])).ToString();
                            //抗拉
                            item["KLQD1"] = (1000 * GetDouble(item["KLHZ1"]) / GetDouble(item["JMJ1"])).ToString();
                            item["KLQD2"] = (1000 * GetDouble(item["KLHZ2"]) / GetDouble(item["JMJ1"])).ToString();
                            //SCL1 伸长率
                            double scl1 = GetDouble(item["SCZ1"]) - GetDouble(item["YSBJ"]) * 100 / GetDouble(item["YSBJ"]);
                            double scl2 = GetDouble(item["SCZ2"]) - GetDouble(item["YSBJ"]) * 100 / GetDouble(item["YSBJ"]);
                            item["SCL1"] = Math.Round(scl1, 0).ToString();
                            item["SCL2"] = Math.Round(scl2, 0).ToString();

                            //帮组表信息
                            item["G_QFQD"] = extraLSFields["QFHGGS"]; //屈服强度标准
                            item["HG_QFQD"] = item.Count(u => u.Key.Contains("QFQD") && u.Key.Length == 5 && GetDouble(u.Value) >= GetDouble(item["G_QFQD"])) == GetInt(extraLSFields["QFHGGS"]) ? "合格" : "不合格";

                            item["G_SCL"] = extraLSFields["G_SCL"]; //伸长率标准
                            item["HG_SCL"] = item.Count(u => u.Key.Contains("SCL") && u.Key.Length == 4 && GetDouble(u.Value) >= GetDouble(item["G_SCL"])) == GetInt(extraLSFields["SCHGGS"]) ? "合格" : "不合格";

                            item["G_KLQD"] = extraLSFields["G_KLQD"];
                            item["HG_KLQD"] = item.Count(u => u.Key.Contains("KLQD") && u.Key.Length == 5 && GetDouble(u.Value) >= GetDouble(item["G_KLQD"])) == GetInt(extraLSFields["KLHGGS"]) ? "合格" : "不合格";

                        }
                        break;
                    case "压扁":
                        var YBItems = retData[jcxm]["S_GG"];
                        foreach (var item in YBItems)
                        {
                            string zjm = item["SJGGXH"].Split(' ').ToList()[1];
                            var extraLSFields = extraData.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(item["GGXH"])) && (u.ContainsKey("ZJM") && u.Values.Contains(zjm)));

                            int LWHGGS = GetInt(extraLSFields["LWHGGS"]);
                            item["HG_YB"] = item.Count(u => u.Key.Contains("YB") && u.Key.Length == 3) == GetInt(extraLSFields["YBHGGS"]) ? "合格" : "不合格"; ;
                        }
                        break;
                    case "弯曲":
                        var WQItems = retData[jcxm]["S_GG"];
                        foreach (var item in WQItems)
                        {
                            string zjm = item["SJGGXH"].Split(' ').ToList()[1];
                            var extraLSFields = extraData.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(item["GGXH"])) && (u.ContainsKey("ZJM") && u.Values.Contains(zjm)));

                            //G_LWl 冷弯要求

                            int LWHGGS = GetInt(extraLSFields["LWHGGS"]);
                            item["HG_LW"] = item.Count(u => u.Key.Contains("LW") && u.Key.Length == 3) == GetInt(extraLSFields["LWHGGS"]) ? "合格" : "不合格"; ;

                        }
                        break;
                    case "外径":
                        var wjItems = retData["外径"]["S_GG"];
                        foreach (var item in wjItems)
                        {
                            //计算外径
                            item["GGWJ1"] = ((GetDouble(item["GGWJ1_1"]) + GetDouble(item["GGWJ1_2"])) / 2).ToString("0.00");
                            item["GGWJ2"] = ((GetDouble(item["GGWJ2_1"]) + GetDouble(item["GGWJ2_2"])) / 2).ToString("0.00");


                            //MC='直缝电焊钢管φ48.3×3.5' and ZJM='Q235A'
                            //获取规格
                            string zjm = item["SJGGXH"].Split(' ').ToList()[1];
                            var extraWJFields = extraData.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(item["GGXH"])) && (u.ContainsKey("ZJM") && u.Values.Contains(zjm)));

                            //外径标准
                            item["WJPD"] = extraWJFields["GGWJ"];

                            //允许偏差
                            item["GWJPC"] = extraWJFields["WJPC"];

                            //是否合格 //检测值-标准值 决定值 < 允许偏差
                            if (Math.Abs(GetDouble(item["GGWJ1"]) - GetDouble(item["WJPD"])) < GetDouble(item["GWJPC"]) && Math.Abs(GetDouble(item["GGWJ2"]) - GetDouble(item["WJPD"])) < GetDouble(item["GWJPC"]))
                            {
                                //合格
                            }
                            else
                            {

                            }
                        }
                        break;
                    case "壁厚":
                        var BHItems = retData["壁厚"]["S_GG"];
                        foreach (var item in BHItems)
                        {
                            //计算壁厚
                            item["GGBH1"] = ((GetDouble(item["GGBH1_1"]) + GetDouble(item["GGBH1_2"])) / 2).ToString("0.00");
                            item["GGBH2"] = ((GetDouble(item["GGBH2_1"]) + GetDouble(item["GGBH2_2"])) / 2).ToString("0.00");
                            item["GGBH3"] = ((GetDouble(item["GGBH3_1"]) + GetDouble(item["GGBH3_2"])) / 2).ToString("0.00");
                            item["GGBH4"] = ((GetDouble(item["GGBH4_1"]) + GetDouble(item["GGBH4_2"])) / 2).ToString("0.00");

                            //MC='直缝电焊钢管φ48.3×3.5' and ZJM='Q235A'
                            //获取规格
                            string zjm = item["SJGGXH"].Split(' ').ToList()[1];
                            var extraWJFields = extraData.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(item["GGXH"])) && (u.ContainsKey("ZJM") && u.Values.Contains(zjm)));

                            //壁厚标准
                            item["GGGBH"] = extraWJFields["GGBH"];

                            //壁厚偏差
                            item["GBHPC"] = extraWJFields["BHPC"];

                            //是否合格 //检测值-标准值 决定值 < 允许偏差
                            if (Math.Abs(GetDouble(item["GGBH1"]) - GetDouble(item["GGGBH"])) < GetDouble(item["GBHPC"]) && Math.Abs(GetDouble(item["GGBH2"]) - GetDouble(item["GGGBH"])) < GetDouble(item["GBHPC"]) && Math.Abs(GetDouble(item["GGBH3"]) - GetDouble(item["GGGBH"])) < GetDouble(item["GBHPC"]) && Math.Abs(GetDouble(item["GGBH4"]) - GetDouble(item["GGGBH"])) < GetDouble(item["GBHPC"]))
                            {
                                //合格
                            }
                            else
                            {

                            }
                        }
                        break;
                    case "锈蚀深度":
                        var XSItems = retData["壁厚"]["S_GG"];
                        foreach (var item in XSItems)
                        {
                            //计算壁厚item["XSSD"]
                            if (item["YPXJ"] == "新")
                            {
                                //XSPD 锈蚀判定   XSSD锈蚀深度 赋值
                                item["XSPD"] = "----";
                                item["XSSD"] = "----";
                            }
                            else
                            {
                                //如果判定>深度，不合格

                                item["XSSD"] = ((GetDouble(item["XSSD1"]) + GetDouble(item["XSSD2"]) + GetDouble(item["XSSD3"])) / 3).ToString("0.00");

                                //wgpd 外观判定

                                string zjm = item["SJGGXH"].Split(' ').ToList()[1];
                                var extraWJFields = extraData.FirstOrDefault(u => (u.ContainsKey("MC") && u.Values.Contains(item["GGXH"])) && (u.ContainsKey("ZJM") && u.Values.Contains(zjm)));

                                //锈蚀深度标准
                                item["GXSSD"] = extraWJFields["XSSD"];

                                //是否合格 //检测值-标准值 决定值 < 允许偏差
                                if (GetDouble(item["XSSD"]) < GetDouble(item["GXSSD"]))
                                {
                                    //合格
                                    item["XSPD"] = "合格";
                                }
                                else
                                {
                                    item["XSPD"] = "不合格";
                                }
                            }
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
