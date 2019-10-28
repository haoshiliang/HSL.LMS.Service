using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity
{
    public class DayLog : EntityBase
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        [Column("CREATE_USER_ID", TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string CreateUserId { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("CREATE_DATE")]
        [Required]
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 日志日期
        /// </summary>
        [Column("LOG_DATE")]
        [Required]
        public virtual DateTime LogDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [ForeignKey("CreateUserId")]
        public virtual User CreateUser { get; set; }

        /// <summary>
        /// 日志明细
        /// </summary>
        public virtual ICollection<DayLogDetail> DayLogDetailList { get; set; }
    }
}
