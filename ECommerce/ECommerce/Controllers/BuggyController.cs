using Core.Entities;
using ECommerce.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("unauthorized")]
        public IActionResult GetAuthorized()
        {
            return Unauthorized();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest("Not a good request");
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("This is a test exception");
        }

       

        [HttpPost("validationerror")]
        public IActionResult GetValidationError(CreateProductDto product)
        {
            return Ok();
        }
    }
}
