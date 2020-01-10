using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    /*现场保温板拉伸*/
    public class BZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_BZ_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_BZS = data["S_BZ"];
            if (!data.ContainsKey("M_BZ"))
            {
                data["M_BZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_BZ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            MItem[0]["FJJJ1"] = "";
            MItem[0]["FJJJ2"] = "";
            MItem[0]["FJJJ3"] = "";
            string mJSFF = "";
            string mPhjmqk, mDkbxy = "";
            double mMaxKyqd, mMinKyqd = 0;
            string mlongStr = "";
            List<double> mkyhzArray = new List<double>();
            List<string> mtmpArray = new List<string>();
            string mypmc = "";
            //遍历从表数据
            foreach (var sItem in S_BZS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if (string.IsNullOrEmpty(sItem["KLDJ"]))
                {
                    sItem["KLDJ"] = "";
                }
                if (string.IsNullOrEmpty(sItem["MDLB"]))
                {
                    sItem["MDLB"] = "";
                }

                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["BCMC"] == sItem["JTCL"].Trim() && u["KLDJ"] == sItem["KLDJ"].Trim() && u["MDLB"] == sItem["MDLB"].Trim());
                if (null != extraFieldsDj)
                {
                    mPhjmqk = extraFieldsDj["PHJMQK"];
                    mDkbxy = extraFieldsDj["DKBXY"];
                }
                else
                {
                    mPhjmqk = "0";
                    mDkbxy = "0";
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "不合格";
                    continue;
                }

                if ("酚醛" == sItem["JTCL"])
                {
                    sItem["BZYQ"] = "粘结强度不应低于0.08MPa，破坏界面在酚醛泡沫板上";
                    mDkbxy = "0.08";
                }
                else if ("聚氨酯" == sItem["JTCL"])
                {
                    sItem["BZYQ"] = "粘结强度不应低于0.10MPa，且破部位不得位于粘结界面 ";
                }
                else
                {
                    sItem["BZYQ"] = "粘结强度不应低于" + mDkbxy.Trim() + "MPa，" + mPhjmqk;
                }

                if (jcxm.Contains("、拉伸粘结强度、"))
                {
                    sItem["JCJG"] = "合格";
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["MJ" + i] = (Conversion.Val(sItem["CD" + i]) * Conversion.Val(sItem["KD" + i])).ToString();
                        if (0 != Conversion.Val(sItem["MJ" + i]))
                        {
                            if ("岩棉" == sItem["JTCL"])
                            {
                                sItem["KYQD" + i] = Math.Round(1000 * Conversion.Val(sItem["KYHZ" + i]) / double.Parse(sItem["MJ" + i]), 4).ToString("0.0000");
                            }
                            else
                            {
                                sItem["KYQD" + i] = Math.Round(1000 * Conversion.Val(sItem["KYHZ" + i]) / double.Parse(sItem["MJ" + i]), 2).ToString("0.00");
                            }
                            if (Conversion.Val(sItem["KYQD" + i])  >= Conversion.Val(mDkbxy) && "合格" == sItem["PHJMPD"+i])
                            {
                            }
                            else
                            {
                                sItem["JCJG"] = "不合格";
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["KYQD" + i] = "----";
                            sItem["JCJG"] = "----";
                        }
                    }
                }

                string mZh = sItem["ZH_G"];
                //string mZh = "组号";
                if ("合格" == sItem["JCJG"])
                {
                    if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"]))
                    {
                        MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + mZh.Trim() + "#";
                    }
                    else
                    {
                        MItem[0]["FJJJ3"] = mZh.Trim() + "#";
                    }
                }

                if ("不合格" == sItem["JCJG"])
                {
                    if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"]))
                    {
                        MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + mZh.Trim() + "#";
                    }
                    else
                    {
                        MItem[0]["FJJJ2"] = mZh.Trim() + "#";
                    }
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
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"].Trim()))
            {
                jsbeizhu = "试样" + MItem[0]["FJJJ3"] + "所检项目符合标准要求。";
                MItem[0]["FJJJ3"] = "试样" + MItem[0]["FJJJ3"] + "所检项目符合上述标准要求";
            }
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"].Trim()))
            {
                jsbeizhu = "试样" + MItem[0]["FJJJ2"] + "不符合标准要求。";
                MItem[0]["FJJJ2"] = "试样" + MItem[0]["FJJJ2"] + "不符合上述标准要求";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
