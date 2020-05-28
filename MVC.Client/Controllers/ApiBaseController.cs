using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr;
using LMS.Domain.Seedwork.RepositoryInterface;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Security;
using System.Web;
using LMS.Application.Seedwork.EnumData;

namespace MVC.Client.Controllers
{
    /// <summary>
    /// API基础类
    /// </summary>
    [UserAuth]
    public class ApiBaseController : ApiController
    {
        #region 属性

        /// <summary>
        /// 登录信息
        /// </summary>
        protected LoginUserInfo LoginInfo { get; set; }

        /// <summary>
        /// 用户服务类
        /// </summary>
        private IUserService UserService
        {
            get
            {
                return (UserService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUserService));
            }
        }

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        public ApiBaseController()
        {
            var auth = HttpContext.Current.Request.Headers["Authorization"];
            if (auth != null && auth.ToString() != "")
            {
                var ticketKey = RedisPrefixEnum.Sys_UserRole_.ToString() + "Ticket_" + auth.ToString();
                if (UserService.IsExistToken(ticketKey))
                {
                    LoginInfo = (LoginUserInfo)JsonConvert.DeserializeObject<LoginUserInfo>(UserService.GetToken(ticketKey));
                }
            }
        }

        #endregion

        #region 公共方法 

        /// <summary>
        /// 返回成功数据
        /// </summary>
        /// <returns></returns>
        protected object ToSuccessObject()
        {
            return ToSuccessObject(null);
        }

        /// <summary>
        /// 返回成功数据
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        protected object ToSuccessObject(object userData)
        {
            return new { status = "1", message = "", data = userData };
        }

        /// <summary>
        /// 转到失败操作
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected object ToFailureObject(string error)
        {
            return new { status = "0", message = error };
        }

        #endregion
    }
}
