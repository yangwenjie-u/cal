using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            bool flag, sign, mark = false;
            bool SFlg = false;//是否两根
            bool itemHG = true;//判断单组是否合格
            int mbHggs = 0;//检测项目合格数量

            //遍历从表数据
            foreach (var sItem in S_GXS)
            {
                itemHG = true;
                string jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
           
                //外径、壁厚
                if (string.IsNullOrEmpty(sItem["GGWJ"]) || !IsNumeric(sItem["GGWJ"]))
                {
                    sItem["GGWJ"] = "48.3";
                }
                if (string.IsNullOrEmpty(sItem["GGBH"]) || !IsNumeric(sItem["GGBH"]))
                {
                    sItem["GGBH"] = "3.5";
                }
                #region 根据样品名称，要求根数是否为2根
                switch (sItem["YPMC"])
                {
                    case "低合金高强度结构钢钢管":
                    case "合金钢钢管":
                        SFlg = true;
                        break;
                    case "结构用无缝钢管":
                    case "优质碳素结构钢钢管":
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
                                if (IsQualified("≤16", sItem["GGBH"], false) == "合格")
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD1"].Trim();
                                }
                                else
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD2"].Trim();
                                }
                                if (IsQualified("≤168.3", sItem["GGWJ"], false) == "合格")
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
                                if (IsQualified("≤16", sItem["GGBH"], false) == "合格")
                                {
                                    sItem["G_QFQD"] = extraFieldsDj["QFQD1"].Trim();
                                }
                                else if (IsQualified("＞30", sItem["GGBH"], false) == "合格")
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

                #region 压扁    
                if (jcxm.Contains("、压扁、"))
                {
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

                #region  弯曲
                if (jcxm.Contains("、弯曲、"))
                {
                    jcxmCur = "弯曲";
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

                #region  拉伸
                if (jcxm.Contains("、拉伸、"))
                {
                    jcxmCur = "拉伸";
                    sign = true;
                    flag = true;
                    flag = IsNumeric(sItem["QFHZ"]) ? flag : false;
                    flag = IsNumeric(sItem["KLHZ"]) ? flag : false;
                    flag = IsNumeric(sItem["SCZ"]) ? flag : false;
                    if (SFlg)
                    {
                        flag = IsNumeric(sItem["QFHZ2"]) ? flag : false;
                        flag = IsNumeric(sItem["KLHZ2"]) ? flag : false;
                        flag = IsNumeric(sItem["SCZ2"]) ? flag : false;
                    }
                    //通过钢管壁厚
                    //md1 = GetSafeDouble(sItem["GGWJ"].Trim());
                    //md2 = GetSafeDouble(sItem["GGBH"].Trim());
                    //sum = Math.Round(3.14159 * (Math.Pow((md1 / 2), 2) - Math.Pow(md2, 2)));
                    //通过截取试样
                    if (IsNumeric(sItem["SYHD"]) && IsNumeric(sItem["SYKD"]) && GetSafeDouble(sItem["SYHD"].Trim()) > 0 && GetSafeDouble(sItem["SYKD"].Trim()) > 0)
                    {
                        md1 = GetSafeDouble(sItem["SYHD"].Trim());
                        md2 = GetSafeDouble(sItem["SYKD"].Trim());
                        sum = Math.Round(md1 * md2, 2);
                    }
                    else
                    {
                        md1 = GetSafeDouble(sItem["GGWJ"].Trim());
                        md2 = GetSafeDouble(sItem["GGBH"].Trim());
                        sum = 3.14159 * (Math.Pow((md1 / 2), 2) - Math.Pow(md1 / 2 - md2, 2));
                        if (IsNumeric(sItem["GGWJ2"]) && IsNumeric(sItem["GGBH2"]))
                        {
                            md1 = GetSafeDouble(sItem["GGWJ2"].Trim());
                            md2 = GetSafeDouble(sItem["GGBH2"].Trim());
                            sum2 = 3.14159 * (Math.Pow((md1 / 2), 2) - Math.Pow(md1 / 2 - md2, 2));
                        }
                        else
                        {
                            sum2 = sum;
                        }
                    }
                    #region  字段null值暂时解决方案   报告编号、201900038  示例
                    //if(!string.IsNullOrEmpty(sItem["SYHD2"]) && !string.IsNullOrEmpty(sItem["SYKD2"]))
                    //{
                    if (IsNumeric(sItem["SYHD2"]) && IsNumeric(sItem["SYKD2"]) && GetSafeDouble(sItem["SYHD2"].Trim()) > 0 && GetSafeDouble(sItem["SYKD2"].Trim()) > 0)
                    {//进入这里
                        md1 = GetSafeDouble(sItem["SYHD2"].Trim());//6.1
                        md2 = GetSafeDouble(sItem["SYKD2"].Trim());//30.5
                        sum2 = Math.Round(md1 * md2, 2);
                    }
                    //}
                    #endregion
                    flag = true;
                    if (flag)
                    {
                        md1 = GetSafeDouble(sItem["QFHZ"].Trim());//59.28
                        md2 = sum;//183
                        md = Math.Round(1000 * md1 / md2);
                        md = Math.Round(md / 5, 0) * 5;//325
                        sItem["QFQD"] = md.ToString("0");
                        if (SFlg)
                        {
                            md1 = GetSafeDouble(sItem["QFHZ2"].Trim());//59.28
                            md2 = sum2;//183
                            md = 1000 * md1 / md2;
                            md = Math.Round(md / 5, 0) * 5;//325
                            sItem["QFQD2"] = md.ToString("0");
                            sItem["HG_QFQD"] = IsQualified(sItem["G_QFQD"], sItem["QFQD"], false) == "合格" && IsQualified(sItem["G_QFQD"], sItem["QFQD2"], false) == "合格" ? "合格" : "不合格";
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
                        }
                        else
                        {
                            //#region  字段null值暂时解决方案   报告编号、201900038  示例
                            //if (sItem["HG_QFQD"] != null && sItem["G_QFQD"] != null && sItem["QFQD"] != null)
                            //{
                            //之后恢复将该段代码提出来
                            sItem["HG_QFQD"] = IsQualified(sItem["G_QFQD"], sItem["QFQD"], false);
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
                            
                            

                        }
                       
                        // 抗拉强度
                        md1 = GetSafeDouble(sItem["KLHZ"]);
                        md2 = sum;
                        md = 1000 * md1 / md2;
                        md = Math.Round(md / 5, 0) * 5;
                        sItem["KLQD"] = md.ToString("0");
                        if (SFlg)
                        {
                            md1 = GetSafeDouble(sItem["KLHZ2"].Trim());
                            md2 = sum2;
                            md = 1000 * md1 / md2;
                            md = Math.Round(md / 5, 0) * 5;
                            sItem["KLQD2"] = md.ToString("0");
                            sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false) == "合格" && IsQualified(sItem["G_KLQD"], sItem["KLQD2"], false) == "合格" ? "合格" : "不合格";
                            if (sItem["HG_KLQD"] == "不合格")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur+"抗拉强度" + "、";
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
                            sItem["HG_KLQD"] = IsQualified(sItem["G_KLQD"], sItem["KLQD"], false);
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
                        }

                        //伸长率
                        md1 = sum;
                        md1 = Math.Sqrt(md1);
                        md1 = md1 * 5.65;
                        pjmd = Math.Round(md1, 0);
                        pjmd = 5 * (Math.Floor(0.5 + pjmd / 5));
                        pjmd = Math.Round(pjmd, 0);
                        sItem["YSBJ"] = pjmd.ToString("0");

                        md1 = GetSafeDouble(sItem["SCZ"].Trim());
                        md2 = GetSafeDouble(sItem["YSBJ"].Trim());
                        md = 100 * (md1 - md2) / md2;
                        if (md > 10)
                        {
                            md = Math.Round(0.5 * (Math.Floor(0.5 + md / 0.5)), 1);
                        }

                        sItem["SCL"] = Math.Round(md, 1).ToString("0.0");
                        if (SFlg)
                        {
                            md1 = sum2;//158
                            md1 = Math.Sqrt(md1);
                            md1 = 5.65 * md1;
                            pjmd = Math.Round(md1, 0);
                            pjmd = 5 * (Math.Floor(0.5 + pjmd / 5));
                            sItem["YSBJ2"] = pjmd.ToString("0");

                            md1 = GetSafeDouble(sItem["SCZ2"].Trim());//158
                            md2 = GetSafeDouble(sItem["YSBJ2"].Trim());//120
                            md = 100 * (md1 - md2) / md2;//31.5
                            if (md > 10)
                            {
                                md = Math.Round(0.5 * (Math.Floor(0.5 + md / 0.5)), 1);
                            }
                            sItem["SCL2"] = md.ToString("0.0");
                            sItem["HG_SCL"] = IsQualified(sItem["G_SCL"], sItem["SCL"], false) == "合格" && IsQualified(sItem["G_SCL"], sItem["SCL2"], false) == "合格" ? "合格" : "不合格";
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
                        else
                        {
                            sItem["HG_SCL"] = IsQualified(sItem["G_SCL"], sItem["SCL"], false);
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
                    else
                    {
                        sign = false;
                    }
                }
                else
                {
                    sign = false;
                }
                #endregion

                if (!sign)
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
                if (sItem["G_SCL"] == "----")
                {
                    //sItem["SCL1"] = "----";
                    sItem["SCL2"] = "----";
                }
                if (sItem["G_QFQD"] == "----")
                {
                    sItem["QFQD"] = "----";
                    sItem["QFQD2"] = "----";
                }
              
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
    }
}
