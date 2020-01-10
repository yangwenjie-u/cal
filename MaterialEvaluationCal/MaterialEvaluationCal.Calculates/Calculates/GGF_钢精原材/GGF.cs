using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GGF : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            var extraDJ = dataExtra["BZ_GGF_DJ"];

            //var jcxmItems = retData.Select(u => u.Key).ToArray();
            var jsbeizhu = "";
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var S_GGFS = data["S_GGF"];
            if (!data.ContainsKey("M_GGF"))
            {
                data["M_GGF"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GGF"];

            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            bool itemHG = true;//判断单组是否合格

            List<double> sggwjArray = new List<double>();
            List<double> sggbhArray = new List<double>();

            List<string> mtmpArray = new List<string>();
            List<double> mkyqdArray = new List<double>();

            string mJSFF, mlongStr = "";
            double mMj, mScl = 0;
            double mklgs = 0;
            double mqfgs = 0;
            double msclgs = 0;
            double mlwgs = 0;

            foreach (var sItem in S_GGFS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim() + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDJ = extraDJ.FirstOrDefault(u => u["MC"] == sItem["GGXH"]);
                if (null == extraFieldsDJ)
                {
                    sItem["GGGWJ"] = extraFieldsDJ["GGWJ"];
                    sItem["GWJPC"] = extraFieldsDJ["WJPC"];
                    sItem["GGGBH"] = extraFieldsDJ["GGBH"];
                    sItem["SJGGXH"] = extraFieldsDJ["GGXH"];
                    sItem["GBHPC"] = extraFieldsDJ["BHPC"];
                    sItem["GXSSD"] = extraFieldsDJ["XSSD"];
                    sItem["G_QFQD"] = extraFieldsDJ["G_QFQD"];
                    sItem["G_KLQD"] = extraFieldsDJ["G_KLQD"];
                    sItem["G_SCL"] = extraFieldsDJ["G_SCL"];
                    sItem["G_LW"] = extraFieldsDJ["G_LW"];
                    mJSFF = (extraFieldsDJ["JSFF"] == null ? "" : extraFieldsDJ["JSFF"]).Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    MItem[0]["BGBH"] = "";
                    sItem["JCJG"] = "依据不详";
                    jsbeizhu = MItem[0]["JSBEIZHU"] + "单组流水号:" + "试件尺寸为空";
                    mAllHg = false;
                    continue;
                }

                #region  外径
                if (jcxm.Contains("、外径、"))
                {
                    double mwj = 0;
                    for (int i = 1; i < 5; i++)
                    {
                        mwj = GetSafeDouble(sItem["GGWJ" + i + "_1"]) + GetSafeDouble(sItem["GGWJ" + i + "_2"]) / 2;
                        sItem["GGWJ" + i] = Math.Round(mwj, 2).ToString();
                        sItem["GGWJPC" + i] = (GetSafeDouble(sItem["GGWJ" + i]) - GetSafeDouble(extraFieldsDJ["GGWJ"])).ToString();
                        //因后续要做判断，将数据放入集合
                        sggwjArray.Add(GetSafeDouble(sItem["GGWJ" + i]));
                    }
                    if (GetSafeDouble(extraFieldsDJ["LSGS"]) == 2)
                    {
                        if (sggwjArray[0] >= GetSafeDouble(extraFieldsDJ["GGWJ"]) - GetSafeDouble(extraFieldsDJ["WJPC"]) && sggwjArray[1] >= GetSafeDouble(extraFieldsDJ["GGWJ"]) - GetSafeDouble(extraFieldsDJ["WJPC"]))
                        {
                            sItem["WJPD"] = "合格";
                        }
                        else
                        {
                            sItem["WJPD"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                    else
                    {
                        if (sggwjArray[0] >= GetSafeDouble(extraFieldsDJ["GGWJ"]) - GetSafeDouble(extraFieldsDJ["WJPC"]) && sggwjArray[1] >= GetSafeDouble(extraFieldsDJ["GGWJ"]) - GetSafeDouble(extraFieldsDJ["WJPC"]) && sggwjArray[2] >= GetSafeDouble(extraFieldsDJ["GGWJ"]) - GetSafeDouble(extraFieldsDJ["WJPC"]) && sggwjArray[3] >= GetSafeDouble(extraFieldsDJ["GGWJ"]) - GetSafeDouble(extraFieldsDJ["WJPC"]))
                        {
                            sItem["WJPD"] = "合格";
                        }
                        else
                        {
                            sItem["WJPD"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                }
                else
                {
                    sItem["WJPD"] = "----";
                }
                #endregion

                #region  壁厚
                if (jcxm.Contains("、壁厚、"))
                {
                    double mbh = 0;
                    for (int i = 1; i < 5; i++)
                    {
                        mbh = GetSafeDouble(sItem["GGbh" + i + "_1"]) + GetSafeDouble(sItem["GGbh" + i + "_2"]) + GetSafeDouble(sItem["GGbh" + i + "_3"]) + GetSafeDouble(sItem["GGbh" + i + "_4"]) / 4;
                        sItem["GGBH" + i] = Math.Round(mbh, 2).ToString();
                        sItem["GGBHPC" + i] = (GetSafeDouble(sItem["GGBH" + i]) - GetSafeDouble(extraFieldsDJ["GGBH"])).ToString();
                        sggbhArray.Add(GetSafeDouble(sItem["GGBH" + i]));
                    }
                    if (GetSafeDouble(extraFieldsDJ["LSGS"]) == 4 && sggbhArray.Count == 4)
                    {
                        if (GetSafeDouble(sItem["GGBH1"]) >= GetSafeDouble(extraFieldsDJ["GGBH"]) - GetSafeDouble(extraFieldsDJ["BHPC"])
                            && sggbhArray[1] >= GetSafeDouble(extraFieldsDJ["GGBH"]) - GetSafeDouble(extraFieldsDJ["BHPC"])
                            && sggbhArray[2] >= GetSafeDouble(extraFieldsDJ["GGBH"]) - GetSafeDouble(extraFieldsDJ["BHPC"])
                            && sggbhArray[3] >= GetSafeDouble(extraFieldsDJ["GGBH"]) - GetSafeDouble(extraFieldsDJ["BHPC"]))
                        {
                            sItem["BHPD"] = "合格";
                        }
                        else
                        {
                            sItem["BHPD"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                    else
                    {
                        if (sggbhArray[0] >= GetSafeDouble(extraFieldsDJ["GGBH"]) - GetSafeDouble(extraFieldsDJ["BHPC"])
                            && sggbhArray[1] >= GetSafeDouble(extraFieldsDJ["GGBH"]) - GetSafeDouble(extraFieldsDJ["BHPC"])
                            && sggbhArray[2] >= GetSafeDouble(extraFieldsDJ["GGBH"]) - GetSafeDouble(extraFieldsDJ["BHPC"])
                            && sggbhArray[3] >= GetSafeDouble(extraFieldsDJ["GGBH"]) - GetSafeDouble(extraFieldsDJ["BHPC"]))
                        {
                            sItem["BHPD"] = "合格";
                        }
                        else
                        {
                            sItem["BHPD"] = "不合格";
                            mAllHg = false;
                            itemHG = false;
                        }
                    }
                }
                else
                {
                    sItem["BHPD"] = "----";
                }
                #endregion

                #region  锈蚀深度
                if (jcxm.Contains("、锈蚀深度、"))
                {
                    if (string.Equals(sItem["YPXJ"], "新"))
                    {
                        sItem["XSSD"] = "----";
                        sItem["XSPD"] = "----";
                    }
                    else
                    {
                        mlongStr = sItem["XSSD1"] + "," + sItem["XSSD2"] + "," + sItem["XSSD3"] + "," + sItem["XSSD4"] + "," + sItem["XSSD5"] + "," + sItem["XSSD6"];
                        mtmpArray = mlongStr.Split(',').ToList<string>();

                    }
                    for (int i = 1; i < 6; i++)
                    {
                        mkyqdArray[i] = GetSafeDouble(mtmpArray[i]);
                    }
                    sItem["XSSD"] = mkyqdArray[5].ToString();
                    if (GetSafeDouble(sItem["XSSD"]) > GetSafeDouble(extraFieldsDJ["XSSD"]))
                    {
                        sItem["XSPD"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        sItem["XSPD"] = "合格";
                    }
                }
                else
                {
                    sItem["XSPD"] = "----";
                }
                #endregion

                #region 拉伸  &&  sItem["BHPD"] == 不合格
                if (jcxm.Contains("、拉伸、") || string.Equals(sItem["BHPD"], "不合格"))
                {
                    sItem["WGPD"] = "不合格";
                    mAllHg = false;
                    itemHG = false;
                }
                else
                {
                    if (string.Equals(sItem["WJPD"], "----") && string.Equals(sItem["BHPD"], "----") && string.Equals(sItem["XSPD"], "----"))
                    {
                        sItem["WGPD"] = "合格";
                    }
                }

                if (jcxm.Contains("、拉伸、") && string.Equals(sItem["WGPD"], "合格"))
                {
                    for (int i = 1; i < 5; i++)
                    {
                        mMj = Math.Round(((GetSafeDouble(sItem["GGWJ" + i]) * 3.14159) / 2)
                            * GetSafeDouble(sItem["GGWJ" + i]) / 2 - 3.14159 * GetSafeDouble(sItem["GGWJ" + i]) / 2 
                            - GetSafeDouble(sItem["GGBH" + i]) * (GetSafeDouble(sItem["GGWJ" + i]) / 2 
                            - GetSafeDouble(sItem["GGBH" + i])), 2);
                        sItem["JMJ" + i] = mMj.ToString();
                        sItem["YSBJ"] = "130";
                        if ((GetSafeDouble(sItem["JMJ" + i]) != 0))
                        {
                            sItem["QFQD" + i] = (GetSafeDouble(sItem["QFHZ" + i]) / GetSafeDouble(sItem["JMJ" + i]) * 1000).ToString();
                            sItem["KLQD" + i] = (GetSafeDouble(sItem["KLHZ" + i]) / GetSafeDouble(sItem["JMJ" + i]) * 1000).ToString();
                            mScl = 100 * (GetSafeDouble(sItem["SCZ" + i]) - GetSafeDouble(sItem["YSBJ"])) / GetSafeDouble(sItem["YSBJ"]);
                            if (mScl >= 10)
                            {
                                sItem["SCL" + i] = Math.Round(mScl, 0).ToString();
                            }
                            else
                            {
                                sItem["SCL" + i] = Math.Round(mScl, 1).ToString();
                            }
                        }
                        if (GetSafeDouble(sItem["QFQD" + i]) >= GetSafeDouble(sItem["G_QFQD"]) && GetSafeDouble(sItem["QFQD" + i]) != 0 && GetSafeDouble(sItem["G_QFQD"]) != 0)
                        {
                            mqfgs++;
                        }
                        if (GetSafeDouble(sItem["KLQD" + i]) >= GetSafeDouble(sItem["G_KLQD"]) && GetSafeDouble(sItem["KLQD" + i]) != 0 && GetSafeDouble(sItem["G_KLQD"]) != 0)
                        {
                            mklgs++;
                        }
                        if (GetSafeDouble(sItem["scl" + i]) >= GetSafeDouble(sItem["G_SCL"]) && GetSafeDouble(sItem["scl" + i]) != 0 && GetSafeDouble(sItem["G_SCL"]) != 0)
                        {
                            msclgs++;
                        }
                    }
                    if (mqfgs > GetSafeDouble(extraFieldsDJ["QFHGGS"]))
                    {
                        sItem["HG_QFQD"] = "合格";
                    }
                    else
                    {
                        sItem["HG_QFQD"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                    if (GetSafeDouble(extraFieldsDJ["G_QFQD"]) == 0 || GetSafeDouble(sItem["QFQD1"]) == 0)
                    {
                        sItem["HG_QFQD"] = "----";
                    }
                    if (mklgs > GetSafeDouble(extraFieldsDJ["KLHGGS"]))
                    {
                        sItem["HG_KLQD"] = "合格";
                    }
                    else
                    {
                        sItem["HG_KLQD"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                    if (GetSafeDouble(extraFieldsDJ["G_KLQD"]) == 0 || GetSafeDouble(sItem["KLQD1"]) == 0)
                    {
                        sItem["HG_KLQD"] = "----";
                    }
                    if (msclgs > GetSafeDouble(extraFieldsDJ["SCHGGS"]))
                    {
                        sItem["HG_SCL"] = "合格";
                    }
                    else
                    {
                        sItem["HG_SCL"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                    if (GetSafeDouble(extraFieldsDJ["G_SCL"]) == 0 || GetSafeDouble(sItem["SCL1"]) == 0)
                    {
                        sItem["HG_SCL"] = "----";
                    }

                }
                else
                {
                    sItem["HG_QFQD"] = "----";
                    sItem["HG_KLQD"] = "----";
                    sItem["HG_SCL"] = "----";
                }
                #endregion

                #region   弯曲
                if (jcxm.Contains("、弯曲、"))
                {
                    for (int i = 1; i < 5; i++)
                    {
                        if (GetSafeDouble(sItem["LW" + i]) == 1 || GetSafeDouble(sItem["LW" + i]) < 0)
                        {
                            mlwgs++;
                        }
                    }
                    if (mlwgs > GetSafeDouble(extraFieldsDJ["LWHGGS"]))
                    {
                        sItem["HG_LW"] = "合格";
                    }
                    else
                    {
                        sItem["HG_LW"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                    if (GetSafeDouble(extraFieldsDJ["G_LW"]) == 0)
                    {
                        sItem["LW1"] = "-1";
                        sItem["LW2"] = "-1";
                        sItem["LW3"] = "-1";
                        sItem["LW4"] = "-1";
                        sItem["HG_LW"] = "----";
                    }
                }
                else
                {
                    sItem["LW1"] = "-1";
                    sItem["LW2"] = "-1";
                    sItem["LW3"] = "-1";
                    sItem["LW4"] = "-1";
                    sItem["HG_LW"] = "----";
                }
                #endregion

                //单组判定  
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
            }

            #region 添加最终报告
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

