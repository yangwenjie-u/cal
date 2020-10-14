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
                MItem[0]["G_QRSD"] = extraFieldsDj["QRSD"].Trim();
                sitem["G_HWGD"]= extraFieldsDj["BZHWGD"].Trim();

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
                    if (sitem["LB"] == "A15" || sitem["LB"] == "B125" || sitem["LB"] == "C250")
                    {
                        if (2 < GetSafeDouble(sitem["HWGD"]) && GetSafeDouble(sitem["HWGD"]) < 6)
                        {
                            sitem["GDPD"] = "合格";
                        }
                        else
                        {
                            sitem["GDPD"] = "不合格";
                        }


                    }
                    if (sitem["LB"] == "D400" || sitem["LB"] == "E600" || sitem["LB"] == "F900")
                    {
                        if (3 < GetSafeDouble(sitem["HWGD"]) && GetSafeDouble(sitem["HWGD"]) < 8)
                        {
                            sitem["GDPD"] = "合格";
                        }
                        else
                        {
                            sitem["GDPD"] = "不合格";
                        }


                    }
                    //嵌入深度
                    if (GetSafeDouble(sitem["SCSD"]) >= GetSafeDouble(MItem[0]["G_QRSD"]))
                    {
                        sitem["SDPD"] = "合格";
                    }
                    else
                    {
                        sitem["SDPD"] = "不合格";
                    }
                    //总间隙
                    if (GetSafeDouble(sitem["GJSL"]) == 1)
                    {
                        if (GetSafeDouble(sitem["JKK"]) <= 400)
                        {
                            sitem["G_JX"] = "≤3";

                            if (GetSafeDouble(sitem["JX"]) <= 3)
                            {
                                sitem["JXPD"] = "合格";
                            }
                            else
                            {
                                sitem["JXPD"] = "不合格";
                            }

                        }
                        else
                        {
                            sitem["G_JX"] = "≤6";
                            if (GetSafeDouble(sitem["JX"]) <= 6)
                            {
                                sitem["JXPD"] = "合格";
                            }
                            else
                            {
                                sitem["JXPD"] = "不合格";
                            }
                        }





                    }
                    if (GetSafeDouble(sitem["GJSL"]) == 2)
                    {
                        if (GetSafeDouble(sitem["JKK"]) <= 400)
                        {
                            sitem["G_JX"] = "≤7";
                            if (GetSafeDouble(sitem["ZJX"]) <= 7)
                            {
                                sitem["JXPD"] = "合格";
                            }
                            else
                            {
                                sitem["JXPD"] = "不合格";
                            }

                        }
                        else
                        {
                            sitem["G_JX"] = "≤9";
                            if (GetSafeDouble(sitem["JX"]) <= 9)
                            {
                                sitem["JXPD"] = "合格";
                            }
                            else
                            {
                                sitem["JXPD"] = "不合格";
                            }
                        }


                    }
                    if (GetSafeDouble(sitem["GJSL"]) >= 3)
                    {
                        sitem["G_JX"] = "≤15";
                        if (GetSafeDouble(sitem["JX"]) <= 15)
                        {
                            sitem["JXPD"] = "合格";
                        }
                        else
                        {
                            sitem["JXPD"] = "不合格";
                        }

                    }
                    //井座支层面宽度
                    if (GetSafeDouble(sitem["JKK"]) < 600)
                    {
                        sitem["G_CMKD"] = "≥20";
                        if (GetSafeDouble(sitem["CMKD"]) >= 20)
                        {
                            sitem["KDPD"] = "合格";
                        }
                        else
                        {
                            sitem["KDPD"] = "不合格";
                        }

                    }
                    if (GetSafeDouble(sitem["JKK"]) >= 600)
                    {
                        sitem["G_CMKD"] = "≥24";
                        if (GetSafeDouble(sitem["CMKD"]) >= 24)
                        {
                            sitem["KDPD"] = "合格";
                        }
                        else
                        {
                            sitem["KDPD"] = "不合格";
                        }

                    }

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

