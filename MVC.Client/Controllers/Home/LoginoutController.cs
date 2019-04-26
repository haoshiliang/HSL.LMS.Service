using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;
using LMS.Application.MainBounderContext.SystemMgr.ModuleMgr;
using LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr;
using LMS.Application.Seedwork.EnumData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVC.Client.Controllers.Home
{
    /// <summary>
    /// 退出登录
    /// </summary>
    public class LoginoutController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserService userService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService">用户服务</param>
        public LoginoutController(IUserService userService)
        {
            try
            {
                this.userService = userService;
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
            }
        }

        #endregion

        #region API

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="loginUser">登录名</param>
        // POST api/<controller>
        public object Post([FromBody]UserDTO loginUser)
        {
            try
            {
                var ticket = RedisPrefixEnum.Sys_UserRole_.ToString() + "Ticket_" + loginUser.LoginName;
                if (userService.IsExistToken(ticket))
                {
                    userService.RemoveToken(ticket);
                }
                return base.ToSuccessObject();
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }
        #endregion
    }
}