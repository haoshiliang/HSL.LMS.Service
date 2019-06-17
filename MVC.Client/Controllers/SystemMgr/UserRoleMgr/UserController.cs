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
using System.Text;
using LMS.Domain.Seedwork;

namespace MVC.Client.Controllers.SystemMgr.UserRoleMgr
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserController : ApiBaseController
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
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        #endregion

        #region API

        // GET: api/Role/5
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(string id)
        {
            try
            {
                var model = this.userService.FindById(id);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="queryWhere">分页数据及查询条件</param>
        /// <returns></returns>
        [Route("api/User/List")]
        public object Post(QueryWhere queryWhere)
        {
            try
            {
                if (queryWhere.QueryParamModel.SortList.Count == 0)
                {
                    queryWhere.QueryParamModel.SortList.Add(new SortField() { SortValue = "CODE" });
                }
                var list = this.userService.FindList(queryWhere.PaginationModel, queryWhere.QueryParamModel);
                return base.ToSuccessObject(new { List = list, RecordTotal = queryWhere.PaginationModel.RecordTotal });
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/User
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]User value)
        {
            try
            {
                if (value.Id == "" || value.OldPassword != value.Password)
                {
                    var sha1 = System.Security.Cryptography.SHA1.Create();
                    value.SaltValue = Guid.NewGuid().ToString();
                    value.Password = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(value.Password + value.SaltValue))).Replace("-", null);
                }
                this.userService.AddOrModity(value);
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

        // DELETE: api/Role/5
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        public object Delete(string id)
        {
            try
            {
                this.userService.Delete(id);
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
