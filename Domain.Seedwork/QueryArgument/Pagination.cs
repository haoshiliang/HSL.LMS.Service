using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Seedwork
{
    public class Pagination
    {
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordTotal { get; set; }

        /// <summary>
        /// 页总数
        /// </summary>
        public int PageTotal
        {
            get
            {
                if (RecordTotal > 0)
                {
                    return RecordTotal % this.PageSize == 0 ? RecordTotal / this.PageSize : RecordTotal / this.PageSize + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
