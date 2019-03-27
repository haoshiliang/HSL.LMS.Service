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
    public class UserRoleService : IUserRoleService
    {
        #region 私有变量

        /// <summary>
        /// 用户角色仓储
        /// </summary>
        private readonly IUserRoleRepository userRoleRepository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userRoleRepository"></param>
        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public void Add(IList<UserRole> userRoleList,string userId,string roleId)
        {
            try
            {
                userRoleRepository.UnitOfWork.BeginTrans();
                userRoleRepository.BatchRemore(userId, roleId);
                foreach (var m in userRoleList)
                {
                    m.Id = Guid.NewGuid().ToString();
                    userRoleRepository.Add(m);
                }
                userRoleRepository.SaveChanges();
                userRoleRepository.UnitOfWork.Commit();
            }
            catch(Exception ex)
            {
                userRoleRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }
        
        #endregion
    }
}
