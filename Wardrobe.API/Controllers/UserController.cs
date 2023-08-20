using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wardrobe.Application.User;

namespace Wardrobe.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    public record SetAddressRequest(string Address);
    
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> SetAddress([FromBody] SetAddressRequest addressRequest)
    {
        try
        {
            await this._userService.EditAddress(addressRequest.Address, User.Identity.Name);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest(e.Message);
        }
    }
    
}