using Bugsnag;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    static class Program
    {
        private static IErrorReportingService _errorReportingService;

        private static void Main()
        {
            Console.WriteLine("Hi! This is an app to test how a service can record/report on some errors in a console/background application.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // Setup.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var service = configuration.GetSection("BugsnagService");
            Console.WriteLine($"Api key: {service["apiKey"]}");
            Console.WriteLine($"Secret: {service["secret"]}");

            var bugSnagConfiguration = new Configuration
            {
                AutoCaptureSessions = true,
                ApiKey = service["apiKey"],
                AppType = "Console app",
                AppVersion = "1.1",
                IgnoreClasses = new[]       // can ignore fluentvalidation ++++++++
                {
                    typeof(NotImplementedException),
                    typeof(ArgumentOutOfRangeException)
                }
            };

            var bugSnag = new Client(bugSnagConfiguration);
            _errorReportingService = new BugSnagErrorReportingService(bugSnag); // change to service being tested.

            // Go!
            ReportSomeErrorsAsync().Wait();
        }

        private static async Task ReportSomeErrorsAsync()
        {
            // Error #1: Manual 'Exception'.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to record some manual 'exceptions'...");
            Console.ReadKey();

            try
            {
                throw new InvalidDataException();
            }
            catch (InvalidDataException exception)
            {
                await _errorReportingService.ReportExceptionAsync(exception);
            }
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException exception)
            {
                await _errorReportingService.ReportExceptionAsync(exception);
            }
            try
            {
                throw new Exception();
            }
            catch (Exception exception)
            {
                await _errorReportingService.ReportExceptionAsync(exception);
            }

            try
            {
                throw new Exception("Exception");
            }
            catch (Exception exception)
            {
                await _errorReportingService.ReportExceptionAsync(exception);
            }

            try
            {
                throw new Exception("Exception with inner exception", new Exception("Inner exception"));
            }
            catch (Exception exception)
            {
                await _errorReportingService.ReportExceptionAsync(exception);
            }

            // Error #2: Manual 'Error'.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to record a manual 'error'...");
            Console.ReadKey();
            await _errorReportingService.ReportErrorAsync("This is a manual error");

            // Error #3: Application crash.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Okay, now we're going to actually crash...to see if the tool has some magic 'hooks' to record on it. Press any key, and then hold onto your butts!!");
            Console.ReadKey();
            throw new Exception("This application has crashed! Arrrgghhhhh!");
        }
    }
}