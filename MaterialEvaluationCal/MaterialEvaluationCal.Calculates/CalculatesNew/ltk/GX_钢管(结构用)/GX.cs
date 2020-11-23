using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class GX : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/

            #region

            var extraDJ = dataExtra["BZ_GX_DJ"];

            //var jcxmItems = retData.Select(u => u.Key).ToArray();
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            var jcjg = "";
            var jcxmCur = "";
            var jcxmBhg = "";
            var S_GXS = data["S_GX"];
            if (!data.ContainsKey("M_GX"))
            {
                data["M_GX"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GX"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            double md1, md2, md, pjmd, sum, sum2 = 0;
            bool flag, sign = false;
            bool SFlg = false;//是否两根
            bool itemHG = true;//判断单组是否合格
            int mbHggs = 0;//检测项目合格数量

            //遍历从表数据
            foreach (var sItem in S_GXS)
            {
                itemHG = true;
                var jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";


                #region 根据样品名称，要求根数是否为2根
                switch (sItem["YPMC"])
                {
                    case "低合金高强度结构钢钢管":
                    case "合金钢钢管":
                        SFlg = true;
                        break;
                    case "结构用无缝钢管":
                        SFlg = true;
                        break;
                    case "优质碳素结构钢钢管":
                        SFlg = true;
                        break;
                    case "结构用无缝钢管(对接焊)":
                        SFlg = true;
                        break;
                    default:
                        SFlg = false;
                        break;
                }
                #endregion
                #region 等级表处理
                foreach (var extraFieldsDj in extraDJ)
                {
                    if (sItem["GGPH"].Trim() == extraFieldsDj["GGPH"].Trim() && sItem["YPMC"].Trim() == extraFieldsDj["GGLB"].Trim())
                    {
                        switch (sItem["YPMC"].Trim())
                        {
                            case "低压流体输送用焊接钢管":
                                if (IsQualified("≤16", sItem["BHFW"], false) == "合格")
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD1"].Trim();
                                }
                                else
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD2"].Trim();
                                }
                                if (IsQualified("≤168.3", sItem["WJFW"], false) == "合格")
                                {
                                    sItem["G_SCL"] = extraFieldsDj["SCL1"].Trim();
                                }
                                else
                                {
                                    sItem["G_SCL"] = extraFieldsDj["SCL2"].Trim();
                                }
                                sItem["G_YB"] = "压扁试验后不得出现裂纹、分层";
                                break;

                            case "合金钢钢管":
                            case "直缝电焊钢管":
                                sItem["G_QFQD"] = extraFieldsDj["QFQD1"].Trim();
                                sItem["G_SCL"] = extraFieldsDj["SCL1"].Trim();
                                sItem["G_YB"] = "压扁试验后不得出现裂纹或裂口";
                                break;

                            default:
                                if (IsQualified("≤16", sItem["BHFW"], false) == "合格")
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD1"].Trim();
                                }
                                else if (IsQualified("＞30", sItem["BHFW"], false) == "合格")
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD3"].Trim();
                                }
                                else
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD2"].Trim();
                                }
                                sItem["G_SCL"] = extraFieldsDj["SCL1"].Trim();
                                sItem["G_YB"] = "压扁试验后不得出现裂纹或裂口";
                                break;
                        }
                        sItem["G_KLQD"] = extraFieldsDj["KLQD"].Trim();
                        sItem["G_LW"] = "弯心d为6D弯曲角度90°受弯部位表面不得产生裂纹";
                        break;
                    }
                }
                #endregion

                #region 尺寸偏差

                if (jcxm.Contains("、尺寸偏差、"))
                {

                    #region 外径

                    #region 外径标准值 GWJPC
                    if (sItem["GGLX"] == "热扩钢管" || sItem["GGLX"] == "热轧钢管")
                    {

                        if (GetSafeDouble(sItem["ZJFW"]) * 0.01 > 0.5)
                        {
                            sItem["GWJPC"] = sItem["ZJFW"];
                        }
                        else { sItem["GWJPC"] = "0.5"; }
                    }
                    else
                    {
                        if (GetSafeDouble(sItem["ZJFW"]) * 0.0075 > 0.3)
                        {
                            sItem["GWJPC"] = sItem["ZJFW"];
                        }
                        else { sItem["GWJPC"] = "0.3"; }
                    }
                    #endregion

                    sItem["GGWJ1"] = Round((GetSafeDouble(sItem["GGWJ1_1"]) + GetSafeDouble(sItem["GGWJ1_2"])) / 2, 1).ToString("0.0");
                    sItem["GGWJ2"] = Round((GetSafeDouble(sItem["GGWJ2_1"]) + GetSafeDouble(sItem["GGWJ2_2"])) / 2, 1).ToString("0.0");
                    sItem["GGWJPC1"] = Round(GetSafeDouble(sItem["GGWJ1"]) - GetSafeDouble(sItem["ZJFW"]), 2).ToString("0.0");
                    sItem["GGWJPC2"] = Round(GetSafeDouble(sItem["GGWJ2"]) - GetSafeDouble(sItem["ZJFW"]), 2).ToString("0.0");

                    if (Math.Abs(GetSafeDouble(sItem["GGWJPC1"])) > Math.Abs(GetSafeDouble(sItem["GWJPC"])) && Math.Abs(GetSafeDouble(sItem["GGWJPC2"])) > Math.Abs(GetSafeDouble(sItem["GWJPC"])))
                    {
                        sItem["WJPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                   
                    else { sItem["WJPD"] = "合格";mbHggs++; }

                    #endregion

                    #region 壁厚

                    #region 壁厚标准值 GBHPC
                    if (sItem["GGLX"] == "热轧钢管")
                    {
                        if (GetSafeDouble(sItem["ZJFW"]) <= 102)
                        {
                            if (GetSafeDouble(sItem["BHFW"]) * 0.125 > 0.4)
                            {
                                sItem["GBHPC"] = "-" + sItem["BHFW"] + "～" + sItem["BHFW"];
                            }
                            else { sItem["GBHPC"] = "-" + "0.4" + "～" + "0.4"; }
                        }
                        if (GetSafeDouble(sItem["ZJFW"]) > 102)
                        {
                            if (GetSafeDouble(sItem["BHFW"]) / GetSafeDouble(sItem["ZJFW"]) <= 0.05)
                            {
                                if (GetSafeDouble(sItem["BHFW"]) * 0.15 > 0.4)
                                {
                                    sItem["GBHPC"] = "-" + sItem["BHFW"] + "～" + sItem["BHFW"];
                                }
                                else { sItem["GBHPC"] = "-" + "0.4" + "～" + "0.4"; }
                            }
                            if (GetSafeDouble(sItem["BHFW"]) / GetSafeDouble(sItem["ZJFW"]) > 0.05 && GetSafeDouble(sItem["BHFW"]) / GetSafeDouble(sItem["ZJFW"]) <= 0.10)
                            {
                                if (GetSafeDouble(sItem["BHFW"]) * 0.125 > 0.4)
                                {
                                    sItem["GBHPC"] = "-" + sItem["BHFW"] + "～" + sItem["BHFW"];
                                }
                                else { sItem["GBHPC"] = "-" + "0.4" + "～" + "0.4"; }
                            }
                            if (GetSafeDouble(sItem["BHFW"]) / GetSafeDouble(sItem["ZJFW"]) > 0.10)
                            {
                                sItem["GBHPC"] = "-" + 0.10 * GetSafeDouble(sItem["BHFW"]) + "～" + 0.125 * GetSafeDouble(sItem["BHFW"]);
                            }
                        }

                    }
                    if (sItem["GGLX"] == "热扩钢管") { sItem["GBHPC"] = "-" + 0.15 * GetSafeDouble(sItem["BHFW"]) + "～" + 0.15 * GetSafeDouble(sItem["BHFW"]); }
                    if (sItem["GGLX"] == "冷轧钢管" || sItem["GGLX"] == "冷扩钢管")
                    {
                        if (GetSafeDouble(sItem["BHFW"]) <= 3)
                        {
                            if (GetSafeDouble(sItem["BHFW"]) * 0.15 > 0.15)
                            {
                                sItem["GBHPC"] = "-" + 0.10 * GetSafeDouble(sItem["BHFW"]) + "～" + 0.15 * GetSafeDouble(sItem["BHFW"]);
                            }
                            else { sItem["GBHPC"] = "-" + "0.15" + "～" + "0.15"; }

                        }
                        if (GetSafeDouble(sItem["BHFW"]) > 3 && GetSafeDouble(sItem["BHFW"]) <= 10)
                        {
                            sItem["GBHPC"] = "-" + 0.10 * GetSafeDouble(sItem["BHFW"]) + "～" + 0.125 * GetSafeDouble(sItem["BHFW"]);

                        }
                        if (GetSafeDouble(sItem["BHFW"]) > 10)
                        {
                            sItem["GBHPC"] = "-" + 0.10 * GetSafeDouble(sItem["BHFW"]) + "～" + 0.10 * GetSafeDouble(sItem["BHFW"]);

                        }

                    }
                    #endregion

                    sItem["GGBH1"] = Round((GetSafeDouble(sItem["GGBH1_1"]) + GetSafeDouble(sItem["GGBH1_2"]) + GetSafeDouble(sItem["GGBH1_3"]) + GetSafeDouble(sItem["GGBH1_4"])) / 4, 2).ToString("0.00");
                    sItem["GGBH2"] = Round((GetSafeDouble(sItem["GGBH2_1"]) + GetSafeDouble(sItem["GGBH2_2"]) + GetSafeDouble(sItem["GGBH2_3"]) + GetSafeDouble(sItem["GGBH2_4"])) / 4, 2).ToString("0.00");
                    sItem["GGBHPC1"] = Round(GetSafeDouble(sItem["GGBH1"]) - GetSafeDouble(sItem["BHFW"]), 2).ToString("0.00");
                    sItem["GGBHPC2"] = Round(GetSafeDouble(sItem["GGBH2"]) - GetSafeDouble(sItem["BHFW"]), 2).ToString("0.00");
                    sItem["BHPD"] = IsQualified(sItem["GBHPC"], sItem["GGBHPC1"], false);

                    if (sItem["BHPD"] == "合格")
                    {
                        sItem["BHPD"] = IsQualified(sItem["GBHPC"], sItem["GGBHPC2"], false);
                        mbHggs++;

                    }
                    else
                    {
                        sItem["BHPD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        itemHG = false;
                    }

                    #endregion
                }

                #endregion


                #region 压扁    
                if (jcxm.Contains("、压扁、"))
                {
                    for (int i = 1; i < 3; i++)
                    {
                        if (sItem["YB" + i] == "无裂纹")
                        {
                            sItem["YB" + i] = "1";
                        }
                        if (sItem["YB" + i] == "有裂纹")
                        {
                            sItem["YB" + i] = "0";
                        }
                        if (sItem["YB" + i] == "----")
                        {
                            sItem["YB" + i] = "-1";
                        }

                    }
                    jcxmCur = "压扁";
                    if (sItem["YB1"].Trim() != "0" && sItem["YB2"].Trim() != "0")
                    {
                        sItem["HG_YB"] = "合格";
                        mbHggs++;
                    }
                    else
                    {
                        sItem["HG_YB"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                }
                else
                {
                    sItem["HG_YB"] = "----";
                    sItem["YB1"] = "----";
                    sItem["YB2"] = "----";
                    sItem["G_YB"] = "----";
                }
                #endregion

                for (int i = 1; i < 3; i++)
                {
                    if (sItem["YB" + i] == "1")
                    {
                        sItem["YB" + i] = "无裂纹";
                    }
                    if (sItem["YB" + i] == "0")
                    {
                        sItem["YB" + i] = "有裂纹";
                    }
                    if (sItem["YB" + i] == "-1")
                    {
                        sItem["YB" + i] = "----";
                    }

                }

                #region  弯曲
                if (jcxm.Contains("、弯曲、"))
                {
                    jcxmCur = "弯曲";
                    for (int i = 2; i < 3; i++)
                    {
                        if (sItem["LW" + i] == "无裂纹")
                        {
                            sItem["LW" + i] = "1";
                        }
                        if (sItem["LW" + i] == "有裂纹")
                        {
                            sItem["LW" + i] = "0";
                        }
                        if (sItem["LW" + i] == "----")
                        {
                            sItem["LW" + i] = "-1";
                        }

                    }
                    if (sItem["LW"] == "无裂纹")
                    {
                        sItem["LW"] = "1";
                    }
                    if (sItem["LW"] == "有裂纹")
                    {
                        sItem["LW"] = "0";
                    }
                    if (sItem["LW"] == "----")
                    {
                        sItem["LW"] = "-1";
                    }

                    if (SFlg)
                    {
                        sItem["HG_LW"] = sItem["LW"].Trim() != "0" && sItem["LW2"].Trim() != "0" ? "合格" : "不合格";
                        if (sItem["HG_LW"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                    else
                    {
                        sItem["HG_LW"] = sItem["LW"].Trim() != "0" ? "合格" : "不合格";
                        if (sItem["HG_LW"] == "不合格")
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            mAllHg = false;
                            itemHG = false;
                        }
                        else
                        {
                            mbHggs++;
                        }
                    }
                }
                else
                {
                    sItem["HG_LW"] = "----";
                    sItem["LW"] = "----";
                    sItem["LW2"] = "----";
                    sItem["G_LW"] = "----";
                }
                #endregion
                for (int i = 2; i < 3; i++)
                {
                    if (sItem["LW" + i] == "1")
                    {
                        sItem["LW" + i] = "无裂纹";
                    }
                    if (sItem["LW" + i] == "0")
                    {
                        sItem["LW" + i] = "有裂纹";
                    }
                    if (sItem["LW" + i] == "-1")
                    {
                        sItem["LW" + i] = "----";
                    }

                }
                if (sItem["LW"] == "1")
                {
                    sItem["LW"] = "无裂纹";
                }
                if (sItem["LW"] == "0")
                {
                    sItem["LW"] = "有裂纹";
                }
                if (sItem["LW"] == "-1")
                {
                    sItem["LW"] = "----";
                }

                #region 拉伸
                if (jcxm.Contains("、拉伸、"))
                {
                    jcxmCur = "拉伸";
                    var WJ1 = GetSafeDouble(sItem["GGWJ1"]);
                    var BH1 = GetSafeDouble(sItem["GGBH1"]);
                    var WJ2 = GetSafeDouble(sItem["GGWJ2"]);
                    var BH2 = GetSafeDouble(sItem["GGBH2"]);
                    var MJ1 = 3.14159 * (Math.Pow((WJ1 / 2), 2) - Math.Pow((WJ1 - BH1) / 2, 2));
                    var MJ2 = 3.14159 * (Math.Pow((WJ2 / 2), 2) - Math.Pow((WJ2 - BH2) / 2, 2));

                    #region 抗拉强度
                    sItem["KLQD"] = (Round(1000 * GetSafeDouble(sItem["KLHZ"]) / MJ1 / 5, 0) * 5).ToString("0");
                    sItem["KLQD2"] = (Round(1000 * GetSafeDouble(sItem["KLHZ2"]) / MJ2 / 5, 0) * 5).ToString("0");
                    if (SFlg)
                    {
                        sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false) == "合格" && IsQualified(sItem["G_KLQD"], sItem["KLQD2"], false) == "合格" ? "合格" : "不合格";
                    }
                    else { sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false); }

                    if (sItem["HG_KLQD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "抗拉强度" + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        mbHggs++;
                    }



                    #endregion

                    #region 屈服强度
                    sItem["QFQD"] = (Round(1000 * GetSafeDouble(sItem["QFHZ"]) / MJ1 / 5, 0) * 5).ToString("0");
                    sItem["QFQD2"] = (Round(1000 * GetSafeDouble(sItem["QFHZ2"]) / MJ2 / 5, 0) * 5).ToString("0");
                    if (SFlg)
                    {
                        sItem["HG_QFQD"] = IsQualified(sItem["G_QFQD"], sItem["QFQD"], false) == "合格" && IsQualified(sItem["G_QFQD"], sItem["QFQD2"], false) == "合格" ? "合格" : "不合格";
                    }
                    else { sItem["HG_QFQD"] = IsQualified(sItem["G_QFQD"], sItem["QFQD"], false); }

                    if (sItem["HG_QFQD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                    #endregion

                    #region 伸长率

                    var YSBJ = Round(5.65 * Math.Sqrt(MJ1), 0);
                    YSBJ = 5 * (Math.Floor(0.5 + YSBJ / 5));
                    sItem["YSBJ"] = YSBJ.ToString("0");
                    var SCZ = GetSafeDouble(sItem["SCZ"]);
                    var SCL = (SCZ - YSBJ) / YSBJ * 100;
                    if (SCL > 10)
                    {
                        //修约到%0.5
                        SCL = Math.Round(0.5 * (Math.Floor(0.5 + SCL / 0.5)), 1);
                    }
                    sItem["SCL"] = SCL.ToString("0");

                    var YSBJ2 = Round(5.65 * Math.Sqrt(MJ1), 0);
                    YSBJ2 = 5 * (Math.Floor(0.5 + YSBJ / 5));
                    sItem["YSBJ2"] = YSBJ.ToString("0");
                    var SCZ2 = GetSafeDouble(sItem["SCZ2"]);
                    var SCL2 = (SCZ - YSBJ) / YSBJ * 100;
                    if (SCL2 > 10)
                    {
                        //修约到%0.5
                        SCL2 = Math.Round(0.5 * (Math.Floor(0.5 + SCL2 / 0.5)), 1);
                    }
                    sItem["SCL2"] = SCL2.ToString("0");
                    if (SFlg)
                    {
                        sItem["HG_SCL"] = IsQualified(sItem["G_SCL"], sItem["SCL"], false) == "合格" && IsQualified(sItem["G_SCL"], sItem["SCL2"], false) == "合格" ? "合格" : "不合格";
                    }
                    else { sItem["HG_SCL"] = IsQualified(sItem["G_SCL"], sItem["SCL"], false); }

                    if (sItem["HG_SCL"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "伸长率" + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                    #endregion


                }
                else
                {
                    sItem["HG_SCL"] = "----";
                    sItem["SCL"] = "----";
                    sItem["SCL2"] = "----";
                    sItem["G_SCL"] = "----";
                    sItem["G_KLQD"] = "----";
                    sItem["KLQD"] = "----";
                    sItem["KLQD2"] = "----";
                    sItem["HG_KLQD"] = "----";
                    sItem["G_QFQD"] = "----";
                    sItem["QFQD"] = "----";
                    sItem["QFQD2"] = "----";
                    sItem["HG_QFQD"] = "----";
                }
                #endregion

                if (itemHG)
                {
                    sItem["JCJG"] = "合格";
                }
                else
                {
                    sItem["JCJG"] = "不合格";
                }

            }
            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。";
            }
            else
            {
                if (mbHggs > 0)
                {
                    jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion

            /************************ 代码结束 *********************/
        }

        public void DH()
        {
            #region 德浩
            #region 集合取值
            var extraDJ = dataExtra["BZ_GX_DJ"];
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            var jcjg = "";
            var jcxmCur = "";
            var jcxmBhg = "";
            var S_GXS = data["S_GX"];
            if (!data.ContainsKey("M_GX"))
            {
                data["M_GX"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_GX"];
            #endregion
            bool itemHG = true;//判断单组是否合格
            int mbHggs = 0;//检测项目合格数量,总体不合格情况下，部分项目合格统计
            var ggph = "";
            #region 获取不合格数据

            foreach (var sItem in S_GXS)
            {
                ggph = sItem["GGPH"];
                var jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";

                #region 尺寸偏差

                if (jcxm.Contains("、尺寸偏差、")) {
                   
                    if( sItem["WJPD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "外径" + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else { sItem["WJPD"] = "合格"; mbHggs++; }
                    if (sItem["BHPD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "壁厚" + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else { sItem["BHPD"] = "合格"; mbHggs++; }

                }

                #endregion

                #region 压扁
                if (jcxm.Contains("、压扁、"))
                {
                    if(sItem["YBPD"] == "不合格")
                    { 
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                    }
                    else
                    {
                        sItem["HG_YB"] = "合格";
                        mbHggs++;
                    }
                   
                }

                #endregion

                #region  弯曲
                if (jcxm.Contains("、弯曲、"))
                {
                    if (sItem["HG_LW"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        mbHggs++;
                    }

                }
                #endregion

                if (jcxm.Contains("、拉伸、"))
                {
                    if (sItem["HG_KLQD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "抗拉强度" + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                    if (sItem["HG_QFQD"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        mbHggs++;
                    }

                    if (sItem["HG_SCL"] == "不合格")
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "伸长率" + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        mbHggs++;
                    }
                }

            }
            //添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "该组试样所检项目符合" + MItem[0]["PDBZ"] +"钢管牌号"+ggph+ " 标准要求。";
            }
            else
            {
                if (mbHggs > 0)
                {
                    jsbeizhu = "该组试样所检项目部分符合" + MItem[0]["PDBZ"] + "钢管牌号" + ggph + " 标准要求。";
                }
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + ggph + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
        }
    } 
}

