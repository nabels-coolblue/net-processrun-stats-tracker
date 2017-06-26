using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace serilog_event_capture_sink
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            log.Information("Hello world!");

            Console.ReadKey();
        }
    }
}
