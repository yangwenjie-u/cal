using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JBL:BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_JBL_DJ"];
            var data = retData;

            var SItem = data["S_JBL"];
            var MItem = data["M_JBL"];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["CPMC"].Trim());
                //if (null != extraFieldsDj)
                //{
                //    //sItem["G_MD"] = extraFieldsDj["MD"];
                //    //sItem["G_XSL"] = extraFieldsDj["XSL"];
                //    //sItem["G_HSL"] = extraFieldsDj["HSL"];
                //    //sItem["G_KZQD"] = extraFieldsDj["KZQD"];
                //    //sItem["G_KYQD"] = extraFieldsDj["KYQD"];
                //}
                //else
                //{
                //    mAllHg = false;
                //    mjcjg = "不下结论";
                //    jsbeizhu = jsbeizhu + "依据不详";
                //    continue;
                //}

                #region 露点
                if (jcxm.Contains("、露点、"))
                {
                    jcxmCur = "露点";
                    sItem["G_LD"] = "<-40℃";
                    sItem["HG_LD"] = "合格";
                    List<double> iArray = new List<double>();
                    for (int i = 1; i < 16; i++)
                    {
                        if (Conversion.Val(sItem["LDWD" + i]) > -40)
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_LD"] = "不合格";
                            mAllHg = false;
                        }
                    }
                }
                else
                {
                    sItem["G_LD"] = "----";
                    sItem["HG_LD"] = "----";
                    //sItem["W_LD"] = "----";
                }
                #endregion

                #region 耐紫外线辐照
                if (jcxm.Contains("、耐紫外线辐照、"))
                {
                    jcxmCur = "耐紫外线辐照";
                    sItem["G_NZWXFZ"] = "无结雾、水气凝结或污染的痕迹且密封胶无明显变形";
                    sItem["HG_NZWXFZ"] = "合格";
                    for (int i = 1; i < 5; i++)
                    {
                        if (sItem["NZWXFZ" + i] == "否")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_NZWXFZ"] = "不合格";
                            mAllHg = false;
                        }
                        else if (string.IsNullOrEmpty(sItem["NZWXFZ" + i]))
                        {
                            sItem["NZWXFZ" + i] = "----";
                        }
                    }
                }
                else
                {
                    sItem["G_NZWXFZ"] = "----";
                    //sItem["W_NZWXFZ"] = "----";
                    sItem["HG_NZWXFZ"] = "----";
                }
                #endregion

                #region 可见光透射比
                if (jcxm.Contains("、可见光透射比、"))
                {
                    jcxmCur = "可见光透射比";
                    if (sItem["SJ_TSB"] !="----" && !sItem["SJ_TSB"].Contains("≥"))
                    {
                        sItem["G_KJGTSB"] = "≥" + sItem["SJ_TSB"];
                    }
                   
                    if (IsQualified(sItem["G_KJGTSB"], sItem["W_KJGTSB"], false) == "合格")
                    {
                        sItem["HG_KJGTSB"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KJGTSB"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_KJGTSB"] = "----";
                    sItem["W_KJGTSB"] = "----";
                    sItem["HG_KJGTSB"] = "----";
                }
                #endregion

                #region 可见光反射比
                if (jcxm.Contains("、可见光反射比、"))
                {
                    jcxmCur = "可见光反射比";
                    if (sItem["G_KJGFSB"] != "----" && !sItem["G_KJGFSB"].Contains("≥"))
                    {
                        sItem["G_KJGFSB"] = "≥" + sItem["G_KJGFSB"];
                    }

                    if (IsQualified(sItem["G_KJGFSB"], sItem["W_KJGFSB"], false) == "合格")
                    {
                        sItem["HG_KJGFSB"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KJGFSB"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_KJGFSB"] = "----";
                    sItem["W_KJGFSB"] = "----";
                    sItem["HG_KJGFSB"] = "----";
                }
                #endregion

                #region 太阳光直接投射比
                if (jcxm.Contains("、太阳光直接投射比、"))
                {
                    jcxmCur = "太阳光直接投射比";
                    if (sItem["G_TYGZJTSB"] != "----" && !sItem["G_TYGZJTSB"].Contains("≥"))
                    {
                        sItem["G_TYGZJTSB"] = "≥" + sItem["G_TYGZJTSB"];
                    }

                    if (IsQualified(sItem["G_TYGZJTSB"], sItem["W_TYGZJTSB"], false) == "合格")
                    {
                        sItem["HG_TYGZJTSB"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_TYGZJTSB"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_TYGZJTSB"] = "----";
                    sItem["W_TYGZJTSB"] = "----";
                    sItem["HG_TYGZJTSB"] = "----";
                }
                #endregion

                #region 太阳光直接反射比
                if (jcxm.Contains("、太阳光直接反射比、"))
                {
                    jcxmCur = "太阳光直接反射比";
                    if (sItem["G_TYGZJFSB"] != "----" && !sItem["G_TYGZJFSB"].Contains("≥"))
                    {
                        sItem["G_TYGZJFSB"] = "≥" + sItem["G_TYGZJFSB"];
                    }

                    if (IsQualified(sItem["G_TYGZJFSB"], sItem["W_TYGZJFSB"], false) == "合格")
                    {
                        sItem["HG_TYGZJFSB"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_TYGZJFSB"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_TYGZJFSB"] = "----";
                    sItem["W_TYGZJFSB"] = "----";
                    sItem["HG_TYGZJFSB"] = "----";
                }
                #endregion

                #region 太阳能总透射比
                if (jcxm.Contains("、太阳能总透射比、"))
                {
                    jcxmCur = "太阳能总透射比";
                    if (sItem["G_TYNZTSB"] != "----" && !sItem["G_TYNZTSB"].Contains("≥"))
                    {
                        sItem["G_TYNZTSB"] = "≥" + sItem["G_TYNZTSB"];
                    }

                    if (IsQualified(sItem["G_TYNZTSB"], sItem["W_TYNZTSB"], false) == "合格")
                    {
                        sItem["HG_TYNZTSB"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_TYNZTSB"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_TYNZTSB"] = "----";
                    sItem["W_TYNZTSB"] = "----";
                    sItem["HG_TYNZTSB"] = "----";
                }
                #endregion

                #region 紫外线透射比
                if (jcxm.Contains("、紫外线透射比、"))
                {
                    jcxmCur = "紫外线透射比";
                    if (sItem["G_ZWXTSB"] != "----" && !sItem["G_ZWXTSB"].Contains("≥"))
                    {
                        sItem["G_ZWXTSB"] = "≥" + sItem["G_ZWXTSB"];
                    }

                    if (IsQualified(sItem["G_ZWXTSB"], sItem["W_ZWXTSB"], false) == "合格")
                    {
                        sItem["HG_ZWXTSB"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_ZWXTSB"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_ZWXTSB"] = "----";
                    sItem["W_ZWXTSB"] = "----";
                    sItem["HG_ZWXTSB"] = "----";
                }
                #endregion

                #region 紫外线反射比
                if (jcxm.Contains("、紫外线反射比、"))
                {
                    jcxmCur = "紫外线反射比";
                    if (sItem["G_ZWXFSB"] != "----" && !sItem["G_ZWXFSB"].Contains("≥"))
                    {
                        sItem["G_ZWXFSB"] = "≥" + sItem["G_ZWXFSB"];
                    }

                    if (IsQualified(sItem["G_ZWXFSB"], sItem["W_ZWXFSB"], false) == "合格")
                    {
                        sItem["HG_ZWXFSB"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_ZWXFSB"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_ZWXFSB"] = "----";
                    sItem["W_ZWXFSB"] = "----";
                    sItem["HG_ZWXFSB"] = "----";
                }
                #endregion

                #region 遮蔽系数
                if (jcxm.Contains("、遮蔽系数、"))
                {
                    jcxmCur = "遮蔽系数";
                    if (sItem["ZBXSSJZ"] != "----" && !sItem["ZBXSSJZ"].Contains("≤"))
                    {
                        sItem["G_ZBXS"] = "≤" + sItem["ZBXSSJZ"];
                    }

                    if (IsQualified(sItem["G_ZBXS"], sItem["W_ZBXS"], false) == "合格")
                    {
                        sItem["HG_ZBXS"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_ZBXS"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_ZBXS"] = "----";
                    sItem["W_ZBXS"] = "----";
                    sItem["HG_ZBXS"] = "----";
                }
                #endregion

                #region 传热系数
                if (jcxm.Contains("、传热系数、"))
                {
                    jcxmCur = "传热系数";
                    if (sItem["G_CRXS"] != "----" && !sItem["G_CRXS"].Contains("≤"))
                    {
                        sItem["G_CRXS"] = "≤" + sItem["G_CRXS"];
                    }

                    if (IsQualified(sItem["G_CRXS"], sItem["W_CRXS"], false) == "合格")
                    {
                        sItem["HG_CRXS"] = "合格";
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_CRXS"] = "不合格";
                        mAllHg = false;
                    }
                }
                else
                {
                    sItem["G_CRXS"] = "----";
                    sItem["W_CRXS"] = "----";
                    sItem["HG_CRXS"] = "----";
                }
                #endregion

            }

            #region 最终结果
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion

            #endregion
        }
    }
}
