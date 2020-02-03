using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class XC : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            //获取帮助表数据
            var extraDJ = dataExtra["BZ_XC_DJ"];

            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "合格";
            bool itemHG = true;//判断单组是否合格
            var S_XCS = data["S_XC"];
            //数据表数据
            var MS_MCS = data["MS_MC"];
            if (!data.ContainsKey("M_XC"))
            {
                data["M_XC"] = new List<IDictionary<string, string>>();
            }
            var MItem = data["M_XC"];

            if (MItem == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                MItem.Add(m);
            }
            double sum = 0;
            double md,pjmd = 0;
            double zqt, fqt, q2, fq2, kqfc, kqmj = 0;
            double fqf1 = 0;
            double fqf2 = 0;
            double fqz1 = 0;
            double fqz2 = 0;
            double zqf1 = 0;
            double zqf2 = 0;
            double zqz1 = 0;
            double zqz2 = 0;
            bool flag = true;

            string mhgjg = "";
            string mbhgjg = "";
            string djjg = "";
            int xd, Gs = 0;
            int mbhggs = 0;//不合格数量


            foreach (var sItem in S_XCS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                List<double> nArr = new List<double>();
                nArr.Add(0);
                //玻璃厚度
                if (IsNumeric(sItem["BLHD"]))
                {
                    sItem["BLHD"] = sItem["BLHD"] + "mm";
                }
                //玻璃构造
                //If Not(InStr(1, .Fields("blgz"), "mm") > 0) Then.Fields("blgz") = "(" & .Fields("blgz") & ")mm"
                //if (!((sItem["BLGZ"]).IndexOf("mm") > 0))
                if (!sItem["BLGZ"].Contains("mm"))
                {
                    sItem["BLGZ"] = "(" + sItem["BLGZ"] + ")mm";
                }
                //If InStr(1, .Fields("ggcc"), "×") Then
                if (sItem["GGCC"].Contains("×"))
                {
                    sItem["MCCD"] = sItem["GGCC"].Substring(0, (sItem["GGCC"]).IndexOf("×")) + "mm";
                    sItem["MCKD"] = sItem["GGCC"].Substring(sItem["GGCC"].Length - (sItem["GGCC"].Length - sItem["GGCC"].IndexOf("×")) + 1, sItem["GGCC"].Length - sItem["GGCC"].IndexOf("×") -1) + "mm";
                }
                sItem["BLZD"] = sItem["BLZDC"].Trim() + "×" + sItem["BLZDK"].Trim();

                if (jcxm.Contains("、气密性能、"))
                {      
                    //var extraFieldsMS_MC = extraMS_MC.Where(u => u["试验编号"] == sItem["WTDBH"] && u["CSYLB"] == "XC");
                    //var extraFieldsMS_MC = extraMS_MC.Where(u => u["SYSJBRECID"] == sItem["RECID"]);
                    int count = 1;
                    foreach (var MS_MC in MS_MCS)
                    {
                        if (MS_MC.Count == 0)
                        {
                            count++;
                            continue;
                        }
                        if (MS_MC["开启缝长"] != null)
                        {
                            kqfc = double.Parse(MS_MC["开启缝长"].Trim());//1 
                        }
                        if (MS_MC["试件面积"] != null)
                        {
                            kqmj = double.Parse(MS_MC["试件面积"].Trim());//2.25
                        }
                        if (MS_MC["升压流量100F"] != null)
                        {
                            zqf1 = double.Parse(MS_MC["升压流量100F"].Trim());//null
                        }
                        if (MS_MC["降压流量100F"] != null)
                        {
                            zqf2 = double.Parse(MS_MC["降压流量100F"].Trim());//null
                        }
                        if (MS_MC["升压流量100z"] != null)
                        {
                            zqz1 = double.Parse(MS_MC["升压流量100z"].Trim());//null
                        }
                        if (MS_MC["降压流量100z"] != null)
                        {
                            zqz2 = double.Parse(MS_MC["降压流量100z"].Trim());//null
                        }
                        if (MS_MC["负升压流量100F"] != null)
                        {
                            fqf1 = double.Parse(MS_MC["负升压流量100F"].Trim());//0.1
                        }
                        if (MS_MC["负降压流量100F"] != null)
                        {
                            fqf2 = double.Parse(MS_MC["负降压流量100F"].Trim());//0.1
                        }
                        if (MS_MC["负升压流量100z"] != null)
                        {
                            fqz1 = double.Parse(MS_MC["负升压流量100z"].Trim());//28.5
                        }
                        if (MS_MC["负降压流量100z"] != null)
                        {
                            fqz2 = double.Parse(MS_MC["负降压流量100z"].Trim());//28.2
                        }

                        zqf1 = (zqf1 + zqf2) / 2;//0
                        zqz1 = (zqz1 + zqz2) / 2;//0
                        fqf1 = (fqf1 + fqf2) / 2;//0.1
                        fqz1 = (fqz1 + fqz2) / 2;//28.35
                        zqt = zqz1 - zqf1;//0
                        fqt = fqz1 - fqf1;//28.25

                        q2 = Math.Round(zqt / kqmj, 2);
                        q2 = Math.Round(q2 / 4.65, 2);//0
                        fq2 = Math.Round(fqt / kqmj, 2);
                        fq2 = Math.Round(fq2 / 4.65, 2);//2.7
                        sItem["MJQMFY" + count] = q2.ToString("0.00");
                        sItem["MJQMZY" + count] = fq2.ToString("0.00");
                        count++;
                        if (count > 3)
                        {
                            break;
                        }
                    }

                    sum = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        md = double.Parse(sItem["MJQMZY" + i].Trim());
                        nArr.Add(md);
                        sum = sum + md;
                    }
                    pjmd = Math.Round(sum / 3, 1);
                    sItem["AVG_MZ"] = Math.Round(pjmd, 1).ToString("0.0");

                    sum = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        md = double.Parse(sItem["MJQMFY" + i]);
                        nArr[i] = md;
                        sum = sum + md;
                        if (string.Equals(sItem["MJQMFY" + i], "0.00"))
                        {
                            sItem["MJQMFY" + i] = "----";
                        }
                    }
                    pjmd = Math.Round(sum / 3, 1);
                    sItem["AVG_MF"] = pjmd.ToString("0.0");
                    if (string.Equals(sItem["AVG_MF"].Trim(), "0.0"))
                    {
                        sItem["AVG_MF"] = "----";
                    }

                    flag = sItem["JCLB"] == "工程检测" ? true : false;
                    //气密性要求
                    sItem["QMXQ2YQ"] = sItem["QMXQ2YQ"].Trim();
                    sItem["QMXQ2YQ"] = IsNumeric(sItem["QMXQ2YQ"]) ? "≥" + sItem["QMXQ2YQ"] : sItem["QMXQ2YQ"];
                    if (IsQualified(sItem["QMXQ2YQ"], sItem["AVG_MZ"], true) == "符合")
                    {
                        sItem["PD_QM"] = "符合";
                    }
                    else if (IsQualified(sItem["QMXQ2YQ"], sItem["AVG_MZ"], true) == "不符合")
                    {
                        sItem["PD_QM"] = "不符合";
                        mbhggs++;
                    }
                    else
                    {
                        sItem["PD_QM"] = "----";
                    }

                    Gs = extraDJ.Count;
                    foreach (var extraFieldsDj in extraDJ)
                    {
                        if (IsQualified(extraFieldsDj["MJYQ"], sItem["AVG_MZ"], true) == "符合")
                        {
                            sItem["DJ_QZ"] = "第" + extraFieldsDj["MJJB"].Trim() + "级";
                            break;
                        }
                    }

                    if (extraDJ == null)
                    {
                        sItem["DJ_QZ"] = "不符合任一级别";
                    }

                    switch (sItem["PD_QM"])
                    {
                        case "符合":
                            mbhgjg = mbhgjg + "、气密性能";
                            jsbeizhu = "该组试样气密性能符合设计要求。";
                            sItem["JCJG"] = "合格";
                            break;

                        case "不符合":
                            mhgjg = mhgjg + "、气密性能";
                            sItem["JCJG"] = "不合格";
                            jsbeizhu = "该组试样气密性能不符合设计要求。";
                            mbhggs++;
                            break;

                        default:
                            djjg = djjg + "、气密性能";
                            sItem["JCJG"] = "合格";
                            jsbeizhu = "无设计要求， 该组试样气密性能检测结果如上。"; ;
                            break;
                    }
                    mjcjg = sItem["JCJG"];
                }
                else
                {
                    sItem["DJ_QF"] = "----";
                    sItem["DJ_QZ"] = "----";
                    sItem["PD_QM"] = "----";
                }
            }


            //添加最终报告

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}








