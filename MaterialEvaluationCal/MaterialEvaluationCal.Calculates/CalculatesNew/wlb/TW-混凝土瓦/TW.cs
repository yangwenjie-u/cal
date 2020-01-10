using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    /* 混凝土瓦 */
    public class TW : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_TW_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_TWS = data["S_TW"];
            if (!data.ContainsKey("M_TW"))
            {
                data["M_TW"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_TW"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            int mHggs = 0;//统计合格数量
            bool sign = true;
            string mSjdj = "";
            double sum = 0;
            foreach (var sItem in S_TWS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                mSjdj = sItem["TWLB"];//等级名称
                if (string.IsNullOrEmpty(mSjdj))
                {
                    mSjdj = "";
                }
                else
                {
                    mSjdj = mSjdj.Trim();
                }
                //计算龄期
                //sItem["LQ"] = "0";
                sItem["LQ"] = (DateTime.Parse(sItem["SYRQ"]) - DateTime.Parse(sItem["ZZRQ"])).Days.ToString();
                //if ("1900-1-1" == MItem[0]["SYRQ2"])
                //{
                //    MItem[0]["SYRQ2"] = MItem[0]["YPJSRQ_SY"];
                //}


                #region 承载力
                if (jcxm.Contains("、承载力、"))
                {
                    string mKd, mGd = "";
                    sItem["GD"] = Math.Round((Conversion.Val(sItem["GD1"]) + Conversion.Val(sItem["GD2"])) / 2, 0).ToString("0") + "mm";
                    string d = sItem["GD"];
                    sItem["KD"] = Math.Round((Conversion.Val(sItem["KD1"]) + Conversion.Val(sItem["KD2"])) / 2, 0).ToString("0") + "mm";
                    string b1 = sItem["KD"];
                    if (d == null)
                    {
                        d = "0";
                    }
                    if (b1 == null)
                    {
                        mKd = "0";
                    }
                    if (mSjdj.Contains("混凝土波形屋面"))
                    {
                        if (Conversion.Val(d) > 20)
                        {
                            mGd = "d＞20";
                        }
                        else
                        {
                            mGd = "d<=20";
                        }
                    }
                    else
                    {
                        mGd = "----";
                    }
                    if (Conversion.Val(b1) >= 300)
                    {
                        mKd = "b1≥300";
                    }
                    else
                    {
                        if (Conversion.Val(b1) <= 200)
                        {
                            mKd = "b1≤200";
                        }
                        else
                        {
                            mKd = "200＜b1＜300";
                        }
                    }
                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"].Trim() == mSjdj.Trim() && u["GD"].Trim() == mGd.Trim() && u["KD"].Trim() == mKd.Trim());
                    if (null != extraFieldsDj)
                    {
                        MItem[0]["G_KYHZ"] = extraFieldsDj["CZLYQ"].Trim();
                        if ("6b1" == MItem[0]["G_KYHZ"])
                        {
                            MItem[0]["G_KYHZ"] = (Math.Round(6 * Conversion.Val(b1) / 10, 0) * 10).ToString();
                        }
                        if ("3b1+300" == MItem[0]["G_KYHZ"])
                        {
                            MItem[0]["G_KYHZ"] = (Math.Round((3 * Conversion.Val(b1) + 300) / 10, 0) * 10).ToString();
                        }
                        if ("2b1+400" == MItem[0]["G_KYHZ"])
                        {
                            MItem[0]["G_KYHZ"] = (Math.Round((2 * Conversion.Val(b1) + 400) / 10, 0) * 10).ToString();
                        }
                    }
                    double md, pjmd = 0;
                    sum = 0;
                    for (int i = 1; i < 8; i++)
                    {
                        md = double.Parse(sItem["KYHZ" + i].Trim());
                        if (md <= 100)
                        {
                            md = 1000 * md;
                        }
                        md = Math.Round(md, 0);
                        sum = sum + md;
                        MItem[0]["W_CZL" + i] = md.ToString("0");
                    }
                    md = sum / 7;
                    pjmd = Math.Round(md / 10, 0) * 10;
                    sItem["KYPJ"] = pjmd.ToString("0");

                    sum = 0;
                    for (int i = 1; i < 8; i++)
                    {
                        md = double.Parse(MItem[0]["W_CZL" + i].Trim());
                        sum = sum + Math.Pow((md - pjmd), 2);
                    }

                    md = Math.Sqrt(sum / (7 - 1));
                    md = Math.Round(md / 10, 0) * 10;
                    sItem["CZLBZC"] = md.ToString("0");
                    md = pjmd - 1.64 * md;
                    md = Math.Round(md / 10, 0) * 10;
                    sItem["FOK"] = md.ToString("0");
                    //判断
                    if (Conversion.Val(sItem["FOK"]) >= Conversion.Val(MItem[0]["G_KYHZ"]))
                    {
                        sItem["CZLPD"] = "合格";
                        mHggs++;
                    }
                    else
                    {
                        sItem["CZLPD"] = "不合格";
                        itemHG = false;
                        mAllHg = false;
                    }
                    MItem[0]["G_KYHZ"] = "≥" + MItem[0]["G_KYHZ"];
                }
                else
                {
                    sItem["CZLPD"] = "----";
                    MItem[0]["G_KYHZ"] = "----";
                    for (int i = 1; i < 8; i++)
                    {
                        MItem[0]["W_CZL" + i] = "----";
                    }
                    sItem["FOK"] = "----";
                    sItem["KYPJ"] = "----";
                }
                #endregion

                #region 吸水率
                if (jcxm.Contains("、吸水率、"))
                {
                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"].Trim() == mSjdj.Trim());
                    if (null != extraFieldsDj)
                    {
                        MItem[0]["G_XSL"] = extraFieldsDj["XSLYQ"].Trim();
                    }
                    sum = 0;
                    double md1, md2, md, pjmd = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        md1 = Conversion.Val(sItem["M1" + i] );
                        md2 = Conversion.Val(sItem["M0" + i] );
                        md = Math.Round(100 * (md1 - md2) / md2, 2);
                        sItem["XSL" + i] = md.ToString("0.00");
                        sum = sum + md;
                        MItem[0]["W_XSL" + i] = sItem["XSL" + i].Trim();
                    }
                    pjmd = Math.Round(sum / 5, 1);
                    sItem["XSL"] = pjmd.ToString("0.0");
                    if (Conversion.Val(sItem["XSL"]) <= Conversion.Val(MItem[0]["G_XSL"]))
                    {
                        sItem["XSLPD"] = "合格";
                        mHggs++;
                    }
                    else
                    {
                        sItem["XSLPD"] = "不合格";
                        itemHG = false;
                        mAllHg = false;
                    }
                    MItem[0]["G_XSL"] = "≤" + MItem[0]["G_XSL"];
                }
                else
                {
                    sItem["XSLPD"] = "----";
                    MItem[0]["G_XSL"] = "----";
                    for (int i = 1; i < 6; i++)
                    {
                        MItem[0]["W_XSL" + i] = "----";
                    }
                    sItem["XSL"] = "----";
                }
                #endregion

                #region 抗渗性能
                if (jcxm.Contains("、抗渗性能、"))
                {
                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"].Trim() == mSjdj.Trim());
                    if (null != extraFieldsDj)
                    {
                        MItem[0]["G_KS"] = extraFieldsDj["KSYQ"].Trim();
                    }
                    if (0 < Conversion.Val(sItem["KSGS"]))
                    {
                        sItem["KSPD"] = "不合格";
                        sItem["KSSM"] = "瓦背面" + sItem["KSGS"] + "片滴水";
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        sItem["KSPD"] = "合格";
                        sItem["KSSM"] = "瓦背面无水滴";
                        mHggs++;
                    }
                }
                else
                {
                    sItem["KSPD"] = "----";
                    sItem["KSSM"] = "----";
                    MItem[0]["G_KS"] = "----";
                }
                #endregion

                #region 耐热性能
                if (jcxm.Contains("、耐热性能、"))
                {
                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"].Trim() == mSjdj.Trim());
                    if (null != extraFieldsDj)
                    {
                        MItem[0]["G_NRXN"] = extraFieldsDj["NRXNYQ"].Trim();
                    }
                    if (0 < Conversion.Val(sItem["NRSHGS"]))
                    {
                        sItem["NRXNPD"] = "不合格";
                        sItem["NRSHSM"] = "瓦表面涂层" + sItem["NRSHGS"] + "片烫坏";
                        itemHG = false;
                        mAllHg = false;
                    }
                    else
                    {
                        sItem["NRXNPD"] = "合格";
                        sItem["NRSHSM"] = "瓦表面涂层完好";
                        mHggs++;
                    }
                }
                else
                {
                    sItem["NRXNPD"] = "----";
                    sItem["NRSHSM"] = "----";
                    MItem[0]["G_NRXN"] = "----";
                }
                #endregion

                #region 抗冻性能
                if (jcxm.Contains("、抗冻性能、"))
                {
                    sItem["GD"] = Math.Round((Conversion.Val(sItem["GD1"]) + Conversion.Val(sItem["GD2"])) / 2, 0).ToString("0") + "mm";
                    string d = sItem["GD"];
                    sItem["KD"] = Math.Round((Conversion.Val(sItem["KD1"]) + Conversion.Val(sItem["KD2"])) / 2, 0).ToString("0") + "mm";
                    string b1 = sItem["KD"];
                    string mKd, mGd = "";
                    if (d == null)
                    {
                        d = "0";
                    }
                    if (b1 == null)
                    {
                        mKd = "0";
                    }
                    if (mSjdj.Contains("混凝土波形屋面"))
                    {
                        if (Conversion.Val(d) > 20)
                        {
                            mGd = "d＞20";
                        }
                        else
                        {
                            mGd = "d≤20";
                        }
                    }
                    else
                    {
                        mGd = "----";
                    }
                    if (Conversion.Val(b1) >= 300)
                    {
                        mKd = "b1≥300";
                    }
                    else
                    {
                        if (Conversion.Val(b1) <= 200)
                        {
                            mKd = "b1≤200";
                        }
                        else
                        {
                            mKd = "200＜b1＜300";
                        }
                    }
                    //从设计等级表中取得相应的计算数值、等级标准
                    var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"].Trim() == mSjdj.Trim() && u["GD"].Trim() == mGd.Trim() && u["KD"].Trim() == mKd.Trim());
                    if (null != extraFieldsDj)
                    {
                        MItem[0]["G_DHHZ"] = extraFieldsDj["CZLYQ"].Trim();
                        if ("6b1" == MItem[0]["G_DHHZ"])
                        {
                            MItem[0]["G_DHHZ"] = (Math.Round(6 * double.Parse(b1) / 10, 0) * 10).ToString();
                        }
                        if ("3b1+300" == MItem[0]["G_DHHZ"])
                        {
                            MItem[0]["G_DHHZ"] = (Math.Round((3 * double.Parse(b1) + 300) / 10, 0) * 10).ToString();
                        }
                        if ("2b1+400" == MItem[0]["G_DHHZ"])
                        {
                            MItem[0]["G_DHHZ"] = (Math.Round((2 * double.Parse(b1) + 400) / 10, 0) * 10).ToString();
                        }
                    }
                    double md, pjmd = 0;
                    sum = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        md = double.Parse(sItem["DHHZ"+i]);
                        md = md < 100 ? md * 1000 : md;
                        sum = sum + md;
                        MItem[0]["W_DHCZL" + i] = md.ToString("0");
                    }
                    pjmd = sum / 3;
                    pjmd = Math.Round(pjmd / 10, 0) * 10;
                    sItem["DHPJ"] = pjmd.ToString("0");

                    sum = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        md = double.Parse(MItem[0]["W_DHCZL"+i]);
                        sum = sum + Math.Pow(md - pjmd, 2);
                    }
                    md = Math.Sqrt(sum / 2);
                    md = Math.Round(md / 10, 0) * 10;
                    sItem["DHFOK"] = (Math.Round((pjmd - 1.64 * md) / 10, 0) * 10).ToString("0");
                    sItem["DHBZC"] = md.ToString("0");

                    if (Conversion.Val(sItem["DHFOK"]) >= Conversion.Val(MItem[0]["G_DHHZ"]))
                    {
                        sItem["KDXNPD"] = "合格";
                        mHggs++;
                    }
                    else
                    {
                        sItem["KDXNPD"] = "不合格";
                        itemHG = false;
                        mAllHg = false;
                    }

                    if (1 > Conversion.Val(sItem["WGDHGS"]))
                    {
                        sItem["WGDHSM"] = extraFieldsDj["DHWGYQ"];
                        sItem["KDWGPD"] = "合格";
                        mHggs++;
                    }
                    else
                    {
                        sItem["WGDHSM"] = "外观有" + sItem["WGDHSM"] + "片冻坏";
                        sItem["KDWGPD"] = "不合格";
                        itemHG = false;
                        mAllHg = false;
                    }
                    MItem[0]["G_DHHZ"] = "≥" + MItem[0]["G_DHHZ"];
                    MItem[0]["G_DHWG"] = extraFieldsDj["DHWGYQ"];
                }
                else
                {
                    sItem["KDXNPD"] = "----";
                    sItem["G_DHHZ"] = "----";
                    sItem["G_DHwg"] = "----";
                    sItem["DHBZC"] = "----";
                    sItem["DHFOK"] = "----";
                    sItem["WGDHSM"] = "----";
                    sItem["DHPJ"] = "----";
                    for (int i = 1; i < 4; i++)
                    {
                        MItem[0]["W_DHCZL" + i] = "----";
                    }
                }
                #endregion

                //单组判断
                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
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
                jsbeizhu = "该组样品所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                if (mHggs > 0)
                {
                    jsbeizhu = "该组样品所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "该组样品所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
