using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class LSM : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_LSM"];
            var MItem = data["M_LSM"];
            var mrsDj = dataExtra["BZ_LSM_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_LSM"))
            {
                data["M_LSM"] = new List<IDictionary<string, string>>();
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
                var mrsdj = mrsDj.FirstOrDefault(u => u["LWLX"] == sItem["LWLX"].Trim() && u["LWGG"] == sItem["LWGG"].Trim() && u["SJDJ"] == sItem["SJDJ"].Trim());
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    //MItem[0]["G_ZXKL"] = string.IsNullOrEmpty(extraFieldsDj["ZXKL"]) ? extraFieldsDj["ZXKL"] : GetSafeDouble(extraFieldsDj["ZXKL"].Trim()).ToString("0.0");
                    sItem["G_ZXKL"] = mrsdj["ZXKL"].Trim();//从等级表中获取最小抗拉强度标准值
                    sItem["G_LLHZ"] = mrsdj["LLHZ"].Trim();//从等级表中获取楔负载标准值
                    sItem["G_BZHZ"] = mrsdj["BZHZ"].Trim();//从等级表中获取保证荷载标准值
                    sItem["GCMJ"] = mrsdj["GCMJ"].Trim();//从等级表中获取公称面积
                    sItem["G_KLQD"] = mrsdj["KLQD"].Trim();//从等级表中获取抗拉强度标准值
                    //mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"];
                }
                else
                {
                    //mJSFF = "";
                    mAllHg = false;
                    mItem["bgbh"] = "";
                    sItem["JCJG"] = "依据不详";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 最小拉力载荷
                sign = true;
                if (jcxm.Contains("、最小拉力载荷、"))
                {
                    jcxmCur = "最小拉力载荷";
                    
                    int sum = 0;
                    for (int i = 1; i < 9; i++)//循环8次，判定最小拉力载荷是否符合标准要求。
                    {
                         if (GetSafeDouble(sItem["ZXKL" + i]) >= double.Parse(sItem["G_ZXKL"]))
                         {
                              sum++;
                         }
                    }
                    if (sum <= 0)
                    {
                        MItem[0]["HG_ZXKL"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;
                        
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mItem["HG_ZXKL"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }

                 }
                else
                {
                    sItem["HG_ZXKL"] = "----";
                    sItem["G_ZXKL"] = "----";
                    
                }
                #endregion

                #region 楔负载
                sign = true;
                if (jcxm.Contains("、楔负载、"))
                {
                    jcxmCur = "楔负载";

                    int sum = 0;
                    for (int i = 1; i < 9; i++)//循环8次，判定最小拉力载荷是否符合标准要求。
                    {
                        //if (GetSafeDouble(sItem["ZXKL" + i]) <= double.Parse(sItem["G_ZXKL"]))
                        sItem["HG_LLHZ"] = IsQualified(sItem["G_LLHZ"], sItem["LLHZ" + i], false);
                        if (sItem["HG_LLHZ"] == "不合格")
                        {
                            sum++;
                        }
                    }
                    if (sum <= 0)
                    {
                        MItem[0]["HG_LLHZ"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;

                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mItem["HG_LLHZ"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }

                }
                else
                {
                    sItem["HG_LLHZ"] = "----";
                    sItem["G_LLHZ"] = "----";

                }
                #endregion

                #region 保证荷载
                sign = true;
                if (jcxm.Contains("、保证荷载、"))
                {
                    jcxmCur = "保证荷载";

                    int sum = 0;
                    for (int i = 1; i < 9; i++)//循环8次，判定最小拉力载荷是否符合标准要求。
                    {
                        if (GetSafeDouble(sItem["BZHZ" + i]) >= double.Parse(sItem["G_BZHZ"]))
                        {
                            sum++;
                        }
                    }
                    if (sum <= 0)
                    {
                        MItem[0]["HG_BZHZ"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;

                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mItem["HG_BZHZ"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }

                }
                else
                {
                    sItem["HG_BZHZ"] = "----";
                    sItem["G_BZHZ"] = "----";

                }
                #endregion

                #region 抗拉强度
                sign = true;
                if (jcxm.Contains("、抗拉强度、"))
                {
                    jcxmCur = "抗拉强度";

                    int sum = 0;
                    //计算8个试件的抗拉强度
                    mMj1 = double.Parse(sItem["JXLL1"]) / double.Parse(sItem["GCMJ"]);
                    sItem["KLQD1"] = mMj1.ToString();

                    mMj2 = double.Parse(sItem["JXLL2"]) / double.Parse(sItem["GCMJ"]);
                    sItem["KLQD2"] = mMj2.ToString();

                    mMj3 = double.Parse(sItem["JXLL3"]) / double.Parse(sItem["GCMJ"]);
                    sItem["KLQD1"] = mMj3.ToString();

                    mMj4 = double.Parse(sItem["JXLL4"]) / double.Parse(sItem["GCMJ"]);
                    sItem["KLQD1"] = mMj4.ToString();

                    mMj5 = double.Parse(sItem["JXLL5"]) / double.Parse(sItem["GCMJ"]);
                    sItem["KLQD1"] = mMj5.ToString();

                    mMj6 = double.Parse(sItem["JXLL6"]) / double.Parse(sItem["GCMJ"]);
                    sItem["KLQD1"] = mMj6.ToString();

                    mMj7 = double.Parse(sItem["JXLL7"]) / double.Parse(sItem["GCMJ"]);
                    sItem["KLQD1"] = mMj7.ToString();

                    mMj8 = double.Parse(sItem["JXLL8"]) / double.Parse(sItem["GCMJ"]);
                    sItem["KLQD8"] = mMj8.ToString();

                    
                    for (int i = 1; i < 9; i++)//循环8次，判定最小拉力载荷是否符合标准要求。
                    {
                        if (GetSafeDouble(sItem["KLQD" + i]) >= double.Parse(sItem["G_KLQD"]))
                        {
                            sum++;
                        }
                    }

                    if (sum <= 0)
                    {
                        MItem[0]["HG_KLQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;

                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mItem["HG_KLQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }

                }
                else
                {
                    sItem["HG_ZXKL"] = "----";
                    sItem["G_ZXKL"] = "----";

                }
                #endregion

            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_LSM"))
            {
                data["M_LSM"] = new List<IDictionary<string, string>>();
            }
            var M_LSM = data["M_LSM"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_LSM == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_LSM.Add(m);
            }
            else
            {
                M_LSM[0]["JCJG"] = mjcjg;
                M_LSM[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
