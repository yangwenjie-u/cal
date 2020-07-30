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
            #region
            /************************ 代码开始 *********************/
            #region 变量定义
            string mcalBh, mlongStr, mSjdjbh, mSjdj, mMaxBgbh, mJSFF,myqsyrq;
            myqsyrq = "0";

            double mSjcc, mMj, mSjcc1, mMaxKyqd, mMinKyqd, mMidKyqd, mAvgKyqd, mMaxKyhz, mMinKyhz, mMidKyhz, mAvgKyhz,mSz, mQdyq, mHsxs, mttjhsxs;
            int vp;
            bool mAllHg, mGetBgbh, mSFwc;
            double[] mkyhzArray = new double[2];
            double[] mkyqdArray = new double [2];
            mSFwc = true;

            #endregion
            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_TPT_DJ"];
           // var mrsGg = dataExtra["BZ_TPT_Gg"];
            var MItem = data["M_TPT"];
            var SItem = data["S_TPT"];
            //砼合格证明书

            /*var THGSItem = data["S_THG"];
            var THGMItem = data["M_THG"];*/
            //商砼开盘配合比
            
            var TKPSItem = data["S_TKP"];
            var TKPMItem = data["M_TKP"];
            //商砼搅拌记录
            var STJMItem = data["M_STJ"];
            var STJSItem = data["S_STJ"];
           
            //商砼交货检验记录
            var TJHMItem = data["M_TJH"];
            var TJHSItem = data["S_TJH"];
          /*  //商砼抗压强度
            var HGZMItem = data["M_HGZ"];
            var HGZSItem = data["S_HGZ"];*/
            #endregion
            
            #region 计算开始
            foreach (var sitem in SItem)
            {
                #region TPT
                /*myqsyrq = HGZSItem[0]["YQSYRQ"];
                if(string.IsNullOrEmpty(myqsyrq) | GetSafeDate( myqsyrq)< GetSafeDate(" 2000 - 1 - 1"))
                {
                    HGZSItem[0]["LQ"] = "28";
                    HGZSItem[0]["YQSYRQ"] = (GetSafeDouble(HGZSItem[0]["ZZRQ"]) + 28).ToString();
                }
                HGZMItem[0]["YQSYRQ"] = myqsyrq;*/
                double d_SHAHSL, d_SHIHSL, d_SHAHSHIL, d_T_CLSN, d_T_CLSHA, d_T_CLSHI, d_T_CLS, d_T_CLWJJ1, d_T_CLWJJ2, d_T_CLWJJ3, d_T_CLCHL1, d_T_CLCHL2, d_T_CLCHL3;
                //砂含水率
                d_SHAHSL = GetSafeDouble(sitem["SHAHSL"]);
                //石含水率
                d_SHIHSL= GetSafeDouble(sitem["SHIHSL"]);
                //砂含石率
                d_SHAHSHIL= GetSafeDouble(sitem["SHAHSHIL"]);
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
                md = d_T_CLSHA * 100 / (100 - d_SHAHSHIL);

                //由水/（干的石子+砂子的质量）=含水率  得到湿的混合物
                md = md * (100 + d_SHAHSL) / 100;
                var d_H_CLSHA = Round(md, 0);
                //计算含石量
                md1 = d_T_CLSHI;
                md2 = (d_SHAHSHIL / 100) * d_T_CLSHA * 100 / (100 - d_SHAHSHIL);
                //干的石子质量
                md = md1 - md2;
                md = md * (100 + d_SHIHSL) / 100;
                var d_H_CLSHI = Round(md, 0);
                var d_H_CLS = Round((d_T_CLS - d_H_CLSHA * d_SHAHSL / 100 - d_H_CLSHI * d_SHIHSL / 100), 0);
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
                    sitem["H_CLWJJ1"] = d_T_CLWJJ1.ToString();
                }
                else { sitem["H_CLWJJ1"] = "----"; }
                if (d_T_CLWJJ2 == 0)
                {
                    sitem["H_CLWJJ2"] = d_T_CLWJJ2.ToString();
                }
                else { sitem["H_CLWJJ2"] = "----"; }
                if (d_T_CLWJJ3 == 0)
                {
                    sitem["H_CLWJJ3"] = d_T_CLWJJ3.ToString();
                }
                else { sitem["H_CLWJJ3"] = "----"; }

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

                #endregion

                #region STJ
                STJSItem[0]["SHAHSL"] = sitem["SHAHSL"];
                STJSItem[0]["SHIHSL"] = sitem["SHIHSL"];
                STJSItem[0]["SHAHSHIL"] = sitem["SHAHSHIL"];
                STJSItem[0]["S_CLSN"] = sitem["S_CLSN"];
                STJSItem[0]["H_CLSN"] = sitem["H_CLSN"];
                STJSItem[0]["S_CLSHA"] = sitem["S_CLSHA"];
                STJSItem[0]["H_CLSHA"] = sitem["H_CLSHA"];
                STJSItem[0]["S_CLSHI"] = sitem["S_CLSHI"];
                STJSItem[0]["H_CLSHI"] = sitem["H_CLSHI"];
                STJSItem[0]["S_CLS"] = sitem["S_CLS"];
                STJSItem[0]["H_CLS"] = sitem["H_CLS"];
                STJSItem[0]["S_CLWJJ1"] = sitem["S_CLWJJ1"];
                STJSItem[0]["H_CLWJJ1"] = sitem["H_CLWJJ1"];
                STJSItem[0]["S_CLWJJ2"] = sitem["S_CLWJJ2"];
                STJSItem[0]["H_CLWJJ2"] = sitem["H_CLWJJ2"];
                STJSItem[0]["S_CLWJJ3"] = sitem["S_CLWJJ3"];
                STJSItem[0]["H_CLWJJ3"] = sitem["H_CLWJJ3"];
                STJSItem[0]["S_CLCHL1"] = sitem["S_CLCHL1"];
                STJSItem[0]["H_CLCHL1"] = sitem["H_CLCHL1"];
                STJSItem[0]["S_CLCHL2"] = sitem["S_CLCHL2"];
                STJSItem[0]["H_CLCHL2"] = sitem["H_CLCHL2"];
                STJSItem[0]["S_CLCHL3"] = sitem["S_CLCHL3"];
                STJSItem[0]["H_CLCHL3"] = sitem["H_CLCHL3"];
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

            }


            #endregion





            /************************ 代码结束 *********************/
            #endregion

        }
    }
}
