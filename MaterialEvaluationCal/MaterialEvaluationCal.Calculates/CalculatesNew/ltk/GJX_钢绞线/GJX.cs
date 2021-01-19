using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GJX : BaseMethods
    {
        public void Calc()
        {
            #region
            /************************ 代码开始 *********************/
            bool mAllHg = true;
            string jcxm = "", mjcjg = "不合格", jsbeizhu = "";
            var jcxmBhg = "";
            var jcxmCur = "";
            Double md, xd ;

            var extraDJ = dataExtra["BZ_GJX_DJ"];
            var data = retData;

            var SItem = data["S_GJX"];
            var MItem = data["M_GJX"];
            bool sign = true;
            int Max_zs, Bhgs;
            Bhgs = 0;//检测项目不合格个数
            Max_zs = MItem[0]["IFFJ"] == "是" ? 6 : 3;
            foreach (var sItem in SItem)
            {
                jcxm = '、' + sItem["JCXM"].Trim().Replace(",", "、") + "、";
                var extraFieldsDj = extraDJ.FirstOrDefault(u => u["JG"] == sItem["JG"].Trim() && u["ZJ"] == sItem["ZJ"] && u["JB"] == sItem["GCLX_JB"]);
                if (null != extraFieldsDj)
                {
                    sItem["G_JMJ"] = string.IsNullOrEmpty(extraFieldsDj["MJ"]) ? extraFieldsDj["MJ"] : extraFieldsDj["MJ"].Trim();
                    sItem["G_ZDL"] = string.IsNullOrEmpty(extraFieldsDj["KLHZBZZ"]) ? extraFieldsDj["KLHZBZZ"] : extraFieldsDj["KLHZBZZ"].Trim();
                    sItem["G_KLQD"] = string.IsNullOrEmpty(extraFieldsDj["KLQDBZZ"]) ? extraFieldsDj["KLQDBZZ"] : extraFieldsDj["KLQDBZZ"].Trim();
                    sItem["G_YSL"] = string.IsNullOrEmpty(extraFieldsDj["QFHZBZZ"]) ? extraFieldsDj["QFHZBZZ"] : extraFieldsDj["QFHZBZZ"].Trim();
                    sItem["G_SCL"] = string.IsNullOrEmpty(extraFieldsDj["SCLBZZ"]) ? extraFieldsDj["SCLBZZ"] : extraFieldsDj["SCLBZZ"].Trim();
                    sItem["G_MMZL"] = string.IsNullOrEmpty(extraFieldsDj["MMZL"]) ? extraFieldsDj["MMZL"] : extraFieldsDj["MMZL"].Trim();
                    sItem["G_ZJPC1"] = string.IsNullOrEmpty(extraFieldsDj["ZJPC1"]) ? extraFieldsDj["ZJPC1"] : extraFieldsDj["ZJPC1"].Trim();
                    sItem["G_ZJPC2"] = string.IsNullOrEmpty(extraFieldsDj["ZJPC2"]) ? extraFieldsDj["ZJPC2"] : extraFieldsDj["ZJPC2"].Trim();

                    sItem["GH_JMJ"] = string.IsNullOrEmpty(extraFieldsDj["MJ"]) ? extraFieldsDj["MJ"] : extraFieldsDj["MJ"].Trim();
                    sItem["GH_ZDL"] = string.IsNullOrEmpty(extraFieldsDj["KLHZBZZ"]) ? extraFieldsDj["KLHZBZZ"] : extraFieldsDj["KLHZBZZ"].Trim();
                    sItem["GH_KLQD"] = string.IsNullOrEmpty(extraFieldsDj["KLQDBZZ"]) ? extraFieldsDj["KLQDBZZ"] : extraFieldsDj["KLQDBZZ"].Trim();
                    sItem["GH_YSL"] = string.IsNullOrEmpty(extraFieldsDj["QFHZBZZ"]) ? extraFieldsDj["QFHZBZZ"] : extraFieldsDj["QFHZBZZ"].Trim();
                    sItem["GH_SCL"] = string.IsNullOrEmpty(extraFieldsDj["SCLBZZ"]) ? extraFieldsDj["SCLBZZ"] : extraFieldsDj["SCLBZZ"].Trim();
                    sItem["GH_MMZL"] = string.IsNullOrEmpty(extraFieldsDj["MMZL"]) ? extraFieldsDj["MMZL"] : extraFieldsDj["MMZL"].Trim();
                    sItem["GH_ZJPC1"] = string.IsNullOrEmpty(extraFieldsDj["ZJPC1"]) ? extraFieldsDj["ZJPC1"] : extraFieldsDj["ZJPC1"].Trim();
                    sItem["GH_ZJPC2"] = string.IsNullOrEmpty(extraFieldsDj["ZJPC2"]) ? extraFieldsDj["ZJPC2"] : extraFieldsDj["ZJPC2"].Trim();
                }
                else
                {
                    mAllHg = false;
                    mjcjg = "不下结论";
                    jsbeizhu = jsbeizhu + "依据不详";
                    continue;
                }
                //复试就用6个字段的值

                sItem["GGXH"] = sItem["JG"].Trim() + "-" + sItem["ZJ"].Trim() + "-" + sItem["GCLX_JB"].Trim();

                #region 0.2%屈服力

                if (jcxm.Contains("、0.2%屈服力、"))
                {
                    jcxmCur = "0.2%屈服力";
                    sign = true; //判定字段

                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        md = GetSafeDouble(sItem["GDYSL" + xd].Trim());//GDYSL:规定非比例延伸力1-6
                        sItem["W_YSL" + xd] = sItem["GDYSL" + xd].Trim();//W_YSL实测规范非比例延伸力1-6
                        sign = md >= GetSafeDouble(sItem["G_YSL"]) ? true : false;
                    }
                    sItem["GH_YSL"] = "≥" + sItem["G_YSL"];//GH_YSL:要求规范非比例延伸力
                    sItem["G_YSL"] = sign ? "符合" : "不符合";//G_YSL:判定规范非比例延伸力
                    if (!sign)
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    Bhgs = sign ? Bhgs : Bhgs + 1;
                }
                else
                {
                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        sItem["W_YSL" + xd] = "----";
                    }
                    sItem["GH_YSL"] = "----";
                    sItem["G_YSL"] = "----";
                }
                #endregion

                #region 弹性模量

                if (jcxm.Contains("、弹性模量、"))
                {
                    jcxmCur = "弹性模量";
                    sign = true; //判定字段

                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        md = GetSafeDouble(sItem["TXML" + xd].Trim());//TXML:弹性模量1-6
                        sItem["W_TXML" + xd] = sItem["TXML" + xd].Trim();//W_TXML实测弹性模量1-6
                        sign = md >= 185 && md <= 205 ? true : false;
                    }
                    sItem["GH_TXML"] = "195±10";
                    sItem["G_TXML"] = sign ? "符合" : "不符合";
                    if (!sign)
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    Bhgs = sign ? Bhgs : Bhgs + 1;
                }
                else
                {
                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        sItem["W_TXML" + xd] = "----";
                    }
                    sItem["GH_TXML"] = "----";
                    sItem["G_TXML"] = "----";
                }
                #endregion

                #region 最大力

                if (jcxm.Contains("、最大力、"))
                {
                    jcxmCur = "最大力";
                    sign = true; //判定字段

                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        md = GetSafeDouble(sItem["ZDL" + xd].Trim());//ZDL:最大力1-6
                        sItem["W_ZDL" + xd] = sItem["ZDL" + xd].Trim();//ZDL最大力1-6
                        sign = md >= GetSafeDouble(sItem["G_ZDL"]) ? true : false;
                    }
                    sItem["GH_ZDL"] =  sItem["G_ZDL"];
                    sItem["G_ZDL"] = sign ? "符合" : "不符合";
                    if (!sign)
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    Bhgs = sign ? Bhgs : Bhgs + 1;
                }
                else
                {
                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        sItem["W_ZDL" + xd] = "----";
                    }
                    sItem["GH_ZDL"] = "----";
                    sItem["G_ZDL"] = "----";
                }
                #endregion

                #region 公称抗拉强度
                int qdbhggs = 0;//强度不合格个数
                if (jcxm.Contains("、公称抗拉强度、"))
                {
                    jcxmCur = "公称抗拉强度";
                    sign = true; //判定字段


                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        md = GetSafeDouble(sItem["ZDL" + xd].Trim());//ZDL:最大力1-6
                        sItem["W_ZDL" + xd] = sItem["ZDL" + xd].Trim();//ZDL最大力1-6
                        sItem["W_KLQD" + xd] = Round(md / GetSafeDouble(sItem["G_JMJ"]), 0).ToString("0");
                        sign = md >= GetSafeDouble(sItem["G_KLQD"]) ? true : false;
                        if (!sign)
                        {
                            qdbhggs = qdbhggs + 1;
                        }
                    }
                    sItem["GH_KLQD"] = "≥" + sItem["G_KLQD"];
                    if (qdbhggs > 0)
                    {
                        sItem["G_KLQD"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    else { sItem["G_KLQD"] = "合格"; }
                    
                  
                 
                }
                else
                {
                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        sItem["W_KLQD" + xd] = "----";
                    }
                    sItem["GH_KLQD"] = "----";
                    sItem["G_KLQD"] = "----";
                }
                #endregion

                #region 截面积

                /*  if (jcxm.Contains("、截面积、"))
                  {
                      jcxmCur = "截面积";
                      sign = true; //判定字段

                      for (xd = 1; xd < Max_zs + 1; xd++)
                      {
                          md = GetSafeDouble(sItem["JMJ" + xd].Trim());//JMJ:截面积1-6
                          sItem["W_JMJ" + xd] = String.Format(md.ToString(), "0.00") ;//W_JMJ截面积1-6
                          sign = md >= GetSafeDouble(sItem["G_JMJ"]) ? sign : false;

                      }
                      sItem["GH_JMJ"] = "≥" + sItem["G_JMJ"];
                      sItem["G_JMJ"] = sign ? "符合" : "不符合";
                if (!sign)
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                      Bhgs = sign ? Bhgs : Bhgs + 1;
                  }
                  else
                  {
                      for (xd = 1; xd < Max_zs + 1; xd++)
                      {
                          sItem["W_JMJ" + xd] = "----";
                      }
                      sItem["GH_JMJ"] = "----";
                      sItem["G_JMJ"] = "----";
                  }*/
                #endregion

                #region 尺寸

                if (jcxm.Contains("、尺寸、"))
                {
                    jcxmCur = "尺寸";
                    sign = true; //判定字段

                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        md = GetSafeDouble(sItem["W_ZJ" + xd]) - GetSafeDouble(sItem["ZJ"]);
                        //sItem["W_ZJ" + xd] = String.Format(md.ToString(), "0.00");//W_ZJ尺寸1-6

                        sign = md >= GetSafeDouble(sItem["G_ZJPC2"]) * -1 && md <= GetSafeDouble(sItem["G_ZJPC1"]) ? true : false;

                    }
                    if (!string.IsNullOrEmpty(sItem["ZJPC1"]))
                    {
                        sItem["GH_ZJPC1"] = "+" + sItem["ZJPC1"];
                        sItem["GH_ZJPC2"] = "-" + sItem["ZJPC2"];
                    }
                    
                    sItem["GH_ZJ"] = sItem["ZJ"];
                    sItem["ZJ_GH"] = sign ? "符合" : "不符合";
                    if (!sign)
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    Bhgs = sign ? Bhgs : Bhgs + 1;


                }
                else
                {
                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        sItem["W_ZJ" + xd] = "----";
                    }
                    sItem["ZJ_GH"] = "----";
                    sItem["GH_ZJ"] = "----";
                    sItem["ZJPC1"] = "";
                    sItem["ZJPC2"] = "";
                }
                #endregion

                #region 最大力总伸长率

                if (jcxm.Contains("、最大力总伸长率、"))
                {
                    jcxmCur = "最大力总伸长率";
                    sign = true; //判定字段

                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                       
                        md =Round((GetSafeDouble(sItem["DHBJ" + xd])- GetSafeDouble(sItem["YSBJ" + xd]))/ GetSafeDouble(sItem["YSBJ" + xd]),1);
                        sItem["W_SCL" + xd] = md.ToString("0.0");
                        sign = md >= GetSafeDouble(sItem["G_SCL"]) ? true : false;



                    }
                    sItem["GH_SCL"] = "≥" + sItem["G_SCL"];
                    sItem["G_SCL"] = sign ? "符合" : "不符合";
                    if (!sign)
                    {
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                    }
                    Bhgs = sign ? Bhgs : Bhgs + 1;




                }
                else
                {
                    sItem["GH_SCL"] = "----";
                    sItem["G_SCL"] = "----";
                    for (xd = 1; xd < Max_zs + 1; xd++)
                    {
                        sItem["W_SCL" + xd] = "----";
                    }
                }
                sItem["JCJG"] = Bhgs == 0 ? "合格" : "不合格";
                mAllHg = Bhgs == 0 ? true : false;

                #endregion
            }
            #region 添加最终报告
            
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
                if (Max_zs == 3)//初试
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目均符合要求。";
                }//复试
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目均符合要求。";
                }
            }
            else
            {
                mjcjg = "不合格";
                if (Max_zs == 3)//初试
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，需双倍复检。";
                }//复试
                else
                {
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，经复检所检项目" + jcxmBhg.TrimEnd('、') + "不符合要求，该批产品不合格。";
                }
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            /************************ 代码结束 *********************/
            #endregion
        }
    }
}
