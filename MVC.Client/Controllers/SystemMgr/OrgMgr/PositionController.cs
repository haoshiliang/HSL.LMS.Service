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
        public object Get()
        {
            try
            {
                var list = this.positionService.FindList();
                return base.ToSuccessObject(list);
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
        public object Get(Guid id)
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
        public object Post([FromBody]string value)
        {
            try
            {
                this.positionService.AddOrModity(JsonConvert.DeserializeObject<Position>(value));
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

        // PUT: api/Position/5
        /// <summary>
        /// 更新职位信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public object Put(int id, [FromBody]string value)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<Position>(value);
                this.positionService.AddOrModity(model);

                return base.ToSuccessObject();
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
        public object Delete(Guid id)
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
