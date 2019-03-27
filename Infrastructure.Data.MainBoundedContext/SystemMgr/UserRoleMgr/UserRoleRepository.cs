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
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(MainUnitOfWork unitWork) : base(unitWork)
        {
            
        }

        #region 公共方法 

        /// <summary>
        /// 批量删除角色用户信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public void BatchRemore(string userId, string roleId)
        {
            var paramNameList = new List<string>();
            var paramValueList = new List<object>();
            var sqlBuilder = new StringBuilder();
            try
            {
                sqlBuilder.AppendLine("DELETE FROM SYS_ROLE_USER ru");
                sqlBuilder.AppendLine(" WHERE 1 = 1");
                if (!string.IsNullOrEmpty(userId))
                {
                    paramNameList.Add("UserId");
                    paramValueList.Add(userId);
                    sqlBuilder.AppendLine("AND ru.USER_ID = @UserId");
                }
                if (!string.IsNullOrEmpty(roleId))
                {
                    paramNameList.Add("RoleId");
                    paramValueList.Add(roleId);
                    sqlBuilder.AppendLine("AND ru.ROLE_ID = @RoleId");
                }
                base.ExecuteSql(sqlBuilder.ToString(), paramNameList.ToArray(), paramValueList.ToArray());
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
