using Microsoft.Extensions.Options;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Threading.Tasks;

namespace WebApplication.Configuration
{
    public interface IErrorReporter
    {
        Task CaptureAsync(Exception exception);
        Task CaptureAsync(string message);
    }
}