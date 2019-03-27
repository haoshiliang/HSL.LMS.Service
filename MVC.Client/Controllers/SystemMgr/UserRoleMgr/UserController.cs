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

        // GET: api/Role
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            try
            {
                var list = this.userService.FindList();
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // GET: api/Role/5
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(Guid id)
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

        // POST: api/Role
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]string value)
        {
            try
            {
                var m = JsonConvert.DeserializeObject<UserDTO>(value);
                var sha1 = System.Security.Cryptography.SHA1.Create();

                m.SaltValue = Guid.NewGuid().ToString();
                m.Password = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(m.Password + m.SaltValue))).Replace("-", null); 
                this.userService.AddOrModity(m);
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

        // PUT: api/Role/5
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public object Put(Guid id, [FromBody]string value)
        {
            try
            {
                var m = JsonConvert.DeserializeObject<UserDTO>(value);
                if (m.IsModityPassword)
                {
                    var sha1 = System.Security.Cryptography.SHA1.Create();
                    m.SaltValue = Guid.NewGuid().ToString();
                    m.Password = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(m.Password + m.SaltValue))).Replace("-", null);
                }
                this.userService.AddOrModity(m);

                return base.ToSuccessObject();
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
        public object Delete(Guid id)
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
