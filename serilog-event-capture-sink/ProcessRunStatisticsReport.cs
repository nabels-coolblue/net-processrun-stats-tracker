using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serilog_event_capture_sink
{

    public class ProcessRunStatisticsReport
    {
        public int ErrorCount { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}
