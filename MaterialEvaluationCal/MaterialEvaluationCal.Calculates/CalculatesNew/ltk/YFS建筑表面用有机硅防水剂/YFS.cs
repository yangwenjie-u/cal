using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class YFS : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            
            #region
            #region 参数定义
            var extraDJ = dataExtra["BZ_YFS_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var SItems = data["S_YFS"];
            var MItem = data["M_YFS"];
            if (!data.ContainsKey("M_YFS"))
            {
                data["M_YFS"] = new List<IDictionary<string, string>>();
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
                sItem["G_GTHL"] = mrsDj["G_GTHL"];
                #endregion




                #region 固体含量
                if (jcxm.Contains("、固体含量、"))
                {
                    jcxmCur = "固体含量";

                    for (int i = 1; i < 3; i++)
                    {
                        sItem["GTHL" + i] = Round((Conversion.Val(sItem["H_GTHLPYZ" + i]) - Conversion.Val(sItem["GTHLPZ" + i])) / (Conversion.Val(sItem["Q_GTHLPYZ" + i]) - Conversion.Val(sItem["GTHLPZ" + i])) * 100, 2).ToString();
                    }

                    sItem["PJGTHL"] = Round((Conversion.Val(sItem["GTHL1"]) + Conversion.Val(sItem["GTHL2"])) / 2, 2).ToString();

                    sItem["HG_GTHL"] = IsQualified(sItem["G_GTHL"], sItem["PJGTHL"], false);

                    if (sItem["HG_GTHL"] == "合格")
                    {
                        if("合格"== IsQualified(sItem["G_GTHL"], sItem["GTHL1"], false))
                        {
                            sItem["HG_GTHL"] = IsQualified(sItem["G_GTHL"], sItem["GTHL2"], false);
                        }
                        if (sItem["HG_GTHL"] == "合格")
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
                        mbhggs = mbhggs + 1;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mFlag_Bhg = true;
                    }
                }
                else
                {
                    sItem["GTHL1"] = "----";
                    sItem["GTHL2"] = "----";
                    sItem["PJGTHL"] = "----";
                    sItem["G_GTHL"] = "----";
                    sItem["HG_GTHL"] = "----";
                }
                #endregion


               

               
                #region PH值
                if (jcxm.Contains("、PH值、"))
                {
                    var HG_PH1 = "";
                    var HG_PH2 = "";

                    jcxmCur = "PH值";
                    if (sItem["PHKZZ"] == "----")
                    {
                        MItem[0]["G_PH"] = "----";
                    }
                    else
                    {
                        sItem["PJPH"]=Round((GetSafeDouble(sItem["PH1"]) + GetSafeDouble(sItem["PH2"]))/2, 2).ToString();
                        sItem["HG_PH"] = IsQualified(sItem["PHKZZ"], sItem["PJPH"], false);
                        HG_PH1 = IsQualified(sItem["PHKZZ"], sItem["PH1"], false);
                        HG_PH2 = IsQualified(sItem["PHKZZ"], sItem["PH2"], false);
                    }
                    sItem["G_PH"] = sItem["PHKZZ"];
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