using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace LMS.Domain.Seedwork
{
    public interface IQueryableUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 返回IDbSet实例
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
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
        /// 自定义SQL查询数据集
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        DbRawSqlQuery<TEntity> ExecuteQuerySql<TEntity>(string sql, object[] paramList);

    }
}
