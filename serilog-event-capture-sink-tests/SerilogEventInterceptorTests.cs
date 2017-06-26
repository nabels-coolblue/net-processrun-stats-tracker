using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using serilog_event_capture_sink;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;

namespace serilog_event_capture_sink_tests
{
    [TestClass]
    public class SerilogEventInterceptorTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            IFixture fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization()); 

        }
    }
}
