using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Repository;

namespace LMS.Infrastructure.Data.MainBoundedContext.SystemMgr.UserRoleMgr
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(MainUnitOfWork unitWork) : base(unitWork)
        {

        }
    }
}
