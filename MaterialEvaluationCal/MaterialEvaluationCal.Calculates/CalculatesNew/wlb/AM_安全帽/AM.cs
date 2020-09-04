using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
/*安全帽*/
namespace Calculates
{
    public class AM : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_AM_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_AMS = data["S_AM"];
            if (!data.ContainsKey("M_AM"))
            {
                data["M_AM"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_AM"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            string mJSFF = "";
            int mHggs = 0;//统计合格数量
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in S_AMS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["MC"] == sItem["MLX"].Trim());
                if (extraFieldsDj != null)
                {
                    mJSFF = (string.IsNullOrEmpty(extraFieldsDj["JSFF"]) ? "" : extraFieldsDj["JSFF"]).ToLower();
                }
                else
                {
                    mJSFF = "";
                    sItem["JCJG"] = "不合格";
                    mAllHg = false;
                    jsbeizhu = "不合格";
                    continue;
                }

                if (mJSFF == "")
                {
                    #region  冲击吸收性能
                    if (jcxm.Contains("、冲击吸收性能、"))
                    {
                        jcxmCur = "冲击吸收性能";
                        sItem["CJXSYQ"] = extraFieldsDj["CJXS"];
                        if (IsQualified("≤4900", sItem["CJXS1"],true) == "符合")
                        {
                            sItem["CJXS1PD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["CJXS1PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }

                        if (IsQualified("≤4900", sItem["CJXS2"], true) == "符合")
                        {
                            sItem["CJXS2PD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["CJXS2PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }

                        if (IsQualified("≤4900", sItem["CJXS3"], true) == "符合")
                        {
                            sItem["CJXS3PD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["CJXS3PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["CJXSYQ"] = "----";
                        sItem["CJXS1"] = "----";
                        sItem["CJXS2"] = "----";
                        sItem["CJXS3"] = "----";
                        sItem["CJXS1PD"] = "----";
                        sItem["CJXS2PD"] = "----";
                        sItem["CJXS3PD"] = "----";
                    }
                    #endregion

                    #region  耐穿刺性能
                    if (jcxm.Contains("、耐穿刺性能、"))
                    {
                        jcxmCur = "耐穿刺性能";
                        sItem["NCCYQ"] = extraFieldsDj["NCC"];

                        if ("接触头模，无碎片" == sItem["NCC1"] || "接触头模，有碎片" == sItem["NCC1"] || "未接触头模， 有碎片" == sItem["NCC1"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["NCC1PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            sItem["NCC1PD"] = "合格";
                            mHggs++;
                        }

                        if ("接触头模，无碎片" == sItem["NCC2"] || "接触头模，有碎片" == sItem["NCC2"] || "未接触头模， 有碎片" == sItem["NCC2"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["NCC2PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            sItem["NCC2PD"] = "合格";
                            mHggs++;
                        }

                        if ("接触头模，无碎片" == sItem["NCC3"] || "接触头模，有碎片" == sItem["NCC3"] || "未接触头模， 有碎片" == sItem["NCC3"])
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["NCC3PD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                        else
                        {
                            sItem["NCC3PD"] = "合格";
                            mHggs++;
                        }
                    }
                    else
                    {
                        sItem["NCC1"] = "----";
                        sItem["NCC2"] = "----";
                        sItem["NCC3"] = "----";
                        sItem["NCCYQ"] = "----";
                        sItem["NCC1PD"] = "----";
                        sItem["NCC2PD"] = "----";
                        sItem["NCC3PD"] = "----";
                    }
                    #endregion

                    #region  下颏带的强度
                    if (jcxm.Contains("、下颏带的强度、"))
                    {
                        jcxmCur = "下颏带的强度";
                        sItem["XEDDQDYQ"] = extraFieldsDj["XEDDQD"];
                        if (IsQualified(extraFieldsDj["XEDDQD"],sItem["XEDDQD"],true) == "符合")
                        {
                            sItem["XEDDQDPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["XEDDQDPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["XEDDQDPD"] = "----";
                        sItem["XEDDQD"] = "----";
                        sItem["XEDDQDYQ"] = "----";
                    }
                    #endregion

                    #region  电绝缘性能
                    if (jcxm.Contains("、电绝缘性能、"))
                    {
                        jcxmCur = "电绝缘性能";
                        sItem["DJYYQ"] = extraFieldsDj["DJY"];
                        if (sItem["DJY"] == "符合")
                        {
                            sItem["DJYPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["DJYPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["DJYPD"] = "----";
                        sItem["DJYYQ"] = "----";
                        sItem["DJY"] = "----";
                    }
                    #endregion

                    #region  阻燃性能
                    if (jcxm.Contains("、阻燃性能、"))
                    {
                        jcxmCur = "阻燃性能";
                        sItem["ZRYQ"] = extraFieldsDj["ZR"];
                        if (sItem["ZR"] == "符合")
                        {
                            sItem["ZRPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["ZRPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["ZRPD"] = "----";
                        sItem["ZRYQ"] = "----";
                        sItem["ZR"] = "----";
                    }
                    #endregion

                    #region  防静电性能
                    if (jcxm.Contains("、防静电性能、"))
                    {
                        jcxmCur = "防静电性能";
                        sItem["FJDYQ"] = extraFieldsDj["FJD"];
                        if (sItem["FJD"] == "符合")
                        {
                            sItem["FJDPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["FJDPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["FJDPD"] = "----";
                        sItem["FJD"] = "----";
                        sItem["FJDYQ"] = "----";
                    }
                    #endregion

                    #region  侧向刚性
                    if (jcxm.Contains("、侧向刚性、"))
                    {
                        jcxmCur = "侧向刚性";
                        sItem["CXGXYQ"] = extraFieldsDj["CXGX"];
                        if (sItem["CXGX"] == "符合")
                        {
                            sItem["CXGXPD"] = "合格";
                            mHggs++;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                            sItem["CXGXPD"] = "不合格";
                            itemHG = false;
                            mAllHg = false;
                        }
                    }
                    else
                    {
                        sItem["CXGXPD"] = "----";
                        sItem["CXGXYQ"] = "----";
                        sItem["CXGX"] = "----";
                    }
                    #endregion

                    #region  帽舌
                    /*
                     * 要求 10～70mm     2007
                     * 标准变更  ≤70mm  2019
                     */
                    if (jcxm.Contains("、帽舌、"))
                    {
                        jcxmCur = "帽舌";
                        //sItem["MS_YQ"] = "帽舌:10～70mm";
                        //标准变更
                        sItem["MS_YQ"] = "帽舌:≤70mm";
                        if (IsNumeric(sItem["MS_SC"]))
                        {
                            if (GetSafeDouble(sItem["MS_SC"].Trim()) <= 70)
                            {
                                sItem["MS_PD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["MS_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["MS_SC"] = "非数字";
                        }
                    }
                    else
                    {
                        sItem["MS_PD"] = "----";
                        sItem["MS_YQ"] = "----";
                        sItem["MS_SC"] = "----";
                    }
                    #endregion

                    #region  帽沿
                    /*
                     * 要求 ≤70mm
                     */
                    if (jcxm.Contains("、帽沿、"))
                    {
                        jcxmCur = "帽舌";
                        sItem["MY_YQ"] = "帽沿:≤70mm";
                        if (IsNumeric(sItem["MY_SC"]))
                        {
                            if (GetSafeDouble(sItem["MY_SC"].Trim()) <= 70)
                            {
                                sItem["MY_PD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["MY_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["MY_SC"] = "非数字";
                        }
                    }
                    else
                    {
                        sItem["MY_PD"] = "----";
                        sItem["MY_YQ"] = "----";
                        sItem["MY_SC"] = "----";
                    }
                    #endregion

                    #region  佩戴高度
                    /*
                     * 要求 80～90mm
                     * 标准变更 ≥80mm
                     */
                    if (jcxm.Contains("、佩戴高度、"))
                    {
                        jcxmCur = "佩戴高度";
                        //sItem["PDGD_YQ"] = "佩戴高度:80～90mm";
                        sItem["PDGD_YQ"] = "佩戴高度:≥80mm";
                        if (IsNumeric(sItem["PDGD_SC"]))
                        {
                            if (GetSafeDouble(sItem["PDGD_SC"].Trim()) >= 80)
                            {
                                sItem["PDGD_PD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["PDGD_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["PDGD_SC"] = "非数字";
                        }
                    }
                    else
                    {
                        sItem["PDGD_PD"] = "----";
                        sItem["PDGD_YQ"] = "----";
                        sItem["PDGD_SC"] = "----";
                    }
                    #endregion

                    #region  垂直间距
                    /*
                     *  要求 ≤50mm
                     */
                    if (jcxm.Contains("、垂直间距、"))
                    {
                        jcxmCur = "垂直间距";
                        sItem["CZJJ_YQ"] = "垂直间距:≤50mm";
                        if (IsNumeric(sItem["CZJJ_SC"]))
                        {
                            if (GetSafeDouble(sItem["CZJJ_SC"].Trim()) <= 50)
                            {
                                sItem["CZJJ_PD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["CZJJ_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["CZJJ_SC"] = "非数字";
                        }
                    }
                    else
                    {
                        sItem["CZJJ_PD"] = "----";
                        sItem["CZJJ_YQ"] = "----";
                        sItem["CZJJ_SC"] = "----";
                    }
                    #endregion

                    #region  水平间距
                    /*
                     * 要求 5～20mm
                     * 标准变更   ≥6
                     */
                    if (jcxm.Contains("、水平间距、"))
                    {
                        jcxmCur = "水平间距";
                        //sItem["SPJJ_YQ"] = "水平间距:5～20mm";
                        sItem["SPJJ_YQ"] = "水平间距:≥6mm";
                        if (IsNumeric(sItem["SPJJ_SC"]))
                        {
                            if (GetSafeDouble(sItem["SPJJ_SC"].Trim()) <= 20 && GetSafeDouble(sItem["SPJJ_SC"].Trim()) >= 5)
                            {
                                sItem["SPJJ_PD"] = "合格";
                                mHggs++;
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["SPJJ_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["SPJJ_SC"] = "非数字";
                        }
                    }
                    else
                    {
                        sItem["SPJJ_PD"] = "----";
                        sItem["SPJJ_YQ"] = "----";
                        sItem["SPJJ_SC"] = "----";
                    }
                    #endregion

                    #region  质量
                    /*
                     * 普通安全帽   <=430g  防寒安全帽   <=600g
                     */
                    if (jcxm.Contains("、质量、"))
                    {
                        jcxmCur = "质量";
                        //普通安全帽
                        sItem["ZLPT_YQ"] = "质量:≤430g";
                        if (IsNumeric(sItem["ZLPT_SC"]))
                        {
                            if (GetSafeDouble(sItem["ZLPT_SC"].Trim()) <= 430)
                            {
                                sItem["ZLPT_PD"] = "合格";
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["ZLPT_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["ZLPT_SC"] = "非数字";
                        }
                        //防寒安全帽
                        sItem["ZLFH_YQ"] = "质量:≤600g";
                        if (IsNumeric(sItem["ZLFH_SC"]))
                        {
                            if (GetSafeDouble(sItem["ZLFH_SC"].Trim()) <= 600)
                            {
                                sItem["ZLFH_PD"] = "合格";
                            }
                            else
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                sItem["ZLFH_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["ZLFH_SC"] = "非数字";
                        }

                        if (sItem["ZLFH_PD"] == "合格" && sItem["ZLPT_PD"] == "合格")
                        {
                            mHggs++;
                        }
                    }
                    else
                    {
                        sItem["ZLPT_PD"] = "----";
                        sItem["ZLPT_YQ"] = "----";
                        sItem["ZLPT_SC"] = "----";
                        sItem["ZLFH_PD"] = "----";
                        sItem["ZLFH_YQ"] = "----";
                        sItem["ZLFH_SC"] = "----";
                    }
                    #endregion

                    #region  帽壳内部尺寸
                    /*
                     * 标准要求   长195～250mm  宽170～220mm   高120～150mm
                     */
                    if (jcxm.Contains("、帽壳内部尺寸-新标准不做该指标废弃、"))
                    {
                        jcxmCur = "帽壳内部尺寸";
                        //内部尺寸长
                        sItem["NBCC_CYQ"] = "帽壳内部长：195～250mm";
                        if (IsNumeric(sItem["NBCCC_SC"]))
                        {
                            if (GetSafeDouble(sItem["NBCCC_SC"].Trim()) <= 250 && GetSafeDouble(sItem["NBCCC_SC"].Trim()) >= 195)
                            {
                                sItem["NBCCC_PD"] = "合格";
                            }
                            else
                            {
                                
                                sItem["NBCCC_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["NBCCC_SC"] = "非数字";
                        }

                        //内部尺寸宽
                        sItem["NBCC_KYQ"] = "帽壳内部宽：170～220mm";
                        if (IsNumeric(sItem["NBCCK_SC"]))
                        {
                            if (GetSafeDouble(sItem["NBCCK_SC"].Trim()) <= 220 && GetSafeDouble(sItem["NBCCK_SC"].Trim()) >= 170)
                            {
                                sItem["NBCCK_PD"] = "合格";
                            }
                            else
                            {
                                sItem["NBCCK_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["NBCCK_SC"] = "非数字";
                        }


                        //内部尺寸高
                        sItem["NBCC_GYQ"] = "帽壳内部高：120～150mm";
                        if (IsNumeric(sItem["NBCCG_SC"]))
                        {
                            if (GetSafeDouble(sItem["NBCCG_SC"].Trim()) <= 150 && GetSafeDouble(sItem["NBCCG_SC"].Trim()) >= 120)
                            {
                                sItem["NBCCG_PD"] = "合格";
                            }
                            else
                            {
                                sItem["NBCCG_PD"] = "不合格";
                                itemHG = false;
                                mAllHg = false;
                            }
                        }
                        else
                        {
                            sItem["NBCCG_SC"] = "非数字";
                        }

                        if (sItem["NBCCG_PD"] == "合格" && sItem["NBCCK_PD"] == "合格" && sItem["NBCCC_PD"] == "合格")
                        {
                            mHggs++;
                        }
                        else
                        {
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }

                    }
                    else
                    {
                        sItem["NBCCC_PD"] = "----";
                        sItem["NBCC_CYQ"] = "----";
                        sItem["NBCCC_SC"] = "----";
                        sItem["NBCCK_PD"] = "----";
                        sItem["NBCC_KYQ"] = "----";
                        sItem["NBCCK_SC"] = "----";
                        sItem["NBCCG_PD"] = "----";
                        sItem["NBCC_GYQ"] = "----";
                        sItem["NBCCG_SC"] = "----";
                    }
                    #endregion

                }

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
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求	。";
                //if (mHggs > 0)
                //{
                //    jsbeizhu = "该组样品所检项目部分符合" + MItem[0]["PDBZ"] + "标准要求。";
                //}
                //else
                //{
                //    jsbeizhu = "该组样品所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                //}
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
