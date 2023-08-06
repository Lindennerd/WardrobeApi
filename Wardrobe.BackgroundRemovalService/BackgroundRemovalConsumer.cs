using MassTransit;
using Wardrobe.Application.Image.BackgroundRemoval.DTO;
using Wardrobe.Application.Image.BackgroundRemoval.Strategies.Http;
using Wardrobe.Application.Image.Classification;
using Wardrobe.Domain.Image.Events;

namespace Wardrobe.BackgroundRemovalService;

public class BackgroundRemovalConsumer : IConsumer<IImageBackgroundRemovalEvent>
{
    private readonly IBackgroundRemoval _backgroundRemoval;
    private readonly IClassificationQueueService _classificationQueueService;
    private readonly ILogger<BackgroundRemovalConsumer> _logger;

    public BackgroundRemovalConsumer(
        IBackgroundRemoval backgroundRemoval,
        IClassificationQueueService classificationQueueService,
        ILogger<BackgroundRemovalConsumer> logger)
    {
        _backgroundRemoval = backgroundRemoval;
        _classificationQueueService = classificationQueueService;
        _logger = logger;
    }
  
    public async Task Consume(ConsumeContext<IImageBackgroundRemovalEvent> context)
    {
        try
        {
            var result = await _backgroundRemoval.RemoveBackground(new BackgroundRemovalDTO(context.Message.ImageBase64, context.Message.FileMimeType));
            await this._classificationQueueService.SendImageToClassificationQueue(result, context.Message.FileName, context.Message.FileMimeType);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while consuming message");
            throw;
        }
    }
}