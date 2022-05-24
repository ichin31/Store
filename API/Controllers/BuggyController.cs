using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    public class BuggyController :BaseApiController
    {
        [HttpGet("not-found")]
        public ActionResult GetNotFound() 
        {
            return NotFound();
        }
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ProblemDetails { Title = "This is A Bad Request" });
        }
        [HttpGet("unathorised")]
        public ActionResult GetUnathorised()
        {
            return Unauthorized();
        }
        [HttpGet("validation-error")]
        public ActionResult GetValidationError()
        {
            ModelState.AddModelError("Problem1", "This is the first problem");
            ModelState.AddModelError("Problem2", "This is the second error");
            return ValidationProblem();
        }
        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }
    }
}
