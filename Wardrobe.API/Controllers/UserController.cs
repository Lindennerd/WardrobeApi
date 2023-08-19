using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wardrobe.Domain.User;

namespace Wardrobe.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    public UserController()
    {
        
    }

    [HttpPost]
    public async Task EditUser([FromBody] User user)
    {
        return;
    }
    
}