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
            var serilogEventInterceptor= new SerilogEventInterceptor(processIntegrityChecker);

            var log = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Observers(events => events
                    .Do(evt => {
                        serilogEventInterceptor.Intercept(evt);
                    })
                    .Subscribe())
                .CreateLogger();

            log.Information("Hello world!");
            log.Information("Hello again world!");
            log.Error("Hmm, I'm not getting a reply :(");
            log.Fatal("Well then, I'm outta here!");

            var results = processIntegrityChecker.GetReport();

            log.Information($"Process ran with {results.ErrorCount} errors. Errors captured:");

            foreach (var message in results.ErrorMessages)
                log.Information($"Error: {message}");
            
            Console.ReadKey();
        }
    }

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

    public class ProcessRunStatisticsReport
    {
        public int ErrorCount { get; set; }

        public List<string> ErrorMessages { get; set; }
    }
}
