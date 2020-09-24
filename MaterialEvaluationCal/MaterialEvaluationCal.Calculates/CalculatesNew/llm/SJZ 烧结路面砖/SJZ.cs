using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SJZ : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_SJZ"];
            var MItem = data["M_SJZ"];
            var mrsDj = dataExtra["BZ_SJZ_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_SJZ"))
            {
                data["M_SJZ"] = new List<IDictionary<string, string>>();
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
                var mrsdj = mrsDj.FirstOrDefault(u => u["QDLB"] == sItem["QDLB"].Trim());
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    //MItem[0]["G_ZXKL"] = string.IsNullOrEmpty(extraFieldsDj["ZXKL"]) ? extraFieldsDj["ZXKL"] : GetSafeDouble(extraFieldsDj["ZXKL"].Trim()).ToString("0.0");
                    sItem["G_PJKYQD"] = mrsdj["PJKYQD"].Trim();//从等级表中获取抗拉强度标准值
                    sItem["G_MKYQD"] = mrsdj["MKYQD"].Trim();

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

                    //求抗压强度最小值
                    double min = GetSafeDouble(sItem["KYQD1"]);

                    for (int i = 2; i < 11; i++)
                    {
                        if (min > GetSafeDouble(sItem["KYQD"+ i ]))
                        {
                            min = GetSafeDouble(sItem["KYQD" + i]);
                        }
                    }

                    sItem["MKYQD"] = min.ToString();

                    if (GetSafeDouble(sItem["PJKYQD"]) >= GetSafeDouble(sItem["G_PJKYQD"]) && GetSafeDouble(sItem["MKYQD"]) >= GetSafeDouble(sItem["G_MKYQD"]))
                    {
                        sItem["HG_PJKYQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;
                        
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_PJKYQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }

                 }
                else
                {
                    sItem["HG_PJKYQD"] = "----";
                    sItem["G_KYQD"] = "----";
                    
                }
                #endregion
                        

            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_SJZ"))
            {
                data["M_SJZ"] = new List<IDictionary<string, string>>();
            }
            var M_SJZ = data["M_SJZ"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_SJZ == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_SJZ.Add(m);
            }
            else
            {
                M_SJZ[0]["JCJG"] = mjcjg;
                M_SJZ[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
