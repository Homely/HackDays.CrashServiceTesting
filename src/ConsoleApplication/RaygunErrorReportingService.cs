using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.AspNetCore;
using System;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    /// <summary>
    /// Sample implementation to use. Use this, or create a new implementation.
    /// </summary>
    public class RaygunErrorReportingService : IErrorReportingService
    {
        private static RaygunClient _raygunClient;

        public RaygunErrorReportingService(string apiKey) // pass in whatever credentials needed for service
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentException(nameof(apiKey));
            }

            _raygunClient = new RaygunClient(apiKey);
        }

        public Task ReportErrorAsync(string errorMessage)
        {
            var error = new RaygunErrorMessage
            {
                Message = errorMessage
            };

            var details = new RaygunMessageDetails
            {
                Error = error
            };

            var message = new RaygunMessage()
            {
                Details = details,
                OccurredOn = DateTime.UtcNow
            };

            _raygunClient.SendInBackground(message);
            return Task.CompletedTask;
        }

        public Task ReportExceptionAsync(Exception exception)
        {
            _raygunClient.SendInBackground(exception);
            return Task.CompletedTask;
        }
    }
}