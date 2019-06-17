using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using LMS.Application.MainBounderContext.SystemMgr.OrgMgr;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using System.Data.Entity.Validation;
using LMS.Domain.Seedwork;

namespace MVC.Client.Controllers.SystemMgr.OrgMgr
{
    /// <summary>
    /// 职位管理
    /// </summary>
    public class PositionController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 职位服务
        /// </summary>
        private readonly IPositionService positionService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="positionService"></param>
        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }

        #endregion

        #region API

        // GET: api/Position
        /// <summary>
        /// 获取职位列表
        /// </summary>
        /// <returns></returns>
        [Route("api/Position/List")]
        public object Post(QueryWhere queryWhere)
        {
            try
            {
                if (queryWhere.QueryParamModel.SortList.Count == 0)
                {
                    queryWhere.QueryParamModel.SortList.Add(new SortField() { SortValue = "PositionCode" });
                }
                var list = this.positionService.FindList(queryWhere.PaginationModel, queryWhere.QueryParamModel);
                return base.ToSuccessObject(new { List = list, RecordTotal = queryWhere.PaginationModel.RecordTotal });
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // GET: api/Position/5
        /// <summary>
        /// 获取职位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(string id)
        {
            try
            {
                var model = this.positionService.FindById(id);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/Position
        /// <summary>
        /// 添加职位信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]Position value)
        {
            try
            {
                this.positionService.AddOrModity(value);
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

        // DELETE: api/Position/5
        /// <summary>
        /// 删除职位信息
        /// </summary>
        /// <param name="id"></param>
        public object Delete(string id)
        {
            try
            {
                this.positionService.Delete(id);
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
