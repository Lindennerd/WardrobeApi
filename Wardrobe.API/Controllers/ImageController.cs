using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wardrobe.Application.Image.BackgroundRemoval;
using Wardrobe.Application.Image.Upload;
using Wardrobe.CrossCutting;
using Wardrobe.Domain.Entities.Cloth;
using Wardrobe.Infra.Database.Cloth;

namespace Wardrobe.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ImageController : ControllerBase
{
    private readonly IBackgroundRemovalQueueService _backgroundRemovalQueueService;
    private readonly IUploadImageService _uploadImageService;
    private readonly IClothesRepository _clothesRepository;
    private readonly ILogger<ImageController> _logger;

    public ImageController(
        IBackgroundRemovalQueueService backgroundRemovalQueueService,
        IUploadImageService uploadImageService,
        IClothesRepository clothesRepository,
        ILogger<ImageController> logger)
    {
        _backgroundRemovalQueueService = backgroundRemovalQueueService;
        _uploadImageService = uploadImageService;
        _clothesRepository = clothesRepository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> SendImageToBackgroundRemovalQueue(IFormFile file)
    {
        try
        {
            var imageUrl = await _uploadImageService.Upload(
                file.GetStream(), file.FileName, User.Identity.Name);

            var model = await this._clothesRepository.Save(new Cloth
            {
                Owner = User.Identity.Name,
                Name = file.FileName,
                MimeType = file.GetMimeType(),
                Image = imageUrl
            });

            this._backgroundRemovalQueueService.SendImageToBackgroundRemovalQueue(model.Id, file.ConvertToBase64(),
                file.FileName,
                file.GetMimeType());
            return Ok(new { imageUrl });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while sending image to background removal queue");
            throw;
        }
    }
}