using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Application.Seedwork;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;
using LMS.Domain.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr
{
    public class RoleService : IRoleService
    {
        #region 私有变量

        /// <summary>
        /// 角色仓储
        /// </summary>
        private readonly IRoleRepository roleRepository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="roleRepository"></param>
        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        public void AddOrModity(Role model)
        {
            model.PyCode = model.RoleName.ToConvertPyCode();
            if (!string.IsNullOrEmpty(model.Id))
            {
                model.LastUpdateDate = DateTime.Now;
                roleRepository.Modity(model);
            }
            else
            {
                model.GenerateNewIdentity();
                model.CreateDate = DateTime.Now;
                model.LastUpdateDate = DateTime.Now;
                roleRepository.Add(model);
            }
            roleRepository.SaveChanges();
        }

        /// <summary>
        /// 取出角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleDTO FindById(string id)
        {
            var model = roleRepository.Get(id);
            var dto = model.ProjectedAs<RoleDTO>();
            return dto;
        }

        /// <summary>
        /// 取出角色列表
        /// </summary>
        /// <returns></returns>
        public ICollection<RoleDTO> FindList(Pagination pagination, QueryParam queryParam)
        {
            var list = roleRepository.GetPaged(pagination, queryParam).ToList();
            var dtoList= new List<RoleDTO>();
            foreach(var m in list)
            {
                dtoList.Add(m.ProjectedAs<RoleDTO>());
            }
            return dtoList;
        }

        /// <summary>
        /// 取出不分页角色列表
        /// </summary>
        /// <returns></returns>
        public ICollection<RoleDTO> FindNoPageList()
        {
            var list = roleRepository.GetAll();
            var dtoList = new List<RoleDTO>();
            foreach (var m in list)
            {
                dtoList.Add(m.ProjectedAs<RoleDTO>());
            }
            return dtoList;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            roleRepository.Remove(roleRepository.Get(id));
            roleRepository.SaveChanges();
        }

        #endregion
    }
}
