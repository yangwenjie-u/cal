using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
namespace Calculates
{
    public class MXE : BaseMethods
    {
        public void Calc()
        {
            #region  参数定义
            var data = retData;
            var mrsDj = dataExtra["BZ_MXE_DJ"];
            var mrsCtDj=dataExtra["BZ_CT_DJ"];
            var MItem = data["M_MXE"];
            var mitem = MItem[0];
            var SItem = data["S_MXE"];
            int mbhggs = 0;
            string mJSFF;
            bool mAllHg = true;
            bool mFlag_Hg = false, mFlag_Bhg = false;
            var mSjdj = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            mitem["JCJGMS"] = "";

            decimal pjmd, sum, md1, md2, md = 0;
            int Gs = 0;
            mFlag_Hg = false;
            mFlag_Bhg = false;
            bool sign;
            bool flag = false;
            List<double> arr = new List<double>();
            #endregion
            foreach (var sitem in SItem)
            {
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

                sitem["MDBS"] = "马歇尔标准密度";
                #region 等级表取值
                //取等级表，WDDMS:稳定度MS,LZFL:流值FL,KXLVV:空隙率VV
                var mrsDj_item = mrsDj.FirstOrDefault(x => x["DLLX"].Contains(sitem["DLLX"]) && x["JTLX"].Contains(sitem["JTLX"])
                      && x["KXLSD"].Contains(sitem["KXLSD"]) && x["QHFQ"].Contains(sitem["QHFQ"]));
                if (mrsDj_item != null||mrsDj_item.Count()!=0)
                {
                    sitem["G_WDD"] = mrsDj_item["WDDMS"];
                    sitem["G_SJLZ"] = mrsDj_item["LZFL"];
                    sitem["G_KSL"] = mrsDj_item["KXLVV"];
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "不下判定";
                    mitem["JCJGMS"] = "找不到对应的等级";
                    continue;
                }

                //先更据KLJPLX:矿料级配类型,查出GCZDLJ:公称最大粒径(mm)
                var mrsCtDj_item = mrsCtDj.FirstOrDefault(x => x["KLJPLX"].Contains(sitem["KLJPLX"]));
                if (mrsCtDj_item != null|| mrsCtDj_item.Count()!=0)
                {
                    //更据GCZDLJ和SJKXL:设计空隙率(%),查出G_KLJXL：矿料间隙率(%)，LQBHDVFA：沥青饱和度VFA(%)
                    var mrsD_item2 = mrsDj.FirstOrDefault(x => x["GCZDLJ"].Contains(mrsCtDj_item["GCZDLJ"]) && x["SJKXL"].Contains(sitem["SJKXL"]));
                    if (mrsD_item2 != null||mrsD_item2.Count()!=0)
                    {
                        sitem["G_KLJXL"] = mrsD_item2["JXLVMA"];
                        sitem["G_KLBHD"] = mrsD_item2["LQBHDVFA"];
                    }
                    else
                    {
                        mJSFF = "";
                        sitem["JCJG"] = "不下判定";
                        mitem["JCJGMS"] = "找不到对应的等级";
                        continue;
                    }
                }
                else
                {
                    mJSFF = "";
                    sitem["JCJG"] = "不下判定";
                    mitem["JCJGMS"] = "找不到对应的等级";
                    continue;
                }

                //从设计等级表中取得相应的计算数值、等级标准
                //var mrsDj_item = mrsDj.FirstOrDefault(x => x["MC"].Contains(dCpmc) && x["LX"].Contains(dLx) && x["DJ"].Contains(dDj) && x["ZF"].Contains(dZf) && x["BZH"].Contains(dBzh));
                //if (mrsDj_item != null && mrsDj_item.Count() > 0)
                //{

                //}
                //else
                //{
                //    mJSFF = "";
                //    sitem["JCJG"] = "依据不详";
                //    mitem["JCJGMS"] = "找不到对应的等级";
                //}
                //25℃时水的密度
                #endregion


                decimal S25md = (decimal)0.9971;
                #region ZBDH:最大相对密度
                if (jcxm.Contains("、最大相对密度、"))
                {
                    sitem["LRZDMD_RQF"] = "";
                    sitem["LRZDMD_ZKF"] = "";
                    jcxmCur = "理论最大相对密度";
                    //真空发（A类）
                    //相对密度
                    md = 0;
                    #region LRMDSYFF:真空法
                    if (mitem["LRMDSYFF"] == "真空法")
                    {
                        //Ma 空气中净质量
                        if (sitem["LRMDRQLX"] == "A类容器")  //真空法容器类型
                        {
                            for (int i = 1; i < 3; i++)
                            {
                                md1 = GetSafeDecimal(sitem["M2_" + i], 3) - GetSafeDecimal(sitem["M1_" + i], 3);
                                sitem["LRZDMD_ZKF" + i] = Math.Round((S25md * GetSafeDecimal(sitem["Ma" + i], 3) / (GetSafeDecimal(sitem["Ma" + i], 3) - md1)), 3).ToString();
                                md += GetSafeDecimal(sitem["LRZDMD_ZKF" + i]);
                            }
                        }
                        else if (sitem["LRMDRQLX"] == "B类容器" || sitem["LRMDRQLX"] == "C类容器")
                        {
                            for (int i = 1; i < 3; i++)
                            {
                                //B/C类
                                md1 = GetSafeDecimal(sitem["Mb_" + i], 3) - GetSafeDecimal(sitem["Mc_" + i], 3);
                                sitem["LRZDMD_ZKF" + i] = Math.Round((S25md * GetSafeDecimal(sitem["Ma" + i], 3) / (GetSafeDecimal(sitem["Ma" + i], 3) + md1))).ToString("0.000");
                                md += GetSafeDecimal(sitem["LRZDMD_ZKF" + i]);
                            }
                        }
                        else
                        {
                            throw new Exception("请选择负压容器类型");
                        }
                        pjmd = Math.Round(md / 2, 3);

                        sitem["LRZDMD_ZKF"] = pjmd.ToString();
                    }
                    #endregion
                    #region LRMDSYFF:溶剂法
                    else if (mitem["LRMDSYFF"] == "溶剂法")
                    {
                        decimal md3 = 0;
                        //三氯乙烯溶液对水的相对密度
                        decimal xdmd = (decimal)1.4642;
                        for (int i = 1; i < 3; i++)
                        {
                            //瓶加混合料质量-瓶质量
                            md1 = (GetSafeDecimal(sitem["MRJ_PRJZL" + i], 3) - GetSafeDecimal(sitem["MRJ_PZL" + i], 3));
                            //总质量-瓶质量
                            md2 = (GetSafeDecimal(sitem["MRJ_ZZL" + i], 3) - GetSafeDecimal(sitem["MRJ_PLZL" + i], 3));
                            //净质量
                            md3 = GetSafeDecimal(sitem["MRJ_PLZL" + i], 3) - GetSafeDecimal(sitem["MRJ_PZL" + i], 3);

                            md += md3 / ((md1 - md2) / xdmd);
                            sitem["LRZDMD_RQF" + i] = Math.Round(md).ToString();
                        }
                        pjmd = Math.Round(md / 2, 3);

                        sitem["LRZDMD_RQF"] = pjmd.ToString();
                    }
                    #endregion
                    #region 没有选择试验方法
                    else
                    {
                        throw new Exception("请选择理论最大密度试验方法");
                    }
                    if (!string.IsNullOrEmpty(sitem["LRZDMD_ZKF"]) && IsNumeric(sitem["LRZDMD_ZKF"]))
                    {
                        sitem["LRMD"] = sitem["LRZDMD_ZKF"];
                    }
                    if (!string.IsNullOrEmpty(sitem["LRZDMD_RQF"]) && IsNumeric(sitem["LRZDMD_RQF"]))
                    {
                        sitem["LRMD"] = sitem["LRZDMD_RQF"];
                    }
                    #endregion
                }
                #endregion
                //代表值
                decimal dbz = 0;
                #region ZBDH :马歇尔稳定度
                if (jcxm.Contains("、马歇尔稳定度、"))
                {
                    jcxmCur = "马歇尔稳定度";
                    Gs = 1;
                    sum = 0;
                    md = 0;
                    flag = true;
                    List<decimal> listGD = new List<decimal>();
                    //统计一组试件的数量，最少不得少于4个
                    for (int i = 1; i < 6; i++)
                    {
                        if (IsNumeric(sitem["ZJ1_" + i]))
                        {
                            Gs++;
                        }
                    }
                    if (Gs < 4)
                    {
                        throw new Exception("马歇尔标准密度要求数量不小于4个");
                    }


                    for (int i = 1; i < Gs + 1; i++)
                    {
                        flag = true;
                        listGD.Clear();
                        if (!IsNumeric(sitem["ZJ1_" + i]) || !IsNumeric(sitem["ZJ2_" + i]))
                        {
                            throw new Exception("请输入正确的试件直径！");
                        }
                        if (!IsNumeric(sitem["SJGD" + i + "_1"]) || !IsNumeric(sitem["SJGD" + i + "_2"]) || !IsNumeric(sitem["SJGD" + i + "_3"]) || !IsNumeric(sitem["SJGD" + i + "_4"]))
                        {
                            throw new Exception("请输入正确的试件高度！");
                        }
                        //平均直径
                        md1 = Math.Round((GetSafeDecimal(sitem["ZJ1_" + i]) + GetSafeDecimal(sitem["ZJ2_" + i])) / 2, 1);
                        sitem["PJZJ" + i] = md1.ToString();
                        md += md1;

                        //平均高度
                        md2 = Math.Round((GetSafeDecimal(sitem["SJGD" + i + "_1"]) + GetSafeDecimal(sitem["SJGD" + i + "_2"]) + GetSafeDecimal(sitem["SJGD" + i + "_3"]) + GetSafeDecimal(sitem["SJGD" + i + "_4"])) / 4, 1);
                        sitem["SJGD" + i] = md2.ToString();
                        //高度范围
                        flag = IsQualified("62.2～64.8", md2.ToString()) == "合格" ? flag : false;
                        if (!flag)
                        {
                            flag = IsQualified("92.8～97.8", md2.ToString()) == "合格" ? flag : false;
                        }
                        if (!flag)
                        {
                            throw new Exception("试件" + i + "高度不符合63.5mm±1.3mm或95.3mm±2.5mm要求，此试件作废。");
                        }
                        listGD.Add(GetSafeDecimal(sitem["SJGD" + i + "_1"]));
                        listGD.Add(GetSafeDecimal(sitem["SJGD" + i + "_2"]));
                        listGD.Add(GetSafeDecimal(sitem["SJGD" + i + "_3"]));
                        listGD.Add(GetSafeDecimal(sitem["SJGD" + i + "_4"]));
                        listGD.Sort();

                        if (Math.Abs(listGD[0] - listGD[3]) > 2)
                        {
                            throw new Exception("试件" + i + "两侧高度偏差大于2mm，此试件作废。");
                        }
                        sum += md2;
                    }
                    
                    
                    sitem["ZJDBZ"] = Math.Round(md / Gs, 1).ToString();
                    sitem["SJGDDBZ"] = Math.Round(sum / Gs, 1).ToString();

                    //空隙率
                    decimal kxl = 0;
                    List<decimal> kxlList = new List<decimal>();
                    //间隙率
                    decimal jxl = 0;
                    List<decimal> jxlList = new List<decimal>();
                    //饱和率
                    decimal bhd = 0;
                    //稳定度
                    decimal wdd = 0;
                    List<decimal> wddList = new List<decimal>();
                    //流值(0.1mm)
                    decimal sjlz = 0;
                    List<decimal> sjlzList = new List<decimal>();

                    List<decimal> bhdList = new List<decimal>();
                    //沥青体积百分率
                    decimal lqtjbfl = 0;
                    List<decimal> lqtjbflList = new List<decimal>();
                    //理论最大密度
                    decimal llmd = 0;

                    if (string.IsNullOrEmpty(sitem["LRMD"]))
                    {
                        throw new Exception("获取不到理论最大相对密度");
                    }
                    //理论密度
                    llmd = GetSafeDecimal(sitem["LRMD"]);
                    //毛体积相对密度
                    decimal mtjxdmd = 0;
                    //合成矿料相对密度 ==（100-沥青用量）/（（100/理论最大密度）-（沥青用量/沥青相对密度））
                    decimal hcmtjxdmd = 0;
                    hcmtjxdmd = Math.Round((100 - GetSafeDecimal(mitem["W_LQHL"], 2)) / (Math.Round((100 / llmd), 3) - (GetSafeDecimal(mitem["W_LQHL"], 2) / GetSafeDecimal(mitem["LQXDMD"], 2))), 3);
                    
                    switch(sitem["MXEMDFF"]){
                        
                        case "表干法":

                            for (int i = 1; i < 7; i++)
                            {
                                #region 参数检查：是否为数字 
                                //试件的空中质量(g)
                                if (!IsNumeric(sitem["SJKZZL" + i]))
                                {
                                    throw new Exception("请输入正确的试件的空中质量" + i);
                                }
                                //试件的水中质量(g)
                                if (!IsNumeric(sitem["SJSZZL" + i]))
                                {
                                    throw new Exception("请输入正确的 试件的水中质量" + i);
                                }
                                //试件的表干质量(g)
                                if (!IsNumeric(sitem["SJBGZL" + i]))
                                {
                                    throw new Exception("请输入正确的 试件的表干质量" + i);
                                }
                                #endregion
                                
                                //表干-空中质量
                                md1 = GetSafeDecimal(sitem["SJBGZL" + i]) - GetSafeDecimal(sitem["SJKZZL" + i]);
                                //表干-水中质量
                                md2 = GetSafeDecimal(sitem["SJBGZL" + i]) - GetSafeDecimal(sitem["SJSZZL" + i]);
                                //吸水率
                                md = Math.Round(md1 / md2 * 100, 1);
                                sitem["XSL" + i] = md.ToString();

                                 //毛体积相对密度  ==空中质量/（表干-水中质量）
                                mtjxdmd = Math.Round(GetSafeDecimal(sitem["SJKZZL" + i]) / md2, 3);

                                //毛体积密度  ==毛体积相对密度* 水的密度（0.9971）
                                //sitem["MXEMD" + i] = mtjxdmd.ToString();

                                //
                                sitem["MXEMD" + i] = Math.Round(mtjxdmd * S25md, 3).ToString();
                                //理论最大相对密度

                                //llmd = (decimal)1.02;

                                //空隙率  ==（1-(毛体积相对密度/混合料理论最大密度)）*100
                                kxl = Math.Round((1 - mtjxdmd / llmd) * 100, 1);
                                sitem["MXEMD" + i] = kxl.ToString();
                                kxlList.Add(kxl);
                                //矿料间隙率  ==（1-（毛体积相对密度/最大理论密度）*（100-沥青含量）/100  ）*100
                                //毛体积相对密度/最大理论密度
                                jxl = Math.Round((1 - (mtjxdmd / hcmtjxdmd) * (100 - GetSafeDecimal(mitem["W_LQHL"])) / 100) * 100, 1);
                                sitem["KLJXL" + i] = jxl.ToString();
                                jxlList.Add(jxl);

                                // 沥青饱和度 ==（间隙率-空隙率）/空隙率*100
                                bhd = Math.Round((jxl - kxl) / jxl * 100, 1);
                                sitem["KLBHD" + i] = bhd.ToString();
                                bhdList.Add(bhd);
                                //稳定度（KN）
                                wdd = Convert.ToDecimal(sitem["SCWDD" + i]);
                                wddList.Add(wdd);
                                //流值
                                sjlz = Convert.ToDecimal(sitem["SJLZ" + i]);
                                sjlzList.Add(sjlz);



                                // Pa  W_LQHL  沥青含量（石油比）

                                // 被吸收的沥青质量占总质量的百分比 == ((合成矿料相对密度- 理论密度)/(合成矿料相对密度* 理论密度))*25度沥青相对密度*100
                                md1 = Math.Round(((hcmtjxdmd - llmd) / (hcmtjxdmd * llmd)) * GetSafeDecimal(mitem["LQXDMD"], 2) * 100, 3);
                                //有效沥青含量
                                md2 = GetSafeDecimal(mitem["W_LQHL"]);
                                //md1 = Math.Round(((hcmtjxdmd - llmd) / (hcmtjxdmd * llmd)) * GetSafeDecimal(mitem["LQXDMD"], 2) * 100, 3);

                                // 有效沥青百分率==毛体积相对密度*有效沥青含量/25度沥青相对密度
                                lqtjbfl = Math.Round(mtjxdmd * md2 / GetSafeDecimal(mitem["LQXDMD"], 2), 1);
                                sitem["LQTJBFL" + i] = lqtjbfl.ToString();
                                lqtjbflList.Add(lqtjbfl);
                            }
                    
                            //取7组代表平均值：1.空隙率、2.间隙率、3.饱和度，
                            sitem["W_KSL"] =Convert.ToString(kxlList.Average());
                            sitem["KLJXL"] = Convert.ToString(jxlList.Average());
                            sitem["KLBHD"] = Convert.ToString(bhdList.Average());
                            sitem["W_WDD"] = Convert.ToString(wddList.Average());
                            sitem["W_SJLZ"] = Convert.ToString(sjlzList.Average());
                            if (IsQualified(sitem["G_KSL"],sitem["W_KSL"],false)=="不合格"||
                                IsQualified(sitem["G_KLJXL"], sitem["KLJXL"], false) == "不合格" ||
                                IsQualified(sitem["G_KLBHD"], sitem["KLBHD"], false) == "不合格" ||
                                IsQualified(sitem["G_WDD"], sitem["W_WDD"], false) == "不合格" ||
                                IsQualified(sitem["G_SJLZ"], sitem["W_SJLZ"], false) == "不合格" 
                                )
                            {
                                mbhggs = mbhggs + 1;
                                sitem["MXEWDDSYDZPD"] = "不合格";
                            }
                            else
                            {
                                sitem["MXEWDDSYDZPD"] = "合格";
                            }
                            break;
                        
                        case "水中重法":

                            break;
                    }






                    Gs = 1;
                    sum = 0;
                    md = 0;
                    //统计一组试件的数量，最少不得少于4个
                    for (int i = 1; i < 6; i++)
                    {
                        if (IsNumeric(sitem["SCWDD" + i]))
                        {
                            Gs++;
                        }
                    }
                    if (Gs < 4)
                    {
                        throw new Exception("马歇尔稳定度一组试件数量不小于4个");
                    }
                    for (int i = 1; i < Gs + 1; i++)
                    {
                        //foreach (var item in mrsDj)
                        //{
                        //    if (IsQualified(item["SJGD"], sitem["SJGD" + i], true) == "符合")
                        //    {
                        //        //高度修正系数
                        //        md1 = GetSafeDecimal(item["XZXS"]);
                        //        break;
                        //    }
                        //}
                        md1 = GetSafeDecimal(sitem["GDXZXS" + i]);
                        md2 = md1 == 0 ? GetSafeDecimal(sitem["SCWDD" + i]) : GetSafeDecimal(sitem["SCWDD" + i]) * md1;
                        md2 = Math.Round(md2, 3);
                        sitem["SJWD" + i] = md2.ToString();
                        md = Math.Round(md2 / GetSafeDecimal(sitem["SJLZ" + i]), 3);

                        //马偕尔模数
                        sitem["MXEMS" + i] = md.ToString();
                    }
                }
                #endregion


                if (mbhggs == 0)
                {
                    sitem["JCJG"] = "合格";
                }
                else
                {
                    sitem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                mAllHg = (mAllHg && sitem["JCJG"] == "合格");
            }

            //主表总判断赋值
            if (mAllHg)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                MItem[0]["JCJGMS"] = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            #endregion
        }
    }
}