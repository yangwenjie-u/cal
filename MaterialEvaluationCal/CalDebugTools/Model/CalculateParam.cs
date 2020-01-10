using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalDebugTools.Model
{
    public class CalculateParam
    {
        public string Recid { set; get; }

        public string SSDWBH { set; get; }
        public string SYXMBH { set; get; }
        public string JCXM { set; get; }
        public string LocalTableName { set; get; }
        public string LocalZdName { set; get; }
        public string RemoteTableName { set; get; }
        public string RemoteZdName { set; get; }
    }
}
