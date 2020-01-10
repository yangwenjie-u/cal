using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// CreateTable.xaml 的交互逻辑
    /// </summary>
    public partial class CreateTable : Window
    {
        public string XMBH = "";

        Base.SqlBase sqlbase = new Base.SqlBase("jcjt");
        public CreateTable()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string type = txt_type.Text.Trim();
                if (string.IsNullOrEmpty(type))
                {
                    MessageBox.Show("试验编号不可以为空");
                    return;
                }
                XMBH = type;
                string sqlstr = "";
                List<string> sqlStrs = new List<string>();
                int retCount = 0;//返回受影响的行数

                string table_DWZD = "DWZD_" + type.ToUpper();
                #region 创建 DWZD表
                sqlstr = "select Top 1 * from " + table_DWZD;

                retCount = CommondSQL(sqlstr);

                if (retCount == 0)//表不存在，新增表。否则不处理
                {

                    sqlStrs.Add(string.Format("select * into DWZD_{0} from DWZD_PZJ where 1=1",type));

                    sqlStrs.Add(string.Format("insert into DWZD_{0}(SJBMC,ZDMC,SY,ZDLX,ZDCD1,ZDCD2,INPUTZDLX,KJLX,SFBHZD,BHMS,ZDSX,SFXS,XSCD,XSSX,SFGD,MUSTIN,DEFAVAL,HELPLNK,CTRLSTRING,ZDXZ,WXSSX,WSFXS,MSGINFO,EQLFUNC,HELPWHERE,GETBYBH,SSJCX,SFBGZD,VALIDPROC,LX,ZDSXSQL,ENCRYPT,FZYC,FZCS,NOSAVE)   values(	N'M_D_{0}'	,	N'RECID'	,	N'主表RECID'	,	N'nvarchar'	,	50	,	0	,	N''	,	N''	,	NULL	,	NULL	,	1	,	0	,	5	,	0.0000	,	0	,	0	,	N'                                                                                '	,	''	,	''	,	'W'	,	361.0000	,	0	,	''	,	''	,	''	,	1	,	NULL	,	1	,	''	,	N'W'	,	NULL	,	NULL	,	NULL	,	NULL	,	NULL	);",type));
                    sqlStrs.Add(string.Format("insert into DWZD_{0}(SJBMC,ZDMC,SY,ZDLX,ZDCD1,ZDCD2,INPUTZDLX,KJLX,SFBHZD,BHMS,ZDSX,SFXS,XSCD,XSSX,SFGD,MUSTIN,DEFAVAL,HELPLNK,CTRLSTRING,ZDXZ,WXSSX,WSFXS,MSGINFO,EQLFUNC,HELPWHERE,GETBYBH,SSJCX,SFBGZD,VALIDPROC,LX,ZDSXSQL,ENCRYPT,FZYC,FZCS,NOSAVE)   values(N'S_D_{0}'	,	N'RECID'	,	N'从表RECID'	,	N'nvarchar'	,	50	,	0	,	N''	,	N''	,	NULL	,	NULL	,	1	,	0	,	5	,	0.0000	,	0	,	0	,	N'                                                                                '	,	''	,	''	,	'W'	,	361.0000	,	0	,	''	,	''	,	''	,	1	,	NULL	,	1	,	''	,	N'W'	,	NULL	,	NULL	,	NULL	,	NULL	,	NULL	);", type));
                    sqlStrs.Add(string.Format("insert into DWZD_{0}(SJBMC,ZDMC,SY,ZDLX,ZDCD1,ZDCD2,INPUTZDLX,KJLX,SFBHZD,BHMS,ZDSX,SFXS,XSCD,XSSX,SFGD,MUSTIN,DEFAVAL,HELPLNK,CTRLSTRING,ZDXZ,WXSSX,WSFXS,MSGINFO,EQLFUNC,HELPWHERE,GETBYBH,SSJCX,SFBGZD,VALIDPROC,LX,ZDSXSQL,ENCRYPT,FZYC,FZCS,NOSAVE)   values(N'S_D_{0}'	,	N'BYZBRECID'	,	N'主表RECID'	,	N'nvarchar'	,	50	,	0	,	N''	,	N''	,	NULL	,	NULL	,	1	,	0	,	5	,	0.0000	,	0	,	0	,	N'                                                                                '	,	''	,	''	,	'W'	,	361.0000	,	0	,	''	,	''	,	''	,	1	,	NULL	,	1	,	''	,	N'W'	,	NULL	,	NULL	,	NULL	,	NULL	,	NULL	);", type));
                }

                foreach (var item in sqlStrs)
                {
                    retCount = CommondSQL(sqlstr);
                }
                #endregion

                #region   委托单表（M_，S_）
                sqlStrs.Clear();
                string m = "M_" + type;
                string s = "S_" + type;
                sqlstr = "select Top 1 * from " + m;
                retCount = CommondSQL(sqlstr);

                if (retCount == 0)//表不存在，新增表。否则不处理
                {
                    sqlstr = string.Format("select * into M_{0} from M_HNT  where 1=2 ", type);
                }
                sqlstr = "select Top 1 * from " + s;

                retCount = CommondSQL(sqlstr);

                if (retCount == 0)//表不存在，新增表。否则不处理
                {
                    sqlStrs.Add(string.Format("select * into S_{0} from weblab_chifeng.dbo." + s + "  where 1=2", type));
                    sqlStrs.Add(string.Format("alter table S_{0} alter column recid nvarchar(100) not null", type));
                    sqlStrs.Add(string.Format("alter table S_{0} add BYZBRECID nvarchar(100) not null default '0'", type));

                }

                foreach (var item in sqlStrs)
                {
                    retCount = CommondSQL(item);
                }

                #endregion

                #region 创建M_DJ_HNT,S_DJ_HNT
                sqlStrs.Clear();

                sqlStrs.Add(string.Format("select * into  M_DJ_{0}  from M_DJ_HNT  where 1=2", type));
                //S_DJ_HNT通过zdzd添加
                //sqlStrs.Add(string.Format("select * into  S_DJ_{0}  from S_{0}  where 1=2", type));

                foreach (var item in sqlStrs)
                {
                    retCount = CommondSQL(item);
                }
                #endregion

                #region  创建 ZDZD
                sqlStrs.Clear();
                sqlstr = string.Format("select Top 1 * from ZDZD_{0}", type);
                retCount = CommondSQL(sqlstr);

                if (retCount == 0)//表不存在，新增表。否则不处理
                {
                    sqlStrs.Add(string.Format("select * into ZDZD_{0} from weblab_chifeng.dbo.ZDZD_{0} where 1 = 1", type));
                    sqlStrs.Add(string.Format("UPDATE ZDZD_{0} set SJBMC='S_{0}' where sjbmc='S{0}'", type));
                    sqlStrs.Add(string.Format("UPDATE ZDZD_{0} set SJBMC='M_{0}' where sjbmc='M{0}'", type));

                }

                foreach (var item in sqlStrs)
                {
                    retCount = CommondSQL(item);
                }
                #endregion

                #region 创建编号表

                sqlStrs.Clear();
                sqlstr = string.Format(" select * FROM [PR_S_BHMS_JS] where ZD1='{0}' and SSDWBH='{1}'", type, "407a4ecd6e43421babd6d0de0235fc62");
                retCount = CommondSQL(sqlstr);
                if (retCount == 0)//表不存在，新增表。否则不处理
                {

                    sqlStrs.Add(string.Format("insert into[PR_S_BHMS_JS] (SSDWBH,[BHMSJZ],[ZD1],[BHMS],[LX],[XSSX]) values('407a4ecd6e43421babd6d0de0235fc62', 'S_DJ_BY_DJDBH', '{0}', '''DJ{0}''+YYYY+MM+6', 'DJ', 1)", type));
                    sqlStrs.Add(string.Format("insert into[PR_S_BHMS_JS] (SSDWBH,[BHMSJZ],[ZD1],[BHMS],[LX],[XSSX]) values('407a4ecd6e43421babd6d0de0235fc62', 'M_BY_WTDBH', '{0}', '''WT{0}''+YYYY+6', 'WT', 2)", type));
                    sqlStrs.Add(string.Format("insert into[PR_S_BHMS_JS] (SSDWBH,[BHMSJZ],[ZD1],[BHMS],[LX],[XSSX]) values('407a4ecd6e43421babd6d0de0235fc62', 'M_BY_SYBH', '{0}', '''SY{0}''+YYYY+MM+6', 'SY', 3)", type));
                    sqlStrs.Add(string.Format("insert into[PR_S_BHMS_JS] (SSDWBH,[BHMSJZ],[ZD1],[BHMS],[LX],[XSSX]) values('407a4ecd6e43421babd6d0de0235fc62', 'M_BY_BGBH', '{0}', '''BG{0}''+YYYY+MM+6', 'BG', 4)", type));
                    sqlStrs.Add(string.Format("insert into[PR_S_BHMS_JS] (SSDWBH,[BHMSJZ],[ZD1],[BHMS],[LX],[XSSX]) values('407a4ecd6e43421babd6d0de0235fc62', 'S_BY_YPBH', '{0}', '''YP{0}''+YYYY+6', 'YP', 5)", type));
                }
                foreach (var item in sqlStrs)
                {
                    retCount = CommondSQL(item);
                }
                // 登记单列表配置
                //FORMZDZD_DJ：统一通用字段
                //FORMZDZD_S_DJ ：各项目的字段
                //编号模式表： pr_s_bhms_js
                //insert into[PR_S_BHMS_JS] (SSDWBH,[BHMSJZ],[ZD1],[BHMS],[LX],[XSSX]) values('407a4ecd6e43421babd6d0de0235fc62', 'S_DJ_BY__DJDBH', 'HNT', '''DJHNT''+YYYY+MM+6', 'DJ', 1)


                #endregion
            }
            catch (Exception ex)
            {

            }


        }

        private void btn_SDJ_Click(object sender, RoutedEventArgs e)
        {
            string txt_Val = txtare_s_dj.Text.Trim();
            bool fromChifeng = false;
            txt_Val = Regex.Replace(txt_Val, @"[\r\n]", "");
            List<string> lstJcxm = txt_Val.Replace("\r\n", "").Split(',').ToList();
            string type = txt_type.Text.Trim();
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("试验编号不可以为空");
                return;
            }
            List<string> lstInsertSqls = new List<string>();
            Base.SqlBase sqlbaseChiFeng = new Base.SqlBase();
            DataSet ds = new DataSet();

            string sqlstr = "";
            string insertSql = "";
            int retCount = 0;
            int valuesIndex = 0;
            int SJBMCIndex = 0;
            foreach (var zdmc in lstJcxm)
            {
                //判断字段是否已经添加
                sqlstr = string.Format(" select * FROM ZDZD_{0} where SJBMC='S_DJ_{0}' and sy ='{1}'", type, zdmc.Trim());
                retCount = CommondSQL(sqlstr);

                if (retCount > 0)
                {
                    continue;
                }
                fromChifeng = false;
                sqlstr = string.Format(" exec GetZDZDData 'ZDZD_{0}', ' 1=1  and sy = ''{1}'' and  SJBMC=''S_{0}'' ' ", type, zdmc.Trim());
                //sqlstr = string.Format(" exec GetZDZDData 'ZDZD_{0}', ' 1=1  and sy = ''{1}'' and  SJBMC=''S_{0}'' ' ", "HD", "检测项目");
                ds = sqlbase.ExecuteDataset(sqlstr);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count == 0)
                {
                    fromChifeng = true;
                    sqlstr = string.Format(" exec GetZDZDData 'ZDZD_{0}', ' 1=1  and sy = ''{1}'' and  SJBMC=''S{0}'' ' ", type, zdmc.Trim());

                    ds = sqlbaseChiFeng.ExecuteDataset(sqlstr);
                    dt = ds.Tables[0];
                }

                foreach (var item in dt.Rows)
                {
                    var itemArray = ((System.Data.DataRow)item).ItemArray;
                    insertSql = "";
                    for (int i = 0; i < itemArray.Count(); i++)
                    {
                        if (i == 1 || i == 2)
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(itemArray[i].ToString()))
                        {
                            insertSql += "null";
                        }

                        insertSql += itemArray[i];
                    }

                    insertSql = insertSql.ToUpper();
                    if (fromChifeng)
                    {
                        valuesIndex = insertSql.IndexOf("VALUES(");
                        SJBMCIndex = insertSql.IndexOf("'S", valuesIndex);
                    }
                    else
                    {
                        valuesIndex = insertSql.IndexOf("VALUES(");
                        SJBMCIndex = insertSql.IndexOf("N'S_", valuesIndex);
                    }

                    insertSql = insertSql.Remove(valuesIndex + 7, SJBMCIndex - valuesIndex - 7);
                    insertSql = insertSql.Replace("RECID,", "").Replace("SJGJ_ID,", "").Replace("FALSE", "0").Replace("TRUE", "1").Replace(string.Format("N'S_{0}'", type), string.Format("N'S_DJ_{0}'", type)).Replace(string.Format("('S{0}',", type), string.Format("('S_DJ_{0}',", type));

                    lstInsertSqls.Add(insertSql);
                }
            }


            if (lstInsertSqls.Count > 0)
            {
                lstInsertSqls.Add(string.Format("update ZDZD_{0} set LX = '{1}' where SJBMC = 'S_DJ_{0}' and LX is null", type, ((System.Windows.Controls.ContentControl)lisBoxLX.SelectedItem).Content.ToString()));
                lstInsertSqls.Add(string.Format("update ZDZD_{0} set ZDLX = 'NVARCHAR' where ZDLX='C'", type));
                lstInsertSqls.Add(string.Format("update ZDZD_{0} set ZDLX = 'int' where ZDLX='I'", type));
                lstInsertSqls.Add(string.Format("update ZDZD_{0} set ZDLX = 'NUMERIC' where ZDLX='N'", type));
                lstInsertSqls.Add(string.Format("update ZDZD_{0} set ZDLX = 'text' where ZDLX='M'", type));
                lstInsertSqls.Add(string.Format("update ZDZD_{0} set datetime = 'text' where ZDLX='D'", type));
                lstInsertSqls.Add(string.Format("update ZDZD_{0} set  XSCD = 0 where SJBMC='S_DJ_{0}'", type));
                //update ZDZD_WJJ set XSCD = 0 where SJBMC = 's_DJ_WJJ'

                //--update  ZDZD_HD set  ZDLX = 'nvarchar'  where ZDLX = 'C'
                //--update ZDZD_HD set ZDLX = 'int'  where ZDLX = 'I'
                //--update ZDZD_HD set ZDLX = 'bit'  where ZDLX = 'L'
                //--update ZDZD_HD set ZDLX = 'numeric'  where ZDLX = 'N'
                //--update ZDZD_HD set ZDLX = 'text'  where ZDLX = 'M'
                //--update ZDZD_HD set ZDLX = 'datetime'  where ZDLX = 'D'

                //--update ZDZD_HD set KJLX = 'CHECKBOX'  where KJLX = 'C'
                //--  不定类型 update  ZDZD_HD set  KJLX = 'COMBOBOX'  where KJLX = 'B'
                //--update ZDZD_HD set KJLX = 'RADIO'  where KJLX = 'R'
                //--update ZDZD_HD set KJLX = 'SELECT'  where KJLX = 'S'
            }
            foreach (var item in lstInsertSqls)
            {
                int we = CommondSQL(item);
            }

        }

        private int CommondSQL(string strSql)
        {
            try
            {
                var rData = sqlbase.ExecuteDataset(strSql);
                return rData.Tables[0].Rows.Count;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            string type = txt_type.Text.Trim();
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("试验编号不可以为空");
                return;
            }
            string  sqlstr = string.Format(" UPDATE ZDZD_{0} set CTRLSTRING = 'valueTable--table-View_PR_JCXM|fieldname-BGXSMC|distinct-1|customwhere-SYXMBH=''##syxmbh##'' and qybh=''##YTDWBH##''' where SJBMC = 'S_DJ_{0}'  and sy = '{1}'", type, "检测项目");

            List<string> listStr = new List<string>();

         int   retCount = CommondSQL(sqlstr);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string txt_Val = txtare_s_dj.Text.Trim();
            bool fromChifeng = false;
            txt_Val = Regex.Replace(txt_Val, @"[\r\n]", "");
            List<string> lstJcxm = txt_Val.Replace("\r\n", "").Split(',').ToList();
            string type = txt_type.Text.Trim();
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("试验编号不可以为空");
                return;
            }
            List<string> lstInsertSqls = new List<string>();
            Base.SqlBase sqlbaseChiFeng = new Base.SqlBase();
            DataSet ds = new DataSet();

            string sqlstr = "";
            string insertSql = "";
            int retCount = 0;
            int valuesIndex = 0;
            int SJBMCIndex = 0;

            //select* from weblab_chifeng.dbo.ZDZD_WJJ where SJBMC = '' ZDMC like '%JPHWZL_%'
        }
    }
}