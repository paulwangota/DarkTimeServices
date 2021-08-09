using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using LogOperations;
namespace DarkTimeServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Audit.Load("DarkTimeServices", 20, "DarkTimeServices", Config.PARAM("DataHubDBServer"), Config.PARAM("DataHubDBName"));
#if DEBUG
            DarkTimeServices services = new DarkTimeServices();
            services.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DarkTimeServices()
            };
            ServiceBase.Run(ServicesToRun);

#endif

        }
    }
}
