using Rollbar;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class RollbarService : IErrorReportingService
    {
        public RollbarService(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentException("message", nameof(accessToken));
            }

            RollbarLocator.RollbarInstance.Configure(new RollbarConfig(accessToken));
        }

        public Task ReportErrorAsync(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                throw new ArgumentException("message", nameof(error));
            }

            RollbarLocator.RollbarInstance.Error(error);

            return Task.CompletedTask;
        }

        public Task ReportExceptionAsync(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            RollbarLocator.RollbarInstance.Error(exception);

            return Task.CompletedTask;
        }
    }
}
