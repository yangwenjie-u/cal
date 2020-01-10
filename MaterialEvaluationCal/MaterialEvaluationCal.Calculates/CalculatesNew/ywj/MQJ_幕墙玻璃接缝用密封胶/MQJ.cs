using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class MQJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_MQJ_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_MQJ"];

            if (!data.ContainsKey("M_MQJ"))
            {
                data["M_MQJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_MQJ"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";


            bool flag = true;
            bool sign = true;
            double xd, Gs = 0;
            int md1, md2, md, sum, pjqd = 0;
            var mJSFF = "";
            var mbhggs = 0;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;

            int mbHggs = 0;
            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                var mrsDj = extraDJ.FirstOrDefault(u => u["YPLB"] == sItem["YPLB"] && u["JLX"] == sItem["CPCJB"]);

                if (null == mrsDj)
                {
                    mJSFF = "";
                    jsbeizhu = "依据不详\r\n";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                sItem["G_BGSJ"] = mrsDj["G_BGSJ"];
                sItem["G_JCX"] = mrsDj["G_JCX"];
                sItem["G_LSML1"] = mrsDj["G_LSML1"];
                sItem["G_LSML2"] = mrsDj["G_LSML2"];
                sItem["G_SYQ"] = mrsDj["G_SYQ"];
                sItem["G_TXHFL"] = mrsDj["G_TXHFL"];
                sItem["G_WG"] = mrsDj["G_WG"];
                sItem["G_XCD"] = mrsDj["G_XCD"];
                sItem["G_ZLSSL"] = mrsDj["G_ZLSSL"];
                sItem["G_JCX"] = mrsDj["G_JCX"];
                //MItem[0]["WHICH"] = mrsDj["WHICH"];

                sign = true;
                if (jcxm.Contains("、外观、"))
                {
                    sign = !string.IsNullOrEmpty(sItem["W_WG"]) ? sign : false;
                    if (sign)
                    {
                        if (MItem[0]["GH_WG"] == "合格")
                            MItem[0]["GH_WG"] = "合格";
                        else
                            MItem[0]["GH_WG"] = "不合格";
                    }
                    else
                    {
                        //MItem[0]["SYR"] = ""
                    }
                }
                else
                {
                    sItem["G_WG"] = "----";
                    sItem["W_WG"] = "----";
                    MItem[0]["GH_WG"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、下垂度、"))
                {
                    sign = IsNumeric(sItem["W_XCDCZ"]) ? sign : false;
                    sign = !string.IsNullOrEmpty(sItem["W_XCDSP"]) ? sign : false;

                    if (sign)
                    {
                        MItem[0]["GH_XCDCZ"] = IsQualified(sItem["G_XCD"], sItem["W_XCDCZ"], false);
                        if (sItem["W_XCDSP"] == "无变形")
                            MItem[0]["GH_XCDSP"] = "合格";
                        else
                            MItem[0]["GH_WG"] = "不合格";
                    }
                    else
                    {
                        //MItem[0]["SYR"] = ""
                    }
                }
                else
                {
                    sItem["W_XCDCZ"] = "----";
                    sItem["W_XCDSP"] = "----";
                    sItem["G_XCD"] = "----";
                    MItem[0]["GH_XCDCZ"] = "----";
                    MItem[0]["GH_XCDSP"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、挤出性、"))
                {
                    //sign = IsNumeric(sItem["W_JCX"]) ? sign : false;
                    //sign = IsNumeric(sItem["W_MIN_JCX"]) ? sign : false;
                    //if (sign)
                    //{
                    //    var mlsfhg1 = IsQualified(sItem["G_JCX"], sItem["W_JCX"], false);
                    //    var mlsfhg2 = IsQualified(sItem["G_JCX"], sItem["W_MIN_JCX"], false);
                    //    if (mlsfhg1 == "不合格" || mlsfhg2 == "不合格")
                    //        MItem[0]["GH_JCX"] = "不合格";
                    //    else
                    //        MItem[0]["GH_JCX"] = "合格";
                    //}
                    //else
                    //{
                    //    //MItem[0]["SYR"] = ""
                    //}
                }
                else
                {
                    sItem["G_JCX"] = "----";
                    sItem["W_JCX"] = "----";
                    MItem[0]["GH_JCX"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、表干时间、"))
                {
                    sign = IsNumeric(sItem["W_BGSJ1"]) ? sign : false;
                    sign = IsNumeric(sItem["W_BGSJ2"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_BGSJ1"] = IsQualified(sItem["G_BGSJ"], sItem["W_BGSJ1"], false);
                        MItem[0]["GH_BGSJ2"] = IsQualified(sItem["G_BGSJ"], sItem["W_BGSJ2"], false);
                    }
                    else
                    {
                        //MItem[0]["SYR"] = ""
                    }
                }
                else
                {
                    sItem["G_BGSJ"] = "----";
                    MItem[0]["GH_BGSJ1"] = "----";
                    MItem[0]["GH_BGSJ2"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、相容性、"))
                {
                    sItem["G_XRX1"] = "试验试件与对比试件颜色变化一致";
                    sItem["G_XRX2"] = "试验试件、对比试件与玻璃粘结破坏面积的差值≤ 5";
                    var XRX_YSBH = sItem["XRX_YSBH"];
                    if (XRX_YSBH.Contains("试验试件与对比试件颜色变化一致"))
                    {
                        MItem[0]["GH_XRX_YSBH"] = "合格";
                    }
                    else
                    {
                        MItem[0]["GH_XRX_YSBH"] = "不合格";
                    }

                    var XRX_BL = Conversion.Val(sItem["XRX_BL"]);
                    if (XRX_BL <= 5)
                        MItem[0]["GH_XRX_BL"] = "合格";
                    else
                        MItem[0]["GH_XRX_BL"] = "不合格";
                }
                else
                {
                    sItem["G_XRX1"] = "----";
                    sItem["G_XRX2"] = "----";
                    sItem["XRX_YSBH"] = "----";
                    sItem["XRX_BL"] = "----";
                    MItem[0]["GH_XRX_YSBH"] = "----";
                    MItem[0]["GH_XRX_YSBH"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、弹性恢复率、"))
                {
                    sign = IsNumeric(sItem["W_TXHHL"]) ? sign : false;
                    if (sign)

                        MItem[0]["GH_TXHHL"] = IsQualified(sItem["G_TXHFL"], sItem["W_TXHHL"], false);
                    else
                    { //MItem[0]["SYR"] = ""
                    }
                }
                else
                {
                    sItem["G_TXHFL"] = "----";
                    sItem["W_TXHHL"] = "----";
                    MItem[0]["GH_TXHHL"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、拉伸模量、"))
                {
                    sign = IsNumeric(sItem["W_LSML1"]) ? sign : false;
                    sign = IsNumeric(sItem["W_LSML2"]) ? sign : false;
                    if (sign)
                    {
                        var mlsfhg1 = IsQualified(sItem["G_LSML1"], sItem["W_LSML1"], false);
                        var mlsfhg2 = IsQualified(sItem["G_LSML2"], sItem["W_LSML2"], false);

                        if (mlsfhg1 == "不合格" || mlsfhg2 == "不合格")
                            MItem[0]["GH_LSML"] = "不合格";
                        else
                            MItem[0]["GH_LSML"] = "合格";
                        if (sItem["CPCJB"].Contains("高模量"))
                        {
                            sItem["G_LSML1"] = sItem["G_LSML1"] + "或";
                        }
                    }
                    else
                    { //MItem[0]["SYR"] = ""
                    }
                }
                else
                {
                    sItem["G_LSML1"] = "----";
                    sItem["G_LSML2"] = "----";
                    sItem["W_LSML1"] = "----";
                    sItem["W_LSML2"] = "----";
                    MItem[0]["GH_LSML"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、质量损失率、"))
                {
                    sign = IsNumeric(sItem["W_ZLSSL"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_ZLSSL"] = IsQualified(sItem["G_ZLSSL"], sItem["W_ZLSSL"], false);
                    }
                    else
                    { //MItem[0]["SYR"] = ""
                    }
                }
                else
                {
                    sItem["G_ZLSSL"] = "----";
                    sItem["W_ZLSSL"] = "----";
                    MItem[0]["GH_ZLSSL"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、适用期、"))
                {
                    sign = IsNumeric(sItem["W_SYQ"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_SYQ"] = IsQualified(sItem["G_SYQ"], sItem["W_SYQ"], false);
                    }
                    else
                    { //MItem[0]["SYR"] = ""
                    }
                }
                else
                {
                    sItem["G_SYQ"] = "----";
                    sItem["W_SYQ"] = "----";
                    MItem[0]["GH_SYQ"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、定伸粘结性、"))
                {
                    sign = IsNumeric(sItem["W_DSNJQD"]) ? sign : false;
                    if (sign)
                    {
                        if (MItem[0]["GH_DSNJQD"] == "合格")
                            sItem["W_DSNJQD"] = "无破坏";
                        else
                            sItem["W_DSNJQD"] = "破坏";
                    }
                    else
                    { //MItem[0]["SYR"] = ""
                    }
                }
                else
                {
                    sItem["W_DSNJQD"] = "----";
                    MItem[0]["GH_DSNJQD"] = "----";
                }

                sign = true;
                if (jcxm.Contains("、浸水光照后的定伸粘结性、"))
                {
                    sign = IsNumeric(sItem["W_ZWXFZHNJX"]) ? sign : false;
                    if (sign)
                    {
                        if (MItem[0]["GH_ZWXFZHNJX"] == "合格")
                            sItem["W_ZWXFZHNJX"] = "无破坏";
                        else
                            sItem["W_ZWXFZHNJX"] = "破坏";
                    }
                    else
                    { //MItem[0]["SYR"] = ""
                    }
                }
                else
                {
                    sItem["W_ZWXFZHNJX"] = "----";
                    MItem[0]["GH_ZWXFZHNJX"] = "----";
                }

                if (MItem[0]["GH_XRX_BL"] == "不合格" ||
                    MItem[0]["GH_XRX_YSBH"] == "不合格" ||
                    MItem[0]["GH_BGSJ1"] == "不合格" ||
                    MItem[0]["GH_BGSJ2"] == "不合格" ||
                    MItem[0]["GH_DSNJQD"] == "不合格" ||
                    MItem[0]["GH_JCX"] == "不合格" ||
                    MItem[0]["GH_LSML"] == "不合格" ||
                    MItem[0]["GH_SYQ"] == "不合格" ||
                    MItem[0]["GH_TXHHL"] == "不合格" ||
                    MItem[0]["GH_WG"] == "不合格" ||
                    MItem[0]["GH_XCDCZ"] == "不合格" ||
                    MItem[0]["GH_XCDSP"] == "不合格" ||
                    MItem[0]["GH_ZLSSL"] == "不合格" ||
                    MItem[0]["GH_ZWXFZHNJX"] == "不合格")
                {
                    mbHggs += 1;
                    mAllHg = false;
                }

                if (mbhggs == 0)
                {
                    jsbeizhu = "该组样品所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";

                    sItem["JCJG"] = "合格";
                    mAllHg = true;
                }

                if (mbhggs > 0)
                {
                    jsbeizhu = "该组样品所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";

                    if (MItem[0]["GH_XRX_BL"] == "合格" ||
                        MItem[0]["GH_XRX_YSBH"] == "合格" ||
                        MItem[0]["GH_BGSJ1"] == "合格" ||
                        MItem[0]["GH_BGSJ2"] == "合格" ||
                        MItem[0]["GH_DSNJQD"] == "合格" ||
                        MItem[0]["GH_JCX"] == "合格" ||
                        MItem[0]["GH_LSML"] == "合格" ||
                        MItem[0]["GH_SYQ"] == "合格" ||
                        MItem[0]["GH_TXHHL"] == "合格" ||
                        MItem[0]["GH_WG"] == "合格" ||
                        MItem[0]["GH_XCDCZ"] == "合格" ||
                        MItem[0]["GH_XCDSP"] == "合格" ||
                        MItem[0]["GH_ZLSSL"] == "合格" ||
                        MItem[0]["GH_ZWXFZHNJX"] == "合格")
                    {
                        jsbeizhu = "该组样品所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                    }

                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }
            }

            #region 添加最终报告

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                jsbeizhu = "该组试样不符合" + MItem[0]["PDBZ"] + "标准要求。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
