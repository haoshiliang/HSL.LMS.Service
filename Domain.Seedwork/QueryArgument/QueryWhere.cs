using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Seedwork
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class QueryWhere
    {
        /// <summary>
        /// 分页参数
        /// </summary>
        public Pagination PaginationModel { get; set; }
        /// <summary>
        /// 查询条件参数
        /// </summary>
        public QueryParam QueryParamModel { get; set; }
    }
}
