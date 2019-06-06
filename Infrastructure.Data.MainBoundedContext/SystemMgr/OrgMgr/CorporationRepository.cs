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
    public class CorporationRepository : Repository<Corporation>, ICorporationRepository
    {
        public CorporationRepository(MainUnitOfWork unitWork) : base(unitWork)
        {

        }

        /// <summary>
        /// 获取生成的最大编码
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public string GetAutomaticCode(string parentId)
        {
            var parentModel = base.GetList(m => m.Id == parentId).FirstOrDefault();
            var automaticCode = base.GetList(m => m.ParentId == parentId).Max(m => m.AutomaticCode);
            var parentCode = (parentModel != null ? parentModel.AutomaticCode : "");

            if (string.IsNullOrEmpty(automaticCode))
            {
                return parentCode + "00001";
            }
            else
            {
                return parentCode + (Int32.Parse(automaticCode.Substring(automaticCode.Length-5,5)) + 1).ToString().PadLeft(5, '0');
            }
        }

        /// <summary>
        /// 设置自动生成编号
        /// </summary>
        /// <param name="oldCode">旧编号</param>
        /// <param name="newCode">新编号</param>
        /// <param name="id">当前ID</param>
        /// <returns></returns>
        public void SetAutomaticCode(string oldCode,string newCode,string id)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("UPDATE SYS_CORPORATION c");
            sqlBuilder.AppendLine("   SET c.AUTOMATIC_CODE = @NewCode || SUBSTR(c.AUTOMATIC_CODE,@SubStrPos)");
            sqlBuilder.AppendLine(" WHERE c.AUTOMATIC_CODE LIKE @OldCode || '%'");
            sqlBuilder.AppendLine("   AND c.ID <> @Id");
            sqlBuilder.AppendLine("   AND c.IS_DEL = 0");
            base.ExecuteSql(sqlBuilder.ToString(), new string[] { "NewCode", "SubStrPos", "OldCode", "Id"}, new object[] { newCode, oldCode.Length + 1, oldCode, id });
        }
    }
}
