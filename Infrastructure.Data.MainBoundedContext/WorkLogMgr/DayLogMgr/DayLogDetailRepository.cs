using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity;
using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Repository;
using LMS.Infrastructure.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Data.MainBoundedContext.WorkLogMgr.DayLogMgr
{
    public class DayLogDetailRepository : Repository<DayLogDetail>, IDayLogDetailRepository
    {
        public DayLogDetailRepository(MainUnitOfWork unitWork) : base(unitWork)
        {

        }
    }
}
