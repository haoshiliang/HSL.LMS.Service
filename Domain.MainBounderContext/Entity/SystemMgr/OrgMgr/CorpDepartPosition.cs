using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LMS.Domain.Seedwork;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity
{
    public class CorpDepartPosition : EntityBase
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        [Key]
        [Column("CORP_ID",Order = 0,TypeName ="CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string CorpId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [Key]
        [Column("DEPART_ID",Order = 1, TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string DepartId { get; set; }
        /// <summary>
        /// 职位编号
        /// </summary>
        [Key]
        [Column("POSITION_ID", Order = 2, TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string PositionId { get; set; }
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
    }
}
