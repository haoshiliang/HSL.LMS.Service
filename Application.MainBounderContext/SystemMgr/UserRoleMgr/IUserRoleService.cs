using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;

namespace LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr
{
    public interface IUserRoleService
    {
        /// <summary>
        /// 添加用户角色
        /// </summary>
        /// <param name="userRoleList">角色用户列表</param>
        /// <param name="roleId">角色编号</param>
        /// <param name="userId">用户编号</param>
        void Add(IList<UserRole> userRoleList, string userId, string roleId);
    }
}
