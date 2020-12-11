using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class ZQK : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            
            #region
            #region 参数定义
            var extraDJ = dataExtra["BZ_ZQK_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var SItems = data["S_ZQK"];
            var MItem = data["M_ZQK"];
            if (!data.ContainsKey("M_ZQK"))
            {
                data["M_ZQK"] = new List<IDictionary<string, string>>();
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
                QBZZZD = sItem["KSLX"] + " " + sItem["QDJB"];
                
                #region 等级表数据
                var mrsDj = extraDJ.FirstOrDefault(u =>  u["QDJB"] == sItem["QDJB"]);
             

                sItem["G_PJKYQD"] = mrsDj["G_PJKYQD"];
                sItem["G_MINKYQD"] = mrsDj["G_MINKYQD"];

             

                #endregion

                

                

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {

                    jcxmCur = "抗压强度";
                    List<double> KYQD = new List<double>();
                    for (int i = 1; i < 6; i++)
                    {
                        sItem["KYQD" + i] = Round(GetSafeDouble(sItem["KYHZ" + i])*1000  /  GetSafeDouble(sItem["KYKD" + i]) / GetSafeDouble(sItem["KYGD" + i]), 1).ToString();

                        KYQD.Add(GetSafeDouble(sItem["KYQD" + i]));
                    }
                    KYQD.Sort();
                    sItem["MINKYQD"] = KYQD[0].ToString();
                    sItem["PJKYQD"] = KYQD.Average().ToString();
                    sItem["HG_MINKYQD"] = IsQualified(sItem["G_MINKYQD"], sItem["MINKYQD"], false);
                    sItem["HG_PJKYQD"] = IsQualified(sItem["G_PJKYQD"], sItem["PJKYQD"], false);
                    if (sItem["HG_MINKYQD"] == "合格" && sItem["HG_PJKYQD"] == "合格")
                    {
                        sItem["HG_KYQD"] = "合格";
                    }
                    else { sItem["HG_KYQD"] = "不合格"; }

                    if (sItem["HG_KYQD"] == "合格")
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
                    sItem["MINKYQD"] = "----";
                    sItem["PJKYQD"] = "----";

                    sItem["G_MINKYQD"] = "----";
                    sItem["G_PJKYQD"] = "----";

                    sItem["HG_MINKYQD"] = "----";
                    sItem["HG_PJKYQD"] = "----";
                    sItem["HG_KYQD"] = "----";
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