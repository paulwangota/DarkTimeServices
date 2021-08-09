using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkTimeServices.Models
{
   public class DarkTimeRaw
    {
        public string ReporterName { get; set; }
        public string MessageRaw { get; set; }
        public string SizeTimeRaw { get; set; }
        public DateTime ReceivedDTS { get; set; }
    }
}
