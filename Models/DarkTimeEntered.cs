using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkTimeServices.Models
{
    public class DarkTimeEntered
    {
        public string ReporterName { get; set; }
        public string Room { get; set; }
        public int SizeKB { get; set; }
        public int PeriodMinutes { get; set; }
        public DateTime ReceivedDTS { get; set; }
    }
}
