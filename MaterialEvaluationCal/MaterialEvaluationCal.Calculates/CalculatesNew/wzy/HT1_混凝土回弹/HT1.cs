using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class HT1 : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  定义变量
            var data = retData;
            var extraDJ = dataExtra["BZ_HT1_DJ"];
            var mrsEditH = dataExtra["BZ_EDITH"];
            var mrsEditS = dataExtra["BZ_EDITS"];
            var mrsHtHsb = dataExtra["BZ_HTHSB"];
            var mrsHtHsbNew = dataExtra["BZ_HTHSBNEW"];
            var mrsbsHtHsb = dataExtra["BZ_BSHTHSB"];
            string mcalBh, mlongStr, mBsfs;
            double mDyCount = 0;
            double mXyCount = 0;
            double moldDyCount = 0;
            double moldXyCount = 0;
            double zmDycount = 0;
            double zmXycount = 0;
            double mHtzhz = 0;
            double mHtqdz = 0;
            string mSjdjbh, mSjdj;
            int mPc, vj, vi, fji;
            double mSz = 0;
            double s_Htqdz = 0;
            double z_htqdz = 0;
            bool mSFwc = true;
            Double mQxxzxs = 0;
            string mJsff = string.Empty;
            int c_Ht = 0;
            double s_Htzhz = 0;
            double n_Min = 9999;
            double r_Min = 9999;
            double zn_Min = 9999;
            double s_N2 = 0;
            double z_htzhz = 0;
            double z_ht = 0;
            double z_n2 = 0;
            double zr_Min = 9999;
            double r_Pj = 0;
            double n_Pj = 0;
            double s_N = 0;
            double mQdtdz = 0;
            string xx = string.Empty;
            bool sfcc = false;
            string bgfjs = string.Empty;
            double zr_Pj = 0;
            double zn_Pj = 0;
            double z_n = 0;
            bool mAllHg = true;
            string jcjg = "";
            string mjcjg = "不合格";
            var jsbeizhu = "";
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
                    if (ENDCHAR == "1" || ENDCHAR == "3" || ENDCHAR == "5" || ENDCHAR == "7" || ENDCHAR == "9")
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
                    //剔除3最大值  3个最小值
                    mHtzhz_fun = Math.Round((mSum_fun - mMaxHtz_fun - mMinHtz_fun) / 10, 1);
                    //mHtzhz_fun = Math.Round((mSum_fun - mMaxHtz_fun - mMinHtz_fun) / 10 * 5, 1) / 5;
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
            #endregion

            #region  计算开始
            var MItem = data["M_HT1"];
            var SItem = data["S_HT1"];
            var mrssjTable = data["Y_HT1"];
            var mrsXtab = data["X_HT1"];
            if (MItem == null || MItem.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            else
            {
                MItem[0]["JCJG"] = mjcjg;
                MItem[0]["JCJGMS"] = jsbeizhu;
            }
            //遍历每条数据
            zmDycount = 0;
            zmXycount = 0;
            z_htzhz = 0;
            z_htqdz = 0;
            z_ht = 0;
            z_n2 = 0;
            z_n = 0;
            c_Ht = 0;
            zr_Min = 9999;
            zn_Min = 9999;
            int row = 1;
            foreach (var sitem in SItem)
            {
                if (">60MPa" == sitem["QDTDZ"])
                {
                    continue;
                }
                sitem["BEIZHU"] = "";
                //计算开始
                if (string.IsNullOrEmpty(sitem["BSFS"]))
                    sitem["BSFS"] = "0";
                mBsfs = sitem["BSFS"];  //是否泵送方式
                mSjdj = sitem["SJDJ"]; //设计等级
                MItem[0]["SJDJ"] = sitem["SJDJ"];
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";

                //var mrsXtab2 = mrsXtab.Where(x => x["DZBH"].Contains(MItem[0]["JYDBH"]));
                List<IDictionary<string, string>> mrsXtab2 = new List<IDictionary<string, string>>();
                //if (mrsXtab != null && mrsXtab[0].Count > 0)
                if (mrsXtab != null && mrsXtab[0].Count > 0)
                    mrsXtab2 = mrsXtab.Where(mrsXtab_Filter => mrsXtab_Filter["SYSJBRECID"].Equals(sitem["RECID"])).ToList();
                var mrsDj_Filter = extraDJ.FirstOrDefault(x => x["MC"].Contains(mSjdj));

                if (string.IsNullOrEmpty(MItem[0]["SJTABS"]))  //数据录入使用模板
                    MItem[0]["SJTABS"] = "sjht1";
                if (string.IsNullOrEmpty(MItem[0]["SFQXXZ"]))
                    MItem[0]["SFQXXZ"] = "0";  //是否取芯修正(输入0或1)
                if (MItem[0]["SFQXXZ"] == "1")
                {
                    int xQxgs = 0;
                    double xHtqdz, xZj, xKyqd, xSum, xSum_htz;
                    xSum = 0;
                    xSum_htz = 0;

                    mJsff = mrsDj_Filter["JSFF"];
                    if (mrsXtab2 != null && mrsXtab2.Count() > 0)
                    {
                        foreach (var mrsitem in mrsXtab2)
                        {
                            xHtqdz = calc_htzhz(mrsitem);

                            string ret_val = calc_qxxzqdz(mrsitem, mBsfs, mJsff);
                            xHtqdz = GetSafeDouble(ret_val.Split('@')[0]);
                            mDyCount = GetSafeDouble(ret_val.Split('@')[1]);
                            mXyCount = GetSafeDouble(ret_val.Split('@')[2]);
                            xZj = Round(GetSafeDouble(mrsitem["ZJ1"]) + GetSafeDouble(mrsitem["ZJ2"]), 0) / 2;
                            if (xZj != 0)
                                xKyqd = Math.Round(1000 * GetSafeDouble(mrsitem["KYHZ1"]) / (3.14159 * (xZj / 2) * (xZj / 2)), 1);
                            else
                                xKyqd = 0;
                            if (xHtqdz != 0)
                            {
                                if (MItem[0]["PDBZ"].Contains("2011"))
                                    xSum = xSum + xKyqd;
                                else
                                    xSum = xSum + xKyqd / xHtqdz;
                                xSum_htz = xSum_htz + xHtqdz;
                                xQxgs = xQxgs + 1;
                            }
                        }
                    }
                    if (MItem[0]["PDBZ"].Contains("2011"))
                        mQxxzxs = Math.Round(xSum / xQxgs - xSum_htz / xQxgs, 1);
                    else
                        mQxxzxs = Round(xSum / xQxgs, 2);
                }
                //--------------获取单个项目私有的mian字段数据-----------------
                //if (string.IsNullOrEmpty(sitem["SYR"].Trim()))
                //{
                //    mSFwc = false;
                //    break;
                //}
                if (string.IsNullOrEmpty(MItem[0]["SFQXXZ"]))
                    MItem[0]["SFQXXZ"] = "0";
                if (MItem[0]["SFQXXZ"] == "1")
                    sitem["QXHTXZXS"] = mQxxzxs.ToString();
                else
                {
                    if (MItem[0]["PDBZ"].Contains("2011"))
                        sitem["QXHTXZXS"] = "0";
                    else
                        sitem["QXHTXZXS"] = "1";
                }
                sitem["SJDJ"] = sitem["SJDJ"].ToUpper();
                if (!sitem["SJDJ"].StartsWith("C"))
                {
                    sitem["SJDJ"] = "C" + sitem["SJDJ"];
                }
                mSjdj = sitem["SJDJ"];
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                //从设计等级表中取得相应的计算数值、等级标准
                if (mrsDj_Filter != null && mrsDj_Filter.Count > 0)
                {
                    mSz = GetSafeDouble(mrsDj_Filter["SZ"]);
                    mJsff = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].ToLower();
                }
                else
                {
                    mSz = 0;
                    mJsff = "";
                    jcjg = "依据不详";
                    //item["JCJG"] = "依据不详";
                    break;
                }
                if (string.IsNullOrEmpty(sitem["BSFS"]))
                    sitem["BSFS"] = "0";
                mBsfs = sitem["BSFS"];
                //var mrssjTable2 = mrssjTable.Where(x => x["DZBH"].Equals(sitem["DZBH"].ToString()));
                List<IDictionary<string, string>> mrssjTable2 = new List<IDictionary<string, string>>();
                if (mrssjTable != null && mrssjTable[0].Count > 0)
                    mrssjTable2 = mrssjTable.Where(mrssjTable_Filter => mrssjTable_Filter["SYSJBRECID"].Equals(sitem["RECID"])).ToList();
                c_Ht = mrssjTable2.Count();
                try
                {
                    sitem["LQ"] = ((GetSafeDateTime(MItem[0]["SYRQ1"]) - GetSafeDateTime(DateTime.Now.ToShortDateString())).Days - (GetSafeDateTime(sitem["ZZRQ"]) - GetSafeDateTime(DateTime.Now.ToShortDateString())).Days).ToString();
                    //item["LQ"] = (GetSafeDateTime(MItem[0]["SYRQ"]).CompareTo(GetSafeDateTime(DateTime.Now.ToShortDateString())) - GetSafeDateTime(item["zzrq"]).CompareTo(GetSafeDateTime(DateTime.Now.ToShortDateString()))).ToString();
                }
                catch (Exception)
                {
                    sitem["LQ"] = "0";
                }
                double[] Ht_Qdz = new double[c_Ht];
                mDyCount = 0;
                mXyCount = 0;
                moldDyCount = 0;
                moldXyCount = 0;
                s_Htzhz = 0;
                s_Htqdz = 0;
                r_Min = 9999;
                s_N2 = 0;
                n_Min = 9999;
                foreach (var mrssjTable_Filter in mrssjTable2)
                {
                    if (mJsff == "" || mJsff == "new")
                    {
                        //计算回弹最后值HTZHZ
                        mHtzhz = calc_htzhz(mrssjTable_Filter);
                        if (MItem[0]["PDBZ"].Contains("23-2011") && sitem["BSFS"] == "1")
                            mHtqdz = calc_htqdzbs(MItem[0], sitem, mrssjTable_Filter);
                        else
                            mHtqdz = calc_htqdz(MItem[0], sitem, mrssjTable_Filter);
                        s_Htqdz = s_Htqdz + mHtqdz;
                    }
                    //赋值数据表的报告编号
                    mrssjTable_Filter["GJH"] = row.ToString();
                    //Sjitem["WTBH"] = MItem[0]["WTBH"];
                    //Sjitem["BGBH"] = MItem[0]["WTBH"];
                    s_Htzhz = s_Htzhz + mHtzhz;
                    if (n_Min > mHtzhz)
                        n_Min = mHtzhz; //求n_Min
                    if (r_Min > mHtqdz)
                        r_Min = mHtqdz;  //求r_Min
                    s_N2 = s_N2 + mHtqdz * mHtqdz;
                    row++;
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

                //-------------综合判断----------
                //平均值,最小值等计算
                r_Pj = Math.Round(s_Htqdz / c_Ht, 1);    //求r_Pj
                n_Pj = Round(s_Htzhz / c_Ht, 1);    //求n_Pj

                if (c_Ht > 1)    //求s_N
                    s_N = Math.Round(Math.Sqrt(Math.Abs(s_N2 - c_Ht * (s_Htqdz / c_Ht) * (s_Htqdz / c_Ht)) / (c_Ht - 1)), 2);
                else
                    s_N = 0;
                if (c_Ht < 10)       //求mQdtdz
                    mQdtdz = r_Min;
                else
                    mQdtdz = Math.Round(r_Pj - 1.645 * s_N, 1);

                //计算碳化修正值
                sitem["PJHTZ"] = n_Pj.ToString();    //平均回弹值
                sitem["ZXHTZ"] = n_Min.ToString();   //最小回弹值
                sitem["PJQDZ"] = r_Pj.ToString();    //平均强度值  *****
                sitem["ZXQDZ"] = r_Min.ToString();   //最小强度值  *****
                sitem["CQS"] = c_Ht.ToString();    //测区数
                if (c_Ht >= 10)
                {
                    //强度均方差  *****
                    xx = s_N.ToString();
                    if (xx.Length > 8)
                        s_N = GetSafeDouble(xx.Substring(0, 8));
                    else
                        s_N = GetSafeDouble(xx);
                    sitem["QDJFC"] = s_N.ToString();
                }
                else
                    sitem["QDJFC"] = "0";
                sitem["QDTDZ"] = mQdtdz.ToString();   //强度推断值  *****
                sitem["DY50GS"] = mDyCount.ToString();    //实际强度>60MPa的测区个数
                sitem["XY10GS"] = mXyCount.ToString();    //实际强度<10的测区个数

                if (sitem["BEIZHU"].StartsWith("B"))
                {
                    sitem["PJQDZ"] = "0";
                    sitem["ZXQDZ"] = "5";
                    sitem["QDTDZ"] = "5";
                    sitem["QDJFC"] = "0";
                    zmXycount = 1;
                }
                if (sitem["BEIZHU"].StartsWith("A"))
                {
                    sitem["PJQDZ"] = "0";
                    sitem["QDJFC"] = "0";
                    sitem["QDTDZ"] = r_Min.ToString();
                    zmDycount = 1;
                    sfcc = true;
                }
                if (mSz != 0)
                {
                    sitem["DDSJQD"] = Math.Round(GetSafeDouble(sitem["QDTDZ"]) / mSz * 100, 1).ToString();     //达到设计强度%
                }
                if (Conversion.Val(sitem["DDSJQD"]) < 100)
                {
                    mAllHg = false;
                }
                if (string.IsNullOrEmpty(sitem["BEIZHU"]))
                    sitem["BEIZHU"] = "----";
                sitem["JCJG"] = jcjg;
                row++;
            }
            if (mAllHg)
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，该次检测混凝土强度全部大于等于设计强度。";
            }
            else
            {
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "，该次检测混凝土强度部分小于设计强度。";
            }

            if (MItem[0]["SFPL"] == "True")
            {
                //批量计算
                //if (MItem[0]["SFPL"] == "True")
                //    MItem[0]["WHICH"] = "bght1_88、bght1_1、bght1_2";
                //else
                //    MItem[0]["WHICH"] = "bght1_88、bght1";
                //if (GetSafeInt(MItem[0]["FJCOUNT"]) > 0)
                //{
                //    for (int i = 1; i <= GetSafeInt(MItem[0]["FJCOUNT"]); i++)
                //    {
                //        bgfjs = (10 + i - 1).ToString();
                //        MItem[0]["WHICH"] = MItem[0]["WHICH"] + "、bght1_" + bgfjs;
                //    }
                //}
                zr_Pj = Round((z_htqdz / z_ht), 1);  //求r_Pj
                zn_Pj = Round((z_htzhz / z_ht), 1);  //求n_Pj
                if (z_ht > 1)    //求s_N
                    z_n = Round(Math.Sqrt((Math.Abs(z_n2 - z_ht * (z_htqdz / z_ht) * (z_htqdz / z_ht)) / (z_ht - 1))), 2);
                else
                    z_n = 0;
                mQdtdz = Round((zr_Pj - 1.645 * z_n), 1);    //求mQdtdz
                MItem[0]["PL_B_DG"] = "";
                if (zr_Pj < 25 && z_n > 4.5 && MItem[0]["SFPL"] == "1")
                {
                    MItem[0]["PL_B_DG"] = "平均值小于25MPa，标准差大于4.5MPa，需按单个构件检测";
                    //MItem[0]["WHICH"] = "bght1_88、bght1";
                    //if (GetSafeInt(MItem[0]["FJCOUNT"]) > 0)
                    //{
                    //    for (int i = 1; i <= GetSafeInt(MItem[0]["FJCOUNT"]); i++)
                    //    {
                    //        bgfjs = (10 + i - 1).ToString();
                    //        MItem[0]["WHICH"] = MItem[0]["WHICH"] + "、bght1_" + bgfjs;
                    //    }
                    //}
                }
                MItem[0]["pjhtz"] = zn_Pj.ToString();  //平均回弹值
                MItem[0]["ZXHTZ"] = zn_Min.ToString();  //最小回弹值
                MItem[0]["PJQDZ"] = zr_Pj.ToString();  //平均强度值 ****
                MItem[0]["ZXQDZ"] = zr_Min.ToString();  //最小强度值 ****
                MItem[0]["CQS"] = z_ht.ToString();  //测区数
                MItem[0]["QDJFC"] = z_n.ToString();  //强度均方差 ****
                MItem[0]["QDTDZ"] = mQdtdz.ToString();   //强度推断值 ****
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
                    MItem[0]["QDTDZ"] = "65";
                    MItem[0]["QDJFC"] = "0";
                }
                if (mSz != 0)
                    MItem[0]["DDSJQD"] = Round(GetSafeDouble(MItem[0]["QDTDZ"]) / mSz * 100, 1).ToString(); //达到设计强度%
                                                                                                            //批量结束
                string tmpstr = "";
                if (!string.IsNullOrEmpty(MItem[0]["BEIZHU"]))
                {
                    if (MItem[0]["BEIZHU"].StartsWith("A"))
                    {
                        MItem[0]["QDJFC"] = "0";
                        tmpstr = "A：表示有测区强度超过60.0MPa";
                        MItem[0]["QDTDZ"] = MItem[0]["ZXQDZ"];
                    }
                    if (MItem[0]["BEIZHU"].StartsWith("B"))
                    {
                        if (string.IsNullOrEmpty(tmpstr))
                            tmpstr = "B：表示有测区强度小于10.0MPa";
                        else
                            tmpstr = tmpstr.Trim() + "，" + "B：表示有测区强度小于10.0MPa";
                    }
                    if (MItem[0]["BEIZHU"].StartsWith("C"))
                    {
                        if (string.IsNullOrEmpty(tmpstr))
                            tmpstr = "C：表示是泵送混凝土，碳化深度大于2.0mm";
                        else
                            tmpstr = tmpstr.Trim() + "，" + "C：表示是泵送混凝土，碳化深度大于2.0mm";
                    }
                    if (MItem[0]["BEIZHU"].StartsWith("D"))
                    {
                        if (string.IsNullOrEmpty(tmpstr))
                            tmpstr = "D：表示有平均回弹值小于20";
                        else
                            tmpstr = tmpstr.Trim() + "，" + "D：表示有平均回弹值小于20";
                    }

                    if (MItem[0]["BEIZHU"].Trim().Length > 0)
                        MItem[0]["BEIZHU"] = "说明代号：" + tmpstr.Trim() + "。";

                    //if (MItem[0]["WHICH"] == "1")
                    //{
                    if (Conversion.Val(MItem[0]["DDSJQD"]) < 100)
                        mAllHg = false;
                    else
                        mAllHg = true;
                    MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，该批构件混凝土强度" + MItem[0]["QDTDZ"] + "MPa，" + "占设计强度" + MItem[0]["DDSJQD"] + "%。";
                    //}
                    //else
                    //{
                    if (Conversion.Val(MItem[0]["DDSJQD"]) > 100)
                    {
                        MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，该次检测混凝土强度全部大于等于设计强度。";
                        mAllHg = true;
                    }
                    else
                    {
                        MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，该次检测混凝土强度部分小于设计强度。";
                        mAllHg = false;
                    }
                    //}
                }
                else
                {
                    if (Conversion.Val(MItem[0]["DDSJQD"]) > 100)
                    {
                        MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，该次检测混凝土强度全部大于等于设计强度。";
                        mAllHg = true;
                    }
                    else
                    {
                        MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，该次检测混凝土强度部分小于设计强度。";
                        mAllHg = false;
                    }
                }
            }

            //主表总判断赋值
            if (mAllHg)
                MItem[0]["JCJG"] = "合格";
            else
                MItem[0]["JCJG"] = "不合格";
            //MItem[0]["MSGINFO"] = "合同号：" + MItem[0]["HTBH"] + "，委托编号：" + MItem[0]["WTDBH"] + "的回弹" + MItem[0]["JGSM"];

            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
