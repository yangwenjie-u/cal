using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class ZHZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            
            #region
            #region 参数定义
            var extraDJ = dataExtra["BZ_ZHZ_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var SItems = data["S_ZHZ"];
            var MItem = data["M_ZHZ"];
            if (!data.ContainsKey("M_ZHZ"))
            {
                data["M_ZHZ"] = new List<IDictionary<string, string>>();
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
                QBZZZD = sItem["ZLDJ"] + " " + sItem["QDJB"];
                
                #region 等级表数据
                var mrsDj = extraDJ.FirstOrDefault(u => u["ZLDJ"] == sItem["ZLDJ"]&& u["QDJB"] == sItem["QDJB"]);
                sItem["G_GCCDPC"] = mrsDj["G_GCCDPC"];
                sItem["G_GCKDPC"] = mrsDj["G_GCKDPC"];
                sItem["G_GCHDPC"] = mrsDj["G_GCHDPC"];

                sItem["G_PJKYQD"] = mrsDj["G_PJKYQD"];
                sItem["G_MINKYQD"] = mrsDj["G_MINKYQD"];

                sItem["G_PJKZQD"] = mrsDj["G_PJKZQD"];
                sItem["G_MINKZQD"] = mrsDj["G_MINKZQD"];

                #endregion

                #region 尺寸允许偏差
                if (jcxm.Contains("、尺寸允许偏差、"))
                {
                    jcxmCur = "尺寸允许偏差";
                    #region 长度
                    sItem["PJCD"] = Round((GetSafeDouble(sItem["SCCD1"]) + GetSafeDouble(sItem["SCCD2"])) / 2, 0).ToString();
                    sItem["SCCDPC"] = (GetSafeDouble(sItem["PJCD"]) - GetSafeDouble(sItem["GCCD"])).ToString();

                    var G_GCCDPC = "-" + sItem["G_GCCDPC"]+"～"+sItem["G_GCCDPC"];
                    sItem["HG_CD"] = IsQualified(G_GCCDPC,sItem["SCCDPC"], false);
                    
                    #endregion
                    #region 宽度
                    sItem["PJKD"] = Round((GetSafeDouble(sItem["SCKD1"]) + GetSafeDouble(sItem["SCKD2"])) / 2, 0).ToString();
                    sItem["SCKDPC"] = (GetSafeDouble(sItem["PJKD"]) - GetSafeDouble(sItem["GCKD"])).ToString();

                    var G_GCKDPC = "-" + sItem["G_GCKDPC"] + "～" + sItem["G_GCKDPC"];
                    sItem["HG_KD"] = IsQualified(G_GCKDPC,sItem["SCKDPC"], false);
                    
                    #endregion
                    #region 厚度
                    sItem["PJHD"] = Round((GetSafeDouble(sItem["SCHD1"]) + GetSafeDouble(sItem["SCHD2"])) / 2, 0).ToString();
                    sItem["SCHDPC"] = (GetSafeDouble(sItem["PJHD"]) - GetSafeDouble(sItem["GCHD"])).ToString();

                    var G_GCHDPC = "-" + sItem["G_GCHDPC"] + "～" + sItem["G_GCHDPC"];
                    sItem["HG_HD"] = IsQualified(G_GCHDPC, sItem["SCHDPC"], false);

                    #endregion

                    if (sItem["HG_CD"] == "合格"&& sItem["HG_KD"] == "合格"&& sItem["HG_HD"] == "合格")
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
                    sItem["PJCD"] = "----";
                    sItem["SCCDPC"] = "----";
                    sItem["G_GCCDPC"] = "----";
                    sItem["HG_CD"] = "----";

                    sItem["PJKD"] = "----";
                    sItem["SCKDPC"] = "----";
                    sItem["G_GCKDPC"] = "----";
                    sItem["HG_KD"] = "----";
                    
                    sItem["PJHD"] = "----";
                    sItem["SCHDPC"] = "----";
                    sItem["G_GCHDPC"] = "----";
                    sItem["HG_HD"] = "----";
                    
                }
                #endregion

                #region 抗折强度
                if (jcxm.Contains("、抗折强度、"))
                {
                  
                    jcxmCur = "抗折强度";
                    List<double> KZQD = new List<double>();
                    for(int i = 1; i < 11; i++)
                    {
                        sItem["KZQD" + i] = Round((3 * GetSafeDouble(sItem["KZHZ" + i]) *1000 * GetSafeDouble(sItem["KZKJ" + i])) / (2 * GetSafeDouble(sItem["KZKD" + i]) * GetSafeDouble(sItem["KZGD" + i]) * GetSafeDouble(sItem["KZGD" + i])), 1).ToString();
                        KZQD.Add( GetSafeDouble(sItem["KZQD" + i]));
                    }
                    KZQD.Sort();
                    sItem["MINKZQD"] = KZQD[0].ToString();
                    sItem["PJKZQD"] = KZQD.Average().ToString();
                    sItem["HG_MINKZQD"] = IsQualified(sItem["G_MINKZQD"], sItem["MINKZQD"], false);
                    sItem["HG_PJKZQD"] = IsQualified(sItem["G_PJKZQD"], sItem["PJKZQD"], false);
                    if(sItem["HG_MINKZQD"] == "合格"&& sItem["HG_PJKZQD"] == "合格")
                    {
                        sItem["HG_KZQD"] = "合格";
                    }
                    else { sItem["HG_KZQD"] = "不合格"; }

                    if (sItem["HG_KZQD"] == "合格")
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
                    sItem["MINKZQD"] = "----";
                    sItem["PJKZQD"] = "----";

                    sItem["G_MINKZQD"] = "----";
                    sItem["G_PJKZQD"] = "----";

                    sItem["HG_MINKZQD"] = "----";
                    sItem["HG_PJKZQD"] = "----";
                    sItem["HG_KZQD"] = "----";
                }
                #endregion

                #region 抗压强度
                if (jcxm.Contains("、抗压强度、"))
                {

                    jcxmCur = "抗压强度";
                    List<double> KYQD = new List<double>();
                    for (int i = 1; i < 11; i++)
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