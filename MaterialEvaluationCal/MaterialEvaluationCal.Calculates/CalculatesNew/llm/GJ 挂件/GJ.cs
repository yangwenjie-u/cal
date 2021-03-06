﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GJ : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_GJ"];
            var MItem = data["M_GJ"];
            var mrsDj = dataExtra["BZ_GJ_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_GJ"))
            {
                data["M_GJ"] = new List<IDictionary<string, string>>();
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
                var mrsdj = mrsDj.FirstOrDefault(u => u["ZSWZ"] == sItem["ZSWZ"].Trim());
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    //MItem[0]["G_ZXKL"] = string.IsNullOrEmpty(extraFieldsDj["ZXKL"]) ? extraFieldsDj["ZXKL"] : GetSafeDouble(extraFieldsDj["ZXKL"].Trim()).ToString("0.0");
                    sItem["G_LBHZ"] = mrsdj["LBHZ"].Trim();//从等级表中获取拉拔强度标准值

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

                #region 拉拔强度
                sign = true;
                if (jcxm.Contains("、拉拔强度、"))
                {
                    jcxmCur = "拉拔强度";
                    
                    int sum = 0;
                    for (int i = 1; i < 6; i++)//循环5次，判定拉拔强度是否符合标准要求。
                    {
                         if (GetSafeDouble(sItem["LBHZ" + i]) >= double.Parse(sItem["G_LBQD"]))

                         {
                            sum++;
                            sItem["LBQD" + i] = Round(double.Parse(sItem["LBHZ" + i]) / 1000, 2).ToString("0.00");//拉拔强度报告上为KN
                         }
                    }
                    if (sum <= 1)
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
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
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
            if (!data.ContainsKey("M_GJ"))
            {
                data["M_GJ"] = new List<IDictionary<string, string>>();
            }
            var M_GJ = data["M_GJ"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_GJ == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_GJ.Add(m);
            }
            else
            {
                M_GJ[0]["JCJG"] = mjcjg;
                M_GJ[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
