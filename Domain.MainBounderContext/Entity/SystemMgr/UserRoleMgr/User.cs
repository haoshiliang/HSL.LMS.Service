using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity
{
    public class User : EntityBase
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Column("CODE")]
        [MaxLength(64)]
        [Required]
        public virtual string Code { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [Column("NAME")]
        [MaxLength(128)]
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        [Column("LOGIN_NAME")]
        [MaxLength(64)]
        [Required]
        public virtual string LoginName { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Column("PASSWORD")]
        [MaxLength(64)]
        [Required]
        public virtual string Password { get; set; }
        /// <summary>
        /// 盐值
        /// </summary>
        [Column("SALT_VALUE")]
        [MaxLength(36)]
        [Required]
        public virtual string SaltValue { get; set; }
        /// <summary>
        /// 用户简拼
        /// </summary>
        [Column("PY_CODE")]
        [MaxLength(64)]
        [Required]
        public virtual string PyCode { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        [Column("CORP_ID",TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string CorpId { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        [ForeignKey("CorpId")]
        public virtual Corporation CorporationModel { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        [Column("DEPT_ID", TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string DeptId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [ForeignKey("DeptId")]
        public virtual Department DepartmentModel { get; set; }
        /// <summary>
        /// 职位ID
        /// </summary>
        [Column("POSITION_ID", TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string PositionId { get; set; }
        /// <summary>
        /// 所属职位
        /// </summary>
        [ForeignKey("PositionId")]
        public virtual Position PositionModel { get; set; }
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
        /// 用户名称列表
        /// </summary>
        public virtual ICollection<UserRole> UserRoleList { get; set; }
    }
}
