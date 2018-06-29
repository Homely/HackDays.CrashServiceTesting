using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class AppInsightsService : IErrorReportingService
    {
        private readonly TelemetryClient _telemetryClient;

        public AppInsightsService(string instrumentationKey)
        {
            if (string.IsNullOrWhiteSpace(instrumentationKey))
            {
                throw new ArgumentException("message", nameof(instrumentationKey));
            }

            TelemetryConfiguration.Active.InstrumentationKey = instrumentationKey;
            _telemetryClient = new TelemetryClient { InstrumentationKey = instrumentationKey };
        }

        public Task ReportErrorAsync(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                throw new ArgumentException("message", nameof(error));
            }

            _telemetryClient.TrackTrace(error, SeverityLevel.Error);
            _telemetryClient.Flush();

            return Task.CompletedTask;
        }

        public Task ReportExceptionAsync(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            _telemetryClient.TrackException(exception);
            _telemetryClient.Flush();

            return Task.CompletedTask;
        }
    }
}