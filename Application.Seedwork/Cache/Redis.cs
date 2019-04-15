using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Configuration;

namespace LMS.Application.Seedwork.Cache
{
    /// <summary>
    /// 缓存实现,采用高可用的哨兵模式
    /// </summary>
    public class Redis : ICache
    {
        #region 私有变量

        /// <summary>
        /// Redis服务连接
        /// </summary>
        private static ConnectionMultiplexer redisConn;
        private static IDatabase database;
        private static ISubscriber sentinelsub;
        private static ConnectionMultiplexer sentinelConn;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        public Redis()
        {
        }
         
        #endregion

        #region 静态构造方法 

        /// <summary>
        /// 首先启动时执行
        /// </summary>
        static Redis()
        {
            var serverList = ConfigurationManager.GetSection("redisConnection") as List<RedisServer>;
            var hostServerList = serverList.Where(m => m.RedisType == 0).ToList();
            var sentinelServerList = serverList.Where(m => m.RedisType == 1).ToList();
            if (hostServerList.Count == 0 || sentinelServerList.Count == 0)
                throw new ApplicationException("请配置Redis主服务地址与哨兵服务地址");

            //创建连接
            CreateRedisConn(hostServerList);
            //创建哨兵
            CreateSentinelConn(sentinelServerList);
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="hostServerList"></param>
        private static void CreateRedisConn(List<RedisServer> hostServerList)
        {
            //是一个列表，一个复杂的的场景中可能包含有主从复制 ， 对于这种情况，只需要指定所有地址在连接字符串中
            //（它将会自动识别出主服务器 set值得时候用的主服务器）假设这里找到了两台主服务器，将会对两台服务进行裁决选出一台作为主服务器
            //来解决这个问题 ， 这种情况是非常罕见的 ，我们也应该避免这种情况的发生。
            ConfigurationOptions config = new ConfigurationOptions();
            foreach(var hostServer in hostServerList)
            {
                config.EndPoints.Add(hostServer.ServerAddress, int.Parse(hostServer.ServerPort));
            }
            //服务器秘密
            config.Password = "123456";
            //客户端名字
            config.ClientName = "127.0.0.1";
            //服务器名字
            config.ServiceName = "127.0.0.1";
            //true表示管理员身份，可以用一些危险的指令。
            config.AllowAdmin = true;
            redisConn = ConnectionMultiplexer.Connect(config);
            database = redisConn.GetDatabase();
        }

        /// <summary>
        /// 创建哨兵
        /// </summary>
        private static void CreateSentinelConn(List<RedisServer> sentinelServerList)
        {
            var sentinelOptions = new ConfigurationOptions() { TieBreaker = "" };
            //添加哨兵地址
            foreach (var hostServer in sentinelServerList)
            {
                sentinelOptions.EndPoints.Add(hostServer.ServerAddress, int.Parse(hostServer.ServerPort));
            }
            //sentinelOptions.Password = "123456";
            //哨兵连接模式
            sentinelOptions.CommandMap = CommandMap.Sentinel;
            sentinelOptions.ServiceName = "mymaster";
            sentinelConn = ConnectionMultiplexer.Connect(sentinelOptions);
            sentinelsub = sentinelConn.GetSubscriber();
            sentinelsub.Subscribe("+switch-master", (ch, mg) =>
            {
                Console.WriteLine((string)mg);
            });
        }

        #endregion

        #region 接口实现 

        public string Get(string key)
        {
            return Get<string>(key);
        }

        public T Get<T>(string key)
        {
            var cacheValue = database.StringGet(key,CommandFlags.PreferMaster);
            var value = default(T);
            if (!cacheValue.IsNull)
            {
                var jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
                value = JsonConvert.DeserializeObject<T>(cacheValue, jsonConfig);
            }
            return value;
        }

        public void Insert(string key, object data)
        {
            var jsonData = this.GetJsonData(data);
            database.StringSet(key, jsonData);
        }

        public void Insert(string key, object data, int cacheTime)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData(data);
            database.StringSet(key, jsonData, timeSpan);
        }

        public void Insert(string key, object data, DateTime cacheTime)
        {
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData(data);
            database.StringSet(key, jsonData, timeSpan);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            database.KeyDelete(key, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public bool Exists(string key)
        {
            return database.KeyExists(key);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取实体JSON
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetJsonData(object data)
        {
            var jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(data, jsonConfig);//序列化对象
        }

        #endregion
    }
}
