using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class WZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_WZ_DJ"];

            //var jcxmItems = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不符合";
            var jsbeizhu = "合格";
            var S_WZS = data["S_WZ"];
            if (!data.ContainsKey("M_WZ"))
            {
                data["M_WZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_WZ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            bool itemHG = true;//判断单组是否合格

            bool sign = true;
            double sum = 0;
            double md1, md2, md, pjmd = 0;
            bool flag = true;

            List<double> nArr = new List<double>();
            foreach (var sItem in S_WZS)
            {

                //wtbh = sItem["WTBH"].Trim();
                //dzbh = sItem["DZBH"].Trim();
                if (null == sItem["SJHD"])//sItem["SJHD"] = 40
                {
                    flag = false;
                }
                for (int i = 1; i < 4; i++)
                {
                    if (string.IsNullOrEmpty(sItem["BWHD" + i]))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    for (int i = 1; i < 4; i++)
                    {
                        sItem["X" + i + "FCZF"] = sItem["BWGZ"];
                        sItem["BWZL" + i] = sItem["YPMC"];

                    }

                    for (int i = 1; i < 4; i++)
                    {
                        if (sItem["WHPD" + i].Trim() == "不符合")
                        {
                            sign = false;
                            sItem["WHJGPD1"] = "不符合";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            sItem["WHJGPD1"] = "符合";
                        }
                    }
                    sum = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        md = GetSafeDouble(sItem["BWHD" + i].Trim());
                        sum = sum + md;
                        nArr.Add(md);
                        //nArr[i] = md;
                    }
                    pjmd = Math.Round(sum / 3, 0);
                    //Call calc_sort(nArr)
                    nArr.Sort();
                    sItem["AVG_HD"] = pjmd.ToString().Trim();
                    md1 = GetSafeDouble(sItem["AVG_HD"]);
                    md2 = GetSafeDouble(sItem["SJHD"].Trim());
                    md = Math.Round(100 * md1 / md2, 0);
                    sItem["PCT_AVG"] = md.ToString().Trim();
                    sign = md >= 95 ? sign : false;
                    sItem["MIN_HD"] = Math.Round(nArr[0], 0).ToString("0");
                    md1 = GetSafeDouble(sItem["MIN_HD"]);
                    md2 = GetSafeDouble(sItem["SJHD"].Trim());
                    md = Math.Round(100 * md1 / md2, 0);
                    sItem["PCT_MIN"] = Math.Round(md, 0).ToString("0");
                    sign = md >= 90 ? sign : false;
                    sItem["PD_HD"] = sign ? "符合" : "不符合";
                    if (sItem["PD_HD"] == "不符合")
                    {
                        mAllHg = false;
                        itemHG = false;
                    }
                }
                else
                {
                    sItem["PD_HD"] = "----";
                    sItem["JCJG"] = "----";
                    //MItem[0]["JGSM"] = "数据录入不完整。";
                    MItem[0]["JCJG"] = "----";
                }
                flag = true;
                for (int i = 3; i < 6; i++)
                {
                    if (string.IsNullOrEmpty(sItem["BWHD" + i]) || sItem["BWHD" + i] == "----")
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    for (int i = 3; i < 6; i++)
                    {
                        sItem["X" + i + "FCZF"] = sItem["BWGZ2"];
                        sItem["BWZL" + i] = sItem["YPMC2"];
                    }
                    sign = true;
                    for (int i = 3; i < 6; i++)
                    {
                        if (sItem["WHPD" + i] == "不符合")
                        {
                            sign = false;
                            sItem["WHJGPD2"] = "不符合";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            sItem["WHJGPD2"] = "符合";
                        }
                    }
                    sum = 0;
                    for (int i = 3; i < 6; i++)
                    {
                        md = GetSafeDouble(sItem["BWHD" + i].Trim());
                        sum = sum + md;
                        nArr.Add(md);
                        //nArr[i] = md;
                    }
                    pjmd = Math.Round(sum / 3, 0);
                    //Call calc_sort(nArr)
                    nArr.Sort();
                    sItem["AVG2_HD"] = Math.Round(pjmd, 0).ToString("0");
                    md1 = GetSafeDouble(sItem["AVG2_HD"]);
                    md2 = GetSafeDouble(sItem["SJHD2"].Trim());
                    md = Math.Round(100 * md1 / md2, 0);
                    sItem["PCT2_AVG"] = md.ToString("0");
                    sign = md >= 95 ? sign : false;
                    sItem["MIN2_HD"] = Math.Round(nArr[3], 0).ToString();
                    md1 = GetSafeDouble(sItem["MIN2_HD"]);
                    md2 = GetSafeDouble(sItem["SJHD2"].Trim());
                    md = Math.Round(100 * md1 / md2, 0);
                    sItem["PCT2_MIN"] = Math.Round(md, 0).ToString("0");
                    sign = md >= 90 ? sign : false;
                    sItem["PD_HD2"] = sign ? "符合" : "不符合";
                    if (sItem["PD_HD2"] == "不符合")
                    {
                        mAllHg = false;
                        itemHG = false;
                    }
                    sItem["XYTP4"] = "芯样4";
                    sItem["XYTP5"] = "芯样5";
                    sItem["XYTP6"] = "芯样6";
                }
                else
                {
                    sItem["QYBW4"] = "----";
                    sItem["QYBW5"] = "----";
                    sItem["QYBW6"] = "----";
                    sItem["XYWG4"] = "----";
                    sItem["XYWG5"] = "----";
                    sItem["XYWG6"] = "----";
                    sItem["BWZL4"] = "----";
                    sItem["BWZL5"] = "----";
                    sItem["BWZL6"] = "----";
                    sItem["X4FCZF"] = "----";
                    sItem["X5FCZF"] = "----";
                    sItem["X6FCZF"] = "----";
                    sItem["WHPD4"] = "----";
                    sItem["WHPD5"] = "----";
                    sItem["WHPD6"] = "----";
                    sItem["BWHD4"] = "----";
                    sItem["BWHD5"] = "----";
                    sItem["BWHD6"] = "----";
                    sItem["AVG2_HD"] = "----";
                    sItem["MIN2_HD"] = "----";
                    sItem["PD_HD2"] = "----";
                    sItem["XYTP4"] = "----";
                    sItem["XYTP5"] = "----";
                    sItem["XYTP6"] = "----";
                    sItem["WHJGPD2"] = "----";
                }

                if (sItem["WHJGPD1"] == "不符合" || sItem["WHJGPD2"] == "不符合" || sItem["PD_HD"] == "不符合" || sItem["PD_HD2"] == "不符合")
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    itemHG = false;
                    MItem[0]["JCJG"] = "不合格";

                    if (sItem["WHJGPD1"] == "不符合" || sItem["WHJGPD2"] == "不符合" && sItem["PD_HD"] == "不符合" || sItem["PD_HD2"] == "不符合")
                    {
                        jsbeizhu = "该组试样:节能构造、保温层厚度不符合设计要求。";
                        //mbhggs++;
                        mAllHg = false;
                        itemHG = false;
                    }
                    if (sItem["PD_HD2"] == "----")
                    {
                        if (sItem["WHJGPD1"] == "不符合" || sItem["WHJGPD2"] == "不符合" && sItem["PD_HD"] == "符合")
                        {
                            jsbeizhu = "该组试样:保温层厚度符合设计要求,节能构造不符合设计要求。";
                            //mbhggs++;
                            mAllHg = false;
                            itemHG = false;
                        }
                        if (sItem["WHJGPD1"] == "符合" && sItem["WHJGPD2"] == "符合" && sItem["PD_HD"] == "不符合")
                        {
                            jsbeizhu = "该组试样:保温层厚度符合设计要求,节能构造不符合设计要求。";
                            //mbhggs++;
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                    else
                    {
                        if (sItem["WHJGPD1"] == "不符合" || sItem["WHJGPD2"] == "不符合" && sItem["PD_HD"] == "符合" && sItem["PD_HD2"] == "符合")
                        {
                            jsbeizhu = "该组试样:节能构造符合设计要求,保温层厚度不符合设计要求。";
                            //mbhggs++;
                            mAllHg = false;
                            itemHG = false;
                        }
                        if (sItem["WHJGPD1"] == "符合" && sItem["WHJGPD2"] == "符合" && sItem["PD_HD"] == "不符合" || sItem["PD_HD2"] == "不符合")
                        {
                            jsbeizhu = "该组试样:节能构造符合设计要求,保温层厚度不符合设计要求。";
                            //mbhggs++;
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                }
                else
                {
                    sItem["JCJG"] = "符合";
                    MItem[0]["JCJG"] = "合格";
                    jsbeizhu = "该组试样:节能构造、保温层厚度符合设计要求。";
                }
                //每条
                if (itemHG)
                {
                    sItem["JCJG"] = "符合";
                }
                else
                {
                    sItem["JCJG"] = "不符合";
                }
            }
            //添加最终报告
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
