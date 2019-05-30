using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Entity;
using LMS.Application.MainBounderContext.DTO.SystemMgr.ModuleMgr;
using LMS.Application.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.ModuleMgr
{
    public class ModuleService : IModuleService
    {
        #region 私有变量

        /// <summary>
        /// 模块功能仓储
        /// </summary>
        private readonly IModuleRepository moduleRepository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="moduleRepository"></param>
        public ModuleService(IModuleRepository moduleRepository)
        {
            this.moduleRepository = moduleRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        public void AddOrModity(Module model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                model.LastUpdateDate = DateTime.Now;
                moduleRepository.Modity(model);
            }
            else
            {
                model.ParentId = string.IsNullOrEmpty(model.ParentId) ? Guid.Empty.ToString() : model.ParentId;
                model.GenerateNewIdentity();
                model.CreateDate = DateTime.Now;
                model.LastUpdateDate = DateTime.Now;
                moduleRepository.Add(model);
            }
            moduleRepository.SaveChanges();
        }

        /// <summary>
        /// 取出模块功能信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ModuleDTO FindById(Guid id)
        {
            var model = moduleRepository.Get(id);
            var dto = model.ProjectedAs<ModuleDTO>();
            dto.ParentName = model.ParentModule.Name;

            return dto;
        }

        /// <summary>
        /// 取出模块功能列表
        /// </summary>
        /// <returns></returns>
        public ICollection<ModuleDTO> FindList()
        {
            var list = moduleRepository.GetAll().ToList();
            var dtoList = new List<ModuleDTO>();
            foreach(var m in list)
            {
                dtoList.Add(m.ProjectedAs<ModuleDTO>());
            }
            return dtoList;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            moduleRepository.Remove(moduleRepository.Get(id));
            moduleRepository.SaveChanges();
        }

        /// <summary>
        /// 获取模块儿树列表
        /// </summary>
        /// <returns></returns>
        public ICollection<ModuleDTO> FindTreeList(string userId)
        {
            var mList = moduleRepository.GetTreeList<ModuleDTO>(userId).ToList();
            var treeList = mList.Where(m => m.ParentId == Guid.Empty.ToString()).OrderBy(m=>m.Code);
            foreach(var m in treeList)
            {
                m.ChildList = this.GetChildList(m.Id, mList).ToList();
            }
            return treeList.ToList();
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 取出模块
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="mList"></param>
        /// <returns></returns>
        private IEnumerable<ModuleDTO> GetChildList(string parentId, IEnumerable<ModuleDTO> mList)
        {
            var treeList = mList.Where(m => m.ParentId == parentId && m.IsFunctionQuery == 0).OrderBy(m => m.Code);
            foreach (var t in treeList)
            {
                t.FunctionList = mList.Where(m => m.ParentId == t.Id && m.IsFunctionQuery == 1).ToList().ToDictionary(key => key.Code, value => value.Id);
                t.ChildList = this.GetChildList(t.Id, mList).ToList();
            }
            return treeList;
        }

        #endregion
    }
}
