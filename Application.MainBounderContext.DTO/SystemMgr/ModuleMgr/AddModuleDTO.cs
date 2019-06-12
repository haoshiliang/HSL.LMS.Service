using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.SystemMgr.ModuleMgr
{
    public class AddModuleDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 父模块编号
        /// </summary>
        public virtual string ParentId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 模块图标
        /// </summary>
        public virtual string Icon { get; set; }
        /// <summary>
        /// 模块路径
        /// </summary>
        public virtual string ModulePath { get; set; }
        /// <summary>
        /// 是否功能
        /// </summary>
        public virtual bool IsFunction { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public virtual bool IsEnabled { get; set; }
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
