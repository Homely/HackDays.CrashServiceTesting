using Exceptionless;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class ExceptionlessService : IErrorReportingService
    {
        private readonly ExceptionlessClient _client;

        public ExceptionlessService(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException("message", nameof(apiKey));
            }

            _client = new ExceptionlessClient(c => {
                c.ApiKey = apiKey;
                c.SetVersion("1.0.0.1");
            });
        }

        public Task ReportErrorAsync(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                throw new ArgumentException("message", nameof(error));
            }

            _client.SubmitEvent(new Exceptionless.Models.Event
            {
                Message = error
            });

            return Task.CompletedTask;
        }

        public Task ReportExceptionAsync(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            _client.SubmitException(exception);

            return Task.CompletedTask;
        }
    }
}
