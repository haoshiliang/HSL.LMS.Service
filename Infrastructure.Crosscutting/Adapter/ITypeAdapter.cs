using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LMS.Infrastructure.Crosscutting.Adapter
{
    public interface ITypeAdapter
    {
        /// <summary>
        /// 持久化类与DTO的转化
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        TTarget Adapt<TSource, TTarget>(TSource source) where TSource : class where TTarget : class, new();

        /// <summary>
        /// DataTable转DTO列表
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        IList<TTarget> Adapt<TTarget>(DataTable dt) where TTarget : class, new();
    }
}
