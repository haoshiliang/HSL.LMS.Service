using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr
{
    public class CorporationDTO
    {
        public CorporationDTO()
        {
            ChildList = new List<CorporationDTO>();
        }
        public virtual string Id { get; set; }
        /// <summary>
        /// 自动编号
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
        /// 父公司编号
        /// </summary>
        public virtual string ParentId { get; set; }
        /// <summary>
        /// 父公司名称
        /// </summary>
        public virtual string ParentName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public virtual DateTime LastUpdateDate { get; set; }
        /// <summary>
        /// 子公司
        /// </summary>
        public virtual ICollection<CorporationDTO> ChildList { get; set; }
    }
}
