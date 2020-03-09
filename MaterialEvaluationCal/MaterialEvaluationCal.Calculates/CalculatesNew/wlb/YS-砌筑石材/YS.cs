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
            //var extraDJ = dataExtra["BZ_YS_DJ"];

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

            foreach (var sItem in S_YSS)
            {

                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    //循环得到每个抗压强度值
                    //double sum = 0;
                    double md1, md2;
                    for (int i = 1; i <= 6; i++)
                    {
                        md1 = GetSafeDouble(sItem["S_JMJ" + i]);
                        md2 = GetSafeDouble(sItem["S_PHHZ" + i]);
                        md1 = md2 / md1;
                        md1 = Math.Round(md1, 1);
                        sItem["W_KY" + i] = md1.ToString("0.0");
                        sum = sum + md1;//和
                    }
                    sum = Math.Round(sum / 6, 1);//平均值
                    sItem["W_KY"] = sum.ToString("0.0");
                }
                else
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        sItem["W_KY" + i] = "----";
                    }
                    sItem["W_KY"] = "----";
                }
                #endregion

                MItem[0]["PD_KY"] = IsQualified(sItem["SJ_KY"], sItem["W_KY"], true);
                if (MItem[0]["PD_KY"] == "符合")
                {
                    Hgxm = Hgxm + "抗压强度";
                }
                else if (MItem[0]["PD_KY"] == "不符合")
                {
                    BHGXM = BHGXM + "抗压强度";
                    itemHG = false;
                }


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
            if (Hgxm.Length>0)
            {
                jsbeizhu = "经检测，该岩石试样所检项目" + Hgxm + "，符合委托方提供的设计要求。";
            }
            else if (BHGXM.Length>0)
            {
                jsbeizhu = "经检测，该岩石试样所检项目" + BHGXM + "，不符合委托方提供的设计要求。";
            }
            else
            {
                jsbeizhu = "经试验，该岩石试样所检项目结果详见报告";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /***********************代码结束********************/
        }
    }
}
