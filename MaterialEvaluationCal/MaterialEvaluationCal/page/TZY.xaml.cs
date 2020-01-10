using Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows;


namespace MaterialEvaluationCal.page
{
    /// <summary>
    /// TZY.xaml 的交互逻辑
    /// </summary>
    public partial class TZY : Window
    {
        public TZY()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string type = "混泥土轴心抗压";
            string tablename = "TZY";
            string err = "";

            Dictionary<string, string> testDatas = new Dictionary<string, string>();

            #region 前段字段赋值
            testDatas.Add("CD1", this.cd1.Text);
            testDatas.Add("KD1", this.kd1.Text);
            testDatas.Add("KYHZ1", this.klhz1.Text);
            testDatas.Add("CD2", this.cd2.Text);
            testDatas.Add("KD2", this.kd2.Text);
            testDatas.Add("KYHZ2", this.klhz2.Text);
            testDatas.Add("CD3", this.cd3.Text);
            testDatas.Add("KD3", this.kd3.Text);
            testDatas.Add("KYHZ3", this.klhz3.Text);

            #endregion

            cal(type,tablename,ref err);
        }


        public void cal(string type,string tablename,ref string err)
        {
            //获取测试数据
            string JsonhelperData = GetAfferentDataJson(tablename, tablename, true);
            //获取帮助表数据
            string extraDatajson = GetDataJson($"BZ_{tablename}_DJ", $"M_{tablename}");

            var listExtraData = GetDictionary(extraDatajson, type);
            var retSData = GetAfferentDictionary(JsonhelperData);
            try
            {
                //Assembly ass = null;
                //Type tp;
                //Object obj;
                //ass = Assembly.Load("xxx");//dll名字
                //tp = ass.GetType("MaterialEvaluationCal.Calculates." + tablename);//类名
                //obj = Activator.CreateInstance(tp);//实例化
                //MethodInfo meth = tp.GetMethod("Calc");//加载方法
                //meth.Invoke(obj, new Object[] { listExtraData, retSData, err });//执行


                //Calculates.TZY.Calc(listExtraData, ref retSData, ref err);
                string strClass = "MaterialEvaluationCal.Calculates." + tablename;//命名空间+类名
                var mtype = Type.GetType("MaterialEvaluationCal.Calculates." + tablename);//通过类名获取同名类
                var obj = System.Activator.CreateInstance(mtype);//创建实例
                MethodInfo method = mtype.GetMethod("Calc");//获取方法信息
                object[] parameters = { listExtraData, retSData, err };

                method.Invoke(obj, parameters);//调用方法
            }
            catch (Exception ex)
            {
                this.jcjg.Text = $"操作失败{ex.Message}";
            }
            return;
        }

        /// <summary>
        /// 获取指定的SQL数据并转为对应的json字符串
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="sql">获取数据的SQL语句</param>
        /// <returns></returns>
        public static string GetAfferentDataJson(string type,string code,bool hasMintable = false )
        {
            string sql = $" select top 5 * from S_{code} ;";
            if (hasMintable)
                sql += $" select top 5 * from M_{code} ; ";
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb.Append("{\"calcData\":[{");
            Base.SqlBase sqlbase = new Base.SqlBase("local");
            DataSet ds = sqlbase.ExecuteDataset(sql);
            DataTable subTable = ds.Tables[0];
            DataTable mainTable = ds.Tables.Count > 1 ? ds.Tables[1] : null;
            string[] jcxm_list = subTable.Rows[0]["jcxm"].ToString().Split('、', ',');
            foreach (string item in jcxm_list)
            {
                StringBuilder sminstr = new StringBuilder();
                sb2.Append("\"" + item + "\":{");
                DataTable dt_json = ToDataTable(subTable.Select(" jcxm like '%" + item + "%'"));
                sb2.Append($"\"S_{code}\":" + JsonHelper.SerializeObject(dt_json) + "");
                string dtminstr = "";
                if (mainTable != null)
                {
                    dtminstr = JsonHelper.SerializeObject(ToDataTable(mainTable.Select($" RECID ={dt_json.Rows[0]["BYZBRECID"]} ")));
                    dtminstr = dtminstr.Substring(1, dtminstr.Length - 2);
                }

                for (int i = 0; i < subTable.Select(" jcxm like '%" + item + "%'").Length; i++)
                {
                    if (dtminstr != "")
                    {
                        if (i == 0)
                            sminstr.Append("\"M_" + code + "\":[");
                        sminstr.Append(dtminstr + ",");
                    }
                    if (i == 0)
                    {
                        sb2.Append(",\"S_BY_RW_XQ\":[");
                    }
                    sb2.Append("{\"recid\":\"19085206791636631332933\",");
                    sb2.Append("\"sjwcjssj\":\"" + DateTime.Now.ToString() + "\",");
                    sb2.Append("\"JCJG\":\"\",");
                    sb2.Append("\"JCJGMS\":\"\"");
                    sb2.Append("},");
                }
                sb2.Remove(sb2.Length - 1, 1).Append("]},");
                if (dtminstr != "")
                    sb2.Remove(sb2.Length - 2, 1).Append(sminstr.Remove(sminstr.Length - 1, 1).Append("]},"));

            }
            sb.Append(sb2.ToString().TrimEnd(','));
            sb.Append("}],\"code\":1, \"message\":\"成功\"}");
            return sb.ToString();
        }

