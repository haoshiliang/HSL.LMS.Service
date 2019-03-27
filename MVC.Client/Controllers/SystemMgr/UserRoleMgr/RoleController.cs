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
    /// 角色管理
    /// </summary>
    public class RoleController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 角色服务
        /// </summary>
        private readonly IRoleService roleService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        #endregion

        #region API

        // GET: api/Role
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            try
            {
                var list = this.roleService.FindList();
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // GET: api/Role/5
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            try
            {
                var model = this.roleService.FindById(id);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/Role
        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]string value)
        {
            try
            {
                this.roleService.AddOrModity(JsonConvert.DeserializeObject<Role>(value));
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
        /// 更新角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public object Put(int id, [FromBody]string value)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<Role>(value);
                this.roleService.AddOrModity(model);

                return base.ToSuccessObject();
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // DELETE: api/Role/5
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="id"></param>
        public object Delete(Guid id)
        {
            try
            {
                this.roleService.Delete(id);
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
