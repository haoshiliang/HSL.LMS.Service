using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVC.Client.Controllers
{
    /// <summary>
    /// API基础类
    /// </summary>
    [UserAuth]
    public class ApiBaseController : ApiController
    {
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
    }
}
