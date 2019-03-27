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
    /// 公司部门职位管理
    /// </summary>
    public class CorpDepartPositionController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 公司部门职位服务
        /// </summary>
        private readonly ICorpDepartPositionService corpDepartPositionService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="corpDepartPositionService"></param>
        public CorpDepartPositionController(ICorpDepartPositionService corpDepartPositionService)
        {
            this.corpDepartPositionService = corpDepartPositionService;
        }

        #endregion

        #region API


        // POST: api/Position
        /// <summary>
        /// 添加公司部门职位关系信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]string value)
        {
            try
            {
                this.corpDepartPositionService.Add(JsonConvert.DeserializeObject<List<CorpDepartPosition>>(value));
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
