using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

namespace LMS.Domain.Seedwork
{
    public interface IUnitOfWork
    {
        #region 事件处理

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 启动事务，在执行原生SQL中使用
        /// </summary>
        //void BeginTrans();

        #endregion

    }
}
