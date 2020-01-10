using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    /*钢结构探伤（超声波）*/
    public class GUT : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_GUT_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_GUTS = data["S_GUT"];
            if (!data.ContainsKey("M_GUT"))
            {
                data["M_GUT"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GUT"];
            string mSjdj = "";
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            string mJSFF = "";
            string bgfjs = "";
            double hfcd , bhgs  = 0;
            double hfs = 0;
            int hgs = 0;
            string sybz = "";

            foreach (var sItem in S_GUTS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                sybz = sItem["SYBZ"].Trim();
                //设计等级名称
                mSjdj = sItem["YSDJ"];
                if (string.IsNullOrEmpty(mSjdj))
                {
                    mSjdj = "";
                }

                //if (0 < double.Parse(MItem[0]["FJCOUNT"]))
                //{
                //    for (int i = 1; i < double.Parse(MItem[0]["FJCOUNT"]); i++)
                //    {
                //        bgfjs = (10 + i - 1).ToString("0");
                //    }

                //}
                //计算修正系数
                //MItem[0]["BGZS"] =(double.Parse(MItem[0]["BGZS"]) + 1).ToString();
                if ("Ⅱ" == sItem["YSDJ"] && "Ⅲ" == sItem["PDDJ"])
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                else
                {
                    sItem["JCJG"] = "合格";
                    mjcjg = "合格";
                    hgs++;
                }

                if (sybz.Contains("50661") && "米" == MItem[0]["SFFS"])
                {
                    hfcd = double.Parse(sItem["HFCD"]);
                    hfs = (hfcd - 1) / 0.3 + 2;
                }
            }

            //添加最终报告
            bhgs = S_GUTS.Count - hgs;
            if (sybz.Contains("50205"))
            {
                jsbeizhu = "本次共检焊缝" + S_GUTS.Count + "条，其中达到" + mSjdj + "焊缝水平" + hgs + "条，未达到" + mSjdj + "焊缝水平" + bhgs + "条。";
            }
            else
            {
                if ("条" == MItem[0]["SFFS"])
                {
                    jsbeizhu = "本次共检焊缝" + S_GUTS.Count + "处，其中合格" + S_GUTS.Count + "处，合格率100%。";
                }
                else
                {
                    jsbeizhu = "本次共检焊缝" + hfs + "处，其中合格" + hfs + "处，合格率100%。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
