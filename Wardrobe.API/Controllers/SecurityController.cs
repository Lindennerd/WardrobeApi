using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wardrobe.Application.Security;
using Wardrobe.CrossCutting.Exceptions;

namespace Wardrobe.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly ILogger<SecurityController> _logger;

        public record LoginRequest(string User, string Password);
        public record RegisterRequest(string Email, string Password);

        public SecurityController(ISecurityService securityService, ILogger<SecurityController> logger)
        {
            _securityService = securityService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation("Login request received for {User}", loginRequest.User);
                var (token, user) = await this._securityService.Login(loginRequest.User, loginRequest.Password);
                return Ok(new { token, user });
            }
            catch (UnauthorizedUser unauthorizedUser)
            {
                _logger.LogInformation(unauthorizedUser.Message);
                return Unauthorized();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                _logger.LogInformation("Registration request received from {Email}", registerRequest.Email);
                await _securityService.Register(registerRequest.Email, registerRequest.Password);
                return Ok();
            }
            catch (UserAlreadyExistsException userAlreadyExistsException)
            {
                _logger.LogInformation(userAlreadyExistsException.Message);
                return BadRequest(userAlreadyExistsException.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return BadRequest();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }
    }
}
