using MassTransit;
using Wardrobe.Application.Image.Classification;
using Wardrobe.Domain.Image.Events;

namespace Wardrobe.ImageClassificationService;

public class ImageClassificationConsumer : IConsumer<IImageClassificationEvent>
{
    private readonly ClassificationService _classificationService;
    private readonly ILogger<ImageClassificationConsumer> _logger;

    public ImageClassificationConsumer(
        ClassificationService classificationService,
        ILogger<ImageClassificationConsumer> logger)
    {
        _classificationService = classificationService;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<IImageClassificationEvent> context)
    {
        await this._classificationService.ClassifyImage(
            context.Message.Id,
            context.Message.ImageBase64,
            context.Message.FileName
        );
    }
}