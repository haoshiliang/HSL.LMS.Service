using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr
{
    public class CorpDeptPositionDTO
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public string CorpId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CorpName { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartName { get; set; }
        /// <summary>
        /// 职位ID
        /// </summary>
        public string PositionId { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// 完整职位ID
        /// 公司ID$部门ID$职位ID
        /// </summary>
        public string FullPositionId { get; set; }
        /// <summary>
        /// 完整职位ID_NAME
        /// 公司ID^公司名称$部门ID^部门名称$职位ID^职位名称
        /// </summary>
        public string FullPositionIdName { get; set; }
        /// <summary>
        /// 是否选择
        /// </summary>
        public int IsSelected { get; set; }
    }
}
