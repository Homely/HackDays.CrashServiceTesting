using System;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    /// <summary>
    /// Sample implementation to use. Use this, or create a new implementation.
    /// </summary>
    public class ExampleErrorReportingService : IErrorReportingService
    {
        public ExampleErrorReportingService() // pass in whatever credentials needed for service
        {
        }

        public Task ReportErrorAsync(string error)
        {
            Console.WriteLine($"ExampleErrorReportingService: {error}");
            return Task.CompletedTask;
        }

        public Task ReportExceptionAsync(Exception exception)
        {
            Console.WriteLine($"ExampleErrorReportingService: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}