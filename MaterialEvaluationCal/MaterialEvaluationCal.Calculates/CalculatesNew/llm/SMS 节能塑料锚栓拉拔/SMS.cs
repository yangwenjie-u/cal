using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SMS : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_SMS"];
            var MItem = data["M_SMS"];
            var mrsDj = dataExtra["BZ_SMS_DJ"];
            int mbHggs = 0;
            double mMj1 = 0, mMj2 = 0, mMj3 = 0, mMj4 = 0, mMj5 = 0, mMj6 = 0, mMj7 = 0, mMj8 = 0;
            if (!data.ContainsKey("M_SMS"))
            {
                data["M_SMS"] = new List<IDictionary<string, string>>();
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
                var mrsdj = mrsDj.FirstOrDefault(u => u["QTLX"] == sItem["QTLX"].Trim());
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    //MItem[0]["G_ZXKL"] = string.IsNullOrEmpty(extraFieldsDj["ZXKL"]) ? extraFieldsDj["ZXKL"] : GetSafeDouble(extraFieldsDj["ZXKL"].Trim()).ToString("0.0");
                    sItem["G_BZZ"] = mrsdj["KLCZL"].Trim();//从等级表中获取抗拉强度标准值
                  

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



                #region 锚栓抗拉承载力标准值
                sign = true;
                
                if (jcxm.Contains("、锚栓抗拉承载力标准值、"))
                {
                    jcxmCur = "锚栓抗拉承载力标准值";

                    int Gs = 0;
                    double sum = 0,pjz = 0,md = 0, bzc = 0, byxs= 0, kz = 0, fjxs = 0, bzz = 0;

                    for ( int xd = 1; xd < 11; xd++)//判定有填了多少个数据
                    {
                        
                        sign = sItem["KYHZ" + xd] != null ? sign : false;
                        if (sign)
                        {
                            Gs = Gs + 1;
                            sum = sum + GetSafeDouble(sItem["KYHZ" + xd]);//数据的总和
                        }

                    }
                    if (Gs > 1)//有数据的时候
                    {
                        pjz = sum / Gs;//数据平均值
                        sum = 0;
                        for (int xd = 1; xd <= Gs; xd++)
                        {
                            md = GetSafeDouble(sItem["KYHZ" + xd]);
                            sum = sum + (md - pjz) * (md - pjz);
                        }
                        bzc = Math.Sqrt(sum / (Gs - 1));//标准偏差
                        byxs = bzc / pjz;//变异系数

                        if (Gs == 5)
                        {
                            kz = 3.4;
                        }
                        else if (Gs == 10)
                        {
                            kz = 2.6;
                        }

                        fjxs = 1 / (1 + (byxs * 100 - 20) * 0.03);//附加系数

                        if (byxs * 100 > 20)
                        {
                            bzz = pjz * (1 - kz * byxs) * fjxs;//标称值
                        }
                        else
                        {
                            bzz = pjz * (1 - kz * byxs);
                        }
                    }
                    sItem["KYPJ"] = pjz.ToString("0.00");
                    sItem["BZPC"] = bzc.ToString("0.00");
                    sItem["BYXS"] = byxs.ToString("0.00");
                    sItem["FJXS"] = fjxs.ToString("0.00");
                    sItem["CZBZZ"] = bzz.ToString("0.00");

                    if (GetSafeDouble(sItem["CZBZZ"]) >= GetSafeDouble(sItem["G_BZZ"]))
                    {
                        sItem["G_BZZ"] = GetSafeDouble(sItem["G_BZZ"]).ToString("0.00");
                        sItem["G_BZZ"] = "≥" + sItem["G_BZZ"];
                        sItem["HG_BZZ"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;

                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_BZZ"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }

                }
                else
                {
                    sItem["HG_BZZ"] = "----";
                    sItem["G_BZZ"] = "----";

                }
                #endregion


            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_SMS"))
            {
                data["M_SMS"] = new List<IDictionary<string, string>>();
            }
            var M_SMS = data["M_SMS"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_SMS == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_SMS.Add(m);
            }
            else
            {
                M_SMS[0]["JCJG"] = mjcjg;
                M_SMS[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
