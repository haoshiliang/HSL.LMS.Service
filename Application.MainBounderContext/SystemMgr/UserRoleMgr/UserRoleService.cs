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
        /// 添加角色用户
        /// </summary>
        /// <param name="userRoleList"></param>
        public void AddRoleUser(IList<UserRole> userRoleList)
        {
            try
            {
                userRoleRepository.UnitOfWork.BeginTrans();
                foreach (var m in userRoleList)
                {
                    m.Id = Guid.NewGuid().ToString();
                    userRoleRepository.Add(m);
                }
                userRoleRepository.SaveChanges();
                userRoleRepository.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                userRoleRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 删除角色用户
        /// </summary>
        /// <param name="userRoleList"></param>
        public void DelRoleUser(IList<UserRole> userRoleList)
        {
            try
            {
                userRoleRepository.UnitOfWork.BeginTrans();
                foreach (var m in userRoleList)
                {
                    userRoleRepository.BatchRemore(m.UserId, m.RoleId);
                }
                userRoleRepository.SaveChanges();
                userRoleRepository.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                userRoleRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public void Add(IList<UserRole> userRoleList,string userId,string roleId)
        {
            try
            {
                userRoleRepository.UnitOfWork.BeginTrans();

                var addList = userRoleList.Where(m => m.RoleId != "" && m.UserId != "");
                userRoleRepository.BatchRemore(userId, roleId);
                foreach (var m in addList)
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

        /// <summary>
        /// 根据用户ID取出角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<string> FindByUserId(string userId)
        {
            return this.userRoleRepository.GetAll().Where(m => m.UserId == userId).Select(m => m.RoleId).ToList();
        }
        
        #endregion
    }
}
