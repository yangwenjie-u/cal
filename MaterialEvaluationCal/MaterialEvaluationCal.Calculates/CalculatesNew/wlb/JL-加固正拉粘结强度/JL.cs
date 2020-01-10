using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class JL : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_JL_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_JLS = data["S_JL"];
            if (!data.ContainsKey("M_JL"))
            {
                data["M_JL"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_JL"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            int groupCount = S_JLS.Count;
            int qualifiedCount = 0;//统计合格组数

            bool sign = true;
            bool flag = true;
            double md1, md2, md, sum, sum1 = 0;
            int Gs = 0;
            double syHg = 0;
            double pjmd = 0;
            string wz = "";
            foreach (var sItem in S_JLS)
            {
                List<double> nArr1 = new List<double>();
                List<double> nArr2 = new List<double>();
                nArr1.Add(0);
                nArr2.Add(0);
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                ////从设计等级表中取得相应的计算数值、等级标准
                //var extraFieldsDj = extraDJ.FirstOrDefault(u => u["YPLB"].Trim() == sItem["YPLB"] && u["JLX"].Trim() == sItem["JLB"]);
                //if (null == extraFieldsDj)
                //{
                //    mAllHg = false;
                //    sItem["JCJG"] = "不合格";
                //    jsbeizhu = "不合格";
                //    continue;
                //}
                //先进行抗压强度的判断
                string sJCC = MItem[0]["SJCC"].Trim();
                if (sJCC == "40×40mm")
                {
                    sum = 40 * 40;
                }
                else
                {
                    sum = Math.Round(3.1415 * 25 * 25, 0);
                }
                string sFDS = sItem["SFDS"].Trim();
                if (sFDS == "是")
                {
                    sum = 0;
                    for (int xd = 1; xd < 7; xd++)
                    {
                        md = GetSafeDouble(sItem["KYQD" + xd]);
                        sum = sum + md;
                    }
                    md = sum / 6;
                    md = Math.Round(md, 2);
                    sItem["PJQD"] = md.ToString("0.00");
                    sum1 = 0;
                    for (int xd = 1; xd < 3; xd++)
                    {
                        md = GetSafeDouble(sItem["KYHZ" + xd]);
                        sum1 = sum1 + md;
                        sItem["KY" + xd] = "----";
                    }
                    md = sum1 / 3;
                    md = Math.Round(md, 2);
                    sItem["PJQD1"] = "----";
                }
                else
                {
                    sum = 0;
                    for (int xd = 1; xd < 4; xd++)
                    {
                        md = GetSafeDouble(sItem["KYHZ" + xd]);
                        sum = sum + md;
                    }
                    md = sum / 3;
                    md = Math.Round(md, 2);
                    sItem["PJQD1"] = md.ToString("0.00");
                    sItem["KYQD1"] = "----";
                    sItem["KYQD2"] = "----";
                    sItem["KYQD3"] = "----";
                    sItem["KYQD4"] = "----";
                    sItem["KYQD5"] = "----";
                    sItem["KYQD6"] = "----";
                    sItem["PJQD"] = "----";
                }
                sItem["SJ_NJ"] = "1.5";
                sign = IsNumeric(sItem["SJ_NJ"]) ? true : false;
                if (sign)
                {
                    pjmd = GetSafeDouble(sItem["SJ_NJ"].Trim());
                }
                //查找等级表数据
                if (!sign && IsNumeric(sItem["SC_QD"]))
                {
                    md = GetSafeDouble(sItem["SC_QD"].Trim());
                    Gs = extraDJ.Count;
                    if (md > 50.2 || md < 10.0)
                    {
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                        jsbeizhu = "不合格";
                        continue;
                    }
                    int xd = 1;
                    foreach (var extraFieldsDj in extraDJ)
                    {
                        nArr1.Add(GetSafeDouble(extraFieldsDj["KYQD"].Trim()));
                        nArr2.Add(GetSafeDouble(extraFieldsDj["KLQD"].Trim()));
                        if (md == GetSafeDouble(extraFieldsDj["KYQD"].Trim()))
                        {
                            pjmd = GetSafeDouble(extraFieldsDj["KLQD"].Trim());
                            xd++;
                            break;
                        }
                        xd++;
                    }

                    flag = true;
                    if (xd > Gs)
                    {
                        flag = false;
                    }
                    if (!flag)
                    {
                        int i = 1;
                        foreach (var extraFieldsDj in extraDJ)
                        {
                            if (md < nArr1[i])
                            {
                                break;
                            }
                            i++;
                        }
                        md1 = nArr2[i] - nArr2[i - 1];
                        md2 = nArr1[i] - nArr1[i - 1];
                        pjmd = (md - nArr1[i - 1]) * md1 / md2;
                        pjmd = pjmd + nArr2[i - 1];
                        pjmd = Math.Round(pjmd, 2);
                    }
                }
                sItem["SCQDBZZ"] = pjmd.ToString("0.00");
                sItem["BZYQ"] = " 单点正拉粘结强度均大于强度设计值1.5MPa ，";
                sItem["BZYQ"] = sItem["BZYQ"] + "且破坏形式均为内聚破坏。";

                //判定
                if (sItem["SFDS"].Trim() == "是")
                {
                    //mrsmainTable!which = "bgjl、bgjl_3"
                    syHg = 0;
                    for (int xd = 1; xd < 7; xd++)
                    {
                        md = GetSafeDouble(sItem["KYQD" + xd].Trim());
                        string pHXSxd = sItem["PHXS" + xd].Trim();
                        if (md >= 1.5 && pHXSxd == "内聚破坏")
                        {
                            syHg++;
                        }
                    }
                    if (syHg == 6)
                    {
                        sItem["DZPD"] = "合格";
                    }
                    else
                    {
                        sItem["DZPD"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                }
                else
                {
                    syHg = 0;
                    for (int xd = 1; xd < 4; xd++)
                    {
                        md = GetSafeDouble(sItem["KYHZ" + xd].Trim());
                        string pHXxd = sItem["PHX" + xd].Trim();
                        if (md >= 1.5 && pHXxd == "内聚破坏")
                        {
                            syHg++;
                        }
                        sItem["PHXS1"] = "----";
                        sItem["PHXS2"] = "----";
                        sItem["PHXS3"] = "----";
                        sItem["PHXS4"] = "----";
                        sItem["PHXS5"] = "----";
                        sItem["PHXS6"] = "----";
                    }
                    if (syHg == 3)
                    {
                        sItem["DZPD"] = "合格";
                    }
                    else if (syHg == 2)
                    {
                        sItem["DZPD"] = "加倍复试";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        sItem["DZPD"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }

                }
                //单组判断
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                    qualifiedCount++;
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }

            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (groupCount % 5 == 0)
            {
                groupCount = groupCount - 1;
            }
            int xc = groupCount / 5 + 2;
            if (xc == 0)
            {
                wz = "结果详见报告第2页。";
            }
            else
            {
                wz = "结果详见报告第2" + "～" + xc + "页。";
            }

            if (groupCount == qualifiedCount)//合格数量
            {
                jsbeizhu = "该正拉粘结强度共测" + S_JLS.Count + "组,均符合";
            }
            else if (0 == qualifiedCount)
            {
                jsbeizhu = "该正拉粘结强度共测" + S_JLS.Count + "组,均不符合";
            }
            else
            {
                jsbeizhu = "该正拉粘结强度共测" + S_JLS.Count + "组,有"+qualifiedCount+"组符合";
            }
            if (sign)
            {
                jsbeizhu = jsbeizhu + "设计要求。"+ wz;
            }
            else
            {
                jsbeizhu = jsbeizhu + "标准要求。" +wz;
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
