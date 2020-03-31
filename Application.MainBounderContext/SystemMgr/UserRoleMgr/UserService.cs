using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Application.Seedwork;
using LMS.Application.Seedwork.Cache;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Domain.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr
{
    public class UserService : IUserService
    {
        #region 私有变量

        /// <summary>
        /// 用户仓储
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// 角色仓储
        /// </summary>
        private readonly IRoleRepository roleRepository;
        /// <summary>
        /// 缓存服务
        /// </summary>
        private readonly ICache cacheService;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, ICache cacheService)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.cacheService = cacheService;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        public void AddOrModity(User userModel)
        {
            userModel.PyCode = userModel.Name.ToConvertPyCode();
            userModel.LastUpdateDate = DateTime.Now;
            userModel.CorporationModel = null;
            userModel.DepartmentModel = null;
            userModel.PositionModel = null;
            if (!userRepository.IsExistLoginName(userModel.LoginName, userModel.Id))
            {
                if (!string.IsNullOrEmpty(userModel.Id))
                {
                    userRepository.Modity(userModel);
                }
                else
                {
                    userModel.GenerateNewIdentity();
                    userModel.CreateDate = DateTime.Now;
                    userRepository.Add(userModel);
                }
                userRepository.SaveChanges();
            }
            else
            {
                throw new Exception("已存在该用户名!");
            }
        }

        /// <summary>
        /// 取出用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User FindById(string id)
        {
            var model = userRepository.Get(id);
            var returnModel = model.ProjectedAs<User>();
            returnModel.OldPassword = model.Password;
            returnModel.CorporationModel = new Corporation() { Id = model.CorporationModel.Id, CorpName = model.CorporationModel.CorpName };
            returnModel.DepartmentModel = new Department() { Id = model.DepartmentModel.Id, DepartName = model.DepartmentModel.DepartName };
            returnModel.PositionModel = new Position() { Id = model.PositionModel.Id, PositionName = model.PositionModel.PositionName };
            return returnModel;
        }

        /// <summary>
        /// 根据用户名取出用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public UserDTO FindByName(string loginName)
        {
            var userList = userRepository.GetList(m => m.LoginName.ToUpper() == loginName.ToUpper() && m.IsEnable==true);
            if (userList.Count() > 0)
            {
                var userModel = userList.FirstOrDefault();
                var userDto = userModel.ProjectedAs<UserDTO>();
                if (userModel.UserRoleList != null)
                {
                    foreach (var role in userModel.UserRoleList)
                    {
                        userDto.RoleList.Add(role.RoleModel.ProjectedAs<RoleDTO>());
                    }
                }
                return userDto;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取出用户列表
        /// </summary>
        /// <returns></returns>
        public ICollection<UserDTO> FindList(Pagination pagination, QueryParam queryParam)
        {
            return this.userRepository.GetList<UserDTO>(pagination, queryParam);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            userRepository.Remove(userRepository.Get(id));
            userRepository.SaveChanges();
        }
        /// <summary>
        /// 设置Token
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="token">token值</param>
        public void SetToken(string key, string token, int cacheTime)
        {
            this.RemoveToken(key);
            this.cacheService.Insert(key, token, cacheTime);
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetToken(string key)
        {
            return this.cacheService.Get(key);
        }
        /// <summary>
        /// 移除Token
        /// </summary>
        /// <param name="key"></param>
        public void RemoveToken(string key)
        {
            if (this.cacheService.Exists(key))
            {
                this.cacheService.Remove(key);
            }
        }
        /// <summary>
        /// 是否存在Token
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExistToken(string key)
        {
            return this.cacheService.Exists(key);
        }

        /// <summary>
        /// 设置Token有效期
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool SetExpire(string key, int cacheTime)
        {
            return this.cacheService.SetExpire(key, cacheTime);
        }
        #endregion
    }
}
