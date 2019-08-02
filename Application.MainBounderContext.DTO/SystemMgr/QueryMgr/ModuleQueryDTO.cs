using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.DTO.SystemMgr.QueryMgr
{
    public class ModuleQueryDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 模块ID
        /// </summary>
        public string ModuleId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 查询字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 参数名
        /// </summary>
        public string ParamName { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 默认查询值
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// 目标参数名
        /// </summary>
        public string TargetName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public string ControlType { get; set; }
        /// <summary>
        /// 是否默认查询
        /// </summary>
        public int IsDefaultQuery { get; set; }
        /// <summary>
        /// 关联字段1对应的控件参数名
        /// </summary>
        public string RelationId_1 { get; set; }
        /// <summary>
        /// 关联字段2对应的控件参数名
        /// </summary>
        public string RelationId_2 { get; set; }
        /// <summary>
        /// 关联字段3对应的控件参数名
        /// </summary>
        public string RelationId_3 { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CreateDate { get; set; }
    }
}
