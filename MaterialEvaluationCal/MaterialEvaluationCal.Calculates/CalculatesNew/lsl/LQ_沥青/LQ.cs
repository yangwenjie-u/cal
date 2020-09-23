using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class LQ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_LQ"];
            var MItem = data["M_LQ"];
            var mrsDj = dataExtra["BZ_LQ_DJ"];
            int mbHggs = 0;
            if (!data.ContainsKey("M_LQ"))
            {
                data["M_LQ"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mItem = MItem[0];
            bool sign = false;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                double pjmd, md, sum;
                var mrsdj = mrsDj.FirstOrDefault(u => u["MC"] == sItem["SJDJ"]);
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                if (mrsdj != null && mrsdj.Count != 0)
                {
                    sItem["G_ZRD"] = mrsdj["ZRD"].Trim();
                    sItem["G_RHD"] = mrsdj["RHD"].Trim();
                    sItem["G_YD"] = mrsdj["YD"].Trim();
                    sItem["G_RJD"] = mrsdj["RJD"].Trim();
                }
                else
                {
                    mItem["bgbh"] = "";
                    sItem["JCJG"] = "依据不详";
                    jsbeizhu = "试件尺寸为空";
                    continue;
                }

                #region 针入度
                sign = true;
                if (jcxm.Contains("、针入度、"))
                {
                    jcxmCur = "针入度";
                    //sItem["TJ_ZRD"] = sItem["SY_ZRD"];
                    for (int i = 1; i <= 3; i++)
                    {
                        sign = IsNumeric(sItem["ZRD" + i]) ? sign : false;
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (int i = 1; i <= 3; i++)
                        {
                            md = Conversion.Val(sItem["ZRD" + i]);
                            sum = md + sum;
                        }
                        pjmd = sum / 3;
                        pjmd = Round(pjmd, 0);
                        sItem["W_ZRD"] = pjmd.ToString();

                        sItem["HG_ZRD"] = IsQualified(sItem["G_ZRD"], sItem["W_ZRD"], false);
                        if (sItem["HG_ZRD"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                            mbHggs = mbHggs + 1;
                        }
                    }
                    else
                    {
                        throw new System.Exception("针入度试验数据录入有误");
                    }
                }
                else
                {
                    sItem["HG_ZRD"] = "----";
                    sItem["G_ZRD"] = "----";
                    sItem["W_ZRD"] = "----";
                    sItem["B_ZRD"] = "----";
                    //sItem["TJ_ZRD"] = "----";
                }
                #endregion

                #region 软化点
                sign = true;
                if (jcxm.Contains("、软化点、"))
                {
                    jcxmCur = "软化点";
                    sign = IsNumeric(sItem["RHD1"]) ? sign : false;
                    sign = IsNumeric(sItem["RHD2"]) ? sign : false;
                    if (sign)
                    {
                        //sItem["TJ_RHD"] = sItem["SY_RHD"];
                        sum = 0;
                        for (int i = 1; i <= 2; i++)
                        {
                            md = Conversion.Val(sItem["RHD" + i]);
                            sum = md + sum;
                        }
                        pjmd = sum / 2 / 5;
                        if ("蒸馏水" == sItem["JRJZ"])
                        {
                            pjmd = Round(pjmd, 1);
                            pjmd = pjmd * 5;
                            sItem["W_RHD"] = Round(pjmd, 1).ToString("0.0");
                        }
                        else
                        {
                            pjmd = Round(pjmd, 0);
                            pjmd = pjmd * 5;
                            sItem["W_RHD"] = Round(pjmd, 0).ToString();
                        }
                        sItem["HG_RHD"] = IsQualified(sItem["G_RHD"], sItem["W_RHD"], false);
                        if (sItem["HG_RHD"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                            mbHggs = mbHggs + 1;
                        }
                    }
                    else
                    {
                        throw new System.Exception("软化点试验数据录入有误");
                    }
                }
                else
                {
                    //sItem["TJ_RHD"] = "----";
                    sItem["HG_RHD"] = "----";
                    sItem["G_RHD"] = "----";
                    sItem["W_RHD"] = "----";
                    sItem["B_RHD"] = "----";
                }
                #endregion

                #region 延度
                sign = true;
                if (jcxm.Contains("、延度、"))
                {
                    jcxmCur = "延度";
                    for (int i = 1; i <= 3; i++)
                    {
                        sign = IsNumeric(sItem["YD" + i]) ? sign : false;
                    }
                    if (sign)
                    {
                        //sItem["TJ_YD"] = sItem["SY_YD"];
                        sum = 0;
                        for (int i = 1; i <= 3; i++)
                        {
                            md = Conversion.Val(sItem["YD" + i]);
                            sum = md + sum;
                        }
                        pjmd = sum / 3;
                        pjmd = Round(pjmd, 0);
                        sItem["W_YD"] = pjmd.ToString();
                        string yd = IsQualified(sItem["G_YD"], sItem["W_YD"], false);
                        sItem["HG_YD"] = yd;
                        sItem["W_YD"] = pjmd > 100 ? "＞100" : sItem["W_YD"];
                        if (sItem["HG_YD"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                            mbHggs = mbHggs + 1;
                        }

                    }
                    else
                    {
                        throw new System.Exception("软化点试验数据录入有误");
                    }
                }
                else
                {
                    sItem["HG_YD"] = "----";
                    sItem["G_YD"] = "----";
                    sItem["W_YD"] = "----";
                    sItem["B_YD"] = "----";
                    // sItem["TJ_YD"] = "----";
                }
                #endregion

                #region 
                if (jcxm.Contains("、溶解度、"))
                {
                    jcxmCur = "溶解度";
                    /**
                     * 溶解度 = {1-[（古氏坩埚、滤纸及不溶物总质量 m4 -古氏坩埚、滤纸质量 m1） + （锥形瓶、玻璃棒与黏附不溶物合质量 m5 - 锥形瓶、玻璃棒质量 m2）]  / （锥形瓶、玻璃棒与试样合质量 m3 - 锥形瓶、玻璃棒质量 m2）} * 100
                     * 平行试验 两次试验结果之差不大于0.1% 取平均值作为试验结果 溶解度大于99.0% 精确至 0.01%  小于等于99.0% 精确至0.1%
                     */
                    sItem["RJD1"] = Round((1 - (GetSafeDouble(sItem["GZSYZZL1"].Trim()) - GetSafeDouble(sItem["GZZL1"].Trim()) + GetSafeDouble(sItem["PBNFSYZL1"].Trim()) - GetSafeDouble(sItem["PBZL1"].Trim()))
                        / (GetSafeDouble(sItem["PBSYZZL1"].Trim()) - GetSafeDouble(sItem["PBZL1"].Trim()))) * 100, 2).ToString("0.00");
                    sItem["RJD2"] = Round((1 - (GetSafeDouble(sItem["GZSYZZL2"].Trim()) - GetSafeDouble(sItem["GZZL2"].Trim()) + GetSafeDouble(sItem["PBNFSYZL2"].Trim()) - GetSafeDouble(sItem["PBZL2"].Trim()))
                        / (GetSafeDouble(sItem["PBSYZZL2"].Trim()) - GetSafeDouble(sItem["PBZL2"].Trim()))) * 100, 2).ToString("0.00");
                    //两次平行试验结果之差不大于0.1% ，取平均值为试验结果
                    if (Math.Abs(GetSafeDouble(sItem["RJD1"]) - GetSafeDouble(sItem["RJD2"])) <= 0.1)
                    {
                        //溶解度大于99.0%  结果精确至0.01%
                        if ((GetSafeDouble(sItem["RJD1"]) + GetSafeDouble(sItem["RJD2"])) / 2 > 99.0)
                        {
                            sItem["W_RJD"] = Round((GetSafeDouble(sItem["RJD1"]) + GetSafeDouble(sItem["RJD2"])) / 2, 2).ToString("0.00");
                        }
                        else
                        {
                            //溶解度小于等于99.0%  结果精确至0.1%
                            sItem["W_RJD"] = Round((GetSafeDouble(sItem["RJD1"]) + GetSafeDouble(sItem["RJD2"])) / 2, 1).ToString("0.0");
                        }
                    }
                    else
                    {
                        throw new SystemException("两次平行试验结果之差大于0.1% ");
                    }

                    sItem["HG_RJD"] = IsQualified(sItem["G_RJD"], sItem["W_RJD"], false);
                    if (sItem["HG_RJD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        mbHggs = mbHggs + 1;
                    }
                }
                else
                {
                    sItem["HG_RJD"] = "----";
                    sItem["G_RJD"] = "----";
                    sItem["W_RJD"] = "----";
                    sItem["B_RJD"] = "----";
                }
                #endregion

                #region 黏附性
                sign = true;
                if (jcxm.Contains("、黏附性、"))
                {
                    sum = 0;
                    jcxmCur = "黏附性";

                    /**
                     * 同一试样平行试验5个集料颗粒   由两名试验人员分别评定，取平均等级作为试验结果
                     * 剥离面积百分率 = 0  黏附等级 5 剥离面积百分率 < 10%  黏附等级 4  剥离面积百分率 >=10% < 30%   黏附等级 3  剥离面积百分率 >30% 黏附等级 2   剥离面积百分率  =100%  黏附等级 1
                     */

                    for (int i = 1; i < 6; i++)
                    {
                        for (int j = 1; j < 3; j++)
                        {
                            if (!string.IsNullOrEmpty(sItem["SYYPDKL" + i + "_" + j]))
                            {
                                switch (sItem["SYYPDKL" + i + "_" + j])
                                {
                                    case "沥青膜完全保存，剥离面积百分率接近于0":
                                        sum = sum + 5;
                                        break;
                                    case "沥青膜少部分为水所移动，厚度不均匀，剥离面积百分率小于10%":
                                        sum = sum + 4;
                                        break;
                                    case "沥青膜局部明显地为水所移动，基本保留在表面上，厚度不均匀，剥离面积百分率小于30%":
                                        sum = sum + 3;
                                        break;
                                    case "沥青膜大部分为水所移动，局部保留在集料表面上，剥离面积百分率大于30%":
                                        sum = sum + 2;
                                        break;
                                    case "沥青膜完全为水所移动，集料基本裸露，沥青全浮于水面上":
                                        sum = sum + 1;
                                        break;
                                }
                            }
                            else
                            {
                                throw new SystemException("黏附性试验剥落程度数据录入不完整");
                            }
                        }
                    }
                    sItem["W_NFX"] = Round(sum / 10, 0).ToString("0");

                    if (!string.IsNullOrEmpty(sItem["G_NFX"]))
                    {
                        sItem["HG_NFX"] = IsQualified(sItem["G_NFX"], sItem["W_NFX"], false);
                        if (sItem["HG_NFX"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                            mbHggs = mbHggs + 1;
                        }
                    }
                    else
                    {
                        sItem["HG_NFX"] = "----";
                    }
                }
                else
                {
                    sItem["NFXJLLJ"] = "----";
                    sItem["NFXSYFF"] = "----";
                    sItem["HG_NFX"] = "----";
                    sItem["G_NFX"] = "----";
                    sItem["W_NFX"] = "----";
                    sItem["B_NFX"] = "----";
                }
                #endregion

                #region 闪点
                if (jcxm.Contains("、闪点、"))
                {
                    jcxmCur = "闪点";
                    sItem["HG_SD"] = IsQualified(sItem["G_SD"], sItem["W_SD"], false);
                    if (sItem["HG_SD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        mbHggs = mbHggs + 1;
                    }
                }
                else
                {
                    sItem["HG_SD"] = "----";
                    sItem["G_SD"] = "----";
                    sItem["W_SD"] = "----";
                    sItem["B_SD"] = "----";
                }
                #endregion

                #region 密度与相对密度
                if (jcxm.Contains("、密度与相对密度、"))
                {
                    jcxmCur = "密度与相对密度";
                    sItem["SMD1"] = "25℃" == sItem["MDXDMDSW1"].Trim() ? "0.9971" : "0.9991";
                    //sItem["SMD2"] = "25℃" == sItem["MDXDMDSW1"].Trim() ? "0.9971" : "0.9991";
                    //根据沥青形态使用不同公式   都为平行试验
                    if ("固体沥青" == sItem["LQXT"] || "黏稠沥青" == sItem["LQXT"])
                    {
                        /**
                         * 固态沥青
                         * 密度 = （比重瓶与沥青试样合计质量m6 - 比重瓶质量 m1） / [（比重瓶加水质量m2 - 比重瓶质量 m1）- (比重瓶加水加试样质量m7 - 比重瓶与沥青试样合计质量m6)] * 试样在试验温度下的密度 pw 
                         * 相对密度 = （比重瓶与沥青试样合计质量m6 - 比重瓶质量 m1） / [（比重瓶加水质量m2 - 比重瓶质量 m1）- (比重瓶加水加试样质量m7 - 比重瓶与沥青试样合计质量m6)]
                         */
                        /**
                         * 黏度沥青
                         * 密度 = （比重瓶与沥青试样合计质量m4 - 比重瓶质量m1）/ [（比重瓶加盛满水质量m2   - 比重瓶质量 m1） - （比重瓶加水加试样质量m5 - 比重瓶与沥青试样合计质量 m4）] * 试样在试验温度下的密度 pw 
                         * 相对密度 = （比重瓶与沥青试样合计质量m4 - 比重瓶质量m1）/ [（比重瓶加盛满水质量m2 - 比重瓶质量 m1） - （比重瓶加水加试样质量m5 - 比重瓶与沥青试样合计质量 m4）] 
                         */
                        sItem["MD1"] = Round((GetSafeDouble(sItem["PJLQSYZL1"].Trim()) - GetSafeDouble(sItem["PZL1"].Trim())) / (GetSafeDouble(sItem["PSZL1"].Trim())
                            - GetSafeDouble(sItem["PZL1"].Trim()) - GetSafeDouble(sItem["PJSJSYZL1"].Trim()) + GetSafeDouble(sItem["PJLQSYZL1"].Trim())) * GetSafeDouble(sItem["SMD1"]), 4).ToString("0.0000");
                        sItem["MD2"] = Round((GetSafeDouble(sItem["PJLQSYZL2"].Trim()) - GetSafeDouble(sItem["PZL2"].Trim())) / (GetSafeDouble(sItem["PSZL2"].Trim())
                            - GetSafeDouble(sItem["PZL2"].Trim()) - GetSafeDouble(sItem["PJSJSYZL2"].Trim()) + GetSafeDouble(sItem["PJLQSYZL2"].Trim())) * GetSafeDouble(sItem["SMD1"]), 4).ToString("0.0000");

                        sItem["XDMD1"] = Round((GetSafeDouble(sItem["PJLQSYZL1"].Trim()) - GetSafeDouble(sItem["PZL1"].Trim())) / (GetSafeDouble(sItem["PSZL1"].Trim())
                            - GetSafeDouble(sItem["PZL1"].Trim()) - GetSafeDouble(sItem["PJSJSYZL1"].Trim()) + GetSafeDouble(sItem["PJLQSYZL1"].Trim())), 4).ToString("0.0000");
                        sItem["XDMD2"] = Round((GetSafeDouble(sItem["PJLQSYZL2"].Trim()) - GetSafeDouble(sItem["PZL2"].Trim())) / (GetSafeDouble(sItem["PSZL2"].Trim())
                            - GetSafeDouble(sItem["PZL2"].Trim()) - GetSafeDouble(sItem["PJSJSYZL2"].Trim()) + GetSafeDouble(sItem["PJLQSYZL2"].Trim())), 4).ToString("0.0000");
                        if ("固体沥青" == sItem["LQXT"])
                        {
                            //固体沥青 重复性试验允许误差为 0.01g/cm³
                            if (Math.Abs(GetSafeDouble(sItem["MD1"]) - GetSafeDouble(sItem["MD2"])) <= 0.01)
                            {
                                sItem["W_MD"] = Round((GetSafeDouble(sItem["MD1"]) + GetSafeDouble(sItem["MD2"])) / 2, 3).ToString("0.000");
                            }
                            else
                            {
                                throw new SystemException("密度与相对密度试验，固体沥青密度平行试验重复性试验允许误差大于0.01g/cm³");
                            }
                            if (Math.Abs(GetSafeDouble(sItem["XDMD1"]) - GetSafeDouble(sItem["XDMD2"])) <= 0.01)
                            {
                                sItem["W_XDMD"] = Round((GetSafeDouble(sItem["XDMD1"]) + GetSafeDouble(sItem["XDMD2"])) / 2, 3).ToString("0.000");
                            }
                            else
                            {
                                throw new SystemException("密度与相对密度试验，固体沥青相对密度平行试验重复性试验允许误差大于0.01g/cm³");
                            }
                        }
                        else
                        {
                            //黏稠沥青 重复性试验允许误差为 0.003g/cm³
                            if (Math.Abs(GetSafeDouble(sItem["MD1"]) - GetSafeDouble(sItem["MD2"])) <= 0.003)
                            {
                                sItem["W_MD"] = Round((GetSafeDouble(sItem["MD1"]) + GetSafeDouble(sItem["MD2"])) / 2, 3).ToString("0.000");
                            }
                            else
                            {
                                throw new SystemException("密度与相对密度试验，黏稠沥青密度平行试验重复性试验允许误差大于0.003g/cm³");
                            }
                            if (Math.Abs(GetSafeDouble(sItem["XDMD1"]) - GetSafeDouble(sItem["XDMD2"])) <= 0.003)
                            {
                                sItem["W_XDMD"] = Round((GetSafeDouble(sItem["XDMD1"]) + GetSafeDouble(sItem["XDMD2"])) / 2, 3).ToString("0.000");
                            }
                            else
                            {
                                throw new SystemException("密度与相对密度试验，黏稠沥青相对密度平行试验重复性试验允许误差大于0.003g/cm³");
                            }
                        }

                    }
                    else if ("液体沥青" == sItem["LQXT"])
                    {
                        /**
                         * 密度 = （比重瓶与所盛满试样合计质量m3 - 比重瓶质量 m1） / （比重瓶加盛满水质量m2 - 比重瓶质量 m1）  * 试样在试验温度下的密度 pw 
                         * 相对密度 = （比重瓶与所盛满试样合计质量m3 - 比重瓶质量 m1） / （比重瓶加盛满水质量m2 - 比重瓶质量 m1）
                         * 15℃ 水密度0.9991   25℃ 水密度0.9971
                         */
                        sItem["MD1"] = Round((GetSafeDouble(sItem["PJYPZL1"].Trim()) - GetSafeDouble(sItem["PZL1"].Trim())) / (GetSafeDouble(sItem["PSZL1"].Trim()) - GetSafeDouble(sItem["PZL1"].Trim())) * GetSafeDouble(sItem["SMD1"]), 4).ToString("0.0000");
                        sItem["MD2"] = Round((GetSafeDouble(sItem["PJYPZL2"].Trim()) - GetSafeDouble(sItem["PZL2"].Trim())) / (GetSafeDouble(sItem["PSZL2"].Trim()) - GetSafeDouble(sItem["PZL2"].Trim())) * GetSafeDouble(sItem["SMD2"]), 4).ToString("0.0000");

                        sItem["XDMD1"] = Round((GetSafeDouble(sItem["PJYPZL1"].Trim()) - GetSafeDouble(sItem["PZL1"].Trim())) / (GetSafeDouble(sItem["PSZL1"].Trim()) - GetSafeDouble(sItem["PZL1"].Trim())) * GetSafeDouble(sItem["SMD1"]), 4).ToString("0.0000");
                        sItem["XDMD2"] = Round((GetSafeDouble(sItem["PJYPZL2"].Trim()) - GetSafeDouble(sItem["PZL2"].Trim())) / (GetSafeDouble(sItem["PSZL2"].Trim()) - GetSafeDouble(sItem["PZL2"].Trim())) * GetSafeDouble(sItem["SMD2"]), 4).ToString("0.0000");
                        if (Math.Abs(GetSafeDouble(sItem["MD1"]) - GetSafeDouble(sItem["MD2"])) <= 0.003)
                        {
                            //液体沥青 重复性试验允许误差为 0.003g/cm³
                            sItem["W_MD"] = Round((GetSafeDouble(sItem["MD1"]) + GetSafeDouble(sItem["MD2"])) / 2, 3).ToString("0.000");
                        }
                        else
                        {
                            throw new SystemException("密度与相对密度试验，液体沥青密度平行试验重复性试验允许误差大于0.003g/cm³");
                        }
                        if (Math.Abs(GetSafeDouble(sItem["XDMD1"]) - GetSafeDouble(sItem["XDMD2"])) <= 0.003)
                        {
                            sItem["W_XDMD"] = Round((GetSafeDouble(sItem["XDMD1"]) + GetSafeDouble(sItem["XDMD2"])) / 2, 3).ToString("0.000");
                        }
                        else
                        {
                            throw new SystemException("密度与相对密度试验，液体沥青相对密度平行试验重复性试验允许误差大于0.003g/cm³");
                        }
                    }
                    else
                    {
                        throw new System.Exception("沥青形态数据有误，请联系技术人员。");
                    }

                    if (IsQualified(sItem["G_MD"], sItem["W_MD"], false) == "不合格" || IsQualified(sItem["G_XDMD"], sItem["W_XDMD"], false) == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        sItem["HG_MD"] = "不合格";
                        mbHggs = mbHggs + 1;
                    }
                    else
                    {
                        if (IsQualified(sItem["G_MD"], sItem["W_MD"], false) == "----" || IsQualified(sItem["G_XDMD"], sItem["W_XDMD"], false) == "----")
                        {
                            sItem["HG_MD"] = "----";
                        }
                        else
                        {
                            sItem["HG_MD"] = "合格";
                        }
                    }
                }
                else
                {
                    sItem["HG_MD"] = "----";
                    sItem["G_MD"] = "----";
                    sItem["W_MD"] = "----";
                    sItem["G_XDMD"] = "----";
                    sItem["W_XDMD"] = "----";
                    sItem["B_MD"] = "----";
                }
                #endregion


                if (mbHggs > 0)
                {
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                }
                else
                {
                    sItem["JCJG"] = "合格";
                }
            }

            #region 添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目$$" + jcxmBhg.TrimEnd('、') + "$$不符合要求。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
