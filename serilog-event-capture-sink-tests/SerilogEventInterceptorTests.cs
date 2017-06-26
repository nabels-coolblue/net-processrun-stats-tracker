using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using serilog_event_capture_sink;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;
using Moq;
using Serilog.Events;
using System.Linq;
using System.Collections.Generic;

namespace serilog_event_capture_sink_tests
{
    [TestClass]
    public class SerilogEventInterceptorTests
    {
        [TestMethod]
        public void LogEventLevel_OfError_ShouldRegisterAnError()
        {
            IFixture fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            var trackerMock = fixture.Freeze<Mock<IProcessRunStatisticsTracker>>();
            var sut = fixture.Create<SerilogEventInterceptor>();
            var logEvent = CreateLogEvent(LogEventLevel.Error);

            sut.Intercept(logEvent);

            trackerMock.Verify(m => m.RegisterError(logEvent.RenderMessage(null)), Times.Once());
        }


        [TestMethod]
        public void LogEventLevel_OfFatal_ShouldRegisterAnError()
        {
            IFixture fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());

            var trackerMock = fixture.Freeze<Mock<IProcessRunStatisticsTracker>>();
            var sut = fixture.Create<SerilogEventInterceptor>();
            var logEvent = CreateLogEvent(LogEventLevel.Fatal);

            sut.Intercept(logEvent);

            trackerMock.Verify(m => m.RegisterError(logEvent.RenderMessage(null)), Times.Once());
        }

        private LogEvent CreateLogEvent(LogEventLevel level)
        {
            var logEvent = new LogEvent(new DateTimeOffset(), level, null, MessageTemplate.Empty, Enumerable.Empty<LogEventProperty>());
            return logEvent;
        }
    }
}
