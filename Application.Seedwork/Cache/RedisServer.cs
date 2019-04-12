using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Seedwork.Cache
{
    /// <summary>
    /// Redis主服务器
    /// </summary>
    public class RedisServer
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerAddress { get; set; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public string ServerPort { get; set; }
    }
}
