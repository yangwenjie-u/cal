using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JGS : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;
            var mrsDj = dataExtra["BZ_JGS_DJ"];
          
            var MItem = data["M_JGS"];
            var SItem = data["S_JGS"];
            var jcxmBhg = "";
            var jcxmCur = "";




            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

               

                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = mrsDj.FirstOrDefault(x => x["D_DJ"].Contains(sitem["LB"]));

                
                MItem[0]["G_BZHZ"] = extraFieldsDj["D_SYHZ"].Trim();

                if (jcxm.Contains("、外观质量、"))
                {
                    sitem["G_WGZL"] = "表面完整，材质均匀，无影响产品使用的缺陷";
                    if (sitem["SCWG"] == "表面完整，材质均匀，无影响产品使用的缺陷")
                    {
                        sitem["WG_DXPD"] = "合格";
                    }
                    else
                    {
                        sitem["WG_DXPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }
                }
                if (jcxm.Contains("、结构尺寸、"))
                {
                    //GB / T23858 - 2009
                }
                if (jcxm.Contains("、承载能力、"))
                {
                    double KYHZ = (GetSafeDouble(sitem["JKK"]) / 250)* GetSafeDouble(MItem[0]["G_BZHZ"]);
                     if (GetSafeDouble(sitem["JKK"]) < 250)
                    {
                        if (KYHZ < (0.6* GetSafeDouble(MItem[0]["G_BZHZ"])))
                        {
                            sitem["JCJG"] = "技术指标不合格，不下结论";
                            break;
                        }
                        else
                        {
                            if (GetSafeDouble(sitem["SCHZ"]) > KYHZ)
                            {
                                sitem["HZ_DXPD"] = "合格";
                            }
                            else
                            {
                                sitem["HZ_DXPD"] = "不合格";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }

                        }
                    }
                    else
                    {
                        if (GetSafeDouble(sitem["SCHZ"]) > GetSafeDouble(MItem[0]["G_BZHZ"]))
                        {
                            sitem["HZ_DXPD"] = "合格";

                        }
                        else
                        {
                            sitem["HZ_DXPD"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                        }
                    
                    }

                }

            }

        }

    }
 }

