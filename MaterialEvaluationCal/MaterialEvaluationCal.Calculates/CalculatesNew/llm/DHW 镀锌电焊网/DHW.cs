using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class DHW : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_DHW"];
            var MItem = data["M_DHW"];
            var mrsDj = dataExtra["BZ_DHW_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_DHW"))
            {
                data["M_DHW"] = new List<IDictionary<string, string>>();
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
                var mrsdj = mrsDj.FirstOrDefault(u => u["SJ"] == sItem["R_SJCC"].Trim());
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    //MItem[0]["G_ZXKL"] = string.IsNullOrEmpty(extraFieldsDj["ZXKL"]) ? extraFieldsDj["ZXKL"] : GetSafeDouble(extraFieldsDj["ZXKL"].Trim()).ToString("0.0");
                    sItem["G_HDLL"] = mrsdj["HDLL"].Trim();//从等级表中获取抗拉强度标准值
                  

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



                #region 焊点抗拉力
                sign = true;
                if (jcxm.Contains("、焊点抗拉力、"))
                {
                    jcxmCur = "焊点抗拉力";

                    if (GetSafeDouble(sItem["HDLL"]) > GetSafeDouble(sItem["G_HDLL"]))
                    {
                        sItem["G_HDLL"] = GetSafeDouble(sItem["G_HDLL"]).ToString("0.0");
                        sItem["G_HDLL"] = "＞" + sItem["G_HDLL"];
                        sItem["HG_HDLL"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;

                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_HDLL"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }

                }
                else
                {
                    sItem["HG_HDLL"] = "----";
                    sItem["G_HDLL"] = "----";

                }
                #endregion


            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_DHW"))
            {
                data["M_DHW"] = new List<IDictionary<string, string>>();
            }
            var M_DHW = data["M_DHW"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_DHW == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_DHW.Add(m);
            }
            else
            {
                M_DHW[0]["JCJG"] = mjcjg;
                M_DHW[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
