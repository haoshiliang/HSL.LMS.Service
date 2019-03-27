using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Transactions;

namespace LMS.Infrastructure.Seedwork
{
    /// <summary>
    /// 暂时不用，扩展多从库时使用
    /// </summary>
    public class DBRoute
    {
        /// <summary>
        /// 是否需要更新,当连接不一样才进行重新更改连接
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="connectionString"></param>
        private bool IsNeedUpdate(string connectionString)
        {
            DbConnectionStringBuilder sourceConn = new DbConnectionStringBuilder();
            sourceConn.ConnectionString = "";// MasterConnectionString;

            DbConnectionStringBuilder newConn = new DbConnectionStringBuilder();
            newConn.ConnectionString = connectionString;

            return !sourceConn.EquivalentTo(newConn);
        }


        /// <summary>
        /// 更新从数据库
        /// </summary>
        /// <param name="dbInterceptorContext"></param>
        private void UpdateToSlave(DbInterceptionContext dbInterceptorContext)
        {
            bool isDistributedTran = Transaction.Current != null && Transaction.Current.TransactionInformation.Status != TransactionStatus.Committed;
            foreach (var dbContext in dbInterceptorContext.DbContexts)
            {
                // 判断该 context 是否处于普通数据库事务中
                bool isDbTran = dbContext.Database.CurrentTransaction != null;
                string connectionString = (isDistributedTran || isDbTran ? "主库" : "从库");
                this.UpateConnection(dbContext.Database.Connection, connectionString);
            }
        }
        /// <summary>
        /// 更新连接
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="connectionString"></param>
        private void UpateConnection(DbConnection conn, string connectionString)
        {
            if (IsNeedUpdate(connectionString))
            {
                ConnectionState connState = conn.State;
                if (connState == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.ConnectionString = connectionString;
                conn.Open();
            }
        }
    }
}
