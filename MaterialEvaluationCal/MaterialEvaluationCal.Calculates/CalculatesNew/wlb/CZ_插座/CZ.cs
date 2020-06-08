using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class CZ : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_CZ_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_CZS = data["S_CZ"];
            int mbHggs = 0;//记录合格数量
            if (!data.ContainsKey("M_CZ"))
            {
                data["M_CZ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_CZ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            string jcxm = "";
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in S_CZS)
            {
                itemHG = true;
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["GGXH"].Trim() == sItem["GG"]);
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "不下结论";
                    mjcjg = "不下结论";
                    continue;
                }
                else
                {
                    MItem[0]["G_BZ"] = extraFieldsDj["BZ"] == null ? "" : extraFieldsDj["BZ"];
                    MItem[0]["G_CC"] = extraFieldsDj["CC"] == null ? "" : extraFieldsDj["CC"];
                    MItem[0]["G_DBH"] = extraFieldsDj["DBH"] == null ? "" : extraFieldsDj["DBH"];
                    MItem[0]["G_JD1"] = extraFieldsDj["JD1"] == null ? "" : extraFieldsDj["JD1"];
                    MItem[0]["G_JD2"] = extraFieldsDj["JD2"] == null ? "" : extraFieldsDj["JD2"];
                    MItem[0]["G_FC"] = extraFieldsDj["FC"] == null ? "" : extraFieldsDj["FC"];
                    MItem[0]["G_JYDZ"] = extraFieldsDj["JYDZ"] == null ? "" : extraFieldsDj["JYDZ"];
                    MItem[0]["G_BCL1"] = extraFieldsDj["BCL1"] == null ? "" : extraFieldsDj["BCL1"];
                    MItem[0]["G_BCL2"] = extraFieldsDj["BCL2"] == null ? "" : extraFieldsDj["BCL2"];
                    MItem[0]["G_BCL3"] = extraFieldsDj["BCL3"] == null ? "" : extraFieldsDj["BCL3"];
                    MItem[0]["G_BCL4"] = extraFieldsDj["BCL4"] == null ? "" : extraFieldsDj["BCL4"];
                    MItem[0]["G_JG1"] = extraFieldsDj["JG1"] == null ? "" : extraFieldsDj["JG1"];
                    MItem[0]["G_JG2"] = extraFieldsDj["JG2"] == null ? "" : extraFieldsDj["JG2"];
                    MItem[0]["G_DQD"] = extraFieldsDj["DQD"] == null ? "" : extraFieldsDj["DQD"];
                    MItem[0]["G_WS"] = extraFieldsDj["WS"] == null ? "" : extraFieldsDj["WS"];
                    MItem[0]["G_FDRL"] = extraFieldsDj["FDRL"] == null ? "" : extraFieldsDj["FDRL"];
                    MItem[0]["G_ZCCZ1"] = extraFieldsDj["ZCCZ1"] == null ? "" : extraFieldsDj["ZCCZ1"];
                    MItem[0]["G_ZCCZ2"] = extraFieldsDj["ZCCZ2"] == null ? "" : extraFieldsDj["ZCCZ2"];
                    MItem[0]["G_ZCCZ3"] = extraFieldsDj["ZCCZ3"] == null ? "" : extraFieldsDj["ZCCZ3"];
                    MItem[0]["G_PDJL"] = extraFieldsDj["PDJL"] == null ? "" : extraFieldsDj["PDJL"];
                    MItem[0]["G_JX"] = extraFieldsDj["JX"] == null ? "" : extraFieldsDj["JX"];
                    MItem[0]["G_JYQH"] = extraFieldsDj["JYQH"] == null ? "" : extraFieldsDj["JYQH"];
                    MItem[0]["G_NR1"] = extraFieldsDj["NR1"] == null ? "" : extraFieldsDj["NR1"];
                    MItem[0]["G_NR2"] = extraFieldsDj["NR2"] == null ? "" : extraFieldsDj["NR2"];
                    MItem[0]["G_NR3"] = extraFieldsDj["NR3"] == null ? "" : extraFieldsDj["NR3"];
                    MItem[0]["G_JXQD"] = extraFieldsDj["JXQD"] == null ? "" : extraFieldsDj["JXQD"];
                    MItem[0]["G_LD1"] = extraFieldsDj["LD1"] == null ? "" : extraFieldsDj["LD1"];
                    MItem[0]["G_LD2"] = extraFieldsDj["LD2"] == null ? "" : extraFieldsDj["LD2"];
                    MItem[0]["G_NLH"] = extraFieldsDj["NLH"] == null ? "" : extraFieldsDj["NLH"];
                    MItem[0]["G_DZ"] = extraFieldsDj["DZ"] == null ? "" : extraFieldsDj["DZ"];
                }

                //旋转裂纹、旋转盖板、旋转缩松、旋转抗滑、旋转破坏、旋转力矩、旋转一般项、对接裂纹、对接盖板、对接缩松、对接抗拉、对接力矩、对接一般项
                #region  标志
                if (jcxm.Contains("、标志、"))
                {
                    jcxmCur = "标志";

                    if (3 > GetSafeDouble(MItem[0]["BZ"]) && 0 <= GetSafeDouble(MItem[0]["BZ"]))
                    {
                        MItem[0]["BZ_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["BZ_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["BZ"])
                    {
                        MItem[0]["BZ_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["BZ"] = "-1";
                    MItem[0]["BZ_HG"] = "----";
                }
                #endregion

                #region  尺寸
                if (jcxm.Contains("、尺寸、"))
                {
                    jcxmCur = "尺寸";
                    if (3 > GetSafeDouble(MItem[0]["CC"]) && 0 <= GetSafeDouble(MItem[0]["BZ"]))
                    {
                        MItem[0]["CC_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["CC_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["CC"])
                    {
                        MItem[0]["CC_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["CC"] = "-1";
                    MItem[0]["CC_HG"] = "----";
                }
                #endregion

                #region  防触电保护
                if (jcxm.Contains("、防触电保护、"))
                {
                    jcxmCur = "防触电保护";
                    if (3 > GetSafeDouble(MItem[0]["DBH"]) && 0 <= GetSafeDouble(MItem[0]["DBH"]))
                    {
                        MItem[0]["DBH_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["DBH_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["DBH"])
                    {
                        MItem[0]["DBH_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["DBH"] = "-1";
                    MItem[0]["DBH_HG"] = "----";
                }
                #endregion

                #region  防潮
                if (jcxm.Contains("、防潮、"))
                {
                    jcxmCur = "防潮";
                    if (3 > GetSafeDouble(MItem[0]["FC"]) && 0 <= GetSafeDouble(MItem[0]["FC"]))
                    {
                        MItem[0]["FC_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["FC_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["FC"])
                    {
                        MItem[0]["FC_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["FC"] = "-1";
                    MItem[0]["FC_HG"] = "----";
                }
                #endregion

                #region  端子
                if (jcxm.Contains("、端子、"))
                {
                    jcxmCur = "端子";
                    if (3 > GetSafeDouble(MItem[0]["DZ"]) && 0 <= GetSafeDouble(MItem[0]["DZ"]))
                    {
                        MItem[0]["DZ_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["DZ_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["DZ"])
                    {
                        MItem[0]["DZ_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["DZ"] = "-1";
                    MItem[0]["DZ_HG"] = "----";
                }
                #endregion

                #region  爬电距离
                if (jcxm.Contains("、爬电距离、"))
                {
                    jcxmCur = "爬电距离";
                    if (3 > GetSafeDouble(MItem[0]["PDJL"]) && 0 <= GetSafeDouble(MItem[0]["PDJL"]))
                    {
                        MItem[0]["PDJL_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["PDJL_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["PDJL"])
                    {
                        MItem[0]["PDJL_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["PDJL"] = "-1";
                    MItem[0]["PDJL_HG"] = "----";
                }
                #endregion

                #region  分断容量
                if (jcxm.Contains("、分断容量、"))
                {
                    jcxmCur = "分断容量";
                    if (3 > GetSafeDouble(MItem[0]["FDRL"]) && 0 <= GetSafeDouble(MItem[0]["FDRL"]))
                    {
                        MItem[0]["FDRL_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["FDRL_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["FDRL"])
                    {
                        MItem[0]["FDRL_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["FDRL"] = "-1";
                    MItem[0]["FDRL_HG"] = "----";
                }
                #endregion

                #region  结构
                if (jcxm.Contains("、结构、"))
                {
                    jcxmCur = "结构";
                    if (3 > GetSafeDouble(MItem[0]["JG1"]) && 0 <= GetSafeDouble(MItem[0]["JG1"]))
                    {
                        MItem[0]["JG1_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["JG1_HG"] = "合格";
                    }

                    if (3 > GetSafeDouble(MItem[0]["JG2"]) && 0 <= GetSafeDouble(MItem[0]["JG2"]))
                    {
                        MItem[0]["JG2_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["JG2_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["JG1"])
                    {
                        MItem[0]["JG1_HG"] = "----";
                    }

                    if ("-1" == MItem[0]["JG2"])
                    {
                        MItem[0]["JG2_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["JG1"] = "-1";
                    MItem[0]["JG1_HG"] = "----";
                    MItem[0]["JG2"] = "-1";
                    MItem[0]["JG2_HG"] = "----";
                }
                #endregion

                #region  电气强度
                if (jcxm.Contains("、电气强度、"))
                {
                    jcxmCur = "电气强度";
                    if (3 > GetSafeDouble(MItem[0]["DQD1"]) && 0 <= GetSafeDouble(MItem[0]["DQD1"]))
                    {
                        MItem[0]["DQD1_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["DQD1_HG"] = "合格";
                    }

                    if (3 > GetSafeDouble(MItem[0]["DQD2"]) && 0 <= GetSafeDouble(MItem[0]["DQD2"]))
                    {
                        MItem[0]["DQD2_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["DQD2_HG"] = "合格";
                        mbHggs++;
                    }

                    if (3 > GetSafeDouble(MItem[0]["DQD3"]) && 0 <= GetSafeDouble(MItem[0]["DQD3"]))
                    {
                        MItem[0]["DQD3_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["DQD3_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["DQD1"])
                    {
                        MItem[0]["DQD1_HG"] = "----";
                    }

                    if ("-1" == MItem[0]["DQD2"])
                    {
                        MItem[0]["DQD2_HG"] = "----";
                    }

                    if ("-1" == MItem[0]["DQD3"])
                    {
                        MItem[0]["DQD3_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["DQD1"] = "-1";
                    MItem[0]["DQD1_HG"] = "----";
                    MItem[0]["DQD2"] = "-1";
                    MItem[0]["DQD2_HG"] = "----";
                    MItem[0]["DQD3"] = "-1";
                    MItem[0]["DQD3_HG"] = "----";
                }
                #endregion

                #region  温升
                if (jcxm.Contains("、温升、"))
                {
                    jcxmCur = "温升";
                    if (3 > GetSafeDouble(MItem[0]["WS"]) && 0 <= GetSafeDouble(MItem[0]["WS"]))
                    {
                        MItem[0]["WS_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["WS_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["WS"])
                    {
                        MItem[0]["WS_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["WS"] = "-1";
                    MItem[0]["WS_HG"] = "----";
                }
                #endregion

                #region  耐老化
                if (jcxm.Contains("、耐老化、"))
                {
                    jcxmCur = "耐老化";
                    if (3 > GetSafeDouble(MItem[0]["NLH"]) && 0 <= GetSafeDouble(MItem[0]["NLH"]))
                    {
                        MItem[0]["NLH_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["NLH_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["NLH"])
                    {
                        MItem[0]["NLH_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["NLH"] = "-1";
                    MItem[0]["NLH_HG"] = "----";
                }
                #endregion

                #region  绝缘电阻
                if (jcxm.Contains("、绝缘电阻、"))
                {
                    jcxmCur = "绝缘电阻";
                    if (3 > GetSafeDouble(MItem[0]["JYDZ1"]) && 0 <= GetSafeDouble(MItem[0]["JYDZ1"]))
                    {
                        MItem[0]["JYDZ1_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["JYDZ1_HG"] = "合格";
                    }

                    if (3 > GetSafeDouble(MItem[0]["JYDZ2"]) && 0 <= GetSafeDouble(MItem[0]["JYDZ2"]))
                    {
                        MItem[0]["JYDZ2_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["JYDZ2_HG"] = "合格";
                    }

                    if (3 > GetSafeDouble(MItem[0]["JYDZ3"]) && 0 <= GetSafeDouble(MItem[0]["JYDZ3"]))
                    {
                        MItem[0]["JYDZ3_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["JYDZ3_HG"] = "合格";
                        mbHggs++;
                    }

                    if ("-1" == MItem[0]["JYDZ1"])
                    {
                        MItem[0]["JYDZ1_HG"] = "----";
                    }

                    if ("-1" == MItem[0]["JYDZ2"])
                    {
                        MItem[0]["JYDZ2_HG"] = "----";
                    }

                    if ("-1" == MItem[0]["JYDZ3"])
                    {
                        MItem[0]["JYDZ3_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["JYDZ1"] = "-1";
                    MItem[0]["JYDZ1_HG"] = "----";
                    MItem[0]["JYDZ2"] = "-1";
                    MItem[0]["JYDZ2_HG"] = "----";
                    MItem[0]["JYDZ3"] = "-1";
                    MItem[0]["JYDZ3_HG"] = "----";
                }
                #endregion

                #region  接地措施
                if (jcxm.Contains("、接地措施、"))
                {
                    jcxmCur = "接地措施";
                    if (3 > GetSafeDouble(MItem[0]["JD1"]) && 0 <= GetSafeDouble(MItem[0]["JD1"]))
                    {
                        MItem[0]["JD1_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["JD1_HG"] = "合格";
                    }

                    if (3 > GetSafeDouble(MItem[0]["JD2"]) && 0 <= GetSafeDouble(MItem[0]["JD2"]))
                    {
                        MItem[0]["JD2_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["JD2_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["JD1"])
                    {
                        MItem[0]["JD1_HG"] = "----";
                    }

                    if ("-1" == MItem[0]["JD2"])
                    {
                        MItem[0]["JD2_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["JD1"] = "-1";
                    MItem[0]["JD1_HG"] = "----";
                    MItem[0]["JD2"] = "-1";
                    MItem[0]["JD2_HG"] = "----";
                }
                #endregion

                #region  拔出力
                if (jcxm.Contains("、拔出力、"))
                {
                    jcxmCur = "拔出力";
                    if (null == MItem[0]["BCL1"])
                    {
                        MItem[0]["BCL1"] = "-1";
                    }
                    if (null == MItem[0]["BCL2"])
                    {
                        MItem[0]["BCL2"] = "-1";
                    }
                    if (null == MItem[0]["BCL3"])
                    {
                        MItem[0]["BCL3"] = "-1";
                    }
                    if (null == MItem[0]["BCL4"])
                    {
                        MItem[0]["BCL4"] = "-1";
                    }

                    if (3 > GetSafeDouble(MItem[0]["BCL1"]) && 0 <= GetSafeDouble(MItem[0]["BCL1"]))
                    {
                        MItem[0]["BCL1_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["BCL1_HG"] = "合格";
                    }

                    if (3 > GetSafeDouble(MItem[0]["BCL2"]) && 0 <= GetSafeDouble(MItem[0]["BCL2"]))
                    {
                        MItem[0]["BCL2_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["BCL2_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["BCL1"])
                    {
                        MItem[0]["BCL1_HG"] = "----";
                        MItem[0]["G_BCL1"] = "----";
                    }
                    if ("-1" == MItem[0]["BCL2"])
                    {
                        MItem[0]["BCL2_HG"] = "----";
                        MItem[0]["G_BCL2"] = "----";
                    }
                    if ("" == MItem[0]["G_BCL1"])
                    {
                        MItem[0]["BCL1_HG"] = "----";
                        MItem[0]["G_BCL1"] = "----";
                        MItem[0]["BCL1"] = "-1";
                    }
                }
                else
                {
                    MItem[0]["BCL1"] = "-1";
                    MItem[0]["BCL1_HG"] = "----";
                    MItem[0]["BCL2"] = "-1";
                    MItem[0]["BCL2_HG"] = "----";
                    //MItem[0]["BCL3"] = "-1";
                    //MItem[0]["BCL3_HG"] = "----";
                    //MItem[0]["BCL4"] = "-1";
                    //MItem[0]["BCL4_HG"] = "----";
                }
                #endregion

                #region  电气间隙
                if (jcxm.Contains("、电气间隙、"))
                {
                    jcxmCur = "电气间隙";
                    if (3 > GetSafeDouble(MItem[0]["JX"]) && 0 <= GetSafeDouble(MItem[0]["JX"]))
                    {
                        MItem[0]["JX_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["JX_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["JX"])
                    {
                        MItem[0]["JX_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["JX"] = "-1";
                    MItem[0]["JX_HG"] = "----";
                }
                #endregion

                #region  正常操作
                if (jcxm.Contains("、正常操作、") || jcxm.Contains("、插座正常操作次数、"))
                {
                    jcxmCur = CurrentJcxm(jcxm, "正常操作,插座正常操作次数");

                    if (3 > GetSafeDouble(MItem[0]["ZCCZ1"]) && 0 <= GetSafeDouble(MItem[0]["ZCCZ1"]))
                    {
                        MItem[0]["ZCCZ1_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["ZCCZ1_HG"] = "合格";
                        mbHggs++;
                    }

                    if ("-1" == MItem[0]["ZCCZ1"])
                    {
                        MItem[0]["ZCCZ1_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["ZCCZ1"] = "-1";
                    MItem[0]["ZCCZ1_HG"] = "----";
                }
                #endregion

                #region  耐热
                if (jcxm.Contains("、耐热、"))
                {
                    jcxmCur = "耐热";

                    if (3 > GetSafeDouble(MItem[0]["NR1"]) && 0 <= GetSafeDouble(MItem[0]["NR1"]))
                    {
                        MItem[0]["NR1_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["NR1_HG"] = "合格";
                    }

                    if (3 > GetSafeDouble(MItem[0]["NR2"]) && 0 <= GetSafeDouble(MItem[0]["NR2"]))
                    {
                        MItem[0]["NR2_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["NR2_HG"] = "合格";
                    }

                    if (3 > GetSafeDouble(MItem[0]["NR3"]) && 0 <= GetSafeDouble(MItem[0]["NR3"]))
                    {
                        MItem[0]["NR3_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["NR3_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["NR1"])
                    {
                        MItem[0]["NR1_HG"] = "----";
                    }

                    if ("-1" == MItem[0]["NR2"])
                    {
                        MItem[0]["NR2_HG"] = "----";
                    }

                    if ("-1" == MItem[0]["NR3"])
                    {
                        MItem[0]["NR3_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["NR1"] = "-1";
                    MItem[0]["NR1_HG"] = "----";
                    MItem[0]["NR2"] = "-1";
                    MItem[0]["NR2_HG"] = "----";
                    MItem[0]["NR3"] = "-1";
                    MItem[0]["NR3_HG"] = "----";
                }
                #endregion

                #region  截流部件及连接
                if (jcxm.Contains("、截流部件及连接、"))
                {
                    jcxmCur = "截流部件及连接";

                    if (3 > GetSafeDouble(MItem[0]["LD1"]) && 0 <= GetSafeDouble(MItem[0]["LD1"]))
                    {
                        MItem[0]["LD1_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["LD1_HG"] = "合格";
                    }

                    if (3 > GetSafeDouble(MItem[0]["LD2"]) && 0 <= GetSafeDouble(MItem[0]["LD2"]))
                    {
                        MItem[0]["LD2_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["LD2_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["LD1"])
                    {
                        MItem[0]["LD1_HG"] = "----";
                    }

                    if ("-1" == MItem[0]["LD2"])
                    {
                        MItem[0]["LD2_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["LD1"] = "-1";
                    MItem[0]["LD1_HG"] = "----";
                    MItem[0]["LD2"] = "-1";
                    MItem[0]["LD2_HG"] = "----";
                }
                #endregion

                #region  耐漏电起痕
                if (jcxm.Contains("、耐漏电起痕、"))
                {
                    jcxmCur = "耐漏电起痕";
                    if (3 > GetSafeDouble(MItem[0]["JYQH"]) && 0 <= GetSafeDouble(MItem[0]["JYQH"]))
                    {
                        MItem[0]["JYQH_HG"] = "不合格";
                        mAllHg = false;
                        mbHggs++;
                        itemHG = false;
                    }
                    else
                    {
                        MItem[0]["JYQH_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["JYQH"])
                    {
                        MItem[0]["JYQH_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["JYQH"] = "-1";
                    MItem[0]["JYQH_HG"] = "----";
                }
                #endregion

                #region  机械强度
                if (jcxm.Contains("、机械强度、"))
                {
                    jcxmCur = "机械强度";
                    if (3 > GetSafeDouble(MItem[0]["JXQD"]) && 0 <= GetSafeDouble(MItem[0]["JXQD"]))
                    {
                        MItem[0]["JXQD_HG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["JXQD_HG"] = "合格";
                    }

                    if ("-1" == MItem[0]["JXQD"])
                    {
                        MItem[0]["JXQD_HG"] = "----";
                    }
                }
                else
                {
                    MItem[0]["JXQD"] = "-1";
                    MItem[0]["JXQD_HG"] = "----";
                }
                #endregion
                //单组判断
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
            if (mAllHg)
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";

                if (mjcjg == "不下结论")
                {
                    jsbeizhu = "找不到对应标准。";
                }
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
