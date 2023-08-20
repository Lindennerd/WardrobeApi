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

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task SetAddress([FromBody] SetAddressRequest addressRequest)
    {
        await this._userService.EditAddress(addressRequest.Address, User.Identity.Name);
    }
    
}