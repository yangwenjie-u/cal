using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class CZJ : BaseMethods
    {
        public void Calc()
        {
            /***********************代码开始 * ********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_CZJ_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_CZJS = data["S_CZJ"];
            if (!data.ContainsKey("M_CZJ"))
            {
                data["M_CZJ"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_CZJ"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            int mbHggs = 0;//检测项目合格数量
            bool sign = true;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in S_CZJS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["YPLB"] == sItem["YPLB"].Trim() && u["CLTJ"] == sItem["CLTJ"].Trim() && u["CZ"] == sItem["CZ"].Trim() && u["JLX"] == sItem["JLB"].Trim());
                if (null == extraFieldsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不下结论";
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }
                else
                {
                    sItem["G_YJNJQD"] = extraFieldsDj["G_YJNJQD"].Trim();
                    //sItem["G_CJQD"] = extraFieldsDj["G_CJQD"].Trim();
                    //sItem["G_DRHYJQD"] = extraFieldsDj["G_DRHYJQD"].Trim();
                    //sItem["G_JSHYJQD"] = extraFieldsDj["G_JSHYJQD"].Trim();
                    //sItem["G_JCLYJQD"] = extraFieldsDj["G_JCLYJQD"].Trim();
                    //sItem["G_RSHYJQD"] = extraFieldsDj["G_RSHYJQD"].Trim();
                    //sItem["G_RCLYJQD"] = extraFieldsDj["G_RCLYJQD"].Trim();
                    //sItem["G_WG"] = extraFieldsDj["G_WG"].Trim();
                    //sItem["G_WQTXML"] = extraFieldsDj["G_WQTXML"].Trim();
                    //sItem["G_YJQDBZG"] = extraFieldsDj["G_YJQDBZG"].Trim();
                    //sItem["G_YJQDBZS"] = extraFieldsDj["G_YJQDBZS"].Trim();
                }

                #region 
                //#region 外观
                //if (jcxm.Contains("、外观、"))
                //{
                //    if ("合格" == MItem[0]["GH_WG"])
                //    {
                //        MItem[0]["GH_WG"] = "合格";
                //        mbHggs++;
                //    }
                //    else
                //    {
                //        MItem[0]["GH_WG"] = "不合格";
                //        mAllHg = false;
                //        itemHG = false;
                //    }
                //}
                //else
                //{
                //    sItem["G_WG"] = "----";
                //    sItem["W_WG"] = "----";
                //    MItem[0]["GH_WG"] = "----";
                //}
                //#endregion

                //#region 弯曲弹性模量
                //if (jcxm.Contains("、弯曲弹性模量、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_WQTXML"]) && !string.IsNullOrEmpty(sItem["W_WQTXML"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_WQTXML"] = IsQualified(sItem["G_WQTXML"], sItem["W_WQTXML"],false);
                //        if ("不合格" == MItem[0]["GH_WQTXML"])
                //        {
                //            itemHG = false;
                //            mAllHg = false;
                //        }
                //        else
                //        {
                //            mbHggs++;
                //        }
                //    }
                //}
                //else
                //{
                //    sItem["G_WQTXML"] = "----";
                //    sItem["W_WQTXML"] = "----";
                //    MItem[0]["GH_WQTXML"] = "----";
                //}
                //#endregion

                //#region 冲击韧性
                //if (jcxm.Contains("、冲击韧性、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_CJQD"]) && !string.IsNullOrEmpty(sItem["W_CJQD"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_CJQD"] = IsQualified(sItem["G_CJQD"], sItem["W_CJQD"], false);
                //        if ("不合格" == MItem[0]["GH_CJQD"])
                //        {
                //            itemHG = false;
                //            mAllHg = false;
                //        }
                //        else
                //        {
                //            mbHggs++;
                //        }
                //    }
                //}
                //else
                //{
                //    sItem["G_CJQD"] = "----";
                //    sItem["W_CJQD"] = "----";
                //    MItem[0]["GH_CJQD"] = "----";
                //}
                //#endregion

                //#region 石材－石材标准条件压剪强度
                //if (jcxm.Contains("、石材－石材标准条件压剪强度、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_YJQDBZS"]) && !string.IsNullOrEmpty(sItem["W_YJQDBZS"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_YJQDBZS"] = IsQualified(sItem["G_YJQDBZS"], sItem["W_YJQDBZS"], false);
                //        if ("不合格" == MItem[0]["GH_YJQDBZS"])
                //        {
                //            itemHG = false;
                //            mAllHg = false;
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
                //        if ("不合格" == MItem[0]["GH_JSHYJQD"])
                //        {
                //            itemHG = false;
                //            mAllHg = false;
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

                //#region 石材－石材碱处理压剪强度
                //if (jcxm.Contains("、石材－石材碱处理压剪强度、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_JCLYJQD"]) && !string.IsNullOrEmpty(sItem["W_JCLYJQD"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_JCLYJQD"] = IsQualified(sItem["G_JCLYJQD"], sItem["W_JCLYJQD"], false);
                //        if ("不合格" == MItem[0]["GH_JCLYJQD"])
                //        {
                //            itemHG = false;
                //            mAllHg = false;
                //        }
                //        else
                //        {
                //            mbHggs++;
                //        }
                //    }
                //}
                //else
                //{
                //    sItem["G_JCLYJQD"] = "----";
                //    sItem["W_JCLYJQD"] = "----";
                //    MItem[0]["GH_JCLYJQD"] = "----";
                //}
                //#endregion

                //#region 石材－石材热水处理压剪强度
                //if (jcxm.Contains("、石材－石材热水处理压剪强度、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_RSHYJQD"]) && !string.IsNullOrEmpty(sItem["W_RSHYJQD"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_RSHYJQD"] = IsQualified(sItem["G_RSHYJQD"], sItem["W_RSHYJQD"], false);
                //        if ("不合格" == MItem[0]["GH_RSHYJQD"])
                //        {
                //            itemHG = false;
                //            mAllHg = false;
                //        }
                //        else
                //        {
                //            mbHggs++;
                //        }
                //    }
                //}
                //else
                //{
                //    sItem["G_RSHYJQD"] = "----";
                //    sItem["W_RSHYJQD"] = "----";
                //    MItem[0]["GH_RSHYJQD"] = "----";
                //}
                //#endregion

                //#region 石材－石材高温处理压剪强度
                //if (jcxm.Contains("、石材－石材高温处理压剪强度、"))
                //{
                //    sign = true;
                //    sign = IsNumeric(sItem["W_RCLYJQD"]) && !string.IsNullOrEmpty(sItem["W_RCLYJQD"]) ? sign : false;
                //    if (sign)
                //    {
                //        MItem[0]["GH_RCLYJQD"] = IsQualified(sItem["G_RCLYJQD"], sItem["W_RCLYJQD"], false);
                //        if ("不合格" == MItem[0]["GH_RCLYJQD"])
                //        {
                //            itemHG = false;
                //            mAllHg = false;
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
                //        if ("不合格" == MItem[0]["GH_DRHYJQD"])
                //        {
                //            itemHG = false;
                //            mAllHg = false;
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
                //        if ("不合格" == MItem[0]["GH_YJQDBZG"])
                //        {
                //            itemHG = false;
                //            mAllHg = false;
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
                #endregion

                #region 压剪粘结强度
                if (jcxm.Contains("、压剪粘结强度、"))
                {
                    jcxmCur = "压剪粘结强度";
                    if (IsQualified(sItem["G_YJNJQD"],sItem["W_YJNJQD"],false) == "合格")
                    {
                        sItem["HG_YJNJQD"] = "合格";
                    }
                    else
                    {
                        sItem["HG_YJNJQD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        itemHG = false;
                    }
                }
                else
                {
                    sItem["G_YJNJQD"] = "----";
                    sItem["W_YJNJQD"] = "----";
                    sItem["HG_YJNJQD"] = "----";
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
