using CalDebugTools.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalDebugTools
{
    public static class Comm
    {
        /// <summary>
        /// 转换成DataSet，若被转换对象为Null，则返回Null。
        /// </summary>
        /// <param name="obj">一个对象实例</param>
        /// <returns>返回DataSet，若被转换对象为Null，则返回Null。</returns>
        public static DataSet ToDataSet(this object obj)
        {
            if (obj == null)
                return null;
            if (obj is DataSet)
            {
                return obj as DataSet;
            }
            DataSet ds = new DataSet();
            if (obj is DataTable)
            {
                ds.Tables.Add(obj as DataTable);
                return ds;
            }
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            IEnumerable<object> list = obj as IEnumerable<object>;
            if (list == null)
            {
                var properties = obj.GetType().GetProperties().ToList();
                if (properties.Count == 0)
                    return null;
                properties.ForEach(s => dt.Columns.Add(s.Name, Nullable.GetUnderlyingType(s.PropertyType) ?? s.PropertyType));
                var row = dt.NewRow();
                properties.ForEach(s => row[s.Name] = s.GetValue(obj, null) ?? DBNull.Value);
                dt.Rows.Add(row);
            }
            else
            {
                try
                {
                    var properties = list.ToList()[0].GetType().GetProperties().ToList();
                    properties.ForEach(s => dt.Columns.Add(s.Name, Nullable.GetUnderlyingType(s.PropertyType) ?? s.PropertyType));
                    foreach (var i in list)
                    {
                        var row = dt.NewRow();
                        properties.ForEach(s => row[s.Name] = s.GetValue(i, null) ?? DBNull.Value);
                        dt.Rows.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            return ds;
        }



        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            //^-?\\d+(\\.\\d+)?$
            //^[+-]?\d*[.]?\d*$
            if (!string.IsNullOrEmpty(str) && Regex.IsMatch(str, @"^\d*[.]?\d*$"))//通过正则表达式验证输入的是否是数字
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckValueEquals(string A, string B)
        {
            bool flag = true;

            if (A == "0.0000" && (B.Trim() == "0" || B.Trim() == "0.0" || string.IsNullOrEmpty(B)))
            {
                return true;
            }
            if (string.IsNullOrEmpty(A) && string.IsNullOrEmpty(B))
            {
                return true;

            }
            if (string.IsNullOrEmpty(A) && B.Trim() == "0")
            {
                return true;
            }
            if (A.Trim()=="0" && string.IsNullOrEmpty(B))
            {
                return true;
            }
       

            if (A.Trim().ToUpper() == "FALSE" && (string.IsNullOrEmpty(B) || B.Trim() == "0"))
            {
                return true;

            }
            if (A.Trim().ToUpper() == "TRUE" && (string.IsNullOrEmpty(B) || B.Trim() == "1"))
            {
                return true;

            }
            if (IsNumeric(A) && IsNumeric(B))
            {
                if (Convert.ToDecimal(A) == Convert.ToDecimal(B))
                {
                    return true;
                }
            }

            if (A.Trim().ToUpper() == B.Trim().ToUpper())
            {
                return true;
            }

            return false;


        }



        /// <summary>
        /// 初始话数据库选择
        /// </summary>
        public static List<JCJGConnectInfo> InitBaseData( out  string  msg)
        {
            msg = "";
            List<string> jcjgInfos = Common.StringsOper.GetTextList(AppDomain.CurrentDomain.BaseDirectory + @"Resources\检测机构配置.txt");

            //数据库信息
            List<JCJGConnectInfo> listData = new List<JCJGConnectInfo>();
            List<string> arrInfo = new List<string>();
            if (jcjgInfos.Count == 0)
            {
                msg = "获取数据库配置信息异常，请确认配置文件格式！";
                return listData;
            }
            JCJGConnectInfo data = new JCJGConnectInfo();

            data.Id = "0";
            data.Abbrevition = "ALL";
            data.Name = "全部";
            data.Code = "";
            listData.Add(data);
            foreach (var info in jcjgInfos)
            {
                if (info.StartsWith("--"))
                {
                    continue;
                }

                arrInfo = info.Split('-').ToList();

                if (arrInfo.Count != 4)
                {
                    continue;
                }
                data = new JCJGConnectInfo();

                data.Id = arrInfo[0];
                data.Abbrevition = arrInfo[1];
                data.Name = arrInfo[2];
                data.Code = arrInfo[3];
                listData.Add(data);

            }

            return listData;
           
        }

    }
}
