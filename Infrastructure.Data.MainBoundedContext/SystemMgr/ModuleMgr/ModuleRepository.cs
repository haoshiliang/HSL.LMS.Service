using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Entity;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Repository;

namespace LMS.Infrastructure.Data.MainBoundedContext.SystemMgr.ModuleMgr
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(MainUnitOfWork unitWork) : base(unitWork)
        {

        }
        /// <summary>
        /// 取出可访问模块树列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<DTO> GetTreeList<DTO>(string userId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("SELECT m.ID AS Id,m.PARENT_ID AS ParentId,m.CODE AS Code,m.NAME,m.ICON,m.MODULE_PATH AS ModulePath,m.IS_FUNCTION AS IsFunctionQuery");
            sqlBuilder.AppendLine("  FROM SYS_MODULE m");
            sqlBuilder.AppendLine(" LEFT JOIN SYS_ROLE_MODULE rm ON rm.MODULE_ID = m.ID");
            sqlBuilder.AppendLine(" LEFT JOIN SYS_ROLE_USER ru ON ru.ROLE_ID = rm.ROLE_ID");
            sqlBuilder.AppendLine("WHERE (ru.USER_ID = @UserId OR 1=1)");
            sqlBuilder.AppendLine("  AND m.ID<>'" + Guid.Empty.ToString() + "'");
            sqlBuilder.AppendLine("ORDER BY m.CREATE_DATE");

            return base.ExecuteQuerySql<DTO>(sqlBuilder.ToString(), new string[] { "UserId" }, new object[] { userId });         
        }
    }
}
