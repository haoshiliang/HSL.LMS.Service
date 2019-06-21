using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.Common
{
    /// <summary>
    /// 下拉DTO
    /// 最多支持四级联动
    /// </summary>
    public class SelectDTO
    {
        /// <summary>
        /// 下拉值
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 下拉显示名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 关联信息Id
        /// </summary>
        public string RelationId_1 { get; set; }
        /// <summary>
        /// 关联信息Id
        /// </summary>
        public string RelationId_2 { get; set; }
        /// <summary>
        /// 关联信息Id
        /// </summary>
        public string RelationId_3 { get; set; }
    }
}
