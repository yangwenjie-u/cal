using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class CJL : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_CJL"];
            var ET_HNL = data["ET_HNL"][0];
            var EJL_ZPZ = data["EJL_ZPZ"][0];
            var ET_YSZ = data["ET_YSZ"][0];
            if (!data.ContainsKey("M_CJL"))
            {
                data["M_CJL"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_CJL"];
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            var mrsDj = dataExtra["BZ_CJL_DJ"];
            var mrsSwDj = dataExtra["BZ_XJL_DJ"];
            //var E_SF = data["E_SF"];
            var mItem = MItem[0];
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                bool sign;
                double md1, md2, md, sum;
                string sfpd1, sfpd2, sfpd3, sfpd4, sfpd5, sfpd6, sfpd7, sfpd8, sfpd9, sfpd10, sfpd11, sfpd12, sfpd13;
                var jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                #region 筛分
                if (jcxm.Contains("、筛分、"))
                {
                    jcxmCur = "筛分";
                    sign = true;

                    //筛余量总和1
                    sItem["SYLZH1"] = Math.Round(GetSafeDouble(sItem["SYL106_1"].Trim()) + GetSafeDouble(sItem["SYL75_1"].Trim()) + GetSafeDouble(sItem["SYL63_1"].Trim()) + GetSafeDouble(sItem["SYL53_1"].Trim())
                         + GetSafeDouble(sItem["SYL375_1"].Trim()) + GetSafeDouble(sItem["SYL315_1"].Trim()) + GetSafeDouble(sItem["SYL265_1"].Trim()) + GetSafeDouble(sItem["SYL19_1"].Trim()) + GetSafeDouble(sItem["SYL16_1"].Trim())
                         + GetSafeDouble(sItem["SYL132_1"].Trim()) + GetSafeDouble(sItem["SYL95_1"].Trim()) + GetSafeDouble(sItem["SYL475_1"].Trim()) + GetSafeDouble(sItem["SYL236_1"].Trim()) + GetSafeDouble(sItem["SYL118_1"].Trim())
                         + GetSafeDouble(sItem["SYL06_1"].Trim()) + GetSafeDouble(sItem["SYL03_1"].Trim()) + GetSafeDouble(sItem["SYL015_1"].Trim()) + GetSafeDouble(sItem["SYL0075_1"].Trim()) + GetSafeDouble(sItem["SYLSD1"].Trim()), 1).ToString("0.0");

                    //筛余量总和2
                    sItem["SYLZH2"] = Math.Round(GetSafeDouble(sItem["SYL106_2"].Trim()) + GetSafeDouble(sItem["SYL75_2"].Trim()) + GetSafeDouble(sItem["SYL63_2"].Trim()) + GetSafeDouble(sItem["SYL53_2"].Trim())
                         + GetSafeDouble(sItem["SYL375_2"].Trim()) + GetSafeDouble(sItem["SYL315_2"].Trim()) + GetSafeDouble(sItem["SYL265_2"].Trim()) + GetSafeDouble(sItem["SYL19_2"].Trim()) + GetSafeDouble(sItem["SYL16_2"].Trim())
                         + GetSafeDouble(sItem["SYL132_2"].Trim()) + GetSafeDouble(sItem["SYL95_2"].Trim()) + GetSafeDouble(sItem["SYL475_2"].Trim()) + GetSafeDouble(sItem["SYL236_2"].Trim()) + GetSafeDouble(sItem["SYL118_2"].Trim())
                         + GetSafeDouble(sItem["SYL06_2"].Trim()) + GetSafeDouble(sItem["SYL03_2"].Trim()) + GetSafeDouble(sItem["SYL015_2"].Trim()) + GetSafeDouble(sItem["SYL0075_2"].Trim()) + GetSafeDouble(sItem["SYLSD2"].Trim()), 1).ToString("0.0");

                    sum = 0;
                    for (int i = 1; i < 3; i++)
                    {
                        sign = IsNumeric(sItem["SXHSSZL" + i].Trim());
                        sign = IsNumeric(sItem["SXHSXL" + i].Trim());
                        sign = IsNumeric(sItem["GZSYZL" + i].Trim());
                    }
                    if (!sign)
                    {
                        throw new SystemException("筛分数据录入有误");
                    }

                    //水洗后0.075mm筛下量  通过率
                    for (int i = 1; i < 3; i++)
                    {
                        //sItem["SXHSXL" + i] =Round(GetSafeDouble(sItem["GZSYZL" + i].Trim()) - GetSafeDouble(sItem["SXHSSZL" + i].Trim()),1).ToString("0.0");
                        sItem["TGL0075_" + i] = Round(GetSafeDouble(sItem["SXHSXL" + i]) / GetSafeDouble(sItem["GZSYZL" + i]) * 100, 1).ToString("0.0");
                        sum = sum + GetSafeDouble(sItem["TGL0075_" + i]);
                    }
                    //水洗后0.075mm 通过率平均值  两者差值超过1%需重新试验
                    mItem["TGLBZYQ0075"] = "≤1";
                    if (Math.Abs(GetSafeDouble(sItem["TGL0075_1"]) - GetSafeDouble(sItem["TGL0075_2"])) > 1)
                    {
                        sItem["PJTGL0075"] = "重新试验";
                        throw new SystemException("两次试验结果 P 0. 075 的差值超过 1％，试验应重新进行。");
                    }
                    else
                    {
                        sItem["PJTGL0075"] = Round((GetSafeDouble(sItem["TGL0075_1"]) + GetSafeDouble(sItem["TGL0075_2"])) / 2, 1).ToString("0.0");
                    }
                    #region 损耗判定
                    for (int i = 1; i < 3; i++)
                    {
                        //损耗质量   干燥试样总量 - 水洗后0.075筛下量(g)1
                        sItem["SYSH" + i] = (GetSafeDouble(sItem["GZSYZL" + i]) - (GetSafeDouble(sItem["SXHSXL" + i]) + GetSafeDouble(sItem["SXHSSZL" + i]))).ToString();
                        //损耗率
                        sItem["SYSHL" + i] = Round(GetSafeDouble(sItem["SYSH" + i]) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.00");
                    }
                    if (GetSafeDouble(sItem["SYSHL1"]) > 0.3 || GetSafeDouble(sItem["SYSHL2"]) > 0.3)
                    {
                        throw new SystemException("损耗率大于0.3%，试验应重新进行。");
                    }
                    #endregion

                    #region 分计筛余
                    for (int i = 1; i < 3; i++)
                    {
                        sItem["FJSY106_" + i] = Round(GetSafeDouble(sItem["SYL106_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY75_" + i] = Round(GetSafeDouble(sItem["SYL75_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY63_" + i] = Round(GetSafeDouble(sItem["SYL63_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY53_" + i] = Round(GetSafeDouble(sItem["SYL53_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY375_" + i] = Round(GetSafeDouble(sItem["SYL375_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY315_" + i] = Round(GetSafeDouble(sItem["SYL315_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY265_" + i] = Round(GetSafeDouble(sItem["SYL265_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY19_" + i] = Round(GetSafeDouble(sItem["SYL19_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY16_" + i] = Round(GetSafeDouble(sItem["SYL16_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY132_" + i] = Round(GetSafeDouble(sItem["SYL132_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY95_" + i] = Round(GetSafeDouble(sItem["SYL95_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY475_" + i] = Round(GetSafeDouble(sItem["SYL475_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY236_" + i] = Round(GetSafeDouble(sItem["SYL236_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY118_" + i] = Round(GetSafeDouble(sItem["SYL118_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY06_" + i] = Round(GetSafeDouble(sItem["SYL06_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY03_" + i] = Round(GetSafeDouble(sItem["SYL03_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY015_" + i] = Round(GetSafeDouble(sItem["SYL015_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                        sItem["FJSY0075_" + i] = Round(GetSafeDouble(sItem["SYL0075_" + i].Trim()) / GetSafeDouble(sItem["GZSYZL" + i].Trim()) * 100, 1).ToString("0.0");
                    }
                    #endregion
                    #region 平均分计筛余
                    sItem["PJFJSY106"] = Round((GetSafeDouble(sItem["FJSY106_1"]) + GetSafeDouble(sItem["FJSY106_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY75"] = Round((GetSafeDouble(sItem["FJSY75_1"]) + GetSafeDouble(sItem["FJSY75_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY63"] = Round((GetSafeDouble(sItem["FJSY63_1"]) + GetSafeDouble(sItem["FJSY106_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY53"] = Round((GetSafeDouble(sItem["FJSY53_1"]) + GetSafeDouble(sItem["FJSY53_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY375"] = Round((GetSafeDouble(sItem["FJSY375_1"]) + GetSafeDouble(sItem["FJSY375_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY315"] = Round((GetSafeDouble(sItem["FJSY315_1"]) + GetSafeDouble(sItem["FJSY315_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY265"] = Round((GetSafeDouble(sItem["FJSY265_1"]) + GetSafeDouble(sItem["FJSY265_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY19"] = Round((GetSafeDouble(sItem["FJSY19_1"]) + GetSafeDouble(sItem["FJSY19_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY16"] = Round((GetSafeDouble(sItem["FJSY16_1"]) + GetSafeDouble(sItem["FJSY16_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY132"] = Round((GetSafeDouble(sItem["FJSY132_1"]) + GetSafeDouble(sItem["FJSY132_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY95"] = Round((GetSafeDouble(sItem["FJSY95_1"]) + GetSafeDouble(sItem["FJSY95_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY475"] = Round((GetSafeDouble(sItem["FJSY475_1"]) + GetSafeDouble(sItem["FJSY475_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY236"] = Round((GetSafeDouble(sItem["FJSY236_1"]) + GetSafeDouble(sItem["FJSY236_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY118"] = Round((GetSafeDouble(sItem["FJSY118_1"]) + GetSafeDouble(sItem["FJSY118_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY06"] = Round((GetSafeDouble(sItem["FJSY06_1"]) + GetSafeDouble(sItem["FJSY06_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY03"] = Round((GetSafeDouble(sItem["FJSY03_1"]) + GetSafeDouble(sItem["FJSY03_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY015"] = Round((GetSafeDouble(sItem["FJSY015_1"]) + GetSafeDouble(sItem["FJSY015_2"])) / 2, 1).ToString("0.0");
                    sItem["PJFJSY0075"] = Round((GetSafeDouble(sItem["FJSY0075_1"]) + GetSafeDouble(sItem["FJSY0075_2"])) / 2, 1).ToString("0.0");
                    #endregion

                    #region 累计筛余
                    for (int i = 1; i < 3; i++)
                    {
                        sItem["LJSY106_" + i] = sItem["FJSY106_" + i];
                        sItem["LJSY75_" + i] = (GetSafeDouble(sItem["LJSY106_" + i]) + GetSafeDouble(sItem["FJSY75_" + i])).ToString("0.0");
                        sItem["LJSY63_" + i] = (GetSafeDouble(sItem["LJSY75_" + i]) + GetSafeDouble(sItem["FJSY63_" + i])).ToString("0.0");
                        sItem["LJSY53_" + i] = (GetSafeDouble(sItem["LJSY63_" + i]) + GetSafeDouble(sItem["FJSY53_" + i])).ToString("0.0");
                        sItem["LJSY375_" + i] = (GetSafeDouble(sItem["LJSY53_" + i]) + GetSafeDouble(sItem["FJSY375_" + i])).ToString("0.0");
                        sItem["LJSY315_" + i] = (GetSafeDouble(sItem["LJSY375_" + i]) + GetSafeDouble(sItem["FJSY315_" + i])).ToString("0.0");
                        sItem["LJSY265_" + i] = (GetSafeDouble(sItem["LJSY315_" + i]) + GetSafeDouble(sItem["FJSY265_" + i])).ToString("0.0");
                        sItem["LJSY19_" + i] = (GetSafeDouble(sItem["LJSY265_" + i]) + GetSafeDouble(sItem["FJSY19_" + i])).ToString("0.0");
                        sItem["LJSY16_" + i] = (GetSafeDouble(sItem["LJSY19_" + i]) + GetSafeDouble(sItem["FJSY16_" + i])).ToString("0.0");
                        sItem["LJSY132_" + i] = (GetSafeDouble(sItem["LJSY16_" + i]) + GetSafeDouble(sItem["FJSY132_" + i])).ToString("0.0");
                        sItem["LJSY95_" + i] = (GetSafeDouble(sItem["LJSY132_" + i]) + GetSafeDouble(sItem["FJSY95_" + i])).ToString("0.0");
                        sItem["LJSY475_" + i] = (GetSafeDouble(sItem["LJSY95_" + i]) + GetSafeDouble(sItem["FJSY475_" + i])).ToString("0.0");
                        sItem["LJSY236_" + i] = (GetSafeDouble(sItem["LJSY475_" + i]) + GetSafeDouble(sItem["FJSY236_" + i])).ToString("0.0");
                        sItem["LJSY118_" + i] = (GetSafeDouble(sItem["LJSY236_" + i]) + GetSafeDouble(sItem["FJSY118_" + i])).ToString("0.0");
                        sItem["LJSY06_" + i] = (GetSafeDouble(sItem["LJSY118_" + i]) + GetSafeDouble(sItem["FJSY06_" + i])).ToString("0.0");
                        sItem["LJSY03_" + i] = (GetSafeDouble(sItem["LJSY06_" + i]) + GetSafeDouble(sItem["FJSY03_" + i])).ToString("0.0");
                        sItem["LJSY015_" + i] = (GetSafeDouble(sItem["LJSY03_" + i]) + GetSafeDouble(sItem["FJSY015_" + i])).ToString("0.0");
                        sItem["LJSY0075_" + i] = (GetSafeDouble(sItem["LJSY015_" + i]) + GetSafeDouble(sItem["FJSY0075_" + i])).ToString("0.0");
                    }
                    #endregion
                    #region 平均累计筛余
                    sItem["PJLJSY106"] = Round((GetSafeDouble(sItem["LJSY106_1"]) + GetSafeDouble(sItem["LJSY106_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY75"] = Round((GetSafeDouble(sItem["LJSY75_1"]) + GetSafeDouble(sItem["LJSY75_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY63"] = Round((GetSafeDouble(sItem["LJSY63_1"]) + GetSafeDouble(sItem["LJSY106_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY53"] = Round((GetSafeDouble(sItem["LJSY53_1"]) + GetSafeDouble(sItem["LJSY53_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY375"] = Round((GetSafeDouble(sItem["LJSY375_1"]) + GetSafeDouble(sItem["LJSY375_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY315"] = Round((GetSafeDouble(sItem["LJSY315_1"]) + GetSafeDouble(sItem["LJSY315_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY265"] = Round((GetSafeDouble(sItem["LJSY265_1"]) + GetSafeDouble(sItem["LJSY265_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY19"] = Round((GetSafeDouble(sItem["LJSY19_1"]) + GetSafeDouble(sItem["LJSY19_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY16"] = Round((GetSafeDouble(sItem["LJSY16_1"]) + GetSafeDouble(sItem["LJSY16_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY132"] = Round((GetSafeDouble(sItem["LJSY132_1"]) + GetSafeDouble(sItem["LJSY132_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY95"] = Round((GetSafeDouble(sItem["LJSY95_1"]) + GetSafeDouble(sItem["LJSY95_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY475"] = Round((GetSafeDouble(sItem["LJSY475_1"]) + GetSafeDouble(sItem["LJSY475_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY236"] = Round((GetSafeDouble(sItem["LJSY236_1"]) + GetSafeDouble(sItem["LJSY236_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY118"] = Round((GetSafeDouble(sItem["LJSY118_1"]) + GetSafeDouble(sItem["LJSY118_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY06"] = Round((GetSafeDouble(sItem["LJSY06_1"]) + GetSafeDouble(sItem["LJSY06_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY03"] = Round((GetSafeDouble(sItem["LJSY03_1"]) + GetSafeDouble(sItem["LJSY03_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY015"] = Round((GetSafeDouble(sItem["LJSY015_1"]) + GetSafeDouble(sItem["LJSY015_2"])) / 2, 1).ToString("0.0");
                    sItem["PJLJSY0075"] = Round((GetSafeDouble(sItem["LJSY0075_1"]) + GetSafeDouble(sItem["LJSY0075_2"])) / 2, 1).ToString("0.0");
                    #endregion

                    #region 通过百分率
                    for (int i = 1; i < 3; i++)
                    {
                        sItem["TGBFL106_" + i] = Round(100 - GetSafeDouble(sItem["LJSY106_" + i]), 1).ToString("0.0");
                        sItem["TGBFL75_" + i] = Round(100 - GetSafeDouble(sItem["LJSY75_" + i]), 1).ToString("0.0");
                        sItem["TGBFL63_" + i] = Round(100 - GetSafeDouble(sItem["LJSY63_" + i]), 1).ToString("0.0");
                        sItem["TGBFL53_" + i] = Round(100 - GetSafeDouble(sItem["LJSY53_" + i]), 1).ToString("0.0");
                        sItem["TGBFL375_" + i] = Round(100 - GetSafeDouble(sItem["LJSY375_" + i]), 1).ToString("0.0");
                        sItem["TGBFL315_" + i] = Round(100 - GetSafeDouble(sItem["LJSY315_" + i]), 1).ToString("0.0");
                        sItem["TGBFL265_" + i] = Round(100 - GetSafeDouble(sItem["LJSY265_" + i]), 1).ToString("0.0");
                        sItem["TGBFL19_" + i] = Round(100 - GetSafeDouble(sItem["LJSY19_" + i]), 1).ToString("0.0");
                        sItem["TGBFL16_" + i] = Round(100 - GetSafeDouble(sItem["LJSY16_" + i]), 1).ToString("0.0");
                        sItem["TGBFL132_" + i] = Round(100 - GetSafeDouble(sItem["LJSY132_" + i]), 1).ToString("0.0");
                        sItem["TGBFL95_" + i] = Round(100 - GetSafeDouble(sItem["LJSY95_" + i]), 1).ToString("0.0");
                        sItem["TGBFL475_" + i] = Round(100 - GetSafeDouble(sItem["LJSY475_" + i]), 1).ToString("0.0");
                        sItem["TGBFL236_" + i] = Round(100 - GetSafeDouble(sItem["LJSY236_" + i]), 1).ToString("0.0");
                        sItem["TGBFL118_" + i] = Round(100 - GetSafeDouble(sItem["LJSY118_" + i]), 1).ToString("0.0");
                        sItem["TGBFL06_" + i] = Round(100 - GetSafeDouble(sItem["LJSY06_" + i]), 1).ToString("0.0");
                        sItem["TGBFL03_" + i] = Round(100 - GetSafeDouble(sItem["LJSY03_" + i]), 1).ToString("0.0");
                        sItem["TGBFL015_" + i] = Round(100 - GetSafeDouble(sItem["LJSY015_" + i]), 1).ToString("0.0");
                        sItem["TGBFL0075_" + i] = Round(100 - GetSafeDouble(sItem["LJSY0075_" + i]), 1).ToString("0.0");
                    }
                    #endregion
                    #region 平均通过百分率
                    sItem["PJTGBFL106"] = Round((GetSafeDouble(sItem["TGBFL106_1"]) + GetSafeDouble(sItem["TGBFL106_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL75"] = Round((GetSafeDouble(sItem["TGBFL75_1"]) + GetSafeDouble(sItem["TGBFL75_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL63"] = Round((GetSafeDouble(sItem["TGBFL63_1"]) + GetSafeDouble(sItem["TGBFL63_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL53"] = Round((GetSafeDouble(sItem["TGBFL53_1"]) + GetSafeDouble(sItem["TGBFL53_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL375"] = Round((GetSafeDouble(sItem["TGBFL375_1"]) + GetSafeDouble(sItem["TGBFL375_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL315"] = Round((GetSafeDouble(sItem["TGBFL315_1"]) + GetSafeDouble(sItem["TGBFL315_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL265"] = Round((GetSafeDouble(sItem["TGBFL265_1"]) + GetSafeDouble(sItem["TGBFL265_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL19"] = Round((GetSafeDouble(sItem["TGBFL19_1"]) + GetSafeDouble(sItem["TGBFL19_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL16"] = Round((GetSafeDouble(sItem["TGBFL16_1"]) + GetSafeDouble(sItem["TGBFL16_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL132"] = Round((GetSafeDouble(sItem["TGBFL132_1"]) + GetSafeDouble(sItem["TGBFL132_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL95"] = Round((GetSafeDouble(sItem["TGBFL95_1"]) + GetSafeDouble(sItem["TGBFL95_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL475"] = Round((GetSafeDouble(sItem["TGBFL475_1"]) + GetSafeDouble(sItem["TGBFL475_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL236"] = Round((GetSafeDouble(sItem["TGBFL236_1"]) + GetSafeDouble(sItem["TGBFL236_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL118"] = Round((GetSafeDouble(sItem["TGBFL118_1"]) + GetSafeDouble(sItem["TGBFL118_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL06"] = Round((GetSafeDouble(sItem["TGBFL06_1"]) + GetSafeDouble(sItem["TGBFL06_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL03"] = Round((GetSafeDouble(sItem["TGBFL03_1"]) + GetSafeDouble(sItem["TGBFL03_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL015"] = Round((GetSafeDouble(sItem["TGBFL015_1"]) + GetSafeDouble(sItem["TGBFL015_2"])) / 2, 1).ToString("0.0");
                    sItem["PJTGBFL0075"] = Round((GetSafeDouble(sItem["TGBFL0075_1"]) + GetSafeDouble(sItem["TGBFL0075_2"])) / 2, 1).ToString("0.0");
                    #endregion
                    //获取标准值
                    var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["GCLJGGMC"] == sItem["GGXH"]);
                    if (null == mrsDj_Filter)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    else
                    {
                        mItem["BZFW106"] = mrsDj_Filter["SKCC_106"];
                        mItem["BZFW75"] = mrsDj_Filter["SKCC_75"];
                        mItem["BZFW63"] = mrsDj_Filter["SKCC_63"];
                        mItem["BZFW53"] = mrsDj_Filter["SKCC_53"];
                        mItem["BZFW375"] = mrsDj_Filter["SKCC_375"];
                        mItem["BZFW315"] = mrsDj_Filter["SKCC_315"];
                        mItem["BZFW265"] = mrsDj_Filter["SKCC_265"];
                        mItem["BZFW19"] = mrsDj_Filter["SKCC_19"];
                        //mItem["BZFW16"  ] = mrsDj_Filter["SKCC_118" ];
                        mItem["BZFW132"] = mrsDj_Filter["SKCC_132"];
                        mItem["BZFW95"] = mrsDj_Filter["SKCC_95"];
                        mItem["BZFW475"] = mrsDj_Filter["SKCC_475"];
                        mItem["BZFW236"] = mrsDj_Filter["SKCC_236"];
                        //mItem["BZFW118" ] = mrsDj_Filter["SKCC_118" ];
                        mItem["BZFW06"] = mrsDj_Filter["SKCC_06"];
                        //mItem["BZFW03"  ] = mrsDj_Filter["SKCC_03"  ];
                        //mItem["BZFW015" ] = mrsDj_Filter["SKCC_015" ];
                        //mItem["BZFW0075"] = mrsDj_Filter["SKCC_0075"];
                    }

                    #region  根据规格型号进行筛分判定  报告中应该是根据规格型号显示筛孔
                    if (100 == GetSafeDouble(mItem["BZFW106"]))
                    {
                        sfpd1 = GetSafeDouble(sItem["PJTGBFL106"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW106"] && 100 != GetSafeDouble(mItem["BZFW106"]))
                    {
                        sfpd1 = IsQualified(mItem["BZFW106"], sItem["PJTGBFL106"], false);
                    }
                    else
                    {
                        mItem["BZFW106"] = "----";
                        sfpd1 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW75"]))
                    {
                        sfpd2 = GetSafeDouble(sItem["PJTGBFL75"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW75"] && 100 != GetSafeDouble(mItem["BZFW75"]))
                    {
                        sfpd2 = IsQualified(mItem["BZFW75"], sItem["PJTGBFL75"], false);
                    }
                    else
                    {
                        mItem["BZFW75"] = "----";
                        sfpd2 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW63"]))
                    {
                        sfpd3 = GetSafeDouble(sItem["PJTGBFL63"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW63"] && 100 != GetSafeDouble(mItem["BZFW63"]))
                    {
                        sfpd3 = IsQualified(mItem["BZFW63"], sItem["PJTGBFL63"], false);
                    }
                    else
                    {
                        mItem["BZFW63"] = "----";
                        sfpd3 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW53"]))
                    {
                        sfpd4 = GetSafeDouble(sItem["PJTGBFL53"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW53"] && 100 != GetSafeDouble(mItem["BZFW53"]))
                    {
                        sfpd4 = IsQualified(mItem["BZFW53"], sItem["PJTGBFL53"], false);
                    }
                    else
                    {
                        mItem["BZFW53"] = "----";
                        sfpd4 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW375"]))
                    {
                        sfpd5 = GetSafeDouble(sItem["PJTGBFL375"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW375"] && 100 != GetSafeDouble(mItem["BZFW375"]))
                    {
                        sfpd5 = IsQualified(mItem["BZFW375"], sItem["PJTGBFL375"], false);
                    }
                    else
                    {
                        mItem["BZFW375"] = "----";
                        sfpd5 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW315"]))
                    {
                        sfpd6 = GetSafeDouble(sItem["PJTGBFL315"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW315"] && 100 != GetSafeDouble(mItem["BZFW315"]))
                    {
                        sfpd6 = IsQualified(mItem["BZFW315"], sItem["PJTGBFL315"], false);
                    }
                    else
                    {
                        mItem["BZFW315"] = "----";
                        sfpd6 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW265"]))
                    {
                        sfpd7 = GetSafeDouble(sItem["PJTGBFL265"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW265"] && 100 != GetSafeDouble(mItem["BZFW265"]))
                    {
                        sfpd7 = IsQualified(mItem["BZFW265"], sItem["PJTGBFL265"], false);
                    }
                    else
                    {
                        mItem["BZFW265"] = "----";
                        sfpd7 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW19"]))
                    {
                        sfpd8 = GetSafeDouble(sItem["PJTGBFL19"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW19"] && 100 != GetSafeDouble(mItem["BZFW19"]))
                    {
                        sfpd8 = IsQualified(mItem["BZFW19"], sItem["PJTGBFL19"], false);
                    }
                    else
                    {
                        mItem["BZFW19"] = "----";
                        sfpd8 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW132"]))
                    {
                        sfpd9 = GetSafeDouble(sItem["PJTGBFL132"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW132"] && 100 != GetSafeDouble(mItem["BZFW132"]))
                    {
                        sfpd9 = IsQualified(mItem["BZFW132"], sItem["PJTGBFL132"], false);
                    }
                    else
                    {
                        mItem["BZFW132"] = "----";
                        sfpd9 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW95"]))
                    {
                        sfpd10 = GetSafeDouble(sItem["PJTGBFL95"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW95"] && 100 != GetSafeDouble(mItem["BZFW95"]))
                    {
                        sfpd10 = IsQualified(mItem["BZFW95"], sItem["PJTGBFL95"], false);
                    }
                    else
                    {
                        mItem["BZFW95"] = "----";
                        sfpd10 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW475"]))
                    {
                        sfpd11 = GetSafeDouble(sItem["PJTGBFL475"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW475"] && 100 != GetSafeDouble(mItem["BZFW475"]))
                    {
                        sfpd11 = IsQualified(mItem["BZFW475"], sItem["PJTGBFL475"], false);
                    }
                    else
                    {
                        mItem["BZFW475"] = "----";
                        sfpd11 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW236"]))
                    {
                        sfpd12 = GetSafeDouble(sItem["PJTGBFL236"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW236"] && 100 != GetSafeDouble(mItem["BZFW236"]))
                    {
                        sfpd12 = IsQualified(mItem["BZFW236"], sItem["PJTGBFL236"], false);
                    }
                    else
                    {
                        mItem["BZFW236"] = "----";
                        sfpd12 = "----";
                    }

                    if (100 == GetSafeDouble(mItem["BZFW06"]))
                    {
                        sfpd13 = GetSafeDouble(sItem["PJTGBFL06"]) == 100 ? "合格" : "不合格";
                    }
                    else if (null != mItem["BZFW06"] && 100 != GetSafeDouble(mItem["BZFW06"]))
                    {
                        sfpd13 = IsQualified(mItem["BZFW06"], sItem["PJTGBFL06"], false);
                    }
                    else
                    {
                        mItem["BZFW06"] = "----";
                        sfpd13 = "----";
                    }
                    #endregion
                    if (sfpd1 == "不合格" || sfpd2 == "不合格" || sfpd3 == "不合格" || sfpd4 == "不合格" || sfpd5 == "不合格" || sfpd6 == "不合格"
                         || sfpd7 == "不合格" || sfpd8 == "不合格" || sfpd9 == "不合格" || sfpd10 == "不合格" || sfpd11 == "不合格" || sfpd12 == "不合格" || sfpd13 == "不合格")
                    {
                        sItem["SFX_GH"] = "不符合" + sItem["GGXH"];
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else
                    {
                        sItem["SFX_GH"] = "符合" + sItem["GGXH"];
                    }
                }
                #endregion

                #region 密度及吸水率
                if (jcxm.Contains("、表观密度、") || jcxm.Contains("、密度、"))
                {

                    //获取标准要求
                    var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["DLDJ"] == sItem["DLDJ"]);
                    if (null == mrsDj_Filter)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    sItem["G_BGMD"] = mrsDj_Filter["G_XDMD"];
                    sItem["G_XSL"] = mrsDj_Filter["G_XSL"];
                    //不同温度水时的密度及水温修正系数
                    string sw = Conversion.Val(sItem["SW"]).ToString();
                    string sw2 = Conversion.Val(sItem["SW2"]).ToString();

                    var mrsSwDj_Filter1 = mrsSwDj.FirstOrDefault(x => x["SW"] == sw);
                    var mrsSwDj_Filter2 = mrsSwDj.FirstOrDefault(x => x["SW"] == sw2);
                    if (null == mrsSwDj_Filter1 || null == mrsSwDj_Filter2)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    string smd1 = mrsSwDj_Filter1["SMD"];
                    string smd2 = mrsSwDj_Filter2["SMD"];
                    sign = true;
                    sign = IsNumeric(sItem["HGZL1"].Trim());
                    sign = IsNumeric(sItem["HGZL2"].Trim());
                    sign = IsNumeric(sItem["BGZL1"].Trim());
                    sign = IsNumeric(sItem["BGZL2"].Trim());
                    sign = IsNumeric(sItem["SZZL1"].Trim());
                    sign = IsNumeric(sItem["SZZL1"].Trim());
                    if (sign)
                    {
                        for (int i = 1; i < 3; i++)
                        {
                            //表观相对密度
                            sItem["BGXDMD" + i] = Round(GetSafeDouble(sItem["HGZL" + i].Trim()) / (GetSafeDouble(sItem["HGZL" + i].Trim()) - GetSafeDouble(sItem["SZZL" + i].Trim())), 3).ToString("0.000");
                            //表干相对密度
                            sItem["XDMDBG" + i] = Round(GetSafeDouble(sItem["BGZL" + i].Trim()) / (GetSafeDouble(sItem["BGZL" + i].Trim()) - GetSafeDouble(sItem["SZZL" + i].Trim())), 3).ToString("0.000");
                            //毛体积相对密度
                            sItem["MTJXDMD" + i] = Round(GetSafeDouble(sItem["HGZL" + i].Trim()) / (GetSafeDouble(sItem["BGZL" + i].Trim()) - GetSafeDouble(sItem["SZZL" + i].Trim())), 3).ToString("0.000");
                            //吸水率
                            sItem["XSL" + i] = Round((GetSafeDouble(sItem["BGZL" + i].Trim()) - GetSafeDouble(sItem["HGZL" + i].Trim())) / GetSafeDouble(sItem["HGZL" + i].Trim()) * 100, 2).ToString("0.00");
                        }
                        //平均表观相对密度
                        sItem["BGXDMD"] = Round((GetSafeDouble(sItem["BGXDMD1"]) + GetSafeDouble(sItem["BGXDMD2"])) / 2, 3).ToString("0.000");
                        //平均表干相对密度
                        sItem["PJBGXDMD"] = Round((GetSafeDouble(sItem["XDMDBG1"]) + GetSafeDouble(sItem["XDMDBG2"])) / 2, 3).ToString("0.000");
                        //平均毛体积相对密度
                        sItem["MTJXDMDPJ"] = Round((GetSafeDouble(sItem["MTJXDMD1"]) + GetSafeDouble(sItem["MTJXDMD2"])) / 2, 3).ToString("0.000");
                        //表观密度
                        sItem["BGMD1"] = Round(GetSafeDouble(sItem["BGXDMD1"]) * GetSafeDouble(smd1), 3).ToString("0.000");
                        sItem["BGMD2"] = Round(GetSafeDouble(sItem["BGXDMD2"]) * GetSafeDouble(smd2), 3).ToString("0.000");
                        //表干密度
                        sItem["MDBG1"] = Round(GetSafeDouble(sItem["XDMDBG1"]) * GetSafeDouble(smd1), 3).ToString("0.000");
                        sItem["MDBG2"] = Round(GetSafeDouble(sItem["XDMDBG2"]) * GetSafeDouble(smd2), 3).ToString("0.000");
                        //毛体积密度
                        sItem["MTJMD1"] = Round(GetSafeDouble(sItem["MTJXDMD1"]) * GetSafeDouble(smd1), 3).ToString("0.000");
                        sItem["MTJMD2"] = Round(GetSafeDouble(sItem["MTJXDMD2"]) * GetSafeDouble(smd2), 3).ToString("0.000");
                        //平均表观密度
                        sItem["PJBGMD"] = Round((GetSafeDouble(sItem["BGMD1"]) + GetSafeDouble(sItem["BGMD2"])) / 2, 3).ToString("0.000");
                        //平均表干密度
                        sItem["MDBGPJ"] = Round((GetSafeDouble(sItem["MDBG1"]) + GetSafeDouble(sItem["MDBG2"])) / 2, 3).ToString("0.000");
                        //平均毛体积密度
                        sItem["PJMTJMD"] = Round((GetSafeDouble(sItem["MTJMD1"]) + GetSafeDouble(sItem["MTJMD2"])) / 2, 3).ToString("0.000");
                        //平均吸水率
                        sItem["PJXSL"] = Round((GetSafeDouble(sItem["XSL1"]) + GetSafeDouble(sItem["XSL2"])) / 2, 2).ToString("0.00");
                    }
                    else
                    {
                        throw new SystemException("密度及吸水率试验数据录入有误");
                    }
                    //吸水率判定
                    sItem["XSL_GH"] = IsQualified(sItem["G_XSL"], sItem["PJXSL"], true);
                    if (sItem["XSL_GH"] == "不符合")
                    {
                        jcxmCur = "吸水率";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }

                    //密度判定
                    sItem["BGMD_GH"] = IsQualified(sItem["G_BGMD"], sItem["BGXDMD"], true);
                    if (sItem["BGMD_GH"] == "不符合")
                    {
                        jcxmCur = "表观相对密度";
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["W_BGMD"] = "----";
                    sItem["BGMD_GH"] = "----";
                    sItem["G_BGMD"] = "----";
                    sItem["BGXDMD"] = "----";
                    sItem["PJXSL"] = "----";
                    sItem["XSL_GH"] = "----";
                }
                #endregion

                #region 堆积密度
                if (jcxm.Contains("、堆积密度、"))
                {
                    sItem["DJMD_GH"] = IsQualified(sItem["G_DJMD"], sItem["W_DJMD"], true);
                    if (sItem["DJMD_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["W_DJMD"] = "----";
                    sItem["DJMD_GH"] = "----";
                    sItem["G_DJMD"] = "----";
                }
                #endregion

                #region 振实密度
                if (jcxm.Contains("、振实密度、"))
                {
                    sItem["ZSMD_GH"] = IsQualified(sItem["G_ZSMD"], sItem["W_ZSMD"], true);
                    if (sItem["ZSMD_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["W_ZSMD"] = "----";
                    sItem["G_ZSMD"] = "----";
                    sItem["ZSMD_GH"] = "----";
                }
                #endregion

                #region 含泥量
                if (jcxm.Contains("、含泥量、"))
                {
                    jcxmCur = "含泥量";
                    if (IsNumeric(ET_HNL["Q_ZL1"].Trim()) && IsNumeric(ET_HNL["Q_ZL2"].Trim()) && IsNumeric(ET_HNL["H_ZL1"].Trim()) && IsNumeric(ET_HNL["H_ZL2"].Trim()))
                    {
                        ET_HNL["HNL1"] = Round((GetSafeDouble(ET_HNL["Q_ZL1"].Trim()) - GetSafeDouble(ET_HNL["H_ZL1"].Trim())) / GetSafeDouble(ET_HNL["Q_ZL1"].Trim()) * 100, 1).ToString("0.0");
                        ET_HNL["HNL2"] = Round((GetSafeDouble(ET_HNL["Q_ZL2"].Trim()) - GetSafeDouble(ET_HNL["H_ZL2"].Trim())) / GetSafeDouble(ET_HNL["Q_ZL2"].Trim()) * 100, 1).ToString("0.0");
                        ET_HNL["HNL"] = Round((GetSafeDouble(ET_HNL["HNL1"]) + GetSafeDouble(ET_HNL["HNL2"])) / 2, 1).ToString("0.0");
                    }
                    else
                    {
                        throw new SystemException("含泥量数据录入有误");
                    }
                    sItem["W_HNL"] = ET_HNL["HNL"];
                    sItem["HNL_GH"] = IsQualified(sItem["G_HNL"], sItem["W_HNL"], true);
                    if (sItem["HNL_GH"] == "不符合")
                    {
                        mAllHg = false;
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                }
                else
                {
                    sItem["HNL_GH"] = "----";
                    sItem["G_HNL"] = "----";
                    sItem["W_HNL"] = "----";
                }
                #endregion

                #region 泥块含量
                if (jcxm.Contains("、泥块含量、"))
                {
                    sItem["NKHL_GH"] = IsQualified(sItem["G_ZSMD"], sItem["W_ZSMD"], true);
                    if (sItem["HNL_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["NKHL_GH"] = "----";
                    sItem["G_NKHL"] = "----";
                    sItem["W_NKHL"] = "----";
                }
                #endregion

                #region 针状和片状颗粒总含量  || 针片状
                if (jcxm.Contains("、针状和片状颗粒总含量、") || jcxm.Contains("、针片状、"))
                {
                    jcxmCur = "针状和片状颗粒总含量";
                    var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["DLDJ"] == sItem["DLDJ"]);
                    if (null == mrsDj_Filter)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    else
                    {
                        sItem["G_ZZKL"] = mrsDj_Filter["G_ZPZ"];
                    }
                    if (IsNumeric(EJL_ZPZ["KLZL1"].Trim()) && IsNumeric(EJL_ZPZ["KLZL2"].Trim()) && IsNumeric(EJL_ZPZ["ZZL1"].Trim()) && IsNumeric(EJL_ZPZ["ZZL2"].Trim()))
                    {

                        if (IsNumeric(EJL_ZPZ["KLZL3"].Trim()) && IsNumeric(EJL_ZPZ["ZZL3"].Trim()))
                        {
                            EJL_ZPZ["ZPZ1"] = Round(GetSafeDouble(EJL_ZPZ["KLZL1"].Trim()) / GetSafeDouble(EJL_ZPZ["ZZL1"].Trim()) * 100, 1).ToString("0.0");
                            EJL_ZPZ["ZPZ2"] = Round(GetSafeDouble(EJL_ZPZ["KLZL2"].Trim()) / GetSafeDouble(EJL_ZPZ["ZZL2"].Trim()) * 100, 1).ToString("0.0");
                            EJL_ZPZ["ZPZ3"] = Round(GetSafeDouble(EJL_ZPZ["KLZL3"].Trim()) / GetSafeDouble(EJL_ZPZ["ZZL3"].Trim()) * 100, 1).ToString("0.0");
                            EJL_ZPZ["ZPZ"] = Round((GetSafeDouble(EJL_ZPZ["ZPZ1"]) + GetSafeDouble(EJL_ZPZ["ZPZ2"]) + GetSafeDouble(EJL_ZPZ["ZPZ3"])) / 3, 1).ToString("0.0");
                            sItem["W_ZZKL"] = EJL_ZPZ["ZPZ"];
                        }
                        else
                        {
                            EJL_ZPZ["ZPZ1"] = Round(GetSafeDouble(EJL_ZPZ["KLZL1"].Trim()) / GetSafeDouble(EJL_ZPZ["ZZL1"].Trim()) * 100, 1).ToString("0.0");
                            EJL_ZPZ["ZPZ2"] = Round(GetSafeDouble(EJL_ZPZ["KLZL2"].Trim()) / GetSafeDouble(EJL_ZPZ["ZZL2"].Trim()) * 100, 1).ToString("0.0");
                            EJL_ZPZ["ZPZ"] = Round((GetSafeDouble(EJL_ZPZ["ZPZ1"]) + GetSafeDouble(EJL_ZPZ["ZPZ2"])) / 2, 1).ToString("0.0");
                            sItem["W_ZZKL"] = EJL_ZPZ["ZPZ"];
                            if (Math.Abs(GetSafeDouble(EJL_ZPZ["ZPZ1"]) - GetSafeDouble(EJL_ZPZ["ZPZ2"])) > 20 || Math.Abs(GetSafeDouble(EJL_ZPZ["ZPZ1"]) - GetSafeDouble(EJL_ZPZ["ZPZ2"])) == 20)
                            {
                                throw new SystemException("针、片状颗粒含量试验两次结果之差大于等于20%，需追加试验");
                            }
                        }

                    }
                    else
                    {
                        throw new SystemException("针、片状颗粒含量试验数据录入有误");
                    }
                    if ("S14 3～5(mm)" == sItem["GGXH"])
                    {
                        sItem["G_ZZKL"] = "----";
                        sItem["ZZKL_GH"] = "----";
                    }
                    else
                    {
                        sItem["ZZKL_GH"] = IsQualified(sItem["G_ZZKL"], sItem["W_ZZKL"], true);
                        if (sItem["ZZKL_GH"] == "不符合")
                        {
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }

                }
                else
                {
                    sItem["ZZKL_GH"] = "----";
                    sItem["G_ZZKL"] = "----";
                    sItem["W_ZZKL"] = "----";
                }
                #endregion

                #region 压碎值指标 || 压碎值
                if (jcxm.Contains("、压碎值指标、") || jcxm.Contains("、压碎值、"))
                {
                    jcxmCur = "压碎值";
                    var mrsDj_Filter = mrsDj.FirstOrDefault(x => x["DLDJ"] == sItem["DLDJ"]);
                    if (null == mrsDj_Filter)
                    {
                        sItem["JCJG"] = "不下结论";
                        mAllHg = false;
                        continue;
                    }
                    else
                    {
                        sItem["G_YSZ"] = mrsDj_Filter["G_YSZ"];
                    }
                    sign = true;
                    for (int i = 1; i < 4; i++)
                    {
                        sign = IsNumeric(ET_YSZ["XL_ZL" + i].Trim());
                        sign = IsNumeric(ET_YSZ["HQ_ZL" + i].Trim());
                    }
                    if (sign)
                    {
                        ET_YSZ["YSZ1"] = Round(GetSafeDouble(ET_YSZ["XL_ZL1"].Trim()) / GetSafeDouble(ET_YSZ["HQ_ZL1"].Trim()) * 100, 1).ToString("0.0");
                        ET_YSZ["YSZ2"] = Round(GetSafeDouble(ET_YSZ["XL_ZL2"].Trim()) / GetSafeDouble(ET_YSZ["HQ_ZL2"].Trim()) * 100, 1).ToString("0.0");
                        ET_YSZ["YSZ3"] = Round(GetSafeDouble(ET_YSZ["XL_ZL3"].Trim()) / GetSafeDouble(ET_YSZ["HQ_ZL3"].Trim()) * 100, 1).ToString("0.0");
                        ET_YSZ["YSZ"] = Round((GetSafeDouble(ET_YSZ["YSZ1"]) + GetSafeDouble(ET_YSZ["YSZ2"]) + GetSafeDouble(ET_YSZ["YSZ3"])) / 3, 1).ToString("0.0");
                        sItem["W_YSZ"] = ET_YSZ["YSZ"];
                        sItem["YSZ_GH"] = IsQualified(sItem["G_YSZ"], sItem["W_YSZ"], true);
                        if (sItem["YSZ_GH"] == "不符合")
                        {
                            mAllHg = false;
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }
                    }

                }
                else
                {
                    sItem["YSZ_GH"] = "----";
                    sItem["G_YSZ"] = "----";
                    sItem["W_YSZ"] = "----";
                }
                #endregion

                #region 含水率
                if (jcxm.Contains("、含水率、"))
                {
                    sItem["HSL_GH"] = IsQualified(sItem["G_HSL"], sItem["W_HSL"], true);
                    if (sItem["HSL_GH"] == "不符合") mAllHg = false;
                }
                else
                {
                    sItem["HSL_GH"] = "----";
                    sItem["G_HSL"] = "----";
                    sItem["W_HSL"] = "----";
                }
                #endregion

                #region 吸水率  
                //if (jcxm.Contains("、吸水率、"))
                //{

                //    sItem["XSL_GH"] = IsQualified(sItem["G_XSL"], sItem["W_XSL"], true);
                //    if (sItem["HSL_GH"] == "不符合") mAllHg = false;
                //}
                //else
                //{
                //    sItem["XSL_GH"] = "----";
                //    sItem["G_XSL"] = "----";
                //    sItem["W_XSL"] = "----";
                //}
                #endregion

                #region 空隙率
                if (jcxm.Contains("、空隙率、") && IsNumeric(sItem["W_BGMD"]) && IsNumeric(sItem["W_DJMD"]))
                {
                    md1 = GetSafeDouble(sItem["W_BGMD"]);
                    md2 = GetSafeDouble(sItem["W_DJMD"]);
                    md = (1 - md2 / md1) * 100;
                    md = Round(md, 1);
                    sItem["W_KXL"] = Round(md, 1).ToString();
                    sItem["KXL_GH"] = IsQualified(sItem["G_KXL"], sItem["W_KXL"], true);
                }
                else
                {
                    sItem["W_KXL"] = "----";
                    sItem["KXL_GH"] = "----";
                    sItem["G_KXL"] = "----";
                }

                jsbeizhu = "";
                #endregion

                //if (jcxm == "、筛分析、" || jcxm == "、筛分、")
                //{
                //    jsbeizhu = "该组试样的检测结果详见报告第1～2页。";
                //}
                //else
                //{
                //    if (jcxm.Contains("筛分析") || jcxm.Contains("筛分"))
                //    {
                //        jsbeizhu = "该组试样的检测结果详见报告第1～2页。";
                //    }
                //    else
                //    {
                //        jsbeizhu = "该组试样的检测结果详见报告第1页。";
                //    }
                //}
                if (mAllHg)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
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
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求	。";
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
