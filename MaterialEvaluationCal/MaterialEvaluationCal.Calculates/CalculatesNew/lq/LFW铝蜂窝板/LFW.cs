using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class LFW : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_LFW"];
            var SItem = data["S_LFW"];
           // var extraDJ = dataExtra["BZ_GYC_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];

            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
             

                if (jcxm.Contains("、外观质量、"))

                {
                    jcxmCur = "外观质量";
                    if (sitem["WGDXPD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                if (jcxm.Contains("、尺寸及允许偏差、"))
                {
                    jcxmCur = "尺寸及允许偏差";
                    sitem["JXWCZ"] = (GetSafeDouble(sitem["SCJXZ"]) - GetSafeDouble(sitem["GCCC"])).ToString();
                    if (GetSafeDouble(sitem["JXWCZ"]) >= 0.25)
                    {
                        sitem["CCDXPD"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        sitem["CCDXPD"] = "合格";
                    }

                }


                if (jcxm.Contains("、铝板厚度、"))

                {
                    jcxmCur = "铝板厚度";
                    if (sitem["BM"] == "面板")
                    {
                        sitem["G_LBHD"] = "≥1.0mm";
                    }
                    else
                    {
                        sitem["G_LBHD"] = "≥0.7mm";
                    }
                    if (GetSafeDouble(sitem["LBMIN"]) >= GetSafeDouble(GetNum(sitem["G_LBHD"])) && GetSafeDouble(sitem["LBPJZ"]) >= GetSafeDouble(GetNum(sitem["G_LBHD"])))
                    {
                        sitem["LBDXPD"] = "合格";

                    }
                    else
                    {
                        sitem["LBDXPD"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }



                }

                if (jcxm.Contains("、装饰面层厚度、"))

                {
                    jcxmCur = "装饰面层厚度";
                    if (sitem["TCLX"] == "氟碳涂层")
                    {
                        if (sitem["TCCS"] == "二涂")
                        {
                            if (sitem["TCFS"] == "辊涂")
                            {
                                sitem["G_ZSMCHD_PJZ"] = "平均值 ≥25";

                                sitem["G_ZSMCHD"] = "最小值 ≥23";
                            }
                            else
                            {
                                sitem["G_ZSMCHD_PJZ"] = "平均值 ≥30";

                                sitem["G_ZSMCHD"] = "最小值 ≥25";

                            }

                        }
                        if (sitem["TCCS"] == "三涂")
                        {
                            if (sitem["TCFS"] == "辊涂")
                            {
                                sitem["G_ZSMCHD_PJZ"] = "平均值 ≥32";

                                sitem["G_ZSMCHD"] = "最小值 ≥30";
                            }
                            else
                            {
                                sitem["G_ZSMCHD_PJZ"] = "平均值 ≥40";

                                sitem["G_ZSMCHD"] = "最小值 ≥35";

                            }

                        }

                    }
                    else
                    {
                        sitem["G_ZSMCHD_PJZ"] = "平均值 ≥20";

                        sitem["G_ZSMCHD"] = "最小值 ≥16";

                    }

                    if (GetSafeDouble(sitem["ZSMCHDMIN"]) >= GetSafeDouble(GetNum(sitem["G_ZSMCHD"])) && GetSafeDouble(sitem["ZSMCHDPJZ"]) >= GetSafeDouble(GetNum(sitem["G_ZSMCHD_PJZ"])))
                    {
                        sitem["ZSMCDXPD"] = "合格";

                    }
                    else
                    {
                        sitem["ZSMCDXPD"] = "合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }


                }

                if (jcxm.Contains("、表面硬度、"))

                {
                    jcxmCur = "表面硬度";
                    if (GetSafeDouble(sitem["YDSCJG"]) < GetSafeDouble(sitem["G_YD"]))
                    {
                        sitem["YDDXPD"] = "合格";

                    }
                    else
                    {
                        sitem["YDDXPD"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }



                }



            }
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求";
            }

        }

    }
 }

