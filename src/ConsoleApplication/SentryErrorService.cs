using SharpRaven;
using SharpRaven.Data;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class SentryErrorService : IErrorReportingService
    {
        private readonly RavenClient _sentry;

        public SentryErrorService(string sentryDSN)
        {
            if (string.IsNullOrWhiteSpace(sentryDSN))
            {
                throw new ArgumentException("message", nameof(sentryDSN));
            }

            _sentry = new RavenClient(sentryDSN)
            {
                Environment = "Test",
                Release = "1.0.0.0"
            };
        }

        public Task ReportErrorAsync(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                throw new ArgumentException("message", nameof(error));
            }

            return _sentry.CaptureAsync(new SentryEvent(error));
        }

        public Task ReportExceptionAsync(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            exception.Data.Add("SomeKey", "Some extra data!");

            return _sentry.CaptureAsync(new SentryEvent(exception));
        }
    }
}
