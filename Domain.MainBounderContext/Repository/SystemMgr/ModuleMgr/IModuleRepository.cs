using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LMS.Domain.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Repository
{
    public interface IModuleRepository : IRepository<Module>
    {
        /// <summary>
        /// 删除功能列表
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        void RemoveFunction(string moduleId);

        /// <summary>
        /// 取出可访问模块树列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        IEnumerable<DTO> GetTreeList<DTO>(string userId,bool isSuperAdmin);

        /// <summary>
        /// 取出可访问模块树列表
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <returns></returns>
        IEnumerable<DTO> GetTreeList<DTO>() where DTO : class;
    }
}
