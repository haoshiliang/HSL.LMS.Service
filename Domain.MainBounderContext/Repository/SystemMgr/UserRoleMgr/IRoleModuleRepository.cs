using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Repository
{
    public interface IRoleModuleRepository : IRepository<RoleModule>
    {
        #region 公共方法 

        /// <summary>
        /// 批量删除角色模块信息
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        void BatchRemore(string roleId);

        #endregion
    }
}
