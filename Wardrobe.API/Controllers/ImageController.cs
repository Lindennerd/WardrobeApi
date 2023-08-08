using Microsoft.AspNetCore.Mvc;
using Wardrobe.Application.Image.BackgroundRemoval;
using Wardrobe.Application.Image.Database;
using Wardrobe.CrossCutting;
using Wardrobe.Infra.Database.Cloth;

namespace Wardrobe.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly IBackgroundRemovalQueueService _backgroundRemovalQueueService;
    private readonly ISaveImage _saveImage;
    private readonly ILogger<ImageController> _logger;

    public ImageController(
        IBackgroundRemovalQueueService  backgroundRemovalQueueService,
        ISaveImage saveImage,
        ILogger<ImageController> logger)
    {
        _backgroundRemovalQueueService = backgroundRemovalQueueService;
        _saveImage = saveImage;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<ActionResult> SendImageToBackgroundRemovalQueue(IFormFile file)
    {
        try
        {
            var model = await this._saveImage.Save(new ClothesModel
            {
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