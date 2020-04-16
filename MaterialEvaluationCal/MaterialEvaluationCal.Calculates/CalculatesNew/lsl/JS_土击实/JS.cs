using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class JS : BaseMethods
    {
        public void Calc()
        {
            #region 自定义函数
            Func<double[], double[], int, double, double, double, double, string> qd =
                delegate (double[] X, double[] Y, int n_fun, double Xmin, double Ymin, double Xmax, double Ymax)
                {

                    int Xh;
                    double[] dt = new double[4];
                    int m = 5;
                    double[] B_main = new double[m + 1];
                    double[] xiaoA = new double[m + 1];
                    double xpj = 0;

                    #region  Iapcir部分
                    int I;
                    int J;
                    int K;
                    double Z;
                    double P;
                    double C;
                    double G;
                    double Q = 0;
                    double D1;
                    double D2;
                    double[] S = new double[20];
                    double[] T = new double[20];
                    double[] B = new double[20];
                    for (I = 1; I <= m; I++)
                        xiaoA[I] = 0;
                    m = 5;

                    Z = 0;

                    for (I = 1; I <= n_fun; I++)
                    {
                        xpj = xpj + X[I];
                        Z = Z + X[I] / (1 * n_fun);
                    }
                    xpj = xpj / n_fun;
                    B[0] = 1;
                    D1 = 1 * n_fun;
                    P = 0;
                    C = 0;
                    for (I = 1; I <= n_fun; I++)
                    {
                        P = P + (X[I] - Z);
                        C = C + Y[I];
                    }

                    C = C / D1;
                    P = P / D1;
                    xiaoA[1] = C * B[0];

                    if (m > 1)
                    {
                        T[1] = 1;
                        T[0] = (-1) * P;
                        D2 = 0;
                        C = 0;
                        G = 0;
                        for (I = 1; I <= n_fun; I++)
                        {
                            Q = X[I] - Z - P;
                            D2 = D2 + Q * Q;
                            C = C + Y[I] * Q;
                            G = G + (X[I] - Z) * Q * Q;
                        }

                        C = C / D2;
                        P = G / D2;
                        Q = D2 / D1;
                        D1 = D2;
                        xiaoA[2] = C * T[1];
                        xiaoA[1] = C * T[0] + xiaoA[1];
                    }
                    for (J = 2; J <= m - 1; J++)
                    {
                        S[J] = T[J - 1];
                        S[J - 1] = (-1) * P * T[J - 1] + T[J - 2];
                        if (J >= 3)
                        {
                            for (K = J - 2; K >= 1; K--)
                                S[K] = (-1) * P * T[K] + T[K - 1] - Q * B[K];
                        }
                        S[0] = (-1) * P * T[0] - Q * B[0];
                        D2 = 0;
                        C = 0;
                        G = 0;

                        for (I = 1; I <= n_fun; I++)
                        {
                            Q = S[J];
                            for (K = J - 1; K >= 0; K--)
                                Q = Q * (X[I] - Z) + S[K];
                            D2 = D2 + Q * Q;
                            C = C + Y[I] * Q;
                            G = G + (X[I] - Z) * Q * Q;
                        }

                        C = C / D2;
                        P = G / D2;
                        Q = D2 / D1;
                        D1 = D2;
                        xiaoA[J + 1] = C * S[J];
                        T[J] = S[J];
                        for (K = J - 1; K >= 0; K--)
                        {
                            xiaoA[K + 1] = C * S[K] + xiaoA[K + 1];
                            B[K] = T[K];
                            T[K] = S[K];
                        }
                    }

                    dt[0] = 0;
                    dt[1] = 0;
                    dt[2] = 0;
                    for (I = 1; I <= n_fun; I++)
                    {
                        Q = xiaoA[m];
                        for (K = m - 2; K >= 0; K--)
                            Q = xiaoA[K + 1] + Q * (X[I] - Z);
                        P = Q - Y[I];

                        if (Math.Abs(P) > dt[2])
                            dt[2] = Math.Abs(P);
                        dt[0] = dt[0] + P * P;
                        dt[1] = dt[1] + Math.Abs(P);
                    }
                    #endregion

                    string Str = "y=";
                    Str = Str + xiaoA[1] + "x^" + (1 - 1);
                    for (I = 2; I <= m; I++)    //写方程
                    {
                        if (xiaoA[I] > 0)
                            Str = Str + "+";
                        Str = Str + xiaoA[I] + "x^" + (I - 1);
                    }
                    #region HuaQuXian部分
                    double Ysum, As, Ii;
                    double qdx, qdy;
                    double qdx1, qdy1;
                    double qfdx, qfdy;
                    double wya, mwya;
                    double qsbz;
                    int mi;
                    double qcxmax_fun = Xmin;
                    double qcymax_fun = Ymin;
                    double qcxmin = Xmax;
                    double qcymin = Ymax;
                    Ii = Xmin;
                    for (mi = 1; mi <= 100; mi++)
                    {
                        Ysum = 0;
                        for (J = 1; J <= m; J++)
                            Ysum = Ysum + xiaoA[J] * Math.Pow((Ii - xpj), (J - 1));

                        if (qcymax_fun <= Ysum)
                        {
                            qcymax_fun = Ysum;
                            qcxmax_fun = Ii;
                        }
                        Ii = Ii + (Xmax - Xmin) / 100;

                    }
                    qcxmax_fun = Round(qcxmax_fun, 1);
                    qcymax_fun = Round(qcymax_fun, 2);
                    #endregion;
                    return qcxmax_fun.ToString() + "@" + qcymax_fun;
                };
            #endregion

            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_JS"];
            var MItem = data["M_JS"];
            int mbHggs = 0, n = 0, k = 5;
            if (!data.ContainsKey("M_JS"))
            {
                data["M_JS"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            string mJSFF;
            int zZh = 1, zGs = 0;
            bool mGetBgbh = false;
            string BHGXM = "", Hgxm = "", mlongStr = "";
            double qcymax = 0, qcxmax = 0;
            double[] x = new double[7];
            double[] y = new double[7];
            foreach (var sItem in SItem)
            {
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //if (!string.IsNullOrEmpty(sItem["SYRQ"]) && !string.IsNullOrEmpty(sItem["ZZRQ"]))
                //{
                //    sItem["LQ"] = (DateTime.Parse(sItem["SYRQ"]) - DateTime.Parse(sItem["ZZRQ"])).Days.ToString();
                //}
                string mYpmcJcxm = sItem["YPMC"].Trim() + "样品" + sItem["JCXM"].Trim();
                mItem["PDBZ"] = "《土工试验方法标准》GB/T 50123-2019";
                double sStzl, sSmd, sSzl1, sSzl2, sGtzl1, sGtzl2, sHsl1, sHsl2;
                for (int i = 1; i <= 6; i++)
                {
                    //模桶加湿土质量
                    if (double.Parse(sItem["MTJST" + i]) != 0)
                    {
                        //湿土质量
                        sStzl = double.Parse(sItem["MTJST" + i]) - double.Parse(mItem["MTZL"]);
                        sItem["STZL1_" + i] = sStzl.ToString();
                        if (double.Parse(mItem["MTTJ"]) == 0)
                        {
                            sSmd = 0;
                            sItem["SMD1_" + i] = "0";
                        }
                        else
                        {
                            //计算湿密度
                            sSmd = Round(sStzl / double.Parse(mItem["MTTJ"]), 2);
                            sItem["SMD1_" + i] = sSmd.ToString("0.00");
                        }
                        //水质量 = 盒加湿土质量 - 盒加干土质量
                        sSzl1 = double.Parse(sItem["HJST" + i + "1"]) - double.Parse(sItem["HJGT" + i + "1"]);
                        sSzl2 = double.Parse(sItem["HJST" + i + "2"]) - double.Parse(sItem["HJGT" + i + "2"]);
                        //水分质量
                        sItem["SZL1_" + i] = Round((sSzl1 + sSzl2) / 2, 2).ToString("0.00");
                        //干土质量
                        sGtzl1 = double.Parse(sItem["HJGT" + i + "1"]) - double.Parse(sItem["HZL" + i + "1"]);
                        sGtzl2 = double.Parse(sItem["HJGT" + i + "2"]) - double.Parse(sItem["HZL" + i + "2"]);
                        //干土质量
                        sItem["GTZL1_" + i] = Round((sGtzl1+ sGtzl2)/2,2).ToString("0.00");
                        if (sGtzl1 == 0)
                        {
                            sHsl1 = 0;
                        }
                        else
                        {
                            //含水率1
                            sHsl1 = Round(sSzl1 / sGtzl1 * 100, 1);
                        }
                        if (sGtzl2 == 0)
                        {
                            sHsl2 = 0;
                        }
                        else
                        {
                            //含水率2 
                            sHsl2 = Round(sSzl2 / sGtzl2 * 100, 1);
                        }
                        //计算平均含水率时应该先判断两个含水率的差值应不大于1%
                        if (sHsl1 == 0 || sHsl2 == 0)
                            sItem["PJHSL" + i] = Round((sHsl1 + sHsl2), 1).ToString();
                        else
                            sItem["PJHSL" + i] = Round((sHsl1 + sHsl2) / 2, 1).ToString();
                        sItem["GMD" + i] = Round(sSmd / (1 + 0.01 * double.Parse(sItem["PJHSL" + i])), 2).ToString("0.00");
                        x[i] = double.Parse(sItem["PJHSL" + i]);
                        y[i] = double.Parse(sItem["GMD" + i]);
                        n = n + 1;
                    }
                    else
                    {
                        sItem["PJHSL" + i] = "0";
                        sItem["GMD" + i] = "0";
                    }
                }
                double Xmin = 1000,
                Ymin = 1000,
                Xmax = 0,
                Ymax = 0;
                if (n >= 5)
                {

                    for (int i = 1; i <= n; i++)
                    {
                        if (x[i] < Xmin && x[i] != 0)
                            Xmin = x[i];
                        if (y[i] < Ymin && y[i] != 0)
                            Ymin = y[i];
                        if (x[i] > Xmax && x[i] != 0)
                            Xmax = x[i];
                        if (y[i] > Ymax && y[i] != 0)
                            Ymax = x[i];
                    }
                    string qdstr = qd(x, y, n, Xmin, Ymin, Xmax, Ymax);
                    qcxmax = double.Parse(qdstr.Split('@')[0]);
                    qcymax = double.Parse(qdstr.Split('@')[1]);

                    mItem["ZDGMD1"] = Round(qcymax, 2).ToString();
                    mItem["ZJHSL1"] = Round(qcxmax, 1).ToString();
                }
                else
                {
                    mItem["ZDGMD1"] = Round(0, 2).ToString();
                    mItem["ZJHSL1"] = Round(0, 1).ToString();
                }

                n = 0;
                for (int i = 1; i <= 6; i++)
                {
                    if (0 != double.Parse(sItem["S_MTJST" + i]))
                    {
                        sStzl = double.Parse(sItem["S_MTJST" + i]) - double.Parse(mItem["MTZL"]);
                        //湿土质量
                        sItem["STZL2_" + i] = sStzl.ToString();
                        if (double.Parse(mItem["MTTJ"]) == 0)
                        {
                            sSmd = 0;
                            sItem["SMD2_" + i] = "0";
                        }
                        else
                        {
                            //计算湿密度
                            sSmd = Round(sStzl / double.Parse(mItem["MTTJ"]), 2);
                            sItem["SMD2_" + i] = sSmd.ToString("0.00");
                        }
                        sSzl1 = double.Parse(sItem["S_HJST" + i + "1"]) - double.Parse(sItem["S_HJGT" + i + "1"]);
                        sSzl2 = double.Parse(sItem["S_HJST" + i + "2"]) - double.Parse(sItem["S_HJGT" + i + "2"]);
                        //水分质量
                        sItem["SZL2_" + i] = Round((sSzl1 + sSzl2) / 2, 2).ToString("0.00");
                        sGtzl1 = double.Parse(sItem["S_HJGT" + i + "1"]) - double.Parse(sItem["S_HZL" + i + "1"]);
                        sGtzl2 = double.Parse(sItem["S_HJGT" + i + "2"]) - double.Parse(sItem["S_HZL" + i + "2"]);
                        //干土质量
                        sItem["GTZL2_" + i] = Round((sGtzl1 + sGtzl2) / 2, 2).ToString("0.00");
                        if (sGtzl1 == 0) sHsl1 = 0;
                        else sHsl1 = Round(sSzl1 / sGtzl1 * 100, 1);
                        if (sGtzl2 == 0) sHsl2 = 0;
                        else sHsl2 = Round(sSzl2 / sGtzl2 * 100, 1);
                        if (sHsl1 == 0 || sHsl2 == 0)
                        {
                            sItem["S_PJHSL" + i] = Round((sHsl1 + sHsl2), 1).ToString();
                        }
                        else
                        {
                            sItem["S_PJHSL" + i] = Round((sHsl1 + sHsl2) / 2, 1).ToString();
                        }
                        sItem["S_GMD" + i] = Round(sSmd / (1 + 0.01 * double.Parse(sItem["S_PJHSL" + i])), 2).ToString();
                        x[i] = double.Parse(sItem["S_PJHSL" + i]);
                        y[i] = double.Parse(sItem["S_GMD" + i]);
                        n = n + 1;
                    }
                    else
                    {
                        sItem["S_PJHSL" + i] = "0";
                        sItem["S_GMD" + i] = "0";
                    }
                }

                if (n >= 5)
                {
                    Xmin = 1000;
                    Ymin = 1000;
                    Xmax = 0;
                    Ymax = 0;
                    for (int i = 0; i <= n - 1; i++)
                    {
                        if (x[i] < Xmin && x[i] != 0)
                            Xmin = x[i];
                        if (y[i] < Ymin && y[i] != 0)
                            Ymin = y[i];
                        if (x[i] > Xmax && x[i] != 0)
                            Xmax = x[i];
                        if (y[i] > Ymax && y[i] != 0)
                            Ymax = x[i];
                    }
                    string qdstr = qd(x, y, n, Xmin, Ymin, Xmax, Ymax);
                    qcxmax = double.Parse(qdstr.Split('@')[0]);
                    qcymax = double.Parse(qdstr.Split('@')[1]);
                    mItem["ZDGMD2"] = Round(qcymax, 2).ToString();
                    mItem["ZJHSL2"] = Round(qcxmax, 1).ToString();
                }
                else
                {
                    mItem["ZDGMD2"] = Round(0, 2).ToString();
                    mItem["ZJHSL2"] = Round(0, 1).ToString();
                }
                if (double.Parse(mItem["ZDGMD1"]) * double.Parse(mItem["ZDGMD2"]) == 0)
                {
                    mItem["ZDGMD"] = Round(double.Parse(mItem["ZDGMD1"]) + double.Parse(mItem["ZDGMD2"]), 2).ToString("0.00");
                }
                else
                {
                    mItem["ZDGMD"] = Round((double.Parse(mItem["ZDGMD1"]) + double.Parse(mItem["ZDGMD2"])) / 2, 2).ToString("0.00");
                }
                if (double.Parse(mItem["ZJHSL1"]) * double.Parse(mItem["ZJHSL2"]) == 0)
                {
                    mItem["ZJHSL"] = Round(double.Parse(mItem["ZJHSL1"]) + double.Parse(mItem["ZJHSL2"]), 1).ToString("0.0");
                }
                else
                {
                    mItem["ZJHSL"] = Round((double.Parse(mItem["ZJHSL1"]) + double.Parse(mItem["ZJHSL2"])) / 2, 1).ToString("0.0");
                }
                bool flag, sign;
                mItem["S_BZ"] = "1";
                if (mItem["JCYJ"].Contains("51"))
                {
                    mItem["S_BZ"] = "0";
                }
                double[] dArray = new double[4];
                double[] sArray = new double[3];
                if (mItem["S_BZ"] == "1")
                {
                    sign = true;
                    sign = IsNumeric(mItem["S_BFL"].Trim()) ? sign : false;
                    sign = IsNumeric(mItem["S_MTJ"].Trim()) ? sign : false;
                    sign = IsNumeric(mItem["S_XSL"].Trim()) ? sign : false;
                    if (sign)
                    {
                        dArray[1] = double.Parse(mItem["S_BFL"]);
                        dArray[2] = double.Parse(mItem["S_MTJ"]);
                        dArray[3] = double.Parse(mItem["S_XSL"]);
                        qcymax = ((1 - 0.01 * dArray[1]) / qcymax) + 0.01 * dArray[1] / dArray[2];
                        qcymax = 1 / qcymax;
                        qcymax = Round(qcymax, 2);
                        sArray[1] = qcymax;
                        qcxmax = qcxmax * (1 - 0.01 * dArray[1]) + 0.01 * dArray[1] * dArray[3];
                        qcxmax = Round(qcxmax, 2);
                        sArray[2] = qcxmax;
                        jsbeizhu = "已修约 ";
                    }
                    else
                    {
                        sArray[1] = Round(qcymax, 2);
                        sArray[2] = Round(qcxmax, 1);
                        jsbeizhu = "";
                    }
                }
                else
                {
                    mItem["JCYJ"] = "JTG E51-2009 《公路工程无机结合料稳定材料试验规程》";
                    sign = true;
                    sign = IsNumeric(mItem["S_BFL"]) ? sign : false;
                    sign = IsNumeric(mItem["S_MTJ"]) ? sign : false;
                    sign = IsNumeric(mItem["S_XSL"]) ? sign : false;
                    if (sign)
                    {
                        dArray[1] = double.Parse(mItem["S_BFL"].Trim());
                        dArray[2] = double.Parse(mItem["S_MTJ"].Trim());
                        dArray[3] = double.Parse(mItem["S_XSL"].Trim());
                        qcymax = qcymax * (1 - 0.01 * dArray[1]) + 0.9 * 0.01 * dArray[1] * dArray[2];
                        qcymax = Round(qcymax, 2);
                        qcxmax = qcxmax * (1 - 0.1 * dArray[1]) + 0.01 * dArray[1] * dArray[3];
                        qcxmax = Round(qcxmax, 1);
                        jsbeizhu = "已修约";
                    }
                    else
                    {
                        qcymax = Round(qcymax, 2);
                        qcxmax = Round(qcxmax, 1);
                        jsbeizhu = "";
                    }
                    sArray[1] = qcymax;
                    sArray[2] = qcxmax;
                }
                if (jsbeizhu.Length > 1)
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，该" + mYpmcJcxm + "试验的校正后最佳含水率为" + sArray[2] + "%，校正后最大干密度为" + sArray[1] + "克/立方厘米。";
                }
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，该" + mYpmcJcxm + "试验的最佳含水率为" + sArray[2] + "%，最大干密度为" + sArray[1] + "克/立方厘米。";
                }
            }
            //mItem["MSGINFO"] = "合同号：" + mItem["HTBH"] + "，委托编号：" + mItem["WTDBH"] + "的土击实最大干密度：" + mItem["ZDGMD"] + "，最佳含水率：" + mItem["ZJHSL"];

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
