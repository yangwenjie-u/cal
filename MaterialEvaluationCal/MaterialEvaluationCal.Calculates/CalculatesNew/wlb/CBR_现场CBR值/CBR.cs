using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
/*安全帽*/
namespace Calculates
{
    public class CBR : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_CBR_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            //bool itemHG = true;//判断单组是否合格
            var S_CBRS = data["S_CBR"];
            var Y_CZB = data["Y_CZB"];
            if (!data.ContainsKey("M_CBR"))
            {
                data["M_CBR"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_CBR"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var jcxmBhg = "";
            var jcxmCur = "";
            bool sign = true;
            bool itemHG = true;
            foreach (var sItem in S_CBRS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                #region 膨胀量
                if (jcxm.Contains("、膨胀量、"))
                {
                    sign = true;
                    for (int i = 1; i < 4; i++)
                    {
                        sign = IsNumeric(sItem["PSQSJGD" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["PSQSJGD" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["PSQSJGD" + i + "_3"]) ? sign : false;
                        sign = IsNumeric(sItem["PSHSJGD" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["PSHSJGD" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["PSHSJGD" + i + "_3"]) ? sign : false;
                    }
                    if (sign)
                    {
                        //膨胀量 % = （泡水后试件高度 - 泡水前试件高度）/泡水前试件高度 *100   两位小数
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["PZL" + i + "_1"] = Math.Round((GetSafeDouble(sItem["PSHSJGD" + i + "_1"].Trim()) - GetSafeDouble(sItem["PSQSJGD" + i + "_1"].Trim())) / GetSafeDouble(sItem["PSQSJGD" + i + "_1"].Trim()) * 100, 2).ToString("0.00");
                            sItem["PZL" + i + "_2"] = Math.Round((GetSafeDouble(sItem["PSHSJGD" + i + "_2"].Trim()) - GetSafeDouble(sItem["PSQSJGD" + i + "_2"].Trim())) / GetSafeDouble(sItem["PSQSJGD" + i + "_2"].Trim()) * 100, 2).ToString("0.00");
                            sItem["PZL" + i + "_3"] = Math.Round((GetSafeDouble(sItem["PSHSJGD" + i + "_3"].Trim()) - GetSafeDouble(sItem["PSQSJGD" + i + "_3"].Trim())) / GetSafeDouble(sItem["PSQSJGD" + i + "_3"].Trim()) * 100, 2).ToString("0.00");
                        }
                        //30击膨胀量平均值
                        MItem[0]["PZL1"] = Math.Round((GetSafeDouble(sItem["PZL1_1"]) + GetSafeDouble(sItem["PZL1_2"]) + GetSafeDouble(sItem["PZL1_3"])) / 3, 2).ToString("0.00");
                        //50击膨胀量平均值
                        MItem[0]["PZL2"] = Math.Round((GetSafeDouble(sItem["PZL2_1"]) + GetSafeDouble(sItem["PZL2_2"]) + GetSafeDouble(sItem["PZL2_3"])) / 3, 2).ToString("0.00");
                        //98击膨胀量平均值
                        MItem[0]["PZL3"] = Math.Round((GetSafeDouble(sItem["PZL3_1"]) + GetSafeDouble(sItem["PZL3_2"]) + GetSafeDouble(sItem["PZL3_3"])) / 3, 2).ToString("0.00");

                    }
                    else
                    {
                        throw new SystemException("膨胀量试验数据录入有误");
                    }

                }
                else
                {

                }
                #endregion

                #region 密度
                if (jcxm.Contains("、密度、"))
                {
                    sign = true;
                    for (int i = 1; i < 4; i++)
                    {
                        sign = IsNumeric(sItem["TZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["TZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["TZL" + i + "_3"]) ? sign : false;
                        sign = IsNumeric(sItem["TSJZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["TSJZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["TSJZL" + i + "_3"]) ? sign : false;
                        sign = IsNumeric(sItem["TTJ" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["TTJ" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["TTJ" + i + "_3"]) ? sign : false;
                        //sign = IsNumeric(sItem["HSL" + i + "_1"]) ? sign : false;
                        //sign = IsNumeric(sItem["HSL" + i + "_2"]) ? sign : false;
                        //sign = IsNumeric(sItem["HSL" + i + "_3"]) ? sign : false;
                        sign = IsNumeric(sItem["HZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["HZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["HZL" + i + "_3"]) ? sign : false;
                        sign = IsNumeric(sItem["HJSTZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["HJSTZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["HJSTZL" + i + "_3"]) ? sign : false;
                        sign = IsNumeric(sItem["HJGTZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["HJGTZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["HJGTZL" + i + "_3"]) ? sign : false;
                    }
                    if (sign)
                    {
                        //湿密度 = （ 筒加试件质量 - 筒质量 ）/ 筒容积（2177）    精确至0.01
                        //干密度 =  湿密度/（1+0.01*含水率）  精确至0.01
                        for (int i = 1; i < 4; i++)
                        {
                            //湿密度
                            sItem["SMD" + i + "_1"] = Round((GetSafeDouble(sItem["TSJZL" + i + "_1"].Trim()) - GetSafeDouble(sItem["TZL" + i + "_1"].Trim())) / GetSafeDouble(sItem["TTJ" + i + "_1"].Trim()), 2).ToString("0.00");
                            sItem["SMD" + i + "_2"] = Round((GetSafeDouble(sItem["TSJZL" + i + "_2"].Trim()) - GetSafeDouble(sItem["TZL" + i + "_2"].Trim())) / GetSafeDouble(sItem["TTJ" + i + "_2"].Trim()), 2).ToString("0.00");
                            sItem["SMD" + i + "_3"] = Round((GetSafeDouble(sItem["TSJZL" + i + "_3"].Trim()) - GetSafeDouble(sItem["TZL" + i + "_3"].Trim())) / GetSafeDouble(sItem["TTJ" + i + "_3"].Trim()), 2).ToString("0.00");
                            //含水率  =  （盒加湿土质量 - 盒加干土质量） / （盒加干土质量 - 盒质量） * 100  保留一位小数
                            sItem["HSL" + i + "_1"] = Round((GetSafeDouble(sItem["HJSTZL" + i + "_1"].Trim()) - GetSafeDouble(sItem["HJGTZL" + i + "_1"].Trim()))
                                / (GetSafeDouble(sItem["HJGTZL" + i + "_1"].Trim()) - GetSafeDouble(sItem["HZL" + i + "_1"].Trim())) * 100, 1).ToString("0.0");

                            sItem["HSL" + i + "_2"] = Round((GetSafeDouble(sItem["HJSTZL" + i + "_2"].Trim()) - GetSafeDouble(sItem["HJGTZL" + i + "_2"].Trim()))
                                / (GetSafeDouble(sItem["HJGTZL" + i + "_2"].Trim()) - GetSafeDouble(sItem["HZL" + i + "_2"].Trim())) * 100, 1).ToString("0.0");

                            sItem["HSL" + i + "_3"] = Round((GetSafeDouble(sItem["HJSTZL" + i + "_3"].Trim()) - GetSafeDouble(sItem["HJGTZL" + i + "_3"].Trim()))
                                / (GetSafeDouble(sItem["HJGTZL" + i + "_3"].Trim()) - GetSafeDouble(sItem["HZL" + i + "_3"].Trim())) * 100, 1).ToString("0.0");
                            //干密度
                            sItem["GMD" + i + "_1"] = Round(GetSafeDouble(sItem["SMD" + i + "_1"]) / (1 + 0.01 * GetSafeDouble(sItem["HSL" + i + "_1"].Trim())), 2).ToString("0.00");
                            sItem["GMD" + i + "_2"] = Round(GetSafeDouble(sItem["SMD" + i + "_2"]) / (1 + 0.01 * GetSafeDouble(sItem["HSL" + i + "_2"].Trim())), 2).ToString("0.00");
                            sItem["GMD" + i + "_3"] = Round(GetSafeDouble(sItem["SMD" + i + "_3"]) / (1 + 0.01 * GetSafeDouble(sItem["HSL" + i + "_3"].Trim())), 2).ToString("0.00");
                        }
                        //平均干密度
                        sItem["PJGMD1"] = Round((GetSafeDouble(sItem["GMD1_1"]) + GetSafeDouble(sItem["GMD1_2"]) + GetSafeDouble(sItem["GMD1_3"])) / 3, 2).ToString("0.00");
                        sItem["PJGMD2"] = Round((GetSafeDouble(sItem["GMD2_1"]) + GetSafeDouble(sItem["GMD2_2"]) + GetSafeDouble(sItem["GMD2_3"])) / 3, 2).ToString("0.00");
                        sItem["PJGMD3"] = Round((GetSafeDouble(sItem["GMD3_1"]) + GetSafeDouble(sItem["GMD3_2"]) + GetSafeDouble(sItem["GMD3_3"])) / 3, 2).ToString("0.00");
                    }
                    else
                    {
                        throw new SystemException("密度试验数据录入有误");
                    }
                }
                else
                {

                }
                #endregion

                #region 吸水量
                if (jcxm.Contains("、吸水量、"))
                {
                    sign = true;
                    for (int i = 1; i < 4; i++)
                    {
                        sign = IsNumeric(sItem["PSHTSJZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["PSHTSJZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["PSHTSJZL" + i + "_3"]) ? sign : false;
                        sign = IsNumeric(sItem["TSJZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["TSJZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["TSJZL" + i + "_3"]) ? sign : false;
                    }
                    //泡水后试件吸水量 =  泡水后筒加试件质量 - 筒加试件质量
                    if (sign)
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["XSL" + i + "_1"] = Round(GetSafeDouble(sItem["PSHTSJZL" + i + "_1"].Trim()) - GetSafeDouble(sItem["TSJZL" + i + "_1"].Trim()), 1).ToString("0");
                            sItem["XSL" + i + "_2"] = Round(GetSafeDouble(sItem["PSHTSJZL" + i + "_2"].Trim()) - GetSafeDouble(sItem["TSJZL" + i + "_2"].Trim()), 1).ToString("0");
                            sItem["XSL" + i + "_3"] = Round(GetSafeDouble(sItem["PSHTSJZL" + i + "_3"].Trim()) - GetSafeDouble(sItem["TSJZL" + i + "_3"].Trim()), 1).ToString("0");
                        }
                        //平均吸水量
                        sItem["XSLPJ1"] = Round((GetSafeDouble(sItem["XSL1_1"]) + GetSafeDouble(sItem["XSL1_2"]) + GetSafeDouble(sItem["XSL1_3"])) / 3, 1).ToString("0");
                        sItem["XSLPJ2"] = Round((GetSafeDouble(sItem["XSL2_1"]) + GetSafeDouble(sItem["XSL2_2"]) + GetSafeDouble(sItem["XSL2_3"])) / 3, 1).ToString("0");
                        sItem["XSLPJ3"] = Round((GetSafeDouble(sItem["XSL3_1"]) + GetSafeDouble(sItem["XSL3_2"]) + GetSafeDouble(sItem["XSL3_3"])) / 3, 1).ToString("0");
                    }
                    else
                    {
                        throw new SystemException("吸水量试验数据录入有误");
                    }
                }
                else
                {

                }
                #endregion

                #region 含水率
                if (jcxm.Contains("、含水率、"))
                {
                    sign = true;
                    for (int i = 1; i < 4; i++)
                    {
                        sign = IsNumeric(sItem["HZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["HZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["HZL" + i + "_3"]) ? sign : false;
                        sign = IsNumeric(sItem["HJSTZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["HJSTZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["HJSTZL" + i + "_3"]) ? sign : false;
                        sign = IsNumeric(sItem["HJGTZL" + i + "_1"]) ? sign : false;
                        sign = IsNumeric(sItem["HJGTZL" + i + "_2"]) ? sign : false;
                        sign = IsNumeric(sItem["HJGTZL" + i + "_3"]) ? sign : false;
                    }
                    if (sign)
                    {

                        for (int i = 1; i < 4; i++)
                        {
                            //水分质量 =    盒加湿土质量 - 盒加干土质量  两位小数
                            sItem["SFZL" + i + "_1"] = Round(GetSafeDouble(sItem["HJSTZL" + i + "_1"]) - GetSafeDouble(sItem["HJGTZL" + i + "_1"]), 2).ToString("0.00");
                            sItem["SFZL" + i + "_2"] = Round(GetSafeDouble(sItem["HJSTZL" + i + "_2"]) - GetSafeDouble(sItem["HJGTZL" + i + "_2"]), 2).ToString("0.00");
                            sItem["SFZL" + i + "_3"] = Round(GetSafeDouble(sItem["HJSTZL" + i + "_3"]) - GetSafeDouble(sItem["HJGTZL" + i + "_3"]), 2).ToString("0.00");
                            //干土质量
                            sItem["GTZL" + i + "_1"] = Round(GetSafeDouble(sItem["HJGTZL" + i + "_1"]) - GetSafeDouble(sItem["HZL" + i + "_1"]), 2).ToString("0.00");
                            sItem["GTZL" + i + "_2"] = Round(GetSafeDouble(sItem["HJGTZL" + i + "_2"]) - GetSafeDouble(sItem["HZL" + i + "_2"]), 2).ToString("0.00");
                            sItem["GTZL" + i + "_3"] = Round(GetSafeDouble(sItem["HJGTZL" + i + "_3"]) - GetSafeDouble(sItem["HZL" + i + "_3"]), 2).ToString("0.00");
                        }
                        //含水率 密度试验中已经计算
                        sItem["HSL1"] = Round((GetSafeDouble(sItem["HSL1_1"]) + GetSafeDouble(sItem["HSL1_2"]) + GetSafeDouble(sItem["HSL1_3"])) / 3, 1).ToString("0.0");
                        sItem["HSL2"] = Round((GetSafeDouble(sItem["HSL2_1"]) + GetSafeDouble(sItem["HSL2_2"]) + GetSafeDouble(sItem["HSL2_3"])) / 3, 1).ToString("0.0");
                        sItem["HSL3"] = Round((GetSafeDouble(sItem["HSL3_1"]) + GetSafeDouble(sItem["HSL3_2"]) + GetSafeDouble(sItem["HSL3_3"])) / 3, 1).ToString("0.0");
                    }
                    else
                    {
                        throw new SystemException("含水率试验数据录入有误");
                    }
                }
                else
                {

                }
                #endregion

                #region 承载比
                if (jcxm.Contains("、承载比、"))
                {
                    //计算平均承载比
                    for (int i = 1; i < 10; i++)
                    {
                        if (Y_CZB != null && Y_CZB.Count != 0)
                        {
                            sItem["CZB" + i] = Y_CZB[i - 1]["CBRZ25"];
                        }
                        else
                        {
                            throw new SystemException("第"+i+"组承载比相关试验数据录入不完整或曲线计算有误");
                        }
                    }

                    sItem["PJCZB1"] = Math.Round((GetSafeDouble(sItem["CZB1"]) + GetSafeDouble(sItem["CZB2"]) + GetSafeDouble(sItem["CZB3"])) / 3, 1).ToString("0.0");
                    sItem["PJCZB2"] = Math.Round((GetSafeDouble(sItem["CZB4"]) + GetSafeDouble(sItem["CZB5"]) + GetSafeDouble(sItem["CZB6"])) / 3, 1).ToString("0.0");
                    sItem["PJCZB3"] = Math.Round((GetSafeDouble(sItem["CZB7"]) + GetSafeDouble(sItem["CZB8"]) + GetSafeDouble(sItem["CZB9"])) / 3, 1).ToString("0.0");
                }
                else
                {

                }
                #endregion

                //单组判断
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                //jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目检测结果如上。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求	。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
