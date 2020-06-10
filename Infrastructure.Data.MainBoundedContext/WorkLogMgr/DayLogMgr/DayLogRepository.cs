using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity;
using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Repository;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Data.MainBoundedContext.WorkLogMgr.DayLogMgr
{
    public class DayLogRepository : Repository<DayLog>, IDayLogRepository
    {
        public DayLogRepository(MainUnitOfWork unitWork) : base(unitWork)
        {

        }

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="pagination"></param>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DTO> GetPaged<DTO>(Pagination pagination, QueryParam queryParam) where DTO : class
        {
            var sqlBulder = new StringBuilder();
            sqlBulder.AppendLine("SELECT wdl.ID,wdl.CREATE_USER AS CreateUser,wdl.CREATE_DATE AS CreateDate,wdl.LOG_DATE AS LogDate,wdl.LOG_TITLE AS LogTitle");
            sqlBulder.AppendLine("  FROM WL_DAY_LOG wdl");
            sqlBulder.AppendLine(" WHERE 1=1");
            sqlBulder.AppendLine("{WHERE}");
            sqlBulder.AppendLine("ORDER BY wdl.LogDate");
            return base.GetPagedSql<DTO>(sqlBulder.ToString(), pagination, queryParam).ToList();
        }
    }
}
