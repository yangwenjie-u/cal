using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Calculates
{
    public class UFS : BaseMethods
    {
        public void Calc()
        {
            #region Code
            
            #region 字段定义
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItems = data["S_UFS"];
            var MItem = data["M_UFS"];
            var mrsDjs = dataExtra["BZ_FS_DJSU"];
            var mAllHg = true;
            var QBZZZD = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            var jcxm = "";
            if (!data.ContainsKey("M_UFS"))
            {
                data["M_UFS"] = new List<IDictionary<string, string>>();
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
                sItem["G_H_ZGFLL"] = mrsDj["G_HXZDFLL"];
                sItem["G_Z_ZGFLL"] = mrsDj["G_ZXZDFLL"];

                sItem["G_H_CGFLL"] = mrsDj["G_HXCGFLL"];
                sItem["G_Z_CGFLL"] = mrsDj["G_ZXCGFLL"];

                sItem["G_H_ZDFSCL"]= mrsDj["G_HXZDFYSL"];
                sItem["G_Z_ZDFSCL"] = mrsDj["G_ZXZDFYSL"];

                sItem["G_H_DRFSCL"] = mrsDj["G_HXDRFYSL"];
                sItem["G_Z_DRFSCL"] = mrsDj["G_ZXDRFYSL"];

                sItem["G_NRX"] = mrsDj["G_NRX"];
                sItem["G_DWRX"] = mrsDj["G_DWRX"];

                sItem["G_BTSX"] = mrsDj["G_BTSX"];
                #endregion

                #region 拉力
                if (jcxm.Contains("、拉力、")){
                   
                    #region 最大峰横向拉力（N/50mm）
                    jcxmCur = "最大峰横向拉力（N/50mm）";
                    double HXzdfLlSUM =0;
                    for(int i = 1; i<6; i++)
                    {
                        HXzdfLlSUM = GetSafeDouble( sItem["H_ZDFLL" + i]) + HXzdfLlSUM;
                    }
                    sItem["H_PJZGFLL"] = Round(HXzdfLlSUM / 5, 0).ToString();
                    sItem["HG_H_ZGFLL"] = IsQualified(sItem["G_H_ZGFLL"], sItem["H_PJZGFLL"], false);
                    if (sItem["HG_H_ZGFLL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion
                    #region 最大峰纵向拉力（N/50mm）
                    jcxmCur = "最大峰纵向拉力（N/50mm）";
                    double ZXzdfLlSUM = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        ZXzdfLlSUM = GetSafeDouble(sItem["Z_ZDFLL" + i]) + ZXzdfLlSUM;
                    }
                    sItem["Z_PJZGFLL"] = Round(ZXzdfLlSUM / 5, 0).ToString();
                    sItem["HG_Z_ZGFLL"] = IsQualified(sItem["G_Z_ZGFLL"], sItem["Z_PJZGFLL"], false);
                    if (sItem["HG_Z_ZGFLL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion

                    #region 次高峰横向拉力（N/50mm）
                    jcxmCur = "次高峰横向拉力（N/50mm）";
                    double HXcgfLlSUM = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        HXcgfLlSUM = GetSafeDouble(sItem["H_CGFLL" + i]) + HXcgfLlSUM;
                        if(GetSafeDouble(sItem["H_CGFLL" + i])==0|| sItem["H_CGFLL" + i] == null)
                        {
                            sItem["H_CGFLL" + i] = "----";
                        }
                    }
                    sItem["H_PJCGFLL"] = Round(HXcgfLlSUM / 5, 0).ToString();
                    if(HXcgfLlSUM==0|| sItem["H_PJCGFLL"]==null)
                    {
                        sItem["H_PJCGFLL"] = "----";
                    }
                    sItem["HG_H_CGFLL"] = IsQualified(sItem["G_H_CGFLL"], sItem["H_PJCGFLL"], false);
                    if (sItem["HG_H_CGFLL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion
                    #region 次高峰纵向拉力（N/50mm）
                    jcxmCur = "次高峰纵向拉力（N/50mm）";
                    double ZXcgfLlSUM = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        ZXcgfLlSUM = GetSafeDouble(sItem["Z_CGFLL" + i]) + ZXcgfLlSUM;
                        if (GetSafeDouble(sItem["Z_CGFLL" + i]) == 0 || sItem["H_Z_CGFLLGFLL" + i] == null)
                        {
                            sItem["Z_CGFLL" + i] = "----";
                        }
                    }
                    sItem["Z_PJCGFLL"] = Round(ZXcgfLlSUM / 5, 0).ToString();
                    if (ZXcgfLlSUM == 0 || sItem["Z_PJCGFLL"] == null)
                    {
                        sItem["Z_PJCGFLL"] = "----";
                    }
                    sItem["HG_Z_CGFLL"] = IsQualified(sItem["G_Z_CGFLL"], sItem["Z_PJCGFLL"], false);
                    if (sItem["HG_Z_CGFLL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion


                    #region 横向拉伸现象
                    jcxmCur = "横向拉伸现象";
                    int HXbhggs = 0;
                    for(int i = 1; i < 6; i++)
                    {
                        if (sItem["H_LSXX" + i] != "无分离")
                        {
                            HXbhggs = HXbhggs + 1;
                        }
                    }
                    if (HXbhggs > 0)
                    {
                        sItem["HG_H_LSXX"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else { sItem["HG_H_LSXX"] = "合格"; sItem["NG_HXLSXXJCJG"] = "无沥青涂盖层开裂或与胎基分离现象"; }
                    #endregion
                    #region 纵向拉伸现象
                    jcxmCur = "纵向拉伸现象";
                    int ZXbhggs = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        if (sItem["Z_LSXX" + i] != "无分离")
                        {
                           ZXbhggs = ZXbhggs + 1;
                        }
                    }
                    if (ZXbhggs > 0)
                    {
                        sItem["HG_Z_LSXX"] = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else { sItem["HG_Z_LSXX"] = "合格"; sItem["NG_ZXLSXXJCJG"] = "无沥青涂盖层开裂或与胎基分离现象"; }
                    #endregion
                }
                else 
                { 
                    
                }
                #endregion
                
                #region 延伸率
                if (jcxm.Contains("、延伸率、"))
                {

                    #region 最大峰时横向延伸率
                    jcxmCur = "最大峰时横向延伸率";
                    double HXzdfSclSUM = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        sItem["H_ZDFSCL" + i] = Round(GetSafeDouble(sItem["H_ZDFSCZ"+i]) / GetSafeDouble(sItem["H_CSBJ" + i])*100, 0).ToString();
                        HXzdfSclSUM = GetSafeDouble(sItem["H_ZDFSCL" + i]) + HXzdfSclSUM;
                    }
                    sItem["H_PJZDFSCL"] = Round(HXzdfSclSUM / 5, 0).ToString();
                    sItem["HG_H_ZDFSCL"] = IsQualified(sItem["G_H_ZDFSCL"], sItem["H_PJZDFSCL"], false);
                    if (sItem["HG_H_ZDFSCL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion
                    #region 最大峰时纵向延伸率
                    jcxmCur = "最大峰时纵向延伸率";
                    double ZXzdfSclSUM = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        sItem["Z_ZDFSCL" + i] = Round(GetSafeDouble(sItem["Z_ZDFSCZ"+i]) / GetSafeDouble(sItem["Z_CSBJ" + i])*100, 0).ToString();

                        ZXzdfSclSUM = GetSafeDouble(sItem["Z_ZDFSCL" + i]) + ZXzdfSclSUM;
                    }
                    sItem["Z_PJZDFSCL"] = Round(ZXzdfSclSUM / 5, 0).ToString();
                    sItem["HG_Z_ZDFSCL"] = IsQualified(sItem["G_Z_ZDFSCL"], sItem["Z_PJZDFSCL"], false);
                    if (sItem["HG_Z_ZDFSCL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion

                    #region 第二峰时横向延伸率
                    jcxmCur = "第二峰时横向延伸率";
                    double HXzdRfSclSUM = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        sItem["H_DRFSCL" + i] = Round(GetSafeDouble(sItem["H_DRFSCZ"+i]) / GetSafeDouble(sItem["H_CSBJ" + i]) * 100, 0).ToString();

                        HXzdRfSclSUM = GetSafeDouble(sItem["H_DRFSCL" + i]) + HXzdRfSclSUM;
                        if (sItem["H_DRFSCZ" + i] == null)
                        {
                            sItem["H_DRFSCL" + i] = "----";
                        }
                    }
                    sItem["H_PJDRFSCL"] = Round(HXzdRfSclSUM / 5, 0).ToString();
                    if(HXzdRfSclSUM==0|| sItem["H_PJDRFSCL"] == null)
                    {
                        sItem["H_PJDRFSCL"] = "----";
                    }
                    sItem["HG_H_DRFSCL"] = IsQualified(sItem["G_H_DRFSCL"], sItem["H_PJDRFSCL"], false);
                    if (sItem["HG_H_DRFSCL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion
                    #region 第二峰时纵向延伸率
                    jcxmCur = "第二峰时纵向延伸率";
                    double ZXzdRfSclSUM = 0;
                    for (int i = 1; i < 6; i++)
                    {
                        sItem["Z_DRFSCL" + i] = Round(GetSafeDouble(sItem["Z_DRFSCZ"+i]) / GetSafeDouble(sItem["Z_CSBJ" + i]) * 100, 0).ToString();

                        ZXzdRfSclSUM = GetSafeDouble(sItem["Z_DRFSCL" + i]) + ZXzdRfSclSUM;
                        if (sItem["Z_DRFSCL" + i] == null)
                        {
                            sItem["Z_DRFSCL" + i] = "----";
                        }
                    }
                    sItem["Z_PJDRFSCL"] = Round(ZXzdRfSclSUM / 5, 0).ToString();
                    if (ZXzdRfSclSUM == 0 || sItem["Z_PJDRFSCL"] == null)
                    {
                        sItem["Z_PJDRFSCL"] = "----";
                    }
                    sItem["HG_Z_DRFSCL"] = IsQualified(sItem["G_Z_DRFSCL"], sItem["Z_PJDRFSCL"], false);
                    if (sItem["HG_Z_DRFSCL"] == "不合格")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    #endregion

                }
                #endregion
                
                #region 耐热性  
                if (jcxm.Contains("、耐热性、")) {
                    jcxmCur = "耐热性";
                    int NRbhggs = 0;
                    List<double> HDJLS = new List<double>();
                    HDJLS.Add(GetSafeDouble(sItem["HDJLS1"]));
                    HDJLS.Add(GetSafeDouble(sItem["HDJLS2"]));
                    HDJLS.Add(GetSafeDouble(sItem["HDJLS3"]));
                    var SPJ=Round( HDJLS.Average(),1);
                    sItem["PJHDJLS"] = SPJ.ToString();
                    List<double> HDJLX = new List<double>();
                    HDJLX.Add(GetSafeDouble(sItem["HDJLS1"]));
                    HDJLX.Add(GetSafeDouble(sItem["HDJLS2"]));
                    HDJLX.Add(GetSafeDouble(sItem["HDJLS3"]));
                    var XPJ=Round( HDJLX.Average(),1);
                    sItem["PJHDJLX"] = XPJ.ToString();
                    for (int i = 1; i < 4; i++)
                    {
                        if (sItem["HDXXS" + i] != "无流淌、滴落" || SPJ>2 || (sItem["HDXXX" + i]) != "无流淌、滴落" || XPJ > 2)
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
                        sItem["NG_NRXJCJG"] = "无流淌、滴落";
                    }

                }

                #endregion

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

                #region 低温柔性
                if (jcxm.Contains("、低温柔性、") ){
                    jcxmCur = "低温柔性";
                    
                    int dwrxbhggs = 0;
                    
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
                #endregion

                #region 标准值----，数据----
                #endregion
            }
            #endregion

            #region 添加最终报告
            //综合判断
            if (mAllHg != false)
            {
                MItem[0]["JCJG"] = "合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"]+ "中" + QBZZZD + "的规定，所检项目均符合要求。";
            }
            else
            {
                MItem[0]["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"]+ "中" + QBZZZD + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
             
            }
            #endregion
            #endregion
        }
    }
}
