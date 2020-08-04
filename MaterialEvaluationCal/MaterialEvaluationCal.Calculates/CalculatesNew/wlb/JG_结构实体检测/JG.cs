using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
/*结构实体检测*/
namespace Calculates
{
    public class JG : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            //var extraDJ = dataExtra["BZ_AM_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_JGS = data["S_JG"];
            //if (!data.ContainsKey("M_JG"))
            //{
            //    data["M_AM"] = new List<IDictionary<string, string>>();
            //}
            //var MItem = data["M_JG"];

            //if (MItem == null)
            //{
            //    IDictionary<string, string> m = new Dictionary<string, string>();
            //    m["JCJG"] = mjcjg;
            //    m["JCJGMS"] = jsbeizhu;
            //    MItem.Add(m);
            //}
            string mJSFF = "";
            int mHggs = 0;//统计合格数量
            var jcxmBhg = "";
            var jcxmCur = "";

            //foreach (var sItem in S_JGS)
            //{

            //}

            ////添加最终报告
            //if (mAllHg && mjcjg != "----")
            //{
            //    mjcjg = "合格";
            //    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            //}
            //else
            //{
            //    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求	。";
            //    //if (mHggs > 0)
            //    //{
            //    //    jsbeizhu = "该组样品所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
            //    //}
            //    //else
            //    //{
            //    //    jsbeizhu = "该组样品所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
            //    //}
            //}

            //MItem[0]["JCJG"] = mjcjg;
            //MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
