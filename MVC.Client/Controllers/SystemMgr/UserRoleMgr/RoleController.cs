﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using System.Data.Entity.Validation;
using LMS.Domain.Seedwork;

namespace MVC.Client.Controllers.SystemMgr.UserRoleMgr
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController : ApiBaseController
    {
        #region 私有变量

        /// <summary>
        /// 角色服务
        /// </summary>
        private readonly IRoleService roleService;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleService"></param>
        public RoleController(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        #endregion

        #region API

        // GET: api/Role
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public object Get(string pagination, string queryParam)
        {
            try
            {
                var paginationModel = JsonConvert.DeserializeObject<Pagination>(pagination);
                var queryParamModel = JsonConvert.DeserializeObject<QueryParam>(queryParam);
                if (queryParamModel.SortList.Count == 0)
                {
                    queryParamModel.SortList.Add(new SortField() { SortValue = "RoleCode" });
                }
                var list = this.roleService.FindList(paginationModel, queryParamModel);
                return base.ToSuccessObject(new { List = list, RecordTotal = paginationModel.RecordTotal });
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // GET: api/Role/5
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Get(string id)
        {
            try
            {
                var model = this.roleService.FindById(id);
                return base.ToSuccessObject(model);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/Role
        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]Role value)
        {
            try
            {
                this.roleService.AddOrModity(value);
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

        // DELETE: api/Role/5
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="id"></param>
        public object Delete(string id)
        {
            try
            {
                this.roleService.Delete(id);
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
