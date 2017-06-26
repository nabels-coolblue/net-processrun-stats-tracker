using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serilog_event_capture_sink
{
    public interface IProcessRunStatisticsTracker
    {
        ProcessRunStatisticsReport GetReport();
        void RegisterError(string error);
    }

    public class ProcessRunStatisticsTracker : IProcessRunStatisticsTracker
    {
        private List<string> _errors { get; set; } = new List<string>();

        public ProcessRunStatisticsReport GetReport()
        {
            var statistics = new ProcessRunStatisticsReport() { ErrorCount = _errors.Count, ErrorMessages = _errors };
            return statistics;
        }

        public void RegisterError(string error)
        {
            _errors.Add(error);
        }
    }
}
