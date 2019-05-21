using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Seedwork
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public class QueryParam
    {
        public QueryParam()
        {
            WhereList = new List<WhereParam>();
            SortList = new List<SortField>();
            IsAdvancedQuery = false;
        }
        /// <summary>
        /// 是否高级查询
        /// </summary>
        public bool IsAdvancedQuery { get; set; }
        /// <summary>
        /// 条件列表
        /// </summary>
        public IList<WhereParam> WhereList { get; set; }
        /// <summary>
        /// 排序列表
        /// </summary>
        public IList<SortField> SortList { get; set; }
    }
}
