using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class ZZQ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_ZZQ_DJ"];
            //获取数据表数据
            //var extraMJZ1 = dataExtra["MJZ1"];
            //var extraSJZ1 = dataExtra["SJZ1"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            bool itemHG = true;//判断单组是否合格
            var S_ZZQS = data["S_ZZQ"];
            if (!data.ContainsKey("M_ZZQ"))
            {
                data["M_GZZQ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_ZZQ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            double mMinKyqd = 0;
            string zlx = "";

            //遍历从表数据
            foreach (var sItem in S_ZZQS)
            {
                //itemHG = true;
                //string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                //mMinKyqd = 9999;
                //zlx = sItem["ZLX"];
                //if (GetSafeDouble(MItem[0]["SSJCF"]) <= 0)
                //{
                //    MItem[0]["SKBZ"] = "0";
                //}
                ////mrsZxm
                //var extraFieldsMjz1 = extraMJZ1.FirstOrDefault(u => u["WTBH"] == MItem[0]["WTBH"]);
                ////mrssubZxm
                //var extraFieldsSjz1 = extraSJZ1.FirstOrDefault(u => u["WTBH"] == MItem[0]["WTBH"]);
                //extraFieldsMjz1
                ////mrsZxm.AddNew
                ////mrsZxm!jydbh = mrsmainTable!jydbh
                ////mrsZxm!wtbh = mrsmainTable!wtbh
                ////mrsZxm!bgbh = mrsmainTable!bgbh
                ////mrsZxm!gcmc = mrsmainTable!gcmc
                ////mrsZxm!gcmc_mx = mrsmainTable!gcmc_mx
                ////mrsZxm!which = "bgzj1_1"
                ////mrssubTable.MoveFirst

                #endregion
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
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
