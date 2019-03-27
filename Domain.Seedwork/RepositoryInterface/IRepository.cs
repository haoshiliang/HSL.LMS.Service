using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.Seedwork
{
    public interface IRepository<TEntity> where TEntity: EntityBase
    {
        #region 公共变量 

        IUnitOfWork UnitOfWork { get; }

        #endregion

        #region 数据库基本操作

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        void Add(TEntity t);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        void Modity(TEntity t);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        void Remove(TEntity t);
        /// <summary>
        /// 取出当前值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(object id, bool isSlave = false);

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 取出所有记录
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(bool isSlave = false);

        /// <summary>
        /// 获取过虑列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter, bool isSlave = false);


        /// <summary>
        /// 获取分页过虑列表
        /// </summary>
        /// <param name="pagination">分页参数实体</param>
        /// <param name="queryParam">查询数据实体列表</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPaged(Pagination pagination, QueryParam queryParam, bool isSlave = false);

        /// <summary>
        /// 获取分页过虑列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        IEnumerable<DTO> GetPagedSql<DTO>(string sql, Pagination pagination, QueryParam queryParam, bool isSlave = false);

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramNameList">参数名</param>
        /// <param name="paramValueList">参数值</param>
        /// <returns></returns>
        int ExecuteSql(string sql, string[] paramNameList, object[] paramValueList);

        /// <summary>
        /// 自定义SQL查询数据集
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramNameList">参数名</param>
        /// <param name="paramValueList">参数值</param>
        /// <returns></returns>
        IEnumerable<DTO> ExecuteQuerySql<DTO>(string sql, string[] paramNameList, object[] paramValueList, bool isSlave = false);

        #endregion;
    }
}
