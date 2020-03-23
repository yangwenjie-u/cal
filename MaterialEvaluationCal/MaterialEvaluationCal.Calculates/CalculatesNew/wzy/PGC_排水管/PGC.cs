using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


namespace Calculates
{
    public class PGC : BaseMethods
    {
        public void Calc()
        {
            #region 
            #region  参数定义
            string mcalBh;
            string mMaxBgbh;
            string mJSFF;
            bool mAllHg;
            bool mGetBgbh;
            int mbhggs, mbhggs1, mhgpds, mbhgpds;
            string mGxl, mSjdj;
            bool mSFwc;
            mSFwc = true;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_PGC_DJ"];
            var mrscyfa = dataExtra["BZ_GCCYFA"];
            var mrslccj = dataExtra["BZ_GCLCCJ"];
            var mrsYysycs = dataExtra["BZ_GCYYSYCS"];
            var MItem = data["M_PGC"];
            var mitem = MItem[0];
            var SItem = data["S_PGC"];
            #endregion

            #region 计算开始
            mGetBgbh = false;
            mAllHg = true;
            mitem["JCJGMS"] = "";
            foreach (var mrscyfa_item in mrscyfa)
            {
                var sitem = SItem[0];
                var df = sitem["DBSL"];

                if (GetSafeDouble(sitem["DBSL"]) >= GetSafeDouble(mrscyfa_item["PFW1"]) && GetSafeDouble(sitem["DBSL"]) <= GetSafeDouble(mrscyfa_item["PFW2"]))
                {
                    mhgpds = GetSafeInt(mrscyfa_item["HGPDS"]);
                    mbhgpds = GetSafeInt(mrscyfa_item["BHGPDS"]);
                    break;
                }
            }
            var jcxm = "";

            foreach (var sitem in SItem)
            {
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                mSjdj = sitem["SJDJ"]; //管材名称
                if (string.IsNullOrEmpty(mSjdj))
                    mSjdj = "";
                mGxl = sitem["GXL"]; //环刚度(管系列)
                if (string.IsNullOrEmpty(mGxl))
                    mGxl = "";
                if (mGxl == "")
                    mGxl = "----";
                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["MC"].Contains(mSjdj) && x["HGDDH"].Contains(mGxl));
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                    mitem["G_WG"] = string.IsNullOrEmpty(mrsDj_Filter["WG"]) ? "" : mrsDj_Filter["WG"].Trim();
                    mitem["G_BZ"] = string.IsNullOrEmpty(mrsDj_Filter["BZ"]) ? "" : mrsDj_Filter["BZ"].Trim();
                    mitem["G_DLSCL"] = string.IsNullOrEmpty(mrsDj_Filter["DLSCL"]) ? "0" : mrsDj_Filter["DLSCL"].Trim();
                    mitem["G_PJWJ"] = string.IsNullOrEmpty(mrsDj_Filter["PJWJ"]) ? "0" : mrsDj_Filter["PJWJ"].Trim();
                    mitem["G_PJWJPC"] = string.IsNullOrEmpty(mrsDj_Filter["PJWJPC"]) ? "0" : mrsDj_Filter["PJWJPC"].Trim();
                    mitem["G_BH"] = string.IsNullOrEmpty(mrsDj_Filter["LCBH"]) ? "0" : mrsDj_Filter["LCBH"].Trim();
                    mitem["G_BHPC"] = string.IsNullOrEmpty(mrsDj_Filter["BHPC"]) ? "0" : mrsDj_Filter["BHPC"].Trim();
                    mitem["G_BHPCL"] = string.IsNullOrEmpty(mrsDj_Filter["BHPCL"]) ? "0" : mrsDj_Filter["BHPCL"].Trim();
                    mitem["G_JXLL1"] = string.IsNullOrEmpty(mrsDj_Filter["JXLL1"]) ? "0" : mrsDj_Filter["JXLL1"].Trim();
                    mitem["G_JXLL2"] = string.IsNullOrEmpty(mrsDj_Filter["JXLL2"]) ? "0" : mrsDj_Filter["JXLL2"].Trim();
                    mitem["G_NHQD"] = string.IsNullOrEmpty(mrsDj_Filter["NHQD"]) ? "" : mrsDj_Filter["NHQD"].Trim();
                    mitem["G_JNYQD"] = string.IsNullOrEmpty(mrsDj_Filter["JNYQD"]) ? "" : mrsDj_Filter["JNYQD"].Trim();
                    mitem["G_YLSY"] = string.IsNullOrEmpty(mrsDj_Filter["YLSY"]) ? "" : mrsDj_Filter["YLSY"].Trim();
                    mitem["G_BPQD"] = string.IsNullOrEmpty(mrsDj_Filter["BPQD"]) ? "0" : mrsDj_Filter["BPQD"].Trim();
                    mitem["G_NJYXZX"] = string.IsNullOrEmpty(mrsDj_Filter["NJYXZX"]) ? "0" : mrsDj_Filter["NJYXZX"].Trim();
                    mitem["G_WJYXZX"] = string.IsNullOrEmpty(mrsDj_Filter["WJYXZX"]) ? "0" : mrsDj_Filter["WJYXZX"].Trim();
                    mitem["G_LCZXHD"] = string.IsNullOrEmpty(mrsDj_Filter["LCZXHD"]) ? "0" : mrsDj_Filter["LCZXHD"].Trim();
                    mitem["G_MD"] = string.IsNullOrEmpty(mrsDj_Filter["MD"]) ? "" : mrsDj_Filter["MD"].Trim();
                    mitem["G_MFSY"] = string.IsNullOrEmpty(mrsDj_Filter["MFSY"]) ? "" : mrsDj_Filter["MFSY"].Trim();
                    mitem["G_BTGX"] = string.IsNullOrEmpty(mrsDj_Filter["BTGX"]) ? "" : mrsDj_Filter["BTGX"].Trim();
                    //以下赤峰开展项目
                    mitem["G_BPSY"] = string.IsNullOrEmpty(mrsDj_Filter["BPSY"]) ? "" : mrsDj_Filter["BPSY"].Trim(); //扁平试验
                    mitem["G_RHWD"] = string.IsNullOrEmpty(mrsDj_Filter["RHWD"]) ? "0" : mrsDj_Filter["RHWD"].Trim(); //维卡软化软化温度
                    mitem["G_ZXHSL"] = string.IsNullOrEmpty(mrsDj_Filter["ZXHSL"]) ? "0" : mrsDj_Filter["ZXHSL"].Trim(); //纵向回缩率
                    mitem["G_LCCJ"] = string.IsNullOrEmpty(mrsDj_Filter["LCCJ"]) ? "0" : mrsDj_Filter["LCCJ"].Trim(); //落锤冲击试验
                    mitem["G_QFQD"] = string.IsNullOrEmpty(mrsDj_Filter["QFQD"]) ? "0" : mrsDj_Filter["QFQD"].Trim(); //拉伸屈服强度
                    mitem["G_YYSY1"] = string.IsNullOrEmpty(mrsDj_Filter["YYSY"]) ? "" : mrsDj_Filter["YYSY"].Trim(); //静液压试验
                    mitem["G_YYSY2"] = string.IsNullOrEmpty(mrsDj_Filter["YYSY"]) ? "" : mrsDj_Filter["YYSY"].Trim(); //静液压试验
                    mitem["G_YYSY3"] = string.IsNullOrEmpty(mrsDj_Filter["YYSY"]) ? "" : mrsDj_Filter["YYSY"].Trim(); //静液压试验
                    mitem["G_YYSY4"] = string.IsNullOrEmpty(mrsDj_Filter["YYSY"]) ? "" : mrsDj_Filter["YYSY"].Trim(); //静液压试验
                    mitem["G_ZLSY"] = string.IsNullOrEmpty(mrsDj_Filter["ZLSY"]) ? "" : mrsDj_Filter["ZLSY"].Trim(); //坠落试验
                    mitem["G_JWJZ"] = string.IsNullOrEmpty(mrsDj_Filter["JWJZ"]) ? "" : mrsDj_Filter["JWJZ"].Trim(); //甲烷浸渍
                    mitem["G_HXSY"] = string.IsNullOrEmpty(mrsDj_Filter["HXSY"]) ? "" : mrsDj_Filter["HXSY"].Trim(); //烘箱试验
                    mitem["G_HGD"] = string.IsNullOrEmpty(mrsDj_Filter["HGD"]) ? "" : mrsDj_Filter["HGD"].Trim(); //环刚度
                    mitem["G_HRX"] = string.IsNullOrEmpty(mrsDj_Filter["HRX"]) ? "" : mrsDj_Filter["HRX"].Trim(); //环柔度
                    mitem["G_JZLCJ"] = string.IsNullOrEmpty(mrsDj_Filter["JZLCJ"]) ? "" : mrsDj_Filter["JZLCJ"].Trim(); //简支梁冲击
                    mJSFF = string.IsNullOrEmpty(mrsDj_Filter["JSFF"]) ? "" : mrsDj_Filter["JSFF"].Trim().ToLower();
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "依据不详";
                    mitem["JCJGMS"] = mitem["JCJGMS"] + "获取标准要求出错，找不到对应项";
                    continue;
                }
                //具体检测项目处理
                mbhggs = 0;
                mbhggs1 = 0;
                bool sign;
                int xd, Gs;
                double md1, md2, md, sum;
                double[] nArr;
                mbhggs = 0;
                string[] mtmpArray;
                mtmpArray = sitem["JCXM"].Replace(',', '、').Split('、');
                int jcxmCount; //启用的检查项目个数
                int curJcxmCount; //现在处理的是第几个检测项目
                jcxmCount = mtmpArray.Length;
                curJcxmCount = 0;

