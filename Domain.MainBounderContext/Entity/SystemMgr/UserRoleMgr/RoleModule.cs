using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LMS.Domain.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity
{
    public class RoleModule : EntityBase
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
        /// 模块编号
        /// </summary>
        [Column("MODULE_ID", TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string ModuleId { get; set; }
        /// <summary>
        /// 模块模型
        /// </summary>
        [ForeignKey("ModuleId")]
        public virtual Module ModuleModel { get; set; }
    }
}
