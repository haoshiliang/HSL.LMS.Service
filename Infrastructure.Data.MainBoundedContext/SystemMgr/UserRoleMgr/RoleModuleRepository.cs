using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Repository;
using System.Linq.Expressions;

namespace LMS.Infrastructure.Data.MainBoundedContext.SystemMgr.UserRoleMgr
{
    public class RoleModuleRepository : Repository<RoleModule>, IRoleModuleRepository
    {
        public RoleModuleRepository(MainUnitOfWork unitWork) : base(unitWork)
        {
            
        }

        #region 公共方法 

        /// <summary>
        /// 批量删除角色模块信息
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        public void BatchRemore(string roleId)
        {
            var sqlBuilder = new StringBuilder();
            try
            {
                sqlBuilder.AppendLine("DELETE FROM SYS_ROLE_MODULE rm");
                sqlBuilder.AppendLine(" WHERE rm.ROLE_ID = @RoleId");

                base.ExecuteSql(sqlBuilder.ToString(), new string[] { "RoleId" }, new object[] { roleId });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
