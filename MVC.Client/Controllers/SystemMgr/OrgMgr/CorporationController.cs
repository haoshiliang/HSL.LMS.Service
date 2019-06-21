using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using LMS.Application.MainBounderContext.SystemMgr.OrgMgr;
using LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using System.Data.Entity.Validation;

namespace MVC.Client.Controllers.SystemMgr.OrgMgr
{
    /// <summary>
    /// 公司管理
    /// </summary>
    public class CorporationController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 公司服务
        /// </summary>
        private readonly ICorporationService corpService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="corpService"></param>
        public CorporationController(ICorporationService corpService)
        {
            this.corpService = corpService;
        }

        #endregion

        #region API

        // GET: api/Corporation/5
        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public object Get(string id)
        {
            try
            {
                var model = this.corpService.FindById(id);
                return base.ToSuccessObject(model);
            }
            catch(Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <returns></returns>
        public object GetList()
        {
            try
            {
                var list = this.corpService.FindList(null);
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取可用公司列表
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [Route("api/Corporation/EnableList")]
        public object GetEnableList(string id)
        {
            try
            {
                var list = this.corpService.FindList(id);
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取可用公司树列表
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        [Route("api/Corporation/TreeList")]
        public object GetTreeList()
        {
            try
            {
                var list = this.corpService.FindTreeList();
                return base.ToSuccessObject(list);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/Corporation
        /// <summary>
        /// 添加公司信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Post([FromBody]Corporation value)
        {
            try
            {
                this.corpService.AddOrModity(value);
                return base.ToSuccessObject();
            }
            catch(DbEntityValidationException dbEx)
            {
                return base.ToFailureObject(dbEx.Message);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // DELETE:api/
        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Delete(string id)
        {
            try
            {
                this.corpService.Delete(id);
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
