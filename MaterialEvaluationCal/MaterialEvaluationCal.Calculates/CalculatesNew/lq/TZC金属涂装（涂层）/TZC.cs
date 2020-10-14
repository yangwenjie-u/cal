using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class TZC : BaseMethods
    {
        public void Calc()
        {
           
            var data = retData;        
            var MItem = data["M_TZC"];
            var SItem = data["S_TZC"];
            //var mrsDj = dataExtra["BZ_LJM_DJ"];
            bool mAllHg =true;
            var mitem = MItem[0];
            var jcxmBhg = "";
            var jcxmCur = "";

           
            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
            
                if (jcxm.Contains("、涂层厚度、"))
                {
                    jcxmCur = "涂层厚度";
                    int hggs=0;//合格个数
                    int dzhggs = 0;
                    double sum1 = 0,sum2=0,sum3=0;
                    //每处三个测点的涂层平均厚度不应小于设计厚度的85%，同一构件上的15个测点的平均厚度值不应小于设计厚度
                    //当对设计厚度无要求时涂层总厚度室外应为150um，室内 125 um，允许偏差为-25
                    if (sitem["TCSJHD"] == "")//涂层设计厚度
                    {
                        if (sitem["TCSYHJ"] == "室外")
                        {
                            if (GetSafeDouble(sitem["SYPJZ1"]) >= 125 && GetSafeDouble(sitem["SYPJZ1"]) <= 150) 
                            {
                                hggs = hggs + 1;
                            }
                            if (GetSafeDouble(sitem["SYPJZ2"]) >= 125 && GetSafeDouble(sitem["SYPJZ2"]) <= 150)
                            {
                                hggs = hggs + 1;
                            }
                            if (GetSafeDouble(sitem["SYPJZ3"]) >= 125 && GetSafeDouble(sitem["SYPJZ3"]) <= 150)
                            {
                                hggs = hggs + 1;
                            }
                            if (hggs == 3)
                            {
                                sitem["TCHDDXPD"] = "合格";
                            }
                            else
                            {
                                mAllHg = false;
                                sitem["TCHDDXPD"] = "不合格";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }

                        }
                        if (sitem["TCSYHJ"] == "室内")    
                        {
                            if (GetSafeDouble(sitem["SYPJZ1"]) >= 100 && GetSafeDouble(sitem["SYPJZ1"]) <= 125)
                            {
                                hggs = hggs + 1;
                            }
                            if (GetSafeDouble(sitem["SYPJZ2"]) >= 100 && GetSafeDouble(sitem["SYPJZ2"]) <= 125)
                            {
                                hggs = hggs + 1;
                            }
                            if (GetSafeDouble(sitem["SYPJZ3"]) >= 100 && GetSafeDouble(sitem["SYPJZ3"]) <= 125)
                            {
                                hggs = hggs + 1;
                            }
                            if (hggs == 3)
                            {
                                sitem["TCHDDXPD"] = "合格";
                            }
                            else
                            {
                                mAllHg = false;
                                sitem["TCHDDXPD"] = "不合格";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }

                        }

                    }
                    else
                    {
                        for (int i = 1; i < 16; i++)
                        {
                            if (GetSafeDouble(sitem["MZPJZ1_" + i]) <= GetSafeDouble(sitem["TCSJHD"]) * 0.85)
                            {
                                dzhggs = dzhggs + 1;
                                
                            }
                            for (int a = 1; a < 16; a++)
                            {
                                //求一组15个测点的平均值
                                sum1+= GetSafeDouble(sitem["TCSCHD1_" + a]);
                            }
                            for (int b = 1; b < 16; b++)
                            {
                                //求一组15个测点的平均值
                                sum2 += GetSafeDouble(sitem["TCSCHD2_" + b]);
                            }
                            for (int c = 1; c < 16; c++)
                            {
                                //求一组15个测点的平均值
                                sum3 += GetSafeDouble(sitem["TCSCHD3_" + c]);
                            }
                            if (sum1 / 15 >= GetSafeDouble(sitem["TCSJHD"]))
                            {
                                dzhggs = dzhggs + 1;
                            }
                            if (sum2 / 15 >= GetSafeDouble(sitem["TCSJHD"]))
                            {
                                dzhggs = dzhggs + 1;
                            }
                            if (sum3 / 15 >= GetSafeDouble(sitem["TCSJHD"]))
                            {
                                dzhggs = dzhggs + 1;
                            }
                            if (dzhggs == 4)
                            {
                                sitem["TCHDDXPD"] = "合格";
                            }
                            else
                            {
                                mAllHg = false;
                                sitem["TCHDDXPD"] = "不合格";
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            }


                        }                 
                    }



                }

                if (jcxm.Contains("附着性"))
                {
                    jcxmCur = "附着性";
                    if (sitem["FZXDXPD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }

                if (jcxm.Contains("韦氏硬度"))
                {
                    jcxmCur = "韦氏硬度";

                    if (GetSafeDouble(sitem["WSPJYLZ"]) > GetSafeDouble(sitem["G_WSYLZ"]))
                    {
                        sitem["WSDXPD"] = "合格";
                    }
                    else
                    {
                        mAllHg = false;
                        sitem["WSDXPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                
                }
                if (jcxm.Contains("镀锌层质量"))
                {
                    jcxmCur = "镀锌层质量";

                    if (GetSafeDouble(sitem["DXCZL"]) >= GetSafeDouble(sitem["G_DXCZL"]))
                    {
                        sitem["DXCZLDXPD"] = "合格";
                    }
                    else
                    {
                        mAllHg = false;
                        sitem["DXCZLDXPD"] = "不合格";
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
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') +"不符合要求";
            }



        }

    }
 }

