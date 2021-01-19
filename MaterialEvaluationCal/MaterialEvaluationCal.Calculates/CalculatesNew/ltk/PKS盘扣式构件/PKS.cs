using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculates
{
    public class PKS : BaseMethods
    {
        public void Calc()
        {
            /************************ 代码开始 *********************/
            
            #region Code
            #region 参数定义
            var extraDJ = dataExtra["BZ_PKS_DJ"];
            var data = retData;
            var mjcjg = "不合格";
            var SItems = data["S_PKS"];
            var MItem = data["M_PKS"];
            var jcxm = "";
            var pdbz = "";
            var mAllHg = true;
            var jcxmCur = "";
            var jcxmBhg = "";
            int jcxmBhgZs = 0;
            var jsbeizhu = "";
            var ggxh = "";

            #endregion
            //遍历从表数据
            foreach (var sItem in SItems)
            {
                jcxm = "、" + sItem["JCXM"].Replace(',', '、') + "、";
                ggxh = sItem["GGXH"];
                if (string.IsNullOrEmpty(pdbz))
                {
                    pdbz = "----";
                }
                #region 等级表处理
                var mrsDj = extraDJ.FirstOrDefault(u => u["XH"] == ggxh);
                if (null == mrsDj)
                {
                    mAllHg = false;
                    sItem["JCJG"] = "不合格";
                   
                }
                sItem["G_PDKJQD"] = mrsDj["G_PDKJQD"];
                sItem["G_PSKJQD"] = mrsDj["G_PSKJQD"];
                sItem["G_PKWQD"] = mrsDj["G_PKWQD"];
                sItem["G_PKLQD"] = mrsDj["G_PKLQD"];
                sItem["G_PNFKJQD"] = mrsDj["G_PNFKJQD"];
                sItem["G_CZKYQD"] = mrsDj["G_CZKYQD"];

                #endregion


                #region 连接盘单侧抗剪强度

                int PDKJQDBhg = 0;
                if (jcxm.Contains("、连接盘单侧抗剪强度、"))
                {
                    jcxmCur = "连接盘单侧抗剪强度";
                    for (int i = 1;  i < 9; i++)
                    {
                        if (GetSafeInt( sItem["PDKJQD" + i]) != 1)
                        {
                            PDKJQDBhg = PDKJQDBhg + 1;
                        }
                    }
                    if (PDKJQDBhg > 1)
                    {
                        sItem["HG_PDKJQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_PDKJQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        
                    }
                   
                }
                else
                {
                    sItem["HG_PDKJQD"] = "----";
                    sItem["G_PDKJQD"] = "----";
                }
                #endregion

                #region 连接盘双侧抗剪强度

                int PSKJQDBhg = 0;
                if (jcxm.Contains("、连接盘单侧抗剪强度、"))
                {
                    jcxmCur = "连接盘单侧抗剪强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["PSKJQD" + i]) != 1)
                        {
                            PSKJQDBhg = PSKJQDBhg + 1;
                        }
                    }
                    if (PSKJQDBhg > 1)
                    {
                        sItem["HG_PSKJQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_PSKJQD"] = "合格";
                        sItem["JCJG"] = "合格";

                    }

                }
                else
                {
                    sItem["HG_PSKJQD"] = "----";
                    sItem["G_PSKJQD"] = "----";
                }
                #endregion

                #region 连接盘抗弯强度
                int PKWQDBhg = 0;
                if (jcxm.Contains("、连接盘抗弯强度试验、"))
                {
                    jcxmCur = "连接盘抗弯强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["PKWQD" + i]) != 1)
                        {
                            PKWQDBhg = PKWQDBhg + 1;
                        }
                    }
                    if (PKWQDBhg > 1)
                    {
                       
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                        sItem["HG_PKWQD"] = "合格";
                    }
                    else
                    {
                        sItem["HG_PKWQD"] = "合格";
                        sItem["JCJG"] = "合格";
                       
                    }

                }
                else
                {
                    sItem["HG_PKWQD"] = "----";
                    sItem["G_PKWQD"] = "----";
                }
                #endregion

                #region 连接盘抗拉强度
                int PKLQDBhg = 0;
                if (jcxm.Contains("、连接盘抗拉强度试验、"))
                {
                    jcxmCur = "连接盘抗拉强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["PKLQD" + i]) != 1)
                        {
                            PKLQDBhg = PKLQDBhg + 1;
                        }
                    }
                    if (PKLQDBhg > 1)
                    {

                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                        sItem["HG_PKLQD"] = "合格";
                    }
                    else
                    {
                        sItem["HG_PKLQD"] = "合格";
                        sItem["JCJG"] = "合格";

                    }

                }
                else
                {
                    sItem["HG_PKLQD"] = "----";
                    sItem["G_PKLQD"] = "----";
                }
                #endregion


                #region 连接盘内侧环焊缝抗剪强度
                int PNFKJQDBhg = 0;
                if (jcxm.Contains("、连接盘内侧环焊缝抗剪强度、"))
                {
                    jcxmCur = "连接盘内侧环焊缝抗剪强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["PNFKJQD" + i]) != 1)
                        {
                            PNFKJQDBhg = PNFKJQDBhg + 1;
                        }
                    }
                    if (PNFKJQDBhg > 1)
                    {
                        sItem["HG_PNFKJQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_PNFKJQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        
                    }

                }
                else
                {
                    sItem["HG_PNFKJQD"] = "----";
                    sItem["G_PNFKJQD"] = "----";
                }
                #endregion

                #region 可调托撑和可调底座抗压强度
                int CZKYQDBhg = 0;
                if (jcxm.Contains("、可调托撑和底座抗压强度、"))
                {
                    jcxmCur = "可调托撑和可调底座抗压强度";
                    for (int i = 1; i < 9; i++)
                    {
                        if (GetSafeInt(sItem["CZKYQD" + i]) != 1)
                        {
                            CZKYQDBhg = CZKYQDBhg + 1;
                        }
                    }
                    if (CZKYQDBhg > 1)
                    {
                        sItem["HG_CZKYQD"] = "不合格";
                        sItem["JCJG"] = "不合格";
                        jcxmBhg += jcxmBhg.Contains(jcxmCur) ? "" : jcxmCur + "、";
                        mAllHg = false;
                        jcxmBhgZs = jcxmBhgZs + 1;
                    }
                    else
                    {
                        sItem["HG_CZKYQD"] = "合格";
                        sItem["JCJG"] = "合格";
                        
                    }

                }
                else
                {
                    sItem["HG_CZKYQD"] = "----";
                    sItem["G_CZKYQD"] = "----";
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