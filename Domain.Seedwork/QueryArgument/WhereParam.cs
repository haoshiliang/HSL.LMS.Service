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
        /// 操作类型
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 查询值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public QueryConfig.DataType DataType { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public QueryConfig.ControlType  ControlType { get; set; }
        /// <summary>
        /// 绑定列表
        /// </summary>
        public object BinderList { get; set; }
    }

    /// <summary>
    /// 控件类型
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// 文本框
        /// </summary>
        [Description("文本框")]
        ReportTextBox = 1,

        /// <summary>
        /// 密码框
        /// </summary>
        [Description("密码框")]
        ReportPasswordBox = 7,

        /// <summary>
        /// 日期框
        /// </summary>
        [Description("日期框")]
        ReportTimePicker = 2,

        /// <summary>
        /// 下拉单元框
        /// </summary>
        [Description("下拉单选框")]
        ReportComboRadioBox = 3,

        /// <summary>
        /// 下拉复选框
        /// </summary>
        [Description("下拉复选框")]
        ReportComboCheckBox = 4,
        /// <summary>
        /// 快速查找文本框
        /// </summary>
        [Description("快速查找文本框")]
        ReportComboTextBox = 5,
        /// <summary>
        /// 复选框
        /// </summary>
        [Description("复选框")]
        ReportCheckBox = 6
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 字符型
        /// </summary>
        [Description("字符型")]
        String = 1,

        /// <summary>
        /// 日期型
        /// </summary>
        [Description("日期型")]
        DateTime,

        /// <summary>
        /// 数值型
        /// </summary>
        [Description("数值型")]
        Double
    }

    /// <summary>
    /// 日期默认值
    /// </summary>
    public enum DateDefalutValueType
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,
        /// <summary>
        /// 当前日期
        /// </summary>
        [Description("当前日期")]
        CurrentDate = 1,
        /// <summary>
        /// 上月当前日
        /// </summary>
        [Description("上月当前日")]
        PreCurrentDate = 2,
        /// <summary>
        /// 本月第－天
        /// </summary>
        [Description("本月第－天")]
        ThisMonthFirstDay = 11,
        /// <summary>
        /// 本月最后一天
        /// </summary>
        [Description("本月最后一天")]
        ThisMonthLastDay = 12,
        /// <summary>
        /// 上月第一天
        /// </summary>
        [Description("上月第一天")]
        PreviousMonthFirstDay = 21,
        /// <summary>
        /// 上月最后一天
        /// </summary>
        [Description("上月最后一天")]
        PreviousMonthLastDay = 22,
        /// <summary>
        /// 下月第一天
        /// </summary>
        [Description("下月第一天")]
        NextMonthFirstDay = 31,
        /// <summary>
        /// 下月最后一天
        /// </summary>
        [Description("下月最后一天")]
        NextMonthLastDay = 32,
        /// <summary>
        /// 本周第一天
        /// </summary>
        [Description("本周第一天")]
        ThisWeekFirstDay = 41,
        /// <summary>
        /// 本周最后一天
        /// </summary>
        [Description("本周最后一天")]
        ThisWeekLastDay = 42,
        /// <summary>
        /// 上周第一天
        /// </summary>
        [Description("上周第一天")]
        PreviousWeekFirstDay = 51,
        /// <summary>
        /// 上周最后一天
        /// </summary>
        [Description("上周最后一天")]
        PreviousWeekLastDay = 52,
        /// <summary>
        /// 下周第一天
        /// </summary>
        [Description("下周第一天")]
        NextWeekFirstDay = 61,
        /// <summary>
        /// 下周最后一天
        /// </summary>
        [Description("下周最后一天")]
        NextWeekLastDay = 62
    }

}
