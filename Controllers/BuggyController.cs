﻿
using Microsoft.AspNetCore.Mvc;


namespace ReStore.Controllers
{
    public class BuggyController:BaseApiController
    {

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ProblemDetails {Title=" This is a bad request " } );
        }

        [HttpGet ("unauthorized")]
        public ActionResult GetUnauthorized()
        {
            return Unauthorized();
        }
        [HttpGet("validation-error")]
        public ActionResult GetValidationError()
        {
            ModelState.AddModelError("Problem 1", "This is the first error");
            ModelState.AddModelError("Problem 2", "This is the second error");
            return ValidationProblem(ModelState);
        }

        [HttpGet ("server-error")]
        public ActionResult GetSeverError()
        {
            throw new Exception("This is a Server Error");
        }

    }
}