        public static DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0)
                return null;
            DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构  
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中  
            return tmp;
        }

        public static string GetDataJson(params string[] type)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sqlsr = new StringBuilder();
            foreach (var tabName in type)
            {
                sqlsr.Append($" select * from {tabName} ; ");
            }
            Base.SqlBase sqlbase = new Base.SqlBase("local");
            DataSet ds = sqlbase.ExecuteDataset(sqlsr.ToString());

            sb.Append("{\"calcData\":{");
            for (int i = 0; i < type.Length; i++)
            {
                sb.Append("\"" + type[i] + "\":");
                ds.Tables[i].TableName = type[i];
                sb.Append(JsonHelper.SerializeObject(ds.Tables[i]));
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1).Append("},\"code\":1,\"message\":\"成功\"}");
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static IDictionary<string, IList<IDictionary<string, string>>> GetDictionary(string json, string tableName)
        {
            IDictionary<string, IList<IDictionary<string, string>>> dataExtra = new Dictionary<string, IList<IDictionary<string, string>>>();
            var json_main = new { calcData = new object(), message = string.Empty };
            var stract_class = JsonHelper.DeserializeAnonymousType(json, json_main);
            string json_str = stract_class.calcData.ToString().Replace("\"" + tableName + "\":", "");//.TrimStart('{').TrimEnd('}')
            DataSet ds = JsonHelper.DeserializeJsonToObject<DataSet>(json_str);
            string name = ds.Tables[0].TableName;
            for (int d = 0; d < ds.Tables.Count; d++)
            {
                IList<IDictionary<string, string>> list = new List<IDictionary<string, string>>();
                DataTable dt = ds.Tables[d];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IDictionary<string, string> openWith = new Dictionary<string, string>();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        openWith.Add(dt.Columns[j].ColumnName, dt.Rows[i][dt.Columns[j].ColumnName].ToString());
                    }
                    list.Add(openWith);
                }
                dataExtra.Add(ds.Tables[d].TableName, list);
            }
            return dataExtra;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> GetAfferentDictionary(string json, string projectName = null)
        {
            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> dataExtra = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();
            var json_main = new { calcData = new object(), message = string.Empty };
            var stract_class = JsonHelper.DeserializeAnonymousType(json, json_main);
            string json_str = stract_class.calcData.ToString().TrimStart('[').TrimEnd(']').TrimStart('{').TrimEnd('}');
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Dictionary<string, object> dicM = new Dictionary<string, object>();
            dic = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(json_str);

            if (projectName != null)
            {
                dicM = JsonHelper.DeserializeJsonToObject<Dictionary<string, object>>(projectName);
            }

            foreach (var item in dic)
            {
                DataSet ds = JsonHelper.DeserializeJsonToObject<DataSet>(item.Value.ToString().TrimStart('[').TrimEnd(']'));
                IDictionary<string, IList<IDictionary<string, string>>> diclist = new Dictionary<string, IList<IDictionary<string, string>>>();
                for (int d = 0; d < ds.Tables.Count; d++)
                {
                    IList<IDictionary<string, string>> list = new List<IDictionary<string, string>>();
                    DataTable dt = ds.Tables[d];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IDictionary<string, string> openWith = new Dictionary<string, string>();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            openWith.Add(dt.Columns[j].ColumnName, dt.Rows[i][dt.Columns[j].ColumnName].ToString());
                        }
                        list.Add(openWith);
                    }
                    diclist.Add(ds.Tables[d].TableName, list);
                }
                dataExtra.Add(item.Key, diclist);
            }
            return dataExtra;
        }
    }
}
