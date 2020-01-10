using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculates
{
    public class GSF : BaseMethods
    {
        //使用标准 GBT 14684-2011 建设用砂
        public void Calc(IDictionary<string, IList<IDictionary<string, string>>> dataExtra, ref IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> retData, ref string err)
        {
            /************************ 代码开始 *********************/
            var extraDJ = dataExtra["BZ_GSF_DJ"];

            var jcxmItems = retData.Select(u => u.Key).ToArray();
            
            //string jsbeizhu = "";
            bool mSFwc = true;
            bool mAllHg = true;
            string mSjdjbh , mSjdj = "";
            double vi = 0;
            double mSz, mQdyq, mgmd, msmd, mskys, msktj , mysd = 0;
            string mJSFF = "";
            int c_ht = 0;
            
            try
            {
                foreach (var jcxm in jcxmItems)
                {
                    int index = -1; ;
                    var SItem = retData[jcxm]["S_GSF"];
                    var MItem = retData[jcxm]["M_GSF"];
                    var XQData = retData[jcxm]["S_BY_RW_XQ"];
                    MItem[0]["JSBEIZHU"] = "";
                    foreach (var item in SItem)
                    {
                        index++;

                        //通用判断
                        if (null == MItem)
                        {
                            MItem[0]["BGBH"] = "0";
                            MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "规格不祥。";

                            mAllHg = false;
                            XQData[0]["JCJG"] = "不合格";
                            XQData[0]["JCJGMS"] = "获取不到主表数据";
                            continue;
                        }
                        //获取sub共有的字段
                        mSjdjbh = MItem[0]["SJDJBH"];//设计等级编号
                        mSjdj = MItem[0]["SJDJ"];//设计等级名称
                        if (string.IsNullOrEmpty(mSjdj))
                        {
                            mSjdj = "";
                            item["SJCC"] = "0";
                            item["HSXS"] = "0";
                            MItem[0]["BGBH"] = "0";
                            MItem[0]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "规格不祥。";

                            mAllHg = false;
                            XQData[0]["JCJG"] = "不合格";
                            XQData[0]["JCJGMS"] = "获取不到设计等级{SJDJ}";
                            continue;
                        }

                        if (string.IsNullOrEmpty(mSjdjbh))
                        {
                            mSjdjbh = "";
                        }
                        // 是否完成检测
                        if (string.IsNullOrEmpty(item["SYR"]))
                        {
                            mSFwc = false;
                        }

                        //从设计等级表中取得相应的计算数值、等级标准
                        var extraFieldsDJ = extraDJ.FirstOrDefault(u => u.Keys.Contains("MC") && u.Values.Contains(item["GGXH"]) && u.Keys.Contains("BH") && 
                        u.Values.Contains(item["GGXH"]));

                        if (null == extraFieldsDJ)
                        {
                            mSz = 0;
                            mQdyq = 0;
                            mJSFF = "";
                            item["JCJG"] = "依据不详";
                            MItem[index]["JSBEIZHU"] = MItem[0]["JSBEIZHU"] + "单组流水号:" + item["DZBH"] + "设计等级为空或不存在";
                            MItem[index]["BGBH"] = "";

                            mAllHg = false;
                            XQData[index]["JCJG"] = "不合格";
                            XQData[index]["JCJGMS"] = @"帮助表HNTGG中获取不到" + item["GGXH"] + "数据";
                            continue;
                        }
                        else
                        {
                            mSz = string.IsNullOrEmpty(extraFieldsDJ["SZ"]) ? 0 : GetSafeInt(extraFieldsDJ["SZ"]);
                            mQdyq = string.IsNullOrEmpty(extraFieldsDJ["QDYQ"]) ? 0 : GetSafeInt(extraFieldsDJ["QDYQ"]);
                            mJSFF = string.IsNullOrEmpty(extraFieldsDJ["JSFF"]) ? "" : extraFieldsDJ["QDYQ"].Trim().ToLower();
                        }

                        //计算龄期
                        item["LQ"] = (GetSafeDate(MItem[index]["SYRQ"]) - GetSafeDate(item["ZZRQ"])).Days.ToString();

                        if (mJSFF == "")
                        {//计算单组的抗压强度,并进行合格判断
                            if (GetSafeDouble(MItem[index]["ZJHSL"]) == 0 || GetSafeDouble(MItem[index]["ZJHSL"]) == -1)
                            {
                                MItem[index]["ZJHSL"] = "-1";
                                item["GMD"] = "0";

                            }
                            if (GetSafeDouble(MItem[index]["SJYSD"]) == 0 || GetSafeDouble(MItem[index]["SJYSD"]) == -1)
                            {
                                MItem[index]["SJYSD"] = "-1";
                                item["YSD"] = "0";
                            }
                            mskys = Math.Round(GetSafeDouble(item["RQYS"]) - GetSafeDouble(item["RQSS"]) - GetSafeDouble(MItem[index]["ZDYSL"]), 0);
                            msktj = Math.Round(mskys / GetSafeDouble(MItem[index]["BZSMD"]), 1);
                            //msyhsl = 100 * (sitem["ssyzl - sitem["gsyzl) / sitem["gsyzl
                            //msmd = sitem["ssyzl / msktj
                            item["SKYS"] = mskys.ToString();
                            item["SKTJ"] = msktj.ToString();
                            msmd = Round(GetSafeDouble(item["QBCL"]) / msktj, 2); //湿密度计算
                            item["SMD"] = msmd.ToString();
                            item["SZL1"] = (GetSafeDouble(item["HJST1"]) - GetSafeDouble(item["HJGT1"])).ToString();//水质量计算  
                            item["SZL2"] = (GetSafeDouble(item["HJST2"]) - GetSafeDouble(item["HJGT2"])).ToString();
                            item["GTZL1"] = (GetSafeDouble(item["HJGT1"]) - GetSafeDouble(item["HZL1"])).ToString();//干土质量计算
                            item["GTZL2"] = (GetSafeDouble(item["HJGT2"]) - GetSafeDouble(item["HZL2"])).ToString();
                            item["HSL1"] = Math.Round(GetSafeDouble(item["SZL1"])/GetSafeDouble(item["GTZL1"])*100, 1).ToString();//含水量计算
                            item["HSL2"] = Math.Round(GetSafeDouble(item["SZL2"])/GetSafeDouble(item["GTZL2"])*100, 1).ToString();
                            item["PJHSL"] = Math.Round((GetSafeDouble(item["HSL1"]) - GetSafeDouble(item["HSL2"])) / 2, 1).ToString();//平均含水量

                            mgmd = Math.Round(msmd / (1 + (0.01 * GetSafeDouble(item["PJHSL"]))), 2); //干密度计算
                            item["GMD"] = mgmd.ToString();

                            if (GetSafeDouble(MItem[index]["ZDGMD"]) != 0)
                            {
                                mysd = Math.Round(100 * (GetSafeDouble(item["GMD"]) / GetSafeDouble(MItem[index]["ZDGMD"])), 0);
                                item["YSD"] = mysd.ToString();

                            }
                            else
                            {
                                item["YSD"] = "0";
                            }
                            if (GetSafeDouble(MItem[index]["SJYSD"]) == -1 && GetSafeDouble(MItem[index]["ZJHSL"]) > 0)
                            {
                                if (GetSafeDouble(item["GMD"]) >= GetSafeDouble(MItem[index]["zjhsl"]))
                                {
                                    vi = vi + 1;
                                }
                            }
                            else
                            {
                                if (GetSafeDouble(item["YSD"]) >= GetSafeDouble(MItem[index]["SJYSD"]))
                                {
                                    vi = vi + 1;
                                }
                            }
                        }


                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "获取不到主表数据";
                        //-------------------------------------综合判断--------------------------------------------
                        //平均值,最小值等计算
                        MItem[index]["HGDS"] = vi.ToString();
                        //c_Ht = mrssubTable.RecordCount  c_ht为从表记录条数
                        MItem[index]["JCDS"] = c_ht.ToString();//
                        MItem[index]["HGL"] = (Math.Round((vi / c_ht) * 100, 1)).ToString();
                        if (vi == c_ht)
                        {
                            mAllHg = true;
                        }
                        else
                        {
                            mAllHg = false;
                        }
                        //主表总判断赋值
                        if (mAllHg)
                        {
                            MItem[index]["JCJG"] = "合格";
                        }
                        else
                        {
                            MItem[index]["JCJG"] = "不合格";

                        }
                        XQData[index]["JCJG"] = "不合格";
                        XQData[index]["JCJGMS"] = "单组流水号: " + item["DZBH"] + "设计等级为空或不存在";
                    }
                }
            

                #region 添加最终报告

                IList<IDictionary<string, string>> bgjg = new List<IDictionary<string, string>>();
                IDictionary<string, string> bgjgDic = new Dictionary<string, string>();
                if (!mAllHg)
                {
                    //不合格
                    bgjgDic.Add("JCJG", "不合格");
                    bgjgDic.Add("JCJGMS", $"该组试样所检项目符合不标准要求。");
                }
                else
                {
                    bgjgDic.Add("JCJG", "合格");
                    bgjgDic.Add("JCJGMS", $"该组试样所检项目符合标准要求。");
                }

                bgjg.Add(bgjgDic);
                retData["BGJG"] = new Dictionary<string, IList<IDictionary<string, string>>>();
                retData["BGJG"].Add("BGJG", bgjg);
                #endregion
            }
            catch (Exception ex)
            {

            }
            /************************ 代码结束 *********************/

        }
    }
}

