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
            sqlBulder.AppendLine("SELECT mq.ID,mq.MODULE_ID AS ModuleId,m.NAME AS ModuleName,mq.TITLE AS Title,mq.FIELD,mq.PARAM_NAME AS ParamName,mq.OPERATOR_TYPE AS OperatorType,");
            sqlBulder.AppendLine("       mq.DEFAULT_VALUE AS DefaultValue,mq.TARGET_NAME AS TargetName,mq.DATA_TYPE AS DataType,mq.CONTROL_TYPE AS ControlType,");
            sqlBulder.AppendLine("       mq.IS_DEFAULT_QUERY AS IsDefaultQuery,mq.DISPLAY_ORDER AS DisplayOrder,mq.RELATIONID_1,mq.RELATIONID_2,mq.RELATIONID_3,mq.CREATE_DATE AS CreateDate");
            sqlBulder.AppendLine("  FROM SYS_MODULE_QUERY mq,");
            sqlBulder.AppendLine("       SYS_MODULE m");
            sqlBulder.AppendLine(" WHERE mq.MODULE_ID = m.ID");
            return base.GetPagedSql<DTO>(sqlBulder.ToString(), pagination, queryParam).ToList();
        }
    }
}
