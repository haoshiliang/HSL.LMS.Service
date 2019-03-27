using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Application.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public class CorpDepartPositionService : ICorpDepartPositionService
    {
        #region 私有变量

        /// <summary>
        /// 公司部门职位仓储
        /// </summary>
        private readonly ICorpDepartPositionRepository corpDepartPositionRepository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="corpDepartPositionRepository"></param>
        public CorpDepartPositionService(ICorpDepartPositionRepository corpDepartPositionRepository)
        {
            this.corpDepartPositionRepository = corpDepartPositionRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="modelList"></param>
        public void Add(IList<CorpDepartPosition> modelList)
        {
            var corpDepartList = modelList.GroupBy(m => new { m.CorpId, m.DepartId }).Select(m => new { m.Key.CorpId, m.Key.DepartId });
            foreach(var m in corpDepartList)
            {
                this.corpDepartPositionRepository.RemoveByCorpId(m.CorpId, m.DepartId);
            }
            foreach(var m in modelList)
            {
                m.CreateDate = DateTime.Now;
                m.LastUpdateDate = DateTime.Now;
                this.corpDepartPositionRepository.Add(m);
            }
            this.corpDepartPositionRepository.SaveChanges();
        }

        #endregion
    }
}
