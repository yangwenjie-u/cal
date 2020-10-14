using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class ZSG : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_ZSG"];
            var SItem = data["S_ZSG"];
            var mrsDj = dataExtra["BZ_ZSG_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";

            //GB/T9775-2008
            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                var extraFieldsDj = mrsDj.FirstOrDefault(x => x["HD"].Contains(sitem["BCHD"]));
                MItem[0]["G_MMD"] = extraFieldsDj["MMD"].Trim();
                MItem[0]["G_ZXPJZ"] = extraFieldsDj["ZXPJZ"].Trim();
                MItem[0]["G_ZXMIN"] = extraFieldsDj["ZXMIN"].Trim();
                MItem[0]["G_HXPJZ"] = extraFieldsDj["HXPJZ"].Trim();
                MItem[0]["G_HXMIN"] = extraFieldsDj["HXMIN"].Trim();
              
                if (jcxm.Contains("、断裂荷载、"))
                {
                    sitem["ZXPPD"] = IsQualified(MItem[0]["G_ZXPJZ"], sitem["SCZXP"], false);
                    sitem["ZXMPD"] = IsQualified(MItem[0]["G_ZXMIN"], sitem["SCZXM"], false); 
                    sitem["HXPPD"] = IsQualified(MItem[0]["G_HXPJZ"], sitem["SCHXP"], false);
                    sitem["HXMPD"] = IsQualified(MItem[0]["G_HXMIN"], sitem["SCHXM"], false);

                    if (sitem["ZXPPD"] == "合格" && sitem["ZXMPD"] == "合格" && sitem["ZXMPD"] == "合格" && sitem["ZXMPD"] == "合格")
                    {
                        MItem[0]["HG_DXPD"] = "合格";
                    }
                    else
                    {

                        MItem[0]["HG_DXPD"] = "不合格";

                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }




                }
                if (jcxm.Contains("、尺寸偏差、"))
                {
                    if (GetSafeDouble(sitem["SCCD"]) + 6 >= GetSafeDouble(sitem["CD"]))
                    {
                        sitem["CDPD"] = "合格";
                    }
                    else
                    {
                        sitem["CDPD"] = "不合格";
                    }
                    if (GetSafeDouble(sitem["SCKD"]) + 5 >= GetSafeDouble(sitem["KD"]))
                    {
                        sitem["KDPD"] = "合格";
                    }
                    else
                    {
                        sitem["KDPD"] = "不合格";
                    }
                    if (GetSafeDouble(sitem["HD"]) == 9.5)
                    {
                        if (GetSafeDouble(sitem["SCHD"]) >= 9 || GetSafeDouble(sitem["SCHD"]) <= 10)
                        {
                            sitem["HDPD"] = "合格";
                        }
                        else
                        {
                            sitem["HDPD"] = "不合格";
                        }
                    }

                    if (sitem["CDPD"] == "合格" && sitem["KDPD"] == "合格" && sitem["HDPD"] == "合格")
                    {
                        MItem[0]["HZ_DXPD"] = "合格";
                    }
                    else
                    {
                        MItem[0]["HZ_DXPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                if (jcxm.Contains("、面密度、"))
                {
                    if (GetSafeDouble(sitem["SCMMD"]) <= GetSafeDouble(MItem[0]["G_MMD"]))
                    {
                        sitem["MDPD"] = "合格";
                    }
                    else
                    {
                        sitem["MDPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    if (sitem["MDPD"] == "合格")
                    {
                        MItem[0]["MD_DXPD"] = "合格";
                    }
                    else
                    {
                        MItem[0]["MD_DXPD"] = "不合格";
                    }



                }

            }

        }

    }
 }

