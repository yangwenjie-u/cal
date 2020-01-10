using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalDebugTools.Model
{
    /// <summary>
    ///  ProjectInfo    
    /// </summary>
    public class ProjectInfo
    {
        /// <summary>
        /// 主键 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 项目编号   
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 主表名    
        /// </summary>
        public string MTable { get; set; }

        /// <summary>
        /// 从表名    
        /// </summary>
        public string STable { get; set; }

        /// <summary>
        /// 帮助表名    
        /// </summary>
        public string BZTable { get; set; }

        /// <summary>
        /// 数据表
        /// </summary>
        public String YTable { get; set; }

        /// <summary>
        /// 数据表查询字段
        /// </summary>
        public string DataFiled { get; set; }

        public string Cal { get; set; }

    }

}
