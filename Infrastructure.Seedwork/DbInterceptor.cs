using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.Common;
using System.Transactions;
using LMS.Domain.Seedwork;
using System.Threading;
using System.IO;

namespace LMS.Infrastructure.Seedwork
{
    public class DbInterceptor: DbCommandInterceptor
    {
        #region 重写函数
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
        }
        /// <summary>
        /// 取出第一条
        /// </summary>
        /// <param name="command"></param>
        /// <param name="interceptionContext"></param>
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
        }
        #endregion
    }
}
