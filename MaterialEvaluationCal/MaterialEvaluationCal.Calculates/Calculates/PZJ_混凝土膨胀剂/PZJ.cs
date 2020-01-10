using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class PZJ : BaseMethods
    {
        public static void Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_PZJ_DJ"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();

            List<double> mtmpList = new List<double>();
            string mcalBh, mlongStr = "";
            List<double> mkyqdArray = new List<double>();
            List<double> mkyhzArray = new List<double>();
            List<double> mtmpArray = new List<double>();

            double mSjcc, mMj, mSjcc1 = 0;
            double mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd = 0;
            double md = 0, md1 = 0, md2 = 0, pjmd, Gs, cd1 = 0, cd2 = 0, sum = 0;
            string mSjdjbh, mSjdj = "";
            double mSz, mQdyq, mHsxs, mttjhsxs = 0;
            string mJSFF = "";
            bool mItemHg = true;//每一条检测项合格标识
            bool mSFwc = true;
            bool flag = true;
            string pdStr = "";//范围值判断结果 合格/不合格
            mttjhsxs = 1;
            int mbhggs = 0;//不合格数量
            bool mFlag_Hg, mFlag_Bhg = true;
            bool sign = true;
            try
            {
                mbhggs = 0;
                mFlag_Hg = false;
                mFlag_Bhg = false;
                foreach (var jcxm in jcxmItems)
                {
                    if (jcxm.ToUpper().Contains("BGJG"))
                    {
                        continue;
                    }

                    mItemHg = true;
                    //var MItem = retData[jcxm]["M_PZJ"];
                    var SItem = retData[jcxm]["S_PZJ"];
                    var XQData = retData[jcxm]["S_BY_RW_XQ"];
                    int index = 0;
                    //遍历每条数据
                    foreach (var item in SItem)
                    {
                        //XQData[0]["RECID"] = item["RECID"];
                        //XQData[0]["SJWCJSSJ"] = DateTime.Now.ToString();
                        #region 数据准备工作
                        //if (null == MItem)
                        //{
                        //    mItemHg = false;
                        //    XQData[index]["JCJG"] = "不合格";
                        //    XQData[index]["JCJGMS"] = "获取不到主表数据";
                        //    mbhggs = mbhggs + 1;
                        //    mFlag_Bhg = true;
                        //    index++;
                        //    continue;
                        //}

                        //if (string.IsNullOrEmpty(item["SYR"]))
                        //{
                        //    mSFwc = false;
                        //    mItemHg = false;
                        //    XQData[index]["JCJG"] = "不合格";
                        //    XQData[index]["JCJGMS"] = "获取实验人信息失败";
                        //    index++;
                        //    continue;
                        //}
                        var extraFieldsDj = extraDJ.FirstOrDefault(u => u.Keys.Contains("LX") && u.Values.Contains(item["LX"].Trim()));

                        if (null == extraFieldsDj)
                        {
                            mJSFF = "";
                            //MItem[0]["BGBH"] = "";
                            //MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "单组流水号" + item["DZBH"] + "试件尺寸为空";
                            mItemHg = false;
                            XQData[index]["JCJG"] = "不合格";
                            XQData[index]["JCJGMS"] = "获取实验人信息失败";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            index++;

                            continue;
                        }
                        #region 标准表赋值
                        item["G_XDBM"] = extraFieldsDj["XD1"];
                        item["G_XD118"] = extraFieldsDj["XD2"];
                        item["G_CNSJ"] = extraFieldsDj["CNSJ"];
                        item["G_ZNSJ"] = extraFieldsDj["ZNSJ"];
                        item["G_7PZL"] = extraFieldsDj["XZPZLS7D"];
                        item["G_21PZL"] = extraFieldsDj["XZPZLK21D"];
                        item["G_7KYQD"] = extraFieldsDj["KYQD7D"];
                        item["G_28KYQD"] = extraFieldsDj["KYQD28D"];
                        item["G_YHM"] = extraFieldsDj["YHM"];
                        item["G_ZJL"] = extraFieldsDj["JHL"];
                        mJSFF = string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"].ToLower();

                        #endregion

                        List<double> mdList = new List<double>();

                        //if (string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                        //{
                        //    mItemHg = false;
                        //    XQData[index]["JCJG"] = "不合格";
                        //    XQData[index]["JCJGMS"] = "获取实验人信息失败";
                        //    index++;
                        //    continue;
                        //}
                        #endregion
                        switch (jcxm)
                        {
                            case "氧化镁":
                                #region 氧化镁
                                pdStr = IsQualified(item["G_YHM"], item["W_YHM"]);

                                if (pdStr.Equals("不符合"))
                                {
                                    item["GH_YHM"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                    mItemHg = false;
                                }
                                else if (pdStr.Equals("符合"))
                                {
                                    item["GH_YHM"] = "合格";
                                }
                                else
                                {
                                    item["GH_YHM"] = "----";
                                }
                                break;
                            #endregion
                            case "碱含量":
                                #region 碱含量
                                pdStr = IsQualified(item["G_ZJL"], item["W_ZJL"]);
                                if (pdStr.Equals("不符合"))
                                {
                                    item["GH_ZJL"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                    mItemHg = false;
                                }
                                else if (pdStr.Equals("符合"))
                                {
                                    item["GH_ZJL"] = "合格";
                                }
                                else
                                {
                                    item["GH_ZJL"] = "----";
                                }
                                #endregion
                                break;
                            case "细度":
                                #region 细度
                                sign = true;
                                item["BBMJSY"] = "----";
                                item["GH_XD"] = "----";
                                item["S5PJXD"] = "----";
                                for (int i = 1; i < 3; i++)
                                {
                                    md1 = GetSafeDouble(item["BBWDBZ"]);
                                    md2 = GetSafeDouble(item["BBWDSY" + i]);
                                    flag = Math.Abs(md1 - md2) > 3 ? true : false;

                                    if (GetSafeDouble(item["BBMD"]) == GetSafeDouble(item["SYMD"]) && GetSafeDouble(item["BBKXBZ"]) == GetSafeDouble(item["BBKXSY"]))
                                    {
                                        md1 = GetSafeDouble(item["BBYLBZ"].Trim());
                                        md2 = GetSafeDouble(item["BBYLSY" + i].Trim());
                                        md = GetSafeDouble(item["BBMJBZ"].Trim());
                                        md = Math.Pow(md * (md2 / md1), 0.5);
                                    }
                                    else if (GetSafeDouble(item["BBMD"]) == GetSafeDouble(item["SYMD"]) && GetSafeDouble(item["BBKXBZ"]) != GetSafeDouble(item["BBKXSY"]))
                                    {
                                        md1 = GetSafeDouble(item["BBYLBZ"].Trim());
                                        md2 = GetSafeDouble(item["BBYLSY" + i].Trim());
                                        md = GetSafeDouble(item["BBMJBZ"].Trim());
                                        md = Math.Pow(md * (md2 / md1), 0.5);

                                        md1 = GetSafeDouble(item["BBKXBZ"].Trim());
                                        md2 = GetSafeDouble(item["BBKXSY"].Trim());
                                        md = GetSafeDouble(item["BBMJBZ"].Trim());
                                        md = Math.Pow((md * (1 - md1) / (1 - md2) * (md2 / md1)), (3.0 / 2));
                                    }
                                    else
                                    {
                                        md1 = GetSafeDouble(item["BBYLBZ"].Trim());
                                        md2 = GetSafeDouble(item["BBYLSY" + i].Trim());
                                        md = GetSafeDouble(item["BBMJBZ"].Trim());
                                        md = Math.Pow(md * (md2 / md1), 0.5);

                                        md1 = GetSafeDouble(item["BBKXBZ"].Trim());
                                        md2 = GetSafeDouble(item["BBKXSY"].Trim());
                                        md = GetSafeDouble(item["BBMJBZ"].Trim());
                                        md = Math.Pow((md * (1 - md1) / (1 - md2) * (md2 / md1)), (3.0 / 2));

                                        md1 = GetSafeDouble(item["BBMD"].Trim());
                                        md2 = GetSafeDouble(item["SYMD"].Trim());
                                        md = md * md1 / md2;
                                    }

                                    if (flag)
                                    {
                                        md1 = GetSafeDouble(item["BBKQBZ"]);
                                        md2 = GetSafeDouble(item["BBKQSY" + i].Trim());
                                        md = Math.Pow(md * (md2 / md1), 0.5);
                                    }
                                    md = Round(md, 0);

                                    mdList.Add(md);
                                    sum += md;
                                }

                                pjmd = Math.Round(sum / 2 / 10, 0) * 10;

                                item["BBMJSY"] = pjmd.ToString("0");
                                md = Math.Abs(mdList[0] - mdList[1]);

                                if (md / mdList[0] > 0.02 || md / mdList[1] > 0.02)
                                {
                                    item["BBMJSY"] = "重做";
                                }

                                sign = IsQualified(item["G_XDBM"], item["BBMJSY"]) == "不符合" ? false : true;

                                //'计算1.18mm筛了
                                sum = 0;
                                if (string.IsNullOrEmpty((item["S5XZXS"])))
                                {
                                    item["S5XZXS"] = "1";
                                }

                                for (int i = 1; i < 3; i++)
                                {
                                    md1 = GetSafeDouble(item["SY5ZL" + i].Trim());
                                    md2 = GetSafeDouble(item["YP5ZL" + i].Trim());
                                    md = Round(100 * md1 / md2 * GetSafeDouble(item["S5XZXS"].Trim()), 1);

                                    item["S5XD" + i] = md.ToString("0.0");
                                    sum = sum + md;
                                }

                                pjmd = Round(sum / 2, 1);
                                item["S5PJXD"] = pjmd.ToString("0.0");
                                sign = IsQualified(item["G_XD118"], item["S5PJXD"]) == "不符合" ? false : true;

                                //总结
                                item["GH_XD"] = sign ? "合格" : "不合格";
                                mbhggs = sign ? mbhggs : mbhggs + 1;

                                if (sign)
                                    mFlag_Hg = true;
                                else
                                    mFlag_Bhg = true;
                                #endregion
                                break;
                            case "凝结时间":
                                #region 凝结时间
                                flag = true;
                                sign = true;
                                flag = string.IsNullOrEmpty(item["JSSJH"]) && IsNumeric(item["JSSJH"]) ? flag : false;
                                flag = string.IsNullOrEmpty(item["JSSJM"]) && IsNumeric(item["JSSJM"]) ? flag : false;
                                flag = string.IsNullOrEmpty(item["CNSJH"]) && IsNumeric(item["CNSJH"]) ? flag : false;
                                flag = string.IsNullOrEmpty(item["CNSJM"]) && IsNumeric(item["CNSJM"]) ? flag : false;
                                flag = string.IsNullOrEmpty(item["ZNSJH"]) && IsNumeric(item["ZNSJH"]) ? flag : false;
                                flag = string.IsNullOrEmpty(item["ZNSJM"]) && IsNumeric(item["ZNSJM"]) ? flag : false;

                                if (!flag)
                                {
                                    mSFwc = false;
                                }
                                else
                                {
                                    sum = 0;
                                    md = Round(60 * (GetSafeDouble(item["JSSJH"] + GetSafeDouble(item["JSSJM"]))), 0);
                                    md1 = Round(60 * (GetSafeDouble(item["CNSJH"] + GetSafeDouble(item["JSSJM"]))), 0);
                                    md2 = Round(60 * (GetSafeDouble(item["ZNSJH"] + GetSafeDouble(item["ZNSJM"]))), 0);
                                    md1 = Round(md1 - md, 0);
                                    md2 = Round(md2 - md, 0);
                                    item["CNSJ"] = md1.ToString("0");
                                    item["ZNSJ"] = md2.ToString("0");
                                    sign = IsQualified(item["G_CNSJ"], item["CNSJ"]) == "不符合" ? false : true;
                                    sign = IsQualified(item["G_ZNSJ"], item["ZNSJ"]) == "不符合" ? false : true;
                                    item["GH_NJSJ"] = sign ? "合格" : "不合格";
                                    mbhggs = sign ? mbhggs : mbhggs + 1;
                                    if (sign)
                                        mFlag_Hg = true;
                                    else
                                        mFlag_Bhg = true;
                                }
                                #endregion
                                break;
                            case "限制膨胀率":
                                #region 限制膨胀率
                                flag = true;
                                sign = true;
                                item["GH_PZL"] = "----";
                                item["PJ7PZL"] = "----";
                                item["PJ21PZL"] = "----";

                                //水中7d
                                for (int i = 1; i < 3; i++)
                                {
                                    flag = IsNumeric(item["P7CS" + i]) ? flag : false;
                                    flag = IsNumeric(item["P7XZ" + i]) ? flag : false;
                                }
                                if (!flag)
                                {
                                    mSFwc = false;
                                }
                                else
                                {
                                    sum = 0;

                                    for (int i = 1; i < 3; i++)
                                    {
                                        md1 = GetSafeDouble(item["P7XZ" + i].Trim());
                                        md2 = GetSafeDouble(item["P7CS" + i].Trim());
                                        md = Round(100 * (md1 - md2) / 140, 3);
                                        item["P7ZL" + i] = md.ToString("0.000");
                                        sum = sum + md;
                                    }
                                    pjmd = Round(sum / 2, 3);
                                    item["PJ7PZL"] = pjmd.ToString("0.000");
                                    sign = IsQualified(item["G_7PZL"], item["PJ7PZL"]) == "不符合" ? false : true;
                                }

                                //空中21d
                                for (int i = 1; i < 3; i++)
                                {
                                    flag = IsNumeric(item["P21CS" + i]) ? flag : false;
                                    flag = IsNumeric(item["P21XZ" + i]) ? flag : false;
                                }
                                if (!flag)
                                {
                                    mSFwc = false;
                                }
                                else
                                {
                                    sum = 0;
                                    for (int i = 1; i < 3; i++)
                                    {
                                        md1 = GetSafeDouble(item["P21XZ" + i].Trim());
                                        md2 = GetSafeDouble(item["P21CS" + i].Trim());
                                        md = Round(100 * (md1 - md2) / 140, 3);
                                        item["P21ZL" + i] = md.ToString("0.000");
                                        sum = sum + md;
                                    }
                                    pjmd = Round(sum / 2, 3);
                                    item["PJ21PZL"] = pjmd.ToString("0.000");
                                    sign = IsQualified(item["G_21PZL"], item["PJ21PZL"]) == "不符合" ? false : true;
                                }

                                //总括
                                item["GH_PZL"] = sign ? "合格" : "不合格";
                                mbhggs = sign ? mbhggs : mbhggs + 1;
                                if (sign)
                                    mFlag_Hg = true;
                                else
                                    mFlag_Bhg = true;
                                #endregion
                                break;
                            case "7d抗压强度":
                                #region 7d抗压强度
                                flag = true;

                                for (int i = 1; i < 7; i++)
                                {
                                    flag = IsNumeric(item["KY7HZ" + i]) ? flag : false;
                                }
                                if (!flag)
                                {
                                    mSFwc = false;
                                }
                                else
                                {
                                    sum = 0;
                                    mdList = new List<double>();
                                    for (int i = 1; i < 7; i++)
                                    {
                                        md1 = GetSafeDouble(item["KY7HZ" + i].Trim());
                                        md = Round(10 * md1 / 16, 1);
                                        item["KY7QD" + i] = md.ToString("0.0");
                                        mdList.Add(md);
                                        sum = sum + md;
                                    }
                                    pjmd = Round(sum / 6, 1);
                                    Gs = 0;
                                    sum = 0;

                                    for (int i = 0; i < 6; i++)
                                    {
                                        md = Round(100 * Math.Abs(mdList[i] - pjmd) / pjmd, 0);

                                        if (md <= 10)
                                        {
                                            Gs += 1;
                                            sum += mdList[0];
                                        }
                                    }
                                    if (Gs >= 5)
                                    {
                                        item["PJKY7QD"] = Round(sum / Gs, 1).ToString("0.0");
                                    }
                                    else
                                        item["PJKY7QD"] = "重做";

                                }
                                #endregion
                                break;
                            case "28d抗压强度":
                                #region 28d抗压强度
                                flag = true;
                                //'单位是kN
                                for (int i = 1; i < 7; i++)
                                {
                                    flag = IsNumeric(item["KY28HZ" + i]) ? flag : false;
                                }
                                if (!flag)
                                {
                                    mSFwc = false;
                                }
                                else
                                {
                                    sum = 0;
                                    mdList = new List<double>();
                                    for (int i = 1; i < 7; i++)
                                    {
                                        md1 = GetSafeDouble(item["KY28HZ" + i].Trim());
                                        md = Round(10 * md1 / 16, 1);
                                        item["KY28QD" + i] = md.ToString("0.0");
                                        mdList.Add(md);
                                        sum = sum + md;
                                    }
                                    pjmd = Round(sum / 6, 1);
                                    Gs = 0;
                                    sum = 0;

                                    for (int i = 0; i < 6; i++)
                                    {
                                        md = Round(100 * Math.Abs(mdList[i] - pjmd) / pjmd, 0);

                                        if (md <= 10)
                                        {
                                            Gs += 1;
                                            sum += mdList[0];
                                        }
                                    }
                                    if (Gs >= 5)
                                    {
                                        item["PJKY28QD"] = Round(sum / Gs, 1).ToString("0.0");
                                    }
                                    else
                                        item["PJKY28QD"] = "重做";
                                }
                                #endregion
                                break;
                            default:
                                if (item["JCXM"].Contains("抗压强度"))
                                {
                                    sign = true;
                                    flag = IsQualified(item["G_7KYQD"], item["PJKY7QD"]) == "不符合" ? flag : false;
                                    flag = IsQualified(item["G_28KYQD"], item["PJKY28QD"]) == "不符合" ? flag : false;
                                    item["GH_KYQD"] = sign ? "合格" : "不合格";

                                    mbhggs = sign ? mbhggs : mbhggs + 1;
                                    if (sign)
                                        mFlag_Hg = true;
                                    else
                                        mFlag_Bhg = true;
                                }
                                break;
                        }

                        if (mbhggs == 0)
                        {
                            XQData[index]["JCJG"] = "合格";
                            XQData[index]["JCJGMS"] = "该组试样所检项目符合{}" + "标准要求。";
                            //MItem[0]["JSBEIZHU"] = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
                            //MItem[0]["JCJG"] = "合格";
                        }
                        else
                        {
                            XQData[index]["JCJG"] = "不合格";
                            XQData[index]["JCJGMS"] = "该组试样不符合{}" + "标准要求。";
                            //MItem[0]["JSBEIZHU"] = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
                            //MItem[0]["JCJG"] = "不合格";
                            if (mFlag_Bhg && mFlag_Hg)
                            {
                                XQData[index]["JCJGMS"] = "该组试样所检项目部分符合{}" + "标准要求。";
                                //MItem[0]["JSBEIZHU"] = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                            }
                        }
                    }
                 
                }


            }
            catch (Exception ex)
            {
                err = ex.Message;
            }

            #region 添加最终报告

            IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
            IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
            if (mbhggs > 0)
            {
                //不合格
                bgjgDic.Add("JCJG", "不合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合不标准要求。" + err);
            }
            else
            {
                bgjgDic.Add("JCJG", "合格");
                bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。" + err);
            }

            bgjg.Add(bgjgDic);
            retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
            retData["BGJG"].Add("BGJG", bgjg);
            #endregion
            #endregion
            /************************ 代码结束 *********************/

        }
    }
}
