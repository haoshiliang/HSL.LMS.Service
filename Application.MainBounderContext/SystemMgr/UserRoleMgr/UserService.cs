﻿using System;
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

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        public void AddOrModity(UserDTO model)
        {
            var userModel = model.ProjectedAs<UserDTO, User>();
            userModel.PyCode = model.Name.ToConvertPyCode();
            userModel.LastUpdateDate = DateTime.Now;
            userModel.Id = model.Id;            

            if (!userRepository.IsExistLoginName(userModel.LoginName, userModel.Id))
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    var list = this.roleRepository.GetAll();
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
        public User FindById(Guid id)
        {
            var model = userRepository.Get(id);
            return model;
        }

        /// <summary>
        /// 根据用户名取出用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public UserDTO FindByName(string loginName)
        {
            var userList = userRepository.GetList(m => m.LoginName.ToUpper() == loginName.ToUpper());
            if (userList.Count() > 0)
            {
                var userModel = userList.FirstOrDefault();
                var userDto = userModel.ProjectedAs<UserDTO>();
                if (userModel.UserRoleList != null)
                {
                    foreach (var role in userModel.UserRoleList)
                    {
                        userDto.RoleList.Add(role.RoleModule.ProjectedAs<RoleDTO>());
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
        public ICollection<User> FindList()
        {
            var list = userRepository.GetAll().ToList();
            return list;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            userRepository.Remove(userRepository.Get(id));
            userRepository.SaveChanges();
        }

        #endregion
    }
}
