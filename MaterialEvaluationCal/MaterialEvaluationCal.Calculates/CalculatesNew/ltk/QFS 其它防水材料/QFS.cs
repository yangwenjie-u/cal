using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class QFS : BaseMethods
    {
        public void Calc()
        {
            #region Code

            #region 参数定义

            var extraDJ = dataExtra["BZ_QFS_DJ"];
            var data = retData;
            var mAllHg = true;
            var mjcjg = "不合格";
            
            var SItems = data["S_QFS"];
            var MItem = data["M_QFS"];
            var jcxm = "";
            var jcxmCur = "";
            var jcxmBhg = "";
            var jsbeizhu = "";
            var ggxh = "";

            #endregion

            //遍历从表数据
            foreach (var sItem in SItems)
            {
                //单条从表记录，合格不合格
                var sjcjg = "合格";
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                //需要增加if条件，根据不同产品，拼接字段
                if(sItem["CPMC"]== "止水带") {
                    ggxh = sItem["YT"] + "-" + sItem["JGXS"] + "-" + sItem["KD"] + "×" + sItem["HD"];
                }
                
                sItem["GGXH"] = ggxh;

                #region 获取等级表，指标标准值
                var mrsDj = extraDJ.FirstOrDefault(u => u["YT"] == sItem["YT"] && u["CPMC"] == sItem["CPMC"]);
                if (null == mrsDj)
                {
                    throw new Exception("获取指标标准值失败");
                }
                #region 将标准值从等级表赋值到S_QFS
                
                sItem["G_YD"] = mrsDj["G_YD"];
                sItem["G_CXWD"] = mrsDj["G_CXWD"];
                sItem["G_LSQD"] = mrsDj["G_LSQD"];
                sItem["G_LDSCL"] = mrsDj["G_LDSCL"];
                sItem["G_SLQD"] = mrsDj["G_SLQD"];
                if (sItem["BXWD"].Equals("70")) {
                    sItem["G_YSYJBX"] = mrsDj["G_YSYJBX7_24"];
                }
                else
                {
                    sItem["G_YSYJBX"] = mrsDj["G_YSYJBX23_168"];

                }


                #endregion
                #endregion

                #region 硬度
                if (jcxm.Contains("、硬度、"))
                {
                    jcxmCur = "硬度";
                    List<double> YD = new List<double>();
                    for(int i = 1; i < 6; i++)
                    {
                        YD.Add(GetSafeDouble(sItem["YD"+i]));
                    }
                    YD.Sort();
                    sItem["YDZZ"] = Round( YD[3],0).ToString("0");
                    sItem["HG_YD"] = IsQualified(sItem["G_YD"], sItem["YDZZ"], false);
                    if (sItem["HG_YD"] == "不合格")
                    {
                        sjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }

                }
                else
                {
                    sItem["YDZZ"] = "----";
                    sItem["G_YD"] = "----";
                    sItem["HG_YD"] = "----";
                }
                #endregion

                #region 脆性温度
                if (jcxm.Contains("、脆性温度、"))
                {
                    int n = 0;
                    jcxmCur = "脆性温度";
                    
                    for (int i = 1; i < 5; i++)
                    {
                        if (sItem["CXWDXX" + i]  == "有裂纹")
                        {
                            n = n + 1;
                        }
                       
                    }
                    if (n > 0)
                    {
                        sItem["HG_CXWD"] = "不合格";
                        sjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        sItem["HG_CXWD"] = "合格";
                    }
                   
                   

                }
                else
                {
                    for (int i = 1; i < 5; i++)
                    {
                        sItem["CXWDXX" + i] ="----";
                       

                    }
                    sItem["G_CXWD"] = "----";
                    sItem["HG_CXWD"] = "----";
                }
                #endregion
             
                #region 拉伸强度
                if (jcxm.Contains("、拉伸强度、"))
                {
                    double mj = 0;
                    int n = 0;
                    jcxmCur = "拉伸强度";
                    List<double> HDZZ  = new List<double>();
                    List<double> LSQD = new List<double>();

                    for (int i=1;i<6; i++) {
                        HDZZ.Add(GetSafeDouble(sItem["HDS1_"+i]));
                        HDZZ.Add(GetSafeDouble(sItem["HDZ1_"+i]));
                        HDZZ.Add(GetSafeDouble(sItem["HDX1_"+i]));
                        HDZZ.Sort();
                        sItem["HDZZ"+i] = Round(HDZZ[2], 0).ToString("0");
                        mj = GetSafeDouble(sItem["XZKD" + i]) * GetSafeDouble(sItem["HDZZ" + i]);
                        sItem["LSQD" + i] =Round( GetSafeDouble( sItem["ZDLL" + i] )/ mj,0).ToString("0");
                        LSQD.Add(GetSafeDouble(sItem["LSQD" + i]));
                        HDZZ.Clear();
                    }
                    LSQD.Sort();
                    sItem["LSQDZZ"] = LSQD[3].ToString("0");
                    sItem["HG_LSQD"] = IsQualified(sItem["G_LSQD"], sItem["LSQDZZ"], false);
                    if (sItem["HG_LSQD"] == "不合格")
                    {
                        sjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }


                }
                else
                {
                    sItem["LSQDZZ"] = "----";
                    sItem["G_LSQD"] = "----";
                    sItem["HG_LSQD"] = "----";
                }
                #endregion

                #region 拉断伸长率
                if (jcxm.Contains("、拉断伸长率、"))
                {
                   jcxmCur = "拉断伸长率";
                    List<double> LDSCL = new List<double>();
                    for (int i = 1; i < 6; i++)
                    {
                        sItem["LDSCL" + i] = Round((GetSafeDouble(sItem["DLSYCD" + i]) - GetSafeDouble(sItem["CSSYCD" + i])) / GetSafeDouble(sItem["CSSYCD" + i]),1).ToString();
                        LDSCL.Add(GetSafeDouble(sItem["LDSCL" + i]));
                    }
                    LDSCL.Sort();
                    sItem["LDSCLZZ"] = Round(LDSCL[3], 0).ToString("0");
                    
                    sItem["HG_LDSCL"] = IsQualified(sItem["G_LDSCL"], sItem["LDSCLZZ"], false);
                    if (sItem["HG_LDSCL"] == "不合格")
                    {
                        sjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }



                }
                else
                {
                    sItem["LDSCLZZ"] = "----";
                    sItem["G_LDSCL"] = "----";
                    sItem["HG_LDSCL"] = "----";
                }
                #endregion

                #region 撕裂强度
                if (jcxm.Contains("、撕裂强度、"))
                {
                    
                    int n = 0;
                    jcxmCur = "撕裂强度";
                    List<double> SLHDZZ = new List<double>();
                    List<double> SLQD = new List<double>();

                    for (int i = 1; i < 6; i++)
                    {
                        SLHDZZ.Add(GetSafeDouble(sItem["SLHDS" + i]));
                        SLHDZZ.Add(GetSafeDouble(sItem["SLHDZ" + i]));
                        SLHDZZ.Add(GetSafeDouble(sItem["SLHDX" + i]));
                        SLHDZZ.Sort();
                        sItem["SLHDZZ" + i] = Round(SLHDZZ[2], 0).ToString("0");
                        
                        sItem["SLQD" + i] = Round(GetSafeDouble(sItem["SLZDL" + i]) / GetSafeDouble(sItem["SLHDZZ" + i]), 1).ToString();
                        SLQD.Add(GetSafeDouble(sItem["SLQD" + i]));
                        SLHDZZ.Clear();
                    }
                    SLQD.Sort();
                    sItem["SLQDZZ"] = SLQD[3].ToString("0");
                    sItem["HG_SLQD"] = IsQualified(sItem["G_SLQD"], sItem["SLQDZZ"], false);
                    if (sItem["HG_SLQD"] == "不合格")
                    {
                        sjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }


                }
                else
                {
                    sItem["SLQDZZ"] = "----";
                    sItem["G_SLQD"] = "----";
                    sItem["HG_SLQD"] = "----";
                }
                #endregion

                #region 压缩永久变形
                if (jcxm.Contains("、压缩永久变形、"))
                {
                    jcxmCur = "压缩永久变形";
                    List<double> XSYJBX = new List<double>();

                    for (int i = 1; i < 4; i++)
                    {
                        sItem["YSYJBX" + i] = Round((GetSafeDouble(sItem["CSGD" + i]) - GetSafeDouble(sItem["HFHGD" + i])) / (GetSafeDouble(sItem["CSGD" + i]) - GetSafeDouble(sItem["XZQGD" + i])), 1).ToString();
                        XSYJBX.Add(GetSafeDouble(sItem["YSYJBX" + i]));
                    }
                    XSYJBX.Sort();
                    sItem["YSYJBXZZ"] = Round(XSYJBX[2], 0).ToString("0");
                    sItem["YSYJBXZZ"] = XSYJBX[2].ToString("0");
                    sItem["HG_YSYJBX"] = IsQualified(sItem["G_YSYJBX"], sItem["YSYJBXZZ"], false);
                    if (sItem["HG_YSYJBX"] == "不合格")
                    {
                        sjcjg = "不合格";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";

                    }


                }
                #endregion



            }


            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求	。";
            
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            #endregion

        }
    }
}
