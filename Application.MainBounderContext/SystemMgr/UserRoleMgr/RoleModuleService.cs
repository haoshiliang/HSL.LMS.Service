using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Application.Seedwork;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;

namespace LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr
{
    public class RoleModuleService : IRoleModuleService
    {
        #region 私有变量

        /// <summary>
        /// 角色模块仓储
        /// </summary>
        private readonly IRoleModuleRepository roleModuleRepository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="roleRepository"></param>
        public RoleModuleService(IRoleModuleRepository roleModuleRepository)
        {
            this.roleModuleRepository = roleModuleRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="modelList">模块列表</param>
        public void Add(IList<RoleModule> modelList,string roleId)
        {
            try
            {
                this.roleModuleRepository.UnitOfWork.BeginTrans();

                this.roleModuleRepository.BatchRemore(roleId);
                foreach (var m in modelList)
                {
                    m.Id = Guid.NewGuid().ToString();
                    this.roleModuleRepository.Add(m);
                }
                this.roleModuleRepository.SaveChanges();

                this.roleModuleRepository.UnitOfWork.Commit();
            }
            catch
            {
                this.roleModuleRepository.UnitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        public IList<string> FindList(string roleId)
        {
            return this.roleModuleRepository.GetList(m=>m.RoleId == roleId).Select(m=>m.ModuleId).ToList();
        }
        
        #endregion
    }
}
