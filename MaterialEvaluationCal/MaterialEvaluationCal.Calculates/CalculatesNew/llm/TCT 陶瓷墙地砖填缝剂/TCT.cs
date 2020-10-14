using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class TCT : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_TCT"];
            var MItem = data["M_TCT"];
            var mrsDj = dataExtra["BZ_TCT_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_TCT"))
            {
                data["M_TCT"] = new List<IDictionary<string, string>>();
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
                    sItem["G_KZQD"] = mrsdj["KZQD"].Trim();//从等级表中获取抗拉强度标准值
                    sItem["G_KYQD"] = mrsdj["KYQD"].Trim();

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
                #region 抗折强度
                sign = true;
                if (jcxm.Contains("、抗折强度、"))
                {
                    jcxmCur = "抗折强度";
   
                    if (GetSafeDouble(sItem["KZQD"]) >= GetSafeDouble(sItem["G_KZQD"]))
                    {
                        
                        sItem["HG_KZQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;

                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KZQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }
                    sItem["G_KZQD"] = GetSafeDouble(sItem["G_KZQD"]).ToString("0.00");
                    sItem["G_KZQD"] = "≥" + sItem["G_KZQD"];

                }
                else
                {
                    sItem["HG_KZQD"] = "----";
                    sItem["G_KZQD"] = "----";
                    sItem["KZQD1"] = "----";
                    sItem["KZQD2"] = "----";
                    sItem["KZQD3"] = "----";
                    sItem["KZQD"] = "----";

                }
                #endregion


                #region 抗压强度
                sign = true;
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";

                    if (GetSafeDouble(sItem["KYQD"]) >= GetSafeDouble(sItem["G_KYQD"]))
                    {
                        
                        sItem["HG_KYQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;

                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KZQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }
                    sItem["G_KYQD"] = GetSafeDouble(sItem["G_KYQD"]).ToString("0.0");
                    sItem["G_KYQD"] = "≥" + sItem["G_KYQD"];

                }
                else
                {
                    sItem["HG_KYQD"] = "----";
                    sItem["G_KYQD"] = "----";
                    sItem["KYQD"] = "----";
                    sItem["KYQD1"] = "----";
                    sItem["KYQD2"] = "----"; 
                    sItem["KYQD3"] = "----";
                    sItem["KYQD4"] = "----";
                    sItem["KYQD5"] = "----";
                    sItem["KYQD6"] = "----";
                }
                #endregion


            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_TCT"))
            {
                data["M_TCT"] = new List<IDictionary<string, string>>();
            }
            var M_TCT = data["M_TCT"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_TCT == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_TCT.Add(m);
            }
            else
            {
                M_TCT[0]["JCJG"] = mjcjg;
                M_TCT[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
