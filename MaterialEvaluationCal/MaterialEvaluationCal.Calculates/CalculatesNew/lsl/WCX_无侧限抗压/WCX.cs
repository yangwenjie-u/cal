using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Calculates
{
    public class WCX : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region
            bool mAllHg = true;
            var data = retData;
            var mjcjg = "不合格";
            var jsbeizhu = "";
            var SItem = data["S_WCX"];
            var MItem = data["M_WCX"];
            var mItem = MItem[0];
            double sum = 0;

            foreach (var sItem in SItem)
            {
                bool jcjgHg = true;
                double md1, md2,pjmd,pcxs,fHsl;
                int hggs = 0;
                int count = 0;
                #region
                //for (int i = 1; i <= 1; i++)
                //{
                //    if (GetSafeDouble(sItem["MTJST" + i]) != 0)
                //    {
                //        sStzl = GetSafeDouble(sItem["MTJST" + i]) - GetSafeDouble(sItem["MTZL"]);
                //        if (GetSafeDouble(sItem["MTTJ"]) == 0)
                //        {
                //            sSmd = 0;
                //        }
                //        else
                //        {
                //            sSmd = Round(sStzl / GetSafeDouble(sItem["MTTJ"]), 3);
                //        }
                //        sSzl1 = GetSafeDouble(sItem["HJST" + "11"]) - GetSafeDouble(sItem["HJGT" + "11"]);
                //        sSzl2 = GetSafeDouble(sItem["HJST" + "12"]) - GetSafeDouble(sItem["HJGT" + "12"]);
                //        sGtzl1 = GetSafeDouble(sItem["HJGT" + "11"]) - GetSafeDouble(sItem["HZL" + "11"]);
                //        sGtzl2 = GetSafeDouble(sItem["HJGT" + "12"]) - GetSafeDouble(sItem["HZL" + "12"]);
                //        if (sGtzl1 == 0)
                //            sHsl1 = 0;
                //        else
                //            sHsl1 = Round(sSzl1 / sGtzl1 * 100, 1);
                //        if (sGtzl2 == 0)
                //            sHsl2 = 0;
                //        else
                //            sHsl2 = Round(sSzl2 / sGtzl2 * 100, 1);
                //        if (sGtzl1 == 0 || sGtzl2 == 0)
                //            sItem["PJHSL" + "1"] = Round(sHsl1 + sHsl2, 1).ToString();
                //        else
                //            sItem["PJHSL" + "1"] = Round((sHsl1 + sHsl2) / 2, 1).ToString();
                //        sItem["GMD" + "1"] = Round(sSmd / (1 + 0.01 * GetSafeDouble(sItem["PJHSL" + "1"])), 3).ToString();
                //    }
                //    else
                //    {
                //        sItem["PJHSL" + "1"] = "0";
                //        sItem["GMD" + "1"] = "0";
                //    }
                //}
                #endregion
                //计算 
                sum = 0;
                double sysl = GetSafeDouble(sItem["SYSL"]);
                for (int i = 1; i <= sysl; i++)
                {
                    if (IsNumeric(sItem["YSQSJZL" + i].Trim()) && IsNumeric(sItem["JSQSJZL" + i].Trim()) && IsNumeric(sItem["JSHSJZL" + i].Trim()))
                    {
                        //养生期质量损失 = 养生前试件质量 - 浸水前试件质量  取整
                        sItem["YSQZLSS" + i] = Round(GetSafeDouble(sItem["YSQSJZL" + i].Trim()) - GetSafeDouble(sItem["JSQSJZL" + i].Trim()), 0).ToString("0");
                        //浸水后吸水量  = 浸水后试件质量 - 浸水前试件质量  取整
                        sItem["JSHXSL" + i] = Round(GetSafeDouble(sItem["JSHSJZL" + i].Trim()) - GetSafeDouble(sItem["JSQSJZL" + i].Trim()), 0).ToString("0");
                        //抗压强度 = 试样破坏时最大压力（N）/ 试样破坏截面积
                        sItem["KYQD" + i] = Round((GetSafeDouble(sItem["ZDYL" + i].Trim()) * 1000) / ((GetSafeDouble(sItem["SJZJ" + i].Trim()) * GetSafeDouble(sItem["SJZJ" + i].Trim()) * 3.14159) / 4), 1).ToString("0.0");
                        sum = sum + GetSafeDouble(sItem["KYQD" + i]);
                    }
                    else
                    {
                        throw new SystemException("试验数据录入有误");
                    }
                }
                //平均抗压强度
                sItem["PJKYQD"] = Round(sum / sysl, 1).ToString("0.0");
                //计算标准差
                sum = 0;
                for (int i = 1; i <= sysl; i++)
                {
                    sum = sum + Math.Pow(GetSafeDouble(sItem["KYQD" + i]) - GetSafeDouble(sItem["PJKYQD"]) , 2);
                }
                sItem["BZC"] = Round(Math.Sqrt(sum / (sysl - 1)), 2).ToString("0.00");
                //三倍均方差剔除异常值
                sum = 0;
                fHsl = 0;
                for (int i = 1; i <= sysl; i++)
                {
                    if (GetSafeDouble(sItem["BZC"]) * 3 >= Math.Abs( GetSafeDouble(sItem["KYQD"+i]) - GetSafeDouble(sItem["PJKYQD"])))
                    {
                        sum = sum + GetSafeDouble(sItem["KYQD" + i]);
                        fHsl++;
                    }
                    else
                    {
                        sItem["KYQD" + i] = "3倍均方差剔除";
                        count++;
                    }
                }

                if ((sysl == 6 && count > 1) ||(sysl == 9 && count > 2) || (sysl == 13 && count > 3))
                {
                    sItem["PJKYQD"] = "重做";
                }
                else
                {
                    sItem["PJKYQD"] = Round(sum / fHsl, 1).ToString("0.0");
                }

                //偏差系数
                pcxs = 100 * Round(GetSafeDouble(sItem["BZC"]) / GetSafeDouble(sItem["PJKYQD"]), 3);
                sItem["PCXS"] = pcxs.ToString("0.0");
                //有效试件判定
                if (sysl == 6 && GetSafeDouble(sItem["PCXS"]) > 6)
                {
                    sItem["PJKYQD"] =  "无效试件，重做";
                }
                if (sysl == 9 && GetSafeDouble(sItem["PCXS"]) > 10)
                {
                    sItem["PJKYQD"] = "无效试件，重做";
                }
                if (sysl == 13 && GetSafeDouble(sItem["PCXS"]) > 15)
                {
                    sItem["PJKYQD"] = "无效试件，重做";
                }
                //计算RC值
                //sItem["CQS"] = EItem.Count.ToString();
                md1 = GetSafeDouble(sItem["PJKYQD"]);
                md2 = GetSafeDouble(sItem["BZC"]);
                if (mItem["BZLXS"] == "0.90")
                {
                    pjmd = Round(md1 - 1.282 * md2, 1);
                }
                else
                {
                    pjmd = Round(md1 - 1.645 * md2, 1);
                }
                pjmd = Round(md1 - 1.645 * md2, 1);
                sItem["RC0_95"] = Round(pjmd, 1).ToString("0.0");
                string[] sArray =  sItem["SJQD"].Split('>');
                md1 = Round(GetSafeDouble(sArray[1]), 1);
                md2 = Round(GetSafeDouble(sItem["PCXS"]), 1);
                //计算得到的偏差系数为百分数
                md2 = md2 / 100;
                pjmd = Round(md1 / (1 - 1.282 * md2), 1);
                sItem["RD"] = Round(pjmd, 1).ToString();
                # region 平均值为准
                if (mItem["PDFF"] == "平均值为准")
                {
                    md1 = Round(GetSafeDouble(sItem["PJKYQD"]), 1);
                    md2 = Round(GetSafeDouble(sItem["RD"]), 1);
                    sItem["JL"] = md1 >= md2 ? "符合" : "不符合";
                    mAllHg = md1 >= md2 ? mAllHg : false;
                    jcjgHg = mAllHg;
                    hggs = md1 >= md2 ? hggs + 1 : hggs;
                }
                else
                {
                    md1 = GetSafeDouble(sItem["RC0_95"]);
                    md2 = GetSafeDouble(sItem["SJQD"]);
                    sItem["JL"] = IsQualified(md2.ToString(),md1.ToString(),true);
                    //sItem["JL"] = md1 >= md2 ? "符合" : "不符合";
                    mAllHg = md1 >= md2 ? mAllHg : false;
                    jcjgHg = mAllHg;
                    hggs = md1 >= md2 ? hggs + 1 : hggs;
                }
                if (mItem["SFPD"] == "否")
                    sItem["JL"] = "----";
                if (sItem["JL"] == "符合")
                {
                    sItem["JCJG"] = "合格";
                    mItem["JCJG"] = "合格";
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                }
                else if (sItem["JL"] == "不符合")
                {
                    sItem["JCJG"] = "不合格";
                    mItem["JCJG"] = "不合格";
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目不符合要求。";
                    jcjgHg = false;
                    mAllHg = false;
                }
                #endregion
            }

            //添加最终报告
            if (mAllHg)
            {
                mjcjg = "合格";
            }
            if (!data.ContainsKey("M_WCX"))
            {
                data["M_WCX"] = new List<IDictionary<string, string>>();
            }
            var M_WCX = data["M_WCX"];

            if (M_WCX.Count == 0)
            {
                IDictionary<string, string> m = new Dictionary<string, string>();
                m["JCJG"] = mjcjg;
                m["JCJGMS"] = jsbeizhu;
                M_WCX.Add(m);
            }
            else
            {
                M_WCX[0]["JCJG"] = mjcjg;
                M_WCX[0]["JCJGMS"] = jsbeizhu;
            }
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
