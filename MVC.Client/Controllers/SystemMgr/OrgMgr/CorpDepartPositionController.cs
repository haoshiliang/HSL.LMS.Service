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
                        postList.Add(new CorpDepartPosition() { CorpId = pModel.Id.Split('/')[0], DepartId = pModel.Id.Split('/')[1], PositionId = pModel.Id.Split('/')[2] });
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
