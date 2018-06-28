using System;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public interface IErrorReportingService
    {
        Task ReportExceptionAsync(Exception exception);
        Task ReportErrorAsync(string error);
    }
}