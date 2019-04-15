using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr;
using System.Text;
using LMS.Application.MainBounderContext.DTO.SystemMgr.ModuleMgr;
using LMS.Application.MainBounderContext.SystemMgr.ModuleMgr;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;
using LMS.Application.Seedwork.Cache;

namespace MVC.Client.Controllers.Home
{
    /// <summary>
    /// 系统登录
    /// </summary>
    public class LoginController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 用户服务
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// 模块服务
        /// </summary>
        private readonly IModuleService moduleService;

        /// <summary>
        /// 缓存服务
        /// </summary>
        private readonly ICache cacheService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService">用户服务</param>
        /// <param name="moduleService">模块服务</param>
        /// <param name="cacheService">缓存服务</param>
        public LoginController(IUserService userService, IModuleService moduleService)
        {
            try
            {
                this.userService = userService;
                this.moduleService = moduleService;
                this.cacheService = new Redis();
            }
            catch (Exception ex)
            {
                var aa = ex.Message;
            }
        }

        #endregion

        #region API

        // POST: api/Login
        /// <summary>
        /// 验证用户，并返回ticket
        /// </summary>
        /// <param name="loginUser">用户名密码信息</param>
        [AllowAnonymous]
        public object Post([FromBody]UserDTO loginUser)
        {
            try
            {
                var userDto = this.userService.FindByName(loginUser.LoginName);
                if (userDto != null)
                {
                    var sha1 = System.Security.Cryptography.SHA1.Create();
                    var ticket = new FormsAuthenticationTicket(0, loginUser.LoginName, DateTime.Now, DateTime.Now.AddDays(1), false, Guid.NewGuid().ToString());
                    if (userDto.Password == BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(loginUser.Password + userDto.SaltValue))).Replace("-", null))
                    {
                        var userModel = new
                        {
                            UserInfo = userDto,
                            Ticket = FormsAuthentication.Encrypt(ticket),
                            SysRoleVoList = this.moduleService.FindTreeList(userDto.Id)
                        };
                        if (cacheService.Exists("Sys_Ticket_" + loginUser.LoginName))
                        {
                            cacheService.Remove("Sys_Ticket_" + loginUser.LoginName);
                        }
                        cacheService.Insert("Sys_Ticket_" + loginUser.LoginName, userModel.Ticket);
                        return base.ToSuccessObject(userModel);
                    }
                    else
                    {
                        return base.ToFailureObject("密码不正确！");
                    }
                }
                else
                {
                    return base.ToFailureObject("用户名不存在！");
                }
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        #endregion
    }
}
