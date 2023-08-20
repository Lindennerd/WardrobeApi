using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wardrobe.Application.Cloth;
using Wardrobe.Domain.Entities.Cloth;

namespace Wardrobe.API.Controllers;

[Route("api/[controller]/[action]")]
[Authorize]
[ApiController]
public class ClothController : ControllerBase
{
    private readonly IClothService _clothService;
    private readonly ILogger<ClothController> _logger;

    public ClothController(IClothService clothService, ILogger<ClothController> logger)
    {
        _clothService = clothService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Cloth>> GetUsersClothes()
    {
        try
        {
            return await this._clothService.GetClothesForUser(User.Identity.Name);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            throw;
        }
    }
    
}