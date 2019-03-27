using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity
{
    public class CorpDepartPosition : EntityBase
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public virtual string CorpId { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        public virtual string DepartId { get; set; }
        /// <summary>
        /// 职位编号
        /// </summary>
        public virtual string PositionId { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public virtual DateTime LastUpdateDate { get; set; }
    }
}
