using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;
using LMS.Domain.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr
{
    public interface IRoleService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        void AddOrModity(Role model);

        /// <summary>
        /// 取出职位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        RoleDTO FindById(string id);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

        /// <summary>
        /// 取出不分页角色列表
        /// </summary>
        /// <returns></returns>
        ICollection<RoleDTO> FindNoPageList();

        /// <summary>
        /// 取出角色列表
        /// </summary>
        /// <returns></returns>
        ICollection<RoleDTO> FindList(Pagination pagination, QueryParam queryParam);
    }
}
