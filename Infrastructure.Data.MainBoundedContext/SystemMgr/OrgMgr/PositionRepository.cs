using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository;

namespace LMS.Infrastructure.Data.MainBoundedContext.SystemMgr.OrgMgr
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(MainUnitOfWork unitWork) : base(unitWork)
        {

        }
    }
}
