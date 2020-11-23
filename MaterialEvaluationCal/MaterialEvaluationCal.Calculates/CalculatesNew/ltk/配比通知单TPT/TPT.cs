using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class TPT: BaseMethods
    {
        public void Calc()
        {
            #region Code
            /************************ 代码开始 *********************/
          
            #region 变量定义
          
            double[] mkyhzArray = new double[2];
            double[] mkyqdArray = new double [2];
    

            #endregion
           
            #region  集合取值
            
            var data = retData;
            var mrsDj = dataExtra["BZ_TPT_DJ"];
           
            // var mrsGg = dataExtra["BZ_TPT_Gg"];
            var MItem = data["M_TPT"];
            var SItem = data["S_TPT"];
            
            //砼合格证明书
            var THGMItem = data["M_THG"];
            var THGSItem = data["S_THG"];

            //质量卡
            var ZLKMItem = data["M_ZLK"];
            var ZLKSItem = data["S_ZLK"];
            
            //商砼开盘配合比
            var TKPSItem = data["S_TKP"];
            //var TKPMItem = data["M_TKP"];
            
            //商砼搅拌记录
            //var STJMItem = data["M_STJ"];
            var TJBSItem = data["S_TJB"];
           
            //商砼交货检验记录
            //var TJHMItem = data["M_TJH"];
            var TJHSItem = data["S_TJH"];

            //商砼抗压强度
            //var HGZMItem = data["M_HGZ"];
            var HGZSItem = data["S_HGZ"];
            
            var ZHGMItem = data["M_ZHG"];
            //var ZHGSItem = data["S_ZHG"];
            
            var MHGMItem = data["M_SHG"];
            //var ZHGSItem = data["S_ZHG"];

            #endregion

            #region 计算开始
            foreach (var sitem in SItem)
            {
                ZLKMItem[0]["QYZZZSBH"] = MItem[0]["QYZZZSBH"];
                ZLKMItem[0]["SYSHGZSBH"] = MItem[0]["SYSHGZSBH"];

                #region TPT 通知单

                double d_SHAHSL, d_SHIHSL, d_SHAHSHIL, d_T_CLSN, d_T_CLSHA, d_T_CLSHI, d_T_CLS, d_T_CLWJJ1, d_T_CLWJJ2, d_T_CLWJJ3, d_T_CLCHL1, d_T_CLCHL2, d_T_CLCHL3;
                //砂含水率
                d_SHAHSL = GetSafeDouble(sitem["SHAHSL"]);
                //石含水率
                d_SHIHSL= GetSafeDouble(sitem["SHIHSL"]);
                //砂含石率
                //d_SHAHSHIL= GetSafeDouble(sitem["SHAHSHIL"]);
                #region 每立方用量T_
              if (GetSafeDouble(sitem["T_CLSN"]) == 0)
                {
                    sitem["T_CLSN"] = "----";
                }
                if (GetSafeDouble(sitem["T_CLSHA"]) == 0)
                {
                    sitem["T_CLSHA"] = "----";
                }
                if (GetSafeDouble(sitem["T_CLSHI"]) == 0)
                {
                    sitem["T_CLSHI"] = "----";
                }
                if (GetSafeDouble(sitem["T_CLS"]) == 0)
                {
                    sitem["T_CLS"] = "----";
                }
                if (GetSafeDouble(sitem["T_CLWJJ1"]) == 0)
                {
                    sitem["T_CLWJJ1"] = "----";
                }
                if (GetSafeDouble(sitem["T_CLWJJ2"]) == 0)
                {
                    sitem["T_CLWJJ2"] = "----";
                }
                if (GetSafeDouble(sitem["T_CLWJJ3"]) == 0)
                {
                    sitem["T_CLWJJ3"] = "----";
                }
                if (GetSafeDouble(sitem["T_CLCHL1"]) == 0)
                {
                    sitem["T_CLCHL1"] = "----";
                }
                if (GetSafeDouble(sitem["T_CLCHL2"]) == 0)
                {
                    sitem["T_CLCHL2"] = "----";
                }
                if (GetSafeDouble(sitem["T_CLCHL3"]) == 0)
                {
                    sitem["T_CLCHL3"] = "----";
                }
                d_T_CLSN = GetSafeDouble(sitem["T_CLSN"]);
                d_T_CLSHA = GetSafeDouble(sitem["T_CLSHA"]);
                d_T_CLSHI = GetSafeDouble(sitem["T_CLSHI"]);
                d_T_CLS = GetSafeDouble(sitem["T_CLS"]);
                d_T_CLWJJ1 = GetSafeDouble(sitem["T_CLWJJ1"]);
                d_T_CLWJJ2 = GetSafeDouble(sitem["T_CLWJJ2"]);
                d_T_CLWJJ3 = GetSafeDouble(sitem["T_CLWJJ3"]);
                d_T_CLCHL1 = GetSafeDouble(sitem["T_CLCHL1"]);
                d_T_CLCHL2 = GetSafeDouble(sitem["T_CLCHL2"]);
                d_T_CLCHL3 = GetSafeDouble(sitem["T_CLCHL3"]);
                #endregion
                
                //计算出干的 石子+砂子的混合物质量
                double md1, md2, md;
                //d_T_CLSHA 每立方用量(砂子)
                //md = d_T_CLSHA * 100 / (100 - d_SHAHSHIL);
                md = d_T_CLSHA * (100 + d_SHAHSL) / 100;
               
             
                var d_H_CLSHA = Round(md, 0);
                //计算含石量
                //md1 = d_T_CLSHI;
                //md2 = (d_SHAHSHIL / 100) * d_T_CLSHA * 100 / (100 - d_SHAHSHIL);
                //干的石子质量
                //md = md1 - md2;
                //md = md * (100 + d_SHIHSL) / 100;
                //每立方用量(石)
                md = d_T_CLSHI * (100 + d_SHIHSL) / 100;
                var d_H_CLSHI = Round(md, 0);
                //每立方用量(水)
                var d_H_CLS = Round((d_T_CLS - d_T_CLSHA * d_SHAHSL / 100 - d_T_CLSHI * d_SHIHSL / 100), 0);
                #region XZL_校准量;H_调整;S_实际
                if (GetSafeDouble(sitem["XZL_SHUI"]) == 0)
                {
                    sitem["XZL_SHUI"] = (d_H_CLS - d_T_CLS).ToString();
                }
                if (GetSafeDouble(sitem["XZL_SHA"]) == 0)
                {
                    sitem["XZL_SHA"] = Round(d_H_CLSHA - d_T_CLSHA, 2).ToString();
                }
                if (GetSafeDouble(sitem["XZL_SI"]) == 0)
                {
                    sitem["XZL_SI"] = Round(d_H_CLSHI - d_T_CLSHI, 2).ToString();
                }
                sitem["H_CLSN"] = d_T_CLSN.ToString();
                sitem["H_CLSHA"] = Round(d_T_CLSHA + GetSafeDouble(sitem["XZL_SHA"]),0).ToString();
                sitem["H_CLSHI"] = Round(d_T_CLSHI + GetSafeDouble(sitem["XZL_SI"]), 0).ToString();
                sitem["H_CLS"] = Round(d_T_CLS + GetSafeDouble(sitem["XZL_SHUI"]), 0).ToString();

                if (d_T_CLWJJ1 == 0)
                {
                    sitem["H_CLWJJ1"] = "----";
                    
                }
                else { sitem["H_CLWJJ1"] = d_T_CLWJJ1.ToString(); }
                if (d_T_CLWJJ2 == 0)
                {
                    sitem["H_CLWJJ2"] = "----";
                    
                }
                else { sitem["H_CLWJJ2"] = d_T_CLWJJ2.ToString(); }
                if (d_T_CLWJJ3 == 0)
                {
                    sitem["H_CLWJJ3"] = "----";
                    
                }
                else { sitem["H_CLWJJ3"] = d_T_CLWJJ3.ToString(); }

                if (d_T_CLCHL1 == 0)
                {
                    sitem["H_CLCHL1"] = "----";
                }
                else { sitem["H_CLCHL1"] = d_T_CLCHL1.ToString(); }
                if (d_T_CLCHL2== 0)
                {
                    sitem["H_CLCHL2"] = "----";
                }
                else { sitem["H_CLCHL2"] = d_T_CLCHL2.ToString(); }
                if (d_T_CLCHL3 == 0)
                {
                    sitem["H_CLCHL3"] = "----";
                }
                else { sitem["H_CLCHL3"] = d_T_CLCHL3.ToString(); }

                if (GetSafeDouble(sitem["S_CLSN"]) == 0)
                {
                    sitem["S_CLSN"] = sitem["H_CLSN"];
                    sitem["S_CLSHA"] = sitem["H_CLSHA"];
                    sitem["S_CLSHI"] = sitem["H_CLSHI"];
                    sitem["S_CLS"] = sitem["H_CLS"];
                    sitem["S_CLWJJ1"] = sitem["H_CLWJJ1"];
                    sitem["S_CLWJJ2"] = sitem["H_CLWJJ2"];
                    sitem["S_CLWJJ3"] = sitem["H_CLWJJ3"];
                    sitem["S_CLCHL1"] = sitem["H_CLCHL1"];
                    sitem["S_CLCHL2"] = sitem["H_CLCHL2"];
                    sitem["S_CLCHL3"] = sitem["H_CLCHL3"];
                }
                #endregion
                sitem["SGTLD"]= sitem["TLD"]; 
                #endregion

                #region ZLK 质量卡
                ZLKSItem[0]["HNTBJ"] = sitem["HNTBJ"];
                ZLKSItem[0]["HPBBGBH"] = sitem["HPBBGBH"];
                ZLKSItem[0]["TLDSGYQ"] = sitem["TLD"]; 
                ZLKSItem[0]["TLDSJYQ"] = sitem["TLD"];
                ZLKMItem[0]["JZBW"] = sitem["JZBW"];
                ZLKSItem[0]["CCRQ"] = MItem[0]["ZLKCCRQ"];
                ZLKSItem[0]["SGGY"]= sitem["SGGY"];

                #endregion

                MHGMItem[0]["HPBBGBH"] = sitem["HPBBGBH"];
                MHGMItem[0]["JZBW"] = sitem["JZBW"];
              

                #region THG 合格证
                THGMItem[0]["JZBW"] = sitem["JZBW"];
                THGSItem[0]["LQ"] = sitem["LQ"];
                THGSItem[0]["SJDJ"] = sitem["SJDJ"];
                THGSItem[0]["HNTBJ"] = sitem["HNTBJ"];
                THGSItem[0]["BJWYQ"] = sitem["BJWYQ"];
                //配合比
                THGSItem[0]["HPBBGBH"] = sitem["HPBBGBH"];
                //外加剂
                THGSItem[0]["WJJ1MC"] = sitem["WJJ1MC"];
                THGSItem[0]["WJJ2MC"] = sitem["WJJ2MC"];
                THGSItem[0]["WJJ3MC"] = sitem["WJJ3MC"];
                THGSItem[0]["WJJ1ZL"] = sitem["WJJ1ZL"];
                THGSItem[0]["WJJ2ZL"] = sitem["WJJ2ZL"];
                THGSItem[0]["WJJ3ZL"] = sitem["WJJ3ZL"];
                THGSItem[0]["WJJ1BGBH"] = sitem["WJJ1BGBH"];
                THGSItem[0]["WJJ2BGBH"] = sitem["WJJ2BGBH"];
                THGSItem[0]["WJJ3BGBH"] = sitem["WJJ3BGBH"];
                //水泥
                THGSItem[0]["SNBGBH"] = sitem["SNBGBH"];
                THGSItem[0]["SNPZDJ"] = sitem["SNPZDJ"];
                //砂
                THGSItem[0]["SABGBH"] = sitem["SABGBH"];
                THGSItem[0]["SAGG"] = sitem["SAGG"];
                //石子
                THGSItem[0]["SZBGBH"] = sitem["SZBGBH"];
                THGSItem[0]["SZGG"] = sitem["SZGG"];
                //掺合料
                THGSItem[0]["CHL1BGBH"] = sitem["CHL1BGBH"];
                THGSItem[0]["CHL2BGBH"] = sitem["CHL2BGBH"];
                THGSItem[0]["CHL3BGBH"] = sitem["CHL3BGBH"];
                THGSItem[0]["CHL1MC"] = sitem["CHL1MC"];
                THGSItem[0]["CHL2MC"] = sitem["CHL3MC"];
                THGSItem[0]["CHL3MC"] = sitem["CHL3MC"];
                THGSItem[0]["CHL1ZL"] = sitem["CHL1ZL"];
                THGSItem[0]["CHL2ZL"] = sitem["CHL3ZL"];
                THGSItem[0]["CHL3ZL"] = sitem["CHL3ZL"];
                #endregion
                #region ZHG 商砼抗折
                ZHGMItem[0]["TLD"] = sitem["TLD"];
                ZHGMItem[0]["JZBW"] = sitem["JZBW"];
                #endregion


                #region 萧山暂不要

                #region TTZ 调整记录

                #endregion

                #region TJB
                TJBSItem[0]["SHAHSL"] = sitem["SHAHSL"];
                TJBSItem[0]["SHIHSL"] = sitem["SHIHSL"];
                TJBSItem[0]["SHAHSHIL"] = sitem["SHAHSHIL"];
                TJBSItem[0]["S_CLSN"] = sitem["S_CLSN"];
                TJBSItem[0]["H_CLSN"] = sitem["H_CLSN"];
                TJBSItem[0]["S_CLSHA"] = sitem["S_CLSHA"];
                TJBSItem[0]["H_CLSHA"] = sitem["H_CLSHA"];
                TJBSItem[0]["S_CLSHI"] = sitem["S_CLSHI"];
                TJBSItem[0]["H_CLSHI"] = sitem["H_CLSHI"];
                TJBSItem[0]["S_CLS"] = sitem["S_CLS"];
                TJBSItem[0]["H_CLS"] = sitem["H_CLS"];
                TJBSItem[0]["S_CLWJJ1"] = sitem["S_CLWJJ1"];
                TJBSItem[0]["H_CLWJJ1"] = sitem["H_CLWJJ1"];
                TJBSItem[0]["S_CLWJJ2"] = sitem["S_CLWJJ2"];
                TJBSItem[0]["H_CLWJJ2"] = sitem["H_CLWJJ2"];
                TJBSItem[0]["S_CLWJJ3"] = sitem["S_CLWJJ3"];
                TJBSItem[0]["H_CLWJJ3"] = sitem["H_CLWJJ3"];
                TJBSItem[0]["S_CLCHL1"] = sitem["S_CLCHL1"];
                TJBSItem[0]["H_CLCHL1"] = sitem["H_CLCHL1"];
                TJBSItem[0]["S_CLCHL2"] = sitem["S_CLCHL2"];
                TJBSItem[0]["H_CLCHL2"] = sitem["H_CLCHL2"];
                TJBSItem[0]["S_CLCHL3"] = sitem["S_CLCHL3"];
                TJBSItem[0]["H_CLCHL3"] = sitem["H_CLCHL3"];
                TJBSItem[0]["HPBBGBH"] = sitem["HPBBGBH"];
                #endregion

                #region TJH
                TJHSItem[0]["SHAHSL"] = sitem["SHAHSL"];
                TJHSItem[0]["SHIHSL"] = sitem["SHIHSL"];
                TJHSItem[0]["SHAHSHIL"] = sitem["SHAHSHIL"];
                TJHSItem[0]["S_CLSN"] = sitem["S_CLSN"];
                TJHSItem[0]["H_CLSN"] = sitem["H_CLSN"];
                TJHSItem[0]["S_CLSHA"] = sitem["S_CLSHA"];
                TJHSItem[0]["H_CLSHA"] = sitem["H_CLSHA"];
                TJHSItem[0]["S_CLSHI"] = sitem["S_CLSHI"];
                TJHSItem[0]["H_CLSHI"] = sitem["H_CLSHI"];
                TJHSItem[0]["S_CLS"] = sitem["S_CLS"];
                TJHSItem[0]["H_CLS"] = sitem["H_CLS"];
                TJHSItem[0]["S_CLWJJ1"] = sitem["S_CLWJJ1"];
                TJHSItem[0]["H_CLWJJ1"] = sitem["H_CLWJJ1"];
                TJHSItem[0]["S_CLWJJ2"] = sitem["S_CLWJJ2"];
                TJHSItem[0]["H_CLWJJ2"] = sitem["H_CLWJJ2"];
                TJHSItem[0]["S_CLWJJ3"] = sitem["S_CLWJJ3"];
                TJHSItem[0]["H_CLWJJ3"] = sitem["H_CLWJJ3"];
                TJHSItem[0]["S_CLCHL1"] = sitem["S_CLCHL1"];
                TJHSItem[0]["H_CLCHL1"] = sitem["H_CLCHL1"];
                TJHSItem[0]["S_CLCHL2"] = sitem["S_CLCHL2"];
                TJHSItem[0]["H_CLCHL2"] = sitem["H_CLCHL2"];
                TJHSItem[0]["S_CLCHL3"] = sitem["S_CLCHL3"];
                TJHSItem[0]["H_CLCHL3"] = sitem["H_CLCHL3"];
                #endregion
                
                #region TKP
                TKPSItem[0]["SHAHSL"] = sitem["SHAHSL"];
                TKPSItem[0]["SHIHSL"] = sitem["SHIHSL"];
                TKPSItem[0]["SHAHSHIL"] = sitem["SHAHSHIL"];
                TKPSItem[0]["S_CLSN"] = sitem["S_CLSN"];
                TKPSItem[0]["H_CLSN"] = sitem["H_CLSN"];
                TKPSItem[0]["S_CLSHA"] = sitem["S_CLSHA"];
                TKPSItem[0]["H_CLSHA"] = sitem["H_CLSHA"];
                TKPSItem[0]["S_CLSHI"] = sitem["S_CLSHI"];
                TKPSItem[0]["H_CLSHI"] = sitem["H_CLSHI"];
                TKPSItem[0]["S_CLS"] = sitem["S_CLS"];
                TKPSItem[0]["H_CLS"] = sitem["H_CLS"];
                TKPSItem[0]["S_CLWJJ1"] = sitem["S_CLWJJ1"];
                TKPSItem[0]["H_CLWJJ1"] = sitem["H_CLWJJ1"];
                TKPSItem[0]["S_CLWJJ2"] = sitem["S_CLWJJ2"];
                TKPSItem[0]["H_CLWJJ2"] = sitem["H_CLWJJ2"];
                TKPSItem[0]["S_CLWJJ3"] = sitem["S_CLWJJ3"];
                TKPSItem[0]["H_CLWJJ3"] = sitem["H_CLWJJ3"];
                TKPSItem[0]["S_CLCHL1"] = sitem["S_CLCHL1"];
                TKPSItem[0]["H_CLCHL1"] = sitem["H_CLCHL1"];
                TKPSItem[0]["S_CLCHL2"] = sitem["S_CLCHL2"];
                TKPSItem[0]["H_CLCHL2"] = sitem["H_CLCHL2"];
                TKPSItem[0]["S_CLCHL3"] = sitem["S_CLCHL3"];
                TKPSItem[0]["H_CLCHL3"] = sitem["H_CLCHL3"];
                TKPSItem[0]["WJJ1MC"] = sitem["WJJ1MC"];
                TKPSItem[0]["WJJ2MC"] = sitem["WJJ2MC"];
                TKPSItem[0]["WJJ3MC"] = sitem["WJJ3MC"];
                TKPSItem[0]["CHL1MC"] = sitem["CHL1MC"];
                TKPSItem[0]["CHL2MC"] = sitem["CHL3MC"];
                TKPSItem[0]["CHL3MC"] = sitem["CHL3MC"];
                TKPSItem[0]["HPBBGBH"] = sitem["HPBBGBH"];
                #endregion

                #region HGZ
                HGZSItem[0]["WJJ1MC"] = sitem["WJJ1MC"];
                HGZSItem[0]["WJJ2MC"] = sitem["WJJ2MC"];
                HGZSItem[0]["WJJ3MC"] = sitem["WJJ3MC"];
                HGZSItem[0]["CHL1MC"] = sitem["CHL1MC"];
                HGZSItem[0]["CHL2MC"] = sitem["CHL3MC"];
                HGZSItem[0]["CHL3MC"] = sitem["CHL3MC"];
                HGZSItem[0]["SNPZDJ"] = sitem["SNPZDJ"];
                HGZSItem[0]["SAGG"] = sitem["SAGG"];
                HGZSItem[0]["SZGG"] = sitem["SZGG"];
                HGZSItem[0]["WJJ1ZL"] = sitem["WJJ1ZL"];
                HGZSItem[0]["WJJ2ZL"] = sitem["WJJ2ZL"];
                HGZSItem[0]["WJJ3ZL"] = sitem["WJJ3ZL"];
                HGZSItem[0]["CHL1ZL"] = sitem["CHL1ZL"];
                HGZSItem[0]["CHL2ZL"] = sitem["CHL3ZL"];
                HGZSItem[0]["CHL3ZL"] = sitem["CHL3ZL"];
                HGZSItem[0]["SNSCCJ"] = sitem["SNSCCJ"];
                HGZSItem[0]["SACD"] = sitem["SACD"];
                HGZSItem[0]["SZCD"] = sitem["SZCD"];
                HGZSItem[0]["WJJ1CJ"] = sitem["WJJ1CJ"];
                HGZSItem[0]["WJJ2CJ"] = sitem["WJJ2CJ"];
                HGZSItem[0]["WJJ3CJ"] = sitem["WJJ3CJ"];
                HGZSItem[0]["CHL1CJ"] = sitem["CHL1CJ"];
                HGZSItem[0]["CHL2CJ"] = sitem["CHL3CJ"];
                HGZSItem[0]["CHL3CJ"] = sitem["CHL3CJ"];
                #endregion
                
                #endregion


            }
            #endregion
            
            MItem[0]["JCJG"] = "----";
            MItem[0]["JCJGMS"] = "----";
            
            /************************ 代码结束 *********************/
            
            #endregion

        }
    }
}
