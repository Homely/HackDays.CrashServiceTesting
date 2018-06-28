using Microsoft.Extensions.Options;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Threading.Tasks;

namespace WebApplication.Configuration
{
    public class SentryErrorReporter : IErrorReporter
    {
        private readonly IRavenClient _client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SentryErrorReporter" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     options
        ///     or
        ///     Can not construct a SentryErrorReporter without a valid DSN!
        /// </exception>
        public SentryErrorReporter(IOptions<SentryOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrEmpty(options.Value.Dsn))
                throw new ArgumentNullException("Can not construct a SentryErrorReporter without a valid DSN!");

            _client = new RavenClient(options.Value.Dsn)
            {
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                Release = "1.0.1.0"
            };
        }

        /// <summary>
        ///     Captures the specified exception asynchronously and hands it off to an error handling service.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">exception</exception>
        public Task CaptureAsync(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            return _client.CaptureAsync(new SentryEvent(exception));
        }

        /// <summary>
        ///     Captures the specified message asynchronously and hands it off to an error handling service.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">message</exception>
        public Task CaptureAsync(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            return _client.CaptureAsync(new SentryEvent(message));
        }
    }
}