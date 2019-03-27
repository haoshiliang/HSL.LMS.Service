using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity
{
    public class UserRole : EntityBase
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public virtual string RoleId { get; set; }
        /// <summary>
        /// 角色模型
        /// </summary>
        public virtual Role RoleModule { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public virtual string UserId { get; set; }
        /// <summary>
        /// 用户模型
        /// </summary>
        public virtual User UserModule { get; set; }
        /// <summary>
        /// 标识角色是否为主入口
        /// </summary>
        public virtual bool IsRoleMaster { get; set; }
    }
}
