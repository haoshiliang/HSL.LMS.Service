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
    public class Department : EntityBase
    {
        public Department()
        {
            IsDel = false;
        }
        /// <summary>
        /// 科室编号
        /// </summary>
        [Column("DEPART_CODE")]
        [MaxLength(64)]
        [Required]
        public virtual string DepartCode { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        [Column("DEPART_NAME")]
        [MaxLength(128)]
        [Required]
        public virtual string DepartName { get; set; }
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
    }
}
