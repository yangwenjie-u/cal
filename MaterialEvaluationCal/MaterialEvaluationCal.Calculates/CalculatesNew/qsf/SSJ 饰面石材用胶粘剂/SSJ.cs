using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class SSJ : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            int mbhggs = 0;//不合格数量
            var extraDJ = dataExtra["BZ_SSJ_DJ"];
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";
            var data = retData;

            var SItem = data["S_SSJ"];
            var MItem = data["M_SSJ"];
            if (!data.ContainsKey("M_SSJ"))
            {
                data["M_SSJ"] = new List<IDictionary<string, string>>();
            }
            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }

            string BHGXM = "";
            int Zxms = 0;
            bool sign = true;

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                IDictionary<string, string> extraFieldsDj = null;
                //if (sItem["CPZF"] == "水泥基胶粘剂(C)")
                //{
                //    extraFieldsDj = extraDJ.FirstOrDefault(u => u["YPLB"] == sItem["YPLB"].Trim() && u["JLX"] == sItem["CPMC"].Trim() && u["YPYT"] == sItem["JLX"]);
                //}
                //else
                //{
                    extraFieldsDj = extraDJ.FirstOrDefault(u => u["YPLB"].Trim() == sItem["YPLB"].Trim() && u["JLX"].Trim() == sItem["CPMC"].Trim());
                //}
                if (null != extraFieldsDj)
                {
                    sItem["G_CJQD"] = extraFieldsDj["G_CJQD"].Trim();
                    sItem["G_DRHYJQD"] = extraFieldsDj["G_DRHYJQD"].Trim();
                    sItem["G_DRHLSQD"] = extraFieldsDj["G_DRHLSQD"].Trim();
                    sItem["G_JSHLSQD"] = extraFieldsDj["G_JSHLSQD"].Trim();
                    sItem["G_JSHYJQD"] = extraFieldsDj["G_JSHYJQD"].Trim();
                    sItem["G_LJQD"] = extraFieldsDj["G_LJQD"].Trim();
                    sItem["G_RLHLSQD"] = extraFieldsDj["G_RLHLSQD"].Trim();
                    sItem["G_RLHYJQD"] = extraFieldsDj["G_RLHYJQD"].Trim();
                    sItem["G_LZLSQD20"] = extraFieldsDj["G_LZLSQD20"].Trim();
                    sItem["G_LZLSQD10"] = extraFieldsDj["G_LZLSQD10"].Trim();
                    sItem["G_ZQLSQD"] = extraFieldsDj["G_ZQLSQD"].Trim();
                    sItem["G_WQTXML"] = extraFieldsDj["G_WQTXML"].Trim();
                    sItem["G_GDWYJQD"] = extraFieldsDj["G_GDWYJQD"].Trim();
                    sItem["G_YJQD"] = extraFieldsDj["G_YJQD"].Trim();
                    sItem["G_LSQD"] = extraFieldsDj["G_LSQD"].Trim();

                    //MItem[0]["WHICH"] = extraFieldsDj["WHICH"].Trim();
                }
                else
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }

                #region 弯曲弹性模量
                sign = true;
                if (jcxm.Contains("、弯曲弹性模量、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_WQTXML"].Trim()) && !string.IsNullOrEmpty(sItem["W_WQTXML"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_WQTXML"] = IsQualified(sItem["G_WQTXML"], sItem["W_WQTXML"], false);
                    }
                    BHGXM = MItem[0]["GH_WQTXML"] == "不合格" ? BHGXM + "弯曲弹性模量、" : BHGXM;
                }
                else
                {
                    sItem["G_WQTXML"] = "----";
                    sItem["W_WQTXML"] = "----";
                    MItem[0]["GH_WQTXML"] = "----";
                }
                #endregion

                #region 冲击强度
                sign = true;
                if (jcxm.Contains("、冲击强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_CJQD"]) && !string.IsNullOrEmpty(sItem["W_CJQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_CJQD"] = IsQualified(sItem["G_CJQD"], sItem["W_CJQD"], false);
                    }
                    BHGXM = MItem[0]["GH_CJQD"] == "不合格" ? BHGXM + "冲击强度、" : BHGXM;
                }
                else
                {
                    sItem["G_CJQD"] = "----";
                    sItem["W_CJQD"] = "----";
                    MItem[0]["GH_CJQD"] = "----";
                }

                #endregion

                #region 拉剪粘结强度 
                sign = true;
                if (jcxm.Contains("、拉剪粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_LJQD"]) && !string.IsNullOrEmpty(sItem["W_LJQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_LJQD"] = IsQualified(sItem["G_LJQD"], sItem["W_LJQD"], false);
                    }
                    BHGXM = MItem[0]["GH_LJQD"] == "不合格" ? BHGXM + "拉剪粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_LJQD"] = "----";
                    sItem["W_LJQD"] = "----";
                    MItem[0]["GH_LJQD"] = "----";
                }
                #endregion

                #region 压剪粘结强度
                sign = true;
                if (jcxm.Contains("、压剪粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_YJQD"]) && !string.IsNullOrEmpty(sItem["W_YJQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_YJQD"] = IsQualified(sItem["G_YJQD"], sItem["W_YJQD"], false);
                    }
                    BHGXM = MItem[0]["GH_YJQD"] == "不合格" ? BHGXM + "压剪粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_YJQD"] = "----";
                    sItem["W_YJQD"] = "----";
                    MItem[0]["GH_YJQD"] = "----";
                }
                #endregion

                #region 拉伸粘结强度
                sign = true;
                if (jcxm.Contains("、拉伸粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_LSQD"]) && !string.IsNullOrEmpty(sItem["W_LSQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_LSQD"] = IsQualified(sItem["G_LSQD"], sItem["W_LSQD"], false);
                    }
                    BHGXM = MItem[0]["GH_LSQD"] == "不合格" ? BHGXM + "拉伸粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_LSQD"] = "----";
                    sItem["W_LSQD"] = "----";
                    MItem[0]["GH_LSQD"] = "----";
                }
                #endregion

                #region 浸水后拉伸粘结强度
                sign = true;
                if (jcxm.Contains("、浸水后拉伸粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_JSHLSQD"]) && !string.IsNullOrEmpty(sItem["W_JSHLSQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_JSHLSQD"] = IsQualified(sItem["G_JSHLSQD"], sItem["W_JSHLSQD"], false);
                    }
                    BHGXM = MItem[0]["GH_JSHLSQD"] == "不合格" ? BHGXM + "浸水后拉伸粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_JSHLSQD"] = "----";
                    sItem["W_JSHLSQD"] = "----";
                    MItem[0]["GH_JSHLSQD"] = "----";
                }
                #endregion

                #region 浸水后压剪粘结强度 
                sign = true;
                if (jcxm.Contains("、浸水后压剪粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_JSHYJQD"]) && !string.IsNullOrEmpty(sItem["W_JSHYJQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_JSHYJQD"] = IsQualified(sItem["G_JSHYJQD"], sItem["W_JSHYJQD"], false);
                    }
                    BHGXM = MItem[0]["GH_JSHYJQD"] == "不合格" ? BHGXM + "浸水后压剪粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_JSHYJQD"] = "----";
                    sItem["W_JSHYJQD"] = "----";
                    MItem[0]["GH_JSHYJQD"] = "----";
                }
                #endregion

                #region 热老化后压剪粘结强度
                sign = true;
                if (jcxm.Contains("、热老化后压剪粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_RLHYJQD"]) && !string.IsNullOrEmpty(sItem["W_RLHYJQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_RLHYJQD"] = IsQualified(sItem["G_RLHYJQD"], sItem["W_RLHYJQD"], false);
                    }
                    BHGXM = MItem[0]["GH_RLHYJQD"] == "不合格" ? BHGXM + "热老化后压剪粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_RLHYJQD"] = "----";
                    sItem["W_RLHYJQD"] = "----";
                    MItem[0]["GH_RLHYJQD"] = "----";
                }
                #endregion

                #region 热老化后拉伸粘结强度
                sign = true;
                if (jcxm.Contains("、热老化后拉伸粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_RLHLSQD"]) && !string.IsNullOrEmpty(sItem["W_RLHLSQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_RLHLSQD"] = IsQualified(sItem["G_RLHLSQD"], sItem["W_RLHLSQD"], false);
                    }
                    BHGXM = MItem[0]["GH_RLHLSQD"] == "不合格" ? BHGXM + "热老化后拉伸粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_RLHLSQD"] = "----";
                    sItem["W_RLHLSQD"] = "----";
                    MItem[0]["GH_RLHLSQD"] = "----";
                }
                #endregion

                #region 冻融循环后压剪粘结强度
                sign = true;
                if (jcxm.Contains("、冻融循环后压剪粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_DRHYJQD"]) && !string.IsNullOrEmpty(sItem["W_DRHYJQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_DRHYJQD"] = IsQualified(sItem["G_DRHYJQD"], sItem["W_DRHYJQD"], false);
                    }
                    BHGXM = MItem[0]["GH_DRHYJQD"] == "不合格" ? BHGXM + "冻融循环后压剪粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_DRHYJQD"] = "----";
                    sItem["W_DRHYJQD"] = "----";
                    MItem[0]["GH_DRHYJQD"] = "----";
                }
                #endregion

                #region 冻融循环后拉伸粘结强度
                sign = true;
                if (jcxm.Contains("、冻融循环后拉伸粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_DRHLSQD"]) && !string.IsNullOrEmpty(sItem["W_DRHLSQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_DRHLSQD"] = IsQualified(sItem["G_DRHLSQD"], sItem["W_DRHLSQD"], false);
                    }
                    BHGXM = MItem[0]["GH_DRHLSQD"] == "不合格" ? BHGXM + "冻融循环后拉伸粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_DRHLSQD"] = "----";
                    sItem["W_DRHLSQD"] = "----";
                    MItem[0]["GH_DRHLSQD"] = "----";
                }
                #endregion

                #region 晾置20min后拉伸粘结强度
                sign = true;
                if (jcxm.Contains("、晾置20min后拉伸粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_LZLSQD20"]) && !string.IsNullOrEmpty(sItem["W_LZLSQD20"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_LZLSQD20"] = IsQualified(sItem["G_LZLSQD20"], sItem["W_LZLSQD20"], false);
                    }
                    BHGXM = MItem[0]["GH_LZLSQD20"] == "不合格" ? BHGXM + "晾置20min后拉伸粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_LZLSQD20"] = "----";
                    sItem["W_LZLSQD20"] = "----";
                    MItem[0]["GH_LZLSQD20"] = "----";
                }
                #endregion

                #region 晾置10min后拉伸粘结强度
                sign = true;
                if (jcxm.Contains("、晾置10min后拉伸粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_LZLSQD10"]) && !string.IsNullOrEmpty(sItem["W_LZLSQD10"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_LZLSQD10"] = IsQualified(sItem["G_LZLSQD10"], sItem["W_LZLSQD10"], false);
                    }
                    BHGXM = MItem[0]["GH_LZLSQD10"] == "不合格" ? BHGXM + "晾置10min后拉伸粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_LZLSQD10"] = "----";
                    sItem["W_LZLSQD10"] = "----";
                    MItem[0]["GH_LZLSQD10"] = "----";
                }
                #endregion

                #region 早期拉伸粘结强度(24h)
                sign = true;
                if (jcxm.Contains("、早期拉伸粘结强度(24h)、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_ZQLSQD"]) && !string.IsNullOrEmpty(sItem["W_ZQLSQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_ZQLSQD"] = IsQualified(sItem["G_ZQLSQD"], sItem["W_ZQLSQD"], false);
                    }
                    BHGXM = MItem[0]["GH_ZQLSQD"] == "不合格" ? BHGXM + "早期拉伸粘结强度(24h)、" : BHGXM;
                }
                else
                {
                    sItem["G_ZQLSQD"] = "----";
                    sItem["W_ZQLSQD"] = "----";
                    MItem[0]["GH_ZQLSQD"] = "----";
                }
                #endregion

                #region 高低温交变后压剪粘结强度
                sign = true;
                if (jcxm.Contains("、高低温交变后压剪粘结强度、"))
                {
                    Zxms = Zxms + 1;
                    sign = (IsNumeric(sItem["W_GDWYJQD"]) && !string.IsNullOrEmpty(sItem["W_GDWYJQD"])) ? sign : false;
                    if (sign)
                    {
                        MItem[0]["GH_GDWYJQD"] = IsQualified(sItem["G_GDWYJQD"], sItem["W_GDWYJQD"], false);
                    }
                    BHGXM = MItem[0]["GH_GDWYJQD"] == "不合格" ? BHGXM + "高低温交变后压剪粘结强度、" : BHGXM;
                }
                else
                {
                    sItem["G_GDWYJQD"] = "----";
                    sItem["W_GDWYJQD"] = "----";
                    MItem[0]["GH_GDWYJQD"] = "----";
                }
                #endregion

                mbhggs = MItem[0]["GH_CJQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_DRHYJQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_DRHLSQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_JSHYJQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_JSHLSQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_LJQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_LSQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_RLHYJQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_RLHLSQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_GDWYJQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_WQTXML"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_YJQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_ZQLSQD"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_LZLSQD10"] == "不合格" ? mbhggs++ : mbhggs;
                mbhggs = MItem[0]["GH_LZLSQD20"] == "不合格" ? mbhggs++ : mbhggs;

                sItem["JCJG"] = mbhggs == 0 ? "合格" : "不合格";
                mAllHg = mbhggs == 0 ? true : false;
                if (mbhggs == Zxms || mbhggs == 0)
                {
                    jsbeizhu = mbhggs == 0 ? "该组试样所检项目符合" + MItem[0]["PDBZ"] + "标准要求。" : "该组试样所检项目不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
                else
                {
                    jsbeizhu = "该组试样所检项中" + BHGXM.Substring(0, BHGXM.Length - 1) + "不符合" + MItem[0]["PDBZ"] + "标准要求。";
                }
            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion

            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
