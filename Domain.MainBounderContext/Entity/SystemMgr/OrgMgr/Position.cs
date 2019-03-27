using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity
{
    public class Position : EntityBase
    {
        /// <summary>
        /// 职位编号
        /// </summary>
        public virtual string PositionCode { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>
        public virtual string PositionName { get; set; }
        /// <summary>
        /// 公司简拼
        /// </summary>
        public virtual string PyCode { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDel { get; set; }
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
