using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity
{
    public class User : EntityBase
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public virtual string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public virtual string Password { get; set; }
        /// <summary>
        /// 盐值
        /// </summary>
        public virtual string SaltValue { get; set; }
        /// <summary>
        /// 用户简拼
        /// </summary>
        public virtual string PyCode { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public virtual DateTime LastUpdateDate { get; set; }
        /// <summary>
        /// 用户名称列表
        /// </summary>
        public virtual ICollection<UserRole> UserRoleList { get; set; }
    }
}
