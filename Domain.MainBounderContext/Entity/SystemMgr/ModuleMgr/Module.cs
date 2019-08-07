using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LMS.Domain.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Entity
{
    public class Module : EntityBase
    {
        /// <summary>
        /// 父模块编号
        /// </summary>
        [Column("PARENT_ID",TypeName = "CHAR")]
        [MaxLength(36)]
        public virtual string ParentId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [Column("CODE")]
        [MaxLength(128)]
        [Required]
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column("NAME")]
        [MaxLength(64)]
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 模块图标
        /// </summary>
        [Column("ICON")]
        [MaxLength(64)]
        public virtual string Icon { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [Column("MODULE_NAME")]
        [MaxLength(128)]
        public virtual string ModuleName { get; set; }
        /// <summary>
        /// 模块路径
        /// </summary>
        [Column("MODULE_PATH")]
        [MaxLength(128)]
        public virtual string ModulePath { get; set; }
        /// <summary>
        /// 是否功能
        /// </summary>
        [Column("IS_FUNCTION")]
        [Required]
        public virtual bool IsFunction { get; set; }
        /// <summary>
        /// 是否允许设置查询
        /// </summary>
        [Column("IS_ALLOW_QUERY")]
        [Required]
        public virtual bool IsAllowQuery { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        [Column("IS_ENABLED")]
        [Required]
        public virtual bool IsEnabled { get; set; }
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
        /// 父模块
        /// </summary>
        [ForeignKey("ParentId")]
        public virtual Module ParentModule { get; set; }
        /// <summary>
        /// 子模块列表
        /// </summary>
        public virtual ICollection<Module> ChildList { get; set; }
        /// <summary>
        /// 角色模块功能列表
        /// </summary>
        public virtual ICollection<RoleModule> RoleModuleList { get; set; }
    }
}
