using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.Entity;
using LMS.Domain.MainBounderContext.SystemMgr.Repository;

namespace LMS.Infrastructure.Data.MainBoundedContext.SystemMgr
{
    public class CorporationRepository : Repository<Corporation>, ICorporationRepository
    {
        public CorporationRepository(MainUnitOfWork unitWork) : base(unitWork)  
        {

        }
    }
}
