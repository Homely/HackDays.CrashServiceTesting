using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApplication.Controllers
{
    [Route("")]
    public class ErrorsController : Controller
    {
        [HttpGet("")]
        public ViewResult Details()
        {
            return View();
        }

        // GET: api/<controller>
        [HttpGet("error1")]
        public ActionResult Error1()
        {
            throw new Exception();
        }

        [HttpGet("error2")]
        public ActionResult Error2()
        {
            throw new Exception("Some error occured");
        }

        [HttpGet("error3")]
        public ActionResult Error3()
        {
            var innerException = new Exception("i'm an inner exception");
            throw new Exception("Some error occured", innerException);
        }
    }
}
