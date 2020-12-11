using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class S_ZHG : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            
            #region Code
            #region 参数定义
            var extraDJ = dataExtra["BZ_KWS_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var SItems = data["S_KWS"];
            var MItem = data["M_KWS"];
            var jcxm = "";
            var CPMC = "";
            var mAllHg = true;
            var jcxmCur = "";
            var jcxmBhg = "";
            int jcxmBhgZs = 0;
            var jsbeizhu = "";

            #endregion
            //遍历从表数据
            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                CPMC = sItem["CPMC"];
                if (string.IsNullOrEmpty(CPMC))
                {
                    CPMC = "----";
                }
                #region 等级表处理
                var mrsDj = extraDJ.FirstOrDefault(u => u["CPMC"] == CPMC);
                if (null == mrsDj)
                {
                    //横杆插头焊接剪切强度
                    sItem["G_HCHJQD"] = "当P=25kN时，各部位不应破坏";
                    //轮盘组焊后剪切强度
                    sItem["G_LPJQQD"] = "当P=60kN时，各部位不应破坏";
                    //可调托撑抗压强度
                    sItem["G_KTTZKYQD"] = "当P=50kN时，各部位不应破坏";
                    //可调托撑抗压强度
                    sItem["G_KTDZKYQD"] = "当P=50kN时，各部位不应破坏";

                }
                else {
                    sItem["G_SWKQD"] = mrsDj["G_SWKQD"];
                    sItem["G_XWKHJQD"] = mrsDj["G_XWKHJQD"];
                    sItem["G_HGJTQD"] = mrsDj["G_HGJTQD"];
                    sItem["G_HGJTHJQD"] = mrsDj["G_HGJTHJQD"];
                    sItem["G_KTZZKYQDQD"] = mrsDj["G_KTZZKYQD"];
                }
                

                #endregion


                #region 上碗扣强度

                int SwkqdBhg=0;
                if (jcxm.Contains("、上碗扣强度、"))
                {
                    jcxmCur = "上碗扣强度";
                    for (int i = 1;  i < 9; i++)
                    {
                        if (GetSafeInt( sItem["SWKQDSC" + i]) != 1)
                        {
                            SwkqdBhg = SwkqdBhg + 1;
                        }
                    }
                    if (SwkqdBhg > 1)
                    {
                        sItem["HG_SWKQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_SWKQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        
                    }
                   
                }
                else
                {
                    sItem["HG_SWKQD"] = "----";
                    sItem["G_SWKQD"] = "----";
                }
                #endregion

                #region 下碗扣焊接强度
                int XwkhjqdBhg = 0;
                if (jcxm.Contains("、下碗扣焊接强度、"))
                {
                    jcxmCur = "下碗扣焊接强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["XWKHJQDSC" + i]) != 1)
                        {
                            XwkhjqdBhg = XwkhjqdBhg + 1;
                        }
                    }
                    if (XwkhjqdBhg > 1)
                    {
                       
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                        sItem["HG_XWKHJQD"] = "合格";
                    }
                    else
                    {
                        sItem["HG_XWKHJQD"] = "合格";
                        sItem["JCJG"] = "合格";
                       
                    }

                }
                else
                {
                    sItem["HG_XWKHJQD"] = "----";
                    sItem["G_XWKHJQD"] = "----";
                }
                #endregion

                #region 横杆接头强度
                int HgjtqdBhg = 0;
                if (jcxm.Contains("、横杆接头强度、"))
                {
                    jcxmCur = "横杆接头强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["HGJTQDSC" + i]) != 1)
                        {
                            HgjtqdBhg = HgjtqdBhg + 1;
                        }
                    }
                    if (HgjtqdBhg > 1)
                    {
                        sItem["HG_HGJTQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_HGJTQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        
                    }

                }
                else
                {
                    sItem["HG_HGJTQD"] = "----";
                    sItem["G_HGJTQD"] = "----";
                }
                #endregion

                #region 横杆接头焊接强度
                int HgjthjqdBhg = 0;
                if (jcxm.Contains("、横杆接头焊接强度、"))
                {
                    jcxmCur = "横杆接头焊接强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["HGJTHJQDSC" + i]) != 1)
                        {
                            HgjthjqdBhg = HgjthjqdBhg + 1;
                        }
                    }
                    if (HgjthjqdBhg > 1)
                    {
                        sItem["HG_HGJTHJQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_HGJTHJQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        
                    }

                }
                else
                {
                    sItem["HG_HGJTHJQD"] = "----";
                    sItem["G_HGJTHJQD"] = "----";
                }
                #endregion

                #region 可调支座抗压强度
                int KtzzkyqdBhg = 0;
                if (jcxm.Contains("、可调支座抗压强度、"))
                {
                    jcxmCur = "可调支座抗压强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["KTZZKYQDSC" + i]) != 1)
                        {
                            KtzzkyqdBhg = KtzzkyqdBhg + 1;
                        }
                    }
                    if (KtzzkyqdBhg > 1)
                    {
                        sItem["HG_KTZZKYQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_KTZZKYQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        
                    }

                }
                else
                {
                    sItem["HG_KTZZKYQD"] = "----";
                    sItem["G_KTZZKYQDQD"] = "----";
                }
                #endregion

                #region 横杆插头焊接剪切强度
                

                int HCHJQDBhg = 0;
                if (jcxm.Contains("、横杆插头焊接剪切强度、"))
                {
                    jcxmCur = "横杆插头焊接剪切强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["HCHJQD" + i]) != 1)
                        {
                            HCHJQDBhg = HCHJQDBhg + 1;
                        }
                    }
                    if (HCHJQDBhg > 1)
                    {
                        sItem["HG_HCHJQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_HCHJQD"] = "合格";
                        sItem["JCJG"] = "合格";

                    }

                }
                else
                {
                    sItem["HG_HCHJQD"] = "----";
                    sItem["G_HCHJQD"] = "----";
                }

                #endregion

                #region 轮盘组焊后剪切强度


                int LPJQQDBhg = 0;
                if (jcxm.Contains("、轮盘组焊后剪切强度、"))
                {
                    jcxmCur = "轮盘组焊后剪切强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["LPJQQD" + i]) != 1)
                        {
                            LPJQQDBhg = LPJQQDBhg + 1;
                        }
                    }
                    if (LPJQQDBhg > 1)
                    {
                        sItem["HG_LPJQQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_LPJQQD"] = "合格";
                        sItem["JCJG"] = "合格";

                    }

                }
                else
                {
                    sItem["HG_LPJQQD"] = "----";
                    sItem["G_LPJQQD"] = "----";
                }

                #endregion

                #region 可调托撑抗压强度


                int KTTZKYQDBhg = 0;
                if (jcxm.Contains("、可调托撑抗压强度、"))
                {
                    jcxmCur = "可调托撑抗压强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["KTTZKYQD" + i]) != 1)
                        {
                            KTTZKYQDBhg = KTTZKYQDBhg + 1;
                        }
                    }
                    if (KTTZKYQDBhg > 1)
                    {
                        sItem["HG_KTTZKYQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_KTTZKYQD"] = "合格";
                        sItem["JCJG"] = "合格";

                    }

                }
                else
                {
                    sItem["HG_KTTZKYQD"] = "----";
                    sItem["G_KTTZKYQD"] = "----";
                }

                #endregion

                #region 可调底座抗压强度


                int KTDZKYQDBhg = 0;
                if (jcxm.Contains("、可调底座抗压强度、"))
                {
                    jcxmCur = "可调底座抗压强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["KTDZKYQD" + i]) != 1)
                        {
                            KTDZKYQDBhg = KTDZKYQDBhg + 1;
                        }
                    }
                    if (KTDZKYQDBhg > 1)
                    {
                        sItem["HG_KTDZKYQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_KTDZKYQD"] = "合格";
                        sItem["JCJG"] = "合格";

                    }

                }
                else
                {
                    sItem["HG_KTDZKYQD"] = "----";
                    sItem["G_KTDZKYQD"] = "----";
                }

                #endregion

                if (mAllHg) {
                    sItem["JCJG"] = "符合";
                    //jsbeizhu = "该试样符合" + sItem["RSDJ"] + "级要求";
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目符合要求" + "。";
                }
                else{
                    sItem["JCJG"] = "不符合";
                    //jsbeizhu = "该试样不符合" + sItem["RSDJ"] + "级要求";
                    jsbeizhu = "依据" + MItem[0]["PDBZ"] + "的规定，所检项目" + jcxmBhg.TrimEnd('、') + "不符合" + "要求。";
                }
              

                

            }

            #region 添加最终报告
            if (mAllHg && mjcjg != "----")
            {
                mjcjg = "合格";
            }
            else
            {
                mjcjg = "不合格";
            }
            MItem[0]["JCJG"] = mjcjg;
            MItem[0]["JCJGMS"] = jsbeizhu;
            #endregion
            #endregion

            /************************ 代码结束 *********************/

        }
    }
}