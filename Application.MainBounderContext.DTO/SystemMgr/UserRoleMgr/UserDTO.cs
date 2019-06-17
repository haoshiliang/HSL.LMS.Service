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
            RoleList = new List<RoleDTO>();
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
        /// 公司部门职位
        /// </summary>
        public virtual string CorpName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public virtual string DeptName { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public virtual string PositionName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Tel { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// 角色列表
        /// </summary>
        public virtual IList<RoleDTO> RoleList { get; set; }
    }
}
