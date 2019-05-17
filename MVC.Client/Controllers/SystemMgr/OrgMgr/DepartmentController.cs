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
    /// 部门管理
    /// </summary>
    public class DepartmentController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 部门服务
        /// </summary>
        private readonly IDepartmentService deptService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deptService"></param>
        public DepartmentController(IDepartmentService deptService)
        {
            this.deptService = deptService;
        }

        #endregion

        #region API

        // GET: api/Department
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public object Get(string pagination,string queryParam)
        {
            try
            {
                var paginationModel = JsonConvert.DeserializeObject<Pagination>(pagination);
                var queryParamModel = JsonConvert.DeserializeObject<QueryParam>(queryParam);
                if (queryParamModel.SortList.Count == 0)
                {
                    queryParamModel.SortList.Add(new SortField() { SortValue = "DeprtCode" });
                }
                var list = this.deptService.FindList(paginationModel, queryParamModel);
                return base.ToSuccessObject(new { List = list, RecordTotal = paginationModel.RecordTotal });
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // GET: api/Department/5
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(Guid id)
        {
            try
            {
                var model = this.deptService.FindById(id);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/Department
        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]string value)
        {
            try
            {
                this.deptService.AddOrModity(JsonConvert.DeserializeObject<Department>(value));
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

        // PUT: api/Department/5
        /// <summary>
        /// 更新部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public object Put(int id, [FromBody]string value)
        {
            try
            {
                var model = JsonConvert.DeserializeObject<Department>(value);
                this.deptService.AddOrModity(model);

                return base.ToSuccessObject();
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // DELETE: api/Department/5
        /// <summary>
        /// 删除部门信息
        /// </summary>
        /// <param name="id"></param>
        public object Delete(Guid id)
        {
            try
            {
                this.deptService.Delete(id);
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
