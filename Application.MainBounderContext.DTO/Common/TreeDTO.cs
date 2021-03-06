﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.Common
{
    /// <summary>
    /// 树数据DTO
    /// </summary>
    public class TreeDTO
    {
        public TreeDTO()
        {
            this.ChildList = new List<TreeDTO>();
        }
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// ID完整值
        /// </summary>
        public string FullId { get; set; }
        /// <summary>
        /// ID_NAME完整值
        /// </summary>
        public string FullIdName { get; set; }
        /// <summary>
        /// 是否叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }
        /// <summary>
        /// 子列表
        /// </summary>
        public IList<TreeDTO> ChildList { get; set; }
    }
}
