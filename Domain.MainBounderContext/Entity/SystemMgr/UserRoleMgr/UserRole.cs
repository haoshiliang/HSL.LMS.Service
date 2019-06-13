using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LMS.Domain.Seedwork;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity
{
    public class UserRole : EntityBase
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        [Column("ROLE_ID", TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string RoleId { get; set; }
        /// <summary>
        /// 角色模型
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual Role RoleModel { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        [Column("USER_ID", TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string UserId { get; set; }
        /// <summary>
        /// 用户模型
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User UserModel { get; set; }
        /// <summary>
        /// 标识角色是否为主入口
        /// </summary>
        [NotMapped]
        public virtual bool IsRoleMaster { get; set; }
    }
}
