using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class GSC : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_GSC"];
            var SItem = data["S_GSC"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];
            int hgs = 0;


            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
             

                if (jcxm.Contains("、外径，壁厚、"))
                {
                    jcxmCur = "外径,壁厚";

                    for (int i = 1; i < 4; i++)
                    {
                        if ((GetSafeDouble(sitem["G_SJWJ"]) * 0.98) < GetSafeDouble(sitem["SCWJ"+i]) && GetSafeDouble(sitem["SCWJ"+i]) < (GetSafeDouble(sitem["G_SJWJ"]) * 1.02) && (GetSafeDouble(sitem["G_SJBH"]) * 0.95) < GetSafeDouble(sitem["SCBH"+i]) && GetSafeDouble(sitem["SCBH"+i]) < (GetSafeDouble(sitem["G_SJBH"]) * 1.05))
                        {
                             hgs = hgs + 1;                           
                        }
                  
                    }
                    if (hgs >= 2)
                    {
                        sitem["DXPDWJBH"] = "合格";
                    }
                    else
                    {
                        sitem["DXPDWJBH"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                 
                   


                }
                if (jcxm.Contains("、抗拉强度、"))
                {
                    jcxmCur = "抗拉强度";

                    sitem["G_KLQD"] = "≥316";
                    if (316 < (GetSafeDouble(sitem["KLHZ1"]) / (GetSafeDouble(sitem["BJ"]) * GetSafeDouble(sitem["BJ"]) * 3.14))/1000 && 316 < (GetSafeDouble(sitem["KLHZ2"]) / (GetSafeDouble(sitem["BJ"]) * GetSafeDouble(sitem["BJ"]) * 3.14)) / 1000)
                    {
                        sitem["DXPDKLQD"] = "合格";
                    }
                    else
                    {
                        sitem["DXPDKLHZ"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                if (jcxm.Contains("、伸长率、"))
                {
                    jcxmCur = "伸长率";
                    sitem["G_SCL"] = "≥14";

                    if (0.14 <= GetSafeDouble(sitem["YSBJ1"]) / GetSafeDouble(sitem["SCHBJ1"])&& 0.14 <= GetSafeDouble(sitem["YSBJ2"]) / GetSafeDouble(sitem["SCHBJ2"]))
                    {
                        sitem["DXPDSCL"] = "合格";

                    }
                    else
                    {
                        sitem["DXPDSCL"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                if (jcxm.Contains("、抗弯曲性能、"))
                {
                    jcxmCur = "抗弯曲性能";
                    sitem["G_WQXN"] = "声测管应进行弯曲试验抗弯曲性能，弯曲试验时，管内不得带填充物，弯曲半径为外径的6倍，弯曲角度为45°，声测管不应出现裂纹";
                    if (sitem["DXPDWQXN"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }

                }
                if (jcxm.Contains("、抗压扁性能、"))
                {
                    jcxmCur = "抗压扁性能";
                    sitem["G_NYBXN"] = "试验时，当两压平板间距离为声测管外径的75%时，声测管不应出现裂纹。";
                    if (sitem["DXPDNYBXN"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }


                }
                if (jcxm.Contains("、连接的可靠性、"))
                {
                    jcxmCur = "连接的可靠性";
                    sitem["G_KKX"] = "常温下声测管应能承受3000N的拉拔力，持续60min连接部分无松动，断裂。";
                    if (sitem["DXPDKKX"] == "不合格")
                    {
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

