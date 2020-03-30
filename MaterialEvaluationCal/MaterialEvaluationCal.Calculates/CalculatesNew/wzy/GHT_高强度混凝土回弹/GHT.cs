using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class GHT : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/

            #region 参数定义
            string mcalBh, mlongStr;
            double mDyCount, mXyCount, zmDycount, zmXycount;
            double moldDyCount, moldXyCount;
            double mHtzhz = 0, mHtqdz = 0;
            string mSjdjbh, mSjdj;
            int mPc, vj, vi, fji;
            double mSz = 0;
            double s_Htqdz, z_htqdz;
            double[] Ht_Qdz;
            double mThsd, mQdtdz;
            int[] e_c_ht;
            int c_Ht, c_Thsd, z_ht, zc_thsd;
            double r_Min, r_Pj, s_N2, s_N, n_Pj, n_Min, z_n2, z_n, zr_Min, zn_Min, zr_Pj, zn_Pj;
            double mThxzz, s_Thsd, mAvgThsd, s_Htzhz, z_thsd, z_htzhz;
            int vp;
            string xx, tmpstr;
            string mMaxBgbh;
            string mJsff, bgfjs;
            string mBsfs;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            mSFwc = true;
            string which = "";
            #endregion

            #region  自定义函数

            //计算回弹最后值HTZHZ
            Func<IDictionary<string, string>, double> calc_htzhz =
                delegate (IDictionary<string, string> mrssjTable_Filter)
                {
                    //计算回弹最后值HTZHZ
                    double[] mhtzArray = new double[16];
                    List<string> mtmpArray = new List<string>();
                    string mfuncVal = string.Empty;
                    string mlongStr_fun = string.Empty;
                    double mMaxHtz_fun;
                    double mMinHtz_fun;
                    double mSum_fun;
                    double mHtzhz_fun = 0;
                    if (string.IsNullOrEmpty(mrssjTable_Filter["HTZ1"]))
                        mrssjTable_Filter["HTZ1"] = "0";
                    for (int i = 1; i <= 16; i++)
                    {
                        if (i == 1)
                            mlongStr_fun = mrssjTable_Filter["HTZ1"].Trim();
                        else
                        {
                            if (string.IsNullOrEmpty(mrssjTable_Filter["HTZ" + i]))
                                mrssjTable_Filter["HTZ" + i] = "0";
                            mlongStr_fun = mlongStr_fun + "," + mrssjTable_Filter["HTZ" + i].Trim();
                        }
                    }
                    if (!string.IsNullOrEmpty(mlongStr_fun))
                    {
                        foreach (var mlongStrarray in mlongStr_fun.Split(','))
                        {
                            mtmpArray.Add(mlongStrarray);
                        }
                    }
                    mSum_fun = 0;
                    for (int i = 0; i < 16; i++)
                    {
                        mhtzArray[i] = GetSafeDouble(mtmpArray[i]);
                        mSum_fun += mhtzArray[i];
                    }
                    //数组排序
                    int min;
                    for (int i = 0; i < mhtzArray.Length; i++)
                    {
                        min = i;
                        for (int j = i + 1; j < mhtzArray.Length; j++)
                        {
                            if (mhtzArray[j] < mhtzArray[min])
                                min = j;
                        }
                        double t = mhtzArray[min];
                        mhtzArray[min] = mhtzArray[i];
                        mhtzArray[i] = t;
                    }
                    mMaxHtz_fun = mhtzArray[15] + mhtzArray[14] + mhtzArray[13];
                    mMinHtz_fun = mhtzArray[0] + mhtzArray[1] + mhtzArray[2];
                    mHtzhz_fun = Math.Round((mSum_fun - mMaxHtz_fun - mMinHtz_fun) / 10, 1);
                    mrssjTable_Filter["HTZHZ"] = !string.IsNullOrEmpty(mHtzhz_fun.ToString()) ? mHtzhz_fun.ToString() : "0";
                    return mHtzhz_fun;
                    //计算回弹最后值HTZHZ结束
                };
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_GHT_DJ"];
            var MItem = data["M_GHT"];
            var SItem = data["S_GHT"];
            var mrsXtab = data["X_GHT"];
            var mrssjTable = data["Y_GHT"];
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            MItem[0]["JCJGMS"] = "";
            //是否取芯修正
            zmDycount = 0;
            zmXycount = 0;
            z_htzhz = 0;
            z_htqdz = 0;
            z_ht = 0;
            z_thsd = 0;
            zc_thsd = 0;
            z_n2 = 0;
            z_n = 0;
            c_Ht = 0;
            zr_Min = 9999;
            zn_Min = 9999;
            foreach (var sitem in SItem)
            {
                #region  取芯修正开始
                int xQxgs = 0;
                double xHtqdz, xZj, xKyqd, xSum, xSum_htz;
                double mQxxzxs = 0;
                xSum = 0;
                xSum_htz = 0;
                if (string.IsNullOrEmpty(MItem[0]["SFQXXZ"]))
                    MItem[0]["SFQXXZ"] = "0";
                if (MItem[0]["SFQXXZ"] == "1")
                {
                    //var mrsXtab2 = mrsXtab;
                    //var mrsXtab2 = mrsXtab.Where(mrsXtab_Filter => mrsXtab_Filter["DZBH"].Contains(MItem[0]["JYDBH"]));
                    //var mrsXtab2 = mrsXtab.Where(mrsXtab_Filter => mrsXtab_Filter["SYSJBRECID"].Equals(sitem["RECID"]));
                    if (mrsXtab != null && mrsXtab.Count() > 0)
                    {
                        foreach (var mrsXtab_Filter in mrsXtab)
                        {
                            xHtqdz = calc_htzhz(mrsXtab_Filter);
                            xZj = Round(GetSafeDouble(mrsXtab_Filter["ZJ1"]) + GetSafeDouble(mrsXtab_Filter["ZJ2"]), 0) / 2;
                            if (xZj != 0)
                                xKyqd = Round(1000 * GetSafeDouble(mrsXtab_Filter["KYHZ1"]) / (3.14159 * (xZj / 2) * (xZj / 2)), 1);
                            else
                                xKyqd = 0;
                            if (xHtqdz != 0)
                            {
                                xSum = xSum + xKyqd;
                                xSum_htz = xSum_htz + xHtqdz;
                                xQxgs = xQxgs + 1;
                            }
                        }
                        mQxxzxs = Round(xSum / xQxgs - xSum_htz / xQxgs, 1);
                    }
                }
                #endregion 取芯修正结束
                mSjdj = sitem["SJDJ"];
                MItem[0]["SJDJ"] = sitem["SJDJ"];
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                if (string.IsNullOrEmpty(MItem[0]["SFQXXZ"]))
                    MItem[0]["SFQXXZ"] = "0";
                if (MItem[0]["SFQXXZ"] == "1")
                    sitem["QXHTXZXS"] = mQxxzxs.ToString();
                else
                    sitem["QXHTXZXS"] = "0";
                sitem["SJDJ"] = sitem["SJDJ"].ToUpper();
                if (!sitem["SJDJ"].StartsWith("C"))
                    sitem["SJDJ"] = "C" + sitem["SJDJ"];
                mSjdj = sitem["SJDJ"];
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj));
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
                if (string.IsNullOrEmpty(sitem["BSFS"]))
                    sitem["BSFS"] = "0";
                mBsfs = sitem["BSFS"];
                //var mrssjTable2 = mrssjTable;
                //var mrssjTable2 = mrssjTable.Where(mrssjTable_Filter => mrssjTable_Filter["DZBH"].Equals(sitem["DZBH"].ToString()));
                var mrssjTable2 = mrssjTable.Where(mrssjTable_Filter => mrssjTable_Filter["SYSJBRECID"].Equals(sitem["RECID"]));
                c_Ht = mrssjTable.Count();
                if (c_Ht == 0)
                    break;
                //sitem["LQ"] = ((GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime(DateTime.Now.ToShortDateString())).Days - (GetSafeDateTime(sitem["ZZRQ"]) - GetSafeDateTime(DateTime.Now.ToShortDateString())).Days).ToString();
                sitem["LQ"] = ((GetSafeDateTime(MItem[0]["SYRQ"]) - GetSafeDateTime(sitem["ZZRQ"])).Days).ToString();
                Ht_Qdz = new double[c_Ht + 1];
                mDyCount = 0;
                mXyCount = 0;
                moldDyCount = 0;
                moldXyCount = 0;
                s_Htzhz = 0;
                s_Htqdz = 0;
                mThsd = 0;
                c_Thsd = 0;
                s_Thsd = 0;
                r_Min = 9999;
                s_N2 = 0;
                n_Min = 9999;
                vi = 1;    //测区组数计数器
                sitem["BEIZHU"] = "";
                if (mrssjTable2 != null && mrssjTable2.Count() > 0)
                {
                    foreach (var mrssjTable_Filter in mrssjTable2)
                    {
                        if (string.IsNullOrEmpty(mJsff) || mJsff == "new")
                        {
                            //计算回弹最后值HTZHZ
                            mHtzhz = calc_htzhz(mrssjTable_Filter);
                            mHtqdz = Round(0.75 * mHtzhz + 0.0079 * mHtzhz * mHtzhz - 7.83 + GetSafeDouble(sitem["QXHTXZXS"]), 1);
                            s_Htqdz = s_Htqdz + mHtqdz;
                            if (mHtqdz < 20)
                            {
                                mXyCount = mXyCount + 1;    //实际强度<20的测区个数
                                if (!sitem["BEIZHU"].StartsWith("B"))
                                    sitem["BEIZHU"] = sitem["BEIZHU"].Trim() + "B";
                            }
                            if (mHtqdz > 110)
                            {
                                mDyCount = mDyCount + 1; //实际强度>110MPa的测区个数
                                if (!sitem["BEIZHU"].StartsWith("A"))
                                    sitem["BEIZHU"] = sitem["BEIZHU"].Trim() + "A";
                            }
                        }
                        //var zh ="";
                        var zh = sitem["ZH_G"];
                        mrssjTable_Filter["GJH"] = zh;
                        s_Htzhz = s_Htzhz + mHtzhz;
                        if (n_Min > mHtzhz)   //求n_Min
                            n_Min = mHtzhz;
                        if (r_Min > mHtqdz)
                            r_Min = mHtqdz;  //求r_Min
                        s_N2 = s_N2 + mHtqdz * mHtqdz;
                        vi = vi + 1; //计算单个测区组数
                    }
                }
                zmDycount = zmDycount + mDyCount;
                zmXycount = zmXycount + mXyCount;
                z_htzhz = z_htzhz + s_Htzhz;
                z_htqdz = z_htqdz + s_Htqdz;
                z_ht = z_ht + c_Ht;
                z_n2 = z_n2 + s_N2;
                if (zn_Min > n_Min)          //求n_Min
                    zn_Min = n_Min;
                if (zr_Min > r_Min)
                    zr_Min = r_Min;  //求r_Min

                //----综合判断----
                r_Pj = Round(s_Htqdz / c_Ht, 1);    //求r_Pj
                n_Pj = Round(s_Htzhz / c_Ht, 1);    //求n_Pj

                if (c_Ht > 1)    //求s_N
                    s_N = Round(Math.Sqrt(Math.Abs(s_N2 - c_Ht * (s_Htqdz / c_Ht) * (s_Htqdz / c_Ht)) / (c_Ht - 1)), 2);
                else
                    s_N = 0;
                if (c_Ht < 10)       //求mQdtdz
                    mQdtdz = Round(r_Min, 1);
                else
                    mQdtdz = Round(r_Pj - 1.645 * s_N, 1);
                //将计算结果赋值给主表相应字段
                sitem["PJHTZ"] = n_Pj.ToString();    //平均回弹值
                sitem["ZXHTZ"] = n_Min.ToString();    //最小回弹值
                sitem["PJQDZ"] = r_Pj.ToString();    //平均强度值  *****
                sitem["ZXQDZ"] = r_Min.ToString();   //最小强度值  *****
                sitem["CQS"] = c_Ht.ToString();    //测区数
                if (c_Ht >= 10)   //强度均方差  *****
                {
                    xx = s_N.ToString();
                    if (xx.Length >= 8)
                        s_N = GetSafeDouble(xx.Substring(0, 8));
                    else
                        s_N = GetSafeDouble(xx);
                    sitem["QDJFC"] = s_N.ToString();
                }
                else
                    sitem["QDJFC"] = "0";
                sitem["QDTDZ"] = mQdtdz.ToString();   //强度推断值  *****
                sitem["DY50GS"] = mDyCount.ToString();   //实际强度>60MPa的测区个数
                sitem["xy10gs"] = mXyCount.ToString();    //实际强度<10的测区个数
                if (sitem["BEIZHU"].Contains("B"))
                {
                    sitem["PJQDZ"] = "0";
                    sitem["ZXQDZ"] = "5";
                    sitem["QDTDZ"] = "5";
                    sitem["QDJFC"] = "0";
                    zmXycount = 1;
                }
                if (sitem["BEIZHU"].Contains("A"))
                {
                    sitem["PJQDZ"] = "0";
                    sitem["QDJFC"] = "0";
                    sitem["QDTDZ"] = r_Min.ToString();
                    zmDycount = 1;
                }
                if (mSz != 0)
                    sitem["DDSJQD"] = Round(GetSafeDouble(sitem["QDTDZ"]) / mSz * 100, 1).ToString();     //达到设计强度%
                if (Conversion.Val(sitem["DDSJQD"]) < 100)
                    mAllHg = false;
                if (sitem["BEIZHU"] == "")
                    sitem["BEIZHU"] = "----";
            }
            //批量计算
            if (MItem[0]["SFPL"] == "1")
                which = "bgght_88、bgght_1、bgght_2";
            else
                which = "bgght_88、bgght";
            //if (GetSafeDouble(MItem[0]["FJCOUNT"]) > 0)
            //{
            //    for (fji = 1; fji <= int.Parse(MItem[0]["FJCOUNT"]); fji++)
            //    {
            //        bgfjs = (10 + fji - 1).ToString();
            //        which = which + "、bgght_" + bgfjs;
            //    }
            //}
            zr_Pj = Round(z_htqdz / z_ht, 1);  //求r_Pj
            zn_Pj = Round(z_htzhz / z_ht, 1);  //求n_Pj
            if (z_ht > 1)    //求s_N
                z_n = Round(Math.Sqrt(Math.Abs(z_n2 - z_ht * (z_htqdz / z_ht) * (z_htqdz / z_ht)) / (z_ht - 1)), 2);
            else
                z_n = 0;
            mQdtdz = Round(zr_Pj - 1.645 * z_n, 1);  //求mQdtdz
            MItem[0]["PL_B_DG"] = "";
            if (zr_Pj <= 50 && z_n > 5.5 && MItem[0]["SFPL"] == "1")
            {
                MItem[0]["PL_B_DG"] = "平均值不大于50.0MPa，标准差大于5.50MPa，需按单个构件检测";
                which = "bgght_88、bgght";
                //if (int.Parse(MItem[0]["FJCOUNT"]) > 0)
                //{
                //    for (fji = 1; fji <= int.Parse(MItem[0]["FJCOUNT"]); fji++)
                //    {
                //        bgfjs = (10 + fji - 1).ToString();
                //        which = which + "、bgght_" + bgfjs;
                //    }
                //}
            }


            if (zr_Pj > 50 && z_n > 6.5)
            {
                MItem[0]["PL_B_DG"] = "平均值大于50.0MPa， 标准差大于5.50MPa，需按单个构件检测";
                //if (int.Parse(MItem[0]["FJCOUNT"]) > 0)
                //{
                //    for (fji = 1; fji <= int.Parse(MItem[0]["FJCOUNT"]); fji++)
                //    {
                //        bgfjs = (10 + fji - 1).ToString();
                //        which = which + "、bgght_" + bgfjs;
                //    }
                //}
            }
            MItem[0]["PJHTZ"] = zn_Pj.ToString();  //平均回弹值
            MItem[0]["ZXHTZ"] = zn_Min.ToString(); //最小回弹值
            MItem[0]["PJQDZ"] = zr_Pj.ToString();  //平均强度值 ****
            MItem[0]["ZXQDZ"] = zr_Min.ToString(); //最小强度值 ****
            MItem[0]["CQS"] = z_ht.ToString();    //测区数
            MItem[0]["QDJFC"] = z_n.ToString();   //强度均方差 ****
            MItem[0]["QDTDZ"] = mQdtdz.ToString(); //强度推断值 ****
            MItem[0]["DY50GS"] = zmDycount.ToString();  //实际强度>60MPa的测区个数
            MItem[0]["XY10GS"] = zmXycount.ToString();  //实际强度<10的测区个数
            if (zmXycount > 0)
            {
                MItem[0]["PJQDZ"] = "0";
                MItem[0]["ZXQDZ"] = "5";
                MItem[0]["QDTDZ"] = "5";
                MItem[0]["QDJFC"] = "0";
            }
            if (zmDycount > 0)
            {
                MItem[0]["PJQDZ"] = "0";
                MItem[0]["ZXQDZ"] = "110";
                MItem[0]["QDJFC"] = "0";
            }
            if (mSz != 0)
                MItem[0]["DDSJQD"] = Round(GetSafeDouble(MItem[0]["QDTDZ"]) / mSz * 100, 1).ToString(); //达到设计强度%
            //批量结束
            tmpstr = "";


            if (MItem[0]["JCJGMS"].Contains("A"))
            {
                MItem[0]["qdjfc"] = "0";
                tmpstr = "A：表示有测区强度超过110.0MPa";
                MItem[0]["QDTDZ"] = MItem[0]["ZXQDZ"];
            }
            if (MItem[0]["JCJGMS"].Contains("B"))
            {
                if (tmpstr == "")
                    tmpstr = "B：表示有测区强度小于20.0MPa";
                else
                    tmpstr = tmpstr.Trim() + "，" + "B：表示有测区强度小于20.0MPa";
            }
            if (MItem[0]["JCJGMS"].Trim().Length > 0)
                MItem[0]["JCJGMS"] = "说明代号：" + tmpstr.Trim() + "。";


            if (which == "1")
            {
                if (Conversion.Val(MItem[0]["DDSJQD"]) < 100)
                    mAllHg = false;
                else
                    mAllHg = true;
                MItem[0]["JCJGMS"] = "该批构件混凝土强度" + MItem[0]["QDTDZ"] + "MPa，" + "占设计强度" + MItem[0]["DDSJQD"] + " %。";
            }
            else
            {
                if (Conversion.Val(MItem[0]["DDSJQD"]) > 100)
                {
                    MItem[0]["JCJGMS"] = "该次检测混凝土强度全部大于等于设计强度。";
                    mAllHg = true;
                }
                else
                {
                    MItem[0]["JCJGMS"] = "该次检测混凝土强度部分小于设计强度。";
                    mAllHg = false;
                }
            }

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
