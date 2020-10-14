using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class RPM : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_RPM"];
            var MItem = data["M_RPM"];
            var mrsDj = dataExtra["BZ_RPM_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_RPM"))
            {
                data["M_RPM"] = new List<IDictionary<string, string>>();
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
                var mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim());
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    //MItem[0]["G_ZXKL"] = string.IsNullOrEmpty(extraFieldsDj["ZXKL"]) ? extraFieldsDj["ZXKL"] : GetSafeDouble(extraFieldsDj["ZXKL"].Trim()).ToString("0.0");
                    sItem["G_XSL"] = mrsdj["XSL"].Trim();//从等级表中获取最小抗拉强度标准值
                    
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

                #region 真空吸水率
                sign = true;
                if (jcxm.Contains("、真空吸水率、"))
                {
                    jcxmCur = "真空吸水率";
                    
                    //int sum = 0;
                    sItem["XSL1"] = Round((double.Parse(sItem["ZZZL1"]) - double.Parse(sItem["CSZL1"])) / double.Parse(sItem["CSZL1"]) * 100, 0).ToString("0");
                    sItem["XSL2"] = Round((double.Parse(sItem["ZZZL2"]) - double.Parse(sItem["CSZL2"])) / double.Parse(sItem["CSZL2"]) * 100, 0).ToString("0");

                    if (GetSafeDouble(sItem["XSL1"]) <= double.Parse(sItem["G_XSL"]) && GetSafeDouble(sItem["XSL2"]) <= double.Parse(sItem["G_XSL"]))
                    {
                        MItem[0]["HG_XSL"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;
                        
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mItem["HG_XSL"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }
                    sItem["G_XSL"] = "≤" + sItem["G_XSL"];
                 }
                else
                {
                    mItem["HG_XSL"] = "----";
                    sItem["G_XSL"] = "----";
                    
                }
                #endregion
                        

            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_RPM"))
            {
                data["M_RPM"] = new List<IDictionary<string, string>>();
            }
            var M_RPM = data["M_RPM"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_RPM == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_RPM.Add(m);
            }
            else
            {
                M_RPM[0]["JCJG"] = mjcjg;
                M_RPM[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
