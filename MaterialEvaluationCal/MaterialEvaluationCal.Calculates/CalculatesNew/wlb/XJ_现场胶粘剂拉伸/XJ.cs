using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class XJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_XJ_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_XJS = data["S_XJ"];
            if (!data.ContainsKey("M_XJ"))
            {
                data["M_XJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_XJ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            else
            {
                MItem[0]["FJJJ1"] = "";
                MItem[0]["FJJJ2"] = "";
                MItem[0]["FJJJ3"] = "";
            }

            string mTkmj, mDkbxy = "";
            string mJSFF = "";

            //遍历从表数据
            foreach (var sItem in S_XJS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault();
                if (null != extraFieldsDj)
                {
                    mTkmj = extraFieldsDj["TKMJ"];
                    mDkbxy = extraFieldsDj["DKBXY"];
                    mJSFF = extraFieldsDj["JSFF"] == null ? "" : extraFieldsDj["JSFF"].ToLower();
                }
                else
                {
                    mDkbxy = "0";
                    mTkmj = "0";
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "不合格";
                    continue;
                }
                sItem["BZYQ"] = "粘结强度不应低于" + mDkbxy.Trim() + "MPa，" + "并且粘结界面脱开面积不应大于" + mTkmj + "%";
                if (mJSFF == "")
                {
                    if (jcxm.Contains("、拉伸粘结强度、"))
                    {
                        for (int i = 1; i < 6; i++)
                        {
                            sItem["MJ" + i] = (GetSafeDouble(sItem["CD" + i]) * GetSafeDouble(sItem["KD" + i])).ToString();
                            if (GetSafeDouble(sItem["MJ" + i]) != 0)
                            {
                                sItem["KYQD" + i] = Math.Round(1000* GetSafeDouble(sItem["KYHZ" + i])/ GetSafeDouble(sItem["MJ" + i]),2).ToString("0.00");
                                if (GetSafeDouble(sItem["KYQD" + i].Trim()) >= GetSafeDouble(mDkbxy) && GetSafeDouble(sItem["PHMJ" + i]) <= GetSafeDouble(mTkmj) 
                                    || sItem["PHMJ" + i].Trim() == "＜50" || sItem["PHMJ" + i].Trim() == "≤50")
                                {
                                }
                                else
                                {
                                    itemHG = false;
                                    mAllHg = false;
                                }
                            }
                        }
                    }
                }

                #endregion
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                    if (MItem[0]["FJJJ3"] != "")
                    {
                        //sItem["zhmit = 1
                        MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + "、" + "1" + "#";
                    }
                    else
                    {
                        MItem[0]["FJJJ3"] = "1" + "#";
                    }
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    if (MItem[0]["FJJJ2"] != "")
                    {
                        MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + "、" + "1" + "#";
                    }
                    else
                    {
                        MItem[0]["FJJJ2"] = "1" + "#";
                    }
                }

                if ("" != MItem[0]["FJJJ3"].Trim())
                {
                    MItem[0]["FJJJ3"] = "试样" + MItem[0]["FJJJ3"] + "所检项目符合上述标准要求";
                }
                if ("" != MItem[0]["FJJJ2"].Trim())
                {
                    MItem[0]["FJJJ3"] = "试样" + MItem[0]["FJJJ3"] + "不符合上述标准要求";
                }
            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            else
            {
                jsbeizhu = "不合格";
            }
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"].Trim()) && string.IsNullOrEmpty(MItem[0]["FJJJ2"].Trim()))
            {
                jsbeizhu = MItem[0]["FJJJ3"] + "。";
            }
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"].Trim()) && string.IsNullOrEmpty(MItem[0]["FJJJ3"].Trim()))
            {
                jsbeizhu = MItem[0]["FJJJ2"] + "。";
            }
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"].Trim()) && !string.IsNullOrEmpty(MItem[0]["FJJJ3"].Trim()))
            {
                jsbeizhu = MItem[0]["FJJJ3"] + "，"+MItem[0]["FJJJ2"] + "。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
