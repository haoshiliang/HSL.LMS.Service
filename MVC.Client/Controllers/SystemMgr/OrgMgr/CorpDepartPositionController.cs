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
using LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr;
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

        /// <summary>
        /// 获取部门职位关系列表
        /// </summary>
        /// <param name="corpId">公司编号</param>
        /// <returns></returns>
        public object Get(string corpId)
        {
            try
            {
                var m = this.corpDepartPositionService.FindAllList(corpId);
                return base.ToSuccessObject(m);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取树列表
        /// </summary>
        /// <returns></returns>
        [Route("api/CorpDepartPosition/TreeList")]
        public object GetTreeList()
        {
            try
            {
                var m = this.corpDepartPositionService.FindOrgList();
                return base.ToSuccessObject(m);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取所有公司下部门
        /// </summary>
        /// <returns></returns>
        [Route("api/CorpDepartPosition/DeptList")]
        public object GetDeptList()
        {
            try
            {
                var m = this.corpDepartPositionService.FindAllDeptList();
                return base.ToSuccessObject(m);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        /// <summary>
        /// 获取所有公司所有部门下职位
        /// </summary>
        /// <returns></returns>
        [Route("api/CorpDepartPosition/PositionList")]
        public object GetPositionList()
        {
            try
            {
                var m = this.corpDepartPositionService.FindAllPositionList();
                return base.ToSuccessObject(m);
            }
            catch (Exception ex)
            {
                return base.ToFailureObject(ex.Message);
            }
        }

        // POST: api/Position
        /// <summary>
        /// 添加公司部门职位关系信息
        /// </summary>
        /// <param name="value"></param>
        public object Post([FromBody]IList<DeptPositionDTO> value)
        {
            try
            {
                //解析公司-部门-职位列表
                var postList = new List<CorpDepartPosition>();
                foreach(var deptM in value)
                {
                    var pList = deptM.PositionList.Where(m=>m.IsChecked==true);
                    foreach(var pModel in pList)
                    {
                        postList.Add(new CorpDepartPosition() { CorpId = pModel.Id.Split('_')[0], DepartId = pModel.Id.Split('_')[1], PositionId = pModel.Id.Split('_')[2] });
                    }
                }
                this.corpDepartPositionService.Add(postList);

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
