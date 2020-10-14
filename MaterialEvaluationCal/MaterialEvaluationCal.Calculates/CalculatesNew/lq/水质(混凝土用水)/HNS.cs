using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class HNS : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_HNS"];
            var SItem = data["S_HNS"];
            var mrsDj = dataExtra["BZ_HNS_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";

            //GB/T9775-2008
            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                var extraFieldsDj = mrsDj.FirstOrDefault(x => x["LB"].Contains(sitem["HNTLX"]));//用途类型

                MItem[0]["G_LSGHL"] = extraFieldsDj["SO"].Trim();
                MItem[0]["G_LLZHL"] = extraFieldsDj["CI"].Trim();
                MItem[0]["G_BRW"] = extraFieldsDj["BRW"].Trim();
                MItem[0]["G_KRW"] = extraFieldsDj["KRW"].Trim();
                MItem[0]["G_PH"] = extraFieldsDj["PH"].Trim();

                if (jcxm.Contains("、硫酸根含量、"))
                {
                    //硫酸钡含量，试料体积
                    sitem["LSGHL"] = ((GetSafeDouble(sitem["LSB"]) * 411.6 * 1000) / GetSafeDouble(sitem["SLTJ"])).ToString();

                    sitem["HG_LSPD"] = IsQualified(MItem[0]["G_LSGHL"],sitem["LSGHL"], false);
                    if (sitem["HG_LSPD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }


                }
                if (jcxm.Contains("、氯离子含量、"))
                {
                    sitem["LLZHL"] = (((GetSafeDouble(sitem["SYXSY"]) - GetSafeDouble(sitem["ZLSXSY"])) * GetSafeDouble(sitem["XSYRD"]) * 35.45 * 1000) / GetSafeDouble(sitem["SYTJ"])).ToString();
                    sitem["HG_LLZ"] = IsQualified(MItem[0]["G_LLZHL"], sitem["LLZHL"],  false);
                    if (sitem["HG_LLZ"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }


                }
                if (jcxm.Contains("、可溶物、"))
                {
                    sitem["KRW"] = (((GetSafeDouble(sitem["M2"]) - GetSafeDouble(sitem["M1"])) * 1000000) / GetSafeDouble(sitem["BRWYPTJ"])).ToString();
                    sitem["HG_KRW"]= IsQualified(MItem[0]["G_KRW"], sitem["KRW"],  false);
                    if (sitem["HG_KRW"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                if (jcxm.Contains("、PH值、"))
                {
                    sitem["HG_PH"] = IsQualified(MItem[0]["G_PH"], sitem["PH"],  false);
                    if (sitem["HG_PH"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                }
                if (jcxm.Contains("、悬浮物、"))
                {
                    sitem["XFW"] = (((GetSafeDouble(sitem["A"]) - GetSafeDouble(sitem["B"])) * 1000000) / GetSafeDouble(sitem["XFWSYTJ"])).ToString();

                    sitem["HG_BRW"] = IsQualified(MItem[0]["G_BRW"], sitem["XFW"], false);
                    if (sitem["HG_BRW"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }


                }
                if (jcxm.Contains("、易沉固体、"))
                {

                }


            }

        }

    }
 }

