using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class PB : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_PB"];
            var MItem = data["M_PB"];
            var mrsDj = dataExtra["BZ_PB_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_PB"))
            {
                data["M_PB"] = new List<IDictionary<string, string>>();
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
                var mrsdj = mrsDj.FirstOrDefault(u => u["MDDJ"] == sItem["MDDJ"].Trim() && u["CPMC"] == sItem["CPMC"].Trim());
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    //MItem[0]["G_ZXKL"] = string.IsNullOrEmpty(extraFieldsDj["ZXKL"]) ? extraFieldsDj["ZXKL"] : GetSafeDouble(extraFieldsDj["ZXKL"].Trim()).ToString("0.0");
                    sItem["G_KYQD"] = mrsdj["QDAVG"].Trim();//从等级表中获取抗压强度标准值
                    

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

                #region 抗压强度
                sign = true;
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";

                    //int sum = 0;
                    //sItem["XSL1"] = Round((double.Parse(sItem["ZZZL1"]) - double.Parse(sItem["CSZL1"])) / double.Parse(sItem["CSZL1"]) * 100, 0).ToString("0");
                    //sItem["XSL2"] = Round((double.Parse(sItem["ZZZL2"]) - double.Parse(sItem["CSZL2"])) / double.Parse(sItem["CSZL2"]) * 100, 0).ToString("0");

                    int m = 0;
                    double avg = GetSafeDouble(sItem["KYQD"]);//抗压强度平均值

                    for (int i = 1; i < 7; i++)//试样中是否有抗压强度偏离平均值20%
                    {
                        if (avg * 0.8 > GetSafeDouble(sItem["KYQD"+ i ]) || avg * 1.2 < GetSafeDouble(sItem["KYQD" + i]))
                        {
                            m++;
                        }
                    }
                    
                    
                    if (m > 0)
                    {
                        sItem["G_KYQD"] = "≥" + sItem["G_KYQD"];
                        sItem["HG_KYQD"] = "该组试样数据无效";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "该组试样数据无效，应重新制备试样进行试验";
                        mAllHg = false;

                    }
                        else if(GetSafeDouble(sItem["KYQD"]) >= GetSafeDouble(sItem["G_KYQD"]))
                        {
                        sItem["G_KYQD"] = "≥" + sItem["G_KYQD"];
                        sItem["HG_KYQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["G_KYQD"] = "≥" + sItem["G_KYQD"];
                            sItem["HG_KYQD"] = "不合格";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                            mAllHg = false;
                        }
                 }
                else
                {
                    sItem["HG_KYQD"] = "----";
                    sItem["G_KYQD"] = "----";
                    
                }
                #endregion
                        

            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_PB"))
            {
                data["M_PB"] = new List<IDictionary<string, string>>();
            }
            var M_PB = data["M_PB"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_PB == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_PB.Add(m);
            }
            else
            {
                M_PB[0]["JCJG"] = mjcjg;
                M_PB[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
