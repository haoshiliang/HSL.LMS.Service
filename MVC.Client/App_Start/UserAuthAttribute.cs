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

namespace MVC.Client
{
    /// <summary>
    /// 用户身份验证过虑器
    /// </summary>
    public class UserAuthAttribute : AuthorizeAttribute
    {
        #region 变量

        /// <summary>
        /// 根据属性注入用户服务
        /// </summary>
        public IUserService LoginService { get; set; }

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
            if (authorization != null && authorization.Parameter != null)
            {
                if (this.ValidateUser(authorization.Parameter,ref error))
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
                if (!faTicket.Expired)
                {
                    var userName = faTicket.Name;
                    var password = faTicket.UserData;
                    var userDto = this.LoginService.FindByName(userName);
                    if (userDto != null)
                    {
                        var sha1 = System.Security.Cryptography.SHA1.Create();
                        if (userDto.Password != BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(password + userDto.SaltValue))).Replace("-", null))
                        {
                            error = "密码不正确！";
                            returnValue = false;
                        }
                    }
                    else
                    {
                        error = "用户名不存在！";
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