using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Entity;
using LMS.Application.MainBounderContext.DTO.SystemMgr.ModuleMgr;

namespace LMS.Application.MainBounderContext.SystemMgr.ModuleMgr
{
    public interface IModuleService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        void AddOrModity(Module model);

        /// <summary>
        /// 添加模块功能
        /// </summary>
        /// <param name="fList"></param>
        void AddFunction(IList<Module> fList);
        /// <summary>
        /// 取出职位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Module FindById(string id);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

        /// <summary>
        /// 获取模块儿树列表
        /// </summary>
        /// <returns></returns>
        ICollection<ModuleDTO> FindAllowVisitList(string userId);

        /// <summary>
        /// 获取模块儿树列表
        /// </summary>
        /// <returns></returns>
        ICollection<ModuleDTO> FindTreeList(string id);

        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="id">模块编号</param>
        /// <returns></returns>
        ICollection<Module> FindFunctionList(string id);
    }
}
