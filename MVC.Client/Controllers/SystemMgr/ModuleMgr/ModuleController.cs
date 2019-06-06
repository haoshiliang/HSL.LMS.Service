using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using LMS.Application.MainBounderContext.SystemMgr.ModuleMgr;
using LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Entity;
using System.Data.Entity.Validation;

namespace MVC.Client.Controllers.SystemMgr.ModuleMgr
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public class ModuleController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 模块服务
        /// </summary>
        private readonly IModuleService moduleService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="moduleService"></param>
        public ModuleController(IModuleService moduleService)
        {
            this.moduleService = moduleService;
        }

        #endregion

        #region API

        // GET: api/Module
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public object Get()
        {
            try
            {
                var list = this.moduleService.FindTreeList();
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // GET: api/Module/5
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            try
            {
                var model = this.moduleService.FindById(id);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/Module
        /// <summary>
        /// 添加模块信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]string value)
        {
            try
            {
                this.moduleService.AddOrModity(JsonConvert.DeserializeObject<Module>(value));
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

        // PUT: api/Module/5
        /// <summary>
        /// 更新模块信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public object Put(int id, [FromBody]string value)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<Module>(value);
                this.moduleService.AddOrModity(model);

                return base.ToSuccessObject();
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // DELETE: api/Module/5
        /// <summary>
        /// 删除模块信息
        /// </summary>
        /// <param name="id"></param>
        public object Delete(Guid id)
        {
            try
            {
                this.moduleService.Delete(id);
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
