using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;
using System.Web.Helpers;
using System.Text;
using LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr;
using LMS.Application.Seedwork.Cache;

namespace MVC.Client
{
    /// <summary>
    /// 用户身份验证过虑器
    /// </summary>
    public class UserAuthAttribute : AuthorizeAttribute
    {
        #region 变量

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
        public UserAuthAttribute(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region 重写函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //从http的header中取出ticket
            var authorization = actionContext.Request.Headers.Authorization;
            var error = "";//错误信息
            if (authorization != null && authorization.Scheme != null)
            {
                if (this.ValidateUser(authorization.Scheme, ref error))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext, error);
                }
            }
            else
            {
                //判断是否可匿名访问
                var anonymousAttrList = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                if (anonymousAttrList.Count() > 0)
                {
                    base.OnAuthorization(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext, "身份验证出错！");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext);
        }

        /// <summary>
        /// 验证失败时
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="error">错误信息</param>
        protected void HandleUnauthorizedRequest(HttpActionContext actionContext,string error)
        {
            base.HandleUnauthorizedRequest(actionContext);
            var response = actionContext.Response = actionContext.Response ?? new HttpResponseMessage();
            // 这句话可以改变请求状态值 就是 200 403 之类的那个状态值
            response.StatusCode = HttpStatusCode.OK;
            // 将这个出错信息加入到返回对象中.
            response.Content = new StringContent(Json.Encode(new { status = "0", message = error }), Encoding.UTF8, "application/json");
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="ticket">验证ticket</param>
        /// <param name="error">返回信息</param>
        /// <returns></returns>
        private bool ValidateUser(string ticket, ref string error)
        {
            try
            {
                var returnValue = true;
                var faTicket = FormsAuthentication.Decrypt(ticket);
                var ticketKey = "Sys_Ticket_" + faTicket.Name;
                if (userService.IsExistToken(ticketKey))
                {
                    if(userService.GetToken(ticketKey)!= ticket)
                    {
                        error = "用户登录验证失败！";
                        returnValue = false;
                    }
                }
                else
                {
                    error = "用户登录过期！";
                    returnValue = false;
                }
                return returnValue;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        #endregion
    }
}