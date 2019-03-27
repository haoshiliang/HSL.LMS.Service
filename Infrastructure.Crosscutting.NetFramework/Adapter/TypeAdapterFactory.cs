using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Infrastructure.Crosscutting.Adapter;

namespace LMS.Infrastructure.Crosscutting.NetFramework.Adapter
{
    public class TypeAdapterFactory
    {
        /// <summary>
        /// 创建转化实例
        /// </summary>
        /// <returns></returns>
        public static ITypeAdapter Create()
        {
            return new TypeAdapter();
        }
    }
}
