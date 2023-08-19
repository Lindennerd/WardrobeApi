using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wardrobe.Application.Image.BackgroundRemoval;
using Wardrobe.CrossCutting;
using Wardrobe.Domain.Cloth;
using Wardrobe.Infra.Database.Cloth;

namespace Wardrobe.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ImageController : ControllerBase
{
    private readonly IBackgroundRemovalQueueService _backgroundRemovalQueueService;
    private readonly IClothesRepository _clothesRepository;
    private readonly ILogger<ImageController> _logger;

    public ImageController(
        IBackgroundRemovalQueueService  backgroundRemovalQueueService,
        IClothesRepository clothesRepository,
        ILogger<ImageController> logger)
    {
        _backgroundRemovalQueueService = backgroundRemovalQueueService;
        _clothesRepository = clothesRepository;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<ActionResult> SendImageToBackgroundRemovalQueue(IFormFile file)
    {
        try
        {
            var model = await this._clothesRepository.Save(new Cloth
            {
                Owner = User.Identity.Name,
                Name = file.FileName,
                MimeType = file.GetMimeType(),
                Image = file.ConvertToBase64()
            });
        
            this._backgroundRemovalQueueService.SendImageToBackgroundRemovalQueue(model.Id, file.ConvertToBase64(), file.FileName,
                file.GetMimeType());
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while sending image to background removal queue");
            throw;
        }
    }
}