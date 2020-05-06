using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class WB : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_WB_DJ"];
            //var extraZM_DRJL = dataExtra["ZM_DRJL"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_WBS = data["S_WB"];
            if (!data.ContainsKey("M_WB"))
            {
                data["M_WB"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_WB"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            List<double> nArr = new List<double>();
            bool sjtabcalc = true;
            int mbHggs = 0;//统计合格数量
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in S_WBS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var mrsDj = extraDJ.FirstOrDefault(u => u["GGXH"] == sItem["CPMC"]);
                if (mrsDj != null)
                {
                    sItem["G_DRXS"] = mrsDj["DRXS"];
                    sItem["G_GMD"] = mrsDj["GMD"];
                    sItem["G_NJQD_YQD"] = mrsDj["NJQD_YQD"];
                    sItem["G_NJQD_NS"] = mrsDj["NJQD_NS"];
                    if (sItem["CPMC"].Contains("保温隔热型") && sItem["SYYT"].Contains("墙体用"))
                    {
                        sItem["G_KYQD"] = mrsDj["KYQD_QT"];
                    }
                    else
                    {
                        sItem["G_KYQD"] = mrsDj["KYQD_DM"];
                    }


                }
                else
                {
                    jsbeizhu = "依据不详";
                    sItem["JCJG"] = "不下结论";
                    mAllHg = false;
                    continue;
                }
                if (!string.IsNullOrEmpty(MItem[0]["SJTABS"]))
                {
                    #region 跳转
                    sjtabcalc = true;
                    #region 干表观密度
                    if (jcxm.Contains("、干表观密度、"))
                    {
                        jcxmCur = "干表观密度";
                        sItem["HG_GMD"] = IsQualified(sItem["G_GMD"], sItem["GMD"], false);
                        if ("不合格" == sItem["HG_GMD"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        sItem["GMD"] = "----";
                        sItem["HG_GMD"] = "----";
                        sItem["G_GMD"] = "----";
                    }
                    #endregion

                    #region 堆积密度
                    if (jcxm.Contains("、堆积密度、"))
                    {
                        jcxmCur = "堆积密度";
                        sItem["HG_DJMD"] = IsQualified(sItem["G_DJMD"], sItem["DJMD"], false);
                        if ("不合格" == sItem["HG_DJMD"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        sItem["DJMD"] = "----";
                        sItem["HG_DJMD"] = "----";
                        sItem["G_DJMD"] = "----";
                    }
                    #endregion

                    #region 抗压强度
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        jcxmCur = "抗压强度";
                        if (GetSafeDouble(sItem["KYQD"]) == 0)
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_KYQD"] = IsQualified(sItem["G_KYQD"], sItem["KYQD"], false);
                        if ("不合格" == sItem["HG_KYQD"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        sItem["KYQD"] = "----";
                        sItem["HG_KYQD"] = "----";
                        sItem["G_KYQD"] = "----";
                    }
                    #endregion

                    #region 软化系数
                    if (jcxm.Contains("、软化系数、"))
                    {
                        jcxmCur = "软化系数";
                        if (GetSafeDouble(sItem["RHXS"]) == 0)
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_RHXS"] = IsQualified(sItem["G_RHXS"], sItem["RHXS"], false);
                        if ("不合格" == sItem["HG_RHXS"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        sItem["RHXS"] = "----";
                        sItem["HG_RHXS"] = "----";
                        sItem["G_RHXS"] = "----";
                    }
                    #endregion

                    #region 抗拉强度
                    if (jcxm.Contains("、抗拉强度、"))
                    {
                        jcxmCur = "抗拉强度";
                        if (0 == GetSafeDouble(sItem["KLQD"]))
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false);
                        if ("不合格" == sItem["HG_KLQD"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        sItem["KLQD"] = "----";
                        sItem["HG_KLQD"] = "----";
                        sItem["G_KLQD"] = "----";
                    }
                    #endregion

                    #region 导热系数
                    if (jcxm.Contains("、导热系数、"))
                    {
                        jcxmCur = "导热系数";
                        int mcd = sItem["G_DRXS"].Length;
                        int mdwz = sItem["G_DRXS"].IndexOf(".");
                        mcd = mcd - mdwz + 1;
                        //if (0 < MItem[0]["DEVCODE"].IndexOf("XCS17-067") || 0< MItem[0]["DEVCODE"].IndexOf("XCS17-066"))
                        //{
                        //    //从表zm_drjl筛选出数据
                        //    var mrsDrxs = extraZM_DRJL.FirstOrDefault(u => u["SYLB"] == "WB" && u["SYBH"] ==  MItem[0]["JYDBH"]);
                        //    //var mrsDrxs = extraZM_DRJL.Where(extraZM_DRJL_Filter => extraZM_DRJL_Filter["SYSJBRECID"].Equals(sitem["RECID"]));
                        //    sItem["DRXS"] = mrsDrxs["DRXS"];
                        //    MItem[0]["JCYJ"] = MItem[0]["JCYJ"].Replace("10294", "10295");//字符串替换
                        //}
                        sItem["DRXS"] = Math.Round(GetSafeDouble(sItem["DRXS"]), mcd).ToString();
                        sItem["HG_DRXS"] = IsQualified(sItem["G_DRXS"], sItem["DRXS"], false);
                        if ("不合格" == sItem["HG_DRXS"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        sItem["DRXS"] = "----";
                        sItem["HG_DRXS"] = "----";
                        sItem["G_DRXS"] = "----";
                    }
                    #endregion

                    #region 线性收缩率
                    if (jcxm.Contains("、线性收缩率、"))
                    {
                        jcxmCur = "线性收缩率";
                        sItem["HG_XXSSL"] = IsQualified(sItem["G_XXSSL"], sItem["XXSSL"], false);
                        if ("不合格" == sItem["HG_XXSSL"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        sItem["XXSSL"] = "----";
                        sItem["HG_XXSSL"] = "----";
                        sItem["G_XXSSL"] = "----";
                    }
                    #endregion

                    #region 压剪粘结强度(与水泥砂浆块)耐水强度
                    if (jcxm.Contains("、压剪粘结强度(与水泥砂浆块)耐水强度、"))
                    {
                        jcxmCur = "压剪粘结强度(与水泥砂浆块)耐水强度";
                        if (GetSafeDouble(sItem["NJQD_YQD"]) == 0)
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_NJQD_NS"] = IsQualified(sItem["G_NJQD_NS"], sItem["NJQD_NS"], false);
                        if ("不合格" == sItem["HG_NJQD_NS"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        sItem["NJQD_NS"] = "----";
                        sItem["HG_NJQD_NS"] = "----";
                        sItem["G_NJQD_NS"] = "----";
                    }
                    #endregion

                    #region 压剪粘结强度(与水泥砂浆块)原强度
                    if (jcxm.Contains("、压剪粘结强度(与水泥砂浆块)原强度、"))
                    {
                        jcxmCur = "压剪粘结强度(与水泥砂浆块)原强度";
                        if (0 == GetSafeDouble(sItem["NJQD_YQD"]))
                        {
                            sjtabcalc = false;
                            //sItem["SYR"] = "";
                        }
                        sItem["HG_NJQD_YQD"] = IsQualified(sItem["G_NJQD_YQD"], sItem["NJQD_YQD"], false);
                        if ("不合格" == sItem["HG_NJQD_YQD"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
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
                        sItem["NJQD_YQD"] = "----";
                        sItem["HG_NJQD_YQD"] = "----";
                        sItem["G_NJQD_YQD"] = "----";
                    }
                    #endregion
                    #endregion
                }
                else //非跳转
                {
                    #region 导热系数
                    if (jcxm.Contains("、导热系数、"))
                    {
                        jcxmCur = "导热系数";
                        if (IsQualified(sItem["G_DRXS"], sItem["DRXS"], false) == "合格")
                        {
                            sItem["HG_DRXS"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_DRXS"] = "不合格";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["DRXS"] = "----";
                        sItem["HG_DRXS"] = "----";
                        sItem["G_DRXS"] = "----";
                    }
                    #endregion

                    #region 干表观密度
                    if (jcxm.Contains("、干表观密度、"))
                    {
                        jcxmCur = "干表观密度";

                        List<double> lArray = new List<double>();
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["BGMD" + i] = Round((GetSafeDouble(sItem["BGMDCZL" + i]) / (GetSafeDouble(sItem["BGMDCD" + i]) / 1000) * GetSafeDouble(sItem["BGMDCK" + i]) / 1000 * GetSafeDouble(sItem["BGMDCH" + i]) / 1000), 0).ToString();
                            lArray.Add(GetSafeDouble(sItem["BGMD" + i]));
                        }
                        lArray.Sort();
                        sItem["BGMD"] = lArray.Average().ToString();

                        if (IsQualified(sItem["G_GMD"], sItem["BGMD"], false) == "合格")
                        {
                            sItem["HG_GMD"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_GMD"] = "不合格";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["BGMD"] = "----";
                        sItem["HG_GMD"] = "----";
                        sItem["G_GMD"] = "----";
                    }
                    #endregion

                    #region 抗压强度
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        jcxmCur = "抗压强度";
                        List<double> lArray = new List<double>();
                        for (int i = 1; i < 3; i++)
                        {
                            sItem["KYQD" + i] = Round(GetSafeDouble(sItem["KYQDHZ" + i]) / (GetSafeDouble(sItem["KYQDC" + i]) * GetSafeDouble(sItem["KYQDK" + i])), 2).ToString();
                            lArray.Add(GetSafeDouble(sItem["KYQD" + i]));
                        }
                        lArray.Sort();
                        sItem["KYQD"] = lArray.Average().ToString();

                        if (IsQualified(sItem["G_KYQD"], sItem["KYQD"], false) == "合格")
                        {
                            sItem["HG_KYQD"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_KYQD"] = "不合格";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["KYQD"] = "----";
                        sItem["HG_KYQD"] = "----";
                        sItem["G_KYQD"] = "----";
                    }
                    #endregion

                    #region 压剪粘结强度(与水泥砂浆块)原强度
                    if (jcxm.Contains("、压剪粘结强度(与水泥砂浆块)原强度、"))
                    {
                        jcxmCur = "压剪粘结强度(与水泥砂浆块)原强度";
                        List<double> lArray = new List<double>();
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["NJQD_YQD" + i] = Round(GetSafeDouble(sItem["NJQDYQDHZ"+i])/10000,3).ToString();
                            lArray.Add(GetSafeDouble(sItem["NJQD_YQD" + i]));
                        }
                        lArray.Sort();
                        lArray.Remove(lArray.Max());
                        lArray.Remove(lArray.Min());
                        sItem["NJQD_YQD"] = lArray.Average().ToString();

                        if (IsQualified(sItem["G_NJQD_YQD"], sItem["NJQD_YQD"], false) == "合格")
                        {
                            sItem["HG_NJQD_YQD"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_NJQD_YQD"] = "不合格";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["NJQD_YQD"] = "----";
                        sItem["HG_NJQD_YQD"] = "----";
                        sItem["G_NJQD_YQD"] = "----";
                    }
                    #endregion

                    #region 压剪粘结强度(与水泥砂浆块)耐水强度
                    if (jcxm.Contains("、压剪粘结强度(与水泥砂浆块)耐水强度、"))
                    {
                        jcxmCur = "压剪粘结强度(与水泥砂浆块)耐水强度";
                        List<double> lArray = new List<double>();
                        for (int i = 1; i < 7; i++)
                        {
                            sItem["NJQD_NS" + i] = Round(GetSafeDouble(sItem["NJQDNSHZ" + i]) / 10000, 3).ToString();
                            lArray.Add(GetSafeDouble(sItem["NJQD_NS" + i]));
                        }
                        lArray.Sort();
                        lArray.Remove(lArray.Max());
                        lArray.Remove(lArray.Min());
                        sItem["NJQD_NS"] = lArray.Average().ToString();

                        if (IsQualified(sItem["G_NJQD_NS"], sItem["NJQD_NS"], false) == "合格")
                        {
                            sItem["HG_NJQD_NS"] = "合格";
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_NJQD_NS"] = "不合格";
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["NJQD_NS"] = "----";
                        sItem["HG_NJQD_NS"] = "----";
                        sItem["G_NJQD_NS"] = "----";
                    }
                    #endregion
                }
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
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                //if (mbHggs > 0)
                //{
                //    jsbeizhu = "该组试件所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                //}
                //else
                //{
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                //}
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/


        }

    }
}
