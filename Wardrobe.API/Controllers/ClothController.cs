using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wardrobe.Application.Cloth;
using Wardrobe.Domain.Cloth;

namespace Wardrobe.API.Controllers;

[Route("api/[controller]/[action]")]
[Authorize]
[ApiController]
public class ClothController : ControllerBase
{
    private readonly IClothService _clothService;

    public ClothController(IClothService clothService)
    {
        _clothService = clothService;
    }

    [HttpGet]
    public async Task<IEnumerable<Cloth>> GetUsersClothes()
    {
        return await this._clothService.GetClothesForUser(User.Identity.Name);
    }
    
}