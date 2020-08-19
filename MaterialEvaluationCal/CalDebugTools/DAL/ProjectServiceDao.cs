using CalDebugTools.Common;
using CalDebugTools.Common.DBUtility;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalDebugTools.DAL
{
    public class ProjectServiceDao
    {
        public void CreateProjectTable(string jcjgAbbrevition, string projectCode,out  string msg)
        {
            msg = "";
            SqlBase jcjtService = new SqlBase(ESqlConnType.ConnectionStringJCJT, jcjgAbbrevition);
            SqlBase jcjgService = new SqlBase(ESqlConnType.ConnectionStringJCJG, jcjgAbbrevition);
            SqlBase debugToolsService = new SqlBase(ESqlConnType.ConnectionStringDebugTool, jcjgAbbrevition);
            List<string> cmdList = new List<string>();
            string sqlstr = "";
            #region 创建基本表（7张）
            //创建M_DJ表
            sqlstr = string.Format($"CREATE TABLE [dbo].[M_DJ_{projectCode}]([RECID][nvarchar](50) NOT NULL) ON[PRIMARY];");
            cmdList.Add(sqlstr);

            //创建S_DJ表
            sqlstr = string.Format($"CREATE TABLE [dbo].[S_DJ_{projectCode}]([RECID][nvarchar](50) NOT NULL,[BYZBRECID][nvarchar](50) NULL," +
                $"[YPSL][nvarchar](50) NULL,[BWSX][nvarchar](120) NULL, [JCXM][varchar](max) NULL) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] ;");
            cmdList.Add(sqlstr);

            //创建M_D表
            sqlstr = string.Format($"CREATE TABLE[dbo].[M_D_{projectCode}]([RECID][nvarchar](50) NULL) ON[PRIMARY];");
            cmdList.Add(sqlstr);
            //创建S_D表
            sqlstr = string.Format($"CREATE TABLE[dbo].[S_D_{projectCode}]([RECID][nvarchar](50) NULL,[BYZBRECID][nvarchar](50) NULL) ON[PRIMARY];");
            cmdList.Add(sqlstr);

            //创建帮助表 BZ_{项目代号}_DJ
            sqlstr = string.Format($"CREATE TABLE [dbo].[BZ_{projectCode}_DJ]([RECID][int] IDENTITY(1, 1) NOT NULL,[BGNAME][nvarchar](10) NULL," +
                $"[BH][nvarchar](5) NULL,[GFZJ][nvarchar](100) NULL,[JCBZ][nvarchar](60) NULL,[JCYJ][nvarchar](250) NULL," +
                $"[JSFF][nvarchar](3) NULL,[MC][nvarchar](20) NULL,[PDBZ][nvarchar](250) NULL,[QDYQ][numeric](12, 4) NULL," +
                $"[SZ][numeric](12, 4) NULL,[ZJM][nvarchar](10) NULL) ON[PRIMARY];");
            cmdList.Add(sqlstr);

            //创建主表
            sqlstr = string.Format($"CREATE TABLE [dbo].[M_{projectCode}]([RECID][nvarchar](50) NOT NULL,[JCJGMS][varchar](max) NULL," +
                $"[JCJG][nvarchar](10) NULL,[SYWD][nvarchar](30) NULL,[YCQK][nvarchar](200) NULL,[SBMCBH][nvarchar](200) NULL)" +
                $" ON[PRIMARY] TEXTIMAGE_ON[PRIMARY];");
            cmdList.Add(sqlstr);

            //创建从表
            sqlstr = string.Format($"CREATE TABLE [dbo].[S_{projectCode}]([RECID][nvarchar](50) NOT NULL,[BYZBRECID][nvarchar](50) NULL," +
                $"[YPSL][nvarchar](50) NULL,[BWSX][nvarchar](120) NULL,[JCJG][nvarchar](10) NULL,[JCXM][varchar](max) NULL," +
                $"[SJ_ZS][int] NULL,[SBMCBH][nvarchar](200) NULL) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY];");
            cmdList.Add(sqlstr);
            #endregion

            #region zdzd表
            //创建表
            //zdzd表需要在caldebug数据库中创建一个
            List<string> cmdList_zdzd = new List<string>();
            string sqlstr_zdzd = "";
            sqlstr_zdzd = string.Format($"CREATE TABLE [dbo].[ZDZD_{projectCode}]([SJGJ_ID][int] IDENTITY(1, 1) NOT NULL,[SJBMC][nvarchar](20) NOT NULL," +
                $"[ZDMC][nvarchar](20) NOT NULL,[SY][nvarchar](50) NULL,[ZDLX][nvarchar](20) NULL,[ZDCD1][int] NULL,[ZDCD2][int] NULL," +
                $"[INPUTZDLX][nvarchar](20) NULL,[KJLX][nvarchar](20) NULL,[SFBHZD][bit] NULL,[BHMS][nvarchar](200) NULL,[ZDSX][bit] NULL," +
                $"[SFXS][bit] NULL,[XSCD][int] NULL,[XSSX][numeric](12, 4) NULL,[SFGD][bit] NULL,[MUSTIN][bit] NULL,[DEFAVAL][nvarchar](100) NULL," +
                $"[HELPLNK][text] NULL,[CTRLSTRING][text] NULL,[ZDXZ][varchar](20) NULL,[WXSSX][numeric](12, 4) NULL,[WSFXS][bit] NULL," +
                $"[MSGINFO][varchar](80) NULL,[EQLFUNC][varchar](240) NULL,[HELPWHERE][varchar](100) NULL,[GETBYBH][bit] NULL,[SSJCX][text] NULL," +
                $"[SFBGZD][bit] NULL,[VALIDPROC][text] NULL,[LX][nvarchar](100) NULL,[ZDSXSQL][nvarchar](max) NULL,[ENCRYPT][bit] NULL," +
                $"[FZYC][bit] NULL,[FZCS][text] NULL,[NOSAVE][bit] NULL,[location][nvarchar](200) NULL,[bjzd][bit] NULL," +
                $"[Onchange_Fun][nvarchar](2000) NULL,[tabIndex][nvarchar](100) NULL,[BJLX][nvarchar](50) NULL,[location1][nvarchar](200) NULL," +
                $"[Onchange_Fun1][nvarchar](2000) NULL,[bjzd1][bit] NULL,[tabIndex1][nvarchar](50) NULL,PRIMARY KEY CLUSTERED( [SJGJ_ID] ASC)" +
                $"WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY])" +
                $" ON[PRIMARY] TEXTIMAGE_ON[PRIMARY];");
            cmdList.Add(sqlstr_zdzd);
            cmdList_zdzd.Add(sqlstr_zdzd);

            //添加数据
            DataSet ds_zdzd = jcjtService.ExecuteDataset("execute pro_GetTableInfoToInsert 'ZDZD_TTT1', 'SJBMC  <> ''''' ");

            foreach (DataRow item in ds_zdzd.Tables[0].Rows)
            {
                sqlstr_zdzd = "";
                for (int i = 0; i < item.ItemArray.Length; i++)
                {
                    if (string.IsNullOrEmpty(item[i].ToString()))
                    {
                        sqlstr_zdzd += "NULL";
                    }
                    else
                    {
                        sqlstr_zdzd += item[i].ToString();
                    }
                }
                sqlstr_zdzd = sqlstr_zdzd.Replace("{projectCode}", projectCode).Replace("ZDZD_TTT1", $"ZDZD_{projectCode}").Replace("SJGJ_ID,", "");
                sqlstr_zdzd = Strings.Replace(sqlstr_zdzd, "False", "0", 1, -1, CompareMethod.Text);
                sqlstr_zdzd = Strings.Replace(sqlstr_zdzd, "true", "1", 1, -1, CompareMethod.Text);
                sqlstr_zdzd = Regex.Replace(sqlstr_zdzd, $"values\\(\\s*[0-9]*\\s*,", "values (");//Regex.Replace
                cmdList.Add(sqlstr_zdzd);
                cmdList_zdzd.Add(sqlstr_zdzd);
            }
            #endregion

            #region 插入数据库
            int reflag = 0;
            //检测集团
            reflag = CheckProjectIsExist(jcjtService, projectCode);
            if (reflag == -2 || reflag > 0)
            {
                Log.Warn("新增项目", $"{jcjgAbbrevition}_检测集团数据库:新增项目失败，已存在项目{projectCode}。");
            }
            else
            {
                if (!jcjtService.ExecuteTrans(cmdList, out msg))
                {
                    Log.Warn("新增项目", $"{jcjgAbbrevition}_检测集团数据库:添加字段失败，数据已回滚。");
                }
            }
            //检测监管

            reflag = CheckProjectIsExist(jcjtService, projectCode);
            if (reflag == -2 || reflag > 0)
            {
                Log.Warn("新增项目", $"{jcjgAbbrevition}_检测监管数据库:新增项目失败，已存在项目{projectCode}。");
            }
            else
            {
                if (cmdList.Count > 0 && !jcjgService.ExecuteTrans(cmdList,out msg))
                {
                    Log.Warn("新增项目", $"{jcjgAbbrevition}_检测监管数据库:添加项目失败，数据已回滚。");
                }
            }
            //CalDebugTools
            reflag = CheckZDZD_ProjectIsExist(jcjtService, projectCode);
            if (reflag == -2 || reflag > 0)
            {
                Log.Warn("新增项目", $"CalDebugTools数据库:新增表失败，已存在表ZDZD_{projectCode}。");
            }
            else
            {
                if (cmdList_zdzd.Count > 0 && !debugToolsService.ExecuteTrans(cmdList_zdzd, out msg))
                {
                    Log.Warn("新增项目", $"CalDebugTools数据库:新增表ZDZD_{projectCode}及添加表数据失败，数据已回滚。");
                }
            }
            #endregion
        }

        /// <summary>
        /// 检查项目是否已经存在
        /// </summary>
        /// <param name="dbService"></param>
        /// <param name="projectCode">项目代号</param>
        /// <returns></returns>
        public int CheckProjectIsExist(SqlBase dbService, string projectCode)
        {
            string sqlstr = string.Format($"select * from  pr_m_syxm where syxmbh='{projectCode}';");
            try
            {
                return dbService.ExecuteScalar(sqlstr) == null ? 0 : 1;
            }
            catch
            {
                return -2;
            }
        }

        /// <summary>
        /// 检查项目对应zdzd表是否存在
        /// </summary>
        /// <param name="dbService"></param>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public int CheckZDZD_ProjectIsExist(SqlBase dbService, string projectCode)
        {
            string sqlstr = string.Format($"select * from information_schema.columns where table_name ='{projectCode}';");
            try
            {
                return dbService.ExecuteScalar(sqlstr) == null ? 0 : 1;
            }
            catch
            {
                return -2;
            }
        }
    }
}
