using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SJG: BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr;
            double mDyCount, mXyCount;
            double moldDyCount, moldXyCount;
            double mHtzhz, mHtqdz = 0;
            string mSjdjbh, mSjdj = "";
            int mPc, vj, vi;
            double mSz = 0;
            double s_Htqdz;
            double[] Ht_Qdz;
            double mThsd, mQdtdz;
            int[] e_c_ht;
            int c_Ht, c_Thsd;
            double r_Min, r_Pj, s_N2, s_N, n_Pj, n_Min;
            double mThxzz, s_Thsd, mAvgThsd, s_Htzhz;
            int vp;
            string xx;
            string sjzl = "";
            string mMaxBgbh;
            string mJsff;
            bool mBsfs;
            bool mAllHg;
            bool mGetBgbh;
            double mQxhtxzxs;
            bool mSFwc;
            mSFwc = true;
            mAllHg = true;
            #endregion

            #region 自定义函数
            //计算回弹最后值HTZHZ
            Func<IDictionary<string, string>, double> calc_htzhz =
                delegate (IDictionary<string, string> sitem)
                {
                    double[] mhtzArray = new double[16];
                    string[] mtmpArray_fun;
                    string mlongStr_fun = "";
                    double mMaxHtz, mMinHtz, mSum;
                    double mHtzhz_fun;
                    if (string.IsNullOrEmpty(sitem["HTZ1"]))
                        sitem["HTZ1"] = "0";
                    for (int i = 1; i <= 16; i++)
                    {
                        if (i == 1)
                            mlongStr_fun = sitem["HTZ1"];
                        else
                        {
                            if (string.IsNullOrEmpty(sitem["HTZ" + i]))
                                sitem["HTZ" + i] = "0";
                            mlongStr_fun = mlongStr_fun + "," + sitem["HTZ" + i];
                        }
                    }

                    mtmpArray_fun = mlongStr_fun.Split(',');
                    mSum = 0;
                    for (vp = 0; vp <= 15; vp++)
                    {
                        mhtzArray[vp] = GetSafeDouble(mtmpArray_fun[vp]);
                        mSum = mSum + mhtzArray[vp];
                    }
                    Array.Sort(mhtzArray);
                    mMaxHtz = mhtzArray[15] + mhtzArray[14] + mhtzArray[13];
                    mMinHtz = mhtzArray[0] + mhtzArray[1] + mhtzArray[2];
                    mHtzhz = Round((mSum - mMaxHtz - mMinHtz) / 10, 2);
                    sitem["HTZHZ"] = mHtzhz.ToString();
                    return mHtzhz;
                };

            //查找回弹换算表(2001-10-01之后的新方法)，获取回弹强度值
            Func<IDictionary<string, string>, string, double> getQdzNew =
                delegate (IDictionary<string, string> mrssjhsb_fun, string sjzl_fun)
                {
                    double ret_Val, mGo_Thsd;
                    double mRm;

                    mRm = GetSafeDouble(mrssjhsb_fun["GRSD"]);
                    ret_Val = GetSafeDouble(mrssjhsb_fun[sjzl_fun].ToUpper());
                    return ret_Val;
                };

            //回弹函数
            Func<IList<IDictionary<string, string>>, double, string, double> sjhsb =
                delegate (IList<IDictionary<string, string>> mrssjhsb_fun, double mHtzhz_fun, string sjzl_fun)
                {
                    double mHtzhz0_fun = 0, mHtzhz1_fun = 0, mHtqdz_fun = 0;
                    if (mHtzhz_fun < 2.9)
                        mHtzhz_fun = 2.9;
                    if (mHtzhz_fun > 19.1)
                        mHtzhz_fun = 19.1;
                    string endchar_str = Round(mHtzhz_fun, 2).ToString("#####0.00").Trim();
                    double endchar = GetSafeDouble(endchar_str.Substring(endchar_str.Length - 1, 1));
                    if (endchar != 0)
                    {
                        //不在换算表中Begin
                        if (endchar >= 5)
                        {
                            mHtzhz0_fun = Round(mHtzhz_fun + 0.00001 - 0.1, 1);
                            mHtzhz1_fun = Round(mHtzhz_fun + 0.00001, 1);
                        }
                        else
                        {
                            mHtzhz0_fun = Round(mHtzhz_fun, 1);
                            mHtzhz1_fun = Round(mHtzhz_fun + 0.1, 1);
                        }
                        IDictionary<string, string> mrssjhsb_Filter = new Dictionary<string, string>();
                        IDictionary<string, string> mrssjhsb_Filter2 = new Dictionary<string, string>(); ;
                        var mrssjhsb_Find = mrssjhsb_fun.Where(x => GetSafeDouble(x["GRSD"]).Equals(mHtzhz0_fun));
                        if (mrssjhsb_Find != null && mrssjhsb_Find.Count() > 0)
                        {
                            if (mHtzhz0_fun < 2.9)
                                mrssjhsb_Filter = mrssjhsb_Find.FirstOrDefault();
                            else
                                mrssjhsb_Filter = mrssjhsb_Find.LastOrDefault();
                        }
                        double MHTQDZ0 = getQdzNew(mrssjhsb_Filter, sjzl);
                        var mrssjhsb_Find2 = mrssjhsb_fun.Where(x => GetSafeDouble(x["GRSD"]).Equals(mHtzhz1_fun));
                        if (mrssjhsb_Find2 != null && mrssjhsb_Find2.Count() > 0)
                        {
                            if (mHtzhz1_fun < 2.9)
                                mrssjhsb_Filter2 = mrssjhsb_Find2.FirstOrDefault();
                            else
                                mrssjhsb_Filter2 = mrssjhsb_Find2.LastOrDefault();
                        }
                        double MHTQDZ1 = getQdzNew(mrssjhsb_Filter2, sjzl);
                        if (MHTQDZ0 == 0)
                            mHtqdz_fun = MHTQDZ1;
                        else if (MHTQDZ1 == 0)
                            mHtqdz_fun = MHTQDZ0;
                        else
                            mHtqdz_fun = MHTQDZ0 - ((MHTQDZ0 - MHTQDZ1) * endchar) / 10;
                        // 不在换算表中End
                    }
                    else
                    {
                        //在换算表中Begin
                        IDictionary<string, string> mrssjhsb_Filter = new Dictionary<string, string>();
                        var mrssjhsb_Find = mrssjhsb_fun.Where(x => GetSafeDouble(x["GRSD"]).Equals(mHtzhz_fun));
                        if (mrssjhsb_Find != null && mrssjhsb_Find.Count() > 0)
                        {
                            if (mHtzhz_fun < 20.2)
                                mrssjhsb_Filter = mrssjhsb_Find.FirstOrDefault();
                            else
                                mrssjhsb_Filter = mrssjhsb_Find.LastOrDefault();
                        }
                        mHtqdz_fun = getQdzNew(mrssjhsb_Filter, sjzl);
                        //换算表中End
                    }
                    if (sjzl == "snqd" && mHtqdz_fun == 0)
                        mHtqdz_fun = 17;
                    if (sjzl == "mxbs" && mHtqdz_fun == 0)
                        mHtqdz_fun = 18;
                    if (sjzl == "mybs" && mHtqdz_fun == 0)
                        mHtqdz_fun = 19;
                    if (sjzl == "mxbs" && mHtqdz_fun == 0)
                        mHtqdz_fun = 20;
                    if (sjzl == "qxbs" && mHtqdz_fun == 0)
                        mHtqdz_fun = 21;
                    if (sjzl == "qybs" && mHtqdz_fun == 0)
                        mHtqdz_fun = 22;
                    return mHtqdz_fun;
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_SJG_DJ"];
            var mrssjhsb = dataExtra["BZ_SJGHSB"];
            var MItem = data["M_SJG"];
            var SItem = data["S_SJG"];
            #endregion

            #region 计算开始
            c_Ht = 0;
            c_Ht = SItem.Count();
            Ht_Qdz = new double[c_Ht + 1];
            mDyCount = 0;
            mXyCount = 0;
            moldDyCount = 0;
            moldXyCount = 0;
            s_Htzhz = 0;
            s_Htqdz = 0;
            c_Thsd = 0;
            s_Thsd = 0;
            n_Min = 9999;
            vi = 1; //构件数计数器
            foreach (var sitem in SItem)
            {
                sitem["LQ"] = (GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days.ToString();
                if (sitem["ZZRQ"] == "1900-01-01")
                    sitem["LQ"] = "0";
                if (GetSafeDouble(sitem["LQ"]) > 3000)
                    sitem["LQ"] = "0";
                mSjdj = sitem["SJDJ"];  //设计等级名称
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj.Trim()));
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    mSz = GetSafeDouble(mrsDj_Filter["SZ"]);
                    mJsff = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mSz = 0;
                    mJsff = "";
                    sitem["JCJG"] = "依据不详";
                    break;
                }
                if (mJsff == "" || mJsff == "new")
                {
                    for (vp = 1; vp < 17; vp++)
                    {
                        sitem["BPZD" + vp] = "20";
                        sitem["HTZ" + vp] = sitem["SDDS" + vp];
                    }
                    mHtzhz = calc_htzhz(sitem);
                    if (MItem[0]["PDBZ"].Contains("136-2017"))
                    {
                        if (sitem["SJDJ"].Contains("水泥砂浆"))
                            sjzl = "qxbs";
                        if (sitem["SJDJ"].Contains("水泥混合"))
                            sjzl = "qxbh";
                        if (sitem["SJDJ"].Contains("预拌砂浆"))
                            sjzl = "qybs";
                        if (sitem["SJDJ"].Contains("预拌抹灰"))
                            sjzl = "mybs";
                        if (sitem["SJDJ"].Contains("水泥抹灰"))
                            sjzl = "mxbs";


                        mHtqdz = Round(sjhsb(mrssjhsb, mHtzhz, sjzl), 1);

                        sitem["HTQDZ"] = mHtqdz.ToString(("0.0"));
                        if (sitem["SJDJ"].Contains("抹灰"))
                            sitem["QDTDZ"] = mHtqdz.ToString("0.0");
                        else
                            sitem["QDTDZ"] = (0.91 * mHtqdz).ToString("0.0");
                    }
                    else
                    {
                        if (sitem["SJDJ"].Contains("水泥砂浆"))
                            sjzl = "snqd";
                        else
                            sjzl = "hhqd";
                        mHtqdz = Round(sjhsb(mrssjhsb, mHtzhz, sjzl), 1);
                        sitem["HTQDZ"] = mHtqdz.ToString("0.0");
                        sitem["QDTDZ"] = mHtqdz.ToString("0.0");
                    }
                    if (mSz != 0)
                        sitem["DDSJQD"] = Round(GetSafeDouble(sitem["QDTDZ"]) / mSz * 100, 0).ToString();     //达到设计强度%
                }
                //平均值,最小值等计算
                s_Htqdz = s_Htqdz + mHtqdz;


                if (n_Min > mHtqdz)       //求n_Min
                    n_Min = mHtqdz;
                Ht_Qdz[vi] = mHtqdz;
                vi = vi + 1; //计算单个构件数
            }
            //---综合判断---
            //平均值,最小值等计算
            r_Pj = Round(s_Htqdz / c_Ht, 1);  //求
            s_N2 = 0;
            MItem[0]["BYXS"] = "1";
            if (c_Ht >= 6)
            {
                for (vi = 1; vi <= c_Ht; vi++)
                    s_N2 = s_N2 + (r_Pj - Ht_Qdz[vi]) * (r_Pj - Ht_Qdz[vi]);

                s_N = Round(Math.Sqrt(s_N2 / (c_Ht - 1)), 2);



                if (MItem[0]["PDBZ"].Contains("136-2017"))
                {
                    if (mSjdj.Contains("抹灰"))
                    {
                        if (r_Pj > Round((1.33 * n_Min), 1))
                            MItem[0]["QDTDZ"] = Round(1.33 * n_Min, 1).ToString("0.0");
                        else
                            MItem[0]["QDTDZ"] = r_Pj.ToString();
                    }
                    else
                    {
                        if (Round(0.91 * r_Pj, 1) > Round(1.18 * n_Min, 1))
                            MItem[0]["QDTDZ"] = Round(1.18 * n_Min, 1).ToString("0.0");
                        else
                            MItem[0]["QDTDZ"] = Round(0.91 * r_Pj, 1).ToString("0.0");
                    }
                }
                else
                {
                    if (r_Pj > Round(n_Min / 0.75, 1))
                        MItem[0]["QDTDZ"] = Round(n_Min / 0.75, 1).ToString("0.0");

                    else
                        MItem[0]["QDTDZ"] = r_Pj.ToString();
                }



                //将计算结果赋值给主表相应字段


                MItem[0]["PJQDZ"] = r_Pj.ToString();  //'平均强度值  *****

                MItem[0]["QDBZC"] = s_N.ToString();    //'标准差

                MItem[0]["ZXQDZ"] = n_Min.ToString();    //'最小强度值  *****

                if (r_Pj != 0)
                    MItem[0]["BYXS"] = Round(s_N / r_Pj, 2).ToString("0.00");    //变异系数
            }
            MItem[0]["CQS"] = c_Ht.ToString();
            if (c_Ht < 6 || GetSafeDouble(MItem[0]["BYXS"]) >= 0.3)
                MItem[0]["QDTDZ"] = "0";
            if (mSz != 0)
                MItem[0]["DDSJQD"] = Round(GetSafeDouble(MItem[0]["QDTDZ"]) / mSz * 100, 0).ToString();   //'达到设计强度%
                                                                                                          //主表总判断赋值
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
