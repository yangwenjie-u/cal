using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalDebugTools.Model
{
    [Serializable] 
    public class Token
    {
        /// <summary>
        /// Token
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 获取时间
        /// </summary>
        public DateTime auth_time { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime expires_at { get; set; }
        /// <summary>
        /// 获取用户名
        /// </summary>
        public string username { get; set; }
    }
}
