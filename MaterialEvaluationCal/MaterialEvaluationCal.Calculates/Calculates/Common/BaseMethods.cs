using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace Calculates
{
    public partial class BaseMethods : BaseData
    {
        public IDictionary<string, IList<IDictionary<string, string>>> dataExtra = new Dictionary<string, IList<IDictionary<string, string>>>(StringComparer.OrdinalIgnoreCase);
        public IDictionary<string, IList<IDictionary<string, string>>> retData = null;


        public bool Calculate(IDictionary<string, IList<IDictionary<string, string>>> dataExtraTmp,
            IDictionary<string, IList<IDictionary<string, string>>> retDataTmp, out string err)
        {
            err = "";
            dataExtra = dataExtraTmp;
            retData = retDataTmp;
            return true;
        }

        #region 获取标准数据
        ///// <summary>
        ///// 获取标准表数据   Table[JYDBH=123&SYRQ=].Field
        ///// </summary>
        ///// <param name="format">参数</param>
        ///// <param name="condition">条件</param>
        ///// <returns></returns>
        //private string GetExtraData(string format, string condition)
        //{
        //    var s = format.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        //    if (s.Length != 2)
        //    {
        //        throw new Exception(format + "格式错误");
        //    }

        //}
        /// <summary>
        /// 获取标准表数据
        /// </summary>
        /// <param name="format">参数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public string GetExtraData(string format, Func<IDictionary<string, string>, bool> condition)
        {
            try
            {
                var s = format.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length != 2)
                {
                    throw new Exception(format + "格式错误");
                }
                if (!dataExtraTmp.ContainsKey(s[0]))
                {
                    throw new Exception(s[0] + "表数据不存在");
                }
                var tableData = dataExtraTmp[s[0]];
                var fieldData = tableData.FirstOrDefault(condition);
                if (fieldData != null && !fieldData.ContainsKey(s[1]))
                {
                    throw new Exception(s[0] + "表不存在" + s[1] + "字段");
                }
                if (fieldData != null)
                {
                    return fieldData[s[1]].Trim();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取标准参数数量
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int GetExtraDataCount(string table)
        {
            if (!dataExtraTmp.ContainsKey(table))
            {
                throw new Exception(table + "表数据不存在");
            }
            var tableData = dataExtraTmp[table];
            return tableData.Count;
        }

        /// <summary>
        /// 获取标准表数据   Table[JYDBH=123&SYRQ=].Field
        /// </summary>
        /// <param name="format">参数</param>
        /// <param name="condition">序列</param>
        /// <returns></returns>
        public string GetExtraData(string format, int condition = 0)
        {
            var s = format.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length != 2)
            {
                throw new Exception(format + "格式错误");
            }
            if (!dataExtraTmp.ContainsKey(s[0]))
            {
                throw new Exception(s[0] + "表数据不存在");
            }
            var tableData = dataExtraTmp[s[0]];
            if (tableData.Count > condition)
            {
                var fieldData = tableData[condition];
                if (!fieldData.ContainsKey(s[1]))
                {
                    throw new Exception(s[0] + "表不存在" + s[1] + "字段");
                }
                return fieldData[s[1]].Trim();
            }
            else
            {
                throw new Exception("序号:" + condition + ";超过表存在的条数");
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="format"></param>
        /// <param name="t">0：字符串（默认）；1：数字；2：日期</param>
        /// <param name="asc"></param>
        public void SortExtraData(string format, int t = 0, bool asc = true)
        {
            var s = format.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length != 2)
            {
                throw new Exception(format + "格式错误");
            }
            if (!dataExtraTmp.ContainsKey(s[0]))
            {
                throw new Exception(s[0] + "表数据不存在");
            }
            var tableData = dataExtraTmp[s[0]];
            if (tableData.Count > 0)
            {
                var fieldData = tableData[0];
                if (!fieldData.ContainsKey(s[1]))
                {
                    throw new Exception(s[0] + "表不存在" + s[1] + "字段");
                }
                if (asc)
                {
                    if (t == 1)
                    {
                        tableData = tableData.OrderBy(x => Convert.ToDouble(x[s[1]])).ToList();
                    }
                    else if (t == 2)
                    {
                        tableData = tableData.OrderBy(x => Convert.ToDateTime(x[s[1]])).ToList();
                    }
                    else
                    {
                        tableData = tableData.OrderBy(x => x[s[1]]).ToList();
                    }
                }
                else
                {
                    if (t == 1)
                    {
                        tableData = tableData.OrderByDescending(x => Convert.ToDouble(x[s[1]])).ToList();
                    }
                    else if (t == 2)
                    {
                        tableData = tableData.OrderByDescending(x => Convert.ToDateTime(x[s[1]])).ToList();
                    }
                    else
                    {
                        tableData = tableData.OrderByDescending(x => x[s[1]]).ToList();
                    }
                }
                dataExtraTmp[s[0]] = tableData;
            }
        }
        #endregion

        #region 获取上传数据

        /// <summary>
        /// 获取上传表数据
        /// </summary>
        /// <param name="format">参数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public string GetData(string format, Func<IDictionary<string, string>, bool> condition)
        {
            var s = format.Split(new char[] { '.' });
            if (s.Length != 3)
            {
                throw new Exception(format + "格式错误");
            }
            if (!retDataTmp.ContainsKey(s[0]))
            {
                throw new Exception(s[0] + "检测项目不存在");
            }
            var jcxmData = retDataTmp[s[0]];

            if (!jcxmData.ContainsKey(s[1]))
            {
                throw new Exception(s[1] + "表数据不存在");
            }

            var tableData = jcxmData[s[1]];

            var fieldData = tableData.FirstOrDefault(condition);
            if (fieldData != null && !fieldData.ContainsKey(s[2]))
            {
                throw new Exception(s[1] + "表不存在" + s[2] + "字段");
            }
            if (fieldData != null)
            {
                return fieldData[s[2]].Trim();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取上传表数据
        /// </summary>
        /// <param name="format">参数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int GetDataCount(string format)
        {
            var s = format.Split(new char[] { '.' });
            if (s.Length != 2)
            {
                throw new Exception(format + "格式错误");
            }
            if (!retDataTmp.ContainsKey(s[0]))
            {
                throw new Exception(s[0] + "检测项目不存在");
            }
            var jcxmData = retDataTmp[s[0]];

            if (!jcxmData.ContainsKey(s[1]))
            {
                throw new Exception(s[1] + "表数据不存在");
            }

            var tableData = jcxmData[s[1]];
            return tableData.Count;
        }

        /// <summary>
        /// 获取上传表数据
        /// </summary>
        /// <param name="format">参数</param>
        /// <param name="condition">序列</param>
        /// <returns></returns>
        public string GetData(string format, int condition = 0)
        {
            var s = format.Split(new char[] { '.' });
            if (s.Length != 3)
            {
                throw new Exception(format + "格式错误");
            }
            if (!retDataTmp.ContainsKey(s[0]))
            {
                throw new Exception(s[0] + "检测项目不存在");
            }
            var jcxmData = retDataTmp[s[0]];

            if (!jcxmData.ContainsKey(s[1]))
            {
                throw new Exception(s[1] + "表数据不存在");
            }

            var tableData = jcxmData[s[1]];
            if (tableData.Count > condition)
            {
                var fieldData = tableData[condition];
                if (!fieldData.ContainsKey(s[2]))
                {
                    throw new Exception(s[1] + "表不存在" + s[2] + "字段");
                }
                return fieldData[s[2]].Trim();
            }
            else
            {
                throw new Exception("序号:" + condition + ";超过表存在的条数");
            }
        }

        /// <summary>
        /// 设置上传表数据
        /// </summary>
        /// <param name="format">参数</param>
        /// <param name="condition">序列</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public void SetData(string format, string value, int condition = 0)
        {
            var s = format.Split(new char[] { '.' });
            if (s.Length != 3)
            {
                throw new Exception(format + "格式错误");
            }
            if (!retDataTmp.ContainsKey(s[0]))
            {
                throw new Exception(s[0] + "检测项目不存在");
            }
            var jcxmData = retDataTmp[s[0]];

            if (!jcxmData.ContainsKey(s[1]))
            {
                throw new Exception(s[1] + "表数据不存在");
            }

            var tableData = jcxmData[s[1]];
            if (tableData.Count > condition)
            {
                var fieldData = tableData[condition];
                if (!fieldData.ContainsKey(s[2]))
                {
                    fieldData.Add(s[2], value);
                }
                else
                {
                    fieldData[s[2]] = value;
                }
            }
            else
            {
                throw new Exception("序号:" + condition + ";超过表存在的条数");
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="format"></param>
        /// <param name="t">0：字符串（默认）；1：数字；2：日期</param>
        /// <param name="asc"></param>
        public void SortData(string format, int t = 0, bool asc = true)
        {
            var s = format.Split(new char[] { '.' });
            if (s.Length != 3)
            {
                throw new Exception(format + "格式错误");
            }
            if (!retDataTmp.ContainsKey(s[0]))
            {
                throw new Exception(s[0] + "检测项目不存在");
            }
            var jcxmData = retDataTmp[s[0]];

            if (!jcxmData.ContainsKey(s[1]))
            {
                throw new Exception(s[1] + "表数据不存在");
            }

            var tableData = jcxmData[s[1]];
            if (tableData.Count > 0)
            {
                var fieldData = tableData[0];
                if (!fieldData.ContainsKey(s[2]))
                {
                    throw new Exception(s[1] + "表不存在" + s[2] + "字段");
                }
                if (asc)
                {
                    if (t == 1)
                    {
                        tableData = tableData.OrderBy(x => Convert.ToDouble(x[s[2]])).ToList();
                    }
                    else if (t == 2)
                    {
                        tableData = tableData.OrderBy(x => Convert.ToDateTime(x[s[2]])).ToList();
                    }
                    else
                    {
                        tableData = tableData.OrderBy(x => x[s[2]]).ToList();
                    }
                }
                else
                {
                    if (t == 1)
                    {
                        tableData = tableData.OrderByDescending(x => Convert.ToDouble(x[s[2]])).ToList();
                    }
                    else if (t == 2)
                    {
                        tableData = tableData.OrderByDescending(x => Convert.ToDateTime(x[s[2]])).ToList();
                    }
                    else
                    {
                        tableData = tableData.OrderByDescending(x => x[s[2]]).ToList();
                    }
                }
                jcxmData[s[1]] = tableData;
            }
        }
        #endregion

        #region 数字
        /// <summary>
        /// 保留小数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static double Round(double d, int digits)
        {
            return Math.Round(d, digits);
        }
        /// <summary>
        /// 强制保留小数
        /// </summary>
        /// <param name="d"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public string RoundEx(double d, int digits)
        {
            var s = Math.Round(d, digits).ToString();
            if (!s.Contains("."))
            {
                s += ".";
                for (int i = 0; i < digits; i++)
                {
                    s += "0";
                }
                return s;
            }
            else
            {
                var ss = s.Split('.')[1];
                if (ss.Length < digits)
                {
                    for (int i = ss.Length; i < digits; i++)
                    {
                        s += "0";
                    }
                }
                return s;
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="asc">True 顺序 False倒叙</param>
        /// <param name="t">参数</param>
        /// <returns></returns>
        public List<T> GetSort<T>(bool asc = true, params T[] t)
        {
            List<T> s = new List<T>();
            for (int i = 0; i < t.Length; i++)
            {
                s.Add(t[i]);
            }
            if (asc)
                s = s.OrderBy(x => x).ToList();
            else
                s = s.OrderByDescending(x => x).ToList();
            return s;
        }

        /// <summary>
        /// 取中间值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetMiddle<T>(params T[] t)
        {
            var s = GetSort(true, t);
            return s[(t.Length + 1) / 2 - 1];
        }
        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetMin<T>(params T[] t)
        {
            var s = GetSort(true, t);
            return s[0];
        }
        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetMax<T>(params T[] t)
        {
            var s = GetSort(true, t);
            return s[s.Count - 1];
        }

        /// <summary>
        /// 获取数字
        /// </summary>
        /// <param name="s"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public string GetNum(string s, string desc = "")
        {
            Regex reg = new Regex("\\d+\\.?\\d*");
            var match = reg.Match(s);
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                if (desc != "")
                    throw new Exception(s + "中未找到数字");
                else
                    return "";
            }
        }
        /// <summary>
        /// 判断是不是数字
        /// </summary>
        /// <param name="str">输入的参数</param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            //^-?\\d+(\\.\\d+)?$
            //^[+-]?\d*[.]?\d*$
            if (!string.IsNullOrEmpty(str) && Regex.IsMatch(str.Trim(), @"^[+-]?\d+[.]?\d*$"))//通过正则表达式验证输入的是否是数字
            //if (!string.IsNullOrEmpty(str) && Regex.IsMatch(str, @"^\d*[.]?\d*$"))//通过正则表达式验证输入的是否是数字
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region  共用函数
        /// <summary>
        /// 校验字符串是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <param name="desc"></param>
        public static void CheckEmpty(string s, string desc)
        {
            if (String.IsNullOrEmpty(s))
            {
                throw new Exception(desc);
            }
        }

        /// <summary>
        /// 转成整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetInt(string s)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch
            {
                throw new Exception(s + "转成Int型失败");
            }
        }

        /// <summary>
        /// 转成Double型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double GetDouble(string s)
        {
            try
            {
                return Convert.ToDouble(s);
            }
            catch
            {
                throw new Exception(s + "转成Double型失败");
            }
        }

        /// <summary>
        /// 转成DateTime型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime GetDate(string s)
        {
            try
            {
                return Convert.ToDateTime(s).Date;
            }
            catch
            {
                throw new Exception(s + "转成DateTime型失败");
            }
        }

        /// <summary>
        /// 转成DateTime型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string s)
        {
            try
            {
                return Convert.ToDateTime(s);
            }
            catch
            {
                throw new Exception(s + "转成DateTime型失败");
            }
        }

        /// <summary>
        /// 转成整型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int GetSafeInt(string s, int def = 0)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// 转成Double型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static double GetSafeDouble(string s, double def = 0.0)
        {
            try
            {
                if (string.IsNullOrEmpty(s))
                {
                    return def;
                }
                return Convert.ToDouble(s);
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// 转成Decimal型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static decimal GetSafeDecimal(string s, decimal def = 0)
        {
            try
            {
                if (string.IsNullOrEmpty(s))
                {
                    return def;
                }
                return Convert.ToDecimal(s);
            }
            catch
            {
                return def;
            }
        }

        /// <summary>
        /// 转成DateTime型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime GetSafeDate(string s, string def = "1900-1-1")
        {
            try
            {
                return Convert.ToDateTime(s).Date;
            }
            catch
            {
                try
                {
                    return DateTime.Parse(def);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// 转成DateTime型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime GetSafeDateTime(string s, string def = "1900-1-1")
        {
            try
            {
                return Convert.ToDateTime(s);
            }
            catch
            {
                try
                {
                    return DateTime.Parse(def);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// 转义
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EncodeCode(string s)
        {
            return s.Replace("-", "&hbr;").Replace("#", "&wno;").Replace("|", "&vbr;");
        }

        /// <summary>
        /// 转义
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DecodeCode(string s)
        {
            s = Regex.Replace(s, "&hbr;", "-", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, "&wno;", "#", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, "&vbr;", "|", RegexOptions.IgnoreCase);
            return s;
        }
        #endregion

        #region 变量
        /// <summary>
        /// 1900年1月1号时间
        /// </summary>
        public static DateTime Date19000101
        {
            get
            {
                return DateTime.Parse("1900-1-1");
            }
        }
        #endregion

        ///<summary>
        /// 判断是否合格 默认合格/不合格
        /// </summary>
        /// <param name="sj">范围值</param>
        /// <param name="sc">比较值</param>
        /// <param name="flag">返回(符合,不符合) 还是判断(合格,不合格)  true：符合/不符合  false :合格,不合格</param>
        /// <returns></returns>
        public static string IsQualified(string sj, string sc, bool flag = false)
        {
            if (string.IsNullOrEmpty(sj) || string.IsNullOrEmpty(sc))
            {
                if (flag)
                {
                    return "不符合";
                }
                else
                {
                    return "不合格";
                }
            }
            sj = sj.Trim();
            sc = sc.Trim();

            sj = sj.Replace("~", "～");
            sj = sj.Replace(">", "＞");
            sj = sj.Replace("<", "＜");
            sj = sj.Replace("%", "");
            sc = sc.Replace("%", "");

            if (!IsNumeric(sc))
            {
                return "----";
            }
            #region 判断 取文字中的数值 

            string temStr = sj;//"提取123.11abc提取"; //我们抓取当前字符当中的123.11
            temStr = Regex.Replace(temStr, @"[^[+-]?\d.\d]", "");

            //sj 是文字加数字 如：检测值》234.43
            if (temStr.Length + 1 != sj.Length && sj.IndexOf("～") == -1)
            {
                if (sj.IndexOf(temStr) > 1)
                {
                    sj = sj.Substring(sj.IndexOf(temStr) - 1, temStr.Length + 1);
                }
            }
            #endregion

            string l_bl, r_bl = "";
            decimal min_sjz, max_sjz, scz = 0;
            bool min_bl, max_bl, sign = false;

            min_sjz = -99999;
            max_sjz = 99999;
            scz = Convert.ToDecimal(sc);

            sign = false;
            min_bl = false;
            max_bl = false;

            int length = 0, dw = 0;
            try
            {

                if (sj.IndexOf('＞') != -1)
                {
                    length = sj.Length;
                    dw = sj.IndexOf('＞');
                    dw += 1;
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw, length - dw);

                    if (!string.IsNullOrEmpty(l_bl) && IsNumeric(l_bl))
                    {
                        max_sjz = Convert.ToDecimal(l_bl);
                        max_bl = false;
                    }

                    if (!string.IsNullOrEmpty(r_bl) && IsNumeric(r_bl))
                    {
                        min_sjz = Convert.ToDecimal(r_bl);
                        min_bl = false;
                    }
                    sign = true;

                }

                if (sj.IndexOf('≥') != -1)
                {
                    length = sj.Length;
                    dw = sj.IndexOf('≥');
                    dw += 1;
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw, length - dw);

                    if (!string.IsNullOrEmpty(l_bl) && IsNumeric(l_bl))
                    {
                        max_sjz = Convert.ToDecimal(l_bl);
                        max_bl = true;
                    }
                    if (!string.IsNullOrEmpty(r_bl) && IsNumeric(r_bl))
                    {
                        min_sjz = Convert.ToDecimal(r_bl);
                        min_bl = true;
                    }
                    sign = true;
                }

                if (sj.IndexOf('＜') != -1)
                {
                    length = sj.Length;
                    dw = sj.IndexOf('＜');
                    dw += 1;
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw, length - dw);

                    if (!string.IsNullOrEmpty(l_bl) && IsNumeric(l_bl))
                    {
                        min_sjz = Convert.ToDecimal(l_bl);
                        min_bl = false;
                    }

                    if (!string.IsNullOrEmpty(r_bl) && IsNumeric(r_bl))
                    {
                        max_sjz = Convert.ToDecimal(r_bl);
                        max_bl = false;
                    }
                    sign = true;
                }

                if (sj.IndexOf('≤') != -1)
                {
                    length = sj.Length;
                    dw = sj.IndexOf('≤');
                    dw += 1;
                    l_bl = sj.Substring(0, dw - 1);
                    r_bl = sj.Substring(dw, length - dw);

                    if (!string.IsNullOrEmpty(l_bl) && IsNumeric(l_bl))
                    {
                        min_sjz = Convert.ToDecimal(l_bl);
                        min_bl = true;
                    }

                    if (!string.IsNullOrEmpty(r_bl) && IsNumeric(r_bl))
                    {
                        max_sjz = Convert.ToDecimal(r_bl);
                        max_bl = true;
                    }
                    sign = true;
                }
                if (sj.IndexOf('～') != -1)
                {
                    length = sj.Length;
                    dw = sj.IndexOf('～');
                    dw += 1;
                    min_sjz = Convert.ToDecimal(Conversion.Val(sj.Substring(0, dw - 1)));
                    max_sjz = Convert.ToDecimal(Conversion.Val(sj.Substring(dw, length - dw)));

                    max_bl = true;
                    min_bl = true;

                    sign = true;


                }
                if (sj.IndexOf('±') != -1)
                {
                    length = sj.Length;
                    dw = sj.IndexOf('±');
                    dw += 1;
                    min_sjz = Convert.ToDecimal(Conversion.Val(sj.Substring(0, dw - 1)));
                    max_sjz = Convert.ToDecimal(Conversion.Val(sj.Substring(dw, length - dw)));
                    min_sjz = Convert.ToDecimal(min_sjz - max_sjz);
                    max_sjz = Convert.ToDecimal(min_sjz + 2 * max_sjz);
                    max_bl = true;
                    min_bl = true;

                    sign = true;
                }
            }
            catch (Exception)
            {
                throw new Exception(sj + "格式不正确，请确认标准要求");
            }

            if (sj == "0")
            {
                sign = true;
                min_bl = false;
                max_bl = true;
                max_sjz = 0;
            }

            if (!sign)
            {
                return "----";
            }

            string hgjl, bhgjl = "";
            hgjl = flag ? "符合" : "合格";
            bhgjl = flag ? "不符合" : "不合格";
            sign = true;

            if (min_bl)
                sign = scz >= min_sjz ? sign : false;
            else
                sign = scz > min_sjz ? sign : false;

            if (max_bl)
                sign = scz <= max_sjz ? sign : false;
            else
                sign = scz < max_sjz ? sign : false;

            return sign ? hgjl : bhgjl;
        }

        /// <summary>
        /// 获取匹配的检测项目
        /// </summary>
        /// <param name="jcxm"></param>
        /// <param name="compareItems"></param>
        /// <returns></returns>
        public static string CurrentJcxm(string jcxm, string compareItems)
        {
            compareItems = compareItems.Replace(',', '、').Trim('、');
            if (compareItems.IndexOf('、') == -1)
            {
                return compareItems;
            }
            List<string> listItems = compareItems.Split('、').ToList();

            foreach (var item in listItems)
            {
                if (jcxm.Contains("、" + item + "、"))
                {
                    return item;
                }
            }

            return "";
        }
    }
}
