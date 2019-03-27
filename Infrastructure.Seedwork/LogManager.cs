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
        public static void WriteLog(string sql)
        {
            string isEnableLog = ConfigurationManager.AppSettings["IsEnableLog"];
            if(isEnableLog=="1")
            {
                string logPath = HttpContext.Current.Server.MapPath("~")+ ConfigurationManager.AppSettings["LogPath"];
                if(!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                string logFile = logPath + "/log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                if (!System.IO.File.Exists(logFile))
                {
                    FileStream stream = System.IO.File.Create(logFile);
                    stream.Close();
                    stream.Dispose();
                }
                using (StreamWriter writer = new StreamWriter(logFile, true))
                {
                    writer.WriteLine(sql);
                }
            }
        }
    }
}
