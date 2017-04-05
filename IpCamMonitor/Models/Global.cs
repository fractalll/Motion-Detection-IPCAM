using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IpCamMonitor.Models
{
    public static class Global
    {
        public static string RootFolder
        {
            get
            {    //TODO: перенести в конфиги web.config
                return "C:\\IPCamMonitorUtil\\";
            }
        }

    }
}