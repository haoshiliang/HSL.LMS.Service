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
    /// 模块功能模块
    /// </summary>
    public class FunctionController : ApiBaseController
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
        public FunctionController(IModuleService moduleService)
        {
            this.moduleService = moduleService;
        }

        #endregion

        #region API

        // GET: api/Function
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <returns></returns>
        public object Get(string id)
        {
            try
            {
                var list = this.moduleService.FindFunctionList(id);
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 添加模块功能
        /// </summary>
        /// <param name="itemList">功能列表</param>
        /// <returns></returns>
        public object Post([FromBody]IList<Module> itemList)
        {
            try
            {
                this.moduleService.AddFunction(itemList);
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
