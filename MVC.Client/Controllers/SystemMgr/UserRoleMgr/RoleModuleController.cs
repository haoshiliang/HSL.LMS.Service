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
    /// 角色模块管理
    /// </summary>
    public class RoleModuleController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 角色模块服务
        /// </summary>
        private readonly IRoleModuleService roleModuleService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleModuleService"></param>
        public RoleModuleController(IRoleModuleService roleModuleService)
        {
            this.roleModuleService = roleModuleService;
        }

        #endregion

        #region API

        // GET: api/Role
        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <returns></returns>
        public object Get()
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

        // POST: api/Role
        /// <summary>
        /// 添加角色模块信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]string value)
        {
            try
            {
                var list = JsonConvert.DeserializeObject<List<RoleModule>>(value);
                this.roleModuleService.Add(list, list.FirstOrDefault().RoleId);

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
