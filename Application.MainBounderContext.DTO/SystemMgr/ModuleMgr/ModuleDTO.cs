using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.SystemMgr.ModuleMgr
{
    public class ModuleDTO
    {
        #region 私有变量

        /// <summary>
        /// 模块路径
        /// </summary>
        private string modulePath;

        /// <summary>
        /// 图标
        /// </summary>
        private string icon;

        #endregion

        #region 构造函数

        public ModuleDTO()
        {
            this.ChildList = new List<ModuleDTO>();
            this.FunctionList = new Dictionary<string, string>();
        }

        #endregion

        #region 属性

        public virtual string Id { get; set; }
        /// <summary>
        /// 父模块编号
        /// </summary>
        public virtual string ParentId { get; set; }
        /// <summary>
        /// 父模块名称
        /// </summary>
        public virtual string ParentName { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 当前分层数
        /// </summary>
        public virtual int Level { get; set; }
        /// <summary>
        /// 模块图标
        /// </summary>
        public virtual string Icon
        {
            get { return this.icon ?? ""; }
            set { this.icon = value; }
        }
        /// <summary>
        /// 模块路径
        /// </summary>
        public virtual string ModulePath
        {
            get { return this.modulePath ?? ""; }
            set { this.modulePath = value; }
        }
        /// <summary>
        /// 是否属性功能
        /// </summary>
        public virtual int IsFunction { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public virtual int IsEnabled { get; set; }
        /// <summary>
        /// 是否允许设置查询
        /// </summary>
        public virtual int IsAllowQuery { get; set; }
        /// <summary>
        /// 模块层级
        /// </summary>
        public virtual int ModuleLevel { get; set; }

        /// <summary>
        /// 子模块列表
        /// </summary>
        public virtual ICollection<ModuleDTO> ChildList { get; set; }
        /// <summary>
        /// 功能列表
        /// </summary>
        public virtual Dictionary<string,string> FunctionList { get; set; }

        #endregion
    }
}
