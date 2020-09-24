using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates.CalculatesNew.qsf
{
    class test : BaseMethods
    {
        public void T()
        {
            #region 市政取芯数据录入求值
            sql = $"select m.*,s.jcxm,s.BZMD_S1 from {ysjbmc} m inner join {sjbmc} e on e.recid = m.sysjbrecid inner join s_lqx s on s.recid = e.sysjbrecid  where m.sysjbrecid = '{recid}' ";
            dt = CommonDao.GetDataTableTran(sql);
            if (dt.Count > 0)
            {
                var dic = dt[0];
                string jcxm = "、" + dic["jcxm"].Replace(',', '、') + "、";
                if (jcxm.Contains("、厚度、"))
                {
                    dic["hd"] = Math.Round((dic["schd1"].GetSafeDouble() + dic["schd2"].GetSafeDouble() + dic["schd3"].GetSafeDouble() + dic["schd4"].GetSafeDouble()) / 4, 2).ToString("0.00");
                }
                else
                {
                    dic["hd"] = "----";
                }

                if (jcxm.Contains("、密度（表干法、水中法、蜡封法）、"))
                {
                    if (dic["syff"] == "表干法" || dic["syff"] == "水中重法")
                    {
                        dic["md"] = Math.Round(dic["kqzzl"].GetSafeDouble() / (dic["bgzl"].GetSafeDouble() - dic["szzl"].GetSafeDouble()) * 0.9771, 3).ToString("F3");
                    }
                    else if (dic["syff"] == "蜡封法")
                    {
                        if (dic["sfhs"] == "是")   //用滑石粉
                        {
                            dic["md"] = Math.Round(dic["sjkz"].GetSafeDouble() / ((dic["lkzl"].GetSafeDouble() - dic["lszl"].GetSafeDouble()) - ((dic["lkzl"].GetSafeDouble() - dic["thkz"].GetSafeDouble()) / dic["slmd"].GetSafeDouble() + (dic["thkz"].GetSafeDouble() - dic["sjkz"].GetSafeDouble()) / dic["hsmd"].GetSafeDouble())) * 0.9771, 3).ToString("F3");
                        }
                        else
                        {
                            dic["md"] = Math.Round(dic["sjkz"].GetSafeDouble() / ((dic["lkzl"].GetSafeDouble() - dic["lszl"].GetSafeDouble()) - (dic["lkzl"].GetSafeDouble() - dic["sjkz"].GetSafeDouble()) / dic["slmd"].GetSafeDouble()) * 0.9771, 3).ToString("F3");
                        }
                    }
                    else
                    {
                        dic["syff"] = "----";
                    }

                    if (MatchValue.IsNumeric(dic["md"]) && MatchValue.IsNumeric(dic["BZMD_S1"]))
                    {
                        dic["YSD"] = Math.Round(dic["md"].GetSafeDouble() / dic["BZMD_S1"].GetSafeDouble() * 100, 2).GetSafeString();
                    }
                }
                else
                {
                    dic["syff"] = "----";
                }
                upsqls.Add($"update {ysjbmc} set hd = '{dic["hd"]}',ysd = '{dic["ysd"]}' where sysjbrecid = '{recid}'");
                upsqls.Add($"update {sjbmc} set SCHD = '{dic["hd"]}',SCYSD = '{dic["ysd"]}',MD = '{dic["md"]}' where recid = '{recid}'");
            }
            #endregion



        }


        public wh() { }
        public fy() { }
    }
}
