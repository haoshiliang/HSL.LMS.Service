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
        /// 取出职位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ModuleDTO FindById(Guid id);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);

        /// <summary>
        /// 取出模块功能列表
        /// </summary>
        /// <returns></returns>
        ICollection<ModuleDTO> FindList();

        /// <summary>
        /// 获取模块儿树列表
        /// </summary>
        /// <returns></returns>
        ICollection<ModuleDTO> FindTreeList(string userId);

        /// <summary>
        /// 获取模块儿树列表
        /// </summary>
        /// <returns></returns>
        ICollection<ModuleDTO> FindTreeList();
    }
}
