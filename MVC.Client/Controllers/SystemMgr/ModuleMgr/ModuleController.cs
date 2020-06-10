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
using LMS.Domain.Seedwork.RepositoryInterface;

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

        // GET: api/ModuleFunction
        /// <summary>
        /// 获取模块功能列表
        /// </summary>
        /// <returns></returns>
        [Route("api/ModuleFunction")]
        public object GetModuleFunction()
        {
            try
            {
                var list = this.moduleService.FindModuleFunctionList();
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [Route("api/Module/TreeList")]
        public object GetTreeList(string id)
        {
            try
            {                
                var list = this.moduleService.FindTreeList(id);
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取可用模块列表
        /// </summary>
        /// <returns></returns>
        [Route("api/Module/EnableTreeList")]
        public object GetEnableTreeList()
        {
            try
            {
                var list = this.moduleService.FindEnableTreeList();
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
        public object Get(string id)
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
        /// <param name="item"></param>
        public object Post([FromBody]Module item)
        {
            try
            {
                this.moduleService.AddOrModity(item);
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

        // DELETE: api/Module/5
        /// <summary>
        /// 删除模块信息
        /// </summary>
        /// <param name="id"></param>
        public object Delete(string id)
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
