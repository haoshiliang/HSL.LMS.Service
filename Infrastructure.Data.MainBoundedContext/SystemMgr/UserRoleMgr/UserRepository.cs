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

        #endregion
    }
}
