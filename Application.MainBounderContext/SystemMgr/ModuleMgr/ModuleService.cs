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
        /// 添加模块功能
        /// </summary>
        /// <param name="fList"></param>
        public void AddFunction(IList<Module> fList)
        {
            try
            {
                moduleRepository.UnitOfWork.BeginTrans();

                //删除模块功能
                moduleRepository.RemoveFunction(fList.FirstOrDefault().ParentId);
                var addList = fList.Where(m => m.Id != "-1");
                //添加模块功能
                foreach (var m in addList)
                {
                    m.IsFunction = true;
                    m.GenerateNewIdentity();
                    moduleRepository.Add(m);
                }
                moduleRepository.SaveChanges();
                moduleRepository.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                moduleRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 取出模块功能信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Module FindById(string id)
        {
            var model = moduleRepository.Get(id);
            return model.ProjectedAs<Module>();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            moduleRepository.Remove(moduleRepository.Get(id));
            moduleRepository.SaveChanges();
        }

        /// <summary>
        /// 获取模块儿树列表
        /// </summary>
        /// <returns></returns>
        public ICollection<ModuleDTO> FindAllowVisitList(string userId,bool isSuperAdmin)
        {
            var mList = moduleRepository.GetTreeList<ModuleDTO>(userId,isSuperAdmin).ToList();
            var treeList = mList.Where(m => m.ParentId == Guid.Empty.ToString() && m.IsEnabled == 1).OrderBy(m => m.Code);
            foreach (var m in treeList)
            {
                m.ChildList = this.GetChildList(m.Id, mList, null, 1).ToList();
            }
            return treeList.ToList();
        }

        /// <summary>
        /// 获取模块儿树列表
        /// </summary>
        /// <returns></returns>
        public ICollection<ModuleDTO> FindTreeList(string id)
        {
            var mList = moduleRepository.GetTreeList<ModuleDTO>().ToList();
            var treeList = mList.Where(m => m.ParentId == Guid.Empty.ToString() && m.Id != id).OrderBy(m => m.Code);
            foreach (var m in treeList)
            {
                m.ChildList = this.GetChildList(m.Id, mList, id).ToList();
            }
            return treeList.ToList();
        }

        /// <summary>
        /// 获取可用模块儿树列表
        /// </summary>
        /// <returns></returns>
        public ICollection<ModuleDTO> FindEnableTreeList()
        {
            var mList = moduleRepository.GetTreeList<ModuleDTO>(false, true, true).ToList();
            var treeList = mList.Where(m => m.ParentId == Guid.Empty.ToString()).OrderBy(m => m.Code);
            foreach (var m in treeList)
            {
                m.ChildList = this.GetChildList(m.Id, mList).ToList();
            }
            return treeList.ToList();
        }

        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="id">模块编号</param>
        /// <returns></returns>
        public ICollection<Module> FindFunctionList(string id)
        {
            var funList = new List<Module>();
            var model = moduleRepository.Get(id);
            var fList = model.ChildList.Where(m => m.IsFunction).OrderBy(m => m.Id);
            foreach (var m in fList)
            {
                funList.Add(m.ProjectedAs<Module>());
            }
            return funList;
        }

        /// <summary>
        /// 获取可用的模块及功能树列表
        /// </summary>
        /// <returns></returns>
        public ICollection<ModuleDTO> FindModuleFunctionList()
        {
            var mList = moduleRepository.GetTreeList<ModuleDTO>(true).ToList();
            var treeList = mList.Where(m => m.ParentId == Guid.Empty.ToString() && m.IsEnabled==1).OrderBy(m => m.Code);
            foreach (var m in treeList)
            {
                m.ChildList = this.GetMFunctionChildList(m.Id, mList).ToList();
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
        private IEnumerable<ModuleDTO> GetChildList(string parentId, IEnumerable<ModuleDTO> mList, string id = null, int? isEnabled = null)
        {
            var treeList = new List<ModuleDTO>();
            if (isEnabled != null)
                treeList = mList.Where(m => m.ParentId == parentId && m.IsFunction == 0 && m.Id != id && m.IsEnabled == isEnabled.Value).OrderBy(m => m.Code).ToList();
            else
                treeList = mList.Where(m => m.ParentId == parentId && m.IsFunction == 0 && m.Id != id).OrderBy(m => m.Code).ToList();

            foreach (var t in treeList)
            {
                if (isEnabled != null)
                    t.FunctionList = mList.Where(m => m.ParentId == t.Id && m.IsFunction == 1 && m.IsEnabled == isEnabled.Value).ToList().ToDictionary(key => key.Code, value => value.Id);
                else
                    t.FunctionList = mList.Where(m => m.ParentId == t.Id && m.IsFunction == 1).ToList().ToDictionary(key => key.Code, value => value.Id);
                t.ChildList = this.GetChildList(t.Id, mList, id, isEnabled).ToList();
            }
            return treeList;
        }

        /// <summary>
        /// 取出子模块功能
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="mList"></param>
        /// <returns></returns>
        private IEnumerable<ModuleDTO> GetMFunctionChildList(string parentId, IEnumerable<ModuleDTO> mList)
        {
            var treeList = mList.Where(m => m.ParentId == parentId && m.IsEnabled == 1).OrderBy(m => m.Code).ToList();
            foreach (var t in treeList)
            {
                t.ChildList = this.GetMFunctionChildList(t.Id,mList).ToList();
            }
            return treeList;
        }

        #endregion
    }
}
