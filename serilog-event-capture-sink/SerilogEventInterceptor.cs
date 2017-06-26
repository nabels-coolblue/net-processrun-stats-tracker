using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serilog_event_capture_sink
{
    public class SerilogEventInterceptor
    {
        private const LogEventLevel LOG_EVENT_ERROR_FLOOR_VALUE = LogEventLevel.Error;

        private IProcessRunStatisticsTracker _processRunStatisticsTracker { get; set; }

        public SerilogEventInterceptor(IProcessRunStatisticsTracker processRunStatisticsTracker)
        {
            _processRunStatisticsTracker = processRunStatisticsTracker;
        }

        public void Intercept(LogEvent logEvent)
        {
            if (logEvent.Level >= LOG_EVENT_ERROR_FLOOR_VALUE)
                _processRunStatisticsTracker.RegisterError(logEvent.RenderMessage());
        }
    }
}
