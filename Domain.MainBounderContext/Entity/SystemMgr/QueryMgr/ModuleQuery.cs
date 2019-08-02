using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Entity
{
    /// <summary>
    /// 模块查询
    /// </summary>
    public class ModuleQuery : EntityBase
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        [Column("MODULE_ID",TypeName = "CHAR")]
        [MaxLength(36)]
        [Required]
        public virtual string ModuleId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Column("TITLE")]
        [MaxLength(64)]
        [Required]
        public virtual string Title { get; set; }
        /// <summary>
        /// 查询字段
        /// </summary>
        [Column("FIELD")]
        [MaxLength(256)]
        public virtual string Field { get; set; }
        /// <summary>
        /// 参数名
        /// </summary>
        [Column("PARAM_NAME")]
        [MaxLength(64)]
        [Required]
        public virtual string ParamName { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        [Column("OPERATOR_TYPE")]
        [MaxLength(32)]
        [Required]
        public virtual string Operator { get; set; }
        /// <summary>
        /// 默认查询值
        /// </summary>
        [Column("DEFAULT_VALUE")]
        [MaxLength(128)]
        public virtual string DefaultValue { get; set; }
        /// <summary>
        /// 下拉选项值
        /// </summary>
        [Column("DOWN_LIST_VALUE")]
        [MaxLength(512)]
        public virtual string DownListValue { get; set; }
        /// <summary>
        /// Exists条件
        /// </summary>
        [Column("EXISTS_VALUE")]
        [MaxLength(512)]
        public virtual string Exists { get; set; }
        /// <summary>
        /// 目标参数名
        /// </summary>
        [Column("TARGET_NAME")]
        [MaxLength(64)]
        public virtual string TargetName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        [Column("DATA_TYPE")]
        public virtual int DataType { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        [Column("CONTROL_TYPE")]
        public virtual int ControlType { get; set; }
        /// <summary>
        /// 是否默认查询
        /// </summary>
        [Column("IS_DEFAULT_QUERY")]
        public virtual bool IsDefaultQuery { get; set; }
        /// <summary>
        /// 关联字段1对应的控件参数名
        /// </summary>
        [Column("RELATIONID_1")]
        [MaxLength(64)]
        public virtual string RelationId_1 { get; set; }
        /// <summary>
        /// 关联字段2对应的控件参数名
        /// </summary>
        [Column("RELATIONID_2")]
        [MaxLength(64)]
        public virtual string RelationId_2 { get; set; }
        /// <summary>
        /// 关联字段3对应的控件参数名
        /// </summary>
        [Column("RELATIONID_3")]
        [MaxLength(64)]
        public virtual string RelationId_3 { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        [Column("DISPLAY_ORDER")]
        public virtual int DisplayOrder { get; set; }
        /// <summary>
        /// 下拉数据来源
        /// </summary>
        [Column("DROPDOWN_DATASOURCE")]
        public virtual int DropdownDataSource { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("CREATE_DATE")]
        [Required]
        public virtual DateTime CreateDate { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        [Column("LAST_UPDATE_DATE")]
        [Required]
        public virtual DateTime LastUpdateDate { get; set; }
    }
}
