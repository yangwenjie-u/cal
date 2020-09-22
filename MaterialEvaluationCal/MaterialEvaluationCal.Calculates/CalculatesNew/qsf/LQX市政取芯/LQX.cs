using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class LQX : BaseMethods
    {
        public void Calc()
        {
            #region 
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "", jcjg = "";

            var extraDJ = dataExtra["BZ_LQX_DJ"];
            var data = retData;

            var SItem = data["S_LQX"];
            var MItem = data["M_LQX"];
            var extraE_LQX = data["E_LQX"];
            bool ywyc = true;
            var jcxmBhg = "";
            var jcxmCur = "";

            foreach (var sItem in SItem)
            {
                bool ysdflag = true, hdflag = true;
                string hdbeizhu = "", ysdbeizhu = "";
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                //var extraFieldsE_LQX = extraE_LQX.Where(u => u["DZBH"] == sItem["DZBH"].Trim());
                foreach (var E_LQX in extraE_LQX)
                {
                    if (E_LQX["FYH"] == "1")
                    {
                        hdbeizhu = string.IsNullOrEmpty(E_LQX["HDBEIZHU"]) ? E_LQX["HDBEIZHU"] : E_LQX["HDBEIZHU"].Trim();
                        ysdbeizhu = string.IsNullOrEmpty(E_LQX["YSDBEIZHU"]) ? E_LQX["YSDBEIZHU"] : E_LQX["YSDBEIZHU"].Trim();
                    }
                    E_LQX["SHDDW"] = "mm";
                    E_LQX["SYSDDW"] = "%";
                    E_LQX["HDBEIZHU"] = hdbeizhu;
                    E_LQX["YSDBEIZHU"] = ysdbeizhu;
                    E_LQX["SDZBH"] = "LQX" + sItem["DZBH"].Trim();

                    E_LQX["SJYSD"] = "----";
                    sItem["JCJG"] = "合格";

                    #region 密度（表干法、水中法、蜡封法）= 压实度
                    if (jcxm.Contains("、密度（表干法、水中法、蜡封法）、"))
                    {
                        jcxmCur = "密度（表干法、水中法、蜡封法）";
                        if (!string.IsNullOrEmpty(sItem["SJYSD"]))
                        {
                            E_LQX["SJYSD"] = "≥" + sItem["SJYSD"];
                        }

                        if (E_LQX["SCYSD"] == "" || E_LQX["SCYSD"] == "----")
                        {
                            E_LQX["SCYSD"] = "----";
                            E_LQX["SYSDPD"] = "----";
                            E_LQX["SJYSD"] = "----";
                        }
                        else
                        {
                            E_LQX["SYSDPD"] = IsQualified(E_LQX["SJYSD"], E_LQX["SCYSD"], true);
                            if (E_LQX["SYSDPD"] == "不符合")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                ysdflag = false;
                            }
                            ywyc = Conversion.Val(E_LQX["SCYSD"]) > 100 ? false : ywyc;
                        }
                    }
                    #endregion

                    #region 厚度
                    if (jcxm.Contains("、厚度、"))
                    {
                        jcxmCur = "厚度";
                        if (E_LQX["SCHD"] == "----" || E_LQX["SCHD"] == "")
                        {
                            E_LQX["SCHD"] = "----";
                        }

                        if (string.IsNullOrEmpty(sItem["PC_S"]) || sItem["PC_S"] == "----")
                        {
                            E_LQX["SSJHD"] = "----";
                            E_LQX["HDPC"] = "----";
                            MItem[0]["PDBZ"] = "----";
                            E_LQX["SHDPD"] = "----";
                        }
                        else
                        {
                            E_LQX["SSJHD"] = E_LQX["SJHD"] + "(" + sItem["PC_S"] + ")";

                            int i = 0;
                            double md = 0, md1 = 0, md2 = 0;
                            string stemp = "----";

                            i = sItem["PC_S"].IndexOf("，");
                            if (i > 0 && i < sItem["PC_S"].Length)
                            {
                                md1 = Conversion.Val(sItem["PC_S"].Substring(0, i - 1));
                                md2 = Conversion.Val(sItem["PC_S"].Substring(i - 1, sItem["PC_S"].Length - i - 1));
                                md = Conversion.Val(E_LQX["SJHD"]);

                                md1 = md + md1;
                                md2 = md + md2;
                                md1 = Math.Round(md1, 0);
                                md2 = Math.Round(md2, 0);
                                stemp = md1.ToString("0") + "～" + md2.ToString("0");
                            }

                            i = sItem["PC_S"].IndexOf("～");
                            if (i > 0 && i < sItem["PC_S"].Length)
                            {
                                md1 = Conversion.Val(sItem["PC_S"].Substring(0, i - 1));
                                md2 = Conversion.Val(sItem["PC_S"].Substring(i - 1, sItem["PC_S"].Length - i - 1));
                                md = Conversion.Val(E_LQX["SJHD"]);

                                md1 = md + md1;
                                md2 = md + md2;
                                md1 = Math.Round(md1, 0);
                                md2 = Math.Round(md2, 0);
                                stemp = md1.ToString("0") + "～" + md2.ToString("0");
                            }

                            i = sItem["PC_S"].IndexOf("±");
                            if (i > 0)
                            {
                                md1 = Conversion.Val(sItem["PC_S"].Substring(0, sItem["PC_S"].Length - 1));
                                md = Conversion.Val(E_LQX["SJHD"]);
                                md1 = md - md1;
                                md2 = md + md1;
                                stemp = md1.ToString("0") + "～" + md2.ToString("0");
                            }

                            E_LQX["HDPC"] = (Conversion.Val(E_LQX["SCHD"]) - Conversion.Val(E_LQX["SJHD"])).ToString();
                            E_LQX["SHDPD"] = IsQualified(stemp, E_LQX["SCHD"], true);
                            if (E_LQX["SHDPD"] == "不符合")
                            {
                                jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                                hdflag = false;
                            }
                        }

                        //eLqx["BGBH"] = sItem["WTBH"];
                        //E_LQX["SXCZH"] = sItem["QYZH"];
                        //E_LQX["SHJ"] = sItem["HJ_S"];
                    }
                    #endregion

                }

                sItem["JCJG"] = (hdflag && ysdflag) ? "合格" : "不合格";
                mAllHg = sItem["JCJG"] == "不合格" ? false : mAllHg;
            }
            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求。";
            }
            if (!data.ContainsKey("M_LQX"))
            {
                data["M_LQX"] = new List<IDictionary<string, string>>();
            }
            var M_LQX = data["M_LQX"];
            //if (M_HNT == default || M_HNT.Count == 0)
            if (M_LQX == null)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_LQX.Add(m);
            }
            else
            {
                M_LQX[0]["JCJG"] = mjcjg;
                M_LQX[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion


            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
