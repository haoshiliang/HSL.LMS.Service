using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class Corporation: EntityBase
    {
        /// <summary>
        /// 自动编码生成
        /// </summary>
        public virtual string AutomaticCode { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public virtual string CorpCode { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CorpName { get; set; }
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
        /// <summary>
        /// 父公司编号
        /// </summary>
        public virtual string ParentId { get; set; }
        /// <summary>
        /// 旧公司编号
        /// </summary>
        public virtual string OldParentId { get; set; }
        /// <summary>
        /// 父公司
        /// </summary>
        public virtual Corporation ParentCorp { get; set; }
        /// <summary>
        /// 子公司
        /// </summary>
        public virtual ICollection<Corporation> ChildCorpList { get; set; }

    }
}
