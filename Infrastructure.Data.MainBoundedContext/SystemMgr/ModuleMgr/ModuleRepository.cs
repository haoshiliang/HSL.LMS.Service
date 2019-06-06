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
            sqlBuilder.AppendLine("SELECT m.ID AS Id,m.PARENT_ID AS ParentId,m.CODE AS Code,m.NAME,m.ICON,m.MODULE_PATH AS ModulePath,m.IS_FUNCTION AS IsFunction");
            sqlBuilder.AppendLine("  FROM SYS_MODULE m");
            sqlBuilder.AppendLine(" LEFT JOIN SYS_ROLE_MODULE rm ON rm.MODULE_ID = m.ID");
            sqlBuilder.AppendLine(" LEFT JOIN SYS_ROLE_USER ru ON ru.ROLE_ID = rm.ROLE_ID");
            sqlBuilder.AppendLine("WHERE (ru.USER_ID = @UserId OR 1=1)");
            sqlBuilder.AppendLine("  AND m.ID<>'" + Guid.Empty.ToString() + "'");
            sqlBuilder.AppendLine("ORDER BY m.CREATE_DATE");

            return base.ExecuteQuerySql<DTO>(sqlBuilder.ToString(), new string[] { "UserId" }, new object[] { userId });         
        }

        public IEnumerable<DTO> GetTreeList<DTO>() where DTO : class
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("WITH MODULE_DT(ID,PARENT_ID,PARENT_NAME,CODE,NAME,ICON,MODULE_PATH,IS_ENABLED,MODULE_LEVEL)");
            sqlBuilder.AppendLine("  AS (");
            sqlBuilder.AppendLine("       SELECT m.ID,m.PARENT_ID,'根模块' AS PARENT_NAME,m.CODE,m.NAME,m.ICON,m.MODULE_PATH,m.IS_ENABLED,1 AS MODULE_LEVEL");
            sqlBuilder.AppendLine("         FROM SYS_MODULE m");
            sqlBuilder.AppendLine("        WHERE m.PARENT_ID = '00000000-0000-0000-0000-000000000000'");
            sqlBuilder.AppendLine("        UNION ALL");
            sqlBuilder.AppendLine("       SELECT m.ID,m.PARENT_ID,TO_CHAR(md.NAME),m.CODE,m.NAME,m.ICON,m.MODULE_PATH,m.IS_ENABLED,md.MODULE_LEVEL+1 AS MODULE_LEVEL");
            sqlBuilder.AppendLine("         FROM SYS_MODULE m");
            sqlBuilder.AppendLine("        INNER JOIN MODULE_DT md ON m.PARENT_ID = md.ID");
            sqlBuilder.AppendLine("        WHERE m.IS_FUNCTION = '0'");
            sqlBuilder.AppendLine("     )");
            sqlBuilder.AppendLine("SELECT ID,PARENT_ID AS ParentId,PARENT_NAME AS ParentName,CODE,NAME,ICON,MODULE_PATH AS ModulePath,IS_ENABLED AS IsEnabled,MODULE_LEVEL AS ModuleLevel");
            sqlBuilder.AppendLine("  FROM MODULE_DT");

            return base.ExecuteQuerySql<DTO>(sqlBuilder.ToString(), null, null);
        }
    }
}
