using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class ZDB : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_ZDB"];
            var MItem = data["M_ZDB"];
            var mrsDj = dataExtra["BZ_ZDB_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_ZDB"))
            {
                data["M_ZDB"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            bool sign = false;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == sItem["SJDJ"].Trim());
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    //MItem[0]["G_ZXKL"] = string.IsNullOrEmpty(extraFieldsDj["ZXKL"]) ? extraFieldsDj["ZXKL"] : GetSafeDouble(extraFieldsDj["ZXKL"].Trim()).ToString("0.0");
                    sItem["G_HFWHL"] = mrsdj["HFWHL"].Trim();//从等级表中获取挥发物含量标准值
                    

                    //mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"];
                }
                else
                {
                    //mJSFF = "";
                    mAllHg = false;
                    
                    sItem["JCJG"] = "依据不详";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 挥发物
                sign = true;
                if (jcxm.Contains("、挥发物、"))
                {
                    jcxmCur = "挥发物";
                 
                    if (GetSafeDouble(sItem["HFWHL"]) <= GetSafeDouble(sItem["G_HFWHL"]) )
                    {
                        sItem["HG_HFWHL"] = "合格";
                        sItem["G_HFWHL"] = GetSafeDouble(sItem["G_HFWHL"]).ToString("0.00");                      
                        sItem["G_HFWHL"] = "≤" + sItem["G_HFWHL"];
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;
                        
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_HFWHL"] = "不合格";
                        sItem["G_HFWHL"] = "≤" + sItem["G_HFWHL"];
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }

                 }
                else
                {
                    sItem["HG_HFWHL"] = "----";
                    sItem["G_HFWHL"] = "----";
                    sItem["HFWHL"] = "----";
                }
                #endregion
                        

            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_ZDB"))
            {
                data["M_ZDB"] = new List<IDictionary<string, string>>();
            }
            var M_ZDB = data["M_ZDB"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_ZDB == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_ZDB.Add(m);
            }
            else
            {
                M_ZDB[0]["JCJG"] = mjcjg;
                M_ZDB[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
