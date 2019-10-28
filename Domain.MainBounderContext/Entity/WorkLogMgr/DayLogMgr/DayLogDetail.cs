using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity
{
    public class DayLogDetail : EntityBase
    {
        /// <summary>
        /// 日志主表ID
        /// </summary>
        [Column("DAY_LOG_ID", TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public string DayLogId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Column("TITLE")]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 工作描述
        /// </summary>
        [Column("WORK_DESCRIPTION")]
        [Required]
        public string WorkDescription { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("CREATE_DATE")]
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 日志主单
        /// </summary>
        [ForeignKey("DayLogId")]
        public DayLog DayLogModel { get; set; }

    }
}
