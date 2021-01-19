using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZSB : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_ZSB"];
            var SItem = data["S_ZSB"];
            var extraDJ = dataExtra["BZ_ZSBB_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];
            string BC,HD,PMD,ZJPLD;

            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                var extraFieldsDj = extraDJ.FirstOrDefault(x => x["DH"].Contains(sitem["SGBLX"]));

               sitem["SJDLHZPJZ"] = extraFieldsDj["dlhzpjz"].Trim();
               sitem["SJDLHZMIN"] = extraFieldsDj["dlhzmin"].Trim();

                sitem["SJHSLPJZ"] = extraFieldsDj["hslpjz"].Trim();
               sitem["SJHSLMAX"] = extraFieldsDj["hslmax"].Trim();


                if (jcxm.Contains("、尺寸偏差、"))

                {
                    jcxmCur = "尺寸偏差";
                    //边长
                    if (1 > (GetSafeDouble(sitem["CCBCPJZ1"]) - GetSafeDouble(sitem["SJCD"])) && (GetSafeDouble(sitem["CCBCPJZ1"]) - GetSafeDouble(sitem["SJCD"])) > -2 && 1 > (GetSafeDouble(sitem["CCBCPJZ2"]) - GetSafeDouble(sitem["SJCD"])) && (GetSafeDouble(sitem["CCBCPJZ2"]) - GetSafeDouble(sitem["SJCD"])) > -2 && 1 > (GetSafeDouble(sitem["CCBCPJZ3"]) - GetSafeDouble(sitem["SJCD"])) && (GetSafeDouble(sitem["CCBCPJZ3"]) - GetSafeDouble(sitem["SJCD"])) > -2)
                    {
                        BC = "合格";
                    }
                    else
                    {
                        BC = "不合格";
                    }
                    //厚度
                    if (1 > (GetSafeDouble(sitem["CCHDPJZ1"]) - GetSafeDouble(sitem["SJHD"])) && (GetSafeDouble(sitem["CCHDPJZ1"]) - GetSafeDouble(sitem["SJHD"])) > -1 && 1 > (GetSafeDouble(sitem["CCHDPJZ2"]) - GetSafeDouble(sitem["SJHD"])) && (GetSafeDouble(sitem["CCHDPJZ2"]) - GetSafeDouble(sitem["SJHD"])) > -1 && 1 > (GetSafeDouble(sitem["CCHDPJZ3"]) - GetSafeDouble(sitem["SJHD"])) && (GetSafeDouble(sitem["CCHDPJZ3"]) - GetSafeDouble(sitem["SJHD"])) > -1)
                    {
                        HD = "合格";
                    }
                    else
                    {
                        HD = "不合格";
                    }
                    //平面度
                    if (GetSafeDouble(sitem["PMD1"]) <= 2 && GetSafeDouble(sitem["PMD2"]) <= 2 && GetSafeDouble(sitem["PMD3"]) <= 2)
                    {
                        PMD = "合格";
                    }
                    else
                    {
                        PMD = "不合格";
                    }
                    //直角偏离度
                    if ((GetSafeDouble(sitem["SJZJPLD"]) - GetSafeDouble(sitem["SCZJPLD1"]) <= 2) && (GetSafeDouble(sitem["SJZJPLD"]) - GetSafeDouble(sitem["SCZJPLD2"]) <= 2) && (GetSafeDouble(sitem["SJZJPLD"]) - GetSafeDouble(sitem["SCZJPLD3"]) <= 2))
                    {
                        ZJPLD = "合格";

                    }
                    else
                    {
                        ZJPLD = "不合格";
                    }

                    if (BC == "合格" && HD == "合格" && PMD == "合格" && ZJPLD == "合格")
                    {
                        sitem["DXPDCC"] = "合格";
                    }
                    else
                    {
                        sitem["DXPDCC"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                if (jcxm.Contains("、含水率、"))
                {
                    jcxmCur = "含水率";

                    if (GetSafeDouble(sitem["HSLPJZ"]) <= GetSafeDouble(sitem["SJHSLPJZ"]) && GetSafeDouble(sitem["HSLMAX"]) <= GetSafeDouble(sitem["SJHSLMAX"]))
                    {

                        sitem["DXPDHSL"] = "合格";
                    }
                    else
                    {
                        sitem["DXPDHSL"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }



                }


                if (jcxm.Contains("、断裂荷载、"))
                {
                    jcxmCur = "断裂荷载";
                    if (GetSafeDouble(sitem["DLHZPJZ"]) >= GetSafeDouble(sitem["SJDLHZPJZ"]) && GetSafeDouble(sitem["DLHZMIN"]) >= GetSafeDouble(sitem["SJDLHZMIN"]))
                    {

                        sitem["DXPDDLHZ"] = "合格";
                    }
                    else
                    {
                        sitem["DXPDDLHZ"] = "不合格";
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

