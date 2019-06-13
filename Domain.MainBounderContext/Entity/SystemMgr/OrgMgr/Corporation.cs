using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class Corporation: EntityBase
    {
        [Column("PARENT_ID",TypeName ="CHAR")]
        [MaxLength(36)]
        /// <summary>
        /// 父公司编号
        /// </summary>
        public virtual string ParentId { get; set; }
        /// <summary>
        /// 旧公司编号
        /// </summary>
        [NotMapped]
        public virtual string OldParentId { get; set; }
        /// <summary>
        /// 父公司
        /// </summary>
        [ForeignKey("ParentId")]
        public virtual Corporation ParentCorp { get; set; }
        /// <summary>
        /// 自动编码生成
        /// </summary>
        [Column("AUTOMATIC_CODE")]
        [MaxLength(128)]
        [Required]
        public virtual string AutomaticCode { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        [Column("CORP_CODE")]
        [MaxLength(128)]
        [Required]
        public virtual string CorpCode { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [Column("CORP_NAME")]
        [MaxLength(64)]
        [Required]
        public virtual string CorpName { get; set; }
        /// <summary>
        /// 公司简拼
        /// </summary>
        [Column("PY_CODE")]
        [MaxLength(64)]
        [Required]
        public virtual string PyCode { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("IS_DEL")]
        [Required]
        public virtual bool IsDel { get; set; }
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
        /// 子公司
        /// </summary>
        public virtual ICollection<Corporation> ChildCorpList { get; set; }
    }
}
