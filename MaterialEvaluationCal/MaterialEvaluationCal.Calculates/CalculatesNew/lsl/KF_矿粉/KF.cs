using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class KF : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/

            string bhg_jcxm = "";
            bool mAllHg = true, sign = true, mSFwc = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var jcjg = "";
            double md, md1, md2;
            var SItem = data["S_KF"];
            var MItem = data["M_KF"];
            //var mItem = MItem[0];
            double sum;
            var EKF_MD = data["EKF_MD"][0];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                bool jcjgHg = true;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                #region 筛分
                if (jcxm.Contains("、筛分、"))
                {
                    jcxmCur = "筛分";
                    if (IsNumeric(sItem["SY1_06"]) && IsNumeric(sItem["SY1_03"]) && IsNumeric(sItem["SY1_0075"]) && IsNumeric(sItem["SY1_015"]) && IsNumeric(sItem["SY2_0075"])
                        && IsNumeric(sItem["SY2_015"]) && IsNumeric(sItem["SY2_03"]) && IsNumeric(sItem["SY2_06"]) && IsNumeric(sItem["SYYL"]))
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            //分计筛余百分率 % = 筛上重g / 筛余用量g
                            sItem["FJSYBFL" + i + "_06"] = Round(GetSafeDouble(sItem["SY" + i + "_06"].Trim()) / GetSafeDouble(sItem["SYYL"].Trim()) * 100, 1).ToString("0.0");//0.6mm筛孔
                            sItem["FJSYBFL" + i + "_03"] = Round(GetSafeDouble(sItem["SY" + i + "_03"].Trim()) / GetSafeDouble(sItem["SYYL"].Trim()) * 100, 1).ToString("0.0");//0.3mm筛孔
                            sItem["FJSYBFL" + i + "_015"] = Round(GetSafeDouble(sItem["SY" + i + "_015"].Trim()) / GetSafeDouble(sItem["SYYL"].Trim()) * 100, 1).ToString("0.0");//0.15mm筛孔
                            sItem["FJSYBFL" + i + "_0075"] = Round(GetSafeDouble(sItem["SY" + i + "_0075"].Trim()) / GetSafeDouble(sItem["SYYL"].Trim()) * 100, 1).ToString("0.0");//0.075mm筛孔
                            //累计筛余百分率% 
                            sItem["LJSYBFL" + i + "_06"] = sItem["FJSYBFL" + i + "_06"];//0.6mm筛孔
                            sItem["LJSYBFL" + i + "_03"] = Round(GetSafeDouble(sItem["FJSYBFL" + i + "_06"]) + GetSafeDouble(sItem["FJSYBFL" + i + "_03"]), 1).ToString("0.0");//0.3mm筛孔
                            sItem["LJSYBFL" + i + "_015"] = Round(GetSafeDouble(sItem["FJSYBFL" + i + "_06"]) + GetSafeDouble(sItem["FJSYBFL" + i + "_03"]) + GetSafeDouble(sItem["FJSYBFL" + i + "_015"]), 1).ToString("0.0");//0.15mm筛孔
                            sItem["LJSYBFL" + i + "_0075"] = Round(GetSafeDouble(sItem["FJSYBFL" + i + "_06"]) + GetSafeDouble(sItem["FJSYBFL" + i + "_03"]) + GetSafeDouble(sItem["FJSYBFL" + i + "_015"]) + GetSafeDouble(sItem["FJSYBFL" + i + "_0075"]), 1).ToString("0.0");//0.075mm筛孔
                            //累计通过百分率%  = 100 - 累计筛余百分率
                            sItem["LJTGBFL" + i + "_06"] = Round(100 - GetSafeDouble(sItem["LJSYBFL" + i + "_06"]), 1).ToString("0.0");//0.6mm筛孔
                            sItem["LJTGBFL" + i + "_03"] = Round(100 - GetSafeDouble(sItem["LJSYBFL" + i + "_03"]), 1).ToString("0.0");//0.3mm筛孔
                            sItem["LJTGBFL" + i + "_015"] = Round(100 - GetSafeDouble(sItem["LJSYBFL" + i + "_015"]), 1).ToString("0.0");//0.15mm筛孔
                            sItem["LJTGBFL" + i + "_0075"] = Round(100 - GetSafeDouble(sItem["LJSYBFL" + i + "_0075"]), 1).ToString("0.0");//0.075mm筛孔
                        }
                        //平均累计筛余百分率%
                        sItem["PJLJSYBFL_06"] = Round((GetSafeDouble(sItem["LJSYBFL1_06"]) + GetSafeDouble(sItem["LJSYBFL2_06"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSYBFL_03"] = Round((GetSafeDouble(sItem["LJSYBFL1_03"]) + GetSafeDouble(sItem["LJSYBFL2_03"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSYBFL_015"] = Round((GetSafeDouble(sItem["LJSYBFL1_015"]) + GetSafeDouble(sItem["LJSYBFL2_015"])) / 2, 1).ToString("0.0");
                        sItem["PJLJSYBFL_0075"] = Round((GetSafeDouble(sItem["LJSYBFL1_0075"]) + GetSafeDouble(sItem["LJSYBFL2_0075"])) / 2, 1).ToString("0.0");
                        //平均累计通过百分率%
                        sItem["PJLJTGBFL_06"] = Round((GetSafeDouble(sItem["LJTGBFL1_06"]) + GetSafeDouble(sItem["LJTGBFL2_06"])) / 2, 1).ToString("0.0");
                        sItem["PJLJTGBFL_03"] = Round((GetSafeDouble(sItem["LJTGBFL1_03"]) + GetSafeDouble(sItem["LJTGBFL2_03"])) / 2, 1).ToString("0.0");
                        sItem["PJLJTGBFL_015"] = Round((GetSafeDouble(sItem["LJTGBFL1_015"]) + GetSafeDouble(sItem["LJTGBFL2_015"])) / 2, 1).ToString("0.0");
                        sItem["PJLJTGBFL_0075"] = Round((GetSafeDouble(sItem["LJTGBFL1_0075"]) + GetSafeDouble(sItem["LJTGBFL2_0075"])) / 2, 1).ToString("0.0");

                        //< 0.075mm筛孔含量
                        sItem["SKHL_0075"] = Round(GetSafeDouble(sItem["PJLJTGBFL_0075"])/100 * GetSafeDouble(sItem["SYYL"]), 1).ToString("0.0");

                        //判定 0.6mm  0.15mm  0.015mm筛孔 平均累计通过率是否符合规范 BZZ_06   100%  BZZ_03 ----  BZZ_015  90~100  BZZ_0075  75-100
                        string hgpd_015 = IsQualified(sItem["BZZ_015"],sItem["PJLJTGBFL_015"],true);
                        string hgpd_0075 = IsQualified(sItem["BZZ_0075"], sItem["PJLJTGBFL_0075"], true);
                        if (100 == GetSafeDouble(sItem["PJLJTGBFL_06"]) && "符合"== hgpd_015 && "符合" == hgpd_0075)
                        {
                            sItem["SFPD"] = "合格";
                        }
                        else
                        {
                            mAllHg = false;
                            sItem["SFPD"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }

                    }
                    else
                    {
                        throw new System.Exception("筛分数据录入有误");
                    }
                }
                else
                {
                    sItem["PJLJTGBFL_0075"] = "----";
                    sItem["PJLJTGBFL_015"] = "----";
                    sItem["PJLJTGBFL_03"] = "----";
                    sItem["PJLJTGBFL_06"] = "----";
                    sItem["SFPD"] = "----";
                }
                #endregion

                #region 密度
                if (jcxm.Contains("、密度、"))
                {
                    jcxmCur = "密度";
                    if (IsNumeric(EKF_MD["ZLQ1"]) && IsNumeric(EKF_MD["ZLQ2"]) && IsNumeric(EKF_MD["ZLH1"]) && IsNumeric(EKF_MD["ZLH2"])
                        && IsNumeric(EKF_MD["DSZ1"]) && IsNumeric(EKF_MD["DSZ2"]) && IsNumeric(EKF_MD["DSC1"]) && IsNumeric(EKF_MD["DSC2"]))
                    {
                        //密度 =( 试验前矿粉干燥质量1 - 试验后矿粉干燥质量1)/(比重瓶终读数1-比重瓶初读数1)  精确到 0.001  g/cm³
                        for (int i = 1; i < 3; i++)
                        {
                            EKF_MD["MD" + i] = Round((GetSafeDouble(EKF_MD["ZLQ" + i].Trim()) - GetSafeDouble(EKF_MD["ZLH" + i].Trim())) / (GetSafeDouble(EKF_MD["DSZ" + i].Trim()) - GetSafeDouble(EKF_MD["DSC" + i].Trim())), 3).ToString("0.000");
                        }
                        //平均密度
                        EKF_MD["MD"] = Round((GetSafeDouble(EKF_MD["MD1"]) + GetSafeDouble(EKF_MD["MD2"])) / 2, 3).ToString("0.000");
                        double xdmd1 = Round(GetSafeDouble(EKF_MD["MD1"]) / GetSafeDouble(EKF_MD["WD1"]), 3);
                        double xdmd2 = Round(GetSafeDouble(EKF_MD["MD2"]) / GetSafeDouble(EKF_MD["WD2"]), 3);
                        //平均相对密度
                        EKF_MD["XDMD"] = Round((xdmd1 + xdmd2) / 2, 3).ToString("0.000");
                        sItem["W_MD"] = EKF_MD["MD"];

                        sItem["MD_GH"] = IsQualified(sItem["G_MD"], sItem["W_MD"], true);
                        if (sItem["MD_GH"] == "不符合") mAllHg = false;
                        if (sItem["MD_GH"] == "不符合")
                        {
                            mAllHg = false;
                            sItem["MD_GH"] = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }
                    else
                    {
                        throw new System.Exception("密度数据录入有误");
                    }
                }
                else
                {
                    sItem["MD_GH"] = "----";
                    sItem["G_MD"] = "----";
                    sItem["W_MD"] = "----";
                }
                #endregion

                #region 塑性指数
                if (jcxm.Contains("、塑性指数、"))
                {
                    jcxmCur = "塑性指数";
                    sItem["SXZS_GH"] = IsQualified(sItem["G_SXZS"], sItem["W_SXZS"], true);
                    if (sItem["SXZS_GH"] == "不符合")
                    {
                        mAllHg = false;
                        sItem["SXZS_GH"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["G_SXZS"] = "----";
                    sItem["SXZS_GH"] = "----";
                    sItem["W_SXZS"] = "----";
                }
                #endregion

                //MItem[0]["BHG_JCXM"] = bhg_jcxm;
                //jsbeizhu = "";
                //{
                //    jsbeizhu = "该试样的检测结果详见报告。";
                //}
                //if (jcjgHg)
                //{
                //    sItem["JCJG"] = "合格";
                //    MItem[0]["JCJG"] = "合格";
                //}
                //else
                //{
                //    sItem["JCJG"] = "不合格";
                //    MItem[0]["JCJG"] = "不合格";
                //}
                //mAllHg = mAllHg & sItem["JCJG"] == "合格" ? true : false;
            }
            //if (mAllHg)
            //{
            //    MItem[0]["JCJG"] = "合格";
            //}
            //else
            //{
            //    MItem[0]["JCJG"] = "不合格";
            //}

            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                jsbeizhu = "该组试样所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + MItem[0]["PDBZ"] + "标准要求。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;

            //#region 添加最终报告
            //if (mAllHg)
            //{
            //    mjcjg = "合格";
            //}
            //if (!data.ContainsKey("M_KF"))
            //{
            //    data["M_KF"] = new List<IDictionary<string, string>>();
            //}
            //var M_KF = data["M_KF"];
            //if (M_KF.Count == 0)
            //{
            //    IDictionary<string, string> m = new Dictionary<string, string>();
            //    m["JCJG"] = mjcjg;
            //    m["JCJGMS"] = jsbeizhu;
            //    M_KF.Add(m);
            //}
            //else
            //{
            //    M_KF[0]["JCJG"] = mjcjg;
            //    M_KF[0]["JCJGMS"] = jsbeizhu;
            //}
            #endregion
        }
    }
}
