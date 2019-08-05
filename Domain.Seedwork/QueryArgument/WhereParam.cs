using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Seedwork
{
    public class WhereParam
    {
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
        /// 查询值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// Exists条件
        /// </summary>
        public string Exists { get; set; }
        /// <summary>
        /// 目标参数名
        /// </summary>
        public string TargetName { get; set; }
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
        /// 数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public string  ControlType { get; set; }
        /// <summary>
        /// 绑定列表
        /// </summary>
        public object BinderList { get; set; }  
        /// <summary>
        /// 所有绑定列表
        /// </summary>
                   
        public object AllBinderList { get; set; }   
        /// <summary>
        /// 是否默认查询
        /// </summary>
        public bool IsDefaultQuery { get; set; }
    }
}
