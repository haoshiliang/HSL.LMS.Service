using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;

namespace LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr
{
    public interface IRoleModuleService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="modelList">角色模块</param>
        /// <param name="roleId">角色编号</param>
        void Add(IList<RoleModule> modelList, string roleId);
    }
}
