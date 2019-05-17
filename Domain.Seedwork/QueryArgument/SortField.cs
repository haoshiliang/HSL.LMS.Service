using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Seedwork
{
    public class SortField
    {
        public SortField()
        {
            SortValue = "";
            SortType = "ASC";
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortValue { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string SortType { get; set; }
    }
}
