using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class HTZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            string mcalBh, mlongStr = "";
            double mDyCount = 0, mXyCount = 0, zmDycount, zmXycount;
            double moldDyCount, moldXyCount;
            double mHtzhz, mHtqdz;
            string mSjdjbh, mSjdj;
            int mPc, vj, vi, fji;
            double mSz = 0;
            double s_Htqdz, z_htqdz;
            double[] Ht_Qdz;
            double mThsd, mQdtdz;
            int[] e_c_ht;
            int m_Ht, c_Ht, c_Thsd, z_ht, zc_thsd;
            double r_Min, r_Pj, s_N2, s_N, n_Pj, n_Min, z_n2, z_n, zr_Min, zn_Min, zr_Pj, zn_Pj, mMinkyqd;
            double mThxzz, s_Thsd, mAvgThsd, s_Htzhz, z_thsd, z_htzhz, mkyqd1, mSum, mZj, mMj;
            int vp;
            string xx, tmpstr;
            string mMaxBgbh;
            string mJsff = "", bgfjs;
            string mBsfs;
            bool mAllHg;
            bool mGetBgbh;
            bool mSFwc;
            double[] mhtzxzArray;
            bool sign1, sign2, sign3;
            sign1 = false;
            sign2 = false;
            sign3 = false;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_HTZ_DJ"];
            var mrsEditXs = dataExtra["BZ_EDITXS"];
            var mrsEditH = dataExtra["BZ_EDITH"];
            var mrsEditS = dataExtra["BZ_EDITS"];
            var mrsHtHsb = dataExtra["BZ_HTHSB"];
            var mrsHtHsbNew = dataExtra["BZ_HTHSBNEW"];
            var mrsbsHtHsb = dataExtra["BZ_BSHTHSB"];
            var MItem = data["M_HTZ"];
            var mitem = MItem[0];
            var SItem = data["S_HTZ"];
            var mrsXtab = data["X_HT1"];
            var mrssjTable = data["Y_HT1"];
            #endregion

            #region  局部函数
            //非水平方向修正
            Func<IList<IDictionary<string, string>>, double, int, double> edith =
                delegate (IList<IDictionary<string, string>> mrsEditH_Filter, double mHtzhz_fun, int mCsjd_fun)
                {
                    double mHtzhz0_fun = 0;
                    double mHtzhz1_fun = 0;
                    double mAdd0_fun = 0;
                    double mAdd1_fun = 0;
                    double mAdd_X_fun = 0;
                    var mrsEditH_find = mrsEditH_Filter.FirstOrDefault(x => GetSafeDouble(x["RMA"]).Equals(mHtzhz_fun));
                    if (mrsEditH_find != null && mrsEditH_find.Count > 0)
                    {
                        switch (mCsjd_fun)
                        {
                            case 90:
                                mHtzhz_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find["D90"]);
                                break;
                            case 60:
                                mHtzhz_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find["D60"]);
                                break;
                            case 45:
                                mHtzhz_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find["D45"]);
                                break;
                            case -30:
                                mHtzhz_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find["D_30"]);
                                break;
                            case -45:
                                mHtzhz_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find["D_45"]);
                                break;
                            case -60:
                                mHtzhz_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find["D_60"]);
                                break;
                            case -90:
                                mHtzhz_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find["D_90"]);
                                break;
                        }
                    }
                    else
                    {
                        mHtzhz0_fun = (int)mHtzhz_fun;
                        mHtzhz1_fun = (int)mHtzhz_fun + 1;
                        if (mHtzhz_fun < 20)
                        {
                            mHtzhz0_fun = 20;
                            mHtzhz1_fun = 20;
                        }
                        if (mHtzhz_fun > 50)
                        {
                            mHtzhz0_fun = 50;
                            mHtzhz1_fun = 50;
                        }
                        var mrsEditH_find2 = mrsEditH_Filter.FirstOrDefault(x => GetSafeDouble(x["RMA"]).Equals(mHtzhz0_fun));
                        if (mrsEditH_find2 != null && mrsEditH_find2.Count > 0)
                        {
                            switch (mCsjd_fun)
                            {
                                case 90:
                                    mAdd0_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find2["D90"]);
                                    break;
                                case 60:
                                    mAdd0_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find2["D60"]);
                                    break;
                                case 45:
                                    mAdd0_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find2["D45"]);
                                    break;
                                case -30:
                                    mAdd0_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find2["D_30"]);
                                    break;
                                case -45:
                                    mAdd0_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find2["D_45"]);
                                    break;
                                case -60:
                                    mAdd0_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find2["D_60"]);
                                    break;
                                case -90:
                                    mAdd0_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find2["D_90"]);
                                    break;
                                default:
                                    mAdd0_fun = 0;
                                    break;
                            }
                        }
                        var mrsEditH_find3 = mrsEditH_Filter.FirstOrDefault(x => GetSafeDouble(x["RMA"]).Equals(mHtzhz1_fun));
                        if (mrsEditH_find3 != null && mrsEditH_find3.Count > 0)
                        {
                            switch (mCsjd_fun)
                            {
                                case 90:
                                    mAdd1_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find3["D90"]);
                                    break;
                                case 60:
                                    mAdd1_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find3["D60"]);
                                    break;
                                case 45:
                                    mAdd1_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find3["D45"]);
                                    break;
                                case -30:
                                    mAdd1_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find3["D_30"]);
                                    break;
                                case -45:
                                    mAdd1_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find3["D_45"]);
                                    break;
                                case -60:
                                    mAdd1_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find3["D_60"]);
                                    break;
                                case -90:
                                    mAdd1_fun = mHtzhz_fun + GetSafeDouble(mrsEditH_find3["D_90"]);
                                    break;
                                default:
                                    mAdd1_fun = 0;
                                    break;
                            }
                        }
                        mAdd_X_fun = mAdd0_fun - (mAdd0_fun - mAdd1_fun) * (mHtzhz0_fun - mHtzhz_fun) / -1;
                        mHtzhz_fun = mHtzhz_fun + mAdd_X_fun;
                    }
                    return mHtzhz_fun;
                };

            //查找回弹换算表(老方法2002.10.01之前)，获取回弹强度值
            Func<IDictionary<string, string>, double, double, double, string> getQdz =
                delegate (IDictionary<string, string> mrsHtHsb_Filter, double mThsd_fun, double mDyCount_fun, double mXyCount_fun)
                {
                    string[] fldName = new string[14];
                    int subNum;
                    string maybe_Fld, just_Fld;
                    double ret_Val = 0;
                    double mGo_Thsd;
                    double mRm = 0;
                    fldName[1] = "D_0";
                    fldName[2] = "D_5";
                    fldName[3] = "D_10";
                    fldName[4] = "D_15";
                    fldName[5] = "D_20";
                    fldName[6] = "D_25";
                    fldName[7] = "D_30";
                    fldName[8] = "D_35";
                    fldName[9] = "D_40";
                    fldName[10] = "D_45";
                    fldName[11] = "D_50";
                    fldName[12] = "D_55";
                    fldName[13] = "D_60";
                    if (mrsHtHsb_Filter != null && mrsHtHsb_Filter.Count > 0)
                        mRm = GetSafeDouble(mrsHtHsb_Filter["RM"]);
                    mGo_Thsd = mThsd_fun;
                    subNum = GetSafeInt((mThsd_fun * 10 / 5 + 1).ToString());
                    maybe_Fld = fldName[subNum];
                    if (mRm <= 23.6)
                    {
                        just_Fld = maybe_Fld;
                        ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                        while (ret_Val == 0)
                        {
                            mGo_Thsd = mGo_Thsd - 0.5;
                            subNum = GetSafeInt((mGo_Thsd * 10 / 5 + 1).ToString());
                            just_Fld = fldName[subNum];
                            ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                        }
                        if (just_Fld != maybe_Fld)
                            mXyCount_fun = mXyCount_fun + 1;
                    }
                    else if (mRm >= 23.601 && mRm <= 44.199)
                    {
                        just_Fld = maybe_Fld;
                        ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                    }
                    else if (mRm >= 44.2)
                    {
                        just_Fld = maybe_Fld;
                        ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                        while (ret_Val == 0)
                        {
                            mGo_Thsd = mGo_Thsd + 0.5;
                            subNum = GetSafeInt((mGo_Thsd * 10 / 5 + 1).ToString());
                            just_Fld = fldName[subNum];
                            ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                        }
                        if (just_Fld != maybe_Fld)
                            mDyCount_fun = mDyCount_fun + 1;
                    }
                    return ret_Val + "@" + mDyCount_fun + "@" + mXyCount_fun;
                };

            //查找回弹换算表(2001-10-01之后的新方法)，获取回弹强度值
            Func<IDictionary<string, string>, double, double, double, string> getQdzNew =
                delegate (IDictionary<string, string> mrsHtHsb_Filter, double mThsd_fun, double mDyCount_fun, double mXyCount_fun)
                {
                    string[] fldName = new string[14];
                    int subNum;
                    string maybe_Fld, just_Fld;
                    double ret_Val = 0;
                    double mGo_Thsd;
                    double mRm = 0;
                    fldName[1] = "D_0";
                    fldName[2] = "D_5";
                    fldName[3] = "D_10";
                    fldName[4] = "D_15";
                    fldName[5] = "D_20";
                    fldName[6] = "D_25";
                    fldName[7] = "D_30";
                    fldName[8] = "D_35";
                    fldName[9] = "D_40";
                    fldName[10] = "D_45";
                    fldName[11] = "D_50";
                    fldName[12] = "D_55";
                    fldName[13] = "D_60";
                    if (mrsHtHsb_Filter != null && mrsHtHsb_Filter.Count > 0)
                        mRm = GetSafeDouble(mrsHtHsb_Filter["RM"]);
                    mGo_Thsd = mThsd_fun;
                    subNum = GetSafeInt((mThsd_fun * 10 / 5 + 1).ToString());
                    maybe_Fld = fldName[subNum];
                    if (mRm <= 23.8)
                    {
                        just_Fld = maybe_Fld;
                        ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                        while (ret_Val == 0)
                        {
                            mGo_Thsd = mGo_Thsd - 0.5;
                            subNum = GetSafeInt((mGo_Thsd * 10 / 5 + 1).ToString());
                            just_Fld = fldName[subNum];
                            ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                        }
                        if (just_Fld != maybe_Fld)
                            mXyCount_fun = mXyCount_fun + 1;
                    }
                    else if (mRm >= 23.801 && mRm <= 48.199)
                    {
                        just_Fld = maybe_Fld;
                        ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                    }
                    else if (mRm >= 48.2)
                    {
                        just_Fld = maybe_Fld;
                        ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                        while (ret_Val == 0)
                        {
                            mGo_Thsd = mGo_Thsd + 0.5;
                            subNum = GetSafeInt((mGo_Thsd * 10 / 5 + 1).ToString());
                            just_Fld = fldName[subNum];
                            ret_Val = GetSafeDouble(mrsHtHsb_Filter[just_Fld]);
                        }
                        if (just_Fld != maybe_Fld)
                            mDyCount_fun = mDyCount_fun + 1;
                    }
                    return ret_Val + "@" + mDyCount_fun + "@" + mXyCount_fun;
                };

            Func<IList<IDictionary<string, string>>, double, double, double, double, string, string> hthsb =
                delegate (IList<IDictionary<string, string>> mrsHtHsbNew_Filter, double mHtzhz_fun, double mThsd_fun, double mDyCount_fun, double mXyCount_fun, string mJsff_fun)
                {
                    double mHtzhz0_fun = 0;
                    double mHtzhz1_fun = 0;
                    double mHtqdz_fun = 0;
                    if (mJsff_fun.Equals("old"))
                    {
                        if (mHtzhz_fun < 20)
                            mHtzhz_fun = 20;
                        if (mHtzhz_fun > 55.8)
                            mHtzhz_fun = 55.8;
                    }
                    else if (mJsff_fun.Equals("new") || mJsff_fun == "")
                    {
                        if (mHtzhz_fun < 20)
                            mHtzhz_fun = 20;
                        if (mHtzhz_fun > 60)
                            mHtzhz_fun = 60;
                    }
                    string ENDCHAR = Math.Round(mHtzhz_fun, 1).ToString("0.0").Split('.')[1];
                    if (ENDCHAR == "1" || ENDCHAR == "3" || ENDCHAR == "7" || ENDCHAR == "9")
                    {
                        //不在换算表中
                        mHtzhz0_fun = Convert.ToDouble((decimal)mHtzhz_fun - (decimal)0.1);
                        mHtzhz1_fun = Convert.ToDouble((decimal)mHtzhz_fun + (decimal)0.1);
                        var mrsHtHsbNew_Filter_dic = mrsHtHsbNew_Filter.FirstOrDefault(u => u["RM"].Contains(mHtzhz0_fun.ToString()));
                        double MHTQDZ0 = 0;
                        if (mJsff_fun == "old")
                        {//MHTQDZ0
                            string ret_val = getQdz(mrsHtHsbNew_Filter_dic, mThsd_fun, mDyCount_fun, mXyCount_fun);
                            MHTQDZ0 = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                        }
                        if (mJsff_fun == "" || mJsff_fun == "new")
                        {
                            string ret_val = getQdzNew(mrsHtHsbNew_Filter_dic, mThsd_fun, mDyCount_fun, mXyCount_fun);
                            MHTQDZ0 = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                        }
                        var mrsHtHsbNew_Filter_dic2 = mrsHtHsbNew_Filter.FirstOrDefault(u => u["RM"].Contains(mHtzhz1_fun.ToString()));
                        double MHTQDZ1 = 0;
                        if (mJsff_fun == "old")
                        {
                            string ret_val = getQdz(mrsHtHsbNew_Filter_dic2, mThsd_fun, mDyCount_fun, mXyCount_fun);
                            MHTQDZ1 = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                        }
                        if (mJsff_fun == "" || mJsff_fun == "new")
                        {
                            string ret_val = getQdzNew(mrsHtHsbNew_Filter_dic2, mThsd_fun, mDyCount_fun, mXyCount_fun);
                            MHTQDZ1 = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                        }
                        mHtqdz_fun = Math.Round((MHTQDZ0 + MHTQDZ1) / 2, 1);
                    }
                    else
                    {
                        //在换算表中
                        var mrsHtHsbNew_Filter_dic = mrsHtHsbNew_Filter.FirstOrDefault(u => u["RM"].Contains(mHtzhz_fun.ToString()));
                        if (mJsff_fun == "old")
                        {
                            string ret_val = getQdz(mrsHtHsbNew_Filter_dic, mThsd_fun, mDyCount_fun, mXyCount_fun);
                            mHtqdz_fun = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                        }
                        if (mJsff_fun == "" || mJsff_fun == "new")
                        {
                            string ret_val = getQdzNew(mrsHtHsbNew_Filter_dic, mThsd_fun, mDyCount_fun, mXyCount_fun);
                            mHtqdz_fun = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                        }
                    }
                    return mHtqdz_fun + "@" + mDyCount_fun + "@" + mXyCount_fun;
                };

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

            //端面修正
            Func<IList<IDictionary<string, string>>, double, string, double> edits =
                delegate (IList<IDictionary<string, string>> mrsEditS_Filter, double mHtzhz_fun, string mCmzt_fun)
                {
                    double mHtzhz0_fun = 0;
                    double mHtzhz1_fun = 0;
                    double mAdd0_fun = 0;
                    double mAdd1_fun = 0;
                    double mAdd_X_fun = 0;
                    var mrsEditS_find = mrsEditS_Filter.FirstOrDefault(x => x["RM"].Equals(mHtzhz_fun.ToString()));
                    mCmzt_fun = mCmzt_fun.ToUpper().Trim();
                    if (mrsEditS_find != null && mrsEditS_find.Count > 0)
                    {
                        if (mCmzt_fun.Equals("底面") || mCmzt_fun.Equals("D"))
                            mHtzhz_fun = mHtzhz_fun + GetSafeDouble(mrsEditS_find["RB"]);
                        if (mCmzt_fun.Equals("表面") || mCmzt_fun.Equals("B"))
                            mHtzhz_fun = mHtzhz_fun + GetSafeDouble(mrsEditS_find["RT"]);
                    }
                    else
                    {
                        mHtzhz0_fun = (int)mHtzhz_fun;
                        mHtzhz1_fun = (int)mHtzhz_fun + 1;
                        if (mHtzhz_fun < 20)
                        {
                            mHtzhz0_fun = 20;
                            mHtzhz1_fun = 20;
                        }
                        if (mHtzhz_fun > 50)
                        {
                            mHtzhz0_fun = 50;
                            mHtzhz1_fun = 50;
                        }
                        var mrsEditS_find2 = mrsEditS_Filter.FirstOrDefault(x => GetSafeDouble(x["RM"]).Equals(mHtzhz0_fun));
                        if (mCmzt_fun.Equals("底面") || mCmzt_fun.Equals("D"))
                            mAdd0_fun = GetSafeDouble(mrsEditS_find2["RB"]);
                        else if (mCmzt_fun.Equals("表面") || mCmzt_fun.Equals("B"))
                            mAdd0_fun = GetSafeDouble(mrsEditS_find2["RT"]);
                        else
                            mAdd0_fun = 0;
                        var mrsEditS_find3 = mrsEditS_Filter.FirstOrDefault(x => GetSafeDouble(x["RM"]).Equals(mHtzhz1_fun));
                        if (mCmzt_fun.Equals("底面") || mCmzt_fun.Equals("D"))
                            mAdd1_fun = GetSafeDouble(mrsEditS_find3["RB"]);
                        else if (mCmzt_fun.Equals("表面") || mCmzt_fun.Equals("B"))
                            mAdd1_fun = GetSafeDouble(mrsEditS_find3["RT"]);
                        else
                            mAdd1_fun = 0;
                        mAdd_X_fun = mAdd0_fun - (mAdd0_fun - mAdd1_fun) * (mHtzhz0_fun - mHtzhz_fun) / -1;
                        mHtzhz_fun = mHtzhz_fun + mAdd_X_fun;
                    }
                    mHtzhz_fun = Math.Round(mHtzhz_fun, 1);
                    return mHtzhz_fun;
                };


            //查找泵送回弹换算表(2011-12-01之后的新方法)，获取回弹强度值
            Func<IDictionary<string, string>, double, double, double, string> getQdzbs =
                delegate (IDictionary<string, string> mrsbsHtHsb_Filter, double mThsd_fun, double mDyCount_fun, double mXyCount_fun)
                {
                    string[] fldName_fun = new string[14];
                    int subNum_fun = 0;
                    string maybe_Fld_fun, just_Fld_fun;
                    double ret_Val_fun = 0;
                    double mGo_Thsd_fun = 0;
                    double mRm_fun;
                    fldName_fun[1] = "D_0";
                    fldName_fun[2] = "D_5";
                    fldName_fun[3] = "D_10";
                    fldName_fun[4] = "D_15";
                    fldName_fun[5] = "D_20";
                    fldName_fun[6] = "D_25";
                    fldName_fun[7] = "D_30";
                    fldName_fun[8] = "D_35";
                    fldName_fun[9] = "D_40";
                    fldName_fun[10] = "D_45";
                    fldName_fun[11] = "D_50";
                    fldName_fun[12] = "D_55";
                    fldName_fun[13] = "D_60";
                    mRm_fun = GetSafeDouble(mrsbsHtHsb_Filter["RM"]);
                    mGo_Thsd_fun = mThsd_fun;
                    subNum_fun = GetSafeInt((mThsd_fun * 10 / 5 + 1).ToString());
                    maybe_Fld_fun = fldName_fun[subNum_fun];
                    if (mRm_fun <= 20.8)
                    {
                        just_Fld_fun = maybe_Fld_fun;
                        ret_Val_fun = GetSafeDouble(mrsbsHtHsb_Filter[just_Fld_fun]);
                        while (ret_Val_fun == 0)
                        {
                            mGo_Thsd_fun = mGo_Thsd_fun - 0.5;
                            subNum_fun = GetSafeInt((mGo_Thsd_fun * 10 / 5 + 1).ToString());
                            just_Fld_fun = fldName_fun[subNum_fun];
                            ret_Val_fun = GetSafeDouble(mrsbsHtHsb_Filter[just_Fld_fun]);
                        }
                    }
                    else if (mRm_fun >= 20.801 && mRm_fun <= 46.999)
                    {
                        just_Fld_fun = maybe_Fld_fun;
                        ret_Val_fun = GetSafeDouble(mrsbsHtHsb_Filter[just_Fld_fun]);
                    }
                    else if (mRm_fun >= 47)
                    {
                        just_Fld_fun = maybe_Fld_fun;
                        ret_Val_fun = GetSafeDouble(mrsbsHtHsb_Filter[just_Fld_fun]);
                        while (ret_Val_fun == 0)
                        {
                            mGo_Thsd_fun = mGo_Thsd_fun + 0.5;
                            subNum_fun = GetSafeInt((mGo_Thsd_fun * 10 / 5 + 1).ToString());
                            just_Fld_fun = fldName_fun[subNum_fun];
                            ret_Val_fun = GetSafeDouble(mrsbsHtHsb_Filter[just_Fld_fun]);
                        }
                        if (just_Fld_fun != maybe_Fld_fun)
                            mDyCount_fun = mDyCount_fun + 1;
                    }
                    return ret_Val_fun + "@" + mDyCount_fun + "@" + mXyCount_fun;
                };

            // 进行修正,最后换算成回弹强度值(取芯修正用)
            Func<IDictionary<string, string>, string, string, string> calc_qxxzqdz =
                delegate (IDictionary<string, string> mrssjTable_Filter, string mBsfs_fun, string mJsff_fun)
                {
                    string mCmzt_fun = "";
                    int mCsjd_fun = 0;
                    double mThsd_fun = 0;
                    //double moldDyCount, moldXyCount;
                    double mHt_Qdz_fun;
                    double mHtzhz_fun = 0;
                    double mDyCount_fun = 0;
                    double mXyCount_fun = 0;
                    double moldDyCount_fun = 0;
                    double moldXyCount_fun = 0;
                    double mHtqdz_fun = 0;


                    mHtzhz_fun = GetSafeDouble(mrssjTable_Filter["HTZHZ"]);
                    if (!string.IsNullOrEmpty(mrssjTable_Filter["CSJD"]))
                        mCsjd_fun = GetSafeInt(mrssjTable_Filter["CSJD"]);
                    if (!string.IsNullOrEmpty(mrssjTable_Filter["CMZT"]))
                        mCmzt_fun = mrssjTable_Filter["CMZT"].ToString();
                    mThsd_fun = GetSafeDouble(mrssjTable_Filter["THSD"]);
                    mHtzhz_fun = edith(mrsEditH, mHtzhz_fun, mCsjd_fun);
                    mHtzhz_fun = edits(mrsEditS, mHtzhz_fun, mCmzt_fun);

                    moldDyCount_fun = mDyCount_fun;
                    moldXyCount_fun = mXyCount_fun;
                    if (mBsfs_fun == "0")
                    {
                        //mrsHtHsbNew
                        string ret_val = hthsb(mrsHtHsbNew, mHtzhz_fun, mThsd_fun, mDyCount_fun, mXyCount_fun, mJsff_fun);
                        mHtqdz_fun = GetSafeDouble(ret_val.Split('@')[0]);
                        mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                        mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                    }
                    else
                    {
                        //mrsbsHtHsb
                        string ret_val = hthsb(mrsbsHtHsb, mHtzhz_fun, mThsd_fun, mDyCount_fun, mXyCount_fun, mJsff_fun);
                        mHtqdz_fun = GetSafeDouble(ret_val.Split('@')[0]);
                        mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                        mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                    }
                    mHt_Qdz_fun = Math.Round(mHtqdz_fun, 1);
                    mrssjTable_Filter["HTQDZ"] = !string.IsNullOrEmpty(mHtqdz_fun.ToString()) ? mHtqdz_fun.ToString() : "0";
                    mrssjTable_Filter["XZHQDZ"] = mHt_Qdz_fun.ToString();
                    return mHtqdz_fun + "@" + mDyCount_fun + "@" + mXyCount_fun;
                };

            Func<IList<IDictionary<string, string>>, double, double, double, double, string, string> bshthsb =
                delegate (IList<IDictionary<string, string>> mrsbsHtHsb_Filter, double mHtzhz_fun, double mThsd_fun, double mDyCount_fun, double mXyCount_fun, string mJsff_fun)
                {
                    double mHtzhz0_fun = 0;
                    double mHtzhz1_fun = 0;
                    double mHtqdz_fun = 0;
                    double MHTQDZ0 = 0;
                    double MHTQDZ1 = 0;
                    if (mJsff_fun == "" || mJsff_fun == "new")
                    {
                        if (mHtzhz_fun < 18.6)
                            mHtzhz_fun = 18.6;

                        if (mHtzhz_fun > 52.8)
                            mHtzhz_fun = 52.8;
                    }
                    string ENDCHAR = Math.Round(mHtzhz_fun, 1).ToString("0.0").Split('.')[1];
                    if (ENDCHAR == "1" || ENDCHAR == "3" || ENDCHAR == "5" || ENDCHAR == "7" || ENDCHAR == "9")
                    {
                        //不在换算表中Begin
                        mHtzhz0_fun = Convert.ToDouble((decimal)mHtzhz_fun - (decimal)0.1);
                        mHtzhz1_fun = Convert.ToDouble((decimal)mHtzhz_fun + (decimal)0.1);
                        var mrsbsHtHsb_find = mrsbsHtHsb_Filter.FirstOrDefault(u => GetSafeDouble(u["RM"]).Equals(mHtzhz0_fun));
                        if (mJsff_fun == "" || mJsff_fun == "new")
                        {
                            string ret_val = getQdzbs(mrsbsHtHsb_find, mThsd_fun, mDyCount_fun, mXyCount_fun);
                            MHTQDZ0 = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                        }
                        var mrsbsHtHsb_find2 = mrsbsHtHsb_Filter.FirstOrDefault(u => GetSafeDouble(u["RM"]).Equals(mHtzhz1_fun));
                        if (mJsff_fun == "" || mJsff_fun == "new")
                        {
                            string ret_val = getQdzbs(mrsbsHtHsb_find2, mThsd_fun, mDyCount_fun, mXyCount_fun);
                            MHTQDZ1 = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                        }
                        mHtqdz_fun = Math.Round((MHTQDZ0 + MHTQDZ1) / 2, 1);
                    }
                    else
                    {
                        //在换算表中Begin
                        var mrsbsHtHsb_find = mrsbsHtHsb_Filter.FirstOrDefault(u => GetSafeDouble(u["RM"]).Equals(mHtzhz_fun));
                        if (mJsff_fun == "" || mJsff_fun == "new")
                        {
                            string ret_val = getQdzbs(mrsbsHtHsb_find, mThsd_fun, mDyCount_fun, mXyCount_fun);
                            mHtqdz_fun = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                        }
                    }
                    return mHtqdz_fun + "@" + mDyCount_fun + "@" + mXyCount_fun;
                };

            //泵送混凝土回弹强度值修正
            Func<IDictionary<string, string>, IDictionary<string, string>, double, double, double> bsQdzXz =
                delegate (IDictionary<string, string> mrsmainTable_Filter, IDictionary<string, string> mrssubtable_Filter, double mQdz_fun, double mThsd_fun)
                {
                    double bsQdzXz_ret = 0;
                    if (mThsd_fun == 0 || mThsd_fun == 0.5 || mThsd_fun == 1)
                    {
                        if (mQdz_fun <= 40)
                            bsQdzXz_ret = mQdz_fun + 4.5;
                        else if (mQdz_fun > 40 && mQdz_fun < 45)
                            bsQdzXz_ret = mQdz_fun + 4.5 - (4.5 - 3) * (40 - mQdz_fun) / (40 - 45);
                        else if (mQdz_fun == 45)
                            bsQdzXz_ret = mQdz_fun + 3;
                        else if (mQdz_fun > 45 && mQdz_fun < 50)
                            bsQdzXz_ret = mQdz_fun + 3 - (3 - 1.5) * (45 - mQdz_fun) / (45 - 50);
                        else if (mQdz_fun == 50)
                            bsQdzXz_ret = mQdz_fun + 1.5;
                        else if (mQdz_fun > 50 && mQdz_fun < 55)
                            bsQdzXz_ret = mQdz_fun + 1.5 - (1.5 - 0) * (50 - mQdz_fun) / (50 - 55);
                        else if (mQdz_fun >= 55)
                            bsQdzXz_ret = mQdz_fun;
                    }
                    else if (mThsd_fun == 1.5 || mThsd_fun == 2)
                    {
                        if (mQdz_fun <= 30)
                            bsQdzXz_ret = mQdz_fun + 3;
                        else if (mQdz_fun > 30 && mQdz_fun < 35)
                            bsQdzXz_ret = mQdz_fun + 3 - (3 - 1.5) * (30 - mQdz_fun) / (30 - 35);
                        else if (mQdz_fun == 35)
                            bsQdzXz_ret = mQdz_fun + 1.5;
                        else if (mQdz_fun > 35 && mQdz_fun <= 40)
                            bsQdzXz_ret = mQdz_fun + 1.5 - (1.5 - 0) * (35 - mQdz_fun) / (35 - 40);
                        else if (mQdz_fun > 40)
                            bsQdzXz_ret = mQdz_fun;
                    }
                    else
                    {
                        bsQdzXz_ret = mQdz_fun;
                        if (mrssubtable_Filter["BEIZHU"].StartsWith("C"))
                        { }
                        else
                            mrssubtable_Filter["BEIZHU"] = mrssubtable_Filter["BEIZHU"].Trim() + "C";
                        //if (mrsmainTable_Filter["JSBEIZHU"].StartsWith("C"))
                        //{ }
                        //else
                        //    mrsmainTable_Filter["JSBEIZHU"] = mrsmainTable_Filter["JSBEIZHU"].Trim() + "C";
                    }
                    return bsQdzXz_ret;
                };

            //进行修正,最后换算成回弹强度值
            Func<IDictionary<string, string>, IDictionary<string, string>, IDictionary<string, string>, double> calc_htqdzbs =
                delegate (IDictionary<string, string> mrsmainTable_Filter, IDictionary<string, string> mrssubtable_Filter, IDictionary<string, string> mrssjTable_Filter)
                {
                    double mHtzhz_fun = 0;
                    int mCsjd_fun = 0;
                    string mCmzt_fun = "";
                    double mThsd_fun = 0;
                    double mQxhtxzxs_fun;
                    double mDyCount_fun = 0;
                    double mXyCount_fun = 0;
                    double moldDyCount_fun = 0;
                    double moldXyCount_fun = 0;
                    string mJsff_fun = "";
                    double mHt_Qdz_fun = 0;
                    double mHtqdz_fun = 0;
                    double s_Htqdz_fun = 0;
                    if (string.IsNullOrEmpty(mrssubtable_Filter["QXHTXZXS"]))
                    {
                        if (mrsmainTable_Filter["PDBZ"].Contains("2011"))
                            mrssubtable_Filter["QXHTXZXS"] = "0";
                        else
                            mrssubtable_Filter["QXHTXZXS"] = "1";
                    }
                    mQxhtxzxs_fun = GetSafeDouble(mrssubtable_Filter["QXHTXZXS"]);
                    mHtzhz_fun = GetSafeDouble(mrssjTable_Filter["HTZHZ"]);
                    if (!string.IsNullOrEmpty(mrssjTable_Filter["CSJD"]))
                        mCsjd_fun = GetSafeInt(mrssjTable_Filter["CSJD"]);
                    if (!string.IsNullOrEmpty(mrssjTable_Filter["CMZT"]))
                        mCmzt_fun = mrssjTable_Filter["CMZT"];
                    mThsd_fun = Round(GetSafeDouble(mrssjTable_Filter["THSD"]) * 2, 0) / 2;
                    mHtzhz_fun = edith(mrsEditH, mHtzhz_fun, mCsjd_fun);
                    mHtzhz_fun = edits(mrsEditS, mHtzhz_fun, mCmzt_fun);
                    moldDyCount_fun = mDyCount_fun;
                    moldXyCount_fun = mXyCount_fun;
                    string ret_val = bshthsb(mrsbsHtHsb, mHtzhz_fun, mThsd_fun, mDyCount_fun, mXyCount_fun, mJsff_fun);
                    mHtqdz_fun = GetSafeDouble(ret_val.Split('@')[0]);
                    mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                    mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                    if (mDyCount_fun > moldDyCount_fun)
                    {
                        if (mrssubtable_Filter["BEIZHU"].StartsWith("A"))
                        { }
                        else
                            mrssubtable_Filter["BEIZHU"] = mrssubtable_Filter["BEIZHU"].Trim() + "A"; //有强度超过60.0MPa的
                        //if (mrssubtable_Filter["JSBEIZHU"].StartsWith("A"))
                        //{ }
                        //else
                        //    mrssubtable_Filter["JSBEIZHU"] = mrssubtable_Filter["JSBEIZHU"].Trim() + "A";
                    }
                    if (mXyCount_fun > moldXyCount_fun)
                    {
                        if (mrssubtable_Filter["BEIZHU"].StartsWith("B"))
                        { }
                        else
                            mrssubtable_Filter["BEIZHU"] = mrssubtable_Filter["BEIZHU"].Trim() + "B"; //有强度小于10.0MPa的
                        //if (mrssubtable_Filter["JSBEIZHU"].StartsWith("B"))
                        //{ }
                        //else
                        //    mrssubtable_Filter["JSBEIZHU"] = mrssubtable_Filter["JSBEIZHU"].Trim() + "B";
                    }

                    if (mHtzhz_fun < 18.6)
                    {
                        if (mrssubtable_Filter["BEIZHU"].StartsWith("B"))
                        { }
                        else
                            mrssubtable_Filter["BEIZHU"] = mrssubtable_Filter["BEIZHU"].Trim() + "B";  //有强度小于10.0MPa的
                        //if (mrssubtable_Filter["JSBEIZHU"].StartsWith("B"))
                        //{ }
                        //else
                        //    mrssubtable_Filter["JSBEIZHU"] = mrssubtable_Filter["JSBEIZHU"].Trim() + "B";
                    }
                    if (string.IsNullOrEmpty(mQxhtxzxs_fun.ToString()) || mQxhtxzxs_fun == 0)
                        mHt_Qdz_fun = Math.Round(mHtqdz_fun, 1);
                    else
                    {
                        if (mrsmainTable_Filter["PDBZ"].StartsWith("2011"))
                            mHt_Qdz_fun = Math.Round(mHtqdz_fun + mQxhtxzxs_fun, 1);
                        else
                            mHt_Qdz_fun = Math.Round(mHtqdz_fun * mQxhtxzxs_fun, 1);
                    }
                    s_Htqdz_fun = s_Htqdz_fun + mHt_Qdz_fun;
                    mrssjTable_Filter["HTQDZ"] = !string.IsNullOrEmpty(mHtqdz_fun.ToString()) ? mHtqdz_fun.ToString() : "0";
                    mrssjTable_Filter["XZHQDZ"] = mHt_Qdz_fun.ToString();
                    mrssubtable_Filter["THSD"] = mThsd_fun.ToString();
                    return mHt_Qdz_fun;
                };

            //进行修正,最后换算成回弹强度值
            Func<IDictionary<string, string>, IDictionary<string, string>, IDictionary<string, string>, double> calc_htqdz =
                delegate (IDictionary<string, string> mrsmainTable_Filter, IDictionary<string, string> mrssubtable_Filter, IDictionary<string, string> mrssjTable_Filter)
                {
                    string mCmzt_fun;
                    int mCsjd_fun;
                    double mQxhtxzxs_fun;
                    double mThsd_fun, mHtzhz_fun, mHtqdz_fun;
                    double mDyCount_fun = 0;
                    double mXyCount_fun = 0;
                    double moldDyCount_fun = 0;
                    double moldXyCount_fun = 0;
                    double mHt_Qdz_fun = 0;
                    mHtzhz_fun = 0;
                    mCsjd_fun = 0;
                    mCmzt_fun = "";
                    mThsd_fun = 0;

                    if (string.IsNullOrEmpty(mrssubtable_Filter["QXHTXZXS"]))
                    {
                        if (mrsmainTable_Filter["PDBZ"].StartsWith("2011"))
                            mrssubtable_Filter["QXHTXZXS"] = "0";
                        else
                            mrssubtable_Filter["QXHTXZXS"] = "1";
                    }
                    mQxhtxzxs_fun = GetSafeDouble(mrssubtable_Filter["QXHTXZXS"]);
                    mHtzhz_fun = GetSafeDouble(mrssjTable_Filter["HTZHZ"]);
                    if (!string.IsNullOrEmpty(mrssjTable_Filter["CSJD"]))
                        mCsjd_fun = GetSafeInt(mrssjTable_Filter["CSJD"]);
                    if (!string.IsNullOrEmpty(mrssjTable_Filter["CMZT"]))
                        mCmzt_fun = mrssjTable_Filter["CMZT"];
                    mThsd_fun = GetSafeDouble(mrssjTable_Filter["THSD"]);
                    mHtzhz_fun = edith(mrsEditH, mHtzhz_fun, mCsjd_fun);
                    mHtzhz_fun = edits(mrsEditS, mHtzhz_fun, mCmzt_fun);
                    moldDyCount_fun = mDyCount_fun;
                    moldXyCount_fun = mXyCount_fun;
                    string ret_val = hthsb(mrsHtHsbNew, mHtzhz_fun, mThsd_fun, mDyCount, mXyCount, mJsff);
                    mHtqdz_fun = GetSafeDouble(ret_val.Split('@')[0]);
                    mDyCount_fun = GetSafeDouble(ret_val.Split('@')[1]);
                    mXyCount_fun = GetSafeDouble(ret_val.Split('@')[2]);
                    if (mDyCount_fun > moldDyCount_fun)
                    {
                        if (mrssubtable_Filter["BEIZHU"].StartsWith("A"))
                        { }
                        else
                            mrssubtable_Filter["BEIZHU"] = mrssubtable_Filter["BEIZHU"].Trim() + "A"; //有强度超过60.0MPa的
                        //if (mrssubtable_Filter["JSBEIZHU"].StartsWith("A"))
                        //{ }
                        //else
                        //    mrssubtable_Filter["JSBEIZHU"] = mrssubtable_Filter["JSBEIZHU"].Trim() + "A";
                    }
                    if (mXyCount_fun > moldXyCount_fun)
                    {
                        if (mrssubtable_Filter["BEIZHU"].StartsWith("B"))
                        { }
                        else
                            mrssubtable_Filter["BEIZHU"] = mrssubtable_Filter["BEIZHU"].Trim() + "B"; //有强度小于10.0MPa的
                        //if (mrssubtable_Filter["JSBEIZHU"].StartsWith("B"))
                        //{ }
                        //else
                        //    mrssubtable_Filter["JSBEIZHU"] = mrssubtable_Filter["JSBEIZHU"].Trim() + "B";
                    }

                    if (mrssubtable_Filter["BSFS"] == "1")
                        mHtqdz_fun = bsQdzXz(mrsmainTable_Filter, mrssubtable_Filter, mHtqdz_fun, mThsd_fun);


                    if (string.IsNullOrEmpty(mQxhtxzxs_fun.ToString()) || mQxhtxzxs_fun == 0 || string.IsNullOrEmpty(mrsmainTable_Filter["SFQXXZ"]) || mrsmainTable_Filter["SFQXXZ"] == "0")
                    {
                        mHtqdz_fun = Math.Round(mHtqdz_fun, 1);
                        mHt_Qdz_fun = mHtqdz_fun;
                    }
                    else
                    {
                        if (mrsmainTable_Filter["PDBZ"].StartsWith("2011"))
                            mHt_Qdz_fun = Math.Round(mHtqdz_fun + mQxhtxzxs_fun, 1);
                        else
                            mHt_Qdz_fun = Math.Round(mHtqdz_fun * mQxhtxzxs_fun, 1);
                    }
                    mrssjTable_Filter["HTQDZ"] = !string.IsNullOrEmpty(mHtqdz_fun.ToString()) ? mHtqdz_fun.ToString() : "0";
                    mrssjTable_Filter["XZHQDZ"] = mHt_Qdz_fun.ToString();
                    mrssubtable_Filter["THSD"] = mThsd_fun.ToString();
                    return mHt_Qdz_fun;
                };

            //计算回弹最后值HTZHZ
            Func<IDictionary<string, string>, double> calc_htpjz =
                delegate (IDictionary<string, string> Y_HTZ)
                {

                    double[] mhtzArray = new double[16];
                    string[] mtmpArray;
                    double mMaxHtz, mMinHtz;
                    if (string.IsNullOrEmpty(Y_HTZ["HTZ1"]))
                        Y_HTZ["HTZ1"] = "0";
                    for (int i = 1; i <= 16; i++)
                    {
                        if (i == 1)
                            mlongStr = Y_HTZ["HTZ1"];
                        else
                        {
                            if (string.IsNullOrEmpty(Y_HTZ["HTZ" + i]))
                                Y_HTZ["HTZ" + i] = "0";
                            mlongStr = mlongStr + "," + Y_HTZ["HTZ" + i];
                        }
                    }
                    mtmpArray = mlongStr.Split(',');
                    mSum = 0;
                    for (vp = 0; vp <= 15; vp++)
                    {
                        mhtzArray[vp] = GetSafeDouble(mtmpArray[vp]);
                        mSum = mSum + mhtzArray[vp];
                    }
                    Array.Sort(mhtzArray);
                    mMaxHtz = mhtzArray[15] + mhtzArray[14] + mhtzArray[13];
                    mMinHtz = mhtzArray[0] + mhtzArray[1] + mhtzArray[2];
                    mHtzhz = Round((mSum - mMaxHtz - mMinHtz) / 10, 1);
                    Y_HTZ["HTZHZ"] = mHtzhz.ToString();
                    return mHtzhz;
                };
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            double mQxxzxs = 0;
            mitem["JCJGMS"] = "";
            mSjdj = SItem[0]["SJDJ"];
            mitem["SJDJ"] = SItem[0]["SJDJ"];
            if (string.IsNullOrEmpty(mSjdj))
                mSjdj = "";
            mBsfs = SItem[0]["BSFS"];
            if (string.IsNullOrEmpty(mitem["SFQXXZ"]))
                mitem["SFQXXZ"] = "0";
            if (mitem["SFQXXZ"] == "1")
            {
                var mrsXtab2 = mrsXtab.Where(x => x["DZBH"].Contains(MItem[0]["JYDBH"]));
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj));
                mJsff = mrsDj_Filter["JSFF"];
                //var mrsXtab2 = mrsXtab.Where(mrsXtab_Filter => mrsXtab_Filter["SYSJBRECID"].Equals(sitem["RECID"]));
                int xQxgs = 0;
                double xHtqdz, xZj, xKyqd, xSum, xSum_htz;
                xSum = 0;
                xSum_htz = 0;
                foreach (var XHT1 in mrsXtab2)
                {
                    xHtqdz = calc_htzhz(XHT1);
                    string ret_val = calc_qxxzqdz(XHT1, mBsfs, mJsff);
                    xHtqdz = GetSafeDouble(ret_val.Split('@')[0]);
                    mDyCount = GetSafeDouble(ret_val.Split('@')[1]);
                    mXyCount = GetSafeDouble(ret_val.Split('@')[2]);
                    xZj = Round(Conversion.Val(XHT1["ZJ1"]) + Conversion.Val(XHT1["ZJ2"]), 0) / 2;
                    if (xZj != 0)
                        xKyqd = Round(1000 * GetSafeDouble(XHT1["KYHZ1"]) / (3.14159 * (xZj / 2) * (xZj / 2)), 1);
                    else
                        xKyqd = 0;
                    if (xHtqdz != 0)
                    {
                        if (mitem["PDBZ"].Contains("2011"))
                            xSum = xSum + xKyqd;
                        else
                            xSum = xSum + xKyqd / xHtqdz;
                        xSum_htz = xSum_htz + xHtqdz;
                        xQxgs = xQxgs + 1;
                    }
                }
                if (mitem["PDBZ"].Contains("2011"))
                    mQxxzxs = Round(xSum / xQxgs - xSum_htz / xQxgs, 1);
                else
                    mQxxzxs = Round(xSum / xQxgs, 2);
            }
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
            mSum = 0;
            zr_Min = 9999;
            zn_Min = 9999;
            mMinkyqd = 9999;
            m_Ht = SItem.Count();
            mhtzxzArray = new double[m_Ht];
            foreach (var sitem in SItem)
            {
                //---------------------是否完成检测-------------------------------------
                if (string.IsNullOrEmpty(mitem["SFQXXZ"]))
                    mitem["SFQXXZ"] = "0";
                if (mitem["SFQXXZ"] == "1")
                    sitem["QXHTXZXS"] = mQxxzxs.ToString();
                else
                {
                    if (mitem["PDBZ"].Contains("2011"))
                        sitem["QXHTXZXS"] = "0";
                    else
                        sitem["QXHTXZXS"] = "1";
                }
                sitem["SJDJ"] = sitem["SJDJ"].ToUpper();
                if (!sitem["SJDJ"].Contains("C"))
                    sitem["SJDJ"] = "C" + sitem["SJDJ"];

                mSjdj = sitem["SJDJ"];
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj));
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    mSz = Conversion.Val(mrsDj_Filter["SZ"]);
                    mJsff = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mSz = 0;
                    mJsff = "";
                    sitem["JCJG"] = "依据不详";
                    continue;
                }
                if (string.IsNullOrEmpty(sitem["BSFS"]))
                    sitem["BSFS"] = "0";


                mBsfs = sitem["BSFS"];
                var mrssjTable2 = mrssjTable.Where(x => x["DZBH"].Equals(sitem["DZBH"].ToString()));
                //var mrssjTable2 = mrssjTable.Where(mrssjTable_Filter => mrssjTable_Filter["SYSJBRECID"].Equals(sitem["RECID"]));
                c_Ht = mrssjTable2.Count();
                if (c_Ht == 0)
                {
                    continue;
                }
                try
                {
                    sitem["LQ"] = ((GetSafeDateTime(MItem[0]["SYRQ1"]) - GetSafeDateTime(DateTime.Now.ToShortDateString())).Days - (GetSafeDateTime(sitem["ZZRQ"]) - GetSafeDateTime(DateTime.Now.ToShortDateString())).Days).ToString();
                    //item["LQ"] = (GetSafeDateTime(MItem[0]["SYRQ"]).CompareTo(GetSafeDateTime(DateTime.Now.ToShortDateString())) - GetSafeDateTime(item["ZZRQ"]).CompareTo(GetSafeDateTime(DateTime.Now.ToShortDateString()))).ToString();
                }
                catch (Exception)
                {
                    sitem["LQ"] = "0";
                }
                Ht_Qdz = new double[c_Ht];
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
                vi = 1; //测区组数计数器
                sitem["BEIZHU"] = "";
                foreach (var Y_HTZ in mrssjTable2)
                {
                    //计算回弹最后值HTZHZ
                    mHtzhz = calc_htpjz(Y_HTZ);
                    //赋值数据表的报告编号
                    Y_HTZ["GJH"] = sitem["ZH"];
                    //平均值,最小值等计算
                    s_Htzhz = s_Htzhz + mHtzhz;
                    if (n_Min > mHtzhz)          //求n_Min
                        n_Min = mHtzhz;
                    vi = vi + 1; //计算单个测区组数
                }
                z_htzhz = z_htzhz + s_Htzhz;
                z_ht = z_ht + c_Ht;
                z_n2 = z_n2 + s_N2;
                if (zn_Min > n_Min)          //求n_Min
                    zn_Min = n_Min;
                if (zr_Min > r_Min)
                    zr_Min = r_Min;  //求r_Min
                                     //综合判断
                                     //平均值,最小值等计算
                n_Pj = Round(s_Htzhz / c_Ht, 1);    //求n_Pj
                mhtzxzArray[(int)GetSafeDouble(sitem["ZH"]) - 1] = n_Min;
                sitem["PJHTZ"] = n_Pj.ToString();    //平均回弹值
                sitem["ZXHTZ"] = n_Min.ToString();   //最小回弹值
                sitem["CQS"] = c_Ht.ToString();    //测区数
                //取芯
                sitem["ZJ"] = (Round(Conversion.Val(sitem["ZJ1"]) + Conversion.Val(sitem["ZJ2"]), 0) / 2).ToString("0.0");
                mZj = Conversion.Val(sitem["ZJ"]);
                mMj = 3.14159 * (mZj / 2) * (mZj / 2);
                if (mMj != 0)
                {
                    sitem["MJ"] = Round(mMj, 0).ToString();
                    //计算单组的抗压强度,并进行合格判断
                    sitem["KYHZ1"] = sitem["KYHZ1"];
                    mkyqd1 = Round(1000 * GetSafeDouble(sitem["KYHZ1"]) / mMj, 1);
                }
                else
                    mkyqd1 = 0;
                if (mkyqd1 != 0)
                {
                    if (mkyqd1 < mMinkyqd)
                        mMinkyqd = mkyqd1;
                    sitem["KYQD1"] = (mkyqd1 / 1).ToString();
                    mSum = mSum + mkyqd1;
                }
            }
            //批量计算
            if (GetSafeDouble(mitem["FJCOUNT"]) > 0)
            {
                for (fji = 1; fji <= GetSafeInt(mitem["FJCOUNT"]); fji++)
                {
                    bgfjs = (10 + fji - 1).ToString();
                }
            }
            zn_Pj = Round((z_htzhz / z_ht), 1);  //求n_Pj
            Array.Sort(mhtzxzArray);
            mitem["ZXHTZ"] = zn_Min.ToString(); //最小回弹值
            foreach (var sitem in SItem)
            {
                if (GetSafeDouble(sitem["ZXHTZ"]) == mhtzxzArray[1] && sign1 == false)
                {
                    mitem["ZXHTZ1"] = mhtzxzArray[1].ToString();
                    mitem["KYQD1"] = sitem["KYQD1"];
                    sign1 = true;
                }
                else if (GetSafeDouble(sitem["ZXHTZ"]) == mhtzxzArray[2] && !sign2)
                {
                    mitem["ZXHTZ2"] = mhtzxzArray[2].ToString();
                    mitem["KYQD2"] = sitem["KYQD1"];
                    sign2 = true;
                }
                else if (GetSafeDouble(sitem["ZXHTZ"]) == mhtzxzArray[3] && !sign3)
                {
                    mitem["ZXHTZ3"] = mhtzxzArray[3].ToString();
                    mitem["KYQD3"] = sitem["KYQD1"];
                    sign3 = true;
                }
            }
            mitem["KYPJ"] = Round(mSum / 3, 1).ToString();
            mitem["KYMIN"] = mMinkyqd.ToString();
            if (mSz != 0)
            {
                mitem["DDSJQDX"] = Round(mMinkyqd / mSz * 100, 1).ToString(); //达到设计强度%
                mitem["DDSJQDQ"] = Round(GetSafeDouble(mitem["KYPJ"]) / mSz * 100, 1).ToString();
            }
            if (GetSafeDouble(mitem["DDSJQDX"]) >= 80 && GetSafeDouble(mitem["DDSJQDQ"]) >= 88)
            { }
            else
                mAllHg = false;


            //主表总判断赋值
            if (mAllHg)
                mitem["JCJG"] = "合格";
            else
                mitem["JCJG"] = "不合格";
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
