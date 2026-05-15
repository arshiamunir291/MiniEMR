using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniEMR.Models.AuthModels;
using MiniEMR.Services;
using MiniEMR.Services.Interfaces;

namespace MiniEMR.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var result = await authService.LoginAsync(request);
            if (result == null) return Unauthorized(new {message="Invalid credentials"});
            return Ok(result);
        }
        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(typeof(UserDetail), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDetail>> GetCurrentUser()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (userIdClaim == null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized();

            var user = await authService.GetUserDetailAsync(userId);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
