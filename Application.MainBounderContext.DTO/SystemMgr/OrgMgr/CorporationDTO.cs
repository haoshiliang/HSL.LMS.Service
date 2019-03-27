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
            ChildCorpList = new List<CorporationDTO>();
        }
        public virtual string Id { get; set; }
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
        /// 子公司
        /// </summary>
        public virtual ICollection<CorporationDTO> ChildCorpList { get; set; }
    }
}
