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

namespace MVC.Client.Controllers.CommonMgr
{
    /// <summary>
    /// 公共数据
    /// </summary>
    public class CommonController : ApiBaseController
    {
        #region API

        // GET: api/Common/ControlType
        /// <summary>
        /// 获取控件类型
        /// </summary>
        /// <returns></returns>
        [Route("api/Common/ControlType")]
        public object GetControlType()
        {
            try
            {
                var model = QueryConfig.GetEnumList(typeof(QueryConfig.ControlType));
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }
        // GET: api/Common/DataType
        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <returns></returns>
        [Route("api/Common/DataType")]
        public object GetDataType()
        {
            try
            {
                var model = QueryConfig.GetEnumList(typeof(QueryConfig.DataType));
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }
        // GET: api/Common/DataType
        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <returns></returns>
        [Route("api/Common/DateDefalutValueType")]
        public object GetDateDefalutValueType()
        {
            try
            {
                var model = QueryConfig.GetEnumList(typeof(QueryConfig.DateDefalutValueType));
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }
        #endregion
    }
}
