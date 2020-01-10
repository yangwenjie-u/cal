using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class MZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_MZ_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_MZS = data["S_MZ"];
            if (!data.ContainsKey("M_MZ"))
            {
                data["M_MZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_MZ"];

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

            string mDkbxy, mJSFF = "";
            double pjqd, sum = 0;
            double qd = 0;
            int lx = 0;

            //遍历从表数据
            foreach (var sItem in S_MZS)
            {
                List<double> nArr = new List<double>();
                nArr.Add(0);
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表获取数据
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["SJPZ"]);
                if (extraFieldsDj != null)
                {
                    mDkbxy = extraFieldsDj["DKBXY"];
                    mJSFF = extraFieldsDj["JSFF"] == null ? "" : extraFieldsDj["JSFF"].ToLower();
                }
                else
                {
                    mDkbxy = "0";
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "不合格";
                    continue;
                }
                if (mJSFF == "")
                {
                    if (jcxm.Contains("、拉伸粘结强度、"))
                    {
                        for (int xd = 1; xd < 8; xd++)
                        {
                            qd = GetSafeDouble(sItem["KYQD" + xd].Trim());
                            nArr.Add(qd);
                            if (sItem["DKSS" + xd].Trim() == "3" || sItem["DKSS" + xd].Trim() == "4" && qd < GetSafeDouble(mDkbxy.Trim()))
                            {
                                sItem["JCJG"] = "不合格";
                                jsbeizhu = "不合格";
                                mAllHg = false;
                                continue;
                            }
                            sum = sum + qd;
                        }
                        pjqd = sum / 7;
                        for (int xd = 8; xd > 1; xd--)
                        {
                            for (int Gs = 1; Gs < xd - 2; Gs++)
                            {
                                if (nArr[Gs] < nArr[Gs + 1])
                                {
                                    qd = nArr[Gs];
                                    nArr[Gs] = nArr[Gs + 1];
                                    nArr[Gs + 1] = qd;
                                }
                            }
                        }

                        if ((nArr[1] - pjqd) / pjqd > 0.2)
                        {
                            for (int xd = 2; xd < 7; xd++)
                            {
                                nArr[xd] = qd;
                                sum = sum + qd;
                            }
                            pjqd = sum / 5;
                            lx++;
                            if ((nArr[3] - pjqd) / pjqd > 0.2)
                            {
                                sItem["JCJG"] = "无效";
                                jsbeizhu = "不合格";
                                mAllHg = false;
                                continue;
                            }
                            if ((nArr[2] - pjqd) / pjqd > 0.2)
                            {
                                for (int xd = 3; xd < 6; xd++)
                                {
                                    nArr[xd] = qd;
                                    sum = sum + qd;
                                }
                                pjqd = sum / 3;
                                lx++;
                            }
                        }
                        sItem["KYPJ"] = pjqd.ToString();
                        if (lx == 0)
                        {
                            if (pjqd >= GetSafeDouble(mDkbxy) && nArr[7] >= GetSafeDouble(mDkbxy) * 0.75)
                            {
                            }
                            else
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                        }

                        if (lx == 1)
                        {
                            if (pjqd >= GetSafeDouble(mDkbxy) && nArr[6] >= GetSafeDouble(mDkbxy) * 0.75)
                            {
                            }
                            else
                            {
                                itemHG = false;
                                mAllHg = false;
                            }
                        }

                        if (lx == 2)
                        {
                            if (nArr[5] >= GetSafeDouble(mDkbxy))
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
                #endregion
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                    if (MItem[0]["FJJJ3"] != "")
                    {
                        MItem[0]["FJJJ3"] = MItem[0]["FJJJ3"] + "、" + sItem["ZH_G"] + "#";
                    }
                    else
                    {
                        MItem[0]["FJJJ3"] = sItem["ZH_G"] + "#";
                    }
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    if (MItem[0]["FJJJ2"] != "")
                    {
                        MItem[0]["FJJJ2"] = MItem[0]["FJJJ2"] + "、" + sItem["ZH_G"] + "#";
                    }
                    else
                    {
                        MItem[0]["FJJJ2"] = sItem["ZH_G"] + "#";
                    }
                }
            }
            //添加最终报告
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ3"].Trim()))
            {
                jsbeizhu = "试样" + MItem[0]["FJJJ3"] + "所检项目符合标准要求";
                MItem[0]["FJJJ3"] = "试样" + MItem[0]["FJJJ3"] + "所检项目符合上述标准要求";
            }
            if (!string.IsNullOrEmpty(MItem[0]["FJJJ2"].Trim()))
            {
                jsbeizhu = "试样" + MItem[0]["FJJJ2"] + "不符合标准要求";
                MItem[0]["FJJJ2"] = "试样" + MItem[0]["FJJJ2"] + "不符合上述标准要求";
            }
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }


            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
