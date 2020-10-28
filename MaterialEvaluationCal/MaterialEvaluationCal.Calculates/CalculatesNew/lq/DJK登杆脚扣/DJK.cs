using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class DJK : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_DJK"];
            var SItem = data["S_DJK"];
           // var extraDJ = dataExtra["BZ_GYC_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];

            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
             

                if (jcxm.Contains("、外观检查、"))

                {
                    jcxmCur = "外观检查";
                    sitem["G_WGJC"] = "围杆钩在扣体内应滑动灵活、可靠、无卡阻现象；保险装置应能可靠防止围杆钩在扣体内脱落。小爪应连接牢固，活动灵活。橡胶防滑快与小爪钢板、围杆钩连接应牢固、覆盖完整，无破损。脚带应完好，止脱扣应良好，无霉变、裂缝或严重变形。";
                    if (sitem["WGDXPD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }


                }
                if (jcxm.Contains("、整体静负荷、"))
                {
                    jcxmCur = "整体静负荷";
                    sitem["G_JFHSY"] = "施加1176N静压力，持续5min,卸载后活动钩应符合外观检查要求，其他受力部位无影响正常工作的变形和其他可见的缺陷。";
                    if (sitem["DXPDFH"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                if (jcxm.Contains("、扣带强力、"))
                {
                    jcxmCur = "扣带强力";
                    sitem["G_KDQL"] = "施加90N静压力，持续5min,卸载后不应出现织带撕裂、金属件明显变形、扣合处明显松脱等现象。";
                    if (sitem["DXPDKDQL"] == "不合格")
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

