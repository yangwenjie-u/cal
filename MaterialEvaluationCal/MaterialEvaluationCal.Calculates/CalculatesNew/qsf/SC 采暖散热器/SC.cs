using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class SC : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";


            //var extraDJ = dataExtra["BZ_SC_DJ"];
            var data = retData;

            var SItem = data["S_SC"];
            var MItem = data["M_SC"];
            bool sign = true;
            double md1 = 0, md2 = 0, md = 0;
            string Bz_Srl = "", Js_Rqd = "";

            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";

                //wtbh = sItem["WTBH"].Trim();
                //dzbh = sItem["DZBH"].Trim();

                //if (DateTime.Compare(GetSafeDate(MItem[0]["JYRQ"].Trim()), GetSafeDate("2018-04-01")) > 0)
                //{
                //    MItem[0]["WHICH"] = "bgsc_10、bgsc_1";
                //    if (sItem["SRQLX"] == "对流散热器")
                //    {
                //        MItem[0]["WHICH"] = "bgsc_12、bgsc_1";
                //    }
                //    if (MItem[0]["QRBM"].Trim().Contains("90"))
                //    {
                //        MItem[0]["WHICH"] = "bgsc_99、bgsc_10、bgsc_1";
                //        if (sItem["SRQLX"] == "对流散热器")
                //        {
                //            MItem[0]["WHICH"] = "bgsc_99、bgsc_12、bgsc_1";
                //        }
                //    }
                //}
                //else
                //{
                //    MItem[0]["WHICH"] = "bgsc、bgsc_1";
                //    if (sItem["SRQLX"].Trim() == "对流散热器")
                //    {
                //        MItem[0]["WHICH"] = "bgsc_2、bgsc_1";
                //    }
                //    if (MItem[0]["QRBM"].Trim().Contains("90"))
                //    {
                //        MItem[0]["WHICH"] = "bgsc_99、bgsc、bgsc_1";
                //        if (sItem["SRQLX"].Trim() == "对流散热器")
                //        {
                //            MItem[0]["WHICH"] = "bgsc_99、bgsc_2、bgsc_1";
                //        }
                //    }
                //}

                //RootDir = "D:\tomcat\webapps\ROOT\DBConfigue\tab\pic\sc\"
                //For xd = 1 To 3 Step 1
                //       FilePath = RootDir & dzbh & "_" & xd & ".gif"
                //       If Dir(FilePath) <> "" Then
                //            'Set giffile = fso.CreateTextFile(FilePath, True)
                //            oldPath = FilePath
                //            newPath = RootDir & wtbh & "_" & xd & ".gif"
                //            Name oldPath As newPath
                //       End If
                //Next xd

                sign = IsNumeric(sItem["KM_VAL"]) ? sign : false;
                sign = IsNumeric(sItem["N_VAL"]) ? sign : false;
                sign = IsNumeric(sItem["YP_ZL"]) ? sign : false;

                if (sign)
                {
                    //if (DateTime.Compare(GetSafeDate(MItem[0]["JYRQ"].Trim()), GetDate("2018-04-01"))>0)
                    //{
                    sItem["S_GXS"] = "Q=" + sItem["KM_VAL"].Trim() + "△T";
                    sItem["S_SBCS"] = sItem["N_VAL"];

                    md1 = GetSafeDouble(sItem["KM_VAL"].Trim());
                    md2 = GetSafeDouble(sItem["N_VAL"].Trim());
                    md = md1 * Math.Pow(44.5, md2);
                    md = Math.Round(md, 1);
                    Bz_Srl = md.ToString("0.0");
                    sItem["S_SRL"] = "Qs=" + md.ToString("0.0");
                    sItem["W_SRL"] = md.ToString("0.0");

                    md1 = md;
                    md2 = GetSafeDouble(sItem["YP_ZL"].Trim());
                    md = md1 / (44.5 * md2);
                    md = Math.Round(md, 3);
                    Js_Rqd = md.ToString("0.000");
                    sItem["W_RQD"] = md.ToString("0.000");
                    sItem["S_RQD"] = "q=" + md.ToString("0.000");
                    //}
                    //else
                    //{
                    //    sItem["S_GXS"] = "Q=" + sItem["KM_VAL"] + "△T";
                    //    sItem["S_SBCS"] = sItem["N_VAL"];
                    //    md1 = GetSafeDouble(sItem["KM_VAL"].Trim());
                    //    md2 = GetSafeDouble(sItem["N_VAL"].Trim());
                    //    md = md1 * Math.Pow(64.5,md2);
                    //    md = Math.Round(md,1);
                    //    Bz_Srl = md.ToString("0.0");
                    //    sItem["S_SRL"] = "Qs="+ md.ToString("0.0");
                    //    sItem["W_SRL"] = md.ToString("0.0");

                    //    md1 = md;
                    //    md2 = GetSafeDouble(sItem["YP_ZL"].Trim());
                    //    md = md1 / (64.5 * md2);
                    //    md = Math.Round(md,3);
                    //    Js_Rqd = md.ToString("0.000");
                    //    sItem["W_RQD"] = md.ToString("0.000");
                    //    sItem["S_RQD"] = "q=" + md.ToString("0.000");
                    //}


                    if (IsNumeric(sItem["W_SRL"].Trim()))
                    {
                        md1 = GetSafeDouble(sItem["W_SRL"].Trim());
                        //md2 = GetSafeDouble(sItem["YP_PS"].Trim());

                        md2 = Conversion.Val(sItem["YP_PS"].Trim());


                        if (md2 > 0)
                        {
                            md = md1 / md2;
                            md = Math.Round(md, 1);
                            sItem["W_DSRL"] = md.ToString("0.0");
                        }
                        else
                        {
                            sItem["W_DSRL"] = "----";
                        }
                    }
                }
                else
                {
                    sItem["S_GXS"] = "";
                    sItem["S_SBCS"] = "";
                    sItem["S_SRL"] = "";
                    sItem["S_RQD"] = "";
                    sItem["W_SRL"] = "";
                    sItem["W_RQD"] = "";
                    sItem["W_DSRL"] = "";
                }

                #region 压力试验
                if (jcxm.Contains("、压力试验、")|| jcxm.Contains("、液压试验、") )
                {
                    if (sItem["SJSYYL"] != "")
                    {
                        sItem["YLSYYQ"] = "试验压力为" + sItem["SJSYYL"] + "MPa；持续2～3min，压力不降且不渗不漏";
                        if (GetSafeDouble(sItem["YLSYSM"]) >= GetSafeDouble(sItem["SJSYYL"]) && GetSafeDouble(sItem["YLSYSM"]) >= 0.6 && sItem["SLQK"] == "压力不降且不渗不漏")
                        {
                            MItem[0]["GH_YLSY"] = "合格";
                        }
                        else
                        {
                            MItem[0]["GH_YLSY"] = "不合格";
                            //mbhggs++;
                        }
                    }
                    else
                    {
                        sItem["YLSYYQ"] = "试验压力为1.5倍工作压力，但不小于0.6MPa；持续2～3min，压力不降且不渗不漏";
                        if (GetSafeDouble(sItem["YLSYSM"].Trim()) >= GetSafeDouble(sItem["GZYL"]) * 1.5 && GetSafeDouble(sItem["YLSYSM"]) >= 0.6 && sItem["SLQK"] == "压力不降且不渗不漏")
                        {
                            MItem[0]["GH_YLSY"] = "合格";
                        }
                        else
                        {
                            MItem[0]["GH_YLSY"] = "不合格";
                            //mbhggs++;
                        }
                    }
                }
                else
                {
                    sItem["YLSYSM"] = "----";
                    MItem[0]["GH_YLSY"] = "----";
                    sItem["YLSYYQ"] = "----";
                    sItem["CXSJ"] = "----";
                    sItem["SLQK"] = "----";
                }
                #endregion

                //MItem[0]["JGSM"] = "该组试样";
                jsbeizhu = "该组试样";
                #region 散热量
                if (jcxm.Contains("、散热量、"))
                {
                    //MItem[0]["JGSM"] = MItem[0]["JGSM"] + "标准散热量为" + Bz_Srl + "W";
                    jsbeizhu = jsbeizhu + "标准散热量为" + Bz_Srl + "W";
                }
                else
                {
                    sItem["W_SRL"] = "----";
                    sItem["W_DSRL"] = "----";
                    sItem["S_GXS"] = "----";
                    sItem["S_SBCS"] = "----";
                }
                #endregion

                #region 金属热强度
                if (jcxm.Contains("、金属热强度、"))
                {
                    if (jsbeizhu == "该组试样")
                    {
                        jsbeizhu = jsbeizhu + "金属热强度为" + Js_Rqd + "W/(kg·K) ";
                    }
                    else
                    {
                        jsbeizhu = jsbeizhu + ",金属热强度为" + Js_Rqd + "W/(kg·K) ";
                    }
                }
                else
                {
                    sItem["W_RQD"] = "----";
                }
                #endregion

                #region 压力试验
                if (jcxm.Contains("、压力试验、") || jcxm.Contains("、液压试验、"))
                {
                    if (MItem[0]["GH_YLSY"] == "不合格")
                    {
                        sItem["JCJG"] = "不合格";
                        mAllHg = false;
                        if (jsbeizhu == "该组试样")
                        {
                            jsbeizhu = jsbeizhu + "压力试验不符合GB 50242-2002 《建筑给水排水及采暖工程施工质量验收规范》标准要求。";
                        }
                        else
                        {
                            jsbeizhu = jsbeizhu + "；压力试验不符合GB 50242-2002 《建筑给水排水及采暖工程施工质量验收规范》标准要求。";
                        }
                    }
                    else
                    {
                        sItem["JCJG"] = "合格";
                        mAllHg = true;
                        if (jsbeizhu == "该组试样")
                        {
                            jsbeizhu = jsbeizhu + "压力试验不符合GB 50242-2002 《建筑给水排水及采暖工程施工质量验收规范》标准要求。";
                        }
                        else
                        {
                            jsbeizhu = jsbeizhu + "；压力试验不符合GB 50242-2002 《建筑给水排水及采暖工程施工质量验收规范》标准要求。";
                        }
                    }
                }
                else
                {
                    sItem["JCJG"] = "合格";
                    mAllHg = true;
                    jsbeizhu = jsbeizhu + ",标准特征公式如上。";
                }
                #endregion
            }


            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_SC"))
            {
                data["M_SC"] = new List<IDictionary<string, string>>();
            }
            var M_SC = data["M_SC"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_SC == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_SC.Add(m);
            }
            else
            {
                M_SC[0]["JCJG"] = mjcjg;
                M_SC[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
