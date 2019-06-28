using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using System.Data.Entity.Validation;

namespace MVC.Client.Controllers.SystemMgr.UserRoleMgr
{
    /// <summary>
    /// 用户角色管理
    /// </summary>
    public class UserRoleController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 用户角色服务
        /// </summary>
        private readonly IUserRoleService userRoleService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userRoleService"></param>
        public UserRoleController(IUserRoleService userRoleService)
        {
            this.userRoleService = userRoleService;
        }

        #endregion

        #region API

        // GET: api/UserRole/GetByUserId
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <returns></returns>
        [Route("api/UserRole/GetByUserId")]
        public object GetByUserId(string userId)
        {
            try
            {
                var list = this.userRoleService.FindByUserId(userId);
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // GET: api/UserRole/GetByRoleId
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <returns></returns>
        [Route("api/UserRole/GetByRoleId")]
        public object GetByRoleId(string roleId)
        {
            try
            {
                var list = new List<string>();
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/UserRole
        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]IList<UserRole> value)
        {
            try
            {
                if (!value.FirstOrDefault().IsRoleMaster)
                {
                    this.userRoleService.Add(value, value.FirstOrDefault().UserId, null);
                }
                else
                {
                    this.userRoleService.Add(value, null, value.FirstOrDefault().RoleId);
                }

                return base.ToSuccessObject();
            }
            catch (DbEntityValidationException dbEx)
            {
                return base.ToFailureObject(dbEx.Message);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        #endregion
    }
}
