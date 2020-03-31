using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMS.Application.MainBounderContext.DTO.SystemMgr.QueryMgr;
using LMS.Application.MainBounderContext.SystemMgr.QueryMgr;
using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Entity;
using LMS.Domain.Seedwork;
using System.Data.Entity.Validation;

namespace MVC.Client.Controllers.SystemMgr.QueryMgr
{
    /// <summary>
    /// 模块查询设置
    /// </summary>
    public class ModuleQueryController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 模块查询服务
        /// </summary>
        private readonly IModuleQueryService  moduleQueryService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="moduleQueryService"></param>
        public ModuleQueryController(IModuleQueryService moduleQueryService)
        {
            this.moduleQueryService = moduleQueryService;
        }

        #endregion

        #region API

        // GET: api/ModuleQuery/List
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        [Route("api/ModuleQuery/List")]
        public object Post(QueryWhere queryWhere)
        {
            try
            {
                if (queryWhere.QueryParamModel.SortList.Count == 0)
                {
                    queryWhere.QueryParamModel.SortList.Add(new SortField() { SortValue = "DisplayOrder" });
                }
                var list = this.moduleQueryService.FindList(queryWhere.PaginationModel, queryWhere.QueryParamModel);
                return base.ToSuccessObject(new { List = list, RecordTotal = queryWhere.PaginationModel.RecordTotal });
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // GET: api/ModuleQuery/5
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(string id)
        {
            try
            {
                var model = this.moduleQueryService.FindById(id);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// api/ModuleQuery/GetByModuleId/1
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <param name="id">查询ID</param>
        /// <returns></returns>
        [Route("api/ModuleQuery/GetByModuleId")]
        public object GetByModuleId(string moduleId,string id)
        {
            try
            {
                var model = this.moduleQueryService.FindByModuleList(moduleId, id);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取每个页面的查询条件列表
        /// </summary>
        /// <param name="moduleId">模块ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [Route("api/ModuleQuery/GetSearchList")]
        public object GetSearchList(string moduleId)
        {
            try
            {
                var model = this.moduleQueryService.FindQueryParam(moduleId, base.LoginInfo.UserId);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }
        // POST: api/ModuleQuery
        /// <summary>
        /// 添加模块信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]ModuleQuery value)
        {
            try
            {
                this.moduleQueryService.AddOrModity(value);
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

        // DELETE: api/ModuleQuery/5
        /// <summary>
        /// 删除模块信息
        /// </summary>
        /// <param name="id"></param>
        public object Delete(string id)
        {
            try
            {
                this.moduleQueryService.Delete(id);
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
