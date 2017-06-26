using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using serilog_event_capture_sink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace serilog_event_capture_sink_tests
{
    [TestClass]
    public class ProcessRunStatisticsTrackerTests
    {
        [TestMethod]
        public void RegisteredErrors_AskedForReport_ShouldListTheErrors()
        {
            const int NUMBER_OF_ERRORS = 10;

            IFixture fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
            var sut = fixture.Create<ProcessRunStatisticsTracker>();

            for (int i = 0; i < NUMBER_OF_ERRORS; i++)
                sut.RegisterError(string.Empty);

            var report = sut.GetReport();
            report.ErrorCount.Should().Be(NUMBER_OF_ERRORS);
            report.ErrorMessages.Count.Should().Be(NUMBER_OF_ERRORS);
        }
    }
}
