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
        /// <returns></returns>
        public void RemoveByCorpId(string corpId)
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.AppendLine("DELETE FROM SYS_CORP_DEPART_POSITION cdp");
            sBuilder.AppendLine(" WHERE cdp.CORP_ID = @CorpId");
            base.ExecuteSql(sBuilder.ToString(), new string[] { "CorpId"}, new object[] { corpId });
        }

        /// <summary>
        /// 获取公司-部门-职位列表
        /// </summary>
        /// <param name="corpId">公司编号</param>
        public IList<DTO> GetAllList<DTO>(string corpId) where DTO : class
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.AppendLine("SELECT d.ID AS DepartId,d.DEPART_NAME AS DepartName,@CorpId || '/' || d.ID || '/'|| p.ID AS PositionId,p.POSITION_NAME AS PositionName,");
            sBuilder.AppendLine("       CASE WHEN cdp.CORP_ID IS NULL THEN 0 ELSE 1 END IsSelected");
            sBuilder.AppendLine("  FROM SYS_CORPORATION c");
            sBuilder.AppendLine("  LEFT JOIN SYS_DEPARTMENT d ON 1=1");
            sBuilder.AppendLine("  LEFT JOIN SYS_POSITION p ON 1=1");
            sBuilder.AppendLine("  LEFT JOIN SYS_CORP_DEPART_POSITION cdp ON cdp.CORP_ID = c.ID AND cdp.DEPART_ID = d.ID AND cdp.POSITION_ID = p.ID ");
            sBuilder.AppendLine(" WHERE c.ID = @CorpId");
            sBuilder.AppendLine("   AND d.IS_DEL = '0'");
            sBuilder.AppendLine("   AND p.IS_DEL = '0'");

            return base.ExecuteQuerySql<DTO>(sBuilder.ToString(), new string[] { "CorpId" }, new object[] { corpId }).ToList();
        }

        /// <summary>
        /// 获取公司-部门-职位列表
        /// </summary>
        /// <param name="corpId">公司编号</param>
        public IList<DTO> GetList<DTO>(string corpId) where DTO : class
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.AppendLine("SELECT d.ID AS DepartId,d.DEPART_NAME AS DepartName,p.ID AS PositionId,p.POSITION_NAME AS PositionName");
            sBuilder.AppendLine("  FROM SYS_CORP_DEPART_POSITION cdp");
            sBuilder.AppendLine(" INNER JOIN SYS_CORPORATION c ON c.ID = cdp.CORP_ID");
            sBuilder.AppendLine(" INNER JOIN SYS_DEPARTMENT d ON d.ID = cdp.DEPART_ID");
            sBuilder.AppendLine(" INNER JOIN SYS_POSITION p ON p.ID = cdp.POSITION_ID");
            sBuilder.AppendLine(" WHERE c.ID = @CorpId");
            sBuilder.AppendLine("   AND d.IS_DEL = '0'");
            sBuilder.AppendLine("   AND p.IS_DEL = '0'");

            return base.ExecuteQuerySql<DTO>(sBuilder.ToString(), new string[] { "CorpId" }, new object[] { corpId }).ToList();
        }
    }
}
