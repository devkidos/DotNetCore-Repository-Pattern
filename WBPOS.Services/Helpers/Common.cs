using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WBPOS.Services.Helpers
{
    public class Common
    {
        public static void writeLog(string logValue)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            var writelogFlag = config["writelog"];

            if(writelogFlag == "yes")
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory + "log.txt";

                File.AppendAllText(dir, Environment.NewLine + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") + logValue);
            } 
        }
    }
}