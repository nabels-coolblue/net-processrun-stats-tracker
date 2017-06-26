using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

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
}
