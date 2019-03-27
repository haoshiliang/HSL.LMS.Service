using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        #region 公共方法 

        /// <summary>
        /// 是否存在登录名
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        bool IsExistLoginName(string loginName,string id);

        #endregion
    }
}
