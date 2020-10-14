using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class HNL : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_HNL"];
            var SItem = data["S_HNL"];
            var extraDJ = dataExtra["BZ_HNL_DJ"];
            var jcxmBhg = "";
            var jcxmCur = "";
            bool mAllHg = true;
            var mitem = MItem[0];




            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                var extraFieldsDj = extraDJ.FirstOrDefault(x => x["KZQDDJ"].Contains(sitem["KZQDDJ"]));
                SItem[0]["G_KZPJZ"] = extraFieldsDj["KZPJZ"].Trim();
                SItem[0]["G_KZMIN"] = extraFieldsDj["KZMIN"].Trim();

                var extraFields = extraDJ.FirstOrDefault(x => x["KYQDDJ"].Contains(sitem["KYQDDJ"]));
                SItem[0]["G_KYPJZ"] = extraFields["KYPJZ"].Trim();
                SItem[0]["G_KYMIN"] = extraFields["KYMIN"].Trim();

                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                  

                    if (GetSafeDouble(GetNum(sitem["SCKYPJZ"])) >= GetSafeDouble(GetNum(sitem["G_KYPJZ"])) && GetSafeDouble(GetNum(sitem["SCKYMIN"])) >= GetSafeDouble(GetNum(sitem["G_KYMIN"])))
                    {
                        sitem["KYDXPD"] = "合格";
                    }
                    else
                    {
                        sitem["KYDXPD"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }



                }
                if (jcxm.Contains("、抗折强度、"))
                {
                    jcxmCur = "抗折强度";

                    if (GetSafeDouble(sitem["SCKZPJZ"]) >= GetSafeDouble(GetNum(sitem["G_KZPJZ"])))
                    {
                        sitem["KZDXPD"] = "合格";
                    }
                    else
                    {
                        sitem["KZDXPD"] = "不合格";
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

