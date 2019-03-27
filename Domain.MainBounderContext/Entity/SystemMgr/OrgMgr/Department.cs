using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity
{
    public class Department : EntityBase
    {
        /// <summary>
        /// 科室编号
        /// </summary>
        public virtual string DepartCode { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public virtual string DepartName { get; set; }
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
