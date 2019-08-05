using LMS.Application.MainBounderContext.DTO.SystemMgr.QueryMgr;
using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Entity;
using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.SystemMgr.QueryMgr
{
    public interface IModuleQueryService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        void AddOrModity(ModuleQuery model);

        /// <summary>
        /// 取出部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ModuleQuery FindById(string id);

        /// <summary>
        /// 取出部门列表
        /// </summary>
        /// <returns></returns>
        ICollection<ModuleQueryDTO> FindList(Pagination pagination, QueryParam queryParam);

        /// <summary>
        /// 根据模块ID获取列表
        /// </summary>
        /// <param name="mId"></param>
        /// <returns></returns>
        ICollection<ModuleQueryDTO> FindByModuleList(string mId,string id);

        /// <summary>
        /// 取出查询条件
        /// </summary>
        /// <param name="mId">模块ID</param>
        /// <returns></returns>
        QueryParam FindQueryParam(string mId, string userId);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);
    }
}
