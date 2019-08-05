using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Seedwork
{
    public class QueryConfig
    {
        #region 枚举类型

        /// <summary>
        /// 控件类型
        /// </summary>
        public enum ControlType
        {
            /// <summary>
            /// 文本框
            /// </summary>
            [Description("文本框")]
            TextBox = 1,

            /// <summary>
            /// 日期框
            /// </summary>
            [Description("日期框")]
            TimeText = 2,

            /// <summary>
            /// 下拉单选框
            /// </summary>
            [Description("下拉单选")]
            ComboRadioBox = 3,

            /// <summary>
            /// 下拉多选框
            /// </summary>
            [Description("下拉多选")]
            ComboMultipleBox = 4,

            /// <summary>
            /// 下拉公司单选框
            /// </summary>
            [Description("下拉公司单选框")]
            ComboTreeBox = 5,

            /// <summary>
            /// 复选框
            /// </summary>
            [Description("复选框")]
            CheckBox = 6
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
            DateTime=2,

            /// <summary>
            /// 数值型
            /// </summary>
            [Description("数值型")]
            Double=3,

            /// <summary>
            /// 布尔类型
            /// </summary>
            [Description("布尔类型")]
            Bool = 4
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

        #endregion

        #region 获取枚举列表

        /// <summary>
        /// 获取枚举的键值与描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static IList<Dictionary<string, string>> GetEnumList(Type type)
        {
            var enumDt = new List<Dictionary<string, string>>();

            FieldInfo[] fields = type.GetFields();
            for (int i = 1; i < fields.Length; i++)
            {
                FieldInfo field = fields[i];
                enumDt.Add(new Dictionary<string, string>()
                {
                    {"Id",((int)Enum.Parse(type, field.Name, true)).ToString() },
                    {"Value",field.Name },
                    {"Name",GetDescription(field)}
                });
            }
            return enumDt;
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private static string GetDescription(FieldInfo field)
        {
            object[] descList = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (descList == null || descList.Length <= 0)
            {
                return field.Name;
            }
            else
            {
                DescriptionAttribute da = (DescriptionAttribute)descList[0];
                return da.Description;
            }
        }

        #endregion
    }
}
