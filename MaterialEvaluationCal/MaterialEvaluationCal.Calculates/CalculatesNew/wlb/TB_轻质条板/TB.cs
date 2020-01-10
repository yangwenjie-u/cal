using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class TB : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_TB_DJ"];

            //var jcxmItems = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            var S_TBS = data["S_TB"];
            if (!data.ContainsKey("M_TB"))
            {
                data["M_TB"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_TB"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            bool itemHG = true;//判断单组是否合格
            int mbHggs = 0;//合格数量
            double md1, md2, md, pjmd, sum = 0;
            bool flag, sign = false;

            foreach (var sItem in S_TBS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["YPMC"] == sItem["YPMC"] && u["HD"] == sItem["DLDJ"]);
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    continue;
                }
                else
                {
                    MItem[0]["G_PHHZ"] = extraFieldsDj["G_PHHZ"];
                    MItem[0]["G_KYQD"] = extraFieldsDj["G_KYQD"];
                    MItem[0]["G_MMD"] = extraFieldsDj["G_MMD"];
                    MItem[0]["G_HSL"] = extraFieldsDj["G_HSL"];
                    MItem[0]["G_CRXS"] = extraFieldsDj["G_CRXS"];
                    string mm = extraFieldsDj["G_CRXS"];
                    MItem[0]["G_RHXS"] = extraFieldsDj["G_RHXS"];
                    MItem[0]["G_GZSSZ"] = extraFieldsDj["G_GZSSZ"];
                }
                #region vb跳转代码
                if (string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    flag = sItem["TBLB"] == "建筑隔墙用轻质条板" ? true : false;
                    sItem["XS_KW"] = flag ? "抗弯破坏荷载" : "抗弯承载";

                    #region 抗弯破坏荷载 || 抗弯承载
                    if (jcxm == "抗弯破坏荷载" || jcxm == "抗弯承载")
                    {
                        sign = true;
                        sign = IsNumeric(MItem[0]["W_PHHZ"]) ? sign : false;
                        if (sign)
                        {
                            MItem[0]["GH_PHHZ"] = IsQualified(MItem[0]["G_PHHZ"], MItem[0]["W_PHHZ"], false);
                        }
                        if (MItem[0]["GH_PHHZ"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sign = false;
                    }
                    if (!sign)
                    {
                        MItem[0]["G_PHHZ"] = "----";
                        MItem[0]["W_PHHZ"] = "----";
                        MItem[0]["GH_PHHZ"] = "----";
                    }
                    #endregion

                    #region 软化系数
                    if (jcxm == "软化系数")
                    {
                        sign = true;
                        if (sItem["YPMC"] == "石膏条板")
                        {
                            MItem[0]["G_RHXS"] = "≥0.6";
                        }
                        MItem[0]["GH_RHXS"] = IsQualified(MItem[0]["G_RHXS"], MItem[0]["W_RHXS"], false);
                        if (MItem[0]["GH_RHXS"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sign = false;
                    }
                    if (!sign)
                    {
                        MItem[0]["W_RHXS"] = "----";
                        MItem[0]["G_RHXS"] = "----";
                        MItem[0]["GH_RHXS"] = "----";
                    }
                    #endregion

                    #region 面密度
                    if (jcxm == "面密度")
                    {
                        sign = true;
                        MItem[0]["GH_MMD"] = IsQualified(MItem[0]["G_MMD"], MItem[0]["W_MMD"], false);
                        if (MItem[0]["GH_MMD"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        MItem[0]["W_MMD"] = "----";
                        MItem[0]["GH_MMD"] = "----";
                       // MItem[0]["G_MDD"] = "----";
                    }
                    #endregion

                    #region 含水率
                    if (jcxm == "含水率")
                    {
                        MItem[0]["GH_HSL"] = IsQualified(MItem[0]["G_HSL"], MItem[0]["W_HSL"], false);
                        if (MItem[0]["GH_HSL"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        MItem[0]["W_HSL"] = "----";
                        MItem[0]["G_HSL"] = "----";
                        MItem[0]["W_HSL"] = "----";
                    }
                    #endregion

                    #region 传热系数
                    if (jcxm == "传热系数")
                    {
                        sign = true;
                        MItem[0]["W_CRXS"] = sItem["CRXS"].Trim();
                        MItem[0]["GH_CRXS"] = IsQualified(MItem[0]["G_CRXS"], MItem[0]["W_CRXS"], false);
                        if (MItem[0]["GH_CRXS"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sign = false;
                    }
                    if (!sign)
                    {
                        MItem[0]["W_CRXS"] = "----";
                        MItem[0]["G_CRXS"] = "----";
                        MItem[0]["GH_CRXS"] = "----";
                    }
                    #endregion

                    #region 干燥收缩值
                    if (jcxm == "干燥收缩值")
                    {
                        sign = true;
                        MItem[0]["GH_GZSSZ"] = IsQualified(MItem[0]["G_GZSSZ"], MItem[0]["W_GZSSZ"], false);
                        if (MItem[0]["GH_GZSSZ"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sign = false;
                    }
                    if (!sign)
                    {
                        MItem[0]["W_GZSSZ"] = "----";
                        MItem[0]["G_GZSSZ"] = "----";
                        MItem[0]["GH_GZSSZ"] = "----";
                    }
                    #endregion
                }
                #endregion

                flag = sItem["TBLB"] == "建筑隔墙用轻质条板" ? true : false;
                sItem["XS_KW"] = flag ? "抗弯破坏荷载" : "抗弯承载";

                #region 抗弯破坏荷载 || 抗弯承载
                if (jcxm.Contains("、抗弯破坏荷载、") || jcxm.Contains("、抗弯承载、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["PHHZ"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["W_PHHZ"] = sItem["PHHZ"].Trim();
                        MItem[0]["GH_PHHZ"] = IsQualified(MItem[0]["G_PHHZ"], MItem[0]["W_PHHZ"], false);
                        //mbHggs = MItem[0]["GH_PHHZ"] == "不合格" ? mbHggs++ : mbHggs;
                        if (MItem[0]["GH_PHHZ"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }

                }
                else
                {
                    sign = false;
                }
                if (!sign)
                {
                    MItem[0]["G_PHHZ"] = "----";
                    MItem[0]["W_PHHZ"] = "----";
                    MItem[0]["GH_PHHZ"] = "----";
                }
                #endregion

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {
                    sign = true;
                    for (int xd = 1; xd < 4; xd++)
                    {
                        sign = IsNumeric(sItem["KY_CD" + xd]) ? sign : false;
                        sign = IsNumeric(sItem["KY_KD" + xd]) ? sign : false;
                        sign = IsNumeric(sItem["KY_PHHZ" + xd]) ? sign : false;
                        if (!sign)
                        {
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (int xd = 1; xd < 4; xd++)
                        {
                            md = GetSafeDouble(sItem["KY_PHHZ" + xd]);
                            md1 = GetSafeDouble(sItem["KY_CD" + xd]);
                            md2 = GetSafeDouble(sItem["KY_PHHZ" + xd]);
                            md = Math.Round(md / (md1 * md2), 1);
                            sum = sum + md;
                        }
                        pjmd = Math.Round(sum / 3, 1);
                        MItem[0]["W_KYQD"] = pjmd.ToString("0.0");
                        MItem[0]["GH_KYQD"] = IsQualified(MItem[0]["G_KYQD"], MItem[0]["W_KYQD"], false);
                        //mbHggs = MItem[0]["GH_KYQD"] == "不合格" ? mbHggs++ : mbHggs;
                        if (MItem[0]["GH_KYQD"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                }
                else
                {
                    sign = false;
                }
                if (!sign)
                {
                    MItem[0]["W_KYQD"] = "----";
                    MItem[0]["G_KYQD"] = "----";
                    MItem[0]["GH_KYQD"] = "----";
                }
                #endregion

                #region 面密度
                if (jcxm.Contains("、面密度、"))
                {
                    sign = true;
                    for (int xd = 1; xd < 4; xd++)
                    {
                        sign = IsNumeric(sItem["MD_CD" + xd]) ? sign : false;
                        sign = IsNumeric(sItem["MD_KD" + xd]) ? sign : false;
                        sign = IsNumeric(sItem["MD_ZL" + xd]) ? sign : false;
                        if (!sign)
                        {
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (int xd = 1; xd < 4; xd++)
                        {
                            md = GetSafeDouble(sItem["MD_ZL" + xd].Trim());
                            md1 = GetSafeDouble(sItem["MD_CD" + xd].Trim());
                            md2 = GetSafeDouble(sItem["MD_KD" + xd].Trim());
                            md = Math.Round(md / (md1 * md2));
                            md = Math.Round(md / 0.5, 0);
                            md = md * 0.5;
                            sum = sum + md;
                        }
                        pjmd = Math.Round(sum / 3, 1);
                        pjmd = Math.Round(pjmd / 0.5, 0);
                        pjmd = pjmd * 0.5;

                        MItem[0]["W_MMD"] = flag ? pjmd.ToString("0.0") : pjmd.ToString("0");
                        MItem[0]["GH_MMD"] = IsQualified(MItem[0]["G_MMD"], MItem[0]["W_MMD"]);
                        if (MItem[0]["GH_MMD"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                }
                else
                {
                    MItem[0]["W_MMD"] = "----";
                    MItem[0]["GH_MMD"] = "----";
                    //MItem[0]["G_MDD"] = "----";
                }
                #endregion

                #region 含水率
                if (jcxm.Contains("、含水率、"))
                {
                    sign = true;
                    for (int xd = 1; xd < 4; xd++)
                    {
                        sign = IsNumeric(sItem["HS_QYZL"+ xd]) ? sign : false;
                        sign = IsNumeric(sItem["HS_JGZL"+ xd]) ? sign : false;
                        if (!sign)
                        {
                            break;
                        }
                    }
                    if (sign)
                    {
                        sum = 0;
                        pjmd = Math.Round(sum / 3, 1);
                        MItem[0]["W_HSL"] = pjmd.ToString("0.0");
                        MItem[0]["GH_HSL"] = IsQualified(MItem[0]["G_HSL"], MItem[0]["W_HSL"], false);
                        if (MItem[0]["GH_HSL"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                }
                else
                {
                    MItem[0]["W_HSL"] = "----";
                    MItem[0]["G_HSL"] = "----";
                    MItem[0]["W_HSL"] = "----";
                }
                #endregion

                #region 传热系数
                if (jcxm.Contains("、传热系数、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["CRXS"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["W_CRXS"] = sItem["CRXS"].Trim();
                        MItem[0]["GH_CRXS"] = IsQualified(MItem[0]["G_CRXS"], MItem[0]["W_CRXS"], false);
                        if (MItem[0]["GH_CRXS"] == "不合格")
                        {
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                }
                else
                {
                    sign = false;
                }
                if (!sign)
                {
                    MItem[0]["W_CRXS"] = "----";
                    MItem[0]["G_CRXS"] = "----";
                    MItem[0]["GH_CRXS"] = "----";
                }
                #endregion

                #region //单组判断
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }
                #endregion

            }
            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组样品所检项目符合上述标准要求。";
            }
            else
            {
                if (mbHggs > 0)
                {
                    jsbeizhu = "该组样品所检项目部分符合上述标准要求。";
                }
                else
                {
                    jsbeizhu = "该组样品所检项目不符合上述标准要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
