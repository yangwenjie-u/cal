using CalDebugTools.Common.DBUtility;
using CalDebugTools.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalDebugTools.DAL
{
    public class FieldManage
    {
        private Common.DBUtility.SqlBase _sqlBase = null;
        private Common.DBUtility.SqlBase _sqlDebugTool = null;
        public FieldManage()
        {
      
            if (_sqlBase == null)
            {
                _sqlBase = new Common.DBUtility.SqlBase(ESqlConnType.ConnectionStringMain);
            }
            if (_sqlDebugTool == null)
                _sqlDebugTool = new SqlBase(ESqlConnType.ConnectionStringDebugTool);
        }

        public int InsertFields(string xmbh, string jcxm, string sjbmc, string field, string lx)
        {
            try
            {
                string sqlStr = string.Format("select  SJBMC,ZDMC,LX,SSJCX  from [dbo].[ZDZD_{0}] where sjbmc ='{1}' and ZDMC='{2}'", xmbh, sjbmc, field);

                var ds = _sqlDebugTool.ExecuteDataset(sqlStr);
                if (null == ds)
                {
                    return -1;
                }

                DataTable dt = ds.Tables[0];
                string dtLX = dt.Rows[0]["LX"].ToString();
                string dtSSJCX = dt.Rows[0]["SSJCX"].ToString();
                //J
                //、抗冻性、
                if (dtLX.Length == 0)
                {
                    dtLX = lx;
                }
                else
                {
                    dtLX += dtLX.Contains(lx) ? "" : "," + lx;
                }

                if (dtSSJCX.Length == 0)
                {
                    dtSSJCX = $"、{jcxm}、";
                }
                else
                {
                    dtSSJCX += dtSSJCX.Contains(jcxm) ? "" : $"{jcxm}、";
                }

                sqlStr = $" Update  ZDZD_{xmbh} set  LX = '{dtLX}',SSJCX ='{dtSSJCX}'  where sjbmc ='{sjbmc}' and ZDMC='{field}'";

                return _sqlDebugTool.ExecuteNonQuery(sqlStr);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取字段
        /// </summary>
        /// <param name="xmbh"></param>
        /// <param name="zdmcs"></param>
        /// <returns></returns>
        public DataSet GetZdzdByZdmc(string xmbh, List<string> zdmcs, string table_name)
        {
            try
            {
                string sqlStr = "";
                string whereStr = "";
                DataSet ds = null;

                foreach (var item in zdmcs)
                {
                    whereStr += $"  ZDMC like '{item.Trim()}%' or";
                }
                whereStr = whereStr.Substring(0, whereStr.Length - 2) + ")";
                //if (!mtab)
                //{
                sqlStr = $"select SJGJ_ID,SJBMC,ZDMC,SY,SSJCX,LX  from  ZDZD_{xmbh} where SJBMC in('{table_name.Replace(",", "','")}')  and (  ";
                //(lx like '%w%' or LX like 'S') and SFXS = 1
                //}
                //else
                //{
                //    sqlStr = $"select SJGJ_ID, SJBMC,ZDMC,SY,SSJCX,LX  from  ZDZD_{xmbh} where SJBMC ='M_{xmbh}' and (  ";
                //}

                ds = _sqlDebugTool.ExecuteDataset(sqlStr + whereStr);

                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<string> GetInputFields(string xmbh, string table_name)
        {
            List<string> lst = new List<string>();

            string sqlStr = $"select SJGJ_ID,SJBMC,ZDMC,SY,SSJCX,LX  from  ZDZD_{xmbh} where SJBMC ='{table_name}' and SFXS = 1 and(lx like '%w%' or LX like 'S') ";
            var s_ds = _sqlBase.ExecuteDataset(sqlStr);

            if (s_ds != null)
            {
                foreach (DataRow item in s_ds.Tables[0].Rows)
                {
                    lst.Add(item["ZDMC"].ToString());
                }
            }
            return lst;
        }

        public string GetFieldsResult(DataSet redata, string strJcxm)
        {
            string result = "";
            List<string> lstJcxm = strJcxm.Replace(',', '、').Split('、').ToList();

            foreach (DataRow item in redata.Tables[0].Rows)
            {
                if (item["ZDMC"].ToString() == "SLQD1")
                {

                }
                if (!string.IsNullOrEmpty(item["SSJCX"].ToString()))
                {
                    foreach (string jcxm in lstJcxm)
                    {
                        if (item["SSJCX"].ToString().Replace(",", "、").Contains("、" + jcxm + "、") && !result.Contains($",{item["ZDMC"]},"))
                        {
                            result += item["ZDMC"].ToString() + ",";
                        }
                    }
                }
                else
                {
                    result += item["ZDMC"].ToString() + ",";
                }
            }

            return result;
        }

        public List<string> GetIOFields(string xmbh, string wtdbh)
        {
            List<string> lisResult = new List<string>();
            string result = "";

            string strJcxm = "";
            var jcxmData = _sqlBase.ExecuteDataset($"select jcxm from s_{xmbh}  where BYZBRECID in(select RECID from M_BY where WTDBH = '{wtdbh}' AND YTDWBH in('{FormMain._qybh.Replace(",", "','")}') )");

            if (jcxmData != null)
            {
                foreach (DataRow item in jcxmData.Tables[0].Rows)
                {
                    strJcxm += item["jcxm"].ToString();
                }
            }

            string sqlStr = $"select ZDMC,SSJCX from  ZDZD_{xmbh} where ( SJBMC = 'M_{xmbh}') and( lx like '%I%' or lx like '%O%')";
            var redata = _sqlDebugTool.ExecuteDataset(sqlStr);

            if (redata != null)
            {
                result = GetFieldsResult(redata, strJcxm);
            }

            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            lisResult.Add(result);

            //从表
            sqlStr = $"select ZDMC,SSJCX from  ZDZD_{xmbh} where (SJBMC = 'S_{xmbh}') and(lx like '%I%' or lx like '%O%')";
            redata = _sqlDebugTool.ExecuteDataset(sqlStr);

            result = "";
            if (redata != null)
            {
                result = GetFieldsResult(redata, strJcxm);
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            lisResult.Add(result);
            return lisResult;

        }
        public List<string> GetIFields(string xmbh, string wtdbh)
        {
            List<string> lisResult = new List<string>();
            string result = "";
            string strJcxm = "";
            var jcxmData = _sqlBase.ExecuteDataset($"select jcxm from s_{xmbh}  where BYZBRECID in(select RECID from M_BY where WTDBH = '{wtdbh}' AND YTDWBH in('{FormMain._qybh.Replace(",", "','")}'))");

            if (jcxmData != null)
            {
                foreach (DataRow item in jcxmData.Tables[0].Rows)
                {
                    strJcxm += item["jcxm"].ToString();
                }
            }
            List<string> lstJcxm = strJcxm.Replace(',', '、').Split('、').ToList();

            string sqlStr = $"select ZDMC ,SSJCX from  ZDZD_{xmbh} where ( SJBMC = 'M_{xmbh}') and( lx like '%I%')";
            //string sqlStr = $"select ZDMC ,SSJCX from  ZDZD_{xmbh} where ( SJBMC = 'M_{xmbh}') and( lx like '%I%') and mustin= '1'";
            var redata = _sqlDebugTool.ExecuteDataset(sqlStr);

            if (redata != null)
            {
                result = GetFieldsResult(redata, strJcxm);
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            lisResult.Add(result);

            //从表
            sqlStr = $"select ZDMC,SSJCX  from  ZDZD_{xmbh} where (SJBMC = 'S_{xmbh}') and(lx like '%I%')";
            //sqlStr = $"select ZDMC,SSJCX  from  ZDZD_{xmbh} where (SJBMC = 'S_{xmbh}') and(lx like '%I%') and mustin= '1'";
            redata = _sqlDebugTool.ExecuteDataset(sqlStr);

            result = "";
            if (redata != null)
            {
                result = GetFieldsResult(redata, strJcxm);
            }
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 1);
            }
            lisResult.Add(result);
            return lisResult;

        }
        public int UpdateZdzd(string xmbh, string recid, string lx, string SSJCX)
        {
            try
            {
                //SJBMC,ZDMC,SY,SSJCX,LX from
                string sqlStr = $"update ZDZD_{xmbh} set LX='{lx}',SSJCX='{SSJCX}'  where SJGJ_ID ='{recid}'  ";
                var result = _sqlDebugTool.ExecuteNonQuery(sqlStr);

                return result;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        /// <summary>
        /// 获取参数（乌海）
        /// </summary>
        /// <param name="zdzdParms"></param>
        /// <param name="JYDBH"></param>
        /// <returns></returns>
        public string GetParams(List<string> zdzdParms, string JYDBH)
        {
            //string jcxmBH = this.txt_jcxmbh.Text.Trim();

            //string xmbh = this.txt_jcxmbh.Text.Trim();


            //// zdzd表中获取需要获取的参数  

            ////调式 时 赤峰
            //string strParams = _projectInfo.GetPar(jcxmBH, zdzdParms, txt_y.Text, txtdatafiled.Text, ESqlConnType.ConnectionStringCF, JYDBH);

            //if (string.IsNullOrWhiteSpace(strParams.Trim()))
            //{
            //    MessageBox.Show("参数数据不能为空！", "调试", MessageBoxButtons.OK);
            return "";
            //}
            //return strParams;

        }
        public int InsertParam(CalculateParam param)
        {
            try
            {
                var cout = 0;
                string sqlStr = string.Format($"select * from H_Calculate_Param where SYXMBH ='{param.SYXMBH}' and RemoteTableName ='{param.RemoteTableName}' and RemoteZdName='{param.RemoteZdName}'");
                var ds = _sqlBase.ExecuteDataset(sqlStr);
                if (ds != null && ds.Tables[0].Rows.Count != 0)
                {
                    //已有字段
                    return -2;
                }

                sqlStr = $"select 1 from ZDZD_{param.SYXMBH} where ZDMC ='{param.RemoteZdName}' and  SJBMC ='{param.RemoteTableName}'";

                cout = _sqlBase.ExecuteNonQuery(sqlStr);
                if (cout > 0)
                {
                    return 1;
                }
                //sqlStr = $" INSERT INTO [dbo].[ZDZD_{param.SYXMBH}]([SJBMC],[ZDMC],[SFXS],[MUSTIN],[LX],[SY])     VALUES('{param.RemoteTableName}','{param.RemoteZdName}',0,0,'H,I','配置字段')";
                sqlStr = $"insert into ZDZD_{param.SYXMBH} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
                        $"VALUES('{param.RemoteTableName}', '{param.RemoteZdName}', '配置字段', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', 'False', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', 'H,I', NULL, NULL, NULL, NULL, NULL, NULL)  " +
                        $"";
                cout = _sqlDebugTool.ExecuteNonQuery(sqlStr);
                if (cout > 0)
                {
                    sqlStr = $"insert into   H_Calculate_Param  (SYXMBH,JCXM,LocalTableName,LocalZdName,RemoteTableName,RemoteZdName) values('{param.SYXMBH}','{param.JCXM}','{param.LocalTableName}','" +
                        $"{param.LocalZdName}','{param.RemoteTableName}','{param.RemoteZdName}')";

                    return _sqlBase.ExecuteNonQuery(sqlStr);

                    //sqlStr = $"alter table {param.RemoteTableName} add {param.RemoteZdName} varchar(200) null ";
                    //return _sqlBase.ExecuteNonQuery(sqlStr);
                }

                //zdzd 插入失败
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int UpdateParam(CalculateParam info)
        {

            if (info == null)
            {
                return -1;
            }
            string sqlStr = "";
            sqlStr = string.Format($"select * from H_Calculate_Param where recid ='{info.Recid}' ");

            var ds = _sqlBase.ExecuteDataset(sqlStr);
            if (ds != null && ds.Tables[0].Rows.Count != 0)
            {
                var fieldName = ds.Tables[0].Rows[0]["RemoteZdName"];

                sqlStr = $"update H_Calculate_Param set SYXMBH = '{info.SYXMBH}', JCXM = '{info.JCXM}', LocalTableName = '{info.LocalTableName}'," +
                    $" LocalZdName = '{info.LocalZdName}', RemoteTableName = '{info.RemoteTableName}', RemoteZdName = '{info.RemoteZdName}' where recid = '{info.Recid}';";
                _sqlBase.ExecuteNonQuery(sqlStr);

                sqlStr = $"update ZDZD_{info.SYXMBH}  set ZDMC = '{info.RemoteZdName}' where SJBMC = '{info.RemoteTableName}' and ZDMC = '{fieldName}'";

                return _sqlBase.ExecuteNonQuery(sqlStr);

            }
            return -1;

        }
        public DataSet GetSettingFieldsInfos()
        {
            DataSet Ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringMain"].ToString()))
            {
                SqlDataAdapter sda = new SqlDataAdapter("select * from H_Calculate_Param", conn);


                sda.Fill(Ds, "H_Calculate_Param");

            }
            return Ds;
        }

        /// <summary>
        /// 添加配置字段到物理表
        /// </summary>
        public void df()
        {
            try
            {
                DataSet ds = GetSettingFieldsInfos();
                var tableName = "";
                var xmbh = "";
                var fieldName = "";
                var sqlStr = "";
                var cout = 0;
                List<string> lst = new List<string>();
                foreach (var item in ds.Tables[0].Rows)
                {
                    xmbh = ((System.Data.DataRow)item).ItemArray[2].ToString();
                    tableName = ((System.Data.DataRow)item).ItemArray[6].ToString();
                    fieldName = ((System.Data.DataRow)item).ItemArray[7].ToString();

                    //sqlStr = $"alter table {tableName} add {fieldName} varchar(16) null ";
                    //if (!lst.Contains(sqlStr))
                    //{
                    //    lst.Add(sqlStr);
                    //}

                    sqlStr = $"select 1 from ZDZD_{xmbh} where ZDMC ='{fieldName}'";


                    var ds2 = _sqlBase.ExecuteDataset(sqlStr);
                    if (ds2 != null && ds2.Tables[0].Rows.Count == 0)
                    {
                        sqlStr = $"insert into ZDZD_{xmbh} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
$"VALUES('{tableName}', '{fieldName}', '配置字段', 'nvarchar', '200', '0', 'nvarchar', '', 'False', '', 'False', 'False', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', 'H,I', NULL, NULL, NULL, NULL, NULL, NULL)  " +
$"";
                        lst.Add(sqlStr);

                        //_sqlBase.ExecuteNonQuery(sqlStr);
                        //sqlStr = $" INSERT INTO [dbo].[ZDZD_{tableName}]([SJBMC],[ZDMC],[SFXS],[MUSTIN],[LX],[SY],ZDLX,ZDCD1,ZDCD2) VALUES('{tableName}','{fieldName}',0,0,'H,I','配置字段','nvarchar',30,0)";
                        continue;
                    }

                }


                foreach (var item in lst)
                {
                    try
                    {
                        _sqlBase.ExecuteNonQuery(item);

                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        /// <summary>
        /// 判断zdzd表是否存在
        /// </summary>
        /// <param name="zdzdName"></param>
        /// <returns></returns>
        public bool CheckZDZDIsTrue(string zdzdName)
        {

            string sqlStr = string.Format($"select top 1 *  from {zdzdName}");

            var ds = _sqlBase.ExecuteDataset(sqlStr);
            if (null != ds)
            {
                return true;
            }
            return false;
        }

        public bool InsertTableFieldToZDZD(string zdzdName, string tabName, string fieldName = "")
        {
            try
            {
                List<string> lst = new List<string>();
                var sy = "";
                string wheresql = string.IsNullOrEmpty(fieldName) ? "" : $" and a.name like '{fieldName}%'";
                string sqlStr = string.Format($"SELECT 表名 = D.name,字段序号 = A.colorder,字段名 = A.name,字段说明 = isnull(G.[value], ''),类型 = B.name,占用字节数 = A.Length " +
                    $"FROM syscolumns A  Left Join systypes B On A.xusertype = B.xusertype Inner Join sysobjects D On A.id = D.id and D.xtype = 'U' and D.name <> 'dtproperties'" +
                    $" Left Join syscomments E on A.cdefault = E.id Left Join sys.extended_properties G on A.id = G.major_id and A.colid = G.minor_id Left Join sys.extended_properties F On D.id = F.major_id and F.minor_id = 0 " +
                    $"where d.name = '{tabName}' "
                    + wheresql +
                    $"  Order By A.id, A.colorder");

                var ds = _sqlBase.ExecuteDataset(sqlStr);

                if (ds == null || ds.Tables[0].Rows.Count == 0)
                {
                    return false;
                }
                DataSet data = new DataSet();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sqlStr = $"select 1 from {zdzdName} where ZDMC ='{ds.Tables[0].Rows[i]["字段名"]}'  and SJBMC='{ds.Tables[0].Rows[i]["表名"]}'";

                    data = _sqlBase.ExecuteDataset(sqlStr);
                    if (data == null || data.Tables[0].Rows.Count != 0)
                    {
                        continue;
                    }
                    sy = !string.IsNullOrEmpty(ds.Tables[0].Rows[i]["字段说明"].ToString().ToUpper()) ? ds.Tables[0].Rows[i]["字段说明"].ToString().ToUpper() : ds.Tables[0].Rows[i]["字段名"].ToString().ToUpper();
                    sqlStr = $"insert into {zdzdName} ( SJBMC, ZDMC, SY, ZDLX, ZDCD1, ZDCD2, INPUTZDLX, KJLX, SFBHZD, BHMS,ZDSX, SFXS, XSCD, XSSX, SFGD, MUSTIN, DEFAVAL, HELPLNK, CTRLSTRING, ZDXZ,WXSSX, WSFXS, MSGINFO, EQLFUNC, HELPWHERE, GETBYBH, SSJCX, SFBGZD,VALIDPROC, LX, ZDSXSQL, ENCRYPT, FZYC, FZCS, NOSAVE, location)" +
                            $"VALUES('{tabName.ToUpper()}', '{ds.Tables[0].Rows[i]["字段名"].ToString().ToUpper()}', '{sy}', '{ds.Tables[0].Rows[i]["类型"]}', '{ds.Tables[0].Rows[i]["占用字节数"]}', '0', 'nvarchar', '', 'False', '', 'False', 'False', '0', '367.0000', 'False', 'False', '', '', '', 'S', '367.0000', 'True', '', '', '', 'True', '', 'True', '', '', NULL, NULL, NULL, NULL, NULL, NULL)  ";
                    lst.Add(sqlStr);
                    //cout = _sqlBase.ExecuteNonQuery(sqlStr);
                }

                foreach (var item in lst)
                {
                    _sqlBase.ExecuteNonQuery(item);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public void InsertBZInfos(List<string> sqlList)
        {
            foreach (var item in sqlList)
            {
                _sqlBase.ExecuteNonQuery(item);
            }
        }

    }
}