                //以下初始化报告字段
                for (xd = 0; xd <= 9; xd++)
                {
                    sitem["BGJCXM" + xd] = "";
                    sitem["BGDW" + xd] = "";
                    sitem["BGBZYQ" + xd] = "";
                    sitem["BGSCJG" + xd] = "";
                    sitem["BGDXPD" + xd] = "";
                }

                if (!string.IsNullOrEmpty(mitem["SJTABS"]))
                {
                    if (jcxm.Contains("、屈服强度、") || jcxm.Contains("、屈服应力、"))
                    {
                        mitem["QFQD_HG"] = IsQualified(mitem["G_QFQD"], mitem["QFQD"], false);
                        mbhggs = mitem["QFQD_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("屈服强度") || mtmpArray[xd].Contains("屈服应力"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGJCXM" + curJcxmCount] = sitem["BGJCXM" + curJcxmCount] + "(MPa)";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_QFQD"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["QFQD"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["QFQD_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["QFQD"] = "----";
                        mitem["QFQD_HG"] = "----";
                        mitem["G_QFQD"] = "----";
                    }
                    if (jcxm.Contains("、软化温度、") || jcxm.Contains("、维卡软化温度、"))
                    {
                        mitem["RHWD_HG"] = IsQualified(mitem["G_RHWD"], mitem["RHWD"], false);
                        mbhggs = mitem["RHWD_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        //向报告用字段赋值
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("软化温度") || mtmpArray[xd].Contains("维卡软化温度"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGJCXM" + curJcxmCount] = sitem["BGJCXM" + curJcxmCount] + "(℃)";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_RHWD"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["RHWD"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["RHWD_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["RHWD"] = "----";
                        mitem["RHWD_HG"] = "----";
                        mitem["G_RHWD"] = "----";
                    }
                    if (jcxm.Contains("、落锤冲击、") || jcxm.Contains("、冲击性能、") || jcxm.Contains("、落锤冲击试验、"))
                    {
                        if (mitem["G_LCCJ"].Contains("≤10"))
                        {
                            IDictionary<string, string> mrslccj_Sel = new Dictionary<string, string>();
                            foreach (var mrslccj_item in mrslccj)
                            {
                                if (GetSafeDouble(mrslccj_item["LCCJCS"]) == GetSafeDouble(mitem["LCCJCS"]))
                                    break;
                                mrslccj_Sel = mrslccj_item;
                            }

                            if (mrslccj_Sel == mrslccj.First() || mrslccj_Sel == mrslccj.Last())
                            {
                                md1 = GetSafeDouble(mitem["LCCJBHGS"].Trim());
                                md2 = GetSafeDouble(mitem["LCCJCS"].Trim());
                                md = 100 * md1 / md2;
                                mitem["LCCJ"] = Round(md, 0).ToString("0.0");
                                mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
                                mbhggs = mitem["LCCJ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                                mitem["LCCJ"] = mitem["LCCJ"] + "%";
                            }
                            else
                            {
                                if (GetSafeDouble(mitem["LCCJBHGS"]) <= GetSafeDouble(mrslccj_Sel["AQPHCS"]))
                                {
                                    mitem["LCCJ_HG"] = "合格";
                                    mitem["LCCJ"] = "冲击总数：" + mitem["LCCJCS"] + "；破坏数：" + mitem["LCCJBHGS"] + "；TIR值为：A(≤10%)";
                                }
                                if (GetSafeDouble(mitem["LCCJBHGS"]) >= GetSafeDouble(mrslccj_Sel["BQPHCS1"]) && GetSafeDouble(mitem["LCCJBHGS"]) <= GetSafeDouble(mrslccj_Sel["BQPHCS2"]))
                                {
                                    mitem["LCCJ_HG"] = "不判定";
                                    mitem["LCCJ"] = "根据现有冲击试样数不能作出判定";
                                    mbhggs = mbhggs + 1;
                                }
                                if (GetSafeDouble(mitem["LCCJBHGS"]) >= GetSafeDouble(mrslccj_Sel["CQPHCS"]))
                                {
                                    mitem["LCCJ_HG"] = "不合格";
                                    mitem["LCCJ"] = "冲击总数：" + mitem["LCCJCS"] + "；破坏数：" + mitem["LCCJBHGS"] + "；TIR值为：C(＞10%)";
                                    mbhggs = mbhggs + 1;
                                }
                            }
                        }
                        else
                        {
                            if (mitem["G_LCCJ"].Contains("≤"))
                            {
                                mitem["LCCJ"] = Round(100 * GetSafeDouble(mitem["LCCJBHGS"]) / GetSafeDouble(mitem["LCCJCS"]), 0).ToString("0.0");
                                mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
                                mbhggs = mitem["LCCJ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                                mitem["LCCJ"] = mitem["LCCJ"] + "%";
                            }
                            else
                            {
                                md1 = GetSafeDouble(mitem["LCCJCS"].Trim());
                                md2 = GetSafeDouble(mitem["LCCJBHGS"].Trim());
                                md1 = Round(md1, 0);
                                md2 = Round(md2, 0);
                                if (mitem["G_LCCJ"].Contains("12次冲击，12次不破裂"))
                                {
                                    mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                    if (md2 == 0)
                                        mitem["LCCJ_HG"] = "合格";
                                    else
                                    {
                                        mitem["LCCJ_HG"] = "不合格";
                                        mbhggs = mbhggs + 1;
                                    }
                                }
                                if (mitem["G_LCCJ"].Contains("10次冲击，9次不破裂"))
                                {
                                    mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                    if (md2 <= 1)
                                        mitem["LCCJ_HG"] = "合格";
                                    else
                                    {
                                        mitem["LCCJ_HG"] = "不合格";
                                        mbhggs = mbhggs + 1;
                                    }
                                }
                                if (mitem["G_LCCJ"].Contains("9/10"))
                                {
                                    mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                    if (md2 <= 1)
                                        mitem["LCCJ_HG"] = "合格";
                                    else
                                    {
                                        mitem["LCCJ_HG"] = "不合格";
                                        mbhggs = mbhggs + 1;
                                    }
                                }
                            }
                        }
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("落锤冲击") || mtmpArray[xd].Contains("冲击性能") || mtmpArray[xd].Contains("落锤冲击试验"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_LCCJ"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["LCCJ"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["LCCJ_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["LCCJ"] = "----";
                        mitem["LCCJ_HG"] = "----";
                        mitem["G_LCCJ"] = "----";
                    }
                    if (jcxm.Contains("、纵向回缩率、"))
                    {
                        mitem["ZXHSL_HG"] = IsQualified(mitem["G_ZXHSL"], mitem["ZXHSL"], false);
                        mbhggs = mitem["ZXHSL_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("纵向回缩率"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_ZXHSL"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["ZXHSL"] + "%";
                        sitem["BGDXPD" + curJcxmCount] = mitem["ZXHSL_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["ZXHSL"] = "----";
                        mitem["ZXHSL_HG"] = "----";
                        mitem["G_ZXHSL"] = "----";
                    }
                    if (jcxm.Contains("、二氯甲烷浸渍、"))
                    {
                        mbhggs = mitem["JWJZ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("二氯甲烷浸渍"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_JWJZ"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["JWJZ"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["JWJZ_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["JWJZ"] = "----";
                        mitem["JWJZ_HG"] = "----";
                        mitem["G_JWJZ"] = "----";
                    }
                    if (jcxm.Contains("、烘箱、") || jcxm.Contains("、烘箱试验、"))
                    {
                        mbhggs = mitem["HXSY_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("烘箱") || mtmpArray[xd].Contains("烘箱试验"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_HXSY"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["HXSY"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["HXSY_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["HXSY"] = "----";
                        mitem["HXSY_HG"] = "----";
                        mitem["G_HXSY"] = "----";
                    }
                    if (jcxm.Contains("、扁平试验、"))
                    {
                        mbhggs = mitem["BPSY_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("扁平试验"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_BPSY"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["BPSY"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["BPSY_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["BPSY"] = "----";
                        mitem["BPSY_HG"] = "----";
                        mitem["G_BPSY"] = "----";
                    }
                    if (jcxm.Contains("、环柔、") || jcxm.Contains("、环柔性、"))
                    {
                        mbhggs = mitem["HRX_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        //向报告用字段赋值
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("环柔") || mtmpArray[xd].Contains("环柔性"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_HRX"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["HRX"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["HRX_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["HRX"] = "";
                        mitem["HRX_HG"] = "----";
                        mitem["G_HRX"] = "";
                    }
                    if (jcxm.Contains("、坠落、"))
                    {
                        mbhggs = mitem["ZLSY_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("坠落"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_ZLSY"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["ZLSY"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["ZLSY_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["ZLSY"] = "";
                        mitem["ZLSY_HG"] = "----";
                        mitem["G_ZLSY"] = "";
                    }
                    if (jcxm.Contains("、环刚、") || jcxm.Contains("、环刚度、"))
                    {
                        mitem["HGD_HG"] = IsQualified(mitem["G_HGD"], mitem["HGD"], false);
                        mbhggs = mitem["HGD_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("环刚") || mtmpArray[xd].Contains("环刚度"))
                            {
                                if (sitem["SJDJ"] == "埋地用聚乙烯(PE)双壁波纹管材" || sitem["SJDJ"] == "埋地用聚乙烯(PE)缠绕结构壁管材")
                                    sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd] + "(kN/㎡)";
                                else
                                    sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "kN/㎡";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_HGD"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["HGD"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["HGD_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["HGD"] = "----";
                        mitem["HGD_HG"] = "----";
                        mitem["G_HGD"] = "----";
                    }
                    if (curJcxmCount < 9)
                        sitem["BGBZYQ" + curJcxmCount] = "以下空白";
                    if (mbhggs == 0)
                    {
                        mitem["JCJGMS"] = "该组试件所检项目符合" + mitem["PDBZ"] + "标准要求。";
                        sitem["JCJG"] = "合格";
                    }
                    if (mbhggs >= 1)
                    {
                        mitem["JCJGMS"] = "该组试件不符合" + mitem["PDBZ"] + "标准要求。";
                        sitem["JCJG"] = "不合格";
                    }
                    mAllHg = (mAllHg && sitem["JCJG"].Trim() == "合格");
                }
                else
                {

                    if (jcxm.Contains("、外观颜色、"))
                    {
                        //
                        //MItem[0]["WG"] = "";
                        MItem[0]["G_WG"] = "管材内外表面应清洁、光滑, 不应有气泡、明显的划伤、凹陷、杂质、颜色不均等缺陷。 管材两端应切割平整, 并与管材轴线垂直。";
                        //MItem[0]["WG_HG"] = "";

                        //MItem[0]["BZ"] = "符合";
                        MItem[0]["G_BZ"] = "管材应为黑色或蓝色, 黑色管材上应共挤出 至少三条蓝色条, 色条应沿管材圆周方向均匀分布。蓝色管材仅用于暗敷。";
                        //MItem[0]["BZ_HG"] = "合格";

                        if (MItem[0]["WG_HG"] == "合格" && MItem[0]["BZ_HG"] == "合格")
                        {
                        }
                        else
                        {
                            mbhggs = mbhggs + 1;
                            mAllHg = false;
                        }

                        for (xd = 0; xd < jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("外观颜色"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = "外观";
                                sitem["BGDW" + curJcxmCount] = "----";
                                sitem["BGBZYQ" + curJcxmCount] = mitem["G_WG"];
                                sitem["BGSCJG" + curJcxmCount] = mitem["WG"];
                                sitem["BGDXPD" + curJcxmCount] = mitem["WG_HG"];
                                curJcxmCount = curJcxmCount + 1;

                                sitem["BGJCXM" + curJcxmCount] = "颜色";
                                sitem["BGDW" + curJcxmCount] = "----";
                                sitem["BGBZYQ" + curJcxmCount] = mitem["G_BZ"];
                                sitem["BGSCJG" + curJcxmCount] = mitem["BZ"];
                                sitem["BGDXPD" + curJcxmCount] = mitem["BZ_HG"];
                                curJcxmCount = curJcxmCount + 1;
                                break;
                            }
                        }

                    }
                    else
                    {
                        MItem[0]["WG_HG"] = "----";
                        MItem[0]["G_WG"] = "----";
                        MItem[0]["G_BZ"] = "----";
                        MItem[0]["BZ_HG"] = "----";
                    }

                    if (jcxm.Contains("、规格尺寸、"))
                    {
                        //测试的数量4-12个
                        //1 长度 2.平均外径 3.不圆度 4.壁厚公差
                        //int count = Convert.ToInt32(sitem["ZHCLSL"]);

                        //长度
                        //MItem[0]["G_GCCD"] = "长度一般为6m 、9m、12m,也可由供需双方商定。";
                        //MItem[0]["HG_GCCD"] = "";
                        //MItem[0]["HG_GCCD"] = MItem[0]["HG_GCCD"];


                        //MItem[0]["HG_ZGBYD"] = "依据不详";
                        //MItem[0]["PJWJ_HG"] = "依据不详";
                        //MItem[0]["HG_GCBH"] = "依据不详";
                        var ddf = MItem[0]["HG_GCBH"];
                        ddf = MItem[0]["PJWJ_HG"];
                        //ddf = MItem[0]["HG_ZGBYD"];
                        MItem[0]["HG_GCBH"] = MItem[0]["HG_GCBH"];
                        //MItem[0]["HG_ZGBYD"] = MItem[0]["HG_ZGBYD"];
                        MItem[0]["PJWJ_HG"] = MItem[0]["PJWJ_HG"];

                        for (xd = 0; xd < jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("规格尺寸"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = "平均外径";
                                sitem["BGDW" + curJcxmCount] = "----";
                                sitem["BGSCJG" + curJcxmCount] = MItem[0]["PJWJ"];
                                sitem["BGBZYQ" + curJcxmCount] = MItem[0]["G_PJWJ"];
                                sitem["BGDXPD" + curJcxmCount] = MItem[0]["PJWJ_HG"];
                                curJcxmCount = curJcxmCount + 1;

                                sitem["BGJCXM" + curJcxmCount] = "壁厚";
                                sitem["BGDW" + curJcxmCount] = "----";
                                sitem["BGSCJG" + curJcxmCount] = sitem["GCBH"];
                                sitem["BGBZYQ" + curJcxmCount] = MItem[0]["G_GCBH"];
                                sitem["BGDXPD" + curJcxmCount] = MItem[0]["HG_GCBH"];
                                curJcxmCount = curJcxmCount + 1;

                                break;
                            }
                        }

                    }
                    if (jcxm.Contains("、环刚、") || jcxm.Contains("、环刚度、"))
                    {
                        double Yi, S1, S2, S3 = 0;
                        Yi = GetSafeDouble(sitem["GCWJ"]) * 0.03 / 1000;
                        if ((GetSafeDouble(MItem[0]["HGD_LI1"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI2"]) * Yi != 0) && (GetSafeDouble(MItem[0]["HGD_LI2"]) * Yi != 0))
                        {
                            S1 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI1"]) / (GetSafeDouble(MItem[0]["HGD_LI1"]) * Yi);
                            S2 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI2"]) / (GetSafeDouble(MItem[0]["HGD_LI2"]) * Yi);
                            S3 = (0.0186 + 0.025 * 0.03) * GetSafeDouble(MItem[0]["HGD_FI3"]) / (GetSafeDouble(MItem[0]["HGD_LI3"]) * Yi);
                            MItem[0]["HGD"] = ((S1 + S2 + S3) / 3).ToString("0.0");
                        }

                        MItem[0]["HGD_HG"] = IsQualified(MItem[0]["G_HGD"], MItem[0]["HGD"]);

                        if (MItem[0]["HGD_HG"] == "不合格")
                        {
                            mbhggs1 = mbhggs1 + 1;
                        }
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("环刚") || mtmpArray[xd].Contains("环刚度"))
                            {
                                if (sitem["SJDJ"] == "埋地用聚乙烯(PE)双壁波纹管材" || sitem["SJDJ"] == "埋地用聚乙烯(PE)缠绕结构壁管材")
                                    sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd] + "(kN/㎡)";
                                else
                                    sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "kN/㎡";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_HGD"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["HGD"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["HGD_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["HGD"] = "0";
                        mitem["HGD_HG"] = "----";
                        mitem["G_HGD"] = "0";
                    }
                    if (jcxm.Contains("、软化温度、") || jcxm.Contains("、维卡软化温度、"))
                    {
                        mitem["RHWD_HG"] = IsQualified(mitem["G_RHWD"], mitem["RHWD"], false);
                        mbhggs = mitem["RHWD_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        //向报告用字段赋值
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("软化温度") || mtmpArray[xd].Contains("维卡软化温度"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGJCXM" + curJcxmCount] = sitem["BGJCXM" + curJcxmCount] + "(℃)";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_RHWD"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["RHWD"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["RHWD_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["RHWD"] = "0";
                        mitem["RHWD_HG"] = "----";
                        mitem["G_RHWD"] = "0";
                    }
                    if (jcxm.Contains("、落锤冲击、") || jcxm.Contains("、落锤冲击试验、"))
                    {
                        if (mitem["G_LCCJ"].Contains("≤10"))
                        {
                            IDictionary<string, string> mrslccj_Sel = new Dictionary<string, string>();
                            foreach (var mrslccj_item in mrslccj)
                            {
                                if (GetSafeDouble(mrslccj_item["LCCJCS"]) == GetSafeDouble(mitem["LCCJCS"]))
                                    break;
                                mrslccj_Sel = mrslccj_item;
                            }

                            if (mrslccj_Sel == mrslccj.First() || mrslccj_Sel == mrslccj.Last())
                            {
                                md1 = GetSafeDouble(mitem["LCCJBHGS"]);
                                md2 = GetSafeDouble(mitem["LCCJCS"]); ;
                                md = md2 == 0 ? 0 : (100 * md1 / md2);
                                mitem["LCCJ"] = Round(md, 0).ToString("0.0");
                                mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
                                mbhggs = mitem["LCCJ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                                mitem["LCCJ"] = mitem["LCCJ"] + "%";
                            }
                            else
                            {
                                if (GetSafeDouble(mitem["LCCJBHGS"]) <= GetSafeDouble(mrslccj_Sel["AQPHCS"]))
                                {
                                    mitem["LCCJ_HG"] = "合格";
                                    mitem["LCCJ"] = "冲击总数：" + mitem["LCCJCS"] + "；破坏数：" + mitem["LCCJBHGS"] + "；TIR值为：A(≤10%)";
                                }
                                if (GetSafeDouble(mitem["LCCJBHGS"]) >= GetSafeDouble(mrslccj_Sel["BQPHCS1"]) && GetSafeDouble(mitem["LCCJBHGS"]) <= GetSafeDouble(mrslccj_Sel["BQPHCS2"]))
                                {
                                    mitem["LCCJ_HG"] = "不判定";
                                    mitem["LCCJ"] = "根据现有冲击试样数不能作出判定";
                                    mbhggs = mbhggs + 1;
                                }
                                if (GetSafeDouble(mitem["LCCJBHGS"]) >= GetSafeDouble(mrslccj_Sel["CQPHCS"]))
                                {
                                    mitem["LCCJ_HG"] = "不合格";
                                    mitem["LCCJ"] = "冲击总数：" + mitem["LCCJCS"] + "；破坏数：" + mitem["LCCJBHGS"] + "；TIR值为：C(＞10%)";
                                    mbhggs = mbhggs + 1;
                                }
                            }
                        }
                        else
                        {
                            if (mitem["G_LCCJ"].Contains("≤"))
                            {
                                mitem["LCCJ"] = GetSafeDouble(mitem["LCCJCS"]) == 0 ? "0" : Round(100 * GetSafeDouble(mitem["LCCJBHGS"]) / GetSafeDouble(mitem["LCCJCS"]), 0).ToString("0.0");
                                mitem["LCCJ_HG"] = IsQualified(mitem["G_LCCJ"], mitem["LCCJ"], false);
                                mbhggs = mitem["LCCJ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                                mitem["LCCJ"] = mitem["LCCJ"] + "%";
                            }
                            else
                            {
                                md1 = GetSafeDouble(mitem["LCCJCS"].Trim());
                                md2 = GetSafeDouble(mitem["LCCJBHGS"].Trim());
                                md1 = Round(md1, 0);
                                md2 = Round(md2, 0);
                                if (mitem["G_LCCJ"].Contains("12次冲击，12次不破裂"))
                                {
                                    mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                    if (md2 == 0)
                                        mitem["LCCJ_HG"] = "合格";
                                    else
                                    {
                                        mitem["LCCJ_HG"] = "不合格";
                                        mbhggs = mbhggs + 1;
                                    }
                                }
                                if (mitem["G_LCCJ"].Contains("10次冲击，9次不破裂"))
                                {
                                    mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                    if (md2 <= 1)
                                        mitem["LCCJ_HG"] = "合格";
                                    else
                                    {
                                        mitem["LCCJ_HG"] = "不合格";
                                        mbhggs = mbhggs + 1;
                                    }
                                }
                                if (mitem["G_LCCJ"].Contains("9/10"))
                                {
                                    mitem["LCCJ"] = md1 + "次冲击，" + (md1 - md2) + "次不破裂";
                                    if (md2 <= 1)
                                        mitem["LCCJ_HG"] = "合格";
                                    else
                                    {
                                        mitem["LCCJ_HG"] = "不合格";
                                        mbhggs = mbhggs + 1;
                                    }
                                }
                            }
                        }
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("落锤冲击") || mtmpArray[xd].Contains("冲击性能") || mtmpArray[xd].Contains("落锤冲击试验"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_LCCJ"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["LCCJ"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["LCCJ_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["LCCJ"] = "----";
                        mitem["LCCJ_HG"] = "----";
                        mitem["G_LCCJ"] = "----";
                    }
                    if (jcxm.Contains("、烘箱、") || jcxm.Contains("、烘箱试验、"))
                    {
                        mbhggs = mitem["HXSY_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("烘箱") || mtmpArray[xd].Contains("烘箱试验"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_HXSY"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["HXSY"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["HXSY_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["HXSY"] = "----";
                        mitem["HXSY_HG"] = "----";
                        mitem["G_HXSY"] = "----";
                    }
                    if (jcxm.Contains("、环柔、") || jcxm.Contains("、环柔性、"))
                    {
                        mbhggs = mitem["HRX_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        //向报告用字段赋值
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("环柔") || mtmpArray[xd].Contains("环柔性"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_HRX"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["HRX"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["HRX_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["HRX"] = "";
                        mitem["HRX_HG"] = "----";
                        mitem["G_HRX"] = "";
                    }
                    if (jcxm.Contains("、二氯甲烷浸渍、"))
                    {
                        mbhggs = mitem["JWJZ_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("二氯甲烷浸渍"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_JWJZ"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["JWJZ"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["JWJZ_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["JWJZ"] = "----";
                        mitem["JWJZ_HG"] = "----";
                        mitem["G_JWJZ"] = "----";
                    }
                    if (jcxm.Contains("、扁平试验、"))
                    {
                        mbhggs = mitem["BPSY_HG"] == "不合格" ? mbhggs + 1 : mbhggs;
                        for (xd = 0; xd <= jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("扁平试验"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_BPSY"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["BPSY"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["BPSY_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["BPSY"] = "----";
                        mitem["BPSY_HG"] = "----";
                        mitem["G_BPSY"] = "----";
                    }
                    if (jcxm.Contains("、纵向回缩率、"))
                    {
                        double zxhsl1, zxhsl2, zxhsl3 = 0.0;
                        if ((GetSafeDouble(MItem[0]["HSLL0_1"]) == 0 || GetSafeDouble(MItem[0]["HSLL0_2"]) == 0 || GetSafeDouble(MItem[0]["HSLL0_3"]) == 0))
                        {
                            MItem[0]["ZXHSL"] = "0.0%";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(MItem[0]["HSLL0_1"]))
                            {
                                zxhsl1 = 0;
                            }
                            else
                            {
                                zxhsl1 = Math.Abs(Double.Parse((100 * (GetSafeDouble(MItem[0]["HSLL0_1"]) - GetSafeDouble(MItem[0]["HSLLI_1"])) / GetSafeDouble(MItem[0]["HSLL0_1"])).ToString("0.00")));
                            }
                            if (string.IsNullOrEmpty(MItem[0]["HSLL0_2"]))
                            {
                                zxhsl2 = 0;
                            }
                            else
                            {
                                zxhsl2 = Math.Abs(Double.Parse((100 * (GetSafeDouble(MItem[0]["HSLL0_2"]) - GetSafeDouble(MItem[0]["HSLLI_2"])) / GetSafeDouble(MItem[0]["HSLL0_2"])).ToString("0.00")));
                            }
                            if (string.IsNullOrEmpty(MItem[0]["HSLL0_3"]))
                            {
                                zxhsl3 = 0;
                            }
                            else
                            {
                                zxhsl3 = Math.Abs(Double.Parse((100 * (GetSafeDouble(MItem[0]["HSLL0_3"]) - GetSafeDouble(MItem[0]["HSLLI_3"])) / GetSafeDouble(MItem[0]["HSLL0_3"])).ToString("0.00")));
                            }
                            MItem[0]["ZXHSL"] = ((zxhsl1 + zxhsl2 + zxhsl3) / 3).ToString("0.0") + "%";

                        }
                        mitem["ZXHSL_HG"] = IsQualified(mitem["G_ZXHSL"], mitem["ZXHSL"], false);
                        if (MItem[0]["ZXHSL_HG"] == "合格")
                        {
                        }
                        else
                        {
                            mbhggs1 = mbhggs1 + 1;
                        }
                        //  '向报告用字段赋值
                        for (xd = 0; xd < jcxmCount; xd++)
                        {
                            if (mtmpArray[xd].Contains("纵向回缩率"))
                            {
                                sitem["BGJCXM" + curJcxmCount] = mtmpArray[xd];
                                break;
                            }
                        }
                        sitem["BGDW" + curJcxmCount] = "----";
                        sitem["BGBZYQ" + curJcxmCount] = mitem["G_ZXHSL"];
                        sitem["BGSCJG" + curJcxmCount] = mitem["ZXHSL"];
                        sitem["BGDXPD" + curJcxmCount] = mitem["ZXHSL_HG"];
                        curJcxmCount = curJcxmCount + 1;
                    }
                    else
                    {
                        mitem["ZXHSL"] = "0";
                        mitem["ZXHSL_HG"] = "----";
                        mitem["G_ZXHSL"] = "0";
                    }
                }

                if (curJcxmCount < 9)
                    sitem["BGBZYQ" + curJcxmCount] = "以下空白";
                if (mbhggs == 0)
                {
                    mitem["JCJGMS"] = "该组试件所检项目符合" + mitem["PDBZ"] + "标准要求。";
                    sitem["JCJG"] = "合格";
                }
                if (mbhggs >= 1)
                {
                    mitem["JCJGMS"] = "该组试件不符合" + mitem["PDBZ"] + "标准要求。";
                    sitem["JCJG"] = "不合格";
                }
                mAllHg = (mAllHg && sitem["JCJG"].Trim() == "合格");
            }
            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "该组试样所检项目符合" + mitem["PDBZ"] + "标准要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "该组试样不符合" + mitem["PDBZ"] + "标准要求。";
            }
            #endregion
            #endregion
        }
    }
}