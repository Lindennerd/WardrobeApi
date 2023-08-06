using System.Web;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Wardrobe.Application.Image;
using Wardrobe.Application.Image.BackgroundRemoval;
using Wardrobe.CrossCutting;

namespace Wardrobe.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly IBackgroundRemovalQueueService _backgroundRemovalQueueService;

    public ImageController(IBackgroundRemovalQueueService  backgroundRemovalQueueService)
    {
        _backgroundRemovalQueueService = backgroundRemovalQueueService;
    }
    
    [HttpPost]
    public async Task<ActionResult> PostImage(IFormFile file)
    {
        this._backgroundRemovalQueueService.SendImageToBackgroundRemovalQueue(file.ConvertToBase64(), file.FileName,
            file.GetMimeType());
        return Ok();
    }
}