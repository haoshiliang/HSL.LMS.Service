using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity;
using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Repository
{
    public interface IDayLogRepository : IRepository<DayLog>
    {
        IList<DTO> GetPaged<DTO>(Pagination pagination, QueryParam queryParam) where DTO : class;
    }
}
