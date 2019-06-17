using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Repository;
using System.Linq.Expressions;

namespace LMS.Infrastructure.Data.MainBoundedContext.SystemMgr.UserRoleMgr
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MainUnitOfWork unitWork) : base(unitWork)
        {
            
        }

        #region 公共方法 

        /// <summary>
        /// 是否存在登录名
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public bool IsExistLoginName(string loginName, string id)
        {
            return base.GetList(m => m.LoginName.ToUpper() == loginName.ToUpper() && m.Id != id).Count() > 0;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <param name="pagination"></param>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public ICollection<DTO> GetList<DTO>(Pagination pagination, QueryParam queryParam) where DTO : class
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendLine("SELECT u.ID,u.CODE,u.NAME,u.LOGIN_NAME AS LoginName,u.TELEPHONE,u.ADDRESS,u.CREATE_DATE,c.CORP_NAME AS CorpName,d.DEPART_NAME AS DeptName,p.POSITION_NAME AS PositionName");
            sqlBuilder.AppendLine("  FROM SYS_USER u");
            sqlBuilder.AppendLine(" INNER JOIN SYS_CORPORATION c ON c.ID = u.CORP_ID");
            sqlBuilder.AppendLine(" INNER JOIN SYS_DEPARTMENT d ON d.ID = u.DEPT_ID");
            sqlBuilder.AppendLine(" INNER JOIN SYS_POSITION p ON p.ID = u.POSITION_ID");
            sqlBuilder.AppendLine(" WHERE 1=1");
            sqlBuilder.AppendLine("{WHERE}");
            return base.GetPagedSql<DTO>(sqlBuilder.ToString(), pagination, queryParam).ToList();
        }

        #endregion
    }
}
