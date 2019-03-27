﻿using System;
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
            this.FunctionList = new List<ModuleDTO>();
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
        public virtual bool IsFunction { get; set; }
        /// <summary>
        /// 是否属性功能 0 否,1 是
        /// </summary>
        public virtual int IsFunctionQuery { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public virtual DateTime LastUpdateDate { get; set; }
        /// <summary>
        /// 子模块列表
        /// </summary>
        public virtual ICollection<ModuleDTO> ChildList { get; set; }
        /// <summary>
        /// 功能列表
        /// </summary>
        public virtual ICollection<ModuleDTO> FunctionList { get; set; }

        #endregion
    }
}