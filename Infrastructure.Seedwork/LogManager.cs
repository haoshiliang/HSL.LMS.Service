using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Web;

namespace LMS.Infrastructure.Seedwork
{
    public class LogManager
    {
        private static readonly object lockObj = new object();

        public static void WriteLog(string sql)
        {
            lock (lockObj)
            {
                string isEnableLog = ConfigurationManager.AppSettings["IsEnableLog"];
                if (isEnableLog == "1")
                {
                    string logPath = HttpContext.Current.Server.MapPath("~") + ConfigurationManager.AppSettings["LogPath"];
                    if (!Directory.Exists(logPath))
                    {
                        Directory.CreateDirectory(logPath);
                    }

                    string logFile = logPath + "/log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                    if (!System.IO.File.Exists(logFile))
                    {
                        File.Create(logFile).Dispose();
                    }
                    using (StreamWriter writer = new StreamWriter(logFile, true, Encoding.Default))
                    {
                        writer.WriteLine(sql);
                    }
                }
            }
        }
    }
}
