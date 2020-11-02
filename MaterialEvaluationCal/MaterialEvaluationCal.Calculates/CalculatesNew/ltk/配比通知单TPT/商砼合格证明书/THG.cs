using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.CalculatesNew.ltk.配比通知单.商砼合格证明书
{
    public class THG : BaseMethods
    {
        public void Calc()
        {
      

            #region Code
            var extraDJ = dataExtra["BZ_THG_DJ"];
            var extraGG = dataExtra["BZ_THGGG"];

            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jgsm = "";
            var jcjg = "合格";
            var S_THGS = data["S_THG"];
            var mAllHg = true;
            var MItem = data["M_THG"];
            double [] DbqdArrqy = new double[210];//代表强度集合
            double [] PjqdArrqy = new double[210]; //平均强度集合
            int hntzs = 0;//混凝土组数
            int bhgzs = 0;
            double DbqdSum = 0;//代表强度之和
            double bzkyqd = 0;
            string dzpjkyqd="";


            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            foreach (var sItem in S_THGS)
            {
                var mrsgg = extraGG.FirstOrDefault(x => x["MC"].Trim().Equals(sItem["GGXH"], StringComparison.OrdinalIgnoreCase));
                if (mrsgg != null)
                {
                    sItem["SJCC"] = GetSafeDouble(mrsgg["CD"]).ToString("0");
                    sItem["HSXS"] = mrsgg["HSXS"];
                }
                else
                {
                    mjcjg = "不下结论";
                    sItem["SJCC"] = "0";
                    sItem["HSXS"] = "0";
                    jsbeizhu = "规格不祥。";
                    sItem["JCJG"] = "不下结论";
                    mAllHg = false;
                    continue;
                }

                var InitDate = DateTime.Parse("1900-1-1");
                var ZZRQ = InitDate;
                var SYRQ = InitDate;
                DateTime.TryParse(sItem["ZZRQ"], out ZZRQ);
                DateTime.TryParse(sItem["SYRQ"], out SYRQ);
                var YHTJ = sItem["YHTJ"].Trim();

               /* var LQ = 0;
                if (ZZRQ <= InitDate || SYRQ <= InitDate)
                {
                    LQ = 0;
                }
                else
                {
                    LQ = (SYRQ - ZZRQ).Days;
                    //if (LQ != 28 && YHTJ.Equals("标准"))
                    //{
                    // LQ = 28;
                    //}
                }*/

                var KYHZ1 = GetSafeDouble(sItem["KYHZ1"]);
                var KYHZ2 = GetSafeDouble(sItem["KYHZ2"]);
                var KYHZ3 = GetSafeDouble(sItem["KYHZ3"]);
                var SJCC = sItem["SJCC"];
                var SJDJ = sItem["SJDJ"];

                var SJCCDouble = GetSafeDouble(SJCC);
                var SJCCMJ = SJCCDouble * SJCCDouble;

                var HSXS = 1.0;
                if (SJCC == "100")
                {
                    HSXS = 0.95;
                }
                else if (SJCC == "200")
                {
                    HSXS = 1.05;
                }

                var KYQD1 = Math.Round(KYHZ1 * 1000 / SJCCMJ * HSXS, 1);
                var KYQD2 = Math.Round(KYHZ2 * 1000 / SJCCMJ * HSXS, 1);
                var KYQD3 = Math.Round(KYHZ3 * 1000 / SJCCMJ * HSXS, 1);
               
                List<double> KYQDS = new List<double>();
                KYQDS.Add(KYQD1);
                KYQDS.Add(KYQD2);
                KYQDS.Add(KYQD3);
                KYQDS.Sort();
                var MinKYQD = KYQDS[0];
                var MiddleKYQD = KYQDS[1];
                var MaxKYQD = KYQDS[2];
                

                var KYPJ = "";

                var mSz = 0.0;
                var mQdyq = 0.0;
                if (!String.IsNullOrEmpty(SJDJ))
                {
                    var DJ = extraDJ.FirstOrDefault(x => x["MC"].Trim().Equals(SJDJ, StringComparison.OrdinalIgnoreCase));
                    if (DJ != null)
                    {
                        var sz = DJ["SZ"].Trim();
                        var qdyq = DJ["QDYQ"].Trim();
                        mSz = GetSafeDouble(sz);
                        mQdyq = GetSafeDouble(qdyq);
                        bzkyqd = mQdyq;
                    }
                    else
                    {
                        mjcjg = "不下结论";
                        mSz = 0;
                        mQdyq = 0;
                        jsbeizhu = "设计等级为空或不存在。";
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                }

                var BaiFenBi1 = ((MiddleKYQD - MinKYQD) / MiddleKYQD) * 100;
                var BaiFenBi2 = ((MaxKYQD - MiddleKYQD) / MiddleKYQD) * 100;

                if (MItem[0]["PDBZ"].Contains("2019"))
                {
                    if ((BaiFenBi2 > 15 || BaiFenBi1 > 15))
                    {
                        jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                        KYPJ = MiddleKYQD.ToString("0.0");
                        dzpjkyqd = Math.Round((KYQD1 + KYQD2 + KYQD3) / 3, 1).ToString("0.0");
                    }
                    else if (BaiFenBi1 > 15 && BaiFenBi2 > 15)
                    {
                        mjcjg = "不下结论";
                        KYPJ = "试验结果无效";
                        jcjg = "不下结论";
                        jsbeizhu = "最大最小强度值超出中间值的15%,试验结果不作评定依据";
                        mAllHg = false;
                    }
                    else
                    {
                        KYPJ = Math.Round((KYQD1 + KYQD2 + KYQD3) / 3, 1).ToString("0.0");
                        dzpjkyqd = Math.Round((KYQD1 + KYQD2 + KYQD3) / 3, 1).ToString("0.0");
                    }
                }
                else
                {
                    if ((BaiFenBi2 > 15 && BaiFenBi1 <= 15))
                    {
                        jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                        KYPJ = MiddleKYQD.ToString("0.0");
                        dzpjkyqd = Math.Round((KYQD1 + KYQD2 + KYQD3) / 3, 1).ToString("0.0");
                    }
                    else if (BaiFenBi1 > 15 && BaiFenBi2 <= 15)
                    {
                        jsbeizhu = "最大最小强度值其中一个超出中间值的15%,试验结果取中间值";
                        KYPJ = MiddleKYQD.ToString("0.0");
                        dzpjkyqd = Math.Round((KYQD1 + KYQD2 + KYQD3) / 3, 1).ToString("0.0");
                    }
                    else if (BaiFenBi1 > 15 && BaiFenBi2 > 15)
                    {
                        mjcjg = "不下结论";
                        KYPJ = "试验结果无效";
                        jcjg = "不下结论";
                        jsbeizhu = "最大最小强度值超出中间值的15%,试验结果不作评定依据";
                        mAllHg = false;
                    }
                    else
                    {
                        KYPJ = Math.Round((KYQD1 + KYQD2 + KYQD3) / 3, 1).ToString("0.0");
                        dzpjkyqd = Math.Round((KYQD1 + KYQD2 + KYQD3) / 3, 1).ToString("0.0");
                    }
                }
                sItem["KYQD1"] = Round(KYQD1, 1).ToString("0.0");
                sItem["KYQD2"] = Round(KYQD2, 1).ToString("0.0");
                sItem["KYQD3"] = Round(KYQD3, 1).ToString("0.0");

                //试验结果无效判定逻辑有误
                if (KYPJ == "试验结果无效")
                {
                    sItem["KYPJ"] = KYPJ;
                    mjcjg = "不下结论";
                    jcjg = "不下结论";
                    jgsm = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样强度代表值无效。";
                }
                else
                {
                    //同条件换算 代表值除以 0.88
                    if (sItem["YHTJ"] == "同条件养护(600℃ · d)" || (sItem["YHTJ"] == "同条件" && sItem["YHWD"] == "≥600"))
                    {
                        sItem["KYPJ"] = Round(GetSafeDouble(KYPJ) / 0.88, 1).ToString("0.0");
                        sItem["TTJHSXS"] = "0.88";
                    }
                    else
                    {
                        sItem["TTJHSXS"] = "----";
                        sItem["KYPJ"] = KYPJ;
                    }

                    sItem["DDSJQD"] = Round(GetSafeDouble(sItem["KYPJ"]) / mSz * 100, 0).ToString("0");
                    sItem["HSXS"] = HSXS.ToString();
                    //sItem["LQ"] = LQ.ToString();

                    if (100 > GetSafeDouble(sItem["DDSJQD"]))
                    {
                        jcjg = "不合格";
                        mAllHg = false;
                        bhgzs = bhgzs + 1;
                    }

                    jgsm = "依据" + MItem[0]["PDBZ"] + "的规定，该组试样强度代表值" + sItem["KYPJ"] + "MPa，" + "达到设计强度" + sItem["DDSJQD"] + "%。";
                }
                sItem["JCJG"] = jcjg;
                DbqdArrqy[hntzs] =GetSafeDouble(KYPJ);
                PjqdArrqy[hntzs] = GetSafeDouble(dzpjkyqd); 
                
                hntzs = hntzs + 1;//从表组数，试件总组数
            }
                MItem[0]["ZZS"] = hntzs.ToString();
                int n = hntzs;//可作为评定依据的组数





            #region 所有组混凝土代表强度得到后，商砼统计评定
            
            #region 总组数>1
            //标准差
            //和；平均值
            double bzczbz1 = 0;//标准差准备值
            double bzczbz2 = 0;
            int ddhgzs = 0;//达到合格组数
            for (int i = 0; i < hntzs; i++)
            {
                if (DbqdArrqy[i] != 0)
                {
                    DbqdSum = DbqdSum + DbqdArrqy[i];
                    
                    bzczbz1 = DbqdArrqy[i] * DbqdArrqy[i] + bzczbz1;
                }
                if (DbqdArrqy[i] == 0)
                {
                    n = n - 1;
                }
                MItem[0]["QDDBZ" + (i + 1)] = DbqdArrqy[i].ToString("0.0");
            }
            #region 总组数或可作为评定依据的组数等于1
            if (GetSafeDouble(MItem[0]["ZZS"]) == 1)
            {
                MItem[0]["QDBZL"] = "100";
                MItem[0]["MZQDZS"] = n.ToString();
                MItem[0]["QDDBZ1"] = S_THGS[0]["KYPJ"];
                MItem[0]["PNPJQD"] = MItem[0]["QDDBZ1"];
                MItem[0]["QDBZC"] = "/";
                MItem[0]["QDGFQZ"] = "/";
                MItem[0]["QDZXZ"] = MItem[0]["QDDBZ1"];
                MItem[0]["BGHGPDXS1"] = "1.15";
                MItem[0]["BGHGPDXS2"] = "0.95";
                if (bhgzs == 0)
                {
                    MItem[0]["PHGPD"] = "合格";
                }
                else
                {
                    MItem[0]["PHGPD"] = "不合格";
                }
            }
            #endregion
            else {
                //强度保证率：合格强度组数/有效组数*10
                MItem[0]["QDBZL"]=(100- Round(bhgzs / n * 100, 0)).ToString("0");
                MItem[0]["MZQDZS"] = n.ToString();
                for (int a = hntzs; a < 209; a++)
                {
                    MItem[0]["QDDBZ" + (a + 1)] = "/";
                }
                bzczbz2 = (DbqdSum / n) * (DbqdSum / n) * n;
                MItem[0]["PNPJQD"] = (DbqdSum / n).ToString("0.00");

                MItem[0]["QDBZC"] = Math.Sqrt((bzczbz1 - bzczbz2) / (n - 1)).ToString("0.00");
                if (Math.Sqrt((bzczbz1 - bzczbz2) / (n - 1)) < 2.5)
                {
                    MItem[0]["QDGFQZ"] = "2.50";
                }
                else
                {
                    MItem[0]["QDGFQZ"] = MItem[0]["QDBZC"];
                }
                DbqdArrqy.ToList().Sort();
                MItem[0]["QDZXZ"] = DbqdArrqy[0].ToString();
           
            
                if (hntzs >= 10)//统计方法为gb t50107-2012 5.1.3
                {
                    if (hntzs < 15){ MItem[0]["HGPDXS1"] = "1.15"; MItem[0]["HGPDXS2"] = "0.90"; }
                    if (hntzs >14 && hntzs<20) { MItem[0]["HGPDXS1"] = "1.05"; MItem[0]["HGPDXS2"] = "0.90"; }
                    if (hntzs > 19) { MItem[0]["HGPDXS1"] = "0.95"; MItem[0]["HGPDXS2"] = "0.85"; }
                    MItem[0]["BGHGPDXS1"] = MItem[0]["HGPDXS1"];
                    MItem[0]["BGHGPDXS2"] = MItem[0]["HGPDXS2"];

               
                    if (DbqdSum / n >= (bzkyqd + GetSafeDouble(MItem[0]["HGPDXS1"]) * Math.Sqrt((bzczbz1 - bzczbz2) / (n - 1))) && DbqdArrqy[0] >= GetSafeDouble(MItem[0]["HGPDXS2"]) * bzkyqd)
                    {
                        MItem[0]["PHGPD"] = "合格";
                    }
                    else
                    {
                        MItem[0]["PHGPD"] = "不合格";
                    }


                

                }
                else
                {//非统计方法
                    MItem[0]["HGPDXS4"] = "0.95";
                    if (GetSafeDouble( GetNum(S_THGS[0]["SJDJ"])) < 60)
                    {
                        MItem[0]["HGPDXS3"] = "1.15";
                    }
                    else { MItem[0]["HGPDXS3"] = "1.10"; }
                    MItem[0]["BGHGPDXS1"] = MItem[0]["HGPDXS3"];
                    MItem[0]["BGHGPDXS2"] = MItem[0]["HGPDXS4"];
                
                    if (DbqdSum / n >= bzkyqd*GetSafeDouble(MItem[0]["HGPDXS3"]) && DbqdArrqy[0] >= GetSafeDouble(MItem[0]["HGPDXS4"]) * bzkyqd)
                    {
                        MItem[0]["PHGPD"] = "合格";
                    }
                    else
                    {
                        MItem[0]["PHGPD"] = "不合格";
                    }
                
              
                }
            }
            #endregion
            jgsm = "依据" + MItem[0]["PDBZ"] + "的规定，"+ MItem[0]["PHGPD"];
            #endregion



            if (MItem[0]["PHGPD"] == "合格")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_THG"))
            {

                data["M_THG"] = new List<IDictionary<string, string>>();
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jgsm;
            //单组混凝土试块
            if ("----" == S_THGS[0]["SJDJ"])
            {
                S_THGS[0]["JCJG"] = "不下结论";
                MItem[0]["JCJGMS"] = "----";
                MItem[0]["JCJG"] = "不下结论";
            }
            MItem[0]["JCJG"] = "----";

            MItem[0]["JCJGMS"] = "----";
            #endregion
        }
    }
}
