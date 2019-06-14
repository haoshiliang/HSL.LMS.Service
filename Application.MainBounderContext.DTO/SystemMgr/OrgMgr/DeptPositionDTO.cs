using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr
{
    public class DeptPositionDTO
    {
        public DeptPositionDTO()
        {
            PositionList = new List<DeptPositionDTO>();
        }

        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否选择
        /// </summary>
        public bool IsChecked { get; set; }
        /// <summary>
        /// 职位列表
        /// </summary>
        public IList<DeptPositionDTO> PositionList { get; set; }
    }
}
