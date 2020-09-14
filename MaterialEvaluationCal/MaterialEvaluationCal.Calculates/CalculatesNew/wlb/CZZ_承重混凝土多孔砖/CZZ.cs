using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
//承重混凝土多孔砖
namespace Calculates
{
    public class CZZ : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            #region  参数定义
            bool mAllHg;
            #endregion

            #region  集合取值
            var data = retData;
            var mrsDj = dataExtra["BZ_CZZ_DJ"];
            var MItem = data["M_CZZ"];
            var mitem = MItem[0];
            var SItem = data["S_CZZ"];
            #endregion

            #region  计算开始
            mAllHg = true;
            var jcxmBhg = "";
            var jcxmCur = "";
            var jsbeizhu = "";
            var mjcjg = "不合格";
            var jcjg = "合格";
            foreach (var sitem in SItem)
            {
                List<double> kyqdlist = new List<double>();
                double md1, md2, md, pjmd, sum;
                bool sign, itemHg;
                itemHg = false;
                var jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";
                //从设计等级表中取得相应的计算数值、等级标准
                var extraFieldsDj = mrsDj.FirstOrDefault(u => u["QDDJ"] == sitem["QDDJ"].Trim());
                if (extraFieldsDj != null && extraFieldsDj.Count() > 0)
                {
                    mitem["PJQDYQ"] = extraFieldsDj["QDYQPJZ"];
                    mitem["QDZXZYQ"] = extraFieldsDj["QDYQZXZ"];
                }
                else
                {
                    sitem["JCJG"] = "不下结论";
                    jsbeizhu = "不下结论";
                    mAllHg = false;
                    mjcjg = "不下结论";
                    continue;
                }

                #region 抗压强度
                sign = true;
                if (jcxm.Contains("、抗压强度、"))
                {
                    jcxmCur = "抗压强度";
                    for (int i = 1; i < 6; i++)
                    {
                        sign = IsNumeric(sitem["KYCD1_" + i]) ? sign : false;
                        sign = IsNumeric(sitem["KYCD2_" + i]) ? sign : false;
                        sign = IsNumeric(sitem["KYKD1_" + i]) ? sign : false;
                        sign = IsNumeric(sitem["KYKD2_" + i]) ? sign : false;
                        sign = IsNumeric(sitem["KYQDHZ" + i]) ? sign : false;
                    }
                    if (sign)
                    {
                        sum = 0;
                        for (int i = 1; i < 6; i++)
                        {
                            //平均长度
                            md1 = Round((GetSafeDouble(sitem["KYCD1_" + i].Trim()) + GetSafeDouble(sitem["KYCD2_" + i].Trim())) / 2, 0);
                            //平均宽度
                            md2 = Round((GetSafeDouble(sitem["KYKD1_" + i].Trim()) + GetSafeDouble(sitem["KYKD2_" + i].Trim())) / 2, 0);
                            //抗压面积
                            md = md1 * md2;
                            sitem["KYQD" + i] = Round(GetSafeDouble(sitem["KYQDHZ" + i].Trim()) / md, 1).ToString("0.0");
                            kyqdlist.Add(GetSafeDouble(sitem["KYQD" + i]));
                            sum = sum + GetSafeDouble(sitem["KYQD" + i]);
                        }
                        kyqdlist.Sort();
                        pjmd = sum / 5;
                        pjmd = Round(pjmd, 1);
                        //抗压强度平均
                        sitem["KYQDPJZ"] = pjmd.ToString("0.0");
                        //抗压强度最小值
                        sitem["KYQDZXZ"] = kyqdlist[0].ToString("0.0");

                        sitem["PJQDHGPD"] = IsQualified(mitem["PJQDYQ"], sitem["KYQDPJZ"]);
                        sitem["ZXQDHGPD"] = IsQualified(mitem["QDZXZYQ"], sitem["KYQDZXZ"]);

                        if ("不合格" == sitem["PJQDHGPD"] || "不合格" == sitem["ZXQDHGPD"])
                        {
                            itemHg = false;
                            mAllHg = false;
                            jcjg = "不合格";
                            jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        }

                    }
                    else
                    {
                        throw new SystemException("抗压强度试验数据录入有误");
                    }
                }
                else
                {
                    sitem["PJQDHGPD"] = "----";
                    sitem["ZXQDHGPD"] = "----";
                    sitem["KYQDPJZ"] = "----";
                    sitem["KYQDZXZ"] = "----";
                    mitem["PJQDYQ"] = "----";
                    mitem["QDZXZYQ"] = "----";
                }
                #endregion

                sitem["JCJG"] = jcjg;
            }

            if (mAllHg && mjcjg != "不下结论")
            {
                mjcjg = "合格";
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
            }
            else
            {
                jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求	。";
            }
            if (!data.ContainsKey("M_CZZ"))
            {

                data["M_CZZ"] = new List<IDictionary<string, string>>();
            }

            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
        }
    }
}
