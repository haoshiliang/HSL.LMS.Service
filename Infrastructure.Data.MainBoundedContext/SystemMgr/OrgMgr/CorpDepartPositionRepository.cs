using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository;

namespace LMS.Infrastructure.Data.MainBoundedContext.SystemMgr.OrgMgr
{
    public class CorpDepartPositionRepository : Repository<CorpDepartPosition>, ICorpDepartPositionRepository
    {
        public CorpDepartPositionRepository(MainUnitOfWork unitWork) : base(unitWork)
        {

        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="corpId">公司编号</param>
        /// <param name="deprtId">部门编号</param>
        /// <returns></returns>
        public void RemoveByCorpId(string corpId, string deprtId)
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.AppendLine("DELETE FROM SYS_CORP_DEPART_POSITION cdp");
            sBuilder.AppendLine(" WHERE cdp.CORP_ID = @CorpId");
            sBuilder.AppendLine("   AND cdp.DEPART_ID = @DepartId");
            base.ExecuteSql(sBuilder.ToString(), new string[] { "CorpId", "DepartId" }, new object[] { corpId, deprtId });
        }
    }
}
