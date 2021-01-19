using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Calculates
{
    public class PFS : BaseMethods
    {
        public void Calc()
        {
            #region Code
            
            #region 字段定义
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItems = data["S_PFS"];
            var MItem = data["M_PFS"];
            var mrsDjs = dataExtra["BZ_FS_DJP"];
            var mAllHg = true;
            int mbhggs = 0;
            var jcxmBhg = "";
            var jcxmCur = "";
            var jcxm = "";
            var QBZZZD = "";
            if (!data.ContainsKey("M_PFS"))
            {
                data["M_PFS"] = new List<IDictionary<string, string>>();
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
               
                sItem["G_H_LSQD"] = mrsDj["G_HXLSQD"];
                sItem["G_Z_LSQD"] = mrsDj["G_ZXLSQD"];
               
                //sItem["G_NRX"] = mrsDj["G_NRX"];
                //sItem["G_DWRX"] = mrsDj["G_DWRX"];
                sItem["G_DWWZ"] = mrsDj["G_DWWZX"];
                sItem["G_BTSX"] = mrsDj["G_BTSX"];

                //更具qbzzzd，选择不同赋值
                if (QBZZZD == "P")
                {
                    sItem["G_H_MDSCL"] = mrsDj["G_HXZDLSCL"];
                    sItem["G_Z_MDSCL"] = mrsDj["G_ZXZDLSCL"];
                }else 
                {
                    sItem["G_H_MDSCL"] = mrsDj["G_HXDLSCL"];
                    sItem["G_Z_MDSCL"] = mrsDj["G_ZXDLSCL"];

                }
                
               
                #endregion

                #region 拉伸性能
                if (jcxm.Contains("、拉伸性能、")){
                   
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
                    #region 横向膜断伸长率
                    jcxmCur = "横向膜断伸长率";
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
                    #region 纵向膜断伸长率
                    jcxmCur = "纵向膜断伸长率";
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
                    sItem["G_H_SDLL"] = "----";
                    sItem["H_PJSDLL"] = "----";
                    sItem["HG_H_SDLL"] = "----";
                    sItem["G_Z_SDLL"] = "----";
                    sItem["Z_PJSDLL"] = "----";
                    sItem["HG_Z_SDLL"] = "----";
                }
                #endregion

               /* #region 耐热性  
                if (jcxm.Contains("、耐热性、")) {
                    jcxmCur = "耐热性";
                    int NRbhggs = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        if (sItem["NRXX" + i] != "无滑移、流淌、滴落")
                        {
                            NRbhggs = NRbhggs + 1;
                        }
                    }
                    if (NRbhggs > 0)
                    {
                        sItem["HG_NRX"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else { 
                        sItem["HG_NRX"] = "合格";
                        sItem["NG_NRXJCJG"] = "无滑移、流淌、滴落";
                    }

                }

                #endregion*/

                #region 不透水性  
                if (jcxm.Contains("、不透水性、"))
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
                    
                    else { sItem["HG_BTSX"] = "合格"; sItem["NG_BTSXJCJG"] = "不透水"; }

                }

                #endregion

                #region 低温弯折性 
                if (jcxm.Contains("、低温弯折性、") && sItem["QBZZZD"]!="PY")
                {
                    int SYGS = 0;
                    if (sItem["QBZZZD"] == "P")
                    {
                        SYGS = 2;
                    }
                    if (sItem["QBZZZD"] == "R")
                    {
                        SYGS = 4;
                    }
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
                        if (sItem["QBZZZD"] == "P") { sItem["NG_DWWZXJCJG"] = "主体材料无裂纹"; }
                        if (sItem["QBZZZD"] == "R") { sItem["NG_DWWZXJCJG"] = "主体材料和胶层无裂纹"; }

                    }

                }

                #endregion

               /* #region 低温柔性
                if (jcxm.Contains("、低温柔性、") && sItem["QBZZZD"] != "R"){
                    jcxmCur = "低温柔性";
                    int SYGS = 0;
                    int dwrxbhggs = 0;
                    if (sItem["QBZZZD"] == "PY")
                    {
                        SYGS = 10;
                        if(sItem["DWRXXX1"]!="无裂纹"|| sItem["DWRXXX2"] != "无裂纹") { dwrxbhggs = dwrxbhggs + 1; }
                        if (sItem["DWRXXX3"] != "无裂纹" || sItem["DWRXXX4"] != "无裂纹") { dwrxbhggs = dwrxbhggs + 1; }
                        if (sItem["DWRXXX5"] != "无裂纹" || sItem["DWRXXX6"] != "无裂纹") { dwrxbhggs = dwrxbhggs + 1; }
                        if (sItem["DWRXXX7"] != "无裂纹" || sItem["DWRXXX8"] != "无裂纹") { dwrxbhggs = dwrxbhggs + 1; }
                        if (sItem["DWRXXX9"] != "无裂纹" || sItem["DWRXXX10"] != "无裂纹") { dwrxbhggs = dwrxbhggs + 1; }
                        if( dwrxbhggs > 1)
                        {
                            sItem["HG_DWRX"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        else { sItem["HG_DWRX"] = "合格"; sItem["NG_DWRXJCJG"] = "无裂纹"; }

                    }
                    if (sItem["QBZZZD"] == "P")
                    {
                        SYGS = 5;
                        if (sItem["DWRXXX1"] != "无裂纹" ) { dwrxbhggs = dwrxbhggs + 1; }
                        if (sItem["DWRXXX2"] != "无裂纹" ) { dwrxbhggs = dwrxbhggs + 1; }
                        if (sItem["DWRXXX3"] != "无裂纹" ) { dwrxbhggs = dwrxbhggs + 1; }
                        if (sItem["DWRXXX4"] != "无裂纹" ) { dwrxbhggs = dwrxbhggs + 1; }
                        if (sItem["DWRXXX5"] != "无裂纹" ) { dwrxbhggs = dwrxbhggs + 1; }
                        if (dwrxbhggs > 1)
                        {
                            sItem["HG_DWRX"] = "不合格";
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                        else { sItem["HG_DWRX"] = "合格"; sItem["NG_DWRXJCJG"] = "胶层无裂纹"; }
                    }

                }
                #endregion*/
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
