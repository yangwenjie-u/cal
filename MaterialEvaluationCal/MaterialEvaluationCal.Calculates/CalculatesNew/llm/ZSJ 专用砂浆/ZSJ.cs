using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class ZSJ : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_ZSJ"];
            var MItem = data["M_ZSJ"];
            var mrsDj = dataExtra["BZ_ZSJ_DJ"];
            
            if (!data.ContainsKey("M_ZSJ"))
            {
                data["M_ZSJ"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            bool sign = false;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var sjlx = sItem["SJLX"];
                var mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == sItem["SJLX"].Trim() && u["SZ"] == sItem["SJDJ"].Trim());
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    if (sItem["SJLX"] == "蒸压加气混凝土用薄层砌筑砂浆" || sItem["SJLX"] == "蒸压加气混凝土用抹灰砂浆")
                    {
                        sItem["G_KYQD"] = mrsdj["SJDJ"].Trim();//从等级表中获取抗拉强度标准值
                        sItem["G_BSL"] = mrsdj["BSL"].Trim();//保水率
                        sItem["G_NJQD"] = mrsdj["NJQD"].Trim();//粘结强度
                    }
                    if (sItem["SJLX"].Contains("蒸压加气混凝土用界面砂浆"))
                    {
                        
                        sItem["G_BSL"] = mrsdj["SJDJ"].Trim();//保水率
                        sItem["G_NJQD"] = mrsdj["NJQD"].Trim();//粘结强度
                    }
                    if (sItem["SJLX"].Contains("抹灰石膏"))
                    {
                        sItem["G_KYQD"] = mrsdj["SJDJ"].Trim();//从等级表中获取抗拉强度标准值
                        sItem["G_BSL"] = mrsdj["BSL"].Trim();//保水率
                        sItem["G_NJQD"] = mrsdj["NJQD"].Trim();//粘结强度
                        sItem["G_KZQD"] = mrsdj["KZQD"].Trim();//抗折强度
                    }

                }
                else
                {
                    mAllHg = false;
                    sItem["JCJG"] = "依据不详";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }



                #region 抗压强度
                sign = true;
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";

                    #region 抹灰石膏
                    if (sItem["SJLX"].Contains("抹灰石膏"))//如果是抹灰石膏类的用一种标准
                    {
                        List<double> KYQD = new List<double>();
                        //Double[] KYQD = new double[3];
                        double sum = 0 ,pjmd = 0;
                        for (int xd = 0; xd < 3; xd++)//将数值赋值到数组中，并求出总和
                        {
                            sum = sum + GetSafeDouble(sItem["KYQD2_" + (xd + 1)]);
                            //KYQD[xd] = GetSafeDouble(sItem["KYQD2_" + (xd + 1)]);
                            KYQD.Add(GetSafeDouble(sItem["KYQD2_" + (xd + 1)]));
                        }
                        pjmd = sum / 3;//求平均值
                        
                        KYQD.Sort();//将三个数进行排序

                        if ((pjmd - KYQD[0]) > pjmd * 0.15 && (KYQD[2] - pjmd) <= pjmd * 0.15) //最小值超过平均值15%，最大值不超过15%
                        {
                            pjmd = Round((KYQD[1] + KYQD[2]), 1) / 2;
                        }
                        if ((pjmd - KYQD[0]) <= pjmd * 0.15 && (KYQD[2] - pjmd) > pjmd * 0.15)//最大值超过平均值15%，最小值不超过15%
                        {
                            
                            pjmd = Round((KYQD[1] + KYQD[0]),1) / 2;
                            
                        }
                        if ((pjmd - KYQD[0]) > pjmd * 0.15 && (KYQD[2] - pjmd) > pjmd * 0.15)//最大值超过平均值15%，最小值超过15%
                        {
                            sItem["HG_KYQD"] = "该组试验结果无效";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "该组试验结果无效";
                            mAllHg = false;
                        }
                        if ((pjmd - KYQD[0]) <= pjmd * 0.15 && (KYQD[2] - pjmd) <= pjmd * 0.15)//最大值不超过平均值15%，最小值不超过15%
                        {

                            pjmd = sum / 3;
                            pjmd = Round(Round(pjmd * 2, 1) / 2,2);
                            
                        }
                        sItem["KYPJ2_1"] = pjmd.ToString("0.00");

                        if(GetSafeDouble(sItem["KYPJ2_1"]) >= GetSafeDouble(sItem["G_KYQD"]))
                        {
                            sItem["HG_KYQD"] = "合格";
                            sItem["JCJG"] = "合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                            mAllHg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_KYQD"] = "不合格";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                            mAllHg = false;
                        }
                        sItem["G_KYQD"] = "≥" + sItem["G_KYQD"];

                    }
                    #endregion

                    #region 其他类型
                    else
                    {
                        
                        double max, mid, min;

                        // 对3个抗压值进行排序
                        List<double> KYQD = new List<double>(); 

                        KYQD.Add(GetSafeDouble(sItem["KYQD1"]));
                        KYQD.Add(GetSafeDouble(sItem["KYQD2"]));
                        KYQD.Add(GetSafeDouble(sItem["KYQD3"]));

                        KYQD.Sort();

                        max = KYQD[2];
                        mid = KYQD[1];
                        min = KYQD[0];

                        //若最大或最小值与平均值相差15%，
                        if (max > mid * 1.15 || min < mid * 0.85)
                        {
                            sItem["KYQD"] = mid.ToString("0.0");//舍去最大最小值，强度值为中间值

                        }
                        else
                        {
                            sItem["KYQD"] = sItem["KYPJ"];//强度值为平均值
                        }


                        if (max > mid * 1.15 && min < mid * 0.85)//若最大最小值都和中间值相差15%则结果无效
                        {
                            sItem["HG_KYQD"] = "该组试验结果无效";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "该组试验结果无效";
                            mAllHg = false;
                        }
                        else if (GetSafeDouble(sItem["KYQD"]) >= GetSafeDouble(sItem["G_KYQD"]))//判定是否合格
                        {
                            sItem["HG_KYQD"] = "合格";
                            sItem["JCJG"] = "合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                            mAllHg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_KYQD"] = "不合格";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                            mAllHg = false;
                        }
                        sItem["G_KYQD"] = "≥" + sItem["G_KYQD"];

                    }
                    #endregion
                }
                else
                {
                    sItem["HG_KYQD"] = "----";
                    sItem["G_KYQD"] = "----";
                    sItem["KYPJ2_1"] = "----";
                    sItem["KYQD"] = "----";
                }

               
                #endregion

                #region 保水率
                sign = true;
                if (jcxm.Contains("、保水率、"))
                {
                    jcxmCur = "保水率";

                    #region 抹灰石膏
                    if (sItem["SJLX"].Contains("抹灰石膏"))
                    { 
                        if(Math.Abs(GetSafeDouble(sItem["BSL2_1"]) - GetSafeDouble(sItem["PJBSL2_1"])) > (GetSafeDouble(sItem["PJBSL2_1"]) * 0.03) || Math.Abs(GetSafeDouble(sItem["BSL2_2"]) - GetSafeDouble(sItem["PJBSL2_1"])) > (GetSafeDouble(sItem["PJBSL2_1"]) * 0.03))
                        {
                            sItem["HG_BSL"] = "该组试验结果无效";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "该组试验结果无效";
                            mAllHg = false;
                        }
                        else if(GetSafeDouble(sItem["PJBSL2_1"])>= GetSafeDouble(sItem["G_BSL"]))
                        {
                            sItem["HG_BSL"] = "合格";
                            sItem["JCJG"] = "合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                            mAllHg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_BSL"] = "不合格";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                            mAllHg = false;
                        }
                        sItem["G_BSL"] = "≥" + sItem["G_BSL"];
                    }
                    #endregion

                    #region 其他类型
                    else
                    { 
                        if (Math.Abs(GetSafeDouble(sItem["BSL1"]) - GetSafeDouble(sItem["PJBSL"])) > (GetSafeDouble(sItem["PJBSL"]) * 0.05) || Math.Abs(GetSafeDouble(sItem["BSL2"]) - GetSafeDouble(sItem["PJBSL"])) > (GetSafeDouble(sItem["PJBSL"]) * 0.05))//若最大最小值都和中间值相差15%则结果无效
                        {
                            sItem["HG_BSL"] = "该组试验结果无效";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "该组试验结果无效";
                            mAllHg = false;
                        }
                        else if (GetSafeDouble(sItem["PJBSL"]) >= GetSafeDouble(sItem["G_BSL"]))//判定是否合格
                        {
                            sItem["HG_BSL"] = "合格";
                            sItem["JCJG"] = "合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                            mAllHg = true;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["HG_BSL"] = "不合格";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                            mAllHg = false;
                        }
                        sItem["G_BSL"] = "≥" + sItem["G_BSL"];
                    }
                    #endregion
                }
                else
                {
                    sItem["HG_BSL"] = "----";
                    sItem["G_BSL"] = "----";
                    sItem["PJBSL2_1"] = "----";
                    sItem["PJBSL"] = "----";
                }
                #endregion

                #region 14d拉伸粘结强度
                
                if (jcxm.Contains("、14d拉伸粘结强度、"))
                {
                    jcxmCur = "14d拉伸粘结强度";

                    double sum = 0,pjmd = 0, temp = 0;
                    sign = true;

                    List<double> NJQD = new List<double>(); ;

                    for (int xd = 0; xd < 6; xd++)//将数值赋值到数组中，并求出总和
                    { 
                         sum = sum + GetSafeDouble(sItem["NJQD" + (xd + 1)]);
                        //NJQD[xd] = GetSafeDouble(sItem["NJQD" + (xd + 1)]);
                        NJQD.Add(GetSafeDouble(sItem["NJQD" + (xd + 1)]));
                    }
                    pjmd = Round(sum / 6, 2);//求平均值

                    for (int xd = 0; xd < 5; xd++)//求出偏差值最大的，赋值给NJQD[5]
                    {
                        if (Math.Abs(NJQD[xd] - pjmd) > Math.Abs(NJQD[5] - pjmd))
                        {
                            temp = NJQD[xd];
                            NJQD[xd] = NJQD[5];
                            NJQD[5] = temp;
                        }

                    }

                    if (Math.Abs(NJQD[5] - pjmd) > pjmd * 0.2)//若偏差值最大的实验值与平均值相差20%，则将改数值舍去
                    {
                        sum = 0;
                        for (int xd = 0; xd < 5; xd++)
                        {
                            sum = sum + NJQD[xd];
                        }
                        pjmd = Round(sum / 5, 2);
                        for (int xd = 0; xd < 4; xd++)
                        {
                            if (Math.Abs(NJQD[xd] - pjmd) > Math.Abs(NJQD[4] - pjmd))
                            {
                                temp = NJQD[xd];
                                NJQD[xd] = NJQD[4];
                                NJQD[4] = temp;
                            }
                        }
                        if (Math.Abs(NJQD[4] - pjmd) > pjmd * 0.2)
                        {

                            sum = 0;
                            for (int xd = 0; xd < 4; xd++)
                            {
                                sum = sum + NJQD[xd];
                            }
                            pjmd = Round(sum / 4, 2);
                            for (int xd = 0; xd < 3; xd++)
                            {
                                if (Math.Abs(NJQD[xd] - pjmd) > Math.Abs(NJQD[3] - pjmd))
                                {
                                    temp = NJQD[xd];
                                    NJQD[xd] = NJQD[3];
                                    NJQD[3] = temp;
                                }
                            }
                            if (Math.Abs(NJQD[3] - pjmd) > pjmd * 0.2)
                            {
                                sItem["HG_NJQD"] = "该组试验结果无效";
                                sItem["JCJG"] = "不合格";
                                jsbeizhu = "该组试验结果无效";
                                sign = false;
                            }  
                            
                        }
                        
                    }
                    sItem["NJPJ"] = pjmd.ToString("0.00");

                    if (sign == true &&  GetSafeDouble(sItem["NJPJ"]) >= GetSafeDouble(sItem["G_NJQD"]))//判定是否合格
                    {
                        sItem["HG_NJQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;
                    }
                    else if (sign == true && GetSafeDouble(sItem["NJPJ"]) < GetSafeDouble(sItem["G_NJQD"]))
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_NJQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }
                    sItem["G_NJQD"] = "≥" + sItem["G_NJQD"];

                }
                else
                {
                    sItem["HG_NJQD"] = "----";
                    sItem["G_NJQD"] = "----";
                    sItem["NJPJ"] = "----";

                }
                #endregion

                #region 抗折强度
                sign = true;
                if (jcxm.Contains("、抗折强度、"))
                {
                    jcxmCur = "抗折强度";

                    #region 抹灰石膏
                    List<double> KZQD = new List<double>(); 
                    double sum = 0, pjmd = 0;
                    for (int xd = 0; xd < 3; xd++)//将数值赋值到数组中，并求出总和
                    {
                        sum = sum + GetSafeDouble(sItem["KZQD2_" + (xd + 1)]);
                        //KZQD[xd] = GetSafeDouble(sItem["KZQD2_" + (xd + 1)]);
                        KZQD.Add(GetSafeDouble(sItem["KZQD2_" + (xd + 1)]));
                    }
                    pjmd = sum / 3;//求平均值

                    KZQD.Sort();//将三个数进行排序

                    if ((pjmd - KZQD[0]) > pjmd * 0.15 && (KZQD[2] - pjmd) <= pjmd * 0.15) //最小值超过平均值15%，最大值不超过15%
                    {
                        pjmd = Round((KZQD[1] + KZQD[2]), 1) / 2;
                    }
                    if ((pjmd - KZQD[0]) <= pjmd * 0.15 && (KZQD[2] - pjmd) > pjmd * 0.15)//最大值超过平均值15%，最小值不超过15%
                    {

                        pjmd = Round((KZQD[1] + KZQD[0]), 1) / 2;

                    }
                    if ((pjmd - KZQD[0]) > pjmd * 0.15 && (KZQD[2] - pjmd) > pjmd * 0.15)//最大值超过平均值15%，最小值超过15%
                    {
                        sItem["HG_KZQD"] = "该组试验结果无效";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "该组试验结果无效";
                        mAllHg = false;
                    }
                    if ((pjmd - KZQD[0]) <= pjmd * 0.15 && (KZQD[2] - pjmd) <= pjmd * 0.15)//最大值不超过平均值15%，最小值不超过15%
                    {

                        pjmd = sum / 3;
                        pjmd =Round(Round(pjmd * 2, 1)/2,2);//修约到0.05

                    }
                    sItem["KZPJ2_1"] = pjmd.ToString("0.00");

                    if (GetSafeDouble(sItem["KZPJ2_1"]) >= GetSafeDouble(sItem["G_KZQD"]))
                    {
                        sItem["HG_KZQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                        mAllHg = true;
                    }
                    else
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        sItem["HG_KZQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                        mAllHg = false;
                    }
                    sItem["G_KZQD"] = "≥" + sItem["G_KZQD"];

                }
                #endregion

                else
                {
                    sItem["HG_KZQD"] = "----";
                    sItem["G_KZQD"] = "----";
                    sItem["KZPJ2_1"] = "----";
                }
                #endregion

            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_ZSJ"))
            {
                data["M_ZSJ"] = new List<IDictionary<string, string>>();
            }
            var M_ZSJ = data["M_ZSJ"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_ZSJ == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_ZSJ.Add(m);
            }
            else
            {
                M_ZSJ[0]["JCJG"] = mjcjg;
                M_ZSJ[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
