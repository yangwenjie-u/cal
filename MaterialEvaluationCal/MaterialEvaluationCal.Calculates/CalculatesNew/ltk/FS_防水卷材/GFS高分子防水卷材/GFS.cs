using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Calculates
{
    public class GFS : BaseMethods
    {
        public void Calc()
        {
            #region Code
            
            #region 字段定义
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItems = data["S_GFS"];
            var MItem = data["M_GFS"];
            var mrsDjs = dataExtra["BZ_FS_DJGP"];
            var mAllHg = true;
            int mbhggs = 0;
            var jcxmBhg = "";
            var jcxmCur = "";
            var jcxm = "";
            var QBZZZD = "";
            if (!data.ContainsKey("M_GFS"))
            {
                data["M_GFS"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            #endregion
            #region 计算
            
            foreach (var sItem in SItems){
                QBZZZD = sItem["QBZZZD"];
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                #region 等级表数据
                var mrsDj = mrsDjs.FirstOrDefault(u => u["QBZZZD"] == sItem["QBZZZD"]); 
            
                if (null == mrsDj)
                {
                    mAllHg = false;
                    throw new Exception("未取得指标，标准值数据");
                }
               
                sItem["G_H_LSQD"] = mrsDj["G_CWLSQD"];
                sItem["G_Z_LSQD"] = mrsDj["G_CWLSQD"];
                sItem["G_H_MDSCL"] = mrsDj["G_CWLDSCL"];
                sItem["G_Z_MDSCL"] = mrsDj["G_CWLDSCL"];
               
                sItem["G_DWWZ"] = mrsDj["G_DWWZ"];
                sItem["G_BTSX"] = mrsDj["G_BTSX"];
                #endregion

                #region 拉伸
                if (jcxm.Contains("、拉伸强度、")){
                  
                    #region 横向拉伸强度
                    jcxmCur = "横向拉伸强度";
                    double HXLSQDSUM = 0;
                    for(int i = 1; i < 6; i++)
                    {
                        sItem["H_LSQD" + i] = Round(GetSafeDouble(sItem["H_LLFB"+i]) / GetSafeDouble(sItem["H_ZJKD"+i]) / GetSafeDouble(sItem["H_HDT"+i]), 1).ToString("0.0");
                        HXLSQDSUM = HXLSQDSUM + GetSafeDouble(sItem["H_LSQD" + i]);
                    }
                    sItem["H_PJLSQD"] = Round(HXLSQDSUM / 5, 1).ToString();
                    sItem["HG_H_LSQD"] = IsQualified(sItem["G_H_LSQD"], sItem["H_PJLSQD"], false);
                    if (sItem["HG_H_LSQD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion
                    #region 纵向拉伸强度
                    jcxmCur = "纵向拉伸强度";
                    double ZXLSQDSUM = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        sItem["Z_LSQD" + i] = Round(GetSafeDouble(sItem["Z_LLFB"+i]) / GetSafeDouble(sItem["Z_ZJKD"+i]) / GetSafeDouble(sItem["Z_HDT"+i]), 1).ToString("0.0");
                        ZXLSQDSUM = ZXLSQDSUM + GetSafeDouble(sItem["Z_LSQD" + i]);
                    }
                    sItem["Z_PJLSQD"] = Round(ZXLSQDSUM / 5, 1).ToString();
                    sItem["HG_Z_LSQD"] = IsQualified(sItem["G_Z_LSQD"], sItem["Z_PJLSQD"], false);
                    if (sItem["HG_Z_LSQD"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion
                    #region 横向拉断伸长率
                    jcxmCur = "横向拉断伸长率";
                    double HXMDSCLSUM=0;
                    for(int i=1;i<6;i++)
                    {
                        sItem["H_MDSCL" + i] = Round(GetSafeDouble(sItem["H_MDSCZ" + i]) / GetSafeDouble(sItem["H_CSBJ" + i])*100, 0).ToString();
                        HXMDSCLSUM = HXMDSCLSUM + GetSafeDouble( sItem["H_MDSCL" + i]);
                    }
                    sItem["H_PJMDSCL"] = Round(HXMDSCLSUM / 5, 0).ToString();
                    sItem["HG_H_MDSCL"] = IsQualified(sItem["G_H_MDSCL"], sItem["H_PJMDSCL"], false);
                    if (sItem["HG_H_MDSCL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion
                    #region 纵向拉断伸长率
                    jcxmCur = "纵向拉断伸长率";
                    double ZXMDSCLSUM = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        sItem["Z_MDSCL" + i] = Round(GetSafeDouble(sItem["Z_MDSCZ" + i]) / GetSafeDouble(sItem["Z_CSBJ" + i])*100, 0).ToString();
                        ZXMDSCLSUM = ZXMDSCLSUM + GetSafeDouble(sItem["Z_MDSCL" + i]);
                    }
                    sItem["Z_PJMDSCL"] = Round(ZXMDSCLSUM / 5, 0).ToString();
                    sItem["HG_Z_MDSCL"] = IsQualified(sItem["G_Z_MDSCL"], sItem["Z_PJMDSCL"], false);
                    if (sItem["HG_Z_MDSCL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion

                }
                else 
                { 
                    
                }
                #endregion

                #region 不透水性  
                if (jcxm.Contains("、不透水、"))
                {
                    jcxmCur = "不透水性";
                    int BTSXbhggs = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        if (sItem["BTSXX" + i] != "不透水")
                        {
                            BTSXbhggs = BTSXbhggs + 1;
                        }
                    }
                    if (BTSXbhggs > 0)
                    {
                        sItem["HG_BTSX"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    
                    else { sItem["HG_BTSX"] = "合格"; sItem["NG_BTSXJCJG"] = "无渗漏"; }

                }

                #endregion

                #region 低温弯折性 
                if (jcxm.Contains("、低温弯折、") )
                {
                    int SYGS = 4;
                   
                    jcxmCur = "低温弯折性";
                    int DWWZbhggs = 0;
                    if (SYGS != 0) {
                        for (int i = 1; i < SYGS + 1; i++)
                        {
                            if (sItem["DWWZXX" + i] != "无裂纹")
                            {
                                DWWZbhggs = DWWZbhggs + 1;
                            }
                        }
                    }
                    
                    if (DWWZbhggs > 0)
                    {
                        sItem["HG_DWWZ"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else { 
                        sItem["HG_DWWZ"] = "合格";
                        sItem["NG_DWWZXJCJG"] = "无裂纹"; 
                    }

                }

                #endregion

               
            }
            #endregion

            #region 添加最终报告
            //综合判断
            if (mAllHg)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"]+"中"+QBZZZD +"类型"+ "的规定，所检项目均符合要求。";
            }
            if (!mAllHg)
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "中" + QBZZZD + "类型" + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                 MItem[0]["JCJGMS"] = "依据标准" + MItem[0]["PDBZ"] + ",所检项目" + jcxmBhg.TrimEnd('、') + "不符合标准要求。";
            }
            #endregion
            #endregion
        }
    }
}
