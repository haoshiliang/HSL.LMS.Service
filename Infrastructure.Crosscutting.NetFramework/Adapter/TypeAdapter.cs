using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using LMS.Infrastructure.Crosscutting.Adapter;

namespace LMS.Infrastructure.Crosscutting.NetFramework.Adapter
{
    public class TypeAdapter : ITypeAdapter
    {
        #region 接口实现

        /// <summary>
        /// DataTable转DTO列表
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public IList<TTarget> Adapt<TTarget>(DataTable dt) where TTarget : class, new()
        {
            // 定义集合     
            IList<TTarget> ts = new List<TTarget>();
            string tempName = "";
            if (dt == null) return ts;
            foreach (DataRow dr in dt.Rows)
            {
                var t = new TTarget();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                string logName = t.GetType().Name;
                foreach (PropertyInfo pi in propertys)
                {
                    if (!pi.PropertyType.IsValueType && pi.PropertyType.Name != "String") continue;

                    tempName = pi.Name;
                    // 检查DataTable是否包含此列  
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter   
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                        {
                            Type propType = pi.PropertyType;
                            if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                NullableConverter nullableConverter = new NullableConverter(propType);
                                propType = nullableConverter.UnderlyingType;
                            }

                            if (propType == typeof(Guid))
                            {
                                Guid gid = new Guid(value.ToString());
                                pi.SetValue(t, gid, null);
                            }
                            else
                            {
                                if (!propType.IsEnum)
                                {
                                    pi.SetValue(t, Convert.ChangeType(value, propType), null);
                                }
                                else
                                {
                                    pi.SetValue(t, Convert.ToInt32(value), null);
                                }
                            }
                        }
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// 持久化类与DTO的转化
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TTarget Adapt<TSource, TTarget>(TSource source)
            where TSource : class
            where TTarget : class, new()
        {
            var t = source;
            var toT = new TTarget();
            if (source == null) return toT;
            //取出转化后的类的所有属性
            PropertyInfo[] propertys = toT.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                //取出有没有对应属性名与类型的字段
                PropertyInfo existProperty = t.GetType().GetProperty(pi.Name, pi.PropertyType);
                if (existProperty != null && pi.CanWrite)
                {
                    Type propType = pi.PropertyType;
                    object propertyValue = existProperty.GetValue(t, null);
                    if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        NullableConverter nullableConverter = new NullableConverter(propType);
                        propType = nullableConverter.UnderlyingType;
                    }
                    if (propertyValue != null && propType.Namespace.ToUpper()=="SYSTEM")
                    {
                        if (propType == typeof(Guid))
                        {
                            pi.SetValue(toT, Guid.Parse(propertyValue.ToString()), null);
                        }
                        else
                        {
                            pi.SetValue(toT, Convert.ChangeType(propertyValue, propType), null);
                        }
                    }
                }
            }
            return toT;
        }

        #endregion
    }
}
