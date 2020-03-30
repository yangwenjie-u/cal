using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZJZ : BaseMethods
    {
        public void Calc()
        {

            /************************ 代码开始 *********************/
            #region old
            //#region 参数定义
            //string mcalBh, mlongStr;

            //string[] mtmpArray;
            //double mZj, mMj, mGjb, mXzxs;
            //string mcGjb;
            //double mMaxKyqd, mMinKyqd;
            //string mGcbw;
            //double mPjz;
            //string mSjdjbh, mSjdj;
            //double mSz;
            //int vp, zZh, zGs, fji;
            //string mMaxBgbh;
            //string mJSFF, bgfjs;
            //bool mAllHg;
            //bool mGetBgbh;
            //int manbhgs, mjqbhgs, mbbhgs, mtvocbhgs, mdbhgs, fsqg;
            //bool mSFwc;
            //mSFwc = true;
            //#endregion

            //#region  集合取值
            //var data = retData;
            //var mrsDj = dataExtra["BZ_ZJZ_DJ"];
            //var MItem = data["M_ZJZ"];
            //var SItem = data["S_ZJZ"];
            //var mrsZxm = dataExtra["MJZ1"];
            //var mrssubZxm = dataExtra["SJZ1"];
            //#endregion

            //#region 计算开始
            //mGetBgbh = false;
            //mAllHg = true;
            //zZh = 1;
            //mlongStr = "";
            //zGs = 0;
            //string qsbh, zlx;
            //int qszs;
            //qszs = 1;
            //qsbh = SItem[0]["DZBH"];
            //mMinKyqd = 9999;
            //zlx = SItem[0]["ZLX"];
            ////if (GetSafeDouble(MItem[0]["SSJCF"]) <= 0)
            ////    MItem[0]["skbz"] = "0";
            //var mrsZxm2 = mrsZxm.Where(x => x["WTBH"].Contains(MItem[0]["WTBH"]));
            ////var mrsZxm2 = mrsZxm.Where(mrsXtab_Filter => mrsXtab_Filter["SYSJBRECID"].Equals(sitem["RECID"]));
            //var mrssubZxm2 = mrssubZxm.Where(x => x["WTBH"].Contains(MItem[0]["WTBH"]));
            ////var mrssubZxm2 = mrssubZxm.Where(mrsXtab_Filter => mrsXtab_Filter["SYSJBRECID"].Equals(sitem["RECID"]));
            //foreach (var sitem in SItem)
            //{
            //    if (GetSafeDouble(sitem["DZBH"].Trim()) - GetSafeDouble(qsbh) == qszs)
            //        qszs = qszs + 1;
            //    sitem["JCRQ"] = MItem[0]["SYRQQ"] + "至" + MItem[0]["SYRQ"];
            //    sitem["JCJG"] = "合格";
            //}
            //#endregion
            #endregion

            #region 
            /************************ 代码开始 *********************/
            //word导入不需要计算
            var data = retData;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            var MItem = data["M_ZJZ"];
            if (!data.ContainsKey("M_ZJZ"))
            {
                data["M_ZJZ"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> mItem = new Dictionary<string, string>();
                mItem["JCJG"] = mjcjg;
                mItem["JCJGMS"] = jsbeizhu;
                MItem.Add(mItem);
            }

            /************************ 代码结束 *********************/
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
