using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Serilog.Events;

namespace serilog_event_capture_sink
{
    class Program
    {
        static void Main(string[] args)
        {
            var processIntegrityChecker = new ProcessRunStatisticsTracker();

            var log = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Observers(events => events
                    .Do(evt => {
                        processIntegrityChecker.InterceptLogEvent(evt);
                    })
                    .Subscribe())
                .CreateLogger();

            log.Information("Hello world!");
            log.Information("Hello again world!");
            log.Error("I'm not getting a reply :(");
            log.Fatal("I'm outta here!");
            log.Information("World says hello!");

            var results = processIntegrityChecker.GetResults();

            log.Information($"Process ran with {results.ErrorCount} errors.");

            Console.ReadKey();
        }
    }

    public class ProcessRunStatisticsTracker
    {
        public ProcessRunStatistics GetResults()
        {
            var statistics = new ProcessRunStatistics() { ErrorCount = _logEvents.Count(logEvent => logEvent.Level >= LogEventLevel.Error) };
            return statistics;
        }

        private List<LogEvent> _logEvents { get; set; } = new List<LogEvent>();

        public void InterceptLogEvent(LogEvent logEvent)
        {
            _logEvents.Add(logEvent);
        }
    }

    public class ProcessRunStatistics
    {
        public int ErrorCount { get; set; }
    }
}
