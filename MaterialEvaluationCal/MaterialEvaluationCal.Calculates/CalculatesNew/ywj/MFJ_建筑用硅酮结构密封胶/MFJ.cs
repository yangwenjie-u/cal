using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class MFJ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            var extraDJ = dataExtra["BZ_MFJ_DJ"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "该组试样的检测结果全部合格";
            var sItems = data["S_MFJ"];

            if (!data.ContainsKey("M_MFJ"))
            {
                data["M_MFJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_MFJ"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";

            bool sign = true;
            int sign1 = 0;

            bool flag = true;
            bool mSFwc = true;
            List<double> nArr = new List<double>();
            double xd, Gs = 0;
            var mJSFF = "";
            var mbhggs = 0;
            var mFlag_Hg = false;
            var mFlag_Bhg = false;


            foreach (var sItem in sItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                sign1 = 0;
                foreach (var mrsDj in extraDJ)
                {
                    if (null == mrsDj)
                    {
                        continue;
                    }

                    if (mrsDj["YPLB"] == sItem["YPLB"] && mrsDj["JLX"] == sItem["CPZF"])
                    {
                        sItem["G_23LSNJQD"] = mrsDj["G_23LSNJQD"];
                        sItem["G_BGSJ"] = mrsDj["G_BGSJ"];
                        sItem["G_YJCNJX"] = mrsDj["G_YJCNJX"];
                        sItem["G_GZNJPHMJ"] = mrsDj["G_GZNJPHMJ"];
                        sItem["G_GZHLSQD"] = mrsDj["G_GZHLSQD"];
                        sItem["G_JCX"] = mrsDj["G_JCX"];
                        sItem["G_JSHLSQD"] = mrsDj["G_JSHLSQD"];
                        sItem["G_JSNJPHMJ"] = mrsDj["G_JSNJPHMJ"];
                        sItem["G_XRX1"] = mrsDj["G_XRX1"];
                        sItem["G_XRX2"] = mrsDj["G_XRX2"];
                        sItem["G_NJPHMJ"] = mrsDj["G_NJPHMJ"];
                        sItem["G_RLH"] = mrsDj["G_RLH"];
                        sItem["G_SYQ"] = mrsDj["G_SYQ"];
                        sItem["G_WG"] = mrsDj["G_WG"];
                        sItem["G_XCD"] = mrsDj["G_XCD"];
                        sItem["G_YD"] = mrsDj["G_YD"];
                        sItem["G_ZDSSL"] = mrsDj["G_ZDSSL"];
                        sign1 = sign1 + 1;
                        break;
                    }
                }
                if (sign1 < 1)
                {
                    mJSFF = "";
                    jsbeizhu = "找不到对应的等级";
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                //mrsmainTable!which = "bgmfj、bgmfj_3"

                if (jcxm.Contains("、外观、"))
                {
                    //MItem[0]["WHICH"] = "bgmfj、bgmfj_1、bgmfj_2";
                    if (MItem[0]["GH_WG"] == "合格")
                        MItem[0]["GH_WG"] = "合格";
                    else
                        MItem[0]["GH_WG"] = "不合格";
                }
                else
                {
                    sItem["G_WG"] = "----";
                    sItem["W_WG"] = "----";
                    MItem[0]["GH_WG"] = "----";
                }

                if (jcxm.Contains("、下垂度、"))
                {
                    //MItem[0]["WHICH"] = "bgmfj、bgmfj_1、bgmfj_2";
                    MItem[0]["GH_XCDCZ"] = IsQualified(sItem["G_XCD"], sItem["W_XCDCZ"], false);
                    if ("不变形" == sItem["W_XCDSP"].Trim())
                        MItem[0]["GH_XCDSP"] = "合格";
                    else
                        MItem[0]["GH_XCDSP"] = "不合格";
                }
                else
                {
                    sItem["W_XCDCZ"] = "----";
                    sItem["W_XCDSP"] = "----";
                    sItem["G_XCD"] = "----";
                    MItem[0]["GH_XCDSP"] = "----";
                    MItem[0]["GH_XCDCZ"] = "----";
                }


                if (jcxm.Contains("、挤出性、"))
                {
                    //MItem[0]["WHICH"] = "bgmfj、bgmfj_1、bgmfj_2";
                    MItem[0]["GH_JCX"] = IsQualified(sItem["G_JCX"], sItem["W_JCX"], false);
                }
                else
                {
                    sItem["G_JCX"] = "----";
                    sItem["W_JCX"] = "----";
                    MItem[0]["GH_JCX"] = "----";
                }
                if (jcxm.Contains("、适用期、"))
                {
                    //MItem[0]["WHICH"] = "bgmfj、bgmfj_1、bgmfj_2";
                    MItem[0]["GH_SYQ"] = IsQualified(sItem["G_SYQ"], sItem["W_SYQ"], false);
                }
                else
                {
                    sItem["G_SYQ"] = "----";
                    sItem["W_SYQ"] = "----";
                    MItem[0]["GH_SYQ"] = "----";
                }

                if (jcxm.Contains("、表干时间、"))
                {
                    //MItem[0]["WHICH"] = "bgmfj、bgmfj_1、bgmfj_2";
                    MItem[0]["GH_BGSJ1"] = IsQualified(sItem["G_BGSJ"], sItem["W_BGSJ1"], false);
                    MItem[0]["GH_BGSJ2"] = IsQualified(sItem["G_BGSJ"], sItem["W_BGSJ2"], false);
                }
                else
                {
                    sItem["G_BGSJ"] = "----";
                    sItem["W_BGSJ1"] = "----";
                    MItem[0]["GH_BGSJ1"] = "----";
                    sItem["W_BGSJ2"] = "----";
                    MItem[0]["GH_BGSJ2"] = "----";
                }


                if (jcxm.Contains("、硬度、"))
                {
                    MItem[0]["GH_YD"] = IsQualified(sItem["G_YD"], sItem["W_YD"], false);
                }
                else
                {
                    sItem["G_YD"] = "----";
                    sItem["W_YD"] = "----";
                    MItem[0]["GH_YD"] = "----";
                }

                if (jcxm.Contains("、热老化、"))
                {
                    var sum = 0;
                    MItem[0]["GH_RLH"] = IsQualified(sItem["G_RLH"], sItem["W_RLH"], false);
                    if (MItem[0]["GH_RLH"] == "不合格")
                        sum = sum + 1;
                    if (sItem["RLH_GL1"] == "无龟裂" && sItem["RLH_GL2"] == "无龟裂")
                        sItem["RLH_GLMS"] = "无龟裂";
                    else
                    {
                        sItem["RLH_GLMS"] = "龟裂";
                        sum = sum + 1;
                    }

                    if (sItem["RLH_GL1"] == "无粉化" && sItem["RLH_GL2"] == "无粉化")
                        sItem["RLH_FHMS"] = "无粉化";
                    else
                    {
                        sItem["RLH_FHMS"] = "无粉化";
                        sum = sum + 1;
                    }

                    if (sum > 0)
                        MItem[0]["GH_RLH"] = "不合格";
                    else
                    {
                        MItem[0]["GH_RLH"] = "合格";
                    }

                }
                else
                {
                    sItem["G_RLH"] = "----";
                    sItem["W_RLH"] = "----";
                    sItem["RLH_GLMS"] = "----";
                    sItem["RLH_FHMS"] = "----";
                    MItem[0]["GH_RLH"] = "----";
                }

                if (jcxm.Contains("、相容性、"))
                {
                    sItem["G_XRX1"] = "试验试件与对比试件颜色变化一致";
                    sItem["G_XRX2"] = "试验试件、对比试件与玻璃粘结破坏面积的差值≤ 5";

                    if (sItem["XRX_YSBH"].Contains("试验试件与对比试件颜色变化一致"))
                    {
                        MItem[0]["GH_XRX_YSBH"] = "合格";
                    }
                    else
                    {
                        MItem[0]["GH_XRX_YSBH"] = "不合格";
                    }
                    if (Conversion.Val(sItem["XRX_BL"]) < 5)
                    {
                        MItem[0]["GH_XRX_BL"] = "合格";
                    }
                    else
                    {
                        MItem[0]["GH_XRX_BL"] = "不合格";
                    }
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

                if (jcxm.Contains("、与实际工程用基材的粘结性、"))
                {
                    sItem["G_YJCNJX"] = "≤ 20";

                    if (sItem["JCNJX_1"] == "" || sItem["JCNJX_1"] == "----")
                    {
                        sItem["JCNJX_1"] = "----";
                        MItem[0]["GH_JCNJX_1"] = "----";
                    }
                    else
                    {
                        if (Conversion.Val(sItem["JCNJX_1"]) <= 20)
                            MItem[0]["GH_JCNJX_1"] = "合格";
                        else
                        {
                            MItem[0]["GH_JCNJX_1"] = "不合格";
                        }
                    }

                    if (sItem["JCNJX_2"] == "#QUOTE" || sItem["JCNJX_2"] == "----")
                    {
                        sItem["JCNJX_2"] = "----";
                        MItem[0]["GH_JCNJX_2"] = "----";
                    }
                    else
                    {
                        if (Conversion.Val(sItem["JCNJX_2"]) <= 20)

                            MItem[0]["GH_JCNJX_2"] = "合格";
                        else
                            MItem[0]["GH_JCNJX_2"] = "不合格";
                    }
                    if (sItem["JCNJX_3"] == "#QUOTE" || sItem["JCNJX_3"] == "----")
                    {
                        sItem["JCNJX_3"] = "----";
                        MItem[0]["GH_JCNJX_3"] = "----";
                    }
                    else
                    {
                        if (Conversion.Val(sItem["JCNJX_3"]) <= 20)
                            MItem[0]["GH_JCNJX_3"] = "合格";
                        else
                            MItem[0]["GH_JCNJX_3"] = "不合格";

                    }
                }
                else
                {
                    sItem["JCNJX_1"] = "----";
                    sItem["JCNJX_2"] = "----";
                    sItem["JCNJX_3"] = "----";
                    sItem["G_YJCNJX"] = "----";
                    MItem[0]["GH_JCNJX_1"] = "----";
                    MItem[0]["GH_JCNJX_2"] = "----";
                    MItem[0]["GH_JCNJX_3"] = "----";
                }

                if (jcxm.Contains("、浸水后拉伸粘结强度、"))
                {
                    //MItem[0]["WHICH"] = "bgmfj、bgmfj_1、bgmfj_2"；
                    sign = true;
                    for (xd = 1; xd < 6; xd++)
                    {
                        sign = IsQualified(sItem["G_JSHLSQD"], sItem["JSLSQD_" + xd], false) == "合格" ? sign : false;
                    }

                    if (sign)
                        MItem[0]["GH_JSLSQD"] = "合格";
                    else
                        MItem[0]["GH_JSLSQD"] = "不合格";

                    sign = true;
                    for (xd = 1; xd < 6; xd++)
                    {
                        sign = IsQualified(sItem["G_JSNJPHMJ"], sItem["JSNJPHMJ" + xd], false) == "合格" ? sign : false;
                    }

                    if (sign)
                        MItem[0]["GH_JSNJPHMJ"] = "合格";
                    else
                        MItem[0]["GH_JSNJPHMJ"] = "不合格";

                }
                else
                {
                    MItem[0]["GH_JSLSQD"] = "----";
                    MItem[0]["GH_JSNJPHMJ"] = "----";
                    sItem["G_JSNJPHMJ"] = "----";
                    sItem["G_JSHLSQD"] = "----";
                    for (xd = 1; xd < 6; xd++)
                    {
                        sItem["JSLSQD_" + xd] = "----";
                        sItem["JSNJPHMJ" + xd] = "----";
                    }
                }

                if (jcxm.Contains("、定伸粘结强度、"))
                {
                    //MItem[0]["WHICH"] = "bgmfj、bgmfj_1、bgmfj_2"；
                    sign = true;
                    for (xd = 1; xd < 6; xd++)
                    {
                        sign = IsQualified(sItem["G_GZHLSQD"], sItem["GZHSQD_" + xd], false) == "合格" ? sign : false;
                    }

                    if (sign)
                        MItem[0]["GH_GZLSQD"] = "合格";
                    else
                        MItem[0]["GH_GZLSQD"] = "不合格";

                    sign = true;
                    for (xd = 1; xd < 6; xd++)
                    {
                        sign = IsQualified(sItem["G_GZNJPHMJ"], sItem["GZNJPHMJ" + xd], false) == "合格" ? sign : false;
                    }

                    if (sign)
                        MItem[0]["GH_GZNJPHMJ"] = "合格";
                    else
                        MItem[0]["GH_GZNJPHMJ"] = "不合格";
                }
                else
                {
                    MItem[0]["GH_GZLSQD"] = "----";
                    MItem[0]["GH_GZNJPHMJ"] = "----";
                    sItem["G_GZHLSQD"] = "----";
                    sItem["G_GZNJPHMJ"] = "----";
                    for (xd = 1; xd < 6; xd++)
                    {
                        sItem["GZHSQD_" + xd] = "----";
                        sItem["GZNJPHMJ" + xd] = "----";
                    }
                }
                if (jcxm.Contains("、拉伸粘结性、"))
                {

               
                 

                    
                   



                   
                 
                }
                else
                {
                 
                 
                }
                if (
                MItem[0]["GH_23LSQD"] == "不合格" ||
                MItem[0]["GH_BGSJ1"] == "不合格" ||
                MItem[0]["GH_BGSJ2"] == "不合格" ||
                MItem[0]["GH_NJPHMJ"] == "不合格" ||
                MItem[0]["GH_GZLSQD"] == "不合格" ||
                MItem[0]["GH_GZNJPHMJ"] == "不合格" ||
                MItem[0]["GH_JSLSQD"] == "不合格" ||
                MItem[0]["GH_JCX"] == "不合格" ||
                MItem[0]["GH_JSNJPHMJ"] == "不合格" ||
                MItem[0]["GH_RLH"] == "不合格" ||
                MItem[0]["GH_SYQ"] == "不合格" ||
                MItem[0]["GH_WG"] == "不合格" ||
                MItem[0]["GH_XCDCZ"] == "不合格" ||
                MItem[0]["GH_XCDSP"] == "不合格" ||
                MItem[0]["GH_YD"] == "不合格" ||
                MItem[0]["GH_ZDSCL"] == "不合格" ||
                MItem[0]["GH_XRX_YSBH"] == "不合格" ||
                MItem[0]["GH_XRX_BL"] == "不合格" ||
                MItem[0]["GH_JCNJX_1"] == "不合格" ||
                MItem[0]["GH_JCNJX_2"] == "不合格" ||
                MItem[0]["GH_JCNJX_3"] == "不合格")
                {
                    mbhggs += 1;
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

                    if (MItem[0]["GH_23LSQD"] == "合格" ||
                MItem[0]["GH_BGSJ1"] == "合格" ||
                MItem[0]["GH_BGSJ2"] == "合格" ||
                MItem[0]["GH_NJPHMJ"] == "合格" ||
                MItem[0]["GH_GZLSQD"] == "合格" ||
                MItem[0]["GH_GZNJPHMJ"] == "合格" ||
                MItem[0]["GH_JSLSQD"] == "合格" ||
                MItem[0]["GH_JCX"] == "合格" ||
                MItem[0]["GH_JSNJPHMJ"] == "合格" ||
                MItem[0]["GH_RLH"] == "合格" ||
                MItem[0]["GH_SYQ"] == "合格" ||
                MItem[0]["GH_WG"] == "合格" ||
                MItem[0]["GH_XCDCZ"] == "合格" ||
                MItem[0]["GH_XCDSP"] == "合格" ||
                MItem[0]["GH_YD"] == "合格" ||
                MItem[0]["GH_ZDSCL"] == "合格" ||
                MItem[0]["GH_XRX_YSBH"] == "合格" ||
                MItem[0]["GH_XRX_BL"] == "合格" ||
                MItem[0]["GH_JCNJX_1"] == "合格" ||
                MItem[0]["GH_JCNJX_2"] == "合格" ||
                MItem[0]["GH_JCNJX_3"] == "合格")
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
