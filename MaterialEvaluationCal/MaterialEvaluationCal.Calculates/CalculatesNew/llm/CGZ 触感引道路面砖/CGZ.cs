using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class CGZ : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_CGZ"];
            var MItem = data["M_CGZ"];
            var mrsDj = dataExtra["BZ_CGZ_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_CGZ"))
            {
                data["M_CGZ"] = new List<IDictionary<string, string>>();
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
                

                #region 抗压强度
                sign = true;
                if (jcxm.Contains("、抗压强度、"))
                {
                    var mrsdj = mrsDj.FirstOrDefault(u => u["KYDJ"] == sItem["KYDJ"].Trim());
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
                    jcxmCur = "抗压强度";

                    //int sum = 0;
                    //sItem["XSL1"] = Round((double.Parse(sItem["ZZZL1"]) - double.Parse(sItem["CSZL1"])) / double.Parse(sItem["CSZL1"]) * 100, 0).ToString("0");
                    //sItem["XSL2"] = Round((double.Parse(sItem["ZZZL2"]) - double.Parse(sItem["CSZL2"])) / double.Parse(sItem["CSZL2"]) * 100, 0).ToString("0");

                    //求抗压强度最小值
                    double min = GetSafeDouble(sItem["KYQD1"]);

                    for (int i = 2; i < 6; i++)
                    {
                        if (min > GetSafeDouble(sItem["KYQD"+ i ]))
                        {
                            min = GetSafeDouble(sItem["KYQD" + i]);
                        }
                    }

                    sItem["MKYQD"] = min.ToString("0.0");

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
                    sItem["G_PJKYQD"] = "平均值 ≥" + sItem["G_PJKYQD"] + ",且最小值≥" + sItem["G_MKYQD"];

                }
                else
                {
                    sItem["HG_PJKYQD"] = "----";
                    sItem["G_KYQD"] = "----";
                    
                }
                #endregion

                #region 抗折强度
                sign = true;
                if (jcxm.Contains("、抗折强度、"))
                {
                    var mrsdj = mrsDj.FirstOrDefault(u => u["KZDJ"] == sItem["KZDJ"].Trim());
                    if (mrsdj != null && mrsdj.Count != 0)
                    {
                        //MItem[0]["G_ZXKL"] = string.IsNullOrEmpty(extraFieldsDj["ZXKL"]) ? extraFieldsDj["ZXKL"] : GetSafeDouble(extraFieldsDj["ZXKL"].Trim()).ToString("0.0");
                        sItem["G_PJKZQD"] = mrsdj["PJKZQD"].Trim();//从等级表中获取抗折强度标准值
                        sItem["G_MKZQD"] = mrsdj["MKZQD"].Trim();

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
                    jcxmCur = "抗折强度";

                    //int sum = 0;
                    //sItem["XSL1"] = Round((double.Parse(sItem["ZZZL1"]) - double.Parse(sItem["CSZL1"])) / double.Parse(sItem["CSZL1"]) * 100, 0).ToString("0");
                    //sItem["XSL2"] = Round((double.Parse(sItem["ZZZL2"]) - double.Parse(sItem["CSZL2"])) / double.Parse(sItem["CSZL2"]) * 100, 0).ToString("0");

                    //求抗压强度最小值
                    double min = GetSafeDouble(sItem["KZQD1"]);

                    for (int i = 2; i < 6; i++)
                    {
                        if (min > GetSafeDouble(sItem["KZQD" + i]))
                        {
                            min = GetSafeDouble(sItem["KZQD" + i]);
                        }
                    }

                    sItem["MKZQD"] = min.ToString("0.00");

                    if (GetSafeDouble(sItem["PJKZQD"]) >= GetSafeDouble(sItem["G_PJKZQD"]) && GetSafeDouble(sItem["MKZQD"]) >= GetSafeDouble(sItem["G_MKZQD"]))
                    {
                        sItem["HG_PJKZQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;

                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_PJKZQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }
                    sItem["G_PJKZQD"] = "平均值 ≥" + sItem["G_PJKZQD"] + ",且最小值≥" + sItem["G_MZYQD"];

                }
                else
                {
                    sItem["HG_PJKZQD"] = "----";
                    sItem["G_KZQD"] = "----";

                }
                #endregion


            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_CGZ"))
            {
                data["M_CGZ"] = new List<IDictionary<string, string>>();
            }
            var M_CGZ = data["M_CGZ"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_CGZ == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_CGZ.Add(m);
            }
            else
            {
                M_CGZ[0]["JCJG"] = mjcjg;
                M_CGZ[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
