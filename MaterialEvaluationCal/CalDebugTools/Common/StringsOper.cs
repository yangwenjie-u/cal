using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalDebugTools.Common
{
    public static class StringsOper
    {
        /// <summary>
        /// 一行一行的读取txt文件
        /// </summary>
        /// <param name="path"></param>
        public static List<string> GetTextList(string path)
        {

            List<string> result = new List<string>();
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                result.Add(line.ToString());
            }
            return result;
        }

    }
}
