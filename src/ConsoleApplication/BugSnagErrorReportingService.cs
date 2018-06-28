using System;
using System.Threading.Tasks;
using Bugsnag;

namespace ConsoleApplication
{
    /// <summary>
    /// Sample implementation to use. Use this, or create a new implementation.
    /// </summary>
    public class BugSnagErrorReportingService : IErrorReportingService
    {
        private readonly IClient _bugsnag;

        public BugSnagErrorReportingService(IClient client) // pass in whatever credentials needed for service
        {
            _bugsnag = client;
        }

        public Task ReportErrorAsync(string error)
        {
            _bugsnag.Notify(new Exception(error), Severity.Error);
            Console.WriteLine($"BugSnagErrorReportingService: {error}");
            return Task.CompletedTask;
        }

        public Task ReportExceptionAsync(Exception exception)
        {
            _bugsnag.Notify(exception, Severity.Warning);
            Console.WriteLine($"BugSnagErrorReportingService: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}