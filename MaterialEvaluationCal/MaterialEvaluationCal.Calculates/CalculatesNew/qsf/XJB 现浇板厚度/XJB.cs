using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class XJB : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            int mbhggs = 0;//不合格数量
            int zs = 0; //组数
            //var extraDJ = dataExtra["BZ_XJB_DJ"];

            var data = retData;

            var SItem = data["S_XJB"];
            var MItem = data["M_XJB"];
            if (!data.ContainsKey("M_XJB"))
            {
                data["M_XJB"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            int mbHggs1 = 0, mbHggs2 = 0, mbHggs3 = 0;
            bool sign = true, mark = true;
            string djjg = "";

            //单组判定
            foreach (var sItem in SItem)
            {
                zs++;
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                sItem["W_YXPC"] = "-5~10";

                #region 楼板实测厚度
                if (jcxm.Contains("、楼板实测厚度、"))
                {
                    double schd1 = 0, schd2 = 0, schd3 = 0, pjhd = 0, sjhd = 0, c = 0;
                    //if (sItem["S_FS"] == "否")   //是否复试
                    //{
                        schd1 = string.IsNullOrEmpty(sItem["SCHD1"]) ? 0 : double.Parse(sItem["SCHD1"].Trim());
                        schd2 = string.IsNullOrEmpty(sItem["SCHD2"]) ? 0 : double.Parse(sItem["SCHD2"].Trim());
                        schd3 = string.IsNullOrEmpty(sItem["SCHD3"]) ? 0 : double.Parse(sItem["SCHD3"].Trim());
                    //}
                    //else
                    //{
                    //    schd1 = string.IsNullOrEmpty(sItem["W_HD1"]) ? 0 : double.Parse(sItem["W_HD1"].Trim());
                    //    schd2 = string.IsNullOrEmpty(sItem["W_HD2"]) ? 0 : double.Parse(sItem["W_HD2"].Trim());
                    //    schd3 = string.IsNullOrEmpty(sItem["W_HD3"]) ? 0 : double.Parse(sItem["W_HD3"].Trim());
                    //}

                    pjhd = (schd1 + schd2 + schd3) / 3;
                    sItem["SCPJHD"] = pjhd.ToString("0.0");
                    sjhd = string.IsNullOrEmpty(sItem["SJHD"]) ? 0 : double.Parse(sItem["SJHD"].Trim());
                    c = pjhd - sjhd;    //平均厚度与设计厚度差
                    if (sjhd > 0 && c >= -5 && c <= 10)
                    {
                        sItem["SFCCN"] = "0";   //合格
                    }
                    else
                    {
                        sItem["SFCCN"] = "1";   //不合格
                        mbhggs++;
                    }
                }
                else
                {
                    sItem["W_YXPC"] = "----";
                }
                #endregion
            }


            //综合判定
            MItem[0]["ZDS"] = zs.ToString();    //总组数
            MItem[0]["ZHGD"] = (zs - mbhggs).ToString();    //合格组数
            double hg = 0;  //合格率
            if (zs > 0)
            {
                hg = (zs - mbhggs) / zs;
            }
            MItem[0]["ZHGL"] = (hg * 100).ToString("0"); //合格率
            if (hg < 0.7)
            {
                mAllHg = false;
                jsbeizhu = "合格率小于70%不符合标准要求。";
            }
            else if (hg >= 0.7 && hg < 0.8)
            {
                mAllHg = false;
                jsbeizhu = "合格率小于80%但不小于70%需要复试。";
            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_XJB"))
            {
                data["M_XJB"] = new List<IDictionary<string, string>>();
            }
            var M_XJB = data["M_XJB"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_XJB == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_XJB.Add(m);
            }
            else
            {
                M_XJB[0]["JCJG"] = mjcjg;
                M_XJB[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}

