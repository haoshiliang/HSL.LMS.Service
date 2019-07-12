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
        /// 删除功能列表
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        public void RemoveFunction(string moduleId)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("DELETE FROM SYS_MODULE m");
            sqlBuilder.AppendLine(" WHERE m.IS_FUNCTION = 1 ");
            sqlBuilder.AppendLine("   AND m.PARENT_ID = @ParentId");
            base.ExecuteSql(sqlBuilder.ToString(), new string[] { "ParentId" }, new object[] { moduleId });
        }
        /// <summary>
        /// 取出可访问模块树列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<DTO> GetTreeList<DTO>(string userId, bool isSuperAdmin)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            StringBuilder tmpBuilder = new StringBuilder();
            if (isSuperAdmin)
            {
                sqlBuilder.AppendLine("SELECT DISTINCT m.ID AS Id,m.PARENT_ID AS ParentId,m.CODE AS Code,m.NAME,m.ICON,m.MODULE_PATH AS ModulePath,m.IS_FUNCTION AS IsFunction,m.IS_ENABLED AS IsEnabled");
                sqlBuilder.AppendLine("  FROM SYS_MODULE m");
                sqlBuilder.AppendLine("WHERE m.ID<>'" + Guid.Empty.ToString() + "'");
                sqlBuilder.AppendLine("  AND m.IS_ENABLED = '1'");
                sqlBuilder.AppendLine("ORDER BY m.CREATE_DATE");
            }
            else
            {
                tmpBuilder.AppendLine("SELECT m.ID AS Id,m.PARENT_ID AS ParentId,m.CODE AS Code,m.NAME,m.ICON,m.MODULE_PATH AS ModulePath,m.IS_FUNCTION AS IsFunction,m.IS_ENABLED AS IsEnabled");
                tmpBuilder.AppendLine("  FROM SYS_MODULE m");
                tmpBuilder.AppendLine(" INNER JOIN SYS_ROLE_MODULE rm ON rm.MODULE_ID = m.ID");
                tmpBuilder.AppendLine(" INNER JOIN SYS_ROLE_USER ru ON ru.ROLE_ID = rm.ROLE_ID");
                tmpBuilder.AppendLine(" INNER JOIN SYS_USER u ON u.ID = ru.USER_ID");
                tmpBuilder.AppendLine("WHERE ru.USER_ID = @UserId");
                tmpBuilder.AppendLine("  AND m.ID<>'" + Guid.Empty.ToString() + "'");
                tmpBuilder.AppendLine("  AND m.IS_ENABLED = '1'");
                tmpBuilder.AppendLine("ORDER BY m.CREATE_DATE");
                //取出父模块与子模块
                sqlBuilder.AppendLine("WITH MODULE_DT(Id,ParentId,Code,Name,Icon,ModulePath,IsFunction,IsEnabled)");
                sqlBuilder.AppendLine("  AS (");
                sqlBuilder.AppendLine("        SELECT m.Id,m.ParentId,m.Code,m.Name,m.Icon,m.ModulePath,m.IsFunction,m.IsEnabled");
                sqlBuilder.AppendLine("          FROM (" + tmpBuilder.ToString() + ") m");
                sqlBuilder.AppendLine("         UNION ALL");
                sqlBuilder.AppendLine("        SELECT m.ID AS Id,m.PARENT_ID AS ParentId,m.CODE AS Code,m.NAME,m.ICON,m.MODULE_PATH AS ModulePath,m.IS_FUNCTION AS IsFunction,m.IS_ENABLED AS IsEnabled");
                sqlBuilder.AppendLine("          FROM SYS_MODULE m");
                sqlBuilder.AppendLine("         INNER JOIN MODULE_DT md ON m.PARENT_ID=md.ID ");
                sqlBuilder.AppendLine("     ),");
                sqlBuilder.AppendLine("     PARENT_MODULE_DT(Id,ParentId,Code,Name,Icon,ModulePath,IsFunction,IsEnabled)");
                sqlBuilder.AppendLine("  AS (");
                sqlBuilder.AppendLine("        SELECT mp.ID AS Id,mp.PARENT_ID AS ParentId,mp.CODE AS Code,mp.NAME,mp.ICON,mp.MODULE_PATH AS ModulePath,mp.IS_FUNCTION AS IsFunction,mp.IS_ENABLED AS IsEnabled");
                sqlBuilder.AppendLine("          FROM (" + tmpBuilder.ToString() + ") m");
                sqlBuilder.AppendLine("         INNER JOIN SYS_MODULE mp ON mp.ID = m.ParentId");
                sqlBuilder.AppendLine("         UNION ALL");
                sqlBuilder.AppendLine("        SELECT m.ID AS Id,m.PARENT_ID AS ParentId,m.CODE AS Code,m.NAME,m.ICON,m.MODULE_PATH AS ModulePath,m.IS_FUNCTION AS IsFunction,m.IS_ENABLED AS IsEnabled");
                sqlBuilder.AppendLine("          FROM SYS_MODULE m");
                sqlBuilder.AppendLine("         INNER JOIN PARENT_MODULE_DT mp ON mp.ParentId=m.ID ");
                sqlBuilder.AppendLine("     )");
                sqlBuilder.AppendLine("SELECT Id,ParentId,Code,Name,Icon,ModulePath,IsFunction,IsEnabled");
                sqlBuilder.AppendLine("  FROM MODULE_DT");
                sqlBuilder.AppendLine(" UNION");
                sqlBuilder.AppendLine("SELECT Id,ParentId,Code,Name,Icon,ModulePath,IsFunction,IsEnabled");
                sqlBuilder.AppendLine("  FROM PARENT_MODULE_DT");
            }

            return base.ExecuteQuerySql<DTO>(sqlBuilder.ToString(), new string[] { "UserId" }, new object[] { userId });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="isShowFunction">是否显示功能</param>
        /// <returns></returns>
        public IEnumerable<DTO> GetTreeList<DTO>(bool isShowFunction = false) where DTO : class
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
            if (!isShowFunction)
            {
                sqlBuilder.AppendLine("        WHERE m.IS_FUNCTION = '0'");
            }
            sqlBuilder.AppendLine("     )");
            sqlBuilder.AppendLine("SELECT ID,PARENT_ID AS ParentId,PARENT_NAME AS ParentName,CODE,NAME,ICON,MODULE_PATH AS ModulePath,IS_ENABLED AS IsEnabled,MODULE_LEVEL AS ModuleLevel");
            sqlBuilder.AppendLine("  FROM MODULE_DT");

            return base.ExecuteQuerySql<DTO>(sqlBuilder.ToString(), null, null);
        }
    }
}
