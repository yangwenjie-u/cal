using CalDebugTools.Common;
using CalDebugTools.Common.DBUtility;
using Microsoft.VisualBasic;
using Renci.SshNet.Common;
using System;
using System.Collections;
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
        public struct DataBaseType
        {
            public string SqlClient;
            public string MySqlClient;
        };
        public void CreateProjectTable(string jcjgCode, string projectCode, bool addJcjt, bool addJcjg,
             out string msg)
        {
            DataBaseType baseType = new DataBaseType()
            { SqlClient = "System.Data.SqlClient", MySqlClient = "MySql.Data.MySqlClient" };
            msg = "";
            DataHelper TemplateService = new DataHelper($"ConnectionStringJCJT_WH");
            DataHelper jcjtService = new DataHelper($"ConnectionStringJCJT_{jcjgCode}");
            DataHelper jcjgService = new DataHelper($"ConnectionStringJCJG_{jcjgCode}");
            DataHelper debugToolsService = new DataHelper($"ConnectionStringDebugTool");

            ArrayList cmdListJcjt = new ArrayList();
            ArrayList cmdListJcjg = new ArrayList();

            string dbTypeJcjt = jcjtService.dbType;
            string dbTypeJcjg = jcjgService.dbType;

            ArrayList debugTool_zdzd = new ArrayList();
            try
            {

                //需要添加创建DWZD表 

                //添加检测集团项目
                if (addJcjt)
                {
                    //数据库类型不用，添加的语句不用
                    if (dbTypeJcjt == baseType.SqlClient)
                    {
                        //添加集团项目是，需要在caldebugtool数据库中创建一个zdzd表
                        var lists = CreateSqlServerZDZDCommond(projectCode);
                        cmdListJcjt.AddRange(lists);
                        debugTool_zdzd.AddRange(lists);
                        cmdListJcjt.AddRange(CreateSqlServer7BaseCommond(projectCode));

                        //zdzd表添加数据
                        lists = InsertSqlServcrZDZDDataCommond(projectCode, TemplateService);
                        cmdListJcjt.AddRange(lists);
                        debugTool_zdzd.AddRange(lists);
                    }
                    else
                    {
                        cmdListJcjt.AddRange(CreateMySqlZDZDCommond(projectCode));
                        cmdListJcjt.AddRange(CreateMySql7BaseCommond(projectCode));
                        cmdListJcjt.AddRange(InsertMySqlZDZDDataCommond(projectCode, TemplateService));

                        //zdzd数据暂时没有来源
                    }
                }
                //添加监管项目
                if (addJcjg)
                {
                    if (dbTypeJcjg == baseType.SqlClient)
                    {
                        cmdListJcjg.AddRange(CreateSqlServerZDZDCommond(projectCode));
                        cmdListJcjg.AddRange(CreateSqlServer7BaseCommond(projectCode));
                    }
                    else
                    {
                        cmdListJcjg.AddRange(CreateMySqlZDZDCommond(projectCode));
                        cmdListJcjg.AddRange(CreateMySql7BaseCommond(projectCode));
                    }
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                Log.Warn("新增项目", $"获取项目模板信息失败,ex:{msg}。");
                return;
            }

            #region 插入数据库
            int reflag = 0;
            //检测集团
            string sqlstr = "";
            if (addJcjt)
            {
                if (cmdListJcjt.Count == 0)
                {
                    msg = "添加检测集团项目失败，没有可插入的数据，请联系人员";
                    Log.Warn("新增项目", $"插入的数据列为空,{msg}。");
                    return;
                }
                sqlstr = string.Format($"select * from  PR_M_SYXM where syxmbh='{projectCode}';");
                reflag = CheckProjectIsExist(jcjtService, sqlstr);
                if (reflag == -2 || reflag > 0)
                {
                    Log.Warn("新增项目", $"{jcjgCode}_检测集团数据库:新增项目失败，PR_M_SYXM表已存在项目{projectCode}。");
                    msg = "warn";
                }
                else
                {
                    jcjtService.ExecuteSqlTran(cmdListJcjt, out msg);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        Log.Warn("新增项目", $"{jcjgCode}_检测集团数据库:添加字段失败，数据已回滚。msg:" + msg);
                    }
                }

                //CalDebugTools
                sqlstr = string.Format($"select * from information_schema.columns where table_name ='ZDZD_{projectCode}';");
                reflag = CheckProjectIsExist(debugToolsService, sqlstr);
                if (reflag == -2 || reflag > 0)
                {
                    Log.Warn("新增项目", $"CalDebugTools数据库:新增表失败，已存在表ZDZD_{projectCode}。");
                    //msg = "warn";
                }
                else
                {
                    //debugToolsService.ExecuteSqlTran(debugTool_zdzd, out msg);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        Log.Warn("新增项目", $"CalDebugTools数据库:新增表ZDZD_{projectCode}及添加表数据失败，数据已回滚。msg:" + msg);
                    }
                }
            }
            //检测监管
            if (addJcjg)
            {
                if (cmdListJcjg.Count == 0)
                {
                    msg = "添加监管项目失败，没有可插入的数据，请联系人员";
                    Log.Warn("新增项目", $"插入的数据列为空,{msg}。");
                    return;
                }
                sqlstr = string.Format($"select * from  PR_M_SYXM where syxmbh='{projectCode}';");
                reflag = CheckProjectIsExist(jcjgService, sqlstr);
                if (reflag == -2 || reflag > 0)
                {
                    //msg = "warn";
                    Log.Warn("新增项目", $"{jcjgCode}_监管数据库:新增项目失败，已存在项目{projectCode}。");
                }
                else
                {
                    jcjgService.ExecuteSqlTran(cmdListJcjg, out msg);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        Log.Warn("新增项目", $"{jcjgCode}_监管数据库:添加字段失败，数据已回滚。msg:{msg}");
                    }
                }
            }

            #endregion
        }


        /// <summary>
        /// sql server 添加基本表(7张)
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public List<string> CreateSqlServer7BaseCommond(string projectCode)
        {
            List<string> cmdList = new List<string>();
            string sqlstr = "";
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
                $"[YPSL][nvarchar](50) NULL,[BWSX][nvarchar](120) NULL,[JCJG][nvarchar](10) NULL,[JCXM][varchar](max) NULL,[JCXMDH][varchar](max) NULL," +
                $"[SJ_ZS][int] NULL,[SBMCBH][nvarchar](200) NULL) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY];");
            cmdList.Add(sqlstr);


            //创建DWZD表
            return cmdList;

        }

        /// <summary>
        /// MY sql  添加基本表(7张)
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public List<string> CreateMySql7BaseCommond(string projectCode)
        {
            List<string> cmdList = new List<string>();
            string sqlstr = "";
            //创建M_DJ表
            sqlstr = string.Format($"CREATE TABLE `M_DJ_{projectCode}`  (" +
                $" `RECID` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL," +
                $" `SJDJ` varchar(60) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL" +
                $") ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;" +
                $"SET FOREIGN_KEY_CHECKS = 1; ");
            cmdList.Add(sqlstr);

            //创建S_DJ表
            sqlstr = string.Format($"CREATE TABLE `S_DJ_{projectCode}`  (" +
                 $" `RECID` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL," +
                 $" `BYZBRECID` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL," +
                 $" `CPMC` varchar(40) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL," +
                 $" `JCXM` longtext CHARACTER SET utf8 COLLATE utf8_general_ci," +
                 $" `CPLX` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL," +
                 $" `ZF` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL," +
                 $" `YPSL` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL," +
                 $" `DBSL` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL," +
                 $" `CCPH` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL," +
                 $" `CCCJ` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL," +
                 $" `PBQK` text CHARACTER SET utf8 COLLATE utf8_general_ci)" +
                 $" ENGINE = MyISAM CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;" +
                 $" SET FOREIGN_KEY_CHECKS = 1; ");
            cmdList.Add(sqlstr);

            //创建M_D表
            sqlstr = string.Format($"CREATE TABLE `M_D_{projectCode}`  (" +
                $"`RECID` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL" +
                $") ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;");
            cmdList.Add(sqlstr);
            //创建S_D表
            sqlstr = string.Format($"CREATE TABLE `S_D_{projectCode}`  (" +
                $"`RECID` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL," +
                $"`BYZBRECID` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL" +
                $") ENGINE = MyISAM CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;");
            cmdList.Add(sqlstr);

            //创建帮助表 BZ_{项目代号}_DJ
            sqlstr = string.Format($"CREATE TABLE `BZ_{projectCode}_DJ`  (" +
                 $" `RECID` int(11) NOT NULL AUTO_INCREMENT," +
                 $" `CXGX` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL," +
                 $" `BH` varchar(5) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL," +
                 $" `JSFF` varchar(3) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL," +
                 $" `MC` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL," +
                 $" `BGNAME` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL," +
                 $" `ZJM` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL," +
                 $" `GGXH` varchar(60) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL," +
                 $" `CJXS` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL," +
                 $" INDEX `RECID`(`RECID`) USING BTREE" +
                 $") ENGINE = MyISAM AUTO_INCREMENT = 2 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;" +
                 $"SET FOREIGN_KEY_CHECKS = 1; ");
            cmdList.Add(sqlstr);

            //创建主表
            sqlstr = string.Format($"CREATE TABLE `M_{projectCode}`(" +
                 $"`RECID` nvarchar(50) NOT NULL," +
                 $"`JCJGMS` longtext NULL," +
                 $"`JCJG` nvarchar(10) NULL," +
                 $"`SYWD` nvarchar(30) NULL," +
                 $"`YCQK` nvarchar(200) NULL," +
                 $"`SBMCBH` nvarchar(200) NULL); ");
            cmdList.Add(sqlstr);

            //创建从表
            sqlstr = string.Format($"CREATE TABLE `S_{projectCode}`(" +
                $"`RECID` nvarchar(50) NOT NULL," +
                $"`BYZBRECID` nvarchar(50) NULL," +
                $"`YPSL` nvarchar(50) NULL," +
                $"`BWSX` nvarchar(120) NULL," +
                $"`JCJG` nvarchar(10) NULL," +
                $"`JCXM` longtext NULL," +
                $"`JCXMDH` longtext NULL," +
                $"`SJ_ZS` int NULL," +
                $"`SBMCBH` nvarchar(200) NULL); ");
            cmdList.Add(sqlstr);

            return cmdList;

        }

        /// <summary>
        /// 添加zdzd表字段（Sql Server）
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public List<string> CreateSqlServerZDZDCommond(string projectCode)
        {
            List<string> cmdList = new List<string>();
            string sqlstr_zdzd = string.Format($"CREATE TABLE [dbo].[ZDZD_{projectCode}]([SJGJ_ID][int] IDENTITY(1, 1) NOT NULL,[SJBMC][nvarchar](20) NOT NULL," +
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

            sqlstr_zdzd = string.Format($"CREATE TABLE[dbo].[DWZD_{ projectCode}](" +
                $" [SJGJ_ID][int] IDENTITY(1, 1) NOT NULL," +
                $" [SJBMC][nvarchar](20) NOT NULL," +
                $" [ZDMC][nvarchar](20) NOT NULL," +
                $" [SY][nvarchar](50) NULL," +
                $" [ZDLX][nvarchar](20) NULL," +
                $" [ZDCD1][int] NULL," +
                $" [ZDCD2][int] NULL," +
                $" [INPUTZDLX][nvarchar](20) NULL," +
                $" [KJLX][nvarchar](20) NULL," +
                $" [SFBHZD][bit] NULL," +
                $" [BHMS][nvarchar](200) NULL," +
                $" [ZDSX][bit] NULL," +
                $" [SFXS][bit] NULL," +
                $" [XSCD][int] NULL," +
                $" [XSSX][numeric](12,4) NULL," +
                $" [SFGD][bit] NULL," +
                $" [MUSTIN][bit] NULL," +
                $" [DEFAVAL][nvarchar](100) NULL," +
                $" [HELPLNK][text] NULL," +
                $" [CTRLSTRING][text] NULL," +
                $" [ZDXZ][varchar](20) NULL," +
                $" [WXSSX][numeric](12, 4) NULL," +
                $" [WSFXS][bit] NULL," +
                $" [MSGINFO][varchar](80) NULL," +
                $" [EQLFUNC][varchar](240) NULL," +
                $" [HELPWHERE][varchar](100) NULL," +
                $" [GETBYBH][bit] NULL," +
                $" [SSJCX][text] NULL," +
                $" [SFBGZD][bit] NULL," +
                $" [VALIDPROC][text] NULL," +
                $" [LX][nvarchar](100) NULL," +
                $" [ZDSXSQL][nvarchar](max) NULL," +
                $" [ENCRYPT][bit] NULL," +
                $" [FZYC][bit] NULL," +
                $" [FZCS][text] NULL," +
                $" [NOSAVE][bit] NULL," +
                $" [location][nvarchar](200) NULL," +
                $" [bjzd][bit] NULL," +
                $" [Onchange_Fun][nvarchar](2000) NULL," +
                $" [tabIndex][nvarchar](100) NULL," +
                $" [BJLX][nvarchar](50) NULL," +
                $" [location1][nvarchar](200) NULL," +
                $" [Onchange_Fun1][nvarchar](2000) NULL," +
                $" [bjzd1][bit] NULL," +
                $" [tabIndex1][nvarchar](50) NULL," +
                $"PRIMARY KEY CLUSTERED( [SJGJ_ID] ASC" +
                $")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                $") ON[PRIMARY] TEXTIMAGE_ON[PRIMARY];");
            cmdList.Add(sqlstr_zdzd);
            return cmdList;
        }

        /// <summary>
        /// 添加zdzd表字段（MySql）
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public List<string> CreateMySqlZDZDCommond(string projectCode)
        {
            List<string> cmdList = new List<string>();
            string sqlstr_zdzd = string.Format($"CREATE TABLE `ZDZD_{projectCode}`  ( `SJGJ_ID` int(11) NOT NULL AUTO_INCREMENT," +
                $"`SJBMC` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL," +
                $"`ZDMC` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL," +
                $"`SY` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $"`ZDLX` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $"`ZDCD1` int(11) DEFAULT NULL," +
                $"`ZDCD2` int(11) DEFAULT NULL," +
               $"`INPUTZDLX` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`KJLX` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`SFBHZD` tinyint(4) DEFAULT NULL," +
               $"`BHMS` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`ZDSX` tinyint(4) DEFAULT NULL," +
               $"`SFXS` tinyint(4) DEFAULT NULL," +
               $"`XSCD` int(11) DEFAULT NULL," +
               $"`XSSX` decimal(12, 4) DEFAULT NULL," +
               $"`SFGD` tinyint(4) DEFAULT NULL," +
               $"`MUSTIN` tinyint(4) DEFAULT NULL," +
               $"`DEFAVAL` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`HELPLNK` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
               $"`CTRLSTRING` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
               $"`ZDXZ` varchar(1) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`WXSSX` decimal(12, 4) DEFAULT NULL," +
               $"`WSFXS` tinyint(4) DEFAULT NULL," +
               $"`MSGINFO` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`EQLFUNC` varchar(240) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`HELPWHERE` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`GETBYBH` tinyint(4) DEFAULT NULL," +
               $"`SSJCX` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
               $"`SFBGZD` tinyint(4) DEFAULT NULL," +
               $"`VALIDPROC` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
               $"`LX` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`ZDSXSQL` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
               $"`ENCRYPT` tinyint(4) DEFAULT NULL," +
               $"`FZYC` tinyint(4) DEFAULT NULL," +
               $"`FZCS` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
               $"`NOSAVE` tinyint(4) DEFAULT NULL," +
               $"`location` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`bjzd` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"`SFZZZD` tinyint(4) DEFAULT NULL," +
               $"`SYBM` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
               $"PRIMARY KEY(`SJGJ_ID`) USING BTREE" +
               $") ENGINE = InnoDB AUTO_INCREMENT = 33 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;" +
               $"SET FOREIGN_KEY_CHECKS = 1; ");

            cmdList.Add(sqlstr_zdzd);

            //单位字段
            sqlstr_zdzd = string.Format($"CREATE TABLE `DWZD_{projectCode}`  (" +
                $" `SJGJ_ID` int(11) NOT NULL," +
                $" `SJBMC` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL," +
                $" `ZDMC` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL," +
                $" `SY` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `ZDLX` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `ZDCD1` int(11) DEFAULT NULL," +
                $" `ZDCD2` int(11) DEFAULT NULL," +
                $" `INPUTZDLX` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `KJLX` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `SFBHZD` tinyint(4) DEFAULT NULL," +
                $" `BHMS` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `ZDSX` tinyint(4) DEFAULT NULL," +
                $" `SFXS` tinyint(4) DEFAULT NULL," +
                $" `XSCD` int(11) DEFAULT NULL," +
                $" `XSSX` decimal(12, 4) DEFAULT NULL," +
                $" `SFGD` tinyint(4) DEFAULT NULL," +
                $" `MUSTIN` tinyint(4) DEFAULT NULL," +
                $" `DEFAVAL` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `HELPLNK` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
                $" `CTRLSTRING` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
                $" `ZDXZ` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `WXSSX` decimal(12, 4) DEFAULT NULL," +
                $" `WSFXS` tinyint(4) DEFAULT NULL," +
                $" `MSGINFO` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `EQLFUNC` varchar(240) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `HELPWHERE` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `GETBYBH` tinyint(4) DEFAULT NULL," +
                $" `SSJCX` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
                $" `SFBGZD` tinyint(4) DEFAULT NULL," +
                $" `VALIDPROC` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
                $" `LX` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `ZDSXSQL` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
                $" `ENCRYPT` tinyint(4) DEFAULT NULL," +
                $" `FZYC` tinyint(4) DEFAULT NULL," +
                $" `FZCS` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci," +
                $" `NOSAVE` tinyint(4) DEFAULT NULL," +
                $" `location` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `bjzd` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL," +
                $" `SFZZZD` tinyint(4) DEFAULT NULL," +
                $" `SYBM` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT '释义别名'," +
                $" PRIMARY KEY(`SJGJ_ID`) USING BTREE" +
                $") ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic; ");
            cmdList.Add(sqlstr_zdzd);

            return cmdList;
        }

        public List<string> InsertSqlServcrZDZDDataCommond(string projectCode, DataHelper TemplateService)
        {
            string sqlstr_zdzd = "";
            List<string> cmdList = new List<string>();
            DataSet ds_zdzd = TemplateService.GetDataSet("execute pro_GetTableInfoToInsert 'ZDZD_TTT1', 'SJBMC  <> ''''' ");
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
            }
            return cmdList;
        }


        public List<string> InsertMySqlZDZDDataCommond(string projectCode, DataHelper TemplateService)
        {
            string sqlstr_zdzd = "";
            List<string> cmdList = new List<string>();
            DataSet ds_zdzd = TemplateService.GetDataSet("execute pro_GetTableInfoToInsert 'ZDZD_TTT1', 'SJBMC  <> ''''' ");
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
            }
            return cmdList;
        }

        /// <summary>
        /// 检查项目是否已经存在
        /// </summary>
        /// <param name="dbService"></param>
        /// <param name="projectCode">项目代号</param>
        /// <returns></returns>
        public int CheckProjectIsExist(DataHelper dbService, string sql)
        {
            try
            {
                return dbService.GetDataSet(sql).Tables[0].Rows.Count == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                Log.Error("新增项目", $" 检查项目是否存在是异常，msg:" + ex.Message);
                return -2;

            }
        }
    }
}
