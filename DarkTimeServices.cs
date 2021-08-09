using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DarkTimeServices.Mail;

namespace DarkTimeServices
{
    public partial class DarkTimeServices : ServiceBase
    {

        private static bool _isDarkTime_Timer = false;
        public DarkTimeServices()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            object obj = new object();
            lock (obj)
            {
                ReadDarkTimeData();
            }

            obj = null;
        }
        protected override void OnStart(string[] args)
        {
            object obj = new object();
            lock (obj)
            {

                try
                {

                  ReadDarkTimeData();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                }
            }
            obj = null;
        }


        private static void ReadDarkTimeData()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds; // read data every 1 minute (test) , change to 30 minutes
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!_isDarkTime_Timer)
            {
                _isDarkTime_Timer = true; // enable time
                MsExchangeMail.ReadDarkTimeEmails();
                


                _isDarkTime_Timer = false; // disable timer
            }
        }

        protected override void OnStop()
        {

            try
            {
                // disable timer
                _isDarkTime_Timer = false;

            }
            catch (Exception ex) {

            }
            finally { }          
        
        }

        private void DarkTimeServiceLog_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {
            //  throw new System.NotImplementedException();
        }
    }
}
