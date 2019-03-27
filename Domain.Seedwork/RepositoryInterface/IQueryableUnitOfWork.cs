using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace LMS.Domain.Seedwork
{
    public interface IQueryableUnitOfWork : IUnitOfWork, IReadUnitOfWork
    {
        /// <summary>
        /// 设置当前状态为修改状态
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="t"></param>
        void SetEntityModity<TEntity>(TEntity t) where TEntity : class;

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramList">参数</param>
        /// <returns></returns>
        int ExecuteSql(string sql, object[] paramList);

        /// <summary>
        /// 保存改变
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

    }
}
