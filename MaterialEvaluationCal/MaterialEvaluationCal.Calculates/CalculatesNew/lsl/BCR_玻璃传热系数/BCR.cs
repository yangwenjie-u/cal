using Microsoft.VisualBasic;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class BCR : BaseMethods
    {
        public void Calc()
        {
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_BCR"];
            var mrsDj = dataExtra["BZ_BCR_DJ"];
            var MSBW = data["MS_BW"];
            var MItem = data["M_BCR"];
            if (!data.ContainsKey("M_BCR"))
            {
                data["M_BCR"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];

            foreach (var sItem in SItem)
            {
                bool flag = true;
                double md1, md2, sum, pjmd, md;
                int gs;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if (sItem["SJGG"].Contains("×"))
                {
                    sItem["MCCD"] = sItem["SJGG"].Substring(0, sItem["SJGG"].IndexOf("×"));
                    sItem["MCCD"] = sItem["MCCD"] + "mm";
                    sItem["MCKD"] = sItem["SJGG"].Substring(sItem["SJGG"].IndexOf("×") + 1);
                    sItem["MCKD"] = sItem["MCKD"] + "mm";
                }

                gs = MSBW.Count;
                if (gs > 0)
                {
                    var msbw_filter = MSBW[0];
                    flag = true;
                    for (int i = 1;i<=6;i++)
                    {
                        if (!IsNumeric(msbw_filter["相对湿度" + i]) || string.IsNullOrEmpty(msbw_filter["相对湿度" + i]))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag) 
                    {
                        sum = 0;
                        for (int i = 1; i <= 6; i++)
                        {
                            sum = sum + Conversion.Val(msbw_filter["相对湿度" + i]);
                        }
                        pjmd = sum / 6;
                        pjmd = Round(pjmd,1);
                        sItem["XDSD"] = pjmd.ToString();
                    }
                }

                if (jcxm.Contains("、传热系数、"))
                {
                    flag = true;
                    gs = mrsDj.Count;
                    int i;
                    for (i = 0; i < gs;i++)
                    {
                        string djfw = IsQualified(mrsDj[i]["DJFW"], sItem["CRXS"], true);
                        if (djfw == "符合")
                        {
                            mItem["SSFJ"] = mrsDj[i]["DJBH"] + "级";
                            mItem["SSFW"] = mrsDj[i]["DJFW"];
                            break;
                        }
                    }
                    if (i > gs)
                    {
                        mItem["SSFJ"] = "不符合任一级别";
                        mItem["SSFW"] = "----";
                        flag = false;
                    }
                    if (sItem["CRXSSJZ"].Length != 0 && sItem["CRXSSJZ"] != "----")
                    {
                        md1 = GetSafeDouble(sItem["CRXS"]);
                        md2 = GetSafeDouble(sItem["CRXSSJZ"]);
                        if (md1 <= md2)
                        {
                            sItem["GH_CRXS"] = "符合";
                        }
                        else
                        {
                            sItem["GH_CRXS"] = "不符合";
                            mAllHg = false;
                        }
                        jsbeizhu = jsbeizhu + sItem["GH_CRXS"] + "设计要求。";
                    }else
                    {
                        sItem["GH_CRXS"] = "----";
                    }
                    
                    if (jcxm.Contains("、抗结露因子、"))
                    {
                        flag = true;
                        gs = mrsDj.Count;
                        for (i = 0; i < gs; i++)
                        {
                            if (IsQualified(mrsDj[i]["KJLDJFW"], sItem["JLYZ"], true) == "符合")
                            {
                                mItem["SSFJ1"] = mrsDj[i]["DJBH"] + "级";
                            }
                        }
                        if (i > gs)
                        {
                            mItem["SSFJ1"] = "不符合任一级别";
                            flag = false;
                        }
                        sItem["GH_CRXS"] = "----";
                    }
                    else
                    {
                        sItem["JLYZ"] = "----";
                        sItem["GH_JLYZ"] = "----";
                        sItem["JLJS2"] = "----";
                    }
                }


                if (sItem["CRXSSJZ"].Length != 0 && sItem["CRXSSJZ"] != "----")
                {
                    if (mAllHg) jsbeizhu = "该试样传热系数检测结果符合设计要求";
                    else jsbeizhu = "该试样传热系数检测结果不符合设计要求";
                }
                else
                {
                    sItem["G_TSB"] = "----";
                    sItem["GH_TSB"] = "----";
                    jsbeizhu = "该试样传热系数检测结果如上";
                }
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
#endregion
            /************************ 代码结束 *********************/
        }
    }
}
