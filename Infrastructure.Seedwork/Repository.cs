using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using System.Linq.Expressions;
using System.Reflection;
using System.Configuration;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;
using System.Data.SqlClient;

namespace LMS.Infrastructure.Seedwork
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        #region 私有变量

        /// <summary>
        /// 写工作单元
        /// </summary>
        private IQueryableUnitOfWork unitOfWork;

        #endregion

        #region 公共变量

        /// <summary>
        /// 事件工作单元
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this.unitOfWork;
            }
        }

        /// <summary>
        /// 读工作单元
        /// </summary>
        public IReadUnitOfWork ReadUnitWork { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitWork"></param>
        public Repository(IQueryableUnitOfWork unitWork)
        {
            //写工作单元
            this.unitOfWork = unitWork;

            //读工作单元
            if (ConfigurationManager.AppSettings["IsEnabledRW"] != "1")
            {
                this.ReadUnitWork = unitWork;
            }
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t"></param>
        public void Add(TEntity t)
        {
            this.DbSet().Add(t);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        public void Modity(TEntity t)
        {
            this.unitOfWork.SetEntityModity(t);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="t"></param>
        public void Remove(TEntity t)
        {
            this.DbSet().Remove(t);
        }

        /// <summary>
        /// 保存改变
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return this.unitOfWork.SaveChanges();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(object id, bool isSlave = false)
        {
            if (isSlave)
            {
                return this.ReadDbSet().Find(id);
            }
            else
            {
                return this.DbSet().Find(id);
            }
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll(bool isSlave = false)
        {
            if (isSlave)
            {
                return this.ReadDbSet().AsNoTracking().ToList();
            }
            else
            {
                return this.DbSet().AsNoTracking().ToList();
            }
        }

        /// <summary>
        /// 获取过虑列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> filter, bool isSlave = false)
        {
            if (isSlave)
            {
                return this.ReadDbSet().AsNoTracking().Where(filter);
            }
            else
            {
                return this.DbSet().AsNoTracking().Where(filter);
            }
        }

        /// <summary>
        /// 获取分页过虑列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetPaged(Pagination pagination, QueryParam queryParam, bool isSlave = false)
        {
            IQueryable<TEntity> tempData = null;
            IList<WhereParam> queryParamList = (queryParam.IsAdvancedQuery?queryParam.WhereList:queryParam.WhereList.Where(m=>m.IsDefaultQuery == true)).ToList();
            if (isSlave)
            {
                tempData = this.ReadDbSet().Where(this.GetAndExpression(queryParamList));
            }
            else
            {
                tempData = this.DbSet().Where(this.GetAndExpression(queryParamList));
            }
            MethodCallExpression resultExp = null;
            foreach (var sort in queryParam.SortList)
            {
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(sort.SortValue);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), sort.SortType.ToUpper() == "DESC" ? "OrderByDescending" : "OrderBy", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
                tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            }
            pagination.RecordTotal = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.PageSize * (pagination.PageIndex - 1)).Take<TEntity>(pagination.PageSize).AsQueryable();
            return tempData.ToList();
        }

        /// <summary>
        /// 获取分页过虑列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public IEnumerable<DTO> GetPagedSql<DTO>(string sql, Pagination pagination, QueryParam queryParam, bool isSlave = false)
        {
            IList<string> paramNameList = new List<string>();
            IList<object> paramValueList = new List<object>();
            StringBuilder whereBuilder = new StringBuilder();
            IList<WhereParam> queryParamList = (queryParam.IsAdvancedQuery ? queryParam.WhereList : queryParam.WhereList.Where(m => m.IsDefaultQuery == true)).ToList();

            if (queryParamList.Count > 0)
            {
                foreach(var whereItem in queryParamList)
                {
                    if (!string.IsNullOrEmpty(whereItem.Value))
                    {
                        string whereStr = "";   
                        string[] fields = whereItem.Field.Split('|');
                        foreach(var field in fields)
                        {
                            whereStr += this.GetCondition(field, whereItem.Operator, whereItem.Value) + " OR ";
                            if(whereItem.Operator.ToLower() != "in")
                            {
                                paramNameList.Add(field);
                                paramValueList.Add(this.GetParamValue(whereItem.Value, whereItem.DataType.ToString()));
                            }
                        }
                        whereBuilder.AppendLine("AND ("+ whereStr.Substring(0, whereStr.Length- 4) + ")");
                    }
                }
                sql = sql.Replace("{WHERE}", whereBuilder.ToString());
            }
            //取出行总数
            pagination.RecordTotal = this.ExecuteQuerySql<int>("SELECT COUNT(*) FROM (" + sql + ") tmp", paramNameList.ToArray(), paramValueList.ToArray(), isSlave).FirstOrDefault();
            //返回数据结果
            return this.ExecuteQuerySql<DTO>(this.CreatePagedSql(sql, pagination, queryParam.SortList), paramNameList.ToArray(), paramValueList.ToArray(), isSlave);
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paramNameList">参数名</param>
        /// <param name="paramValueList">参数值</param>
        /// <returns></returns>
        public int ExecuteSql(string sql, string[] paramNameList,object[] paramValueList)
        {
            return this.unitOfWork.ExecuteSql(this.GetParametersSql(sql), this.GetParameter(paramNameList, paramValueList));
        }
        /// <summary>
        /// 自定义SQL查询数据集
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramNameList">参数名</param>
        /// <param name="paramValueList">参数值</param>
        /// <returns></returns>
        public IEnumerable<DTO> ExecuteQuerySql<DTO>(string sql, string[] paramNameList, object[] paramValueList, bool isSlave = false)
        {
            if (isSlave)
            {
                return this.ReadUnitWork.ExecuteQuerySql<DTO>(this.GetParametersSql(sql), this.GetParameter(paramNameList, paramValueList));
            }
            else
            {
                return this.unitOfWork.ExecuteQuerySql<DTO>(this.GetParametersSql(sql), this.GetParameter(paramNameList, paramValueList));
            }
        }

        #endregion

        #region 私有方法

        #region 获取EF查询表达式

        /// <summary>
        /// 获取AND条件
        /// </summary>
        /// <param name="whereParams"></param>
        /// <returns></returns>
        private Expression<Func<TEntity, bool>> GetAndExpression(IList<WhereParam> whereParams)
        {
            ParameterExpression nickNameParam = Expression.Parameter(typeof(TEntity), "m");
            Expression expConstant = Expression.Constant(true);
            if (whereParams != null)
            {
                foreach (var queryParam in whereParams)
                {
                    if (!string.IsNullOrEmpty(queryParam.Value))
                        expConstant = Expression.AndAlso(expConstant, this.GetOrExpression(nickNameParam,queryParam));
                }
            }
            return Expression.Lambda<Func<TEntity, Boolean>>(expConstant, nickNameParam);
        }

        /// <summary>
        /// 获取OR条件
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        private Expression GetOrExpression(ParameterExpression nickNameParam,WhereParam queryParam)
        {
            string[] fieldList = queryParam.Field.Split('|');
            Expression expConstant = Expression.Constant(false);
            foreach (string field in fieldList)
            {
                if (this.GetProperty(typeof(TEntity), field) != null)
                {
                    expConstant = Expression.OrElse(expConstant, this.GetExpression(field, queryParam.Value, queryParam.Operator, queryParam.DataType.ToString(), nickNameParam));
                }
            }
            return expConstant;
        }


        /// <summary>
        /// 得到过滤表达式
        /// </summary>
        /// <param name="queryField">字段</param>
        /// <param name="queryValue">值</param>
        /// <param name="operationType">操作类型</param>
        /// <param name="dataType">数据类型</param>
        /// <returns></returns>
        private Expression GetExpression(string queryField, string queryValue, string operationType, string dataType, ParameterExpression param)
        {
            Expression opExpression = null;
            Expression rExpression = null;
            Expression lExpression = Expression.Property(param, typeof(TEntity).GetProperty(queryField.Split('.').First()));
            if (operationType.ToLower() != "in")
                rExpression = Expression.Constant(Convert.ChangeType(queryValue, GetTypeByString(dataType)));

            switch (operationType.ToLower())
            {
                case "=":
                    opExpression = Expression.Equal(lExpression, rExpression);
                    break;
                case "<>":
                    opExpression = Expression.NotEqual(lExpression, rExpression);
                    break;
                case ">":
                    opExpression = Expression.GreaterThan(lExpression, rExpression);
                    break;
                case ">=":
                    opExpression = Expression.GreaterThanOrEqual(lExpression, rExpression);
                    break;
                case "<":
                    opExpression = Expression.LessThan(lExpression, rExpression);
                    break;
                case "<=":
                    opExpression = Expression.LessThanOrEqual(lExpression, rExpression);
                    break;
                case "in":
                    //声明查询的属性
                    opExpression = Expression.Call
                    (
                      typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),
                      lExpression,
                      Expression.Constant(queryValue)
                    );
                    break;
                default:
                    //声明查询的属性
                    opExpression = Expression.Call
                    (
                      lExpression,
                      typeof(string).GetMethod("Contains", new Type[] { typeof(string) }),
                      Expression.Constant(queryValue)
                    );
                    break;
            }
            return opExpression;
        }


        /// <summary>
        /// 得到类的属性
        /// </summary>
        /// <param name="baseType">类型</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>PropertyInfo</returns>
        private PropertyInfo GetProperty(Type type, string propertyName)
        {
            string[] parts = propertyName.Split('.');

            return (parts.Length > 1) ? GetProperty(type.GetProperty(parts[0]).PropertyType, parts.Skip(1).Aggregate((a, i) => a + "." + i)) : type.GetProperty(propertyName);
        }

        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Type GetTypeByString(string type)
        {
            switch (type.ToLower())
            {
                case "bool":
                    return Type.GetType("System.Boolean", true, true);
                case "byte":
                    return Type.GetType("System.Byte", true, true);
                case "sbyte":
                    return Type.GetType("System.SByte", true, true);
                case "char":
                    return Type.GetType("System.Char", true, true);
                case "decimal":
                    return Type.GetType("System.Decimal", true, true);
                case "double":
                    return Type.GetType("System.Double", true, true);
                case "float":
                    return Type.GetType("System.Single", true, true);
                case "int":
                    return Type.GetType("System.Int32", true, true);
                case "uint":
                    return Type.GetType("System.UInt32", true, true);
                case "long":
                    return Type.GetType("System.Int64", true, true);
                case "ulong":
                    return Type.GetType("System.UInt64", true, true);
                case "object":
                    return Type.GetType("System.Object", true, true);
                case "short":
                    return Type.GetType("System.Int16", true, true);
                case "ushort":
                    return Type.GetType("System.UInt16", true, true);
                case "string":
                    return Type.GetType("System.String", true, true);
                case "date":
                case "datetime":
                    return Type.GetType("System.DateTime", true, true);
                case "guid":
                    return Type.GetType("System.Guid", true, true);
                default:
                    return Type.GetType(type, true, true);
            }
        }

        #endregion

        #region 执行原生SQL相关方法

        /// <summary>
        /// 创建分页SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pagination">分页类</param>
        /// <param name="sortList">排序字段</param>
        /// <returns></returns>
        private string CreatePagedSql(string sql, Pagination pagination,IList<SortField> sortList)
        {
            StringBuilder pagedSql = new StringBuilder();
            if (ConfigurationManager.AppSettings["DBType"] == "1")
            {
                pagedSql.AppendLine("SELECT tmp.* FROM (" + sql + ") tmp");
                pagedSql.AppendLine(this.GetSortSql(sortList));
                pagedSql.AppendLine("LIMIT " + ((pagination.PageIndex - 1) * pagination.PageSize) + "," + pagination.PageSize);
            }
            else if (ConfigurationManager.AppSettings["DBType"] == "2")
            {
                pagedSql.AppendLine("SELECT tmp.* FROM (");
                pagedSql.AppendLine("SELECT tmp.*, ROWNUM AS ROWINDEX  FROM(" + sql + ") tmp");
                pagedSql.AppendLine(this.GetSortSql(sortList));
                pagedSql.AppendLine(") tmp ");
                pagedSql.AppendLine("WHERE tmp.ROWINDEX > " + ((pagination.PageIndex - 1) * pagination.PageSize) + " AND tmp.ROWINDEX <= " + ((pagination.PageIndex) * pagination.PageSize));
            }
            else
            {
                pagedSql.AppendLine("SELECT tmp.* FROM (" + sql + ") tmp");
                pagedSql.AppendLine(this.GetSortSql(sortList));
                pagedSql.AppendLine("OFFSET " + ((pagination.PageIndex - 1) * pagination.PageSize) + " ROW FETCH NEXT " + pagination.PageSize + " ROW ONLY");
            }
            return pagedSql.ToString();
        }

        /// <summary>
        /// 获取排序SQL
        /// </summary>
        /// <param name="sortList"></param>
        /// <returns></returns>
        private string GetSortSql(IList<SortField> sortList)
        {
            StringBuilder sortSql = new StringBuilder();
            if (sortList.Count > 0)
            {
                sortSql.Append("ORDER BY ");
                foreach (var sort in sortList)
                {
                    sortSql.Append(sort.SortValue + " " + (!string.IsNullOrEmpty(sort.SortType) ? sort.SortType : "ASC")+",");
                }
            }
            return sortSql.ToString().TrimEnd(',');
        }


        /// <summary>
        /// 得到过滤表达式
        /// </summary>
        /// <param name="queryField">字段</param>
        /// <param name="operationType">操作类型</param>
        /// <returns></returns>
        private string GetCondition(string queryField, string operationType,string queryValue)
        {
            string condition = "";
            switch (operationType.ToLower().Trim().Substring(0, operationType.Trim().Length>6?6: operationType.Trim().Length))
            {
                case "in":
                    condition = string.Format("{0} {1} ({2})", queryField, operationType,this.GetInValue(queryValue));
                    break;
                case "exists":
                    condition = operationType;
                    break;
                default:
                    condition = string.Format("{0} {1} @{0}", queryField, operationType);
                    break;
            }
            return condition;
        }

        /// <summary>
        /// 获取in值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetInValue(string value)
        {
            IList<string> inValueList = new List<string>();
            string[] valueList = value.Replace("'", "").Split(',');
            foreach(var v in valueList)
            {
                inValueList.Add("'" + v + "'");
            }
            return string.Join(",", inValueList);
        }

        /// <summary>
        /// 获取不同数据库参数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private string GetParametersSql(string sql)
        {
            Regex reg = new Regex(@"(?<=[^'])(@)", RegexOptions.Multiline);
            if (ConfigurationManager.AppSettings["DBType"] == "1")
            {
                sql = reg.Replace(sql,"?");
            }
            else if (ConfigurationManager.AppSettings["DBType"] == "2")
            {
                sql = reg.Replace(sql, ":");
            }
            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramNameList">参数名</param>
        /// <param name="paramValueList">参数值</param>
        /// <returns></returns>
        private DbParameter[] GetParameter(string[] paramNameList, object[] paramValueList)
        {
            DbParameter[] dbParameters = null;
            if (ConfigurationManager.AppSettings["DBType"] == "1")
            {

            }
            else if (ConfigurationManager.AppSettings["DBType"] == "2")
            {
                if (paramNameList != null && paramNameList.Length > 0 && paramValueList != null && paramValueList.Length > 0)
                {
                    dbParameters = new OracleParameter[paramNameList.Length];
                    for (int i = 0; i < paramNameList.Length; i++)
                    {
                        if (paramValueList[i].GetType() == typeof(Guid))
                        {
                            dbParameters[i] = new OracleParameter(paramNameList[i], BitConverter.ToString((new Guid(paramValueList[i].ToString())).ToByteArray()).Replace("-", ""));
                        }
                        else
                        {
                            dbParameters[i] = new OracleParameter(paramNameList[i], paramValueList[i]);
                        }
                    }
                }
                else
                {
                    dbParameters = new OracleParameter[0];
                }
            }
            else
            {
                if (paramNameList != null && paramNameList.Length > 0 && paramValueList != null && paramValueList.Length > 0)
                {
                    dbParameters = new SqlParameter[paramNameList.Length];
                    for (int i = 0; i < paramNameList.Length; i++)
                    {
                        dbParameters[i] = new SqlParameter(paramNameList[i], paramValueList[i]);
                    }
                }
                else
                {
                    dbParameters = new SqlParameter[0];
                }
            }
            return dbParameters;
        }

        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        private object GetParamValue(string fieldValue,string fieldType)
        {
            object returnValue = null;
            switch (fieldType.ToLower())
            {
                case "bool":
                    returnValue = bool.Parse(fieldValue);
                    break;
                case "decimal":
                    returnValue = decimal.Parse(fieldValue);
                    break;
                case "double":
                    returnValue = double.Parse(fieldValue);
                    break;
                case "float":
                    returnValue = float.Parse(fieldValue);
                    break;
                case "int":
                    returnValue = int.Parse(fieldValue);
                    break;
                case "long":
                    returnValue = long.Parse(fieldValue);
                    break;
                case "short":
                    returnValue = short.Parse(fieldValue);
                    break;
                case "date":
                case "datetime":
                    returnValue = DateTime.Parse(fieldValue);
                    break;
                case "guid":
                    returnValue = Guid.Parse(fieldValue);
                    break;
                default:
                    returnValue = fieldValue;
                    break;
            }
            return returnValue;
        }


    #endregion

    #region 创建IDbSet

    /// <summary>
    /// 创建IDbSet
    /// </summary>
    /// <returns></returns>
    private IDbSet<TEntity> DbSet()
        {
            return this.unitOfWork.CreateSet<TEntity>();
        }

        /// <summary>
        /// 创建IDbSet
        /// </summary>
        /// <returns></returns>
        private IDbSet<TEntity> ReadDbSet()
        {
            return this.ReadUnitWork.CreateSet<TEntity>();
        }

        #endregion

        #endregion
    }
}
