using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class JBC : BaseMethods
    {
        public void Calc()
        {
            #region 
            bool mAllHg = true, mSFwc = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_JBC"];
            var MItem = data["M_JBC"];
            var mrsDj = dataExtra["BZ_JBC_DJ"];
            var mrsDrxs = dataExtra["ZM_DRJL"];
            if (!data.ContainsKey("M_JBC"))
            {
                data["M_JBC"] = new List<IDictionary<string, string>>();
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
            int mbhggs = 0;
            bool mFlag_Hg = false, mFlag_Bhg = false;
            foreach (var sItem in SItem)
            {
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                double md1, md2, md3, md, sum, pjmd;
                if (sItem["GGLB"] != "Ⅰ" && sItem["GGLB"] != "Ⅱ")
                {
                    sItem["GGLB"] = "Ⅱ";
                }
                var mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == sItem["GGLB"].Trim());
                if (sItem["BZXZ"] == "Q/THBW 001-2013")
                {
                    mrsdj = mrsDj.FirstOrDefault(u => u["JCYJ"] == sItem["BZXZ"].Trim() && u["MC"] == sItem["GGLB"].Trim());
                }

                if (mrsdj != null)
                {
                    mItem["G_DRXS"] = mrsdj["DRXS"];
                    mItem["G_KYQD"] = mrsdj["KYQD"];
                    mItem["G_YSQD"] = mrsdj["YSQD"];
                    mItem["G_BGMD"] = mrsdj["BGMD"];
                    mItem["G_HSL"] = mrsdj["HSL"];
                    mItem["G_SBGMD"] = mrsdj["SBGMD"];
                    mItem["G_XXSSL"] = mrsdj["XXSSL"];
                    mItem["G_RHXS"] = mrsdj["RHXS"];
                    mItem["G_NRX"] = mrsdj["NRX"];
                    mItem["G_FCD"] = mrsdj["FCD"];
                    mItem["G_ZLSSL"] = mrsdj["ZLSSL"];
                    mItem["G_QDSSL"] = mrsdj["QDSSL"];
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "依据不详";
                    mItem["BGBH"] = "";
                }

                //跳转
                if (!string.IsNullOrEmpty(mItem["SJTABS"]))
                {
                    int mcd, mdwz;
                    bool sjtabcalc = true;

                    if (sItem["BZXZ"].Contains("JC/T 2200-2013") || sItem["CPMC"].Contains("水泥基泡沫保温板"))
                    {
                        #region 表观密度
                        if (jcxm.Contains("、表观密度、"))
                        {
                            if (GetSafeDouble(sItem["GSJC1_1"]) != 0)
                            {
                                sItem["GSJC1"] = (Round((GetSafeDouble(sItem["GSJC1_1"]) + GetSafeDouble(sItem["GSJC1_2"]) + GetSafeDouble(sItem["GSJC1_3"]) + GetSafeDouble(sItem["GSJC1_4"])) / 4, 1)).ToString();
                                sItem["GSJC2"] = (Round((GetSafeDouble(sItem["GSJC2_1"]) + GetSafeDouble(sItem["GSJC2_2"]) + GetSafeDouble(sItem["GSJC2_3"]) + GetSafeDouble(sItem["GSJC2_4"])) / 4, 1)).ToString();
                                sItem["GSJC3"] = (Round((GetSafeDouble(sItem["GSJC3_1"]) + GetSafeDouble(sItem["GSJC3_2"]) + GetSafeDouble(sItem["GSJC3_3"]) + GetSafeDouble(sItem["GSJC3_4"])) / 4, 1)).ToString();
                                sItem["GSJC4"] = (Round((GetSafeDouble(sItem["GSJC4_1"]) + GetSafeDouble(sItem["GSJC4_2"]) + GetSafeDouble(sItem["GSJC4_3"]) + GetSafeDouble(sItem["GSJC4_4"])) / 4, 1)).ToString();
                                sItem["GSJC5"] = (Round((GetSafeDouble(sItem["GSJC5_1"]) + GetSafeDouble(sItem["GSJC5_2"]) + GetSafeDouble(sItem["GSJC5_3"]) + GetSafeDouble(sItem["GSJC5_4"])) / 4, 1)).ToString();
                                sItem["GSJC6"] = (Round((GetSafeDouble(sItem["GSJC6_1"]) + GetSafeDouble(sItem["GSJC6_2"]) + GetSafeDouble(sItem["GSJC6_3"]) + GetSafeDouble(sItem["GSJC6_4"])) / 4, 1)).ToString();
                            }
                            if (GetSafeDouble(sItem["GSJK1_1"]) != 0)
                            {
                                sItem["GSJK1"] = (Round((GetSafeDouble(sItem["GSJK1_1"]) + GetSafeDouble(sItem["GSJK1_2"]) + GetSafeDouble(sItem["GSJK1_3"]) + GetSafeDouble(sItem["GSJK1_4"])) / 4, 1)).ToString();
                                sItem["GSJK2"] = (Round((GetSafeDouble(sItem["GSJK2_1"]) + GetSafeDouble(sItem["GSJK2_2"]) + GetSafeDouble(sItem["GSJK2_3"]) + GetSafeDouble(sItem["GSJK2_4"])) / 4, 1)).ToString();
                                sItem["GSJK3"] = (Round((GetSafeDouble(sItem["GSJK3_1"]) + GetSafeDouble(sItem["GSJK3_2"]) + GetSafeDouble(sItem["GSJK3_3"]) + GetSafeDouble(sItem["GSJK3_4"])) / 4, 1)).ToString();
                                sItem["GSJK4"] = (Round((GetSafeDouble(sItem["GSJK4_1"]) + GetSafeDouble(sItem["GSJK4_2"]) + GetSafeDouble(sItem["GSJK4_3"]) + GetSafeDouble(sItem["GSJK4_4"])) / 4, 1)).ToString();
                                sItem["GSJK5"] = (Round((GetSafeDouble(sItem["GSJK5_1"]) + GetSafeDouble(sItem["GSJK5_2"]) + GetSafeDouble(sItem["GSJK5_3"]) + GetSafeDouble(sItem["GSJK5_4"])) / 4, 1)).ToString();
                                sItem["GSJK6"] = (Round((GetSafeDouble(sItem["GSJK6_1"]) + GetSafeDouble(sItem["GSJK6_2"]) + GetSafeDouble(sItem["GSJK6_3"]) + GetSafeDouble(sItem["GSJK6_4"])) / 4, 1)).ToString();
                            }
                            if (GetSafeDouble(sItem["GSJG1_1"]) != 0)
                            {
                                sItem["GSJG1"] = (Round((GetSafeDouble(sItem["GSJG1_1"]) + GetSafeDouble(sItem["GSJG1_2"]) + GetSafeDouble(sItem["GSJG1_3"]) + GetSafeDouble(sItem["GSJG1_4"]) + GetSafeDouble(sItem["GSJG1_5"]) + GetSafeDouble(sItem["GSJG1_6"])) / 6, 1)).ToString();
                                sItem["GSJG2"] = (Round((GetSafeDouble(sItem["GSJG2_1"]) + GetSafeDouble(sItem["GSJG2_2"]) + GetSafeDouble(sItem["GSJG2_3"]) + GetSafeDouble(sItem["GSJG2_4"]) + GetSafeDouble(sItem["GSJG2_5"]) + GetSafeDouble(sItem["GSJG2_6"])) / 6, 1)).ToString();
                                sItem["GSJG3"] = (Round((GetSafeDouble(sItem["GSJG3_1"]) + GetSafeDouble(sItem["GSJG3_2"]) + GetSafeDouble(sItem["GSJG3_3"]) + GetSafeDouble(sItem["GSJG3_4"]) + GetSafeDouble(sItem["GSJG3_5"]) + GetSafeDouble(sItem["GSJG3_6"])) / 6, 1)).ToString();
                                sItem["GSJG4"] = (Round((GetSafeDouble(sItem["GSJG4_1"]) + GetSafeDouble(sItem["GSJG4_2"]) + GetSafeDouble(sItem["GSJG4_3"]) + GetSafeDouble(sItem["GSJG4_4"]) + GetSafeDouble(sItem["GSJG4_5"]) + GetSafeDouble(sItem["GSJG4_6"])) / 6, 1)).ToString();
                                sItem["GSJG5"] = (Round((GetSafeDouble(sItem["GSJG5_1"]) + GetSafeDouble(sItem["GSJG5_2"]) + GetSafeDouble(sItem["GSJG5_3"]) + GetSafeDouble(sItem["GSJG5_4"]) + GetSafeDouble(sItem["GSJG5_5"]) + GetSafeDouble(sItem["GSJG5_6"])) / 6, 1)).ToString();
                                sItem["GSJG6"] = (Round((GetSafeDouble(sItem["GSJG6_1"]) + GetSafeDouble(sItem["GSJG6_2"]) + GetSafeDouble(sItem["GSJG6_3"]) + GetSafeDouble(sItem["GSJG6_4"]) + GetSafeDouble(sItem["GSJG6_5"]) + GetSafeDouble(sItem["GSJG6_6"])) / 6, 1)).ToString();
                            }

                            sItem["GSJTJ1"] = (Round(GetSafeDouble(sItem["GSJC1"]) * (GetSafeDouble(sItem["GSJK1"])) * (GetSafeDouble(sItem["GSJG1"])), 0)).ToString();
                            sItem["GSJTJ2"] = (Round(GetSafeDouble(sItem["GSJC2"]) * (GetSafeDouble(sItem["GSJK2"])) * (GetSafeDouble(sItem["GSJG2"])), 0)).ToString();
                            sItem["GSJTJ3"] = (Round(GetSafeDouble(sItem["GSJC3"]) * (GetSafeDouble(sItem["GSJK3"])) * (GetSafeDouble(sItem["GSJG3"])), 0)).ToString();
                            sItem["GSJTJ4"] = (Round(GetSafeDouble(sItem["GSJC4"]) * (GetSafeDouble(sItem["GSJK4"])) * (GetSafeDouble(sItem["GSJG4"])), 0)).ToString();
                            sItem["GSJTJ5"] = (Round(GetSafeDouble(sItem["GSJC5"]) * (GetSafeDouble(sItem["GSJK5"])) * (GetSafeDouble(sItem["GSJG5"])), 0)).ToString();
                            sItem["GSJTJ6"] = (Round(GetSafeDouble(sItem["GSJC6"]) * (GetSafeDouble(sItem["GSJK6"])) * (GetSafeDouble(sItem["GSJG6"])), 0)).ToString();


                            sItem["BGMD1"] = (GetSafeDouble(sItem["GSJZL1"]) / 1000 / GetSafeDouble(sItem["GSJC1"]) / 1000 / GetSafeDouble(sItem["GSJK1"]) / 1000 / GetSafeDouble(sItem["GSJG1"]) / 1000).ToString("G4");
                            sItem["BGMD2"] = (GetSafeDouble(sItem["GSJZL2"]) / 1000 / GetSafeDouble(sItem["GSJC2"]) / 1000 / GetSafeDouble(sItem["GSJK2"]) / 1000 / GetSafeDouble(sItem["GSJG2"]) / 1000).ToString("G4");
                            sItem["BGMD3"] = (GetSafeDouble(sItem["GSJZL3"]) / 1000 / GetSafeDouble(sItem["GSJC3"]) / 1000 / GetSafeDouble(sItem["GSJK3"]) / 1000 / GetSafeDouble(sItem["GSJG3"]) / 1000).ToString("G4");
                            sItem["BGMD4"] = (GetSafeDouble(sItem["GSJZL4"]) / 1000 / GetSafeDouble(sItem["GSJC4"]) / 1000 / GetSafeDouble(sItem["GSJK4"]) / 1000 / GetSafeDouble(sItem["GSJG4"]) / 1000).ToString("G4");
                            sItem["BGMD5"] = (GetSafeDouble(sItem["GSJZL5"]) / 1000 / GetSafeDouble(sItem["GSJC5"]) / 1000 / GetSafeDouble(sItem["GSJK5"]) / 1000 / GetSafeDouble(sItem["GSJG5"]) / 1000).ToString("G4");
                            sItem["BGMD6"] = (GetSafeDouble(sItem["GSJZL6"]) / 1000 / GetSafeDouble(sItem["GSJC6"]) / 1000 / GetSafeDouble(sItem["GSJK6"]) / 1000 / GetSafeDouble(sItem["GSJG6"]) / 1000).ToString("G4");


                            sItem["BGMD"] = (GetSafeDouble(sItem["BGMD1"]) + GetSafeDouble(sItem["BGMD2"]) + GetSafeDouble(sItem["BGMD3"]) + GetSafeDouble(sItem["BGMD4"]) + GetSafeDouble(sItem["BGMD5"]) + GetSafeDouble(sItem["BGMD6"]) / 6).ToString("G3");

                            string drxs = IsQualified(mItem["G_BGMD"], sItem["BGMD"], true);
                            if (drxs == "符合")
                            {
                                mItem["HG_BGMD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mItem["HG_BGMD"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["BGMD"] = "----";
                            mItem["HG_BGMD"] = "----";
                            mItem["G_BGMD"] = "----";
                        }
                        #endregion

                        #region 抗压强度
                        if (jcxm.Contains("、抗压强度、"))
                        {
                            double mMj1, mMj2, mMj3, mMj4;
                            double mKyqd1 = 0, mKyqd2 = 0, mKyqd3 = 0, mKyqd4 = 0;
                            if (GetSafeDouble(sItem["KYHZ1"]) > 0)
                            {
                                mMj1 = GetSafeDouble(sItem["KYCD1"]) * GetSafeDouble(sItem["KYKD1"]);
                                mMj2 = GetSafeDouble(sItem["KYCD2"]) * GetSafeDouble(sItem["KYKD2"]);
                                mMj3 = GetSafeDouble(sItem["KYCD3"]) * GetSafeDouble(sItem["KYKD3"]);
                                mMj4 = GetSafeDouble(sItem["KYCD4"]) * GetSafeDouble(sItem["KYKD4"]);

                                sItem["KYMJ1"] = mMj1.ToString();
                                sItem["KYMJ2"] = mMj2.ToString();
                                sItem["KYMJ3"] = mMj3.ToString();
                                sItem["KYMJ4"] = mMj4.ToString();

                                if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0)
                                {
                                    mKyqd1 = Round(GetSafeDouble(sItem["KYHZ1"]) / mMj1, 3);
                                    mKyqd2 = Round(GetSafeDouble(sItem["KYHZ2"]) / mMj2, 3);
                                    mKyqd3 = Round(GetSafeDouble(sItem["KYHZ3"]) / mMj3, 3);
                                    mKyqd4 = Round(GetSafeDouble(sItem["KYHZ4"]) / mMj4, 3);
                                }
                                else
                                {
                                    mKyqd1 = 0;
                                    mKyqd2 = 0;
                                    mKyqd3 = 0;
                                    mKyqd4 = 0;
                                }
                                sItem["KYQD1"] = mKyqd1.ToString();
                                sItem["KYQD2"] = mKyqd2.ToString();
                                sItem["KYQD3"] = mKyqd3.ToString();
                                sItem["KYQD4"] = mKyqd4.ToString();

                                string mlongStr = (mKyqd1) + "," + (mKyqd2) + "," + (mKyqd3) + "," + (mKyqd4);
                                string[] mtmpArray = mlongStr.Split(',');
                                List<double> mtmpList = new List<double>();
                                foreach (string str in mtmpArray)
                                {
                                    mtmpList.Add(GetSafeDouble(str));
                                }
                                mtmpList.Sort();
                                double mAvgKyqd = Round(mtmpList.Average(), 2);

                                sItem["KYQD"] = Round(mAvgKyqd, 2).ToString();//'抗压平均字符类型为字符型

                                string drxs = IsQualified(mItem["G_KYQD"], sItem["KYQD"], true);
                                if (drxs == "符合")
                                {
                                    mItem["HG_KYQD"] = "合格";
                                    mFlag_Hg = true;
                                }
                                else
                                {
                                    mItem["HG_KYQD"] = "不合格";
                                    mbhggs = mbhggs + 1;
                                    mFlag_Bhg = true;
                                    mAllHg = false;
                                }
                            }
                            else
                            {
                                mSFwc = false;
                                sItem["KYQD"] = "----";
                                mItem["HG_KYQD"] = "----";
                                mItem["G_KYQD"] = "----";
                            }
                        }
                        #endregion

                        #region 导热系数
                        if (jcxm.Contains("、导热系数、"))
                        {
                            string drxs = IsQualified(mItem["G_DRXS"], sItem["DRXS"], true);
                            if (drxs == "符合")
                            {
                                mItem["HG_DRXS"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mItem["HG_DRXS"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["DRXS"] = "----";
                            mItem["HG_DRXS"] = "----";
                            mItem["G_DRXS"] = "----";
                        }
                        #endregion
                    }
                    else
                    {
                        if (jcxm.Contains("、导热系数、"))
                        {
                            mcd = mItem["G_DRXS"].Length;
                            mdwz = mItem["G_DRXS"].IndexOf(".");
                            mcd = mcd - mdwz + 1;

                            if (mItem["DEVCODE"].Contains("XCS17 - 067") || mItem["DEVCODE"].Contains("XCS17-066"))
                            {
                                //var ZM_DRJL = mrsDrxs.FirstOrDefault();
                                var ZM_DRJL = mrsDrxs.FirstOrDefault(x => x["SYSJBRECID"].Equals(x["RECID"]));
                                //var ZM_DRJL = mrsDrxs.FirstOrDefault(u => u["SYLB"] == "jbc" && u["SYBH"] == mItem["JYDBH"]);
                                sItem["DRXS"] = ZM_DRJL["DRXS"];
                                mItem["JCYJ"] = mItem["JCYJ"].Replace("10294", "10295");
                            }
                            sItem["DRXS"] = Round(double.Parse(sItem["DRXS"]), mcd).ToString();
                            mItem["HG_DRXS"] = IsQualified(mItem["G_DRXS"], sItem["DRXS"], false);
                            mbhggs = (mItem["HG_DRXS"] == "不合格" ? mbhggs + 1 : mbhggs);
                            if (mItem["HG_DRXS"] != "不合格")
                            { 
                                mFlag_Hg = true;
                            }
                            else { 
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["DRXS"] = "----";
                            mItem["HG_DRXS"] = "----";
                            mItem["G_DRXS"] = "----";
                        }

                        if (jcxm.Contains("、抗压强度、"))
                        {
                            if (double.Parse(sItem["KYQD"]) == 0)
                            {
                                sjtabcalc = false;
                            }
                            mItem["HG_KYQD"] = IsQualified(mItem["G_KYQD"], sItem["KYQD"], false);
                            mbhggs = (mItem["HG_KYQD"] == "不合格" ? mbhggs + 1 : mbhggs);
                            if (mItem["HG_KYQD"] != "不合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["KYQD"] = "----";
                            mItem["HG_KYQD"] = "----";
                            mItem["G_KYQD"] = "----";
                        }

                        if (jcxm.Contains("、压剪粘结强度、"))
                        {
                            if (Conversion.Val(sItem["YSQD"]) == 0)
                                sjtabcalc = false;
                            mItem["HG_YSQD"] = IsQualified(mItem["G_YSQD"], sItem["YSQD"], false);
                            mbhggs = mItem["HG_YSQD"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (mItem["HG_YSQD"] != "不合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["YSQD"] = "----";
                            mItem["HG_YSQD"] = "----";
                            mItem["G_YSQD"] = "----";
                        }
                        if (jcxm.Contains("、干密度、"))
                        {
                            mItem["HG_BGMD"] = IsQualified(mItem["G_BGMD"], sItem["BGMD"], false);
                            mbhggs = mItem["HG_BGMD"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (mItem["HG_BGMD"] != "不合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["BGMD"] = "----";
                            mItem["HG_BGMD"] = "----";
                            mItem["G_BGMD"] = "----";
                        }
                        if (jcxm.Contains("、含水率、"))
                        {
                            mItem["HG_HSL"] = IsQualified(mItem["G_HSL"], sItem["HSL"], false);
                            mbhggs = mItem["HG_BGMD"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (mItem["HG_BGMD"] != "不合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["HSL"] = "----";
                            mItem["HG_HSL"] = "----";
                            mItem["G_HSL"] = "----";
                        }
                        if (jcxm.Contains("、堆积密度、"))
                        {
                            mItem["HG_SBGMD"] = IsQualified(mItem["G_SBGMD"], sItem["SBGMD"], false);
                            mbhggs = mItem["HG_SBGMD"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (mItem["HG_SBGMD"] != "不合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["SBGMD"] = "----";
                            mItem["HG_SBGMD"] = "----";
                            mItem["G_SBGMD"] = "----";
                        }
                        if (jcxm.Contains("、线性收缩率、") || jcxm.Contains("、线收缩率、"))
                        {
                            if (sItem["XXSSL"] == "无效")
                                mItem["HG_XXSSL"] = "不合格";
                            else
                                mItem["HG_XXSSL"] = IsQualified(mItem["G_XXSSL"], sItem["XXSSL"], false);
                            mbhggs = mItem["HG_XXSSL"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (mItem["HG_XXSSL"] != "不合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["XXSSL"] = "----";
                            mItem["HG_XXSSL"] = "----";
                            mItem["G_XXSSL"] = "----";
                        }
                        if (jcxm.Contains("、软化系数、"))
                        {
                            mItem["HG_RHXS"] = IsQualified(mItem["G_RHXS"], sItem["RHXS"], false);
                            mbhggs = mItem["HG_RHXS"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (mItem["HG_RHXS"] != "不合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["RHXS"] = "----";
                            mItem["HG_RHXS"] = "----";
                            mItem["G_RHXS"] = "----";
                        }
                        if (jcxm.Contains("、难燃性、"))
                        {
                            if (sItem["NRX"] == "符合")
                            {
                                mItem["HG_NRX"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mItem["HG_NRX"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["NRX"] = "----";
                            mItem["HG_NRX"] = "----";
                            mItem["G_NRX"] = "----";
                        }
                        if (jcxm.Contains("、分层度、"))
                        {
                            if (sItem["FCD"] == "无效")
                                mItem["HG_FCD"] = "不合格";
                            else
                                mItem["HG_FCD"] = IsQualified(mItem["G_FCD"], sItem["FCD"], false);
                            mbhggs = mItem["HG_FCD"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (mItem["HG_FCD"] != "不合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["FCD"] = "----";
                            mItem["HG_FCD"] = "----";
                            mItem["G_FCD"] = "----";
                        }

                    }
                    mItem["JCJGMS"] = "";
                    if (jcxm.Contains("、抗冻性、"))
                    {
                        if (sItem["WG"] == "终止试验")
                        {
                            mItem["HG_WG"] = "不合格";
                            mItem["JCJGMS"] = "冻融试验过程中试件明显破坏,";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                        }
                        else
                        {
                            mItem["HG_WG"] = "合格";
                            mItem["HG_ZLSSL"] = IsQualified(mItem["G_ZLSSL"], sItem["ZLSSL"], false);
                            mbhggs = mItem["HG_ZLSSL"] == "不合格" ? mbhggs + 1 : mbhggs;
                            mItem["HG_QDSSL"] = IsQualified(mItem["G_QDSSL"], sItem["QDSSL"], false);
                            mbhggs = mItem["HG_QDSSL"] == "不合格" ? mbhggs + 1 : mbhggs;
                            if (mItem["HG_QDSSL"] != "不合格")
                            {
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                    }
                    else
                    {
                        sItem["WG"] = "----";
                        sItem["ZLSSL"] = "----";
                        sItem["QDSSL"] = "----";
                        mItem["HG_WG"] = "----";
                        mItem["HG_ZLSSL"] = "----";
                        mItem["HG_QDSSL"] = "----";
                        mItem["G_ZLSSL"] = "----";
                        mItem["G_QDSSL"] = "----";
                    }
                    if (mbhggs == 0)
                    {
                        mItem["JCJGMS"] = "该组试件所检项目符合" + mItem["PDBZ"] + "标准要求。";
                        sItem["JCJG"] = "合格";
                    }
                    if (mbhggs >= 1)
                    {
                        mItem["JCJGMS"] = mItem["JCJGMS"] + "该组试件不符合" + mItem["PDBZ"] + "标准要求。";
                        sItem["JCJG"] = "不合格";
                        if (mFlag_Bhg && mFlag_Hg)
                            mItem["JCJGMS"] = "该组试件所检项目部分符合" + mItem["PDBZ"] + "标准要求。";
                    }
                    //mAllHg = (mAllHg && sItem["JCJG"].Trim() == "合格");
                    continue;
                }

                if (sItem["BZXZ"].Contains("JC/T 2200-2013") || sItem["CPMC"].Contains("水泥基泡沫保温板"))
                {
                    #region 表观密度
                    if (jcxm.Contains("、表观密度、"))
                    {
                        if (GetSafeDouble(sItem["GSJC1_1"]) != 0)
                        {
                            sItem["GSJC1"] = (Round((GetSafeDouble(sItem["GSJC1_1"]) + GetSafeDouble(sItem["GSJC1_2"]) + GetSafeDouble(sItem["GSJC1_3"]) + GetSafeDouble(sItem["GSJC1_4"])) / 4, 1)).ToString();
                            sItem["GSJC2"] = (Round((GetSafeDouble(sItem["GSJC2_1"]) + GetSafeDouble(sItem["GSJC2_2"]) + GetSafeDouble(sItem["GSJC2_3"]) + GetSafeDouble(sItem["GSJC2_4"])) / 4, 1)).ToString();
                            sItem["GSJC3"] = (Round((GetSafeDouble(sItem["GSJC3_1"]) + GetSafeDouble(sItem["GSJC3_2"]) + GetSafeDouble(sItem["GSJC3_3"]) + GetSafeDouble(sItem["GSJC3_4"])) / 4, 1)).ToString();
                            sItem["GSJC4"] = (Round((GetSafeDouble(sItem["GSJC4_1"]) + GetSafeDouble(sItem["GSJC4_2"]) + GetSafeDouble(sItem["GSJC4_3"]) + GetSafeDouble(sItem["GSJC4_4"])) / 4, 1)).ToString();
                            sItem["GSJC5"] = (Round((GetSafeDouble(sItem["GSJC5_1"]) + GetSafeDouble(sItem["GSJC5_2"]) + GetSafeDouble(sItem["GSJC5_3"]) + GetSafeDouble(sItem["GSJC5_4"])) / 4, 1)).ToString();
                            sItem["GSJC6"] = (Round((GetSafeDouble(sItem["GSJC6_1"]) + GetSafeDouble(sItem["GSJC6_2"]) + GetSafeDouble(sItem["GSJC6_3"]) + GetSafeDouble(sItem["GSJC6_4"])) / 4, 1)).ToString();
                        }
                        if (GetSafeDouble(sItem["GSJK1_1"]) != 0)
                        {
                            sItem["GSJK1"] = (Round((GetSafeDouble(sItem["GSJK1_1"]) + GetSafeDouble(sItem["GSJK1_2"]) + GetSafeDouble(sItem["GSJK1_3"]) + GetSafeDouble(sItem["GSJK1_4"])) / 4, 1)).ToString();
                            sItem["GSJK2"] = (Round((GetSafeDouble(sItem["GSJK2_1"]) + GetSafeDouble(sItem["GSJK2_2"]) + GetSafeDouble(sItem["GSJK2_3"]) + GetSafeDouble(sItem["GSJK2_4"])) / 4, 1)).ToString();
                            sItem["GSJK3"] = (Round((GetSafeDouble(sItem["GSJK3_1"]) + GetSafeDouble(sItem["GSJK3_2"]) + GetSafeDouble(sItem["GSJK3_3"]) + GetSafeDouble(sItem["GSJK3_4"])) / 4, 1)).ToString();
                            sItem["GSJK4"] = (Round((GetSafeDouble(sItem["GSJK4_1"]) + GetSafeDouble(sItem["GSJK4_2"]) + GetSafeDouble(sItem["GSJK4_3"]) + GetSafeDouble(sItem["GSJK4_4"])) / 4, 1)).ToString();
                            sItem["GSJK5"] = (Round((GetSafeDouble(sItem["GSJK5_1"]) + GetSafeDouble(sItem["GSJK5_2"]) + GetSafeDouble(sItem["GSJK5_3"]) + GetSafeDouble(sItem["GSJK5_4"])) / 4, 1)).ToString();
                            sItem["GSJK6"] = (Round((GetSafeDouble(sItem["GSJK6_1"]) + GetSafeDouble(sItem["GSJK6_2"]) + GetSafeDouble(sItem["GSJK6_3"]) + GetSafeDouble(sItem["GSJK6_4"])) / 4, 1)).ToString();
                        }
                        if (GetSafeDouble(sItem["GSJG1_1"]) != 0)
                        {
                            sItem["GSJG1"] = (Round((GetSafeDouble(sItem["GSJG1_1"]) + GetSafeDouble(sItem["GSJG1_2"]) + GetSafeDouble(sItem["GSJG1_3"]) + GetSafeDouble(sItem["GSJG1_4"]) + GetSafeDouble(sItem["GSJG1_5"]) + GetSafeDouble(sItem["GSJG1_6"])) / 6, 1)).ToString();
                            sItem["GSJG2"] = (Round((GetSafeDouble(sItem["GSJG2_1"]) + GetSafeDouble(sItem["GSJG2_2"]) + GetSafeDouble(sItem["GSJG2_3"]) + GetSafeDouble(sItem["GSJG2_4"]) + GetSafeDouble(sItem["GSJG2_5"]) + GetSafeDouble(sItem["GSJG2_6"])) / 6, 1)).ToString();
                            sItem["GSJG3"] = (Round((GetSafeDouble(sItem["GSJG3_1"]) + GetSafeDouble(sItem["GSJG3_2"]) + GetSafeDouble(sItem["GSJG3_3"]) + GetSafeDouble(sItem["GSJG3_4"]) + GetSafeDouble(sItem["GSJG3_5"]) + GetSafeDouble(sItem["GSJG3_6"])) / 6, 1)).ToString();
                            sItem["GSJG4"] = (Round((GetSafeDouble(sItem["GSJG4_1"]) + GetSafeDouble(sItem["GSJG4_2"]) + GetSafeDouble(sItem["GSJG4_3"]) + GetSafeDouble(sItem["GSJG4_4"]) + GetSafeDouble(sItem["GSJG4_5"]) + GetSafeDouble(sItem["GSJG4_6"])) / 6, 1)).ToString();
                            sItem["GSJG5"] = (Round((GetSafeDouble(sItem["GSJG5_1"]) + GetSafeDouble(sItem["GSJG5_2"]) + GetSafeDouble(sItem["GSJG5_3"]) + GetSafeDouble(sItem["GSJG5_4"]) + GetSafeDouble(sItem["GSJG5_5"]) + GetSafeDouble(sItem["GSJG5_6"])) / 6, 1)).ToString();
                            sItem["GSJG6"] = (Round((GetSafeDouble(sItem["GSJG6_1"]) + GetSafeDouble(sItem["GSJG6_2"]) + GetSafeDouble(sItem["GSJG6_3"]) + GetSafeDouble(sItem["GSJG6_4"]) + GetSafeDouble(sItem["GSJG6_5"]) + GetSafeDouble(sItem["GSJG6_6"])) / 6, 1)).ToString();
                        }

                        sItem["GSJTJ1"] = (Round(GetSafeDouble(sItem["GSJC1"]) * (GetSafeDouble(sItem["GSJK1"])) * (GetSafeDouble(sItem["GSJG1"])), 0)).ToString();
                        sItem["GSJTJ2"] = (Round(GetSafeDouble(sItem["GSJC2"]) * (GetSafeDouble(sItem["GSJK2"])) * (GetSafeDouble(sItem["GSJG2"])), 0)).ToString();
                        sItem["GSJTJ3"] = (Round(GetSafeDouble(sItem["GSJC3"]) * (GetSafeDouble(sItem["GSJK3"])) * (GetSafeDouble(sItem["GSJG3"])), 0)).ToString();
                        sItem["GSJTJ4"] = (Round(GetSafeDouble(sItem["GSJC4"]) * (GetSafeDouble(sItem["GSJK4"])) * (GetSafeDouble(sItem["GSJG4"])), 0)).ToString();
                        sItem["GSJTJ5"] = (Round(GetSafeDouble(sItem["GSJC5"]) * (GetSafeDouble(sItem["GSJK5"])) * (GetSafeDouble(sItem["GSJG5"])), 0)).ToString();
                        sItem["GSJTJ6"] = (Round(GetSafeDouble(sItem["GSJC6"]) * (GetSafeDouble(sItem["GSJK6"])) * (GetSafeDouble(sItem["GSJG6"])), 0)).ToString();


                        sItem["BGMD1"] = (GetSafeDouble(sItem["GSJZL1"]) / 1000 / GetSafeDouble(sItem["GSJC1"]) / 1000 / GetSafeDouble(sItem["GSJK1"]) / 1000 / GetSafeDouble(sItem["GSJG1"]) / 1000).ToString("G4");
                        sItem["BGMD2"] = (GetSafeDouble(sItem["GSJZL2"]) / 1000 / GetSafeDouble(sItem["GSJC2"]) / 1000 / GetSafeDouble(sItem["GSJK2"]) / 1000 / GetSafeDouble(sItem["GSJG2"]) / 1000).ToString("G4");
                        sItem["BGMD3"] = (GetSafeDouble(sItem["GSJZL3"]) / 1000 / GetSafeDouble(sItem["GSJC3"]) / 1000 / GetSafeDouble(sItem["GSJK3"]) / 1000 / GetSafeDouble(sItem["GSJG3"]) / 1000).ToString("G4");
                        sItem["BGMD4"] = (GetSafeDouble(sItem["GSJZL4"]) / 1000 / GetSafeDouble(sItem["GSJC4"]) / 1000 / GetSafeDouble(sItem["GSJK4"]) / 1000 / GetSafeDouble(sItem["GSJG4"]) / 1000).ToString("G4");
                        sItem["BGMD5"] = (GetSafeDouble(sItem["GSJZL5"]) / 1000 / GetSafeDouble(sItem["GSJC5"]) / 1000 / GetSafeDouble(sItem["GSJK5"]) / 1000 / GetSafeDouble(sItem["GSJG5"]) / 1000).ToString("G4");
                        sItem["BGMD6"] = (GetSafeDouble(sItem["GSJZL6"]) / 1000 / GetSafeDouble(sItem["GSJC6"]) / 1000 / GetSafeDouble(sItem["GSJK6"]) / 1000 / GetSafeDouble(sItem["GSJG6"]) / 1000).ToString("G4");


                        sItem["BGMD"] = (GetSafeDouble(sItem["BGMD1"]) + GetSafeDouble(sItem["BGMD2"]) + GetSafeDouble(sItem["BGMD3"]) + GetSafeDouble(sItem["BGMD4"]) + GetSafeDouble(sItem["BGMD5"]) + GetSafeDouble(sItem["BGMD6"]) / 6).ToString("G3");

                        string drxs = IsQualified(mItem["G_BGMD"], sItem["BGMD"], true);
                        if (drxs == "符合")
                        {
                            mItem["HG_BGMD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_BGMD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["BGMD"] = "----";
                        mItem["HG_BGMD"] = "----";
                        mItem["G_BGMD"] = "----";
                    }
                    #endregion

                    #region 抗压强度
                    if (jcxm.Contains("、抗压强度、"))
                    {
                        double mMj1, mMj2, mMj3, mMj4;
                        double mKyqd1 = 0, mKyqd2 = 0, mKyqd3 = 0, mKyqd4 = 0;
                        if (GetSafeDouble(sItem["KYHZ1"]) > 0)
                        {
                            mMj1 = GetSafeDouble(sItem["KYCD1"]) * GetSafeDouble(sItem["KYKD1"]);
                            mMj2 = GetSafeDouble(sItem["KYCD2"]) * GetSafeDouble(sItem["KYKD2"]);
                            mMj3 = GetSafeDouble(sItem["KYCD3"]) * GetSafeDouble(sItem["KYKD3"]);
                            mMj4 = GetSafeDouble(sItem["KYCD4"]) * GetSafeDouble(sItem["KYKD4"]);

                            sItem["KYMJ1"] = mMj1.ToString();
                            sItem["KYMJ2"] = mMj2.ToString();
                            sItem["KYMJ3"] = mMj3.ToString();
                            sItem["KYMJ4"] = mMj4.ToString();

                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0)
                            {
                                mKyqd1 = Round(GetSafeDouble(sItem["KYHZ1"]) / mMj1, 3);
                                mKyqd2 = Round(GetSafeDouble(sItem["KYHZ2"]) / mMj2, 3);
                                mKyqd3 = Round(GetSafeDouble(sItem["KYHZ3"]) / mMj3, 3);
                                mKyqd4 = Round(GetSafeDouble(sItem["KYHZ4"]) / mMj4, 3);
                            }
                            else
                            {
                                mKyqd1 = 0;
                                mKyqd2 = 0;
                                mKyqd3 = 0;
                                mKyqd4 = 0;
                            }
                            sItem["KYQD1"] = mKyqd1.ToString();
                            sItem["KYQD2"] = mKyqd2.ToString();
                            sItem["KYQD3"] = mKyqd3.ToString();
                            sItem["KYQD4"] = mKyqd4.ToString();

                            string mlongStr = (mKyqd1) + "," + (mKyqd2) + "," + (mKyqd3) + "," + (mKyqd4);
                            string[] mtmpArray = mlongStr.Split(',');
                            List<double> mtmpList = new List<double>();
                            foreach (string str in mtmpArray)
                            {
                                mtmpList.Add(GetSafeDouble(str));
                            }
                            mtmpList.Sort();
                            double mAvgKyqd = Round(mtmpList.Average(), 2);

                            sItem["KYQD"] = Round(mAvgKyqd, 2).ToString();//'抗压平均字符类型为字符型

                            string drxs = IsQualified(mItem["G_KYQD"], sItem["KYQD"], true);
                            if (drxs == "符合")
                            {
                                mItem["HG_KYQD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mItem["HG_KYQD"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            mSFwc = false;
                            sItem["KYQD"] = "----";
                            mItem["HG_KYQD"] = "----";
                            mItem["G_KYQD"] = "----";
                        }
                    }
                    #endregion

                    #region 导热系数
                    if (jcxm.Contains("、导热系数、"))
                    {
                        string drxs = IsQualified(mItem["G_DRXS"], sItem["DRXS"], true);
                        if (drxs == "符合")
                        {
                            mItem["HG_DRXS"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_DRXS"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["DRXS"] = "----";
                        mItem["HG_DRXS"] = "----";
                        mItem["G_DRXS"] = "----";
                    }
                    #endregion
                }
                else
                {
                    if (jcxm.Contains("、导热系数、"))
                    {
                        string drxs = IsQualified(mItem["G_DRXS"], sItem["DRXS"], true);
                        if (drxs == "符合")
                        {
                            mItem["HG_DRXS"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_DRXS"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["DRXS"] = "----";
                        mItem["HG_DRXS"] = "----";
                        mItem["G_DRXS"] = "----";
                    }

                    if (jcxm.Contains("、抗压强度、"))
                    {
                        double mMj1, mMj2, mMj3, mMj4, mMj5, mMj6;
                        double mKyqd1 = 0, mKyqd2 = 0, mKyqd3 = 0, mKyqd4 = 0, mKyqd5 = 0, mKyqd6 = 0;
                        if (GetSafeDouble(sItem["KYHZ1"]) > 0)
                        {
                            mMj1 = GetSafeDouble(sItem["KYCD1"]) * GetSafeDouble(sItem["KYKD1"]);
                            mMj2 = GetSafeDouble(sItem["KYCD2"]) * GetSafeDouble(sItem["KYKD2"]);
                            mMj3 = GetSafeDouble(sItem["KYCD3"]) * GetSafeDouble(sItem["KYKD3"]);
                            mMj4 = GetSafeDouble(sItem["KYCD4"]) * GetSafeDouble(sItem["KYKD4"]);
                            mMj5 = GetSafeDouble(sItem["KYCD5"]) * GetSafeDouble(sItem["KYKD5"]);
                            mMj6 = GetSafeDouble(sItem["KYCD6"]) * GetSafeDouble(sItem["KYKD6"]);

                            sItem["KYMJ1"] = mMj1.ToString();
                            sItem["KYMJ2"] = mMj2.ToString();
                            sItem["KYMJ3"] = mMj3.ToString();
                            sItem["KYMJ4"] = mMj4.ToString();
                            sItem["KYMJ5"] = mMj5.ToString();
                            sItem["KYMJ6"] = mMj6.ToString();

                            if (mMj1 != 0 && mMj2 != 0 && mMj3 != 0 && mMj4 != 0 && mMj5 != 0 && mMj6 != 0)
                            {
                                mKyqd1 = Round(GetSafeDouble(sItem["KYHZ1"]) / mMj1, 3);
                                mKyqd2 = Round(GetSafeDouble(sItem["KYHZ2"]) / mMj2, 3);
                                mKyqd3 = Round(GetSafeDouble(sItem["KYHZ3"]) / mMj3, 3);
                                mKyqd4 = Round(GetSafeDouble(sItem["KYHZ4"]) / mMj4, 3);
                                mKyqd5 = Round(GetSafeDouble(sItem["KYHZ5"]) / mMj5, 3);
                                mKyqd6 = Round(GetSafeDouble(sItem["KYHZ6"]) / mMj6, 3);
                            }
                            else
                            {
                                mKyqd1 = 0;
                                mKyqd2 = 0;
                                mKyqd3 = 0;
                                mKyqd4 = 0;
                                mKyqd5 = 0;
                                mKyqd6 = 0;
                            }
                            sItem["KYQD1"] = mKyqd1.ToString();
                            sItem["KYQD2"] = mKyqd2.ToString();
                            sItem["KYQD3"] = mKyqd3.ToString();
                            sItem["KYQD4"] = mKyqd4.ToString();
                            sItem["KYQD5"] = mKyqd5.ToString();
                            sItem["KYQD6"] = mKyqd6.ToString();

                            string mlongStr = (mKyqd1) + "," + (mKyqd2) + "," + (mKyqd3) + "," + (mKyqd4) + "," + (mKyqd5) + ",";
                            string[] mtmpArray = mlongStr.Split(',');
                            List<double> mtmpList = new List<double>();
                            foreach (string str in mtmpArray)
                            {
                                mtmpList.Add(GetSafeDouble(str));
                            }
                            mtmpList.Sort();
                            double mMaxKyqd = mtmpList[4];
                            double mMinKyqd = mtmpList[0];
                            double mAvgKyqd = Round(mtmpList.Average(), 3);
                            if (mAvgKyqd > 0.00001 && mMaxKyqd > 0.000001)
                            {
                                if ((mMaxKyqd - mAvgKyqd) > mAvgKyqd * 0.2 || (mAvgKyqd - mMinKyqd) > mAvgKyqd * 0.2)
                                {
                                    mAvgKyqd = (mtmpList.Sum() - mMaxKyqd - mMinKyqd) / 3;
                                    jsbeizhu = "最大强度或最小强度超出平均强度值的20%结果为剔除最大和最小值后的平均值";
                                }
                            }
                            sItem["KYQD"] = Round(mAvgKyqd, 2).ToString();//'抗压平均字符类型为字符型

                            string drxs = IsQualified(mItem["G_KYQD"], sItem["KYQD"], true);
                            if (drxs == "符合")
                            {
                                mItem["HG_KYQD"] = "合格";
                                mFlag_Hg = true;
                            }
                            else
                            {
                                mItem["HG_KYQD"] = "不合格";
                                mbhggs = mbhggs + 1;
                                mFlag_Bhg = true;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            mSFwc = false;
                            sItem["KYQD"] = "----";
                            mItem["HG_KYQD"] = "----";
                            mItem["G_KYQD"] = "----";
                        }
                    }
                    else
                    {
                        sItem["KYQD"] = "----";
                        mItem["HG_KYQD"] = "----";
                        mItem["G_KYQD"] = "----";
                    }

                    if (jcxm.Contains("、压剪粘结强度、"))
                    {
                        if (GetSafeDouble(sItem["YJHZ1"]) == 0)
                        {
                            mSFwc = false;
                        }
                        double mysqd1, mysqd2, mysqd3;
                        mysqd1 = Round(GetSafeDouble(sItem["YJHZ1"]) * 1000 / (GetSafeDouble(sItem["YJC1"]) * GetSafeDouble(sItem["YJK1"])), 1);
                        mysqd2 = Round(GetSafeDouble(sItem["YJHZ2"]) * 1000 / (GetSafeDouble(sItem["YJC2"]) * GetSafeDouble(sItem["YJK2"])), 1);
                        mysqd3 = Round(GetSafeDouble(sItem["YJHZ3"]) * 1000 / (GetSafeDouble(sItem["YJC3"]) * GetSafeDouble(sItem["YJK3"])), 1);
                        sItem["YSQD"] = Round((mysqd1 + mysqd2 + mysqd3) / 3, 0).ToString();

                        string drxs = IsQualified(mItem["G_YSQD"], sItem["YSQD"], true);
                        if (drxs == "符合")
                        {
                            mItem["HG_YSQD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_YSQD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["YSQD"] = "----";
                        mItem["HG_YSQD"] = "----";
                        mItem["G_YSQD"] = "----";
                    }

                    if (jcxm.Contains("、干密度、"))
                    {
                        if (GetSafeDouble(sItem["GSJC1_1"]) != 0)
                        {
                            sItem["GSJC1"] = (Round((GetSafeDouble(sItem["GSJC1_1"]) + GetSafeDouble(sItem["GSJC1_2"]) + GetSafeDouble(sItem["GSJC1_3"]) + GetSafeDouble(sItem["GSJC1_4"])) / 4, 1)).ToString();
                            sItem["GSJC2"] = (Round((GetSafeDouble(sItem["GSJC2_1"]) + GetSafeDouble(sItem["GSJC2_2"]) + GetSafeDouble(sItem["GSJC2_3"]) + GetSafeDouble(sItem["GSJC2_4"])) / 4, 1)).ToString();
                            sItem["GSJC3"] = (Round((GetSafeDouble(sItem["GSJC3_1"]) + GetSafeDouble(sItem["GSJC3_2"]) + GetSafeDouble(sItem["GSJC3_3"]) + GetSafeDouble(sItem["GSJC3_4"])) / 4, 1)).ToString();
                            sItem["GSJC4"] = (Round((GetSafeDouble(sItem["GSJC4_1"]) + GetSafeDouble(sItem["GSJC4_2"]) + GetSafeDouble(sItem["GSJC4_3"]) + GetSafeDouble(sItem["GSJC4_4"])) / 4, 1)).ToString();
                            sItem["GSJC5"] = (Round((GetSafeDouble(sItem["GSJC5_1"]) + GetSafeDouble(sItem["GSJC5_2"]) + GetSafeDouble(sItem["GSJC5_3"]) + GetSafeDouble(sItem["GSJC5_4"])) / 4, 1)).ToString();
                            sItem["GSJC6"] = (Round((GetSafeDouble(sItem["GSJC6_1"]) + GetSafeDouble(sItem["GSJC6_2"]) + GetSafeDouble(sItem["GSJC6_3"]) + GetSafeDouble(sItem["GSJC6_4"])) / 4, 1)).ToString();
                        }
                        if (GetSafeDouble(sItem["GSJK1_1"]) != 0)
                        {
                            sItem["GSJK1"] = (Round((GetSafeDouble(sItem["GSJK1_1"]) + GetSafeDouble(sItem["GSJK1_2"]) + GetSafeDouble(sItem["GSJK1_3"]) + GetSafeDouble(sItem["GSJK1_4"])) / 4, 1)).ToString();
                            sItem["GSJK2"] = (Round((GetSafeDouble(sItem["GSJK2_1"]) + GetSafeDouble(sItem["GSJK2_2"]) + GetSafeDouble(sItem["GSJK2_3"]) + GetSafeDouble(sItem["GSJK2_4"])) / 4, 1)).ToString();
                            sItem["GSJK3"] = (Round((GetSafeDouble(sItem["GSJK3_1"]) + GetSafeDouble(sItem["GSJK3_2"]) + GetSafeDouble(sItem["GSJK3_3"]) + GetSafeDouble(sItem["GSJK3_4"])) / 4, 1)).ToString();
                            sItem["GSJK4"] = (Round((GetSafeDouble(sItem["GSJK4_1"]) + GetSafeDouble(sItem["GSJK4_2"]) + GetSafeDouble(sItem["GSJK4_3"]) + GetSafeDouble(sItem["GSJK4_4"])) / 4, 1)).ToString();
                            sItem["GSJK5"] = (Round((GetSafeDouble(sItem["GSJK5_1"]) + GetSafeDouble(sItem["GSJK5_2"]) + GetSafeDouble(sItem["GSJK5_3"]) + GetSafeDouble(sItem["GSJK5_4"])) / 4, 1)).ToString();
                            sItem["GSJK6"] = (Round((GetSafeDouble(sItem["GSJK6_1"]) + GetSafeDouble(sItem["GSJK6_2"]) + GetSafeDouble(sItem["GSJK6_3"]) + GetSafeDouble(sItem["GSJK6_4"])) / 4, 1)).ToString();
                        }
                        if (GetSafeDouble(sItem["GSJG1_1"]) != 0)
                        {
                            sItem["GSJG1"] = (Round((GetSafeDouble(sItem["GSJG1_1"]) + GetSafeDouble(sItem["GSJG1_2"]) + GetSafeDouble(sItem["GSJG1_3"]) + GetSafeDouble(sItem["GSJG1_4"]) + GetSafeDouble(sItem["GSJG1_5"]) + GetSafeDouble(sItem["GSJG1_6"])) / 6, 1)).ToString();
                            sItem["GSJG2"] = (Round((GetSafeDouble(sItem["GSJG2_1"]) + GetSafeDouble(sItem["GSJG2_2"]) + GetSafeDouble(sItem["GSJG2_3"]) + GetSafeDouble(sItem["GSJG2_4"]) + GetSafeDouble(sItem["GSJG2_5"]) + GetSafeDouble(sItem["GSJG2_6"])) / 6, 1)).ToString();
                            sItem["GSJG3"] = (Round((GetSafeDouble(sItem["GSJG3_1"]) + GetSafeDouble(sItem["GSJG3_2"]) + GetSafeDouble(sItem["GSJG3_3"]) + GetSafeDouble(sItem["GSJG3_4"]) + GetSafeDouble(sItem["GSJG3_5"]) + GetSafeDouble(sItem["GSJG3_6"])) / 6, 1)).ToString();
                            sItem["GSJG4"] = (Round((GetSafeDouble(sItem["GSJG4_1"]) + GetSafeDouble(sItem["GSJG4_2"]) + GetSafeDouble(sItem["GSJG4_3"]) + GetSafeDouble(sItem["GSJG4_4"]) + GetSafeDouble(sItem["GSJG4_5"]) + GetSafeDouble(sItem["GSJG4_6"])) / 6, 1)).ToString();
                            sItem["GSJG5"] = (Round((GetSafeDouble(sItem["GSJG5_1"]) + GetSafeDouble(sItem["GSJG5_2"]) + GetSafeDouble(sItem["GSJG5_3"]) + GetSafeDouble(sItem["GSJG5_4"]) + GetSafeDouble(sItem["GSJG5_5"]) + GetSafeDouble(sItem["GSJG5_6"])) / 6, 1)).ToString();
                            sItem["GSJG6"] = (Round((GetSafeDouble(sItem["GSJG6_1"]) + GetSafeDouble(sItem["GSJG6_2"]) + GetSafeDouble(sItem["GSJG6_3"]) + GetSafeDouble(sItem["GSJG6_4"]) + GetSafeDouble(sItem["GSJG6_5"]) + GetSafeDouble(sItem["GSJG6_6"])) / 6, 1)).ToString();
                        }

                        sItem["GSJTJ1"] = (Round(GetSafeDouble(sItem["GSJC1"]) * (GetSafeDouble(sItem["GSJK1"])) * (GetSafeDouble(sItem["GSJG1"])), 0)).ToString();
                        sItem["GSJTJ2"] = (Round(GetSafeDouble(sItem["GSJC2"]) * (GetSafeDouble(sItem["GSJK2"])) * (GetSafeDouble(sItem["GSJG2"])), 0)).ToString();
                        sItem["GSJTJ3"] = (Round(GetSafeDouble(sItem["GSJC3"]) * (GetSafeDouble(sItem["GSJK3"])) * (GetSafeDouble(sItem["GSJG3"])), 0)).ToString();
                        sItem["GSJTJ4"] = (Round(GetSafeDouble(sItem["GSJC4"]) * (GetSafeDouble(sItem["GSJK4"])) * (GetSafeDouble(sItem["GSJG4"])), 0)).ToString();
                        sItem["GSJTJ5"] = (Round(GetSafeDouble(sItem["GSJC5"]) * (GetSafeDouble(sItem["GSJK5"])) * (GetSafeDouble(sItem["GSJG5"])), 0)).ToString();
                        sItem["GSJTJ6"] = (Round(GetSafeDouble(sItem["GSJC6"]) * (GetSafeDouble(sItem["GSJK6"])) * (GetSafeDouble(sItem["GSJG6"])), 0)).ToString();


                        sItem["BGMD1"] = (GetSafeDouble(sItem["GSJZL1"]) / 1000 / GetSafeDouble(sItem["GSJC1"]) / 1000 / GetSafeDouble(sItem["GSJK1"]) / 1000 / GetSafeDouble(sItem["GSJG1"]) / 1000).ToString("G4");
                        sItem["BGMD2"] = (GetSafeDouble(sItem["GSJZL2"]) / 1000 / GetSafeDouble(sItem["GSJC2"]) / 1000 / GetSafeDouble(sItem["GSJK2"]) / 1000 / GetSafeDouble(sItem["GSJG2"]) / 1000).ToString("G4");
                        sItem["BGMD3"] = (GetSafeDouble(sItem["GSJZL3"]) / 1000 / GetSafeDouble(sItem["GSJC3"]) / 1000 / GetSafeDouble(sItem["GSJK3"]) / 1000 / GetSafeDouble(sItem["GSJG3"]) / 1000).ToString("G4");
                        sItem["BGMD4"] = (GetSafeDouble(sItem["GSJZL4"]) / 1000 / GetSafeDouble(sItem["GSJC4"]) / 1000 / GetSafeDouble(sItem["GSJK4"]) / 1000 / GetSafeDouble(sItem["GSJG4"]) / 1000).ToString("G4");
                        sItem["BGMD5"] = (GetSafeDouble(sItem["GSJZL5"]) / 1000 / GetSafeDouble(sItem["GSJC5"]) / 1000 / GetSafeDouble(sItem["GSJK5"]) / 1000 / GetSafeDouble(sItem["GSJG5"]) / 1000).ToString("G4");
                        sItem["BGMD6"] = (GetSafeDouble(sItem["GSJZL6"]) / 1000 / GetSafeDouble(sItem["GSJC6"]) / 1000 / GetSafeDouble(sItem["GSJK6"]) / 1000 / GetSafeDouble(sItem["GSJG6"]) / 1000).ToString("G4");


                        sItem["BGMD"] = (GetSafeDouble(sItem["BGMD1"]) + GetSafeDouble(sItem["BGMD2"]) + GetSafeDouble(sItem["BGMD3"]) + GetSafeDouble(sItem["BGMD4"]) + GetSafeDouble(sItem["BGMD5"]) + GetSafeDouble(sItem["BGMD6"]) / 6).ToString("G3");

                        string drxs = IsQualified(mItem["G_BGMD"], sItem["BGMD"], true);
                        if (drxs == "符合")
                        {
                            mItem["HG_BGMD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_BGMD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["BGMD"] = "----";
                        mItem["HG_BGMD"] = "----";
                        mItem["G_BGMD"] = "----";
                    }

                    if (jcxm.Contains("、堆积密度、"))
                    {
                        if (!string.IsNullOrEmpty(sItem["SJLLTZL1"]))
                        {
                            for (int i = 1; i <= 3; i++)
                            {
                                sItem["SJLZL" + i] = (GetSafeDouble(sItem["SJLLTZL" + i]) - GetSafeDouble(sItem["SLTZL"])).ToString();
                            }
                        }
                        for (int i = 1; i <= 3; i++)
                        {
                            sItem["SBGMD" + i] = (GetSafeDouble(sItem["SJLZL" + i]) / 1000 / GetSafeDouble(sItem["SLTTJ"])).ToString("G4");
                        }
                        sItem["SBGMD"] = ((GetSafeDouble(sItem["SBGMD1"]) + GetSafeDouble(sItem["SBGMD2"]) + GetSafeDouble(sItem["SBGMD3"])) / 3).ToString("G3");

                        string drxs = IsQualified(mItem["G_SBGMD"], sItem["SBGMD"], true);
                        if (drxs == "符合")
                        {
                            mItem["HG_SBGMD"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_SBGMD"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["SBGMD"] = "----";
                        mItem["HG_SBGMD"] = "----";
                        mItem["G_SBGMD"] = "----";
                    }

                    if (jcxm.Contains("、软化系数、"))
                    {

                        string drxs = IsQualified(mItem["G_RHXS"], sItem["RHXS"], true);
                        if (drxs == "符合")
                        {
                            mItem["HG_RHXS"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_RHXS"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["RHXS"] = "----";
                        mItem["HG_RHXS"] = "----";
                        mItem["G_RHXS"] = "----";
                    }

                    if (jcxm.Contains("、难燃性、"))
                    {
                        if (sItem["NRX"] == "符合")
                        {
                            mItem["HG_NRX"] = "合格";
                            mFlag_Hg = true;
                        }
                        else
                        {
                            mItem["HG_NRX"] = "不合格";
                            mbhggs = mbhggs + 1;
                            mFlag_Bhg = true;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["NRX"] = "----";
                        mItem["HG_NRX"] = "----";
                        mItem["G_NRX"] = "----";
                    }

                }
                //if (mbhggs == 0)
                //{
                //    jsbeizhu = "该组试件所检项目符合上述标准要求";
                //    sItem["JCJG"] = "合格";
                //}
                //else
                //{
                //    jsbeizhu = "该组试件不符合上述标准要求";
                //    sItem["JCJG"] = "不合格";
                //    if (mFlag_Bhg && mFlag_Hg)
                //    {
                //        jsbeizhu = "该组试件所检项目符合上述标准要求。";
                //    }
                //}
            }

            #region 添加最终报告
            //主表总判断赋值
            if (mAllHg)
            {
                mjcjg = "合格";
                jsbeizhu = "该组试件所检项目符合上述标准要求。";
            }
            else
            {
                mjcjg = "不合格";
                jsbeizhu = "该组试件不符合上述标准要求";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
