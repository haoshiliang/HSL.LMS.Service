using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;
using System.Collections.Specialized;

namespace LMS.Application.Seedwork.Cache
{
    public class RedisConnectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var list = new List<RedisServer>();
            if (section.ChildNodes.Count > 0)
            {
                foreach(XmlNode childNode in section.ChildNodes)
                {
                    list.Add(new RedisServer() { ServerAddress = childNode.Attributes["serverAddress"].Value, ServerPort = childNode.Attributes["serverPort"].Value });
                }
            }
            return list;
        }
    }
}
