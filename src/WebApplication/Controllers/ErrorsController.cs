using Bugsnag;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication.Controllers
{
    [Route("")]
    public class ErrorsController : Controller
    {
        private readonly IClient _bugsnagClient;

        public ErrorsController(IClient client)
        {
            _bugsnagClient = client;
        }

        [HttpGet("")]
        public ViewResult Details()
        {
            return View();
        }

        // GET: api/<controller>
        [HttpGet("error1")]
        public ActionResult Error1()
        {
            try
            {
                _bugsnagClient.Notify(new Exception("about to throw an exception"), Severity.Info);
                throw new Exception();
            }
            catch (Exception excepiton)
            {
                _bugsnagClient.Notify(excepiton, Severity.Warning);
            }

            return null;
        }

        [HttpGet("error2")]
        public ActionResult Error2()
        {
            try
            {
                _bugsnagClient.Notify(new Exception("about to throw an exception with some error"), Severity.Info);
                throw new Exception("Some error occured");
            }
            catch (Exception exception)
            {
                _bugsnagClient.Notify(exception, Severity.Warning);
            }

            return null;
        }

        [HttpGet("error3")]
        public void Error3()
        {
            try
            {
                _bugsnagClient.Notify(new Exception("This has inner exception"), Severity.Info);
                var innerException = new Exception("i'm an inner exception");
                throw new Exception("Some error occured", innerException);
            }
            catch (Exception exception)
            {
                _bugsnagClient.Notify(exception, Severity.Error);
            }
        }
    }
}
