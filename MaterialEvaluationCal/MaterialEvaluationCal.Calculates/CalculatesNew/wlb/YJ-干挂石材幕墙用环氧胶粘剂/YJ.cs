using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class YJ : BaseMethods
    {
        public void Calc()
        {
            /*********************** 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_YJ_DJ"];

            //var jcxmItems = retData.Select(u => u.Key).ToArray();

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_YJS = data["S_YJ"];
            if (!data.ContainsKey("M_YJ"))
            {
                data["M_YJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_YJ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            int mbHggs = 0;//统计合格数量
            bool sign = true;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in S_YJS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["YPLB"].Trim() == sItem["YPLB"] && u["JLX"].Trim() == sItem["JLB"]);
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = "不合格";
                    continue;
                }
                else
                {
                    sItem["G_CJQD"] = extraFieldsDj["G_CJQD"].Trim();
                    sItem["G_DRHYJQD"] = extraFieldsDj["G_DRHYJQD"].Trim(); //压剪强度--冻融
                    sItem["G_JSHYJQD"] = extraFieldsDj["G_JSHYJQD"].Trim(); //压剪强度--浸水
                    sItem["G_LJQD"] = extraFieldsDj["G_LJQD"].Trim();
                    sItem["G_RCLYJQD"] = extraFieldsDj["G_RCLYJQD"].Trim();  //压剪强度--热处理
                    sItem["G_WG"] = extraFieldsDj["G_WG"].Trim();
                    sItem["G_WQTXML"] = extraFieldsDj["G_WQTXML"].Trim();
                    sItem["G_YJQDBZG"] = extraFieldsDj["G_YJQDBZG"].Trim(); //压剪强度--不锈钢标准条件
                    sItem["G_YJQDBZS"] = extraFieldsDj["G_YJQDBZS"].Trim(); //压剪强度--石材标准条件
                    switch (sItem["CPMC"])
                    {
                        case "石材-石材(标准条件48h)":
                            sItem["G_YJQD"] = extraFieldsDj["G_YJQDBZS"].Trim();
                            break;
                        case "石材-石材(浸水168h)":
                            sItem["G_YJQD"] = extraFieldsDj["G_JSHYJQD"].Trim();
                            break;
                        case "石材-石材(热处理80℃,168h)":
                            sItem["G_YJQD"] = extraFieldsDj["G_RCLYJQD"].Trim();
                            break;
                        case "石材-石材(冻融循环50次)":
                            sItem["G_YJQD"] = extraFieldsDj["G_DRHYJQD"].Trim();
                            break;
                        case "石材-不锈钢(标准条件48h)":
                            sItem["G_YJQD"] = extraFieldsDj["G_YJQDBZG"].Trim();
                            break;
                    }
                }
                #region 外观
                if (jcxm.Contains("、外观、"))
                {
                    if (MItem[0]["GH_WG"] == "合格")
                    {
                        MItem[0]["GH_WG"] = "合格";
                        mbHggs++;
                    }
                    else
                    {
                        MItem[0]["GH_WG"] = "不合格";
                        mAllHg = false;
                        itemHG = false;
                    }
                }
                else
                {
                    sItem["G_WG"] = "----";
                    sItem["W_WG"] = "----";
                    MItem[0]["GH_WG"] = "----";
                }
                #endregion

                #region 弯曲弹性模量
                if (jcxm.Contains("、弯曲弹性模量、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["W_WQTXML"]) && !string.IsNullOrEmpty(sItem["W_WQTXML"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_WQTXML"] = IsQualified(sItem["G_WQTXML"], sItem["W_WQTXML"], false);
                        if (MItem[0]["GH_WQTXML"] == "不合格")
                        {
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
                    sItem["G_WQTXML"] = "----";
                    sItem["W_WQTXML"] = "----";
                    MItem[0]["GH_WQTXML"] = "----";
                }
                #endregion

                #region 冲击强度
                if (jcxm.Contains("、冲击强度、"))
                {
                    sign = true;
                    sign = IsNumeric(sItem["W_CJQD"]) && !string.IsNullOrEmpty(sItem["W_CJQD"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_CJQD"] = IsQualified(sItem["G_CJQD"], sItem["W_CJQD"], false);
                        if (MItem[0]["GH_CJQD"] == "不合格")
                        {
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
                    sItem["G_CJQD"] = "----";
                    sItem["W_CJQD"] = "----";
                    MItem[0]["GH_CJQD"] = "----";
                }
                #endregion

                #region 拉剪强度
                if (jcxm.Contains("、拉剪强度、"))
                {
                    jcxmCur = "拉剪强度";
                    sign = true;
                    sign = IsNumeric(sItem["W_LJQD"]) && !string.IsNullOrEmpty(sItem["W_LJQD"]) ? sign : false;
                    if (sign)
                    {
                        sItem["HG_LJQD"] = IsQualified(sItem["G_LJQD"], sItem["W_LJQD"], false);
                        if (sItem["HG_LJQD"] == "不合格")
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
                    sItem["G_LJQD"] = "----";
                    sItem["W_LJQD"] = "----";
                    sItem["HG_LJQD"] = "----";
                }
                #endregion

                #region 压剪强度
                if (jcxm.Contains("、压剪强度、"))
                {
                    jcxmCur = "压剪强度";
                    sign = true;
                    sign = IsNumeric(sItem["W_YJQD"]) && !string.IsNullOrEmpty(sItem["W_YJQD"]) ? sign : false;
                    if (sign)
                    {
                        sItem["HG_YJQD"] = IsQualified(sItem["G_YJQD"], sItem["W_YJQD"], false);
                        if (sItem["HG_YJQD"] == "不合格")
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
                    sItem["G_YJQD"] = "----";
                    sItem["W_YJQD"] = "----";
                    sItem["HG_YJQD"] = "----";
                }
                #endregion

                #region 压剪强度(石材—石材 标准条件)
                if (jcxm.Contains("、压剪强度(石材—石材 标准条件)、"))
                {
                    jcxmCur = "压剪强度(石材—石材 标准条件)";
                    sign = true;
                    sign = IsNumeric(sItem["W_YJQDBZS"]) && !string.IsNullOrEmpty(sItem["W_YJQDBZS"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_YJQDBZS"] = IsQualified(sItem["G_YJQDBZS"], sItem["W_YJQDBZS"], false);
                        if (MItem[0]["GH_YJQDBZS"] == "不合格")
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
                    sItem["G_YJQDBZS"] = "----";
                    sItem["W_YJQDBZS"] = "----";
                    MItem[0]["GH_YJQDBZS"] = "----";
                }
                #endregion

                #region 压剪强度(石材—石材 浸水)
                if (jcxm.Contains("、压剪强度(石材—石材 浸水)、"))
                {
                    jcxmCur = "压剪强度(石材—石材 浸水)";
                    sign = true;
                    sign = IsNumeric(sItem["W_JSHYJQD"]) && !string.IsNullOrEmpty(sItem["W_JSHYJQD"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_JSHYJQD"] = IsQualified(sItem["G_JSHYJQD"], sItem["W_JSHYJQD"], false);
                        if (MItem[0]["GH_JSHYJQD"] == "不合格")
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
                    sItem["G_JSHYJQD"] = "----";
                    sItem["W_JSHYJQD"] = "----";
                    MItem[0]["GH_JSHYJQD"] = "----";
                }
                #endregion

                #region 压剪强度(石材—石材 热处理)
                if (jcxm.Contains("、压剪强度(石材—石材 热处理)、"))
                {
                    jcxmCur = "压剪强度(石材—石材 热处理)";
                    sign = true;
                    sign = IsNumeric(sItem["W_RCLYJQD"]) && !string.IsNullOrEmpty(sItem["W_RCLYJQD"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_RCLYJQD"] = IsQualified(sItem["G_RCLYJQD"], sItem["W_RCLYJQD"], false);
                        if (MItem[0]["GH_RCLYJQD"] == "不合格")
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
                    sItem["G_RCLYJQD"] = "----";
                    sItem["W_RCLYJQD"] = "----";
                    MItem[0]["GH_RCLYJQD"] = "----";
                }
                #endregion

                #region 压剪强度(石材—石材 冻融循环)
                if (jcxm.Contains("、压剪强度(石材—石材 冻融循环)、"))
                {
                    jcxmCur = "压剪强度(石材—石材 冻融循环)";
                    sign = true;
                    sign = IsNumeric(sItem["W_DRHYJQD"]) && !string.IsNullOrEmpty(sItem["W_DRHYJQD"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_DRHYJQD"] = IsQualified(sItem["G_DRHYJQD"], sItem["W_DRHYJQD"], false);
                        if (MItem[0]["GH_DRHYJQD"] == "不合格")
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
                    sItem["G_DRHYJQD"] = "----";
                    sItem["W_DRHYJQD"] = "----";
                    MItem[0]["GH_DRHYJQD"] = "----";
                }
                #endregion

                #region 压剪强度(石材—不锈钢 标准条件)
                if (jcxm.Contains("、压剪强度(石材—不锈钢 标准条件)、"))
                {
                    jcxmCur = "压剪强度(石材—不锈钢 标准条件)";
                    sign = true;
                    sign = IsNumeric(sItem["W_YJQDBZG"]) && !string.IsNullOrEmpty(sItem["W_YJQDBZG"]) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_YJQDBZG"] = IsQualified(sItem["G_YJQDBZG"], sItem["W_YJQDBZG"], false);
                        if (MItem[0]["GH_YJQDBZG"] == "不合格")
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
                    sItem["G_YJQDBZG"] = "----";
                    sItem["W_YJQDBZG"] = "----";
                    MItem[0]["GH_YJQDBZG"] = "----";
                }
                #endregion

                #region

                //#region 石材－石材标准条件压剪强度
                //if (jcxm.Contains("、石材－石材标准条件压剪强度、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_YJQDBZS"]) && !string.IsNullOrEmpty(sItem["W_YJQDBZS"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_YJQDBZS"] = IsQualified(sItem["G_YJQDBZS"], sItem["W_YJQDBZS"], false);
                //        if (MItem[0]["GH_YJQDBZS"] == "不合格")
                //        {
                //            mAllHg = false;
                //            itemHG = false;
                //        }
                //        else
                //        {
                //            mbHggs++;
                //        }
                //    }
                //}
                //else
                //{
                //    sItem["G_YJQDBZS"] = "----";
                //    sItem["W_YJQDBZS"] = "----";
                //    MItem[0]["GH_YJQDBZS"] = "----";
                //}
                //#endregion

                //#region 石材－石材浸水压剪强度
                //if (jcxm.Contains("、石材－石材浸水压剪强度、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_JSHYJQD"]) && !string.IsNullOrEmpty(sItem["W_JSHYJQD"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_JSHYJQD"] = IsQualified(sItem["G_JSHYJQD"], sItem["W_JSHYJQD"], false);
                //        if (MItem[0]["GH_JSHYJQD"] == "不合格")
                //        {
                //            mAllHg = false;
                //            itemHG = false;
                //        }
                //        else
                //        {
                //            mbHggs++;
                //        }
                //    }

                //}
                //else
                //{
                //    sItem["G_JSHYJQD"] = "----";
                //    sItem["W_JSHYJQD"] = "----";
                //    MItem[0]["GH_JSHYJQD"] = "----";
                //}
                //#endregion

                //#region 石材－石材热处理压剪强度
                //if (jcxm.Contains("、石材－石材热处理压剪强度、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_RCLYJQD"]) && !string.IsNullOrEmpty(sItem["W_RCLYJQD"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_RCLYJQD"] = IsQualified(sItem["G_RCLYJQD"], sItem["W_RCLYJQD"], false);
                //        if (MItem[0]["GH_RCLYJQD"] == "不合格")
                //        {
                //            mAllHg = false;
                //            itemHG = false;
                //        }
                //        else
                //        {
                //            mbHggs++;
                //        }
                //    }
                //}
                //else
                //{
                //    sItem["G_RCLYJQD"] = "----";
                //    sItem["W_RCLYJQD"] = "----";
                //    MItem[0]["GH_RCLYJQD"] = "----";
                //}
                //#endregion

                //#region 石材－石材冻融循环压剪强度
                //if (jcxm.Contains("、石材－石材冻融循环压剪强度、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_DRHYJQD"]) && !string.IsNullOrEmpty(sItem["W_DRHYJQD"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_DRHYJQD"] = IsQualified(sItem["G_DRHYJQD"], sItem["W_DRHYJQD"], false);
                //        if (MItem[0]["GH_DRHYJQD"] == "不合格")
                //        {
                //            mAllHg = false;
                //            itemHG = false;
                //        }
                //        else
                //        {
                //            mbHggs++;
                //        }
                //    }
                //}
                //else
                //{
                //    sItem["G_DRHYJQD"] = "----";
                //    sItem["W_DRHYJQD"] = "----";
                //    MItem[0]["GH_DRHYJQD"] = "----";
                //}
                //#endregion

                //#region 石材－不锈钢标准条件压剪强度
                //if (jcxm.Contains("、石材－不锈钢标准条件压剪强度、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_YJQDBZG"]) && !string.IsNullOrEmpty(sItem["W_YJQDBZG"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_YJQDBZG"] = IsQualified(sItem["G_YJQDBZG"], sItem["W_YJQDBZG"], false);
                //        if (MItem[0]["GH_YJQDBZG"] == "不合格")
                //        {
                //            mAllHg = false;
                //            itemHG = false;
                //        }
                //        else
                //        {
                //            mbHggs++;
                //        }
                //    }
                //}
                //else
                //{
                //    sItem["G_YJQDBZG"] = "----";
                //    sItem["W_YJQDBZG"] = "----";
                //    MItem[0]["GH_YJQDBZG"] = "----";
                //}
                //#endregion

                //#region vb 代码中跳转之后代码所加的检测项,暂时用不到
                ////#region 抗弯承载力
                ////if (jcxm.Contains("、抗弯承载力、"))
                ////{
                ////    sign = true;
                ////    sign = IsNumeric(sItem["W_PHHZ"]) && !string.IsNullOrEmpty(sItem["W_PHHZ"]) ? sign : false;
                ////    if (sign)
                ////    {
                ////        MItem[0]["GH_PHHZ"] = IsQualified(sItem["G_PHHZ"], sItem["W_PHHZ"], false);
                ////        if (MItem[0]["GH_PHHZ"] == "不合格")
                ////        {
                ////            mAllHg = false;
                ////            itemHG = false;
                ////        }
                ////    }
                ////}
                ////else
                ////{
                ////    sItem["G_PHHZ"] = "----";
                ////    sItem["W_PHHZ"] = "----";
                ////    MItem[0]["GH_PHHZ"] = "----";
                ////}
                ////#endregion

                ////#region 传热系数
                ////if (jcxm.Contains("、传热系数、"))
                ////{
                ////    sign = true;
                ////    sign = IsNumeric(sItem["W_CRXS"]) && !string.IsNullOrEmpty(sItem["W_CRXS"]) ? sign : false;
                ////    if (sign)
                ////    {
                ////        MItem[0]["GH_CRXS"] = IsQualified(sItem["G_CRXS"], sItem["W_CRXS"], false);
                ////        if (MItem[0]["GH_CRXS"] == "不合格")
                ////        {
                ////            mAllHg = false;
                ////            itemHG = false;
                ////        }
                ////    }
                ////}
                ////else
                ////{
                ////    sItem["G_CRXS"] = "----";
                ////    sItem["W_CRXS"] = "----";
                ////    MItem[0]["GH_CRXS"] = "----";
                ////}
                ////#endregion

                ////#region 粘结强度
                ////if (jcxm.Contains("、粘结强度、"))
                ////{
                ////    sign = true;
                ////    sign = IsNumeric(sItem["W_NJQD"]) && !string.IsNullOrEmpty(sItem["W_NJQD"]) ? sign : false;
                ////    if (sign)
                ////    {
                ////        MItem[0]["GH_NJQD"] = IsQualified(sItem["G_NJQD"], sItem["W_NJQD"], false);
                ////        if (MItem[0]["GH_NJQD"] == "不合格")
                ////        {
                ////            mAllHg = false;
                ////            itemHG = false;
                ////        }
                ////    }
                ////}
                ////else
                ////{
                ////    sItem["G_NJQD"] = "----";
                ////    sItem["W_NJQD"] = "----";
                ////    MItem[0]["GH_NJQD"] = "----";
                ////}
                ////#endregion

                ////#region 剥离性能
                ////if (jcxm.Contains("、剥离性能、"))
                ////{
                ////    sign = true;
                ////    sign = IsNumeric(sItem["W_BLXN"]) && !string.IsNullOrEmpty(sItem["W_BLXN"]) ? sign : false;
                ////    if (sign)
                ////    {
                ////        MItem[0]["GH_BLXN"] = IsQualified(sItem["G_BLXN"], sItem["W_BLXN"], false);
                ////        if (MItem[0]["GH_BLXN"] == "不合格")
                ////        {
                ////            mAllHg = false;
                ////            itemHG = false;
                ////        }
                ////    }
                ////}
                ////else
                ////{
                ////    sItem["G_BLXN"] = "----";
                ////    sItem["W_BLXN"] = "----";
                ////    MItem[0]["GH_BLXN"] = "----";
                ////}
                //#endregion
                #endregion

                //单组
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
            #region 最终结果
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion
            /************************ 代码结束 ********************/
        }
    }
}
