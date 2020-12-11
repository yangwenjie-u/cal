using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class WFS : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            
            #region
            #region 参数定义
            var extraDJ = dataExtra["BZ_WFS_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var SItems = data["S_WFS"];
            var MItem = data["M_WFS"];
            if (!data.ContainsKey("M_WFS"))
            {
                data["M_WFS"] = new List<IDictionary<string, string>>();
            }
            if (MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = "";
                MItem.Add(m);
            }

            var mAllHg = true;
            var jcxm = "";
            var jcxmCur = "";
            var jcxmBhg = "";
            List<double> mTmpArray = new List<double>();
            var mFlag_Bhg = false;
            var mFlag_Hg = false;

            var mbhggs = 0;
            var QBZZZD = "";


            List<double> narr = new List<double>();
            #endregion
            //遍历从表数据
            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                #region 等级表数据
                var mrsDj = extraDJ.FirstOrDefault(u => u["QBZZZD"] == sItem["QBZZZD"]);
                sItem["G_MD"] = mrsDj["G_MD"];
                sItem["G_PH"] = mrsDj["G_PH"];

                #endregion




                #region 密度
                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";

                    #region  //固体密度
                    if (!string.IsNullOrEmpty(sItem["MDBJWJJ_1"]))
                    {
                        if (!IsNumeric(sItem["MDBJWJJ_1"]) || !IsNumeric(sItem["MDBJWJJ_2"]) || !IsNumeric(sItem["MDRLBZ_1"]) || !IsNumeric(sItem["MDRLBZ_2"]) || !IsNumeric(sItem["MDTJ_1"]) || !IsNumeric(sItem["MDTJ_2"]))
                        {
                            throw new Exception("请输入密度数据");
                        }
                        for (int i = 1; i < 3; i++)
                        {
                            sItem["MD_" + i] = Round((Conversion.Val(sItem["MDBJWJJ_" + i]) - Conversion.Val(sItem["MDRLBZ_" + i])) / Conversion.Val(sItem["MDTJ_" + i]), 3).ToString();
                        }

                        sItem["MD"] = Round((Conversion.Val(sItem["MD_1"]) + Conversion.Val(sItem["MD_2"])) / 2, 3).ToString();
                    }
                    #endregion
                    #region  //液体
                    else
                    {
                        sItem["MD"] = Round((Conversion.Val(sItem["MD2_1"]) + Conversion.Val(sItem["MD2_2"])) / 2, 3).ToString();
                    }
                    #endregion
                    sItem["HG_MD"] = IsQualified(sItem["G_MD"], sItem["MD"], false);
                    if (sItem["HG_MD"] == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["MD_1"] = "----";
                    sItem["MD_2"] = "----";
                    sItem["MD"] = "----";
                    sItem["HG_MD"] = "----";
                    sItem["G_MD"] = "----";
                }
                #endregion





                #region PH值
                if (jcxm.Contains("、PH值、"))
                {
                    var HG_PH1 = "";
                    var HG_PH2 = "";

                    jcxmCur = "PH值";
                   
                        sItem["PJPH"]=Round((GetSafeDouble(sItem["PH1"]) + GetSafeDouble(sItem["PH2"]))/2, 2).ToString();
                        sItem["HG_PH"] = IsQualified(sItem["G_PH"], sItem["PJPH"], false);
                        HG_PH1 = IsQualified(sItem["G_PH"], sItem["PH1"], false);
                        HG_PH2 = IsQualified(sItem["G_PH"], sItem["PH2"], false);
                    
                   
                    if (sItem["HG_PH"] == "合格"&& HG_PH1 == "合格" && HG_PH2 == "合格")
                    {
                        mFlag_Hg = true;
                    }
                    else
                    {
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }

                }
                else
                {
                    sItem["PJPH"] = "----";
                    sItem["PH1"] = "----";
                    sItem["PH2"] = "----";
                    sItem["HG_PH"] = "----";
                    sItem["G_PH"] = "----";
                }
                #endregion
                
                if (mbhggs == 0)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                mAllHg = (mAllHg && sItem["JCJG"] == "合格");
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }

            //主表总判断赋值
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] +"中"+QBZZZD+"类型"+ "的规定，所检项目均符合要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
              
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "中" + QBZZZD + "类型" + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }

            #endregion
            #endregion

            /************************ 代码结束 *********************/

        }
    }
}