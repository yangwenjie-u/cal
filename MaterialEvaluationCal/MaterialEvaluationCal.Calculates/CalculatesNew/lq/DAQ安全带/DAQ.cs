using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class DAQ : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_DAQ"];
            var SItem = data["S_DAQ"];
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
                    jcxmCur = "外观检测";
                    sitem["G_WGJC"] = "商标，合格证和检验证等标识应清晰完整，各部件应完整无缺失，无伤残破损。";
                    if (sitem["WGDXPD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }


                }
                if (jcxm.Contains("、静负荷试验、"))
                {
                    jcxmCur = "静荷载试验";
                    if (sitem["ZL"] == "坠落悬挂安全带")
                    {
                        sitem["G_JFHSY"] = "3300";
                        sitem["G_HZSJ"] = "5";
                        if (GetSafeDouble(sitem["SCJFHSY"]) >= GetSafeDouble(sitem["G_JFHSY"]) && GetSafeDouble(sitem["SCHZSJ"]) >= GetSafeDouble(sitem["G_HZSJ"]))
                        {
                            sitem["JFHSYDXPD"] = "合格";
                        }
                        else
                        {
                            sitem["JFHSYDXPD"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    if (sitem["ZL"] == "围杆作业安全带")
                    {
                        sitem["G_JFHSY"] = "2205";
                        sitem["G_HZSJ"] = "5";
                        if (GetSafeDouble(sitem["SCJFHSY"]) >= GetSafeDouble(sitem["G_JFHSY"]) && GetSafeDouble(sitem["SCHZSJ"]) >= GetSafeDouble(sitem["G_HZSJ"]))
                        {
                            sitem["JFHSYDXPD"] = "合格";
                        }
                        else
                        {
                            sitem["JFHSYDXPD"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    if (sitem["ZL"] == "区域限制安全带")
                    {
                        sitem["G_JFHSY"] = "1200";
                        sitem["G_HZSJ"] = "5";
                        if (GetSafeDouble(sitem["SCJFHSY"]) >= GetSafeDouble(sitem["G_JFHSY"]) && GetSafeDouble(sitem["SCHZSJ"]) >= GetSafeDouble(sitem["G_HZSJ"]))
                        {
                            sitem["JFHSYDXPD"] = "合格";
                        }
                        else
                        {
                            sitem["JFHSYDXPD"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
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

