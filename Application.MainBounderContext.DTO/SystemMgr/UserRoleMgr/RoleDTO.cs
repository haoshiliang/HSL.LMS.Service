using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr
{
    public class RoleDTO
    {
        public RoleDTO()
        {
        }

        public virtual string Id { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        public virtual string RoleCode { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public virtual string RoleName { get; set; }
        /// <summary>
        /// 角色简拼
        /// </summary>
        public virtual string PyCode { get; set; }
    }
}
