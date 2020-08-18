using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;


namespace Calculates
{
    public class JZL : BaseMethods
    {
        public void Calc()
        {
            #region 计算开始
            var data = retData;
            var mrsDj = dataExtra["BZ_SNK_DJ"];
            var MItem = data["M_SNK"];
            var mitem = MItem[0];
            var SItem = data["S_SNK"];
            bool mAllHg = true;


            var jcxm = "";

          


            foreach (var sitem in SItem)
            {


              


                var mrsDj_Filter = mrsDj.FirstOrDefault(x => x[""].Contains(""));
                if (mrsDj_Filter != null && mrsDj_Filter.Count() > 0)
                {
                  
                }
                else
                {
                    sitem["JCJG"] = "不下结论";
                    mitem["JCJGMS"] = "获取标准要求出错，找不到对应项";
                    continue;
                }


              
                jcxm = "、" + sitem["JCXM"].Replace(',', '、') + "、";

             
                if (jcxm.Contains("、下垂度、"))
                {
                }
                if (jcxm.Contains("、挤出性、"))
                {
                }
                if (jcxm.Contains("、适用性、"))
                {
                }
                if (jcxm.Contains("、密度、"))
                {
                }
                if (jcxm.Contains("、表干时间、"))
                {
                }
                if (jcxm.Contains("、弹性恢复率、"))
                {
                }
                if (jcxm.Contains("、拉伸模量、"))
                {
                }
                if (jcxm.Contains("、质量损失率、"))
                {
                }
                if (jcxm.Contains("、定伸粘接性、"))
                {
                }
                if (jcxm.Contains("、紫外线辐射后粘接性、"))
                {
                }
                if(jcxm.Contains("、浸水后定伸粘接性、"))
                {
                }

            }
            if (mAllHg == true)
            {
                mitem["JCJG"] = "合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目抗压强度符合要求";
            }
            else
            {
                mitem["JCJG"] = "不合格";
                mitem["JCJGMS"] = "依据" + mitem["PDBZ"] + "的规定，所检项目抗压强度不符合要求";
            }
          

            #endregion
        }
    }
}