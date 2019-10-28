using LMS.Application.MainBounderContext.WorkLogMgr.DayLogMgr;
using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity;
using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC.Client.Controllers.WorkLogMgr.DayLogMgr
{
    /// <summary>
    /// 日志控制器
    /// </summary>
    public class DayLogController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 服务
        /// </summary>
        private readonly IDayLogService dayLogService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dayLogService"></param>
        public DayLogController(IDayLogService dayLogService)
        {
            this.dayLogService = dayLogService;
        }

        #endregion

        #region API

        // GET: api/DayLog/List
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.Route("api/DayLog/List")]
        public object Post(QueryWhere queryWhere)
        {
            try
            {
                var list = this.dayLogService.FindList(queryWhere.PaginationModel, queryWhere.QueryParamModel);
                return base.ToSuccessObject(new { List = list, RecordTotal = queryWhere.PaginationModel.RecordTotal });
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // GET: api/DayLog/5
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(string id)
        {
            try
            {
                var model = this.dayLogService.FindById(id);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/DayLog
        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]DayLog value)
        {
            try
            {
                this.dayLogService.AddOrModity(value);
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

        // DELETE: api/DayLog/5
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public object Delete(string id)
        {
            try
            {
                this.dayLogService.Delete(id);
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