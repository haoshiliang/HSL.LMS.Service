using LMS.Domain.MainBounderContext.Repository.SystemMgr.QueryMgr;
using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Entity;
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
    }
}
