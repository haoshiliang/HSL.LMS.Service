﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection;
using System.Xml;
using LMS.Domain.Seedwork;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure;
using System.Configuration;
using System.Text.RegularExpressions;

namespace LMS.Infrastructure.Seedwork
{
    public class ReadUnitOfWork : DbContext, IReadUnitOfWork
    {
        #region 构造函数
        /// <summary>
        /// 连接数据库
        /// </summary>
        public ReadUnitOfWork() : base("SlaveConnctionString")
        {
            //该处可扩展，可更改数据库连接，假如有多台只读服务器
            //base.Database.Connection.ConnectionString = "";

            /*
                1.CreateDatabaseIfNotExists：这是默认的策略。如果数据库不存在，那么就创建数据库。但是如果数据库存在了，而且实体发生了变化，就会出现异常。
                2.DropCreateDatabaseIfModelChanges：此策略表明，如果模型变化了，数据库就会被重新创建，原来的数据库被删除掉了。
                3.DropCreateDatabaseAlways：此策略表示，每次运行程序都会重新创建数据库，这在开发和调试的时候非常有用。
                4.自定制数据库策略：可以自己实现IDatabaseInitializer来创建自己的策略。或者从已有的实现了IDatabaseInitializer接口的类派生。
            */
            Database.SetInitializer<ReadUnitOfWork>(new CreateDatabaseIfNotExists<ReadUnitOfWork>());
            Database.Log = LogManager.WriteLog;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 创建IDbSet
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// 自定义SQL查询数据集
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public IQueryable<TEntity> ExecuteQuerySql<TEntity>(string sql, object[] paramList)
        {
            return base.Database.SqlQuery<TEntity>(sql, paramList).AsQueryable<TEntity>();
        }

        #endregion

        #region 数据库映射
        /// <summary>
        /// 重写
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string conntionString = ConfigurationManager.ConnectionStrings["MasterConnctionString"].ConnectionString.ToUpper();

            if (ConfigurationManager.AppSettings["DBType"] == "2")
            {
                Regex connRegex = new Regex(@";USER ID\s*=\s*([^;]*)");
                MatchCollection connRegexList = connRegex.Matches(conntionString);
                if (connRegexList.Count > 0)
                {
                    modelBuilder.HasDefaultSchema(connRegexList[0].Groups[1].Value);
                }
            }

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            var assemblyConfig = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(assemblyConfig + "/AssemblyConfig/Assembly.xml");
            XmlNodeList list = xmlDoc.SelectNodes("Assembly/Item");
            foreach (XmlNode item in list)
            {
                modelBuilder.Configurations.AddFromAssembly(Assembly.Load(item.InnerText));
            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
