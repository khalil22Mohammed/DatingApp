using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controllers
{
    public class BuggyController : BaesAPIController
    {
        [HttpGet("auth")]
        public ActionResult GetAuth()
        {
            return Unauthorized();
        }
        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("This is a bad request");
        }
    }
}
