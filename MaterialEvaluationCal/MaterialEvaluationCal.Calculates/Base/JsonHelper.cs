using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Base
{
    /// <summary>
    /// Json帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            // string json = JsonConvert.SerializeObject(o, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });//空数据不格式化
            string json = JsonConvert.SerializeObject(o);//默认空数据json会出来“null”字符串

            return json;
        }
        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }
        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }
        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        public static string GetExtraDataJson(string work, string tablename)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"calcData\":{\"" + work + "\":");
            string sql = string.Format(@"select * from " + tablename);
            Base.SqlBase sqlbase = new Base.SqlBase("local");
            DataSet ds = sqlbase.ExecuteDataset(sql);
            ds.Tables[0].TableName = tablename;
            string json = Base.JsonHelper.SerializeObject(ds);
            sb.Append(json);
            sb.Append("},\"code\":1,\"message\":\"成功\"}");
            GetDictionary(sb.ToString(), work);
            return sb.ToString();
        }

        public static IDictionary<string, IList<IDictionary<string, string>>> GetDictionary(string json, string work)
        {
            IDictionary<string, IList<IDictionary<string, string>>> dataExtra = new Dictionary<string, IList<IDictionary<string, string>>>();
            var json_main = new { calcData = new object(), message = string.Empty };
            var stract_class = Base.JsonHelper.DeserializeAnonymousType(json, json_main);
            string json_str = stract_class.calcData.ToString().Replace("\"" + work + "\":", "").TrimStart('{').TrimEnd('}');
            DataSet ds = Base.JsonHelper.DeserializeJsonToObject<DataSet>(json_str);
            string name = ds.Tables[0].TableName;
            DataTable dt = ds.Tables[0];
            IList<IDictionary<string, string>> list = new List<IDictionary<string, string>>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IDictionary<string, string> openWith = new Dictionary<string, string>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    openWith.Add(dt.Columns[j].ColumnName, dt.Rows[i][dt.Columns[j].ColumnName].ToString());
                }
                list.Add(openWith);
            }
            dataExtra.Add(name, list);
            return dataExtra;
        }
        public static IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> GetAfferentDictionary(string json, string mJson = null, List<string> jsonList = null)
        {
            IDictionary<string, IDictionary<string, IList<IDictionary<string, string>>>> dataExtra = new Dictionary<string, IDictionary<string, IList<IDictionary<string, string>>>>();
            var json_main = new { calcData = new object(), message = string.Empty };
            var stract_class = DeserializeAnonymousType(json, json_main);
            string json_str = stract_class.calcData.ToString().TrimStart('[').TrimEnd(']').TrimStart('{').TrimEnd('}');
            Dictionary<string, object> dic = new Dictionary<string, object>();
            Dictionary<string, object> dicM = new Dictionary<string, object>();
            dic = DeserializeJsonToObject<Dictionary<string, object>>(json_str);

            if (mJson != null)
            {
                dicM = DeserializeJsonToObject<Dictionary<string, object>>(mJson.ToUpper());
            }

            if (jsonList != null)
            {
                foreach (var item in jsonList)
                {
                    dicM = DeserializeJsonToObject<Dictionary<string, object>>(item.ToUpper());
                }
            }
            foreach (var item in dic)
            {
                DataSet ds = DeserializeJsonToObject<DataSet>(item.Value.ToString().TrimStart('[').TrimEnd(']'));
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
                            openWith.Add(dt.Columns[j].ColumnName.ToUpper(), dt.Rows[i][dt.Columns[j].ColumnName].ToString());
                        }
                        list.Add(openWith);
                    }
                    diclist.Add(ds.Tables[d].TableName, list);
                }
                dataExtra.Add(item.Key, diclist);
            }
            return dataExtra;
        }

        public static IDictionary<string, IList<IDictionary<string, string>>> GetAfferentDictionaryNew(string json, string mJson = null, List<string> jsonList = null)
        {
           IDictionary<string, IList<IDictionary<string, string>>> dataExtra = new Dictionary<string, IList<IDictionary<string, string>>>();
            var json_main = new { data = new object(), message = string.Empty };
            var stract_class = DeserializeAnonymousType(json, json_main);
            string json_str = stract_class.data.ToString().TrimStart('[').TrimEnd(']').TrimStart('{').TrimEnd('}');
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic = DeserializeJsonToObject<Dictionary<string, object>>(json_str);
            foreach (var item in dic)
            {
                IDictionary<string, string> dicitem = new Dictionary<string, string>();
                dicitem = DeserializeJsonToObject<Dictionary<string, string>>(item.Value.ToString().Replace("[", "").Replace("]", ""));
            
                IList<IDictionary<string, string>> list = new List<IDictionary<string, string>>();
                list.Add(dicitem);
                dataExtra.Add(item.Key, list);
            }
           

            //foreach (var item in dic)
            //{
            //    DataSet ds = DeserializeAnonymousType<DataSet>(item);
            //    IDictionary<string, IList<IDictionary<string, string>>> diclist = new Dictionary<string, IList<IDictionary<string, string>>>();
            //    for (int d = 0; d < ds.Tables.Count; d++)
            //    {
            //        IList<IDictionary<string, string>> list = new List<IDictionary<string, string>>();
            //        DataTable dt = ds.Tables[d];
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            IDictionary<string, string> openWith = new Dictionary<string, string>();
            //            for (int j = 0; j < dt.Columns.Count; j++)
            //            {
            //                openWith.Add(dt.Columns[j].ColumnName.ToUpper(), dt.Rows[i][dt.Columns[j].ColumnName].ToString());
            //            }
            //            list.Add(openWith);
            //        }
            //        diclist.Add(ds.Tables[d].TableName, list);
            //    }
            //    dataExtra.Add(item.Key, diclist);
            //}
            return dataExtra;
        }


        public static string GetAfferentDataJson(string type, string sql)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb.Append("{\"calcData\":[{");
            //string sql = string.Format(@"select top 1 * from " + tableName);
            Base.SqlBase sqlbase = new Base.SqlBase("local");
            DataSet ds = sqlbase.ExecuteDataset(sql);
            DataTable dt = ds.Tables[0];
            string[] jcxm_list = dt.Rows[0]["jcxm"].ToString().Split('、');
            foreach (string item in jcxm_list)
            {
                sb2.Append("\"" + item + "\":{");
                DataTable dt_json = ToDataTable(dt.Select(" jcxm like '%" + item.Trim() + "%'"));
                string json = Base.JsonHelper.SerializeObject(dt_json);
                sb2.Append("\"" + type + "\":" + json + "");
                sb2.Append(",\"S_BY_RW_XQ\":[");
                for (int i = 0; i < dt.Select(" jcxm like '%" + item.Trim() + "%'").Length; i++)
                {
                    sb2.Append("{\"RECID\":\"19085206791636631332933\",");
                    sb2.Append("\"SJWCJSSJ\":\"" + DateTime.Now.ToString() + "\",");
                    sb2.Append("\"JCJG\":\"\",");
                    sb2.Append("\"JCJGMS\":\"\"");
                    sb2.Append("},");
                }
                sb2.Remove(sb2.Length - 1, 1).Append("]},");

                sb2.Append("\"BGJG\": {\"M_BY_BG\": [{\"JCJG\": \"不合格\",\"JCJGMS\": \"\"}]},");
            }
            sb.Append(sb2.ToString().TrimEnd(','));
            sb.Append("}],\"code\":1, \"message\":\"成功\"}");
            return sb.ToString();
        }

        public static string GetAfferentDataJson(string type, string sql, string m_json = null)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb.Append("{\"calcData\":[{");
            //string sql = string.Format(@"select top 1 * from " + tableName);
            Base.SqlBase sqlbase = new Base.SqlBase("local");
            DataSet ds = sqlbase.ExecuteDataset(sql);
            DataTable dt = ds.Tables[0];
            string[] jcxm_list = dt.Rows[0]["jcxm"].ToString().Split('、', ',');
            foreach (string item in jcxm_list)
            {
                sb2.Append("\"" + item + "\":{");
                DataTable dt_json = ToDataTable(dt.Select(" jcxm like '%" + item.Trim() + "%'"));

                string json = Base.JsonHelper.SerializeObject(dt_json);
                sb2.Append("\"" + type + "\":" + json + "");

                sb2.Append(",\"S_BY_RW_XQ\":[{");
                sb2.Append("\"RECID\":\"19085206791636631332933\",");
                sb2.Append("\"SJWCKSSJ\":\"1900/1/1 0:00:00\",");
                sb2.Append("\"SJWCJSSJ\":\"1900/1/1 0:00:00\",");
                //sb2.Append("\"JCJG\":\"\",");
                //sb2.Append("\"JCJGMS\":\"\"");
                sb2.Append("}]");

                if (m_json != null)
                {
                    sb2.Append(m_json);
                }
                sb2.Append("},");
            }
            sb.Append(sb2.ToString().TrimEnd(','));
            sb.Append("}],\"code\":1, \"message\":\"成功\"}");

            return sb.ToString();
        }
        public static string GetAfferentDataJson2(string type, string sql, string m_json = null, string y_json = null)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            sb.Append("{\"data\":[{");
            Base.SqlBase sqlbase = new Base.SqlBase("local");
            DataSet ds = sqlbase.ExecuteDataset(sql);
            DataTable dt = ds.Tables[0];

            string json = Base.JsonHelper.SerializeObject(dt);
            sb2.Append("\"" + type + "\":");

            sb2.Append(json);
            if (m_json != null)
            {
                sb2.Append(m_json);
            }
            if (y_json != null)
                sb2.Append(y_json);
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


        public static string GetDataJson(string type, string sqlstr, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"calcData\":{\"" + type + "\":");

            string sql = string.Format(@sqlstr);
            Base.SqlBase sqlbase = new Base.SqlBase();
            DataSet ds = sqlbase.ExecuteDataset(sql);

            ds.Tables[0].TableName = tableName;
            string json = Base.JsonHelper.SerializeObject(ds);
            sb.Append(json);
            sb.Append("},\"code\":1,\"message\":\"成功\"}");
            Base.JsonHelper.GetDictionary(sb.ToString(), type);
            return sb.ToString();
        }

        public static string GetMdataJson(string type, string sql, string table_name)
        {
            StringBuilder sb = new StringBuilder();
            Base.SqlBase sqlbase = new Base.SqlBase(type);
            DataSet ds = sqlbase.ExecuteDataset(sql);
            DataTable dt = ds.Tables[0];
            string json = Base.JsonHelper.SerializeObject(dt);
            sb.Append(",\"" + table_name + "\":");
            sb.Append(json);

            return sb.ToString();
        }
    }
}
/*
 https://www.cnblogs.com/freshman0216/p/4161800.html
 /// <summary>
    /// Json测试
    /// </summary>
    public class JsonTest : IRun
    {
        public void Run()
        {
            Student sdudent = new Student();
            sdudent.ID = 1;
            sdudent.Name = "陈晨";
            sdudent.NickName = "石子儿";
            sdudent.Class = new Class() { Name = "CS0216", ID = 0216 };

            //实体序列化和反序列化
            string json1 = JsonHelper.SerializeObject(sdudent);
            //json1 : {"ID":1,"Name":"陈晨","NickName":"石子儿","Class":{"ID":216,"Name":"CS0216"}}
            Student sdudent1 = JsonHelper.DeserializeJsonToObject<Student>(json1);

            //实体集合序列化和反序列化
            List<Student> sdudentList = new List<Student>() { sdudent, sdudent1 };
            string json2 = JsonHelper.SerializeObject(sdudentList);
            //json: [{"ID":1,"Name":"陈晨","NickName":"石子儿","Class":{"ID":216,"Name":"CS0216"}},{"ID":1,"Name":"陈晨","NickName":"石子儿","Class":{"ID":216,"Name":"CS0216"}}]
            List<Student> sdudentList2 = JsonHelper.DeserializeJsonToList<Student>(json2);

            //DataTable序列化和反序列化
            DataTable dt = new DataTable();
            dt.TableName = "Student";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name");
            dt.Columns.Add("NickName");
            DataRow dr = dt.NewRow();
            dr["ID"] = 112;
            dr["Name"] = "战三";
            dr["NickName"] = "小三";
            dt.Rows.Add(dr);
            string json3 = JsonHelper.SerializeObject(dt);
            //json3 : [{"ID":112,"Name":"战三","NickName":"小三"}]
            DataTable sdudentDt3 = JsonHelper.DeserializeJsonToObject<DataTable>(json3);
            List<Student> sdudentList3 = JsonHelper.DeserializeJsonToList<Student>(json3);

            //验证对象和数组
            Student sdudent4 = JsonHelper.DeserializeJsonToObject<Student>("{\"ID\":\"112\",\"Name\":\"石子儿\"}");
            List<Student> sdudentList4 = JsonHelper.DeserializeJsonToList<Student>("[{\"ID\":\"112\",\"Name\":\"石子儿\"}]");

            //匿名对象解析
            var tempEntity = new { ID = 0, Name = string.Empty };
            string json5 = JsonHelper.SerializeObject(tempEntity);
            //json5 : {"ID":0,"Name":""}
            tempEntity = JsonHelper.DeserializeAnonymousType("{\"ID\":\"112\",\"Name\":\"石子儿\"}", tempEntity);
            var tempStudent = new Student();
            tempStudent = JsonHelper.DeserializeAnonymousType("{\"ID\":\"112\",\"Name\":\"石子儿\"}", tempStudent);

            Console.Read();
        }

    }

    /// <summary>
    /// 学生信息实体
    /// </summary>
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public Class Class { get; set; }
    }

    /// <summary>
    /// 学生班级实体
    /// </summary>
    public class Class
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
 */

