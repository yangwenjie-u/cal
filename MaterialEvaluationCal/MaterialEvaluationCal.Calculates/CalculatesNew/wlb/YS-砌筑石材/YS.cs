using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    /*砌筑石材*/
    public class YS : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_YSXSDJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_YSS = data["S_YS"];
            if (!data.ContainsKey("M_YS"))
            {
                data["M_YS"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_YS"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            double sum = 0;
            string Hgxm = "";
            string BHGXM = "";
            bool sign = true;
            foreach (var sItem in S_YSS)
            {
                //获得换算系数
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["GGXH"] == sItem["GGXH"]);
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                else
                {
                    sItem["KYZSXS"] = extraFieldsDj["HSXS"].Trim();
                }
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                string sjqd = "";

                #region 获取设计强度等级
                if (sItem["SJDJ"].Contains("20"))
                {
                    sjqd = "≥20";
                }
                else if (sItem["SJDJ"].Contains("30"))
                {
                    sjqd = "≥30";
                }
                else if (sItem["SJDJ"].Contains("40"))
                {
                    sjqd = "≥40";
                }
                else if (sItem["SJDJ"].Contains("50"))
                {
                    sjqd = "≥50";
                }
                else if (sItem["SJDJ"].Contains("60"))
                {
                    sjqd = "≥60";
                }
                else if (sItem["SJDJ"].Contains("80"))
                {
                    sjqd = "≥80";
                }
                else if (sItem["SJDJ"].Contains("100"))
                {
                    sjqd = "≥100";
                }
                else
                {
                    sjqd = "----";
                }
                #endregion

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    sum = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        sign = IsNumeric(sItem["SYPHHZ" + i].Trim());
                        sign = IsNumeric(sItem["SYCD" + i].Trim());
                        sign = IsNumeric(sItem["SYKD" + i].Trim());
                    }
                    if (sign)
                    {
                        //抗压强度MPa = 破坏荷载（kN） / 面积mm²
                        for (int i = 1; i < 4; i++)
                        {
                            sItem["DKKYQD" + i] = Round(GetSafeDouble(sItem["SYPHHZ" + i].Trim()) * 1000 / (GetSafeDouble(sItem["SYCD" + i].Trim()) * GetSafeDouble(sItem["SYKD" + i].Trim())), 1).ToString("0.0"); ;
                            sum = sum + GetSafeDouble(sItem["DKKYQD" + i]);
                        }
                        //平均抗压强度
                        sItem["KYQDPJ"] = Round(sum / 3, 1).ToString("0.0");
                        //换算  抗压强度代表值
                        sItem["KYDBQD"] = Round(GetSafeDouble(sItem["KYQDPJ"]) * GetSafeDouble(sItem["KYZSXS"]), 1).ToString("0.0");
                    }
                    else
                    {
                        throw new SystemException("试验数据录入有误");
                    }

                    MItem[0]["PD_KY"] = IsQualified(sjqd, sItem["KYDBQD"], true);
                    if (MItem[0]["PD_KY"] == "符合")
                    {
                        Hgxm = Hgxm + "抗压强度";
                    }
                    else if (MItem[0]["PD_KY"] == "不符合")
                    {
                        BHGXM = BHGXM + "抗压强度";
                        itemHG = false;
                    }

                }
                else
                {
                    sItem["KYQDPJ"] = "----";
                    sItem["KYDBQD"] = "----";
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["DKKYQD" + i] = "----";
                        sItem["SYPHHZ" + i] = "----";
                    }
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
            if (Hgxm.Length > 0)
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else if (BHGXM.Length > 0)
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目抗压强度不符合要求。";
                mjcjg = "不合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /***********************代码结束********************/
        }
    }
}
