using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalDebugTools.Model
{
    [Serializable]
    public class TokenEntity
    {
        /// <summary>
        /// 状态吗
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// token数据
        /// </summary>
        public Token data { get; set; }
    }
}
