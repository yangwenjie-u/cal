using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class DAM : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_DAM"];
            var SItem = data["S_DAM"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];

            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
             

                if (jcxm.Contains("、外观检查、"))
                {
                    jcxmCur = "外观检测";
                    sitem["G_WGJC"] = "永久标识和产品说明等标识应清晰完整；安全帽的帽壳，帽衬(帽箍，吸汗带，缓冲垫及衬带)，帽箍扣，下颌带等组件应完好无缺失，帽壳内外表面应平整光滑，无划痕，裂缝和孔洞，无灼伤，冲击痕迹。";
                    if (sitem["DXPDWG"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                    


                }
                if (jcxm.Contains("、冲击性能试验、"))
                {
                    jcxmCur = "冲击性能试验";
                    sitem["G_CJXN"] = "传递到头模上的冲击力小于4900N，帽壳不得有碎片脱落";

                    if (GetSafeDouble(sitem["SCCJXN1"]) < 4900 && sitem["SFTL1"] == "否")
                    {
                        if (GetSafeDouble(sitem["SCCJXN2"]) < 4900 && sitem["SFTL1"] == "否")
                        {
                            sitem["DXPDCJXN"] = "合格";
                        }
                        else
                        {
                            sitem["DXPDCJXN"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }

                    }
                    else
                    {
                        sitem["DXPDCJXN"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }
                }
                if (jcxm.Contains("、耐穿刺性能试验、"))
                {
                    jcxmCur = "耐穿刺性能试验";
                    sitem["G_NCCXN"] = "钢锥不接触头模表面，帽壳不得有碎片脱落";
                    if (sitem["DXPDNCCXN"] == "不合格")
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

