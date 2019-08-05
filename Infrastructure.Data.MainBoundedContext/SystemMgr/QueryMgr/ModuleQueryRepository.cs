using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Entity;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Data.MainBoundedContext.SystemMgr.QueryMgr
{
    public class ModuleQueryRepository: Repository<ModuleQuery>, IModuleQueryRepository
    {
        public ModuleQueryRepository(MainUnitOfWork unitWork) : base(unitWork)
        {

        }

        /// <summary>
        /// 获取模块儿查询列表
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="pagination"></param>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DTO> GetPaged<DTO>(Pagination pagination, QueryParam queryParam) where DTO : class
        {
            var sqlBulder = new StringBuilder();
            sqlBulder.AppendLine("SELECT mq.ID,mq.MODULE_ID AS ModuleId,m.NAME AS ModuleName,mq.TITLE AS Title,mq.FIELD,mq.PARAM_NAME AS ParamName,mq.OPERATOR_TYPE AS Operator,");
            sqlBulder.AppendLine("       mq.DEFAULT_VALUE AS DefaultValue,md.PARAM_NAME AS TargetName,mq.DATA_TYPE AS DataType,mq.CONTROL_TYPE AS ControlType,");
            sqlBulder.AppendLine("       mq.IS_DEFAULT_QUERY AS IsDefaultQuery,mq.DISPLAY_ORDER AS DisplayOrder,mr1.PARAM_NAME AS RELATIONID_1,mr2.PARAM_NAME AS RELATIONID_2,");
            sqlBulder.AppendLine("       mr3.PARAM_NAME AS RELATIONID_3,mq.CREATE_DATE AS CreateDate");
            sqlBulder.AppendLine("  FROM SYS_MODULE_QUERY mq");
            sqlBulder.AppendLine(" INNER JOIN SYS_MODULE m ON m.ID = mq.MODULE_ID");
            sqlBulder.AppendLine("  LEFT JOIN SYS_MODULE_QUERY md ON md.ID = mq.TARGET_NAME");
            sqlBulder.AppendLine("  LEFT JOIN SYS_MODULE_QUERY mr1 ON mr1.ID = mq.RELATIONID_1");
            sqlBulder.AppendLine("  LEFT JOIN SYS_MODULE_QUERY mr2 ON mr2.ID = mq.RELATIONID_2");
            sqlBulder.AppendLine("  LEFT JOIN SYS_MODULE_QUERY mr3 ON mr3.ID = mq.RELATIONID_3");
            sqlBulder.AppendLine(" WHERE 1=1");
            sqlBulder.AppendLine("{WHERE}");
            sqlBulder.AppendLine("ORDER BY mq.DISPLAY_ORDER");
            return base.GetPagedSql<DTO>(sqlBulder.ToString(), pagination, queryParam).ToList();
        }

        /// <summary>
        /// 获取模块儿查询列表
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="pagination"></param>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DTO> GetListByModuleId<DTO>(string mId) where DTO : class
        {
            var sqlBulder = new StringBuilder();
            sqlBulder.AppendLine("SELECT mq.ID,mq.MODULE_ID AS ModuleId,m.NAME AS ModuleName,mq.TITLE AS Title,mq.FIELD,mq.PARAM_NAME AS ParamName,mq.OPERATOR_TYPE AS Operator,");
            sqlBulder.AppendLine("       mq.DEFAULT_VALUE AS DefaultValue,md.PARAM_NAME AS TargetName,mq.DATA_TYPE AS DataType,mq.CONTROL_TYPE AS ControlType,mq.EXISTS_VALUE AS ExistsValue,");
            sqlBulder.AppendLine("       mq.IS_DEFAULT_QUERY AS IsDefaultQuery,mq.DISPLAY_ORDER AS DisplayOrder,mr1.PARAM_NAME AS RELATIONID_1,mr2.PARAM_NAME AS RELATIONID_2,");
            sqlBulder.AppendLine("       mr3.PARAM_NAME AS RELATIONID_3,mq.CREATE_DATE AS CreateDate,mq.DOWN_LIST_VALUE AS DownListValue,mq.DROPDOWN_DATASOURCE AS DropdownDataSource");
            sqlBulder.AppendLine("  FROM SYS_MODULE_QUERY mq");
            sqlBulder.AppendLine(" INNER JOIN SYS_MODULE m ON m.ID = mq.MODULE_ID");
            sqlBulder.AppendLine("  LEFT JOIN SYS_MODULE_QUERY md ON md.ID = mq.TARGET_NAME");
            sqlBulder.AppendLine("  LEFT JOIN SYS_MODULE_QUERY mr1 ON mr1.ID = mq.RELATIONID_1");
            sqlBulder.AppendLine("  LEFT JOIN SYS_MODULE_QUERY mr2 ON mr2.ID = mq.RELATIONID_2");
            sqlBulder.AppendLine("  LEFT JOIN SYS_MODULE_QUERY mr3 ON mr3.ID = mq.RELATIONID_3");
            sqlBulder.AppendLine(" WHERE mq.MODULE_ID=@ModuleId");
            sqlBulder.AppendLine("ORDER BY mq.DISPLAY_ORDER");
            return base.ExecuteQuerySql<DTO>(sqlBulder.ToString(), new string[] { "ModuleId" }, new object[] { mId }, true).ToList();
        }
    }
}
