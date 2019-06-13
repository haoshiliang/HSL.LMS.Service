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
    public class Role : EntityBase
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        [Column("ROLE_CODE")]
        [MaxLength(64)]
        [Required]
        public virtual string RoleCode { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        [Column("ROLE_NAME")]
        [MaxLength(128)]
        [Required]
        public virtual string RoleName { get; set; }
        /// <summary>
        /// 角色简拼
        /// </summary>
        [Column("PY_CODE")]
        [MaxLength(64)]
        [Required]
        public virtual string PyCode { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("CREATE_DATE")]
        [Required]
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        [Column("LAST_UPDATE_DATE")]
        [Required]
        public virtual DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// 用户角色列表
        /// </summary>
        public virtual ICollection<UserRole> UserRoleList { get; set; }
        /// <summary>
        /// 角色模块列表
        /// </summary>
        public virtual ICollection<RoleModule> RoleModuleList { get; set; }
    }
}
