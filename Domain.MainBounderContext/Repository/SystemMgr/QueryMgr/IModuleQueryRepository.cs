using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Entity;
using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Repository
{
    public interface IModuleQueryRepository : IRepository<ModuleQuery>
    {
        /// <summary>
        /// 获取模块儿查询列表
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="pagination"></param>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        IList<DTO> GetPaged<DTO>(Pagination pagination, QueryParam queryParam) where DTO : class;
    }
}
