using CalDebugTools.Common.DBUtility;
using CalDebugTools.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CalDebugTools
{
    public class ProjectInfos
    {
        private Common.DBUtility.SqlBase _sqlBase = null;
        List<ProjectInfo> _listProInfo = new List<ProjectInfo>();
        public ProjectInfos()
        {
            if (_sqlBase == null)
            {
                _sqlBase = new Common.DBUtility.SqlBase(ESqlConnType.ConnectionStringDebugTool);
            }
        }
        public DataSet GetAllProjectInfos()
        {
            DataSet Ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringDebugTool"].ToString()))
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * from ProjectInfo", conn);


                sda.Fill(Ds, "ProjectInfo");

            }

            return Ds;
        }


        public ProjectInfo GetProjectInfoByBH(string BH)
        {
            string sqlStr = $"select * from ProjectInfo where BH ='{BH}'";

            ProjectInfo projectInfo = null;
            DataSet ds = _sqlBase.ExecuteDataset(sqlStr);

            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    projectInfo = new ProjectInfo();

                    projectInfo.BH = dr["BH"].ToString();
                    projectInfo.MTable = dr["MTable"].ToString();
                    projectInfo.STable = dr["STable"].ToString();
                    projectInfo.BZTable = dr["BZTable"].ToString();
                    projectInfo.YTable = dr["YTable"].ToString();
                    projectInfo.DataFiled = dr["DataFiled"].ToString();
                }
            }

            return projectInfo;
        }
        public int DataInsert(ProjectInfo info)
        {
            if (info == null)
            {
                return -1;
            }

            string sqlStr = $"select * from  PR_M_SYXM where SYXMBH  = '{info.BH}'";
            var _sqlBase2 = new Common.DBUtility.SqlBase(ESqlConnType.ConnectionStringWH);
            var dfd = _sqlBase2.ExecuteScalar(sqlStr);
            if (dfd == null)
            {
                return -3;
            }
            sqlStr = $"select * from  ProjectInfo where BH = '{info.BH}'";


            dfd = _sqlBase.ExecuteScalar(sqlStr);
            if (dfd != null)
            {
                return -2;
            }

            sqlStr = $" insert into ProjectInfo  ([BH],[MTable],[STable],[BZTable],[YTable],[DataFiled]) values('{info.BH}','{info.MTable}','{info.STable}','{info.BZTable}','{info.YTable}','{info.DataFiled}') ";

            return _sqlBase.ExecuteNonQuery(sqlStr);
        }
        public void InsertTableInfo()
        {
            for (int i = 0; i < 100; i++)
            {
                string sqlStr = $"insert into BZ_GCCJPD(CJCS) VALUES('{25 + i}')";
                _sqlBase.ExecuteDataset(sqlStr);
            }

        }

        public int UpdateProInfos(ProjectInfo info)
        {
            if (info == null)
            {
                return -1;
            }

            string sqlStr = $"update ProjectInfo set MTable='{info.MTable}',[STable]='{info.STable}',[BZTable]='{info.BZTable}',[YTable]='{info.YTable}',[DataFiled]='{info.DataFiled}' where [ID] ='{info.ID}'";
            return _sqlBase.ExecuteNonQuery(sqlStr);
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="BH">试验项目编号</param>
        /// <param name="jydbh">主从表关联号</param>
        /// <returns></returns>
        public string GetPar(string BH, List<string> fields, string ytable, string datafiled, ESqlConnType connType, string jydbh = "")
        {
            List<string> listDataJson = new List<string>();
            string ParData = "";

            if (fields.Count != 2)
            {
                return "";
            }
            string mFields = fields[0];
            string sFields = fields[1];

            sFields = string.IsNullOrEmpty(sFields) ? "* ,jcxm" : (sFields + ",jcxm");
            string sqlStr = $"select {sFields} from S{BH} where  JYDBH='{jydbh}'";
            try
            {
                string m_json = "";
                string y_json = "";
                if (string.IsNullOrEmpty(mFields))
                {
                    mFields = "JCYJ,PDBZ,SYRQ";
                }
                else
                {
                    mFields += ",JCYJ,PDBZ,SYRQ";
                }

                mFields = mFields.Replace("JCJGMS", "JGSM");

                //获取测试数据
                if (!string.IsNullOrEmpty(mFields))
                {
                    m_json = JsonHelper.GetMdataJson($"select {mFields} from  M{BH} where JYDBH ='{jydbh}' ", $"M_{BH}", connType);
                }

                //获取数据表
                if (!string.IsNullOrEmpty(ytable))
                {
                    y_json = GetYtableJson(ytable, datafiled, BH, jydbh, connType);
                }
                var retSDataJosn = JsonHelper.GetAfferentDataJson2($"S_{BH}", sqlStr, connType, m_json, y_json);
                listDataJson.Add(retSDataJosn);
            }
            catch (Exception)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder("");

            for (int i = 0; i < listDataJson.Count; i++)
            {
                sb.Append(listDataJson[i] + "\r\n");
            }
            ParData = sb.ToString();
            return ParData;

        }
        public string GetPar2(string BH, List<string> fields, string ytable, string datafiled, ESqlConnType connType, string wtdbh)
        {
            List<string> listDataJson = new List<string>();
            string ParData = "";

            if (fields.Count != 2)
            {
                return "";
            }
            string mFields = fields[0];
            string sFields = fields[1];
            string sqlStr = "";
            //通过

            sFields = string.IsNullOrEmpty(sFields) ? "* ,jcxm" : (sFields + ",jcxm");
            sqlStr = $"select {sFields} from S_{BH} where  BYZBRECID=(select RECID from M_BY where WTDBH = '{wtdbh}')";
            try
            {
                string m_json = "";
                //获取测试数据
                if (string.IsNullOrEmpty(mFields))
                {
                    mFields = "JCYJ,PDBZ";
                }
                else
                {
                    mFields += ",JCYJ,PDBZ";
                }
                mFields = mFields.Replace("JCJGMS,", "");
                m_json = JsonHelper.GetMdataJson($"select {mFields} from  M_{BH} join M_BY on M_{BH}.RECID=M_BY.RECID  where WTDBH = '{wtdbh}' ", $"M_{BH}", connType);

                //获取数据表
                //if (!string.IsNullOrEmpty(ytable))
                //{
                //    y_json = GetYtableJson(ytable, datafiled, BH, jydbh, connType);
                //}

                var retSDataJosn = JsonHelper.GetAfferentDataJson2($"S_{BH}", sqlStr, connType, m_json);
                listDataJson.Add(retSDataJosn);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            StringBuilder sb = new StringBuilder("");

            for (int i = 0; i < listDataJson.Count; i++)
            {
                sb.Append(listDataJson[i] + "\r\n");
            }
            ParData = sb.ToString();
            return ParData;

        }


        public string GetYtableJson(string ytable, string datafiled, string BH, string jydbh, ESqlConnType connType)
        {
            string y_json = "";

            string[] ytablist = ytable.Split('|');
            string[] filedlist = datafiled.Split('|');
            for (int i = 0; i < ytablist.Length; i++)
            {
                string where = string.Empty;
                string[] fil = filedlist[i].Split(',');
                foreach (var item in fil)
                {
                    string item_s = string.Empty;
                    string item_y = string.Empty;
                    if (item.Contains(":"))
                    {
                        item_s = item.Split(':')[0];
                        item_y = item.Split(':')[1];
                    }
                    else
                    {
                        item_s = item;
                        item_y = item;
                    }

                    if (item.ToUpper().Contains("SYLB"))
                        where += " and sylb = '" + BH + "'";
                    else
                        where += " and [" + item_y + "] in (select [" + item_s + "] from S" + BH + " where JYDBH = '" + jydbh + "')";
                }
                #region 获取数据表字段
                var _sqlBase_jcjt = new Common.DBUtility.SqlBase(ESqlConnType.ConnectionStringMain);
                string result = string.Empty;
                string sqlStr = $"select ZDMC  from  ZDZD_{BH} where ( SJBMC = '{ytablist[i]}') and( lx like '%I%' or lx like '%O%')";
                var redata = _sqlBase_jcjt.ExecuteDataset(sqlStr);

                if (redata != null)
                {
                    foreach (DataRow item in redata.Tables[0].Rows)
                    {
                        result += item["ZDMC"].ToString() + ",";
                    }
                }
                if (result.Length > 0)
                {
                    result = result.Substring(0, result.Length - 1);
                }
                #endregion
                string temjson = JsonHelper.GetMdataJson($"select {result} from  {ytablist[i]} where 1=1 {where}", ytablist[i], connType);
                y_json += temjson;
            }

            return y_json;
        }


        public DataSet GetChiFengDate(string BH, string jydbh, string jydbhTo = "")
        {
            SqlBase sqlbase = new SqlBase(ESqlConnType.ConnectionStringCF);
            string where = "where 1=1";
            int top = 100;

            if (!string.IsNullOrEmpty(jydbh))
            {
                where = $" where   JYDBH={jydbh}";
            }
            if (!string.IsNullOrEmpty(jydbh) && !string.IsNullOrEmpty(jydbhTo))
            {
                where = $" where   JYDBH>={jydbh}";
                top = 9999;
            }

            if (!string.IsNullOrEmpty(jydbhTo))
            {
                where += $" and  JYDBH<={jydbhTo}";
            }
            string sqlStr = $"select top　{top} * from M{BH}   {where} and BGBH <>'' order by RECID DESC";

            DataSet mdata = sqlbase.ExecuteDataset(sqlStr);

            return mdata;
        }
        public DataSet GetParmsCF(string BH, List<string> lisFields, string jydbh)
        {
            SqlBase baseChifeng = new SqlBase(ESqlConnType.ConnectionStringCF);

            if (lisFields.Count != 2)
            {
                return null;
            }

            string sfield = lisFields[1];

            sfield = string.IsNullOrEmpty(sfield) ? "*" : sfield;
            string extra_sql = string.Format($"select {sfield} from S{BH} where  JYDBH='{jydbh}'");
            DataSet extra_dt = baseChifeng.ExecuteDataset(extra_sql);
            return extra_dt;
        }

        public DataSet GetParmsWH(string BH, List<string> lisFields, string wtdbh)
        {
            SqlBase baseChifeng = new SqlBase(ESqlConnType.ConnectionStringWH);

            if (lisFields.Count != 2)
            {
                return null;
            }

            string sfield = lisFields[1];

            sfield = string.IsNullOrEmpty(sfield) ? "*" : sfield;
            string extra_sql = string.Format($"select {sfield} from S_{BH} where  BYZBRECID=(select RECID from M_BY where WTDBH = '{wtdbh}')");
            DataSet extra_dt = baseChifeng.ExecuteDataset(extra_sql);
            return extra_dt;
        }

        /// <summary>
        /// 获取Y数据表
        /// </summary>
        /// <param name="lisFields"></param>
        /// <param name="jydbh"></param>
        /// <param name="ytable"></param>
        /// <returns></returns>
        public DataSet GetParmsYtable(List<string> lisFields, string jydbh, string ytable)
        {
            SqlBase baseChifeng = new SqlBase(ESqlConnType.ConnectionStringCF);

            if (lisFields.Count != 2)
            {
                return null;
            }

            string sfield = lisFields[1];

            sfield = string.IsNullOrEmpty(sfield) ? "*" : sfield;
            string extra_sql = string.Format($"select {sfield} from ytable where  JYDBH='{jydbh}'");
            DataSet extra_dt = baseChifeng.ExecuteDataset(extra_sql);
            return extra_dt;
        }

        /// <summary>
        /// 获取帮助表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //public string GetExtra(string tableName)
        //{
        //    string extraDJjson = Common.DBUtility.JsonHelper.GetDataJson("select * from GHFDJ ", "BZ_GHF_DJ");
        //    return extraDJjson;
        //}
    }
}
