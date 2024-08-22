using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SnackHub.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class UserController : ControllerBase
    {
        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userName = User.Identity?.Name;

            if (userName == null)
            {
                return Unauthorized();
            }

            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            return Ok(new
            {
                UserName = userName,
                Claims = claims
            });
        }
    }
}
