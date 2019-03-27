using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr
{
    public class UserDTO
    {
        public UserDTO()
        {
            this.RoleList = new List<RoleDTO>();
        }
        public virtual string Id { get; set; }
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
        /// 是否修改密码
        /// </summary>
        public virtual bool IsModityPassword { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public virtual IList<RoleDTO> RoleList { get; set; }
    }
}
