using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Seedwork
{
    public class QueryParams
    {
        /// <summary>
        /// 查询字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 查询值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
    }
}
